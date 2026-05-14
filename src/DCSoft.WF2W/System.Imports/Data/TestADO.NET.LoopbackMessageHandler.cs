#if UNITTEST

using System.Net;
using System.Net.Http;
using System.Text;

namespace System.Data;

internal sealed class TestAdoNetLoopbackMessageHandler : HttpMessageHandler
{
    private readonly TestAdoNetServerApi _serverApi;

    internal TestAdoNetLoopbackMessageHandler(TestAdoNetServerApi serverApi)
    {
        _serverApi = serverApi;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        // 中文注释：模拟客户端请求，将请求内容发送到本地服务器 API。
        var payload = request.Content is null
            ? string.Empty
            : await request.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

        var response = await _serverApi.ExecuteAsync(payload, cancellationToken).ConfigureAwait(false);
        return new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(response, Encoding.UTF8, "application/json")
        };
    }
}

#endif