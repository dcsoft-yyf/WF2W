using System.Data.Common;
using System.Text.Json;
using System;
using System.Threading.Tasks;
using System.Threading;
namespace System.Data;

internal static class AdoExecutionHelper
{
    internal static AdoServerResult Execute(
        string provider,
        EmulatedDbConnection connection,
        DbTransaction? transaction,
        string commandText,
        CommandType commandType,
        int timeout,
        DbParameterCollection parameters,
        string operation)
    {
        return ExecuteAsync(provider, connection, transaction, commandText, commandType, timeout, parameters, operation, CancellationToken.None)
            .GetAwaiter()
            .GetResult();
    }

    internal static async Task<AdoServerResult> ExecuteAsync(
        string provider,
        EmulatedDbConnection connection,
        DbTransaction? transaction,
        string commandText,
        CommandType commandType,
        int timeout,
        DbParameterCollection parameters,
        string operation,
        CancellationToken cancellationToken)
    {
        var request = CreateRequest(provider, connection.ConnectionString, commandText, commandType, timeout, parameters, operation, transaction);
        InMemorySqlStore.Add(request);

        var payload = JsonSerializer.Serialize(request, AdoJsonOptions.Options);
        var responseText = await connection.ResolveForwarder().SendAsync(payload, cancellationToken).ConfigureAwait(false);
        var result = JsonSerializer.Deserialize<AdoServerResult>(responseText, AdoJsonOptions.Options);

        if (result is null)
        {
            throw new InvalidOperationException("Server response deserialization failed.");
        }

        if (!result.Success)
        {
            throw new InvalidOperationException(result.Message ?? "Server execution failed.");
        }

        return result;
    }

    private static AdoRequestEnvelope CreateRequest(
        string provider,
        string connectionString,
        string commandText,
        CommandType commandType,
        int timeout,
        DbParameterCollection parameters,
        string operation,
        DbTransaction? transaction)
    {
        var request = new AdoRequestEnvelope
        {
            Provider = provider,
            ConnectionString = connectionString,
            CommandText = commandText,
            CommandType = commandType,
            CommandTimeout = timeout,
            Operation = operation,
            TransactionId = transaction is EmulatedDbTransaction emulated ? emulated.TransactionId : null
        };

        foreach (DbParameter parameter in parameters)
        {
            request.Parameters.Add(new AdoParameterInfo
            {
                ParameterName = parameter.ParameterName,
                Value = parameter.Value,
                DbType = parameter.DbType,
                Direction = parameter.Direction,
                IsNullable = parameter.IsNullable
            });
        }

        return request;
    }
}
