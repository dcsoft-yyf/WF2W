using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DCSoft.TemperatureChart
{
    /// <summary>
    /// 文档双击事件委托类型
    /// </summary>
    /// <param name="eventSender"></param>
    /// <param name="args"></param>
    [System.Runtime.InteropServices.ComVisible( true )]
    [System.Runtime.InteropServices. Guid("0CF9FF68-CBD0-4FF2-B632-5FD2F6C9989B")]
    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true )]
    public delegate void DocumentDblClickEventHandler(
        object eventSender , 
        DocumentDblClickEventArgs args );

    /// <summary>
    /// 文档双击事件参数
    /// </summary>
    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = false )]
    public partial class DocumentDblClickEventArgs  
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        /// <param name="doc">文档对象</param>
        public DocumentDblClickEventArgs(
            TemperatureDocument doc,
            MouseButtons button,
            DateTime dt,
            float value,
            ValuePoint vp,
            TitleLineInfo info,
            DCTimeLineLabel label)
        {
            this._Button = button;
            this._DateTime = dt;
            this._Value = value;
            this._Document = doc;
            this._ValuePoint = vp;
            this._TitleLineInfo = info;
            this._TimeLineLabel = label;
        }

        private TemperatureDocument _Document = null;
        /// <summary>
        /// 文档对象
        /// </summary>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public TemperatureDocument Document
        {
            get
            {
                return _Document; 
            }
        }

        private MouseButtons _Button = MouseButtons.Left;
        /// <summary>
        /// 获取当前鼠标信息
        /// </summary>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public MouseButtons Button
        {
            get
            {
                return _Button;
            }
        }

        private DateTime _DateTime = DateTime.MinValue;
        /// <summary>
        /// 获取当前鼠标位置与刻度换算的对应时间
        /// </summary>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public DateTime DateTime
        {
            get
            {
                return _DateTime;
            }
        }

        private float _Value = float.NaN;
        /// <summary>
        /// 获取当前鼠标位置与选中的Y轴换算的对应Y轴数值
        /// </summary>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public float Value
        {
            get
            {
                return _Value;
            }
        }

        private ValuePoint _ValuePoint = null;
        /// <summary>
        /// 获取当前鼠标位置点击获取到的数据点对象
        /// </summary>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public ValuePoint ValuePoint
        {
            get
            {
                return _ValuePoint;
            }
        }

        private TitleLineInfo _TitleLineInfo = null;
        /// <summary>
        /// 获取当前鼠标位置点击获取到的数据行对象
        /// </summary>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public TitleLineInfo TitleLineInfo
        {
            get
            {
                return _TitleLineInfo;
            }
        }

        private DCTimeLineLabel _TimeLineLabel = null;
        /// <summary>
        /// 获取当前鼠标位置点击获取到的文本标签对象
        /// </summary>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public DCTimeLineLabel TimeLineLabel
        {
            get
            {
                return _TimeLineLabel;
            }
        }
    }








    /// <summary>
    /// 文档单击事件委托类型
    /// </summary>
    /// <param name="eventSender"></param>
    /// <param name="args"></param>
    [System.Runtime.InteropServices.ComVisible(true)]
    [System.Runtime.InteropServices.Guid("3F2630A6-26AE-4687-80EE-B032B4B260F9")]
    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
    public delegate void DocumentClickEventHandler(
        object eventSender,
        DocumentClickEventArgs args);

    /// <summary>
    /// 文档双击事件参数
    /// </summary>
    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = false )]
    public partial class DocumentClickEventArgs  
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        /// <param name="doc">文档对象</param>
        public DocumentClickEventArgs(
            TemperatureDocument doc,
            MouseButtons button,
            DateTime dt,
            float value,
            ValuePoint vp,
            TitleLineInfo info,
            DCTimeLineLabel label)
        {
            this._Button = button;
            this._DateTime = dt;
            this._Value = value;
            this._Document = doc;
            this._ValuePoint = vp;
            this._TitleLineInfo = info;
            this._TimeLineLabel = label;
        }

        private readonly TemperatureDocument _Document = null;
        /// <summary>
        /// 文档对象
        /// </summary>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public TemperatureDocument Document
        {
            get
            {
                return _Document;
            }
        }

        private readonly MouseButtons _Button = MouseButtons.Left;
        /// <summary>
        /// 获取当前鼠标信息
        /// </summary>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public MouseButtons Button
        {
            get
            {
                return _Button;
            }
        }

        private readonly DateTime _DateTime = DateTime.MinValue;
        /// <summary>
        /// 获取当前鼠标位置与刻度换算的对应时间
        /// </summary>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public DateTime DateTime
        {
            get
            {
                return _DateTime;
            }
        }

        private readonly float _Value = float.NaN;
        /// <summary>
        /// 获取当前鼠标位置与选中的Y轴换算的对应Y轴数值
        /// </summary>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public float Value
        {
            get
            {
                return _Value;
            }
        }

        private readonly ValuePoint _ValuePoint = null;
        /// <summary>
        /// 获取当前鼠标位置点击获取到的数据点对象
        /// </summary>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public ValuePoint ValuePoint
        {
            get
            {
                return _ValuePoint;
            }
        }

        private readonly TitleLineInfo _TitleLineInfo = null;
        /// <summary>
        /// 获取当前鼠标位置点击获取到的数据行对象
        /// </summary>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public TitleLineInfo TitleLineInfo
        {
            get
            {
                return _TitleLineInfo;
            }
        }

        private DCTimeLineLabel _TimeLineLabel = null;
        /// <summary>
        /// 获取当前鼠标位置点击获取到的文本标签对象
        /// </summary>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public DCTimeLineLabel TimeLineLabel
        {
            get
            {
                return _TimeLineLabel;
            }
        }
    }
}


