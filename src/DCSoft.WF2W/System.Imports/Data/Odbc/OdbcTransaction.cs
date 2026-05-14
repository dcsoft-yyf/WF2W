using System.Reflection;

namespace System.Data.Odbc;

/// <summary>
/// Represents an Odbc transaction.
/// </summary>
[Obfuscation(Exclude = true, ApplyToMembers = false)]
public class OdbcTransaction : EmulatedDbTransaction
{
    internal OdbcTransaction(OdbcConnection connection, IsolationLevel isolationLevel) : base(connection, isolationLevel)
    {
    }

    public new OdbcConnection Connection => (OdbcConnection)DbConnection;
}
