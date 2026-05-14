//------------------------------------------------------------------------------
// <copyright file="HatchBrush.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>                                                                
//------------------------------------------------------------------------------

namespace System.Drawing.Drawing2D {

    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = false )]
    public sealed class HatchBrush : Brush 
    {
        internal string ToCanvasString()
        {
            var strCode = new System.Text.StringBuilder();
            strCode.Append("^");
            strCode.Append((int)this.HatchStyle);
            strCode.Append('$');
            strCode.Append(ColorTranslator.ToHtml(this.ForegroundColor));
            strCode.Append('$');
            strCode.Append(ColorTranslator.ToHtml(this.BackgroundColor));
            var strText2 = strCode.ToString();
            return strText2;
        }
        public HatchBrush(HatchStyle hatchstyle, Color foreColor) : 
            this(hatchstyle, foreColor, Color.FromArgb( unchecked( (int) 0xff000000) ) ) 
        {
        }

        public HatchBrush(HatchStyle hatchstyle, Color foreColor, Color backColor) 
        {
            this._HatchStyle = hatchstyle;
            this._ForegroundColor = foreColor;
            this._BackgroundColor = backColor;
        }
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public override object Clone() 
        {
            return this.MemberwiseClone();
        }

        private readonly HatchStyle _HatchStyle;
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public HatchStyle HatchStyle
        {
            get {
                return this._HatchStyle;
            }
        }

        private readonly Color _ForegroundColor;
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public Color ForegroundColor
        {
            get {
                return this._ForegroundColor;
            }
        }

        private readonly Color _BackgroundColor;
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public Color BackgroundColor
        {
            get {
                return this._BackgroundColor;
            }        
        }

        internal override bool EqualsValue(Brush b)
        {
            if( b is HatchBrush hb ) 
            {
                return ( this._HatchStyle == hb._HatchStyle &&
                         this._ForegroundColor == hb._ForegroundColor &&
                         this._BackgroundColor == hb._BackgroundColor );
            }
            else
            {
                return false;
            }
        }
    }
}
