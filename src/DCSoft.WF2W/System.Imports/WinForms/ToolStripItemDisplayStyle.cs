//------------------------------------------------------------------------------
// <copyright file="ToolStripItemDisplayStyle.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>                                                                
//------------------------------------------------------------------------------


namespace System.Windows.Forms {
    using System;

    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
    public enum ToolStripItemDisplayStyle {
       /// <include file='doc\ToolStripItemDisplayStyle.uex' path='docs/doc[@for="ToolStripItemDisplayStyle.None"]/*' />
       None                     = 0x0000,       
       /// <include file='doc\ToolStripItemDisplayStyle.uex' path='docs/doc[@for="ToolStripItemDisplayStyle.Text"]/*' />
       Text                     = 0x0001, // 0001
       /// <include file='doc\ToolStripItemDisplayStyle.uex' path='docs/doc[@for="ToolStripItemDisplayStyle.Image"]/*' />
       Image                    = 0x0002, // 0010
       /// <include file='doc\ToolStripItemDisplayStyle.uex' path='docs/doc[@for="ToolStripItemDisplayStyle.ImageAndText"]/*' />
       ImageAndText             = 0x0003, // 0011
    }
}
