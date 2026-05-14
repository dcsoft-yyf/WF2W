using System.Data.Common;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System;
using System.Threading.Tasks;
using System.Threading;
namespace System.Data.SqlClient;

/// <summary>
/// Represents a Transact-SQL statement or stored procedure to execute against a SQL Server database.
/// </summary>
[Obfuscation(Exclude = true, ApplyToMembers = false)]
public class SqlCommand : DbCommand
{
    private SqlConnection? _connection;
    private SqlTransaction? _transaction;
    private readonly SqlParameterCollection _parameters = new();

    /// <summary>
    /// Initializes a new instance of the SqlCommand class.
    /// </summary>
    public SqlCommand()
    {
    }

    /// <summary>
    /// Initializes a new instance of the SqlCommand class with the text of the query.
    /// </summary>
    public SqlCommand(string cmdText)
    {
        CommandText = cmdText;
    }

    /// <summary>
    /// Initializes a new instance of the SqlCommand class with the text of the query and a SqlConnection.
    /// </summary>
    public SqlCommand(string cmdText, SqlConnection connection)
    {
        CommandText = cmdText;
        Connection = connection;
    }

    /// <summary>
    /// Initializes a new instance of the SqlCommand class with the text of the query, a SqlConnection, and a SqlTransaction.
    /// </summary>
    public SqlCommand(string cmdText, SqlConnection connection, SqlTransaction transaction)
    {
        CommandText = cmdText;
        Connection = connection;
        Transaction = transaction;
    }

    public override string CommandText { get; set; } = string.Empty;

    public override int CommandTimeout { get; set; } = 30;

    public override CommandType CommandType { get; set; } = CommandType.Text;

    public override bool DesignTimeVisible { get; set; }

    public override UpdateRowSource UpdatedRowSource { get; set; }

    protected override DbConnection? DbConnection
    {
        get => _connection;
        set => _connection = (SqlConnection?)value;
    }

    protected override DbTransaction? DbTransaction
    {
        get => _transaction;
        set => _transaction = (SqlTransaction?)value;
    }

    protected override DbParameterCollection DbParameterCollection => _parameters;

    /// <summary>
    /// Gets or sets the SqlConnection used by this instance of the SqlCommand.
    /// </summary>
    public new SqlConnection? Connection
    {
        get => _connection;
        set => _connection = value;
    }

    /// <summary>
    /// Gets the parameters collection.
    /// </summary>
    public new SqlParameterCollection Parameters => _parameters;

    /// <summary>
    /// Gets or sets the SqlTransaction within which the SqlCommand executes.
    /// </summary>
    public new SqlTransaction? Transaction
    {
        get => _transaction;
        set => _transaction = value;
    }

    /// <summary>
    /// Attempts to cancel the execution of a SqlCommand.
    /// </summary>
    public override void Cancel()
    {
        // 中文注释：模拟器不维护远程执行句柄，取消为空实现�?
    }

    /// <summary>
    /// Creates a new instance of a SqlParameter object.
    /// </summary>
    protected override DbParameter CreateDbParameter() => new SqlParameter();

    /// <summary>
    /// Executes a Transact-SQL statement against the connection and returns the number of rows affected.
    /// </summary>
    public override int ExecuteNonQuery()
    {
        return Execute("ExecuteNonQuery").AffectedRows;
    }

    /// <summary>
    /// Sends the CommandText to the Connection and builds a SqlDataReader.
    /// </summary>
    public new SqlDataReader ExecuteReader()
    {
        return (SqlDataReader)ExecuteDbDataReader(CommandBehavior.Default);
    }

    /// <summary>
    /// Executes the query, and returns the first column of the first row in the result set.
    /// </summary>
    public override object? ExecuteScalar()
    {
        var result = Execute("ExecuteScalar");
        if (result.Scalar is not null)
        {
            return result.Scalar;
        }

        using var reader = CreateReaderFromResult(result);
        return reader.Read() && reader.FieldCount > 0 ? reader.GetValue(0) : null;
    }

    public override Task<int> ExecuteNonQueryAsync(CancellationToken cancellationToken)
    {
        return ExecuteAsync("ExecuteNonQuery", cancellationToken).ContinueWith(task => task.Result.AffectedRows, cancellationToken);
    }

    public override async Task<object?> ExecuteScalarAsync(CancellationToken cancellationToken)
    {
        var result = await ExecuteAsync("ExecuteScalar", cancellationToken).ConfigureAwait(false);
        if (result.Scalar is not null)
        {
            return result.Scalar;
        }

        using var reader = CreateReaderFromResult(result);
        return reader.Read() && reader.FieldCount > 0 ? reader.GetValue(0) : null;
    }

    /// <summary>
    /// Creates a prepared (or compiled) version of the command on the data source.
    /// </summary>
    public override void Prepare()
    {
        // 中文注释：模拟实现无需预编译�?
    }

    protected override DbDataReader ExecuteDbDataReader(CommandBehavior behavior)
    {
        var result = Execute("ExecuteReader");
        return CreateReaderFromResult(result);
    }

    protected override async Task<DbDataReader> ExecuteDbDataReaderAsync(CommandBehavior behavior, CancellationToken cancellationToken)
    {
        var result = await ExecuteAsync("ExecuteReader", cancellationToken).ConfigureAwait(false);
        return CreateReaderFromResult(result);
    }

    private AdoServerResult Execute(string operation)
    {
        var connection = EnsureConnection();
        return AdoExecutionHelper.Execute("SqlClient", connection, _transaction, CommandText, CommandType, CommandTimeout, _parameters, operation);
    }

    private Task<AdoServerResult> ExecuteAsync(string operation, CancellationToken cancellationToken)
    {
        var connection = EnsureConnection();
        return AdoExecutionHelper.ExecuteAsync("SqlClient", connection, _transaction, CommandText, CommandType, CommandTimeout, _parameters, operation, cancellationToken);
    }

    private SqlDataReader CreateReaderFromResult(AdoServerResult result)
    {
        if (string.IsNullOrEmpty(result.DataSetJson))
        {
            throw new InvalidOperationException("Server did not return DataSetJson for ExecuteReader.");
        }

        var utf8 = Encoding.UTF8.GetBytes(result.DataSetJson);
        var jsonReader = new Utf8JsonReader(utf8);
        return new SqlDataReader(jsonReader);
    }

    private SqlConnection EnsureConnection()
    {
        if (_connection is null)
        {
            throw new InvalidOperationException("SqlConnection is required.");
        }

        if (_connection.State != ConnectionState.Open)
        {
            _connection.Open();
        }

        return _connection;
    }
}
