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
    /// 编辑多个标题和内容的窗体
    /// </summary>
    [System.Runtime.InteropServices.ComVisible( false )]
    //[System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = false)]
    internal partial class dlgTitleValues : Form
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public dlgTitleValues()
        {
            DCSoft.WinForms.WinFormUtils.SetFormDefaultFont(this);
            InitializeComponent();
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private readonly List<string> _InputTitles = new List<string>();
        /// <summary>
        /// 标题列表
        /// </summary>
        public List<string> InputTitles
        {
            get { return _InputTitles; }
        }

        private readonly List<string> _InputValues = new List<string>();
        /// <summary>
        /// 数值列表
        /// </summary>
        public List<string> InputValues
        {
            get { return _InputValues; }
        }
        private void dlgEditHeaderLabels_Load(object sender, EventArgs e)
        {
            int max = Math.Max(this.InputTitles.Count, this.InputValues.Count);
            for (int iCount = 0; iCount < max; iCount++)
            {
                this.dgvLabels.Rows.Add(this.InputTitles[iCount], this.InputValues[iCount]);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            int max = Math.Max(this.InputTitles.Count, this.InputValues.Count);
            for (int iCount = 0; iCount < max; iCount++)
            {
                this.InputValues[iCount] = Convert.ToString( dgvLabels.Rows[iCount].Cells[1].Value );
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
