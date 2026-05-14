using System.Data.Common;
using System.Reflection;

namespace System.Data.OleDb;

/// <summary>
/// Represents an open connection to a data source.
/// </summary>
[Obfuscation(Exclude = true, ApplyToMembers = false)]
public class OleDbConnection : EmulatedDbConnection
{
    public OleDbConnection()
    {
    }

    public OleDbConnection(string connectionString) : base(connectionString)
    {
    }

    public new OleDbTransaction BeginTransaction() => (OleDbTransaction)BeginDbTransaction(IsolationLevel.ReadCommitted);

    public new OleDbTransaction BeginTransaction(IsolationLevel isolationLevel) => (OleDbTransaction)BeginDbTransaction(isolationLevel);

    public new OleDbCommand CreateCommand() => (OleDbCommand)base.CreateCommand();

    protected override DbTransaction BeginDbTransaction(IsolationLevel isolationLevel) => new OleDbTransaction(this, isolationLevel);

    protected override DbCommand CreateProviderCommand() => new OleDbCommand { Connection = this };
}
