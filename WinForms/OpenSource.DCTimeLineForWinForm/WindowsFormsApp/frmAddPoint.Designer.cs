using System.Windows.Forms;

namespace WindowsFormsApp
{
    partial class frmAddPoint
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.lblName = new System.Windows.Forms.Label();
			this.lblTime = new System.Windows.Forms.Label();
			this.lblText = new System.Windows.Forms.Label();
			this.lblValue = new System.Windows.Forms.Label();
			this.txtName = new System.Windows.Forms.TextBox();
			this.txtText = new System.Windows.Forms.TextBox();
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
			this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
			this.chkLanternValue = new System.Windows.Forms.CheckBox();
			this.chkSpecifySymbol = new System.Windows.Forms.CheckBox();
			this.cboboxSymbolType = new System.Windows.Forms.ComboBox();
			this.labelAlign = new System.Windows.Forms.Label();
			this.cboboxAlignment = new System.Windows.Forms.ComboBox();
			((System.ComponentModel.ISupportInitialize)this.numericUpDown1).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.numericUpDown2).BeginInit();
			base.SuspendLayout();
			this.lblName.AutoSize = true;
			this.lblName.Location = new System.Drawing.Point(27, 32);
			this.lblName.Name = "lblName";
			this.lblName.Size = new System.Drawing.Size(89, 12);
			this.lblName.TabIndex = 0;
			this.lblName.Text = "数据序列名称：";
			this.lblTime.AutoSize = true;
			this.lblTime.Location = new System.Drawing.Point(27, 77);
			this.lblTime.Name = "lblTime";
			this.lblTime.Size = new System.Drawing.Size(77, 12);
			this.lblTime.TabIndex = 0;
			this.lblTime.Text = "数据点时间：";
			this.lblText.AutoSize = true;
			this.lblText.Location = new System.Drawing.Point(27, 122);
			this.lblText.Name = "lblText";
			this.lblText.Size = new System.Drawing.Size(77, 12);
			this.lblText.TabIndex = 0;
			this.lblText.Text = "数据点文本：";
			this.lblValue.AutoSize = true;
			this.lblValue.Location = new System.Drawing.Point(27, 169);
			this.lblValue.Name = "lblValue";
			this.lblValue.Size = new System.Drawing.Size(77, 12);
			this.lblValue.TabIndex = 0;
			this.lblValue.Text = "数据点数值：";
			this.txtName.Location = new System.Drawing.Point(196, 29);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(200, 21);
			this.txtName.TabIndex = 1;
			this.txtText.Location = new System.Drawing.Point(196, 113);
			this.txtText.Multiline = true;
			this.txtText.Name = "txtText";
			this.txtText.Size = new System.Drawing.Size(200, 36);
			this.txtText.TabIndex = 1;
			this.btnOK.Location = new System.Drawing.Point(29, 320);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(112, 21);
			this.btnOK.TabIndex = 2;
			this.btnOK.Text = "加点";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(btnOK_Click);
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(277, 320);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(119, 21);
			this.btnCancel.TabIndex = 2;
			this.btnCancel.Text = "取消";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(btnCancel_Click);
			this.dateTimePicker1.CustomFormat = "yyyy-MM-dd HH:mm:ss";
			this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dateTimePicker1.Location = new System.Drawing.Point(196, 73);
			this.dateTimePicker1.Name = "dateTimePicker1";
			this.dateTimePicker1.Size = new System.Drawing.Size(200, 21);
			this.dateTimePicker1.TabIndex = 3;
			this.numericUpDown1.DecimalPlaces = 2;
			this.numericUpDown1.Location = new System.Drawing.Point(196, 165);
			this.numericUpDown1.Maximum = new decimal(new int[4] { 10000, 0, 0, 0 });
			this.numericUpDown1.Minimum = new decimal(new int[4] { 10000, 0, 0, -2147483648 });
			this.numericUpDown1.Name = "numericUpDown1";
			this.numericUpDown1.Size = new System.Drawing.Size(200, 21);
			this.numericUpDown1.TabIndex = 4;
			this.numericUpDown2.DecimalPlaces = 2;
			this.numericUpDown2.Location = new System.Drawing.Point(196, 280);
			this.numericUpDown2.Maximum = new decimal(new int[4] { 10000, 0, 0, 0 });
			this.numericUpDown2.Minimum = new decimal(new int[4] { 10000, 0, 0, -2147483648 });
			this.numericUpDown2.Name = "numericUpDown2";
			this.numericUpDown2.Size = new System.Drawing.Size(200, 21);
			this.numericUpDown2.TabIndex = 4;
			this.chkLanternValue.AutoSize = true;
			this.chkLanternValue.Location = new System.Drawing.Point(30, 285);
			this.chkLanternValue.Name = "chkLanternValue";
			this.chkLanternValue.Size = new System.Drawing.Size(84, 16);
			this.chkLanternValue.TabIndex = 5;
			this.chkLanternValue.Text = "使用灯笼值";
			this.chkLanternValue.UseVisualStyleBackColor = true;
			this.chkSpecifySymbol.AutoSize = true;
			this.chkSpecifySymbol.Location = new System.Drawing.Point(29, 247);
			this.chkSpecifySymbol.Name = "chkSpecifySymbol";
			this.chkSpecifySymbol.Size = new System.Drawing.Size(84, 16);
			this.chkSpecifySymbol.TabIndex = 6;
			this.chkSpecifySymbol.Text = "自定义图例";
			this.chkSpecifySymbol.UseVisualStyleBackColor = true;
			this.cboboxSymbolType.FormattingEnabled = true;
			this.cboboxSymbolType.Location = new System.Drawing.Point(196, 243);
			this.cboboxSymbolType.Name = "cboboxSymbolType";
			this.cboboxSymbolType.Size = new System.Drawing.Size(200, 20);
			this.cboboxSymbolType.TabIndex = 7;
			this.labelAlign.AutoSize = true;
			this.labelAlign.Location = new System.Drawing.Point(27, 210);
			this.labelAlign.Name = "labelAlign";
			this.labelAlign.Size = new System.Drawing.Size(149, 12);
			this.labelAlign.TabIndex = 8;
			this.labelAlign.Text = "数据点文本水平对齐方式：";
			this.cboboxAlignment.FormattingEnabled = true;
			this.cboboxAlignment.Location = new System.Drawing.Point(196, 202);
			this.cboboxAlignment.Name = "cboboxAlignment";
			this.cboboxAlignment.Size = new System.Drawing.Size(200, 20);
			this.cboboxAlignment.TabIndex = 9;
			base.AcceptButton = this.btnOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.btnCancel;
			base.ClientSize = new System.Drawing.Size(414, 353);
			base.Controls.Add(this.cboboxAlignment);
			base.Controls.Add(this.labelAlign);
			base.Controls.Add(this.cboboxSymbolType);
			base.Controls.Add(this.chkSpecifySymbol);
			base.Controls.Add(this.chkLanternValue);
			base.Controls.Add(this.numericUpDown2);
			base.Controls.Add(this.numericUpDown1);
			base.Controls.Add(this.dateTimePicker1);
			base.Controls.Add(this.btnCancel);
			base.Controls.Add(this.btnOK);
			base.Controls.Add(this.txtText);
			base.Controls.Add(this.txtName);
			base.Controls.Add(this.lblValue);
			base.Controls.Add(this.lblText);
			base.Controls.Add(this.lblTime);
			base.Controls.Add(this.lblName);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "frmAddPoint";
			base.ShowInTaskbar = false;
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "设置时间轴数据点";
			base.Load += new System.EventHandler(frmAddPoint_Load);
			((System.ComponentModel.ISupportInitialize)this.numericUpDown1).EndInit();
			((System.ComponentModel.ISupportInitialize)this.numericUpDown2).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private Label lblName;

		private Label lblTime;

		private Label lblText;

		private Label lblValue;

		private TextBox txtName;

		private TextBox txtText;

		private Button btnOK;

		private Button btnCancel;

		private DateTimePicker dateTimePicker1;

		private NumericUpDown numericUpDown1;

		private NumericUpDown numericUpDown2;

		private CheckBox chkLanternValue;

		private CheckBox chkSpecifySymbol;

		private ComboBox cboboxSymbolType;

		private Label labelAlign;

		private ComboBox cboboxAlignment;

		#endregion
	}
}