using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel ;
using System.Xml.Serialization ;
using System.Drawing ;
using DCSoft.Drawing ;
using System.Drawing.Design;

namespace DCSoft.TemperatureChart
{
    /// <summary>
    /// 时间轴贴图对象
    /// </summary>

#if !DCWriterForWASM
    [Serializable]
    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = false  )]
    [System.Runtime.InteropServices.ComVisible(false)]
#endif
    public partial class DCTimeLineImage
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public DCTimeLineImage()
        {
        }

        private string _Name = null;
        /// <summary>
        /// 名称
        /// </summary>
        [DefaultValue( null )]
        [XmlAttribute]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        private float _Left = 0;
        /// <summary>
        /// 左端位置,采用Document为单位
        /// </summary>
        [DefaultValue( 0f )]
        [XmlAttribute]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public float Left
        {
            get { return _Left; }
            set { _Left = value; }
        }

        private float _Top = 0;
        /// <summary>
        /// 顶端位置,采用Document为单位
        /// </summary>
        [DefaultValue( 0f )]
        [XmlAttribute]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public float Top
        {
            get { return _Top; }
            set { _Top = value; }
        }

        ///// <summary>
        ///// 宽度
        ///// </summary>
        //internal float Width = 0;
        ///// <summary>
        ///// 高度
        ///// </summary>
        //internal float Height = 0;
#if ! DCWriterForWASM
        /// <summary>
        /// 图片像素宽度
        /// </summary>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public int ImagePixelWidth
        {
            get
            {
                if (_Image == null)
                {
                    return 0;
                }
                else
                {
                    return _Image.Width;
                }
            }
        }

        /// <summary>
        /// 图片像素高度
        /// </summary>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public int ImagePixelHeight
        {
            get
            {
                if (_Image == null)
                {
                    return 0;
                }
                else
                {
                    return _Image.Height;
                }
            }
        }
#endif
        private XImageValue _Image = null;
        /// <summary>
        /// 图片对象
        /// </summary>
        [DefaultValue( null )]
        [Browsable( true )]
#if WINFORM || DCWriterForWinFormNET6
        [Editor(typeof(MyImageValueEditor), typeof(UITypeEditor))]
#endif
        [System.Runtime.InteropServices.ComVisible( false )]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public XImageValue Image
        {
            get
            {
                return _Image; 
            }
            set
            {
                _Image = value; 
            }
        }

        /// <summary>
        /// 复制对象
        /// </summary>
        /// <returns>复制品</returns>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public DCTimeLineImage Clone()
        {
            DCTimeLineImage img = (DCTimeLineImage)this.MemberwiseClone();
            if (this._Image != null)
            {
                img._Image = this._Image.Clone();
            }
            return img;
        }
#if !DCWriterForWASM

        /// <summary>
        /// 返回表示数据的字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.Name + " " + this.GetType().Name;
        }
#endif
    }

#if WINFORM || DCWriterForWinFormNET6
    /// <summary>
    /// 图片数据编辑器
    /// </summary>
    [System.Runtime.InteropServices.ComVisible( false )]
    public class MyImageValueEditor : UITypeEditor
    {
        /// <summary>
        /// 采用对话框模式
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }
        /// <summary>
        /// 编辑图片数据
        /// </summary>
        /// <param name="context"></param>
        /// <param name="provider"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            using (System.Windows.Forms.OpenFileDialog dlg = new System.Windows.Forms.OpenFileDialog())
            {
                dlg.CheckFileExists = true;
                dlg.ShowReadOnly = false;
                dlg.Filter = DCTimeLineStrings.ImageFilter;
                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    return new XImageValue(dlg.FileName);
                }
            }
            return value;
        }
    }
#endif
    /// <summary>
    /// 图片列表
    /// </summary>

#if !DCWriterForWASM
    [Serializable]
    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true )]
    [System.Runtime.InteropServices.ComVisible(false)]
#endif
    public partial class DCTimeLineImageList : List<DCTimeLineImage>
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public DCTimeLineImageList()
        {
        }
    }
}
