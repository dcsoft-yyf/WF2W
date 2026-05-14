using System;
using System.Drawing.Imaging;
using System.Collections;
using System.Collections.Generic;
using System.Drawing ;
using System.IO;

namespace DCSoft.Common
{
	/// <summary>
	/// 图片对象帮助模块
	/// </summary>
    /// <remarks>编制 袁永福</remarks>
    [System.Runtime.InteropServices.ComVisible(false)]
    public static class ImageHelper
	{
		static ImageHelper()
		{

        }
             

        private readonly static Dictionary<byte[], Image> _BinaryImages = new Dictionary<byte[], Image>();
        /// <summary>
        /// 使用缓存的加载图片
        /// </summary>
        /// <param name="base64String">包含图片的BASE64字符串</param>
        /// <returns>加载的图片</returns>
        public static Image LoadImageBase64UseBuffer(string base64String)
        {
            if (base64String == null || base64String.Length == 0 )// string.IsNullOrEmpty(base64String))
            {
                return null;
            }
            byte[] bs = Convert.FromBase64String(base64String);
            foreach (byte[] bs2 in _BinaryImages.Keys)
            {
                if (bs.Length == bs2.Length)
                {
                    bool match = true;
                    for (int iCount = 0; iCount < bs.Length; iCount++)
                    {
                        if (bs[iCount] != bs2[iCount])
                        {
                            match = false;
                            break;
                        }
                    }
                    if (match)
                    {
                        return _BinaryImages[bs2];
                    }
                }
            }//foreach
            MemoryStream ms = new MemoryStream(bs);
            Image img = Image.FromStream(ms);
            _BinaryImages[bs] = img;
            return img;
        }

    }//public sealed class ImageHelper
}