//------------------------------------------------------------------------------
// <copyright file="AdjustableArrowCap.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>                                                                
//------------------------------------------------------------------------------

namespace System.Drawing.Drawing2D {

    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = false )]
    public sealed class AdjustableArrowCap : CustomLineCap {

        public AdjustableArrowCap(float width,
                                  float height) :
            this(width, height, true) {}
            
        public AdjustableArrowCap(float width,
                                  float height,
                                  bool isFilled)
        {
            this._Width = width;
            this._Height = height;
            this._Filled = isFilled;
        }

        private float _Height = 0;
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public float Height
        {
            get { return this._Height; }
            set { this._Height = value; }
        }
        
        private float _Width = 0;

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public float Width
        {
            get { return this._Width; }
            set { this._Width = value; }
        }
        
        private float _MiddleInset = 0;
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public float MiddleInset
        {
            get { return this._MiddleInset; }
            set { this._MiddleInset = value; }
        }
        
        private bool _Filled = false;

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public bool Filled
        {
            get { return this._Filled; }
            set { this._Filled = value ; }
        }
    }
}
