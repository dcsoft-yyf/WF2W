//------------------------------------------------------------------------------
// <copyright file="PaperSource.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>                                                                
//------------------------------------------------------------------------------

namespace System.Drawing.Printing {
    using System.Runtime.Serialization.Formatters;
    using System.Runtime.InteropServices;
    using System.Diagnostics;
    using System;    
    using System.Drawing;
    using System.ComponentModel;
    using Microsoft.Win32;

    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = false)]
    public class PaperSource {
        private string name;
        private PaperSourceKind kind;

        /// <include file='doc\PaperSource.uex' path='docs/doc[@for="PaperSource.PaperSource"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Initializes a new instance of the <see cref='System.Drawing.Printing.PaperSource'/> class with default properties.
        ///       This constructor is required for the serialization of the <see cref='System.Drawing.Printing.PaperSource'/> class.
        ///    </para>
        /// </devdoc>
        public PaperSource()
        {
            this.kind = PaperSourceKind.Custom;
            this.name = String.Empty;
        }

        internal PaperSource(PaperSourceKind kind, string name) {
            this.kind = kind;
            this.name = name;
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public PaperSourceKind Kind {
            get {
                if ((unchecked((int) kind)) >= SafeNativeMethods.DMBIN_USER)
                    return PaperSourceKind.Custom;
                else
                    return kind;
            }
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public int RawKind
        {
            get { return unchecked((int) kind); }
            set { kind = unchecked((PaperSourceKind) value); }
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string SourceName {
            get { return name;}
            set { name = value; }
        }

        /// <include file='doc\PaperSource.uex' path='docs/doc[@for="PaperSource.ToString"]/*' />
        /// <internalonly/>
        /// <devdoc>
        ///    <para>
        ///       Provides some interesting information about the PaperSource in
        ///       String form.
        ///    </para>
        /// </devdoc>
        public override string ToString() {
            return "[PaperSource " + SourceName
            + " Kind=" + TypeDescriptor.GetConverter(typeof(PaperSourceKind)).ConvertToString(Kind)
            + "]";
        }
    }
}
