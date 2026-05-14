using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;

namespace DCSoft.Drawing
{
	/// <summary>
	/// 几何以及数学运算的通用例程模块
	/// </summary>
    [System.Runtime.InteropServices.ComVisible(false)]
    public static class MathCommon
	{
//#if !DCWriterForWASM
//        public static void UnTransform(System.Drawing.Drawing2D.Matrix matrix, System.Drawing.PointF[] ps)
//        {
//            if (matrix == null)
//            {
//                return;
//            }
//            if (ps == null)
//            {
//                return;
//            }
//            float[] vs = matrix.Elements;
//            if (vs[0] == 1 && vs[1] == 0 && vs[2] == 0 && vs[3] == 1 && vs[4] == 0 && vs[5] == 0)
//            {
//                // 无需转换
//                return;
//            }
//            for (int iCount = 0; iCount < ps.Length; iCount++)
//            {
//                float x = ps[iCount].X;
//                float y = ps[iCount].Y;
//                x -= vs[4];
//                y -= vs[5];
//                x = x / vs[0];
//                y = y / vs[0];
//                ps[iCount].X = x;
//                ps[iCount].Y = y;
//            }
//        }
        

//        /// <summary>
//        /// 将数值修正为某个根数值的整数倍
//        /// </summary>
//        /// <param name="Value"></param>
//        /// <param name="baseValue"></param>
//        /// <returns></returns>
//        public static float FixToIntegerMultiple(float Value, float baseValue)
//        {
//            if (baseValue == 0)
//            {
//                return Value;
//            }
//            int v = (int)(Value / baseValue);
//            return v * baseValue;
//        }
//#endif
//        /// <summary>
//        /// 获得指定数值的所有的约数，返回一个整数数组，小的在前，大的在后。
//        /// </summary>
//        /// <param name="Value">数值</param>
//        /// <returns>约数组成的数组</returns>
//        public static int[] GetApproximateNumbers(int Value)
//        {
//            List<int> result = new List<int>();
//            //result.Add(1);
//            for (int iCount = 1; iCount <= Value; iCount++)
//            {
//                if (( Value % iCount) == 0)
//                {
//                    result.Add(iCount);
//                }
//            }
//            //if (Value > 1)
//            //{
//            //    result.Add(Value);
//            //}
//            return result.ToArray();
//        }

//        /// <summary>
//        /// 修正元素的大小，使得能完全的放置在一个容器中而不被剪切掉
//        /// </summary>
//        /// <param name="containerSize">容器的大小</param>
//        /// <param name="elementSize">元素的原始大小</param>
//        /// <param name="keepWidthHeightRate">是否保持元素的宽度高度比率</param>
//        /// <returns>修正后的元素大小</returns>
//        public static SizeF FixSize(SizeF containerSize, SizeF elementSize, bool keepWidthHeightRate)
//        {
//            if (elementSize.Width <= 0 || elementSize.Height <= 0)
//            {
//                // 元素宽度或者高度出现0值
//                if (elementSize.Width <= 0)
//                {
//                    elementSize.Width = Math.Min(containerSize.Width, elementSize.Width);
//                }
//                if (elementSize.Height <= 0)
//                {
//                    elementSize.Height = Math.Min(containerSize.Height, elementSize.Height);
//                }
//                return elementSize;
//            }
//            if (elementSize.Width > containerSize.Width
//                || elementSize.Height > containerSize.Height)
//            {
//                // 元素的宽度或者高度大于容器则需要进行修正
//                if (keepWidthHeightRate)
//                {
//                    // 计算缩小比例
//                    double zoomRate = Math.Min(
//                        containerSize.Width / elementSize.Width,
//                        containerSize.Height / elementSize.Height);
//                    SizeF result = new SizeF(
//                        (float)(elementSize.Width * zoomRate),
//                        (float)(elementSize.Height * zoomRate));
//                    return result;
//                }
//                else
//                {
//                    SizeF result = new SizeF(
//                        Math.Min(elementSize.Width, containerSize.Width),
//                        Math.Min(elementSize.Height, containerSize.Height));
//                    return result;
//                }
//            }
//            else
//            {
//                // 无需修正，返回原值
//                return elementSize;
//            }
//        }

       
       
		
		///// <summary>
		///// 对若干条线段进行矩形区域剪切处理
		///// </summary>
		///// <remarks>本函数修改点数组,使之包含在矩形区域中,若线段不在矩形区域中,
		///// 则设置起点和终点坐标为( int.MinValue , int.MinValue )</remarks>
		///// <param name="ClipRectangle">剪切矩形</param>
		///// <param name="LinesPoints">线段起点和终点的坐标</param>
		//public static void RectangleClipLines( 
		//	System.Drawing.Rectangle ClipRectangle ,
		//	System.Drawing.Point[] LinesPoints )
		//{
		//	if( ClipRectangle.IsEmpty )
		//		throw new System.ArgumentException("ClipRectangle is Empty" , "ClipRectangle");
		//	if( LinesPoints == null )
		//		throw new System.ArgumentNullException("LinesPoints");
		//	if( LinesPoints.Length == 0 )
		//		throw new System.ArgumentException("LinesPoints is empty" , "LinesPoints");
		//	// 点数组必须是二的倍数
		//	if( ( LinesPoints.Length % 2 ) != 0 )
		//		throw new System.ArgumentException("LinesPoints is error" , "LinesPoints");

		//	System.Drawing.Point BlankPoint = new System.Drawing.Point( int.MinValue , int.MinValue );

		//	int left = ClipRectangle.Left ;
		//	int top = ClipRectangle.Top ;
		//	int right = ClipRectangle.Right ;
		//	int bottom = ClipRectangle.Bottom ;

		//	for( int iCount = 0 ; iCount < LinesPoints.Length ; iCount += 2 )
		//	{
		//		System.Drawing.Point p1 = LinesPoints[ iCount ] ;
		//		System.Drawing.Point p2 = LinesPoints[ iCount + 1 ] ;

		//		bool c1 = ClipRectangle.Contains( p1 );

		//		// 若两点重合
		//		if( p1.Equals( p2 ))
		//		{
		//			if( c1 == false )
		//			{
		//				LinesPoints[ iCount ] = BlankPoint ;
		//				LinesPoints[ iCount + 1] = BlankPoint ;
		//			}
		//			continue ;
		//		}
		//		bool c2 = ClipRectangle.Contains( p2 );
		//		// 两个端点都在矩形内部则不需要处理
		//		if( c1 && c2 )
		//			continue ;

		//		if( p1.X == p2.X )
		//		{
		//			// 垂直线
		//			if( p1.X >= left && p1.X <= right )
		//			{
		//				LinesPoints[ iCount ].Y = FixToRange( p1.Y , top , bottom ) ;
		//				LinesPoints[ iCount + 1].Y = FixToRange( p2.Y , top , bottom ) ;
		//			}
		//			else
		//			{
		//				LinesPoints[ iCount ] = BlankPoint ;
		//				LinesPoints[ iCount + 1] = BlankPoint ;
		//			}
		//		}
		//		else if( p1.Y == p2.Y )
		//		{
		//			// 水平线
		//			if( p1.Y >= top && p1.Y <= bottom )
		//			{
		//				LinesPoints[ iCount ].X = FixToRange( p1.X , left , right ) ;
		//				LinesPoints[ iCount + 1].X = FixToRange( p2.X , left ,  right );
		//			}
		//			else
		//			{
		//				LinesPoints[ iCount ] = BlankPoint ;
		//				LinesPoints[ iCount + 1 ] = BlankPoint ;
		//			}
		//		}
		//		else
		//		{
		//			// 斜线
		//			double[] ps = GetLineEquationParameter( p1.X , p1.Y , p2.X , p2.Y );
		//			//int index = 0 ;
		//			double a = ps[0] ;
		//			double b = ps[1] ;

		//			if( p1.X < left )
		//			{
		//				p1.X = left ;
		//				p1.Y = ( int ) ( a * p1.X + b );
		//			}
		//			else if( p1.X > right )
		//			{
		//				p1.X = right ;
		//				p1.Y = ( int ) ( a * p1.X + b );
		//			}
		//			if( p1.Y < top )
		//			{
		//				p1.Y = top ;
		//				p1.X = ( int ) ( ( p1.Y - b ) / a );
		//			}
		//			else if( p1.Y > bottom )
		//			{
		//				p1.Y = bottom ;
		//				p1.X = ( int ) ( ( p1.Y - b ) / a );
		//			}

		//			if( p2.X < left )
		//			{
		//				p2.X = left ;
		//				p2.Y = ( int ) ( a * p2.X + b );
		//			}
		//			else if( p2.X > right )
		//			{
		//				p2.X = right ;
		//				p2.Y = ( int ) ( a * p2.X + b );
		//			}
		//			if( p2.Y < top )
		//			{
		//				p2.Y = top ;
		//				p2.X = ( int ) ( ( p2.Y - b ) / a );
		//			}
		//			else if( p2.Y > bottom )
		//			{
		//				p2.Y = bottom ;
		//				p2.X = ( int ) ( ( p2.Y - b ) / a );
		//			}

		//			bool flag = false;
		//			if( p1.X >= left && p1.X <= right )
		//			{
		//				if( p1.Y >= top && p1.Y <= bottom )
		//				{
		//					if( p2.X >= left && p2.X <= right )
		//					{
		//						if( p2.Y >= top && p2.Y <= bottom )
		//						{
		//							flag = true;
		//						}
		//					}		
		//				}
		//			}
		//			if( flag )
		//			{
		//				LinesPoints[ iCount ] = p1 ;
		//				LinesPoints[ iCount + 1 ] = p2 ;
		//			}
		//			else
		//			{
		//				LinesPoints[ iCount ] = BlankPoint ;
		//				LinesPoints[ iCount + 1 ] = BlankPoint ;
		//			}
		//		}
		//	}//for( int iCount = 0 ; iCount < LinesPoints.Length ; iCount += 2 )
		//}

        

		//public static int FixToRange( int vValue , int min , int max )
		//{
  //          if (vValue < min)
  //          {
  //              return min;
  //          }
  //          if (vValue > max)
  //          {
  //              return max;
  //          }
		//	return vValue ;
		//}

		

		/// <summary>
		/// 计算一个点到一个线段或直线的距离,该距离大于等于0,若是计算点到线段的距离且点在线段所在直线的投影点不在该线段则返回-1
		/// </summary>
		/// <param name="x1">线段起点X坐标</param>
		/// <param name="y1">线段起点Y坐标</param>
		/// <param name="x2">线段终点X坐标</param>
		/// <param name="y2">线段终点X坐标</param>
		/// <param name="x">点的X坐标</param>
		/// <param name="y">点的Y坐标</param>
		/// <param name="ShortLine">true:计算点到线段的距离 false:计算点到直线的距离</param>
		/// <returns></returns>
		public static double DistanceToLine(
			double x1 , 
			double y1 , 
			double x2 ,
			double y2 , 
			double x , 
			double y , 
			bool ShortLine)
		{
			// 线段起点和终点重合则参数不正确
            if (x1 == x2 && y1 == y2)
            {
                return -1;
            }
			// 将线段两个端点和指定点组成三角形,计算其边长
			double a = System.Math.Sqrt( ( x-x1) * ( x- x1) + (y-y1) * ( y-y1));
			double b = System.Math.Sqrt( ( x-x2) * ( x- x2) + (y-y2) * ( y-y2));
			//double c = System.Math.Sqrt( ( x1-x2) * ( x1- x2) + (y1-y2) * ( y1-y2));
			// 获得点在线段上的投影点坐标
			double xd = x1 + ( x2 - x1) * a / ( a + b );
			double yd = y1 + ( y2 - y1) * a / ( a + b );

			// 若计算点到线段的距离且投影点在线段外边则返回-1
			if( ShortLine )
			{
				if( x1 != x2 )
				{
					if( ( xd-x1) * (xd-x2)>=0)
						return -1 ;
				}
				else
				{
					if( ( yd-y1) * (yd-y2) >= 0)
						return -1;
				}
			}

			// 计算点和投影点的距离
			double ds = System.Math.Sqrt( (x - xd) * ( x-xd) + ( y-yd)*(y-yd));
			// 指定点和投影点间的距离就是点和线段间的距离
			return ds ;
		}//public static double DistanceToLine( double x1 , double y1 , double x2 , double y2 , double x , double y , bool ShortLine)

		
		
////		/// <summary>
////		/// 设置标志位
////		/// </summary>
////		/// <param name="intAttributes">原始的标志数据</param>
////		/// <param name="intValue">要设置的标志位的数据</param>
////		/// <param name="bolSet">是否设置或者清除</param>
////		/// <returns>修改后的标志数据</returns>
////		public static int SetIntAttribute(int intAttributes , int intValue , bool bolSet)
////		{
////			return bolSet ? intAttributes | intValue : intAttributes & ~ intValue ;
////		}

      
////#if !DCWriterForWASM
////		/// <summary>
////		/// 进行逆时针旋转指定弧度的角度处理
////		/// </summary>
////		/// <param name="o">原点</param>
////		/// <param name="p">处理的点</param>
////		/// <param name="angle">旋转的角度(弧度)</param>
////		/// <returns>处理后的点</returns>
////		public static System.Drawing.Point RotatePoint(
////			System.Drawing.Point o,
////			System.Drawing.Point p,
////			double angle)
////		{
////			if (o.X == p.X && o.Y == p.Y)
////				return p;
////			double l = (p.X - o.X) * (p.X - o.X) + (p.Y - o.Y) * (p.Y - o.Y);
////			l = System.Math.Sqrt(l);
////			double alf = System.Math.Atan2(p.Y - o.Y, p.X - o.X);
////			alf = alf - angle;
////			System.Drawing.Point p2 = System.Drawing.Point.Empty;
////			p2.X = (int)(o.X + l * System.Math.Cos(alf));
////			p2.Y = (int)(o.Y + l * System.Math.Sin(alf));
////			return p2;
////		}
////#endif
//        /// <summary>
//        /// 进行逆时针旋转指定弧度的角度处理
//        /// </summary>
//        /// <param name="o">原点</param>
//        /// <param name="p">处理的点</param>
//        /// <param name="angle">旋转的角度(弧度)</param>
//        /// <returns>处理后的点</returns>
//        public static System.Drawing.PointF RotatePoint(
//			System.Drawing.PointF o,
//			System.Drawing.PointF p,
//			double angle)
//		{
//			if (o.X == p.X && o.Y == p.Y)
//				return p;
//			double l = (p.X - o.X) * (p.X - o.X) + (p.Y - o.Y) * (p.Y - o.Y);
//			l = System.Math.Sqrt(l);
//			double alf = System.Math.Atan2(p.Y - o.Y, p.X - o.X);
//			alf = alf - angle;
//			System.Drawing.PointF p2 = System.Drawing.PointF.Empty;
//			p2.X = (float)(o.X + l * System.Math.Cos(alf));
//			p2.Y = (float)(o.Y + l * System.Math.Sin(alf));
//			return p2;
//		}


		

//		/// <summary>
//		/// 获得直线的方程参数
//		/// </summary>
//		/// <param name="x1">线段的起点坐标</param>
//		/// <param name="y1">线段的起点坐标</param>
//		/// <param name="x2">线段的终点坐标</param>
//		/// <param name="y2">线段的终点坐标</param>
//		/// <returns>方程参数组成的数组</returns>
//		/// <remarks>
//		/// 一个直线的方程为 y = a * x + b ,本函数就是根据两个点的坐标计算出
//		/// 直线方程的参数 a 和 b , 并将其放置到一个数组中.若直线垂直,则直线
//		/// 方程就变成了 x = b ,此时函数返回一个空引用.
//		/// </remarks>
//		public static double[] GetLineEquationParameter( double x1 , double y1 , double x2 , double y2 )
//		{
//			if(DoubleNaN.IsNaN( x1 ))
//				throw new System.ArgumentException("x1 is Nan");
//			if(DoubleNaN.IsNaN( y1 ))
//				throw new System.ArgumentException("y1 is Nan");
//			if(DoubleNaN.IsNaN( x2 ))
//				throw new System.ArgumentException("x2 is Nan");
//			if(DoubleNaN.IsNaN( y2 ))
//				throw new System.ArgumentException("y2 is Nan");

//			if( x1 != x2 )
//			{
//				double a = ( y2 - y1 ) / ( x2 - x1 );
//				double b = y1 - a * x1 ;
//				return new double[]{ a , b };
//			}
//			else
//				return null;
//		}

	
	}//public class MathCommon
}