using System.Data.Common;
using System.Reflection;

namespace System.Data.OracleClient;

/// <summary>
/// Represents an open connection to an Oracle database.
/// </summary>
[Obfuscation(Exclude = true, ApplyToMembers = false)]
public class OracleConnection : EmulatedDbConnection
{
    public OracleConnection()
    {
    }

    public OracleConnection(string connectionString) : base(connectionString)
    {
    }

    public new OracleTransaction BeginTransaction() => (OracleTransaction)BeginDbTransaction(IsolationLevel.ReadCommitted);

    public new OracleTransaction BeginTransaction(IsolationLevel isolationLevel) => (OracleTransaction)BeginDbTransaction(isolationLevel);

    public new OracleCommand CreateCommand() => (OracleCommand)base.CreateCommand();

    protected override DbTransaction BeginDbTransaction(IsolationLevel isolationLevel) => new OracleTransaction(this, isolationLevel);

    protected override DbCommand CreateProviderCommand() => new OracleCommand { Connection = this };
}
