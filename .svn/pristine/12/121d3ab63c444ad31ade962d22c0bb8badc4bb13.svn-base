using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text.Json;
using System.Linq;

namespace DCSoft.WF2W.Core.WinFormCore
{
    internal static class DCFontList
    {
        internal sealed class FontJsonInfo
        {
            public string Name = null;
            public string[] Names = null;
            public bool Bold = false;
            public bool Italic = false;
            public byte[] Snapshort = null;
            public string FileName = null;
            public double BoldZoomRate = 1;
            public int LineSpacing = 0;
            public int CellAscent = 0;
            public int CellDescent = 0;
            public int EmHeight = 0;
            public TrueTypeFontSnapshort TTF_Snapshort = null;
        }

        // 运行时缓存表：key -> FontInfo
        private static Dictionary<string, FontJsonInfo>? s_fontTable;

        // 原始数据（由 GenDCWriter5FontFiles 生成）
        private static readonly List<FontJsonInfo> _Values = new();

        public static void Start()
        {
            var assembly = typeof(DCFontList).Assembly;
            string resourceName = null;
            foreach (var name in assembly.GetManifestResourceNames())
            {
                if (name.EndsWith(".DCFontList.json", StringComparison.OrdinalIgnoreCase))
                {
                    resourceName = name;
                    break;
                }
            }
            if (resourceName == null || resourceName.Length == 0)
            {
                throw new InvalidOperationException("Embedded resource DCFontList.json not found.");
            }
            byte[] jsonData = DCTextUtils.LoadBinaryResource(typeof(DCFontList).Assembly, "DCFontList.json", true);
            jsonData = DCTextUtils.FixJsonData(jsonData);
            if (jsonData == null || jsonData.Length == 0)
            {
                throw new NotSupportedException("DCFontList.json is empty");
            }
            var reader = new Utf8JsonReader(jsonData);
            if (!reader.Read() || reader.TokenType != JsonTokenType.StartArray)
            {
                throw new InvalidOperationException("Invalid DCFontList.json format: expected array.");
            }
            var jsondoc = System.Text.Json.JsonDocument.Parse(jsonData);
            var e = jsondoc.RootElement;
            //_Values.Clear();
            var lstNames = new List<string>();
            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndArray)
                {
                    break;
                }

                if (reader.TokenType != JsonTokenType.StartObject)
                {
                    throw new InvalidOperationException("Invalid DCFontList.json format: expected object.");
                }

                var font = new FontJsonInfo();
                while (reader.Read())
                {
                    if (reader.TokenType == JsonTokenType.EndObject)
                    {
                        break;
                    }

                    if (reader.TokenType != JsonTokenType.PropertyName)
                    {
                        throw new InvalidOperationException("Invalid DCFontList.json format: expected property name.");
                    }
                    if (reader.ValueTextEquals("Name"u8))
                    {
                        reader.Read();
                        font.Name = reader.GetString();
                        continue;
                    }
                    else if (reader.ValueTextEquals("Names"u8))
                    {
                        reader.Read();
                        if (reader.TokenType == JsonTokenType.StartArray)
                        {
                            var list = new List<string>();
                            while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                            {
                                if (reader.TokenType == JsonTokenType.String)
                                {
                                    var n = reader.GetString();
                                    if (n != null && n.Length > 0)
                                    {
                                        list.Add(n);
                                    }
                                }
                                else
                                {
                                    reader.TrySkip();
                                }
                            }
                            font.Names = list.ToArray();
                        }
                        else
                        {
                            reader.TrySkip();
                        }
                    }
                    else if (reader.ValueTextEquals("Bold"u8))
                    {
                        reader.Read();
                        font.Bold = reader.GetBoolean();
                    }
                    else if (reader.ValueTextEquals("Italic"u8))
                    {
                        reader.Read();
                        font.Italic = reader.GetBoolean();
                    }
                    else if (reader.ValueTextEquals("Snapshort"u8))
                    {
                        reader.Read();
                        if (reader.TokenType == JsonTokenType.String)
                        {
                            var b64_ = reader.ValueSpan;
                            if (b64_ != null && b64_.Length > 0)
                            {
                                font.Snapshort = DCTextUtils.FromBase64StringBinary(b64_);
                            }
                            //var b64 = reader.GetString();
                            //if (!string.IsNullOrEmpty(b64))
                            //{
                            //    font.Snapshort = Convert.FromBase64String(b64);
                            //}
                        }
                    }
                    else if (reader.ValueTextEquals("FileName"u8))
                    {
                        reader.Read();
                        font.FileName = reader.GetString();
                    }
                    else if (reader.ValueTextEquals("BoldZoomRate"u8))
                    {
                        reader.Read();
                        if (reader.TokenType == JsonTokenType.Number && reader.TryGetDouble(out var rate))
                        {
                            font.BoldZoomRate = rate;
                        }
                    }
                    else if (reader.ValueTextEquals("LineSpacing"u8))
                    {
                        reader.Read();
                        if (reader.TokenType == JsonTokenType.Number && reader.TryGetInt32(out var intValue))
                        {
                            font.LineSpacing = intValue;
                        }
                    }
                    else if (reader.ValueTextEquals("CellAscent"u8))
                    {
                        reader.Read();
                        if (reader.TokenType == JsonTokenType.Number && reader.TryGetInt32(out var intValue))
                        {
                            font.CellAscent = intValue;
                        }
                    }
                    else if (reader.ValueTextEquals("CellDescent"u8))
                    {
                        reader.Read();
                        if (reader.TokenType == JsonTokenType.Number && reader.TryGetInt32(out var intValue))
                        {
                            font.CellDescent = intValue;
                        }
                    }
                    else if (reader.ValueTextEquals("EmHeight"u8))
                    {
                        reader.Read();
                        if (reader.TokenType == JsonTokenType.Number && reader.TryGetInt32(out var intValue))
                        {
                            font.EmHeight = intValue;
                        }
                    }
                    else
                    {
                        reader.TrySkip();
                    }
                }
                //Console.WriteLine(font.Name);
                //if (lstNames.Contains(font.Name) == false)
                //{
                //    lstNames.Add(font.Name);
                //}
                _Values.Add(font);
            }
            var fms = new List<FontFamily>();
            foreach (var item in _Values)
            {
                if (item.Name != null && item.Name.Length > 0)
                {
                    var fm = new FontFamily(
                    item.Name,
                    item.CellAscent,
                    item.CellDescent,
                    item.EmHeight,
                    item.LineSpacing);
                    fms.Add(fm);
                }
                else if (item.Names != null && item.Names.Length > 0)
                {
                    foreach (var item2 in item.Names)
                    {
                        if ( string.IsNullOrEmpty(item2) == false )
                        {
                            var fm = new FontFamily(
                                item2,
                                item.CellAscent,
                                item.CellDescent,
                                item.EmHeight,
                                item.LineSpacing);
                            fms.Add(fm);
                        }
                    }
                }
            }
            fms.Sort(delegate (FontFamily f1, FontFamily f2)
            {
                return string.Compare(f1.Name, f2.Name , true );
            });
            FontFamily.InnerSetFamilies(fms.ToArray());
            lstNames.Clear();
        }
        /*
          修正 DCFontList.json的代码如下
        
        var strJson = System.IO.File.ReadAllText(@"E:\Source\DCWriter5\trunk\DCSoft.WF2W\DCSoft.WF2W.Core\WinFormCore\DCFontList.json");
            System.Text.Json.Nodes.JsonArray json2 = System.Text.Json.Nodes.JsonNode.Parse(strJson).AsArray();
            var enumer = json2.GetEnumerator();
            while (enumer.MoveNext())
            {
                try
                {
                    FontFamily fm = null;
                    var curNode = enumer.Current.AsObject();
                    if (curNode.TryGetPropertyValue("Name", out var strName))
                    {
                        try
                        {
                            fm = new FontFamily(strName.GetValue<string>());
                        }
                        catch (Exception ext)
                        {
                            Console.WriteLine(ext.ToString());
                        }
                    }
                    else if (curNode.TryGetPropertyValue("Names", out var namesNode))
                    {
                        foreach (var item2 in namesNode.AsArray())
                        {
                            try
                            {
                                fm = new FontFamily(item2.GetValue<string>());
                                break;
                            }
                            catch (Exception ext2)
                            {
                                Console.WriteLine(ext2.ToString());
                            }
                        }
                    }
                    if (fm != null)
                    {
                        curNode.Add("LineSpacing", fm.GetLineSpacing(FontStyle.Regular));
                        curNode.Add("CellAscent", fm.GetCellAscent(FontStyle.Regular));
                        curNode.Add("CellDescent", fm.GetCellDescent(FontStyle.Regular));
                        curNode.Add("EmHeight", fm.GetEmHeight(FontStyle.Regular));
                    }
                }
                catch (Exception ext9)
                {
                    Console.WriteLine(ext9.ToString());
                }
            }
            var strJson3 = json2.ToJsonString(new System.Text.Json.JsonSerializerOptions()
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            });

         */
        // 获得本模块支持的字体名称
        public static List<string> GetFontNames()
        {
            var names = new List<string>();
            foreach (var item in _Values)
            {
                if (!string.IsNullOrEmpty(item.Name))
                {
                    if (!names.Contains(item.Name))
                    {
                        names.Add(item.Name);
                    }
                }
                else if (item.Names != null)
                {
                    foreach (var n in item.Names)
                    {
                        if (!names.Contains(n))
                        {
                            names.Add(n);
                        }
                    }
                }
            }
            return names;
        }
        public static FontJsonInfo GetInfo(string strName, bool bolBold, bool bolItalic)
        {
            if (strName == null || strName.Length == 0)
            {
                return null;
            }
            List<FontJsonInfo> matchItems = null;
            foreach (var item in _Values)
            {
                if (item.Name == strName)
                {
                    if (item.Bold == bolBold && item.Italic == bolItalic)
                    {
                        return item;
                    }
                    if (matchItems == null)
                    {
                        matchItems = new List<FontJsonInfo>();
                    }
                    matchItems.Add(item);
                }
                else if (item.Names != null && Array.IndexOf<string>(item.Names, strName) >= 0)
                {
                    if (item.Bold == bolBold && item.Italic == bolItalic)
                    {
                        return item;
                    }
                    if (matchItems == null)
                    {
                        matchItems = new List<FontJsonInfo>();
                    }
                    matchItems.Add(item);
                }
            }
            if (matchItems != null)
            {
                if (bolBold)
                {
                    foreach (var item in matchItems)
                    {
                        if (item.Bold)
                        {
                            return item;
                        }
                    }
                }
                if (bolItalic)
                {
                    foreach (var item in matchItems)
                    {
                        if (item.Italic)
                        {
                            return item;
                        }
                    }
                }
                return matchItems[0];
            }
            return null;
        }

        // 获得字体信息键值
        public static string? GetFontInfoKeyName(string? name, bool bold, bool italic)
        {
            if (string.IsNullOrEmpty(name))
            {
                return null;
            }

            FontJsonInfo? resultItem = null;
            var matchItems = new List<FontJsonInfo>();

            foreach (var item in _Values)
            {
                var itemBold = item.Bold;
                var itemItalic = item.Italic;

                if (item.Name != null && item.Name == name)
                {
                    if (itemBold == bold && itemItalic == italic)
                    {
                        resultItem = item;
                        break;
                    }
                    matchItems.Add(item);
                }
                else if (item.Names != null && item.Names.Contains(name))
                {
                    if (itemBold == bold && itemItalic == italic)
                    {
                        resultItem = item;
                        break;
                    }
                    matchItems.Add(item);
                }
            }

            if (resultItem == null && matchItems.Count > 0)
            {
                if (italic)
                {
                    resultItem = matchItems.FirstOrDefault(mi => mi.Italic);
                }
                if (resultItem == null && bold)
                {
                    resultItem = matchItems.FirstOrDefault(mi => mi.Bold);
                }
                resultItem ??= matchItems[0];
            }

            s_fontTable ??= new Dictionary<string, FontJsonInfo>(StringComparer.Ordinal);
            s_fontTable["dc_bad_font_key"] = null!; // 映射占位

            if (resultItem == null)
            {
                return "dc_bad_font_key";
            }
            else
            {
                var key = GetNativeKey(name, resultItem.Bold, resultItem.Italic);
                s_fontTable[key] = resultItem;
                return key;
            }
        }

        // 生成原生键名
        public static string GetNativeKey(string name, bool bold, bool italic)
        {
            if (bold && italic)
            {
                return name + "$bold italic";
            }
            else if (bold && !italic)
            {
                return name + "$bold";
            }
            else if (!bold && italic)
            {
                return name + "$italic";
            }
            else
            {
                return name;
            }
        }


    }
}