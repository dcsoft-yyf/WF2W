//------------------------------------------------------------------------------
// <copyright file="TreeNodeMouseClickEventArgs.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>                                                                
//------------------------------------------------------------------------------

namespace System.Windows.Forms {

    using System.Diagnostics;

    using System;
    using System.ComponentModel;
    using System.Drawing;
    using Microsoft.Win32;


    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
    public class TreeNodeMouseClickEventArgs : MouseEventArgs {
        
        private TreeNode node;
        

        /// <include file='doc\TreeNodeClickEventArgs.uex' path='docs/doc[@for="TreeNodeClickEventArgs.TreeNodeClickEventArgs"]/*' />
        /// <devdoc>
        ///    <para>[To be supplied.]</para>
        /// </devdoc>
        public TreeNodeMouseClickEventArgs(TreeNode node, MouseButtons button, int clicks, int x, int y)
            : base(button, clicks, x, y, 0) {
            this.node = node;
        }

        /// <include file='doc\NodeLabelEditEvent.uex' path='docs/doc[@for="NodeLabelEditEventArgs.Node"]/*' />
        /// <devdoc>
        ///    <para>[To be supplied.]</para>
        /// </devdoc>
        public TreeNode Node {
            get {
                return node;
            }
        }
    }
}
