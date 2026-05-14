//------------------------------------------------------------------------------
// <copyright file="PrintEventHandler.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>                                                                
//------------------------------------------------------------------------------

namespace System.Drawing.Printing {

    using System.Diagnostics;
    using System;
    using System.Drawing;
    using System.ComponentModel;
    using Microsoft.Win32;

    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
    public delegate void PrintEventHandler(object sender, PrintEventArgs e);
}

