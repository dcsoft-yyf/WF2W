using System;
using System.Text;
using System.ComponentModel;
using System.Drawing.Design;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections;
using System.Collections.Generic;

namespace DCSoft.Drawing
{
    /// <summary>
    /// 用于XPenStyle类型的数据编辑器
    /// </summary>
    [System.Runtime.InteropServices.ComVisible(false)]
    public class XPenStyleTypeEditor : UITypeEditor
    {
        /// <summary>
        /// 获得编辑样式
        /// </summary>
        /// <param name="context">上下文</param>
        /// <returns>编辑样式</returns>
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.None;
        }

        /// <summary>
        /// 是否自定义绘制数据
        /// </summary>
        /// <param name="context">上下文</param>
        /// <returns>支持自定义绘制数据</returns>
        public override bool GetPaintValueSupported(ITypeDescriptorContext context)
        {
            return true;
        }
        /// <summary>
        /// 自定义绘制数据
        /// </summary>
        /// <param name="e">事件参数</param>
        public override void PaintValue(PaintValueEventArgs e)
        {
            XPenStyle style = e.Value as XPenStyle;
            System.Drawing.Color c = System.Drawing.Color.Black;
            if (style != null)
            {
                c = style.Color;
            }
            using (System.Drawing.SolidBrush b = new System.Drawing.SolidBrush(c))
            {
                e.Graphics.FillRectangle(b, e.Bounds);
            }
        }
    }
}
