using System;
using System.Runtime.Serialization ;
using System.Runtime ;
using System.ComponentModel ;
using System.ComponentModel.Design ;
using System.ComponentModel.Design.Serialization ;
using System.Drawing;
using System.Drawing.Imaging;
using DCSoft.Common;
using System.Collections.Generic;
using System.Reflection;

namespace DCSoft.Drawing
{
    /// <summary>
    /// 设计文档图片数据对象
    /// </summary>
    /// <remarks>
    /// 设计文档图片数据对象。它是System.Drawing.Image的一个封装，这个对象保存图片对象，还保存构造图片对象的原始二进制数据。
    /// <br />在设计器的属性列表中，需要从一个文件中加载图片数据，为了保持原始数据的完整性，在保存设计文档时是保存加载图片
    /// 的二进制数据的，加载设计文档时，会从这个原始的二进制数据来加载图片，这样保证的设计的完整性。本对象就是图片和二进制
    /// 数据的混合封装。方便程序加载和保存图片数据。
    /// <br />本对象内部使用了 System.Drawing.Image 对象,可能使用了非托管资源,因此实现了IDisposable 接口,可以用来显式的释放
    /// 非托管资源.
    /// </remarks>
    [System.Serializable()]
    [System.ComponentModel.DefaultProperty("ImageData")]
#if !DCWriterForWASM
    [System.ComponentModel.TypeConverter(typeof(XImageValueTypeConverter))]
#endif
#if WINFORM || DCWriterForWinFormNET6
    [System.ComponentModel.Editor(
        typeof(DCSoft.Drawing.Design.SimpleImageValueEditor),
        //"DCSoft.WinForms.Design.ImageUITypeEditor",
        typeof(System.Drawing.Design.UITypeEditor))]
#endif
    [System.ComponentModel.ToolboxItem(false)]
    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = false)]
    [DCSoft.Common.DCPublishAPI]
    [DCSoft.Common.DCXSD("ImageValue")]
    public partial class XImageValue : System.ICloneable, System.IDisposable, IComponent
    {
       

        public static System.Drawing.Bitmap ConvertToBitmap(object v)
        {
            if (v is System.Drawing.Bitmap)
            {
                return (System.Drawing.Bitmap)v;
            }
            else if (v is byte[])
            {
                return new Bitmap(new System.IO.MemoryStream((byte[])v));
            }
            return null;
        }

        /// <summary>
        /// 对象变量的建议名称前缀
        /// </summary>

        public static string SuggestBaseName = "Image";

        private static int _InstanceIndexCount = 0;
        /// <summary>
        /// 初始化对象
        /// </summary>
        public XImageValue()
        {
            this._ContentVersion = this.GetHashCode();
        }

        /// <summary>
        /// 初始化对象
        /// </summary>
        /// <param name="img">图片数据</param>
        public XImageValue(System.Drawing.Image img)
        {
            this.Value = img;
            this._ContentVersion = this.GetHashCode();
            this._HasNativeImageData = false;
        }
#if !DCWriterForWASM
        /// <summary>
        /// 初始化对象
        /// </summary>
        /// <param name="width">图片宽度</param>
        /// <param name="height">图片高度</param>
        public XImageValue(int width , int height)
        {
            this.myValue = new Bitmap(width, height);
            this._ContentVersion = this.GetHashCode();
            this._HasNativeImageData = false;
        }
#endif
        /// <summary>
        /// 初始化对象
        /// </summary>
        /// <param name="stream">包含图片数据的流对象</param>
        public XImageValue(System.IO.Stream stream)
        {
            byte[] bs = new byte[1024];
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            while (true)
            {
                int len = stream.Read(bs, 0, bs.Length);
                if (len <= 0)
                    break;
                ms.Write(bs, 0, len);
            }
            this.ImageData = ms.ToArray();
            this._ContentVersion = this.GetHashCode();
        }

        /// <summary>
        /// 初始化对象
        /// </summary>
        /// <param name="bs">图片数据</param>
        public XImageValue(byte[] bs)
        {
            this.ImageData = bs;
            this._ContentVersion = this.GetHashCode();
        }

        /// <summary>
        /// 初始化对象
        /// </summary>
        /// <param name="bs">图片数据</param>
        /// <param name="safeLoadMode">是否为安全加载模式</param>
        public XImageValue(byte[] bs, bool safeLoadMode)
        {
            this.SafeLoadMode = safeLoadMode;
            this.ImageData = bs;
            this._ContentVersion = this.GetHashCode();
        }

        /// <summary>
        /// 初始化对象
        /// </summary>
        /// <param name="url">要加载图片数据的URL</param>
        public XImageValue(string url)
        {
            if (this.Load(url) <= 0)
            {
                throw new Exception(string.Format(DrawingStrings.FailToLoadImage_URL, url));
            }
            this._ContentVersion = this.GetHashCode();
        }
#if !DCWriterForWASM
        [NonSerialized]
        private int _InstanceIndex = _InstanceIndexCount++;
        [System.Xml.Serialization.XmlIgnore]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public int InstanceIndex
        {
            get
            {
                return _InstanceIndex;
            }
        }
#endif
        /// <summary>
        /// 比较两个图片数据内容是否一致
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>

        public bool EqualsImageData(XImageValue img)
        {
            if (img == null)
            {
                return false;
            }
            if (img == this)
            {
                return true;
            }
            if (img.myValue == this.myValue)
            {
                return true;
            }
            if (img.bsImage == this.bsImage)
            {
                return true;
            }
            if (img.bsImage != null && this.bsImage != null && img.bsImage.Length == this.bsImage.Length)
            {
                if( img.ImageDataHashCode() != this.ImageDataHashCode())
                {
                    // 数据哈希值不同，肯定不一样。
                    return false;
                }
                for (int index = this.bsImage.Length - 1; index >= 0; index--)
                {
                    if (this.bsImage[index] != img.bsImage[index])
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }

        private float _HorizontalResolution;
        /// <summary>
        /// 获取图片的水平分辨率（以“像素/英寸”为单位）。 
        /// </summary>
        [DefaultValue(0f)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        [DCSoft.Common.DCXSD( DCXSDOutputType.Attribute)]
        public float HorizontalResolution
        {
            get
            {
                return _HorizontalResolution;
            }
            set
            {
                _HorizontalResolution = value;
            }
        }

        private float _VerticalResolution;
        /// <summary>
        /// 获取图片的垂直分辨率（以“像素/英寸”为单位）。 
        /// </summary>
        [DefaultValue(0f)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        [DCSoft.Common.DCXSD( DCXSDOutputType.Attribute)]
        public float VerticalResolution
        {
            get
            {
                return _VerticalResolution;
            }
            set
            {
                _VerticalResolution = value;
            }
        }

        [NonSerialized]
        private int _ContentVersion;
        /// <summary>
        /// 内容版本号,对象数据发生任何改变都会修改此版本号
        /// </summary>
        [Browsable(false)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public int ContentVersion
        {
            get
            {
                return _ContentVersion;
            }
        }

        /// <summary>
        /// 对象是否包含数据
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public bool HasContent
        {
            get
            {
                if (this.myValue != null)
                {
                    return true;
                }
                if (this.bsImage != null && this.bsImage.Length > 0)
                {
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// 转换图片大小
        /// </summary>
        /// <param name="unit">转换后采用的度量单位</param>
        /// <returns>转换后的大小</returns>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public Size ConvertSize( GraphicsUnit unit )
        {
            return GraphicsUnitConvert.Convert(this.Size , GraphicsUnit.Pixel, unit );
        }
        //[NonSerialized]
        //private int _BufferVersion = 0;
#if !DCWriterForWASM
        /// <summary>
        /// 内部的判断是否有图片对象
        /// </summary>
        /// <returns></returns>
        public bool InnerHasValue()
        {
            return this.myValue != null;
        }
#endif
        /// <summary>
        /// 判断是否为BMP位图类型
        /// </summary>
        public bool IsBitmap
        {
            get
            {
                if (this.myValue != null)
                {
                    return this.myValue is System.Drawing.Bitmap;
                }
                if (this.bsImage != null && this.bsImage.Length > 0)
                {
                    return FileHeaderHelper.HasBMPHeader(this.bsImage);
                }
                return this.Value is System.Drawing.Bitmap;
            }
        }
        /// <summary>
        /// 将图片转换为JPEG图片数据数组
        /// </summary>
        /// <returns>转换后的数组</returns>
        public byte[] ToJpegBytes()
        {
            if( this.bsImage != null
                && this.bsImage.Length > 0 
                && FileHeaderHelper.HasJpegHeader( this.bsImage))
            {
                return this.bsImage;
            }
            var v = this.Value;
            if( v != null )
            {
                var ms = new System.IO.MemoryStream();
                v.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                var bs = ms.ToArray();
                ms.Close();
                return bs;
            }
            return null;
        }
        private static readonly string _Header_bmp = "data:image/bmp;base64,";
        private static readonly string _Header_png = "data:image/png;base64,";
        private static readonly string _Header_gif = "data:image/gif;base64,";
        private static readonly string _Header_jpeg = "data:image/jpeg;base64,";
        /// <summary>
        /// 解析DataUrl，获得其中的二进制数据
        /// </summary>
        /// <param name="dataUrl">URL字符串</param>
        /// <returns>二进制数据</returns>
        [System.Reflection.Obfuscation( Exclude = true , ApplyToMembers = true )]
        public static byte[] ParseEmitImageSource( string dataUrl )
        {
            if(dataUrl == null || dataUrl.Length == 0 )
            {
                return null;
            }
            string strData = null;
            if (dataUrl.StartsWith(_Header_png, StringComparison.Ordinal))
            {
                strData = dataUrl.Substring(_Header_png.Length);
            }
            else  if (dataUrl.StartsWith(_Header_jpeg, StringComparison.Ordinal))
            {
                strData = dataUrl.Substring(_Header_jpeg.Length);
            }
            else if (dataUrl.StartsWith(_Header_bmp, StringComparison.Ordinal))
            {
                strData = dataUrl.Substring(_Header_bmp.Length);
            }
            else if (dataUrl.StartsWith(_Header_gif, StringComparison.Ordinal))
            {
                strData = dataUrl.Substring(_Header_gif.Length);
            }
            if( strData != null )
            {
                return Convert.FromBase64String(strData);
            }
            return null;
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public static string StaticGetEmitImageSource(byte[] bs )
        {
            if (bs != null)
            {
                if (FileHeaderHelper.HasBMPHeader(bs))
                {
                    return _Header_bmp + Convert.ToBase64String(bs);
                }
                else if (FileHeaderHelper.HasPNGHeader(bs))
                {
                    return _Header_png + Convert.ToBase64String(bs);
                }
                else if (FileHeaderHelper.HasGIFHeader(bs))
                {
                    return _Header_gif + Convert.ToBase64String(bs);
                }
                else if (FileHeaderHelper.HasJpegHeader(bs))
                {
                    return _Header_jpeg + Convert.ToBase64String(bs);
                }
                else
                {
                    return _Header_jpeg + Convert.ToBase64String(bs);
                }
            }
            return null;
        }
#if !DCWriterForWASM

        public string GetEmitImageSource()
        {
            return StaticGetEmitImageSource(this.ImageData);
        }
#endif
        /// <summary>
        /// 保存图片数据到流中
        /// </summary>
        /// <param name="stream">流对象</param>
        /// <param name="format">图片格式</param>
        public void Save( System.IO.Stream stream , System.Drawing.Imaging.ImageFormat format )
        {
            this.Value.Save(stream, format);
        }

        [System.NonSerialized()]
        private System.Drawing.Image myValue;
        /// <summary>
        /// 显示的图片对象
        /// </summary>
        [System.Xml.Serialization.XmlIgnore()]
        [System.ComponentModel.Browsable(false)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public virtual System.Drawing.Image Value
        {
            get
            {
                
                if (myValue == null)
                {
                    CheckImageData();
                    //if( bsImage != null )
                    //{
                    //    byte[] bs = this.bsImage ;
                    //    this.ImageData = bs ;
                    //}
                }
                return myValue;
            }
            set
            {
                this._ByteSize = 0;
                this._ImageDataHashCode = int.MinValue;
                myValue = value;
                bsImage = null;
                _ContentVersion++;
                if (this.myValue != null)
                {

                    this._HorizontalResolution = myValue.HorizontalResolution;
                    this._VerticalResolution = myValue.VerticalResolution;
                }
                else
                {
                    this._HorizontalResolution = 0;
                    this._VerticalResolution = 0;
                }
                this._HasNativeImageData = false;
            }
        }
#if !DCWriterForWASM
        /// <summary>
        /// 设置透明色
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public bool MakeTransparent()
        {
            Bitmap bmp = (Bitmap)this.Value;
            if (bmp != null)
            {
                bmp.MakeTransparent();
                return true;
            }
            return false;
        }
#endif
        /// <summary>
        /// 设置透明色
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public bool MakeTransparent( System.Drawing.Color c )
        {
            if (c.A > 0)
            {
                Bitmap bmp = (Bitmap)this.Value;
                if( bmp != null )
                {
                    bmp.MakeTransparent(c);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 修改图片格式
        /// </summary>
        /// <param name="format">新的图片格式</param>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
         
        public void ChangeImageFormat(System.Drawing.Imaging.ImageFormat format)
        {
            if (format == null)
            {
                throw new ArgumentNullException("format");
            }
            Image img = this.Value;
            if (img != null)
            {
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                img.Save(ms, format);
                this.myValue.Dispose();
                ms.Close();
                this.myValue = null;
                this.bsImage = ms.ToArray();
            }
        }

        private byte[] bsImage;

        //[System.NonSerialized()]
        //private System.IO.MemoryStream ms = null;
        /// <summary>
        /// 保存图形数据的二进制数据
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        [System.Xml.Serialization.XmlIgnore()]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public byte[] ImageData
        {
            get
            {
                if (bsImage == null && myValue != null)
                {
                    bool flag = false;
                    try
                    {
                        using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                        {
                            var format = GetImageFormat(this.myValue);
                            switch( format )
                            {
                                case XImageFormatType.Bmp: myValue.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp); break;
                                case XImageFormatType.Png: myValue.Save(ms, System.Drawing.Imaging.ImageFormat.Png); break;
                                case XImageFormatType.Jpeg: myValue.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg); break;
                                case XImageFormatType.Gif: myValue.Save(ms, System.Drawing.Imaging.ImageFormat.Gif); break;
                                case XImageFormatType.Emf: myValue.Save(ms, System.Drawing.Imaging.ImageFormat.Emf); break;
                                default: myValue.Save(ms, System.Drawing.Imaging.ImageFormat.Png); break;
                            }
                            bsImage = ms.ToArray();
                        }
                        flag = true;
                    }
                    catch (Exception)
                    {

                    }
                    if (flag == false)
                    {
                        // 发生错误，换种格式再存
                        try
                        {
                            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                            {
                                myValue.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                                bsImage = ms.ToArray();
                            }
                            flag = true;
                        }
                        catch (Exception)
                        {

                        }
                    }
                    if (flag == false)
                    {
                        // 还是发生错误，复制图片对象然后再存
                        try
                        {
                            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                            {
                                using (Image img2 = (Image)myValue.Clone())
                                {
                                    img2.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                                    bsImage = ms.ToArray();
                                    flag = true;
                                }
                            }
                        }
                        catch (Exception)
                        {

                        }
                    }
                }
                return bsImage;
            }
            set
            {
                this._ByteSize = 0;
                this._ImageDataHashCode = int.MinValue;
                if (myValue != null)
                {
                    if (_ImageDataList.IsInBuffer(this.myValue) == false)
                    {
                        myValue.Dispose();
                    }
                    //myValue.Dispose();
                    this._VerticalResolution = 0;
                    this._HorizontalResolution = 0;
                }
                bsImage = GetContent(value);
                myValue = null;
                this._SizeFromBinary = GetImageSizeFromBinary(bsImage);
               
                _HasNativeImageData = true;
                
                _ContentVersion++;
            }
        }

        public byte[] GetImageDataRaw()
        {
            return this.bsImage;
        }

        [NonSerialized]
        private int _ImageDataHashCode = int.MinValue;
        /// <summary>
        /// 图片数据HashCode值。
        /// </summary>
        private int ImageDataHashCode()
        {

            if (_ImageDataHashCode == int.MinValue)
            {
                if (bsImage == null || bsImage.Length == 0)
                {
                    if (this.myValue == null)
                    {
                        return 0;
                    }
                }
                else
                {
                    for (int iCount = this.bsImage.Length - 1; iCount >= 0; iCount--)
                    {
                        _ImageDataHashCode += bsImage[iCount];
                    }
                }
            }
            return _ImageDataHashCode;

        }

      
        /// <summary>
        /// 是否缓存长度不超过100KB大小的图片数据,如果缓存，能加快一些图片对象的加载速度。
        /// </summary>
        public static bool EnabledCache100KBImageData = true;

        private void CheckImageData()
        {
            if (this.bsImage != null && this.bsImage.Length > 0 && myValue == null)
            {
               
#if DCWriterForWASM
                this.myValue = new System.Drawing.Bitmap(this.bsImage);
#else
                if (EnabledCache100KBImageData && IsTooBig(this.bsImage) == false)
                {
                    this.myValue = _ImageDataList.GetValue(this.bsImage, false);
                }
                else
                {
                    //bsImage = new byte[] { 0,1,2,3,4,5,65,7,8,9,9};
                    var ms = new System.IO.MemoryStream(this.bsImage);
                    try
                    {
                        this.myValue = System.Drawing.Image.FromStream(ms);
                    }
                    catch (System.InvalidOperationException ext)
                    {
                        if (DCSoft.Common.FileHeaderHelper.HasGIFHeader(this.bsImage)
                            && DCSoft.Common.DebugHelper.IsLinuxOrUnixPlatform)
                        {
                            throw new System.Exception("本Linux服务器可能不支持 GIF 图片文件格式,请手工转换图片文件格式。" + ext.Message, ext);
                        }
                        else
                        {
                            //throw new System.InvalidOperationException(
                            //    "Hexs:0x" + DCSoft.Common.StringCommon.PartToHexString(
                            //        this.bsImage,
                            //        0,
                            //        (int)Math.Min(10, this.bsImage.Length))
                            //    + "(" + DCSoft.Common.FileHelper.FormatByteSize(this.bsImage.Length)
                            //    + ")," + ext.Message, ext);
                        }
                    }
                    catch( System.Exception ext )
                    {

                    }
                }
                
#endif
            }
        }


         
        public static void ClearImageDataBuffer()
        {
            if (_ImageDataList != null)
            {
                lock (_ImageDataList)
                {
                    _ImageDataList.Clear();
                }
            }
        }

       
        /// <summary>
        /// 全局图片数据缓存对象
        /// </summary>
        private class GlobalImageValueBuffer
        {
            /// <summary>
            /// 初始化对象
            /// </summary>
            public GlobalImageValueBuffer()
            {

            }

            private int _Version = 0;
            //public int Version
            //{
            //    get
            //    {
            //        return this._Version;
            //    }
            //}
            /// <summary>
            /// 清空对象数据
            /// </summary>
            public void Clear()
            {
                this._Version++;
                foreach (GlobalImageBufferItem item in this._Items)
                {
                    item._Content = null;
                    //if (item._Value != null)
                    {
                        //if (item._AllowDisposeImage)
                        //{
                        //    item._Value.Dispose();
                        //}
                        item._Value = null;
                    }
                }
                this._Items.Clear();
                this._ByteSize = 0;
            }
            /// <summary>
            /// 获得缓存的数据
            /// </summary>
            /// <param name="bs">原始数据</param>
            /// <returns>缓存的数据</returns>
            public byte[] GetContent(byte[] bs)
            {
                lock (this)
                {
                    int index = GetIndex(bs);
                    if (index >= 0)
                    {
                        return this._Items[index]._Content;
                    }
                    return null;
                }
            }
            private int _MaxByteSize = 20 * 1024 * 1024;
#if !DCWriterForWASM
            /// <summary>
            /// 最大允许缓存的字节数
            /// </summary>
            public int MaxByteSize
            {
                get
                {
                    return _MaxByteSize;
                }
                set
                {
                    _MaxByteSize = value;
                }
            }
#endif
            private int _ByteSize = 0;

            private int GetIndex( byte[] bs )
            {
                if (bs == null || bs.Length == 0)
                {
                    return -1;
                }
                for (int iCount = 0; iCount < this._Items.Count; iCount++)
                {
                    GlobalImageBufferItem item = this._Items[iCount];
                    if ( DCSoft.Common.BinaryHelper.Equals( item._Content , bs ))
                    {
                        return iCount;
                    }
                }
                _ByteSize += bs.Length;
                if(_ByteSize >= _MaxByteSize )
                {
                    this.Clear();
                }
                GlobalImageBufferItem ni = new GlobalImageBufferItem( );
                ni._Content = bs;
                this._Items.Insert(0, ni);
                this._Version++;
                return 0;// this._Items.Count - 1 ;
            }
#if !DCWriterForWASM
            /// <summary>
            /// 获得缓存的图片对象
            /// </summary>
            /// <param name="bs">图片数据</param>
            /// <param name="allowDisposeImage">允许销毁图片对象</param>
            /// <returns>获得的图片对象</returns>
            public System.Drawing.Image GetValue( byte[] bs , bool allowDisposeImage = true )
            {
                lock (this)
                {
                    int index = GetIndex(bs);
                    if (index >= 0)
                    {
                        this._Items[index]._AllowDisposeImage = allowDisposeImage;
                        return this._Items[index].GetValue();
                    }
                }
                return null;
            }
#endif
           
            public bool Remove(System.Drawing.Image img)
            {
                if( img == null )
                {
                    return false;
                }
                lock (this)
                {
                    for (int iCount = this._Items.Count - 1; iCount >= 0; iCount--)
                    {
                        GlobalImageBufferItem item = this._Items[iCount];
                        if (item._Value == img)
                        {
                            this._Items.RemoveAt(iCount);
                            item._Content = null;
                            item._Value = null;
                            return true;
                        }
                    }
                    return false;
                }
            }
            public bool Remove(byte[] bs)
            {
                if( bs == null )
                {
                    return false;
                }
                lock (this)
                {
                    for (int iCount = this._Items.Count - 1; iCount >= 0; iCount--)
                    {
                        GlobalImageBufferItem item = this._Items[iCount];
                        if (item._Content == bs )
                        {
                            this._Items.RemoveAt(iCount);
                            item._Content = null;
                            item._Value = null;
                            return true;
                        }
                    }
                    return false;
                }
            }

            /// <summary>
            /// 判断图片是否是在缓存区中
            /// </summary>
            /// <param name="img">图片对象</param>
            /// <returns>是否在缓存区中</returns>
            public bool IsInBuffer(System.Drawing.Image img)
            {
                lock (this)
                {
                    foreach (GlobalImageBufferItem item in this._Items)
                    {
                        if (item._Value != null && item._Value == img)
                        {
                            return true;
                        }
                    }
                    return false;
                }
            }

            private readonly List<GlobalImageBufferItem> _Items = new List<GlobalImageBufferItem>();
            private class GlobalImageBufferItem
            {
                public GlobalImageBufferItem()
                {

                }
                public byte[] _Content = null;
                private bool _IsBad = false;
                public Image _Value = null;
                public bool _AllowDisposeImage = true;
#if !DCWriterForWASM

                public Image GetValue()
                {
                    if(  _IsBad )
                    {
                        return null;
                    }
                    if (_Value == null || IsDisposed(_Value))
                    {
                        try
                        {
                            System.IO.MemoryStream ms = new System.IO.MemoryStream(this._Content);
                            this._Value = System.Drawing.Image.FromStream(ms);
                        }
                        catch( System.Exception ext )
                        {
                            //DCConsole.Default.WriteLineError(ext.ToString());
                            this._IsBad = true;
                        }
                    }
                    return this._Value;
                }
#endif
            }
        }//private class GlobalImageValueBuffer

        private static object FieldNativeImage = null;
        /// <summary>
        /// 判断图片是否被销毁掉了。
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        public static bool IsDisposed(System.Drawing.Image img)
        {
            if (img == null)
            {
                throw new ArgumentNullException("img");
            }
            if (FieldNativeImage == null)
            {
                FieldNativeImage = typeof(System.Drawing.Image).GetField(
                    "nativeImage",
                    BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                if (FieldNativeImage == null)
                {
                    FieldNativeImage = new object();
                }
            }
            if (FieldNativeImage is FieldInfo)
            {
                IntPtr p = (IntPtr)((FieldInfo)FieldNativeImage).GetValue(img);
                if (p == IntPtr.Zero)
                {
                    // 图片句柄为空，说明已经被销毁掉了。
                    return true;
                }
            }
            return false;
        }

        private static bool IsTooBig( byte[] bs )
        {
            if( bs != null && bs.Length > 100 * 1024)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static GlobalImageValueBuffer _ImageDataList = new GlobalImageValueBuffer();

        private static byte[] GetContent(byte[] bs)
        {
            if (EnabledCache100KBImageData)
            {
                if (bs == null || bs.Length == 0)
                {
                    return bs;
                }
                if (IsTooBig(bs))
                {
                    return bs;
                }
                return _ImageDataList.GetContent(bs);
            }
            else
            {
                return bs;
            }
        }



        private bool _SafeLoadMode = true;
        /// <summary>
        /// 安全模式
        /// </summary>
        [Browsable( false )]
        [System.Xml.Serialization.XmlIgnore]
        public bool SafeLoadMode
        {
            get
            {
                return _SafeLoadMode; 
            }
            set
            {
                _SafeLoadMode = value; 
            }
        }

        private bool _FormatBase64String;
        /// <summary>
        /// 是否格式化输出Base64字符串
        /// </summary>
        [Browsable( false )]
        [System.Xml.Serialization.XmlIgnore]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public bool FormatBase64String
        {
            get
            {
                return _FormatBase64String; 
            }
            set
            {
                _FormatBase64String = value; 
            }
        }

        /// <summary>
        /// 包含图片数据的Base64字符串
        /// </summary>
        [System.ComponentModel.Browsable( false )]
        [System.Xml.Serialization.XmlElement()]
        [System.ComponentModel.DefaultValue( null )]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string ImageDataBase64String
        {
            get
            {
                byte[] bs = this.ImageData;
                if (bs != null && bs.Length > 0)
                {
                    string txt = Convert.ToBase64String(bs);
                    if (this.FormatBase64String)
                    {
                        txt = DCSoft.Common.StringFormatHelper.GroupFormatString(txt, 8, 16);
                    }
                    return txt;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                this._ByteSize = 0;
                if (value != null && value.Length > 0)
                {
                    try
                    {
                       
                        var t1 = DateTime.Now;
//#if DCWriterForWASM
//                        this.ImageData = DCSoft.Writer.Controls.WASMEnvironment.FromBase64String(value);
//#else
                        this.ImageData = Convert.FromBase64String(value );
//#endif
                        var tick = (DateTime.Now - t1).TotalMilliseconds;

                    }
                    catch (FormatException e)
                    {
                        // 数据格式错误，试图修复
                        var bs = DCSoft.Common.StringCommon.TryConvertFromBase64String(value);
                        this.ImageData = bs;
                    }
                    catch( System.Exception e3)
                    {
                        Console.WriteLine(e3.ToString());
                        this.ImageData = null;
                    }
                    _HasNativeImageData = true;
                    _ContentVersion++;
                }
            }
        }

        [NonSerialized]
        private bool _HasNativeImageData;
        /// <summary>
        /// 是否具有原始图片数据
        /// </summary>
        public bool HasNativeImageData()
        {
            return this._HasNativeImageData;
        }
        /// <summary>
        /// 图片格式
        /// </summary>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public System.Drawing.Imaging.ImageFormat RawFormat
		{
			get
			{
				if( this.Value != null )
				{
					return this.Value.RawFormat ;
				}
				return null;
			}
		}

		/// <summary>
		/// 图片宽度
		/// </summary>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public int Width
		{
			get
			{
                return this.Size.Width;
			}
		}
		/// <summary>
		/// 图片高度
		/// </summary>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public int Height
		{
			get
			{
                return this.Size.Height;
			}
		}

        private System.Drawing.Size _SizeFromBinary = Size.Empty;

		/// <summary>
		/// 图片大小
		/// </summary>
		[System.ComponentModel.Browsable( false )]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public System.Drawing.Size Size
		{
			get
			{
                if(this.myValue != null )
                {
                    return this.myValue.Size;
                }
                if( this._SizeFromBinary.Width > 0 )
                {
                    return this._SizeFromBinary;
                }
                if (this.Value == null)
                {
                    return System.Drawing.Size.Empty;
                }
                else
                {
                    return this.Value.Size;
                }
			}
		}

        

        /// <summary>
        /// 创建指定大小的缩略图片
        /// </summary>
        /// <param name="thumbWidth">缩略图宽度</param>
        /// <param name="thumbHeight">缩略图高度</param>
        /// <returns>生成的缩略图对象</returns>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public XImageValue GetThumbnailImage(int thumbWidth, int thumbHeight)
        {
            System.Drawing.Image img = this.Value;
            if (img != null)
            {
                System.Drawing.Image img2 = img.GetThumbnailImage(
                    thumbWidth, 
                    thumbHeight, 
                    new System.Drawing.Image.GetThumbnailImageAbort(this.ThumbnailCallback),
                    IntPtr.Zero);
                return new XImageValue(img2);
            }
            return null;
        }
#if ! DCWriterForWASM
        /// <summary>
        /// 创建指定大小的缩略图片
        /// </summary>
        /// <param name="thumbWidth">缩略图宽度</param>
        /// <param name="thumbHeight">缩略图高度</param>
        /// <param name="backColor">背景色</param>
        /// <returns>生成的缩略图对象</returns>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public XImageValue GetThumbnailImage(int thumbWidth, int thumbHeight, Color backColor )
        {
            System.Drawing.Image img = this.Value;
            if (img != null)
            {
                Bitmap bmp = new Bitmap(thumbWidth, thumbHeight);
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.Clear(backColor);
                    g.DrawImage(img, 0, 0, thumbWidth, thumbHeight);
                }
                return new XImageValue(bmp);
            }
            return null;
        }
#endif

        private bool ThumbnailCallback()
        {
            return false;
        }


		/// <summary>
		/// 复制对象
		/// </summary>
		/// <returns>复制后的对象</returns>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
         
        public XImageValue Clone()
		{
            XImageValue obj = ( XImageValue ) this.MemberwiseClone();
            obj.myValue = null; ;
            obj.bsImage = null;
            if( this.bsImage != null )
			{
				obj.bsImage = new byte[ this.bsImage.Length ];
				Array.Copy(this.bsImage , 0 , obj.bsImage , 0 , this.bsImage.Length );
			}
            else if (this.myValue != null)
            {
                obj.myValue = (System.Drawing.Image)this.myValue.Clone();
            }
            obj._ContentVersion = this._ContentVersion;
			return obj ;
		}
#if !DCWriterForWASM
        /// <summary>
        /// 引用模式的复制对象
        /// </summary>
        /// <returns>复制品</returns>
        /// <remarks>
        /// 通过这种模式复制出来的对象，其引用的原始图片对象和字节数组是相同的。
        /// 而Clone()方法复制出来的对象，其原始图片对象和字节数组也是完全复制的。
        /// </remarks>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public XImageValue CloneImageDataOnly()
        {
            XImageValue v = new XImageValue();
            v.myValue = this.myValue;
            v.bsImage = this.bsImage;
            v._ContentVersion = this._ContentVersion;
            return v;
        }
#endif
        /// <summary>
        /// 复制对象
        /// </summary>
        /// <returns>复制后的对象</returns>
        object System.ICloneable.Clone()
		{
			XImageValue obj = new XImageValue();
			if( myValue != null )
			{
				obj.myValue = ( System.Drawing.Image ) myValue.Clone();
			}
			if( bsImage != null )
			{
				obj.bsImage = new byte[ bsImage.Length ];
				Array.Copy( bsImage , 0 , obj.bsImage , 0 , bsImage.Length );
			}
            obj._ContentVersion = this._ContentVersion;
			return obj ;
		}

        /// <summary>
        /// 从Base64字符串加载图片数据
        /// </summary>
        /// <param name="base64">BASE64字符串</param>
        /// <returns>加载的字节数</returns>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public int LoadBase64String(string base64)
        {
            this._ByteSize = 0;
            if (base64 == null || base64.Length == 0 )// string.IsNullOrEmpty(base64))
            {
                this.bsImage = null;
                this.myValue = null;
                return 0;
            }
            else
            {
                byte[] bs = Convert.FromBase64String(base64);
                this.ImageData = bs;
                return bs.Length;
            }
        }

        /// <summary>
        /// 从指定URL加载图片数据
        /// </summary>
        /// <param name="strUrl">URL字符串</param>
        /// <returns>加载的字节数</returns>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public int Load(string strUrl)
        {
            System.Uri uri = new Uri(strUrl);
            if (uri.Scheme == Uri.UriSchemeFile)
            {

                if (System.IO.File.Exists(strUrl) == false)
                {
                    return -1;
                }
                byte[] bs = null;
                using (System.IO.FileStream file = new System.IO.FileStream(
                    strUrl,
                    System.IO.FileMode.Open,
                    System.IO.FileAccess.Read))
                {
                    bs = new byte[file.Length];
                    file.Read(bs, 0, bs.Length);
                    file.Close();
                }
                this.ImageData = bs;
                if (bs == null)
                {
                    return -1;
                }
                else
                {
                    return bs.Length;
                }

            }
            //else if (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps)
            //{
            //    byte[] bs = DCSoft.Common.DCHttpClient.Default.DownloadData(strUrl);
            //    this.ImageData = bs;
            //    if (bs != null)
            //    {
            //        return bs.Length;
            //    }
            //}
            return 0;
        }

        [NonSerialized]
        private int _ByteSize;
        /// <summary>
        /// 图片内容的字节长度。可能不精确。
        /// </summary>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public int ByteSize
        {
            get
            {
                if( _ByteSize == 0 )
                {
                    if( this.bsImage != null && this.bsImage .Length > 0  )
                    {
                        return this.bsImage.Length;
                    }
                }
                if( this.myValue != null && IsDisposed( this.myValue ) == false )
                {
                    int v = this.myValue.Width * this.myValue.Height ;
                    v = v / 2;
                    return v;
                }
                return _ByteSize;
            }
        }
#if !DCWriterForWASM
		/// <summary>
		/// 保存图片数据到指定文件中
		/// </summary>
		/// <param name="FileName">文件名</param>
		/// <returns>操作是否成功</returns>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public bool Save(string FileName)
		{
			if( myValue != null || bsImage != null )
			{
				using( System.IO.FileStream stream = new System.IO.FileStream( 
						   FileName ,
						   System.IO.FileMode.Create ,
						   System.IO.FileAccess.Write ))
				{
					if( bsImage != null )
					{
						stream.Write( bsImage , 0 , bsImage.Length );
					}
					else
					{
						myValue.Save( stream , GetFormat( FileName ));
                        this._ByteSize = (int)stream.Length;
                    }
				}
				return true ;
			}
			return false;
		}

        /// <summary>
        /// 获得图片格式
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns>图片格式</returns>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
         
        public static ImageFormat GetFormat(string fileName)
        {
            if (fileName != null)
            {
                fileName = fileName.Trim().ToLower();
                if (fileName.EndsWith(".bmp"))
                {
                    return ImageFormat.Bmp;
                }
                else if (fileName.EndsWith(".jpg") || fileName.EndsWith(".jpeg"))
                {
                    return ImageFormat.Jpeg;
                }
                else if (fileName.EndsWith(".png"))
                {
                    return ImageFormat.Png;
                }
                else if (fileName.EndsWith(".gif"))
                {
                    return ImageFormat.Gif;
                }
                else if (fileName.EndsWith(".wmf"))
                {
                    return ImageFormat.Wmf;
                }
            }
            return ImageFormat.Png;
        }
#endif
        /// <summary>
        /// 从全局缓存中删除对象
        /// </summary>
        public void RemoveFromInnerBuffer()
        {
            if (EnabledCache100KBImageData)
            {
                if (this.bsImage != null)
                {
                    _ImageDataList.Remove(this.bsImage);
                }
                if (this.myValue != null)
                {
                    _ImageDataList.Remove(this.myValue);
                }
            }
        }
		/// <summary>
		/// 销毁对象
		/// </summary>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void Dispose()
        {
            if (myValue != null)
            {
                if ( EnabledCache100KBImageData)
                {
                    if (_ImageDataList.IsInBuffer(this.myValue) == false)
                    {
                        myValue.Dispose();
                    }
                }
                else
                {
                    myValue.Dispose();
                }
                myValue = null;
            }
            this.mySite = null;
            //if (bsImage != null)
            {
                bsImage = null;
            }
            if (Disposed != null)
            {
                Disposed(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// 返回表示对象内容的字符串
        /// </summary>
        /// <returns></returns>
         
        public override string ToString()
        {
            System.Drawing.Image img = this.Value;
            byte[] bs = this.ImageData;
            if (bsImage == null || myValue == null)
            {
                return "None";
            }
            if (bsImage.Length < 1024)
            {
                return bsImage.Length + "B " + img.Width + "*" + img.Height;
            }
            else
            {
                return Convert.ToInt32(bsImage.Length / 1024) + "KB " + img.Width + "*" + img.Height;
            }
        }

#region IComponent 成员

        /// <summary>
        /// 对象销毁事件
        /// </summary>
         
        [field:NonSerialized]
        public event EventHandler Disposed = null;
        [NonSerialized]
        private ISite mySite;
        /// <summary>
        /// 对象站点
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        [System.Xml.Serialization.XmlIgnore()]
        [System.ComponentModel.DesignerSerializationVisibility(
            DesignerSerializationVisibility.Hidden)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
         
        public ISite Site
        {
            get
            {
                return mySite;
            }
            set
            {
                mySite = value;
            }
        }

#endregion
         
        public void CopyValueTo(XImageValue v)
        {
            if (v == null || v == this )
            {
                return;
            }
            v._ContentVersion = this._ContentVersion;
            v._FormatBase64String = this._FormatBase64String;
            v._HorizontalResolution = this._HorizontalResolution;
            v._VerticalResolution = this._VerticalResolution;
            v.bsImage = this.bsImage;
            v.myValue = this.myValue;
            v.mySite = this.mySite;
        }
#if !DCWriterForWASM
        public XImageFormatType FormatType
        {
            get
            {
                if (this.bsImage != null)
                {
                    return GetImageFormat(this.bsImage);
                }
                else if (this.myValue != null)
                {
                    return GetImageFormat(this.myValue);
                }
                else
                {
                    var img = this.Value;
                    if (img != null)
                    {
                        return GetImageFormat(img);
                    }
                }
                return XImageFormatType.Unkonw;
            }
        }

        public static XImageFormatType GetImageFormat(byte[] data )
        {
            if (FileHeaderHelper.HasJpegHeader(data))
            {
                return XImageFormatType.Jpeg;
            }
            else if (FileHeaderHelper.HasPNGHeader(data))
            {
                return XImageFormatType.Png;
            }
            else if (FileHeaderHelper.HasGIFHeader(data))
            {
                return XImageFormatType.Gif;
            }
            else if (FileHeaderHelper.HasBMPHeader(data))
            {
                return XImageFormatType.Bmp;
            }
            else
            {
                return XImageFormatType.Unkonw;
            }
        }
        
#endif
        public static Size GetImageSizeFromBinary(byte[] bs)
        {
            if (bs == null)
            {
                return Size.Empty;// throw new ArgumentNullException("bs");
            }
            if (FileHeaderHelper.HasBMPHeader(bs))
            {
                var width = BitConverter.ToInt32(bs, 0x12);
                var height = BitConverter.ToInt32(bs, 0x16);
                return new Size(width, height);
            }
            else if (FileHeaderHelper.HasPNGHeader(bs))
            {
                var width = BytesToInt32(bs, 16);
                var height = BytesToInt32(bs, 20);
                return new Size(width, height);
            }
            else if (FileHeaderHelper.HasJpegHeader(bs))
            {
                return GetJpgSize(bs);
            }
            else if (FileHeaderHelper.HasGIFHeader(bs))
            {
                byte[] buffer = { bs[6], bs[7], bs[8], bs[9] };
                var width = BitConverter.ToInt16(buffer, 0);
                var height = BitConverter.ToInt16(buffer, 2);
                return new Size(width, height);
            }
            return Size.Empty;
        }

        private static int BytesToInt32( byte[] bs , int startIndex )
        {
            int result = bs[startIndex];
            result =( result << 8  ) + bs[startIndex + 1];
            result = (result << 8) + bs[startIndex + 2];
            result = (result << 8) + bs[startIndex + 3];
            return result;
        }
        /// <summary>
        /// 获取图片宽高和高度
        /// </summary>
        /// <param name="bs"></param>
        /// <returns></returns>
        private static Size GetJpgSize(byte[] bs)
        {
            Size JpgSize = new Size(0, 0);
            if (!(bs[0] == 0xFF) && (bs[1] == 0xD8))
            {//不是jpg
                return JpgSize;
            }
            //段类型
            int mytype = -1;
            int mytype2 = -1;
            //记录当前读取位置
            long myps = 1;

            do
            {
                do
                {
                    //每个新段的开始标识为0xff，查找下一个新段
                    myps = myps + 1;
                    mytype2 = bs[myps];

                    if (mytype2 < 0)//文件结束
                    {
                        return JpgSize;
                    }
                } while (mytype2 != 0xff);
                do
                {
                    //段与段之间有一个或多个0xff间隔，跳过这些0xff之后的字节为段标识
                    myps = myps + 1;
                    mytype = bs[myps];
                } while (mytype == 0xff);
                //long s1 = myps;//调试使用
                switch (mytype)
                {
                    case 0x00:
                    case 0x01:
                    case 0xD0:
                    case 0xD1:
                    case 0xD2:
                    case 0xD3:
                    case 0xD4:
                    case 0xD5:
                    case 0xD6:
                    case 0xD7:
                        break;
                    case 0xc0://普通JPG的SOFO段
                    case 0xc2://JFIF格式的SOFO段
                        {
                            myps = myps + 3;//跳过：数据长度2个字节、精度1个字节
                            int height = bs[myps + 1] * 256;
                            height += bs[myps + 2];
                            int width = bs[myps + 3] * 256;
                            width += bs[myps + 4];
                            JpgSize.Width = width;
                            JpgSize.Height = height;
                            return JpgSize;
                        }
                    default:
                        int a1 = bs[myps + 1];//下一个
                        int ps1 = a1 * 256;
                        long p2 = myps + 1;
                        int a2 = bs[myps + 1 + 1];//再下一个
                        myps = p2 + ps1 + a2 - 2;
                        break;
                }
                if (myps + 1 >= bs.Length)//文件结束
                {
                    return JpgSize;
                }
            } while (mytype != 0xda);//扫描结束
            return JpgSize;
        }
        public static XImageFormatType GetImageFormat( System.Drawing.Image img )
        {
            if(img == null )
            {
                throw new ArgumentNullException("img");
            }
            if(img.RawFormat == null )
            {
                throw new ArgumentNullException("img.RawFormat");
            }
            var id = img.RawFormat.Guid;
            if(id == ImageFormat.Png.Guid )
            {
                return XImageFormatType.Png;
            }
            else if( id == ImageFormat.Jpeg.Guid)
            {
                return XImageFormatType.Jpeg;
            }
            else if(id == ImageFormat.Gif.Guid)
            {
                return XImageFormatType.Gif;
            }
            else if( id == ImageFormat.Bmp.Guid)
            {
                return XImageFormatType.Bmp;
            }
            else if( id == ImageFormat.Emf.Guid)
            {
                return XImageFormatType.Emf;
            }
            return XImageFormatType.Unkonw;
        }
#if !DCWriterForWASM
        private static readonly string[] _UrlHeaders = new string[] {
            "data:image/jpeg;base64,",
            "data:image/bmp;base64,",
            "data:image/png;base64,",
            "data:image/gif;base64,",
            "data:image/emf;base64,"
        };
        /// <summary>
        /// 获得以BASE64字符串为内容的图片来源的标记头
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string GetBse64UrlHeader(XImageFormatType format)
        {
            if (format >= 0 && (int)format < _UrlHeaders.Length)
            {
                return _UrlHeaders[(int)format];
            }
            else
            {
                return _UrlHeaders[0];
            }
        }
        /// <summary>
        /// 修改图片格式为指定类型
        /// </summary>
        /// <param name="type">图片格式</param>
        /// <returns>修改是否成功</returns>
        public bool ChangeFormat( XImageFormatType type )
        {
            if( this.FormatType != type )
            {
                var v = this.Value;
                if (v != null)
                {
                    var ms = new System.IO.MemoryStream();
                    switch (type)
                    {
                        case XImageFormatType.Bmp: v.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp); break;
                        case XImageFormatType.Png: v.Save(ms, System.Drawing.Imaging.ImageFormat.Png); break;
                        case XImageFormatType.Jpeg: v.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg); break;
                        default: return false;
                    }
                    this.bsImage = ms.ToArray();
                    this.myValue = null;
                    ms.Close();
                    return true;
                }
            }
            return false;
        }
#endif
    }//public class XImageValue : System.ICloneable , System.IDisposable

    /// <summary>
    /// 图片格式类型
    /// </summary>
    [System.Runtime.InteropServices.ComVisible(true)]
#if !DCWriterForWASM
    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
#endif
    public enum XImageFormatType
    {
        Jpeg,
        Bmp,
        Png,
        Gif,
        Emf,
        Unkonw
    }

#if !DCWriterForWASM
	/// <summary>
	/// 图片数据类型转换器,设计器内部使用
	/// </summary>
    [System.Runtime.InteropServices.ComVisible(false)]
    //[System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
    public class XImageValueTypeConverter : TypeConverter
	{
        /// <summary>
        /// 判断能否将指定类型的数据转换为图片
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="sourceType">指定的数据类型</param>
        /// <returns>能否转换</returns>
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return ((sourceType == typeof(byte[]))
                || sourceType == typeof( string ) 
                || base.CanConvertFrom(context, sourceType));
		}

        /// <summary>
        /// 判断能否将图片转换为指定的类型
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="destinationType">指定的数据类型</param>
        /// <returns>能否转换</returns>
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			if( destinationType == null )
			{
				return false;
			}
			return ( destinationType.Equals( typeof( byte[] ))
				|| destinationType.Equals( typeof( InstanceDescriptor ))
				|| base.CanConvertTo( context , destinationType ));
		}

        /// <summary>
        /// 将指定的数据转换为图片对象
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="culture">区域信息</param>
        /// <param name="Value">要转换的数据</param>
        /// <returns>转换结果</returns>
		public override object ConvertFrom(
            ITypeDescriptorContext context, 
            System.Globalization.CultureInfo culture, 
            object Value)
		{
			if (Value is byte[])
			{
				return new XImageValue( ( byte[] ) Value );
			}
            if (Value is string)
            {
                string str = (string)Value;
                if (str == null || str.Trim().Length == 0)
                {
                    return new XImageValue();
                }
                else
                {
                    foreach (char c in str)
                    {
                        if ("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789+/= \r\n\t".IndexOf(c) < 0)
                        {
                            // 不是合格的BASE64字符串
                            return new XImageValue();
                        }
                    }
                    byte[] bs = Convert.FromBase64String(str);
                    return new XImageValue(bs);
                }
            }
			return base.ConvertFrom(context, culture, Value);
		}

        /// <summary>
        /// 将图片转换为指定类型的数据
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="culture">区域信息</param>
        /// <param name="Value">图片数据</param>
        /// <param name="destinationType">指定的类型</param>
        /// <returns>转换结果</returns>
		public override object ConvertTo(
            ITypeDescriptorContext context,
            System.Globalization.CultureInfo culture, 
            object Value,
            Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			
			XImageValue img = ( XImageValue ) Value ;
			if( img == null )
			{
				return "[NULL]";
			}
			if (destinationType == typeof(string))
			{
				return img.ToString();
			}

			if (destinationType == typeof(byte[]))
			{
				return img.ImageData ;
			}
		
			if ( destinationType == typeof(InstanceDescriptor))
			{
				byte[] bs = img.ImageData ;
				if( bs == null || bs.Length == 0 )
				{
					return new InstanceDescriptor( typeof( XImageValue).GetConstructor(
                        new Type[]{}) ,
                        new object[]{});
				}
				else
				{
					System.Reflection.MemberInfo constructor = typeof( XImageValue ).GetConstructor( new Type[]{ typeof( byte[] ) });
					return new InstanceDescriptor(constructor , new object[]{ img.ImageData });
				}
			}

			return base.ConvertTo(context, culture, Value, destinationType);
		}

        /// <summary>
        /// 获得图片对象的属性
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="Value">图片数据</param>
        /// <param name="attributes">特性</param>
        /// <returns>属性列表</returns>
        public override PropertyDescriptorCollection GetProperties(
            ITypeDescriptorContext context,
            object Value,
            Attribute[] attributes)
        {
            return TypeDescriptor.GetProperties( Value == null ? typeof( XImageValue ) : Value.GetType(), attributes).Sort(new string[] { "Width", "Height", "RawFormat" });
        }

        /// <summary>
        /// 支持获得图片对象的属性
        /// </summary>
        /// <param name="context">上下文</param>
        /// <returns>是否支持获得属性</returns>
		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return true;
		}
	}
#endif
}