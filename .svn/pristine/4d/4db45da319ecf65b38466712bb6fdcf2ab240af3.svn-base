	//------------------------------------------------------------------------------
// <copyright file="Region.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>                                                                
//------------------------------------------------------------------------------

namespace System.Drawing {
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Drawing.Drawing2D;
    using System.IO;
    using System.Runtime.Versioning;

    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = false )]
    public sealed class Region : MarshalByRefObject, IDisposable {
        static Region()
        {
            DCValueConvert.CheckIsBlazorWASM();
        }
        private const float LargeBoundsExtent = 1000000000f;
        private static readonly object HrgnSync = new object();
        private static readonly Dictionary<int, Region> HrgnMap = new Dictionary<int, Region>();
        private static int NextHrgn = 1;

        private List<RectangleF> _rects;
        private List<RectangleF> _exclusions;
        private bool _isInfinite;
        private bool _disposed;

        /// <include file='doc\Region.uex' path='docs/doc[@for="Region.Region"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Initializes a new instance of the <see cref='System.Drawing.Region'/> class.
        ///    </para>
        /// </devdoc>
        public Region() {
            _isInfinite = true;
        }

        /// <include file='doc\Region.uex' path='docs/doc[@for="Region.Region1"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Initializes a new instance of the <see cref='System.Drawing.Region'/> class from the specified <see cref='System.Drawing.RectangleF'/> .
        ///    </para>
        /// </devdoc>
        public Region(RectangleF rect) {
            _isInfinite = false;
            _rects = new List<RectangleF>();
            AddRect(rect);
        }

        /// <include file='doc\Region.uex' path='docs/doc[@for="Region.Region2"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Initializes a new instance of the <see cref='System.Drawing.Region'/> class from the specified <see cref='System.Drawing.Rectangle'/>.
        ///    </para>
        /// </devdoc>
        public Region(Rectangle rect) : this(new RectangleF(rect.X, rect.Y, rect.Width, rect.Height)) {
        }

        /// <include file='doc\Region.uex' path='docs/doc[@for="Region.Region3"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Initializes a new instance of the <see cref='System.Drawing.Region'/> class
        ///       with the specified <see cref='System.Drawing.Drawing2D.GraphicsPath'/>.
        ///    </para>
        /// </devdoc>
        public Region(GraphicsPath path) {
            if (path == null)
                throw new ArgumentNullException("path");

            _isInfinite = false;
            _rects = new List<RectangleF>();
            AddRect(path.GetBounds());
        }

        /// <include file='doc\Region.uex' path='docs/doc[@for="Region.Region4"]/*' />
        /// <devdoc>
        ///    Initializes a new instance of the <see cref='System.Drawing.Region'/> class
        ///    from the specified data.
        /// </devdoc>
        public Region(RegionData rgnData) {
            if (rgnData == null)
                throw new ArgumentNullException("rgnData");

            InitializeFromRegionData(rgnData.Data);
        }

        internal Region(IntPtr nativeRegion) {
            _isInfinite = true;
        }
        internal bool IsSingleRectangleF()
        {
            if(this._exclusions != null && this._exclusions.Count > 0 )
            {
                return false;
            }
            return this._rects != null && this._rects.Count == 1;
        }
        public RectangleF GetSingleRectangleF()
        {
            if(this._rects != null && this._rects.Count == 1 )
            {
                return this._rects[0];
            }
            else
            {
                return RectangleF.Empty;
            }
        }
        /// <include file='doc\Region.uex' path='docs/doc[@for="Region.FromHrgn"]/*' />
        /// <devdoc>
        ///    Initializes a new instance of the <see cref='System.Drawing.Region'/> class
        ///    from the specified existing <see cref='System.Drawing.Region'/>.
        /// </devdoc>
        public static Region FromHrgn(IntPtr hrgn) {
            if (hrgn == IntPtr.Zero)
                return new Region();

            int id = hrgn.ToInt32();
            lock (HrgnSync) {
                if (HrgnMap.TryGetValue(id, out var region)) {
                    return region.Clone();
                }
            }

            return new Region();
        }

        /**
         * Make a copy of the region object
         */
        /// <include file='doc\Region.uex' path='docs/doc[@for="Region.Clone"]/*' />
        /// <devdoc>
        ///    Creates an exact copy if this <see cref='System.Drawing.Region'/>.
        /// </devdoc>
        [ResourceExposure(ResourceScope.Process)]
        [ResourceConsumption(ResourceScope.Process)]
        public Region Clone() {
            EnsureNotDisposed();
            var clone = new Region();
            clone._isInfinite = _isInfinite;
            clone._rects = CloneRects(_rects);
            clone._exclusions = CloneRects(_exclusions);
            return clone;
        }

        /**
         * Dispose of resources associated with the
         */
        /// <include file='doc\Region.uex' path='docs/doc[@for="Region.Dispose"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Cleans up Windows resources for this
        ///    <see cref='System.Drawing.Region'/>.
        ///    </para>
        /// </devdoc>
        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        void Dispose(bool disposing) {
            _disposed = true;
            _rects = null;
            _exclusions = null;
        }

        /// <include file='doc\Region.uex' path='docs/doc[@for="Region.Finalize"]/*' />
        /// <devdoc>
        ///    Cleans up Windows resources for this
        /// <see cref='System.Drawing.Region'/>.
        /// </devdoc>
        ~Region() {
            Dispose(false);
        }

        /*
         * Region operations
         */

        /// <include file='doc\Region.uex' path='docs/doc[@for="Region.MakeInfinite"]/*' />
        /// <devdoc>
        ///    Initializes this <see cref='System.Drawing.Region'/> to an
        ///    infinite interior.
        /// </devdoc>
        public void MakeInfinite() {
            EnsureNotDisposed();
            _isInfinite = true;
            _rects = null;
            _exclusions = null;
        }

        /// <include file='doc\Region.uex' path='docs/doc[@for="Region.MakeEmpty"]/*' />
        /// <devdoc>
        ///    Initializes this <see cref='System.Drawing.Region'/> to an
        ///    empty interior.
        /// </devdoc>
        public void MakeEmpty() {
            EnsureNotDisposed();
            _isInfinite = false;
            _rects = new List<RectangleF>();
            _exclusions = null;
        }

        // float version
        /// <include file='doc\Region.uex' path='docs/doc[@for="Region.Intersect"]/*' />
        /// <devdoc>
        ///    Updates this <see cref='System.Drawing.Region'/> to the intersection of itself
        ///    with the specified <see cref='System.Drawing.RectangleF'/>.
        /// </devdoc>
        public void Intersect(RectangleF rect) {
            EnsureNotDisposed();
            if (_isInfinite) {
                _isInfinite = false;
                _rects = new List<RectangleF>();
                AddRect(rect);
                return;
            }
            if (_rects == null || _rects.Count == 0) {
                return;
            }
            var result = new List<RectangleF>();
            foreach (var r in _rects) {
                var inter = IntersectRect(r, rect);
                if (!IsEmptyRect(inter)) {
                    result.Add(inter);
                }
            }
            _rects = result;
        }

        // int version
        /// <include file='doc\Region.uex' path='docs/doc[@for="Region.Intersect1"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Updates this <see cref='System.Drawing.Region'/> to the intersection of itself with the specified
        ///    <see cref='System.Drawing.Rectangle'/>.
        ///    </para>
        /// </devdoc>
        public void Intersect(Rectangle rect) {
            Intersect(new RectangleF(rect.X, rect.Y, rect.Width, rect.Height));
        }

        /// <include file='doc\Region.uex' path='docs/doc[@for="Region.Intersect2"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Updates this <see cref='System.Drawing.Region'/> to the intersection of itself with the specified
        ///    <see cref='System.Drawing.Drawing2D.GraphicsPath'/>. 
        ///    </para>
        /// </devdoc>
        public void Intersect(GraphicsPath path) {
            if (path == null)
                throw new ArgumentNullException("path");
            Intersect(path.GetBounds());
        }

        /// <include file='doc\Region.uex' path='docs/doc[@for="Region.Intersect3"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Updates this <see cref='System.Drawing.Region'/> to the intersection of itself with the specified
        ///    <see cref='System.Drawing.Region'/>. 
        ///    </para>
        /// </devdoc>
        public void Intersect(Region region) {
            if (region == null)
                throw new ArgumentNullException("region");

            EnsureNotDisposed();
            region.EnsureNotDisposed();

            if (region._isInfinite) {
                if (region._exclusions != null && region._exclusions.Count > 0) {
                    ExcludeRects(region._exclusions);
                }
                return;
            }

            if (_isInfinite) {
                _isInfinite = false;
                _rects = CloneRects(region._rects) ?? new List<RectangleF>();
                return;
            }

            if (_rects == null || _rects.Count == 0 || region._rects == null || region._rects.Count == 0) {
                MakeEmpty();
                return;
            }

            var result = new List<RectangleF>();
            foreach (var r in _rects) {
                foreach (var c in region._rects) {
                    var inter = IntersectRect(r, c);
                    if (!IsEmptyRect(inter)) {
                        result.Add(inter);
                    }
                }
            }
            _rects = result;
        }

        /// <include file='doc\Region.uex' path='docs/doc[@for="Region.ReleaseHrgn"]/*' />
        /// <devdoc>
        ///     Releases the handle to the region handle.
        /// </devdoc>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public void ReleaseHrgn(IntPtr regionHandle) {
            if (regionHandle == IntPtr.Zero)
                return;

            int id = regionHandle.ToInt32();
            lock (HrgnSync) {
                HrgnMap.Remove(id);
            }
        }

        // float version
        /// <include file='doc\Region.uex' path='docs/doc[@for="Region.Union"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Updates this <see cref='System.Drawing.Region'/> to the union of itself and the
        ///       specified <see cref='System.Drawing.RectangleF'/>.
        ///    </para>
        /// </devdoc>
        public void Union(RectangleF rect) {
            EnsureNotDisposed();
            if (_isInfinite) {
                return;
            }
            if (_rects == null) {
                _rects = new List<RectangleF>();
            }
            AddRect(rect);
        }

        // int version
        /// <include file='doc\Region.uex' path='docs/doc[@for="Region.Union1"]/*' />
        /// <devdoc>
        ///    Updates this <see cref='System.Drawing.Region'/> to the union of itself and the
        ///    specified <see cref='System.Drawing.Rectangle'/>.
        /// </devdoc>
        public void Union(Rectangle rect) {
            Union(new RectangleF(rect.X, rect.Y, rect.Width, rect.Height));
        }

        /// <include file='doc\Region.uex' path='docs/doc[@for="Region.Union2"]/*' />
        /// <devdoc>
        ///    Updates this <see cref='System.Drawing.Region'/> to the union of itself and the
        ///    specified <see cref='System.Drawing.Drawing2D.GraphicsPath'/>.
        /// </devdoc>
        public void Union(GraphicsPath path) {
            if (path == null)
                throw new ArgumentNullException("path");
            Union(path.GetBounds());
        }

        /// <include file='doc\Region.uex' path='docs/doc[@for="Region.Union3"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Updates this <see cref='System.Drawing.Region'/> to the union of itself and the specified <see cref='System.Drawing.Region'/>.
        ///    </para>
        /// </devdoc>
        public void Union(Region region) {
            if (region == null)
                throw new ArgumentNullException("region");

            EnsureNotDisposed();
            region.EnsureNotDisposed();

            if (_isInfinite || region._isInfinite) {
                _isInfinite = true;
                _rects = null;
                _exclusions = null;
                return;
            }

            if (region._rects == null || region._rects.Count == 0) {
                return;
            }
            if (_rects == null) {
                _rects = new List<RectangleF>();
            }
            _rects.AddRange(region._rects);
        }

        // float version
        /// <include file='doc\Region.uex' path='docs/doc[@for="Region.Xor"]/*' />
        /// <devdoc>
        ///    Updates this <see cref='System.Drawing.Region'/> to the union minus the
        ///    intersection of itself with the specified <see cref='System.Drawing.RectangleF'/>.
        /// </devdoc>
        public void Xor(RectangleF rect) {
            Xor(new Region(rect));
        }

        // int version
        /// <include file='doc\Region.uex' path='docs/doc[@for="Region.Xor1"]/*' />
        /// <devdoc>
        ///    Updates this <see cref='System.Drawing.Region'/> to the union minus the
        ///    intersection of itself with the specified <see cref='System.Drawing.Rectangle'/>.
        /// </devdoc>
        public void Xor(Rectangle rect) {
            Xor(new RectangleF(rect.X, rect.Y, rect.Width, rect.Height));
        }

        /// <include file='doc\Region.uex' path='docs/doc[@for="Region.Xor2"]/*' />
        /// <devdoc>
        ///    Updates this <see cref='System.Drawing.Region'/> to the union minus the
        ///    intersection of itself with the specified <see cref='System.Drawing.Drawing2D.GraphicsPath'/>.
        /// </devdoc>
        public void Xor(GraphicsPath path) {
            if (path == null)
                throw new ArgumentNullException("path");
            Xor(path.GetBounds());
        }

        /// <include file='doc\Region.uex' path='docs/doc[@for="Region.Xor3"]/*' />
        /// <devdoc>
        ///    Updates this <see cref='System.Drawing.Region'/> to the union minus the
        ///    intersection of itself with the specified <see cref='System.Drawing.Region'/>.
        /// </devdoc>
        public void Xor(Region region) {
            if (region == null)
                throw new ArgumentNullException("region");

            EnsureNotDisposed();
            region.EnsureNotDisposed();

            var left = Clone();
            left.Exclude(region);
            var right = region.Clone();
            right.Exclude(this);
            left.Union(right);
            CopyFrom(left);
        }

        // float version
        /// <include file='doc\Region.uex' path='docs/doc[@for="Region.Exclude"]/*' />
        /// <devdoc>
        ///    Updates this <see cref='System.Drawing.Region'/> to the portion of its interior
        ///    that does not intersect with the specified <see cref='System.Drawing.RectangleF'/>.
        /// </devdoc>
        public void Exclude(RectangleF rect) {
            EnsureNotDisposed();
            if (_isInfinite) {
                if (_exclusions == null) {
                    _exclusions = new List<RectangleF>();
                }
                AddRect(rect, _exclusions);
                return;
            }

            if (_rects == null || _rects.Count == 0) {
                return;
            }

            _rects = SubtractRects(_rects, rect);
        }

        // int version
        /// <include file='doc\Region.uex' path='docs/doc[@for="Region.Exclude1"]/*' />
        /// <devdoc>
        ///    Updates this <see cref='System.Drawing.Region'/> to the portion of its interior
        ///    that does not intersect with the specified <see cref='System.Drawing.Rectangle'/>.
        /// </devdoc>
        public void Exclude(Rectangle rect) {
            Exclude(new RectangleF(rect.X, rect.Y, rect.Width, rect.Height));
        }

        /// <include file='doc\Region.uex' path='docs/doc[@for="Region.Exclude2"]/*' />
        /// <devdoc>
        ///    Updates this <see cref='System.Drawing.Region'/> to the portion of its interior
        ///    that does not intersect with the specified <see cref='System.Drawing.Drawing2D.GraphicsPath'/>.
        /// </devdoc>
        public void Exclude(GraphicsPath path) {
            if (path == null)
                throw new ArgumentNullException("path");
            Exclude(path.GetBounds());
        }

        /// <include file='doc\Region.uex' path='docs/doc[@for="Region.Exclude3"]/*' />
        /// <devdoc>
        ///    Updates this <see cref='System.Drawing.Region'/> to the portion of its interior
        ///    that does not intersect with the specified <see cref='System.Drawing.Region'/>.
        /// </devdoc>
        public void Exclude(Region region) {
            if (region == null)
                throw new ArgumentNullException("region");

            EnsureNotDisposed();
            region.EnsureNotDisposed();

            if (region._isInfinite) {
                MakeEmpty();
                return;
            }

            if (region._rects == null || region._rects.Count == 0) {
                return;
            }

            if (_isInfinite) {
                if (_exclusions == null) {
                    _exclusions = new List<RectangleF>();
                }
                _exclusions.AddRange(region._rects);
                return;
            }

            ExcludeRects(region._rects);
        }

        // float version
        /// <include file='doc\Region.uex' path='docs/doc[@for="Region.Complement"]/*' />
        /// <devdoc>
        ///    Updates this <see cref='System.Drawing.Region'/> to the portion of the
        ///    specified <see cref='System.Drawing.RectangleF'/> that does not intersect with this <see cref='System.Drawing.Region'/>.
        /// </devdoc>
        public void Complement(RectangleF rect) {
            var temp = new Region(rect);
            temp.Exclude(this);
            CopyFrom(temp);
        }

        // int version
        /// <include file='doc\Region.uex' path='docs/doc[@for="Region.Complement1"]/*' />
        /// <devdoc>
        ///    Updates this <see cref='System.Drawing.Region'/> to the portion of the
        ///    specified <see cref='System.Drawing.Rectangle'/> that does not intersect with this <see cref='System.Drawing.Region'/>.
        /// </devdoc>
        public void Complement(Rectangle rect) {
            Complement(new RectangleF(rect.X, rect.Y, rect.Width, rect.Height));
        }

        /// <include file='doc\Region.uex' path='docs/doc[@for="Region.Complement2"]/*' />
        /// <devdoc>
        ///    Updates this <see cref='System.Drawing.Region'/> to the portion of the
        ///    specified <see cref='System.Drawing.Drawing2D.GraphicsPath'/> that does not intersect with this
        /// <see cref='System.Drawing.Region'/>.
        /// </devdoc>
        public void Complement(GraphicsPath path) {
            if (path == null)
                throw new ArgumentNullException("path");
            Complement(path.GetBounds());
        }

        /// <include file='doc\Region.uex' path='docs/doc[@for="Region.Complement3"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Updates this <see cref='System.Drawing.Region'/> to the portion of the
        ///       specified <see cref='System.Drawing.Region'/> that does not intersect with this <see cref='System.Drawing.Region'/>.
        ///    </para>
        /// </devdoc>
        public void Complement(Region region) {
            if (region == null)
                throw new ArgumentNullException("region");
            var temp = region.Clone();
            temp.Exclude(this);
            CopyFrom(temp);
        }

        /**
         * Transform operations
         */
        /// <include file='doc\Region.uex' path='docs/doc[@for="Region.Translate"]/*' />
        /// <devdoc>
        ///    Offsets the coordinates of this <see cref='System.Drawing.Region'/> by the
        ///    specified amount.
        /// </devdoc>
        public void Translate(float dx, float dy) {
            EnsureNotDisposed();
            if (_rects != null) {
                for (int i = 0; i < _rects.Count; i++) {
                    var r = _rects[i];
                    r.X += dx;
                    r.Y += dy;
                    _rects[i] = r;
                }
            }
            if (_exclusions != null) {
                for (int i = 0; i < _exclusions.Count; i++) {
                    var r = _exclusions[i];
                    r.X += dx;
                    r.Y += dy;
                    _exclusions[i] = r;
                }
            }
        }

        /// <include file='doc\Region.uex' path='docs/doc[@for="Region.Translate1"]/*' />
        /// <devdoc>
        ///    Offsets the coordinates of this <see cref='System.Drawing.Region'/> by the
        ///    specified amount.
        /// </devdoc>
        public void Translate(int dx, int dy) {
            Translate((float)dx, (float)dy);
        }

        /// <include file='doc\Region.uex' path='docs/doc[@for="Region.Transform"]/*' />
        /// <devdoc>
        ///    Transforms this <see cref='System.Drawing.Region'/> by the
        ///    specified <see cref='System.Drawing.Drawing2D.Matrix'/>.
        /// </devdoc>
        public void Transform(Matrix matrix) {
            if (matrix == null)
                throw new ArgumentNullException("matrix");

            EnsureNotDisposed();

            if (_rects != null) {
                for (int i = 0; i < _rects.Count; i++) {
                    _rects[i] = TransformRect(_rects[i], matrix);
                }
            }
            if (_exclusions != null) {
                for (int i = 0; i < _exclusions.Count; i++) {
                    _exclusions[i] = TransformRect(_exclusions[i], matrix);
                }
            }
        }

        /**
         * Get region attributes
         */
        /// <include file='doc\Region.uex' path='docs/doc[@for="Region.GetBounds"]/*' />
        /// <devdoc>
        ///    Returns a <see cref='System.Drawing.RectangleF'/> that represents a rectangular
        ///    region that bounds this <see cref='System.Drawing.Region'/> on the drawing surface of a <see cref='System.Drawing.Graphics'/>.
        /// </devdoc>
        public RectangleF GetBounds(Graphics g) {
            if (g == null)
                throw new ArgumentNullException("g");

            EnsureNotDisposed();
            return GetBoundsInternal();
        }

        /// <include file='doc\Region.uex' path='docs/doc[@for="Region.GetHrgn"]/*' />
        /// <devdoc>
        ///    Returns a Windows handle to this <see cref='System.Drawing.Region'/> in the
        ///    specified graphics context.
        ///    
        ///    Remarks from MSDN: 
        ///         It is the caller's responsibility to call the GDI function 
        ///         DeleteObject to free the GDI region when it is no longer needed.
        /// </devdoc>
        public IntPtr GetHrgn(Graphics g) {
            if (g == null)
                throw new ArgumentNullException("g");

            EnsureNotDisposed();

            int id;
            lock (HrgnSync) {
                id = NextHrgn++;
                HrgnMap[id] = Clone();
            }
            return new IntPtr(id);
        }

        /// <include file='doc\Region.uex' path='docs/doc[@for="Region.IsEmpty"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Tests whether this <see cref='System.Drawing.Region'/> has an
        ///       empty interior on the specified drawing surface.
        ///    </para>
        /// </devdoc>
        public bool IsEmpty(Graphics g) {
            if (g == null)
                throw new ArgumentNullException("g");

            EnsureNotDisposed();
            return !_isInfinite && (_rects == null || _rects.Count == 0);
        }

        /// <include file='doc\Region.uex' path='docs/doc[@for="Region.IsInfinite"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Tests whether this <see cref='System.Drawing.Region'/> has
        ///       an infinite interior on the specified drawing surface.
        ///    </para>
        /// </devdoc>
        public bool IsInfinite(Graphics g) {
            if (g == null)
                throw new ArgumentNullException("g");

            EnsureNotDisposed();
            return _isInfinite;
        }

        /// <include file='doc\Region.uex' path='docs/doc[@for="Region.Equals"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Tests whether the specified <see cref='System.Drawing.Region'/> is
        ///       identical to this <see cref='System.Drawing.Region'/>
        ///       on the specified drawing surface.
        ///    </para>
        /// </devdoc>
        public bool Equals(Region region, Graphics g) {
            if (g == null)
                throw new ArgumentNullException("g");

            if (region == null)
                throw new ArgumentNullException("region");

            EnsureNotDisposed();
            region.EnsureNotDisposed();

            if (_isInfinite != region._isInfinite) {
                return false;
            }
            if (!RectListsEqual(_rects, region._rects)) {
                return false;
            }
            if (!RectListsEqual(_exclusions, region._exclusions)) {
                return false;
            }
            return true;
        }

        /// <include file='doc\Region.uex' path='docs/doc[@for="Region.GetRegionData"]/*' />
        /// <devdoc>
        ///    Returns a <see cref='System.Drawing.Drawing2D.RegionData'/> that represents the
        ///    information that describes this <see cref='System.Drawing.Region'/>.
        /// </devdoc>
        public RegionData GetRegionData() {
            EnsureNotDisposed();
            using var stream = new MemoryStream();
            using var writer = new BinaryWriter(stream);
            writer.Write((byte)1);
            writer.Write(_isInfinite);
            WriteRectList(writer, _rects);
            WriteRectList(writer, _exclusions);
            writer.Flush();
            return new RegionData(stream.ToArray());
        }

        /*
         * Hit testing operations
         */
        // float version
        /// <include file='doc\Region.uex' path='docs/doc[@for="Region.IsVisible"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Tests whether the specified point is
        ///       contained within this <see cref='System.Drawing.Region'/> in the specified graphics context.
        ///    </para>
        /// </devdoc>
        public bool IsVisible(float x, float y) {
            return IsVisible(new PointF(x, y), null);
        }
        
        /// <include file='doc\Region.uex' path='docs/doc[@for="Region.IsVisible1"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Tests whether the specified <see cref='System.Drawing.PointF'/> is contained within this <see cref='System.Drawing.Region'/>.
        ///    </para>
        /// </devdoc>
        public bool IsVisible(PointF point) {
            return IsVisible(point, null);
        }
        
        /// <include file='doc\Region.uex' path='docs/doc[@for="Region.IsVisible2"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Tests whether the specified point is contained within this <see cref='System.Drawing.Region'/> in the
        ///       specified graphics context.
        ///    </para>
        /// </devdoc>
        public bool IsVisible(float x, float y, Graphics g) {
            return IsVisible(new PointF(x, y), g);
        }
        
        /// <include file='doc\Region.uex' path='docs/doc[@for="Region.IsVisible3"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Tests whether the specified <see cref='System.Drawing.PointF'/> is
        ///       contained within this <see cref='System.Drawing.Region'/> in the specified graphics context.
        ///    </para>
        /// </devdoc>
        public bool IsVisible(PointF point, Graphics g) {
            EnsureNotDisposed();
            if (_isInfinite) {
                return !IsExcluded(point);
            }
            if (_rects == null || _rects.Count == 0) {
                return false;
            }
            foreach (var r in _rects) {
                if (r.Contains(point)) {
                    return true;
                }
            }
            return false;
        }

        /// <include file='doc\Region.uex' path='docs/doc[@for="Region.IsVisible4"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Tests whether the specified rectangle is contained within this <see cref='System.Drawing.Region'/>
        ///       .
        ///    </para>
        /// </devdoc>
        public bool IsVisible(float x, float y, float width, float height) {
            return IsVisible(new RectangleF(x, y, width, height), null);
        }
        
        /// <include file='doc\Region.uex' path='docs/doc[@for="Region.IsVisible5"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Tests whether the specified <see cref='System.Drawing.RectangleF'/> is contained within this
        ///    <see cref='System.Drawing.Region'/>. 
        ///    </para>
        /// </devdoc>
        public bool IsVisible(RectangleF rect) {
            return IsVisible(rect, null);
        }
            
        /// <include file='doc\Region.uex' path='docs/doc[@for="Region.IsVisible6"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Tests whether the specified rectangle is contained within this <see cref='System.Drawing.Region'/> in the
        ///       specified graphics context.
        ///    </para>
        /// </devdoc>
        public bool IsVisible(float x, float y, float width, float height, Graphics g) {
            return IsVisible(new RectangleF(x, y, width, height), g);
        }
        
        /// <include file='doc\Region.uex' path='docs/doc[@for="Region.IsVisible7"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Tests whether the specified <see cref='System.Drawing.RectangleF'/> is contained within this <see cref='System.Drawing.Region'/> in the specified graphics context.
        ///    </para>
        /// </devdoc>
        public bool IsVisible(RectangleF rect, Graphics g) {
            EnsureNotDisposed();
            if (_isInfinite) {
                return !IsExcluded(rect);
            }
            if (_rects == null || _rects.Count == 0) {
                return false;
            }
            foreach (var r in _rects) {
                if (!IsEmptyRect(IntersectRect(r, rect))) {
                    return true;
                }
            }
            return false;
        }

        // int version
        /// <include file='doc\Region.uex' path='docs/doc[@for="Region.IsVisible8"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Tests whether the specified point is contained within this <see cref='System.Drawing.Region'/> in the
        ///       specified graphics context.
        ///    </para>
        /// </devdoc>
        public bool IsVisible(int x, int y, Graphics g) {
            return IsVisible(new Point(x, y), g);
        }
        
        /// <include file='doc\Region.uex' path='docs/doc[@for="Region.IsVisible9"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Tests whether the specified <see cref='System.Drawing.Point'/> is contained within this <see cref='System.Drawing.Region'/>.
        ///    </para>
        /// </devdoc>
        public bool IsVisible(Point point) {
            return IsVisible(point, null);
        }
        
        /// <include file='doc\Region.uex' path='docs/doc[@for="Region.IsVisible10"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Tests whether the specified <see cref='System.Drawing.Point'/> is contained within this
        ///    <see cref='System.Drawing.Region'/> in the specified 
        ///       graphics context.
        ///    </para>
        /// </devdoc>
        public bool IsVisible(Point point, Graphics g) {
            return IsVisible(new PointF(point.X, point.Y), g);
        }

        /// <include file='doc\Region.uex' path='docs/doc[@for="Region.IsVisible11"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Tests whether the specified rectangle is contained within this <see cref='System.Drawing.Region'/>
        ///       .
        ///    </para>
        /// </devdoc>
        public bool IsVisible(int x, int y, int width, int height) {
            return IsVisible(new Rectangle(x, y, width, height), null);
        }
        
        /// <include file='doc\Region.uex' path='docs/doc[@for="Region.IsVisible12"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Tests whether the specified <see cref='System.Drawing.Rectangle'/> is contained within this
        ///    <see cref='System.Drawing.Region'/>. 
        ///    </para>
        /// </devdoc>
        public bool IsVisible(Rectangle rect) {
            return IsVisible(rect, null);
        }
        
        /// <include file='doc\Region.uex' path='docs/doc[@for="Region.IsVisible13"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Tests whether the specified rectangle is contained within this <see cref='System.Drawing.Region'/> in the
        ///       specified graphics context.
        ///    </para>
        /// </devdoc>
        public bool IsVisible(int x, int y, int width, int height, Graphics g) {
            return IsVisible(new Rectangle(x, y, width, height), g);
        }
        
        /// <include file='doc\Region.uex' path='docs/doc[@for="Region.IsVisible14"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Tests whether the specified <see cref='System.Drawing.Rectangle'/> is contained within this
        ///    <see cref='System.Drawing.Region'/> 
        ///    in the specified graphics context.
        /// </para>
        /// </devdoc>
        public bool IsVisible(Rectangle rect, Graphics g) {
            return IsVisible(new RectangleF(rect.X, rect.Y, rect.Width, rect.Height), g);
        }

        /// <include file='doc\Region.uex' path='docs/doc[@for="Region.GetRegionScans"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Returns an array of <see cref='System.Drawing.RectangleF'/>
        ///       objects that approximate this Region on the specified
        ///    </para>
        /// </devdoc>        
        [SuppressMessage("Microsoft.Performance", "CA1808:AvoidCallsThatBoxValueTypes")]
        public RectangleF[] GetRegionScans(Matrix matrix) {
            if (matrix == null)
                throw new ArgumentNullException("matrix");

            EnsureNotDisposed();

            if (_isInfinite) {
                return new[] { TransformRect(GetLargeBounds(), matrix) };
            }
            if (_rects == null || _rects.Count == 0) {
                return Array.Empty<RectangleF>();
            }

            var result = new RectangleF[_rects.Count];
            for (int i = 0; i < _rects.Count; i++) {
                result[i] = TransformRect(_rects[i], matrix);
            }
            return result;
        }

        private void EnsureNotDisposed() {
            if (_disposed) {
                throw new ObjectDisposedException(nameof(Region));
            }
        }

        private void CopyFrom(Region other) {
            _isInfinite = other._isInfinite;
            _rects = CloneRects(other._rects);
            _exclusions = CloneRects(other._exclusions);
        }

        private static RectangleF TransformRect(RectangleF rect, Matrix matrix) {
            var points = new[] {
                new PointF(rect.Left, rect.Top),
                new PointF(rect.Right, rect.Top),
                new PointF(rect.Right, rect.Bottom),
                new PointF(rect.Left, rect.Bottom)
            };
            matrix.TransformPoints(points);
            float left = points[0].X;
            float right = points[0].X;
            float top = points[0].Y;
            float bottom = points[0].Y;
            for (int i = 1; i < points.Length; i++) {
                var p = points[i];
                if (p.X < left) left = p.X;
                if (p.X > right) right = p.X;
                if (p.Y < top) top = p.Y;
                if (p.Y > bottom) bottom = p.Y;
            }
            return new RectangleF(left, top, right - left, bottom - top);
        }

        private static RectangleF IntersectRect(RectangleF a, RectangleF b) {
            float left = Math.Max(a.Left, b.Left);
            float top = Math.Max(a.Top, b.Top);
            float right = Math.Min(a.Right, b.Right);
            float bottom = Math.Min(a.Bottom, b.Bottom);
            return new RectangleF(left, top, right - left, bottom - top);
        }

        private static bool IsEmptyRect(RectangleF rect) {
            return rect.Width <= 0 || rect.Height <= 0;
        }

        private static RectangleF GetLargeBounds() {
            return new RectangleF(-LargeBoundsExtent, -LargeBoundsExtent, LargeBoundsExtent * 2, LargeBoundsExtent * 2);
        }

        private void AddRect(RectangleF rect) {
            AddRect(rect, _rects);
        }

        private static void AddRect(RectangleF rect, List<RectangleF> target) {
            if (target == null) {
                return;
            }
            if (!IsEmptyRect(rect)) {
                target.Add(rect);
            }
        }

        private void ExcludeRects(List<RectangleF> cuts) {
            if (_rects == null || _rects.Count == 0) {
                return;
            }
            var current = _rects;
            foreach (var cut in cuts) {
                current = SubtractRects(current, cut);
                if (current.Count == 0) {
                    break;
                }
            }
            _rects = current;
        }

        private static List<RectangleF> SubtractRects(List<RectangleF> source, RectangleF cut) {
            var result = new List<RectangleF>();
            foreach (var rect in source) {
                result.AddRange(SubtractRect(rect, cut));
            }
            return result;
        }

        private static IEnumerable<RectangleF> SubtractRect(RectangleF rect, RectangleF cut) {
            var inter = IntersectRect(rect, cut);
            if (IsEmptyRect(inter)) {
                yield return rect;
                yield break;
            }

            if (inter.Top > rect.Top) {
                yield return new RectangleF(rect.Left, rect.Top, rect.Width, inter.Top - rect.Top);
            }
            if (inter.Bottom < rect.Bottom) {
                yield return new RectangleF(rect.Left, inter.Bottom, rect.Width, rect.Bottom - inter.Bottom);
            }
            if (inter.Left > rect.Left) {
                yield return new RectangleF(rect.Left, inter.Top, inter.Left - rect.Left, inter.Height);
            }
            if (inter.Right < rect.Right) {
                yield return new RectangleF(inter.Right, inter.Top, rect.Right - inter.Right, inter.Height);
            }
        }

        private RectangleF GetBoundsInternal() {
            if (_isInfinite) {
                return GetLargeBounds();
            }
            if (_rects == null || _rects.Count == 0) {
                return RectangleF.Empty;
            }
            float left = _rects[0].Left;
            float right = _rects[0].Right;
            float top = _rects[0].Top;
            float bottom = _rects[0].Bottom;
            for (int i = 1; i < _rects.Count; i++) {
                var r = _rects[i];
                if (r.Left < left) left = r.Left;
                if (r.Right > right) right = r.Right;
                if (r.Top < top) top = r.Top;
                if (r.Bottom > bottom) bottom = r.Bottom;
            }
            return new RectangleF(left, top, right - left, bottom - top);
        }

        private bool IsExcluded(PointF point) {
            if (_exclusions == null) {
                return false;
            }
            foreach (var r in _exclusions) {
                if (r.Contains(point)) {
                    return true;
                }
            }
            return false;
        }

        private bool IsExcluded(RectangleF rect) {
            if (_exclusions == null) {
                return false;
            }
            foreach (var r in _exclusions) {
                if (r.Contains(rect)) {
                    return true;
                }
            }
            return false;
        }

        private static List<RectangleF> CloneRects(List<RectangleF> rects) {
            if (rects == null) {
                return null;
            }
            return new List<RectangleF>(rects);
        }

        private static bool RectListsEqual(List<RectangleF> left, List<RectangleF> right) {
            if (ReferenceEquals(left, right)) {
                return true;
            }
            if (left == null || right == null) {
                return false;
            }
            if (left.Count != right.Count) {
                return false;
            }
            for (int i = 0; i < left.Count; i++) {
                if (left[i] != right[i]) {
                    return false;
                }
            }
            return true;
        }

        private static void WriteRectList(BinaryWriter writer, List<RectangleF> rects) {
            if (rects == null || rects.Count == 0) {
                writer.Write(0);
                return;
            }
            writer.Write(rects.Count);
            foreach (var rect in rects) {
                writer.Write(rect.X);
                writer.Write(rect.Y);
                writer.Write(rect.Width);
                writer.Write(rect.Height);
            }
        }

        private void InitializeFromRegionData(byte[] data) {
            _isInfinite = false;
            _rects = new List<RectangleF>();
            _exclusions = null;

            if (data == null || data.Length == 0) {
                return;
            }

            try {
                using var stream = new MemoryStream(data, false);
                using var reader = new BinaryReader(stream);
                byte version = reader.ReadByte();
                if (version != 1) {
                    return;
                }
                _isInfinite = reader.ReadBoolean();
                _rects = ReadRectList(reader);
                _exclusions = ReadRectList(reader);
            }
            catch {
                _isInfinite = false;
                _rects = new List<RectangleF>();
                _exclusions = null;
            }
        }

        private static List<RectangleF> ReadRectList(BinaryReader reader) {
            int count = reader.ReadInt32();
            if (count <= 0) {
                return new List<RectangleF>();
            }
            var rects = new List<RectangleF>(count);
            for (int i = 0; i < count; i++) {
                float x = reader.ReadSingle();
                float y = reader.ReadSingle();
                float w = reader.ReadSingle();
                float h = reader.ReadSingle();
                rects.Add(new RectangleF(x, y, w, h));
            }
            return rects;
        }
    }
}
