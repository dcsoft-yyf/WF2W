//------------------------------------------------------------------------------
// <copyright file="PageSetupDialog.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>                                                                
//------------------------------------------------------------------------------

namespace System.Windows.Forms {

    using Microsoft.Win32;
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Drawing.Printing;
    using System.Runtime.InteropServices;
    using System.Security;
    using System.Text;
    using System.Globalization;
    
    /// <include file='doc\PageSetupDialog.uex' path='docs/doc[@for="PageSetupDialog"]/*' />
    /// <devdoc>
    ///    <para> Represents
    ///       a dialog box that allows users to manipulate page settings, including margins and paper orientation.</para>
    /// </devdoc>
    [DefaultProperty("Document")]
    [SRDescription(DCSR.DescriptionPageSetupDialog)]
    // The only event this dialog has is HelpRequested, which isn't very useful
    public sealed class PageSetupDialog : CommonDialog {
        // If PrintDocument != null, pageSettings == printDocument.PageSettings
        private PrintDocument printDocument = null;
        private PageSettings pageSettings = null;
        private PrinterSettings printerSettings = null;

        private bool allowMargins;
        private bool allowOrientation;
        private bool allowPaper;
        private bool allowPrinter;
        private Margins minMargins;
        private bool showHelp;
        private bool showNetwork;
        private bool enableMetric;
        
        /// <include file='doc\PageSetupDialog.uex' path='docs/doc[@for="PageSetupDialog.PageSetupDialog"]/*' />
        /// <devdoc>
        /// <para>Initializes a new instance of the <see cref='System.Windows.Forms.PageSetupDialog'/> class.</para>
        /// </devdoc>
        public PageSetupDialog() {
            Reset();
        }

        /// <include file='doc\PageSetupDialog.uex' path='docs/doc[@for="PageSetupDialog.AllowMargins"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Gets or sets a value indicating whether the margins section of the dialog box is enabled.
        ///       
        ///    </para>
        /// </devdoc>
        [
        SRCategory(DCSR.CatBehavior), 
        DefaultValue(true),
        SRDescription(DCSR.PSDallowMarginsDescr)
        ]
        public bool AllowMargins {
            get { 
                return allowMargins;
            }
            set { 
                allowMargins = value;
            }
        }

        /// <include file='doc\PageSetupDialog.uex' path='docs/doc[@for="PageSetupDialog.AllowOrientation"]/*' />
        /// <devdoc>
        ///    <para> Gets or sets a value indicating whether the orientation section of the dialog box (landscape vs. portrait)
        ///       is enabled.
        ///       </para>
        /// </devdoc>
        [
        SRCategory(DCSR.CatBehavior), 
        DefaultValue(true),
        SRDescription(DCSR.PSDallowOrientationDescr)
        ]
        public bool AllowOrientation {
            get { return allowOrientation;}
            set { allowOrientation = value;}
        }

        /// <include file='doc\PageSetupDialog.uex' path='docs/doc[@for="PageSetupDialog.AllowPaper"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Gets or sets a value indicating whether the paper section of the dialog box (paper size and paper source)
        ///       is enabled.
        ///       
        ///    </para>
        /// </devdoc>
        [
        SRCategory(DCSR.CatBehavior), 
        DefaultValue(true),
        SRDescription(DCSR.PSDallowPaperDescr)
        ]
        public bool AllowPaper {
            get { return allowPaper;}
            set { allowPaper = value;}
        }

        /// <include file='doc\PageSetupDialog.uex' path='docs/doc[@for="PageSetupDialog.AllowPrinter"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Gets or sets a value indicating whether the Printer button is enabled.
        ///       
        ///    </para>
        /// </devdoc>
        [
        SRCategory(DCSR.CatBehavior), 
        DefaultValue(true),
        SRDescription(DCSR.PSDallowPrinterDescr)
        ]
        public bool AllowPrinter {
            get { return allowPrinter;}
            set { allowPrinter = value;}
        }

        /// <include file='doc\PageSetupDialog.uex' path='docs/doc[@for="PageSetupDialog.Document"]/*' />
        /// <devdoc>
        /// <para>Gets or sets a value indicating the <see cref='System.Drawing.Printing.PrintDocument'/> 
        /// to get page settings from.
        /// </para>
        /// </devdoc>
        [
        SRCategory(DCSR.CatData), 
        DefaultValue(null),
        SRDescription(DCSR.PDdocumentDescr)
        ]
        public PrintDocument Document {
            get { return printDocument;}
            set { 
                printDocument = value;
                if (printDocument != null) {
                    pageSettings = printDocument.DefaultPageSettings;
                    printerSettings = printDocument.PrinterSettings;
                }
            }
        }

        /// <include file='doc\PageSetupDialog.uex' path='docs/doc[@for="PageSetupDialog.EnableMetric"]/*' />
        /// <devdoc>
        ///    This allows the user to override the current behavior where the Metric is converted to ThousandOfInch even for METRIC MEASUREMENTSYSTEM
        ///    which returns a HUNDREDSOFMILLIMETER value.
        /// </devdoc>
        [
        DefaultValue(false),
        SRDescription(DCSR.PSDenableMetricDescr),
        Browsable(true), EditorBrowsable(EditorBrowsableState.Always)
        ]
        public bool EnableMetric {
            get { return enableMetric;}
            set { enableMetric = value;}
        }

        /// <include file='doc\PageSetupDialog.uex' path='docs/doc[@for="PageSetupDialog.MinMargins"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Gets or sets a value indicating the minimum margins the
        ///       user is allowed to select, in hundredths of an inch.
        ///       
        ///    </para>
        /// </devdoc>
        [
        SRCategory(DCSR.CatData), 
        SRDescription(DCSR.PSDminMarginsDescr)
        ]
        public Margins MinMargins {
            get { return minMargins;}
            set { 
                if (value == null)
                    value = new Margins(0, 0, 0, 0);
                minMargins = value;
            }
        }

        /// <include file='doc\PageSetupDialog.uex' path='docs/doc[@for="PageSetupDialog.PageSettings"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Gets
        ///       or sets
        ///       a value indicating
        ///       the page settings modified by the dialog box.
        ///       
        ///    </para>
        /// </devdoc>
        [
        SRCategory(DCSR.CatData), 
        DefaultValue(null),
        Browsable(false),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
        SRDescription(DCSR.PSDpageSettingsDescr)
        ]
        public PageSettings PageSettings {
            get { return pageSettings;}
            set {
                pageSettings = value;
                printDocument = null;
            }
        }

        /// <include file='doc\PageSetupDialog.uex' path='docs/doc[@for="PageSetupDialog.PrinterSettings"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Gets
        ///       or sets the printer
        ///       settings the dialog box will modify if the user clicks the Printer button.
        ///    </para>
        /// </devdoc>
        [
        SRCategory(DCSR.CatData), 
        DefaultValue(null),
        Browsable(false),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
        SRDescription(DCSR.PSDprinterSettingsDescr)
        ]
        public PrinterSettings PrinterSettings {
            get { return printerSettings;}
            set {
                printerSettings = value;
                printDocument = null;
            }
        }

        /// <include file='doc\PageSetupDialog.uex' path='docs/doc[@for="PageSetupDialog.ShowHelp"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Gets or sets a value indicating whether the Help button is visible.
        ///    </para>
        /// </devdoc>
        [
        SRCategory(DCSR.CatBehavior), 
        DefaultValue(false),
        SRDescription(DCSR.PSDshowHelpDescr)
        ]
        public bool ShowHelp {
            get { return showHelp;}
            set { showHelp = value;}
        }

        /// <include file='doc\PageSetupDialog.uex' path='docs/doc[@for="PageSetupDialog.ShowNetwork"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Gets or sets a value indicating whether the Network button is visible.
        ///    </para>
        /// </devdoc>
        [
        SRCategory(DCSR.CatBehavior), 
        DefaultValue(true),
        SRDescription(DCSR.PSDshowNetworkDescr)
        ]
        public bool ShowNetwork {
            get { return showNetwork;}
            set { showNetwork = value;}
        }

        private int GetFlags() {
            int flags = 0;
            flags |= WinFormNativeMethods.PSD_ENABLEPAGESETUPHOOK;

            if (!allowMargins) flags |= WinFormNativeMethods.PSD_DISABLEMARGINS;
            if (!allowOrientation) flags |= WinFormNativeMethods.PSD_DISABLEORIENTATION;
            if (!allowPaper) flags |= WinFormNativeMethods.PSD_DISABLEPAPER;
            if (!allowPrinter || printerSettings == null) flags |= WinFormNativeMethods.PSD_DISABLEPRINTER;

            if (showHelp) flags |= WinFormNativeMethods.PSD_SHOWHELP;
            if (!showNetwork) flags |= WinFormNativeMethods.PSD_NONETWORKBUTTON;
            if (minMargins != null) flags |= WinFormNativeMethods.PSD_MINMARGINS;
            if (pageSettings.Margins != null) flags |= WinFormNativeMethods.PSD_MARGINS;

            // 
            return flags;
        }

        /// <include file='doc\PageSetupDialog.uex' path='docs/doc[@for="PageSetupDialog.Reset"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Resets all options to their default values.
        ///    </para>
        /// </devdoc>
        public override void Reset() {
            allowMargins = true;
            allowOrientation = true;
            allowPaper = true;
            allowPrinter = true;
            MinMargins = null; // turns into Margin with all zeros
            pageSettings = null;
            printDocument = null;
            printerSettings = null;
            showHelp = false;
            showNetwork = true;
        }

        private void ResetMinMargins() {
            MinMargins = null;
        }

        /// <include file='doc\PageSetupDialog.uex' path='docs/doc[@for="PageSetupDialog.ShouldSerializeMinMargins"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Indicates whether the <see cref='System.Windows.Forms.PageSetupDialog.MinMargins'/>
        ///       property should be
        ///       persisted.
        ///    </para>
        /// </devdoc>
        private bool ShouldSerializeMinMargins() {
            return minMargins.Left != 0
            || minMargins.Right != 0
            || minMargins.Top != 0
            || minMargins.Bottom != 0;
        }

        private static void UpdateSettings(WinFormNativeMethods.PAGESETUPDLG data, PageSettings pageSettings,
                                           PrinterSettings printerSettings) {
            // SetHDevMode demands AllPrintingAndUnmanagedCode Permission : Since we are calling that function we should Assert the permision,
            //IntSecurity.AllPrintingAndUnmanagedCode.Assert();
            try
            {
                pageSettings.SetHdevmode(data.hDevMode);
                if (printerSettings != null) {
                    printerSettings.SetHdevmode(data.hDevMode);
                    printerSettings.SetHdevnames(data.hDevNames);
                }
            }
            finally
            {
                //CodeAccessPermission.RevertAssert();
            }

            Margins newMargins = new Margins();
            newMargins.Left = data.marginLeft;
            newMargins.Top = data.marginTop;
            newMargins.Right = data.marginRight;
            newMargins.Bottom = data.marginBottom;

            PrinterUnit fromUnit = ((data.Flags & WinFormNativeMethods.PSD_INHUNDREDTHSOFMILLIMETERS) != 0)
                                   ? PrinterUnit.HundredthsOfAMillimeter
                                   : PrinterUnit.ThousandthsOfAnInch;
            
            pageSettings.Margins = PrinterUnitConvert.Convert(newMargins, fromUnit, PrinterUnit.Display);
        }


        /// <include file='doc\PageSetupDialog.uex' path='docs/doc[@for="PageSetupDialog.RunDialog"]/*' />
        /// <devdoc>
        /// </devdoc>
        /// <internalonly/>
        protected override bool RunDialog(IntPtr hwndOwner) {
            //IntSecurity.SafePrinting.Demand();

            WinFormNativeMethods.WndProc hookProcPtr = new WinFormNativeMethods.WndProc(this.HookProc);
            if (pageSettings == null)
                throw new ArgumentException(DCSR.GetString(DCSR.PSDcantShowWithoutPage));

            WinFormNativeMethods.PAGESETUPDLG data = new WinFormNativeMethods.PAGESETUPDLG();
            data.lStructSize = Marshal.SizeOf(data);
            data.Flags = GetFlags();
            data.hwndOwner = hwndOwner;
            data.lpfnPageSetupHook = hookProcPtr;
            
            PrinterUnit toUnit = PrinterUnit.ThousandthsOfAnInch;

            // Refer VSWhidbey: 331160. Below was a breaking change from RTM and EVERETT even though this was a correct FIX.
            // EnableMetric is a new Whidbey property which we allow the users to choose between the AutoConversion or not.
            if (EnableMetric)
            {
                //take the Units of Measurement while determining the PrinterUnits...
                //bug (121347)...
                StringBuilder sb = new StringBuilder(2);
                int result = WinFormUnsafeNativeMethods.GetLocaleInfo(WinFormNativeMethods.LOCALE_USER_DEFAULT,WinFormNativeMethods.LOCALE_IMEASURE, sb,sb.Capacity);
                
                if (result > 0 && Int32.Parse(sb.ToString(), CultureInfo.InvariantCulture) == 0) {
                    toUnit = PrinterUnit.HundredthsOfAMillimeter;
                }
            }

            if (MinMargins != null) {
                Margins margins = PrinterUnitConvert.Convert(MinMargins, PrinterUnit.Display, toUnit);
                data.minMarginLeft = margins.Left;
                data.minMarginTop = margins.Top;
                data.minMarginRight = margins.Right;
                data.minMarginBottom = margins.Bottom;
            }

            if (pageSettings.Margins != null) {
                Margins margins = PrinterUnitConvert.Convert(pageSettings.Margins, PrinterUnit.Display, toUnit);
                data.marginLeft = margins.Left;
                data.marginTop = margins.Top;
                data.marginRight = margins.Right;
                data.marginBottom = margins.Bottom;
            }

            // Ensure that the margins are >= minMargins.
            // This is a requirement of the PAGESETUPDLG structure.
            //
            data.marginLeft = Math.Max(data.marginLeft, data.minMarginLeft);
            data.marginTop = Math.Max(data.marginTop, data.minMarginTop);
            data.marginRight = Math.Max(data.marginRight, data.minMarginRight);
            data.marginBottom = Math.Max(data.marginBottom, data.minMarginBottom);           

            PrinterSettings printer = (printerSettings == null) ? pageSettings.PrinterSettings : printerSettings;

            // GetHDevmode demands AllPrintingAndUnmanagedCode Permission : Since we are calling that function we should Assert the permision,
            //IntSecurity.AllPrintingAndUnmanagedCode.Assert();
            try {
                data.hDevMode = printer.GetHdevmode(pageSettings);
                data.hDevNames = printer.GetHdevnames();
            }
            finally {
                //CodeAccessPermission.RevertAssert();
            }

            try {
                bool status = WinFormUnsafeNativeMethods.PageSetupDlg(data);
                if (!status) {
                    // Debug.WriteLine(Windows.CommonDialogErrorToString(Windows.CommDlgExtendedError()));
                    return false;
                }
                
                UpdateSettings(data, pageSettings, printerSettings); // yes, printerSettings, not printer
                return true;
            }
            finally {
                WinFormUnsafeNativeMethods.GlobalFree(new HandleRef(data, data.hDevMode));
                WinFormUnsafeNativeMethods.GlobalFree(new HandleRef(data, data.hDevNames));
            }
        }
    }
}

