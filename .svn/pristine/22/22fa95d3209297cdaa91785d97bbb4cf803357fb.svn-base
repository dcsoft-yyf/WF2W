//------------------------------------------------------------------------------
// <copyright file="OpenFileDialog.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>                                                                
//------------------------------------------------------------------------------

namespace System.Windows.Forms
{

    using System.Diagnostics;

    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Drawing;
    //using CodeAccessPermission = System.Security.CodeAccessPermission;
    using System.Security.Permissions;
    using System.IO;
    using System.ComponentModel;
    using Microsoft.Win32;
    using System.Runtime.Versioning;
    using System.Text.Json.Nodes;
    using System.Threading.Tasks;

    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = false )]
    [SRDescription(DCSR.DescriptionOpenFileDialog)]
    public sealed class OpenFileDialog : FileDialog
    {
        public OpenFileDialog()
        {

        }
        private DialogResult _DialogResult = DialogResult.Cancel;
        public JsonObject ToJsonObject()
        {
            JsonObject json = new JsonObject();
            json.Add("CheckFileExists", this.CheckFileExists);
            json.Add("CheckPathExists", this.CheckPathExists);
            json.Add("DefaultExt", this.DefaultExt);
            json.Add("DereferenceLinks", this.DereferenceLinks);
            json.Add("FileName", this.FileName);
            json.Add("Filter", this.Filter);
            json.Add("FilterIndex", this.FilterIndex);
            json.Add("Multiselect", this.Multiselect);
            json.Add("ReadOnlyChecked", this.ReadOnlyChecked);
            json.Add("ShowReadOnly", this.ShowReadOnly);
            json.Add("Title", this.Title);
            return json;
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        [
        DefaultValue(true),
        SRDescription(DCSR.OFDcheckFileExistsDescr)
        ]
        public override bool CheckFileExists
        {
            get
            {
                return base.CheckFileExists;
            }
            set
            {
                base.CheckFileExists = value;
            }
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        [
        SRCategory(DCSR.CatBehavior),
        DefaultValue(false),
        SRDescription(DCSR.OFDmultiSelectDescr)
        ]
        public bool Multiselect
        {
            get
            {
                return GetOption(WinFormNativeMethods.OFN_ALLOWMULTISELECT);
            }
            set
            {
                SetOption(WinFormNativeMethods.OFN_ALLOWMULTISELECT, value);
            }
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        [
        SRCategory(DCSR.CatBehavior),
        DefaultValue(false),
        SRDescription(DCSR.OFDreadOnlyCheckedDescr)
        ]
        public bool ReadOnlyChecked
        {
            get
            {
                return GetOption(WinFormNativeMethods.OFN_READONLY);
            }
            set
            {
                SetOption(WinFormNativeMethods.OFN_READONLY, value);
            }
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        [
        SRCategory(DCSR.CatBehavior),
        DefaultValue(false),
        SRDescription(DCSR.OFDshowReadOnlyDescr)
        ]
        public bool ShowReadOnly
        {
            get
            {
                return !GetOption(WinFormNativeMethods.OFN_HIDEREADONLY);
            }
            set
            {
                SetOption(WinFormNativeMethods.OFN_HIDEREADONLY, !value);
            }
        }
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public async Task<DialogResult> ShowDialog()
        {
            return await ShowDialog(null);
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public async Task<DialogResult> ShowDialog(IWin32Window owner)
        {
            this._FileNames = null;
            await DCWin32API.EnsureLoadJSModule(typeof( System.Windows.Forms.OpenFileDialog));
            var json = this.ToJsonObject();
            var strResult = await DCWin32API.JSRuntime.InvokeAsync<string>("__DCWin32API.ShowOpenFileDialog", json);
            if (strResult != null && strResult.Length > 0 )
            {
                this._FileNames = strResult.Split('|');
                this._DialogResult = DialogResult.OK;
            }
            else
            {
                this._DialogResult = DialogResult.Cancel;
            }
            return this._DialogResult;
        }
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public async Task<Stream> OpenFile()
        {
            if (this._DialogResult == DialogResult.OK)
            {
                var strFileName = this.FileName;
                if (strFileName != null && strFileName.Length > 0)
                {
                    var bs = await DCWin32API.JSRuntime.InvokeAsync<byte[]>("__DCWin32API.ReadContentFromFileReader", strFileName);
                    if (bs != null)
                    {
                        return new MemoryStream(bs);
                    }
                }
            }
            return null;

            ////Debug.WriteLineIf(IntSecurity.SecurityDemand.TraceVerbose, "FileDialogOpenFile Demanded");
            ////IntSecurity.FileDialogOpenFile.Demand();

            //string filename = FileNamesInternal[0];

            //if (filename == null || (filename.Length == 0))
            //    throw new ArgumentNullException("FileName");

            //Stream s = null;

            //// SECREVIEW : We demanded the FileDialog permission above, so it is safe
            ////           : to assert this here. Since the user picked the file, it
            ////           : is OK to give them readonly access to the stream.
            ////
            ////new FileIOPermission(FileIOPermissionAccess.Read, IntSecurity.UnsafeGetFullPath(filename)).Assert();
            //try
            //{
            //    s = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read);
            //}
            //finally
            //{
            //    //CodeAccessPermission.RevertAssert();
            //}
            //return s;
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public override void Reset()
        {
            base.Reset();
            SetOption(WinFormNativeMethods.OFN_FILEMUSTEXIST, true);
        }

        internal override void EnsureFileDialogPermission()
        {
            //Debug.WriteLineIf(IntSecurity.SecurityDemand.TraceVerbose, "FileDialogOpenFile Demanded in OpenFileDialog.RunFileDialog");
            //IntSecurity.FileDialogOpenFile.Demand();
        }

        /// <include file='doc\OpenFileDialog.uex' path='docs/doc[@for="OpenFileDialog.RunFileDialog"]/*' />
        /// <devdoc>
        ///     Displays a file open dialog.
        /// </devdoc>
        /// <internalonly/>
        internal override bool RunFileDialog(WinFormNativeMethods.OPENFILENAME_I ofn)
        {
            //We have already done the demand in EnsureFileDialogPermission but it doesn't hurt to do it again
            //Debug.WriteLineIf(IntSecurity.SecurityDemand.TraceVerbose, "FileDialogOpenFile Demanded in OpenFileDialog.RunFileDialog");
            //IntSecurity.FileDialogOpenFile.Demand();

            bool result = WinFormUnsafeNativeMethods.GetOpenFileName(ofn);
            if (!result)
            {
                // Something may have gone wrong - check for error condition
                //
                int errorCode = WinFormSafeNativeMethods.CommDlgExtendedError();
                switch (errorCode)
                {
                    case WinFormNativeMethods.FNERR_INVALIDFILENAME:
                        throw new InvalidOperationException(DCSR.GetString(DCSR.FileDialogInvalidFileName, FileName));

                    case WinFormNativeMethods.FNERR_SUBCLASSFAILURE:
                        throw new InvalidOperationException(DCSR.GetString(DCSR.FileDialogSubLassFailure));

                    case WinFormNativeMethods.FNERR_BUFFERTOOSMALL:
                        throw new InvalidOperationException(DCSR.GetString(DCSR.FileDialogBufferTooSmall));
                }
            }
            return result;
        }

        internal override string[] ProcessVistaFiles(FileDialogNative.IFileDialog dialog)
        {
            FileDialogNative.IFileOpenDialog openDialog = (FileDialogNative.IFileOpenDialog)dialog;
            if (Multiselect)
            {
                FileDialogNative.IShellItemArray results;
                openDialog.GetResults(out results);
                uint count;
                results.GetCount(out count);
                string[] files = new string[count];
                for (uint i = 0; i < count; ++i)
                { 
                    FileDialogNative.IShellItem item;
                    results.GetItemAt(i, out item);
                    files[unchecked((int)i)] = GetFilePathFromShellItem(item);
                }
                return files;
            }
            else
            { 
                FileDialogNative.IShellItem item;
                openDialog.GetResult(out item);
                return new string[] { GetFilePathFromShellItem(item) };
            }
        }

        internal override FileDialogNative.IFileDialog CreateVistaDialog()
        {
            return new FileDialogNative.NativeFileOpenDialog();
        }

        [
            Browsable(false),
            DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
            SuppressMessage("Microsoft.Security", "CA2106:SecureAsserts")
        ]
        public string SafeFileName
        {
            get
            {
                //new FileIOPermission(PermissionState.Unrestricted).Assert();
                string fullPath = FileName;
                //CodeAccessPermission.RevertAssert();
                if (string.IsNullOrEmpty(fullPath))
                {
                    return "";
                }

                string safePath = RemoveSensitivePathInformation(fullPath);
                return safePath;
            }
        }

        private static string RemoveSensitivePathInformation(string fullPath)
        {
            return System.IO.Path.GetFileName(fullPath);
        }

        [
            Browsable(false),
            DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
            SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays"),
            SuppressMessage("Microsoft.Security", "CA2106:SecureAsserts")
        ]
        public string[] SafeFileNames
        {
            get
            {
                //new FileIOPermission(PermissionState.Unrestricted).Assert();
                string[] fullPaths = FileNames;
                //CodeAccessPermission.RevertAssert();
                if (null == fullPaths || 0 == fullPaths.Length)
                { return new string[0]; }
                string[] safePaths = new string[fullPaths.Length];
                for (int i = 0; i < safePaths.Length; ++i)
                {
                    safePaths[i] = RemoveSensitivePathInformation(fullPaths[i]);
                }
                return safePaths;
            }
        }

        internal override bool SettingsSupportVistaDialog
        { 
            get
            {
                return base.SettingsSupportVistaDialog && !this.ShowReadOnly;
            }
        }
    }
}
