//------------------------------------------------------------------------------
// <copyright file="ToolStripLayoutStyle.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>                                                                
//------------------------------------------------------------------------------


namespace System.Windows.Forms {
    using System;
    using System.ComponentModel;

    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
    public enum ToolStripLayoutStyle {
        StackWithOverflow = 0x0,
        HorizontalStackWithOverflow = 0x1,
        VerticalStackWithOverflow =  0x2,
        Flow = 0x3,
        Table = 0x4    
    }
}
