//------------------------------------------------------------------------------
// <copyright file="StringFormat.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>                                                                
//------------------------------------------------------------------------------

namespace System.Drawing {
    using System.Drawing.Text;

    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
    public struct CharacterRange {

        private int first;
        private int length;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public CharacterRange(int First, int Length) {
            this.first = First;
            this.length = Length;
        }

        public int First {
            get {
                return first;
            }
            set {
                first = value;
            }
        }
        public int Length {
            get {
                return length;
            }
            set {
                length = value;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() != typeof(CharacterRange))
                return false;

            CharacterRange cr = (CharacterRange)obj;
            return ((this.first == cr.First) && (this.length == cr.Length));
        }

        public static bool operator ==(CharacterRange cr1, CharacterRange cr2)
        {
            return ((cr1.First == cr2.First) && (cr1.Length == cr2.Length));
        }

        public static bool operator !=(CharacterRange cr1, CharacterRange cr2)
        {
            return !(cr1 == cr2);
        }

        public override int GetHashCode()
        {
            return unchecked(this.first << 8 + this.length);
        }
    }

    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = false )]
    public sealed class StringFormat : MarshalByRefObject, ICloneable, IDisposable {
        public IntPtr nativeFormat = IntPtr.Zero;
        public StringFormat() : this(StringFormatFlags.NoClip & 0, 0) { // default flags = 0
        }

        public StringFormat(StringFormatFlags options) : this(options, 0) { }

        public StringFormat(StringFormatFlags options, int language) {
            _flags = options;
            _alignment = StringAlignment.Near;
            _lineAlignment = StringAlignment.Near;
            _hotkeyPrefix = HotkeyPrefix.None;
            _digitLanguage = language;
            _digitSubstitute = StringDigitSubstitute.User;
            _firstTabOffset = 0f;
            _tabStops = Array.Empty<float>();
        }

        public StringFormat(StringFormat format) {
            if (format == null) throw new ArgumentNullException("format");
            _flags = format._flags;
            _alignment = format._alignment;
            _lineAlignment = format._lineAlignment;
            _hotkeyPrefix = format._hotkeyPrefix;
            _firstTabOffset = format._firstTabOffset;
            _tabStops = (float[])format._tabStops.Clone();
            _measurableRanges = (CharacterRange[])format._measurableRanges.Clone();
            _digitSubstitute = format._digitSubstitute;
            _digitLanguage = format._digitLanguage;
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void Dispose() {
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public object Clone() {
            return new StringFormat(this);
        }


        private StringFormatFlags _flags;
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public StringFormatFlags FormatFlags {
            get { return _flags; }
            set { _flags = value; }
        }

        private CharacterRange[] _measurableRanges = Array.Empty<CharacterRange>();
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void SetMeasurableCharacterRanges(CharacterRange[] ranges) {
            if (ranges == null) throw new ArgumentNullException("ranges");
            _measurableRanges = (CharacterRange[])ranges.Clone();
        }

        private StringAlignment _alignment;
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public StringAlignment Alignment {
            get { return _alignment; }
            set { _alignment = value; }
        }

        private StringAlignment _lineAlignment;
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public StringAlignment LineAlignment {
            get { return _lineAlignment; }
            set { _lineAlignment = value; }
        }

        private HotkeyPrefix _hotkeyPrefix;
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public HotkeyPrefix HotkeyPrefix {
            get { return _hotkeyPrefix; }
            set { _hotkeyPrefix = value; }
        }

        private float _firstTabOffset;
        private float[] _tabStops = Array.Empty<float>();
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void SetTabStops(float firstTabOffset, float[] tabStops) {
            if (firstTabOffset < 0)
                throw new ArgumentException(DCSR.GetString(DCSR.InvalidArgument, "firstTabOffset", firstTabOffset));
            if (tabStops == null) throw new ArgumentNullException("tabStops");
            _firstTabOffset = firstTabOffset;
            _tabStops = (float[])tabStops.Clone();
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public float [] GetTabStops(out float firstTabOffset) {
            firstTabOffset = _firstTabOffset;
            return (float[])_tabStops.Clone();
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public StringTrimming Trimming {
            get { return (_flags & StringFormatFlags.LineLimit) != 0 ? StringTrimming.None : StringTrimming.Character; }
            set {
                if (value == StringTrimming.None) _flags |= StringFormatFlags.LineLimit; else _flags &= ~StringFormatFlags.LineLimit;
            }
        }
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public static StringFormat GenericDefault {
            [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
            get {
                var sf = new StringFormat(StringFormatFlags.NoClip & 0, 0);
                sf._alignment = StringAlignment.Near;
                sf._lineAlignment = StringAlignment.Near;
                sf._hotkeyPrefix = HotkeyPrefix.None;
                sf._digitSubstitute = StringDigitSubstitute.User;
                sf._tabStops = Array.Empty<float>();
                return sf;
            }
        }
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public static StringFormat GenericTypographic {
            [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
            get {
                var sf = new StringFormat(StringFormatFlags.LineLimit | StringFormatFlags.NoClip , 0);
                sf._alignment = StringAlignment.Near;
                sf._lineAlignment = StringAlignment.Near;
                sf._hotkeyPrefix = HotkeyPrefix.None;
                sf._digitSubstitute = StringDigitSubstitute.User;
                sf._tabStops = Array.Empty<float>();
                return sf;
            }
        }

        private StringDigitSubstitute _digitSubstitute = StringDigitSubstitute.User;
        private int _digitLanguage = 0;
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void SetDigitSubstitution(int language, StringDigitSubstitute substitute)
        {
            _digitLanguage = language;
            _digitSubstitute = substitute;
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public StringDigitSubstitute DigitSubstitutionMethod {
            get { return _digitSubstitute; }
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public int DigitSubstitutionLanguage {
            get { return _digitLanguage; }
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public override string ToString() {
            return "[StringFormat, FormatFlags=" + _flags.ToString() + "]";
        }


    }
}    

