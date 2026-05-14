//------------------------------------------------------------------------------
// <copyright file="Image.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>                                                                
//------------------------------------------------------------------------------

namespace System.Drawing {
    using System.Runtime.Serialization.Formatters;
    using System.Threading;
    using System.Runtime.InteropServices;
    using System.Runtime.Serialization;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System;
    using System.Drawing.Design;
    using System.IO;    
    using System.Reflection;    
    using System.ComponentModel;
    using ArrayList = System.Collections.ArrayList;
    using Microsoft.Win32;
    using System.Drawing.Imaging;
    using System.Drawing.Internal;
    using System.Security;
    using System.Security.Permissions;
    using System.Globalization;
    using System.Runtime.Versioning;

    /**
     * Represent an image object (could be bitmap or vector)
     */
    /// <include file='doc\Image.uex' path='docs/doc[@for="Image"]/*' />
    /// <devdoc>
    ///    An abstract base class that provides
    ///    functionality for 'Bitmap', 'Icon', 'Cursor', and 'Metafile' descended classes.
    /// </devdoc>
    [
    TypeConverterAttribute(typeof(ImageConverter)),
    Editor("System.Drawing.Design.ImageEditor, " + AssemblyRef.SystemDrawingDesign, typeof(UITypeEditor)),
    ImmutableObject(true)
    ]
    [Serializable]
    [ComVisible(true)]
    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = false)]
    public abstract class Image : MarshalByRefObject, ISerializable, ICloneable, IDisposable {

        static Image()
        {
            DCValueConvert.CheckIsBlazorWASM();
        }

        private string _BlobUrl = null;
        private bool _AutoDeleteBlobUrl = false;
        public virtual string CreateBlobUrl(bool autoDeleteBlobUrl = false )
        {
            this._AutoDeleteBlobUrl = autoDeleteBlobUrl;
            if (this._BlobUrl == null)
            {
                this._BinaryForCanvasGraphics = null;
                var bs = this.ToBinary();
                if (bs != null && bs.Length > 0)
                {
                    this._BlobUrl = DCTextUtils.EventCreateBlobUrl(bs, this.GetMimeType());
                }
            }
            return this._BlobUrl;
        }
        public static bool IsBlobUrlData(byte[] bs )
        {
            return bs != null && bs.Length > 1 && bs[0] == 99;
        }
        internal bool _FromAssemblyResource = false;

        private byte[] _BinaryForCanvasGraphics = null;
        internal byte[] ToBinaryForCanvasGraphics()
        {
            if( this._FromAssemblyResource && this._BlobUrl == null )
            {
                this.CreateBlobUrl(true);
            }
            if(this._BlobUrl != null && this._BlobUrl.Length > 0)
            {
                if (this._BinaryForCanvasGraphics == null)
                {
                    var bs = System.Text.Encoding.UTF8.GetBytes(this._BlobUrl);
                    var bsResult = new byte[bs.Length + 1];
                    bsResult[0] = 99;
                    Array.Copy(bs, 0 , bsResult, 1 , bs.Length);
                    this._BinaryForCanvasGraphics = bsResult;
                }
                return this._BinaryForCanvasGraphics;
            }
            else
            {
                return this.ToBinary();
            }
        }

        internal virtual void CheckDispose() { }
        internal virtual byte[] ToBinary() { return null; }
#if FINALIZATION_WATCH
        private string allocationSite = Graphics.GetAllocationStack();
#endif


        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public delegate bool GetThumbnailImageAbort();

        /*
         * Handle to native image object
         */
        internal IntPtr nativeImage;

        // used to work around lack of animated gif encoder... rarely set...
        //
        byte[] rawData;

        //userData : so that user can use TAGS with IMAGES..
        private object userData;

        /**
         * Constructor can't be invoked directly
         */
        internal Image() {
        }

        /**
         * Constructor used in deserialization
         */
        [ResourceExposure(ResourceScope.Machine)]
        [ResourceConsumption(ResourceScope.Machine)]
        internal Image(SerializationInfo info, StreamingContext context) {
            SerializationInfoEnumerator sie = info.GetEnumerator();
            if (sie == null) {
                return;
            }
            for (; sie.MoveNext();) {
                if (String.Equals(sie.Name, "Data", StringComparison.OrdinalIgnoreCase))
                {
                    try {
                        byte[] dat = (byte[])sie.Value;
                        if (dat != null) {
                            InitializeFromStream(new MemoryStream(dat));
                        }

                    }
                    catch (ExternalException e) {
                        Debug.Fail("failure: " + e.ToString());
                    }
                    catch (ArgumentException e) {
                        Debug.Fail("failure: " + e.ToString());
                    }
                    catch (OutOfMemoryException e) {
                        Debug.Fail("failure: " + e.ToString());
                    }
                    catch (InvalidOperationException e) {
                        Debug.Fail("failure: " + e.ToString());
                    }
                    catch (NotImplementedException e) {
                        Debug.Fail("failure: " + e.ToString());
                    }
                    catch (FileNotFoundException e) {
                        Debug.Fail("failure: " + e.ToString());
                    }
                }
            }
        }

        /// <include file='doc\Image.uex' path='docs/doc[@for="Image.Tag"]/*' />
        [
        Localizable(false),
        Bindable(true),
        DefaultValue(null),
        TypeConverter(typeof(StringConverter))
        ]
        public object Tag {
            get {
                return userData;
            }
            set {
                userData = value;
            }
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public static Image FromFile(String filename) {
            return Image.FromFile(filename, false);
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public static Image FromFile(String filename,
                                     bool useEmbeddedColorManagement) {
            
            // SECREVIEW : The File.Exists() below will do the demand for the FileIOPermission
            //             for us. So, we do not need an additional demand anymore.
            //
            if (!File.Exists(filename)) {
                // I have to do this so I can give a meaningful
                // error back to the user. File.Exists() cal fail because of either
                // a failure to demand security or because the file does not exist.
                // Always telling the user that the file does not exist is not a good
                // choice. So, we demand the permission again. This means that we are
                // going to demand the permission twice for the failure case, but that's
                // better than always demanding the permission twice.
                //
                //IntSecurity.DemandReadFileIO(filename);

                throw new FileNotFoundException(filename);
            }

            //GDI+ will read this file multiple times.  Get the fully qualified path
            //so if our app changes default directory we won't get an error
            // SECREVIEW : If path does exist, the caller must have FileIOPermissionAccess.PathDiscovery permission. 
            //             Note that unlike most members of the Path class, this method accesses the file system.
            //
            filename = Path.GetFullPath(filename);

            IntPtr image = IntPtr.Zero;
            int status;
            
            if (useEmbeddedColorManagement) {
                status = SafeNativeMethods.Gdip.GdipLoadImageFromFileICM(filename, out image);
            }
            else {
                status = SafeNativeMethods.Gdip.GdipLoadImageFromFile(filename, out image);
            }
            
            if (status != SafeNativeMethods.Gdip.Ok)
                throw SafeNativeMethods.Gdip.StatusException(status);

            status = SafeNativeMethods.Gdip.GdipImageForceValidation(new HandleRef(null, image));

            if (status != SafeNativeMethods.Gdip.Ok) {
                SafeNativeMethods.Gdip.GdipDisposeImage(new HandleRef(null, image));
                throw SafeNativeMethods.Gdip.StatusException(status);
            }

            Image img = CreateImageObject(image);

            EnsureSave(img, filename, null);

            return img;
        }


        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public static Image FromStream(Stream stream) {
            return Image.FromStream(stream, false);
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public static Image FromStream(Stream stream, 
                                       bool useEmbeddedColorManagement) 
        {
            return FromStream( stream, useEmbeddedColorManagement, true );
        }
        
        public virtual bool HasData()
        {
            return false;
        }
        
        public virtual string GetMimeType()
        {
            if(this._RawFormat == ImageFormat.Jpeg)
                return "image/jpeg";
            else if(this._RawFormat == ImageFormat.Png)
                return "image/png";
            else if(this._RawFormat == ImageFormat.Gif)
                return "image/gif";
            else if(this._RawFormat == ImageFormat.Bmp)
                return "image/bmp";
            else if(this._RawFormat == ImageFormat.Tiff)
                return "image/tiff";
            return null;
        }
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public static Image FromStream(Stream stream, bool useEmbeddedColorManagement, bool validateImageData ) {
            if( !validateImageData ) {
                //IntSecurity.UnmanagedCode.Demand();
            }
            
            if (stream == null){
                throw new ArgumentException(DCSR.GetString(DCSR.InvalidArgument, "stream", "null"));
            }
            var bsData = DCSoft.DCTextUtils.LoadBinaryStream(stream);
            return new Bitmap(bsData);

            //IntPtr image = IntPtr.Zero;
            //int status;
            
            //if (useEmbeddedColorManagement) {
            //    status = SafeNativeMethods.Gdip.GdipLoadImageFromStreamICM(new GPStream(stream), out image);
            //}
            //else {
            //    status = SafeNativeMethods.Gdip.GdipLoadImageFromStream(new GPStream(stream), out image);
            //}
            
            //if (status != SafeNativeMethods.Gdip.Ok){
            //    throw SafeNativeMethods.Gdip.StatusException(status);
            //}

            //if( validateImageData ) {
            //    status = SafeNativeMethods.Gdip.GdipImageForceValidation(new HandleRef(null, image));

            //    if (status != SafeNativeMethods.Gdip.Ok) {
            //        SafeNativeMethods.Gdip.GdipDisposeImage(new HandleRef(null, image));
            //        throw SafeNativeMethods.Gdip.StatusException(status);
            //    }
            //}

            //Image img = CreateImageObject(image);

            //EnsureSave(img, null, stream);

            //return img;
        }

        // Used for serialization
        [ResourceExposure(ResourceScope.Machine)]
        [ResourceConsumption(ResourceScope.Machine)]
        private void InitializeFromStream(Stream stream) {
            IntPtr image = IntPtr.Zero;

            int status = SafeNativeMethods.Gdip.GdipLoadImageFromStream(new GPStream(stream), out image);

            if (status != SafeNativeMethods.Gdip.Ok)
                throw SafeNativeMethods.Gdip.StatusException(status);

            status = SafeNativeMethods.Gdip.GdipImageForceValidation(new HandleRef(null, image));

            if (status != SafeNativeMethods.Gdip.Ok) {
                SafeNativeMethods.Gdip.GdipDisposeImage(new HandleRef(null, image));
                throw SafeNativeMethods.Gdip.StatusException(status);
            }

            this.nativeImage = image;

            int type = -1;

            status = SafeNativeMethods.Gdip.GdipGetImageType(new HandleRef(this, nativeImage), out type);

            EnsureSave(this, null, stream);

            if (status != SafeNativeMethods.Gdip.Ok)
                throw SafeNativeMethods.Gdip.StatusException(status);
        }

        internal Image(IntPtr nativeImage) {
            SetNativeImage(nativeImage);
        }

        /**
         * Make a copy of the image object
         */
        /// <include file='doc\Image.uex' path='docs/doc[@for="Image.Clone"]/*' />
        /// <devdoc>
        ///    Creates an exact copy of this <see cref='System.Drawing.Image'/>.
        /// </devdoc>
        [ResourceExposure(ResourceScope.Machine)]
        [ResourceConsumption(ResourceScope.Machine)]
        public object Clone() {
            var img = (Image)this.MemberwiseClone();
            return img;

            //IntPtr cloneImage = IntPtr.Zero;

            //int status = SafeNativeMethods.Gdip.GdipCloneImage(new HandleRef(this, nativeImage), out cloneImage);

            //if (status != SafeNativeMethods.Gdip.Ok)
            //    throw SafeNativeMethods.Gdip.StatusException(status);

            //status = SafeNativeMethods.Gdip.GdipImageForceValidation(new HandleRef(null, cloneImage));

            //if (status != SafeNativeMethods.Gdip.Ok) {
            //    SafeNativeMethods.Gdip.GdipDisposeImage(new HandleRef(null, cloneImage));
            //    throw SafeNativeMethods.Gdip.StatusException(status);
            //}

            //return CreateImageObject(cloneImage);
        }
        protected void DeleteBlobUrl()
        {
            var str = this._BlobUrl;
            this._BlobUrl = null;
            this._BinaryForCanvasGraphics = null;
            if( str != null && str.Length > 0 )
            {
                DCTextUtils.EventDeleteBlobUrl(str);
            }
        }
        /**
         * Dispose of resources associated with the Image object
         */
        /// <include file='doc\Image.uex' path='docs/doc[@for="Image.Dispose"]/*' />
        /// <devdoc>
        ///    Cleans up Windows resources for this
        /// <see cref='System.Drawing.Image'/>.
        /// </devdoc>
        public void Dispose() {
            if (this._AutoDeleteBlobUrl
                && this._BlobUrl != null
                && this._BlobUrl.Length > 0)
            {
                var str = this._BlobUrl;
                this._BlobUrl = null;
                this._BinaryForCanvasGraphics = null;
                DCTextUtils.EventDeleteBlobUrl(str);
            }
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <include file='doc\Image.uex' path='docs/doc[@for="Image.Dispose2"]/*' />
        protected virtual void Dispose(bool disposing) {
#if FINALIZATION_WATCH
            if (!disposing && nativeImage != IntPtr.Zero)
                Debug.WriteLine("**********************\nDisposed through finalization:\n" + allocationSite);
#endif
            if (nativeImage != IntPtr.Zero) {
                try{
#if DEBUG
                    int status = 
#endif
                    SafeNativeMethods.Gdip.GdipDisposeImage(new HandleRef(this, nativeImage));
#if DEBUG
                    Debug.Assert(status == SafeNativeMethods.Gdip.Ok, "GDI+ returned an error status: " + status.ToString(CultureInfo.InvariantCulture));
#endif
                }
                catch( Exception ex ){
                    //if( ClientUtils.IsSecurityOrCriticalException( ex ) ) {
                    //    throw;
                    //}

                    Debug.Fail( "Exception thrown during Dispose: " + ex.ToString() );
                }
                finally{
                    nativeImage = IntPtr.Zero;
                }
            }
        }

        /// <include file='doc\Image.uex' path='docs/doc[@for="Image.Finalize"]/*' />
        /// <devdoc>
        ///    Cleans up Windows resources for this
        /// <see cref='System.Drawing.Image'/>.
        /// </devdoc>
        ~Image() {
            Dispose(false);
        }

        [ResourceExposure(ResourceScope.None)]
        [ResourceConsumption(ResourceScope.Machine, ResourceScope.Machine)]
        internal static void EnsureSave(Image image, string filename, Stream dataStream) {

            if (image.RawFormat.Equals(ImageFormat.Gif)) {
                bool animatedGif = false;

                Guid[] dimensions = image.FrameDimensionsList;
                foreach (Guid guid in dimensions) {
                    FrameDimension dimension = new FrameDimension(guid);
                    if (dimension.Equals(FrameDimension.Time)) {
                        animatedGif = image.GetFrameCount(FrameDimension.Time) > 1;
                        break;
                    }
                }


                if (animatedGif) {
                    try {
                        Stream created = null;
                        long lastPos = 0;
                        if (dataStream != null) {
                            lastPos = dataStream.Position;
                            dataStream.Position = 0;
                        }

                        try {
                            if (dataStream == null) {
                                created = dataStream = File.OpenRead(filename);
                            }

                            image.rawData = new byte[(int)dataStream.Length];
                            dataStream.Read(image.rawData, 0, (int)dataStream.Length);
                        }
                        finally {
                            if (created != null) {
                                created.Close();
                            }
                            else {
                                dataStream.Position = lastPos;
                            }
                        }
                    }
                    // possible exceptions for reading the filename
                    catch (UnauthorizedAccessException) {
                    }
                    catch (DirectoryNotFoundException) {
                    }
                    catch (IOException) {
                    }
                    // possible exceptions for setting/getting the position inside dataStream
                    catch (NotSupportedException) {
                    }
                    catch (ObjectDisposedException) {
                    }
                    // possible exception when reading stuff into dataStream
                    catch (ArgumentException) {
                    }
                }
            }
        }

        private enum ImageTypeEnum  {
            Bitmap = 1,
            Metafile = 2,
        }

        /* FxCop rule 'AvoidBuildingNonCallableCode' - Left here in case it is needed in the future.
        private ImageTypeEnum ImageType
        {
            get { 
                int type = -1;

                int status = SafeNativeMethods.Gdip.GdipGetImageType(new HandleRef(this, nativeImage), out type);

                if (status != SafeNativeMethods.Gdip.Ok)
                    throw SafeNativeMethods.Gdip.StatusException(status);

                return(ImageTypeEnum) type;
            }
        }
        */

        internal static Image CreateImageObject(IntPtr nativeImage) {
            Image image;

            int type = -1;

            int status = SafeNativeMethods.Gdip.GdipGetImageType(new HandleRef(null, nativeImage), out type);

            if (status != SafeNativeMethods.Gdip.Ok)
                throw SafeNativeMethods.Gdip.StatusException(status);

            switch ((ImageTypeEnum)type) {
                case ImageTypeEnum.Bitmap:     
                    image = Bitmap.FromGDIplus(nativeImage);
                    break;

                case ImageTypeEnum.Metafile:
                    image = Metafile.FromGDIplus(nativeImage);
                    break;

                default:
                    throw new ArgumentException(DCSR.GetString(DCSR.InvalidImage));
            }

            return image;
        }

        /// <include file='doc\Image.uex' path='docs/doc[@for="Image.ISerializable.GetObjectData"]/*' />
        /// <devdoc>
        ///     ISerializable private implementation
        /// </devdoc>
        /// <internalonly/>
       // [SecurityPermissionAttribute(SecurityAction.LinkDemand, Flags=SecurityPermissionFlag.SerializationFormatter)]
       // [SuppressMessage("Microsoft.Usage", "CA2240:ImplementISerializableCorrectly")]        
        void ISerializable.GetObjectData(SerializationInfo si, StreamingContext context) {
            using( MemoryStream stream = new MemoryStream() ) {
                Save( stream );
                si.AddValue("Data", stream.ToArray(), typeof(byte[]));
            }
        }

        /// <include file='doc\Image.uex' path='docs/doc[@for="Image.GetEncoderParameterList"]/*' />
        /// <devdoc>
        ///    Returns information about the codecs used
        ///    for this <see cref='System.Drawing.Image'/>.
        /// </devdoc>
        public EncoderParameters GetEncoderParameterList(Guid encoder) {
            EncoderParameters p;
            int size;

            int status = SafeNativeMethods.Gdip.GdipGetEncoderParameterListSize(new HandleRef(this, nativeImage), 
                                                                 ref encoder, 
                                                                 out size);
            if (status != SafeNativeMethods.Gdip.Ok)
                throw SafeNativeMethods.Gdip.StatusException(status);

            if (size <= 0)
                return null;

            IntPtr buffer = Marshal.AllocHGlobal(size);

            status = SafeNativeMethods.Gdip.GdipGetEncoderParameterList(new HandleRef(this, nativeImage),
                                                         ref encoder, 
                                                         size,
                                                         buffer);

            try{
                if (status != SafeNativeMethods.Gdip.Ok) {
                    throw SafeNativeMethods.Gdip.StatusException(status);
                }

                p = EncoderParameters.ConvertFromMemory(buffer);
            }
            finally{
                Marshal.FreeHGlobal(buffer);
            }

            return p;  
        }

        /// <include file='doc\Image.uex' path='docs/doc[@for="Image.Save"]/*' />
        /// <devdoc>
        ///    Saves this <see cref='System.Drawing.Image'/> to the specified file.
        /// </devdoc>
        public void Save(string filename) {
            Save( filename, RawFormat );
        }

        /// <include file='doc\Image.uex' path='docs/doc[@for="Image.Save1"]/*' />
        /// <devdoc>
        ///    Saves this <see cref='System.Drawing.Image'/> to the specified file in the
        ///    specified format.
        /// </devdoc>
        public void Save(string filename, ImageFormat format) {
            if (format == null)
                throw new ArgumentNullException("format");

            ImageCodecInfo codec = format.FindEncoder();

            if (codec == null)
                codec = ImageFormat.Png.FindEncoder();

            Save(filename, codec, null);
        }

        /// <include file='doc\Image.uex' path='docs/doc[@for="Image.Save2"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Saves this <see cref='System.Drawing.Image'/> to the specified file in the specified format
        ///       and with the specified encoder parameters.
        ///    </para>
        /// </devdoc>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        [ResourceExposure(ResourceScope.None)]
        [ResourceConsumption(ResourceScope.Machine, ResourceScope.Machine)]
        public void Save(string filename, ImageCodecInfo encoder, EncoderParameters encoderParams) {
            if (filename == null)
                throw new ArgumentNullException("filename");
            if (encoder == null)
                throw new ArgumentNullException("encoder");

            //IntSecurity.DemandWriteFileIO(filename);

            IntPtr encoderParamsMemory = IntPtr.Zero;

            if (encoderParams != null) {
                rawData = null;
                encoderParamsMemory = encoderParams.ConvertToMemory();
            }
            int status = SafeNativeMethods.Gdip.Ok;

            try {
                Guid g = encoder.Clsid;
                bool saved = false;

                if (rawData != null) {
                    ImageCodecInfo rawEncoder = RawFormat.FindEncoder();
                    if (rawEncoder != null && rawEncoder.Clsid == g) {
                        using (FileStream fs = File.OpenWrite(filename)) {
                            fs.Write(rawData, 0, rawData.Length);
                            saved = true;
                        }
                    }
                }

                if (!saved) {
                    status = SafeNativeMethods.Gdip.GdipSaveImageToFile(new HandleRef(this, nativeImage),
                                                             filename,
                                                             ref g,
                                                             new HandleRef(encoderParams, encoderParamsMemory));
                }
            }
            finally {
                if (encoderParamsMemory != IntPtr.Zero) {
                    Marshal.FreeHGlobal(encoderParamsMemory);
                }
            }

            if (status != SafeNativeMethods.Gdip.Ok) {
                throw SafeNativeMethods.Gdip.StatusException(status);
            }
        }

        internal void Save(MemoryStream stream) {
            // Jpeg loses data, so we don't want to use it to serialize...
            //
            ImageFormat dest = RawFormat;
            if (dest == ImageFormat.Jpeg) {
                dest = ImageFormat.Png;
            }
            ImageCodecInfo codec = dest.FindEncoder();

            // If we don't find an Encoder (for things like Icon), we
            // just switch back to PNG...
            //
            if (codec == null) {
                codec = ImageFormat.Png.FindEncoder();
            }
            Save(stream, codec, null);
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void Save(Stream stream, ImageFormat format) {
            if (format == null)
                throw new ArgumentNullException("format");

            ImageCodecInfo codec = format.FindEncoder();
            Save(stream, codec, null);
        }

        /// <include file='doc\Image.uex' path='docs/doc[@for="Image.Save4"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Saves this <see cref='System.Drawing.Image'/> to the specified stream in the specified
        ///       format.
        ///    </para>
        /// </devdoc>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public void Save(Stream stream, ImageCodecInfo encoder, EncoderParameters encoderParams) {
            if (stream == null){
                throw new ArgumentNullException("stream");
            }
            if (encoder == null) {
                throw new ArgumentNullException("encoder");
            }

            IntPtr encoderParamsMemory = IntPtr.Zero;

            if (encoderParams != null) {
                rawData = null;
                encoderParamsMemory = encoderParams.ConvertToMemory();
            }

            int status = SafeNativeMethods.Gdip.Ok;

            try {
                Guid g = encoder.Clsid;
                bool saved = false;

                if (rawData != null) {
                    ImageCodecInfo rawEncoder = RawFormat.FindEncoder();
                    if (rawEncoder != null && rawEncoder.Clsid == g) {
                        stream.Write(rawData, 0, rawData.Length);
                        saved = true;
                    }
                }

                if (!saved) {
                    status = SafeNativeMethods.Gdip.GdipSaveImageToStream(new HandleRef(this, nativeImage),
                                                                     new UnsafeNativeMethods.ComStreamFromDataStream(stream),
                                                                     ref g,
                                                                     new HandleRef(encoderParams, encoderParamsMemory));
                }
            }
            finally {
                if (encoderParamsMemory != IntPtr.Zero) {
                    Marshal.FreeHGlobal(encoderParamsMemory);
                }
            }
            
            if (status != SafeNativeMethods.Gdip.Ok) {
                throw SafeNativeMethods.Gdip.StatusException(status);
            }
        }

        /// <include file='doc\Image.uex' path='docs/doc[@for="Image.SaveAdd"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Adds an <see cref='System.Drawing.Imaging.EncoderParameters'/> to this
        ///    <see cref='System.Drawing.Image'/>.
        ///    </para>
        /// </devdoc>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public void SaveAdd(EncoderParameters encoderParams) {
            IntPtr encoder = IntPtr.Zero;
            if (encoderParams != null) {
                encoder = encoderParams.ConvertToMemory();
            }

            rawData = null;
            int status = SafeNativeMethods.Gdip.GdipSaveAdd(new HandleRef(this, nativeImage), new HandleRef(encoderParams, encoder));

            if (encoder != IntPtr.Zero) {
                Marshal.FreeHGlobal(encoder);
            }
            if (status != SafeNativeMethods.Gdip.Ok) {
                throw SafeNativeMethods.Gdip.StatusException(status);
            }
        }

        /// <include file='doc\Image.uex' path='docs/doc[@for="Image.SaveAdd1"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Adds an <see cref='System.Drawing.Imaging.EncoderParameters'/> to the
        ///       specified <see cref='System.Drawing.Image'/>.
        ///    </para>
        /// </devdoc>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public void SaveAdd(Image image, EncoderParameters encoderParams) {
            IntPtr encoder = IntPtr.Zero;

            if (image == null) {
                throw new ArgumentNullException("image");
            }
            if (encoderParams != null) {
                encoder = encoderParams.ConvertToMemory();
            }

            rawData = null;
            int status = SafeNativeMethods.Gdip.GdipSaveAddImage(new HandleRef(this, nativeImage), new HandleRef(image, image.nativeImage), new HandleRef(encoderParams, encoder));

            if (encoder != IntPtr.Zero){
                Marshal.FreeHGlobal(encoder);
            }
            if (status != SafeNativeMethods.Gdip.Ok) {
                throw SafeNativeMethods.Gdip.StatusException(status);
            }
        }

        /**
         * Return; image size information
         */
        private SizeF _GetPhysicalDimension() {
            float width;
            float height;

            int status = SafeNativeMethods.Gdip.GdipGetImageDimension(new HandleRef(this, nativeImage), out width, out height);

            if (status != SafeNativeMethods.Gdip.Ok)
                throw SafeNativeMethods.Gdip.StatusException(status);

            return new SizeF(width, height);
        }

        /// <include file='doc\Image.uex' path='docs/doc[@for="Image.PhysicalDimension"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Gets the width and height of this
        ///    <see cref='System.Drawing.Image'/>.
        ///    </para>
        /// </devdoc>
        public SizeF PhysicalDimension {
            get { return _GetPhysicalDimension();}
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public Size Size {
            [System.Runtime.TargetedPatchingOptOutAttribute("Performance critical to inline across NGen image boundaries")]
            get {
                return new Size(Width, Height);
            }
        }

        protected int _Width = 0;
        [
        DefaultValue(false),
        Browsable(false),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)
        ]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public int Width {
            get {
                return this._Width;
            }
        }

        protected int _Height = 0;
        [
        DefaultValue(false),
        Browsable(false),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)
        ]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public int Height {
            get {
                return this._Height;
            }
        }

        protected float _HorizontalResolution = 96;
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public float HorizontalResolution {
            get {
                return this._HorizontalResolution;
            }
        }

        protected float _VerticalResolution = 96;
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public float VerticalResolution {
            get {
                return this._VerticalResolution;
            }
        }

        protected int _Flags = 0;
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public int Flags {
            get {
                return this._Flags;
            }
        }

        protected Imaging.ImageFormat _RawFormat = Imaging.ImageFormat.Bmp;
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public ImageFormat RawFormat {
            get {
                return this._RawFormat;
            }
        }

        protected PixelFormat _PixelFormat = PixelFormat.Format24bppRgb;
        [System.Reflection.Obfuscation( Exclude = true , ApplyToMembers = true )]
        public PixelFormat PixelFormat {
            get {
                return this._PixelFormat;
            }
        }

        /// <include file='doc\Image.uex' path='docs/doc[@for="Image.GetBounds"]/*' />
        /// <devdoc>
        ///    Gets a bounding rectangle in
        ///    the specified units for this <see cref='System.Drawing.Image'/>.
        /// </devdoc>        
        [SuppressMessage("Microsoft.Design", "CA1045:DoNotPassTypesByReference")]
        public RectangleF GetBounds(ref GraphicsUnit pageUnit) {
            GPRECTF gprectf = new GPRECTF();

            int status = SafeNativeMethods.Gdip.GdipGetImageBounds(new HandleRef(this, nativeImage), ref gprectf, out pageUnit);

            if (status != SafeNativeMethods.Gdip.Ok)
                throw SafeNativeMethods.Gdip.StatusException(status);

            return gprectf.ToRectangleF();
        }

        private ColorPalette _GetColorPalette() {
            int size = -1;

            int status = SafeNativeMethods.Gdip.GdipGetImagePaletteSize(new HandleRef(this, nativeImage), out size);
            // "size" is total byte size:
            // sizeof(ColorPalette) + (pal->Count-1)*sizeof(ARGB)

            if (status != SafeNativeMethods.Gdip.Ok) {
                throw SafeNativeMethods.Gdip.StatusException(status);
            }

            ColorPalette palette = new ColorPalette(size);

            // Memory layout is:
            //    UINT Flags
            //    UINT Count
            //    ARGB Entries[size]

            IntPtr memory = Marshal.AllocHGlobal(size);

            status = SafeNativeMethods.Gdip.GdipGetImagePalette(new HandleRef(this, nativeImage), memory, size);

            try {
                if (status != SafeNativeMethods.Gdip.Ok) {
                    throw SafeNativeMethods.Gdip.StatusException(status);
                }

                palette.ConvertFromMemory(memory);
            }
            finally {
                Marshal.FreeHGlobal(memory);
            }

            return palette;
        }

        private void _SetColorPalette(ColorPalette palette) {
            IntPtr memory = palette.ConvertToMemory();

            int status = SafeNativeMethods.Gdip.GdipSetImagePalette(new HandleRef(this, nativeImage), memory);

            if (memory != IntPtr.Zero) {
                Marshal.FreeHGlobal(memory);
            }
            if (status != SafeNativeMethods.Gdip.Ok) {
                throw SafeNativeMethods.Gdip.StatusException(status);
            }
        }

        protected ColorPalette _Palette;
        public ColorPalette Palette
        {
            get {
                return this._Palette;
            }
            set {
                this._Palette = value;
            }
        }

        // Thumbnail support

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public Image GetThumbnailImage(int thumbWidth, int thumbHeight, 
                                       GetThumbnailImageAbort callback, IntPtr callbackData) {
            IntPtr thumbImage = IntPtr.Zero;

            int status = SafeNativeMethods.Gdip.GdipGetImageThumbnail(new HandleRef(this, nativeImage), thumbWidth, thumbHeight, out thumbImage,
                                                       callback, callbackData);
            if (status != SafeNativeMethods.Gdip.Ok)
                throw SafeNativeMethods.Gdip.StatusException(status);

            return CreateImageObject(thumbImage);
        }

        // Multi-frame support

        /// <include file='doc\Image.uex' path='docs/doc[@for="Image.FrameDimensionsList"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Gets an array of GUIDs that represent the
        ///       dimensions of frames within this <see cref='System.Drawing.Image'/>.
        ///    </para>
        /// </devdoc>        
        [Browsable(false)]
        public Guid[] FrameDimensionsList {
            [SuppressMessage("Microsoft.Performance", "CA1808:AvoidCallsThatBoxValueTypes")]
            get {
                int count;

                int status = SafeNativeMethods.Gdip.GdipImageGetFrameDimensionsCount(new HandleRef(this, nativeImage), out count);

                if (status != SafeNativeMethods.Gdip.Ok) {
                    throw SafeNativeMethods.Gdip.StatusException(status);
                }

                Debug.Assert(count >= 0, "FrameDimensionsList returns bad count");                    
                if (count <= 0) {
                    return new Guid[0];
                }

                int size = (int) Marshal.SizeOf(typeof(Guid));

                IntPtr buffer = Marshal.AllocHGlobal(size*count);
                if (buffer == IntPtr.Zero) {
                    throw SafeNativeMethods.Gdip.StatusException(SafeNativeMethods.Gdip.OutOfMemory);
                }

                status = SafeNativeMethods.Gdip.GdipImageGetFrameDimensionsList(new HandleRef(this, nativeImage), buffer, count);

                if (status != SafeNativeMethods.Gdip.Ok) {
                    Marshal.FreeHGlobal(buffer);
                    throw SafeNativeMethods.Gdip.StatusException(status);
                }

                Guid[] guids = new Guid[count];

                try {
                    for (int i=0; i<count; i++) {
                        guids[i] = (Guid) UnsafeNativeMethods.PtrToStructure((IntPtr)((long)buffer + size*i), typeof(Guid));
                    }
                }
                finally {
                    Marshal.FreeHGlobal(buffer);
                }

                return guids;
            }
        }

        /// <include file='doc\Image.uex' path='docs/doc[@for="Image.GetFrameCount"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Returns the number of frames of the given
        ///       dimension.
        ///    </para>
        /// </devdoc>
        public int GetFrameCount(FrameDimension dimension) {
            int[] count = new int[] { 0};

            Guid dimensionID = dimension.Guid;
            int status = SafeNativeMethods.Gdip.GdipImageGetFrameCount(new HandleRef(this, nativeImage), ref dimensionID, count);

            if (status != SafeNativeMethods.Gdip.Ok)
                throw SafeNativeMethods.Gdip.StatusException(status);

            return count[0];
        }

        /// <include file='doc\Image.uex' path='docs/doc[@for="Image.SelectActiveFrame"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Selects the frame specified by the given
        ///       dimension and index.
        ///    </para>
        /// </devdoc>
        public int SelectActiveFrame(FrameDimension dimension, int frameIndex) {
            int[] count = new int[] { 0};

            Guid dimensionID = dimension.Guid;
            int status = SafeNativeMethods.Gdip.GdipImageSelectActiveFrame(new HandleRef(this, nativeImage), ref dimensionID, frameIndex);

            if (status != SafeNativeMethods.Gdip.Ok)
                throw SafeNativeMethods.Gdip.StatusException(status);

            return count[0];
        }

        /// <include file='doc\Image.uex' path='docs/doc[@for="Image.RotateFlip"]/*' />
        /// <devdoc>
        ///    <para>
        ///    </para>
        /// </devdoc>
        public void RotateFlip(RotateFlipType rotateFlipType) {

            int status = SafeNativeMethods.Gdip.GdipImageRotateFlip(new HandleRef(this, nativeImage), unchecked((int) rotateFlipType));

            if (status != SafeNativeMethods.Gdip.Ok)
                throw SafeNativeMethods.Gdip.StatusException(status);

        }

        /// <include file='doc\Image.uex' path='docs/doc[@for="Image.PropertyIdList"]/*' />
        /// <devdoc>
        ///    Gets an array of the property IDs stored in
        ///    this <see cref='System.Drawing.Image'/>.
        /// </devdoc>
        [Browsable(false)]
        public int[] PropertyIdList
        {
            get {
                int count;

                int status = SafeNativeMethods.Gdip.GdipGetPropertyCount(new HandleRef(this, nativeImage), out count);

                if (status != SafeNativeMethods.Gdip.Ok)
                    throw SafeNativeMethods.Gdip.StatusException(status);

                int[] propid = new int[count];

                //if we have a 0 count, just return our empty array
                if (count == 0)
                    return propid;

                status = SafeNativeMethods.Gdip.GdipGetPropertyIdList(new HandleRef(this, nativeImage), count, propid);

                if (status != SafeNativeMethods.Gdip.Ok)
                    throw SafeNativeMethods.Gdip.StatusException(status);

                return propid;    
            }
        }

        /// <include file='doc\Image.uex' path='docs/doc[@for="Image.GetPropertyItem"]/*' />
        /// <devdoc>
        ///    Gets the specified property item from this
        /// <see cref='System.Drawing.Image'/>.
        /// </devdoc>
        public PropertyItem GetPropertyItem(int propid) {
            PropertyItem propitem;
            int size;

            int status = SafeNativeMethods.Gdip.GdipGetPropertyItemSize(new HandleRef(this, nativeImage), propid, out size);

            if (status != SafeNativeMethods.Gdip.Ok)
                throw SafeNativeMethods.Gdip.StatusException(status);

            if (size == 0)
                return null;

            IntPtr propdata = Marshal.AllocHGlobal(size);

            if (propdata == IntPtr.Zero)
                throw SafeNativeMethods.Gdip.StatusException(SafeNativeMethods.Gdip.OutOfMemory);

            status = SafeNativeMethods.Gdip.GdipGetPropertyItem(new HandleRef(this, nativeImage), propid, size, propdata);

            try {
                if (status != SafeNativeMethods.Gdip.Ok) {
                    throw SafeNativeMethods.Gdip.StatusException(status);
                }

                propitem = PropertyItemInternal.ConvertFromMemory(propdata, 1)[0];
            }
            finally {
                Marshal.FreeHGlobal(propdata);
            }

            return propitem;
        }

        /// <include file='doc\Image.uex' path='docs/doc[@for="Image.RemovePropertyItem"]/*' />
        /// <devdoc>
        ///    Removes the specified property item from
        ///    this <see cref='System.Drawing.Image'/>.
        /// </devdoc>
        public void RemovePropertyItem(int propid) {
            int status = SafeNativeMethods.Gdip.GdipRemovePropertyItem(new HandleRef(this, nativeImage), propid);
            if (status != SafeNativeMethods.Gdip.Ok)
                throw SafeNativeMethods.Gdip.StatusException(status);
        }

        /// <include file='doc\Image.uex' path='docs/doc[@for="Image.SetPropertyItem"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Sets the specified property item to the
        ///       specified value.
        ///    </para>
        /// </devdoc>
        public void SetPropertyItem(PropertyItem propitem) {
            PropertyItemInternal propItemInternal = PropertyItemInternal.ConvertFromPropertyItem(propitem);

            using (propItemInternal) {
                int status = SafeNativeMethods.Gdip.GdipSetPropertyItem(new HandleRef(this, nativeImage), propItemInternal);
                if (status != SafeNativeMethods.Gdip.Ok)
                    throw SafeNativeMethods.Gdip.StatusException(status);
            }
        }

        /// <include file='doc\Image.uex' path='docs/doc[@for="Image.PropertyItems"]/*' />
        /// <devdoc>
        ///    Gets an array of <see cref='System.Drawing.Imaging.PropertyItem'/> objects that describe this <see cref='System.Drawing.Image'/>.
        /// </devdoc>
        [Browsable(false)]
        public PropertyItem[] PropertyItems
        {
            get {
                int size;
                int count;

                int status = SafeNativeMethods.Gdip.GdipGetPropertyCount(new HandleRef(this, nativeImage), out count);               

                if (status != SafeNativeMethods.Gdip.Ok)
                    throw SafeNativeMethods.Gdip.StatusException(status);

                status = SafeNativeMethods.Gdip.GdipGetPropertySize(new HandleRef(this, nativeImage), out size, ref count);

                if (status != SafeNativeMethods.Gdip.Ok)
                    throw SafeNativeMethods.Gdip.StatusException(status);

                if (size == 0 || count == 0)
                    return new PropertyItem[0];

                IntPtr propdata = Marshal.AllocHGlobal(size);

                status = SafeNativeMethods.Gdip.GdipGetAllPropertyItems(new HandleRef(this, nativeImage), size, count, propdata);

                PropertyItem[] props = null;

                try {
                    if (status != SafeNativeMethods.Gdip.Ok) {
                        throw SafeNativeMethods.Gdip.StatusException(status);
                    }

                    props = PropertyItemInternal.ConvertFromMemory(propdata, count);
                }
                finally {
                    Marshal.FreeHGlobal(propdata);
                }

                return props;
            }
        }

        internal void SetNativeImage(IntPtr handle) {
            if (handle == IntPtr.Zero)
                throw new ArgumentException(DCSR.GetString(DCSR.NativeHandle0), "handle");

            nativeImage = handle;
        }

        // !! Ambiguous to offer constructor for 'FromHbitmap'
        /// <include file='doc\Image.uex' path='docs/doc[@for="Image.FromHbitmap"]/*' />
        /// <devdoc>
        ///    Creates a <see cref='System.Drawing.Bitmap'/> from a Windows handle.
        /// </devdoc>
        [ResourceExposure(ResourceScope.Machine)]
        [ResourceConsumption(ResourceScope.Machine)]
        public static Bitmap FromHbitmap(IntPtr hbitmap) {
            //IntSecurity.ObjectFromWin32Handle.Demand();

            return FromHbitmap(hbitmap, IntPtr.Zero);
        }

        /// <include file='doc\Image.uex' path='docs/doc[@for="Image.FromHbitmap1"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Creates a <see cref='System.Drawing.Bitmap'/> from the specified Windows
        ///       handle with the specified color palette.
        ///    </para>
        /// </devdoc>
        [ResourceExposure(ResourceScope.Machine)]
        [ResourceConsumption(ResourceScope.Machine)]
        public static Bitmap FromHbitmap(IntPtr hbitmap, IntPtr hpalette) {
            //IntSecurity.ObjectFromWin32Handle.Demand();

            IntPtr bitmap = IntPtr.Zero;
            int status = SafeNativeMethods.Gdip.GdipCreateBitmapFromHBITMAP(new HandleRef(null, hbitmap), new HandleRef(null, hpalette), out bitmap);

            if (status != SafeNativeMethods.Gdip.Ok)
                throw SafeNativeMethods.Gdip.StatusException(status);

            return Bitmap.FromGDIplus(bitmap);
        }

        /*
         * Return the pixel size for the specified format (in bits)
         */
        /// <include file='doc\Image.uex' path='docs/doc[@for="Image.GetPixelFormatSize"]/*' />
        /// <devdoc>
        ///    Returns the size of the specified pixel
        ///    format.
        /// </devdoc>
        public static int GetPixelFormatSize(PixelFormat pixfmt) {
            return(unchecked((int)pixfmt) >> 8) & 0xFF;
        }

        /*
         * Determine if the pixel format can have alpha channel
         */
        /// <include file='doc\Image.uex' path='docs/doc[@for="Image.IsAlphaPixelFormat"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Returns a value indicating whether the
        ///       pixel format contains alpha information.
        ///    </para>
        /// </devdoc>
        public static bool IsAlphaPixelFormat(PixelFormat pixfmt) {
            return(pixfmt & PixelFormat.Alpha) != 0;
        }

        /*
         * Determine if the pixel format is an extended format,
         * i.e. supports 16-bit per channel
         */
        /// <include file='doc\Image.uex' path='docs/doc[@for="Image.IsExtendedPixelFormat"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Returns a value indicating whether the pixel format is extended.
        ///    </para>
        /// </devdoc>
        public static bool IsExtendedPixelFormat(PixelFormat pixfmt) {
            return(pixfmt & PixelFormat.Extended) != 0;
        }

        /*
         * Determine if the pixel format is canonical format:
         *   PixelFormat32bppARGB
         *   PixelFormat32bppPARGB
         *   PixelFormat64bppARGB
         *   PixelFormat64bppPARGB
         */
        /// <include file='doc\Image.uex' path='docs/doc[@for="Image.IsCanonicalPixelFormat"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Returns a value indicating whether the pixel format is canonical.
        ///    </para>
        /// </devdoc>
        public static bool IsCanonicalPixelFormat(PixelFormat pixfmt) {
            return(pixfmt & PixelFormat.Canonical) != 0;
        }
    }
}

