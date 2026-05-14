using System;
using System.Windows.Forms;
using System.Collections;

namespace Minesweeper
{
	/// <summary>
	/// 雷
	/// </summary>
	sealed public class Mine
	{
		private Mines mines;
		private Mine Mine00;
		private Mine Mine01;
		private Mine Mine02;
		private Mine Mine10;
		private Mine Mine12;
		private Mine Mine20;
		private Mine Mine21;
		private Mine Mine22;

		private int index;

		public int Index
		{
			get
			{
				return index;
			}
		}

		/// <summary>
		/// Constructor
		/// </summary>
		public Mine(int i, Mines m)
		{
			index = i;
			mines = m;
		}

		public void Init()
		{
			int i;
			i = index - mines.Width - 1;
			if (valideIndex(i) && index % mines.Width != 0)
				Mine00 = mines.mines[i];
			else
				Mine00 = null;

			i = index - mines.Width;
			if (valideIndex(i))
				Mine01 = mines.mines[i];
			else
				Mine01 = null;

			i = index - mines.Width + 1;
			if (valideIndex(i) && index % mines.Width != mines.Width - 1)
				Mine02 = mines.mines[i];
			else
				Mine02 = null;

			i = index - 1;
			if (valideIndex(i) && index % mines.Width != 0)
				Mine10 = mines.mines[i];
			else
				Mine10 = null;

			i = index + 1;
			if (valideIndex(i) && index % mines.Width != mines.Width - 1)
				Mine12 = mines.mines[i];
			else
				Mine12 = null;

			i = index + mines.Width - 1;
			if (valideIndex(i) && index % mines.Width != 0)
				Mine20 = mines.mines[i];
			else
				Mine20 = null;

			i = index + mines.Width;
			if (valideIndex(i))
				Mine21 = mines.mines[i];
			else
				Mine21 = null;

			i = index + mines.Width + 1;
			if (valideIndex(i) && index % mines.Width != mines.Width - 1)
				Mine22 = mines.mines[i];
			else
				Mine22 = null;
		}

		private bool doubt;

		/// <summary>
		/// 可疑标志
		/// </summary>
		public bool Doubt
		{
			get
			{
				return doubt;
			}
			set
			{
				doubt = value;
			}
		}

		private bool valideIndex(int i)
		{
			int Count = mines.Width * mines.Height;
			return i >= 0 && i < Count;
		}

		/// <summary>
		/// 状态
		/// </summary>
		public MineStatus MineStatus
		{
			get
			{
				return mines.MineArray[index % mines.Width, index / mines.Width];
			}
			set
			{
				mines.MineArray[index % mines.Width, index / mines.Width] =  value;
				if (mines.GameState != GameState.Complete)
					mines.updateGameState();
			}
		}

		/// <summary>
		/// 相邻区域雷数
		/// </summary>
		public int MineCount
		{
			get
			{
				if (MineStatus == MineStatus.Clear || MineStatus == MineStatus.NoMine)
				{
					int i = 0;
					if (hasMine(Mine00))
						i++;
					if (hasMine(Mine01))
						i++;
					if (hasMine(Mine02))
						i++;
					if (hasMine(Mine10))
						i++;
					if (hasMine(Mine12))
						i++;
					if (hasMine(Mine20))
						i++;
					if (hasMine(Mine21))
						i++;
					if (hasMine(Mine22))
						i++;
					return i;
				}
				else
					return -1;
			}
		}

		private bool hasMine(Mine mine)
		{
			return  (mine != null && (mine.MineStatus == MineStatus.HasMine ||
				mine.MineStatus == MineStatus.MarkedRight || mine.MineStatus == MineStatus.Exploded));
		}

		/// <summary>
		/// 标志这个雷
		/// </summary>
		public void Mark()
		{
			switch (MineStatus)
			{
				case MineStatus.NoMine:
					if (Doubt)
						Doubt = false;
					else
						MineStatus = MineStatus.MarkedWrong;
					break;
				case MineStatus.HasMine:
					if (Doubt)
						Doubt = false;
					else
						MineStatus = MineStatus.MarkedRight;
					break;
				case MineStatus.MarkedWrong:
					if (mines.AllowMarkDoubt)
						Doubt = true;
					MineStatus = MineStatus.NoMine;
					break;
				case MineStatus.MarkedRight:
					if (mines.AllowMarkDoubt)
						Doubt = true;
					MineStatus = MineStatus.HasMine;
					break;
				default:
					return;
			}
			mines.mineStatusChange(this);
		}

		/// <summary>
		/// 挖开这个雷
		/// </summary>
		public void Dig(bool update)
		{
			#region 首次挖雷需确保挖不到雷
			if (mines.GameState == GameState.NotStarted)
				mines.moveMineToOtherPlace(this);
			#endregion

			Doubt = false;
			if (MineStatus == MineStatus.HasMine)
				MineStatus = MineStatus.Exploded;
			else if (MineStatus == MineStatus.NoMine)
			{
				MineStatus = MineStatus.Clear;
				if (MineCount == 0)
				{
					digMine(Mine00);
					digMine(Mine01);
					digMine(Mine02);
					digMine(Mine10);
					digMine(Mine12);
					digMine(Mine20);
					digMine(Mine21);
					digMine(Mine22);
				}
			}
			else
				return;
			if (update)
			{
				if (MineCount == 0)
					mines.mineStatusChange(null);
				else
					mines.mineStatusChange(this);
			}
		}

		private void digMine(Mine m)
		{
			if (m != null)
				m.Dig(false);
		}

		/// <summary>
		/// 快速挖开周围的雷
		/// </summary>
		public void QuickDig()
		{
			if (MineStatus == MineStatus.Clear)
			{
				if (markedMineCount == MineCount)
				{
					autoDig(Mine00);
					autoDig(Mine01);
					autoDig(Mine02);
					autoDig(Mine10);
					autoDig(Mine12);
					autoDig(Mine20);
					autoDig(Mine21);
					autoDig(Mine22);
				}
			}
		}

		private void autoDig(Mine mine)
		{
			if (mine == null)
				return;
			if (mine.MineStatus == MineStatus.HasMine || mine.MineStatus == MineStatus.NoMine)
				mine.Dig(true);
		}

		private bool markedMine(Mine mine)
		{
			return  (mine != null && (mine.MineStatus == MineStatus.MarkedRight ||
				mine.MineStatus == MineStatus.MarkedWrong || mine.MineStatus == MineStatus.Exploded));
		}

		private int markedMineCount
		{
			get
			{
				int i = 0;
				if (markedMine(Mine00))
					i++;
				if (markedMine(Mine01))
					i++;
				if (markedMine(Mine02))
					i++;
				if (markedMine(Mine10))
					i++;
				if (markedMine(Mine12))
					i++;
				if (markedMine(Mine20))
					i++;
				if (markedMine(Mine21))
					i++;
				if (markedMine(Mine22))
					i++;
				return i;
			}
		}
	}

	/// <summary>
	/// 雷的状态
	/// </summary>
	public enum MineStatus
	{
		/// <summary>
		/// 此处无雷
		/// </summary>
		NoMine = 0,
		
		/// <summary>
		/// 此处有雷
		/// </summary>
		HasMine = 1,

		/// <summary>
		/// 此处无雷，但是被标为有雷
		/// </summary>
		MarkedWrong = 2,

		/// <summary>
		/// 此处有雷且被标记
		/// </summary>
		MarkedRight = 3,

		/// <summary>
		/// 此处已被点开
		/// </summary>
		Clear = 4,

		/// <summary>
		/// 此处的雷被引爆
		/// </summary>
		Exploded = 5
	}

	/// <summary>
	/// 游戏状态
	/// </summary>
	public enum GameState
	{
		/// <summary>
		/// 游戏尚未开始
		/// </summary>
		NotStarted,

		/// <summary>
		/// 游戏进行中
		/// </summary>
		Processing,

		/// <summary>
		/// 游戏完成
		/// </summary>
		Complete,

		/// <summary>
		/// 游戏失败
		/// </summary>
		Fail
	}

	/// <summary>
	/// 雷区
	/// </summary>
	sealed public class Mines
	{
		private int m_Width = 9;
		private int m_Height = 9;
		private int m_Count = 10;
		internal MineStatus[,] MineArray;
		internal Mine[] mines;

		/// <summary>
		/// Constructor
		/// </summary>
		public Mines()
		{
			init();
		}

		/// <summary>
		/// 宽度
		/// </summary>
		public int Width
		{
			get
			{
				return m_Width;
			}
		}

		/// <summary>
		/// 高度
		/// </summary>
		public int Height
		{
			get
			{
				return m_Height;
			}
		}

		/// <summary>
		/// 雷数
		/// </summary>
		public int Count
		{
			get
			{
				return m_Count;
			}
		}

		private void init()
		{
			MineArray = new MineStatus[Width, Height];
			mines = new Mine[Width * Height];
			for (int i = 0; i < Width; i++)
			{
				for (int j = 0; j < Height; j++)
				{
					MineArray[i, j] = MineStatus.NoMine;
					int index = j * Width + i;
					mines[index] = new Mine(index, this);
				}
			}
			for (int i = 0; i < Width; i++)
			{
				for (int j = 0; j < Height; j++)
				{
					int index = j * Width + i;
					mines[index].Init();
				}
			}

			gameState = GameState.Complete;
			#region 随机布雷1 (效率高)
			int[] temp = new int[mines.Length];
			for (int i = 0; i < temp.Length; i++)
			{
				temp[i] = i;
			}
			int d = 0, e;
			int cc = Width * Height;
			for (int c = 0; c < Count; c++)
			{
				Random random = new Random(System.Environment.TickCount / (c + 1) + System.Environment.TickCount - d);
				d = random.Next(cc - c);
				e = temp[d];
				if (mines[e].MineStatus != MineStatus.HasMine)
					mines[e].MineStatus = MineStatus.HasMine;
				else
					throw new MineException("随机布雷算法有错误。");
				temp[d] = temp[cc - 1 - c];
			}
			#endregion

			#region 随机布雷2(效率低)
//			int c = 0;
//			while (true)
//			{
//				Random random = new Random(System.Environment.TickCount / (c + 1) + System.Environment.TickCount - d);
//				int d = random.Next(Width * Height);
//				if (mines[d].MineStatus == MineStatus.NoMine)
//				{
//					mines[d].MineStatus = MineStatus.HasMine;
//					c++;
//				}
//				if (c >= Count)
//					break;
//			}
			#endregion
			gameState = GameState.NotStarted;
		}

		/// <summary>
		/// 重新设置雷
		/// </summary>
		public void Clear()
		{
			init();
		}

		public void Clear(int width, int height, int count)
		{
			if (width > 30)
				width = 30;
			if (width < 9)
				width = 9;
			if (height > 24)
				height = 24;
			if (height < 9)
				height = 9;
			m_Width = width;
			m_Height = height;

			int tCount = width * height / 2;
			if (count < 10)
				count = 10;
			if (count > tCount)
				count = tCount;
			m_Count = count;
			init();
		}

		/// <summary>
		/// 剩余未发现或标志的雷数
		/// </summary>
		public int MineRemainCount
		{
			get
			{
				if (GameState == GameState.Complete)
					return 0;

				int result = Count;
				for (int i = 0; i < Width; i++)
				{
					for (int j = 0; j < Height; j++)
					{
						Mine m = mines[j * Width + i];
						if (m.MineStatus == MineStatus.MarkedRight || m.MineStatus == MineStatus.MarkedWrong)
							result--;
					}
				}
				return result;
			}
		}

		private bool allowMarkDoubt = true;

		public bool AllowMarkDoubt
		{
			get
			{
				return allowMarkDoubt;
			}
			set
			{
				allowMarkDoubt = value;
			}
		}

		private GameState gameState = GameState.NotStarted;

		/// <summary>
		/// 游戏状态
		/// </summary>
		public GameState GameState
		{
			get
			{
				return gameState;
			}
		}

		internal void updateGameState()
		{
			int clearMine = 0;
			int cell = 0; //未挖开或未标记的数量
			foreach(Mine mine in mines)
			{
				if (mine.MineStatus == MineStatus.Exploded)
				{
					gameState = GameState.Fail;
					return;
				}
				if (mine.MineStatus == MineStatus.Clear || mine.MineStatus == MineStatus.HasMine || mine.MineStatus == MineStatus.MarkedRight)
					clearMine++;
				else if ((mine.MineStatus == MineStatus.HasMine || mine.MineStatus == MineStatus.NoMine) && !mine.Doubt)
					cell++;
			}
			if (clearMine == Width * Height)
				gameState = GameState.Complete;
			else if (cell != mines.Length)
				gameState = GameState.Processing;

			if (gameState == GameState.Complete)
			{
				foreach(Mine m in mines)
				{
					if (m.MineStatus == MineStatus.HasMine)
						m.MineStatus = MineStatus.MarkedRight;
				}
			}
		}

		public event MineStatusChangeEventHandler OnMineStatusChange;

		internal void mineStatusChange(Mine m)
		{
			if (OnMineStatusChange != null)
				OnMineStatusChange(this, new MineStatusChangeEventArgs(m));
		}

		internal void moveMineToOtherPlace(Mine m)
		{
			if (GameState == GameState.NotStarted && m.MineStatus == MineStatus.HasMine)
			{
				int[] temp = new int[Width * Height - Count];
				int j = 0;
				for (int i = 0; i < mines.Length; i++)
				{
					if (mines[i].MineStatus == MineStatus.NoMine)
					{
						temp[j] = i;
						j++;
					}
				}
				Random random = new Random(System.Environment.TickCount / (m.Index + 1) + System.Environment.TickCount);
				mines[temp[random.Next(temp.Length)]].MineStatus = MineStatus.HasMine;
				m.MineStatus = MineStatus.NoMine;
			}
		}

	}

	public delegate void MineStatusChangeEventHandler(object sender, MineStatusChangeEventArgs e);
	
	public class MineStatusChangeEventArgs: EventArgs
	{
		private Mine mine;

		public Mine Mine
		{
			get
			{
				return mine;
			}
		}

		public MineStatusChangeEventArgs(Mine m)
		{
			mine = m;
		}
	}

}
