using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;

namespace DCSoft.TemperatureChart
{
    /// <summary>
    /// 消息列表
    /// </summary>
    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = false )]
     
    [System.Runtime.InteropServices.ComVisible(false)]
    public class DCTimeLineControlEventMessageManager
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public DCTimeLineControlEventMessageManager()
        {
        }

        private readonly List<DCTimeLineControlEventMessage> _Items = new List<DCTimeLineControlEventMessage>();

        private DCTimeLineControlEventMessage _LastEventMessage = null;
        /// <summary>
        /// 最后一次获得的事件消息对象
        /// </summary>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
         
        public DCTimeLineControlEventMessage LastEventMessage
        {
            get
            {
                return _LastEventMessage;
            }
        }

        internal void ClearLastEventMessage()
        {
            this._LastEventMessage = null;
        }

        /// <summary>
        /// 清空超时的消息列表
        /// </summary>
        internal void ClearTimeoutEventMessage()
        {
            if (_LastEventMessage != null && _LastEventMessage.CheckTimeout())
            {
                _LastEventMessage = null;
            }
            if (this._Items.Count > 0)
            {
                for (int iCount = this._Items.Count - 1; iCount >= 0; iCount--)
                {
                    if (this._Items[iCount].CheckTimeout())
                    {
                        this._Items.RemoveAt(iCount);
                    }
                }
            }
        }

        internal void AddMessage(DCTimeLineControlEventMessage msg)
        {
            if (msg == null)
            {
                throw new ArgumentNullException("msg");
            }
            this._Items.Add(msg);
            this.ClearTimeoutEventMessage();
        }

        /// <summary>
        /// 清空内部的编辑器控件事件消息队列
        /// </summary>
        internal void Clear()
        {
            this._LastEventMessage = null;
            this._Items.Clear();
        }

        /// <summary>
        /// 获得一个编辑器控件事件消息.只有当控件的EnabledControlEvent=false时，本函数才有效。
        /// </summary>
        /// <returns></returns>
        internal DCTimeLineControlEventMessage GetEventMessage()
        {
            this._LastEventMessage = null;
            lock (this)
            {
                ClearTimeoutEventMessage();
                if (this._Items.Count > 0)
                {
                    DCTimeLineControlEventMessage info = this._Items[0];
                    this._Items.RemoveAt(0);
                    this._LastEventMessage = info;
                    return info;
                }
            }
            return null;
        }
    }//public class WriterControlEventMessageList : List<WriterControlEventMessage>

    /// <summary>
    /// 编辑器事件消息信息对象,不可继承
    /// </summary>
    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = false )]
    public sealed partial class DCTimeLineControlEventMessage
    {
        //public WriterControlEventMessageInfo()
        //{
        //}
        public DCTimeLineControlEventMessage(TemperatureControl ctl, SelectPageIndexChangeArgs args)
        {
            this._TypeValue = DCTimeLineControlEventMessageType.SelectPageIndexChanged;
            this._Control = ctl;
            this._Document = ctl.Document;
            this._PageIndex = args.PageIndex;
        }

        public DCTimeLineControlEventMessage(TemperatureControl ctl, DocumentDblClickEventArgs args)
        {
            this._TypeValue = DCTimeLineControlEventMessageType.DocumentDblClick;
            this._Control = ctl;
            this._Document = args.Document;
        }

        public DCTimeLineControlEventMessage(TemperatureControl ctl, DocumentMouseMoveEventArgs args)
        {
            this._TypeValue = DCTimeLineControlEventMessageType.DocumentMouseMove;
            this._Control = ctl;
            this._Document = args.Document;
        }

        public DCTimeLineControlEventMessage(TemperatureControl ctl, DocumentClickEventArgs args)
        {
            this._TypeValue = DCTimeLineControlEventMessageType.DocumentClick;
            this._Control = ctl;
            this._Document = args.Document;
        }

        public DCTimeLineControlEventMessage(TemperatureControl ctl, AfterRunDesignerEventArgs args)
        {
            this._TypeValue = DCTimeLineControlEventMessageType.AfterRunDesigner;
            this._Control = ctl;
            this._Document = ctl.Document;
            this._ConfigXml = args.ConfigXML;
        }

        public DCTimeLineControlEventMessage(TemperatureControl ctl, EditValuePointEventArgs args)
        {
            this._TypeValue = DCTimeLineControlEventMessageType.EditValuePoint;
            this._Control = ctl;
            this._Document = ctl.Document;
            this._ValuePoint = args.ValuePoint;
            this._SerialName = args.SerialName;
            this._SerialTitle = args.SerialTitle;
        }

        public DCTimeLineControlEventMessage(TemperatureControl ctl, DocumentLinkClickEventArgs args)
        {
            this._TypeValue = DCTimeLineControlEventMessageType.DocumentLinkClick;
            this._Control = ctl;
            this._Document = ctl.Document;
            this._ValuePoint = args.ValuePoint;
            this._Link = args.Link;
            this._LinkTarget = args.LinkTarget;
            this._ScreenBounds = args.ScreenBounds;
        }

        public DCTimeLineControlEventMessage(TemperatureControl ctl, ValuePointClickEventArgs args)
        {
            this._TypeValue = DCTimeLineControlEventMessageType.ValuePointClick;
            this._Control = ctl;
            this._Document = ctl.Document;
            this._ValuePoint = args.Point;
            this._SerialName = args.SerialName;
            this._SerialTitle = args.SerialTitle;
        }
        public DCTimeLineControlEventMessage(TemperatureControl ctl, DCTimeLineControlEventMessageType type)
        {
            this._TypeValue = type;
            this._Control = ctl;
            this._Document = ctl.Document;
        }

        public DCTimeLineControlEventMessage(TemperatureControl ctl, TimeLineZoneEventArgs args, DCTimeLineControlEventMessageType type)
        {
            this._TypeValue = type;
            this._Control = ctl;
            this._Document = ctl.Document;
            this._Zone = args.Zone;
        }

        private TimeLineZoneInfo _Zone = null;

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public TimeLineZoneInfo Zone
        {
            get { return _Zone; }
        }
        private string _Link = null;
        /// <summary>
        /// 超链接地址
        /// </summary>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string Link
        {
            get
            {
                return _Link;
            }
        }

        private string _LinkTarget = null;
        /// <summary>
        /// 超链接目标
        /// </summary>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string LinkTarget
        {
            get
            {
                return _LinkTarget;
            }
        }

        private Rectangle _ScreenBounds = Rectangle.Empty;
        /// <summary>
        /// 超链接区域在屏幕坐标中的 边界
        /// </summary>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public Rectangle ScreenBounds
        {
            get
            {
                return _ScreenBounds;
            }
        }


        private string _SerialName = null;

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string SerialName
        {
            get { return _SerialName; }
        }

        private string _SerialTitle = null;

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string SerialTitle
        {
            get { return _SerialTitle; }
        }

        private string _ConfigXml = null;

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string ConfigXml
        {
            get { return _ConfigXml; }
        }
        private TemperatureDocument _Document = null;
        /// <summary>
        /// 文档对象
        /// </summary>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public TemperatureDocument Document
        {
            get { return _Document; }
        }

        private TemperatureControl _Control = null;
        /// <summary>
        /// 控件对象
        /// </summary>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public TemperatureControl Control
        {
            get { return _Control; }
        }

        private ValuePoint _ValuePoint = null;
        /// <summary>
        /// 数据点对象
        /// </summary>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public ValuePoint ValuePoint
        {
            get { return _ValuePoint; }
        }
        private DCTimeLineControlEventMessageType _TypeValue = DCTimeLineControlEventMessageType.None;

        internal DCTimeLineControlEventMessageType TypeValue
        {
            get { return _TypeValue; }
            set { _TypeValue = value; }
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string Type
        {
            get
            {
                return _TypeValue.ToString();
            }
        }

        private int _PageIndex = 0;

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public int PageIndex
        {
            get { return _PageIndex; }
            set { _PageIndex = value; }
        }

         
        [System.Runtime.InteropServices.ComVisible(false)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void Clear()
        {
            this._Control = null;
            this._Document = null;
        }

        private DateTime _CreationTime = DateTime.Now;
        public bool CheckTimeout()
        {
            TimeSpan span = DateTime.Now - _CreationTime;
            return span.TotalSeconds > 3;
        }

    }

    /// <summary>
    /// 编辑器事件类型
    /// </summary>
    [System.Runtime.InteropServices.ComVisible(true)]
     
    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
    [System.Runtime.InteropServices.Guid("6F60A1B1-09C3-4A0D-BE23-44791948E6C6")]
    public enum DCTimeLineControlEventMessageType
    {
        /// <summary>
        /// 无事件
        /// </summary>
        None,
        SelectPageIndexChanged,
        DocumentDblClick,
        DocumentMouseMove,
        DocumentClick,
        AfterRunDesigner,
        EditValuePoint,
        DocumentLinkClick,
        ValuePointClick,
        AfterRefreshView,
        EventZoneAfterCollapse,
        EventZoneAfterExpand
    }
}
