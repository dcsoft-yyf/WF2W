using System;
using System.Drawing ;
using System.Collections.Generic;

namespace DCSoft.Drawing
{
	/// <summary>
	/// 定义矩形相关的例称模块，本对象不能实例化
	/// </summary>
    /// <remarks>编制 袁永福</remarks>
    [System.Runtime.InteropServices.ComVisible(false)]
    //[System.Reflection.Obfuscation( Exclude= true , ApplyToMembers = true )]
    public static class RectangleCommon
	{
//        /// <summary>
//        /// 缩放矩形
//        /// </summary>
//        /// <param name="rect">矩形区域</param>
//        /// <param name="x0">原点X坐标</param>
//        /// <param name="y0">原点Y坐标</param>
//        /// <param name="xZoomRate">X方向缩放比率</param>
//        /// <param name="yZoomRate">Y方向缩放比率</param>
//        /// <returns>转换后的矩形</returns>
//        public static RectangleF ZoomRectangleF(RectangleF rect, float x0, float y0, float xZoomRate, float yZoomRate)
//        {
//            float x1 = x0 + (rect.X - x0) * xZoomRate;
//            float y1 = y0 + (rect.Y - y0) * yZoomRate;
//            float x2 = x0 + (rect.Right - x0) * xZoomRate;
//            float y2 = y0 + (rect.Bottom - y0) * yZoomRate;
//            return new RectangleF(x1, y1, x2 - x1, y2 - y1);
//        }
//#if WINFORM || DCWriterForWinFormNET6 || DCWriterForWASM
//        public static RectangleF[] CompressRectangles(RectangleF[] rects)
//        {
//            List<RectangleF> result = new List<RectangleF>();
//            foreach (RectangleF rect in rects)
//            {
//                if (rect.IsEmpty)
//                {
//                    continue;
//                }
//                RectangleF rect2 = rect;
//                bool addItem = true  ;
//                for (int iCount2 = 0; iCount2 < result.Count; iCount2++)
//                {
//                    RectangleF rect3 = result[iCount2];
//                    if (rect3.Contains(rect2))
//                    {
//                        addItem = false;
//                        break;
//                    }
//                    else if (rect2.Contains(rect3))
//                    {
//                        result[iCount2] = rect2;
//                        addItem = false;
//                        break;
//                    }
//                    else if (rect2.Top == rect3.Top 
//                        && rect2.Height == rect3.Height )
//                    {
//                        // 两个矩形可能横向合并
//                        if (rect2.IntersectsWith(rect3)
//                            || rect2.Left == rect3.Right
//                            || rect2.Left == rect3.Right + 1 
//                            || rect2.Right == rect3.Left
//                            || rect2.Right == rect3.Left -1 )
//                        {
//                            result[iCount2] = RectangleF.Union(rect2, rect3);
//                            addItem = false;
//                            break;
//                        }
//                    }
//                    else if (rect2.Left == rect3.Left
//                        && rect2.Width == rect3.Width)
//                    {
//                        // 两个矩形可能纵向合并
//                        if (rect2.IntersectsWith(rect3)
//                            || rect2.Top == rect3.Bottom
//                            || rect2.Top == rect3.Bottom + 1
//                            || rect2.Bottom == rect3.Top
//                            || rect2.Bottom == rect3.Top - 1)
//                        {
//                            result[iCount2] = RectangleF.Union(rect2, rect3);
//                            addItem = false;
//                            break;
//                        }
//                    }
//                }//for
//                if (addItem)
//                {
//                    result.Add(rect2);
//                }
//            }//foreach
//            return result.ToArray();
//        }
 
//        /// <summary>
//        /// 修改矩形
//        /// </summary>
//        /// <remarks>
//        /// 本函数支持9种修改方式
//        /// 0：移动矩形的左上角的位置，右下角保持不变。
//        /// 1：移动上边框线的位置，其他边框线位置不变。
//        /// 2：移动右上角的位置，左下角保持不变。
//        /// 3：移动右边框线的位置，其他边框线位置不变。
//        /// 4：移动右下角位置，左上角保持不变。
//        /// 5：移动下边框线的位置，其他边框线位置不变。
//        /// 6：移动左下角位置，右上角位置不变。
//        /// 7：移动左边框线位置，其他边框线位置不变。
//        /// 8：移动矩形位置，矩形宽度和高度不变。
//        /// </remarks>
//        /// <param name="rect">原始矩形</param>
//        /// <param name="dx">横向偏移量</param>
//        /// <param name="dy">纵向偏移量</param>
//        /// <param name="controlPoint">修改方式</param>
//        /// <returns>修改后的矩形</returns>
//        public static Rectangle ChangeRectangle(Rectangle rect, int dx, int dy, int controlPoint)
//        {
//            if (dx != 0 || dy != 0)
//            {
//                switch (controlPoint)
//                {
//                    case 0 :// 修改左上角位置
//                        rect.X = rect.X + dx;
//                        rect.Width = rect.Width - dx;
//                        rect.Y = rect.Y + dy;
//                        rect.Height = rect.Height - dy;
//                        break;
//                    case 1:// 修改顶端位置，其他不变
//                        rect.Y = rect.Y + dy;
//                        rect.Height = rect.Height - dy;
//                        break;
//                    case 2:// 修改右上角位置
//                        rect.Width = rect.Width + dx;
//                        rect.Y = rect.Y + dy;
//                        rect.Height = rect.Height - dy;
//                        break;
//                    case 3:// 修改右边位置
//                        rect.Width = rect.Width + dx;
//                        break;
//                    case 4:// 修改右下角位置
//                        rect.Width = rect.Width + dx;
//                        rect.Height = rect.Height + dy;
//                        break;
//                    case 5:// 修改低端位置
//                        rect.Height = rect.Height + dy;
//                        break;
//                    case 6:// 修改左下角位置
//                        rect.X = rect.X + dx;
//                        rect.Width = rect.Width - dx;
//                        rect.Height = rect.Height + dy;
//                        break;
//                    case 7:// 修改左端位置
//                        rect.X = rect.X + dx;
//                        rect.Width = rect.Width - dx;
//                        break;
//                    case 8:// 移动矩形
//                        rect.X = rect.X + dx;
//                        rect.Y = rect.Y + dy;
//                        break;
//                }
//            }
//            return rect;
//        }

//        /// <summary>
//        /// 修改矩形
//        /// </summary>
//        /// <remarks>
//        /// 本函数支持9种修改方式
//        /// 0：移动矩形的左上角的位置，右下角保持不变。
//        /// 1：移动上边框线的位置，其他边框线位置不变。
//        /// 2：移动右上角的位置，左下角保持不变。
//        /// 3：移动右边框线的位置，其他边框线位置不变。
//        /// 4：移动右下角位置，左上角保持不变。
//        /// 5：移动下边框线的位置，其他边框线位置不变。
//        /// 6：移动左下角位置，右上角位置不变。
//        /// 7：移动左边框线位置，其他边框线位置不变。
//        /// 8：移动矩形位置，矩形宽度和高度不变。
//        /// </remarks>
//        /// <param name="rect">原始矩形</param>
//        /// <param name="dx">横向偏移量</param>
//        /// <param name="dy">纵向偏移量</param>
//        /// <param name="controlPoint">修改方式</param>
//        /// <returns>修改后的矩形</returns>
//        public static RectangleF ChangeRectangle(RectangleF rect, float dx, float dy, int controlPoint)
//        {
//            if (dx != 0 || dy != 0)
//            {
//                switch (controlPoint)
//                {
//                    case 0:// 修改左上角位置
//                        rect.X = rect.X + dx;
//                        rect.Width = rect.Width - dx;
//                        rect.Y = rect.Y + dy;
//                        rect.Height = rect.Height - dy;
//                        break;
//                    case 1:// 修改顶端位置，其他不变
//                        rect.Y = rect.Y + dy;
//                        rect.Height = rect.Height - dy;
//                        break;
//                    case 2:// 修改右上角位置
//                        rect.Width = rect.Width + dx;
//                        rect.Y = rect.Y + dy;
//                        rect.Height = rect.Height - dy;
//                        break;
//                    case 3:// 修改右边位置
//                        rect.Width = rect.Width + dx;
//                        break;
//                    case 4:// 修改右下角位置
//                        rect.Width = rect.Width + dx;
//                        rect.Height = rect.Height + dy;
//                        break;
//                    case 5:// 修改低端位置
//                        rect.Height = rect.Height + dy;
//                        break;
//                    case 6:// 修改左下角位置
//                        rect.X = rect.X + dx;
//                        rect.Width = rect.Width - dx;
//                        rect.Height = rect.Height + dy;
//                        break;
//                    case 7:// 修改左端位置
//                        rect.X = rect.X + dx;
//                        rect.Width = rect.Width - dx;
//                        break;
//                    case 8:// 移动矩形
//                        rect.X = rect.X + dx;
//                        rect.Y = rect.Y + dy;
//                        break;
//                }
//            }
//            return rect;
//        }
//#endif

        /////// <summary>
        /////// 获得距离指定点最近的矩形区域,各个矩形区域相互不相重
        /////// </summary>
        /////// <param name="x"></param>
        /////// <param name="y"></param>
        /////// <param name="rectangles"></param>
        /////// <returns></returns>
        ////public static int GetNearestRectangle(float x, float y, RectangleF[] rectangles)
        ////{
        ////    float minLen = 0;
        ////    int index = -1;
        ////    for (int iCount = 0; iCount < rectangles.Length; iCount++)
        ////    {
        ////        RectangleF rect = rectangles[iCount];
        ////        if (rect.Contains(x, y))
        ////        {
        ////            return iCount;
        ////        }
        ////        float len = GetDistance(x, y, rect);
        ////        if ( iCount == 0 || len < minLen)
        ////        {
        ////            minLen = len;
        ////            index = iCount;
        ////        }
        ////    }//for
        ////    return index;
        ////}

        ///// <summary>
        ///// 获得指定点相对于指定矩形的距离
        ///// </summary>
        ///// <param name="x">指定点的X坐标</param>
        ///// <param name="y">指定点的Y坐标</param>
        ///// <param name="rectangle">矩形边框</param>
        ///// <returns>距离，若小于0则点被包含在矩形区域中</returns>
        //public static float GetDistance(float x, float y, RectangleF rectangle)
        //{
        //    float len = 0;
        //    int area = GetRectangleArea(rectangle, x, y);
        //    switch (area)
        //    {
        //        case 0:
        //            len = Math.Min(x - rectangle.Left, rectangle.Right - x);
        //            float len2 = Math.Min(y - rectangle.Top, rectangle.Bottom - y);
        //            len = - Math.Min(len, len2);
        //            break;
        //        case 1:
        //            len = (float)Math.Sqrt(
        //                (x - rectangle.Left) * (x - rectangle.Left)
        //                + (y - rectangle.Top) * (y - rectangle.Top));
        //            break;
        //        case 2:
        //            len = rectangle.Top - y;
        //            break;
        //        case 3:
        //            len = (float)Math.Sqrt(
        //                (x - rectangle.Right) * (x - rectangle.Right)
        //                + (y - rectangle.Top) * (y - rectangle.Top));
        //            break;
        //        case 4:
        //            len = x - rectangle.Right;
        //            break;
        //        case 5:
        //            len = (float)Math.Sqrt(
        //                (x - rectangle.Right) * (x - rectangle.Right)
        //                + (y - rectangle.Bottom) * (y - rectangle.Bottom));
        //            break;
        //        case 6:
        //            len = y - rectangle.Bottom;
        //            break;
        //        case 7:
        //            len = (float)Math.Sqrt(
        //                (x - rectangle.Left) * (x - rectangle.Left)
        //                + (y - rectangle.Bottom) * (y - rectangle.Bottom));
        //            break;
        //        case 8:
        //            len = rectangle.Left - x;
        //            break;
        //    }
        //    return len;
        //}

        /// <summary>
        /// 获得指定点在指定矩形相对的区域编号
        /// </summary>
        /// <remarks>
        /// 
        ///   1  |   2   |  3
        /// -----+=======+-------
        ///      ||     ||
        ///   8  ||  0  ||  4
        ///      ||     ||
        /// -----+=======+-------
        ///   7  |   6   |  5
        ///   
        /// </remarks>
        /// <param name="rectangle"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static int GetRectangleArea(RectangleF rectangle, float x, float y)
        {
            if (rectangle.Contains(x, y))
            {
                return 0;
            }
            else if (y < rectangle.Top)
            {
                if (x < rectangle.Left)
                {
                    return 1;
                }
                else if (x < rectangle.Right)
                {
                    return 2;
                }
                else
                {
                    return 3;
                }
            }
            else if (y < rectangle.Bottom)
            {
                if (x < rectangle.Left)
                {
                    return 8;
                }
                else if (x < rectangle.Right)
                {
                    return 0;
                }
                else
                {
                    return 4;
                }
            }
            else
            {
                if (x < rectangle.Left)
                {
                    return 7;
                }
                else if (x < rectangle.Right)
                {
                    return 6;
                }
                else
                {
                    return 5;
                }
            }
        }

        /// <summary>
        /// 获得两个矩形间的一个过渡矩形
        /// </summary>
        /// <param name="rect1">原始矩形</param>
        /// <param name="rect2">目标矩形</param>
        /// <param name="rate">过渡系数，若为0则返回原始矩形，若为1则返回目标矩形</param>
        /// <returns>过渡矩形</returns>
        //      public static System.Drawing.Rectangle GetMiddleRectangle( 
        //	System.Drawing.Rectangle rect1 ,
        //	System.Drawing.Rectangle rect2 ,
        //	double rate )
        //{
        //	int left = rect1.Left + ( int ) ( ( rect2.Left - rect1.Left ) * rate ) ;
        //	int top = rect1.Top + ( int ) ( ( rect2.Top - rect1.Top ) * rate );
        //	int w = rect1.Width + ( int ) ( ( rect2.Width - rect1.Width ) * rate );
        //	int h = rect1.Height + ( int ) ( ( rect2.Height - rect1.Height ) * rate );
        //	return new System.Drawing.Rectangle( left , top , w , h );
        //}
        ///// <summary>
        ///// 修改矩形的宽度和高度
        ///// </summary>
        ///// <param name="rect">矩形区域</param>
        ///// <param name="dsize">大小改变量</param>
        ///// <returns>获得的新矩形</returns>
        //public static System.Drawing.Rectangle ReSize( 
        //          System.Drawing.Rectangle rect , 
        //          int dsize )
        //{
        //	return new System.Drawing.Rectangle(
        //              rect.Left ,
        //              rect.Top ,
        //              rect.Width + dsize ,
        //              rect.Height + dsize );
        //}
        ///// <summary>
        ///// 计算内置旋转矩形的大小
        ///// </summary>
        ///// <param name="Width">矩形的宽度</param>
        //      /// <param name="Height">矩形的高度</param>
        ///// <param name="angle">旋转的角度</param>
        ///// <returns>旋转后的外接矩形的大小</returns>
        //public static System.Drawing.Size InnerRoteteRectangleSize(
        //          int Width ,
        //          int Height ,
        //          double angle )
        //{
        //	if( Width <= 0 || Height <= 0 )
        //		return System.Drawing.Size.Empty ;
        //	double a = System.Math.Atan2( Height , Width );
        //	double r = Math.Sqrt( Width * Width + Height * Height ) /2 ;
        //	double w = r * Math.Cos( a - angle );
        //	double h = r * Math.Sin( a + angle );
        //	double rate = w * 2 / Width ;
        //          if (rate < h * 2 / Height)
        //          {
        //              rate = h * 2 / Height;
        //          }
        //	return new System.Drawing.Size(
        //              (int)(Width / rate ) ,
        //              ( int) ( Height / rate ));
        //}

        ///// <summary>
        ///// 逆时针旋转矩形
        ///// </summary>
        ///// <param name="o">原点</param>
        ///// <param name="rect">旋转的矩形</param>
        ///// <param name="angle">弧度</param>
        ///// <returns>旋转后的矩形的四个顶点的坐标</returns>
        //public static System.Drawing.PointF[] RotateRectanglePoints(
        //    System.Drawing.PointF o,
        //    System.Drawing.RectangleF rect,
        //    double angle)
        //{
        //    var ps = To4Points(rect);
        //    for (int iCount = 0; iCount < ps.Length; iCount++)
        //    {
        //        ps[iCount] = MathCommon.RotatePoint(o, ps[iCount], angle);
        //    }
        //    return ps;
        //}
         
        //      /// <summary>
        //      /// 逆时针旋转矩形
        //      /// </summary>
        //      /// <param name="o">原点</param>
        //      /// <param name="rect">旋转的矩形</param>
        //      /// <param name="angle">弧度</param>
        //      /// <returns>旋转后的矩形的四个顶点的最小外切矩形</returns>
        //      public static System.Drawing.Rectangle RotateRectangle(
        //          System.Drawing.Point o,
        //          System.Drawing.Rectangle rect,
        //          double angle)
        //      {
        //          System.Drawing.Point[] ps = To4Points(rect);
        //          for (int iCount = 0; iCount < ps.Length; iCount++)
        //          {
        //              ps[iCount] = MathCommon.RotatePoint(o, ps[iCount], angle);
        //          }
        //          return GetPointsBounds(ps);
        //      }


        ///// <summary>
        ///// 返回包含指定的点集合的最小外切矩形
        ///// </summary>
        ///// <param name="ps">点坐标数组</param>
        ///// <returns>包含所有点的方框对象,若没有数据则返回空方框对象</returns>
        //public static System.Drawing.Rectangle GetPointsBounds(System.Drawing.Point[] ps)
        //{
        //    if (ps != null && ps.Length > 1)
        //    {
        //        int XMax = ps[0].X;
        //        int XMin = ps[0].X;
        //        int YMax = ps[0].Y;
        //        int YMin = ps[0].Y;
        //        for (int iCount = 0; iCount < ps.Length; iCount++)
        //        {
        //            if (XMax < ps[iCount].X)
        //                XMax = ps[iCount].X;
        //            if (XMin > ps[iCount].X)
        //                XMin = ps[iCount].X;

        //            if (YMax < ps[iCount].Y)
        //                YMax = ps[iCount].Y;
        //            if (YMin > ps[iCount].Y)
        //                YMin = ps[iCount].Y;
        //        }
        //        return new System.Drawing.Rectangle(XMin, YMin, XMax - XMin, YMax - YMin);
        //    }
        //    return System.Drawing.Rectangle.Empty;
        //}//public static System.Drawing.Rectangle GetBounds( System.Drawing.Point[] ps)

//        /// <summary>
//        /// 返回包含指定的点集合的最小外切矩形
//        /// </summary>
//        /// <param name="ps">点坐标数组</param>
//        /// <returns>包含所有点的方框对象,若没有数据则返回空方框对象</returns>
//        public static System.Drawing.RectangleF GetPointsBounds(System.Drawing.PointF[] ps)
//        {
//            if (ps != null && ps.Length > 1)
//            {
//                float XMax = ps[0].X;
//                float XMin = ps[0].X;
//                float YMax = ps[0].Y;
//                float YMin = ps[0].Y;
//                for (int iCount = 0; iCount < ps.Length; iCount++)
//                {
//                    if (XMax < ps[iCount].X)
//                        XMax = ps[iCount].X;
//                    if (XMin > ps[iCount].X)
//                        XMin = ps[iCount].X;

//                    if (YMax < ps[iCount].Y)
//                        YMax = ps[iCount].Y;
//                    if (YMin > ps[iCount].Y)
//                        YMin = ps[iCount].Y;
//                }
//                return new System.Drawing.RectangleF(XMin, YMin, XMax - XMin, YMax - YMin);
//            }
//            return System.Drawing.RectangleF.Empty;
//        }//public static System.Drawing.Rectangle GetBounds( System.Drawing.Point[] ps)
//#if !DCWriterForWASM
        
       
//        /// <summary>
//        /// 返回表示矩形四个顶点坐标的点结构体数组
//        /// </summary>
//        /// <param name="rect">矩形区域</param>
//        /// <returns>包含4个元素的点结构体数组</returns>
//        public static System.Drawing.Point[] To4Points(System.Drawing.Rectangle rect)
//        {
//            System.Drawing.Point[] p = new System.Drawing.Point[4];
//            p[0] = new System.Drawing.Point(rect.X, rect.Y);
//            p[1] = new System.Drawing.Point(rect.Right, rect.Y);
//            p[2] = new System.Drawing.Point(rect.Right, rect.Bottom);
//            p[3] = new System.Drawing.Point(rect.Left, rect.Bottom);
//            return p;
//        }
//#endif
      

//        /// <summary>
//        /// 返回整数矩形的中心坐标
//        /// </summary>
//        /// <param name="rect">整数矩形对象</param>
//        /// <returns>矩形中心坐标</returns>
//        public static System.Drawing.PointF Center(System.Drawing.RectangleF rect)
//        {
//            return new System.Drawing.PointF(rect.Left + rect.Width / 2, rect.Top + rect.Height / 2);
//        }

        


//#if WINFORM || DCWriterForWinFormNET6  || DCWriterForWASM
//        /// <summary>
//        /// 根据两点坐标获得方框对象
//        /// </summary>
//        /// <param name="x1"></param>
//        /// <param name="y1"></param>
//        /// <param name="x2"></param>
//        /// <param name="y2"></param>
//        /// <returns></returns>
//        public static System.Drawing.Rectangle GetRectangle( int x1 , int y1 , int x2 , int y2)
//		{
//			System.Drawing.Rectangle myRect = System.Drawing.Rectangle.Empty ;
//			if( x1 < x2)
//			{
//				myRect.X 		= x1 ;
//				myRect.Width 	= x2 - x1 ;
//			}
//			else
//			{
//				myRect.X 		= x2;
//				myRect.Width	= x1 - x2 ;
//			}
//			if( y1 < y2)
//			{
//				myRect.Y 		= y1;
//				myRect.Height	= y2 - y1 ;
//			}
//			else
//			{
//				myRect.Y 		= y2;
//				myRect.Height	= y1 - y2;
//			}
//			if( myRect.Width < 1) myRect.Width = 1 ;
//			if( myRect.Height < 1) myRect.Height = 1 ;
//			return myRect ;
//		}

		
//		///// <summary>
//		///// 根据两点坐标获得方框对象
//		///// </summary>
//		///// <param name="p1">第一个点的坐标</param>
//		///// <param name="p2">第二个点的坐标</param>
//		///// <returns></returns>
//		//public static System.Drawing.Rectangle GetRectangle( System.Drawing.Point p1 , System.Drawing.Point p2)
//		//{
//		//	return GetRectangle( p1.X ,p1.Y , p2.X , p2.Y );
//		//}
//#endif
       
       
  //      /// <summary>
  //      /// 将点移动到矩形区域中
  //      /// </summary>
  //      /// <param name="p">点坐标</param>
  //      /// <param name="Bounds">矩形区域</param>
  //      /// <returns>修正后的点坐标</returns>
  //      public static System.Drawing.Point MoveInto(
		//	System.Drawing.Point p , 
		//	System.Drawing.Rectangle Bounds)
		//{
		//	if( !Bounds.IsEmpty )
		//	{
		//		if( p.X < Bounds.Left )
		//			p.X = Bounds.Left ;
		//		if( p.X >= Bounds.Right )
		//			p.X = Bounds.Right ;
		//		if( p.Y < Bounds.Top )
		//			p.Y = Bounds.Top ;
		//		if( p.Y >= Bounds.Bottom )
		//			p.Y = Bounds.Bottom ;
		//	}
		//	return p;
		//}

         
		

        public static RectangleF AlignRect(RectangleF mainRect, float width , float height , StringFormat format)
        {
            float x = mainRect.Left ;
            float y = mainRect.Top ;
            if (format != null)
            {
                return AlignRect(mainRect, width, height, format.Alignment, format.LineAlignment);
            }
            return new RectangleF(x, y, width, height);
        }
 
        ///// <summary>
        ///// 进行矩形对齐操作
        ///// </summary>
        ///// <param name="MainRect">主矩形</param>
        ///// <param name="width">要对齐的矩形的宽度</param>
        ///// <param name="height">要对齐的矩形的高度</param>
        ///// <param name="align">对齐方式</param>
        ///// <returns>对齐操作所得的矩形</returns>
        //public static System.Drawing.RectangleF AlignRect(
        //    System.Drawing.RectangleF MainRect,
        //    float width,
        //    float height,
        //    System.Drawing.ContentAlignment align)
        //{
        //    System.Drawing.RectangleF rect = new System.Drawing.RectangleF(0, 0, width, height);
        //    switch (align)
        //    {
        //        case System.Drawing.ContentAlignment.BottomCenter:
        //            rect.X = MainRect.Left + (MainRect.Width - rect.Width) / 2;
        //            rect.Y = MainRect.Bottom - rect.Height;
        //            break;
        //        case System.Drawing.ContentAlignment.BottomLeft:
        //            rect.X = MainRect.Left;
        //            rect.Y = MainRect.Bottom - rect.Height;
        //            break;
        //        case System.Drawing.ContentAlignment.BottomRight:
        //            rect.X = MainRect.Right - rect.Width;
        //            rect.Y = MainRect.Bottom - rect.Height;
        //            break;
        //        case System.Drawing.ContentAlignment.MiddleCenter:
        //            rect.X = MainRect.Left + (MainRect.Width - rect.Width) / 2;
        //            rect.Y = MainRect.Top + (MainRect.Height - rect.Height) / 2;
        //            break;
        //        case System.Drawing.ContentAlignment.MiddleLeft:
        //            rect.X = MainRect.Left;
        //            rect.Y = MainRect.Top + (MainRect.Height - rect.Height) / 2;
        //            break;
        //        case System.Drawing.ContentAlignment.MiddleRight:
        //            rect.X = MainRect.Right - rect.Width;
        //            rect.Y = MainRect.Top + (MainRect.Height - rect.Height) / 2;
        //            break;
        //        case System.Drawing.ContentAlignment.TopCenter:
        //            rect.X = MainRect.Left + (MainRect.Width - rect.Width) / 2;
        //            rect.Y = MainRect.Top;
        //            break;
        //        case System.Drawing.ContentAlignment.TopLeft:
        //            rect.X = MainRect.Left;
        //            rect.Y = MainRect.Top;
        //            break;
        //        case System.Drawing.ContentAlignment.TopRight:
        //            rect.X = MainRect.Right - rect.Width;
        //            rect.Y = MainRect.Top;
        //            break;
        //        default:
        //            rect.X = MainRect.Left;
        //            rect.Y = MainRect.Top;
        //            break;
        //    }
        //    return rect;
        //}
 

        public static RectangleF AlignRect(
            RectangleF mainRect, 
            float width,
            float height, 
            StringAlignment alignment,
            StringAlignment lineAlignment)
        {
            float x = mainRect.Left;
            float y = mainRect.Top;
            if (alignment == StringAlignment.Near)
            {
                x = mainRect.Left;
            }
            else if (alignment == StringAlignment.Center)
            {
                x = mainRect.Left + (mainRect.Width - width) / 2;
            }
            else if (alignment == StringAlignment.Far)
            {
                x = mainRect.Right - width;
            }
            if (lineAlignment == StringAlignment.Near)
            {
                y = mainRect.Top;
            }
            else if (lineAlignment == StringAlignment.Center)
            {
                y = mainRect.Top + (mainRect.Height - height) / 2;
            }
            else if (lineAlignment == StringAlignment.Far)
            {
                y = mainRect.Bottom - height;
            }

            return new RectangleF(x, y, width, height);
        }

		///// <summary>
		///// 进行矩形对齐操作
		///// </summary>
		///// <param name="MainRect">主矩形</param>
		///// <param name="width">宽度</param>
		///// <param name="height">高度</param>
		///// <param name="Align">水平对齐方式 1:左对齐 2:居中对齐 3:右对齐 其他:不进行对齐操作</param>
		///// <param name="VAlign">垂直对齐方式 1:左对齐 2:居中对齐 3:右对齐 其他:不进行对齐操作</param>
		///// <returns>对齐操作产生的矩形</returns>
		//public static System.Drawing.RectangleF AlignRect(
  //          System.Drawing.RectangleF MainRect ,
  //          float width ,
  //          float height , 
  //          int Align , 
  //          int VAlign)
		//{
		//	System.Drawing.RectangleF rect = new System.Drawing.RectangleF(
  //              0 ,
  //              0 ,
  //              width ,
  //              height ) ;
  //          if (Align == 1)
  //          {
  //              rect.X = MainRect.Left;
  //          }
  //          else if (Align == 2)
  //          {
  //              rect.X = MainRect.Left + (MainRect.Width - width) / 2;
  //          }
  //          else if (Align == 3)
  //          {
  //              rect.X = MainRect.Right - width;
  //          }
  //          if (VAlign == 1)
  //          {
  //              rect.Y = MainRect.Top;
  //          }
  //          else if (VAlign == 2)
  //          {
  //              rect.Y = MainRect.Top + (MainRect.Height - height) / 2;
  //          }
  //          else if (VAlign == 3)
  //          {
  //              rect.Y = MainRect.Bottom - height;
  //          }
		//	return rect ;
		//}//public static System.Drawing.Rectanglef AlignRect( System.Drawing.RectangleF MainRect , float width , float height , int Align , int VAlign)

//
//		/// <summary>
//		/// 将整数矩形转换为浮点数矩形
//		/// </summary>
//		/// <param name="rect">整数矩形对象</param>
//		/// <returns>转换所得的浮点数矩形</returns>
//		public static System.Drawing.RectangleF ToRectangleF( System.Drawing.Rectangle rect)
//		{
//			return new System.Drawing.RectangleF( rect.Left , rect.Top , rect.Width , rect.Height );
//		}
//
//		/// <summary>
//		/// 将浮点数矩形转换为整数矩形
//		/// </summary>
//		/// <param name="rect">浮点数矩形</param>
//		/// <returns>转换所得的整数矩形</returns>
//		public static System.Drawing.Rectangle ToRectangle( System.Drawing.RectangleF rect )
//		{
//			return new System.Drawing.Rectangle( (int) rect.Left , ( int) rect.Top , (int) rect.Width , ( int) rect.Height );
//		}
		///// <summary>
		///// 在绘图操作中判断指定的大小是否表示有效的大小
		///// </summary>
		///// <param name="width">宽度</param>
		///// <param name="height">高度</param>
		///// <returns>是否表示有效大小,若为有效大小则需要进行绘制操作,否则不需要进行绘制操作</returns>
		//public static bool PaintInvalidSize( float width , float height )
		//{
		//	if( width > 0 && height >= 0 )
		//		return true;
		//	if( height > 0 && width >= 0 )
		//		return true;
		//	return false;
		//}

        ///// <summary>
        ///// 根据宽度高度比获得居中的内置矩形
        ///// </summary>
        ///// <param name="rect">矩形</param>
        ///// <param name="widthHeightRate">宽度高度比</param>
        ///// <returns>获得的矩形</returns>
        //public static RectangleF GetInnerRectangle(RectangleF rect, float widthHeightRate)
        //{
        //    if (rect.Width == 0 || rect.Height == 0)
        //    {
        //        return RectangleF.Empty;
        //    }
        //    if (widthHeightRate <= 0)
        //    {
        //        return RectangleF.Empty;
        //    }
        //    if (widthHeightRate > rect.Width / rect.Height)
        //    {
        //        RectangleF result = new RectangleF(
        //            rect.Left  , 
        //            rect.Top , 
        //            rect.Width , 
        //            rect.Width / widthHeightRate );
        //        result.Y = rect.Top + (rect.Height - result.Height) / 2;
        //        return result;
        //    }
        //    else
        //    {
        //        RectangleF result = new RectangleF(
        //            rect.Left , 
        //            rect.Top  , 
        //            rect.Height * widthHeightRate , 
        //            rect.Height);
        //        result.X = rect.Left + (rect.Width - result.Width) / 2;
        //        return result;
        //    }
        //}

       
        /// <summary>
        /// 返回表示矩形四个顶点坐标的点结构体数组
        /// </summary>
        /// <param name="rect">矩形区域</param>
        /// <returns>包含4个元素的点结构体数组</returns>
        public static System.Drawing.PointF[] To4Points(System.Drawing.RectangleF rect)
        {
            System.Drawing.PointF[] p = new System.Drawing.PointF[4];
            p[0] = new System.Drawing.PointF(rect.X, rect.Y);
            p[1] = new System.Drawing.PointF(rect.Right, rect.Y);
            p[2] = new System.Drawing.PointF(rect.Right, rect.Bottom);
            p[3] = new System.Drawing.PointF(rect.Left, rect.Bottom);
            return p;
        }

      

  //      /// <summary>
  //      /// 获得正方形
  //      /// </summary>
  //      /// <param name="rect">矩形区域</param>
  //      /// <returns>正方形区域</returns>
  //      public static System.Drawing.RectangleF GetSquare( System.Drawing.RectangleF rect )
		//{
		//	if( rect.Width == rect.Height )
		//		return rect ;
		//	if( rect.Width > rect.Height )
		//		return new System.Drawing.RectangleF(
		//			rect.Left + ( rect.Width - rect.Height ) / 2 ,
		//			rect.Top , 
		//			rect.Height ,
		//			rect.Height );
		//	else
		//		return new System.Drawing.RectangleF(
		//			rect.Left , 
		//			rect.Top + ( rect.Height - rect.Width ) / 2 ,
		//			rect.Width ,
		//			rect.Width );
		//}
		 
		

		

		


  //      /// <summary>
  //      /// 将点移动到矩形区域中
  //      /// </summary>
  //      /// <param name="p">点坐标</param>
  //      /// <param name="Bounds">矩形区域</param>
  //      /// <returns>修正后的点坐标</returns>
  //      public static System.Drawing.PointF MoveInto(
  //          System.Drawing.PointF p ,
  //          System.Drawing.RectangleF Bounds)
		//{
		//	if( !Bounds.IsEmpty )
		//	{
		//		if( p.X < Bounds.Left )
		//			p.X = Bounds.Left ;
		//		if( p.X >= Bounds.Right )
		//			p.X = Bounds.Right ;
		//		if( p.Y < Bounds.Top )
		//			p.Y = Bounds.Top ;
		//		if( p.Y >= Bounds.Bottom )
		//			p.Y = Bounds.Bottom ;
		//	}
		//	return p;
		//}

  
		//private RectangleCommon(){}
	}//public sealed class RectangleCommon
     
}