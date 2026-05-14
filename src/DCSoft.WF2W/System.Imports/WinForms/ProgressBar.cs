//------------------------------------------------------------------------------
// <copyright file="ProgressBar.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

/*
 */
namespace System.Windows.Forms {
    using System.Runtime.Serialization.Formatters;
    using System.Runtime.Remoting;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System;
    using System.Security.Permissions;
    using System.Drawing;
    using System.ComponentModel;
    using System.ComponentModel.Design;
    using System.Windows.Forms;
    using Microsoft.Win32;
    using System.Runtime.InteropServices;
    using System.Windows.Forms.Layout;
    using System.Globalization;

    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = false)]
    public class ProgressBar : Control {


        //# VS7 205: simcooke
        //REMOVED: AddOnValueChanged, RemoveOnValueChanged, OnValueChanged and all designer plumbing associated with it.
        //         OnValueChanged event no longer exists.

        // these four values define the range of possible values, how to navigate
        // through them, and the current position
        //
        private int minimum = 0;
        private int maximum = 100;
        private int step = 10;
        private int value = 0;

        //this defines marquee animation speed
        private int marqueeSpeed = 100;

        private Color defaultForeColor = SystemColors.Highlight;

        private ProgressBarStyle style = ProgressBarStyle.Blocks;

        private EventHandler onRightToLeftLayoutChanged;
        private bool rightToLeftLayout = false;


        /// <include file='doc\ProgressBar.uex' path='docs/doc[@for="ProgressBar.ProgressBar"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Initializes a new instance of the <see cref='System.Windows.Forms.ProgressBar'/> class in its default
        ///       state.
        ///    </para>
        /// </devdoc>
        public ProgressBar()
        : base() {
            SetStyle(ControlStyles.UserPaint |
                     ControlStyles.UseTextForAccessibility |
                     ControlStyles.Selectable, false);
            ForeColor = defaultForeColor;
        }

        /// <include file='doc\ProgressBar.uex' path='docs/doc[@for="ProgressBar.CreateParams"]/*' />
        /// <internalonly/>
        /// <devdoc>
        ///    <para>
        ///       This is called when creating a window. Inheriting classes can ovveride
        ///       this to add extra functionality, but should not forget to first call
        ///       base.getCreateParams() to make sure the control continues to work
        ///       correctly.
        ///    </para>
        /// </devdoc>
        protected override CreateParams CreateParams {
            //[SecurityPermission(SecurityAction.LinkDemand, Flags=SecurityPermissionFlag.UnmanagedCode)]
            get {
                CreateParams cp = base.CreateParams;
                cp.ClassName = WinFormNativeMethods.WC_PROGRESS;
                if (this.Style == ProgressBarStyle.Continuous) {
                    cp.Style |= WinFormNativeMethods.PBS_SMOOTH;
                }
                else if (this.Style == ProgressBarStyle.Marquee && !DesignMode) {
                    cp.Style |= WinFormNativeMethods.PBS_MARQUEE;
                }

                if (RightToLeft == RightToLeft.Yes && RightToLeftLayout == true) {
                    //We want to turn on mirroring for Form explicitly.
                    cp.ExStyle |= WinFormNativeMethods.WS_EX_LAYOUTRTL;
                    //Don't need these styles when mirroring is turned on.
                    cp.ExStyle &= ~(WinFormNativeMethods.WS_EX_RTLREADING | WinFormNativeMethods.WS_EX_RIGHT | WinFormNativeMethods.WS_EX_LEFTSCROLLBAR);
                }
                return cp;
            }
        }

        /// <include file='doc\ProgressBar.uex' path='docs/doc[@for="ProgressBar.AllowDrop"]/*' />
        /// <internalonly/>
        /// <devdoc>
        /// </devdoc>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public override bool AllowDrop {
            get {
                return base.AllowDrop;
            }
            set {
                base.AllowDrop = value;
            }
        }

        /// <include file='doc\ProgressBar.uex' path='docs/doc[@for="ProgressBar.BackgroundImage"]/*' />
        /// <internalonly/>
        /// <devdoc>
        /// </devdoc>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public override Image BackgroundImage {
            get {
                return base.BackgroundImage;
            }
            set {
                base.BackgroundImage = value;
            }
        }


        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public ProgressBarStyle Style {
            get {
                return style;
            }
            set {
                if (style != value) {
                    //valid values are 0x0 to 0x2
                    if (!ClientUtils.IsEnumValid(value, (int)value, (int)ProgressBarStyle.Blocks, (int)ProgressBarStyle.Marquee)){
                        throw new InvalidEnumArgumentException("value", (int)value, typeof(ProgressBarStyle));
                    }
                    style = value;
                    if (IsHandleCreated)
                        RecreateHandle();
                    if (style == ProgressBarStyle.Marquee)
                    {
                        StartMarquee();
                    }
                    this.LogWF2WPropertyValue("Style", value );
                }
            }
        }

        /// <include file='doc\ProgressBar.uex' path='docs/doc[@for="ProgressBar.BackgroundImageChanged"]/*' />
        /// <internalonly/>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        new public event EventHandler BackgroundImageChanged {
            add {
                base.BackgroundImageChanged += value;
            }
            remove {
                base.BackgroundImageChanged -= value;
            }
        }


        /// <include file='doc\ProgressBar.uex' path='docs/doc[@for="ProgressBar.BackgroundImageLayout"]/*' />
        /// <internalonly/>
        /// <devdoc>
        /// </devdoc>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public override ImageLayout BackgroundImageLayout {
            get {
                return base.BackgroundImageLayout;
            }
            set {
                base.BackgroundImageLayout = value;
            }
        }

        /// <include file='doc\ProgressBar.uex' path='docs/doc[@for="ProgressBar.BackgroundImageLayoutChanged"]/*' />
        /// <internalonly/>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        new public event EventHandler BackgroundImageLayoutChanged {
            add {
                base.BackgroundImageLayoutChanged += value;
            }
            remove {
                base.BackgroundImageLayoutChanged -= value;
            }
        }


        /// <include file='doc\ProgressBar.uex' path='docs/doc[@for="ProgressBar.CausesValidation"]/*' />
        /// <internalonly/>
        /// <devdoc/>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public new bool CausesValidation {
            get {
                return base.CausesValidation;
            }
            set {
                base.CausesValidation = value;
            }
        }

        /// <include file='doc\ProgressBar.uex' path='docs/doc[@for="ProgressBar.CausesValidationChanged"]/*' />
        /// <internalonly/>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        new public event EventHandler CausesValidationChanged {
            add {
                base.CausesValidationChanged += value;
            }
            remove {
                base.CausesValidationChanged -= value;
            }
        }

        /// <include file='doc\ProgressBar.uex' path='docs/doc[@for="ProgressBar.DefaultImeMode"]/*' />
        protected override ImeMode DefaultImeMode {
            get {
                return ImeMode.Disable;
            }
        }

        /// <include file='doc\ProgressBar.uex' path='docs/doc[@for="ProgressBar.DefaultSize"]/*' />
        /// <devdoc>
        ///     Deriving classes can override this to configure a default size for their control.
        ///     This is more efficient than setting the size in the control's constructor.
        /// </devdoc>
        protected override Size DefaultSize {
            get {
                return new Size(100, 23);
            }
        }

        /// <include file='doc\ProgressBar.uex' path='docs/doc[@for="ProgressBar.DoubleBuffered"]/*' />
        /// <devdoc>
        ///     This property is overridden and hidden from statement completion
        ///     on controls that are based on Win32 Native Controls.
        /// </devdoc>
        [EditorBrowsable(EditorBrowsableState.Never)]
        protected override bool DoubleBuffered {
            get {
                return base.DoubleBuffered;
            }
            set {
                base.DoubleBuffered = value;
            }
        }

        /// <include file='doc\ProgressBar.uex' path='docs/doc[@for="ProgressBar.Font"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Gets or sets the font of text in the <see cref='System.Windows.Forms.ProgressBar'/>.
        ///    </para>
        /// </devdoc>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public override Font Font {
            get {
                return base.Font;
            }
            set {
                base.Font = value;
            }
        }

        /// <include file='doc\ProgressBar.uex' path='docs/doc[@for="ProgressBar.FontChanged"]/*' />
        /// <internalonly/>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        new public event EventHandler FontChanged {
            add {
                base.FontChanged += value;
            }
            remove {
                base.FontChanged -= value;
            }
        }

        /// <include file='doc\ProgressBar.uex' path='docs/doc[@for="ProgressBar.ImeMode"]/*' />
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        new public ImeMode ImeMode {
            get {
                return base.ImeMode;
            }
            set {
                base.ImeMode = value;
            }
        }

        /// <include file='doc\ProgressBar.uex' path='docs/doc[@for="ProgressBar.ImeModeChanged"]/*' />
        /// <internalonly/>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public new event EventHandler ImeModeChanged {
            add {
                base.ImeModeChanged += value;
            }
            remove {
                base.ImeModeChanged -= value;
            }
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public int MarqueeAnimationSpeed {
            get {
                return marqueeSpeed;
            }            
            [SuppressMessage("Microsoft.Usage", "CA2208:InstantiateArgumentExceptionsCorrectly")]
            set {
                if (value < 0) {
                    throw new ArgumentOutOfRangeException("MarqueeAnimationSpeed must be non-negative");
                }
                marqueeSpeed = value;
                if (!DesignMode) {
                    StartMarquee();
                }
                this.LogWF2WPropertyValue("MarqueeAnimationSpeed", value);
            }
        }

        /// <include file='doc\ProgressBar.uex' path='docs/doc[@for="ProgressBar.Maximum"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Start the Marquee rolling (or stop it, if the speed = 0)
        ///    </para>
        /// </devdoc>
        private void StartMarquee()
        {
            if (IsHandleCreated && style == ProgressBarStyle.Marquee)
            {
                if (marqueeSpeed == 0)
                {
                    SendMessage(WinFormNativeMethods.PBM_SETMARQUEE, 0, marqueeSpeed);
                }
                else
                {
                    SendMessage(WinFormNativeMethods.PBM_SETMARQUEE, 1, marqueeSpeed);
                }
            }
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public int Maximum {
            get {
                return maximum;
            }
            set {
                if (maximum != value) {
                    // Ensure that value is in the Win32 control's acceptable range
                    // Message: '%1' is not a valid value for '%0'. '%0' must be greater than %2.
                    // Should this set a boundary for the top end too?
                    if (value < 0)
                        throw new ArgumentOutOfRangeException("Maximum", DCSR.GetString(DCSR.InvalidLowBoundArgumentEx, "Maximum", value.ToString(CultureInfo.CurrentCulture), (0).ToString(CultureInfo.CurrentCulture)));

                    if (minimum > value) minimum = value;

                    maximum = value;

                    if (this.value > maximum) this.value = maximum;

                    if (IsHandleCreated) {
                        SendMessage(WinFormNativeMethods.PBM_SETRANGE32, minimum, maximum);
                        UpdatePos() ;
                    }
                    this.LogWF2WPropertyValue("Maximum", value );
                }
            }
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public int Minimum {
            get {
                return minimum;
            }
            set {
                if (minimum != value) {
                    // Ensure that value is in the Win32 control's acceptable range
                    // Message: '%1' is not a valid value for '%0'. '%0' must be greater than %2.
                    // Should this set a boundary for the top end too?
                    if (value < 0)
                        throw new ArgumentOutOfRangeException("Minimum", DCSR.GetString(DCSR.InvalidLowBoundArgumentEx, "Minimum", value.ToString(CultureInfo.CurrentCulture), (0).ToString(CultureInfo.CurrentCulture)));
                    if (maximum < value) maximum = value;

                    minimum = value;

                    if (this.value < minimum) this.value = minimum;

                    if (IsHandleCreated) {
                        SendMessage(WinFormNativeMethods.PBM_SETRANGE32, minimum, maximum);
                        UpdatePos() ;
                    }
                    this.LogWF2WPropertyValue("Minimum", value );
                }
            }
        }

        protected override void OnBackColorChanged(EventArgs e)
        {
            base.OnBackColorChanged(e);
            if (IsHandleCreated)
            {
                WinFormUnsafeNativeMethods.SendMessage(new HandleRef(this, Handle), WinFormNativeMethods.PBM_SETBKCOLOR, 0, ColorTranslator.ToWin32(BackColor));
            }
        }

        protected override void OnForeColorChanged(EventArgs e)
        {
            base.OnForeColorChanged(e);
            if (IsHandleCreated)
            {
                WinFormUnsafeNativeMethods.SendMessage(new HandleRef(this, Handle), WinFormNativeMethods.PBM_SETBARCOLOR, 0, ColorTranslator.ToWin32(ForeColor));
            }
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public new Padding Padding {
            get { return base.Padding; }
            set { base.Padding = value;}
        }

        [
        Browsable(false),
        EditorBrowsable(EditorBrowsableState.Never)
        ]
        public new event EventHandler PaddingChanged {
            add { base.PaddingChanged += value; }
            remove { base.PaddingChanged -= value; }
        }

        /// <include file='doc\ProgressBar.uex' path='docs/doc[@for="ProgressBar.RightToLeftLayout"]/*' />
        /// <devdoc>
        ///     This is used for international applications where the language
        ///     is written from RightToLeft. When this property is true,
        //      and the RightToLeft is true, mirroring will be turned on on the form, and
        ///     control placement and text will be from right to left.
        /// </devdoc>
        [
        SRCategory(DCSR.CatAppearance),
        Localizable(true),
        DefaultValue(false),
        SRDescription(DCSR.ControlRightToLeftLayoutDescr)
        ]
        public virtual bool RightToLeftLayout {
            get {

                return rightToLeftLayout;
            }

            set {
                if (value != rightToLeftLayout) {
                    rightToLeftLayout = value;
                    using(new LayoutTransaction(this, this, PropertyNames.RightToLeftLayout)) {
                        OnRightToLeftLayoutChanged(EventArgs.Empty);
                    }
                }
            }
        }


        /// <include file='doc\Form.uex' path='docs/doc[@for="Form.RightToLeftLayoutChanged"]/*' />
        /// <devdoc>
        ///    <para>[To be supplied.]</para>
        /// </devdoc>
        [SRCategory(DCSR.CatPropertyChanged), SRDescription(DCSR.ControlOnRightToLeftLayoutChangedDescr)]
        public event EventHandler RightToLeftLayoutChanged {
            add {
                onRightToLeftLayoutChanged += value;
            }
            remove {
                onRightToLeftLayoutChanged -= value;
            }
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public int Step {
            get {
                return step;
            }
            set {
                step = value;
                if (IsHandleCreated) SendMessage(WinFormNativeMethods.PBM_SETSTEP, step, 0);
                this.LogWF2WPropertyValue("Step", value );
            }
        }

        /// <include file='doc\ProgressBar.uex' path='docs/doc[@for="ProgressBar.TabStop"]/*' />
        /// <internalonly/>
        /// <devdoc>
        /// </devdoc>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        new public bool TabStop {
            get {
                return base.TabStop;
            }
            set {
                base.TabStop = value;
            }
        }

        /// <include file='doc\ProgressBar.uex' path='docs/doc[@for="ProgressBar.TabStopChanged"]/*' />
        /// <internalonly/>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        new public event EventHandler TabStopChanged {
            add {
                base.TabStopChanged += value;
            }
            remove {
                base.TabStopChanged -= value;
            }
        }

        /// <include file='doc\ProgressBar.uex' path='docs/doc[@for="ProgressBar.Text"]/*' />
        /// <internalonly/>
        /// <devdoc>
        /// </devdoc>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), Bindable(false)]
        public override string Text {
            get {
                return base.Text;
            }
            set {
                base.Text = value;
            }
        }

        /// <include file='doc\ProgressBar.uex' path='docs/doc[@for="ProgressBar.TextChanged"]/*' />
        /// <internalonly/>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        new public event EventHandler TextChanged {
            add {
                base.TextChanged += value;
            }
            remove {
                base.TextChanged -= value;
            }
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public int Value {
            get {
                return value;
            }
            set {
                if (this.value != value) {
                    if ((value < minimum) || (value > maximum))
                        throw new ArgumentOutOfRangeException("Value", DCSR.GetString(DCSR.InvalidBoundArgument, "Value", value.ToString(CultureInfo.CurrentCulture), "'minimum'", "'maximum'"));
                    this.value = value;
                    UpdatePos() ;
                    this.LogWF2WPropertyValue("Value", value );
                }
            }
        }

        /// <include file='doc\ProgressBar.uex' path='docs/doc[@for="ProgressBar.DoubleClick"]/*' />
        /// <internalonly/><hideinheritance/>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public new event EventHandler DoubleClick {
            add {
                base.DoubleClick += value;
            }
            remove {
                base.DoubleClick -= value;
            }
        }

        /// <include file='doc\ProgressBar.uex' path='docs/doc[@for="ProgressBar.MouseDoubleClick"]/*' />
        /// <internalonly/><hideinheritance/>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public new event MouseEventHandler MouseDoubleClick {
            add {
                base.MouseDoubleClick += value;
            }
            remove {
                base.MouseDoubleClick -= value;
            }
        }

        /// <include file='doc\ProgressBar.uex' path='docs/doc[@for="ProgressBar.KeyUp"]/*' />
        /// <internalonly/><hideinheritance/>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public new event KeyEventHandler KeyUp {
            add {
                base.KeyUp += value;
            }
            remove {
                base.KeyUp -= value;
            }
        }

        /// <include file='doc\ProgressBar.uex' path='docs/doc[@for="ProgressBar.KeyDown"]/*' />
        /// <internalonly/><hideinheritance/>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public new event KeyEventHandler KeyDown {
            add {
                base.KeyDown += value;
            }
            remove {
                base.KeyDown -= value;
            }
        }

        /// <include file='doc\ProgressBar.uex' path='docs/doc[@for="ProgressBar.KeyPress"]/*' />
        /// <internalonly/><hideinheritance/>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public new event KeyPressEventHandler KeyPress {
            add {
                base.KeyPress += value;
            }
            remove {
                base.KeyPress -= value;
            }
        }

        /// <include file='doc\ProgressBar.uex' path='docs/doc[@for="ProgressBar.Enter"]/*' />
        /// <internalonly/>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public new event EventHandler Enter {
            add {
                base.Enter += value;
            }
            remove {
                base.Enter -= value;
            }
        }

        /// <include file='doc\ProgressBar.uex' path='docs/doc[@for="ProgressBar.Leave"]/*' />
        /// <internalonly/>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public new event EventHandler Leave {
            add {
                base.Leave += value;
            }
            remove {
                base.Leave -= value;
            }
        }

        /// <include file='doc\ProgressBar.uex' path='docs/doc[@for="ProgressBar.OnPaint"]/*' />
        /// <devdoc>
        ///     ProgressBar Onpaint.
        /// </devdoc>
        /// <internalonly/><hideinheritance/>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public new event PaintEventHandler Paint {
            add {
                base.Paint += value;
            }
            remove {
                base.Paint -= value;
            }
        }


        /// <include file='doc\ProgressBar.uex' path='docs/doc[@for="ProgressBar.CreateHandle"]/*' />
        /// <devdoc>
        /// </devdoc>
        /// <internalonly/>
        protected override void CreateHandle() {
            if (!RecreatingHandle) {
                IntPtr userCookie = WinFormUnsafeNativeMethods.ThemingScope.Activate();   
                try {
                    WinFormNativeMethods.INITCOMMONCONTROLSEX icc = new WinFormNativeMethods.INITCOMMONCONTROLSEX();
                    icc.dwICC = WinFormNativeMethods.ICC_PROGRESS_CLASS;
                    WinFormSafeNativeMethods.InitCommonControlsEx(icc);
                }
                finally {
                    WinFormUnsafeNativeMethods.ThemingScope.Deactivate(userCookie);
                }
            }
            base.CreateHandle();
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void Increment(int value) {
            if (this.Style == ProgressBarStyle.Marquee) {
                throw new InvalidOperationException(DCSR.GetString(DCSR.ProgressBarIncrementMarqueeException));
            }
            this.value += value;

            // Enforce that value is within the range (minimum, maximum)
            if (this.value < minimum) {
                this.value = minimum;
            }
            if (this.value > maximum) {
                this.value = maximum;
            }

            UpdatePos();
        }

        /// <include file='doc\ProgressBar.uex' path='docs/doc[@for="ProgressBar.OnHandleCreated"]/*' />
        /// <devdoc>
        ///    Overridden to set up our properties.
        /// </devdoc>
        /// <internalonly/>
        protected override void OnHandleCreated(EventArgs e) {
            base.OnHandleCreated(e);
            SendMessage(WinFormNativeMethods.PBM_SETRANGE32, minimum, maximum);
            SendMessage(WinFormNativeMethods.PBM_SETSTEP, step, 0);
            SendMessage(WinFormNativeMethods.PBM_SETPOS, value, 0);
            WinFormUnsafeNativeMethods.SendMessage(new HandleRef(this, Handle), WinFormNativeMethods.PBM_SETBKCOLOR, 0, ColorTranslator.ToWin32(BackColor));
            WinFormUnsafeNativeMethods.SendMessage(new HandleRef(this, Handle), WinFormNativeMethods.PBM_SETBARCOLOR, 0, ColorTranslator.ToWin32(ForeColor));
            StartMarquee();
            SystemEvents.UserPreferenceChanged += new UserPreferenceChangedEventHandler(UserPreferenceChangedHandler);
        }

        /// <include file='doc\ProgressBar.uex' path='docs/doc[@for="ProgressBar.OnHandleDestroyed"]/*' />
        /// <devdoc>
        ///    Overridden to remove event handler.
        /// </devdoc>
        /// <internalonly/>  
        protected override void OnHandleDestroyed(EventArgs e)
        {
            SystemEvents.UserPreferenceChanged -= new UserPreferenceChangedEventHandler(UserPreferenceChangedHandler);
            base.OnHandleDestroyed(e);
        }

        /// <include file='doc\Form.uex' path='docs/doc[@for="Form.OnRightToLeftLayoutChanged"]/*' />
        /// <devdoc>
        ///    <para>[To be supplied.]</para>
        /// </devdoc>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnRightToLeftLayoutChanged(EventArgs e) {
            if (GetAnyDisposingInHierarchy()) {
                return;
            }

            if (RightToLeft == RightToLeft.Yes) {
                RecreateHandle();
            }

            if (onRightToLeftLayoutChanged != null) {
                 onRightToLeftLayoutChanged(this, e);
            }
        }



        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void PerformStep() {
            if (this.Style == ProgressBarStyle.Marquee) {
                throw new InvalidOperationException(DCSR.GetString(DCSR.ProgressBarPerformStepMarqueeException));
            }
            Increment(step);
        }

        /// <include file='doc\Control.uex' path='docs/doc[@for="Control.ResetForeColor"]/*' />
        /// <devdoc>
        ///     Resets the fore color to be based on the parent's fore color.
        /// </devdoc>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override void ResetForeColor() {
            ForeColor = defaultForeColor;
        }


        /// <include file='doc\ProgressBar.uex' path='docs/doc[@for="ProgressBar.ShouldSerializeForeColor"]/*' />
        /// <devdoc>
        ///     Returns true if the ForeColor should be persisted in code gen.
        /// </devdoc>
        [EditorBrowsable(EditorBrowsableState.Never)]
        internal override bool ShouldSerializeForeColor() {
            return ForeColor != defaultForeColor;
        }


        /// <include file='doc\ProgressBar.uex' path='docs/doc[@for="ProgressBar.ToString"]/*' />
        /// <devdoc>
        ///    Returns a string representation for this control.
        /// </devdoc>
        /// <internalonly/>
        public override string ToString() {

            string s = base.ToString();
            return s + ", Minimum: " + Minimum.ToString(CultureInfo.CurrentCulture) + ", Maximum: " + Maximum.ToString(CultureInfo.CurrentCulture) + ", Value: " + Value.ToString(CultureInfo.CurrentCulture);
        }

        /// <include file='doc\ProgressBar.uex' path='docs/doc[@for="ProgressBar.UpdatePos"]/*' />
        /// <devdoc>
        ///     Sends the underlying window a PBM_SETPOS message to update
        ///     the current value of the progressbar.
        /// </devdoc>
        /// <internalonly/>
        private void UpdatePos() {
            if (IsHandleCreated) SendMessage(WinFormNativeMethods.PBM_SETPOS, value, 0);
        }

        //Note: ProgressBar doesn't work like other controls as far as setting ForeColor/
        //BackColor -- you need to send messages to update the colors 
        private void UserPreferenceChangedHandler(object o, UserPreferenceChangedEventArgs e)
        {
            if (IsHandleCreated)
            {
                WinFormUnsafeNativeMethods.SendMessage(new HandleRef(this, Handle), WinFormNativeMethods.PBM_SETBARCOLOR, 0, ColorTranslator.ToWin32(ForeColor));
                WinFormUnsafeNativeMethods.SendMessage(new HandleRef(this, Handle), WinFormNativeMethods.PBM_SETBKCOLOR, 0, ColorTranslator.ToWin32(BackColor));
            }
        }
    }
}

