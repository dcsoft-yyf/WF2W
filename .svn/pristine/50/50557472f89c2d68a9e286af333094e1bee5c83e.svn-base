//------------------------------------------------------------------------------
// <copyright file="TextBox.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>                                                                
//------------------------------------------------------------------------------

/*
 */
namespace System.Windows.Forms {
    using System.Runtime.Remoting;
    using System.ComponentModel;
    using System.Diagnostics;
    using System;
    using System.Security.Permissions;
    using System.Windows.Forms;
    using System.ComponentModel.Design;    
    using System.Drawing;
    using Microsoft.Win32;
    using System.Reflection;
    using System.Text;
    using System.Runtime.InteropServices;
    using System.Collections.Specialized;
    using System.Drawing.Design;
    using System.Security;
    using System.Windows.Forms.VisualStyles;
    using System.Text.Json.Nodes;
    using System.Threading.Tasks;

    /// <include file='doc\TextBox.uex' path='docs/doc[@for="TextBox"]/*' />
    /// <devdoc>
    ///    <para>
    ///       Represents a Windows text box control.
    ///    </para>
    /// </devdoc>


    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = false )]
    public class TextBox : TextBoxBase {

        //public override void WriteAttributeTo(JsonObject json)
        //{
        //    json["AutoCompleteSource"] = this.AutoCompleteSource.ToString();
        //    json["AutoCompleteMode"] = this.AutoCompleteMode.ToString();
        //    json["AcceptsReturn"] = this.AcceptsReturn;
        //    json["Multiline"] = this.Multiline;
        //    json["WordWrap"] = this.WordWrap;
        //    json["ScrollBars"] = this.ScrollBars.ToString();
        //    json["TextAlign"] = this.TextAlign.ToString();
        //    base.WriteAttributeTo(json);
        //}

        private static readonly object EVENT_TEXTALIGNCHANGED = new object();
    
        /// <devdoc>
        ///     Controls whether or not the edit box consumes/respects ENTER key
        ///     presses.  While this is typically desired by multiline edits, this
        ///     can interfere with normal key processing in a dialog.
        /// </devdoc>
        private bool acceptsReturn = false;

        /// <devdoc>
        ///     Indicates what the current special password character is.  This is 
        ///     displayed instead of any other text the user might enter.
        /// </devdoc>
        private char passwordChar = (char)0;

        private bool useSystemPasswordChar;

        /// <devdoc>
        ///     Controls whether or not the case of characters entered into the edit
        ///     box is forced to a specific case.
        /// </devdoc>
        private CharacterCasing characterCasing = System.Windows.Forms.CharacterCasing.Normal;

        /// <devdoc>
        ///     Controls which scrollbars appear by default.
        /// </devdoc>
        private ScrollBars scrollBars = System.Windows.Forms.ScrollBars.None;

        /// <devdoc>
        ///     Controls text alignment in the edit box.
        /// </devdoc>
        private HorizontalAlignment textAlign = HorizontalAlignment.Left;
        
        /// <devdoc>
        ///     True if the selection has been set by the user.  If the selection has
        ///     never been set and we get focus, we focus all the text in the control
        ///     so we mimic the Windows dialog manager.
        /// </devdoc>
        private bool selectionSet = false;

        /// <include file='doc\TextBox.uex' path='docs/doc[@for="TextBox.autoCompleteMode"]/*' />
        /// <devdoc>
        ///     This stores the value for the autocomplete mode which can be either
        ///     None, AutoSuggest, AutoAppend or AutoSuggestAppend.
        /// </devdoc>
        private AutoCompleteMode autoCompleteMode = AutoCompleteMode.None;
        
        /// <include file='doc\TextBox.uex' path='docs/doc[@for="TextBox.autoCompleteSource"]/*' />
        /// <devdoc>
        ///     This stores the value for the autoCompleteSource mode which can be one of the values
        ///     from AutoCompleteSource enum.
        /// </devdoc>
        private AutoCompleteSource autoCompleteSource = AutoCompleteSource.None;

        /// <include file='doc\TextBox.uex' path='docs/doc[@for="TextBox.autoCompleteCustomSource"]/*' />
        /// <devdoc>
        ///     This stores the custom StringCollection required for the autoCompleteSource when its set to CustomSource.
        /// </devdoc>
        private AutoCompleteStringCollection autoCompleteCustomSource;
        private bool fromHandleCreate = false;
        private StringSource stringSource = null;

        /// <include file='doc\TextBox.uex' path='docs/doc[@for="TextBox.TextBox"]/*' />
        public TextBox(){
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public bool AcceptsReturn {
            get {
                return acceptsReturn;
            }

            set {
                acceptsReturn = value;
                this.LogWF2WPropertyValue("AcceptsReturn", value);
            }
        }


        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public AutoCompleteMode AutoCompleteMode {
            get {
                return autoCompleteMode;
            }
            set {
                if (!ClientUtils.IsEnumValid(value, (int)value, (int)AutoCompleteMode.None, (int)AutoCompleteMode.SuggestAppend)){
                    throw new InvalidEnumArgumentException("value", (int)value, typeof(AutoCompleteMode));
                }
                bool resetAutoComplete = false;
                if (autoCompleteMode != AutoCompleteMode.None && value == AutoCompleteMode.None) {
                    resetAutoComplete = true;
                }
                autoCompleteMode = value;
                SetAutoComplete(resetAutoComplete);
            }
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public AutoCompleteSource AutoCompleteSource {
            get {
                return this.autoCompleteSource;
            }
            set {
                // FxCop: Avoid usage of Enum.IsDefined - this looks like an enum that could grow
                if (!ClientUtils.IsEnumValid_NotSequential(value, 
                                             (int)value,
                                             (int)AutoCompleteSource.None, 
                                             (int)AutoCompleteSource.AllSystemSources,
                                             (int)AutoCompleteSource.AllUrl,
                                             (int)AutoCompleteSource.CustomSource,
                                             (int)AutoCompleteSource.FileSystem,
                                             (int)AutoCompleteSource.FileSystemDirectories,
                                             (int)AutoCompleteSource.HistoryList,
                                             (int)AutoCompleteSource.ListItems,
                                             (int)AutoCompleteSource.RecentlyUsedList)){   
                    throw new InvalidEnumArgumentException("value", (int)value, typeof(AutoCompleteSource));
                }
                if (value == AutoCompleteSource.ListItems) {
                    throw new NotSupportedException(DCSR.GetString(DCSR.TextBoxAutoCompleteSourceNoItems));
                }

                // VSWhidbey 466300
                if (value != AutoCompleteSource.None && value != AutoCompleteSource.CustomSource)
                {
                    //FileIOPermission fiop = new FileIOPermission(PermissionState.Unrestricted);
                    //fiop.AllFiles = FileIOPermissionAccess.PathDiscovery;
                    //fiop.Demand();
                }

                autoCompleteSource = value;
                SetAutoComplete(false);
            }
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public AutoCompleteStringCollection AutoCompleteCustomSource {
            get {
                if (autoCompleteCustomSource == null) {
                    autoCompleteCustomSource = new AutoCompleteStringCollection();
                    autoCompleteCustomSource.CollectionChanged += new CollectionChangeEventHandler(this.OnAutoCompleteCustomSourceChanged);
                }
                return autoCompleteCustomSource;
            }
            set {
                if (autoCompleteCustomSource != value) {
                    if (autoCompleteCustomSource != null) {
                        autoCompleteCustomSource.CollectionChanged -= new CollectionChangeEventHandler(this.OnAutoCompleteCustomSourceChanged);
                    }
                    
                    autoCompleteCustomSource = value;
                    
                    if (value != null) {
                        autoCompleteCustomSource.CollectionChanged += new CollectionChangeEventHandler(this.OnAutoCompleteCustomSourceChanged);
                    }
                    SetAutoComplete(false);
                }
                
            }
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public CharacterCasing CharacterCasing {
            get {
                return characterCasing;
            }
            set {
                if (characterCasing != value) {
                    //verify that 'value' is a valid enum type...
                    //valid values are 0x0 to 0x2
                    if (!ClientUtils.IsEnumValid(value, (int)value, (int)CharacterCasing.Normal, (int)CharacterCasing.Lower)){
                        throw new InvalidEnumArgumentException("value", (int)value, typeof(CharacterCasing));
                    }

                    characterCasing = value;
                    RecreateHandle();
                    this.LogWF2WPropertyValue("CharacterCasing", value);
                }
            }
        }

        /// <include file='doc\TextBox.uex' path='docs/doc[@for="TextBox.Multiline"]/*' />
        /// <devdoc>
        ///    <para>[To be supplied.]</para>
        /// </devdoc>
        public override bool Multiline {
            get {
                return base.Multiline;
            }
            set {
                
                if (Multiline != value)
                {
                    base.Multiline = value;
                    if (value && AutoCompleteMode != AutoCompleteMode.None)
                    {
                        RecreateHandle();
                    }
                    this.LogWF2WPropertyValue("Multiline", value);
                }
            }
        }

        /// <devdoc>
        ///     Determines if the control is in password protect mode.
        /// </devdoc>
        internal override bool PasswordProtect {
            get {
                return this.PasswordChar != '\0';
            }
        }

     
        /// <include file='doc\TextBox.uex' path='docs/doc[@for="TextBox.CreateParams"]/*' />
        /// <internalonly/>
        /// <devdoc>
        ///    <para>
        ///       Returns the parameters needed to create the handle. Inheriting classes
        ///       can override this to provide extra functionality. They should not,
        ///       however, forget to call base.getCreateParams() first to get the struct
        ///       filled up with the basic info.
        ///    </para>
        /// </devdoc>
        protected override CreateParams CreateParams {
            //[SecurityPermission(SecurityAction.LinkDemand, Flags=SecurityPermissionFlag.UnmanagedCode)]
            get {
                CreateParams cp = base.CreateParams;
                switch (characterCasing) {
                    case CharacterCasing.Lower:
                        cp.Style |= WinFormNativeMethods.ES_LOWERCASE;
                        break;
                    case CharacterCasing.Upper:
                        cp.Style |= WinFormNativeMethods.ES_UPPERCASE;
                        break;
                }

                // Translate for Rtl if necessary
                //
                HorizontalAlignment align = RtlTranslateHorizontal(textAlign);
                cp.ExStyle &= ~WinFormNativeMethods.WS_EX_RIGHT;   // WS_EX_RIGHT overrides the ES_XXXX alignment styles
                switch (align) {
                    case HorizontalAlignment.Left:
                        cp.Style |= WinFormNativeMethods.ES_LEFT;
                        break;
                    case HorizontalAlignment.Center:
                        cp.Style |= WinFormNativeMethods.ES_CENTER;
                        break;
                    case HorizontalAlignment.Right:
                        cp.Style |= WinFormNativeMethods.ES_RIGHT;
                        break;
                }
                
                if (Multiline) {
                    // vs 59731: Don't show horizontal scroll bars which won't do anything
                    if ((scrollBars & ScrollBars.Horizontal) == ScrollBars.Horizontal
                        && textAlign == HorizontalAlignment.Left
                        && !WordWrap) {
                        cp.Style |= WinFormNativeMethods.WS_HSCROLL;
                    }
                    if ((scrollBars & ScrollBars.Vertical) == ScrollBars.Vertical) {
                        cp.Style |= WinFormNativeMethods.WS_VSCROLL;
                    }
                }

                if (useSystemPasswordChar) {
                    cp.Style |= WinFormNativeMethods.ES_PASSWORD;
                }
                
                return cp;
            }
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public char PasswordChar {
            get {
                if (!IsHandleCreated) {
                    CreateHandle();
                }
                return (char)SendMessage(WinFormNativeMethods.EM_GETPASSWORDCHAR, 0, 0);
            }
            set {
                passwordChar = value;
                if (!useSystemPasswordChar) {
                    if (IsHandleCreated) {
                        if (PasswordChar != value) {
                            // Set the password mode.
                            SendMessage(WinFormNativeMethods.EM_SETPASSWORDCHAR, value, 0);

                            // Disable IME if setting the control to password mode.
                            VerifyImeRestrictedModeChanged();

                            ResetAutoComplete(false);
                            Invalidate();
                        }
                    }
                }
                this.LogWF2WPropertyValue("PasswordChar", value);
            }
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public ScrollBars ScrollBars {
            get {
                return scrollBars;
            }
            set {
                if (scrollBars != value) {
                    //valid values are 0x0 to 0x3
                    if (!ClientUtils.IsEnumValid(value, (int)value, (int)ScrollBars.None, (int)ScrollBars.Both)){
                        throw new InvalidEnumArgumentException("value", (int)value, typeof(ScrollBars));
                    }

                    scrollBars = value;
                    RecreateHandle();
                    this.LogWF2WPropertyValue("ScrollBars", value);
                }
            }
        }

        internal override Size GetPreferredSizeCore(Size proposedConstraints) {
            Size scrollBarPadding = Size.Empty;

            if(Multiline && !WordWrap && (ScrollBars & ScrollBars.Horizontal) != 0) {
                scrollBarPadding.Height += SystemInformation.HorizontalScrollBarHeight;
            }
            if(Multiline && (ScrollBars & ScrollBars.Vertical) != 0) {
                scrollBarPadding.Width += SystemInformation.VerticalScrollBarWidth;
            }

            // Subtract the scroll bar padding before measuring
            proposedConstraints -= scrollBarPadding;
            
            Size prefSize = base.GetPreferredSizeCore(proposedConstraints);

            return prefSize + scrollBarPadding;
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public override string Text {
            get {
                return base.Text;
            }
            set {
                base.Text = value;
                selectionSet = false;
            }
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public HorizontalAlignment TextAlign {
            get {
                return textAlign;
            }
            set {
                if (textAlign != value) {
                    //verify that 'value' is a valid enum type...

                    //valid values are 0x0 to 0x2
                    if (!ClientUtils.IsEnumValid(value, (int)value, (int)HorizontalAlignment.Left, (int)HorizontalAlignment.Center)){
                        throw new InvalidEnumArgumentException("value", (int)value, typeof(HorizontalAlignment));
                    }

                    textAlign = value;
                    RecreateHandle();
                    OnTextAlignChanged(EventArgs.Empty);
                    this.LogWF2WPropertyValue("TextAlign", value);
                }
            }
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public bool UseSystemPasswordChar {
            get {
                return useSystemPasswordChar;
            }
            set {
                if (value != useSystemPasswordChar) {
                    useSystemPasswordChar = value;

                    // RecreateHandle will update IME restricted mode.
                    RecreateHandle();

                    if (value) {
                        ResetAutoComplete(false);
                    }
                    this.LogWF2WPropertyValue("UseSystemPasswordChar", value);
                }
            }
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public event EventHandler TextAlignChanged {
            add {
                Events.AddHandler(EVENT_TEXTALIGNCHANGED, value);
            }

            remove {
                Events.RemoveHandler(EVENT_TEXTALIGNCHANGED, value);
            }
        }

        /// <include file='doc\TabControl.uex' path='docs/doc[@for="TextBox.Dispose"]/*' />
        protected override void Dispose(bool disposing) {
            if (disposing) {
                // VSWhidbey 281668 - Reset this just in case, because the SHAutoComplete stuff
                // will subclass this guys wndproc (and nativewindow can't know about it).
                // so this will undo it, but on a dispose we'll be Destroying the window anyay.
                //
                ResetAutoComplete(true);
                if (autoCompleteCustomSource != null) {
                    autoCompleteCustomSource.CollectionChanged -= new CollectionChangeEventHandler(this.OnAutoCompleteCustomSourceChanged);
                }
                if (stringSource != null)
                {
                    stringSource.ReleaseAutoComplete();
                    stringSource = null;
                }
            }
            base.Dispose(disposing);
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        protected override bool IsInputKey(Keys keyData) {
            if (Multiline && (keyData & Keys.Alt) == 0) {
                switch (keyData & Keys.KeyCode) {
                    case Keys.Return:
                        return acceptsReturn;
                }
            }
            return base.IsInputKey(keyData);
        }


        private void OnAutoCompleteCustomSourceChanged(object sender, CollectionChangeEventArgs e) {
            if (AutoCompleteSource == AutoCompleteSource.CustomSource)
            {
                SetAutoComplete(true);
            }
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        protected override void OnBackColorChanged(EventArgs e) {
            base.OnBackColorChanged(e);
            // VSWhidbey 465708. Force repainting of the entire window frame
            if (Application.RenderWithVisualStyles && this.IsHandleCreated && this.BorderStyle == BorderStyle.Fixed3D) {
                WinFormSafeNativeMethods.RedrawWindow(new HandleRef(this, this.Handle), null, WinFormNativeMethods.NullHandleRef, WinFormNativeMethods.RDW_INVALIDATE | WinFormNativeMethods.RDW_FRAME);
            }
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        protected override void OnFontChanged(EventArgs e) {
            base.OnFontChanged(e);
            if (this.AutoCompleteMode != AutoCompleteMode.None) {
                //we always will recreate the handle when autocomplete mode is on
                RecreateHandle();
            }
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        protected override void OnGotFocus(EventArgs e) {
            base.OnGotFocus(e);
            if (!selectionSet) {
                // We get one shot at selecting when we first get focus.  If we don't
                // do it, we still want to act like the selection was set.
                selectionSet = true;

                // If the user didn't provide a selection, force one in.
                if (SelectionLength == 0 && Control.MouseButtons == MouseButtons.None) {
                    SelectAll();
                }
            }
        }

        /// <include file='doc\TextBox.uex' path='docs/doc[@for="TextBox.OnHandleCreated"]/*' />
        /// <internalonly/>
        /// <devdoc>
        ///    Overridden to update the newly created handle with the settings of the
        ///    PasswordChar properties.
        /// </devdoc>
        protected override void OnHandleCreated(EventArgs e) {
            base.OnHandleCreated(e);
            base.SetSelectionOnHandle();

            if (passwordChar != 0) {
                if (!useSystemPasswordChar) {
                    SendMessage(WinFormNativeMethods.EM_SETPASSWORDCHAR, passwordChar, 0);
                }
            }

            VerifyImeRestrictedModeChanged();

            if (AutoCompleteMode != AutoCompleteMode.None) {
                try
                {
                    fromHandleCreate = true;
                    SetAutoComplete(false);
                }
                finally
                {
                    fromHandleCreate = false;
                }
            }
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            if (stringSource != null)
            {
                stringSource.ReleaseAutoComplete();
                stringSource = null;
            }
            base.OnHandleDestroyed(e);
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        protected virtual void OnTextAlignChanged(EventArgs e) {
            EventHandler eh = Events[EVENT_TEXTALIGNCHANGED] as EventHandler;
            if (eh != null) {
                 eh(this, e);
            }
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void Paste(string text){
            base.SetSelectedTextInternal(text, false);
        }
     
        /// <devdoc>
        ///     Performs the actual select without doing arg checking.
        /// </devdoc>        
        internal override void SelectInternal(int start, int length, int textLen) {
            // If user set selection into text box, mark it so we don't
            // clobber it when we get focus.
            selectionSet = true;
            base.SelectInternal( start, length, textLen );
        }

        private string[] GetStringsForAutoComplete()
        {
            string[] strings = new string[AutoCompleteCustomSource.Count];
            for (int i = 0; i < AutoCompleteCustomSource.Count; i++) {
                strings[i] = AutoCompleteCustomSource[i];
            }
            return strings;
        }



        /// <include file='doc\TextBox.uex' path='docs/doc[@for="TextBox.SetAutoComplete"]/*' />
        /// <devdoc>
        ///     Sets the AutoComplete mode in TextBox.
        /// </devdoc>
        internal void SetAutoComplete(bool reset)
        {
            //Autocomplete Not Enabled for Password enabled and MultiLine Textboxes.
            if (Multiline || passwordChar != 0 || useSystemPasswordChar || AutoCompleteSource == AutoCompleteSource.None) {
                return;
            }

            if (AutoCompleteMode != AutoCompleteMode.None) {
                if (!fromHandleCreate)
                {
                    //RecreateHandle to avoid Leak.
                    // notice the use of member variable to avoid re-entrancy
                    AutoCompleteMode backUpMode = this.AutoCompleteMode;
                    autoCompleteMode = AutoCompleteMode.None;
                    RecreateHandle();
                    autoCompleteMode = backUpMode;
                }
                
                if (AutoCompleteSource == AutoCompleteSource.CustomSource) {
                    if (IsHandleCreated && AutoCompleteCustomSource != null) {
                        if (AutoCompleteCustomSource.Count == 0) {
                            ResetAutoComplete(true);
                        }
                        else {
                            if (stringSource == null)
                            {
                                stringSource = new StringSource(GetStringsForAutoComplete());
                                if (!stringSource.Bind(new HandleRef(this, Handle), (int)AutoCompleteMode))
                                {
                                   throw new ArgumentException(DCSR.GetString(DCSR.AutoCompleteFailure));
                                }
                            }
                            else
                            {
                                stringSource.RefreshList(GetStringsForAutoComplete());
                            }
                        }
                    }
        
                }
                else {
                    try {
                        if (IsHandleCreated) {
                            int mode = 0;
                            if (AutoCompleteMode == AutoCompleteMode.Suggest) {
                                mode |=  WinFormNativeMethods.AUTOSUGGEST | WinFormNativeMethods.AUTOAPPEND_OFF;
                            }
                            if (AutoCompleteMode == AutoCompleteMode.Append) {
                                mode |=  WinFormNativeMethods.AUTOAPPEND | WinFormNativeMethods.AUTOSUGGEST_OFF;
                            }
                            if (AutoCompleteMode == AutoCompleteMode.SuggestAppend) {
                                mode |=  WinFormNativeMethods.AUTOSUGGEST;
                                mode |=  WinFormNativeMethods.AUTOAPPEND;
                            }
                            int ret = WinFormSafeNativeMethods.SHAutoComplete(new HandleRef(this, Handle) , (int)AutoCompleteSource | mode);
                        }
                    }
                    catch (SecurityException) {
                        // If we don't have full trust, degrade gracefully. Allow the control to
                        // function without auto-complete. Allow the app to continue running.
                    }
                }
            }
            else if (reset) {
                ResetAutoComplete(true);
            }
        }


        // <include file='doc\TextBox.uex' path='docs/doc[@for="TextBox.ResetAutoComplete"]/*' />
        /// <devdoc>
        ///     Resets the AutoComplete mode in TextBox.
        /// </devdoc>
        private void ResetAutoComplete(bool force) {
            //if ((AutoCompleteMode != AutoCompleteMode.None || force) && IsHandleCreated) {
            //    int mode = (int)AutoCompleteSource.AllSystemSources | WinFormNativeMethods.AUTOSUGGEST_OFF | WinFormNativeMethods.AUTOAPPEND_OFF;
            //    WinFormSafeNativeMethods.SHAutoComplete(new HandleRef(this, Handle) , mode);
            //}
        }

        private void ResetAutoCompleteCustomSource() {
            AutoCompleteCustomSource = null;
        }

        private void WmPrint(ref Message m) {
            base.WndProc( m);
            if ((WinFormNativeMethods.PRF_NONCLIENT & (int)m.LParam) != 0 && Application.RenderWithVisualStyles && this.BorderStyle == BorderStyle.Fixed3D) {
                //IntSecurity.UnmanagedCode.Assert();
                try {
                    using (Graphics g = Graphics.FromHdc(m.WParam)) {
                        Rectangle rect = new Rectangle(0, 0, this.Size.Width - 1, this.Size.Height - 1);
                        using (Pen pen = new Pen(VisualStyleInformation.TextControlBorder)) {
                            g.DrawRectangle(pen, rect);
                        }
                        rect.Inflate(-1, -1);
                        g.DrawRectangle(SystemPens.Window, rect);
                    }
                }
                finally {
                    //CodeAccessPermission.RevertAssert();
                }
            }
        }
	
	//-------------------------------------------------------------------------------------------------
        
        /// <include file='doc\TextBox.uex' path='docs/doc[@for="TextBox.WndProc"]/*' />
        /// <internalonly/>
        /// <devdoc>
        ///    The edits window procedure.  Inheritng classes can override this
        ///    to add extra functionality, but should not forget to call
        ///    base.wndProc(m); to ensure the combo continues to function properly.
        /// </devdoc>
        //[SecurityPermission(SecurityAction.LinkDemand, Flags=SecurityPermissionFlag.UnmanagedCode)]
        protected override async  Task WndProc(Message m) {
            switch (m.Msg) {
                // Work around a very obscure Windows issue. See ASURT 45255.
                case WinFormNativeMethods.WM_LBUTTONDOWN:
                    MouseButtons realState = MouseButtons;
                    bool wasValidationCancelled = ValidationCancelled;
                    FocusInternal();
                    if (realState == MouseButtons && 
                       (!ValidationCancelled || wasValidationCancelled)) {
                        await base.WndProc(m);
                    }                    
                    break;
                //for readability ... so that we know whats happening ...
                // case WM_LBUTTONUP is included here eventhough it just calls the base.
                case WinFormNativeMethods.WM_LBUTTONUP:  
                    await base.WndProc(m);
                    break;
                case WinFormNativeMethods.WM_PRINT:
                    WmPrint(ref m);
                    break;
                default:
                    await base.WndProc(m);
                    break;
            }
            
        }
        
    }
}
