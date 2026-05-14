using System;
using System.Drawing;
using System.Windows.Forms;

namespace WF2WWinFormDemo.ControlsDemo
{
    public partial class frmTestMdiClient : Form
    {
        public frmTestMdiClient()
        {
            InitializeComponent();
        }

        private void frmTestMdiClient_Load(object sender, EventArgs e)
        {
            this.txtName.Text = this.ctlDemo.Name;
            this.chkEnabled.Checked = this.ctlDemo.Enabled;
            this.chkVisible.Checked = this.ctlDemo.Visible;
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            this.ctlDemo.Name = this.txtName.Text;
            this.ctlDemo.Enabled = this.chkEnabled.Checked;
            this.ctlDemo.Visible = this.chkVisible.Checked;
        }
    }
}
