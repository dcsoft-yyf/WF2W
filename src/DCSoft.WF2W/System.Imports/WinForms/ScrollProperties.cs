//------------------------------------------------------------------------------
// <copyright file="ScrollProperties.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>                                                                
//------------------------------------------------------------------------------

namespace System.Windows.Forms {
    using System.Runtime.InteropServices;
    using System.Diagnostics;
    using System;
    using System.Security.Permissions;
    using System.Runtime.Serialization.Formatters;
    using System.ComponentModel;
    using System.Drawing;
    using Microsoft.Win32;
    using System.Windows.Forms;
    using System.Globalization;
    
    /// <include file='doc\ScrollProperties.uex' path='docs/doc[@for="ScrollProperties"]/*' />
    /// <devdoc>
    ///    <para>
    ///       Basic Properties for Scrollbars.
    ///    </para>
    /// </devdoc>
    public abstract class ScrollProperties {

        internal int minimum = 0;
        internal int maximum = 100;
        internal int smallChange = 1;
        internal int largeChange = 10;
        internal int value = 0;
        internal bool maximumSetExternally;
        internal bool smallChangeSetExternally;
        internal bool largeChangeSetExternally;

        /// <include file='doc\ScrollProperties.uex' path='docs/doc[@for="ScrollProperties.parent"]/*' />
        private ScrollableControl parent;

        protected ScrollableControl ParentControl {
            get
            {
                return parent;
            }
        }
        
        /// <devdoc>
        ///     Number of pixels to scroll the client region as a "line" for autoscroll.
        /// </devdoc>
        /// <internalonly/>
        private const int SCROLL_LINE = 5;

        internal bool visible = false;

        //Always Enabled
        private bool enabled = true;


        /// <include file='doc\ScrollProperties.uex' path='docs/doc[@for="ScrollProperties.ScrollProperties"]/*' />
        protected ScrollProperties(ScrollableControl container)  {
            this.parent = container;
        }

        /// <include file='doc\ScrollProperties.uex' path='docs/doc[@for="ScrollProperties.Enabled"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Gets or sets a bool value controlling whether the scrollbar is enabled.
        ///    </para>
        /// </devdoc>
        [
        SRCategory(DCSR.CatBehavior),
        DefaultValue(true),
        SRDescription(DCSR.ScrollBarEnableDescr)
        ]
        public bool Enabled {
            get {
                return enabled;
            }
            set {
                if (parent.AutoScroll) {
                    return;
                }
                if (value != enabled) {
                    enabled = value;
                    EnableScroll(value);
                }
            }
        }


        /// <include file='doc\ScrollProperties.uex' path='docs/doc[@for="ScrollProperties.LargeChange"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Gets or sets a value to be added or subtracted to the <see cref='System.Windows.Forms.ScrollProperties.LargeChange'/>
        ///       property when the scroll box is moved a large distance.
        ///    </para>
        /// </devdoc>
        [
        SRCategory(DCSR.CatBehavior),
        DefaultValue(10),
        SRDescription(DCSR.ScrollBarLargeChangeDescr),
        RefreshProperties(RefreshProperties.Repaint)
        ]
        public int LargeChange {
            get {
                // We preserve the actual large change value that has been set, but when we come to
                // get the value of this property, make sure it's within the maximum allowable value.
                // This way we ensure that we don't depend on the order of property sets when
                // code is generated at design-time.
                //
                return Math.Min(largeChange, maximum - minimum + 1);                
            }
            set {
                if (largeChange != value ) {
                    if (value < 0) {                
                        throw new ArgumentOutOfRangeException("LargeChange", DCSR.GetString(DCSR.InvalidLowBoundArgumentEx, "LargeChange", (value).ToString(CultureInfo.CurrentCulture), (0).ToString(CultureInfo.CurrentCulture)));
                    }
                    largeChange = value;
                    largeChangeSetExternally = true;
                    UpdateScrollInfo();
                }
            }
        }

        /// <include file='doc\ScrollProperties.uex' path='docs/doc[@for="ScrollProperties.Maximum"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Gets or sets the upper limit of values of the scrollable range.
        ///    </para>
        /// </devdoc>
        [
        SRCategory(DCSR.CatBehavior),
        DefaultValue(100),
        SRDescription(DCSR.ScrollBarMaximumDescr),
        RefreshProperties(RefreshProperties.Repaint)
        ]
        public int Maximum {
            get {
                return maximum;
            }
            set {
                if (parent.AutoScroll) {
                    return;
                }
                if (maximum != value) {
                    if (minimum > value)
                        minimum = value;
                    // bring this.value in line.
                    if (value < this.value)
                        Value = value;
                    maximum = value;
                    maximumSetExternally = true;
                    UpdateScrollInfo();
                }
            }
        }

        /// <include file='doc\ScrollProperties.uex' path='docs/doc[@for="ScrollProperties.Minimum"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Gets or sets the lower limit of values of the scrollable range.
        ///    </para>
        /// </devdoc>
        [
        SRCategory(DCSR.CatBehavior),
        DefaultValue(0),
        SRDescription(DCSR.ScrollBarMinimumDescr),
        RefreshProperties(RefreshProperties.Repaint)
        ]
        public int Minimum {
            get {
                return minimum;
            }

            set {
                if (parent.AutoScroll) {
                    return;
                }
                if (minimum != value) {
                    if (value < 0) {                
                        throw new ArgumentOutOfRangeException("Minimum", DCSR.GetString(DCSR.InvalidLowBoundArgumentEx, "Minimum", (value).ToString(CultureInfo.CurrentCulture), (0).ToString(CultureInfo.CurrentCulture)));
                    }
                    if (maximum < value)
                        maximum = value;
                    // bring this.value in line.
                    if (value > this.value)
                        this.value = value;
                    minimum = value;
                    UpdateScrollInfo();
                }
            }
        }

        internal abstract int PageSize  {
            get;
        }

        internal abstract int Orientation  {
            get;
        }

        internal abstract int HorizontalDisplayPosition  {
            get;
        }


        internal abstract int VerticalDisplayPosition  {
            get;
        }

        /// <include file='doc\ScrollProperties.uex' path='docs/doc[@for="ScrollProperties.SmallChange"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Gets or sets the value to be added or subtracted to the
        ///    <see cref='System.Windows.Forms.ScrollBar.Value'/> 
        ///    property when the scroll box is
        ///    moved a small distance.
        /// </para>
        /// </devdoc>
        [
        SRCategory(DCSR.CatBehavior),
        DefaultValue(1),
        SRDescription(DCSR.ScrollBarSmallChangeDescr)
        ]
        public int SmallChange {
            get {
                // We can't have SmallChange > LargeChange, but we shouldn't manipulate
                // the set values for these properties, so we just return the smaller 
                // value here. 
                //
                return Math.Min(smallChange, LargeChange);
            }
            set {
                if (smallChange != value) {
                
                    if (value < 0) {                
                        throw new ArgumentOutOfRangeException("SmallChange", DCSR.GetString(DCSR.InvalidLowBoundArgumentEx, "SmallChange", (value).ToString(CultureInfo.CurrentCulture), (0).ToString(CultureInfo.CurrentCulture)));
                    }
                
                    smallChange = value;
                    smallChangeSetExternally = true;
                    UpdateScrollInfo();
                }
            }
        }

        /// <include file='doc\ScrollProperties.uex' path='docs/doc[@for="ScrollProperties.Value"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Gets or sets a numeric value that represents the current
        ///       position of the scroll box
        ///       on
        ///       the scroll bar control.
        ///    </para>
        /// </devdoc>
        [
        SRCategory(DCSR.CatBehavior),
        DefaultValue(0),
        Bindable(true),
        SRDescription(DCSR.ScrollBarValueDescr)
        ]
        public int Value {
            get {
                return value;
            }
            set {
                if (this.value != value) {
                    if (value < minimum || value > maximum) {
                        throw new ArgumentOutOfRangeException("Value", DCSR.GetString(DCSR.InvalidBoundArgument, "Value", (value).ToString(CultureInfo.CurrentCulture), "'minimum'", "'maximum'"));
                    }
                    this.value = value;
                    UpdateScrollInfo();
                    parent.SetDisplayFromScrollProps(HorizontalDisplayPosition, VerticalDisplayPosition);
                }
            }
        }


        /// <include file='doc\ScrollProperties.uex' path='docs/doc[@for="ScrollProperties.Visible"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Gets or sets a bool value controlling whether the scrollbar is showing.
        ///    </para>
        /// </devdoc>
        [
        SRCategory(DCSR.CatBehavior),
        DefaultValue(false),
        SRDescription(DCSR.ScrollBarVisibleDescr)
        ]
        public bool Visible {
            get {
                return visible;
            }
            set {
                if (parent.AutoScroll) {
                    return;
                }
                if (value != visible) {
                    visible = value;
                    parent.UpdateStylesCore ();
                    UpdateScrollInfo();
                    parent.SetDisplayFromScrollProps(HorizontalDisplayPosition, VerticalDisplayPosition);
                }
            }
        }
        
        /// <include file='doc\ScrollProperties.uex' path='docs/doc[@for="ScrollProperties.UpdateScrollInfo"]/*' />
        /// <devdoc>
        ///     Internal helper method
        /// </devdoc>
        /// <internalonly/>
        internal void UpdateScrollInfo() {
            if (parent.IsHandleCreated && visible) {
                WinFormNativeMethods.SCROLLINFO si = new WinFormNativeMethods.SCROLLINFO();
                si.cbSize = Marshal.SizeOf(typeof(WinFormNativeMethods.SCROLLINFO));
                si.fMask = WinFormNativeMethods.SIF_ALL;
                si.nMin = minimum;
                si.nMax = maximum;
                si.nPage = (parent.AutoScroll) ? PageSize : LargeChange;
                si.nPos = value;
                si.nTrackPos = 0;
                WinFormUnsafeNativeMethods.SetScrollInfo(new HandleRef(parent, parent.Handle), Orientation, si, true);
            }
        }

        /// <include file='doc\ScrollProperties.uex' path='docs/doc[@for="ScrollProperties.EnableScroll"]/*' />
        /// <devdoc>
        ///     Internal helper method for enabling or disabling the Vertical Scroll bar.
        /// </devdoc>
        /// <internalonly/>
        private void EnableScroll(bool enable)
        {
            if (enable) {
                WinFormUnsafeNativeMethods.EnableScrollBar(new HandleRef(parent, parent.Handle), Orientation, WinFormNativeMethods.ESB_ENABLE_BOTH);
            }
            else {
                WinFormUnsafeNativeMethods.EnableScrollBar(new HandleRef(parent, parent.Handle), Orientation, WinFormNativeMethods.ESB_DISABLE_BOTH);
            }
            
        }
        
    }
}

