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
    /// 时间轴配置XML文本对话框
    /// </summary>
    [System.Runtime.InteropServices.ComVisible( false )]
    //[System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true  )]
    public partial class frmConfigXML : Form
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public frmConfigXML()
        {
            DCSoft.WinForms.WinFormUtils.SetFormDefaultFont(this);
            InitializeComponent();
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        /// <summary>
        /// XML文本
        /// </summary>
        public string XMLText
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
        private void frmConfigXML_Load(object sender, EventArgs e)
        {
            this.textBox1.Modified = false;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.textBox1.Modified)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnReplaceChars_Click(object sender, EventArgs e)
        {
            bool m = this.textBox1.Modified;
            string txt = this.textBox1.Text.Replace('"','\'');
            this.textBox1.Text = txt;
            this.textBox1.Modified = m;
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            try
            {
                System.Windows.Forms.Clipboard.SetText(this.textBox1.Text);
            }
            catch (Exception ext)
            {
                MessageBox.Show(
                    this , 
                    ext.Message , 
                    this.Text , 
                    MessageBoxButtons.OK , 
                    MessageBoxIcon.Error );
            }
        }

        private void btnPaste_Click(object sender, EventArgs e)
        {
            try
            {
                string txt = System.Windows.Forms.Clipboard.GetText();
                if (string.IsNullOrEmpty(txt) == false )
                {
                    this.textBox1.Text = txt;
                }
            }
            catch
            {
            }
        }
    }
}
