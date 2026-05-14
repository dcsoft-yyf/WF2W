using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WF2WWinFormDemo
{
    public partial class dlgMessage : Form
    {
        public dlgMessage()
        {
            InitializeComponent();
            this.DialogResult = DialogResult.Cancel;
        }

        public string InputMessageText
        {
            get { return this.txtMessage.Text; }
            set { this.txtMessage.Text = value; }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void pictureBox1_Move(object sender, EventArgs e)
        {
            var s = 1;
        }

        private void panel1_Resize(object sender, EventArgs e)
        {
            var s = 1;
        }
    }
}
