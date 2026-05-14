using System.Data.Common;
using System.Data.Json;
using System.Reflection;
using System.Text.Json;

namespace System.Data.OleDb;

/// <summary>
/// Provides a way of reading a forward-only stream of data records.
/// </summary>
[Obfuscation(Exclude = true, ApplyToMembers = false)]
public class OleDbDataReader : JsonDataReader
{
    internal OleDbDataReader(Utf8JsonReader reader) : base(reader)
    {
    }
}
