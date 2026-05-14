using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace GameComponent
{
    partial class GameBoard
    {
        /////////////////宝饭设计，精简代码，追求智慧//////////////////
        ////棋型估值

        //成五
        private const int BE_FIVE = 1000000000;
        //活四
        private const int ACTIVIE_FOUR = 50000;
        //冲四
        private const int RUSH_FOUR = 3000;
        //活三
        private const int ACTIVIE_THREE = 3000;
        //眠三
        private const int SLEEP_THREE = 200;
        //活二
        private const int ACTIVE_TWO = 200;
        //眠二
        private const int SLEEP_TWO = 10;
        //其他
        private const int OTHER = 1;


        //棋型
        public enum SituationType
        {
            BeFive,
            ActiveFour,
            RushFour,
            ActiveThree,
            SleepThree,
            ActiveTwo,
            SleepTwo,
            Other
        }

        /// <summary>
        /// 电脑下下一着棋，返回内部坐标
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public Point ComputerNextStep(int color)
        {
            //建立估分表
            int[,] gradeMyMap = new int[gameSize, gameSize];
            int[,] gradeOppoMap = new int[gameSize, gameSize];
            for (int i = 0; i < gameSize; i++)
            {
                for (int j = 0; j < gameSize; j++)
                {
                    if(map[i, j] == 0)
                    {
                        int sumMy = evaluate(j, i, color);
                        int sumOppo = evaluate(j, i, 3 - color);
                        gradeMyMap[i, j] = sumMy;
                        gradeOppoMap[i, j] = sumOppo;
                    }
                }
            }
            Point pMy = GetBestPoint(gradeMyMap);
            Point pOppo = GetBestPoint(gradeOppoMap);
            if (pMy != pOppo)
            {
                if (gradeOppoMap[pOppo.Y, pOppo.X] > gradeMyMap[pMy.Y, pMy.X] * 10)
                    return pOppo;
                else
                    return pMy;
            }
            else
                return pMy;
        }

        /// <summary>
        /// 查找最优点
        /// </summary>
        /// <param name="map">估分表数组</param>
        /// <returns></returns>
        private Point GetBestPoint(int[,] map)
        {
            int iMax = 0, jMax = 0;
            for (int i = 0; i < gameSize; i++)
            {
                for (int j = 0; j < gameSize; j++)
                {
                    if (map[i, j] > map[iMax, jMax])
                    {
                        iMax = i;
                        jMax = j;
                    }
                }
            }
            return new Point(jMax, iMax);
        }


        /// <summary>
        /// 四个方向上对某个点某种颜色棋子进行估分
        /// </summary>
        /// <param name="x">横坐标</param>
        /// <param name="y">纵坐标</param>
        /// <param name="color"></param>
        /// <returns></returns>
        public int evaluate(int x, int y, int color)
        {
            int sum = 0;
            //四个方向估值
            for (int i = 0; i < 4; i++)
            {
                SituationType type = GetSituationType(x, y, i, color);
                switch(type)
                {
                    case SituationType.BeFive:
                        sum += BE_FIVE;
                        break;
                    case SituationType.ActiveFour:
                        sum += ACTIVIE_FOUR;
                        break;
                    case SituationType.RushFour:
                        sum += RUSH_FOUR;
                        break;
                    case SituationType.ActiveThree:
                        sum += ACTIVIE_THREE;
                        break;
                    case SituationType.SleepThree:
                        sum += SLEEP_THREE;
                        break;
                    case SituationType.ActiveTwo:
                        sum += ACTIVE_TWO;
                        break;
                    case SituationType.SleepTwo:
                        sum += SLEEP_TWO;
                        break;
                    default:
                        sum += OTHER;
                        break;
                }
            }
            return sum;
        }

        /// <summary>
        /// 某种颜色棋子在某个方向上的棋型
        /// </summary>
        /// <param name="x">横坐标</param>
        /// <param name="y">纵坐标</param>
        /// <param name="dir">方向</param>
        /// <param name="color">棋子颜色</param>
        /// <returns></returns>
        public SituationType GetSituationType(int x, int y, int dir, int color)
        {
            SituationType type = JudgeBeFive(x, y, dir, color);
            if (type == SituationType.Other)
            {
                type = JudgeActiveFour(x, y, dir, color);
                if (type == SituationType.Other)
                {
                    type = JudgeRushFour(x, y, dir, color);
                    if (type == SituationType.Other)
                    {
                        type = JudgeActiveThreeSleepThree(x, y, dir, color);
                        if (type == SituationType.Other)
                        {
                            return type = JudgeActvieTwoSleepTwo(x, y, dir, color);
                        }
                        else
                            return type;
                    }
                    else
                        return type;
                }
                else
                    return type;
            }
            else
                return type;
        }

        /// <summary>
        /// 判断一个坐标是否没越界
        /// </summary>
        /// 
        /// <param name="x">横坐标</param>
        /// <param name="y">纵坐标</param>
        /// <returns></returns>
        private bool NoCrossBorder(int x, int y)
        {
            return x >= 0 && x < gameSize && y >= 0 && y < gameSize;
        }

        /// <summary>
        /// 在没有越界的基础上，是否存在同颜色的棋子
        /// </summary>
        /// <param name="x">横坐标</param>
        /// <param name="y">纵坐标</param>
        /// <param name="color">棋子颜色</param>
        /// <returns></returns>
        private bool ExistSameColor(int x, int y, int color)
        {
            return NoCrossBorder(x, y) && map[y, x] == color;
        }

        /// <summary>
        /// 在没有越界的基础上，判断某个点是否没有棋子
        /// </summary>
        /// <param name="x">横坐标</param>
        /// <param name="y">纵坐标</param>
        /// <returns></returns>
        private bool Empty(int x, int y)
        {
            return NoCrossBorder(x, y) && map[y, x] == 0;
        }

        /// <summary>
        /// 处理值0，如果为0，返回-1，其余不变
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        private int SolveZero(int n)
        {
            return n == 0 ? 1 : n;
        }


        /// <summary>
        /// 某种颜色棋子在某个方向上是否成五
        /// </summary>
        /// <param name="x">横坐标</param>
        /// <param name="y">纵坐标</param>
        /// <param name="dir">方向</param>
        /// <param name="color">棋子颜色</param>
        /// <returns></returns>
        private SituationType JudgeBeFive(int x, int y, int dir, int color)
        {
            for (int i = 1; i >= -4; i--)
            {
                if (i == 0)
                    continue;
                else
                {
                    int delta1 = i;
                    int delta2 = SolveZero(i + 1);
                    int delta3 = SolveZero(delta2 + 1);
                    int delta4 = SolveZero(delta3 + 1);
                    if (dir == 0) //上下方向
                    {
                        Point p1 = new Point(x, y + delta1);
                        Point p2 = new Point(x, y + delta2);
                        Point p3 = new Point(x, y + delta3);
                        Point p4 = new Point(x, y + delta4);
                        if (BeFiveHelper(p1, p2, p3, p4, color))
                            return SituationType.BeFive;
                    }
                    else if (dir == 1) //左右方向
                    {
                        Point p1 = new Point(x + delta1, y);
                        Point p2 = new Point(x + delta2, y);
                        Point p3 = new Point(x + delta3, y);
                        Point p4 = new Point(x + delta4, y);
                        if (BeFiveHelper(p1, p2, p3, p4, color))
                            return SituationType.BeFive;
                    }
                    else if (dir == 2) //左斜线
                    {
                        Point p1 = new Point(x + delta1, y + delta1);
                        Point p2 = new Point(x + delta2, y + delta2);
                        Point p3 = new Point(x + delta3, y + delta3);
                        Point p4 = new Point(x + delta4, y + delta4);
                        if (BeFiveHelper(p1, p2, p3, p4, color))
                            return SituationType.BeFive;
                    }
                    else //右斜线
                    {
                        Point p1 = new Point(x + delta1, y - delta1);
                        Point p2 = new Point(x + delta2, y - delta2);
                        Point p3 = new Point(x + delta3, y - delta3);
                        Point p4 = new Point(x + delta4, y - delta4);
                        if (BeFiveHelper(p1, p2, p3, p4, color))
                            return SituationType.BeFive;
                    }
                }
            }
            return SituationType.Other;
        }

        /// <summary>
        /// 成五条件判断
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        /// <param name="p4"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        private bool BeFiveHelper(Point p1, Point p2, Point p3, Point p4, int color)
        {
            if (ExistSameColor(p1.X, p1.Y, color) && ExistSameColor(p2.X, p2.Y, color) && ExistSameColor(p3.X, p3.Y, color) && ExistSameColor(p4.X, p4.Y, color))
                return true;
            else
                return false;
        }

        /// <summary>
        /// 某种颜色棋子在某个方向上是否活四
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="dir"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        private SituationType JudgeActiveFour(int x, int y, int dir, int color)
        {
            for (int i = -1; i >= -4; i--)
            {
                if (i == 0)
                    continue;
                else
                {
                    int delta1 = i;
                    int delta2 = SolveZero(i + 1);
                    int delta3 = SolveZero(delta2 + 1);
                    int delta4 = SolveZero(delta3 + 1);
                    int delta5 = SolveZero(delta4 + 1);

                    if (dir == 0) //上下方向
                    {
                        Point p1 = new Point(x, y + delta1);
                        Point p2 = new Point(x, y + delta2);
                        Point p3 = new Point(x, y + delta3);
                        Point p4 = new Point(x, y + delta4);
                        Point p5 = new Point(x, y + delta5);
                        if (ActiveFourHelper(p1, p2, p3, p4, p5, color))
                            return SituationType.ActiveFour;
                    }
                    else if (dir == 1) //左右方向
                    {
                        Point p1 = new Point(x + delta1, y);
                        Point p2 = new Point(x + delta2, y);
                        Point p3 = new Point(x + delta3, y);
                        Point p4 = new Point(x + delta4, y);
                        Point p5 = new Point(x + delta5, y);
                        if (ActiveFourHelper(p1, p2, p3, p4, p5, color))
                            return SituationType.ActiveFour;
                    }
                    else if (dir == 2) //左斜线
                    {
                        Point p1 = new Point(x + delta1, y + delta1);
                        Point p2 = new Point(x + delta2, y + delta2);
                        Point p3 = new Point(x + delta3, y + delta3);
                        Point p4 = new Point(x + delta4, y + delta4);
                        Point p5 = new Point(x + delta5, y + delta5);
                        if (ActiveFourHelper(p1, p2, p3, p4, p5, color))
                            return SituationType.ActiveFour;
                    }
                    else //右斜线
                    {
                        Point p1 = new Point(x + delta1, y - delta1);
                        Point p2 = new Point(x + delta2, y - delta2);
                        Point p3 = new Point(x + delta3, y - delta3);
                        Point p4 = new Point(x + delta4, y - delta4);
                        Point p5 = new Point(x + delta5, y - delta5);
                        if (ActiveFourHelper(p1, p2, p3, p4, p5, color))
                            return SituationType.ActiveFour;
                    }
                }
            }
            return SituationType.Other;
        }

        /// <summary>
        /// 活四条件判断
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        /// <param name="p4"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        private bool ActiveFourHelper(Point p1, Point p2, Point p3, Point p4, Point p5, int color)
        {
            if (Empty(p1.X, p1.Y) && ExistSameColor(p2.X, p2.Y, color) && ExistSameColor(p3.X, p3.Y, color) && ExistSameColor(p4.X, p4.Y, color) && Empty(p5.X, p5.Y))
                return true;
            else
                return false;
        }


        /// <summary>
        /// 某种颜色棋子在某个方向上是否冲四
        /// </summary>
        /// <param name="x">横坐标</param>
        /// <param name="y">纵坐标</param>
        /// <param name="dir">方向</param>
        /// <param name="color">棋子颜色</param>
        /// <returns></returns>
        private SituationType JudgeRushFour(int x, int y, int dir, int color)
        {
            for (int i = 1; i >= -4; i--)
            {
                if (i == 0)
                    continue;
                else
                {
                    int delta1 = i;
                    int delta2 = SolveZero(i + 1);
                    int delta3 = SolveZero(delta2 + 1);
                    int delta4 = SolveZero(delta3 + 1);

                    if (dir == 0) //上下方向
                    {
                        Point p1 = new Point(x, y + delta1);
                        Point p2 = new Point(x, y + delta2);
                        Point p3 = new Point(x, y + delta3);
                        Point p4 = new Point(x, y + delta4);
                        if (RushFourHelper(p1, p2, p3, p4, color))
                            return SituationType.RushFour;
                    }
                    else if (dir == 1) //左右方向
                    {
                        Point p1 = new Point(x + delta1, y);
                        Point p2 = new Point(x + delta2, y);
                        Point p3 = new Point(x + delta3, y);
                        Point p4 = new Point(x + delta4, y);
                        if (RushFourHelper(p1, p2, p3, p4, color))
                            return SituationType.RushFour;
                    }
                    else if (dir == 2) //左斜线
                    {
                        Point p1 = new Point(x + delta1, y + delta1);
                        Point p2 = new Point(x + delta2, y + delta2);
                        Point p3 = new Point(x + delta3, y + delta3);
                        Point p4 = new Point(x + delta4, y + delta4);
                        if (RushFourHelper(p1, p2, p3, p4, color))
                            return SituationType.RushFour;
                    }
                    else //右斜线
                    {
                        Point p1 = new Point(x + delta1, y - delta1);
                        Point p2 = new Point(x + delta2, y - delta2);
                        Point p3 = new Point(x + delta3, y - delta3);
                        Point p4 = new Point(x + delta4, y - delta4);
                        if (RushFourHelper(p1, p2, p3, p4, color))
                            return SituationType.RushFour;
                    }
                }
            }
            return SituationType.Other;
        }

        /// <summary>
        /// 冲四条件判断
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        /// <param name="p4"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        private bool RushFourHelper(Point p1, Point p2, Point p3, Point p4, int color)
        {
            if (Empty(p1.X, p1.Y) && ExistSameColor(p2.X, p2.Y, color) && ExistSameColor(p3.X, p3.Y, color) && ExistSameColor(p4.X, p4.Y, color))
                return true;
            else if (ExistSameColor(p1.X, p1.Y, color) && Empty(p2.X, p2.Y) && ExistSameColor(p3.X, p3.Y, color) && ExistSameColor(p4.X, p4.Y, color))
                return true;
            else if (ExistSameColor(p1.X, p1.Y, color) && ExistSameColor(p2.X, p2.Y, color) && Empty(p3.X, p3.Y) && ExistSameColor(p4.X, p4.Y, color))
                return true;
            else if (ExistSameColor(p1.X, p1.Y, color) && ExistSameColor(p2.X, p2.Y, color) && ExistSameColor(p3.X, p3.Y, color) && Empty(p4.X, p4.Y))
                return true;
            else
                return false;
        }


        /// <summary>
        /// 某种颜色棋子在某个方向上是否活三，眠三
        /// </summary>
        /// <param name="x">横坐标</param>
        /// <param name="y">纵坐标</param>
        /// <param name="dir">方向</param>
        /// <param name="color">棋子颜色</param>
        /// <returns></returns>
        private SituationType JudgeActiveThreeSleepThree(int x, int y, int dir, int color)
        {
            for (int i = -1; i >= -4; i--)
            {
                int delta1 = i;
                int delta2 = SolveZero(i + 1);
                int delta3 = SolveZero(delta2 + 1);
                int delta4 = SolveZero(delta3 + 1);
                int delta5 = SolveZero(delta4 + 1);

                if (dir == 0) //上下方向
                {
                    Point p1 = new Point(x, y + delta1);
                    Point p2 = new Point(x, y + delta2);
                    Point p3 = new Point(x, y + delta3);
                    Point p4 = new Point(x, y + delta4);
                    Point p5 = new Point(x, y + delta5);
                    int res = ActiveThreeSleepThreeHelper(p1, p2, p3, p4, p5, color);
                    if (res == 2)
                        return SituationType.ActiveThree;
                    else if (res == 1)
                        return SituationType.SleepThree;
                }
                else if (dir == 1) //左右方向
                {
                    Point p1 = new Point(x + delta1, y);
                    Point p2 = new Point(x + delta2, y);
                    Point p3 = new Point(x + delta3, y);
                    Point p4 = new Point(x + delta4, y);
                    Point p5 = new Point(x + delta5, y);
                    int res = ActiveThreeSleepThreeHelper(p1, p2, p3, p4, p5, color);
                    if (res == 2)
                        return SituationType.ActiveThree;
                    else if (res == 1)
                        return SituationType.SleepThree;
                }
                else if (dir == 2) //左斜线
                {
                    Point p1 = new Point(x + delta1, y + delta1);
                    Point p2 = new Point(x + delta2, y + delta2);
                    Point p3 = new Point(x + delta3, y + delta3);
                    Point p4 = new Point(x + delta4, y + delta4);
                    Point p5 = new Point(x + delta5, y + delta5);
                    int res = ActiveThreeSleepThreeHelper(p1, p2, p3, p4, p5, color);
                    if (res == 2)
                        return SituationType.ActiveThree;
                    else if (res == 1)
                        return SituationType.SleepThree;
                }
                else //右斜线
                {
                    Point p1 = new Point(x + delta1, y - delta1);
                    Point p2 = new Point(x + delta2, y - delta2);
                    Point p3 = new Point(x + delta3, y - delta3);
                    Point p4 = new Point(x + delta4, y - delta4);
                    Point p5 = new Point(x + delta5, y - delta5);
                    int res = ActiveThreeSleepThreeHelper(p1, p2, p3, p4, p5, color);
                    if (res == 2)
                        return SituationType.ActiveThree;
                    else if (res == 1)
                        return SituationType.SleepThree;
                }
            }
            return SituationType.Other;
        }


        /// <summary>
        /// 活三，眠三判断条件
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        /// <param name="p4"></param>
        /// <param name="p5"></param>
        /// <param name="color"></param>
        /// <returns>2为活三，1为眠三</returns>
        private int ActiveThreeSleepThreeHelper(Point p1, Point p2, Point p3, Point p4, Point p5, int color)
        {
            bool oneSide = (!Empty(p5.X, p5.Y) && Empty(p1.X, p1.Y)) || (Empty(p5.X, p5.Y) && !Empty(p1.X, p1.Y));
            if (Empty(p1.X, p1.Y) && Empty(p2.X, p2.Y) && ExistSameColor(p3.X, p3.Y, color) && ExistSameColor(p4.X, p4.Y, color) && Empty(p5.X, p5.Y))
                return 2;
            else if (Empty(p1.X, p1.Y) && ExistSameColor(p2.X, p2.Y, color) && Empty(p3.X, p3.Y) && ExistSameColor(p4.X, p4.Y, color) && Empty(p5.X, p5.Y))
                return 2;
            else if (Empty(p1.X, p1.Y) && ExistSameColor(p2.X, p2.Y, color) && ExistSameColor(p3.X, p3.Y, color) && Empty(p4.X, p4.Y) && Empty(p5.X, p5.Y))
                return 2;
            else if (Empty(p2.X, p2.Y) && ExistSameColor(p3.X, p3.Y, color) && ExistSameColor(p4.X, p4.Y, color) && oneSide)
                return 1;
            else if (ExistSameColor(p2.X, p2.Y, color) && Empty(p3.X, p3.Y) && ExistSameColor(p4.X, p4.Y, color) && oneSide)
                return 1;
            else if (ExistSameColor(p2.X, p2.Y, color) && ExistSameColor(p3.X, p3.Y, color) && Empty(p4.X, p4.Y) && oneSide)
                return 1;
            else
                return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="dir"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        private SituationType JudgeActvieTwoSleepTwo(int x, int y, int dir, int color)
        {
            for (int i = -1; i >= -4; i--)
            {
                int delta1 = i;
                int delta2 = SolveZero(i + 1);
                int delta3 = SolveZero(delta2 + 1);
                int delta4 = SolveZero(delta3 + 1);
                int delta5 = SolveZero(delta4 + 1);

                if (dir == 0) //上下方向
                {
                    Point p1 = new Point(x, y + delta1);
                    Point p2 = new Point(x, y + delta2);
                    Point p3 = new Point(x, y + delta3);
                    Point p4 = new Point(x, y + delta4);
                    Point p5 = new Point(x, y + delta5);
                    int res = ActiveTwoSleepTwoHelper(p1, p2, p3, p4, p5, color);
                    if (res == 2)
                        return SituationType.ActiveTwo;
                    else if (res == 1)
                        return SituationType.SleepTwo;
                }
                else if (dir == 1) //左右方向
                {
                    Point p1 = new Point(x + delta1, y);
                    Point p2 = new Point(x + delta2, y);
                    Point p3 = new Point(x + delta3, y);
                    Point p4 = new Point(x + delta4, y);
                    Point p5 = new Point(x + delta5, y);
                    int res = ActiveTwoSleepTwoHelper(p1, p2, p3, p4, p5, color);
                    if (res == 2)
                        return SituationType.ActiveTwo;
                    else if (res == 1)
                        return SituationType.SleepTwo;
                }
                else if (dir == 2) //左斜线
                {
                    Point p1 = new Point(x + delta1, y + delta1);
                    Point p2 = new Point(x + delta2, y + delta2);
                    Point p3 = new Point(x + delta3, y + delta3);
                    Point p4 = new Point(x + delta4, y + delta4);
                    Point p5 = new Point(x + delta5, y + delta5);
                    int res = ActiveTwoSleepTwoHelper(p1, p2, p3, p4, p5, color);
                    if (res == 2)
                        return SituationType.ActiveTwo;
                    else if (res == 1)
                        return SituationType.SleepTwo;
                }
                else //右斜线
                {
                    Point p1 = new Point(x + delta1, y - delta1);
                    Point p2 = new Point(x + delta2, y - delta2);
                    Point p3 = new Point(x + delta3, y - delta3);
                    Point p4 = new Point(x + delta4, y - delta4);
                    Point p5 = new Point(x + delta5, y - delta5);
                    int res = ActiveTwoSleepTwoHelper(p1, p2, p3, p4, p5, color);
                    if (res == 2)
                        return SituationType.ActiveTwo;
                    else if (res == 1)
                        return SituationType.SleepTwo;
                }
            }
            return SituationType.Other;
        }

        /// <summary>
        /// 活二，眠二判断条件
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        /// <param name="p4"></param>
        /// <param name="p5"></param>
        /// <param name="color"></param>
        /// <returns>2为活三，1为眠三</returns>
        private int ActiveTwoSleepTwoHelper(Point p1, Point p2, Point p3, Point p4, Point p5, int color)
        {
            bool oneSide = (!Empty(p5.X, p5.Y) && Empty(p1.X, p1.Y)) || (Empty(p5.X, p5.Y) && !Empty(p1.X, p1.Y));
            if (Empty(p1.X, p1.Y) && ExistSameColor(p2.X, p2.Y, color) && Empty(p3.X, p3.Y) && Empty(p4.X, p4.Y) && Empty(p5.X, p5.Y))
                return 2;
            else if (Empty(p1.X, p1.Y) && Empty(p2.X, p2.Y) && ExistSameColor(p3.X, p3.Y, color) && Empty(p4.X, p4.Y) && Empty(p5.X, p5.Y))
                return 2;
            else if (Empty(p1.X, p1.Y) && Empty(p2.X, p2.Y) && Empty(p3.X, p3.Y) && ExistSameColor(p4.X, p4.Y, color) && Empty(p5.X, p5.Y))
                return 2;
            else if (ExistSameColor(p2.X, p2.Y, color) && Empty(p3.X, p3.Y) && Empty(p4.X, p4.Y) && oneSide)
                return 1;
            else if (Empty(p2.X, p2.Y) && ExistSameColor(p3.X, p3.Y, color) && Empty(p4.X, p4.Y) && oneSide)
                return 1;
            else if (Empty(p2.X, p2.Y) && Empty(p3.X, p3.Y) && ExistSameColor(p4.X, p4.Y, color) && oneSide)
                return 1;
            else
                return 0;
        }


    }
}
