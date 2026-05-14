using System;
using System.Collections.Generic;
using System.Text;

namespace DCSoft.TemperatureChart
{
    /// <summary>
    /// 时间轴运行设计器之后的事件的委托类型
    /// </summary>
    /// <param name="eventSender"></param>
    /// <param name="args"></param>
    [System.Runtime.InteropServices.ComVisible( true )]
    [System.Reflection.Obfuscation( Exclude= true , ApplyToMembers = true )]
     
    [DCSoft.Common.DCPublishAPI]
    [System.Runtime.InteropServices.Guid("3A78251F-92EF-4F5E-8F99-F3A0FADDFFCC")]
    public delegate void AfterRunDesignerEventHandler(object eventSender , AfterRunDesignerEventArgs args );

    /// <summary>
    /// 时间轴运行设计器之后的事件参数
    /// </summary>
    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = false )]
     
    [DCSoft.Common.DCPublishAPI]
    public partial class AfterRunDesignerEventArgs
    {
#if WINFORM || DCWriterForWinFormNET6 || DCWriterForWASM
        public AfterRunDesignerEventArgs(TemperatureControl ctl, TemperatureDocument document )
        {
            if (ctl == null)
            {
                throw new ArgumentNullException("ctl");
            }
            if (document == null)
            {
                throw new ArgumentNullException("document");
            }
            this._Control = ctl;
            this._Document = document;
            //this._ConfigXML = xml;
        }

        private TemperatureControl _Control = null;
        /// <summary>
        /// 时间轴控件对象
        /// </summary>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public TemperatureControl Control
        {
            get { return _Control; }
        }
#else
        public AfterRunDesignerEventArgs( TemperatureDocument document)
        {
            if (document == null)
            {
                throw new ArgumentNullException("document");
            }
            this._Document = document;
            //this._ConfigXML = xml;
        }
#endif
        private TemperatureDocument _Document = null;
        /// <summary>
        /// 文档对象
        /// </summary>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public TemperatureDocument Document
        {
            get { return _Document; }
        }

        //private string _ConfigXML = null;
        /// <summary>
        /// 配置XML字符串
        /// </summary>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string ConfigXML
        {
            get { return this._Document.ConfigXml ; }
        }

    }
}
