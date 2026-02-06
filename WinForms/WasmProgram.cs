using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.JSInterop;
using WindowsFormsApp;

namespace MWGAWinFormDemo
{
    public static class WasmProgram
    {

        [JSInvokable]
        public static void TestCustomCompoundControl()
        {
            try
            {
                var frm = new Form();
                var customctl = new CompoundControl1();
                customctl.Dock = DockStyle.Fill;
                frm.Controls.Add(customctl);
                Application.Run(frm);
            }
            catch (Exception ext)
            {
                Console.WriteLine(ext.ToString());
                throw ext;
            }
        }

        [JSInvokable]
        public static void RunDCTimeLine()
        {
            try
            {
                var frm = new frmDCTimlineDemo();
                Application.Run(frm);
            }
            catch (Exception ext)
            {
                Console.WriteLine(ext.ToString());
                throw ext;
            }
        }

        [JSInvokable]
        public static void RunTextFileViewer()
        {
            var frm = new Form();
            frm.Text = "Text file viewer";
            frm.StartPosition = FormStartPosition.CenterScreen;

            var txt = new TextBox();
            txt.ScrollBars = ScrollBars.Both;
            txt.Multiline = true;
            txt.WordWrap = true;
            txt.Dock = DockStyle.Fill;
            frm.Controls.Add(txt);

            var btn = new Button();
            btn.Text = "Open...";
            btn.Dock = DockStyle.Top;
            frm.Controls.Add(btn);


            btn.ClickAsync += async delegate (object sender, EventArgs e)
            {
                var ofd = new OpenFileDialog();
                ofd.Filter = "Text files|*.txt|All files|*.*";
                if (await ofd.ShowDialog(frm) == DialogResult.OK)
                {
                    frm.Text = "Text file viewer - " + ofd.FileName;
                    var stream = await ofd.OpenFile();
                    var reader = new System.IO.StreamReader(stream);
                    txt.Text = reader.ReadToEnd();
                    reader.Close();
                }
            };
            Application.Run(frm);
        }

        private static Task Btn_ClickAsync1(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private static Task Btn_ClickAsync(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        [JSInvokable]
        public static void RunMinesweeper()
        {
            try
            {
                Application.Run(new Minesweeper.frmMinesweeper());
            }
            catch (Exception ext)
            {
                Console.WriteLine(ext.ToString());
            }
        }
        [JSInvokable]
        public static void TestResourceForm()
        {
            try
            {
                var dlg = new dlgMessage();
                dlg.InputMessageText = "Some text " + DateTime.Now.ToString();
                Application.Run(dlg);
            }
            catch (System.Exception ext)
            {
                Console.WriteLine(ext.ToString());
            }
        }
        [JSInvokable]
        public static void TestTimer()
        {
            try
            {
                var frm = new Form();
                frm.Width = 500;
                frm.Height = 400;
                var timer = new System.Windows.Forms.Timer();
                timer.Interval = 500;
                timer.Tick += delegate (object obj2, EventArgs args2)
                {
                    frm.Text = DateTime.Now.ToLongTimeString();
                };
                timer.Start();
                Application.Run(frm);
            }
            catch (Exception ext)
            {
                Console.WriteLine(ext.ToString());
            }
        }

        [JSInvokable]
        public static void TestMessageBox()
        {
            Application.EnableVisualStyles();
            var frm = new Form();
            frm.Text = "Demo of MessageBox ";
            frm.Size = new System.Drawing.Size(400, 300);
            var btn = new Button();
            btn.Text = "Show Old MessageBox";
            btn.Bounds = new System.Drawing.Rectangle(20, 20, 150, 40);
            btn.Dock = DockStyle.Top;
            btn.Click += delegate (object? sender, EventArgs e)
            {
                var result = System.Windows.Forms.MessageBox.Show(
                    "Hello from old message box!",
                    "Old MessageBox",
                    MessageBoxButtons.YesNo);
                frm.Text = "Old=" + result.ToString();
            };
            frm.Controls.Add(btn);

            var lbl = new Label();
            lbl.AutoSize = true;
            lbl.Text = "Show New MessageBox";
            lbl.Height = 50;
            lbl.Dock = DockStyle.Bottom;
            lbl.ClickAsync += async delegate (object sender, EventArgs e)
            {
                var result = await System.Windows.Forms.MessageBox.ShowAsync(
                    "Hello from new message box!",
                    "New MessageBox",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2);
                frm.Text = "New=" + result.ToString();
                //throw new Exception("zzzz");
            };
            frm.Controls.Add(lbl);
            Application.Run(frm);
        }


        /// <summary>
        /// 一个很简单的测试窗体
        /// </summary>
        [JSInvokable]
        public static void TestFirstForm()
        {
            Application.EnableVisualStyles();
            var frm = new Form();
            frm.Text = "First form" + DateTime.Now.ToString();
            frm.Size = new System.Drawing.Size(200, 300);
            frm.BackColor = System.Drawing.Color.Red;
            frm.Load += delegate (object? sender, EventArgs e)
            {
                frm.Text = frm.Size.ToString();
            };
            Application.Run(frm);
        }

        /// <summary>
        /// 计算器的应用程序的主入口点。
        /// </summary>
        [JSInvokable]
        public static void Main4WinFormCalculator()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new CalculatorForm());
        }
    }
}
