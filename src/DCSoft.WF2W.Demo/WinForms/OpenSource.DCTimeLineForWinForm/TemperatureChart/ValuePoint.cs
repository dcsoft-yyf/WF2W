using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.ComponentModel;
using DCSoft.Data;
using System.Drawing;
using DCSoft.Drawing;
using System.Runtime.InteropServices;
using DCSoft.Common;

// 袁永福到此一游

namespace DCSoft.TemperatureChart
{
    /// <summary>
    /// 数据点对象
    /// </summary> 
#if !DCWriterForWASM
    [DCSoft.Common.DCPublishAPI]
    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = false )]
    [Serializable]
#endif
    public partial class ValuePoint
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public ValuePoint()
        {
        }
        [NonSerialized]
        private ValuePointList _OwnerList = null;
        /// <summary>
        /// 对象所示的列表
        /// </summary>
        [XmlIgnore]
        internal ValuePointList OwnerList
        {
            get
            {
                return _OwnerList; 
            }
            set
            {
                _OwnerList = value; 
            }
        }
        [NonSerialized]
        internal bool HollowCovertFlag = false;
#if !DCWriterForWASM
        private static int _InstanceCount = 0 ;
        private int _InstanceIndex = _InstanceCount++;
        /// <summary>
        /// 对象实例编号
        /// </summary>
        public int InstanceIndex
        {
            get
            {
                return _InstanceIndex; 
            }
        }
#endif
        private bool _Verified = false;
        /// <summary>
        /// 是否核实，体温若突然上升或下降与病情不符时应予复测，核实无误后在原体温上方用蓝笔写一小写英文字母"V"
        /// </summary>
        [DefaultValue(false)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public bool Verified
        {
            get 
            {
                return _Verified;
            }
            set 
            { 
                _Verified = value;
            }
        }

        private DCTimeLineBooleanValue _LineStop = DCTimeLineBooleanValue.Inherit ;
        /// <summary>
        /// 数据点是否断线，设为True则强制不连线，默认Inherit则采用老版断线方案决定数据点是否连线
        /// </summary>
        [DefaultValue(DCTimeLineBooleanValue.Inherit)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        [XmlAttribute]
        public DCTimeLineBooleanValue LineStop
        {
            get
            {
                return _LineStop;
            }
            set
            {
                _LineStop = value;
            }
        }

        private Color _VerifiedColor = Color.Black;
        /// <summary>
        /// 核实字符颜色
        /// </summary>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        [XmlIgnore]
        public Color VerifiedColor
        {
            get
            {
                return _VerifiedColor;
            }
            set
            {
                _VerifiedColor = value;
            }
        }
        private string _VerifiedColorValue = "black";
        /// <summary>
        /// 核实字符颜色值
        /// </summary>
        [DefaultValue("black")]
        [XmlAttribute]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string VerifiedColorValue
        {
            get
            {
                return XMLSerializeHelper.ColorToString(this._VerifiedColor, Color.Black);
            }
            set
            {
                this._VerifiedColor = XMLSerializeHelper.StringToColor(value, Color.Black);
            }
        }
        private StringAlignment _VerifiedAlignment = StringAlignment.Center;
        /// <summary>
        /// 核实字符对齐
        /// </summary>
        [DefaultValue( StringAlignment.Center )]
        [XmlAttribute]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public StringAlignment VerifiedAlignment
        {
            get
            {
                return _VerifiedAlignment;
            }
            set
            {
                this._VerifiedAlignment = value;
            }
        }

        private DCTimeLineBooleanValue _ShowPointValue = DCTimeLineBooleanValue.Inherit;
        /// <summary>
        /// 获取或设置是否在数据点的图例旁边显示数据点的值
        /// </summary>
        [DefaultValue( DCTimeLineBooleanValue.Inherit )]
        [System.Xml.Serialization.XmlAttribute]
        [DCDisplayName(typeof(YAxisInfo), "ShowPointValue")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public DCTimeLineBooleanValue ShowPointValue
        {
            get
            {
                return _ShowPointValue;
            }
            set
            {
                _ShowPointValue = value;
            }
        }



#if !DCWriterForWASM

        internal int _OwnerPageIndex = -1;
        /// <summary>
        /// 获取数据点所属第几页，若值为-1则无效
        /// </summary>
        [DefaultValue(-1)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public int OwnerPageIndex
        {
            get
            {
                return _OwnerPageIndex;
            }
        }
#endif
        private float _ValueTextTopPadding = 0f;
        /// <summary>
        /// 数值提示文本顶端内距
        /// </summary>
        [DefaultValue(0f)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public float ValueTextTopPadding
        {
            get
            {
                return _ValueTextTopPadding;
            }
            set
            {
                _ValueTextTopPadding = value;
            }
        }
        private string _TagValue = null;
        /// <summary>
        /// 额外标记值
        /// </summary>
        [DefaultValue( null )]
        [XmlAttribute]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string TagValue
        {
            get 
            {
                return _TagValue; 
            }
            set
            {
                _TagValue = value; 
            }
        }

        private string _ID = null;
        /// <summary>
        /// 节点编号
        /// </summary>
        [System.Xml.Serialization.XmlAttribute]
        [DefaultValue( null )]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string ID
        {
            get
            {
                return _ID; 
            }
            set
            {
                _ID = value; 
            }
        }

        private bool _Superscript = false;
        /// <summary>
        /// 数据点文本是否为上标样式
        /// </summary>
        [System.Xml.Serialization.XmlAttribute]
        [DefaultValue(false)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public bool Superscript
        {
            get
            {
                return _Superscript;
            }
            set
            {
                _Superscript = value;
            }
        }

        private ValuePointSymbolStyle _SpecifySymbolStyle = ValuePointSymbolStyle.Default;
        /// <summary>
        /// 指定的数据点图例样式
        /// </summary>
        [DefaultValue( ValuePointSymbolStyle.Default )]
        [XmlAttribute]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public ValuePointSymbolStyle SpecifySymbolStyle
        {
            get
            {
                return _SpecifySymbolStyle; 
            }
            set
            {
                _SpecifySymbolStyle = value; 
            }
        }

        private float _SymbolOffsetX = 0f;
        /// <summary>
        /// 数据点符号绘制横向偏移
        /// </summary>
        [DefaultValue(0f)]
        [System.Xml.Serialization.XmlAttribute]
        [DCDisplayName(typeof(YAxisInfo), "SymbolOffsetX")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public float SymbolOffsetX
        {
            get
            {
                return _SymbolOffsetX;
            }
            set
            {
                _SymbolOffsetX = value;
            }
        }

        private float _SymbolOffsetY = 0f;
        /// <summary>
        /// 数据点符号绘制纵向偏移
        /// </summary>
        [DefaultValue(0f)]
        [System.Xml.Serialization.XmlAttribute]
        [DCDisplayName(typeof(YAxisInfo), "SymbolOffsetY")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public float SymbolOffsetY
        {
            get
            {
                return _SymbolOffsetY;
            }
            set
            {
                _SymbolOffsetY = value;
            }
        }

        private ValuePointSymbolStyle _SpecifyLanternSymbolStyle = ValuePointSymbolStyle.HollowCicle;
        /// <summary>
        /// 指定的数据点的灯笼数据的图例样式
        /// </summary>
        [DefaultValue(ValuePointSymbolStyle.HollowCicle)]
        [XmlAttribute]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public ValuePointSymbolStyle SpecifyLanternSymbolStyle
        {
            get
            {
                return _SpecifyLanternSymbolStyle;
            }
            set
            {
                _SpecifyLanternSymbolStyle = value;
            }
        }

        private char _CharacterForLanternSymbolStyle = 'R';
        /// <summary>
        /// 获取或设置当灯笼数据图例枚举被设置成字符或套圈字符时，此处将要使用的字符变量
        /// </summary>
        //[DefaultValue('R')]
        [XmlIgnore]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public char CharacterForLanternSymbolStyle
        {
            get
            {
                return _CharacterForLanternSymbolStyle;
            }
            set
            {
                _CharacterForLanternSymbolStyle = value;
            }
        }

        [DefaultValue(82)]
        [XmlAttribute]
        public int IntCharLantern
        {
            get
            {
                return (int)CharacterForLanternSymbolStyle;
            }
            set
            {
                CharacterForLanternSymbolStyle = (char)value;
            }
        }


        private char _CharacterForCharSymbolStyle = 'R';
        /// <summary>
        /// 获取或设置当SpecifySymbolStyle枚举被设置成字符或套圈字符时，此处将要使用的字符变量
        /// </summary>
        //[DefaultValue('R')]
        [XmlIgnore]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public char CharacterForCharSymbolStyle
        {
            get
            {
                return _CharacterForCharSymbolStyle;
            }
            set
            {
                _CharacterForCharSymbolStyle = value;
            }
        }

        [DefaultValue(82)]
        [XmlAttribute]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public int IntCharSymbol
        {
            get
            {
                return (int)CharacterForCharSymbolStyle;
            }
            set
            {
                CharacterForCharSymbolStyle = (char)value;
            }
        }

        private string _Link = null;
        /// <summary>
        /// 超链接地址
        /// </summary>
        [System.Xml.Serialization.XmlAttribute]
        [DefaultValue(null)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string Link
        {
            get
            {
                return _Link; 
            }
            set
            {
                _Link = value; 
            }
        }

        private XFontValue _Font = null;
        /// <summary>
        /// 数据点自己使用的字体，默认为空
        /// </summary>
        [DefaultValue(null)]
        [XmlElement]
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
#if !DCWriterForWASM
        /// <summary>
        /// 使用BASE64字符串来加载图标
        /// </summary>
        /// <param name="txt"></param>
        [System.Runtime.InteropServices.ComVisible( true )]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void LoadImageByBase64String(string txt)
        {
            Image img = DCSoft.Common.ImageHelper.LoadImageBase64UseBuffer(txt);
            _Image = new XImageValue(img);
        }
#endif
        private XImageValue _Image = null;
        /// <summary>
        /// 图标
        /// </summary>
        [DefaultValue( null )]
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

        private XImageValue _CustomImage = null;
        /// <summary>
        /// 自定义图标
        /// </summary>
        [DefaultValue(null)]
        [Browsable(true)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public XImageValue CustomImage
        {
            get { return _CustomImage; }
            set { _CustomImage = value; }
        }
#if !DCWriterForWASM

        /// <summary>
        /// 图标数值
        /// </summary>
        [DefaultValue( null )]
        [Browsable( false )]
        [XmlIgnore]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public Image ImageValue
        {
            get
            {
                if (_Image == null)
                {
                    return null;
                }
                else
                {
                    return _Image.Value;
                }
            }
            set
            {
                if (value == null)
                {
                    _Image = null;
                }
                else
                {
                    _Image = new XImageValue(value);
                }
            }
        }

#endif
        private string _LinkTarget = null;
        /// <summary>
        /// 超链接目标
        /// </summary>
        [System.Xml.Serialization.XmlAttribute]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        [DefaultValue(null)]
        public string LinkTarget
        {
            get
            {
                return _LinkTarget;
            }
            set
            {
                _LinkTarget = value;
            }
        }

        private string _Title = null;
        /// <summary>
        /// 标题
        /// </summary>
        [System.Xml.Serialization.XmlAttribute]
        [DefaultValue(null)]
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
#if !DCWriterForWASM
        [NonSerialized]
        private bool _RuntimeSeperatorLineVisible = false;
        /// <summary>
        /// 文本数据点在出现重合时上方分隔线在运行时是否显示
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        [DefaultValue(false)]
        [Browsable(false)]
        internal bool RuntimeSeperatorLineVisible
        {
            get
            {
                return _RuntimeSeperatorLineVisible;
            }
            set
            {
                _RuntimeSeperatorLineVisible = value;
            }
        }
        /// <summary>
        /// 运行时标题
        /// </summary>
        [Browsable( false)]
        public string RuntimeTitle
        {
            get
            {
                string origintitle = GetRuntimeTitle(this);
                if (this.CoincidePoint != null)
                {
                    origintitle = origintitle + Environment.NewLine + GetRuntimeTitle(this.CoincidePoint);
                }
                if (this.ConveredPoint != null)
                {
                    origintitle = origintitle + Environment.NewLine + GetRuntimeTitle(this.ConveredPoint);
                }
                return origintitle;
            }
        }
        private string GetRuntimeTitle(ValuePoint vp)
        {
            if (vp.Title == null || vp.Title.Length == 0)
            {
                string result = vp.Time.ToString("yyyy-MM-dd HH:mm:ss");
                if (vp.Parent is YAxisInfo)
                {
                    YAxisInfo ya = (YAxisInfo)vp.Parent;
                    if (ya.Style == YAxisInfoStyle.Value)
                    {
                        result = result + " " + ((YAxisInfo)vp.Parent).Title;
                    }
                }
                else if (vp.Parent is TitleLineInfo)
                {
                    result = result + " " + ((TitleLineInfo)vp.Parent).Title;
                }
                if (string.IsNullOrEmpty(vp.Text) == false)
                {
                    string temptxt = vp.Text;
                    if(vp.UseAdvVerticalStyle && vp.UseAdvVerticalStyle2)
                    {
                        temptxt = temptxt.Replace("^", "");
                    }
                    result = result + " " + temptxt;
                }
                else if (TemperatureDocument.IsNullValue(vp.Value) == false)
                {
                    result = result + " " + vp.Value;
                }

                if (TemperatureDocument.IsNullValue(vp.LanternValue) == false)
                {
                    string arrow = "";
                    if(vp.LanternValue > vp.Value)
                    {
                        arrow = "↑";
                    }else if(vp.LanternValue < vp.Value)
                    {
                        arrow = "↓";
                    }
                    result = result + " " + arrow + vp.LanternValue;
                }
                if (string.IsNullOrEmpty(vp.Link) == false)
                {
                    result = result + Environment.NewLine + vp.Link;
                }
                return result;
            }
            else
            {
                return vp.Title;
            }
        }

#endif

        private DCTimeLineBooleanValue _VerticalLine = DCTimeLineBooleanValue.Inherit;
        /// <summary>
        /// 控制文本数据点在阴影区域上下是否单独连线，默认为继承Y轴设置
        /// </summary>
        [System.Xml.Serialization.XmlAttribute]
        [DefaultValue(DCTimeLineBooleanValue.Inherit)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public DCTimeLineBooleanValue VerticalLine
        {
            get
            {
                return _VerticalLine;
            }
            set
            {
                _VerticalLine = value;
            }
        }

        private bool _UseAdvVerticalStyle = false;
        /// <summary>
        /// 控制该文本数据点是否启用高级竖行排列功能，高级竖行排列功能指当文本中出现单独的英文字符或数字时，该字符仍然横向显示不会竖向排列
        /// </summary>
        [System.Xml.Serialization.XmlAttribute]
        [DefaultValue(false)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public bool UseAdvVerticalStyle
        {
            get
            {
                return _UseAdvVerticalStyle;
            }
            set
            {
                _UseAdvVerticalStyle = value;
            }
        }

        private bool _UseAdvVerticalStyle2 = false;
        /// <summary>
        /// 控制该文本数据点是否启用高级竖行排列功能，竖形排列文本始终^特殊字符来控制竖向排列
        /// </summary>
        [System.Xml.Serialization.XmlAttribute]
        [DefaultValue(false)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public bool UseAdvVerticalStyle2
        {
            get
            {
                return _UseAdvVerticalStyle2;
            }
            set
            {
                _UseAdvVerticalStyle2 = value;
            }
        }

        private DateTime _Time = DateTime.MinValue;
        /// <summary>
        /// 数据时间
        /// </summary>
        [System.Xml.Serialization.XmlAttribute]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public DateTime Time
        {
            get
            {
                return _Time; 
            }
            set
            {
                _Time = value; 
            }
        }

        private DateTime _EndTime = DateTime.MinValue;
        /// <summary>
        /// 数据结束时间
        /// </summary>
        [System.Xml.Serialization.XmlAttribute]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        [DefaultValue( typeof( DateTime), "0001-01-01T00:00:00")]
        public DateTime EndTime
        {
            get
            {
                return _EndTime;
            }
            set
            {
                _EndTime = value;
            }
        }
#if !DCWriterForWASM

        [NonSerialized]
        private bool _Visible = false;
        /// <summary>
        /// 数据点是否可见
        /// </summary>
        internal bool Visible
        {
            get
            {
                return _Visible; 
            }
            set
            {
                _Visible = value; 
            }
        }

        //[NonSerialized]
        //private float _X = float.NaN;
        ///// <summary>
        ///// 视图坐标中的X坐标值
        ///// </summary>
        //internal float X
        //{
        //    get
        //    {
        //        return _X; 
        //    }
        //    set
        //    {
        //        _X = value; 
        //    }
        //}

        //[NonSerialized]
        //private float _Y = float.NaN;
        ///// <summary>
        ///// 视图坐标中的Y坐标值
        ///// </summary>
        //internal float Y
        //{
        //    get
        //    {
        //        return _Y; 
        //    }
        //    set
        //    {
        //        _Y = value; 
        //    }
        //}

        //[NonSerialized]
        //private int _OutofRangeFlag = 0;
        /// <summary>
        /// 超出取值范围标记，伍贻超20201014补充：若为0代表数据点没有超限，若为1则代表数据点的值高出Y轴上限，若为-1则代表数据点的值低于Y轴下限
        /// </summary>
        internal int OutofRangeFlag
        {
            get
            {
                if (this.Parent != null && this.Parent is YAxisInfo)
                {
                    YAxisInfo y = this.Parent as YAxisInfo;
                    if (y.AllowOutofRange == false)
                    {
                        return 0;
                    }
                    else if (this.Value > y.MaxValue)
                    {
                        return 1;
                    }
                    else if (this.Value < y.MinValue)
                    {
                        return -1;
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    return 0;
                }
            }
            //get
            //{
            //    return _OutofRangeFlag;
            //}
            //set
            //{
            //    _OutofRangeFlag = value;
            //}
        }
#endif
        private float _Value = TemperatureDocument.InnerNullValue;
        /// <summary>
        /// 数值
        /// </summary>
        [DefaultValue(TemperatureDocument.InnerNullValue)]
        [XmlAttribute]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public float Value
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
#if !DCWriterForWASM

        /// <summary>
        /// 数值文本
        /// </summary>
        [Browsable( false )]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string ValueString
        {
            get
            {
                if (this.Parent is YAxisInfo)
                {
                    string format = ((YAxisInfo)this.Parent).ValueFormatString;
                    if (string.IsNullOrEmpty(format) == false )
                    {
                        return this.Value.ToString(format);
                    }
                }
                return this.Value.ToString();
            }
        }

        /// <summary>
        /// 判断是否为空数值
        /// </summary>
        [Browsable( false )]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public bool IsNullValue
        {
            get
            {
                return TemperatureDocument.IsNullValue(this.Value);
            }
        }

        [NonSerialized]
        private object _Parent = null;
        /// <summary>
        /// 获取或设置数据点所属的Y轴或是标题行对象
        /// </summary>
        [Browsable(false)]
        [XmlIgnore]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public object Parent
        {
            get
            {
                return _Parent; 
            }
            set
            {
                _Parent = value; 
            }
        }

        [NonSerialized]
        private float _Left = float.NaN;
        /// <summary>
        /// 数据点在视图中的X坐标
        /// </summary>
        [Browsable(false)]
        [XmlIgnore]
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

        [NonSerialized]
        private float _Top = float.NaN;
        /// <summary>
        /// 数据点在视图中的Y坐标
        /// </summary>
        [Browsable(false)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        [XmlIgnore]
        public float Top
        {
            get
            {
                return _Top;
            }
            set
            {
                //if (_Top != value)
                {
                    _Top = value;
                }
            }
        }

        [NonSerialized]
        private float _Width = 0;
        /// <summary>
        /// 视图宽度
        /// </summary>
        [Browsable(false)]
        [XmlIgnore]
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

        [NonSerialized]
        private float _Height = 0;
        /// <summary>
        /// 数据点在视图中的高度
        /// </summary>
        [Browsable( false )]
        [XmlIgnore]
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

        internal float CenterX
        {
            get
            {
                return this.Left + this.Width / 2;
            }
        }
        internal float CenterY
        {
            get
            {
                float y = this.Top + this.Height / 2;
                if (float.IsNaN(y))
                {
                }
                return y;
            }
        }
        [NonSerialized]
        private RectangleF _ViewBounds = RectangleF.Empty;
        /// <summary>
        /// 显示边界
        /// </summary>
        [Browsable(false)]
        [XmlIgnore]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public RectangleF ViewBounds
        {
            get
            {
                return _ViewBounds;
            }
            set
            {
                this._ViewBounds = value;
            }
        }


        /// <summary>
        /// 为LINUX下绘制AutoHeight数据行多行文本手动拆分数据点文本所记录的单行字符数
        /// </summary>
        [NonSerialized]
        internal int TextSplitNumForLinux = -1;

        /// <summary>
        /// 对应的阴影数据点对象
        /// </summary>
        [NonSerialized]
        internal ValuePoint ReverseShadowPoint = null;

        /// <summary>
        /// 对应的阴影数据点对象
        /// </summary>
        [NonSerialized]
        internal ValuePoint ShadowPoint = null;
        /// <summary>
        /// 绘制阴影数据点
        /// </summary>
        [XmlIgnore]
        internal bool ShowShadowPoint
        {
            get
            {
                if (this.ShadowPoint != null)
                {
                    if (this.ShadowPoint.IsNullValue == false 
                        && this.IsNullValue == false
                        && Math.Abs(this.ShadowPoint.Value - this.Value) > 0.01)
                    {
                        return true;
                    }
                }
                return false;
            }
        }
#endif
        private float _LanternValue = TemperatureDocument.InnerNullValue;
        /// <summary>
        /// 挂灯笼的数值
        /// </summary>
        [DefaultValue(TemperatureDocument.InnerNullValue)]
        [XmlAttribute]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public float LanternValue
        {
            get
            {
                return _LanternValue; 
            }
            set
            {
                _LanternValue = value; 
            }
        }

        private string _Text = null;
        /// <summary>
        /// 文本值
        /// </summary>
        [System.ComponentModel.DefaultValue( null )]
        [XmlAttribute]
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

        private StringAlignment _TextAlign = StringAlignment.Center;
        /// <summary>
        /// 文本数据行的数据点文本水平对齐方式
        /// </summary>
        [DefaultValue(StringAlignment.Center)]
        [XmlAttribute]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public StringAlignment TextAlign
        {
            get
            {
                return _TextAlign;
            }
            set
            {
                _TextAlign = value;
            }
        }


        private ValuePointUpAndDown _UpAndDown = ValuePointUpAndDown.None;
        /// <summary>
        /// 数据点垂直对齐方式
        /// </summary>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        [System.Xml.Serialization.XmlIgnore]
        [DefaultValue(ValuePointUpAndDown.None)]
        public ValuePointUpAndDown UpAndDown
        {
            get 
            { 
                return _UpAndDown; 
            }
            set 
            { 
                _UpAndDown = value; 
            }
        }
#if !DCWriterForWASM

        /// <summary>
        /// 程序指定该数据点垂直对齐方式
        /// </summary>
        [XmlIgnore]
        internal bool hasSpecifyUpDownType = false;


        private string _RuntimeText = string.Empty; 
        /// <summary>
        /// 运行时使用的文本
        /// </summary>
        internal string RuntimeText
        {
            get
            {
                string vpText = this.Text;
                TitleLineInfo info = this.Parent as TitleLineInfo;
                if (string.IsNullOrEmpty(vpText))
                {
                    if ( TemperatureDocument.IsNullValue(this.Value) == false)
                    {
                        vpText = this.Value.ToString();
                        if (info != null && string.IsNullOrEmpty(info.ValueDisplayFormat) == false)
                        {
                            vpText = this.Value.ToString(info.ValueDisplayFormat);
                        }
                    }
                }
                if(this.TextSplitNumForLinux >= 2)
                {
                    var vpTextLength = vpText.Length;
                    int textNumPerRow = vpTextLength / this.TextSplitNumForLinux;
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i <  vpTextLength; i++)
                    {
                        sb.Append(vpText[i]);
                        if( (i % textNumPerRow )== 0 && i > 0 && i < vpTextLength - 1)
                        {
                            sb.AppendLine();
                        }
                        //if (i % textNumPerRow == 0 && i >0)//!= vpText.Length)
                        //{
                        //    sb.Append(Environment.NewLine);
                        //}
                    }
                    vpText = sb.ToString();
                }
                ////////////////////////////////////////////////////////////////////
                return vpText;
            }
            //set
            //{
            //    this._RuntimeText = value;
            //}
        }
#endif
        private Color _Color = Color.Transparent;
        /// <summary>
        /// 颜色值
        /// </summary>
        [XmlIgnore]
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
        /// 文本形式的颜色值
        /// </summary>
        [Browsable(false)]
        [DefaultValue(null)]
        [System.Xml.Serialization.XmlAttribute]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string ColorValue
        {
            get
            {
                return XMLSerializeHelper.ColorToString(this.Color, Color.Transparent);
            }
            set
            {
                this.Color = XMLSerializeHelper.StringToColor(value, Color.Transparent);
            }
        }

        private Color _TextColor = Color.Empty;
        /// <summary>
        /// 数据点自身的文本颜色值
        /// </summary>
        [XmlIgnore]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public Color TextColor
        {
            get
            {
                return _TextColor;
            }
            set
            {
                _TextColor = value;
            }
        }

        /// <summary>
        /// 文本形式的颜色值
        /// </summary>
        [Browsable(false)]
        [DefaultValue("#00000000")]
        [System.Xml.Serialization.XmlAttribute]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string TextColorValue
        {
            get
            {
                return XMLSerializeHelper.ColorToString(this.TextColor, Color.Empty);
            }
            set
            {
                this.TextColor = XMLSerializeHelper.StringToColor(value, Color.Empty);
            }
        }
#if !DCWriterForWASM

        /// <summary>
        /// 返回表示对象数据的字符串
        /// </summary>
        /// <returns>字符串</returns>
        public override string ToString()
        {
            return this.Time.ToString("yyyy-MM-dd HH:mm:ss") + "#" + this.RuntimeText;
        }

        /// <summary>
        /// 判断是否具有阴影图形
        /// </summary>
        [NonSerialized]
        internal bool HasShadowShape = false;
        [NonSerialized]
        private object _DataBoundItem = null;
        /// <summary>
        /// 绑定的数据源对象
        /// </summary>
        [Browsable( false )]
        [XmlIgnore]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public object DataBoundItem
        {
            get
            {
                return _DataBoundItem; 
            }
            set
            {
                _DataBoundItem = value; 
            }
        }

        [NonSerialized]
        internal ValuePoint _CoincidePoint = null;
        /// <summary>
        /// 获取此刻与该数据点重合的数据点，若没有重合点则为空
        /// </summary>
        [Browsable(false)]
        [XmlIgnore]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public ValuePoint CoincidePoint
        {
            get
            {
                return _CoincidePoint;
            }
        }

        [NonSerialized]
        internal ValuePoint _ConveredPoint = null;
        /// <summary>
        /// 获取此刻被重合的数据点，若没有重合点则为空
        /// </summary>
        [Browsable(false)]
        [XmlIgnore]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public ValuePoint ConveredPoint
        {
            get
            {
                return _ConveredPoint;
            }
        }
#endif
        /// <summary>
        /// 复制对象
        /// </summary>
        /// <returns>复制品</returns>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public ValuePoint Clone()
        {
            return (ValuePoint)this.MemberwiseClone();
        }

        //改造时间轴代码所新增的临时性保存变量
#if !DCWriterForWASM


        [XmlIgnore]
        internal int _TickIndexForDraw = 0;

        /// <summary>
        /// 绘制数据行数据点所需的计算出来的区域
        /// </summary>
        [XmlIgnore]
        internal RectangleF _OriginalBlockBounds = RectangleF.Empty;
        internal void WriteJson(DCFastJsonTextWriter writer)
        {
            writer.WritePropertyNoFixName("Value", this.Value.ToString());
            writer.WritePropertyNoFixName("LanternValue", this.LanternValue.ToString());
            writer.WritePropertyNoFixName("Time", this.Time.ToString("yyyy-MM-dd HH:mm:ss"));
            writer.WritePropertyNoFixName("EndTime", this.EndTime.ToString("yyyy-MM-dd HH:mm:ss"));
            writer.WritePropertyNoFixName("Link", this.Link);
            writer.WritePropertyNoFixName("Text", this.Text);
            writer.WritePropertyNoFixName("Title", this.Title);
            writer.WritePropertyNoFixName("ID", this.ID);
            writer.WritePropertyNoFixName("LineStop", this.LineStop.ToString());
            writer.WritePropertyNoFixName("ColorValue", this.ColorValue);
            writer.WritePropertyNoFixName("TextColorValue", this.TextColorValue);
            writer.WritePropertyNoFixName("TextAlign", this.TextAlign.ToString());
            writer.WritePropertyNoFixName("SpecifySymbolStyle", this.SpecifySymbolStyle.ToString());
            writer.WritePropertyNoFixName("CharSymbol", this.CharacterForCharSymbolStyle.ToString());
            writer.WritePropertyNoFixName("CharLantern", this.CharacterForLanternSymbolStyle.ToString());
            writer.WritePropertyNoFixName("TextFontName", this.Font != null ? this.Font.Name : null);
            writer.WritePropertyNoFixName("TextFontSize", this.Font != null ? this.Font.Size.ToString() : null);
            writer.WritePropertyNoFixNameBoolean("UseAdvVerticalStyle", this.UseAdvVerticalStyle);
            writer.WritePropertyNoFixNameBoolean("UseAdvVerticalStyle2", this.UseAdvVerticalStyle2);
            writer.WritePropertyNoFixNameBoolean("Verified", this.Verified);
            writer.WritePropertyNoFixName("ShowPointValue", this.ShowPointValue.ToString());

            //临时诊断信息
            string info = "l" + this.Left.ToString() + "t" + this.Top.ToString() + "w" + this.Width.ToString() + "h" + this.Height.ToString();
            writer.WritePropertyNoFixName("size", info);
        }
#endif
    }
    /// <summary>
    /// 数据点对象列表
    /// </summary>

#if !DCWriterForWASM
    [Serializable]
    [System.Diagnostics.DebuggerDisplay("Count={ Count }")]
    [System.Diagnostics.DebuggerTypeProxy(typeof(DCSoft.Common.ListDebugView))]
    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = false )]
#endif
    public partial class ValuePointList : List<ValuePoint>
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public ValuePointList()
        {
        }
#if !DCWriterForWASM


        internal void CheckSortInvalidate()
        {
            if (this._SortInvalidate)
            {
                SortByTime();
            }
        }

        private bool _SortInvalidate = true;
        /// <summary>
        /// 排序状态无效标记
        /// </summary>
        internal bool SortInvalidate
        {
            get
            {
                return _SortInvalidate;
            }
            set
            {
                _SortInvalidate = value;
            }
        }
        /// <summary>
        /// 判断列表中的数据是否为文本类型的数据
        /// </summary>
        internal bool IsTextMode(TitleLineInfo info)
        {

            int txtCount = 0;
            int vCount = 0;
            foreach (ValuePoint vp in this)
            {
                if (string.IsNullOrEmpty(vp.Text) == false)
                {
                    txtCount++;
                    if (txtCount > 5)
                    {
                        return true;
                    }
                }
                else if (TemperatureDocument.IsNullValue(vp.Value) == false)
                {
                    vCount++;
                    if (vCount > 5)
                    {
                        return false;
                    }
                }
            }//foreach
            if (txtCount > 0 && vCount > 0)
            {
                return txtCount > vCount;
            }
            if (info != null)
            {
                if (string.IsNullOrEmpty(info.ValueDisplayFormat) == false)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            return true;
        }

        /// <summary>
        /// 获得前一个数据点对象
        /// </summary>
        /// <param name="vp">数据点对象</param>
        /// <returns>获得的数据点对象</returns>
        public ValuePoint GetPrePoint(ValuePoint vp)
        {
            int index = this.IndexOf(vp);
            if (index > 0)
            {
                return this[index - 1];
            }
            return null;
        }
        /// <summary>
        /// 获得下一个数据点对象
        /// </summary>
        /// <param name="vp">数据点对象</param>
        /// <returns>获得的数据点对象</returns>
        public ValuePoint GetNextPoint(ValuePoint vp)
        {
            int index = this.IndexOf(vp);
            if (index >= 0 && index < this.Count - 2)
            {
                return this[index + 1];
            }
            return null;
        }

        /// <summary>
        /// 根据时间和数据添加项目
        /// </summary>
        /// <param name="dtm">时间</param>
        /// <param name="Value">数据</param>
        /// <returns>添加的项目</returns>
        public ValuePoint AddByTimeValue(DateTime dtm, float Value)
        {
            ValuePoint vp = new ValuePoint();
            vp.Time = dtm;
            vp.Value = Value;
            this.Add(vp);
            this._SortInvalidate = true;
            return vp;
        }

        /// <summary>
        /// 根据时间和文本添加项目
        /// </summary>
        /// <param name="dtm">时间</param>
        /// <param name="Text">文本</param>
        /// <returns>添加的项目</returns>
        public ValuePoint AddByTimeText(DateTime dtm, string Text )
        {
            ValuePoint vp = new ValuePoint();
            vp.Time = dtm;
            vp.Text = Text;
            this.Add(vp);
            this._SortInvalidate = true;
            return vp;
        }

        /// <summary>
        /// 根据时间和文本添加项目
        /// </summary>
        /// <param name="dtm">时间</param>
        /// <param name="Text">文本</param>
        /// <param name="Value">数值</param>
        /// <returns>添加的项目</returns>
        public ValuePoint AddByTimeTextValue(DateTime dtm, string Text , float Value )
        {
            ValuePoint vp = new ValuePoint();
            vp.Time = dtm;
            vp.Text = Text;
            vp.Value = Value;
            this.Add(vp);
            this._SortInvalidate = true;
            return vp;
        }

        /// <summary>
        /// 根据时间、数据和灯笼数据添加项目
        /// </summary>
        /// <param name="dtm">时间</param>
        /// <param name="Value">数据</param>
        /// <param name="landernValue">灯笼数据</param>
        /// <returns>新增的项目对象</returns>
        public ValuePoint AddByTimeValueLandernValue(DateTime dtm, float Value, float landernValue)
        {
            ValuePoint vp = new ValuePoint();
            vp.Time = dtm;
            vp.Value = Value;
            vp.LanternValue = landernValue;
            this.Add(vp);
            this._SortInvalidate = true;
            return vp;
        }
        internal void BindingDataSource(
            object datasource,
            string timeFieldName,
            string textFieldName,
            string valueFieldName,
            string lanternFieldName)
        {
            this.Clear();
            if (datasource == null)
            {
                return;
            }
            if (string.IsNullOrEmpty(timeFieldName))
            {
                return;
            }
            if (string.IsNullOrEmpty(valueFieldName) && string.IsNullOrEmpty(textFieldName))
            {
                return;
            }
            DCDataSource ds = new DCDataSource(datasource);
            ds.Start();
            while (ds.MoveNext())
            {
                ValuePoint p = new ValuePoint();
                p.Value = float.NaN;
                p.Time = Convert.ToDateTime(ds.ReadValue(timeFieldName));
                p.DataBoundItem = ds.Current;
                //if (textMode)
                //{
                //    p.Text = Convert.ToString(ds.ReadValue(valueFieldName));
                //    p.Value = 100;
                //}
                //else
                //{
                //    p.Value = ToSingleValue(ds.ReadValue(valueFieldName));
                //}
                if (string.IsNullOrEmpty(textFieldName) == false)
                {
                    p.Text = Convert.ToString(ds.ReadValue(textFieldName));
                }
                if (string.IsNullOrEmpty(valueFieldName) == false)
                {
                    p.Value = ToSingleValue(ds.ReadValue(valueFieldName));
                }
                if (string.IsNullOrEmpty(lanternFieldName) == false)
                {
                    p.LanternValue = ToSingleValue(ds.ReadValue(lanternFieldName));
                }
                this.Add(p);
            }//while
        }

        private float ToSingleValue(object v)
        {
            if (v == null || DBNull.Value.Equals(v))
            {
                return TemperatureDocument.NullValue;
            }
            else
            {
                try
                {
                    float fv = Convert.ToSingle(v);
                    if (float.IsNaN(fv))
                    {
                        return TemperatureDocument.NullValue;
                    }
                    else
                    {
                        return fv;
                    }
                }
                catch
                {
                    return TemperatureDocument.NullValue;
                }
            }
        }
#endif
#if !DCWriterForWASM

        /// <summary>
        /// 最大时间
        /// </summary>
        public DateTime MaxDate
        {
            get
            {
                if (this.Count > 0)
                {
                    DateTime dtm = this[this.Count - 1].Time;
                    DateTime dtm2 = this[this.Count - 1].EndTime;
                    if (TemperatureDocument.IsNullDate(dtm2) == false )
                    {
                        if (dtm2 > dtm)
                        {
                            return dtm2;
                        }
                    }
                    return dtm;
                }
                return TemperatureDocument.NullDate;
            }
        }

        /// <summary>
        /// 最小时间
        /// </summary>
        public DateTime MinDate
        {
            get
            {
                if (this.Count > 0)
                {
                    return this[0].Time;
                }
                return TemperatureDocument.NullDate;
            }
        }

#endif
#if !DCWriterForWASM
        /// <summary>
        /// 根据数据时间进行元素排序
        /// </summary>
        public void SortByTime()
        {
            base.Sort(new TimeComparer());
#if !DCWriterForWASM

            this._SortInvalidate = false ;
#endif
        }

        private class TimeComparer : IComparer<ValuePoint>
        {
            public int Compare(ValuePoint x, ValuePoint y)
            {
                return DateTime.Compare(x.Time, y.Time);
            }
        }

        /// <summary>
        /// 根据数据点X坐标与数据点Y轴坐标值进行排序
        /// </summary>
        public void SortByLeftValue()
        {
            base.Sort(new LeftValueComparer());
            this._SortInvalidate = false;
        }

        private class LeftValueComparer : IComparer<ValuePoint>
        {
            public int Compare(ValuePoint x, ValuePoint y)
            {
                if (x.Left != y.Left)
                {
                    return x.Left.CompareTo(y.Left);
                }
                else if (x.Value == y.Value)
                {
                    return DateTime.Compare(x.Time, y.Time);
                }
                else
                {
                    return x.Value.CompareTo(y.Value) * -1;
                }
            }
        }

        internal ValuePoint GetNearestPoint(DateTime dtm, float maxSecondSpan)
        {
            int index = GetNearestPointIndex(dtm, maxSecondSpan);
            if (index >= 0)
            {
                return this[index];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获得时间小于等于指定时间的最大的数据点序号
        /// </summary>
        /// <param name="dtm"></param>
        /// <returns></returns>
        internal int GetFloorIndexByTime(DateTime dtm)
        {
            CheckSortInvalidate();
            if (this.Count == 0)
            {
                return -1;
            }
            if (dtm < this.MinDate)
            {
                return -1;
            }
            if (dtm == this.MinDate)
            {
                return 0;
            }
            if (dtm >= this.MaxDate)
            {
                return this.Count - 1;
            }
            //int num = (int)Math.Ceiling(Math.Log(this.Count, 2)) - 1;
            int startIndex = 0;
            int endIndex = this.Count - 1;
            // 首先使用二分法快速缩小搜索范围
            while( endIndex - startIndex > 10 )
            //for (int iCount = 0; iCount < num; iCount++)
            {
                int index = (startIndex + endIndex) / 2;
                if (this[index].Time > dtm)
                {
                    endIndex = index;
                }
                else
                {
                    startIndex = index;
                }
            }//for
            for (int iCount = endIndex; iCount >= startIndex; iCount--)
            {
                if (this[iCount].Time <= dtm)
                {
                    // 命中
                    return iCount;
                }
            }
            return startIndex;
        }
        internal int GetNearestPointIndex(DateTime dtm , float maxSecondSpan )
        {
            if (this.Count == 0)
            {
                return -1;
            }
            if (dtm <= this.MinDate)
            {
                if (dtm >= this.MinDate.AddSeconds(-maxSecondSpan))
                {
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
            if (dtm >= this.MaxDate)
            {
                if (dtm <= this.MaxDate.AddSeconds(maxSecondSpan))
                {
                    return this.Count - 1;
                }
                else
                {
                    return -1;
                }
            }
            //int num = (int)Math.Ceiling(Math.Log(this.Count, 2)) - 1 ;
            int startIndex = 0;
            int endIndex = this.Count -1 ;
            // 首先使用二分法快速缩小搜索范围
            int loopCount = 0;
            while (endIndex - startIndex > 10)
            {
                loopCount++;
                int index = (startIndex + endIndex) / 2;
                if (this[index].Time > dtm)
                {
                    endIndex = index;
                }
                else
                {
                    startIndex = index;
                }
            }//for
            long maxTick = long.MaxValue;
            int resultIndex = startIndex;
            for (int iCount = startIndex; iCount <= endIndex; iCount++)
            {
                loopCount++;
                long tick = Math.Abs(this[iCount].Time.Ticks - dtm.Ticks);
                if (tick == 0)
                {
                    // 直接命中
                    return iCount;
                }
                if (tick < maxTick)
                {
                    maxTick = tick;
                    resultIndex = iCount;
                }
            }
            if (float.IsNaN(maxSecondSpan) == false)
            {
                DateTime dtm2 = this[resultIndex].Time;
                TimeSpan span = dtm2 - dtm;
                if (Math.Abs(span.TotalSeconds) > maxSecondSpan)
                {
                    // 超出允许范围了
                    return -1;
                }
            }
            return resultIndex;
        }
#endif

        

        /// <summary>
        /// 复制对象
        /// </summary>
        /// <returns>复制品</returns>
        public ValuePointList Clone()
        {
            ValuePointList list = new ValuePointList();
            foreach (ValuePoint p in this)
            {
                list.Add(p.Clone());
            }
            return list;
        }

    }
    
 
}
