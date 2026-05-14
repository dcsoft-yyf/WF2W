//------------------------------------------------------------------------------
// <copyright file="GraphicsState.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>                                                                
//------------------------------------------------------------------------------

namespace System.Drawing.Drawing2D {

    using System.Diagnostics;

    using System;

    /// <include file='doc\GraphicsState.uex' path='docs/doc[@for="GraphicsState"]/*' />
    /// <devdoc>
    ///    <para>[To be supplied.]</para>
    /// </devdoc>
    public sealed class CanvasGraphicsState : GraphicsState {
        internal CanvasGraphicsState(CanvasGraphics g)
        {
            this._Unit = g.PageUnit;
            this._ClipRectangle = g.ClipBounds;
            if (g.Transform.IsIdentity == false)
            {
                this._Matrix = g.Transform.Clone();
                if (this._Matrix.OffsetX != (int)this._Matrix.OffsetX)
                {
                    this._ClipRectangle.Width += 1;
                }
            }
        }
        internal void Restore(CanvasGraphics g)
        {
            g.PageUnit = this._Unit;
            g.Transform = this._Matrix;
            g.ResetClip();
            //g.SetClip(this._ClipRectangle);
        }
        private Matrix _Matrix = null;
        private GraphicsUnit _Unit = GraphicsUnit.Document;
        private RectangleF _ClipRectangle = RectangleF.Empty;
    }
}

