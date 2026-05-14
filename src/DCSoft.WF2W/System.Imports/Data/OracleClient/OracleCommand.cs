using System.Data.Common;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System;
using System.Threading.Tasks;
using System.Threading;
namespace System.Data.OracleClient;

/// <summary>
/// Represents an SQL statement or stored procedure to execute against an Oracle database.
/// </summary>
[Obfuscation(Exclude = true, ApplyToMembers = false)]
public class OracleCommand : DbCommand
{
    private OracleConnection? _connection;
    private OracleTransaction? _transaction;
    private readonly OracleParameterCollection _parameters = new();

    public OracleCommand()
    {
    }

    public OracleCommand(string cmdText)
    {
        CommandText = cmdText;
    }

    public OracleCommand(string cmdText, OracleConnection connection)
    {
        CommandText = cmdText;
        Connection = connection;
    }

    public override string CommandText { get; set; } = string.Empty;
    public override int CommandTimeout { get; set; } = 30;
    public override CommandType CommandType { get; set; } = CommandType.Text;
    public override bool DesignTimeVisible { get; set; }
    public override UpdateRowSource UpdatedRowSource { get; set; }

    protected override DbConnection? DbConnection { get => _connection; set => _connection = (OracleConnection?)value; }
    protected override DbTransaction? DbTransaction { get => _transaction; set => _transaction = (OracleTransaction?)value; }
    protected override DbParameterCollection DbParameterCollection => _parameters;

    public new OracleConnection? Connection { get => _connection; set => _connection = value; }
    public new OracleParameterCollection Parameters => _parameters;
    public new OracleTransaction? Transaction { get => _transaction; set => _transaction = value; }

    public override void Cancel() { }

    protected override DbParameter CreateDbParameter() => new OracleParameter();

    public override int ExecuteNonQuery() => Execute("ExecuteNonQuery").AffectedRows;

    public new OracleDataReader ExecuteReader() => (OracleDataReader)ExecuteDbDataReader(CommandBehavior.Default);

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

    public override void Prepare() { }

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

    public override Task<int> ExecuteNonQueryAsync(CancellationToken cancellationToken)
        => ExecuteAsync("ExecuteNonQuery", cancellationToken).ContinueWith(task => task.Result.AffectedRows, cancellationToken);

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

    private AdoServerResult Execute(string operation)
    {
        var connection = EnsureConnection();
        return AdoExecutionHelper.Execute("OracleClient", connection, _transaction, CommandText, CommandType, CommandTimeout, _parameters, operation);
    }

    private Task<AdoServerResult> ExecuteAsync(string operation, CancellationToken cancellationToken)
    {
        var connection = EnsureConnection();
        return AdoExecutionHelper.ExecuteAsync("OracleClient", connection, _transaction, CommandText, CommandType, CommandTimeout, _parameters, operation, cancellationToken);
    }

    private OracleDataReader CreateReaderFromResult(AdoServerResult result)
    {
        if (string.IsNullOrEmpty(result.DataSetJson))
        {
            throw new InvalidOperationException("Server did not return DataSetJson for ExecuteReader.");
        }

        var utf8 = Encoding.UTF8.GetBytes(result.DataSetJson);
        var jsonReader = new Utf8JsonReader(utf8);
        return new OracleDataReader(jsonReader);
    }

    private OracleConnection EnsureConnection()
    {
        if (_connection is null)
        {
            throw new InvalidOperationException("OracleConnection is required.");
        }

        if (_connection.State != ConnectionState.Open)
        {
            _connection.Open();
        }

        return _connection;
    }
}
