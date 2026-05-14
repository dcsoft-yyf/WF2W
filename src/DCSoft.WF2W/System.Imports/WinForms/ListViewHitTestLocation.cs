//------------------------------------------------------------------------------
// <copyright file="ListViewHitTestLocations.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>                                                                
//------------------------------------------------------------------------------


namespace System.Windows.Forms {
    [
    Flags
    ]
    /// <include file='doc\ListViewHitTestLocations.uex' path='docs/doc[@for="ListViewHitTestLocations"]/*' />
    public enum ListViewHitTestLocations {
        /// <include file='doc\ListViewHitTestLocations.uex' path='docs/doc[@for="ListViewHitTestLocations.None"]/*' />
        None = WinFormNativeMethods.LVHT_NOWHERE,
        /// <include file='doc\ListViewHitTestLocations.uex' path='docs/doc[@for="ListViewHitTestLocations.AboveClientArea"]/*' />
        AboveClientArea = 0x0100,
        /// <include file='doc\ListViewHitTestLocations.uex' path='docs/doc[@for="ListViewHitTestLocations.BelowClientArea"]/*' />
        BelowClientArea = WinFormNativeMethods.LVHT_BELOW,
        /// <include file='doc\ListViewHitTestLocations.uex' path='docs/doc[@for="ListViewHitTestLocations.LeftOfClientArea"]/*' />
        LeftOfClientArea = WinFormNativeMethods.LVHT_LEFT,
        /// <include file='doc\ListViewHitTestLocations.uex' path='docs/doc[@for="ListViewHitTestLocations.RightOfClientArea"]/*' />
        RightOfClientArea = WinFormNativeMethods.LVHT_RIGHT,
        /// <include file='doc\ListViewHitTestLocations.uex' path='docs/doc[@for="ListViewHitTestLocations.Image"]/*' />
        Image = WinFormNativeMethods.LVHT_ONITEMICON,
        /// <include file='doc\ListViewHitTestLocations.uex' path='docs/doc[@for="ListViewHitTestLocations.StateImage"]/*' />
        StateImage = 0x0200, 
        /// <include file='doc\ListViewHitTestLocations.uex' path='docs/doc[@for="ListViewHitTestLocations.Label"]/*' />
        Label = WinFormNativeMethods.LVHT_ONITEMLABEL
    }
}
