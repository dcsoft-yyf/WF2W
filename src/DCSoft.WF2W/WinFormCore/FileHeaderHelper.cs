using System;
using System.Text;
using System.IO;

namespace DCSoft
{
    /// <summary>
    /// 文件头判断 编写袁永福
    /// </summary>
    internal static class FileHeaderHelper
    {
        public static int DetectEncodingByBOM(byte[] byteBuffer, int startPosition, ref System.Text.Encoding encoding)
        {
            if (byteBuffer == null || byteBuffer.Length <= startPosition)
            {
                return 0;
            }
            int byteLen = byteBuffer.Length - startPosition;
            if (byteLen < 2)
            {
                return 0;
            }
            if (byteBuffer[startPosition + 0] == 254
                && byteBuffer[startPosition + 1] == byte.MaxValue)
            {
                encoding = System.Text.Encoding.BigEndianUnicode;// _Unicode_Big;// new UnicodeEncoding(bigEndian: true, byteOrderMark: true);
                return 2;
            }
            else if (byteBuffer[startPosition + 0] == byte.MaxValue
                && byteBuffer[startPosition + 1] == 254)
            {
                if (byteLen < 4 || byteBuffer[2] != 0 || byteBuffer[3] != 0)
                {
                    encoding = System.Text.Encoding.Unicode;// _Unicode_Little;// new UnicodeEncoding(bigEndian: false, byteOrderMark: true);
                    return 2;
                }
                else
                {
                    encoding = System.Text.Encoding.Unicode;// _Unicode_Little;// new UTF32Encoding(bigEndian: false, byteOrderMark: true);
                    return 4;
                }
            }
            else if (byteLen >= 3
                && byteBuffer[startPosition + 0] == 239
                && byteBuffer[startPosition + 1] == 187
                && byteBuffer[startPosition + 2] == 191)
            {
                encoding = Encoding.UTF8;
                return 3;
            }
            else if (byteLen >= 4
                && byteBuffer[startPosition + 0] == 0
                && byteBuffer[startPosition + 1] == 0
                && byteBuffer[startPosition + 2] == 254
                && byteBuffer[startPosition + 3] == byte.MaxValue)
            {
                encoding = System.Text.Encoding.BigEndianUnicode;// _Unicode_Big;// new UTF32Encoding(bigEndian: true, byteOrderMark: true);
                return 4;
            }
            else if (byteLen == 2)
            {
                return 0;
            }
            return 0;
        }

        /// <summary>
        /// 判断数据块是否具有ZIP文件头
        /// </summary>
        /// <param name="bs">数据块</param>
        /// <returns>是否具有ZIP文件头</returns>
        public static bool HasZIPHeader(byte[] bs)
        {
            if (bs != null && bs.Length >= 4)
            {
                if (bs[0] == 0x50 && bs[1] == 0x4b && bs[2] == 0x03 && bs[3] == 0x04)
                {
                    return true;
                }
            }
            return false;
        }

        ///// <summary>
        ///// 判断数据块是否具有RAR文件标记头
        ///// </summary>
        ///// <param name="bs">数据块</param>
        ///// <returns>是否具有RAR文件标记头</returns>
        //public static bool HasRARHeader(byte[] bs)
        //{
        //    if (bs != null && bs.Length >= 3)
        //    {
        //        if (bs[0] == 0x52 && bs[1] == 0x61 && bs[2] == 0x72)
        //        {
        //            // 字符串 Rar
        //            return true;
        //        }
        //    }
        //    return false;
        //}
        ///// <summary>
        ///// 判断数据块是否具有PDF文件头
        ///// </summary>
        ///// <param name="bs">数据块</param>
        ///// <returns>是否具有PDF文件头</returns>
        //public static bool HasPDFHeader(byte[] bs)
        //{
        //    if (bs != null && bs.Length >= 4)
        //    {
        //        if (bs[0] == 0x25 && bs[1] == 0x50 && bs[2] == 0x44 && bs[3] == 0x46)
        //        {
        //            // %PDF
        //            return true;
        //        }
        //    }
        //    return false;
        //}

        ///// <summary>
        ///// 判断数据块是否具有RTF文件标记头
        ///// </summary>
        ///// <param name="bs">数据块</param>
        ///// <returns>是否具有RTF标记头</returns>
        //public static bool HasRTFHeader(byte[] bs)
        //{
        //    if (bs != null && bs.Length > 5)
        //    {
        //        string txt = System.Text.Encoding.ASCII.GetString(bs, 0, 5);
        //        if (txt == "{\rtf")
        //            return true;
        //    }
        //    return false;
        //}
        ///// <summary>
        ///// 判断是否具有PCX文件头
        ///// </summary>
        ///// <param name="bs"></param>
        ///// <returns></returns>
        //public static bool HasPCXHeader(byte[] bs)
        //{
        //    if (bs != null && bs.Length >= 2)
        //    {
        //        if (bs[0] == 0x0a)
        //        {
        //            if (bs[1] == 0 || bs[1] == 2 || bs[1] == 3 || bs[1] == 4 || bs[1] == 5)
        //            {
        //                return true;
        //            }
        //        }
        //    }
        //    return false;
        //}

        ///// <summary>
        ///// 判断数据块是否具有PSD图像文件标记头
        ///// </summary>
        ///// <param name="bs">数据块</param>
        ///// <returns>是否具有PSD图像文件标记头</returns>
        //public static bool HasPSDHeader(byte[] bs)
        //{
        //    if (bs != null && bs.Length >= 2)
        //    {
        //        if (bs[0] == 0x38 && bs[1] == 0x42)
        //            return true;
        //    }
        //    return false;
        //}

        ///// <summary>
        ///// 判断数据块是否具有TIFF图像文件标记头
        ///// </summary>
        ///// <param name="bs">数据块</param>
        ///// <returns>是否具有TIFF图像文件标记头</returns>
        //public static bool HasTIFFHeader(byte[] bs)
        //{
        //    if (bs != null && bs.Length >= 2)
        //    {
        //        if (bs[0] == 0x49 && bs[1] == 0x49)
        //        {
        //            // 文本 II
        //            return true;
        //        }
        //        if (bs[0] == 0x4d && bs[1] == 0x4d)
        //        {
        //            // 文本 MM
        //            return true;
        //        }
        //    }
        //    return false;
        //}
        ///// <summary>
        ///// 判断数据块是否具有WMF图像文件标记头
        ///// </summary>
        ///// <param name="bs">数据块</param>
        ///// <returns>是否具有WMF图像文件标记头</returns>
        //public static bool HasWMFHeader(byte[] bs)
        //{
        //    if (bs != null && bs.Length >= 2)
        //    {
        //        if (bs[0] == 0xd7 && bs[1] == 0xcd)
        //            return true;
        //    }
        //    return false;
        //}

        ///// <summary>
        ///// 判断数据块是否具有图标文件标记头
        ///// </summary>
        ///// <param name="bs">数据块</param>
        ///// <returns>是否具有图标文件标记头</returns>
        //public static bool HasICONHeader(byte[] bs)
        //{
        //    if (bs != null && bs.Length >= 4)
        //    {
        //        if (bs[0] == 0 && bs[1] == 0)
        //        {
        //            if (bs[2] == 1 && bs[3] == 0)
        //            {
        //                // 图标
        //                return true;
        //            }
        //            if (bs[2] == 2 && bs[3] == 0)
        //            {
        //                // 光标
        //                return true;
        //            }
        //        }
        //    }
        //    return false;
        //}

        private readonly static byte[] pngHeader = new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A };
        /// <summary>
        /// 判断数据块是否具有PNG图片文件标记头
        /// </summary>
        /// <param name="bs">数据块</param>
        /// <returns>是否具有PNG图片文件标记头</returns>
        public static bool HasPNGHeader(byte[] bs)
        {
            if (bs != null && bs.Length >= pngHeader.Length)
            {
                for (int iCount = 0; iCount < pngHeader.Length; iCount++)
                {
                    if (bs[iCount] != pngHeader[iCount])
                        return false;
                }
                return true;
            }
            return false;
        }


        /// <summary>
        /// 判断数据块是否具有GIF图像文件标记头
        /// </summary>
        /// <param name="bs">数据块</param>
        /// <returns>是否具有GIF图像文件标记头</returns>
        public static bool HasGIFHeader(byte[] bs)
        {
            if (bs != null && bs.Length >= 6)
            {
                if (bs[0] == 0x47 && bs[1] == 0x49 && bs[2] == 0x46)
                {
                    if (bs[3] == 0x38 && bs[4] == 0x37 && bs[5] == 0x61)
                    {
                        // 以 GIF87a 开头
                        return true;
                    }
                    if (bs[3] == 0x38 && bs[4] == 0x39 && bs[5] == 0x61)
                    {
                        // 以 GIF89a 开头
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 判断数据块是否具有BMP标记头
        /// </summary>
        /// <param name="bs">数据块</param>
        /// <returns>是否具有BMP标记头</returns>
        public static bool HasBMPHeader(byte[] bs)
        {
            if (bs != null && bs.Length >= 2)
            {
                if (bs[0] == 0x42 && bs[1] == 0x4d)
                    return true;
            }
            return false;
        }
        /// <summary>
        /// 判断二进制数据是否具有JPEG格式的标记头
        /// </summary>
        /// <param name="bs"></param>
        /// <returns></returns>
        public static bool HasJpegHeader(byte[] bs)
        {
            return HasJpegHeader(bs, false);
        }
        /// <summary>
        /// 判断二进制数据是否具有JPEG格式的标记头
        /// </summary>
        /// <param name="bs">二进制数据</param>
        /// <param name="strict">是否进行严格的判断</param>
        /// <returns>是否有JPEG标记头</returns>
        public static bool HasJpegHeader(byte[] bs, bool strict)
        {
            if (bs != null && bs.Length >= 12)
            {
                int length = bs.Length;
                if (strict)
                {
                    if (bs[length - 2] != 0xff
                        || bs[length - 1] != 0xd9)
                    {
                        return false;
                    }
                }
                //调整判断JPG的逻辑，只判断开头为FFD8则通过
                if (bs[0] == 0xff
                    && bs[1] == 0xd8
                    && bs[2] == 0xff
                    //&& bs[10] == 0x00
                    //&& bs[length - 2] == 0xff
                    //&& bs[length - 1] == 0xd9
                    )
                {
                    return true;
                    //if (bs[3] == 0xe0
                    //    && bs[6] == 0x4a
                    //    && bs[7] == 0x46
                    //    && bs[8] == 0x49
                    //    && bs[9] == 0x46)//JFIF
                    //{
                    //    return true;
                    //}

                    //if (bs[3] == 0xe1
                    //    && bs[6] == 0x45
                    //    && bs[7] == 0x78
                    //    && bs[8] == 0x69
                    //    && bs[9] == 0x66)//Exif
                    //{
                    //    return true;
                    //}
                }
            }
            return false;
        }

        //private static bool BinaryCompare(byte[] bs, int index, byte[] bs2)
        //{
        //    if (bs == null)
        //        throw new ArgumentNullException("bs");
        //    if (bs2 == null)
        //        throw new ArgumentNullException("bs2");
        //    if (index < 0 || index > bs.Length - bs2.Length)
        //        throw new ArgumentOutOfRangeException("index");
        //    for (int iCount = 0; iCount < bs2.Length; iCount++)
        //    {
        //        if (bs[iCount + index] != bs2[iCount])
        //            return false;
        //    }
        //    return true;
        //}
    }
}