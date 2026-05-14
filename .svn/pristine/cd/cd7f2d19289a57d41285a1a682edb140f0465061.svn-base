using System;
using System.Drawing;
using System.ComponentModel;
using System.Collections;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;
using DCSoft;

namespace Minesweeper
{

	#region ResetButton
	/// <summary>
	/// ResetButton
	/// </summary>
	public class ResetButton: Button
	{
		public ResetButton()
		{
			InitializeComponent();
			SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.Selectable, false);
			BackColor = gray;
			Width = 26;
			Height = 26;
		}

		/// <summary> 
		/// 清理所有正在使用的资源。
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				darkGrayPen.Dispose();
				lightPen.Dispose();
				grayPen.Dispose();
				grayBrush.Dispose();
			}
			base.Dispose( disposing );
		}

		#region 组件设计器生成的代码
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			Name = "resetButton";
			darkGrayPen = new Pen(darkGray, 1);
			lightPen = new Pen(Color.White, 1);
			grayPen = new Pen(gray, 1);
			grayBrush = new SolidBrush(gray);
		}
		#endregion

		protected override CreateParams CreateParams
		{
			get
			{
				//const int WS_EX_CLIENTEDGE = 0x200;
				CreateParams cp = base.CreateParams;
				//cp.ExStyle = cp.ExStyle | WS_EX_CLIENTEDGE;
				return cp;
			}
		}

		private Color gray = Color.Silver;
		private Color darkGray = Color.Gray;
		private Pen lightPen,  darkGrayPen, grayPen;
		private Brush grayBrush;

		protected override void OnPaint(PaintEventArgs e)
		{
			Rectangle rect = ClientRectangle;
			Graphics g = e.Graphics;

			g.FillRectangle(grayBrush, rect);
			drawFrame(g, new Rectangle(rect.Left, rect.Top, rect.Width - 1, rect.Height - 1));
			if (Image != null)
			{
				int offset;
				if (pressed)
					offset = 1;
				else
					offset = 0;
				g.DrawImage(Image, rect.Left + 4 + offset, rect.Top + 4 + offset);
			}
		}

		private void drawFrame(Graphics g, Rectangle rect)
		{
			if (pressed)
			{
				#region Top Border
				g.DrawLine(darkGrayPen, rect.Left, rect.Top, rect.Right - 1, rect.Top);
				g.DrawLine(darkGrayPen, rect.Left + 1, rect.Top + 1, rect.Right, rect.Top + 1);
				#endregion

				#region Bottom Border
				g.DrawLine(darkGrayPen, rect.Left + 1, rect.Bottom, rect.Right, rect.Bottom);
				#endregion

				#region Left Border
				g.DrawLine(darkGrayPen, rect.Left, rect.Top, rect.Left, rect.Bottom - 1);
				g.DrawLine(darkGrayPen, rect.Left + 1, rect.Top + 1, rect.Left + 1, rect.Bottom);
				#endregion

				#region Right Border
				g.DrawLine(darkGrayPen, rect.Right, rect.Top + 1, rect.Right, rect.Bottom);
				#endregion
			}
			else
			{
				#region Top Border
				g.DrawLine(darkGrayPen, rect.Left, rect.Top, rect.Right - 1, rect.Top);
				g.DrawLine(lightPen, rect.Left + 1, rect.Top + 1, rect.Right - 2, rect.Top + 1);
				#endregion

				#region Bottom Border
				g.DrawLine(darkGrayPen, rect.Left + 1, rect.Bottom, rect.Right, rect.Bottom);
				g.DrawLine(darkGrayPen, rect.Left + 2, rect.Bottom - 1, rect.Right, rect.Bottom - 1);
				g.DrawLine(darkGrayPen, rect.Left + 3, rect.Bottom - 2, rect.Right, rect.Bottom - 2);
				#endregion

				#region Left Border
				g.DrawLine(darkGrayPen, rect.Left, rect.Top, rect.Left, rect.Bottom - 1);
				g.DrawLine(lightPen, rect.Left + 1, rect.Top + 1, rect.Left + 1, rect.Bottom - 2);
				g.DrawLine(lightPen, rect.Left + 2, rect.Top + 1, rect.Left + 2, rect.Bottom - 3);
				#endregion

				#region Right Border
				g.DrawLine(darkGrayPen, rect.Right, rect.Top + 1, rect.Right, rect.Bottom);
				g.DrawLine(darkGrayPen, rect.Right - 1, rect.Top + 2, rect.Right - 1, rect.Bottom);
				g.DrawLine(darkGrayPen, rect.Right - 2, rect.Top + 3, rect.Right - 2, rect.Bottom);
				#endregion
			}
		}

		private bool pressed;

		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);

			if (e.Button == MouseButtons.Left)
			{
				pressed = true;
				Invalidate();
			}
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);

			if (e.Button == MouseButtons.Left)
			{
				pressed = false;
				Invalidate();
			}
		}
	}
	#endregion

	#region LEDPanel
	/// <summary>
	/// ResetButton
	/// </summary>
	public class LEDPanel: Panel
	{
		public LEDPanel()
		{
			InitializeComponent();
			SetStyle(ControlStyles.UserPaint, true);
			SetStyle(ControlStyles.Selectable, false);
			BorderStyle = BorderStyle.Fixed3D;
			ClientSize = new Size(charWidth * charCount, charHeight);
		}

		/// <summary> 
		/// 清理所有正在使用的资源。
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
			}
			base.Dispose( disposing );
		}

		#region 组件设计器生成的代码
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			Name = "LEDPanel";
		}
		#endregion

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			Rectangle rect = ClientRectangle;
			Graphics g = e.Graphics;

			string num = number.ToString().PadLeft(charCount, '0').Replace("0-", "-0");
			for (int i = 0; i < charCount; i++)
			{
				int j;
				if (num[i] == '-')
					j = 10;
				else
					j = Convert.ToInt32(num[i]) - 48;
				leds.Draw(g, rect.Left + charWidth * i, rect.Top, j);
			}
		}

		private const int charWidth = 13;
		private const int charHeight = 23;
		private int charCount = 3;

		public int CharCount
		{
			get
			{
				return charCount;
			}
			set
			{
				if (value != charCount)
				{
					charCount = value;
					ClientSize = new Size(charWidth * charCount, charHeight);
				}
			}
		}

		private ImageList leds;

		public ImageList LEDImages
		{
			get
			{
				return leds;
			}
			set
			{
				leds = value;
			}
		}

		private int number;

		public int Number
		{
			get
			{
				return number;
			}
			set
			{
				if (value >= 0)
				{
					int maxValue = Convert.ToInt32(String.Empty.PadRight(charCount, '9'));
					if (value > maxValue)
						value = maxValue;
				}
				else
				{
					int minValue = -Convert.ToInt32(String.Empty.PadRight(charCount - 1, '9'));
					if (value < minValue)
						value = minValue;
				}
				if (number != value)
				{
					number = value;
					Invalidate();
				}
			}
		}
	}
	#endregion

	/// <summary>
	/// MinePanel
	/// </summary>
	public class MinePanel : Panel
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public MinePanel()
		{
            //DCSoft.WF2WSystem.SetControlOwnerDraw(this);

            InitializeComponent();

			SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.ResizeRedraw | ControlStyles.DoubleBuffer |
				ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
			SetStyle(ControlStyles.Selectable, false);
			BackColor = Color.Silver;
			Width = 320;
			Height = 240;
			ChangeFace(1);
			ArrangeChildChildren();
		}

		/// <summary> 
		/// 清理所有正在使用的资源。
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				darkGrayPen.Dispose();
				lightPen.Dispose();
				rbReset.Dispose();
				pnlLeft.Dispose();
				pnlRight.Dispose();
				ilLED.Dispose();
				tmrCount.Dispose();
			}
			base.Dispose( disposing );
		}

		private Color darkGray = Color.Gray;

		private Pen lightPen,  darkGrayPen;

		private ResetButton rbReset;
		private LEDPanel pnlLeft, pnlRight;
		private ImageList ilLED;
		private System.Windows.Forms.Timer tmrCount;

		#region 组件设计器生成的代码
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			Name = "minePanel";
			darkGrayPen = new Pen(darkGray, 1);
			lightPen = new Pen(Color.White, 1);
			rbReset = new ResetButton();
			rbReset.Name = "rbReset";
			rbReset.Text = "";
			rbReset.Parent = this;
			rbReset.Click += new EventHandler(OnReset);

			ilLED = new ImageList();
			ilLED.ImageSize = new Size(13, 23);
			string FileName = "0123456789-";
			for (int i = 0; i < FileName.Length; i++)
			{
				ilLED.Images.Add(getBitmap(FileName[i] + ".png", false));
			}

			pnlLeft = new LEDPanel();
			pnlLeft.Parent = this;
			pnlLeft.Name = "pnlLeft";
			pnlLeft.LEDImages = ilLED;
			pnlRight = new LEDPanel();
			pnlRight.Parent = this;
			pnlRight.Name = "pnlRight";
			pnlRight.LEDImages = ilLED;

			tmrCount = new System.Windows.Forms.Timer();
			tmrCount.Interval = 1000;
			tmrCount.Tick += new EventHandler(tmrCount_Tick);
			tmrCount.Stop();
		}
		#endregion

		private void tmrCount_Tick(object sender, EventArgs e)
		{
			pnlRight.Number = pnlRight.Number + 1;
		}

		private Bitmap getBitmap(string fileName, bool transparent)
		{
			Image img =	Image.FromStream(GetResource(fileName));
			Bitmap bmp = new Bitmap(img);
			img.Dispose();
			img = null;
			if (transparent)bmp.MakeTransparent(bmp.GetPixel(1, 1));
			return bmp;
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			Rectangle rect = ClientRectangle;
			Graphics g = e.Graphics;

			// Top Border
			g.DrawLine(lightPen, rect.Left, rect.Top, rect.Right - 1, rect.Top);
			g.DrawLine(lightPen, rect.Left, rect.Top + 1, rect.Right - 1, rect.Top + 1);
			g.DrawLine(lightPen, rect.Left, rect.Top + 2, rect.Right - 1, rect.Top + 2);

			//Left Border
			g.DrawLine(lightPen, rect.Left, rect.Top, rect.Left, rect.Bottom - 1);
			g.DrawLine(lightPen, rect.Left + 1, rect.Top, rect.Left + 1, rect.Bottom - 1);
			g.DrawLine(lightPen, rect.Left + 2, rect.Top, rect.Left + 2, rect.Bottom - 1);

			drawFrame(g, new Rectangle(rect.Left + 9, rect.Top + 9, rect.Width - 9 - 6, 36), false);

			drawFrame(g, new Rectangle(rect.Left + 9, rect.Top + 9 + 36 + 6, rect.Width - 9 - 6, rect.Height - 9 - 36 - 6 - 6), true);
		}

		/// <summary>
		/// 根据MineControl的大小返回Window的大小
		/// </summary>
		public Size GetWindowClientSize(Size mineControlSize)
		{
			return new Size(mineControlSize.Width + 9 + 3 + 3 + 6, mineControlSize.Height + 9 + 2 + 36 + 2 + 6 + 3 + 4);
		}

		/// <summary>
		/// 获取MineControl的位置
		/// </summary>
		/// <returns></returns>
		public Point MineControlLocation
		{
			get
			{
				return new Point(this.ClientRectangle.Left + 9 + 3, this.ClientRectangle.Top + 9 + 36 + 6 + 3);
			}
		}

		private void drawFrame(Graphics g, Rectangle rect, bool frameWidthIsThree)
		{
			#region Top Border
			g.DrawLine(darkGrayPen, rect.Left, rect.Top, rect.Right - 1, rect.Top);
			g.DrawLine(darkGrayPen, rect.Left, rect.Top + 1, rect.Right - 2, rect.Top + 1);
			if (frameWidthIsThree)
				g.DrawLine(darkGrayPen, rect.Left, rect.Top + 2, rect.Right - 3, rect.Top + 2);
			#endregion

			#region Bottom Border
			g.DrawLine(lightPen, rect.Left + 1, rect.Bottom, rect.Right, rect.Bottom);
			g.DrawLine(lightPen, rect.Left + 2, rect.Bottom - 1, rect.Right, rect.Bottom - 1);
			if (frameWidthIsThree)
				g.DrawLine(lightPen, rect.Left + 3, rect.Bottom - 2, rect.Right, rect.Bottom - 2);
			#endregion

			#region Left Border
			g.DrawLine(darkGrayPen, rect.Left, rect.Top, rect.Left, rect.Bottom - 1);
			g.DrawLine(darkGrayPen, rect.Left + 1, rect.Top, rect.Left + 1, rect.Bottom - 2);
			if (frameWidthIsThree)
				g.DrawLine(darkGrayPen, rect.Left + 2, rect.Top, rect.Left + 2, rect.Bottom - 3);
			#endregion

			#region Right Border
			g.DrawLine(lightPen, rect.Right, rect.Top + 1, rect.Right, rect.Bottom);
			g.DrawLine(lightPen, rect.Right - 1, rect.Top + 2, rect.Right - 1, rect.Bottom);
			if (frameWidthIsThree)
				g.DrawLine(lightPen, rect.Right - 2, rect.Top + 3, rect.Right - 2, rect.Bottom);
			#endregion
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
				foreach( var name in asm.GetManifestResourceNames())
				{
					if(name.EndsWith("." + fileName ))
					{
						stream = asm.GetManifestResourceStream(name);
						break;
					}
				}
			}
			if (stream == null)
				throw new MineException("无法取得资源22: " + fileName);
			return stream;
		}

		internal void ChangeFace(int faceID)
		{
			if (rbReset.Image != null)
			{
				rbReset.Image.Dispose();
				rbReset.Image = null;
			}
			rbReset.Image = getBitmap("Face" + faceID.ToString() + ".png", true);
		}
        protected override void OnResize(EventArgs eventargs)
        {
            base.OnResize(eventargs);
        }
		public void ArrangeChildChildren()
		{
			var cs = this.ClientSize;
			rbReset.Left = 3 + (cs.Width - 3 - rbReset.Width) / 2;
			rbReset.Top = 3 + (6 + 6 + 36 - rbReset.Height) / 2;

			pnlLeft.Left = 3 + 6 + 7;
			pnlLeft.Top = rbReset.Top;
			pnlRight.Left = cs.Width - 6 - 7 - pnlRight.Width;
			pnlRight.Top = rbReset.Top;
		}

		public event EventHandler Reset;

		private void OnReset(object sender, EventArgs e)
		{
			if (Reset != null)
				Reset(this, e);
		}

		public int RemainMineCount
		{
			get
			{
				return pnlLeft.Number;
			}
			set
			{
				pnlLeft.Number = value;
			}
		}

		public int CountSecond
		{
			get
			{
				return pnlRight.Number;
			}
			set
			{
				pnlRight.Number = value;
			}
		}

		public void StartTimer()
		{
			tmrCount.Start();
		}

		public void StopTimer()
		{
			tmrCount.Stop();
		}
	}

	[SerializableAttribute]
	public class MineException: System.Exception
	{
		public MineException(string message): base(message)
		{
		}

		public MineException(): base()
		{
		}

		public MineException(string message, Exception innerException): base(message, innerException)
		{
		}
	}
}
