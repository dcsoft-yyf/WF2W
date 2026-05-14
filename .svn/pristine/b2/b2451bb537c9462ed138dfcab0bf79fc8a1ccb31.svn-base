using System.Reflection;

namespace System.Data.OleDb;

/// <summary>
/// Represents a collection of parameters relevant to an OleDbCommand.
/// </summary>
[Obfuscation(Exclude = true, ApplyToMembers = false)]
public class OleDbParameterCollection : EmulatedDbParameterCollection<OleDbParameter>
{
    public OleDbParameter Add(string parameterName, object? value)
    {
        var parameter = new OleDbParameter(parameterName, value);
        AddTyped(parameter);
        return parameter;
    }
}
