using System.Data.Common;

namespace System.Data;

[System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = false)]
public abstract class EmulatedDbParameter : DbParameter
{
    public override DbType DbType { get; set; }
    public override ParameterDirection Direction { get; set; } = ParameterDirection.Input;
    public override bool IsNullable { get; set; }
    public override string ParameterName { get; set; } = string.Empty;
    public override string SourceColumn { get; set; } = string.Empty;
    public override object? Value { get; set; }
    public override bool SourceColumnNullMapping { get; set; }
    public override int Size { get; set; }

    /// <summary>
    /// Resets the type associated with this parameter.
    /// </summary>
    public override void ResetDbType()
    {
        // 中文注释：模拟器无需重置实际数据库类型�?
    }
}
