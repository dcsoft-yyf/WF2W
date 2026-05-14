using System.Collections.Concurrent;
using System.Reflection;
using System.Text.Json;

namespace System.Data;

/// <summary>
/// 内存 SQL 存储器。
/// </summary>
[Obfuscation(Exclude = true, ApplyToMembers = false)]
public static class InMemorySqlStore
{
    private static readonly ConcurrentQueue<AdoRequestEnvelope> Entries = new();

    /// <summary>
    /// 添加一个请求条目。
    /// </summary>
    public static void Add(AdoRequestEnvelope request)
    {
        // 中文注释：添加请求条目，用于记录和测试验证。
        Entries.Enqueue(request);
    }

    /// <summary>
    /// 导出所有请求条目为 JSON 字符串。
    /// </summary>
    /// </summary>
    public static string ExportJson()
    {
        return JsonSerializer.Serialize(Entries.ToArray(), AdoJsonOptions.Options);
    }

    /// <summary>
    /// 清空所有请求条目。
    /// </summary>
    public static void Clear()
    {
        while (Entries.TryDequeue(out _))
        {
        }
    }
}
