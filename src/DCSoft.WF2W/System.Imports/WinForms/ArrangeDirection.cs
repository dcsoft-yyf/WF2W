//------------------------------------------------------------------------------
// <copyright file="ArrangeDirection.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>                                                                
//------------------------------------------------------------------------------

/*
* Copyright (c) 1997, Microsoft Corporation. All Rights Reserved.
* Information Contained Herein is Proprietary and Confidential.
*/
namespace System.Windows.Forms {

    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;

    /// <include file='doc\ArrangeDirection.uex' path='docs/doc[@for="ArrangeDirection"]/*' />
    /// <devdoc>
    ///    <para>
    ///       Specifies the direction the system uses to arrange
    ///       minimized windows.
    ///    </para>
    /// </devdoc>
    [System.Runtime.InteropServices.ComVisible(true)]
    [Flags]
    [
        SuppressMessage("Microsoft.Design", "CA1008:EnumsShouldHaveZeroValue")  // Maps to native enum.
    ]
    public enum ArrangeDirection {

        /// <include file='doc\ArrangeDirection.uex' path='docs/doc[@for="ArrangeDirection.Down"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Arranges vertically, from top to bottom.
        ///    </para>
        /// </devdoc>
        Down = WinFormNativeMethods.ARW_DOWN,

        /// <include file='doc\ArrangeDirection.uex' path='docs/doc[@for="ArrangeDirection.Left"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Arranges horizontally, from left to right.
        ///    </para>
        /// </devdoc>
        Left = WinFormNativeMethods.ARW_LEFT,

        /// <include file='doc\ArrangeDirection.uex' path='docs/doc[@for="ArrangeDirection.Right"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Arranges horizontally, from right to left.
        ///    </para>
        /// </devdoc>
        Right = WinFormNativeMethods.ARW_RIGHT,

        /// <include file='doc\ArrangeDirection.uex' path='docs/doc[@for="ArrangeDirection.Up"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Arranges vertically, from bottom to top.
        ///    </para>
        /// </devdoc>
        Up = WinFormNativeMethods.ARW_UP,
    }
}

