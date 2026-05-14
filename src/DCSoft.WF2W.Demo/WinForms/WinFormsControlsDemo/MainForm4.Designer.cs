using System.Drawing;
using System.Windows.Forms;

namespace WinFormsControlsDemo
{
    partial class MainForm4
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
            this.pnlMain = new Panel();
            this.SuspendLayout();

            // pnlMain
            this.pnlMain.Dock = DockStyle.Fill;
            this.pnlMain.AutoScroll = true;
            this.pnlMain.Padding = new Padding(10);
            this.pnlMain.Name = "pnlMain";

            // MainForm
            this.AutoScaleDimensions = new SizeF(6F, 12F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(1024, 768);
            this.Controls.Add(this.pnlMain);
            this.ResumeLayout(false);
        }

        #endregion

        private Panel pnlMain;
    }
}