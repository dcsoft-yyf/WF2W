//------------------------------------------------------------------------------
// <copyright file="ScrollEventHandler.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>                                                                
//------------------------------------------------------------------------------

namespace System.Windows.Forms {

    using System.Diagnostics;

    using System;
    using System.Threading.Tasks;


    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
    public delegate void ScrollEventHandler(object sender, ScrollEventArgs e);
    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
    public delegate ValueTask ScrollEventAsyncHandler(object sender, ScrollEventArgs e);
}
