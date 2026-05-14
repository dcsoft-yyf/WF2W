//------------------------------------------------------------------------------
// <copyright file="CustomLineCap.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>                                                                
//------------------------------------------------------------------------------

namespace System.Drawing.Drawing2D {

    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = false)]
    public class CustomLineCap : MarshalByRefObject, ICloneable, IDisposable {

        internal CustomLineCap() {}
        
        public CustomLineCap(GraphicsPath fillPath,
                             GraphicsPath strokePath) :
            this(fillPath, strokePath, LineCap.Flat) {}
            
        public CustomLineCap(GraphicsPath fillPath,
                             GraphicsPath strokePath,
                             LineCap baseCap) :
            this(fillPath, strokePath, baseCap, 0) {}
            
        public CustomLineCap(GraphicsPath fillPath,
                             GraphicsPath strokePath,
                             LineCap baseCap,
                             float baseInset)
        {
            this._FillPath = fillPath;
            this._StrokePath = strokePath;
            this._BaseCap = baseCap;
            this._BaseInset = baseInset;
        }

        internal CustomLineCap(IntPtr nativeLineCap)
        {
            SetNativeLineCap(nativeLineCap);
        }
        private GraphicsPath _FillPath = null;
        private GraphicsPath _StrokePath = null;
        internal void SetNativeLineCap(IntPtr handle) {
            throw new NotSupportedException();
        }
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void Dispose() 
        {
            if(this._FillPath != null) {
                this._FillPath.Dispose();
                this._FillPath = null;
            }
            if( this._StrokePath != null) {
                this._StrokePath.Dispose();
                this._StrokePath = null;
            }
        }
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public object Clone()
        {
            return this.MemberwiseClone();
        }
        

        private LineJoin _StrokeJoin = LineJoin.Miter;
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true )]
        public LineJoin StrokeJoin
        {
            get { return this._StrokeJoin; }
            set { this._StrokeJoin = value; }
        }
         
        private LineCap _BaseCap = LineCap.Flat;
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public LineCap BaseCap
        {
            get { return this._BaseCap; }
            set { this._BaseCap = value; }
        }
         
        private float _BaseInset = 0.0f;
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public float BaseInset
        {
            get { return this._BaseInset; }
            set { this._BaseInset = value; }
        }
         
        
        private float _WidthScale = 1.0f;
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public float WidthScale
        {
            get { return this._WidthScale; }
            set { this._WidthScale = value; }
        }
    }
}
