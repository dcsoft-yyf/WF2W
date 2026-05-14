using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApp
{
    public partial class frmPicture : Form
    {
        private string picFileName = null;
        public frmPicture()
        {
            InitializeComponent();
        }

        public frmPicture(string picFileUrl)
        {
            picFileName = picFileUrl;
            InitializeComponent();
        }

        private void frmPicture_Load(object sender, EventArgs e)
        {
            if (picFileName != null)
            {
                pictureBox1.ImageLocation = picFileName;
                //pictureBox1.Image = Image.FromFile(picFileName);
            }
        }
    }
}
