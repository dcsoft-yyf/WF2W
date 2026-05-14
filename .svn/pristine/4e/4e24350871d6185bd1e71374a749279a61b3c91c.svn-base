using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel ;
using System.Drawing.Design ;

namespace DCSoft.TemperatureChart
{
    /// <summary>
    /// SQL文本编辑对话框
    /// </summary>
    [System.Runtime.InteropServices.ComVisible( false )]
    //[System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = false)]
    internal partial class dlgSQLText : Form
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public dlgSQLText()
        {
            DCSoft.WinForms.WinFormUtils.SetFormDefaultFont(this);
            InitializeComponent();
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        /// <summary>
        /// 输入的文本
        /// </summary>
        public string InputText
        {
            get
            {
                return this.txtSQL.Text;
            }
            set
            {
                this.txtSQL.Text = value;
            }
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// SQL文本编辑器
        /// </summary>

        [System.Runtime.InteropServices.ComVisible(false)]
        public class SQLTextEditor : UITypeEditor 
        {
            /// <summary>
            /// 采用对话框编辑模式
            /// </summary>
            /// <param name="context"></param>
            /// <returns></returns>
            public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
            {
                return UITypeEditorEditStyle.Modal;
            }
            /// <summary>
            /// 编辑数值
            /// </summary>
            /// <param name="context"></param>
            /// <param name="provider"></param>
            /// <param name="value"></param>
            /// <returns></returns>
            public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
            {
                using (dlgSQLText dlg = new dlgSQLText())
                {
                    dlg.InputText = Convert.ToString(value);
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        return dlg.InputText;
                    }
                }
                return value;
            }
        }
    }
}
