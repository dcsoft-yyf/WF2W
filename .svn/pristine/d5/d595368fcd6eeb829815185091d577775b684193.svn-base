using System;
using System.Text;
using System.IO;

namespace DCSoft.Common
{
    /// <summary>
    /// 文件头判断 编写袁永福
    /// </summary>
#if DCWriterForWASM
    [System.Reflection.Obfuscation( Exclude = true , ApplyToMembers = true )]
#endif
    [System.Runtime.InteropServices.ComVisible(false)]
    public static class FileHeaderHelper
    {
        //public static readonly byte[] _UTF8BOM = new byte[] {239,187,191 };// EF BB BF

        //private static readonly System.Text.Encoding _Unicode_Big = new UnicodeEncoding(bigEndian: true, byteOrderMark: true);
        //private static readonly System.Text.Encoding _Unicode_Little = new UnicodeEncoding(bigEndian: false, byteOrderMark: true);

        //public static int DetectEncodingByBOM(byte[] byteBuffer, int startPosition, ref System.Text.Encoding encoding)
        //{
        //    if (byteBuffer == null || byteBuffer.Length <= startPosition)
        //    {
        //        return 0;
        //    }
        //    int byteLen = byteBuffer.Length - startPosition;
        //    if (byteLen < 2)
        //    {
        //        return 0;
        //    }
        //    if (byteBuffer[startPosition + 0] == 254
        //        && byteBuffer[startPosition + 1] == byte.MaxValue)
        //    {
        //        encoding = new UnicodeEncoding(bigEndian: true, byteOrderMark: true);
        //        return 2;
        //    }
        //    else if (byteBuffer[startPosition + 0] == byte.MaxValue
        //        && byteBuffer[startPosition + 1] == 254)
        //    {
        //        if (byteLen < 4 || byteBuffer[2] != 0 || byteBuffer[3] != 0)
        //        {
        //            encoding = _Unicode_Little;// new UnicodeEncoding(bigEndian: false, byteOrderMark: true);
        //            return 2;
        //        }
        //        else
        //        {
        //            encoding = _Unicode_Little;// new UTF32Encoding(bigEndian: false, byteOrderMark: true);
        //            return 4;
        //        }
        //    }
        //    else if (byteLen >= 3
        //        && byteBuffer[startPosition + 0] == 239 // EF
        //        && byteBuffer[startPosition + 1] == 187 // BB
        //        && byteBuffer[startPosition + 2] == 191) // BF
        //    {
        //        encoding = Encoding.UTF8;
        //        return 3;
        //    }
        //    else if (byteLen >= 4
        //        && byteBuffer[startPosition + 0] == 0
        //        && byteBuffer[startPosition + 1] == 0
        //        && byteBuffer[startPosition + 2] == 254
        //        && byteBuffer[startPosition + 3] == byte.MaxValue)
        //    {
        //        encoding = _Unicode_Big;// new UTF32Encoding(bigEndian: true, byteOrderMark: true);
        //        return 4;
        //    }
        //    else if (byteLen == 2)
        //    {
        //        return 0;
        //    }
        //    return 0;
        //}
//#if ! DCWriterForWASM
        



       

//        public static byte[] Clear_UTF8_BOMHeader(byte[] bs)
//        {
//            if (bs != null && bs.Length > 4)
//            {
//                if (bs[0] == 239 && bs[1] == 187 && bs[2] == 191)
//                {
//                    var bs2 = new byte[bs.Length - 3];
//                    Array.Copy(bs, 3, bs2, 0, bs.Length - 3);
//                    return bs2;
//                }
//            }
//            return bs;
//        }

//        /// <summary>
//        /// 判断前3个字节是否为UTF8的BOM标记头
//        /// </summary>
//        /// <param name="bs"></param>
//        /// <returns></returns>
//        public static bool Has_UTF8_BOMHeader(byte[] bs)
//        {
//            if (bs != null && bs.Length > 4)
//            {
//                if (bs[0] == 239 && bs[1] == 187 && bs[2] == 191)
//                {
//                    return true;
//                }
//            }
//            return false;
//        }

//        /// <summary>
//        /// 判断数据块是否具有CAB文件头
//        /// </summary>
//        /// <param name="bs">数据块</param>
//        /// <returns>是否具有CAB文件头</returns>
//        public static bool HasCABHeader(byte[] bs)
//        {
//            if (bs != null && bs.Length >= 4)
//            {
//                if (bs[0] == 0x4d && bs[1] == 0x53 && bs[2] == 0x43 && bs[3] == 0x46)
//                {
//                    // 字符串 MSCF
//                    return true;
//                }
//            }
//            return false;
//        }

//        /// <summary>
//        /// 判断数据块是否具有ZIP文件头
//        /// </summary>
//        /// <param name="bs">数据块</param>
//        /// <returns>是否具有ZIP文件头</returns>
//        public static bool HasZIPHeader(byte[] bs)
//        {
//            if (bs != null && bs.Length >= 4)
//            {
//                if (bs[0] == 0x50 && bs[1] == 0x4b && bs[2] == 0x03 && bs[3] == 0x04)
//                {
//                    return true;
//                }
//            }
//            return false;
//        }

//        /// <summary>
//        /// 判断数据块是否具有webp图片文件标记头 wyc20210720添加（虽然实际没啥用）
//        /// </summary>
//        /// <param name="bs">数据块</param>
//        /// <returns>是否具有PNG图片文件标记头</returns>
//        public static bool HasWEBPHeader(byte[] bs)
//        {
//            if (bs != null && bs.Length >= webpHeader.Length)
//            {
//                for (int iCount = 0; iCount < webpHeader.Length; iCount++)
//                {
//                    if (iCount >= 4 && iCount <= 7)
//                    {
//                        continue;
//                    }
//                    if (bs[iCount] != webpHeader[iCount])
//                    {
//                        return false;
//                    }
//                }
//                return true;
//            }
//            return false;
//        }
//#endif
       

//        public static bool HasRIFFHeader( byte[] bs )
//        {
//            if( bs != null && bs.Length > 5 )
//            {
//                if( bs[0] == 52 && bs[1] ==49 && bs[2] == 46 && bs[3]== 46)
//                {
//                    return true;
//                }
//            }
//            return false;
//        }

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

        private readonly static byte[] webpHeader = new byte[] { 0x52, 0x49, 0x46, 0x46, 0x0, 0x0, 0x0, 0x0, 0x57, 0x45, 0x42, 0x50 };

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
                   
                }
            }
            return false;
        }

        
    }
}