using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WF2WWinFormDemo.ControlsDemo
{
    /// <summary>
    /// System.Windows.Forms.Button control test form
    /// </summary>
    public partial class frmTestButton : Form
    {
        public frmTestButton()
        {
            InitializeComponent();
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {

        }

        private void frmTestButton_Load(object sender, EventArgs e)
        {
            ControlsDemoUtils.FillCusors(this.cboCursor);
            ControlsDemoUtils.FillList<DockStyle>(this.cboDock);
            ControlsDemoUtils.FillList<FlatStyle>(this.cboFlatStyle);
            ControlsDemoUtils.FillList<ContentAlignment>(this.cboTextAlign);

            this.txtLeft.Maximum = 10000;
            this.txtTop.Maximum = 10000;
            this.txtWidth.Maximum = 10000;
            this.txtHeight.Maximum = 10000;

            this.txtName.Text = this.btnDemo.Name;
            this.txtLeft.Value = Math.Min(this.txtLeft.Maximum, Math.Max(this.txtLeft.Minimum, this.btnDemo.Left));
            this.txtTop.Value = Math.Min(this.txtTop.Maximum, Math.Max(this.txtTop.Minimum, this.btnDemo.Top));
            this.txtWidth.Value = Math.Min(this.txtWidth.Maximum, Math.Max(this.txtWidth.Minimum, this.btnDemo.Width));
            this.txtHeight.Value = Math.Min(this.txtHeight.Maximum, Math.Max(this.txtHeight.Minimum, this.btnDemo.Height));
            this.txtText.Text = this.btnDemo.Text;

            this.cboDock.SelectedItem = this.btnDemo.Dock;
            this.cboFlatStyle.SelectedItem = this.btnDemo.FlatStyle;
            this.cboTextAlign.SelectedItem = this.btnDemo.TextAlign;

            for (int i = 0; i < this.cboCursor.Items.Count; i++)
            {
                var item = this.cboCursor.Items[i] as Tuple<string, Cursor>;
                if (item != null && item.Item2 == this.btnDemo.Cursor)
                {
                    this.cboCursor.SelectedIndex = i;
                    break;
                }
            }

            this.chkAutoSize.Checked = this.btnDemo.AutoSize;
            this.chkEnabled.Checked = this.btnDemo.Enabled;
            this.chkVisible.Checked = this.btnDemo.Visible;

        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            this.btnDemo.Name = this.txtName.Text;
            this.btnDemo.Left = (int)this.txtLeft.Value;
            this.btnDemo.Top = (int)this.txtTop.Value;
            this.btnDemo.Width = (int)this.txtWidth.Value;
            this.btnDemo.Height = (int)this.txtHeight.Value;
            this.btnDemo.Text = this.txtText.Text;

            if (this.cboDock.SelectedItem is DockStyle)
            {
                this.btnDemo.Dock = (DockStyle)this.cboDock.SelectedItem;
            }

            if (this.cboFlatStyle.SelectedItem is FlatStyle)
            {
                this.btnDemo.FlatStyle = (FlatStyle)this.cboFlatStyle.SelectedItem;
            }

            if (this.cboTextAlign.SelectedItem is ContentAlignment)
            {
                this.btnDemo.TextAlign = (ContentAlignment)this.cboTextAlign.SelectedItem;
            }

            var cursorItem = this.cboCursor.SelectedItem as Tuple<string, Cursor>;
            if (cursorItem != null)
            {
                this.btnDemo.Cursor = cursorItem.Item2;
            }

            this.btnDemo.AutoSize = this.chkAutoSize.Checked;
            this.btnDemo.Enabled = this.chkEnabled.Checked;
            this.btnDemo.Visible = this.chkVisible.Checked;

        }

        private void btnFont_Click(object sender, EventArgs e)
        {
            this.BeginInvoke(async delegate ()
            {
                using (var dlg = new FontDialog())
                {
                    dlg.Font = this.btnDemo.Font;
                    if (
#if WF2W
                        await
#endif
                        dlg.ShowDialog(this) == DialogResult.OK)
                    {
                        this.btnDemo.Font = dlg.Font;

                    }
                }
            });
        }

        private void btnForeColor_Click(object sender, EventArgs e)
        {
            this.BeginInvoke(async delegate ()
            {
                using (var dlg = new ColorDialog())
                {
                    dlg.Color = this.btnDemo.ForeColor;
                    if (
#if WF2W
                        await dlg.ShowDialog(this)
#else
                        dlg.ShowDialog(this)
#endif
                        == DialogResult.OK)
                    {
                        this.btnDemo.ForeColor = dlg.Color;

                    }
                }
            });
        }

        private void btnBackColor_Click(object sender, EventArgs e)
        {
            this.BeginInvoke(async delegate ()
            {
                using(var dlg = new ColorDialog())
                {
                    dlg.Color = this.btnDemo.BackColor;
                    if (
#if WF2W
                        await dlg.ShowDialog(this)
#else
                        dlg.ShowDialog(this)
#endif
                        == DialogResult.OK)
                    {
                        this.btnDemo.BackColor = dlg.Color;
                    }
                }
            });
        }

        private void btnBackgroundImage_Click(object sender, EventArgs e)
        {
            this.BeginInvoke(async delegate ()
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
                        try
                        {
                            var img = ControlsDemoUtils.LoadImageAndCloseStream(
#if WF2W
                            await dlg.OpenFile()
#else
                        dlg.OpenFile()
#endif
                            );
                            this.btnDemo.BackgroundImage = img;
                        }
                        catch (Exception ext)
                        {
                            MessageBox.Show(this, "Failed to load image: " + ext.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            });
        }

        private void btnDemo_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this, "Button click ");
        }
    }
}
