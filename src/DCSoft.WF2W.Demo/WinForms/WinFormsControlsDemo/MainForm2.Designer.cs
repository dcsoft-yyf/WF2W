using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsControlsDemo
{
    partial class MainForm2
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
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Text = "MainForm2";

            this.toolStrip = new ToolStrip();
            this.cmbControls = new ToolStripComboBox();
            this.pnlDemo = new Panel();

            this.SuspendLayout();

            // toolStrip
            this.toolStrip.Items.Add(new ToolStripLabel("分类:"));
            // 分类按钮将在 BuildCategoryMap 后动态添加，此处先留出位置，稍后插入
            this.toolStrip.Items.Add(new ToolStripSeparator());
            this.toolStrip.Items.Add(new ToolStripLabel("控件:"));
            this.toolStrip.Items.Add(this.cmbControls);
            this.cmbControls.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbControls.Width = 200;
            this.cmbControls.SelectedIndexChanged += CmbControls_SelectedIndexChanged;

            this.toolStrip.Dock = DockStyle.Top;
            this.toolStrip.GripStyle = ToolStripGripStyle.Hidden;
            this.toolStrip.TabIndex = 0;

            // pnlDemo
            this.pnlDemo.Dock = DockStyle.Fill;
            this.pnlDemo.AutoScroll = true;
            this.pnlDemo.Name = "pnlDemo";
            this.pnlDemo.Size = new Size(1024, 720);
            this.pnlDemo.TabIndex = 1;

            // MainForm
            this.Controls.Add(this.pnlDemo);
            this.Controls.Add(this.toolStrip);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private ToolStrip toolStrip;
        private ToolStripComboBox cmbControls;
        private Dictionary<string, List<string>> categoryControlsMap; // 分类->控件列表

        // 下方演示面板
        private Panel pnlDemo;

    }
}