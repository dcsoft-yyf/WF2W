//------------------------------------------------------------------------------
// <copyright file="ToolStripItemOverflow.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>                                                                
//------------------------------------------------------------------------------


namespace System.Windows.Forms {
    using System;


    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
    public enum ToolStripItemOverflow {
        /// <include file='doc\ToolStripItemOverflow.uex' path='docs/doc[@for="ToolStripItemOverflow.Never"]/*' />
        Never,		// on the main winbar itself,
        /// <include file='doc\ToolStripItemOverflow.uex' path='docs/doc[@for="ToolStripItemOverflow.Always"]/*' />
        Always,		// on the overflow window
        /// <include file='doc\ToolStripItemOverflow.uex' path='docs/doc[@for="ToolStripItemOverflow.AsNeeded // DEFAULT try for main"]/*' />
        AsNeeded	// DEFAULT try for main, overflow as necessary
    }
}
