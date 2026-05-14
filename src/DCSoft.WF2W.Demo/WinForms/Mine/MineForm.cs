// source code from https://gitee.com/dingxiaowei/MineGame

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Runtime.InteropServices;
using System.Resources;


// removed alias to avoid conflicts; use System.Resources.ResourceManager explicitly

namespace Minesweeper
{
	/// <summary>
	/// frmMinesweeper 的摘要说明。
	/// </summary>
	public class frmMinesweeper : System.Windows.Forms.Form, IMessageFilter
	{
		
		private System.Windows.Forms.MenuItem miGame;
		private System.Windows.Forms.MenuItem miHelp;
		private System.Windows.Forms.MainMenu mmMainMenu;
		private System.Windows.Forms.MenuItem miIndex;
		private System.Windows.Forms.MenuItem miSearchTopic;
		private System.Windows.Forms.MenuItem miUsingHelp;
		private System.Windows.Forms.MenuItem miAbout;
		private System.Windows.Forms.MenuItem miS1;
		private System.Windows.Forms.MenuItem miS2;
		private System.Windows.Forms.MenuItem miS3;
		private System.Windows.Forms.MenuItem miS4;
		private System.Windows.Forms.MenuItem miS5;
		private System.Windows.Forms.MenuItem miGameStart;
		private System.Windows.Forms.MenuItem miExpert;
		private System.Windows.Forms.MenuItem miCustomGame;
		private System.Windows.Forms.MenuItem miMark;
		private System.Windows.Forms.MenuItem miColor;
		private System.Windows.Forms.MenuItem miSound;
		private System.Windows.Forms.MenuItem miHiscore;
		private System.Windows.Forms.MenuItem miExit;
		private Minesweeper.MinePanel mpMine;
		private Minesweeper.MineControl mcMine;
		private System.Windows.Forms.MenuItem miNovice;
		private System.Windows.Forms.MenuItem miMaster;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmMinesweeper()
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();

			mcMine.Mines = mines;
		}

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
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
			
            ResourceManager resources = new ResourceManager(typeof(frmMinesweeper));
			this.mmMainMenu = new System.Windows.Forms.MainMenu();
			this.miGame = new System.Windows.Forms.MenuItem();
			this.miGameStart = new System.Windows.Forms.MenuItem();
			this.miS1 = new System.Windows.Forms.MenuItem();
			this.miNovice = new System.Windows.Forms.MenuItem();
			this.miMaster = new System.Windows.Forms.MenuItem();
			this.miExpert = new System.Windows.Forms.MenuItem();
			this.miCustomGame = new System.Windows.Forms.MenuItem();
			this.miS2 = new System.Windows.Forms.MenuItem();
			this.miMark = new System.Windows.Forms.MenuItem();
			this.miColor = new System.Windows.Forms.MenuItem();
			this.miSound = new System.Windows.Forms.MenuItem();
			this.miS3 = new System.Windows.Forms.MenuItem();
			this.miHiscore = new System.Windows.Forms.MenuItem();
			this.miS4 = new System.Windows.Forms.MenuItem();
			this.miExit = new System.Windows.Forms.MenuItem();
			this.miHelp = new System.Windows.Forms.MenuItem();
			this.miIndex = new System.Windows.Forms.MenuItem();
			this.miSearchTopic = new System.Windows.Forms.MenuItem();
			this.miUsingHelp = new System.Windows.Forms.MenuItem();
			this.miS5 = new System.Windows.Forms.MenuItem();
			this.miAbout = new System.Windows.Forms.MenuItem();
			this.mpMine = new Minesweeper.MinePanel();
			this.mcMine = new Minesweeper.MineControl();
			this.mpMine.SuspendLayout();
			this.SuspendLayout();
			// 
			// mmMainMenu
			// 
			this.mmMainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					   this.miGame,
																					   this.miHelp});
			// 
			// miGame
			// 
			this.miGame.Index = 0;
			this.miGame.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																				   this.miGameStart,
																				   this.miS1,
																				   this.miNovice,
																				   this.miMaster,
																				   this.miExpert,
																				   this.miCustomGame,
																				   this.miS2,
																				   this.miMark,
																				   this.miColor,
																				   this.miSound,
																				   this.miS3,
																				   this.miHiscore,
																				   this.miS4,
																				   this.miExit});
			this.miGame.Text = "&Game";
			// 
			// miGameStart
			// 
			this.miGameStart.Index = 0;
			this.miGameStart.Shortcut = System.Windows.Forms.Shortcut.F2;
			this.miGameStart.Text = "&New";
			this.miGameStart.Click += new System.EventHandler(this.miGameStart_Click);
			// 
			// miS1
			// 
			this.miS1.Index = 1;
			this.miS1.Text = "-";
			// 
			// miNovice
			// 
			this.miNovice.Checked = true;
			this.miNovice.Index = 2;
			this.miNovice.Text = "&Base";
			this.miNovice.Click += new System.EventHandler(this.miNovice_Click);
			// 
			// miMaster
			// 
			this.miMaster.Index = 3;
			this.miMaster.Text = "&Middle";
			this.miMaster.Click += new System.EventHandler(this.miMaster_Click);
			// 
			// miExpert
			// 
			this.miExpert.Index = 4;
			this.miExpert.Text = "&Expert";
			this.miExpert.Click += new System.EventHandler(this.miExpert_Click);
			// 
			// miCustomGame
			// 
			this.miCustomGame.Index = 5;
			this.miCustomGame.Text = "&Custom...";
			this.miCustomGame.Click += new System.EventHandler(this.miCustomGame_Click);
			// 
			// miS2
			// 
			this.miS2.Index = 6;
			this.miS2.Text = "-";
			// 
			// miMark
			// 
			this.miMark.Checked = true;
			this.miMark.Index = 7;
			this.miMark.Text = "&Mark(?)";
			this.miMark.Click += new System.EventHandler(this.miMark_Click);
			// 
			// miColor
			// 
			this.miColor.Checked = true;
			this.miColor.Enabled = false;
			this.miColor.Index = 8;
			this.miColor.Text = "Color";
			// 
			// miSound
			// 
			this.miSound.Enabled = false;
			this.miSound.Index = 9;
			this.miSound.Text = "&Sound";
			// 
			// miS3
			// 
			this.miS3.Index = 10;
			this.miS3.Text = "-";
			// 
			// miHiscore
			// 
			this.miHiscore.Enabled = false;
			this.miHiscore.Index = 11;
			this.miHiscore.Text = "扫雷英雄榜(&T)...";
			// 
			// miS4
			// 
			this.miS4.Index = 12;
			this.miS4.Text = "-";
			// 
			// miExit
			// 
			this.miExit.Index = 13;
			this.miExit.Text = "&Exit";
			this.miExit.Click += new System.EventHandler(this.miExit_Click);
			// 
			// miHelp
			// 
			this.miHelp.Index = 1;
			this.miHelp.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																				   this.miIndex,
																				   this.miSearchTopic,
																				   this.miUsingHelp,
																				   this.miS5,
																				   this.miAbout});
			this.miHelp.Text = "&Help";
			// 
			// miIndex
			// 
			this.miIndex.Enabled = false;
			this.miIndex.Index = 0;
			this.miIndex.Shortcut = System.Windows.Forms.Shortcut.F1;
			this.miIndex.Text = "Dictionery";
			// 
			// miSearchTopic
			// 
			this.miSearchTopic.Enabled = false;
			this.miSearchTopic.Index = 1;
			this.miSearchTopic.Text = "Search topic";
			// 
			// miUsingHelp
			// 
			this.miUsingHelp.Enabled = false;
			this.miUsingHelp.Index = 2;
			this.miUsingHelp.Text = "&Help";
			// 
			// miS5
			// 
			this.miS5.Index = 3;
			this.miS5.Text = "-";
			// 
			// miAbout
			// 
			this.miAbout.Index = 4;
			this.miAbout.Text = "&About";
			this.miAbout.Click += new System.EventHandler(this.miAbout_Click);
			// 
			// mpMine
			// 
			this.mpMine.BackColor = System.Drawing.Color.Silver;
			this.mpMine.Controls.Add(this.mcMine);
			this.mpMine.CountSecond = 0;
			this.mpMine.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mpMine.Location = new System.Drawing.Point(0, 0);
			this.mpMine.Name = "mpMine";
			this.mpMine.RemainMineCount = 0;
			this.mpMine.Size = new System.Drawing.Size(322, 256);
			this.mpMine.TabIndex = 1;
			// 
			// mcMine
			// 
			this.mcMine.BackColor = System.Drawing.Color.Silver;
			this.mcMine.Location = new System.Drawing.Point(24, 72);
			this.mcMine.Mines = null;
			this.mcMine.Name = "mcMine";
			this.mcMine.Size = new System.Drawing.Size(272, 168);
			this.mcMine.TabIndex = 4;
			// 
			// frmMinesweeper
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(322, 256);
			this.Controls.Add(this.mpMine);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.StartPosition = FormStartPosition.CenterScreen;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Menu = this.mmMainMenu;
			this.Name = "frmMinesweeper";
			this.Text = "扫雷";
			this.SizeChanged += new System.EventHandler(this.frmMinesweeper_SizeChanged);
			this.Load += new System.EventHandler(this.frmMinesweeper_Load);
			this.mpMine.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		///// <summary>
		///// 应用程序的主入口点。
		///// </summary>
		//[STAThread]
		//static void Main() 
		//{
		//	Application.Run(new frmMinesweeper());
		//}

		private Mines mines = new Mines();

		private void miExit_Click(object sender, System.EventArgs e)
		{
			Close();
		}

		private void frmMinesweeper_Load(object sender, System.EventArgs e)
		{
			Application.AddMessageFilter(this);
			mpMine.Reset += new EventHandler(reset);
			mcMine.DigOrMark += new EventHandler(AfterDigOrMark);
			reset(this, e);
		}

		private void miGameStart_Click(object sender, System.EventArgs e)
		{
			reset(this, e);
		}

		private void reset(object sender, EventArgs e)
		{
			prevGameState = GameState.NotStarted;
			mines.Clear();
			mcMine.Location = mpMine.MineControlLocation;
			mcMine.AdjustSize();
			ClientSize = mpMine.GetWindowClientSize(mcMine.Size);
			mcMine.Refresh();
			mpMine.StopTimer();
			mpMine.RemainMineCount = mines.MineRemainCount;
			mpMine.CountSecond = 0;
			mpMine.ChangeFace(1);
		}

		private void frmMinesweeper_SizeChanged(object sender, System.EventArgs e)
		{
			mpMine.ArrangeChildChildren();
		}

		private GameState prevGameState = GameState.NotStarted;

		private void AfterDigOrMark(object sender, EventArgs e)
		{
			if (mines.GameState != prevGameState)
			{
				if (mines.GameState == GameState.Processing)
					mpMine.StartTimer();

				prevGameState = mines.GameState;

				if (mines.GameState == GameState.Complete || mines.GameState == GameState.Fail)
				{
					mpMine.StopTimer();
					if (mines.GameState == GameState.Complete)
						mpMine.ChangeFace(4);
					else
						mpMine.ChangeFace(3);
				}
			}
			mpMine.RemainMineCount = mines.MineRemainCount;
		}

		#region IMessageFilter 成员
		private const int WM_LBUTTONDOWN = 0x0201;
		private const int WM_LBUTTONUP = 0x0202;

		public bool PreFilterMessage(ref Message m)
		{
			Control ctrl = Form.FromHandle(m.HWnd);
			switch (m.Msg)
			{
				case WM_LBUTTONDOWN:
					if (mcMine.Enabled && ctrl.Name != "rbReset" && ctrl.FindForm().GetType() != typeof(frmCustomGame))
						mpMine.ChangeFace(2);
					break;
				case WM_LBUTTONUP:
					if (mcMine.Enabled && ctrl.Name != "rbReset" && ctrl.FindForm().GetType() != typeof(frmCustomGame))
						mpMine.ChangeFace(1);
					break;
			}
			return false;
		}
		#endregion

		private void miMark_Click(object sender, System.EventArgs e)
		{
			MenuItem mi = sender as MenuItem;
			if (mi != null)
			{
				mi.Checked = !mi.Checked;
				mines.AllowMarkDoubt = mi.Checked;
			}
		}
		 
		/// <summary>
		/// 从资源DLL中取得需要的资源
		/// </summary>
		public Stream GetResource(string fileName)
		{
			if (fileName == null || fileName.Length == 0)
				return null;

			Stream stream = null;
			Type resourceType = this.GetType();
			string resourceName = resourceType.Namespace + "." + fileName.Replace("\\", ".");
			System.Reflection.Assembly assembly = System.Reflection.Assembly.GetAssembly(resourceType);
			if (assembly == null)
				throw new MineException("无法装载资源文件: " + resourceType.Namespace + ".dll");
			stream = System.Reflection.Assembly.GetAssembly(resourceType).GetManifestResourceStream(resourceName);
			if (stream == null)
				throw new MineException("无法取得资源: " + fileName);
			return stream;
		}

		private void miAbout_Click(object sender, System.EventArgs e)
		{
			MessageBox.Show("MineSweeper from https://gitee.com/dingxiaowei/MineGame, but use WF2W.");
		}

		private void miNovice_Click(object sender, System.EventArgs e)
		{
			miNovice.Checked = true;
			miMaster.Checked = false;
			miExpert.Checked = false;
			miCustomGame.Checked = false;
			mines.Clear(9, 9, 10);
			reset(this, EventArgs.Empty);
		}

		private void miMaster_Click(object sender, System.EventArgs e)
		{
			miNovice.Checked = false;
			miMaster.Checked = true;
			miExpert.Checked = false;
			miCustomGame.Checked = false;
			mines.Clear(16, 16, 40);
			reset(this, EventArgs.Empty);
		}

		private void miExpert_Click(object sender, System.EventArgs e)
		{
			miNovice.Checked = false;
			miMaster.Checked = false;
			miExpert.Checked = true;
			miCustomGame.Checked = false;
			mines.Clear(30, 16, 99);
			reset(this, EventArgs.Empty);
		}
        /// <summary>
        /// 显示自定义游戏对话框
        /// </summary>
        private
#if WF2W
            async
#endif
            void miCustomGame_Click(object sender, System.EventArgs e)
		{
			int height = mines.Height;
			int width = mines.Width;
			int mineCount = mines.Count;

#if WF2W
            var result = await frmCustomGame.ShowSelf(this, PointToScreen(mpMine.Location), width, height, mineCount);
#else
			var result = frmCustomGame.ShowSelf(this, PointToScreen(mpMine.Location), width, height, mineCount);
#endif
			if( result.Result )
            {
				width = result.Width;
				height = result.Height;
				mineCount = result.MineCount;
                miNovice.Checked = false;
				miMaster.Checked = false;
				miExpert.Checked = false;
				miCustomGame.Checked = true;
				mines.Clear(width, height, mineCount);
				reset(this, EventArgs.Empty);
			}
		}
	}
}
