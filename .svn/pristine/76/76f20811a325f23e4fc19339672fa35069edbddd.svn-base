using System.Drawing;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace DCSoft
{
    internal static class DCValueConvert
    {
        private static bool _IsBlazorWASM = false;
        internal static void InBlazorWASM()
        {
            _IsBlazorWASM = true;
        }
        internal static void CheckIsBlazorWASM()
        {
            if(_IsBlazorWASM == false )
            {
                Console.WriteLine("!!!! WF2W only can used in Blazor WASM !!!!");
                throw new InvalidOperationException("WF2W only can used in Blazor WASM.");
            }
        }

        public static PointF[] ToPointFArray(Point[] points)
        {
            if (points == null) return null;
            var result = new PointF[points.Length];
            for (int i = 0; i < points.Length; i++)
            {
                result[i] = new PointF(points[i].X, points[i].Y);
            }
            return result;
        }
        public static Rectangle ToInt32(RectangleF rect)
        {
            var x2 = (int)rect.X;
            var y2 = (int)rect.Y;
            return new Rectangle(
                x2,
                y2,
                (int)(rect.Right - x2),
                (int)(rect.Bottom - y2));
        }

        private static readonly string[] _Int32Strings = BuildInt32Strings();
        private static string[] BuildInt32Strings()
        {
            var result = new string[200];
            for (int iCount = 0; iCount < result.Length; iCount++)
            {
                result[iCount] = iCount.ToString();
            }
            return result;
        }
        public static string Int32ToString(int v)
        {
            if (v == 0)
            {
                return "0";
            }
            if (v == 1)
            {
                return "1";
            }
            if (v >= 0 && v < 200)
            {
                return _Int32Strings[v];
            }
            else
            {
                return v.ToString();
            }
        }

        public static Size ConvertToSize(this JsonObject json)
        {
            if (json == null) return Size.Empty;
            var result = Size.Empty;
            foreach (var item in json)
            {
                switch (item.Key)
                {
                    case "Width": result.Width = ConvertToInt32(item.Value, 0); break;
                    case "Height": result.Height = ConvertToInt32(item.Value, 0); break;
                }
            }
            return result;
        }

        public static Point ConvertToPoint(this JsonObject json)
        {
            if (json == null) return Point.Empty;
            var result = Point.Empty;
            foreach (var item in json)
            {
                switch (item.Key)
                {
                    case "X": result.X = ConvertToInt32(item.Value, 0); break;
                    case "Y": result.Y = ConvertToInt32(item.Value, 0); break;
                }
            }
            return result;
        }

        public static Rectangle ConvertToRectangle( this JsonObject json)
        {
            if (json == null) return Rectangle.Empty;
            var rect = Rectangle.Empty;
            foreach (var item in json)
            {
                switch (item.Key)
                {
                    case "Left": rect.X = ConvertToInt32(item.Value, 0); break;
                    case "Top": rect.Y = ConvertToInt32(item.Value, 0); break;
                    case "Width": rect.Width = ConvertToInt32(item.Value, 0); break;
                    case "Height": rect.Height = ConvertToInt32(item.Value, 0); break;
                }
            }
            return rect;
        }
        /// <summary>
        /// 计算数据的MD5值
        /// </summary>
        /// <param name="bsData">原始数据</param>
        /// <returns>生成的MD5数据</returns>
        public static byte[] GetMD5(byte[] bsData )
        {
            if (bsData == null) return Array.Empty<byte>();

            // MD5 constants
            uint[] K = new uint[64]
            {
                0xd76aa478, 0xe8c7b756, 0x242070db, 0xc1bdceee,
                0xf57c0faf, 0x4787c62a, 0xa8304613, 0xfd469501,
                0x698098d8, 0x8b44f7af, 0xffff5bb1, 0x895cd7be,
                0x6b901122, 0xfd987193, 0xa679438e, 0x49b40821,
                0xf61e2562, 0xc040b340, 0x265e5a51, 0xe9b6c7aa,
                0xd62f105d, 0x02441453, 0xd8a1e681, 0xe7d3fbc8,
                0x21e1cde6, 0xc33707d6, 0xf4d50d87, 0x455a14ed,
                0xa9e3e905, 0xfcefa3f8, 0x676f02d9, 0x8d2a4c8a,
                0xfffa3942, 0x8771f681, 0x6d9d6122, 0xfde5380c,
                0xa4beea44, 0x4bdecfa9, 0xf6bb4b60, 0xbebfbc70,
                0x289b7ec6, 0xeaa127fa, 0xd4ef3085, 0x04881d05,
                0xd9d4d039, 0xe6db99e5, 0x1fa27cf8, 0xc4ac5665,
                0xf4292244, 0x432aff97, 0xab9423a7, 0xfc93a039,
                0x655b59c3, 0x8f0ccc92, 0xffeff47d, 0x85845dd1,
                0x6fa87e4f, 0xfe2ce6e0, 0xa3014314, 0x4e0811a1,
                0xf7537e82, 0xbd3af235, 0x2ad7d2bb, 0xeb86d391
            };
            int[] s = new int[64]
            {
                7,12,17,22, 7,12,17,22, 7,12,17,22, 7,12,17,22,
                5,9,14,20, 5,9,14,20, 5,9,14,20, 5,9,14,20,
                4,11,16,23, 4,11,16,23, 4,11,16,23, 4,11,16,23,
                6,10,15,21, 6,10,15,21, 6,10,15,21, 6,10,15,21
            };

            // Initialize variables:
            uint a0 = 0x67452301;
            uint b0 = 0xefcdab89;
            uint c0 = 0x98badcfe;
            uint d0 = 0x10325476;

            // Create padded input
            ulong bitLen = (ulong)bsData.Length * 8UL;
            int padding = ((56 - ((bsData.Length + 1) % 64)) + 64) % 64; // bytes to get length to 56 mod 64
            int totalLen = bsData.Length + 1 + padding + 8; // 1 for 0x80, 8 for length
            byte[] chunk = new byte[totalLen];
            Buffer.BlockCopy(bsData, 0, chunk, 0, bsData.Length);
            chunk[bsData.Length] = 0x80;
            // length in bits, little endian
            for (int i = 0; i < 8; i++)
            {
                chunk[totalLen - 8 + i] = (byte)((bitLen >> (8 * i)) & 0xFF);
            }

            static uint LeftRotate(uint x, int c) => (x << c) | (x >> (32 - c));

            // Process in 512-bit chunks
            for (int offset = 0; offset < totalLen; offset += 64)
            {
                // break chunk into sixteen 32-bit words M[j], little-endian
                uint[] M = new uint[16];
                for (int j = 0; j < 16; j++)
                {
                    int idx = offset + j * 4;
                    M[j] = (uint)(chunk[idx] | (chunk[idx + 1] << 8) | (chunk[idx + 2] << 16) | (chunk[idx + 3] << 24));
                }

                uint A = a0, B = b0, C = c0, D = d0;

                for (int i = 0; i < 64; i++)
                {
                    uint F;
                    int g;
                    if (i < 16)
                    {
                        F = (B & C) | (~B & D);
                        g = i;
                    }
                    else if (i < 32)
                    {
                        F = (D & B) | (~D & C);
                        g = (5 * i + 1) % 16;
                    }
                    else if (i < 48)
                    {
                        F = B ^ C ^ D;
                        g = (3 * i + 5) % 16;
                    }
                    else
                    {
                        F = C ^ (B | ~D);
                        g = (7 * i) % 16;
                    }

                    uint temp = D;
                    D = C;
                    C = B;
                    B = B + LeftRotate(A + F + K[i] + M[g], s[i]);
                    A = temp;
                }

                a0 += A;
                b0 += B;
                c0 += C;
                d0 += D;
            }

            // Output is little-endian 128-bit digest
            byte[] digest = new byte[16];
            void WriteLE(uint val, int pos)
            {
                digest[pos + 0] = (byte)(val & 0xFF);
                digest[pos + 1] = (byte)((val >> 8) & 0xFF);
                digest[pos + 2] = (byte)((val >> 16) & 0xFF);
                digest[pos + 3] = (byte)((val >> 24) & 0xFF);
            }

            WriteLE(a0, 0);
            WriteLE(b0, 4);
            WriteLE(c0, 8);
            WriteLE(d0, 12);
            return digest;
        }
        /// <summary>
        /// 将图标数据转化为图片数据
        /// </summary>
        /// <param name="bsIcon">图标原始数据</param>
        /// <returns>转换后的图片数据</returns>
        public static byte[] IconToImage(byte[] bsIcon )
        {
            if (bsIcon == null || bsIcon.Length < 6) return Array.Empty<byte>();
            // ICO header: 6 bytes
            // 0-1: reserved (0), 2-3: type (1=icon), 4-5: count
            int reserved = bsIcon[0] | (bsIcon[1] << 8);
            int type = bsIcon[2] | (bsIcon[3] << 8);
            int count = bsIcon[4] | (bsIcon[5] << 8);
            if (reserved != 0 || type != 1 || count <= 0) return Array.Empty<byte>();

            // Directory entry: 16 bytes each
            int entryOffset = 6;
            if (bsIcon.Length < entryOffset + 16) return Array.Empty<byte>();
            // Use first entry
            byte width = bsIcon[entryOffset + 0];
            byte height = bsIcon[entryOffset + 1];
            // byte colorCount = bsIcon[entryOffset + 2];
            // byte reserved2 = bsIcon[entryOffset + 3];
            ushort planes = (ushort)(bsIcon[entryOffset + 4] | (bsIcon[entryOffset + 5] << 8));
            ushort bitCount = (ushort)(bsIcon[entryOffset + 6] | (bsIcon[entryOffset + 7] << 8));
            int bytesInRes = bsIcon[entryOffset + 8]
                           | (bsIcon[entryOffset + 9] << 8)
                           | (bsIcon[entryOffset + 10] << 16)
                           | (bsIcon[entryOffset + 11] << 24);
            int imgOffset = bsIcon[entryOffset + 12]
                          | (bsIcon[entryOffset + 13] << 8)
                          | (bsIcon[entryOffset + 14] << 16)
                          | (bsIcon[entryOffset + 15] << 24);

            if (imgOffset <= 0 || bytesInRes <= 0 || bsIcon.Length < imgOffset + bytesInRes) return Array.Empty<byte>();
            var imgData = new ReadOnlySpan<byte>(bsIcon, imgOffset, bytesInRes);

            // PNG signature: 89 50 4E 47 0D 0A 1A 0A
            if (imgData.Length >= 8
                && imgData[0] == 0x89 && imgData[1] == 0x50 && imgData[2] == 0x4E && imgData[3] == 0x47
                && imgData[4] == 0x0D && imgData[5] == 0x0A && imgData[6] == 0x1A && imgData[7] == 0x0A)
            {
                // Return PNG image as-is
                return imgData.ToArray();
            }

            // Otherwise assume DIB (BITMAPINFOHEADER + pixels). Create BMP by prepending BITMAPFILEHEADER.
            // BITMAPFILEHEADER (14 bytes):
            // bfType 'BM' (0x4D42), bfSize, bfReserved1=0, bfReserved2=0, bfOffBits = 14 + biSize
            // biSize is the first 4 bytes of DIB
            if (imgData.Length < 4) return Array.Empty<byte>();
            int biSize = imgData[0] | (imgData[1] << 8) | (imgData[2] << 16) | (imgData[3] << 24);
            int bfOffBits = 14 + biSize; // start of pixel array relative to file start
            int bfSize = 14 + imgData.Length;

            byte[] bmp = new byte[bfSize];
            // Write BITMAPFILEHEADER
            // bfType
            bmp[0] = 0x42; // 'B'
            bmp[1] = 0x4D; // 'M'
            // bfSize
            bmp[2] = (byte)(bfSize & 0xFF);
            bmp[3] = (byte)((bfSize >> 8) & 0xFF);
            bmp[4] = (byte)((bfSize >> 16) & 0xFF);
            bmp[5] = (byte)((bfSize >> 24) & 0xFF);
            // bfReserved1, bfReserved2 = 0
            bmp[6] = bmp[7] = bmp[8] = bmp[9] = 0;
            // bfOffBits
            bmp[10] = (byte)(bfOffBits & 0xFF);
            bmp[11] = (byte)((bfOffBits >> 8) & 0xFF);
            bmp[12] = (byte)((bfOffBits >> 16) & 0xFF);
            bmp[13] = (byte)((bfOffBits >> 24) & 0xFF);

            // Copy DIB data after header
            imgData.CopyTo(new Span<byte>(bmp, 14, imgData.Length));
            return bmp;
        }
        /// <summary>
        /// 将字体数据转化为CSS font 属性值,可以在HTML JS 中使用 element.style.font = "font string" 来使用。
        /// </summary>
        /// <param name="f">字体对象</param>
        /// <returns>CSS样式的字符串</returns>
        public static string FontToCssString( System.Drawing.Font f )
        {
            if (f == null) return string.Empty;

            // font-style
            string fontStyle = f.Italic ? "italic" : "normal";
            // font-variant (not supported by System.Drawing.Font) -> normal
            string fontVariant = "normal";
            // font-weight
            string fontWeight = f.Bold ? "bold" : "normal";

            // font-size with unit
            string unitStr;
            switch (f.Unit)
            {
                case GraphicsUnit.Pixel:
                    unitStr = "px";
                    break;
                case GraphicsUnit.Point:
                    unitStr = "pt";
                    break;
                case GraphicsUnit.Inch:
                    unitStr = "in";
                    break;
                case GraphicsUnit.Millimeter:
                    unitStr = "mm";
                    break;
                // Map display & document to pt as a reasonable default
                case GraphicsUnit.Display:
                case GraphicsUnit.Document:
                default:
                    unitStr = "pt";
                    break;
            }

            string fontSize = f.Size.ToString(System.Globalization.CultureInfo.InvariantCulture) + unitStr;

            // line-height: keep normal to avoid affecting layout; shorthand requires size[/line-height] optional
            string lineHeightPart = string.Empty; // e.g., "/normal"

            // font-family: quote if contains spaces or special characters, allow fallback list if provided in Name
            string family = f.Name ?? "sans-serif";
            string familyCss;
            if (family.IndexOf(',') >= 0)
            {
                // Already a list; ensure each item is trimmed/quoted as needed
                var parts = family.Split(',');
                for (int i = 0; i < parts.Length; i++)
                {
                    var p = parts[i].Trim();
                    parts[i] = NeedsQuotes(p) ? QuoteFamily(p) : p;
                }
                familyCss = string.Join(", ", parts);
            }
            else
            {
                familyCss = NeedsQuotes(family) ? QuoteFamily(family) : family;
            }

            // Assemble CSS font shorthand: style variant weight size[/line-height] family
            string css = string.Concat(fontStyle, " ", fontVariant, " ", fontWeight, " ", fontSize, lineHeightPart, " ", familyCss);
            return css;

            static bool NeedsQuotes(string name)
            {
                // Quote if contains space or special punctuation
                for (int i = 0; i < name.Length; i++)
                {
                    char c = name[i];
                    if (char.IsWhiteSpace(c) || c == '-' || c == '_' || c == '#')
                    {
                        return true;
                    }
                }
                // Also quote if starts with a digit
                if (name.Length > 0 && char.IsDigit(name[0])) return true;
                return false;
            }

            static string QuoteFamily(string name)
            {
                // Escape existing quotes
                var safe = name.Replace("\"", "\\\"");
                return "\"" + safe + "\"";
            }
        }


        public static Color ConvertToColor(this JsonValue v, System.Drawing.Color defaultValue)
        {
            if (v == null) { return defaultValue; }
            var vk = v.GetValueKind();
            if (vk == System.Text.Json.JsonValueKind.Null || vk == System.Text.Json.JsonValueKind.Undefined)
            {
                return defaultValue;
            }
            if (vk == JsonValueKind.String)
            {
                return ConvertToColor(v.ToString(), defaultValue);
            }
            else
            {
                return defaultValue;
            }
        }
        public static Color ConvertToColor(this JsonElement v, System.Drawing.Color defaultValue)
        {
            var vk = v.ValueKind;
            if (vk == System.Text.Json.JsonValueKind.Null || vk == System.Text.Json.JsonValueKind.Undefined)
            {
                return defaultValue;
            }
            if (vk == JsonValueKind.String)
            {
                return ConvertToColor(v.GetString(), defaultValue);
            }
            else
            {
                return defaultValue;
            }
        }
        public static float ConvertToSingle(this JsonElement v , float defaultValue)
        {
            var vk = v.ValueKind;
            if (vk == System.Text.Json.JsonValueKind.Null || vk == System.Text.Json.JsonValueKind.Undefined)
            {
                return defaultValue;
            }
            if (vk == JsonValueKind.Number) return (float)v.GetDouble();
            return ConvertToSingle( v.ToString(), defaultValue);
        }
        public static float ConvertToSingle(this JsonNode v , float defaultValue)
        {
            if (v == null) { return defaultValue; }
            var vk = v.GetValueKind();
            if (vk == System.Text.Json.JsonValueKind.Null || vk == System.Text.Json.JsonValueKind.Undefined)
            {
                return defaultValue;
            }
            if (vk == JsonValueKind.Number)
            {
                if (v is JsonValue jv)
                {
                    if (jv.TryGetValue<float>(out var f)) return f;
                    if (jv.TryGetValue<double>(out var d)) return (float)d;
                    if (jv.TryGetValue<decimal>(out var m)) return (float)m;
                    if (jv.TryGetValue<int>(out var i)) return i;
                    if (jv.TryGetValue<long>(out var l)) return l;
                }
                // Fallback stringify
                return ConvertToSingle(v.ToString(), defaultValue);
            }
            return ConvertToSingle(v.ToString(), defaultValue);
        }
        public static int ConvertToInt32(this JsonElement v , int defaultValue)
        {
            var vk = v.ValueKind;
            if (vk == System.Text.Json.JsonValueKind.Null || vk == System.Text.Json.JsonValueKind.Undefined)
            {
                return defaultValue;
            }
            if (vk == JsonValueKind.Number) return (int)v.GetDouble();
            return ConvertToInt32( v.ToString(), defaultValue);
        }
        public static string ConvertToString( this JsonNode node )
        {
            if (node == null) return null;
            var vk = node.GetValueKind();
            if (vk == JsonValueKind.String) return node.ToString();
            if (vk == JsonValueKind.True) return "true";
            if (vk == JsonValueKind.False) return "false";
            if(vk == JsonValueKind.Null || vk == JsonValueKind.Undefined) return null;
            return node.ToString();
        }

        public static int ConvertToInt32(this JsonNode v , int defaultValue)
        {
            if (v == null) { return defaultValue; }
            var vk = v.GetValueKind();
            if (vk == System.Text.Json.JsonValueKind.Null || vk == System.Text.Json.JsonValueKind.Undefined)
            {
                return defaultValue;
            }
            if (vk == JsonValueKind.Number)
            {
                if (v is JsonValue jv)
                {
                    if (jv.TryGetValue<int>(out var i)) return i;
                    if (jv.TryGetValue<long>(out var l)) return (int)l;
                    if (jv.TryGetValue<double>(out var d)) return (int)d;
                    if (jv.TryGetValue<decimal>(out var m)) return (int)m;
                }
                return ConvertToInt32(v.ToString(), defaultValue);
            }
            return ConvertToInt32(v.ToString(), defaultValue);
        }
        public static int ConvertToInt32(this JsonNode v)
        {
            if (v == null) { return 0; }
            var vk = v.GetValueKind();
            if (vk == System.Text.Json.JsonValueKind.Null || vk == System.Text.Json.JsonValueKind.Undefined)
            {
                return 0;
            }
            if (vk == JsonValueKind.Number)
            {
                if (v is JsonValue jv)
                {
                    if (jv.TryGetValue<int>(out var i)) return i;
                    if (jv.TryGetValue<long>(out var l)) return (int)l;
                    if (jv.TryGetValue<double>(out var d)) return (int)d;
                    if (jv.TryGetValue<decimal>(out var m)) return (int)m;
                }
                return ConvertToInt32(v.ToString(), 0);
            }
            return ConvertToInt32(v.ToString(), 0);
        }
        public static bool ConvertToBoolean(this JsonElement v , bool defaultValue)
        {
            var vk = v.ValueKind;
            if (vk == System.Text.Json.JsonValueKind.True) return true;
            if (vk == System.Text.Json.JsonValueKind.False) return false;
            if (vk == System.Text.Json.JsonValueKind.Null || vk == System.Text.Json.JsonValueKind.Undefined)
            {
                return defaultValue;
            }
            return ConvertToBoolean(v.ToString(), defaultValue);
        }
        public static bool ConvertToBoolean(this JsonValue v , bool defaultValue)
        {
            if (v == null) { return defaultValue; }
            var vk = v.GetValueKind();
            if (vk == System.Text.Json.JsonValueKind.True) return true;
            if( vk == System.Text.Json.JsonValueKind.False) return false;
            if(vk == System.Text.Json.JsonValueKind.Null || vk == System.Text.Json.JsonValueKind.Undefined )
            {
                return defaultValue;
            }
            return ConvertToBoolean(v.ToString(), defaultValue);
        }
        public static bool ConvertToBoolean(string v, bool defaultValue)
        {
            if (string.IsNullOrEmpty(v)) { return defaultValue; }
            v = v.Trim();
            if (v.Equals("true", StringComparison.OrdinalIgnoreCase)) { return true; }
            if (v.Equals("false", StringComparison.OrdinalIgnoreCase)) { return false; }
            if (v.Equals("yes", StringComparison.OrdinalIgnoreCase)) { return true; }
            if (v.Equals("no", StringComparison.OrdinalIgnoreCase)) { return false; }
            return defaultValue;
        }
        public static int ConvertToInt32(string v, int defaultValue)
        {
            if (string.IsNullOrEmpty(v)) { return defaultValue; }
            v = v.Trim();
            if(int.TryParse(v, out int result)) { return result; }
            return defaultValue;
        }
        public static float ConvertToSingle(string v, float defaultValue)
        {
            if (string.IsNullOrEmpty(v)) { return defaultValue; }
            v = v.Trim();
            if (float.TryParse(v, out float result)) { return result; }
            return defaultValue;
        }
        public static System.Drawing.Color ConvertToColor(string v, System.Drawing.Color defaultValue)
        {
            if (string.IsNullOrEmpty(v)) { return defaultValue; }
            v = v.Trim();
            Color c = ColorTranslator.FromHtml(v);
            return c;
        }
    }
}
