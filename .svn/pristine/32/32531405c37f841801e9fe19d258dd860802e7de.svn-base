using System.Data.Common;
using System.Reflection;

namespace System.Data;

/// <summary>
/// ADO 请求参数模型�?
/// </summary>
[Obfuscation(Exclude = true, ApplyToMembers = false)]
public sealed class AdoParameterInfo
{
    /// <summary>
    /// 参数名�?
    /// </summary>
    public string ParameterName { get; set; } = string.Empty;

    /// <summary>
    /// 参数值�?
    /// </summary>
    public object? Value { get; set; }

    /// <summary>
    /// 参数类型�?
    /// </summary>
    public DbType DbType { get; set; }

    /// <summary>
    /// 参数方向�?
    /// </summary>
    public ParameterDirection Direction { get; set; }

    /// <summary>
    /// 是否可空�?
    /// </summary>
    public bool IsNullable { get; set; }
}
