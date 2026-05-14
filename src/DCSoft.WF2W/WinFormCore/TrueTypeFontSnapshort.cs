using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;

namespace DCSoft
{

    internal sealed partial class TrueTypeFontSnapshort
    {

        static TrueTypeFontSnapshort()
        {
        }

        /// <summary>
        /// 是否支持指定名称的字体
        /// </summary>
        /// <param name="strFontName">字体名称</param>
        /// <returns>是否支持</returns>
        public static bool Support(string strFontName)
        {
            if (strFontName != null && strFontName.Length > 0)
            {
                return _FontNames.Contains(strFontName);
                //for (var iCount = _GlobalParents.Length - 1; iCount >= 0; iCount--)
                //{
                //    if (_GlobalParents[iCount].FontName == strFontName)
                //    {
                //        return true;
                //    }
                //}
            }
            return false;
        }

        private static System.Collections.Generic.HashSet<string> _FontNames = new HashSet<string>();
        public static void SetGlobalParentNames(string[] names )
        {
            if(names != null && names.Length > 0)
            {
                _FontNames = new HashSet<string>(names);
            }
        }

        private static System.Collections.Generic.Dictionary<string, string> _SupportFontNames = null;
        public static string FixFontName( string fontName )
        {
            if (fontName != null && fontName.Length > 0)
            {
                if (_SupportFontNames == null)
                {
                    return fontName;
                }
                if (_SupportFontNames.ContainsKey(fontName))
                {
                    return fontName;
                }
            }
            return SystemFonts._DefaultFontName;// System.Windows.Forms.Control.DefaultFont.Name;
        }



        public static readonly TrueTypeFontSnapshort Empty = new TrueTypeFontSnapshort();

        private class InfoKey
        {
            public InfoKey(string vFontName, FontStyle vStyle, GraphicsUnit vUnit)
            {
                this.FontName = vFontName;
                this.Style = vStyle;
                this.Unit = vUnit;
                this._HashCode = vFontName.GetHashCode() + 10 * (int)this.Style + (int)this.Unit;
            }
            private readonly string FontName;
            private readonly FontStyle Style;
            private readonly GraphicsUnit Unit;
            private readonly int _HashCode;
            public override int GetHashCode()
            {
                return this._HashCode;
            }
            public override bool Equals(object obj)
            {
                if (this == obj)
                {
                    return true;
                }
                var k2 = (InfoKey)obj;
                return this.FontName == k2.FontName
                    && this.Style == k2.Style
                    && this.Unit == k2.Unit;
            }
        }

        //public delegate TrueTypeFontSnapshort GetInstanceHandler(string strFontName, FontStyle style);

        //public static GetInstanceHandler EventGetInstance = null;

        //private static TrueTypeFontSnapshort _LastInstance = null;
        //public static TrueTypeFontSnapshort GetInstance(string fontName, FontStyle style, GraphicsUnit unit = GraphicsUnit.Point)
        //{
        //    if( _LastInstance != null && _LastInstance.FontName == fontName && _LastInstance.Style == style )
        //    {
        //        return _LastInstance;
        //    }
        //    if (EventGetInstance == null)
        //    {
        //        throw new NotSupportedException("EventGetInstance");
        //    }
        //    var info = EventGetInstance(fontName, style);
        //    _LastInstance = info;
        //    return info;
        //}
        /// <summary>
        /// 全局的父对象
        /// </summary>
        private static TrueTypeFontSnapshort[] _GlobalParents = null;

        private TrueTypeFontSnapshort()
        {

        }
        public static TrueTypeFontSnapshort Create(string strName , byte[] bsData )
        {
            var reader = new FastBinaryReader(bsData, Encoding.UTF8);
            var newItem = new TrueTypeFontSnapshort(reader);
            reader.Close();
            //newItem._Loaded = true;
            newItem.FontName = strName;
            Console.WriteLine("加载字体信息:" + newItem.FontName + " , " + newItem.Style.ToString());
            return newItem;
        }
         




        internal string FileName = null;
        internal float BoldZoomRate = 1;
        ///// <summary>
        ///// 数据是否加载完毕
        ///// </summary>
        //private bool _Loaded = false;

        private readonly bool _NeedFixWidthForBold = false;
        /// <summary>
        /// 是否需要针对粗体而修正字符宽度
        /// </summary>
        /// <param name="style"></param>
        /// <returns></returns>
        public bool NeedFixWidthForBold(FontStyle style)
        {
            //var str = this.Style.ToString() + " " + this.FontName;
            return ((style & FontStyle.Bold) == FontStyle.Bold) && this._NeedFixWidthForBold;
            //return this.Style != style
            //    && ((style & FontStyle.Bold) == FontStyle.Bold)
            //    && ((this.Style & FontStyle.Bold) != FontStyle.Bold);
        }


        private class FastBinaryReader
        {
#if DCSoftWASMTest
            public FastBinaryReader(System.IO.Stream stream, System.Text.Encoding encoding)
            {
                var stream2 = new System.IO.Compression.GZipStream(stream, System.IO.Compression.CompressionMode.Decompress);
                this._Data = DCSoft.Common.FileHelper.LoadBinaryStream(stream2);// new byte[stream.Length];
                stream2.Close();
                //stream.Read(this._Data, 0, this._Data.Length);
                this._Encoding = encoding;
                //this._DataLength = this._Data.Length;
            }
#endif
            public FastBinaryReader(byte[] data , System.Text.Encoding encoding)
            {
                this._Data = data;
                this._Encoding = encoding;
            }

            public void Close()
            {
                this._Data = null;
                this._Encoding = null;
            }
            //private System.Text.Decoder _Decoder = System.Text.Encoding.UTF8.GetDecoder();
            private System.Text.Encoding _Encoding = null;

            private byte[] _Data = null;
            private int _Position = 0;
            //private int _DataLength = 0;
            public short ReadInt16()
            {
                return (short)( this._Data[this._Position++] | (this._Data[this._Position++] << 8 ));
            }
            public short[] ReadInt16Array(int len )
            {
                var result = new short[len];
                if( BitConverter.IsLittleEndian)
                {
                    Buffer.BlockCopy(this._Data, this._Position, result, 0, len*2);
                    this._Position += len * 2;
                }
                else
                {
                    for(var iCount = 0;iCount < len;iCount++)
                    {
                        result[iCount] = (short)(this._Data[this._Position++] | (this._Data[this._Position++] << 8));
                    }
                }
                return result;
            }
            public int ReadInt32()
            {
                return this._Data[this._Position++]
                    | (this._Data[this._Position++] << 8)
                    | (this._Data[this._Position++] << 16)
                    | (this._Data[this._Position++] << 24);
            }
            public unsafe int[] ReadUInt16Array(int len)
            {
                var result = new int[len];
                fixed (byte* pStart = &this._Data[this._Position])
                {
                    fixed (int* pResult = result)
                    {
                        var p2 = pResult;
                        if (BitConverter.IsLittleEndian)
                        {
                            ushort* p = (ushort*)pStart;
                            ushort* pEnd = p + len;
                            while( p < pEnd)
                            {
                                *p2++ = *p++;
                            }
                            //for (var iCount = 0; iCount < len; iCount++)
                            //{
                            //    *p2++ = *p++;
                            //}
                        }
                        else
                        {
                            byte* p = pStart;
                            for (var iCount = 0; iCount < len; iCount++)
                            {
                                *p2++ = ((*p++) | ((*p++) << 8));
                            }
                        }
                    }
                }
                this._Position += len * 2;
                return result;
            }
            public byte ReadByte()
            {
                return this._Data[this._Position++];
            }
            public string ReadString()
            {
                var len = Read7BitEncodedInt();
                if(len == 0 )
                {
                    return String.Empty;
                }
                var strResult = this._Encoding.GetString(this._Data, this._Position, len);
                this._Position += len;
                return strResult;
            }
            public float ReadSingle()
            {
                float result = BitConverter.ToSingle(this._Data, this._Position);
                this._Position += 4;
                return result;
            }
            private int Read7BitEncodedInt()
            {
                // Read out an Int32 7 bits at a time.  The high bit
                // of the byte when on means to continue reading more bytes.
                int count = 0;
                int shift = 0;
                byte b;
                do
                {
                    // Check for a corrupted stream.  Read a max of 5 bytes.
                    // In a future version, add a DataFormatException.
                    if (shift == 5 * 7)  // 5 bytes max per Int32, shift += 7
                        throw new FormatException("Format_Bad7BitInt32");

                    // ReadByte handles end of stream cases for us.
                    b = ReadByte();
                    count |= (b & 0x7F) << shift;
                    shift += 7;
                } while ((b & 0x80) != 0);
                return count;
            }

        }
        private TrueTypeFontSnapshort(FastBinaryReader reader)
        {
            if (reader == null)
            {
                throw new ArgumentNullException("reader");
            }
            //this.FontName = vFontName;
            //this.Style = vStyle;
            //this.Unit = vunit;
            //this._UnitConvertRate = new float[7];
            //var rate1 = GetUnit(this.Unit);
            //this._UnitConvertRate[(int)GraphicsUnit.Display] = (float)(rate1 / GetUnit(GraphicsUnit.Display));
            //this._UnitConvertRate[(int)GraphicsUnit.Document] = (float)(rate1 / GetUnit(GraphicsUnit.Document));
            //this._UnitConvertRate[(int)GraphicsUnit.Inch] = (float)(rate1 / GetUnit(GraphicsUnit.Inch));
            //this._UnitConvertRate[(int)GraphicsUnit.Millimeter] = (float)(rate1 / GetUnit(GraphicsUnit.Millimeter));
            //this._UnitConvertRate[(int)GraphicsUnit.Pixel] = (float)(rate1 / GetUnit(GraphicsUnit.Pixel));
            //this._UnitConvertRate[(int)GraphicsUnit.Point] = (float)(rate1 / GetUnit(GraphicsUnit.Point));

            int len = reader.ReadInt16();
            if (len > 0)
            {
                this.CharRanges = reader.ReadUInt16Array(len);
                //this.CharRanges = new int[len];
                //for (int iCount = 0; iCount < len; iCount++)
                //{
                //    this.CharRanges[iCount] = reader.ReadUInt16();
                //}
                this.CharWidths = reader.ReadInt16Array(len / 2);
                //len = len / 2;
                //this.CharWidths = new short[len ];
                //for (int iCount = 0; iCount < len ; iCount++)
                //{
                //    this.CharWidths[iCount] = reader.ReadInt16();
                //}
            }
            this.FontName = reader.ReadString();
            this.LineSpacing = reader.ReadSingle();
            this.MaxCharCode = (char)reader.ReadInt32();
            this.Style = (FontStyle)reader.ReadByte();
            this.Unit = (GraphicsUnit)reader.ReadByte();
            this.Unit = GraphicsUnit.Point;
            this.UnitsPerEm = reader.ReadSingle();
            this.Ascent = reader.ReadSingle();
            this.Descent = reader.ReadSingle();

            this._UnitConvertRate = BuildUnitConvertRate();
            //this._Loaded = true;
            var index2 = IndexOfRanges(this.CharRanges, '袁');
            if (index2 >= 0)
            {
                // 支持汉字
                this._SupportChinese = true;
                this._ChineseWidth = this.CharWidths[index2];
            }
            else
            {
                // 不支持汉字，在采用H字母的两倍当做汉字宽度
                this._SupportChinese = false;
                index2 = IndexOfRanges(this.CharRanges, 'H');
                if (index2 >= 0)
                {
                    this._ChineseWidth = this.CharWidths[index2] * 2;
                }
            }
            //this._UnitConvertRate = new float[7];
            //for (int iCount = 0; iCount < 7; iCount++)
            //{
            //    this._UnitConvertRate[iCount] = reader.ReadSingle();
            //}
            //reader.Close();
        }
        private readonly float _ChineseWidth = 0;
        public float GetChineseWidth(float fontSize, GraphicsUnit unit)
        {
            return fontSize * this._ChineseWidth * this._UnitConvertRate[(int)unit] / this.UnitsPerEm;
        }
        private float[] BuildUnitConvertRate()
        {
            var rate1 = GetUnit(this.Unit);
            var result = new float[7];
            result[(int)GraphicsUnit.Display] = (float)(rate1 / GetUnit(GraphicsUnit.Display));
            result[(int)GraphicsUnit.Document] = (float)(rate1 / GetUnit(GraphicsUnit.Document));
            result[(int)GraphicsUnit.Inch] = (float)(rate1 / GetUnit(GraphicsUnit.Inch));
            result[(int)GraphicsUnit.Millimeter] = (float)(rate1 / GetUnit(GraphicsUnit.Millimeter));
            result[(int)GraphicsUnit.Pixel] = (float)(rate1 / GetUnit(GraphicsUnit.Pixel));
            result[(int)GraphicsUnit.Point] = (float)(rate1 / GetUnit(GraphicsUnit.Point));
            return result;
        }
        public float GetFontHeight(float fontSize, GraphicsUnit unit)
        {
            if (this == Empty)
            {
                return 0;
            }
            return (float)(this.LineSpacing * fontSize * this._UnitConvertRate[(int)unit]);
        }
        private readonly float[] _UnitConvertRate = null;
        [ThreadStatic]
        private static bool _NotSupportCharForLastGetCharWidth = false;

        private readonly bool _SupportChinese = false;

        public bool NotSupportCharForLastGetCharWidth
        {
            get { return _NotSupportCharForLastGetCharWidth; }
        }
        public SizeF GetCharSize(char c, float fontSize, GraphicsUnit unit)
        {
            if (this == Empty)
            {
                _NotSupportCharForLastGetCharWidth = true;
                return SizeF.Empty;
            }
            var index = IndexOfRanges(this.CharRanges, c);
            if (index < 0 && this._SupportChinese && c > 9000)
            {
                // 当做汉字来处理
                index = IndexOfRanges(this.CharRanges, '袁');
            }
            if (index >= 0)
            {
                _NotSupportCharForLastGetCharWidth = false;
                var rate = this._UnitConvertRate[(int)unit] * fontSize;
                return new SizeF(
                    (float)(this.CharWidths[index] * rate / this.UnitsPerEm),
                    (float)(this.LineSpacing * rate));
            }
            _NotSupportCharForLastGetCharWidth = true;
            return SizeF.Empty;
        }

        public float GetCharWidth(char c, float fontSize, GraphicsUnit unit)
        {
            if (this == Empty)
            {
                _NotSupportCharForLastGetCharWidth = true;
                return 0;
            }
            int index = IndexOfRanges(this.CharRanges, c);
            if ( index < 0 && this._SupportChinese && c > 9000)
            {
                // 当做汉字来处理
                index = IndexOfRanges(this.CharRanges, '袁');
            }
            if (index >= 0)
            {
                _NotSupportCharForLastGetCharWidth = false;
                float w = (float)(fontSize * this.CharWidths[index] * this._UnitConvertRate[(int)unit] / this.UnitsPerEm);
                return w;
            }
            _NotSupportCharForLastGetCharWidth = true;
            return 0;
        }
        /// <summary>
        /// 判断指定字体是否支持全部的字符
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        public bool SupportAllChars(string txt)
        {
            if (string.IsNullOrEmpty( txt ))
            {
                return true;
            }
            if (this == Empty)
            {
                return false;
            }
            for (var iCount = txt.Length - 1; iCount >= 0; iCount--)
            {
                if (IndexOfRanges(this.CharRanges, txt[iCount]) < 0)
                {
                    return false;
                }
            }
            return true;
        }
        public bool SupportChar( char c )
        {
            return this != Empty && IndexOfRanges(this.CharRanges, c) >= 0;
        }

        private static readonly double fDpi = 96;

        /// <summary>
		/// 获得一个单位占据的英寸数
		/// </summary>
		/// <param name="unit">单位类型</param>
		/// <returns>英寸数</returns>
		private static float GetUnit( GraphicsUnit unit)
        {
            switch (unit)
            {
                case  GraphicsUnit.Display:
                    return 1 / (float)fDpi;
                case  GraphicsUnit.Document:
                    return 1 / 300.0f;
                case  GraphicsUnit.Inch:
                    return 1;
                case  GraphicsUnit.Millimeter:
                    return 1 / 25.4f;
                case  GraphicsUnit.Pixel:
                    return 1 / (float)fDpi;
                case  GraphicsUnit.Point:
                    return 1 / 72.0f;
                default:
                    throw new System.NotSupportedException("Not support " + unit.ToString());
            }
        }

        ///// <summary>
        ///// 判断是否支持指定的字符
        ///// </summary>
        ///// <param name="c">字符值</param>
        ///// <returns>是否支持</returns>
        //public bool IsSupportChar(char c)
        //{
        //    if (this == Empty)
        //    {
        //        return false;
        //    }
        //    if (c <= this.MaxCharCode)
        //    {
        //        return IndexOfRanges(this.CharRanges, c) >= 0;
        //    }
        //    return false;
        //}

        internal  int[] CharRanges = null;

        private readonly char MaxCharCode = (char)0;
        //public readonly char MinCharCode = char.MinValue;
        public string NativeJSName = null;
         
        public string FontName = null;
        public readonly FontStyle Style = FontStyle.Regular;
        public bool Bold
        {
            get
            {
                return (this.Style & FontStyle.Bold) == FontStyle.Bold;
            }
        }
        public bool Italic
        {
            get
            {
                return (this.Style & FontStyle.Italic) == FontStyle.Italic;
            }
        }
        private readonly GraphicsUnit Unit = GraphicsUnit.Point;

        private  short[] CharWidths = null;

        private readonly float UnitsPerEm = 0;

        public  float LineSpacing = 0;
        public readonly float Ascent = 0;
        public readonly float Descent = 0;
        private static int IndexOfRanges(int[] ranges, int value)
        {
            if (ranges != null && ranges.Length > 0 && (ranges.Length % 2 == 0))
            {
                int startIndex = 0;
                int endIndex = ranges.Length >> 1;
                while (endIndex > startIndex)
                {
                    int middleIndex = (startIndex + endIndex) >> 1;
                    int pos = middleIndex << 1;
                    if (middleIndex == startIndex)
                    {
                        if (value >= ranges[pos] && value <= ranges[pos + 1])
                        {
                            // 命中范围
                            return middleIndex;
                        }
                        break;
                    }
                    if (value < ranges[pos])
                    {
                        endIndex = middleIndex;
                    }
                    else if (value <= ranges[pos + 1])
                    {
                        // 命中范围
                        return middleIndex;
                    }
                    else
                    {
                        startIndex = middleIndex;
                    }

                }
            }
            return -1;
        }
        /// <summary>
        /// 获得字符宽度分组信息对象
        /// </summary>
        /// <returns></returns>
        public int[] GetCharWidthRanges()
        {
            return this.CharRanges;
        }
    }

}
