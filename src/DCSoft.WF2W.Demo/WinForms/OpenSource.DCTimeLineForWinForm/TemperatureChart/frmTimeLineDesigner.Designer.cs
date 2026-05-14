namespace DCSoft.TemperatureChart
{
    partial class frmTimeLineDesigner
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTimeLineDesigner));
            this.myDesignerControl = new DCSoft.TemperatureChart.TimeLineDesignerControl();
            this.SuspendLayout();
            // 
            // myDesignerControl
            // 
            resources.ApplyResources(this.myDesignerControl, "myDesignerControl");
            //this.$1.Name = "myDesignerControl";
            // 
            // frmTimeLineDesigner
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.myDesignerControl);
            this.MinimizeBox = false;
//            //this.$1.Name = "frmTimeLineDesigner";
            this.ShowInTaskbar = false;
            this.ResumeLayout(false);

        }

        #endregion

        private TimeLineDesignerControl myDesignerControl;
         
    }
}