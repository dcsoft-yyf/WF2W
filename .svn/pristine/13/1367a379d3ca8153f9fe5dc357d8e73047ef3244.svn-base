using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using DCSoft.Drawing;

namespace DCSoft.TemperatureChart
{
    /// <summary>
    /// 时间轴设计器窗体
    /// </summary>
    [System.Runtime.InteropServices.ComVisible( false )]
    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = false )]
    public partial class frmTimeLineDesigner : Form
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public frmTimeLineDesigner()
        {
            DCSoft.WinForms.WinFormUtils.SetFormDefaultFont(this);
            InitializeComponent();
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.myDesignerControl.EventOKButtonClick += new EventHandler(myDesignerControl_EventOKButtonClick);
            this.myDesignerControl.EventCancelButtonClick += new EventHandler(myDesignerControl_EventCancelButtonClick);
            
        }
        /// <summary>
        /// 文档对象
        /// </summary>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true )]
        public TemperatureDocument SourceDocument
        {
            get
            {
                return this.myDesignerControl.SourceDocument;
            }
            set
            {
                this.myDesignerControl.SourceDocument = value;
            }
        }
        /// <summary>
        /// 控件对象
        /// </summary>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public TemperatureControl SourceControl
        {
            get
            {
                return this.myDesignerControl.SourceControl;
            }
            set
            {
                this.myDesignerControl.SourceControl = value;
            }
        }

        void myDesignerControl_EventCancelButtonClick(object sender, EventArgs e)
        {
            this.Close();
        }

        void myDesignerControl_EventOKButtonClick(object sender, EventArgs e)
        {

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
         

    }
}


