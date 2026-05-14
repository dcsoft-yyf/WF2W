//------------------------------------------------------------------------------
// <copyright file="ToolStripProgressBar.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>                                                                
//------------------------------------------------------------------------------

namespace System.Windows.Forms {

    using System;
    using System.Windows.Forms;
    using System.ComponentModel;
    using System.Drawing;
    using System.Security;
    using System.Security.Permissions;

    /// <include file='doc\ToolStripProgressBar.uex' path='docs/doc[@for="ToolStripProgressBar"]/*' />
    [DefaultProperty("Value")]
    public class ToolStripProgressBar : ToolStripControlHost {

        internal static readonly object EventRightToLeftLayoutChanged = new object();

        /// <include file='doc\ToolStripProgressBar.uex' path='docs/doc[@for="ToolStripProgressBar.ToolStripProgressBar"]/*' />
        public ToolStripProgressBar()
            : base(CreateControlInstance()) {
        }

        public ToolStripProgressBar(string name) : this() {
            this.Name = name;
        }

        /// <include file='doc\ToolStripProgressBar.uex' path='docs/doc[@for="ToolStripProgressBar.ProgressBar"]/*' />
        /// <devdoc>
        /// Create a strongly typed accessor for the class
        /// </devdoc>
        /// <value></value>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ProgressBar ProgressBar {
            get {
                return this.Control as ProgressBar;
            }
        }

        [
        Browsable(false),
        EditorBrowsable(EditorBrowsableState.Never),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
        ]
        public override Image BackgroundImage {
            get {
                return base.BackgroundImage;
            }
            set {
                base.BackgroundImage = value;
            }
        }

        [
        Browsable(false),
        EditorBrowsable(EditorBrowsableState.Never),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)
        ]
        public override ImageLayout BackgroundImageLayout {
            get {
                return base.BackgroundImageLayout;
            }
            set {
                base.BackgroundImageLayout = value;
            }
        }

        /// <include file='doc\ToolStripProgressBar.uex' path='docs/doc[@for="ToolStripProgressBar.DefaultSize"]/*' />
        /// <devdoc>
        /// Specify what size you want the item to start out at
        /// </devdoc>
        /// <value></value>
        protected override System.Drawing.Size DefaultSize {
            get {              
                return new Size(100,15);
            }
        }

        /// <include file='doc\ToolStripProgressBar.uex' path='docs/doc[@for="ToolStripProgressBar.DefaultMargin"]/*' />
        /// <devdoc>
        /// Specify how far from the edges you want to be
        /// </devdoc>
        /// <value></value>
        protected internal override Padding DefaultMargin {
            get {
                if (this.Owner != null && this.Owner is StatusStrip) {
                    return new Padding(1, 3, 1, 3);
                }
                else {
                    return new Padding(1, 2, 1, 1);
                }
            }
        }
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        [
        DefaultValue(100),
        SRCategory(DCSR.CatBehavior),
        SRDescription(DCSR.ProgressBarMarqueeAnimationSpeed)
        ]
        public int MarqueeAnimationSpeed {
            get { return ProgressBar.MarqueeAnimationSpeed; }
            set
            {
                ProgressBar.MarqueeAnimationSpeed = value; 
                this.LogWF2WPropertyValue("MarqueeAnimationSpeed", value);
            }
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        [
       DefaultValue(100),
       SRCategory(DCSR.CatBehavior),
       RefreshProperties(RefreshProperties.Repaint),
       SRDescription(DCSR.ProgressBarMaximumDescr)
       ]
        public int Maximum {
            get {
                return ProgressBar.Maximum;
            }
            set {
                ProgressBar.Maximum = value;
                this.LogWF2WPropertyValue("Maximum", value);
            }
        }
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        [
        DefaultValue(0),
        SRCategory(DCSR.CatBehavior),
        RefreshProperties(RefreshProperties.Repaint),
        SRDescription(DCSR.ProgressBarMinimumDescr)
        ]
        public int Minimum {
            get {
                return ProgressBar.Minimum;
            }
            set {
                ProgressBar.Minimum = value;
                this.LogWF2WPropertyValue("Minimum", value);
            }
        }

        /// <include file='doc\ToolStripProgressBar.uex' path='docs/doc[@for="ToolStripProgressBar.RightToLeftLayout"]/*' />
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

                return ProgressBar.RightToLeftLayout;
            }

            set {
                ProgressBar.RightToLeftLayout = value;
            }
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        [
        DefaultValue(10),
        SRCategory(DCSR.CatBehavior),
        SRDescription(DCSR.ProgressBarStepDescr)
        ]
        public int Step {
            get {
                return ProgressBar.Step;
            }
            set {
                ProgressBar.Step = value;
                this.LogWF2WPropertyValue("Step", value);
            }
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        [
        DefaultValue(ProgressBarStyle.Blocks),
        SRCategory(DCSR.CatBehavior),
        SRDescription(DCSR.ProgressBarStyleDescr)
        ]
        public ProgressBarStyle Style {
            get {
                return ProgressBar.Style;
            }
            set {
                ProgressBar.Style = value;
                this.LogWF2WPropertyValue("Style", value);
            }
        }

        /// <include file='doc\ToolStripControlHost.uex' path='docs/doc[@for="ToolStripControlHost.Text"]/*' />
        /// <devdoc>
        /// Hide the property.
        /// </devdoc>
        [
        Browsable(false), 
        EditorBrowsable(EditorBrowsableState.Never), 
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)
        ]
        public override string Text
        {
            get
            {
                return Control.Text;
            }
            set
            {
                Control.Text = value;
            }
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        [
        DefaultValue(0),
        SRCategory(DCSR.CatBehavior),
        Bindable(true),
        SRDescription(DCSR.ProgressBarValueDescr)
        ]
        public int Value {
            get {
                return ProgressBar.Value;
            }
            set {
                ProgressBar.Value = value;
                this.LogWF2WPropertyValue("Value", value);
            }
        }


        private static Control CreateControlInstance() {
            ProgressBar progressBar = new ProgressBar();
            progressBar.Size = new Size(100,15);
            return progressBar;
        }

        private void HandleRightToLeftLayoutChanged(object sender, EventArgs e) {
            OnRightToLeftLayoutChanged(e);
        }

        /// <include file='doc\ToolStripProgressBar.uex' path='docs/doc[@for="ToolStripProgressBar.OnRightToLeftLayoutChanged"]/*' />
        protected virtual void OnRightToLeftLayoutChanged(EventArgs e) {
            RaiseEvent(EventRightToLeftLayoutChanged, e);
        }

        /// <include file='doc\ToolStripProgressBar.uex' path='docs/doc[@for="ToolStripProgressBar.OnSubscribeControlEvents"]/*' />
        protected override void OnSubscribeControlEvents(Control control) {
            ProgressBar bar = control as ProgressBar;
            if (bar != null) {
                // Please keep this alphabetized and in [....] with Unsubscribe
                // 
                bar.RightToLeftLayoutChanged += new EventHandler(HandleRightToLeftLayoutChanged);
            }

            base.OnSubscribeControlEvents(control);
        }

        /// <include file='doc\ToolStripProgressBar.uex' path='docs/doc[@for="ToolStripProgressBar.OnUnsubscribeControlEvents"]/*' />
        protected override void OnUnsubscribeControlEvents(Control control) {

            ProgressBar bar = control as ProgressBar;
            if (bar != null) {
                // Please keep this alphabetized and in [....] with Subscribe
                // 
                bar.RightToLeftLayoutChanged -= new EventHandler(HandleRightToLeftLayoutChanged);
            }
            base.OnUnsubscribeControlEvents(control);

        }

        /// <include file='doc\ToolStripProgressBar.uex' path='docs/doc[@for="ToolStripProgressBar.Paint"]/*' />
        /// <devdoc>
        /// <para>Hide the event.</para>
        /// </devdoc>
        [
        Browsable(false),
        EditorBrowsable(EditorBrowsableState.Never)
        ]
        new public event KeyEventHandler KeyDown
        {
            add
            {
                base.KeyDown += value;
            }
            remove
            {
                base.KeyDown -= value;
            }
        }

        /// <include file='doc\ToolStripProgressBar.uex' path='docs/doc[@for="ToolStripProgressBar.Paint"]/*' />
        /// <devdoc>
        /// <para>Hide the event.</para>
        /// </devdoc>
        [
        Browsable(false),
        EditorBrowsable(EditorBrowsableState.Never)
        ]
        new public event KeyPressEventHandler KeyPress
        {
            add
            {
                base.KeyPress += value;
            }
            remove
            {
                base.KeyPress -= value;
            }
        }

        /// <include file='doc\ToolStripProgressBar.uex' path='docs/doc[@for="ToolStripProgressBar.Paint"]/*' />
        /// <devdoc>
        /// <para>Hide the event.</para>
        /// </devdoc>
        [
        Browsable(false),
        EditorBrowsable(EditorBrowsableState.Never)
        ]
        new public event KeyEventHandler KeyUp
        {
            add
            {
                base.KeyUp += value;
            }
            remove
            {
                base.KeyUp -= value;
            }
        }
        /// <include file='doc\ToolStripProgressBar.uex' path='docs/doc[@for="ToolStripProgressBar.Paint"]/*' />
        /// <devdoc>
        /// <para>Hide the event.</para>
        /// </devdoc>
        [
        Browsable(false),
        EditorBrowsable(EditorBrowsableState.Never)
        ]
        new public event EventHandler LocationChanged
        {
            add
            {
                base.LocationChanged += value;
            }
            remove
            {
                base.LocationChanged -= value;
            }
        }

        /// <include file='doc\ToolStripProgressBar.uex' path='docs/doc[@for="ToolStripProgressBar.Paint"]/*' />
        /// <devdoc>
        /// <para>Hide the event.</para>
        /// </devdoc>
        [
        Browsable(false),
        EditorBrowsable(EditorBrowsableState.Never)
        ]
        new public event EventHandler OwnerChanged
        {
            add
            {
                base.OwnerChanged += value;
            }
            remove
            {
                base.OwnerChanged -= value;
            }
        }

        /// <include file='doc\ToolStripProgressBar.uex' path='docs/doc[@for="ToolStripProgressBar.RightToLeftLayoutChanged"]/*' />
        [SRCategory(DCSR.CatPropertyChanged), SRDescription(DCSR.ControlOnRightToLeftLayoutChangedDescr)]
        public event EventHandler RightToLeftLayoutChanged {
            add {
                Events.AddHandler(EventRightToLeftLayoutChanged, value);
            }
            remove {
                Events.RemoveHandler(EventRightToLeftLayoutChanged, value);
            }
        }

        /// <include file='doc\ToolStripProgressBar.uex' path='docs/doc[@for="ToolStripProgressBar.Paint"]/*' />
        /// <devdoc>
        /// <para>Hide the event.</para>
        /// </devdoc>
        [
        Browsable(false),
        EditorBrowsable(EditorBrowsableState.Never)
        ]
        new public event EventHandler TextChanged
        {
            add
            {
                base.TextChanged += value;
            }
            remove
            {
                base.TextChanged -= value;
            }
        }


        /// <include file='doc\ToolStripProgressBar.uex' path='docs/doc[@for="ToolStripProgressBar.Paint"]/*' />
        /// <devdoc>
        /// <para>Hide the event.</para>
        /// </devdoc>
        [
        Browsable(false),
        EditorBrowsable(EditorBrowsableState.Never)
        ]
        new public event EventHandler Validated
        {
            add
            {
                base.Validated += value;
            }
            remove
            {
                base.Validated -= value;
            }
        }

        /// <include file='doc\ToolStripProgressBar.uex' path='docs/doc[@for="ToolStripProgressBar.Paint"]/*' />
        /// <devdoc>
        /// <para>Hide the event.</para>
        /// </devdoc>
        [
        Browsable(false),
        EditorBrowsable(EditorBrowsableState.Never)
        ]
        new public event CancelEventHandler Validating
        {
            add
            {
                base.Validating += value;
            }
            remove
            {
                base.Validating -= value;
            }
        }
        public void Increment(int value) {
            ProgressBar.Increment(value);
        }

        public void PerformStep() {
            ProgressBar.PerformStep();
        }

    }
}
