//------------------------------------------------------------------------------
// <copyright file="PrintPageEvent.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>                                                                
//------------------------------------------------------------------------------

namespace System.Drawing.Printing {

    using System.Diagnostics;
    using System;
    using System.Drawing;
    using System.ComponentModel;
    using Microsoft.Win32;

    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = false )]
    public class PrintPageEventArgs : EventArgs {
        private bool hasMorePages;
        private bool cancel;

        private Graphics graphics;
        private readonly Rectangle marginBounds;
        private readonly Rectangle pageBounds;
        private readonly PageSettings pageSettings;


        /// <include file='doc\PrintPageEvent.uex' path='docs/doc[@for="PrintPageEventArgs.PrintPageEventArgs"]/*' />
        /// <devdoc>
        /// <para>Initializes a new instance of the <see cref='System.Drawing.Printing.PrintPageEventArgs'/> class.</para>
        /// </devdoc>
        public PrintPageEventArgs(Graphics graphics, Rectangle marginBounds, Rectangle pageBounds, PageSettings pageSettings) {
            this.graphics = graphics; // may be null, see PrintController
            this.marginBounds = marginBounds;
            this.pageBounds = pageBounds;
            this.pageSettings = pageSettings;
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true )]
        public bool Cancel {
            get { return cancel;}
            set { cancel = value;}
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public Graphics Graphics {
            get {
                return graphics;
            }
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public bool HasMorePages {
            get { return hasMorePages;}
            set { hasMorePages = value;}
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public Rectangle MarginBounds {
            get {
                return marginBounds;
            }
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public Rectangle PageBounds {
            get {
                return pageBounds;
            }
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public PageSettings PageSettings {
            get {
                return pageSettings;
            }
        }

        /// <include file='doc\PrintPageEvent.uex' path='docs/doc[@for="PrintPageEventArgs.Dispose"]/*' />
        /// <devdoc>
        ///    <para>Disposes
        ///       of the resources (other than memory) used by
        ///       the <see cref='System.Drawing.Printing.PrintPageEventArgs'/>.</para>
        /// </devdoc>
        // We want a way to dispose the GDI+ Graphics, but we don't want to create one
        // simply to dispose it
        internal void Dispose() {
            graphics.Dispose();
        }

        internal void SetGraphics(Graphics value) {
            this.graphics = value;
        }
    }
}

