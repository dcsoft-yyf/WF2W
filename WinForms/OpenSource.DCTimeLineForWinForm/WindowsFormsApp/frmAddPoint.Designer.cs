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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAddPoint));
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
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            this.SuspendLayout();
            // 
            // lblName
            // 
            resources.ApplyResources(this.lblName, "lblName");
            this.lblName.Name = "lblName";
            // 
            // lblTime
            // 
            resources.ApplyResources(this.lblTime, "lblTime");
            this.lblTime.Name = "lblTime";
            // 
            // lblText
            // 
            resources.ApplyResources(this.lblText, "lblText");
            this.lblText.Name = "lblText";
            // 
            // lblValue
            // 
            resources.ApplyResources(this.lblValue, "lblValue");
            this.lblValue.Name = "lblValue";
            // 
            // txtName
            // 
            resources.ApplyResources(this.txtName, "txtName");
            this.txtName.Name = "txtName";
            // 
            // txtText
            // 
            resources.ApplyResources(this.txtText, "txtText");
            this.txtText.Name = "txtText";
            // 
            // btnOK
            // 
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.Name = "btnOK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // dateTimePicker1
            // 
            resources.ApplyResources(this.dateTimePicker1, "dateTimePicker1");
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Name = "dateTimePicker1";
            // 
            // numericUpDown1
            // 
            resources.ApplyResources(this.numericUpDown1, "numericUpDown1");
            this.numericUpDown1.DecimalPlaces = 2;
            this.numericUpDown1.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.numericUpDown1.Name = "numericUpDown1";
            // 
            // numericUpDown2
            // 
            resources.ApplyResources(this.numericUpDown2, "numericUpDown2");
            this.numericUpDown2.DecimalPlaces = 2;
            this.numericUpDown2.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDown2.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.numericUpDown2.Name = "numericUpDown2";
            // 
            // chkLanternValue
            // 
            resources.ApplyResources(this.chkLanternValue, "chkLanternValue");
            this.chkLanternValue.Name = "chkLanternValue";
            this.chkLanternValue.UseVisualStyleBackColor = true;
            // 
            // chkSpecifySymbol
            // 
            resources.ApplyResources(this.chkSpecifySymbol, "chkSpecifySymbol");
            this.chkSpecifySymbol.Name = "chkSpecifySymbol";
            this.chkSpecifySymbol.UseVisualStyleBackColor = true;
            // 
            // cboboxSymbolType
            // 
            resources.ApplyResources(this.cboboxSymbolType, "cboboxSymbolType");
            this.cboboxSymbolType.FormattingEnabled = true;
            this.cboboxSymbolType.Name = "cboboxSymbolType";
            // 
            // labelAlign
            // 
            resources.ApplyResources(this.labelAlign, "labelAlign");
            this.labelAlign.Name = "labelAlign";
            // 
            // cboboxAlignment
            // 
            resources.ApplyResources(this.cboboxAlignment, "cboboxAlignment");
            this.cboboxAlignment.FormattingEnabled = true;
            this.cboboxAlignment.Name = "cboboxAlignment";
            // 
            // frmAddPoint
            // 
            this.AcceptButton = this.btnOK;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.Controls.Add(this.cboboxAlignment);
            this.Controls.Add(this.labelAlign);
            this.Controls.Add(this.cboboxSymbolType);
            this.Controls.Add(this.chkSpecifySymbol);
            this.Controls.Add(this.chkLanternValue);
            this.Controls.Add(this.numericUpDown2);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtText);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblValue);
            this.Controls.Add(this.lblText);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.lblName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAddPoint";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.frmAddPoint_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

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