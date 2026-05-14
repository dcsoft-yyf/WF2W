using System;
using System.Diagnostics.CodeAnalysis;

namespace System.Windows.Forms {
    /// <include file='doc\SearchDirectionHint.uex' path='docs/doc[@for="SearchDirectionHint"]/*' />
    /// <devdoc>
    ///
    /// </devdoc>
    [
        SuppressMessage("Microsoft.Design", "CA1008:EnumsShouldHaveZeroValue")  // Maps to native enum.
    ]
    public enum SearchDirectionHint {
        /// <include file='doc\SearchDirectionHint.uex' path='docs/doc[@for="SearchDirectionHint.Up"]/*' />
        /// <devdoc>
        ///
        /// </devdoc>
        Up = WinFormNativeMethods.VK_UP,
        /// <include file='doc\SearchDirectionHint.uex' path='docs/doc[@for="SearchDirectionHint.Down"]/*' />
        /// <devdoc>
        ///
        /// </devdoc>
        Down = WinFormNativeMethods.VK_DOWN,
        /// <include file='doc\SearchDirectionHint.uex' path='docs/doc[@for="SearchDirectionHint.Left"]/*' />
        /// <devdoc>
        ///
        /// </devdoc>
        Left = WinFormNativeMethods.VK_LEFT,
        /// <include file='doc\SearchDirectionHint.uex' path='docs/doc[@for="SearchDirectionHint.Right"]/*' />
        /// <devdoc>
        ///
        /// </devdoc>
        Right = WinFormNativeMethods.VK_RIGHT
    }
}
