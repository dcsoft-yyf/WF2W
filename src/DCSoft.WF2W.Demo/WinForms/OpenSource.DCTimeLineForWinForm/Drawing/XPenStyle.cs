using System;
using System.Text;
using System.ComponentModel;
using System.Drawing.Design;
using System.Drawing ;
using System.Drawing.Drawing2D ;
using System.Collections ;
using System.Collections.Generic ;

namespace DCSoft.Drawing
{
    /// <summary>
    /// 画笔样式信息对象,本对象可以参与XML序列化和反序列化
    /// </summary>
    [System.ComponentModel.TypeConverter(typeof(XPenStyleTypeConverter))]
#if WINFORM || DCWriterForWinFormNET6
    [System.ComponentModel.Editor( typeof( XPenStyleTypeEditor ) , typeof( UITypeEditor ))]
#endif
    [Serializable()]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = false )]
    [DCSoft.Common.DCXSD("PenSettings")]
    public partial class XPenStyle : System.ICloneable, IComponent
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public XPenStyle()
        {
        }

        /// <summary>
        /// 初始化对象
        /// </summary>
        /// <param name="color">颜色</param>
        public XPenStyle(Color color)
        {
            _Color = color;
        }

        /// <summary>
        /// 初始化对象
        /// </summary>
        /// <param name="color">颜色</param>
        /// <param name="width">线条宽度</param>
        public XPenStyle(Color color, float width)
        {
            this._Color = color;
            this._Width = width;
        }

        /// <summary>
        /// 初始化对象
        /// </summary>
        /// <param name="color">颜色</param>
        /// <param name="width">线条宽度</param>
        /// <param name="dashStyle">虚线样式</param>
        public XPenStyle(Color color, float width, DashStyle dashStyle)
        {
            _Color = color;
            _Width = width;
            _DashStyle = dashStyle;
        }


        private System.Drawing.Color _Color = System.Drawing.Color.Black;
        /// <summary>
        /// 画笔颜色
        /// </summary>
        [System.ComponentModel.Category("Appearance")]
        [DefaultColorValue("Black" )]
        [System.Xml.Serialization.XmlIgnore()]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public System.Drawing.Color Color
        {
            get 
            {
                return _Color; 
            }
            set
            {
                _Color = value;
                _Value = null;
            }
        }

        /// <summary>
        /// 画笔颜色文本值
        /// </summary>
        [System.ComponentModel.Browsable( false )]
        [System.Xml.Serialization.XmlElement("Color")]
        [DefaultValue(null)]
        [DCSoft.Common.DCXSD( Common.DCXSDOutputType.Attribute)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string ColorString
        {
            get
            {
                return DCSoft.Common.XMLSerializeHelper.ColorToString(this.Color, Color.Black);
            }
            set
            {
                this.Color = DCSoft.Common.XMLSerializeHelper.StringToColor(value, Color.Black);
                _Value = null;
            }
        }

        private float _Width = 1f;
        /// <summary>
        /// 线条宽度
        /// </summary>
        [System.ComponentModel.Category("Appearance")]
        [System.ComponentModel.DefaultValue(1f)]
        [DCSoft.Common.DCXSD( Common.DCXSDOutputType.Attribute)]
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
                _Value = null;
            }
        }

        private System.Drawing.Drawing2D.DashStyle _DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
        /// <summary>
        /// 线条虚线样式
        /// </summary>
        [System.ComponentModel.Category("Appearance")]
        [System.ComponentModel.DefaultValue(System.Drawing.Drawing2D.DashStyle.Solid)]
#if WINFORM || DCWriterForWinFormNET6
        [System.ComponentModel.Editor("DCSoft.WinForms.Design.DashStyleEditor", typeof( System.Drawing.Design.UITypeEditor ))]
#endif
        [DCSoft.Common.DCXSD( Common.DCXSDOutputType.Attribute)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public System.Drawing.Drawing2D.DashStyle DashStyle
        {
            get 
            {
                return _DashStyle; 
            }
            set 
            {
                if (value != System.Drawing.Drawing2D.DashStyle.Custom)
                {
                    _DashStyle = value;
                    _Value = null;
                }
            }
        }

        private System.Drawing.Drawing2D.DashCap _DashCap = System.Drawing.Drawing2D.DashCap.Flat;
        /// <summary>
        /// 虚线帽样式
        /// </summary>
        [System.ComponentModel.Category("Appearance")]
        [System.ComponentModel.DefaultValue(System.Drawing.Drawing2D.DashCap.Flat )]
#if WINFORM || DCWriterForWinFormNET6
        [System.ComponentModel.Editor( "DCSoft.Editor.DashCapEditor" , typeof( System.Drawing.Design.UITypeEditor ))]
#endif
        [DCSoft.Common.DCXSD( Common.DCXSDOutputType.Attribute)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public System.Drawing.Drawing2D.DashCap DashCap
        {
            get
            {
                return _DashCap; 
            }
            set
            {
                _DashCap = value;
                _Value = null;
            }
        }

        private System.Drawing.Drawing2D.LineJoin _LineJoin = System.Drawing.Drawing2D.LineJoin.Bevel;
        /// <summary>
        /// 线段连接处样式
        /// </summary>
        [System.ComponentModel.Category("Appearance")]
        [System.ComponentModel.DefaultValue(System.Drawing.Drawing2D.LineJoin.Bevel )]
        [DCSoft.Common.DCXSD( Common.DCXSDOutputType.Attribute)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public System.Drawing.Drawing2D.LineJoin LineJoin
        {
            get 
            {
                return _LineJoin; 
            }
            set
            {
                _LineJoin = value;
                _Value = null;
            }
        }

        private System.Drawing.Drawing2D.LineCap _StartCap = System.Drawing.Drawing2D.LineCap.Flat;
        /// <summary>
        /// 线段起点箭头样式
        /// </summary>
        [System.ComponentModel.Category("Appearance")]
        [System.ComponentModel.DefaultValue(System.Drawing.Drawing2D.LineCap.Flat)]
#if WINFORM || DCWriterForWinFormNET6
        [System.ComponentModel.Editor("DCSoft.Editor.LineCapEditor", typeof(System.Drawing.Design.UITypeEditor))]
#endif
        [DCSoft.Common.DCXSD( Common.DCXSDOutputType.Attribute)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public System.Drawing.Drawing2D.LineCap StartCap
        {
            get
            {
                return _StartCap; 
            }
            set
            {
                if (value != System.Drawing.Drawing2D.LineCap.Custom)
                {
                    _StartCap = value;
                    _Value = null;
                }
            }
        }

        private System.Drawing.Drawing2D.LineCap _EndCap = System.Drawing.Drawing2D.LineCap.Flat;
        /// <summary>
        /// 线段终点箭头样式
        /// </summary>
        [System.ComponentModel.Category("Appearance")]
        [System.ComponentModel.DefaultValue(System.Drawing.Drawing2D.LineCap.Flat)]
#if WINFORM || DCWriterForWinFormNET6
        [System.ComponentModel.Editor("DCSoft.Editor.LineCapEditor", typeof(System.Drawing.Design.UITypeEditor))]
#endif
        [DCSoft.Common.DCXSD( Common.DCXSDOutputType.Attribute)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public System.Drawing.Drawing2D.LineCap EndCap
        {
            get 
            {
                return _EndCap; 
            }
            set
            {
                if (value != System.Drawing.Drawing2D.LineCap.Custom)
                {
                    _EndCap = value;
                    _Value = null;
                }
            }
        }

        private float _MiterLimit = 10f;
        /// <summary>
        /// 参数
        /// </summary>
        [DefaultValue( 10f)]
        [DCSoft.Common.DCXSD( Common.DCXSDOutputType.Attribute)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public float MiterLimit
        {
            get
            {
                return _MiterLimit; 
            }
            set
            {
                if (_MiterLimit != value)
                {
                    _MiterLimit = value;
                    _Value = null;
                }
            }
        }

        private PenAlignment _Alignment = PenAlignment.Center;
        /// <summary>
        /// 对齐方式
        /// </summary>
        [DefaultValue( PenAlignment.Center )]
        [DCSoft.Common.DCXSD( Common.DCXSDOutputType.Attribute)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public PenAlignment Alignment
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
        private static readonly Dictionary<string, Pen> _Buffer = new Dictionary<string, Pen>();

        [NonSerialized]
        private Pen _Value = null;
        /// <summary>
        /// 画笔对象
        /// </summary>
        [Browsable(false)]
        [System.Xml.Serialization.XmlIgnore()]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public Pen Value
        {
            get
            {
                if (_Value == null)
                {
                    string key = string.Concat(new string[]{
                        this.Color.ToArgb().ToString(),
                        this.DashCap.ToString(),
                        this.Width.ToString(),
                        this.DashStyle.ToString(),
                        this.StartCap.ToString(),
                        this.EndCap.ToString() ,
                        this.MiterLimit.ToString(),
                        this.Alignment.ToString()});
                    lock (_Buffer)
                    {
                        if (_Buffer.TryGetValue(key, out this._Value) == false)
                        {
                            if (_Buffer.Count > 100)
                            {
                                // 缓存的画笔太多，清空掉
                                foreach (Pen p2 in _Buffer.Values)
                                {
                                    p2.Dispose();
                                }
                                _Buffer.Clear();
                            }
                            Pen p = this.CreatePen();
                            _Buffer[key] = p;
                            this._Value = p;
                        }
                    }
                }
                return _Value;
            }
        }

        //public void DrawPath(System.Drawing.Graphics g, System.Drawing.Drawing2D.GraphicsPath path)
        //{
        //    using (System.Drawing.Pen p = this.CreatePen())
        //    {
        //        g.DrawPath(p, path);
        //    }
        //}

        /// <summary>
        /// 根据设置创建画笔对象
        /// </summary>
        /// <returns>创建的画笔对象</returns>
        public System.Drawing.Pen CreatePen()
        {
            System.Drawing.Pen p = new System.Drawing.Pen(this.Color, this.Width);
            p.DashStyle = this.DashStyle;
            p.DashCap = this.DashCap;
#if ! DCWriterForWASM
            p.LineJoin = this.LineJoin;
            p.StartCap = this.StartCap;
            p.EndCap = this.EndCap;
            p.MiterLimit = this.MiterLimit;
            p.Alignment = this.Alignment;
#endif
            return p;
        }
        /// <summary>
        /// 复制对象
        /// </summary>
        /// <returns>复制品</returns>
        object System.ICloneable.Clone()
        {
            XPenStyle style = (XPenStyle)this.MemberwiseClone();
            return style;
        }

        /// <summary>
        /// 复制对象
        /// </summary>
        /// <returns>复制品</returns>
        public XPenStyle Clone()
        {
            return (XPenStyle)((System.ICloneable)this).Clone();
        }

        /// <summary>
        /// 返回表示对象的字符串
        /// </summary>
        /// <returns>字符串</returns>
        public override string ToString()
        {
            System.Drawing.ColorConverter cc = new System.Drawing.ColorConverter();
            System.Text.StringBuilder str = new StringBuilder();
            str.Append(_DashStyle.ToString());
            str.Append(" " + System.Drawing.ColorTranslator.ToHtml(_Color));
            str.Append(" " + _Width);
            if (this.MiterLimit != 10f)
            {
                str.Append(" MiterLimit:" + this.MiterLimit);
            }
            if (this.Alignment != PenAlignment.Center)
            {
                str.Append(" " + this.Alignment);
            }
            return str.ToString();
        }

#region IComponent 成员

        /// <summary>
        /// 对象销毁事件
        /// </summary>
        [field:NonSerialized]
        public event EventHandler Disposed = null;

        [NonSerialized]
        private ISite _Site = null;
        /// <summary>
        /// 对象站点
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        [System.Xml.Serialization.XmlIgnore()]
        [System.ComponentModel.DesignerSerializationVisibility(
            DesignerSerializationVisibility.Hidden)]
        public ISite Site
        {
            get
            {
                return _Site;
            }
            set
            {
                _Site = value;
            }
        }

#endregion

#region IDisposable 成员

        /// <summary>
        /// 销毁对象
        /// </summary>
        public void Dispose()
        {
            if (Disposed != null)
            {
                Disposed(this, EventArgs.Empty);
            }
        }

#endregion
    }

    
    /// <summary>
    /// 用于XPenStyle的类型转换器
    /// </summary>
    [System.Runtime.InteropServices.ComVisible(false)]
    public class XPenStyleTypeConverter : TypeConverter
    {
        /// <summary>
        /// 获得对象属性
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="value">数值</param>
        /// <param name="attributes">特性</param>
        /// <returns>获得的属性列表</returns>
        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            return TypeDescriptor.GetProperties(typeof(XPenStyle), attributes);
        }
        /// <summary>
        /// 是否支持获得对象属性
        /// </summary>
        /// <param name="context">上下文</param>
        /// <returns>支持获得对象数据</returns>
        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
    }
}
