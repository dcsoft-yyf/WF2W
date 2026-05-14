namespace WF2WWinFormDemo.ControlsDemo
{
    partial class frmTestProgressBar
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblLeft;
        private System.Windows.Forms.Label lblTop;
        private System.Windows.Forms.Label lblWidth;
        private System.Windows.Forms.Label lblHeight;
        private System.Windows.Forms.Label lblText;
        private System.Windows.Forms.Label lblCursor;
        private System.Windows.Forms.Label lblDock;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.NumericUpDown txtLeft;
        private System.Windows.Forms.NumericUpDown txtTop;
        private System.Windows.Forms.NumericUpDown txtWidth;
        private System.Windows.Forms.NumericUpDown txtHeight;
        private System.Windows.Forms.TextBox txtText;
        private System.Windows.Forms.ComboBox cboCursor;
        private System.Windows.Forms.ComboBox cboDock;
        private System.Windows.Forms.CheckBox chkEnabled;
        private System.Windows.Forms.CheckBox chkVisible;
        private System.Windows.Forms.Button btnFont;
        private System.Windows.Forms.Button btnForeColor;
        private System.Windows.Forms.Button btnBackColor;
        private System.Windows.Forms.Button btnBackgroundImage;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ProgressBar ctlDemo;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.btnApply = new System.Windows.Forms.Button();
            this.lblName = new System.Windows.Forms.Label();
            this.lblLeft = new System.Windows.Forms.Label();
            this.lblTop = new System.Windows.Forms.Label();
            this.lblWidth = new System.Windows.Forms.Label();
            this.lblHeight = new System.Windows.Forms.Label();
            this.lblText = new System.Windows.Forms.Label();
            this.lblCursor = new System.Windows.Forms.Label();
            this.lblDock = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtLeft = new System.Windows.Forms.NumericUpDown();
            this.txtTop = new System.Windows.Forms.NumericUpDown();
            this.txtWidth = new System.Windows.Forms.NumericUpDown();
            this.txtHeight = new System.Windows.Forms.NumericUpDown();
            this.txtText = new System.Windows.Forms.TextBox();
            this.cboCursor = new System.Windows.Forms.ComboBox();
            this.cboDock = new System.Windows.Forms.ComboBox();
            this.chkEnabled = new System.Windows.Forms.CheckBox();
            this.chkVisible = new System.Windows.Forms.CheckBox();
            this.btnFont = new System.Windows.Forms.Button();
            this.btnForeColor = new System.Windows.Forms.Button();
            this.btnBackColor = new System.Windows.Forms.Button();
            this.btnBackgroundImage = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ctlDemo = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.txtLeft)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHeight)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            this.btnApply.Location = new System.Drawing.Point(12, 12); this.btnApply.Size = new System.Drawing.Size(210, 23); this.btnApply.Text = "Apply"; this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            this.lblName.Location = new System.Drawing.Point(12, 49); this.lblName.Size = new System.Drawing.Size(50, 13); this.lblName.Text = "Name:";
            this.lblLeft.Location = new System.Drawing.Point(12, 75); this.lblLeft.Size = new System.Drawing.Size(50, 13); this.lblLeft.Text = "Left:";
            this.lblTop.Location = new System.Drawing.Point(12, 101); this.lblTop.Size = new System.Drawing.Size(50, 13); this.lblTop.Text = "Top:";
            this.lblWidth.Location = new System.Drawing.Point(12, 127); this.lblWidth.Size = new System.Drawing.Size(50, 13); this.lblWidth.Text = "Width:";
            this.lblHeight.Location = new System.Drawing.Point(12, 153); this.lblHeight.Size = new System.Drawing.Size(50, 13); this.lblHeight.Text = "Height:";
            this.lblText.Location = new System.Drawing.Point(12, 179); this.lblText.Size = new System.Drawing.Size(50, 13); this.lblText.Text = "Text:";
            this.lblCursor.Location = new System.Drawing.Point(12, 321); this.lblCursor.Size = new System.Drawing.Size(50, 13); this.lblCursor.Text = "Cursor:";
            this.lblDock.Location = new System.Drawing.Point(12, 348); this.lblDock.Size = new System.Drawing.Size(50, 13); this.lblDock.Text = "Dock:";
            this.txtName.Location = new System.Drawing.Point(69, 46); this.txtName.Size = new System.Drawing.Size(153, 20);
            this.txtLeft.Location = new System.Drawing.Point(69, 72); this.txtLeft.Size = new System.Drawing.Size(153, 20);
            this.txtTop.Location = new System.Drawing.Point(69, 98); this.txtTop.Size = new System.Drawing.Size(153, 20);
            this.txtWidth.Location = new System.Drawing.Point(69, 124); this.txtWidth.Size = new System.Drawing.Size(153, 20);
            this.txtHeight.Location = new System.Drawing.Point(69, 150); this.txtHeight.Size = new System.Drawing.Size(153, 20);
            this.txtText.Location = new System.Drawing.Point(69, 176); this.txtText.Size = new System.Drawing.Size(153, 20);
            this.btnFont.Location = new System.Drawing.Point(12, 201); this.btnFont.Size = new System.Drawing.Size(210, 23); this.btnFont.Text = "Font..."; this.btnFont.Click += new System.EventHandler(this.btnFont_Click);
            this.btnForeColor.Location = new System.Drawing.Point(12, 230); this.btnForeColor.Size = new System.Drawing.Size(210, 23); this.btnForeColor.Text = "ForeColor..."; this.btnForeColor.Click += new System.EventHandler(this.btnForeColor_Click);
            this.btnBackColor.Location = new System.Drawing.Point(12, 259); this.btnBackColor.Size = new System.Drawing.Size(210, 23); this.btnBackColor.Text = "BackColor..."; this.btnBackColor.Click += new System.EventHandler(this.btnBackColor_Click);
            this.btnBackgroundImage.Location = new System.Drawing.Point(12, 288); this.btnBackgroundImage.Size = new System.Drawing.Size(210, 23); this.btnBackgroundImage.Text = "BackgroundImage..."; this.btnBackgroundImage.Click += new System.EventHandler(this.btnBackgroundImage_Click);
            this.cboCursor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList; this.cboCursor.Location = new System.Drawing.Point(69, 317); this.cboCursor.Size = new System.Drawing.Size(153, 21);
            this.cboDock.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList; this.cboDock.Location = new System.Drawing.Point(69, 344); this.cboDock.Size = new System.Drawing.Size(153, 21);
            this.chkEnabled.Location = new System.Drawing.Point(12, 371); this.chkEnabled.Text = "Enabled";
            this.chkVisible.Location = new System.Drawing.Point(12, 394); this.chkVisible.Text = "Visible";
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right))); this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D; this.panel1.Location = new System.Drawing.Point(241, 0); this.panel1.Size = new System.Drawing.Size(569, 621);
            this.ctlDemo.Location = new System.Drawing.Point(120, 220); this.panel1.Controls.Add(this.ctlDemo);
            this.ClientSize = new System.Drawing.Size(813, 633);
            this.Controls.Add(this.panel1); this.Controls.Add(this.chkVisible); this.Controls.Add(this.chkEnabled); this.Controls.Add(this.cboDock); this.Controls.Add(this.cboCursor); this.Controls.Add(this.btnBackgroundImage); this.Controls.Add(this.btnBackColor); this.Controls.Add(this.btnForeColor); this.Controls.Add(this.btnFont); this.Controls.Add(this.txtText); this.Controls.Add(this.txtHeight); this.Controls.Add(this.txtWidth); this.Controls.Add(this.txtTop); this.Controls.Add(this.txtLeft); this.Controls.Add(this.txtName); this.Controls.Add(this.lblDock); this.Controls.Add(this.lblCursor); this.Controls.Add(this.lblText); this.Controls.Add(this.lblHeight); this.Controls.Add(this.lblWidth); this.Controls.Add(this.lblTop); this.Controls.Add(this.lblLeft); this.Controls.Add(this.lblName); this.Controls.Add(this.btnApply);
            this.Name = "frmTestProgressBar";
            this.Text = "Test ProgressBar";
            this.Load += new System.EventHandler(this.frmTestProgressBar_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtLeft)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHeight)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
