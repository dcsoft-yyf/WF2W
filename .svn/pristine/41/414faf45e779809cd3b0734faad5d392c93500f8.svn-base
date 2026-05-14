using System.Reflection;

namespace System.Data.OracleClient;

/// <summary>
/// Represents a collection of parameters relevant to an OracleCommand.
/// </summary>
[Obfuscation(Exclude = true, ApplyToMembers = false)]
public class OracleParameterCollection : EmulatedDbParameterCollection<OracleParameter>
{
    public OracleParameter Add(string parameterName, object? value)
    {
        var parameter = new OracleParameter(parameterName, value);
        AddTyped(parameter);
        return parameter;
    }
}
