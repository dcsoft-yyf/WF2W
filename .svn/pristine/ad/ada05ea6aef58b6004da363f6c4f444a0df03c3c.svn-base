using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace DCSoft.Drawing
{
    [System.Runtime.InteropServices.ComVisible(false)]
    public class DCMatrix
    {
        //public DCMatrix()
        //{

        //}
        public DCMatrix(System.Drawing.Drawing2D.Matrix m)
        {
            if (m == null)
            {
                throw new ArgumentNullException("m");
            }
            var vs = m.Elements;
            this.A = vs[0];
            this.B = vs[1];
            this.C = vs[2];
            this.D = vs[3];
            this.E = vs[4];
            this.F = vs[5];
            this.IsDefault = this.A == 1 && this.B == 0 && this.C == 0 && this.D == 1 && this.E == 0 && this.F == 0;
        }
        public readonly float A = 1;
        public readonly float B = 0;
        public readonly float C = 0;
        public readonly float D = 1;
        public readonly float E = 0;
        public readonly float F = 0;
        internal bool IsDefault = true;

        public void TransformPoints(PointF[] ps)
        {
            if (ps != null && ps.Length > 0 && this.IsDefault == false)
            {
                int len = ps.Length;
                for (int iCount = 0; iCount < len; iCount++)
                {
                    float x = ps[iCount].X;
                    float y = ps[iCount].Y;
                    ps[iCount].X = x * this.A + y * this.C + this.E;
                    ps[iCount].Y = x * this.B + y * this.D + this.F;
                    //ps[iCount] = new PointF(x * this.A + y * this.C + this.E, x * this.B + y * this.D + this.F);
                }
            }
        }

        public void UnTransformPoints(PointF[] ps)
        {
            if (ps != null && ps.Length > 0 && this.IsDefault == false)
            {
                int len = ps.Length;
                for (int iCount = 0; iCount < len; iCount++)
                {
                    float x = ps[iCount].X;
                    float y = ps[iCount].Y;
                    x -= this.E;// vs[4];
                    y -= this.F;// vs[5];
                    x = x / this.A;// vs[0];
                    y = y / this.A;// vs[0];
                    ps[iCount].X = x;
                    ps[iCount].Y = y;
                }
            }
        }

        public void TransformPoint(ref float x, ref float y)
        {
            if (this.IsDefault == false)
            {
                x = x * this.A + y * this.C + this.E;
                y = x * this.B + y * this.D + this.F;
            }
        }

        public void UnTransformPoint(ref float x, ref float y)
        {
            if (this.IsDefault == false)
            {
                x -= this.E;// vs[4];
                y -= this.F;// vs[5];
                x = x / this.A;// vs[0];
                y = y / this.A;// vs[0];
            }
        }
    }
}
