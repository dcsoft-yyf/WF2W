//------------------------------------------------------------------------------
// <copyright file="Pen.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>                                                                
//------------------------------------------------------------------------------
using System.Drawing.Drawing2D;

namespace System.Drawing
{

    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = false)]
    public sealed class Pen : MarshalByRefObject, ICloneable, IDisposable
    {
        static Pen ()
        {
            DCValueConvert.CheckIsBlazorWASM();
        }
        private Color color;
        private bool immutable;

        internal Pen(Color color, bool immutable) : this(color)
        {
            this.immutable = immutable;
        }

        public Pen(Color color) : this(color, (float)1.0)
        {
        }

        public Pen(Color color, float width)
        {
            this.color = color;
            this._Width = width;
        }

        public Pen(Brush brush) : this(brush, (float)1.0)
        {
        }

        public Pen(Brush brush, float width)
        {
            if (brush == null)
                throw new ArgumentNullException("brush");
            this._Brush = brush;
            this._Width = width;
        }

        internal bool EqualsValue(Pen p )
        {
            return p.color == this.color && p._Width == this._Width && p._DashStyle == this._DashStyle;
        }
        internal IntPtr NativePen
        {
            get
            {
                throw new NotSupportedException();
            }
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public object Clone()
        {
            var p = (Pen)this.MemberwiseClone();
            if (this._Brush != null)
            {
                p._Brush = (Brush)this._Brush.Clone();
            }
            if (this._CustomStartCap != null)
            {
                p._CustomStartCap = (CustomLineCap)this._CustomStartCap.Clone();
            }
            if (this._CustomEndCap != null)
            {
                p._CustomEndCap = (CustomLineCap)this._CustomEndCap.Clone();
            }
            if (this._DashPattern != null)
            {
                p._DashPattern = (float[])this._DashPattern.Clone();
            }
            if (this._CompoundArray != null)
            {
                p._CompoundArray = (float[])this._CompoundArray.Clone();
            }
            return p;
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void Dispose()
        {
            this.CheckImmutable();
            if (this._Brush != null)
            {
                this._Brush.Dispose();
                this._Brush = null;
            }
            if (this._CustomEndCap != null)
            {
                this._CustomEndCap.Dispose();
                this._CustomEndCap = null;
            }
            if (this._CustomStartCap != null)
            {
                this._CustomStartCap.Dispose();
                this._CustomStartCap = null;
            }
            this._DashPattern = null;
            this._CompoundArray = null;
        }

        private float _Width = 1;
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public float Width
        {
            get
            {
                return this._Width;
            }

            set
            {
                this.CheckImmutable();
                this._Width = value;
            }
        }
        private void CheckImmutable()
        {
            if (immutable)
            {
                throw new ArgumentException(DCSR.GetString(DCSR.CantChangeImmutableObjects, "Pen"));
            }
        }
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void SetLineCap(LineCap startCap, LineCap endCap, DashCap dashCap)
        {
            this.CheckImmutable();
            this._StartCap = startCap;
            this._EndCap = endCap;
            this._DashCap = dashCap;
        }

        private LineCap _StartCap = LineCap.Flat;
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public LineCap StartCap
        {
            get
            {
                return this._StartCap;
            }
            set
            {
                this.CheckImmutable();
                this._StartCap = value;
            }
        }

        private LineCap _EndCap = LineCap.Flat;
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public LineCap EndCap
        {
            get
            {
                return this._EndCap;
            }
            set
            {
                this.CheckImmutable();
                this._EndCap = value;
            }
        }

        private DashCap _DashCap = DashCap.Flat;
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public DashCap DashCap
        {
            get
            {
                return this._DashCap;
            }

            set
            {
                this.CheckImmutable();
                this._DashCap = value;
            }
        }

        private LineJoin _LineJoin = LineJoin.Miter;
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public LineJoin LineJoin
        {
            get
            {
                return this._LineJoin;
            }

            set
            {
                this.CheckImmutable();
                this._LineJoin = value;
            }
        }

        private CustomLineCap _CustomStartCap = null;
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public CustomLineCap CustomStartCap
        {
            get
            {
                return this._CustomStartCap;
            }

            set
            {
                this.CheckImmutable();
                this._CustomStartCap = value;
            }
        }

        private CustomLineCap _CustomEndCap = null;
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public CustomLineCap CustomEndCap
        {
            get
            {
                return this._CustomEndCap;
            }

            set
            {
                this.CheckImmutable();
                this._CustomEndCap = value;
            }
        }

        private float _MiterLimit = 10.0f;
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public float MiterLimit
        {
            get
            {
                return this._MiterLimit;
            }

            set
            {
                this.CheckImmutable();
                this._MiterLimit = value;
            }
        }

        private PenAlignment _Alignment = PenAlignment.Center;
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public PenAlignment Alignment
        {
            get
            {
                return this._Alignment;
            }
            set
            {
                this.CheckImmutable();
                this._Alignment = value;
            }
        }

        private Matrix _Transform = null;
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public Matrix Transform
        {
            get
            {
                return this._Transform;
            }

            set
            {
                this.CheckImmutable();
                this._Transform = value;
            }
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void ResetTransform()
        {
            this._Transform?.Reset();
        }
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void MultiplyTransform(Matrix matrix)
        {
            MultiplyTransform(matrix, MatrixOrder.Prepend);
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void MultiplyTransform(Matrix matrix, MatrixOrder order)
        {
            if (this._Transform == null)
            {
                this._Transform = new Matrix();
            }
            this._Transform.Multiply(matrix, order);
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void TranslateTransform(float dx, float dy)
        {
            TranslateTransform(dx, dy, MatrixOrder.Prepend);
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void TranslateTransform(float dx, float dy, MatrixOrder order)
        {
            if (this._Transform == null)
            {
                this._Transform = new Matrix();
            }
            this._Transform.Translate(dx, dy, order);
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void ScaleTransform(float sx, float sy)
        {
            if (this._Transform == null)
            {
                this._Transform = new Matrix();
            }
            this._Transform.Scale(sx, sy);
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void ScaleTransform(float sx, float sy, MatrixOrder order)
        {
            if (this._Transform == null)
            {
                this._Transform = new Matrix();
            }
            this._Transform.Scale(sx, sy, order);
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void RotateTransform(float angle)
        {
            RotateTransform(angle, MatrixOrder.Prepend);
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void RotateTransform(float angle, MatrixOrder order)
        {
            if (this._Transform == null)
            {
                this._Transform = new Matrix();
            }
            this._Transform.Rotate(angle, order);
        }


        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public PenType PenType
        {
            get
            {
                // Infer pen type solely from managed state without GDI+ calls
                // If a Brush is set, map by its runtime type; otherwise treat as solid color
                if (this._Brush == null)
                {
                    return PenType.SolidColor;
                }

                Brush b = this._Brush;
                if (b is SolidBrush)
                {
                    return PenType.SolidColor;
                }
                if (b is TextureBrush)
                {
                    return PenType.TextureFill;
                }
                if (b is HatchBrush)
                {
                    return PenType.HatchFill;
                }
                if (b is LinearGradientBrush)
                {
                    return PenType.LinearGradient;
                }
                if (b is PathGradientBrush)
                {
                    return PenType.PathGradient;
                }

                // Unknown brush type, default to solid color for safety
                return PenType.SolidColor;
            }
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public Color Color
        {
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
        private Brush _Brush = null;
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public Brush Brush
        {
            get
            {
                return this._Brush;
            }

            set
            {
                this.CheckImmutable();
                this._Brush = value;
            }
        }

        private DashStyle _DashStyle = DashStyle.Solid;
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public DashStyle DashStyle
        {
            get
            {
                return this._DashStyle;
            }

            set
            {
                this.CheckImmutable();
                this._DashStyle = value;
                if (value == DashStyle.Custom)
                {
                    EnsureValidDashPattern();
                }
            }
        }

        private void EnsureValidDashPattern()
        {
            if (this._DashPattern == null)
            {
                this._DashPattern = new float[] { 1 };
            }
        }
        private float _DashOffset = 0;
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public float DashOffset
        {
            get
            {
                return this._DashOffset;
            }
            set
            {
                this.CheckImmutable();
                this._DashOffset = value;
            }
        }

        private float[] _DashPattern = null;
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public float[] DashPattern
        {
            get
            {
                return this._DashPattern;
            }

            set
            {
                this.CheckImmutable();
                this._DashPattern = value;
            }
        }

        private float[] _CompoundArray = null;
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public float[] CompoundArray
        {
            get
            {
                return this._CompoundArray;
            }
            set
            {
                this.CheckImmutable();
                this._CompoundArray = value;
            }
        }

    }
}
