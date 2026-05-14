using System;
using System.Drawing;
using System.Windows.Forms;

namespace WF2WWinFormDemo.ControlsDemo
{
    public partial class frmTestComboBox : Form
    {
        public frmTestComboBox()
        {
            InitializeComponent();
        }

        private void frmTestComboBox_Load(object sender, EventArgs e)
        {
            ControlsDemoUtils.FillCusors(this.cboCursor);
            ControlsDemoUtils.FillList<DockStyle>(this.cboDock);

            this.txtLeft.Maximum = 10000;
            this.txtTop.Maximum = 10000;
            this.txtWidth.Maximum = 10000;
            this.txtHeight.Maximum = 10000;

            this.txtName.Text = this.cboDemo.Name;
            this.txtLeft.Value = Math.Min(this.txtLeft.Maximum, Math.Max(this.txtLeft.Minimum, this.cboDemo.Left));
            this.txtTop.Value = Math.Min(this.txtTop.Maximum, Math.Max(this.txtTop.Minimum, this.cboDemo.Top));
            this.txtWidth.Value = Math.Min(this.txtWidth.Maximum, Math.Max(this.txtWidth.Minimum, this.cboDemo.Width));
            this.txtHeight.Value = Math.Min(this.txtHeight.Maximum, Math.Max(this.txtHeight.Minimum, this.cboDemo.Height));
            this.txtText.Text = this.cboDemo.Text;
            this.cboDock.SelectedItem = this.cboDemo.Dock;

            for (int i = 0; i < this.cboCursor.Items.Count; i++)
            {
                var item = this.cboCursor.Items[i] as Tuple<string, Cursor>;
                if (item != null && item.Item2 == this.cboDemo.Cursor)
                {
                    this.cboCursor.SelectedIndex = i;
                    break;
                }
            }

            this.chkEnabled.Checked = this.cboDemo.Enabled;
            this.chkVisible.Checked = this.cboDemo.Visible;
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            this.cboDemo.Name = this.txtName.Text;
            this.cboDemo.Left = (int)this.txtLeft.Value;
            this.cboDemo.Top = (int)this.txtTop.Value;
            this.cboDemo.Width = (int)this.txtWidth.Value;
            this.cboDemo.Height = (int)this.txtHeight.Value;
            this.cboDemo.Text = this.txtText.Text;

            if (this.cboDock.SelectedItem is DockStyle)
            {
                this.cboDemo.Dock = (DockStyle)this.cboDock.SelectedItem;
            }

            var cursorItem = this.cboCursor.SelectedItem as Tuple<string, Cursor>;
            if (cursorItem != null)
            {
                this.cboDemo.Cursor = cursorItem.Item2;
            }

            this.cboDemo.Enabled = this.chkEnabled.Checked;
            this.cboDemo.Visible = this.chkVisible.Checked;
        }

        private void btnFont_Click(object sender, EventArgs e)
        {
            this.BeginInvoke(
#if WF2W
                async
#endif
                delegate ()
            {
                using (var dlg = new FontDialog())
                {
                    dlg.Font = this.cboDemo.Font;
                    if (
#if WF2W
                        await dlg.ShowDialog(this)
#else
                        dlg.ShowDialog(this)
#endif
                        == DialogResult.OK)
                    {
                        this.cboDemo.Font = dlg.Font;
                    }
                }
            });
        }

        private void btnForeColor_Click(object sender, EventArgs e)
        {
            this.BeginInvoke(
#if WF2W
                async
#endif
                delegate ()
            {
                using (var dlg = new ColorDialog())
                {
                    dlg.Color = this.cboDemo.ForeColor;
                    if (
#if WF2W
                        await dlg.ShowDialog(this)
#else
                        dlg.ShowDialog(this)
#endif
                        == DialogResult.OK)
                    {
                        this.cboDemo.ForeColor = dlg.Color;
                    }
                }
            });
        }

        private void btnBackColor_Click(object sender, EventArgs e)
        {
            this.BeginInvoke(
#if WF2W
                async
#endif
                delegate ()
            {
                using (var dlg = new ColorDialog())
                {
                    dlg.Color = this.cboDemo.BackColor;
                    if (
#if WF2W
                        await dlg.ShowDialog(this)
#else
                        dlg.ShowDialog(this)
#endif
                        == DialogResult.OK)
                    {
                        this.cboDemo.BackColor = dlg.Color;
                    }
                }
            });
        }

        private void btnBackgroundImage_Click(object sender, EventArgs e)
        {
            this.BeginInvoke(
#if WF2W
                async
#endif
                delegate ()
            {
                using (var dlg = new OpenFileDialog())
                {
                    dlg.Filter = "Image files|*.bmp;*.jpg;*.jpeg;*.png;*.gif|All files|*.*";
                    if (
#if WF2W
                        await dlg.ShowDialog(this)
#else
                        dlg.ShowDialog(this)
#endif
                        == DialogResult.OK)
                    {
                        this.cboDemo.BackgroundImage = ControlsDemoUtils.LoadImageAndCloseStream(
#if WF2W
                        await dlg.OpenFile()
#else
                        dlg.OpenFile()
#endif
                        );
                    }
                }
            });
        }
    }
}
