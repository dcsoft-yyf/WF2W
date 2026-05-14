//------------------------------------------------------------------------------
// <copyright file="ImageList.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace System.Windows.Forms
{
    using System.Runtime.InteropServices;
    using System.ComponentModel;
    using System.ComponentModel.Design;
    using System.Diagnostics;
    using System;
    using System.Collections.Specialized;
    using System.Collections;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Drawing.Design;
    using System.Windows.Forms;
    using System.Windows.Forms.Design;
    using System.IO;
    using System.ComponentModel.Design.Serialization;
    using System.Runtime.Versioning;

    using Microsoft.Win32;
    using System.Security;
    using System.Security.Permissions;
    using System.Globalization;

    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = false)]
    public sealed class ImageList : Component
    {

        // gpr: Copied from Icon
        private static Color fakeTransparencyColor = Color.FromArgb(0x0d, 0x0b, 0x0c);
        private static Size DefaultImageSize = new Size(16, 16);

        private const int INITIAL_CAPACITY = 4;
        private const int GROWBY = 4;

        // Native handle is no longer used; keep field for compatibility but unused
        private NativeImageList nativeImageList;

        // private int himlTemp;
        // private Bitmap temp = null;  // Used for drawing

        private ColorDepth colorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
        private Color transparentColor = Color.Transparent;
        private Size imageSize = DefaultImageSize;

        private ImageCollection imageCollection;

        private object userData;

        // The usual handle virtualization problem, with a new twist: image
        // lists are lossy.  At runtime, we delay handle creation as long as possible, and store
        // away the original images until handle creation (and hope no one disposes of the images!).  At design time, we keep the originals around indefinitely.
        // This variable will become null when the original images are lost. See ASURT 65162.
        private IList /* of Original */ originals = new ArrayList();
        private EventHandler recreateHandler = null;
        private EventHandler changeHandler = null;

        private bool inAddRange = false;

        // In-memory storage for images
        private readonly List<Bitmap> _images = new List<Bitmap>();

        /// <include file='doc\ImageList.uex' path='docs/doc[@for="ImageList.ImageList"]/*' />
        /// <devdoc>
        ///     Creates a new ImageList Control with a default image size of 16x16
        ///     pixels
        /// </devdoc>
        public ImageList()
        { // DO NOT DELETE -- AUTOMATION BP 1
        }

        // Normalize any incoming image to the list's storage format (size, transparency)
        private Bitmap NormalizeToStorageImage(Image src)
        {
            if (src == null) throw new ArgumentNullException(nameof(src));
            Bitmap bmp;
            if (src is Bitmap b)
            {
                bmp = (Bitmap)b.Clone();
            }
            else
            {
                bmp = new Bitmap(src);
            }
            // Resize if needed
            if (bmp.Width != imageSize.Width || bmp.Height != imageSize.Height)
            {
                var resized = new Bitmap(imageSize.Width, imageSize.Height);
                using (var g = Graphics.FromImage(resized))
                {
                    g.DrawImage(bmp, new Rectangle(0, 0, imageSize.Width, imageSize.Height));
                }
                bmp.Dispose();
                bmp = resized;
            }
            // Apply transparency if configured
            if (UseTransparentColor && transparentColor.A > 0)
            {
                bmp.MakeTransparent(transparentColor);
            }
            return bmp;
        }

        /// <include file='doc\ImageList.uex' path='docs/doc[@for="ImageList.ImageList1"]/*' />
        /// <devdoc>
        ///     Creates a new ImageList Control with a default image size of 16x16
        ///     pixels and adds the ImageList to the passed in container.
        /// </devdoc>
        public ImageList(IContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            container.Add(this);
        }

        // This class is for classes that want to support both an ImageIndex
        // and ImageKey.  We want to toggle between using keys or indexes.
        // Default is to use the integer index.
        internal class Indexer
        {
            private string key = String.Empty;
            private int index = -1;
            private bool useIntegerIndex = true;
            private ImageList imageList = null;

            public virtual ImageList ImageList
            {
                get { return imageList; }
                set { imageList = value; }
            }

            public virtual string Key
            {
                get { return key; }
                set
                {
                    index = -1;
                    key = (value == null ? String.Empty : value);
                    useIntegerIndex = false;
                }
            }

            public virtual int Index
            {
                get { return index; }
                set
                {
                    key = String.Empty;
                    index = value;
                    useIntegerIndex = true;
                }

            }

            public virtual int ActualIndex
            {
                get
                {
                    if (useIntegerIndex)
                    {
                        return Index;
                    }
                    else if (ImageList != null)
                    {
                        return ImageList.Images.IndexOfKey(Key);
                    }

                    return -1;
                }
            }
        }

        /// <include file='doc\ImageList.uex' path='docs/doc[@for="ImageList.ColorDepth"]/*' />
        /// <devdoc>
        ///     Retrieves the color depth of the imagelist.
        /// </devdoc>
        [
        SRCategory(DCSR.CatAppearance),
        SRDescription(DCSR.ImageListColorDepthDescr)
        ]
        public ColorDepth ColorDepth
        {
            get
            {
                return colorDepth;
            }
            set
            {
                // ColorDepth is not conitguous - list the members instead.
                if (!ClientUtils.IsEnumValid_NotSequential(value,
                                                     (int)value,
                                                    (int)ColorDepth.Depth4Bit,
                                                    (int)ColorDepth.Depth8Bit,
                                                    (int)ColorDepth.Depth16Bit,
                                                    (int)ColorDepth.Depth24Bit,
                                                    (int)ColorDepth.Depth32Bit))
                {
                    throw new InvalidEnumArgumentException("value", (int)value, typeof(ColorDepth));
                }

                if (colorDepth != value)
                {
                    colorDepth = value;
                    PerformRecreateHandle("ColorDepth");
                }
            }
        }

        private bool ShouldSerializeColorDepth()
        {
            return (Images.Count == 0);
        }
        private void ResetColorDepth()
        {
            ColorDepth = ColorDepth.Depth8Bit;
        }

        /// <include file='doc\ImageList.uex' path='docs/doc[@for="ImageList.Handle"]/*' />
        /// <devdoc>
        ///     The handle of the ImageList object.  This corresponds to a win32
        ///     HIMAGELIST Handle.
        /// </devdoc>
        [
        Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
        SRDescription(DCSR.ImageListHandleDescr)
        ]
        public IntPtr Handle
        {
            get
            {
                // No native handle in memory-only implementation
                return IntPtr.Zero;
            }
        }

        /// <include file='doc\ImageList.uex' path='docs/doc[@for="ImageList.HandleCreated"]/*' />
        /// <devdoc>
        ///     Whether or not the underlying Win32 handle has been created.
        /// </devdoc>
        [
        Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
        SRDescription(DCSR.ImageListHandleCreatedDescr)
        ]
        public bool HandleCreated { get { return false; } }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        [
        SRCategory(DCSR.CatAppearance),
        DefaultValue(null),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
        SRDescription(DCSR.ImageListImagesDescr),
        MergableProperty(false)
        ]
        public ImageCollection Images
        {
            get
            {
                if (imageCollection == null)
                    imageCollection = new ImageCollection(this);
                return imageCollection;
            }
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        [
        SRCategory(DCSR.CatBehavior),
        Localizable(true),
        SRDescription(DCSR.ImageListSizeDescr)
        ]
        public Size ImageSize
        {
            get
            {
                return imageSize;
            }
            set
            {
                if (value.IsEmpty)
                {
                    throw new ArgumentException(DCSR.GetString(DCSR.InvalidArgument, "ImageSize", "Size.Empty"));
                }

                // ImageList appears to consume an exponential amount of memory
                // based on image size x bpp.  Restrict this to a reasonable maximum
                // to keep people's systems from crashing.
                //
                if (value.Width <= 0 || value.Width > 256)
                {
                    throw new ArgumentOutOfRangeException("ImageSize", DCSR.GetString(DCSR.InvalidBoundArgument, "ImageSize.Width", value.Width.ToString(CultureInfo.CurrentCulture), (1).ToString(CultureInfo.CurrentCulture), "256"));
                }

                if (value.Height <= 0 || value.Height > 256)
                {
                    throw new ArgumentOutOfRangeException("ImageSize", DCSR.GetString(DCSR.InvalidBoundArgument, "ImageSize.Height", value.Height.ToString(CultureInfo.CurrentCulture), (1).ToString(CultureInfo.CurrentCulture), "256"));
                }

                if (imageSize.Width != value.Width || imageSize.Height != value.Height)
                {
                    imageSize = new Size(value.Width, value.Height);
                    PerformRecreateHandle("ImageSize");
                }
            }
        }

        private bool ShouldSerializeImageSize()
        {
            return (Images.Count == 0);
        }

        /// <include file='doc\ImageList.uex' path='docs/doc[@for="ImageList.ImageStream"]/*' />
        /// <devdoc>
        ///     Returns an ImageListStreamer, or null if the image list is empty.
        /// </devdoc>
        [
        Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced),
        DefaultValue(null),
        SRDescription(DCSR.ImageListImageStreamDescr)
        ]
        public ImageListStreamer ImageStream
        {
            get
            {
                // In-memory implementation does not expose a streamer
                return null;
            }
            set
            {
                // Ignore non-null assignments in memory-only mode; allow clearing when set to null
                if (value == null) { Images.Clear(); }

            }
        }

        /// <include file='doc\ImageList.uex' path='docs/doc[@for="ImageList.Tag"]/*' />
        [
        SRCategory(DCSR.CatData),
        Localizable(false),
        Bindable(true),
        SRDescription(DCSR.ControlTagDescr),
        DefaultValue(null),
        TypeConverter(typeof(StringConverter)),
        ]
        public object Tag
        {
            get
            {
                return userData;
            }
            set
            {
                userData = value;
            }
        }

        /// <include file='doc\ImageList.uex' path='docs/doc[@for="ImageList.TransparentColor"]/*' />
        /// <devdoc>
        ///     The color to treat as transparent.
        /// </devdoc>
        [
        SRCategory(DCSR.CatBehavior),
        SRDescription(DCSR.ImageListTransparentColorDescr)
        ]
        public Color TransparentColor
        {
            get
            {
                return transparentColor;
            }
            set
            {
                transparentColor = value;
            }
        }

        // Whether to use the transparent color, or rely on alpha instead
        private bool UseTransparentColor
        {
            get { return TransparentColor.A > 0; }
        }


        /// <include file='doc\ImageList.uex' path='docs/doc[@for="ImageList.RecreateHandle"]/*' />
        /// <devdoc>
        ///    <para>[To be supplied.]</para>
        /// </devdoc>
        [
        Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced),
        SRDescription(DCSR.ImageListOnRecreateHandleDescr)
        ]
        public event EventHandler RecreateHandle
        {
            add
            {
                recreateHandler += value;
            }
            remove
            {
                recreateHandler -= value;
            }
        }

        internal event EventHandler ChangeHandle
        {
            add
            {
                changeHandler += value;
            }
            remove
            {
                changeHandler -= value;
            }
        }

        //Creates a bitmap from the original image source..
        //

        [ResourceExposure(ResourceScope.Machine)]
        [ResourceConsumption(ResourceScope.Machine)]
        private Bitmap CreateBitmap(Original original, out bool ownsBitmap)
        {
            Color transparent = transparentColor;
            ownsBitmap = false;
            if ((original.options & OriginalOptions.CustomTransparentColor) != 0)
                transparent = original.customTransparentColor;

            Bitmap bitmap;
            if (original.image is Bitmap)
            {
                bitmap = (Bitmap)original.image;
            }
            else if (original.image is Icon)
            {
                bitmap = ((Icon)original.image).ToBitmap();
                ownsBitmap = true;
            }
            else
            {
                bitmap = new Bitmap((Image)original.image);
                ownsBitmap = true;
            }

            if (transparent.A > 0)
            {
                // ImageList_AddMasked doesn't work on high color bitmaps,
                // so we always create the mask ourselves
                Bitmap source = bitmap;
                bitmap = (Bitmap)bitmap.Clone();
                bitmap.MakeTransparent(transparent);
                if (ownsBitmap)
                    source.Dispose();
                ownsBitmap = true;
            }

            Size size = bitmap.Size;
            if ((original.options & OriginalOptions.ImageStrip) != 0)
            {
                // strip width must be a positive multiple of image list width
                if (size.Width == 0 || (size.Width % imageSize.Width) != 0)
                    throw new ArgumentException(DCSR.GetString(DCSR.ImageListStripBadWidth), "original");
                if (size.Height != imageSize.Height)
                    throw new ArgumentException(DCSR.GetString(DCSR.ImageListImageTooShort), "original");
            }
            else if (!size.Equals(ImageSize))
            {
                Bitmap source = bitmap;
                bitmap = new Bitmap(source, ImageSize);
                if (ownsBitmap)
                    source.Dispose();
                ownsBitmap = true;
            }
            return bitmap;

        }

        private int AddIconToHandle(Original original, Icon icon)
        {
            try
            {
                Debug.Assert(HandleCreated, "Calling AddIconToHandle when there is no handle");
                int index = WinFormSafeNativeMethods.ImageList_ReplaceIcon(new HandleRef(this, Handle), -1, new HandleRef(icon, icon.Handle));
                if (index == -1) throw new InvalidOperationException(DCSR.GetString(DCSR.ImageListAddFailed));
                return index;
            }
            finally
            {
                if ((original.options & OriginalOptions.OwnsImage) != 0)
                { /// this is to handle the case were we clone the icon (see WHY WHY WHY below)
                    icon.Dispose();
                }
            }
        }
        // Adds bitmap to the Imagelist handle...
        //
        private int AddToHandle(Original original, Bitmap bitmap)
        {

            Debug.Assert(HandleCreated, "Calling AddToHandle when there is no handle");
            IntPtr hMask = ControlPaint.CreateHBitmapTransparencyMask(bitmap);   // Calls GDI to create Bitmap.
            IntPtr hBitmap = ControlPaint.CreateHBitmapColorMask(bitmap, hMask); // Calls GDI+ to create Bitmap. Need to add handle to HandleCollector.
            int index = WinFormSafeNativeMethods.ImageList_Add(new HandleRef(this, Handle), new HandleRef(null, hBitmap), new HandleRef(null, hMask));
            WinFormSafeNativeMethods.DeleteObject(new HandleRef(null, hBitmap));
            WinFormSafeNativeMethods.DeleteObject(new HandleRef(null, hMask));

            if (index == -1) throw new InvalidOperationException(DCSR.GetString(DCSR.ImageListAddFailed));
            return index;
        }

        /// <include file='doc\ImageList.uex' path='docs/doc[@for="ImageList.CreateHandle"]/*' />
        /// <devdoc>
        ///     Creates the underlying HIMAGELIST handle, and sets up all the
        ///     appropriate values with it.  Inheriting classes overriding this method
        ///     should not forget to call base.createHandle();
        /// </devdoc>
        private void CreateHandle() { /* no-op in memory-only mode */ }

        // Don't merge this function into Dispose() -- that base.Dispose() will damage the design time experience
        private void DestroyHandle() { /* no-op in memory-only mode */ }

        /// <include file='doc\ImageList.uex' path='docs/doc[@for="ImageList.Dispose"]/*' />
        /// <devdoc>
        ///     Frees all resources assocaited with this component.
        /// </devdoc>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (originals != null)
                { // we might own some of the stuff that's not been created yet
                    foreach (Original original in originals)
                    {
                        if ((original.options & OriginalOptions.OwnsImage) != 0)
                        {
                            ((IDisposable)original.image).Dispose();
                        }
                    }
                }
                DestroyHandle();
            }
            base.Dispose(disposing);
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void Draw(Graphics g, Point pt, int index)
        {
            Draw(g, pt.X, pt.Y, index);
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void Draw(Graphics g, int x, int y, int index)
        {
            Draw(g, x, y, imageSize.Width, imageSize.Height, index);
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void Draw(Graphics g, int x, int y, int width, int height, int index)
        {
            if (index < 0 || index >= Images.Count)
                throw new ArgumentOutOfRangeException("index", DCSR.GetString(DCSR.InvalidArgument, "index", index.ToString(CultureInfo.CurrentCulture)));
            var img = this.Images[index];
            // Use GDI+ drawing instead of native ImageList drawing
            img.CreateBlobUrl(true);
            g.DrawImage(img, new Rectangle(x, y, width, height));
            //var bs = img.ToBinary();
            //Console.WriteLine(Convert.ToBase64String(bs));

            // Old native drawing implementation (kept for reference)
            //IntPtr dc = g.GetHdc();
            //try {
            //    WinFormSafeNativeMethods.ImageList_DrawEx(new HandleRef(this, Handle), index, new HandleRef(g, dc), x, y,
            //                           width, height, WinFormNativeMethods.CLR_NONE, WinFormNativeMethods.CLR_NONE, WinFormNativeMethods.ILD_TRANSPARENT);
            //}
            //finally {
            //    g.ReleaseHdcInternal(dc);
            //}
        }


        private void CopyBitmapData(BitmapData sourceData, BitmapData targetData)
        {
            // do the actual copy
            int offsetSrc = 0;
            int offsetDest = 0;
            unsafe
            {
                for (int i = 0; i < targetData.Height; i++)
                {
                    IntPtr srcPtr, destPtr;
                    if (IntPtr.Size == 4)
                    {
                        srcPtr = new IntPtr(sourceData.Scan0.ToInt32() + offsetSrc);
                        destPtr = new IntPtr(targetData.Scan0.ToInt32() + offsetDest);
                    }
                    else
                    {
                        srcPtr = new IntPtr(sourceData.Scan0.ToInt64() + offsetSrc);
                        destPtr = new IntPtr(targetData.Scan0.ToInt64() + offsetDest);
                    }
                    WinFormUnsafeNativeMethods.CopyMemory(new HandleRef(this, destPtr), new HandleRef(this, srcPtr), Math.Abs(targetData.Stride));
                    offsetSrc += sourceData.Stride;
                    offsetDest += targetData.Stride;
                }
            }
        }

        private static bool BitmapHasAlpha(BitmapData bmpData)
        {
            if (bmpData.PixelFormat != PixelFormat.Format32bppArgb && bmpData.PixelFormat != PixelFormat.Format32bppRgb)
            {
                return false;
            }
            bool hasAlpha = false;
            unsafe
            {
                for (int i = 0; i < bmpData.Height; i++)
                {
                    int offsetRow = i * bmpData.Stride;
                    for (int j = 3; j < bmpData.Width * 4; j += 4)
                    { // *4 is safe since we know PixelFormat is ARGB
                        unsafe
                        {
                            byte* candidate = ((byte*)bmpData.Scan0.ToPointer()) + offsetRow + j;
                            if (*candidate != 0)
                            {
                                hasAlpha = true;
                                goto Found; // gotos are fugly but it's the best thing here...
                            }
                        }
                    }
                }
            Found:
                return hasAlpha;
            }
        }

        /// <include file='doc\ImageList.uex' path='docs/doc[@for="ImageList.GetBitmap"]/*' />
        /// <devdoc>
        ///     Returns the image specified by the given index.  The bitmap returned is a
        ///     copy of the original image.
        /// </devdoc>
        // NOTE: forces handle creation, so doesn't return things from the original list
        [ResourceExposure(ResourceScope.Machine)]
        [ResourceConsumption(ResourceScope.Machine | ResourceScope.Process, ResourceScope.Machine | ResourceScope.Process)]
        private Bitmap GetBitmap(int index)
        {
            if (index < 0 || index >= Images.Count)
                throw new ArgumentOutOfRangeException("index", DCSR.GetString(DCSR.InvalidArgument, "index", index.ToString(CultureInfo.CurrentCulture)));
            Bitmap result = null;
            var imgResult = this._images[index];
            imgResult.CreateBlobUrl(true);
            return imgResult;
            // if the imagelist is 32bpp, if the image slot at index
            // has valid alpha information (not all zero... which is cause by windows just painting RGB values
            // and not touching the alpha byte for images < 32bpp painted to a 32bpp imagelist)
            // we're not using the mask. That means that
            // we can just get the whole image strip, cut out the piece that we want
            // and return that, that way we don't flatten the alpha by painting the value with the alpha... (ie using the alpha)

            if (ColorDepth == ColorDepth.Depth32Bit)
            {

                WinFormNativeMethods.IMAGEINFO imageInfo = new WinFormNativeMethods.IMAGEINFO(); // review? do I need to delete the mask and image inside of imageinfo?
                if (WinFormSafeNativeMethods.ImageList_GetImageInfo(new HandleRef(this, this.Handle), index, imageInfo))
                {
                    Bitmap tmpBitmap = null;
                    BitmapData bmpData = null;
                    BitmapData targetData = null;
                    //IntSecurity.ObjectFromWin32Handle.Assert();
                    try
                    {
                        tmpBitmap = Bitmap.FromHbitmap(imageInfo.hbmImage);
                        // 




                        bmpData = tmpBitmap.LockBits(new Rectangle(imageInfo.rcImage_left, imageInfo.rcImage_top, imageInfo.rcImage_right - imageInfo.rcImage_left, imageInfo.rcImage_bottom - imageInfo.rcImage_top), ImageLockMode.ReadOnly, tmpBitmap.PixelFormat);

                        int offset = bmpData.Stride * imageSize.Height * index;
                        // we need do the following if the image has alpha because otherwise the image is fully transparent even though it has data
                        if (BitmapHasAlpha(bmpData))
                        {
                            result = new Bitmap(imageSize.Width, imageSize.Height, PixelFormat.Format32bppArgb);
                            targetData = result.LockBits(new Rectangle(0, 0, imageSize.Width, imageSize.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
                            CopyBitmapData(bmpData, targetData);
                        }
                    }
                    finally
                    {
                        //CodeAccessPermission.RevertAssert();
                        if (tmpBitmap != null)
                        {
                            if (bmpData != null)
                            {
                                tmpBitmap.UnlockBits(bmpData);
                            }
                            tmpBitmap.Dispose();
                        }
                        if (result != null && targetData != null)
                        {
                            result.UnlockBits(targetData);
                        }
                    }
                }
            }

            if (result == null)
            { // paint with the mask but no alpha...
                result = new Bitmap(imageSize.Width, imageSize.Height);

                Graphics graphics = Graphics.FromImage(result);
                try
                {
                    IntPtr dc = graphics.GetHdc();
                    try
                    {
                        WinFormSafeNativeMethods.ImageList_DrawEx(new HandleRef(this, Handle), index, new HandleRef(graphics, dc), 0, 0,
                                                imageSize.Width, imageSize.Height, WinFormNativeMethods.CLR_NONE, WinFormNativeMethods.CLR_NONE, WinFormNativeMethods.ILD_TRANSPARENT);

                    }
                    finally
                    {
                        graphics.ReleaseHdcInternal(dc);
                    }
                }
                finally
                {
                    graphics.Dispose();
                }
            }

            // gpr: See Icon for description of fakeTransparencyColor
            result.MakeTransparent(fakeTransparencyColor);
            return result;
        }



#if DEBUG_ONLY_APIS
        /// <include file='doc\ImageList.uex' path='docs/doc[@for="ImageList.DebugOnly_GetMasterImage"]/*' />
        /// <devdoc>
        ///    <para>[To be supplied.]</para>
        /// </devdoc>
        public Bitmap DebugOnly_GetMasterImage() {
            if (Images.Empty)
                return null;

            return Image.FromHBITMAP(GetImageInfo(0).hbmImage);
        }

        /// <include file='doc\ImageList.uex' path='docs/doc[@for="ImageList.DebugOnly_GetMasterMask"]/*' />
        /// <devdoc>
        ///    <para>[To be supplied.]</para>
        /// </devdoc>
        public Bitmap DebugOnly_GetMasterMask() {
            if (Images.Empty)
                return null;

            return Image.FromHBITMAP(GetImageInfo(0).hbmMask);
        }
#endif // DEBUG_ONLY_APIS

        /// <include file='doc\ImageList.uex' path='docs/doc[@for="ImageList.OnRecreateHandle"]/*' />
        /// <devdoc>
        ///     Called when the Handle property changes.
        /// </devdoc>
        private void OnRecreateHandle(EventArgs eventargs)
        {
            if (recreateHandler != null)
            {
                recreateHandler(this, eventargs);
            }
        }

        private void OnChangeHandle(EventArgs eventargs)
        {
            if (changeHandler != null)
            {
                changeHandler(this, eventargs);
            }
        }

#if false
        /// <include file='doc\ImageList.uex' path='docs/doc[@for="ImageList.PutImageInTempBitmap"]/*' />
        /// <devdoc>
        ///     Copies the image at the specified index into the temporary Bitmap object.
        ///     The temporary Bitmap object is used for stuff that the Windows ImageList
        ///     control doesn't support, such as stretching images or copying images from
        ///     different image lists.  Since bitmap creation is expensive, the same instance
        ///     of the temporary Bitmap is reused.
        /// </devdoc>
        private void PutImageInTempBitmap(int index, bool useSnapshot) {
            Debug.Assert(!useSnapshot || himlTemp != 0, "Where's himlTemp?");

            IntPtr handleUse = (useSnapshot ? himlTemp : Handle);
            int count = SafeNativeMethods.ImageList_GetImageCount(handleUse);

            if (index < 0 || index >= count)
                throw new ArgumentOutOfRangeException("index", SR.GetString(SR.InvalidArgument, "index", (index).ToString()));

            if (temp != null) {
                Size size = temp.Size;
                if (!temp.Size.Equals(imageSize)) {
                    temp.Dispose();
                    temp = null;
                }
            }
            if (temp == null) {
                temp = new Bitmap(imageSize.Width, imageSize.Height);
            }

            temp.Transparent = useMask;
            // OldGraphics gTemp = /*gpr useMask ? temp.ColorMask.GetGraphics() :*/ temp.GetGraphics();
            SafeNativeMethods.ImageList_DrawEx(handleUse, index, gTemp.Handle, 0, 0,
                                    imageSize.Width, imageSize.Height, useMask ? 0 : NativeMethods.CLR_DEFAULT, NativeMethods.CLR_NONE, NativeMethods.ILD_NORMAL);

            if (useMask) {
                gTemp = temp/*gpr .MonochromeMask*/.GetGraphics();
                SafeNativeMethods.ImageList_DrawEx(handleUse, index, gTemp.Handle, 0, 0, imageSize.Width, imageSize.Height, NativeMethods.CLR_DEFAULT, NativeMethods.CLR_NONE, NativeMethods.ILD_MASK);
            }
        }
#endif

        // PerformRecreateHandle doesn't quite do what you would suspect.
        // Any existing images in the imagelist will NOT be copied to the
        // new image list -- they really should. This bug has existed for a
        // loooong time.
        // The net effect is that if you add images to an imagelist, and
        // then e.g. change the ImageSize any existing images will be lost
        // and you will have to add them back. This is probably a corner case
        // but it should be mentioned.
        //
        // The fix isn't as straightforward as you might think, i.e. we
        // cannot just blindly store off the images and copy them into
        // the newly created imagelist. E.g. say you change the ColorDepth
        // from 8-bit to 32-bit. Just copying the 8-bit images would be wrong.
        // Therefore we are going to leave this as is. Users should make sure
        // to set these properties before actually adding the images.

        // The Designer works around this by shadowing any Property that ends
        // up calling PerformRecreateHandle (ImageSize, ColorDepth, ImageStream).

        // Thus, if you add a new Property to ImageList which ends up calling
        // PerformRecreateHandle, you must shadow the property in ImageListDesigner.
        private void PerformRecreateHandle(string reason) { /* no-op in memory-only mode */ }

        private void ResetImageSize()
        {
            ImageSize = DefaultImageSize;
        }

        private void ResetTransparentColor()
        {
            TransparentColor = Color.LightGray;
        }

        private bool ShouldSerializeTransparentColor()
        {
            return !TransparentColor.Equals(Color.LightGray);
        }


        /// <include file='doc\ImageList.uex' path='docs/doc[@for="ImageList.ToString"]/*' />
        /// <devdoc>
        ///     Returns a string representation for this control.
        /// </devdoc>
        /// <internalonly/>
        public override string ToString()
        {
            string s = base.ToString();
            if (Images != null)
            {
                return s + " Images.Count: " + Images.Count.ToString(CultureInfo.CurrentCulture) + ", ImageSize: " + ImageSize.ToString();
            }
            else
            {
                return s;
            }
        }

        internal class NativeImageList : IDisposable
        {
            private IntPtr himl;
#if DEBUG
            private string callStack;
#endif

            internal NativeImageList(IntPtr himl)
            {
                this.himl = himl;
#if DEBUG
                //new EnvironmentPermission(PermissionState.Unrestricted).Assert();
                try {
                    callStack = Environment.StackTrace;
                }
                finally {
                    //CodeAccessPermission.RevertAssert();
                }
#endif
            }

            internal IntPtr Handle
            {
                get
                {
                    return himl;
                }
            }

            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            public void Dispose(bool disposing)
            {
                if (himl != IntPtr.Zero)
                {
                    WinFormSafeNativeMethods.ImageList_Destroy(new HandleRef(null, himl));
                    himl = IntPtr.Zero;
                }
            }

            ~NativeImageList()
            {
                Dispose(false);
            }

        }

        // An image before we add it to the image list, along with a few details about how to add it.
        private class Original
        {
            internal object image;
            internal OriginalOptions options;
            internal Color customTransparentColor = Color.Transparent;

            internal int nImages = 1;

            internal Original(object image, OriginalOptions options)
            : this(image, options, Color.Transparent)
            {
            }

            internal Original(object image, OriginalOptions options, int nImages)
            : this(image, options, Color.Transparent)
            {
                this.nImages = nImages;
            }

            internal Original(object image, OriginalOptions options, Color customTransparentColor)
            {
                Debug.Assert(image != null, "image is null");
                if (!(image is Icon) && !(image is Image))
                {
                    throw new InvalidOperationException(DCSR.GetString(DCSR.ImageListEntryType));
                }
                this.image = image;
                this.options = options;
                this.customTransparentColor = customTransparentColor;
                if ((options & OriginalOptions.CustomTransparentColor) == 0)
                {
                    Debug.Assert(customTransparentColor.Equals(Color.Transparent),
                                 "Specified a custom transparent color then told us to ignore it");
                }
            }
        }

        [Flags]
        private enum OriginalOptions
        {
            Default = 0x00,

            ImageStrip = 0x01,
            CustomTransparentColor = 0x02,
            OwnsImage = 0x04
        }





        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = false)]
        [
        Editor("System.Windows.Forms.Design.ImageCollectionEditor, " + AssemblyRef.SystemDesign, typeof(UITypeEditor))
        ]
        public sealed class ImageCollection : IList
        {
            private ImageList owner;
            private ArrayList imageInfoCollection = new ArrayList();

            /// A caching mechanism for key accessor
            /// We use an index here rather than control so that we don't have lifetime
            /// issues by holding on to extra references.
            private int lastAccessedIndex = -1;

            [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
            public StringCollection Keys
            {
                get
                {
                    // pass back a copy of the current state.
                    StringCollection keysCollection = new StringCollection();

                    for (int i = 0; i < imageInfoCollection.Count; i++)
                    {
                        ImageInfo image = imageInfoCollection[i] as ImageInfo;
                        if ((image != null) && (image.Name != null) && (image.Name.Length != 0))
                        {
                            keysCollection.Add(image.Name);
                        }
                        else
                        {
                            keysCollection.Add(string.Empty);
                        }
                    }
                    return keysCollection;
                }
            }
            internal ImageCollection(ImageList owner)
            {
                this.owner = owner;
            }

            internal void ResetKeys()
            {
                if (imageInfoCollection != null)
                    imageInfoCollection.Clear();

                for (int i = 0; i < this.Count; i++)
                {
                    imageInfoCollection.Add(new ImageCollection.ImageInfo());
                }
            }

            [Conditional("DEBUG")]
            private void AssertInvariant()
            {
                Debug.Assert(owner != null, "ImageCollection has no owner (ImageList)");
                Debug.Assert((owner.originals == null) == (owner.HandleCreated), " Either we should have the original images, or the handle should be created");
            }

            [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
            [Browsable(false)]
            public int Count { [ResourceExposure(ResourceScope.None)] get { return owner._images.Count; } }

            /// <include file='doc\ImageList.uex' path='docs/doc[@for="ImageCollection.ICollection.SyncRoot"]/*' />
            /// <internalonly/>
            object ICollection.SyncRoot
            {
                get
                {
                    return this;
                }
            }

            /// <include file='doc\ImageList.uex' path='docs/doc[@for="ImageCollection.ICollection.IsSynchronized"]/*' />
            /// <internalonly/>
            bool ICollection.IsSynchronized
            {
                get
                {
                    return false;
                }
            }

            /// <include file='doc\ImageList.uex' path='docs/doc[@for="ImageCollection.IList.IsFixedSize"]/*' />
            /// <internalonly/>
            bool IList.IsFixedSize
            {
                get
                {
                    return false;
                }
            }

            [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
            public bool IsReadOnly
            {
                get
                {
                    return false;
                }
            }

            [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
            public bool Empty
            {
                get
                {
                    return Count == 0;
                }
            }

            [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
            [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
            public Image this[int index]
            {
                [ResourceExposure(ResourceScope.Machine)]
                [ResourceConsumption(ResourceScope.Machine)]
                get
                {
                    if (index < 0 || index >= Count)
                        throw new ArgumentOutOfRangeException("index", DCSR.GetString(DCSR.InvalidArgument, "index", index.ToString(CultureInfo.CurrentCulture)));
                    return owner.GetBitmap(index);
                }
                set
                {
                    if (index < 0 || index >= Count)
                        throw new ArgumentOutOfRangeException("index", DCSR.GetString(DCSR.InvalidArgument, "index", index.ToString(CultureInfo.CurrentCulture)));

                    if (value == null)
                    {
                        throw new ArgumentNullException("value");
                    }

                    Bitmap normalized = owner.NormalizeToStorageImage(value);
                    owner._images[index] = normalized;
                    owner.OnChangeHandle(EventArgs.Empty);
                }
            }

            [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
            object IList.this[int index]
            {
                [ResourceExposure(ResourceScope.Machine)]
                [ResourceConsumption(ResourceScope.Machine)]
                get
                {
                    return this[index];
                }
                set
                {
                    if (value is Image)
                    {
                        this[index] = (Image)value;
                    }
                    else
                    {
                        throw new ArgumentException(DCSR.GetString(DCSR.ImageListBadImage), "value");
                    }
                }
            }

            [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
            public Image this[string key]
            {
                [ResourceExposure(ResourceScope.Machine)]
                [ResourceConsumption(ResourceScope.Machine)]
                get
                {
                    // We do not support null and empty string as valid keys.
                    if ((key == null) || (key.Length == 0))
                    {
                        return null;
                    }

                    // Search for the key in our collection
                    int index = IndexOfKey(key);
                    if (IsValidIndex(index))
                    {
                        return this[index];
                    }
                    else
                    {
                        return null;
                    }

                }
            }


            [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
            public void Add(string key, Image image)
            {
                Debug.Assert((this.Count == imageInfoCollection.Count), "The count of these two collections should be equal.");
                Add(image);
                // set key name for last added
                if (imageInfoCollection.Count > 0)
                {
                    imageInfoCollection[imageInfoCollection.Count - 1] = new ImageInfo { Name = key };
                }

            }
            [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
            public void Add(string key, Icon icon)
            {
                Debug.Assert((this.Count == imageInfoCollection.Count), "The count of these two collections should be equal.");
                Add(icon);
                if (imageInfoCollection.Count > 0)
                {
                    imageInfoCollection[imageInfoCollection.Count - 1] = new ImageInfo { Name = key };
                }


            }

            [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
            int IList.Add(object value)
            {
                if (value is Image)
                {
                    Add((Image)value);
                    return Count - 1;
                }
                else
                {
                    throw new ArgumentException(DCSR.GetString(DCSR.ImageListBadImage), "value");
                }
            }

            [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
            public void Add(Icon value)
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                // Normalize and store
                using (var bmp = value.ToBitmap())
                {
                    Add((Image)bmp.Clone());
                }
            }

            [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
            public void Add(Image value)
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                Bitmap normalized = owner.NormalizeToStorageImage(value);
                owner._images.Add(normalized);
                imageInfoCollection.Add(new ImageInfo());
                if (!owner.inAddRange) owner.OnChangeHandle(EventArgs.Empty);
            }

            [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
            public int Add(Image value, Color transparentColor)
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                var old = owner.transparentColor;
                try
                {
                    owner.transparentColor = transparentColor;
                    Add(value);
                    return Count - 1;
                }
                finally { owner.transparentColor = old; }
            }
            private int Add(Original original, ImageInfo imageInfo) { throw new NotSupportedException(); }
            [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
            public void AddRange(Image[] images)
            {
                if (images == null)
                {
                    throw new ArgumentNullException("images");
                }
                owner.inAddRange = true;
                foreach (Image image in images)
                {
                    Add(image);
                }
                owner.inAddRange = false;
                owner.OnChangeHandle(new EventArgs());
            }

            [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
            public int AddStrip(Image value)
            {

                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }

                // strip width must be a positive multiple of image list width
                //
                if (value.Width == 0 || (value.Width % owner.ImageSize.Width) != 0)
                    throw new ArgumentException(DCSR.GetString(DCSR.ImageListStripBadWidth), "value");
                if (value.Height != owner.ImageSize.Height)
                    throw new ArgumentException(DCSR.GetString(DCSR.ImageListImageTooShort), "value");

                int nImages = value.Width / owner.ImageSize.Width;
                int w = owner.ImageSize.Width;
                int h = owner.ImageSize.Height;
                using (var strip = new Bitmap(value))
                {
                    for (int i = 0; i < nImages; i++)
                    {
                        var rect = new Rectangle(i * w, 0, w, h);
                        var bmp = new Bitmap(w, h);
                        using (var g = Graphics.FromImage(bmp))
                        {
                            g.DrawImage(strip, new Rectangle(0, 0, w, h), rect, GraphicsUnit.Pixel);
                        }
                        if (owner.UseTransparentColor && owner.transparentColor.A > 0) bmp.MakeTransparent(owner.transparentColor);
                        owner._images.Add(bmp);
                        imageInfoCollection.Add(new ImageInfo());
                    }
                }
                if (!owner.inAddRange) owner.OnChangeHandle(EventArgs.Empty);
                return Count - 1;
            }

            [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
            public void Clear()
            {
                owner._images.Clear();
                imageInfoCollection.Clear();
                owner.OnChangeHandle(new EventArgs());
            }
            [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
            [EditorBrowsable(EditorBrowsableState.Never)]
            public bool Contains(Image image)
            {
                throw new NotSupportedException();
            }
            [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
            bool IList.Contains(object image)
            {
                if (image is Image)
                {
                    return Contains((Image)image);
                }
                else
                {
                    return false;
                }
            }

            [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
            public bool ContainsKey(string key)
            {
                return IsValidIndex(IndexOfKey(key));
            }

            [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
            [EditorBrowsable(EditorBrowsableState.Never)]
            public int IndexOf(Image image)
            {
                throw new NotSupportedException();
            }

            [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
            int IList.IndexOf(object image)
            {
                if (image is Image)
                {
                    return IndexOf((Image)image);
                }
                else
                {
                    return -1;
                }
            }

            [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
            public int IndexOfKey(String key)
            {
                // Step 0 - Arg validation
                if ((key == null) || (key.Length == 0))
                {
                    return -1; // we dont support empty or null keys.
                }


                // step 1 - check the last cached item
                if (IsValidIndex(lastAccessedIndex))
                {
                    if ((imageInfoCollection[lastAccessedIndex] != null) &&
                        (WindowsFormsUtils.SafeCompareStrings(((ImageInfo)imageInfoCollection[lastAccessedIndex]).Name, key, /* ignoreCase = */ true)))
                    {
                        return lastAccessedIndex;
                    }
                }

                // step 2 - search for the item
                for (int i = 0; i < this.Count; i++)
                {
                    if ((imageInfoCollection[i] != null) &&
                            (WindowsFormsUtils.SafeCompareStrings(((ImageInfo)imageInfoCollection[i]).Name, key, /* ignoreCase = */ true)))
                    {
                        lastAccessedIndex = i;
                        return i;
                    }
                }

                // step 3 - we didn't find it.  Invalidate the last accessed index and return -1.
                lastAccessedIndex = -1;
                return -1;
            }


            [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
            void IList.Insert(int index, object value)
            {
                throw new NotSupportedException();
            }

            /// <include file='doc\ImageList.uex' path='docs/doc[@for="ImageCollection.IsValidIndex"]/*' />
            /// <devdoc>
            ///     <para>Determines if the index is valid for the collection.</para>
            /// </devdoc>
            /// <internalonly/>
            private bool IsValidIndex(int index)
            {
                return ((index >= 0) && (index < this.Count));
            }

            /// <include file='doc\ImageList.uex' path='docs/doc[@for="ImageCollection.ICollection.CopyTo"]/*' />
            /// <internalonly/>
            void ICollection.CopyTo(Array dest, int index)
            {
                AssertInvariant();
                for (int i = 0; i < Count; ++i)
                {
                    dest.SetValue(owner.GetBitmap(i), index++);
                }
            }

            [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
            public IEnumerator GetEnumerator()
            {
                // Forces handle creation

                AssertInvariant();
                Image[] images = new Image[Count];
                for (int i = 0; i < images.Length; ++i)
                    images[i] = owner.GetBitmap(i);

                return images.GetEnumerator();
            }

            [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
            [EditorBrowsable(EditorBrowsableState.Never)]
            public void Remove(Image image)
            {
                throw new NotSupportedException();
            }

            /// <include file='doc\ImageList.uex' path='docs/doc[@for="ImageCollection.IList.Remove"]/*' />
            /// <internalonly/>
            void IList.Remove(object image)
            {
                if (image is Image)
                {
                    Remove((Image)image);
                    owner.OnChangeHandle(new EventArgs());
                }
            }

            [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
            public void RemoveAt(int index)
            {
                if (index < 0 || index >= Count)
                    throw new ArgumentOutOfRangeException("index", DCSR.GetString(DCSR.InvalidArgument, "index", index.ToString(CultureInfo.CurrentCulture)));
                owner._images.RemoveAt(index);
                if ((imageInfoCollection != null) && (index >= 0 && index < imageInfoCollection.Count))
                {
                    imageInfoCollection.RemoveAt(index);
                }
                owner.OnChangeHandle(new EventArgs());
            }


            [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
            public void RemoveByKey(string key)
            {
                int index = IndexOfKey(key);
                if (IsValidIndex(index))
                {
                    RemoveAt(index);
                }
            }

            [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
            public void SetKeyName(int index, string name)
            {
                if (!IsValidIndex(index))
                {
                    throw new IndexOutOfRangeException(); // 
                }

                if (imageInfoCollection[index] == null)
                {
                    imageInfoCollection[index] = new ImageInfo();
                }

                ((ImageInfo)imageInfoCollection[index]).Name = name;
            }

            /// <include file='doc\ImageList.uex' path='docs/doc[@for="ImageInfo"]/*' />
            /// <internalonly/>
            internal class ImageInfo
            {
                private string name;
                public ImageInfo()
                {
                }

                public string Name
                {
                    get { return name; }
                    set { name = value; }
                }
            }

        } // end class ImageCollection
    }

    /// <include file='doc\ImageListConverter.uex' path='docs/doc[@for="ImageListConverter"]/*' />
    /// <internalonly/>
    internal class ImageListConverter : ComponentConverter
    {

        public ImageListConverter() : base(typeof(ImageList))
        {
        }

        /// <include file='doc\ImageListConverter.uex' path='docs/doc[@for="ImageListConverter.GetPropertiesSupported"]/*' />
        /// <internalonly/>
        /// <devdoc>
        ///    <para>Gets a value indicating
        ///       whether this object supports properties using the
        ///       specified context.</para>
        /// </devdoc>
        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
    }
}

