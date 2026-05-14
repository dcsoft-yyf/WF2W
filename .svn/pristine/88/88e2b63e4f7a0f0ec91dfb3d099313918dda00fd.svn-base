using System;
using System.Drawing;
using System.Windows.Forms;

namespace WF2WWinFormDemo.ControlsDemo
{
    public partial class frmTestDataGridView : Form
    {
        public frmTestDataGridView()
        {
            InitializeComponent();
        }

        private void frmTestDataGridView_Load(object sender, EventArgs e)
        {
            ControlsDemoUtils.FillCusors(this.cboCursor);
            ControlsDemoUtils.FillList<DockStyle>(this.cboDock);

            this.txtLeft.Maximum = 10000;
            this.txtTop.Maximum = 10000;
            this.txtWidth.Maximum = 10000;
            this.txtHeight.Maximum = 10000;

            this.txtName.Text = this.ctlDemo.Name;
            this.txtLeft.Value = Math.Min(this.txtLeft.Maximum, Math.Max(this.txtLeft.Minimum, this.ctlDemo.Left));
            this.txtTop.Value = Math.Min(this.txtTop.Maximum, Math.Max(this.txtTop.Minimum, this.ctlDemo.Top));
            this.txtWidth.Value = Math.Min(this.txtWidth.Maximum, Math.Max(this.txtWidth.Minimum, this.ctlDemo.Width));
            this.txtHeight.Value = Math.Min(this.txtHeight.Maximum, Math.Max(this.txtHeight.Minimum, this.ctlDemo.Height));
            this.txtText.Text = this.ctlDemo.Text;
            this.cboDock.SelectedItem = this.ctlDemo.Dock;

            for (int i = 0; i < this.cboCursor.Items.Count; i++)
            {
                var item = this.cboCursor.Items[i] as Tuple<string, Cursor>;
                if (item != null && item.Item2 == this.ctlDemo.Cursor)
                {
                    this.cboCursor.SelectedIndex = i;
                    break;
                }
            }

            this.chkEnabled.Checked = this.ctlDemo.Enabled;
            this.chkVisible.Checked = this.ctlDemo.Visible;
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            this.ctlDemo.Name = this.txtName.Text;
            this.ctlDemo.Left = (int)this.txtLeft.Value;
            this.ctlDemo.Top = (int)this.txtTop.Value;
            this.ctlDemo.Width = (int)this.txtWidth.Value;
            this.ctlDemo.Height = (int)this.txtHeight.Value;
            this.ctlDemo.Text = this.txtText.Text;

            if (this.cboDock.SelectedItem is DockStyle)
            {
                this.ctlDemo.Dock = (DockStyle)this.cboDock.SelectedItem;
            }

            var cursorItem = this.cboCursor.SelectedItem as Tuple<string, Cursor>;
            if (cursorItem != null)
            {
                this.ctlDemo.Cursor = cursorItem.Item2;
            }

            this.ctlDemo.Enabled = this.chkEnabled.Checked;
            this.ctlDemo.Visible = this.chkVisible.Checked;
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
                    dlg.Font = this.ctlDemo.Font;
                    if (
#if WF2W
                        await dlg.ShowDialog(this)
#else
                        dlg.ShowDialog(this)
#endif
                        == DialogResult.OK)
                    {
                        this.ctlDemo.Font = dlg.Font;
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
                    dlg.Color = this.ctlDemo.ForeColor;
                    if (
#if WF2W
                        await dlg.ShowDialog(this)
#else
                        dlg.ShowDialog(this)
#endif
                        == DialogResult.OK)
                    {
                        this.ctlDemo.ForeColor = dlg.Color;
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
                    dlg.Color = this.ctlDemo.BackColor;
                    if (
#if WF2W
                        await dlg.ShowDialog(this)
#else
                        dlg.ShowDialog(this)
#endif
                        == DialogResult.OK)
                    {
                        this.ctlDemo.BackColor = dlg.Color;
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
                        this.ctlDemo.BackgroundImage = ControlsDemoUtils.LoadImageAndCloseStream(
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
