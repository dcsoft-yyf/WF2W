using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.Xml.Serialization;
using DCSoft.Common;

// 袁永福到此一游，对StringFormat的封装是为JAVA版本做准备。

namespace DCSoft.Drawing
{
    /// <summary>
    /// 绘制字符串的格式化信息对象。是System.Drawing.StringFormat的一个封装.
    /// </summary>
    //[System.Reflection.Obfuscation( Exclude= true , ApplyToMembers = true )]
    [Serializable]
    [System.Runtime.InteropServices.ComVisible(false)]
    public partial class DrawStringFormatExt : IDisposable
    {
        private static DrawStringFormatExt _GenericDefault = null;
        /// <summary>
        /// 通用格式对象
        /// </summary>
        public static DrawStringFormatExt GenericDefault
        {
            get
            {
                if (_GenericDefault == null)
                {
                    _GenericDefault = new DrawStringFormatExt();
                    _GenericDefault.SetStringFormatAsGenericDefault();
                    _GenericDefault._Readonly = true;
                }
                return _GenericDefault; 
            }
        }

        private static DrawStringFormatExt _GenericTypographic = null;
        /// <summary>
        /// 通用格式对象
        /// </summary>
        public static DrawStringFormatExt GenericTypographic
        {
            get
            {
                if (_GenericTypographic == null)
                {
                    _GenericTypographic = new DrawStringFormatExt();
                    _GenericTypographic.SetStringFormatAsGenericTypographic();
                    _GenericTypographic._Readonly = true;
                }
                return _GenericTypographic;
            }
        }

        /// <summary>
        /// 初始化对象
        /// </summary>
        public DrawStringFormatExt(DrawStringFormatExt baseFormat )
        {
            if (baseFormat != null)
            {
                this._Alignment = baseFormat._Alignment;
                this._Color = baseFormat._Color;
                if (baseFormat._Font != null)
                {
                    this._Font = baseFormat._Font.Clone();
                }
                this._FormatFlags = baseFormat._FormatFlags;
                this._FormatModified = baseFormat._FormatModified;
                this._FormatType = baseFormat._FormatType;
                this._Height = baseFormat._Height;
                this._Left = baseFormat._Left;
                this._LineAlignment = baseFormat._LineAlignment;
                this._Top = baseFormat._Top;
                this._Trimming = baseFormat._Trimming;
                this._Value = null;
                this._Width = baseFormat._Width;
            }
        }

        /// <summary>
        /// 初始化对象
        /// </summary>
        public DrawStringFormatExt()
        {
        }

        /// <summary>
        /// 内容只读
        /// </summary>
        private bool _Readonly = false;

        public DrawStringFormatExt Clone()
        {
            DrawStringFormatExt f =  (DrawStringFormatExt)this.MemberwiseClone();
            if (this.Font != null)
            {
                f.Font = this.Font.Clone();
            }
            f._Value = null;
            f._Readonly = false;
            return f;
        }

#if ! DCWriterForWASM
        
        public void SetStringFormatAlignment(ContentAlignment alignment)
        {
            if (this._Readonly)
            {
                return;
            }
            switch (alignment)
            {
                case ContentAlignment.BottomCenter:
                    this.Alignment = StringAlignment.Center;
                    this.LineAlignment = StringAlignment.Far;
                    break;
                case ContentAlignment.BottomLeft:
                    this.Alignment = StringAlignment.Near;
                    this.LineAlignment = StringAlignment.Far;
                    break;
                case ContentAlignment.BottomRight:
                    this.Alignment = StringAlignment.Far;
                    this.LineAlignment = StringAlignment.Far;
                    break;
                case ContentAlignment.MiddleCenter:
                    this.Alignment = StringAlignment.Center;
                    this.LineAlignment = StringAlignment.Center;
                    break;
                case ContentAlignment.MiddleLeft:
                    this.Alignment = StringAlignment.Near;
                    this.LineAlignment = StringAlignment.Center;
                    break;
                case ContentAlignment.MiddleRight:
                    this.Alignment = StringAlignment.Far;
                    this.LineAlignment = StringAlignment.Center;
                    break;
                case ContentAlignment.TopCenter:
                    this.Alignment = StringAlignment.Center;
                    this.LineAlignment = StringAlignment.Near;
                    break;
                case ContentAlignment.TopLeft:
                    this.Alignment = StringAlignment.Near;
                    this.LineAlignment = StringAlignment.Near;
                    break;
                case ContentAlignment.TopRight:
                    this.Alignment = StringAlignment.Far;
                    this.LineAlignment = StringAlignment.Near;
                    break;
            }
        }
        /// <summary>
        /// 设置字符串格式为多行文本
        /// </summary>
        /// <param name="multiLine">是否多行文本</param>
        public void SetMultiLine( bool multiLine)
        {
            if (this._Readonly)
            {
                return;
            }

            if (multiLine)
            {
                this.FormatFlags = this.FormatFlags & ~StringFormatFlags.NoWrap;
            }
            else
            {
                this.FormatFlags = this.FormatFlags | StringFormatFlags.NoWrap;
            }
        }
#endif
        

        private bool _UseAdvancedDirectionVertical = false;
        /// <summary>
        /// 获取或设置是否使用高级竖行文本模式
        /// </summary>
        /// <remarks>
        /// 该扩展标记试图影响MeasureString函数和DrawString函数，使其在处理竖行排列文本时，
        /// 将字符串中的单个数字或单个英文字符全部采用正常排版模式，避免其在竖向排列时“倒下来”，
        /// 不处理连续的数字或连续的英文字符串
        /// </remarks>
        public bool UseAdvancedDirectionVertical
        {
            get
            {
                return _UseAdvancedDirectionVertical;
            }
            set
            {
                _UseAdvancedDirectionVertical = value; 
            }
        }

        private bool _UseAdvancedDirectionVertical2 = false;
        /// <summary>
        /// 获取或设置是否使用特殊的高级竖行文本模式
        /// </summary>
        /// <remarks>
        /// 在UseAdvancedDirectionVertical的基础上，处理 HH:mm:ss 格式的字符串时，
        /// 将HH/mm/ss的两个数字字符并列”（云南达远的新需求wyc20220406）
        /// </remarks>
        public bool UseAdvancedDirectionVertical2
        {
            get
            {
                return _UseAdvancedDirectionVertical2;
            }
            set
            {
                _UseAdvancedDirectionVertical2 = value;
            }
        }


        private StringFormatType _FormatType = StringFormatType.Normal;
        private enum StringFormatType
        {
            Normal ,
            GenericDefault,
            GenericTypographic
        }

        private bool _FormatModified = false;
        private void SetFormatModified()
        {
            if (this._Value != null)
            {
                this._Value.Dispose();
                this._Value = null;
            }
            this._FormatModified = true;
        }
        /// <summary>
        /// 设置通用排版格式
        /// </summary>
        public void SetStringFormatAsGenericDefault()
        {
            SetStringFormat(StringFormat.GenericDefault);
            this._FormatType = StringFormatType.GenericDefault;
            ClearValue();
            this._FormatModified = false;
        }

        /// <summary>
        /// 设置通用排版格式
        /// </summary>
        public void SetStringFormatAsGenericTypographic()
        {
            SetStringFormat(StringFormat.GenericTypographic);
            this._FormatType = StringFormatType.GenericTypographic;
            ClearValue();
            this._FormatModified = false;
        }
        /// <summary>
        /// 创建字符串格式化对象
        /// </summary>
        /// <returns></returns>
        public StringFormat CreateStringFormat()
        {
            StringFormat result = null;
            if (this._FormatType == StringFormatType.GenericTypographic)
            {
                result = new StringFormat(StringFormat.GenericTypographic);
                if (this._FormatModified == false)
                {
                    return result;
                }
            }
            else if (this._FormatType == StringFormatType.GenericDefault)
            {
                result = new StringFormat(StringFormat.GenericDefault);
                if (this._FormatModified == false)
                {
                    return result;
                }
            }
            else
            {
                result = new StringFormat();
            }
            result.Alignment = this.Alignment;
            result.LineAlignment = this.LineAlignment;
            result.FormatFlags = this.FormatFlags;
            result.Trimming = this.Trimming;
            return result;
        }

        [NonSerialized]
        private StringFormat _Value = null;
        /// <summary>
        /// 内部的对象值
        /// </summary>
        [Browsable( false )]
        [XmlIgnore]
        public StringFormat Value
        {
            get
            {
                if (this._FormatModified == false)
                {
                    if (this._FormatType == StringFormatType.GenericDefault)
                    {
                        return StringFormat.GenericDefault;
                    }
                    else if (this._FormatType == StringFormatType.GenericTypographic)
                    {
                        return StringFormat.GenericTypographic;
                    }
                }
                if (_Value == null)
                {
                    _Value = CreateStringFormat();
                }
                return _Value;
            }
        }
        public DCStringFormat CreateDCStringFormat()
        {
            if( this._FormatType == StringFormatType.GenericDefault)
            {
                return DCStringFormat.GenericDefault;
            }
            else if( this._FormatType == StringFormatType.GenericTypographic)
            {
                return DCStringFormat.GenericTypographic;
            }
            else
            {
                var result = new DCStringFormat();
                result.Alignment = this._Alignment;
                result.LineAlignment = this._LineAlignment;
                result.FormatFlags = this._FormatFlags;
                result.Trimming = this._Trimming;
                return result;
            }
        }
        /// <summary>
        /// 设置格式化对象
        /// </summary>
        /// <param name="f">格式化对象</param>
        public void SetStringFormat(StringFormat f)
        {
            if (this._Readonly)
            {
                return;
            }
            if (f != null)
            {
                this._Alignment = f.Alignment;
                this._LineAlignment = f.LineAlignment;
                this._Trimming = f.Trimming;
                this._FormatFlags = f.FormatFlags;
                SetFormatModified();
            }
        }

        private Color _Color = System.Drawing.Color.Black;
        /// <summary>
        /// 文本颜色
        /// </summary>
        [DefaultColorValue("Black")]
        public Color Color
        {
            get 
            {
                return _Color; 
            }
            set
            {
                if (this._Readonly)
                {
                    return;
                }
                _Color = value;
            }
        }

        private Color _TextBackColor = Color.Empty;
        /// <summary>
        /// 文本背景色
        /// </summary>
        [DefaultColorValue()]
        [XmlIgnore]
        public Color TextBackColor
        {
            get 
            {
                return _TextBackColor; 
            }
            set
            {
                _TextBackColor = value; 
            }
        }

        private XFontValue _Font = null;
        /// <summary>
        /// 字体设置
        /// </summary>
        [DefaultValue( null )]
        [XmlElement]
        public XFontValue Font
        {
            get
            {
                return _Font; 
            }
            set
            {
                if (this._Readonly)
                {
                    return;
                }
                _Font = value; 
            }
        }

        private StringAlignment _Alignment = StringAlignment.Near;
        /// <summary>
        /// 水平对齐方式
        /// </summary>
        [DefaultValue(StringAlignment.Near)]
        [XmlAttribute]
        public StringAlignment Alignment
        {
            get
            {
                return _Alignment;
            }
            set
            {
                if (this._Readonly)
                {
                    return;
                }
                _Alignment = value;
                SetFormatModified();
            }
        }

        private StringAlignment _LineAlignment = StringAlignment.Near;
        /// <summary>
        /// 垂直对齐方式
        /// </summary>
        [DefaultValue(StringAlignment.Near)]
        [XmlAttribute]
        public StringAlignment LineAlignment
        {
            get
            {
                return _LineAlignment;
            }
            set
            {
                if (this._Readonly)
                {
                    return;
                }
                _LineAlignment = value;
                SetFormatModified();
            }
        }

        private StringFormatFlags _FormatFlags = StringFormatFlags.NoWrap;
        /// <summary>
        /// 格式化标记
        /// </summary>
        [DefaultValue(StringFormatFlags.NoWrap)]
        [XmlAttribute]
        public StringFormatFlags FormatFlags
        {
            get
            {
                return _FormatFlags;
            }
            set
            {
                if (this._Readonly)
                {
                    return;
                }
                _FormatFlags = value;
                SetFormatModified();
            }
        }

        private StringTrimming _Trimming = StringTrimming.None;
        /// <summary>
        /// 设置字符修整模式
        /// </summary>
        [DefaultValue(StringTrimming.None)]
        [XmlAttribute]
        public StringTrimming Trimming
        {
            get
            {
                return _Trimming;
            }
            set
            {
                if (this._Readonly)
                {
                    return;
                }
                _Trimming = value;
                SetFormatModified();
            }
        }

        private float _Left = 0;
        /// <summary>
        /// 文本布局矩形位置
        /// </summary>
        public float Left
        {
            get 
            {
                return _Left; 
            }
            set
            {
                if (this._Readonly)
                {
                    return;
                }
                _Left = value;
            }
        }

        private float _Top = 0;

        /// <summary>
        /// 文本布局矩形位置
        /// </summary>
        public float Top
        {
            get 
            {
                return _Top; 
            }
            set
            {
                if (this._Readonly)
                {
                    return;
                }
                _Top = value;
            }
        }

        private float _Width = 0;

        /// <summary>
        /// 文本布局矩形位置
        /// </summary>
        public float Width
        {
            get 
            {
                return _Width; 
            }
            set
            {
                if (this._Readonly)
                {
                    return;
                }
                _Width = value;
            }
        }

        private float _Height = 0;

        /// <summary>
        /// 文本布局矩形位置
        /// </summary>
        public float Height
        {
            get 
            {
                return _Height; 
            }
            set
            {
                if (this._Readonly)
                {
                    return;
                }
                _Height = value;
            }
        }


#if ! DCWriterForWASM
        public void SetBounds(RectangleF bounds)
        {
            if (this._Readonly)
            {
                return;
            }
            this._Left = bounds.Left;
            this._Top = bounds.Top;
            this._Width = bounds.Width;
            this._Height = bounds.Height;
        }
#endif
        


        public void Dispose()
        {
            ClearValue();
        }
        private void ClearValue()
        {
            if (this._Value != null)
            {
                this._Value.Dispose();
                this._Value = null;
            }
        }
    }
}
