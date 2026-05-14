//------------------------------------------------------------------------------
// <copyright file="Button.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>                                                                
//------------------------------------------------------------------------------

/*
 */
namespace System.Windows.Forms {
    using System.Runtime.Serialization.Formatters;
    using System.Runtime.Remoting;
    using System.Runtime.InteropServices;

    using System.Diagnostics;

    using System;
    using System.Security.Permissions;
    using System.Windows.Forms.ButtonInternal;
    using System.ComponentModel.Design;
    using System.ComponentModel;
    using System.Windows.Forms.Layout;

    using System.Drawing;
    using System.Windows.Forms.Internal;
    using Microsoft.Win32;
    using System.Text.Json.Nodes;
    using System.Threading.Tasks;

    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = false )]
    public class Button : ButtonBase, IButtonControl {

        //public override void WriteAttributeTo(JsonObject json)
        //{
        //    base.WriteAttributeTo(json);
        //    json["DialogResult"] = this.DialogResult.ToString();
        //    json["AutoSizeMode"] = this.AutoSizeMode.ToString();
        //    json["DialogResult"] = this.DialogResult.ToString();
        //}
        /// <include file='doc\Button.uex' path='docs/doc[@for="Button.dialogResult"]/*' />
        /// <devdoc>
        ///     The dialog result that will be sent to the parent dialog form when
        ///     we are clicked.
        /// </devdoc>
        private DialogResult _dialogResult;

        /// <include file='doc\Button.uex' path='docs/doc[@for="Button.dialogResult"]/*' />
        /// <devdoc>
        ///     For buttons whose FaltStyle = FlatStyle.Flat, this property specifies the size, in pixels  
        ///     of the border around the button.
        /// </devdoc>
        private Size systemSize = new Size(Int32.MinValue, Int32.MinValue);

        /// <include file='doc\Button.uex' path='docs/doc[@for="Button.Button"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Initializes a new instance of the <see cref='System.Windows.Forms.Button'/>
        ///       class.
        ///    </para>
        /// </devdoc>
        public Button() : base() {
            // Buttons shouldn't respond to right clicks, so we need to do all our own click logic
            SetStyle(ControlStyles.StandardClick |
                     ControlStyles.StandardDoubleClick,
                     false);
            SetStyle(ControlStyles.UserPaint, false);
        }

        /// <devdoc>
        ///     Allows the control to optionally shrink when AutoSize is true.
        /// </devdoc>
        [
        SRCategory(DCSR.CatLayout),
        Browsable(true),
        DefaultValue(AutoSizeMode.GrowOnly),
        Localizable(true),
        SRDescription(DCSR.ControlAutoSizeModeDescr)
        ]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public AutoSizeMode AutoSizeMode {
            get {
                return GetAutoSizeMode();
            }
            set {

                if (!ClientUtils.IsEnumValid(value, (int)value, (int)AutoSizeMode.GrowAndShrink, (int)AutoSizeMode.GrowOnly)){
                    throw new InvalidEnumArgumentException("value", (int)value, typeof(AutoSizeMode));
                }
                
                if (GetAutoSizeMode() != value) {                    
                    SetAutoSizeMode(value);
                    if(ParentInternal != null) {
                        // DefaultLayout does not keep anchor information until it needs to.  When
                        // AutoSize became a common property, we could no longer blindly call into
                        // DefaultLayout, so now we do a special InitLayout just for DefaultLayout.
                        if(ParentInternal.LayoutEngine == DefaultLayout.Instance) {
                            ParentInternal.LayoutEngine.InitLayout(this, BoundsSpecified.Size);
                        }
                        LayoutTransaction.DoLayout(ParentInternal, this, PropertyNames.AutoSize);
                    }
                }
            }
        }

        internal override ButtonBaseAdapter CreateFlatAdapter() {
            return new ButtonFlatAdapter(this);
        }

        internal override ButtonBaseAdapter CreatePopupAdapter() {
            return new ButtonPopupAdapter(this);
        }
            
        internal override ButtonBaseAdapter CreateStandardAdapter() {
            return new ButtonStandardAdapter(this);
        }

        internal override Size GetPreferredSizeCore(Size proposedConstraints) {
            if(FlatStyle != FlatStyle.System) {
                Size prefSize = base.GetPreferredSizeCore(proposedConstraints);
                return AutoSizeMode == AutoSizeMode.GrowAndShrink ? prefSize : LayoutUtils.UnionSizes(prefSize, Size);                
            }

            if (systemSize.Width == Int32.MinValue) {
                Size requiredSize;
                // Note: The result from the BCM_GETIDEALSIZE message isn't accurate if the font has been
                // changed, because this method is called before the font is set into the device context.
                // Commenting this line is the fix for bug VSWhidbey#228843.
                //if(UnsafeNativeMethods.SendMessage(hWnd, NativeMethods.BCM_GETIDEALSIZE, 0, size) != IntPtr.Zero) {
                //    requiredSize = size.ToSize(); ...
                requiredSize = TextRenderer.MeasureText(this.Text, this.Font);
                requiredSize = SizeFromClientSize(requiredSize);

                // This padding makes FlatStyle.System about the same size as FlatStyle.Standard
                // with an 8px font.
                requiredSize.Width += 14;
                requiredSize.Height += 9;
                systemSize = requiredSize;
            }
            Size paddedSize = systemSize + Padding.Size;            
            return AutoSizeMode == AutoSizeMode.GrowAndShrink ? paddedSize : LayoutUtils.UnionSizes(paddedSize, Size);
        }

        [System.Reflection.Obfuscation(Exclude = true , ApplyToMembers = true )]
        protected override CreateParams CreateParams {
            //[SecurityPermission(SecurityAction.LinkDemand, Flags=SecurityPermissionFlag.UnmanagedCode)]
            get {
                CreateParams cp = base.CreateParams;
                cp.ClassName = "BUTTON";                
                if (GetStyle(ControlStyles.UserPaint)) {
                    cp.Style |= WinFormNativeMethods.BS_OWNERDRAW;
                }
                else {
                    cp.Style |= WinFormNativeMethods.BS_PUSHBUTTON;
                    if (IsDefault) {
                        cp.Style |= WinFormNativeMethods.BS_DEFPUSHBUTTON;
                    }
                }
                return cp;
            }
        }

        /// <include file='doc\Button.uex' path='docs/doc[@for="Button.DialogResult"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Gets or sets a value that is returned to the
        ///       parent form when the button
        ///       is clicked.
        ///    </para>
        /// </devdoc>
        [
        SRCategory(DCSR.CatBehavior),
        DefaultValue(DialogResult.None),
        SRDescription(DCSR.ButtonDialogResultDescr)
        ]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true )]
        public virtual DialogResult DialogResult {
            get {
                return _dialogResult;
            }

            set {
                if (!ClientUtils.IsEnumValid(value, (int)value, (int)DialogResult.None, (int) DialogResult.No)) {
                    throw new InvalidEnumArgumentException("value", (int)value, typeof(DialogResult));
                }
                _dialogResult = value;
                this.LogWF2WPropertyValue("DialogResult", value);
            }
        }

        /// <include file='doc\Button.uex' path='docs/doc[@for="Button.OnMouseEnter"]/*' />
        /// <internalonly/>
        /// <devdoc>
        ///    <para>
        ///       Raises the <see cref='System.Windows.Forms.Control.OnMouseEnter'/> event.
        ///    </para>
        /// </devdoc>
        protected override void OnMouseEnter(EventArgs e) {
            base.OnMouseEnter(e);
        }

        /// <include file='doc\Button.uex' path='docs/doc[@for="Button.OnMouseLeave"]/*' />
        /// <internalonly/>
        /// <devdoc>
        ///    <para>
        ///       Raises the <see cref='System.Windows.Forms.Control.OnMouseLeave'/> event.
        ///    </para>
        /// </devdoc>
        protected override void OnMouseLeave(EventArgs e) {
            base.OnMouseLeave(e);
        }

        /// <include file='doc\Button.uex' path='docs/doc[@for="Button.DoubleClick"]/*' />
        /// <internalonly/><hideinheritance/>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced)]
        public new event EventHandler DoubleClick {
            add {
                base.DoubleClick += value;
            }
            remove {
                base.DoubleClick -= value;
            }
        }

        /// <include file='doc\Button.uex' path='docs/doc[@for="Button.MouseDoubleClick"]/*' />
        /// <internalonly/><hideinheritance/>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced)]
        public new event MouseEventHandler MouseDoubleClick {
            add {
                base.MouseDoubleClick += value;
            }
            remove {
                base.MouseDoubleClick -= value;
            }
        }

        /// <include file='doc\Button.uex' path='docs/doc[@for="Button.NotifyDefault"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Notifies the <see cref='System.Windows.Forms.Button'/>
        ///       whether it is the default button so that it can adjust its appearance
        ///       accordingly.
        ///       
        ///    </para>
        /// </devdoc>
        public virtual void NotifyDefault(bool value) {
            if (IsDefault != value) {
                IsDefault = value;                
            }
        }

        /// <include file='doc\Button.uex' path='docs/doc[@for="Button.OnClick"]/*' />
        /// <internalonly/>
        /// <devdoc>
        ///    <para>
        ///       This method actually raises the Click event. Inheriting classes should
        ///       override this if they wish to be notified of a Click event. (This is far
        ///       preferable to actually adding an event handler.) They should not,
        ///       however, forget to call base.onClick(e); before exiting, to ensure that
        ///       other recipients do actually get the event.
        ///
        ///    </para>
        /// </devdoc>
        protected override void OnClick(EventArgs e) {
            Form form = FindFormInternal();
            if (form != null) form.DialogResult = _dialogResult;


            // accessibility stuff
            //
            AccessibilityNotifyClients(AccessibleEvents.StateChange, -1);
            AccessibilityNotifyClients(AccessibleEvents.NameChange, -1);

            base.OnClick(e);
        }

        protected override void OnFontChanged(EventArgs e) {
            systemSize = new Size(Int32.MinValue, Int32.MinValue);
            base.OnFontChanged(e);
        }

        protected override async Task OnMouseUpAsync(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && MouseIsPressed)
            {
                bool isMouseDown = base.MouseIsDown;
                if (isMouseDown)
                {
                    if (!ValidationCancelled)
                    {
                        //if (GetStyle(ControlStyles.UserPaint))
                        {
                            await OnClickAsync(e);
                        }
                        //OnMouseClick(mevent);
                    }

                    //Point pt = PointToScreen(new Point(mevent.X, mevent.Y));
                    //if (WinFormUnsafeNativeMethods.WindowFromPoint(pt.X, pt.Y) == Handle && !ValidationCancelled) {
                    //    if (GetStyle(ControlStyles.UserPaint)) {
                    //        OnClick(mevent);
                    //    }
                    //    OnMouseClick(mevent);
                    //}
                }
            }
            await base.OnMouseUpAsync(e);
        }
        /// <include file='doc\Button.uex' path='docs/doc[@for="Button.OnMouseUp"]/*' />
        /// <internalonly/>
        /// <devdoc>
        ///    <para>
        ///       Raises the <see cref='System.Windows.Forms.ButtonBase.OnMouseUp'/> event.
        ///       
        ///    </para>
        /// </devdoc>
        protected override void OnMouseUp(MouseEventArgs mevent) {
            if (mevent.Button == MouseButtons.Left && MouseIsPressed) {
                bool isMouseDown = base.MouseIsDown;

                if (GetStyle(ControlStyles.UserPaint)) {
                    //Paint in raised state...
                    ResetFlagsandPaint();
                }
                if (isMouseDown) {
                    if (!ValidationCancelled)
                    {
                        //if (GetStyle(ControlStyles.UserPaint))
                        {
                            OnClick(mevent);
                        }
                        //OnMouseClick(mevent);
                    }

                    //Point pt = PointToScreen(new Point(mevent.X, mevent.Y));
                    //if (WinFormUnsafeNativeMethods.WindowFromPoint(pt.X, pt.Y) == Handle && !ValidationCancelled) {
                    //    if (GetStyle(ControlStyles.UserPaint)) {
                    //        OnClick(mevent);
                    //    }
                    //    OnMouseClick(mevent);
                    //}
                }
            }
            base.OnMouseUp(mevent);
        }

        protected override void OnTextChanged(EventArgs e) {
            systemSize = new Size(Int32.MinValue, Int32.MinValue);
            base.OnTextChanged(e);
        }

        /// <include file='doc\Button.uex' path='docs/doc[@for="Button.PerformClick"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Generates a <see cref='System.Windows.Forms.Control.Click'/> event for a
        ///       button.
        ///    </para>
        /// </devdoc>
        public void PerformClick() {
            if (CanSelect) {
                bool validatedControlAllowsFocusChange;
                bool validate = ValidateActiveControl(out validatedControlAllowsFocusChange);
                if (!ValidationCancelled && (validate || validatedControlAllowsFocusChange))
                {
                    //Paint in raised state...
                    //
                    ResetFlagsandPaint();
                    OnClick(EventArgs.Empty);
                }
            }
        }

        /// <include file='doc\Button.uex' path='docs/doc[@for="Button.ProcessMnemonic"]/*' />
        /// <internalonly/>
        /// <devdoc>
        ///    <para>
        ///       Lets a control process mnmemonic characters. Inheriting classes can
        ///       override this to add extra functionality, but should not forget to call
        ///       base.ProcessMnemonic(charCode); to ensure basic functionality
        ///       remains unchanged.
        ///
        ///    </para>
        /// </devdoc>
        //[UIPermission(SecurityAction.LinkDemand, Window=UIPermissionWindow.AllWindows)]
        protected internal override bool ProcessMnemonic(char charCode) {
            if (UseMnemonic && CanProcessMnemonic() && IsMnemonic(charCode, Text)) {
                PerformClick();
                return true;
            }
            return base.ProcessMnemonic(charCode);
        }

        /// <include file='doc\Button.uex' path='docs/doc[@for="Button.ToString"]/*' />
        /// <internalonly/>
        /// <devdoc>
        ///    <para>
        ///       Provides some interesting information for the Button control in
        ///       String form.
        ///    </para>
        /// </devdoc>
        public override string ToString() {

            string s = base.ToString();
            return s + ", Text: " + Text;
        }

        /// <include file='doc\Button.uex' path='docs/doc[@for="Button.WndProc"]/*' />
        /// <devdoc>
        ///     The button's window procedure.  Inheriting classes can override this
        ///     to add extra functionality, but should not forget to call
        ///     base.wndProc(m); to ensure the button continues to function properly.
        /// </devdoc>
        /// <internalonly/>
        //[SecurityPermission(SecurityAction.LinkDemand, Flags=SecurityPermissionFlag.UnmanagedCode)]
        protected override async Task WndProc(Message m) {
            switch (m.Msg) {
                case WinFormNativeMethods.WM_REFLECT + WinFormNativeMethods.WM_COMMAND:
                    if (WinFormNativeMethods.Util.HIWORD(m.WParam) == WinFormNativeMethods.BN_CLICKED) {
                        Debug.Assert(!GetStyle(ControlStyles.UserPaint), "Shouldn't get BN_CLICKED when UserPaint");
                        if (!ValidationCancelled) {
                            OnClick(EventArgs.Empty);
                        }                        
                    }
                    break;
                case WinFormNativeMethods.WM_ERASEBKGND:
                    DefWndProc(ref m);
                    break;
                default:
                    await base.WndProc(m);
                    break;
            }
        }
    }
}


