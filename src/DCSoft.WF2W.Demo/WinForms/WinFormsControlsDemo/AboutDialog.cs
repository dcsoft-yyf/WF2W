using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsControlsDemo
{
    public class AboutDialog : Form
    {
        private Label lblTitle;
        private Label lblVersion;
        private Label lblCopyright;
        private Button btnOK;
        private PictureBox pictureBox;

        public AboutDialog()
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            this.Text = "关于";
            this.Size = new Size(400, 300);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // 图标图片框
            pictureBox = new PictureBox();
            pictureBox.Location = new Point(20, 20);
            pictureBox.Size = new Size(64, 64);
            pictureBox.BackColor = SystemColors.Control;

#if !WF2W
            pictureBox.Image = SystemIcons.Application.ToBitmap();
#endif
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;

            // 标题标签
            lblTitle = new Label();
            lblTitle.Text = "多级菜单演示程序";
            lblTitle.Font = new Font("微软雅黑", 14, FontStyle.Bold);
            lblTitle.Location = new Point(100, 30);
            lblTitle.AutoSize = true;

            // 版本标签
            lblVersion = new Label();
            lblVersion.Text = "版本 1.0.0";
            lblVersion.Location = new Point(100, 65);
            lblVersion.AutoSize = true;

            // 版权标签
            lblCopyright = new Label();
            lblCopyright.Text = "© 2024 版权所有\n这是一个演示多级菜单的示例程序";
            lblCopyright.Location = new Point(100, 95);
            lblCopyright.AutoSize = true;

            // 确定按钮
            btnOK = new Button();
            btnOK.Text = "确定";
            btnOK.Location = new Point(155, 220);
            btnOK.Size = new Size(75, 23);
            btnOK.DialogResult = DialogResult.OK;

            // 添加控件
            this.Controls.AddRange(new Control[] {
                pictureBox, lblTitle, lblVersion, lblCopyright, btnOK
            });
        }
    }
}