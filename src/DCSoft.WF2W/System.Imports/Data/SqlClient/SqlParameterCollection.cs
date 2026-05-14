using System.Reflection;

namespace System.Data.SqlClient;

/// <summary>
/// Represents a collection of parameters relevant to a SqlCommand.
/// </summary>
[Obfuscation(Exclude = true, ApplyToMembers = false)]
public class SqlParameterCollection : EmulatedDbParameterCollection<SqlParameter>
{
    /// <summary>
    /// Adds a SqlParameter to the SqlParameterCollection.
    /// </summary>
    public SqlParameter Add(string parameterName, object? value)
    {
        var parameter = new SqlParameter(parameterName, value);
        AddTyped(parameter);
        return parameter;
    }
}
