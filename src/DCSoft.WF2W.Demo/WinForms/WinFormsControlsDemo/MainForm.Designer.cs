using System.Drawing;
using System.Windows.Forms;

namespace WinFormsControlsDemo
{
    partial class MainForm
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
            this.Text = "MainForm";

            this.splitContainer = new SplitContainer();
            this.tvControls = new TreeView();
            this.pnlDemo = new Panel();

            this.SuspendLayout();

            // splitContainer
            this.splitContainer.Dock = DockStyle.Fill;
            this.splitContainer.Location = new Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Size = new Size(1024, 768);
            this.splitContainer.SplitterDistance = 250;
            this.splitContainer.TabIndex = 0;

            // tvControls (左侧树)
            this.tvControls.Dock = DockStyle.Fill;
            this.tvControls.Location = new Point(0, 0);
            this.tvControls.Name = "tvControls";
            this.tvControls.Size = new Size(250, 768);
            this.tvControls.TabIndex = 0;
            this.tvControls.NodeMouseClick += TvControls_NodeMouseClick;

            // pnlDemo (右侧演示区)
            this.pnlDemo.Dock = DockStyle.Fill;
            this.pnlDemo.AutoScroll = true;
            this.pnlDemo.Name = "pnlDemo";
            this.pnlDemo.Size = new Size(774, 768);
            this.pnlDemo.TabIndex = 1;

            // 将左右侧加入SplitContainer
            this.splitContainer.Panel1.Controls.Add(this.tvControls);
            this.splitContainer.Panel2.Controls.Add(this.pnlDemo);

            // MainForm
            this.Controls.Add(this.splitContainer);
            this.ResumeLayout(false);
        }

        #endregion
        private TreeView tvControls;
        private Panel pnlDemo;
        private SplitContainer splitContainer;
    }
}