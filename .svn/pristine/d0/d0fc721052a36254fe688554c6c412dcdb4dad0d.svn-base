using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameComponent
{
    /// <summary>
    /// 游戏结束参数类
    /// </summary>
    public class GameEndEventArgs: EventArgs
    {
        /// <summary>
        /// 棋子的颜色
        /// </summary>
        public int Color { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }

        public GameEndEventArgs(int color)
        {
            this.Color = color;
            this.Message = (color == 1 ? "黑棋" : "白棋") + "赢了";
        }

    }
}
