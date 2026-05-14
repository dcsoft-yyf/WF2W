//------------------------------------------------------------------------------
// <copyright file="KeyPressEvent.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>                                                                
//------------------------------------------------------------------------------

/*
 */
namespace System.Windows.Forms {

    using System.Diagnostics;

    using System;
    using System.Drawing;
    using System.ComponentModel;
    using Microsoft.Win32;

    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = false )]
    public class KeyPressEventArgs : EventArgs {

        /// <include file='doc\KeyPressEvent.uex' path='docs/doc[@for="KeyPressEventArgs.keyChar"]/*' />
        /// <devdoc>
        ///     Contains the character of the current KeyPress event.
        /// </devdoc>
        private char keyChar;

        /// <include file='doc\KeyPressEvent.uex' path='docs/doc[@for="KeyPressEventArgs.handled"]/*' />
        /// <devdoc>
        ///     Determines if this event has been handled by a handler.  If handled, the
        ///     key event will not be sent along to Windows.  If not handled, the event
        ///     will be sent to Windows for default processing.
        /// </devdoc>
        private bool handled;

        /// <include file='doc\KeyPressEvent.uex' path='docs/doc[@for="KeyPressEventArgs.KeyPressEventArgs"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Initializes a new
        ///       instance of the <see cref='System.Windows.Forms.KeyPressEventArgs'/>
        ///       class.
        ///    </para>
        /// </devdoc>
        public KeyPressEventArgs(char keyChar) {
            this.keyChar = keyChar;
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public char KeyChar {
            get {
                return keyChar;
            }
            set {
                keyChar = value;
            }
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public bool Handled {
            get {
                return handled;
            }
            set {
                handled = value;
            }
        }
    }
}
