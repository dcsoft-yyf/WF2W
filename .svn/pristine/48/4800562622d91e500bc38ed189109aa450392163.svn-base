//------------------------------------------------------------------------------
// <copyright file="KeyEvent.cs" company="Microsoft">
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
    using System.Diagnostics.CodeAnalysis;

    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = false )]
    [System.Runtime.InteropServices.ComVisible(true)]
    public class KeyEventArgs : EventArgs {

        /// <include file='doc\KeyEvent.uex' path='docs/doc[@for="KeyEventArgs.keyData"]/*' />
        /// <devdoc>
        ///     Contains key data for KeyDown and KeyUp events.  This is a combination
        ///     of keycode and modifer flags.
        /// </devdoc>
        private readonly Keys keyData;

        /// <include file='doc\KeyEvent.uex' path='docs/doc[@for="KeyEventArgs.handled"]/*' />
        /// <devdoc>
        ///     Determines if this event has been handled by a handler.  If handled, the
        ///     key event will not be sent along to Windows.  If not handled, the event
        ///     will be sent to Windows for default processing.
        /// </devdoc>
        private bool handled = false;

        /// <include file='doc\KeyEvent.uex' path='docs/doc[@for="KeyEventArgs.suppressKeyPress"]/*' />
        /// <devdoc>
        /// </devdoc>
        private bool suppressKeyPress = false;

        /// <include file='doc\KeyEvent.uex' path='docs/doc[@for="KeyEventArgs.KeyEventArgs"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Initializes a new
        ///       instance of the <see cref='System.Windows.Forms.KeyEventArgs'/> class.
        ///    </para>
        /// </devdoc>
        public KeyEventArgs(Keys keyData) {
            this.keyData = keyData;
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public virtual bool Alt {
            get {
                return (keyData & Keys.Alt) == Keys.Alt;
            }
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public bool Control {
            get {
                return (keyData & Keys.Control) == Keys.Control;
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

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public Keys KeyCode {
            [
                // Keys is discontiguous so we have to use Enum.IsDefined.
                SuppressMessage("Microsoft.Performance", "CA1803:AvoidCostlyCallsWherePossible")
            ]
            get {
                Keys keyGenerated =  keyData & Keys.KeyCode;

                // since Keys can be discontiguous, keeping Enum.IsDefined.
                if (!Enum.IsDefined(typeof(Keys),(int)keyGenerated))
                    return Keys.None;
                else
                    return keyGenerated;
            }
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public int KeyValue {
            get {
                return (int)(keyData & Keys.KeyCode);
            }
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public Keys KeyData {
            get {
                return keyData;
            }
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public Keys Modifiers {
            get {
                return keyData & Keys.Modifiers;
            }
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public virtual bool Shift {
            get {
                return (keyData & Keys.Shift) == Keys.Shift;
            }
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public bool SuppressKeyPress {
            get {
                return suppressKeyPress;
            }
            set {
                suppressKeyPress = value;
                handled = value;
            }
        }

    }
}
