//------------------------------------------------------------------------------
// <copyright file="TextureBrush.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>                                                                
//------------------------------------------------------------------------------

namespace System.Drawing {
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;

    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = false )]
    public sealed class TextureBrush : Brush {
        internal string SVGID = null;
        internal string ToCanvasString()
        {
            var strCode = new System.Text.StringBuilder();
            strCode.Append("@");
            if(this._Image != null )
            {
                
            }
             
            strCode.Append('$');
            strCode.Append((int)this.WrapMode);
            var strText2 = strCode.ToString();
            return strText2;
        }

        public TextureBrush(Image bitmap)  
            : this(bitmap, System.Drawing.Drawing2D.WrapMode.Tile) {
        }

        public TextureBrush(Image image, WrapMode wrapMode) {
            if (image == null)
                throw new ArgumentNullException("image");
                
            this._Image = image;
            this._WrapMode = wrapMode;
        }
        public TextureBrush(Image image, WrapMode wrapMode, RectangleF dstRect)
        {
            if (image == null)
                throw new ArgumentNullException("image");

            this._Image = image;
            this._WrapMode = wrapMode;
            this._DstRect = new Rectangle((int)dstRect.X, (int)dstRect.Y,
                                        (int)dstRect.Width, (int)dstRect.Height);
        }

        public TextureBrush(Image image, WrapMode wrapMode, Rectangle dstRect) {
            this._Image = image;
            this._WrapMode = wrapMode;
            this._DstRect = dstRect;
        }
        public TextureBrush(Image image, RectangleF dstRect)
        : this(image, dstRect, (ImageAttributes)null) {}
         
        public TextureBrush(Image image, RectangleF dstRect,
                            ImageAttributes imageAttr)
        {
            if (image == null)
                throw new ArgumentNullException("image");

            this._Image = image;
            this._DstRect = new Rectangle((int)dstRect.X, (int)dstRect.Y,
                                        (int)dstRect.Width, (int)dstRect.Height);
        }
        public TextureBrush(Image image, Rectangle dstRect)
        : this(image, dstRect, (ImageAttributes)null) {}
         
        public TextureBrush(Image image, Rectangle dstRect,
                            ImageAttributes imageAttr)
        {
            this._Image = image;
            this._DstRect = dstRect;
        }
        private Rectangle _DstRect;
        
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public override Object Clone() {
            var result = (TextureBrush) this.MemberwiseClone();
            result._Transform = this._Transform?.Clone();
            //result._Image = this._Image?.Clone();
            return result;
        }


        private Matrix _Transform = new Matrix();
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public Matrix Transform
        {
            get { return this._Transform; }
            set {
                if (value == null) {
                    throw new ArgumentNullException("value");
                }
                this._Transform = value;
            }
        }

        private WrapMode _WrapMode = WrapMode.Tile;
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public WrapMode WrapMode
        {
            get {
                return this._WrapMode;
            }
            set {
                this._WrapMode = value;
            }
        }

        private Image _Image;
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public Image Image {
            get {
                return this._Image;
            }
        }
        
        
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void ResetTransform()
        {
            this._Transform?.Reset();
        }
        
        
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void MultiplyTransform(Matrix matrix)
        { MultiplyTransform(matrix, MatrixOrder.Prepend); }
        
        
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void MultiplyTransform(Matrix matrix, MatrixOrder order)
        {
            this._Transform.Multiply(matrix, order);
        }

        /// <include file='doc\TextureBrush.uex' path='docs/doc[@for="TextureBrush.TranslateTransform"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Translates the local geometrical transform by the specified dimmensions. This
        ///       method prepends the translation to the transform.
        ///    </para>
        /// </devdoc>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void TranslateTransform(float dx, float dy)
        { TranslateTransform(dx, dy, MatrixOrder.Prepend); }
                
        /// <include file='doc\TextureBrush.uex' path='docs/doc[@for="TextureBrush.TranslateTransform1"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Translates the local geometrical transform by the specified dimmensions in
        ///       the specified order.
        ///    </para>
        /// </devdoc>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void TranslateTransform(float dx, float dy, MatrixOrder order)
        {
            this._Transform.Translate(dx, dy, order);
        }
       
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void ScaleTransform(float sx, float sy)
        { ScaleTransform(sx, sy, MatrixOrder.Prepend); }
                
        /// <include file='doc\TextureBrush.uex' path='docs/doc[@for="TextureBrush.ScaleTransform1"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Scales the local geometric transform by the specified amounts in the
        ///       specified order.
        ///    </para>
        /// </devdoc>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void ScaleTransform(float sx, float sy, MatrixOrder order)
        {
            this._Transform.Scale( sx, sy, order);
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void RotateTransform(float angle)
        { RotateTransform(angle, MatrixOrder.Prepend); }
                
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void RotateTransform(float angle, MatrixOrder order)
        {
            this._Transform.Rotate(angle, order);
        }
    }
}

