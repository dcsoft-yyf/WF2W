using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WinClock
{
    public partial class Form10 : Form
    {
        public Form10()
        {
            InitializeComponent();            
        }

        private void Form10_Load(object sender, EventArgs e)
        {
            ClockControl clock = new ClockControl();
            panel1.Controls.Add(clock);
        }
    }
}
