using System.Data.Common;
using System.Reflection;

namespace System.Data.SqlClient;

/// <summary>
/// Represents a connection to a SQL Server database.
/// </summary>
[Obfuscation(Exclude = true, ApplyToMembers = false)]
public class SqlConnection : EmulatedDbConnection
{
    /// <summary>
    /// Initializes a new instance of the SqlConnection class.
    /// </summary>
    public SqlConnection()
    {
    }

    /// <summary>
    /// Initializes a new instance of the SqlConnection class when given a string that contains the connection string.
    /// </summary>
    public SqlConnection(string connectionString) : base(connectionString)
    {
    }

    /// <summary>
    /// Begins a database transaction.
    /// </summary>
    public new SqlTransaction BeginTransaction()
    {
        return (SqlTransaction)BeginDbTransaction(IsolationLevel.ReadCommitted);
    }

    /// <summary>
    /// Begins a database transaction with the specified isolation level.
    /// </summary>
    public new SqlTransaction BeginTransaction(IsolationLevel isolationLevel)
    {
        return (SqlTransaction)BeginDbTransaction(isolationLevel);
    }

    /// <summary>
    /// Creates and returns a SqlCommand object associated with the SqlConnection.
    /// </summary>
    public new SqlCommand CreateCommand()
    {
        return (SqlCommand)base.CreateCommand();
    }

    protected override DbTransaction BeginDbTransaction(IsolationLevel isolationLevel)
    {
        return new SqlTransaction(this, isolationLevel);
    }

    protected override DbCommand CreateProviderCommand()
    {
        return new SqlCommand { Connection = this };
    }
}
