using System.Reflection;

namespace System.Data;

/// <summary>
/// ADO 模拟请求模型�?
/// </summary>
[Obfuscation(Exclude = true, ApplyToMembers = false)]
public sealed class AdoRequestEnvelope
{
    /// <summary>
    /// 数据提供程序名称�?
    /// </summary>
    public string Provider { get; set; } = string.Empty;

    /// <summary>
    /// 连接字符串�?
    /// </summary>
    public string ConnectionString { get; set; } = string.Empty;

    /// <summary>
    /// 命令文本�?
    /// </summary>
    public string CommandText { get; set; } = string.Empty;

    /// <summary>
    /// 命令类型�?
    /// </summary>
    public CommandType CommandType { get; set; }

    /// <summary>
    /// 超时时间�?
    /// </summary>
    public int CommandTimeout { get; set; }

    /// <summary>
    /// 执行动作�?
    /// </summary>
    public string Operation { get; set; } = string.Empty;

    /// <summary>
    /// 事务标识�?
    /// </summary>
    public string? TransactionId { get; set; }

    /// <summary>
    /// 参数集合�?
    /// </summary>
    public List<AdoParameterInfo> Parameters { get; set; } = new();
}
