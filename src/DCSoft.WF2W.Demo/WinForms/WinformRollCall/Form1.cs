using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace 点名系统
{
    public partial class Form1 : Form
    {
        //string st = File.ReadAllText("name.txt",Encoding.Default);
        //string[] st = new string[] {"李四","张三","王五"};//存储要参与点名的人的名字，可修改
        bool b = true;//最开始第一次设为true,此时文本值为 开始
        string st1="";
        string str2 = "李林 李一 张三 李四 王五 张思 赵一 王二 李二  王立 李小"; ///File.ReadAllText("name.txt", Encoding.Default);//文件name的路径在E:\new program\接口\点名系统\bin\Debug当中，在其中输入要点名的人用空格分开
            string[] str3;
            // 下载于www.mycodes.net
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)//在窗体中加入一个按钮，按钮上方放一个label空间
        {
            
            st1 = "开始";
            if (b==true)//当前值为开始时就进入这条分支
            {   
                //button1.Text = "停止";//一旦点了开始就把按钮文本设置为停止 ,同时把b的值设为false就会进入else分支           
                b = false;
                timer1.Start();
            }
            else
            {
                timer1.Stop();
                //button1.Text = st1;
                b = true;
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            Random r = new Random();
            int i = r.Next(str3.Length);
            //label1.Text=str3[i];
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }
        // 下载于www.mycodes.net
        private void Form1_Load(object sender, EventArgs e)//在窗体加载时就把文件中的信息读到str3这个数组里
        {
            //label1.Text = st[0];//以数组的形式存储
            //string[] str3;//下面使用读文件方式读取名字
            str3 = str2.Split(' ');//以空格为分割依据，将str2这个字符串分割成多个名字
            //label1.Text = str3[0];//把数组的首元素付给标签，初始值
        }
    }
}
