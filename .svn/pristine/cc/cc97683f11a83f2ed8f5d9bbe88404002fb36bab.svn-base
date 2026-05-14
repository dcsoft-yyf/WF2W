global using DCSoft;
using System.Runtime.CompilerServices;
namespace DCSoft
{
    using System.ComponentModel.DataAnnotations;
    using System.Drawing;
    using System.Drawing.Imaging;

    internal static class DCTextUtils
    {
        private static readonly string[] _SingleTextArray = new string[1];
        /// <summary>
        /// 根据文本中的换行回车符将一个字符串拆分成多个字符串
        /// </summary>
        /// <param name="strText">原始文本</param>
        /// <param name="useCachedSingleTextArray">是否启用缓存的单个字符串数组</param>
        /// <returns>拆分后的文本</returns>
        /// <remarks>如果只有一个字符串，则使用_SingleTextArray来避免重复分配单个字符串数组</remarks>
        public static string[] GetLines( string strText , bool useCachedSingleTextArray)
        {
            if (strText == null)
            {
                return null;
            }
            if (strText.Length == 0)
            {
                _SingleTextArray[0] = string.Empty;
                return _SingleTextArray;
            }
            int lineCount = 1;
            var strTextLength = strText.Length;
            for (int i = 0; i < strTextLength; i++)
            {
                char c = strText[i];
                if (c == '\r')
                {
                    lineCount++;
                    if (i + 1 < strTextLength && strText[i + 1] == '\n')
                    {
                        i++;
                    }
                }
                else if (c == '\n')
                {
                    lineCount++;
                }
            }

            if (lineCount == 1)
            {
                if (useCachedSingleTextArray)
                {
                    _SingleTextArray[0] = strText;
                    return _SingleTextArray;
                }
                else
                {
                    return new string[] { strText };
                }
            }

            var result = new string[lineCount];
            int lineIndex = 0;
            int start = 0;
            for (int i = 0; i < strTextLength; i++)
            {
                char c = strText[i];
                if (c == '\r' || c == '\n')
                {
                    result[lineIndex++] = strText.Substring(start, i - start);
                    if (c == '\r' && i + 1 < strTextLength && strText[i + 1] == '\n')
                    {
                        i++;
                    }
                    start = i + 1;
                }
            }

            result[lineIndex] = start <= strTextLength ? strText.Substring(start, strTextLength - start) : string.Empty;
            return result;
        }
        /// <summary>
        ///  &nbsp;
        /// </summary>
        /// <remarks>
        /// 不换行空格，全称No-Break Space，它是最常见和我们使用最多的空格，
        /// 大多数的人可能只接触了&nbsp;，它是按下space键产生的空格。在HTML中，
        /// 如果你用空格键产生此空格，空格是不会累加的（只算1个）。要使用html
        /// 实体表示才可累加，该空格占据宽度受字体影响明显而强烈。
        /// </remarks>
        public const char CHAR_NBSP = (char)160;
        /// <summary>
        /// @ensp
        /// </summary>
        /// <remarks>
        /// “半角空格”，全称是En Space，en是字体排印学的计量单位，为em宽度的一半。
        /// 根据定义，它等同于字体度的一半（如16px字体中就是8px）。名义上是小写字母n
        /// 的宽度。此空格传承空格家族一贯的特性：透明的，此空格有个相当稳健的特性，
        /// 就是其占据的宽度正好是1/2个中文宽度，而且基本上不受字体影响。
        /// </remarks>
        public const char CHAR_ENSP = (char)8194;
        public static readonly string STRING_ENSP = new string(CHAR_ENSP, 1);
        /// <summary>
        /// &emsp
        /// </summary>
        /// <remarks>
        /// “全角空格”，全称是Em Space，em是字体排印学的计量单位，相当于当前指定的点数。
        /// 例如，1 em在16px的字体中就是16px。此空格也传承空格家族一贯的特性：透明的，
        /// 此空格也有个相当稳健的特性，就是其占据的宽度正好是1个中文宽度，而且基本上
        /// 不受字体影响。
        /// </remarks>
        public const char CHAR_EMSP = (char)8195;
        /// <summary>
        /// thinsp
        /// </summary>
        /// <remarks>
        /// 窄空格，全称是Thin Space。我们不妨称之为“瘦弱空格”，就是该空格长得比较瘦弱，
        /// 身体单薄，占据的宽度比较小。它是em之六分之一宽。
        /// </remarks>
        public const char CHAR_THINSP = (char)8201;
        /// <summary>
        /// zwnj
        /// </summary>
        /// <remarks>
        /// 零宽不连字，全称是Zero Width Non Joiner，简称“ZWNJ”，是一个不打印字符，放在
        /// 电子文本的两个字符之间，抑制本来会发生的连字，而是以这两个字符原本的字形来绘制。
        /// Unicode中的零宽不连字字符映射为“”（zero width non-joiner，U+200C），
        /// HTML字符值引用为： &#8204;
        /// </remarks>
        public const char CHAR_ZWNJ = (char)8204;
        /// <summary>
        /// zwj
        /// </summary>
        /// <remarks>
        /// 零宽连字，全称是Zero Width Joiner，简称“ZWJ”，是一个不打印字符，放在某些需要复杂
        /// 排版语言（如阿拉伯语、印地语）的两个字符之间，使得这两个本不会发生连字的字符产生了
        /// 连字效果。零宽连字符的Unicode码位是U+200D (HTML: &#8205; &zwj;）。
        /// </remarks>
        public const char CHAR_ZWJ = (char)8205;
        /// <summary>
        /// 标准空格
        /// </summary>
        public const char CHAR_WHITESPACE = (char)32;
        /// <summary>
        /// 制表符
        /// </summary>
        public const char CHAR_TAB = (char)8;
        /// <summary>
        /// 是否为HTML中的特殊的空白字符
        /// </summary>
        /// <returns></returns>
        public static bool IsHtmlWhitespace(char c)
        {
            if (c == 32 || c == CHAR_TAB)
            {
                return true;
            }
            if (c > 8000)
            {
                return c == CHAR_EMSP
                    || c == CHAR_ENSP
                    || c == CHAR_THINSP
                    || c == CHAR_ZWJ
                    || c == CHAR_ZWNJ;
            }
            else
            {
                return c == CHAR_NBSP;
            }
        }


        public static Action<string> EventDeleteBlobUrl = null;

        public static Func<byte[], string, string> EventCreateBlobUrl = null;

        /// <summary>
        /// 将一个包含BASE64字符串的字节数组转换为字节数组
        /// </summary>
        /// <param name="bsBase64">BASE64字符串的字节数组</param>
        /// <returns>转换后的结果</returns>
        public static byte[] FromBase64StringBinary(ReadOnlySpan<byte> bsBase64)
        {
            if (bsBase64.IsEmpty)
            {
                return Array.Empty<byte>();
            }

            static int DecodeByte(byte b)
            {
                if (b >= 'A' && b <= 'Z') return b - 'A';
                if (b >= 'a' && b <= 'z') return b - 'a' + 26;
                if (b >= '0' && b <= '9') return b - '0' + 52;
                if (b == (byte)'+') return 62;
                if (b == (byte)'/') return 63;
                if (b == (byte)'=') return -3; // padding
                if (b == (byte)' ' || b == (byte)'\r' || b == (byte)'\n' || b == (byte)'\t') return -1; // whitespace
                return -2; // invalid
            }

            int maxDecoded = (bsBase64.Length / 4 + 1) * 3;
            byte[] output = new byte[maxDecoded];
            int outIndex = 0;
            int quartet = 0;
            int buffer = 0;
            int padCount = 0;

            foreach (var b in bsBase64)
            {
                int val = DecodeByte(b);
                if (val == -1)
                {
                    continue; // skip whitespace
                }
                if (val == -2)
                {
                    throw new FormatException("Invalid base64 data.");
                }
                if (val == -3)
                {
                    padCount++;
                    val = 0;
                }

                buffer = (buffer << 6) | val;
                quartet++;

                if (quartet == 4)
                {
                    output[outIndex++] = (byte)(buffer >> 16);
                    if (padCount < 2) output[outIndex++] = (byte)(buffer >> 8);
                    if (padCount < 1) output[outIndex++] = (byte)buffer;

                    buffer = 0;
                    quartet = 0;
                    padCount = 0;
                }
            }

            if (quartet != 0)
            {
                throw new FormatException("Invalid base64 data length.");
            }

            if (outIndex == output.Length)
            {
                return output;
            }

            var result = new byte[outIndex];
            Buffer.BlockCopy(output, 0, result, 0, outIndex);
            return result;
        }
        /// <summary>
        /// 修改一个JSON字符串
        /// </summary>
        /// <param name="bs">字节数组</param>
        /// <returns>修正后的字节数组</returns>
        public static byte[] FixJsonData(byte[] bs )
        {
            if(bs != null &&bs.Length > 2 )
            {
                var len = Math.Min(bs.Length, 10);
                var intStartIndex = 0;
                var intEndIndex = bs.Length - 1;
                for(var iCount = 0;iCount < len;iCount ++)
                {
                    var c = (char)bs[iCount];
                    if( c == '[' || c == '{')
                    {
                        intStartIndex = iCount;
                        break;
                    }
                }
                for(var iCount = bs.Length -1;iCount >=0;iCount --)
                {
                    var c = (char)bs[iCount];
                    if(c == ']' || c == '}')
                    {
                        intEndIndex = iCount;
                        break;
                    }
                }
                if(intStartIndex == 0 && intEndIndex == bs.Length -1 )
                {
                    return bs;
                }
                var temp = new byte[intEndIndex - intStartIndex+1];
                Buffer.BlockCopy(bs, intStartIndex, temp, 0, temp.Length);
                return temp;
            }
            return bs;
        }

        //public static ImageFormat GetImageFormat(byte[] bs)
        //{
        //    if (bs == null)
        //    {
        //        return ImageFormat.Bmp;
        //    }
        //    if (FileHeaderHelper.HasBMPHeader(bs))
        //    {
        //        return ImageFormat.Bmp;
        //    }
        //    if (FileHeaderHelper.HasPNGHeader(bs))
        //    {
        //        return ImageFormat.Png;
        //    }
        //    if (FileHeaderHelper.HasJpegHeader(bs))
        //    {
        //        return ImageFormat.Jpeg;
        //    }
        //    if (FileHeaderHelper.HasGIFHeader(bs))
        //    {
        //        return ImageFormat.Gif;
        //    }
        //    return ImageFormat.Bmp;
        //}
        //public static Size GetImageSizeFromBinary(byte[] bs)
        //{
        //    if (bs == null)
        //    {
        //        return Size.Empty;// throw new ArgumentNullException("bs");
        //    }
        //    if (FileHeaderHelper.HasBMPHeader(bs))
        //    {
        //        var width = BitConverter.ToInt32(bs, 0x12);
        //        var height = BitConverter.ToInt32(bs, 0x16);
        //        return new Size(width, height);
        //    }
        //    else if (FileHeaderHelper.HasPNGHeader(bs))
        //    {
        //        var width = BytesToInt32(bs, 16);
        //        var height = BytesToInt32(bs, 20);
        //        return new Size(width, height);
        //    }
        //    else if (FileHeaderHelper.HasJpegHeader(bs))
        //    {
        //        return GetJpgSize(bs);
        //    }
        //    else if (FileHeaderHelper.HasGIFHeader(bs))
        //    {
        //        byte[] buffer = { bs[6], bs[7], bs[8], bs[9] };
        //        var width = BitConverter.ToInt16(buffer, 0);
        //        var height = BitConverter.ToInt16(buffer, 2);
        //        return new Size(width, height);
        //    }
        //    return Size.Empty;
        //}

        ///// <summary>
        ///// 解析JPEG二进制数据获取图片尺寸（宽度/高度）
        ///// </summary>
        ///// <param name="bs">JPEG文件的二进制字节数组</param>
        ///// <returns>图片尺寸（Size.Width=宽度，Size.Height=高度）</returns>
        ///// <exception cref="ArgumentException">非有效JPEG数据</exception>
        ///// <exception cref="InvalidOperationException">未找到尺寸信息</exception>
        //internal static Size GetJpgSize(byte[] bs)
        //{
        //    // 1. 校验基础条件
        //    if (bs == null || bs.Length < 2)
        //        throw new ArgumentException("无效的JPEG二进制数据（空或长度不足）");

        //    // 2. 校验JPEG文件头（SOI标记：0xFFD8）
        //    if (bs[0] != 0xFF || bs[1] != 0xD8)
        //        throw new ArgumentException("不是有效的JPEG文件（缺少SOI标记）");

        //    int offset = 2; // 跳过SOI标记（0xFFD8）
        //    int length = bs.Length;

        //    // 3. 遍历所有标记段，寻找SOF0/SOF2（包含尺寸的标记）
        //    while (offset < length - 2)
        //    {
        //        // 跳过连续的0xFF（JPEG填充字节）
        //        while (offset < length && bs[offset] == 0xFF)
        //            offset++;

        //        if (offset >= length)
        //            break;

        //        // 获取标记类型（0xC0=SOF0，0xC2=SOF2，其他标记跳过）
        //        byte marker = bs[offset];
        //        offset++; // 移动到标记长度字段

        //        // 仅处理SOF0/SOF2（包含尺寸的核心标记）
        //        if (marker != 0xC0 && marker != 0xC2)
        //        {
        //            // 解析当前标记段的长度（2字节，大端序）
        //            if (offset + 1 >= length)
        //                throw new InvalidOperationException("JPEG标记段长度解析失败");

        //            int segmentLength = (bs[offset] << 8) | bs[offset + 1];
        //            offset += segmentLength; // 跳过整个标记段
        //            continue;
        //        }

        //        // 4. 解析SOF0/SOF2段的尺寸信息
        //        // SOF段结构（从标记后开始）：
        //        // 2字节：段长度 | 1字节：精度 | 2字节：高度 | 2字节：宽度
        //        if (offset + 6 >= length) // 至少需要：长度(2)+精度(1)+高度(2)+宽度(2) = 7字节（已跳过标记，此处offset指向长度字段）
        //            throw new InvalidOperationException("JPEG SOF段数据不完整");

        //        // 跳过段长度（2字节）+ 精度（1字节），指向高度字段
        //        offset += 3;

        //        // 解析高度（2字节，大端序）
        //        int height = (bs[offset] << 8) | bs[offset + 1];
        //        offset += 2;

        //        // 解析宽度（2字节，大端序）
        //        int width = (bs[offset] << 8) | bs[offset + 1];

        //        // 校验尺寸有效性
        //        if (height <= 0 || width <= 0)
        //            throw new InvalidOperationException("解析到无效的JPEG尺寸");

        //        return new Size(width, height);
        //    }

        //    // 未找到尺寸标记
        //    throw new InvalidOperationException("未在JPEG数据中找到尺寸信息");
        //}

        //private static int BytesToInt32(byte[] bs, int startIndex)
        //{
        //    int result = bs[startIndex];
        //    result = (result << 8) + bs[startIndex + 1];
        //    result = (result << 8) + bs[startIndex + 2];
        //    result = (result << 8) + bs[startIndex + 3];
        //    return result;
        //}
        /// <summary>
        /// 最小化JavaScript代码
        /// </summary>
        /// <param name="strJavaScript">JS代码</param>
        /// <returns>转换后的代码</returns>
        /// <remarks>删除JS中的所有的单行或者多行注释，删除无意义的空白字符和换行字符。
        /// 注意不能破坏字符串中的空白字符。还有``定义的多行字符串也不能破坏。
        /// 不能破坏正则表达式的信息，如果某个语句没有;结尾，记得留个空格。</remarks>
        public static string MimiJavaScript( string strJavaScript)
        {
            if(strJavaScript == null || strJavaScript.Length ==0)
            {
                return strJavaScript;
            }
            // 基于状态机的轻量JS压缩：
            // - 去掉 // 与 /* */ 注释
            // - 合并/删除多余空白（保留字符串/模板字符串中的内容）
            // - 识别正则字面量，避免与除法混淆（启发式）
            // - 在可能引起词法合并处保留一个空格

            ReadOnlySpan<char> src = strJavaScript.AsSpan();
            var sb = new System.Text.StringBuilder(src.Length);

            bool inS = false;     // '...'
            bool inD = false;     // "..."
            bool inT = false;     // `...`
            int  tExprDepth = 0;  // 模板字符串中 ${ ... } 深度（0 表示处于模板纯文本区）
            bool inRegex = false; // /.../flags
            bool inSL = false;    // // ... \n
            bool inML = false;    // /* ... */

            // 记录前一个已输出的非空白字符，用于空白合并与正则启发式判断
            char prevOutNonWs = '\0';
            // 记录前一个token是否为标识符或数字（结束一个表达式的常见情形）
            bool prevWasIdentOrNumber = false;

            // 判断字符类别
            static bool IsWs(char c) => c == ' ' || c == '\t' || c == '\r' || c == '\n' || c == '\f' || c == '\v';
            static bool IsIdentStart(char c) => char.IsLetter(c) || c == '_' || c == '$';
            static bool IsIdentPart(char c) => char.IsLetterOrDigit(c) || c == '_' || c == '$';
            static bool IsDecDigit(char c) => c >= '0' && c <= '9';

            // 判断前一个token是否可以结束表达式（若为真，则后续 '/' 更可能是除法而非正则）
            bool PrevCouldEndExpr()
            {
                if (prevWasIdentOrNumber) return true;
                switch (prevOutNonWs)
                {
                    case ')': case ']': case '}':
                    case '"': case '\'': case '`':
                        return true;
                    default:
                        return false;
                }
            }

            int i = 0; int n = src.Length;
            while (i < n)
            {
                char c = src[i];

                // 注释处理（单/多行）
                if (inSL)
                {
                    if (c == '\n')
                    {
                        inSL = false;
                        // Collapse consecutive blank lines
                        if (prevOutNonWs != '\n') { sb.Append(c); prevOutNonWs = '\n'; }
                        prevWasIdentOrNumber = false;
                    }
                    i++; continue;
                }
                if (inML)
                {
                    if (c == '*' && i + 1 < n && src[i + 1] == '/') { inML = false; i += 2; continue; }
                    i++; continue;
                }

                // 字符串与模板字符串（非表达式区）
                if (inS)
                {
                    sb.Append(c);
                    if (c == '\\')
                    {
                        if (i + 1 < n) { sb.Append(src[i + 1]); i += 2; continue; }
                        i++; continue;
                    }
                    if (c == '\'') { inS = false; prevOutNonWs = '\''; prevWasIdentOrNumber = false; }
                    i++; continue;
                }
                if (inD)
                {
                    sb.Append(c);
                    if (c == '\\')
                    {
                        if (i + 1 < n) { sb.Append(src[i + 1]); i += 2; continue; }
                        i++; continue;
                    }
                    if (c == '"') { inD = false; prevOutNonWs = '"'; prevWasIdentOrNumber = false; }
                    i++; continue;
                }
                if (inT && tExprDepth == 0)
                {
                    // 模板字符串文本区：逐字输出，处理转义与 ${
                    if (c == '\\')
                    {
                        sb.Append(c);
                        if (i + 1 < n) { sb.Append(src[i + 1]); i += 2; continue; }
                        i++; continue;
                    }
                    if (c == '$' && i + 1 < n && src[i + 1] == '{')
                    {
                        sb.Append("${");
                        tExprDepth = 1; i += 2; continue;
                    }
                    sb.Append(c);
                    if (c == '`') { inT = false; prevOutNonWs = '`'; prevWasIdentOrNumber = false; }
                    i++; continue;
                }

                // 正则字面量处理
                if (inRegex)
                {
                    sb.Append(c);
                    if (c == '\\')
                    {
                        if (i + 1 < n) { sb.Append(src[i + 1]); i += 2; continue; }
                        i++; continue;
                    }
                    if (c == '[')
                    {
                        // 字符类，直到下一个非转义的 ']'
                        i++;
                        while (i < n)
                        {
                            char rc = src[i];
                            sb.Append(rc);
                            if (rc == '\\') { if (i + 1 < n) { sb.Append(src[i + 1]); i += 2; continue; } else { i++; break; } }
                            if (rc == ']') { i++; break; }
                            i++;
                        }
                        continue;
                    }
                    if (c == '/')
                    {
                        // 结束正则，收集可选flags
                        i++;
                        while (i < n && IsIdentPart(src[i])) { sb.Append(src[i]); i++; }
                        inRegex = false; prevOutNonWs = '/'; prevWasIdentOrNumber = false; continue;
                    }
                    i++; continue;
                }

                // 模板字符串表达式区：按普通代码最小化，并跟踪花括号深度
                if (inT && tExprDepth > 0)
                {
                    // 更新深度
                    if (c == '{') { tExprDepth++; }
                    else if (c == '}') { tExprDepth--; if (tExprDepth == 0) { sb.Append('}'); i++; continue; } }

                    // 在表达式内，走常规最小化逻辑（不进入此分支重新实现，继续往下走通用分支）
                }

                // 通用起始：处理字符串/模板开头
                if (c == '\'') { inS = true; sb.Append(c); i++; continue; }
                if (c == '"') { inD = true; sb.Append(c); i++; continue; }
                if (c == '`') { inT = true; tExprDepth = 0; sb.Append(c); i++; continue; }

                // 处理注释或除法/正则判定
                if (c == '/' && i + 1 < n)
                {
                    char n1 = src[i + 1];
                    if (n1 == '/') { inSL = true; i += 2; continue; }
                    if (n1 == '*') { inML = true; i += 2; continue; }

                    // 判定是否正则字面量的起始
                    bool isRegexStart = !PrevCouldEndExpr();
                    if (isRegexStart)
                    {
                        inRegex = true; sb.Append('/'); i++; continue;
                    }
                    // 否则作为除法运算符
                    sb.Append('/'); prevOutNonWs = '/'; prevWasIdentOrNumber = false; i++; continue;
                }

                // 空白压缩：合并/删除
                if (IsWs(c))
                {
                    // 跳过连续空白，只在必要处保留一个空格
                    int j = i + 1;
                    while (j < n && IsWs(src[j])) j++;
                    char nextNonWs = j < n ? src[j] : '\0';

                    bool prevIsIdent = prevWasIdentOrNumber || IsIdentPart(prevOutNonWs);
                    bool nextIsIdent = IsIdentStart(nextNonWs) || IsDecDigit(nextNonWs);
                    bool needSpace = prevIsIdent && nextIsIdent;
                    // 避免把 "+ +" 或 "- -" 合并成 ++/--
                    if (!needSpace)
                    {
                        if ((prevOutNonWs == '+' && nextNonWs == '+') || (prevOutNonWs == '-' && nextNonWs == '-'))
                            needSpace = true;
                    }
                    if (needSpace) { sb.Append(' '); prevOutNonWs = ' '; }
                    i = j; continue;
                }

                // 其他符号/标识符/数字：直接输出，并更新prev标识（不注入标记，避免被浏览器单独换行显示）
                sb.Append(c);
                if (IsIdentPart(c))
                {
                    prevWasIdentOrNumber = true;
                    prevOutNonWs = c;
                }
                else if (IsDecDigit(c))
                {
                    prevWasIdentOrNumber = true;
                    prevOutNonWs = c;
                }
                else
                {
                    prevWasIdentOrNumber = false;
                    prevOutNonWs = c;
                }
                i++;
            }

            return sb.ToString();
        }
        public static byte[] LoadBinaryStream( System.IO.Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }
            if (stream is System.IO.MemoryStream)
            {
                return ((System.IO.MemoryStream)stream).ToArray();
            }
            else
            {
                var temp = new System.Collections.Generic.List<byte>();
                var bs = new byte[1024];
                while( true )
                {
                    int len = stream.Read(bs, 0, bs.Length);
                    if(len > 0)
                    {
                        temp.AddRange(bs.AsSpan<byte>(0, len));
                    }
                    else
                    {
                        break;
                    }
                }
                var result = temp.ToArray();
                temp.Clear();
                return result;
            }
        }
        public static byte[] LoadBinaryResource(System.Reflection.Assembly asm , string resourceName , bool throwException )
        {
            if (asm == null)
            {
                asm = System.Reflection.Assembly.GetExecutingAssembly();
            }
            var stream = asm.GetManifestResourceStream(resourceName);
            if (stream == null)
            {
                var names = asm.GetManifestResourceNames();
                foreach (var name in names)
                {
                    if (name.EndsWith(resourceName))
                    {
                        stream = asm.GetManifestResourceStream(name);
                        break;
                    }
                }
            }
            if (stream == null)
            {
                if (throwException)
                {
                    throw new System.IO.FileNotFoundException("Resource not found: " + resourceName);
                }
                else
                {
                    return null;
                }
            }
            else
            {
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                stream.Close();
                return buffer;
            }
        }

        internal static bool IsLowSurrogate(int ch)
        {
            return ch >= 56320 && ch <= 57343;
            //return InRange(ch, 56320, 57343);
        }

        internal static bool IsHighSurrogate(int ch)
        {
            return ch >= 55296 && ch <= 56319;
            //return InRange(ch, 55296, 56319);
        }
        /// <summary>
        /// 判断字符串是否包含单个字符，如果有UNICODE代理，则2个字符编码也认为是单个字符
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        public static bool IsSingleCharCheckHighSurrogate(string txt)
        {
            return txt.Length == 1 || (txt.Length == 2 && IsHighSurrogate(txt[0]));
        }
    }
}
