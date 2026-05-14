using System;
using System.Drawing;
using System.ComponentModel;
using System.Collections;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;

namespace Minesweeper
{
	/// <summary>
	/// MineControl
	/// </summary>
	public class MineControl : Control
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public MineControl()
		{

            InitializeComponent();
			SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.ResizeRedraw | ControlStyles.DoubleBuffer |
				ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
			SetStyle(ControlStyles.Selectable, false);
			BackColor = Color.Silver;
			Width = cellSize * 9;
			Height = cellSize * 9;
		}

		/// <summary> 
		/// 清理所有正在使用的资源。
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				grayBrush.Dispose();
				darkGrayBrush.Dispose();
				darkGrayPen.Dispose();
				lightPen.Dispose();
				font.Dispose();
				brush1.Dispose();
				brush2.Dispose();
				brush3.Dispose();
				brush4.Dispose();
				brush5.Dispose();
				brush6.Dispose();
				brush7.Dispose();
				brush8.Dispose();
				redBrush.Dispose();
				doubtBrush.Dispose();
				imgMarked.Dispose();
				imgNotDiscovery.Dispose();
				imgMarkedWrong.Dispose();
			}
			base.Dispose( disposing );
		}

		private const int cellSize = 16;
		private Color gray = Color.Silver;
		private Color darkGray = Color.Gray;
		private Brush grayBrush, darkGrayBrush, brush1, brush2, brush3, brush4, brush5, brush6, brush7, brush8, doubtBrush, redBrush;
		private Pen lightPen,  darkGrayPen;
		private Font font;
		private Bitmap imgMarked, imgNotDiscovery, imgMarkedWrong;

		#region 组件设计器生成的代码
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.Name = "mineControl";
			grayBrush = new SolidBrush(gray);
			darkGrayBrush = new SolidBrush(darkGray);
			redBrush = new SolidBrush(Color.Red);
			darkGrayPen = new Pen(darkGray, 1);
			lightPen = new Pen(Color.White, 1);
			font = new Font("Arial Black", 9, FontStyle.Regular);
			brush1 = new SolidBrush(Color.Blue);
			brush2 = new SolidBrush(Color.Green);
			brush3 = new SolidBrush(Color.Red);
			brush4 = new SolidBrush(Color.Navy);
			brush5 = new SolidBrush(Color.Maroon);
			brush6 = new SolidBrush(Color.Teal);
			brush7 = new SolidBrush(Color.Black);
			brush8 = new SolidBrush(Color.Gray);
			doubtBrush = new SolidBrush(Color.Black);
			imgMarked =  getBitmap("Marked.png");
			imgNotDiscovery = getBitmap("NotDiscovery.png");
			imgMarkedWrong = getBitmap("MarkedWrong.png");
		}
		#endregion

		private Bitmap getBitmap(string fileName)
		{
			Image img = 	Image.FromStream(GetResource(fileName));
			Bitmap bmp = new Bitmap(img);
			img.Dispose();
			img = null;
			//Console.WriteLine(fileName + " " + Convert.ToBase64String(bmp.Data));
			bmp.MakeTransparent(bmp.GetPixel(1, 1));
            //Console.WriteLine(fileName + "222 " + Convert.ToBase64String(bmp.Data));
            return bmp;
		}

		private Mines mines;

		/// <summary>
		/// Mines
		/// </summary>
		public Mines Mines
		{
			get
			{
				return mines;
			}
			set
			{
				mines = value;
				if (mines != null)
					mines.OnMineStatusChange += new MineStatusChangeEventHandler(mineStatusChange);
			}
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			if (mines == null)
			{
				base.OnPaint(e);
				return;
			}

			if (mines.GameState == GameState.Processing || mines.GameState == GameState.NotStarted)
				Enabled = true;

			for (int i = 0; i < mines.Width; i++)
			{
				for (int j = 0; j < mines.Height; j++)
				{
					MineCell_Paint(this, new MineControlPaintEventArgs(e.Graphics, calcRect(e.ClipRectangle, i, j), i, j));
				}
			}
		}

		private Rectangle calcRect(Rectangle rect, int x, int y)
		{
			Rectangle result = new Rectangle(ClientRectangle.Left + x * cellSize, ClientRectangle.Top + y * cellSize, cellSize, cellSize);
			if (rect == ClientRectangle || Rectangle.Intersect(result, rect) != Rectangle.Empty)
				return result;
			else
				return Rectangle.Empty;
		}

		private MouseButtons mouseButton = MouseButtons.None;
		private Rectangle activeRect = Rectangle.Empty;
		private Rectangle prevRect = Rectangle.Empty;

		private static bool compareMouseButton(MouseButtons mb1, MouseButtons mb2)
		{
			return ((mb1 & mb2) == mb2);
		}

		private static Rectangle getSmallerRect(Rectangle rect)
		{
			return new Rectangle(rect.X + 2, rect.Y + 2, rect.Width - 4, rect.Height - 4);
		}

		private void MineCell_Paint(object sender, MineControlPaintEventArgs e)
		{
			Rectangle rect = new Rectangle(e.ClipRectangle.Location, new Size(e.ClipRectangle.Size.Width - 1, e.ClipRectangle.Size.Height - 1));
			Graphics g = e.Graphics;

			if (rect == Rectangle.Empty || !g.IsVisible(rect))
				return;

			Mine mine = mines.mines[e.Y * mines.Width + e.X];
			switch(mine.MineStatus)
			{
				case MineStatus.HasMine:
				case MineStatus.NoMine:
					if (mine.MineStatus == MineStatus.HasMine && (mines.GameState != GameState.Processing && mines.GameState != GameState.NotStarted))
					{
						drawFrame(g, rect);
						g.DrawImage(imgNotDiscovery, rect.Left + 1,  rect.Top + 1);
					}
					else
					{
						int offset = 0;
						if ((compareMouseButton(mouseButton, MouseButtons.Left) &&	activeRect == e.ClipRectangle) ||
							(compareMouseButton(mouseButton, mbLeftnRight) &&	Rectangle.Intersect(activeRect, getSmallerRect(e.ClipRectangle)) != Rectangle.Empty))
						{
							drawFrame(g, rect);
							offset = 1;
						}
						else
							drawButton(g, rect);
						if (mine.Doubt)
							g.DrawString("?", font, doubtBrush, rect.Left + 2 + offset, rect.Top + offset);
					}
					break;
				case MineStatus.MarkedRight:
					drawButton(g, rect);	
					g.DrawImage(imgMarked, rect.Left + 2,  rect.Top + 2);
					break;
				case MineStatus.MarkedWrong:
					if (mines.GameState == GameState.Processing || mines.GameState == GameState.NotStarted)
					{
						drawButton(g, rect);	
						g.DrawImage(imgMarked, rect.Left + 2,  rect.Top + 2);
					}
					else
					{
						drawFrame(g, rect);
						g.DrawImage(imgMarkedWrong, rect.Left + 1,  rect.Top + 1);
					}
					break;
				case MineStatus.Exploded:
					g.FillRectangle(redBrush, e.ClipRectangle);
					drawFrame(g, rect);
					g.DrawImage(imgNotDiscovery, rect.Left + 1,  rect.Top + 1);
					break;
				case MineStatus.Clear:
					drawFrame(g, rect);
					int count = mine.MineCount;
					if  (count > 0)
						g.DrawString(count.ToString(), font, getBrush(count), rect.Left + 2, rect.Top);
					break;
			}
		}

		private void drawFrame(Graphics g, Rectangle rect)
		{
			g.DrawLine(darkGrayPen, rect.Left, rect.Top, rect.Left, rect.Bottom);
			g.DrawLine(darkGrayPen, rect.Left, rect.Top, rect.Right, rect.Top);
		}

		private void drawButton(Graphics g, Rectangle rect)
		{
			#region Top Border
			g.DrawLine(lightPen, rect.Left, rect.Top, rect.Right - 1, rect.Top);
			g.DrawLine(lightPen, rect.Left, rect.Top + 1, rect.Right - 2, rect.Top + 1);
			#endregion

			#region Bottom Border
			g.DrawLine(darkGrayPen, rect.Left + 1, rect.Bottom, rect.Right, rect.Bottom);
			g.DrawLine(darkGrayPen, rect.Left + 2, rect.Bottom - 1, rect.Right, rect.Bottom - 1);
			#endregion

			#region Left Border
			g.DrawLine(lightPen, rect.Left, rect.Top, rect.Left, rect.Bottom - 1);
			g.DrawLine(lightPen, rect.Left + 1, rect.Top, rect.Left + 1, rect.Bottom - 2);
			#endregion

			#region Right Border
			g.DrawLine(darkGrayPen, rect.Right, rect.Top + 1, rect.Right, rect.Bottom);
			g.DrawLine(darkGrayPen, rect.Right - 1, rect.Top + 2, rect.Right - 1, rect.Bottom);
			#endregion
		}

		private Brush getBrush(int i)
		{
			switch (i)
			{
				case 1:
					return brush1;
				case 2:
					return brush2;
				case 3:
					return brush3;
				case 4:
					return brush4;
				case 5:
					return brush5;
				case 6:
					return brush6;
				case 7:
					return brush7;
				case 8:
					return brush8;
				default:
					return null;
			}
		}

		public override string Text
		{
			get
			{
				return "";
			}
			set
			{
			}
		}

		const MouseButtons mbLeftnRight = MouseButtons.Left | MouseButtons.Right;

		private Rectangle expandRect(Rectangle rect)
		{
			if (rect.Width == cellSize)
			{
				rect = new Rectangle(rect.X - cellSize, rect.Y - cellSize, rect.Width + cellSize * 2, rect.Height + cellSize * 2);
				return Rectangle.Intersect(rect, ClientRectangle);
			}
			else
				return rect;
		}

		private bool twoKeyDoubleClick;

		private void setCellState(MouseButtons mb, Rectangle rect)
		{
			if (mb != MouseButtons.None && compareMouseButton(mouseButton | mb, mbLeftnRight))
			{
				twoKeyDoubleClick = true;
				mouseButton = mbLeftnRight;
				rect = expandRect(rect);
			}
			else
				mouseButton = mb;
			if (compareMouseButton(mouseButton, MouseButtons.Left) || compareMouseButton(mouseButton, mbLeftnRight))
			{
				if (rect !=Rectangle.Empty && rect != activeRect)
				{
					prevRect = activeRect;
					activeRect = rect;
					Invalidate(prevRect);
				}
				Invalidate(activeRect);
			}
			else
			{
				if (prevRect != Rectangle.Empty)
					Invalidate(prevRect);
				if (activeRect != Rectangle.Empty)
					Invalidate(activeRect);
				prevRect = Rectangle.Empty;
				activeRect = Rectangle.Empty;
			}
		}

		private void setCellState(MouseButtons mb)
		{
			setCellState(mb, Rectangle.Empty);
		}

		protected override void OnMouseEnter(EventArgs e)
		{
			base.OnMouseEnter (e);

			setCellState(MouseButtons.None);
		}

		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave (e);

			setCellState(MouseButtons.None);
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);

			setCellState(mouseButton, getRect(e.X, e.Y));
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown (e);

			Mine m = getMine(e.X, e.Y);
			if (m == null)
				return;

			twoKeyDoubleClick = false;
			setCellState(e.Button, getRect(e.X, e.Y));
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp (e);

			Mine m = getMine(e.X, e.Y);
			if (m == null || !Enabled)
				return;

			if (mouseButton != MouseButtons.None && (mines.GameState == GameState.Processing || mines.GameState == GameState.NotStarted))
			{
				if (compareMouseButton(mouseButton, mbLeftnRight))
				{
					m.QuickDig();
					AfterDigOrMark(this, EventArgs.Empty);
				}
				else if (compareMouseButton(e.Button, MouseButtons.Left) && !twoKeyDoubleClick)
				{
					m.Dig(true);
					AfterDigOrMark(this, EventArgs.Empty);
				}
				else if (compareMouseButton(e.Button, MouseButtons.Right) && !twoKeyDoubleClick)
				{
					m.Mark();
					AfterDigOrMark(this, EventArgs.Empty);
				}

				if (mines.GameState != GameState.Processing && mines.GameState != GameState.NotStarted)
				{
					Enabled = false;
					Refresh();
				}
			}

			if (compareMouseButton(mouseButton, mbLeftnRight))
				mouseButton = e.Button == MouseButtons.Left ? MouseButtons.Right : MouseButtons.Left;
			else
				mouseButton = MouseButtons.None;
			setCellState(mouseButton, getRect(e.X, e.Y));
		}

		private Rectangle getRect(int x, int y)
		{
			Rectangle rect = calcRect(ClientRectangle, x / cellSize, y / cellSize);
			return rect;
		}

		private Mine getMine(int x, int y)
		{
			if (x > cellSize * mines.Width || y > cellSize * mines.Height || x < 0 || y < 0)
				return null;
			int i = y / cellSize * mines.Width + x / cellSize;
			if (i >= 0 && i < mines.mines.Length)
				return mines.mines[i];
			else
				return null;
		}

		private void mineStatusChange(object sender, MineStatusChangeEventArgs e)
		{
			if (e.Mine == null)
				Refresh();
			else
			{
				Rectangle rect = calcRect(ClientRectangle, e.Mine.Index % mines.Width, e.Mine.Index / mines.Width);
				Invalidate(rect);
				Update();
			}
		}

		public void AdjustSize()
		{
			if (Dock != DockStyle.Fill && mines != null)
			{
				Width = cellSize * mines.Width;
				Height = cellSize * mines.Height;
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
			if( stream == null )
			{
				var asm = this.GetType().Assembly;
				foreach( var name in asm.GetManifestResourceNames() )
				{
					if(name.EndsWith("." + fileName))
					{
						stream = asm.GetManifestResourceStream(name);
						break;
					}
				}
			}
			if (stream == null)
				throw new MineException("无法取得资源+++: " + fileName);
			return stream;
		}

		public event EventHandler DigOrMark;

		private void AfterDigOrMark(object sender, EventArgs e)
		{
			if (DigOrMark != null)
				DigOrMark(this, e);
		}

	}

	public class MineControlPaintEventArgs: EventArgs
	{
		private int m_X, m_Y;
		private Rectangle clipRectangle;
		private Graphics g;

		public Graphics Graphics
		{
			get
			{
				return g;
			}
		}

		public Rectangle ClipRectangle
		{
			get
			{
				return clipRectangle;
			}
		}

		public int X
		{
			get
			{
				return m_X;
			}
		}

		public int Y
		{
			get
			{
				return m_Y;
			}
		}

		public MineControlPaintEventArgs(Graphics graphics, Rectangle clipRect, int x, int y)
		{
			m_X = x;
			m_Y = y;
			clipRectangle = clipRect;
			g = graphics;
		}
	}

}
