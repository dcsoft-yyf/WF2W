using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Printing;

namespace DCSoft.Drawing
{
    /// <summary>
    /// 提供标准的页面大小的信息对象
    /// </summary>
    [System.Runtime.InteropServices.ComVisible( false )]
    [System.Reflection.Obfuscation ( Exclude= false , ApplyToMembers= true )]
    public static class StandardPaperSizeInfo
    {
#if !DCWriterForWASM
        public static Rectangle SafeGetBounds(PageSettings ps)
        {
            if (ps == null)
            {
                throw new ArgumentNullException("ps");
            }
            try
            {
                return ps.Bounds;
            }
            catch (Exception ext)
            {
                //DCConsole.Default.WriteLineError(ext.ToString());
                System.Drawing.Size s = System.Drawing.Size.Empty;
                try
                {
                    if (ps.PaperSize.Kind == PaperKind.Custom)
                    {
                        s.Width = ps.PaperSize.Width;
                        s.Height = ps.PaperSize.Height;
                    }
                    else
                    {
                        s = GetStandardPaperSize(ps.PaperSize.Kind);
                    }
                }
                catch (Exception ext2)
                {
                    //DCConsole.Default.WriteLineError(ext2.ToString());
                    s = GetStandardPaperSize(PaperKind.A4);
                }
                int w = s.Width;
                int h = s.Height;
                if (ps.Landscape)
                {
                    int t = w;
                    w = h;
                    h = t;
                }
                return new Rectangle(0, 0, w, h);
            }
        }
#endif
        

        public static Size GetStandardPaperSize(PaperKind kind)
        {
            int index = (int)kind;
            if (index >= 0 && index < myStandardPaperSize.Length)
            {
                return myStandardPaperSize[index];
            }
            else
            {
                return Size.Empty;
            }
        }

        private readonly static Size[] myStandardPaperSize = null;
        static StandardPaperSizeInfo()
        {
            myStandardPaperSize = new Size[120];
            // 定义标准页面大小
            myStandardPaperSize[(int)System.Drawing.Printing.PaperKind.A2] = new Size(1654, 2339); 	//A2 纸（420 毫米 × 594 毫米）。
            myStandardPaperSize[(int)System.Drawing.Printing.PaperKind.A3] = new Size(1169, 1654); 	//A3 纸（297 毫米 × 420 毫米）。
            myStandardPaperSize[(int)System.Drawing.Printing.PaperKind.A3Extra] = new Size(1268, 1752); 	//A3 extra 纸（322 毫米 × 445 毫米）。
            myStandardPaperSize[(int)System.Drawing.Printing.PaperKind.A3ExtraTransverse] = new Size(1268, 1752); 	//A3 extra transverse 纸（322 毫米 × 445 毫米）。
            myStandardPaperSize[(int)System.Drawing.Printing.PaperKind.A3Rotated] = new Size(1654, 1169); 	//A3 rotated 纸（420 毫米 × 297 毫米）。
            myStandardPaperSize[(int)System.Drawing.Printing.PaperKind.A3Transverse] = new Size(1169, 1654); 	//A3 transverse 纸（297 毫米 × 420 毫米）。
            myStandardPaperSize[(int)System.Drawing.Printing.PaperKind.A4] = new Size(827, 1169); 	//A4 纸（210 毫米 × 297 毫米）。
            myStandardPaperSize[(int)System.Drawing.Printing.PaperKind.A4Extra] = new Size(929, 1268); 	//A4 extra 纸（236 毫米 × 322 毫米）。该值是针对 PostScript 驱动程序的，仅供 Linotronic 打印机使用以节省纸张。
            myStandardPaperSize[(int)System.Drawing.Printing.PaperKind.A4Plus] = new Size(827, 1299); 	//A4 plus 纸（210 毫米 × 330 毫米）。
            myStandardPaperSize[(int)System.Drawing.Printing.PaperKind.A4Rotated] = new Size(1169, 827); 	//A4 rotated 纸（297 毫米 × 210 毫米）。需要 Windows 98、Windows NT 4.0 或更高版本。
            myStandardPaperSize[(int)System.Drawing.Printing.PaperKind.A4Small] = new Size(827, 1169); 	//A4 small 纸（210 毫米 × 297 毫米）。
            myStandardPaperSize[(int)System.Drawing.Printing.PaperKind.A4Transverse] = new Size(827, 1169); 	//A4 transverse 纸（210 毫米 × 297 毫米）。
            myStandardPaperSize[(int)System.Drawing.Printing.PaperKind.A5] = new Size(583, 827); 	//A5 纸（148 毫米 × 210 毫米）。
            myStandardPaperSize[(int)System.Drawing.Printing.PaperKind.A5Extra] = new Size(685, 925); 	//A5 extra 纸（174 毫米 × 235 毫米）。
            myStandardPaperSize[(int)System.Drawing.Printing.PaperKind.A5Rotated] = new Size(827, 583); 	//A5 rotated 纸（210 毫米 × 148 毫米）。需要 Windows 98、Windows NT 4.0 或更高版本。
            myStandardPaperSize[(int)System.Drawing.Printing.PaperKind.A5Transverse] = new Size(583, 827); 	//A5 transverse 纸（148 毫米 × 210 毫米）。
            myStandardPaperSize[(int)System.Drawing.Printing.PaperKind.A6] = new Size(413, 583); 	//A6 纸（105 毫米 × 148 毫米）。需要 Windows 98、Windows NT 4.0 或更高版本。
            myStandardPaperSize[(int)System.Drawing.Printing.PaperKind.A6Rotated] = new Size(583, 413); 	//A6 rotated 纸（148 毫米 × 105 毫米）。需要 Windows 98、Windows NT 4.0 或更高版本。
            myStandardPaperSize[(int)System.Drawing.Printing.PaperKind.APlus] = new Size(894, 1402); 	//SuperA/SuperA/A4 纸（227 毫米 × 356 毫米）。
            myStandardPaperSize[(int)System.Drawing.Printing.PaperKind.B4] = new Size(984, 1390); 	//B4 纸（250 × 353 毫米）。
            myStandardPaperSize[(int)System.Drawing.Printing.PaperKind.B4Envelope] = new Size(984, 1390); 	//B4 信封（250 × 353 毫米）。
            myStandardPaperSize[(int)System.Drawing.Printing.PaperKind.B5] = new Size(693, 984); 	//B5 纸（176 毫米 × 250 毫米）。
            myStandardPaperSize[(int)System.Drawing.Printing.PaperKind.B5Envelope] = new Size(693, 984); 	//B5 信封（176 毫米 × 250 毫米）。
            myStandardPaperSize[(int)System.Drawing.Printing.PaperKind.B5Extra] = new Size(791, 1087); 	//ISO B5 extra 纸（201 毫米 × 276 毫米）。
            myStandardPaperSize[(int)System.Drawing.Printing.PaperKind.B5JisRotated] = new Size(1012, 717); 	//JIS B5 rotated 纸（257 毫米 × 182 毫米）。需要 Windows 98、Windows NT 4.0 或更高版本。
            myStandardPaperSize[(int)System.Drawing.Printing.PaperKind.B5Transverse] = new Size(717, 1012); 	//JIS B5 transverse 纸（182 毫米 × 257 毫米）。
            myStandardPaperSize[(int)System.Drawing.Printing.PaperKind.B6Envelope] = new Size(693, 492); 	//B6 信封（176 毫米 × 125 毫米）。
            myStandardPaperSize[(int)System.Drawing.Printing.PaperKind.B6Jis] = new Size(504, 717); 	//JIS B6 纸（128 毫米 × 182 毫米）。需要 Windows 98、Windows NT 4.0 或更高版本。
            myStandardPaperSize[(int)System.Drawing.Printing.PaperKind.BPlus] = new Size(1201, 1917); 	//SuperB/SuperB/A3 纸（305 毫米 × 487 毫米）。
            myStandardPaperSize[(int)System.Drawing.Printing.PaperKind.C3Envelope] = new Size(1201, 1917); 	//SuperB/SuperB/A3 纸（305 毫米 × 487 毫米）。
            myStandardPaperSize[(int)System.Drawing.Printing.PaperKind.C4Envelope] = new Size(902, 1276); 	//C4 信封（229 毫米 × 324 毫米）。
            myStandardPaperSize[(int)System.Drawing.Printing.PaperKind.C5Envelope] = new Size(638, 902); 	//C5 信封（162 毫米 × 229 毫米）。
            myStandardPaperSize[(int)System.Drawing.Printing.PaperKind.C65Envelope] = new Size(449, 902); 	//C65 信封（114 毫米 × 229 毫米）。
            myStandardPaperSize[(int)System.Drawing.Printing.PaperKind.C6Envelope] = new Size(449, 638); 	//C6 信封（114 毫米 × 162 毫米）。
            myStandardPaperSize[(int)System.Drawing.Printing.PaperKind.CSheet] = new Size(449, 638); 	//C6 信封（114 毫米 × 162 毫米）。 
            //////////myStandardPaperSize[(int)System.Drawing.Printing.PaperKind.Custom] = new Size(0, 0); // 自定义大小
            myStandardPaperSize[(int)System.Drawing.Printing.PaperKind.DLEnvelope] = new Size(433, 866); 	//DL 信封（110 毫米 × 220 毫米）。
            myStandardPaperSize[(int)System.Drawing.Printing.PaperKind.DSheet] = new Size(2201, 3402); 	//D 纸（559 毫米 × 864 毫米）。
            myStandardPaperSize[(int)System.Drawing.Printing.PaperKind.ESheet] = new Size(3402, 4402); 	//E 纸（864 毫米 × 1118 毫米）。
            myStandardPaperSize[(int)System.Drawing.Printing.PaperKind.Executive] = new Size(724, 1051); 	//Executive 纸（184 毫米 × 267 毫米）。
            myStandardPaperSize[(int)System.Drawing.Printing.PaperKind.Folio] = new Size(850, 1299); 	//Folio 纸（216 毫米 × 330 毫米）。
            myStandardPaperSize[(int)System.Drawing.Printing.PaperKind.GermanLegalFanfold] = new Size(850, 1299); 	//German legal fanfold（216 毫米 × 330 毫米）。
            myStandardPaperSize[(int)System.Drawing.Printing.PaperKind.GermanStandardFanfold] = new Size(850, 1201); 	//German standard fanfold（216 毫米 × 305 毫米）。
            myStandardPaperSize[(int)System.Drawing.Printing.PaperKind.InviteEnvelope] = new Size(866, 866); 	//Invite envelope（220 毫米 × 220 毫米）。
            myStandardPaperSize[(int)System.Drawing.Printing.PaperKind.IsoB4] = new Size(984, 1390); 	//ISO B4（250 毫米 × 353 毫米）。
            myStandardPaperSize[(int)System.Drawing.Printing.PaperKind.ItalyEnvelope] = new Size(433, 906); 	//Italy envelope（110 毫米 × 230 毫米）。
            myStandardPaperSize[(int)System.Drawing.Printing.PaperKind.JapaneseDoublePostcard] = new Size(787, 583); 	//Japanese double postcard（200 毫米 × 148 毫米）。需要 Windows 98、Windows NT 4.0 或更高版本。
            myStandardPaperSize[(int)System.Drawing.Printing.PaperKind.JapaneseDoublePostcardRotated] = new Size(583, 787); 	//Japanese rotated double postcard（148 毫米 × 200 毫米）。需要 Windows 98、Windows NT 4.0 或更高版本。
            myStandardPaperSize[(int)System.Drawing.Printing.PaperKind.JapanesePostcard] = new Size(394, 583); 	//Japanese postcard（100 毫米 × 148 毫米）。
            myStandardPaperSize[(int)System.Drawing.Printing.PaperKind.JapanesePostcardRotated] = new Size(583, 394); 	//Japanese rotated postcard（148 毫米 × 100 毫米）。需要 Windows 98、Windows NT 4.0 或更高版本。
            myStandardPaperSize[(int)System.Drawing.Printing.PaperKind.Ledger] = new Size(1701, 1098); 	//Ledger 纸（432 × 279 毫米）。
            myStandardPaperSize[(int)System.Drawing.Printing.PaperKind.Legal] = new Size(850, 1402); 	//Legal 纸（216 × 356 毫米）。
            myStandardPaperSize[(int)System.Drawing.Printing.PaperKind.LegalExtra] = new Size(929, 1500); 	//Legal extra 纸（236 毫米 × 381 毫米）。该值特定于 PostScript 驱动程序，仅供 Linotronic 打印机使用以节省纸张。
            myStandardPaperSize[(int)System.Drawing.Printing.PaperKind.Letter] = new Size(850, 1098); 	//Letter 纸（216 毫米 × 279 毫米）。
            myStandardPaperSize[(int)System.Drawing.Printing.PaperKind.LetterExtra] = new Size(929, 1197); 	//Letter extra 纸（236 毫米 × 304 毫米）。该值特定于 PostScript 驱动程序，仅供 Linotronic 打印机使用以节省纸张。
            myStandardPaperSize[(int)System.Drawing.Printing.PaperKind.LetterExtraTransverse] = new Size(929, 1201); 	//Letter extra transverse 纸（236 毫米 × 305 毫米）。
            myStandardPaperSize[(int)System.Drawing.Printing.PaperKind.LetterPlus] = new Size(850, 1268); 	//Letter plus 纸（216 毫米 毫米 × 322 毫米）。
            myStandardPaperSize[(int)System.Drawing.Printing.PaperKind.LetterRotated] = new Size(1098, 850); 	//Letter rotated 纸（279 毫米 × 216 毫米）。
            myStandardPaperSize[(int)System.Drawing.Printing.PaperKind.LetterSmall] = new Size(850, 1098); 	//Letter small 纸（216 × 279 毫米）。
            myStandardPaperSize[(int)System.Drawing.Printing.PaperKind.LetterTransverse] = new Size(827, 1098); 	//Letter transverse 纸（210 毫米 × 279 毫米）。
            myStandardPaperSize[(int)System.Drawing.Printing.PaperKind.MonarchEnvelope] = new Size(386, 752); 	//Monarch envelope（98 毫米 × 191 毫米）。
            myStandardPaperSize[(int)System.Drawing.Printing.PaperKind.Note] = new Size(850, 1098); 	//Note 纸（216 × 279 毫米）。
            myStandardPaperSize[(int)System.Drawing.Printing.PaperKind.Number10Envelope] = new Size(413, 949); 	//#10 envelope（105 × 241 毫米）。
            myStandardPaperSize[(int)System.Drawing.Printing.PaperKind.PersonalEnvelope] = new Size(362, 650); 	//6 3/4 envelope（92 毫米 × 165 毫米）。
            myStandardPaperSize[(int)System.Drawing.Printing.PaperKind.Prc16K] = new Size(575, 846); 	//PRC 16K 纸（146 × 215 毫米）。需要 Windows 98、Windows NT 4.0 或更高版本。
            myStandardPaperSize[(int)System.Drawing.Printing.PaperKind.Prc16KRotated] = new Size(575, 846); 	//PRC 16K rotated 纸（146 × 215 毫米）。需要 Windows 98、Windows NT 4.0 或更高版本。
            myStandardPaperSize[(int)System.Drawing.Printing.PaperKind.Prc32K] = new Size(382, 594); 	//PRC 32K 纸（97 × 151 毫米）。需要 Windows 98、Windows NT 4.0 或更高版本。
            myStandardPaperSize[(int)System.Drawing.Printing.PaperKind.Prc32KBig] = new Size(382, 594); 	//PRC 32K(Big) 纸（97 × 151 毫米）。需要 Windows 98、Windows NT 4.0 或更高版本。
            myStandardPaperSize[(int)System.Drawing.Printing.PaperKind.Prc32KBigRotated] = new Size(382, 594); 	//PRC 32K rotated 纸（97 × 151 毫米）。需要 Windows 98、Windows NT 4.0 或更高版本。
            myStandardPaperSize[(int)System.Drawing.Printing.PaperKind.Prc32KRotated] = new Size(382, 594); 	//PRC 32K rotated 纸（97 × 151 毫米）。需要 Windows 98、Windows NT 4.0 或更高版本。
            myStandardPaperSize[(int)System.Drawing.Printing.PaperKind.PrcEnvelopeNumber1] = new Size(402, 650); 	//PRC #1 envelope（102 × 165 毫米）。需要 Windows 98、Windows NT 4.0 或更高版本。
            myStandardPaperSize[(int)System.Drawing.Printing.PaperKind.PrcEnvelopeNumber10] = new Size(1276, 1803); 	//PRC #10 envelope（324 × 458 毫米）。需要 Windows 98、Windows NT 4.0 或更高版本。
            myStandardPaperSize[(int)System.Drawing.Printing.PaperKind.PrcEnvelopeNumber10Rotated] = new Size(1803, 1276); 	//PRC #10 rotated envelope（458 × 324 毫米）。需要 Windows 98、Windows NT 4.0 或更高版本。
            myStandardPaperSize[(int)System.Drawing.Printing.PaperKind.PrcEnvelopeNumber1Rotated] = new Size(650, 402); 	//PRC #1 rotated envelope（165 × 102 毫米）。需要 Windows 98、Windows NT 4.0 或更高版本。
            myStandardPaperSize[(int)System.Drawing.Printing.PaperKind.PrcEnvelopeNumber2] = new Size(402, 693); 	//PRC #2 envelope（102 × 176 毫米）。需要 Windows 98、Windows NT 4.0 或更高版本。
            myStandardPaperSize[(int)System.Drawing.Printing.PaperKind.PrcEnvelopeNumber2Rotated] = new Size(693, 402); 	//PRC #2 rotated envelope（176 × 102 毫米）。需要 Windows 98、Windows NT 4.0 或更高版本。
            myStandardPaperSize[(int)System.Drawing.Printing.PaperKind.PrcEnvelopeNumber3] = new Size(492, 693); 	//PRC #3 envelope（125 × 176 毫米）。需要 Windows 98、Windows NT 4.0 或更高版本。
            myStandardPaperSize[(int)System.Drawing.Printing.PaperKind.PrcEnvelopeNumber3Rotated] = new Size(693, 492); 	//PRC #3 rotated envelope（176 × 125 毫米）。需要 Windows 98、Windows NT 4.0 或更高版本。
            myStandardPaperSize[(int)System.Drawing.Printing.PaperKind.PrcEnvelopeNumber4] = new Size(433, 819); 	//PRC #4 envelope（110 × 208 毫米）。需要 Windows 98、Windows NT 4.0 或更高版本。
            myStandardPaperSize[(int)System.Drawing.Printing.PaperKind.PrcEnvelopeNumber4Rotated] = new Size(819, 433); 	//PRC #4 rotated envelope（208 × 110 毫米）。需要 Windows 98、Windows NT 4.0 或更高版本。
            myStandardPaperSize[(int)System.Drawing.Printing.PaperKind.PrcEnvelopeNumber5] = new Size(433, 866); 	//PRC #5 envelope（110 × 220 毫米）。需要 Windows 98、Windows NT 4.0 或更高版本。
            myStandardPaperSize[(int)System.Drawing.Printing.PaperKind.PrcEnvelopeNumber5Rotated] = new Size(866, 433); 	//PRC #5 rotated envelope（220 × 110 毫米）。需要 Windows 98、Windows NT 4.0 或更高版本。
            myStandardPaperSize[(int)System.Drawing.Printing.PaperKind.PrcEnvelopeNumber6] = new Size(472, 906); 	//PRC #6 envelope（120 × 230 毫米）。需要 Windows 98、Windows NT 4.0 或更高版本。
            myStandardPaperSize[(int)System.Drawing.Printing.PaperKind.PrcEnvelopeNumber6Rotated] = new Size(906, 472); 	//PRC #6 rotated envelope（230 × 120 毫米）。需要 Windows 98、Windows NT 4.0 或更高版本。
            myStandardPaperSize[(int)System.Drawing.Printing.PaperKind.PrcEnvelopeNumber7] = new Size(630, 906); 	//PRC #7 envelope（160 × 230 毫米）。需要 Windows 98、Windows NT 4.0 或更高版本。
            myStandardPaperSize[(int)System.Drawing.Printing.PaperKind.PrcEnvelopeNumber7Rotated] = new Size(906, 630); 	//PRC #7 rotated envelope（230 × 160 毫米）。需要 Windows 98、Windows NT 4.0 或更高版本。
            myStandardPaperSize[(int)System.Drawing.Printing.PaperKind.PrcEnvelopeNumber8] = new Size(472, 1217); 	//PRC #8 envelope（120 × 309 毫米）。需要 Windows 98、Windows NT 4.0 或更高版本。
            myStandardPaperSize[(int)System.Drawing.Printing.PaperKind.PrcEnvelopeNumber8Rotated] = new Size(1217, 472); 	//PRC #8 rotated envelope（309 × 120 毫米）。需要 Windows 98、Windows NT 4.0 或更高版本。
            myStandardPaperSize[(int)System.Drawing.Printing.PaperKind.PrcEnvelopeNumber9] = new Size(902, 1276); 	//PRC #9 envelope（229 × 324 毫米）。需要 Windows 98、Windows NT 4.0 或更高版本。
            myStandardPaperSize[(int)System.Drawing.Printing.PaperKind.PrcEnvelopeNumber9Rotated] = new Size(902, 1276); 	//PRC #9 rotated envelope（229 × 324 毫米）。需要 Windows 98、Windows NT 4.0 或更高版本。
            myStandardPaperSize[(int)System.Drawing.Printing.PaperKind.Quarto] = new Size(846, 1083); 	//Quarto 纸（215 毫米 × 275 毫米）。
            myStandardPaperSize[(int)System.Drawing.Printing.PaperKind.Standard10x11] = new Size(1000, 1098); 	//Standard 纸（254 毫米 × 279 毫米）。
            myStandardPaperSize[(int)System.Drawing.Printing.PaperKind.Standard10x14] = new Size(1000, 1402); 	//Standard 纸（254 毫米 × 356 毫米）。
            myStandardPaperSize[(int)System.Drawing.Printing.PaperKind.Standard11x17] = new Size(1098, 1701); 	//Standard 纸（279 毫米 × 432 毫米）。
            myStandardPaperSize[(int)System.Drawing.Printing.PaperKind.Standard12x11] = new Size(1201, 1098); 	//Standard 纸（305 × 279 毫米）。需要 Windows 98、Windows NT 4.0 或更高版本。
            myStandardPaperSize[(int)System.Drawing.Printing.PaperKind.Standard15x11] = new Size(1500, 1098); 	//Standard 纸（381 毫米 × 279 毫米）。
            myStandardPaperSize[(int)System.Drawing.Printing.PaperKind.Standard9x11] = new Size(902, 1098); 	//Standard 纸（229 × 279 毫米）。
            myStandardPaperSize[(int)System.Drawing.Printing.PaperKind.Statement] = new Size(551, 850); 	//Statement 纸（140 毫米 × 216 毫米）。
            myStandardPaperSize[(int)System.Drawing.Printing.PaperKind.Tabloid] = new Size(1098, 1701); 	//Tabloid 纸（279 毫米 × 432 毫米）。
            myStandardPaperSize[(int)System.Drawing.Printing.PaperKind.TabloidExtra] = new Size(1169, 1799); 	//Tabloid extra 纸（297 毫米 × 457 毫米）。该值特定于 PostScript 驱动程序，仅供 Linotronic 打印机使用以节省纸张。
            myStandardPaperSize[(int)System.Drawing.Printing.PaperKind.USStandardFanfold] = new Size(1488, 1098); 	//US standard fanfold（378 毫米 × 279 毫米）。

        }
    }
}
