using System;

namespace DCSoft.TemperatureChart
{
    public class TemperatureDocumentWriter
    {
        
        private System.Xml.XmlWriter _BaseWriter = null;
        public TemperatureDocumentWriter()
        {
            System.Xml.XmlWriterSettings settings = new System.Xml.XmlWriterSettings();
            settings.Indent = true;
            settings.Encoding = System.Text.Encoding.UTF8;
            _BaseWriter = System.Xml.XmlTextWriter.Create(new System.IO.StringWriter(), settings);
        }
        public TemperatureDocumentWriter(System.IO.TextWriter w)
        {
            System.Xml.XmlWriterSettings settings = new System.Xml.XmlWriterSettings();
            settings.Indent = true;
            settings.Encoding = System.Text.Encoding.UTF8;
            _BaseWriter = System.Xml.XmlTextWriter.Create(w, settings);
        }
        public TemperatureDocumentWriter(string filename)
        {
            System.Xml.XmlWriterSettings settings = new System.Xml.XmlWriterSettings();
            settings.Indent = true;
            settings.Encoding = System.Text.Encoding.UTF8;
            _BaseWriter = System.Xml.XmlTextWriter.Create(filename, settings);
        }
        public TemperatureDocumentWriter(System.IO.Stream stream)
        {
            System.Xml.XmlWriterSettings settings = new System.Xml.XmlWriterSettings();
            settings.Indent = true;
            settings.Encoding = System.Text.Encoding.UTF8;
            _BaseWriter = System.Xml.XmlTextWriter.Create(stream, settings);
        }
        public void Flush()
        {
            if (this._BaseWriter != null)
            {
                this._BaseWriter.Flush();
            }
            
        }
        public void Close()
        {
            if (this._BaseWriter != null)
            {
                this._BaseWriter.Close();
            }
            
        }
        private Exception CreateUnknownTypeException(object o)
        {
            Type t = o.GetType();
            string s = "未知的类型名称：" + t.FullName;
            Exception e = new Exception(s);
            return e;
        }
        private void MyWriteElementString(string localName, string value)
        {

            if (value != null && value.Length > 0)
            {
                this._BaseWriter.WriteElementString(localName, null, value);
            }
        }
        private void MyWriteElementStringRaw(string localName, string Value)
        {
            if (Value != null && Value.Length > 0)
            {
                this._BaseWriter.WriteStartElement(localName, null);
                this._BaseWriter.WriteRaw(Value);
                this._BaseWriter.WriteEndElement();
            }
        }
        private void MyWriteStartElement(string name, string ns, object o, bool writePrefixed, string xmlns)
        {
            this._BaseWriter.WriteStartElement(name);
        }
        private void MyWriteEndElement(object o)
        {
            this._BaseWriter.WriteEndElement();
        }
        private void MyWriteAttribute(string localName, string ns, string value)
        {
            this._BaseWriter.WriteAttributeString(localName, value);
        }
        private void WriteValue(string value)
        {
            this._BaseWriter.WriteString(value);
        }
        private string FromChar(char c)
        {
            int ii = (int)c;
            return ii.ToString();
        }
        private string FromDateTime(DateTime dt)
        {
            return dt.ToString("o");
        }
        private void MyWriteXsiType(string x, string y)
        {

        }
        internal static System.Exception CreateInvalidEnumValueException(long v, System.Type t)
        {
            return new System.InvalidOperationException(t.FullName + "不含数值" + v);
        }

        string Write119_StringAlignment(global::System.Drawing.StringAlignment v)
        {
            string s = null;
            switch (v)
            {
                case global::System.Drawing.StringAlignment.@Near: s = @"Near"; break;
                case global::System.Drawing.StringAlignment.@Center: s = @"Center"; break;
                case global::System.Drawing.StringAlignment.@Far: s = @"Far"; break;
                default: throw CreateInvalidEnumValueException((long)v, typeof(System.Drawing.StringAlignment));
            }
            return s;
        }
        string Write43_DashStyle(global::System.Drawing.Drawing2D.DashStyle v)
        {
            string s = null;
            switch (v)
            {
                case global::System.Drawing.Drawing2D.DashStyle.@Solid: s = @"Solid"; break;
                case global::System.Drawing.Drawing2D.DashStyle.@Dash: s = @"Dash"; break;
                case global::System.Drawing.Drawing2D.DashStyle.@Dot: s = @"Dot"; break;
                case global::System.Drawing.Drawing2D.DashStyle.@DashDot: s = @"DashDot"; break;
                case global::System.Drawing.Drawing2D.DashStyle.@DashDotDot: s = @"DashDotDot"; break;
                case global::System.Drawing.Drawing2D.DashStyle.@Custom: s = @"Custom"; break;
                default: throw CreateInvalidEnumValueException((long)v, typeof(System.Drawing.Drawing2D.DashStyle));
            }
            return s;
        }
        internal protected void Write192_XPenStyle(string n, string ns, global::DCSoft.Drawing.XPenStyle o, bool isNullable, bool needType)
        {
            
            var localWriter = this._BaseWriter;
            if (o == null)
            {
                //if (isNullable) WriteNullTagLiteral(n, ns);
                return;
            }
            if (!needType)
            {
                System.Type t = o.GetType();
                if (t == typeof(global::DCSoft.Drawing.XPenStyle))
                {
                }
                else
                {
                    throw CreateUnknownTypeException(o);
                }
            }
            MyWriteStartElement(n, ns, o, false, null);
            if (needType) MyWriteXsiType(@"XPenStyle", string.Empty);
            MyWriteElementString(@"Color", (o.@ColorString));
            if ((o.@Width) != 1f)
            {
                MyWriteElementStringRaw(@"Width", DCXMLConvert.ToString((o.@Width)));
            }
            if ((o.@DashStyle) != global::System.Drawing.Drawing2D.DashStyle.@Solid)
            {
                localWriter.WriteElementString(@"DashStyle", null, Write43_DashStyle((o.@DashStyle)));
            }
            if ((o.@DashCap) != global::System.Drawing.Drawing2D.DashCap.@Flat)
            {
                localWriter.WriteElementString(@"DashCap", null, Write188_DashCap((o.@DashCap)));
            }
            if ((o.@LineJoin) != global::System.Drawing.Drawing2D.LineJoin.@Bevel)
            {
                localWriter.WriteElementString(@"LineJoin", null, Write189_LineJoin((o.@LineJoin)));
            }
            if ((o.@StartCap) != global::System.Drawing.Drawing2D.LineCap.@Flat)
            {
                localWriter.WriteElementString(@"StartCap", null, Write190_LineCap((o.@StartCap)));
            }
            if ((o.@EndCap) != global::System.Drawing.Drawing2D.LineCap.@Flat)
            {
                localWriter.WriteElementString(@"EndCap", null, Write190_LineCap((o.@EndCap)));
            }
            if ((o.@MiterLimit) != 10f)
            {
                MyWriteElementStringRaw(@"MiterLimit", DCXMLConvert.ToString((o.@MiterLimit)));
            }
            if ((o.@Alignment) != global::System.Drawing.Drawing2D.PenAlignment.@Center)
            {
                localWriter.WriteElementString(@"Alignment", null, Write191_PenAlignment((o.@Alignment)));
            }
            MyWriteEndElement(o);
        }
        string Write188_DashCap(global::System.Drawing.Drawing2D.DashCap v)
        {
            string s = null;
            switch (v)
            {
                case global::System.Drawing.Drawing2D.DashCap.@Flat: s = @"Flat"; break;
                case global::System.Drawing.Drawing2D.DashCap.@Round: s = @"Round"; break;
                case global::System.Drawing.Drawing2D.DashCap.@Triangle: s = @"Triangle"; break;
                default: throw CreateInvalidEnumValueException((long)v, typeof(System.Drawing.Drawing2D.DashCap));
            }
            return s;
        }
        string Write190_LineCap(global::System.Drawing.Drawing2D.LineCap v)
        {
            string s = null;
            switch (v)
            {
                case global::System.Drawing.Drawing2D.LineCap.@Flat: s = @"Flat"; break;
                case global::System.Drawing.Drawing2D.LineCap.@Square: s = @"Square"; break;
                case global::System.Drawing.Drawing2D.LineCap.@Round: s = @"Round"; break;
                case global::System.Drawing.Drawing2D.LineCap.@Triangle: s = @"Triangle"; break;
                case global::System.Drawing.Drawing2D.LineCap.@NoAnchor: s = @"NoAnchor"; break;
                case global::System.Drawing.Drawing2D.LineCap.@SquareAnchor: s = @"SquareAnchor"; break;
                case global::System.Drawing.Drawing2D.LineCap.@RoundAnchor: s = @"RoundAnchor"; break;
                case global::System.Drawing.Drawing2D.LineCap.@DiamondAnchor: s = @"DiamondAnchor"; break;
                case global::System.Drawing.Drawing2D.LineCap.@ArrowAnchor: s = @"ArrowAnchor"; break;
                case global::System.Drawing.Drawing2D.LineCap.@Custom: s = @"Custom"; break;
                case global::System.Drawing.Drawing2D.LineCap.@AnchorMask: s = @"AnchorMask"; break;
                default: throw CreateInvalidEnumValueException((long)v, typeof(System.Drawing.Drawing2D.LineCap));
            }
            return s;
        }
        string Write189_LineJoin(global::System.Drawing.Drawing2D.LineJoin v)
        {
            string s = null;
            switch (v)
            {
                case global::System.Drawing.Drawing2D.LineJoin.@Miter: s = @"Miter"; break;
                case global::System.Drawing.Drawing2D.LineJoin.@Bevel: s = @"Bevel"; break;
                case global::System.Drawing.Drawing2D.LineJoin.@Round: s = @"Round"; break;
                case global::System.Drawing.Drawing2D.LineJoin.@MiterClipped: s = @"MiterClipped"; break;
                default: throw CreateInvalidEnumValueException((long)v, typeof(System.Drawing.Drawing2D.LineJoin));
            }
            return s;
        }
        string Write191_PenAlignment(global::System.Drawing.Drawing2D.PenAlignment v)
        {
            string s = null;
            switch (v)
            {
                case global::System.Drawing.Drawing2D.PenAlignment.@Center: s = @"Center"; break;
                case global::System.Drawing.Drawing2D.PenAlignment.@Inset: s = @"Inset"; break;
                case global::System.Drawing.Drawing2D.PenAlignment.@Outset: s = @"Outset"; break;
                case global::System.Drawing.Drawing2D.PenAlignment.@Left: s = @"Left"; break;
                case global::System.Drawing.Drawing2D.PenAlignment.@Right: s = @"Right"; break;
                default: throw CreateInvalidEnumValueException((long)v, typeof(System.Drawing.Drawing2D.PenAlignment));
            }
            return s;
        }
        internal protected void Write114_Color(string n, string ns, global::System.Drawing.Color o, bool needType)
        {
            
            var localWriter = this._BaseWriter;
            if (!needType)
            {
                System.Type t = o.GetType();
                if (t == typeof(global::System.Drawing.Color))
                {
                }
                else
                {
                    throw CreateUnknownTypeException(o);
                }
            }
            MyWriteStartElement(n, ns, o, false, null);
            if (needType) MyWriteXsiType(@"Color", string.Empty);
            MyWriteEndElement(o);
        }
        string Write60_GraphicsUnit(global::System.Drawing.GraphicsUnit v)
        {
            string s = null;
            switch (v)
            {
                case global::System.Drawing.GraphicsUnit.@World: s = @"World"; break;
                case global::System.Drawing.GraphicsUnit.@Display: s = @"Display"; break;
                case global::System.Drawing.GraphicsUnit.@Pixel: s = @"Pixel"; break;
                case global::System.Drawing.GraphicsUnit.@Point: s = @"Point"; break;
                case global::System.Drawing.GraphicsUnit.@Inch: s = @"Inch"; break;
                case global::System.Drawing.GraphicsUnit.@Document: s = @"Document"; break;
                case global::System.Drawing.GraphicsUnit.@Millimeter: s = @"Millimeter"; break;
                default: throw CreateInvalidEnumValueException((long)v, typeof(System.Drawing.GraphicsUnit));
            }
            return s;
        }
        internal protected void Write34_XImageValue(string n, string ns, global::DCSoft.Drawing.XImageValue o, bool isNullable, bool needType)
        {
            
            var localWriter = this._BaseWriter;
            if (o == null)
            {
                //if (isNullable) WriteNullTagLiteral(n, ns);
                return;
            }
            if (!needType)
            {
                System.Type t = o.GetType();
                if (t == typeof(global::DCSoft.Drawing.XImageValue))
                {
                }
                else
                {
                    throw CreateUnknownTypeException(o);
                }
            }
            MyWriteStartElement(n, ns, o, false, null);
            if (needType) MyWriteXsiType(@"XImageValue", string.Empty);
            if ((o.@HorizontalResolution) != 0f)
            {
                MyWriteElementStringRaw(@"HorizontalResolution", DCXMLConvert.ToString((o.@HorizontalResolution)));
            }
            if ((o.@VerticalResolution) != 0f)
            {
                MyWriteElementStringRaw(@"VerticalResolution", DCXMLConvert.ToString((o.@VerticalResolution)));
            }
            MyWriteElementString(@"ImageDataBase64String", (o.@ImageDataBase64String));
            MyWriteEndElement(o);
        }
        internal protected void Write64_XFontValue(string n, string ns, global::DCSoft.Drawing.XFontValue o, bool isNullable, bool needType)
        {
            
            var localWriter = this._BaseWriter;
            if (o == null)
            {
                //if (isNullable) WriteNullTagLiteral(n, ns);
                return;
            }
            if (!needType)
            {
                System.Type t = o.GetType();
                if (t == typeof(global::DCSoft.Drawing.XFontValue))
                {
                }
                else
                {
                    throw CreateUnknownTypeException(o);
                }
            }
            MyWriteStartElement(n, ns, o, false, null);
            if (needType) MyWriteXsiType(@"XFontValue", string.Empty);
            if ((o.@Name) != @"宋体")
            {
                MyWriteElementString(@"Name", (o.@Name));
            }
            if ((o.@Size) != 9f)
            {
                MyWriteElementStringRaw(@"Size", DCXMLConvert.ToString((o.@Size)));
            }
            if ((o.@Unit) != global::System.Drawing.GraphicsUnit.@Point)
            {
                localWriter.WriteElementString(@"Unit", null, Write60_GraphicsUnit((o.@Unit)));
            }
            if ((o.@Bold) != false)
            {
                MyWriteElementStringRaw(@"Bold", DCXMLConvert.ToString((o.@Bold)));
            }
            if ((o.@Italic) != false)
            {
                MyWriteElementStringRaw(@"Italic", DCXMLConvert.ToString((o.@Italic)));
            }
            if ((o.@Underline) != false)
            {
                MyWriteElementStringRaw(@"Underline", DCXMLConvert.ToString((o.@Underline)));
            }
            if ((o.@Strikeout) != false)
            {
                MyWriteElementStringRaw(@"Strikeout", DCXMLConvert.ToString((o.@Strikeout)));
            }
            MyWriteEndElement(o);
        }
        public void Write_TemperatureDocument(object o)
        {
            
            var localWriter = this._BaseWriter;
            this._BaseWriter.WriteStartDocument();
            //WriteStartDocument();
            //if (o == null)
            //{
            //    WriteNullTagLiteral(@"TemperatureDocument", string.Empty);
            //    return;
            //}
            //TopLevelElement();
            Write243_TemperatureDocument(@"TemperatureDocument", string.Empty, ((global::DCSoft.TemperatureChart.TemperatureDocument)o), true, false);

            this._BaseWriter.WriteEndDocument();
        }
        internal protected void Write243_TemperatureDocument(string n, string ns, global::DCSoft.TemperatureChart.TemperatureDocument o, bool isNullable, bool needType)
        {
            
            var localWriter = this._BaseWriter;
            if (o == null)
            {
                //if (isNullable) WriteNullTagLiteral(n, ns);
                return;
            }
            if (!needType)
            {
                System.Type t = o.GetType();
                if (t == typeof(global::DCSoft.TemperatureChart.TemperatureDocument))
                {
                }
                else
                {
                    throw CreateUnknownTypeException(o);
                }
            }
            MyWriteStartElement(n, ns, o, false, null);
            if (needType) MyWriteXsiType(@"TemperatureDocument", string.Empty);
            Write237_TemperatureDocumentConfig(@"Config", string.Empty, (o.@Config), false, false);
            {
                global::DCSoft.TemperatureChart.DCTimeLineParameterList a = (global::DCSoft.TemperatureChart.DCTimeLineParameterList)(o.@Parameters);
                if (a != null && a.Count > 0)
                {
                    var aCount = a.Count;//2222222
                    localWriter.WriteStartElement(null, @"Parameters", null);
                    for (int ia = 0; ia < aCount; ia++)
                    {
                        Write238_DCTimeLineParameter(@"Parameter", string.Empty, (a[ia]), true, false);
                    }
                    localWriter.WriteEndElement();
                }
            }
            {
                global::DCSoft.TemperatureChart.DocumentDataList a = (global::DCSoft.TemperatureChart.DocumentDataList)(o.@Datas);
                if (a != null && a.Count > 0)
                {
                    var aCount = a.Count;//2222222
                    localWriter.WriteStartElement(null, @"Datas", null);
                    for (int ia = 0; ia < aCount; ia++)
                    {
                        Write242_DocumentData(@"Data", string.Empty, (a[ia]), true, false);
                    }
                    localWriter.WriteEndElement();
                }
            }
            MyWriteEndElement(o);
        }
        internal protected void Write242_DocumentData(string n, string ns, global::DCSoft.TemperatureChart.DocumentData o, bool isNullable, bool needType)
        {
            
            var localWriter = this._BaseWriter;
            if (o == null)
            {
                //if (isNullable) WriteNullTagLiteral(n, ns);
                return;
            }
            if (!needType)
            {
                System.Type t = o.GetType();
                if (t == typeof(global::DCSoft.TemperatureChart.DocumentData))
                {
                }
                else
                {
                    throw CreateUnknownTypeException(o);
                }
            }
            MyWriteStartElement(n, ns, o, false, null);
            if (needType) MyWriteXsiType(@"DocumentData", string.Empty);
            MyWriteAttribute(@"Name", string.Empty, (o.@Name));
            {
                global::DCSoft.TemperatureChart.ValuePointList a = (global::DCSoft.TemperatureChart.ValuePointList)(o.@Values);
                if (a != null && a.Count > 0)
                {
                    var aCount = a.Count;//2222222
                    localWriter.WriteStartElement(null, @"Values", null);
                    for (int ia = 0; ia < aCount; ia++)
                    {
                        Write241_ValuePoint(@"Value", string.Empty, (a[ia]), true, false);
                    }
                    localWriter.WriteEndElement();
                }
            }
            MyWriteEndElement(o);
        }
        internal protected void Write241_ValuePoint(string n, string ns, global::DCSoft.TemperatureChart.ValuePoint o, bool isNullable, bool needType)
        {
            
            var localWriter = this._BaseWriter;
            if (o == null)
            {
                //if (isNullable) WriteNullTagLiteral(n, ns);
                return;
            }
            if (!needType)
            {
                System.Type t = o.GetType();
                if (t == typeof(global::DCSoft.TemperatureChart.ValuePoint))
                {
                }
                else
                {
                    throw CreateUnknownTypeException(o);
                }
            }
            MyWriteStartElement(n, ns, o, false, null);
            if (needType) MyWriteXsiType(@"ValuePoint", string.Empty);
            if ((o.@VerifiedColorValue) != @"black")
            {
                MyWriteAttribute(@"VerifiedColorValue", string.Empty, (o.@VerifiedColorValue));
            }
            if ((o.@VerifiedAlignment) != global::System.Drawing.StringAlignment.@Center)
            {
                localWriter.WriteAttributeString(@"VerifiedAlignment", null, Write119_StringAlignment((o.@VerifiedAlignment)));
            }
            MyWriteAttribute(@"TagValue", string.Empty, (o.@TagValue));
            MyWriteAttribute(@"ID", string.Empty, (o.@ID));
            if ((o.@Superscript) != false)
            {
                localWriter.WriteAttributeString(@"Superscript", null, DCXMLConvert.ToString((o.@Superscript)));
            }
            if ((o.@SpecifySymbolStyle) != global::DCSoft.TemperatureChart.ValuePointSymbolStyle.@Default)
            {
                localWriter.WriteAttributeString(@"SpecifySymbolStyle", null, Write235_ValuePointSymbolStyle((o.@SpecifySymbolStyle)));
            }
            if ((o.@SymbolOffsetX) != 0f)
            {
                localWriter.WriteAttributeString(@"SymbolOffsetX", null, DCXMLConvert.ToString((o.@SymbolOffsetX)));
            }
            if ((o.@SymbolOffsetY) != 0f)
            {
                localWriter.WriteAttributeString(@"SymbolOffsetY", null, DCXMLConvert.ToString((o.@SymbolOffsetY)));
            }
            if ((o.@SpecifyLanternSymbolStyle) != global::DCSoft.TemperatureChart.ValuePointSymbolStyle.@HollowCicle)
            {
                localWriter.WriteAttributeString(@"SpecifyLanternSymbolStyle", null, Write235_ValuePointSymbolStyle((o.@SpecifyLanternSymbolStyle)));
            }
            if ((o.@IntCharLantern) != 82)
            {
                localWriter.WriteAttributeString(@"IntCharLantern", null, DCXMLConvert.ToString((o.@IntCharLantern)));
            }
            if ((o.@IntCharSymbol) != 82)
            {
                localWriter.WriteAttributeString(@"IntCharSymbol", null, DCXMLConvert.ToString((o.@IntCharSymbol)));
            }
            MyWriteAttribute(@"Link", string.Empty, (o.@Link));
            MyWriteAttribute(@"LinkTarget", string.Empty, (o.@LinkTarget));
            MyWriteAttribute(@"Title", string.Empty, (o.@Title));
            if ((o.@VerticalLine) != global::DCSoft.TemperatureChart.DCTimeLineBooleanValue.@Inherit)
            {
                localWriter.WriteAttributeString(@"VerticalLine", null, Write239_DCTimeLineBooleanValue((o.@VerticalLine)));
            }
            if ((o.@UseAdvVerticalStyle) != false)
            {
                localWriter.WriteAttributeString(@"UseAdvVerticalStyle", null, DCXMLConvert.ToString((o.@UseAdvVerticalStyle)));
            }
            if ((o.@UseAdvVerticalStyle2) != false)
            {
                localWriter.WriteAttributeString(@"UseAdvVerticalStyle2", null, DCXMLConvert.ToString((o.@UseAdvVerticalStyle2)));
            }
            localWriter.WriteAttributeString(@"Time", null, FromDateTime((o.@Time)));
            if ((o.@EndTime).Ticks != (0))
            {
                localWriter.WriteAttributeString(@"EndTime", null, FromDateTime((o.@EndTime)));
            }
            if ((o.@Value) != -10000f)
            {
                localWriter.WriteAttributeString(@"Value", null, DCXMLConvert.ToString((o.@Value)));
            }
            if ((o.@LanternValue) != -10000f)
            {
                localWriter.WriteAttributeString(@"LanternValue", null, DCXMLConvert.ToString((o.@LanternValue)));
            }
            MyWriteAttribute(@"Text", string.Empty, (o.@Text));
            if ((o.@TextAlign) != global::System.Drawing.StringAlignment.@Center)
            {
                localWriter.WriteAttributeString(@"TextAlign", null, Write119_StringAlignment((o.@TextAlign)));
            }
            MyWriteAttribute(@"ColorValue", string.Empty, (o.@ColorValue));
            if ((o.@TextColorValue) != @"#00000000")
            {
                MyWriteAttribute(@"TextColorValue", string.Empty, (o.@TextColorValue));
            }
            if ((o.@Verified) != false)
            {
                MyWriteElementStringRaw(@"Verified", DCXMLConvert.ToString((o.@Verified)));
            }
            Write114_Color(@"VerifiedColor", string.Empty, (o.@VerifiedColor), false);
            if ((o.@ValueTextTopPadding) != 0f)
            {
                MyWriteElementStringRaw(@"ValueTextTopPadding", DCXMLConvert.ToString((o.@ValueTextTopPadding)));
            }
            Write64_XFontValue(@"Font", string.Empty, (o.@Font), false, false);
            Write34_XImageValue(@"Image", string.Empty, (o.@Image), false, false);
            Write34_XImageValue(@"CustomImage", string.Empty, (o.@CustomImage), false, false);
            if ((o.@UpAndDown) != global::DCSoft.TemperatureChart.ValuePointUpAndDown.@None)
            {
                localWriter.WriteElementString(@"UpAndDown", null, Write240_ValuePointUpAndDown((o.@UpAndDown)));
            }
            MyWriteEndElement(o);
        }
        string Write240_ValuePointUpAndDown(global::DCSoft.TemperatureChart.ValuePointUpAndDown v)
        {
            string s = null;
            switch (v)
            {
                case global::DCSoft.TemperatureChart.ValuePointUpAndDown.@None: s = @"None"; break;
                case global::DCSoft.TemperatureChart.ValuePointUpAndDown.@Up: s = @"Up"; break;
                case global::DCSoft.TemperatureChart.ValuePointUpAndDown.@Down: s = @"Down"; break;
                default: throw CreateInvalidEnumValueException((long)v, typeof(DCSoft.TemperatureChart.ValuePointUpAndDown));
            }
            return s;
        }
        string Write239_DCTimeLineBooleanValue(global::DCSoft.TemperatureChart.DCTimeLineBooleanValue v)
        {
            string s = null;
            switch (v)
            {
                case global::DCSoft.TemperatureChart.DCTimeLineBooleanValue.@Inherit: s = @"Inherit"; break;
                case global::DCSoft.TemperatureChart.DCTimeLineBooleanValue.@True: s = @"True"; break;
                case global::DCSoft.TemperatureChart.DCTimeLineBooleanValue.@False: s = @"False"; break;
                default: throw CreateInvalidEnumValueException((long)v, typeof(DCSoft.TemperatureChart.DCTimeLineBooleanValue));
            }
            return s;
        }
        string Write235_ValuePointSymbolStyle(global::DCSoft.TemperatureChart.ValuePointSymbolStyle v)
        {
            string s = null;
            switch (v)
            {
                case global::DCSoft.TemperatureChart.ValuePointSymbolStyle.@None: s = @"None"; break;
                case global::DCSoft.TemperatureChart.ValuePointSymbolStyle.@Default: s = @"Default"; break;
                case global::DCSoft.TemperatureChart.ValuePointSymbolStyle.@SolidCicle: s = @"SolidCicle"; break;
                case global::DCSoft.TemperatureChart.ValuePointSymbolStyle.@HollowCicle: s = @"HollowCicle"; break;
                case global::DCSoft.TemperatureChart.ValuePointSymbolStyle.@OpaqueHollowCicle: s = @"OpaqueHollowCicle"; break;
                case global::DCSoft.TemperatureChart.ValuePointSymbolStyle.@Cross: s = @"Cross"; break;
                case global::DCSoft.TemperatureChart.ValuePointSymbolStyle.@Square: s = @"Square"; break;
                case global::DCSoft.TemperatureChart.ValuePointSymbolStyle.@HollowSquare: s = @"HollowSquare"; break;
                case global::DCSoft.TemperatureChart.ValuePointSymbolStyle.@Diamond: s = @"Diamond"; break;
                case global::DCSoft.TemperatureChart.ValuePointSymbolStyle.@HollowDiamond: s = @"HollowDiamond"; break;
                case global::DCSoft.TemperatureChart.ValuePointSymbolStyle.@V: s = @"V"; break;
                case global::DCSoft.TemperatureChart.ValuePointSymbolStyle.@VReversed: s = @"VReversed"; break;
                case global::DCSoft.TemperatureChart.ValuePointSymbolStyle.@SolidTriangle: s = @"SolidTriangle"; break;
                case global::DCSoft.TemperatureChart.ValuePointSymbolStyle.@SolidTriangleReversed: s = @"SolidTriangleReversed"; break;
                case global::DCSoft.TemperatureChart.ValuePointSymbolStyle.@HollowTriangle: s = @"HollowTriangle"; break;
                case global::DCSoft.TemperatureChart.ValuePointSymbolStyle.@HollowTriangleReversed: s = @"HollowTriangleReversed"; break;
                case global::DCSoft.TemperatureChart.ValuePointSymbolStyle.@Character: s = @"Character"; break;
                case global::DCSoft.TemperatureChart.ValuePointSymbolStyle.@CharacterCircle: s = @"CharacterCircle"; break;
                case global::DCSoft.TemperatureChart.ValuePointSymbolStyle.@Custom: s = @"Custom"; break;
                default: throw CreateInvalidEnumValueException((long)v, typeof(DCSoft.TemperatureChart.ValuePointSymbolStyle));
            }
            return s;
        }
        internal protected void Write238_DCTimeLineParameter(string n, string ns, global::DCSoft.TemperatureChart.DCTimeLineParameter o, bool isNullable, bool needType)
        {
            
            var localWriter = this._BaseWriter;
            if (o == null)
            {
                //if (isNullable) WriteNullTagLiteral(n, ns);
                return;
            }
            if (!needType)
            {
                System.Type t = o.GetType();
                if (t == typeof(global::DCSoft.TemperatureChart.DCTimeLineParameter))
                {
                }
                else
                {
                    throw CreateUnknownTypeException(o);
                }
            }
            MyWriteStartElement(n, ns, o, false, null);
            if (needType) MyWriteXsiType(@"DCTimeLineParameter", string.Empty);
            MyWriteAttribute(@"Name", string.Empty, (o.@Name));
            if ((o.@Value) != null)
            {
                WriteValue((o.@Value));
            }
            MyWriteEndElement(o);
        }
        internal protected void Write237_TemperatureDocumentConfig(string n, string ns, global::DCSoft.TemperatureChart.TemperatureDocumentConfig o, bool isNullable, bool needType)
        {
            
            var localWriter = this._BaseWriter;
            if (o == null)
            {
                //if (isNullable) WriteNullTagLiteral(n, ns);
                return;
            }
            if (!needType)
            {
                System.Type t = o.GetType();
                if (t == typeof(global::DCSoft.TemperatureChart.TemperatureDocumentConfig))
                {
                }
                else
                {
                    throw CreateUnknownTypeException(o);
                }
            }
            MyWriteStartElement(n, ns, o, false, null);
            if (needType) MyWriteXsiType(@"TemperatureDocumentConfig", string.Empty);
            MyWriteAttribute(@"Version", string.Empty, (o.@Version));
            if ((o.@BothBlackWhenPrint) != false)
            {
                MyWriteElementStringRaw(@"BothBlackWhenPrint", DCXMLConvert.ToString((o.@BothBlackWhenPrint)));
            }
            if ((o.@LineWidthZoomRateWhenPrint) != 1f)
            {
                MyWriteElementStringRaw(@"LineWidthZoomRateWhenPrint", DCXMLConvert.ToString((o.@LineWidthZoomRateWhenPrint)));
            }
            if ((o.@LinkVisualStyle) != global::DCSoft.TemperatureChart.DocumentLinkVisualStyle.@Hover)
            {
                localWriter.WriteElementString(@"LinkVisualStyle", null, Write212_DocumentLinkVisualStyle((o.@LinkVisualStyle)));
            }
            if ((o.@DebugMode) != false)
            {
                MyWriteElementStringRaw(@"DebugMode", DCXMLConvert.ToString((o.@DebugMode)));
            }
            if ((o.@EditValuePointMode) != global::DCSoft.TemperatureChart.EditValuePointEventHandleMode.@None)
            {
                localWriter.WriteElementString(@"EditValuePointMode", null, Write213_EditValuePointEventHandleMode((o.@EditValuePointMode)));
            }
            if ((o.@EnableExtMouseMoveEvent) != false)
            {
                MyWriteElementStringRaw(@"EnableExtMouseMoveEvent", DCXMLConvert.ToString((o.@EnableExtMouseMoveEvent)));
            }
            if ((o.@EnableDataGridLinearAxisMode) != false)
            {
                MyWriteElementStringRaw(@"EnableDataGridLinearAxisMode", DCXMLConvert.ToString((o.@EnableDataGridLinearAxisMode)));
            }
            if ((o.@Readonly) != false)
            {
                MyWriteElementStringRaw(@"Readonly", DCXMLConvert.ToString((o.@Readonly)));
            }
            if ((o.@ExtendDaysForTimeLine) != 0f)
            {
                MyWriteElementStringRaw(@"ExtendDaysForTimeLine", DCXMLConvert.ToString((o.@ExtendDaysForTimeLine)));
            }
            MyWriteElementString(@"IllegalTextEndCharForLinux", (o.@IllegalTextEndCharForLinux));
            MyWriteElementString(@"TitleForToolTip", (o.@TitleForToolTip));
            if ((o.@EnableCustomValuePointSymbol) != false)
            {
                MyWriteElementStringRaw(@"EnableCustomValuePointSymbol", DCXMLConvert.ToString((o.@EnableCustomValuePointSymbol)));
            }
            if ((o.@HeaderLabelLineAlignment) != global::System.Drawing.StringAlignment.@Far)
            {
                localWriter.WriteElementString(@"HeaderLabelLineAlignment", null, Write119_StringAlignment((o.@HeaderLabelLineAlignment)));
            }
            if ((o.@SelectionMode) != global::DCSoft.TemperatureChart.DCTimeLineSelectionMode.@None)
            {
                localWriter.WriteElementString(@"SelectionMode", null, Write214_DCTimeLineSelectionMode((o.@SelectionMode)));
            }
            if ((o.@ShowTooltip) != true)
            {
                MyWriteElementStringRaw(@"ShowTooltip", DCXMLConvert.ToString((o.@ShowTooltip)));
            }
            if ((o.@AllowUserCollapseZone) != true)
            {
                MyWriteElementStringRaw(@"AllowUserCollapseZone", DCXMLConvert.ToString((o.@AllowUserCollapseZone)));
            }
            if ((o.@TickUnit) != global::DCSoft.TemperatureChart.DCTimeUnit.@Hour)
            {
                localWriter.WriteElementString(@"TickUnit", null, Write215_DCTimeUnit((o.@TickUnit)));
            }
            if ((o.@DataGridTopPadding) != 0f)
            {
                MyWriteElementStringRaw(@"DataGridTopPadding", DCXMLConvert.ToString((o.@DataGridTopPadding)));
            }
            if ((o.@DataGridBottomPadding) != 0f)
            {
                MyWriteElementStringRaw(@"DataGridBottomPadding", DCXMLConvert.ToString((o.@DataGridBottomPadding)));
            }
            MyWriteElementString(@"SQLTextForHeaderLabel", (o.@SQLTextForHeaderLabel));
            if ((o.@SpecifyTickWidth) != 0f)
            {
                MyWriteElementStringRaw(@"SpecifyTickWidth", DCXMLConvert.ToString((o.@SpecifyTickWidth)));
            }
            {
                global::DCSoft.TemperatureChart.DCTimeLineImageList a = (global::DCSoft.TemperatureChart.DCTimeLineImageList)(o.@Images);
                if (a != null && a.Count > 0)
                {
                    var aCount = a.Count;//2222222
                    localWriter.WriteStartElement(null, @"Images", null);
                    for (int ia = 0; ia < aCount; ia++)
                    {
                        Write216_DCTimeLineImage(@"Image", string.Empty, (a[ia]), true, false);
                    }
                    localWriter.WriteEndElement();
                }
            }
            {
                global::DCSoft.TemperatureChart.DCTimeLineLabelList a = (global::DCSoft.TemperatureChart.DCTimeLineLabelList)(o.@Labels);
                if (a != null && a.Count > 0)
                {
                    var aCount = a.Count;//2222222
                    localWriter.WriteStartElement(null, @"Labels", null);
                    for (int ia = 0; ia < aCount; ia++)
                    {
                        Write218_DCTimeLineLabel(@"Label", string.Empty, (a[ia]), true, false);
                    }
                    localWriter.WriteEndElement();
                }
            }
            MyWriteElementString(@"PageIndexText", (o.@PageIndexText));
            MyWriteElementString(@"SpecifyStartDate", (o.@SpecifyStartDate));
            MyWriteElementString(@"SpecifyEndDate", (o.@SpecifyEndDate));
            Write219_DocumentPageSettings(@"PageSettings", string.Empty, (o.@PageSettings), false, false);
            MyWriteElementString(@"FooterDescription", (o.@FooterDescription));
            if ((o.@ShowIcon) != false)
            {
                MyWriteElementStringRaw(@"ShowIcon", DCXMLConvert.ToString((o.@ShowIcon)));
            }
            if ((o.@ImagePixelWidth) != 16)
            {
                MyWriteElementStringRaw(@"ImagePixelWidth", DCXMLConvert.ToString((o.@ImagePixelWidth)));
            }
            if ((o.@ImagePixelHeight) != 16)
            {
                MyWriteElementStringRaw(@"ImagePixelHeight", DCXMLConvert.ToString((o.@ImagePixelHeight)));
            }
            if ((o.@ShadowPointDetectSeconds) != 2000)
            {
                MyWriteElementStringRaw(@"ShadowPointDetectSeconds", DCXMLConvert.ToString((o.@ShadowPointDetectSeconds)));
            }
            if ((o.@GridYSplitNum) != 8)
            {
                MyWriteElementStringRaw(@"GridYSplitNum", DCXMLConvert.ToString((o.@GridYSplitNum)));
            }
            Write221_GridYSplitInfo(@"GridYSplitInfo", string.Empty, (o.@GridYSplitInfo), false, false);
            {
                global::DCSoft.TemperatureChart.TimeLineZoneInfoList a = (global::DCSoft.TemperatureChart.TimeLineZoneInfoList)(o.@Zones);
                if (a != null && a.Count > 0)
                {
                    var aCount = a.Count;//2222222
                    localWriter.WriteStartElement(null, @"Zones", null);
                    for (int ia = 0; ia < aCount; ia++)
                    {
                        Write223_TimeLineZoneInfo(@"Zone", string.Empty, (a[ia]), true, false);
                    }
                    localWriter.WriteEndElement();
                }
            }
            {
                global::DCSoft.TemperatureChart.TickInfoList a = (global::DCSoft.TemperatureChart.TickInfoList)(o.@Ticks);
                if (a != null && a.Count > 0)
                {
                    var aCount = a.Count;//2222222
                    localWriter.WriteStartElement(null, @"Ticks", null);
                    for (int ia = 0; ia < aCount; ia++)
                    {
                        Write222_TickInfo(@"Tick", string.Empty, (a[ia]), true, false);
                    }
                    localWriter.WriteEndElement();
                }
            }
            if ((o.@SymbolSize) != 20f)
            {
                MyWriteElementStringRaw(@"SymbolSize", DCXMLConvert.ToString((o.@SymbolSize)));
            }
            MyWriteElementString(@"FontName", (o.@FontName));
            if ((o.@FontSize) != 9f)
            {
                MyWriteElementStringRaw(@"FontSize", DCXMLConvert.ToString((o.@FontSize)));
            }
            if ((o.@BigTitleFontSize) != 27f)
            {
                MyWriteElementStringRaw(@"BigTitleFontSize", DCXMLConvert.ToString((o.@BigTitleFontSize)));
            }
            Write64_XFontValue(@"PageIndexFont", string.Empty, (o.@PageIndexFont), false, false);
            MyWriteElementString(@"ForeColorValue", (o.@ForeColorValue));
            if ((o.@BigVerticalGridLineWidth) != 2f)
            {
                MyWriteElementStringRaw(@"BigVerticalGridLineWidth", DCXMLConvert.ToString((o.@BigVerticalGridLineWidth)));
            }
            MyWriteElementString(@"BigVerticalGridLineColorValue", (o.@BigVerticalGridLineColorValue));
            MyWriteElementString(@"BackColorValue", (o.@BackColorValue));
            MyWriteElementString(@"PageBackColorValue", (o.@PageBackColorValue));
            MyWriteElementString(@"GridLineColorValue", (o.@GridLineColorValue));
            MyWriteElementString(@"GridBackColorValue", (o.@GridBackColorValue));
            if ((o.@DateFormatString) != @"yyyy-MM-dd")
            {
                MyWriteElementString(@"DateFormatString", (o.@DateFormatString));
            }
            if ((o.@DateFormatStringForCrossYear) != @"yyyy-MM-dd")
            {
                MyWriteElementString(@"DateFormatStringForCrossYear", (o.@DateFormatStringForCrossYear));
            }
            if ((o.@DateFormatStringForCrossMonth) != @"MM-dd")
            {
                MyWriteElementString(@"DateFormatStringForCrossMonth", (o.@DateFormatStringForCrossMonth));
            }
            if ((o.@DateFormatStringForCrossWeek) != @"dd")
            {
                MyWriteElementString(@"DateFormatStringForCrossWeek", (o.@DateFormatStringForCrossWeek));
            }
            if ((o.@DateFormatStringForFirstIndexFirstPage) != @"yyyy-MM-dd")
            {
                MyWriteElementString(@"DateFormatStringForFirstIndexFirstPage", (o.@DateFormatStringForFirstIndexFirstPage));
            }
            if ((o.@DateFormatStringForFirstIndexOtherPage) != @"dd")
            {
                MyWriteElementString(@"DateFormatStringForFirstIndexOtherPage", (o.@DateFormatStringForFirstIndexOtherPage));
            }
            MyWriteElementString(@"Title", (o.@Title));
            if ((o.@SpecifyTitleHeight) != 0f)
            {
                MyWriteElementStringRaw(@"SpecifyTitleHeight", DCXMLConvert.ToString((o.@SpecifyTitleHeight)));
            }
            {
                global::DCSoft.TemperatureChart.HeaderLabelInfoList a = (global::DCSoft.TemperatureChart.HeaderLabelInfoList)(o.@HeaderLabels);
                if (a != null && a.Count > 0)
                {
                    var aCount = a.Count;//2222222
                    localWriter.WriteStartElement(null, @"HeaderLabels", null);
                    for (int ia = 0; ia < aCount; ia++)
                    {
                        Write224_HeaderLabelInfo(@"Label", string.Empty, (a[ia]), true, false);
                    }
                    localWriter.WriteEndElement();
                }
            }
            if ((o.@NumOfDaysInOnePage) != 7)
            {
                MyWriteElementStringRaw(@"NumOfDaysInOnePage", DCXMLConvert.ToString((o.@NumOfDaysInOnePage)));
            }
            {
                global::DCSoft.TemperatureChart.TitleLineInfoList a = (global::DCSoft.TemperatureChart.TitleLineInfoList)(o.@HeaderLines);
                if (a != null && a.Count > 0)
                {
                    var aCount = a.Count;//2222222
                    localWriter.WriteStartElement(null, @"HeaderLines", null);
                    for (int ia = 0; ia < aCount; ia++)
                    {
                        Write232_TitleLineInfo(@"Line", string.Empty, (a[ia]), true, false);
                    }
                    localWriter.WriteEndElement();
                }
            }
            {
                global::DCSoft.TemperatureChart.TitleLineInfoList a = (global::DCSoft.TemperatureChart.TitleLineInfoList)(o.@FooterLines);
                if (a != null && a.Count > 0)
                {
                    var aCount = a.Count;//2222222
                    localWriter.WriteStartElement(null, @"FooterLines", null);
                    for (int ia = 0; ia < aCount; ia++)
                    {
                        Write232_TitleLineInfo(@"Line", string.Empty, (a[ia]), true, false);
                    }
                    localWriter.WriteEndElement();
                }
            }
            {
                global::DCSoft.TemperatureChart.YAxisInfoList a = (global::DCSoft.TemperatureChart.YAxisInfoList)(o.@YAxisInfos);
                if (a != null && a.Count > 0)
                {
                    var aCount = a.Count;//2222222
                    localWriter.WriteStartElement(null, @"YAxisInfos", null);
                    for (int ia = 0; ia < aCount; ia++)
                    {
                        Write236_YAxisInfo(@"YAxis", string.Empty, (a[ia]), true, false);
                    }
                    localWriter.WriteEndElement();
                }
            }
            MyWriteEndElement(o);
        }
        internal protected void Write236_YAxisInfo(string n, string ns, global::DCSoft.TemperatureChart.YAxisInfo o, bool isNullable, bool needType)
        {
            
            var localWriter = this._BaseWriter;
            if (o == null)
            {
                //if (isNullable) WriteNullTagLiteral(n, ns);
                return;
            }
            if (!needType)
            {
                System.Type t = o.GetType();
                if (t == typeof(global::DCSoft.TemperatureChart.YAxisInfo))
                {
                }
                else
                {
                    throw CreateUnknownTypeException(o);
                }
            }
            MyWriteStartElement(n, ns, o, false, null);
            if (needType) MyWriteXsiType(@"YAxisInfo", string.Empty);
            if ((o.@MergeIntoLeft) != false)
            {
                localWriter.WriteAttributeString(@"MergeIntoLeft", null, DCXMLConvert.ToString((o.@MergeIntoLeft)));
            }
            MyWriteAttribute(@"Name", string.Empty, (o.@Name));
            if ((o.@HighlightOutofNormalRange) != true)
            {
                localWriter.WriteAttributeString(@"HighlightOutofNormalRange", null, DCXMLConvert.ToString((o.@HighlightOutofNormalRange)));
            }
            if ((o.@InputTimePrecision) != global::DCSoft.TemperatureChart.DateTimePrecisionMode.@Minute)
            {
                localWriter.WriteAttributeString(@"InputTimePrecision", null, Write225_DateTimePrecisionMode((o.@InputTimePrecision)));
            }
            if ((o.@ValuePrecision) != 2)
            {
                localWriter.WriteAttributeString(@"ValuePrecision", null, DCXMLConvert.ToString((o.@ValuePrecision)));
            }
            if ((o.@AllowInterrupt) != true)
            {
                localWriter.WriteAttributeString(@"AllowInterrupt", null, DCXMLConvert.ToString((o.@AllowInterrupt)));
            }
            if ((o.@LineWidth) != 1)
            {
                localWriter.WriteAttributeString(@"LineWidth", null, DCXMLConvert.ToString((o.@LineWidth)));
            }
            MyWriteAttribute(@"LanternValueColorForUpValue", string.Empty, (o.@LanternValueColorForUpValue));
            MyWriteAttribute(@"LanternValueColorForDownValue", string.Empty, (o.@LanternValueColorForDownValue));
            if ((o.@LineStyleForLanternValue) != global::System.Drawing.Drawing2D.DashStyle.@Dash)
            {
                localWriter.WriteAttributeString(@"LineStyleForLanternValue", null, Write43_DashStyle((o.@LineStyleForLanternValue)));
            }
            if ((o.@SymbolSize) != 20f)
            {
                localWriter.WriteAttributeString(@"SymbolSize", null, DCXMLConvert.ToString((o.@SymbolSize)));
            }
            if ((o.@SpecifyTitleWidth) != 0f)
            {
                localWriter.WriteAttributeString(@"SpecifyTitleWidth", null, DCXMLConvert.ToString((o.@SpecifyTitleWidth)));
            }
            if ((o.@AllowOutofRange) != false)
            {
                localWriter.WriteAttributeString(@"AllowOutofRange", null, DCXMLConvert.ToString((o.@AllowOutofRange)));
            }
            if ((o.@SeparatorLineVisible) != true)
            {
                localWriter.WriteAttributeString(@"SeparatorLineVisible", null, DCXMLConvert.ToString((o.@SeparatorLineVisible)));
            }
            if ((o.@ClickToHide) != true)
            {
                localWriter.WriteAttributeString(@"ClickToHide", null, DCXMLConvert.ToString((o.@ClickToHide)));
            }
            if ((o.@Visible) != true)
            {
                localWriter.WriteAttributeString(@"Visible", null, DCXMLConvert.ToString((o.@Visible)));
            }
            if ((o.@ValueVisible) != true)
            {
                localWriter.WriteAttributeString(@"ValueVisible", null, DCXMLConvert.ToString((o.@ValueVisible)));
            }
            if ((o.@EnableLanternValue) != false)
            {
                localWriter.WriteAttributeString(@"EnableLanternValue", null, DCXMLConvert.ToString((o.@EnableLanternValue)));
            }
            MyWriteAttribute(@"LanternValueTitle", string.Empty, (o.@LanternValueTitle));
            if ((o.@Style) != global::DCSoft.TemperatureChart.YAxisInfoStyle.@Value)
            {
                localWriter.WriteAttributeString(@"Style", null, Write233_YAxisInfoStyle((o.@Style)));
            }
            MyWriteAttribute(@"HollowCovertTargetName", string.Empty, (o.@HollowCovertTargetName));
            MyWriteAttribute(@"ShadowName", string.Empty, (o.@ShadowName));
            MyWriteAttribute(@"TitleValueDispalyFormat", string.Empty, (o.@TitleValueDispalyFormat));
            if ((o.@TitleVisible) != true)
            {
                localWriter.WriteAttributeString(@"TitleVisible", null, DCXMLConvert.ToString((o.@TitleVisible)));
            }
            MyWriteAttribute(@"Title", string.Empty, (o.@Title));
            if ((o.@YSplitNum) != 8)
            {
                localWriter.WriteAttributeString(@"YSplitNum", null, DCXMLConvert.ToString((o.@YSplitNum)));
            }
            MyWriteAttribute(@"ValueFormatString", string.Empty, (o.@ValueFormatString));
            MyWriteAttribute(@"AlertLineColorValue", string.Empty, (o.@AlertLineColorValue));
            if ((o.@RedLineValue) != -10000f)
            {
                localWriter.WriteAttributeString(@"RedLineValue", null, DCXMLConvert.ToString((o.@RedLineValue)));
            }
            if ((o.@RedLineWidth) != 1f)
            {
                localWriter.WriteAttributeString(@"RedLineWidth", null, DCXMLConvert.ToString((o.@RedLineWidth)));
            }
            MyWriteAttribute(@"ValueTextBackColorValue", string.Empty, (o.@ValueTextBackColorValue));
            if ((o.@MaxValue) != 100f)
            {
                localWriter.WriteAttributeString(@"MaxValue", null, DCXMLConvert.ToString((o.@MaxValue)));
            }
            if ((o.@MinValue) != 0f)
            {
                localWriter.WriteAttributeString(@"MinValue", null, DCXMLConvert.ToString((o.@MinValue)));
            }
            if ((o.@ShowLegendInRule) != true)
            {
                localWriter.WriteAttributeString(@"ShowLegendInRule", null, DCXMLConvert.ToString((o.@ShowLegendInRule)));
            }
            if ((o.@ShowPointValue) != false)
            {
                localWriter.WriteAttributeString(@"ShowPointValue", null, DCXMLConvert.ToString((o.@ShowPointValue)));
            }
            MyWriteAttribute(@"ColorValueForPointValue", string.Empty, (o.@ColorValueForPointValue));
            MyWriteAttribute(@"ColorValueForDownValue", string.Empty, (o.@ColorValueForDownValue));
            MyWriteAttribute(@"ColorValueForUpValue", string.Empty, (o.@ColorValueForUpValue));
            if ((o.@SymbolStyle) != global::DCSoft.TemperatureChart.ValuePointSymbolStyle.@SolidCicle)
            {
                localWriter.WriteAttributeString(@"SymbolStyle", null, Write235_ValuePointSymbolStyle((o.@SymbolStyle)));
            }
            if ((o.@SymbolOffsetX) != 0f)
            {
                localWriter.WriteAttributeString(@"SymbolOffsetX", null, DCXMLConvert.ToString((o.@SymbolOffsetX)));
            }
            if ((o.@SymbolOffsetY) != 0f)
            {
                localWriter.WriteAttributeString(@"SymbolOffsetY", null, DCXMLConvert.ToString((o.@SymbolOffsetY)));
            }
            if ((o.@IntCharSymbol) != 82)
            {
                localWriter.WriteAttributeString(@"IntCharSymbol", null, DCXMLConvert.ToString((o.@IntCharSymbol)));
            }
            MyWriteAttribute(@"BottomTitle", string.Empty, (o.@BottomTitle));
            MyWriteAttribute(@"TitleBackColorValue", string.Empty, (o.@TitleBackColorValue));
            MyWriteAttribute(@"HiddenValueTitleBackColorValue", string.Empty, (o.@HiddenValueTitleBackColorValue));
            MyWriteAttribute(@"TitleColorValue", string.Empty, (o.@TitleColorValue));
            if ((o.@SymbolColorValue) != @"Red")
            {
                MyWriteAttribute(@"SymbolColorValue", string.Empty, (o.@SymbolColorValue));
            }
            MyWriteAttribute(@"DataSourceName", string.Empty, (o.@DataSourceName));
            MyWriteAttribute(@"ValueFieldName", string.Empty, (o.@ValueFieldName));
            MyWriteAttribute(@"LanternValueFieldName", string.Empty, (o.@LanternValueFieldName));
            if ((o.@SpecifyLanternSymbolStyle) != global::DCSoft.TemperatureChart.ValuePointSymbolStyle.@HollowCicle)
            {
                localWriter.WriteAttributeString(@"SpecifyLanternSymbolStyle", null, Write235_ValuePointSymbolStyle((o.@SpecifyLanternSymbolStyle)));
            }
            if ((o.@IntCharLantern) != 82)
            {
                localWriter.WriteAttributeString(@"IntCharLantern", null, DCXMLConvert.ToString((o.@IntCharLantern)));
            }
            MyWriteAttribute(@"TimeFieldName", string.Empty, (o.@TimeFieldName));
            if ((o.@MaxTextDisplayLength) != 0f)
            {
                MyWriteElementStringRaw(@"MaxTextDisplayLength", DCXMLConvert.ToString((o.@MaxTextDisplayLength)));
            }
            if ((o.@TopPadding) != -10000f)
            {
                MyWriteElementStringRaw(@"TopPadding", DCXMLConvert.ToString((o.@TopPadding)));
            }
            if ((o.@BottomPadding) != -10000f)
            {
                MyWriteElementStringRaw(@"BottomPadding", DCXMLConvert.ToString((o.@BottomPadding)));
            }
            Write64_XFontValue(@"Font", string.Empty, (o.@Font), false, false);
            Write64_XFontValue(@"ValueFont", string.Empty, (o.@ValueFont), false, false);
            Write227_ValuePointDataSourceInfo(@"DataSource", string.Empty, (o.@DataSource), false, false);
            if ((o.@ShadowPointVisible) != true)
            {
                MyWriteElementStringRaw(@"ShadowPointVisible", DCXMLConvert.ToString((o.@ShadowPointVisible)));
            }
            if ((o.@VerticalLine) != false)
            {
                MyWriteElementStringRaw(@"VerticalLine", DCXMLConvert.ToString((o.@VerticalLine)));
            }
            if ((o.@RedLinePrintVisible) != true)
            {
                MyWriteElementStringRaw(@"RedLinePrintVisible", DCXMLConvert.ToString((o.@RedLinePrintVisible)));
            }
            Write234_AbNormalRangeSettings(@"AbNormalRangeSettings", string.Empty, (o.@AbNormalRangeSettings), false, false);
            {
                global::DCSoft.TemperatureChart.YAxisScaleInfoList a = (global::DCSoft.TemperatureChart.YAxisScaleInfoList)(o.@Scales);
                if (a != null && a.Count > 0)
                {
                    var aCount = a.Count;//2222222
                    localWriter.WriteStartElement(null, @"Scales", null);
                    for (int ia = 0; ia < aCount; ia++)
                    {
                        Write231_YAxisScaleInfo(@"Scale", string.Empty, (a[ia]), true, false);
                    }
                    localWriter.WriteEndElement();
                }
            }
            MyWriteEndElement(o);
        }
        internal protected void Write231_YAxisScaleInfo(string n, string ns, global::DCSoft.TemperatureChart.YAxisScaleInfo o, bool isNullable, bool needType)
        {
            
            var localWriter = this._BaseWriter;
            if (o == null)
            {
                //if (isNullable) WriteNullTagLiteral(n, ns);
                return;
            }
            if (!needType)
            {
                System.Type t = o.GetType();
                if (t == typeof(global::DCSoft.TemperatureChart.YAxisScaleInfo))
                {
                }
                else
                {
                    throw CreateUnknownTypeException(o);
                }
            }
            MyWriteStartElement(n, ns, o, false, null);
            if (needType) MyWriteXsiType(@"YAxisScaleInfo", string.Empty);
            MyWriteAttribute(@"Text", string.Empty, (o.@Text));
            if ((o.@Value) != 0)
            {
                localWriter.WriteAttributeString(@"Value", null, DCXMLConvert.ToString((o.@Value)));
            }
            if ((o.@ScaleRate) != 0f)
            {
                localWriter.WriteAttributeString(@"ScaleRate", null, DCXMLConvert.ToString((o.@ScaleRate)));
            }
            MyWriteAttribute(@"ColorValue", string.Empty, (o.@ColorValue));
            MyWriteEndElement(o);
        }
        internal protected void Write234_AbNormalRangeSettings(string n, string ns, global::DCSoft.TemperatureChart.AbNormalRangeSettings o, bool isNullable, bool needType)
        {
            
            var localWriter = this._BaseWriter;
            if (o == null)
            {
                //if (isNullable) WriteNullTagLiteral(n, ns);
                return;
            }
            if (!needType)
            {
                System.Type t = o.GetType();
                if (t == typeof(global::DCSoft.TemperatureChart.AbNormalRangeSettings))
                {
                }
                else
                {
                    throw CreateUnknownTypeException(o);
                }
            }
            MyWriteStartElement(n, ns, o, false, null);
            if (needType) MyWriteXsiType(@"AbNormalRangeSettings", string.Empty);
            MyWriteAttribute(@"NormalRangeBackColorValue", string.Empty, (o.@NormalRangeBackColorValue));
            MyWriteAttribute(@"OutofNormalRangeBackColorValue", string.Empty, (o.@OutofNormalRangeBackColorValue));
            if ((o.@NormalMaxValue) != -10000f)
            {
                localWriter.WriteAttributeString(@"NormalMaxValue", null, DCXMLConvert.ToString((o.@NormalMaxValue)));
            }
            if ((o.@NormalMinValue) != -10000f)
            {
                localWriter.WriteAttributeString(@"NormalMinValue", null, DCXMLConvert.ToString((o.@NormalMinValue)));
            }
            Write192_XPenStyle(@"NormalRangeUpLineStyle", string.Empty, (o.@NormalRangeUpLineStyle), false, false);
            Write192_XPenStyle(@"NormalRangeDownLineStyle", string.Empty, (o.@NormalRangeDownLineStyle), false, false);
            MyWriteEndElement(o);
        }
        internal protected void Write227_ValuePointDataSourceInfo(string n, string ns, global::DCSoft.TemperatureChart.ValuePointDataSourceInfo o, bool isNullable, bool needType)
        {
            
            var localWriter = this._BaseWriter;
            if (o == null)
            {
                //if (isNullable) WriteNullTagLiteral(n, ns);
                return;
            }
            if (!needType)
            {
                System.Type t = o.GetType();
                if (t == typeof(global::DCSoft.TemperatureChart.ValuePointDataSourceInfo))
                {
                }
                else
                {
                    throw CreateUnknownTypeException(o);
                }
            }
            MyWriteStartElement(n, ns, o, false, null);
            if (needType) MyWriteXsiType(@"ValuePointDataSourceInfo", string.Empty);
            MyWriteAttribute(@"FieldNameForID", string.Empty, (o.@FieldNameForID));
            MyWriteAttribute(@"FieldNameForLink", string.Empty, (o.@FieldNameForLink));
            MyWriteAttribute(@"FieldNameForTitle", string.Empty, (o.@FieldNameForTitle));
            MyWriteAttribute(@"FieldNameForTime", string.Empty, (o.@FieldNameForTime));
            MyWriteAttribute(@"FieldNameForValue", string.Empty, (o.@FieldNameForValue));
            MyWriteAttribute(@"FieldNameForLanternValue", string.Empty, (o.@FieldNameForLanternValue));
            MyWriteAttribute(@"FieldNameForText", string.Empty, (o.@FieldNameForText));
            MyWriteElementString(@"SQLText", (o.@SQLText));
            MyWriteEndElement(o);
        }
        string Write233_YAxisInfoStyle(global::DCSoft.TemperatureChart.YAxisInfoStyle v)
        {
            string s = null;
            switch (v)
            {
                case global::DCSoft.TemperatureChart.YAxisInfoStyle.@Value: s = @"Value"; break;
                case global::DCSoft.TemperatureChart.YAxisInfoStyle.@Text: s = @"Text"; break;
                case global::DCSoft.TemperatureChart.YAxisInfoStyle.@Background: s = @"Background"; break;
                case global::DCSoft.TemperatureChart.YAxisInfoStyle.@PartialBackground: s = @"PartialBackground"; break;
                case global::DCSoft.TemperatureChart.YAxisInfoStyle.@TextInsideGrid: s = @"TextInsideGrid"; break;
                default: throw CreateInvalidEnumValueException((long)v, typeof(DCSoft.TemperatureChart.YAxisInfoStyle));
            }
            return s;
        }
        string Write225_DateTimePrecisionMode(global::DCSoft.TemperatureChart.DateTimePrecisionMode v)
        {
            string s = null;
            switch (v)
            {
                case global::DCSoft.TemperatureChart.DateTimePrecisionMode.@NoLimited: s = @"NoLimited"; break;
                case global::DCSoft.TemperatureChart.DateTimePrecisionMode.@Second: s = @"Second"; break;
                case global::DCSoft.TemperatureChart.DateTimePrecisionMode.@Minute: s = @"Minute"; break;
                case global::DCSoft.TemperatureChart.DateTimePrecisionMode.@Hour: s = @"Hour"; break;
                case global::DCSoft.TemperatureChart.DateTimePrecisionMode.@Day: s = @"Day"; break;
                case global::DCSoft.TemperatureChart.DateTimePrecisionMode.@Month: s = @"Month"; break;
                case global::DCSoft.TemperatureChart.DateTimePrecisionMode.@Year: s = @"Year"; break;
                default: throw CreateInvalidEnumValueException((long)v, typeof(DCSoft.TemperatureChart.DateTimePrecisionMode));
            }
            return s;
        }
        internal protected void Write232_TitleLineInfo(string n, string ns, global::DCSoft.TemperatureChart.TitleLineInfo o, bool isNullable, bool needType)
        {
            
            var localWriter = this._BaseWriter;
            if (o == null)
            {
                //if (isNullable) WriteNullTagLiteral(n, ns);
                return;
            }
            if (!needType)
            {
                System.Type t = o.GetType();
                if (t == typeof(global::DCSoft.TemperatureChart.TitleLineInfo))
                {
                }
                else
                {
                    throw CreateUnknownTypeException(o);
                }
            }
            MyWriteStartElement(n, ns, o, false, null);
            if (needType) MyWriteXsiType(@"TitleLineInfo", string.Empty);
            if ((o.@InputTimePrecision) != global::DCSoft.TemperatureChart.DateTimePrecisionMode.@Minute)
            {
                localWriter.WriteAttributeString(@"InputTimePrecision", null, Write225_DateTimePrecisionMode((o.@InputTimePrecision)));
            }
            MyWriteAttribute(@"Name", string.Empty, (o.@Name));
            if ((o.@AutoHeight) != false)
            {
                localWriter.WriteAttributeString(@"AutoHeight", null, DCXMLConvert.ToString((o.@AutoHeight)));
            }
            if ((o.@VisibleWhenNoValuePoint) != true)
            {
                localWriter.WriteAttributeString(@"VisibleWhenNoValuePoint", null, DCXMLConvert.ToString((o.@VisibleWhenNoValuePoint)));
            }
            if ((o.@Visible) != true)
            {
                localWriter.WriteAttributeString(@"Visible", null, DCXMLConvert.ToString((o.@Visible)));
            }
            if ((o.@BlankDateWhenNoData) != false)
            {
                localWriter.WriteAttributeString(@"BlankDateWhenNoData", null, DCXMLConvert.ToString((o.@BlankDateWhenNoData)));
            }
            if ((o.@HiddenOnPageViewWhenNoValuePoints) != false)
            {
                localWriter.WriteAttributeString(@"HiddenOnPageViewWhenNoValuePoints", null, DCXMLConvert.ToString((o.@HiddenOnPageViewWhenNoValuePoints)));
            }
            MyWriteAttribute(@"GroupName", string.Empty, (o.@GroupName));
            if ((o.@AfterOperaDaysFromZero) != true)
            {
                localWriter.WriteAttributeString(@"AfterOperaDaysFromZero", null, DCXMLConvert.ToString((o.@AfterOperaDaysFromZero)));
            }
            if ((o.@AfterOperaDaysBeginOne) != false)
            {
                localWriter.WriteAttributeString(@"AfterOperaDaysBeginOne", null, DCXMLConvert.ToString((o.@AfterOperaDaysBeginOne)));
            }
            MyWriteAttribute(@"OutofNormalRangeTextColorValue", string.Empty, (o.@OutofNormalRangeTextColorValue));
            if ((o.@NormalMaxValue) != -10000f)
            {
                localWriter.WriteAttributeString(@"NormalMaxValue", null, DCXMLConvert.ToString((o.@NormalMaxValue)));
            }
            if ((o.@NormalMinValue) != -10000f)
            {
                localWriter.WriteAttributeString(@"NormalMinValue", null, DCXMLConvert.ToString((o.@NormalMinValue)));
            }
            if ((o.@ExtendGridLineType) != global::DCSoft.TemperatureChart.DCExtendGridLineType.@Below)
            {
                localWriter.WriteAttributeString(@"ExtendGridLineType", null, Write226_DCExtendGridLineType((o.@ExtendGridLineType)));
            }
            if ((o.@EnableEndTime) != true)
            {
                localWriter.WriteAttributeString(@"EnableEndTime", null, DCXMLConvert.ToString((o.@EnableEndTime)));
            }
            if ((o.@BlockWidth) != 15f)
            {
                localWriter.WriteAttributeString(@"BlockWidth", null, DCXMLConvert.ToString((o.@BlockWidth)));
            }
            MyWriteAttribute(@"ValueDisplayFormat", string.Empty, (o.@ValueDisplayFormat));
            MyWriteAttribute(@"LoopTextList", string.Empty, (o.@LoopTextList));
            if ((o.@SpecifyTitleWidth) != 0f)
            {
                localWriter.WriteAttributeString(@"SpecifyTitleWidth", null, DCXMLConvert.ToString((o.@SpecifyTitleWidth)));
            }
            MyWriteAttribute(@"Title", string.Empty, (o.@Title));
            MyWriteAttribute(@"PageTitleTexts", string.Empty, (o.@PageTitleTexts));
            MyWriteAttribute(@"TitleColorValue", string.Empty, (o.@TitleColorValue));
            MyWriteAttribute(@"TextColorValue", string.Empty, (o.@TextColorValue));
            if ((o.@TitleAlign) != global::System.Drawing.StringAlignment.@Center)
            {
                localWriter.WriteAttributeString(@"TitleAlign", null, Write119_StringAlignment((o.@TitleAlign)));
            }
            if ((o.@ValueAlign) != global::System.Drawing.StringAlignment.@Center)
            {
                localWriter.WriteAttributeString(@"ValueAlign", null, Write119_StringAlignment((o.@ValueAlign)));
            }
            if ((o.@MaxValueForDayIndex) != 13)
            {
                localWriter.WriteAttributeString(@"MaxValueForDayIndex", null, DCXMLConvert.ToString((o.@MaxValueForDayIndex)));
            }
            MyWriteAttribute(@"CircleText", string.Empty, (o.@CircleText));
            if ((o.@SpecifyHeight) != 0f)
            {
                localWriter.WriteAttributeString(@"SpecifyHeight", null, DCXMLConvert.ToString((o.@SpecifyHeight)));
            }
            MyWriteAttribute(@"EndDateKeyword", string.Empty, (o.@EndDateKeyword));
            if ((o.@StartDate).Ticks != (599266080000000000))
            {
                localWriter.WriteAttributeString(@"StartDate", null, FromDateTime((o.@StartDate)));
            }
            MyWriteAttribute(@"StartDateKeyword", string.Empty, (o.@StartDateKeyword));
            if ((o.@PreserveStartKeywordOrder) != false)
            {
                localWriter.WriteAttributeString(@"PreserveStartKeywordOrder", null, DCXMLConvert.ToString((o.@PreserveStartKeywordOrder)));
            }
            if ((o.@ShowBackColor) != true)
            {
                localWriter.WriteAttributeString(@"ShowBackColor", null, DCXMLConvert.ToString((o.@ShowBackColor)));
            }
            if ((o.@LayoutType) != global::DCSoft.TemperatureChart.TitleLineLayoutType.@Normal)
            {
                localWriter.WriteAttributeString(@"LayoutType", null, Write228_TitleLineLayoutType((o.@LayoutType)));
            }
            if ((o.@TickStep) != 1)
            {
                localWriter.WriteAttributeString(@"TickStep", null, DCXMLConvert.ToString((o.@TickStep)));
            }
            if ((o.@TickLineVisible) != true)
            {
                localWriter.WriteAttributeString(@"TickLineVisible", null, DCXMLConvert.ToString((o.@TickLineVisible)));
            }
            if ((o.@ForceUpWhenPageFirstPoint) != false)
            {
                localWriter.WriteAttributeString(@"ForceUpWhenPageFirstPoint", null, DCXMLConvert.ToString((o.@ForceUpWhenPageFirstPoint)));
            }
            if ((o.@UpAndDownTextType) != global::DCSoft.TemperatureChart.UpAndDownTextType.@None)
            {
                localWriter.WriteAttributeString(@"UpAndDownTextType", null, Write229_UpAndDownTextType((o.@UpAndDownTextType)));
            }
            if ((o.@ValueType) != global::DCSoft.TemperatureChart.TitleLineValueType.@SerialDate)
            {
                localWriter.WriteAttributeString(@"ValueType", null, Write230_TitleLineValueType((o.@ValueType)));
            }
            MyWriteAttribute(@"DataSourceName", string.Empty, (o.@DataSourceName));
            MyWriteAttribute(@"ValueFieldName", string.Empty, (o.@ValueFieldName));
            MyWriteAttribute(@"TimeFieldName", string.Empty, (o.@TimeFieldName));
            if ((o.@ValueTextMultiLine) != false)
            {
                MyWriteElementStringRaw(@"ValueTextMultiLine", DCXMLConvert.ToString((o.@ValueTextMultiLine)));
            }
            Write64_XFontValue(@"Font", string.Empty, (o.@Font), false, false);
            Write64_XFontValue(@"ValueFont", string.Empty, (o.@ValueFont), false, false);
            Write227_ValuePointDataSourceInfo(@"DataSource", string.Empty, (o.@DataSource), false, false);
            {
                global::DCSoft.TemperatureChart.YAxisScaleInfoList a = (global::DCSoft.TemperatureChart.YAxisScaleInfoList)(o.@Scales);
                if (a != null && a.Count > 0)
                {
                    var aCount = a.Count;//2222222
                    localWriter.WriteStartElement(null, @"Scales", null);
                    for (int ia = 0; ia < aCount; ia++)
                    {
                        Write231_YAxisScaleInfo(@"Scale", string.Empty, (a[ia]), true, false);
                    }
                    localWriter.WriteEndElement();
                }
            }
            MyWriteEndElement(o);
        }
        string Write230_TitleLineValueType(global::DCSoft.TemperatureChart.TitleLineValueType v)
        {
            string s = null;
            switch (v)
            {
                case global::DCSoft.TemperatureChart.TitleLineValueType.@NewSerialDate: s = @"NewSerialDate"; break;
                case global::DCSoft.TemperatureChart.TitleLineValueType.@SerialDate: s = @"SerialDate"; break;
                case global::DCSoft.TemperatureChart.TitleLineValueType.@GlobalDayIndex: s = @"GlobalDayIndex"; break;
                case global::DCSoft.TemperatureChart.TitleLineValueType.@InDayIndex: s = @"InDayIndex"; break;
                case global::DCSoft.TemperatureChart.TitleLineValueType.@DayIndex: s = @"DayIndex"; break;
                case global::DCSoft.TemperatureChart.TitleLineValueType.@HourTick: s = @"HourTick"; break;
                case global::DCSoft.TemperatureChart.TitleLineValueType.@Text: s = @"Text"; break;
                case global::DCSoft.TemperatureChart.TitleLineValueType.@Data: s = @"Data"; break;
                case global::DCSoft.TemperatureChart.TitleLineValueType.@TickText: s = @"TickText"; break;
                default: throw CreateInvalidEnumValueException((long)v, typeof(DCSoft.TemperatureChart.TitleLineValueType));
            }
            return s;
        }
        string Write229_UpAndDownTextType(global::DCSoft.TemperatureChart.UpAndDownTextType v)
        {
            string s = null;
            switch (v)
            {
                case global::DCSoft.TemperatureChart.UpAndDownTextType.@None: s = @"None"; break;
                case global::DCSoft.TemperatureChart.UpAndDownTextType.@ShowByTick: s = @"ShowByTick"; break;
                case global::DCSoft.TemperatureChart.UpAndDownTextType.@ShowByText: s = @"ShowByText"; break;
                default: throw CreateInvalidEnumValueException((long)v, typeof(DCSoft.TemperatureChart.UpAndDownTextType));
            }
            return s;
        }
        string Write228_TitleLineLayoutType(global::DCSoft.TemperatureChart.TitleLineLayoutType v)
        {
            string s = null;
            switch (v)
            {
                case global::DCSoft.TemperatureChart.TitleLineLayoutType.@Normal: s = @"Normal"; break;
                case global::DCSoft.TemperatureChart.TitleLineLayoutType.@Free: s = @"Free"; break;
                case global::DCSoft.TemperatureChart.TitleLineLayoutType.@FreeText: s = @"FreeText"; break;
                case global::DCSoft.TemperatureChart.TitleLineLayoutType.@Cascade: s = @"Cascade"; break;
                case global::DCSoft.TemperatureChart.TitleLineLayoutType.@HorizCascade: s = @"HorizCascade"; break;
                case global::DCSoft.TemperatureChart.TitleLineLayoutType.@AutoCascade: s = @"AutoCascade"; break;
                case global::DCSoft.TemperatureChart.TitleLineLayoutType.@Slant: s = @"Slant"; break;
                case global::DCSoft.TemperatureChart.TitleLineLayoutType.@Slant2: s = @"Slant2"; break;
                case global::DCSoft.TemperatureChart.TitleLineLayoutType.@Slant3: s = @"Slant3"; break;
                case global::DCSoft.TemperatureChart.TitleLineLayoutType.@Fraction: s = @"Fraction"; break;
                default: throw CreateInvalidEnumValueException((long)v, typeof(DCSoft.TemperatureChart.TitleLineLayoutType));
            }
            return s;
        }
        string Write226_DCExtendGridLineType(global::DCSoft.TemperatureChart.DCExtendGridLineType v)
        {
            string s = null;
            switch (v)
            {
                case global::DCSoft.TemperatureChart.DCExtendGridLineType.@None: s = @"None"; break;
                case global::DCSoft.TemperatureChart.DCExtendGridLineType.@Above: s = @"Above"; break;
                case global::DCSoft.TemperatureChart.DCExtendGridLineType.@Below: s = @"Below"; break;
                default: throw CreateInvalidEnumValueException((long)v, typeof(DCSoft.TemperatureChart.DCExtendGridLineType));
            }
            return s;
        }
        internal protected void Write224_HeaderLabelInfo(string n, string ns, global::DCSoft.TemperatureChart.HeaderLabelInfo o, bool isNullable, bool needType)
        {
            
            var localWriter = this._BaseWriter;
            if (o == null)
            {
                //if (isNullable) WriteNullTagLiteral(n, ns);
                return;
            }
            if (!needType)
            {
                System.Type t = o.GetType();
                if (t == typeof(global::DCSoft.TemperatureChart.HeaderLabelInfo))
                {
                }
                else
                {
                    throw CreateUnknownTypeException(o);
                }
            }
            MyWriteStartElement(n, ns, o, false, null);
            if (needType) MyWriteXsiType(@"HeaderLabelInfo", string.Empty);
            if (((o.@Title) != null) && ((o.@Title).Length != 0))
            {
                MyWriteAttribute(@"Title", string.Empty, (o.@Title));
            }
            MyWriteAttribute(@"DataSourceName", string.Empty, (o.@DataSourceName));
            MyWriteAttribute(@"ValueFieldName", string.Empty, (o.@ValueFieldName));
            MyWriteAttribute(@"ParameterName", string.Empty, (o.@ParameterName));
            if (((o.@Value) != null) && ((o.@Value).Length != 0))
            {
                MyWriteAttribute(@"Value", string.Empty, (o.@Value));
            }
            localWriter.WriteAttributeString(@"SeperatorChar", null, FromChar((o.@SeperatorChar)));
            MyWriteEndElement(o);
        }
        internal protected void Write222_TickInfo(string n, string ns, global::DCSoft.TemperatureChart.TickInfo o, bool isNullable, bool needType)
        {
            
            var localWriter = this._BaseWriter;
            if (o == null)
            {
                //if (isNullable) WriteNullTagLiteral(n, ns);
                return;
            }
            if (!needType)
            {
                System.Type t = o.GetType();
                if (t == typeof(global::DCSoft.TemperatureChart.TickInfo))
                {
                }
                else
                {
                    throw CreateUnknownTypeException(o);
                }
            }
            MyWriteStartElement(n, ns, o, false, null);
            if (needType) MyWriteXsiType(@"TickInfo", string.Empty);
            if ((o.@Value) != 0f)
            {
                localWriter.WriteAttributeString(@"Value", null, DCXMLConvert.ToString((o.@Value)));
            }
            MyWriteAttribute(@"Text", string.Empty, (o.@Text));
            MyWriteAttribute(@"ColorValue", string.Empty, (o.@ColorValue));
            MyWriteEndElement(o);
        }
        internal protected void Write223_TimeLineZoneInfo(string n, string ns, global::DCSoft.TemperatureChart.TimeLineZoneInfo o, bool isNullable, bool needType)
        {
            
            var localWriter = this._BaseWriter;
            if (o == null)
            {
                //if (isNullable) WriteNullTagLiteral(n, ns);
                return;
            }
            if (!needType)
            {
                System.Type t = o.GetType();
                if (t == typeof(global::DCSoft.TemperatureChart.TimeLineZoneInfo))
                {
                }
                else
                {
                    throw CreateUnknownTypeException(o);
                }
            }
            MyWriteStartElement(n, ns, o, false, null);
            if (needType) MyWriteXsiType(@"TimeLineZoneInfo", string.Empty);
            MyWriteAttribute(@"Name", string.Empty, (o.@Name));
            if ((o.@StartTime).Ticks != (599266080000000000))
            {
                localWriter.WriteAttributeString(@"StartTime", null, FromDateTime((o.@StartTime)));
            }
            if ((o.@EndTime).Ticks != (599266080000000000))
            {
                localWriter.WriteAttributeString(@"EndTime", null, FromDateTime((o.@EndTime)));
            }
            if ((o.@AlignToGrid) != true)
            {
                localWriter.WriteAttributeString(@"AlignToGrid", null, DCXMLConvert.ToString((o.@AlignToGrid)));
            }
            if ((o.@GridLineStyle) != global::System.Drawing.Drawing2D.DashStyle.@Solid)
            {
                localWriter.WriteAttributeString(@"GridLineStyle", null, Write43_DashStyle((o.@GridLineStyle)));
            }
            MyWriteAttribute(@"GridLineColorValue", string.Empty, (o.@GridLineColorValue));
            MyWriteAttribute(@"BackColorValue", string.Empty, (o.@BackColorValue));
            if ((o.@SpecifyTickWidth) != 0f)
            {
                localWriter.WriteAttributeString(@"SpecifyTickWidth", null, DCXMLConvert.ToString((o.@SpecifyTickWidth)));
            }
            if ((o.@AutoTickStepSeconds) != 0)
            {
                localWriter.WriteAttributeString(@"AutoTickStepSeconds", null, DCXMLConvert.ToString((o.@AutoTickStepSeconds)));
            }
            MyWriteAttribute(@"AutoTickFormatString", string.Empty, (o.@AutoTickFormatString));
            {
                global::DCSoft.TemperatureChart.TickInfoList a = (global::DCSoft.TemperatureChart.TickInfoList)(o.@Ticks);
                if (a != null && a.Count > 0)
                {
                    var aCount = a.Count;//2222222
                    localWriter.WriteStartElement(null, @"Ticks", null);
                    for (int ia = 0; ia < aCount; ia++)
                    {
                        Write222_TickInfo(@"Tick", string.Empty, (a[ia]), true, false);
                    }
                    localWriter.WriteEndElement();
                }
            }
            MyWriteEndElement(o);
        }
        internal protected void Write221_GridYSplitInfo(string n, string ns, global::DCSoft.TemperatureChart.GridYSplitInfo o, bool isNullable, bool needType)
        {
            
            var localWriter = this._BaseWriter;
            if (o == null)
            {
                //if (isNullable) WriteNullTagLiteral(n, ns);
                return;
            }
            if (!needType)
            {
                System.Type t = o.GetType();
                if (t == typeof(global::DCSoft.TemperatureChart.GridYSplitInfo))
                {
                }
                else
                {
                    throw CreateUnknownTypeException(o);
                }
            }
            MyWriteStartElement(n, ns, o, false, null);
            if (needType) MyWriteXsiType(@"GridYSplitInfo", string.Empty);
            if ((o.@GridYSplitNum) != 8)
            {
                MyWriteElementStringRaw(@"GridYSplitNum", DCXMLConvert.ToString((o.@GridYSplitNum)));
            }
            if ((o.@GridYSpaceNum) != 5)
            {
                MyWriteElementStringRaw(@"GridYSpaceNum", DCXMLConvert.ToString((o.@GridYSpaceNum)));
            }
            if ((o.@GridYSpaceNumForBottomPadding) != -1)
            {
                MyWriteElementStringRaw(@"GridYSpaceNumForBottomPadding", DCXMLConvert.ToString((o.@GridYSpaceNumForBottomPadding)));
            }
            if ((o.@ThickLineWidth) != 2f)
            {
                MyWriteElementStringRaw(@"ThickLineWidth", DCXMLConvert.ToString((o.@ThickLineWidth)));
            }
            if ((o.@ThinLineWidth) != 1f)
            {
                MyWriteElementStringRaw(@"ThinLineWidth", DCXMLConvert.ToString((o.@ThinLineWidth)));
            }
            MyWriteEndElement(o);
        }
        internal protected void Write219_DocumentPageSettings(string n, string ns, global::DCSoft.TemperatureChart.DocumentPageSettings o, bool isNullable, bool needType)
        {
            
            var localWriter = this._BaseWriter;
            if (o == null)
            {
                //if (isNullable) WriteNullTagLiteral(n, ns);
                return;
            }
            if (!needType)
            {
                System.Type t = o.GetType();
                if (t == typeof(global::DCSoft.TemperatureChart.DocumentPageSettings))
                {
                }
                else
                {
                    throw CreateUnknownTypeException(o);
                }
            }
            MyWriteStartElement(n, ns, o, false, null);
            if (needType) MyWriteXsiType(@"DocumentPageSettings", string.Empty);
            if ((o.@PaperSizeName) != @"A4")
            {
                MyWriteElementString(@"PaperSizeName", (o.@PaperSizeName));
            }
            if ((o.@PaperWidth) != 827)
            {
                MyWriteElementStringRaw(@"PaperWidth", DCXMLConvert.ToString((o.@PaperWidth)));
            }
            if ((o.@PaperHeight) != 1169)
            {
                MyWriteElementStringRaw(@"PaperHeight", DCXMLConvert.ToString((o.@PaperHeight)));
            }
            if ((o.@LeftMargin) != 100)
            {
                MyWriteElementStringRaw(@"LeftMargin", DCXMLConvert.ToString((o.@LeftMargin)));
            }
            if ((o.@TopMargin) != 100)
            {
                MyWriteElementStringRaw(@"TopMargin", DCXMLConvert.ToString((o.@TopMargin)));
            }
            if ((o.@RightMargin) != 100)
            {
                MyWriteElementStringRaw(@"RightMargin", DCXMLConvert.ToString((o.@RightMargin)));
            }
            if ((o.@BottomMargin) != 100)
            {
                MyWriteElementStringRaw(@"BottomMargin", DCXMLConvert.ToString((o.@BottomMargin)));
            }
            if ((o.@Landscape) != false)
            {
                MyWriteElementStringRaw(@"Landscape", DCXMLConvert.ToString((o.@Landscape)));
            }
            if ((o.@AutoFitPageSize) != false)
            {
                MyWriteElementStringRaw(@"AutoFitPageSize", DCXMLConvert.ToString((o.@AutoFitPageSize)));
            }
            MyWriteEndElement(o);
        }
        internal protected void Write218_DCTimeLineLabel(string n, string ns, global::DCSoft.TemperatureChart.DCTimeLineLabel o, bool isNullable, bool needType)
        {
            
            var localWriter = this._BaseWriter;
            if (o == null)
            {
                //if (isNullable) WriteNullTagLiteral(n, ns);
                return;
            }
            if (!needType)
            {
                System.Type t = o.GetType();
                if (t == typeof(global::DCSoft.TemperatureChart.DCTimeLineLabel))
                {
                }
                else
                {
                    throw CreateUnknownTypeException(o);
                }
            }
            MyWriteStartElement(n, ns, o, false, null);
            if (needType) MyWriteXsiType(@"DCTimeLineLabel", string.Empty);
            MyWriteAttribute(@"Text", string.Empty, (o.@Text));
            MyWriteAttribute(@"ParameterName", string.Empty, (o.@ParameterName));
            if ((o.@MultiLine) != false)
            {
                localWriter.WriteAttributeString(@"MultiLine", null, DCXMLConvert.ToString((o.@MultiLine)));
            }
            MyWriteAttribute(@"ColorValue", string.Empty, (o.@ColorValue));
            MyWriteAttribute(@"BackColorValue", string.Empty, (o.@BackColorValue));
            if ((o.@Alignment) != global::System.Drawing.StringAlignment.@Center)
            {
                localWriter.WriteAttributeString(@"Alignment", null, Write119_StringAlignment((o.@Alignment)));
            }
            if ((o.@LineAlignment) != global::System.Drawing.StringAlignment.@Center)
            {
                localWriter.WriteAttributeString(@"LineAlignment", null, Write119_StringAlignment((o.@LineAlignment)));
            }
            if ((o.@Left) != 0f)
            {
                localWriter.WriteAttributeString(@"Left", null, DCXMLConvert.ToString((o.@Left)));
            }
            if ((o.@Top) != 0f)
            {
                localWriter.WriteAttributeString(@"Top", null, DCXMLConvert.ToString((o.@Top)));
            }
            if ((o.@Width) != 100f)
            {
                localWriter.WriteAttributeString(@"Width", null, DCXMLConvert.ToString((o.@Width)));
            }
            if ((o.@Height) != 100f)
            {
                localWriter.WriteAttributeString(@"Height", null, DCXMLConvert.ToString((o.@Height)));
            }
            if ((o.@PositionFixModeForAutoHeightLine) != global::DCSoft.TemperatureChart.LabelPositionFixMode.@None)
            {
                localWriter.WriteAttributeString(@"PositionFixModeForAutoHeightLine", null, Write217_LabelPositionFixMode((o.@PositionFixModeForAutoHeightLine)));
            }
            Write34_XImageValue(@"Image", string.Empty, (o.@Image), false, false);
            if ((o.@ShowBorder) != false)
            {
                MyWriteElementStringRaw(@"ShowBorder", DCXMLConvert.ToString((o.@ShowBorder)));
            }
            Write64_XFontValue(@"Font", string.Empty, (o.@Font), false, false);
            MyWriteEndElement(o);
        }
        string Write217_LabelPositionFixMode(global::DCSoft.TemperatureChart.LabelPositionFixMode v)
        {
            string s = null;
            switch (v)
            {
                case global::DCSoft.TemperatureChart.LabelPositionFixMode.@None: s = @"None"; break;
                case global::DCSoft.TemperatureChart.LabelPositionFixMode.@InsideDataGrid: s = @"InsideDataGrid"; break;
                case global::DCSoft.TemperatureChart.LabelPositionFixMode.@InsideAutoHeightLine: s = @"InsideAutoHeightLine"; break;
                case global::DCSoft.TemperatureChart.LabelPositionFixMode.@AboveAutoHeightLine: s = @"AboveAutoHeightLine"; break;
                default: throw CreateInvalidEnumValueException((long)v, typeof(DCSoft.TemperatureChart.LabelPositionFixMode));
            }
            return s;
        }
        internal protected void Write216_DCTimeLineImage(string n, string ns, global::DCSoft.TemperatureChart.DCTimeLineImage o, bool isNullable, bool needType)
        {
            
            var localWriter = this._BaseWriter;
            if (o == null)
            {
                //if (isNullable) WriteNullTagLiteral(n, ns);
                return;
            }
            if (!needType)
            {
                System.Type t = o.GetType();
                if (t == typeof(global::DCSoft.TemperatureChart.DCTimeLineImage))
                {
                }
                else
                {
                    throw CreateUnknownTypeException(o);
                }
            }
            MyWriteStartElement(n, ns, o, false, null);
            if (needType) MyWriteXsiType(@"DCTimeLineImage", string.Empty);
            MyWriteAttribute(@"Name", string.Empty, (o.@Name));
            if ((o.@Left) != 0f)
            {
                localWriter.WriteAttributeString(@"Left", null, DCXMLConvert.ToString((o.@Left)));
            }
            if ((o.@Top) != 0f)
            {
                localWriter.WriteAttributeString(@"Top", null, DCXMLConvert.ToString((o.@Top)));
            }
            Write34_XImageValue(@"Image", string.Empty, (o.@Image), false, false);
            MyWriteEndElement(o);
        }
        string Write215_DCTimeUnit(global::DCSoft.TemperatureChart.DCTimeUnit v)
        {
            string s = null;
            switch (v)
            {
                case global::DCSoft.TemperatureChart.DCTimeUnit.@Second: s = @"Second"; break;
                case global::DCSoft.TemperatureChart.DCTimeUnit.@Minute: s = @"Minute"; break;
                case global::DCSoft.TemperatureChart.DCTimeUnit.@Hour: s = @"Hour"; break;
                case global::DCSoft.TemperatureChart.DCTimeUnit.@Day: s = @"Day"; break;
                case global::DCSoft.TemperatureChart.DCTimeUnit.@Week: s = @"Week"; break;
                case global::DCSoft.TemperatureChart.DCTimeUnit.@Month: s = @"Month"; break;
                case global::DCSoft.TemperatureChart.DCTimeUnit.@Year: s = @"Year"; break;
                default: throw CreateInvalidEnumValueException((long)v, typeof(DCSoft.TemperatureChart.DCTimeUnit));
            }
            return s;
        }
        string Write214_DCTimeLineSelectionMode(global::DCSoft.TemperatureChart.DCTimeLineSelectionMode v)
        {
            string s = null;
            switch (v)
            {
                case global::DCSoft.TemperatureChart.DCTimeLineSelectionMode.@None: s = @"None"; break;
                case global::DCSoft.TemperatureChart.DCTimeLineSelectionMode.@SingleSelect: s = @"SingleSelect"; break;
                case global::DCSoft.TemperatureChart.DCTimeLineSelectionMode.@MultiSelec: s = @"MultiSelec"; break;
                default: throw CreateInvalidEnumValueException((long)v, typeof(DCSoft.TemperatureChart.DCTimeLineSelectionMode));
            }
            return s;
        }
        string Write213_EditValuePointEventHandleMode(global::DCSoft.TemperatureChart.EditValuePointEventHandleMode v)
        {
            string s = null;
            switch (v)
            {
                case global::DCSoft.TemperatureChart.EditValuePointEventHandleMode.@None: s = @"None"; break;
                case global::DCSoft.TemperatureChart.EditValuePointEventHandleMode.@Program: s = @"Program"; break;
                case global::DCSoft.TemperatureChart.EditValuePointEventHandleMode.@Silent: s = @"Silent"; break;
                case global::DCSoft.TemperatureChart.EditValuePointEventHandleMode.@OwnedUI: s = @"OwnedUI"; break;
                default: throw CreateInvalidEnumValueException((long)v, typeof(DCSoft.TemperatureChart.EditValuePointEventHandleMode));
            }
            return s;
        }
        string Write212_DocumentLinkVisualStyle(global::DCSoft.TemperatureChart.DocumentLinkVisualStyle v)
        {
            string s = null;
            switch (v)
            {
                case global::DCSoft.TemperatureChart.DocumentLinkVisualStyle.@None: s = @"None"; break;
                case global::DCSoft.TemperatureChart.DocumentLinkVisualStyle.@Hover: s = @"Hover"; break;
                case global::DCSoft.TemperatureChart.DocumentLinkVisualStyle.@Always: s = @"Always"; break;
                default: throw CreateInvalidEnumValueException((long)v, typeof(DCSoft.TemperatureChart.DocumentLinkVisualStyle));
            }
            return s;
        }
    }

    public class TemperatureDocumentReader
    {
        private System.Xml.XmlReader Reader = null;
        public TemperatureDocumentReader(System.IO.TextReader w)
        {
            Reader = new System.Xml.XmlTextReader(w);
            InitIDs();
        }
        public TemperatureDocumentReader(string filename)
        {
            Reader = new System.Xml.XmlTextReader(filename);
            InitIDs();
        }
        public TemperatureDocumentReader(System.IO.Stream stream)
        {
            Reader = new System.Xml.XmlTextReader(stream);
            InitIDs();

        }

        public void Close()
        {
            if (Reader != null)
            {
                Reader.Close();
            }
        }

        private void UnknownNode(object o, string qnames)
        {
            Exception e = new Exception("未知的XML节点：" + qnames);
            throw e;
        }

        private static int ReaderCount = 0;
        private void CheckReaderCount(ref int i, ref int j)
        {

        }

        private DateTime ToDateTime(string s)
        {
            DateTime dt = DateTime.MinValue;
            DateTime.TryParse(s, out dt);
            return dt;
        }

        private char ToChar(string s)
        {
            int i = int.MinValue;
            int.TryParse(s, out i);
            char c = (char)i;
            return c;
        }


        private System.Collections.Generic.Dictionary<string, string> _StringValues
            = new System.Collections.Generic.Dictionary<string, string>();
        private string CacheString(string v)
        {
            if (v == null || v.Length == 0)
            {
                return v;
            }
            string result = v;
            if (_StringValues.TryGetValue(v, out result) == false)
            {
                _StringValues[v] = v;
                result = v;
            }
            return result;
        }
        internal protected global::System.Drawing.StringAlignment Read119_StringAlignment(string s)
        {
            switch (s)
            {
                case @"Near": return global::System.Drawing.StringAlignment.@Near;
                case @"Center": return global::System.Drawing.StringAlignment.@Center;
                case @"Far": return global::System.Drawing.StringAlignment.@Far;
                default: return (default(global::System.Drawing.StringAlignment));
            }
        }
        internal protected global::System.Drawing.Drawing2D.PenAlignment Read191_PenAlignment(string s)
        {
            switch (s)
            {
                case @"Center": return global::System.Drawing.Drawing2D.PenAlignment.@Center;
                case @"Inset": return global::System.Drawing.Drawing2D.PenAlignment.@Inset;
                case @"Outset": return global::System.Drawing.Drawing2D.PenAlignment.@Outset;
                case @"Left": return global::System.Drawing.Drawing2D.PenAlignment.@Left;
                case @"Right": return global::System.Drawing.Drawing2D.PenAlignment.@Right;
                default: return (default(global::System.Drawing.Drawing2D.PenAlignment));
            }
        }
        internal protected global::System.Drawing.Drawing2D.LineCap Read190_LineCap(string s)
        {

            if (__System_Drawing_Drawing2D_LineCap == null)
            {
                lock (_NewDictionaryLockObject)
                {
                    var dic20200818 = new System.Collections.Generic.Dictionary<string, System.Drawing.Drawing2D.LineCap>();
                    dic20200818.Add("Flat", System.Drawing.Drawing2D.LineCap.@Flat);
                    dic20200818.Add("Square", System.Drawing.Drawing2D.LineCap.@Square);
                    dic20200818.Add("Round", System.Drawing.Drawing2D.LineCap.@Round);
                    dic20200818.Add("Triangle", System.Drawing.Drawing2D.LineCap.@Triangle);
                    dic20200818.Add("NoAnchor", System.Drawing.Drawing2D.LineCap.@NoAnchor);
                    dic20200818.Add("SquareAnchor", System.Drawing.Drawing2D.LineCap.@SquareAnchor);
                    dic20200818.Add("RoundAnchor", System.Drawing.Drawing2D.LineCap.@RoundAnchor);
                    dic20200818.Add("DiamondAnchor", System.Drawing.Drawing2D.LineCap.@DiamondAnchor);
                    dic20200818.Add("ArrowAnchor", System.Drawing.Drawing2D.LineCap.@ArrowAnchor);
                    dic20200818.Add("Custom", System.Drawing.Drawing2D.LineCap.@Custom);
                    dic20200818.Add("AnchorMask", System.Drawing.Drawing2D.LineCap.@AnchorMask);
                    __System_Drawing_Drawing2D_LineCap = dic20200818;
                }
            }
            var result = default(System.Drawing.Drawing2D.LineCap);
            if (__System_Drawing_Drawing2D_LineCap.TryGetValue(s, out result))
            {
                return result;
            }
            else
            {
                return default(System.Drawing.Drawing2D.LineCap);
            }
        }
        private static System.Collections.Generic.Dictionary<string, System.Drawing.Drawing2D.LineCap> __System_Drawing_Drawing2D_LineCap = null;

        internal protected global::System.Drawing.Drawing2D.LineJoin Read189_LineJoin(string s)
        {
            switch (s)
            {
                case @"Miter": return global::System.Drawing.Drawing2D.LineJoin.@Miter;
                case @"Bevel": return global::System.Drawing.Drawing2D.LineJoin.@Bevel;
                case @"Round": return global::System.Drawing.Drawing2D.LineJoin.@Round;
                case @"MiterClipped": return global::System.Drawing.Drawing2D.LineJoin.@MiterClipped;
                default: return (default(global::System.Drawing.Drawing2D.LineJoin));
            }
        }
        internal protected global::System.Drawing.Drawing2D.DashCap Read188_DashCap(string s)
        {
            switch (s)
            {
                case @"Flat": return global::System.Drawing.Drawing2D.DashCap.@Flat;
                case @"Round": return global::System.Drawing.Drawing2D.DashCap.@Round;
                case @"Triangle": return global::System.Drawing.Drawing2D.DashCap.@Triangle;
                default: return (default(global::System.Drawing.Drawing2D.DashCap));
            }
        }
        internal protected global::System.Drawing.Drawing2D.DashStyle Read43_DashStyle(string s)
        {

            if (__System_Drawing_Drawing2D_DashStyle == null)
            {
                lock (_NewDictionaryLockObject)
                {
                    var dic20200818 = new System.Collections.Generic.Dictionary<string, System.Drawing.Drawing2D.DashStyle>();
                    dic20200818.Add("Solid", System.Drawing.Drawing2D.DashStyle.@Solid);
                    dic20200818.Add("Dash", System.Drawing.Drawing2D.DashStyle.@Dash);
                    dic20200818.Add("Dot", System.Drawing.Drawing2D.DashStyle.@Dot);
                    dic20200818.Add("DashDot", System.Drawing.Drawing2D.DashStyle.@DashDot);
                    dic20200818.Add("DashDotDot", System.Drawing.Drawing2D.DashStyle.@DashDotDot);
                    dic20200818.Add("Custom", System.Drawing.Drawing2D.DashStyle.@Custom);
                    __System_Drawing_Drawing2D_DashStyle = dic20200818;
                }
            }
            var result = default(System.Drawing.Drawing2D.DashStyle);
            if (__System_Drawing_Drawing2D_DashStyle.TryGetValue(s, out result))
            {
                return result;
            }
            else
            {
                return default(System.Drawing.Drawing2D.DashStyle);
            }
        }
        private static System.Collections.Generic.Dictionary<string, System.Drawing.Drawing2D.DashStyle> __System_Drawing_Drawing2D_DashStyle = null;

        internal protected global::DCSoft.Drawing.XPenStyle Read192_XPenStyle(bool isNullable, bool checkType)
        {
            var ThisReader = this.Reader;
            global::DCSoft.Drawing.XPenStyle o;
            o = new global::DCSoft.Drawing.XPenStyle();
            //bool[] paramsRead = new bool[9];
            //while (ThisReader.MoveToNextAttribute()){}
            ThisReader.MoveToElement();
            if (ThisReader.IsEmptyElement)
            {
                ThisReader.Skip();
                return o;
            }
            ThisReader.ReadStartElement();
            ThisReader.MoveToContent();
            int whileIterations288 = 0;
            int readerCount288 = ReaderCount;
            var _ReaderNodeType1 = ThisReader.NodeType;
            while (_ReaderNodeType1 != System.Xml.XmlNodeType.EndElement && _ReaderNodeType1 != System.Xml.XmlNodeType.None)
            {
                if (_ReaderNodeType1 == System.Xml.XmlNodeType.Element)
                {
                    string _ReaderLocalName = ThisReader.LocalName;
                    if (((object)_ReaderLocalName == (object)id168_Color))
                    {
                        {
                            o.@ColorString = CacheString(ThisReader.ReadElementString());
                        }
                    }
                    else if (((object)_ReaderLocalName == (object)id217_Width))
                    {
                        if (ThisReader.IsEmptyElement)
                        {
                            ThisReader.Skip();
                        }
                        else
                        {
                            o.@Width = ToSingle(ThisReader.ReadElementString());
                        }
                    }
                    else if (((object)_ReaderLocalName == (object)id802_DashStyle))
                    {
                        if (ThisReader.IsEmptyElement)
                        {
                            ThisReader.Skip();
                        }
                        else
                        {
                            o.@DashStyle = Read43_DashStyle(ThisReader.ReadElementString());
                        }
                    }
                    else if (((object)_ReaderLocalName == (object)id803_DashCap))
                    {
                        if (ThisReader.IsEmptyElement)
                        {
                            ThisReader.Skip();
                        }
                        else
                        {
                            o.@DashCap = Read188_DashCap(ThisReader.ReadElementString());
                        }
                    }
                    else if (((object)_ReaderLocalName == (object)id804_LineJoin))
                    {
                        if (ThisReader.IsEmptyElement)
                        {
                            ThisReader.Skip();
                        }
                        else
                        {
                            o.@LineJoin = Read189_LineJoin(ThisReader.ReadElementString());
                        }
                    }
                    else if (((object)_ReaderLocalName == (object)id805_StartCap))
                    {
                        if (ThisReader.IsEmptyElement)
                        {
                            ThisReader.Skip();
                        }
                        else
                        {
                            o.@StartCap = Read190_LineCap(ThisReader.ReadElementString());
                        }
                    }
                    else if (((object)_ReaderLocalName == (object)id806_EndCap))
                    {
                        if (ThisReader.IsEmptyElement)
                        {
                            ThisReader.Skip();
                        }
                        else
                        {
                            o.@EndCap = Read190_LineCap(ThisReader.ReadElementString());
                        }
                    }
                    else if (((object)_ReaderLocalName == (object)id807_MiterLimit))
                    {
                        if (ThisReader.IsEmptyElement)
                        {
                            ThisReader.Skip();
                        }
                        else
                        {
                            o.@MiterLimit = ToSingle(ThisReader.ReadElementString());
                        }
                    }
                    else if (((object)_ReaderLocalName == (object)id469_Alignment))
                    {
                        if (ThisReader.IsEmptyElement)
                        {
                            ThisReader.Skip();
                        }
                        else
                        {
                            o.@Alignment = Read191_PenAlignment(ThisReader.ReadElementString());
                        }
                    }
                    else
                    {
                        UnknownNode(o, _ReaderLocalName);//":Color, :Width, :DashStyle, :DashCap, :LineJoin, :StartCap, :EndCap, :MiterLimit, :Alignment");
                    }
                }
                else
                {
                    UnknownNode(o, ThisReader.LocalName);//":Color, :Width, :DashStyle, :DashCap, :LineJoin, :StartCap, :EndCap, :MiterLimit, :Alignment");
                }
                ThisReader.MoveToContent();
                CheckReaderCount(ref whileIterations288, ref readerCount288);
                _ReaderNodeType1 = ThisReader.NodeType;
            }
            ThisReader.ReadEndElement();
            return o;
        }

        internal protected global::System.Drawing.Color Read114_Color(bool checkType)
        {
            var ThisReader = this.Reader;
            //var xsiType = checkType ? GetXsiType() : null;
            //bool isNull = false;
            //if (checkType)
            //{
            //    var xsiTypeName = xsiType == null ? null : xsiType.Name;
            //    if (xsiTypeName == null || ((object)xsiTypeName == (object)id168_Color))
            //    {
            //    }
            //    else
            //        throw CreateUnknownTypeException(xsiType);
            //}
            global::System.Drawing.Color o;
            try
            {
                o = (global::System.Drawing.Color)System.Activator.CreateInstance(typeof(global::System.Drawing.Color), System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.CreateInstance | System.Reflection.BindingFlags.NonPublic, null, new object[0], null);
            }
            catch (System.MissingMethodException)
            {
                throw new Exception("InaccessibleConstructorException: global::System.Drawing.Color");
            }
            catch (System.Security.SecurityException)
            {
                throw new Exception("CtorHasSecurityException: global::System.Drawing.Color");
            }
            //bool[] paramsRead = new bool[0];
            //while (ThisReader.MoveToNextAttribute()){}
            ThisReader.MoveToElement();
            if (ThisReader.IsEmptyElement)
            {
                ThisReader.Skip();
                return o;
            }
            ThisReader.ReadStartElement();
            ThisReader.MoveToContent();
            int whileIterations125 = 0;
            int readerCount125 = ReaderCount;
            var _ReaderNodeType1 = ThisReader.NodeType;
            while (_ReaderNodeType1 != System.Xml.XmlNodeType.EndElement && _ReaderNodeType1 != System.Xml.XmlNodeType.None)
            {
                if (_ReaderNodeType1 == System.Xml.XmlNodeType.Element)
                {
                    UnknownNode(o, ThisReader.LocalName);
                }
                else
                {
                    UnknownNode(o, ThisReader.LocalName);
                }
                ThisReader.MoveToContent();
                CheckReaderCount(ref whileIterations125, ref readerCount125);
                _ReaderNodeType1 = ThisReader.NodeType;
            }
            ThisReader.ReadEndElement();
            return o;
        }
        internal protected global::System.Drawing.GraphicsUnit Read60_GraphicsUnit(string s)
        {

            if (__System_Drawing_GraphicsUnit == null)
            {
                lock (_NewDictionaryLockObject)
                {
                    var dic20200818 = new System.Collections.Generic.Dictionary<string, System.Drawing.GraphicsUnit>();
                    dic20200818.Add("World", System.Drawing.GraphicsUnit.@World);
                    dic20200818.Add("Display", System.Drawing.GraphicsUnit.@Display);
                    dic20200818.Add("Pixel", System.Drawing.GraphicsUnit.@Pixel);
                    dic20200818.Add("Point", System.Drawing.GraphicsUnit.@Point);
                    dic20200818.Add("Inch", System.Drawing.GraphicsUnit.@Inch);
                    dic20200818.Add("Document", System.Drawing.GraphicsUnit.@Document);
                    dic20200818.Add("Millimeter", System.Drawing.GraphicsUnit.@Millimeter);
                    __System_Drawing_GraphicsUnit = dic20200818;
                }
            }
            var result = default(System.Drawing.GraphicsUnit);
            if (__System_Drawing_GraphicsUnit.TryGetValue(s, out result))
            {
                return result;
            }
            else
            {
                return default(System.Drawing.GraphicsUnit);
            }
        }
        private static System.Collections.Generic.Dictionary<string, System.Drawing.GraphicsUnit> __System_Drawing_GraphicsUnit = null;

        public static bool ToBoolean(string v)
        {
            if (v == "true") return true;
            else if (v == "false") return false;
            if (v == null || v.Length == 0)
            {
                return false;
            }
            else
            {
                v = v.Trim().ToLower();
                if (v == "true" || v == "1") return true;
                else if (v == "false" || v == "0") return false;
                throw new FormatException("XML-Boolean:" + v);
            }
        }
        internal protected global::DCSoft.Drawing.XFontValue Read64_XFontValue(bool isNullable, bool checkType)
        {
            var ThisReader = this.Reader;
            global::DCSoft.Drawing.XFontValue o;
            o = new global::DCSoft.Drawing.XFontValue();
            //bool[] paramsRead = new bool[7];
            //while (ThisReader.MoveToNextAttribute()){}
            ThisReader.MoveToElement();
            if (ThisReader.IsEmptyElement)
            {
                ThisReader.Skip();
                return o;
            }
            ThisReader.ReadStartElement();
            ThisReader.MoveToContent();
            int whileIterations18 = 0;
            int readerCount18 = ReaderCount;
            var _ReaderNodeType1 = ThisReader.NodeType;
            while (_ReaderNodeType1 != System.Xml.XmlNodeType.EndElement && _ReaderNodeType1 != System.Xml.XmlNodeType.None)
            {
                if (_ReaderNodeType1 == System.Xml.XmlNodeType.Element)
                {
                    string _ReaderLocalName = ThisReader.LocalName;
                    if (((object)_ReaderLocalName == (object)id62_Name))
                    {
                        {
                            o.@Name = CacheString(ThisReader.ReadElementString());
                        }
                    }
                    else if (((object)_ReaderLocalName == (object)id151_Size))
                    {
                        if (ThisReader.IsEmptyElement)
                        {
                            ThisReader.Skip();
                        }
                        else
                        {
                            o.@Size = ToSingle(ThisReader.ReadElementString());
                        }
                    }
                    else if (((object)_ReaderLocalName == (object)id152_Unit))
                    {
                        if (ThisReader.IsEmptyElement)
                        {
                            ThisReader.Skip();
                        }
                        else
                        {
                            o.@Unit = Read60_GraphicsUnit(ThisReader.ReadElementString());
                        }
                    }
                    else if (((object)_ReaderLocalName == (object)id153_Bold))
                    {
                        if (ThisReader.IsEmptyElement)
                        {
                            ThisReader.Skip();
                        }
                        else
                        {
                            o.@Bold = ToBoolean(ThisReader.ReadElementString());
                        }
                    }
                    else if (((object)_ReaderLocalName == (object)id154_Italic))
                    {
                        if (ThisReader.IsEmptyElement)
                        {
                            ThisReader.Skip();
                        }
                        else
                        {
                            o.@Italic = ToBoolean(ThisReader.ReadElementString());
                        }
                    }
                    else if (((object)_ReaderLocalName == (object)id155_Underline))
                    {
                        if (ThisReader.IsEmptyElement)
                        {
                            ThisReader.Skip();
                        }
                        else
                        {
                            o.@Underline = ToBoolean(ThisReader.ReadElementString());
                        }
                    }
                    else if (((object)_ReaderLocalName == (object)id156_Strikeout))
                    {
                        if (ThisReader.IsEmptyElement)
                        {
                            ThisReader.Skip();
                        }
                        else
                        {
                            o.@Strikeout = ToBoolean(ThisReader.ReadElementString());
                        }
                    }
                    else
                    {
                        UnknownNode(o, _ReaderLocalName);//":Name, :Size, :Unit, :Bold, :Italic, :Underline, :Strikeout");
                    }
                }
                else
                {
                    UnknownNode(o, ThisReader.LocalName);//":Name, :Size, :Unit, :Bold, :Italic, :Underline, :Strikeout");
                }
                ThisReader.MoveToContent();
                CheckReaderCount(ref whileIterations18, ref readerCount18);
                _ReaderNodeType1 = ThisReader.NodeType;
            }
            ThisReader.ReadEndElement();
            return o;
        }
        private static readonly object _NewDictionaryLockObject = new object();

        internal protected global::DCSoft.Drawing.XImageValue Read34_XImageValue(bool isNullable, bool checkType)
        {
            var ThisReader = this.Reader;
            global::DCSoft.Drawing.XImageValue o;
            o = new global::DCSoft.Drawing.XImageValue();
            //bool[] paramsRead = new bool[3];
            //while (ThisReader.MoveToNextAttribute()){}
            ThisReader.MoveToElement();
            if (ThisReader.IsEmptyElement)
            {
                ThisReader.Skip();
                return o;
            }
            ThisReader.ReadStartElement();
            ThisReader.MoveToContent();
            int whileIterations15 = 0;
            int readerCount15 = ReaderCount;
            var _ReaderNodeType1 = ThisReader.NodeType;
            while (_ReaderNodeType1 != System.Xml.XmlNodeType.EndElement && _ReaderNodeType1 != System.Xml.XmlNodeType.None)
            {
                if (_ReaderNodeType1 == System.Xml.XmlNodeType.Element)
                {
                    string _ReaderLocalName = ThisReader.LocalName;
                    if (((object)_ReaderLocalName == (object)id135_HorizontalResolution))
                    {
                        if (ThisReader.IsEmptyElement)
                        {
                            ThisReader.Skip();
                        }
                        else
                        {
                            o.@HorizontalResolution = ToSingle(ThisReader.ReadElementString());
                        }
                    }
                    else if (((object)_ReaderLocalName == (object)id136_VerticalResolution))
                    {
                        if (ThisReader.IsEmptyElement)
                        {
                            ThisReader.Skip();
                        }
                        else
                        {
                            o.@VerticalResolution = ToSingle(ThisReader.ReadElementString());
                        }
                    }
                    else if (((object)_ReaderLocalName == (object)id137_ImageDataBase64String))
                    {
                        {
                            ReadImageDataBase64String(ThisReader, o);
                        }
                    }
                    else
                    {
                        UnknownNode(o, _ReaderLocalName);//":HorizontalResolution, :VerticalResolution, :ImageDataBase64String");
                    }
                }
                else
                {
                    UnknownNode(o, ThisReader.LocalName);//":HorizontalResolution, :VerticalResolution, :ImageDataBase64String");
                }
                ThisReader.MoveToContent();
                CheckReaderCount(ref whileIterations15, ref readerCount15);
                _ReaderNodeType1 = ThisReader.NodeType;
            }
            ThisReader.ReadEndElement();
            return o;
        }
        public static void ReadImageDataBase64String(System.Xml.XmlReader reader, DCSoft.Drawing.XImageValue img)
        {
            img.ImageDataBase64String = reader.ReadElementString();
        }
        public static float ToSingle(string v)
        {
            if (v == null || v.Length == 0)
            {
                return 0;
            }
            if (v == "NaN")
            {
                return 0;// float.NaN;
            }
            float dv = 0;
            if (float.TryParse(v, out dv))
            {
                return dv;
            }
            return 0;
        }
        public object Read_TemperatureDocument()
        {
            var ThisReader = this.Reader;
            object o = null;
            ThisReader.MoveToContent();
            if (ThisReader.NodeType == System.Xml.XmlNodeType.Element)
            {
                string _ReaderLocalName = ThisReader.LocalName;
                if (((object)_ReaderLocalName == (object)id1_TemperatureDocument))
                {
                    o = Read243_TemperatureDocument(true, true);
                }
                else
                {
                    throw new Exception("UnknownNodeException");
                }
            }
            else
            {
                UnknownNode(null, ThisReader.LocalName); //":XTextDocument");
            }
            this._StringValues.Clear();
            return o;
        }
        internal protected global::DCSoft.TemperatureChart.TemperatureDocument Read243_TemperatureDocument(bool isNullable, bool checkType)
        {
            var ThisReader = this.Reader;
            global::DCSoft.TemperatureChart.TemperatureDocument o;
            o = new global::DCSoft.TemperatureChart.TemperatureDocument();
            //            if ((object)(o.@Parameters) == null) o.@Parameters = new global::DCSoft.TemperatureChart.DCTimeLineParameterList();
            //            global::DCSoft.TemperatureChart.DCTimeLineParameterList a_1 = (global::DCSoft.TemperatureChart.DCTimeLineParameterList)o.@Parameters;
            //            if ((object)(o.@Datas) == null) o.@Datas = new global::DCSoft.TemperatureChart.DocumentDataList();
            //            global::DCSoft.TemperatureChart.DocumentDataList a_2 = (global::DCSoft.TemperatureChart.DocumentDataList)o.@Datas;
            //bool[] paramsRead = new bool[3];
            //while (ThisReader.MoveToNextAttribute()){}
            ThisReader.MoveToElement();
            if (ThisReader.IsEmptyElement)
            {
                ThisReader.Skip();
                return o;
            }
            ThisReader.ReadStartElement();
            ThisReader.MoveToContent();
            int whileIterations306 = 0;
            int readerCount306 = ReaderCount;
            var _ReaderNodeType1 = ThisReader.NodeType;
            while (_ReaderNodeType1 != System.Xml.XmlNodeType.EndElement && _ReaderNodeType1 != System.Xml.XmlNodeType.None)
            {
                if (_ReaderNodeType1 == System.Xml.XmlNodeType.Element)
                {
                    string _ReaderLocalName = ThisReader.LocalName;
                    if (((object)_ReaderLocalName == (object)id838_Config))
                    {
                        o.@Config = Read237_TemperatureDocumentConfig(false, true);
                    }
                    else if (((object)_ReaderLocalName == (object)id64_Parameters))
                    {
                        if (true)
                        {//if (!ReadNull()) {
                            if ((ThisReader.IsEmptyElement))
                            {
                                ThisReader.Skip();
                            }
                            else
                            {
                                if ((o.@Parameters) == null) o.@Parameters = new global::DCSoft.TemperatureChart.DCTimeLineParameterList();
                                global::DCSoft.TemperatureChart.DCTimeLineParameterList a_1_0 = o.@Parameters;
                                ThisReader.ReadStartElement();
                                ThisReader.MoveToContent();
                                int whileIterations307 = 0;
                                int readerCount307 = ReaderCount;
                                var _ReaderNodeType2 = ThisReader.NodeType;
                                while (_ReaderNodeType2 != System.Xml.XmlNodeType.EndElement && _ReaderNodeType2 != System.Xml.XmlNodeType.None)
                                {
                                    if (_ReaderNodeType2 == System.Xml.XmlNodeType.Element)
                                    {
                                        if (((object)ThisReader.LocalName == (object)id65_Parameter))
                                        {
                                            if ((a_1_0) == null) ThisReader.Skip(); else a_1_0.Add(Read238_DCTimeLineParameter(true, true));
                                        }
                                        else
                                        {
                                            UnknownNode(null, ThisReader.LocalName); //":Parameter");
                                        }
                                    }
                                    else
                                    {
                                        UnknownNode(null, ThisReader.LocalName); //":Parameter");
                                    }
                                    ThisReader.MoveToContent();
                                    CheckReaderCount(ref whileIterations307, ref readerCount307);
                                    _ReaderNodeType2 = ThisReader.NodeType;
                                }
                                ThisReader.ReadEndElement();
                            }
                        }
                    }
                    else if (((object)_ReaderLocalName == (object)id839_Datas))
                    {
                        if (true)
                        {//if (!ReadNull()) {
                            if ((ThisReader.IsEmptyElement))
                            {
                                ThisReader.Skip();
                            }
                            else
                            {
                                if ((o.@Datas) == null) o.@Datas = new global::DCSoft.TemperatureChart.DocumentDataList();
                                global::DCSoft.TemperatureChart.DocumentDataList a_2_0 = o.@Datas;
                                ThisReader.ReadStartElement();
                                ThisReader.MoveToContent();
                                int whileIterations308 = 0;
                                int readerCount308 = ReaderCount;
                                var _ReaderNodeType2 = ThisReader.NodeType;
                                while (_ReaderNodeType2 != System.Xml.XmlNodeType.EndElement && _ReaderNodeType2 != System.Xml.XmlNodeType.None)
                                {
                                    if (_ReaderNodeType2 == System.Xml.XmlNodeType.Element)
                                    {
                                        if (((object)ThisReader.LocalName == (object)id840_Data))
                                        {
                                            if ((a_2_0) == null) ThisReader.Skip(); else a_2_0.Add(Read242_DocumentData(true, true));
                                        }
                                        else
                                        {
                                            UnknownNode(null, ThisReader.LocalName); //":Data");
                                        }
                                    }
                                    else
                                    {
                                        UnknownNode(null, ThisReader.LocalName); //":Data");
                                    }
                                    ThisReader.MoveToContent();
                                    CheckReaderCount(ref whileIterations308, ref readerCount308);
                                    _ReaderNodeType2 = ThisReader.NodeType;
                                }
                                ThisReader.ReadEndElement();
                            }
                        }
                    }
                    else
                    {
                        UnknownNode(o, _ReaderLocalName);//":Config, :Parameters, :Datas");
                    }
                }
                else
                {
                    UnknownNode(o, ThisReader.LocalName);//":Config, :Parameters, :Datas");
                }
                ThisReader.MoveToContent();
                CheckReaderCount(ref whileIterations306, ref readerCount306);
                _ReaderNodeType1 = ThisReader.NodeType;
            }
            ThisReader.ReadEndElement();
            return o;
        }
        internal protected global::DCSoft.TemperatureChart.DocumentData Read242_DocumentData(bool isNullable, bool checkType)
        {
            var ThisReader = this.Reader;
            global::DCSoft.TemperatureChart.DocumentData o;
            o = new global::DCSoft.TemperatureChart.DocumentData();
            //            if ((object)(o.@Values) == null) o.@Values = new global::DCSoft.TemperatureChart.ValuePointList();
            //            global::DCSoft.TemperatureChart.ValuePointList a_1 = (global::DCSoft.TemperatureChart.ValuePointList)o.@Values;
            //bool[] paramsRead = new bool[2];
            while (ThisReader.MoveToNextAttribute())
            {
                string _ReaderLocalName = ThisReader.LocalName;
                if (((object)_ReaderLocalName == (object)id62_Name))
                {
                    o.@Name = ThisReader.Value;
                }
            }
            ThisReader.MoveToElement();
            if (ThisReader.IsEmptyElement)
            {
                ThisReader.Skip();
                return o;
            }
            ThisReader.ReadStartElement();
            ThisReader.MoveToContent();
            int whileIterations309 = 0;
            int readerCount309 = ReaderCount;
            var _ReaderNodeType1 = ThisReader.NodeType;
            while (_ReaderNodeType1 != System.Xml.XmlNodeType.EndElement && _ReaderNodeType1 != System.Xml.XmlNodeType.None)
            {
                if (_ReaderNodeType1 == System.Xml.XmlNodeType.Element)
                {
                    string _ReaderLocalName = ThisReader.LocalName;
                    if (((object)_ReaderLocalName == (object)id842_Values))
                    {
                        if (true)
                        {//if (!ReadNull()) {
                            if ((ThisReader.IsEmptyElement))
                            {
                                ThisReader.Skip();
                            }
                            else
                            {
                                if ((o.@Values) == null) o.@Values = new global::DCSoft.TemperatureChart.ValuePointList();
                                global::DCSoft.TemperatureChart.ValuePointList a_1_0 = o.@Values;
                                ThisReader.ReadStartElement();
                                ThisReader.MoveToContent();
                                int whileIterations310 = 0;
                                int readerCount310 = ReaderCount;
                                var _ReaderNodeType2 = ThisReader.NodeType;
                                while (_ReaderNodeType2 != System.Xml.XmlNodeType.EndElement && _ReaderNodeType2 != System.Xml.XmlNodeType.None)
                                {
                                    if (_ReaderNodeType2 == System.Xml.XmlNodeType.Element)
                                    {
                                        if (((object)ThisReader.LocalName == (object)id248_Value))
                                        {
                                            if ((a_1_0) == null) ThisReader.Skip(); else a_1_0.Add(Read241_ValuePoint(true, true));
                                        }
                                        else
                                        {
                                            UnknownNode(null, ThisReader.LocalName); //":Value");
                                        }
                                    }
                                    else
                                    {
                                        UnknownNode(null, ThisReader.LocalName); //":Value");
                                    }
                                    ThisReader.MoveToContent();
                                    CheckReaderCount(ref whileIterations310, ref readerCount310);
                                    _ReaderNodeType2 = ThisReader.NodeType;
                                }
                                ThisReader.ReadEndElement();
                            }
                        }
                    }
                    else
                    {
                        UnknownNode(o, _ReaderLocalName);//":Values");
                    }
                }
                else
                {
                    UnknownNode(o, ThisReader.LocalName);//":Values");
                }
                ThisReader.MoveToContent();
                CheckReaderCount(ref whileIterations309, ref readerCount309);
                _ReaderNodeType1 = ThisReader.NodeType;
            }
            ThisReader.ReadEndElement();
            return o;
        }
        internal protected global::DCSoft.TemperatureChart.ValuePoint Read241_ValuePoint(bool isNullable, bool checkType)
        {
            var ThisReader = this.Reader;
            global::DCSoft.TemperatureChart.ValuePoint o;
            o = new global::DCSoft.TemperatureChart.ValuePoint();
            //bool[] paramsRead = new bool[32];
            while (ThisReader.MoveToNextAttribute())
            {
                string _ReaderLocalName = ThisReader.LocalName;
                if (((object)_ReaderLocalName == (object)id844_VerifiedColorValue))
                {
                    o.@VerifiedColorValue = ThisReader.Value;
                }
                else if (((object)_ReaderLocalName == (object)id845_VerifiedAlignment))
                {
                    o.@VerifiedAlignment = Read119_StringAlignment(ThisReader.Value);
                }
                else if (((object)_ReaderLocalName == (object)id846_TagValue))
                {
                    o.@TagValue = ThisReader.Value;
                }
                else if (((object)_ReaderLocalName == (object)id11_ID))
                {
                    o.@ID = ThisReader.Value;
                }
                else if (((object)_ReaderLocalName == (object)id174_Superscript))
                {
                    o.@Superscript = DCXMLConvert.ToBoolean(ThisReader.Value);
                }
                else if (((object)_ReaderLocalName == (object)id847_SpecifySymbolStyle))
                {
                    o.@SpecifySymbolStyle = Read235_ValuePointSymbolStyle(ThisReader.Value);
                }
                else if (((object)_ReaderLocalName == (object)id848_SymbolOffsetX))
                {
                    o.@SymbolOffsetX = ToSingle(ThisReader.Value);
                }
                else if (((object)_ReaderLocalName == (object)id849_SymbolOffsetY))
                {
                    o.@SymbolOffsetY = ToSingle(ThisReader.Value);
                }
                else if (((object)_ReaderLocalName == (object)id850_SpecifyLanternSymbolStyle))
                {
                    o.@SpecifyLanternSymbolStyle = Read235_ValuePointSymbolStyle(ThisReader.Value);
                }
                else if (((object)_ReaderLocalName == (object)id851_IntCharLantern))
                {
                    o.@IntCharLantern = DCXMLConvert.ToInt32(ThisReader.Value);
                }
                else if (((object)_ReaderLocalName == (object)id852_IntCharSymbol))
                {
                    o.@IntCharSymbol = DCXMLConvert.ToInt32(ThisReader.Value);
                }
                else if (((object)_ReaderLocalName == (object)id307_Link))
                {
                    o.@Link = ThisReader.Value;
                }
                else if (((object)_ReaderLocalName == (object)id853_LinkTarget))
                {
                    o.@LinkTarget = ThisReader.Value;
                }
                else if (((object)_ReaderLocalName == (object)id238_Title))
                {
                    o.@Title = ThisReader.Value;
                }
                else if (((object)_ReaderLocalName == (object)id854_VerticalLine))
                {
                    o.@VerticalLine = Read239_DCTimeLineBooleanValue(ThisReader.Value);
                }
                else if (((object)_ReaderLocalName == (object)id855_UseAdvVerticalStyle))
                {
                    o.@UseAdvVerticalStyle = DCXMLConvert.ToBoolean(ThisReader.Value);
                }
                else if (((object)_ReaderLocalName == (object)id856_UseAdvVerticalStyle2))
                {
                    o.@UseAdvVerticalStyle2 = DCXMLConvert.ToBoolean(ThisReader.Value);
                }
                else if (((object)_ReaderLocalName == (object)id857_Time))
                {
                    o.@Time = ToDateTime(ThisReader.Value);
                }
                else if (((object)_ReaderLocalName == (object)id858_EndTime))
                {
                    o.@EndTime = ToDateTime(ThisReader.Value);
                }
                else if (((object)_ReaderLocalName == (object)id248_Value))
                {
                    o.@Value = ToSingle(ThisReader.Value);
                }
                else if (((object)_ReaderLocalName == (object)id859_LanternValue))
                {
                    o.@LanternValue = ToSingle(ThisReader.Value);
                }
                else if (((object)_ReaderLocalName == (object)id146_Text))
                {
                    o.@Text = ThisReader.Value;
                }
                else if (((object)_ReaderLocalName == (object)id860_TextAlign))
                {
                    o.@TextAlign = Read119_StringAlignment(ThisReader.Value);
                }
                else if (((object)_ReaderLocalName == (object)id143_ColorValue))
                {
                    o.@ColorValue = ThisReader.Value;
                }
                else if (((object)_ReaderLocalName == (object)id766_TextColorValue))
                {
                    o.@TextColorValue = ThisReader.Value;
                }
            }
            ThisReader.MoveToElement();
            if (ThisReader.IsEmptyElement)
            {
                ThisReader.Skip();
                return o;
            }
            ThisReader.ReadStartElement();
            ThisReader.MoveToContent();
            int whileIterations311 = 0;
            int readerCount311 = ReaderCount;
            var _ReaderNodeType1 = ThisReader.NodeType;
            while (_ReaderNodeType1 != System.Xml.XmlNodeType.EndElement && _ReaderNodeType1 != System.Xml.XmlNodeType.None)
            {
                if (_ReaderNodeType1 == System.Xml.XmlNodeType.Element)
                {
                    string _ReaderLocalName = ThisReader.LocalName;
                    if (((object)_ReaderLocalName == (object)id861_Verified))
                    {
                        if (ThisReader.IsEmptyElement)
                        {
                            ThisReader.Skip();
                        }
                        else
                        {
                            o.@Verified = DCXMLConvert.ToBoolean(ThisReader.ReadElementString());
                        }
                    }
                    else if (((object)_ReaderLocalName == (object)id862_VerifiedColor))
                    {
                        o.@VerifiedColor = Read114_Color(true);
                    }
                    else if (((object)_ReaderLocalName == (object)id863_ValueTextTopPadding))
                    {
                        if (ThisReader.IsEmptyElement)
                        {
                            ThisReader.Skip();
                        }
                        else
                        {
                            o.@ValueTextTopPadding = ToSingle(ThisReader.ReadElementString());
                        }
                    }
                    else if (((object)_ReaderLocalName == (object)id142_Font))
                    {
                        o.@Font = Read64_XFontValue(false, true);
                    }
                    else if (((object)_ReaderLocalName == (object)id75_Image))
                    {
                        o.@Image = Read34_XImageValue(false, true);
                    }
                    else if (((object)_ReaderLocalName == (object)id864_CustomImage))
                    {
                        o.@CustomImage = Read34_XImageValue(false, true);
                    }
                    else if (((object)_ReaderLocalName == (object)id865_UpAndDown))
                    {
                        if (ThisReader.IsEmptyElement)
                        {
                            ThisReader.Skip();
                        }
                        else
                        {
                            o.@UpAndDown = Read240_ValuePointUpAndDown(ThisReader.ReadElementString());
                        }
                    }
                    else
                    {
                        UnknownNode(o, _ReaderLocalName);//":Verified, :VerifiedColor, :ValueTextTopPadding, :Font, :Image, :CustomImage, :UpAndDown");
                    }
                }
                else
                {
                    UnknownNode(o, ThisReader.LocalName);//":Verified, :VerifiedColor, :ValueTextTopPadding, :Font, :Image, :CustomImage, :UpAndDown");
                }
                ThisReader.MoveToContent();
                CheckReaderCount(ref whileIterations311, ref readerCount311);
                _ReaderNodeType1 = ThisReader.NodeType;
            }
            ThisReader.ReadEndElement();
            return o;
        }
        internal protected global::DCSoft.TemperatureChart.ValuePointUpAndDown Read240_ValuePointUpAndDown(string s)
        {
            switch (s)
            {
                case @"None": return global::DCSoft.TemperatureChart.ValuePointUpAndDown.@None;
                case @"Up": return global::DCSoft.TemperatureChart.ValuePointUpAndDown.@Up;
                case @"Down": return global::DCSoft.TemperatureChart.ValuePointUpAndDown.@Down;
                default: return (default(global::DCSoft.TemperatureChart.ValuePointUpAndDown));
            }
        }
        internal protected global::DCSoft.TemperatureChart.DCTimeLineBooleanValue Read239_DCTimeLineBooleanValue(string s)
        {
            switch (s)
            {
                case @"Inherit": return global::DCSoft.TemperatureChart.DCTimeLineBooleanValue.@Inherit;
                case @"True": return global::DCSoft.TemperatureChart.DCTimeLineBooleanValue.@True;
                case @"False": return global::DCSoft.TemperatureChart.DCTimeLineBooleanValue.@False;
                default: return (default(global::DCSoft.TemperatureChart.DCTimeLineBooleanValue));
            }
        }
        internal protected global::DCSoft.TemperatureChart.ValuePointSymbolStyle Read235_ValuePointSymbolStyle(string s)
        {

            if (__DCSoft_TemperatureChart_ValuePointSymbolStyle == null)
            {
                lock (_NewDictionaryLockObject)
                {
                    var dic20200818 = new System.Collections.Generic.Dictionary<string, DCSoft.TemperatureChart.ValuePointSymbolStyle>();
                    dic20200818.Add("None", DCSoft.TemperatureChart.ValuePointSymbolStyle.@None);
                    dic20200818.Add("Default", DCSoft.TemperatureChart.ValuePointSymbolStyle.@Default);
                    dic20200818.Add("SolidCicle", DCSoft.TemperatureChart.ValuePointSymbolStyle.@SolidCicle);
                    dic20200818.Add("HollowCicle", DCSoft.TemperatureChart.ValuePointSymbolStyle.@HollowCicle);
                    dic20200818.Add("OpaqueHollowCicle", DCSoft.TemperatureChart.ValuePointSymbolStyle.@OpaqueHollowCicle);
                    dic20200818.Add("Cross", DCSoft.TemperatureChart.ValuePointSymbolStyle.@Cross);
                    dic20200818.Add("Square", DCSoft.TemperatureChart.ValuePointSymbolStyle.@Square);
                    dic20200818.Add("HollowSquare", DCSoft.TemperatureChart.ValuePointSymbolStyle.@HollowSquare);
                    dic20200818.Add("Diamond", DCSoft.TemperatureChart.ValuePointSymbolStyle.@Diamond);
                    dic20200818.Add("HollowDiamond", DCSoft.TemperatureChart.ValuePointSymbolStyle.@HollowDiamond);
                    dic20200818.Add("V", DCSoft.TemperatureChart.ValuePointSymbolStyle.@V);
                    dic20200818.Add("VReversed", DCSoft.TemperatureChart.ValuePointSymbolStyle.@VReversed);
                    dic20200818.Add("SolidTriangle", DCSoft.TemperatureChart.ValuePointSymbolStyle.@SolidTriangle);
                    dic20200818.Add("SolidTriangleReversed", DCSoft.TemperatureChart.ValuePointSymbolStyle.@SolidTriangleReversed);
                    dic20200818.Add("HollowTriangle", DCSoft.TemperatureChart.ValuePointSymbolStyle.@HollowTriangle);
                    dic20200818.Add("HollowTriangleReversed", DCSoft.TemperatureChart.ValuePointSymbolStyle.@HollowTriangleReversed);
                    dic20200818.Add("Character", DCSoft.TemperatureChart.ValuePointSymbolStyle.@Character);
                    dic20200818.Add("CharacterCircle", DCSoft.TemperatureChart.ValuePointSymbolStyle.@CharacterCircle);
                    dic20200818.Add("Custom", DCSoft.TemperatureChart.ValuePointSymbolStyle.@Custom);
                    __DCSoft_TemperatureChart_ValuePointSymbolStyle = dic20200818;
                }
            }
            var result = default(DCSoft.TemperatureChart.ValuePointSymbolStyle);
            if (__DCSoft_TemperatureChart_ValuePointSymbolStyle.TryGetValue(s, out result))
            {
                return result;
            }
            else
            {
                return default(DCSoft.TemperatureChart.ValuePointSymbolStyle);
            }
        }
        private static System.Collections.Generic.Dictionary<string, DCSoft.TemperatureChart.ValuePointSymbolStyle> __DCSoft_TemperatureChart_ValuePointSymbolStyle = null;

        internal protected global::DCSoft.TemperatureChart.DCTimeLineParameter Read238_DCTimeLineParameter(bool isNullable, bool checkType)
        {
            var ThisReader = this.Reader;
            global::DCSoft.TemperatureChart.DCTimeLineParameter o;
            o = new global::DCSoft.TemperatureChart.DCTimeLineParameter();
            //bool[] paramsRead = new bool[2];
            while (ThisReader.MoveToNextAttribute())
            {
                string _ReaderLocalName = ThisReader.LocalName;
                if (((object)_ReaderLocalName == (object)id62_Name))
                {
                    o.@Name = ThisReader.Value;
                }
            }
            ThisReader.MoveToElement();
            if (ThisReader.IsEmptyElement)
            {
                ThisReader.Skip();
                return o;
            }
            ThisReader.ReadStartElement();
            ThisReader.MoveToContent();
            int whileIterations312 = 0;
            int readerCount312 = ReaderCount;
            var _ReaderNodeType1 = ThisReader.NodeType;
            while (_ReaderNodeType1 != System.Xml.XmlNodeType.EndElement && _ReaderNodeType1 != System.Xml.XmlNodeType.None)
            {
                string tmp = null;
                if (ThisReader.NodeType == System.Xml.XmlNodeType.Element)
                {
                    UnknownNode(o, ThisReader.LocalName);
                }
                else if (ThisReader.NodeType == System.Xml.XmlNodeType.Text ||
                ThisReader.NodeType == System.Xml.XmlNodeType.CDATA ||
                ThisReader.NodeType == System.Xml.XmlNodeType.Whitespace ||
                ThisReader.NodeType == System.Xml.XmlNodeType.SignificantWhitespace)
                {
                    tmp = ThisReader.ReadString();// ReadString(tmp, false);
                    o.@Value = tmp;
                }
                else
                {
                    UnknownNode(o, ThisReader.LocalName);
                }
                ThisReader.MoveToContent();
                CheckReaderCount(ref whileIterations312, ref readerCount312);
                _ReaderNodeType1 = ThisReader.NodeType;
            }
            ThisReader.ReadEndElement();
            return o;
        }
        internal protected global::DCSoft.TemperatureChart.TemperatureDocumentConfig Read237_TemperatureDocumentConfig(bool isNullable, bool checkType)
        {
            var ThisReader = this.Reader;
            global::DCSoft.TemperatureChart.TemperatureDocumentConfig o;
            o = new global::DCSoft.TemperatureChart.TemperatureDocumentConfig();
            //            if ((object)(o.@Images) == null) o.@Images = new global::DCSoft.TemperatureChart.DCTimeLineImageList();
            //            global::DCSoft.TemperatureChart.DCTimeLineImageList a_22 = (global::DCSoft.TemperatureChart.DCTimeLineImageList)o.@Images;
            //            if ((object)(o.@Labels) == null) o.@Labels = new global::DCSoft.TemperatureChart.DCTimeLineLabelList();
            //            global::DCSoft.TemperatureChart.DCTimeLineLabelList a_23 = (global::DCSoft.TemperatureChart.DCTimeLineLabelList)o.@Labels;
            //            if ((object)(o.@Zones) == null) o.@Zones = new global::DCSoft.TemperatureChart.TimeLineZoneInfoList();
            //            global::DCSoft.TemperatureChart.TimeLineZoneInfoList a_36 = (global::DCSoft.TemperatureChart.TimeLineZoneInfoList)o.@Zones;
            //            if ((object)(o.@Ticks) == null) o.@Ticks = new global::DCSoft.TemperatureChart.TickInfoList();
            //            global::DCSoft.TemperatureChart.TickInfoList a_37 = (global::DCSoft.TemperatureChart.TickInfoList)o.@Ticks;
            //            if ((object)(o.@HeaderLabels) == null) o.@HeaderLabels = new global::DCSoft.TemperatureChart.HeaderLabelInfoList();
            //            global::DCSoft.TemperatureChart.HeaderLabelInfoList a_58 = (global::DCSoft.TemperatureChart.HeaderLabelInfoList)o.@HeaderLabels;
            //            if ((object)(o.@HeaderLines) == null) o.@HeaderLines = new global::DCSoft.TemperatureChart.TitleLineInfoList();
            //            global::DCSoft.TemperatureChart.TitleLineInfoList a_60 = (global::DCSoft.TemperatureChart.TitleLineInfoList)o.@HeaderLines;
            //            if ((object)(o.@FooterLines) == null) o.@FooterLines = new global::DCSoft.TemperatureChart.TitleLineInfoList();
            //            global::DCSoft.TemperatureChart.TitleLineInfoList a_61 = (global::DCSoft.TemperatureChart.TitleLineInfoList)o.@FooterLines;
            //            if ((object)(o.@YAxisInfos) == null) o.@YAxisInfos = new global::DCSoft.TemperatureChart.YAxisInfoList();
            //            global::DCSoft.TemperatureChart.YAxisInfoList a_62 = (global::DCSoft.TemperatureChart.YAxisInfoList)o.@YAxisInfos;
            //bool[] paramsRead = new bool[63];
            while (ThisReader.MoveToNextAttribute())
            {
                string _ReaderLocalName = ThisReader.LocalName;
                if (((object)_ReaderLocalName == (object)id258_Version))
                {
                    o.@Version = ThisReader.Value;
                }
            }
            ThisReader.MoveToElement();
            if (ThisReader.IsEmptyElement)
            {
                ThisReader.Skip();
                return o;
            }
            ThisReader.ReadStartElement();
            ThisReader.MoveToContent();
            int whileIterations313 = 0;
            int readerCount313 = ReaderCount;
            var _ReaderNodeType1 = ThisReader.NodeType;
            while (_ReaderNodeType1 != System.Xml.XmlNodeType.EndElement && _ReaderNodeType1 != System.Xml.XmlNodeType.None)
            {
                if (_ReaderNodeType1 == System.Xml.XmlNodeType.Element)
                {
                    string _ReaderLocalName = ThisReader.LocalName;
                    if (((object)_ReaderLocalName == (object)id868_BothBlackWhenPrint))
                    {
                        if (ThisReader.IsEmptyElement)
                        {
                            ThisReader.Skip();
                        }
                        else
                        {
                            o.@BothBlackWhenPrint = DCXMLConvert.ToBoolean(ThisReader.ReadElementString());
                        }
                    }
                    else if (((object)_ReaderLocalName == (object)id869_LineWidthZoomRateWhenPrint))
                    {
                        if (ThisReader.IsEmptyElement)
                        {
                            ThisReader.Skip();
                        }
                        else
                        {
                            o.@LineWidthZoomRateWhenPrint = ToSingle(ThisReader.ReadElementString());
                        }
                    }
                    else if (((object)_ReaderLocalName == (object)id870_LinkVisualStyle))
                    {
                        if (ThisReader.IsEmptyElement)
                        {
                            ThisReader.Skip();
                        }
                        else
                        {
                            o.@LinkVisualStyle = Read212_DocumentLinkVisualStyle(ThisReader.ReadElementString());
                        }
                    }
                    else if (((object)_ReaderLocalName == (object)id871_DebugMode))
                    {
                        if (ThisReader.IsEmptyElement)
                        {
                            ThisReader.Skip();
                        }
                        else
                        {
                            o.@DebugMode = DCXMLConvert.ToBoolean(ThisReader.ReadElementString());
                        }
                    }
                    else if (((object)_ReaderLocalName == (object)id872_EditValuePointMode))
                    {
                        if (ThisReader.IsEmptyElement)
                        {
                            ThisReader.Skip();
                        }
                        else
                        {
                            o.@EditValuePointMode = Read213_EditValuePointEventHandleMode(ThisReader.ReadElementString());
                        }
                    }
                    else if (((object)_ReaderLocalName == (object)id873_EnableExtMouseMoveEvent))
                    {
                        if (ThisReader.IsEmptyElement)
                        {
                            ThisReader.Skip();
                        }
                        else
                        {
                            o.@EnableExtMouseMoveEvent = DCXMLConvert.ToBoolean(ThisReader.ReadElementString());
                        }
                    }
                    else if (((object)_ReaderLocalName == (object)id874_EnableDataGridLinearAxisMode))
                    {
                        if (ThisReader.IsEmptyElement)
                        {
                            ThisReader.Skip();
                        }
                        else
                        {
                            o.@EnableDataGridLinearAxisMode = DCXMLConvert.ToBoolean(ThisReader.ReadElementString());
                        }
                    }
                    else if (((object)_ReaderLocalName == (object)id251_Readonly))
                    {
                        if (ThisReader.IsEmptyElement)
                        {
                            ThisReader.Skip();
                        }
                        else
                        {
                            o.@Readonly = DCXMLConvert.ToBoolean(ThisReader.ReadElementString());
                        }
                    }
                    else if (((object)_ReaderLocalName == (object)id875_ExtendDaysForTimeLine))
                    {
                        if (ThisReader.IsEmptyElement)
                        {
                            ThisReader.Skip();
                        }
                        else
                        {
                            o.@ExtendDaysForTimeLine = ToSingle(ThisReader.ReadElementString());
                        }
                    }
                    else if (((object)_ReaderLocalName == (object)id876_IllegalTextEndCharForLinux))
                    {
                        {
                            o.@IllegalTextEndCharForLinux = CacheString(ThisReader.ReadElementString());
                        }
                    }
                    else if (((object)_ReaderLocalName == (object)id877_TitleForToolTip))
                    {
                        {
                            o.@TitleForToolTip = CacheString(ThisReader.ReadElementString());
                        }
                    }
                    else if (((object)_ReaderLocalName == (object)id878_EnableCustomValuePointSymbol))
                    {
                        if (ThisReader.IsEmptyElement)
                        {
                            ThisReader.Skip();
                        }
                        else
                        {
                            o.@EnableCustomValuePointSymbol = DCXMLConvert.ToBoolean(ThisReader.ReadElementString());
                        }
                    }
                    else if (((object)_ReaderLocalName == (object)id879_HeaderLabelLineAlignment))
                    {
                        if (ThisReader.IsEmptyElement)
                        {
                            ThisReader.Skip();
                        }
                        else
                        {
                            o.@HeaderLabelLineAlignment = Read119_StringAlignment(ThisReader.ReadElementString());
                        }
                    }
                    else if (((object)_ReaderLocalName == (object)id880_SelectionMode))
                    {
                        if (ThisReader.IsEmptyElement)
                        {
                            ThisReader.Skip();
                        }
                        else
                        {
                            o.@SelectionMode = Read214_DCTimeLineSelectionMode(ThisReader.ReadElementString());
                        }
                    }
                    else if (((object)_ReaderLocalName == (object)id881_ShowTooltip))
                    {
                        if (ThisReader.IsEmptyElement)
                        {
                            ThisReader.Skip();
                        }
                        else
                        {
                            o.@ShowTooltip = DCXMLConvert.ToBoolean(ThisReader.ReadElementString());
                        }
                    }
                    else if (((object)_ReaderLocalName == (object)id882_AllowUserCollapseZone))
                    {
                        if (ThisReader.IsEmptyElement)
                        {
                            ThisReader.Skip();
                        }
                        else
                        {
                            o.@AllowUserCollapseZone = DCXMLConvert.ToBoolean(ThisReader.ReadElementString());
                        }
                    }
                    else if (((object)_ReaderLocalName == (object)id883_TickUnit))
                    {
                        if (ThisReader.IsEmptyElement)
                        {
                            ThisReader.Skip();
                        }
                        else
                        {
                            o.@TickUnit = Read215_DCTimeUnit(ThisReader.ReadElementString());
                        }
                    }
                    else if (((object)_ReaderLocalName == (object)id884_DataGridTopPadding))
                    {
                        if (ThisReader.IsEmptyElement)
                        {
                            ThisReader.Skip();
                        }
                        else
                        {
                            o.@DataGridTopPadding = ToSingle(ThisReader.ReadElementString());
                        }
                    }
                    else if (((object)_ReaderLocalName == (object)id885_DataGridBottomPadding))
                    {
                        if (ThisReader.IsEmptyElement)
                        {
                            ThisReader.Skip();
                        }
                        else
                        {
                            o.@DataGridBottomPadding = ToSingle(ThisReader.ReadElementString());
                        }
                    }
                    else if (((object)_ReaderLocalName == (object)id886_SQLTextForHeaderLabel))
                    {
                        {
                            o.@SQLTextForHeaderLabel = CacheString(ThisReader.ReadElementString());
                        }
                    }
                    else if (((object)_ReaderLocalName == (object)id887_SpecifyTickWidth))
                    {
                        if (ThisReader.IsEmptyElement)
                        {
                            ThisReader.Skip();
                        }
                        else
                        {
                            o.@SpecifyTickWidth = ToSingle(ThisReader.ReadElementString());
                        }
                    }
                    else if (((object)_ReaderLocalName == (object)id888_Images))
                    {
                        if (true)
                        {//if (!ReadNull()) {
                            if ((ThisReader.IsEmptyElement))
                            {
                                ThisReader.Skip();
                            }
                            else
                            {
                                if ((o.@Images) == null) o.@Images = new global::DCSoft.TemperatureChart.DCTimeLineImageList();
                                global::DCSoft.TemperatureChart.DCTimeLineImageList a_22_0 = o.@Images;
                                ThisReader.ReadStartElement();
                                ThisReader.MoveToContent();
                                int whileIterations314 = 0;
                                int readerCount314 = ReaderCount;
                                var _ReaderNodeType2 = ThisReader.NodeType;
                                while (_ReaderNodeType2 != System.Xml.XmlNodeType.EndElement && _ReaderNodeType2 != System.Xml.XmlNodeType.None)
                                {
                                    if (_ReaderNodeType2 == System.Xml.XmlNodeType.Element)
                                    {
                                        if (((object)ThisReader.LocalName == (object)id75_Image))
                                        {
                                            if ((a_22_0) == null) ThisReader.Skip(); else a_22_0.Add(Read216_DCTimeLineImage(true, true));
                                        }
                                        else
                                        {
                                            UnknownNode(null, ThisReader.LocalName); //":Image");
                                        }
                                    }
                                    else
                                    {
                                        UnknownNode(null, ThisReader.LocalName); //":Image");
                                    }
                                    ThisReader.MoveToContent();
                                    CheckReaderCount(ref whileIterations314, ref readerCount314);
                                    _ReaderNodeType2 = ThisReader.NodeType;
                                }
                                ThisReader.ReadEndElement();
                            }
                        }
                    }
                    else if (((object)_ReaderLocalName == (object)id889_Labels))
                    {
                        if (true)
                        {//if (!ReadNull()) {
                            if ((ThisReader.IsEmptyElement))
                            {
                                ThisReader.Skip();
                            }
                            else
                            {
                                if ((o.@Labels) == null) o.@Labels = new global::DCSoft.TemperatureChart.DCTimeLineLabelList();
                                global::DCSoft.TemperatureChart.DCTimeLineLabelList a_23_0 = o.@Labels;
                                ThisReader.ReadStartElement();
                                ThisReader.MoveToContent();
                                int whileIterations315 = 0;
                                int readerCount315 = ReaderCount;
                                var _ReaderNodeType2 = ThisReader.NodeType;
                                while (_ReaderNodeType2 != System.Xml.XmlNodeType.EndElement && _ReaderNodeType2 != System.Xml.XmlNodeType.None)
                                {
                                    if (_ReaderNodeType2 == System.Xml.XmlNodeType.Element)
                                    {
                                        if (((object)ThisReader.LocalName == (object)id890_Label))
                                        {
                                            if ((a_23_0) == null) ThisReader.Skip(); else a_23_0.Add(Read218_DCTimeLineLabel(true, true));
                                        }
                                        else
                                        {
                                            UnknownNode(null, ThisReader.LocalName); //":Label");
                                        }
                                    }
                                    else
                                    {
                                        UnknownNode(null, ThisReader.LocalName); //":Label");
                                    }
                                    ThisReader.MoveToContent();
                                    CheckReaderCount(ref whileIterations315, ref readerCount315);
                                    _ReaderNodeType2 = ThisReader.NodeType;
                                }
                                ThisReader.ReadEndElement();
                            }
                        }
                    }
                    else if (((object)_ReaderLocalName == (object)id891_PageIndexText))
                    {
                        {
                            o.@PageIndexText = CacheString(ThisReader.ReadElementString());
                        }
                    }
                    else if (((object)_ReaderLocalName == (object)id892_SpecifyStartDate))
                    {
                        {
                            o.@SpecifyStartDate = CacheString(ThisReader.ReadElementString());
                        }
                    }
                    else if (((object)_ReaderLocalName == (object)id893_SpecifyEndDate))
                    {
                        {
                            o.@SpecifyEndDate = CacheString(ThisReader.ReadElementString());
                        }
                    }
                    else if (((object)_ReaderLocalName == (object)id90_PageSettings))
                    {
                        o.@PageSettings = Read219_DocumentPageSettings(false, true);
                    }
                    else if (((object)_ReaderLocalName == (object)id894_FooterDescription))
                    {
                        {
                            o.@FooterDescription = CacheString(ThisReader.ReadElementString());
                        }
                    }
                    else if (((object)_ReaderLocalName == (object)id896_ShowIcon))
                    {
                        if (ThisReader.IsEmptyElement)
                        {
                            ThisReader.Skip();
                        }
                        else
                        {
                            o.@ShowIcon = DCXMLConvert.ToBoolean(ThisReader.ReadElementString());
                        }
                    }
                    else if (((object)_ReaderLocalName == (object)id897_ImagePixelWidth))
                    {
                        if (ThisReader.IsEmptyElement)
                        {
                            ThisReader.Skip();
                        }
                        else
                        {
                            o.@ImagePixelWidth = DCXMLConvert.ToInt32(ThisReader.ReadElementString());
                        }
                    }
                    else if (((object)_ReaderLocalName == (object)id898_ImagePixelHeight))
                    {
                        if (ThisReader.IsEmptyElement)
                        {
                            ThisReader.Skip();
                        }
                        else
                        {
                            o.@ImagePixelHeight = DCXMLConvert.ToInt32(ThisReader.ReadElementString());
                        }
                    }
                    else if (((object)_ReaderLocalName == (object)id899_ShadowPointDetectSeconds))
                    {
                        if (ThisReader.IsEmptyElement)
                        {
                            ThisReader.Skip();
                        }
                        else
                        {
                            o.@ShadowPointDetectSeconds = DCXMLConvert.ToInt32(ThisReader.ReadElementString());
                        }
                    }
                    else if (((object)_ReaderLocalName == (object)id772_GridYSplitNum))
                    {
                        if (ThisReader.IsEmptyElement)
                        {
                            ThisReader.Skip();
                        }
                        else
                        {
                            o.@GridYSplitNum = DCXMLConvert.ToInt32(ThisReader.ReadElementString());
                        }
                    }
                    else if (((object)_ReaderLocalName == (object)id900_GridYSplitInfo))
                    {
                        o.@GridYSplitInfo = Read221_GridYSplitInfo(false, true);
                    }
                    else if (((object)_ReaderLocalName == (object)id901_Zones))
                    {
                        if (true)
                        {//if (!ReadNull()) {
                            if ((ThisReader.IsEmptyElement))
                            {
                                ThisReader.Skip();
                            }
                            else
                            {
                                if ((o.@Zones) == null) o.@Zones = new global::DCSoft.TemperatureChart.TimeLineZoneInfoList();
                                global::DCSoft.TemperatureChart.TimeLineZoneInfoList a_36_0 = o.@Zones;
                                ThisReader.ReadStartElement();
                                ThisReader.MoveToContent();
                                int whileIterations316 = 0;
                                int readerCount316 = ReaderCount;
                                var _ReaderNodeType2 = ThisReader.NodeType;
                                while (_ReaderNodeType2 != System.Xml.XmlNodeType.EndElement && _ReaderNodeType2 != System.Xml.XmlNodeType.None)
                                {
                                    if (_ReaderNodeType2 == System.Xml.XmlNodeType.Element)
                                    {
                                        if (((object)ThisReader.LocalName == (object)id902_Zone))
                                        {
                                            if ((a_36_0) == null) ThisReader.Skip(); else a_36_0.Add(Read223_TimeLineZoneInfo(true, true));
                                        }
                                        else
                                        {
                                            UnknownNode(null, ThisReader.LocalName); //":Zone");
                                        }
                                    }
                                    else
                                    {
                                        UnknownNode(null, ThisReader.LocalName); //":Zone");
                                    }
                                    ThisReader.MoveToContent();
                                    CheckReaderCount(ref whileIterations316, ref readerCount316);
                                    _ReaderNodeType2 = ThisReader.NodeType;
                                }
                                ThisReader.ReadEndElement();
                            }
                        }
                    }
                    else if (((object)_ReaderLocalName == (object)id903_Ticks))
                    {
                        if (true)
                        {//if (!ReadNull()) {
                            if ((ThisReader.IsEmptyElement))
                            {
                                ThisReader.Skip();
                            }
                            else
                            {
                                if ((o.@Ticks) == null) o.@Ticks = new global::DCSoft.TemperatureChart.TickInfoList();
                                global::DCSoft.TemperatureChart.TickInfoList a_37_0 = o.@Ticks;
                                ThisReader.ReadStartElement();
                                ThisReader.MoveToContent();
                                int whileIterations317 = 0;
                                int readerCount317 = ReaderCount;
                                var _ReaderNodeType2 = ThisReader.NodeType;
                                while (_ReaderNodeType2 != System.Xml.XmlNodeType.EndElement && _ReaderNodeType2 != System.Xml.XmlNodeType.None)
                                {
                                    if (_ReaderNodeType2 == System.Xml.XmlNodeType.Element)
                                    {
                                        if (((object)ThisReader.LocalName == (object)id904_Tick))
                                        {
                                            if ((a_37_0) == null) ThisReader.Skip(); else a_37_0.Add(Read222_TickInfo(true, true));
                                        }
                                        else
                                        {
                                            UnknownNode(null, ThisReader.LocalName); //":Tick");
                                        }
                                    }
                                    else
                                    {
                                        UnknownNode(null, ThisReader.LocalName); //":Tick");
                                    }
                                    ThisReader.MoveToContent();
                                    CheckReaderCount(ref whileIterations317, ref readerCount317);
                                    _ReaderNodeType2 = ThisReader.NodeType;
                                }
                                ThisReader.ReadEndElement();
                            }
                        }
                    }
                    else if (((object)_ReaderLocalName == (object)id811_SymbolSize))
                    {
                        if (ThisReader.IsEmptyElement)
                        {
                            ThisReader.Skip();
                        }
                        else
                        {
                            o.@SymbolSize = ToSingle(ThisReader.ReadElementString());
                        }
                    }
                    else if (((object)_ReaderLocalName == (object)id169_FontName))
                    {
                        {
                            o.@FontName = CacheString(ThisReader.ReadElementString());
                        }
                    }
                    else if (((object)_ReaderLocalName == (object)id170_FontSize))
                    {
                        if (ThisReader.IsEmptyElement)
                        {
                            ThisReader.Skip();
                        }
                        else
                        {
                            o.@FontSize = ToSingle(ThisReader.ReadElementString());
                        }
                    }
                    else if (((object)_ReaderLocalName == (object)id905_BigTitleFontSize))
                    {
                        if (ThisReader.IsEmptyElement)
                        {
                            ThisReader.Skip();
                        }
                        else
                        {
                            o.@BigTitleFontSize = ToSingle(ThisReader.ReadElementString());
                        }
                    }
                    else if (((object)_ReaderLocalName == (object)id906_PageIndexFont))
                    {
                        o.@PageIndexFont = Read64_XFontValue(false, true);
                    }
                    else if (((object)_ReaderLocalName == (object)id907_ForeColorValue))
                    {
                        {
                            o.@ForeColorValue = CacheString(ThisReader.ReadElementString());
                        }
                    }
                    else if (((object)_ReaderLocalName == (object)id908_BigVerticalGridLineWidth))
                    {
                        if (ThisReader.IsEmptyElement)
                        {
                            ThisReader.Skip();
                        }
                        else
                        {
                            o.@BigVerticalGridLineWidth = ToSingle(ThisReader.ReadElementString());
                        }
                    }
                    else if (((object)_ReaderLocalName == (object)id909_BigVerticalGridLineColorValue))
                    {
                        {
                            o.@BigVerticalGridLineColorValue = CacheString(ThisReader.ReadElementString());
                        }
                    }
                    else if (((object)_ReaderLocalName == (object)id144_BackColorValue))
                    {
                        {
                            o.@BackColorValue = CacheString(ThisReader.ReadElementString());
                        }
                    }
                    else if (((object)_ReaderLocalName == (object)id910_PageBackColorValue))
                    {
                        {
                            o.@PageBackColorValue = CacheString(ThisReader.ReadElementString());
                        }
                    }
                    else if (((object)_ReaderLocalName == (object)id911_GridLineColorValue))
                    {
                        {
                            o.@GridLineColorValue = CacheString(ThisReader.ReadElementString());
                        }
                    }
                    else if (((object)_ReaderLocalName == (object)id912_GridBackColorValue))
                    {
                        {
                            o.@GridBackColorValue = CacheString(ThisReader.ReadElementString());
                        }
                    }
                    else if (((object)_ReaderLocalName == (object)id913_DateFormatString))
                    {
                        {
                            o.@DateFormatString = CacheString(ThisReader.ReadElementString());
                        }
                    }
                    else if (((object)_ReaderLocalName == (object)id914_DateFormatStringForCrossYear))
                    {
                        {
                            o.@DateFormatStringForCrossYear = CacheString(ThisReader.ReadElementString());
                        }
                    }
                    else if (((object)_ReaderLocalName == (object)id915_DateFormatStringForCrossMonth))
                    {
                        {
                            o.@DateFormatStringForCrossMonth = CacheString(ThisReader.ReadElementString());
                        }
                    }
                    else if (((object)_ReaderLocalName == (object)id916_DateFormatStringForCrossWeek))
                    {
                        {
                            o.@DateFormatStringForCrossWeek = CacheString(ThisReader.ReadElementString());
                        }
                    }
                    else if (((object)_ReaderLocalName == (object)id917_Item))
                    {
                        {
                            o.@DateFormatStringForFirstIndexFirstPage = CacheString(ThisReader.ReadElementString());
                        }
                    }
                    else if (((object)_ReaderLocalName == (object)id918_Item))
                    {
                        {
                            o.@DateFormatStringForFirstIndexOtherPage = CacheString(ThisReader.ReadElementString());
                        }
                    }
                    else if (((object)_ReaderLocalName == (object)id238_Title))
                    {
                        {
                            o.@Title = CacheString(ThisReader.ReadElementString());
                        }
                    }
                    else if (((object)_ReaderLocalName == (object)id919_SpecifyTitleHeight))
                    {
                        if (ThisReader.IsEmptyElement)
                        {
                            ThisReader.Skip();
                        }
                        else
                        {
                            o.@SpecifyTitleHeight = ToSingle(ThisReader.ReadElementString());
                        }
                    }
                    else if (((object)_ReaderLocalName == (object)id920_HeaderLabels))
                    {
                        if (true)
                        {//if (!ReadNull()) {
                            if ((ThisReader.IsEmptyElement))
                            {
                                ThisReader.Skip();
                            }
                            else
                            {
                                if ((o.@HeaderLabels) == null) o.@HeaderLabels = new global::DCSoft.TemperatureChart.HeaderLabelInfoList();
                                global::DCSoft.TemperatureChart.HeaderLabelInfoList a_58_0 = o.@HeaderLabels;
                                ThisReader.ReadStartElement();
                                ThisReader.MoveToContent();
                                int whileIterations318 = 0;
                                int readerCount318 = ReaderCount;
                                var _ReaderNodeType2 = ThisReader.NodeType;
                                while (_ReaderNodeType2 != System.Xml.XmlNodeType.EndElement && _ReaderNodeType2 != System.Xml.XmlNodeType.None)
                                {
                                    if (_ReaderNodeType2 == System.Xml.XmlNodeType.Element)
                                    {
                                        if (((object)ThisReader.LocalName == (object)id890_Label))
                                        {
                                            if ((a_58_0) == null) ThisReader.Skip(); else a_58_0.Add(Read224_HeaderLabelInfo(true, true));
                                        }
                                        else
                                        {
                                            UnknownNode(null, ThisReader.LocalName); //":Label");
                                        }
                                    }
                                    else
                                    {
                                        UnknownNode(null, ThisReader.LocalName); //":Label");
                                    }
                                    ThisReader.MoveToContent();
                                    CheckReaderCount(ref whileIterations318, ref readerCount318);
                                    _ReaderNodeType2 = ThisReader.NodeType;
                                }
                                ThisReader.ReadEndElement();
                            }
                        }
                    }
                    else if (((object)_ReaderLocalName == (object)id921_NumOfDaysInOnePage))
                    {
                        if (ThisReader.IsEmptyElement)
                        {
                            ThisReader.Skip();
                        }
                        else
                        {
                            o.@NumOfDaysInOnePage = DCXMLConvert.ToInt32(ThisReader.ReadElementString());
                        }
                    }
                    else if (((object)_ReaderLocalName == (object)id922_HeaderLines))
                    {
                        if (true)
                        {//if (!ReadNull()) {
                            if ((ThisReader.IsEmptyElement))
                            {
                                ThisReader.Skip();
                            }
                            else
                            {
                                if ((o.@HeaderLines) == null) o.@HeaderLines = new global::DCSoft.TemperatureChart.TitleLineInfoList();
                                global::DCSoft.TemperatureChart.TitleLineInfoList a_60_0 = o.@HeaderLines;
                                ThisReader.ReadStartElement();
                                ThisReader.MoveToContent();
                                int whileIterations319 = 0;
                                int readerCount319 = ReaderCount;
                                var _ReaderNodeType2 = ThisReader.NodeType;
                                while (_ReaderNodeType2 != System.Xml.XmlNodeType.EndElement && _ReaderNodeType2 != System.Xml.XmlNodeType.None)
                                {
                                    if (_ReaderNodeType2 == System.Xml.XmlNodeType.Element)
                                    {
                                        if (((object)ThisReader.LocalName == (object)id923_Line))
                                        {
                                            if ((a_60_0) == null) ThisReader.Skip(); else a_60_0.Add(Read232_TitleLineInfo(true, true));
                                        }
                                        else
                                        {
                                            UnknownNode(null, ThisReader.LocalName); //":Line");
                                        }
                                    }
                                    else
                                    {
                                        UnknownNode(null, ThisReader.LocalName); //":Line");
                                    }
                                    ThisReader.MoveToContent();
                                    CheckReaderCount(ref whileIterations319, ref readerCount319);
                                    _ReaderNodeType2 = ThisReader.NodeType;
                                }
                                ThisReader.ReadEndElement();
                            }
                        }
                    }
                    //
                    else if (((object)_ReaderLocalName == (object)id895_PageTitlePosition))
                    {
                        ThisReader.Skip();
                    }
                    else if (((object)_ReaderLocalName == (object)id924_FooterLines))
                    {
                        if (true)
                        {//if (!ReadNull()) {
                            if ((ThisReader.IsEmptyElement))
                            {
                                ThisReader.Skip();
                            }
                            else
                            {
                                if ((o.@FooterLines) == null) o.@FooterLines = new global::DCSoft.TemperatureChart.TitleLineInfoList();
                                global::DCSoft.TemperatureChart.TitleLineInfoList a_61_0 = o.@FooterLines;
                                ThisReader.ReadStartElement();
                                ThisReader.MoveToContent();
                                int whileIterations320 = 0;
                                int readerCount320 = ReaderCount;
                                var _ReaderNodeType2 = ThisReader.NodeType;
                                while (_ReaderNodeType2 != System.Xml.XmlNodeType.EndElement && _ReaderNodeType2 != System.Xml.XmlNodeType.None)
                                {
                                    if (_ReaderNodeType2 == System.Xml.XmlNodeType.Element)
                                    {
                                        if (((object)ThisReader.LocalName == (object)id923_Line))
                                        {
                                            if ((a_61_0) == null) ThisReader.Skip(); else a_61_0.Add(Read232_TitleLineInfo(true, true));
                                        }
                                        else
                                        {
                                            UnknownNode(null, ThisReader.LocalName); //":Line");
                                        }
                                    }
                                    else
                                    {
                                        UnknownNode(null, ThisReader.LocalName); //":Line");
                                    }
                                    ThisReader.MoveToContent();
                                    CheckReaderCount(ref whileIterations320, ref readerCount320);
                                    _ReaderNodeType2 = ThisReader.NodeType;
                                }
                                ThisReader.ReadEndElement();
                            }
                        }
                    }
                    else if (((object)_ReaderLocalName == (object)id925_YAxisInfos))
                    {
                        if (true)
                        {//if (!ReadNull()) {
                            if ((ThisReader.IsEmptyElement))
                            {
                                ThisReader.Skip();
                            }
                            else
                            {
                                if ((o.@YAxisInfos) == null) o.@YAxisInfos = new global::DCSoft.TemperatureChart.YAxisInfoList();
                                global::DCSoft.TemperatureChart.YAxisInfoList a_62_0 = o.@YAxisInfos;
                                ThisReader.ReadStartElement();
                                ThisReader.MoveToContent();
                                int whileIterations321 = 0;
                                int readerCount321 = ReaderCount;
                                var _ReaderNodeType2 = ThisReader.NodeType;
                                while (_ReaderNodeType2 != System.Xml.XmlNodeType.EndElement && _ReaderNodeType2 != System.Xml.XmlNodeType.None)
                                {
                                    if (_ReaderNodeType2 == System.Xml.XmlNodeType.Element)
                                    {
                                        if (((object)ThisReader.LocalName == (object)id926_YAxis))
                                        {
                                            if ((a_62_0) == null) ThisReader.Skip(); else a_62_0.Add(Read236_YAxisInfo(true, true));
                                        }
                                        else
                                        {
                                            UnknownNode(null, ThisReader.LocalName); //":YAxis");
                                        }
                                    }
                                    else
                                    {
                                        UnknownNode(null, ThisReader.LocalName); //":YAxis");
                                    }
                                    ThisReader.MoveToContent();
                                    CheckReaderCount(ref whileIterations321, ref readerCount321);
                                    _ReaderNodeType2 = ThisReader.NodeType;
                                }
                                ThisReader.ReadEndElement();
                            }
                        }
                    }
                    else
                    {
                        UnknownNode(o, _ReaderLocalName);//":BothBlackWhenPrint, :LineWidthZoomRateWhenPrint, :LinkVisualStyle, :DebugMode, :EditValuePointMode, :EnableExtMouseMoveEvent, :EnableDataGridLinearAxisMode, :Readonly, :ExtendDaysForTimeLine, :IllegalTextEndCharForLinux, :TitleForToolTip, :EnableCustomValuePointSymbol, :HeaderLabelLineAlignment, :SelectionMode, :ShowTooltip, :AllowUserCollapseZone, :TickUnit, :DataGridTopPadding, :DataGridBottomPadding, :SQLTextForHeaderLabel, :SpecifyTickWidth, :Images, :Labels, :PageIndexText, :SpecifyStartDate, :SpecifyEndDate, :PageSettings, :FooterDescription, :PageTitlePosition, :ShowIcon, :ImagePixelWidth, :ImagePixelHeight, :ShadowPointDetectSeconds, :GridYSplitNum, :GridYSplitInfo, :Zones, :Ticks, :SymbolSize, :FontName, :FontSize, :BigTitleFontSize, :PageIndexFont, :ForeColorValue, :BigVerticalGridLineWidth, :BigVerticalGridLineColorValue, :BackColorValue, :PageBackColorValue, :GridLineColorValue, :GridBackColorValue, :DateFormatString, :DateFormatStringForCrossYear, :DateFormatStringForCrossMonth, :DateFormatStringForCrossWeek, :DateFormatStringForFirstIndexFirstPage, :DateFormatStringForFirstIndexOtherPage, :Title, :SpecifyTitleHeight, :HeaderLabels, :NumOfDaysInOnePage, :HeaderLines, :FooterLines, :YAxisInfos");
                    }
                }
                else
                {
                    UnknownNode(o, ThisReader.LocalName);//":BothBlackWhenPrint, :LineWidthZoomRateWhenPrint, :LinkVisualStyle, :DebugMode, :EditValuePointMode, :EnableExtMouseMoveEvent, :EnableDataGridLinearAxisMode, :Readonly, :ExtendDaysForTimeLine, :IllegalTextEndCharForLinux, :TitleForToolTip, :EnableCustomValuePointSymbol, :HeaderLabelLineAlignment, :SelectionMode, :ShowTooltip, :AllowUserCollapseZone, :TickUnit, :DataGridTopPadding, :DataGridBottomPadding, :SQLTextForHeaderLabel, :SpecifyTickWidth, :Images, :Labels, :PageIndexText, :SpecifyStartDate, :SpecifyEndDate, :PageSettings, :FooterDescription, :PageTitlePosition, :ShowIcon, :ImagePixelWidth, :ImagePixelHeight, :ShadowPointDetectSeconds, :GridYSplitNum, :GridYSplitInfo, :Zones, :Ticks, :SymbolSize, :FontName, :FontSize, :BigTitleFontSize, :PageIndexFont, :ForeColorValue, :BigVerticalGridLineWidth, :BigVerticalGridLineColorValue, :BackColorValue, :PageBackColorValue, :GridLineColorValue, :GridBackColorValue, :DateFormatString, :DateFormatStringForCrossYear, :DateFormatStringForCrossMonth, :DateFormatStringForCrossWeek, :DateFormatStringForFirstIndexFirstPage, :DateFormatStringForFirstIndexOtherPage, :Title, :SpecifyTitleHeight, :HeaderLabels, :NumOfDaysInOnePage, :HeaderLines, :FooterLines, :YAxisInfos");
                }
                ThisReader.MoveToContent();
                CheckReaderCount(ref whileIterations313, ref readerCount313);
                _ReaderNodeType1 = ThisReader.NodeType;
            }
            ThisReader.ReadEndElement();
            return o;
        }
        internal protected global::DCSoft.TemperatureChart.YAxisInfo Read236_YAxisInfo(bool isNullable, bool checkType)
        {
            var ThisReader = this.Reader;
            global::DCSoft.TemperatureChart.YAxisInfo o;
            o = new global::DCSoft.TemperatureChart.YAxisInfo();
            //            if ((object)(o.@Scales) == null) o.@Scales = new global::DCSoft.TemperatureChart.YAxisScaleInfoList();
            //            global::DCSoft.TemperatureChart.YAxisScaleInfoList a_63 = (global::DCSoft.TemperatureChart.YAxisScaleInfoList)o.@Scales;
            //bool[] paramsRead = new bool[64];
            while (ThisReader.MoveToNextAttribute())
            {
                string _ReaderLocalName = ThisReader.LocalName;
                if (((object)_ReaderLocalName == (object)id928_MergeIntoLeft))
                {
                    o.@MergeIntoLeft = DCXMLConvert.ToBoolean(ThisReader.Value);
                }
                else if (((object)_ReaderLocalName == (object)id62_Name))
                {
                    o.@Name = ThisReader.Value;
                }
                else if (((object)_ReaderLocalName == (object)id929_HighlightOutofNormalRange))
                {
                    o.@HighlightOutofNormalRange = DCXMLConvert.ToBoolean(ThisReader.Value);
                }
                else if (((object)_ReaderLocalName == (object)id930_InputTimePrecision))
                {
                    o.@InputTimePrecision = Read225_DateTimePrecisionMode(ThisReader.Value);
                }
                else if (((object)_ReaderLocalName == (object)id931_ValuePrecision))
                {
                    o.@ValuePrecision = DCXMLConvert.ToInt32(ThisReader.Value);
                }
                else if (((object)_ReaderLocalName == (object)id932_AllowInterrupt))
                {
                    o.@AllowInterrupt = DCXMLConvert.ToBoolean(ThisReader.Value);
                }
                else if (((object)_ReaderLocalName == (object)id232_LineWidth))
                {
                    o.@LineWidth = DCXMLConvert.ToInt32(ThisReader.Value);
                }
                else if (((object)_ReaderLocalName == (object)id933_LanternValueColorForUpValue))
                {
                    o.@LanternValueColorForUpValue = ThisReader.Value;
                }
                else if (((object)_ReaderLocalName == (object)id934_LanternValueColorForDownValue))
                {
                    o.@LanternValueColorForDownValue = ThisReader.Value;
                }
                else if (((object)_ReaderLocalName == (object)id935_LineStyleForLanternValue))
                {
                    o.@LineStyleForLanternValue = Read43_DashStyle(ThisReader.Value);
                }
                else if (((object)_ReaderLocalName == (object)id811_SymbolSize))
                {
                    o.@SymbolSize = ToSingle(ThisReader.Value);
                }
                else if (((object)_ReaderLocalName == (object)id936_SpecifyTitleWidth))
                {
                    o.@SpecifyTitleWidth = ToSingle(ThisReader.Value);
                }
                else if (((object)_ReaderLocalName == (object)id937_AllowOutofRange))
                {
                    o.@AllowOutofRange = DCXMLConvert.ToBoolean(ThisReader.Value);
                }
                else if (((object)_ReaderLocalName == (object)id938_SeparatorLineVisible))
                {
                    o.@SeparatorLineVisible = DCXMLConvert.ToBoolean(ThisReader.Value);
                }
                else if (((object)_ReaderLocalName == (object)id939_ClickToHide))
                {
                    o.@ClickToHide = DCXMLConvert.ToBoolean(ThisReader.Value);
                }
                else if (((object)_ReaderLocalName == (object)id54_Visible))
                {
                    o.@Visible = DCXMLConvert.ToBoolean(ThisReader.Value);
                }
                else if (((object)_ReaderLocalName == (object)id940_ValueVisible))
                {
                    o.@ValueVisible = DCXMLConvert.ToBoolean(ThisReader.Value);
                }
                else if (((object)_ReaderLocalName == (object)id941_EnableLanternValue))
                {
                    o.@EnableLanternValue = DCXMLConvert.ToBoolean(ThisReader.Value);
                }
                else if (((object)_ReaderLocalName == (object)id942_LanternValueTitle))
                {
                    o.@LanternValueTitle = ThisReader.Value;
                }
                else if (((object)_ReaderLocalName == (object)id293_Style))
                {
                    o.@Style = Read233_YAxisInfoStyle(ThisReader.Value);
                }
                else if (((object)_ReaderLocalName == (object)id943_HollowCovertTargetName))
                {
                    o.@HollowCovertTargetName = ThisReader.Value;
                }
                else if (((object)_ReaderLocalName == (object)id944_ShadowName))
                {
                    o.@ShadowName = ThisReader.Value;
                }
                else if (((object)_ReaderLocalName == (object)id945_TitleValueDispalyFormat))
                {
                    o.@TitleValueDispalyFormat = ThisReader.Value;
                }
                else if (((object)_ReaderLocalName == (object)id946_TitleVisible))
                {
                    o.@TitleVisible = DCXMLConvert.ToBoolean(ThisReader.Value);
                }
                else if (((object)_ReaderLocalName == (object)id238_Title))
                {
                    o.@Title = ThisReader.Value;
                }
                else if (((object)_ReaderLocalName == (object)id947_YSplitNum))
                {
                    o.@YSplitNum = DCXMLConvert.ToInt32(ThisReader.Value);
                }
                else if (((object)_ReaderLocalName == (object)id948_ValueFormatString))
                {
                    o.@ValueFormatString = ThisReader.Value;
                }
                else if (((object)_ReaderLocalName == (object)id949_AlertLineColorValue))
                {
                    o.@AlertLineColorValue = ThisReader.Value;
                }
                else if (((object)_ReaderLocalName == (object)id950_RedLineValue))
                {
                    o.@RedLineValue = ToSingle(ThisReader.Value);
                }
                else if (((object)_ReaderLocalName == (object)id951_RedLineWidth))
                {
                    o.@RedLineWidth = ToSingle(ThisReader.Value);
                }
                else if (((object)_ReaderLocalName == (object)id952_ValueTextBackColorValue))
                {
                    o.@ValueTextBackColorValue = ThisReader.Value;
                }
                else if (((object)_ReaderLocalName == (object)id408_MaxValue))
                {
                    o.@MaxValue = ToSingle(ThisReader.Value);
                }
                else if (((object)_ReaderLocalName == (object)id409_MinValue))
                {
                    o.@MinValue = ToSingle(ThisReader.Value);
                }
                else if (((object)_ReaderLocalName == (object)id953_ShowLegendInRule))
                {
                    o.@ShowLegendInRule = DCXMLConvert.ToBoolean(ThisReader.Value);
                }
                else if (((object)_ReaderLocalName == (object)id954_ShowPointValue))
                {
                    o.@ShowPointValue = DCXMLConvert.ToBoolean(ThisReader.Value);
                }
                else if (((object)_ReaderLocalName == (object)id955_ColorValueForPointValue))
                {
                    o.@ColorValueForPointValue = ThisReader.Value;
                }
                else if (((object)_ReaderLocalName == (object)id956_ColorValueForDownValue))
                {
                    o.@ColorValueForDownValue = ThisReader.Value;
                }
                else if (((object)_ReaderLocalName == (object)id957_ColorValueForUpValue))
                {
                    o.@ColorValueForUpValue = ThisReader.Value;
                }
                else if (((object)_ReaderLocalName == (object)id958_SymbolStyle))
                {
                    o.@SymbolStyle = Read235_ValuePointSymbolStyle(ThisReader.Value);
                }
                else if (((object)_ReaderLocalName == (object)id848_SymbolOffsetX))
                {
                    o.@SymbolOffsetX = ToSingle(ThisReader.Value);
                }
                else if (((object)_ReaderLocalName == (object)id849_SymbolOffsetY))
                {
                    o.@SymbolOffsetY = ToSingle(ThisReader.Value);
                }
                else if (((object)_ReaderLocalName == (object)id852_IntCharSymbol))
                {
                    o.@IntCharSymbol = DCXMLConvert.ToInt32(ThisReader.Value);
                }
                else if (((object)_ReaderLocalName == (object)id959_BottomTitle))
                {
                    o.@BottomTitle = ThisReader.Value;
                }
                else if (((object)_ReaderLocalName == (object)id960_TitleBackColorValue))
                {
                    o.@TitleBackColorValue = ThisReader.Value;
                }
                else if (((object)_ReaderLocalName == (object)id961_HiddenValueTitleBackColorValue))
                {
                    o.@HiddenValueTitleBackColorValue = ThisReader.Value;
                }
                else if (((object)_ReaderLocalName == (object)id962_TitleColorValue))
                {
                    o.@TitleColorValue = ThisReader.Value;
                }
                else if (((object)_ReaderLocalName == (object)id963_SymbolColorValue))
                {
                    o.@SymbolColorValue = ThisReader.Value;
                }
                else if (((object)_ReaderLocalName == (object)id755_DataSourceName))
                {
                    o.@DataSourceName = ThisReader.Value;
                }
                else if (((object)_ReaderLocalName == (object)id964_ValueFieldName))
                {
                    o.@ValueFieldName = ThisReader.Value;
                }
                else if (((object)_ReaderLocalName == (object)id965_LanternValueFieldName))
                {
                    o.@LanternValueFieldName = ThisReader.Value;
                }
                else if (((object)_ReaderLocalName == (object)id850_SpecifyLanternSymbolStyle))
                {
                    o.@SpecifyLanternSymbolStyle = Read235_ValuePointSymbolStyle(ThisReader.Value);
                }
                else if (((object)_ReaderLocalName == (object)id851_IntCharLantern))
                {
                    o.@IntCharLantern = DCXMLConvert.ToInt32(ThisReader.Value);
                }
                else if (((object)_ReaderLocalName == (object)id966_TimeFieldName))
                {
                    o.@TimeFieldName = ThisReader.Value;
                }
            }
            ThisReader.MoveToElement();
            if (ThisReader.IsEmptyElement)
            {
                ThisReader.Skip();
                return o;
            }
            ThisReader.ReadStartElement();
            ThisReader.MoveToContent();
            int whileIterations322 = 0;
            int readerCount322 = ReaderCount;
            var _ReaderNodeType1 = ThisReader.NodeType;
            while (_ReaderNodeType1 != System.Xml.XmlNodeType.EndElement && _ReaderNodeType1 != System.Xml.XmlNodeType.None)
            {
                if (_ReaderNodeType1 == System.Xml.XmlNodeType.Element)
                {
                    string _ReaderLocalName = ThisReader.LocalName;
                    if (((object)_ReaderLocalName == (object)id967_MaxTextDisplayLength))
                    {
                        if (ThisReader.IsEmptyElement)
                        {
                            ThisReader.Skip();
                        }
                        else
                        {
                            o.@MaxTextDisplayLength = ToSingle(ThisReader.ReadElementString());
                        }
                    }
                    else if (((object)_ReaderLocalName == (object)id968_TopPadding))
                    {
                        if (ThisReader.IsEmptyElement)
                        {
                            ThisReader.Skip();
                        }
                        else
                        {
                            o.@TopPadding = ToSingle(ThisReader.ReadElementString());
                        }
                    }
                    else if (((object)_ReaderLocalName == (object)id969_BottomPadding))
                    {
                        if (ThisReader.IsEmptyElement)
                        {
                            ThisReader.Skip();
                        }
                        else
                        {
                            o.@BottomPadding = ToSingle(ThisReader.ReadElementString());
                        }
                    }
                    else if (((object)_ReaderLocalName == (object)id142_Font))
                    {
                        o.@Font = Read64_XFontValue(false, true);
                    }
                    else if (((object)_ReaderLocalName == (object)id970_ValueFont))
                    {
                        o.@ValueFont = Read64_XFontValue(false, true);
                    }
                    else if (((object)_ReaderLocalName == (object)id392_DataSource))
                    {
                        o.@DataSource = Read227_ValuePointDataSourceInfo(false, true);
                    }
                    else if (((object)_ReaderLocalName == (object)id971_ShadowPointVisible))
                    {
                        if (ThisReader.IsEmptyElement)
                        {
                            ThisReader.Skip();
                        }
                        else
                        {
                            o.@ShadowPointVisible = DCXMLConvert.ToBoolean(ThisReader.ReadElementString());
                        }
                    }
                    else if (((object)_ReaderLocalName == (object)id854_VerticalLine))
                    {
                        if (ThisReader.IsEmptyElement)
                        {
                            ThisReader.Skip();
                        }
                        else
                        {
                            o.@VerticalLine = DCXMLConvert.ToBoolean(ThisReader.ReadElementString());
                        }
                    }
                    else if (((object)_ReaderLocalName == (object)id972_RedLinePrintVisible))
                    {
                        if (ThisReader.IsEmptyElement)
                        {
                            ThisReader.Skip();
                        }
                        else
                        {
                            o.@RedLinePrintVisible = DCXMLConvert.ToBoolean(ThisReader.ReadElementString());
                        }
                    }
                    else if (((object)_ReaderLocalName == (object)id973_AbNormalRangeSettings))
                    {
                        o.@AbNormalRangeSettings = Read234_AbNormalRangeSettings(false, true);
                    }
                    else if (((object)_ReaderLocalName == (object)id974_Scales))
                    {
                        if (true)
                        {//if (!ReadNull()) {
                            if ((ThisReader.IsEmptyElement))
                            {
                                ThisReader.Skip();
                            }
                            else
                            {
                                if ((o.@Scales) == null) o.@Scales = new global::DCSoft.TemperatureChart.YAxisScaleInfoList();
                                global::DCSoft.TemperatureChart.YAxisScaleInfoList a_63_0 = o.@Scales;
                                ThisReader.ReadStartElement();
                                ThisReader.MoveToContent();
                                int whileIterations323 = 0;
                                int readerCount323 = ReaderCount;
                                var _ReaderNodeType2 = ThisReader.NodeType;
                                while (_ReaderNodeType2 != System.Xml.XmlNodeType.EndElement && _ReaderNodeType2 != System.Xml.XmlNodeType.None)
                                {
                                    if (_ReaderNodeType2 == System.Xml.XmlNodeType.Element)
                                    {
                                        if (((object)ThisReader.LocalName == (object)id975_Scale))
                                        {
                                            if ((a_63_0) == null) ThisReader.Skip(); else a_63_0.Add(Read231_YAxisScaleInfo(true, true));
                                        }
                                        else
                                        {
                                            UnknownNode(null, ThisReader.LocalName); //":Scale");
                                        }
                                    }
                                    else
                                    {
                                        UnknownNode(null, ThisReader.LocalName); //":Scale");
                                    }
                                    ThisReader.MoveToContent();
                                    CheckReaderCount(ref whileIterations323, ref readerCount323);
                                    _ReaderNodeType2 = ThisReader.NodeType;
                                }
                                ThisReader.ReadEndElement();
                            }
                        }
                    }
                    else
                    {
                        UnknownNode(o, _ReaderLocalName);//":MaxTextDisplayLength, :TopPadding, :BottomPadding, :Font, :ValueFont, :DataSource, :ShadowPointVisible, :VerticalLine, :RedLinePrintVisible, :AbNormalRangeSettings, :Scales");
                    }
                }
                else
                {
                    UnknownNode(o, ThisReader.LocalName);//":MaxTextDisplayLength, :TopPadding, :BottomPadding, :Font, :ValueFont, :DataSource, :ShadowPointVisible, :VerticalLine, :RedLinePrintVisible, :AbNormalRangeSettings, :Scales");
                }
                ThisReader.MoveToContent();
                CheckReaderCount(ref whileIterations322, ref readerCount322);
                _ReaderNodeType1 = ThisReader.NodeType;
            }
            ThisReader.ReadEndElement();
            return o;
        }
        internal protected global::DCSoft.TemperatureChart.YAxisScaleInfo Read231_YAxisScaleInfo(bool isNullable, bool checkType)
        {
            var ThisReader = this.Reader;
            global::DCSoft.TemperatureChart.YAxisScaleInfo o;
            o = new global::DCSoft.TemperatureChart.YAxisScaleInfo();
            //bool[] paramsRead = new bool[4];
            while (ThisReader.MoveToNextAttribute())
            {
                string _ReaderLocalName = ThisReader.LocalName;
                if (((object)_ReaderLocalName == (object)id146_Text))
                {
                    o.@Text = ThisReader.Value;
                }
                else if (((object)_ReaderLocalName == (object)id248_Value))
                {
                    o.@Value = ToSingle(ThisReader.Value);
                }
                else if (((object)_ReaderLocalName == (object)id977_ScaleRate))
                {
                    o.@ScaleRate = ToSingle(ThisReader.Value);
                }
                else if (((object)_ReaderLocalName == (object)id143_ColorValue))
                {
                    o.@ColorValue = ThisReader.Value;
                }
            }
            ThisReader.MoveToElement();
            if (ThisReader.IsEmptyElement)
            {
                ThisReader.Skip();
                return o;
            }
            ThisReader.ReadStartElement();
            ThisReader.MoveToContent();
            int whileIterations324 = 0;
            int readerCount324 = ReaderCount;
            var _ReaderNodeType1 = ThisReader.NodeType;
            while (_ReaderNodeType1 != System.Xml.XmlNodeType.EndElement && _ReaderNodeType1 != System.Xml.XmlNodeType.None)
            {
                if (_ReaderNodeType1 == System.Xml.XmlNodeType.Element)
                {
                    UnknownNode(o, ThisReader.LocalName);
                }
                else
                {
                    UnknownNode(o, ThisReader.LocalName);
                }
                ThisReader.MoveToContent();
                CheckReaderCount(ref whileIterations324, ref readerCount324);
                _ReaderNodeType1 = ThisReader.NodeType;
            }
            ThisReader.ReadEndElement();
            return o;
        }
        internal protected global::DCSoft.TemperatureChart.AbNormalRangeSettings Read234_AbNormalRangeSettings(bool isNullable, bool checkType)
        {
            var ThisReader = this.Reader;
            global::DCSoft.TemperatureChart.AbNormalRangeSettings o;
            o = new global::DCSoft.TemperatureChart.AbNormalRangeSettings();
            //bool[] paramsRead = new bool[6];
            while (ThisReader.MoveToNextAttribute())
            {
                string _ReaderLocalName = ThisReader.LocalName;
                if (((object)_ReaderLocalName == (object)id978_NormalRangeBackColorValue))
                {
                    o.@NormalRangeBackColorValue = ThisReader.Value;
                }
                else if (((object)_ReaderLocalName == (object)id979_OutofNormalRangeBackColorValue))
                {
                    o.@OutofNormalRangeBackColorValue = ThisReader.Value;
                }
                else if (((object)_ReaderLocalName == (object)id980_NormalMaxValue))
                {
                    o.@NormalMaxValue = ToSingle(ThisReader.Value);
                }
                else if (((object)_ReaderLocalName == (object)id981_NormalMinValue))
                {
                    o.@NormalMinValue = ToSingle(ThisReader.Value);
                }
            }
            ThisReader.MoveToElement();
            if (ThisReader.IsEmptyElement)
            {
                ThisReader.Skip();
                return o;
            }
            ThisReader.ReadStartElement();
            ThisReader.MoveToContent();
            int whileIterations325 = 0;
            int readerCount325 = ReaderCount;
            var _ReaderNodeType1 = ThisReader.NodeType;
            while (_ReaderNodeType1 != System.Xml.XmlNodeType.EndElement && _ReaderNodeType1 != System.Xml.XmlNodeType.None)
            {
                if (_ReaderNodeType1 == System.Xml.XmlNodeType.Element)
                {
                    string _ReaderLocalName = ThisReader.LocalName;
                    if (((object)_ReaderLocalName == (object)id982_NormalRangeUpLineStyle))
                    {
                        o.@NormalRangeUpLineStyle = Read192_XPenStyle(false, true);
                    }
                    else if (((object)_ReaderLocalName == (object)id983_NormalRangeDownLineStyle))
                    {
                        o.@NormalRangeDownLineStyle = Read192_XPenStyle(false, true);
                    }
                    else
                    {
                        UnknownNode(o, _ReaderLocalName);//":NormalRangeUpLineStyle, :NormalRangeDownLineStyle");
                    }
                }
                else
                {
                    UnknownNode(o, ThisReader.LocalName);//":NormalRangeUpLineStyle, :NormalRangeDownLineStyle");
                }
                ThisReader.MoveToContent();
                CheckReaderCount(ref whileIterations325, ref readerCount325);
                _ReaderNodeType1 = ThisReader.NodeType;
            }
            ThisReader.ReadEndElement();
            return o;
        }
        internal protected global::DCSoft.TemperatureChart.ValuePointDataSourceInfo Read227_ValuePointDataSourceInfo(bool isNullable, bool checkType)
        {
            var ThisReader = this.Reader;
            global::DCSoft.TemperatureChart.ValuePointDataSourceInfo o;
            o = new global::DCSoft.TemperatureChart.ValuePointDataSourceInfo();
            //bool[] paramsRead = new bool[8];
            while (ThisReader.MoveToNextAttribute())
            {
                string _ReaderLocalName = ThisReader.LocalName;
                if (((object)_ReaderLocalName == (object)id985_FieldNameForID))
                {
                    o.@FieldNameForID = ThisReader.Value;
                }
                else if (((object)_ReaderLocalName == (object)id986_FieldNameForLink))
                {
                    o.@FieldNameForLink = ThisReader.Value;
                }
                else if (((object)_ReaderLocalName == (object)id987_FieldNameForTitle))
                {
                    o.@FieldNameForTitle = ThisReader.Value;
                }
                else if (((object)_ReaderLocalName == (object)id988_FieldNameForTime))
                {
                    o.@FieldNameForTime = ThisReader.Value;
                }
                else if (((object)_ReaderLocalName == (object)id989_FieldNameForValue))
                {
                    o.@FieldNameForValue = ThisReader.Value;
                }
                else if (((object)_ReaderLocalName == (object)id990_FieldNameForLanternValue))
                {
                    o.@FieldNameForLanternValue = ThisReader.Value;
                }
                else if (((object)_ReaderLocalName == (object)id991_FieldNameForText))
                {
                    o.@FieldNameForText = ThisReader.Value;
                }
            }
            ThisReader.MoveToElement();
            if (ThisReader.IsEmptyElement)
            {
                ThisReader.Skip();
                return o;
            }
            ThisReader.ReadStartElement();
            ThisReader.MoveToContent();
            int whileIterations326 = 0;
            int readerCount326 = ReaderCount;
            var _ReaderNodeType1 = ThisReader.NodeType;
            while (_ReaderNodeType1 != System.Xml.XmlNodeType.EndElement && _ReaderNodeType1 != System.Xml.XmlNodeType.None)
            {
                if (_ReaderNodeType1 == System.Xml.XmlNodeType.Element)
                {
                    string _ReaderLocalName = ThisReader.LocalName;
                    if (((object)_ReaderLocalName == (object)id992_SQLText))
                    {
                        {
                            o.@SQLText = CacheString(ThisReader.ReadElementString());
                        }
                    }
                    else
                    {
                        UnknownNode(o, _ReaderLocalName);//":SQLText");
                    }
                }
                else
                {
                    UnknownNode(o, ThisReader.LocalName);//":SQLText");
                }
                ThisReader.MoveToContent();
                CheckReaderCount(ref whileIterations326, ref readerCount326);
                _ReaderNodeType1 = ThisReader.NodeType;
            }
            ThisReader.ReadEndElement();
            return o;
        }
        internal protected global::DCSoft.TemperatureChart.YAxisInfoStyle Read233_YAxisInfoStyle(string s)
        {
            switch (s)
            {
                case @"Value": return global::DCSoft.TemperatureChart.YAxisInfoStyle.@Value;
                case @"Text": return global::DCSoft.TemperatureChart.YAxisInfoStyle.@Text;
                case @"Background": return global::DCSoft.TemperatureChart.YAxisInfoStyle.@Background;
                case @"PartialBackground": return global::DCSoft.TemperatureChart.YAxisInfoStyle.@PartialBackground;
                case @"TextInsideGrid": return global::DCSoft.TemperatureChart.YAxisInfoStyle.@TextInsideGrid;
                default: return (default(global::DCSoft.TemperatureChart.YAxisInfoStyle));
            }
        }
        internal protected global::DCSoft.TemperatureChart.DateTimePrecisionMode Read225_DateTimePrecisionMode(string s)
        {

            if (__DCSoft_TemperatureChart_DateTimePrecisionMode == null)
            {
                lock (_NewDictionaryLockObject)
                {
                    var dic20200818 = new System.Collections.Generic.Dictionary<string, DCSoft.TemperatureChart.DateTimePrecisionMode>();
                    dic20200818.Add("NoLimited", DCSoft.TemperatureChart.DateTimePrecisionMode.@NoLimited);
                    dic20200818.Add("Second", DCSoft.TemperatureChart.DateTimePrecisionMode.@Second);
                    dic20200818.Add("Minute", DCSoft.TemperatureChart.DateTimePrecisionMode.@Minute);
                    dic20200818.Add("Hour", DCSoft.TemperatureChart.DateTimePrecisionMode.@Hour);
                    dic20200818.Add("Day", DCSoft.TemperatureChart.DateTimePrecisionMode.@Day);
                    dic20200818.Add("Month", DCSoft.TemperatureChart.DateTimePrecisionMode.@Month);
                    dic20200818.Add("Year", DCSoft.TemperatureChart.DateTimePrecisionMode.@Year);
                    __DCSoft_TemperatureChart_DateTimePrecisionMode = dic20200818;
                }
            }
            var result = default(DCSoft.TemperatureChart.DateTimePrecisionMode);
            if (__DCSoft_TemperatureChart_DateTimePrecisionMode.TryGetValue(s, out result))
            {
                return result;
            }
            else
            {
                return default(DCSoft.TemperatureChart.DateTimePrecisionMode);
            }
        }
        private static System.Collections.Generic.Dictionary<string, DCSoft.TemperatureChart.DateTimePrecisionMode> __DCSoft_TemperatureChart_DateTimePrecisionMode = null;

        internal protected global::DCSoft.TemperatureChart.TitleLineInfo Read232_TitleLineInfo(bool isNullable, bool checkType)
        {
            var ThisReader = this.Reader;
            global::DCSoft.TemperatureChart.TitleLineInfo o;
            o = new global::DCSoft.TemperatureChart.TitleLineInfo();
            //            if ((object)(o.@Scales) == null) o.@Scales = new global::DCSoft.TemperatureChart.YAxisScaleInfoList();
            //            global::DCSoft.TemperatureChart.YAxisScaleInfoList a_46 = (global::DCSoft.TemperatureChart.YAxisScaleInfoList)o.@Scales;
            //bool[] paramsRead = new bool[47];
            while (ThisReader.MoveToNextAttribute())
            {
                string _ReaderLocalName = ThisReader.LocalName;
                if (((object)_ReaderLocalName == (object)id930_InputTimePrecision))
                {
                    o.@InputTimePrecision = Read225_DateTimePrecisionMode(ThisReader.Value);
                }
                else if (((object)_ReaderLocalName == (object)id62_Name))
                {
                    o.@Name = ThisReader.Value;
                }
                else if (((object)_ReaderLocalName == (object)id670_AutoHeight))
                {
                    o.@AutoHeight = DCXMLConvert.ToBoolean(ThisReader.Value);
                }
                else if (((object)_ReaderLocalName == (object)id994_VisibleWhenNoValuePoint))
                {
                    o.@VisibleWhenNoValuePoint = DCXMLConvert.ToBoolean(ThisReader.Value);
                }
                else if (((object)_ReaderLocalName == (object)id54_Visible))
                {
                    o.@Visible = DCXMLConvert.ToBoolean(ThisReader.Value);
                }
                else if (((object)_ReaderLocalName == (object)id995_BlankDateWhenNoData))
                {
                    o.@BlankDateWhenNoData = DCXMLConvert.ToBoolean(ThisReader.Value);
                }
                else if (((object)_ReaderLocalName == (object)id996_Item))
                {
                    o.@HiddenOnPageViewWhenNoValuePoints = DCXMLConvert.ToBoolean(ThisReader.Value);
                }
                else if (((object)_ReaderLocalName == (object)id481_GroupName))
                {
                    o.@GroupName = ThisReader.Value;
                }
                else if (((object)_ReaderLocalName == (object)id997_AfterOperaDaysFromZero))
                {
                    o.@AfterOperaDaysFromZero = DCXMLConvert.ToBoolean(ThisReader.Value);
                }
                else if (((object)_ReaderLocalName == (object)id998_AfterOperaDaysBeginOne))
                {
                    o.@AfterOperaDaysBeginOne = DCXMLConvert.ToBoolean(ThisReader.Value);
                }
                else if (((object)_ReaderLocalName == (object)id999_OutofNormalRangeTextColorValue))
                {
                    o.@OutofNormalRangeTextColorValue = ThisReader.Value;
                }
                else if (((object)_ReaderLocalName == (object)id980_NormalMaxValue))
                {
                    o.@NormalMaxValue = ToSingle(ThisReader.Value);
                }
                else if (((object)_ReaderLocalName == (object)id981_NormalMinValue))
                {
                    o.@NormalMinValue = ToSingle(ThisReader.Value);
                }
                else if (((object)_ReaderLocalName == (object)id1000_ExtendGridLineType))
                {
                    o.@ExtendGridLineType = Read226_DCExtendGridLineType(ThisReader.Value);
                }
                else if (((object)_ReaderLocalName == (object)id1001_EnableEndTime))
                {
                    o.@EnableEndTime = DCXMLConvert.ToBoolean(ThisReader.Value);
                }
                else if (((object)_ReaderLocalName == (object)id1002_BlockWidth))
                {
                    o.@BlockWidth = ToSingle(ThisReader.Value);
                }
                else if (((object)_ReaderLocalName == (object)id1003_ValueDisplayFormat))
                {
                    o.@ValueDisplayFormat = ThisReader.Value;
                }
                else if (((object)_ReaderLocalName == (object)id1004_LoopTextList))
                {
                    o.@LoopTextList = ThisReader.Value;
                }
                else if (((object)_ReaderLocalName == (object)id936_SpecifyTitleWidth))
                {
                    o.@SpecifyTitleWidth = ToSingle(ThisReader.Value);
                }
                else if (((object)_ReaderLocalName == (object)id238_Title))
                {
                    o.@Title = ThisReader.Value;
                }
                else if (((object)_ReaderLocalName == (object)id1005_PageTitleTexts))
                {
                    o.@PageTitleTexts = ThisReader.Value;
                }
                else if (((object)_ReaderLocalName == (object)id962_TitleColorValue))
                {
                    o.@TitleColorValue = ThisReader.Value;
                }
                else if (((object)_ReaderLocalName == (object)id766_TextColorValue))
                {
                    o.@TextColorValue = ThisReader.Value;
                }
                else if (((object)_ReaderLocalName == (object)id1006_TitleAlign))
                {
                    o.@TitleAlign = Read119_StringAlignment(ThisReader.Value);
                }
                else if (((object)_ReaderLocalName == (object)id1007_ValueAlign))
                {
                    o.@ValueAlign = Read119_StringAlignment(ThisReader.Value);
                }
                else if (((object)_ReaderLocalName == (object)id1008_MaxValueForDayIndex))
                {
                    o.@MaxValueForDayIndex = DCXMLConvert.ToInt32(ThisReader.Value);
                }
                else if (((object)_ReaderLocalName == (object)id1009_CircleText))
                {
                    o.@CircleText = ThisReader.Value;
                }
                else if (((object)_ReaderLocalName == (object)id457_SpecifyHeight))
                {
                    o.@SpecifyHeight = ToSingle(ThisReader.Value);
                }
                else if (((object)_ReaderLocalName == (object)id1010_EndDateKeyword))
                {
                    o.@EndDateKeyword = ThisReader.Value;
                }
                else if (((object)_ReaderLocalName == (object)id1011_StartDate))
                {
                    o.@StartDate = ToDateTime(ThisReader.Value);
                }
                else if (((object)_ReaderLocalName == (object)id1012_StartDateKeyword))
                {
                    o.@StartDateKeyword = ThisReader.Value;
                }
                else if (((object)_ReaderLocalName == (object)id1013_PreserveStartKeywordOrder))
                {
                    o.@PreserveStartKeywordOrder = DCXMLConvert.ToBoolean(ThisReader.Value);
                }
                else if (((object)_ReaderLocalName == (object)id1014_ShowBackColor))
                {
                    o.@ShowBackColor = DCXMLConvert.ToBoolean(ThisReader.Value);
                }
                else if (((object)_ReaderLocalName == (object)id1015_LayoutType))
                {
                    o.@LayoutType = Read228_TitleLineLayoutType(ThisReader.Value);
                }
                else if (((object)_ReaderLocalName == (object)id1016_TickStep))
                {
                    o.@TickStep = DCXMLConvert.ToInt32(ThisReader.Value);
                }
                else if (((object)_ReaderLocalName == (object)id1017_TickLineVisible))
                {
                    o.@TickLineVisible = DCXMLConvert.ToBoolean(ThisReader.Value);
                }
                else if (((object)_ReaderLocalName == (object)id1018_ForceUpWhenPageFirstPoint))
                {
                    o.@ForceUpWhenPageFirstPoint = DCXMLConvert.ToBoolean(ThisReader.Value);
                }
                else if (((object)_ReaderLocalName == (object)id1019_UpAndDownTextType))
                {
                    o.@UpAndDownTextType = Read229_UpAndDownTextType(ThisReader.Value);
                }
                else if (((object)_ReaderLocalName == (object)id318_ValueType))
                {
                    o.@ValueType = Read230_TitleLineValueType(ThisReader.Value);
                }
                else if (((object)_ReaderLocalName == (object)id755_DataSourceName))
                {
                    o.@DataSourceName = ThisReader.Value;
                }
                else if (((object)_ReaderLocalName == (object)id964_ValueFieldName))
                {
                    o.@ValueFieldName = ThisReader.Value;
                }
                else if (((object)_ReaderLocalName == (object)id966_TimeFieldName))
                {
                    o.@TimeFieldName = ThisReader.Value;
                }
            }
            ThisReader.MoveToElement();
            if (ThisReader.IsEmptyElement)
            {
                ThisReader.Skip();
                return o;
            }
            ThisReader.ReadStartElement();
            ThisReader.MoveToContent();
            int whileIterations327 = 0;
            int readerCount327 = ReaderCount;
            var _ReaderNodeType1 = ThisReader.NodeType;
            while (_ReaderNodeType1 != System.Xml.XmlNodeType.EndElement && _ReaderNodeType1 != System.Xml.XmlNodeType.None)
            {
                if (_ReaderNodeType1 == System.Xml.XmlNodeType.Element)
                {
                    string _ReaderLocalName = ThisReader.LocalName;
                    if (((object)_ReaderLocalName == (object)id1020_ValueTextMultiLine))
                    {
                        if (ThisReader.IsEmptyElement)
                        {
                            ThisReader.Skip();
                        }
                        else
                        {
                            o.@ValueTextMultiLine = DCXMLConvert.ToBoolean(ThisReader.ReadElementString());
                        }
                    }
                    else if (((object)_ReaderLocalName == (object)id142_Font))
                    {
                        o.@Font = Read64_XFontValue(false, true);
                    }
                    else if (((object)_ReaderLocalName == (object)id970_ValueFont))
                    {
                        o.@ValueFont = Read64_XFontValue(false, true);
                    }
                    else if (((object)_ReaderLocalName == (object)id392_DataSource))
                    {
                        o.@DataSource = Read227_ValuePointDataSourceInfo(false, true);
                    }
                    else if (((object)_ReaderLocalName == (object)id974_Scales))
                    {
                        if (true)
                        {//if (!ReadNull()) {
                            if ((ThisReader.IsEmptyElement))
                            {
                                ThisReader.Skip();
                            }
                            else
                            {
                                if ((o.@Scales) == null) o.@Scales = new global::DCSoft.TemperatureChart.YAxisScaleInfoList();
                                global::DCSoft.TemperatureChart.YAxisScaleInfoList a_46_0 = o.@Scales;
                                ThisReader.ReadStartElement();
                                ThisReader.MoveToContent();
                                int whileIterations328 = 0;
                                int readerCount328 = ReaderCount;
                                var _ReaderNodeType2 = ThisReader.NodeType;
                                while (_ReaderNodeType2 != System.Xml.XmlNodeType.EndElement && _ReaderNodeType2 != System.Xml.XmlNodeType.None)
                                {
                                    if (_ReaderNodeType2 == System.Xml.XmlNodeType.Element)
                                    {
                                        if (((object)ThisReader.LocalName == (object)id975_Scale))
                                        {
                                            if ((a_46_0) == null) ThisReader.Skip(); else a_46_0.Add(Read231_YAxisScaleInfo(true, true));
                                        }
                                        else
                                        {
                                            UnknownNode(null, ThisReader.LocalName); //":Scale");
                                        }
                                    }
                                    else
                                    {
                                        UnknownNode(null, ThisReader.LocalName); //":Scale");
                                    }
                                    ThisReader.MoveToContent();
                                    CheckReaderCount(ref whileIterations328, ref readerCount328);
                                    _ReaderNodeType2 = ThisReader.NodeType;
                                }
                                ThisReader.ReadEndElement();
                            }
                        }
                    }
                    else
                    {
                        UnknownNode(o, _ReaderLocalName);//":ValueTextMultiLine, :Font, :ValueFont, :DataSource, :Scales");
                    }
                }
                else
                {
                    UnknownNode(o, ThisReader.LocalName);//":ValueTextMultiLine, :Font, :ValueFont, :DataSource, :Scales");
                }
                ThisReader.MoveToContent();
                CheckReaderCount(ref whileIterations327, ref readerCount327);
                _ReaderNodeType1 = ThisReader.NodeType;
            }
            ThisReader.ReadEndElement();
            return o;
        }
        internal protected global::DCSoft.TemperatureChart.TitleLineValueType Read230_TitleLineValueType(string s)
        {

            if (__DCSoft_TemperatureChart_TitleLineValueType == null)
            {
                lock (_NewDictionaryLockObject)
                {
                    var dic20200818 = new System.Collections.Generic.Dictionary<string, DCSoft.TemperatureChart.TitleLineValueType>();
                    dic20200818.Add("NewSerialDate", DCSoft.TemperatureChart.TitleLineValueType.@NewSerialDate);
                    dic20200818.Add("SerialDate", DCSoft.TemperatureChart.TitleLineValueType.@SerialDate);
                    dic20200818.Add("GlobalDayIndex", DCSoft.TemperatureChart.TitleLineValueType.@GlobalDayIndex);
                    dic20200818.Add("InDayIndex", DCSoft.TemperatureChart.TitleLineValueType.@InDayIndex);
                    dic20200818.Add("DayIndex", DCSoft.TemperatureChart.TitleLineValueType.@DayIndex);
                    dic20200818.Add("HourTick", DCSoft.TemperatureChart.TitleLineValueType.@HourTick);
                    dic20200818.Add("Text", DCSoft.TemperatureChart.TitleLineValueType.@Text);
                    dic20200818.Add("Data", DCSoft.TemperatureChart.TitleLineValueType.@Data);
                    dic20200818.Add("TickText", DCSoft.TemperatureChart.TitleLineValueType.@TickText);
                    __DCSoft_TemperatureChart_TitleLineValueType = dic20200818;
                }
            }
            var result = default(DCSoft.TemperatureChart.TitleLineValueType);
            if (__DCSoft_TemperatureChart_TitleLineValueType.TryGetValue(s, out result))
            {
                return result;
            }
            else
            {
                return default(DCSoft.TemperatureChart.TitleLineValueType);
            }
        }
        private static System.Collections.Generic.Dictionary<string, DCSoft.TemperatureChart.TitleLineValueType> __DCSoft_TemperatureChart_TitleLineValueType = null;

        internal protected global::DCSoft.TemperatureChart.UpAndDownTextType Read229_UpAndDownTextType(string s)
        {
            switch (s)
            {
                case @"None": return global::DCSoft.TemperatureChart.UpAndDownTextType.@None;
                case @"ShowByTick": return global::DCSoft.TemperatureChart.UpAndDownTextType.@ShowByTick;
                case @"ShowByText": return global::DCSoft.TemperatureChart.UpAndDownTextType.@ShowByText;
                default: return (default(global::DCSoft.TemperatureChart.UpAndDownTextType));
            }
        }
        internal protected global::DCSoft.TemperatureChart.TitleLineLayoutType Read228_TitleLineLayoutType(string s)
        {

            if (__DCSoft_TemperatureChart_TitleLineLayoutType == null)
            {
                lock (_NewDictionaryLockObject)
                {
                    var dic20200818 = new System.Collections.Generic.Dictionary<string, DCSoft.TemperatureChart.TitleLineLayoutType>();
                    dic20200818.Add("Normal", DCSoft.TemperatureChart.TitleLineLayoutType.@Normal);
                    dic20200818.Add("Free", DCSoft.TemperatureChart.TitleLineLayoutType.@Free);
                    dic20200818.Add("FreeText", DCSoft.TemperatureChart.TitleLineLayoutType.@FreeText);
                    dic20200818.Add("Cascade", DCSoft.TemperatureChart.TitleLineLayoutType.@Cascade);
                    dic20200818.Add("HorizCascade", DCSoft.TemperatureChart.TitleLineLayoutType.@HorizCascade);
                    dic20200818.Add("AutoCascade", DCSoft.TemperatureChart.TitleLineLayoutType.@AutoCascade);
                    dic20200818.Add("Slant", DCSoft.TemperatureChart.TitleLineLayoutType.@Slant);
                    dic20200818.Add("Slant2", DCSoft.TemperatureChart.TitleLineLayoutType.@Slant2);
                    dic20200818.Add("Slant3", DCSoft.TemperatureChart.TitleLineLayoutType.@Slant3);
                    dic20200818.Add("Fraction", DCSoft.TemperatureChart.TitleLineLayoutType.@Fraction);
                    __DCSoft_TemperatureChart_TitleLineLayoutType = dic20200818;
                }
            }
            var result = default(DCSoft.TemperatureChart.TitleLineLayoutType);
            if (__DCSoft_TemperatureChart_TitleLineLayoutType.TryGetValue(s, out result))
            {
                return result;
            }
            else
            {
                return default(DCSoft.TemperatureChart.TitleLineLayoutType);
            }
        }
        private static System.Collections.Generic.Dictionary<string, DCSoft.TemperatureChart.TitleLineLayoutType> __DCSoft_TemperatureChart_TitleLineLayoutType = null;

        internal protected global::DCSoft.TemperatureChart.DCExtendGridLineType Read226_DCExtendGridLineType(string s)
        {
            switch (s)
            {
                case @"None": return global::DCSoft.TemperatureChart.DCExtendGridLineType.@None;
                case @"Above": return global::DCSoft.TemperatureChart.DCExtendGridLineType.@Above;
                case @"Below": return global::DCSoft.TemperatureChart.DCExtendGridLineType.@Below;
                default: return (default(global::DCSoft.TemperatureChart.DCExtendGridLineType));
            }
        }
        internal protected global::DCSoft.TemperatureChart.HeaderLabelInfo Read224_HeaderLabelInfo(bool isNullable, bool checkType)
        {
            var ThisReader = this.Reader;
            global::DCSoft.TemperatureChart.HeaderLabelInfo o;
            o = new global::DCSoft.TemperatureChart.HeaderLabelInfo();
            //bool[] paramsRead = new bool[6];
            while (ThisReader.MoveToNextAttribute())
            {
                string _ReaderLocalName = ThisReader.LocalName;
                if (((object)_ReaderLocalName == (object)id238_Title))
                {
                    o.@Title = ThisReader.Value;
                }
                else if (((object)_ReaderLocalName == (object)id755_DataSourceName))
                {
                    o.@DataSourceName = ThisReader.Value;
                }
                else if (((object)_ReaderLocalName == (object)id964_ValueFieldName))
                {
                    o.@ValueFieldName = ThisReader.Value;
                }
                else if (((object)_ReaderLocalName == (object)id1022_ParameterName))
                {
                    o.@ParameterName = ThisReader.Value;
                }
                else if (((object)_ReaderLocalName == (object)id248_Value))
                {
                    o.@Value = ThisReader.Value;
                }
                else if (((object)_ReaderLocalName == (object)id1023_SeperatorChar))
                {
                    o.@SeperatorChar = ToChar(ThisReader.Value);
                }
            }
            ThisReader.MoveToElement();
            if (ThisReader.IsEmptyElement)
            {
                ThisReader.Skip();
                return o;
            }
            ThisReader.ReadStartElement();
            ThisReader.MoveToContent();
            int whileIterations329 = 0;
            int readerCount329 = ReaderCount;
            var _ReaderNodeType1 = ThisReader.NodeType;
            while (_ReaderNodeType1 != System.Xml.XmlNodeType.EndElement && _ReaderNodeType1 != System.Xml.XmlNodeType.None)
            {
                if (_ReaderNodeType1 == System.Xml.XmlNodeType.Element)
                {
                    UnknownNode(o, ThisReader.LocalName);
                }
                else
                {
                    UnknownNode(o, ThisReader.LocalName);
                }
                ThisReader.MoveToContent();
                CheckReaderCount(ref whileIterations329, ref readerCount329);
                _ReaderNodeType1 = ThisReader.NodeType;
            }
            ThisReader.ReadEndElement();
            return o;
        }
        internal protected global::DCSoft.TemperatureChart.TickInfo Read222_TickInfo(bool isNullable, bool checkType)
        {
            var ThisReader = this.Reader;
            global::DCSoft.TemperatureChart.TickInfo o;
            o = new global::DCSoft.TemperatureChart.TickInfo();
            //bool[] paramsRead = new bool[3];
            while (ThisReader.MoveToNextAttribute())
            {
                string _ReaderLocalName = ThisReader.LocalName;
                if (((object)_ReaderLocalName == (object)id248_Value))
                {
                    o.@Value = ToSingle(ThisReader.Value);
                }
                else if (((object)_ReaderLocalName == (object)id146_Text))
                {
                    o.@Text = ThisReader.Value;
                }
                else if (((object)_ReaderLocalName == (object)id143_ColorValue))
                {
                    o.@ColorValue = ThisReader.Value;
                }
            }
            ThisReader.MoveToElement();
            if (ThisReader.IsEmptyElement)
            {
                ThisReader.Skip();
                return o;
            }
            ThisReader.ReadStartElement();
            ThisReader.MoveToContent();
            int whileIterations330 = 0;
            int readerCount330 = ReaderCount;
            var _ReaderNodeType1 = ThisReader.NodeType;
            while (_ReaderNodeType1 != System.Xml.XmlNodeType.EndElement && _ReaderNodeType1 != System.Xml.XmlNodeType.None)
            {
                if (_ReaderNodeType1 == System.Xml.XmlNodeType.Element)
                {
                    UnknownNode(o, ThisReader.LocalName);
                }
                else
                {
                    UnknownNode(o, ThisReader.LocalName);
                }
                ThisReader.MoveToContent();
                CheckReaderCount(ref whileIterations330, ref readerCount330);
                _ReaderNodeType1 = ThisReader.NodeType;
            }
            ThisReader.ReadEndElement();
            return o;
        }
        internal protected global::DCSoft.TemperatureChart.TimeLineZoneInfo Read223_TimeLineZoneInfo(bool isNullable, bool checkType)
        {
            var ThisReader = this.Reader;
            global::DCSoft.TemperatureChart.TimeLineZoneInfo o;
            o = new global::DCSoft.TemperatureChart.TimeLineZoneInfo();
            //            if ((object)(o.@Ticks) == null) o.@Ticks = new global::DCSoft.TemperatureChart.TickInfoList();
            //            global::DCSoft.TemperatureChart.TickInfoList a_8 = (global::DCSoft.TemperatureChart.TickInfoList)o.@Ticks;
            //bool[] paramsRead = new bool[11];
            while (ThisReader.MoveToNextAttribute())
            {
                string _ReaderLocalName = ThisReader.LocalName;
                if (((object)_ReaderLocalName == (object)id62_Name))
                {
                    o.@Name = ThisReader.Value;
                }
                else if (((object)_ReaderLocalName == (object)id1026_StartTime))
                {
                    o.@StartTime = ToDateTime(ThisReader.Value);
                }
                else if (((object)_ReaderLocalName == (object)id858_EndTime))
                {
                    o.@EndTime = ToDateTime(ThisReader.Value);
                }
                else if (((object)_ReaderLocalName == (object)id1027_AlignToGrid))
                {
                    o.@AlignToGrid = DCXMLConvert.ToBoolean(ThisReader.Value);
                }
                else if (((object)_ReaderLocalName == (object)id303_GridLineStyle))
                {
                    o.@GridLineStyle = Read43_DashStyle(ThisReader.Value);
                }
                else if (((object)_ReaderLocalName == (object)id911_GridLineColorValue))
                {
                    o.@GridLineColorValue = ThisReader.Value;
                }
                else if (((object)_ReaderLocalName == (object)id144_BackColorValue))
                {
                    o.@BackColorValue = ThisReader.Value;
                }
                else if (((object)_ReaderLocalName == (object)id887_SpecifyTickWidth))
                {
                    o.@SpecifyTickWidth = ToSingle(ThisReader.Value);
                }
                else if (((object)_ReaderLocalName == (object)id1028_AutoTickStepSeconds))
                {
                    o.@AutoTickStepSeconds = DCXMLConvert.ToInt32(ThisReader.Value);
                }
                else if (((object)_ReaderLocalName == (object)id1029_AutoTickFormatString))
                {
                    o.@AutoTickFormatString = ThisReader.Value;
                }
            }
            ThisReader.MoveToElement();
            if (ThisReader.IsEmptyElement)
            {
                ThisReader.Skip();
                return o;
            }
            ThisReader.ReadStartElement();
            ThisReader.MoveToContent();
            int whileIterations331 = 0;
            int readerCount331 = ReaderCount;
            var _ReaderNodeType1 = ThisReader.NodeType;
            while (_ReaderNodeType1 != System.Xml.XmlNodeType.EndElement && _ReaderNodeType1 != System.Xml.XmlNodeType.None)
            {
                if (_ReaderNodeType1 == System.Xml.XmlNodeType.Element)
                {
                    string _ReaderLocalName = ThisReader.LocalName;
                    if (((object)_ReaderLocalName == (object)id903_Ticks))
                    {
                        if (true)
                        {//if (!ReadNull()) {
                            if ((ThisReader.IsEmptyElement))
                            {
                                ThisReader.Skip();
                            }
                            else
                            {
                                if ((o.@Ticks) == null) o.@Ticks = new global::DCSoft.TemperatureChart.TickInfoList();
                                global::DCSoft.TemperatureChart.TickInfoList a_8_0 = o.@Ticks;
                                ThisReader.ReadStartElement();
                                ThisReader.MoveToContent();
                                int whileIterations332 = 0;
                                int readerCount332 = ReaderCount;
                                var _ReaderNodeType2 = ThisReader.NodeType;
                                while (_ReaderNodeType2 != System.Xml.XmlNodeType.EndElement && _ReaderNodeType2 != System.Xml.XmlNodeType.None)
                                {
                                    if (_ReaderNodeType2 == System.Xml.XmlNodeType.Element)
                                    {
                                        if (((object)ThisReader.LocalName == (object)id904_Tick))
                                        {
                                            if ((a_8_0) == null) ThisReader.Skip(); else a_8_0.Add(Read222_TickInfo(true, true));
                                        }
                                        else
                                        {
                                            UnknownNode(null, ThisReader.LocalName); //":Tick");
                                        }
                                    }
                                    else
                                    {
                                        UnknownNode(null, ThisReader.LocalName); //":Tick");
                                    }
                                    ThisReader.MoveToContent();
                                    CheckReaderCount(ref whileIterations332, ref readerCount332);
                                    _ReaderNodeType2 = ThisReader.NodeType;
                                }
                                ThisReader.ReadEndElement();
                            }
                        }
                    }
                    else
                    {
                        UnknownNode(o, _ReaderLocalName);//":Ticks");
                    }
                }
                else
                {
                    UnknownNode(o, ThisReader.LocalName);//":Ticks");
                }
                ThisReader.MoveToContent();
                CheckReaderCount(ref whileIterations331, ref readerCount331);
                _ReaderNodeType1 = ThisReader.NodeType;
            }
            ThisReader.ReadEndElement();
            return o;
        }
        internal protected global::DCSoft.TemperatureChart.GridYSplitInfo Read221_GridYSplitInfo(bool isNullable, bool checkType)
        {
            var ThisReader = this.Reader;
            global::DCSoft.TemperatureChart.GridYSplitInfo o;
            o = new global::DCSoft.TemperatureChart.GridYSplitInfo();
            //bool[] paramsRead = new bool[5];
            //while (ThisReader.MoveToNextAttribute()){}
            ThisReader.MoveToElement();
            if (ThisReader.IsEmptyElement)
            {
                ThisReader.Skip();
                return o;
            }
            ThisReader.ReadStartElement();
            ThisReader.MoveToContent();
            int whileIterations333 = 0;
            int readerCount333 = ReaderCount;
            var _ReaderNodeType1 = ThisReader.NodeType;
            while (_ReaderNodeType1 != System.Xml.XmlNodeType.EndElement && _ReaderNodeType1 != System.Xml.XmlNodeType.None)
            {
                if (_ReaderNodeType1 == System.Xml.XmlNodeType.Element)
                {
                    string _ReaderLocalName = ThisReader.LocalName;
                    if (((object)_ReaderLocalName == (object)id772_GridYSplitNum))
                    {
                        if (ThisReader.IsEmptyElement)
                        {
                            ThisReader.Skip();
                        }
                        else
                        {
                            o.@GridYSplitNum = DCXMLConvert.ToInt32(ThisReader.ReadElementString());
                        }
                    }
                    else if (((object)_ReaderLocalName == (object)id1030_GridYSpaceNum))
                    {
                        if (ThisReader.IsEmptyElement)
                        {
                            ThisReader.Skip();
                        }
                        else
                        {
                            o.@GridYSpaceNum = DCXMLConvert.ToInt32(ThisReader.ReadElementString());
                        }
                    }
                    else if (((object)_ReaderLocalName == (object)id1031_GridYSpaceNumForBottomPadding))
                    {
                        if (ThisReader.IsEmptyElement)
                        {
                            ThisReader.Skip();
                        }
                        else
                        {
                            o.@GridYSpaceNumForBottomPadding = DCXMLConvert.ToInt32(ThisReader.ReadElementString());
                        }
                    }
                    else if (((object)_ReaderLocalName == (object)id1032_ThickLineWidth))
                    {
                        if (ThisReader.IsEmptyElement)
                        {
                            ThisReader.Skip();
                        }
                        else
                        {
                            o.@ThickLineWidth = ToSingle(ThisReader.ReadElementString());
                        }
                    }
                    else if (((object)_ReaderLocalName == (object)id1033_ThinLineWidth))
                    {
                        if (ThisReader.IsEmptyElement)
                        {
                            ThisReader.Skip();
                        }
                        else
                        {
                            o.@ThinLineWidth = ToSingle(ThisReader.ReadElementString());
                        }
                    }
                    else
                    {
                        UnknownNode(o, _ReaderLocalName);//":GridYSplitNum, :GridYSpaceNum, :GridYSpaceNumForBottomPadding, :ThickLineWidth, :ThinLineWidth");
                    }
                }
                else
                {
                    UnknownNode(o, ThisReader.LocalName);//":GridYSplitNum, :GridYSpaceNum, :GridYSpaceNumForBottomPadding, :ThickLineWidth, :ThinLineWidth");
                }
                ThisReader.MoveToContent();
                CheckReaderCount(ref whileIterations333, ref readerCount333);
                _ReaderNodeType1 = ThisReader.NodeType;
            }
            ThisReader.ReadEndElement();
            return o;
        }

        internal protected global::DCSoft.TemperatureChart.DocumentPageSettings Read219_DocumentPageSettings(bool isNullable, bool checkType)
        {
            var ThisReader = this.Reader;
            global::DCSoft.TemperatureChart.DocumentPageSettings o;
            o = new global::DCSoft.TemperatureChart.DocumentPageSettings();
            //bool[] paramsRead = new bool[9];
            //while (ThisReader.MoveToNextAttribute()){}
            ThisReader.MoveToElement();
            if (ThisReader.IsEmptyElement)
            {
                ThisReader.Skip();
                return o;
            }
            ThisReader.ReadStartElement();
            ThisReader.MoveToContent();
            int whileIterations334 = 0;
            int readerCount334 = ReaderCount;
            var _ReaderNodeType1 = ThisReader.NodeType;
            while (_ReaderNodeType1 != System.Xml.XmlNodeType.EndElement && _ReaderNodeType1 != System.Xml.XmlNodeType.None)
            {
                if (_ReaderNodeType1 == System.Xml.XmlNodeType.Element)
                {
                    string _ReaderLocalName = ThisReader.LocalName;
                    if (((object)_ReaderLocalName == (object)id1035_PaperSizeName))
                    {
                        {
                            o.@PaperSizeName = CacheString(ThisReader.ReadElementString());
                        }
                    }
                    else if (((object)_ReaderLocalName == (object)id124_PaperWidth))
                    {
                        if (ThisReader.IsEmptyElement)
                        {
                            ThisReader.Skip();
                        }
                        else
                        {
                            o.@PaperWidth = DCXMLConvert.ToInt32(ThisReader.ReadElementString());
                        }
                    }
                    else if (((object)_ReaderLocalName == (object)id126_PaperHeight))
                    {
                        if (ThisReader.IsEmptyElement)
                        {
                            ThisReader.Skip();
                        }
                        else
                        {
                            o.@PaperHeight = DCXMLConvert.ToInt32(ThisReader.ReadElementString());
                        }
                    }
                    else if (((object)_ReaderLocalName == (object)id127_LeftMargin))
                    {
                        if (ThisReader.IsEmptyElement)
                        {
                            ThisReader.Skip();
                        }
                        else
                        {
                            o.@LeftMargin = DCXMLConvert.ToInt32(ThisReader.ReadElementString());
                        }
                    }
                    else if (((object)_ReaderLocalName == (object)id128_TopMargin))
                    {
                        if (ThisReader.IsEmptyElement)
                        {
                            ThisReader.Skip();
                        }
                        else
                        {
                            o.@TopMargin = DCXMLConvert.ToInt32(ThisReader.ReadElementString());
                        }
                    }
                    else if (((object)_ReaderLocalName == (object)id129_RightMargin))
                    {
                        if (ThisReader.IsEmptyElement)
                        {
                            ThisReader.Skip();
                        }
                        else
                        {
                            o.@RightMargin = DCXMLConvert.ToInt32(ThisReader.ReadElementString());
                        }
                    }
                    else if (((object)_ReaderLocalName == (object)id130_BottomMargin))
                    {
                        if (ThisReader.IsEmptyElement)
                        {
                            ThisReader.Skip();
                        }
                        else
                        {
                            o.@BottomMargin = DCXMLConvert.ToInt32(ThisReader.ReadElementString());
                        }
                    }
                    else if (((object)_ReaderLocalName == (object)id131_Landscape))
                    {
                        if (ThisReader.IsEmptyElement)
                        {
                            ThisReader.Skip();
                        }
                        else
                        {
                            o.@Landscape = DCXMLConvert.ToBoolean(ThisReader.ReadElementString());
                        }
                    }
                    else if (((object)_ReaderLocalName == (object)id119_AutoFitPageSize))
                    {
                        if (ThisReader.IsEmptyElement)
                        {
                            ThisReader.Skip();
                        }
                        else
                        {
                            o.@AutoFitPageSize = DCXMLConvert.ToBoolean(ThisReader.ReadElementString());
                        }
                    }
                    else
                    {
                        UnknownNode(o, _ReaderLocalName);//":PaperSizeName, :PaperWidth, :PaperHeight, :LeftMargin, :TopMargin, :RightMargin, :BottomMargin, :Landscape, :AutoFitPageSize");
                    }
                }
                else
                {
                    UnknownNode(o, ThisReader.LocalName);//":PaperSizeName, :PaperWidth, :PaperHeight, :LeftMargin, :TopMargin, :RightMargin, :BottomMargin, :Landscape, :AutoFitPageSize");
                }
                ThisReader.MoveToContent();
                CheckReaderCount(ref whileIterations334, ref readerCount334);
                _ReaderNodeType1 = ThisReader.NodeType;
            }
            ThisReader.ReadEndElement();
            return o;
        }
        internal protected global::DCSoft.TemperatureChart.DCTimeLineLabel Read218_DCTimeLineLabel(bool isNullable, bool checkType)
        {
            var ThisReader = this.Reader;
            global::DCSoft.TemperatureChart.DCTimeLineLabel o;
            o = new global::DCSoft.TemperatureChart.DCTimeLineLabel();
            //bool[] paramsRead = new bool[15];
            while (ThisReader.MoveToNextAttribute())
            {
                string _ReaderLocalName = ThisReader.LocalName;
                if (((object)_ReaderLocalName == (object)id146_Text))
                {
                    o.@Text = ThisReader.Value;
                }
                else if (((object)_ReaderLocalName == (object)id1022_ParameterName))
                {
                    o.@ParameterName = ThisReader.Value;
                }
                else if (((object)_ReaderLocalName == (object)id1037_MultiLine))
                {
                    o.@MultiLine = DCXMLConvert.ToBoolean(ThisReader.Value);
                }
                else if (((object)_ReaderLocalName == (object)id143_ColorValue))
                {
                    o.@ColorValue = ThisReader.Value;
                }
                else if (((object)_ReaderLocalName == (object)id144_BackColorValue))
                {
                    o.@BackColorValue = ThisReader.Value;
                }
                else if (((object)_ReaderLocalName == (object)id469_Alignment))
                {
                    o.@Alignment = Read119_StringAlignment(ThisReader.Value);
                }
                else if (((object)_ReaderLocalName == (object)id1038_LineAlignment))
                {
                    o.@LineAlignment = Read119_StringAlignment(ThisReader.Value);
                }
                else if (((object)_ReaderLocalName == (object)id215_Left))
                {
                    o.@Left = ToSingle(ThisReader.Value);
                }
                else if (((object)_ReaderLocalName == (object)id216_Top))
                {
                    o.@Top = ToSingle(ThisReader.Value);
                }
                else if (((object)_ReaderLocalName == (object)id217_Width))
                {
                    o.@Width = ToSingle(ThisReader.Value);
                }
                else if (((object)_ReaderLocalName == (object)id218_Height))
                {
                    o.@Height = ToSingle(ThisReader.Value);
                }
                else if (((object)_ReaderLocalName == (object)id1039_Item))
                {
                    o.@PositionFixModeForAutoHeightLine = Read217_LabelPositionFixMode(ThisReader.Value);
                }
            }
            ThisReader.MoveToElement();
            if (ThisReader.IsEmptyElement)
            {
                ThisReader.Skip();
                return o;
            }
            ThisReader.ReadStartElement();
            ThisReader.MoveToContent();
            int whileIterations335 = 0;
            int readerCount335 = ReaderCount;
            var _ReaderNodeType1 = ThisReader.NodeType;
            while (_ReaderNodeType1 != System.Xml.XmlNodeType.EndElement && _ReaderNodeType1 != System.Xml.XmlNodeType.None)
            {
                if (_ReaderNodeType1 == System.Xml.XmlNodeType.Element)
                {
                    string _ReaderLocalName = ThisReader.LocalName;
                    if (((object)_ReaderLocalName == (object)id75_Image))
                    {
                        o.@Image = Read34_XImageValue(false, true);
                    }
                    else if (((object)_ReaderLocalName == (object)id1040_ShowBorder))
                    {
                        if (ThisReader.IsEmptyElement)
                        {
                            ThisReader.Skip();
                        }
                        else
                        {
                            o.@ShowBorder = DCXMLConvert.ToBoolean(ThisReader.ReadElementString());
                        }
                    }
                    else if (((object)_ReaderLocalName == (object)id142_Font))
                    {
                        o.@Font = Read64_XFontValue(false, true);
                    }
                    else
                    {
                        UnknownNode(o, _ReaderLocalName);//":Image, :ShowBorder, :Font");
                    }
                }
                else
                {
                    UnknownNode(o, ThisReader.LocalName);//":Image, :ShowBorder, :Font");
                }
                ThisReader.MoveToContent();
                CheckReaderCount(ref whileIterations335, ref readerCount335);
                _ReaderNodeType1 = ThisReader.NodeType;
            }
            ThisReader.ReadEndElement();
            return o;
        }
        internal protected global::DCSoft.TemperatureChart.LabelPositionFixMode Read217_LabelPositionFixMode(string s)
        {
            switch (s)
            {
                case @"None": return global::DCSoft.TemperatureChart.LabelPositionFixMode.@None;
                case @"InsideDataGrid": return global::DCSoft.TemperatureChart.LabelPositionFixMode.@InsideDataGrid;
                case @"InsideAutoHeightLine": return global::DCSoft.TemperatureChart.LabelPositionFixMode.@InsideAutoHeightLine;
                case @"AboveAutoHeightLine": return global::DCSoft.TemperatureChart.LabelPositionFixMode.@AboveAutoHeightLine;
                default: return (default(global::DCSoft.TemperatureChart.LabelPositionFixMode));
            }
        }
        internal protected global::DCSoft.TemperatureChart.DCTimeLineImage Read216_DCTimeLineImage(bool isNullable, bool checkType)
        {
            var ThisReader = this.Reader;
            global::DCSoft.TemperatureChart.DCTimeLineImage o;
            o = new global::DCSoft.TemperatureChart.DCTimeLineImage();
            //bool[] paramsRead = new bool[4];
            while (ThisReader.MoveToNextAttribute())
            {
                string _ReaderLocalName = ThisReader.LocalName;
                if (((object)_ReaderLocalName == (object)id62_Name))
                {
                    o.@Name = ThisReader.Value;
                }
                else if (((object)_ReaderLocalName == (object)id215_Left))
                {
                    o.@Left = ToSingle(ThisReader.Value);
                }
                else if (((object)_ReaderLocalName == (object)id216_Top))
                {
                    o.@Top = ToSingle(ThisReader.Value);
                }
            }
            ThisReader.MoveToElement();
            if (ThisReader.IsEmptyElement)
            {
                ThisReader.Skip();
                return o;
            }
            ThisReader.ReadStartElement();
            ThisReader.MoveToContent();
            int whileIterations336 = 0;
            int readerCount336 = ReaderCount;
            var _ReaderNodeType1 = ThisReader.NodeType;
            while (_ReaderNodeType1 != System.Xml.XmlNodeType.EndElement && _ReaderNodeType1 != System.Xml.XmlNodeType.None)
            {
                if (_ReaderNodeType1 == System.Xml.XmlNodeType.Element)
                {
                    string _ReaderLocalName = ThisReader.LocalName;
                    if (((object)_ReaderLocalName == (object)id75_Image))
                    {
                        o.@Image = Read34_XImageValue(false, true);
                    }
                    else
                    {
                        UnknownNode(o, _ReaderLocalName);//":Image");
                    }
                }
                else
                {
                    UnknownNode(o, ThisReader.LocalName);//":Image");
                }
                ThisReader.MoveToContent();
                CheckReaderCount(ref whileIterations336, ref readerCount336);
                _ReaderNodeType1 = ThisReader.NodeType;
            }
            ThisReader.ReadEndElement();
            return o;
        }
        internal protected global::DCSoft.TemperatureChart.DCTimeUnit Read215_DCTimeUnit(string s)
        {

            if (__DCSoft_TemperatureChart_DCTimeUnit == null)
            {
                lock (_NewDictionaryLockObject)
                {
                    var dic20200818 = new System.Collections.Generic.Dictionary<string, DCSoft.TemperatureChart.DCTimeUnit>();
                    dic20200818.Add("Second", DCSoft.TemperatureChart.DCTimeUnit.@Second);
                    dic20200818.Add("Minute", DCSoft.TemperatureChart.DCTimeUnit.@Minute);
                    dic20200818.Add("Hour", DCSoft.TemperatureChart.DCTimeUnit.@Hour);
                    dic20200818.Add("Day", DCSoft.TemperatureChart.DCTimeUnit.@Day);
                    dic20200818.Add("Week", DCSoft.TemperatureChart.DCTimeUnit.@Week);
                    dic20200818.Add("Month", DCSoft.TemperatureChart.DCTimeUnit.@Month);
                    dic20200818.Add("Year", DCSoft.TemperatureChart.DCTimeUnit.@Year);
                    __DCSoft_TemperatureChart_DCTimeUnit = dic20200818;
                }
            }
            var result = default(DCSoft.TemperatureChart.DCTimeUnit);
            if (__DCSoft_TemperatureChart_DCTimeUnit.TryGetValue(s, out result))
            {
                return result;
            }
            else
            {
                return default(DCSoft.TemperatureChart.DCTimeUnit);
            }
        }
        private static System.Collections.Generic.Dictionary<string, DCSoft.TemperatureChart.DCTimeUnit> __DCSoft_TemperatureChart_DCTimeUnit = null;

        internal protected global::DCSoft.TemperatureChart.DCTimeLineSelectionMode Read214_DCTimeLineSelectionMode(string s)
        {
            switch (s)
            {
                case @"None": return global::DCSoft.TemperatureChart.DCTimeLineSelectionMode.@None;
                case @"SingleSelect": return global::DCSoft.TemperatureChart.DCTimeLineSelectionMode.@SingleSelect;
                case @"MultiSelec": return global::DCSoft.TemperatureChart.DCTimeLineSelectionMode.@MultiSelec;
                default: return (default(global::DCSoft.TemperatureChart.DCTimeLineSelectionMode));
            }
        }
        internal protected global::DCSoft.TemperatureChart.EditValuePointEventHandleMode Read213_EditValuePointEventHandleMode(string s)
        {
            switch (s)
            {
                case @"None": return global::DCSoft.TemperatureChart.EditValuePointEventHandleMode.@None;
                case @"Program": return global::DCSoft.TemperatureChart.EditValuePointEventHandleMode.@Program;
                case @"Silent": return global::DCSoft.TemperatureChart.EditValuePointEventHandleMode.@Silent;
                case @"OwnedUI": return global::DCSoft.TemperatureChart.EditValuePointEventHandleMode.@OwnedUI;
                default: return (default(global::DCSoft.TemperatureChart.EditValuePointEventHandleMode));
            }
        }
        internal protected global::DCSoft.TemperatureChart.DocumentLinkVisualStyle Read212_DocumentLinkVisualStyle(string s)
        {
            switch (s)
            {
                case @"None": return global::DCSoft.TemperatureChart.DocumentLinkVisualStyle.@None;
                case @"Hover": return global::DCSoft.TemperatureChart.DocumentLinkVisualStyle.@Hover;
                case @"Always": return global::DCSoft.TemperatureChart.DocumentLinkVisualStyle.@Always;
                default: return (default(global::DCSoft.TemperatureChart.DocumentLinkVisualStyle));
            }
        }


        string id144_BackColorValue;
        string id594_ContentSource;
        string id825_ItemBorderStyle;
        string id44_ContentReadonly;
        string id902_Zone;
        string id210_PaddingLeft;
        string id785_VerticalXLabel;
        string id829_InitialAngle;
        string id832_PieLabelStyle;
        string id21_DataName;
        string id669_SpecifyPageIndexs;
        string id810_MaxSize;
        string id396_WriteTextBindingPath;
        string id491_HeaderStyle;
        string id231_AlignToGridLine;
        string id18_HiddenPrintWhenEmpty;
        string id451_InsertEmptyPageForNewPage;
        string id399_ValueValidateStyle;
        string id959_BottomTitle;
        string id860_TextAlign;
        string id392_DataSource;
        string id762_DataItems;
        string id816_MaxMinValueStyle;
        string id30_Item;
        string id185_VerticalAlign;
        string id881_ShowTooltip;
        string id147_Repeat;
        string id789_XAxisCaption;
        string id673_SpecifyPageIndexTextList;
        string id826_EllipseStyle;
        string id983_NormalRangeDownLineStyle;
        string id605_StringTag;
        string id241_Author;
        string id158_Index;
        string id69_FileFormat;
        string id584_PrintGrid;
        string id353_XParagraphFlag;
        string id546_ListItems;
        string id791_LabelStyle;
        string id1057_X;
        string id1058_Y;
        string id648_X2;
        string id649_Y2;
        string id946_TitleVisible;
        string id607_EnableEditImageAdditionShape;
        string id243_CreatorIndex;
        string id86_Comment;
        string id53_PrintVisibility;
        string id774_GridLineStyleH;
        string id556_LonelyChecked;
        string id1016_TickStep;
        string id105_AutoChoosePageSize;
        string id660_Caption;
        string id929_HighlightOutofNormalRange;
        string id516_EnableHighlight;
        string id316_DocumentParameter;
        string id237_OldWhitespaceWidth;
        string id140_WatermarkInfo;
        string id704_TemplateDocuments;
        string id266_DepartmentID;
        string id374_XTextFooter;
        string id557_TextInList;
        string id891_PageIndexText;
        string id338_XTextLabelElementBase;
        string id162_BackgroundImage;
        string id728_ContactAction;
        string id794_ColorTheme;
        string id772_GridYSplitNum;
        string id731_PageLabelText;
        string id1010_EndDateKeyword;
        string id153_Bold;
        string id22_CanBeReferenced;
        string id876_IllegalTextEndCharForLinux;
        string id694_TempClassForSer20200817;
        string id619_CustomAdditionShapeContent;
        string id290_DocumentContentStyleContainer;
        string id305_Cursor;
        string id211_PaddingTop;
        string id559_Group;
        string id203_BorderTop;
        string id441_AutoFixFontSizeMode;
        string id885_DataGridBottomPadding;
        string id914_DateFormatStringForCrossYear;
        string id389_AuthorisedUserIDList;
        string id719_XMLViewStateBagItem;
        string id753_DataForSelfCheck;
        string id913_DateFormatString;
        string id259_Description;
        string id45_Expressions;
        string id490_CanSplitByPageLine;
        string id134_RepeatedImageValue;
        string id814_Color2;
        string id1061_DCNameValueItem;
        string id138_ReferenceCount;
        string id723_PageTexts;
        string id653_CheckedUserHistories;
        string id997_AfterOperaDaysFromZero;
        string id117_PaperKind;
        string id120_Copies;
        string id988_FieldNameForTime;
        string id254_KBEntryRangeMask;
        string id191_LayoutAlign;
        string id998_AfterOperaDaysBeginOne;
        string id686_PreviewImageData;
        string id861_Verified;
        string id48_CopySource;
        string id854_VerticalLine;
        string id560_CheckedTime;
        string id506_EventExpressions;
        string id678_ControlType;
        string id391_Enabled;
        string id14_EmitDataFieldName;
        string id795_CustomColorTheme;
        string id930_InputTimePrecision;
        string id312_PermissionLevel;
        string id513_BackgroundText;
        string id831_SliceRelativeDisplacement;
        string id1032_ThickLineWidth;
        string id37_ContentReadonlyExpression;
        string id106_PageIndexsForHideHeaderFooter;
        string id720_XMLValue;
        string id1034_DocumentPageSettings;
        string id918_Item;
        string id489_AllowUserToResizeHeight;
        string id651_LabelAtUp;
        string id99_HeaderFooterDifferentFirstPage;
        string id8_Attributes;
        string id971_ShadowPointVisible;
        string id515_BorderVisible;
        string id590_MinBarWidth;
        string id599_TopicID;
        string id859_LanternValue;
        string id714_SubEntries;
        string id347_XPageInfo;
        string id132_StrictUsePageSize;
        string id1022_ParameterName;
        string id527_SelectedIndex;
        string id289_ImportFooter;
        string id125_DesignerPaperHeight;
        string id999_OutofNormalRangeTextColorValue;
        string id1047_PartitionList;
        string id969_BottomPadding;
        string id470_HoldWholeLine;
        string id956_ColorValueForDownValue;
        string id730_LinkTextForContactAction;
        string id696_KBLibrary;
        string id767_BarOpacity;
        string id606_TransparentColorValue;
        string id317_TypeName;
        string id622_LocalStyle;
        string id711_RangeMask;
        string id638_ShapeEllipseElement;
        string id384_VBScriptItem;
        string id786_GridBorderLineStyle;
        string id968_TopPadding;
        string id309_UserHistoryInfo;
        string id190_Multiline;
        string id444_SlantSplitLineStyle;
        string id295_PrintColor;
        string id827_PieHeight;
        string id563_SpellCode3;
        string id722_LineSize;
        string id135_HorizontalResolution;
        string id724_PageText;
        string id3_StyleIndex;
        string id884_DataGridTopPadding;
        string id776_GridTextHeight;
        string id91_XPageSettings;
        string id422_DataFeedbackInfo;
        string id812_Background;
        string id682_EnableViewState;
        string id127_LeftMargin;
        string id845_VerifiedAlignment;
        string id416_IncludeKeywords;
        string id1029_AutoTickFormatString;
        string id113_Item;
        string id419_RequiredInvalidateFlag;
        string id631_ShapeContentStyleContainer;
        string id60_LocalExcludeKeywords;
        string id907_ForeColorValue;
        string id1004_LoopTextList;
        string id61_DisabledParameterNames;
        string id28_JavaScriptForDoubleClick;
        string id1_TemperatureDocument;
        string id852_IntCharSymbol;
        string id756_DataFieldNameForSeriesName;
        string id687_OptionsPropertyName;
        string id640_ShapeDocumentPage;
        string id925_YAxisInfos;
        string id1051_BrushStyle;
        string id617_PageImages;
        string id270_DocumentProcessState;
        string id339_XTDBarcodeField;
        string id635_ShapeRectangleElement;
        string id658_PrintTextForUnChecked;
        string id551_ValuePath;
        string id616_CompressSaveMode;
        string id291_Default;
        string id146_Text;
        string id394_BindingPath;
        string id232_LineWidth;
        string id975_Scale;
        string id738_ScriptTextForClick;
        string id253_FieldBorderElementWidth;
        string id1002_BlockWidth;
        string id350_XTextCheckBox;
        string id343_XFileBlock;
        string id445_RowSpan;
        string id662_CaptionAlign;
        string id667_CheckBoxVisible;
        string id54_Visible;
        string id889_Labels;
        string id514_ViewEncryptType;
        string id887_SpecifyTickWidth;
        string id717_OwnerID;
        string id532_CustomValueEditorTypeName;
        string id461_PrintPositionInPage;
        string id94_SwapGutter;
        string id684_AllowUserResize;
        string id797_Colors;
        string id239_BindingUserTrack;
        string id636_ShapeZoomInElement;
        string id410_CheckDecimalDigits;
        string id217_Width;
        string id835_EnumLableType;
        string id89_DocumentGraphicsUnit;
        string id488_Item;
        string id233_GridNumInOnePage;
        string id591_VAlign;
        string id926_YAxis;
        string id1031_GridYSpaceNumForBottomPadding;
        string id10_InnerID;
        string id78_GlobalJavaScriptReferences;
        string id4_PreviewTextLength;
        string id838_Config;
        string id853_LinkTarget;
        string id304_GridLineColor;
        string id440_StressBorder;
        string id1006_TitleAlign;
        string id1042_IsMultiSelect;
        string id358_XInputFieldBase;
        string id858_EndTime;
        string id569_NextTarget;
        string id521_AdornTextType;
        string id77_PageContentVersions;
        string id935_LineStyleForLanternValue;
        string id765_ChartDocumentStyle;
        string id346_XMedia;
        string id1063_WhiteSpaceLength;
        string id174_Superscript;
        string id624_Elements;
        string id71_UserHistories;
        string id547_InputFieldListItem;
        string id417_ExcludeKeywords;
        string id95_GutterPosition;
        string id562_SpellCode2;
        string id315_CheckedValue;
        string id483_Item;
        string id500_BackgroundTextColor;
        string id863_ValueTextTopPadding;
        string id989_FieldNameForValue;
        string id729_AttributeNameForContactAction;
        string id614_AdditionShape;
        string id7_EditorVersionString;
        string id335_XSignImage;
        string id228_TextInMiddlePage;
        string id806_EndCap;
        string id413_DateTimeMinValue;
        string id1041_DCTimeLineImage;
        string id27_JavaScriptForClick;
        string id385_ScriptMethodName;
        string id655_CheckboxVisibility;
        string id873_EnableExtMouseMoveEvent;
        string id116_PaperSource;
        string id246_BorderColor;
        string id136_VerticalResolution;
        string id284_MotherTemplateInfo;
        string id611_KeepWidthHeightRate;
        string id50_XElements;
        string id986_FieldNameForLink;
        string id964_ValueFieldName;
        string id976_YAxisScaleInfo;
        string id298_CommentIndex;
        string id609_ValueIndexOfRepeatedImage;
        string id895_PageTitlePosition;
        string id242_CreationTime;
        string id672_ChangePageIndexMidway;
        string id552_Items;
        string id865_UpAndDown;
        string id123_DesignerPaperWidth;
        string id798_ShadeCount;
        string id130_BottomMargin;
        string id74_RepeatedImages;
        string id985_FieldNameForID;
        string id741_DrawContentHandlerName;
        string id269_DocumentType;
        string id382_DescPropertyName;
        string id779_GridBackground;
        string id992_SQLText;
        string id109_OffsetX;
        string id110_OffsetY;
        string id204_BorderRight;
        string id995_BlankDateWhenNoData;
        string id496_BorderElementColorValue;
        string id25_ToolTip;
        string id586_Digitals;
        string id1005_PageTitleTexts;
        string id811_SymbolSize;
        string id429_AutoClean;
        string id207_MarginTop;
        string id455_IsCollapsed;
        string id965_LanternValueFieldName;
        string id11_ID;
        string id181_LetterSpacing;
        string id770_BarGroupSpacing;
        string id911_GridLineColorValue;
        string id561_SpellCode;
        string id40_ValueExpression;
        string id642_AutoSize;
        string id771_ViewStyle;
        string id482_DataSourceRowSpan;
        string id250_SubDocumentSettings;
        string id471_PrintBothBorderWhenJumpPrint;
        string id698_BaseURL;
        string id813_XBrushStyle;
        string id33_AutoHideMode;
        string id1044_IdentyColorARGBValue;
        string id1043_ElementIDForExporting;
        string id862_VerifiedColor;
        string id52_AcceptChildElementTypes2;
        string id517_EnableUserEditInnerValue;
        string id847_SpecifySymbolStyle;
        string id957_ColorValueForUpValue;
        string id188_VertialText;
        string id251_Readonly;
        string id150_XFontValue;
        string id1040_ShowBorder;
        string id454_EnableCollapse;
        string id282_PageContentVersionInfo;
        string id401_ValueName;
        string id273_NumOfPage;
        string id894_FooterDescription;
        string id121_HeaderDistance;
        string id792_RipenessRate;
        string id866_DCTimeLineParameter;
        string id342_XTextBlockElement;
        string id303_GridLineStyle;
        string id367_XTextContentElement;
        string id365_XTextTableRow;
        string id485_PrintCellBorder;
        string id904_Tick;
        string id709_KBEntry;
        string id180_LineSpacingStyle;
        string id1026_StartTime;
        string id187_LeftIndent;
        string id575_EventExpressionInfo;
        string id1027_AlignToGrid;
        string id387_DCContentLockInfo;
        string id937_AllowOutofRange;
        string id700_TemplateSourceFormatString;
        string id963_SymbolColorValue;
        string id70_BodyGridLineOffset;
        string id533_EnableValueEditor;
        string id126_PaperHeight;
        string id703_Entry;
        string id201_BorderLeft;
        string id598_FileContentSource;
        string id277_SubDocumentSpacing;
        string id97_ShowGutterLine;
        string id51_Element;
        string id996_Item;
        string id733_BarcodeType;
        string id898_ImagePixelHeight;
        string id1024_TickInfo;
        string id519_AutoSetSpellCodeInDropdownList;
        string id216_Top;
        string id383_IgnoreChildElements;
        string id1059_SynchroDataMode;
        string id1060_AutoSize;
        string id1060_EmptyWhenNoData;
        string id809_ChartLegendStyle;
        string id240_AuthorID;
        string id595_UpdateState;
        string id379_CopySourceInfo;
        string id869_LineWidthZoomRateWhenPrint;
        string id118_AutoPaperWidth;
        string id680_ValuePropertyName;
        string id755_DataSourceName;
        string id5_TransparentEncryptErrorMessage;
        string id503_TabIndex;
        string id24_ReferencedDataName;
        string id38_VisibleExpression;
        string id801_XPenStyle;
        string id221_ParagraphMultiLevel;
        string id1028_AutoTickStepSeconds;
        string id905_BigTitleFontSize;
        string id1015_LayoutType;
        string id970_ValueFont;
        string id602_InnerRepeatImageIndex;
        string id834_DownleadWidth;
        string id967_MaxTextDisplayLength;
        string id571_AutoUpdateTargetElement;
        string id19_DataFeedback;
        string id345_XTextControlHost;
        string id318_ValueType;
        string id219_PageBreakAfter;
        string id271_DocumentEditState;
        string id236_Printable;
        string id625_ShapeElement;
        string id745_SignErrorMessage;
        string id319_SourceColumn;
        string id447_DesignRowIndex;
        string id725_StrictMatchPageIndex;
        string id637_ShapePolygonElement;
        string id361_XBarcodeField;
        string id301_GridLineType;
        string id425_KeyFieldName;
        string id775_GridTextWidth;
        string id842_Values;
        string id163_Visibility;
        string id768_BarWidth;
        string id449_MirrorViewForCrossPage;
        string id310_Name2;
        string id570_NextTargetID;
        string id176_FixedSpacing;
        string id981_NormalMinValue;
        string id276_HeightInPrintJob;
        string id395_BindingPathForText;
        string id227_DocumentTerminalTextInfo;
        string id141_Type;
        string id206_MarginLeft;
        string id314_Tag;
        string id924_FooterLines;
        string id973_AbNormalRangeSettings;
        string id368_XTextSection;
        string id565_LinkListBindingInfo;
        string id235_LineStyle;
        string id740_ChartPageIndex;
        string id460_PrintedPageIndex;
        string id697_TemplateFileSystemName;
        string id523_EditorControlTypeName;
        string id452_ExpendForDataBinding;
        string id157_PageBorderBackgroundStyle;
        string id823_Item;
        string id701_TemplateFileFormat;
        string id939_ClickToHide;
        string id381_SourcePropertyName;
        string id920_HeaderLabels;
        string id879_HeaderLabelLineAlignment;
        string id524_LinkListBinding;
        string id670_AutoHeight;
        string id65_Parameter;
        string id67_InnerRepeatImageDataList;
        string id941_EnableLanternValue;
        string id934_LanternValueColorForDownValue;
        string id643_Points;
        string id993_TitleLineInfo;
        string id708_Content;
        string id874_EnableDataGridLinearAxisMode;
        string id890_Label;
        string id154_Italic;
        string id1048_XImagePartition;
        string id732_BarcodeStyle2;
        string id183_RTFLineSpacing;
        string id710_CopyListItems;
        string id764_ChartStyle;
        string id324_XPageBreak;
        string id351_XImage;
        string id549_SourceName;
        string id75_Image;
        string id41_UserFlags;
        string id333_XPie;
        string id961_HiddenValueTitleBackColorValue;
        string id950_RedLineValue;
        string id307_Link;
        string id566_ProviderName;
        string id948_ValueFormatString;
        string id377_GridLine;
        string id966_TimeFieldName;
        string id715_ListItemsSource;
        string id782_GridLineOffsetX;
        string id302_GridLineOffsetY;
        string id255_IsTemplate;
        string id695_KB;
        string id721_LineLengthInCM;
        string id705_Document;
        string id431_CommandText;
        string id322_SerializeValue;
        string id1037_MultiLine;
        string id267_DepartmentName;
        string id676_SpecifyPageIndex;
        string id415_RegExpression;
        string id917_Item;
        string id321_ValueTypeFullName;
        string id804_LineJoin;
        string id915_DateFormatStringForCrossMonth;
        string id629_EditMode;
        string id1017_TickLineVisible;
        string id478_Columns;
        string id458_DocumentID;
        string id297_LayoutDirection;
        string id13_TransparentEncryptMode;
        string id151_Size;
        string id870_LinkVisualStyle;
        string id363_XDirectoryField;
        string id87_MeasureMode;
        string id424_FieldName;
        string id892_SpecifyStartDate;
        string id328_XLineBreak;
        string id1053_PointF;
        string id481_GroupName;
        string id539_MultiColumn;
        string id758_DataFieldNameForText;
        string id896_ShowIcon;
        string id621_ShapeDocument;
        string id900_GridYSplitInfo;
        string id43_ContentLock;
        string id79_Reference;
        string id875_ExtendDaysForTimeLine;
        string id20_MaxInputLength;
        string id397_ProcessState;
        string id912_GridBackColorValue;
        string id972_RedLinePrintVisible;
        string id464_ContentLoaded;
        string id370_XTextTableCell;
        string id355_XField;
        string id487_AllowInsertRowDownUseHotKey;
        string id373_XTextHeaderForFirstPage;
        string id778_GridValueFormat;
        string id215_Left;
        string id927_YAxisInfo;
        string id632_ShapeLinesElement;
        string id435_DisplayFormat;
        string id1050_XPartition;
        string id982_NormalRangeUpLineStyle;
        string id329_XTextObjectElement;
        string id530_LastSelectedListItems;
        string id734_ErroeCorrectionLevel;
        string id857_Time;
        string id783_VerticalTextAlign;
        string id336_XCustomShape;
        string id945_TitleValueDispalyFormat;
        string id612_Source;
        string id601_AutoCreate;
        string id707_KBID;
        string id769_BarSpacing;
        string id675_RawPageIndex;
        string id763_DataItem;
        string id212_PaddingRight;
        string id685_SavePreviewImage;
        string id103_DocumentGridLine;
        string id84_BodyText;
        string id592_EditValueInDialog;
        string id62_Name;
        string id331_XPartitionImage;
        string id1049_Url;
        string id208_MarginRight;
        string id851_IntCharLantern;
        string id1025_TimeLineZoneInfo;
        string id936_SpecifyTitleWidth;
        string id504_SpecifyWidth;
        string id279_Locked;
        string id1060_SmartChartMode;
        string id172_UnderlineStyle;
        string id664_ControlStyle;
        string id564_ListIndex;
        string id991_FieldNameForText;
        string id693_Temp20200817;
        string id958_SymbolStyle;
        string id430_ConnectionName;
        string id529_EnableLastSelectedListItems;
        string id175_Subscript;
        string id1014_ShowBackColor;
        string id450_CloneType;
        string id665_Checked;
        string id380_SourceID;
        string id285_FileSystemName;
        string id537_GetValueOrderByTime;
        string id674_SpecifyPageIndexInfo;
        string id112_Watermark;
        string id588_TextAlignment;
        string id752_LastVerifyResult;
        string id683_ViewState;
        string id256_MRID;
        string id868_BothBlackWhenPrint;
        string id390_XDataBinding;
        string id39_PrintVisibilityExpression;
        string id376_XTextBody;
        string id634_ShapeWireLabelElement;
        string id727_ReferencedTopicID;
        string id620_PageImageInfo;
        string id641_ShapeDocumentImagePage;
        string id306_DeleterIndex;
        string id139_ValueIndex;
        string id692_FileContentType;
        string id799_LightCorrectionFactor;
        string id234_GridSpanInCM;
        string id178_SpacingBeforeParagraph;
        string id129_RightMargin;
        string id626_LocalElementStyleMode;
        string id796_ThemeType;
        string id509_UserEditable;
        string id498_BackgroundTextItalic;
        string id947_YSplitNum;
        string id114_Item;
        string id659_CheckAlignLeft;
        string id354_XTextContainerElement;
        string id145_Alpha;
        string id296_PrintBackColor;
        string id580_DisplayLevel;
        string id520_DefaultValueType;
        string id583_ShowGrid;
        string id406_CheckMaxValue;
        string id840_Data;
        string id6_DataEncryptProviderName;
        string id108_ForPOSPrinter;
        string id88_LocalConfig;
        string id104_TerminalText;
        string id921_NumOfDaysInOnePage;
        string id330_XNewMedicalExpression;
        string id472_AllowUserDeleteRow;
        string id897_ImagePixelWidth;
        string id749_UseInnerSignProvider;
        string id32_ValidateStyle;
        string id92_JointPrintNumber;
        string id499_LableUnitTextBold;
        string id257_TimeoutHours;
        string id790_LegendStyle;
        string id428_DCEmitDataSource;
        string id657_PrintTextForChecked;
        string id954_ShowPointValue;
        string id938_SeparatorLineVisible;
        string id681_HostMode;
        string id689_LoopPlay;
        string id456_CompressOwnerLineSpacing;
        string id880_SelectionMode;
        string id476_AllowUserToResizeRows;
        string id258_Version;
        string id465_NumOfRows;
        string id388_OwnerUserID;
        string id93_PrintZoomRate;
        string id849_SymbolOffsetY;
        string id848_SymbolOffsetX;
        string id173_UnderlineColor;
        string id940_ValueVisible;
        string id1007_ValueAlign;
        string id323_XTextLock;
        string id497_TextColor;
        string id308_ContentStyle;
        string id1023_SeperatorChar;
        string id718_ObjectParameter;
        string id443_MoveFocusHotKey;
        string id356_XBean;
        string id82_DocumentContentVersion;
        string id73_ContentStyles;
        string id581_ShowPageIndex;
        string id736_ImageForDown;
        string id867_TemperatureDocumentConfig;
        string id856_UseAdvVerticalStyle2;
        string id403_BinaryLength;
        string id507_UnitText;
        string id200_BorderWidth;
        string id119_AutoFitPageSize;
        string id143_ColorValue;
        string id923_Line;
        string id459_Printed;
        string id650_LabelAtLeft;
        string id541_MultiSelect;
        string id668_VisualStyle;
        string id748_SignTime;
        string id953_ShowLegendInRule;
        string id57_ScriptText;
        string id412_DateTimeMaxValue;
        string id501_BorderTextPosition;
        string id746_DefaultSignMode;
        string id171_EmphasisMark;
        string id492_EndingLineBreak;
        string id469_Alignment;
        string id404_MaxLength;
        string id805_StartCap;
        string id819_ChartDataItem;
        string id49_EventTemplateName;
        string id597_SaveLinkedContent;
        string id448_DesignColIndex;
        string id759_DataFieldNameForValue;
        string id671_PageIndexFix;
        string id229_MinHeightInCMUnit;
        string id83_Info;
        string id260_LicenseText;
        string id59_JavaScriptTextForWebClient;
        string id1055_IsCustomFill;
        string id903_Ticks;
        string id275_StartPositionInPringJob;
        string id334_XChart;
        string id408_MaxValue;
        string id666_DefaultCheckedForValueBinding;
        string id47_ScriptItems;
        string id194_CharacterCircle;
        string id544_ListValueFormatString;
        string id299_ProtectType;
        string id538_EditStyle;
        string id446_ColSpan;
        string id58_ScriptLanguage;
        string id268_DocumentFormat;
        string id952_ValueTextBackColorValue;
        string id1035_PaperSizeName;
        string id909_BigVerticalGridLineColorValue;
        string id263_LastPrintTime;
        string id36_ElementIDForEditableDependent;
        string id362_XAccountingNumber;
        string id280_NewPage;
        string id349_XTextRadioBox;
        string id202_BorderBottom;
        string id615_AdditionShapeFixSize;
        string id167_BackgroundRepeat;
        string id398_AutoUpdate;
        string id124_PaperWidth;
        string id386_DomExpression;
        string id864_CustomImage;
        string id652_HyperlinkInfo;
        string id750_SignRange;
        string id230_DCGridLineInfo;
        string id822_SymbolType;
        string id679_TypeFullName;
        string id214_Zoom;
        string id337_XTextButton;
        string id582_AutoExitEditMode;
        string id411_MaxDecimalDigits;
        string id100_SwapLeftRightMargin;
        string id161_BackgroundStyle;
        string id645_SmoothZoomIn;
        string id111_PrinterName;
        string id76_MotherTemplate;
        string id542_DynamicListItems;
        string id248_Value;
        string id855_UseAdvVerticalStyle;
        string id1046_DivCharForMultiMode;
        string id72_History;
        string id808_ChartLabelStyle;
        string id531_FieldSettings;
        string id222_ParagraphOutlineLevel;
        string id213_PaddingBottom;
        string id261_LastModifiedTime;
        string id128_TopMargin;
        string id788_ChartCaptionStyle;
        string id572_AutoSetFirstItems;
        string id493_StartBorderText;
        string id477_ShowCellNoneBorder;
        string id169_FontName;
        string id300_TitleLevel;
        string id262_EditMinute;
        string id475_AllowUserToResizeColumns;
        string id654_Requried;
        string id577_Target;
        string id526_EditorActiveMode;
        string id189_RightToLeft;
        string id1019_UpAndDownTextType;
        string id933_LanternValueColorForUpValue;
        string id978_NormalRangeBackColorValue;
        string id803_DashCap;
        string id777_GroupGridLine;
        string id737_ImageForMouseOver;
        string id133_XImageValue;
        string id984_ValuePointDataSourceInfo;
        string id888_Images;
        string id841_DocumentData;
        string id34_ValueBinding;
        string id473_AllowUserInsertRow;
        string id142_Font;
        string id567_UserFlag;
        string id977_ScaleRate;
        string id608_EnableRepeatedImage;
        string id883_TickUnit;
        string id540_RepulsionForGroup;
        string id249_DocumentInfo;
        string id192_RoundRadio;
        string id198_BorderBottomColor;
        string id787_YAxisCaptions;
        string id702_KBEntries;
        string id744_SignMessage;
        string id878_EnableCustomValuePointSymbol;
        string id218_Height;
        string id960_TitleBackColorValue;
        string id402_Required;
        string id274_UseLanguage2;
        string id1030_GridYSpaceNum;
        string id846_TagValue;
        string id107_PageBorderBackground;
        string id807_MiterLimit;
        string id12_EncryptContent;
        string id843_ValuePoint;
        string id155_Underline;
        string id522_CustomAdornText;
        string id484_GenerateByValueBingding;
        string id238_Title;
        string id644_ZoomInRate;
        string id281_BorderColorValue;
        string id164_BackgroundPosition;
        string id1038_LineAlignment;
        string id747_SignProviderName;
        string id427_KeyFeildDataSourcePath;
        string id149_Angle;
        string id364_XInputField;
        string id910_PageBackColorValue;
        string id196_BorderTopColor;
        string id573_ValueFormater;
        string id46_Expression;
        string id699_ListItemsSourceFormatString;
        string id278_AllowSave;
        string id56_WebClientHtmlText;
        string id193_Rotate;
        string id1012_StartDateKeyword;
        string id962_TitleColorValue;
        string id348_XTextCheckBoxElementBase;
        string id757_DataFieldNameForGroupName;
        string id131_Landscape;
        string id205_BorderSpacing;
        string id29_PropertyExpressions;
        string id1054_IsSelect;
        string id9_Attribute;
        string id182_LineSpacing;
        string id1018_ForceUpWhenPageFirstPoint;
        string id199_BorderStyle;
        string id495_BorderElementColor;
        string id166_BackgroundPositionY;
        string id165_BackgroundPositionX;
        string id833_DownleadLength;
        string id453_ForeColorValueForCollapsed;
        string id357_XContentLinkField;
        string id293_Style;
        string id1001_EnableEndTime;
        string id292_Styles;
        string id893_SpecifyEndDate;
        string id433_FieldsForDesign;
        string id1020_ValueTextMultiLine;
        string id534_ShowFormButton;
        string id220_PageBreakBefore;
        string id754_ImageData;
        string id987_FieldNameForTitle;
        string id23_BringoutToSave;
        string id943_HollowCovertTargetName;
        string id42_EnablePermission;
        string id751_SignUserID;
        string id525_EnableFieldTextColor;
        string id604_LinkInfo;
        string id836_PieDataItem;
        string id510_SelectedSpellCode;
        string id423_TableName;
        string id223_VisibleInDirectory;
        string id596_ReplaceUpdateMode;
        string id548_ListSourceInfo;
        string id824_PieDocumentStyle;
        string id850_SpecifyLanternSymbolStyle;
        string id593_ExpressionStyle;
        string id122_FooterDistance;
        string id409_MinValue;
        string id462_ImportUserTrack;
        string id663_AutoHeightForMultiline;
        string id980_NormalMaxValue;
        string id438_TabStop;
        string id287_ImportPageSettings;
        string id341_XTextLabelElement;
        string id955_ColorValueForPointValue;
        string id800_XColorValue;
        string id554_ListItem;
        string id327_XBookMark;
        string id739_CommandName;
        string id170_FontSize;
        string id96_GutterStyle;
        string id550_DisplayPath;
        string id405_MinLength;
        string id177_SpacingAfterParagraph;
        string id545_ListValueSeparatorChar;
        string id1003_ValueDisplayFormat;
        string id899_ShadowPointDetectSeconds;
        string id432_ParameterStyle;
        string id928_MergeIntoLeft;
        string id325_XTextTableColumn;
        string id195_BorderLeftColor;
        string id974_Scales;
        string id90_PageSettings;
        string id101_SpecifyDuplex;
        string id148_DensityForRepeat;
        string id372_XTextFooterForFirstPage;
        string id639_ShapeContainerElement;
        string id837_TemperatureDocument;
        string id690_EnableMediaContextMenu;
        string id742_SignUserName;
        string id568_IsRoot;
        string id511_InnerValue;
        string id288_ImportHeader;
        string id951_RedLineWidth;
        string id420_PropertyExpressionInfo;
        string id512_PrintBackgroundText;
        string id1052_RatioToPointFsList;
        string id463_DelayLoadWhenExpand;
        string id115_EditTimeBackgroundImage;
        string id426_KeyFieldValue;
        string id781_Thickness;
        string id872_EditValuePointMode;
        string id378_SpecifyFixedLineHeight;
        string id844_VerifiedColorValue;
        string id17_LimitedInputChars;
        string id1008_MaxValueForDayIndex;
        string id340_NewBarcode;
        string id579_TargetPropertyName;
        string id508_LabelText;
        string id15_EmitDataSource;
        string id55_Deleteable;
        string id828_PieOpacity;
        string id418_CustomMessage;
        string id656_PrintVisibilityWhenUnchecked;
        string id802_DashStyle;
        string id466_NumOfColumns;
        string id613_SaveContentInFile;
        string id436_DCEmitDataSourceFieldInfo;
        string id1062_WhitespaceCount;
        string id871_DebugMode;
        string id1056_ImgBase64ForCustomFill;
        string id468_DataForReValueBinding;
        string id906_PageIndexFont;
        string id98_EnableHeaderFooter;
        string id760_DataFieldNameForLink;
        string id942_LanternValueTitle;
        string id691_PlayerUIMode;
        string id64_Parameters;
        string id817_GridStep;
        string id574_NoneText;
        string id137_ImageDataBase64String;
        string id66_DetectRepeatImageForSave;
        string id990_FieldNameForLanternValue;
        string id677_DelayLoadControl;
        string id264_AuthorName;
        string id578_CustomTargetName;
        string id1011_StartDate;
        string id1013_PreserveStartKeywordOrder;
        string id407_CheckMinValue;
        string id320_DefaultValue;
        string id332_XTemperatureChart;
        string id558_Text2;
        string id882_AllowUserCollapseZone;
        string id931_ValuePrecision;
        string id630_DefaultFont;
        string id821_TipText;
        string id589_ShowText;
        string id1021_HeaderLabelInfo;
        string id815_LeftSide;
        string id2_Item;
        string id393_FormatString;
        string id414_Range;
        string id226_BorderRange;
        string id536_InputFieldSettings;
        string id265_AuthorPermissionLevel;
        string id908_BigVerticalGridLineWidth;
        string id712_ParentID;
        string id252_ShowHeaderBottomLine;
        string id901_Zones;
        string id224_ParagraphListStyle;
        string id160_BackgroundColor2;
        string id437_EnabledTransprentEncrypt;
        string id535_FormButtonStyle;
        string id85_Comments;
        string id633_ShapeLineElement;
        string id587_BarcodeStyle;
        string id618_SmoothZoom;
        string id949_AlertLineColorValue;
        string id994_VisibleWhenNoValuePoint;
        string id168_Color;
        string id467_AllowReBindingDataSource;
        string id245_ForeColor;
        string id359_XTextShapeInputFieldElement;
        string id661_CaptionFlowLayout;
        string id623_Resizeable;
        string id713_EntryTemplateContent;
        string id766_TextColorValue;
        string id366_XTextTable;
        string id474_Item;
        string id1039_Item;
        string id726_Item;
        string id439_BorderPrintedWhenJumpPrint;
        string id1036_DCTimeLineLabel;
        string id311_SavedTime;
        string id486_PrintCellBackground;
        string id479_SubfieldMode;
        string id944_ShadowName;
        string id244_BackColor;
        string id1000_ExtendGridLineType;
        string id247_XAttribute;
        string id480_SubfieldNumber;
        string id553_BufferItems;
        string id922_HeaderLines;
        string id400_Level;
        string id932_AllowInterrupt;
        string id421_AllowChainReaction;
        string id184_Align;
        string id743_SignClientName;
        string id360_XMedicalExpressionField;
        string id156_Strikeout;
        string id179_LayoutGridHeight;
        string id761_DataFieldNameForTipText;
        string id610_Alt;
        string id780_AxisCompress;
        string id457_SpecifyHeight;
        string id283_PageIndex;
        string id735_PrintAsText;
        string id628_AutoZoomFontSize;
        string id294_DocumentContentStyle;
        string id979_OutofNormalRangeBackColorValue;
        string id555_EntryID;
        string id102_PowerDocumentGridLine;
        string id716_OwnerLevel;
        string id877_TitleForToolTip;
        string id313_ClientName;
        string id26_AcceptTab;
        string id375_XTextHeader;
        string id371_XTextDocumentContentElement;
        string id886_SQLTextForHeaderLabel;
        string id1009_CircleText;
        string id434_Field;
        string id369_XTextSubDocument;
        string id16_AutoFixTextMode;
        string id627_TextBackColorString;
        string id31_EnableValueValidate;
        string id505_DefaultEventExpression;
        string id646_X1;
        string id647_Y1;
        string id543_ListSource;
        string id784_HorizontalTextAlign;
        string id502_FastInputMode;
        string id35_DefaultValueForValueBinding;
        string id528_DefaultSelectedIndexs;
        string id152_Unit;
        string id159_BackgroundColor;
        string id1045_IsIdentyPartition;
        string id344_HorizontalLine;
        string id352_XTextEOFElement;
        string id1033_ThinLineWidth;
        string id919_SpecifyTitleHeight;
        string id830_DrawingStyle;
        string id603_ZOrderStyle;
        string id286_Format;
        string id68_FileName;
        string id442_AutoFixFontSize;
        string id81_SpecialTag;
        string id916_DateFormatStringForCrossWeek;
        string id63_SerializeParameterValue;
        string id773_CustomColorThemeH;
        string id706_KBTemplateDocument;
        string id186_FirstLineIndent;
        string id818_TickTextList;
        string id793_BarBorderPen;
        string id600_ResetListIndexFlag;
        string id820_SeriesName;
        string id518_ShowInputFieldStateTag;
        string id225_DefaultValuePropertyNames;
        string id272_Operator;
        string id494_EndBorderText;
        string id585_UnitMode;
        string id326_XString;
        string id576_EventName;
        string id688_CsMediaPlayer;
        string id80_GlobalJavaScript;
        string id197_BorderRightColor;
        string id839_Datas;
        string id209_MarginBottom;
        protected void InitIDs()
        {
            var myNameTable = this.Reader.NameTable;
            id144_BackColorValue = myNameTable.Add(@"BackColorValue");
            id594_ContentSource = myNameTable.Add(@"ContentSource");
            id825_ItemBorderStyle = myNameTable.Add(@"ItemBorderStyle");
            id44_ContentReadonly = myNameTable.Add(@"ContentReadonly");
            id902_Zone = myNameTable.Add(@"Zone");
            id210_PaddingLeft = myNameTable.Add(@"PaddingLeft");
            id785_VerticalXLabel = myNameTable.Add(@"VerticalXLabel");
            id829_InitialAngle = myNameTable.Add(@"InitialAngle");
            id832_PieLabelStyle = myNameTable.Add(@"PieLabelStyle");
            id21_DataName = myNameTable.Add(@"DataName");
            id669_SpecifyPageIndexs = myNameTable.Add(@"SpecifyPageIndexs");
            id810_MaxSize = myNameTable.Add(@"MaxSize");
            id396_WriteTextBindingPath = myNameTable.Add(@"WriteTextBindingPath");
            id491_HeaderStyle = myNameTable.Add(@"HeaderStyle");
            id231_AlignToGridLine = myNameTable.Add(@"AlignToGridLine");
            id18_HiddenPrintWhenEmpty = myNameTable.Add(@"HiddenPrintWhenEmpty");
            id451_InsertEmptyPageForNewPage = myNameTable.Add(@"InsertEmptyPageForNewPage");
            id399_ValueValidateStyle = myNameTable.Add(@"ValueValidateStyle");
            id959_BottomTitle = myNameTable.Add(@"BottomTitle");
            id860_TextAlign = myNameTable.Add(@"TextAlign");
            id392_DataSource = myNameTable.Add(@"DataSource");
            id762_DataItems = myNameTable.Add(@"DataItems");
            id816_MaxMinValueStyle = myNameTable.Add(@"MaxMinValueStyle");
            id30_Item = myNameTable.Add(@"Item");
            id185_VerticalAlign = myNameTable.Add(@"VerticalAlign");
            id881_ShowTooltip = myNameTable.Add(@"ShowTooltip");
            id147_Repeat = myNameTable.Add(@"Repeat");
            id789_XAxisCaption = myNameTable.Add(@"XAxisCaption");
            id673_SpecifyPageIndexTextList = myNameTable.Add(@"SpecifyPageIndexTextList");
            id826_EllipseStyle = myNameTable.Add(@"EllipseStyle");
            id983_NormalRangeDownLineStyle = myNameTable.Add(@"NormalRangeDownLineStyle");
            id605_StringTag = myNameTable.Add(@"StringTag");
            id241_Author = myNameTable.Add(@"Author");
            id158_Index = myNameTable.Add(@"Index");
            id69_FileFormat = myNameTable.Add(@"FileFormat");
            id584_PrintGrid = myNameTable.Add(@"PrintGrid");
            id353_XParagraphFlag = myNameTable.Add(@"XParagraphFlag");
            id546_ListItems = myNameTable.Add(@"ListItems");
            id791_LabelStyle = myNameTable.Add(@"LabelStyle");
            id1057_X = myNameTable.Add(@"X");
            id1058_Y = myNameTable.Add(@"Y");
            id648_X2 = myNameTable.Add(@"X2");
            id649_Y2 = myNameTable.Add(@"Y2");
            id946_TitleVisible = myNameTable.Add(@"TitleVisible");
            id607_EnableEditImageAdditionShape = myNameTable.Add(@"EnableEditImageAdditionShape");
            id243_CreatorIndex = myNameTable.Add(@"CreatorIndex");
            id86_Comment = myNameTable.Add(@"Comment");
            id53_PrintVisibility = myNameTable.Add(@"PrintVisibility");
            id774_GridLineStyleH = myNameTable.Add(@"GridLineStyleH");
            id556_LonelyChecked = myNameTable.Add(@"LonelyChecked");
            id1016_TickStep = myNameTable.Add(@"TickStep");
            id105_AutoChoosePageSize = myNameTable.Add(@"AutoChoosePageSize");
            id660_Caption = myNameTable.Add(@"Caption");
            id929_HighlightOutofNormalRange = myNameTable.Add(@"HighlightOutofNormalRange");
            id516_EnableHighlight = myNameTable.Add(@"EnableHighlight");
            id316_DocumentParameter = myNameTable.Add(@"DocumentParameter");
            id237_OldWhitespaceWidth = myNameTable.Add(@"OldWhitespaceWidth");
            id140_WatermarkInfo = myNameTable.Add(@"WatermarkInfo");
            id704_TemplateDocuments = myNameTable.Add(@"TemplateDocuments");
            id266_DepartmentID = myNameTable.Add(@"DepartmentID");
            id374_XTextFooter = myNameTable.Add(@"XTextFooter");
            id557_TextInList = myNameTable.Add(@"TextInList");
            id891_PageIndexText = myNameTable.Add(@"PageIndexText");
            id338_XTextLabelElementBase = myNameTable.Add(@"XTextLabelElementBase");
            id162_BackgroundImage = myNameTable.Add(@"BackgroundImage");
            id728_ContactAction = myNameTable.Add(@"ContactAction");
            id794_ColorTheme = myNameTable.Add(@"ColorTheme");
            id772_GridYSplitNum = myNameTable.Add(@"GridYSplitNum");
            id731_PageLabelText = myNameTable.Add(@"PageLabelText");
            id1010_EndDateKeyword = myNameTable.Add(@"EndDateKeyword");
            id153_Bold = myNameTable.Add(@"Bold");
            id22_CanBeReferenced = myNameTable.Add(@"CanBeReferenced");
            id876_IllegalTextEndCharForLinux = myNameTable.Add(@"IllegalTextEndCharForLinux");
            id694_TempClassForSer20200817 = myNameTable.Add(@"TempClassForSer20200817");
            id619_CustomAdditionShapeContent = myNameTable.Add(@"CustomAdditionShapeContent");
            id290_DocumentContentStyleContainer = myNameTable.Add(@"DocumentContentStyleContainer");
            id305_Cursor = myNameTable.Add(@"Cursor");
            id211_PaddingTop = myNameTable.Add(@"PaddingTop");
            id559_Group = myNameTable.Add(@"Group");
            id203_BorderTop = myNameTable.Add(@"BorderTop");
            id441_AutoFixFontSizeMode = myNameTable.Add(@"AutoFixFontSizeMode");
            id885_DataGridBottomPadding = myNameTable.Add(@"DataGridBottomPadding");
            id914_DateFormatStringForCrossYear = myNameTable.Add(@"DateFormatStringForCrossYear");
            id389_AuthorisedUserIDList = myNameTable.Add(@"AuthorisedUserIDList");
            id719_XMLViewStateBagItem = myNameTable.Add(@"XMLViewStateBagItem");
            id753_DataForSelfCheck = myNameTable.Add(@"DataForSelfCheck");
            id913_DateFormatString = myNameTable.Add(@"DateFormatString");
            id259_Description = myNameTable.Add(@"Description");
            id45_Expressions = myNameTable.Add(@"Expressions");
            id490_CanSplitByPageLine = myNameTable.Add(@"CanSplitByPageLine");
            id134_RepeatedImageValue = myNameTable.Add(@"RepeatedImageValue");
            id814_Color2 = myNameTable.Add(@"Color2");
            id1061_DCNameValueItem = myNameTable.Add(@"DCNameValueItem");
            id138_ReferenceCount = myNameTable.Add(@"ReferenceCount");
            id723_PageTexts = myNameTable.Add(@"PageTexts");
            id653_CheckedUserHistories = myNameTable.Add(@"CheckedUserHistories");
            id997_AfterOperaDaysFromZero = myNameTable.Add(@"AfterOperaDaysFromZero");
            id117_PaperKind = myNameTable.Add(@"PaperKind");
            id120_Copies = myNameTable.Add(@"Copies");
            id988_FieldNameForTime = myNameTable.Add(@"FieldNameForTime");
            id254_KBEntryRangeMask = myNameTable.Add(@"KBEntryRangeMask");
            id191_LayoutAlign = myNameTable.Add(@"LayoutAlign");
            id998_AfterOperaDaysBeginOne = myNameTable.Add(@"AfterOperaDaysBeginOne");
            id686_PreviewImageData = myNameTable.Add(@"PreviewImageData");
            id861_Verified = myNameTable.Add(@"Verified");
            id48_CopySource = myNameTable.Add(@"CopySource");
            id854_VerticalLine = myNameTable.Add(@"VerticalLine");
            id560_CheckedTime = myNameTable.Add(@"CheckedTime");
            id506_EventExpressions = myNameTable.Add(@"EventExpressions");
            id678_ControlType = myNameTable.Add(@"ControlType");
            id391_Enabled = myNameTable.Add(@"Enabled");
            id14_EmitDataFieldName = myNameTable.Add(@"EmitDataFieldName");
            id795_CustomColorTheme = myNameTable.Add(@"CustomColorTheme");
            id930_InputTimePrecision = myNameTable.Add(@"InputTimePrecision");
            id312_PermissionLevel = myNameTable.Add(@"PermissionLevel");
            id513_BackgroundText = myNameTable.Add(@"BackgroundText");
            id831_SliceRelativeDisplacement = myNameTable.Add(@"SliceRelativeDisplacement");
            id1032_ThickLineWidth = myNameTable.Add(@"ThickLineWidth");
            id37_ContentReadonlyExpression = myNameTable.Add(@"ContentReadonlyExpression");
            id106_PageIndexsForHideHeaderFooter = myNameTable.Add(@"PageIndexsForHideHeaderFooter");
            id720_XMLValue = myNameTable.Add(@"XMLValue");
            id1034_DocumentPageSettings = myNameTable.Add(@"DocumentPageSettings");
            id918_Item = myNameTable.Add(@"DateFormatStringForFirstIndexOtherPage");
            id489_AllowUserToResizeHeight = myNameTable.Add(@"AllowUserToResizeHeight");
            id651_LabelAtUp = myNameTable.Add(@"LabelAtUp");
            id99_HeaderFooterDifferentFirstPage = myNameTable.Add(@"HeaderFooterDifferentFirstPage");
            id8_Attributes = myNameTable.Add(@"Attributes");
            id971_ShadowPointVisible = myNameTable.Add(@"ShadowPointVisible");
            id515_BorderVisible = myNameTable.Add(@"BorderVisible");
            id590_MinBarWidth = myNameTable.Add(@"MinBarWidth");
            id599_TopicID = myNameTable.Add(@"TopicID");
            id859_LanternValue = myNameTable.Add(@"LanternValue");
            id714_SubEntries = myNameTable.Add(@"SubEntries");
            id347_XPageInfo = myNameTable.Add(@"XPageInfo");
            id132_StrictUsePageSize = myNameTable.Add(@"StrictUsePageSize");
            id1022_ParameterName = myNameTable.Add(@"ParameterName");
            id527_SelectedIndex = myNameTable.Add(@"SelectedIndex");
            id289_ImportFooter = myNameTable.Add(@"ImportFooter");
            id125_DesignerPaperHeight = myNameTable.Add(@"DesignerPaperHeight");
            id999_OutofNormalRangeTextColorValue = myNameTable.Add(@"OutofNormalRangeTextColorValue");
            id1047_PartitionList = myNameTable.Add(@"PartitionList");
            id969_BottomPadding = myNameTable.Add(@"BottomPadding");
            id470_HoldWholeLine = myNameTable.Add(@"HoldWholeLine");
            id956_ColorValueForDownValue = myNameTable.Add(@"ColorValueForDownValue");
            id730_LinkTextForContactAction = myNameTable.Add(@"LinkTextForContactAction");
            id696_KBLibrary = myNameTable.Add(@"KBLibrary");
            id767_BarOpacity = myNameTable.Add(@"BarOpacity");
            id606_TransparentColorValue = myNameTable.Add(@"TransparentColorValue");
            id317_TypeName = myNameTable.Add(@"TypeName");
            id622_LocalStyle = myNameTable.Add(@"LocalStyle");
            id711_RangeMask = myNameTable.Add(@"RangeMask");
            id638_ShapeEllipseElement = myNameTable.Add(@"ShapeEllipseElement");
            id384_VBScriptItem = myNameTable.Add(@"VBScriptItem");
            id786_GridBorderLineStyle = myNameTable.Add(@"GridBorderLineStyle");
            id968_TopPadding = myNameTable.Add(@"TopPadding");
            id309_UserHistoryInfo = myNameTable.Add(@"UserHistoryInfo");
            id190_Multiline = myNameTable.Add(@"Multiline");
            id444_SlantSplitLineStyle = myNameTable.Add(@"SlantSplitLineStyle");
            id295_PrintColor = myNameTable.Add(@"PrintColor");
            id827_PieHeight = myNameTable.Add(@"PieHeight");
            id563_SpellCode3 = myNameTable.Add(@"SpellCode3");
            id722_LineSize = myNameTable.Add(@"LineSize");
            id135_HorizontalResolution = myNameTable.Add(@"HorizontalResolution");
            id724_PageText = myNameTable.Add(@"PageText");
            id3_StyleIndex = myNameTable.Add(@"StyleIndex");
            id884_DataGridTopPadding = myNameTable.Add(@"DataGridTopPadding");
            id776_GridTextHeight = myNameTable.Add(@"GridTextHeight");
            id91_XPageSettings = myNameTable.Add(@"XPageSettings");
            id422_DataFeedbackInfo = myNameTable.Add(@"DataFeedbackInfo");
            id812_Background = myNameTable.Add(@"Background");
            id682_EnableViewState = myNameTable.Add(@"EnableViewState");
            id127_LeftMargin = myNameTable.Add(@"LeftMargin");
            id845_VerifiedAlignment = myNameTable.Add(@"VerifiedAlignment");
            id416_IncludeKeywords = myNameTable.Add(@"IncludeKeywords");
            id1029_AutoTickFormatString = myNameTable.Add(@"AutoTickFormatString");
            id113_Item = myNameTable.Add(@"PageIndexsForPrintBackgroundImage");
            id419_RequiredInvalidateFlag = myNameTable.Add(@"RequiredInvalidateFlag");
            id631_ShapeContentStyleContainer = myNameTable.Add(@"ShapeContentStyleContainer");
            id60_LocalExcludeKeywords = myNameTable.Add(@"LocalExcludeKeywords");
            id907_ForeColorValue = myNameTable.Add(@"ForeColorValue");
            id1004_LoopTextList = myNameTable.Add(@"LoopTextList");
            id61_DisabledParameterNames = myNameTable.Add(@"DisabledParameterNames");
            id28_JavaScriptForDoubleClick = myNameTable.Add(@"JavaScriptForDoubleClick");
            id1_TemperatureDocument = myNameTable.Add(@"TemperatureDocument");
            id852_IntCharSymbol = myNameTable.Add(@"IntCharSymbol");
            id756_DataFieldNameForSeriesName = myNameTable.Add(@"DataFieldNameForSeriesName");
            id687_OptionsPropertyName = myNameTable.Add(@"OptionsPropertyName");
            id640_ShapeDocumentPage = myNameTable.Add(@"ShapeDocumentPage");
            id925_YAxisInfos = myNameTable.Add(@"YAxisInfos");
            id1051_BrushStyle = myNameTable.Add(@"BrushStyle");
            id617_PageImages = myNameTable.Add(@"PageImages");
            id270_DocumentProcessState = myNameTable.Add(@"DocumentProcessState");
            id339_XTDBarcodeField = myNameTable.Add(@"XTDBarcodeField");
            id635_ShapeRectangleElement = myNameTable.Add(@"ShapeRectangleElement");
            id658_PrintTextForUnChecked = myNameTable.Add(@"PrintTextForUnChecked");
            id551_ValuePath = myNameTable.Add(@"ValuePath");
            id616_CompressSaveMode = myNameTable.Add(@"CompressSaveMode");
            id291_Default = myNameTable.Add(@"Default");
            id146_Text = myNameTable.Add(@"Text");
            id394_BindingPath = myNameTable.Add(@"BindingPath");
            id232_LineWidth = myNameTable.Add(@"LineWidth");
            id975_Scale = myNameTable.Add(@"Scale");
            id738_ScriptTextForClick = myNameTable.Add(@"ScriptTextForClick");
            id253_FieldBorderElementWidth = myNameTable.Add(@"FieldBorderElementWidth");
            id1002_BlockWidth = myNameTable.Add(@"BlockWidth");
            id350_XTextCheckBox = myNameTable.Add(@"XTextCheckBox");
            id343_XFileBlock = myNameTable.Add(@"XFileBlock");
            id445_RowSpan = myNameTable.Add(@"RowSpan");
            id662_CaptionAlign = myNameTable.Add(@"CaptionAlign");
            id667_CheckBoxVisible = myNameTable.Add(@"CheckBoxVisible");
            id54_Visible = myNameTable.Add(@"Visible");
            id889_Labels = myNameTable.Add(@"Labels");
            id514_ViewEncryptType = myNameTable.Add(@"ViewEncryptType");
            id887_SpecifyTickWidth = myNameTable.Add(@"SpecifyTickWidth");
            id717_OwnerID = myNameTable.Add(@"OwnerID");
            id532_CustomValueEditorTypeName = myNameTable.Add(@"CustomValueEditorTypeName");
            id461_PrintPositionInPage = myNameTable.Add(@"PrintPositionInPage");
            id94_SwapGutter = myNameTable.Add(@"SwapGutter");
            id684_AllowUserResize = myNameTable.Add(@"AllowUserResize");
            id797_Colors = myNameTable.Add(@"Colors");
            id239_BindingUserTrack = myNameTable.Add(@"BindingUserTrack");
            id636_ShapeZoomInElement = myNameTable.Add(@"ShapeZoomInElement");
            id410_CheckDecimalDigits = myNameTable.Add(@"CheckDecimalDigits");
            id217_Width = myNameTable.Add(@"Width");
            id835_EnumLableType = myNameTable.Add(@"EnumLableType");
            id89_DocumentGraphicsUnit = myNameTable.Add(@"DocumentGraphicsUnit");
            id488_Item = myNameTable.Add(@"AllowUserPressTabKeyToInsertRowDown");
            id233_GridNumInOnePage = myNameTable.Add(@"GridNumInOnePage");
            id591_VAlign = myNameTable.Add(@"VAlign");
            id926_YAxis = myNameTable.Add(@"YAxis");
            id1031_GridYSpaceNumForBottomPadding = myNameTable.Add(@"GridYSpaceNumForBottomPadding");
            id10_InnerID = myNameTable.Add(@"InnerID");
            id78_GlobalJavaScriptReferences = myNameTable.Add(@"GlobalJavaScriptReferences");
            id4_PreviewTextLength = myNameTable.Add(@"PreviewTextLength");
            id838_Config = myNameTable.Add(@"Config");
            id853_LinkTarget = myNameTable.Add(@"LinkTarget");
            id304_GridLineColor = myNameTable.Add(@"GridLineColor");
            id440_StressBorder = myNameTable.Add(@"StressBorder");
            id1006_TitleAlign = myNameTable.Add(@"TitleAlign");
            id1042_IsMultiSelect = myNameTable.Add(@"IsMultiSelect");
            id358_XInputFieldBase = myNameTable.Add(@"XInputFieldBase");
            id858_EndTime = myNameTable.Add(@"EndTime");
            id569_NextTarget = myNameTable.Add(@"NextTarget");
            id521_AdornTextType = myNameTable.Add(@"AdornTextType");
            id77_PageContentVersions = myNameTable.Add(@"PageContentVersions");
            id935_LineStyleForLanternValue = myNameTable.Add(@"LineStyleForLanternValue");
            id765_ChartDocumentStyle = myNameTable.Add(@"ChartDocumentStyle");
            id346_XMedia = myNameTable.Add(@"XMedia");
            id1063_WhiteSpaceLength = myNameTable.Add(@"WhiteSpaceLength");
            id174_Superscript = myNameTable.Add(@"Superscript");
            id624_Elements = myNameTable.Add(@"Elements");
            id71_UserHistories = myNameTable.Add(@"UserHistories");
            id547_InputFieldListItem = myNameTable.Add(@"InputFieldListItem");
            id417_ExcludeKeywords = myNameTable.Add(@"ExcludeKeywords");
            id95_GutterPosition = myNameTable.Add(@"GutterPosition");
            id562_SpellCode2 = myNameTable.Add(@"SpellCode2");
            id315_CheckedValue = myNameTable.Add(@"CheckedValue");
            id483_Item = myNameTable.Add(@"CloneMultipleBaseForBindingDataSource");
            id500_BackgroundTextColor = myNameTable.Add(@"BackgroundTextColor");
            id863_ValueTextTopPadding = myNameTable.Add(@"ValueTextTopPadding");
            id989_FieldNameForValue = myNameTable.Add(@"FieldNameForValue");
            id729_AttributeNameForContactAction = myNameTable.Add(@"AttributeNameForContactAction");
            id614_AdditionShape = myNameTable.Add(@"AdditionShape");
            id7_EditorVersionString = myNameTable.Add(@"EditorVersionString");
            id335_XSignImage = myNameTable.Add(@"XSignImage");
            id228_TextInMiddlePage = myNameTable.Add(@"TextInMiddlePage");
            id806_EndCap = myNameTable.Add(@"EndCap");
            id413_DateTimeMinValue = myNameTable.Add(@"DateTimeMinValue");
            id1041_DCTimeLineImage = myNameTable.Add(@"DCTimeLineImage");
            id27_JavaScriptForClick = myNameTable.Add(@"JavaScriptForClick");
            id385_ScriptMethodName = myNameTable.Add(@"ScriptMethodName");
            id655_CheckboxVisibility = myNameTable.Add(@"CheckboxVisibility");
            id873_EnableExtMouseMoveEvent = myNameTable.Add(@"EnableExtMouseMoveEvent");
            id116_PaperSource = myNameTable.Add(@"PaperSource");
            id246_BorderColor = myNameTable.Add(@"BorderColor");
            id136_VerticalResolution = myNameTable.Add(@"VerticalResolution");
            id284_MotherTemplateInfo = myNameTable.Add(@"MotherTemplateInfo");
            id611_KeepWidthHeightRate = myNameTable.Add(@"KeepWidthHeightRate");
            id50_XElements = myNameTable.Add(@"XElements");
            id986_FieldNameForLink = myNameTable.Add(@"FieldNameForLink");
            id964_ValueFieldName = myNameTable.Add(@"ValueFieldName");
            id976_YAxisScaleInfo = myNameTable.Add(@"YAxisScaleInfo");
            id298_CommentIndex = myNameTable.Add(@"CommentIndex");
            id609_ValueIndexOfRepeatedImage = myNameTable.Add(@"ValueIndexOfRepeatedImage");
            id895_PageTitlePosition = myNameTable.Add(@"PageTitlePosition");
            id242_CreationTime = myNameTable.Add(@"CreationTime");
            id672_ChangePageIndexMidway = myNameTable.Add(@"ChangePageIndexMidway");
            id552_Items = myNameTable.Add(@"Items");
            id865_UpAndDown = myNameTable.Add(@"UpAndDown");
            id123_DesignerPaperWidth = myNameTable.Add(@"DesignerPaperWidth");
            id798_ShadeCount = myNameTable.Add(@"ShadeCount");
            id130_BottomMargin = myNameTable.Add(@"BottomMargin");
            id74_RepeatedImages = myNameTable.Add(@"RepeatedImages");
            id985_FieldNameForID = myNameTable.Add(@"FieldNameForID");
            id741_DrawContentHandlerName = myNameTable.Add(@"DrawContentHandlerName");
            id269_DocumentType = myNameTable.Add(@"DocumentType");
            id382_DescPropertyName = myNameTable.Add(@"DescPropertyName");
            id779_GridBackground = myNameTable.Add(@"GridBackground");
            id992_SQLText = myNameTable.Add(@"SQLText");
            id109_OffsetX = myNameTable.Add(@"OffsetX");
            id110_OffsetY = myNameTable.Add(@"OffsetY");
            id204_BorderRight = myNameTable.Add(@"BorderRight");
            id995_BlankDateWhenNoData = myNameTable.Add(@"BlankDateWhenNoData");
            id496_BorderElementColorValue = myNameTable.Add(@"BorderElementColorValue");
            id25_ToolTip = myNameTable.Add(@"ToolTip");
            id586_Digitals = myNameTable.Add(@"Digitals");
            id1005_PageTitleTexts = myNameTable.Add(@"PageTitleTexts");
            id811_SymbolSize = myNameTable.Add(@"SymbolSize");
            id429_AutoClean = myNameTable.Add(@"AutoClean");
            id207_MarginTop = myNameTable.Add(@"MarginTop");
            id455_IsCollapsed = myNameTable.Add(@"IsCollapsed");
            id965_LanternValueFieldName = myNameTable.Add(@"LanternValueFieldName");
            id11_ID = myNameTable.Add(@"ID");
            id181_LetterSpacing = myNameTable.Add(@"LetterSpacing");
            id770_BarGroupSpacing = myNameTable.Add(@"BarGroupSpacing");
            id911_GridLineColorValue = myNameTable.Add(@"GridLineColorValue");
            id561_SpellCode = myNameTable.Add(@"SpellCode");
            id40_ValueExpression = myNameTable.Add(@"ValueExpression");
            id642_AutoSize = myNameTable.Add(@"AutoSize");
            id771_ViewStyle = myNameTable.Add(@"ViewStyle");
            id482_DataSourceRowSpan = myNameTable.Add(@"DataSourceRowSpan");
            id250_SubDocumentSettings = myNameTable.Add(@"SubDocumentSettings");
            id471_PrintBothBorderWhenJumpPrint = myNameTable.Add(@"PrintBothBorderWhenJumpPrint");
            id698_BaseURL = myNameTable.Add(@"BaseURL");
            id813_XBrushStyle = myNameTable.Add(@"XBrushStyle");
            id33_AutoHideMode = myNameTable.Add(@"AutoHideMode");
            id1044_IdentyColorARGBValue = myNameTable.Add(@"IdentyColorARGBValue");
            id1043_ElementIDForExporting = myNameTable.Add(@"ElementIDForExporting");
            id862_VerifiedColor = myNameTable.Add(@"VerifiedColor");
            id52_AcceptChildElementTypes2 = myNameTable.Add(@"AcceptChildElementTypes2");
            id517_EnableUserEditInnerValue = myNameTable.Add(@"EnableUserEditInnerValue");
            id847_SpecifySymbolStyle = myNameTable.Add(@"SpecifySymbolStyle");
            id957_ColorValueForUpValue = myNameTable.Add(@"ColorValueForUpValue");
            id188_VertialText = myNameTable.Add(@"VertialText");
            id251_Readonly = myNameTable.Add(@"Readonly");
            id150_XFontValue = myNameTable.Add(@"XFontValue");
            id1040_ShowBorder = myNameTable.Add(@"ShowBorder");
            id454_EnableCollapse = myNameTable.Add(@"EnableCollapse");
            id282_PageContentVersionInfo = myNameTable.Add(@"PageContentVersionInfo");
            id401_ValueName = myNameTable.Add(@"ValueName");
            id273_NumOfPage = myNameTable.Add(@"NumOfPage");
            id894_FooterDescription = myNameTable.Add(@"FooterDescription");
            id121_HeaderDistance = myNameTable.Add(@"HeaderDistance");
            id792_RipenessRate = myNameTable.Add(@"RipenessRate");
            id866_DCTimeLineParameter = myNameTable.Add(@"DCTimeLineParameter");
            id342_XTextBlockElement = myNameTable.Add(@"XTextBlockElement");
            id303_GridLineStyle = myNameTable.Add(@"GridLineStyle");
            id367_XTextContentElement = myNameTable.Add(@"XTextContentElement");
            id365_XTextTableRow = myNameTable.Add(@"XTextTableRow");
            id485_PrintCellBorder = myNameTable.Add(@"PrintCellBorder");
            id904_Tick = myNameTable.Add(@"Tick");
            id709_KBEntry = myNameTable.Add(@"KBEntry");
            id180_LineSpacingStyle = myNameTable.Add(@"LineSpacingStyle");
            id1026_StartTime = myNameTable.Add(@"StartTime");
            id187_LeftIndent = myNameTable.Add(@"LeftIndent");
            id575_EventExpressionInfo = myNameTable.Add(@"EventExpressionInfo");
            id1027_AlignToGrid = myNameTable.Add(@"AlignToGrid");
            id387_DCContentLockInfo = myNameTable.Add(@"DCContentLockInfo");
            id937_AllowOutofRange = myNameTable.Add(@"AllowOutofRange");
            id700_TemplateSourceFormatString = myNameTable.Add(@"TemplateSourceFormatString");
            id963_SymbolColorValue = myNameTable.Add(@"SymbolColorValue");
            id70_BodyGridLineOffset = myNameTable.Add(@"BodyGridLineOffset");
            id533_EnableValueEditor = myNameTable.Add(@"EnableValueEditor");
            id126_PaperHeight = myNameTable.Add(@"PaperHeight");
            id703_Entry = myNameTable.Add(@"Entry");
            id201_BorderLeft = myNameTable.Add(@"BorderLeft");
            id598_FileContentSource = myNameTable.Add(@"FileContentSource");
            id277_SubDocumentSpacing = myNameTable.Add(@"SubDocumentSpacing");
            id97_ShowGutterLine = myNameTable.Add(@"ShowGutterLine");
            id51_Element = myNameTable.Add(@"Element");
            id996_Item = myNameTable.Add(@"HiddenOnPageViewWhenNoValuePoints");
            id733_BarcodeType = myNameTable.Add(@"BarcodeType");
            id898_ImagePixelHeight = myNameTable.Add(@"ImagePixelHeight");
            id1024_TickInfo = myNameTable.Add(@"TickInfo");
            id519_AutoSetSpellCodeInDropdownList = myNameTable.Add(@"AutoSetSpellCodeInDropdownList");
            id216_Top = myNameTable.Add(@"Top");
            id383_IgnoreChildElements = myNameTable.Add(@"IgnoreChildElements");
            id1059_SynchroDataMode = myNameTable.Add(@"SynchroDataMode");
            id1060_AutoSize = myNameTable.Add(@"AutoSize");
            id1060_EmptyWhenNoData = myNameTable.Add(@"EmptyWhenNoData");
            id809_ChartLegendStyle = myNameTable.Add(@"ChartLegendStyle");
            id240_AuthorID = myNameTable.Add(@"AuthorID");
            id595_UpdateState = myNameTable.Add(@"UpdateState");
            id379_CopySourceInfo = myNameTable.Add(@"CopySourceInfo");
            id869_LineWidthZoomRateWhenPrint = myNameTable.Add(@"LineWidthZoomRateWhenPrint");
            id118_AutoPaperWidth = myNameTable.Add(@"AutoPaperWidth");
            id680_ValuePropertyName = myNameTable.Add(@"ValuePropertyName");
            id755_DataSourceName = myNameTable.Add(@"DataSourceName");
            id5_TransparentEncryptErrorMessage = myNameTable.Add(@"TransparentEncryptErrorMessage");
            id503_TabIndex = myNameTable.Add(@"TabIndex");
            id24_ReferencedDataName = myNameTable.Add(@"ReferencedDataName");
            id38_VisibleExpression = myNameTable.Add(@"VisibleExpression");
            id801_XPenStyle = myNameTable.Add(@"XPenStyle");
            id221_ParagraphMultiLevel = myNameTable.Add(@"ParagraphMultiLevel");
            id1028_AutoTickStepSeconds = myNameTable.Add(@"AutoTickStepSeconds");
            id905_BigTitleFontSize = myNameTable.Add(@"BigTitleFontSize");
            id1015_LayoutType = myNameTable.Add(@"LayoutType");
            id970_ValueFont = myNameTable.Add(@"ValueFont");
            id602_InnerRepeatImageIndex = myNameTable.Add(@"InnerRepeatImageIndex");
            id834_DownleadWidth = myNameTable.Add(@"DownleadWidth");
            id967_MaxTextDisplayLength = myNameTable.Add(@"MaxTextDisplayLength");
            id571_AutoUpdateTargetElement = myNameTable.Add(@"AutoUpdateTargetElement");
            id19_DataFeedback = myNameTable.Add(@"DataFeedback");
            id345_XTextControlHost = myNameTable.Add(@"XTextControlHost");
            id318_ValueType = myNameTable.Add(@"ValueType");
            id219_PageBreakAfter = myNameTable.Add(@"PageBreakAfter");
            id271_DocumentEditState = myNameTable.Add(@"DocumentEditState");
            id236_Printable = myNameTable.Add(@"Printable");
            id625_ShapeElement = myNameTable.Add(@"ShapeElement");
            id745_SignErrorMessage = myNameTable.Add(@"SignErrorMessage");
            id319_SourceColumn = myNameTable.Add(@"SourceColumn");
            id447_DesignRowIndex = myNameTable.Add(@"DesignRowIndex");
            id725_StrictMatchPageIndex = myNameTable.Add(@"StrictMatchPageIndex");
            id637_ShapePolygonElement = myNameTable.Add(@"ShapePolygonElement");
            id361_XBarcodeField = myNameTable.Add(@"XBarcodeField");
            id301_GridLineType = myNameTable.Add(@"GridLineType");
            id425_KeyFieldName = myNameTable.Add(@"KeyFieldName");
            id775_GridTextWidth = myNameTable.Add(@"GridTextWidth");
            id842_Values = myNameTable.Add(@"Values");
            id163_Visibility = myNameTable.Add(@"Visibility");
            id768_BarWidth = myNameTable.Add(@"BarWidth");
            id449_MirrorViewForCrossPage = myNameTable.Add(@"MirrorViewForCrossPage");
            id310_Name2 = myNameTable.Add(@"Name2");
            id570_NextTargetID = myNameTable.Add(@"NextTargetID");
            id176_FixedSpacing = myNameTable.Add(@"FixedSpacing");
            id981_NormalMinValue = myNameTable.Add(@"NormalMinValue");
            id276_HeightInPrintJob = myNameTable.Add(@"HeightInPrintJob");
            id395_BindingPathForText = myNameTable.Add(@"BindingPathForText");
            id227_DocumentTerminalTextInfo = myNameTable.Add(@"DocumentTerminalTextInfo");
            id141_Type = myNameTable.Add(@"Type");
            id206_MarginLeft = myNameTable.Add(@"MarginLeft");
            id314_Tag = myNameTable.Add(@"Tag");
            id924_FooterLines = myNameTable.Add(@"FooterLines");
            id973_AbNormalRangeSettings = myNameTable.Add(@"AbNormalRangeSettings");
            id368_XTextSection = myNameTable.Add(@"XTextSection");
            id565_LinkListBindingInfo = myNameTable.Add(@"LinkListBindingInfo");
            id235_LineStyle = myNameTable.Add(@"LineStyle");
            id740_ChartPageIndex = myNameTable.Add(@"ChartPageIndex");
            id460_PrintedPageIndex = myNameTable.Add(@"PrintedPageIndex");
            id697_TemplateFileSystemName = myNameTable.Add(@"TemplateFileSystemName");
            id523_EditorControlTypeName = myNameTable.Add(@"EditorControlTypeName");
            id452_ExpendForDataBinding = myNameTable.Add(@"ExpendForDataBinding");
            id157_PageBorderBackgroundStyle = myNameTable.Add(@"PageBorderBackgroundStyle");
            id823_Item = myNameTable.Add(@"DataFieldNameForFillColorString");
            id701_TemplateFileFormat = myNameTable.Add(@"TemplateFileFormat");
            id939_ClickToHide = myNameTable.Add(@"ClickToHide");
            id381_SourcePropertyName = myNameTable.Add(@"SourcePropertyName");
            id920_HeaderLabels = myNameTable.Add(@"HeaderLabels");
            id879_HeaderLabelLineAlignment = myNameTable.Add(@"HeaderLabelLineAlignment");
            id524_LinkListBinding = myNameTable.Add(@"LinkListBinding");
            id670_AutoHeight = myNameTable.Add(@"AutoHeight");
            id65_Parameter = myNameTable.Add(@"Parameter");
            id67_InnerRepeatImageDataList = myNameTable.Add(@"InnerRepeatImageDataList");
            id941_EnableLanternValue = myNameTable.Add(@"EnableLanternValue");
            id934_LanternValueColorForDownValue = myNameTable.Add(@"LanternValueColorForDownValue");
            id643_Points = myNameTable.Add(@"Points");
            id993_TitleLineInfo = myNameTable.Add(@"TitleLineInfo");
            id708_Content = myNameTable.Add(@"Content");
            id874_EnableDataGridLinearAxisMode = myNameTable.Add(@"EnableDataGridLinearAxisMode");
            id890_Label = myNameTable.Add(@"Label");
            id154_Italic = myNameTable.Add(@"Italic");
            id1048_XImagePartition = myNameTable.Add(@"XImagePartition");
            id732_BarcodeStyle2 = myNameTable.Add(@"BarcodeStyle2");
            id183_RTFLineSpacing = myNameTable.Add(@"RTFLineSpacing");
            id710_CopyListItems = myNameTable.Add(@"CopyListItems");
            id764_ChartStyle = myNameTable.Add(@"ChartStyle");
            id324_XPageBreak = myNameTable.Add(@"XPageBreak");
            id351_XImage = myNameTable.Add(@"XImage");
            id549_SourceName = myNameTable.Add(@"SourceName");
            id75_Image = myNameTable.Add(@"Image");
            id41_UserFlags = myNameTable.Add(@"UserFlags");
            id333_XPie = myNameTable.Add(@"XPie");
            id961_HiddenValueTitleBackColorValue = myNameTable.Add(@"HiddenValueTitleBackColorValue");
            id950_RedLineValue = myNameTable.Add(@"RedLineValue");
            id307_Link = myNameTable.Add(@"Link");
            id566_ProviderName = myNameTable.Add(@"ProviderName");
            id948_ValueFormatString = myNameTable.Add(@"ValueFormatString");
            id377_GridLine = myNameTable.Add(@"GridLine");
            id966_TimeFieldName = myNameTable.Add(@"TimeFieldName");
            id715_ListItemsSource = myNameTable.Add(@"ListItemsSource");
            id782_GridLineOffsetX = myNameTable.Add(@"GridLineOffsetX");
            id302_GridLineOffsetY = myNameTable.Add(@"GridLineOffsetY");
            id255_IsTemplate = myNameTable.Add(@"IsTemplate");
            id695_KB = myNameTable.Add(@"KB");
            id721_LineLengthInCM = myNameTable.Add(@"LineLengthInCM");
            id705_Document = myNameTable.Add(@"Document");
            id431_CommandText = myNameTable.Add(@"CommandText");
            id322_SerializeValue = myNameTable.Add(@"SerializeValue");
            id1037_MultiLine = myNameTable.Add(@"MultiLine");
            id267_DepartmentName = myNameTable.Add(@"DepartmentName");
            id676_SpecifyPageIndex = myNameTable.Add(@"SpecifyPageIndex");
            id415_RegExpression = myNameTable.Add(@"RegExpression");
            id917_Item = myNameTable.Add(@"DateFormatStringForFirstIndexFirstPage");
            id321_ValueTypeFullName = myNameTable.Add(@"ValueTypeFullName");
            id804_LineJoin = myNameTable.Add(@"LineJoin");
            id915_DateFormatStringForCrossMonth = myNameTable.Add(@"DateFormatStringForCrossMonth");
            id629_EditMode = myNameTable.Add(@"EditMode");
            id1017_TickLineVisible = myNameTable.Add(@"TickLineVisible");
            id478_Columns = myNameTable.Add(@"Columns");
            id458_DocumentID = myNameTable.Add(@"DocumentID");
            id297_LayoutDirection = myNameTable.Add(@"LayoutDirection");
            id13_TransparentEncryptMode = myNameTable.Add(@"TransparentEncryptMode");
            id151_Size = myNameTable.Add(@"Size");
            id870_LinkVisualStyle = myNameTable.Add(@"LinkVisualStyle");
            id363_XDirectoryField = myNameTable.Add(@"XDirectoryField");
            id87_MeasureMode = myNameTable.Add(@"MeasureMode");
            id424_FieldName = myNameTable.Add(@"FieldName");
            id892_SpecifyStartDate = myNameTable.Add(@"SpecifyStartDate");
            id328_XLineBreak = myNameTable.Add(@"XLineBreak");
            id1053_PointF = myNameTable.Add(@"PointF");
            id481_GroupName = myNameTable.Add(@"GroupName");
            id539_MultiColumn = myNameTable.Add(@"MultiColumn");
            id758_DataFieldNameForText = myNameTable.Add(@"DataFieldNameForText");
            id896_ShowIcon = myNameTable.Add(@"ShowIcon");
            id621_ShapeDocument = myNameTable.Add(@"ShapeDocument");
            id900_GridYSplitInfo = myNameTable.Add(@"GridYSplitInfo");
            id43_ContentLock = myNameTable.Add(@"ContentLock");
            id79_Reference = myNameTable.Add(@"Reference");
            id875_ExtendDaysForTimeLine = myNameTable.Add(@"ExtendDaysForTimeLine");
            id20_MaxInputLength = myNameTable.Add(@"MaxInputLength");
            id397_ProcessState = myNameTable.Add(@"ProcessState");
            id912_GridBackColorValue = myNameTable.Add(@"GridBackColorValue");
            id972_RedLinePrintVisible = myNameTable.Add(@"RedLinePrintVisible");
            id464_ContentLoaded = myNameTable.Add(@"ContentLoaded");
            id370_XTextTableCell = myNameTable.Add(@"XTextTableCell");
            id355_XField = myNameTable.Add(@"XField");
            id487_AllowInsertRowDownUseHotKey = myNameTable.Add(@"AllowInsertRowDownUseHotKey");
            id373_XTextHeaderForFirstPage = myNameTable.Add(@"XTextHeaderForFirstPage");
            id778_GridValueFormat = myNameTable.Add(@"GridValueFormat");
            id215_Left = myNameTable.Add(@"Left");
            id927_YAxisInfo = myNameTable.Add(@"YAxisInfo");
            id632_ShapeLinesElement = myNameTable.Add(@"ShapeLinesElement");
            id435_DisplayFormat = myNameTable.Add(@"DisplayFormat");
            id1050_XPartition = myNameTable.Add(@"XPartition");
            id982_NormalRangeUpLineStyle = myNameTable.Add(@"NormalRangeUpLineStyle");
            id329_XTextObjectElement = myNameTable.Add(@"XTextObjectElement");
            id530_LastSelectedListItems = myNameTable.Add(@"LastSelectedListItems");
            id734_ErroeCorrectionLevel = myNameTable.Add(@"ErroeCorrectionLevel");
            id857_Time = myNameTable.Add(@"Time");
            id783_VerticalTextAlign = myNameTable.Add(@"VerticalTextAlign");
            id336_XCustomShape = myNameTable.Add(@"XCustomShape");
            id945_TitleValueDispalyFormat = myNameTable.Add(@"TitleValueDispalyFormat");
            id612_Source = myNameTable.Add(@"Source");
            id601_AutoCreate = myNameTable.Add(@"AutoCreate");
            id707_KBID = myNameTable.Add(@"KBID");
            id769_BarSpacing = myNameTable.Add(@"BarSpacing");
            id675_RawPageIndex = myNameTable.Add(@"RawPageIndex");
            id763_DataItem = myNameTable.Add(@"DataItem");
            id212_PaddingRight = myNameTable.Add(@"PaddingRight");
            id685_SavePreviewImage = myNameTable.Add(@"SavePreviewImage");
            id103_DocumentGridLine = myNameTable.Add(@"DocumentGridLine");
            id84_BodyText = myNameTable.Add(@"BodyText");
            id592_EditValueInDialog = myNameTable.Add(@"EditValueInDialog");
            id62_Name = myNameTable.Add(@"Name");
            id331_XPartitionImage = myNameTable.Add(@"XPartitionImage");
            id1049_Url = myNameTable.Add(@"Url");
            id208_MarginRight = myNameTable.Add(@"MarginRight");
            id851_IntCharLantern = myNameTable.Add(@"IntCharLantern");
            id1025_TimeLineZoneInfo = myNameTable.Add(@"TimeLineZoneInfo");
            id936_SpecifyTitleWidth = myNameTable.Add(@"SpecifyTitleWidth");
            id504_SpecifyWidth = myNameTable.Add(@"SpecifyWidth");
            id279_Locked = myNameTable.Add(@"Locked");
            id1060_SmartChartMode = myNameTable.Add(@"SmartChartMode");
            id172_UnderlineStyle = myNameTable.Add(@"UnderlineStyle");
            id664_ControlStyle = myNameTable.Add(@"ControlStyle");
            id564_ListIndex = myNameTable.Add(@"ListIndex");
            id991_FieldNameForText = myNameTable.Add(@"FieldNameForText");
            id693_Temp20200817 = myNameTable.Add(@"Temp20200817");
            id958_SymbolStyle = myNameTable.Add(@"SymbolStyle");
            id430_ConnectionName = myNameTable.Add(@"ConnectionName");
            id529_EnableLastSelectedListItems = myNameTable.Add(@"EnableLastSelectedListItems");
            id175_Subscript = myNameTable.Add(@"Subscript");
            id1014_ShowBackColor = myNameTable.Add(@"ShowBackColor");
            id450_CloneType = myNameTable.Add(@"CloneType");
            id665_Checked = myNameTable.Add(@"Checked");
            id380_SourceID = myNameTable.Add(@"SourceID");
            id285_FileSystemName = myNameTable.Add(@"FileSystemName");
            id537_GetValueOrderByTime = myNameTable.Add(@"GetValueOrderByTime");
            id674_SpecifyPageIndexInfo = myNameTable.Add(@"SpecifyPageIndexInfo");
            id112_Watermark = myNameTable.Add(@"Watermark");
            id588_TextAlignment = myNameTable.Add(@"TextAlignment");
            id752_LastVerifyResult = myNameTable.Add(@"LastVerifyResult");
            id683_ViewState = myNameTable.Add(@"ViewState");
            id256_MRID = myNameTable.Add(@"MRID");
            id868_BothBlackWhenPrint = myNameTable.Add(@"BothBlackWhenPrint");
            id390_XDataBinding = myNameTable.Add(@"XDataBinding");
            id39_PrintVisibilityExpression = myNameTable.Add(@"PrintVisibilityExpression");
            id376_XTextBody = myNameTable.Add(@"XTextBody");
            id634_ShapeWireLabelElement = myNameTable.Add(@"ShapeWireLabelElement");
            id727_ReferencedTopicID = myNameTable.Add(@"ReferencedTopicID");
            id620_PageImageInfo = myNameTable.Add(@"PageImageInfo");
            id641_ShapeDocumentImagePage = myNameTable.Add(@"ShapeDocumentImagePage");
            id306_DeleterIndex = myNameTable.Add(@"DeleterIndex");
            id139_ValueIndex = myNameTable.Add(@"ValueIndex");
            id692_FileContentType = myNameTable.Add(@"FileContentType");
            id799_LightCorrectionFactor = myNameTable.Add(@"LightCorrectionFactor");
            id234_GridSpanInCM = myNameTable.Add(@"GridSpanInCM");
            id178_SpacingBeforeParagraph = myNameTable.Add(@"SpacingBeforeParagraph");
            id129_RightMargin = myNameTable.Add(@"RightMargin");
            id626_LocalElementStyleMode = myNameTable.Add(@"LocalElementStyleMode");
            id796_ThemeType = myNameTable.Add(@"ThemeType");
            id509_UserEditable = myNameTable.Add(@"UserEditable");
            id498_BackgroundTextItalic = myNameTable.Add(@"BackgroundTextItalic");
            id947_YSplitNum = myNameTable.Add(@"YSplitNum");
            id114_Item = myNameTable.Add(@"PageIndexsForShowBackgroundImage");
            id659_CheckAlignLeft = myNameTable.Add(@"CheckAlignLeft");
            id354_XTextContainerElement = myNameTable.Add(@"XTextContainerElement");
            id145_Alpha = myNameTable.Add(@"Alpha");
            id296_PrintBackColor = myNameTable.Add(@"PrintBackColor");
            id580_DisplayLevel = myNameTable.Add(@"DisplayLevel");
            id520_DefaultValueType = myNameTable.Add(@"DefaultValueType");
            id583_ShowGrid = myNameTable.Add(@"ShowGrid");
            id406_CheckMaxValue = myNameTable.Add(@"CheckMaxValue");
            id840_Data = myNameTable.Add(@"Data");
            id6_DataEncryptProviderName = myNameTable.Add(@"DataEncryptProviderName");
            id108_ForPOSPrinter = myNameTable.Add(@"ForPOSPrinter");
            id88_LocalConfig = myNameTable.Add(@"LocalConfig");
            id104_TerminalText = myNameTable.Add(@"TerminalText");
            id921_NumOfDaysInOnePage = myNameTable.Add(@"NumOfDaysInOnePage");
            id330_XNewMedicalExpression = myNameTable.Add(@"XNewMedicalExpression");
            id472_AllowUserDeleteRow = myNameTable.Add(@"AllowUserDeleteRow");
            id897_ImagePixelWidth = myNameTable.Add(@"ImagePixelWidth");
            id749_UseInnerSignProvider = myNameTable.Add(@"UseInnerSignProvider");
            id32_ValidateStyle = myNameTable.Add(@"ValidateStyle");
            id92_JointPrintNumber = myNameTable.Add(@"JointPrintNumber");
            id499_LableUnitTextBold = myNameTable.Add(@"LableUnitTextBold");
            id257_TimeoutHours = myNameTable.Add(@"TimeoutHours");
            id790_LegendStyle = myNameTable.Add(@"LegendStyle");
            id428_DCEmitDataSource = myNameTable.Add(@"DCEmitDataSource");
            id657_PrintTextForChecked = myNameTable.Add(@"PrintTextForChecked");
            id954_ShowPointValue = myNameTable.Add(@"ShowPointValue");
            id938_SeparatorLineVisible = myNameTable.Add(@"SeparatorLineVisible");
            id681_HostMode = myNameTable.Add(@"HostMode");
            id689_LoopPlay = myNameTable.Add(@"LoopPlay");
            id456_CompressOwnerLineSpacing = myNameTable.Add(@"CompressOwnerLineSpacing");
            id880_SelectionMode = myNameTable.Add(@"SelectionMode");
            id476_AllowUserToResizeRows = myNameTable.Add(@"AllowUserToResizeRows");
            id258_Version = myNameTable.Add(@"Version");
            id465_NumOfRows = myNameTable.Add(@"NumOfRows");
            id388_OwnerUserID = myNameTable.Add(@"OwnerUserID");
            id93_PrintZoomRate = myNameTable.Add(@"PrintZoomRate");
            id849_SymbolOffsetY = myNameTable.Add(@"SymbolOffsetY");
            id848_SymbolOffsetX = myNameTable.Add(@"SymbolOffsetX");
            id173_UnderlineColor = myNameTable.Add(@"UnderlineColor");
            id940_ValueVisible = myNameTable.Add(@"ValueVisible");
            id1007_ValueAlign = myNameTable.Add(@"ValueAlign");
            id323_XTextLock = myNameTable.Add(@"XTextLock");
            id497_TextColor = myNameTable.Add(@"TextColor");
            id308_ContentStyle = myNameTable.Add(@"ContentStyle");
            id1023_SeperatorChar = myNameTable.Add(@"SeperatorChar");
            id718_ObjectParameter = myNameTable.Add(@"ObjectParameter");
            id443_MoveFocusHotKey = myNameTable.Add(@"MoveFocusHotKey");
            id356_XBean = myNameTable.Add(@"XBean");
            id82_DocumentContentVersion = myNameTable.Add(@"DocumentContentVersion");
            id73_ContentStyles = myNameTable.Add(@"ContentStyles");
            id581_ShowPageIndex = myNameTable.Add(@"ShowPageIndex");
            id736_ImageForDown = myNameTable.Add(@"ImageForDown");
            id867_TemperatureDocumentConfig = myNameTable.Add(@"TemperatureDocumentConfig");
            id856_UseAdvVerticalStyle2 = myNameTable.Add(@"UseAdvVerticalStyle2");
            id403_BinaryLength = myNameTable.Add(@"BinaryLength");
            id507_UnitText = myNameTable.Add(@"UnitText");
            id200_BorderWidth = myNameTable.Add(@"BorderWidth");
            id119_AutoFitPageSize = myNameTable.Add(@"AutoFitPageSize");
            id143_ColorValue = myNameTable.Add(@"ColorValue");
            id923_Line = myNameTable.Add(@"Line");
            id459_Printed = myNameTable.Add(@"Printed");
            id650_LabelAtLeft = myNameTable.Add(@"LabelAtLeft");
            id541_MultiSelect = myNameTable.Add(@"MultiSelect");
            id668_VisualStyle = myNameTable.Add(@"VisualStyle");
            id748_SignTime = myNameTable.Add(@"SignTime");
            id953_ShowLegendInRule = myNameTable.Add(@"ShowLegendInRule");
            id57_ScriptText = myNameTable.Add(@"ScriptText");
            id412_DateTimeMaxValue = myNameTable.Add(@"DateTimeMaxValue");
            id501_BorderTextPosition = myNameTable.Add(@"BorderTextPosition");
            id746_DefaultSignMode = myNameTable.Add(@"DefaultSignMode");
            id171_EmphasisMark = myNameTable.Add(@"EmphasisMark");
            id492_EndingLineBreak = myNameTable.Add(@"EndingLineBreak");
            id469_Alignment = myNameTable.Add(@"Alignment");
            id404_MaxLength = myNameTable.Add(@"MaxLength");
            id805_StartCap = myNameTable.Add(@"StartCap");
            id819_ChartDataItem = myNameTable.Add(@"ChartDataItem");
            id49_EventTemplateName = myNameTable.Add(@"EventTemplateName");
            id597_SaveLinkedContent = myNameTable.Add(@"SaveLinkedContent");
            id448_DesignColIndex = myNameTable.Add(@"DesignColIndex");
            id759_DataFieldNameForValue = myNameTable.Add(@"DataFieldNameForValue");
            id671_PageIndexFix = myNameTable.Add(@"PageIndexFix");
            id229_MinHeightInCMUnit = myNameTable.Add(@"MinHeightInCMUnit");
            id83_Info = myNameTable.Add(@"Info");
            id260_LicenseText = myNameTable.Add(@"LicenseText");
            id59_JavaScriptTextForWebClient = myNameTable.Add(@"JavaScriptTextForWebClient");
            id1055_IsCustomFill = myNameTable.Add(@"IsCustomFill");
            id903_Ticks = myNameTable.Add(@"Ticks");
            id275_StartPositionInPringJob = myNameTable.Add(@"StartPositionInPringJob");
            id334_XChart = myNameTable.Add(@"XChart");
            id408_MaxValue = myNameTable.Add(@"MaxValue");
            id666_DefaultCheckedForValueBinding = myNameTable.Add(@"DefaultCheckedForValueBinding");
            id47_ScriptItems = myNameTable.Add(@"ScriptItems");
            id194_CharacterCircle = myNameTable.Add(@"CharacterCircle");
            id544_ListValueFormatString = myNameTable.Add(@"ListValueFormatString");
            id299_ProtectType = myNameTable.Add(@"ProtectType");
            id538_EditStyle = myNameTable.Add(@"EditStyle");
            id446_ColSpan = myNameTable.Add(@"ColSpan");
            id58_ScriptLanguage = myNameTable.Add(@"ScriptLanguage");
            id268_DocumentFormat = myNameTable.Add(@"DocumentFormat");
            id952_ValueTextBackColorValue = myNameTable.Add(@"ValueTextBackColorValue");
            id1035_PaperSizeName = myNameTable.Add(@"PaperSizeName");
            id909_BigVerticalGridLineColorValue = myNameTable.Add(@"BigVerticalGridLineColorValue");
            id263_LastPrintTime = myNameTable.Add(@"LastPrintTime");
            id36_ElementIDForEditableDependent = myNameTable.Add(@"ElementIDForEditableDependent");
            id362_XAccountingNumber = myNameTable.Add(@"XAccountingNumber");
            id280_NewPage = myNameTable.Add(@"NewPage");
            id349_XTextRadioBox = myNameTable.Add(@"XTextRadioBox");
            id202_BorderBottom = myNameTable.Add(@"BorderBottom");
            id615_AdditionShapeFixSize = myNameTable.Add(@"AdditionShapeFixSize");
            id167_BackgroundRepeat = myNameTable.Add(@"BackgroundRepeat");
            id398_AutoUpdate = myNameTable.Add(@"AutoUpdate");
            id124_PaperWidth = myNameTable.Add(@"PaperWidth");
            id386_DomExpression = myNameTable.Add(@"DomExpression");
            id864_CustomImage = myNameTable.Add(@"CustomImage");
            id652_HyperlinkInfo = myNameTable.Add(@"HyperlinkInfo");
            id750_SignRange = myNameTable.Add(@"SignRange");
            id230_DCGridLineInfo = myNameTable.Add(@"DCGridLineInfo");
            id822_SymbolType = myNameTable.Add(@"SymbolType");
            id679_TypeFullName = myNameTable.Add(@"TypeFullName");
            id214_Zoom = myNameTable.Add(@"Zoom");
            id337_XTextButton = myNameTable.Add(@"XTextButton");
            id582_AutoExitEditMode = myNameTable.Add(@"AutoExitEditMode");
            id411_MaxDecimalDigits = myNameTable.Add(@"MaxDecimalDigits");
            id100_SwapLeftRightMargin = myNameTable.Add(@"SwapLeftRightMargin");
            id161_BackgroundStyle = myNameTable.Add(@"BackgroundStyle");
            id645_SmoothZoomIn = myNameTable.Add(@"SmoothZoomIn");
            id111_PrinterName = myNameTable.Add(@"PrinterName");
            id76_MotherTemplate = myNameTable.Add(@"MotherTemplate");
            id542_DynamicListItems = myNameTable.Add(@"DynamicListItems");
            id248_Value = myNameTable.Add(@"Value");
            id855_UseAdvVerticalStyle = myNameTable.Add(@"UseAdvVerticalStyle");
            id1046_DivCharForMultiMode = myNameTable.Add(@"DivCharForMultiMode");
            id72_History = myNameTable.Add(@"History");
            id808_ChartLabelStyle = myNameTable.Add(@"ChartLabelStyle");
            id531_FieldSettings = myNameTable.Add(@"FieldSettings");
            id222_ParagraphOutlineLevel = myNameTable.Add(@"ParagraphOutlineLevel");
            id213_PaddingBottom = myNameTable.Add(@"PaddingBottom");
            id261_LastModifiedTime = myNameTable.Add(@"LastModifiedTime");
            id128_TopMargin = myNameTable.Add(@"TopMargin");
            id788_ChartCaptionStyle = myNameTable.Add(@"ChartCaptionStyle");
            id572_AutoSetFirstItems = myNameTable.Add(@"AutoSetFirstItems");
            id493_StartBorderText = myNameTable.Add(@"StartBorderText");
            id477_ShowCellNoneBorder = myNameTable.Add(@"ShowCellNoneBorder");
            id169_FontName = myNameTable.Add(@"FontName");
            id300_TitleLevel = myNameTable.Add(@"TitleLevel");
            id262_EditMinute = myNameTable.Add(@"EditMinute");
            id475_AllowUserToResizeColumns = myNameTable.Add(@"AllowUserToResizeColumns");
            id654_Requried = myNameTable.Add(@"Requried");
            id577_Target = myNameTable.Add(@"Target");
            id526_EditorActiveMode = myNameTable.Add(@"EditorActiveMode");
            id189_RightToLeft = myNameTable.Add(@"RightToLeft");
            id1019_UpAndDownTextType = myNameTable.Add(@"UpAndDownTextType");
            id933_LanternValueColorForUpValue = myNameTable.Add(@"LanternValueColorForUpValue");
            id978_NormalRangeBackColorValue = myNameTable.Add(@"NormalRangeBackColorValue");
            id803_DashCap = myNameTable.Add(@"DashCap");
            id777_GroupGridLine = myNameTable.Add(@"GroupGridLine");
            id737_ImageForMouseOver = myNameTable.Add(@"ImageForMouseOver");
            id133_XImageValue = myNameTable.Add(@"XImageValue");
            id984_ValuePointDataSourceInfo = myNameTable.Add(@"ValuePointDataSourceInfo");
            id888_Images = myNameTable.Add(@"Images");
            id841_DocumentData = myNameTable.Add(@"DocumentData");
            id34_ValueBinding = myNameTable.Add(@"ValueBinding");
            id473_AllowUserInsertRow = myNameTable.Add(@"AllowUserInsertRow");
            id142_Font = myNameTable.Add(@"Font");
            id567_UserFlag = myNameTable.Add(@"UserFlag");
            id977_ScaleRate = myNameTable.Add(@"ScaleRate");
            id608_EnableRepeatedImage = myNameTable.Add(@"EnableRepeatedImage");
            id883_TickUnit = myNameTable.Add(@"TickUnit");
            id540_RepulsionForGroup = myNameTable.Add(@"RepulsionForGroup");
            id249_DocumentInfo = myNameTable.Add(@"DocumentInfo");
            id192_RoundRadio = myNameTable.Add(@"RoundRadio");
            id198_BorderBottomColor = myNameTable.Add(@"BorderBottomColor");
            id787_YAxisCaptions = myNameTable.Add(@"YAxisCaptions");
            id702_KBEntries = myNameTable.Add(@"KBEntries");
            id744_SignMessage = myNameTable.Add(@"SignMessage");
            id878_EnableCustomValuePointSymbol = myNameTable.Add(@"EnableCustomValuePointSymbol");
            id218_Height = myNameTable.Add(@"Height");
            id960_TitleBackColorValue = myNameTable.Add(@"TitleBackColorValue");
            id402_Required = myNameTable.Add(@"Required");
            id274_UseLanguage2 = myNameTable.Add(@"UseLanguage2");
            id1030_GridYSpaceNum = myNameTable.Add(@"GridYSpaceNum");
            id846_TagValue = myNameTable.Add(@"TagValue");
            id107_PageBorderBackground = myNameTable.Add(@"PageBorderBackground");
            id807_MiterLimit = myNameTable.Add(@"MiterLimit");
            id12_EncryptContent = myNameTable.Add(@"EncryptContent");
            id843_ValuePoint = myNameTable.Add(@"ValuePoint");
            id155_Underline = myNameTable.Add(@"Underline");
            id522_CustomAdornText = myNameTable.Add(@"CustomAdornText");
            id484_GenerateByValueBingding = myNameTable.Add(@"GenerateByValueBingding");
            id238_Title = myNameTable.Add(@"Title");
            id644_ZoomInRate = myNameTable.Add(@"ZoomInRate");
            id281_BorderColorValue = myNameTable.Add(@"BorderColorValue");
            id164_BackgroundPosition = myNameTable.Add(@"BackgroundPosition");
            id1038_LineAlignment = myNameTable.Add(@"LineAlignment");
            id747_SignProviderName = myNameTable.Add(@"SignProviderName");
            id427_KeyFeildDataSourcePath = myNameTable.Add(@"KeyFeildDataSourcePath");
            id149_Angle = myNameTable.Add(@"Angle");
            id364_XInputField = myNameTable.Add(@"XInputField");
            id910_PageBackColorValue = myNameTable.Add(@"PageBackColorValue");
            id196_BorderTopColor = myNameTable.Add(@"BorderTopColor");
            id573_ValueFormater = myNameTable.Add(@"ValueFormater");
            id46_Expression = myNameTable.Add(@"Expression");
            id699_ListItemsSourceFormatString = myNameTable.Add(@"ListItemsSourceFormatString");
            id278_AllowSave = myNameTable.Add(@"AllowSave");
            id56_WebClientHtmlText = myNameTable.Add(@"WebClientHtmlText");
            id193_Rotate = myNameTable.Add(@"Rotate");
            id1012_StartDateKeyword = myNameTable.Add(@"StartDateKeyword");
            id962_TitleColorValue = myNameTable.Add(@"TitleColorValue");
            id348_XTextCheckBoxElementBase = myNameTable.Add(@"XTextCheckBoxElementBase");
            id757_DataFieldNameForGroupName = myNameTable.Add(@"DataFieldNameForGroupName");
            id131_Landscape = myNameTable.Add(@"Landscape");
            id205_BorderSpacing = myNameTable.Add(@"BorderSpacing");
            id29_PropertyExpressions = myNameTable.Add(@"PropertyExpressions");
            id1054_IsSelect = myNameTable.Add(@"IsSelect");
            id9_Attribute = myNameTable.Add(@"Attribute");
            id182_LineSpacing = myNameTable.Add(@"LineSpacing");
            id1018_ForceUpWhenPageFirstPoint = myNameTable.Add(@"ForceUpWhenPageFirstPoint");
            id199_BorderStyle = myNameTable.Add(@"BorderStyle");
            id495_BorderElementColor = myNameTable.Add(@"BorderElementColor");
            id166_BackgroundPositionY = myNameTable.Add(@"BackgroundPositionY");
            id165_BackgroundPositionX = myNameTable.Add(@"BackgroundPositionX");
            id833_DownleadLength = myNameTable.Add(@"DownleadLength");
            id453_ForeColorValueForCollapsed = myNameTable.Add(@"ForeColorValueForCollapsed");
            id357_XContentLinkField = myNameTable.Add(@"XContentLinkField");
            id293_Style = myNameTable.Add(@"Style");
            id1001_EnableEndTime = myNameTable.Add(@"EnableEndTime");
            id292_Styles = myNameTable.Add(@"Styles");
            id893_SpecifyEndDate = myNameTable.Add(@"SpecifyEndDate");
            id433_FieldsForDesign = myNameTable.Add(@"FieldsForDesign");
            id1020_ValueTextMultiLine = myNameTable.Add(@"ValueTextMultiLine");
            id534_ShowFormButton = myNameTable.Add(@"ShowFormButton");
            id220_PageBreakBefore = myNameTable.Add(@"PageBreakBefore");
            id754_ImageData = myNameTable.Add(@"ImageData");
            id987_FieldNameForTitle = myNameTable.Add(@"FieldNameForTitle");
            id23_BringoutToSave = myNameTable.Add(@"BringoutToSave");
            id943_HollowCovertTargetName = myNameTable.Add(@"HollowCovertTargetName");
            id42_EnablePermission = myNameTable.Add(@"EnablePermission");
            id751_SignUserID = myNameTable.Add(@"SignUserID");
            id525_EnableFieldTextColor = myNameTable.Add(@"EnableFieldTextColor");
            id604_LinkInfo = myNameTable.Add(@"LinkInfo");
            id836_PieDataItem = myNameTable.Add(@"PieDataItem");
            id510_SelectedSpellCode = myNameTable.Add(@"SelectedSpellCode");
            id423_TableName = myNameTable.Add(@"TableName");
            id223_VisibleInDirectory = myNameTable.Add(@"VisibleInDirectory");
            id596_ReplaceUpdateMode = myNameTable.Add(@"ReplaceUpdateMode");
            id548_ListSourceInfo = myNameTable.Add(@"ListSourceInfo");
            id824_PieDocumentStyle = myNameTable.Add(@"PieDocumentStyle");
            id850_SpecifyLanternSymbolStyle = myNameTable.Add(@"SpecifyLanternSymbolStyle");
            id593_ExpressionStyle = myNameTable.Add(@"ExpressionStyle");
            id122_FooterDistance = myNameTable.Add(@"FooterDistance");
            id409_MinValue = myNameTable.Add(@"MinValue");
            id462_ImportUserTrack = myNameTable.Add(@"ImportUserTrack");
            id663_AutoHeightForMultiline = myNameTable.Add(@"AutoHeightForMultiline");
            id980_NormalMaxValue = myNameTable.Add(@"NormalMaxValue");
            id438_TabStop = myNameTable.Add(@"TabStop");
            id287_ImportPageSettings = myNameTable.Add(@"ImportPageSettings");
            id341_XTextLabelElement = myNameTable.Add(@"XTextLabelElement");
            id955_ColorValueForPointValue = myNameTable.Add(@"ColorValueForPointValue");
            id800_XColorValue = myNameTable.Add(@"XColorValue");
            id554_ListItem = myNameTable.Add(@"ListItem");
            id327_XBookMark = myNameTable.Add(@"XBookMark");
            id739_CommandName = myNameTable.Add(@"CommandName");
            id170_FontSize = myNameTable.Add(@"FontSize");
            id96_GutterStyle = myNameTable.Add(@"GutterStyle");
            id550_DisplayPath = myNameTable.Add(@"DisplayPath");
            id405_MinLength = myNameTable.Add(@"MinLength");
            id177_SpacingAfterParagraph = myNameTable.Add(@"SpacingAfterParagraph");
            id545_ListValueSeparatorChar = myNameTable.Add(@"ListValueSeparatorChar");
            id1003_ValueDisplayFormat = myNameTable.Add(@"ValueDisplayFormat");
            id899_ShadowPointDetectSeconds = myNameTable.Add(@"ShadowPointDetectSeconds");
            id432_ParameterStyle = myNameTable.Add(@"ParameterStyle");
            id928_MergeIntoLeft = myNameTable.Add(@"MergeIntoLeft");
            id325_XTextTableColumn = myNameTable.Add(@"XTextTableColumn");
            id195_BorderLeftColor = myNameTable.Add(@"BorderLeftColor");
            id974_Scales = myNameTable.Add(@"Scales");
            id90_PageSettings = myNameTable.Add(@"PageSettings");
            id101_SpecifyDuplex = myNameTable.Add(@"SpecifyDuplex");
            id148_DensityForRepeat = myNameTable.Add(@"DensityForRepeat");
            id372_XTextFooterForFirstPage = myNameTable.Add(@"XTextFooterForFirstPage");
            id639_ShapeContainerElement = myNameTable.Add(@"ShapeContainerElement");
            id837_TemperatureDocument = myNameTable.Add(@"TemperatureDocument");
            id690_EnableMediaContextMenu = myNameTable.Add(@"EnableMediaContextMenu");
            id742_SignUserName = myNameTable.Add(@"SignUserName");
            id568_IsRoot = myNameTable.Add(@"IsRoot");
            id511_InnerValue = myNameTable.Add(@"InnerValue");
            id288_ImportHeader = myNameTable.Add(@"ImportHeader");
            id951_RedLineWidth = myNameTable.Add(@"RedLineWidth");
            id420_PropertyExpressionInfo = myNameTable.Add(@"PropertyExpressionInfo");
            id512_PrintBackgroundText = myNameTable.Add(@"PrintBackgroundText");
            id1052_RatioToPointFsList = myNameTable.Add(@"RatioToPointFsList");
            id463_DelayLoadWhenExpand = myNameTable.Add(@"DelayLoadWhenExpand");
            id115_EditTimeBackgroundImage = myNameTable.Add(@"EditTimeBackgroundImage");
            id426_KeyFieldValue = myNameTable.Add(@"KeyFieldValue");
            id781_Thickness = myNameTable.Add(@"Thickness");
            id872_EditValuePointMode = myNameTable.Add(@"EditValuePointMode");
            id378_SpecifyFixedLineHeight = myNameTable.Add(@"SpecifyFixedLineHeight");
            id844_VerifiedColorValue = myNameTable.Add(@"VerifiedColorValue");
            id17_LimitedInputChars = myNameTable.Add(@"LimitedInputChars");
            id1008_MaxValueForDayIndex = myNameTable.Add(@"MaxValueForDayIndex");
            id340_NewBarcode = myNameTable.Add(@"NewBarcode");
            id579_TargetPropertyName = myNameTable.Add(@"TargetPropertyName");
            id508_LabelText = myNameTable.Add(@"LabelText");
            id15_EmitDataSource = myNameTable.Add(@"EmitDataSource");
            id55_Deleteable = myNameTable.Add(@"Deleteable");
            id828_PieOpacity = myNameTable.Add(@"PieOpacity");
            id418_CustomMessage = myNameTable.Add(@"CustomMessage");
            id656_PrintVisibilityWhenUnchecked = myNameTable.Add(@"PrintVisibilityWhenUnchecked");
            id802_DashStyle = myNameTable.Add(@"DashStyle");
            id466_NumOfColumns = myNameTable.Add(@"NumOfColumns");
            id613_SaveContentInFile = myNameTable.Add(@"SaveContentInFile");
            id436_DCEmitDataSourceFieldInfo = myNameTable.Add(@"DCEmitDataSourceFieldInfo");
            id1062_WhitespaceCount = myNameTable.Add(@"WhitespaceCount");
            id871_DebugMode = myNameTable.Add(@"DebugMode");
            id1056_ImgBase64ForCustomFill = myNameTable.Add(@"ImgBase64ForCustomFill");
            id468_DataForReValueBinding = myNameTable.Add(@"DataForReValueBinding");
            id906_PageIndexFont = myNameTable.Add(@"PageIndexFont");
            id98_EnableHeaderFooter = myNameTable.Add(@"EnableHeaderFooter");
            id760_DataFieldNameForLink = myNameTable.Add(@"DataFieldNameForLink");
            id942_LanternValueTitle = myNameTable.Add(@"LanternValueTitle");
            id691_PlayerUIMode = myNameTable.Add(@"PlayerUIMode");
            id64_Parameters = myNameTable.Add(@"Parameters");
            id817_GridStep = myNameTable.Add(@"GridStep");
            id574_NoneText = myNameTable.Add(@"NoneText");
            id137_ImageDataBase64String = myNameTable.Add(@"ImageDataBase64String");
            id66_DetectRepeatImageForSave = myNameTable.Add(@"DetectRepeatImageForSave");
            id990_FieldNameForLanternValue = myNameTable.Add(@"FieldNameForLanternValue");
            id677_DelayLoadControl = myNameTable.Add(@"DelayLoadControl");
            id264_AuthorName = myNameTable.Add(@"AuthorName");
            id578_CustomTargetName = myNameTable.Add(@"CustomTargetName");
            id1011_StartDate = myNameTable.Add(@"StartDate");
            id1013_PreserveStartKeywordOrder = myNameTable.Add(@"PreserveStartKeywordOrder");
            id407_CheckMinValue = myNameTable.Add(@"CheckMinValue");
            id320_DefaultValue = myNameTable.Add(@"DefaultValue");
            id332_XTemperatureChart = myNameTable.Add(@"XTemperatureChart");
            id558_Text2 = myNameTable.Add(@"Text2");
            id882_AllowUserCollapseZone = myNameTable.Add(@"AllowUserCollapseZone");
            id931_ValuePrecision = myNameTable.Add(@"ValuePrecision");
            id630_DefaultFont = myNameTable.Add(@"DefaultFont");
            id821_TipText = myNameTable.Add(@"TipText");
            id589_ShowText = myNameTable.Add(@"ShowText");
            id1021_HeaderLabelInfo = myNameTable.Add(@"HeaderLabelInfo");
            id815_LeftSide = myNameTable.Add(@"LeftSide");
            id2_Item = myNameTable.Add(string.Empty);
            id393_FormatString = myNameTable.Add(@"FormatString");
            id414_Range = myNameTable.Add(@"Range");
            id226_BorderRange = myNameTable.Add(@"BorderRange");
            id536_InputFieldSettings = myNameTable.Add(@"InputFieldSettings");
            id265_AuthorPermissionLevel = myNameTable.Add(@"AuthorPermissionLevel");
            id908_BigVerticalGridLineWidth = myNameTable.Add(@"BigVerticalGridLineWidth");
            id712_ParentID = myNameTable.Add(@"ParentID");
            id252_ShowHeaderBottomLine = myNameTable.Add(@"ShowHeaderBottomLine");
            id901_Zones = myNameTable.Add(@"Zones");
            id224_ParagraphListStyle = myNameTable.Add(@"ParagraphListStyle");
            id160_BackgroundColor2 = myNameTable.Add(@"BackgroundColor2");
            id437_EnabledTransprentEncrypt = myNameTable.Add(@"EnabledTransprentEncrypt");
            id535_FormButtonStyle = myNameTable.Add(@"FormButtonStyle");
            id85_Comments = myNameTable.Add(@"Comments");
            id633_ShapeLineElement = myNameTable.Add(@"ShapeLineElement");
            id587_BarcodeStyle = myNameTable.Add(@"BarcodeStyle");
            id618_SmoothZoom = myNameTable.Add(@"SmoothZoom");
            id949_AlertLineColorValue = myNameTable.Add(@"AlertLineColorValue");
            id994_VisibleWhenNoValuePoint = myNameTable.Add(@"VisibleWhenNoValuePoint");
            id168_Color = myNameTable.Add(@"Color");
            id467_AllowReBindingDataSource = myNameTable.Add(@"AllowReBindingDataSource");
            id245_ForeColor = myNameTable.Add(@"ForeColor");
            id359_XTextShapeInputFieldElement = myNameTable.Add(@"XTextShapeInputFieldElement");
            id661_CaptionFlowLayout = myNameTable.Add(@"CaptionFlowLayout");
            id623_Resizeable = myNameTable.Add(@"Resizeable");
            id713_EntryTemplateContent = myNameTable.Add(@"EntryTemplateContent");
            id766_TextColorValue = myNameTable.Add(@"TextColorValue");
            id366_XTextTable = myNameTable.Add(@"XTextTable");
            id474_Item = myNameTable.Add(@"AllowUserToResizeEvenInFormViewMode");
            id1039_Item = myNameTable.Add(@"PositionFixModeForAutoHeightLine");
            id726_Item = myNameTable.Add(@"AllowUserEditCurrentPageLabelText");
            id439_BorderPrintedWhenJumpPrint = myNameTable.Add(@"BorderPrintedWhenJumpPrint");
            id1036_DCTimeLineLabel = myNameTable.Add(@"DCTimeLineLabel");
            id311_SavedTime = myNameTable.Add(@"SavedTime");
            id486_PrintCellBackground = myNameTable.Add(@"PrintCellBackground");
            id479_SubfieldMode = myNameTable.Add(@"SubfieldMode");
            id944_ShadowName = myNameTable.Add(@"ShadowName");
            id244_BackColor = myNameTable.Add(@"BackColor");
            id1000_ExtendGridLineType = myNameTable.Add(@"ExtendGridLineType");
            id247_XAttribute = myNameTable.Add(@"XAttribute");
            id480_SubfieldNumber = myNameTable.Add(@"SubfieldNumber");
            id553_BufferItems = myNameTable.Add(@"BufferItems");
            id922_HeaderLines = myNameTable.Add(@"HeaderLines");
            id400_Level = myNameTable.Add(@"Level");
            id932_AllowInterrupt = myNameTable.Add(@"AllowInterrupt");
            id421_AllowChainReaction = myNameTable.Add(@"AllowChainReaction");
            id184_Align = myNameTable.Add(@"Align");
            id743_SignClientName = myNameTable.Add(@"SignClientName");
            id360_XMedicalExpressionField = myNameTable.Add(@"XMedicalExpressionField");
            id156_Strikeout = myNameTable.Add(@"Strikeout");
            id179_LayoutGridHeight = myNameTable.Add(@"LayoutGridHeight");
            id761_DataFieldNameForTipText = myNameTable.Add(@"DataFieldNameForTipText");
            id610_Alt = myNameTable.Add(@"Alt");
            id780_AxisCompress = myNameTable.Add(@"AxisCompress");
            id457_SpecifyHeight = myNameTable.Add(@"SpecifyHeight");
            id283_PageIndex = myNameTable.Add(@"PageIndex");
            id735_PrintAsText = myNameTable.Add(@"PrintAsText");
            id628_AutoZoomFontSize = myNameTable.Add(@"AutoZoomFontSize");
            id294_DocumentContentStyle = myNameTable.Add(@"DocumentContentStyle");
            id979_OutofNormalRangeBackColorValue = myNameTable.Add(@"OutofNormalRangeBackColorValue");
            id555_EntryID = myNameTable.Add(@"EntryID");
            id102_PowerDocumentGridLine = myNameTable.Add(@"PowerDocumentGridLine");
            id716_OwnerLevel = myNameTable.Add(@"OwnerLevel");
            id877_TitleForToolTip = myNameTable.Add(@"TitleForToolTip");
            id313_ClientName = myNameTable.Add(@"ClientName");
            id26_AcceptTab = myNameTable.Add(@"AcceptTab");
            id375_XTextHeader = myNameTable.Add(@"XTextHeader");
            id371_XTextDocumentContentElement = myNameTable.Add(@"XTextDocumentContentElement");
            id886_SQLTextForHeaderLabel = myNameTable.Add(@"SQLTextForHeaderLabel");
            id1009_CircleText = myNameTable.Add(@"CircleText");
            id434_Field = myNameTable.Add(@"Field");
            id369_XTextSubDocument = myNameTable.Add(@"XTextSubDocument");
            id16_AutoFixTextMode = myNameTable.Add(@"AutoFixTextMode");
            id627_TextBackColorString = myNameTable.Add(@"TextBackColorString");
            id31_EnableValueValidate = myNameTable.Add(@"EnableValueValidate");
            id505_DefaultEventExpression = myNameTable.Add(@"DefaultEventExpression");
            id646_X1 = myNameTable.Add(@"X1");
            id647_Y1 = myNameTable.Add(@"Y1");
            id543_ListSource = myNameTable.Add(@"ListSource");
            id784_HorizontalTextAlign = myNameTable.Add(@"HorizontalTextAlign");
            id502_FastInputMode = myNameTable.Add(@"FastInputMode");
            id35_DefaultValueForValueBinding = myNameTable.Add(@"DefaultValueForValueBinding");
            id528_DefaultSelectedIndexs = myNameTable.Add(@"DefaultSelectedIndexs");
            id152_Unit = myNameTable.Add(@"Unit");
            id159_BackgroundColor = myNameTable.Add(@"BackgroundColor");
            id1045_IsIdentyPartition = myNameTable.Add(@"IsIdentyPartition");
            id344_HorizontalLine = myNameTable.Add(@"HorizontalLine");
            id352_XTextEOFElement = myNameTable.Add(@"XTextEOFElement");
            id1033_ThinLineWidth = myNameTable.Add(@"ThinLineWidth");
            id919_SpecifyTitleHeight = myNameTable.Add(@"SpecifyTitleHeight");
            id830_DrawingStyle = myNameTable.Add(@"DrawingStyle");
            id603_ZOrderStyle = myNameTable.Add(@"ZOrderStyle");
            id286_Format = myNameTable.Add(@"Format");
            id68_FileName = myNameTable.Add(@"FileName");
            id442_AutoFixFontSize = myNameTable.Add(@"AutoFixFontSize");
            id81_SpecialTag = myNameTable.Add(@"SpecialTag");
            id916_DateFormatStringForCrossWeek = myNameTable.Add(@"DateFormatStringForCrossWeek");
            id63_SerializeParameterValue = myNameTable.Add(@"SerializeParameterValue");
            id773_CustomColorThemeH = myNameTable.Add(@"CustomColorThemeH");
            id706_KBTemplateDocument = myNameTable.Add(@"KBTemplateDocument");
            id186_FirstLineIndent = myNameTable.Add(@"FirstLineIndent");
            id818_TickTextList = myNameTable.Add(@"TickTextList");
            id793_BarBorderPen = myNameTable.Add(@"BarBorderPen");
            id600_ResetListIndexFlag = myNameTable.Add(@"ResetListIndexFlag");
            id820_SeriesName = myNameTable.Add(@"SeriesName");
            id518_ShowInputFieldStateTag = myNameTable.Add(@"ShowInputFieldStateTag");
            id225_DefaultValuePropertyNames = myNameTable.Add(@"DefaultValuePropertyNames");
            id272_Operator = myNameTable.Add(@"Operator");
            id494_EndBorderText = myNameTable.Add(@"EndBorderText");
            id585_UnitMode = myNameTable.Add(@"UnitMode");
            id326_XString = myNameTable.Add(@"XString");
            id576_EventName = myNameTable.Add(@"EventName");
            id688_CsMediaPlayer = myNameTable.Add(@"CsMediaPlayer");
            id80_GlobalJavaScript = myNameTable.Add(@"GlobalJavaScript");
            id197_BorderRightColor = myNameTable.Add(@"BorderRightColor");
            id839_Datas = myNameTable.Add(@"Datas");
            id209_MarginBottom = myNameTable.Add(@"MarginBottom");

        }
    }


    #region 废弃代码

    //[System.Runtime.InteropServices.ComVisible(false)]
    //public sealed class TemperatureDocumentSerializer : System.Xml.Serialization.XmlSerializer
    //{
    //    public override System.Boolean CanDeserialize(System.Xml.XmlReader xmlReader)
    //    {
    //        return xmlReader.IsStartElement(@"TemperatureDocument", string.Empty);
    //    }
    //    protected override void Serialize(object objectToSerialize, System.Xml.Serialization.XmlSerializationWriter writer)
    //    {
    //        ((WriterTemperatureDocumentXmlSerialization)writer).Write_TemperatureDocument(objectToSerialize);
    //    }
    //    protected override object Deserialize(System.Xml.Serialization.XmlSerializationReader reader)
    //    {
    //        return ((ReaderTemperatureDocumentXmlSerialization)reader).Read_TemperatureDocument();
    //    }

    //    protected override XmlSerializationWriter CreateWriter()
    //    {
    //        return new WriterTemperatureDocumentXmlSerialization();
    //    }

    //    protected override XmlSerializationReader CreateReader()
    //    {
    //        return new ReaderTemperatureDocumentXmlSerialization();
    //    }
    //}
    //public class ReaderTemperatureDocumentXmlSerialization : DCXmlSerializationReader
    //{

    //    private string GetXsiTypeName()
    //    {
    //        var reader = this.Reader;
    //        var xsiTypeName = reader.GetAttribute("type", "http://www.w3.org/2001/XMLSchema-instance");
    //        if (xsiTypeName == null)
    //        {
    //            var xsiType = this.GetXsiType();
    //            if (xsiType != null)
    //            {
    //                xsiTypeName = xsiType.Name;
    //            }
    //            if (xsiTypeName != null)
    //            {
    //                xsiTypeName = reader.NameTable.Get(xsiTypeName);
    //            }
    //        }
    //        else
    //        {
    //            xsiTypeName = reader.NameTable.Get(xsiTypeName);
    //        }
    //        return xsiTypeName;
    //    }
    //    private System.Collections.Generic.Dictionary<string, string> _StringValues
    //        = new System.Collections.Generic.Dictionary<string, string>();
    //    private string CacheString(string v)
    //    {
    //        if (v == null || v.Length == 0)
    //        {
    //            return v;
    //        }
    //        string result = v;
    //        if (_StringValues.TryGetValue(v, out result) == false)
    //        {
    //            _StringValues[v] = v;
    //            result = v;
    //        }
    //        return result;
    //    }
    //    internal protected global::System.Drawing.StringAlignment Read119_StringAlignment(string s)
    //    {
    //        switch (s)
    //        {
    //            case @"Near": return global::System.Drawing.StringAlignment.@Near;
    //            case @"Center": return global::System.Drawing.StringAlignment.@Center;
    //            case @"Far": return global::System.Drawing.StringAlignment.@Far;
    //            default: return (default(global::System.Drawing.StringAlignment));
    //        }
    //    }
    //    internal protected global::System.Drawing.Drawing2D.PenAlignment Read191_PenAlignment(string s)
    //    {
    //        switch (s)
    //        {
    //            case @"Center": return global::System.Drawing.Drawing2D.PenAlignment.@Center;
    //            case @"Inset": return global::System.Drawing.Drawing2D.PenAlignment.@Inset;
    //            case @"Outset": return global::System.Drawing.Drawing2D.PenAlignment.@Outset;
    //            case @"Left": return global::System.Drawing.Drawing2D.PenAlignment.@Left;
    //            case @"Right": return global::System.Drawing.Drawing2D.PenAlignment.@Right;
    //            default: return (default(global::System.Drawing.Drawing2D.PenAlignment));
    //        }
    //    }
    //    internal protected global::System.Drawing.Drawing2D.LineCap Read190_LineCap(string s)
    //    {

    //        if (__System_Drawing_Drawing2D_LineCap == null)
    //        {
    //            lock (_NewDictionaryLockObject)
    //            {
    //                var dic20200818 = new System.Collections.Generic.Dictionary<string, System.Drawing.Drawing2D.LineCap>();
    //                dic20200818.Add("Flat", System.Drawing.Drawing2D.LineCap.@Flat);
    //                dic20200818.Add("Square", System.Drawing.Drawing2D.LineCap.@Square);
    //                dic20200818.Add("Round", System.Drawing.Drawing2D.LineCap.@Round);
    //                dic20200818.Add("Triangle", System.Drawing.Drawing2D.LineCap.@Triangle);
    //                dic20200818.Add("NoAnchor", System.Drawing.Drawing2D.LineCap.@NoAnchor);
    //                dic20200818.Add("SquareAnchor", System.Drawing.Drawing2D.LineCap.@SquareAnchor);
    //                dic20200818.Add("RoundAnchor", System.Drawing.Drawing2D.LineCap.@RoundAnchor);
    //                dic20200818.Add("DiamondAnchor", System.Drawing.Drawing2D.LineCap.@DiamondAnchor);
    //                dic20200818.Add("ArrowAnchor", System.Drawing.Drawing2D.LineCap.@ArrowAnchor);
    //                dic20200818.Add("Custom", System.Drawing.Drawing2D.LineCap.@Custom);
    //                dic20200818.Add("AnchorMask", System.Drawing.Drawing2D.LineCap.@AnchorMask);
    //                __System_Drawing_Drawing2D_LineCap = dic20200818;
    //            }
    //        }
    //        var result = default(System.Drawing.Drawing2D.LineCap);
    //        if (__System_Drawing_Drawing2D_LineCap.TryGetValue(s, out result))
    //        {
    //            return result;
    //        }
    //        else
    //        {
    //            return default(System.Drawing.Drawing2D.LineCap);
    //        }
    //    }
    //    private static System.Collections.Generic.Dictionary<string, System.Drawing.Drawing2D.LineCap> __System_Drawing_Drawing2D_LineCap = null;

    //    internal protected global::System.Drawing.Drawing2D.LineJoin Read189_LineJoin(string s)
    //    {
    //        switch (s)
    //        {
    //            case @"Miter": return global::System.Drawing.Drawing2D.LineJoin.@Miter;
    //            case @"Bevel": return global::System.Drawing.Drawing2D.LineJoin.@Bevel;
    //            case @"Round": return global::System.Drawing.Drawing2D.LineJoin.@Round;
    //            case @"MiterClipped": return global::System.Drawing.Drawing2D.LineJoin.@MiterClipped;
    //            default: return (default(global::System.Drawing.Drawing2D.LineJoin));
    //        }
    //    }
    //    internal protected global::System.Drawing.Drawing2D.DashCap Read188_DashCap(string s)
    //    {
    //        switch (s)
    //        {
    //            case @"Flat": return global::System.Drawing.Drawing2D.DashCap.@Flat;
    //            case @"Round": return global::System.Drawing.Drawing2D.DashCap.@Round;
    //            case @"Triangle": return global::System.Drawing.Drawing2D.DashCap.@Triangle;
    //            default: return (default(global::System.Drawing.Drawing2D.DashCap));
    //        }
    //    }
    //    internal protected global::System.Drawing.Drawing2D.DashStyle Read43_DashStyle(string s)
    //    {

    //        if (__System_Drawing_Drawing2D_DashStyle == null)
    //        {
    //            lock (_NewDictionaryLockObject)
    //            {
    //                var dic20200818 = new System.Collections.Generic.Dictionary<string, System.Drawing.Drawing2D.DashStyle>();
    //                dic20200818.Add("Solid", System.Drawing.Drawing2D.DashStyle.@Solid);
    //                dic20200818.Add("Dash", System.Drawing.Drawing2D.DashStyle.@Dash);
    //                dic20200818.Add("Dot", System.Drawing.Drawing2D.DashStyle.@Dot);
    //                dic20200818.Add("DashDot", System.Drawing.Drawing2D.DashStyle.@DashDot);
    //                dic20200818.Add("DashDotDot", System.Drawing.Drawing2D.DashStyle.@DashDotDot);
    //                dic20200818.Add("Custom", System.Drawing.Drawing2D.DashStyle.@Custom);
    //                __System_Drawing_Drawing2D_DashStyle = dic20200818;
    //            }
    //        }
    //        var result = default(System.Drawing.Drawing2D.DashStyle);
    //        if (__System_Drawing_Drawing2D_DashStyle.TryGetValue(s, out result))
    //        {
    //            return result;
    //        }
    //        else
    //        {
    //            return default(System.Drawing.Drawing2D.DashStyle);
    //        }
    //    }
    //    private static System.Collections.Generic.Dictionary<string, System.Drawing.Drawing2D.DashStyle> __System_Drawing_Drawing2D_DashStyle = null;

    //    internal protected global::DCSoft.Drawing.XPenStyle Read192_XPenStyle(bool isNullable, bool checkType)
    //    {
    //        var ThisReader = this.Reader;
    //        global::DCSoft.Drawing.XPenStyle o;
    //        o = new global::DCSoft.Drawing.XPenStyle();
    //        //bool[] paramsRead = new bool[9];
    //        //while (ThisReader.MoveToNextAttribute()){}
    //        ThisReader.MoveToElement();
    //        if (ThisReader.IsEmptyElement)
    //        {
    //            ThisReader.Skip();
    //            return o;
    //        }
    //        ThisReader.ReadStartElement();
    //        ThisReader.MoveToContent();
    //        int whileIterations288 = 0;
    //        int readerCount288 = ReaderCount;
    //        var _ReaderNodeType1 = ThisReader.NodeType;
    //        while (_ReaderNodeType1 != System.Xml.XmlNodeType.EndElement && _ReaderNodeType1 != System.Xml.XmlNodeType.None)
    //        {
    //            if (_ReaderNodeType1 == System.Xml.XmlNodeType.Element)
    //            {
    //                string _ReaderLocalName = ThisReader.LocalName;
    //                if (((object)_ReaderLocalName == (object)id168_Color))
    //                {
    //                    {
    //                        o.@ColorString = CacheString(ThisReader.ReadElementString());
    //                    }
    //                }
    //                else if (((object)_ReaderLocalName == (object)id217_Width))
    //                {
    //                    if (ThisReader.IsEmptyElement)
    //                    {
    //                        ThisReader.Skip();
    //                    }
    //                    else
    //                    {
    //                        o.@Width = ToSingle(ThisReader.ReadElementString());
    //                    }
    //                }
    //                else if (((object)_ReaderLocalName == (object)id802_DashStyle))
    //                {
    //                    if (ThisReader.IsEmptyElement)
    //                    {
    //                        ThisReader.Skip();
    //                    }
    //                    else
    //                    {
    //                        o.@DashStyle = Read43_DashStyle(ThisReader.ReadElementString());
    //                    }
    //                }
    //                else if (((object)_ReaderLocalName == (object)id803_DashCap))
    //                {
    //                    if (ThisReader.IsEmptyElement)
    //                    {
    //                        ThisReader.Skip();
    //                    }
    //                    else
    //                    {
    //                        o.@DashCap = Read188_DashCap(ThisReader.ReadElementString());
    //                    }
    //                }
    //                else if (((object)_ReaderLocalName == (object)id804_LineJoin))
    //                {
    //                    if (ThisReader.IsEmptyElement)
    //                    {
    //                        ThisReader.Skip();
    //                    }
    //                    else
    //                    {
    //                        o.@LineJoin = Read189_LineJoin(ThisReader.ReadElementString());
    //                    }
    //                }
    //                else if (((object)_ReaderLocalName == (object)id805_StartCap))
    //                {
    //                    if (ThisReader.IsEmptyElement)
    //                    {
    //                        ThisReader.Skip();
    //                    }
    //                    else
    //                    {
    //                        o.@StartCap = Read190_LineCap(ThisReader.ReadElementString());
    //                    }
    //                }
    //                else if (((object)_ReaderLocalName == (object)id806_EndCap))
    //                {
    //                    if (ThisReader.IsEmptyElement)
    //                    {
    //                        ThisReader.Skip();
    //                    }
    //                    else
    //                    {
    //                        o.@EndCap = Read190_LineCap(ThisReader.ReadElementString());
    //                    }
    //                }
    //                else if (((object)_ReaderLocalName == (object)id807_MiterLimit))
    //                {
    //                    if (ThisReader.IsEmptyElement)
    //                    {
    //                        ThisReader.Skip();
    //                    }
    //                    else
    //                    {
    //                        o.@MiterLimit = ToSingle(ThisReader.ReadElementString());
    //                    }
    //                }
    //                else if (((object)_ReaderLocalName == (object)id469_Alignment))
    //                {
    //                    if (ThisReader.IsEmptyElement)
    //                    {
    //                        ThisReader.Skip();
    //                    }
    //                    else
    //                    {
    //                        o.@Alignment = Read191_PenAlignment(ThisReader.ReadElementString());
    //                    }
    //                }
    //                else
    //                {
    //                    UnknownNode(o, _ReaderLocalName);//":Color, :Width, :DashStyle, :DashCap, :LineJoin, :StartCap, :EndCap, :MiterLimit, :Alignment");
    //                }
    //            }
    //            else
    //            {
    //                UnknownNode(o, ThisReader.LocalName);//":Color, :Width, :DashStyle, :DashCap, :LineJoin, :StartCap, :EndCap, :MiterLimit, :Alignment");
    //            }
    //            ThisReader.MoveToContent();
    //            CheckReaderCount(ref whileIterations288, ref readerCount288);
    //            _ReaderNodeType1 = ThisReader.NodeType;
    //        }
    //        ReadEndElement();
    //        return o;
    //    }
    //    internal protected global::System.Drawing.Color Read114_Color(bool checkType)
    //    {
    //        var ThisReader = this.Reader;
    //        var xsiType = checkType ? GetXsiType() : null;
    //        bool isNull = false;
    //        if (checkType)
    //        {
    //            var xsiTypeName = xsiType == null ? null : xsiType.Name;
    //            if (xsiTypeName == null || ((object)xsiTypeName == (object)id168_Color))
    //            {
    //            }
    //            else
    //                throw CreateUnknownTypeException(xsiType);
    //        }
    //        global::System.Drawing.Color o;
    //        try
    //        {
    //            o = (global::System.Drawing.Color)System.Activator.CreateInstance(typeof(global::System.Drawing.Color), System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.CreateInstance | System.Reflection.BindingFlags.NonPublic, null, new object[0], null);
    //        }
    //        catch (System.MissingMethodException)
    //        {
    //            throw CreateInaccessibleConstructorException(@"global::System.Drawing.Color");
    //        }
    //        catch (System.Security.SecurityException)
    //        {
    //            throw CreateCtorHasSecurityException(@"global::System.Drawing.Color");
    //        }
    //        //bool[] paramsRead = new bool[0];
    //        //while (ThisReader.MoveToNextAttribute()){}
    //        ThisReader.MoveToElement();
    //        if (ThisReader.IsEmptyElement)
    //        {
    //            ThisReader.Skip();
    //            return o;
    //        }
    //        ThisReader.ReadStartElement();
    //        ThisReader.MoveToContent();
    //        int whileIterations125 = 0;
    //        int readerCount125 = ReaderCount;
    //        var _ReaderNodeType1 = ThisReader.NodeType;
    //        while (_ReaderNodeType1 != System.Xml.XmlNodeType.EndElement && _ReaderNodeType1 != System.Xml.XmlNodeType.None)
    //        {
    //            if (_ReaderNodeType1 == System.Xml.XmlNodeType.Element)
    //            {
    //                UnknownNode(o, ThisReader.LocalName);
    //            }
    //            else
    //            {
    //                UnknownNode(o, ThisReader.LocalName);
    //            }
    //            ThisReader.MoveToContent();
    //            CheckReaderCount(ref whileIterations125, ref readerCount125);
    //            _ReaderNodeType1 = ThisReader.NodeType;
    //        }
    //        ReadEndElement();
    //        return o;
    //    }
    //    internal protected global::System.Drawing.GraphicsUnit Read60_GraphicsUnit(string s)
    //    {

    //        if (__System_Drawing_GraphicsUnit == null)
    //        {
    //            lock (_NewDictionaryLockObject)
    //            {
    //                var dic20200818 = new System.Collections.Generic.Dictionary<string, System.Drawing.GraphicsUnit>();
    //                dic20200818.Add("World", System.Drawing.GraphicsUnit.@World);
    //                dic20200818.Add("Display", System.Drawing.GraphicsUnit.@Display);
    //                dic20200818.Add("Pixel", System.Drawing.GraphicsUnit.@Pixel);
    //                dic20200818.Add("Point", System.Drawing.GraphicsUnit.@Point);
    //                dic20200818.Add("Inch", System.Drawing.GraphicsUnit.@Inch);
    //                dic20200818.Add("Document", System.Drawing.GraphicsUnit.@Document);
    //                dic20200818.Add("Millimeter", System.Drawing.GraphicsUnit.@Millimeter);
    //                __System_Drawing_GraphicsUnit = dic20200818;
    //            }
    //        }
    //        var result = default(System.Drawing.GraphicsUnit);
    //        if (__System_Drawing_GraphicsUnit.TryGetValue(s, out result))
    //        {
    //            return result;
    //        }
    //        else
    //        {
    //            return default(System.Drawing.GraphicsUnit);
    //        }
    //    }
    //    private static System.Collections.Generic.Dictionary<string, System.Drawing.GraphicsUnit> __System_Drawing_GraphicsUnit = null;

    //    public static bool ToBoolean(string v)
    //    {
    //        if (v == "true") return true;
    //        else if (v == "false") return false;
    //        if (v == null || v.Length == 0)
    //        {
    //            return false;
    //        }
    //        else
    //        {
    //            v = v.Trim().ToLower();
    //            if (v == "true" || v == "1") return true;
    //            else if (v == "false" || v == "0") return false;
    //            throw new FormatException("XML-Boolean:" + v);
    //        }
    //    }
    //    internal protected global::DCSoft.Drawing.XFontValue Read64_XFontValue(bool isNullable, bool checkType)
    //    {
    //        var ThisReader = this.Reader;
    //        global::DCSoft.Drawing.XFontValue o;
    //        o = new global::DCSoft.Drawing.XFontValue();
    //        //bool[] paramsRead = new bool[7];
    //        //while (ThisReader.MoveToNextAttribute()){}
    //        ThisReader.MoveToElement();
    //        if (ThisReader.IsEmptyElement)
    //        {
    //            ThisReader.Skip();
    //            return o;
    //        }
    //        ThisReader.ReadStartElement();
    //        ThisReader.MoveToContent();
    //        int whileIterations18 = 0;
    //        int readerCount18 = ReaderCount;
    //        var _ReaderNodeType1 = ThisReader.NodeType;
    //        while (_ReaderNodeType1 != System.Xml.XmlNodeType.EndElement && _ReaderNodeType1 != System.Xml.XmlNodeType.None)
    //        {
    //            if (_ReaderNodeType1 == System.Xml.XmlNodeType.Element)
    //            {
    //                string _ReaderLocalName = ThisReader.LocalName;
    //                if (((object)_ReaderLocalName == (object)id62_Name))
    //                {
    //                    {
    //                        o.@Name = CacheString(ThisReader.ReadElementString());
    //                    }
    //                }
    //                else if (((object)_ReaderLocalName == (object)id151_Size))
    //                {
    //                    if (ThisReader.IsEmptyElement)
    //                    {
    //                        ThisReader.Skip();
    //                    }
    //                    else
    //                    {
    //                        o.@Size = ToSingle(ThisReader.ReadElementString());
    //                    }
    //                }
    //                else if (((object)_ReaderLocalName == (object)id152_Unit))
    //                {
    //                    if (ThisReader.IsEmptyElement)
    //                    {
    //                        ThisReader.Skip();
    //                    }
    //                    else
    //                    {
    //                        o.@Unit = Read60_GraphicsUnit(ThisReader.ReadElementString());
    //                    }
    //                }
    //                else if (((object)_ReaderLocalName == (object)id153_Bold))
    //                {
    //                    if (ThisReader.IsEmptyElement)
    //                    {
    //                        ThisReader.Skip();
    //                    }
    //                    else
    //                    {
    //                        o.@Bold = ToBoolean(ThisReader.ReadElementString());
    //                    }
    //                }
    //                else if (((object)_ReaderLocalName == (object)id154_Italic))
    //                {
    //                    if (ThisReader.IsEmptyElement)
    //                    {
    //                        ThisReader.Skip();
    //                    }
    //                    else
    //                    {
    //                        o.@Italic = ToBoolean(ThisReader.ReadElementString());
    //                    }
    //                }
    //                else if (((object)_ReaderLocalName == (object)id155_Underline))
    //                {
    //                    if (ThisReader.IsEmptyElement)
    //                    {
    //                        ThisReader.Skip();
    //                    }
    //                    else
    //                    {
    //                        o.@Underline = ToBoolean(ThisReader.ReadElementString());
    //                    }
    //                }
    //                else if (((object)_ReaderLocalName == (object)id156_Strikeout))
    //                {
    //                    if (ThisReader.IsEmptyElement)
    //                    {
    //                        ThisReader.Skip();
    //                    }
    //                    else
    //                    {
    //                        o.@Strikeout = ToBoolean(ThisReader.ReadElementString());
    //                    }
    //                }
    //                else
    //                {
    //                    UnknownNode(o, _ReaderLocalName);//":Name, :Size, :Unit, :Bold, :Italic, :Underline, :Strikeout");
    //                }
    //            }
    //            else
    //            {
    //                UnknownNode(o, ThisReader.LocalName);//":Name, :Size, :Unit, :Bold, :Italic, :Underline, :Strikeout");
    //            }
    //            ThisReader.MoveToContent();
    //            CheckReaderCount(ref whileIterations18, ref readerCount18);
    //            _ReaderNodeType1 = ThisReader.NodeType;
    //        }
    //        ReadEndElement();
    //        return o;
    //    }
    //    private static readonly object _NewDictionaryLockObject = new object();

    //    internal protected global::DCSoft.Drawing.XImageValue Read34_XImageValue(bool isNullable, bool checkType)
    //    {
    //        var ThisReader = this.Reader;
    //        global::DCSoft.Drawing.XImageValue o;
    //        o = new global::DCSoft.Drawing.XImageValue();
    //        //bool[] paramsRead = new bool[3];
    //        //while (ThisReader.MoveToNextAttribute()){}
    //        ThisReader.MoveToElement();
    //        if (ThisReader.IsEmptyElement)
    //        {
    //            ThisReader.Skip();
    //            return o;
    //        }
    //        ThisReader.ReadStartElement();
    //        ThisReader.MoveToContent();
    //        int whileIterations15 = 0;
    //        int readerCount15 = ReaderCount;
    //        var _ReaderNodeType1 = ThisReader.NodeType;
    //        while (_ReaderNodeType1 != System.Xml.XmlNodeType.EndElement && _ReaderNodeType1 != System.Xml.XmlNodeType.None)
    //        {
    //            if (_ReaderNodeType1 == System.Xml.XmlNodeType.Element)
    //            {
    //                string _ReaderLocalName = ThisReader.LocalName;
    //                if (((object)_ReaderLocalName == (object)id135_HorizontalResolution))
    //                {
    //                    if (ThisReader.IsEmptyElement)
    //                    {
    //                        ThisReader.Skip();
    //                    }
    //                    else
    //                    {
    //                        o.@HorizontalResolution = ToSingle(ThisReader.ReadElementString());
    //                    }
    //                }
    //                else if (((object)_ReaderLocalName == (object)id136_VerticalResolution))
    //                {
    //                    if (ThisReader.IsEmptyElement)
    //                    {
    //                        ThisReader.Skip();
    //                    }
    //                    else
    //                    {
    //                        o.@VerticalResolution = ToSingle(ThisReader.ReadElementString());
    //                    }
    //                }
    //                else if (((object)_ReaderLocalName == (object)id137_ImageDataBase64String))
    //                {
    //                    {
    //                        ReadImageDataBase64String(ThisReader, o);
    //                    }
    //                }
    //                else
    //                {
    //                    UnknownNode(o, _ReaderLocalName);//":HorizontalResolution, :VerticalResolution, :ImageDataBase64String");
    //                }
    //            }
    //            else
    //            {
    //                UnknownNode(o, ThisReader.LocalName);//":HorizontalResolution, :VerticalResolution, :ImageDataBase64String");
    //            }
    //            ThisReader.MoveToContent();
    //            CheckReaderCount(ref whileIterations15, ref readerCount15);
    //            _ReaderNodeType1 = ThisReader.NodeType;
    //        }
    //        ReadEndElement();
    //        return o;
    //    }
    //    public static void ReadImageDataBase64String(System.Xml.XmlReader reader, DCSoft.Drawing.XImageValue img)
    //    {
    //        img.ImageDataBase64String = reader.ReadElementString();
    //    }
    //    public static float ToSingle(string v)
    //    {
    //        if (v == null || v.Length == 0)
    //        {
    //            return 0;
    //        }
    //        if (v == "NaN")
    //        {
    //            return 0;// float.NaN;
    //        }
    //        float dv = 0;
    //        if (float.TryParse(v, out dv))
    //        {
    //            return dv;
    //        }
    //        return 0;
    //    }
    //    public object Read_TemperatureDocument()
    //    {
    //        var ThisReader = this.Reader;
    //        object o = null;
    //        ThisReader.MoveToContent();
    //        if (ThisReader.NodeType == System.Xml.XmlNodeType.Element)
    //        {
    //            string _ReaderLocalName = ThisReader.LocalName;
    //            if (((object)_ReaderLocalName == (object)id1_TemperatureDocument))
    //            {
    //                o = Read243_TemperatureDocument(true, true);
    //            }
    //            else
    //            {
    //                throw CreateUnknownNodeException();
    //            }
    //        }
    //        else
    //        {
    //            UnknownNode(null, ThisReader.LocalName); //":XTextDocument");
    //        }
    //        this._StringValues.Clear();
    //        return o;
    //    }
    //    internal protected global::DCSoft.TemperatureChart.TemperatureDocument Read243_TemperatureDocument(bool isNullable, bool checkType)
    //    {
    //        var ThisReader = this.Reader;
    //        global::DCSoft.TemperatureChart.TemperatureDocument o;
    //        o = new global::DCSoft.TemperatureChart.TemperatureDocument();
    //        //            if ((object)(o.@Parameters) == null) o.@Parameters = new global::DCSoft.TemperatureChart.DCTimeLineParameterList();
    //        //            global::DCSoft.TemperatureChart.DCTimeLineParameterList a_1 = (global::DCSoft.TemperatureChart.DCTimeLineParameterList)o.@Parameters;
    //        //            if ((object)(o.@Datas) == null) o.@Datas = new global::DCSoft.TemperatureChart.DocumentDataList();
    //        //            global::DCSoft.TemperatureChart.DocumentDataList a_2 = (global::DCSoft.TemperatureChart.DocumentDataList)o.@Datas;
    //        //bool[] paramsRead = new bool[3];
    //        //while (ThisReader.MoveToNextAttribute()){}
    //        ThisReader.MoveToElement();
    //        if (ThisReader.IsEmptyElement)
    //        {
    //            ThisReader.Skip();
    //            return o;
    //        }
    //        ThisReader.ReadStartElement();
    //        ThisReader.MoveToContent();
    //        int whileIterations306 = 0;
    //        int readerCount306 = ReaderCount;
    //        var _ReaderNodeType1 = ThisReader.NodeType;
    //        while (_ReaderNodeType1 != System.Xml.XmlNodeType.EndElement && _ReaderNodeType1 != System.Xml.XmlNodeType.None)
    //        {
    //            if (_ReaderNodeType1 == System.Xml.XmlNodeType.Element)
    //            {
    //                string _ReaderLocalName = ThisReader.LocalName;
    //                if (((object)_ReaderLocalName == (object)id838_Config))
    //                {
    //                    o.@Config = Read237_TemperatureDocumentConfig(false, true);
    //                }
    //                else if (((object)_ReaderLocalName == (object)id64_Parameters))
    //                {
    //                    if (true)
    //                    {//if (!ReadNull()) {
    //                        if ((ThisReader.IsEmptyElement))
    //                        {
    //                            ThisReader.Skip();
    //                        }
    //                        else
    //                        {
    //                            if ((o.@Parameters) == null) o.@Parameters = new global::DCSoft.TemperatureChart.DCTimeLineParameterList();
    //                            global::DCSoft.TemperatureChart.DCTimeLineParameterList a_1_0 = o.@Parameters;
    //                            ThisReader.ReadStartElement();
    //                            ThisReader.MoveToContent();
    //                            int whileIterations307 = 0;
    //                            int readerCount307 = ReaderCount;
    //                            var _ReaderNodeType2 = ThisReader.NodeType;
    //                            while (_ReaderNodeType2 != System.Xml.XmlNodeType.EndElement && _ReaderNodeType2 != System.Xml.XmlNodeType.None)
    //                            {
    //                                if (_ReaderNodeType2 == System.Xml.XmlNodeType.Element)
    //                                {
    //                                    if (((object)ThisReader.LocalName == (object)id65_Parameter))
    //                                    {
    //                                        if ((a_1_0) == null) ThisReader.Skip(); else a_1_0.Add(Read238_DCTimeLineParameter(true, true));
    //                                    }
    //                                    else
    //                                    {
    //                                        UnknownNode(null, ThisReader.LocalName); //":Parameter");
    //                                    }
    //                                }
    //                                else
    //                                {
    //                                    UnknownNode(null, ThisReader.LocalName); //":Parameter");
    //                                }
    //                                ThisReader.MoveToContent();
    //                                CheckReaderCount(ref whileIterations307, ref readerCount307);
    //                                _ReaderNodeType2 = ThisReader.NodeType;
    //                            }
    //                            ReadEndElement();
    //                        }
    //                    }
    //                }
    //                else if (((object)_ReaderLocalName == (object)id839_Datas))
    //                {
    //                    if (true)
    //                    {//if (!ReadNull()) {
    //                        if ((ThisReader.IsEmptyElement))
    //                        {
    //                            ThisReader.Skip();
    //                        }
    //                        else
    //                        {
    //                            if ((o.@Datas) == null) o.@Datas = new global::DCSoft.TemperatureChart.DocumentDataList();
    //                            global::DCSoft.TemperatureChart.DocumentDataList a_2_0 = o.@Datas;
    //                            ThisReader.ReadStartElement();
    //                            ThisReader.MoveToContent();
    //                            int whileIterations308 = 0;
    //                            int readerCount308 = ReaderCount;
    //                            var _ReaderNodeType2 = ThisReader.NodeType;
    //                            while (_ReaderNodeType2 != System.Xml.XmlNodeType.EndElement && _ReaderNodeType2 != System.Xml.XmlNodeType.None)
    //                            {
    //                                if (_ReaderNodeType2 == System.Xml.XmlNodeType.Element)
    //                                {
    //                                    if (((object)ThisReader.LocalName == (object)id840_Data))
    //                                    {
    //                                        if ((a_2_0) == null) ThisReader.Skip(); else a_2_0.Add(Read242_DocumentData(true, true));
    //                                    }
    //                                    else
    //                                    {
    //                                        UnknownNode(null, ThisReader.LocalName); //":Data");
    //                                    }
    //                                }
    //                                else
    //                                {
    //                                    UnknownNode(null, ThisReader.LocalName); //":Data");
    //                                }
    //                                ThisReader.MoveToContent();
    //                                CheckReaderCount(ref whileIterations308, ref readerCount308);
    //                                _ReaderNodeType2 = ThisReader.NodeType;
    //                            }
    //                            ReadEndElement();
    //                        }
    //                    }
    //                }
    //                else
    //                {
    //                    UnknownNode(o, _ReaderLocalName);//":Config, :Parameters, :Datas");
    //                }
    //            }
    //            else
    //            {
    //                UnknownNode(o, ThisReader.LocalName);//":Config, :Parameters, :Datas");
    //            }
    //            ThisReader.MoveToContent();
    //            CheckReaderCount(ref whileIterations306, ref readerCount306);
    //            _ReaderNodeType1 = ThisReader.NodeType;
    //        }
    //        ReadEndElement();
    //        return o;
    //    }
    //    internal protected global::DCSoft.TemperatureChart.DocumentData Read242_DocumentData(bool isNullable, bool checkType)
    //    {
    //        var ThisReader = this.Reader;
    //        global::DCSoft.TemperatureChart.DocumentData o;
    //        o = new global::DCSoft.TemperatureChart.DocumentData();
    //        //            if ((object)(o.@Values) == null) o.@Values = new global::DCSoft.TemperatureChart.ValuePointList();
    //        //            global::DCSoft.TemperatureChart.ValuePointList a_1 = (global::DCSoft.TemperatureChart.ValuePointList)o.@Values;
    //        //bool[] paramsRead = new bool[2];
    //        while (ThisReader.MoveToNextAttribute())
    //        {
    //            string _ReaderLocalName = ThisReader.LocalName;
    //            if (((object)_ReaderLocalName == (object)id62_Name))
    //            {
    //                o.@Name = ThisReader.Value;
    //            }
    //        }
    //        ThisReader.MoveToElement();
    //        if (ThisReader.IsEmptyElement)
    //        {
    //            ThisReader.Skip();
    //            return o;
    //        }
    //        ThisReader.ReadStartElement();
    //        ThisReader.MoveToContent();
    //        int whileIterations309 = 0;
    //        int readerCount309 = ReaderCount;
    //        var _ReaderNodeType1 = ThisReader.NodeType;
    //        while (_ReaderNodeType1 != System.Xml.XmlNodeType.EndElement && _ReaderNodeType1 != System.Xml.XmlNodeType.None)
    //        {
    //            if (_ReaderNodeType1 == System.Xml.XmlNodeType.Element)
    //            {
    //                string _ReaderLocalName = ThisReader.LocalName;
    //                if (((object)_ReaderLocalName == (object)id842_Values))
    //                {
    //                    if (true)
    //                    {//if (!ReadNull()) {
    //                        if ((ThisReader.IsEmptyElement))
    //                        {
    //                            ThisReader.Skip();
    //                        }
    //                        else
    //                        {
    //                            if ((o.@Values) == null) o.@Values = new global::DCSoft.TemperatureChart.ValuePointList();
    //                            global::DCSoft.TemperatureChart.ValuePointList a_1_0 = o.@Values;
    //                            ThisReader.ReadStartElement();
    //                            ThisReader.MoveToContent();
    //                            int whileIterations310 = 0;
    //                            int readerCount310 = ReaderCount;
    //                            var _ReaderNodeType2 = ThisReader.NodeType;
    //                            while (_ReaderNodeType2 != System.Xml.XmlNodeType.EndElement && _ReaderNodeType2 != System.Xml.XmlNodeType.None)
    //                            {
    //                                if (_ReaderNodeType2 == System.Xml.XmlNodeType.Element)
    //                                {
    //                                    if (((object)ThisReader.LocalName == (object)id248_Value))
    //                                    {
    //                                        if ((a_1_0) == null) ThisReader.Skip(); else a_1_0.Add(Read241_ValuePoint(true, true));
    //                                    }
    //                                    else
    //                                    {
    //                                        UnknownNode(null, ThisReader.LocalName); //":Value");
    //                                    }
    //                                }
    //                                else
    //                                {
    //                                    UnknownNode(null, ThisReader.LocalName); //":Value");
    //                                }
    //                                ThisReader.MoveToContent();
    //                                CheckReaderCount(ref whileIterations310, ref readerCount310);
    //                                _ReaderNodeType2 = ThisReader.NodeType;
    //                            }
    //                            ReadEndElement();
    //                        }
    //                    }
    //                }
    //                else
    //                {
    //                    UnknownNode(o, _ReaderLocalName);//":Values");
    //                }
    //            }
    //            else
    //            {
    //                UnknownNode(o, ThisReader.LocalName);//":Values");
    //            }
    //            ThisReader.MoveToContent();
    //            CheckReaderCount(ref whileIterations309, ref readerCount309);
    //            _ReaderNodeType1 = ThisReader.NodeType;
    //        }
    //        ReadEndElement();
    //        return o;
    //    }
    //    internal protected global::DCSoft.TemperatureChart.ValuePoint Read241_ValuePoint(bool isNullable, bool checkType)
    //    {
    //        var ThisReader = this.Reader;
    //        global::DCSoft.TemperatureChart.ValuePoint o;
    //        o = new global::DCSoft.TemperatureChart.ValuePoint();
    //        //bool[] paramsRead = new bool[32];
    //        while (ThisReader.MoveToNextAttribute())
    //        {
    //            string _ReaderLocalName = ThisReader.LocalName;
    //            if (((object)_ReaderLocalName == (object)id844_VerifiedColorValue))
    //            {
    //                o.@VerifiedColorValue = ThisReader.Value;
    //            }
    //            else if (((object)_ReaderLocalName == (object)id845_VerifiedAlignment))
    //            {
    //                o.@VerifiedAlignment = Read119_StringAlignment(ThisReader.Value);
    //            }
    //            else if (((object)_ReaderLocalName == (object)id846_TagValue))
    //            {
    //                o.@TagValue = ThisReader.Value;
    //            }
    //            else if (((object)_ReaderLocalName == (object)id11_ID))
    //            {
    //                o.@ID = ThisReader.Value;
    //            }
    //            else if (((object)_ReaderLocalName == (object)id174_Superscript))
    //            {
    //                o.@Superscript = DCXMLConvert.ToBoolean(ThisReader.Value);
    //            }
    //            else if (((object)_ReaderLocalName == (object)id847_SpecifySymbolStyle))
    //            {
    //                o.@SpecifySymbolStyle = Read235_ValuePointSymbolStyle(ThisReader.Value);
    //            }
    //            else if (((object)_ReaderLocalName == (object)id848_SymbolOffsetX))
    //            {
    //                o.@SymbolOffsetX = ToSingle(ThisReader.Value);
    //            }
    //            else if (((object)_ReaderLocalName == (object)id849_SymbolOffsetY))
    //            {
    //                o.@SymbolOffsetY = ToSingle(ThisReader.Value);
    //            }
    //            else if (((object)_ReaderLocalName == (object)id850_SpecifyLanternSymbolStyle))
    //            {
    //                o.@SpecifyLanternSymbolStyle = Read235_ValuePointSymbolStyle(ThisReader.Value);
    //            }
    //            else if (((object)_ReaderLocalName == (object)id851_IntCharLantern))
    //            {
    //                o.@IntCharLantern = DCXMLConvert.ToInt32(ThisReader.Value);
    //            }
    //            else if (((object)_ReaderLocalName == (object)id852_IntCharSymbol))
    //            {
    //                o.@IntCharSymbol = DCXMLConvert.ToInt32(ThisReader.Value);
    //            }
    //            else if (((object)_ReaderLocalName == (object)id307_Link))
    //            {
    //                o.@Link = ThisReader.Value;
    //            }
    //            else if (((object)_ReaderLocalName == (object)id853_LinkTarget))
    //            {
    //                o.@LinkTarget = ThisReader.Value;
    //            }
    //            else if (((object)_ReaderLocalName == (object)id238_Title))
    //            {
    //                o.@Title = ThisReader.Value;
    //            }
    //            else if (((object)_ReaderLocalName == (object)id854_VerticalLine))
    //            {
    //                o.@VerticalLine = Read239_DCTimeLineBooleanValue(ThisReader.Value);
    //            }
    //            else if (((object)_ReaderLocalName == (object)id855_UseAdvVerticalStyle))
    //            {
    //                o.@UseAdvVerticalStyle = DCXMLConvert.ToBoolean(ThisReader.Value);
    //            }
    //            else if (((object)_ReaderLocalName == (object)id856_UseAdvVerticalStyle2))
    //            {
    //                o.@UseAdvVerticalStyle2 = DCXMLConvert.ToBoolean(ThisReader.Value);
    //            }
    //            else if (((object)_ReaderLocalName == (object)id857_Time))
    //            {
    //                o.@Time = ToDateTime(ThisReader.Value);
    //            }
    //            else if (((object)_ReaderLocalName == (object)id858_EndTime))
    //            {
    //                o.@EndTime = ToDateTime(ThisReader.Value);
    //            }
    //            else if (((object)_ReaderLocalName == (object)id248_Value))
    //            {
    //                o.@Value = ToSingle(ThisReader.Value);
    //            }
    //            else if (((object)_ReaderLocalName == (object)id859_LanternValue))
    //            {
    //                o.@LanternValue = ToSingle(ThisReader.Value);
    //            }
    //            else if (((object)_ReaderLocalName == (object)id146_Text))
    //            {
    //                o.@Text = ThisReader.Value;
    //            }
    //            else if (((object)_ReaderLocalName == (object)id860_TextAlign))
    //            {
    //                o.@TextAlign = Read119_StringAlignment(ThisReader.Value);
    //            }
    //            else if (((object)_ReaderLocalName == (object)id143_ColorValue))
    //            {
    //                o.@ColorValue = ThisReader.Value;
    //            }
    //            else if (((object)_ReaderLocalName == (object)id766_TextColorValue))
    //            {
    //                o.@TextColorValue = ThisReader.Value;
    //            }
    //        }
    //        ThisReader.MoveToElement();
    //        if (ThisReader.IsEmptyElement)
    //        {
    //            ThisReader.Skip();
    //            return o;
    //        }
    //        ThisReader.ReadStartElement();
    //        ThisReader.MoveToContent();
    //        int whileIterations311 = 0;
    //        int readerCount311 = ReaderCount;
    //        var _ReaderNodeType1 = ThisReader.NodeType;
    //        while (_ReaderNodeType1 != System.Xml.XmlNodeType.EndElement && _ReaderNodeType1 != System.Xml.XmlNodeType.None)
    //        {
    //            if (_ReaderNodeType1 == System.Xml.XmlNodeType.Element)
    //            {
    //                string _ReaderLocalName = ThisReader.LocalName;
    //                if (((object)_ReaderLocalName == (object)id861_Verified))
    //                {
    //                    if (ThisReader.IsEmptyElement)
    //                    {
    //                        ThisReader.Skip();
    //                    }
    //                    else
    //                    {
    //                        o.@Verified = DCXMLConvert.ToBoolean(ThisReader.ReadElementString());
    //                    }
    //                }
    //                else if (((object)_ReaderLocalName == (object)id862_VerifiedColor))
    //                {
    //                    o.@VerifiedColor = Read114_Color(true);
    //                }
    //                else if (((object)_ReaderLocalName == (object)id863_ValueTextTopPadding))
    //                {
    //                    if (ThisReader.IsEmptyElement)
    //                    {
    //                        ThisReader.Skip();
    //                    }
    //                    else
    //                    {
    //                        o.@ValueTextTopPadding = ToSingle(ThisReader.ReadElementString());
    //                    }
    //                }
    //                else if (((object)_ReaderLocalName == (object)id142_Font))
    //                {
    //                    o.@Font = Read64_XFontValue(false, true);
    //                }
    //                else if (((object)_ReaderLocalName == (object)id75_Image))
    //                {
    //                    o.@Image = Read34_XImageValue(false, true);
    //                }
    //                else if (((object)_ReaderLocalName == (object)id864_CustomImage))
    //                {
    //                    o.@CustomImage = Read34_XImageValue(false, true);
    //                }
    //                else if (((object)_ReaderLocalName == (object)id865_UpAndDown))
    //                {
    //                    if (ThisReader.IsEmptyElement)
    //                    {
    //                        ThisReader.Skip();
    //                    }
    //                    else
    //                    {
    //                        o.@UpAndDown = Read240_ValuePointUpAndDown(ThisReader.ReadElementString());
    //                    }
    //                }
    //                else
    //                {
    //                    UnknownNode(o, _ReaderLocalName);//":Verified, :VerifiedColor, :ValueTextTopPadding, :Font, :Image, :CustomImage, :UpAndDown");
    //                }
    //            }
    //            else
    //            {
    //                UnknownNode(o, ThisReader.LocalName);//":Verified, :VerifiedColor, :ValueTextTopPadding, :Font, :Image, :CustomImage, :UpAndDown");
    //            }
    //            ThisReader.MoveToContent();
    //            CheckReaderCount(ref whileIterations311, ref readerCount311);
    //            _ReaderNodeType1 = ThisReader.NodeType;
    //        }
    //        ReadEndElement();
    //        return o;
    //    }
    //    internal protected global::DCSoft.TemperatureChart.ValuePointUpAndDown Read240_ValuePointUpAndDown(string s)
    //    {
    //        switch (s)
    //        {
    //            case @"None": return global::DCSoft.TemperatureChart.ValuePointUpAndDown.@None;
    //            case @"Up": return global::DCSoft.TemperatureChart.ValuePointUpAndDown.@Up;
    //            case @"Down": return global::DCSoft.TemperatureChart.ValuePointUpAndDown.@Down;
    //            default: return (default(global::DCSoft.TemperatureChart.ValuePointUpAndDown));
    //        }
    //    }
    //    internal protected global::DCSoft.TemperatureChart.DCTimeLineBooleanValue Read239_DCTimeLineBooleanValue(string s)
    //    {
    //        switch (s)
    //        {
    //            case @"Inherit": return global::DCSoft.TemperatureChart.DCTimeLineBooleanValue.@Inherit;
    //            case @"True": return global::DCSoft.TemperatureChart.DCTimeLineBooleanValue.@True;
    //            case @"False": return global::DCSoft.TemperatureChart.DCTimeLineBooleanValue.@False;
    //            default: return (default(global::DCSoft.TemperatureChart.DCTimeLineBooleanValue));
    //        }
    //    }
    //    internal protected global::DCSoft.TemperatureChart.ValuePointSymbolStyle Read235_ValuePointSymbolStyle(string s)
    //    {

    //        if (__DCSoft_TemperatureChart_ValuePointSymbolStyle == null)
    //        {
    //            lock (_NewDictionaryLockObject)
    //            {
    //                var dic20200818 = new System.Collections.Generic.Dictionary<string, DCSoft.TemperatureChart.ValuePointSymbolStyle>();
    //                dic20200818.Add("None", DCSoft.TemperatureChart.ValuePointSymbolStyle.@None);
    //                dic20200818.Add("Default", DCSoft.TemperatureChart.ValuePointSymbolStyle.@Default);
    //                dic20200818.Add("SolidCicle", DCSoft.TemperatureChart.ValuePointSymbolStyle.@SolidCicle);
    //                dic20200818.Add("HollowCicle", DCSoft.TemperatureChart.ValuePointSymbolStyle.@HollowCicle);
    //                dic20200818.Add("OpaqueHollowCicle", DCSoft.TemperatureChart.ValuePointSymbolStyle.@OpaqueHollowCicle);
    //                dic20200818.Add("Cross", DCSoft.TemperatureChart.ValuePointSymbolStyle.@Cross);
    //                dic20200818.Add("Square", DCSoft.TemperatureChart.ValuePointSymbolStyle.@Square);
    //                dic20200818.Add("HollowSquare", DCSoft.TemperatureChart.ValuePointSymbolStyle.@HollowSquare);
    //                dic20200818.Add("Diamond", DCSoft.TemperatureChart.ValuePointSymbolStyle.@Diamond);
    //                dic20200818.Add("HollowDiamond", DCSoft.TemperatureChart.ValuePointSymbolStyle.@HollowDiamond);
    //                dic20200818.Add("V", DCSoft.TemperatureChart.ValuePointSymbolStyle.@V);
    //                dic20200818.Add("VReversed", DCSoft.TemperatureChart.ValuePointSymbolStyle.@VReversed);
    //                dic20200818.Add("SolidTriangle", DCSoft.TemperatureChart.ValuePointSymbolStyle.@SolidTriangle);
    //                dic20200818.Add("SolidTriangleReversed", DCSoft.TemperatureChart.ValuePointSymbolStyle.@SolidTriangleReversed);
    //                dic20200818.Add("HollowTriangle", DCSoft.TemperatureChart.ValuePointSymbolStyle.@HollowTriangle);
    //                dic20200818.Add("HollowTriangleReversed", DCSoft.TemperatureChart.ValuePointSymbolStyle.@HollowTriangleReversed);
    //                dic20200818.Add("Character", DCSoft.TemperatureChart.ValuePointSymbolStyle.@Character);
    //                dic20200818.Add("CharacterCircle", DCSoft.TemperatureChart.ValuePointSymbolStyle.@CharacterCircle);
    //                dic20200818.Add("Custom", DCSoft.TemperatureChart.ValuePointSymbolStyle.@Custom);
    //                __DCSoft_TemperatureChart_ValuePointSymbolStyle = dic20200818;
    //            }
    //        }
    //        var result = default(DCSoft.TemperatureChart.ValuePointSymbolStyle);
    //        if (__DCSoft_TemperatureChart_ValuePointSymbolStyle.TryGetValue(s, out result))
    //        {
    //            return result;
    //        }
    //        else
    //        {
    //            return default(DCSoft.TemperatureChart.ValuePointSymbolStyle);
    //        }
    //    }
    //    private static System.Collections.Generic.Dictionary<string, DCSoft.TemperatureChart.ValuePointSymbolStyle> __DCSoft_TemperatureChart_ValuePointSymbolStyle = null;

    //    internal protected global::DCSoft.TemperatureChart.DCTimeLineParameter Read238_DCTimeLineParameter(bool isNullable, bool checkType)
    //    {
    //        var ThisReader = this.Reader;
    //        global::DCSoft.TemperatureChart.DCTimeLineParameter o;
    //        o = new global::DCSoft.TemperatureChart.DCTimeLineParameter();
    //        //bool[] paramsRead = new bool[2];
    //        while (ThisReader.MoveToNextAttribute())
    //        {
    //            string _ReaderLocalName = ThisReader.LocalName;
    //            if (((object)_ReaderLocalName == (object)id62_Name))
    //            {
    //                o.@Name = ThisReader.Value;
    //            }
    //        }
    //        ThisReader.MoveToElement();
    //        if (ThisReader.IsEmptyElement)
    //        {
    //            ThisReader.Skip();
    //            return o;
    //        }
    //        ThisReader.ReadStartElement();
    //        ThisReader.MoveToContent();
    //        int whileIterations312 = 0;
    //        int readerCount312 = ReaderCount;
    //        var _ReaderNodeType1 = ThisReader.NodeType;
    //        while (_ReaderNodeType1 != System.Xml.XmlNodeType.EndElement && _ReaderNodeType1 != System.Xml.XmlNodeType.None)
    //        {
    //            string tmp = null;
    //            if (ThisReader.NodeType == System.Xml.XmlNodeType.Element)
    //            {
    //                UnknownNode(o, ThisReader.LocalName);
    //            }
    //            else if (ThisReader.NodeType == System.Xml.XmlNodeType.Text ||
    //            ThisReader.NodeType == System.Xml.XmlNodeType.CDATA ||
    //            ThisReader.NodeType == System.Xml.XmlNodeType.Whitespace ||
    //            ThisReader.NodeType == System.Xml.XmlNodeType.SignificantWhitespace)
    //            {
    //                tmp = ReadString(tmp, false);
    //                o.@Value = tmp;
    //            }
    //            else
    //            {
    //                UnknownNode(o, ThisReader.LocalName);
    //            }
    //            ThisReader.MoveToContent();
    //            CheckReaderCount(ref whileIterations312, ref readerCount312);
    //            _ReaderNodeType1 = ThisReader.NodeType;
    //        }
    //        ReadEndElement();
    //        return o;
    //    }
    //    internal protected global::DCSoft.TemperatureChart.TemperatureDocumentConfig Read237_TemperatureDocumentConfig(bool isNullable, bool checkType)
    //    {
    //        var ThisReader = this.Reader;
    //        global::DCSoft.TemperatureChart.TemperatureDocumentConfig o;
    //        o = new global::DCSoft.TemperatureChart.TemperatureDocumentConfig();
    //        //            if ((object)(o.@Images) == null) o.@Images = new global::DCSoft.TemperatureChart.DCTimeLineImageList();
    //        //            global::DCSoft.TemperatureChart.DCTimeLineImageList a_22 = (global::DCSoft.TemperatureChart.DCTimeLineImageList)o.@Images;
    //        //            if ((object)(o.@Labels) == null) o.@Labels = new global::DCSoft.TemperatureChart.DCTimeLineLabelList();
    //        //            global::DCSoft.TemperatureChart.DCTimeLineLabelList a_23 = (global::DCSoft.TemperatureChart.DCTimeLineLabelList)o.@Labels;
    //        //            if ((object)(o.@Zones) == null) o.@Zones = new global::DCSoft.TemperatureChart.TimeLineZoneInfoList();
    //        //            global::DCSoft.TemperatureChart.TimeLineZoneInfoList a_36 = (global::DCSoft.TemperatureChart.TimeLineZoneInfoList)o.@Zones;
    //        //            if ((object)(o.@Ticks) == null) o.@Ticks = new global::DCSoft.TemperatureChart.TickInfoList();
    //        //            global::DCSoft.TemperatureChart.TickInfoList a_37 = (global::DCSoft.TemperatureChart.TickInfoList)o.@Ticks;
    //        //            if ((object)(o.@HeaderLabels) == null) o.@HeaderLabels = new global::DCSoft.TemperatureChart.HeaderLabelInfoList();
    //        //            global::DCSoft.TemperatureChart.HeaderLabelInfoList a_58 = (global::DCSoft.TemperatureChart.HeaderLabelInfoList)o.@HeaderLabels;
    //        //            if ((object)(o.@HeaderLines) == null) o.@HeaderLines = new global::DCSoft.TemperatureChart.TitleLineInfoList();
    //        //            global::DCSoft.TemperatureChart.TitleLineInfoList a_60 = (global::DCSoft.TemperatureChart.TitleLineInfoList)o.@HeaderLines;
    //        //            if ((object)(o.@FooterLines) == null) o.@FooterLines = new global::DCSoft.TemperatureChart.TitleLineInfoList();
    //        //            global::DCSoft.TemperatureChart.TitleLineInfoList a_61 = (global::DCSoft.TemperatureChart.TitleLineInfoList)o.@FooterLines;
    //        //            if ((object)(o.@YAxisInfos) == null) o.@YAxisInfos = new global::DCSoft.TemperatureChart.YAxisInfoList();
    //        //            global::DCSoft.TemperatureChart.YAxisInfoList a_62 = (global::DCSoft.TemperatureChart.YAxisInfoList)o.@YAxisInfos;
    //        //bool[] paramsRead = new bool[63];
    //        while (ThisReader.MoveToNextAttribute())
    //        {
    //            string _ReaderLocalName = ThisReader.LocalName;
    //            if (((object)_ReaderLocalName == (object)id258_Version))
    //            {
    //                o.@Version = ThisReader.Value;
    //            }
    //        }
    //        ThisReader.MoveToElement();
    //        if (ThisReader.IsEmptyElement)
    //        {
    //            ThisReader.Skip();
    //            return o;
    //        }
    //        ThisReader.ReadStartElement();
    //        ThisReader.MoveToContent();
    //        int whileIterations313 = 0;
    //        int readerCount313 = ReaderCount;
    //        var _ReaderNodeType1 = ThisReader.NodeType;
    //        while (_ReaderNodeType1 != System.Xml.XmlNodeType.EndElement && _ReaderNodeType1 != System.Xml.XmlNodeType.None)
    //        {
    //            if (_ReaderNodeType1 == System.Xml.XmlNodeType.Element)
    //            {
    //                string _ReaderLocalName = ThisReader.LocalName;
    //                if (((object)_ReaderLocalName == (object)id868_BothBlackWhenPrint))
    //                {
    //                    if (ThisReader.IsEmptyElement)
    //                    {
    //                        ThisReader.Skip();
    //                    }
    //                    else
    //                    {
    //                        o.@BothBlackWhenPrint = DCXMLConvert.ToBoolean(ThisReader.ReadElementString());
    //                    }
    //                }
    //                else if (((object)_ReaderLocalName == (object)id869_LineWidthZoomRateWhenPrint))
    //                {
    //                    if (ThisReader.IsEmptyElement)
    //                    {
    //                        ThisReader.Skip();
    //                    }
    //                    else
    //                    {
    //                        o.@LineWidthZoomRateWhenPrint = ToSingle(ThisReader.ReadElementString());
    //                    }
    //                }
    //                else if (((object)_ReaderLocalName == (object)id870_LinkVisualStyle))
    //                {
    //                    if (ThisReader.IsEmptyElement)
    //                    {
    //                        ThisReader.Skip();
    //                    }
    //                    else
    //                    {
    //                        o.@LinkVisualStyle = Read212_DocumentLinkVisualStyle(ThisReader.ReadElementString());
    //                    }
    //                }
    //                else if (((object)_ReaderLocalName == (object)id871_DebugMode))
    //                {
    //                    if (ThisReader.IsEmptyElement)
    //                    {
    //                        ThisReader.Skip();
    //                    }
    //                    else
    //                    {
    //                        o.@DebugMode = DCXMLConvert.ToBoolean(ThisReader.ReadElementString());
    //                    }
    //                }
    //                else if (((object)_ReaderLocalName == (object)id872_EditValuePointMode))
    //                {
    //                    if (ThisReader.IsEmptyElement)
    //                    {
    //                        ThisReader.Skip();
    //                    }
    //                    else
    //                    {
    //                        o.@EditValuePointMode = Read213_EditValuePointEventHandleMode(ThisReader.ReadElementString());
    //                    }
    //                }
    //                else if (((object)_ReaderLocalName == (object)id873_EnableExtMouseMoveEvent))
    //                {
    //                    if (ThisReader.IsEmptyElement)
    //                    {
    //                        ThisReader.Skip();
    //                    }
    //                    else
    //                    {
    //                        o.@EnableExtMouseMoveEvent = DCXMLConvert.ToBoolean(ThisReader.ReadElementString());
    //                    }
    //                }
    //                else if (((object)_ReaderLocalName == (object)id874_EnableDataGridLinearAxisMode))
    //                {
    //                    if (ThisReader.IsEmptyElement)
    //                    {
    //                        ThisReader.Skip();
    //                    }
    //                    else
    //                    {
    //                        o.@EnableDataGridLinearAxisMode = DCXMLConvert.ToBoolean(ThisReader.ReadElementString());
    //                    }
    //                }
    //                else if (((object)_ReaderLocalName == (object)id251_Readonly))
    //                {
    //                    if (ThisReader.IsEmptyElement)
    //                    {
    //                        ThisReader.Skip();
    //                    }
    //                    else
    //                    {
    //                        o.@Readonly = DCXMLConvert.ToBoolean(ThisReader.ReadElementString());
    //                    }
    //                }
    //                else if (((object)_ReaderLocalName == (object)id875_ExtendDaysForTimeLine))
    //                {
    //                    if (ThisReader.IsEmptyElement)
    //                    {
    //                        ThisReader.Skip();
    //                    }
    //                    else
    //                    {
    //                        o.@ExtendDaysForTimeLine = ToSingle(ThisReader.ReadElementString());
    //                    }
    //                }
    //                else if (((object)_ReaderLocalName == (object)id876_IllegalTextEndCharForLinux))
    //                {
    //                    {
    //                        o.@IllegalTextEndCharForLinux = CacheString(ThisReader.ReadElementString());
    //                    }
    //                }
    //                else if (((object)_ReaderLocalName == (object)id877_TitleForToolTip))
    //                {
    //                    {
    //                        o.@TitleForToolTip = CacheString(ThisReader.ReadElementString());
    //                    }
    //                }
    //                else if (((object)_ReaderLocalName == (object)id878_EnableCustomValuePointSymbol))
    //                {
    //                    if (ThisReader.IsEmptyElement)
    //                    {
    //                        ThisReader.Skip();
    //                    }
    //                    else
    //                    {
    //                        o.@EnableCustomValuePointSymbol = DCXMLConvert.ToBoolean(ThisReader.ReadElementString());
    //                    }
    //                }
    //                else if (((object)_ReaderLocalName == (object)id879_HeaderLabelLineAlignment))
    //                {
    //                    if (ThisReader.IsEmptyElement)
    //                    {
    //                        ThisReader.Skip();
    //                    }
    //                    else
    //                    {
    //                        o.@HeaderLabelLineAlignment = Read119_StringAlignment(ThisReader.ReadElementString());
    //                    }
    //                }
    //                else if (((object)_ReaderLocalName == (object)id880_SelectionMode))
    //                {
    //                    if (ThisReader.IsEmptyElement)
    //                    {
    //                        ThisReader.Skip();
    //                    }
    //                    else
    //                    {
    //                        o.@SelectionMode = Read214_DCTimeLineSelectionMode(ThisReader.ReadElementString());
    //                    }
    //                }
    //                else if (((object)_ReaderLocalName == (object)id881_ShowTooltip))
    //                {
    //                    if (ThisReader.IsEmptyElement)
    //                    {
    //                        ThisReader.Skip();
    //                    }
    //                    else
    //                    {
    //                        o.@ShowTooltip = DCXMLConvert.ToBoolean(ThisReader.ReadElementString());
    //                    }
    //                }
    //                else if (((object)_ReaderLocalName == (object)id882_AllowUserCollapseZone))
    //                {
    //                    if (ThisReader.IsEmptyElement)
    //                    {
    //                        ThisReader.Skip();
    //                    }
    //                    else
    //                    {
    //                        o.@AllowUserCollapseZone = DCXMLConvert.ToBoolean(ThisReader.ReadElementString());
    //                    }
    //                }
    //                else if (((object)_ReaderLocalName == (object)id883_TickUnit))
    //                {
    //                    if (ThisReader.IsEmptyElement)
    //                    {
    //                        ThisReader.Skip();
    //                    }
    //                    else
    //                    {
    //                        o.@TickUnit = Read215_DCTimeUnit(ThisReader.ReadElementString());
    //                    }
    //                }
    //                else if (((object)_ReaderLocalName == (object)id884_DataGridTopPadding))
    //                {
    //                    if (ThisReader.IsEmptyElement)
    //                    {
    //                        ThisReader.Skip();
    //                    }
    //                    else
    //                    {
    //                        o.@DataGridTopPadding = ToSingle(ThisReader.ReadElementString());
    //                    }
    //                }
    //                else if (((object)_ReaderLocalName == (object)id885_DataGridBottomPadding))
    //                {
    //                    if (ThisReader.IsEmptyElement)
    //                    {
    //                        ThisReader.Skip();
    //                    }
    //                    else
    //                    {
    //                        o.@DataGridBottomPadding = ToSingle(ThisReader.ReadElementString());
    //                    }
    //                }
    //                else if (((object)_ReaderLocalName == (object)id886_SQLTextForHeaderLabel))
    //                {
    //                    {
    //                        o.@SQLTextForHeaderLabel = CacheString(ThisReader.ReadElementString());
    //                    }
    //                }
    //                else if (((object)_ReaderLocalName == (object)id887_SpecifyTickWidth))
    //                {
    //                    if (ThisReader.IsEmptyElement)
    //                    {
    //                        ThisReader.Skip();
    //                    }
    //                    else
    //                    {
    //                        o.@SpecifyTickWidth = ToSingle(ThisReader.ReadElementString());
    //                    }
    //                }
    //                else if (((object)_ReaderLocalName == (object)id888_Images))
    //                {
    //                    if (true)
    //                    {//if (!ReadNull()) {
    //                        if ((ThisReader.IsEmptyElement))
    //                        {
    //                            ThisReader.Skip();
    //                        }
    //                        else
    //                        {
    //                            if ((o.@Images) == null) o.@Images = new global::DCSoft.TemperatureChart.DCTimeLineImageList();
    //                            global::DCSoft.TemperatureChart.DCTimeLineImageList a_22_0 = o.@Images;
    //                            ThisReader.ReadStartElement();
    //                            ThisReader.MoveToContent();
    //                            int whileIterations314 = 0;
    //                            int readerCount314 = ReaderCount;
    //                            var _ReaderNodeType2 = ThisReader.NodeType;
    //                            while (_ReaderNodeType2 != System.Xml.XmlNodeType.EndElement && _ReaderNodeType2 != System.Xml.XmlNodeType.None)
    //                            {
    //                                if (_ReaderNodeType2 == System.Xml.XmlNodeType.Element)
    //                                {
    //                                    if (((object)ThisReader.LocalName == (object)id75_Image))
    //                                    {
    //                                        if ((a_22_0) == null) ThisReader.Skip(); else a_22_0.Add(Read216_DCTimeLineImage(true, true));
    //                                    }
    //                                    else
    //                                    {
    //                                        UnknownNode(null, ThisReader.LocalName); //":Image");
    //                                    }
    //                                }
    //                                else
    //                                {
    //                                    UnknownNode(null, ThisReader.LocalName); //":Image");
    //                                }
    //                                ThisReader.MoveToContent();
    //                                CheckReaderCount(ref whileIterations314, ref readerCount314);
    //                                _ReaderNodeType2 = ThisReader.NodeType;
    //                            }
    //                            ReadEndElement();
    //                        }
    //                    }
    //                }
    //                else if (((object)_ReaderLocalName == (object)id889_Labels))
    //                {
    //                    if (true)
    //                    {//if (!ReadNull()) {
    //                        if ((ThisReader.IsEmptyElement))
    //                        {
    //                            ThisReader.Skip();
    //                        }
    //                        else
    //                        {
    //                            if ((o.@Labels) == null) o.@Labels = new global::DCSoft.TemperatureChart.DCTimeLineLabelList();
    //                            global::DCSoft.TemperatureChart.DCTimeLineLabelList a_23_0 = o.@Labels;
    //                            ThisReader.ReadStartElement();
    //                            ThisReader.MoveToContent();
    //                            int whileIterations315 = 0;
    //                            int readerCount315 = ReaderCount;
    //                            var _ReaderNodeType2 = ThisReader.NodeType;
    //                            while (_ReaderNodeType2 != System.Xml.XmlNodeType.EndElement && _ReaderNodeType2 != System.Xml.XmlNodeType.None)
    //                            {
    //                                if (_ReaderNodeType2 == System.Xml.XmlNodeType.Element)
    //                                {
    //                                    if (((object)ThisReader.LocalName == (object)id890_Label))
    //                                    {
    //                                        if ((a_23_0) == null) ThisReader.Skip(); else a_23_0.Add(Read218_DCTimeLineLabel(true, true));
    //                                    }
    //                                    else
    //                                    {
    //                                        UnknownNode(null, ThisReader.LocalName); //":Label");
    //                                    }
    //                                }
    //                                else
    //                                {
    //                                    UnknownNode(null, ThisReader.LocalName); //":Label");
    //                                }
    //                                ThisReader.MoveToContent();
    //                                CheckReaderCount(ref whileIterations315, ref readerCount315);
    //                                _ReaderNodeType2 = ThisReader.NodeType;
    //                            }
    //                            ReadEndElement();
    //                        }
    //                    }
    //                }
    //                else if (((object)_ReaderLocalName == (object)id891_PageIndexText))
    //                {
    //                    {
    //                        o.@PageIndexText = CacheString(ThisReader.ReadElementString());
    //                    }
    //                }
    //                else if (((object)_ReaderLocalName == (object)id892_SpecifyStartDate))
    //                {
    //                    {
    //                        o.@SpecifyStartDate = CacheString(ThisReader.ReadElementString());
    //                    }
    //                }
    //                else if (((object)_ReaderLocalName == (object)id893_SpecifyEndDate))
    //                {
    //                    {
    //                        o.@SpecifyEndDate = CacheString(ThisReader.ReadElementString());
    //                    }
    //                }
    //                else if (((object)_ReaderLocalName == (object)id90_PageSettings))
    //                {
    //                    o.@PageSettings = Read219_DocumentPageSettings(false, true);
    //                }
    //                else if (((object)_ReaderLocalName == (object)id894_FooterDescription))
    //                {
    //                    {
    //                        o.@FooterDescription = CacheString(ThisReader.ReadElementString());
    //                    }
    //                }
    //                else if (((object)_ReaderLocalName == (object)id896_ShowIcon))
    //                {
    //                    if (ThisReader.IsEmptyElement)
    //                    {
    //                        ThisReader.Skip();
    //                    }
    //                    else
    //                    {
    //                        o.@ShowIcon = DCXMLConvert.ToBoolean(ThisReader.ReadElementString());
    //                    }
    //                }
    //                else if (((object)_ReaderLocalName == (object)id897_ImagePixelWidth))
    //                {
    //                    if (ThisReader.IsEmptyElement)
    //                    {
    //                        ThisReader.Skip();
    //                    }
    //                    else
    //                    {
    //                        o.@ImagePixelWidth = DCXMLConvert.ToInt32(ThisReader.ReadElementString());
    //                    }
    //                }
    //                else if (((object)_ReaderLocalName == (object)id898_ImagePixelHeight))
    //                {
    //                    if (ThisReader.IsEmptyElement)
    //                    {
    //                        ThisReader.Skip();
    //                    }
    //                    else
    //                    {
    //                        o.@ImagePixelHeight = DCXMLConvert.ToInt32(ThisReader.ReadElementString());
    //                    }
    //                }
    //                else if (((object)_ReaderLocalName == (object)id899_ShadowPointDetectSeconds))
    //                {
    //                    if (ThisReader.IsEmptyElement)
    //                    {
    //                        ThisReader.Skip();
    //                    }
    //                    else
    //                    {
    //                        o.@ShadowPointDetectSeconds = DCXMLConvert.ToInt32(ThisReader.ReadElementString());
    //                    }
    //                }
    //                else if (((object)_ReaderLocalName == (object)id772_GridYSplitNum))
    //                {
    //                    if (ThisReader.IsEmptyElement)
    //                    {
    //                        ThisReader.Skip();
    //                    }
    //                    else
    //                    {
    //                        o.@GridYSplitNum = DCXMLConvert.ToInt32(ThisReader.ReadElementString());
    //                    }
    //                }
    //                else if (((object)_ReaderLocalName == (object)id900_GridYSplitInfo))
    //                {
    //                    o.@GridYSplitInfo = Read221_GridYSplitInfo(false, true);
    //                }
    //                else if (((object)_ReaderLocalName == (object)id901_Zones))
    //                {
    //                    if (true)
    //                    {//if (!ReadNull()) {
    //                        if ((ThisReader.IsEmptyElement))
    //                        {
    //                            ThisReader.Skip();
    //                        }
    //                        else
    //                        {
    //                            if ((o.@Zones) == null) o.@Zones = new global::DCSoft.TemperatureChart.TimeLineZoneInfoList();
    //                            global::DCSoft.TemperatureChart.TimeLineZoneInfoList a_36_0 = o.@Zones;
    //                            ThisReader.ReadStartElement();
    //                            ThisReader.MoveToContent();
    //                            int whileIterations316 = 0;
    //                            int readerCount316 = ReaderCount;
    //                            var _ReaderNodeType2 = ThisReader.NodeType;
    //                            while (_ReaderNodeType2 != System.Xml.XmlNodeType.EndElement && _ReaderNodeType2 != System.Xml.XmlNodeType.None)
    //                            {
    //                                if (_ReaderNodeType2 == System.Xml.XmlNodeType.Element)
    //                                {
    //                                    if (((object)ThisReader.LocalName == (object)id902_Zone))
    //                                    {
    //                                        if ((a_36_0) == null) ThisReader.Skip(); else a_36_0.Add(Read223_TimeLineZoneInfo(true, true));
    //                                    }
    //                                    else
    //                                    {
    //                                        UnknownNode(null, ThisReader.LocalName); //":Zone");
    //                                    }
    //                                }
    //                                else
    //                                {
    //                                    UnknownNode(null, ThisReader.LocalName); //":Zone");
    //                                }
    //                                ThisReader.MoveToContent();
    //                                CheckReaderCount(ref whileIterations316, ref readerCount316);
    //                                _ReaderNodeType2 = ThisReader.NodeType;
    //                            }
    //                            ReadEndElement();
    //                        }
    //                    }
    //                }
    //                else if (((object)_ReaderLocalName == (object)id903_Ticks))
    //                {
    //                    if (true)
    //                    {//if (!ReadNull()) {
    //                        if ((ThisReader.IsEmptyElement))
    //                        {
    //                            ThisReader.Skip();
    //                        }
    //                        else
    //                        {
    //                            if ((o.@Ticks) == null) o.@Ticks = new global::DCSoft.TemperatureChart.TickInfoList();
    //                            global::DCSoft.TemperatureChart.TickInfoList a_37_0 = o.@Ticks;
    //                            ThisReader.ReadStartElement();
    //                            ThisReader.MoveToContent();
    //                            int whileIterations317 = 0;
    //                            int readerCount317 = ReaderCount;
    //                            var _ReaderNodeType2 = ThisReader.NodeType;
    //                            while (_ReaderNodeType2 != System.Xml.XmlNodeType.EndElement && _ReaderNodeType2 != System.Xml.XmlNodeType.None)
    //                            {
    //                                if (_ReaderNodeType2 == System.Xml.XmlNodeType.Element)
    //                                {
    //                                    if (((object)ThisReader.LocalName == (object)id904_Tick))
    //                                    {
    //                                        if ((a_37_0) == null) ThisReader.Skip(); else a_37_0.Add(Read222_TickInfo(true, true));
    //                                    }
    //                                    else
    //                                    {
    //                                        UnknownNode(null, ThisReader.LocalName); //":Tick");
    //                                    }
    //                                }
    //                                else
    //                                {
    //                                    UnknownNode(null, ThisReader.LocalName); //":Tick");
    //                                }
    //                                ThisReader.MoveToContent();
    //                                CheckReaderCount(ref whileIterations317, ref readerCount317);
    //                                _ReaderNodeType2 = ThisReader.NodeType;
    //                            }
    //                            ReadEndElement();
    //                        }
    //                    }
    //                }
    //                else if (((object)_ReaderLocalName == (object)id811_SymbolSize))
    //                {
    //                    if (ThisReader.IsEmptyElement)
    //                    {
    //                        ThisReader.Skip();
    //                    }
    //                    else
    //                    {
    //                        o.@SymbolSize = ToSingle(ThisReader.ReadElementString());
    //                    }
    //                }
    //                else if (((object)_ReaderLocalName == (object)id169_FontName))
    //                {
    //                    {
    //                        o.@FontName = CacheString(ThisReader.ReadElementString());
    //                    }
    //                }
    //                else if (((object)_ReaderLocalName == (object)id170_FontSize))
    //                {
    //                    if (ThisReader.IsEmptyElement)
    //                    {
    //                        ThisReader.Skip();
    //                    }
    //                    else
    //                    {
    //                        o.@FontSize = ToSingle(ThisReader.ReadElementString());
    //                    }
    //                }
    //                else if (((object)_ReaderLocalName == (object)id905_BigTitleFontSize))
    //                {
    //                    if (ThisReader.IsEmptyElement)
    //                    {
    //                        ThisReader.Skip();
    //                    }
    //                    else
    //                    {
    //                        o.@BigTitleFontSize = ToSingle(ThisReader.ReadElementString());
    //                    }
    //                }
    //                else if (((object)_ReaderLocalName == (object)id906_PageIndexFont))
    //                {
    //                    o.@PageIndexFont = Read64_XFontValue(false, true);
    //                }
    //                else if (((object)_ReaderLocalName == (object)id907_ForeColorValue))
    //                {
    //                    {
    //                        o.@ForeColorValue = CacheString(ThisReader.ReadElementString());
    //                    }
    //                }
    //                else if (((object)_ReaderLocalName == (object)id908_BigVerticalGridLineWidth))
    //                {
    //                    if (ThisReader.IsEmptyElement)
    //                    {
    //                        ThisReader.Skip();
    //                    }
    //                    else
    //                    {
    //                        o.@BigVerticalGridLineWidth = ToSingle(ThisReader.ReadElementString());
    //                    }
    //                }
    //                else if (((object)_ReaderLocalName == (object)id909_BigVerticalGridLineColorValue))
    //                {
    //                    {
    //                        o.@BigVerticalGridLineColorValue = CacheString(ThisReader.ReadElementString());
    //                    }
    //                }
    //                else if (((object)_ReaderLocalName == (object)id144_BackColorValue))
    //                {
    //                    {
    //                        o.@BackColorValue = CacheString(ThisReader.ReadElementString());
    //                    }
    //                }
    //                else if (((object)_ReaderLocalName == (object)id910_PageBackColorValue))
    //                {
    //                    {
    //                        o.@PageBackColorValue = CacheString(ThisReader.ReadElementString());
    //                    }
    //                }
    //                else if (((object)_ReaderLocalName == (object)id911_GridLineColorValue))
    //                {
    //                    {
    //                        o.@GridLineColorValue = CacheString(ThisReader.ReadElementString());
    //                    }
    //                }
    //                else if (((object)_ReaderLocalName == (object)id912_GridBackColorValue))
    //                {
    //                    {
    //                        o.@GridBackColorValue = CacheString(ThisReader.ReadElementString());
    //                    }
    //                }
    //                else if (((object)_ReaderLocalName == (object)id913_DateFormatString))
    //                {
    //                    {
    //                        o.@DateFormatString = CacheString(ThisReader.ReadElementString());
    //                    }
    //                }
    //                else if (((object)_ReaderLocalName == (object)id914_DateFormatStringForCrossYear))
    //                {
    //                    {
    //                        o.@DateFormatStringForCrossYear = CacheString(ThisReader.ReadElementString());
    //                    }
    //                }
    //                else if (((object)_ReaderLocalName == (object)id915_DateFormatStringForCrossMonth))
    //                {
    //                    {
    //                        o.@DateFormatStringForCrossMonth = CacheString(ThisReader.ReadElementString());
    //                    }
    //                }
    //                else if (((object)_ReaderLocalName == (object)id916_DateFormatStringForCrossWeek))
    //                {
    //                    {
    //                        o.@DateFormatStringForCrossWeek = CacheString(ThisReader.ReadElementString());
    //                    }
    //                }
    //                else if (((object)_ReaderLocalName == (object)id917_Item))
    //                {
    //                    {
    //                        o.@DateFormatStringForFirstIndexFirstPage = CacheString(ThisReader.ReadElementString());
    //                    }
    //                }
    //                else if (((object)_ReaderLocalName == (object)id918_Item))
    //                {
    //                    {
    //                        o.@DateFormatStringForFirstIndexOtherPage = CacheString(ThisReader.ReadElementString());
    //                    }
    //                }
    //                else if (((object)_ReaderLocalName == (object)id238_Title))
    //                {
    //                    {
    //                        o.@Title = CacheString(ThisReader.ReadElementString());
    //                    }
    //                }
    //                else if (((object)_ReaderLocalName == (object)id919_SpecifyTitleHeight))
    //                {
    //                    if (ThisReader.IsEmptyElement)
    //                    {
    //                        ThisReader.Skip();
    //                    }
    //                    else
    //                    {
    //                        o.@SpecifyTitleHeight = ToSingle(ThisReader.ReadElementString());
    //                    }
    //                }
    //                else if (((object)_ReaderLocalName == (object)id920_HeaderLabels))
    //                {
    //                    if (true)
    //                    {//if (!ReadNull()) {
    //                        if ((ThisReader.IsEmptyElement))
    //                        {
    //                            ThisReader.Skip();
    //                        }
    //                        else
    //                        {
    //                            if ((o.@HeaderLabels) == null) o.@HeaderLabels = new global::DCSoft.TemperatureChart.HeaderLabelInfoList();
    //                            global::DCSoft.TemperatureChart.HeaderLabelInfoList a_58_0 = o.@HeaderLabels;
    //                            ThisReader.ReadStartElement();
    //                            ThisReader.MoveToContent();
    //                            int whileIterations318 = 0;
    //                            int readerCount318 = ReaderCount;
    //                            var _ReaderNodeType2 = ThisReader.NodeType;
    //                            while (_ReaderNodeType2 != System.Xml.XmlNodeType.EndElement && _ReaderNodeType2 != System.Xml.XmlNodeType.None)
    //                            {
    //                                if (_ReaderNodeType2 == System.Xml.XmlNodeType.Element)
    //                                {
    //                                    if (((object)ThisReader.LocalName == (object)id890_Label))
    //                                    {
    //                                        if ((a_58_0) == null) ThisReader.Skip(); else a_58_0.Add(Read224_HeaderLabelInfo(true, true));
    //                                    }
    //                                    else
    //                                    {
    //                                        UnknownNode(null, ThisReader.LocalName); //":Label");
    //                                    }
    //                                }
    //                                else
    //                                {
    //                                    UnknownNode(null, ThisReader.LocalName); //":Label");
    //                                }
    //                                ThisReader.MoveToContent();
    //                                CheckReaderCount(ref whileIterations318, ref readerCount318);
    //                                _ReaderNodeType2 = ThisReader.NodeType;
    //                            }
    //                            ReadEndElement();
    //                        }
    //                    }
    //                }
    //                else if (((object)_ReaderLocalName == (object)id921_NumOfDaysInOnePage))
    //                {
    //                    if (ThisReader.IsEmptyElement)
    //                    {
    //                        ThisReader.Skip();
    //                    }
    //                    else
    //                    {
    //                        o.@NumOfDaysInOnePage = DCXMLConvert.ToInt32(ThisReader.ReadElementString());
    //                    }
    //                }
    //                else if (((object)_ReaderLocalName == (object)id922_HeaderLines))
    //                {
    //                    if (true)
    //                    {//if (!ReadNull()) {
    //                        if ((ThisReader.IsEmptyElement))
    //                        {
    //                            ThisReader.Skip();
    //                        }
    //                        else
    //                        {
    //                            if ((o.@HeaderLines) == null) o.@HeaderLines = new global::DCSoft.TemperatureChart.TitleLineInfoList();
    //                            global::DCSoft.TemperatureChart.TitleLineInfoList a_60_0 = o.@HeaderLines;
    //                            ThisReader.ReadStartElement();
    //                            ThisReader.MoveToContent();
    //                            int whileIterations319 = 0;
    //                            int readerCount319 = ReaderCount;
    //                            var _ReaderNodeType2 = ThisReader.NodeType;
    //                            while (_ReaderNodeType2 != System.Xml.XmlNodeType.EndElement && _ReaderNodeType2 != System.Xml.XmlNodeType.None)
    //                            {
    //                                if (_ReaderNodeType2 == System.Xml.XmlNodeType.Element)
    //                                {
    //                                    if (((object)ThisReader.LocalName == (object)id923_Line))
    //                                    {
    //                                        if ((a_60_0) == null) ThisReader.Skip(); else a_60_0.Add(Read232_TitleLineInfo(true, true));
    //                                    }
    //                                    else
    //                                    {
    //                                        UnknownNode(null, ThisReader.LocalName); //":Line");
    //                                    }
    //                                }
    //                                else
    //                                {
    //                                    UnknownNode(null, ThisReader.LocalName); //":Line");
    //                                }
    //                                ThisReader.MoveToContent();
    //                                CheckReaderCount(ref whileIterations319, ref readerCount319);
    //                                _ReaderNodeType2 = ThisReader.NodeType;
    //                            }
    //                            ReadEndElement();
    //                        }
    //                    }
    //                }
    //                else if (((object)_ReaderLocalName == (object)id924_FooterLines))
    //                {
    //                    if (true)
    //                    {//if (!ReadNull()) {
    //                        if ((ThisReader.IsEmptyElement))
    //                        {
    //                            ThisReader.Skip();
    //                        }
    //                        else
    //                        {
    //                            if ((o.@FooterLines) == null) o.@FooterLines = new global::DCSoft.TemperatureChart.TitleLineInfoList();
    //                            global::DCSoft.TemperatureChart.TitleLineInfoList a_61_0 = o.@FooterLines;
    //                            ThisReader.ReadStartElement();
    //                            ThisReader.MoveToContent();
    //                            int whileIterations320 = 0;
    //                            int readerCount320 = ReaderCount;
    //                            var _ReaderNodeType2 = ThisReader.NodeType;
    //                            while (_ReaderNodeType2 != System.Xml.XmlNodeType.EndElement && _ReaderNodeType2 != System.Xml.XmlNodeType.None)
    //                            {
    //                                if (_ReaderNodeType2 == System.Xml.XmlNodeType.Element)
    //                                {
    //                                    if (((object)ThisReader.LocalName == (object)id923_Line))
    //                                    {
    //                                        if ((a_61_0) == null) ThisReader.Skip(); else a_61_0.Add(Read232_TitleLineInfo(true, true));
    //                                    }
    //                                    else
    //                                    {
    //                                        UnknownNode(null, ThisReader.LocalName); //":Line");
    //                                    }
    //                                }
    //                                else
    //                                {
    //                                    UnknownNode(null, ThisReader.LocalName); //":Line");
    //                                }
    //                                ThisReader.MoveToContent();
    //                                CheckReaderCount(ref whileIterations320, ref readerCount320);
    //                                _ReaderNodeType2 = ThisReader.NodeType;
    //                            }
    //                            ReadEndElement();
    //                        }
    //                    }
    //                }
    //                else if (((object)_ReaderLocalName == (object)id925_YAxisInfos))
    //                {
    //                    if (true)
    //                    {//if (!ReadNull()) {
    //                        if ((ThisReader.IsEmptyElement))
    //                        {
    //                            ThisReader.Skip();
    //                        }
    //                        else
    //                        {
    //                            if ((o.@YAxisInfos) == null) o.@YAxisInfos = new global::DCSoft.TemperatureChart.YAxisInfoList();
    //                            global::DCSoft.TemperatureChart.YAxisInfoList a_62_0 = o.@YAxisInfos;
    //                            ThisReader.ReadStartElement();
    //                            ThisReader.MoveToContent();
    //                            int whileIterations321 = 0;
    //                            int readerCount321 = ReaderCount;
    //                            var _ReaderNodeType2 = ThisReader.NodeType;
    //                            while (_ReaderNodeType2 != System.Xml.XmlNodeType.EndElement && _ReaderNodeType2 != System.Xml.XmlNodeType.None)
    //                            {
    //                                if (_ReaderNodeType2 == System.Xml.XmlNodeType.Element)
    //                                {
    //                                    if (((object)ThisReader.LocalName == (object)id926_YAxis))
    //                                    {
    //                                        if ((a_62_0) == null) ThisReader.Skip(); else a_62_0.Add(Read236_YAxisInfo(true, true));
    //                                    }
    //                                    else
    //                                    {
    //                                        UnknownNode(null, ThisReader.LocalName); //":YAxis");
    //                                    }
    //                                }
    //                                else
    //                                {
    //                                    UnknownNode(null, ThisReader.LocalName); //":YAxis");
    //                                }
    //                                ThisReader.MoveToContent();
    //                                CheckReaderCount(ref whileIterations321, ref readerCount321);
    //                                _ReaderNodeType2 = ThisReader.NodeType;
    //                            }
    //                            ReadEndElement();
    //                        }
    //                    }
    //                }
    //                else
    //                {
    //                    UnknownNode(o, _ReaderLocalName);//":BothBlackWhenPrint, :LineWidthZoomRateWhenPrint, :LinkVisualStyle, :DebugMode, :EditValuePointMode, :EnableExtMouseMoveEvent, :EnableDataGridLinearAxisMode, :Readonly, :ExtendDaysForTimeLine, :IllegalTextEndCharForLinux, :TitleForToolTip, :EnableCustomValuePointSymbol, :HeaderLabelLineAlignment, :SelectionMode, :ShowTooltip, :AllowUserCollapseZone, :TickUnit, :DataGridTopPadding, :DataGridBottomPadding, :SQLTextForHeaderLabel, :SpecifyTickWidth, :Images, :Labels, :PageIndexText, :SpecifyStartDate, :SpecifyEndDate, :PageSettings, :FooterDescription, :PageTitlePosition, :ShowIcon, :ImagePixelWidth, :ImagePixelHeight, :ShadowPointDetectSeconds, :GridYSplitNum, :GridYSplitInfo, :Zones, :Ticks, :SymbolSize, :FontName, :FontSize, :BigTitleFontSize, :PageIndexFont, :ForeColorValue, :BigVerticalGridLineWidth, :BigVerticalGridLineColorValue, :BackColorValue, :PageBackColorValue, :GridLineColorValue, :GridBackColorValue, :DateFormatString, :DateFormatStringForCrossYear, :DateFormatStringForCrossMonth, :DateFormatStringForCrossWeek, :DateFormatStringForFirstIndexFirstPage, :DateFormatStringForFirstIndexOtherPage, :Title, :SpecifyTitleHeight, :HeaderLabels, :NumOfDaysInOnePage, :HeaderLines, :FooterLines, :YAxisInfos");
    //                }
    //            }
    //            else
    //            {
    //                UnknownNode(o, ThisReader.LocalName);//":BothBlackWhenPrint, :LineWidthZoomRateWhenPrint, :LinkVisualStyle, :DebugMode, :EditValuePointMode, :EnableExtMouseMoveEvent, :EnableDataGridLinearAxisMode, :Readonly, :ExtendDaysForTimeLine, :IllegalTextEndCharForLinux, :TitleForToolTip, :EnableCustomValuePointSymbol, :HeaderLabelLineAlignment, :SelectionMode, :ShowTooltip, :AllowUserCollapseZone, :TickUnit, :DataGridTopPadding, :DataGridBottomPadding, :SQLTextForHeaderLabel, :SpecifyTickWidth, :Images, :Labels, :PageIndexText, :SpecifyStartDate, :SpecifyEndDate, :PageSettings, :FooterDescription, :PageTitlePosition, :ShowIcon, :ImagePixelWidth, :ImagePixelHeight, :ShadowPointDetectSeconds, :GridYSplitNum, :GridYSplitInfo, :Zones, :Ticks, :SymbolSize, :FontName, :FontSize, :BigTitleFontSize, :PageIndexFont, :ForeColorValue, :BigVerticalGridLineWidth, :BigVerticalGridLineColorValue, :BackColorValue, :PageBackColorValue, :GridLineColorValue, :GridBackColorValue, :DateFormatString, :DateFormatStringForCrossYear, :DateFormatStringForCrossMonth, :DateFormatStringForCrossWeek, :DateFormatStringForFirstIndexFirstPage, :DateFormatStringForFirstIndexOtherPage, :Title, :SpecifyTitleHeight, :HeaderLabels, :NumOfDaysInOnePage, :HeaderLines, :FooterLines, :YAxisInfos");
    //            }
    //            ThisReader.MoveToContent();
    //            CheckReaderCount(ref whileIterations313, ref readerCount313);
    //            _ReaderNodeType1 = ThisReader.NodeType;
    //        }
    //        ReadEndElement();
    //        return o;
    //    }
    //    internal protected global::DCSoft.TemperatureChart.YAxisInfo Read236_YAxisInfo(bool isNullable, bool checkType)
    //    {
    //        var ThisReader = this.Reader;
    //        global::DCSoft.TemperatureChart.YAxisInfo o;
    //        o = new global::DCSoft.TemperatureChart.YAxisInfo();
    //        //            if ((object)(o.@Scales) == null) o.@Scales = new global::DCSoft.TemperatureChart.YAxisScaleInfoList();
    //        //            global::DCSoft.TemperatureChart.YAxisScaleInfoList a_63 = (global::DCSoft.TemperatureChart.YAxisScaleInfoList)o.@Scales;
    //        //bool[] paramsRead = new bool[64];
    //        while (ThisReader.MoveToNextAttribute())
    //        {
    //            string _ReaderLocalName = ThisReader.LocalName;
    //            if (((object)_ReaderLocalName == (object)id928_MergeIntoLeft))
    //            {
    //                o.@MergeIntoLeft = DCXMLConvert.ToBoolean(ThisReader.Value);
    //            }
    //            else if (((object)_ReaderLocalName == (object)id62_Name))
    //            {
    //                o.@Name = ThisReader.Value;
    //            }
    //            else if (((object)_ReaderLocalName == (object)id929_HighlightOutofNormalRange))
    //            {
    //                o.@HighlightOutofNormalRange = DCXMLConvert.ToBoolean(ThisReader.Value);
    //            }
    //            else if (((object)_ReaderLocalName == (object)id930_InputTimePrecision))
    //            {
    //                o.@InputTimePrecision = Read225_DateTimePrecisionMode(ThisReader.Value);
    //            }
    //            else if (((object)_ReaderLocalName == (object)id931_ValuePrecision))
    //            {
    //                o.@ValuePrecision = DCXMLConvert.ToInt32(ThisReader.Value);
    //            }
    //            else if (((object)_ReaderLocalName == (object)id932_AllowInterrupt))
    //            {
    //                o.@AllowInterrupt = DCXMLConvert.ToBoolean(ThisReader.Value);
    //            }
    //            else if (((object)_ReaderLocalName == (object)id232_LineWidth))
    //            {
    //                o.@LineWidth = DCXMLConvert.ToInt32(ThisReader.Value);
    //            }
    //            else if (((object)_ReaderLocalName == (object)id933_LanternValueColorForUpValue))
    //            {
    //                o.@LanternValueColorForUpValue = ThisReader.Value;
    //            }
    //            else if (((object)_ReaderLocalName == (object)id934_LanternValueColorForDownValue))
    //            {
    //                o.@LanternValueColorForDownValue = ThisReader.Value;
    //            }
    //            else if (((object)_ReaderLocalName == (object)id935_LineStyleForLanternValue))
    //            {
    //                o.@LineStyleForLanternValue = Read43_DashStyle(ThisReader.Value);
    //            }
    //            else if (((object)_ReaderLocalName == (object)id811_SymbolSize))
    //            {
    //                o.@SymbolSize = ToSingle(ThisReader.Value);
    //            }
    //            else if (((object)_ReaderLocalName == (object)id936_SpecifyTitleWidth))
    //            {
    //                o.@SpecifyTitleWidth = ToSingle(ThisReader.Value);
    //            }
    //            else if (((object)_ReaderLocalName == (object)id937_AllowOutofRange))
    //            {
    //                o.@AllowOutofRange = DCXMLConvert.ToBoolean(ThisReader.Value);
    //            }
    //            else if (((object)_ReaderLocalName == (object)id938_SeparatorLineVisible))
    //            {
    //                o.@SeparatorLineVisible = DCXMLConvert.ToBoolean(ThisReader.Value);
    //            }
    //            else if (((object)_ReaderLocalName == (object)id939_ClickToHide))
    //            {
    //                o.@ClickToHide = DCXMLConvert.ToBoolean(ThisReader.Value);
    //            }
    //            else if (((object)_ReaderLocalName == (object)id54_Visible))
    //            {
    //                o.@Visible = DCXMLConvert.ToBoolean(ThisReader.Value);
    //            }
    //            else if (((object)_ReaderLocalName == (object)id940_ValueVisible))
    //            {
    //                o.@ValueVisible = DCXMLConvert.ToBoolean(ThisReader.Value);
    //            }
    //            else if (((object)_ReaderLocalName == (object)id941_EnableLanternValue))
    //            {
    //                o.@EnableLanternValue = DCXMLConvert.ToBoolean(ThisReader.Value);
    //            }
    //            else if (((object)_ReaderLocalName == (object)id942_LanternValueTitle))
    //            {
    //                o.@LanternValueTitle = ThisReader.Value;
    //            }
    //            else if (((object)_ReaderLocalName == (object)id293_Style))
    //            {
    //                o.@Style = Read233_YAxisInfoStyle(ThisReader.Value);
    //            }
    //            else if (((object)_ReaderLocalName == (object)id943_HollowCovertTargetName))
    //            {
    //                o.@HollowCovertTargetName = ThisReader.Value;
    //            }
    //            else if (((object)_ReaderLocalName == (object)id944_ShadowName))
    //            {
    //                o.@ShadowName = ThisReader.Value;
    //            }
    //            else if (((object)_ReaderLocalName == (object)id945_TitleValueDispalyFormat))
    //            {
    //                o.@TitleValueDispalyFormat = ThisReader.Value;
    //            }
    //            else if (((object)_ReaderLocalName == (object)id946_TitleVisible))
    //            {
    //                o.@TitleVisible = DCXMLConvert.ToBoolean(ThisReader.Value);
    //            }
    //            else if (((object)_ReaderLocalName == (object)id238_Title))
    //            {
    //                o.@Title = ThisReader.Value;
    //            }
    //            else if (((object)_ReaderLocalName == (object)id947_YSplitNum))
    //            {
    //                o.@YSplitNum = DCXMLConvert.ToInt32(ThisReader.Value);
    //            }
    //            else if (((object)_ReaderLocalName == (object)id948_ValueFormatString))
    //            {
    //                o.@ValueFormatString = ThisReader.Value;
    //            }
    //            else if (((object)_ReaderLocalName == (object)id949_AlertLineColorValue))
    //            {
    //                o.@AlertLineColorValue = ThisReader.Value;
    //            }
    //            else if (((object)_ReaderLocalName == (object)id950_RedLineValue))
    //            {
    //                o.@RedLineValue = ToSingle(ThisReader.Value);
    //            }
    //            else if (((object)_ReaderLocalName == (object)id951_RedLineWidth))
    //            {
    //                o.@RedLineWidth = ToSingle(ThisReader.Value);
    //            }
    //            else if (((object)_ReaderLocalName == (object)id952_ValueTextBackColorValue))
    //            {
    //                o.@ValueTextBackColorValue = ThisReader.Value;
    //            }
    //            else if (((object)_ReaderLocalName == (object)id408_MaxValue))
    //            {
    //                o.@MaxValue = ToSingle(ThisReader.Value);
    //            }
    //            else if (((object)_ReaderLocalName == (object)id409_MinValue))
    //            {
    //                o.@MinValue = ToSingle(ThisReader.Value);
    //            }
    //            else if (((object)_ReaderLocalName == (object)id953_ShowLegendInRule))
    //            {
    //                o.@ShowLegendInRule = DCXMLConvert.ToBoolean(ThisReader.Value);
    //            }
    //            else if (((object)_ReaderLocalName == (object)id954_ShowPointValue))
    //            {
    //                o.@ShowPointValue = DCXMLConvert.ToBoolean(ThisReader.Value);
    //            }
    //            else if (((object)_ReaderLocalName == (object)id955_ColorValueForPointValue))
    //            {
    //                o.@ColorValueForPointValue = ThisReader.Value;
    //            }
    //            else if (((object)_ReaderLocalName == (object)id956_ColorValueForDownValue))
    //            {
    //                o.@ColorValueForDownValue = ThisReader.Value;
    //            }
    //            else if (((object)_ReaderLocalName == (object)id957_ColorValueForUpValue))
    //            {
    //                o.@ColorValueForUpValue = ThisReader.Value;
    //            }
    //            else if (((object)_ReaderLocalName == (object)id958_SymbolStyle))
    //            {
    //                o.@SymbolStyle = Read235_ValuePointSymbolStyle(ThisReader.Value);
    //            }
    //            else if (((object)_ReaderLocalName == (object)id848_SymbolOffsetX))
    //            {
    //                o.@SymbolOffsetX = ToSingle(ThisReader.Value);
    //            }
    //            else if (((object)_ReaderLocalName == (object)id849_SymbolOffsetY))
    //            {
    //                o.@SymbolOffsetY = ToSingle(ThisReader.Value);
    //            }
    //            else if (((object)_ReaderLocalName == (object)id852_IntCharSymbol))
    //            {
    //                o.@IntCharSymbol = DCXMLConvert.ToInt32(ThisReader.Value);
    //            }
    //            else if (((object)_ReaderLocalName == (object)id959_BottomTitle))
    //            {
    //                o.@BottomTitle = ThisReader.Value;
    //            }
    //            else if (((object)_ReaderLocalName == (object)id960_TitleBackColorValue))
    //            {
    //                o.@TitleBackColorValue = ThisReader.Value;
    //            }
    //            else if (((object)_ReaderLocalName == (object)id961_HiddenValueTitleBackColorValue))
    //            {
    //                o.@HiddenValueTitleBackColorValue = ThisReader.Value;
    //            }
    //            else if (((object)_ReaderLocalName == (object)id962_TitleColorValue))
    //            {
    //                o.@TitleColorValue = ThisReader.Value;
    //            }
    //            else if (((object)_ReaderLocalName == (object)id963_SymbolColorValue))
    //            {
    //                o.@SymbolColorValue = ThisReader.Value;
    //            }
    //            else if (((object)_ReaderLocalName == (object)id755_DataSourceName))
    //            {
    //                o.@DataSourceName = ThisReader.Value;
    //            }
    //            else if (((object)_ReaderLocalName == (object)id964_ValueFieldName))
    //            {
    //                o.@ValueFieldName = ThisReader.Value;
    //            }
    //            else if (((object)_ReaderLocalName == (object)id965_LanternValueFieldName))
    //            {
    //                o.@LanternValueFieldName = ThisReader.Value;
    //            }
    //            else if (((object)_ReaderLocalName == (object)id850_SpecifyLanternSymbolStyle))
    //            {
    //                o.@SpecifyLanternSymbolStyle = Read235_ValuePointSymbolStyle(ThisReader.Value);
    //            }
    //            else if (((object)_ReaderLocalName == (object)id851_IntCharLantern))
    //            {
    //                o.@IntCharLantern = DCXMLConvert.ToInt32(ThisReader.Value);
    //            }
    //            else if (((object)_ReaderLocalName == (object)id966_TimeFieldName))
    //            {
    //                o.@TimeFieldName = ThisReader.Value;
    //            }
    //        }
    //        ThisReader.MoveToElement();
    //        if (ThisReader.IsEmptyElement)
    //        {
    //            ThisReader.Skip();
    //            return o;
    //        }
    //        ThisReader.ReadStartElement();
    //        ThisReader.MoveToContent();
    //        int whileIterations322 = 0;
    //        int readerCount322 = ReaderCount;
    //        var _ReaderNodeType1 = ThisReader.NodeType;
    //        while (_ReaderNodeType1 != System.Xml.XmlNodeType.EndElement && _ReaderNodeType1 != System.Xml.XmlNodeType.None)
    //        {
    //            if (_ReaderNodeType1 == System.Xml.XmlNodeType.Element)
    //            {
    //                string _ReaderLocalName = ThisReader.LocalName;
    //                if (((object)_ReaderLocalName == (object)id967_MaxTextDisplayLength))
    //                {
    //                    if (ThisReader.IsEmptyElement)
    //                    {
    //                        ThisReader.Skip();
    //                    }
    //                    else
    //                    {
    //                        o.@MaxTextDisplayLength = ToSingle(ThisReader.ReadElementString());
    //                    }
    //                }
    //                else if (((object)_ReaderLocalName == (object)id968_TopPadding))
    //                {
    //                    if (ThisReader.IsEmptyElement)
    //                    {
    //                        ThisReader.Skip();
    //                    }
    //                    else
    //                    {
    //                        o.@TopPadding = ToSingle(ThisReader.ReadElementString());
    //                    }
    //                }
    //                else if (((object)_ReaderLocalName == (object)id969_BottomPadding))
    //                {
    //                    if (ThisReader.IsEmptyElement)
    //                    {
    //                        ThisReader.Skip();
    //                    }
    //                    else
    //                    {
    //                        o.@BottomPadding = ToSingle(ThisReader.ReadElementString());
    //                    }
    //                }
    //                else if (((object)_ReaderLocalName == (object)id142_Font))
    //                {
    //                    o.@Font = Read64_XFontValue(false, true);
    //                }
    //                else if (((object)_ReaderLocalName == (object)id970_ValueFont))
    //                {
    //                    o.@ValueFont = Read64_XFontValue(false, true);
    //                }
    //                else if (((object)_ReaderLocalName == (object)id392_DataSource))
    //                {
    //                    o.@DataSource = Read227_ValuePointDataSourceInfo(false, true);
    //                }
    //                else if (((object)_ReaderLocalName == (object)id971_ShadowPointVisible))
    //                {
    //                    if (ThisReader.IsEmptyElement)
    //                    {
    //                        ThisReader.Skip();
    //                    }
    //                    else
    //                    {
    //                        o.@ShadowPointVisible = DCXMLConvert.ToBoolean(ThisReader.ReadElementString());
    //                    }
    //                }
    //                else if (((object)_ReaderLocalName == (object)id854_VerticalLine))
    //                {
    //                    if (ThisReader.IsEmptyElement)
    //                    {
    //                        ThisReader.Skip();
    //                    }
    //                    else
    //                    {
    //                        o.@VerticalLine = DCXMLConvert.ToBoolean(ThisReader.ReadElementString());
    //                    }
    //                }
    //                else if (((object)_ReaderLocalName == (object)id972_RedLinePrintVisible))
    //                {
    //                    if (ThisReader.IsEmptyElement)
    //                    {
    //                        ThisReader.Skip();
    //                    }
    //                    else
    //                    {
    //                        o.@RedLinePrintVisible = DCXMLConvert.ToBoolean(ThisReader.ReadElementString());
    //                    }
    //                }
    //                else if (((object)_ReaderLocalName == (object)id973_AbNormalRangeSettings))
    //                {
    //                    o.@AbNormalRangeSettings = Read234_AbNormalRangeSettings(false, true);
    //                }
    //                else if (((object)_ReaderLocalName == (object)id974_Scales))
    //                {
    //                    if (true)
    //                    {//if (!ReadNull()) {
    //                        if ((ThisReader.IsEmptyElement))
    //                        {
    //                            ThisReader.Skip();
    //                        }
    //                        else
    //                        {
    //                            if ((o.@Scales) == null) o.@Scales = new global::DCSoft.TemperatureChart.YAxisScaleInfoList();
    //                            global::DCSoft.TemperatureChart.YAxisScaleInfoList a_63_0 = o.@Scales;
    //                            ThisReader.ReadStartElement();
    //                            ThisReader.MoveToContent();
    //                            int whileIterations323 = 0;
    //                            int readerCount323 = ReaderCount;
    //                            var _ReaderNodeType2 = ThisReader.NodeType;
    //                            while (_ReaderNodeType2 != System.Xml.XmlNodeType.EndElement && _ReaderNodeType2 != System.Xml.XmlNodeType.None)
    //                            {
    //                                if (_ReaderNodeType2 == System.Xml.XmlNodeType.Element)
    //                                {
    //                                    if (((object)ThisReader.LocalName == (object)id975_Scale))
    //                                    {
    //                                        if ((a_63_0) == null) ThisReader.Skip(); else a_63_0.Add(Read231_YAxisScaleInfo(true, true));
    //                                    }
    //                                    else
    //                                    {
    //                                        UnknownNode(null, ThisReader.LocalName); //":Scale");
    //                                    }
    //                                }
    //                                else
    //                                {
    //                                    UnknownNode(null, ThisReader.LocalName); //":Scale");
    //                                }
    //                                ThisReader.MoveToContent();
    //                                CheckReaderCount(ref whileIterations323, ref readerCount323);
    //                                _ReaderNodeType2 = ThisReader.NodeType;
    //                            }
    //                            ReadEndElement();
    //                        }
    //                    }
    //                }
    //                else
    //                {
    //                    UnknownNode(o, _ReaderLocalName);//":MaxTextDisplayLength, :TopPadding, :BottomPadding, :Font, :ValueFont, :DataSource, :ShadowPointVisible, :VerticalLine, :RedLinePrintVisible, :AbNormalRangeSettings, :Scales");
    //                }
    //            }
    //            else
    //            {
    //                UnknownNode(o, ThisReader.LocalName);//":MaxTextDisplayLength, :TopPadding, :BottomPadding, :Font, :ValueFont, :DataSource, :ShadowPointVisible, :VerticalLine, :RedLinePrintVisible, :AbNormalRangeSettings, :Scales");
    //            }
    //            ThisReader.MoveToContent();
    //            CheckReaderCount(ref whileIterations322, ref readerCount322);
    //            _ReaderNodeType1 = ThisReader.NodeType;
    //        }
    //        ReadEndElement();
    //        return o;
    //    }
    //    internal protected global::DCSoft.TemperatureChart.YAxisScaleInfo Read231_YAxisScaleInfo(bool isNullable, bool checkType)
    //    {
    //        var ThisReader = this.Reader;
    //        global::DCSoft.TemperatureChart.YAxisScaleInfo o;
    //        o = new global::DCSoft.TemperatureChart.YAxisScaleInfo();
    //        //bool[] paramsRead = new bool[4];
    //        while (ThisReader.MoveToNextAttribute())
    //        {
    //            string _ReaderLocalName = ThisReader.LocalName;
    //            if (((object)_ReaderLocalName == (object)id146_Text))
    //            {
    //                o.@Text = ThisReader.Value;
    //            }
    //            else if (((object)_ReaderLocalName == (object)id248_Value))
    //            {
    //                o.@Value = ToSingle(ThisReader.Value);
    //            }
    //            else if (((object)_ReaderLocalName == (object)id977_ScaleRate))
    //            {
    //                o.@ScaleRate = ToSingle(ThisReader.Value);
    //            }
    //            else if (((object)_ReaderLocalName == (object)id143_ColorValue))
    //            {
    //                o.@ColorValue = ThisReader.Value;
    //            }
    //        }
    //        ThisReader.MoveToElement();
    //        if (ThisReader.IsEmptyElement)
    //        {
    //            ThisReader.Skip();
    //            return o;
    //        }
    //        ThisReader.ReadStartElement();
    //        ThisReader.MoveToContent();
    //        int whileIterations324 = 0;
    //        int readerCount324 = ReaderCount;
    //        var _ReaderNodeType1 = ThisReader.NodeType;
    //        while (_ReaderNodeType1 != System.Xml.XmlNodeType.EndElement && _ReaderNodeType1 != System.Xml.XmlNodeType.None)
    //        {
    //            if (_ReaderNodeType1 == System.Xml.XmlNodeType.Element)
    //            {
    //                UnknownNode(o, ThisReader.LocalName);
    //            }
    //            else
    //            {
    //                UnknownNode(o, ThisReader.LocalName);
    //            }
    //            ThisReader.MoveToContent();
    //            CheckReaderCount(ref whileIterations324, ref readerCount324);
    //            _ReaderNodeType1 = ThisReader.NodeType;
    //        }
    //        ReadEndElement();
    //        return o;
    //    }
    //    internal protected global::DCSoft.TemperatureChart.AbNormalRangeSettings Read234_AbNormalRangeSettings(bool isNullable, bool checkType)
    //    {
    //        var ThisReader = this.Reader;
    //        global::DCSoft.TemperatureChart.AbNormalRangeSettings o;
    //        o = new global::DCSoft.TemperatureChart.AbNormalRangeSettings();
    //        //bool[] paramsRead = new bool[6];
    //        while (ThisReader.MoveToNextAttribute())
    //        {
    //            string _ReaderLocalName = ThisReader.LocalName;
    //            if (((object)_ReaderLocalName == (object)id978_NormalRangeBackColorValue))
    //            {
    //                o.@NormalRangeBackColorValue = ThisReader.Value;
    //            }
    //            else if (((object)_ReaderLocalName == (object)id979_OutofNormalRangeBackColorValue))
    //            {
    //                o.@OutofNormalRangeBackColorValue = ThisReader.Value;
    //            }
    //            else if (((object)_ReaderLocalName == (object)id980_NormalMaxValue))
    //            {
    //                o.@NormalMaxValue = ToSingle(ThisReader.Value);
    //            }
    //            else if (((object)_ReaderLocalName == (object)id981_NormalMinValue))
    //            {
    //                o.@NormalMinValue = ToSingle(ThisReader.Value);
    //            }
    //        }
    //        ThisReader.MoveToElement();
    //        if (ThisReader.IsEmptyElement)
    //        {
    //            ThisReader.Skip();
    //            return o;
    //        }
    //        ThisReader.ReadStartElement();
    //        ThisReader.MoveToContent();
    //        int whileIterations325 = 0;
    //        int readerCount325 = ReaderCount;
    //        var _ReaderNodeType1 = ThisReader.NodeType;
    //        while (_ReaderNodeType1 != System.Xml.XmlNodeType.EndElement && _ReaderNodeType1 != System.Xml.XmlNodeType.None)
    //        {
    //            if (_ReaderNodeType1 == System.Xml.XmlNodeType.Element)
    //            {
    //                string _ReaderLocalName = ThisReader.LocalName;
    //                if (((object)_ReaderLocalName == (object)id982_NormalRangeUpLineStyle))
    //                {
    //                    o.@NormalRangeUpLineStyle = Read192_XPenStyle(false, true);
    //                }
    //                else if (((object)_ReaderLocalName == (object)id983_NormalRangeDownLineStyle))
    //                {
    //                    o.@NormalRangeDownLineStyle = Read192_XPenStyle(false, true);
    //                }
    //                else
    //                {
    //                    UnknownNode(o, _ReaderLocalName);//":NormalRangeUpLineStyle, :NormalRangeDownLineStyle");
    //                }
    //            }
    //            else
    //            {
    //                UnknownNode(o, ThisReader.LocalName);//":NormalRangeUpLineStyle, :NormalRangeDownLineStyle");
    //            }
    //            ThisReader.MoveToContent();
    //            CheckReaderCount(ref whileIterations325, ref readerCount325);
    //            _ReaderNodeType1 = ThisReader.NodeType;
    //        }
    //        ReadEndElement();
    //        return o;
    //    }
    //    internal protected global::DCSoft.TemperatureChart.ValuePointDataSourceInfo Read227_ValuePointDataSourceInfo(bool isNullable, bool checkType)
    //    {
    //        var ThisReader = this.Reader;
    //        global::DCSoft.TemperatureChart.ValuePointDataSourceInfo o;
    //        o = new global::DCSoft.TemperatureChart.ValuePointDataSourceInfo();
    //        //bool[] paramsRead = new bool[8];
    //        while (ThisReader.MoveToNextAttribute())
    //        {
    //            string _ReaderLocalName = ThisReader.LocalName;
    //            if (((object)_ReaderLocalName == (object)id985_FieldNameForID))
    //            {
    //                o.@FieldNameForID = ThisReader.Value;
    //            }
    //            else if (((object)_ReaderLocalName == (object)id986_FieldNameForLink))
    //            {
    //                o.@FieldNameForLink = ThisReader.Value;
    //            }
    //            else if (((object)_ReaderLocalName == (object)id987_FieldNameForTitle))
    //            {
    //                o.@FieldNameForTitle = ThisReader.Value;
    //            }
    //            else if (((object)_ReaderLocalName == (object)id988_FieldNameForTime))
    //            {
    //                o.@FieldNameForTime = ThisReader.Value;
    //            }
    //            else if (((object)_ReaderLocalName == (object)id989_FieldNameForValue))
    //            {
    //                o.@FieldNameForValue = ThisReader.Value;
    //            }
    //            else if (((object)_ReaderLocalName == (object)id990_FieldNameForLanternValue))
    //            {
    //                o.@FieldNameForLanternValue = ThisReader.Value;
    //            }
    //            else if (((object)_ReaderLocalName == (object)id991_FieldNameForText))
    //            {
    //                o.@FieldNameForText = ThisReader.Value;
    //            }
    //        }
    //        ThisReader.MoveToElement();
    //        if (ThisReader.IsEmptyElement)
    //        {
    //            ThisReader.Skip();
    //            return o;
    //        }
    //        ThisReader.ReadStartElement();
    //        ThisReader.MoveToContent();
    //        int whileIterations326 = 0;
    //        int readerCount326 = ReaderCount;
    //        var _ReaderNodeType1 = ThisReader.NodeType;
    //        while (_ReaderNodeType1 != System.Xml.XmlNodeType.EndElement && _ReaderNodeType1 != System.Xml.XmlNodeType.None)
    //        {
    //            if (_ReaderNodeType1 == System.Xml.XmlNodeType.Element)
    //            {
    //                string _ReaderLocalName = ThisReader.LocalName;
    //                if (((object)_ReaderLocalName == (object)id992_SQLText))
    //                {
    //                    {
    //                        o.@SQLText = CacheString(ThisReader.ReadElementString());
    //                    }
    //                }
    //                else
    //                {
    //                    UnknownNode(o, _ReaderLocalName);//":SQLText");
    //                }
    //            }
    //            else
    //            {
    //                UnknownNode(o, ThisReader.LocalName);//":SQLText");
    //            }
    //            ThisReader.MoveToContent();
    //            CheckReaderCount(ref whileIterations326, ref readerCount326);
    //            _ReaderNodeType1 = ThisReader.NodeType;
    //        }
    //        ReadEndElement();
    //        return o;
    //    }
    //    internal protected global::DCSoft.TemperatureChart.YAxisInfoStyle Read233_YAxisInfoStyle(string s)
    //    {
    //        switch (s)
    //        {
    //            case @"Value": return global::DCSoft.TemperatureChart.YAxisInfoStyle.@Value;
    //            case @"Text": return global::DCSoft.TemperatureChart.YAxisInfoStyle.@Text;
    //            case @"Background": return global::DCSoft.TemperatureChart.YAxisInfoStyle.@Background;
    //            case @"PartialBackground": return global::DCSoft.TemperatureChart.YAxisInfoStyle.@PartialBackground;
    //            case @"TextInsideGrid": return global::DCSoft.TemperatureChart.YAxisInfoStyle.@TextInsideGrid;
    //            default: return (default(global::DCSoft.TemperatureChart.YAxisInfoStyle));
    //        }
    //    }
    //    internal protected global::DCSoft.TemperatureChart.DateTimePrecisionMode Read225_DateTimePrecisionMode(string s)
    //    {

    //        if (__DCSoft_TemperatureChart_DateTimePrecisionMode == null)
    //        {
    //            lock (_NewDictionaryLockObject)
    //            {
    //                var dic20200818 = new System.Collections.Generic.Dictionary<string, DCSoft.TemperatureChart.DateTimePrecisionMode>();
    //                dic20200818.Add("NoLimited", DCSoft.TemperatureChart.DateTimePrecisionMode.@NoLimited);
    //                dic20200818.Add("Second", DCSoft.TemperatureChart.DateTimePrecisionMode.@Second);
    //                dic20200818.Add("Minute", DCSoft.TemperatureChart.DateTimePrecisionMode.@Minute);
    //                dic20200818.Add("Hour", DCSoft.TemperatureChart.DateTimePrecisionMode.@Hour);
    //                dic20200818.Add("Day", DCSoft.TemperatureChart.DateTimePrecisionMode.@Day);
    //                dic20200818.Add("Month", DCSoft.TemperatureChart.DateTimePrecisionMode.@Month);
    //                dic20200818.Add("Year", DCSoft.TemperatureChart.DateTimePrecisionMode.@Year);
    //                __DCSoft_TemperatureChart_DateTimePrecisionMode = dic20200818;
    //            }
    //        }
    //        var result = default(DCSoft.TemperatureChart.DateTimePrecisionMode);
    //        if (__DCSoft_TemperatureChart_DateTimePrecisionMode.TryGetValue(s, out result))
    //        {
    //            return result;
    //        }
    //        else
    //        {
    //            return default(DCSoft.TemperatureChart.DateTimePrecisionMode);
    //        }
    //    }
    //    private static System.Collections.Generic.Dictionary<string, DCSoft.TemperatureChart.DateTimePrecisionMode> __DCSoft_TemperatureChart_DateTimePrecisionMode = null;

    //    internal protected global::DCSoft.TemperatureChart.TitleLineInfo Read232_TitleLineInfo(bool isNullable, bool checkType)
    //    {
    //        var ThisReader = this.Reader;
    //        global::DCSoft.TemperatureChart.TitleLineInfo o;
    //        o = new global::DCSoft.TemperatureChart.TitleLineInfo();
    //        //            if ((object)(o.@Scales) == null) o.@Scales = new global::DCSoft.TemperatureChart.YAxisScaleInfoList();
    //        //            global::DCSoft.TemperatureChart.YAxisScaleInfoList a_46 = (global::DCSoft.TemperatureChart.YAxisScaleInfoList)o.@Scales;
    //        //bool[] paramsRead = new bool[47];
    //        while (ThisReader.MoveToNextAttribute())
    //        {
    //            string _ReaderLocalName = ThisReader.LocalName;
    //            if (((object)_ReaderLocalName == (object)id930_InputTimePrecision))
    //            {
    //                o.@InputTimePrecision = Read225_DateTimePrecisionMode(ThisReader.Value);
    //            }
    //            else if (((object)_ReaderLocalName == (object)id62_Name))
    //            {
    //                o.@Name = ThisReader.Value;
    //            }
    //            else if (((object)_ReaderLocalName == (object)id670_AutoHeight))
    //            {
    //                o.@AutoHeight = DCXMLConvert.ToBoolean(ThisReader.Value);
    //            }
    //            else if (((object)_ReaderLocalName == (object)id994_VisibleWhenNoValuePoint))
    //            {
    //                o.@VisibleWhenNoValuePoint = DCXMLConvert.ToBoolean(ThisReader.Value);
    //            }
    //            else if (((object)_ReaderLocalName == (object)id54_Visible))
    //            {
    //                o.@Visible = DCXMLConvert.ToBoolean(ThisReader.Value);
    //            }
    //            else if (((object)_ReaderLocalName == (object)id995_BlankDateWhenNoData))
    //            {
    //                o.@BlankDateWhenNoData = DCXMLConvert.ToBoolean(ThisReader.Value);
    //            }
    //            else if (((object)_ReaderLocalName == (object)id996_Item))
    //            {
    //                o.@HiddenOnPageViewWhenNoValuePoints = DCXMLConvert.ToBoolean(ThisReader.Value);
    //            }
    //            else if (((object)_ReaderLocalName == (object)id481_GroupName))
    //            {
    //                o.@GroupName = ThisReader.Value;
    //            }
    //            else if (((object)_ReaderLocalName == (object)id997_AfterOperaDaysFromZero))
    //            {
    //                o.@AfterOperaDaysFromZero = DCXMLConvert.ToBoolean(ThisReader.Value);
    //            }
    //            else if (((object)_ReaderLocalName == (object)id998_AfterOperaDaysBeginOne))
    //            {
    //                o.@AfterOperaDaysBeginOne = DCXMLConvert.ToBoolean(ThisReader.Value);
    //            }
    //            else if (((object)_ReaderLocalName == (object)id999_OutofNormalRangeTextColorValue))
    //            {
    //                o.@OutofNormalRangeTextColorValue = ThisReader.Value;
    //            }
    //            else if (((object)_ReaderLocalName == (object)id980_NormalMaxValue))
    //            {
    //                o.@NormalMaxValue = ToSingle(ThisReader.Value);
    //            }
    //            else if (((object)_ReaderLocalName == (object)id981_NormalMinValue))
    //            {
    //                o.@NormalMinValue = ToSingle(ThisReader.Value);
    //            }
    //            else if (((object)_ReaderLocalName == (object)id1000_ExtendGridLineType))
    //            {
    //                o.@ExtendGridLineType = Read226_DCExtendGridLineType(ThisReader.Value);
    //            }
    //            else if (((object)_ReaderLocalName == (object)id1001_EnableEndTime))
    //            {
    //                o.@EnableEndTime = DCXMLConvert.ToBoolean(ThisReader.Value);
    //            }
    //            else if (((object)_ReaderLocalName == (object)id1002_BlockWidth))
    //            {
    //                o.@BlockWidth = ToSingle(ThisReader.Value);
    //            }
    //            else if (((object)_ReaderLocalName == (object)id1003_ValueDisplayFormat))
    //            {
    //                o.@ValueDisplayFormat = ThisReader.Value;
    //            }
    //            else if (((object)_ReaderLocalName == (object)id1004_LoopTextList))
    //            {
    //                o.@LoopTextList = ThisReader.Value;
    //            }
    //            else if (((object)_ReaderLocalName == (object)id936_SpecifyTitleWidth))
    //            {
    //                o.@SpecifyTitleWidth = ToSingle(ThisReader.Value);
    //            }
    //            else if (((object)_ReaderLocalName == (object)id238_Title))
    //            {
    //                o.@Title = ThisReader.Value;
    //            }
    //            else if (((object)_ReaderLocalName == (object)id1005_PageTitleTexts))
    //            {
    //                o.@PageTitleTexts = ThisReader.Value;
    //            }
    //            else if (((object)_ReaderLocalName == (object)id962_TitleColorValue))
    //            {
    //                o.@TitleColorValue = ThisReader.Value;
    //            }
    //            else if (((object)_ReaderLocalName == (object)id766_TextColorValue))
    //            {
    //                o.@TextColorValue = ThisReader.Value;
    //            }
    //            else if (((object)_ReaderLocalName == (object)id1006_TitleAlign))
    //            {
    //                o.@TitleAlign = Read119_StringAlignment(ThisReader.Value);
    //            }
    //            else if (((object)_ReaderLocalName == (object)id1007_ValueAlign))
    //            {
    //                o.@ValueAlign = Read119_StringAlignment(ThisReader.Value);
    //            }
    //            else if (((object)_ReaderLocalName == (object)id1008_MaxValueForDayIndex))
    //            {
    //                o.@MaxValueForDayIndex = DCXMLConvert.ToInt32(ThisReader.Value);
    //            }
    //            else if (((object)_ReaderLocalName == (object)id1009_CircleText))
    //            {
    //                o.@CircleText = ThisReader.Value;
    //            }
    //            else if (((object)_ReaderLocalName == (object)id457_SpecifyHeight))
    //            {
    //                o.@SpecifyHeight = ToSingle(ThisReader.Value);
    //            }
    //            else if (((object)_ReaderLocalName == (object)id1010_EndDateKeyword))
    //            {
    //                o.@EndDateKeyword = ThisReader.Value;
    //            }
    //            else if (((object)_ReaderLocalName == (object)id1011_StartDate))
    //            {
    //                o.@StartDate = ToDateTime(ThisReader.Value);
    //            }
    //            else if (((object)_ReaderLocalName == (object)id1012_StartDateKeyword))
    //            {
    //                o.@StartDateKeyword = ThisReader.Value;
    //            }
    //            else if (((object)_ReaderLocalName == (object)id1013_PreserveStartKeywordOrder))
    //            {
    //                o.@PreserveStartKeywordOrder = DCXMLConvert.ToBoolean(ThisReader.Value);
    //            }
    //            else if (((object)_ReaderLocalName == (object)id1014_ShowBackColor))
    //            {
    //                o.@ShowBackColor = DCXMLConvert.ToBoolean(ThisReader.Value);
    //            }
    //            else if (((object)_ReaderLocalName == (object)id1015_LayoutType))
    //            {
    //                o.@LayoutType = Read228_TitleLineLayoutType(ThisReader.Value);
    //            }
    //            else if (((object)_ReaderLocalName == (object)id1016_TickStep))
    //            {
    //                o.@TickStep = DCXMLConvert.ToInt32(ThisReader.Value);
    //            }
    //            else if (((object)_ReaderLocalName == (object)id1017_TickLineVisible))
    //            {
    //                o.@TickLineVisible = DCXMLConvert.ToBoolean(ThisReader.Value);
    //            }
    //            else if (((object)_ReaderLocalName == (object)id1018_ForceUpWhenPageFirstPoint))
    //            {
    //                o.@ForceUpWhenPageFirstPoint = DCXMLConvert.ToBoolean(ThisReader.Value);
    //            }
    //            else if (((object)_ReaderLocalName == (object)id1019_UpAndDownTextType))
    //            {
    //                o.@UpAndDownTextType = Read229_UpAndDownTextType(ThisReader.Value);
    //            }
    //            else if (((object)_ReaderLocalName == (object)id318_ValueType))
    //            {
    //                o.@ValueType = Read230_TitleLineValueType(ThisReader.Value);
    //            }
    //            else if (((object)_ReaderLocalName == (object)id755_DataSourceName))
    //            {
    //                o.@DataSourceName = ThisReader.Value;
    //            }
    //            else if (((object)_ReaderLocalName == (object)id964_ValueFieldName))
    //            {
    //                o.@ValueFieldName = ThisReader.Value;
    //            }
    //            else if (((object)_ReaderLocalName == (object)id966_TimeFieldName))
    //            {
    //                o.@TimeFieldName = ThisReader.Value;
    //            }
    //        }
    //        ThisReader.MoveToElement();
    //        if (ThisReader.IsEmptyElement)
    //        {
    //            ThisReader.Skip();
    //            return o;
    //        }
    //        ThisReader.ReadStartElement();
    //        ThisReader.MoveToContent();
    //        int whileIterations327 = 0;
    //        int readerCount327 = ReaderCount;
    //        var _ReaderNodeType1 = ThisReader.NodeType;
    //        while (_ReaderNodeType1 != System.Xml.XmlNodeType.EndElement && _ReaderNodeType1 != System.Xml.XmlNodeType.None)
    //        {
    //            if (_ReaderNodeType1 == System.Xml.XmlNodeType.Element)
    //            {
    //                string _ReaderLocalName = ThisReader.LocalName;
    //                if (((object)_ReaderLocalName == (object)id1020_ValueTextMultiLine))
    //                {
    //                    if (ThisReader.IsEmptyElement)
    //                    {
    //                        ThisReader.Skip();
    //                    }
    //                    else
    //                    {
    //                        o.@ValueTextMultiLine = DCXMLConvert.ToBoolean(ThisReader.ReadElementString());
    //                    }
    //                }
    //                else if (((object)_ReaderLocalName == (object)id142_Font))
    //                {
    //                    o.@Font = Read64_XFontValue(false, true);
    //                }
    //                else if (((object)_ReaderLocalName == (object)id970_ValueFont))
    //                {
    //                    o.@ValueFont = Read64_XFontValue(false, true);
    //                }
    //                else if (((object)_ReaderLocalName == (object)id392_DataSource))
    //                {
    //                    o.@DataSource = Read227_ValuePointDataSourceInfo(false, true);
    //                }
    //                else if (((object)_ReaderLocalName == (object)id974_Scales))
    //                {
    //                    if (true)
    //                    {//if (!ReadNull()) {
    //                        if ((ThisReader.IsEmptyElement))
    //                        {
    //                            ThisReader.Skip();
    //                        }
    //                        else
    //                        {
    //                            if ((o.@Scales) == null) o.@Scales = new global::DCSoft.TemperatureChart.YAxisScaleInfoList();
    //                            global::DCSoft.TemperatureChart.YAxisScaleInfoList a_46_0 = o.@Scales;
    //                            ThisReader.ReadStartElement();
    //                            ThisReader.MoveToContent();
    //                            int whileIterations328 = 0;
    //                            int readerCount328 = ReaderCount;
    //                            var _ReaderNodeType2 = ThisReader.NodeType;
    //                            while (_ReaderNodeType2 != System.Xml.XmlNodeType.EndElement && _ReaderNodeType2 != System.Xml.XmlNodeType.None)
    //                            {
    //                                if (_ReaderNodeType2 == System.Xml.XmlNodeType.Element)
    //                                {
    //                                    if (((object)ThisReader.LocalName == (object)id975_Scale))
    //                                    {
    //                                        if ((a_46_0) == null) ThisReader.Skip(); else a_46_0.Add(Read231_YAxisScaleInfo(true, true));
    //                                    }
    //                                    else
    //                                    {
    //                                        UnknownNode(null, ThisReader.LocalName); //":Scale");
    //                                    }
    //                                }
    //                                else
    //                                {
    //                                    UnknownNode(null, ThisReader.LocalName); //":Scale");
    //                                }
    //                                ThisReader.MoveToContent();
    //                                CheckReaderCount(ref whileIterations328, ref readerCount328);
    //                                _ReaderNodeType2 = ThisReader.NodeType;
    //                            }
    //                            ReadEndElement();
    //                        }
    //                    }
    //                }
    //                else
    //                {
    //                    UnknownNode(o, _ReaderLocalName);//":ValueTextMultiLine, :Font, :ValueFont, :DataSource, :Scales");
    //                }
    //            }
    //            else
    //            {
    //                UnknownNode(o, ThisReader.LocalName);//":ValueTextMultiLine, :Font, :ValueFont, :DataSource, :Scales");
    //            }
    //            ThisReader.MoveToContent();
    //            CheckReaderCount(ref whileIterations327, ref readerCount327);
    //            _ReaderNodeType1 = ThisReader.NodeType;
    //        }
    //        ReadEndElement();
    //        return o;
    //    }
    //    internal protected global::DCSoft.TemperatureChart.TitleLineValueType Read230_TitleLineValueType(string s)
    //    {

    //        if (__DCSoft_TemperatureChart_TitleLineValueType == null)
    //        {
    //            lock (_NewDictionaryLockObject)
    //            {
    //                var dic20200818 = new System.Collections.Generic.Dictionary<string, DCSoft.TemperatureChart.TitleLineValueType>();
    //                dic20200818.Add("NewSerialDate", DCSoft.TemperatureChart.TitleLineValueType.@NewSerialDate);
    //                dic20200818.Add("SerialDate", DCSoft.TemperatureChart.TitleLineValueType.@SerialDate);
    //                dic20200818.Add("GlobalDayIndex", DCSoft.TemperatureChart.TitleLineValueType.@GlobalDayIndex);
    //                dic20200818.Add("InDayIndex", DCSoft.TemperatureChart.TitleLineValueType.@InDayIndex);
    //                dic20200818.Add("DayIndex", DCSoft.TemperatureChart.TitleLineValueType.@DayIndex);
    //                dic20200818.Add("HourTick", DCSoft.TemperatureChart.TitleLineValueType.@HourTick);
    //                dic20200818.Add("Text", DCSoft.TemperatureChart.TitleLineValueType.@Text);
    //                dic20200818.Add("Data", DCSoft.TemperatureChart.TitleLineValueType.@Data);
    //                dic20200818.Add("TickText", DCSoft.TemperatureChart.TitleLineValueType.@TickText);
    //                __DCSoft_TemperatureChart_TitleLineValueType = dic20200818;
    //            }
    //        }
    //        var result = default(DCSoft.TemperatureChart.TitleLineValueType);
    //        if (__DCSoft_TemperatureChart_TitleLineValueType.TryGetValue(s, out result))
    //        {
    //            return result;
    //        }
    //        else
    //        {
    //            return default(DCSoft.TemperatureChart.TitleLineValueType);
    //        }
    //    }
    //    private static System.Collections.Generic.Dictionary<string, DCSoft.TemperatureChart.TitleLineValueType> __DCSoft_TemperatureChart_TitleLineValueType = null;

    //    internal protected global::DCSoft.TemperatureChart.UpAndDownTextType Read229_UpAndDownTextType(string s)
    //    {
    //        switch (s)
    //        {
    //            case @"None": return global::DCSoft.TemperatureChart.UpAndDownTextType.@None;
    //            case @"ShowByTick": return global::DCSoft.TemperatureChart.UpAndDownTextType.@ShowByTick;
    //            case @"ShowByText": return global::DCSoft.TemperatureChart.UpAndDownTextType.@ShowByText;
    //            default: return (default(global::DCSoft.TemperatureChart.UpAndDownTextType));
    //        }
    //    }
    //    internal protected global::DCSoft.TemperatureChart.TitleLineLayoutType Read228_TitleLineLayoutType(string s)
    //    {

    //        if (__DCSoft_TemperatureChart_TitleLineLayoutType == null)
    //        {
    //            lock (_NewDictionaryLockObject)
    //            {
    //                var dic20200818 = new System.Collections.Generic.Dictionary<string, DCSoft.TemperatureChart.TitleLineLayoutType>();
    //                dic20200818.Add("Normal", DCSoft.TemperatureChart.TitleLineLayoutType.@Normal);
    //                dic20200818.Add("Free", DCSoft.TemperatureChart.TitleLineLayoutType.@Free);
    //                dic20200818.Add("FreeText", DCSoft.TemperatureChart.TitleLineLayoutType.@FreeText);
    //                dic20200818.Add("Cascade", DCSoft.TemperatureChart.TitleLineLayoutType.@Cascade);
    //                dic20200818.Add("HorizCascade", DCSoft.TemperatureChart.TitleLineLayoutType.@HorizCascade);
    //                dic20200818.Add("AutoCascade", DCSoft.TemperatureChart.TitleLineLayoutType.@AutoCascade);
    //                dic20200818.Add("Slant", DCSoft.TemperatureChart.TitleLineLayoutType.@Slant);
    //                dic20200818.Add("Slant2", DCSoft.TemperatureChart.TitleLineLayoutType.@Slant2);
    //                dic20200818.Add("Slant3", DCSoft.TemperatureChart.TitleLineLayoutType.@Slant3);
    //                dic20200818.Add("Fraction", DCSoft.TemperatureChart.TitleLineLayoutType.@Fraction);
    //                __DCSoft_TemperatureChart_TitleLineLayoutType = dic20200818;
    //            }
    //        }
    //        var result = default(DCSoft.TemperatureChart.TitleLineLayoutType);
    //        if (__DCSoft_TemperatureChart_TitleLineLayoutType.TryGetValue(s, out result))
    //        {
    //            return result;
    //        }
    //        else
    //        {
    //            return default(DCSoft.TemperatureChart.TitleLineLayoutType);
    //        }
    //    }
    //    private static System.Collections.Generic.Dictionary<string, DCSoft.TemperatureChart.TitleLineLayoutType> __DCSoft_TemperatureChart_TitleLineLayoutType = null;

    //    internal protected global::DCSoft.TemperatureChart.DCExtendGridLineType Read226_DCExtendGridLineType(string s)
    //    {
    //        switch (s)
    //        {
    //            case @"None": return global::DCSoft.TemperatureChart.DCExtendGridLineType.@None;
    //            case @"Above": return global::DCSoft.TemperatureChart.DCExtendGridLineType.@Above;
    //            case @"Below": return global::DCSoft.TemperatureChart.DCExtendGridLineType.@Below;
    //            default: return (default(global::DCSoft.TemperatureChart.DCExtendGridLineType));
    //        }
    //    }
    //    internal protected global::DCSoft.TemperatureChart.HeaderLabelInfo Read224_HeaderLabelInfo(bool isNullable, bool checkType)
    //    {
    //        var ThisReader = this.Reader;
    //        global::DCSoft.TemperatureChart.HeaderLabelInfo o;
    //        o = new global::DCSoft.TemperatureChart.HeaderLabelInfo();
    //        //bool[] paramsRead = new bool[6];
    //        while (ThisReader.MoveToNextAttribute())
    //        {
    //            string _ReaderLocalName = ThisReader.LocalName;
    //            if (((object)_ReaderLocalName == (object)id238_Title))
    //            {
    //                o.@Title = ThisReader.Value;
    //            }
    //            else if (((object)_ReaderLocalName == (object)id755_DataSourceName))
    //            {
    //                o.@DataSourceName = ThisReader.Value;
    //            }
    //            else if (((object)_ReaderLocalName == (object)id964_ValueFieldName))
    //            {
    //                o.@ValueFieldName = ThisReader.Value;
    //            }
    //            else if (((object)_ReaderLocalName == (object)id1022_ParameterName))
    //            {
    //                o.@ParameterName = ThisReader.Value;
    //            }
    //            else if (((object)_ReaderLocalName == (object)id248_Value))
    //            {
    //                o.@Value = ThisReader.Value;
    //            }
    //            else if (((object)_ReaderLocalName == (object)id1023_SeperatorChar))
    //            {
    //                o.@SeperatorChar = ToChar(ThisReader.Value);
    //            }
    //        }
    //        ThisReader.MoveToElement();
    //        if (ThisReader.IsEmptyElement)
    //        {
    //            ThisReader.Skip();
    //            return o;
    //        }
    //        ThisReader.ReadStartElement();
    //        ThisReader.MoveToContent();
    //        int whileIterations329 = 0;
    //        int readerCount329 = ReaderCount;
    //        var _ReaderNodeType1 = ThisReader.NodeType;
    //        while (_ReaderNodeType1 != System.Xml.XmlNodeType.EndElement && _ReaderNodeType1 != System.Xml.XmlNodeType.None)
    //        {
    //            if (_ReaderNodeType1 == System.Xml.XmlNodeType.Element)
    //            {
    //                UnknownNode(o, ThisReader.LocalName);
    //            }
    //            else
    //            {
    //                UnknownNode(o, ThisReader.LocalName);
    //            }
    //            ThisReader.MoveToContent();
    //            CheckReaderCount(ref whileIterations329, ref readerCount329);
    //            _ReaderNodeType1 = ThisReader.NodeType;
    //        }
    //        ReadEndElement();
    //        return o;
    //    }
    //    internal protected global::DCSoft.TemperatureChart.TickInfo Read222_TickInfo(bool isNullable, bool checkType)
    //    {
    //        var ThisReader = this.Reader;
    //        global::DCSoft.TemperatureChart.TickInfo o;
    //        o = new global::DCSoft.TemperatureChart.TickInfo();
    //        //bool[] paramsRead = new bool[3];
    //        while (ThisReader.MoveToNextAttribute())
    //        {
    //            string _ReaderLocalName = ThisReader.LocalName;
    //            if (((object)_ReaderLocalName == (object)id248_Value))
    //            {
    //                o.@Value = ToSingle(ThisReader.Value);
    //            }
    //            else if (((object)_ReaderLocalName == (object)id146_Text))
    //            {
    //                o.@Text = ThisReader.Value;
    //            }
    //            else if (((object)_ReaderLocalName == (object)id143_ColorValue))
    //            {
    //                o.@ColorValue = ThisReader.Value;
    //            }
    //        }
    //        ThisReader.MoveToElement();
    //        if (ThisReader.IsEmptyElement)
    //        {
    //            ThisReader.Skip();
    //            return o;
    //        }
    //        ThisReader.ReadStartElement();
    //        ThisReader.MoveToContent();
    //        int whileIterations330 = 0;
    //        int readerCount330 = ReaderCount;
    //        var _ReaderNodeType1 = ThisReader.NodeType;
    //        while (_ReaderNodeType1 != System.Xml.XmlNodeType.EndElement && _ReaderNodeType1 != System.Xml.XmlNodeType.None)
    //        {
    //            if (_ReaderNodeType1 == System.Xml.XmlNodeType.Element)
    //            {
    //                UnknownNode(o, ThisReader.LocalName);
    //            }
    //            else
    //            {
    //                UnknownNode(o, ThisReader.LocalName);
    //            }
    //            ThisReader.MoveToContent();
    //            CheckReaderCount(ref whileIterations330, ref readerCount330);
    //            _ReaderNodeType1 = ThisReader.NodeType;
    //        }
    //        ReadEndElement();
    //        return o;
    //    }
    //    internal protected global::DCSoft.TemperatureChart.TimeLineZoneInfo Read223_TimeLineZoneInfo(bool isNullable, bool checkType)
    //    {
    //        var ThisReader = this.Reader;
    //        global::DCSoft.TemperatureChart.TimeLineZoneInfo o;
    //        o = new global::DCSoft.TemperatureChart.TimeLineZoneInfo();
    //        //            if ((object)(o.@Ticks) == null) o.@Ticks = new global::DCSoft.TemperatureChart.TickInfoList();
    //        //            global::DCSoft.TemperatureChart.TickInfoList a_8 = (global::DCSoft.TemperatureChart.TickInfoList)o.@Ticks;
    //        //bool[] paramsRead = new bool[11];
    //        while (ThisReader.MoveToNextAttribute())
    //        {
    //            string _ReaderLocalName = ThisReader.LocalName;
    //            if (((object)_ReaderLocalName == (object)id62_Name))
    //            {
    //                o.@Name = ThisReader.Value;
    //            }
    //            else if (((object)_ReaderLocalName == (object)id1026_StartTime))
    //            {
    //                o.@StartTime = ToDateTime(ThisReader.Value);
    //            }
    //            else if (((object)_ReaderLocalName == (object)id858_EndTime))
    //            {
    //                o.@EndTime = ToDateTime(ThisReader.Value);
    //            }
    //            else if (((object)_ReaderLocalName == (object)id1027_AlignToGrid))
    //            {
    //                o.@AlignToGrid = DCXMLConvert.ToBoolean(ThisReader.Value);
    //            }
    //            else if (((object)_ReaderLocalName == (object)id303_GridLineStyle))
    //            {
    //                o.@GridLineStyle = Read43_DashStyle(ThisReader.Value);
    //            }
    //            else if (((object)_ReaderLocalName == (object)id911_GridLineColorValue))
    //            {
    //                o.@GridLineColorValue = ThisReader.Value;
    //            }
    //            else if (((object)_ReaderLocalName == (object)id144_BackColorValue))
    //            {
    //                o.@BackColorValue = ThisReader.Value;
    //            }
    //            else if (((object)_ReaderLocalName == (object)id887_SpecifyTickWidth))
    //            {
    //                o.@SpecifyTickWidth = ToSingle(ThisReader.Value);
    //            }
    //            else if (((object)_ReaderLocalName == (object)id1028_AutoTickStepSeconds))
    //            {
    //                o.@AutoTickStepSeconds = DCXMLConvert.ToInt32(ThisReader.Value);
    //            }
    //            else if (((object)_ReaderLocalName == (object)id1029_AutoTickFormatString))
    //            {
    //                o.@AutoTickFormatString = ThisReader.Value;
    //            }
    //        }
    //        ThisReader.MoveToElement();
    //        if (ThisReader.IsEmptyElement)
    //        {
    //            ThisReader.Skip();
    //            return o;
    //        }
    //        ThisReader.ReadStartElement();
    //        ThisReader.MoveToContent();
    //        int whileIterations331 = 0;
    //        int readerCount331 = ReaderCount;
    //        var _ReaderNodeType1 = ThisReader.NodeType;
    //        while (_ReaderNodeType1 != System.Xml.XmlNodeType.EndElement && _ReaderNodeType1 != System.Xml.XmlNodeType.None)
    //        {
    //            if (_ReaderNodeType1 == System.Xml.XmlNodeType.Element)
    //            {
    //                string _ReaderLocalName = ThisReader.LocalName;
    //                if (((object)_ReaderLocalName == (object)id903_Ticks))
    //                {
    //                    if (true)
    //                    {//if (!ReadNull()) {
    //                        if ((ThisReader.IsEmptyElement))
    //                        {
    //                            ThisReader.Skip();
    //                        }
    //                        else
    //                        {
    //                            if ((o.@Ticks) == null) o.@Ticks = new global::DCSoft.TemperatureChart.TickInfoList();
    //                            global::DCSoft.TemperatureChart.TickInfoList a_8_0 = o.@Ticks;
    //                            ThisReader.ReadStartElement();
    //                            ThisReader.MoveToContent();
    //                            int whileIterations332 = 0;
    //                            int readerCount332 = ReaderCount;
    //                            var _ReaderNodeType2 = ThisReader.NodeType;
    //                            while (_ReaderNodeType2 != System.Xml.XmlNodeType.EndElement && _ReaderNodeType2 != System.Xml.XmlNodeType.None)
    //                            {
    //                                if (_ReaderNodeType2 == System.Xml.XmlNodeType.Element)
    //                                {
    //                                    if (((object)ThisReader.LocalName == (object)id904_Tick))
    //                                    {
    //                                        if ((a_8_0) == null) ThisReader.Skip(); else a_8_0.Add(Read222_TickInfo(true, true));
    //                                    }
    //                                    else
    //                                    {
    //                                        UnknownNode(null, ThisReader.LocalName); //":Tick");
    //                                    }
    //                                }
    //                                else
    //                                {
    //                                    UnknownNode(null, ThisReader.LocalName); //":Tick");
    //                                }
    //                                ThisReader.MoveToContent();
    //                                CheckReaderCount(ref whileIterations332, ref readerCount332);
    //                                _ReaderNodeType2 = ThisReader.NodeType;
    //                            }
    //                            ReadEndElement();
    //                        }
    //                    }
    //                }
    //                else
    //                {
    //                    UnknownNode(o, _ReaderLocalName);//":Ticks");
    //                }
    //            }
    //            else
    //            {
    //                UnknownNode(o, ThisReader.LocalName);//":Ticks");
    //            }
    //            ThisReader.MoveToContent();
    //            CheckReaderCount(ref whileIterations331, ref readerCount331);
    //            _ReaderNodeType1 = ThisReader.NodeType;
    //        }
    //        ReadEndElement();
    //        return o;
    //    }
    //    internal protected global::DCSoft.TemperatureChart.GridYSplitInfo Read221_GridYSplitInfo(bool isNullable, bool checkType)
    //    {
    //        var ThisReader = this.Reader;
    //        global::DCSoft.TemperatureChart.GridYSplitInfo o;
    //        o = new global::DCSoft.TemperatureChart.GridYSplitInfo();
    //        //bool[] paramsRead = new bool[5];
    //        //while (ThisReader.MoveToNextAttribute()){}
    //        ThisReader.MoveToElement();
    //        if (ThisReader.IsEmptyElement)
    //        {
    //            ThisReader.Skip();
    //            return o;
    //        }
    //        ThisReader.ReadStartElement();
    //        ThisReader.MoveToContent();
    //        int whileIterations333 = 0;
    //        int readerCount333 = ReaderCount;
    //        var _ReaderNodeType1 = ThisReader.NodeType;
    //        while (_ReaderNodeType1 != System.Xml.XmlNodeType.EndElement && _ReaderNodeType1 != System.Xml.XmlNodeType.None)
    //        {
    //            if (_ReaderNodeType1 == System.Xml.XmlNodeType.Element)
    //            {
    //                string _ReaderLocalName = ThisReader.LocalName;
    //                if (((object)_ReaderLocalName == (object)id772_GridYSplitNum))
    //                {
    //                    if (ThisReader.IsEmptyElement)
    //                    {
    //                        ThisReader.Skip();
    //                    }
    //                    else
    //                    {
    //                        o.@GridYSplitNum = DCXMLConvert.ToInt32(ThisReader.ReadElementString());
    //                    }
    //                }
    //                else if (((object)_ReaderLocalName == (object)id1030_GridYSpaceNum))
    //                {
    //                    if (ThisReader.IsEmptyElement)
    //                    {
    //                        ThisReader.Skip();
    //                    }
    //                    else
    //                    {
    //                        o.@GridYSpaceNum = DCXMLConvert.ToInt32(ThisReader.ReadElementString());
    //                    }
    //                }
    //                else if (((object)_ReaderLocalName == (object)id1031_GridYSpaceNumForBottomPadding))
    //                {
    //                    if (ThisReader.IsEmptyElement)
    //                    {
    //                        ThisReader.Skip();
    //                    }
    //                    else
    //                    {
    //                        o.@GridYSpaceNumForBottomPadding = DCXMLConvert.ToInt32(ThisReader.ReadElementString());
    //                    }
    //                }
    //                else if (((object)_ReaderLocalName == (object)id1032_ThickLineWidth))
    //                {
    //                    if (ThisReader.IsEmptyElement)
    //                    {
    //                        ThisReader.Skip();
    //                    }
    //                    else
    //                    {
    //                        o.@ThickLineWidth = ToSingle(ThisReader.ReadElementString());
    //                    }
    //                }
    //                else if (((object)_ReaderLocalName == (object)id1033_ThinLineWidth))
    //                {
    //                    if (ThisReader.IsEmptyElement)
    //                    {
    //                        ThisReader.Skip();
    //                    }
    //                    else
    //                    {
    //                        o.@ThinLineWidth = ToSingle(ThisReader.ReadElementString());
    //                    }
    //                }
    //                else
    //                {
    //                    UnknownNode(o, _ReaderLocalName);//":GridYSplitNum, :GridYSpaceNum, :GridYSpaceNumForBottomPadding, :ThickLineWidth, :ThinLineWidth");
    //                }
    //            }
    //            else
    //            {
    //                UnknownNode(o, ThisReader.LocalName);//":GridYSplitNum, :GridYSpaceNum, :GridYSpaceNumForBottomPadding, :ThickLineWidth, :ThinLineWidth");
    //            }
    //            ThisReader.MoveToContent();
    //            CheckReaderCount(ref whileIterations333, ref readerCount333);
    //            _ReaderNodeType1 = ThisReader.NodeType;
    //        }
    //        ReadEndElement();
    //        return o;
    //    }

    //    internal protected global::DCSoft.TemperatureChart.DocumentPageSettings Read219_DocumentPageSettings(bool isNullable, bool checkType)
    //    {
    //        var ThisReader = this.Reader;
    //        global::DCSoft.TemperatureChart.DocumentPageSettings o;
    //        o = new global::DCSoft.TemperatureChart.DocumentPageSettings();
    //        //bool[] paramsRead = new bool[9];
    //        //while (ThisReader.MoveToNextAttribute()){}
    //        ThisReader.MoveToElement();
    //        if (ThisReader.IsEmptyElement)
    //        {
    //            ThisReader.Skip();
    //            return o;
    //        }
    //        ThisReader.ReadStartElement();
    //        ThisReader.MoveToContent();
    //        int whileIterations334 = 0;
    //        int readerCount334 = ReaderCount;
    //        var _ReaderNodeType1 = ThisReader.NodeType;
    //        while (_ReaderNodeType1 != System.Xml.XmlNodeType.EndElement && _ReaderNodeType1 != System.Xml.XmlNodeType.None)
    //        {
    //            if (_ReaderNodeType1 == System.Xml.XmlNodeType.Element)
    //            {
    //                string _ReaderLocalName = ThisReader.LocalName;
    //                if (((object)_ReaderLocalName == (object)id1035_PaperSizeName))
    //                {
    //                    {
    //                        o.@PaperSizeName = CacheString(ThisReader.ReadElementString());
    //                    }
    //                }
    //                else if (((object)_ReaderLocalName == (object)id124_PaperWidth))
    //                {
    //                    if (ThisReader.IsEmptyElement)
    //                    {
    //                        ThisReader.Skip();
    //                    }
    //                    else
    //                    {
    //                        o.@PaperWidth = DCXMLConvert.ToInt32(ThisReader.ReadElementString());
    //                    }
    //                }
    //                else if (((object)_ReaderLocalName == (object)id126_PaperHeight))
    //                {
    //                    if (ThisReader.IsEmptyElement)
    //                    {
    //                        ThisReader.Skip();
    //                    }
    //                    else
    //                    {
    //                        o.@PaperHeight = DCXMLConvert.ToInt32(ThisReader.ReadElementString());
    //                    }
    //                }
    //                else if (((object)_ReaderLocalName == (object)id127_LeftMargin))
    //                {
    //                    if (ThisReader.IsEmptyElement)
    //                    {
    //                        ThisReader.Skip();
    //                    }
    //                    else
    //                    {
    //                        o.@LeftMargin = DCXMLConvert.ToInt32(ThisReader.ReadElementString());
    //                    }
    //                }
    //                else if (((object)_ReaderLocalName == (object)id128_TopMargin))
    //                {
    //                    if (ThisReader.IsEmptyElement)
    //                    {
    //                        ThisReader.Skip();
    //                    }
    //                    else
    //                    {
    //                        o.@TopMargin = DCXMLConvert.ToInt32(ThisReader.ReadElementString());
    //                    }
    //                }
    //                else if (((object)_ReaderLocalName == (object)id129_RightMargin))
    //                {
    //                    if (ThisReader.IsEmptyElement)
    //                    {
    //                        ThisReader.Skip();
    //                    }
    //                    else
    //                    {
    //                        o.@RightMargin = DCXMLConvert.ToInt32(ThisReader.ReadElementString());
    //                    }
    //                }
    //                else if (((object)_ReaderLocalName == (object)id130_BottomMargin))
    //                {
    //                    if (ThisReader.IsEmptyElement)
    //                    {
    //                        ThisReader.Skip();
    //                    }
    //                    else
    //                    {
    //                        o.@BottomMargin = DCXMLConvert.ToInt32(ThisReader.ReadElementString());
    //                    }
    //                }
    //                else if (((object)_ReaderLocalName == (object)id131_Landscape))
    //                {
    //                    if (ThisReader.IsEmptyElement)
    //                    {
    //                        ThisReader.Skip();
    //                    }
    //                    else
    //                    {
    //                        o.@Landscape = DCXMLConvert.ToBoolean(ThisReader.ReadElementString());
    //                    }
    //                }
    //                else if (((object)_ReaderLocalName == (object)id119_AutoFitPageSize))
    //                {
    //                    if (ThisReader.IsEmptyElement)
    //                    {
    //                        ThisReader.Skip();
    //                    }
    //                    else
    //                    {
    //                        o.@AutoFitPageSize = DCXMLConvert.ToBoolean(ThisReader.ReadElementString());
    //                    }
    //                }
    //                else
    //                {
    //                    UnknownNode(o, _ReaderLocalName);//":PaperSizeName, :PaperWidth, :PaperHeight, :LeftMargin, :TopMargin, :RightMargin, :BottomMargin, :Landscape, :AutoFitPageSize");
    //                }
    //            }
    //            else
    //            {
    //                UnknownNode(o, ThisReader.LocalName);//":PaperSizeName, :PaperWidth, :PaperHeight, :LeftMargin, :TopMargin, :RightMargin, :BottomMargin, :Landscape, :AutoFitPageSize");
    //            }
    //            ThisReader.MoveToContent();
    //            CheckReaderCount(ref whileIterations334, ref readerCount334);
    //            _ReaderNodeType1 = ThisReader.NodeType;
    //        }
    //        ReadEndElement();
    //        return o;
    //    }
    //    internal protected global::DCSoft.TemperatureChart.DCTimeLineLabel Read218_DCTimeLineLabel(bool isNullable, bool checkType)
    //    {
    //        var ThisReader = this.Reader;
    //        global::DCSoft.TemperatureChart.DCTimeLineLabel o;
    //        o = new global::DCSoft.TemperatureChart.DCTimeLineLabel();
    //        //bool[] paramsRead = new bool[15];
    //        while (ThisReader.MoveToNextAttribute())
    //        {
    //            string _ReaderLocalName = ThisReader.LocalName;
    //            if (((object)_ReaderLocalName == (object)id146_Text))
    //            {
    //                o.@Text = ThisReader.Value;
    //            }
    //            else if (((object)_ReaderLocalName == (object)id1022_ParameterName))
    //            {
    //                o.@ParameterName = ThisReader.Value;
    //            }
    //            else if (((object)_ReaderLocalName == (object)id1037_MultiLine))
    //            {
    //                o.@MultiLine = DCXMLConvert.ToBoolean(ThisReader.Value);
    //            }
    //            else if (((object)_ReaderLocalName == (object)id143_ColorValue))
    //            {
    //                o.@ColorValue = ThisReader.Value;
    //            }
    //            else if (((object)_ReaderLocalName == (object)id144_BackColorValue))
    //            {
    //                o.@BackColorValue = ThisReader.Value;
    //            }
    //            else if (((object)_ReaderLocalName == (object)id469_Alignment))
    //            {
    //                o.@Alignment = Read119_StringAlignment(ThisReader.Value);
    //            }
    //            else if (((object)_ReaderLocalName == (object)id1038_LineAlignment))
    //            {
    //                o.@LineAlignment = Read119_StringAlignment(ThisReader.Value);
    //            }
    //            else if (((object)_ReaderLocalName == (object)id215_Left))
    //            {
    //                o.@Left = ToSingle(ThisReader.Value);
    //            }
    //            else if (((object)_ReaderLocalName == (object)id216_Top))
    //            {
    //                o.@Top = ToSingle(ThisReader.Value);
    //            }
    //            else if (((object)_ReaderLocalName == (object)id217_Width))
    //            {
    //                o.@Width = ToSingle(ThisReader.Value);
    //            }
    //            else if (((object)_ReaderLocalName == (object)id218_Height))
    //            {
    //                o.@Height = ToSingle(ThisReader.Value);
    //            }
    //            else if (((object)_ReaderLocalName == (object)id1039_Item))
    //            {
    //                o.@PositionFixModeForAutoHeightLine = Read217_LabelPositionFixMode(ThisReader.Value);
    //            }
    //        }
    //        ThisReader.MoveToElement();
    //        if (ThisReader.IsEmptyElement)
    //        {
    //            ThisReader.Skip();
    //            return o;
    //        }
    //        ThisReader.ReadStartElement();
    //        ThisReader.MoveToContent();
    //        int whileIterations335 = 0;
    //        int readerCount335 = ReaderCount;
    //        var _ReaderNodeType1 = ThisReader.NodeType;
    //        while (_ReaderNodeType1 != System.Xml.XmlNodeType.EndElement && _ReaderNodeType1 != System.Xml.XmlNodeType.None)
    //        {
    //            if (_ReaderNodeType1 == System.Xml.XmlNodeType.Element)
    //            {
    //                string _ReaderLocalName = ThisReader.LocalName;
    //                if (((object)_ReaderLocalName == (object)id75_Image))
    //                {
    //                    o.@Image = Read34_XImageValue(false, true);
    //                }
    //                else if (((object)_ReaderLocalName == (object)id1040_ShowBorder))
    //                {
    //                    if (ThisReader.IsEmptyElement)
    //                    {
    //                        ThisReader.Skip();
    //                    }
    //                    else
    //                    {
    //                        o.@ShowBorder = DCXMLConvert.ToBoolean(ThisReader.ReadElementString());
    //                    }
    //                }
    //                else if (((object)_ReaderLocalName == (object)id142_Font))
    //                {
    //                    o.@Font = Read64_XFontValue(false, true);
    //                }
    //                else
    //                {
    //                    UnknownNode(o, _ReaderLocalName);//":Image, :ShowBorder, :Font");
    //                }
    //            }
    //            else
    //            {
    //                UnknownNode(o, ThisReader.LocalName);//":Image, :ShowBorder, :Font");
    //            }
    //            ThisReader.MoveToContent();
    //            CheckReaderCount(ref whileIterations335, ref readerCount335);
    //            _ReaderNodeType1 = ThisReader.NodeType;
    //        }
    //        ReadEndElement();
    //        return o;
    //    }
    //    internal protected global::DCSoft.TemperatureChart.LabelPositionFixMode Read217_LabelPositionFixMode(string s)
    //    {
    //        switch (s)
    //        {
    //            case @"None": return global::DCSoft.TemperatureChart.LabelPositionFixMode.@None;
    //            case @"InsideDataGrid": return global::DCSoft.TemperatureChart.LabelPositionFixMode.@InsideDataGrid;
    //            case @"InsideAutoHeightLine": return global::DCSoft.TemperatureChart.LabelPositionFixMode.@InsideAutoHeightLine;
    //            case @"AboveAutoHeightLine": return global::DCSoft.TemperatureChart.LabelPositionFixMode.@AboveAutoHeightLine;
    //            default: return (default(global::DCSoft.TemperatureChart.LabelPositionFixMode));
    //        }
    //    }
    //    internal protected global::DCSoft.TemperatureChart.DCTimeLineImage Read216_DCTimeLineImage(bool isNullable, bool checkType)
    //    {
    //        var ThisReader = this.Reader;
    //        global::DCSoft.TemperatureChart.DCTimeLineImage o;
    //        o = new global::DCSoft.TemperatureChart.DCTimeLineImage();
    //        //bool[] paramsRead = new bool[4];
    //        while (ThisReader.MoveToNextAttribute())
    //        {
    //            string _ReaderLocalName = ThisReader.LocalName;
    //            if (((object)_ReaderLocalName == (object)id62_Name))
    //            {
    //                o.@Name = ThisReader.Value;
    //            }
    //            else if (((object)_ReaderLocalName == (object)id215_Left))
    //            {
    //                o.@Left = ToSingle(ThisReader.Value);
    //            }
    //            else if (((object)_ReaderLocalName == (object)id216_Top))
    //            {
    //                o.@Top = ToSingle(ThisReader.Value);
    //            }
    //        }
    //        ThisReader.MoveToElement();
    //        if (ThisReader.IsEmptyElement)
    //        {
    //            ThisReader.Skip();
    //            return o;
    //        }
    //        ThisReader.ReadStartElement();
    //        ThisReader.MoveToContent();
    //        int whileIterations336 = 0;
    //        int readerCount336 = ReaderCount;
    //        var _ReaderNodeType1 = ThisReader.NodeType;
    //        while (_ReaderNodeType1 != System.Xml.XmlNodeType.EndElement && _ReaderNodeType1 != System.Xml.XmlNodeType.None)
    //        {
    //            if (_ReaderNodeType1 == System.Xml.XmlNodeType.Element)
    //            {
    //                string _ReaderLocalName = ThisReader.LocalName;
    //                if (((object)_ReaderLocalName == (object)id75_Image))
    //                {
    //                    o.@Image = Read34_XImageValue(false, true);
    //                }
    //                else
    //                {
    //                    UnknownNode(o, _ReaderLocalName);//":Image");
    //                }
    //            }
    //            else
    //            {
    //                UnknownNode(o, ThisReader.LocalName);//":Image");
    //            }
    //            ThisReader.MoveToContent();
    //            CheckReaderCount(ref whileIterations336, ref readerCount336);
    //            _ReaderNodeType1 = ThisReader.NodeType;
    //        }
    //        ReadEndElement();
    //        return o;
    //    }
    //    internal protected global::DCSoft.TemperatureChart.DCTimeUnit Read215_DCTimeUnit(string s)
    //    {

    //        if (__DCSoft_TemperatureChart_DCTimeUnit == null)
    //        {
    //            lock (_NewDictionaryLockObject)
    //            {
    //                var dic20200818 = new System.Collections.Generic.Dictionary<string, DCSoft.TemperatureChart.DCTimeUnit>();
    //                dic20200818.Add("Second", DCSoft.TemperatureChart.DCTimeUnit.@Second);
    //                dic20200818.Add("Minute", DCSoft.TemperatureChart.DCTimeUnit.@Minute);
    //                dic20200818.Add("Hour", DCSoft.TemperatureChart.DCTimeUnit.@Hour);
    //                dic20200818.Add("Day", DCSoft.TemperatureChart.DCTimeUnit.@Day);
    //                dic20200818.Add("Week", DCSoft.TemperatureChart.DCTimeUnit.@Week);
    //                dic20200818.Add("Month", DCSoft.TemperatureChart.DCTimeUnit.@Month);
    //                dic20200818.Add("Year", DCSoft.TemperatureChart.DCTimeUnit.@Year);
    //                __DCSoft_TemperatureChart_DCTimeUnit = dic20200818;
    //            }
    //        }
    //        var result = default(DCSoft.TemperatureChart.DCTimeUnit);
    //        if (__DCSoft_TemperatureChart_DCTimeUnit.TryGetValue(s, out result))
    //        {
    //            return result;
    //        }
    //        else
    //        {
    //            return default(DCSoft.TemperatureChart.DCTimeUnit);
    //        }
    //    }
    //    private static System.Collections.Generic.Dictionary<string, DCSoft.TemperatureChart.DCTimeUnit> __DCSoft_TemperatureChart_DCTimeUnit = null;

    //    internal protected global::DCSoft.TemperatureChart.DCTimeLineSelectionMode Read214_DCTimeLineSelectionMode(string s)
    //    {
    //        switch (s)
    //        {
    //            case @"None": return global::DCSoft.TemperatureChart.DCTimeLineSelectionMode.@None;
    //            case @"SingleSelect": return global::DCSoft.TemperatureChart.DCTimeLineSelectionMode.@SingleSelect;
    //            case @"MultiSelec": return global::DCSoft.TemperatureChart.DCTimeLineSelectionMode.@MultiSelec;
    //            default: return (default(global::DCSoft.TemperatureChart.DCTimeLineSelectionMode));
    //        }
    //    }
    //    internal protected global::DCSoft.TemperatureChart.EditValuePointEventHandleMode Read213_EditValuePointEventHandleMode(string s)
    //    {
    //        switch (s)
    //        {
    //            case @"None": return global::DCSoft.TemperatureChart.EditValuePointEventHandleMode.@None;
    //            case @"Program": return global::DCSoft.TemperatureChart.EditValuePointEventHandleMode.@Program;
    //            case @"Silent": return global::DCSoft.TemperatureChart.EditValuePointEventHandleMode.@Silent;
    //            case @"OwnedUI": return global::DCSoft.TemperatureChart.EditValuePointEventHandleMode.@OwnedUI;
    //            default: return (default(global::DCSoft.TemperatureChart.EditValuePointEventHandleMode));
    //        }
    //    }
    //    internal protected global::DCSoft.TemperatureChart.DocumentLinkVisualStyle Read212_DocumentLinkVisualStyle(string s)
    //    {
    //        switch (s)
    //        {
    //            case @"None": return global::DCSoft.TemperatureChart.DocumentLinkVisualStyle.@None;
    //            case @"Hover": return global::DCSoft.TemperatureChart.DocumentLinkVisualStyle.@Hover;
    //            case @"Always": return global::DCSoft.TemperatureChart.DocumentLinkVisualStyle.@Always;
    //            default: return (default(global::DCSoft.TemperatureChart.DocumentLinkVisualStyle));
    //        }
    //    }


    //    string id144_BackColorValue;
    //    string id594_ContentSource;
    //    string id825_ItemBorderStyle;
    //    string id44_ContentReadonly;
    //    string id902_Zone;
    //    string id210_PaddingLeft;
    //    string id785_VerticalXLabel;
    //    string id829_InitialAngle;
    //    string id832_PieLabelStyle;
    //    string id21_DataName;
    //    string id669_SpecifyPageIndexs;
    //    string id810_MaxSize;
    //    string id396_WriteTextBindingPath;
    //    string id491_HeaderStyle;
    //    string id231_AlignToGridLine;
    //    string id18_HiddenPrintWhenEmpty;
    //    string id451_InsertEmptyPageForNewPage;
    //    string id399_ValueValidateStyle;
    //    string id959_BottomTitle;
    //    string id860_TextAlign;
    //    string id392_DataSource;
    //    string id762_DataItems;
    //    string id816_MaxMinValueStyle;
    //    string id30_Item;
    //    string id185_VerticalAlign;
    //    string id881_ShowTooltip;
    //    string id147_Repeat;
    //    string id789_XAxisCaption;
    //    string id673_SpecifyPageIndexTextList;
    //    string id826_EllipseStyle;
    //    string id983_NormalRangeDownLineStyle;
    //    string id605_StringTag;
    //    string id241_Author;
    //    string id158_Index;
    //    string id69_FileFormat;
    //    string id584_PrintGrid;
    //    string id353_XParagraphFlag;
    //    string id546_ListItems;
    //    string id791_LabelStyle;
    //    string id1057_X;
    //    string id1058_Y;
    //    string id648_X2;
    //    string id649_Y2;
    //    string id946_TitleVisible;
    //    string id607_EnableEditImageAdditionShape;
    //    string id243_CreatorIndex;
    //    string id86_Comment;
    //    string id53_PrintVisibility;
    //    string id774_GridLineStyleH;
    //    string id556_LonelyChecked;
    //    string id1016_TickStep;
    //    string id105_AutoChoosePageSize;
    //    string id660_Caption;
    //    string id929_HighlightOutofNormalRange;
    //    string id516_EnableHighlight;
    //    string id316_DocumentParameter;
    //    string id237_OldWhitespaceWidth;
    //    string id140_WatermarkInfo;
    //    string id704_TemplateDocuments;
    //    string id266_DepartmentID;
    //    string id374_XTextFooter;
    //    string id557_TextInList;
    //    string id891_PageIndexText;
    //    string id338_XTextLabelElementBase;
    //    string id162_BackgroundImage;
    //    string id728_ContactAction;
    //    string id794_ColorTheme;
    //    string id772_GridYSplitNum;
    //    string id731_PageLabelText;
    //    string id1010_EndDateKeyword;
    //    string id153_Bold;
    //    string id22_CanBeReferenced;
    //    string id876_IllegalTextEndCharForLinux;
    //    string id694_TempClassForSer20200817;
    //    string id619_CustomAdditionShapeContent;
    //    string id290_DocumentContentStyleContainer;
    //    string id305_Cursor;
    //    string id211_PaddingTop;
    //    string id559_Group;
    //    string id203_BorderTop;
    //    string id441_AutoFixFontSizeMode;
    //    string id885_DataGridBottomPadding;
    //    string id914_DateFormatStringForCrossYear;
    //    string id389_AuthorisedUserIDList;
    //    string id719_XMLViewStateBagItem;
    //    string id753_DataForSelfCheck;
    //    string id913_DateFormatString;
    //    string id259_Description;
    //    string id45_Expressions;
    //    string id490_CanSplitByPageLine;
    //    string id134_RepeatedImageValue;
    //    string id814_Color2;
    //    string id1061_DCNameValueItem;
    //    string id138_ReferenceCount;
    //    string id723_PageTexts;
    //    string id653_CheckedUserHistories;
    //    string id997_AfterOperaDaysFromZero;
    //    string id117_PaperKind;
    //    string id120_Copies;
    //    string id988_FieldNameForTime;
    //    string id254_KBEntryRangeMask;
    //    string id191_LayoutAlign;
    //    string id998_AfterOperaDaysBeginOne;
    //    string id686_PreviewImageData;
    //    string id861_Verified;
    //    string id48_CopySource;
    //    string id854_VerticalLine;
    //    string id560_CheckedTime;
    //    string id506_EventExpressions;
    //    string id678_ControlType;
    //    string id391_Enabled;
    //    string id14_EmitDataFieldName;
    //    string id795_CustomColorTheme;
    //    string id930_InputTimePrecision;
    //    string id312_PermissionLevel;
    //    string id513_BackgroundText;
    //    string id831_SliceRelativeDisplacement;
    //    string id1032_ThickLineWidth;
    //    string id37_ContentReadonlyExpression;
    //    string id106_PageIndexsForHideHeaderFooter;
    //    string id720_XMLValue;
    //    string id1034_DocumentPageSettings;
    //    string id918_Item;
    //    string id489_AllowUserToResizeHeight;
    //    string id651_LabelAtUp;
    //    string id99_HeaderFooterDifferentFirstPage;
    //    string id8_Attributes;
    //    string id971_ShadowPointVisible;
    //    string id515_BorderVisible;
    //    string id590_MinBarWidth;
    //    string id599_TopicID;
    //    string id859_LanternValue;
    //    string id714_SubEntries;
    //    string id347_XPageInfo;
    //    string id132_StrictUsePageSize;
    //    string id1022_ParameterName;
    //    string id527_SelectedIndex;
    //    string id289_ImportFooter;
    //    string id125_DesignerPaperHeight;
    //    string id999_OutofNormalRangeTextColorValue;
    //    string id1047_PartitionList;
    //    string id969_BottomPadding;
    //    string id470_HoldWholeLine;
    //    string id956_ColorValueForDownValue;
    //    string id730_LinkTextForContactAction;
    //    string id696_KBLibrary;
    //    string id767_BarOpacity;
    //    string id606_TransparentColorValue;
    //    string id317_TypeName;
    //    string id622_LocalStyle;
    //    string id711_RangeMask;
    //    string id638_ShapeEllipseElement;
    //    string id384_VBScriptItem;
    //    string id786_GridBorderLineStyle;
    //    string id968_TopPadding;
    //    string id309_UserHistoryInfo;
    //    string id190_Multiline;
    //    string id444_SlantSplitLineStyle;
    //    string id295_PrintColor;
    //    string id827_PieHeight;
    //    string id563_SpellCode3;
    //    string id722_LineSize;
    //    string id135_HorizontalResolution;
    //    string id724_PageText;
    //    string id3_StyleIndex;
    //    string id884_DataGridTopPadding;
    //    string id776_GridTextHeight;
    //    string id91_XPageSettings;
    //    string id422_DataFeedbackInfo;
    //    string id812_Background;
    //    string id682_EnableViewState;
    //    string id127_LeftMargin;
    //    string id845_VerifiedAlignment;
    //    string id416_IncludeKeywords;
    //    string id1029_AutoTickFormatString;
    //    string id113_Item;
    //    string id419_RequiredInvalidateFlag;
    //    string id631_ShapeContentStyleContainer;
    //    string id60_LocalExcludeKeywords;
    //    string id907_ForeColorValue;
    //    string id1004_LoopTextList;
    //    string id61_DisabledParameterNames;
    //    string id28_JavaScriptForDoubleClick;
    //    string id1_TemperatureDocument;
    //    string id852_IntCharSymbol;
    //    string id756_DataFieldNameForSeriesName;
    //    string id687_OptionsPropertyName;
    //    string id640_ShapeDocumentPage;
    //    string id925_YAxisInfos;
    //    string id1051_BrushStyle;
    //    string id617_PageImages;
    //    string id270_DocumentProcessState;
    //    string id339_XTDBarcodeField;
    //    string id635_ShapeRectangleElement;
    //    string id658_PrintTextForUnChecked;
    //    string id551_ValuePath;
    //    string id616_CompressSaveMode;
    //    string id291_Default;
    //    string id146_Text;
    //    string id394_BindingPath;
    //    string id232_LineWidth;
    //    string id975_Scale;
    //    string id738_ScriptTextForClick;
    //    string id253_FieldBorderElementWidth;
    //    string id1002_BlockWidth;
    //    string id350_XTextCheckBox;
    //    string id343_XFileBlock;
    //    string id445_RowSpan;
    //    string id662_CaptionAlign;
    //    string id667_CheckBoxVisible;
    //    string id54_Visible;
    //    string id889_Labels;
    //    string id514_ViewEncryptType;
    //    string id887_SpecifyTickWidth;
    //    string id717_OwnerID;
    //    string id532_CustomValueEditorTypeName;
    //    string id461_PrintPositionInPage;
    //    string id94_SwapGutter;
    //    string id684_AllowUserResize;
    //    string id797_Colors;
    //    string id239_BindingUserTrack;
    //    string id636_ShapeZoomInElement;
    //    string id410_CheckDecimalDigits;
    //    string id217_Width;
    //    string id835_EnumLableType;
    //    string id89_DocumentGraphicsUnit;
    //    string id488_Item;
    //    string id233_GridNumInOnePage;
    //    string id591_VAlign;
    //    string id926_YAxis;
    //    string id1031_GridYSpaceNumForBottomPadding;
    //    string id10_InnerID;
    //    string id78_GlobalJavaScriptReferences;
    //    string id4_PreviewTextLength;
    //    string id838_Config;
    //    string id853_LinkTarget;
    //    string id304_GridLineColor;
    //    string id440_StressBorder;
    //    string id1006_TitleAlign;
    //    string id1042_IsMultiSelect;
    //    string id358_XInputFieldBase;
    //    string id858_EndTime;
    //    string id569_NextTarget;
    //    string id521_AdornTextType;
    //    string id77_PageContentVersions;
    //    string id935_LineStyleForLanternValue;
    //    string id765_ChartDocumentStyle;
    //    string id346_XMedia;
    //    string id1063_WhiteSpaceLength;
    //    string id174_Superscript;
    //    string id624_Elements;
    //    string id71_UserHistories;
    //    string id547_InputFieldListItem;
    //    string id417_ExcludeKeywords;
    //    string id95_GutterPosition;
    //    string id562_SpellCode2;
    //    string id315_CheckedValue;
    //    string id483_Item;
    //    string id500_BackgroundTextColor;
    //    string id863_ValueTextTopPadding;
    //    string id989_FieldNameForValue;
    //    string id729_AttributeNameForContactAction;
    //    string id614_AdditionShape;
    //    string id7_EditorVersionString;
    //    string id335_XSignImage;
    //    string id228_TextInMiddlePage;
    //    string id806_EndCap;
    //    string id413_DateTimeMinValue;
    //    string id1041_DCTimeLineImage;
    //    string id27_JavaScriptForClick;
    //    string id385_ScriptMethodName;
    //    string id655_CheckboxVisibility;
    //    string id873_EnableExtMouseMoveEvent;
    //    string id116_PaperSource;
    //    string id246_BorderColor;
    //    string id136_VerticalResolution;
    //    string id284_MotherTemplateInfo;
    //    string id611_KeepWidthHeightRate;
    //    string id50_XElements;
    //    string id986_FieldNameForLink;
    //    string id964_ValueFieldName;
    //    string id976_YAxisScaleInfo;
    //    string id298_CommentIndex;
    //    string id609_ValueIndexOfRepeatedImage;
    //    string id895_PageTitlePosition;
    //    string id242_CreationTime;
    //    string id672_ChangePageIndexMidway;
    //    string id552_Items;
    //    string id865_UpAndDown;
    //    string id123_DesignerPaperWidth;
    //    string id798_ShadeCount;
    //    string id130_BottomMargin;
    //    string id74_RepeatedImages;
    //    string id985_FieldNameForID;
    //    string id741_DrawContentHandlerName;
    //    string id269_DocumentType;
    //    string id382_DescPropertyName;
    //    string id779_GridBackground;
    //    string id992_SQLText;
    //    string id109_OffsetX;
    //    string id110_OffsetY;
    //    string id204_BorderRight;
    //    string id995_BlankDateWhenNoData;
    //    string id496_BorderElementColorValue;
    //    string id25_ToolTip;
    //    string id586_Digitals;
    //    string id1005_PageTitleTexts;
    //    string id811_SymbolSize;
    //    string id429_AutoClean;
    //    string id207_MarginTop;
    //    string id455_IsCollapsed;
    //    string id965_LanternValueFieldName;
    //    string id11_ID;
    //    string id181_LetterSpacing;
    //    string id770_BarGroupSpacing;
    //    string id911_GridLineColorValue;
    //    string id561_SpellCode;
    //    string id40_ValueExpression;
    //    string id642_AutoSize;
    //    string id771_ViewStyle;
    //    string id482_DataSourceRowSpan;
    //    string id250_SubDocumentSettings;
    //    string id471_PrintBothBorderWhenJumpPrint;
    //    string id698_BaseURL;
    //    string id813_XBrushStyle;
    //    string id33_AutoHideMode;
    //    string id1044_IdentyColorARGBValue;
    //    string id1043_ElementIDForExporting;
    //    string id862_VerifiedColor;
    //    string id52_AcceptChildElementTypes2;
    //    string id517_EnableUserEditInnerValue;
    //    string id847_SpecifySymbolStyle;
    //    string id957_ColorValueForUpValue;
    //    string id188_VertialText;
    //    string id251_Readonly;
    //    string id150_XFontValue;
    //    string id1040_ShowBorder;
    //    string id454_EnableCollapse;
    //    string id282_PageContentVersionInfo;
    //    string id401_ValueName;
    //    string id273_NumOfPage;
    //    string id894_FooterDescription;
    //    string id121_HeaderDistance;
    //    string id792_RipenessRate;
    //    string id866_DCTimeLineParameter;
    //    string id342_XTextBlockElement;
    //    string id303_GridLineStyle;
    //    string id367_XTextContentElement;
    //    string id365_XTextTableRow;
    //    string id485_PrintCellBorder;
    //    string id904_Tick;
    //    string id709_KBEntry;
    //    string id180_LineSpacingStyle;
    //    string id1026_StartTime;
    //    string id187_LeftIndent;
    //    string id575_EventExpressionInfo;
    //    string id1027_AlignToGrid;
    //    string id387_DCContentLockInfo;
    //    string id937_AllowOutofRange;
    //    string id700_TemplateSourceFormatString;
    //    string id963_SymbolColorValue;
    //    string id70_BodyGridLineOffset;
    //    string id533_EnableValueEditor;
    //    string id126_PaperHeight;
    //    string id703_Entry;
    //    string id201_BorderLeft;
    //    string id598_FileContentSource;
    //    string id277_SubDocumentSpacing;
    //    string id97_ShowGutterLine;
    //    string id51_Element;
    //    string id996_Item;
    //    string id733_BarcodeType;
    //    string id898_ImagePixelHeight;
    //    string id1024_TickInfo;
    //    string id519_AutoSetSpellCodeInDropdownList;
    //    string id216_Top;
    //    string id383_IgnoreChildElements;
    //    string id1059_SynchroDataMode;
    //    string id1060_AutoSize;
    //    string id1060_EmptyWhenNoData;
    //    string id809_ChartLegendStyle;
    //    string id240_AuthorID;
    //    string id595_UpdateState;
    //    string id379_CopySourceInfo;
    //    string id869_LineWidthZoomRateWhenPrint;
    //    string id118_AutoPaperWidth;
    //    string id680_ValuePropertyName;
    //    string id755_DataSourceName;
    //    string id5_TransparentEncryptErrorMessage;
    //    string id503_TabIndex;
    //    string id24_ReferencedDataName;
    //    string id38_VisibleExpression;
    //    string id801_XPenStyle;
    //    string id221_ParagraphMultiLevel;
    //    string id1028_AutoTickStepSeconds;
    //    string id905_BigTitleFontSize;
    //    string id1015_LayoutType;
    //    string id970_ValueFont;
    //    string id602_InnerRepeatImageIndex;
    //    string id834_DownleadWidth;
    //    string id967_MaxTextDisplayLength;
    //    string id571_AutoUpdateTargetElement;
    //    string id19_DataFeedback;
    //    string id345_XTextControlHost;
    //    string id318_ValueType;
    //    string id219_PageBreakAfter;
    //    string id271_DocumentEditState;
    //    string id236_Printable;
    //    string id625_ShapeElement;
    //    string id745_SignErrorMessage;
    //    string id319_SourceColumn;
    //    string id447_DesignRowIndex;
    //    string id725_StrictMatchPageIndex;
    //    string id637_ShapePolygonElement;
    //    string id361_XBarcodeField;
    //    string id301_GridLineType;
    //    string id425_KeyFieldName;
    //    string id775_GridTextWidth;
    //    string id842_Values;
    //    string id163_Visibility;
    //    string id768_BarWidth;
    //    string id449_MirrorViewForCrossPage;
    //    string id310_Name2;
    //    string id570_NextTargetID;
    //    string id176_FixedSpacing;
    //    string id981_NormalMinValue;
    //    string id276_HeightInPrintJob;
    //    string id395_BindingPathForText;
    //    string id227_DocumentTerminalTextInfo;
    //    string id141_Type;
    //    string id206_MarginLeft;
    //    string id314_Tag;
    //    string id924_FooterLines;
    //    string id973_AbNormalRangeSettings;
    //    string id368_XTextSection;
    //    string id565_LinkListBindingInfo;
    //    string id235_LineStyle;
    //    string id740_ChartPageIndex;
    //    string id460_PrintedPageIndex;
    //    string id697_TemplateFileSystemName;
    //    string id523_EditorControlTypeName;
    //    string id452_ExpendForDataBinding;
    //    string id157_PageBorderBackgroundStyle;
    //    string id823_Item;
    //    string id701_TemplateFileFormat;
    //    string id939_ClickToHide;
    //    string id381_SourcePropertyName;
    //    string id920_HeaderLabels;
    //    string id879_HeaderLabelLineAlignment;
    //    string id524_LinkListBinding;
    //    string id670_AutoHeight;
    //    string id65_Parameter;
    //    string id67_InnerRepeatImageDataList;
    //    string id941_EnableLanternValue;
    //    string id934_LanternValueColorForDownValue;
    //    string id643_Points;
    //    string id993_TitleLineInfo;
    //    string id708_Content;
    //    string id874_EnableDataGridLinearAxisMode;
    //    string id890_Label;
    //    string id154_Italic;
    //    string id1048_XImagePartition;
    //    string id732_BarcodeStyle2;
    //    string id183_RTFLineSpacing;
    //    string id710_CopyListItems;
    //    string id764_ChartStyle;
    //    string id324_XPageBreak;
    //    string id351_XImage;
    //    string id549_SourceName;
    //    string id75_Image;
    //    string id41_UserFlags;
    //    string id333_XPie;
    //    string id961_HiddenValueTitleBackColorValue;
    //    string id950_RedLineValue;
    //    string id307_Link;
    //    string id566_ProviderName;
    //    string id948_ValueFormatString;
    //    string id377_GridLine;
    //    string id966_TimeFieldName;
    //    string id715_ListItemsSource;
    //    string id782_GridLineOffsetX;
    //    string id302_GridLineOffsetY;
    //    string id255_IsTemplate;
    //    string id695_KB;
    //    string id721_LineLengthInCM;
    //    string id705_Document;
    //    string id431_CommandText;
    //    string id322_SerializeValue;
    //    string id1037_MultiLine;
    //    string id267_DepartmentName;
    //    string id676_SpecifyPageIndex;
    //    string id415_RegExpression;
    //    string id917_Item;
    //    string id321_ValueTypeFullName;
    //    string id804_LineJoin;
    //    string id915_DateFormatStringForCrossMonth;
    //    string id629_EditMode;
    //    string id1017_TickLineVisible;
    //    string id478_Columns;
    //    string id458_DocumentID;
    //    string id297_LayoutDirection;
    //    string id13_TransparentEncryptMode;
    //    string id151_Size;
    //    string id870_LinkVisualStyle;
    //    string id363_XDirectoryField;
    //    string id87_MeasureMode;
    //    string id424_FieldName;
    //    string id892_SpecifyStartDate;
    //    string id328_XLineBreak;
    //    string id1053_PointF;
    //    string id481_GroupName;
    //    string id539_MultiColumn;
    //    string id758_DataFieldNameForText;
    //    string id896_ShowIcon;
    //    string id621_ShapeDocument;
    //    string id900_GridYSplitInfo;
    //    string id43_ContentLock;
    //    string id79_Reference;
    //    string id875_ExtendDaysForTimeLine;
    //    string id20_MaxInputLength;
    //    string id397_ProcessState;
    //    string id912_GridBackColorValue;
    //    string id972_RedLinePrintVisible;
    //    string id464_ContentLoaded;
    //    string id370_XTextTableCell;
    //    string id355_XField;
    //    string id487_AllowInsertRowDownUseHotKey;
    //    string id373_XTextHeaderForFirstPage;
    //    string id778_GridValueFormat;
    //    string id215_Left;
    //    string id927_YAxisInfo;
    //    string id632_ShapeLinesElement;
    //    string id435_DisplayFormat;
    //    string id1050_XPartition;
    //    string id982_NormalRangeUpLineStyle;
    //    string id329_XTextObjectElement;
    //    string id530_LastSelectedListItems;
    //    string id734_ErroeCorrectionLevel;
    //    string id857_Time;
    //    string id783_VerticalTextAlign;
    //    string id336_XCustomShape;
    //    string id945_TitleValueDispalyFormat;
    //    string id612_Source;
    //    string id601_AutoCreate;
    //    string id707_KBID;
    //    string id769_BarSpacing;
    //    string id675_RawPageIndex;
    //    string id763_DataItem;
    //    string id212_PaddingRight;
    //    string id685_SavePreviewImage;
    //    string id103_DocumentGridLine;
    //    string id84_BodyText;
    //    string id592_EditValueInDialog;
    //    string id62_Name;
    //    string id331_XPartitionImage;
    //    string id1049_Url;
    //    string id208_MarginRight;
    //    string id851_IntCharLantern;
    //    string id1025_TimeLineZoneInfo;
    //    string id936_SpecifyTitleWidth;
    //    string id504_SpecifyWidth;
    //    string id279_Locked;
    //    string id1060_SmartChartMode;
    //    string id172_UnderlineStyle;
    //    string id664_ControlStyle;
    //    string id564_ListIndex;
    //    string id991_FieldNameForText;
    //    string id693_Temp20200817;
    //    string id958_SymbolStyle;
    //    string id430_ConnectionName;
    //    string id529_EnableLastSelectedListItems;
    //    string id175_Subscript;
    //    string id1014_ShowBackColor;
    //    string id450_CloneType;
    //    string id665_Checked;
    //    string id380_SourceID;
    //    string id285_FileSystemName;
    //    string id537_GetValueOrderByTime;
    //    string id674_SpecifyPageIndexInfo;
    //    string id112_Watermark;
    //    string id588_TextAlignment;
    //    string id752_LastVerifyResult;
    //    string id683_ViewState;
    //    string id256_MRID;
    //    string id868_BothBlackWhenPrint;
    //    string id390_XDataBinding;
    //    string id39_PrintVisibilityExpression;
    //    string id376_XTextBody;
    //    string id634_ShapeWireLabelElement;
    //    string id727_ReferencedTopicID;
    //    string id620_PageImageInfo;
    //    string id641_ShapeDocumentImagePage;
    //    string id306_DeleterIndex;
    //    string id139_ValueIndex;
    //    string id692_FileContentType;
    //    string id799_LightCorrectionFactor;
    //    string id234_GridSpanInCM;
    //    string id178_SpacingBeforeParagraph;
    //    string id129_RightMargin;
    //    string id626_LocalElementStyleMode;
    //    string id796_ThemeType;
    //    string id509_UserEditable;
    //    string id498_BackgroundTextItalic;
    //    string id947_YSplitNum;
    //    string id114_Item;
    //    string id659_CheckAlignLeft;
    //    string id354_XTextContainerElement;
    //    string id145_Alpha;
    //    string id296_PrintBackColor;
    //    string id580_DisplayLevel;
    //    string id520_DefaultValueType;
    //    string id583_ShowGrid;
    //    string id406_CheckMaxValue;
    //    string id840_Data;
    //    string id6_DataEncryptProviderName;
    //    string id108_ForPOSPrinter;
    //    string id88_LocalConfig;
    //    string id104_TerminalText;
    //    string id921_NumOfDaysInOnePage;
    //    string id330_XNewMedicalExpression;
    //    string id472_AllowUserDeleteRow;
    //    string id897_ImagePixelWidth;
    //    string id749_UseInnerSignProvider;
    //    string id32_ValidateStyle;
    //    string id92_JointPrintNumber;
    //    string id499_LableUnitTextBold;
    //    string id257_TimeoutHours;
    //    string id790_LegendStyle;
    //    string id428_DCEmitDataSource;
    //    string id657_PrintTextForChecked;
    //    string id954_ShowPointValue;
    //    string id938_SeparatorLineVisible;
    //    string id681_HostMode;
    //    string id689_LoopPlay;
    //    string id456_CompressOwnerLineSpacing;
    //    string id880_SelectionMode;
    //    string id476_AllowUserToResizeRows;
    //    string id258_Version;
    //    string id465_NumOfRows;
    //    string id388_OwnerUserID;
    //    string id93_PrintZoomRate;
    //    string id849_SymbolOffsetY;
    //    string id848_SymbolOffsetX;
    //    string id173_UnderlineColor;
    //    string id940_ValueVisible;
    //    string id1007_ValueAlign;
    //    string id323_XTextLock;
    //    string id497_TextColor;
    //    string id308_ContentStyle;
    //    string id1023_SeperatorChar;
    //    string id718_ObjectParameter;
    //    string id443_MoveFocusHotKey;
    //    string id356_XBean;
    //    string id82_DocumentContentVersion;
    //    string id73_ContentStyles;
    //    string id581_ShowPageIndex;
    //    string id736_ImageForDown;
    //    string id867_TemperatureDocumentConfig;
    //    string id856_UseAdvVerticalStyle2;
    //    string id403_BinaryLength;
    //    string id507_UnitText;
    //    string id200_BorderWidth;
    //    string id119_AutoFitPageSize;
    //    string id143_ColorValue;
    //    string id923_Line;
    //    string id459_Printed;
    //    string id650_LabelAtLeft;
    //    string id541_MultiSelect;
    //    string id668_VisualStyle;
    //    string id748_SignTime;
    //    string id953_ShowLegendInRule;
    //    string id57_ScriptText;
    //    string id412_DateTimeMaxValue;
    //    string id501_BorderTextPosition;
    //    string id746_DefaultSignMode;
    //    string id171_EmphasisMark;
    //    string id492_EndingLineBreak;
    //    string id469_Alignment;
    //    string id404_MaxLength;
    //    string id805_StartCap;
    //    string id819_ChartDataItem;
    //    string id49_EventTemplateName;
    //    string id597_SaveLinkedContent;
    //    string id448_DesignColIndex;
    //    string id759_DataFieldNameForValue;
    //    string id671_PageIndexFix;
    //    string id229_MinHeightInCMUnit;
    //    string id83_Info;
    //    string id260_LicenseText;
    //    string id59_JavaScriptTextForWebClient;
    //    string id1055_IsCustomFill;
    //    string id903_Ticks;
    //    string id275_StartPositionInPringJob;
    //    string id334_XChart;
    //    string id408_MaxValue;
    //    string id666_DefaultCheckedForValueBinding;
    //    string id47_ScriptItems;
    //    string id194_CharacterCircle;
    //    string id544_ListValueFormatString;
    //    string id299_ProtectType;
    //    string id538_EditStyle;
    //    string id446_ColSpan;
    //    string id58_ScriptLanguage;
    //    string id268_DocumentFormat;
    //    string id952_ValueTextBackColorValue;
    //    string id1035_PaperSizeName;
    //    string id909_BigVerticalGridLineColorValue;
    //    string id263_LastPrintTime;
    //    string id36_ElementIDForEditableDependent;
    //    string id362_XAccountingNumber;
    //    string id280_NewPage;
    //    string id349_XTextRadioBox;
    //    string id202_BorderBottom;
    //    string id615_AdditionShapeFixSize;
    //    string id167_BackgroundRepeat;
    //    string id398_AutoUpdate;
    //    string id124_PaperWidth;
    //    string id386_DomExpression;
    //    string id864_CustomImage;
    //    string id652_HyperlinkInfo;
    //    string id750_SignRange;
    //    string id230_DCGridLineInfo;
    //    string id822_SymbolType;
    //    string id679_TypeFullName;
    //    string id214_Zoom;
    //    string id337_XTextButton;
    //    string id582_AutoExitEditMode;
    //    string id411_MaxDecimalDigits;
    //    string id100_SwapLeftRightMargin;
    //    string id161_BackgroundStyle;
    //    string id645_SmoothZoomIn;
    //    string id111_PrinterName;
    //    string id76_MotherTemplate;
    //    string id542_DynamicListItems;
    //    string id248_Value;
    //    string id855_UseAdvVerticalStyle;
    //    string id1046_DivCharForMultiMode;
    //    string id72_History;
    //    string id808_ChartLabelStyle;
    //    string id531_FieldSettings;
    //    string id222_ParagraphOutlineLevel;
    //    string id213_PaddingBottom;
    //    string id261_LastModifiedTime;
    //    string id128_TopMargin;
    //    string id788_ChartCaptionStyle;
    //    string id572_AutoSetFirstItems;
    //    string id493_StartBorderText;
    //    string id477_ShowCellNoneBorder;
    //    string id169_FontName;
    //    string id300_TitleLevel;
    //    string id262_EditMinute;
    //    string id475_AllowUserToResizeColumns;
    //    string id654_Requried;
    //    string id577_Target;
    //    string id526_EditorActiveMode;
    //    string id189_RightToLeft;
    //    string id1019_UpAndDownTextType;
    //    string id933_LanternValueColorForUpValue;
    //    string id978_NormalRangeBackColorValue;
    //    string id803_DashCap;
    //    string id777_GroupGridLine;
    //    string id737_ImageForMouseOver;
    //    string id133_XImageValue;
    //    string id984_ValuePointDataSourceInfo;
    //    string id888_Images;
    //    string id841_DocumentData;
    //    string id34_ValueBinding;
    //    string id473_AllowUserInsertRow;
    //    string id142_Font;
    //    string id567_UserFlag;
    //    string id977_ScaleRate;
    //    string id608_EnableRepeatedImage;
    //    string id883_TickUnit;
    //    string id540_RepulsionForGroup;
    //    string id249_DocumentInfo;
    //    string id192_RoundRadio;
    //    string id198_BorderBottomColor;
    //    string id787_YAxisCaptions;
    //    string id702_KBEntries;
    //    string id744_SignMessage;
    //    string id878_EnableCustomValuePointSymbol;
    //    string id218_Height;
    //    string id960_TitleBackColorValue;
    //    string id402_Required;
    //    string id274_UseLanguage2;
    //    string id1030_GridYSpaceNum;
    //    string id846_TagValue;
    //    string id107_PageBorderBackground;
    //    string id807_MiterLimit;
    //    string id12_EncryptContent;
    //    string id843_ValuePoint;
    //    string id155_Underline;
    //    string id522_CustomAdornText;
    //    string id484_GenerateByValueBingding;
    //    string id238_Title;
    //    string id644_ZoomInRate;
    //    string id281_BorderColorValue;
    //    string id164_BackgroundPosition;
    //    string id1038_LineAlignment;
    //    string id747_SignProviderName;
    //    string id427_KeyFeildDataSourcePath;
    //    string id149_Angle;
    //    string id364_XInputField;
    //    string id910_PageBackColorValue;
    //    string id196_BorderTopColor;
    //    string id573_ValueFormater;
    //    string id46_Expression;
    //    string id699_ListItemsSourceFormatString;
    //    string id278_AllowSave;
    //    string id56_WebClientHtmlText;
    //    string id193_Rotate;
    //    string id1012_StartDateKeyword;
    //    string id962_TitleColorValue;
    //    string id348_XTextCheckBoxElementBase;
    //    string id757_DataFieldNameForGroupName;
    //    string id131_Landscape;
    //    string id205_BorderSpacing;
    //    string id29_PropertyExpressions;
    //    string id1054_IsSelect;
    //    string id9_Attribute;
    //    string id182_LineSpacing;
    //    string id1018_ForceUpWhenPageFirstPoint;
    //    string id199_BorderStyle;
    //    string id495_BorderElementColor;
    //    string id166_BackgroundPositionY;
    //    string id165_BackgroundPositionX;
    //    string id833_DownleadLength;
    //    string id453_ForeColorValueForCollapsed;
    //    string id357_XContentLinkField;
    //    string id293_Style;
    //    string id1001_EnableEndTime;
    //    string id292_Styles;
    //    string id893_SpecifyEndDate;
    //    string id433_FieldsForDesign;
    //    string id1020_ValueTextMultiLine;
    //    string id534_ShowFormButton;
    //    string id220_PageBreakBefore;
    //    string id754_ImageData;
    //    string id987_FieldNameForTitle;
    //    string id23_BringoutToSave;
    //    string id943_HollowCovertTargetName;
    //    string id42_EnablePermission;
    //    string id751_SignUserID;
    //    string id525_EnableFieldTextColor;
    //    string id604_LinkInfo;
    //    string id836_PieDataItem;
    //    string id510_SelectedSpellCode;
    //    string id423_TableName;
    //    string id223_VisibleInDirectory;
    //    string id596_ReplaceUpdateMode;
    //    string id548_ListSourceInfo;
    //    string id824_PieDocumentStyle;
    //    string id850_SpecifyLanternSymbolStyle;
    //    string id593_ExpressionStyle;
    //    string id122_FooterDistance;
    //    string id409_MinValue;
    //    string id462_ImportUserTrack;
    //    string id663_AutoHeightForMultiline;
    //    string id980_NormalMaxValue;
    //    string id438_TabStop;
    //    string id287_ImportPageSettings;
    //    string id341_XTextLabelElement;
    //    string id955_ColorValueForPointValue;
    //    string id800_XColorValue;
    //    string id554_ListItem;
    //    string id327_XBookMark;
    //    string id739_CommandName;
    //    string id170_FontSize;
    //    string id96_GutterStyle;
    //    string id550_DisplayPath;
    //    string id405_MinLength;
    //    string id177_SpacingAfterParagraph;
    //    string id545_ListValueSeparatorChar;
    //    string id1003_ValueDisplayFormat;
    //    string id899_ShadowPointDetectSeconds;
    //    string id432_ParameterStyle;
    //    string id928_MergeIntoLeft;
    //    string id325_XTextTableColumn;
    //    string id195_BorderLeftColor;
    //    string id974_Scales;
    //    string id90_PageSettings;
    //    string id101_SpecifyDuplex;
    //    string id148_DensityForRepeat;
    //    string id372_XTextFooterForFirstPage;
    //    string id639_ShapeContainerElement;
    //    string id837_TemperatureDocument;
    //    string id690_EnableMediaContextMenu;
    //    string id742_SignUserName;
    //    string id568_IsRoot;
    //    string id511_InnerValue;
    //    string id288_ImportHeader;
    //    string id951_RedLineWidth;
    //    string id420_PropertyExpressionInfo;
    //    string id512_PrintBackgroundText;
    //    string id1052_RatioToPointFsList;
    //    string id463_DelayLoadWhenExpand;
    //    string id115_EditTimeBackgroundImage;
    //    string id426_KeyFieldValue;
    //    string id781_Thickness;
    //    string id872_EditValuePointMode;
    //    string id378_SpecifyFixedLineHeight;
    //    string id844_VerifiedColorValue;
    //    string id17_LimitedInputChars;
    //    string id1008_MaxValueForDayIndex;
    //    string id340_NewBarcode;
    //    string id579_TargetPropertyName;
    //    string id508_LabelText;
    //    string id15_EmitDataSource;
    //    string id55_Deleteable;
    //    string id828_PieOpacity;
    //    string id418_CustomMessage;
    //    string id656_PrintVisibilityWhenUnchecked;
    //    string id802_DashStyle;
    //    string id466_NumOfColumns;
    //    string id613_SaveContentInFile;
    //    string id436_DCEmitDataSourceFieldInfo;
    //    string id1062_WhitespaceCount;
    //    string id871_DebugMode;
    //    string id1056_ImgBase64ForCustomFill;
    //    string id468_DataForReValueBinding;
    //    string id906_PageIndexFont;
    //    string id98_EnableHeaderFooter;
    //    string id760_DataFieldNameForLink;
    //    string id942_LanternValueTitle;
    //    string id691_PlayerUIMode;
    //    string id64_Parameters;
    //    string id817_GridStep;
    //    string id574_NoneText;
    //    string id137_ImageDataBase64String;
    //    string id66_DetectRepeatImageForSave;
    //    string id990_FieldNameForLanternValue;
    //    string id677_DelayLoadControl;
    //    string id264_AuthorName;
    //    string id578_CustomTargetName;
    //    string id1011_StartDate;
    //    string id1013_PreserveStartKeywordOrder;
    //    string id407_CheckMinValue;
    //    string id320_DefaultValue;
    //    string id332_XTemperatureChart;
    //    string id558_Text2;
    //    string id882_AllowUserCollapseZone;
    //    string id931_ValuePrecision;
    //    string id630_DefaultFont;
    //    string id821_TipText;
    //    string id589_ShowText;
    //    string id1021_HeaderLabelInfo;
    //    string id815_LeftSide;
    //    string id2_Item;
    //    string id393_FormatString;
    //    string id414_Range;
    //    string id226_BorderRange;
    //    string id536_InputFieldSettings;
    //    string id265_AuthorPermissionLevel;
    //    string id908_BigVerticalGridLineWidth;
    //    string id712_ParentID;
    //    string id252_ShowHeaderBottomLine;
    //    string id901_Zones;
    //    string id224_ParagraphListStyle;
    //    string id160_BackgroundColor2;
    //    string id437_EnabledTransprentEncrypt;
    //    string id535_FormButtonStyle;
    //    string id85_Comments;
    //    string id633_ShapeLineElement;
    //    string id587_BarcodeStyle;
    //    string id618_SmoothZoom;
    //    string id949_AlertLineColorValue;
    //    string id994_VisibleWhenNoValuePoint;
    //    string id168_Color;
    //    string id467_AllowReBindingDataSource;
    //    string id245_ForeColor;
    //    string id359_XTextShapeInputFieldElement;
    //    string id661_CaptionFlowLayout;
    //    string id623_Resizeable;
    //    string id713_EntryTemplateContent;
    //    string id766_TextColorValue;
    //    string id366_XTextTable;
    //    string id474_Item;
    //    string id1039_Item;
    //    string id726_Item;
    //    string id439_BorderPrintedWhenJumpPrint;
    //    string id1036_DCTimeLineLabel;
    //    string id311_SavedTime;
    //    string id486_PrintCellBackground;
    //    string id479_SubfieldMode;
    //    string id944_ShadowName;
    //    string id244_BackColor;
    //    string id1000_ExtendGridLineType;
    //    string id247_XAttribute;
    //    string id480_SubfieldNumber;
    //    string id553_BufferItems;
    //    string id922_HeaderLines;
    //    string id400_Level;
    //    string id932_AllowInterrupt;
    //    string id421_AllowChainReaction;
    //    string id184_Align;
    //    string id743_SignClientName;
    //    string id360_XMedicalExpressionField;
    //    string id156_Strikeout;
    //    string id179_LayoutGridHeight;
    //    string id761_DataFieldNameForTipText;
    //    string id610_Alt;
    //    string id780_AxisCompress;
    //    string id457_SpecifyHeight;
    //    string id283_PageIndex;
    //    string id735_PrintAsText;
    //    string id628_AutoZoomFontSize;
    //    string id294_DocumentContentStyle;
    //    string id979_OutofNormalRangeBackColorValue;
    //    string id555_EntryID;
    //    string id102_PowerDocumentGridLine;
    //    string id716_OwnerLevel;
    //    string id877_TitleForToolTip;
    //    string id313_ClientName;
    //    string id26_AcceptTab;
    //    string id375_XTextHeader;
    //    string id371_XTextDocumentContentElement;
    //    string id886_SQLTextForHeaderLabel;
    //    string id1009_CircleText;
    //    string id434_Field;
    //    string id369_XTextSubDocument;
    //    string id16_AutoFixTextMode;
    //    string id627_TextBackColorString;
    //    string id31_EnableValueValidate;
    //    string id505_DefaultEventExpression;
    //    string id646_X1;
    //    string id647_Y1;
    //    string id543_ListSource;
    //    string id784_HorizontalTextAlign;
    //    string id502_FastInputMode;
    //    string id35_DefaultValueForValueBinding;
    //    string id528_DefaultSelectedIndexs;
    //    string id152_Unit;
    //    string id159_BackgroundColor;
    //    string id1045_IsIdentyPartition;
    //    string id344_HorizontalLine;
    //    string id352_XTextEOFElement;
    //    string id1033_ThinLineWidth;
    //    string id919_SpecifyTitleHeight;
    //    string id830_DrawingStyle;
    //    string id603_ZOrderStyle;
    //    string id286_Format;
    //    string id68_FileName;
    //    string id442_AutoFixFontSize;
    //    string id81_SpecialTag;
    //    string id916_DateFormatStringForCrossWeek;
    //    string id63_SerializeParameterValue;
    //    string id773_CustomColorThemeH;
    //    string id706_KBTemplateDocument;
    //    string id186_FirstLineIndent;
    //    string id818_TickTextList;
    //    string id793_BarBorderPen;
    //    string id600_ResetListIndexFlag;
    //    string id820_SeriesName;
    //    string id518_ShowInputFieldStateTag;
    //    string id225_DefaultValuePropertyNames;
    //    string id272_Operator;
    //    string id494_EndBorderText;
    //    string id585_UnitMode;
    //    string id326_XString;
    //    string id576_EventName;
    //    string id688_CsMediaPlayer;
    //    string id80_GlobalJavaScript;
    //    string id197_BorderRightColor;
    //    string id839_Datas;
    //    string id209_MarginBottom;
    //    protected override void InitIDs()
    //    {
    //        var myNameTable = this.Reader.NameTable;
    //        id144_BackColorValue = myNameTable.Add(@"BackColorValue");
    //        id594_ContentSource = myNameTable.Add(@"ContentSource");
    //        id825_ItemBorderStyle = myNameTable.Add(@"ItemBorderStyle");
    //        id44_ContentReadonly = myNameTable.Add(@"ContentReadonly");
    //        id902_Zone = myNameTable.Add(@"Zone");
    //        id210_PaddingLeft = myNameTable.Add(@"PaddingLeft");
    //        id785_VerticalXLabel = myNameTable.Add(@"VerticalXLabel");
    //        id829_InitialAngle = myNameTable.Add(@"InitialAngle");
    //        id832_PieLabelStyle = myNameTable.Add(@"PieLabelStyle");
    //        id21_DataName = myNameTable.Add(@"DataName");
    //        id669_SpecifyPageIndexs = myNameTable.Add(@"SpecifyPageIndexs");
    //        id810_MaxSize = myNameTable.Add(@"MaxSize");
    //        id396_WriteTextBindingPath = myNameTable.Add(@"WriteTextBindingPath");
    //        id491_HeaderStyle = myNameTable.Add(@"HeaderStyle");
    //        id231_AlignToGridLine = myNameTable.Add(@"AlignToGridLine");
    //        id18_HiddenPrintWhenEmpty = myNameTable.Add(@"HiddenPrintWhenEmpty");
    //        id451_InsertEmptyPageForNewPage = myNameTable.Add(@"InsertEmptyPageForNewPage");
    //        id399_ValueValidateStyle = myNameTable.Add(@"ValueValidateStyle");
    //        id959_BottomTitle = myNameTable.Add(@"BottomTitle");
    //        id860_TextAlign = myNameTable.Add(@"TextAlign");
    //        id392_DataSource = myNameTable.Add(@"DataSource");
    //        id762_DataItems = myNameTable.Add(@"DataItems");
    //        id816_MaxMinValueStyle = myNameTable.Add(@"MaxMinValueStyle");
    //        id30_Item = myNameTable.Add(@"Item");
    //        id185_VerticalAlign = myNameTable.Add(@"VerticalAlign");
    //        id881_ShowTooltip = myNameTable.Add(@"ShowTooltip");
    //        id147_Repeat = myNameTable.Add(@"Repeat");
    //        id789_XAxisCaption = myNameTable.Add(@"XAxisCaption");
    //        id673_SpecifyPageIndexTextList = myNameTable.Add(@"SpecifyPageIndexTextList");
    //        id826_EllipseStyle = myNameTable.Add(@"EllipseStyle");
    //        id983_NormalRangeDownLineStyle = myNameTable.Add(@"NormalRangeDownLineStyle");
    //        id605_StringTag = myNameTable.Add(@"StringTag");
    //        id241_Author = myNameTable.Add(@"Author");
    //        id158_Index = myNameTable.Add(@"Index");
    //        id69_FileFormat = myNameTable.Add(@"FileFormat");
    //        id584_PrintGrid = myNameTable.Add(@"PrintGrid");
    //        id353_XParagraphFlag = myNameTable.Add(@"XParagraphFlag");
    //        id546_ListItems = myNameTable.Add(@"ListItems");
    //        id791_LabelStyle = myNameTable.Add(@"LabelStyle");
    //        id1057_X = myNameTable.Add(@"X");
    //        id1058_Y = myNameTable.Add(@"Y");
    //        id648_X2 = myNameTable.Add(@"X2");
    //        id649_Y2 = myNameTable.Add(@"Y2");
    //        id946_TitleVisible = myNameTable.Add(@"TitleVisible");
    //        id607_EnableEditImageAdditionShape = myNameTable.Add(@"EnableEditImageAdditionShape");
    //        id243_CreatorIndex = myNameTable.Add(@"CreatorIndex");
    //        id86_Comment = myNameTable.Add(@"Comment");
    //        id53_PrintVisibility = myNameTable.Add(@"PrintVisibility");
    //        id774_GridLineStyleH = myNameTable.Add(@"GridLineStyleH");
    //        id556_LonelyChecked = myNameTable.Add(@"LonelyChecked");
    //        id1016_TickStep = myNameTable.Add(@"TickStep");
    //        id105_AutoChoosePageSize = myNameTable.Add(@"AutoChoosePageSize");
    //        id660_Caption = myNameTable.Add(@"Caption");
    //        id929_HighlightOutofNormalRange = myNameTable.Add(@"HighlightOutofNormalRange");
    //        id516_EnableHighlight = myNameTable.Add(@"EnableHighlight");
    //        id316_DocumentParameter = myNameTable.Add(@"DocumentParameter");
    //        id237_OldWhitespaceWidth = myNameTable.Add(@"OldWhitespaceWidth");
    //        id140_WatermarkInfo = myNameTable.Add(@"WatermarkInfo");
    //        id704_TemplateDocuments = myNameTable.Add(@"TemplateDocuments");
    //        id266_DepartmentID = myNameTable.Add(@"DepartmentID");
    //        id374_XTextFooter = myNameTable.Add(@"XTextFooter");
    //        id557_TextInList = myNameTable.Add(@"TextInList");
    //        id891_PageIndexText = myNameTable.Add(@"PageIndexText");
    //        id338_XTextLabelElementBase = myNameTable.Add(@"XTextLabelElementBase");
    //        id162_BackgroundImage = myNameTable.Add(@"BackgroundImage");
    //        id728_ContactAction = myNameTable.Add(@"ContactAction");
    //        id794_ColorTheme = myNameTable.Add(@"ColorTheme");
    //        id772_GridYSplitNum = myNameTable.Add(@"GridYSplitNum");
    //        id731_PageLabelText = myNameTable.Add(@"PageLabelText");
    //        id1010_EndDateKeyword = myNameTable.Add(@"EndDateKeyword");
    //        id153_Bold = myNameTable.Add(@"Bold");
    //        id22_CanBeReferenced = myNameTable.Add(@"CanBeReferenced");
    //        id876_IllegalTextEndCharForLinux = myNameTable.Add(@"IllegalTextEndCharForLinux");
    //        id694_TempClassForSer20200817 = myNameTable.Add(@"TempClassForSer20200817");
    //        id619_CustomAdditionShapeContent = myNameTable.Add(@"CustomAdditionShapeContent");
    //        id290_DocumentContentStyleContainer = myNameTable.Add(@"DocumentContentStyleContainer");
    //        id305_Cursor = myNameTable.Add(@"Cursor");
    //        id211_PaddingTop = myNameTable.Add(@"PaddingTop");
    //        id559_Group = myNameTable.Add(@"Group");
    //        id203_BorderTop = myNameTable.Add(@"BorderTop");
    //        id441_AutoFixFontSizeMode = myNameTable.Add(@"AutoFixFontSizeMode");
    //        id885_DataGridBottomPadding = myNameTable.Add(@"DataGridBottomPadding");
    //        id914_DateFormatStringForCrossYear = myNameTable.Add(@"DateFormatStringForCrossYear");
    //        id389_AuthorisedUserIDList = myNameTable.Add(@"AuthorisedUserIDList");
    //        id719_XMLViewStateBagItem = myNameTable.Add(@"XMLViewStateBagItem");
    //        id753_DataForSelfCheck = myNameTable.Add(@"DataForSelfCheck");
    //        id913_DateFormatString = myNameTable.Add(@"DateFormatString");
    //        id259_Description = myNameTable.Add(@"Description");
    //        id45_Expressions = myNameTable.Add(@"Expressions");
    //        id490_CanSplitByPageLine = myNameTable.Add(@"CanSplitByPageLine");
    //        id134_RepeatedImageValue = myNameTable.Add(@"RepeatedImageValue");
    //        id814_Color2 = myNameTable.Add(@"Color2");
    //        id1061_DCNameValueItem = myNameTable.Add(@"DCNameValueItem");
    //        id138_ReferenceCount = myNameTable.Add(@"ReferenceCount");
    //        id723_PageTexts = myNameTable.Add(@"PageTexts");
    //        id653_CheckedUserHistories = myNameTable.Add(@"CheckedUserHistories");
    //        id997_AfterOperaDaysFromZero = myNameTable.Add(@"AfterOperaDaysFromZero");
    //        id117_PaperKind = myNameTable.Add(@"PaperKind");
    //        id120_Copies = myNameTable.Add(@"Copies");
    //        id988_FieldNameForTime = myNameTable.Add(@"FieldNameForTime");
    //        id254_KBEntryRangeMask = myNameTable.Add(@"KBEntryRangeMask");
    //        id191_LayoutAlign = myNameTable.Add(@"LayoutAlign");
    //        id998_AfterOperaDaysBeginOne = myNameTable.Add(@"AfterOperaDaysBeginOne");
    //        id686_PreviewImageData = myNameTable.Add(@"PreviewImageData");
    //        id861_Verified = myNameTable.Add(@"Verified");
    //        id48_CopySource = myNameTable.Add(@"CopySource");
    //        id854_VerticalLine = myNameTable.Add(@"VerticalLine");
    //        id560_CheckedTime = myNameTable.Add(@"CheckedTime");
    //        id506_EventExpressions = myNameTable.Add(@"EventExpressions");
    //        id678_ControlType = myNameTable.Add(@"ControlType");
    //        id391_Enabled = myNameTable.Add(@"Enabled");
    //        id14_EmitDataFieldName = myNameTable.Add(@"EmitDataFieldName");
    //        id795_CustomColorTheme = myNameTable.Add(@"CustomColorTheme");
    //        id930_InputTimePrecision = myNameTable.Add(@"InputTimePrecision");
    //        id312_PermissionLevel = myNameTable.Add(@"PermissionLevel");
    //        id513_BackgroundText = myNameTable.Add(@"BackgroundText");
    //        id831_SliceRelativeDisplacement = myNameTable.Add(@"SliceRelativeDisplacement");
    //        id1032_ThickLineWidth = myNameTable.Add(@"ThickLineWidth");
    //        id37_ContentReadonlyExpression = myNameTable.Add(@"ContentReadonlyExpression");
    //        id106_PageIndexsForHideHeaderFooter = myNameTable.Add(@"PageIndexsForHideHeaderFooter");
    //        id720_XMLValue = myNameTable.Add(@"XMLValue");
    //        id1034_DocumentPageSettings = myNameTable.Add(@"DocumentPageSettings");
    //        id918_Item = myNameTable.Add(@"DateFormatStringForFirstIndexOtherPage");
    //        id489_AllowUserToResizeHeight = myNameTable.Add(@"AllowUserToResizeHeight");
    //        id651_LabelAtUp = myNameTable.Add(@"LabelAtUp");
    //        id99_HeaderFooterDifferentFirstPage = myNameTable.Add(@"HeaderFooterDifferentFirstPage");
    //        id8_Attributes = myNameTable.Add(@"Attributes");
    //        id971_ShadowPointVisible = myNameTable.Add(@"ShadowPointVisible");
    //        id515_BorderVisible = myNameTable.Add(@"BorderVisible");
    //        id590_MinBarWidth = myNameTable.Add(@"MinBarWidth");
    //        id599_TopicID = myNameTable.Add(@"TopicID");
    //        id859_LanternValue = myNameTable.Add(@"LanternValue");
    //        id714_SubEntries = myNameTable.Add(@"SubEntries");
    //        id347_XPageInfo = myNameTable.Add(@"XPageInfo");
    //        id132_StrictUsePageSize = myNameTable.Add(@"StrictUsePageSize");
    //        id1022_ParameterName = myNameTable.Add(@"ParameterName");
    //        id527_SelectedIndex = myNameTable.Add(@"SelectedIndex");
    //        id289_ImportFooter = myNameTable.Add(@"ImportFooter");
    //        id125_DesignerPaperHeight = myNameTable.Add(@"DesignerPaperHeight");
    //        id999_OutofNormalRangeTextColorValue = myNameTable.Add(@"OutofNormalRangeTextColorValue");
    //        id1047_PartitionList = myNameTable.Add(@"PartitionList");
    //        id969_BottomPadding = myNameTable.Add(@"BottomPadding");
    //        id470_HoldWholeLine = myNameTable.Add(@"HoldWholeLine");
    //        id956_ColorValueForDownValue = myNameTable.Add(@"ColorValueForDownValue");
    //        id730_LinkTextForContactAction = myNameTable.Add(@"LinkTextForContactAction");
    //        id696_KBLibrary = myNameTable.Add(@"KBLibrary");
    //        id767_BarOpacity = myNameTable.Add(@"BarOpacity");
    //        id606_TransparentColorValue = myNameTable.Add(@"TransparentColorValue");
    //        id317_TypeName = myNameTable.Add(@"TypeName");
    //        id622_LocalStyle = myNameTable.Add(@"LocalStyle");
    //        id711_RangeMask = myNameTable.Add(@"RangeMask");
    //        id638_ShapeEllipseElement = myNameTable.Add(@"ShapeEllipseElement");
    //        id384_VBScriptItem = myNameTable.Add(@"VBScriptItem");
    //        id786_GridBorderLineStyle = myNameTable.Add(@"GridBorderLineStyle");
    //        id968_TopPadding = myNameTable.Add(@"TopPadding");
    //        id309_UserHistoryInfo = myNameTable.Add(@"UserHistoryInfo");
    //        id190_Multiline = myNameTable.Add(@"Multiline");
    //        id444_SlantSplitLineStyle = myNameTable.Add(@"SlantSplitLineStyle");
    //        id295_PrintColor = myNameTable.Add(@"PrintColor");
    //        id827_PieHeight = myNameTable.Add(@"PieHeight");
    //        id563_SpellCode3 = myNameTable.Add(@"SpellCode3");
    //        id722_LineSize = myNameTable.Add(@"LineSize");
    //        id135_HorizontalResolution = myNameTable.Add(@"HorizontalResolution");
    //        id724_PageText = myNameTable.Add(@"PageText");
    //        id3_StyleIndex = myNameTable.Add(@"StyleIndex");
    //        id884_DataGridTopPadding = myNameTable.Add(@"DataGridTopPadding");
    //        id776_GridTextHeight = myNameTable.Add(@"GridTextHeight");
    //        id91_XPageSettings = myNameTable.Add(@"XPageSettings");
    //        id422_DataFeedbackInfo = myNameTable.Add(@"DataFeedbackInfo");
    //        id812_Background = myNameTable.Add(@"Background");
    //        id682_EnableViewState = myNameTable.Add(@"EnableViewState");
    //        id127_LeftMargin = myNameTable.Add(@"LeftMargin");
    //        id845_VerifiedAlignment = myNameTable.Add(@"VerifiedAlignment");
    //        id416_IncludeKeywords = myNameTable.Add(@"IncludeKeywords");
    //        id1029_AutoTickFormatString = myNameTable.Add(@"AutoTickFormatString");
    //        id113_Item = myNameTable.Add(@"PageIndexsForPrintBackgroundImage");
    //        id419_RequiredInvalidateFlag = myNameTable.Add(@"RequiredInvalidateFlag");
    //        id631_ShapeContentStyleContainer = myNameTable.Add(@"ShapeContentStyleContainer");
    //        id60_LocalExcludeKeywords = myNameTable.Add(@"LocalExcludeKeywords");
    //        id907_ForeColorValue = myNameTable.Add(@"ForeColorValue");
    //        id1004_LoopTextList = myNameTable.Add(@"LoopTextList");
    //        id61_DisabledParameterNames = myNameTable.Add(@"DisabledParameterNames");
    //        id28_JavaScriptForDoubleClick = myNameTable.Add(@"JavaScriptForDoubleClick");
    //        id1_TemperatureDocument = myNameTable.Add(@"TemperatureDocument");
    //        id852_IntCharSymbol = myNameTable.Add(@"IntCharSymbol");
    //        id756_DataFieldNameForSeriesName = myNameTable.Add(@"DataFieldNameForSeriesName");
    //        id687_OptionsPropertyName = myNameTable.Add(@"OptionsPropertyName");
    //        id640_ShapeDocumentPage = myNameTable.Add(@"ShapeDocumentPage");
    //        id925_YAxisInfos = myNameTable.Add(@"YAxisInfos");
    //        id1051_BrushStyle = myNameTable.Add(@"BrushStyle");
    //        id617_PageImages = myNameTable.Add(@"PageImages");
    //        id270_DocumentProcessState = myNameTable.Add(@"DocumentProcessState");
    //        id339_XTDBarcodeField = myNameTable.Add(@"XTDBarcodeField");
    //        id635_ShapeRectangleElement = myNameTable.Add(@"ShapeRectangleElement");
    //        id658_PrintTextForUnChecked = myNameTable.Add(@"PrintTextForUnChecked");
    //        id551_ValuePath = myNameTable.Add(@"ValuePath");
    //        id616_CompressSaveMode = myNameTable.Add(@"CompressSaveMode");
    //        id291_Default = myNameTable.Add(@"Default");
    //        id146_Text = myNameTable.Add(@"Text");
    //        id394_BindingPath = myNameTable.Add(@"BindingPath");
    //        id232_LineWidth = myNameTable.Add(@"LineWidth");
    //        id975_Scale = myNameTable.Add(@"Scale");
    //        id738_ScriptTextForClick = myNameTable.Add(@"ScriptTextForClick");
    //        id253_FieldBorderElementWidth = myNameTable.Add(@"FieldBorderElementWidth");
    //        id1002_BlockWidth = myNameTable.Add(@"BlockWidth");
    //        id350_XTextCheckBox = myNameTable.Add(@"XTextCheckBox");
    //        id343_XFileBlock = myNameTable.Add(@"XFileBlock");
    //        id445_RowSpan = myNameTable.Add(@"RowSpan");
    //        id662_CaptionAlign = myNameTable.Add(@"CaptionAlign");
    //        id667_CheckBoxVisible = myNameTable.Add(@"CheckBoxVisible");
    //        id54_Visible = myNameTable.Add(@"Visible");
    //        id889_Labels = myNameTable.Add(@"Labels");
    //        id514_ViewEncryptType = myNameTable.Add(@"ViewEncryptType");
    //        id887_SpecifyTickWidth = myNameTable.Add(@"SpecifyTickWidth");
    //        id717_OwnerID = myNameTable.Add(@"OwnerID");
    //        id532_CustomValueEditorTypeName = myNameTable.Add(@"CustomValueEditorTypeName");
    //        id461_PrintPositionInPage = myNameTable.Add(@"PrintPositionInPage");
    //        id94_SwapGutter = myNameTable.Add(@"SwapGutter");
    //        id684_AllowUserResize = myNameTable.Add(@"AllowUserResize");
    //        id797_Colors = myNameTable.Add(@"Colors");
    //        id239_BindingUserTrack = myNameTable.Add(@"BindingUserTrack");
    //        id636_ShapeZoomInElement = myNameTable.Add(@"ShapeZoomInElement");
    //        id410_CheckDecimalDigits = myNameTable.Add(@"CheckDecimalDigits");
    //        id217_Width = myNameTable.Add(@"Width");
    //        id835_EnumLableType = myNameTable.Add(@"EnumLableType");
    //        id89_DocumentGraphicsUnit = myNameTable.Add(@"DocumentGraphicsUnit");
    //        id488_Item = myNameTable.Add(@"AllowUserPressTabKeyToInsertRowDown");
    //        id233_GridNumInOnePage = myNameTable.Add(@"GridNumInOnePage");
    //        id591_VAlign = myNameTable.Add(@"VAlign");
    //        id926_YAxis = myNameTable.Add(@"YAxis");
    //        id1031_GridYSpaceNumForBottomPadding = myNameTable.Add(@"GridYSpaceNumForBottomPadding");
    //        id10_InnerID = myNameTable.Add(@"InnerID");
    //        id78_GlobalJavaScriptReferences = myNameTable.Add(@"GlobalJavaScriptReferences");
    //        id4_PreviewTextLength = myNameTable.Add(@"PreviewTextLength");
    //        id838_Config = myNameTable.Add(@"Config");
    //        id853_LinkTarget = myNameTable.Add(@"LinkTarget");
    //        id304_GridLineColor = myNameTable.Add(@"GridLineColor");
    //        id440_StressBorder = myNameTable.Add(@"StressBorder");
    //        id1006_TitleAlign = myNameTable.Add(@"TitleAlign");
    //        id1042_IsMultiSelect = myNameTable.Add(@"IsMultiSelect");
    //        id358_XInputFieldBase = myNameTable.Add(@"XInputFieldBase");
    //        id858_EndTime = myNameTable.Add(@"EndTime");
    //        id569_NextTarget = myNameTable.Add(@"NextTarget");
    //        id521_AdornTextType = myNameTable.Add(@"AdornTextType");
    //        id77_PageContentVersions = myNameTable.Add(@"PageContentVersions");
    //        id935_LineStyleForLanternValue = myNameTable.Add(@"LineStyleForLanternValue");
    //        id765_ChartDocumentStyle = myNameTable.Add(@"ChartDocumentStyle");
    //        id346_XMedia = myNameTable.Add(@"XMedia");
    //        id1063_WhiteSpaceLength = myNameTable.Add(@"WhiteSpaceLength");
    //        id174_Superscript = myNameTable.Add(@"Superscript");
    //        id624_Elements = myNameTable.Add(@"Elements");
    //        id71_UserHistories = myNameTable.Add(@"UserHistories");
    //        id547_InputFieldListItem = myNameTable.Add(@"InputFieldListItem");
    //        id417_ExcludeKeywords = myNameTable.Add(@"ExcludeKeywords");
    //        id95_GutterPosition = myNameTable.Add(@"GutterPosition");
    //        id562_SpellCode2 = myNameTable.Add(@"SpellCode2");
    //        id315_CheckedValue = myNameTable.Add(@"CheckedValue");
    //        id483_Item = myNameTable.Add(@"CloneMultipleBaseForBindingDataSource");
    //        id500_BackgroundTextColor = myNameTable.Add(@"BackgroundTextColor");
    //        id863_ValueTextTopPadding = myNameTable.Add(@"ValueTextTopPadding");
    //        id989_FieldNameForValue = myNameTable.Add(@"FieldNameForValue");
    //        id729_AttributeNameForContactAction = myNameTable.Add(@"AttributeNameForContactAction");
    //        id614_AdditionShape = myNameTable.Add(@"AdditionShape");
    //        id7_EditorVersionString = myNameTable.Add(@"EditorVersionString");
    //        id335_XSignImage = myNameTable.Add(@"XSignImage");
    //        id228_TextInMiddlePage = myNameTable.Add(@"TextInMiddlePage");
    //        id806_EndCap = myNameTable.Add(@"EndCap");
    //        id413_DateTimeMinValue = myNameTable.Add(@"DateTimeMinValue");
    //        id1041_DCTimeLineImage = myNameTable.Add(@"DCTimeLineImage");
    //        id27_JavaScriptForClick = myNameTable.Add(@"JavaScriptForClick");
    //        id385_ScriptMethodName = myNameTable.Add(@"ScriptMethodName");
    //        id655_CheckboxVisibility = myNameTable.Add(@"CheckboxVisibility");
    //        id873_EnableExtMouseMoveEvent = myNameTable.Add(@"EnableExtMouseMoveEvent");
    //        id116_PaperSource = myNameTable.Add(@"PaperSource");
    //        id246_BorderColor = myNameTable.Add(@"BorderColor");
    //        id136_VerticalResolution = myNameTable.Add(@"VerticalResolution");
    //        id284_MotherTemplateInfo = myNameTable.Add(@"MotherTemplateInfo");
    //        id611_KeepWidthHeightRate = myNameTable.Add(@"KeepWidthHeightRate");
    //        id50_XElements = myNameTable.Add(@"XElements");
    //        id986_FieldNameForLink = myNameTable.Add(@"FieldNameForLink");
    //        id964_ValueFieldName = myNameTable.Add(@"ValueFieldName");
    //        id976_YAxisScaleInfo = myNameTable.Add(@"YAxisScaleInfo");
    //        id298_CommentIndex = myNameTable.Add(@"CommentIndex");
    //        id609_ValueIndexOfRepeatedImage = myNameTable.Add(@"ValueIndexOfRepeatedImage");
    //        id895_PageTitlePosition = myNameTable.Add(@"PageTitlePosition");
    //        id242_CreationTime = myNameTable.Add(@"CreationTime");
    //        id672_ChangePageIndexMidway = myNameTable.Add(@"ChangePageIndexMidway");
    //        id552_Items = myNameTable.Add(@"Items");
    //        id865_UpAndDown = myNameTable.Add(@"UpAndDown");
    //        id123_DesignerPaperWidth = myNameTable.Add(@"DesignerPaperWidth");
    //        id798_ShadeCount = myNameTable.Add(@"ShadeCount");
    //        id130_BottomMargin = myNameTable.Add(@"BottomMargin");
    //        id74_RepeatedImages = myNameTable.Add(@"RepeatedImages");
    //        id985_FieldNameForID = myNameTable.Add(@"FieldNameForID");
    //        id741_DrawContentHandlerName = myNameTable.Add(@"DrawContentHandlerName");
    //        id269_DocumentType = myNameTable.Add(@"DocumentType");
    //        id382_DescPropertyName = myNameTable.Add(@"DescPropertyName");
    //        id779_GridBackground = myNameTable.Add(@"GridBackground");
    //        id992_SQLText = myNameTable.Add(@"SQLText");
    //        id109_OffsetX = myNameTable.Add(@"OffsetX");
    //        id110_OffsetY = myNameTable.Add(@"OffsetY");
    //        id204_BorderRight = myNameTable.Add(@"BorderRight");
    //        id995_BlankDateWhenNoData = myNameTable.Add(@"BlankDateWhenNoData");
    //        id496_BorderElementColorValue = myNameTable.Add(@"BorderElementColorValue");
    //        id25_ToolTip = myNameTable.Add(@"ToolTip");
    //        id586_Digitals = myNameTable.Add(@"Digitals");
    //        id1005_PageTitleTexts = myNameTable.Add(@"PageTitleTexts");
    //        id811_SymbolSize = myNameTable.Add(@"SymbolSize");
    //        id429_AutoClean = myNameTable.Add(@"AutoClean");
    //        id207_MarginTop = myNameTable.Add(@"MarginTop");
    //        id455_IsCollapsed = myNameTable.Add(@"IsCollapsed");
    //        id965_LanternValueFieldName = myNameTable.Add(@"LanternValueFieldName");
    //        id11_ID = myNameTable.Add(@"ID");
    //        id181_LetterSpacing = myNameTable.Add(@"LetterSpacing");
    //        id770_BarGroupSpacing = myNameTable.Add(@"BarGroupSpacing");
    //        id911_GridLineColorValue = myNameTable.Add(@"GridLineColorValue");
    //        id561_SpellCode = myNameTable.Add(@"SpellCode");
    //        id40_ValueExpression = myNameTable.Add(@"ValueExpression");
    //        id642_AutoSize = myNameTable.Add(@"AutoSize");
    //        id771_ViewStyle = myNameTable.Add(@"ViewStyle");
    //        id482_DataSourceRowSpan = myNameTable.Add(@"DataSourceRowSpan");
    //        id250_SubDocumentSettings = myNameTable.Add(@"SubDocumentSettings");
    //        id471_PrintBothBorderWhenJumpPrint = myNameTable.Add(@"PrintBothBorderWhenJumpPrint");
    //        id698_BaseURL = myNameTable.Add(@"BaseURL");
    //        id813_XBrushStyle = myNameTable.Add(@"XBrushStyle");
    //        id33_AutoHideMode = myNameTable.Add(@"AutoHideMode");
    //        id1044_IdentyColorARGBValue = myNameTable.Add(@"IdentyColorARGBValue");
    //        id1043_ElementIDForExporting = myNameTable.Add(@"ElementIDForExporting");
    //        id862_VerifiedColor = myNameTable.Add(@"VerifiedColor");
    //        id52_AcceptChildElementTypes2 = myNameTable.Add(@"AcceptChildElementTypes2");
    //        id517_EnableUserEditInnerValue = myNameTable.Add(@"EnableUserEditInnerValue");
    //        id847_SpecifySymbolStyle = myNameTable.Add(@"SpecifySymbolStyle");
    //        id957_ColorValueForUpValue = myNameTable.Add(@"ColorValueForUpValue");
    //        id188_VertialText = myNameTable.Add(@"VertialText");
    //        id251_Readonly = myNameTable.Add(@"Readonly");
    //        id150_XFontValue = myNameTable.Add(@"XFontValue");
    //        id1040_ShowBorder = myNameTable.Add(@"ShowBorder");
    //        id454_EnableCollapse = myNameTable.Add(@"EnableCollapse");
    //        id282_PageContentVersionInfo = myNameTable.Add(@"PageContentVersionInfo");
    //        id401_ValueName = myNameTable.Add(@"ValueName");
    //        id273_NumOfPage = myNameTable.Add(@"NumOfPage");
    //        id894_FooterDescription = myNameTable.Add(@"FooterDescription");
    //        id121_HeaderDistance = myNameTable.Add(@"HeaderDistance");
    //        id792_RipenessRate = myNameTable.Add(@"RipenessRate");
    //        id866_DCTimeLineParameter = myNameTable.Add(@"DCTimeLineParameter");
    //        id342_XTextBlockElement = myNameTable.Add(@"XTextBlockElement");
    //        id303_GridLineStyle = myNameTable.Add(@"GridLineStyle");
    //        id367_XTextContentElement = myNameTable.Add(@"XTextContentElement");
    //        id365_XTextTableRow = myNameTable.Add(@"XTextTableRow");
    //        id485_PrintCellBorder = myNameTable.Add(@"PrintCellBorder");
    //        id904_Tick = myNameTable.Add(@"Tick");
    //        id709_KBEntry = myNameTable.Add(@"KBEntry");
    //        id180_LineSpacingStyle = myNameTable.Add(@"LineSpacingStyle");
    //        id1026_StartTime = myNameTable.Add(@"StartTime");
    //        id187_LeftIndent = myNameTable.Add(@"LeftIndent");
    //        id575_EventExpressionInfo = myNameTable.Add(@"EventExpressionInfo");
    //        id1027_AlignToGrid = myNameTable.Add(@"AlignToGrid");
    //        id387_DCContentLockInfo = myNameTable.Add(@"DCContentLockInfo");
    //        id937_AllowOutofRange = myNameTable.Add(@"AllowOutofRange");
    //        id700_TemplateSourceFormatString = myNameTable.Add(@"TemplateSourceFormatString");
    //        id963_SymbolColorValue = myNameTable.Add(@"SymbolColorValue");
    //        id70_BodyGridLineOffset = myNameTable.Add(@"BodyGridLineOffset");
    //        id533_EnableValueEditor = myNameTable.Add(@"EnableValueEditor");
    //        id126_PaperHeight = myNameTable.Add(@"PaperHeight");
    //        id703_Entry = myNameTable.Add(@"Entry");
    //        id201_BorderLeft = myNameTable.Add(@"BorderLeft");
    //        id598_FileContentSource = myNameTable.Add(@"FileContentSource");
    //        id277_SubDocumentSpacing = myNameTable.Add(@"SubDocumentSpacing");
    //        id97_ShowGutterLine = myNameTable.Add(@"ShowGutterLine");
    //        id51_Element = myNameTable.Add(@"Element");
    //        id996_Item = myNameTable.Add(@"HiddenOnPageViewWhenNoValuePoints");
    //        id733_BarcodeType = myNameTable.Add(@"BarcodeType");
    //        id898_ImagePixelHeight = myNameTable.Add(@"ImagePixelHeight");
    //        id1024_TickInfo = myNameTable.Add(@"TickInfo");
    //        id519_AutoSetSpellCodeInDropdownList = myNameTable.Add(@"AutoSetSpellCodeInDropdownList");
    //        id216_Top = myNameTable.Add(@"Top");
    //        id383_IgnoreChildElements = myNameTable.Add(@"IgnoreChildElements");
    //        id1059_SynchroDataMode = myNameTable.Add(@"SynchroDataMode");
    //        id1060_AutoSize = myNameTable.Add(@"AutoSize");
    //        id1060_EmptyWhenNoData = myNameTable.Add(@"EmptyWhenNoData");
    //        id809_ChartLegendStyle = myNameTable.Add(@"ChartLegendStyle");
    //        id240_AuthorID = myNameTable.Add(@"AuthorID");
    //        id595_UpdateState = myNameTable.Add(@"UpdateState");
    //        id379_CopySourceInfo = myNameTable.Add(@"CopySourceInfo");
    //        id869_LineWidthZoomRateWhenPrint = myNameTable.Add(@"LineWidthZoomRateWhenPrint");
    //        id118_AutoPaperWidth = myNameTable.Add(@"AutoPaperWidth");
    //        id680_ValuePropertyName = myNameTable.Add(@"ValuePropertyName");
    //        id755_DataSourceName = myNameTable.Add(@"DataSourceName");
    //        id5_TransparentEncryptErrorMessage = myNameTable.Add(@"TransparentEncryptErrorMessage");
    //        id503_TabIndex = myNameTable.Add(@"TabIndex");
    //        id24_ReferencedDataName = myNameTable.Add(@"ReferencedDataName");
    //        id38_VisibleExpression = myNameTable.Add(@"VisibleExpression");
    //        id801_XPenStyle = myNameTable.Add(@"XPenStyle");
    //        id221_ParagraphMultiLevel = myNameTable.Add(@"ParagraphMultiLevel");
    //        id1028_AutoTickStepSeconds = myNameTable.Add(@"AutoTickStepSeconds");
    //        id905_BigTitleFontSize = myNameTable.Add(@"BigTitleFontSize");
    //        id1015_LayoutType = myNameTable.Add(@"LayoutType");
    //        id970_ValueFont = myNameTable.Add(@"ValueFont");
    //        id602_InnerRepeatImageIndex = myNameTable.Add(@"InnerRepeatImageIndex");
    //        id834_DownleadWidth = myNameTable.Add(@"DownleadWidth");
    //        id967_MaxTextDisplayLength = myNameTable.Add(@"MaxTextDisplayLength");
    //        id571_AutoUpdateTargetElement = myNameTable.Add(@"AutoUpdateTargetElement");
    //        id19_DataFeedback = myNameTable.Add(@"DataFeedback");
    //        id345_XTextControlHost = myNameTable.Add(@"XTextControlHost");
    //        id318_ValueType = myNameTable.Add(@"ValueType");
    //        id219_PageBreakAfter = myNameTable.Add(@"PageBreakAfter");
    //        id271_DocumentEditState = myNameTable.Add(@"DocumentEditState");
    //        id236_Printable = myNameTable.Add(@"Printable");
    //        id625_ShapeElement = myNameTable.Add(@"ShapeElement");
    //        id745_SignErrorMessage = myNameTable.Add(@"SignErrorMessage");
    //        id319_SourceColumn = myNameTable.Add(@"SourceColumn");
    //        id447_DesignRowIndex = myNameTable.Add(@"DesignRowIndex");
    //        id725_StrictMatchPageIndex = myNameTable.Add(@"StrictMatchPageIndex");
    //        id637_ShapePolygonElement = myNameTable.Add(@"ShapePolygonElement");
    //        id361_XBarcodeField = myNameTable.Add(@"XBarcodeField");
    //        id301_GridLineType = myNameTable.Add(@"GridLineType");
    //        id425_KeyFieldName = myNameTable.Add(@"KeyFieldName");
    //        id775_GridTextWidth = myNameTable.Add(@"GridTextWidth");
    //        id842_Values = myNameTable.Add(@"Values");
    //        id163_Visibility = myNameTable.Add(@"Visibility");
    //        id768_BarWidth = myNameTable.Add(@"BarWidth");
    //        id449_MirrorViewForCrossPage = myNameTable.Add(@"MirrorViewForCrossPage");
    //        id310_Name2 = myNameTable.Add(@"Name2");
    //        id570_NextTargetID = myNameTable.Add(@"NextTargetID");
    //        id176_FixedSpacing = myNameTable.Add(@"FixedSpacing");
    //        id981_NormalMinValue = myNameTable.Add(@"NormalMinValue");
    //        id276_HeightInPrintJob = myNameTable.Add(@"HeightInPrintJob");
    //        id395_BindingPathForText = myNameTable.Add(@"BindingPathForText");
    //        id227_DocumentTerminalTextInfo = myNameTable.Add(@"DocumentTerminalTextInfo");
    //        id141_Type = myNameTable.Add(@"Type");
    //        id206_MarginLeft = myNameTable.Add(@"MarginLeft");
    //        id314_Tag = myNameTable.Add(@"Tag");
    //        id924_FooterLines = myNameTable.Add(@"FooterLines");
    //        id973_AbNormalRangeSettings = myNameTable.Add(@"AbNormalRangeSettings");
    //        id368_XTextSection = myNameTable.Add(@"XTextSection");
    //        id565_LinkListBindingInfo = myNameTable.Add(@"LinkListBindingInfo");
    //        id235_LineStyle = myNameTable.Add(@"LineStyle");
    //        id740_ChartPageIndex = myNameTable.Add(@"ChartPageIndex");
    //        id460_PrintedPageIndex = myNameTable.Add(@"PrintedPageIndex");
    //        id697_TemplateFileSystemName = myNameTable.Add(@"TemplateFileSystemName");
    //        id523_EditorControlTypeName = myNameTable.Add(@"EditorControlTypeName");
    //        id452_ExpendForDataBinding = myNameTable.Add(@"ExpendForDataBinding");
    //        id157_PageBorderBackgroundStyle = myNameTable.Add(@"PageBorderBackgroundStyle");
    //        id823_Item = myNameTable.Add(@"DataFieldNameForFillColorString");
    //        id701_TemplateFileFormat = myNameTable.Add(@"TemplateFileFormat");
    //        id939_ClickToHide = myNameTable.Add(@"ClickToHide");
    //        id381_SourcePropertyName = myNameTable.Add(@"SourcePropertyName");
    //        id920_HeaderLabels = myNameTable.Add(@"HeaderLabels");
    //        id879_HeaderLabelLineAlignment = myNameTable.Add(@"HeaderLabelLineAlignment");
    //        id524_LinkListBinding = myNameTable.Add(@"LinkListBinding");
    //        id670_AutoHeight = myNameTable.Add(@"AutoHeight");
    //        id65_Parameter = myNameTable.Add(@"Parameter");
    //        id67_InnerRepeatImageDataList = myNameTable.Add(@"InnerRepeatImageDataList");
    //        id941_EnableLanternValue = myNameTable.Add(@"EnableLanternValue");
    //        id934_LanternValueColorForDownValue = myNameTable.Add(@"LanternValueColorForDownValue");
    //        id643_Points = myNameTable.Add(@"Points");
    //        id993_TitleLineInfo = myNameTable.Add(@"TitleLineInfo");
    //        id708_Content = myNameTable.Add(@"Content");
    //        id874_EnableDataGridLinearAxisMode = myNameTable.Add(@"EnableDataGridLinearAxisMode");
    //        id890_Label = myNameTable.Add(@"Label");
    //        id154_Italic = myNameTable.Add(@"Italic");
    //        id1048_XImagePartition = myNameTable.Add(@"XImagePartition");
    //        id732_BarcodeStyle2 = myNameTable.Add(@"BarcodeStyle2");
    //        id183_RTFLineSpacing = myNameTable.Add(@"RTFLineSpacing");
    //        id710_CopyListItems = myNameTable.Add(@"CopyListItems");
    //        id764_ChartStyle = myNameTable.Add(@"ChartStyle");
    //        id324_XPageBreak = myNameTable.Add(@"XPageBreak");
    //        id351_XImage = myNameTable.Add(@"XImage");
    //        id549_SourceName = myNameTable.Add(@"SourceName");
    //        id75_Image = myNameTable.Add(@"Image");
    //        id41_UserFlags = myNameTable.Add(@"UserFlags");
    //        id333_XPie = myNameTable.Add(@"XPie");
    //        id961_HiddenValueTitleBackColorValue = myNameTable.Add(@"HiddenValueTitleBackColorValue");
    //        id950_RedLineValue = myNameTable.Add(@"RedLineValue");
    //        id307_Link = myNameTable.Add(@"Link");
    //        id566_ProviderName = myNameTable.Add(@"ProviderName");
    //        id948_ValueFormatString = myNameTable.Add(@"ValueFormatString");
    //        id377_GridLine = myNameTable.Add(@"GridLine");
    //        id966_TimeFieldName = myNameTable.Add(@"TimeFieldName");
    //        id715_ListItemsSource = myNameTable.Add(@"ListItemsSource");
    //        id782_GridLineOffsetX = myNameTable.Add(@"GridLineOffsetX");
    //        id302_GridLineOffsetY = myNameTable.Add(@"GridLineOffsetY");
    //        id255_IsTemplate = myNameTable.Add(@"IsTemplate");
    //        id695_KB = myNameTable.Add(@"KB");
    //        id721_LineLengthInCM = myNameTable.Add(@"LineLengthInCM");
    //        id705_Document = myNameTable.Add(@"Document");
    //        id431_CommandText = myNameTable.Add(@"CommandText");
    //        id322_SerializeValue = myNameTable.Add(@"SerializeValue");
    //        id1037_MultiLine = myNameTable.Add(@"MultiLine");
    //        id267_DepartmentName = myNameTable.Add(@"DepartmentName");
    //        id676_SpecifyPageIndex = myNameTable.Add(@"SpecifyPageIndex");
    //        id415_RegExpression = myNameTable.Add(@"RegExpression");
    //        id917_Item = myNameTable.Add(@"DateFormatStringForFirstIndexFirstPage");
    //        id321_ValueTypeFullName = myNameTable.Add(@"ValueTypeFullName");
    //        id804_LineJoin = myNameTable.Add(@"LineJoin");
    //        id915_DateFormatStringForCrossMonth = myNameTable.Add(@"DateFormatStringForCrossMonth");
    //        id629_EditMode = myNameTable.Add(@"EditMode");
    //        id1017_TickLineVisible = myNameTable.Add(@"TickLineVisible");
    //        id478_Columns = myNameTable.Add(@"Columns");
    //        id458_DocumentID = myNameTable.Add(@"DocumentID");
    //        id297_LayoutDirection = myNameTable.Add(@"LayoutDirection");
    //        id13_TransparentEncryptMode = myNameTable.Add(@"TransparentEncryptMode");
    //        id151_Size = myNameTable.Add(@"Size");
    //        id870_LinkVisualStyle = myNameTable.Add(@"LinkVisualStyle");
    //        id363_XDirectoryField = myNameTable.Add(@"XDirectoryField");
    //        id87_MeasureMode = myNameTable.Add(@"MeasureMode");
    //        id424_FieldName = myNameTable.Add(@"FieldName");
    //        id892_SpecifyStartDate = myNameTable.Add(@"SpecifyStartDate");
    //        id328_XLineBreak = myNameTable.Add(@"XLineBreak");
    //        id1053_PointF = myNameTable.Add(@"PointF");
    //        id481_GroupName = myNameTable.Add(@"GroupName");
    //        id539_MultiColumn = myNameTable.Add(@"MultiColumn");
    //        id758_DataFieldNameForText = myNameTable.Add(@"DataFieldNameForText");
    //        id896_ShowIcon = myNameTable.Add(@"ShowIcon");
    //        id621_ShapeDocument = myNameTable.Add(@"ShapeDocument");
    //        id900_GridYSplitInfo = myNameTable.Add(@"GridYSplitInfo");
    //        id43_ContentLock = myNameTable.Add(@"ContentLock");
    //        id79_Reference = myNameTable.Add(@"Reference");
    //        id875_ExtendDaysForTimeLine = myNameTable.Add(@"ExtendDaysForTimeLine");
    //        id20_MaxInputLength = myNameTable.Add(@"MaxInputLength");
    //        id397_ProcessState = myNameTable.Add(@"ProcessState");
    //        id912_GridBackColorValue = myNameTable.Add(@"GridBackColorValue");
    //        id972_RedLinePrintVisible = myNameTable.Add(@"RedLinePrintVisible");
    //        id464_ContentLoaded = myNameTable.Add(@"ContentLoaded");
    //        id370_XTextTableCell = myNameTable.Add(@"XTextTableCell");
    //        id355_XField = myNameTable.Add(@"XField");
    //        id487_AllowInsertRowDownUseHotKey = myNameTable.Add(@"AllowInsertRowDownUseHotKey");
    //        id373_XTextHeaderForFirstPage = myNameTable.Add(@"XTextHeaderForFirstPage");
    //        id778_GridValueFormat = myNameTable.Add(@"GridValueFormat");
    //        id215_Left = myNameTable.Add(@"Left");
    //        id927_YAxisInfo = myNameTable.Add(@"YAxisInfo");
    //        id632_ShapeLinesElement = myNameTable.Add(@"ShapeLinesElement");
    //        id435_DisplayFormat = myNameTable.Add(@"DisplayFormat");
    //        id1050_XPartition = myNameTable.Add(@"XPartition");
    //        id982_NormalRangeUpLineStyle = myNameTable.Add(@"NormalRangeUpLineStyle");
    //        id329_XTextObjectElement = myNameTable.Add(@"XTextObjectElement");
    //        id530_LastSelectedListItems = myNameTable.Add(@"LastSelectedListItems");
    //        id734_ErroeCorrectionLevel = myNameTable.Add(@"ErroeCorrectionLevel");
    //        id857_Time = myNameTable.Add(@"Time");
    //        id783_VerticalTextAlign = myNameTable.Add(@"VerticalTextAlign");
    //        id336_XCustomShape = myNameTable.Add(@"XCustomShape");
    //        id945_TitleValueDispalyFormat = myNameTable.Add(@"TitleValueDispalyFormat");
    //        id612_Source = myNameTable.Add(@"Source");
    //        id601_AutoCreate = myNameTable.Add(@"AutoCreate");
    //        id707_KBID = myNameTable.Add(@"KBID");
    //        id769_BarSpacing = myNameTable.Add(@"BarSpacing");
    //        id675_RawPageIndex = myNameTable.Add(@"RawPageIndex");
    //        id763_DataItem = myNameTable.Add(@"DataItem");
    //        id212_PaddingRight = myNameTable.Add(@"PaddingRight");
    //        id685_SavePreviewImage = myNameTable.Add(@"SavePreviewImage");
    //        id103_DocumentGridLine = myNameTable.Add(@"DocumentGridLine");
    //        id84_BodyText = myNameTable.Add(@"BodyText");
    //        id592_EditValueInDialog = myNameTable.Add(@"EditValueInDialog");
    //        id62_Name = myNameTable.Add(@"Name");
    //        id331_XPartitionImage = myNameTable.Add(@"XPartitionImage");
    //        id1049_Url = myNameTable.Add(@"Url");
    //        id208_MarginRight = myNameTable.Add(@"MarginRight");
    //        id851_IntCharLantern = myNameTable.Add(@"IntCharLantern");
    //        id1025_TimeLineZoneInfo = myNameTable.Add(@"TimeLineZoneInfo");
    //        id936_SpecifyTitleWidth = myNameTable.Add(@"SpecifyTitleWidth");
    //        id504_SpecifyWidth = myNameTable.Add(@"SpecifyWidth");
    //        id279_Locked = myNameTable.Add(@"Locked");
    //        id1060_SmartChartMode = myNameTable.Add(@"SmartChartMode");
    //        id172_UnderlineStyle = myNameTable.Add(@"UnderlineStyle");
    //        id664_ControlStyle = myNameTable.Add(@"ControlStyle");
    //        id564_ListIndex = myNameTable.Add(@"ListIndex");
    //        id991_FieldNameForText = myNameTable.Add(@"FieldNameForText");
    //        id693_Temp20200817 = myNameTable.Add(@"Temp20200817");
    //        id958_SymbolStyle = myNameTable.Add(@"SymbolStyle");
    //        id430_ConnectionName = myNameTable.Add(@"ConnectionName");
    //        id529_EnableLastSelectedListItems = myNameTable.Add(@"EnableLastSelectedListItems");
    //        id175_Subscript = myNameTable.Add(@"Subscript");
    //        id1014_ShowBackColor = myNameTable.Add(@"ShowBackColor");
    //        id450_CloneType = myNameTable.Add(@"CloneType");
    //        id665_Checked = myNameTable.Add(@"Checked");
    //        id380_SourceID = myNameTable.Add(@"SourceID");
    //        id285_FileSystemName = myNameTable.Add(@"FileSystemName");
    //        id537_GetValueOrderByTime = myNameTable.Add(@"GetValueOrderByTime");
    //        id674_SpecifyPageIndexInfo = myNameTable.Add(@"SpecifyPageIndexInfo");
    //        id112_Watermark = myNameTable.Add(@"Watermark");
    //        id588_TextAlignment = myNameTable.Add(@"TextAlignment");
    //        id752_LastVerifyResult = myNameTable.Add(@"LastVerifyResult");
    //        id683_ViewState = myNameTable.Add(@"ViewState");
    //        id256_MRID = myNameTable.Add(@"MRID");
    //        id868_BothBlackWhenPrint = myNameTable.Add(@"BothBlackWhenPrint");
    //        id390_XDataBinding = myNameTable.Add(@"XDataBinding");
    //        id39_PrintVisibilityExpression = myNameTable.Add(@"PrintVisibilityExpression");
    //        id376_XTextBody = myNameTable.Add(@"XTextBody");
    //        id634_ShapeWireLabelElement = myNameTable.Add(@"ShapeWireLabelElement");
    //        id727_ReferencedTopicID = myNameTable.Add(@"ReferencedTopicID");
    //        id620_PageImageInfo = myNameTable.Add(@"PageImageInfo");
    //        id641_ShapeDocumentImagePage = myNameTable.Add(@"ShapeDocumentImagePage");
    //        id306_DeleterIndex = myNameTable.Add(@"DeleterIndex");
    //        id139_ValueIndex = myNameTable.Add(@"ValueIndex");
    //        id692_FileContentType = myNameTable.Add(@"FileContentType");
    //        id799_LightCorrectionFactor = myNameTable.Add(@"LightCorrectionFactor");
    //        id234_GridSpanInCM = myNameTable.Add(@"GridSpanInCM");
    //        id178_SpacingBeforeParagraph = myNameTable.Add(@"SpacingBeforeParagraph");
    //        id129_RightMargin = myNameTable.Add(@"RightMargin");
    //        id626_LocalElementStyleMode = myNameTable.Add(@"LocalElementStyleMode");
    //        id796_ThemeType = myNameTable.Add(@"ThemeType");
    //        id509_UserEditable = myNameTable.Add(@"UserEditable");
    //        id498_BackgroundTextItalic = myNameTable.Add(@"BackgroundTextItalic");
    //        id947_YSplitNum = myNameTable.Add(@"YSplitNum");
    //        id114_Item = myNameTable.Add(@"PageIndexsForShowBackgroundImage");
    //        id659_CheckAlignLeft = myNameTable.Add(@"CheckAlignLeft");
    //        id354_XTextContainerElement = myNameTable.Add(@"XTextContainerElement");
    //        id145_Alpha = myNameTable.Add(@"Alpha");
    //        id296_PrintBackColor = myNameTable.Add(@"PrintBackColor");
    //        id580_DisplayLevel = myNameTable.Add(@"DisplayLevel");
    //        id520_DefaultValueType = myNameTable.Add(@"DefaultValueType");
    //        id583_ShowGrid = myNameTable.Add(@"ShowGrid");
    //        id406_CheckMaxValue = myNameTable.Add(@"CheckMaxValue");
    //        id840_Data = myNameTable.Add(@"Data");
    //        id6_DataEncryptProviderName = myNameTable.Add(@"DataEncryptProviderName");
    //        id108_ForPOSPrinter = myNameTable.Add(@"ForPOSPrinter");
    //        id88_LocalConfig = myNameTable.Add(@"LocalConfig");
    //        id104_TerminalText = myNameTable.Add(@"TerminalText");
    //        id921_NumOfDaysInOnePage = myNameTable.Add(@"NumOfDaysInOnePage");
    //        id330_XNewMedicalExpression = myNameTable.Add(@"XNewMedicalExpression");
    //        id472_AllowUserDeleteRow = myNameTable.Add(@"AllowUserDeleteRow");
    //        id897_ImagePixelWidth = myNameTable.Add(@"ImagePixelWidth");
    //        id749_UseInnerSignProvider = myNameTable.Add(@"UseInnerSignProvider");
    //        id32_ValidateStyle = myNameTable.Add(@"ValidateStyle");
    //        id92_JointPrintNumber = myNameTable.Add(@"JointPrintNumber");
    //        id499_LableUnitTextBold = myNameTable.Add(@"LableUnitTextBold");
    //        id257_TimeoutHours = myNameTable.Add(@"TimeoutHours");
    //        id790_LegendStyle = myNameTable.Add(@"LegendStyle");
    //        id428_DCEmitDataSource = myNameTable.Add(@"DCEmitDataSource");
    //        id657_PrintTextForChecked = myNameTable.Add(@"PrintTextForChecked");
    //        id954_ShowPointValue = myNameTable.Add(@"ShowPointValue");
    //        id938_SeparatorLineVisible = myNameTable.Add(@"SeparatorLineVisible");
    //        id681_HostMode = myNameTable.Add(@"HostMode");
    //        id689_LoopPlay = myNameTable.Add(@"LoopPlay");
    //        id456_CompressOwnerLineSpacing = myNameTable.Add(@"CompressOwnerLineSpacing");
    //        id880_SelectionMode = myNameTable.Add(@"SelectionMode");
    //        id476_AllowUserToResizeRows = myNameTable.Add(@"AllowUserToResizeRows");
    //        id258_Version = myNameTable.Add(@"Version");
    //        id465_NumOfRows = myNameTable.Add(@"NumOfRows");
    //        id388_OwnerUserID = myNameTable.Add(@"OwnerUserID");
    //        id93_PrintZoomRate = myNameTable.Add(@"PrintZoomRate");
    //        id849_SymbolOffsetY = myNameTable.Add(@"SymbolOffsetY");
    //        id848_SymbolOffsetX = myNameTable.Add(@"SymbolOffsetX");
    //        id173_UnderlineColor = myNameTable.Add(@"UnderlineColor");
    //        id940_ValueVisible = myNameTable.Add(@"ValueVisible");
    //        id1007_ValueAlign = myNameTable.Add(@"ValueAlign");
    //        id323_XTextLock = myNameTable.Add(@"XTextLock");
    //        id497_TextColor = myNameTable.Add(@"TextColor");
    //        id308_ContentStyle = myNameTable.Add(@"ContentStyle");
    //        id1023_SeperatorChar = myNameTable.Add(@"SeperatorChar");
    //        id718_ObjectParameter = myNameTable.Add(@"ObjectParameter");
    //        id443_MoveFocusHotKey = myNameTable.Add(@"MoveFocusHotKey");
    //        id356_XBean = myNameTable.Add(@"XBean");
    //        id82_DocumentContentVersion = myNameTable.Add(@"DocumentContentVersion");
    //        id73_ContentStyles = myNameTable.Add(@"ContentStyles");
    //        id581_ShowPageIndex = myNameTable.Add(@"ShowPageIndex");
    //        id736_ImageForDown = myNameTable.Add(@"ImageForDown");
    //        id867_TemperatureDocumentConfig = myNameTable.Add(@"TemperatureDocumentConfig");
    //        id856_UseAdvVerticalStyle2 = myNameTable.Add(@"UseAdvVerticalStyle2");
    //        id403_BinaryLength = myNameTable.Add(@"BinaryLength");
    //        id507_UnitText = myNameTable.Add(@"UnitText");
    //        id200_BorderWidth = myNameTable.Add(@"BorderWidth");
    //        id119_AutoFitPageSize = myNameTable.Add(@"AutoFitPageSize");
    //        id143_ColorValue = myNameTable.Add(@"ColorValue");
    //        id923_Line = myNameTable.Add(@"Line");
    //        id459_Printed = myNameTable.Add(@"Printed");
    //        id650_LabelAtLeft = myNameTable.Add(@"LabelAtLeft");
    //        id541_MultiSelect = myNameTable.Add(@"MultiSelect");
    //        id668_VisualStyle = myNameTable.Add(@"VisualStyle");
    //        id748_SignTime = myNameTable.Add(@"SignTime");
    //        id953_ShowLegendInRule = myNameTable.Add(@"ShowLegendInRule");
    //        id57_ScriptText = myNameTable.Add(@"ScriptText");
    //        id412_DateTimeMaxValue = myNameTable.Add(@"DateTimeMaxValue");
    //        id501_BorderTextPosition = myNameTable.Add(@"BorderTextPosition");
    //        id746_DefaultSignMode = myNameTable.Add(@"DefaultSignMode");
    //        id171_EmphasisMark = myNameTable.Add(@"EmphasisMark");
    //        id492_EndingLineBreak = myNameTable.Add(@"EndingLineBreak");
    //        id469_Alignment = myNameTable.Add(@"Alignment");
    //        id404_MaxLength = myNameTable.Add(@"MaxLength");
    //        id805_StartCap = myNameTable.Add(@"StartCap");
    //        id819_ChartDataItem = myNameTable.Add(@"ChartDataItem");
    //        id49_EventTemplateName = myNameTable.Add(@"EventTemplateName");
    //        id597_SaveLinkedContent = myNameTable.Add(@"SaveLinkedContent");
    //        id448_DesignColIndex = myNameTable.Add(@"DesignColIndex");
    //        id759_DataFieldNameForValue = myNameTable.Add(@"DataFieldNameForValue");
    //        id671_PageIndexFix = myNameTable.Add(@"PageIndexFix");
    //        id229_MinHeightInCMUnit = myNameTable.Add(@"MinHeightInCMUnit");
    //        id83_Info = myNameTable.Add(@"Info");
    //        id260_LicenseText = myNameTable.Add(@"LicenseText");
    //        id59_JavaScriptTextForWebClient = myNameTable.Add(@"JavaScriptTextForWebClient");
    //        id1055_IsCustomFill = myNameTable.Add(@"IsCustomFill");
    //        id903_Ticks = myNameTable.Add(@"Ticks");
    //        id275_StartPositionInPringJob = myNameTable.Add(@"StartPositionInPringJob");
    //        id334_XChart = myNameTable.Add(@"XChart");
    //        id408_MaxValue = myNameTable.Add(@"MaxValue");
    //        id666_DefaultCheckedForValueBinding = myNameTable.Add(@"DefaultCheckedForValueBinding");
    //        id47_ScriptItems = myNameTable.Add(@"ScriptItems");
    //        id194_CharacterCircle = myNameTable.Add(@"CharacterCircle");
    //        id544_ListValueFormatString = myNameTable.Add(@"ListValueFormatString");
    //        id299_ProtectType = myNameTable.Add(@"ProtectType");
    //        id538_EditStyle = myNameTable.Add(@"EditStyle");
    //        id446_ColSpan = myNameTable.Add(@"ColSpan");
    //        id58_ScriptLanguage = myNameTable.Add(@"ScriptLanguage");
    //        id268_DocumentFormat = myNameTable.Add(@"DocumentFormat");
    //        id952_ValueTextBackColorValue = myNameTable.Add(@"ValueTextBackColorValue");
    //        id1035_PaperSizeName = myNameTable.Add(@"PaperSizeName");
    //        id909_BigVerticalGridLineColorValue = myNameTable.Add(@"BigVerticalGridLineColorValue");
    //        id263_LastPrintTime = myNameTable.Add(@"LastPrintTime");
    //        id36_ElementIDForEditableDependent = myNameTable.Add(@"ElementIDForEditableDependent");
    //        id362_XAccountingNumber = myNameTable.Add(@"XAccountingNumber");
    //        id280_NewPage = myNameTable.Add(@"NewPage");
    //        id349_XTextRadioBox = myNameTable.Add(@"XTextRadioBox");
    //        id202_BorderBottom = myNameTable.Add(@"BorderBottom");
    //        id615_AdditionShapeFixSize = myNameTable.Add(@"AdditionShapeFixSize");
    //        id167_BackgroundRepeat = myNameTable.Add(@"BackgroundRepeat");
    //        id398_AutoUpdate = myNameTable.Add(@"AutoUpdate");
    //        id124_PaperWidth = myNameTable.Add(@"PaperWidth");
    //        id386_DomExpression = myNameTable.Add(@"DomExpression");
    //        id864_CustomImage = myNameTable.Add(@"CustomImage");
    //        id652_HyperlinkInfo = myNameTable.Add(@"HyperlinkInfo");
    //        id750_SignRange = myNameTable.Add(@"SignRange");
    //        id230_DCGridLineInfo = myNameTable.Add(@"DCGridLineInfo");
    //        id822_SymbolType = myNameTable.Add(@"SymbolType");
    //        id679_TypeFullName = myNameTable.Add(@"TypeFullName");
    //        id214_Zoom = myNameTable.Add(@"Zoom");
    //        id337_XTextButton = myNameTable.Add(@"XTextButton");
    //        id582_AutoExitEditMode = myNameTable.Add(@"AutoExitEditMode");
    //        id411_MaxDecimalDigits = myNameTable.Add(@"MaxDecimalDigits");
    //        id100_SwapLeftRightMargin = myNameTable.Add(@"SwapLeftRightMargin");
    //        id161_BackgroundStyle = myNameTable.Add(@"BackgroundStyle");
    //        id645_SmoothZoomIn = myNameTable.Add(@"SmoothZoomIn");
    //        id111_PrinterName = myNameTable.Add(@"PrinterName");
    //        id76_MotherTemplate = myNameTable.Add(@"MotherTemplate");
    //        id542_DynamicListItems = myNameTable.Add(@"DynamicListItems");
    //        id248_Value = myNameTable.Add(@"Value");
    //        id855_UseAdvVerticalStyle = myNameTable.Add(@"UseAdvVerticalStyle");
    //        id1046_DivCharForMultiMode = myNameTable.Add(@"DivCharForMultiMode");
    //        id72_History = myNameTable.Add(@"History");
    //        id808_ChartLabelStyle = myNameTable.Add(@"ChartLabelStyle");
    //        id531_FieldSettings = myNameTable.Add(@"FieldSettings");
    //        id222_ParagraphOutlineLevel = myNameTable.Add(@"ParagraphOutlineLevel");
    //        id213_PaddingBottom = myNameTable.Add(@"PaddingBottom");
    //        id261_LastModifiedTime = myNameTable.Add(@"LastModifiedTime");
    //        id128_TopMargin = myNameTable.Add(@"TopMargin");
    //        id788_ChartCaptionStyle = myNameTable.Add(@"ChartCaptionStyle");
    //        id572_AutoSetFirstItems = myNameTable.Add(@"AutoSetFirstItems");
    //        id493_StartBorderText = myNameTable.Add(@"StartBorderText");
    //        id477_ShowCellNoneBorder = myNameTable.Add(@"ShowCellNoneBorder");
    //        id169_FontName = myNameTable.Add(@"FontName");
    //        id300_TitleLevel = myNameTable.Add(@"TitleLevel");
    //        id262_EditMinute = myNameTable.Add(@"EditMinute");
    //        id475_AllowUserToResizeColumns = myNameTable.Add(@"AllowUserToResizeColumns");
    //        id654_Requried = myNameTable.Add(@"Requried");
    //        id577_Target = myNameTable.Add(@"Target");
    //        id526_EditorActiveMode = myNameTable.Add(@"EditorActiveMode");
    //        id189_RightToLeft = myNameTable.Add(@"RightToLeft");
    //        id1019_UpAndDownTextType = myNameTable.Add(@"UpAndDownTextType");
    //        id933_LanternValueColorForUpValue = myNameTable.Add(@"LanternValueColorForUpValue");
    //        id978_NormalRangeBackColorValue = myNameTable.Add(@"NormalRangeBackColorValue");
    //        id803_DashCap = myNameTable.Add(@"DashCap");
    //        id777_GroupGridLine = myNameTable.Add(@"GroupGridLine");
    //        id737_ImageForMouseOver = myNameTable.Add(@"ImageForMouseOver");
    //        id133_XImageValue = myNameTable.Add(@"XImageValue");
    //        id984_ValuePointDataSourceInfo = myNameTable.Add(@"ValuePointDataSourceInfo");
    //        id888_Images = myNameTable.Add(@"Images");
    //        id841_DocumentData = myNameTable.Add(@"DocumentData");
    //        id34_ValueBinding = myNameTable.Add(@"ValueBinding");
    //        id473_AllowUserInsertRow = myNameTable.Add(@"AllowUserInsertRow");
    //        id142_Font = myNameTable.Add(@"Font");
    //        id567_UserFlag = myNameTable.Add(@"UserFlag");
    //        id977_ScaleRate = myNameTable.Add(@"ScaleRate");
    //        id608_EnableRepeatedImage = myNameTable.Add(@"EnableRepeatedImage");
    //        id883_TickUnit = myNameTable.Add(@"TickUnit");
    //        id540_RepulsionForGroup = myNameTable.Add(@"RepulsionForGroup");
    //        id249_DocumentInfo = myNameTable.Add(@"DocumentInfo");
    //        id192_RoundRadio = myNameTable.Add(@"RoundRadio");
    //        id198_BorderBottomColor = myNameTable.Add(@"BorderBottomColor");
    //        id787_YAxisCaptions = myNameTable.Add(@"YAxisCaptions");
    //        id702_KBEntries = myNameTable.Add(@"KBEntries");
    //        id744_SignMessage = myNameTable.Add(@"SignMessage");
    //        id878_EnableCustomValuePointSymbol = myNameTable.Add(@"EnableCustomValuePointSymbol");
    //        id218_Height = myNameTable.Add(@"Height");
    //        id960_TitleBackColorValue = myNameTable.Add(@"TitleBackColorValue");
    //        id402_Required = myNameTable.Add(@"Required");
    //        id274_UseLanguage2 = myNameTable.Add(@"UseLanguage2");
    //        id1030_GridYSpaceNum = myNameTable.Add(@"GridYSpaceNum");
    //        id846_TagValue = myNameTable.Add(@"TagValue");
    //        id107_PageBorderBackground = myNameTable.Add(@"PageBorderBackground");
    //        id807_MiterLimit = myNameTable.Add(@"MiterLimit");
    //        id12_EncryptContent = myNameTable.Add(@"EncryptContent");
    //        id843_ValuePoint = myNameTable.Add(@"ValuePoint");
    //        id155_Underline = myNameTable.Add(@"Underline");
    //        id522_CustomAdornText = myNameTable.Add(@"CustomAdornText");
    //        id484_GenerateByValueBingding = myNameTable.Add(@"GenerateByValueBingding");
    //        id238_Title = myNameTable.Add(@"Title");
    //        id644_ZoomInRate = myNameTable.Add(@"ZoomInRate");
    //        id281_BorderColorValue = myNameTable.Add(@"BorderColorValue");
    //        id164_BackgroundPosition = myNameTable.Add(@"BackgroundPosition");
    //        id1038_LineAlignment = myNameTable.Add(@"LineAlignment");
    //        id747_SignProviderName = myNameTable.Add(@"SignProviderName");
    //        id427_KeyFeildDataSourcePath = myNameTable.Add(@"KeyFeildDataSourcePath");
    //        id149_Angle = myNameTable.Add(@"Angle");
    //        id364_XInputField = myNameTable.Add(@"XInputField");
    //        id910_PageBackColorValue = myNameTable.Add(@"PageBackColorValue");
    //        id196_BorderTopColor = myNameTable.Add(@"BorderTopColor");
    //        id573_ValueFormater = myNameTable.Add(@"ValueFormater");
    //        id46_Expression = myNameTable.Add(@"Expression");
    //        id699_ListItemsSourceFormatString = myNameTable.Add(@"ListItemsSourceFormatString");
    //        id278_AllowSave = myNameTable.Add(@"AllowSave");
    //        id56_WebClientHtmlText = myNameTable.Add(@"WebClientHtmlText");
    //        id193_Rotate = myNameTable.Add(@"Rotate");
    //        id1012_StartDateKeyword = myNameTable.Add(@"StartDateKeyword");
    //        id962_TitleColorValue = myNameTable.Add(@"TitleColorValue");
    //        id348_XTextCheckBoxElementBase = myNameTable.Add(@"XTextCheckBoxElementBase");
    //        id757_DataFieldNameForGroupName = myNameTable.Add(@"DataFieldNameForGroupName");
    //        id131_Landscape = myNameTable.Add(@"Landscape");
    //        id205_BorderSpacing = myNameTable.Add(@"BorderSpacing");
    //        id29_PropertyExpressions = myNameTable.Add(@"PropertyExpressions");
    //        id1054_IsSelect = myNameTable.Add(@"IsSelect");
    //        id9_Attribute = myNameTable.Add(@"Attribute");
    //        id182_LineSpacing = myNameTable.Add(@"LineSpacing");
    //        id1018_ForceUpWhenPageFirstPoint = myNameTable.Add(@"ForceUpWhenPageFirstPoint");
    //        id199_BorderStyle = myNameTable.Add(@"BorderStyle");
    //        id495_BorderElementColor = myNameTable.Add(@"BorderElementColor");
    //        id166_BackgroundPositionY = myNameTable.Add(@"BackgroundPositionY");
    //        id165_BackgroundPositionX = myNameTable.Add(@"BackgroundPositionX");
    //        id833_DownleadLength = myNameTable.Add(@"DownleadLength");
    //        id453_ForeColorValueForCollapsed = myNameTable.Add(@"ForeColorValueForCollapsed");
    //        id357_XContentLinkField = myNameTable.Add(@"XContentLinkField");
    //        id293_Style = myNameTable.Add(@"Style");
    //        id1001_EnableEndTime = myNameTable.Add(@"EnableEndTime");
    //        id292_Styles = myNameTable.Add(@"Styles");
    //        id893_SpecifyEndDate = myNameTable.Add(@"SpecifyEndDate");
    //        id433_FieldsForDesign = myNameTable.Add(@"FieldsForDesign");
    //        id1020_ValueTextMultiLine = myNameTable.Add(@"ValueTextMultiLine");
    //        id534_ShowFormButton = myNameTable.Add(@"ShowFormButton");
    //        id220_PageBreakBefore = myNameTable.Add(@"PageBreakBefore");
    //        id754_ImageData = myNameTable.Add(@"ImageData");
    //        id987_FieldNameForTitle = myNameTable.Add(@"FieldNameForTitle");
    //        id23_BringoutToSave = myNameTable.Add(@"BringoutToSave");
    //        id943_HollowCovertTargetName = myNameTable.Add(@"HollowCovertTargetName");
    //        id42_EnablePermission = myNameTable.Add(@"EnablePermission");
    //        id751_SignUserID = myNameTable.Add(@"SignUserID");
    //        id525_EnableFieldTextColor = myNameTable.Add(@"EnableFieldTextColor");
    //        id604_LinkInfo = myNameTable.Add(@"LinkInfo");
    //        id836_PieDataItem = myNameTable.Add(@"PieDataItem");
    //        id510_SelectedSpellCode = myNameTable.Add(@"SelectedSpellCode");
    //        id423_TableName = myNameTable.Add(@"TableName");
    //        id223_VisibleInDirectory = myNameTable.Add(@"VisibleInDirectory");
    //        id596_ReplaceUpdateMode = myNameTable.Add(@"ReplaceUpdateMode");
    //        id548_ListSourceInfo = myNameTable.Add(@"ListSourceInfo");
    //        id824_PieDocumentStyle = myNameTable.Add(@"PieDocumentStyle");
    //        id850_SpecifyLanternSymbolStyle = myNameTable.Add(@"SpecifyLanternSymbolStyle");
    //        id593_ExpressionStyle = myNameTable.Add(@"ExpressionStyle");
    //        id122_FooterDistance = myNameTable.Add(@"FooterDistance");
    //        id409_MinValue = myNameTable.Add(@"MinValue");
    //        id462_ImportUserTrack = myNameTable.Add(@"ImportUserTrack");
    //        id663_AutoHeightForMultiline = myNameTable.Add(@"AutoHeightForMultiline");
    //        id980_NormalMaxValue = myNameTable.Add(@"NormalMaxValue");
    //        id438_TabStop = myNameTable.Add(@"TabStop");
    //        id287_ImportPageSettings = myNameTable.Add(@"ImportPageSettings");
    //        id341_XTextLabelElement = myNameTable.Add(@"XTextLabelElement");
    //        id955_ColorValueForPointValue = myNameTable.Add(@"ColorValueForPointValue");
    //        id800_XColorValue = myNameTable.Add(@"XColorValue");
    //        id554_ListItem = myNameTable.Add(@"ListItem");
    //        id327_XBookMark = myNameTable.Add(@"XBookMark");
    //        id739_CommandName = myNameTable.Add(@"CommandName");
    //        id170_FontSize = myNameTable.Add(@"FontSize");
    //        id96_GutterStyle = myNameTable.Add(@"GutterStyle");
    //        id550_DisplayPath = myNameTable.Add(@"DisplayPath");
    //        id405_MinLength = myNameTable.Add(@"MinLength");
    //        id177_SpacingAfterParagraph = myNameTable.Add(@"SpacingAfterParagraph");
    //        id545_ListValueSeparatorChar = myNameTable.Add(@"ListValueSeparatorChar");
    //        id1003_ValueDisplayFormat = myNameTable.Add(@"ValueDisplayFormat");
    //        id899_ShadowPointDetectSeconds = myNameTable.Add(@"ShadowPointDetectSeconds");
    //        id432_ParameterStyle = myNameTable.Add(@"ParameterStyle");
    //        id928_MergeIntoLeft = myNameTable.Add(@"MergeIntoLeft");
    //        id325_XTextTableColumn = myNameTable.Add(@"XTextTableColumn");
    //        id195_BorderLeftColor = myNameTable.Add(@"BorderLeftColor");
    //        id974_Scales = myNameTable.Add(@"Scales");
    //        id90_PageSettings = myNameTable.Add(@"PageSettings");
    //        id101_SpecifyDuplex = myNameTable.Add(@"SpecifyDuplex");
    //        id148_DensityForRepeat = myNameTable.Add(@"DensityForRepeat");
    //        id372_XTextFooterForFirstPage = myNameTable.Add(@"XTextFooterForFirstPage");
    //        id639_ShapeContainerElement = myNameTable.Add(@"ShapeContainerElement");
    //        id837_TemperatureDocument = myNameTable.Add(@"TemperatureDocument");
    //        id690_EnableMediaContextMenu = myNameTable.Add(@"EnableMediaContextMenu");
    //        id742_SignUserName = myNameTable.Add(@"SignUserName");
    //        id568_IsRoot = myNameTable.Add(@"IsRoot");
    //        id511_InnerValue = myNameTable.Add(@"InnerValue");
    //        id288_ImportHeader = myNameTable.Add(@"ImportHeader");
    //        id951_RedLineWidth = myNameTable.Add(@"RedLineWidth");
    //        id420_PropertyExpressionInfo = myNameTable.Add(@"PropertyExpressionInfo");
    //        id512_PrintBackgroundText = myNameTable.Add(@"PrintBackgroundText");
    //        id1052_RatioToPointFsList = myNameTable.Add(@"RatioToPointFsList");
    //        id463_DelayLoadWhenExpand = myNameTable.Add(@"DelayLoadWhenExpand");
    //        id115_EditTimeBackgroundImage = myNameTable.Add(@"EditTimeBackgroundImage");
    //        id426_KeyFieldValue = myNameTable.Add(@"KeyFieldValue");
    //        id781_Thickness = myNameTable.Add(@"Thickness");
    //        id872_EditValuePointMode = myNameTable.Add(@"EditValuePointMode");
    //        id378_SpecifyFixedLineHeight = myNameTable.Add(@"SpecifyFixedLineHeight");
    //        id844_VerifiedColorValue = myNameTable.Add(@"VerifiedColorValue");
    //        id17_LimitedInputChars = myNameTable.Add(@"LimitedInputChars");
    //        id1008_MaxValueForDayIndex = myNameTable.Add(@"MaxValueForDayIndex");
    //        id340_NewBarcode = myNameTable.Add(@"NewBarcode");
    //        id579_TargetPropertyName = myNameTable.Add(@"TargetPropertyName");
    //        id508_LabelText = myNameTable.Add(@"LabelText");
    //        id15_EmitDataSource = myNameTable.Add(@"EmitDataSource");
    //        id55_Deleteable = myNameTable.Add(@"Deleteable");
    //        id828_PieOpacity = myNameTable.Add(@"PieOpacity");
    //        id418_CustomMessage = myNameTable.Add(@"CustomMessage");
    //        id656_PrintVisibilityWhenUnchecked = myNameTable.Add(@"PrintVisibilityWhenUnchecked");
    //        id802_DashStyle = myNameTable.Add(@"DashStyle");
    //        id466_NumOfColumns = myNameTable.Add(@"NumOfColumns");
    //        id613_SaveContentInFile = myNameTable.Add(@"SaveContentInFile");
    //        id436_DCEmitDataSourceFieldInfo = myNameTable.Add(@"DCEmitDataSourceFieldInfo");
    //        id1062_WhitespaceCount = myNameTable.Add(@"WhitespaceCount");
    //        id871_DebugMode = myNameTable.Add(@"DebugMode");
    //        id1056_ImgBase64ForCustomFill = myNameTable.Add(@"ImgBase64ForCustomFill");
    //        id468_DataForReValueBinding = myNameTable.Add(@"DataForReValueBinding");
    //        id906_PageIndexFont = myNameTable.Add(@"PageIndexFont");
    //        id98_EnableHeaderFooter = myNameTable.Add(@"EnableHeaderFooter");
    //        id760_DataFieldNameForLink = myNameTable.Add(@"DataFieldNameForLink");
    //        id942_LanternValueTitle = myNameTable.Add(@"LanternValueTitle");
    //        id691_PlayerUIMode = myNameTable.Add(@"PlayerUIMode");
    //        id64_Parameters = myNameTable.Add(@"Parameters");
    //        id817_GridStep = myNameTable.Add(@"GridStep");
    //        id574_NoneText = myNameTable.Add(@"NoneText");
    //        id137_ImageDataBase64String = myNameTable.Add(@"ImageDataBase64String");
    //        id66_DetectRepeatImageForSave = myNameTable.Add(@"DetectRepeatImageForSave");
    //        id990_FieldNameForLanternValue = myNameTable.Add(@"FieldNameForLanternValue");
    //        id677_DelayLoadControl = myNameTable.Add(@"DelayLoadControl");
    //        id264_AuthorName = myNameTable.Add(@"AuthorName");
    //        id578_CustomTargetName = myNameTable.Add(@"CustomTargetName");
    //        id1011_StartDate = myNameTable.Add(@"StartDate");
    //        id1013_PreserveStartKeywordOrder = myNameTable.Add(@"PreserveStartKeywordOrder");
    //        id407_CheckMinValue = myNameTable.Add(@"CheckMinValue");
    //        id320_DefaultValue = myNameTable.Add(@"DefaultValue");
    //        id332_XTemperatureChart = myNameTable.Add(@"XTemperatureChart");
    //        id558_Text2 = myNameTable.Add(@"Text2");
    //        id882_AllowUserCollapseZone = myNameTable.Add(@"AllowUserCollapseZone");
    //        id931_ValuePrecision = myNameTable.Add(@"ValuePrecision");
    //        id630_DefaultFont = myNameTable.Add(@"DefaultFont");
    //        id821_TipText = myNameTable.Add(@"TipText");
    //        id589_ShowText = myNameTable.Add(@"ShowText");
    //        id1021_HeaderLabelInfo = myNameTable.Add(@"HeaderLabelInfo");
    //        id815_LeftSide = myNameTable.Add(@"LeftSide");
    //        id2_Item = myNameTable.Add(string.Empty);
    //        id393_FormatString = myNameTable.Add(@"FormatString");
    //        id414_Range = myNameTable.Add(@"Range");
    //        id226_BorderRange = myNameTable.Add(@"BorderRange");
    //        id536_InputFieldSettings = myNameTable.Add(@"InputFieldSettings");
    //        id265_AuthorPermissionLevel = myNameTable.Add(@"AuthorPermissionLevel");
    //        id908_BigVerticalGridLineWidth = myNameTable.Add(@"BigVerticalGridLineWidth");
    //        id712_ParentID = myNameTable.Add(@"ParentID");
    //        id252_ShowHeaderBottomLine = myNameTable.Add(@"ShowHeaderBottomLine");
    //        id901_Zones = myNameTable.Add(@"Zones");
    //        id224_ParagraphListStyle = myNameTable.Add(@"ParagraphListStyle");
    //        id160_BackgroundColor2 = myNameTable.Add(@"BackgroundColor2");
    //        id437_EnabledTransprentEncrypt = myNameTable.Add(@"EnabledTransprentEncrypt");
    //        id535_FormButtonStyle = myNameTable.Add(@"FormButtonStyle");
    //        id85_Comments = myNameTable.Add(@"Comments");
    //        id633_ShapeLineElement = myNameTable.Add(@"ShapeLineElement");
    //        id587_BarcodeStyle = myNameTable.Add(@"BarcodeStyle");
    //        id618_SmoothZoom = myNameTable.Add(@"SmoothZoom");
    //        id949_AlertLineColorValue = myNameTable.Add(@"AlertLineColorValue");
    //        id994_VisibleWhenNoValuePoint = myNameTable.Add(@"VisibleWhenNoValuePoint");
    //        id168_Color = myNameTable.Add(@"Color");
    //        id467_AllowReBindingDataSource = myNameTable.Add(@"AllowReBindingDataSource");
    //        id245_ForeColor = myNameTable.Add(@"ForeColor");
    //        id359_XTextShapeInputFieldElement = myNameTable.Add(@"XTextShapeInputFieldElement");
    //        id661_CaptionFlowLayout = myNameTable.Add(@"CaptionFlowLayout");
    //        id623_Resizeable = myNameTable.Add(@"Resizeable");
    //        id713_EntryTemplateContent = myNameTable.Add(@"EntryTemplateContent");
    //        id766_TextColorValue = myNameTable.Add(@"TextColorValue");
    //        id366_XTextTable = myNameTable.Add(@"XTextTable");
    //        id474_Item = myNameTable.Add(@"AllowUserToResizeEvenInFormViewMode");
    //        id1039_Item = myNameTable.Add(@"PositionFixModeForAutoHeightLine");
    //        id726_Item = myNameTable.Add(@"AllowUserEditCurrentPageLabelText");
    //        id439_BorderPrintedWhenJumpPrint = myNameTable.Add(@"BorderPrintedWhenJumpPrint");
    //        id1036_DCTimeLineLabel = myNameTable.Add(@"DCTimeLineLabel");
    //        id311_SavedTime = myNameTable.Add(@"SavedTime");
    //        id486_PrintCellBackground = myNameTable.Add(@"PrintCellBackground");
    //        id479_SubfieldMode = myNameTable.Add(@"SubfieldMode");
    //        id944_ShadowName = myNameTable.Add(@"ShadowName");
    //        id244_BackColor = myNameTable.Add(@"BackColor");
    //        id1000_ExtendGridLineType = myNameTable.Add(@"ExtendGridLineType");
    //        id247_XAttribute = myNameTable.Add(@"XAttribute");
    //        id480_SubfieldNumber = myNameTable.Add(@"SubfieldNumber");
    //        id553_BufferItems = myNameTable.Add(@"BufferItems");
    //        id922_HeaderLines = myNameTable.Add(@"HeaderLines");
    //        id400_Level = myNameTable.Add(@"Level");
    //        id932_AllowInterrupt = myNameTable.Add(@"AllowInterrupt");
    //        id421_AllowChainReaction = myNameTable.Add(@"AllowChainReaction");
    //        id184_Align = myNameTable.Add(@"Align");
    //        id743_SignClientName = myNameTable.Add(@"SignClientName");
    //        id360_XMedicalExpressionField = myNameTable.Add(@"XMedicalExpressionField");
    //        id156_Strikeout = myNameTable.Add(@"Strikeout");
    //        id179_LayoutGridHeight = myNameTable.Add(@"LayoutGridHeight");
    //        id761_DataFieldNameForTipText = myNameTable.Add(@"DataFieldNameForTipText");
    //        id610_Alt = myNameTable.Add(@"Alt");
    //        id780_AxisCompress = myNameTable.Add(@"AxisCompress");
    //        id457_SpecifyHeight = myNameTable.Add(@"SpecifyHeight");
    //        id283_PageIndex = myNameTable.Add(@"PageIndex");
    //        id735_PrintAsText = myNameTable.Add(@"PrintAsText");
    //        id628_AutoZoomFontSize = myNameTable.Add(@"AutoZoomFontSize");
    //        id294_DocumentContentStyle = myNameTable.Add(@"DocumentContentStyle");
    //        id979_OutofNormalRangeBackColorValue = myNameTable.Add(@"OutofNormalRangeBackColorValue");
    //        id555_EntryID = myNameTable.Add(@"EntryID");
    //        id102_PowerDocumentGridLine = myNameTable.Add(@"PowerDocumentGridLine");
    //        id716_OwnerLevel = myNameTable.Add(@"OwnerLevel");
    //        id877_TitleForToolTip = myNameTable.Add(@"TitleForToolTip");
    //        id313_ClientName = myNameTable.Add(@"ClientName");
    //        id26_AcceptTab = myNameTable.Add(@"AcceptTab");
    //        id375_XTextHeader = myNameTable.Add(@"XTextHeader");
    //        id371_XTextDocumentContentElement = myNameTable.Add(@"XTextDocumentContentElement");
    //        id886_SQLTextForHeaderLabel = myNameTable.Add(@"SQLTextForHeaderLabel");
    //        id1009_CircleText = myNameTable.Add(@"CircleText");
    //        id434_Field = myNameTable.Add(@"Field");
    //        id369_XTextSubDocument = myNameTable.Add(@"XTextSubDocument");
    //        id16_AutoFixTextMode = myNameTable.Add(@"AutoFixTextMode");
    //        id627_TextBackColorString = myNameTable.Add(@"TextBackColorString");
    //        id31_EnableValueValidate = myNameTable.Add(@"EnableValueValidate");
    //        id505_DefaultEventExpression = myNameTable.Add(@"DefaultEventExpression");
    //        id646_X1 = myNameTable.Add(@"X1");
    //        id647_Y1 = myNameTable.Add(@"Y1");
    //        id543_ListSource = myNameTable.Add(@"ListSource");
    //        id784_HorizontalTextAlign = myNameTable.Add(@"HorizontalTextAlign");
    //        id502_FastInputMode = myNameTable.Add(@"FastInputMode");
    //        id35_DefaultValueForValueBinding = myNameTable.Add(@"DefaultValueForValueBinding");
    //        id528_DefaultSelectedIndexs = myNameTable.Add(@"DefaultSelectedIndexs");
    //        id152_Unit = myNameTable.Add(@"Unit");
    //        id159_BackgroundColor = myNameTable.Add(@"BackgroundColor");
    //        id1045_IsIdentyPartition = myNameTable.Add(@"IsIdentyPartition");
    //        id344_HorizontalLine = myNameTable.Add(@"HorizontalLine");
    //        id352_XTextEOFElement = myNameTable.Add(@"XTextEOFElement");
    //        id1033_ThinLineWidth = myNameTable.Add(@"ThinLineWidth");
    //        id919_SpecifyTitleHeight = myNameTable.Add(@"SpecifyTitleHeight");
    //        id830_DrawingStyle = myNameTable.Add(@"DrawingStyle");
    //        id603_ZOrderStyle = myNameTable.Add(@"ZOrderStyle");
    //        id286_Format = myNameTable.Add(@"Format");
    //        id68_FileName = myNameTable.Add(@"FileName");
    //        id442_AutoFixFontSize = myNameTable.Add(@"AutoFixFontSize");
    //        id81_SpecialTag = myNameTable.Add(@"SpecialTag");
    //        id916_DateFormatStringForCrossWeek = myNameTable.Add(@"DateFormatStringForCrossWeek");
    //        id63_SerializeParameterValue = myNameTable.Add(@"SerializeParameterValue");
    //        id773_CustomColorThemeH = myNameTable.Add(@"CustomColorThemeH");
    //        id706_KBTemplateDocument = myNameTable.Add(@"KBTemplateDocument");
    //        id186_FirstLineIndent = myNameTable.Add(@"FirstLineIndent");
    //        id818_TickTextList = myNameTable.Add(@"TickTextList");
    //        id793_BarBorderPen = myNameTable.Add(@"BarBorderPen");
    //        id600_ResetListIndexFlag = myNameTable.Add(@"ResetListIndexFlag");
    //        id820_SeriesName = myNameTable.Add(@"SeriesName");
    //        id518_ShowInputFieldStateTag = myNameTable.Add(@"ShowInputFieldStateTag");
    //        id225_DefaultValuePropertyNames = myNameTable.Add(@"DefaultValuePropertyNames");
    //        id272_Operator = myNameTable.Add(@"Operator");
    //        id494_EndBorderText = myNameTable.Add(@"EndBorderText");
    //        id585_UnitMode = myNameTable.Add(@"UnitMode");
    //        id326_XString = myNameTable.Add(@"XString");
    //        id576_EventName = myNameTable.Add(@"EventName");
    //        id688_CsMediaPlayer = myNameTable.Add(@"CsMediaPlayer");
    //        id80_GlobalJavaScript = myNameTable.Add(@"GlobalJavaScript");
    //        id197_BorderRightColor = myNameTable.Add(@"BorderRightColor");
    //        id839_Datas = myNameTable.Add(@"Datas");
    //        id209_MarginBottom = myNameTable.Add(@"MarginBottom");

    //    }
    //}

    //public class WriterTemperatureDocumentXmlSerialization : System.Xml.Serialization.XmlSerializationWriter
    //{
    //    protected override void InitCallbacks()
    //    {

    //    }
    //    private System.Xml.XmlWriter _BaseWriter = null;
    //    private void MyWriteElementString(string localName, string value)
    //    {

    //        if (value != null && value.Length > 0)
    //        {
    //            this._BaseWriter.WriteElementString(localName, null, value);
    //        }
    //    }
    //    private void MyWriteElementStringRaw(string localName, string Value)
    //    {
    //        if (Value != null && Value.Length > 0)
    //        {
    //            this._BaseWriter.WriteStartElement(localName, null);
    //            this._BaseWriter.WriteRaw(Value);
    //            this._BaseWriter.WriteEndElement();
    //        }
    //    }

    //    internal static System.Exception CreateInvalidEnumValueException(long v, System.Type t)
    //    {
    //        return new System.InvalidOperationException(t.FullName + "不含数值" + v);
    //    }

    //    string Write119_StringAlignment(global::System.Drawing.StringAlignment v)
    //    {
    //        string s = null;
    //        switch (v)
    //        {
    //            case global::System.Drawing.StringAlignment.@Near: s = @"Near"; break;
    //            case global::System.Drawing.StringAlignment.@Center: s = @"Center"; break;
    //            case global::System.Drawing.StringAlignment.@Far: s = @"Far"; break;
    //            default: throw CreateInvalidEnumValueException((long)v, typeof(System.Drawing.StringAlignment));
    //        }
    //        return s;
    //    }
    //    string Write43_DashStyle(global::System.Drawing.Drawing2D.DashStyle v)
    //    {
    //        string s = null;
    //        switch (v)
    //        {
    //            case global::System.Drawing.Drawing2D.DashStyle.@Solid: s = @"Solid"; break;
    //            case global::System.Drawing.Drawing2D.DashStyle.@Dash: s = @"Dash"; break;
    //            case global::System.Drawing.Drawing2D.DashStyle.@Dot: s = @"Dot"; break;
    //            case global::System.Drawing.Drawing2D.DashStyle.@DashDot: s = @"DashDot"; break;
    //            case global::System.Drawing.Drawing2D.DashStyle.@DashDotDot: s = @"DashDotDot"; break;
    //            case global::System.Drawing.Drawing2D.DashStyle.@Custom: s = @"Custom"; break;
    //            default: throw CreateInvalidEnumValueException((long)v, typeof(System.Drawing.Drawing2D.DashStyle));
    //        }
    //        return s;
    //    }
    //    internal protected void Write192_XPenStyle(string n, string ns, global::DCSoft.Drawing.XPenStyle o, bool isNullable, bool needType)
    //    {
    //        this._BaseWriter = base.Writer;
    //        var localWriter = base.Writer;
    //        if (o == null)
    //        {
    //            //if (isNullable) WriteNullTagLiteral(n, ns);
    //            return;
    //        }
    //        if (!needType)
    //        {
    //            System.Type t = o.GetType();
    //            if (t == typeof(global::DCSoft.Drawing.XPenStyle))
    //            {
    //            }
    //            else
    //            {
    //                throw CreateUnknownTypeException(o);
    //            }
    //        }
    //        WriteStartElement(n, ns, o, false, null);
    //        if (needType) WriteXsiType(@"XPenStyle", string.Empty);
    //        MyWriteElementString(@"Color", (o.@ColorString));
    //        if ((o.@Width) != 1f)
    //        {
    //            MyWriteElementStringRaw(@"Width", DCXMLConvert.ToString((o.@Width)));
    //        }
    //        if ((o.@DashStyle) != global::System.Drawing.Drawing2D.DashStyle.@Solid)
    //        {
    //            localWriter.WriteElementString(@"DashStyle", null, Write43_DashStyle((o.@DashStyle)));
    //        }
    //        if ((o.@DashCap) != global::System.Drawing.Drawing2D.DashCap.@Flat)
    //        {
    //            localWriter.WriteElementString(@"DashCap", null, Write188_DashCap((o.@DashCap)));
    //        }
    //        if ((o.@LineJoin) != global::System.Drawing.Drawing2D.LineJoin.@Bevel)
    //        {
    //            localWriter.WriteElementString(@"LineJoin", null, Write189_LineJoin((o.@LineJoin)));
    //        }
    //        if ((o.@StartCap) != global::System.Drawing.Drawing2D.LineCap.@Flat)
    //        {
    //            localWriter.WriteElementString(@"StartCap", null, Write190_LineCap((o.@StartCap)));
    //        }
    //        if ((o.@EndCap) != global::System.Drawing.Drawing2D.LineCap.@Flat)
    //        {
    //            localWriter.WriteElementString(@"EndCap", null, Write190_LineCap((o.@EndCap)));
    //        }
    //        if ((o.@MiterLimit) != 10f)
    //        {
    //            MyWriteElementStringRaw(@"MiterLimit", DCXMLConvert.ToString((o.@MiterLimit)));
    //        }
    //        if ((o.@Alignment) != global::System.Drawing.Drawing2D.PenAlignment.@Center)
    //        {
    //            localWriter.WriteElementString(@"Alignment", null, Write191_PenAlignment((o.@Alignment)));
    //        }
    //        WriteEndElement(o);
    //    }
    //    string Write188_DashCap(global::System.Drawing.Drawing2D.DashCap v)
    //    {
    //        string s = null;
    //        switch (v)
    //        {
    //            case global::System.Drawing.Drawing2D.DashCap.@Flat: s = @"Flat"; break;
    //            case global::System.Drawing.Drawing2D.DashCap.@Round: s = @"Round"; break;
    //            case global::System.Drawing.Drawing2D.DashCap.@Triangle: s = @"Triangle"; break;
    //            default: throw CreateInvalidEnumValueException((long)v, typeof(System.Drawing.Drawing2D.DashCap));
    //        }
    //        return s;
    //    }
    //    string Write190_LineCap(global::System.Drawing.Drawing2D.LineCap v)
    //    {
    //        string s = null;
    //        switch (v)
    //        {
    //            case global::System.Drawing.Drawing2D.LineCap.@Flat: s = @"Flat"; break;
    //            case global::System.Drawing.Drawing2D.LineCap.@Square: s = @"Square"; break;
    //            case global::System.Drawing.Drawing2D.LineCap.@Round: s = @"Round"; break;
    //            case global::System.Drawing.Drawing2D.LineCap.@Triangle: s = @"Triangle"; break;
    //            case global::System.Drawing.Drawing2D.LineCap.@NoAnchor: s = @"NoAnchor"; break;
    //            case global::System.Drawing.Drawing2D.LineCap.@SquareAnchor: s = @"SquareAnchor"; break;
    //            case global::System.Drawing.Drawing2D.LineCap.@RoundAnchor: s = @"RoundAnchor"; break;
    //            case global::System.Drawing.Drawing2D.LineCap.@DiamondAnchor: s = @"DiamondAnchor"; break;
    //            case global::System.Drawing.Drawing2D.LineCap.@ArrowAnchor: s = @"ArrowAnchor"; break;
    //            case global::System.Drawing.Drawing2D.LineCap.@Custom: s = @"Custom"; break;
    //            case global::System.Drawing.Drawing2D.LineCap.@AnchorMask: s = @"AnchorMask"; break;
    //            default: throw CreateInvalidEnumValueException((long)v, typeof(System.Drawing.Drawing2D.LineCap));
    //        }
    //        return s;
    //    }
    //    string Write189_LineJoin(global::System.Drawing.Drawing2D.LineJoin v)
    //    {
    //        string s = null;
    //        switch (v)
    //        {
    //            case global::System.Drawing.Drawing2D.LineJoin.@Miter: s = @"Miter"; break;
    //            case global::System.Drawing.Drawing2D.LineJoin.@Bevel: s = @"Bevel"; break;
    //            case global::System.Drawing.Drawing2D.LineJoin.@Round: s = @"Round"; break;
    //            case global::System.Drawing.Drawing2D.LineJoin.@MiterClipped: s = @"MiterClipped"; break;
    //            default: throw CreateInvalidEnumValueException((long)v, typeof(System.Drawing.Drawing2D.LineJoin));
    //        }
    //        return s;
    //    }
    //    string Write191_PenAlignment(global::System.Drawing.Drawing2D.PenAlignment v)
    //    {
    //        string s = null;
    //        switch (v)
    //        {
    //            case global::System.Drawing.Drawing2D.PenAlignment.@Center: s = @"Center"; break;
    //            case global::System.Drawing.Drawing2D.PenAlignment.@Inset: s = @"Inset"; break;
    //            case global::System.Drawing.Drawing2D.PenAlignment.@Outset: s = @"Outset"; break;
    //            case global::System.Drawing.Drawing2D.PenAlignment.@Left: s = @"Left"; break;
    //            case global::System.Drawing.Drawing2D.PenAlignment.@Right: s = @"Right"; break;
    //            default: throw CreateInvalidEnumValueException((long)v, typeof(System.Drawing.Drawing2D.PenAlignment));
    //        }
    //        return s;
    //    }
    //    internal protected void Write114_Color(string n, string ns, global::System.Drawing.Color o, bool needType)
    //    {
    //        this._BaseWriter = base.Writer;
    //        var localWriter = base.Writer;
    //        if (!needType)
    //        {
    //            System.Type t = o.GetType();
    //            if (t == typeof(global::System.Drawing.Color))
    //            {
    //            }
    //            else
    //            {
    //                throw CreateUnknownTypeException(o);
    //            }
    //        }
    //        WriteStartElement(n, ns, o, false, null);
    //        if (needType) WriteXsiType(@"Color", string.Empty);
    //        WriteEndElement(o);
    //    }
    //    string Write60_GraphicsUnit(global::System.Drawing.GraphicsUnit v)
    //    {
    //        string s = null;
    //        switch (v)
    //        {
    //            case global::System.Drawing.GraphicsUnit.@World: s = @"World"; break;
    //            case global::System.Drawing.GraphicsUnit.@Display: s = @"Display"; break;
    //            case global::System.Drawing.GraphicsUnit.@Pixel: s = @"Pixel"; break;
    //            case global::System.Drawing.GraphicsUnit.@Point: s = @"Point"; break;
    //            case global::System.Drawing.GraphicsUnit.@Inch: s = @"Inch"; break;
    //            case global::System.Drawing.GraphicsUnit.@Document: s = @"Document"; break;
    //            case global::System.Drawing.GraphicsUnit.@Millimeter: s = @"Millimeter"; break;
    //            default: throw CreateInvalidEnumValueException((long)v, typeof(System.Drawing.GraphicsUnit));
    //        }
    //        return s;
    //    }
    //    internal protected void Write34_XImageValue(string n, string ns, global::DCSoft.Drawing.XImageValue o, bool isNullable, bool needType)
    //    {
    //        this._BaseWriter = base.Writer;
    //        var localWriter = base.Writer;
    //        if (o == null)
    //        {
    //            //if (isNullable) WriteNullTagLiteral(n, ns);
    //            return;
    //        }
    //        if (!needType)
    //        {
    //            System.Type t = o.GetType();
    //            if (t == typeof(global::DCSoft.Drawing.XImageValue))
    //            {
    //            }
    //            else
    //            {
    //                throw CreateUnknownTypeException(o);
    //            }
    //        }
    //        WriteStartElement(n, ns, o, false, null);
    //        if (needType) WriteXsiType(@"XImageValue", string.Empty);
    //        if ((o.@HorizontalResolution) != 0f)
    //        {
    //            MyWriteElementStringRaw(@"HorizontalResolution", DCXMLConvert.ToString((o.@HorizontalResolution)));
    //        }
    //        if ((o.@VerticalResolution) != 0f)
    //        {
    //            MyWriteElementStringRaw(@"VerticalResolution", DCXMLConvert.ToString((o.@VerticalResolution)));
    //        }
    //        MyWriteElementString(@"ImageDataBase64String", (o.@ImageDataBase64String));
    //        WriteEndElement(o);
    //    }
    //    internal protected void Write64_XFontValue(string n, string ns, global::DCSoft.Drawing.XFontValue o, bool isNullable, bool needType)
    //    {
    //        this._BaseWriter = base.Writer;
    //        var localWriter = base.Writer;
    //        if (o == null)
    //        {
    //            //if (isNullable) WriteNullTagLiteral(n, ns);
    //            return;
    //        }
    //        if (!needType)
    //        {
    //            System.Type t = o.GetType();
    //            if (t == typeof(global::DCSoft.Drawing.XFontValue))
    //            {
    //            }
    //            else
    //            {
    //                throw CreateUnknownTypeException(o);
    //            }
    //        }
    //        WriteStartElement(n, ns, o, false, null);
    //        if (needType) WriteXsiType(@"XFontValue", string.Empty);
    //        if ((o.@Name) != @"宋体")
    //        {
    //            MyWriteElementString(@"Name", (o.@Name));
    //        }
    //        if ((o.@Size) != 9f)
    //        {
    //            MyWriteElementStringRaw(@"Size", DCXMLConvert.ToString((o.@Size)));
    //        }
    //        if ((o.@Unit) != global::System.Drawing.GraphicsUnit.@Point)
    //        {
    //            localWriter.WriteElementString(@"Unit", null, Write60_GraphicsUnit((o.@Unit)));
    //        }
    //        if ((o.@Bold) != false)
    //        {
    //            MyWriteElementStringRaw(@"Bold", DCXMLConvert.ToString((o.@Bold)));
    //        }
    //        if ((o.@Italic) != false)
    //        {
    //            MyWriteElementStringRaw(@"Italic", DCXMLConvert.ToString((o.@Italic)));
    //        }
    //        if ((o.@Underline) != false)
    //        {
    //            MyWriteElementStringRaw(@"Underline", DCXMLConvert.ToString((o.@Underline)));
    //        }
    //        if ((o.@Strikeout) != false)
    //        {
    //            MyWriteElementStringRaw(@"Strikeout", DCXMLConvert.ToString((o.@Strikeout)));
    //        }
    //        WriteEndElement(o);
    //    }
    //    public void Write_TemperatureDocument(object o)
    //    {
    //        this._BaseWriter = base.Writer;
    //        var localWriter = base.Writer;
    //        WriteStartDocument();
    //        if (o == null)
    //        {
    //            WriteNullTagLiteral(@"TemperatureDocument", string.Empty);
    //            return;
    //        }
    //        TopLevelElement();
    //        Write243_TemperatureDocument(@"TemperatureDocument", string.Empty, ((global::DCSoft.TemperatureChart.TemperatureDocument)o), true, false);
    //    }
    //    internal protected void Write243_TemperatureDocument(string n, string ns, global::DCSoft.TemperatureChart.TemperatureDocument o, bool isNullable, bool needType)
    //    {
    //        this._BaseWriter = base.Writer;
    //        var localWriter = base.Writer;
    //        if (o == null)
    //        {
    //            //if (isNullable) WriteNullTagLiteral(n, ns);
    //            return;
    //        }
    //        if (!needType)
    //        {
    //            System.Type t = o.GetType();
    //            if (t == typeof(global::DCSoft.TemperatureChart.TemperatureDocument))
    //            {
    //            }
    //            else
    //            {
    //                throw CreateUnknownTypeException(o);
    //            }
    //        }
    //        WriteStartElement(n, ns, o, false, null);
    //        if (needType) WriteXsiType(@"TemperatureDocument", string.Empty);
    //        Write237_TemperatureDocumentConfig(@"Config", string.Empty, (o.@Config), false, false);
    //        {
    //            global::DCSoft.TemperatureChart.DCTimeLineParameterList a = (global::DCSoft.TemperatureChart.DCTimeLineParameterList)(o.@Parameters);
    //            if (a != null && a.Count > 0)
    //            {
    //                var aCount = a.Count;//2222222
    //                localWriter.WriteStartElement(null, @"Parameters", null);
    //                for (int ia = 0; ia < aCount; ia++)
    //                {
    //                    Write238_DCTimeLineParameter(@"Parameter", string.Empty, (a[ia]), true, false);
    //                }
    //                localWriter.WriteEndElement();
    //            }
    //        }
    //        {
    //            global::DCSoft.TemperatureChart.DocumentDataList a = (global::DCSoft.TemperatureChart.DocumentDataList)(o.@Datas);
    //            if (a != null && a.Count > 0)
    //            {
    //                var aCount = a.Count;//2222222
    //                localWriter.WriteStartElement(null, @"Datas", null);
    //                for (int ia = 0; ia < aCount; ia++)
    //                {
    //                    Write242_DocumentData(@"Data", string.Empty, (a[ia]), true, false);
    //                }
    //                localWriter.WriteEndElement();
    //            }
    //        }
    //        WriteEndElement(o);
    //    }
    //    internal protected void Write242_DocumentData(string n, string ns, global::DCSoft.TemperatureChart.DocumentData o, bool isNullable, bool needType)
    //    {
    //        this._BaseWriter = base.Writer;
    //        var localWriter = base.Writer;
    //        if (o == null)
    //        {
    //            //if (isNullable) WriteNullTagLiteral(n, ns);
    //            return;
    //        }
    //        if (!needType)
    //        {
    //            System.Type t = o.GetType();
    //            if (t == typeof(global::DCSoft.TemperatureChart.DocumentData))
    //            {
    //            }
    //            else
    //            {
    //                throw CreateUnknownTypeException(o);
    //            }
    //        }
    //        WriteStartElement(n, ns, o, false, null);
    //        if (needType) WriteXsiType(@"DocumentData", string.Empty);
    //        WriteAttribute(@"Name", string.Empty, (o.@Name));
    //        {
    //            global::DCSoft.TemperatureChart.ValuePointList a = (global::DCSoft.TemperatureChart.ValuePointList)(o.@Values);
    //            if (a != null && a.Count > 0)
    //            {
    //                var aCount = a.Count;//2222222
    //                localWriter.WriteStartElement(null, @"Values", null);
    //                for (int ia = 0; ia < aCount; ia++)
    //                {
    //                    Write241_ValuePoint(@"Value", string.Empty, (a[ia]), true, false);
    //                }
    //                localWriter.WriteEndElement();
    //            }
    //        }
    //        WriteEndElement(o);
    //    }
    //    internal protected void Write241_ValuePoint(string n, string ns, global::DCSoft.TemperatureChart.ValuePoint o, bool isNullable, bool needType)
    //    {
    //        this._BaseWriter = base.Writer;
    //        var localWriter = base.Writer;
    //        if (o == null)
    //        {
    //            //if (isNullable) WriteNullTagLiteral(n, ns);
    //            return;
    //        }
    //        if (!needType)
    //        {
    //            System.Type t = o.GetType();
    //            if (t == typeof(global::DCSoft.TemperatureChart.ValuePoint))
    //            {
    //            }
    //            else
    //            {
    //                throw CreateUnknownTypeException(o);
    //            }
    //        }
    //        WriteStartElement(n, ns, o, false, null);
    //        if (needType) WriteXsiType(@"ValuePoint", string.Empty);
    //        if ((o.@VerifiedColorValue) != @"black")
    //        {
    //            WriteAttribute(@"VerifiedColorValue", string.Empty, (o.@VerifiedColorValue));
    //        }
    //        if ((o.@VerifiedAlignment) != global::System.Drawing.StringAlignment.@Center)
    //        {
    //            localWriter.WriteAttributeString(@"VerifiedAlignment", null, Write119_StringAlignment((o.@VerifiedAlignment)));
    //        }
    //        WriteAttribute(@"TagValue", string.Empty, (o.@TagValue));
    //        WriteAttribute(@"ID", string.Empty, (o.@ID));
    //        if ((o.@Superscript) != false)
    //        {
    //            localWriter.WriteAttributeString(@"Superscript", null, DCXMLConvert.ToString((o.@Superscript)));
    //        }
    //        if ((o.@SpecifySymbolStyle) != global::DCSoft.TemperatureChart.ValuePointSymbolStyle.@Default)
    //        {
    //            localWriter.WriteAttributeString(@"SpecifySymbolStyle", null, Write235_ValuePointSymbolStyle((o.@SpecifySymbolStyle)));
    //        }
    //        if ((o.@SymbolOffsetX) != 0f)
    //        {
    //            localWriter.WriteAttributeString(@"SymbolOffsetX", null, DCXMLConvert.ToString((o.@SymbolOffsetX)));
    //        }
    //        if ((o.@SymbolOffsetY) != 0f)
    //        {
    //            localWriter.WriteAttributeString(@"SymbolOffsetY", null, DCXMLConvert.ToString((o.@SymbolOffsetY)));
    //        }
    //        if ((o.@SpecifyLanternSymbolStyle) != global::DCSoft.TemperatureChart.ValuePointSymbolStyle.@HollowCicle)
    //        {
    //            localWriter.WriteAttributeString(@"SpecifyLanternSymbolStyle", null, Write235_ValuePointSymbolStyle((o.@SpecifyLanternSymbolStyle)));
    //        }
    //        if ((o.@IntCharLantern) != 82)
    //        {
    //            localWriter.WriteAttributeString(@"IntCharLantern", null, DCXMLConvert.ToString((o.@IntCharLantern)));
    //        }
    //        if ((o.@IntCharSymbol) != 82)
    //        {
    //            localWriter.WriteAttributeString(@"IntCharSymbol", null, DCXMLConvert.ToString((o.@IntCharSymbol)));
    //        }
    //        WriteAttribute(@"Link", string.Empty, (o.@Link));
    //        WriteAttribute(@"LinkTarget", string.Empty, (o.@LinkTarget));
    //        WriteAttribute(@"Title", string.Empty, (o.@Title));
    //        if ((o.@VerticalLine) != global::DCSoft.TemperatureChart.DCTimeLineBooleanValue.@Inherit)
    //        {
    //            localWriter.WriteAttributeString(@"VerticalLine", null, Write239_DCTimeLineBooleanValue((o.@VerticalLine)));
    //        }
    //        if ((o.@UseAdvVerticalStyle) != false)
    //        {
    //            localWriter.WriteAttributeString(@"UseAdvVerticalStyle", null, DCXMLConvert.ToString((o.@UseAdvVerticalStyle)));
    //        }
    //        if ((o.@UseAdvVerticalStyle2) != false)
    //        {
    //            localWriter.WriteAttributeString(@"UseAdvVerticalStyle2", null, DCXMLConvert.ToString((o.@UseAdvVerticalStyle2)));
    //        }
    //        localWriter.WriteAttributeString(@"Time", null, FromDateTime((o.@Time)));
    //        if ((o.@EndTime).Ticks != (0))
    //        {
    //            localWriter.WriteAttributeString(@"EndTime", null, FromDateTime((o.@EndTime)));
    //        }
    //        if ((o.@Value) != -10000f)
    //        {
    //            localWriter.WriteAttributeString(@"Value", null, DCXMLConvert.ToString((o.@Value)));
    //        }
    //        if ((o.@LanternValue) != -10000f)
    //        {
    //            localWriter.WriteAttributeString(@"LanternValue", null, DCXMLConvert.ToString((o.@LanternValue)));
    //        }
    //        WriteAttribute(@"Text", string.Empty, (o.@Text));
    //        if ((o.@TextAlign) != global::System.Drawing.StringAlignment.@Center)
    //        {
    //            localWriter.WriteAttributeString(@"TextAlign", null, Write119_StringAlignment((o.@TextAlign)));
    //        }
    //        WriteAttribute(@"ColorValue", string.Empty, (o.@ColorValue));
    //        if ((o.@TextColorValue) != @"#00000000")
    //        {
    //            WriteAttribute(@"TextColorValue", string.Empty, (o.@TextColorValue));
    //        }
    //        if ((o.@Verified) != false)
    //        {
    //            MyWriteElementStringRaw(@"Verified", DCXMLConvert.ToString((o.@Verified)));
    //        }
    //        Write114_Color(@"VerifiedColor", string.Empty, (o.@VerifiedColor), false);
    //        if ((o.@ValueTextTopPadding) != 0f)
    //        {
    //            MyWriteElementStringRaw(@"ValueTextTopPadding", DCXMLConvert.ToString((o.@ValueTextTopPadding)));
    //        }
    //        Write64_XFontValue(@"Font", string.Empty, (o.@Font), false, false);
    //        Write34_XImageValue(@"Image", string.Empty, (o.@Image), false, false);
    //        Write34_XImageValue(@"CustomImage", string.Empty, (o.@CustomImage), false, false);
    //        if ((o.@UpAndDown) != global::DCSoft.TemperatureChart.ValuePointUpAndDown.@None)
    //        {
    //            localWriter.WriteElementString(@"UpAndDown", null, Write240_ValuePointUpAndDown((o.@UpAndDown)));
    //        }
    //        WriteEndElement(o);
    //    }
    //    string Write240_ValuePointUpAndDown(global::DCSoft.TemperatureChart.ValuePointUpAndDown v)
    //    {
    //        string s = null;
    //        switch (v)
    //        {
    //            case global::DCSoft.TemperatureChart.ValuePointUpAndDown.@None: s = @"None"; break;
    //            case global::DCSoft.TemperatureChart.ValuePointUpAndDown.@Up: s = @"Up"; break;
    //            case global::DCSoft.TemperatureChart.ValuePointUpAndDown.@Down: s = @"Down"; break;
    //            default: throw CreateInvalidEnumValueException((long)v, typeof(DCSoft.TemperatureChart.ValuePointUpAndDown));
    //        }
    //        return s;
    //    }
    //    string Write239_DCTimeLineBooleanValue(global::DCSoft.TemperatureChart.DCTimeLineBooleanValue v)
    //    {
    //        string s = null;
    //        switch (v)
    //        {
    //            case global::DCSoft.TemperatureChart.DCTimeLineBooleanValue.@Inherit: s = @"Inherit"; break;
    //            case global::DCSoft.TemperatureChart.DCTimeLineBooleanValue.@True: s = @"True"; break;
    //            case global::DCSoft.TemperatureChart.DCTimeLineBooleanValue.@False: s = @"False"; break;
    //            default: throw CreateInvalidEnumValueException((long)v, typeof(DCSoft.TemperatureChart.DCTimeLineBooleanValue));
    //        }
    //        return s;
    //    }
    //    string Write235_ValuePointSymbolStyle(global::DCSoft.TemperatureChart.ValuePointSymbolStyle v)
    //    {
    //        string s = null;
    //        switch (v)
    //        {
    //            case global::DCSoft.TemperatureChart.ValuePointSymbolStyle.@None: s = @"None"; break;
    //            case global::DCSoft.TemperatureChart.ValuePointSymbolStyle.@Default: s = @"Default"; break;
    //            case global::DCSoft.TemperatureChart.ValuePointSymbolStyle.@SolidCicle: s = @"SolidCicle"; break;
    //            case global::DCSoft.TemperatureChart.ValuePointSymbolStyle.@HollowCicle: s = @"HollowCicle"; break;
    //            case global::DCSoft.TemperatureChart.ValuePointSymbolStyle.@OpaqueHollowCicle: s = @"OpaqueHollowCicle"; break;
    //            case global::DCSoft.TemperatureChart.ValuePointSymbolStyle.@Cross: s = @"Cross"; break;
    //            case global::DCSoft.TemperatureChart.ValuePointSymbolStyle.@Square: s = @"Square"; break;
    //            case global::DCSoft.TemperatureChart.ValuePointSymbolStyle.@HollowSquare: s = @"HollowSquare"; break;
    //            case global::DCSoft.TemperatureChart.ValuePointSymbolStyle.@Diamond: s = @"Diamond"; break;
    //            case global::DCSoft.TemperatureChart.ValuePointSymbolStyle.@HollowDiamond: s = @"HollowDiamond"; break;
    //            case global::DCSoft.TemperatureChart.ValuePointSymbolStyle.@V: s = @"V"; break;
    //            case global::DCSoft.TemperatureChart.ValuePointSymbolStyle.@VReversed: s = @"VReversed"; break;
    //            case global::DCSoft.TemperatureChart.ValuePointSymbolStyle.@SolidTriangle: s = @"SolidTriangle"; break;
    //            case global::DCSoft.TemperatureChart.ValuePointSymbolStyle.@SolidTriangleReversed: s = @"SolidTriangleReversed"; break;
    //            case global::DCSoft.TemperatureChart.ValuePointSymbolStyle.@HollowTriangle: s = @"HollowTriangle"; break;
    //            case global::DCSoft.TemperatureChart.ValuePointSymbolStyle.@HollowTriangleReversed: s = @"HollowTriangleReversed"; break;
    //            case global::DCSoft.TemperatureChart.ValuePointSymbolStyle.@Character: s = @"Character"; break;
    //            case global::DCSoft.TemperatureChart.ValuePointSymbolStyle.@CharacterCircle: s = @"CharacterCircle"; break;
    //            case global::DCSoft.TemperatureChart.ValuePointSymbolStyle.@Custom: s = @"Custom"; break;
    //            default: throw CreateInvalidEnumValueException((long)v, typeof(DCSoft.TemperatureChart.ValuePointSymbolStyle));
    //        }
    //        return s;
    //    }
    //    internal protected void Write238_DCTimeLineParameter(string n, string ns, global::DCSoft.TemperatureChart.DCTimeLineParameter o, bool isNullable, bool needType)
    //    {
    //        this._BaseWriter = base.Writer;
    //        var localWriter = base.Writer;
    //        if (o == null)
    //        {
    //            //if (isNullable) WriteNullTagLiteral(n, ns);
    //            return;
    //        }
    //        if (!needType)
    //        {
    //            System.Type t = o.GetType();
    //            if (t == typeof(global::DCSoft.TemperatureChart.DCTimeLineParameter))
    //            {
    //            }
    //            else
    //            {
    //                throw CreateUnknownTypeException(o);
    //            }
    //        }
    //        WriteStartElement(n, ns, o, false, null);
    //        if (needType) WriteXsiType(@"DCTimeLineParameter", string.Empty);
    //        WriteAttribute(@"Name", string.Empty, (o.@Name));
    //        if ((o.@Value) != null)
    //        {
    //            WriteValue((o.@Value));
    //        }
    //        WriteEndElement(o);
    //    }
    //    internal protected void Write237_TemperatureDocumentConfig(string n, string ns, global::DCSoft.TemperatureChart.TemperatureDocumentConfig o, bool isNullable, bool needType)
    //    {
    //        this._BaseWriter = base.Writer;
    //        var localWriter = base.Writer;
    //        if (o == null)
    //        {
    //            //if (isNullable) WriteNullTagLiteral(n, ns);
    //            return;
    //        }
    //        if (!needType)
    //        {
    //            System.Type t = o.GetType();
    //            if (t == typeof(global::DCSoft.TemperatureChart.TemperatureDocumentConfig))
    //            {
    //            }
    //            else
    //            {
    //                throw CreateUnknownTypeException(o);
    //            }
    //        }
    //        WriteStartElement(n, ns, o, false, null);
    //        if (needType) WriteXsiType(@"TemperatureDocumentConfig", string.Empty);
    //        WriteAttribute(@"Version", string.Empty, (o.@Version));
    //        if ((o.@BothBlackWhenPrint) != false)
    //        {
    //            MyWriteElementStringRaw(@"BothBlackWhenPrint", DCXMLConvert.ToString((o.@BothBlackWhenPrint)));
    //        }
    //        if ((o.@LineWidthZoomRateWhenPrint) != 1f)
    //        {
    //            MyWriteElementStringRaw(@"LineWidthZoomRateWhenPrint", DCXMLConvert.ToString((o.@LineWidthZoomRateWhenPrint)));
    //        }
    //        if ((o.@LinkVisualStyle) != global::DCSoft.TemperatureChart.DocumentLinkVisualStyle.@Hover)
    //        {
    //            localWriter.WriteElementString(@"LinkVisualStyle", null, Write212_DocumentLinkVisualStyle((o.@LinkVisualStyle)));
    //        }
    //        if ((o.@DebugMode) != false)
    //        {
    //            MyWriteElementStringRaw(@"DebugMode", DCXMLConvert.ToString((o.@DebugMode)));
    //        }
    //        if ((o.@EditValuePointMode) != global::DCSoft.TemperatureChart.EditValuePointEventHandleMode.@None)
    //        {
    //            localWriter.WriteElementString(@"EditValuePointMode", null, Write213_EditValuePointEventHandleMode((o.@EditValuePointMode)));
    //        }
    //        if ((o.@EnableExtMouseMoveEvent) != false)
    //        {
    //            MyWriteElementStringRaw(@"EnableExtMouseMoveEvent", DCXMLConvert.ToString((o.@EnableExtMouseMoveEvent)));
    //        }
    //        if ((o.@EnableDataGridLinearAxisMode) != false)
    //        {
    //            MyWriteElementStringRaw(@"EnableDataGridLinearAxisMode", DCXMLConvert.ToString((o.@EnableDataGridLinearAxisMode)));
    //        }
    //        if ((o.@Readonly) != false)
    //        {
    //            MyWriteElementStringRaw(@"Readonly", DCXMLConvert.ToString((o.@Readonly)));
    //        }
    //        if ((o.@ExtendDaysForTimeLine) != 0f)
    //        {
    //            MyWriteElementStringRaw(@"ExtendDaysForTimeLine", DCXMLConvert.ToString((o.@ExtendDaysForTimeLine)));
    //        }
    //        MyWriteElementString(@"IllegalTextEndCharForLinux", (o.@IllegalTextEndCharForLinux));
    //        MyWriteElementString(@"TitleForToolTip", (o.@TitleForToolTip));
    //        if ((o.@EnableCustomValuePointSymbol) != false)
    //        {
    //            MyWriteElementStringRaw(@"EnableCustomValuePointSymbol", DCXMLConvert.ToString((o.@EnableCustomValuePointSymbol)));
    //        }
    //        if ((o.@HeaderLabelLineAlignment) != global::System.Drawing.StringAlignment.@Far)
    //        {
    //            localWriter.WriteElementString(@"HeaderLabelLineAlignment", null, Write119_StringAlignment((o.@HeaderLabelLineAlignment)));
    //        }
    //        if ((o.@SelectionMode) != global::DCSoft.TemperatureChart.DCTimeLineSelectionMode.@None)
    //        {
    //            localWriter.WriteElementString(@"SelectionMode", null, Write214_DCTimeLineSelectionMode((o.@SelectionMode)));
    //        }
    //        if ((o.@ShowTooltip) != true)
    //        {
    //            MyWriteElementStringRaw(@"ShowTooltip", DCXMLConvert.ToString((o.@ShowTooltip)));
    //        }
    //        if ((o.@AllowUserCollapseZone) != true)
    //        {
    //            MyWriteElementStringRaw(@"AllowUserCollapseZone", DCXMLConvert.ToString((o.@AllowUserCollapseZone)));
    //        }
    //        if ((o.@TickUnit) != global::DCSoft.TemperatureChart.DCTimeUnit.@Hour)
    //        {
    //            localWriter.WriteElementString(@"TickUnit", null, Write215_DCTimeUnit((o.@TickUnit)));
    //        }
    //        if ((o.@DataGridTopPadding) != 0f)
    //        {
    //            MyWriteElementStringRaw(@"DataGridTopPadding", DCXMLConvert.ToString((o.@DataGridTopPadding)));
    //        }
    //        if ((o.@DataGridBottomPadding) != 0f)
    //        {
    //            MyWriteElementStringRaw(@"DataGridBottomPadding", DCXMLConvert.ToString((o.@DataGridBottomPadding)));
    //        }
    //        MyWriteElementString(@"SQLTextForHeaderLabel", (o.@SQLTextForHeaderLabel));
    //        if ((o.@SpecifyTickWidth) != 0f)
    //        {
    //            MyWriteElementStringRaw(@"SpecifyTickWidth", DCXMLConvert.ToString((o.@SpecifyTickWidth)));
    //        }
    //        {
    //            global::DCSoft.TemperatureChart.DCTimeLineImageList a = (global::DCSoft.TemperatureChart.DCTimeLineImageList)(o.@Images);
    //            if (a != null && a.Count > 0)
    //            {
    //                var aCount = a.Count;//2222222
    //                localWriter.WriteStartElement(null, @"Images", null);
    //                for (int ia = 0; ia < aCount; ia++)
    //                {
    //                    Write216_DCTimeLineImage(@"Image", string.Empty, (a[ia]), true, false);
    //                }
    //                localWriter.WriteEndElement();
    //            }
    //        }
    //        {
    //            global::DCSoft.TemperatureChart.DCTimeLineLabelList a = (global::DCSoft.TemperatureChart.DCTimeLineLabelList)(o.@Labels);
    //            if (a != null && a.Count > 0)
    //            {
    //                var aCount = a.Count;//2222222
    //                localWriter.WriteStartElement(null, @"Labels", null);
    //                for (int ia = 0; ia < aCount; ia++)
    //                {
    //                    Write218_DCTimeLineLabel(@"Label", string.Empty, (a[ia]), true, false);
    //                }
    //                localWriter.WriteEndElement();
    //            }
    //        }
    //        MyWriteElementString(@"PageIndexText", (o.@PageIndexText));
    //        MyWriteElementString(@"SpecifyStartDate", (o.@SpecifyStartDate));
    //        MyWriteElementString(@"SpecifyEndDate", (o.@SpecifyEndDate));
    //        Write219_DocumentPageSettings(@"PageSettings", string.Empty, (o.@PageSettings), false, false);
    //        MyWriteElementString(@"FooterDescription", (o.@FooterDescription));
    //        if ((o.@ShowIcon) != false)
    //        {
    //            MyWriteElementStringRaw(@"ShowIcon", DCXMLConvert.ToString((o.@ShowIcon)));
    //        }
    //        if ((o.@ImagePixelWidth) != 16)
    //        {
    //            MyWriteElementStringRaw(@"ImagePixelWidth", DCXMLConvert.ToString((o.@ImagePixelWidth)));
    //        }
    //        if ((o.@ImagePixelHeight) != 16)
    //        {
    //            MyWriteElementStringRaw(@"ImagePixelHeight", DCXMLConvert.ToString((o.@ImagePixelHeight)));
    //        }
    //        if ((o.@ShadowPointDetectSeconds) != 2000)
    //        {
    //            MyWriteElementStringRaw(@"ShadowPointDetectSeconds", DCXMLConvert.ToString((o.@ShadowPointDetectSeconds)));
    //        }
    //        if ((o.@GridYSplitNum) != 8)
    //        {
    //            MyWriteElementStringRaw(@"GridYSplitNum", DCXMLConvert.ToString((o.@GridYSplitNum)));
    //        }
    //        Write221_GridYSplitInfo(@"GridYSplitInfo", string.Empty, (o.@GridYSplitInfo), false, false);
    //        {
    //            global::DCSoft.TemperatureChart.TimeLineZoneInfoList a = (global::DCSoft.TemperatureChart.TimeLineZoneInfoList)(o.@Zones);
    //            if (a != null && a.Count > 0)
    //            {
    //                var aCount = a.Count;//2222222
    //                localWriter.WriteStartElement(null, @"Zones", null);
    //                for (int ia = 0; ia < aCount; ia++)
    //                {
    //                    Write223_TimeLineZoneInfo(@"Zone", string.Empty, (a[ia]), true, false);
    //                }
    //                localWriter.WriteEndElement();
    //            }
    //        }
    //        {
    //            global::DCSoft.TemperatureChart.TickInfoList a = (global::DCSoft.TemperatureChart.TickInfoList)(o.@Ticks);
    //            if (a != null && a.Count > 0)
    //            {
    //                var aCount = a.Count;//2222222
    //                localWriter.WriteStartElement(null, @"Ticks", null);
    //                for (int ia = 0; ia < aCount; ia++)
    //                {
    //                    Write222_TickInfo(@"Tick", string.Empty, (a[ia]), true, false);
    //                }
    //                localWriter.WriteEndElement();
    //            }
    //        }
    //        if ((o.@SymbolSize) != 20f)
    //        {
    //            MyWriteElementStringRaw(@"SymbolSize", DCXMLConvert.ToString((o.@SymbolSize)));
    //        }
    //        MyWriteElementString(@"FontName", (o.@FontName));
    //        if ((o.@FontSize) != 9f)
    //        {
    //            MyWriteElementStringRaw(@"FontSize", DCXMLConvert.ToString((o.@FontSize)));
    //        }
    //        if ((o.@BigTitleFontSize) != 27f)
    //        {
    //            MyWriteElementStringRaw(@"BigTitleFontSize", DCXMLConvert.ToString((o.@BigTitleFontSize)));
    //        }
    //        Write64_XFontValue(@"PageIndexFont", string.Empty, (o.@PageIndexFont), false, false);
    //        MyWriteElementString(@"ForeColorValue", (o.@ForeColorValue));
    //        if ((o.@BigVerticalGridLineWidth) != 2f)
    //        {
    //            MyWriteElementStringRaw(@"BigVerticalGridLineWidth", DCXMLConvert.ToString((o.@BigVerticalGridLineWidth)));
    //        }
    //        MyWriteElementString(@"BigVerticalGridLineColorValue", (o.@BigVerticalGridLineColorValue));
    //        MyWriteElementString(@"BackColorValue", (o.@BackColorValue));
    //        MyWriteElementString(@"PageBackColorValue", (o.@PageBackColorValue));
    //        MyWriteElementString(@"GridLineColorValue", (o.@GridLineColorValue));
    //        MyWriteElementString(@"GridBackColorValue", (o.@GridBackColorValue));
    //        if ((o.@DateFormatString) != @"yyyy-MM-dd")
    //        {
    //            MyWriteElementString(@"DateFormatString", (o.@DateFormatString));
    //        }
    //        if ((o.@DateFormatStringForCrossYear) != @"yyyy-MM-dd")
    //        {
    //            MyWriteElementString(@"DateFormatStringForCrossYear", (o.@DateFormatStringForCrossYear));
    //        }
    //        if ((o.@DateFormatStringForCrossMonth) != @"MM-dd")
    //        {
    //            MyWriteElementString(@"DateFormatStringForCrossMonth", (o.@DateFormatStringForCrossMonth));
    //        }
    //        if ((o.@DateFormatStringForCrossWeek) != @"dd")
    //        {
    //            MyWriteElementString(@"DateFormatStringForCrossWeek", (o.@DateFormatStringForCrossWeek));
    //        }
    //        if ((o.@DateFormatStringForFirstIndexFirstPage) != @"yyyy-MM-dd")
    //        {
    //            MyWriteElementString(@"DateFormatStringForFirstIndexFirstPage", (o.@DateFormatStringForFirstIndexFirstPage));
    //        }
    //        if ((o.@DateFormatStringForFirstIndexOtherPage) != @"dd")
    //        {
    //            MyWriteElementString(@"DateFormatStringForFirstIndexOtherPage", (o.@DateFormatStringForFirstIndexOtherPage));
    //        }
    //        MyWriteElementString(@"Title", (o.@Title));
    //        if ((o.@SpecifyTitleHeight) != 0f)
    //        {
    //            MyWriteElementStringRaw(@"SpecifyTitleHeight", DCXMLConvert.ToString((o.@SpecifyTitleHeight)));
    //        }
    //        {
    //            global::DCSoft.TemperatureChart.HeaderLabelInfoList a = (global::DCSoft.TemperatureChart.HeaderLabelInfoList)(o.@HeaderLabels);
    //            if (a != null && a.Count > 0)
    //            {
    //                var aCount = a.Count;//2222222
    //                localWriter.WriteStartElement(null, @"HeaderLabels", null);
    //                for (int ia = 0; ia < aCount; ia++)
    //                {
    //                    Write224_HeaderLabelInfo(@"Label", string.Empty, (a[ia]), true, false);
    //                }
    //                localWriter.WriteEndElement();
    //            }
    //        }
    //        if ((o.@NumOfDaysInOnePage) != 7)
    //        {
    //            MyWriteElementStringRaw(@"NumOfDaysInOnePage", DCXMLConvert.ToString((o.@NumOfDaysInOnePage)));
    //        }
    //        {
    //            global::DCSoft.TemperatureChart.TitleLineInfoList a = (global::DCSoft.TemperatureChart.TitleLineInfoList)(o.@HeaderLines);
    //            if (a != null && a.Count > 0)
    //            {
    //                var aCount = a.Count;//2222222
    //                localWriter.WriteStartElement(null, @"HeaderLines", null);
    //                for (int ia = 0; ia < aCount; ia++)
    //                {
    //                    Write232_TitleLineInfo(@"Line", string.Empty, (a[ia]), true, false);
    //                }
    //                localWriter.WriteEndElement();
    //            }
    //        }
    //        {
    //            global::DCSoft.TemperatureChart.TitleLineInfoList a = (global::DCSoft.TemperatureChart.TitleLineInfoList)(o.@FooterLines);
    //            if (a != null && a.Count > 0)
    //            {
    //                var aCount = a.Count;//2222222
    //                localWriter.WriteStartElement(null, @"FooterLines", null);
    //                for (int ia = 0; ia < aCount; ia++)
    //                {
    //                    Write232_TitleLineInfo(@"Line", string.Empty, (a[ia]), true, false);
    //                }
    //                localWriter.WriteEndElement();
    //            }
    //        }
    //        {
    //            global::DCSoft.TemperatureChart.YAxisInfoList a = (global::DCSoft.TemperatureChart.YAxisInfoList)(o.@YAxisInfos);
    //            if (a != null && a.Count > 0)
    //            {
    //                var aCount = a.Count;//2222222
    //                localWriter.WriteStartElement(null, @"YAxisInfos", null);
    //                for (int ia = 0; ia < aCount; ia++)
    //                {
    //                    Write236_YAxisInfo(@"YAxis", string.Empty, (a[ia]), true, false);
    //                }
    //                localWriter.WriteEndElement();
    //            }
    //        }
    //        WriteEndElement(o);
    //    }
    //    internal protected void Write236_YAxisInfo(string n, string ns, global::DCSoft.TemperatureChart.YAxisInfo o, bool isNullable, bool needType)
    //    {
    //        this._BaseWriter = base.Writer;
    //        var localWriter = base.Writer;
    //        if (o == null)
    //        {
    //            //if (isNullable) WriteNullTagLiteral(n, ns);
    //            return;
    //        }
    //        if (!needType)
    //        {
    //            System.Type t = o.GetType();
    //            if (t == typeof(global::DCSoft.TemperatureChart.YAxisInfo))
    //            {
    //            }
    //            else
    //            {
    //                throw CreateUnknownTypeException(o);
    //            }
    //        }
    //        WriteStartElement(n, ns, o, false, null);
    //        if (needType) WriteXsiType(@"YAxisInfo", string.Empty);
    //        if ((o.@MergeIntoLeft) != false)
    //        {
    //            localWriter.WriteAttributeString(@"MergeIntoLeft", null, DCXMLConvert.ToString((o.@MergeIntoLeft)));
    //        }
    //        WriteAttribute(@"Name", string.Empty, (o.@Name));
    //        if ((o.@HighlightOutofNormalRange) != true)
    //        {
    //            localWriter.WriteAttributeString(@"HighlightOutofNormalRange", null, DCXMLConvert.ToString((o.@HighlightOutofNormalRange)));
    //        }
    //        if ((o.@InputTimePrecision) != global::DCSoft.TemperatureChart.DateTimePrecisionMode.@Minute)
    //        {
    //            localWriter.WriteAttributeString(@"InputTimePrecision", null, Write225_DateTimePrecisionMode((o.@InputTimePrecision)));
    //        }
    //        if ((o.@ValuePrecision) != 2)
    //        {
    //            localWriter.WriteAttributeString(@"ValuePrecision", null, DCXMLConvert.ToString((o.@ValuePrecision)));
    //        }
    //        if ((o.@AllowInterrupt) != true)
    //        {
    //            localWriter.WriteAttributeString(@"AllowInterrupt", null, DCXMLConvert.ToString((o.@AllowInterrupt)));
    //        }
    //        if ((o.@LineWidth) != 1)
    //        {
    //            localWriter.WriteAttributeString(@"LineWidth", null, DCXMLConvert.ToString((o.@LineWidth)));
    //        }
    //        WriteAttribute(@"LanternValueColorForUpValue", string.Empty, (o.@LanternValueColorForUpValue));
    //        WriteAttribute(@"LanternValueColorForDownValue", string.Empty, (o.@LanternValueColorForDownValue));
    //        if ((o.@LineStyleForLanternValue) != global::System.Drawing.Drawing2D.DashStyle.@Dash)
    //        {
    //            localWriter.WriteAttributeString(@"LineStyleForLanternValue", null, Write43_DashStyle((o.@LineStyleForLanternValue)));
    //        }
    //        if ((o.@SymbolSize) != 20f)
    //        {
    //            localWriter.WriteAttributeString(@"SymbolSize", null, DCXMLConvert.ToString((o.@SymbolSize)));
    //        }
    //        if ((o.@SpecifyTitleWidth) != 0f)
    //        {
    //            localWriter.WriteAttributeString(@"SpecifyTitleWidth", null, DCXMLConvert.ToString((o.@SpecifyTitleWidth)));
    //        }
    //        if ((o.@AllowOutofRange) != false)
    //        {
    //            localWriter.WriteAttributeString(@"AllowOutofRange", null, DCXMLConvert.ToString((o.@AllowOutofRange)));
    //        }
    //        if ((o.@SeparatorLineVisible) != true)
    //        {
    //            localWriter.WriteAttributeString(@"SeparatorLineVisible", null, DCXMLConvert.ToString((o.@SeparatorLineVisible)));
    //        }
    //        if ((o.@ClickToHide) != true)
    //        {
    //            localWriter.WriteAttributeString(@"ClickToHide", null, DCXMLConvert.ToString((o.@ClickToHide)));
    //        }
    //        if ((o.@Visible) != true)
    //        {
    //            localWriter.WriteAttributeString(@"Visible", null, DCXMLConvert.ToString((o.@Visible)));
    //        }
    //        if ((o.@ValueVisible) != true)
    //        {
    //            localWriter.WriteAttributeString(@"ValueVisible", null, DCXMLConvert.ToString((o.@ValueVisible)));
    //        }
    //        if ((o.@EnableLanternValue) != false)
    //        {
    //            localWriter.WriteAttributeString(@"EnableLanternValue", null, DCXMLConvert.ToString((o.@EnableLanternValue)));
    //        }
    //        WriteAttribute(@"LanternValueTitle", string.Empty, (o.@LanternValueTitle));
    //        if ((o.@Style) != global::DCSoft.TemperatureChart.YAxisInfoStyle.@Value)
    //        {
    //            localWriter.WriteAttributeString(@"Style", null, Write233_YAxisInfoStyle((o.@Style)));
    //        }
    //        WriteAttribute(@"HollowCovertTargetName", string.Empty, (o.@HollowCovertTargetName));
    //        WriteAttribute(@"ShadowName", string.Empty, (o.@ShadowName));
    //        WriteAttribute(@"TitleValueDispalyFormat", string.Empty, (o.@TitleValueDispalyFormat));
    //        if ((o.@TitleVisible) != true)
    //        {
    //            localWriter.WriteAttributeString(@"TitleVisible", null, DCXMLConvert.ToString((o.@TitleVisible)));
    //        }
    //        WriteAttribute(@"Title", string.Empty, (o.@Title));
    //        if ((o.@YSplitNum) != 8)
    //        {
    //            localWriter.WriteAttributeString(@"YSplitNum", null, DCXMLConvert.ToString((o.@YSplitNum)));
    //        }
    //        WriteAttribute(@"ValueFormatString", string.Empty, (o.@ValueFormatString));
    //        WriteAttribute(@"AlertLineColorValue", string.Empty, (o.@AlertLineColorValue));
    //        if ((o.@RedLineValue) != -10000f)
    //        {
    //            localWriter.WriteAttributeString(@"RedLineValue", null, DCXMLConvert.ToString((o.@RedLineValue)));
    //        }
    //        if ((o.@RedLineWidth) != 1f)
    //        {
    //            localWriter.WriteAttributeString(@"RedLineWidth", null, DCXMLConvert.ToString((o.@RedLineWidth)));
    //        }
    //        WriteAttribute(@"ValueTextBackColorValue", string.Empty, (o.@ValueTextBackColorValue));
    //        if ((o.@MaxValue) != 100f)
    //        {
    //            localWriter.WriteAttributeString(@"MaxValue", null, DCXMLConvert.ToString((o.@MaxValue)));
    //        }
    //        if ((o.@MinValue) != 0f)
    //        {
    //            localWriter.WriteAttributeString(@"MinValue", null, DCXMLConvert.ToString((o.@MinValue)));
    //        }
    //        if ((o.@ShowLegendInRule) != true)
    //        {
    //            localWriter.WriteAttributeString(@"ShowLegendInRule", null, DCXMLConvert.ToString((o.@ShowLegendInRule)));
    //        }
    //        if ((o.@ShowPointValue) != false)
    //        {
    //            localWriter.WriteAttributeString(@"ShowPointValue", null, DCXMLConvert.ToString((o.@ShowPointValue)));
    //        }
    //        WriteAttribute(@"ColorValueForPointValue", string.Empty, (o.@ColorValueForPointValue));
    //        WriteAttribute(@"ColorValueForDownValue", string.Empty, (o.@ColorValueForDownValue));
    //        WriteAttribute(@"ColorValueForUpValue", string.Empty, (o.@ColorValueForUpValue));
    //        if ((o.@SymbolStyle) != global::DCSoft.TemperatureChart.ValuePointSymbolStyle.@SolidCicle)
    //        {
    //            localWriter.WriteAttributeString(@"SymbolStyle", null, Write235_ValuePointSymbolStyle((o.@SymbolStyle)));
    //        }
    //        if ((o.@SymbolOffsetX) != 0f)
    //        {
    //            localWriter.WriteAttributeString(@"SymbolOffsetX", null, DCXMLConvert.ToString((o.@SymbolOffsetX)));
    //        }
    //        if ((o.@SymbolOffsetY) != 0f)
    //        {
    //            localWriter.WriteAttributeString(@"SymbolOffsetY", null, DCXMLConvert.ToString((o.@SymbolOffsetY)));
    //        }
    //        if ((o.@IntCharSymbol) != 82)
    //        {
    //            localWriter.WriteAttributeString(@"IntCharSymbol", null, DCXMLConvert.ToString((o.@IntCharSymbol)));
    //        }
    //        WriteAttribute(@"BottomTitle", string.Empty, (o.@BottomTitle));
    //        WriteAttribute(@"TitleBackColorValue", string.Empty, (o.@TitleBackColorValue));
    //        WriteAttribute(@"HiddenValueTitleBackColorValue", string.Empty, (o.@HiddenValueTitleBackColorValue));
    //        WriteAttribute(@"TitleColorValue", string.Empty, (o.@TitleColorValue));
    //        if ((o.@SymbolColorValue) != @"Red")
    //        {
    //            WriteAttribute(@"SymbolColorValue", string.Empty, (o.@SymbolColorValue));
    //        }
    //        WriteAttribute(@"DataSourceName", string.Empty, (o.@DataSourceName));
    //        WriteAttribute(@"ValueFieldName", string.Empty, (o.@ValueFieldName));
    //        WriteAttribute(@"LanternValueFieldName", string.Empty, (o.@LanternValueFieldName));
    //        if ((o.@SpecifyLanternSymbolStyle) != global::DCSoft.TemperatureChart.ValuePointSymbolStyle.@HollowCicle)
    //        {
    //            localWriter.WriteAttributeString(@"SpecifyLanternSymbolStyle", null, Write235_ValuePointSymbolStyle((o.@SpecifyLanternSymbolStyle)));
    //        }
    //        if ((o.@IntCharLantern) != 82)
    //        {
    //            localWriter.WriteAttributeString(@"IntCharLantern", null, DCXMLConvert.ToString((o.@IntCharLantern)));
    //        }
    //        WriteAttribute(@"TimeFieldName", string.Empty, (o.@TimeFieldName));
    //        if ((o.@MaxTextDisplayLength) != 0f)
    //        {
    //            MyWriteElementStringRaw(@"MaxTextDisplayLength", DCXMLConvert.ToString((o.@MaxTextDisplayLength)));
    //        }
    //        if ((o.@TopPadding) != -10000f)
    //        {
    //            MyWriteElementStringRaw(@"TopPadding", DCXMLConvert.ToString((o.@TopPadding)));
    //        }
    //        if ((o.@BottomPadding) != -10000f)
    //        {
    //            MyWriteElementStringRaw(@"BottomPadding", DCXMLConvert.ToString((o.@BottomPadding)));
    //        }
    //        Write64_XFontValue(@"Font", string.Empty, (o.@Font), false, false);
    //        Write64_XFontValue(@"ValueFont", string.Empty, (o.@ValueFont), false, false);
    //        Write227_ValuePointDataSourceInfo(@"DataSource", string.Empty, (o.@DataSource), false, false);
    //        if ((o.@ShadowPointVisible) != true)
    //        {
    //            MyWriteElementStringRaw(@"ShadowPointVisible", DCXMLConvert.ToString((o.@ShadowPointVisible)));
    //        }
    //        if ((o.@VerticalLine) != false)
    //        {
    //            MyWriteElementStringRaw(@"VerticalLine", DCXMLConvert.ToString((o.@VerticalLine)));
    //        }
    //        if ((o.@RedLinePrintVisible) != true)
    //        {
    //            MyWriteElementStringRaw(@"RedLinePrintVisible", DCXMLConvert.ToString((o.@RedLinePrintVisible)));
    //        }
    //        Write234_AbNormalRangeSettings(@"AbNormalRangeSettings", string.Empty, (o.@AbNormalRangeSettings), false, false);
    //        {
    //            global::DCSoft.TemperatureChart.YAxisScaleInfoList a = (global::DCSoft.TemperatureChart.YAxisScaleInfoList)(o.@Scales);
    //            if (a != null && a.Count > 0)
    //            {
    //                var aCount = a.Count;//2222222
    //                localWriter.WriteStartElement(null, @"Scales", null);
    //                for (int ia = 0; ia < aCount; ia++)
    //                {
    //                    Write231_YAxisScaleInfo(@"Scale", string.Empty, (a[ia]), true, false);
    //                }
    //                localWriter.WriteEndElement();
    //            }
    //        }
    //        WriteEndElement(o);
    //    }
    //    internal protected void Write231_YAxisScaleInfo(string n, string ns, global::DCSoft.TemperatureChart.YAxisScaleInfo o, bool isNullable, bool needType)
    //    {
    //        this._BaseWriter = base.Writer;
    //        var localWriter = base.Writer;
    //        if (o == null)
    //        {
    //            //if (isNullable) WriteNullTagLiteral(n, ns);
    //            return;
    //        }
    //        if (!needType)
    //        {
    //            System.Type t = o.GetType();
    //            if (t == typeof(global::DCSoft.TemperatureChart.YAxisScaleInfo))
    //            {
    //            }
    //            else
    //            {
    //                throw CreateUnknownTypeException(o);
    //            }
    //        }
    //        WriteStartElement(n, ns, o, false, null);
    //        if (needType) WriteXsiType(@"YAxisScaleInfo", string.Empty);
    //        WriteAttribute(@"Text", string.Empty, (o.@Text));
    //        if ((o.@Value) != 0)
    //        {
    //            localWriter.WriteAttributeString(@"Value", null, DCXMLConvert.ToString((o.@Value)));
    //        }
    //        if ((o.@ScaleRate) != 0f)
    //        {
    //            localWriter.WriteAttributeString(@"ScaleRate", null, DCXMLConvert.ToString((o.@ScaleRate)));
    //        }
    //        WriteAttribute(@"ColorValue", string.Empty, (o.@ColorValue));
    //        WriteEndElement(o);
    //    }
    //    internal protected void Write234_AbNormalRangeSettings(string n, string ns, global::DCSoft.TemperatureChart.AbNormalRangeSettings o, bool isNullable, bool needType)
    //    {
    //        this._BaseWriter = base.Writer;
    //        var localWriter = base.Writer;
    //        if (o == null)
    //        {
    //            //if (isNullable) WriteNullTagLiteral(n, ns);
    //            return;
    //        }
    //        if (!needType)
    //        {
    //            System.Type t = o.GetType();
    //            if (t == typeof(global::DCSoft.TemperatureChart.AbNormalRangeSettings))
    //            {
    //            }
    //            else
    //            {
    //                throw CreateUnknownTypeException(o);
    //            }
    //        }
    //        WriteStartElement(n, ns, o, false, null);
    //        if (needType) WriteXsiType(@"AbNormalRangeSettings", string.Empty);
    //        WriteAttribute(@"NormalRangeBackColorValue", string.Empty, (o.@NormalRangeBackColorValue));
    //        WriteAttribute(@"OutofNormalRangeBackColorValue", string.Empty, (o.@OutofNormalRangeBackColorValue));
    //        if ((o.@NormalMaxValue) != -10000f)
    //        {
    //            localWriter.WriteAttributeString(@"NormalMaxValue", null, DCXMLConvert.ToString((o.@NormalMaxValue)));
    //        }
    //        if ((o.@NormalMinValue) != -10000f)
    //        {
    //            localWriter.WriteAttributeString(@"NormalMinValue", null, DCXMLConvert.ToString((o.@NormalMinValue)));
    //        }
    //        Write192_XPenStyle(@"NormalRangeUpLineStyle", string.Empty, (o.@NormalRangeUpLineStyle), false, false);
    //        Write192_XPenStyle(@"NormalRangeDownLineStyle", string.Empty, (o.@NormalRangeDownLineStyle), false, false);
    //        WriteEndElement(o);
    //    }
    //    internal protected void Write227_ValuePointDataSourceInfo(string n, string ns, global::DCSoft.TemperatureChart.ValuePointDataSourceInfo o, bool isNullable, bool needType)
    //    {
    //        this._BaseWriter = base.Writer;
    //        var localWriter = base.Writer;
    //        if (o == null)
    //        {
    //            //if (isNullable) WriteNullTagLiteral(n, ns);
    //            return;
    //        }
    //        if (!needType)
    //        {
    //            System.Type t = o.GetType();
    //            if (t == typeof(global::DCSoft.TemperatureChart.ValuePointDataSourceInfo))
    //            {
    //            }
    //            else
    //            {
    //                throw CreateUnknownTypeException(o);
    //            }
    //        }
    //        WriteStartElement(n, ns, o, false, null);
    //        if (needType) WriteXsiType(@"ValuePointDataSourceInfo", string.Empty);
    //        WriteAttribute(@"FieldNameForID", string.Empty, (o.@FieldNameForID));
    //        WriteAttribute(@"FieldNameForLink", string.Empty, (o.@FieldNameForLink));
    //        WriteAttribute(@"FieldNameForTitle", string.Empty, (o.@FieldNameForTitle));
    //        WriteAttribute(@"FieldNameForTime", string.Empty, (o.@FieldNameForTime));
    //        WriteAttribute(@"FieldNameForValue", string.Empty, (o.@FieldNameForValue));
    //        WriteAttribute(@"FieldNameForLanternValue", string.Empty, (o.@FieldNameForLanternValue));
    //        WriteAttribute(@"FieldNameForText", string.Empty, (o.@FieldNameForText));
    //        MyWriteElementString(@"SQLText", (o.@SQLText));
    //        WriteEndElement(o);
    //    }
    //    string Write233_YAxisInfoStyle(global::DCSoft.TemperatureChart.YAxisInfoStyle v)
    //    {
    //        string s = null;
    //        switch (v)
    //        {
    //            case global::DCSoft.TemperatureChart.YAxisInfoStyle.@Value: s = @"Value"; break;
    //            case global::DCSoft.TemperatureChart.YAxisInfoStyle.@Text: s = @"Text"; break;
    //            case global::DCSoft.TemperatureChart.YAxisInfoStyle.@Background: s = @"Background"; break;
    //            case global::DCSoft.TemperatureChart.YAxisInfoStyle.@PartialBackground: s = @"PartialBackground"; break;
    //            case global::DCSoft.TemperatureChart.YAxisInfoStyle.@TextInsideGrid: s = @"TextInsideGrid"; break;
    //            default: throw CreateInvalidEnumValueException((long)v, typeof(DCSoft.TemperatureChart.YAxisInfoStyle));
    //        }
    //        return s;
    //    }
    //    string Write225_DateTimePrecisionMode(global::DCSoft.TemperatureChart.DateTimePrecisionMode v)
    //    {
    //        string s = null;
    //        switch (v)
    //        {
    //            case global::DCSoft.TemperatureChart.DateTimePrecisionMode.@NoLimited: s = @"NoLimited"; break;
    //            case global::DCSoft.TemperatureChart.DateTimePrecisionMode.@Second: s = @"Second"; break;
    //            case global::DCSoft.TemperatureChart.DateTimePrecisionMode.@Minute: s = @"Minute"; break;
    //            case global::DCSoft.TemperatureChart.DateTimePrecisionMode.@Hour: s = @"Hour"; break;
    //            case global::DCSoft.TemperatureChart.DateTimePrecisionMode.@Day: s = @"Day"; break;
    //            case global::DCSoft.TemperatureChart.DateTimePrecisionMode.@Month: s = @"Month"; break;
    //            case global::DCSoft.TemperatureChart.DateTimePrecisionMode.@Year: s = @"Year"; break;
    //            default: throw CreateInvalidEnumValueException((long)v, typeof(DCSoft.TemperatureChart.DateTimePrecisionMode));
    //        }
    //        return s;
    //    }
    //    internal protected void Write232_TitleLineInfo(string n, string ns, global::DCSoft.TemperatureChart.TitleLineInfo o, bool isNullable, bool needType)
    //    {
    //        this._BaseWriter = base.Writer;
    //        var localWriter = base.Writer;
    //        if (o == null)
    //        {
    //            //if (isNullable) WriteNullTagLiteral(n, ns);
    //            return;
    //        }
    //        if (!needType)
    //        {
    //            System.Type t = o.GetType();
    //            if (t == typeof(global::DCSoft.TemperatureChart.TitleLineInfo))
    //            {
    //            }
    //            else
    //            {
    //                throw CreateUnknownTypeException(o);
    //            }
    //        }
    //        WriteStartElement(n, ns, o, false, null);
    //        if (needType) WriteXsiType(@"TitleLineInfo", string.Empty);
    //        if ((o.@InputTimePrecision) != global::DCSoft.TemperatureChart.DateTimePrecisionMode.@Minute)
    //        {
    //            localWriter.WriteAttributeString(@"InputTimePrecision", null, Write225_DateTimePrecisionMode((o.@InputTimePrecision)));
    //        }
    //        WriteAttribute(@"Name", string.Empty, (o.@Name));
    //        if ((o.@AutoHeight) != false)
    //        {
    //            localWriter.WriteAttributeString(@"AutoHeight", null, DCXMLConvert.ToString((o.@AutoHeight)));
    //        }
    //        if ((o.@VisibleWhenNoValuePoint) != true)
    //        {
    //            localWriter.WriteAttributeString(@"VisibleWhenNoValuePoint", null, DCXMLConvert.ToString((o.@VisibleWhenNoValuePoint)));
    //        }
    //        if ((o.@Visible) != true)
    //        {
    //            localWriter.WriteAttributeString(@"Visible", null, DCXMLConvert.ToString((o.@Visible)));
    //        }
    //        if ((o.@BlankDateWhenNoData) != false)
    //        {
    //            localWriter.WriteAttributeString(@"BlankDateWhenNoData", null, DCXMLConvert.ToString((o.@BlankDateWhenNoData)));
    //        }
    //        if ((o.@HiddenOnPageViewWhenNoValuePoints) != false)
    //        {
    //            localWriter.WriteAttributeString(@"HiddenOnPageViewWhenNoValuePoints", null, DCXMLConvert.ToString((o.@HiddenOnPageViewWhenNoValuePoints)));
    //        }
    //        WriteAttribute(@"GroupName", string.Empty, (o.@GroupName));
    //        if ((o.@AfterOperaDaysFromZero) != true)
    //        {
    //            localWriter.WriteAttributeString(@"AfterOperaDaysFromZero", null, DCXMLConvert.ToString((o.@AfterOperaDaysFromZero)));
    //        }
    //        if ((o.@AfterOperaDaysBeginOne) != false)
    //        {
    //            localWriter.WriteAttributeString(@"AfterOperaDaysBeginOne", null, DCXMLConvert.ToString((o.@AfterOperaDaysBeginOne)));
    //        }
    //        WriteAttribute(@"OutofNormalRangeTextColorValue", string.Empty, (o.@OutofNormalRangeTextColorValue));
    //        if ((o.@NormalMaxValue) != -10000f)
    //        {
    //            localWriter.WriteAttributeString(@"NormalMaxValue", null, DCXMLConvert.ToString((o.@NormalMaxValue)));
    //        }
    //        if ((o.@NormalMinValue) != -10000f)
    //        {
    //            localWriter.WriteAttributeString(@"NormalMinValue", null, DCXMLConvert.ToString((o.@NormalMinValue)));
    //        }
    //        if ((o.@ExtendGridLineType) != global::DCSoft.TemperatureChart.DCExtendGridLineType.@Below)
    //        {
    //            localWriter.WriteAttributeString(@"ExtendGridLineType", null, Write226_DCExtendGridLineType((o.@ExtendGridLineType)));
    //        }
    //        if ((o.@EnableEndTime) != true)
    //        {
    //            localWriter.WriteAttributeString(@"EnableEndTime", null, DCXMLConvert.ToString((o.@EnableEndTime)));
    //        }
    //        if ((o.@BlockWidth) != 15f)
    //        {
    //            localWriter.WriteAttributeString(@"BlockWidth", null, DCXMLConvert.ToString((o.@BlockWidth)));
    //        }
    //        WriteAttribute(@"ValueDisplayFormat", string.Empty, (o.@ValueDisplayFormat));
    //        WriteAttribute(@"LoopTextList", string.Empty, (o.@LoopTextList));
    //        if ((o.@SpecifyTitleWidth) != 0f)
    //        {
    //            localWriter.WriteAttributeString(@"SpecifyTitleWidth", null, DCXMLConvert.ToString((o.@SpecifyTitleWidth)));
    //        }
    //        WriteAttribute(@"Title", string.Empty, (o.@Title));
    //        WriteAttribute(@"PageTitleTexts", string.Empty, (o.@PageTitleTexts));
    //        WriteAttribute(@"TitleColorValue", string.Empty, (o.@TitleColorValue));
    //        WriteAttribute(@"TextColorValue", string.Empty, (o.@TextColorValue));
    //        if ((o.@TitleAlign) != global::System.Drawing.StringAlignment.@Center)
    //        {
    //            localWriter.WriteAttributeString(@"TitleAlign", null, Write119_StringAlignment((o.@TitleAlign)));
    //        }
    //        if ((o.@ValueAlign) != global::System.Drawing.StringAlignment.@Center)
    //        {
    //            localWriter.WriteAttributeString(@"ValueAlign", null, Write119_StringAlignment((o.@ValueAlign)));
    //        }
    //        if ((o.@MaxValueForDayIndex) != 13)
    //        {
    //            localWriter.WriteAttributeString(@"MaxValueForDayIndex", null, DCXMLConvert.ToString((o.@MaxValueForDayIndex)));
    //        }
    //        WriteAttribute(@"CircleText", string.Empty, (o.@CircleText));
    //        if ((o.@SpecifyHeight) != 0f)
    //        {
    //            localWriter.WriteAttributeString(@"SpecifyHeight", null, DCXMLConvert.ToString((o.@SpecifyHeight)));
    //        }
    //        WriteAttribute(@"EndDateKeyword", string.Empty, (o.@EndDateKeyword));
    //        if ((o.@StartDate).Ticks != (599266080000000000))
    //        {
    //            localWriter.WriteAttributeString(@"StartDate", null, FromDateTime((o.@StartDate)));
    //        }
    //        WriteAttribute(@"StartDateKeyword", string.Empty, (o.@StartDateKeyword));
    //        if ((o.@PreserveStartKeywordOrder) != false)
    //        {
    //            localWriter.WriteAttributeString(@"PreserveStartKeywordOrder", null, DCXMLConvert.ToString((o.@PreserveStartKeywordOrder)));
    //        }
    //        if ((o.@ShowBackColor) != true)
    //        {
    //            localWriter.WriteAttributeString(@"ShowBackColor", null, DCXMLConvert.ToString((o.@ShowBackColor)));
    //        }
    //        if ((o.@LayoutType) != global::DCSoft.TemperatureChart.TitleLineLayoutType.@Normal)
    //        {
    //            localWriter.WriteAttributeString(@"LayoutType", null, Write228_TitleLineLayoutType((o.@LayoutType)));
    //        }
    //        if ((o.@TickStep) != 1)
    //        {
    //            localWriter.WriteAttributeString(@"TickStep", null, DCXMLConvert.ToString((o.@TickStep)));
    //        }
    //        if ((o.@TickLineVisible) != true)
    //        {
    //            localWriter.WriteAttributeString(@"TickLineVisible", null, DCXMLConvert.ToString((o.@TickLineVisible)));
    //        }
    //        if ((o.@ForceUpWhenPageFirstPoint) != false)
    //        {
    //            localWriter.WriteAttributeString(@"ForceUpWhenPageFirstPoint", null, DCXMLConvert.ToString((o.@ForceUpWhenPageFirstPoint)));
    //        }
    //        if ((o.@UpAndDownTextType) != global::DCSoft.TemperatureChart.UpAndDownTextType.@None)
    //        {
    //            localWriter.WriteAttributeString(@"UpAndDownTextType", null, Write229_UpAndDownTextType((o.@UpAndDownTextType)));
    //        }
    //        if ((o.@ValueType) != global::DCSoft.TemperatureChart.TitleLineValueType.@SerialDate)
    //        {
    //            localWriter.WriteAttributeString(@"ValueType", null, Write230_TitleLineValueType((o.@ValueType)));
    //        }
    //        WriteAttribute(@"DataSourceName", string.Empty, (o.@DataSourceName));
    //        WriteAttribute(@"ValueFieldName", string.Empty, (o.@ValueFieldName));
    //        WriteAttribute(@"TimeFieldName", string.Empty, (o.@TimeFieldName));
    //        if ((o.@ValueTextMultiLine) != false)
    //        {
    //            MyWriteElementStringRaw(@"ValueTextMultiLine", DCXMLConvert.ToString((o.@ValueTextMultiLine)));
    //        }
    //        Write64_XFontValue(@"Font", string.Empty, (o.@Font), false, false);
    //        Write64_XFontValue(@"ValueFont", string.Empty, (o.@ValueFont), false, false);
    //        Write227_ValuePointDataSourceInfo(@"DataSource", string.Empty, (o.@DataSource), false, false);
    //        {
    //            global::DCSoft.TemperatureChart.YAxisScaleInfoList a = (global::DCSoft.TemperatureChart.YAxisScaleInfoList)(o.@Scales);
    //            if (a != null && a.Count > 0)
    //            {
    //                var aCount = a.Count;//2222222
    //                localWriter.WriteStartElement(null, @"Scales", null);
    //                for (int ia = 0; ia < aCount; ia++)
    //                {
    //                    Write231_YAxisScaleInfo(@"Scale", string.Empty, (a[ia]), true, false);
    //                }
    //                localWriter.WriteEndElement();
    //            }
    //        }
    //        WriteEndElement(o);
    //    }
    //    string Write230_TitleLineValueType(global::DCSoft.TemperatureChart.TitleLineValueType v)
    //    {
    //        string s = null;
    //        switch (v)
    //        {
    //            case global::DCSoft.TemperatureChart.TitleLineValueType.@NewSerialDate: s = @"NewSerialDate"; break;
    //            case global::DCSoft.TemperatureChart.TitleLineValueType.@SerialDate: s = @"SerialDate"; break;
    //            case global::DCSoft.TemperatureChart.TitleLineValueType.@GlobalDayIndex: s = @"GlobalDayIndex"; break;
    //            case global::DCSoft.TemperatureChart.TitleLineValueType.@InDayIndex: s = @"InDayIndex"; break;
    //            case global::DCSoft.TemperatureChart.TitleLineValueType.@DayIndex: s = @"DayIndex"; break;
    //            case global::DCSoft.TemperatureChart.TitleLineValueType.@HourTick: s = @"HourTick"; break;
    //            case global::DCSoft.TemperatureChart.TitleLineValueType.@Text: s = @"Text"; break;
    //            case global::DCSoft.TemperatureChart.TitleLineValueType.@Data: s = @"Data"; break;
    //            case global::DCSoft.TemperatureChart.TitleLineValueType.@TickText: s = @"TickText"; break;
    //            default: throw CreateInvalidEnumValueException((long)v, typeof(DCSoft.TemperatureChart.TitleLineValueType));
    //        }
    //        return s;
    //    }
    //    string Write229_UpAndDownTextType(global::DCSoft.TemperatureChart.UpAndDownTextType v)
    //    {
    //        string s = null;
    //        switch (v)
    //        {
    //            case global::DCSoft.TemperatureChart.UpAndDownTextType.@None: s = @"None"; break;
    //            case global::DCSoft.TemperatureChart.UpAndDownTextType.@ShowByTick: s = @"ShowByTick"; break;
    //            case global::DCSoft.TemperatureChart.UpAndDownTextType.@ShowByText: s = @"ShowByText"; break;
    //            default: throw CreateInvalidEnumValueException((long)v, typeof(DCSoft.TemperatureChart.UpAndDownTextType));
    //        }
    //        return s;
    //    }
    //    string Write228_TitleLineLayoutType(global::DCSoft.TemperatureChart.TitleLineLayoutType v)
    //    {
    //        string s = null;
    //        switch (v)
    //        {
    //            case global::DCSoft.TemperatureChart.TitleLineLayoutType.@Normal: s = @"Normal"; break;
    //            case global::DCSoft.TemperatureChart.TitleLineLayoutType.@Free: s = @"Free"; break;
    //            case global::DCSoft.TemperatureChart.TitleLineLayoutType.@FreeText: s = @"FreeText"; break;
    //            case global::DCSoft.TemperatureChart.TitleLineLayoutType.@Cascade: s = @"Cascade"; break;
    //            case global::DCSoft.TemperatureChart.TitleLineLayoutType.@HorizCascade: s = @"HorizCascade"; break;
    //            case global::DCSoft.TemperatureChart.TitleLineLayoutType.@AutoCascade: s = @"AutoCascade"; break;
    //            case global::DCSoft.TemperatureChart.TitleLineLayoutType.@Slant: s = @"Slant"; break;
    //            case global::DCSoft.TemperatureChart.TitleLineLayoutType.@Slant2: s = @"Slant2"; break;
    //            case global::DCSoft.TemperatureChart.TitleLineLayoutType.@Slant3: s = @"Slant3"; break;
    //            case global::DCSoft.TemperatureChart.TitleLineLayoutType.@Fraction: s = @"Fraction"; break;
    //            default: throw CreateInvalidEnumValueException((long)v, typeof(DCSoft.TemperatureChart.TitleLineLayoutType));
    //        }
    //        return s;
    //    }
    //    string Write226_DCExtendGridLineType(global::DCSoft.TemperatureChart.DCExtendGridLineType v)
    //    {
    //        string s = null;
    //        switch (v)
    //        {
    //            case global::DCSoft.TemperatureChart.DCExtendGridLineType.@None: s = @"None"; break;
    //            case global::DCSoft.TemperatureChart.DCExtendGridLineType.@Above: s = @"Above"; break;
    //            case global::DCSoft.TemperatureChart.DCExtendGridLineType.@Below: s = @"Below"; break;
    //            default: throw CreateInvalidEnumValueException((long)v, typeof(DCSoft.TemperatureChart.DCExtendGridLineType));
    //        }
    //        return s;
    //    }
    //    internal protected void Write224_HeaderLabelInfo(string n, string ns, global::DCSoft.TemperatureChart.HeaderLabelInfo o, bool isNullable, bool needType)
    //    {
    //        this._BaseWriter = base.Writer;
    //        var localWriter = base.Writer;
    //        if (o == null)
    //        {
    //            //if (isNullable) WriteNullTagLiteral(n, ns);
    //            return;
    //        }
    //        if (!needType)
    //        {
    //            System.Type t = o.GetType();
    //            if (t == typeof(global::DCSoft.TemperatureChart.HeaderLabelInfo))
    //            {
    //            }
    //            else
    //            {
    //                throw CreateUnknownTypeException(o);
    //            }
    //        }
    //        WriteStartElement(n, ns, o, false, null);
    //        if (needType) WriteXsiType(@"HeaderLabelInfo", string.Empty);
    //        if (((o.@Title) != null) && ((o.@Title).Length != 0))
    //        {
    //            WriteAttribute(@"Title", string.Empty, (o.@Title));
    //        }
    //        WriteAttribute(@"DataSourceName", string.Empty, (o.@DataSourceName));
    //        WriteAttribute(@"ValueFieldName", string.Empty, (o.@ValueFieldName));
    //        WriteAttribute(@"ParameterName", string.Empty, (o.@ParameterName));
    //        if (((o.@Value) != null) && ((o.@Value).Length != 0))
    //        {
    //            WriteAttribute(@"Value", string.Empty, (o.@Value));
    //        }
    //        localWriter.WriteAttributeString(@"SeperatorChar", null, FromChar((o.@SeperatorChar)));
    //        WriteEndElement(o);
    //    }
    //    internal protected void Write222_TickInfo(string n, string ns, global::DCSoft.TemperatureChart.TickInfo o, bool isNullable, bool needType)
    //    {
    //        this._BaseWriter = base.Writer;
    //        var localWriter = base.Writer;
    //        if (o == null)
    //        {
    //            //if (isNullable) WriteNullTagLiteral(n, ns);
    //            return;
    //        }
    //        if (!needType)
    //        {
    //            System.Type t = o.GetType();
    //            if (t == typeof(global::DCSoft.TemperatureChart.TickInfo))
    //            {
    //            }
    //            else
    //            {
    //                throw CreateUnknownTypeException(o);
    //            }
    //        }
    //        WriteStartElement(n, ns, o, false, null);
    //        if (needType) WriteXsiType(@"TickInfo", string.Empty);
    //        if ((o.@Value) != 0f)
    //        {
    //            localWriter.WriteAttributeString(@"Value", null, DCXMLConvert.ToString((o.@Value)));
    //        }
    //        WriteAttribute(@"Text", string.Empty, (o.@Text));
    //        WriteAttribute(@"ColorValue", string.Empty, (o.@ColorValue));
    //        WriteEndElement(o);
    //    }
    //    internal protected void Write223_TimeLineZoneInfo(string n, string ns, global::DCSoft.TemperatureChart.TimeLineZoneInfo o, bool isNullable, bool needType)
    //    {
    //        this._BaseWriter = base.Writer;
    //        var localWriter = base.Writer;
    //        if (o == null)
    //        {
    //            //if (isNullable) WriteNullTagLiteral(n, ns);
    //            return;
    //        }
    //        if (!needType)
    //        {
    //            System.Type t = o.GetType();
    //            if (t == typeof(global::DCSoft.TemperatureChart.TimeLineZoneInfo))
    //            {
    //            }
    //            else
    //            {
    //                throw CreateUnknownTypeException(o);
    //            }
    //        }
    //        WriteStartElement(n, ns, o, false, null);
    //        if (needType) WriteXsiType(@"TimeLineZoneInfo", string.Empty);
    //        WriteAttribute(@"Name", string.Empty, (o.@Name));
    //        if ((o.@StartTime).Ticks != (599266080000000000))
    //        {
    //            localWriter.WriteAttributeString(@"StartTime", null, FromDateTime((o.@StartTime)));
    //        }
    //        if ((o.@EndTime).Ticks != (599266080000000000))
    //        {
    //            localWriter.WriteAttributeString(@"EndTime", null, FromDateTime((o.@EndTime)));
    //        }
    //        if ((o.@AlignToGrid) != true)
    //        {
    //            localWriter.WriteAttributeString(@"AlignToGrid", null, DCXMLConvert.ToString((o.@AlignToGrid)));
    //        }
    //        if ((o.@GridLineStyle) != global::System.Drawing.Drawing2D.DashStyle.@Solid)
    //        {
    //            localWriter.WriteAttributeString(@"GridLineStyle", null, Write43_DashStyle((o.@GridLineStyle)));
    //        }
    //        WriteAttribute(@"GridLineColorValue", string.Empty, (o.@GridLineColorValue));
    //        WriteAttribute(@"BackColorValue", string.Empty, (o.@BackColorValue));
    //        if ((o.@SpecifyTickWidth) != 0f)
    //        {
    //            localWriter.WriteAttributeString(@"SpecifyTickWidth", null, DCXMLConvert.ToString((o.@SpecifyTickWidth)));
    //        }
    //        if ((o.@AutoTickStepSeconds) != 0)
    //        {
    //            localWriter.WriteAttributeString(@"AutoTickStepSeconds", null, DCXMLConvert.ToString((o.@AutoTickStepSeconds)));
    //        }
    //        WriteAttribute(@"AutoTickFormatString", string.Empty, (o.@AutoTickFormatString));
    //        {
    //            global::DCSoft.TemperatureChart.TickInfoList a = (global::DCSoft.TemperatureChart.TickInfoList)(o.@Ticks);
    //            if (a != null && a.Count > 0)
    //            {
    //                var aCount = a.Count;//2222222
    //                localWriter.WriteStartElement(null, @"Ticks", null);
    //                for (int ia = 0; ia < aCount; ia++)
    //                {
    //                    Write222_TickInfo(@"Tick", string.Empty, (a[ia]), true, false);
    //                }
    //                localWriter.WriteEndElement();
    //            }
    //        }
    //        WriteEndElement(o);
    //    }
    //    internal protected void Write221_GridYSplitInfo(string n, string ns, global::DCSoft.TemperatureChart.GridYSplitInfo o, bool isNullable, bool needType)
    //    {
    //        this._BaseWriter = base.Writer;
    //        var localWriter = base.Writer;
    //        if (o == null)
    //        {
    //            //if (isNullable) WriteNullTagLiteral(n, ns);
    //            return;
    //        }
    //        if (!needType)
    //        {
    //            System.Type t = o.GetType();
    //            if (t == typeof(global::DCSoft.TemperatureChart.GridYSplitInfo))
    //            {
    //            }
    //            else
    //            {
    //                throw CreateUnknownTypeException(o);
    //            }
    //        }
    //        WriteStartElement(n, ns, o, false, null);
    //        if (needType) WriteXsiType(@"GridYSplitInfo", string.Empty);
    //        if ((o.@GridYSplitNum) != 8)
    //        {
    //            MyWriteElementStringRaw(@"GridYSplitNum", DCXMLConvert.ToString((o.@GridYSplitNum)));
    //        }
    //        if ((o.@GridYSpaceNum) != 5)
    //        {
    //            MyWriteElementStringRaw(@"GridYSpaceNum", DCXMLConvert.ToString((o.@GridYSpaceNum)));
    //        }
    //        if ((o.@GridYSpaceNumForBottomPadding) != -1)
    //        {
    //            MyWriteElementStringRaw(@"GridYSpaceNumForBottomPadding", DCXMLConvert.ToString((o.@GridYSpaceNumForBottomPadding)));
    //        }
    //        if ((o.@ThickLineWidth) != 2f)
    //        {
    //            MyWriteElementStringRaw(@"ThickLineWidth", DCXMLConvert.ToString((o.@ThickLineWidth)));
    //        }
    //        if ((o.@ThinLineWidth) != 1f)
    //        {
    //            MyWriteElementStringRaw(@"ThinLineWidth", DCXMLConvert.ToString((o.@ThinLineWidth)));
    //        }
    //        WriteEndElement(o);
    //    }
    //    internal protected void Write219_DocumentPageSettings(string n, string ns, global::DCSoft.TemperatureChart.DocumentPageSettings o, bool isNullable, bool needType)
    //    {
    //        this._BaseWriter = base.Writer;
    //        var localWriter = base.Writer;
    //        if (o == null)
    //        {
    //            //if (isNullable) WriteNullTagLiteral(n, ns);
    //            return;
    //        }
    //        if (!needType)
    //        {
    //            System.Type t = o.GetType();
    //            if (t == typeof(global::DCSoft.TemperatureChart.DocumentPageSettings))
    //            {
    //            }
    //            else
    //            {
    //                throw CreateUnknownTypeException(o);
    //            }
    //        }
    //        WriteStartElement(n, ns, o, false, null);
    //        if (needType) WriteXsiType(@"DocumentPageSettings", string.Empty);
    //        if ((o.@PaperSizeName) != @"A4")
    //        {
    //            MyWriteElementString(@"PaperSizeName", (o.@PaperSizeName));
    //        }
    //        if ((o.@PaperWidth) != 827)
    //        {
    //            MyWriteElementStringRaw(@"PaperWidth", DCXMLConvert.ToString((o.@PaperWidth)));
    //        }
    //        if ((o.@PaperHeight) != 1169)
    //        {
    //            MyWriteElementStringRaw(@"PaperHeight", DCXMLConvert.ToString((o.@PaperHeight)));
    //        }
    //        if ((o.@LeftMargin) != 100)
    //        {
    //            MyWriteElementStringRaw(@"LeftMargin", DCXMLConvert.ToString((o.@LeftMargin)));
    //        }
    //        if ((o.@TopMargin) != 100)
    //        {
    //            MyWriteElementStringRaw(@"TopMargin", DCXMLConvert.ToString((o.@TopMargin)));
    //        }
    //        if ((o.@RightMargin) != 100)
    //        {
    //            MyWriteElementStringRaw(@"RightMargin", DCXMLConvert.ToString((o.@RightMargin)));
    //        }
    //        if ((o.@BottomMargin) != 100)
    //        {
    //            MyWriteElementStringRaw(@"BottomMargin", DCXMLConvert.ToString((o.@BottomMargin)));
    //        }
    //        if ((o.@Landscape) != false)
    //        {
    //            MyWriteElementStringRaw(@"Landscape", DCXMLConvert.ToString((o.@Landscape)));
    //        }
    //        if ((o.@AutoFitPageSize) != false)
    //        {
    //            MyWriteElementStringRaw(@"AutoFitPageSize", DCXMLConvert.ToString((o.@AutoFitPageSize)));
    //        }
    //        WriteEndElement(o);
    //    }
    //    internal protected void Write218_DCTimeLineLabel(string n, string ns, global::DCSoft.TemperatureChart.DCTimeLineLabel o, bool isNullable, bool needType)
    //    {
    //        this._BaseWriter = base.Writer;
    //        var localWriter = base.Writer;
    //        if (o == null)
    //        {
    //            //if (isNullable) WriteNullTagLiteral(n, ns);
    //            return;
    //        }
    //        if (!needType)
    //        {
    //            System.Type t = o.GetType();
    //            if (t == typeof(global::DCSoft.TemperatureChart.DCTimeLineLabel))
    //            {
    //            }
    //            else
    //            {
    //                throw CreateUnknownTypeException(o);
    //            }
    //        }
    //        WriteStartElement(n, ns, o, false, null);
    //        if (needType) WriteXsiType(@"DCTimeLineLabel", string.Empty);
    //        WriteAttribute(@"Text", string.Empty, (o.@Text));
    //        WriteAttribute(@"ParameterName", string.Empty, (o.@ParameterName));
    //        if ((o.@MultiLine) != false)
    //        {
    //            localWriter.WriteAttributeString(@"MultiLine", null, DCXMLConvert.ToString((o.@MultiLine)));
    //        }
    //        WriteAttribute(@"ColorValue", string.Empty, (o.@ColorValue));
    //        WriteAttribute(@"BackColorValue", string.Empty, (o.@BackColorValue));
    //        if ((o.@Alignment) != global::System.Drawing.StringAlignment.@Center)
    //        {
    //            localWriter.WriteAttributeString(@"Alignment", null, Write119_StringAlignment((o.@Alignment)));
    //        }
    //        if ((o.@LineAlignment) != global::System.Drawing.StringAlignment.@Center)
    //        {
    //            localWriter.WriteAttributeString(@"LineAlignment", null, Write119_StringAlignment((o.@LineAlignment)));
    //        }
    //        if ((o.@Left) != 0f)
    //        {
    //            localWriter.WriteAttributeString(@"Left", null, DCXMLConvert.ToString((o.@Left)));
    //        }
    //        if ((o.@Top) != 0f)
    //        {
    //            localWriter.WriteAttributeString(@"Top", null, DCXMLConvert.ToString((o.@Top)));
    //        }
    //        if ((o.@Width) != 100f)
    //        {
    //            localWriter.WriteAttributeString(@"Width", null, DCXMLConvert.ToString((o.@Width)));
    //        }
    //        if ((o.@Height) != 100f)
    //        {
    //            localWriter.WriteAttributeString(@"Height", null, DCXMLConvert.ToString((o.@Height)));
    //        }
    //        if ((o.@PositionFixModeForAutoHeightLine) != global::DCSoft.TemperatureChart.LabelPositionFixMode.@None)
    //        {
    //            localWriter.WriteAttributeString(@"PositionFixModeForAutoHeightLine", null, Write217_LabelPositionFixMode((o.@PositionFixModeForAutoHeightLine)));
    //        }
    //        Write34_XImageValue(@"Image", string.Empty, (o.@Image), false, false);
    //        if ((o.@ShowBorder) != false)
    //        {
    //            MyWriteElementStringRaw(@"ShowBorder", DCXMLConvert.ToString((o.@ShowBorder)));
    //        }
    //        Write64_XFontValue(@"Font", string.Empty, (o.@Font), false, false);
    //        WriteEndElement(o);
    //    }
    //    string Write217_LabelPositionFixMode(global::DCSoft.TemperatureChart.LabelPositionFixMode v)
    //    {
    //        string s = null;
    //        switch (v)
    //        {
    //            case global::DCSoft.TemperatureChart.LabelPositionFixMode.@None: s = @"None"; break;
    //            case global::DCSoft.TemperatureChart.LabelPositionFixMode.@InsideDataGrid: s = @"InsideDataGrid"; break;
    //            case global::DCSoft.TemperatureChart.LabelPositionFixMode.@InsideAutoHeightLine: s = @"InsideAutoHeightLine"; break;
    //            case global::DCSoft.TemperatureChart.LabelPositionFixMode.@AboveAutoHeightLine: s = @"AboveAutoHeightLine"; break;
    //            default: throw CreateInvalidEnumValueException((long)v, typeof(DCSoft.TemperatureChart.LabelPositionFixMode));
    //        }
    //        return s;
    //    }
    //    internal protected void Write216_DCTimeLineImage(string n, string ns, global::DCSoft.TemperatureChart.DCTimeLineImage o, bool isNullable, bool needType)
    //    {
    //        this._BaseWriter = base.Writer;
    //        var localWriter = base.Writer;
    //        if (o == null)
    //        {
    //            //if (isNullable) WriteNullTagLiteral(n, ns);
    //            return;
    //        }
    //        if (!needType)
    //        {
    //            System.Type t = o.GetType();
    //            if (t == typeof(global::DCSoft.TemperatureChart.DCTimeLineImage))
    //            {
    //            }
    //            else
    //            {
    //                throw CreateUnknownTypeException(o);
    //            }
    //        }
    //        WriteStartElement(n, ns, o, false, null);
    //        if (needType) WriteXsiType(@"DCTimeLineImage", string.Empty);
    //        WriteAttribute(@"Name", string.Empty, (o.@Name));
    //        if ((o.@Left) != 0f)
    //        {
    //            localWriter.WriteAttributeString(@"Left", null, DCXMLConvert.ToString((o.@Left)));
    //        }
    //        if ((o.@Top) != 0f)
    //        {
    //            localWriter.WriteAttributeString(@"Top", null, DCXMLConvert.ToString((o.@Top)));
    //        }
    //        Write34_XImageValue(@"Image", string.Empty, (o.@Image), false, false);
    //        WriteEndElement(o);
    //    }
    //    string Write215_DCTimeUnit(global::DCSoft.TemperatureChart.DCTimeUnit v)
    //    {
    //        string s = null;
    //        switch (v)
    //        {
    //            case global::DCSoft.TemperatureChart.DCTimeUnit.@Second: s = @"Second"; break;
    //            case global::DCSoft.TemperatureChart.DCTimeUnit.@Minute: s = @"Minute"; break;
    //            case global::DCSoft.TemperatureChart.DCTimeUnit.@Hour: s = @"Hour"; break;
    //            case global::DCSoft.TemperatureChart.DCTimeUnit.@Day: s = @"Day"; break;
    //            case global::DCSoft.TemperatureChart.DCTimeUnit.@Week: s = @"Week"; break;
    //            case global::DCSoft.TemperatureChart.DCTimeUnit.@Month: s = @"Month"; break;
    //            case global::DCSoft.TemperatureChart.DCTimeUnit.@Year: s = @"Year"; break;
    //            default: throw CreateInvalidEnumValueException((long)v, typeof(DCSoft.TemperatureChart.DCTimeUnit));
    //        }
    //        return s;
    //    }
    //    string Write214_DCTimeLineSelectionMode(global::DCSoft.TemperatureChart.DCTimeLineSelectionMode v)
    //    {
    //        string s = null;
    //        switch (v)
    //        {
    //            case global::DCSoft.TemperatureChart.DCTimeLineSelectionMode.@None: s = @"None"; break;
    //            case global::DCSoft.TemperatureChart.DCTimeLineSelectionMode.@SingleSelect: s = @"SingleSelect"; break;
    //            case global::DCSoft.TemperatureChart.DCTimeLineSelectionMode.@MultiSelec: s = @"MultiSelec"; break;
    //            default: throw CreateInvalidEnumValueException((long)v, typeof(DCSoft.TemperatureChart.DCTimeLineSelectionMode));
    //        }
    //        return s;
    //    }
    //    string Write213_EditValuePointEventHandleMode(global::DCSoft.TemperatureChart.EditValuePointEventHandleMode v)
    //    {
    //        string s = null;
    //        switch (v)
    //        {
    //            case global::DCSoft.TemperatureChart.EditValuePointEventHandleMode.@None: s = @"None"; break;
    //            case global::DCSoft.TemperatureChart.EditValuePointEventHandleMode.@Program: s = @"Program"; break;
    //            case global::DCSoft.TemperatureChart.EditValuePointEventHandleMode.@Silent: s = @"Silent"; break;
    //            case global::DCSoft.TemperatureChart.EditValuePointEventHandleMode.@OwnedUI: s = @"OwnedUI"; break;
    //            default: throw CreateInvalidEnumValueException((long)v, typeof(DCSoft.TemperatureChart.EditValuePointEventHandleMode));
    //        }
    //        return s;
    //    }
    //    string Write212_DocumentLinkVisualStyle(global::DCSoft.TemperatureChart.DocumentLinkVisualStyle v)
    //    {
    //        string s = null;
    //        switch (v)
    //        {
    //            case global::DCSoft.TemperatureChart.DocumentLinkVisualStyle.@None: s = @"None"; break;
    //            case global::DCSoft.TemperatureChart.DocumentLinkVisualStyle.@Hover: s = @"Hover"; break;
    //            case global::DCSoft.TemperatureChart.DocumentLinkVisualStyle.@Always: s = @"Always"; break;
    //            default: throw CreateInvalidEnumValueException((long)v, typeof(DCSoft.TemperatureChart.DocumentLinkVisualStyle));
    //        }
    //        return s;
    //    }
    //}

    //public class DCXmlSerializationReader : System.Xml.Serialization.XmlSerializationReader
    //{
    //    protected override void InitCallbacks()
    //    {
    //    }

    //    protected override void InitIDs()
    //    {
    //    }
    //}
    //public class DCXmlSerializationWriter : System.Xml.Serialization.XmlSerializationWriter
    //{
    //    static DCXmlSerializationWriter()
    //    {
    //        var v = System.TimeZone.CurrentTimeZone;
    //        var sp = v.GetUtcOffset(System.DateTime.Now);
    //        _TimeZone = "+" + sp.Hours.ToString("00") + ":" + sp.Minutes.ToString("00");
    //    }
    //    private static readonly string _TimeZone;
    //    protected override void InitCallbacks()
    //    {
    //    }
    //}
    #endregion

    internal static class DCXMLConvert
    {
        private static readonly string[] _AllDateTimeForamts = new string[] {
            "yyyy-MM-ddTHH:mm:ss.FFFFFFFzzzzzz",
            "yyyy-MM-ddTHH:mm:ss.FFFFFFF",
            "yyyy-MM-ddTHH:mm:ss.FFFFFFFZ",
            "HH:mm:ss.FFFFFFF",
            "HH:mm:ss.FFFFFFFZ",
            "HH:mm:ss.FFFFFFFzzzzzz",
            "yyyy-MM-dd",
            "yyyy-MM-ddZ",
            "yyyy-MM-ddzzzzzz",
            "yyyy-MM",
            "yyyy-MMZ",
            "yyyy-MMzzzzzz",
            "yyyy",
            "yyyyZ",
            "yyyyzzzzzz",
            "--MM-dd",
            "--MM-ddZ",
            "--MM-ddzzzzzz",
            "---dd",
            "---ddZ",
            "---ddzzzzzz",
            "--MM--",
            "--MM--Z",
            "--MM--zzzzzz"};
        public static DateTime ToDateTime(string v)
        {
            if (v == null || v.Length == 0)
            {
                return DateTime.MinValue;
            }
            else
            {
                return System.DateTime.ParseExact(
                    v,
                    _AllDateTimeForamts,
                    System.Globalization.DateTimeFormatInfo.InvariantInfo,
                    System.Globalization.DateTimeStyles.AllowLeadingWhite
                    | System.Globalization.DateTimeStyles.AllowTrailingWhite);
            }
        }

        public static bool ToBoolean(string v)
        {
            if (v == "true") return true;
            else if (v == "false") return false;
            if (v == null || v.Length == 0)
            {
                return false;
            }
            else
            {
                v = v.Trim().ToLower();
                if (v == "true" || v == "1") return true;
                else if (v == "false" || v == "0") return false;
                throw new FormatException("XML-Boolean:" + v);
            }
        }
        public static int ToInt32(string v)
        {
            if (v == null || v.Length == 0)
            {
                return 0;
            }
            return int.Parse(v);
        }
        public static char ToChar(string v)
        {
            if (v != null && v.Length > 0)
            {
                return v[0];
            }
            else
            {
                throw new ArgumentNullException("v");
            }
        }
        public static string ToString(int v)
        {
            return DCSoft.Common.StringCommon.Int32ToString(v);
        }
        public static string ToString(float value)
        {
            if (float.IsNegativeInfinity(value))
            {
                return "-INF";
            }
            if (float.IsPositiveInfinity(value))
            {
                return "INF";
            }
            return DCSoft.Common.StringCommon.FloatToString(value);
        }
        public static string ToString(bool v)
        {
            return v ? "true" : "false";
        }
        public static string ToString(double value)
        {
            if (double.IsNegativeInfinity(value))
            {
                return "-INF";
            }
            if (double.IsPositiveInfinity(value))
            {
                return "INF";
            }
            return value.ToString("R", System.Globalization.NumberFormatInfo.InvariantInfo);
        }
    }

    
}