using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms ;
using System.Drawing ;
using System.Drawing.Design ;

// 袁永福到此一游

namespace DCSoft.Drawing.Design
{
    /// <summary>
    /// 简单的图片数值编辑器
    /// </summary>
    //[System.Runtime.InteropServices.ComVisible( false )]
    //[System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = false )]
    public class SimpleImageValueEditor : UITypeEditor
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public SimpleImageValueEditor()
        {
        }
        /// <summary>
        /// 返回编辑模式为对话框
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }
        /// <summary>
        /// 编辑数值
        /// </summary>
        /// <param name="context"></param>
        /// <param name="provider"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            //using (OpenFileDialog dlg = new OpenFileDialog())
            //{
            //    dlg.Filter = "*.jpg;*.png;*.gif;*.bmp|*.jpg;*.png;*.gif;*.bmp";
            //    dlg.CheckFileExists = true;
            //    dlg.ShowReadOnly = false;
            //    if (dlg.ShowDialog() == DialogResult.OK)
            //    {
            //        if (context.PropertyDescriptor.PropertyType.Equals(typeof(XImageValue)))
            //        {
            //            return new XImageValue(dlg.FileName);
            //        }
            //        else
            //        {
            //            Image img = Image.FromFile(dlg.FileName);
            //            return img;
            //        }
            //    }
            //}
            return value;
        }
        /// <summary>
        /// 支持自定义绘制数值
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override bool GetPaintValueSupported(ITypeDescriptorContext context)
        {
            return true;
        }
        /// <summary>
        /// 绘制数值
        /// </summary>
        /// <param name="e"></param>
        public override void PaintValue(PaintValueEventArgs e)
        {
            Image img = null;
            if (e.Value is Image)
            {
                img = (Image)e.Value;
            }
            else if (e.Value is XImageValue)
            {
                img = ((XImageValue)e.Value).Value;
            }
            if (img != null)
            {
                e.Graphics.DrawImage(img, e.Bounds);
            }
        }
    }
}
