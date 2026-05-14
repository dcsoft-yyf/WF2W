//------------------------------------------------------------------------------
// <copyright file="ProgressBarStyle.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>                                                                
//------------------------------------------------------------------------------

namespace System.Windows.Forms {

    using System.Diagnostics;
    using System;

    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
    public enum ProgressBarStyle
    {
        /// <include file='doc\ProgressBarStyle.uex' path='docs/doc[@for="ProgressBarStyle.Blocks"]/*' />
        /// <devdoc>
        ///    <para>
        ///       The progress bar displays the progress status as a segmented bar.  
        ///    </para>
        /// </devdoc>
        Blocks,

        /// <include file='doc\ProgressBarStyle.uex' path='docs/doc[@for="ProgressBarStyle.Continuous"]/*' />
        /// <devdoc>
        ///    <para>
        ///       The progress bar displays the progress status in a smooth scrolling bar.  
        ///    </para>
        /// </devdoc>
        Continuous,

        /// <include file='doc\ProgressBarStyle.uex' path='docs/doc[@for="ProgressBarStyle.Marquee"]/*' />
        /// <devdoc>
        ///    <para>
        ///       The progress bar displays the progress status in the marquee style.  
        ///    </para>
        /// </devdoc>
        Marquee
    }
}
