using System.Data.Common;
using System.Reflection;

namespace System.Data.Odbc;

/// <summary>
/// Represents an open connection to a data source.
/// </summary>
[Obfuscation(Exclude = true, ApplyToMembers = false)]
public class OdbcConnection : EmulatedDbConnection
{
    public OdbcConnection()
    {
    }

    public OdbcConnection(string connectionString) : base(connectionString)
    {
    }

    public new OdbcTransaction BeginTransaction() => (OdbcTransaction)BeginDbTransaction(IsolationLevel.ReadCommitted);

    public new OdbcTransaction BeginTransaction(IsolationLevel isolationLevel) => (OdbcTransaction)BeginDbTransaction(isolationLevel);

    public new OdbcCommand CreateCommand() => (OdbcCommand)base.CreateCommand();

    protected override DbTransaction BeginDbTransaction(IsolationLevel isolationLevel) => new OdbcTransaction(this, isolationLevel);

    protected override DbCommand CreateProviderCommand() => new OdbcCommand { Connection = this };
}
