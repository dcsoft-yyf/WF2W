$forms = @(
'Button','BindingNavigator','CheckBox','CheckedListBox','ComboBox','ContainerControl','ContextMenuStrip','DataGrid','DataGridTextBox','DataGridView',
'DataGridViewComboBoxEditingControl','DataGridViewTextBoxEditingControl','DateTimePicker','DomainUpDown','FlowLayoutPanel','Form','GroupBox','HScrollBar',
'Label','LinkLabel','ListBox','ListView','MaskedTextBox','MdiClient','MenuStrip','MonthCalendar','NumericUpDown','Panel','PictureBox','PrintPreviewControl',
'PrintPreviewDialog','ProgressBar','PropertyGrid','RadioButton','RichTextBox','ScrollableControl','SplitContainer','Splitter','StatusBar','StatusStrip',
'TabControl','TableLayoutPanel','TabPage','TextBox','ToolBar','ToolStrip','ToolStripContainer','ToolStripContentPanel','ToolStripDropDown','ToolStripDropDownMenu',
'ToolStripPanel','TrackBar','TreeView','UserControl','VScrollBar','WebBrowser'
)

$nl = [Environment]::NewLine

$cs = New-Object System.Text.StringBuilder
[void]$cs.Append('using System;' + $nl + 'using System.Windows.Forms;' + $nl + $nl + 'namespace WF2WWinFormDemo.ControlsDemo' + $nl + '{' + $nl + '    public partial class frmMain : Form' + $nl + '    {' + $nl + '        public frmMain()' + $nl + '        {' + $nl + '            InitializeComponent();' + $nl + '        }' + $nl + $nl + '        private void frmMain_Load(object sender, EventArgs e)' + $nl + '        {' + $nl + '        }' + $nl)

foreach($f in $forms)
{
    [void]$cs.Append($nl + '        private void btnTest' + $f + '_Click(object sender, EventArgs e)' + $nl + '        {' + $nl + '            using (var dlg = new frmTest' + $f + '())' + $nl + '            {' + $nl + '                dlg.ShowDialog(this);' + $nl + '            }' + $nl + '        }' + $nl)
}

[void]$cs.Append('    }' + $nl + '}' + $nl)
Set-Content -Path 'frmMain.generated.cs' -Value $cs.ToString() -Encoding UTF8

$ds = New-Object System.Text.StringBuilder
[void]$ds.Append('namespace WF2WWinFormDemo.ControlsDemo' + $nl + '{' + $nl + '    partial class frmMain' + $nl + '    {' + $nl + '        private System.ComponentModel.IContainer components = null;' + $nl + $nl + '        protected override void Dispose(bool disposing)' + $nl + '        {' + $nl + '            if (disposing && (components != null))' + $nl + '            {' + $nl + '                components.Dispose();' + $nl + '            }' + $nl + '            base.Dispose(disposing);' + $nl + '        }' + $nl + $nl + '        private void InitializeComponent()' + $nl + '        {' + $nl + '            this.pnlButtons = new System.Windows.Forms.FlowLayoutPanel();' + $nl)

foreach($f in $forms)
{
    [void]$ds.Append('            this.btnTest' + $f + ' = new System.Windows.Forms.Button();' + $nl)
}

[void]$ds.Append('            this.pnlButtons.SuspendLayout();' + $nl + '            this.SuspendLayout();' + $nl + '            this.pnlButtons.AutoScroll = true;' + $nl + '            this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Fill;' + $nl + '            this.pnlButtons.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;' + $nl + '            this.pnlButtons.Location = new System.Drawing.Point(0, 0);' + $nl + '            this.pnlButtons.Name = "pnlButtons";' + $nl + '            this.pnlButtons.Padding = new System.Windows.Forms.Padding(12);' + $nl + '            this.pnlButtons.Size = new System.Drawing.Size(851, 758);' + $nl + '            this.pnlButtons.TabIndex = 0;' + $nl + '            this.pnlButtons.WrapContents = false;' + $nl)

$tabIndex = 0
foreach($f in $forms)
{
    [void]$ds.Append('            this.btnTest' + $f + '.Location = new System.Drawing.Point(15, 15);' + $nl)
    [void]$ds.Append('            this.btnTest' + $f + '.Name = "btnTest' + $f + '";' + $nl)
    [void]$ds.Append('            this.btnTest' + $f + '.Size = new System.Drawing.Size(260, 28);' + $nl)
    [void]$ds.Append('            this.btnTest' + $f + '.TabIndex = ' + $tabIndex + ';' + $nl)
    [void]$ds.Append('            this.btnTest' + $f + '.Text = "' + $f + '";' + $nl)
    [void]$ds.Append('            this.btnTest' + $f + '.UseVisualStyleBackColor = true;' + $nl)
    [void]$ds.Append('            this.btnTest' + $f + '.Click += new System.EventHandler(this.btnTest' + $f + '_Click);' + $nl)
    [void]$ds.Append('            this.pnlButtons.Controls.Add(this.btnTest' + $f + ');' + $nl)
    $tabIndex++
}

[void]$ds.Append('            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);' + $nl + '            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;' + $nl + '            this.ClientSize = new System.Drawing.Size(851, 758);' + $nl + '            this.Controls.Add(this.pnlButtons);' + $nl + '            this.MinimizeBox = false;' + $nl + '            this.Name = "frmMain";' + $nl + '            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;' + $nl + '            this.Text = "WinForms Control demos";' + $nl + '            this.Load += new System.EventHandler(this.frmMain_Load);' + $nl + '            this.pnlButtons.ResumeLayout(false);' + $nl + '            this.ResumeLayout(false);' + $nl + '        }' + $nl + $nl + '        private System.Windows.Forms.FlowLayoutPanel pnlButtons;' + $nl)

foreach($f in $forms)
{
    [void]$ds.Append('        private System.Windows.Forms.Button btnTest' + $f + ';' + $nl)
}

[void]$ds.Append('    }' + $nl + '}' + $nl)
Set-Content -Path 'frmMain.Designer.generated.cs' -Value $ds.ToString() -Encoding UTF8
