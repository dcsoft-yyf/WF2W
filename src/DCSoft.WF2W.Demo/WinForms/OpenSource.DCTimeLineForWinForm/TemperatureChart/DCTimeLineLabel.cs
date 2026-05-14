using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using DCSoft.Common ;
using System.Runtime.InteropServices ;
using System.Xml.Serialization;
using DCSoft.Drawing ;
using System.Drawing;

// 袁永福到此一游

namespace DCSoft.TemperatureChart
{
    /// <summary>
    /// 文本标签对象
    /// </summary>

#if !DCWriterForWASM
    [ComVisible( false )]
    [Serializable]
    //[DCSoft.Common.DCDescriptionResourceSource(typeof( DCSoft.TemperatureChart.DCTimeLineDescriptionResource))]
    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = false  )]
#endif
    public partial class DCTimeLineLabel
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public DCTimeLineLabel()
        {
        }

        private string _Text = null;
        /// <summary>
        /// 文本
        /// </summary>
        [DefaultValue( null )]
        [XmlAttribute]
        [DCDisplayName(typeof(DCTimeLineLabel), "Text")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string Text
        {
            get
            {
                return _Text; 
            }
            set
            {
                _Text = value; 
            }
        }

        private XImageValue _Image = null;
        /// <summary>
        /// 图片内容
        /// </summary>
        [DefaultValue( null )]
        [XmlElement]
        [DCDisplayName(typeof(DCTimeLineLabel), "Image")]
        [Browsable( true )]
#if WINFORM || DCWriterForWinFormNET6
        //[Editor( 
        //    typeof( DCSoft.Drawing.Design.SimpleImageValueEditor )  , 
        //    typeof( System.Drawing.Design.UITypeEditor ))]
#endif
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public XImageValue Image
        {
            get
            {
                return _Image; 
            }
            set
            {
                _Image = value; 
            }
        }

        private bool _ShowBorder = false;
        /// <summary>
        /// 是否显示边框
        /// </summary>
        [DefaultValue(false)]
        [DCDisplayName(typeof(DCTimeLineLabel), "ShowBorder")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public bool ShowBorder
        {
            get 
            {
                return _ShowBorder; 
            }
            set 
            { 
                _ShowBorder = value; 
            }
        }

        private string _ParameterName = null;
        /// <summary>
        /// 引用的参数名
        /// </summary>
        [DefaultValue( null )]
        [XmlAttribute]
        [DCDisplayName(typeof(DCTimeLineLabel), "ParameterName")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string ParameterName
        {
            get
            {
                return _ParameterName; 
            }
            set
            {
                _ParameterName = value; 
            }
        }

        private bool _MultiLine = false;
        /// <summary>
        /// 显示多行
        /// </summary>
        [DefaultValue( false )]
        [XmlAttribute]
        [DCDisplayName(typeof(DCTimeLineLabel), "MultiLine")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public bool MultiLine
        {
            get
            {
                return _MultiLine; 
            }
            set
            {
                _MultiLine = value; 
            }
        }

        private XFontValue _Font = null;
        /// <summary>
        /// 绘制文本使用的字体
        /// </summary>
        [DefaultValue( null )]
        [DCDisplayName(typeof(DCTimeLineLabel), "Font")]
        [Browsable( true )]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public XFontValue Font
        {
            get 
            {
                return _Font; 
            }
            set
            {
                _Font = value; 
            }
        }

        private Color _Color = Color.Black;
        /// <summary>
        /// 文本颜色
        /// </summary>
        [DefaultColorValue("Black")]
        [XmlIgnore]
        [DCDisplayName(typeof(DCTimeLineLabel), "Color")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public Color Color
        {
            get
            {
                return _Color;
            }
            set
            {
                _Color = value;
            }
        }
        /// <summary>
        /// 文本颜色值
        /// </summary>
        [DefaultValue(null)]
        [Browsable(false)]
        [XmlAttribute]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string ColorValue
        {
            get
            {
                return XMLSerializeHelper.ColorToString(this.Color, Color.Black);
            }
            set
            {
                this.Color = XMLSerializeHelper.StringToColor(value, Color.Black);
            }
        }

        private Color _BackColor = Color.Transparent;
        /// <summary>
        /// 背景色
        /// </summary>
        [DefaultColorValue("Transparent")]
        [XmlIgnore]
        [DCDisplayName(typeof(DCTimeLineLabel), "BackColor")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public Color BackColor
        {
            get
            {
                return _BackColor;
            }
            set
            {
                _BackColor = value;
            }
        }
        /// <summary>
        /// 文本颜色值
        /// </summary>
        [DefaultValue(null)]
        [Browsable(false)]
        [XmlAttribute]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string BackColorValue
        {
            get
            {
                return XMLSerializeHelper.ColorToString(this.BackColor, Color.Transparent);
            }
            set
            {
                this.BackColor = XMLSerializeHelper.StringToColor(value, Color.Transparent);
            }
        }

        private StringAlignment _Alignment = StringAlignment.Center;
        /// <summary>
        /// 水平对齐方式
        /// </summary>
        [DefaultValue( StringAlignment.Center )]
        [XmlAttribute]
        [DCDisplayName(typeof(DCTimeLineLabel), "Alignment")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public StringAlignment Alignment
        {
            get
            {
                return _Alignment; 
            }
            set
            {
                _Alignment = value; 
            }
        }

        private StringAlignment _LineAlignment = StringAlignment.Center;
        /// <summary>
        /// 垂直对齐方式
        /// </summary>
        [DefaultValue( StringAlignment.Center )]
        [XmlAttribute]
        [DCDisplayName(typeof(DCTimeLineLabel), "LineAlignment")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public StringAlignment LineAlignment
        {
            get
            {
                return _LineAlignment; 
            }
            set
            {
                _LineAlignment = value; 
            }
        }

        private float _Left = 0;
        /// <summary>
        /// 左端位置
        /// </summary>
        [DefaultValue( 0f )]
        [XmlAttribute]
        [DCDisplayName(typeof(DCTimeLineLabel), "Left")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public float Left
        {
            get
            {
                return _Left; 
            }
            set
            {
                _Left = value; 
            }
        }

        private float _Top = 0;
        /// <summary>
        /// 顶端位置
        /// </summary>
        [DefaultValue(0f)]
        [XmlAttribute]
        [DCDisplayName(typeof(DCTimeLineLabel), "Top")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public float Top
        {
            get
            {
                return _Top; 
            }
            set
            {
                _Top = value; 
            }
        }

        private float _Width = 100f;
        /// <summary>
        /// 宽度
        /// </summary>
        [DefaultValue( 100f )]
        [XmlAttribute]
        [DCDisplayName(typeof(DCTimeLineLabel), "Width")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public float Width
        {
            get
            {
                return _Width; 
            }
            set
            {
                _Width = value; 
            }
        }

        private float _Height = 100f;
        /// <summary>
        /// 高度
        /// </summary>
        [DefaultValue( 100f )]
        [XmlAttribute]
        [DCDisplayName(typeof(DCTimeLineLabel), "Height")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public float Height
        {
            get
            {
                return _Height; 
            }
            set
            {
                _Height = value; 
            }
        }
#if !DCWriterForWASM

        //代表文本框的整个方框
        [XmlIgnore]
        internal RectangleF _LabelBounds = RectangleF.Empty;
#endif
        /// <summary>
        /// 复制对象
        /// </summary>
        /// <returns>复制品</returns>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public DCTimeLineLabel Clone()
        {
            return (DCTimeLineLabel)this.MemberwiseClone();
        }
#if !DCWriterForWASM
        internal void WriteJson(DCFastJsonTextWriter writer)
        {
            writer.WritePropertyNoFixName("Left", this.Left.ToString());
            writer.WritePropertyNoFixName("Top", this.Top.ToString());
            writer.WritePropertyNoFixName("Width", this.Width.ToString());
            writer.WritePropertyNoFixName("Height", this.Height.ToString());
            writer.WritePropertyNoFixName("ParameterName", this.ParameterName);

            writer.WritePropertyNoFixName("Text", this.Text);
            writer.WritePropertyNoFixNameBoolean("MultiLine", this.MultiLine);
            writer.WritePropertyNoFixNameBoolean("ShowBorder", this.ShowBorder);
            writer.WritePropertyNoFixName("BackColorValue", this.BackColorValue);
            writer.WritePropertyNoFixName("TextColorValue", this.ColorValue);
            
            writer.WritePropertyNoFixName("TextFontName", this.Font != null ? this.Font.Name : XFontValue.DefaultFontName);
            writer.WritePropertyNoFixName("TextFontSize", this.Font != null ? this.Font.Size.ToString() : XFontValue.DefaultFontSize.ToString());
            writer.WritePropertyNoFixNameBoolean("TextFontBold", this.Font != null ? this.Font.Bold : false);
            writer.WritePropertyNoFixNameBoolean("TextFontItalic", this.Font != null ? this.Font.Italic : false);
            writer.WritePropertyNoFixNameBoolean("TextFontUnderline", this.Font != null ? this.Font.Underline : false);
            writer.WritePropertyNoFixName("ImageDataBase64String", this.Image != null ? this.Image.ImageDataBase64String : null);

            writer.WritePropertyNoFixNameEnum("Alignment", typeof(StringAlignment), this.Alignment);
            writer.WritePropertyNoFixNameEnum("LineAlignment", typeof(StringAlignment), this.LineAlignment);
        }

#endif
        private LabelPositionFixMode _PositionFixModeForAutoHeightLine = LabelPositionFixMode.None;
        /// <summary>
        /// 标识该文本框的大致位置
        /// </summary>
        [DefaultValue(LabelPositionFixMode.None)]
        [XmlAttribute]
        [DCDisplayName(typeof(DCTimeLineLabel), "LabelPositionFixMode")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public LabelPositionFixMode PositionFixModeForAutoHeightLine
        {
            get
            {
                return _PositionFixModeForAutoHeightLine;
            }
            set
            {
                _PositionFixModeForAutoHeightLine = value;
            }
        }
    }


/// <summary>
/// 文本框位置修正模式
/// </summary>
#if !DCWriterForWASM
    [System.Runtime.InteropServices.ComVisible(false)]
    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
#endif
    public enum LabelPositionFixMode
    {
        /// <summary>
        /// 不处理
        /// </summary>
        None,
        /// <summary>
        /// 表示该文本框位于数据网格区间
        /// </summary>
        InsideDataGrid,
        /// <summary>
        /// 表示该文本框与自动撑大的数据行位于同一水平线
        /// </summary>
        InsideAutoHeightLine,
        /// <summary>
        /// 表示该文本框位于自动撑大的数据行的上方
        /// </summary>
        AboveAutoHeightLine
    }

    /// <summary>
    /// 文本标签列表
    /// </summary>

#if !DCWriterForWASM
    [ComVisible( false )]
    [Serializable]
    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true )]
#endif
    public class DCTimeLineLabelList : List<DCTimeLineLabel>
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public DCTimeLineLabelList()
        {
        }

        /// <summary>
        /// 复制对象
        /// </summary>
        /// <returns>复制品</returns>
        public DCTimeLineLabelList Clone()
        {
            DCTimeLineLabelList list = new DCTimeLineLabelList();
            foreach (DCTimeLineLabel lbl in this)
            {
                list.Add(lbl.Clone());
            }
            return list;
        }
    }
}
