using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Text;

namespace DCSoft.Drawing
{
    /// <summary>
    /// System.Drawing.StringFormat的包装类型，用于尽量减少用户代码对System.Drawing.StringFormat的直接引用，为跨平台打基础。
    /// </summary>
    [System.Runtime.InteropServices.ComVisible(false)]
    public class DCStringFormat : IDisposable
    {
        static DCStringFormat()
        {
            GenericDefault = new DCStringFormat();
            GenericDefault._Alignment = StringAlignment.Near;
            GenericDefault._FormatFlags = (StringFormatFlags)0;
            GenericDefault._HotkeyPrefix = HotkeyPrefix.None;
            GenericDefault._LineAlignment = StringAlignment.Near;
            GenericDefault._Trimming = StringTrimming.Character;
            GenericDefault._Readonly = true;
            GenericDefault._Source = 1;

            GenericTypographic = new DCStringFormat();
            GenericTypographic._Alignment = StringAlignment.Near;
            GenericTypographic._FormatFlags = StringFormatFlags.FitBlackBox | StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
            GenericTypographic._HotkeyPrefix = HotkeyPrefix.None;
            GenericTypographic._LineAlignment = StringAlignment.Near;
            GenericTypographic._Trimming = StringTrimming.None;
            GenericTypographic._Readonly = true;
            GenericTypographic._Source = 2;
        }
        public static readonly DCStringFormat GenericDefault;
        public static readonly DCStringFormat GenericTypographic;

        public static StringFormat NativeGenericDefault
        {
            get
            {
                return StringFormat.GenericDefault;
            }
        }
        public static StringFormat NativeGenericTypographic
        {
            get
            {
                return StringFormat.GenericTypographic;
            }
        }
        public static StringFormat GenericDefaultValue()
        {
            return StringFormat.GenericDefault;
        }
#if !DCWriterForWASM

        public static StringFormat GenericTypographicValue()
        {
            return StringFormat.GenericTypographic;
        }

        public DCStringFormat(StringFormat f)
        {
            this._Alignment = f.Alignment;
            this._LineAlignment =f.LineAlignment;
            this._FormatFlags = f.FormatFlags;
            this._Trimming = f.Trimming;
            this._HotkeyPrefix = f.HotkeyPrefix;
            //this._DigitSubstitutionMethod = f.DigitSubstitutionMethod;
            //this._DigitSubstitutionLanguage = f.DigitSubstitutionLanguage;
        }
#endif

        public DCStringFormat()
        {

        }

        public DCStringFormat( DCStringFormat f)
        {
            if( f == null )
            {
                throw new ArgumentNullException("f");
            }
            this._Alignment = f._Alignment;
            this._LineAlignment = f._LineAlignment;
            this._FormatFlags = f._FormatFlags;
            this._Trimming = f._Trimming;
            this._HotkeyPrefix = f._HotkeyPrefix;
            //this._DigitSubstitutionLanguage = f._DigitSubstitutionLanguage;
            //this._DigitSubstitutionMethod = f._DigitSubstitutionMethod;
            this._Source = f._Source;
        }
#if !DCWriterForWASM
  
        public bool AllowMultiLine
        {
            get
            {
                return (this._FormatFlags & StringFormatFlags.NoWrap) != StringFormatFlags.NoWrap;
            }
        }

#endif
        private int _Source  ;

        private bool _Readonly  ;
        /// <summary>
        /// 设置字符串格式为多行文本
        /// </summary>
        /// <param name="multiLine">是否多行文本</param>
        public void SetMultiLine(bool multiLine)
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
        private StringAlignment _Alignment = StringAlignment.Near;
        /// <summary>
        /// 文本水平对齐方式
        /// </summary>
        public StringAlignment Alignment
        {
            get
            {
                return _Alignment;
            }
            set
            {
                if (_Readonly == false)
                {
                    if (this._Alignment != value)
                    {
                        _Alignment = value;
                        DisposeValue();
                    }
                }
                else
                {
                    throw new InvalidOperationException("readonly");
                }
            }
        }

        private StringAlignment _LineAlignment = StringAlignment.Near;
        /// <summary>
        /// 文本垂直对齐方式
        /// </summary>
        public StringAlignment LineAlignment
        {
            get
            {
                return _LineAlignment;
            }
            set
            {
                if (_Readonly == false)
                {
                    if (this._LineAlignment != value)
                    {
                        _LineAlignment = value;
                        DisposeValue();
                    }
                }
                else
                {
                    throw new InvalidOperationException("readonly");
                }
            }
        }


        private StringFormatFlags _FormatFlags = (StringFormatFlags)0;
        /// <summary>
        /// 格式化标记
        /// </summary>
        public StringFormatFlags FormatFlags
        {
            get
            {
                return _FormatFlags;
            }
            set
            {
                if (_Readonly == false)
                {
                    if (this._FormatFlags != value)
                    {
                        _FormatFlags = value;
                        DisposeValue();
                    }
                }
                else
                {
                    throw new InvalidOperationException("readonly");
                }
            }
        }
#if !DCWriterForWASM

        /// <summary>
        /// 是否显示多行文本
        /// </summary>
        public bool MultiLine
        {
            get
            {
                return (this._FormatFlags & StringFormatFlags.NoWrap) != StringFormatFlags.NoWrap;
            }
            set
            {
                if (value)
                {
                    this._FormatFlags = this._FormatFlags & ~StringFormatFlags.NoWrap;
                }
                else
                {
                    this._FormatFlags = this._FormatFlags | StringFormatFlags.NoWrap;
                }
                this.DisposeValue();
            }
        }

#endif
        private StringTrimming _Trimming = StringTrimming.Character;
        /// <summary>
        /// 绘制的文本超出布局矩形的边缘时被剪裁的方式
        /// </summary>
        public StringTrimming Trimming
        {
            get
            {
                return _Trimming;
            }
            set
            {
                if (_Readonly == false)
                {
                    if (this._Trimming != value)
                    {
                        _Trimming = value;
                        DisposeValue();
                    }
                }
                else
                {
                    throw new InvalidOperationException("readonly");
                }
            }
        }
        private HotkeyPrefix _HotkeyPrefix = HotkeyPrefix.None;
#if !DCWriterForWASM

        /// <summary>
        /// 热键绘制方式
        /// </summary>
        public HotkeyPrefix HotkeyPrefix
        {
            get
            {
                return _HotkeyPrefix;
            }
            set
            {
                if (_Readonly == false)
                {
                    if (this._HotkeyPrefix != value)
                    {
                        this._HotkeyPrefix = value;
                        DisposeValue();
                    }
                }
                else
                {
                    throw new InvalidOperationException("readonly");
                }
            }
        }
#endif
       

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
        
        private StringFormat _Value  ;
        public StringFormat Value()
        {
            if (this == GenericDefault )
            {
                return StringFormat.GenericDefault;
            }
            else if (this == GenericTypographic )
            {
                return StringFormat.GenericTypographic;
            }
            if (_Value == null)
            {
                StringFormat f = null;
                if (this._Source == 1)
                {
                    f = new StringFormat(StringFormat.GenericDefault);
                }
                else if (this._Source == 2)
                {
                    f = new StringFormat(StringFormat.GenericTypographic);
                }
                else
                {
                    f = new StringFormat();
                }
                f.Alignment = this._Alignment;
                f.LineAlignment = this._LineAlignment;
                f.FormatFlags = this._FormatFlags;
                f.HotkeyPrefix = this._HotkeyPrefix;
                f.Trimming = this._Trimming;
                //f.SetDigitSubstitution(this._DigitSubstitutionLanguage, this._DigitSubstitutionMethod);
                this._Value = f;
            }
            return _Value;
        }

        public void Dispose()
        {
            DisposeValue();
        }
        private void DisposeValue()
        {
            if (this._Value != null)
            {
                this._Value.Dispose();
                this._Value = null;
            }
        }
        public DCStringFormat Clone()
        {
            var resut =  (DCStringFormat)this.MemberwiseClone();
            resut._Value = null;
            resut._Readonly = false;
            return resut;
        }
    }

   
}