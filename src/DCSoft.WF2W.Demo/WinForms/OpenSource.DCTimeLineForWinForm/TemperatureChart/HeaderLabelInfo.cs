using System;
using System.Collections.Generic;
using System.Text;
using DCSoft.Common ;
using System.ComponentModel ;
using System.Xml.Serialization ;
using System.Drawing ;
using System.Drawing.Drawing2D ;
using DCSoft.Drawing ;

namespace DCSoft.TemperatureChart
{
    /// <summary>
    /// 标题文本信息对象
    /// </summary>

#if !DCWriterForWASM
    [Serializable]
    [DCSoft.Common.DCPublishAPI]
    //[DCSoft.Common.DCDescriptionResourceSource(typeof(DCSoft.TemperatureChart.DCTimeLineDescriptionResource))]
    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = false)]
#endif
    public partial class HeaderLabelInfo
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public HeaderLabelInfo()
        {
        }

        private string _Title = string.Empty;
        /// <summary>
        /// 标题
        /// </summary>
        [DefaultValue("")]
        [System.Xml.Serialization.XmlAttribute]
        [DCDisplayName(typeof(HeaderLabelInfo), "Title")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string Title
        {
            get
            {
                return _Title;
            }
            set
            {
                _Title = value;
            }
        }
        private string _DataSourceName = null;
        /// <summary>
        /// 数据源名称
        /// </summary>
        [DefaultValue(null)]
        [System.Xml.Serialization.XmlAttribute]
        [DCDisplayName(typeof(HeaderLabelInfo), "DataSourceName")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string DataSourceName
        {
            get
            {
                return _DataSourceName;
            }
            set
            {
                _DataSourceName = value;
            }
        }

        private string _ValueFieldName = null;
        /// <summary>
        /// 文本绑定的数据字段名
        /// </summary>
        [DefaultValue(null)]
        [System.Xml.Serialization.XmlAttribute]
        [DCDisplayName(typeof(HeaderLabelInfo), "ValueFieldName")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string ValueFieldName
        {
            get
            {
                return _ValueFieldName;
            }
            set
            {
                _ValueFieldName = value;
            }
        }

        private string _ParameterName = null;
        /// <summary>
        /// 使用的参数名
        /// </summary>
        [DefaultValue(null)]
        [XmlAttribute]
        [DCDisplayName(typeof(HeaderLabelInfo), "ParameterName")]
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

        private string _Value = string.Empty;
        /// <summary>
        /// 数值
        /// </summary>
        [DefaultValue("")]
        [System.Xml.Serialization.XmlAttribute]
        [DCDisplayName(typeof(HeaderLabelInfo), "Value")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string Value
        {
            get
            {
                return _Value;
            }
            set
            {
                _Value = value;
            }
        }


        private bool _ValueUnderline = false;
        /// <summary>
        /// 数值是否带下划线
        /// </summary>
        [DefaultValue(false)]
        [System.Xml.Serialization.XmlAttribute]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public bool ValueUnderline
        {
            get
            {
                return _ValueUnderline;
            }
            set
            {
                _ValueUnderline = value;
            }
        }

        private char _SeperatorChar = ':';
        /// <summary>
        /// 标题与数值之间的分隔符
        /// </summary>
        [DefaultValue(':')]
        [System.Xml.Serialization.XmlAttribute]
        [DCDisplayName(typeof(HeaderLabelInfo), "SeperatorChar")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public char SeperatorChar
        {
            get
            {
                return _SeperatorChar;
            }
            set
            {
                _SeperatorChar = value;
            }
        }

        private int _GroupIndex = 0;
        /// <summary>
        /// 设置小标题的组名，相同组名的小标题会位于一行，不同组的小标题会分属不同的行
        /// </summary>
        [DefaultValue(0)]
        [System.Xml.Serialization.XmlAttribute]
        [DCDisplayName(typeof(HeaderLabelInfo), "GroupIndex")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public int GroupIndex
        {
            get
            {
                return _GroupIndex;
            }
            set
            {
                _GroupIndex = value;
            }
        }

#if ! DCWriterForWASM

        private float _Left = 0;
        /// <summary>
        /// 左端位置
        /// </summary>
        [Browsable(false)]
        [XmlIgnore]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public float Left
        {
            get { return _Left; }
            set { _Left = value; }
        }

        private float _Top = 0;
        /// <summary>
        /// 顶端位置
        /// </summary>
        [Browsable(false)]
        [XmlIgnore]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public float Top
        {
            get { return _Top; }
            set { _Top = value; }
        }

        private float _Width = 0;
        /// <summary>
        /// 宽度
        /// </summary>
        [Browsable(false)]
        [XmlIgnore]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public float Width
        {
            get { return _Width; }
            set { _Width = value; }
        }

        private float _Height = 0;
        /// <summary>
        /// 高度
        /// </summary>
        [Browsable(false)]
        [XmlIgnore]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public float Height
        {
            get { return _Height; }
            set { _Height = value; }
        }
        ///// <summary>
        ///// 宽度
        ///// </summary>
        //internal float ViewWidth = 0;

        /// <summary>
        /// 返回表示对象的字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.Title + "  " + this.GetType().Name;
        }
#endif
        /// <summary>
        /// 复制对象
        /// </summary>
        /// <returns>复制品</returns>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public HeaderLabelInfo Clone()
        {
            return (HeaderLabelInfo)this.MemberwiseClone();
        }
#if !DCWriterForWASM
        [NonSerialized]
        private TemperatureDocument _OwnerDocument = null;
        /// <summary>
        /// 对象所在的文档对象
        /// </summary>
        [XmlIgnore]
        [Browsable(false)]
        public TemperatureDocument OwnerDocument
        {
            get
            {
                return _OwnerDocument;
            }
            set
            {
                _OwnerDocument = value;
            }
        }

        /// <summary>
        /// 实际绘制图形时使用的文本值
        /// </summary>
        internal string RuntimeText
        {
            get
            {
                string txt = this.Value;
                if (this.OwnerDocument != null)
                {
                    txt = this.OwnerDocument.Parameters.Convert(this.ParameterName, txt);
                    txt = this.OwnerDocument.GetTextProcessedForLinux(txt);
                }
                return txt;
            }
        }

        private SizeF sizeoftitle = SizeF.Empty;
        private SizeF sizeoftext = SizeF.Empty;
        /// <summary>
        /// 计算大小
        /// </summary>
        /// <param name="g"></param>
        /// <param name="labelFont"></param>
        internal void RefreshSize(DCGraphics g, XFontValue labelFont)
        {
            string s = this.Title + this.SeperatorChar;
            this.sizeoftitle = g.MeasureString(
                s,
                labelFont,
                10000,
                DCStringFormat.GenericTypographic);
            this.sizeoftext = g.MeasureString(
                this.RuntimeText,
                labelFont,
                10000,
                DCStringFormat.GenericTypographic );
            this.Width = sizeoftitle.Width + sizeoftext.Width;
            this.Height = Math.Max(this.OwnerDocument.HeaderLabels.HeaderLabelMaxHeight, Math.Max(sizeoftitle.Height, sizeoftext.Height));
        }

        internal void Draw(DCGraphics g, XFontValue labelFont, Color textColor, string viewMode)
        {
            using (var format = DCStringFormat.GenericTypographic.Clone())
            {
                format.Alignment = StringAlignment.Near;
                format.LineAlignment = this.OwnerDocument.Config.HeaderLabelLineAlignment;
                format.FormatFlags = StringFormatFlags.NoWrap | StringFormatFlags.NoClip ;
                if (this.RuntimeText.IndexOf("\n") > 0)
                {
                    //format.LineAlignment = StringAlignment.Near;
                    //string strPart1 = this.RuntimeText.Substring(0, this.RuntimeText.IndexOf(this.SeperatorChar) + 1);
                    //string strPart2 = this.RuntimeText.Substring(this.RuntimeText.IndexOf(this.SeperatorChar) + 1, this.RuntimeText.IndexOf("\r\n") - 1);
                    //string strPart3 = this.RuntimeText.Substring(this.RuntimeText.IndexOf("\r\n") + 2);

                    if (viewMode == "Page")
                    {
                        //string[] array = this.RuntimeText.ToString().Split("-");

                        //System.Windows.Forms.MessageBox.Show(g.MeasureString(strPart3,labelFont).Width.ToString());
                        g.DrawString(
                            this.Title + this.SeperatorChar,
                            labelFont,
                            textColor,
                            new RectangleF(
                                this.Left,
                                this.Top,
                                this.sizeoftitle.Width,
                                this.Height),
                            format);
                        g.DrawString(
                            this.RuntimeText,
                            labelFont,
                            textColor,
                            new RectangleF(
                                this.Left + this.sizeoftitle.Width,
                                this.Top,
                                this.sizeoftext.Width,
                                this.Height),
                            format);
                       
                    }
                    else
                    {
                        g.DrawString(
                                this.Title + this.SeperatorChar + this.RuntimeText,
                                labelFont,
                                textColor,
                                new RectangleF(
                                this.Left,
                                this.Top,
                                this.Width,
                                this.OwnerDocument.HeaderLabels.HeaderLabelMaxHeight),
                                format);
                    }
                }
                else
                {
                    g.DrawString(
                         this.Title + this.SeperatorChar + this.RuntimeText,
                         labelFont,
                         textColor,
                         new RectangleF(
                             this.Left,
                             this.Top,
                             this.Width,
                             this.Height),
                         format);
                }

                if (DCGraphicsForTimeLine.TimeLineRunInLinuxMode &&
                    this.RuntimeText != null &&
                    this.RuntimeText.Length > 0 &&
                    this.RuntimeText[this.RuntimeText.Length - 1] == '.')
                {
                    SizeF size = g.MeasureString(".", labelFont.Value, 10000, format.Value());
                    RectangleF rect = new RectangleF(
                            this.Left + this.Width - size.Width,
                            this.Top,
                            size.Width,
                            this.OwnerDocument.HeaderLabels.HeaderLabelMaxHeight);
                    g.FillRectangle(
                        Color.White,
                        rect);
                    g.DrawString(
                        "H",
                        labelFont,
                        Color.White,
                        rect,
                        format);
                }
            }
        }
        internal void WriteJson(DCFastJsonTextWriter writer)
        {
            writer.WritePropertyNoFixName("Title", this.Title);
            writer.WritePropertyNoFixName("ParameterName", this.ParameterName);
            writer.WritePropertyNoFixName("Value", this.Value);
        }
#endif
    }

    /// <summary>
    /// 标题信息列表
    /// </summary>

#if !DCWriterForWASM
    [Serializable]
    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true )]
#endif
    public partial class HeaderLabelInfoList : List<HeaderLabelInfo>
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public HeaderLabelInfoList()
        {
        }
#if !DCWriterForWASM
        /// <summary>
        /// 添加对象
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="dataSourceName">数据源名</param>
        /// <returns>新添加的对象</returns>
        public HeaderLabelInfo AddItemByDataSourceName(string title, string dataSourceName)
        {
            HeaderLabelInfo info = new HeaderLabelInfo();
            info.Title = title;
            info.DataSourceName = dataSourceName;
            this.Add(info);
            return info;
        }

        /// <summary>
        /// 添加对象
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="Value">数据</param>
        /// <returns>新添加的对象</returns>
        public HeaderLabelInfo AddItemByValue(string title, string Value)
        {
            HeaderLabelInfo info = new HeaderLabelInfo();
            info.Title = title;
            info.Value = Value;
            this.Add(info);
            return info;
        }
        /// <summary>
        /// 临时用来保存页眉标签最大高度的变量（因为要考虑到标签文本出现多行的情况）
        /// </summary>
        [XmlIgnore]
        internal float HeaderLabelMaxHeight = 0;
#endif

       

        /// <summary>
        /// 复制对象
        /// </summary>
        /// <returns>复制品</returns>
        public HeaderLabelInfoList Clone()
        {
            HeaderLabelInfoList list = new HeaderLabelInfoList();
            foreach (HeaderLabelInfo info in this)
            {
                list.Add(info.Clone());
            }
            return list;
        }
    }
}
