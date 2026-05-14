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
    public partial class frmDubug : Form
    {
        public frmDubug(GameBoard board)
        {
            InitializeComponent();
            this.board = board;
        }

        private GameBoard board = null;

        private void btnJudge_Click(object sender, EventArgs e)
        {
            int x = (int)numX.Value - 1;
            int y = (int)numY.Value - 1;
            int dir = CboDir.SelectedIndex;
            int color = CboColor.SelectedIndex + 1;
            Object obj = board.GetSituationType(x, y, dir, color);
            MessageBox.Show(obj.ToString());
        }
    }
}
