using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Huarongdao
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Method method = new Method();
        int count;
        private void Form1_Load(object sender, EventArgs e)
        {
            method.AddButtons(panel1, variables.buttons);
            label2.Text = "0" + "S";  //初始时间
            timer1.Start();  //启动计时器
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            //默认100毫秒刷新一次
            count += 1;
            label2.Text = (count / 10).ToString() + "S";
            if (method.GameoverOrNot())
            {
                timer1.Stop();
                MessageBox.Show("挑战成功!");
            }
        }

        private void ButtonR_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            for (int i = 0; i < variables.buttons.GetLength(0); i++)
                for (int j = 0; j < variables.buttons.GetLength(1); j++)
                {
                    variables.buttons[i, j].Hide();
                }

            method.AddButtons(panel1, variables.buttons);
            count = 0;
            timer1.Start();
        }
    }
}
