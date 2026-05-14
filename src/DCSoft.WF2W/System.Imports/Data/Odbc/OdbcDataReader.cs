using System.Data.Json;
using System.Reflection;
using System.Text.Json;

namespace System.Data.Odbc;

/// <summary>
/// Provides a way of reading a forward-only stream of rows from an ODBC data source.
/// </summary>
[Obfuscation(Exclude = true, ApplyToMembers = false)]
public class OdbcDataReader : JsonDataReader
{
    internal OdbcDataReader(Utf8JsonReader reader) : base(reader)
    {
    }
}
