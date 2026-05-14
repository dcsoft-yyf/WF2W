using System.Buffers;
using System.Collections;
using System.Data.Common;
using System.Text;
using System.Text.Json;

namespace System.Data.Json
{
    public class JsonDataReader : DbDataReader
    {
        private readonly List<string> _tableNames = new();
        private readonly List<List<(string Name, Type DataType)>> _columnsByTable = new();
        private readonly List<List<object?[]>> _rowsByTable = new();

        private int _tableIndex;
        private int _rowIndex = -1;
        private bool _isClosed;

        /// <summary>
        /// 创建一个新的 JsonDataReader 实例。
        /// </summary>
        /// <param name="jsonReader">用于读取 JSON 数据的 Utf8JsonReader 实例。</param>
        public JsonDataReader(Utf8JsonReader jsonReader)
        {
            ParseAndCacheAll(jsonReader);
        }

        /// <summary>
        /// 创建一个新的 JsonDataReader 实例。
        /// </summary>
        /// <param name="jsonUtf8">完整 JSON 的 UTF8 字节。</param>
        public JsonDataReader(ReadOnlyMemory<byte> jsonUtf8)
        {
            if (jsonUtf8.IsEmpty)
            {
                throw new ArgumentException("JSON 数据不能为空。", nameof(jsonUtf8));
            }

            var reader = new Utf8JsonReader(jsonUtf8.Span);
            ParseAndCacheAll(reader);
        }

        public override object this[int ordinal] => GetValue(ordinal);

        public override object this[string name] => GetValue(GetOrdinal(name));

        public override int Depth => 0;

        public override int FieldCount => CurrentColumns.Count;

        public override bool HasRows => CurrentRows.Count > 0;

        public override bool IsClosed => _isClosed;

        public override int RecordsAffected => -1;

        public override void Close() => _isClosed = true;

        public IDataReader GetData(int ordinal)
        {
            throw new NotSupportedException("JsonDataReader 不支持嵌套 IDataReader。");
        }

        public override string GetName(int ordinal) => CurrentColumns[ordinal].Name;

        public override string GetDataTypeName(int ordinal) => CurrentColumns[ordinal].DataType.FullName ?? CurrentColumns[ordinal].DataType.Name;

        public override Type GetFieldType(int ordinal) => CurrentColumns[ordinal].DataType;

        public override object GetValue(int ordinal)
        {
            EnsureOpen();
            EnsureRowReady();
            return CurrentRows[_rowIndex][ordinal] ?? DBNull.Value;
        }

        public override int GetValues(object[] values)
        {
            var count = Math.Min(values.Length, FieldCount);
            for (var i = 0; i < count; i++)
            {
                values[i] = GetValue(i);
            }

            return count;
        }

        public override int GetOrdinal(string name)
        {
            for (var i = 0; i < CurrentColumns.Count; i++)
            {
                if (string.Equals(CurrentColumns[i].Name, name, StringComparison.OrdinalIgnoreCase))
                {
                    return i;
                }
            }

            throw new IndexOutOfRangeException($"列 {name} 不存在。");
        }

        public override bool GetBoolean(int ordinal) => Convert.ToBoolean(GetValue(ordinal));

        public override byte GetByte(int ordinal) => Convert.ToByte(GetValue(ordinal));

        public override long GetBytes(int ordinal, long dataOffset, byte[]? buffer, int bufferOffset, int length)
        {
            var source = GetValue(ordinal) switch
            {
                byte[] bytes => bytes,
                string text => Encoding.UTF8.GetBytes(text),
                _ => throw new InvalidCastException("字段无法转换为字节数组。")
            };

            var available = Math.Max(0, source.Length - (int)dataOffset);
            var copyLength = Math.Min(available, length);
            if (buffer is not null && copyLength > 0)
            {
                Array.Copy(source, (int)dataOffset, buffer, bufferOffset, copyLength);
            }

            return copyLength;
        }

        public override char GetChar(int ordinal) => Convert.ToChar(GetValue(ordinal));

        public override long GetChars(int ordinal, long dataOffset, char[]? buffer, int bufferOffset, int length)
        {
            var source = Convert.ToString(GetValue(ordinal)) ?? string.Empty;
            var available = Math.Max(0, source.Length - (int)dataOffset);
            var copyLength = Math.Min(available, length);
            if (buffer is not null && copyLength > 0)
            {
                source.CopyTo((int)dataOffset, buffer, bufferOffset, copyLength);
            }

            return copyLength;
        }

        public override Guid GetGuid(int ordinal)
        {
            var value = GetValue(ordinal);
            return value is Guid guid ? guid : Guid.Parse(Convert.ToString(value) ?? string.Empty);
        }

        public override short GetInt16(int ordinal) => Convert.ToInt16(GetValue(ordinal));

        public override int GetInt32(int ordinal) => Convert.ToInt32(GetValue(ordinal));

        public override long GetInt64(int ordinal) => Convert.ToInt64(GetValue(ordinal));

        public override float GetFloat(int ordinal) => Convert.ToSingle(GetValue(ordinal));

        public override double GetDouble(int ordinal) => Convert.ToDouble(GetValue(ordinal));

        public override string GetString(int ordinal) => Convert.ToString(GetValue(ordinal)) ?? string.Empty;

        public override decimal GetDecimal(int ordinal) => Convert.ToDecimal(GetValue(ordinal));

        public override DateTime GetDateTime(int ordinal) => Convert.ToDateTime(GetValue(ordinal));

        public override bool IsDBNull(int ordinal) => GetValue(ordinal) is DBNull;

        public override DataTable GetSchemaTable()
        {
            var table = new DataTable("SchemaTable");
            table.Columns.Add("ColumnName", typeof(string));
            table.Columns.Add("ColumnOrdinal", typeof(int));
            table.Columns.Add("DataType", typeof(Type));

            for (var i = 0; i < CurrentColumns.Count; i++)
            {
                var row = table.NewRow();
                row["ColumnName"] = CurrentColumns[i].Name;
                row["ColumnOrdinal"] = i;
                row["DataType"] = CurrentColumns[i].DataType;
                table.Rows.Add(row);
            }

            return table;
        }

        public override bool NextResult()
        {
            EnsureOpen();
            if (_tableIndex + 1 >= _tableNames.Count)
            {
                return false;
            }

            _tableIndex++;
            _rowIndex = -1;
            return true;
        }

        public override bool Read()
        {
            EnsureOpen();
            if (_rowIndex + 1 >= CurrentRows.Count)
            {
                return false;
            }

            _rowIndex++;
            return true;
        }

        public override IEnumerator GetEnumerator()
        {
            while (Read())
            {
                yield return this;
            }
        }

        private List<(string Name, Type DataType)> CurrentColumns => _columnsByTable[_tableIndex];

        private List<object?[]> CurrentRows => _rowsByTable[_tableIndex];

        private void ParseAndCacheAll(Utf8JsonReader reader)
        {
            if (!reader.Read() || reader.TokenType != JsonTokenType.StartObject)
            {
                throw new InvalidOperationException("JSON 根节点必须是对象。");
            }

            var hasTables = false;
            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    break;
                }

                if (reader.TokenType != JsonTokenType.PropertyName)
                {
                    continue;
                }

                var propertyName = reader.GetString();
                if (!reader.Read())
                {
                    throw new InvalidOperationException("JSON 读取提前结束。");
                }

                if (string.Equals(propertyName, "tables", StringComparison.OrdinalIgnoreCase))
                {
                    hasTables = true;
                    ParseTables(ref reader);
                }
                else
                {
                    SkipValue(ref reader);
                }
            }

            if (!hasTables)
            {
                throw new InvalidOperationException("JSON 中缺少 tables 数组。");
            }

            if (_tableNames.Count == 0)
            {
                throw new InvalidOperationException("JSON 中没有可读取的表数据。");
            }
        }

        private void ParseTables(ref Utf8JsonReader reader)
        {
            if (reader.TokenType != JsonTokenType.StartArray)
            {
                throw new InvalidOperationException("tables 必须是数组。");
            }

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndArray)
                {
                    break;
                }

                ParseTable(ref reader);
            }
        }

        private void ParseTable(ref Utf8JsonReader reader)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new InvalidOperationException("table 项必须是对象。");
            }

            var tableName = "Table";
            List<(string Name, Type DataType)>? columns = null;
            List<object?[]>? rows = null;

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    break;
                }

                if (reader.TokenType != JsonTokenType.PropertyName)
                {
                    continue;
                }

                var propertyName = reader.GetString();
                if (!reader.Read())
                {
                    throw new InvalidOperationException("JSON 读取提前结束。");
                }

                if (string.Equals(propertyName, "tableName", StringComparison.OrdinalIgnoreCase))
                {
                    tableName = reader.TokenType == JsonTokenType.String ? reader.GetString() ?? "Table" : "Table";
                    continue;
                }

                if (string.Equals(propertyName, "columns", StringComparison.OrdinalIgnoreCase))
                {
                    columns = ParseColumns(ref reader);
                    continue;
                }

                if (string.Equals(propertyName, "rows", StringComparison.OrdinalIgnoreCase))
                {
                    rows = ParseRows(ref reader, columns ?? throw new InvalidOperationException($"表 {tableName} 必须先定义 columns。"));
                    continue;
                }

                SkipValue(ref reader);
            }

            if (columns is null)
            {
                throw new InvalidOperationException($"表 {tableName} 缺少 columns 定义。");
            }

            if (rows is null)
            {
                throw new InvalidOperationException($"表 {tableName} 缺少 rows 定义。");
            }

            _tableNames.Add(tableName);
            _columnsByTable.Add(columns);
            _rowsByTable.Add(rows);
        }

        private static List<(string Name, Type DataType)> ParseColumns(ref Utf8JsonReader reader)
        {
            if (reader.TokenType != JsonTokenType.StartArray)
            {
                throw new InvalidOperationException("columns 必须是数组。");
            }

            var columns = new List<(string Name, Type DataType)>();
            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndArray)
                {
                    break;
                }

                if (reader.TokenType != JsonTokenType.StartObject)
                {
                    throw new InvalidOperationException("column 项必须是对象。");
                }

                var columnName = string.Empty;
                var dataTypeName = "System.String";

                while (reader.Read())
                {
                    if (reader.TokenType == JsonTokenType.EndObject)
                    {
                        break;
                    }

                    if (reader.TokenType != JsonTokenType.PropertyName)
                    {
                        continue;
                    }

                    var propertyName = reader.GetString();
                    if (!reader.Read())
                    {
                        throw new InvalidOperationException("JSON 读取提前结束。");
                    }

                    if (string.Equals(propertyName, "columnName", StringComparison.OrdinalIgnoreCase))
                    {
                        columnName = reader.TokenType == JsonTokenType.String ? reader.GetString() ?? string.Empty : string.Empty;
                    }
                    else if (string.Equals(propertyName, "dataType", StringComparison.OrdinalIgnoreCase))
                    {
                        dataTypeName = reader.TokenType == JsonTokenType.String ? reader.GetString() ?? "System.String" : "System.String";
                    }
                    else
                    {
                        SkipValue(ref reader);
                    }
                }

                var dataType = Type.GetType(dataTypeName, throwOnError: false) ?? typeof(string);
                columns.Add((columnName, dataType));
            }

            return columns;
        }

        private static List<object?[]> ParseRows(ref Utf8JsonReader reader, List<(string Name, Type DataType)> columns)
        {
            if (reader.TokenType != JsonTokenType.StartArray)
            {
                throw new InvalidOperationException("rows 必须是数组。");
            }

            var rows = new List<object?[]>();
            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndArray)
                {
                    break;
                }

                if (reader.TokenType != JsonTokenType.StartArray)
                {
                    throw new InvalidOperationException("rows 的每一项必须是数组。");
                }

                var values = new object?[columns.Count];
                var valueIndex = 0;
                while (reader.Read())
                {
                    if (reader.TokenType == JsonTokenType.EndArray)
                    {
                        break;
                    }

                    if (valueIndex < columns.Count)
                    {
                        values[valueIndex] = ReadCurrentValue(ref reader, columns[valueIndex].DataType);
                    }
                    else
                    {
                        SkipValue(ref reader);
                    }

                    valueIndex++;
                }

                rows.Add(values);
            }

            return rows;
        }

        private static object? ReadCurrentValue(ref Utf8JsonReader reader, Type targetType)
        {
            if (reader.TokenType == JsonTokenType.Null)
            {
                return null;
            }

            if (targetType == typeof(string)) return reader.TokenType == JsonTokenType.String ? reader.GetString() : GetRawValueText(reader);
            if (targetType == typeof(int)) return reader.GetInt32();
            if (targetType == typeof(long)) return reader.GetInt64();
            if (targetType == typeof(short)) return reader.GetInt16();
            if (targetType == typeof(bool)) return reader.GetBoolean();
            if (targetType == typeof(decimal)) return reader.GetDecimal();
            if (targetType == typeof(double)) return reader.GetDouble();
            if (targetType == typeof(float)) return reader.GetSingle();
            if (targetType == typeof(DateTime)) return reader.GetDateTime();
            if (targetType == typeof(Guid)) return reader.GetGuid();

            return GetRawValueText(reader);
        }

        private static void SkipValue(ref Utf8JsonReader reader)
        {
            if (reader.TokenType is JsonTokenType.StartObject or JsonTokenType.StartArray)
            {
                reader.Skip();
            }
        }

        private static string GetRawValueText(Utf8JsonReader reader) => Encoding.UTF8.GetString(reader.ValueSpan);

        private static ReadOnlyMemory<byte> CloneJsonBytes(ref Utf8JsonReader reader)
        {
            if (reader.TokenType == JsonTokenType.None && !reader.Read())
            {
                throw new InvalidOperationException("无法读取 JSON 数据。");
            }

            var buffer = new ArrayBufferWriter<byte>();
            using var writer = new Utf8JsonWriter(buffer);
            CopyToken(ref reader, writer);
            writer.Flush();
            return buffer.WrittenMemory.ToArray();
        }

        private static void CopyToken(ref Utf8JsonReader reader, Utf8JsonWriter writer)
        {
            switch (reader.TokenType)
            {
                case JsonTokenType.StartObject:
                    writer.WriteStartObject();
                    while (reader.Read())
                    {
                        if (reader.TokenType == JsonTokenType.EndObject)
                        {
                            writer.WriteEndObject();
                            return;
                        }

                        if (reader.TokenType != JsonTokenType.PropertyName)
                        {
                            continue;
                        }

                        writer.WritePropertyName(reader.GetString());
                        reader.Read();
                        CopyToken(ref reader, writer);
                    }
                    return;

                case JsonTokenType.StartArray:
                    writer.WriteStartArray();
                    while (reader.Read())
                    {
                        if (reader.TokenType == JsonTokenType.EndArray)
                        {
                            writer.WriteEndArray();
                            return;
                        }

                        CopyToken(ref reader, writer);
                    }
                    return;

                case JsonTokenType.String:
                    writer.WriteStringValue(reader.GetString());
                    return;
                case JsonTokenType.Number:
                    writer.WriteRawValue(GetRawValueText(reader), skipInputValidation: true);
                    return;
                case JsonTokenType.True:
                    writer.WriteBooleanValue(true);
                    return;
                case JsonTokenType.False:
                    writer.WriteBooleanValue(false);
                    return;
                case JsonTokenType.Null:
                    writer.WriteNullValue();
                    return;
                default:
                    throw new InvalidOperationException($"不支持的 JsonTokenType: {reader.TokenType}");
            }
        }

        private void EnsureOpen()
        {
            if (_isClosed)
            {
                throw new InvalidOperationException("DataReader 已关闭。");
            }
        }

        private void EnsureRowReady()
        {
            if (_rowIndex < 0 || _rowIndex >= CurrentRows.Count)
            {
                throw new InvalidOperationException("当前没有可读取的行，请先调用 Read()。");
            }
        }
    }
}
