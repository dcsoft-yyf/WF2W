using DCSoft;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MWGAWinFormDemo
{

    public class CompoundControl1 : UserControl
    {
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Array KnownColors;
        public CompoundControl1()
        {
            this.KnownColors = Enum.GetValues(typeof(KnownColor));
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.textBox1.Name = "textBox1";
            this.textBox1.Multiline = true;
            this.button1.Name = "button1";
            this.button1.Text = "弹出对话框";
            this.button1.Click += Button1_Click;
            this.button2.Name = "button2";
            this.button2.Text = "变色";
            this.button2.Click += Button2_Click;
            this.button2.BackColor = Color.FromKnownColor((KnownColor)this.KnownColors.GetValue(0));
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textBox1);
            this.Name = "CompoundControl1";
            this.Size = new System.Drawing.Size(281, 42);
            this.Load += UserControl1_Load;
            this.SizeChanged += UserControl1_SizeChanged;
            
        }

        private int index = 0;
        private void Button2_Click(object sender, EventArgs e)
        {
            index++;
            if (index >= this.KnownColors.Length)
            {
                index = 0;
            }
            //实现功能从KnownColor列表里按顺序换色
            Color cc = Color.FromKnownColor((KnownColor)this.KnownColors.GetValue(index));
            //MessageBox.Show(cc.Name);
            Console.WriteLine(cc.Name);
            this.button2.BackColor = cc;
            this.button2.Refresh();
        }


        private void Button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this.textBox1.Text);
        }

        private void UserControl1_Load(object sender, EventArgs e)
        {
            performlayout();
        }

        private void UserControl1_SizeChanged(object sender, EventArgs e)
        {
            performlayout();
        }

        private void performlayout()
        {
            this.textBox1.Location = new Point(0, 0);
            this.textBox1.Width = this.Width;
            this.textBox1.Height = this.Height / 2;
            this.button1.Location = new Point(0, this.Height / 2);
            this.button1.Width = this.Width / 2;
            this.button1.Height = this.Height / 2;
            this.button2.Location = new Point(this.Width / 2, this.Height / 2);
            this.button2.Width = this.Width / 2;
            this.button2.Height = this.Height / 2;
        }
    }
}
