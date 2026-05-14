using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing ;
using System.Runtime.InteropServices;

// 袁永福到此一游

namespace DCSoft.TemperatureChart
{
    /// <summary>
    /// 文档超链接点击事件委托类型
    /// </summary>
    /// <param name="eventSender">事件发起者</param>
    /// <param name="args">事件参数</param>
     
    [System.Runtime.InteropServices.ComVisible( true )]
    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
[System.Runtime.InteropServices. Guid("CB9CC65D-87DB-428C-90C2-76CFF4988964")]
    public delegate void DocumentLinkClickEventHandler( 
        object eventSender , 
        DocumentLinkClickEventArgs args );


    /// <summary>
    /// 文档中的超链接点击事件
    /// </summary>

    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = false )]
    public partial class DocumentLinkClickEventArgs
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        /// <param name="ctl">控件对象</param>
        /// <param name="doc">文档对象</param>
        /// <param name="vp">数据点对象</param>
        /// <param name="link">超链接地址</param>
        /// <param name="linkTaget">超链接目标</param>
        public DocumentLinkClickEventArgs(
            TemperatureControl ctl,
            TemperatureDocument doc,
            ValuePoint vp,
            string link,
            string linkTaget)
        {
            if (ctl == null)
            {
                throw new ArgumentNullException("ctl");
            }
            this._Control = ctl;
            if (doc == null)
            {
                throw new ArgumentNullException("doc");
            }
            if (vp == null)
            {
                throw new ArgumentNullException("vp");
            }
            this._Document = doc;
            this._ValuePoint = vp;
            this._Link = link;
            this._LinkTarget = linkTaget;
            RectangleF bounds = this._Control.DocumentViewControl.ViewTransform.UnTransformRectangleF(vp.ViewBounds);
            bounds.Location = this._Control.DocumentViewControl.PointToScreen(new Point((int)bounds.Left, (int)bounds.Top));
            this._ScreenBounds = new Rectangle((int)bounds.Left, (int)bounds.Top, (int)bounds.Width, (int)bounds.Height);
        }
        private TemperatureControl _Control = null;
        
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

        private ValuePoint _ValuePoint = null;
        /// <summary>
        /// 数据点对象
        /// </summary>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public ValuePoint ValuePoint
        {
            get
            {
                return _ValuePoint; 
            }
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
        [ComVisible( false )]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public Rectangle ScreenBounds
        {
            get
            {
                return _ScreenBounds; 
            }
        }

        
    }
}
