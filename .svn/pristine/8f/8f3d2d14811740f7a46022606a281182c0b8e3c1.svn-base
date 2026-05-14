//------------------------------------------------------------------------------
// <copyright file="LinearGradientBrush.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>                                                                
//------------------------------------------------------------------------------

namespace System.Drawing.Drawing2D
{
    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = false )]
    public sealed class LinearGradientBrush : Brush
    {
        internal string SVGID = null;
        private bool interpolationColorsWasSet;
        public LinearGradientBrush(PointF point1, PointF point2,
                                   Color color1, Color color2)
        {
            this._Point1 = point1;
            this._Point2 = point2;
            this._Color1 = color1;
            this._Color2 = color2;
        }

        public LinearGradientBrush(Point point1, Point point2,
                                   Color color1, Color color2)
        {
            this._Point1 = point1;
            this._Point2 = point2;
            this._Color1 = color1;
            this._Color2 = color2;
        }

        private PointF _Point1 = Point.Empty;
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public PointF Point1
        {
            get { return this._Point1; }
        }
        private PointF _Point2 = Point.Empty;
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public PointF Point2
        {
            get { return this._Point2; }
        }

        private Color _Color1 = Color.Empty;
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public Color Color1
        {
            get { return this._Color1; }
            set { this._Color1 = value; }
        }
        private Color _Color2 = Color.Empty;
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public Color Color2
        {
            get { return this._Color2; }
            set { this._Color2 = value; }
        }
        public LinearGradientBrush(RectangleF rect, Color color1, Color color2,
                                   LinearGradientMode linearGradientMode)
        {
            this._Rect = rect;
            this._Color1 = color1;
            this._Color2 = color2;
            this._Mode = linearGradientMode;
        }

        private RectangleF _Rect = RectangleF.Empty;
        private LinearGradientMode _Mode = LinearGradientMode.Horizontal;
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public LinearGradientMode Mode
        {
            get { return this._Mode; }
        }
        public LinearGradientBrush(Rectangle rect, Color color1, Color color2,
                                   LinearGradientMode linearGradientMode)
        {
            this._Rect = rect;
            this._Color1 = color1;
            this._Color2 = color2;
            this._Mode = linearGradientMode;
        }

        public LinearGradientBrush(RectangleF rect, Color color1, Color color2,
                                 float angle)
            : this(rect, color1, color2, angle, false) { }
        public LinearGradientBrush(RectangleF rect, Color color1, Color color2,
                                 float angle, bool isAngleScaleable)
        {
            this._Rect = rect;
            this._Color1 = color1;
            this._Color2 = color2;
            this._Angle = angle;
            this._IsAngleScaleable = isAngleScaleable;
        }

        private float _Angle = 0;
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public float Angle
        {
            get { return this._Angle; }
        }
        private bool _IsAngleScaleable = false;
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public bool IsAngleScaleable
        {
            get { return this._IsAngleScaleable; }
        }
        public LinearGradientBrush(Rectangle rect, Color color1, Color color2,
                                   float angle)
            : this(rect, color1, color2, angle, false)
        {
        }

        public LinearGradientBrush(Rectangle rect, Color color1, Color color2,
                                 float angle, bool isAngleScaleable)
        {
            this._Rect = rect;
            this._Color1 = color1;
            this._Color2 = color2;
            this._Angle = angle;
            this._IsAngleScaleable = isAngleScaleable;
        }

        ///// <devdoc>
        /////     Constructor to initialized this object to be owned by GDI+.
        ///// </devdoc>
        //internal LinearGradientBrush(IntPtr nativeBrush)
        //{
        //    Debug.Assert(nativeBrush != IntPtr.Zero, "Initializing native brush with null.");
        //    SetNativeBrushInternal(nativeBrush);
        //}
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public override object Clone()
        {
            var result = (LinearGradientBrush)this.MemberwiseClone();
            if (this._Blend != null)
            {
                
            }
            if(this._Transform != null)
            {
                result._Transform = this._Transform.Clone();
            }
            return result;
        }

        ///**
        // * Get/set colors
        // */

        //private void _SetLinearColors(Color color1, Color color2)
        //{
        //    int status = SafeNativeMethods.Gdip.GdipSetLineColors(new HandleRef(this, this.NativeBrush),
        //                                           color1.ToArgb(),
        //                                           color2.ToArgb());

        //    if (status != SafeNativeMethods.Gdip.Ok)
        //        throw SafeNativeMethods.Gdip.StatusException(status);
        //}

        //private Color[] _GetLinearColors()
        //{
        //    int[] colors =
        //    new int[]
        //    {
        //        0,
        //        0
        //    };

        //    int status = SafeNativeMethods.Gdip.GdipGetLineColors(new HandleRef(this, this.NativeBrush), colors);

        //    if (status != SafeNativeMethods.Gdip.Ok)
        //        throw SafeNativeMethods.Gdip.StatusException(status);

        //    Color[] lineColor = new Color[2];

        //    lineColor[0] = Color.FromArgb(colors[0]);
        //    lineColor[1] = Color.FromArgb(colors[1]);

        //    return lineColor;
        //}

        private Color[] _LinearColors;
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public Color[] LinearColors
        {
            get { return this._LinearColors; }
            set { this._LinearColors = value; }
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public RectangleF Rectangle
        {
            get { return this._Rect; }
        }

        private bool _GammaCorrection = false;
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public bool GammaCorrection
        {
            get
            {
                return this._GammaCorrection;
            }
            set
            {
                this._GammaCorrection = value;
            }
        }



        private Blend _Blend;
        /// <include file='doc\LinearGradientBrush.uex' path='docs/doc[@for="LinearGradientBrush.Blend"]/*' />
        /// <devdoc>
        ///    Gets or sets a <see cref='System.Drawing.Drawing2D.Blend'/> that specifies
        ///    positions and factors that define a custom falloff for the gradient.
        /// </devdoc>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public Blend Blend
        {
            get { return this._Blend; }
            set { this._Blend = value; }
        }

        /*
         * SigmaBlend & LinearBlend not yet implemented
         */

        /// <include file='doc\LinearGradientBrush.uex' path='docs/doc[@for="LinearGradientBrush.SetSigmaBellShape"]/*' />
        /// <devdoc>
        ///    Creates a gradient falloff based on a
        ///    bell-shaped curve.
        /// </devdoc>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void SetSigmaBellShape(float focus)
        {
            SetSigmaBellShape(focus, (float)1.0);
        }

        /// <include file='doc\LinearGradientBrush.uex' path='docs/doc[@for="LinearGradientBrush.SetSigmaBellShape1"]/*' />
        /// <devdoc>
        ///    Creates a gradient falloff based on a
        ///    bell-shaped curve.
        /// </devdoc>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void SetSigmaBellShape(float focus, float scale)
        {
            throw new NotSupportedException();
        }

        /// <include file='doc\LinearGradientBrush.uex' path='docs/doc[@for="LinearGradientBrush.SetBlendTriangularShape"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Creates a triangular gradient.
        ///    </para>
        /// </devdoc>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void SetBlendTriangularShape(float focus)
        {
            SetBlendTriangularShape(focus, (float)1.0);
        }

        /// <include file='doc\LinearGradientBrush.uex' path='docs/doc[@for="LinearGradientBrush.SetBlendTriangularShape1"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Creates a triangular gradient.
        ///    </para>
        /// </devdoc>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void SetBlendTriangularShape(float focus, float scale)
        {
            throw new NotSupportedException();
        }
        private ColorBlend _InterpolationColors;
        /// <include file='doc\LinearGradientBrush.uex' path='docs/doc[@for="LinearGradientBrush.InterpolationColors"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Gets or sets a <see cref='System.Drawing.Drawing2D.ColorBlend'/> that defines a multi-color linear
        ///       gradient.
        ///    </para>
        /// </devdoc>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public ColorBlend InterpolationColors
        {
            get { return _InterpolationColors; }
            set { _InterpolationColors = value; }
        }

        private WrapMode _WrapMode = WrapMode.Tile;
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public WrapMode WrapMode
        {
            get
            {
                return this._WrapMode;
            }
            set
            {
                this._WrapMode = value;
            }
        }
        private Matrix _Transform = null;
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public Matrix Transform
        {
            get { return this._Transform; }
            set { this._Transform = value; }
        }

        /// <include file='doc\LinearGradientBrush.uex' path='docs/doc[@for="LinearGradientBrush.ResetTransform"]/*' />
        /// <devdoc>
        ///    Resets the <see cref='System.Drawing.Drawing2D.LinearGradientBrush.Transform'/> property to identity.
        /// </devdoc>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void ResetTransform()
        {
            this._Transform?.Reset();
        }

        /// <include file='doc\LinearGradientBrush.uex' path='docs/doc[@for="LinearGradientBrush.MultiplyTransform"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Multiplies the <see cref='System.Drawing.Drawing2D.Matrix'/> that represents the local geometrical
        ///       transform of this <see cref='System.Drawing.Drawing2D.LinearGradientBrush'/> by the specified <see cref='System.Drawing.Drawing2D.Matrix'/> by prepending the specified <see cref='System.Drawing.Drawing2D.Matrix'/>.
        ///    </para>
        /// </devdoc>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void MultiplyTransform(Matrix matrix)
        {
            MultiplyTransform(matrix, MatrixOrder.Prepend);
        }

        /// <include file='doc\LinearGradientBrush.uex' path='docs/doc[@for="LinearGradientBrush.MultiplyTransform1"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Multiplies the <see cref='System.Drawing.Drawing2D.Matrix'/> that represents the local geometrical
        ///       transform of this <see cref='System.Drawing.Drawing2D.LinearGradientBrush'/> by the specified <see cref='System.Drawing.Drawing2D.Matrix'/> in the specified order.
        ///    </para>
        /// </devdoc>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void MultiplyTransform(Matrix matrix, MatrixOrder order)
        {
            if (this._Transform == null)
            {
                this._Transform = new Matrix();
            }
            this._Transform.Multiply(matrix, order);
        }


        /// <include file='doc\LinearGradientBrush.uex' path='docs/doc[@for="LinearGradientBrush.TranslateTransform"]/*' />
        /// <devdoc>
        ///    Translates the local geometrical transform
        ///    by the specified dimmensions. This method prepends the translation to the
        ///    transform.
        /// </devdoc>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void TranslateTransform(float dx, float dy)
        { TranslateTransform(dx, dy, MatrixOrder.Prepend); }

        /// <include file='doc\LinearGradientBrush.uex' path='docs/doc[@for="LinearGradientBrush.TranslateTransform1"]/*' />
        /// <devdoc>
        ///    Translates the local geometrical transform
        ///    by the specified dimmensions in the specified order.
        /// </devdoc>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void TranslateTransform(float dx, float dy, MatrixOrder order)
        {
            if (this._Transform == null)
            {
                this._Transform = new Matrix();
            }
            this._Transform.Translate(dx, dy, order);
        }

        /// <include file='doc\LinearGradientBrush.uex' path='docs/doc[@for="LinearGradientBrush.ScaleTransform"]/*' />
        /// <devdoc>
        ///    Scales the local geometric transform by the
        ///    specified amounts. This method prepends the scaling matrix to the transform.
        /// </devdoc>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void ScaleTransform(float sx, float sy)
        { ScaleTransform(sx, sy, MatrixOrder.Prepend); }

        /// <include file='doc\LinearGradientBrush.uex' path='docs/doc[@for="LinearGradientBrush.ScaleTransform1"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Scales the local geometric transform by the
        ///       specified amounts in the specified order.
        ///    </para>
        /// </devdoc>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void ScaleTransform(float sx, float sy, MatrixOrder order)
        {
            if (this._Transform == null)
            {
                this._Transform = new Matrix();
            }
            this._Transform.Scale(sx, sy, order);
        }

        /// <include file='doc\LinearGradientBrush.uex' path='docs/doc[@for="LinearGradientBrush.RotateTransform"]/*' />
        /// <devdoc>
        ///    Rotates the local geometric transform by the
        ///    specified amount. This method prepends the rotation to the transform.
        /// </devdoc>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void RotateTransform(float angle)
        { RotateTransform(angle, MatrixOrder.Prepend); }

        /// <include file='doc\LinearGradientBrush.uex' path='docs/doc[@for="LinearGradientBrush.RotateTransform1"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Rotates the local geometric transform by the specified
        ///       amount in the specified order.
        ///    </para>
        /// </devdoc>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void RotateTransform(float angle, MatrixOrder order)
        {
            if (this._Transform == null)
            {
                this._Transform = new Matrix();
            }
            this._Transform.Rotate(angle, order);
        }
    }
}
