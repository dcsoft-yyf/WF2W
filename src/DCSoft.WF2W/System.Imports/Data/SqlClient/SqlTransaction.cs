using System.Reflection;

namespace System.Data.SqlClient;

/// <summary>
/// Represents a Transact-SQL transaction to be made in a SQL Server database.
/// </summary>
[Obfuscation(Exclude = true, ApplyToMembers = false)]
public class SqlTransaction : EmulatedDbTransaction
{
    internal SqlTransaction(SqlConnection connection, IsolationLevel isolationLevel) : base(connection, isolationLevel)
    {
    }

    /// <summary>
    /// Gets the SqlConnection object associated with the transaction.
    /// </summary>
    public new SqlConnection Connection => (SqlConnection)DbConnection;
}
