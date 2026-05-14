//------------------------------------------------------------------------------
// <copyright file="CreateParams.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>                                                                
//------------------------------------------------------------------------------

namespace System.Windows.Forms {

    using System.Diagnostics;
    using System.Text;
    using System;

    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = false )]
    public class CreateParams {
        string className;
        string caption;
        int style;
        int exStyle;
        int classStyle;
        int x;
        int y;
        int width;
        int height;
        IntPtr parent;
        object param;

        internal Control ControlInstance { get; set; }
        internal Type ControlType { get; set; }
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string ClassName {
            get { return className; }
            set { className = value; }
        }
        //public System.Drawing.Icon FormIcon { get; set; }
        //public System.Drawing.Color BackColor { get; set; }
        //public System.Drawing.Color ForeColor { get; set; }
        //public System.Drawing.Font Font { get; set; }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string Caption {
            get { return caption; }
            set { caption = value; }
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public int Style {
            get { return style; }
            set { style = value; }
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public int ExStyle {
            get { return exStyle; }
            set { exStyle = value; }
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public int ClassStyle {
            get { return classStyle; }
            set { classStyle = value; }
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public int X {
            get { return x; }
            set { x = value; }
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public int Y {
            get { return y; }
            set { y = value; }
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public int Width {
            get { return width; }
            set { width = value; }
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public int Height {
            get { return height; }
            set { height = value; }
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public IntPtr Parent {
            get { return parent; }
            set { parent = value; }
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public object Param {
            get { return param; }
            set { param = value; }
        }

        /// <include file='doc\CreateParams.uex' path='docs/doc[@for="CreateParams.ToString"]/*' />
        /// <devdoc>
        ///    <para>[To be supplied.]</para>
        /// </devdoc>
        public override string ToString() {
            StringBuilder sb = new StringBuilder(64);
            sb.Append("CreateParams {'");
            sb.Append(className);
            sb.Append("', '");
            sb.Append(caption);
            sb.Append("', 0x");
            sb.Append(Convert.ToString(style, 16));
            sb.Append(", 0x");
            sb.Append(Convert.ToString(exStyle, 16));
            sb.Append(", {");
            sb.Append(x);
            sb.Append(", ");
            sb.Append(y);
            sb.Append(", ");
            sb.Append(width);
            sb.Append(", ");
            sb.Append(height);
            sb.Append("}");
            sb.Append("}");
            return sb.ToString();
        }
    }
}
