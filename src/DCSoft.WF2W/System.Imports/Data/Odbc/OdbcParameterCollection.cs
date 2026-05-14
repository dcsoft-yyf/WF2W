using System.Reflection;

namespace System.Data.Odbc;

/// <summary>
/// Represents a collection of parameters relevant to an OdbcCommand.
/// </summary>
[Obfuscation(Exclude = true, ApplyToMembers = false)]
public class OdbcParameterCollection : EmulatedDbParameterCollection<OdbcParameter>
{
    public OdbcParameter Add(string parameterName, object? value)
    {
        var parameter = new OdbcParameter(parameterName, value);
        AddTyped(parameter);
        return parameter;
    }
}
