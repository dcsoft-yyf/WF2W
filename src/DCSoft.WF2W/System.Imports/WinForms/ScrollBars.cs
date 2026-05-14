//------------------------------------------------------------------------------
// <copyright file="ScrollBars.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>                                                                
//------------------------------------------------------------------------------

namespace System.Windows.Forms {

    using System.Diagnostics;

    using System;
    using System.ComponentModel;
    using System.Drawing;
    using Microsoft.Win32;


    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true )]
    public enum ScrollBars {

        /// <include file='doc\ScrollBars.uex' path='docs/doc[@for="ScrollBars.None"]/*' />
        /// <devdoc>
        ///    <para>
        ///       No scroll bars are shown.
        ///       
        ///    </para>
        /// </devdoc>
        None       = 0,

        /// <include file='doc\ScrollBars.uex' path='docs/doc[@for="ScrollBars.Horizontal"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Only horizontal scroll bars are shown.
        ///       
        ///    </para>
        /// </devdoc>
        Horizontal = 1,

        /// <include file='doc\ScrollBars.uex' path='docs/doc[@for="ScrollBars.Vertical"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Only vertical scroll bars are shown.
        ///       
        ///    </para>
        /// </devdoc>
        Vertical   = 2,

        /// <include file='doc\ScrollBars.uex' path='docs/doc[@for="ScrollBars.Both"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Both horizontal and vertical scroll bars are shown.
        ///       
        ///    </para>
        /// </devdoc>
        Both       = 3,

    }
}
