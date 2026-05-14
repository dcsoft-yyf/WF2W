//------------------------------------------------------------------------------
// <copyright file="Matrix.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>                                                                
//------------------------------------------------------------------------------

namespace System.Drawing.Drawing2D
{
    //using System.Runtime.InteropServices;

    //using System.Diagnostics;

    //using System;
    //using System.Drawing;
    //using Microsoft.Win32;
    //using System.ComponentModel;
    //using System.Drawing.Internal;

    /**
     * Represent a Matrix object
     */
    /// <include file='doc\Matrix.uex' path='docs/doc[@for="Matrix"]/*' />
    /// <devdoc>
    ///    Encapsulates a 3 X 3 affine matrix that
    ///    represents a geometric transform.
    /// </devdoc>
    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = false)]
    public sealed class Matrix : MarshalByRefObject, IDisposable
    {
        public IntPtr nativeMatrix = IntPtr.Zero;
        // Managed 3x3 affine matrix represented by 6 elements (m11, m12, m21, m22, dx, dy)
        // Full 3x3 matrix is:
        // [ m11 m12 0 ]
        // [ m21 m22 0 ]
        // [ dx  dy  1 ]
        private float m11 = 1f, m12 = 0f, m21 = 0f, m22 = 1f, dx = 0f, dy = 0f;

        /*
         * Create a new identity matrix
         */

        /// <include file='doc\Matrix.uex' path='docs/doc[@for="Matrix.Matrix"]/*' />
        /// <devdoc>
        ///    Initializes a new instance of the <see cref='System.Drawing.Drawing2D.Matrix'/> class.
        /// </devdoc>
        public Matrix()
        {
            // identity by defaults set above
        }

        /// <include file='doc\Matrix.uex' path='docs/doc[@for="Matrix.Matrix1"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Initialized a new instance of the <see cref='System.Drawing.Drawing2D.Matrix'/> class with the specified
        ///       elements.
        ///    </para>
        /// </devdoc>
        public Matrix(float m11,
                          float m12,
                          float m21,
                          float m22,
                          float dx,
                          float dy)
        {
            this.m11 = m11; this.m12 = m12; this.m21 = m21; this.m22 = m22; this.dx = dx; this.dy = dy;
        }

        // float version
        /// <include file='doc\Matrix.uex' path='docs/doc[@for="Matrix.Matrix2"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Initializes a new instance of the <see cref='System.Drawing.Drawing2D.Matrix'/> class to the geometrical transform
        ///       defined by the specified rectangle and array of points.
        ///    </para>
        /// </devdoc>
        public Matrix(RectangleF rect, PointF[] plgpts)
        {
            if (plgpts == null) throw new ArgumentNullException("plgpts");
            if (plgpts.Length != 3) throw new ArgumentException("plgpts must have 3 points");
            // Compute affine transform that maps rectangle corners to given parallelogram points
            // Source points: (rect.Left, rect.Top), (rect.Right, rect.Top), (rect.Left, rect.Bottom)
            PointF s0 = new PointF(rect.Left, rect.Top);
            PointF s1 = new PointF(rect.Right, rect.Top);
            PointF s2 = new PointF(rect.Left, rect.Bottom);
            PointF d0 = plgpts[0];
            PointF d1 = plgpts[1];
            PointF d2 = plgpts[2];
            // Solve for matrix columns: u = d1-d0 corresponds to vector (rect.Width, 0), v = d2-d0 corresponds to (0, rect.Height)
            float rw = rect.Width != 0 ? rect.Width : 1f;
            float rh = rect.Height != 0 ? rect.Height : 1f;
            PointF u = new PointF(d1.X - d0.X, d1.Y - d0.Y);
            PointF v = new PointF(d2.X - d0.X, d2.Y - d0.Y);
            this.m11 = u.X / rw;
            this.m12 = u.Y / rw;
            this.m21 = v.X / rh;
            this.m22 = v.Y / rh;
            this.dx = d0.X - (m11 * s0.X + m21 * s0.Y);
            this.dy = d0.Y - (m12 * s0.X + m22 * s0.Y);
        }

        // int version
        /// <include file='doc\Matrix.uex' path='docs/doc[@for="Matrix.Matrix3"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Initializes a new instance of the <see cref='System.Drawing.Drawing2D.Matrix'/> class to the geometrical transform
        ///       defined by the specified rectangle and array of points.
        ///    </para>
        /// </devdoc>
        public Matrix(Rectangle rect, Point[] plgpts)
        {
            if (plgpts == null) throw new ArgumentNullException("plgpts");
            if (plgpts.Length != 3) throw new ArgumentException("plgpts must have 3 points");
            Matrix m = new Matrix(new RectangleF(rect.X, rect.Y, rect.Width, rect.Height), new PointF[] { new PointF(plgpts[0].X, plgpts[0].Y), new PointF(plgpts[1].X, plgpts[1].Y), new PointF(plgpts[2].X, plgpts[2].Y) });
            this.m11 = m.m11; this.m12 = m.m12; this.m21 = m.m21; this.m22 = m.m22; this.dx = m.dx; this.dy = m.dy;
        }

        internal PointF TransformPointFPageUnitRate(float x, float y, float pageUnitRate)
        {
            return new PointF(
                x * this.m11 + y * this.m12 + this.dx * pageUnitRate,
                x * this.m21 + y * this.m22 + this.dy * pageUnitRate
                );
        }

        /// <include file='doc\Matrix.uex' path='docs/doc[@for="Matrix.Dispose"]/*' />
        /// <devdoc>
        ///    Cleans up resources allocated for this
        /// <see cref='System.Drawing.Drawing2D.Matrix'/>.
        /// </devdoc>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void Dispose()
        {
            // Managed-only; nothing to release
            GC.SuppressFinalize(this);
        }

        /// <include file='doc\Matrix.uex' path='docs/doc[@for="Matrix.Finalize"]/*' />
        /// <devdoc>
        ///    Cleans up resources allocated for this
        /// <see cref='System.Drawing.Drawing2D.Matrix'/>.
        /// </devdoc>
        ~Matrix()
        {
        }

        /// <include file='doc\Matrix.uex' path='docs/doc[@for="Matrix.Clone"]/*' />
        /// <devdoc>
        ///    Creates an exact copy of this <see cref='System.Drawing.Drawing2D.Matrix'/>.
        /// </devdoc>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public Matrix Clone()
        {
            return new Matrix(m11, m12, m21, m22, dx, dy);
        }

        /// <include file='doc\Matrix.uex' path='docs/doc[@for="Matrix.Elements"]/*' />
        /// <devdoc>
        ///    Gets an array of floating-point values that
        ///    represent the elements of this <see cref='System.Drawing.Drawing2D.Matrix'/>.
        /// </devdoc>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public float[] Elements
        {
            get
            {
                return new float[] { m11, m12, m21, m22, dx, dy };
            }
        }
        internal float Element0 { get { return this.m11; } set { this.m11 = value; } }
        internal float Element1 { get { return this.m12; } set { this.m12 = value; } }
        internal float Element2 { get { return this.m21; } set { this.m21 = value; } }
        internal float Element3 { get { return this.m22; } set { this.m22 = value; } }
        internal float Element4 { get { return this.dx; } set { this.dx = value; } }
        internal float Element5 { get { return this.dy; } set { this.dy = value; } }
        internal string ToCSSString()
        {
            return "matrix(" + this.Element0 + " " + this.Element1 + " " + this.Element2 + " " + this.Element3 + " " + this.Element4 + " " + this.Element5 + ")";
        }
        internal bool HasScale()
        {
            return this.m11 != 1 || this.m22 != 1;
        }
        internal PointF TransformPointF(float x, float y)
        {
            return new PointF(
                x * this.Element0 + y * this.Element2 + this.Element4,
                x * this.Element1 + y * this.Element3 + this.Element5
                );
        }

        /// <include file='doc\Matrix.uex' path='docs/doc[@for="Matrix.OffsetX"]/*' />
        /// <devdoc>
        ///    Gets the x translation value (the dx value,
        ///    or the element in the third row and first column) of this <see cref='System.Drawing.Drawing2D.Matrix'/>.
        /// </devdoc>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public float OffsetX
        {
            get { return dx; }
        }

        /// <include file='doc\Matrix.uex' path='docs/doc[@for="Matrix.OffsetY"]/*' />
        /// <devdoc>
        ///    Gets the y translation value (the dy
        ///    value, or the element in the third row and second column) of this <see cref='System.Drawing.Drawing2D.Matrix'/>.
        /// </devdoc>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public float OffsetY
        {
            get { return dy; }
        }

        /// <include file='doc\Matrix.uex' path='docs/doc[@for="Matrix.Reset"]/*' />
        /// <devdoc>
        ///    Resets this <see cref='System.Drawing.Drawing2D.Matrix'/> to identity.
        /// </devdoc>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void Reset()
        {
            m11 = 1f; m12 = 0f; m21 = 0f; m22 = 1f; dx = 0f; dy = 0f;
        }

        /// <include file='doc\Matrix.uex' path='docs/doc[@for="Matrix.Multiply"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Multiplies this <see cref='System.Drawing.Drawing2D.Matrix'/> by the specified <see cref='System.Drawing.Drawing2D.Matrix'/> by prepending the specified <see cref='System.Drawing.Drawing2D.Matrix'/>.
        ///    </para>
        /// </devdoc>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void Multiply(Matrix matrix)
        {
            Multiply(matrix, MatrixOrder.Prepend);
        }

        /// <include file='doc\Matrix.uex' path='docs/doc[@for="Matrix.Multiply1"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Multiplies this <see cref='System.Drawing.Drawing2D.Matrix'/> by the specified <see cref='System.Drawing.Drawing2D.Matrix'/> in the specified order.
        ///    </para>
        /// </devdoc>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void Multiply(Matrix matrix, MatrixOrder order)
        {
            if (matrix == null)
            {
                throw new ArgumentNullException("matrix");
            }
            // Compose transforms: result = (order == Prepend ? matrix * this : this * matrix)
            float a11 = m11, a12 = m12, a21 = m21, a22 = m22, adx = dx, ady = dy;
            float b11 = matrix.m11, b12 = matrix.m12, b21 = matrix.m21, b22 = matrix.m22, bdx = matrix.dx, bdy = matrix.dy;
            if (order == MatrixOrder.Prepend)
            {
                // M = B * A
                m11 = b11 * a11 + b21 * a12;
                m12 = b12 * a11 + b22 * a12;
                m21 = b11 * a21 + b21 * a22;
                m22 = b12 * a21 + b22 * a22;
                dx = b11 * adx + b21 * ady + bdx;
                dy = b12 * adx + b22 * ady + bdy;
            }
            else
            {
                // M = A * B
                m11 = a11 * b11 + a21 * b12;
                m12 = a12 * b11 + a22 * b12;
                m21 = a11 * b21 + a21 * b22;
                m22 = a12 * b21 + a22 * b22;
                dx = a11 * bdx + a21 * bdy + adx;
                dy = a12 * bdx + a22 * bdy + ady;
            }
        }

        /// <include file='doc\Matrix.uex' path='docs/doc[@for="Matrix.Translate"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Applies the specified translation vector to
        ///       the this <see cref='System.Drawing.Drawing2D.Matrix'/> by
        ///       prepending the translation vector.
        ///    </para>
        /// </devdoc>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void Translate(float offsetX, float offsetY)
        {
            Translate(offsetX, offsetY, MatrixOrder.Prepend);
        }

        /// <include file='doc\Matrix.uex' path='docs/doc[@for="Matrix.Translate1"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Applies the specified translation vector to
        ///       the this <see cref='System.Drawing.Drawing2D.Matrix'/> in the specified order.
        ///    </para>
        /// </devdoc>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void Translate(float offsetX, float offsetY, MatrixOrder order)
        {
            Matrix t = new Matrix(1, 0, 0, 1, offsetX, offsetY);
            Multiply(t, order);
        }

        /// <include file='doc\Matrix.uex' path='docs/doc[@for="Matrix.Scale"]/*' />
        /// <devdoc>
        ///    Applies the specified scale vector to this
        /// <see cref='System.Drawing.Drawing2D.Matrix'/> by prepending the scale vector.
        /// </devdoc>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void Scale(float scaleX, float scaleY)
        {
            Scale(scaleX, scaleY, MatrixOrder.Prepend);
        }

        /// <include file='doc\Matrix.uex' path='docs/doc[@for="Matrix.Scale1"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Applies the specified scale vector to this
        ///    <see cref='System.Drawing.Drawing2D.Matrix'/> using the specified order.
        ///    </para>
        /// </devdoc>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void Scale(float scaleX, float scaleY, MatrixOrder order)
        {
            Matrix s = new Matrix(scaleX, 0, 0, scaleY, 0, 0);
            Multiply(s, order);
        }

        /// <include file='doc\Matrix.uex' path='docs/doc[@for="Matrix.Rotate"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Rotates this <see cref='System.Drawing.Drawing2D.Matrix'/> clockwise about the
        ///       origin by the specified angle.
        ///    </para>
        /// </devdoc>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void Rotate(float angle)
        {
            Rotate(angle, MatrixOrder.Prepend);
        }

        /// <include file='doc\Matrix.uex' path='docs/doc[@for="Matrix.Rotate1"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Rotates this <see cref='System.Drawing.Drawing2D.Matrix'/> clockwise about the
        ///       origin by the specified
        ///       angle in the specified order.
        ///    </para>
        /// </devdoc>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void Rotate(float angle, MatrixOrder order)
        {
            double rad = angle * Math.PI / 180.0;
            float c = (float)Math.Cos(rad);
            float s = (float)Math.Sin(rad);
            Matrix r = new Matrix(c, s, -s, c, 0, 0);
            Multiply(r, order);
        }

        /// <include file='doc\Matrix.uex' path='docs/doc[@for="Matrix.RotateAt"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Applies a clockwise rotation about the
        ///       specified point to this <see cref='System.Drawing.Drawing2D.Matrix'/> by prepending the rotation.
        ///    </para>
        /// </devdoc>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void RotateAt(float angle, PointF point)
        {
            RotateAt(angle, point, MatrixOrder.Prepend);
        }

        /// <include file='doc\Matrix.uex' path='docs/doc[@for="Matrix.RotateAt1"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Applies a clockwise rotation about the specified point
        ///       to this <see cref='System.Drawing.Drawing2D.Matrix'/> in the
        ///       specified order.
        ///    </para>
        /// </devdoc>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void RotateAt(float angle, PointF point, MatrixOrder order)
        {
            if (order == MatrixOrder.Prepend)
            {
                Translate(point.X, point.Y, order);
                Rotate(angle, order);
                Translate(-point.X, -point.Y, order);
            }
            else
            {
                Translate(-point.X, -point.Y, order);
                Rotate(angle, order);
                Translate(point.X, point.Y, order);
            }
        }

        /// <include file='doc\Matrix.uex' path='docs/doc[@for="Matrix.Shear"]/*' />
        /// <devdoc>
        ///    Applies the specified shear
        ///    vector to this <see cref='System.Drawing.Drawing2D.Matrix'/> by prepending the shear vector.
        /// </devdoc>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void Shear(float shearX, float shearY)
        {
            Shear(shearX, shearY, MatrixOrder.Prepend);
        }

        /// <include file='doc\Matrix.uex' path='docs/doc[@for="Matrix.Shear1"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Applies the specified shear
        ///       vector to this <see cref='System.Drawing.Drawing2D.Matrix'/> in the specified order.
        ///    </para>
        /// </devdoc>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void Shear(float shearX, float shearY, MatrixOrder order)
        {
            Matrix sh = new Matrix(1f, shearY, shearX, 1f, 0f, 0f);
            Multiply(sh, order);
        }

        /// <include file='doc\Matrix.uex' path='docs/doc[@for="Matrix.Invert"]/*' />
        /// <devdoc>
        ///    Inverts this <see cref='System.Drawing.Drawing2D.Matrix'/>, if it is
        ///    invertible.
        /// </devdoc>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void Invert()
        {
            // Invert 2x2 linear part and translation
            float det = m11 * m22 - m12 * m21;
            if (Math.Abs(det) < 1e-12f)
            {
                throw new InvalidOperationException("Matrix not invertible");
            }
            float inv11 = m22 / det;
            float inv12 = -m12 / det;
            float inv21 = -m21 / det;
            float inv22 = m11 / det;
            float invdx = -(inv11 * dx + inv21 * dy);
            float invdy = -(inv12 * dx + inv22 * dy);
            m11 = inv11; m12 = inv12; m21 = inv21; m22 = inv22; dx = invdx; dy = invdy;
        }

        // float version
        /// <include file='doc\Matrix.uex' path='docs/doc[@for="Matrix.TransformPoints"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Applies the geometrical transform this <see cref='System.Drawing.Drawing2D.Matrix'/>represents to an
        ///       array of points.
        ///    </para>
        /// </devdoc>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void TransformPoints(PointF[] pts)
        {
            if (pts == null) throw new ArgumentNullException("pts");
            for (int i = 0; i < pts.Length; i++)
            {
                float x = pts[i].X;
                float y = pts[i].Y;
                pts[i] = new PointF(m11 * x + m21 * y + dx, m12 * x + m22 * y + dy);
            }
        }

        // int version
        /// <include file='doc\Matrix.uex' path='docs/doc[@for="Matrix.TransformPoints1"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Applies the geometrical transform this <see cref='System.Drawing.Drawing2D.Matrix'/> represents to an array of points.
        ///    </para>
        /// </devdoc>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void TransformPoints(Point[] pts)
        {
            if (pts == null) throw new ArgumentNullException("pts");
            for (int i = 0; i < pts.Length; i++)
            {
                float x = pts[i].X;
                float y = pts[i].Y;
                PointF p = new PointF(m11 * x + m21 * y + dx, m12 * x + m22 * y + dy);
                pts[i] = new Point((int)Math.Round(p.X), (int)Math.Round(p.Y));
            }
        }

        /// <include file='doc\Matrix.uex' path='docs/doc[@for="Matrix.TransformVectors"]/*' />
        /// <devdoc>
        ///    <para>[To be supplied.]</para>
        /// </devdoc>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void TransformVectors(PointF[] pts)
        {
            if (pts == null) throw new ArgumentNullException("pts");
            for (int i = 0; i < pts.Length; i++)
            {
                float x = pts[i].X;
                float y = pts[i].Y;
                pts[i] = new PointF(m11 * x + m21 * y, m12 * x + m22 * y);
            }
        }

        // int version
        /// <include file='doc\Matrix.uex' path='docs/doc[@for="Matrix.VectorTransformPoints"]/*' />
        /// <devdoc>
        ///    <para>[To be supplied.]</para>
        /// </devdoc>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void VectorTransformPoints(Point[] pts)
        {
            TransformVectors(pts);
        }

        /// <include file='doc\Matrix.uex' path='docs/doc[@for="Matrix.TransformVectors1"]/*' />
        /// <devdoc>
        ///    <para>[To be supplied.]</para>
        /// </devdoc>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void TransformVectors(Point[] pts)
        {
            if (pts == null) throw new ArgumentNullException("pts");
            for (int i = 0; i < pts.Length; i++)
            {
                float x = pts[i].X;
                float y = pts[i].Y;
                PointF p = new PointF(m11 * x + m21 * y, m12 * x + m22 * y);
                pts[i] = new Point((int)Math.Round(p.X), (int)Math.Round(p.Y));
            }
        }

        /// <include file='doc\Matrix.uex' path='docs/doc[@for="Matrix.IsInvertible"]/*' />
        /// <devdoc>
        ///    Gets a value indicating whether this
        /// <see cref='System.Drawing.Drawing2D.Matrix'/> is invertible.
        /// </devdoc>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public bool IsInvertible
        {
            get
            {
                float det = m11 * m22 - m12 * m21;
                return Math.Abs(det) > 1e-12f;
            }
        }

        /// <include file='doc\Matrix.uex' path='docs/doc[@for="Matrix.IsIdentity"]/*' />
        /// <devdoc>
        ///    Gets a value indicating whether this <see cref='System.Drawing.Drawing2D.Matrix'/> is the identity matrix.
        /// </devdoc>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public bool IsIdentity
        {
            get
            {
                return m11 == 1f && m12 == 0f && m21 == 0f && m22 == 1f && dx == 0f && dy == 0f;
            }
        }

        internal bool EqualsValue( Matrix m )
        {
            if(m == null)
            {
                return false;
            }
            if(m == this )
            {
                return true;
            }
            return m11 == m.m11 
                && m12 == m.m12
                && m21 == m.m21 
                && m22 == m.m22 
                && dx == m.dx 
                && dy == m.dy;
        }
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public override bool Equals(object obj)
        {
            Matrix matrix2 = obj as Matrix;
            if (matrix2 == null) return false;
            return m11 == matrix2.m11 && m12 == matrix2.m12 && m21 == matrix2.m21 && m22 == matrix2.m22 && dx == matrix2.dx && dy == matrix2.dy;
        }

        /// <include file='doc\Matrix.uex' path='docs/doc[@for="Matrix.GetHashCode"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Returns a hash code.
        ///    </para>
        /// </devdoc>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 31 + m11.GetHashCode();
                hash = hash * 31 + m12.GetHashCode();
                hash = hash * 31 + m21.GetHashCode();
                hash = hash * 31 + m22.GetHashCode();
                hash = hash * 31 + dx.GetHashCode();
                hash = hash * 31 + dy.GetHashCode();
                return hash;
            }
        }

        //internal Matrix(IntPtr nativeMatrix)
        //{
        //    // Legacy constructor; initialize identity
        //    Reset();
        //}

        //internal void SetNativeMatrix(IntPtr nativeMatrix)
        //{
        //    // No-op in managed implementation
        //}

    }
}
