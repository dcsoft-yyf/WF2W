using System.Text.Json;

namespace System.Data;
using System;
using System.Threading.Tasks;
using System.Threading;
internal sealed class NullAdoDataForwarder : IAdoDataForwarder
{
    public Task<string> SendAsync(string payload, CancellationToken cancellationToken = default)
    {
        var result = new AdoServerResult
        {
            Success = false,
            IsQuery = false,
            AffectedRows = 0,
            Message = "Default IAdoDataForwarder is not configured."
        };
        return Task.FromResult(JsonSerializer.Serialize(result, AdoJsonOptions.Options));
    }
}
