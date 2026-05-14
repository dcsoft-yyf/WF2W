using System;
using System.Drawing ;
using System.Drawing.Drawing2D ;

namespace DCSoft.Drawing
{
	/// <summary>
	/// 绘图单位转换
	/// </summary>
    
    [System.Runtime.InteropServices.ComVisible(false)]
#if !DCWriterForWASM
    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true )]
#endif
    public static class GraphicsUnitConvert
	{
        /// <summary>
        /// 初始化对象
        /// </summary>
        static GraphicsUnitConvert()
        {
            //using (Graphics g = DrawerUtil.SafeCreateGraphics())
            //{
            //    fDpi = g.DpiX;
            //}
        }
        public static void VoidMethod()
        {

        }

        private static float _Dpi = 96 ;
        public static void SetDPI( float v )
        {
            _Dpi = v;
        }

        public static void RefreshDPI()
        {
            
        }
        
        /// <summary>
        /// 进行单位换算
        /// </summary>
        /// <param name="vValue">长度值</param>
        /// <param name="OldUnit">旧单位</param>
        /// <param name="NewUnit">新单位</param>
        /// <returns>换算结果</returns>
        public static double Convert(
			double vValue ,
			System.Drawing.GraphicsUnit OldUnit ,
			System.Drawing.GraphicsUnit  NewUnit )
		{
			if( OldUnit == NewUnit )
				return vValue ;
			else
				return vValue * GetRate( NewUnit , OldUnit );
		}

       

        /// <summary>
        /// 进行单位换算
        /// </summary>
        /// <param name="vValue">长度值</param>
        /// <param name="OldUnit">旧单位</param>
        /// <param name="NewUnit">新单位</param>
        /// <returns>换算结果</returns>
        public static float Convert(
			float vValue ,
			System.Drawing.GraphicsUnit OldUnit ,
			System.Drawing.GraphicsUnit  NewUnit )
		{
            if (OldUnit == NewUnit)
            {
                return vValue;
            }
            else
            {
                return (float)(vValue * GetRate(NewUnit, OldUnit));
            }
		}

        /// <summary>
        /// 将长度转换为厘米
        /// </summary>
        /// <param name="vValue">长度值</param>
        /// <param name="oldUnit">长度单位</param>
        /// <returns>转换的厘米值</returns>
        public static float ConvertToCM(float vValue, GraphicsUnit oldUnit)
        {
            return (float)(Convert(vValue, oldUnit, GraphicsUnit.Millimeter) / 10.0);
        }

        /// <summary>
        /// 将厘米长度值转换为指定单位的长度
        /// </summary>
        /// <param name="cmValue">厘米长度值</param>
        /// <param name="unit">新的长度单位</param>
        /// <returns>转换结果</returns>
        public static float ConvertFromCM(float cmValue, GraphicsUnit unit)
        {
            return Convert( cmValue * 10.0f , GraphicsUnit.Millimeter , unit );
        }

        /// <summary>
        /// 进行单位换算
        /// </summary>
        /// <param name="vValue">长度值</param>
        /// <param name="OldUnit">旧单位</param>
        /// <param name="NewUnit">新单位</param>
        /// <returns>换算结果</returns>
        public static int Convert(
			int vValue ,
			System.Drawing.GraphicsUnit OldUnit ,
			System.Drawing.GraphicsUnit  NewUnit )
		{
			if( OldUnit == NewUnit )
				return vValue ;
			else
				return ( int ) ( vValue * GetRate( NewUnit , OldUnit ) );
		}

        /// <summary>
        /// 进行单位换算
        /// </summary>
        /// <param name="p">长度值</param>
        /// <param name="OldUnit">旧单位</param>
        /// <param name="NewUnit">新单位</param>
        /// <returns>换算结果</returns>
        public static System.Drawing.Point Convert(
			System.Drawing.Point p ,
			System.Drawing.GraphicsUnit OldUnit , 
			System.Drawing.GraphicsUnit  NewUnit )
		{
			if( OldUnit == NewUnit )
				return p ;
			else
			{
				double rate = GetRate( NewUnit , OldUnit );
				return new System.Drawing.Point( 
					( int ) ( p.X * rate ) ,
					( int ) ( p.Y * rate ));
			}
		}

        /// <summary>
        /// 进行单位换算
        /// </summary>
        /// <param name="p">长度值</param>
        /// <param name="OldUnit">旧单位</param>
        /// <param name="NewUnit">新单位</param>
        /// <returns>换算结果</returns>
        public static System.Drawing.PointF Convert(
            System.Drawing.PointF p,
            System.Drawing.GraphicsUnit OldUnit,
            System.Drawing.GraphicsUnit NewUnit)
        {
            if (OldUnit == NewUnit)
            {
                return p;
            }
            else
            {
                double rate = GetRate(NewUnit, OldUnit);
                return new System.Drawing.PointF(
                    (float)(p.X * rate),
                    (float)(p.Y * rate));
            }
        }

  


        /// <summary>
        /// 进行单位换算
        /// </summary>
        /// <param name="size">旧值</param>
        /// <param name="OldUnit">旧单位</param>
        /// <param name="NewUnit">新单位</param>
        /// <returns>换算结果</returns>
        public static System.Drawing.Size Convert(
			System.Drawing.Size size ,
			System.Drawing.GraphicsUnit OldUnit ,
			System.Drawing.GraphicsUnit NewUnit )
		{
			if( OldUnit == NewUnit )
				return size ;
			else
			{
				double rate = GetRate( NewUnit , OldUnit );
				return new System.Drawing.Size(
					( int ) ( size.Width * rate ) , 
					( int ) ( size.Height * rate ));
			}
		}

        /// <summary>
        /// 进行单位换算
        /// </summary>
        /// <param name="size">旧值</param>
        /// <param name="OldUnit">旧单位</param>
        /// <param name="NewUnit">新单位</param>
        /// <returns>换算结果</returns>
        public static System.Drawing.Size ConvertSpecifyDPI(
            System.Drawing.Size size,
            System.Drawing.GraphicsUnit OldUnit,
            System.Drawing.GraphicsUnit NewUnit,
            float dpi )
        {
            if (OldUnit == NewUnit)
            {
                return size;
            }
            else
            {
                double rate = GetRateSpecifyDPI(NewUnit, OldUnit, dpi);
                return new System.Drawing.Size(
                    (int)(size.Width * rate),
                    (int)(size.Height * rate));
            }
        }

        /// <summary>
        /// 进行单位换算
        /// </summary>
        /// <param name="size">旧值</param>
        /// <param name="OldUnit">旧单位</param>
        /// <param name="NewUnit">新单位</param>
        /// <returns>换算结果</returns>
        public static System.Drawing.SizeF Convert(
			System.Drawing.SizeF size , 
			System.Drawing.GraphicsUnit OldUnit ,
			System.Drawing.GraphicsUnit NewUnit )
		{
            if (OldUnit == NewUnit)
            {
                return size;
            }
            else
            {
                double rate = GetRate(NewUnit, OldUnit);
                return new System.Drawing.SizeF(
                    (float)(size.Width * rate),
                    (float)(size.Height * rate));
            }
		}
       

#if ! DCWriterForWASM
        /// <summary>
        /// 进行单位换算
        /// </summary>
        /// <param name="rect">旧值</param>
        /// <param name="OldUnit">旧单位</param>
        /// <param name="NewUnit">新单位</param>
        /// <returns>换算结果</returns>
        public static System.Drawing.Rectangle Convert(
			System.Drawing.Rectangle rect , 
			System.Drawing.GraphicsUnit OldUnit ,
			System.Drawing.GraphicsUnit  NewUnit )
		{
			if( OldUnit == NewUnit )
			{
				return rect ;
			}
			else
			{
				double rate = GetRate( NewUnit , OldUnit );
				return new System.Drawing.Rectangle(
					( int ) ( rect.X * rate ) ,
					( int ) ( rect.Y * rate ) ,
					( int ) ( rect.Width * rate ) ,
					( int ) ( rect.Height * rate ));
			}
		}

        /// <summary>
        /// 进行单位换算
        /// </summary>
        /// <param name="rect">旧值</param>
        /// <param name="OldUnit">旧单位</param>
        /// <param name="NewUnit">新单位</param>
        /// <returns>换算结果</returns>
        public static System.Drawing.RectangleF Convert(
			System.Drawing.RectangleF rect , 
			System.Drawing.GraphicsUnit OldUnit ,
			System.Drawing.GraphicsUnit  NewUnit )
		{
			if( OldUnit == NewUnit )
			{
				return rect ;
			}
			else
			{
				double rate = GetRate( NewUnit , OldUnit );
				return new System.Drawing.RectangleF(
					( float ) ( rect.X * rate ) ,
					( float ) ( rect.Y * rate ) ,
					( float ) ( rect.Width * rate ) ,
					( float ) ( rect.Height * rate ));
			}
		}
#endif
        /// <summary>
        /// 将一个长度从旧单位换算成新单位使用的比率
        /// </summary>
        /// <param name="NewUnit">新单位</param>
        /// <param name="OldUnit">旧单位</param>
        /// <returns>比率数</returns>
        public static double GetRate(
			System.Drawing.GraphicsUnit NewUnit ,
			System.Drawing.GraphicsUnit OldUnit )
		{
            if(NewUnit == OldUnit )
            {
                return 1;
            }
			return GetUnit( OldUnit ) / GetUnit( NewUnit )  ;
		}

        /// <summary>
		/// 将一个长度从旧单位换算成新单位使用的比率
		/// </summary>
		/// <param name="NewUnit">新单位</param>
		/// <param name="OldUnit">旧单位</param>
		/// <returns>比率数</returns>
		public static double GetRateSpecifyDPI(
            System.Drawing.GraphicsUnit NewUnit,
            System.Drawing.GraphicsUnit OldUnit,
            float dpi)
        {
            return GetUnitSpecifyDPI(OldUnit, dpi) / GetUnitSpecifyDPI(NewUnit, dpi);
        }

       


        /// <summary>
        /// 将像素单位转换为打印单位
        /// </summary>
        /// <param name="v">像素单位</param>
        /// <returns>转换后的打印单位</returns>
        public static double PixelToPrintUnit(double v)
        {
            return Convert(v, GraphicsUnit.Pixel, GraphicsUnit.Inch) * 100;
        }

      
        /// <summary>
        /// 将像素单位转换为单位
        /// </summary>
        /// <param name="v">像素单位</param>
        /// <param name="unit">新的单位</param>
        /// <returns>转换后的单位</returns>
        public static double PixelToUnit(double v, GraphicsUnit unit)
        {
            return Convert(v, GraphicsUnit.Pixel, unit);
        }

  

		/// <summary>
		/// 获得一个单位占据的英寸数
		/// </summary>
		/// <param name="unit">单位类型</param>
		/// <returns>英寸数</returns>
		public static double GetUnit( System.Drawing.GraphicsUnit unit )
		{
			switch( unit )
			{
				case System.Drawing.GraphicsUnit.Display :
					return 1 / _Dpi ;
				case System.Drawing.GraphicsUnit.Document :
					return 1 / 300.0 ;
				case System.Drawing.GraphicsUnit.Inch :
					return 1 ;
				case System.Drawing.GraphicsUnit.Millimeter :
					return 1 / 25.4 ;
				case System.Drawing.GraphicsUnit.Pixel :
					return 1 / _Dpi ;
				case System.Drawing.GraphicsUnit.Point :
					return 1 / 72.0 ;
				default:
					throw new System.NotSupportedException("Not support " + unit.ToString());
			}
		}

        /// <summary>
		/// 获得一个单位占据的英寸数
		/// </summary>
		/// <param name="unit">单位类型</param>
		/// <returns>英寸数</returns>
		public static double GetUnitSpecifyDPI(System.Drawing.GraphicsUnit unit , float dpi )
        {
            switch (unit)
            {
                case System.Drawing.GraphicsUnit.Display:
                    return 1 / dpi;
                case System.Drawing.GraphicsUnit.Document:
                    return 1 / 300.0;
                case System.Drawing.GraphicsUnit.Inch:
                    return 1;
                case System.Drawing.GraphicsUnit.Millimeter:
                    return 1 / 25.4;
                case System.Drawing.GraphicsUnit.Pixel:
                    return 1 / dpi;
                case System.Drawing.GraphicsUnit.Point:
                    return 1 / 72.0;
                default:
                    throw new System.NotSupportedException("Not support " + unit.ToString());
            }
        }


       
#if !DCWriterForWASM
        /// <summary>
        /// 进行单位换算
        /// </summary>
        /// <param name="Value">旧值</param>
        /// <param name="OldUnit">旧单位</param>
        /// <param name="NewUnit">新单位</param>
        /// <returns>换算结果</returns>
        public static double Convert(double Value, LengthUnit OldUnit, LengthUnit NewUnit)
        {
            if (OldUnit == NewUnit)
                return Value;
            else
                return Value * GetUnit(OldUnit) / GetUnit( NewUnit );
        }
        /// <summary>
        /// 获得一个单位占据的英寸数
        /// </summary>
        /// <param name="unit">单位类型</param>
        /// <returns>英寸数</returns>
        public static double GetUnit( LengthUnit unit)
        {
            switch (unit)
            {
                case LengthUnit.Document :
                    return 1 / 300.0;
                case LengthUnit.Inch :
                    return 1;
                case LengthUnit.Millimeter :
                    return 1 / 25.4;
                case LengthUnit.Pixel :
                    return 1 / _Dpi;
                case LengthUnit.Point :
                    return 1 / 72.0;
                case LengthUnit.Centimerter :
                    return 1 / 2.54;
                case LengthUnit.Twips :
                    return 1 / 1440.0;
                default:
                    throw new System.NotSupportedException("Not support " + unit.ToString());
            }
        }


        public static double ToPixel(double Value, GraphicsUnit unit , float dpi)
        {
            if (dpi <= 0)
            {
                throw new ArgumentOutOfRangeException("dpi=" + dpi);
            }
            double v = GetUnit(unit);
            double result = Value * v * dpi;
            return result;
        }

        public static double FromPixel(double pixelValue, GraphicsUnit unit, float dpi)
        {
            if (dpi <= 0)
            {
                throw new ArgumentOutOfRangeException("dpi=" + dpi);
            }
            double v = GetUnit(unit);
            double result = pixelValue / (dpi * v );
             
            return result;
        }

#endif


  
        /// <summary>
        /// 将指定单位的指定长度转化为 Twips 单位
        /// </summary>
        /// <param name="Value">长度</param>
        /// <param name="unit">度量单位</param>
        /// <returns>转化的 Twips 数</returns>
        public static int ToTwips(float Value, System.Drawing.GraphicsUnit unit)
        {
            double v = GetUnit(unit);
            return (int)(Value * v * 1440);
        }

		/// <summary>
		/// 将指定的Twips值转化为指定单位的数值
		/// </summary>
		/// <param name="Twips">Twips值</param>
		/// <param name="unit">要转化的目标单位</param>
		/// <returns>转化的长度值</returns>
		public static int FromTwips( int Twips , System.Drawing.GraphicsUnit unit )
		{
			double v = GetUnit( unit );
			return ( int ) ( Twips / ( v * 1440 ));
		}

        /// <summary>
        /// 将指定的Twips值转化为指定单位的数值
        /// </summary>
        /// <param name="twips">Twips值</param>
        /// <param name="unit">要转化的目标单位</param>
        /// <returns>转化的长度值</returns>
        public static double FromTwips(double twips, System.Drawing.GraphicsUnit unit)
        {
            double v = GetUnit(unit);
            return twips / (v * 1440.0);
        }

        private static readonly string _cm = "cm";
        private static readonly string _mm = "mm";
        private static readonly string _in = "in";
        private static readonly string _pt = "pt";
        private static readonly string _pc = "pc";
        private static readonly string _px = "px";


        /// <summary>
        /// 将CSS样式的长度字符串转换为数值,支持的单位有px,pt,em,in,pc,cm,mm。
        /// </summary>
        /// <param name="CSSLength">CSS样式的长度字符串</param>
        /// <param name="unit">要转换为单位</param>
        /// <param name="DefaultValue">默认值</param>
        /// <returns>转换后的数值</returns>
        public static double ParseCSSLength(string CSSLength, GraphicsUnit unit, double DefaultValue)
        {
            if( CSSLength[0] == ' ' || CSSLength[0] == '\t')
            {
                CSSLength = CSSLength.Trim();
            }
            int len = CSSLength.Length;
            double num = 0;
            bool nagative = false;
            long divRate = 0;
            GraphicsUnit OldUnit = unit;
            int len2 = len - 1;
            for (int iCount = 0 ; iCount < len; iCount++)
            {
                char c = CSSLength[iCount];
                if (c >= '0' && c <= '9')
                {
                    num = num * 10 + c - '0';
                    if (divRate > 0)
                    {
                        // 小数点比率
                        divRate = divRate * 10;
                    }
                    if( iCount == len2 )
                    {
                        // 纯数字字符串
                        if( divRate > 0 )
                        {
                            num = num / divRate;
                        }
                        if( nagative )
                        {
                            num = -num;
                        }
                        return Convert(num, GraphicsUnit.Pixel, unit);
                    }
                }
                else if (c == '-')
                {
                    // 负号
                    nagative = true;
                }
                else if (c == '.')
                {
                    divRate = 1;
                }
                else if (c >= 'a' && c <= 'z' || c >= 'A' && c <= 'Z')
                {
                    if (iCount < len - 1)
                    {
                        if (c <= 'Z')
                        {
                            c = (char)(c - 'A' + 'a');
                        }
                        var nc = CSSLength[iCount + 1];
                        if (nc <= 'Z')
                        {
                            nc = (char)(nc - 'A' + 'a');
                        }
                        if (divRate > 0)
                        {
                            num = num / divRate;
                        }
                        if (nagative)
                        {
                            num = -num;
                        }
                        if (c == 'p' && nc == 'x')
                        {
                            return Convert(num, GraphicsUnit.Pixel, unit);
                        }
                        else if (c == 'p' && nc == 't')
                        {
                            return Convert(num, GraphicsUnit.Point, unit);
                        }
                        else if (c == 'e' && nc == 'm')
                        {
                            return Convert(num, GraphicsUnit.Pixel, unit) * 16;
                        }
                        else if (c == 'c' && nc == 'm')
                        {
                            return Convert(num, GraphicsUnit.Millimeter, unit) * 10;
                        }
                        else if (c == 'm' && nc == 'm')
                        {
                            return Convert(num, GraphicsUnit.Millimeter, unit);
                        }
                        else if (c == 'i' && nc == 'n')
                        {
                            return Convert(num, GraphicsUnit.Inch, unit);
                        }
                        else if (c == 'p' && nc == 'c')
                        {
                            return Convert(num, GraphicsUnit.Point, unit) * 12;
                        }
                        //默认为像素单位
                        return Convert(num, GraphicsUnit.Pixel, unit);
                    }
                }
            }
            if (double.TryParse(CSSLength, System.Globalization.NumberStyles.Any, null, out num))
            {
                return Convert(num, GraphicsUnit.Pixel, unit);
            }
            return DefaultValue;
        }

        /// <summary>
        /// 将长度转换为CSS中的长度字符串
        /// </summary>
        /// <param name="Value">长度</param>
        /// <param name="unit">长度单位</param>
        /// <param name="cssUnit">CSS单位</param>
        /// <returns>CSS样式的长度字符串</returns>
        public static string ToCSSLength(double Value, GraphicsUnit unit , CssLengthUnit cssUnit )
        {
            double v = 0 ;
            string strUnit = string.Empty;
            switch (cssUnit)
            {
                case CssLengthUnit.Centimeters :
                    v = Convert(Value, unit, GraphicsUnit.Millimeter) / 10;
                    strUnit = _cm;
                    break;
                case CssLengthUnit.Millimeters :
                    v = Convert(Value, unit , GraphicsUnit.Millimeter);
                    strUnit = _mm;
                    break;
                case CssLengthUnit.Inches :
                    v = Convert(Value, unit, GraphicsUnit.Inch);
                    strUnit = _in;
                    break;
                case CssLengthUnit.Picas :
                    v = Convert(Value, unit, GraphicsUnit.Point) / 12;
                    strUnit = _pc;
                    break;
                case CssLengthUnit.Pixels :
                    v = Convert(Value, unit, GraphicsUnit.Pixel);
                    strUnit = _px;
                    break;
                case CssLengthUnit.Points :
                    v = Convert(Value, unit, GraphicsUnit.Point);
                    strUnit = _pt;
                    break;
            }
            return v.ToString(_ValueFormat) + strUnit;
        }
        private static readonly string _ValueFormat = "0.0000";
	}
#if !DCWriterForWASM
    /// <summary>
    /// 长度单位
    /// </summary>
    [System.Runtime.InteropServices.ComVisible(false)]
    [System.Reflection.Obfuscation( Exclude =true , ApplyToMembers = true )]
    public enum LengthUnit
    {
        /// <summary>
        /// 文档单位
        /// </summary>
        Document ,
        /// <summary>
        /// 英尺单位
        /// </summary>
        Inch ,
        /// <summary>
        /// 毫米单位
        /// </summary>
        Millimeter ,
        /// <summary>
        /// 像素单位
        /// </summary>
        Pixel ,
        /// <summary>
        /// 点单位
        /// </summary>
        Point ,
        /// <summary>
        /// 厘米单位
        /// </summary>
        Centimerter ,
        /// <summary>
        /// Twips单位
        /// </summary>
        Twips
    }

#endif
    /// <summary>
    /// CSS长度单位
    /// </summary>
    [System.Runtime.InteropServices.ComVisible(false)]
    public enum CssLengthUnit
    {
        /// <summary>
        /// 厘米
        /// </summary>
        Centimeters ,
        /// <summary>
        /// 毫米
        /// </summary>
        Millimeters,
        /// <summary>
        /// 英寸
        /// </summary>
        Inches,
        /// <summary>
        /// 点
        /// </summary>
        Points,
        /// <summary>
        /// Picas
        /// </summary>
        Picas ,
        /// <summary>
        /// 像素
        /// </summary>
        Pixels ,
    }
}
