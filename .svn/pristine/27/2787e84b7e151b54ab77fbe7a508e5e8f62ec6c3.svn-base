using System.Data.Common;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System;
using System.Threading.Tasks;
using System.Threading;
namespace System.Data.Odbc;

/// <summary>
/// Represents an SQL statement or stored procedure to execute against a data source.
/// </summary>
[Obfuscation(Exclude = true, ApplyToMembers = false)]
public class OdbcCommand : DbCommand
{
    private OdbcConnection? _connection;
    private OdbcTransaction? _transaction;
    private readonly OdbcParameterCollection _parameters = new();

    public OdbcCommand()
    {
    }

    public OdbcCommand(string cmdText)
    {
        CommandText = cmdText;
    }

    public OdbcCommand(string cmdText, OdbcConnection connection)
    {
        CommandText = cmdText;
        Connection = connection;
    }

    public override string CommandText { get; set; } = string.Empty;
    public override int CommandTimeout { get; set; } = 30;
    public override CommandType CommandType { get; set; } = CommandType.Text;
    public override bool DesignTimeVisible { get; set; }
    public override UpdateRowSource UpdatedRowSource { get; set; }

    protected override DbConnection? DbConnection { get => _connection; set => _connection = (OdbcConnection?)value; }
    protected override DbTransaction? DbTransaction { get => _transaction; set => _transaction = (OdbcTransaction?)value; }
    protected override DbParameterCollection DbParameterCollection => _parameters;

    public new OdbcConnection? Connection { get => _connection; set => _connection = value; }
    public new OdbcParameterCollection Parameters => _parameters;
    public new OdbcTransaction? Transaction { get => _transaction; set => _transaction = value; }

    public override void Cancel() { }

    protected override DbParameter CreateDbParameter() => new OdbcParameter();

    public override int ExecuteNonQuery() => Execute("ExecuteNonQuery").AffectedRows;

    public new OdbcDataReader ExecuteReader() => (OdbcDataReader)ExecuteDbDataReader(CommandBehavior.Default);

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
        return AdoExecutionHelper.Execute("Odbc", connection, _transaction, CommandText, CommandType, CommandTimeout, _parameters, operation);
    }

    private Task<AdoServerResult> ExecuteAsync(string operation, CancellationToken cancellationToken)
    {
        var connection = EnsureConnection();
        return AdoExecutionHelper.ExecuteAsync("Odbc", connection, _transaction, CommandText, CommandType, CommandTimeout, _parameters, operation, cancellationToken);
    }

    private OdbcDataReader CreateReaderFromResult(AdoServerResult result)
    {
        if (string.IsNullOrEmpty(result.DataSetJson))
        {
            throw new InvalidOperationException("Server did not return DataSetJson for ExecuteReader.");
        }

        var utf8 = Encoding.UTF8.GetBytes(result.DataSetJson);
        var jsonReader = new Utf8JsonReader(utf8);
        return new OdbcDataReader(jsonReader);
    }

    private OdbcConnection EnsureConnection()
    {
        if (_connection is null)
        {
            throw new InvalidOperationException("OdbcConnection is required.");
        }

        if (_connection.State != ConnectionState.Open)
        {
            _connection.Open();
        }

        return _connection;
    }
}
