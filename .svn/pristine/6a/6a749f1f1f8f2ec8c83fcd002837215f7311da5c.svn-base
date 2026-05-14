using System.Reflection;

namespace System.Data.OleDb;

/// <summary>
/// Represents an SQL transaction.
/// </summary>
[Obfuscation(Exclude = true, ApplyToMembers = false)]
public class OleDbTransaction : EmulatedDbTransaction
{
    internal OleDbTransaction(OleDbConnection connection, IsolationLevel isolationLevel) : base(connection, isolationLevel)
    {
    }

    public new OleDbConnection Connection => (OleDbConnection)DbConnection;
}
