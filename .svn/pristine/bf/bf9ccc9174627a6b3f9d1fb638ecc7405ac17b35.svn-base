//------------------------------------------------------------------------------
// <copyright file="TreeViewHitTestLocations.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>                                                                
//------------------------------------------------------------------------------

namespace System.Windows.Forms {

    using System.Diagnostics;

    using System;
    using System.ComponentModel;
    using System.Drawing;
    using Microsoft.Win32;

    /// <include file='doc\TreeViewHitTestLocations.uex' path='docs/doc[@for="TreeViewHitTestLocations"]/*' />
    /// <devdoc>
    ///    <para>
    ///       Specifies the
    ///       return value for HITTEST on treeview.
    ///    </para>
    /// </devdoc>
    [
    Flags,
    System.Runtime.InteropServices.ComVisible(true)
    ]
    public enum TreeViewHitTestLocations {

        /// <include file='doc\TreeViewHitTestLocations.uex' path='docs/doc[@for="TreeViewHitTestLocations.None"]/*' />
        /// <devdoc>
        ///    <para>
        ///       No Information.
        ///    </para>
        /// </devdoc>
        None = WinFormNativeMethods.TVHT_NOWHERE,

        /// <include file='doc\TreeViewHitTestLocations.uex' path='docs/doc[@for="TreeViewHitTestLocations.Image"]/*' />
        /// <devdoc>
        ///    <para>
        ///       On Image.
        ///    </para>
        /// </devdoc>
        Image = WinFormNativeMethods.TVHT_ONITEMICON,

        /// <include file='doc\TreeViewHitTestLocations.uex' path='docs/doc[@for="TreeViewHitTestLocations.Label"]/*' />
        /// <devdoc>
        ///    <para>
        ///       On Label.
        ///    </para>
        /// </devdoc>
        Label = WinFormNativeMethods.TVHT_ONITEMLABEL,

        /// <include file='doc\TreeViewHitTestLocations.uex' path='docs/doc[@for="TreeViewHitTestLocations.Indent"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Indent.
        ///    </para>
        /// </devdoc>
        Indent = WinFormNativeMethods.TVHT_ONITEMINDENT,

        /// <include file='doc\TreeViewHitTestLocations.uex' path='docs/doc[@for="TreeViewHitTestLocations.AboveClientArea"]/*' />
        /// <devdoc>
        ///    <para>
        ///       AboveClientArea.
        ///    </para>
        /// </devdoc>
        AboveClientArea =  WinFormNativeMethods.TVHT_ABOVE,

        /// <include file='doc\TreeViewHitTestLocations.uex' path='docs/doc[@for="TreeViewHitTestLocations.BelowClientArea"]/*' />
        /// <devdoc>
        ///    <para>
        ///       BelowClientArea.
        ///    </para>
        /// </devdoc>
        BelowClientArea = WinFormNativeMethods.TVHT_BELOW,

        /// <include file='doc\TreeViewHitTestInfo.uex' path='docs/doc[@for="TreeViewHitTestInfo.LeftOfClientArea"]/*' />
        /// <devdoc>
        ///    <para>
        ///       LeftOfClientArea.
        ///    </para>
        /// </devdoc>
        LeftOfClientArea = WinFormNativeMethods.TVHT_TOLEFT,

        /// <include file='doc\TreeViewHitTestLocations.uex' path='docs/doc[@for="TreeViewHitTestLocations.RightOfClientArea"]/*' />
        /// <devdoc>
        ///    <para>
        ///       RightOfClientArea.
        ///    </para>
        /// </devdoc>
        RightOfClientArea = WinFormNativeMethods.TVHT_TORIGHT,

        /// <include file='doc\TreeViewHitTestLocations.uex' path='docs/doc[@for="TreeViewHitTestLocations.RightOfNode"]/*' />
        /// <devdoc>
        ///    <para>
        ///       RightOfNode.
        ///    </para>
        /// </devdoc>
        RightOfLabel =   WinFormNativeMethods.TVHT_ONITEMRIGHT,

        /// <include file='doc\TreeViewHitTestLocations.uex' path='docs/doc[@for="TreeViewHitTestLocations.StateImage"]/*' />
        /// <devdoc>
        ///    <para>
        ///       StateImage.
        ///    </para>
        /// </devdoc>
        StateImage = WinFormNativeMethods.TVHT_ONITEMSTATEICON,

        /// <include file='doc\TreeViewHitTestLocations.uex' path='docs/doc[@for="TreeViewHitTestLocations.PlusMinus"]/*' />
        /// <devdoc>
        ///    <para>
        ///      PlusMinus.
        ///    </para>
        /// </devdoc>
        PlusMinus = WinFormNativeMethods.TVHT_ONITEMBUTTON,
    }
}

