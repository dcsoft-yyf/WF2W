using GameComponent;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Chess
{
    public partial class frmMain : Form
    {
        private GameBoard board;

        public frmMain()
        {
            InitializeComponent();
            setGameBoard();
            
        }

        private void setGameBoard()
        {
            


            board = new GameBoard();
            board.OnGameEnd += board_OnGameEnd;
            this.Controls.Add(board);
            Rectangle rec = this.ClientRectangle;
            int xPos = (rec.Width - board.Width) / 2;
            int yPos = (rec.Height - menu.Height - board.Height) / 2 + menu.Height;
            board.Left = xPos;
            board.Top = yPos;

            board.StartGame();////////////////////
        }

        private void board_OnGameEnd(object sender, GameComponent.GameEndEventArgs e)
        {
            MessageBox.Show(e.Message, "提示", MessageBoxButtons.OK);
        }
#if WF2W
        private async Task menuItemNewGame_Click(object sender, EventArgs e)
#else
        private void menuItemNewGame_Click(object sender, EventArgs e)
#endif
        {
            board.ReStartGame();
        }

#if WF2W
        private async Task menuItemDebug_Click(object sender, EventArgs e)
#else
        private void menuItemDebug_Click(object sender, EventArgs e)
#endif
        {
            frmDubug frm = new frmDubug(this.board);
            frm.Show();
        }

#if WF2W
        private async Task menuItemExit_Click(object sender, EventArgs e)
#else
        private void menuItemExit_Click(object sender, EventArgs e)
#endif
        {
            this.Close();
        }

#if WF2W
        private async Task menuItemAbout_Click(object sender, EventArgs e)
#else
        private void menuItemAbout_Click(object sender, EventArgs e)
#endif
        {
            MessageBox.Show("本游戏由火蜥蜴开发^_^，\nQQ：1906747819");
        }

    }
}
