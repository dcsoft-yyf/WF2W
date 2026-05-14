using System;
using System.Drawing;
using System.Windows.Forms;

namespace WF2WWinFormDemo.ControlsDemo
{
    public partial class frmTestCheckBox : Form
    {
        public frmTestCheckBox()
        {
            InitializeComponent();
        }

        private void frmTestCheckBox_Load(object sender, EventArgs e)
        {
            ControlsDemoUtils.FillCusors(this.cboCursor);
            ControlsDemoUtils.FillList<DockStyle>(this.cboDock);
            ControlsDemoUtils.FillList<FlatStyle>(this.cboFlatStyle);
            ControlsDemoUtils.FillList<ContentAlignment>(this.cboTextAlign);
            ControlsDemoUtils.FillList<ContentAlignment>(this.cboCheckAlign);
            ControlsDemoUtils.FillList<Appearance>(this.cboAppearance);

            this.txtLeft.Maximum = 10000;
            this.txtTop.Maximum = 10000;
            this.txtWidth.Maximum = 10000;
            this.txtHeight.Maximum = 10000;

            this.txtName.Text = this.chkDemo.Name;
            this.txtLeft.Value = Math.Min(this.txtLeft.Maximum, Math.Max(this.txtLeft.Minimum, this.chkDemo.Left));
            this.txtTop.Value = Math.Min(this.txtTop.Maximum, Math.Max(this.txtTop.Minimum, this.chkDemo.Top));
            this.txtWidth.Value = Math.Min(this.txtWidth.Maximum, Math.Max(this.txtWidth.Minimum, this.chkDemo.Width));
            this.txtHeight.Value = Math.Min(this.txtHeight.Maximum, Math.Max(this.txtHeight.Minimum, this.chkDemo.Height));
            this.txtText.Text = this.chkDemo.Text;

            this.cboDock.SelectedItem = this.chkDemo.Dock;
            this.cboFlatStyle.SelectedItem = this.chkDemo.FlatStyle;
            this.cboTextAlign.SelectedItem = this.chkDemo.TextAlign;
            this.cboCheckAlign.SelectedItem = this.chkDemo.CheckAlign;
            this.cboAppearance.SelectedItem = this.chkDemo.Appearance;

            for (int i = 0; i < this.cboCursor.Items.Count; i++)
            {
                var item = this.cboCursor.Items[i] as Tuple<string, Cursor>;
                if (item != null && item.Item2 == this.chkDemo.Cursor)
                {
                    this.cboCursor.SelectedIndex = i;
                    break;
                }
            }

            this.chkAutoSize.Checked = this.chkDemo.AutoSize;
            this.chkEnabled.Checked = this.chkDemo.Enabled;
            this.chkVisible.Checked = this.chkDemo.Visible;
            this.chkAutoCheck.Checked = this.chkDemo.AutoCheck;
            this.chkChecked.Checked = this.chkDemo.Checked;
            this.chkThreeState.Checked = this.chkDemo.ThreeState;
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            this.chkDemo.Name = this.txtName.Text;
            this.chkDemo.Left = (int)this.txtLeft.Value;
            this.chkDemo.Top = (int)this.txtTop.Value;
            this.chkDemo.Width = (int)this.txtWidth.Value;
            this.chkDemo.Height = (int)this.txtHeight.Value;
            this.chkDemo.Text = this.txtText.Text;

            if (this.cboDock.SelectedItem is DockStyle)
            {
                this.chkDemo.Dock = (DockStyle)this.cboDock.SelectedItem;
            }

            if (this.cboFlatStyle.SelectedItem is FlatStyle)
            {
                this.chkDemo.FlatStyle = (FlatStyle)this.cboFlatStyle.SelectedItem;
            }

            if (this.cboTextAlign.SelectedItem is ContentAlignment)
            {
                this.chkDemo.TextAlign = (ContentAlignment)this.cboTextAlign.SelectedItem;
            }

            if (this.cboCheckAlign.SelectedItem is ContentAlignment)
            {
                this.chkDemo.CheckAlign = (ContentAlignment)this.cboCheckAlign.SelectedItem;
            }

            if (this.cboAppearance.SelectedItem is Appearance)
            {
                this.chkDemo.Appearance = (Appearance)this.cboAppearance.SelectedItem;
            }

            var cursorItem = this.cboCursor.SelectedItem as Tuple<string, Cursor>;
            if (cursorItem != null)
            {
                this.chkDemo.Cursor = cursorItem.Item2;
            }

            this.chkDemo.AutoSize = this.chkAutoSize.Checked;
            this.chkDemo.Enabled = this.chkEnabled.Checked;
            this.chkDemo.Visible = this.chkVisible.Checked;
            this.chkDemo.AutoCheck = this.chkAutoCheck.Checked;
            this.chkDemo.Checked = this.chkChecked.Checked;
            this.chkDemo.ThreeState = this.chkThreeState.Checked;
        }

        private void btnFont_Click(object sender, EventArgs e)
        {
            this.BeginInvoke( async delegate ()
            {
                using (var dlg = new FontDialog())
                {
                    dlg.Font = this.chkDemo.Font;
                    if (
#if WF2W
                        await dlg.ShowDialog(this)
#else
                    dlg.ShowDialog(this)
#endif
                        == DialogResult.OK)
                    {
                        this.chkDemo.Font = dlg.Font;
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
                    dlg.Color = this.chkDemo.ForeColor;
                    if (
#if WF2W
                        await dlg.ShowDialog(this)
#else
                    dlg.ShowDialog(this)
#endif
                        == DialogResult.OK)
                    {
                        this.chkDemo.ForeColor = dlg.Color;
                    }
                }
            });
        }

        private void btnBackColor_Click(object sender, EventArgs e)
        {
            this.BeginInvoke(async delegate ()
            {
                using (var dlg = new ColorDialog())
                {
                    dlg.Color = this.chkDemo.BackColor;
                    if (
#if WF2W
                        await dlg.ShowDialog(this)
#else
                    dlg.ShowDialog(this)
#endif
                        == DialogResult.OK)
                    {
                        this.chkDemo.BackColor = dlg.Color;
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
                            this.chkDemo.BackgroundImage = img;
                        }
                        catch (Exception ext)
                        {
                            MessageBox.Show(this, "Failed to load image: " + ext.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            });
        }
    }
}
