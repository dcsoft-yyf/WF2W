//------------------------------------------------------------------------------
// <copyright file="PaperSize.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>                                                                
//------------------------------------------------------------------------------

namespace System.Drawing.Printing {
    using System.Runtime.Serialization.Formatters;
    using System.Runtime.InteropServices;
    using System.Diagnostics;
    using System;    
    using System.Drawing;
    using System.ComponentModel;
    using Microsoft.Win32;
    using System.Globalization;

    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
    public class PaperSize {
        internal static readonly PaperSize[] StandartInstances;
        internal static readonly PaperSize A4Instance;
        static PaperSize()
        {
            var list = new List<PaperSize>();
            void AddSize(PaperKind kind, int w, int h)
            {
                var ps = new PaperSize(kind, kind.ToString(), w, h);
                list.Add(ps);
            }
            // 定义标准页面大小
            AddSize(PaperKind.A2, 1654, 2339); 	//A2 纸（420 毫米 × 594 毫米）。
            AddSize(PaperKind.A3, 1169, 1654); 	//A3 纸（297 毫米 × 420 毫米）。
            AddSize(PaperKind.A3Extra, 1268, 1752); 	//A3 extra 纸（322 毫米 × 445 毫米）。
            AddSize(PaperKind.A3ExtraTransverse, 1268, 1752); 	//A3 extra transverse 纸（322 毫米 × 445 毫米）。
            AddSize(PaperKind.A3Rotated, 1654, 1169); 	//A3 rotated 纸（420 毫米 × 297 毫米）。
            AddSize(PaperKind.A3Transverse, 1169, 1654); 	//A3 transverse 纸（297 毫米 × 420 毫米）。
            AddSize(PaperKind.A4, 827, 1169); 	//A4 纸（210 毫米 × 297 毫米）。
            A4Instance = list[list.Count - 1];
            AddSize(PaperKind.A4Extra, 929, 1268); 	//A4 extra 纸（236 毫米 × 322 毫米）。该值是针对 PostScript 驱动程序的，仅供 Linotronic 打印机使用以节省纸张。
            AddSize(PaperKind.A4Plus, 827, 1299); 	//A4 plus 纸（210 毫米 × 330 毫米）。
            AddSize(PaperKind.A4Rotated, 1169, 827); 	//A4 rotated 纸（297 毫米 × 210 毫米）。需要 Windows 98、Windows NT 4.0 或更高版本。
            AddSize(PaperKind.A4Small, 827, 1169); 	//A4 small 纸（210 毫米 × 297 毫米）。
            AddSize(PaperKind.A4Transverse, 827, 1169); 	//A4 transverse 纸（210 毫米 × 297 毫米）。
            AddSize(PaperKind.A5, 583, 827); 	//A5 纸（148 毫米 × 210 毫米）。
            AddSize(PaperKind.A5Extra, 685, 925); 	//A5 extra 纸（174 毫米 × 235 毫米）。
            AddSize(PaperKind.A5Rotated, 827, 583); 	//A5 rotated 纸（210 毫米 × 148 毫米）。需要 Windows 98、Windows NT 4.0 或更高版本。
            AddSize(PaperKind.A5Transverse, 583, 827); 	//A5 transverse 纸（148 毫米 × 210 毫米）。
            AddSize(PaperKind.A6, 413, 583); 	//A6 纸（105 毫米 × 148 毫米）。需要 Windows 98、Windows NT 4.0 或更高版本。
            AddSize(PaperKind.A6Rotated, 583, 413); 	//A6 rotated 纸（148 毫米 × 105 毫米）。需要 Windows 98、Windows NT 4.0 或更高版本。
            AddSize(PaperKind.APlus, 894, 1402); 	//SuperA/SuperA/A4 纸（227 毫米 × 356 毫米）。
            AddSize(PaperKind.B4, 984, 1390); 	//B4 纸（250 × 353 毫米）。
            AddSize(PaperKind.B4Envelope, 984, 1390); 	//B4 信封（250 × 353 毫米）。
            AddSize(PaperKind.B5, 693, 984); 	//B5 纸（176 毫米 × 250 毫米）。
            AddSize(PaperKind.B5Envelope, 693, 984); 	//B5 信封（176 毫米 × 250 毫米）。
            AddSize(PaperKind.B5Extra, 791, 1087); 	//ISO B5 extra 纸（201 毫米 × 276 毫米）。
            AddSize(PaperKind.B5JisRotated, 1012, 717); 	//JIS B5 rotated 纸（257 毫米 × 182 毫米）。需要 Windows 98、Windows NT 4.0 或更高版本。
            AddSize(PaperKind.B5Transverse, 717, 1012); 	//JIS B5 transverse 纸（182 毫米 × 257 毫米）。
            AddSize(PaperKind.B6Envelope, 693, 492); 	//B6 信封（176 毫米 × 125 毫米）。
            AddSize(PaperKind.B6Jis, 504, 717); 	//JIS B6 纸（128 毫米 × 182 毫米）。需要 Windows 98、Windows NT 4.0 或更高版本。
            AddSize(PaperKind.BPlus, 1201, 1917); 	//SuperB/SuperB/A3 纸（305 毫米 × 487 毫米）。
            AddSize(PaperKind.C3Envelope, 1201, 1917); 	//SuperB/SuperB/A3 纸（305 毫米 × 487 毫米）。
            AddSize(PaperKind.C4Envelope, 902, 1276); 	//C4 信封（229 毫米 × 324 毫米）。
            AddSize(PaperKind.C5Envelope, 638, 902); 	//C5 信封（162 毫米 × 229 毫米）。
            AddSize(PaperKind.C65Envelope, 449, 902); 	//C65 信封（114 毫米 × 229 毫米）。
            AddSize(PaperKind.C6Envelope, 449, 638); 	//C6 信封（114 毫米 × 162 毫米）。
            AddSize(PaperKind.CSheet, 449, 638); 	//C6 信封（114 毫米 × 162 毫米）。 
            AddSize(PaperKind.Custom, 0, 0); // 自定义大小
            AddSize(PaperKind.DLEnvelope, 433, 866); 	//DL 信封（110 毫米 × 220 毫米）。
            AddSize(PaperKind.DSheet, 2201, 3402); 	//D 纸（559 毫米 × 864 毫米）。
            AddSize(PaperKind.ESheet, 3402, 4402); 	//E 纸（864 毫米 × 1118 毫米）。
            AddSize(PaperKind.Executive, 724, 1051); 	//Executive 纸（184 毫米 × 267 毫米）。
            AddSize(PaperKind.Folio, 850, 1299); 	//Folio 纸（216 毫米 × 330 毫米）。
            AddSize(PaperKind.GermanLegalFanfold, 850, 1299); 	//German legal fanfold（216 毫米 × 330 毫米）。
            AddSize(PaperKind.GermanStandardFanfold, 850, 1201); 	//German standard fanfold（216 毫米 × 305 毫米）。
            AddSize(PaperKind.InviteEnvelope, 866, 866); 	//Invite envelope（220 毫米 × 220 毫米）。
            AddSize(PaperKind.IsoB4, 984, 1390); 	//ISO B4（250 毫米 × 353 毫米）。
            AddSize(PaperKind.ItalyEnvelope, 433, 906); 	//Italy envelope（110 毫米 × 230 毫米）。
            AddSize(PaperKind.JapaneseDoublePostcard, 787, 583); 	//Japanese double postcard（200 毫米 × 148 毫米）。需要 Windows 98、Windows NT 4.0 或更高版本。
            AddSize(PaperKind.JapaneseDoublePostcardRotated, 583, 787); 	//Japanese rotated double postcard（148 毫米 × 200 毫米）。需要 Windows 98、Windows NT 4.0 或更高版本。
            AddSize(PaperKind.JapanesePostcard, 394, 583); 	//Japanese postcard（100 毫米 × 148 毫米）。
            AddSize(PaperKind.JapanesePostcardRotated, 583, 394); 	//Japanese rotated postcard（148 毫米 × 100 毫米）。需要 Windows 98、Windows NT 4.0 或更高版本。
            AddSize(PaperKind.Ledger, 1701, 1098); 	//Ledger 纸（432 × 279 毫米）。
            AddSize(PaperKind.Legal, 850, 1402); 	//Legal 纸（216 × 356 毫米）。
            AddSize(PaperKind.LegalExtra, 929, 1500); 	//Legal extra 纸（236 毫米 × 381 毫米）。该值特定于 PostScript 驱动程序，仅供 Linotronic 打印机使用以节省纸张。
            AddSize(PaperKind.Letter, 850, 1098); 	//Letter 纸（216 毫米 × 279 毫米）。
            AddSize(PaperKind.LetterExtra, 929, 1197); 	//Letter extra 纸（236 毫米 × 304 毫米）。该值特定于 PostScript 驱动程序，仅供 Linotronic 打印机使用以节省纸张。
            AddSize(PaperKind.LetterExtraTransverse, 929, 1201); 	//Letter extra transverse 纸（236 毫米 × 305 毫米）。
            AddSize(PaperKind.LetterPlus, 850, 1268); 	//Letter plus 纸（216 毫米 毫米 × 322 毫米）。
            AddSize(PaperKind.LetterRotated, 1098, 850); 	//Letter rotated 纸（279 毫米 × 216 毫米）。
            AddSize(PaperKind.LetterSmall, 850, 1098); 	//Letter small 纸（216 × 279 毫米）。
            AddSize(PaperKind.LetterTransverse, 827, 1098); 	//Letter transverse 纸（210 毫米 × 279 毫米）。
            AddSize(PaperKind.MonarchEnvelope, 386, 752); 	//Monarch envelope（98 毫米 × 191 毫米）。
            AddSize(PaperKind.Note, 850, 1098); 	//Note 纸（216 × 279 毫米）。
            AddSize(PaperKind.Number10Envelope, 413, 949); 	//#10 envelope（105 × 241 毫米）。
            AddSize(PaperKind.PersonalEnvelope, 362, 650); 	//6 3/4 envelope（92 毫米 × 165 毫米）。
            AddSize(PaperKind.Prc16K, 575, 846); 	//PRC 16K 纸（146 × 215 毫米）。需要 Windows 98、Windows NT 4.0 或更高版本。
            AddSize(PaperKind.Prc16KRotated, 575, 846); 	//PRC 16K rotated 纸（146 × 215 毫米）。需要 Windows 98、Windows NT 4.0 或更高版本。
            AddSize(PaperKind.Prc32K, 382, 594); 	//PRC 32K 纸（97 × 151 毫米）。需要 Windows 98、Windows NT 4.0 或更高版本。
            AddSize(PaperKind.Prc32KBig, 382, 594); 	//PRC 32K(Big) 纸（97 × 151 毫米）。需要 Windows 98、Windows NT 4.0 或更高版本。
            AddSize(PaperKind.Prc32KBigRotated, 382, 594); 	//PRC 32K rotated 纸（97 × 151 毫米）。需要 Windows 98、Windows NT 4.0 或更高版本。
            AddSize(PaperKind.Prc32KRotated, 382, 594); 	//PRC 32K rotated 纸（97 × 151 毫米）。需要 Windows 98、Windows NT 4.0 或更高版本。
            AddSize(PaperKind.PrcEnvelopeNumber1, 402, 650); 	//PRC #1 envelope（102 × 165 毫米）。需要 Windows 98、Windows NT 4.0 或更高版本。
            AddSize(PaperKind.PrcEnvelopeNumber10, 1276, 1803); 	//PRC #10 envelope（324 × 458 毫米）。需要 Windows 98、Windows NT 4.0 或更高版本。
            AddSize(PaperKind.PrcEnvelopeNumber10Rotated, 1803, 1276); 	//PRC #10 rotated envelope（458 × 324 毫米）。需要 Windows 98、Windows NT 4.0 或更高版本。
            AddSize(PaperKind.PrcEnvelopeNumber1Rotated, 650, 402); 	//PRC #1 rotated envelope（165 × 102 毫米）。需要 Windows 98、Windows NT 4.0 或更高版本。
            AddSize(PaperKind.PrcEnvelopeNumber2, 402, 693); 	//PRC #2 envelope（102 × 176 毫米）。需要 Windows 98、Windows NT 4.0 或更高版本。
            AddSize(PaperKind.PrcEnvelopeNumber2Rotated, 693, 402); 	//PRC #2 rotated envelope（176 × 102 毫米）。需要 Windows 98、Windows NT 4.0 或更高版本。
            AddSize(PaperKind.PrcEnvelopeNumber3, 492, 693); 	//PRC #3 envelope（125 × 176 毫米）。需要 Windows 98、Windows NT 4.0 或更高版本。
            AddSize(PaperKind.PrcEnvelopeNumber3Rotated, 693, 492); 	//PRC #3 rotated envelope（176 × 125 毫米）。需要 Windows 98、Windows NT 4.0 或更高版本。
            AddSize(PaperKind.PrcEnvelopeNumber4, 433, 819); 	//PRC #4 envelope（110 × 208 毫米）。需要 Windows 98、Windows NT 4.0 或更高版本。
            AddSize(PaperKind.PrcEnvelopeNumber4Rotated, 819, 433); 	//PRC #4 rotated envelope（208 × 110 毫米）。需要 Windows 98、Windows NT 4.0 或更高版本。
            AddSize(PaperKind.PrcEnvelopeNumber5, 433, 866); 	//PRC #5 envelope（110 × 220 毫米）。需要 Windows 98、Windows NT 4.0 或更高版本。
            AddSize(PaperKind.PrcEnvelopeNumber5Rotated, 866, 433); 	//PRC #5 rotated envelope（220 × 110 毫米）。需要 Windows 98、Windows NT 4.0 或更高版本。
            AddSize(PaperKind.PrcEnvelopeNumber6, 472, 906); 	//PRC #6 envelope（120 × 230 毫米）。需要 Windows 98、Windows NT 4.0 或更高版本。
            AddSize(PaperKind.PrcEnvelopeNumber6Rotated, 906, 472); 	//PRC #6 rotated envelope（230 × 120 毫米）。需要 Windows 98、Windows NT 4.0 或更高版本。
            AddSize(PaperKind.PrcEnvelopeNumber7, 630, 906); 	//PRC #7 envelope（160 × 230 毫米）。需要 Windows 98、Windows NT 4.0 或更高版本。
            AddSize(PaperKind.PrcEnvelopeNumber7Rotated, 906, 630); 	//PRC #7 rotated envelope（230 × 160 毫米）。需要 Windows 98、Windows NT 4.0 或更高版本。
            AddSize(PaperKind.PrcEnvelopeNumber8, 472, 1217); 	//PRC #8 envelope（120 × 309 毫米）。需要 Windows 98、Windows NT 4.0 或更高版本。
            AddSize(PaperKind.PrcEnvelopeNumber8Rotated, 1217, 472); 	//PRC #8 rotated envelope（309 × 120 毫米）。需要 Windows 98、Windows NT 4.0 或更高版本。
            AddSize(PaperKind.PrcEnvelopeNumber9, 902, 1276); 	//PRC #9 envelope（229 × 324 毫米）。需要 Windows 98、Windows NT 4.0 或更高版本。
            AddSize(PaperKind.PrcEnvelopeNumber9Rotated, 902, 1276); 	//PRC #9 rotated envelope（229 × 324 毫米）。需要 Windows 98、Windows NT 4.0 或更高版本。
            AddSize(PaperKind.Quarto, 846, 1083); 	//Quarto 纸（215 毫米 × 275 毫米）。
            AddSize(PaperKind.Standard10x11, 1000, 1098); 	//Standard 纸（254 毫米 × 279 毫米）。
            AddSize(PaperKind.Standard10x14, 1000, 1402); 	//Standard 纸（254 毫米 × 356 毫米）。
            AddSize(PaperKind.Standard11x17, 1098, 1701); 	//Standard 纸（279 毫米 × 432 毫米）。
            AddSize(PaperKind.Standard12x11, 1201, 1098); 	//Standard 纸（305 × 279 毫米）。需要 Windows 98、Windows NT 4.0 或更高版本。
            AddSize(PaperKind.Standard15x11, 1500, 1098); 	//Standard 纸（381 毫米 × 279 毫米）。
            AddSize(PaperKind.Standard9x11, 902, 1098); 	//Standard 纸（229 × 279 毫米）。
            AddSize(PaperKind.Statement, 551, 850); 	//Statement 纸（140 毫米 × 216 毫米）。
            AddSize(PaperKind.Tabloid, 1098, 1701); 	//Tabloid 纸（279 毫米 × 432 毫米）。
            AddSize(PaperKind.TabloidExtra, 1169, 1799); 	//Tabloid extra 纸（297 毫米 × 457 毫米）。该值特定于 PostScript 驱动程序，仅供 Linotronic 打印机使用以节省纸张。
            AddSize(PaperKind.USStandardFanfold, 1488, 1098); 	//US standard fanfold（378 毫米 × 279 毫米）。
            StandartInstances = list.ToArray();
        }
        private PaperKind kind;
        private string name;

        // standard hundredths of an inch units
        private int width;
        private int height;
        private bool createdByDefaultConstructor;

        /// <include file='doc\PaperSize.uex' path='docs/doc[@for="PaperSize.PaperSize2"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Initializes a new instance of the <see cref='System.Drawing.Printing.PaperSize'/> class with default properties.
        ///       This constructor is required for the serialization of the <see cref='System.Drawing.Printing.PaperSize'/> class.
        ///    </para>
        /// </devdoc>
        public PaperSize()
        {
            this.kind = PaperKind.Custom;
            this.name = String.Empty;
            this.createdByDefaultConstructor = true;
        }

        internal PaperSize(PaperKind kind, string name, int width, int height) {
            this.kind = kind;
            this.name = name;
            this.width = width;
            this.height = height;
        }

        internal PaperSize(PaperKind kind, int width, int height)
        {
            this.kind = kind;
            this.name = kind.ToString();
            this.width = width;
            this.height = height;
        }

        /// <include file='doc\PaperSize.uex' path='docs/doc[@for="PaperSize.PaperSize"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Initializes a new instance of the <see cref='System.Drawing.Printing.PaperSize'/> class.
        ///    </para>
        /// </devdoc>
        public PaperSize(string name, int width, int height) {
            this.kind = PaperKind.Custom;
            this.name = name;
            this.width = width;
            this.height = height;
        }

        /// <include file='doc\PaperSize.uex' path='docs/doc[@for="PaperSize.Height"]/*' />
        /// <devdoc>
        ///    <para>Gets or sets
        ///       the height of the paper, in hundredths of an inch.</para>
        /// </devdoc>
        public int Height {
            get {
                return height;
            }

            set {
                if (kind != PaperKind.Custom && !this.createdByDefaultConstructor) throw new ArgumentException(DCSR.GetString(DCSR.PSizeNotCustom));
                height = value;
            }
        }

        /// <include file='doc\PaperSize.uex' path='docs/doc[@for="PaperSize.Kind"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Gets the type of paper.
        ///       
        ///    </para>
        /// </devdoc>
        public PaperKind Kind {
            get {
                if (kind <= (PaperKind)SafeNativeMethods.DMPAPER_LAST && 
                    !(kind == (PaperKind)SafeNativeMethods.DMPAPER_RESERVED_48 || kind == (PaperKind)SafeNativeMethods.DMPAPER_RESERVED_49))
                    return kind;
                else
                    return PaperKind.Custom;
            }
        }

        /// <include file='doc\PaperSize.uex' path='docs/doc[@for="PaperSize.PaperName"]/*' />
        /// <devdoc>
        ///    <para>Gets
        ///       or sets the name of the type of paper.</para>
        /// </devdoc>
        public string PaperName {
            get { return name;}

            set {
                if (kind != PaperKind.Custom && !this.createdByDefaultConstructor) throw new ArgumentException(DCSR.GetString(DCSR.PSizeNotCustom));
                name = value;
            }
        }

        /// <include file='doc\PaperSize.uex' path='docs/doc[@for="PaperSize.RawKind"]/*' />
        /// <devdoc>
        /// <para>
        /// Same as Kind, but values larger than or equal to DMPAPER_LAST do not map to PaperKind.Custom.
        /// This property is needed for serialization of the PrinterSettings object.
        /// </para>
        /// </devdoc>
        public int RawKind
        {
            get { return unchecked((int) kind); }
            set { kind = unchecked((PaperKind) value); }
        }

        /// <include file='doc\PaperSize.uex' path='docs/doc[@for="PaperSize.Width"]/*' />
        /// <devdoc>
        ///    <para>Gets or sets
        ///       the width of the paper, in hundredths of an inch.</para>
        /// </devdoc>
        public int Width {
            get {
                return width;
            }

            set {
                if (kind != PaperKind.Custom && !createdByDefaultConstructor) throw new ArgumentException(DCSR.GetString(DCSR.PSizeNotCustom));
                width = value;
            }
        }

// I don't think we need this anymore
#if false
        private Point Dimensions {
            get {
                Point result;

                // Most of these numbers came straight from the header files.
                // The Japanese envelope ones came from [....].
                switch (Kind) {
                    case PaperKind.Custom: result = new Point(width, height); break;

                    case PaperKind.Letter: result = Inches(8.5, 11); break;
                    case PaperKind.Legal: result = Inches(8.5, 14); break;
                    case PaperKind.A4: result = Millimeters(210, 297); break;
                    case PaperKind.CSheet: result = Inches(17, 22); break;
                    case PaperKind.DSheet: result = Inches(22, 34); break;
                    case PaperKind.ESheet: result = Inches(34, 44); break;
                    case PaperKind.LetterSmall: result = Inches(8.5, 11); break;
                    case PaperKind.Tabloid: result = Inches(11, 17); break;
                    case PaperKind.Ledger: result = Inches(17, 11); break;
                    case PaperKind.Statement: result = Inches(5.5, 8.5); break;
                    case PaperKind.Executive: result = Inches(7.25, 10.5); break;
                    case PaperKind.A3: result = Millimeters(297, 420); break;
                    case PaperKind.A4Small: result = Millimeters(210, 297); break;
                    case PaperKind.A5: result = Millimeters(148, 210); break;
                    case PaperKind.B4: result = Millimeters(250, 354); break;
                    case PaperKind.B5: result = Millimeters(182, 257); break;
                    case PaperKind.Folio: result = Inches(8.5, 13); break;
                    case PaperKind.Quarto: result = Millimeters(215, 275); break;
                    case PaperKind.Standard10x14: result = Inches(10, 14); break;
                    case PaperKind.Standard11x17: result = Inches(11, 17); break;
                    case PaperKind.Note: result = Inches(8.5, 11); break;
                    case PaperKind.Number9Envelope: result = Inches(3.875, 8.875); break;
                    case PaperKind.Number10Envelope: result = Inches(4.125, 9.5); break;
                    case PaperKind.Number11Envelope: result = Inches(4.5, 10.375); break;
                    case PaperKind.Number12Envelope: result = Inches(4.75, 11); break;
                    case PaperKind.Number14Envelope: result = Inches(5, 11.5); break;
                    case PaperKind.DLEnvelope: result = Millimeters(110, 220); break;
                    case PaperKind.C5Envelope: result = Millimeters(162, 229); break;
                    case PaperKind.C3Envelope: result = Millimeters(324, 458); break;
                    case PaperKind.C4Envelope: result = Millimeters(229, 324); break;
                    case PaperKind.C6Envelope: result = Millimeters(114, 162); break;
                    case PaperKind.C65Envelope: result = Millimeters(114, 229); break;
                    case PaperKind.B4Envelope: result = Millimeters(250, 353); break;
                    case PaperKind.B5Envelope: result = Millimeters(176, 250); break;
                    case PaperKind.B6Envelope: result = Millimeters(176, 125); break;
                    case PaperKind.ItalyEnvelope: result = Millimeters(110, 230); break;
                    case PaperKind.MonarchEnvelope: result = Inches(3.875, 7.5); break;
                    case PaperKind.PersonalEnvelope: result = Inches(3.625, 6.5); break;
                    case PaperKind.USStandardFanfold: result = Inches(14.875, 11); break;
                    case PaperKind.GermanStandardFanfold: result = Inches(8.5, 12); break;
                    case PaperKind.GermanLegalFanfold: result = Inches(8.5, 13); break;

                    case PaperKind.ISOB4: result = Millimeters(250, 353); break;
                    case PaperKind.JapanesePostcard: result = Millimeters(100, 148); break;
                    case PaperKind.Standard9x11: result = Inches(9, 11); break;
                    case PaperKind.Standard10x11: result = Inches(10, 11); break;
                    case PaperKind.Standard15x11: result = Inches(15, 11); break;
                    case PaperKind.InviteEnvelope: result = Millimeters(220, 220); break;
                        //= SafeNativeMethods.DMPAPER_RESERVED_48,
                        //= SafeNativeMethods.DMPAPER_RESERVED_49,
                    case PaperKind.LetterExtra: result = Inches(9.275, 12); break;
                    case PaperKind.LegalExtra: result = Inches(9.275, 15); break;
                    case PaperKind.TabloidExtra: result = Inches(11.69, 18); break;
                    case PaperKind.A4Extra: result = Inches(9.27, 12.69); break;
                    case PaperKind.LetterTransverse: result = Inches(8.275, 11); break;
                    case PaperKind.A4Transverse: result = Millimeters(210, 297); break;
                    case PaperKind.LetterExtraTransverse: result = Inches(9.275, 12); break;
                    case PaperKind.APlus: result = Millimeters(227, 356); break;
                    case PaperKind.BPlus: result = Millimeters(305, 487); break;
                    case PaperKind.LetterPlus: result = Inches(8.5, 12.69); break;
                    case PaperKind.A4Plus: result = Millimeters(210, 330); break;
                    case PaperKind.A5Transverse: result = Millimeters(148, 210); break;
                    case PaperKind.B5Transverse: result = Millimeters(182, 257); break;
                    case PaperKind.A3Extra: result = Millimeters(322, 445); break;
                    case PaperKind.A5Extra: result = Millimeters(174, 235); break;
                    case PaperKind.B5Extra: result = Millimeters(201, 276); break;
                    case PaperKind.A2: result = Millimeters(420, 594); break;
                    case PaperKind.A3Transverse: result = Millimeters(297, 420); break;
                    case PaperKind.A3ExtraTransverse: result = Millimeters(322, 445); break;

                    case PaperKind.JapaneseDoublePostcard: result = Millimeters(200, 148); break;
                    case PaperKind.A6: result = Millimeters(105, 148); break;
                    case PaperKind.JapaneseEnvelopeKakuNumber2: result = Millimeters(240, 332); break;
                    case PaperKind.JapaneseEnvelopeKakuNumber3: result = Millimeters(216, 277); break;
                    case PaperKind.JapaneseEnvelopeChouNumber3: result = Millimeters(120, 235); break;
                    case PaperKind.JapaneseEnvelopeChouNumber4: result = Millimeters(90, 205); break;
                    case PaperKind.LetterRotated: result = Inches(11, 8.5); break;
                    case PaperKind.A3Rotated: result = Millimeters(420, 297); break;
                    case PaperKind.A4Rotated: result = Millimeters(297, 210); break;
                    case PaperKind.A5Rotated: result = Millimeters(210, 148); break;
                    case PaperKind.B4JISRotated: result = Millimeters(364, 257); break;
                    case PaperKind.B5JISRotated: result = Millimeters(257, 182); break;
                    case PaperKind.JapanesePostcardRotated: result = Millimeters(148, 100); break;
                    case PaperKind.JapaneseDoublePostcardRotated: result = Millimeters(148, 200); break;
                    case PaperKind.A6Rotated: result = Millimeters(148, 105); break;
                    case PaperKind.JapaneseEnvelopeKakuNumber2Rotated: result = Millimeters(332, 240); break;
                    case PaperKind.JapaneseEnvelopeKakuNumber3Rotated: result = Millimeters(277, 216); break;
                    case PaperKind.JapaneseEnvelopeChouNumber3Rotated: result = Millimeters(235, 120); break;
                    case PaperKind.JapaneseEnvelopeChouNumber4Rotated: result = Millimeters(205, 90); break;
                    case PaperKind.B6JIS: result = Millimeters(128, 182); break;
                    case PaperKind.B6JISRotated: result = Millimeters(182, 128); break;
                    case PaperKind.Standard12x11: result = Inches(12, 11); break;
                    case PaperKind.JapaneseEnvelopeYouNumber4: result = Millimeters(105, 235); break;
                    case PaperKind.JapaneseEnvelopeYouNumber4Rotated: result = Millimeters(235, 105); break;
                    case PaperKind.PRC16K: result = Millimeters(146, 215); break;
                    case PaperKind.PRC32K: result = Millimeters(97, 151); break;
                    case PaperKind.PRC32KBig: result = Millimeters(97, 151); break;
                    case PaperKind.PRCEnvelopeNumber1: result = Millimeters(102, 165); break;
                    case PaperKind.PRCEnvelopeNumber2: result = Millimeters(102, 176); break;
                    case PaperKind.PRCEnvelopeNumber3: result = Millimeters(125, 176); break;
                    case PaperKind.PRCEnvelopeNumber4: result = Millimeters(110, 208); break;
                    case PaperKind.PRCEnvelopeNumber5: result = Millimeters(110, 220); break;
                    case PaperKind.PRCEnvelopeNumber6: result = Millimeters(120, 230); break;
                    case PaperKind.PRCEnvelopeNumber7: result = Millimeters(160, 230); break;
                    case PaperKind.PRCEnvelopeNumber8: result = Millimeters(120, 309); break;
                    case PaperKind.PRCEnvelopeNumber9: result = Millimeters(229, 324); break;
                    case PaperKind.PRCEnvelopeNumber10: result = Millimeters(324, 458); break;
                    case PaperKind.PRC16KRotated: result = Millimeters(215, 146); break;
                    case PaperKind.PRC32KRotated: result = Millimeters(151, 97); break;
                    case PaperKind.PRC32KBigRotated: result = Millimeters(151, 97); break;
                    case PaperKind.PRCEnvelopeNumber1Rotated: result = Millimeters(165, 102); break;
                    case PaperKind.PRCEnvelopeNumber2Rotated: result = Millimeters(176, 102); break;
                    case PaperKind.PRCEnvelopeNumber3Rotated: result = Millimeters(176, 125); break;
                    case PaperKind.PRCEnvelopeNumber4Rotated: result = Millimeters(208, 110); break;
                    case PaperKind.PRCEnvelopeNumber5Rotated: result = Millimeters(220, 110); break;
                    case PaperKind.PRCEnvelopeNumber6Rotated: result = Millimeters(230, 120); break;
                    case PaperKind.PRCEnvelopeNumber7Rotated: result = Millimeters(230, 160); break;
                    case PaperKind.PRCEnvelopeNumber8Rotated: result = Millimeters(309, 120); break;
                    case PaperKind.PRCEnvelopeNumber9Rotated: result = Millimeters(324, 229); break;
                    case PaperKind.PRCEnvelopeNumber10Rotated: result = Millimeters(458, 324); break;

                    default:
                        Debug.Fail("Unknown paper kind " + unchecked((int) kind));
                        result = new Point(0, 0);
                        break;
                }
                return result;
            }
        }

        private static Point Inches(double width, double height) {
            Debug.Assert(width < 20 && height < 20, "You said inches, but you probably meant millimeters (" + width + ", " + height + ")");
            float conversion = 254;
            return new Point((int) (width * conversion), (int) (height * conversion));
        }

        private static Point Millimeters(double width, double height) {
            Debug.Assert(width > 20 && height > 20, "You said millimeters, but you probably meant inches (" + width + ", " + height + ")");
            float conversion = 10;
            return new Point((int) (width * conversion), (int) (height * conversion));
        }
#endif

        /// <include file='doc\PaperSize.uex' path='docs/doc[@for="PaperSize.ToString"]/*' />
        /// <internalonly/>
        /// <devdoc>
        ///    <para>
        ///       Provides some interesting information about the PaperSize in
        ///       String form.
        ///    </para>
        /// </devdoc>
        public override string ToString() {
            return "[PaperSize " + PaperName
            + " Kind=" + unchecked(TypeDescriptor.GetConverter(typeof(PaperKind)).ConvertToString((int) Kind))
            + " Height=" + Height.ToString(CultureInfo.InvariantCulture)
            + " Width=" + Width.ToString(CultureInfo.InvariantCulture)
            + "]";
        }
    }
}

