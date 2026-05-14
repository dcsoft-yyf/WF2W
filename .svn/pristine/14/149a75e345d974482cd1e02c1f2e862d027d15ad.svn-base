#if UNITTEST

using System.Data.Odbc;
using System.Data.OleDb;
using System.Data.OracleClient;
using System.Data.SqlClient;
using System.Net.Http;

namespace System.Data;

/// <summary>
/// ADO.NET 模拟器客户端测试入口。
/// </summary>
public static class TestAdoNetClient
{
    private const string OleDbConnectionString = "Data Source=YYF2023\\SQLEXPRESS;Initial Catalog=master;Persist Security Info=True;User ID=sa;Password=asdf4321;Pooling=False;MultipleActiveResultSets=False;Encrypt=False;TrustServerCertificate=False;Application Name=\"SQL Server Management Studio\";Command Timeout=0";

    /// <summary>
    /// 运行完整测试并返回内存日志 JSON。
    /// </summary>
    public static async Task<string> RunAllAsync()
    {
        var serverApi = new TestAdoNetServerApi();
        using var httpClient = new HttpClient(new TestAdoNetLoopbackMessageHandler(serverApi))
        {
            BaseAddress = new Uri("http://localhost/")
        };

        AdoEmulatorConfiguration.ConfigureDefaultForwarder(new HttpClientAdoDataForwarder(httpClient));
        InMemorySqlStore.Clear();

        await TestSqlClientAsync();
        await TestOleDbAsync();
        await TestOracleAsync();
        await TestOdbcAsync();
        await TestErrorHandlingAsync();

        return InMemorySqlStore.ExportJson();
    }

    private static Task TestSqlClientAsync()
    {
        using var connection = new SqlConnection("Server=mock;Database=Demo;");
        connection.Open();

        using var transaction = connection.BeginTransaction();
        using var query = new SqlCommand("SELECT * FROM Users WHERE Id=@Id", connection, transaction);
        query.Parameters.Add("@Id", 1);
        using var reader = query.ExecuteReader();
        _ = reader.Read();

        using var nonQuery = new SqlCommand("UPDATE Users SET Name=@Name WHERE Id=@Id", connection, transaction);
        nonQuery.Parameters.Add("@Name", "Blazor");
        nonQuery.Parameters.Add("@Id", 1);
        _ = nonQuery.ExecuteNonQuery();

        transaction.Commit();
        return Task.CompletedTask;
    }

    private static async Task TestOleDbAsync()
    {
        using var connection = new OleDbConnection(OleDbConnectionString);
        await connection.OpenAsync();

        using var transaction = connection.BeginTransaction();

        // 中文注释：先读取数据库真实表结构，后续按结构生成测试语句。
        var tableNames = new List<string>();
        using (var tableQuery = new OleDbCommand("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE'", connection))
        {
            tableQuery.Transaction = transaction;
            await using var tableReader = (OleDbDataReader)await tableQuery.ExecuteReaderAsync();
            while (await tableReader.ReadAsync())
            {
                tableNames.Add(tableReader.GetString(0));
            }
        }

        if (tableNames.Count == 0)
        {
            throw new InvalidOperationException("未发现任何基础表，无法执行结构化测试。");
        }

        // 中文注释：按每张表执行查询测试，验证 DataReader 能正确读取。
        foreach (var tableName in tableNames)
        {
            using var query = new OleDbCommand($"SELECT TOP 1 * FROM [{tableName}]", connection)
            {
                Transaction = transaction,
                CommandType = CommandType.Text
            };
            await using var reader = (OleDbDataReader)await query.ExecuteReaderAsync();
            _ = await reader.ReadAsync();
        }

        // 中文注释：执行参数化查询，验证参数绑定。
        using (var parameterQuery = new OleDbCommand("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME=@TableName", connection))
        {
            parameterQuery.Transaction = transaction;
            parameterQuery.CommandType = CommandType.Text;
            parameterQuery.Parameters.Add("@TableName", tableNames[0]);
            _ = await parameterQuery.ExecuteScalarAsync();
        }

        // 中文注释：调用系统存储过程验证存储过程路径。
        using (var procedure = new OleDbCommand("sp_tables", connection))
        {
            procedure.Transaction = transaction;
            procedure.CommandType = CommandType.StoredProcedure;
            await using var procedureReader = (OleDbDataReader)await procedure.ExecuteReaderAsync();
            _ = await procedureReader.ReadAsync();
        }

        transaction.Commit();
    }

    private static async Task TestOracleAsync()
    {
        using var connection = new OracleConnection("Data Source=mock;");
        await connection.OpenAsync();

        using var command = new OracleCommand("sp_oracle_proc", connection)
        {
            CommandType = CommandType.StoredProcedure
        };
        command.Parameters.Add(":id", 3);
        _ = await command.ExecuteScalarAsync();
    }

    private static Task TestOdbcAsync()
    {
        using var connection = new OdbcConnection("Driver=mock;");
        connection.Open();

        using var command = new OdbcCommand("SELECT * FROM Logs WHERE Level=@Level", connection);
        command.Parameters.Add("@Level", "Info");
        using var reader = command.ExecuteReader();
        _ = reader.Read();
        return Task.CompletedTask;
    }

    private static async Task TestErrorHandlingAsync()
    {
        try
        {
            using var invalidCommand = new OleDbCommand("SELECT 1");
            _ = await invalidCommand.ExecuteScalarAsync();
        }
        catch (InvalidOperationException)
        {
            // 中文注释：验证连接缺失时会抛出明确异常。
        }
    }
}

#endif