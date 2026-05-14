using System.Reflection;

namespace System.Data;

/// <summary>
/// 服务器执行结果模型�?
/// </summary>
[Obfuscation(Exclude = true, ApplyToMembers = false)]
public sealed class AdoServerResult
{
    /// <summary>
    /// 是否成功�?
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// 是否查询命令�?
    /// </summary>
    public bool IsQuery { get; set; }

    /// <summary>
    /// 影响行数�?
    /// </summary>
    public int AffectedRows { get; set; }

    /// <summary>
    /// DataSet XML 字符串�?
    /// </summary>
    public string? DataSetJson { get; set; }

    /// <summary>
    /// 提示信息�?
    /// </summary>
    public string? Message { get; set; }

    /// <summary>
    /// 标量返回值�?
    /// </summary>
    public object? Scalar { get; set; }
}
