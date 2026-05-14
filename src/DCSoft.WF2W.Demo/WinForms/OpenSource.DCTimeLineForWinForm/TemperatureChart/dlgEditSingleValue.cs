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
    /// 编辑单个字符串数据的对话框
    /// </summary>
    [System.Runtime.InteropServices.ComVisible( false )]
    //[System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = false)]
    internal partial class dlgEditSingleValue : Form
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public dlgEditSingleValue()
        {
            DCSoft.WinForms.WinFormUtils.SetFormDefaultFont(this);
            InitializeComponent();
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private DateTimePrecisionMode _InputTimePrecision = DateTimePrecisionMode.Second;
        /// <summary>
        /// 输入时间的精度
        /// </summary>
        [DefaultValue(DateTimePrecisionMode.Second)]
        public DateTimePrecisionMode InputTimePrecision
        {
            get
            {
                return _InputTimePrecision;
            }
            set
            {
                _InputTimePrecision = value;
            }
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
                return DCTimeLineUtils.FormatDateTime(this.dateTimePicker1.Value, this.InputTimePrecision);
            }
            set
            {
                 this.dateTimePicker1.Value = value;
            }
        }

        private object _InputParent = null;
        /// <summary>
        /// 父对象
        /// </summary>
        public object InputParent
        {
            get { return _InputParent; }
            set { _InputParent = value; }
        }
        /// <summary>
        /// 标题
        /// </summary>
        public string InputTitle
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
        ///  输入的文本内容
        /// </summary>
        public string InputValue
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

        private float _ResultValue = float.NaN;
        /// <summary>
        /// 结果值
        /// </summary>
        public float ResultValue
        {
            get { return _ResultValue; }
            set { _ResultValue = value; }
        }

        /// <summary>
        /// 确定按钮点击事件
        /// </summary>
        public event CancelEventHandler EventOKButtonClick = null;

        private void dlgEditSingleValue_Load(object sender, EventArgs e)
        {
            dateTimePicker1.CustomFormat = DCTimeLineUtils.GetDateTimeFormatString(this.InputTimePrecision);
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