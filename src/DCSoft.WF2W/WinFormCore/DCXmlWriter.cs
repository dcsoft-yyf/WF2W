using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.IO;


namespace DCSoft
{
    // Specifies formatting options for XmlTextWriter.
    public enum DCFormatting
    {
        // No special formatting is done (this is the default).
        None,

        //This option causes child elements to be indented using the Indentation and IndentChar properties.
        // It only indents Element Content (http://www.w3.org/TR/1998/REC-xml-19980210#sec-element-content)
        // and not Mixed Content (http://www.w3.org/TR/1998/REC-xml-19980210#sec-mixed-content)
        // according to the XML 1.0 definitions of these terms.
        Indented,
    }
    public enum DCXmlSpace
    {
        // xml:space scope has not been specified.
        None = 0,

        // The xml:space scope is "default".
        Default = 1,

        // The xml:space scope is "preserve".
        Preserve = 2
    }
    // Represents a writer that provides fast non-cached forward-only way of generating XML streams
    // containing XML documents that conform to the W3CExtensible Markup Language (XML) 1.0 specification
    // and the Namespaces in XML specification.

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public class DCXmlTextWriter : DCXmlWriter
    {
        //
        // Private types
        //
        //private enum NamespaceState
        //{
        //    Uninitialized,
        //    NotDeclaredButInScope,
        //    DeclaredButNotWrittenOut,
        //    DeclaredAndWrittenOut
        //}

        private struct TagInfo
        {
            internal string name;
            //internal string prefix;
            //internal string defaultNs;
            //internal NamespaceState defaultNsState;
            internal DCXmlSpace xmlSpace;
            internal string xmlLang;
            internal int prevNsTop;
            //internal int prefixCount;
            internal bool mixed; // whether to pretty print the contents of this element.

            internal void Init(int nsTop)
            {
                name = null;
                //defaultNs = string.Empty;
                //defaultNsState = NamespaceState.Uninitialized;
                xmlSpace = DCXmlSpace.None;
                xmlLang = null;
                prevNsTop = nsTop;
                //prefixCount = 0;
                mixed = false;
            }
        }

        //private struct Namespace
        //{
        //    internal string prefix;
        //    internal string ns;
        //    internal bool declared;
        //    internal int prevNsIndex;

        //    internal void Set(string prefix, string ns, bool declared)
        //    {
        //        this.prefix = prefix;
        //        this.ns = ns;
        //        this.declared = declared;
        //        this.prevNsIndex = -1;
        //    }
        //}

        private enum SpecialAttr
        {
            None,
            XmlSpace,
            XmlLang//,
            //XmlNs
        };

        // State machine is working through autocomplete
        private enum State
        {
            Start,
            Prolog,
            PostDTD,
            Element,
            Attribute,
            Content,
            AttrOnly,
            Epilog,
            Error,
            Closed,
        }

        public enum Token
        {
            PI,
            Doctype,
            Comment,
            CData,
            StartElement,
            EndElement,
            LongEndElement,
            StartAttribute,
            EndAttribute,
            Content,
            Base64,
            RawData,
            Whitespace,
            Empty
        }

        //
        // Fields
        //
        // output
        private readonly TextWriter _textWriter = null!;
        public TextWriter BaseTextWriter
        {
            get { return this._textWriter; }
        }
        private readonly DCXmlTextEncoder _xmlEncoder = null!;
        private readonly Encoding _encoding;

        // formatting
        private DCFormatting _formatting;
        private bool _indented; // perf - faster to check a boolean.
        private int _indentation;
        private char[] _indentChars;
        private static readonly char[] s_defaultIndentChars = CreateDefaultIndentChars();

        private static char[] CreateDefaultIndentChars()
        {
            var result = new char[IndentArrayLength];
            Array.Fill(result, DefaultIndentChar);
            return result;
        }

        // element stack
        private TagInfo[] _stack;
        private int _top;

        // state machine for AutoComplete
        private State[] _stateTable;
        private State _currentState;
        private Token _lastToken;

        // Base64 content
        //private XmlTextWriterBase64Encoder? _base64Encoder;

        // misc
        private char _quoteChar;
        private char _curQuoteChar;
        private bool _namespaces;
        private SpecialAttr _specialAttr;
        //private string? _prefixForXmlNs;
        private bool _flush;

        // namespaces
        //private Namespace[] _nsStack;
        private int _nsTop;
        //private Dictionary<string, int>? _nsHashtable;
        //private bool _useNsHashtable;

        //
        // Constants and constant tables
        //
        private const int IndentArrayLength = 64;
        private const char DefaultIndentChar = ' ';
        //private const int NamespaceStackInitialSize = 8;
        //#if DEBUG
        //        private const int MaxNamespacesWalkCount = 3;
        //#else
        //        private const int MaxNamespacesWalkCount = 16;
        //#endif

        //private static readonly string[] s_stateName = {
        //    "Start",
        //    "Prolog",
        //    "PostDTD",
        //    "Element",
        //    "Attribute",
        //    "Content",
        //    "AttrOnly",
        //    "Epilog",
        //    "Error",
        //    "Closed",
        //};

        //private static readonly string[] s_tokenName = {
        //    "PI",
        //    "Doctype",
        //    "Comment",
        //    "CData",
        //    "StartElement",
        //    "EndElement",
        //    "LongEndElement",
        //    "StartAttribute",
        //    "EndAttribute",
        //    "Content",
        //    "Base64",
        //    "RawData",
        //    "Whitespace",
        //    "Empty"
        //};

        private static readonly State[] s_stateTableDefault = {
            //                          State.Start      State.Prolog     State.PostDTD    State.Element    State.Attribute  State.Content   State.AttrOnly   State.Epilog
            //
            /* Token.PI             */ State.Prolog,    State.Prolog,    State.PostDTD,   State.Content,   State.Content,   State.Content,  State.Error,     State.Epilog,
            /* Token.Doctype        */ State.PostDTD,   State.PostDTD,   State.Error,     State.Error,     State.Error,     State.Error,    State.Error,     State.Error,
            /* Token.Comment        */ State.Prolog,    State.Prolog,    State.PostDTD,   State.Content,   State.Content,   State.Content,  State.Error,     State.Epilog,
            /* Token.CData          */ State.Content,   State.Content,   State.Error,     State.Content,   State.Content,   State.Content,  State.Error,     State.Epilog,
            /* Token.StartElement   */ State.Element,   State.Element,   State.Element,   State.Element,   State.Element,   State.Element,  State.Error,     State.Element,
            /* Token.EndElement     */ State.Error,     State.Error,     State.Error,     State.Content,   State.Content,   State.Content,  State.Error,     State.Error,
            /* Token.LongEndElement */ State.Error,     State.Error,     State.Error,     State.Content,   State.Content,   State.Content,  State.Error,     State.Error,
            /* Token.StartAttribute */ State.AttrOnly,  State.Error,     State.Error,     State.Attribute, State.Attribute, State.Error,    State.Error,     State.Error,
            /* Token.EndAttribute   */ State.Error,     State.Error,     State.Error,     State.Error,     State.Element,   State.Error,    State.Epilog,     State.Error,
            /* Token.Content        */ State.Content,   State.Content,   State.Error,     State.Content,   State.Attribute, State.Content,  State.Attribute, State.Epilog,
            /* Token.Base64         */ State.Content,   State.Content,   State.Error,     State.Content,   State.Attribute, State.Content,  State.Attribute, State.Epilog,
            /* Token.RawData        */ State.Prolog,    State.Prolog,    State.PostDTD,   State.Content,   State.Attribute, State.Content,  State.Attribute, State.Epilog,
            /* Token.Whitespace     */ State.Prolog,    State.Prolog,    State.PostDTD,   State.Content,   State.Attribute, State.Content,  State.Attribute, State.Epilog,
        };

        private static readonly State[] s_stateTableDocument = {
            //                          State.Start      State.Prolog     State.PostDTD    State.Element    State.Attribute  State.Content   State.AttrOnly   State.Epilog
            //
            /* Token.PI             */ State.Error,     State.Prolog,    State.PostDTD,   State.Content,   State.Content,   State.Content,  State.Error,     State.Epilog,
            /* Token.Doctype        */ State.Error,     State.PostDTD,   State.Error,     State.Error,     State.Error,     State.Error,    State.Error,     State.Error,
            /* Token.Comment        */ State.Error,     State.Prolog,    State.PostDTD,   State.Content,   State.Content,   State.Content,  State.Error,     State.Epilog,
            /* Token.CData          */ State.Error,     State.Error,     State.Error,     State.Content,   State.Content,   State.Content,  State.Error,     State.Error,
            /* Token.StartElement   */ State.Error,     State.Element,   State.Element,   State.Element,   State.Element,   State.Element,  State.Error,     State.Error,
            /* Token.EndElement     */ State.Error,     State.Error,     State.Error,     State.Content,   State.Content,   State.Content,  State.Error,     State.Error,
            /* Token.LongEndElement */ State.Error,     State.Error,     State.Error,     State.Content,   State.Content,   State.Content,  State.Error,     State.Error,
            /* Token.StartAttribute */ State.Error,     State.Error,     State.Error,     State.Attribute, State.Attribute, State.Error,    State.Error,     State.Error,
            /* Token.EndAttribute   */ State.Error,     State.Error,     State.Error,     State.Error,     State.Element,   State.Error,    State.Error,     State.Error,
            /* Token.Content        */ State.Error,     State.Error,     State.Error,     State.Content,   State.Attribute, State.Content,  State.Error,     State.Error,
            /* Token.Base64         */ State.Error,     State.Error,     State.Error,     State.Content,   State.Attribute, State.Content,  State.Error,     State.Error,
            /* Token.RawData        */ State.Error,     State.Prolog,    State.PostDTD,   State.Content,   State.Attribute, State.Content,  State.Error,     State.Epilog,
            /* Token.Whitespace     */ State.Error,     State.Prolog,    State.PostDTD,   State.Content,   State.Attribute, State.Content,  State.Error,     State.Epilog,
        };

        //
        // Constructors
        //
        private DCXmlTextWriter()
        {
            _namespaces = true;
            _formatting = DCFormatting.None;
            _indentation = 2;
            _indentChars = s_defaultIndentChars;

            // namespaces
            //_nsStack = new Namespace[NamespaceStackInitialSize];
            _nsTop = -1;
            // element stack
            _stack = new TagInfo[10];
            _top = 0; // 0 is an empty sentanial element
            _stack[_top].Init(-1);
            _quoteChar = '"';

            _stateTable = s_stateTableDefault;
            _currentState = State.Start;
            _lastToken = Token.Empty;
        }

        // Creates an instance of the XmlTextWriter class using the specified stream.
        public DCXmlTextWriter(Stream w, Encoding encoding) : this()
        {
            _encoding = encoding;
            if (encoding != null)
                _textWriter = new StreamWriter(w, encoding);
            else
                _textWriter = new StreamWriter(w);
            _xmlEncoder = new DCXmlTextEncoder(_textWriter);
            _xmlEncoder.QuoteChar = _quoteChar;
        }

        // Creates an instance of the XmlTextWriter class using the specified file.
        //public XmlTextWriter(string filename, Encoding? encoding)
        //: this(new FileStream(filename, FileMode.Create,
        //                      FileAccess.Write, FileShare.Read), encoding)
        //{
        //}

        // Creates an instance of the XmlTextWriter class using the specified TextWriter.
        public DCXmlTextWriter(TextWriter w) : this()
        {
            _textWriter = w;
            if (w is System.IO.StringWriter)
            {
                this._MyStrBuilder = ((System.IO.StringWriter)w).GetStringBuilder();
            }
            _encoding = w.Encoding;
            _xmlEncoder = new DCXmlTextEncoder(w);
            _xmlEncoder.QuoteChar = _quoteChar;
        }

        private System.Text.StringBuilder _MyStrBuilder = null;
        //////
        ////// XmlTextWriter properties
        //////
        ////// Gets the XmlTextWriter base stream.
        ////public Stream? BaseStream
        ////{
        ////    get
        ////    {
        ////        if (_textWriter is StreamWriter streamWriter)
        ////        {
        ////            return streamWriter.BaseStream;
        ////        }
        ////        else
        ////        {
        ////            return null;
        ////        }
        ////    }
        ////}

        //// Gets or sets a value indicating whether to do namespace support.
        //public bool Namespaces
        //{
        //    get { return _namespaces; }
        //    set
        //    {
        //        if (_currentState != State.Start)
        //            throw new InvalidOperationException();

        //        _namespaces = value;
        //    }
        //}

        // Indicates how the output is formatted.
        public DCFormatting Formatting
        {
            get { return _formatting; }
            set { _formatting = value; _indented = value == DCFormatting.Indented; }
        }

        // Gets or sets how many IndentChars to write for each level in the hierarchy when Formatting is set to "Indented".
        public int Indentation
        {
            get { return _indentation; }
            set
            {
                if (value < 0)
                    throw new ArgumentException();
                _indentation = value;
            }
        }

        // Gets or sets which character to use for indenting when Formatting is set to "Indented".
        public char IndentChar
        {
            get { return _indentChars[0]; }
            set
            {
                if (value == DefaultIndentChar)
                {
                    _indentChars = s_defaultIndentChars;
                    return;
                }

                if (ReferenceEquals(_indentChars, s_defaultIndentChars))
                {
                    _indentChars = new char[IndentArrayLength];
                }

                for (int i = 0; i < IndentArrayLength; i++)
                {
                    _indentChars[i] = value;
                }
            }
        }

        //// Gets or sets which character to use to quote attribute values.
        //public char QuoteChar
        //{
        //    get { return _quoteChar; }
        //    set
        //    {
        //        if (value != '"' && value != '\'')
        //        {
        //            throw new ArgumentException();
        //        }
        //        _quoteChar = value;
        //        _xmlEncoder.QuoteChar = value;
        //    }
        //}

        //
        // XmlWriter implementation
        //
        // Writes out the XML declaration with the version "1.0".
        public override void WriteStartDocument()
        {
            StartDocument(-1);
        }

        //// Writes out the XML declaration with the version "1.0" and the standalone attribute.
        //public override void WriteStartDocument(bool standalone)
        //{
        //    StartDocument(standalone ? 1 : 0);
        //}

        // Closes any open elements or attributes and puts the writer back in the Start state.
        public override void WriteEndDocument()
        {
            try
            {
                AutoCompleteAll();
                if (_currentState != State.Epilog)
                {
                    if (_currentState == State.Closed)
                    {
                        throw new ArgumentException();
                    }
                    else
                    {
                        throw new ArgumentException();
                    }
                }
                _stateTable = s_stateTableDefault;
                _currentState = State.Start;
                _lastToken = Token.Empty;
            }
            catch
            {
                _currentState = State.Error;
                throw;
            }
        }

        //// Writes out the DOCTYPE declaration with the specified name and optional attributes.
        //public override void WriteDocType(string name, string pubid, string sysid, string subset)
        //{
        //    try
        //    {
        //        ValidateName(name, false);

        //        AutoComplete(Token.Doctype);
        //        _textWriter.Write("<!DOCTYPE ");
        //        _textWriter.Write(name);
        //        if (pubid != null)
        //        {
        //            _textWriter.Write($" PUBLIC {_quoteChar}");
        //            _textWriter.Write(pubid);
        //            _textWriter.Write($"{_quoteChar} {_quoteChar}");
        //            _textWriter.Write(sysid);
        //            _textWriter.Write(_quoteChar);
        //        }
        //        else if (sysid != null)
        //        {
        //            _textWriter.Write($" SYSTEM {_quoteChar}");
        //            _textWriter.Write(sysid);
        //            _textWriter.Write(_quoteChar);
        //        }
        //        if (subset != null)
        //        {
        //            _textWriter.Write("[");
        //            _textWriter.Write(subset);
        //            _textWriter.Write("]");
        //        }
        //        _textWriter.Write('>');
        //    }
        //    catch
        //    {
        //        _currentState = State.Error;
        //        throw;
        //    }
        //}

        //public VoidEventHandler EventBeforeWriteStartElement = null;
        // Writes out the specified start tag and associates it with the given namespace and prefix.
        public override void WriteStartElement(string prefix, string localName, string ns)
        {
            try
            {
                AutoComplete(Token.StartElement);
                PushStack();
                _textWriter.Write('<');
                _stack[_top].name = localName;
                _textWriter.Write(localName);
            }
            catch
            {
                _currentState = State.Error;
                throw;
            }
        }

        // Closes one element and pops the corresponding namespace scope.
        public override void WriteEndElement()
        {
            InternalWriteEndElement(false);
        }

        // Closes one element and pops the corresponding namespace scope.
        public override void WriteFullEndElement()
        {
            InternalWriteEndElement(true);
        }

        // Writes the start of an attribute.
        public override void WriteStartAttribute(string localName)
        {
            try
            {
                AutoComplete(Token.StartAttribute);

                _specialAttr = SpecialAttr.None;

                _xmlEncoder.StartAttribute(_specialAttr != SpecialAttr.None);

                _textWriter.Write(localName);
                _textWriter.Write('=');
                if (_curQuoteChar != _quoteChar)
                {
                    _curQuoteChar = _quoteChar;
                    _xmlEncoder.QuoteChar = _quoteChar;
                }
                _textWriter.Write(_curQuoteChar);
            }
            catch
            {
                _currentState = State.Error;
                throw;
            }
        }

        // Closes the attribute opened by WriteStartAttribute.
        public override void WriteEndAttribute()
        {
            try
            {
                AutoComplete(Token.EndAttribute);
            }
            catch
            {
                _currentState = State.Error;
                throw;
            }
        }


        // Writes out a &lt;![CDATA[...]]&gt; block containing the specified text.
        public override void WriteCData(string text)
        {
            try
            {
                AutoComplete(Token.CData);
                if (null != text && text.Contains("]]>"))
                {
                    throw new ArgumentException();
                }

                _textWriter.Write("<![CDATA[");

                if (null != text)
                {
                    _xmlEncoder.WriteRawWithSurrogateChecking(text);
                }

                _textWriter.Write("]]>");
            }
            catch
            {
                _currentState = State.Error;
                throw;
            }
        }

        //// Writes out a comment <!--...--> containing the specified text.
        //public override void WriteComment(string text)
        //{
        //    try
        //    {
        //        if (null != text && (text.Contains("--") || (text.Length != 0 && text[text.Length - 1] == '-')))
        //        {
        //            throw new ArgumentException();
        //        }
        //        AutoComplete(Token.Comment);
        //        _textWriter.Write("<!--");
        //        if (null != text)
        //        {
        //            _xmlEncoder.WriteRawWithSurrogateChecking(text);
        //        }
        //        _textWriter.Write("-->");
        //    }
        //    catch
        //    {
        //        _currentState = State.Error;
        //        throw;
        //    }
        //}

        // Writes out a processing instruction with a space between the name and text as follows: <?name text?>
        public override void WriteProcessingInstruction(string name, string text)
        {
            try
            {
                if (null != text && text.Contains("?>"))
                {
                    throw new ArgumentException();
                }

                if (string.Equals(name, "xml", StringComparison.OrdinalIgnoreCase) && _stateTable == s_stateTableDocument)
                {
                    throw new ArgumentException();
                }

                AutoComplete(Token.PI);
                InternalWriteProcessingInstruction(name, text);
            }
            catch
            {
                _currentState = State.Error;
                throw;
            }
        }

        // Writes out an entity reference as follows: "&"+name+";".
        public override void WriteEntityRef(string name)
        {
            try
            {
                ValidateName(name, false);
                AutoComplete(Token.Content);
                _xmlEncoder.WriteEntityRef(name);
            }
            catch
            {
                _currentState = State.Error;
                throw;
            }
        }

        // Forces the generation of a character entity for the specified Unicode character value.
        public override void WriteCharEntity(char ch)
        {
            try
            {
                AutoComplete(Token.Content);
                _xmlEncoder.WriteCharEntity(ch);
            }
            catch
            {
                _currentState = State.Error;
                throw;
            }
        }

        //// Writes out the given whitespace.
        //public override void WriteWhitespace(string? ws)
        //{
        //    try
        //    {
        //        if (null == ws)
        //        {
        //            ws = string.Empty;
        //        }

        //        if (!XmlCharType.IsOnlyWhitespace(ws))
        //        {
        //            throw new ArgumentException();
        //        }
        //        AutoComplete(Token.Whitespace);
        //        _xmlEncoder.Write(ws);
        //    }
        //    catch
        //    {
        //        _currentState = State.Error;
        //        throw;
        //    }
        //}

        public void WriteElementStringWithChars(string localName, char[] vValue, int startIndex, int length)
        {
            WriteStartElement(null, localName, null);
            if (length > 0)
            {
                this.WriteString(vValue, startIndex, length);
            }
            WriteEndElement();
        }

        public void WriteString(char[] vValue, int startIndex, int length)
        {
            if (vValue.Length > 0)
            {
                try
                {
                    AutoComplete(Token.Content);
                    if (this._MyStrBuilder != null)
                    {
                        this._MyStrBuilder.Append(vValue, startIndex, length);
                    }
                    else
                    {
                        if (_xmlEncoder._cacheAttrValue == false)
                        {
                            var bolMath = true;
                            for (var index2 = startIndex + length - 1; index2 >= startIndex; index2--)
                            {
                                if (DCXmlCharType.IsAttributeValueChar(vValue[index2]) == false)
                                {
                                    bolMath = false;
                                    break;
                                }
                            }
                            if (bolMath)
                            {
                                this._textWriter.Write(vValue, startIndex, length);
                                return;
                            }
                        }
                        this._xmlEncoder.Write(new string(vValue, startIndex, length));
                    }
                }
                catch
                {
                    _currentState = State.Error;
                    throw;
                }
            }
        }

        //public void WriteString(DCList<char> strValue )
        //{
        //    if(strValue.Length > 0 )
        //    {
        //        try
        //        {
        //            AutoComplete(Token.Content);
        //            if (this._MyStrBuilder != null)
        //            {
        //                this._MyStrBuilder.Append(strValue);
        //            }
        //            else
        //            {
        //                this._textWriter.Write(strValue.ToString());
        //                //_xmlEncoder.Write(strValue.ToString());
        //            }
        //        }
        //        catch
        //        {
        //            _currentState = State.Error;
        //            throw;
        //        }
        //    }
        //}

        // Writes out the specified text content.
        public override void WriteString(string text)
        {
            try
            {
                if (string.IsNullOrEmpty(text) == false)// null != text && text.Length != 0)
                {
                    AutoComplete(Token.Content);
                    _xmlEncoder.Write(text);
                }
            }
            catch
            {
                _currentState = State.Error;
                throw;
            }
        }


        // Writes out the attribute with the specified LocalName, value, and NamespaceURI.
        public void WriteAttributeString(string localName, string value)
        {
            WriteStartAttribute(localName);
            WriteString(value);
            WriteEndAttribute();
        }
        public void WriteAttributeString(string localName, float value)
        {
            WriteStartAttribute(localName);
            WriteString(value.ToString());
            WriteEndAttribute();
        }
        public void WriteAttributeString(string localName, int value)
        {
            WriteStartAttribute(localName);
            var len = StaticAppendInt32(_Buffer_SVGSingleToString, 0, value);
            WriteString(_Buffer_SVGSingleToString, 0, len);
            WriteEndAttribute();
        }
        public void WriteAttributeString(string localName, double value)
        {
            WriteStartAttribute(localName);
            WriteString(value.ToString());
            WriteEndAttribute();
        }

        public void WriteAttributeString(string localName, bool value)
        {
            WriteStartAttribute(localName);
            WriteString(value ? "true" : "false");
            WriteEndAttribute();
        }

        public void WriteChar(char c)
        {
            try
            {
                AutoComplete(Token.Content);
                if (DCXmlCharType.IsAttributeValueChar(c))
                {
                    if (this._MyStrBuilder != null)
                    {
                        this._MyStrBuilder.Append(c);
                    }
                    else
                    {
                        this._textWriter.Write(c);
                    }
                    return;
                }
                else
                {
                    _xmlEncoder.Write(c.ToString());
                }
            }
            catch
            {
                _currentState = State.Error;
                throw;
            }
        }

        private char[] _Base64Buffer = null;

        public void WriteAttributeImageData(string localName, byte[] imgData)
        {
            this.WriteStartAttribute(localName);
            this.AutoComplete(Token.Content);
            if (imgData != null && imgData.Length > 0)
            {
                var strHeader = Bitmap.StaticGetEmitImageSourceHeader(imgData);
                if (this._MyStrBuilder != null)
                {
                    this._MyStrBuilder.Append(strHeader);
                }
                else
                {
                    this._textWriter.Write(strHeader);
                }
                var len2 = (int)(imgData.Length * 4.0 / 3.0) + 100;
                if (this._Base64Buffer == null || this._Base64Buffer.Length < len2)
                {
                    this._Base64Buffer = new char[len2];
                }
                var len = Convert.ToBase64CharArray(imgData, 0, imgData.Length, this._Base64Buffer, 0);
                if (this._MyStrBuilder != null)
                {
                    this._MyStrBuilder.Append(this._Base64Buffer, 0, len);
                }
                else
                {
                    this._textWriter.Write(this._Base64Buffer, 0, len);
                }
            }
            this.WriteEndAttribute();
        }

        internal interface IWriteBase64
        {
            void WriteBase64(byte[] bsData, int startIndex, int length);
        }

        public void WriteElementBase64String(string localName, byte[] bsData)
        {
            this.WriteStartElement(null, localName, null);
            this.AutoComplete(Token.Content);
            if (bsData != null && bsData.Length > 0)
            {
                if (this._textWriter is IWriteBase64)
                {
                    ((IWriteBase64)this._textWriter).WriteBase64(bsData, 0, bsData.Length);
                }
                else
                {
                    var len2 = (int)(bsData.Length * 4.0 / 3.0) + 100;
                    if (this._Base64Buffer == null || this._Base64Buffer.Length < len2)
                    {
                        this._Base64Buffer = new char[len2];
                    }
                    var len = Convert.ToBase64CharArray(bsData, 0, bsData.Length, this._Base64Buffer, 0);
                    if (this._MyStrBuilder != null)
                    {
                        this._MyStrBuilder.Append(this._Base64Buffer, 0, len);
                    }
                    else
                    {
                        this._textWriter.Write(this._Base64Buffer, 0, len);
                    }
                }
            }
            this.WriteEndElement();
        }

        public void WriteElementString(string localName, string ns, int value)
        {
            var len = StaticAppendInt32(_Buffer_SVGSingleToString, 0, value);
            this.WriteStartElement(localName);
            this.WriteString(_Buffer_SVGSingleToString, 0, len);
            this.WriteEndElement();
        }

        public void WriteAttributeStringRaw(string localName, string value)
        {
            this.WriteStartAttribute(localName);
            this.AutoComplete(Token.Content);
            if (value != null && value.Length > 0)
            {
                if (this._MyStrBuilder != null)
                {
                    this._MyStrBuilder.Append(value);
                }
                else
                {
                    this._textWriter.Write(value);
                }
            }
            this.WriteEndAttribute();
        }
        //internal void WriteAttributeStringRaw(string localName, string value1 , string value2 )
        //{
        //    this.WriteStartAttribute(null, localName, null);
        //    this.AutoComplete(Token.Content);
        //    if( this._MyStrBuilder != null )
        //    {
        //        this._MyStrBuilder.Append(value1);
        //        this._MyStrBuilder.Append(value2);
        //    }
        //    else
        //    {
        //        this._textWriter.Write(value1);
        //        this._textWriter.Write(value2);
        //    }
        //    this.WriteEndAttribute();
        //}
        public void WriteAttributeCharsRaw(string name, char[] buffer, int len)
        {
            this.WriteStartAttribute(name);
            this.AutoComplete(Token.Content);
            if (this._MyStrBuilder != null)
            {
                this._MyStrBuilder.Append(buffer, 0, len);
            }
            else
            {
                this._textWriter.Write(buffer, 0, len);
            }
            this.WriteEndAttribute();
        }

        private static int _LastInt32Value = int.MinValue;
        private static int _lastValueLength = 0;
        private static char[] _LastChars = null;

        public void WriteAttributeInt32UseLastValue(string name, int v)
        {
            if (v == _LastInt32Value && _lastValueLength > 0)
            {
                this.WriteAttributeCharsRaw(name, _LastChars, _lastValueLength);
            }
            else
            {
                var len = StaticAppendInt32(_Buffer_SVGSingleToString, 0, v);
                if (len > 0)
                {
                    _LastInt32Value = v;
                    _lastValueLength = len;
                    if (_LastChars == null)
                    {
                        _LastChars = (char[])_Buffer_SVGSingleToString.Clone();
                    }
                    else
                    {
                        Array.Copy(_Buffer_SVGSingleToString, _LastChars, len);
                    }
                    this.WriteAttributeCharsRaw(name, _Buffer_SVGSingleToString, len);
                }
            }
        }


        private static readonly char[] _Buffer_SVGSingleToString = new char[40];

        public void WriteAttributeInt32(string name, int v)
        {
            var len = StaticAppendInt32(_Buffer_SVGSingleToString, 0, v);
            if (len > 0)
            {
                this.WriteAttributeCharsRaw(name, _Buffer_SVGSingleToString, len);
            }
        }

        public void WriteAttributeInt32AddHalf(string name, int v, bool addHalf = false)
        {
            var buf = _Buffer_SVGSingleToString;
            var len = StaticAppendInt32(buf, 0, v);
            if (len > 0)
            {
                buf[len++] = '.';
                buf[len++] = '5';
                this.WriteAttributeCharsRaw(name, buf, len);
            }
        }
        public void WriteAttributeSingle(string name, float v)
        {
            var len = StaticAppendSingle(_Buffer_SVGSingleToString, 0, v, 10000);
            if (len > 0)
            {
                this.WriteAttributeCharsRaw(name, _Buffer_SVGSingleToString, len);
            }
        }
        public static string SingleToString(float v)
        {
            //if( _Buffer_SVGSingleToString == null )
            //{
            //    _Buffer_SVGSingleToString = new char[40];
            //}
            var len = StaticAppendSingle(_Buffer_SVGSingleToString, 0, v, 10000);
            if (len > 0)
            {
                return new string(_Buffer_SVGSingleToString, 0, len);
            }
            else
            {
                return string.Empty;
            }
        }
        public static int StaticAppendSingle(char[] chrBuffer, int position, float v, int maskDigsAfterZero)
        {
            var bufferLength = chrBuffer.Length;
            if (position >= bufferLength)
            {
                return position;
            }
            if (v == 0)
            {
                chrBuffer[position++] = '0';
                return position;
            }
            else if (v == 1)
            {
                chrBuffer[position++] = '1';
                return position;
            }
            if (v > 10000000000000000f || v < -10000000000000000f || float.IsNaN(v))
            {
                // 超出范围
                var str2 = v.ToString();
                var len = Math.Min(str2.Length, bufferLength - position);
                Array.Copy(str2.ToCharArray(), 0, chrBuffer, position, len);
                return position + len;
            }
            if (v < 0)
            {
                v = -v;
                chrBuffer[position++] = '-';
            }
            var startIndex = position;
            if (maskDigsAfterZero <= 1)
            {
                maskDigsAfterZero = 1000000;
            }
            var intValue = (int)Math.Truncate(v);
            var intValueAfterZero = (int)((v - intValue) * maskDigsAfterZero);
            //long intValue = (long)(v * maskDigsAfterZero);
            //long intValueAfterZero = intValue % maskDigsAfterZero;
            //intValue = (intValue - intValueAfterZero) / maskDigsAfterZero;
            if (intValue == 0)
            {
                chrBuffer[startIndex++] = '0';
            }
            else if (intValue < 1000)
            {
                // 大多数情况下处于这个区间
                if (intValue >= 100)
                {
                    var v2 = intValue % 10;
                    chrBuffer[startIndex + 2] = (char)(v2 + '0');
                    intValue = (intValue - v2) / 10;
                    v2 = intValue % 10;
                    chrBuffer[startIndex + 1] = (char)(v2 + '0');
                    chrBuffer[startIndex] = (char)(((intValue - v2) / 10) + '0');
                    startIndex += 3;
                }
                else if (intValue >= 10)
                {
                    var v2 = intValue % 10;
                    chrBuffer[startIndex + 1] = (char)(v2 + '0');
                    chrBuffer[startIndex] = (char)(((intValue - v2) / 10) + '0');
                    startIndex += 2;
                }
                else
                {
                    chrBuffer[startIndex++] = (char)(intValue + '0');
                }
            }
            else
            {
                int oldStartIndex = startIndex;
                while (intValue > 0)
                {
                    var index = (int)(intValue % 10);
                    chrBuffer[startIndex++] = (char)(index + '0');
                    intValue = (intValue - index) / 10;
                }
                if (startIndex > oldStartIndex + 1)
                {
                    Array.Reverse(chrBuffer, oldStartIndex, startIndex - oldStartIndex);
                }
            }
            if (intValueAfterZero > 0)
            {
                // 处理小数部分
                chrBuffer[startIndex++] = '.';
                int oldStartIndex = startIndex;
                while (maskDigsAfterZero > 1)
                {
                    var index = (int)(intValueAfterZero % 10);
                    chrBuffer[startIndex++] = (char)(index + '0');
                    intValueAfterZero = (intValueAfterZero - index) / 10;
                    maskDigsAfterZero = maskDigsAfterZero / 10;
                }
                if (startIndex > oldStartIndex + 1)
                {
                    Array.Reverse(chrBuffer, oldStartIndex, startIndex - oldStartIndex);
                    while (chrBuffer[startIndex - 1] == '0')
                    {
                        startIndex--;
                    }
                }
            }
            return startIndex;
        }

        public static int StaticAppendInt32(char[] chrBuffer, int position, int intValue)
        {
            var bufferLength = chrBuffer.Length;
            if (position >= bufferLength)
            {
                return position;
            }
            if (intValue == 0)
            {
                chrBuffer[position++] = '0';
                return position;
            }
            else if (intValue == 1)
            {
                chrBuffer[position++] = '1';
                return position;
            }
            if (intValue < 0)
            {
                intValue = -intValue;
                chrBuffer[position++] = '-';
            }
            var startIndex = position;
            if (intValue < 1000)
            {
                // 大多数情况下处于这个区间
                if (intValue >= 100)
                {
                    var v2 = intValue % 10;
                    chrBuffer[startIndex + 2] = (char)(v2 + '0');
                    intValue = (intValue - v2) / 10;
                    v2 = intValue % 10;
                    chrBuffer[startIndex + 1] = (char)(v2 + '0');
                    chrBuffer[startIndex] = (char)(((intValue - v2) / 10) + '0');
                    startIndex += 3;
                }
                else if (intValue >= 10)
                {
                    var v2 = intValue % 10;
                    chrBuffer[startIndex + 1] = (char)(v2 + '0');
                    chrBuffer[startIndex] = (char)(((intValue - v2) / 10) + '0');
                    startIndex += 2;
                }
                else
                {
                    chrBuffer[startIndex++] = (char)(intValue + '0');
                }
            }
            else
            {
                int oldStartIndex = startIndex;
                while (intValue > 0)
                {
                    var index = (int)(intValue % 10);
                    chrBuffer[startIndex++] = (char)(index + '0');
                    intValue = (intValue - index) / 10;
                }
                if (startIndex > oldStartIndex + 1)
                {
                    Array.Reverse(chrBuffer, oldStartIndex, startIndex - oldStartIndex);
                }
            }
            return startIndex;
        }


        //// Writes out the specified surrogate pair as a character entity.
        //public override void WriteSurrogateCharEntity(char lowChar, char highChar)
        //{
        //    try
        //    {
        //        AutoComplete(Token.Content);
        //        _xmlEncoder.WriteSurrogateCharEntity(lowChar, highChar);
        //    }
        //    catch
        //    {
        //        _currentState = State.Error;
        //        throw;
        //    }
        //}


        //// Writes out the specified text content.
        //public override void WriteChars(char[] buffer, int index, int count)
        //{
        //    try
        //    {
        //        AutoComplete(Token.Content);
        //        _xmlEncoder.Write(buffer, index, count);
        //    }
        //    catch
        //    {
        //        _currentState = State.Error;
        //        throw;
        //    }
        //}
        //#if ! LightWeight
        //        // Writes raw markup from the specified character buffer.
        //        public override void WriteRaw(char[] buffer, int index, int count)
        //        {
        //            try
        //            {
        //                AutoComplete(Token.RawData);
        //                _xmlEncoder.WriteRaw(buffer, index, count);
        //            }
        //            catch
        //            {
        //                _currentState = State.Error;
        //                throw;
        //            }
        //        }
        //#endif
        // Writes raw markup from the specified character string.
        public override void WriteRaw(string data)
        {
            try
            {
                AutoComplete(Token.RawData);
                _xmlEncoder.WriteRawWithSurrogateChecking(data);
            }
            catch
            {
                _currentState = State.Error;
                throw;
            }
        }

        //// Encodes the specified binary bytes as base64 and writes out the resulting text.
        //public override void WriteBase64(byte[] buffer, int index, int count)
        //{
        //    try
        //    {
        //        if (!_flush)
        //        {
        //            AutoComplete(Token.Base64);
        //        }

        //        _flush = true;
        //        // No need for us to explicitly validate the args. The StreamWriter will do
        //        // it for us.
        //        if (null == _base64Encoder)
        //        {
        //            _base64Encoder = new XmlTextWriterBase64Encoder(_xmlEncoder);
        //        }
        //        // Encode will call WriteRaw to write out the encoded characters
        //        _base64Encoder.Encode(buffer, index, count);
        //    }
        //    catch
        //    {
        //        _currentState = State.Error;
        //        throw;
        //    }
        //}


        //// Encodes the specified binary bytes as binhex and writes out the resulting text.
        //public override void WriteBinHex(byte[] buffer, int index, int count)
        //{
        //    try
        //    {
        //        AutoComplete(Token.Content);
        //        BinHexEncoder.Encode(buffer, index, count, this);
        //    }
        //    catch
        //    {
        //        _currentState = State.Error;
        //        throw;
        //    }
        //}

        // Returns the state of the XmlWriter.
        public override DCWriteState WriteState
        {
            get
            {
                switch (_currentState)
                {
                    case State.Start:
                        return DCWriteState.Start;
                    case State.Prolog:
                    case State.PostDTD:
                        return DCWriteState.Prolog;
                    case State.Element:
                        return DCWriteState.Element;
                    case State.Attribute:
                    case State.AttrOnly:
                        return DCWriteState.Attribute;
                    case State.Content:
                    case State.Epilog:
                        return DCWriteState.Content;
                    case State.Error:
                        return DCWriteState.Error;
                    case State.Closed:
                        return DCWriteState.Closed;
                    default:
                        //Debug.Fail($"Unexpected state {_currentState}");
                        return DCWriteState.Error;
                }
            }
        }

        // Closes the XmlWriter and the underlying stream/TextWriter.
        public override void Close()
        {
            this._Base64Buffer = null;
            this._MyStrBuilder = null;
            try
            {
                AutoCompleteAll();
            }
            catch
            { // never fail
            }
            finally
            {
                _currentState = State.Closed;
                _textWriter.Dispose();
            }
        }

        // Flushes whatever is in the buffer to the underlying stream/TextWriter and flushes the underlying stream/TextWriter.
        public override void Flush()
        {
            _textWriter.Flush();
        }

        ////// Writes out the specified name, ensuring it is a valid Name according to the XML specification
        ////// (http://www.w3.org/TR/1998/REC-xml-19980210#NT-Name
        ////public override void WriteName(string name)
        ////{
        ////    try
        ////    {
        ////        AutoComplete(Token.Content);
        ////        InternalWriteName(name, false);
        ////    }
        ////    catch
        ////    {
        ////        _currentState = State.Error;
        ////        throw;
        ////    }
        ////}

        ////// Writes out the specified namespace-qualified name by looking up the prefix that is in scope for the given namespace.
        ////public override void WriteQualifiedName(string localName, string? ns)
        ////{
        ////    try
        ////    {
        ////        AutoComplete(Token.Content);
        ////        if (_namespaces)
        ////        {
        ////            if (ns != null && ns.Length != 0 && ns != _stack[_top].defaultNs)
        ////            {
        ////                string? prefix = FindPrefix(ns);
        ////                if (prefix == null)
        ////                {
        ////                    if (_currentState != State.Attribute)
        ////                    {
        ////                        throw new ArgumentException("UndefNamespace:" + ns);
        ////                    }

        ////                    prefix = GeneratePrefix();
        ////                    PushNamespace(prefix, ns, false);
        ////                }

        ////                if (prefix.Length != 0)
        ////                {
        ////                    InternalWriteName(prefix, true);
        ////                    _textWriter.Write(':');
        ////                }
        ////            }
        ////        }
        ////        else if (ns != null && ns.Length != 0)
        ////        {
        ////            throw new ArgumentException();
        ////        }

        ////        InternalWriteName(localName, true);
        ////    }
        ////    catch
        ////    {
        ////        _currentState = State.Error;
        ////        throw;
        ////    }
        ////}

        //// Returns the closest prefix defined in the current namespace scope for the specified namespace URI.
        //public override string? LookupPrefix(string ns)
        //{
        //    if (ns == null || ns.Length == 0)
        //    {
        //        throw new ArgumentException();
        //    }

        //    string? s = FindPrefix(ns);
        //    if (s == null && ns == _stack[_top].defaultNs)
        //    {
        //        s = string.Empty;
        //    }

        //    return s;
        //}

        //// Gets an XmlSpace representing the current xml:space scope.
        //public override XmlSpace XmlSpace
        //{
        //    get
        //    {
        //        for (int i = _top; i > 0; i--)
        //        {
        //            XmlSpace xs = _stack[i].xmlSpace;
        //            if (xs != XmlSpace.None)
        //                return xs;
        //        }
        //        return XmlSpace.None;
        //    }
        //}

        //// Gets the current xml:lang scope.
        //public override string? XmlLang
        //{
        //    get
        //    {
        //        for (int i = _top; i > 0; i--)
        //        {
        //            string? xlang = _stack[i].xmlLang;

        //            if (xlang != null)
        //                return xlang;
        //        }

        //        return null;
        //    }
        //}

        //// Writes out the specified name, ensuring it is a valid NmToken
        //// according to the XML specification (http://www.w3.org/TR/1998/REC-xml-19980210#NT-Name).
        //public override void WriteNmToken(string name)
        //{
        //    try
        //    {
        //        AutoComplete(Token.Content);

        //        if (name == null || name.Length == 0)
        //        {
        //            throw new ArgumentException();
        //        }
        //        if (!ValidateNames.IsNmtokenNoNamespaces(name))
        //        {
        //            throw new ArgumentException("InvalidNameChars:" + name);
        //        }
        //        _textWriter.Write(name);
        //    }
        //    catch
        //    {
        //        _currentState = State.Error;
        //        throw;
        //    }
        //}

        //
        // Private implementation methods
        //
        private void StartDocument(int standalone)
        {
            try
            {
                if (_currentState != State.Start)
                {
                    throw new InvalidOperationException();
                }
                _stateTable = s_stateTableDocument;
                _currentState = State.Prolog;

                //StringBuilder bufBld = new StringBuilder(128);
                //bufBld.Append($"version={_quoteChar}1.0{_quoteChar}");
                //if (_encoding != null)
                //{
                //    bufBld.Append(" encoding=");
                //    bufBld.Append(_quoteChar);
                //    bufBld.Append(_encoding.WebName);
                //    bufBld.Append(_quoteChar);
                //}
                //if (standalone >= 0)
                //{
                //    bufBld.Append(" standalone=");
                //    bufBld.Append(_quoteChar);
                //    bufBld.Append(standalone == 0 ? "no" : "yes");
                //    bufBld.Append(_quoteChar);
                //}
                //InternalWriteProcessingInstruction("xml", bufBld.ToString());
            }
            catch
            {
                _currentState = State.Error;
                throw;
            }
        }

        public void AutoComplete(Token token)
        {
            if (_currentState == State.Closed)
            {
                throw new InvalidOperationException();
            }
            else if (_currentState == State.Error)
            {
                throw new InvalidOperationException("WrongToken:" + token.ToString() + "Error");
            }

            State newState = _stateTable[((int)token << 3) + (int)_currentState];
            if (newState == State.Error)
            {
                throw new InvalidOperationException("WrongToken:" + token.ToString() + _currentState.ToString());
            }

            switch (token)
            {
                case Token.Doctype:
                    if (_indented && _currentState != State.Start)
                    {
                        Indent(false);
                    }
                    break;

                case Token.StartElement:
                case Token.Comment:
                case Token.PI:
                case Token.CData:
                    if (_currentState == State.Attribute)
                    {
                        WriteEndAttributeQuote();
                        WriteEndStartTag(false);
                    }
                    else if (_currentState == State.Element)
                    {
                        WriteEndStartTag(false);
                    }
                    if (token == Token.CData)
                    {
                        _stack[_top].mixed = true;
                    }
                    else if (_indented && _currentState != State.Start)
                    {
                        Indent(false);
                    }
                    break;

                case Token.EndElement:
                case Token.LongEndElement:
                    if (_flush)
                    {
                        FlushEncoders();
                    }
                    if (_currentState == State.Attribute)
                    {
                        WriteEndAttributeQuote();
                    }
                    if (_currentState == State.Content)
                    {
                        token = Token.LongEndElement;
                    }
                    else
                    {
                        WriteEndStartTag(token == Token.EndElement);
                    }
                    if (s_stateTableDocument == _stateTable && _top == 1)
                    {
                        newState = State.Epilog;
                    }
                    break;

                case Token.StartAttribute:
                    if (_flush)
                    {
                        FlushEncoders();
                    }
                    if (_currentState == State.Attribute)
                    {
                        WriteEndAttributeQuote();
                        _textWriter.Write(' ');
                    }
                    else if (_currentState == State.Element)
                    {
                        if (this._MyStrBuilder != null)
                        {
                            this._MyStrBuilder.Append(' ');
                        }
                        else
                        {
                            _textWriter.Write(' ');
                        }
                    }
                    break;

                case Token.EndAttribute:
                    if (_flush)
                    {
                        FlushEncoders();
                    }
                    WriteEndAttributeQuote();
                    break;

                case Token.Whitespace:
                case Token.Content:
                case Token.RawData:
                case Token.Base64:

                    if (token != Token.Base64 && _flush)
                    {
                        FlushEncoders();
                    }
                    if (_currentState == State.Element && _lastToken != Token.Content)
                    {
                        WriteEndStartTag(false);
                    }
                    if (newState == State.Content)
                    {
                        _stack[_top].mixed = true;
                    }
                    break;

                default:
                    throw new InvalidOperationException();
            }
            _currentState = newState;
            _lastToken = token;
        }

        private void AutoCompleteAll()
        {
            if (_flush)
            {
                FlushEncoders();
            }
            while (_top > 0)
            {
                WriteEndElement();
            }
        }

        private static readonly char[] s_selfClosingTagOpen = new char[] { '<', '/' };

        private void InternalWriteEndElement(bool longFormat)
        {
            try
            {
                if (_top <= 0)
                {
                    throw new InvalidOperationException();
                }
                // if we are in the element, we need to close it.
                AutoComplete(longFormat ? Token.LongEndElement : Token.EndElement);
                if (_lastToken == Token.LongEndElement)
                {
                    if (_indented)
                    {
                        Indent(true);
                    }
                    _textWriter.Write(s_selfClosingTagOpen);
                    //if (_namespaces && _stack[_top].prefix != null)
                    //{
                    //    _textWriter.Write(_stack[_top].prefix);
                    //    _textWriter.Write(':');
                    //}
                    _textWriter.Write(_stack[_top].name);
                    _textWriter.Write('>');
                }

                // pop namespaces
                int prevNsTop = _stack[_top].prevNsTop;
                //if (_useNsHashtable && prevNsTop < _nsTop)
                //{
                //    PopNamespaces(prevNsTop + 1, _nsTop);
                //}
                _nsTop = prevNsTop;
                _top--;
            }
            catch
            {
                _currentState = State.Error;
                throw;
            }
        }

        private static readonly char[] s_closeTagEnd = new char[] { ' ', '/', '>' };

        private void WriteEndStartTag(bool empty)
        {
            _xmlEncoder.StartAttribute(false);
            //for (int i = _nsTop; i > _stack[_top].prevNsTop; i--)
            //{
            //    if (!_nsStack[i].declared)
            //    {
            //        _textWriter.Write(" xmlns:");
            //        _textWriter.Write(_nsStack[i].prefix);
            //        _textWriter.Write('=');
            //        _textWriter.Write(_quoteChar);
            //        _xmlEncoder.Write(_nsStack[i].ns);
            //        _textWriter.Write(_quoteChar);
            //    }
            //}
            //// Default
            //if ((_stack[_top].defaultNs != _stack[_top - 1].defaultNs) &&
            //    (_stack[_top].defaultNsState == NamespaceState.DeclaredButNotWrittenOut))
            //{
            //    _textWriter.Write(" xmlns=");
            //    _textWriter.Write(_quoteChar);
            //    _xmlEncoder.Write(_stack[_top].defaultNs);
            //    _textWriter.Write(_quoteChar);
            //    _stack[_top].defaultNsState = NamespaceState.DeclaredAndWrittenOut;
            //}
            _xmlEncoder.EndAttribute();
            if (empty)
            {
                _textWriter.Write(s_closeTagEnd);
            }
            else
            {
                _textWriter.Write('>');
            }
        }

        private void WriteEndAttributeQuote()
        {
            if (_specialAttr != SpecialAttr.None)
            {
                // Ok, now to handle xmlspace, etc.
                HandleSpecialAttribute();
            }
            _xmlEncoder.EndAttribute();
            if (this._MyStrBuilder != null)
            {
                this._MyStrBuilder.Append(_curQuoteChar);
            }
            else
            {
                _textWriter.Write(_curQuoteChar);
            }
        }

        private void Indent(bool beforeEndElement)
        {
            // pretty printing.
            if (_top == 0)
            {
                if (this._textWriter is System.IO.StringWriter sw
                    && sw.GetStringBuilder().Length == 0)
                {
                    return;
                }
                _textWriter.WriteLine();
            }
            else if (!_stack[_top].mixed)
            {
                _textWriter.WriteLine();
                int i = (beforeEndElement ? _top - 1 : _top) * _indentation;
                if (i <= _indentChars.Length)
                {
                    _textWriter.Write(_indentChars, 0, i);
                }
                else
                {
                    while (i > 0)
                    {
                        _textWriter.Write(_indentChars, 0, Math.Min(i, _indentChars.Length));
                        i -= _indentChars.Length;
                    }
                }
            }
        }

        //// pushes new namespace scope, and returns generated prefix, if one
        //// was needed to resolve conflicts.
        //public void PushNamespace(string? prefix, string ns, bool declared)
        //{
        //    if (XmlReservedNs.NsXmlNs == ns)
        //    {
        //        throw new ArgumentException();
        //    }

        //    if (prefix == null)
        //    {
        //        switch (_stack[_top].defaultNsState)
        //        {
        //            case NamespaceState.DeclaredButNotWrittenOut:
        //               //Debug.Assert(declared == true, "Unexpected situation!!");
        //                // the first namespace that the user gave us is what we
        //                // like to keep.
        //                break;
        //            case NamespaceState.Uninitialized:
        //            case NamespaceState.NotDeclaredButInScope:
        //                // we now got a brand new namespace that we need to remember
        //                _stack[_top].defaultNs = ns;
        //                break;
        //            default:
        //                Debug.Fail("Should have never come here");
        //                return;
        //        }

        //        _stack[_top].defaultNsState = (declared ? NamespaceState.DeclaredAndWrittenOut : NamespaceState.DeclaredButNotWrittenOut);
        //    }
        //    else
        //    {
        //        if (prefix.Length != 0 && ns.Length == 0)
        //        {
        //            throw new ArgumentException();
        //        }

        //        int existingNsIndex = LookupNamespace(prefix);
        //        if (existingNsIndex != -1 && _nsStack[existingNsIndex].ns == ns)
        //        {
        //            // it is already in scope.
        //            if (declared)
        //            {
        //                _nsStack[existingNsIndex].declared = true;
        //            }
        //        }
        //        else
        //        {
        //            // see if prefix conflicts for the current element
        //            if (declared)
        //            {
        //                if (existingNsIndex != -1 && existingNsIndex > _stack[_top].prevNsTop)
        //                {
        //                    _nsStack[existingNsIndex].declared = true; // old one is silenced now
        //                }
        //            }

        //            AddNamespace(prefix, ns, declared);
        //        }
        //    }
        //}

        //public  void AddNamespace(string prefix, string ns, bool declared)
        //{
        //    int nsIndex = ++_nsTop;
        //    if (nsIndex == _nsStack.Length)
        //    {
        //        Namespace[] newStack = new Namespace[nsIndex * 2];
        //        Array.Copy(_nsStack, newStack, nsIndex);
        //        _nsStack = newStack;
        //    }
        //    _nsStack[nsIndex].Set(prefix, ns, declared);

        //    if (_useNsHashtable)
        //    {
        //        AddToNamespaceHashtable(nsIndex);
        //    }
        //    else if (nsIndex == MaxNamespacesWalkCount)
        //    {
        //        // add all
        //        _nsHashtable = new Dictionary<string, int>();
        //        _useNsHashtable = true;

        //        for (int i = 0; i <= nsIndex; i++)
        //        {
        //            AddToNamespaceHashtable(i);
        //        }
        //    }
        //}

        //private void AddToNamespaceHashtable(int namespaceIndex)
        //{
        //   //Debug.Assert(_useNsHashtable);
        //   //Debug.Assert(_nsHashtable != null);

        //    string prefix = _nsStack[namespaceIndex].prefix;
        //    int existingNsIndex;

        //    if (_nsHashtable.TryGetValue(prefix, out existingNsIndex))
        //    {
        //        _nsStack[namespaceIndex].prevNsIndex = existingNsIndex;
        //    }

        //    _nsHashtable[prefix] = namespaceIndex;
        //}

        //private void PopNamespaces(int indexFrom, int indexTo)
        //{
        //   //Debug.Assert(_useNsHashtable);
        //   //Debug.Assert(_nsHashtable != null);

        //    for (int i = indexTo; i >= indexFrom; i--)
        //    {
        //       //Debug.Assert(_nsHashtable.ContainsKey(_nsStack[i].prefix));
        //        if (_nsStack[i].prevNsIndex == -1)
        //        {
        //            _nsHashtable.Remove(_nsStack[i].prefix);
        //        }
        //        else
        //        {
        //            _nsHashtable[_nsStack[i].prefix] = _nsStack[i].prevNsIndex;
        //        }
        //    }
        //}

        //private string GeneratePrefix()
        //{
        //    int temp = _stack[_top].prefixCount++ + 1;
        //    return string.Create(CultureInfo.InvariantCulture, $"d{_top:d}p{temp:d}");
        //}

        private void InternalWriteProcessingInstruction(string name, string text)
        {
            _textWriter.Write("<?");
            ValidateName(name, false);
            _textWriter.Write(name);
            _textWriter.Write(' ');

            if (null != text)
            {
                _xmlEncoder.WriteRawWithSurrogateChecking(text);
            }

            _textWriter.Write("?>");
        }

        //private int LookupNamespace(string prefix)
        //{
        //    if (_useNsHashtable)
        //    {
        //       Debug.Assert(_nsHashtable != null);
        //        int nsIndex;
        //        if (_nsHashtable.TryGetValue(prefix, out nsIndex))
        //        {
        //            return nsIndex;
        //        }
        //    }
        //    else
        //    {
        //        for (int i = _nsTop; i >= 0; i--)
        //        {
        //            if (_nsStack[i].prefix == prefix)
        //            {
        //                return i;
        //            }
        //        }
        //    }

        //    return -1;
        //}

        //private int LookupNamespaceInCurrentScope(string prefix)
        //{
        //    if (_useNsHashtable)
        //    {
        //       //Debug.Assert(_nsHashtable != null);
        //        int nsIndex;

        //        if (_nsHashtable.TryGetValue(prefix, out nsIndex))
        //        {
        //            if (nsIndex > _stack[_top].prevNsTop)
        //            {
        //                return nsIndex;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        for (int i = _nsTop; i > _stack[_top].prevNsTop; i--)
        //        {
        //            if (_nsStack[i].prefix == prefix)
        //            {
        //                return i;
        //            }
        //        }
        //    }
        //    return -1;
        //}

        //private string? FindPrefix(string ns)
        //{
        //    for (int i = _nsTop; i >= 0; i--)
        //    {
        //        if (_nsStack[i].ns == ns)
        //        {
        //            if (LookupNamespace(_nsStack[i].prefix) == i)
        //            {
        //                return _nsStack[i].prefix;
        //            }
        //        }
        //    }

        //    return null;
        //}

        //// There are three kind of strings we write out - Name, LocalName and Prefix.
        //// Both LocalName and Prefix can be represented with NCName == false and Name
        //// can be represented as NCName == true

        //private void InternalWriteName(string name, bool isNCName)
        //{
        //    ValidateName(name, isNCName);
        //    _textWriter.Write(name);
        //}

        // This method is used for validation of the DOCTYPE, processing instruction and entity names plus names
        // written out by the user via WriteName and WriteQualifiedName.
        // Unfortunatelly the names of elements and attributes are not validated by the XmlTextWriter.
        // Also this method does not check wheather the character after ':' is a valid start name character. It accepts
        // all valid name characters at that position. This can't be changed because of backwards compatibility.
        private void ValidateName(string name, bool isNCName)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException();
            }

            int nameLength = name.Length;

            // Namespaces supported
            if (_namespaces)
            {
                // We can't use ValidateNames.ParseQName here because of backwards compatibility bug we need to preserve.
                // The bug is that the character after ':' is validated only as a NCName characters instead of NCStartName.
                int colonPosition = -1;

                // Parse NCName (may be prefix, may be local name)
                int position = DCValidateNames.ParseNCName(name);

            Continue:
                if (position == nameLength)
                {
                    return;
                }

                // we have prefix:localName
                if (name[position] == ':')
                {
                    if (!isNCName)
                    {
                        // first colon in qname
                        if (colonPosition == -1)
                        {
                            // make sure it is not the first or last characters
                            if (position > 0 && position + 1 < nameLength)
                            {
                                colonPosition = position;
                                // Because of the back-compat bug (described above) parse the rest as Nmtoken
                                position++;
                                position += DCValidateNames.ParseNmtoken(name, position);
                                goto Continue;
                            }
                        }
                    }
                }
            }
            // Namespaces not supported
            else
            {
                if (DCValidateNames.IsNameNoNamespaces(name))
                {
                    return;
                }
            }
            throw new ArgumentException("Xml_InvalidNameChars:" + name);
        }

        private void HandleSpecialAttribute()
        {
            string value = _xmlEncoder.AttributeValue;
            switch (_specialAttr)
            {
                case SpecialAttr.XmlLang:
                    _stack[_top].xmlLang = value;
                    break;
                case SpecialAttr.XmlSpace:
                    // validate XmlSpace attribute
                    value = TrimString(value);
                    if (value == "default")
                    {
                        _stack[_top].xmlSpace = DCXmlSpace.Default;
                    }
                    else if (value == "preserve")
                    {
                        _stack[_top].xmlSpace = DCXmlSpace.Preserve;
                    }
                    else
                    {
                        throw new ArgumentException("InvalidXmlSpace:" + value);
                    }
                    break;
                    //case SpecialAttr.XmlNs:
                    //    VerifyPrefixXml(_prefixForXmlNs, value);
                    //    PushNamespace(_prefixForXmlNs, value, true);
                    //    break;
            }
        }

        internal static readonly char[] WhitespaceChars = new char[] { ' ', '\t', '\n', '\r' };

        // Trim a string using XML whitespace characters
        internal static string TrimString(string value)
        {
            return value.Trim(WhitespaceChars);
        }
        //private static void VerifyPrefixXml(string? prefix, string ns)
        //{
        //    if (prefix != null && prefix.Length == 3)
        //    {
        //        if (
        //           (prefix[0] == 'x' || prefix[0] == 'X') &&
        //           (prefix[1] == 'm' || prefix[1] == 'M') &&
        //           (prefix[2] == 'l' || prefix[2] == 'L')
        //           )
        //        {
        //            if (XmlReservedNs.NsXml != ns)
        //            {
        //                throw new ArgumentException();
        //            }
        //        }
        //    }
        //}

        private void PushStack()
        {
            if (_top == _stack.Length - 1)
            {
                TagInfo[] na = new TagInfo[_stack.Length + 10];
                if (_top > 0) Array.Copy(_stack, na, _top + 1);
                _stack = na;
            }

            _top++; // Move up stack
            _stack[_top].Init(_nsTop);
        }

        private void FlushEncoders()
        {
            //if (null != _base64Encoder)
            //{
            //    // The Flush will call WriteRaw to write out the rest of the encoded characters
            //    _base64Encoder.Flush();
            //}
            _flush = false;
        }
    }

    // Specifies the state of the XmlWriter.
    public enum DCWriteState
    {
        // Nothing has been written yet.
        Start,

        // Writing the prolog.
        Prolog,

        // Writing a the start tag for an element.
        Element,

        // Writing an attribute value.
        Attribute,

        // Writing element content.
        Content,

        // XmlWriter is closed; Close has been called.
        Closed,

        // Writer is in error state.
        Error
    };

    // Represents a writer that provides fast non-cached forward-only way of generating XML streams containing XML documents
    // that conform to the W3C Extensible Markup Language (XML) 1.0 specification and the Namespaces in XML specification.
    public abstract partial class DCXmlWriter : IDisposable
    {
        // Helper buffer for WriteNode(XmlReader, bool)
        //private char[]? _writeNodeBuffer;

        // Constants
        //private const int WriteNodeBufferSize = 1024;

        // Returns the settings describing the features of the writer. Returns null for V1 XmlWriters (XmlTextWriter).
        //public virtual XmlWriterSettings Settings => null;

        // Write methods
        // Writes out the XML declaration with the version "1.0".

        public abstract void WriteStartDocument();

        //Writes out the XML declaration with the version "1.0" and the specified standalone attribute.

        //public abstract void WriteStartDocument(bool standalone);

        //Closes any open elements or attributes and puts the writer back in the Start state.

        public abstract void WriteEndDocument();

        // Writes out the DOCTYPE declaration with the specified name and optional attributes.

        //public abstract void WriteDocType(string name, string pubid, string sysid, string subset);

        // Writes out the specified start tag and associates it with the given namespace.
        public void WriteStartElement(string localName, string ns)
        {
            WriteStartElement(null, localName, ns);
        }

        // Writes out the specified start tag and associates it with the given namespace and prefix.

        public abstract void WriteStartElement(string prefix, string localName, string ns);

        // Writes out a start tag with the specified local name with no namespace.
        public void WriteStartElement(string localName)
        {
            WriteStartElement(null, localName, null);
        }

        // Closes one element and pops the corresponding namespace scope.

        public abstract void WriteEndElement();

        // Closes one element and pops the corresponding namespace scope. Writes out a full end element tag, e.g. </element>.

        public abstract void WriteFullEndElement();

        //// Writes out the attribute with the specified prefix, LocalName, NamespaceURI and value.
        //public void WriteAttributeString( string localName, string value)
        //{
        //    WriteStartAttribute(localName);
        //    WriteString(value);
        //    WriteEndAttribute();
        //}
        //// Writes the start of an attribute.
        //public void WriteStartAttribute(string localName, string? ns)
        //{
        //    WriteStartAttribute(null, localName, ns);
        //}

        // Writes the start of an attribute.

        public abstract void WriteStartAttribute(string localName);

        //// Writes the start of an attribute.
        //public void WriteStartAttribute(string localName)
        //{
        //    WriteStartAttribute(null, localName, null);
        //}

        // Closes the attribute opened by WriteStartAttribute call.

        public abstract void WriteEndAttribute();

        // Writes out a <![CDATA[...]]>; block containing the specified text.

        public abstract void WriteCData(string text);

        // Writes out a comment <!--...-->; containing the specified text.

        //public abstract void WriteComment(string text);

        // Writes out a processing instruction with a space between the name and text as follows: <?name text?>

        public abstract void WriteProcessingInstruction(string name, string text);

        // Writes out an entity reference as follows: "&"+name+";".

        public abstract void WriteEntityRef(string name);

        // Forces the generation of a character entity for the specified Unicode character value.

        public abstract void WriteCharEntity(char ch);

        // Writes out the given whitespace.

        //public abstract void WriteWhitespace(string? ws);

        // Writes out the specified text content.

        public abstract void WriteString(string text);

        //        // Write out the given surrogate pair as an entity reference.

        //        //public abstract void WriteSurrogateCharEntity(char lowChar, char highChar);

        //        // Writes out the specified text content.

        //        //public abstract void WriteChars(char[] buffer, int index, int count);

        //        // Writes raw markup from the given character buffer.
        //#if ! LightWeight
        //        public abstract void WriteRaw(char[] buffer, int index, int count);
        //#endif
        // Writes raw markup from the given string.

        public abstract void WriteRaw(string data);

        // Encodes the specified binary bytes as base64 and writes out the resulting text.

        //public abstract void WriteBase64(byte[] buffer, int index, int count);

        //// Encodes the specified binary bytes as bin hex and writes out the resulting text.
        //public virtual void WriteBinHex(byte[] buffer, int index, int count)
        //{
        //    BinHexEncoder.Encode(buffer, index, count, this);
        //}

        // Returns the state of the XmlWriter.
        public abstract DCWriteState WriteState { get; }

        // Closes the XmlWriter and the underlying stream/TextReader (if Settings.CloseOutput is true).
        public virtual void Close() { }

        // Flushes data that is in the internal buffers into the underlying streams/TextReader and flushes the stream/TextReader.

        public abstract void Flush();

        // Returns the closest prefix defined in the current namespace scope for the specified namespace URI.
        //public abstract string? LookupPrefix(string ns);

        // Gets an XmlSpace representing the current xml:space scope.
        //public virtual XmlSpace XmlSpace => XmlSpace.Default;

        // Gets the current xml:lang scope.
        //public virtual string? XmlLang => string.Empty;

        // Scalar Value Methods

        //// Writes out the specified name, ensuring it is a valid NmToken according to the XML specification
        //// (http://www.w3.org/TR/1998/REC-xml-19980210#NT-Name).
        //public virtual void WriteNmToken(string name)
        //{
        //    if (string.IsNullOrEmpty(name))
        //    {
        //        throw new ArgumentException();
        //    }
        //    WriteString(XmlConvert.VerifyNMTOKEN(name, ExceptionType.ArgumentException));
        //}

        //// Writes out the specified name, ensuring it is a valid Name according to the XML specification
        //// (http://www.w3.org/TR/1998/REC-xml-19980210#NT-Name).
        //public virtual void WriteName(string name)
        //{
        //    WriteString(XmlConvert.VerifyQName(name, ExceptionType.ArgumentException));
        //}

        //// Writes out the specified namespace-qualified name by looking up the prefix that is in scope for the given namespace.
        //public virtual void WriteQualifiedName(string localName, string? ns)
        //{
        //    if (!string.IsNullOrEmpty(ns))
        //    {
        //        string? prefix = LookupPrefix(ns);
        //        if (prefix == null)
        //        {
        //            throw new ArgumentException("UndefNamespace:" + ns);
        //        }
        //        WriteString(prefix);
        //        WriteString(":");
        //    }
        //    WriteString(localName);
        //}

        //// Writes out the specified value.
        //public virtual void WriteValue(object value)
        //{
        //    ArgumentNullException.ThrowIfNull(value);

        //    WriteString(XmlUntypedConverter.Untyped.ToString(value, null));
        //}

        //#if ! LightWeight
        //        // Writes out the specified value.
        //        public virtual void WriteValue(string? value)
        //{
        //    if (value != null)
        //    {
        //        WriteString(value);
        //    }
        //}
        ////// Writes out the specified value.
        ////public virtual void WriteValue(bool value)
        ////{
        ////    WriteString(XmlConvert.ToString(value));
        ////}

        ////// Writes out the specified value.
        ////public virtual void WriteValue(DateTime value)
        ////{
        ////    WriteString(XmlConvert.ToString(value, XmlDateTimeSerializationMode.RoundtripKind));
        ////}

        ////// Writes out the specified value.
        ////public virtual void WriteValue(DateTimeOffset value)
        ////{
        ////    // Under Win8P, WriteValue(DateTime) will invoke this overload, but custom writers
        ////    // might not have implemented it. This base implementation should call WriteValue(DateTime).
        ////    // The following conversion results in the same string as calling ToString with DateTimeOffset.
        ////    WriteValue(value.Offset != TimeSpan.Zero ? value.LocalDateTime : value.UtcDateTime);
        ////}

        //// Writes out the specified value.
        //public virtual void WriteValue(double value)
        //{
        //    WriteString(XmlConvert.ToString(value));
        //}

        //// Writes out the specified value.
        //public virtual void WriteValue(float value)
        //{
        //    WriteString(XmlConvert.ToString(value));
        //}

        //// Writes out the specified value.
        //public virtual void WriteValue(decimal value)
        //{
        //    WriteString(XmlConvert.ToString(value));
        //}

        //// Writes out the specified value.
        //public virtual void WriteValue(int value)
        //{
        //    WriteString(XmlConvert.ToString(value));
        //}

        //// Writes out the specified value.
        //public virtual void WriteValue(long value)
        //{
        //    WriteString(XmlConvert.ToString(value));
        //}
        //#endif
        //// XmlReader Helper Methods

        //// Writes out all the attributes found at the current position in the specified XmlReader.
        //public virtual void WriteAttributes(XmlReader reader, bool defattr)
        //{
        //    ArgumentNullException.ThrowIfNull(reader);

        //    if (reader.NodeType is XmlNodeType.Element or XmlNodeType.XmlDeclaration)
        //    {
        //        if (reader.MoveToFirstAttribute())
        //        {
        //            WriteAttributes(reader, defattr);
        //            reader.MoveToElement();
        //        }
        //    }
        //    else if (reader.NodeType != XmlNodeType.Attribute)
        //    {
        //        throw new XmlException();
        //    }
        //    else
        //    {
        //        do
        //        {
        //            // we need to check both XmlReader.IsDefault and XmlReader.SchemaInfo.IsDefault.
        //            // If either of these is true and defattr=false, we should not write the attribute out
        //            if (defattr || !reader.IsDefaultInternal)
        //            {
        //                WriteStartAttribute(reader.Prefix, reader.LocalName, reader.NamespaceURI);
        //                while (reader.ReadAttributeValue())
        //                {
        //                    if (reader.NodeType == XmlNodeType.EntityReference)
        //                    {
        //                        WriteEntityRef(reader.Name);
        //                    }
        //                    else
        //                    {
        //                        WriteString(reader.Value);
        //                    }
        //                }
        //                WriteEndAttribute();
        //            }
        //        }
        //        while (reader.MoveToNextAttribute());
        //    }
        //}

        //// Copies the current node from the given reader to the writer (including child nodes), and if called on an element moves the XmlReader
        //// to the corresponding end element.
        //public virtual void WriteNode(XmlReader reader, bool defattr)
        //{
        //    ArgumentNullException.ThrowIfNull(reader);

        //    bool canReadChunk = reader.CanReadValueChunk;
        //    int d = reader.NodeType == XmlNodeType.None ? -1 : reader.Depth;
        //    do
        //    {
        //        switch (reader.NodeType)
        //        {
        //            case XmlNodeType.Element:
        //                WriteStartElement(reader.Prefix, reader.LocalName, reader.NamespaceURI);
        //                WriteAttributes(reader, defattr);
        //                if (reader.IsEmptyElement)
        //                {
        //                    WriteEndElement();
        //                }
        //                break;
        //            case XmlNodeType.Text:
        //                if (canReadChunk)
        //                {
        //                    _writeNodeBuffer ??= new char[WriteNodeBufferSize];
        //                    int read;
        //                    while ((read = reader.ReadValueChunk(_writeNodeBuffer, 0, WriteNodeBufferSize)) > 0)
        //                    {
        //                        WriteChars(_writeNodeBuffer, 0, read);
        //                    }
        //                }
        //                else
        //                {
        //                    WriteString(reader.Value);
        //                }
        //                break;
        //            case XmlNodeType.Whitespace:
        //            case XmlNodeType.SignificantWhitespace:

        //                WriteWhitespace(reader.Value);

        //                break;
        //            case XmlNodeType.CDATA:
        //                WriteCData(reader.Value);
        //                break;
        //            case XmlNodeType.EntityReference:
        //                WriteEntityRef(reader.Name);
        //                break;
        //            case XmlNodeType.XmlDeclaration:
        //            case XmlNodeType.ProcessingInstruction:
        //                WriteProcessingInstruction(reader.Name, reader.Value);
        //                break;
        //            case XmlNodeType.DocumentType:
        //                WriteDocType(reader.Name, reader.GetAttribute("PUBLIC"), reader.GetAttribute("SYSTEM"), reader.Value);
        //                break;

        //            case XmlNodeType.Comment:
        //                WriteComment(reader.Value);
        //                break;
        //            case XmlNodeType.EndElement:
        //                WriteFullEndElement();
        //                break;
        //        }
        //    } while (reader.Read() && (d < reader.Depth || (d == reader.Depth && reader.NodeType == XmlNodeType.EndElement)));
        //}

        //// Copies the current node from the given XPathNavigator to the writer (including child nodes).
        //public virtual void WriteNode(XPathNavigator navigator, bool defattr)
        //{
        //    ArgumentNullException.ThrowIfNull(navigator);

        //    int iLevel = 0;

        //    navigator = navigator.Clone();

        //    while (true)
        //    {
        //        bool mayHaveChildren = false;
        //        XPathNodeType nodeType = navigator.NodeType;

        //        switch (nodeType)
        //        {
        //            case XPathNodeType.Element:
        //                WriteStartElement(navigator.Prefix, navigator.LocalName, navigator.NamespaceURI);

        //                // Copy attributes
        //                if (navigator.MoveToFirstAttribute())
        //                {
        //                    do
        //                    {
        //                        IXmlSchemaInfo? schemaInfo = navigator.SchemaInfo;
        //                        if (defattr || (schemaInfo == null || !schemaInfo.IsDefault))
        //                        {
        //                            WriteStartAttribute(navigator.Prefix, navigator.LocalName, navigator.NamespaceURI);
        //                            // copy string value to writer
        //                            WriteString(navigator.Value);
        //                            WriteEndAttribute();
        //                        }
        //                    } while (navigator.MoveToNextAttribute());
        //                    navigator.MoveToParent();
        //                }

        //                // Copy namespaces
        //                if (navigator.MoveToFirstNamespace(XPathNamespaceScope.Local))
        //                {
        //                    WriteLocalNamespaces(navigator);
        //                    navigator.MoveToParent();
        //                }
        //                mayHaveChildren = true;
        //                break;
        //            case XPathNodeType.Attribute:
        //                // do nothing on root level attribute
        //                break;
        //            case XPathNodeType.Text:
        //                WriteString(navigator.Value);
        //                break;
        //            case XPathNodeType.SignificantWhitespace:
        //            case XPathNodeType.Whitespace:
        //                WriteWhitespace(navigator.Value);
        //                break;
        //            case XPathNodeType.Root:
        //                mayHaveChildren = true;
        //                break;
        //            case XPathNodeType.Comment:
        //                WriteComment(navigator.Value);
        //                break;
        //            case XPathNodeType.ProcessingInstruction:
        //                WriteProcessingInstruction(navigator.LocalName, navigator.Value);
        //                break;
        //            case XPathNodeType.Namespace:
        //                // do nothing on root level namespace
        //                break;
        //            default:
        //                Debug.Fail($"Unexpected node type {nodeType}");
        //                break;
        //        }

        //        if (mayHaveChildren)
        //        {
        //            // If children exist, move down to next level
        //            if (navigator.MoveToFirstChild())
        //            {
        //                iLevel++;
        //                continue;
        //            }

        //            // EndElement
        //            if (navigator.NodeType == XPathNodeType.Element)
        //            {
        //                if (navigator.IsEmptyElement)
        //                {
        //                    WriteEndElement();
        //                }
        //                else
        //                {
        //                    WriteFullEndElement();
        //                }
        //            }
        //        }

        //        // No children
        //        while (true)
        //        {
        //            if (iLevel == 0)
        //            {
        //                // The entire subtree has been copied
        //                return;
        //            }

        //            if (navigator.MoveToNext())
        //            {
        //                // Found a sibling, so break to outer loop
        //                break;
        //            }

        //            // No siblings, so move up to previous level
        //            iLevel--;
        //            navigator.MoveToParent();

        //            // EndElement
        //            if (navigator.NodeType == XPathNodeType.Element)
        //                WriteFullEndElement();
        //        }
        //    }
        //}

        // Element Helper Methods

        // Writes out an element with the specified name containing the specified string value.
        public void WriteElementString(string localName, string value)
        {
            WriteElementString(localName, null, value);
        }

        public void WriteElementStringCheckEmpty(string name, string Value)
        {
            if (Value != null && Value.Length > 0)
            {
                this.WriteElementString(name, null, Value);
            }
        }
        // Writes out an attribute with the specified name, namespace URI and string value.
        public void WriteElementString(string localName, string ns, string value)
        {
            WriteStartElement(localName, ns);
            if (!string.IsNullOrEmpty(value))
            {
                WriteString(value);
            }

            WriteEndElement();
        }

        //// Writes out an attribute with the specified name, namespace URI, and string value.
        //public void WriteElementString(string? prefix, string localName, string? ns, string? value)
        //{
        //    WriteStartElement(prefix, localName, ns);
        //    if (!string.IsNullOrEmpty(value))
        //    {
        //        WriteString(value);
        //    }

        //    WriteEndElement();
        //}

        public void Dispose()
        {
            Dispose(true);
        }

        // Dispose the underline stream objects (calls Close on the XmlWriter)
        protected virtual void Dispose(bool disposing)
        {
            if (disposing && WriteState != DCWriteState.Closed)
            {
                Close();
            }
        }

        ////// Copy local namespaces on the navigator's current node to the raw writer. The namespaces are returned by the navigator in reversed order.
        ////// The recursive call reverses them back.
        ////private void WriteLocalNamespaces(XPathNavigator nsNav)
        ////{
        ////    string prefix = nsNav.LocalName;
        ////    string ns = nsNav.Value;

        ////    if (nsNav.MoveToNextNamespace(XPathNamespaceScope.Local))
        ////    {
        ////        WriteLocalNamespaces(nsNav);
        ////    }

        ////    if (prefix.Length == 0)
        ////    {
        ////        WriteAttributeString(string.Empty, "xmlns", XmlReservedNs.NsXmlNs, ns);
        ////    }
        ////    else
        ////    {
        ////        WriteAttributeString("xmlns", prefix, XmlReservedNs.NsXmlNs, ns);
        ////    }
        ////}

        //////
        ////// Static methods for creating writers
        //////
        ////// Creates an XmlWriter for writing into the provided file.
        ////public static XmlWriter Create(string outputFileName)
        ////{
        ////    ArgumentNullException.ThrowIfNull(outputFileName);

        ////    // Avoid using XmlWriter.Create(string, XmlReaderSettings), as it references a lot of types
        ////    // that then can't be trimmed away.
        ////    var fs = new FileStream(outputFileName, FileMode.Create, FileAccess.Write, FileShare.Read);
        ////    try
        ////    {
        ////        var settings = new XmlWriterSettings { CloseOutput = true };
        ////        XmlWriter writer = new XmlEncodedRawTextWriter(fs, settings);
        ////        return new XmlWellFormedWriter(writer, settings);
        ////    }
        ////    catch
        ////    {
        ////        fs.Dispose();
        ////        throw;
        ////    }
        ////}

        ////// Creates an XmlWriter for writing into the provided file with the specified settings.
        ////public static XmlWriter Create(string outputFileName, XmlWriterSettings? settings)
        ////{
        ////    settings ??= XmlWriterSettings.s_defaultWriterSettings;
        ////    return settings.CreateWriter(outputFileName);
        ////}

        ////// Creates an XmlWriter for writing into the provided stream.
        ////public static XmlWriter Create(Stream output)
        ////{
        ////    ArgumentNullException.ThrowIfNull(output);

        ////    // Avoid using XmlWriter.Create(Stream, XmlReaderSettings), as it references a lot of types
        ////    // that then can't be trimmed away.
        ////    XmlWriterSettings settings = XmlWriterSettings.s_defaultWriterSettings;
        ////    XmlWriter writer = new XmlUtf8RawTextWriter(output, settings);
        ////    return new XmlWellFormedWriter(writer, settings);
        ////}

        ////// Creates an XmlWriter for writing into the provided stream with the specified settings.
        ////public static XmlWriter Create(Stream output, XmlWriterSettings? settings)
        ////{
        ////    settings ??= XmlWriterSettings.s_defaultWriterSettings;
        ////    return settings.CreateWriter(output);
        ////}

        //// Creates an XmlWriter for writing into the provided TextWriter.
        //public static XmlWriter Create(TextWriter output)
        //{
        //    ArgumentNullException.ThrowIfNull(output);

        //    // Avoid using XmlWriter.Create(TextWriter, XmlReaderSettings), as it references a lot of types
        //    // that then can't be trimmed away.
        //    XmlWriterSettings settings = XmlWriterSettings.s_defaultWriterSettings;
        //    XmlWriter writer = new XmlEncodedRawTextWriter(output, settings);
        //    return new XmlWellFormedWriter(writer, settings);
        //}

        //// Creates an XmlWriter for writing into the provided TextWriter with the specified settings.
        //public static XmlWriter Create(TextWriter output, XmlWriterSettings? settings)
        //{
        //    settings ??= XmlWriterSettings.s_defaultWriterSettings;
        //    return settings.CreateWriter(output);
        //}

        //// Creates an XmlWriter for writing into the provided StringBuilder.
        //public static XmlWriter Create(StringBuilder output)
        //{
        //    ArgumentNullException.ThrowIfNull(output);

        //    // Avoid using XmlWriter.Create(StringBuilder, XmlReaderSettings), as it references a lot of types
        //    // that then can't be trimmed away.
        //    return Create(new StringWriter(output, CultureInfo.InvariantCulture));
        //}

        //// Creates an XmlWriter for writing into the provided StringBuilder with the specified settings.
        //public static XmlWriter Create(StringBuilder output, XmlWriterSettings? settings)
        //{
        //    ArgumentNullException.ThrowIfNull(output);

        //    settings ??= XmlWriterSettings.s_defaultWriterSettings;
        //    return settings.CreateWriter(new StringWriter(output, CultureInfo.InvariantCulture));
        //}

        //// Creates an XmlWriter wrapped around the provided XmlWriter with the default settings.
        //public static XmlWriter Create(XmlWriter output)
        //{
        //    return Create(output, null);
        //}

        //// Creates an XmlWriter wrapped around the provided XmlWriter with the specified settings.
        //public static XmlWriter Create(XmlWriter output, XmlWriterSettings? settings)
        //{
        //    settings ??= XmlWriterSettings.s_defaultWriterSettings;
        //    return settings.CreateWriter(output);
        //}
    }
    // XmlTextEncoder
    //
    // This class does special handling of text content for XML.  For example
    // it will replace special characters with entities whenever necessary.
    internal sealed class DCXmlTextEncoder
    {
        //
        // Fields
        //
        // output text writer
        private readonly TextWriter _textWriter;

        // true when writing out the content of attribute value
        private bool _inAttribute;

        // quote char of the attribute (when inAttribute)
        private char _quoteChar;

        // caching of attribute value
        private StringBuilder _attrValue;
        internal bool _cacheAttrValue;

        //
        // Constructor
        //
        internal DCXmlTextEncoder(TextWriter textWriter)
        {
            _textWriter = textWriter;
            _quoteChar = '"';
        }

        //
        // Internal methods and properties
        //
        internal char QuoteChar
        {
            set
            {
                _quoteChar = value;
            }
        }

        internal void StartAttribute(bool cacheAttrValue)
        {
            _inAttribute = true;
            _cacheAttrValue = cacheAttrValue;
            if (cacheAttrValue)
            {
                if (_attrValue == null)
                {
                    _attrValue = new StringBuilder();
                }
                else
                {
                    _attrValue.Length = 0;
                }
            }
        }

        internal void EndAttribute()
        {
            if (_cacheAttrValue)
            {
                //Debug.Assert(_attrValue != null);
                _attrValue.Length = 0;
            }

            _inAttribute = false;
            _cacheAttrValue = false;
        }

        internal string AttributeValue
        {
            get
            {
                if (_cacheAttrValue)
                {
                    //Debug.Assert(_attrValue != null);
                    return _attrValue.ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        internal void WriteSurrogateChar(char lowChar, char highChar)
        {
            if (!DCXmlCharType.IsLowSurrogate(lowChar) ||
                 !DCXmlCharType.IsHighSurrogate(highChar))
            {
                throw new InvalidDataException("InvalidSurrogatePair" + lowChar+ "," + highChar);
            }

            _textWriter.Write(highChar);
            _textWriter.Write(lowChar);
        }

        //internal void Write(char[] array, int offset, int count)
        //{
        //    ArgumentNullException.ThrowIfNull(array);

        //    if (0 > offset)
        //    {
        //        throw new ArgumentOutOfRangeException(nameof(offset));
        //    }

        //    if (0 > count)
        //    {
        //        throw new ArgumentOutOfRangeException(nameof(count));
        //    }

        //    if (count > array.Length - offset)
        //    {
        //        throw new ArgumentOutOfRangeException(nameof(count));
        //    }

        //    if (_cacheAttrValue)
        //    {
        //       //Debug.Assert(_attrValue != null);
        //        _attrValue.Append(array, offset, count);
        //    }

        //    int endPos = offset + count;
        //    int i = offset;
        //    char ch = (char)0;
        //    while (true)
        //    {
        //        int startPos = i;
        //        while (i < endPos && XmlCharType.IsAttributeValueChar(ch = array[i]))
        //        {
        //            i++;
        //        }

        //        if (startPos < i)
        //        {
        //            _textWriter.Write(array, startPos, i - startPos);
        //        }
        //        if (i == endPos)
        //        {
        //            break;
        //        }

        //        switch (ch)
        //        {
        //            case (char)0x9:
        //                _textWriter.Write(ch);
        //                break;
        //            case (char)0xA:
        //            case (char)0xD:
        //                if (_inAttribute)
        //                {
        //                    WriteCharEntityImpl(ch);
        //                }
        //                else
        //                {
        //                    _textWriter.Write(ch);
        //                }
        //                break;

        //            case '<':
        //                WriteEntityRefImpl("lt");
        //                break;
        //            case '>':
        //                WriteEntityRefImpl("gt");
        //                break;
        //            case '&':
        //                WriteEntityRefImpl("amp");
        //                break;
        //            case '\'':
        //                if (_inAttribute && _quoteChar == ch)
        //                {
        //                    WriteEntityRefImpl("apos");
        //                }
        //                else
        //                {
        //                    _textWriter.Write('\'');
        //                }
        //                break;
        //            case '"':
        //                if (_inAttribute && _quoteChar == ch)
        //                {
        //                    WriteEntityRefImpl("quot");
        //                }
        //                else
        //                {
        //                    _textWriter.Write('"');
        //                }
        //                break;
        //            default:
        //                if (XmlCharType.IsHighSurrogate(ch))
        //                {
        //                    if (i + 1 < endPos)
        //                    {
        //                        WriteSurrogateChar(array[++i], ch);
        //                    }
        //                    else
        //                    {
        //                        throw new ArgumentException();
        //                    }
        //                }
        //                else if (XmlCharType.IsLowSurrogate(ch))
        //                {
        //                    throw XmlConvert.CreateInvalidHighSurrogateCharException(ch);
        //                }
        //                else
        //                {
        //                   //Debug.Assert((ch < 0x20 && !XmlCharType.IsWhiteSpace(ch)) || (ch > 0xFFFD));
        //                    WriteCharEntityImpl(ch);
        //                }
        //                break;
        //        }
        //        i++;
        //    }
        //}

        //internal void WriteSurrogateCharEntity(char lowChar, char highChar)
        //{
        //    if (!XmlCharType.IsLowSurrogate(lowChar) ||
        //         !XmlCharType.IsHighSurrogate(highChar))
        //    {
        //        throw XmlConvert.CreateInvalidSurrogatePairException(lowChar, highChar);
        //    }
        //    int surrogateChar = XmlCharType.CombineSurrogateChar(lowChar, highChar);

        //    if (_cacheAttrValue)
        //    {
        //       //Debug.Assert(_attrValue != null);
        //        _attrValue.Append(highChar);
        //        _attrValue.Append(lowChar);
        //    }

        //    _textWriter.Write("&#x");
        //    _textWriter.Write(surrogateChar.ToString("X", NumberFormatInfo.InvariantInfo));
        //    _textWriter.Write(';');
        //}

        public static bool IsNeedEncode(char c)
        {
            if (DCXmlCharType.IsAttributeValueChar(c))
            {
                return false;
            }
            return true;
        }
        internal void Write(ReadOnlySpan<char> text)
        {
            if (text.IsEmpty)
            {
                return;
            }

            if (_cacheAttrValue)
            {
                //Debug.Assert(_attrValue != null);
                _attrValue.Append(text);
            }

            // scan through the string to see if there are any characters to be escaped
            int len = text.Length;
            int i = 0;
            int startPos = 0;
            char ch = (char)0;
            while (true)
            {
                while (i < len && DCXmlCharType.IsAttributeValueChar(ch = text[i]))
                {
                    i++;
                }

                if (i == len)
                {
                    // reached the end of the string -> write it whole out
                    _textWriter.Write(text);
                    return;
                }
                if (_inAttribute)
                {
                    if (ch == 0x9)
                    {
                        i++;
                        continue;
                    }
                }
                else
                {
                    if (ch == 0x9 || ch == 0xA || ch == 0xD || ch == '"' || ch == '\'')
                    {
                        i++;
                        continue;
                    }
                }
                // some character that needs to be escaped is found:
                break;
            }

            while (true)
            {
                if (startPos < i)
                {
                    _textWriter.Write(text.Slice(startPos, i - startPos));
                }

                if (i == len)
                {
                    break;
                }

                switch (ch)
                {
                    case (char)0x9:
                        _textWriter.Write(ch);
                        break;
                    case (char)0xA:
                    case (char)0xD:
                        if (_inAttribute)
                        {
                            WriteCharEntityImpl(ch);
                        }
                        else
                        {
                            _textWriter.Write(ch);
                        }
                        break;
                    case '<':
                        WriteEntityRefImpl("lt");
                        break;
                    case '>':
                        WriteEntityRefImpl("gt");
                        break;
                    case '&':
                        WriteEntityRefImpl("amp");
                        break;
                    case '\'':
                        if (_inAttribute && _quoteChar == ch)
                        {
                            WriteEntityRefImpl("apos");
                        }
                        else
                        {
                            _textWriter.Write('\'');
                        }
                        break;
                    case '"':
                        if (_inAttribute && _quoteChar == ch)
                        {
                            WriteEntityRefImpl("quot");
                        }
                        else
                        {
                            _textWriter.Write('"');
                        }
                        break;
                    default:
                        if (DCXmlCharType.IsHighSurrogate(ch))
                        {
                            if (i + 1 < len)
                            {
                                WriteSurrogateChar(text[++i], ch);
                            }
                            else
                            {
                                throw new InvalidDataException("InvalidSurrogatePair" + text[i] + "," + ch);
                            }
                        }
                        else if (DCXmlCharType.IsLowSurrogate(ch))
                        {
                            throw new InvalidDataException("InvalidHighSurrogateChar" +ch);
                        }
                        else
                        {
                            //Debug.Assert((ch < 0x20 && !XmlCharType.IsWhiteSpace(ch)) || (ch > 0xFFFD));
                            WriteCharEntityImpl(ch);
                        }
                        break;
                }
                i++;
                startPos = i;
                while (i < len && DCXmlCharType.IsAttributeValueChar(ch = text[i]))
                {
                    i++;
                }
            }
        }
        //internal void WriteRaw2(string text)
        //{
        //    _textWriter.Write(text);
        //}

        internal void WriteRawWithSurrogateChecking(string text)
        {
            if (text == null)
            {
                return;
            }

            if (_cacheAttrValue)
            {
                //Debug.Assert(_attrValue != null);
                _attrValue.Append(text);
            }

            int len = text.Length;
            int i = 0;
            char ch = (char)0;

            while (true)
            {
                while (i < len && (DCXmlCharType.IsCharData((ch = text[i])) || ch < 0x20))
                {
                    i++;
                }
                if (i == len)
                {
                    break;
                }
                if (DCXmlCharType.IsHighSurrogate(ch))
                {
                    if (i + 1 < len)
                    {
                        char lowChar = text[i + 1];
                        if (DCXmlCharType.IsLowSurrogate(lowChar))
                        {
                            i += 2;
                            continue;
                        }
                        else
                        {
                            throw new InvalidDataException("InvalidSurrogatePair"+ lowChar + "," + ch);
                        }
                    }
                    throw new ArgumentException();
                }
                else if (DCXmlCharType.IsLowSurrogate(ch))
                {
                    throw new InvalidDataException("InvalidHighSurrogateChar" + ch);
                }
                else
                {
                    i++;
                }
            }

            _textWriter.Write(text);
            return;
        }
        //#if ! LightWeight
        //        internal void WriteRaw(char[] array, int offset, int count)
        //        {
        //            ArgumentNullException.ThrowIfNull(array);

        //            if (0 > count)
        //            {
        //                throw new ArgumentOutOfRangeException(nameof(count));
        //            }

        //            if (0 > offset)
        //            {
        //                throw new ArgumentOutOfRangeException(nameof(offset));
        //            }

        //            if (count > array.Length - offset)
        //            {
        //                throw new ArgumentOutOfRangeException(nameof(count));
        //            }

        //            if (_cacheAttrValue)
        //            {
        //               //Debug.Assert(_attrValue != null);
        //                _attrValue.Append(array, offset, count);
        //            }

        //            _textWriter.Write(array, offset, count);
        //        }
        //#endif


        internal void WriteCharEntity(char ch)
        {
            if (DCXmlCharType.IsSurrogate(ch))
            {
                throw new ArgumentException();
            }

            string strVal = ((int)ch).ToString("X", System.Globalization.NumberFormatInfo.InvariantInfo);
            if (_cacheAttrValue)
            {
                //Debug.Assert(_attrValue != null);
                _attrValue.Append("&#x");
                _attrValue.Append(strVal);
                _attrValue.Append(';');
            }

            WriteCharEntityImpl(strVal);
        }

        internal void WriteEntityRef(string name)
        {
            if (_cacheAttrValue)
            {
                //Debug.Assert(_attrValue != null);
                _attrValue.Append('&');
                _attrValue.Append(name);
                _attrValue.Append(';');
            }

            WriteEntityRefImpl(name);
        }

        //
        // Private implementation methods
        //

        private void WriteCharEntityImpl(char ch)
        {
            WriteCharEntityImpl(((int)ch).ToString("X", System.Globalization.NumberFormatInfo.InvariantInfo));
        }

        private void WriteCharEntityImpl(string strVal)
        {
            _textWriter.Write("&#x");
            _textWriter.Write(strVal);
            _textWriter.Write(';');
        }

        private void WriteEntityRefImpl(string name)
        {
            _textWriter.Write('&');
            _textWriter.Write(name);
            _textWriter.Write(';');
        }
    }
    internal static class DCXmlCharType
    {
        // Surrogate constants
        internal const int SurHighStart = 0xd800;    // 1101 10xx
        internal const int SurHighEnd = 0xdbff;
        internal const int SurLowStart = 0xdc00;    // 1101 11xx
        internal const int SurLowEnd = 0xdfff;
        //internal const int SurMask = 0xfc00;    // 1111 11xx

        // Characters defined in the XML 1.0 Fourth Edition
        // Whitespace chars -- Section 2.3 [3]
        // Letters -- Appendix B [84]
        // Starting NCName characters -- Section 2.3 [5] (Starting Name characters without ':')
        // NCName characters -- Section 2.3 [4]          (Name characters without ':')
        // Character data characters -- Section 2.2 [2]
        // PubidChar ::=  #x20 | #xD | #xA | [a-zA-Z0-9] | [-'()+,./:=?;!*#@$_%] Section 2.3 of spec
        internal const uint Whitespace = 1;
        //internal const uint Letter = 2;
        internal const uint NCStartNameSC = 4;
        internal const uint NCNameSC = 8;
        internal const uint CharData = 16;
        //internal const uint NCNameXml4e = 32;
        //internal const uint Text = 64;
        internal const uint AttrValue = 128;

        // bitmap for public ID characters - 1 bit per character 0x0 - 0x80; no character > 0x80 is a PUBLIC ID char
        //private const string PublicIdBitmap = "\u2400\u0000\uffbb\uafff\uffff\u87ff\ufffe\u07ff";


        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsWhiteSpace(char ch) => (GetCharProperties(ch) & Whitespace) != 0u;

        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNCNameSingleChar(char ch) => (GetCharProperties(ch) & NCNameSC) != 0u;

        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsStartNCNameSingleChar(char ch) => (GetCharProperties(ch) & NCStartNameSC) != 0u;

        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static bool IsNameSingleChar(char ch) => IsNCNameSingleChar(ch) || ch == ':';

        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsCharData(char ch) => (GetCharProperties(ch) & CharData) != 0u;

        //// [13] PubidChar ::=  #x20 | #xD | #xA | [a-zA-Z0-9] | [-'()+,./:=?;!*#@$_%] Section 2.3 of spec
        ////[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static bool IsPubidChar(char ch)
        //{
        //    if (ch < (char)0x80)
        //    {
        //        return (PublicIdBitmap[ch >> 4] & (1 << (ch & 0xF))) != 0;
        //    }
        //    return false;
        //}

        // TextChar = CharData - { 0xA, 0xD, '<', '&', ']' }
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //internal static bool IsTextChar(char ch) => (GetCharProperties(ch) & Text) != 0u;

        // AttrValueChar = CharData - { 0xA, 0xD, 0x9, '<', '>', '&', '\'', '"' }
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static bool IsAttributeValueChar(char ch) => (GetCharProperties(ch) & AttrValue) != 0u;

        //// XML 1.0 Fourth Edition definitions
        ////[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static bool IsLetter(char ch) => (GetCharProperties(ch) & Letter) != 0u;

        //// This method uses the XML 4th edition name character ranges
        ////[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static bool IsNCNameCharXml4e(char ch) => (GetCharProperties(ch) & NCNameXml4e) != 0u;

        //// This method uses the XML 4th edition name character ranges
        ////[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static bool IsStartNCNameCharXml4e(char ch) => IsLetter(ch) || ch == '_';

        //// This method uses the XML 4th edition name character ranges
        ////[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static bool IsNameCharXml4e(char ch) => IsNCNameCharXml4e(ch) || ch == ':';

        // Digit methods
        public static bool IsDigit(char ch) => InRange(ch, 0x30, 0x39);

        // Surrogate methods
        internal static bool IsHighSurrogate(int ch) => InRange(ch, SurHighStart, SurHighEnd);

        internal static bool IsLowSurrogate(int ch) => InRange(ch, SurLowStart, SurLowEnd);

        internal static bool IsSurrogate(int ch) => InRange(ch, SurHighStart, SurLowEnd);

        //internal static int CombineSurrogateChar(int lowChar, int highChar)
        //{
        //    return (lowChar - SurLowStart) | ((highChar - SurHighStart) << 10) + 0x10000;
        //}

        //internal static void SplitSurrogateChar(int combinedChar, out char lowChar, out char highChar)
        //{
        //    int v = combinedChar - 0x10000;
        //    lowChar = (char)(SurLowStart + v % 1024);
        //    highChar = (char)(SurHighStart + v / 1024);
        //}

        internal static bool IsOnlyWhitespace(string str)
        {
            return IsOnlyWhitespaceWithPos(str) == -1;
        }

        // Character checking on strings
        internal static int IsOnlyWhitespaceWithPos(string str)
        {
            if (str != null)
            {
                for (int i = 0; i < str.Length; i++)
                {
                    if ((GetCharProperties(str[i]) & Whitespace) == 0u)
                    {
                        return i;
                    }
                }
            }
            return -1;
        }

        //internal static int IsOnlyCharData(string str)
        //{
        //    if (str != null)
        //    {
        //        for (int i = 0; i < str.Length; i++)
        //        {
        //            if ((GetCharProperties(str[i]) & CharData) == 0u)
        //            {
        //                if (i + 1 >= str.Length || !(XmlCharType.IsHighSurrogate(str[i]) && XmlCharType.IsLowSurrogate(str[i + 1])))
        //                {
        //                    return i;
        //                }
        //                else
        //                {
        //                    i++;
        //                }
        //            }
        //        }
        //    }
        //    return -1;
        //}

        internal static bool IsOnlyDigits(string str, int startPos, int len)
        {
            //Debug.Assert(str != null);
            //Debug.Assert(startPos + len <= str.Length);
            //Debug.Assert(startPos <= str.Length);

            for (int i = startPos; i < startPos + len; i++)
            {
                if (!IsDigit(str[i]))
                {
                    return false;
                }
            }
            return true;
        }

        //internal static int IsPublicId(string str)
        //{
        //    if (str != null)
        //    {
        //        for (int i = 0; i < str.Length; i++)
        //        {
        //            if (!IsPubidChar(str[i]))
        //            {
        //                return i;
        //            }
        //        }
        //    }
        //    return -1;
        //}

        // This method tests whether a value is in a given range with just one test; start and end should be constants
        private static bool InRange(int value, int start, int end)
        {
            //Debug.Assert(start <= end);
            return (uint)(value - start) <= (uint)(end - start);
        }


        public static uint GetCharProperties(char value)
        {
            if (value <= 127)
            {
                // 在绝大多数情况下，是编码小于128的字符。
                return _ASNICharProperties[value];
            }
            if (value >= 19968 && value <= 40869) return 254;
            int startIndex = 0;
            int endIndex = _CharProperties.Length / 3;
            while (endIndex > startIndex)
            {
                int middleIndex = (startIndex + endIndex) >> 1;
                int pos = middleIndex * 3;
                if (middleIndex == startIndex)
                {
                    if (value >= _CharProperties[pos] && value <= _CharProperties[pos + 1])
                    {
                        // 命中范围
                        return (byte)_CharProperties[pos + 2];
                    }
                    break;
                }
                if (value < _CharProperties[pos])
                {
                    endIndex = middleIndex;
                }
                else if (value <= _CharProperties[pos + 1])
                {
                    // 命中范围
                    return _CharProperties[pos + 2];
                }
                else
                {
                    startIndex = middleIndex;
                }
            }
            return 0;
        }

        private static readonly byte[] _ASNICharProperties = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 17, 17, 0, 0, 17, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 209, 208, 80, 208, 208, 208, 16, 80, 208, 208, 208, 208, 208, 248, 248, 208, 248, 248, 248, 248, 248, 248, 248, 248, 248, 248, 208, 208, 16, 208, 80, 208, 208, 254, 254, 254, 254, 254, 254, 254, 254, 254, 254, 254, 254, 254, 254, 254, 254, 254, 254, 254, 254, 254, 254, 254, 254, 254, 254, 208, 208, 144, 208, 252, 208, 254, 254, 254, 254, 254, 254, 254, 254, 254, 254, 254, 254, 254, 254, 254, 254, 254, 254, 254, 254, 254, 254, 254, 254, 254, 254, 208, 208, 208, 208, 208 };
        private static readonly ushort[] _CharProperties = new ushort[]{       128,128,0,// Len=1
            129,182,208,// Len=54
            183,183,248,// Len=1
            184,191,208,// Len=8
            192,214,254,// Len=23
            215,215,208,// Len=1
            216,246,254,// Len=31
            247,247,208,// Len=1
            248,305,254,// Len=58
            306,307,208,// Len=2
            308,318,254,// Len=11
            319,320,208,// Len=2
            321,328,254,// Len=8
            329,329,208,// Len=1
            330,382,254,// Len=53
            383,383,208,// Len=1
            384,451,254,// Len=68
            452,460,208,// Len=9
            461,496,254,// Len=36
            497,499,208,// Len=3
            500,501,254,// Len=2
            502,505,208,// Len=4
            506,535,254,// Len=30
            536,591,208,// Len=56
            592,680,254,// Len=89
            681,698,208,// Len=18
            699,705,254,// Len=7
            706,719,208,// Len=14
            720,721,248,// Len=2
            722,767,208,// Len=46
            768,837,248,// Len=70
            838,863,208,// Len=26
            864,865,248,// Len=2
            866,901,208,// Len=36
            902,902,254,// Len=1
            903,903,248,// Len=1
            904,906,254,// Len=3
            907,907,208,// Len=1
            908,908,254,// Len=1
            909,909,208,// Len=1
            910,929,254,// Len=20
            930,930,208,// Len=1
            931,974,254,// Len=44
            975,975,208,// Len=1
            976,982,254,// Len=7
            983,985,208,// Len=3
            986,986,254,// Len=1
            987,987,208,// Len=1
            988,988,254,// Len=1
            989,989,208,// Len=1
            990,990,254,// Len=1
            991,991,208,// Len=1
            992,992,254,// Len=1
            993,993,208,// Len=1
            994,1011,254,// Len=18
            1012,1024,208,// Len=13
            1025,1036,254,// Len=12
            1037,1037,208,// Len=1
            1038,1103,254,// Len=66
            1104,1104,208,// Len=1
            1105,1116,254,// Len=12
            1117,1117,208,// Len=1
            1118,1153,254,// Len=36
            1154,1154,208,// Len=1
            1155,1158,248,// Len=4
            1159,1167,208,// Len=9
            1168,1220,254,// Len=53
            1221,1222,208,// Len=2
            1223,1224,254,// Len=2
            1225,1226,208,// Len=2
            1227,1228,254,// Len=2
            1229,1231,208,// Len=3
            1232,1259,254,// Len=28
            1260,1261,208,// Len=2
            1262,1269,254,// Len=8
            1270,1271,208,// Len=2
            1272,1273,254,// Len=2
            1274,1328,208,// Len=55
            1329,1366,254,// Len=38
            1367,1368,208,// Len=2
            1369,1369,254,// Len=1
            1370,1376,208,// Len=7
            1377,1414,254,// Len=38
            1415,1424,208,// Len=10
            1425,1441,248,// Len=17
            1442,1442,208,// Len=1
            1443,1465,248,// Len=23
            1466,1466,208,// Len=1
            1467,1469,248,// Len=3
            1470,1470,208,// Len=1
            1471,1471,248,// Len=1
            1472,1472,208,// Len=1
            1473,1474,248,// Len=2
            1475,1475,208,// Len=1
            1476,1476,248,// Len=1
            1477,1487,208,// Len=11
            1488,1514,254,// Len=27
            1515,1519,208,// Len=5
            1520,1522,254,// Len=3
            1523,1568,208,// Len=46
            1569,1594,254,// Len=26
            1595,1599,208,// Len=5
            1600,1600,248,// Len=1
            1601,1610,254,// Len=10
            1611,1618,248,// Len=8
            1619,1631,208,// Len=13
            1632,1641,248,// Len=10
            1642,1647,208,// Len=6
            1648,1648,248,// Len=1
            1649,1719,254,// Len=71
            1720,1721,208,// Len=2
            1722,1726,254,// Len=5
            1727,1727,208,// Len=1
            1728,1742,254,// Len=15
            1743,1743,208,// Len=1
            1744,1747,254,// Len=4
            1748,1748,208,// Len=1
            1749,1749,254,// Len=1
            1750,1764,248,// Len=15
            1765,1766,254,// Len=2
            1767,1768,248,// Len=2
            1769,1769,208,// Len=1
            1770,1773,248,// Len=4
            1774,1775,208,// Len=2
            1776,1785,248,// Len=10
            1786,2304,208,// Len=519
            2305,2307,248,// Len=3
            2308,2308,208,// Len=1
            2309,2361,254,// Len=53
            2362,2363,208,// Len=2
            2364,2364,248,// Len=1
            2365,2365,254,// Len=1
            2366,2381,248,// Len=16
            2382,2384,208,// Len=3
            2385,2388,248,// Len=4
            2389,2391,208,// Len=3
            2392,2401,254,// Len=10
            2402,2403,248,// Len=2
            2404,2405,208,// Len=2
            2406,2415,248,// Len=10
            2416,2432,208,// Len=17
            2433,2435,248,// Len=3
            2436,2436,208,// Len=1
            2437,2444,254,// Len=8
            2445,2446,208,// Len=2
            2447,2448,254,// Len=2
            2449,2450,208,// Len=2
            2451,2472,254,// Len=22
            2473,2473,208,// Len=1
            2474,2480,254,// Len=7
            2481,2481,208,// Len=1
            2482,2482,254,// Len=1
            2483,2485,208,// Len=3
            2486,2489,254,// Len=4
            2490,2491,208,// Len=2
            2492,2492,248,// Len=1
            2493,2493,208,// Len=1
            2494,2500,248,// Len=7
            2501,2502,208,// Len=2
            2503,2504,248,// Len=2
            2505,2506,208,// Len=2
            2507,2509,248,// Len=3
            2510,2518,208,// Len=9
            2519,2519,248,// Len=1
            2520,2523,208,// Len=4
            2524,2525,254,// Len=2
            2526,2526,208,// Len=1
            2527,2529,254,// Len=3
            2530,2531,248,// Len=2
            2532,2533,208,// Len=2
            2534,2543,248,// Len=10
            2544,2545,254,// Len=2
            2546,2561,208,// Len=16
            2562,2562,248,// Len=1
            2563,2564,208,// Len=2
            2565,2570,254,// Len=6
            2571,2574,208,// Len=4
            2575,2576,254,// Len=2
            2577,2578,208,// Len=2
            2579,2600,254,// Len=22
            2601,2601,208,// Len=1
            2602,2608,254,// Len=7
            2609,2609,208,// Len=1
            2610,2611,254,// Len=2
            2612,2612,208,// Len=1
            2613,2614,254,// Len=2
            2615,2615,208,// Len=1
            2616,2617,254,// Len=2
            2618,2619,208,// Len=2
            2620,2620,248,// Len=1
            2621,2621,208,// Len=1
            2622,2626,248,// Len=5
            2627,2630,208,// Len=4
            2631,2632,248,// Len=2
            2633,2634,208,// Len=2
            2635,2637,248,// Len=3
            2638,2648,208,// Len=11
            2649,2652,254,// Len=4
            2653,2653,208,// Len=1
            2654,2654,254,// Len=1
            2655,2661,208,// Len=7
            2662,2673,248,// Len=12
            2674,2676,254,// Len=3
            2677,2688,208,// Len=12
            2689,2691,248,// Len=3
            2692,2692,208,// Len=1
            2693,2699,254,// Len=7
            2700,2700,208,// Len=1
            2701,2701,254,// Len=1
            2702,2702,208,// Len=1
            2703,2705,254,// Len=3
            2706,2706,208,// Len=1
            2707,2728,254,// Len=22
            2729,2729,208,// Len=1
            2730,2736,254,// Len=7
            2737,2737,208,// Len=1
            2738,2739,254,// Len=2
            2740,2740,208,// Len=1
            2741,2745,254,// Len=5
            2746,2747,208,// Len=2
            2748,2748,248,// Len=1
            2749,2749,254,// Len=1
            2750,2757,248,// Len=8
            2758,2758,208,// Len=1
            2759,2761,248,// Len=3
            2762,2762,208,// Len=1
            2763,2765,248,// Len=3
            2766,2783,208,// Len=18
            2784,2784,254,// Len=1
            2785,2789,208,// Len=5
            2790,2799,248,// Len=10
            2800,2816,208,// Len=17
            2817,2819,248,// Len=3
            2820,2820,208,// Len=1
            2821,2828,254,// Len=8
            2829,2830,208,// Len=2
            2831,2832,254,// Len=2
            2833,2834,208,// Len=2
            2835,2856,254,// Len=22
            2857,2857,208,// Len=1
            2858,2864,254,// Len=7
            2865,2865,208,// Len=1
            2866,2867,254,// Len=2
            2868,2869,208,// Len=2
            2870,2873,254,// Len=4
            2874,2875,208,// Len=2
            2876,2876,248,// Len=1
            2877,2877,254,// Len=1
            2878,2883,248,// Len=6
            2884,2886,208,// Len=3
            2887,2888,248,// Len=2
            2889,2890,208,// Len=2
            2891,2893,248,// Len=3
            2894,2901,208,// Len=8
            2902,2903,248,// Len=2
            2904,2907,208,// Len=4
            2908,2909,254,// Len=2
            2910,2910,208,// Len=1
            2911,2913,254,// Len=3
            2914,2917,208,// Len=4
            2918,2927,248,// Len=10
            2928,2945,208,// Len=18
            2946,2947,248,// Len=2
            2948,2948,208,// Len=1
            2949,2954,254,// Len=6
            2955,2957,208,// Len=3
            2958,2960,254,// Len=3
            2961,2961,208,// Len=1
            2962,2965,254,// Len=4
            2966,2968,208,// Len=3
            2969,2970,254,// Len=2
            2971,2971,208,// Len=1
            2972,2972,254,// Len=1
            2973,2973,208,// Len=1
            2974,2975,254,// Len=2
            2976,2978,208,// Len=3
            2979,2980,254,// Len=2
            2981,2983,208,// Len=3
            2984,2986,254,// Len=3
            2987,2989,208,// Len=3
            2990,2997,254,// Len=8
            2998,2998,208,// Len=1
            2999,3001,254,// Len=3
            3002,3005,208,// Len=4
            3006,3010,248,// Len=5
            3011,3013,208,// Len=3
            3014,3016,248,// Len=3
            3017,3017,208,// Len=1
            3018,3021,248,// Len=4
            3022,3030,208,// Len=9
            3031,3031,248,// Len=1
            3032,3046,208,// Len=15
            3047,3055,248,// Len=9
            3056,3072,208,// Len=17
            3073,3075,248,// Len=3
            3076,3076,208,// Len=1
            3077,3084,254,// Len=8
            3085,3085,208,// Len=1
            3086,3088,254,// Len=3
            3089,3089,208,// Len=1
            3090,3112,254,// Len=23
            3113,3113,208,// Len=1
            3114,3123,254,// Len=10
            3124,3124,208,// Len=1
            3125,3129,254,// Len=5
            3130,3133,208,// Len=4
            3134,3140,248,// Len=7
            3141,3141,208,// Len=1
            3142,3144,248,// Len=3
            3145,3145,208,// Len=1
            3146,3149,248,// Len=4
            3150,3156,208,// Len=7
            3157,3158,248,// Len=2
            3159,3167,208,// Len=9
            3168,3169,254,// Len=2
            3170,3173,208,// Len=4
            3174,3183,248,// Len=10
            3184,3201,208,// Len=18
            3202,3203,248,// Len=2
            3204,3204,208,// Len=1
            3205,3212,254,// Len=8
            3213,3213,208,// Len=1
            3214,3216,254,// Len=3
            3217,3217,208,// Len=1
            3218,3240,254,// Len=23
            3241,3241,208,// Len=1
            3242,3251,254,// Len=10
            3252,3252,208,// Len=1
            3253,3257,254,// Len=5
            3258,3261,208,// Len=4
            3262,3268,248,// Len=7
            3269,3269,208,// Len=1
            3270,3272,248,// Len=3
            3273,3273,208,// Len=1
            3274,3277,248,// Len=4
            3278,3284,208,// Len=7
            3285,3286,248,// Len=2
            3287,3293,208,// Len=7
            3294,3294,254,// Len=1
            3295,3295,208,// Len=1
            3296,3297,254,// Len=2
            3298,3301,208,// Len=4
            3302,3311,248,// Len=10
            3312,3329,208,// Len=18
            3330,3331,248,// Len=2
            3332,3332,208,// Len=1
            3333,3340,254,// Len=8
            3341,3341,208,// Len=1
            3342,3344,254,// Len=3
            3345,3345,208,// Len=1
            3346,3368,254,// Len=23
            3369,3369,208,// Len=1
            3370,3385,254,// Len=16
            3386,3389,208,// Len=4
            3390,3395,248,// Len=6
            3396,3397,208,// Len=2
            3398,3400,248,// Len=3
            3401,3401,208,// Len=1
            3402,3405,248,// Len=4
            3406,3414,208,// Len=9
            3415,3415,248,// Len=1
            3416,3423,208,// Len=8
            3424,3425,254,// Len=2
            3426,3429,208,// Len=4
            3430,3439,248,// Len=10
            3440,3584,208,// Len=145
            3585,3630,254,// Len=46
            3631,3631,208,// Len=1
            3632,3632,254,// Len=1
            3633,3633,248,// Len=1
            3634,3635,254,// Len=2
            3636,3642,248,// Len=7
            3643,3647,208,// Len=5
            3648,3653,254,// Len=6
            3654,3662,248,// Len=9
            3663,3663,208,// Len=1
            3664,3673,248,// Len=10
            3674,3712,208,// Len=39
            3713,3714,254,// Len=2
            3715,3715,208,// Len=1
            3716,3716,254,// Len=1
            3717,3718,208,// Len=2
            3719,3720,254,// Len=2
            3721,3721,208,// Len=1
            3722,3722,254,// Len=1
            3723,3724,208,// Len=2
            3725,3725,254,// Len=1
            3726,3731,208,// Len=6
            3732,3735,254,// Len=4
            3736,3736,208,// Len=1
            3737,3743,254,// Len=7
            3744,3744,208,// Len=1
            3745,3747,254,// Len=3
            3748,3748,208,// Len=1
            3749,3749,254,// Len=1
            3750,3750,208,// Len=1
            3751,3751,254,// Len=1
            3752,3753,208,// Len=2
            3754,3755,254,// Len=2
            3756,3756,208,// Len=1
            3757,3758,254,// Len=2
            3759,3759,208,// Len=1
            3760,3760,254,// Len=1
            3761,3761,248,// Len=1
            3762,3763,254,// Len=2
            3764,3769,248,// Len=6
            3770,3770,208,// Len=1
            3771,3772,248,// Len=2
            3773,3773,254,// Len=1
            3774,3775,208,// Len=2
            3776,3780,254,// Len=5
            3781,3781,208,// Len=1
            3782,3782,248,// Len=1
            3783,3783,208,// Len=1
            3784,3789,248,// Len=6
            3790,3791,208,// Len=2
            3792,3801,248,// Len=10
            3802,3863,208,// Len=62
            3864,3865,248,// Len=2
            3866,3871,208,// Len=6
            3872,3881,248,// Len=10
            3882,3892,208,// Len=11
            3893,3893,248,// Len=1
            3894,3894,208,// Len=1
            3895,3895,248,// Len=1
            3896,3896,208,// Len=1
            3897,3897,248,// Len=1
            3898,3901,208,// Len=4
            3902,3903,248,// Len=2
            3904,3911,254,// Len=8
            3912,3912,208,// Len=1
            3913,3945,254,// Len=33
            3946,3952,208,// Len=7
            3953,3972,248,// Len=20
            3973,3973,208,// Len=1
            3974,3979,248,// Len=6
            3980,3983,208,// Len=4
            3984,3989,248,// Len=6
            3990,3990,208,// Len=1
            3991,3991,248,// Len=1
            3992,3992,208,// Len=1
            3993,4013,248,// Len=21
            4014,4016,208,// Len=3
            4017,4023,248,// Len=7
            4024,4024,208,// Len=1
            4025,4025,248,// Len=1
            4026,4255,208,// Len=230
            4256,4293,254,// Len=38
            4294,4303,208,// Len=10
            4304,4342,254,// Len=39
            4343,4351,208,// Len=9
            4352,4352,254,// Len=1
            4353,4353,208,// Len=1
            4354,4355,254,// Len=2
            4356,4356,208,// Len=1
            4357,4359,254,// Len=3
            4360,4360,208,// Len=1
            4361,4361,254,// Len=1
            4362,4362,208,// Len=1
            4363,4364,254,// Len=2
            4365,4365,208,// Len=1
            4366,4370,254,// Len=5
            4371,4411,208,// Len=41
            4412,4412,254,// Len=1
            4413,4413,208,// Len=1
            4414,4414,254,// Len=1
            4415,4415,208,// Len=1
            4416,4416,254,// Len=1
            4417,4427,208,// Len=11
            4428,4428,254,// Len=1
            4429,4429,208,// Len=1
            4430,4430,254,// Len=1
            4431,4431,208,// Len=1
            4432,4432,254,// Len=1
            4433,4435,208,// Len=3
            4436,4437,254,// Len=2
            4438,4440,208,// Len=3
            4441,4441,254,// Len=1
            4442,4446,208,// Len=5
            4447,4449,254,// Len=3
            4450,4450,208,// Len=1
            4451,4451,254,// Len=1
            4452,4452,208,// Len=1
            4453,4453,254,// Len=1
            4454,4454,208,// Len=1
            4455,4455,254,// Len=1
            4456,4456,208,// Len=1
            4457,4457,254,// Len=1
            4458,4460,208,// Len=3
            4461,4462,254,// Len=2
            4463,4465,208,// Len=3
            4466,4467,254,// Len=2
            4468,4468,208,// Len=1
            4469,4469,254,// Len=1
            4470,4509,208,// Len=40
            4510,4510,254,// Len=1
            4511,4519,208,// Len=9
            4520,4520,254,// Len=1
            4521,4522,208,// Len=2
            4523,4523,254,// Len=1
            4524,4525,208,// Len=2
            4526,4527,254,// Len=2
            4528,4534,208,// Len=7
            4535,4536,254,// Len=2
            4537,4537,208,// Len=1
            4538,4538,254,// Len=1
            4539,4539,208,// Len=1
            4540,4546,254,// Len=7
            4547,4586,208,// Len=40
            4587,4587,254,// Len=1
            4588,4591,208,// Len=4
            4592,4592,254,// Len=1
            4593,4600,208,// Len=8
            4601,4601,254,// Len=1
            4602,7679,208,// Len=3078
            7680,7835,254,// Len=156
            7836,7839,208,// Len=4
            7840,7929,254,// Len=90
            7930,7935,208,// Len=6
            7936,7957,254,// Len=22
            7958,7959,208,// Len=2
            7960,7965,254,// Len=6
            7966,7967,208,// Len=2
            7968,8005,254,// Len=38
            8006,8007,208,// Len=2
            8008,8013,254,// Len=6
            8014,8015,208,// Len=2
            8016,8023,254,// Len=8
            8024,8024,208,// Len=1
            8025,8025,254,// Len=1
            8026,8026,208,// Len=1
            8027,8027,254,// Len=1
            8028,8028,208,// Len=1
            8029,8029,254,// Len=1
            8030,8030,208,// Len=1
            8031,8061,254,// Len=31
            8062,8063,208,// Len=2
            8064,8116,254,// Len=53
            8117,8117,208,// Len=1
            8118,8124,254,// Len=7
            8125,8125,208,// Len=1
            8126,8126,254,// Len=1
            8127,8129,208,// Len=3
            8130,8132,254,// Len=3
            8133,8133,208,// Len=1
            8134,8140,254,// Len=7
            8141,8143,208,// Len=3
            8144,8147,254,// Len=4
            8148,8149,208,// Len=2
            8150,8155,254,// Len=6
            8156,8159,208,// Len=4
            8160,8172,254,// Len=13
            8173,8177,208,// Len=5
            8178,8180,254,// Len=3
            8181,8181,208,// Len=1
            8182,8188,254,// Len=7
            8189,8399,208,// Len=211
            8400,8412,248,// Len=13
            8413,8416,208,// Len=4
            8417,8417,248,// Len=1
            8418,8485,208,// Len=68
            8486,8486,254,// Len=1
            8487,8489,208,// Len=3
            8490,8491,254,// Len=2
            8492,8493,208,// Len=2
            8494,8494,254,// Len=1
            8495,8575,208,// Len=81
            8576,8578,254,// Len=3
            8579,12292,208,// Len=3714
            12293,12293,248,// Len=1
            12294,12294,208,// Len=1
            12295,12295,254,// Len=1
            12296,12320,208,// Len=25
            12321,12329,254,// Len=9
            12330,12335,248,// Len=6
            12336,12336,208,// Len=1
            12337,12341,248,// Len=5
            12342,12352,208,// Len=11
            12353,12436,254,// Len=84
            12437,12440,208,// Len=4
            12441,12442,248,// Len=2
            12443,12444,208,// Len=2
            12445,12446,248,// Len=2
            12447,12448,208,// Len=2
            12449,12538,254,// Len=90
            12539,12539,208,// Len=1
            12540,12542,248,// Len=3
            12543,12548,208,// Len=6
            12549,12588,254,// Len=40
            12589,19967,208,// Len=7379
            19968,40869,254,// Len=20902
            40870,44031,208,// Len=3162
            44032,55203,254,// Len=11172
            55204,55295,208,// Len=92
            55296,57343,0,// Len=2048
            57344,65533,208// Len=8190
        };
    }

    internal static class DCValidateNames
    {
        //internal enum Flags
        //{
        //    NCNames = 0x1,              // Validate that each non-empty prefix and localName is a valid NCName
        //    CheckLocalName = 0x2,       // Validate the local-name
        //    CheckPrefixMapping = 0x4,   // Validate the prefix --> namespace mapping
        //    All = 0x7,
        //    AllExceptNCNames = 0x6,
        //    AllExceptPrefixMapping = 0x3,
        //};

        //-----------------------------------------------
        // Nmtoken parsing
        //-----------------------------------------------
        /// <summary>
        /// Attempts to parse the input string as an Nmtoken (see the XML spec production [7] &amp;&amp; XML Namespaces spec).
        /// Quits parsing when an invalid Nmtoken char is reached or the end of string is reached.
        /// Returns the number of valid Nmtoken chars that were parsed.
        /// </summary>
        internal static int ParseNmtoken(string s, int offset)
        {

            // Keep parsing until the end of string or an invalid NCName character is reached
            int i = offset;
            while (i < s.Length)
            {
                if (DCXmlCharType.IsNCNameSingleChar(s[i]))
                {
                    i++;
                }
                else
                {
                    break;
                }
            }

            return i - offset;
        }

        ////-----------------------------------------------
        //// Nmtoken parsing (no XML namespaces support)
        ////-----------------------------------------------
        ///// <summary>
        ///// Attempts to parse the input string as an Nmtoken (see the XML spec production [7]) without taking
        ///// into account the XML Namespaces spec. What it means is that the ':' character is allowed at any
        ///// position and any number of times in the token.
        ///// Quits parsing when an invalid Nmtoken char is reached or the end of string is reached.
        ///// Returns the number of valid Nmtoken chars that were parsed.
        ///// </summary>
        //internal static int ParseNmtokenNoNamespaces(string s, int offset)
        //{

        //    // Keep parsing until the end of string or an invalid Name character is reached
        //    int i = offset;
        //    while (i < s.Length)
        //    {
        //        if (XmlCharType.IsNameSingleChar(s[i]) || s[i] == ':')
        //        {
        //            i++;
        //        }
        //        else
        //        {
        //            break;
        //        }
        //    }

        //    return i - offset;
        //}

        //// helper methods
        //internal static bool IsNmtokenNoNamespaces(string s)
        //{
        //    int endPos = ParseNmtokenNoNamespaces(s, 0);
        //    return endPos > 0 && endPos == s.Length;
        //}

        //-----------------------------------------------
        // Name parsing (no XML namespaces support)
        //-----------------------------------------------
        /// <summary>
        /// Attempts to parse the input string as a Name without taking into account the XML Namespaces spec.
        /// What it means is that the ':' character does not delimiter prefix and local name, but it is a regular
        /// name character, which is allowed to appear at any position and any number of times in the name.
        /// Quits parsing when an invalid Name char is reached or the end of string is reached.
        /// Returns the number of valid Name chars that were parsed.
        /// </summary>
        internal static int ParseNameNoNamespaces(string s, int offset)
        {

            // Quit if the first character is not a valid NCName starting character
            int i = offset;
            if (i < s.Length)
            {
                if (DCXmlCharType.IsStartNCNameSingleChar(s[i]) || s[i] == ':')
                {
                    i++;
                }
                else
                {
                    return 0; // no valid StartNCName char
                }

                // Keep parsing until the end of string or an invalid NCName character is reached
                while (i < s.Length)
                {
                    if (DCXmlCharType.IsNCNameSingleChar(s[i]) || s[i] == ':')
                    {
                        i++;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            return i - offset;
        }

        // helper methods
        internal static bool IsNameNoNamespaces(string s)
        {
            int endPos = ParseNameNoNamespaces(s, 0);
            return endPos > 0 && endPos == s.Length;
        }

        //-----------------------------------------------
        // NCName parsing
        //-----------------------------------------------

        /// <summary>
        /// Attempts to parse the input string as an NCName (see the XML Namespace spec).
        /// Quits parsing when an invalid NCName char is reached or the end of string is reached.
        /// Returns the number of valid NCName chars that were parsed.
        /// </summary>
        internal static int ParseNCName(string s, int offset)
        {

            // Quit if the first character is not a valid NCName starting character
            int i = offset;
            if (i < s.Length)
            {
                if (DCXmlCharType.IsStartNCNameSingleChar(s[i]))
                {
                    i++;
                }
                else
                {
                    return 0; // no valid StartNCName char
                }

                // Keep parsing until the end of string or an invalid NCName character is reached
                while (i < s.Length)
                {
                    if (DCXmlCharType.IsNCNameSingleChar(s[i]))
                    {
                        i++;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            return i - offset;
        }

        internal static int ParseNCName(string s)
        {
            return ParseNCName(s, 0);
        }

        ///// <summary>
        ///// Calls parseName and throws exception if the resulting name is not a valid NCName.
        ///// Returns the input string if there is no error.
        ///// </summary>
        //internal static string ParseNCNameThrow(string s)
        //{
        //    // throwOnError = true
        //    ParseNCNameInternal(s, true);
        //    return s;
        //}

        ///// <summary>
        ///// Calls parseName and returns false or throws exception if the resulting name is not
        ///// a valid NCName.  Returns the input string if there is no error.
        ///// </summary>
        //private static bool ParseNCNameInternal(string s, bool throwOnError)
        //{
        //    int len = ParseNCName(s, 0);

        //    if (len == 0 || len != s.Length)
        //    {
        //        // If the string is not a valid NCName, then throw or return false
        //        if (throwOnError) ThrowInvalidName(s, 0, len);
        //        return false;
        //    }

        //    return true;
        //}

        //-----------------------------------------------
        // QName parsing
        //-----------------------------------------------

        /// <summary>
        /// Attempts to parse the input string as a QName (see the XML Namespace spec).
        /// Quits parsing when an invalid QName char is reached or the end of string is reached.
        /// Returns the number of valid QName chars that were parsed.
        /// Sets colonOffset to the offset of a colon character if it exists, or 0 otherwise.
        /// </summary>
        //internal static int ParseQName(string s, int offset, out int colonOffset)
        //{
        //    int len, lenLocal;

        //    // Assume no colon
        //    colonOffset = 0;

        //    // Parse NCName (may be prefix, may be local name)
        //    len = ParseNCName(s, offset);
        //    if (len != 0)
        //    {
        //        // Non-empty NCName, so look for colon if there are any characters left
        //        offset += len;
        //        if (offset < s.Length && s[offset] == ':')
        //        {
        //            // First NCName was prefix, so look for local name part
        //            lenLocal = ParseNCName(s, offset + 1);
        //            if (lenLocal != 0)
        //            {
        //                // Local name part found, so increase total QName length (add 1 for colon)
        //                colonOffset = offset;
        //                len += lenLocal + 1;
        //            }
        //        }
        //    }

        //    return len;
        //}

        ///// <summary>
        ///// Calls parseQName and throws exception if the resulting name is not a valid QName.
        ///// Returns the colon offset in the name.
        ///// </summary>
        //internal static int ParseQNameThrow(string s)
        //{
        //    int colonOffset;
        //    int len = ParseQName(s, 0, out colonOffset);

        //    if (len == 0 || len != s.Length)
        //    {
        //        // If the string is not a valid QName, then throw
        //        ThrowInvalidName(s, 0, len);
        //    }

        //    return colonOffset;
        //}

        ///// <summary>
        ///// Calls parseQName and throws exception if the resulting name is not a valid QName.
        ///// Returns the prefix and local name parts.
        ///// </summary>
        //internal static void ParseQNameThrow(string s, out string prefix, out string localName)
        //{
        //    int colonOffset = ParseQNameThrow(s);
        //    if (colonOffset != 0)
        //    {
        //        prefix = s.Substring(0, colonOffset);
        //        localName = s.Substring(colonOffset + 1);
        //    }
        //    else
        //    {
        //        prefix = "";
        //        localName = s;
        //    }
        //}

        ///// <summary>
        ///// Parses the input string as a NameTest (see the XPath spec), returning the prefix and
        ///// local name parts.  Throws an exception if the given string is not a valid NameTest.
        ///// If the NameTest contains a star, null values for localName (case NCName':*'), or for
        ///// both localName and prefix (case '*') are returned.
        ///// </summary>
        //internal static void ParseNameTestThrow(string s, out string? prefix, out string? localName)
        //{
        //    int len, lenLocal, offset;

        //    if (s.Length != 0 && s[0] == '*')
        //    {
        //        // '*' as a NameTest
        //        prefix = localName = null;
        //        len = 1;
        //    }
        //    else
        //    {
        //        // Parse NCName (may be prefix, may be local name)
        //        len = ParseNCName(s, 0);
        //        if (len != 0)
        //        {
        //            // Non-empty NCName, so look for colon if there are any characters left
        //            localName = s.Substring(0, len);
        //            if (len < s.Length && s[len] == ':')
        //            {
        //                // First NCName was prefix, so look for local name part
        //                prefix = localName;
        //                offset = len + 1;
        //                if (offset < s.Length && s[offset] == '*')
        //                {
        //                    // '*' as a local name part, add 2 to len for colon and star
        //                    localName = null;
        //                    len += 2;
        //                }
        //                else
        //                {
        //                    lenLocal = ParseNCName(s, offset);
        //                    if (lenLocal != 0)
        //                    {
        //                        // Local name part found, so increase total NameTest length
        //                        localName = s.Substring(offset, lenLocal);
        //                        len += lenLocal + 1;
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                prefix = string.Empty;
        //            }
        //        }
        //        else
        //        {
        //            // Make the compiler happy
        //            prefix = localName = null;
        //        }
        //    }

        //    if (len == 0 || len != s.Length)
        //    {
        //        // If the string is not a valid NameTest, then throw
        //        ThrowInvalidName(s, 0, len);
        //    }
        //}

        ///// <summary>
        ///// Throws an invalid name exception.
        ///// </summary>
        ///// <param name="s">String that was parsed.</param>
        ///// <param name="offsetStartChar">Offset in string where parsing began.</param>
        ///// <param name="offsetBadChar">Offset in string where parsing failed.</param>
        //internal static void ThrowInvalidName(string s, int offsetStartChar, int offsetBadChar)
        //{
        //    // If the name is empty, throw an exception
        //    if (offsetStartChar >= s.Length)
        //        throw new XmlException("EmptyName");

        //   //Debug.Assert(offsetBadChar < s.Length);

        //    if (XmlCharType.IsNCNameSingleChar(s[offsetBadChar]) && !XmlCharType.IsStartNCNameSingleChar(s[offsetBadChar]))
        //    {
        //        // The error character is a valid name character, but is not a valid start name character
        //        throw new XmlException();
        //    }
        //    else
        //    {
        //        // The error character is an invalid name character
        //        throw new XmlException();
        //    }
        //}

        //internal static Exception GetInvalidNameException(string s, int offsetStartChar, int offsetBadChar)
        //{
        //    // If the name is empty, throw an exception
        //    if (offsetStartChar >= s.Length)
        //        return new XmlException();

        //   //Debug.Assert(offsetBadChar < s.Length);

        //    if (XmlCharType.IsNCNameSingleChar(s[offsetBadChar]) && !XmlCharType.IsStartNCNameSingleChar(s[offsetBadChar]))
        //    {
        //        // The error character is a valid name character, but is not a valid start name character
        //        return new XmlException();
        //    }
        //    else
        //    {
        //        // The error character is an invalid name character
        //        return new XmlException();
        //    }
        //}

        ///// <summary>
        ///// Returns true if "prefix" starts with the characters 'x', 'm', 'l' (case-insensitive).
        ///// </summary>
        //internal static bool StartsWithXml(string s)
        //{
        //    if (s.Length < 3)
        //        return false;

        //    if (s[0] != 'x' && s[0] != 'X')
        //        return false;

        //    if (s[1] != 'm' && s[1] != 'M')
        //        return false;

        //    if (s[2] != 'l' && s[2] != 'L')
        //        return false;

        //    return true;
        //}

        ///// <summary>
        ///// Returns true if "s" is a namespace that is reserved by Xml 1.0 or Namespace 1.0.
        ///// </summary>
        //internal static bool IsReservedNamespace(string s)
        //{
        //    return s.Equals(XmlReservedNs.NsXml) || s.Equals(XmlReservedNs.NsXmlNs);
        //}

        ///// <summary>
        ///// Throw if the specified name parts are not valid according to the rules of "nodeKind".  Check only rules that are
        ///// specified by the Flags.
        ///// NOTE: Namespaces should be passed using a prefix, ns pair.  "localName" is always string.Empty.
        ///// </summary>
        //internal static void ValidateNameThrow(string prefix, string localName, string ns, XPathNodeType nodeKind, Flags flags)
        //{
        //    // throwOnError = true
        //    ValidateNameInternal(prefix, localName, ns, nodeKind, flags, true);
        //}

        ///// <summary>
        ///// Return false if the specified name parts are not valid according to the rules of "nodeKind".  Check only rules that are
        ///// specified by the Flags.
        ///// NOTE: Namespaces should be passed using a prefix, ns pair.  "localName" is always string.Empty.
        ///// </summary>
        //internal static bool ValidateName(string prefix, string localName, string ns, XPathNodeType nodeKind, Flags flags)
        //{
        //    // throwOnError = false
        //    return ValidateNameInternal(prefix, localName, ns, nodeKind, flags, false);
        //}

        ///// <summary>
        ///// Return false or throw if the specified name parts are not valid according to the rules of "nodeKind".  Check only rules
        ///// that are specified by the Flags.
        ///// NOTE: Namespaces should be passed using a prefix, ns pair.  "localName" is always string.Empty.
        ///// </summary>
        //private static bool ValidateNameInternal(string prefix, string localName, string ns, XPathNodeType nodeKind, Flags flags, bool throwOnError)
        //{
        //   //Debug.Assert(prefix != null && localName != null && ns != null);

        //    if ((flags & Flags.NCNames) != 0)
        //    {
        //        // 1. Verify that each non-empty prefix and localName is a valid NCName
        //        if (prefix.Length != 0)
        //            if (!ParseNCNameInternal(prefix, throwOnError))
        //            {
        //                return false;
        //            }

        //        if (localName.Length != 0)
        //            if (!ParseNCNameInternal(localName, throwOnError))
        //            {
        //                return false;
        //            }
        //    }

        //    if ((flags & Flags.CheckLocalName) != 0)
        //    {
        //        // 2. Determine whether the local name is valid
        //        switch (nodeKind)
        //        {
        //            case XPathNodeType.Element:
        //                // Elements and attributes must have a non-empty local name
        //                if (localName.Length == 0)
        //                {
        //                    if (throwOnError) throw new XmlException();
        //                    return false;
        //                }
        //                break;

        //            case XPathNodeType.Attribute:
        //                // Attribute local name cannot be "xmlns" if namespace is empty
        //                if (ns.Length == 0 && localName.Equals("xmlns"))
        //                {
        //                    if (throwOnError) throw new XmlException();
        //                    return false;
        //                }
        //                goto case XPathNodeType.Element;

        //            case XPathNodeType.ProcessingInstruction:
        //                // PI's local-name must be non-empty and cannot be 'xml' (case-insensitive)
        //                if (localName.Length == 0 || (localName.Length == 3 && StartsWithXml(localName)))
        //                {
        //                    if (throwOnError) throw new XmlException();
        //                    return false;
        //                }
        //                break;

        //            default:
        //                // All other node types must have empty local-name
        //                if (localName.Length != 0)
        //                {
        //                    if (throwOnError) throw new XmlException();
        //                    return false;
        //                }
        //                break;
        //        }
        //    }

        //    if ((flags & Flags.CheckPrefixMapping) != 0)
        //    {
        //        // 3. Determine whether the prefix is valid
        //        switch (nodeKind)
        //        {
        //            case XPathNodeType.Element:
        //            case XPathNodeType.Attribute:
        //            case XPathNodeType.Namespace:
        //                if (ns.Length == 0)
        //                {
        //                    // If namespace is empty, then prefix must be empty
        //                    if (prefix.Length != 0)
        //                    {
        //                        if (throwOnError) throw new XmlException();
        //                        return false;
        //                    }
        //                }
        //                else
        //                {
        //                    // Don't allow empty attribute prefix since namespace is non-empty
        //                    if (prefix.Length == 0 && nodeKind == XPathNodeType.Attribute)
        //                    {
        //                        if (throwOnError) throw new XmlException();
        //                        return false;
        //                    }

        //                    if (prefix.Equals("xml"))
        //                    {
        //                        // xml prefix must be mapped to the xml namespace
        //                        if (!ns.Equals(XmlReservedNs.NsXml))
        //                        {
        //                            if (throwOnError) throw new XmlException();
        //                            return false;
        //                        }
        //                    }
        //                    else if (prefix.Equals("xmlns"))
        //                    {
        //                        // Prefix may never be 'xmlns'
        //                        if (throwOnError) throw new XmlException();
        //                        return false;
        //                    }
        //                    else if (IsReservedNamespace(ns))
        //                    {
        //                        // Don't allow non-reserved prefixes to map to xml or xmlns namespaces
        //                        if (throwOnError) throw new XmlException();
        //                        return false;
        //                    }
        //                }
        //                break;

        //            case XPathNodeType.ProcessingInstruction:
        //                // PI's prefix and namespace must be empty
        //                if (prefix.Length != 0 || ns.Length != 0)
        //                {
        //                    if (throwOnError) throw new XmlException();
        //                    return false;
        //                }
        //                break;

        //            default:
        //                // All other node types must have empty prefix and namespace
        //                if (prefix.Length != 0 || ns.Length != 0)
        //                {
        //                    if (throwOnError) throw new XmlException();
        //                    return false;
        //                }
        //                break;
        //        }
        //    }

        //    return true;
        //}

        ///// <summary>
        ///// Creates a colon-delimited qname from prefix and local name parts.
        ///// </summary>
        //private static string CreateName(string prefix, string localName)
        //{
        //    return (prefix.Length != 0) ? $"{prefix}:{localName}" : localName;
        //}

        ///// <summary>
        ///// Split a QualifiedName into prefix and localname, w/o any checking.
        ///// (Used for XmlReader/XPathNavigator MoveTo(name) methods)
        ///// </summary>
        //internal static void SplitQName(string name, out string prefix, out string lname)
        //{
        //    int colonPos = name.IndexOf(':');
        //    if (-1 == colonPos)
        //    {
        //        prefix = string.Empty;
        //        lname = name;
        //    }
        //    else if (0 == colonPos || (name.Length - 1) == colonPos)
        //    {
        //        throw new ArgumentException("BadNameChar:" + XmlException.BuildCharExceptionArgs(':', '\0'));
        //    }
        //    else
        //    {
        //        prefix = name.Substring(0, colonPos);
        //        colonPos++; // move after colon
        //        lname = name.Substring(colonPos, name.Length - colonPos);
        //    }
        //}
    }

}
