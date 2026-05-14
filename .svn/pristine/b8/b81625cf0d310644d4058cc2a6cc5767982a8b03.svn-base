//------------------------------------------------------------------------------
// <copyright file="PrintDocument.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>                                                                
//------------------------------------------------------------------------------

namespace System.Drawing.Printing {

    using Microsoft.Win32;
    using System;
    using System.ComponentModel;
    using System.ComponentModel.Design;
    using System.Diagnostics;
    using System.Drawing;    
    using System.Drawing.Design;
    using System.Reflection;
    using System.Security;
    using System.Security.Permissions;
    using System.IO;
    using System.Runtime.Versioning;

    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = false)]
    public class PrintDocument : Component {
        static PrintDocument()
        {
            DCValueConvert.CheckIsBlazorWASM();
        }
        private string documentName = "document";

        private PrintEventHandler beginPrintHandler;
        private PrintEventHandler endPrintHandler;
        private PrintPageEventHandler printPageHandler;
        private QueryPageSettingsEventHandler queryHandler;

        private PrinterSettings _PrinterSettings = new PrinterSettings();
        private PageSettings _DefaultPageSettings;

        private PrintController printController;

        private bool originAtMargins;
        private bool userSetPageSettings;

        /// <include file='doc\PrintDocument.uex' path='docs/doc[@for="PrintDocument.PrintDocument"]/*' />
        /// <devdoc>
        /// <para>Initializes a new instance of the <see cref='System.Drawing.Printing.PrintDocument'/>
        /// class.</para>
        /// </devdoc>
        public PrintDocument() {
            _DefaultPageSettings = new PageSettings(_PrinterSettings);
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true )]
        [
        Browsable(false),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
        SRDescription(DCSR.PDOCdocumentPageSettingsDescr)
        ]
        public PageSettings DefaultPageSettings {
            get { return _DefaultPageSettings;}
            set { 
                if (value == null)
                    value = new PageSettings();
                _DefaultPageSettings = value;
                userSetPageSettings = true;
            }
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        [
        DefaultValue("document"),
        SRDescription(DCSR.PDOCdocumentNameDescr)
        ]
        public string DocumentName {
            get { return documentName;}

            set {
                if (value == null)
                    value = "";
                documentName = value;
            }
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        [
        DefaultValue(false),
        SRDescription(DCSR.PDOCoriginAtMarginsDescr)
        ]
        public bool OriginAtMargins {
            get
            {
                return originAtMargins;
            }
            set
            {
                originAtMargins = value;
            }
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        [
        Browsable(false),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
        SRDescription(DCSR.PDOCprintControllerDescr)
        ]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public PrintController PrintController {
            get { 
                //IntSecurity.SafePrinting.Demand();

                if (printController == null) {
                    printController = new StandardPrintController();
                    ////new ReflectionPermission(PermissionState.Unrestricted).Assert();
                    //try {
                    //    try {
                    //        // SECREVIEW 332064: this is here because System.Drawing doesnt want a dependency on
                    //        // System.Windows.Forms.  Since this creates a public type in another assembly, this 
                    //        // appears to be OK.
                    //        Type type = Type.GetType("System.Windows.Forms.PrintControllerWithStatusDialog, " + AssemblyRef.SystemWindowsForms);
                    //        printController = (PrintController) Activator.CreateInstance(type, 
                    //            BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.CreateInstance, 
                    //            null, new object[] { printController}, null);
                    //    }
                    //    catch (TypeLoadException) {
                    //        Debug.Fail("Can't find System.Windows.Forms.PrintControllerWithStatusDialog, proceeding with StandardPrintController");
                    //    }
                    //    catch (TargetInvocationException) {
                    //        Debug.Fail("Can't find System.Windows.Forms.PrintControllerWithStatusDialog, proceeding with StandardPrintController");
                    //    }
                    //    catch (MissingMethodException) {
                    //        Debug.Fail("Can't find System.Windows.Forms.PrintControllerWithStatusDialog, proceeding with StandardPrintController");
                    //    }
                    //    catch (MethodAccessException) {
                    //        Debug.Fail("Can't find System.Windows.Forms.PrintControllerWithStatusDialog, proceeding with StandardPrintController");
                    //    }
                    //    catch (MemberAccessException) {
                    //        Debug.Fail("Can't find System.Windows.Forms.PrintControllerWithStatusDialog, proceeding with StandardPrintController");
                    //    }
                    //    catch (FileNotFoundException) {
                    //        Debug.Fail("Can't find System.Windows.Forms.PrintControllerWithStatusDialog, proceeding with StandardPrintController");
                    //    }
                    //}
                    //finally {
                    //    //CodeAccessPermission.RevertAssert();
                    //}
                }
                return printController;
            }
            set {
                //IntSecurity.SafePrinting.Demand();

                printController = value;
            }
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        [
        Browsable(false),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
        SRDescription(DCSR.PDOCprinterSettingsDescr)
        ]
        public PrinterSettings PrinterSettings {
            get { return _PrinterSettings;}
            set { 
                if (value == null)
                    value = new PrinterSettings();
                _PrinterSettings = value;
                // reset the PageSettings that match the PrinterSettings only if we have created the defaultPageSettings..
                if (!userSetPageSettings)
                {
                    _DefaultPageSettings = _PrinterSettings.DefaultPageSettings;
                }
            }
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        [SRDescription(DCSR.PDOCbeginPrintDescr)]
        public event PrintEventHandler BeginPrint {
            add {
                beginPrintHandler += value;
            }
            remove {
                beginPrintHandler -= value;
            }
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        [SRDescription(DCSR.PDOCendPrintDescr)]
        public event PrintEventHandler EndPrint {
            add {
                endPrintHandler += value;
            }
            remove {
                endPrintHandler -= value;
            }
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        [SRDescription(DCSR.PDOCprintPageDescr)]
        public event PrintPageEventHandler PrintPage {
            add {
                printPageHandler += value;
            }
            remove {
                printPageHandler -= value;
            }
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        [SRDescription(DCSR.PDOCqueryPageSettingsDescr)]
        public event QueryPageSettingsEventHandler QueryPageSettings {
            add {
                queryHandler += value;
            }
            remove {
                queryHandler -= value;
            }
        }

        internal void _OnBeginPrint(PrintEventArgs e) {
            OnBeginPrint(e);
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        protected virtual void OnBeginPrint(PrintEventArgs e) {
            if (beginPrintHandler != null)
                beginPrintHandler(this, e);
        }

        internal void _OnEndPrint(PrintEventArgs e) {
            OnEndPrint(e);
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        protected virtual void OnEndPrint(PrintEventArgs e) {
            if (endPrintHandler != null)
                endPrintHandler(this, e);
        }

        internal void _OnPrintPage(PrintPageEventArgs e) {
            OnPrintPage(e);
        }


        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        protected virtual void OnPrintPage(PrintPageEventArgs e) {
            if (printPageHandler != null)
                printPageHandler(this, e);
        }

        internal void _OnQueryPageSettings(QueryPageSettingsEventArgs e) {
            OnQueryPageSettings(e);
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        protected virtual void OnQueryPageSettings(QueryPageSettingsEventArgs e) {
            if (queryHandler != null)
                queryHandler(this, e);
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public async System.Threading.Tasks.ValueTask PrintAsync() {
            // It is possible to SetPrinterName using signed secured dll which can be used to by-pass Printing security model.
            // hence here check if the PrinterSettings.IsDefaultPrinter and if not demand AllPrinting.
            // Refer : VsWhidbey : 235920
            //if (!this.PrinterSettings.IsDefaultPrinter && !this.PrinterSettings.PrintDialogDisplayed)
            //{
            //    IntSecurity.AllPrinting.Demand();
            //}
            PrintController controller = PrintController;
            await controller.PrintAsync(this);
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void Print()
        {
            // It is possible to SetPrinterName using signed secured dll which can be used to by-pass Printing security model.
            // hence here check if the PrinterSettings.IsDefaultPrinter and if not demand AllPrinting.
            // Refer : VsWhidbey : 235920
            //if (!this.PrinterSettings.IsDefaultPrinter && !this.PrinterSettings.PrintDialogDisplayed)
            //{
            //    IntSecurity.AllPrinting.Demand();
            //}
            PrintController controller = PrintController;
            controller.Print(this);
        }
        /// <include file='doc\PrintDocument.uex' path='docs/doc[@for="PrintDocument.ToString"]/*' />
        /// <internalonly/>
        /// <devdoc>
        ///    <para>
        ///       Provides some interesting information about the PrintDocument in
        ///       String form.
        ///    </para>
        /// </devdoc>
        public override string ToString() {
            return "[PrintDocument " + DocumentName + "]";
        }
    }
}

