using System.Reflection;

namespace System.Data.OracleClient;

/// <summary>
/// Represents an Oracle transaction.
/// </summary>
[Obfuscation(Exclude = true, ApplyToMembers = false)]
public class OracleTransaction : EmulatedDbTransaction
{
    internal OracleTransaction(OracleConnection connection, IsolationLevel isolationLevel) : base(connection, isolationLevel)
    {
    }

    public new OracleConnection Connection => (OracleConnection)DbConnection;
}
