using System;
using System.Drawing;
using System.Windows.Forms;

namespace WF2WWinFormDemo.ControlsDemo
{
    public partial class frmTestForm : Form
    {
        public frmTestForm()
        {
            InitializeComponent();
        }

        private void frmTestForm_Load(object sender, EventArgs e)
        {
            this.ctlDemo.TopLevel = false;
            this.ctlDemo.FormBorderStyle = FormBorderStyle.None;
            this.panel1.Controls.Add(this.ctlDemo);
            //this.Icon = 
            this.Text = "First form" + DateTime.Now.ToString();
            this.Size = new System.Drawing.Size(200, 300);
            this.BackColor = System.Drawing.Color.LightYellow;
            this.ctlDemo.Show();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            this.ctlDemo.Text = this.txtTitle.Text;

            this.ctlDemo.FormBorderStyle = FormBorderStyle.None;

           // this.ctlDemo.Icon = 
            this.ctlDemo.Text = "Second form" + DateTime.Now.ToString();
            this.ctlDemo.Size = new System.Drawing.Size(200, 300);
            this.ctlDemo.BackColor = System.Drawing.Color.SkyBlue;

        }
    }
}
