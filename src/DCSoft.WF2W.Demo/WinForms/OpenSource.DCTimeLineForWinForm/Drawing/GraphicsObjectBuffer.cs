using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing ;

namespace DCSoft.Drawing
{
    /// <summary>
    /// 一些图形对象的缓存区，使用它能避免频繁的创建和销毁图形对象
    /// </summary>
    [System.Runtime.InteropServices.ComVisible(false)]
     
    public static class GraphicsObjectBuffer
    {
        [ThreadStatic]
        private static Dictionary<int, SolidBrush> _brushes = new Dictionary<int, SolidBrush>();
        [ThreadStatic]
        private static SolidBrush _BlackBrush = null ;
        [ThreadStatic]
        private static SolidBrush _WhiteBrush = null ;
        [ThreadStatic]
        private static SolidBrush _AliceBlueBrush = null;
        [ThreadStatic]
        private static SolidBrush _Brush_Red = null;
        [ThreadStatic]
        private static SolidBrush _Brush_ControlText = null;
        [ThreadStatic]
        private static SolidBrush _Brush_Gray = null;

        internal static readonly int ARGB_White = Color.White.ToArgb();
        internal static readonly int ARGB_Black = Color.Black.ToArgb();
        internal static readonly int ARGB_AliceBlue = Color.AliceBlue.ToArgb();
        internal static readonly int ARGB_Red = Color.Red.ToArgb();
        internal static readonly int ARGB_ControlText = SystemColors.ControlText.ToArgb();
        internal static readonly int ARGB_Gray = Color.Gray.ToArgb();


        /// <summary>
        /// 获得指定颜色的纯色画刷对象
        /// </summary>
        /// <param name="color">指定的颜色</param>
        /// <returns>画刷对象</returns>
        public static SolidBrush GetSolidBrush(Color color)
        {
            var argb = color.ToArgb();
            if (argb == ARGB_Black)
            {
                if (_BlackBrush == null)
                {
                    _BlackBrush = (SolidBrush)Brushes.Black;
                }
                return _BlackBrush;
            }
            if (argb == ARGB_White)
            {
                if (_WhiteBrush == null)
                {
                    _WhiteBrush = (SolidBrush)Brushes.White;
                }
                return _WhiteBrush;
            }
            if (argb == ARGB_AliceBlue)
            {
                if (_AliceBlueBrush == null)
                {
                    _AliceBlueBrush = (SolidBrush)Brushes.AliceBlue;
                }
                return _AliceBlueBrush;
            }
            if (argb == ARGB_Red)
            {
                if (_Brush_Red == null)
                {
                    _Brush_Red = (SolidBrush)Brushes.Red;
                }
                return _Brush_Red;
            }
            if (argb == ARGB_ControlText)
            {
                if (_Brush_ControlText == null)
                {
                    _Brush_ControlText = (SolidBrush)SystemBrushes.ControlText;
                }
                return _Brush_ControlText;
            }
            if (argb == ARGB_Gray)
            {
                if (_Brush_Gray == null)
                {
                    _Brush_Gray = (SolidBrush)Brushes.Gray;
                }
                return _Brush_Gray;
            }
            if (_brushes == null)
            {
                _brushes = new Dictionary<int, SolidBrush>();
            }
            
            SolidBrush b = null;
            if (_brushes.TryGetValue(argb, out b) == false)
            {
                b = new SolidBrush(color);
                _brushes[color.ToArgb()] = b;
            }
            return b;

            
        }

        private static Dictionary<Color, int> _GetSolibBrushCounter = new Dictionary<Color, int>();

        [ThreadStatic]
        private static Dictionary<Color, Pen> _pens = new Dictionary<Color, Pen>();
        /// <summary>
        /// 获得指定颜色的画笔对象
        /// </summary>
        /// <param name="color">指定的颜色</param>
        /// <returns>画笔对象</returns>
        public static Pen GetPen(Color color)
        {
            if( _pens == null )
            {
                _pens = new Dictionary<Color, Pen>();
            }
            Pen result = null;
            if( _pens.TryGetValue( color , out result ) == false )
            {
                result = new Pen(color);
                _pens[color] = result;
            }
            return result;
        }
#if !DCWriterForWASM

        /// <summary>
        /// 清空数据，释放所有资源
        /// </summary>
        public static void Clear()
        {
            if (_brushes != null)
            {
                foreach (SolidBrush b in _brushes.Values)
                {
                    b.Dispose();
                }
                _brushes.Clear();
            }
            if (_pens != null)
            {
                foreach (Pen p in _pens.Values)
                {
                    p.Dispose();
                }
                _pens.Clear();
            }
        }
#endif
    }
}
