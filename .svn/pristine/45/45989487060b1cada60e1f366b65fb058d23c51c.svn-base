using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DCSoft.TemperatureChart
{
    /// <summary>
    /// 编辑2个数据的对话框
    /// </summary>
    [System.Runtime.InteropServices.ComVisible(false)]
    //[System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = false)]
    internal partial class dlgEditTowValues : Form
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public dlgEditTowValues()
        {
            DCSoft.WinForms.WinFormUtils.SetFormDefaultFont(this);
            InitializeComponent();
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
        /// <summary>
        /// 允许输入时间
        /// </summary>
        public bool EnableInputTime
        {
            get
            {
                return this.dateTimePicker1.Enabled;
            }
            set
            {
                this.dateTimePicker1.Enabled = value;
            }
        }
        /// <summary>
        /// 输入的时间
        /// </summary>
        public DateTime InputTime
        {
            get
            {
                return this.dateTimePicker1.Value;
            }
            set
            {
                this.dateTimePicker1.Value = value;
            }
        }
        /// <summary>
        /// 标题1
        /// </summary>
        public string InputTitle1
        {
            get
            {
                return this.lblTitle.Text;
            }
            set
            {
                this.lblTitle.Text = value;
            }
        }
        /// <summary>
        /// 数值1
        /// </summary>
        public string InputValue1
        {
            get
            {
                return this.txtValue.Text;
            }
            set
            {
                this.txtValue.Text = value;
            }
        }

        private object _InputParent1 = null;
        /// <summary>
        /// 父对象1
        /// </summary>
        public object InputParent1
        {
            get { return _InputParent1; }
            set { _InputParent1 = value; }
        }
        /// <summary>
        /// 标题2
        /// </summary>
        public string InputTitle2
        {
            get
            {
                return this.label1.Text;
            }
            set
            {
                this.label1.Text = value;
            }
        }
        /// <summary>
        /// 数值2
        /// </summary>
        public string InputValue2
        {
            get
            {
                return this.textBox1.Text;
            }
            set
            {
                this.textBox1.Text = value;
            }
        }

        private object _InputParent2 = null;
        /// <summary>
        /// 父对象2
        /// </summary>
        public object InputParent2
        {
            get { return _InputParent2; }
            set { _InputParent2 = value; }
        }
        /// <summary>
        /// 确定按钮点击事件
        /// </summary>
        public event CancelEventHandler EventOKButtonClick = null;

        private void dlgEditSingleValue_Load(object sender, EventArgs e)
        {
            txtValue.Focus();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (EventOKButtonClick != null)
            {
                CancelEventArgs args = new CancelEventArgs();
                args.Cancel = false ;
                EventOKButtonClick(this, args);
                if (args.Cancel)
                {
                    return;
                }
            }
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}