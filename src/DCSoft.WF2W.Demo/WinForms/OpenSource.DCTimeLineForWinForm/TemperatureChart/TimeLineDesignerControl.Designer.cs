namespace DCSoft.TemperatureChart
{
    partial class TimeLineDesignerControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TimeLineDesignerControl));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnTreeView = new System.Windows.Forms.ToolStripButton();
            this.btnViewMode = new System.Windows.Forms.ToolStripDropDownButton();
            this.btnNormalViewMode = new System.Windows.Forms.ToolStripMenuItem();
            this.btnPageViewMode = new System.Windows.Forms.ToolStripMenuItem();
            this.btnWidelyViewMode = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.mShowLocalPropertyName = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnPageSettings = new System.Windows.Forms.ToolStripButton();
            this.btnInsert = new System.Windows.Forms.ToolStripDropDownButton();
            this.mInsertHeaderLabel = new System.Windows.Forms.ToolStripMenuItem();
            this.mInsertHeaderLine = new System.Windows.Forms.ToolStripMenuItem();
            this.mInsertYAxis = new System.Windows.Forms.ToolStripMenuItem();
            this.mInsertFooterLine = new System.Windows.Forms.ToolStripMenuItem();
            this.menuInsertLabel = new System.Windows.Forms.ToolStripMenuItem();
            this.mNewTimeZone = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.mCloneInsert = new System.Windows.Forms.ToolStripMenuItem();
            this.btnMovePre = new System.Windows.Forms.ToolStripButton();
            this.btnMoveNext = new System.Windows.Forms.ToolStripButton();
            this.btnDelete = new System.Windows.Forms.ToolStripButton();
            this.btnViewXMLSource = new System.Windows.Forms.ToolStripButton();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnOK = new System.Windows.Forms.ToolStripButton();
            this.btnCancel = new System.Windows.Forms.ToolStripButton();
            this.ctlProerty = new System.Windows.Forms.PropertyGrid();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.spTreeView = new System.Windows.Forms.Splitter();
            this.tvwDOM = new System.Windows.Forms.TreeView();
            this.ctlTimeLine = new DCSoft.TemperatureChart.TemperatureViewControl();
            this.toolStrip1.SuspendLayout();
           // ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnTreeView,
            this.btnViewMode,
            this.toolStripSeparator1,
            this.btnPageSettings,
            this.btnInsert,
            this.btnMovePre,
            this.btnMoveNext,
            this.btnDelete,
            this.btnViewXMLSource,
            this.btnSave,
            this.toolStripSeparator2,
            this.btnOK,
            this.btnCancel});
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.ShowItemToolTips = false;
            // 
            // btnTreeView
            // 
            this.btnTreeView.Checked = true;
            this.btnTreeView.CheckOnClick = true;
            this.btnTreeView.CheckState = System.Windows.Forms.CheckState.Checked;
            this.btnTreeView.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(this.btnTreeView, "btnTreeView");
            this.btnTreeView.Name = "btnTreeView";
            this.btnTreeView.Click += new System.EventHandler(this.btnTreeView_Click);
            // 
            // btnViewMode
            // 
            this.btnViewMode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnViewMode.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNormalViewMode,
            this.btnPageViewMode,
            this.btnWidelyViewMode,
            this.toolStripSeparator3,
            this.mShowLocalPropertyName});
            resources.ApplyResources(this.btnViewMode, "btnViewMode");
            this.btnViewMode.Name = "btnViewMode";
            this.btnViewMode.DropDownOpening += new System.EventHandler(this.btnViewMode_DropDownOpening);
            // 
            // btnNormalViewMode
            // 
            this.btnNormalViewMode.Name = "btnNormalViewMode";
            resources.ApplyResources(this.btnNormalViewMode, "btnNormalViewMode");
            this.btnNormalViewMode.Click += new System.EventHandler(this.btnNormalViewMode_Click);
            // 
            // btnPageViewMode
            // 
            this.btnPageViewMode.Checked = true;
            this.btnPageViewMode.CheckState = System.Windows.Forms.CheckState.Checked;
            this.btnPageViewMode.Name = "btnPageViewMode";
            resources.ApplyResources(this.btnPageViewMode, "btnPageViewMode");
            this.btnPageViewMode.Click += new System.EventHandler(this.btnPageViewMode_Click);
            // 
            // btnWidelyViewMode
            // 
            this.btnWidelyViewMode.Name = "btnWidelyViewMode";
            resources.ApplyResources(this.btnWidelyViewMode, "btnWidelyViewMode");
            this.btnWidelyViewMode.Click += new System.EventHandler(this.btnWidelyViewMode_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
            // 
            // mShowLocalPropertyName
            // 
            this.mShowLocalPropertyName.Name = "mShowLocalPropertyName";
            resources.ApplyResources(this.mShowLocalPropertyName, "mShowLocalPropertyName");
            this.mShowLocalPropertyName.Click += new System.EventHandler(this.mShowLocalPropertyName_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // btnPageSettings
            // 
            resources.ApplyResources(this.btnPageSettings, "btnPageSettings");
            this.btnPageSettings.Name = "btnPageSettings";
            this.btnPageSettings.Click += new System.EventHandler(this.btnPageSettings_Click);
            // 
            // btnInsert
            // 
            this.btnInsert.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mInsertHeaderLabel,
            this.mInsertHeaderLine,
            this.mInsertYAxis,
            this.mInsertFooterLine,
            this.menuInsertLabel,
            this.mNewTimeZone,
            this.toolStripSeparator4,
            this.mCloneInsert});
            resources.ApplyResources(this.btnInsert, "btnInsert");
            this.btnInsert.Name = "btnInsert";
            // 
            // mInsertHeaderLabel
            // 
            this.mInsertHeaderLabel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(this.mInsertHeaderLabel, "mInsertHeaderLabel");
            this.mInsertHeaderLabel.Name = "mInsertHeaderLabel";
            this.mInsertHeaderLabel.Click += new System.EventHandler(this.mInsertHeaderLabel_Click);
            // 
            // mInsertHeaderLine
            // 
            this.mInsertHeaderLine.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(this.mInsertHeaderLine, "mInsertHeaderLine");
            this.mInsertHeaderLine.Name = "mInsertHeaderLine";
            this.mInsertHeaderLine.Click += new System.EventHandler(this.mInsertHeaderLine_Click);
            // 
            // mInsertYAxis
            // 
            this.mInsertYAxis.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(this.mInsertYAxis, "mInsertYAxis");
            this.mInsertYAxis.Name = "mInsertYAxis";
            this.mInsertYAxis.Click += new System.EventHandler(this.mInsertYAxis_Click);
            // 
            // mInsertFooterLine
            // 
            this.mInsertFooterLine.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(this.mInsertFooterLine, "mInsertFooterLine");
            this.mInsertFooterLine.Name = "mInsertFooterLine";
            this.mInsertFooterLine.Click += new System.EventHandler(this.mInsertFooterLine_Click);
            // 
            // menuInsertLabel
            // 
            this.menuInsertLabel.Name = "menuInsertLabel";
            resources.ApplyResources(this.menuInsertLabel, "menuInsertLabel");
            this.menuInsertLabel.Click += new System.EventHandler(this.menuInsertLabel_Click);
            // 
            // mNewTimeZone
            // 
            this.mNewTimeZone.Name = "mNewTimeZone";
            resources.ApplyResources(this.mNewTimeZone, "mNewTimeZone");
            this.mNewTimeZone.Click += new System.EventHandler(this.mNewTimeZone_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            resources.ApplyResources(this.toolStripSeparator4, "toolStripSeparator4");
            // 
            // mCloneInsert
            // 
            this.mCloneInsert.Name = "mCloneInsert";
            resources.ApplyResources(this.mCloneInsert, "mCloneInsert");
            this.mCloneInsert.Click += new System.EventHandler(this.mCloneInsert_Click);
            // 
            // btnMovePre
            // 
            resources.ApplyResources(this.btnMovePre, "btnMovePre");
            this.btnMovePre.Name = "btnMovePre";
            this.btnMovePre.Click += new System.EventHandler(this.btnMovePre_Click);
            // 
            // btnMoveNext
            // 
            resources.ApplyResources(this.btnMoveNext, "btnMoveNext");
            this.btnMoveNext.Name = "btnMoveNext";
            this.btnMoveNext.Click += new System.EventHandler(this.btnMoveNext_Click);
            // 
            // btnDelete
            // 
            resources.ApplyResources(this.btnDelete, "btnDelete");
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnViewXMLSource
            // 
            resources.ApplyResources(this.btnViewXMLSource, "btnViewXMLSource");
            this.btnViewXMLSource.Name = "btnViewXMLSource";
            this.btnViewXMLSource.Click += this.btnViewXMLSource_Click;
            // 
            // btnSave
            // 
            //this.btnSave.Image = global::DCSoft.DCFunctionNameStrings.Save;
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.Name = "btnSave";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // btnOK
            // 
            this.btnOK.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.Name = "btnOK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // ctlProerty
            // 
            resources.ApplyResources(this.ctlProerty, "ctlProerty");
            this.ctlProerty.Name = "ctlProerty";
            this.ctlProerty.PropertySort = System.Windows.Forms.PropertySort.Alphabetical;
            this.ctlProerty.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.ctlProerty_PropertyValueChanged);
            // 
            // splitContainer1
            // 
            resources.ApplyResources(this.splitContainer1, "splitContainer1");
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.ctlTimeLine);
            this.splitContainer1.Panel1.Controls.Add(this.spTreeView);
            this.splitContainer1.Panel1.Controls.Add(this.tvwDOM);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.ctlProerty);
            // 
            // spTreeView
            // 
            resources.ApplyResources(this.spTreeView, "spTreeView");
            this.spTreeView.Name = "spTreeView";
            this.spTreeView.TabStop = false;
            // 
            // tvwDOM
            // 
            resources.ApplyResources(this.tvwDOM, "tvwDOM");
            this.tvwDOM.FullRowSelect = true;
            this.tvwDOM.HideSelection = false;
            this.tvwDOM.Name = "tvwDOM";
            this.tvwDOM.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvwDOM_AfterSelect);
            // 
            // ctlTimeLine
            // 
            resources.ApplyResources(this.ctlTimeLine, "ctlTimeLine");
            this.ctlTimeLine.BackColor = System.Drawing.SystemColors.Window;
            this.ctlTimeLine.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ctlTimeLine.Name = "ctlTimeLine";
            // 
            // TimeLineDesignerControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "TimeLineDesignerControl";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            //((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private TemperatureViewControl ctlTimeLine;
        private System.Windows.Forms.PropertyGrid ctlProerty;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStripDropDownButton btnViewMode;
        private System.Windows.Forms.ToolStripMenuItem btnNormalViewMode;
        private System.Windows.Forms.ToolStripMenuItem btnPageViewMode;
        private System.Windows.Forms.ToolStripMenuItem btnWidelyViewMode;
        private System.Windows.Forms.ToolStripButton btnDelete;
        private System.Windows.Forms.ToolStripButton btnViewXMLSource;
        private System.Windows.Forms.ToolStripButton btnCancel;
        private System.Windows.Forms.ToolStripButton btnOK;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripDropDownButton btnInsert;
        private System.Windows.Forms.ToolStripMenuItem mInsertHeaderLabel;
        private System.Windows.Forms.ToolStripMenuItem mInsertHeaderLine;
        private System.Windows.Forms.ToolStripMenuItem mInsertYAxis;
        private System.Windows.Forms.ToolStripMenuItem mInsertFooterLine;
        private System.Windows.Forms.ToolStripButton btnMovePre;
        private System.Windows.Forms.ToolStripButton btnMoveNext;
        private System.Windows.Forms.ToolStripButton btnPageSettings;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem mShowLocalPropertyName;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem mCloneInsert;
        private System.Windows.Forms.ToolStripButton btnTreeView;
        private System.Windows.Forms.Splitter spTreeView;
        private System.Windows.Forms.TreeView tvwDOM;
        private System.Windows.Forms.ToolStripMenuItem mNewTimeZone;
        private System.Windows.Forms.ToolStripMenuItem menuInsertLabel;
        private System.Windows.Forms.ToolStripButton btnSave;
    }
}