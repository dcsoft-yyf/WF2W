using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsControlsDemo
{
    public class OptionsDialog : Form
    {
        private Button btnOK;
        private Button btnCancel;
        private CheckBox chkAutoSave;
        private CheckBox chkShowTips;
        private NumericUpDown nudAutoSaveInterval;
        private Label lblAutoSaveInterval;
        private ComboBox cmbTheme;

        public OptionsDialog()
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            this.Text = "选项设置";
            this.Size = new Size(400, 250);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // 自动保存复选框
            chkAutoSave = new CheckBox();
            chkAutoSave.Text = "自动保存";
            chkAutoSave.Location = new Point(20, 20);
            chkAutoSave.AutoSize = true;

            // 自动保存间隔标签
            lblAutoSaveInterval = new Label();
            lblAutoSaveInterval.Text = "自动保存间隔(分钟):";
            lblAutoSaveInterval.Location = new Point(40, 50);
            lblAutoSaveInterval.AutoSize = true;

            // 自动保存间隔数值框
            nudAutoSaveInterval = new NumericUpDown();
            nudAutoSaveInterval.Location = new Point(200, 48);
            nudAutoSaveInterval.Size = new Size(60, 21);
            nudAutoSaveInterval.Minimum = 1;
            nudAutoSaveInterval.Maximum = 60;
            nudAutoSaveInterval.Value = 5;
            nudAutoSaveInterval.Enabled = false;

            // 显示提示复选框
            chkShowTips = new CheckBox();
            chkShowTips.Text = "显示操作提示";
            chkShowTips.Location = new Point(20, 85);
            chkShowTips.AutoSize = true;

            // 主题标签
            Label lblTheme = new Label();
            lblTheme.Text = "界面主题:";
            lblTheme.Location = new Point(20, 120);
            lblTheme.AutoSize = true;

            // 主题下拉框
            cmbTheme = new ComboBox();
            cmbTheme.Location = new Point(100, 117);
            cmbTheme.Size = new Size(120, 20);
            cmbTheme.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTheme.Items.AddRange(new object[] { "默认主题", "浅色主题", "深色主题" });
            cmbTheme.SelectedIndex = 0;

            // 确定按钮
            btnOK = new Button();
            btnOK.Text = "确定";
            btnOK.Location = new Point(220, 170);
            btnOK.Size = new Size(75, 23);
            btnOK.DialogResult = DialogResult.OK;

#if WF2W
            btnOK.ClickAsync += async delegate (object sender, EventArgs e)
            {
                MessageBox.Show("设置已保存", "提示",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            };
#else
            btnOK.Click += BtnOK_Click;
#endif

            // 取消按钮
            btnCancel = new Button();
            btnCancel.Text = "取消";
            btnCancel.Location = new Point(305, 170);
            btnCancel.Size = new Size(75, 23);
            btnCancel.DialogResult = DialogResult.Cancel;

            // 事件绑定
            chkAutoSave.CheckedChanged += (s, e) =>
            {
                nudAutoSaveInterval.Enabled = chkAutoSave.Checked;
            };

            // 添加控件
            this.Controls.AddRange(new Control[] {
                chkAutoSave, lblAutoSaveInterval, nudAutoSaveInterval,
                chkShowTips, lblTheme, cmbTheme, btnOK, btnCancel
            });
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            // 保存设置
            MessageBox.Show("设置已保存", "提示",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}