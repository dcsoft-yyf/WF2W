//------------------------------------------------------------------------------
// <copyright file="SaveFileDialog.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>                                                                
//------------------------------------------------------------------------------

namespace System.Windows.Forms {

    using System.Diagnostics;

    using System;
    //using CodeAccessPermission = System.Security.CodeAccessPermission;
    using System.IO;
    using System.Drawing;
    using System.Diagnostics.CodeAnalysis;
    using System.Security.Permissions;
    using System.ComponentModel;
    using System.Windows.Forms;
    using Microsoft.Win32;
    using System.Runtime.Versioning;

    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = false)]
    public sealed class SaveFileDialog : FileDialog {

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true )]
        [
        SRCategory(DCSR.CatBehavior), 
        DefaultValue(false),
        SRDescription(DCSR.SaveFileDialogCreatePrompt)
        ]
        public bool CreatePrompt {
            get {
                return GetOption(WinFormNativeMethods.OFN_CREATEPROMPT);
            }
            set {
                //Debug.WriteLineIf(IntSecurity.SecurityDemand.TraceVerbose, "FileDialogCustomization Demanded");
                //IntSecurity.FileDialogCustomization.Demand();
                SetOption(WinFormNativeMethods.OFN_CREATEPROMPT, value);
            }
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        [
        SRCategory(DCSR.CatBehavior), 
        DefaultValue(true),
        SRDescription(DCSR.SaveFileDialogOverWritePrompt)
        ]
        public bool OverwritePrompt {
            get {
                return GetOption(WinFormNativeMethods.OFN_OVERWRITEPROMPT);
            }
            set {
                //Debug.WriteLineIf(IntSecurity.SecurityDemand.TraceVerbose, "FileDialogCustomization Demanded");
                //IntSecurity.FileDialogCustomization.Demand();
                SetOption(WinFormNativeMethods.OFN_OVERWRITEPROMPT, value);
            }
        }
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public DialogResult ShowDialog()
        {
            MessageBox.Show("SaveFileDialog " + DCSR.GetString("NotSupportInWF2W"));
            return DialogResult.Cancel;
        }
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public DialogResult ShowDialog(IWin32Window owner)
        {
            MessageBox.Show("SaveFileDialog " + DCSR.GetString("NotSupportInWF2W"));
            return DialogResult.Cancel;

        }
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public Stream OpenFile() {
            throw new NotSupportedException(DCSR.GetString("NotSupportInWF2W"));
            ////Debug.WriteLineIf(IntSecurity.SecurityDemand.TraceVerbose, "FileDialogSaveFile Demanded");
            ////IntSecurity.FileDialogSaveFile.Demand();

            //string filename = FileNamesInternal[0];

            //if (string.IsNullOrEmpty(filename))
            //    throw new ArgumentNullException( "FileName" );

            //Stream s = null;

            //// SECREVIEW : We demanded the FileDialog permission above, so it is safe
            ////           : to assert this here. Since the user picked the file, it
            ////           : is OK to give them read/write access to the stream.
            ////
            ////new FileIOPermission(FileIOPermissionAccess.AllAccess, IntSecurity.UnsafeGetFullPath(filename)).Assert();
            //try {
            //    s = new FileStream(filename, FileMode.Create, FileAccess.ReadWrite);
            //}
            //finally {
            //   // CodeAccessPermission.RevertAssert();
            //}
            //return s;
        }

        ///// <include file='doc\SaveFileDialog.uex' path='docs/doc[@for="SaveFileDialog.PromptFileCreate"]/*' />
        ///// <devdoc>
        /////    <para>
        /////       Prompts the user with a <see cref='System.Windows.Forms.MessageBox'/>
        /////       when a file is about to be created. This method is
        /////       invoked when the CreatePrompt property is true and the specified file
        /////       does not exist. A return value of false prevents the dialog from
        /////       closing.
        /////    </para>
        ///// </devdoc>
        //private bool PromptFileCreate(string fileName) 
        //{
        //    return MessageBoxWithFocusRestore(DCSR.GetString(DCSR.FileDialogCreatePrompt, fileName),
        //            DialogCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        //}

        ///// <include file='doc\SaveFileDialog.uex' path='docs/doc[@for="SaveFileDialog.PromptFileOverwrite"]/*' />
        ///// <devdoc>
        /////    <para>
        /////       Prompts the user when a file is about to be overwritten. This method is
        /////       invoked when the "overwritePrompt" property is true and the specified
        /////       file already exists. A return value of false prevents the dialog from
        /////       closing.
        /////       
        /////    </para>
        ///// </devdoc>
        //private bool PromptFileOverwrite(string fileName) {
        //    return MessageBoxWithFocusRestore(DCSR.GetString(DCSR.FileDialogOverwritePrompt, fileName),
        //            DialogCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        //}

        //// If it's necessary to throw up a "This file exists, are you sure?" kind of
        //// MessageBox, here's where we do it.
        //// Return value is whether or not the user hit "okay".
        //internal override bool PromptUserIfAppropriate(string fileName) {
        //    if (!base.PromptUserIfAppropriate(fileName)) {
        //        return false;
        //    }

        //    //Note: When we are using the Vista dialog mode we get two prompts (one from us and one from the OS) if we do this
        //    if ((options & WinFormNativeMethods.OFN_OVERWRITEPROMPT) != 0 && FileExists(fileName) && !this.UseVistaDialogInternal) {
        //        if (!PromptFileOverwrite(fileName)) {
        //            return false;
        //        }
        //    }

        //    if ((options & WinFormNativeMethods.OFN_CREATEPROMPT) != 0 && !FileExists(fileName)) {
        //        if (!PromptFileCreate(fileName)) {
        //            return false;
        //        }
        //    }
            
        //    return true;
        //}

        /// <include file='doc\SaveFileDialog.uex' path='docs/doc[@for="SaveFileDialog.Reset"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Resets all dialog box options to their default
        ///       values.
        ///    </para>
        /// </devdoc>
        public override void Reset() {
            base.Reset();
            SetOption(WinFormNativeMethods.OFN_OVERWRITEPROMPT, true);
        }

        internal override void EnsureFileDialogPermission()
        {
            //Debug.WriteLineIf(IntSecurity.SecurityDemand.TraceVerbose, "FileDialogSaveFile Demanded in SaveFileDialog.RunFileDialog");
            //IntSecurity.FileDialogSaveFile.Demand();
        }

        /// <include file='doc\SaveFileDialog.uex' path='docs/doc[@for="SaveFileDialog.RunFileDialog"]/*' />
        /// <devdoc>
        /// </devdoc>
        /// <internalonly/>
        internal override bool RunFileDialog(WinFormNativeMethods.OPENFILENAME_I ofn) {
            //We have already done the demand in EnsureFileDialogPermission but it doesn't hurt to do it again
            //Debug.WriteLineIf(IntSecurity.SecurityDemand.TraceVerbose, "FileDialogSaveFile Demanded in SaveFileDialog.RunFileDialog");
            //IntSecurity.FileDialogSaveFile.Demand();

            bool result = WinFormUnsafeNativeMethods.GetSaveFileName(ofn);

            if (!result) {
                // Something may have gone wrong - check for error condition
                //
                int errorCode = WinFormSafeNativeMethods.CommDlgExtendedError();
                switch(errorCode) {
                    case WinFormNativeMethods.FNERR_INVALIDFILENAME:
                        throw new InvalidOperationException(DCSR.GetString(DCSR.FileDialogInvalidFileName, FileName));
                }
            }
            
            return result;
         }
        internal override string[] ProcessVistaFiles(FileDialogNative.IFileDialog dialog)
        {
            FileDialogNative.IFileSaveDialog saveDialog = (FileDialogNative.IFileSaveDialog)dialog;
            FileDialogNative.IShellItem item;
            dialog.GetResult(out item);
            return new string[] { GetFilePathFromShellItem(item) };
        }
        internal override FileDialogNative.IFileDialog CreateVistaDialog()
        { return new FileDialogNative.NativeFileSaveDialog(); }


    }
}
