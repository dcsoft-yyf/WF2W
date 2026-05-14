//------------------------------------------------------------------------------
// <copyright file="Bitmap.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>                                                                
//------------------------------------------------------------------------------

namespace System.Drawing
{
    using System.IO;
    using System.IO.Compression;
    using System.Runtime.InteropServices;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Diagnostics.Contracts;
    using System;
    using System.Drawing.Design;
    using Microsoft.Win32;
    using System.ComponentModel;
    using System.Drawing.Imaging;
    using System.Drawing;
    using System.Drawing.Internal;
    using System.Runtime.Serialization;
    using System.Security;
    using System.Security.Permissions;
    using System.Runtime.Versioning;
    using System.Runtime.Serialization.Formatters;

    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = false)]
    [
    Editor("System.Drawing.Design.BitmapEditor, " + AssemblyRef.SystemDrawingDesign, typeof(UITypeEditor)),
    Serializable,
    ComVisible(true)
    ]
    public sealed class Bitmap : Image
    {
        private static readonly string _Header_bmp = "data:image/bmp;base64,";
        private static readonly string _Header_png = "data:image/png;base64,";
        private static readonly string _Header_gif = "data:image/gif;base64,";
        private static readonly string _Header_jpeg = "data:image/jpeg;base64,";
        internal static int StaticGetImageFormatByteHeaderFlag(byte[]bs)
        {
            if(bs == null || bs.Length == 0 )
            {
                return 100;
            }
            if (FileHeaderHelper.HasJpegHeader(bs))
            {
                return 0;
            }
            else if (FileHeaderHelper.HasPNGHeader(bs))
            {
                return 1;
            }
            else if (FileHeaderHelper.HasGIFHeader(bs))
            {
                return 2;
            }
            else if (FileHeaderHelper.HasBMPHeader(bs))
            {
                return 3;
            }
            else if (Image.IsBlobUrlData(bs))
            {
                return 5;
            }
            else
            {
                return 4;
            }
        }
        internal static string StaticGetEmitImageSourceHeader(byte[] bs)
        {
            if (bs != null && bs.Length > 0)
            {
                if (FileHeaderHelper.HasBMPHeader(bs))
                {
                    return _Header_bmp;
                }
                else if (FileHeaderHelper.HasPNGHeader(bs))
                {
                    return _Header_png;
                }
                else if (FileHeaderHelper.HasGIFHeader(bs))
                {
                    return _Header_gif;
                }
                else if (FileHeaderHelper.HasJpegHeader(bs))
                {
                    return _Header_jpeg;
                }
                else
                {
                    return _Header_jpeg;
                }
            }
            return null;
        }

        private static Color defaultTransparentColor = Color.LightGray;

        public Bitmap(byte[] bsData)
        {
            if (bsData == null || bsData.Length == 0)
            {
                throw new ArgumentNullException("bsData");
            }
            this.Data = bsData;
        }

        private byte[] _Data;
        public byte[] Data
        {
            get
            {
                base.CheckDispose();
                return this._Data;
            }
            set
            {
                // Use SetData to parse the binary data and populate width/height/resolution/format
                SetData(value);
            }
        }

        internal bool IsBmp{get{return this._RawFormat == Imaging.ImageFormat.Bmp;}}
        internal bool IsJpeg { get { return this._RawFormat == Imaging.ImageFormat.Jpeg; } }
        internal bool IsPng { get { return this._RawFormat == Imaging.ImageFormat.Png; } }
        internal bool IsGif { get { return this._RawFormat == Imaging.ImageFormat.Gif; } }

        private void SetData(byte[] bsData)
        {
            base.DeleteBlobUrl();
            this._Data = bsData;
            // Defaults
            this._Width = 0;
            this._Height = 0;
            this._HorizontalResolution = 96;
            this._VerticalResolution = 96;
            this._RawFormat = Imaging.ImageFormat.Bmp;
            this._PixelFormat = PixelFormat.Format24bppRgb;
            this._Flags = 0;
            this._DecompressedFromPNG = null;
            if (bsData == null || bsData.Length < 8)
                return;

            try
            {
                // Helper lambdas
                static uint ReadUInt32BE(byte[] b, int idx)
                {
                    if (idx + 4 > b.Length) return 0u;
                    return (uint)((b[idx] << 24) | (b[idx + 1] << 16) | (b[idx + 2] << 8) | b[idx + 3]);
                }
                static uint ReadUInt24LE(byte[] b, int idx)
                {
                    if (idx + 3 > b.Length) return 0u;
                    return (uint)(b[idx] | (b[idx + 1] << 8) | (b[idx + 2] << 16));
                }
                static int ReadInt32LE(byte[] b, int idx)
                {
                    if (idx + 4 > b.Length) return 0;
                    return b[idx] | (b[idx + 1] << 8) | (b[idx + 2] << 16) | (b[idx + 3] << 24);
                }
                static int ReadInt16LE(byte[] b, int idx)
                {
                    if (idx + 2 > b.Length) return 0;
                    return b[idx] | (b[idx + 1] << 8);
                }
                static int ReadInt16BE(byte[] b, int idx)
                {
                    if (idx + 2 > b.Length) return 0;
                    return (b[idx] << 8) | b[idx + 1];
                }

                // PNG
                if (bsData.Length >= 8 && bsData[0] == 0x89 && bsData[1] == 0x50 && bsData[2] == 0x4E && bsData[3] == 0x47
                    && bsData[4] == 0x0D && bsData[5] == 0x0A && bsData[6] == 0x1A && bsData[7] == 0x0A)
                {
                    // IHDR is at offset 8 with length 13 and type 'IHDR'
                    byte[] plteChunk = null;
                    byte[] trnsChunk = null;
                    if (bsData.Length >= 24)
                    {
                        uint width = ReadUInt32BE(bsData, 16);
                        uint height = ReadUInt32BE(bsData, 20);
                        this._Width = (int)width;
                        this._Height = (int)height;
                        this._RawFormat = Imaging.ImageFormat.Png;
                        // Read color type from IHDR (byte at offset 25)
                        byte colorType = 0;
                        if (bsData.Length > 25)
                        {
                            colorType = bsData[25];
                            // colorType 6 or 4 has alpha
                            if (colorType == 6 || colorType == 4)
                                this._PixelFormat = PixelFormat.Format32bppArgb;
                            else if (colorType == 3)
                                this._PixelFormat = PixelFormat.Format8bppIndexed;
                            else
                                this._PixelFormat = PixelFormat.Format24bppRgb;
                        }
                        // Try find pHYs chunk for DPI and also capture PLTE/tRNS
                        int offset = 8 + 8 + 13; // signature + IHDR(length+type) + IHDR payload
                        while (offset + 8 <= bsData.Length)
                        {
                            // read next chunk length and type
                            uint chunkLen = ReadUInt32BE(bsData, offset);
                            if (offset + 8 + (int)chunkLen > bsData.Length) break;
                            string chunkType = System.Text.Encoding.ASCII.GetString(bsData, offset + 4, 4);
                            if (chunkType == "pHYs" && chunkLen >= 9)
                            {
                                // pixels per unit X (4 BE), Y (4 BE), unit specifier (1)
                                uint ppux = ReadUInt32BE(bsData, offset + 8);
                                uint ppuy = ReadUInt32BE(bsData, offset + 12);
                                byte unit = bsData[offset + 16];
                                if (unit == 1 && ppux > 0 && ppuy > 0)
                                {
                                    // pixels per meter -> dpi = ppm * 0.0254
                                    float hDpi = (float)(ppux * 0.0254);
                                    float vDpi = (float)(ppuy * 0.0254);
                                    if (hDpi > 0) this._HorizontalResolution = hDpi;
                                    if (vDpi > 0) this._VerticalResolution = vDpi;
                                }
                            }
                            else if (chunkType == "PLTE")
                            {
                                // palette: RGB triples
                                plteChunk = new byte[chunkLen];
                                Array.Copy(bsData, offset + 8, plteChunk, 0, (int)chunkLen);
                            }
                            else if (chunkType == "tRNS")
                            {
                                trnsChunk = new byte[chunkLen];
                                Array.Copy(bsData, offset + 8, trnsChunk, 0, (int)chunkLen);
                            }

                            // move to next chunk: length(4)+type(4)+data+crc(4)
                            offset += 8 + (int)chunkLen + 4;
                        }

                        // Determine transparent color from tRNS and PLTE when indexed
                        if (trnsChunk != null)
                        {
                            if (colorType == 3 && plteChunk != null)
                            {
                                // trnsChunk contains alpha values for palette entries
                                int len = Math.Min(trnsChunk.Length, plteChunk.Length / 3);
                                for (int i = 0; i < len; i++)
                                {
                                    if (trnsChunk[i] == 0)
                                    {
                                        int pi = i * 3;
                                        byte r = plteChunk[pi];
                                        byte g = plteChunk[pi + 1];
                                        byte b = plteChunk[pi + 2];
                                        this._TransparentColor = Color.FromArgb(r, g, b);
                                        break;
                                    }
                                }
                            }
                            else if ((colorType == 0 || colorType == 2) && trnsChunk.Length >= 6)
                            {
                                // for grayscale or truecolor, tRNS stores a single sample value
                                if (trnsChunk.Length >= 6)
                                {
                                    // truecolor: 6 bytes RGB (BE)
                                    int r = (trnsChunk[0] << 8) | trnsChunk[1];
                                    int g = (trnsChunk[2] << 8) | trnsChunk[3];
                                    int b = (trnsChunk[4] << 8) | trnsChunk[5];
                                    // convert 16-bit samples to 8-bit by >> 8
                                    this._TransparentColor = Color.FromArgb((byte)(r >> 8), (byte)(g >> 8), (byte)(b >> 8));
                                }
                            }
                        }
                    }
                    return;
                }

                // GIF
                if (bsData.Length >= 10 && bsData[0] == 0x47 && bsData[1] == 0x49 && bsData[2] == 0x46)
                {
                    // GIF87a or GIF89a
                    int width = ReadInt16LE(bsData, 6);
                    int height = ReadInt16LE(bsData, 8);
                    this._Width = width;
                    this._Height = height;
                    this._RawFormat = Imaging.ImageFormat.Gif;
                    this._PixelFormat = PixelFormat.Format8bppIndexed;

                    // Attempt to read Global Color Table and Graphics Control Extension for transparent index
                    int gctFlag = bsData[10] & 0x80; // packed field at offset 10
                    int gctSizeVal = bsData[10] & 0x07; // size = 2^(N+1)
                    int gctEntries = 0;
                    int gctOffset = 13; // signature(6) + LSD(7)=13
                    byte[] gct = null;
                    if (gctFlag != 0)
                    {
                        gctEntries = 1 << (gctSizeVal + 1);
                        int paletteBytes = gctEntries * 3;
                        if (gctOffset + paletteBytes <= bsData.Length)
                        {
                            gct = new byte[paletteBytes];
                            Array.Copy(bsData, gctOffset, gct, 0, paletteBytes);
                        }
                    }

                    // Scan blocks after GCT to find Graphics Control Extension (0x21 0xF9)
                    int pos = gctOffset + (gct != null ? gct.Length : 0);
                    while (pos + 1 < bsData.Length)
                    {
                        byte b0 = bsData[pos];
                        if (b0 == 0x3B)
                        { // Trailer
                            break;
                        }
                        if (b0 == 0x21 && pos + 1 < bsData.Length)
                        {
                            byte label = bsData[pos + 1];
                            if (label == 0xF9 && pos + 6 < bsData.Length)
                            {
                                // GCE block: 0x21 0xF9 0x04 [packed] [delay low][delay hi] [transpIndex] 0x00
                                byte packed = bsData[pos + 3];
                                bool transpFlag = (packed & 0x01) != 0;
                                if (transpFlag)
                                {
                                    int transpIndex = bsData[pos + 6];
                                    if (gct != null)
                                    {
                                        int pi = transpIndex * 3;
                                        if (pi + 2 < gct.Length)
                                        {
                                            byte r = gct[pi];
                                            byte g = gct[pi + 1];
                                            byte b = gct[pi + 2];
                                            this._TransparentColor = Color.FromArgb(r, g, b);
                                        }
                                    }
                                }
                                break;
                            }
                            // other extensions: skip
                            if (pos + 2 < bsData.Length)
                            {
                                // extension block: skip introducer + label + block(s)
                                pos += 2;
                                // skip data sub-blocks
                                while (pos < bsData.Length)
                                {
                                    byte blockLen = bsData[pos++];
                                    if (blockLen == 0) break;
                                    pos += blockLen;
                                }
                                continue;
                            }
                        }
                        else if (b0 == 0x2C)
                        {
                            // Image Descriptor starts, stop scanning for GCE
                            break;
                        }
                        else
                        {
                            pos++;
                        }
                    }

                    return;
                }

                // BMP
                if (bsData.Length >= 26 && bsData[0] == 0x42 && bsData[1] == 0x4D)
                {
                    // BITMAPFILEHEADER is 14 bytes, BITMAPINFOHEADER starts at 14
                    int dibHeaderSize = ReadInt32LE(bsData, 14);
                    if (dibHeaderSize >= 12 && bsData.Length >= 14 + dibHeaderSize)
                    {
                        // Handle BITMAPCOREHEADER (size 12) and BITMAPINFOHEADER (size >=40)
                        int width;
                        int heightRaw;
                        if (dibHeaderSize == 12)
                        {
                            width = ReadInt16LE(bsData, 18);
                            heightRaw = ReadInt16LE(bsData, 20);
                        }
                        else
                        {
                            width = ReadInt32LE(bsData, 18);
                            heightRaw = ReadInt32LE(bsData, 22);
                        }
                        this._Width = width;
                        this._Height = Math.Abs(heightRaw);
                        this._RawFormat = Imaging.ImageFormat.Bmp;
                        // bits per pixel at offset 28 for BITMAPINFOHEADER
                        if (dibHeaderSize >= 16)
                        {
                            int bpp = ReadInt16LE(bsData, 28);
                            if (bpp == 32)
                                this._PixelFormat = PixelFormat.Format32bppArgb;
                            else if (bpp == 24)
                                this._PixelFormat = PixelFormat.Format24bppRgb;
                            else if (bpp == 8)
                                this._PixelFormat = PixelFormat.Format8bppIndexed;
                        }
                        // try resolution (pixels per meter) at offsets 38 and 42 for BITMAPINFOHEADER
                        if (dibHeaderSize >= 40)
                        {
                            int ppmX = ReadInt32LE(bsData, 38);
                            int ppmY = ReadInt32LE(bsData, 42);
                            if (ppmX > 0) this._HorizontalResolution = (float)(ppmX * 0.0254);
                            if (ppmY > 0) this._VerticalResolution = (float)(ppmY * 0.0254);
                        }
                    }
                    return;
                }

                // JPEG
                if (bsData.Length >= 3 && bsData[0] == 0xFF && bsData[1] == 0xD8)
                {
                    int offset = 2;
                    // Defaults
                    int foundWidth = 0, foundHeight = 0;
                    // Also look for JFIF APP0 for DPI
                    while (offset + 1 < bsData.Length)
                    {
                        // skip any padding 0xFF
                        while (offset < bsData.Length && bsData[offset] == 0xFF) offset++;
                        if (offset >= bsData.Length) break;
                        byte marker = bsData[offset++];
                        // Standalone markers (no length) range 0xD0-0xD9, 0x01
                        if (marker == 0xD8 || marker == 0x01) continue;
                        if (marker >= 0xD0 && marker <= 0xD9) continue;
                        if (offset + 1 >= bsData.Length) break;
                        int segLen = ReadInt16BE(bsData, offset);
                        offset += 2;
                        if (segLen < 2 || offset + segLen - 2 > bsData.Length) break;
                        int segDataStart = offset;
                        // Check for SOF markers that contain size (C0..C3, C5..C7, C9..CB, CD..CF)
                        if (marker == 0xC0 || marker == 0xC1 || marker == 0xC2 || marker == 0xC3 ||
                            marker == 0xC5 || marker == 0xC6 || marker == 0xC7 ||
                            marker == 0xC9 || marker == 0xCA || marker == 0xCB ||
                            marker == 0xCD || marker == 0xCE || marker == 0xCF)
                        {
                            if (segDataStart + 5 < bsData.Length)
                            {
                                int precision = bsData[segDataStart];
                                int height = ReadInt16BE(bsData, segDataStart + 1);
                                int width = ReadInt16BE(bsData, segDataStart + 3);
                                foundWidth = width;
                                foundHeight = height;
                                this._Width = width;
                                this._Height = height;
                                this._RawFormat = Imaging.ImageFormat.Jpeg;
                                this._PixelFormat = PixelFormat.Format24bppRgb;
                                // continue to look for JFIF APP0
                            }
                        }
                        else if (marker == 0xE0)
                        {
                            // APP0 - could be JFIF
                            // check identifier 'JFIF\0'
                            if (segDataStart + 5 < bsData.Length)
                            {
                                if (bsData[segDataStart] == (byte)'J' && bsData[segDataStart + 1] == (byte)'F' && bsData[segDataStart + 2] == (byte)'I' && bsData[segDataStart + 3] == (byte)'F' && bsData[segDataStart + 4] == 0)
                                {
                                    // JFIF header present
                                    if (segDataStart + 7 < bsData.Length)
                                    {
                                        byte units = bsData[segDataStart + 7];
                                        int xdensity = ReadInt16BE(bsData, segDataStart + 8);
                                        int ydensity = ReadInt16BE(bsData, segDataStart + 10);
                                        if (units == 1)
                                        {
                                            if (xdensity > 0) this._HorizontalResolution = xdensity;
                                            if (ydensity > 0) this._VerticalResolution = ydensity;
                                        }
                                        else if (units == 2)
                                        {
                                            // density is dots per cm
                                            if (xdensity > 0) this._HorizontalResolution = (float)(xdensity * 2.54);
                                            if (ydensity > 0) this._VerticalResolution = (float)(ydensity * 2.54);
                                        }
                                    }
                                }
                            }
                        }
                        offset = segDataStart + segLen - 2;
                        // break early if we have both size and dpi
                        if (this._Width > 0 && this._HorizontalResolution != 96)
                        {
                            // keep going until we find SOF though - but ok to break if both discovered
                        }
                    }
                    return;
                }

                // WebP (RIFF with WEBP) - check 'RIFF' and 'WEBP'
                if (bsData.Length >= 12 && bsData[0] == (byte)'R' && bsData[1] == (byte)'I' && bsData[2] == (byte)'F' && bsData[3] == (byte)'F'
                    && bsData[8] == (byte)'W' && bsData[9] == (byte)'E' && bsData[10] == (byte)'B' && bsData[11] == (byte)'P')
                {
                    // iterate chunks starting at offset 12
                    int offset = 12;
                    while (offset + 8 <= bsData.Length)
                    {
                        string chunkId = System.Text.Encoding.ASCII.GetString(bsData, offset, 4);
                        uint chunkSize = (uint)(bsData[offset + 4] | (bsData[offset + 5] << 8) | (bsData[offset + 6] << 16) | (bsData[offset + 7] << 24));
                        int dataStart = offset + 8;
                        if (dataStart + chunkSize > bsData.Length) break;
                        if (chunkId == "VP8X")
                        {
                            // Extended header: 10 bytes payload, bytes 4..6 width-1 little endian, 7..9 height-1
                            if (chunkSize >= 10 && dataStart + 10 <= bsData.Length)
                            {
                                uint wMinus1 = ReadUInt24LE(bsData, dataStart + 4);
                                uint hMinus1 = ReadUInt24LE(bsData, dataStart + 7);
                                this._Width = (int)(wMinus1 + 1);
                                this._Height = (int)(hMinus1 + 1);
                                this._RawFormat = Imaging.ImageFormat.Bmp; // no WebP static in ImageFormat - fallback to Bmp? We'll set to Bmp-like
                                // There is no WebP ImageFormat in System.Drawing; leave RawFormat as Bmp or create custom guid? Keep Bmp.
                                this._PixelFormat = PixelFormat.Format32bppArgb;
                                return;
                            }
                        }
                        else if (chunkId == "VP8 ")
                        {
                            // Simple lossy VP8: frame header contains width/height in start of payload
                            // Payload starts with 10 bytes of frame header; width/height in little-endian 14-bit values at offsets 6/8
                            if (chunkSize >= 10 && dataStart + 10 <= bsData.Length)
                            {
                                // Parse frame tag: see RFC6386 - but simpler: width = (bs[ dataStart+6 ] | bs[dataStart+7]<<8) & 0x3FFF
                                int rawW = (bsData[dataStart + 6] | (bsData[dataStart + 7] << 8)) & 0x3FFF;
                                int rawH = (bsData[dataStart + 8] | (bsData[dataStart + 9] << 8)) & 0x3FFF;
                                if (rawW > 0 && rawH > 0)
                                {
                                    this._Width = rawW;
                                    this._Height = rawH;
                                    this._RawFormat = Imaging.ImageFormat.Bmp;
                                    this._PixelFormat = PixelFormat.Format24bppRgb;
                                    return;
                                }
                            }
                        }
                        else if (chunkId == "VP8L")
                        {
                            // lossless: first byte signature, then 4 bytes contain width/height
                            if (chunkSize >= 5 && dataStart + 5 <= bsData.Length)
                            {
                                // width = 1 + ((b1) | ((b2 & 0x3F) << 8)) ; height = 1 + (((b2 >> 6) | (b3 << 2) | ((b4 & 0x0F) << 10)))
                                byte b0 = bsData[dataStart];
                                int w = 1 + ((bsData[dataStart + 1] | ((bsData[dataStart + 2] & 0x3F) << 8)));
                                int h = 1 + (((bsData[dataStart + 2] >> 6) | (bsData[dataStart + 3] << 2) | ((bsData[dataStart + 4] & 0x0F) << 10)));
                                if (w > 0 && h > 0)
                                {
                                    this._Width = w;
                                    this._Height = h;
                                    this._RawFormat = Imaging.ImageFormat.Bmp;
                                    this._PixelFormat = PixelFormat.Format32bppArgb;
                                    return;
                                }
                            }
                        }

                        // chunks are padded to even size
                        int advance = 8 + (int)chunkSize + ((chunkSize & 1) == 1 ? 1 : 0);
                        offset += advance;
                    }
                }

            }
            catch
            {
                // ignore parsing errors and keep defaults
            }
        }
        private System.Drawing.Color _TransparentColor = System.Drawing.Color.Empty;
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public System.Drawing.Color TransparentColor
        {
            get
            {
                return this._TransparentColor;
            }
        }
        internal override byte[] ToBinary()
        {
            return this._Data;
        }
        /*
         * Predefined bitmap data formats
         */

        /// <include file='doc\Bitmap.uex' path='docs/doc[@for="Bitmap.Bitmap"]/*' />
        /// <devdoc>
        ///    Initializes a new instance of the
        /// <see cref='System.Drawing.Bitmap'/> 
        /// class from the specified file.
        /// </devdoc>
        /**
         * Create a new bitmap object from URL
         */
        [ResourceExposure(ResourceScope.Machine)]
        [ResourceConsumption(ResourceScope.Machine)]
        public Bitmap(String filename)
        {
            //IntSecurity.DemandReadFileIO(filename);

            //GDI+ will read this file multiple times.  Get the fully qualified path
            //so if our app changes default directory we won't get an error
            //
            filename = Path.GetFullPath(filename);

            IntPtr bitmap = IntPtr.Zero;

            int status = SafeNativeMethods.Gdip.GdipCreateBitmapFromFile(filename, out bitmap);

            if (status != SafeNativeMethods.Gdip.Ok)
                throw SafeNativeMethods.Gdip.StatusException(status);

            status = SafeNativeMethods.Gdip.GdipImageForceValidation(new HandleRef(null, bitmap));

            if (status != SafeNativeMethods.Gdip.Ok)
            {
                SafeNativeMethods.Gdip.GdipDisposeImage(new HandleRef(null, bitmap));
                throw SafeNativeMethods.Gdip.StatusException(status);
            }

            SetNativeImage(bitmap);

            EnsureSave(this, filename, null);
        }

        /// <include file='doc\Bitmap.uex' path='docs/doc[@for="Bitmap.Bitmap1"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Initializes a new instance of the <see cref='System.Drawing.Bitmap'/> class from the specified
        ///       file.
        ///    </para>
        /// </devdoc>
        [ResourceExposure(ResourceScope.Machine)]
        [ResourceConsumption(ResourceScope.Machine)]
        public Bitmap(String filename, bool useIcm)
        {
            //IntSecurity.DemandReadFileIO(filename);

            //GDI+ will read this file multiple times.  Get the fully qualified path
            //so if our app changes default directory we won't get an error
            //
            filename = Path.GetFullPath(filename);

            IntPtr bitmap = IntPtr.Zero;
            int status;

            if (useIcm)
            {
                status = SafeNativeMethods.Gdip.GdipCreateBitmapFromFileICM(filename, out bitmap);
            }
            else
            {
                status = SafeNativeMethods.Gdip.GdipCreateBitmapFromFile(filename, out bitmap);
            }

            if (status != SafeNativeMethods.Gdip.Ok)
                throw SafeNativeMethods.Gdip.StatusException(status);

            status = SafeNativeMethods.Gdip.GdipImageForceValidation(new HandleRef(null, bitmap));

            if (status != SafeNativeMethods.Gdip.Ok)
            {
                SafeNativeMethods.Gdip.GdipDisposeImage(new HandleRef(null, bitmap));
                throw SafeNativeMethods.Gdip.StatusException(status);
            }

            SetNativeImage(bitmap);

            EnsureSave(this, filename, null);
        }

        public Bitmap(Type type, string resource)
        {
            var asm = type.Module.Assembly;
            var stream = asm.GetManifestResourceStream(type, resource);
            if (stream == null)
            {
                var names = asm.GetManifestResourceNames();
                foreach (var name in names)
                {
                    if (name.EndsWith(resource, StringComparison.OrdinalIgnoreCase))
                    {
                        stream = asm.GetManifestResourceStream(name);
                        break;
                    }
                }
            }
            if (stream == null)
            {
                throw new ArgumentException(DCSR.GetString(DCSR.ResourceNotFound, type, resource));
            }
            var bs = new byte[stream.Length];
            stream.Read(bs, 0, bs.Length);
            stream.Close();
            this.SetData(bs);
            this._FromAssemblyResource = true;
        }

        /// <include file='doc\Bitmap.uex' path='docs/doc[@for="Bitmap.Bitmap3"]/*' />
        /// <devdoc>
        ///    Initializes a new instance of the
        /// <see cref='System.Drawing.Bitmap'/> 
        /// class from the specified data stream.
        /// </devdoc>
        /**
         * Create a new bitmap object from a stream
         */
        [ResourceExposure(ResourceScope.Machine)]
        [ResourceConsumption(ResourceScope.Machine)]
        public Bitmap(Stream stream)
        {
            var bs = DCTextUtils.LoadBinaryStream(stream);
            this.SetData(bs);
            //if (stream == null)
            //    throw new ArgumentException(DCSR.GetString(DCSR.InvalidArgument, "stream", "null"));

            //IntPtr bitmap = IntPtr.Zero;

            //int status = SafeNativeMethods.Gdip.GdipCreateBitmapFromStream(new GPStream(stream), out bitmap);

            //if (status != SafeNativeMethods.Gdip.Ok)
            //    throw SafeNativeMethods.Gdip.StatusException(status);

            //status = SafeNativeMethods.Gdip.GdipImageForceValidation(new HandleRef(null, bitmap));

            //if (status != SafeNativeMethods.Gdip.Ok)
            //{
            //    SafeNativeMethods.Gdip.GdipDisposeImage(new HandleRef(null, bitmap));
            //    throw SafeNativeMethods.Gdip.StatusException(status);
            //}

            //SetNativeImage(bitmap);

            //EnsureSave(this, null, stream);
        }

        /// <include file='doc\Bitmap.uex' path='docs/doc[@for="Bitmap.Bitmap4"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Initializes a new instance of the <see cref='System.Drawing.Bitmap'/> class from the specified data
        ///       stream.
        ///    </para>
        /// </devdoc>
        [ResourceExposure(ResourceScope.Machine)]
        [ResourceConsumption(ResourceScope.Machine)]
        public Bitmap(Stream stream, bool useIcm)
        {

            if (stream == null)
                throw new ArgumentException(DCSR.GetString(DCSR.InvalidArgument, "stream", "null"));

            IntPtr bitmap = IntPtr.Zero;
            int status;

            if (useIcm)
            {
                status = SafeNativeMethods.Gdip.GdipCreateBitmapFromStreamICM(new GPStream(stream), out bitmap);
            }
            else
            {
                status = SafeNativeMethods.Gdip.GdipCreateBitmapFromStream(new GPStream(stream), out bitmap);
            }

            if (status != SafeNativeMethods.Gdip.Ok)
                throw SafeNativeMethods.Gdip.StatusException(status);

            status = SafeNativeMethods.Gdip.GdipImageForceValidation(new HandleRef(null, bitmap));

            if (status != SafeNativeMethods.Gdip.Ok)
            {
                SafeNativeMethods.Gdip.GdipDisposeImage(new HandleRef(null, bitmap));
                throw SafeNativeMethods.Gdip.StatusException(status);
            }

            SetNativeImage(bitmap);

            EnsureSave(this, null, stream);
        }

        /// <include file='doc\Bitmap.uex' path='docs/doc[@for="Bitmap.Bitmap5"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Initializes a new instance of the
        ///       Bitmap class with the specified size, pixel format, and pixel data.
        ///    </para>
        /// </devdoc>
        [ResourceExposure(ResourceScope.Machine)]
        [ResourceConsumption(ResourceScope.Machine)]
        public Bitmap(int width, int height, int stride, PixelFormat format, IntPtr scan0)
        {
            //IntSecurity.ObjectFromWin32Handle.Demand();

            IntPtr bitmap = IntPtr.Zero;

            int status = SafeNativeMethods.Gdip.GdipCreateBitmapFromScan0(width, height, stride, unchecked((int)format), new HandleRef(null, scan0), out bitmap);

            if (status != SafeNativeMethods.Gdip.Ok)
                throw SafeNativeMethods.Gdip.StatusException(status);

            SetNativeImage(bitmap);
        }

        /// <include file='doc\Bitmap.uex' path='docs/doc[@for="Bitmap.Bitmap6"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Initializes a new instance of the Bitmap class with the specified
        ///       size and format.
        ///    </para>
        /// </devdoc>
        [ResourceExposure(ResourceScope.Machine)]
        [ResourceConsumption(ResourceScope.Machine)]
        public Bitmap(int width, int height, PixelFormat format)
        {
            this._Width = width;
            this._Height = height;
            this._PixelFormat = format;
            //IntPtr bitmap = IntPtr.Zero;

            //int status = SafeNativeMethods.Gdip.GdipCreateBitmapFromScan0(width, height, 0, unchecked((int) format), NativeMethods.NullHandleRef, out bitmap);

            //if (status != SafeNativeMethods.Gdip.Ok)
            //    throw SafeNativeMethods.Gdip.StatusException(status);

            //SetNativeImage(bitmap);
        }

        /// <include file='doc\Bitmap.uex' path='docs/doc[@for="Bitmap.Bitmap7"]/*' />
        /// <devdoc>
        ///    Initializes a new instance of the
        /// <see cref='System.Drawing.Bitmap'/> 
        /// class with the specified size.
        /// </devdoc>
        [ResourceExposure(ResourceScope.Machine)]
        [ResourceConsumption(ResourceScope.Machine)]
        public Bitmap(int width, int height) : this(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb)
        {
        }

        /// <include file='doc\Bitmap.uex' path='docs/doc[@for="Bitmap.Bitmap8"]/*' />
        /// <devdoc>
        ///    Initializes a new instance of the
        /// <see cref='System.Drawing.Bitmap'/> 
        /// class with the specified size and target <see cref='System.Drawing.Graphics'/>.
        /// </devdoc>
        [ResourceExposure(ResourceScope.Machine)]
        [ResourceConsumption(ResourceScope.Machine)]
        public Bitmap(int width, int height, Graphics g)
        {
            if (g == null)
                throw new ArgumentNullException(DCSR.GetString(DCSR.InvalidArgument, "g", "null"));

            IntPtr bitmap = IntPtr.Zero;

            int status = SafeNativeMethods.Gdip.GdipCreateBitmapFromGraphics(width, height, new HandleRef(g, g.NativeGraphics), out bitmap);

            if (status != SafeNativeMethods.Gdip.Ok)
                throw SafeNativeMethods.Gdip.StatusException(status);

            SetNativeImage(bitmap);
        }

        public Bitmap(Image original)
        {
            if (original == null)
            {
                throw new ArgumentNullException("original");
            }
            if (original is Bitmap b2)
            {
                this._Width = b2._Width;
                this._Height = b2._Height;
                this._Data = b2._Data;
                this._Flags = b2._Flags;
                this._RawFormat = b2._RawFormat;
                this._PixelFormat = b2._PixelFormat;
                this._HorizontalResolution = b2._HorizontalResolution;
                this._TransparentColor = b2._TransparentColor;
                this._VerticalResolution = b2._VerticalResolution;
            }
            else
            {
                throw new NotSupportedException(original.GetType().Name);
            }
        }

        /// <include file='doc\Bitmap.uex' path='docs/doc[@for="Bitmap.Bitmap10"]/*' />
        /// <devdoc>
        ///    Initializes a new instance of the
        /// <see cref='System.Drawing.Bitmap'/> 
        /// class, from the specified existing image, with the specified size.
        /// </devdoc>
        [ResourceExposure(ResourceScope.Machine)]
        [ResourceConsumption(ResourceScope.Machine)]
        public Bitmap(Image original, int width, int height)
        {
            if (original == null)
            {
                throw new ArgumentNullException("original");
            }
            if (original is Bitmap b2)
            {
                this._Width = width;
                this._Height = height;
                this._Data = b2._Data;
                this._Flags = b2._Flags;
                this._RawFormat = b2._RawFormat;
                this._PixelFormat = b2._PixelFormat;
                this._HorizontalResolution = b2._HorizontalResolution;
                this._TransparentColor = b2._TransparentColor;
                this._VerticalResolution = b2._VerticalResolution;
            }
            else
            {
                throw new NotSupportedException(original.GetType().Name);
            }
        }

        /**
         * Constructor used in deserialization
         */
        [ResourceExposure(ResourceScope.Machine)]
        [ResourceConsumption(ResourceScope.Machine)]
        private Bitmap(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        /// <include file='doc\Bitmap.uex' path='docs/doc[@for="Bitmap.FromHicon"]/*' />
        /// <devdoc>
        ///    Creates a <see cref='System.Drawing.Bitmap'/> from a Windows handle to an
        ///    Icon.
        /// </devdoc>
        [ResourceExposure(ResourceScope.Machine)]
        [ResourceConsumption(ResourceScope.Machine)]
        public static Bitmap FromHicon(IntPtr hicon)
        {
            //IntSecurity.ObjectFromWin32Handle.Demand();

            IntPtr bitmap = IntPtr.Zero;

            int status = SafeNativeMethods.Gdip.GdipCreateBitmapFromHICON(new HandleRef(null, hicon), out bitmap);

            if (status != SafeNativeMethods.Gdip.Ok)
                throw SafeNativeMethods.Gdip.StatusException(status);

            return Bitmap.FromGDIplus(bitmap);
        }

        /// <include file='doc\Bitmap.uex' path='docs/doc[@for="Bitmap.FromResource"]/*' />
        /// <devdoc>
        /// </devdoc>
        [ResourceExposure(ResourceScope.Machine)]
        [ResourceConsumption(ResourceScope.Machine)]
        public static Bitmap FromResource(IntPtr hinstance, String bitmapName)
        {
            //IntSecurity.ObjectFromWin32Handle.Demand();

            IntPtr bitmap;

            IntPtr name = Marshal.StringToHGlobalUni(bitmapName);

            int status = SafeNativeMethods.Gdip.GdipCreateBitmapFromResource(new HandleRef(null, hinstance),
                                                              new HandleRef(null, name),
                                                              out bitmap);
            Marshal.FreeHGlobal(name);

            if (status != SafeNativeMethods.Gdip.Ok)
            {
                throw SafeNativeMethods.Gdip.StatusException(status);
            }

            return Bitmap.FromGDIplus(bitmap);
        }

        ///// <include file='doc\Bitmap.uex' path='docs/doc[@for="Bitmap.GetHbitmap"]/*' />
        ///// <devdoc>
        /////    <para>
        /////       Creates a Win32 HBITMAP out of the image. You are responsible for
        /////       de-allocating the HBITMAP with Windows.DeleteObject(handle). If the image uses
        /////       transparency, the background will be filled with the specified background
        /////       color.
        /////    </para>
        ///// </devdoc>
        ////[SecurityPermission(SecurityAction.LinkDemand, Flags=SecurityPermissionFlag.UnmanagedCode)]
        ////[EditorBrowsable(EditorBrowsableState.Advanced)]
        ////[ResourceExposure(ResourceScope.Machine)]
        ////[ResourceConsumption(ResourceScope.Machine)]
        //public IntPtr GetHbitmap() {
        //    return GetHbitmap(Color.LightGray);
        //}

        ///// <include file='doc\Bitmap.uex' path='docs/doc[@for="Bitmap.GetHbitmap1"]/*' />
        ///// <devdoc>
        /////     Creates a Win32 HBITMAP out of the image.  You are responsible for
        /////     de-allocating the HBITMAP with Windows.DeleteObject(handle).
        /////     If the image uses transparency, the background will be filled with the specified background color.
        ///// </devdoc>
        //[SecurityPermission(SecurityAction.LinkDemand, Flags=SecurityPermissionFlag.UnmanagedCode)]
        //[EditorBrowsable(EditorBrowsableState.Advanced)]
        //[ResourceExposure(ResourceScope.Machine)]
        //[ResourceConsumption(ResourceScope.Machine)]
        //public IntPtr GetHbitmap(Color background) {
        //    IntPtr hBitmap = IntPtr.Zero;

        //    int status = SafeNativeMethods.Gdip.GdipCreateHBITMAPFromBitmap(new HandleRef(this, nativeImage), out hBitmap, 
        //                                                     ColorTranslator.ToWin32(background));
        //    if(status == 2 /* invalid parameter*/ && (this.Width >= Int16.MaxValue || this.Height >= Int16.MaxValue)) {
        //        throw (new ArgumentException(SR.GetString(SR.GdiplusInvalidSize)));
        //    }

        //    if (status != SafeNativeMethods.Gdip.Ok)
        //        throw SafeNativeMethods.Gdip.StatusException(status);

        //    return hBitmap;
        //}

        ///// <include file='doc\Bitmap.uex' path='docs/doc[@for="Bitmap.GetHicon"]/*' />
        ///// <devdoc>
        /////    Returns the handle to an icon.
        ///// </devdoc>
        ////[SecurityPermission(SecurityAction.LinkDemand, Flags=SecurityPermissionFlag.UnmanagedCode)]
        ////[EditorBrowsable(EditorBrowsableState.Advanced)]
        ////[ResourceExposure(ResourceScope.Machine)]
        ////[ResourceConsumption(ResourceScope.Machine)]
        //public IntPtr GetHicon() {
        //    IntPtr hIcon = IntPtr.Zero;

        //    int status = SafeNativeMethods.Gdip.GdipCreateHICONFromBitmap(new HandleRef(this, nativeImage), out hIcon);

        //    if (status != SafeNativeMethods.Gdip.Ok)
        //        throw SafeNativeMethods.Gdip.StatusException(status);

        //    return hIcon;
        //}

        /// <include file='doc\Bitmap.uex' path='docs/doc[@for="Bitmap.Bitmap11"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Initializes a new instance of the <see cref='System.Drawing.Bitmap'/> class, from the specified
        ///       existing image, with the specified size.
        ///    </para>
        /// </devdoc>
        //[ResourceExposure(ResourceScope.Machine)]
        //[ResourceConsumption(ResourceScope.Machine)]
        public Bitmap(Image original, Size newSize) :
        this(original, (object)newSize != null ? newSize.Width : 0, (object)newSize != null ? newSize.Height : 0)
        {
        }

        // for use with CreateFromGDIplus
        //[System.Runtime.TargetedPatchingOptOutAttribute("Performance critical to inline across NGen image boundaries")]
        private Bitmap()
        {
        }

        /*
         * Create a new bitmap object from a native bitmap handle.
         * This is only for internal purpose.
         */
        internal static Bitmap FromGDIplus(IntPtr handle)
        {
            Bitmap result = new Bitmap();
            result.SetNativeImage(handle);
            return result;
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public Bitmap Clone(Rectangle rect, PixelFormat format)
        {

            //validate the rect
            if (rect.Width == 0 || rect.Height == 0)
            {
                throw new ArgumentException(DCSR.GetString(DCSR.GdiplusInvalidRectangle, rect.ToString()));
            }

            IntPtr dstHandle = IntPtr.Zero;

            int status = SafeNativeMethods.Gdip.GdipCloneBitmapAreaI(
                                                     rect.X,
                                                     rect.Y,
                                                     rect.Width,
                                                     rect.Height,
                                                     unchecked((int)format),
                                                     new HandleRef(this, nativeImage),
                                                     out dstHandle);

            if (status != SafeNativeMethods.Gdip.Ok || dstHandle == IntPtr.Zero)
                throw SafeNativeMethods.Gdip.StatusException(status);

            return Bitmap.FromGDIplus(dstHandle);
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public Bitmap Clone(RectangleF rect, PixelFormat format)
        {

            //validate the rect
            if (rect.Width == 0 || rect.Height == 0)
            {
                throw new ArgumentException(DCSR.GetString(DCSR.GdiplusInvalidRectangle, rect.ToString()));
            }

            IntPtr dstHandle = IntPtr.Zero;

            int status = SafeNativeMethods.Gdip.GdipCloneBitmapArea(
                                                    rect.X,
                                                    rect.Y,
                                                    rect.Width,
                                                    rect.Height,
                                                    unchecked((int)format),
                                                    new HandleRef(this, nativeImage),
                                                    out dstHandle);

            if (status != SafeNativeMethods.Gdip.Ok || dstHandle == IntPtr.Zero)
                throw SafeNativeMethods.Gdip.StatusException(status);

            return Bitmap.FromGDIplus(dstHandle);
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void MakeTransparent()
        {
            Color transparent = defaultTransparentColor;
            if (Height > 0 && Width > 0)
                transparent = GetPixel(0, Size.Height - 1);
            if (transparent.A < 255)
            {
                // It's already transparent, and if we proceeded, we will do something
                // unintended like making black transparent
                return;
            }
            MakeTransparent(transparent);
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void MakeTransparent(Color transparentColor)
        {
            //return;
            // Directly modify _Data bytes for certain formats to mark a color as transparent
            // Currently supports PNG truecolor (colorType 2) and grayscale (colorType 0) by injecting tRNS chunk.
            var data = this._Data;
            if (data == null || data.Length < 8) return;
            this.DeleteBlobUrl();
            this._DecompressedFromPNG = null;
            this._PngDecodedPixels = null;
            // PNG signature
            if (data[0] == 0x89 && data[1] == 0x50 && data[2] == 0x4E && data[3] == 0x47 &&
                data[4] == 0x0D && data[5] == 0x0A && data[6] == 0x1A && data[7] == 0x0A)
            {
                // Decode to RGBA and zero out matching color to ensure browsers respect alpha
                var pixels = GetPngDecodedPixels();
                if (pixels != null && _PngBytesPerPixel >= 3 && _PngWidth > 0 && _PngHeight > 0)
                {
                    int count = _PngWidth * _PngHeight;
                    var rgbaOut = new byte[count * 4];
                    for (int i = 0; i < count; i++)
                    {
                        int src = i * _PngBytesPerPixel;
                        byte r = _PngDecodedPixels[src + 0];
                        byte g = _PngDecodedPixels[src + 1];
                        byte b = _PngDecodedPixels[src + 2];
                        byte a = (_PngBytesPerPixel == 4) ? _PngDecodedPixels[src + 3] : (byte)255;
                        if (r == transparentColor.R && g == transparentColor.G && b == transparentColor.B)
                        {
                            a = 0;
                        }
                        int dst = i * 4;
                        rgbaOut[dst + 0] = r;
                        rgbaOut[dst + 1] = g;
                        rgbaOut[dst + 2] = b;
                        rgbaOut[dst + 3] = a;
                    }
                    var pngBytes = EncodeRgbaToPng(rgbaOut, _PngWidth, _PngHeight);
                    this._Data = pngBytes;
                    this._TransparentColor = transparentColor;
                    this._RawFormat = Imaging.ImageFormat.Png;
                    this._PixelFormat = PixelFormat.Format32bppArgb;
                    this._DecompressedFromPNG = null;
                    this._PngDecodedPixels = null;
                    return;
                }
            }

            // Other formats not yet supported for in-place transparency without external codecs.
            // Try BMP in-place processing (32bpp only)
            if (data.Length >= 26 && data[0] == 0x42 && data[1] == 0x4D)
            {
                // BITMAPFILEHEADER: offset to pixel array at byte 10 (little endian)
                int pixelOffset = data[10] | (data[11] << 8) | (data[12] << 16) | (data[13] << 24);
                int dibHeaderSize = data[14] | (data[15] << 8) | (data[16] << 16) | (data[17] << 24);
                if (dibHeaderSize >= 12 && data.Length >= 14 + dibHeaderSize)
                {
                    int width;
                    int heightRaw;
                    int bpp;
                    if (dibHeaderSize == 12)
                    {
                        width = data[18] | (data[19] << 8);
                        heightRaw = data[20] | (data[21] << 8);
                        bpp = data[24] | (data[25] << 8); // BITMAPCOREHEADER bitcount at offset 24
                    }
                    else
                    {
                        width = data[18] | (data[19] << 8) | (data[20] << 16) | (data[21] << 24);
                        heightRaw = data[22] | (data[23] << 8) | (data[24] << 16) | (data[25] << 24);
                        bpp = data[28] | (data[29] << 8);
                    }

                    // Process various bit depths by converting to 32bpp so alpha can be applied
                    if (pixelOffset > 0 && pixelOffset + 3 <= data.Length)
                    {
                        bool topDown = heightRaw < 0;
                        int absHeight = Math.Abs(heightRaw);

                        // determine palette if needed (<=8bpp)
                        Color[] palette = null;
                        if (bpp <= 8)
                        {
                            int paletteCount = 0;
                            if (dibHeaderSize >= 40)
                            {
                                if (14 + 35 < data.Length)
                                {
                                    paletteCount = data[46] | (data[47] << 8) | (data[48] << 16) | (data[49] << 24);
                                }
                                if (paletteCount <= 0) paletteCount = 1 << bpp;
                            }
                            else
                            {
                                paletteCount = 1 << bpp;
                            }
                            int paletteEntrySize = (dibHeaderSize == 12) ? 3 : 4;
                            int paletteStart = 14 + dibHeaderSize;
                            int available = Math.Max(0, (data.Length - paletteStart) / paletteEntrySize);
                            int count = Math.Min(paletteCount, available);
                            palette = new Color[count];
                            for (int i = 0; i < count; i++)
                            {
                                int idx = paletteStart + i * paletteEntrySize;
                                byte b = data[idx + 0];
                                byte g = data[idx + 1];
                                byte r = data[idx + 2];
                                palette[i] = Color.FromArgb(r, g, b);
                            }
                        }

                        // Calculate strides
                        int strideSrc = ((width * bpp + 31) / 32) * 4;
                        int stride32 = width * 4;
                        int imageSize32 = stride32 * absHeight;

                        byte[] bmp32 = data;
                        int bmp32Offset = pixelOffset;
                        bool bmp32TopDown = topDown;

                        // If already 32bpp and stride matches, modify in place for efficiency
                        if (bpp == 32)
                        {
                            int stride = width * 4;
                            int imageSize = 0;
                            if (dibHeaderSize >= 40)
                            {
                                imageSize = data[34] | (data[35] << 8) | (data[36] << 16) | (data[37] << 24);
                                if (imageSize > 0 && absHeight > 0)
                                {
                                    int calcStride = imageSize / absHeight;
                                    if (calcStride >= width * 4 && calcStride % 4 == 0) stride = calcStride;
                                }
                            }

                            byte tr = transparentColor.R;
                            byte tg = transparentColor.G;
                            byte tb = transparentColor.B;
                            for (int y = 0; y < absHeight; y++)
                            {
                                int rowIndex = topDown ? y : (absHeight - 1 - y);
                                int rowStart = pixelOffset + rowIndex * stride;
                                if (rowStart < 0 || rowStart + width * 4 > data.Length) break;
                                for (int x = 0; x < width; x++)
                                {
                                    int p = rowStart + x * 4;
                                    byte b = data[p + 0];
                                    byte g = data[p + 1];
                                    byte r = data[p + 2];
                                    if (r == tr && g == tg && b == tb)
                                    {
                                        data[p + 3] = 0x00;
                                    }
                                }
                            }
                            bmp32 = data;
                            bmp32Offset = pixelOffset;
                        }
                        else
                        {
                            // Convert to new 32bpp bitmap
                            int fileSize = 14 + 40 + imageSize32;
                            var newData = new byte[fileSize];

                            // BITMAPFILEHEADER
                            newData[0] = (byte)'B';
                            newData[1] = (byte)'M';
                            newData[2] = (byte)(fileSize & 0xFF);
                            newData[3] = (byte)((fileSize >> 8) & 0xFF);
                            newData[4] = (byte)((fileSize >> 16) & 0xFF);
                            newData[5] = (byte)((fileSize >> 24) & 0xFF);
                            int newPixelOffset = 54;
                            newData[10] = (byte)(newPixelOffset & 0xFF);
                            newData[11] = (byte)((newPixelOffset >> 8) & 0xFF);
                            newData[12] = (byte)((newPixelOffset >> 16) & 0xFF);
                            newData[13] = (byte)((newPixelOffset >> 24) & 0xFF);

                            // BITMAPINFOHEADER
                            newData[14] = 40; // header size
                            newData[18] = (byte)(width & 0xFF);
                            newData[19] = (byte)((width >> 8) & 0xFF);
                            newData[20] = (byte)((width >> 16) & 0xFF);
                            newData[21] = (byte)((width >> 24) & 0xFF);
                            newData[22] = (byte)(heightRaw & 0xFF);
                            newData[23] = (byte)((heightRaw >> 8) & 0xFF);
                            newData[24] = (byte)((heightRaw >> 16) & 0xFF);
                            newData[25] = (byte)((heightRaw >> 24) & 0xFF);
                            newData[26] = 1; // planes
                            newData[28] = 32; // bitcount
                            newData[34] = (byte)(imageSize32 & 0xFF);
                            newData[35] = (byte)((imageSize32 >> 8) & 0xFF);
                            newData[36] = (byte)((imageSize32 >> 16) & 0xFF);
                            newData[37] = (byte)((imageSize32 >> 24) & 0xFF);

                            byte tr2 = transparentColor.R;
                            byte tg2 = transparentColor.G;
                            byte tb2 = transparentColor.B;

                            for (int y = 0; y < absHeight; y++)
                            {
                                int srcRowIndex = topDown ? y : (absHeight - 1 - y);
                                int dstRowIndex = topDown ? y : (absHeight - 1 - y);
                                int srcRowStart = pixelOffset + srcRowIndex * strideSrc;
                                int dstRowStart = newPixelOffset + dstRowIndex * stride32;
                                if (srcRowStart < 0 || srcRowStart + strideSrc > data.Length) break;
                                for (int x = 0; x < width; x++)
                                {
                                    byte r, g, b;
                                    switch (bpp)
                                    {
                                        case 24:
                                            {
                                                int sp = srcRowStart + x * 3;
                                                b = data[sp + 0];
                                                g = data[sp + 1];
                                                r = data[sp + 2];
                                                break;
                                            }
                                        case 16:
                                            {
                                                int sp = srcRowStart + x * 2;
                                                if (sp + 1 >= data.Length) { r = g = b = 0; break; }
                                                int val = data[sp + 0] | (data[sp + 1] << 8);
                                                r = (byte)(((val >> 10) & 0x1F) * 255 / 31);
                                                g = (byte)(((val >> 5) & 0x1F) * 255 / 31);
                                                b = (byte)((val & 0x1F) * 255 / 31);
                                                break;
                                            }
                                        case 8:
                                            {
                                                int sp = srcRowStart + x;
                                                int idx = data[sp];
                                                if (palette != null && idx < palette.Length)
                                                {
                                                    var c = palette[idx]; r = c.R; g = c.G; b = c.B;
                                                }
                                                else { r = g = b = 0; }
                                                break;
                                            }
                                        case 4:
                                            {
                                                int sp = srcRowStart + (x >> 1);
                                                byte val = data[sp];
                                                int idx = ((x & 1) == 0) ? (val >> 4) & 0x0F : val & 0x0F;
                                                if (palette != null && idx < palette.Length)
                                                {
                                                    var c = palette[idx]; r = c.R; g = c.G; b = c.B;
                                                }
                                                else { r = g = b = 0; }
                                                break;
                                            }
                                        case 1:
                                            {
                                                int sp = srcRowStart + (x >> 3);
                                                byte val = data[sp];
                                                int idx = (val >> (7 - (x & 7))) & 0x01;
                                                if (palette != null && idx < palette.Length)
                                                {
                                                    var c = palette[idx]; r = c.R; g = c.G; b = c.B;
                                                }
                                                else { r = g = b = 0; }
                                                break;
                                            }
                                        default:
                                            r = g = b = 0; break;
                                    }

                                    int dp = dstRowStart + x * 4;
                                    if ((r == tr2 && g == tg2 && b == tb2))
                                    {
                                        newData[dp + 0] = 0xFF;
                                        newData[dp + 1] = 0xFF;
                                        newData[dp + 2] = 0xFF;
                                        newData[dp + 3] = 0;
                                    }
                                    else
                                    {
                                        newData[dp + 0] = b;
                                        newData[dp + 1] = g;
                                        newData[dp + 2] = r;
                                        newData[dp + 3] = 0xFF;
                                    }
                                }
                            }

                            bmp32 = newData;
                            bmp32Offset = newPixelOffset;
                        }

                        // Convert processed 32bpp BGRA bitmap to PNG RGBA
                        var rgba = new byte[width * absHeight * 4];
                        for (int y = 0; y < absHeight; y++)
                        {
                            int srcRowIndex = bmp32TopDown ? y : (absHeight - 1 - y);
                            int srcRowStart = bmp32Offset + srcRowIndex * stride32;
                            int dstRowStart = y * width * 4;
                            if (srcRowStart < 0 || srcRowStart + stride32 > bmp32.Length) break;
                            for (int x = 0; x < width; x++)
                            {
                                int sp = srcRowStart + x * 4;
                                byte b = bmp32[sp + 0];
                                byte g = bmp32[sp + 1];
                                byte r = bmp32[sp + 2];
                                byte a = bmp32[sp + 3];
                                int dp = dstRowStart + x * 4;
                                rgba[dp + 0] = r;
                                rgba[dp + 1] = g;
                                rgba[dp + 2] = b;
                                rgba[dp + 3] = a;
                            }
                        }
                        var pngBytes = EncodeRgbaToPng(rgba, width, absHeight);
                        this._Data = pngBytes;
                        this._TransparentColor = transparentColor;
                        this._RawFormat = Imaging.ImageFormat.Png;
                        this._PixelFormat = PixelFormat.Format32bppArgb;
                        return;
                    }
                }
            }
            // Fallback: mark transparent color but leave data unchanged to avoid runtime errors on unsupported BMP subformats
            this._TransparentColor = transparentColor;
            return;
        }

        /// <include file='doc\Bitmap.uex' path='docs/doc[@for="Bitmap.LockBits"]/*' />
        /// <devdoc>
        ///    Locks a Bitmap into system memory.
        /// </devdoc>
        //[SecurityPermission(SecurityAction.LinkDemand, Flags=SecurityPermissionFlag.UnmanagedCode)]
        public BitmapData LockBits(Rectangle rect, ImageLockMode flags, PixelFormat format)
        {
            Contract.Ensures(Contract.Result<BitmapData>() != null);

            BitmapData bitmapData = new BitmapData();

            return LockBits(rect, flags, format, bitmapData);
        }

        /// <include file='doc\Bitmap.uex' path='docs/doc[@for="Bitmap.LockBits1"]/*' />
        /// <devdoc>
        ///    Locks a Bitmap into system memory.  This overload takes a user-defined
        ///    BitmapData object and is intended to be used with an ImageLockMode.UserInputBuffer.
        /// </devdoc>
        //[SecurityPermission(SecurityAction.LinkDemand, Flags=SecurityPermissionFlag.UnmanagedCode)]
        public BitmapData LockBits(Rectangle rect, ImageLockMode flags, PixelFormat format, BitmapData bitmapData)
        {
            Contract.Ensures(Contract.Result<BitmapData>() != null);

            GPRECT gprect = new GPRECT(rect);
            int status = SafeNativeMethods.Gdip.GdipBitmapLockBits(new HandleRef(this, nativeImage), ref gprect,
                                                    flags, format, bitmapData);

            if (status != SafeNativeMethods.Gdip.Ok)
                throw SafeNativeMethods.Gdip.StatusException(status);

            return bitmapData;
        }


        /// <include file='doc\Bitmap.uex' path='docs/doc[@for="Bitmap.UnlockBits"]/*' />
        /// <devdoc>
        ///    Unlocks this <see cref='System.Drawing.Bitmap'/> from system memory.
        /// </devdoc>
        //[SecurityPermission(SecurityAction.LinkDemand, Flags=SecurityPermissionFlag.UnmanagedCode)]
        public void UnlockBits(BitmapData bitmapdata)
        {
            int status = SafeNativeMethods.Gdip.GdipBitmapUnlockBits(new HandleRef(this, nativeImage), bitmapdata);

            if (status != SafeNativeMethods.Gdip.Ok)
                throw SafeNativeMethods.Gdip.StatusException(status);
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public Color GetPixel(int x, int y)
        {
            if (x < 0 || x >= Width)
            {
                throw new ArgumentOutOfRangeException("x", DCSR.GetString(DCSR.ValidRangeX));
            }
            if (y < 0 || y >= Height)
            {
                throw new ArgumentOutOfRangeException("y", DCSR.GetString(DCSR.ValidRangeY));
            }

            // Read directly from _Data when possible
            var data = this._Data;
            if (data == null || data.Length == 0)
            {
                // No raw data available
                return Color.Empty;
            }

            // BMP support (24bpp and 32bpp BI_RGB)
            if (data.Length >= 26 && data[0] == 0x42 && data[1] == 0x4D)
            {
                int pixelOffset = data[10] | (data[11] << 8) | (data[12] << 16) | (data[13] << 24);
                int dibHeaderSize = data[14] | (data[15] << 8) | (data[16] << 16) | (data[17] << 24);
                if (dibHeaderSize >= 40 && data.Length >= 14 + dibHeaderSize)
                {
                    int width = data[18] | (data[19] << 8) | (data[20] << 16) | (data[21] << 24);
                    int heightRaw = data[22] | (data[23] << 8) | (data[24] << 16) | (data[25] << 24);
                    int bpp = data[28] | (data[29] << 8);
                    bool topDown = heightRaw < 0;
                    int height = Math.Abs(heightRaw);
                    if (width != this._Width || height != this._Height)
                    {
                        // Dimensions mismatch; use parsed ones for indexing
                    }

                    if (bpp == 24 || bpp == 32)
                    {
                        int bytesPerPixel = bpp / 8;
                        // stride is padded to 4 bytes boundary
                        int rawStride = ((width * bpp + 31) / 32) * 4;
                        int rowIndex = topDown ? y : (height - 1 - y);
                        int rowStart = pixelOffset + rowIndex * rawStride;
                        int p = rowStart + x * bytesPerPixel;
                        if (p < 0 || p + bytesPerPixel > data.Length) return Color.Empty;
                        byte b = data[p + 0];
                        byte g = data[p + 1];
                        byte r = data[p + 2];
                        byte a = 255;
                        if (bpp == 32)
                        {
                            a = data[p + 3];
                        }
                        return Color.FromArgb(a, r, g, b);
                    }
                }
            }
            var pngDecodedPixels = GetPngDecodedPixels();
            if (pngDecodedPixels != null)
            {
                if (x >= _PngWidth || y >= _PngHeight) throw new ArgumentOutOfRangeException();
                int rowOffset2 = y * _PngWidth * _PngBytesPerPixel;
                int idx2 = rowOffset2 + x * _PngBytesPerPixel;
                if (_PngBytesPerPixel == 4)
                {
                    byte r = _PngDecodedPixels[idx2 + 0];
                    byte g = _PngDecodedPixels[idx2 + 1];
                    byte b = _PngDecodedPixels[idx2 + 2];
                    byte a = _PngDecodedPixels[idx2 + 3];
                    return Color.FromArgb(a, r, g, b);
                }
                else
                {
                    byte r = _PngDecodedPixels[idx2 + 0];
                    byte g = _PngDecodedPixels[idx2 + 1];
                    byte b = _PngDecodedPixels[idx2 + 2];
                    return Color.FromArgb(255, r, g, b);
                }
            }
            return Color.Empty;
            // Other formats unsupported
            throw new NotSupportedException("Only BMP and PNG formats are supported.");
        }
        private byte[] GetPngDecodedPixels()
        {
            var data = this._Data;
            if (data != null && data.Length >= 8 && data[0] == 0x89 && data[1] == 0x50 && data[2] == 0x4E && data[3] == 0x47 &&
                data[4] == 0x0D && data[5] == 0x0A && data[6] == 0x1A && data[7] == 0x0A)
            {
                if (this._PngDecodedPixels == null)
                {
                    if (data.Length < 33) throw new NotSupportedException("PNG too small.");
                    int width = (data[16] << 24) | (data[17] << 16) | (data[18] << 8) | data[19];
                    int height = (data[20] << 24) | (data[21] << 16) | (data[22] << 8) | data[23];
                    int bitDepth = data[24];
                    int colorType = data[25];
                    int interlace = data[28];
                    if (interlace != 0) throw new NotSupportedException("Interlaced PNG not supported.");
                    if (bitDepth != 8) throw new NotSupportedException("Only 8-bit PNG supported.");
                    int bytesPerPixel = colorType == 6 ? 4 : (colorType == 2 ? 3 : -1);
                    if (bytesPerPixel <= 0) throw new NotSupportedException("Only truecolor and RGBA PNG supported.");

                    var comp = GetDecompressedDataFromPNG();
                    if (comp == null) throw new NotSupportedException("PNG IDAT missing or failed to decompress.");
                    int stride = width * bytesPerPixel;
                    int srcStride = stride + 1;
                    int expected = srcStride * height;
                    if (comp.Length < expected) throw new NotSupportedException("PNG data length mismatch.");
                    _PngDecodedPixels = new byte[height * stride];
                    int prevRowStart = -1;
                    for (int row = 0; row < height; row++)
                    {
                        int compRowStart = row * srcStride;
                        byte filter = comp[compRowStart];
                        int outRowStart = row * stride;
                        switch (filter)
                        {
                            case 0:
                                Buffer.BlockCopy(comp, compRowStart + 1, _PngDecodedPixels, outRowStart, stride);
                                break;
                            case 1:
                                for (int i = 0; i < stride; i++)
                                {
                                    int left = i >= bytesPerPixel ? _PngDecodedPixels[outRowStart + i - bytesPerPixel] : 0;
                                    _PngDecodedPixels[outRowStart + i] = (byte)(unchecked(comp[compRowStart + 1 + i] + left) & 0xFF);
                                }
                                break;
                            case 2:
                                for (int i = 0; i < stride; i++)
                                {
                                    int up = prevRowStart >= 0 ? _PngDecodedPixels[prevRowStart + i] : 0;
                                    _PngDecodedPixels[outRowStart + i] = (byte)(unchecked(comp[compRowStart + 1 + i] + up) & 0xFF);
                                }
                                break;
                            case 3:
                                for (int i = 0; i < stride; i++)
                                {
                                    int left = i >= bytesPerPixel ? _PngDecodedPixels[outRowStart + i - bytesPerPixel] : 0;
                                    int up = prevRowStart >= 0 ? _PngDecodedPixels[prevRowStart + i] : 0;
                                    int avg = (left + up) >> 1;
                                    _PngDecodedPixels[outRowStart + i] = (byte)(unchecked(comp[compRowStart + 1 + i] + avg) & 0xFF);
                                }
                                break;
                            case 4:
                                for (int i = 0; i < stride; i++)
                                {
                                    int a = i >= bytesPerPixel ? _PngDecodedPixels[outRowStart + i - bytesPerPixel] : 0;
                                    int b = prevRowStart >= 0 ? _PngDecodedPixels[prevRowStart + i] : 0;
                                    int c = (prevRowStart >= 0 && i >= bytesPerPixel) ? _PngDecodedPixels[prevRowStart + i - bytesPerPixel] : 0;
                                    int p = a + b - c;
                                    int pa = Math.Abs(p - a);
                                    int pb = Math.Abs(p - b);
                                    int pc = Math.Abs(p - c);
                                    int pr = (pa <= pb && pa <= pc) ? a : (pb <= pc ? b : c);
                                    _PngDecodedPixels[outRowStart + i] = (byte)(unchecked(comp[compRowStart + 1 + i] + pr) & 0xFF);
                                }
                                break;
                            default:
                                throw new NotSupportedException("Unsupported PNG filter.");
                        }
                        prevRowStart = outRowStart;
                    }
                    _PngBytesPerPixel = bytesPerPixel;
                    _PngWidth = width;
                    _PngHeight = height;
                }
            }
            return this._PngDecodedPixels;
        }
        // Cache for PNG decoded pixels
        private byte[] _PngDecodedPixels = null;
        private int _PngBytesPerPixel = 0;
        private int _PngWidth = 0;
        private int _PngHeight = 0;
        /// <summary>
        /// 当图片格式为PNG格式，则获得解压缩后的图片数据
        /// </summary>
        /// <returns>解压缩后的数据</returns>
        /// <remarks>内部使用_DecompressedFromPNG来缓存数据</remarks>
        private byte[] GetDecompressedDataFromPNG()
        {
            // Return cached data if present
            if (_DecompressedFromPNG != null)
            {
                return _DecompressedFromPNG;
            }

            var data = this._Data;
            if (data == null || data.Length < 8)
            {
                return null;
            }
            // PNG signature check
            if (!(data[0] == 0x89 && data[1] == 0x50 && data[2] == 0x4E && data[3] == 0x47 &&
                  data[4] == 0x0D && data[5] == 0x0A && data[6] == 0x1A && data[7] == 0x0A))
            {
                return null;
            }

            // Collect IDAT payloads
            int offset = 8; // start after signature
            using (var msCompressed = new MemoryStream())
            {
                // Parse chunks
                while (offset + 8 <= data.Length)
                {
                    // length (BE 4) + type (4)
                    uint len = (uint)((data[offset] << 24) | (data[offset + 1] << 16) | (data[offset + 2] << 8) | data[offset + 3]);
                    string type = System.Text.Encoding.ASCII.GetString(data, offset + 4, 4);
                    int payloadStart = offset + 8;
                    int payloadEnd = payloadStart + (int)len;
                    if (payloadEnd > data.Length) break;

                    if (type == "IDAT")
                    {
                        msCompressed.Write(data, payloadStart, (int)len);
                    }

                    // advance: length + type + data + crc(4)
                    offset = payloadEnd + 4;
                    // IEND terminates
                    if (type == "IEND") break;
                }

                if (msCompressed.Length == 0)
                {
                    return null;
                }

                // IDAT stream is zlib (RFC1950) wrapping deflate (RFC1951)
                msCompressed.Position = 0;
                using (var zlib = new MemoryStream())
                {
#if NET8_0_OR_GREATER
                    using (var zds = new System.IO.Compression.ZLibStream(msCompressed, System.IO.Compression.CompressionMode.Decompress, leaveOpen: true))
                    {
                        zds.CopyTo(zlib);
                    }
#else
                        var buf = msCompressed.ToArray();
                        if (buf.Length <= 6) return null;
                        using (var deflate = new System.IO.Compression.DeflateStream(new MemoryStream(buf, 2, buf.Length - 6), System.IO.Compression.CompressionMode.Decompress, leaveOpen: true))
                        {
                            deflate.CopyTo(zlib);
                        }
#endif
                    _DecompressedFromPNG = zlib.ToArray();
                    return _DecompressedFromPNG;
                }
            }
        }
        /// <summary>
        /// 从PNG图片解压缩出来的数据
        /// </summary>
        private byte[] _DecompressedFromPNG = null;

        /// <include file='doc\Bitmap.uex' path='docs/doc[@for="Bitmap.SetPixel"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Sets the color of the specified pixel in this <see cref='System.Drawing.Bitmap'/> .
        ///    </para>
        /// </devdoc>
        public void SetPixel(int x, int y, Color color)
        {
            if ((PixelFormat & PixelFormat.Indexed) != 0)
            {
                throw new InvalidOperationException(DCSR.GetString(DCSR.GdiplusCannotSetPixelFromIndexedPixelFormat));
            }

            if (x < 0 || x >= Width)
            {
                throw new ArgumentOutOfRangeException("x", DCSR.GetString(DCSR.ValidRangeX));
            }

            if (y < 0 || y >= Height)
            {
                throw new ArgumentOutOfRangeException("y", DCSR.GetString(DCSR.ValidRangeY));
            }

            int status = SafeNativeMethods.Gdip.GdipBitmapSetPixel(new HandleRef(this, nativeImage), x, y, color.ToArgb());

            if (status != SafeNativeMethods.Gdip.Ok)
                throw SafeNativeMethods.Gdip.StatusException(status);
        }

        /// <include file='doc\Bitmap.uex' path='docs/doc[@for="Bitmap.SetResolution"]/*' />
        /// <devdoc>
        ///    Sets the resolution for this <see cref='System.Drawing.Bitmap'/>.
        /// </devdoc>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public void SetResolution(float xDpi, float yDpi)
        {
            int status = SafeNativeMethods.Gdip.GdipBitmapSetResolution(new HandleRef(this, nativeImage), xDpi, yDpi);

            if (status != SafeNativeMethods.Gdip.Ok)
                throw SafeNativeMethods.Gdip.StatusException(status);
        }

        private static uint ComputeCrc32(byte[] buffer, int offset, int count)
        {
            uint crc = 0xFFFFFFFFu;
            for (int i = 0; i < count; i++)
            {
                crc ^= buffer[offset + i];
                for (int k = 0; k < 8; k++)
                {
                    if ((crc & 1) != 0)
                        crc = 0xEDB88320u ^ (crc >> 1);
                    else
                        crc >>= 1;
                }
            }
            return crc ^ 0xFFFFFFFFu;
        }

        private static void WritePngChunk(MemoryStream ms, string type, byte[] payload)
        {
            byte[] typeBytes = System.Text.Encoding.ASCII.GetBytes(type);
            int len = payload?.Length ?? 0;
            ms.WriteByte((byte)((len >> 24) & 0xFF));
            ms.WriteByte((byte)((len >> 16) & 0xFF));
            ms.WriteByte((byte)((len >> 8) & 0xFF));
            ms.WriteByte((byte)(len & 0xFF));
            ms.Write(typeBytes, 0, 4);
            if (payload != null && payload.Length > 0)
            {
                ms.Write(payload, 0, payload.Length);
            }
            byte[] crcInput;
            if (payload != null && payload.Length > 0)
            {
                crcInput = new byte[typeBytes.Length + payload.Length];
                Buffer.BlockCopy(typeBytes, 0, crcInput, 0, typeBytes.Length);
                Buffer.BlockCopy(payload, 0, crcInput, typeBytes.Length, payload.Length);
            }
            else
            {
                crcInput = typeBytes;
            }
            uint crc = ComputeCrc32(crcInput, 0, crcInput.Length);
            ms.WriteByte((byte)((crc >> 24) & 0xFF));
            ms.WriteByte((byte)((crc >> 16) & 0xFF));
            ms.WriteByte((byte)((crc >> 8) & 0xFF));
            ms.WriteByte((byte)(crc & 0xFF));
        }

        private static byte[] EncodeRgbaToPng(byte[] rgba, int width, int height)
        {
            using var ms = new MemoryStream();
            // PNG signature
            ms.Write(new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A });

            // IHDR
            var ihdr = new byte[13];
            ihdr[0] = (byte)((width >> 24) & 0xFF);
            ihdr[1] = (byte)((width >> 16) & 0xFF);
            ihdr[2] = (byte)((width >> 8) & 0xFF);
            ihdr[3] = (byte)(width & 0xFF);
            ihdr[4] = (byte)((height >> 24) & 0xFF);
            ihdr[5] = (byte)((height >> 16) & 0xFF);
            ihdr[6] = (byte)((height >> 8) & 0xFF);
            ihdr[7] = (byte)(height & 0xFF);
            ihdr[8] = 8; // bit depth
            ihdr[9] = 6; // color type RGBA
            ihdr[10] = 0; // compression
            ihdr[11] = 0; // filter
            ihdr[12] = 0; // interlace
            WritePngChunk(ms, "IHDR", ihdr);

            // IDAT: build scanlines with filter 0
            int stride = width * 4;
            var raw = new byte[(stride + 1) * height];
            for (int y = 0; y < height; y++)
            {
                int srcRow = y * stride;
                int dstRow = y * (stride + 1);
                raw[dstRow] = 0; // filter type 0
                Buffer.BlockCopy(rgba, srcRow, raw, dstRow + 1, stride);
            }
            byte[] compressed;
            using (var compMs = new MemoryStream())
            {
                using (var z = new ZLibStream(compMs, CompressionMode.Compress, leaveOpen: true))
                {
                    z.Write(raw, 0, raw.Length);
                }
                compressed = compMs.ToArray();
            }
            WritePngChunk(ms, "IDAT", compressed);

            // IEND
            WritePngChunk(ms, "IEND", Array.Empty<byte>());
            return ms.ToArray();
        }
    }
}
