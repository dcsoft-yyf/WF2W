using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using DCSoft.Common;
using System.Xml.Serialization;

namespace DCSoft.TemperatureChart
{
    /// <summary>
    /// 页面设置
    /// </summary>

#if !DCWriterForWASM
    [Serializable]
    [TypeConverter(typeof(DCSoft.Common.TypeConverterSupportProperties))]
    //[DCSoft.Common.DCDescriptionResourceSource(typeof(DCSoft.TemperatureChart.DCTimeLineDescriptionResource))]
    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = false)]
#endif
    public partial class DocumentPageSettings
    {
        /// <summary>
        /// 文档页面设置
        /// </summary>
        public DocumentPageSettings()
        {
        }

        private string _PaperSizeName = "A4";
        /// <summary>
        /// 纸张大小名称
        /// </summary>
        [DefaultValue("A4")]
        [DCDisplayName(typeof(DocumentPageSettings), "PaperSizeName")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string PaperSizeName
        {
            get
            {
                return _PaperSizeName;
            }
            set
            {
                _PaperSizeName = value;
            }
        }

        private int _RawKind = 0;
        /// <summary>
        /// 纸张大小原始类型编号
        /// </summary>
        [DefaultValue(0)]
        [DCDisplayName(typeof(DocumentPageSettings), "RawKind")]
        [XmlIgnore]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public int RawKind
        {
            get
            {
                return _RawKind;
            }
            set
            {
                _RawKind = value;
            }
        }

        private int intPaperWidth = 827;
        /// <summary>
        /// 纸张宽度,单位百分之一英寸
        /// </summary>
        [System.ComponentModel.DefaultValue(827)]
        [DCDisplayName(typeof(DocumentPageSettings), "PaperWidth")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public int PaperWidth
        {
            get
            {
                return intPaperWidth;
            }
            set
            {
                intPaperWidth = value;
            }
        }

        private int intPaperHeight = 1169;
        /// <summary>
        /// 纸张高度 单位百分之一英寸
        /// </summary>
        [System.ComponentModel.DefaultValue(1169)]
        [DCDisplayName(typeof(DocumentPageSettings), "PaperHeight")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public int PaperHeight
        {
            get
            {
                return intPaperHeight;
            }
            set
            {
                intPaperHeight = value;
            }
        }

        private int _LeftMargin = 100;
        /// <summary>
        /// 左页边距 单位百分之一英寸
        /// </summary>
        [System.ComponentModel.DefaultValue(100)]
        [DCDisplayName(typeof(DocumentPageSettings), "LeftMargin")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public int LeftMargin
        {
            get
            {
                return this._LeftMargin;
            }
            set
            {
                this._LeftMargin = value;
            }
        }

        private int _TopMargin = 100;
        /// <summary>
        /// 顶页边距 单位百分之一英寸
        /// </summary>
        [System.ComponentModel.DefaultValue(100)]
        [DCDisplayName(typeof(DocumentPageSettings), "TopMargin")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public int TopMargin
        {
            get
            {
                return this._TopMargin;
            }
            set
            {
                this._TopMargin = value;
            }
        }

        private int _RightMargin = 100;
        /// <summary>
        /// 右页边距 单位百分之一英寸
        /// </summary>
        [System.ComponentModel.DefaultValue(100)]
        [DCDisplayName(typeof(DocumentPageSettings), "RightMargin")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public int RightMargin
        {
            get
            {
                return _RightMargin;
            }
            set
            {
                this._RightMargin = value;
            }
        }

        private int _BottomMargin = 100;
        /// <summary>
        /// 底页边距 单位百分之一英寸
        /// </summary>
        [System.ComponentModel.DefaultValue(100)]
        [DCDisplayName(typeof(DocumentPageSettings), "BottomMargin")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public int BottomMargin
        {
            get
            {
                return this._BottomMargin;
            }
            set
            {
                this._BottomMargin = value;
            }
        }

        private bool bolLandscape = false;
        /// <summary>
        /// 横向打印标记
        /// </summary>
        [System.ComponentModel.DefaultValue(false)]
        [DCDisplayName(typeof(DocumentPageSettings), "Landscape")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public bool Landscape
        {
            get
            {
                return bolLandscape;
            }
            set
            {
                bolLandscape = value;
            }
        }

        private bool _AutoFitPageSize = false;

        /// <summary>
        /// 打印是否自适应缩放
        /// </summary>
        [System.ComponentModel.DefaultValue(false)]
        [DCDisplayName(typeof(DocumentPageSettings), "AutoFitPageSize")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public bool AutoFitPageSize
        {
            get
            {
                return _AutoFitPageSize;
            }
            set
            {
                _AutoFitPageSize = value;
            }
        }

#if !DCWriterForWASM
        /// <summary>
        /// 页面边界
        /// </summary>
        [Browsable(false)]
        public Rectangle Bounds
        {
            get
            {
                if (this.Landscape)
                {
                    return new Rectangle(0, 0, this.PaperHeight, this.PaperWidth);
                }
                else
                {
                    return new Rectangle(0, 0, this.PaperWidth, this.PaperHeight);
                }
            }
        }
        /// <summary>
        /// 将配置写入到一个页面配置对象中
        /// </summary>
        /// <param name="ps">页面配置对象</param>
        public void WriteTo(PageSettings ps)
        {
            if (ps != null)
            {
                ps.PaperSize = new PaperSize(this.PaperSizeName, this.PaperWidth, this.PaperHeight);
#if WINFORM || DCWriterForWinFormNET6
                ps.PaperSize.RawKind = this.RawKind;
#endif
                ps.Landscape = this.Landscape;
                ps.Margins = new Margins(this.LeftMargin, this.RightMargin, this.TopMargin, this.BottomMargin);
            }
        }
        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="ps">页面设置对象</param>
        public void ReadFrom(PageSettings ps)
        {
            if (ps != null)
            {
                this.PaperSizeName = ps.PaperSize.PaperName;
#if WINFORM || DCWriterForWinFormNET6

                this.RawKind = ps.PaperSize.RawKind;
#endif

                this.PaperWidth = ps.PaperSize.Width;
                this.PaperHeight = ps.PaperSize.Height;
                this.LeftMargin = ps.Margins.Left;
                this.TopMargin = ps.Margins.Top;
                this.RightMargin = ps.Margins.Right;
                this.BottomMargin = ps.Margins.Bottom;
                this.Landscape = ps.Landscape;
            }
        }
        internal void WriteJson(DCFastJsonTextWriter writer)
        {
            writer.WritePropertyNoFixName("PaperSizeName", this.PaperSizeName);
            writer.WritePropertyNoFixName("PaperHeight", ToMM(this.PaperHeight));
            writer.WritePropertyNoFixName("PaperWidth", ToMM(this.PaperWidth));
            writer.WritePropertyNoFixNameBoolean("Landscape", this.Landscape);

            writer.WritePropertyNoFixName("TopMargin", ToMM(this.TopMargin));
            writer.WritePropertyNoFixName("BottomMargin", ToMM(this.BottomMargin));
            writer.WritePropertyNoFixName("LeftMargin", ToMM(this.LeftMargin));
            writer.WritePropertyNoFixName("RightMargin", ToMM(this.RightMargin));
            writer.WritePropertyNoFixName("Unit", "Millimeter");
        }
#endif
#if !DCWriterForWASM
        //百分之一英寸转毫米
        private string ToMM(int value)
        {
            double d = ((double)value) / 100 * 25.4;
            d = Math.Round(d, 2);
            return d.ToString();
        }
#endif
    }
}
