//------------------------------------------------------------------------------
// <copyright file="FileDialog.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>                                                                
//------------------------------------------------------------------------------

namespace System.Windows.Forms {
    using System.Text;
    using System.Threading;
    using System.Runtime.Remoting;
    using System.Runtime.InteropServices;

    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;

    using System;
    using System.Security.Permissions;
    using System.Drawing;
    using System.ComponentModel;
    using System.Windows.Forms;
    using System.IO;
    using ArrayList = System.Collections.ArrayList;

    using Encoding = System.Text.Encoding;
    using Microsoft.Win32;
    using System.Security;
    using System.Runtime.Versioning;

    using CharBuffer = System.Windows.Forms.WinFormUnsafeNativeMethods.CharBuffer;

    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = false )]
    [
    DefaultEvent("FileOk"),
    DefaultProperty("FileName")
    ]
    public abstract partial class FileDialog : CommonDialog {

        private const int FILEBUFSIZE = 8192;

        /// <include file='doc\FileDialog.uex' path='docs/doc[@for="FileDialog.EventFileOk"]/*' />
        /// <devdoc>
        ///    <para>[To be supplied.]</para>
        /// </devdoc>
        /// <internalonly/>
        protected static readonly object EventFileOk = new object();

        internal const int OPTION_ADDEXTENSION = unchecked(unchecked((int)0x80000000));

        internal int options;

        private string title;
        private string initialDir;
        private string defaultExt;
        private string[] fileNames;
        private bool securityCheckFileNames;
        private string filter;
        private int filterIndex;
        private bool supportMultiDottedExtensions;
        private bool ignoreSecondFileOkNotification;  // Used for VS Whidbey 95342
        private int okNotificationCount;              // Same
        private CharBuffer charBuffer;
        private IntPtr dialogHWnd;

        /// <include file='doc\FileDialog.uex' path='docs/doc[@for="FileDialog.FileDialog"]/*' />
        /// <devdoc>
        ///    <para>
        ///       In an inherited class,
        ///       initializes a new instance of the <see cref='System.Windows.Forms.FileDialog'/>
        ///       class.
        ///    </para>
        /// </devdoc>
        [
            SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")  // If the constructor does not call Reset
                                                                                                    // it would be a breaking change.
        ]
        internal FileDialog() {
            Reset();
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        [
        SRCategory(DCSR.CatBehavior), 
        DefaultValue(true),
        SRDescription(DCSR.FDaddExtensionDescr)
        ]
        public bool AddExtension {
            get {
                return GetOption(OPTION_ADDEXTENSION);
            }

            set {
                //Debug.WriteLineIf(IntSecurity.SecurityDemand.TraceVerbose, "FileDialogCustomization Demanded");
                //IntSecurity.FileDialogCustomization.Demand();
                SetOption(OPTION_ADDEXTENSION, value);
            }
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        [
        SRCategory(DCSR.CatBehavior), 
        DefaultValue(false),
        SRDescription(DCSR.FDcheckFileExistsDescr)
        ]
        public virtual bool CheckFileExists {
            get {
                return GetOption(WinFormNativeMethods.OFN_FILEMUSTEXIST);
            }

            set {
                //Debug.WriteLineIf(IntSecurity.SecurityDemand.TraceVerbose, "FileDialogCustomization Demanded");
                //IntSecurity.FileDialogCustomization.Demand();
                SetOption(WinFormNativeMethods.OFN_FILEMUSTEXIST, value);
            }
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        [
        SRCategory(DCSR.CatBehavior), 
        DefaultValue(true),
        SRDescription(DCSR.FDcheckPathExistsDescr)
        ]
        public bool CheckPathExists {
            get {
                return GetOption(WinFormNativeMethods.OFN_PATHMUSTEXIST);
            }

            set {
                //Debug.WriteLineIf(IntSecurity.SecurityDemand.TraceVerbose, "FileDialogCustomization Demanded");
                //IntSecurity.FileDialogCustomization.Demand();
                SetOption(WinFormNativeMethods.OFN_PATHMUSTEXIST, value);
            }
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        [
        SRCategory(DCSR.CatBehavior), 
        DefaultValue(""),
        SRDescription(DCSR.FDdefaultExtDescr)
        ]
        public string DefaultExt {
            get {
                return defaultExt == null? "": defaultExt;
            }

            set {
                if (value != null) {
                    if (value.StartsWith("."))
                        value = value.Substring(1);
                    else if (value.Length == 0)
                        value = null;
                }
                defaultExt = value;
            }
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        [
        SRCategory(DCSR.CatBehavior), 
        DefaultValue(true),
        SRDescription(DCSR.FDdereferenceLinksDescr)
        ]
        public bool DereferenceLinks {
            get { 
                return !GetOption(WinFormNativeMethods.OFN_NODEREFERENCELINKS);
            }
            set { 
               // Debug.WriteLineIf(IntSecurity.SecurityDemand.TraceVerbose, "FileDialogCustomization Demanded");
                //IntSecurity.FileDialogCustomization.Demand();
                SetOption(WinFormNativeMethods.OFN_NODEREFERENCELINKS, !value);
            }
        }

        internal string DialogCaption {
            get {
                return DCWin32API.GetWindowText(dialogHWnd.ToInt32());
                //int textLen = WinFormSafeNativeMethods.GetWindowTextLength(new HandleRef(this, dialogHWnd));
                //StringBuilder sb = new StringBuilder(textLen+1);
                //WinFormUnsafeNativeMethods.GetWindowText(new HandleRef(this, dialogHWnd), sb, sb.Capacity);
                //return sb.ToString();
            }
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        [
        SRCategory(DCSR.CatData),
        DefaultValue(""),
        SRDescription(DCSR.FDfileNameDescr)
        ]
        public string FileName
        {
            get
            {
                if (this._FileNames != null && this._FileNames.Length > 0)
                {
                    return this._FileNames[0];
                }
                else
                {
                    return string.Empty;
                }

            }
            set
            {
                if (value == null || value.Length == 0)
                {
                    this._FileNames = null;
                }
                else
                {
                    this._FileNames = new string[] { value };
                }
            }
        }

        protected string[] _FileNames = null;
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        [
        Browsable(false),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
        SRDescription(DCSR.FDFileNamesDescr)
        ]
        public string[] FileNames {
            get{
                return this._FileNames;
            }
        }

        internal string[] FileNamesInternal {
            get {

                if (fileNames == null) {
                    return new string[0];
                }
                else {
                    return(string[])fileNames.Clone();
                }
            }
        }


        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        [
        SRCategory(DCSR.CatBehavior), 
        DefaultValue(""),
        Localizable(true),
        SRDescription(DCSR.FDfilterDescr)
        ]
        public string Filter {
            get {
                return filter == null? "": filter;
            }

            set {
                if (value != filter) {
                    if (value != null && value.Length > 0) {
                        string[] formats = value.Split('|');
                        if (formats == null || formats.Length % 2 != 0) {
                            throw new ArgumentException(DCSR.GetString(DCSR.FileDialogInvalidFilter));
                        }
                    }
                    else {
                        value = null;
                    }
                    filter = value;
                }
            }
        }

        /// <include file='doc\FileDialog.uex' path='docs/doc[@for="FileDialog.FilterExtensions"]/*' />
        /// <devdoc>
        ///     Extracts the file extensions specified by the current file filter into
        ///     an array of strings.  None of the extensions contain .'s, and the 
        ///     default extension is first.
        /// </devdoc>
        private string[] FilterExtensions {
            get {
                string filter = this.filter;
                ArrayList extensions = new ArrayList();
                
                // First extension is the default one.  It's a little strange if DefaultExt
                // is not in the filters list, but I guess it's legal.
                if (defaultExt != null) 
                    extensions.Add(defaultExt);

                if (filter != null) {
                    string[] tokens = filter.Split('|');
                    
                    if ((filterIndex * 2) - 1 >= tokens.Length) {
                        throw new InvalidOperationException(DCSR.GetString(DCSR.FileDialogInvalidFilterIndex));
                    }
                    
                    if (filterIndex > 0) {
                        string[] exts = tokens[(filterIndex * 2) - 1].Split(';');
                        foreach (string ext in exts) {
                            int i = this.supportMultiDottedExtensions ? ext.IndexOf('.') : ext.LastIndexOf('.');
                            if (i >= 0) {
                                extensions.Add(ext.Substring(i + 1, ext.Length - (i + 1)));
                            }
                        }
                    }
                }
                string[] temp = new string[extensions.Count];
                extensions.CopyTo(temp, 0);
                return temp;
            }
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        [
        SRCategory(DCSR.CatBehavior), 
        DefaultValue(1),
        SRDescription(DCSR.FDfilterIndexDescr)
        ]
        public int FilterIndex {
            get {
                return filterIndex;
            }

            set {
                filterIndex = value;
            }
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        [
        SRCategory(DCSR.CatData), 
        DefaultValue(""),
        SRDescription(DCSR.FDinitialDirDescr)
        ]
        public string InitialDirectory {
            get {
                return initialDir == null? "": initialDir;
            }
            set {
                //Debug.WriteLineIf(IntSecurity.SecurityDemand.TraceVerbose, "FileDialogCustomization Demanded");
                //IntSecurity.FileDialogCustomization.Demand();

                initialDir = value;
            }
        }

        /// <include file='doc\FileDialog.uex' path='docs/doc[@for="FileDialog.Instance"]/*' />
        /// <internalonly/>
        /// <devdoc>
        ///    <para>
        ///       Gets the Win32 instance handle for the application.
        ///    </para>
        /// </devdoc>
        /* SECURITYUNDONE : should require EventQueue permission */
        protected virtual IntPtr Instance {
            //[
            //    SecurityPermission(SecurityAction.LinkDemand, Flags=SecurityPermissionFlag.UnmanagedCode),
            //    SecurityPermission(SecurityAction.InheritanceDemand, Flags=SecurityPermissionFlag.UnmanagedCode)
            //]
            [ResourceExposure(ResourceScope.Process)]
            [ResourceConsumption(ResourceScope.Process)]
            get { return WinFormUnsafeNativeMethods.GetModuleHandle(null); }
        }

        /// <include file='doc\FileDialog.uex' path='docs/doc[@for="FileDialog.Options"]/*' />
        /// <internalonly/>
        /// <devdoc>
        ///    <para>
        ///       Gets the Win32 common Open File Dialog OFN_* option flags.
        ///    </para>
        /// </devdoc>
        protected int Options {
            get {
                return options & (WinFormNativeMethods.OFN_READONLY | WinFormNativeMethods.OFN_HIDEREADONLY |
                                  WinFormNativeMethods.OFN_NOCHANGEDIR | WinFormNativeMethods.OFN_SHOWHELP | WinFormNativeMethods.OFN_NOVALIDATE |
                                  WinFormNativeMethods.OFN_ALLOWMULTISELECT | WinFormNativeMethods.OFN_PATHMUSTEXIST |
                                  WinFormNativeMethods.OFN_NODEREFERENCELINKS);
            }
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        [
        SRCategory(DCSR.CatBehavior), 
        DefaultValue(false),
        SRDescription(DCSR.FDrestoreDirectoryDescr)
        ]
        public bool RestoreDirectory {
            get {
                return GetOption(WinFormNativeMethods.OFN_NOCHANGEDIR);
            }
            set {
                //Debug.WriteLineIf(IntSecurity.SecurityDemand.TraceVerbose, "FileDialogCustomization Demanded");
                //IntSecurity.FileDialogCustomization.Demand();

                SetOption(WinFormNativeMethods.OFN_NOCHANGEDIR, value);
            }
        }


        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        [
        SRCategory(DCSR.CatBehavior), 
        DefaultValue(false),
        SRDescription(DCSR.FDshowHelpDescr)
        ]
        public bool ShowHelp {
            get {
                return GetOption(WinFormNativeMethods.OFN_SHOWHELP);
            }
            set {
                SetOption(WinFormNativeMethods.OFN_SHOWHELP, value);
            }
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        [
        SRCategory(DCSR.CatBehavior), 
        DefaultValue(false),
        SRDescription(DCSR.FDsupportMultiDottedExtensionsDescr)
        ]
        public bool SupportMultiDottedExtensions
        {
            get
            {
                return this.supportMultiDottedExtensions;
            }
            set
            {
                this.supportMultiDottedExtensions = value;
            }
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        [
        SRCategory(DCSR.CatAppearance), 
        DefaultValue(""),
        Localizable(true),
        SRDescription(DCSR.FDtitleDescr)
        ]
        public string Title {
            get {
                return title == null? "": title;
            }
            set {
                //Debug.WriteLineIf(IntSecurity.SecurityDemand.TraceVerbose, "FileDialogCustomization Demanded");
                //IntSecurity.FileDialogCustomization.Demand();

                title = value;
            }
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        [
        SRCategory(DCSR.CatBehavior), 
        DefaultValue(true),
        SRDescription(DCSR.FDvalidateNamesDescr)
        ]
        public bool ValidateNames {
            get {
                return !GetOption(WinFormNativeMethods.OFN_NOVALIDATE);
            }
            set {
                //Debug.WriteLineIf(IntSecurity.SecurityDemand.TraceVerbose, "FileDialogCustomization Demanded");
                //IntSecurity.FileDialogCustomization.Demand();

                SetOption(WinFormNativeMethods.OFN_NOVALIDATE, !value);
            }
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        [SRDescription(DCSR.FDfileOkDescr)]
        public event CancelEventHandler FileOk {
            add {
                Events.AddHandler(EventFileOk, value);
            }
            remove {
                Events.RemoveHandler(EventFileOk, value);
            }
        }

        /// <include file='doc\FileDialog.uex' path='docs/doc[@for="FileDialog.DoFileOk"]/*' />
        /// <devdoc>
        ///     Processes the CDN_FILEOK notification.
        /// </devdoc>
        private bool DoFileOk(IntPtr lpOFN) {
            WinFormNativeMethods.OPENFILENAME_I ofn = (WinFormNativeMethods.OPENFILENAME_I)WinFormUnsafeNativeMethods.PtrToStructure(lpOFN, typeof(WinFormNativeMethods.OPENFILENAME_I));
            int saveOptions = options;
            int saveFilterIndex = filterIndex;
            string[] saveFileNames = fileNames;
            bool saveSecurityCheckFileNames = securityCheckFileNames;
            bool ok = false;
            try {
                options = options & ~WinFormNativeMethods.OFN_READONLY |
                          ofn.Flags & WinFormNativeMethods.OFN_READONLY;
                filterIndex = ofn.nFilterIndex;
                charBuffer.PutCoTaskMem(ofn.lpstrFile);

                // We are filling in the file names list with secure
                // data.  Any access to this list now will require
                // a security demand.  We set this bit before actually
                // setting the names; otherwise a thread ---- could
                // expose them.
                securityCheckFileNames = true;
                Thread.MemoryBarrier();

                if ((options & WinFormNativeMethods.OFN_ALLOWMULTISELECT) == 0) {
                    fileNames = new string[] {charBuffer.GetString()};
                }
                else {
                    fileNames = GetMultiselectFiles(charBuffer);
                }

                if (ProcessFileNames()) {
                    CancelEventArgs ceevent = new CancelEventArgs();
                    if (NativeWindow.WndProcShouldBeDebuggable) {
                        OnFileOk(ceevent);
                        ok = !ceevent.Cancel;
                    }
                    else {
                        try
                        {
                            OnFileOk(ceevent);
                            ok = !ceevent.Cancel;
                        }
                        catch (Exception e)
                        {
                            Application.OnThreadException(e);
                        }
                    }
                }
            }
            finally {
                if (!ok) {
                    securityCheckFileNames = saveSecurityCheckFileNames;
                    Thread.MemoryBarrier();
                    fileNames = saveFileNames;

                    options = saveOptions;
                    filterIndex = saveFilterIndex;
                }
            }
            return ok;
        }

        /// SECREVIEW: ReviewImperativeSecurity
        ///   vulnerability to watch out for: A method uses imperative security and might be constructing the permission using state information or return values that can change while the demand is active.
        ///   reason for exclude: filename is a local variable and not subject to race conditions.
        [SuppressMessage("Microsoft.Security", "CA2103:ReviewImperativeSecurity")]
        [ResourceExposure(ResourceScope.Machine)]
        [ResourceConsumption(ResourceScope.Machine)]
        internal static bool FileExists(string fileName)
        {
            bool fileExists = false;
            try {
                // SECREVIEW : We must Assert just to check if the file exists. Since
                //           : we are doing this as part of the FileDialog, this is OK.
                //new FileIOPermission(FileIOPermissionAccess.Read, IntSecurity.UnsafeGetFullPath(fileName)).Assert();
                try {
                    fileExists = File.Exists(fileName);
                }
                finally {
                 //   CodeAccessPermission.RevertAssert();
                }
            }
            catch (System.IO.PathTooLongException) {
            }
            return fileExists;
        }

        /// <include file='doc\FileDialog.uex' path='docs/doc[@for="FileDialog.GetMultiselectFiles"]/*' />
        /// <devdoc>
        ///     Extracts the filename(s) returned by the file dialog.
        /// </devdoc>
        private string[] GetMultiselectFiles(CharBuffer charBuffer) {
            string directory = charBuffer.GetString();
            string fileName = charBuffer.GetString();
            if (fileName.Length == 0) return new string[] {
                    directory
                };
            if (directory[directory.Length - 1] != '\\') {
                directory = directory + "\\";
            }
            ArrayList names = new ArrayList();
            do {
                if (fileName[0] != '\\' && (fileName.Length <= 3 ||
                                            fileName[1] != ':' || fileName[2] != '\\')) {
                    fileName = directory + fileName;
                }
                names.Add(fileName);
                fileName = charBuffer.GetString();
            } while (fileName.Length > 0);
            string[] temp = new string[names.Count];
            names.CopyTo(temp, 0);
            return temp;
        }

        /// <include file='doc\FileDialog.uex' path='docs/doc[@for="FileDialog.GetOption"]/*' />
        /// <devdoc>
        ///     Returns the state of the given option flag.
        /// </devdoc>
        /// <internalonly/>

        internal bool GetOption(int option) {
            return(options & option) != 0;
        }

        /// <include file='doc\FileDialog.uex' path='docs/doc[@for="FileDialog.HookProc"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Defines the common dialog box hook procedure that is overridden to add
        ///       specific functionality to the file dialog box.
        ///    </para>
        /// </devdoc>
        //[SecurityPermission(SecurityAction.LinkDemand, Flags=SecurityPermissionFlag.UnmanagedCode)]
        protected override IntPtr HookProc(IntPtr hWnd, int msg, IntPtr wparam, IntPtr lparam) {
            if (msg == WinFormNativeMethods.WM_NOTIFY) {
                dialogHWnd = WinFormUnsafeNativeMethods.GetParent(new HandleRef(null, hWnd));
                try {
                    WinFormUnsafeNativeMethods.OFNOTIFY notify = (WinFormUnsafeNativeMethods.OFNOTIFY)WinFormUnsafeNativeMethods.PtrToStructure(lparam, typeof(WinFormUnsafeNativeMethods.OFNOTIFY));

                    switch (notify.hdr_code) {
                        case -601: /* CDN_INITDONE */
                            MoveToScreenCenter(dialogHWnd);
                            break;
                        case -602: /* CDN_SELCHANGE */
                            WinFormNativeMethods.OPENFILENAME_I ofn = (WinFormNativeMethods.OPENFILENAME_I)WinFormUnsafeNativeMethods.PtrToStructure(notify.lpOFN, typeof(WinFormNativeMethods.OPENFILENAME_I));
                            // Get the buffer size required to store the selected file names.
                            int sizeNeeded = (int)WinFormUnsafeNativeMethods.SendMessage(new HandleRef(this, dialogHWnd), 1124 /*CDM_GETSPEC*/, System.IntPtr.Zero, System.IntPtr.Zero);
                            if (sizeNeeded > ofn.nMaxFile) {
                                // A bigger buffer is required.
                                try {
                                    int newBufferSize = sizeNeeded + (FILEBUFSIZE / 4);
                                    // Allocate new buffer
                                    CharBuffer charBufferTmp = CharBuffer.CreateBuffer(newBufferSize);
                                    IntPtr newBuffer = charBufferTmp.AllocCoTaskMem();
                                    // Free old buffer
                                    Marshal.FreeCoTaskMem(ofn.lpstrFile);
                                    // Substitute buffer
                                    ofn.lpstrFile = newBuffer;
                                    ofn.nMaxFile = newBufferSize;
                                    this.charBuffer = charBufferTmp;
                                    Marshal.StructureToPtr(ofn, notify.lpOFN, true);
                                    Marshal.StructureToPtr(notify, lparam, true);
                                }
                                catch {
                                    // intentionaly not throwing here.
                                }
                            }
                            this.ignoreSecondFileOkNotification = false;
                            break;
                        case -604: /* CDN_SHAREVIOLATION */
                            // See VS Whidbey 95342. When the selected file is locked for writing,
                            // we get this notification followed by *two* CDN_FILEOK notifications.                            
                            this.ignoreSecondFileOkNotification = true;  // We want to ignore the second CDN_FILEOK
                            this.okNotificationCount = 0;                // to avoid a second prompt by PromptFileOverwrite.
                            break;
                        case -606: /* CDN_FILEOK */
                            if (this.ignoreSecondFileOkNotification)
                            {
                                // We got a CDN_SHAREVIOLATION notification and want to ignore the second CDN_FILEOK notification
                                if (this.okNotificationCount == 0)
                                {
                                    this.okNotificationCount = 1;   // This one is the first and is all right.
                                }
                                else
                                {
                                    // This is the second CDN_FILEOK, so we want to ignore it.
                                    this.ignoreSecondFileOkNotification = false;
                                    WinFormUnsafeNativeMethods.SetWindowLong(new HandleRef(null, hWnd), 0, new HandleRef(null, WinFormNativeMethods.InvalidIntPtr));
                                    return WinFormNativeMethods.InvalidIntPtr;
                                }
                            }
                            if (!DoFileOk(notify.lpOFN)) {
                                WinFormUnsafeNativeMethods.SetWindowLong(new HandleRef(null, hWnd), 0, new HandleRef(null, WinFormNativeMethods.InvalidIntPtr));
                                return WinFormNativeMethods.InvalidIntPtr;
                            }
                            break;
                    }
                }
                catch {
                    if (dialogHWnd != IntPtr.Zero) {
                        WinFormUnsafeNativeMethods.EndDialog(new HandleRef(this, dialogHWnd), IntPtr.Zero);
                    }
                    throw;
                }
            }
            return IntPtr.Zero;
        }

        /// <include file='doc\FileDialog.uex' path='docs/doc[@for="FileDialog.MakeFilterString"]/*' />
        /// <devdoc>
        ///     Converts the given filter string to the format required in an OPENFILENAME_I
        ///     structure.
        /// </devdoc>
        private static string MakeFilterString(string s, bool dereferenceLinks) {
            if (s == null || s.Length == 0)
            {
                // Workaround for Whidbey bug #5165
                // Apply the workaround only when DereferenceLinks is true and OS is at least WinXP.
                if (dereferenceLinks && System.Environment.OSVersion.Version.Major >= 5)
                {
                    s = " |*.*";
                }
                else if (s == null)
                {
                    return null;
                }
            }
            int length = s.Length;
            char[] filter = new char[length + 2];
            s.CopyTo(0, filter, 0, length);
            for (int i = 0; i < length; i++) {
                if (filter[i] == '|') filter[i] = (char)0;
            }
            filter[length + 1] = (char)0;
            return new string(filter);
        }

        /// <include file='doc\FileDialog.uex' path='docs/doc[@for="FileDialog.OnFileOk"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Raises the <see cref='System.Windows.Forms.FileDialog.FileOk'/> event.
        ///    </para>
        /// </devdoc>
        protected void OnFileOk(CancelEventArgs e) {
            CancelEventHandler handler = (CancelEventHandler)Events[EventFileOk];
            if (handler != null) handler(this, e);
        }

        /// <include file='doc\FileDialog.uex' path='docs/doc[@for="FileDialog.ProcessFileNames"]/*' />
        /// <devdoc>
        ///     Processes the filenames entered in the dialog according to the settings
        ///     of the "addExtension", "checkFileExists", "createPrompt", and
        ///     "overwritePrompt" properties.
        /// </devdoc>
        private bool ProcessFileNames() {
            if ((options & WinFormNativeMethods.OFN_NOVALIDATE) == 0) {
                string[] extensions = FilterExtensions;
                for (int i = 0; i < fileNames.Length; i++) {
                    string fileName = fileNames[i];
                    if ((options & OPTION_ADDEXTENSION) != 0 && !Path.HasExtension(fileName)) {
                        bool fileMustExist = (options & WinFormNativeMethods.OFN_FILEMUSTEXIST) != 0;

                        for (int j = 0; j < extensions.Length; j++) {
                            string currentExtension = Path.GetExtension(fileName);
                            
                            Debug.Assert(!extensions[j].StartsWith("."), 
                                         "FileDialog.FilterExtensions should not return things starting with '.'");
                            Debug.Assert(currentExtension.Length == 0 || currentExtension.StartsWith("."), 
                                         "File.GetExtension should return something that starts with '.'");
                            
                            string s = fileName.Substring(0, fileName.Length - currentExtension.Length);

                            // we don't want to append the extension if it contains wild cards
                            if (extensions[j].IndexOfAny(new char[] { '*', '?' }) == -1) {
                                s += "." + extensions[j];
                            }

                            if (!fileMustExist || FileExists(s)) {
                                fileName = s;
                                break;
                            }
                        }
                        fileNames[i] = fileName;
                    }
                    if (!PromptUserIfAppropriate(fileName))
                        return false;
                }
            }
            return true;
        }

        /// <include file='doc\FileDialog.uex' path='docs/doc[@for="FileDialog.MessageBoxWithFocusRestore"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Prompts the user with a <see cref='System.Windows.Forms.MessageBox'/>
        ///       with the given parameters. It also ensures that
        ///       the focus is set back on the window that had
        ///       the focus to begin with (before we displayed
        ///       the MessageBox).
        ///    </para>
        /// </devdoc>
        internal bool MessageBoxWithFocusRestore(string message, string caption,
                MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            bool ret;
            IntPtr focusHandle = WinFormUnsafeNativeMethods.GetFocus();           
            try {
                ret = RTLAwareMessageBox.Show(null, message, caption, buttons, icon,
                        MessageBoxDefaultButton.Button1, 0) == DialogResult.Yes;
            }
            finally {
                WinFormUnsafeNativeMethods.SetFocus(new HandleRef(null, focusHandle));
            }
            return ret;
        }

        /// <include file='doc\FileDialog.uex' path='docs/doc[@for="FileDialog.PromptFileNotFound"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Prompts the user with a <see cref='System.Windows.Forms.MessageBox'/>
        ///       when a file
        ///       does not exist.
        ///    </para>
        /// </devdoc>
        private void PromptFileNotFound(string fileName) {
            MessageBoxWithFocusRestore(DCSR.GetString(DCSR.FileDialogFileNotFound, fileName), DialogCaption,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        // If it's necessary to throw up a "This file exists, are you sure?" kind of
        // MessageBox, here's where we do it
        // Return value is whether or not the user hit "okay".
        internal virtual bool PromptUserIfAppropriate(string fileName) {
            if ((options & WinFormNativeMethods.OFN_FILEMUSTEXIST) != 0) {
                if (!FileExists(fileName)) {
                    PromptFileNotFound(fileName);
                    return false;
                }
            }
            return true;
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public override void Reset() {
            options = WinFormNativeMethods.OFN_HIDEREADONLY | WinFormNativeMethods.OFN_PATHMUSTEXIST |
                      OPTION_ADDEXTENSION;
            title = null;
            initialDir = null;
            defaultExt = null;
            fileNames = null;
            filter = null;
            filterIndex = 1;
            supportMultiDottedExtensions = false;
            this._customPlaces.Clear();
        }

        /// <include file='doc\FileDialog.uex' path='docs/doc[@for="FileDialog.RunDialog"]/*' />
        /// <devdoc>
        ///    Implements running of a file dialog.
        /// </devdoc>
        /// <internalonly/>
        protected override bool RunDialog(IntPtr hWndOwner) {
            // See VSWhidbey bug 107000. Shell APIs do not support multisthreaded apartment model.
            if (Control.CheckForIllegalCrossThreadCalls && Application.OleRequired() != System.Threading.ApartmentState.STA) {
                throw new System.Threading.ThreadStateException(DCSR.GetString(DCSR.DebuggingExceptionOnly, DCSR.GetString(DCSR.ThreadMustBeSTA)));
            }
            EnsureFileDialogPermission();
            if (this.UseVistaDialogInternal)
            {
                return RunDialogVista(hWndOwner);
            }
            else
            {
                return RunDialogOld(hWndOwner);
            }
        }

        internal abstract void EnsureFileDialogPermission();

        private bool RunDialogOld(IntPtr hWndOwner)
        {
            WinFormNativeMethods.WndProc hookProcPtr = new WinFormNativeMethods.WndProc(this.HookProc);
            WinFormNativeMethods.OPENFILENAME_I ofn = new WinFormNativeMethods.OPENFILENAME_I();
            try {
                charBuffer = CharBuffer.CreateBuffer(FILEBUFSIZE);
                if (fileNames != null) {
                    charBuffer.PutString(fileNames[0]);
                }
                ofn.lStructSize = Marshal.SizeOf(typeof(WinFormNativeMethods.OPENFILENAME_I));
                // Degrade to the older style dialog if we're not on Win2K.
                // We do this by setting the struct size to a different value
                //
                if (Environment.OSVersion.Platform != System.PlatformID.Win32NT ||
                    Environment.OSVersion.Version.Major < 5) {
                    ofn.lStructSize = 0x4C;
                }
                ofn.hwndOwner = hWndOwner;
                ofn.hInstance = Instance;
                ofn.lpstrFilter = MakeFilterString(filter, this.DereferenceLinks);
                ofn.nFilterIndex = filterIndex;
                ofn.lpstrFile = charBuffer.AllocCoTaskMem();
                ofn.nMaxFile = FILEBUFSIZE;
                ofn.lpstrInitialDir = initialDir;
                ofn.lpstrTitle = title;
                ofn.Flags = Options | (WinFormNativeMethods.OFN_EXPLORER | WinFormNativeMethods.OFN_ENABLEHOOK | WinFormNativeMethods.OFN_ENABLESIZING);
                ofn.lpfnHook = hookProcPtr;
                ofn.FlagsEx = WinFormNativeMethods.OFN_USESHELLITEM;
                if (defaultExt != null && AddExtension) {
                    ofn.lpstrDefExt = defaultExt;
                }
                //Security checks happen here
                return RunFileDialog(ofn);
            }
            finally {
                charBuffer = null;
                if (ofn.lpstrFile != IntPtr.Zero) {
                    Marshal.FreeCoTaskMem(ofn.lpstrFile);
                }
            }
        }

        /// <include file='doc\FileDialog.uex' path='docs/doc[@for="FileDialog.RunFileDialog"]/*' />
        /// <devdoc>
        ///     Implements the actual call to GetOPENFILENAME_I or GetSaveFileName.
        /// </devdoc>
        /// <internalonly/>
        internal abstract bool RunFileDialog(WinFormNativeMethods.OPENFILENAME_I ofn);

        /// <include file='doc\FileDialog.uex' path='docs/doc[@for="FileDialog.SetOption"]/*' />
        /// <devdoc>
        ///     Sets the given option to the given boolean value.
        /// </devdoc>
        /// <internalonly/>
        internal void SetOption(int option, bool value) {
            if (value) {
                options |= option;
            }
            else {
                options &= ~option;
            }
        }

        /// <include file='doc\FileDialog.uex' path='docs/doc[@for="FileDialog.ToString"]/*' />
        /// <internalonly/>
        /// <devdoc>
        ///    <para>
        ///       Provides a string version of this Object.
        ///    </para>
        /// </devdoc>
        public override string ToString() {
            StringBuilder sb = new StringBuilder(base.ToString() + ": Title: " + Title + ", FileName: ");
            try
            {
                sb.Append(FileName);
            }
            catch (Exception e)
            {
                sb.Append("<");
                sb.Append(e.GetType().FullName);
                sb.Append(">");
            }
            return sb.ToString();
        }

        
    }
}


