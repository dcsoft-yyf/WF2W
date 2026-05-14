using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace DCSoft.TemperatureChart
{
    /// <summary>
    /// 文档鼠标移动事件委托类型
    /// </summary>
    /// <param name="eventSender"></param>
    /// <param name="args"></param>
    [System.Runtime.InteropServices.ComVisible(true)]
    [System.Runtime.InteropServices.Guid("18C9718F-11E0-4981-9512-41733306CDF7")]
    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
    public delegate void DocumentMouseMoveEventHandler(
        object eventSender,
        DocumentMouseMoveEventArgs args);

    /// <summary>
    /// 文档双击事件参数
    /// </summary>
    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = false)]
    public partial class DocumentMouseMoveEventArgs
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        /// <param name="doc">文档对象</param>
        public DocumentMouseMoveEventArgs(
            TemperatureDocument doc,
            MouseButtons button,
            DateTime dt,
            float value,
            Point p,
            TitleLineInfo info)
        {
            this._Button = button;
            this._DateTime = dt;
            this._Value = value;
            this._Document = doc;
            this._Location = p;
            this._TitleLineInfo = info;
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

        private MouseButtons _Button = MouseButtons.Left;
        /// <summary>
        /// 获取当前鼠标按键信息
        /// </summary>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public MouseButtons Button
        {
            get
            {
                return _Button;
            }
        }

        private Point _Location = Point.Empty;
        /// <summary>
        /// 获取当前鼠标位置信息
        /// </summary>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public Point Location
        {
            get
            {
                return _Location;
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
    }
}


