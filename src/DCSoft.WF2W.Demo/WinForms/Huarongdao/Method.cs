using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Huarongdao
{
    class Method
    {
        //数组打乱顺序
        public int[] NewSorting(int[] a)
        {
            Random r = new Random();
            for (int i = 0; i < a.Length; i++)
            {
                int temp = a[i];
                int randomindex = r.Next(0, a.Length);
                a[i] = a[randomindex];
                a[randomindex] = temp;
            }
            return a;
        }

        //向容器中添加16个按钮
        public void AddButtons(Panel panel, Button[,] buttons)
        {
            //数组随机打乱顺序
            int[] a = variables.a;
            int[] s = NewSorting(a);
            //每个按钮的宽度及高度
            int width = 32;
            int height = 32;
            int x0 = panel.Location.X;
            int y0 = panel.Location.Y;
            for (int i = 0; i < buttons.GetLength(0); i++)
                for (int j = 0; j < buttons.GetLength(1); j++)
                {
                    Button butt = new Button();
                    //设置按钮相关属性
                    butt.Size = new System.Drawing.Size(width, height);
                    butt.Location = new System.Drawing.Point(x0 + (j + 1) * width, y0 + (i + 1) * height);
                    butt.Visible = true;
                    //打乱顺序的数组分配给每个button
                    butt.Text = s[i * buttons.GetLength(0) + j].ToString();
                    if (butt.Text == "16")
                    {
                        butt.Hide();
                    }
                    variables.buttons[i, j] = butt;

                    //手动添加点击事件
                    butt.Click += new EventHandler(butt_Click);
                    //按钮添加到容器
                    panel.Controls.Add(butt);
                }
        }

        //自定义点击事件
        public void butt_Click(Object sender, EventArgs e)
        {
            Button butt = sender as Button;
            Button butt_16 = Find_Button16();

            //判断是否相邻，如果相邻则交换
            if (Neighboor(butt, butt_16))
            {
                swap(butt, butt_16);
                butt_16.Focus();
            }
        }

        //找出隐藏的按钮 即16所在的按钮
        public Button Find_Button16()
        {
            for (int i = 0; i < variables.buttons.GetLength(0); i++)
                for (int j = 0; j < variables.buttons.GetLength(1); j++)
                {
                    if (variables.buttons[i, j].Visible == false)
                        return variables.buttons[i, j];
                }
            return null;
        }

        //判断两个按钮是否相邻   即两个按钮的坐标位置是否差一个宽度或者高度
        public bool Neighboor(Button butt1, Button butt2)
        {
            int x1 = butt1.Location.X;
            int y1 = butt1.Location.Y;

            int x2 = butt2.Location.X;
            int y2 = butt2.Location.Y;

            if (((x1 == x2) && (Math.Abs(y1 - y2) == butt1.Height)) || ((y1 == y2) && (Math.Abs(x1 - x2) == butt1.Width)))
            {
                return true;
            }
            return false;
        }

        //交换两个按钮   交换text 与visible
        public void swap(Button butt1, Button butt2)
        {
            string s = butt1.Text;
            butt1.Text = butt2.Text;
            butt2.Text = s;

            bool a = butt1.Visible;
            butt1.Visible = butt2.Visible;
            butt2.Visible = a;
        }

        //判断游戏是否完成
        public bool GameoverOrNot()
        {
            for (int i = 0; i < variables.buttons.GetLength(1); i++)
                for (int j = 0; j < variables.buttons.GetLength(0); j++)
                {
                    if (int.Parse(variables.buttons[i, j].Text) != (i * variables.buttons.GetLength(0) + j + 1))
                        return false;
                }
            return true;
        }
    }
}
