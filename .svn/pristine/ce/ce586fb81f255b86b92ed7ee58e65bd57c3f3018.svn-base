using System.Collections.Generic;
using System.Text.Json;
using System.Drawing;
using DCSoft.WF2W.Core.WinFormCore;
namespace DCSoft
{
    /// <summary>
    /// TrueType字体文件相关代码
    /// </summary>
    partial class TrueTypeFontSnapshort
    {
        /// <summary>
        /// 字体信息快照缓存区
        /// </summary>
        private static readonly Dictionary<string, DCSoft.TrueTypeFontSnapshort> _FontSnapshorts
            = new Dictionary<string, TrueTypeFontSnapshort>();
        /// <summary>
        /// 错误的字体名称列表
        /// </summary>
        private static List<string> _BadFontNames = null;

        private static TrueTypeFontSnapshort _LastInstance = null;
        public static TrueTypeFontSnapshort GetInstance(string strFontName, FontStyle style, GraphicsUnit unit = GraphicsUnit.Point)
        {
            if (_LastInstance != null && _LastInstance.FontName == strFontName && _LastInstance.Style == style)
            {
                return _LastInstance;
            }
            if (strFontName == null || strFontName.Length == 0)
            {
                strFontName = SystemFonts._DefaultFontName;
            }
            var bolBold = (style & FontStyle.Bold) == FontStyle.Bold;
            var bolItalic = (style & FontStyle.Italic) == FontStyle.Italic;

            if (_BadFontNames != null && _BadFontNames.Contains(strFontName))
            {
                return null;
            }
            var finfo2 = DCFontList.GetInfo(strFontName, bolBold, bolItalic);
            if(finfo2 == null )
            {
                if (_BadFontNames == null)
                {
                    _BadFontNames = new List<string>();
                }
                _BadFontNames.Add(strFontName); 
                return null;
            }
            if (finfo2.TTF_Snapshort == null)
            {
                var info = TrueTypeFontSnapshort.Create(strFontName, finfo2.Snapshort);
                info.BoldZoomRate = (float)finfo2.BoldZoomRate;
                if (info != null && info.FontName == "华文宋体")
                {
                    //针对问题DUWRITER5_0-4067，宋体LineSpacing=1.140625,华文宋体LineSpacing=1.301
                    info.LineSpacing = 1.140625f;
                }
                finfo2.TTF_Snapshort = info;
                finfo2.Snapshort = null;
            }
            return finfo2.TTF_Snapshort;
            //var strKey = DCWin32API.JSRuntime.Invoke<string>("WriterControl_FontList.GetFontInfoKeyName", new object[] { strFontName, bolBold, bolItalic });
            //if (_FontSnapshorts.TryGetValue(strKey, out info))
            //{
            //    return info;
            //}
            //byte[] bsData = null;
            //var strData = DCWin32API.JSRuntime.Invoke<string>("WriterControl_FontList.GetFontSnapshort",new object[] { strKey });
            //if (strData != null && strData.Length > 0)
            //{
            //    float vBoldZoomRate = 1;
            //    var index99 = strData.LastIndexOf(',');
            //    if (index99 > 0)
            //    {
            //        vBoldZoomRate = float.Parse(strData.Substring(index99 + 1));
            //        strData = strData.Substring(0, index99);
            //    }
            //    bsData = Convert.FromBase64String(strData);
            //    info = TrueTypeFontSnapshort.Create(strFontName, bsData);
            //    info.BoldZoomRate = vBoldZoomRate;
            //}
            //else
            //{
            //    if (_BadFontNames == null)
            //    {
            //        _BadFontNames = new List<string>();
            //    }
            //    _BadFontNames.Add(strFontName);
            //}
            //_FontSnapshorts[strKey] = info;
            ////Console.WriteLine(info.FontName + " lll :" + info.LineSpacing);
            //if (info != null && info.FontName == "华文宋体")
            //{
            //    //针对问题DUWRITER5_0-4067，宋体LineSpacing=1.140625,华文宋体LineSpacing=1.301
            //    info.LineSpacing = 1.140625f;
            //}
            //_LastInstance = info;
            //return info;
        }
    }
}
