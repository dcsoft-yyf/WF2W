#if UNITTEST
using System.Text.Json;

namespace System.Data;

/// <summary>
/// 模拟服务器端 API 处理器。
/// </summary>
public sealed class TestAdoNetServerApi
{
    private const string ExpectedOleDbConnectionString = "Data Source=YYF2023\\SQLEXPRESS;Initial Catalog=master;Persist Security Info=True;User ID=sa;Password=asdf4321;Pooling=False;MultipleActiveResultSets=False;Encrypt=False;TrustServerCertificate=False;Application Name=\"SQL Server Management Studio\";Command Timeout=0";

    /// <summary>
    /// 接收请求 JSON 并返回执行结果 JSON。
    /// </summary>
    public Task<string> ExecuteAsync(string payload, CancellationToken cancellationToken = default)
    {
        var request = JsonSerializer.Deserialize<AdoRequestEnvelope>(payload, AdoJsonOptions.Options)
            ?? throw new InvalidOperationException("Request payload is invalid.");

        if (string.Equals(request.Provider, "OleDb", StringComparison.OrdinalIgnoreCase)
            && !string.Equals(request.ConnectionString, ExpectedOleDbConnectionString, StringComparison.Ordinal))
        {
            var invalidConnectionResult = new AdoServerResult
            {
                Success = false,
                IsQuery = false,
                AffectedRows = 0,
                Message = "OleDb connection string mismatch."
            };
            return Task.FromResult(JsonSerializer.Serialize(invalidConnectionResult, AdoJsonOptions.Options));
        }

        var isQuery = request.CommandType == CommandType.StoredProcedure
                      || request.Operation.Contains("Reader", StringComparison.OrdinalIgnoreCase)
                      || request.CommandText.TrimStart().StartsWith("SELECT", StringComparison.OrdinalIgnoreCase);

        AdoServerResult result;
        if (isQuery)
        {
            var dataSet = new DataSet("ResultDataSet");
            var table = dataSet.Tables.Add("Result");
            table.Columns.Add("Provider", typeof(string));
            table.Columns.Add("CommandText", typeof(string));
            table.Columns.Add("ParameterCount", typeof(int));
            table.Columns.Add("ConnectionAccepted", typeof(bool));
            table.Rows.Add(request.Provider, request.CommandText, request.Parameters.Count, true);

            using var xmlWriter = new StringWriter();
            dataSet.WriteXml(xmlWriter, XmlWriteMode.WriteSchema);

            result = new AdoServerResult
            {
                Success = true,
                IsQuery = true,
                AffectedRows = table.Rows.Count,
                DataSetJson = CreateDataSetJson(dataSet, xmlWriter.ToString()),
                Message = "Query execution success."
            };
        }
        else
        {
            result = new AdoServerResult
            {
                Success = true,
                IsQuery = false,
                AffectedRows = 1,
                Scalar = 1,
                Message = "NonQuery execution success."
            };
        }

        return Task.FromResult(JsonSerializer.Serialize(result, AdoJsonOptions.Options));
    }

    private static string CreateDataSetJson(DataSet dataSet, string originalXml)
    {
        var tables = new List<object>(dataSet.Tables.Count);
        foreach (DataTable table in dataSet.Tables)
        {
            var columns = new List<object>(table.Columns.Count);
            foreach (DataColumn column in table.Columns)
            {
                columns.Add(new
                {
                    columnName = column.ColumnName,
                    dataType = column.DataType.FullName ?? "System.String"
                });
            }

            var rows = new List<object[]>(table.Rows.Count);
            foreach (DataRow row in table.Rows)
            {
                var values = new object[table.Columns.Count];
                for (var i = 0; i < table.Columns.Count; i++)
                {
                    values[i] = row[i] == DBNull.Value ? null! : row[i];
                }

                rows.Add(values);
            }

            tables.Add(new
            {
                tableName = table.TableName,
                columns,
                rows
            });
        }

        var payload = new
        {
            dataSetName = dataSet.DataSetName,
            tables,
            originalDataSetXml = originalXml
        };

        return JsonSerializer.Serialize(payload, AdoJsonOptions.Options);
    }
}
#endif