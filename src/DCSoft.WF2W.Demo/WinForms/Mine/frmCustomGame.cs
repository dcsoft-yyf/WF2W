using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Minesweeper
{
	/// <summary>
	/// frmCustomGame 的摘要说明。
	/// </summary>
	public class frmCustomGame : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label lblHeight;
		private System.Windows.Forms.Label lblWidth;
		private System.Windows.Forms.Label lblMineCount;
		internal System.Windows.Forms.TextBox tbHeight;
		internal System.Windows.Forms.TextBox tbWidth;
		internal System.Windows.Forms.TextBox tbMineCount;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmCustomGame()
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();
		}

		private static int strToInt(string text)
		{
			if (Regex.IsMatch(text, @"^-?\d+$"))
				return Int32.Parse(text);
			else
				return 0;
		}
		
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
        }
        public class ShowSelfResult
		{
			public bool Result;
			public int Width;
			public int Height;
			public int MineCount;
		}
#if WF2W
		public static async ValueTask<ShowSelfResult> ShowSelf(IWin32Window parent, Point location, int width, int height, int mineCount)
#else
        public static ShowSelfResult ShowSelf(IWin32Window parent, Point location, int width, int height, int mineCount)
#endif
        {
            bool result;
			frmCustomGame cg = new frmCustomGame();
			cg.tbHeight.Text = height.ToString();
			cg.tbWidth.Text = width.ToString();
			cg.tbMineCount.Text = mineCount.ToString();
			cg.Location = location;
#if WF2W
			if (await cg.ShowDialog(parent) == DialogResult.OK)
#else
            if (cg.ShowDialog(parent) == DialogResult.OK)
#endif
			{
				width = strToInt(cg.tbWidth.Text);
				height = strToInt(cg.tbHeight.Text);
				mineCount = strToInt(cg.tbMineCount.Text);
				result = true;
			}
			else
			{
				result = false;
			}
			cg.Dispose();
			cg = null;
			var myResult = new ShowSelfResult();
			myResult.Result = result;
			myResult.Width = width;
			myResult.Height = height;
			myResult.MineCount = mineCount;
            return myResult;
		}
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
        }
        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
        }
        protected override void OnHandleDestroyed(EventArgs e)
        {
            base.OnHandleDestroyed(e);
        }
        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows 窗体设计器生成的代码
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.lblHeight = new System.Windows.Forms.Label();
			this.lblWidth = new System.Windows.Forms.Label();
			this.lblMineCount = new System.Windows.Forms.Label();
			this.tbHeight = new System.Windows.Forms.TextBox();
			this.tbWidth = new System.Windows.Forms.TextBox();
			this.tbMineCount = new System.Windows.Forms.TextBox();
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// lblHeight
			// 
			this.lblHeight.AutoSize = true;
			this.lblHeight.Location = new System.Drawing.Point(12, 24);
			this.lblHeight.Name = "lblHeight";
			this.lblHeight.Size = new System.Drawing.Size(54, 17);
			this.lblHeight.TabIndex = 0;
			this.lblHeight.Text = "高度(&H):";
			// 
			// lblWidth
			// 
			this.lblWidth.AutoSize = true;
			this.lblWidth.Location = new System.Drawing.Point(12, 52);
			this.lblWidth.Name = "lblWidth";
			this.lblWidth.Size = new System.Drawing.Size(54, 17);
			this.lblWidth.TabIndex = 1;
			this.lblWidth.Text = "宽度(&W):";
			// 
			// lblMineCount
			// 
			this.lblMineCount.AutoSize = true;
			this.lblMineCount.Location = new System.Drawing.Point(12, 80);
			this.lblMineCount.Name = "lblMineCount";
			this.lblMineCount.Size = new System.Drawing.Size(54, 17);
			this.lblMineCount.TabIndex = 2;
			this.lblMineCount.Text = "雷数(&M):";
			// 
			// tbHeight
			// 
			this.tbHeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tbHeight.Location = new System.Drawing.Point(67, 20);
			this.tbHeight.MaxLength = 5;
			this.tbHeight.Name = "tbHeight";
			this.tbHeight.Size = new System.Drawing.Size(40, 21);
			this.tbHeight.TabIndex = 3;
			this.tbHeight.Text = "";
			// 
			// tbWidth
			// 
			this.tbWidth.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tbWidth.Location = new System.Drawing.Point(67, 48);
			this.tbWidth.MaxLength = 5;
			this.tbWidth.Name = "tbWidth";
			this.tbWidth.Size = new System.Drawing.Size(40, 21);
			this.tbWidth.TabIndex = 4;
			this.tbWidth.Text = "";
			// 
			// tbMineCount
			// 
			this.tbMineCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tbMineCount.Location = new System.Drawing.Point(67, 76);
			this.tbMineCount.MaxLength = 5;
			this.tbMineCount.Name = "tbMineCount";
			this.tbMineCount.Size = new System.Drawing.Size(40, 21);
			this.tbMineCount.TabIndex = 5;
			this.tbMineCount.Text = "";
			// 
			// btnOK
			// 
			this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnOK.Location = new System.Drawing.Point(124, 32);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(48, 23);
			this.btnOK.TabIndex = 6;
			this.btnOK.Text = "确定";
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnCancel.Location = new System.Drawing.Point(124, 66);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(48, 23);
			this.btnCancel.TabIndex = 7;
			this.btnCancel.Text = "取消";
			// 
			// frmCustomGame
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(192, 121);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.tbMineCount);
			this.Controls.Add(this.tbWidth);
			this.Controls.Add(this.tbHeight);
			this.Controls.Add(this.lblMineCount);
			this.Controls.Add(this.lblWidth);
			this.Controls.Add(this.lblHeight);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.HelpButton = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmCustomGame";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "自定义雷区";
			this.ResumeLayout(false);

		}
		#endregion

		private void btnOK_Click(object sender, System.EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
			this.Close();
		}
	}
}
