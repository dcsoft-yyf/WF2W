#if SqlConnectionServerController

using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.Common;
using System.Text.Json;
using WebApplication1.Models;
using System.Globalization;
using System.Text;

namespace WebApplication1;

/// <summary>
/// 这是一个 ADO.NET 模拟器使用的服务器端 API 的 ASP.NET Core 控制器，用于接收客户端请求并执行数据库操作，然后返回结果。
/// </summary>
[Route("api/ado")]
[ApiController]
public sealed class SqlConnectionServerController : ControllerBase
{
    private const string ServerConnectionString = "Data Source=(local)\\SQLEXPRESS;Initial Catalog=master;Persist Security Info=True;User ID=sa;Password=asdf4321;Pooling=False;MultipleActiveResultSets=False;Encrypt=False;TrustServerCertificate=False;Application Name=\"SQL Server Management Studio\";Command Timeout=0";

    [HttpPost("execute")]
    public ActionResult<AdoServerResult> Execute([FromBody] AdoRequestEnvelope request)
    {
        if (request is null)
        {
            return BadRequest(new AdoServerResult
            {
                Success = false,
                Message = "Request payload is required."
            });
        }

        try
        {
            if(string.IsNullOrEmpty( request.Operation ))
            {
                request.Operation = "ExecuteReader";
            }
            var factory = DbProviderFactories.GetFactory("System.Data.OleDb");
            using var connection = factory.CreateConnection() ?? throw new InvalidOperationException("Cannot create OleDb connection.");
            connection.ConnectionString = ServerConnectionString;
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = request.CommandText;
            command.CommandType = request.CommandType;
            command.CommandTimeout = request.CommandTimeout <= 0 ? 30 : request.CommandTimeout;

            foreach (var parameter in request.Parameters)
            {
                var dbParameter = command.CreateParameter();
                dbParameter.ParameterName = parameter.ParameterName;
                dbParameter.DbType = parameter.DbType;
                dbParameter.Direction = parameter.Direction;
                dbParameter.Value = ConvertValue(parameter.Value);
                command.Parameters.Add(dbParameter);
            }

            if (request.Operation.Contains("ExecuteReader", StringComparison.OrdinalIgnoreCase))
            {
                using var reader = command.ExecuteReader();
                var dataTable = new DataTable("Result");
                dataTable.Load(reader);

                var dataSet = new DataSet("ResultDataSet");
                dataSet.Tables.Add(dataTable);

                var result = new AdoServerResult
                {
                    Success = true,
                    IsQuery = true,
                    AffectedRows = dataTable.Rows.Count,
                    DataSetJson = CreateDataSetJson(dataSet),
                    Message = "ExecuteReader completed."
                };

                return Ok(result);
            }

            if (request.Operation.Contains("ExecuteScalar", StringComparison.OrdinalIgnoreCase))
            {
                var scalar = command.ExecuteScalar();
                return Ok(new AdoServerResult
                {
                    Success = true,
                    IsQuery = false,
                    AffectedRows = 1,
                    Scalar = scalar,
                    Message = "ExecuteScalar completed."
                });
            }

            var affectedRows = command.ExecuteNonQuery();
            return Ok(new AdoServerResult
            {
                Success = true,
                IsQuery = false,
                AffectedRows = affectedRows,
                Message = "ExecuteNonQuery completed."
            });
        }
        catch (Exception ex)
        {
            return Ok(new AdoServerResult
            {
                Success = false,
                IsQuery = false,
                AffectedRows = 0,
                Message = ex.Message
            });
        }
    }

    private static object ConvertValue(object? rawValue)
    {
        if (rawValue is null)
        {
            return DBNull.Value;
        }

        if (rawValue is JsonElement jsonElement)
        {
            return jsonElement.ValueKind switch
            {
                JsonValueKind.Null => DBNull.Value,
                JsonValueKind.String => jsonElement.GetString() ?? string.Empty,
                JsonValueKind.Number when jsonElement.TryGetInt64(out var longValue) => longValue,
                JsonValueKind.Number when jsonElement.TryGetDecimal(out var decimalValue) => decimalValue,
                JsonValueKind.True => true,
                JsonValueKind.False => false,
                _ => jsonElement.ToString()
            };
        }

        return rawValue;
    }

    private static string CreateDataSetJson(DataSet dataSet)
    {
        var writer = new JsonStringWriter();

        writer.WriteStartObject();
        writer.WriteString("dataSetName", dataSet.DataSetName);
        writer.WritePropertyName("tables");
        writer.WriteStartArray();

        foreach (DataTable table in dataSet.Tables)
        {
            writer.WriteStartObject();
            writer.WriteString("tableName", table.TableName);

            writer.WritePropertyName("columns");
            writer.WriteStartArray();
            foreach (DataColumn column in table.Columns)
            {
                writer.WriteStartObject();
                writer.WriteString("columnName", column.ColumnName);
                writer.WriteString("dataType", column.DataType.FullName ?? "System.String");
                writer.WriteEndObject();
            }
            writer.WriteEndArray();

            writer.WritePropertyName("rows");
            writer.WriteStartArray();

            var columnKinds = new int[table.Columns.Count];
            for (var i = 0; i < table.Columns.Count; i++)
            {
                var dataType = table.Columns[i].DataType;
                if (dataType == typeof(string)) columnKinds[i] = 1;
                else if (dataType == typeof(bool)) columnKinds[i] = 2;
                else if (dataType == typeof(int)) columnKinds[i] = 3;
                else if (dataType == typeof(long)) columnKinds[i] = 4;
                else if (dataType == typeof(short)) columnKinds[i] = 5;
                else if (dataType == typeof(decimal)) columnKinds[i] = 6;
                else if (dataType == typeof(double)) columnKinds[i] = 7;
                else if (dataType == typeof(float)) columnKinds[i] = 8;
                else if (dataType == typeof(DateTime)) columnKinds[i] = 9;
                else if (dataType == typeof(Guid)) columnKinds[i] = 10;
                else columnKinds[i] = 0;
            }

            foreach (DataRow row in table.Rows)
            {
                writer.WriteStartArray();
                for (var i = 0; i < table.Columns.Count; i++)
                {
                    var value = row[i];
                    if (value == DBNull.Value || ReferenceEquals(value, null))
                    {
                        writer.WriteNullValue();
                    }
                    else
                    {
                        switch (columnKinds[i])
                        {
                            case 1:
                                writer.WriteStringValue((string)value);
                                break;
                            case 2:
                                writer.WriteBooleanValue((bool)value);
                                break;
                            case 3:
                                writer.WriteNumberValue((int)value);
                                break;
                            case 4:
                                writer.WriteNumberValue((long)value);
                                break;
                            case 5:
                                writer.WriteNumberValue((short)value);
                                break;
                            case 6:
                                writer.WriteNumberValue((decimal)value);
                                break;
                            case 7:
                                writer.WriteNumberValue((double)value);
                                break;
                            case 8:
                                writer.WriteNumberValue((float)value);
                                break;
                            case 9:
                                writer.WriteStringValue((DateTime)value);
                                break;
                            case 10:
                                writer.WriteStringValue((Guid)value);
                                break;
                            default:
                                writer.WriteStringValue(value.ToString());
                                break;
                        }
                    }
                }
                writer.WriteEndArray();
            }
            writer.WriteEndArray();

            writer.WriteEndObject();
        }
        writer.WriteEndArray();
        writer.WriteEndObject();

        return writer.ToString();
    }
}

[System.Reflection.Obfuscation( Exclude = true , ApplyToMembers = true )]
public sealed class AdoRequestEnvelope
{
    public string Provider { get; set; } = string.Empty;
    public string ConnectionString { get; set; } = string.Empty;
    public string CommandText { get; set; } = string.Empty;
    public CommandType CommandType { get; set; }
    public int CommandTimeout { get; set; }
    public string Operation { get; set; } = string.Empty;
    public string? TransactionId { get; set; }
    public List<AdoParameterInfo> Parameters { get; set; } = new();
}

[System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
public sealed class AdoServerResult
{
    public bool Success { get; set; }
    public bool IsQuery { get; set; }
    public int AffectedRows { get; set; }
    public string? DataSetJson { get; set; }
    public string? Message { get; set; }
    public object? Scalar { get; set; }
}

internal sealed class JsonStringWriter
{
    private readonly StringBuilder _builder = new(1024);
    private readonly Stack<Context> _stack = new();

    public void WriteStartObject()
    {
        WriteValuePrefix();
        _builder.Append('{');
        _stack.Push(new Context(isObject: true));
    }

    public void WriteEndObject()
    {
        _builder.Append('}');
        _stack.Pop();
    }

    public void WriteStartArray()
    {
        WriteValuePrefix();
        _builder.Append('[');
        _stack.Push(new Context(isObject: false));
    }

    public void WriteEndArray()
    {
        _builder.Append(']');
        _stack.Pop();
    }

    public void WritePropertyName(string name)
    {
        if (_stack.Count == 0 || !_stack.Peek().IsObject)
        {
            throw new InvalidOperationException("PropertyName must be written inside object.");
        }

        var context = _stack.Pop();
        if (!context.First)
        {
            _builder.Append(',');
        }
        context.First = false;
        context.AfterPropertyName = true;
        _stack.Push(context);

        WriteEscapedString(name);
        _builder.Append(':');
    }

    public void WriteString(string propertyName, string? value)
    {
        WritePropertyName(propertyName);
        WriteStringValue(value);
    }

    public void WriteStringValue(string? value)
    {
        if (value is null)
        {
            WriteNullValue();
            return;
        }

        WriteValuePrefix();
        WriteEscapedString(value);
    }

    public void WriteStringValue(DateTime value) => WriteStringValue(value.ToString("O", CultureInfo.InvariantCulture));

    public void WriteStringValue(Guid value) => WriteStringValue(value.ToString());

    public void WriteBooleanValue(bool value)
    {
        WriteValuePrefix();
        _builder.Append(value ? "true" : "false");
    }

    public void WriteNullValue()
    {
        WriteValuePrefix();
        _builder.Append("null");
    }

    public void WriteNumberValue(int value) => WriteRawNumber(value.ToString(CultureInfo.InvariantCulture));
    public void WriteNumberValue(long value) => WriteRawNumber(value.ToString(CultureInfo.InvariantCulture));
    public void WriteNumberValue(short value) => WriteRawNumber(value.ToString(CultureInfo.InvariantCulture));
    public void WriteNumberValue(decimal value) => WriteRawNumber(value.ToString(CultureInfo.InvariantCulture));
    public void WriteNumberValue(double value) => WriteRawNumber(value.ToString(CultureInfo.InvariantCulture));
    public void WriteNumberValue(float value) => WriteRawNumber(value.ToString(CultureInfo.InvariantCulture));

    public override string ToString() => _builder.ToString();

    private void WriteRawNumber(string text)
    {
        WriteValuePrefix();
        _builder.Append(text);
    }

    private void WriteValuePrefix()
    {
        if (_stack.Count == 0)
        {
            return;
        }

        var context = _stack.Pop();
        if (context.IsObject)
        {
            if (!context.AfterPropertyName)
            {
                throw new InvalidOperationException("Value in object must follow property name.");
            }

            context.AfterPropertyName = false;
        }
        else
        {
            if (!context.First)
            {
                _builder.Append(',');
            }
            else
            {
                context.First = false;
            }
        }

        _stack.Push(context);
    }

    private void WriteEscapedString(string value)
    {
        _builder.Append('"');
        foreach (var ch in value)
        {
            switch (ch)
            {
                case '"': _builder.Append("\\\""); break;
                case '\\': _builder.Append("\\\\"); break;
                case '\b': _builder.Append("\\b"); break;
                case '\f': _builder.Append("\\f"); break;
                case '\n': _builder.Append("\\n"); break;
                case '\r': _builder.Append("\\r"); break;
                case '\t': _builder.Append("\\t"); break;
                default:
                    if (char.IsControl(ch))
                    {
                        _builder.Append("\\u");
                        _builder.Append(((int)ch).ToString("x4", CultureInfo.InvariantCulture));
                    }
                    else
                    {
                        _builder.Append(ch);
                    }
                    break;
            }
        }
        _builder.Append('"');
    }

    private sealed class Context
    {
        public Context(bool isObject)
        {
            IsObject = isObject;
            First = true;
        }

        public bool IsObject { get; }
        public bool First { get; set; }
        public bool AfterPropertyName { get; set; }
    }
}

#endif