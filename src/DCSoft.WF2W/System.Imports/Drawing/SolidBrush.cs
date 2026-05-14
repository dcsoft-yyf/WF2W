//------------------------------------------------------------------------------
// <copyright file="SolidBrush.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>                                                                
//------------------------------------------------------------------------------

namespace System.Drawing {
    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = false )]
    public sealed class SolidBrush : Brush {
        private Color color = Color.Empty;
        private bool immutable;

        public SolidBrush(Color color)  
        {
            this.color = color;
        }

        internal SolidBrush(Color color, bool immutable) : this(color) {
            this.immutable = immutable;
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public override object Clone() 
        {
            return this.MemberwiseClone();
        }

        private void CheckImmutable() {
            if (immutable) {
                throw new ArgumentException(DCSR.GetString(DCSR.CantChangeImmutableObjects, "Brush"));
            }
        }
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public override void Dispose( ) {
            this.CheckImmutable();
        }
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true )]
        public Color Color {
            get
            {
                return this.color;
            }
            set 
            {
                this.CheckImmutable();
                this.color = value;
            }
        }

        internal override bool EqualsValue(Brush b)
        {
            if(b is SolidBrush b2 )
            {
                return this.color.Equals(b2.color);
            }
            return false;
        }
    }
}

