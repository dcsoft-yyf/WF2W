//------------------------------------------------------------------------------
// <copyright file="UITypeEditorEditStyle.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>                                                                
//------------------------------------------------------------------------------

/*
 */
namespace System.Drawing.Design {

    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;

    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
    public enum UITypeEditorEditStyle {
        /// <include file='doc\UITypeEditorEditStyle.uex' path='docs/doc[@for="UITypeEditorEditStyle.None"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Indicates no editor style.
        ///    </para>
        /// </devdoc>
        None = 1,
        /// <include file='doc\UITypeEditorEditStyle.uex' path='docs/doc[@for="UITypeEditorEditStyle.Modal"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Indicates a modal editor style.
        ///    </para>
        /// </devdoc>
        Modal = 2,
        /// <include file='doc\UITypeEditorEditStyle.uex' path='docs/doc[@for="UITypeEditorEditStyle.DropDown"]/*' />
        /// <devdoc>
        ///    <para> Indicates a drop-down editor style.</para>
        /// </devdoc>
        DropDown = 3
    }
}

