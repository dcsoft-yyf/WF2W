//------------------------------------------------------------------------------
// <copyright file="ToolStripItemClickedEventHandler.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>                                                                
//------------------------------------------------------------------------------

namespace System.Windows.Forms {

    using System;
    using System.Threading.Tasks;

    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
    public delegate void ToolStripItemClickedEventHandler(object sender, ToolStripItemClickedEventArgs e);
    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
    public delegate Task ToolStripItemClickedAsyncEventHandler(object sender, ToolStripItemClickedEventArgs e);
}
