using System;
using System.IO;
using System.Xml;
using System.Text;
using System.ComponentModel;
//using System.Xml.Serialization;
using System.Reflection;
using System.Drawing;
using System.Collections.Generic ;

namespace DCSoft.Common
{
	/// <summary>
	/// 处理XML文档的通用模块
	/// </summary>
    /// <remarks>编制 袁永福</remarks>
    [System.Runtime.InteropServices.ComVisible(false)]
#if ! DOTNETCORE
    //[System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = false )]
#endif
    public static class XMLHelper
	{
//#if !DCWriterForWASM
//        /// <summary>
//        /// 删除所有的子节点
//        /// </summary>
//        /// <param name="rootNode">根节点对象</param>
//        /// <returns>删除的节点个数</returns>
//        public static int RemoveAllChildNodes( System.Xml.XmlNode rootNode )
//        {
//            if(rootNode == null )
//            {
//                throw new ArgumentNullException("rootNode");
//            }
//            int result = 0;
//            while( true )
//            {
//                var fc = rootNode.FirstChild;
//                if( fc == null )
//                {
//                    break;
//                }
//                else
//                {
//                    rootNode.RemoveChild(fc);
//                    result++;
//                }
//            }
//            return result;
//        }
//#endif
        //public static bool BinaryToXmlElement(System.Xml.XmlElement rootElement, byte[] bsData)
        //{
        //    if (bsData == null || bsData.Length == 0)
        //    {
        //        throw new ArgumentNullException("bsData");
        //    }
        //    if (rootElement == null)
        //    {
        //        throw new ArgumentNullException("rootNode");
        //    }
        //    var reader = new DCBinaryReader(bsData);
        //    if (reader.CheckReadInt32(324363) == false)
        //    {
        //        return false;
        //    }
        //    var namesLength = reader.ReadInt16();
        //    var names = new string[namesLength];
        //    for (int iCount = 0; iCount < namesLength; iCount++)
        //    {
        //        names[iCount] = reader.ReadString();
        //    }
        //    var table = new MyNameTable(names, reader);
        //    ReadXmlAttributeListFromBinary(rootElement.OwnerDocument, rootElement, reader, table);
        //    ReadXmlChildNodesFromBinary(rootElement.OwnerDocument, rootElement, reader, table);
        //    table.Clear();
        //    return true;
        //}


        //private static void ReadXmlChildNodesFromBinary(
        //    System.Xml.XmlDocument doc,
        //    System.Xml.XmlElement rootElement,
        //    DCBinaryReader reader,
        //    MyNameTable table)
        //{
        //    int nodeCount = reader.ReadInt16();
        //    bool replaceMode = false;
        //    if( rootElement.ChildNodes.Count > 0 )
        //    {
        //        foreach( var node in rootElement.ChildNodes)
        //        {
        //            if(node is System.Xml.XmlElement )
        //            {
        //                replaceMode = true;
        //                break;
        //            }
        //        }
        //    }
        //    for (int iCount = 0; iCount < nodeCount; iCount++)
        //    {
        //        var fullType = reader.ReadByte();
        //        var eT = fullType & 15;
        //        switch (eT)
        //        {
        //            case XMLNodeType_CDATA:
        //                rootElement.AppendChild(
        //                    doc.CreateCDataSection(table.ReadStringByIndex()));
        //                break;
        //            case XMLNodeType_Comment:
        //                rootElement.AppendChild(
        //                    doc.CreateComment(table.ReadStringByIndex()));
        //                break;
        //            case XMLNodeType_SimpleTextElement:
        //                {
        //                    // 简单文本元素
        //                    var e = doc.CreateElement(table.ReadStringByIndex());
        //                    e.IsEmpty = false;
        //                    rootElement.AppendChild(e);
        //                    e.AppendChild(doc.CreateTextNode(table.ReadStringByIndex()));
        //                }
        //                break;
        //            case XMLNodeType_Element:
        //                {
        //                    System.Xml.XmlElement ce = null;
        //                    System.Xml.XmlElement oldElement = null;
        //                    if ((fullType & XMLNodeType_Flag_Prefix) == 0)
        //                    {
        //                        // 名称无前缀
        //                        ce = doc.CreateElement(table.ReadStringByIndex());
        //                        if(replaceMode)
        //                        {
        //                            foreach( System.Xml.XmlNode node2 in rootElement.ChildNodes )
        //                            {
        //                                if( node2.Name == ce.Name )
        //                                {
        //                                    oldElement =( System.Xml.XmlElement ) node2;
        //                                    break;
        //                                }
        //                            }
        //                        }
        //                    }
        //                    else
        //                    {
        //                        // 名称有前缀
        //                        ce = doc.CreateElement(
        //                            table.ReadStringByIndex(),
        //                            table.ReadStringByIndex(),
        //                            table.ReadStringByIndex());
        //                        if (replaceMode)
        //                        {
        //                            foreach (System.Xml.XmlNode node2 in rootElement.ChildNodes)
        //                            {
        //                                if (node2.Name == ce.Name || node2.Prefix == ce.Prefix)
        //                                {
        //                                    oldElement = (System.Xml.XmlElement)node2;
        //                                    break;
        //                                }
        //                            }
        //                        }
        //                    }
        //                    if (oldElement == null)
        //                    {
        //                        rootElement.AppendChild(ce);
        //                    }
        //                    else
        //                    {
        //                        rootElement.ReplaceChild(ce, oldElement);
        //                    }
        //                    if ((fullType & XMLNodeType_Flag_Empty) == 0)
        //                    {
        //                        // 不是短格式 
        //                        ce.IsEmpty = false;
        //                        if ((fullType & XMLNodeType_Flag_HasAttribute) != 0)
        //                        {
        //                            // 具有属性
        //                            ReadXmlAttributeListFromBinary(doc, ce, reader, table);
        //                        }
        //                        if ((fullType & XMLNodeType_Flag_HasChild) != 0)
        //                        {
        //                            // 具有子节点
        //                            ReadXmlChildNodesFromBinary(doc, ce, reader, table);
        //                        }
        //                    }
        //                    else
        //                    {
        //                        // 短格式
        //                        ce.IsEmpty = true;
        //                        if ((fullType & XMLNodeType_Flag_HasAttribute) != 0)
        //                        {
        //                            // 具有属性
        //                            ReadXmlAttributeListFromBinary(doc, ce, reader, table);
        //                        }
        //                    }
        //                }
        //                break;
        //            case XMLNodeType_SignificantWhitespace:
        //                {
        //                    rootElement.AppendChild(
        //                        doc.CreateSignificantWhitespace(table.ReadStringByIndex()));
        //                }
        //                break;
        //            case XMLNodeType_Text:
        //                rootElement.AppendChild(
        //                    doc.CreateTextNode(table.ReadStringByIndex()));
        //                break;
        //            case XMLNodeType_Whitespace:
        //                rootElement.AppendChild(
        //                    doc.CreateWhitespace(table.ReadStringByIndex()));
        //                break;
        //        }
        //    }
        //}

        //public static byte[] XmlElementToBinary(System.Xml.XmlElement rootElement , bool encryptData)
        //{
        //    var writer = new DCBinaryWriter(encryptData);
        //    var table = new MyNameTable(rootElement);
        //    writer.Write(324363);
        //    var names = table.ToArray();
        //    if( names != null && names.Length > 0 )
        //    {
        //        writer.Write((short)names.Length);
        //        foreach( var item in names )
        //        {
        //            writer.Write(item);
        //        }
        //    }
        //    else
        //    {
        //        writer.Write((short)0);
        //    }
        //    WriteXmlAttributeListToBinary(writer, rootElement.Attributes, table);
        //    WriteXmlChildNodesToBinary(writer, rootElement, table);
        //    table.Clear();
        //    var result = writer.ToBytesArray();
        //    writer.Close();
        //    return result;
        //}

        /// <summary>
        /// 无转换文本块
        /// </summary>
        private const byte XMLNodeType_CDATA = 1;
        /// <summary>
        /// 注释
        /// </summary>
        private const byte XMLNodeType_Comment = 2;
        /// <summary>
        /// 元素
        /// </summary>
        private const byte XMLNodeType_Element = 3;
        /// <summary>
        /// 文本节点
        /// </summary>
        private const byte XMLNodeType_Text = 4;
        /// <summary>
        /// 空白字符元素
        /// </summary>
        private const byte XMLNodeType_Whitespace = 5;
        /// <summary>
        /// 空白字符元素
        /// </summary>
        private const byte XMLNodeType_SignificantWhitespace = 6;
        /// <summary>
        /// 简单文本元素
        /// </summary>
        private const byte XMLNodeType_SimpleTextElement = 7;
        /// <summary>
        /// 有属性
        /// </summary>
        private const byte XMLNodeType_Flag_HasAttribute = 16;
        /// <summary>
        /// 有子节点
        /// </summary>
        private const byte XMLNodeType_Flag_HasChild = 32;
        /// <summary>
        /// 元素名称具有前缀
        /// </summary>
        private const byte XMLNodeType_Flag_Prefix = 64;
        /// <summary>
        /// 空白元素标记
        /// </summary>
        private const byte XMLNodeType_Flag_Empty = 128;

        //private static void WriteXmlChildNodesToBinary(
        //    DCBinaryWriter writer,
        //    System.Xml.XmlNode rootNode,
        //    MyNameTable table)
        //{
        //    if ( rootNode.FirstChild == null )
        //    {
        //        writer.Write((short)0);
        //        return;
        //    }
        //    writer.Write((short)rootNode.ChildNodes.Count);
        //    var curNode = rootNode.FirstChild;
        //    while( curNode != null )
        //    {
        //        if (curNode is XmlElement)
        //        {
        //            var element = (XmlElement)curNode;
        //            byte elementType = XMLNodeType_Element;
        //            if (element.IsEmpty)
        //            {
        //                // 短格式
        //                elementType += XMLNodeType_Flag_Empty;
        //            }
        //            if (element.Attributes.Count > 0)
        //            {
        //                // 具有属性
        //                elementType += XMLNodeType_Flag_HasAttribute;
        //            }
        //            var firstChild = element.FirstChild;
        //            if (firstChild != null)
        //            {
        //                // 具有子节点
        //                elementType += XMLNodeType_Flag_HasChild;
        //            }
        //            var noPrefix = string.IsNullOrEmpty(element.Prefix);
        //            if (noPrefix == false)
        //            {
        //                // 名称具有前缀
        //                elementType += XMLNodeType_Flag_Prefix;
        //            }
        //            if (elementType == XMLNodeType_Element + XMLNodeType_Flag_HasChild
        //                && firstChild is System.Xml.XmlText
        //                && firstChild.NextSibling == null)
        //            {
        //                // 很多情况下，XML元素没有属性,没有前缀，只有一个纯文本节点，则特殊处理
        //                writer.Write(XMLNodeType_SimpleTextElement);
        //                writer.Write(table.GetIndex(element.Name));
        //                writer.Write(table.GetIndex(firstChild.Value));
        //            }
        //            else
        //            {
        //                writer.Write(elementType);
        //                if (noPrefix)
        //                {
        //                    writer.Write(table.GetIndex(element.Name));
        //                }
        //                else
        //                {
        //                    writer.Write(table.GetIndex(element.Prefix));
        //                    writer.Write(table.GetIndex(element.LocalName));
        //                    writer.Write(table.GetIndex(element.NamespaceURI));
        //                }
        //                if ((elementType & XMLNodeType_Flag_HasAttribute) != 0)
        //                {
        //                    WriteXmlAttributeListToBinary(writer, element.Attributes, table);
        //                }
        //                if ( firstChild != null )// (elementType & XMLNodeType_Flag_HasChild) != 0)
        //                {
        //                    WriteXmlChildNodesToBinary(writer, element, table);
        //                }
        //            }
        //        }
        //        else if (curNode is XmlText)
        //        {
        //            writer.Write(XMLNodeType_Text);
        //            writer.Write(table.GetIndex(curNode.Value));
        //        }
        //        else if (curNode is XmlCDataSection)
        //        {
        //            writer.Write(XMLNodeType_CDATA);
        //            writer.Write(table.GetIndex(curNode.Value));
        //        }
        //        else if (curNode is XmlComment)
        //        {
        //            writer.Write(XMLNodeType_Comment);
        //            writer.Write(table.GetIndex(curNode.Value));
        //        }
        //        else if (curNode is XmlSignificantWhitespace)
        //        {
        //            writer.Write(XMLNodeType_SignificantWhitespace);
        //            writer.Write(table.GetIndex(curNode.Value));
        //        }
        //        else if (curNode is XmlWhitespace)
        //        {
        //            writer.Write(XMLNodeType_Whitespace);
        //            writer.Write(table.GetIndex(curNode.Value));
        //        }
        //        curNode = curNode.NextSibling;
        //    }//foreach
        //}

        //private static void ReadXmlAttributeListFromBinary(
        //    System.Xml.XmlDocument doc,
        //    System.Xml.XmlElement rootElement,
        //    DCBinaryReader reader,
        //    MyNameTable table)
        //{
        //    int nodeCount = reader.ReadInt16();
        //    if (nodeCount > 0)
        //    {
        //        // 已经存在属性，则采用替换模式
        //        bool replaceMode = rootElement.Attributes.Count > 0;
        //        for (int iCount = 0; iCount < nodeCount; iCount++)
        //        {
        //            var index = reader.ReadInt16();
        //            System.Xml.XmlAttribute attr = null;
        //            if (index < 0)
        //            {
        //                attr = doc.CreateAttribute(table.GetValue((short)-index));
        //                attr.Value = table.ReadStringByIndex();
        //                if( replaceMode)
        //                {
        //                    rootElement.RemoveAttribute(attr.Name);
        //                }
        //            }
        //            else
        //            {
        //                attr = doc.CreateAttribute(
        //                    table.GetValue(index),
        //                    table.ReadStringByIndex(),
        //                    table.ReadStringByIndex());
        //                attr.Value = table.ReadStringByIndex();
        //                if( replaceMode )
        //                {
        //                    rootElement.RemoveAttribute(attr.LocalName, attr.NamespaceURI);
        //                }
        //            }
        //            rootElement.Attributes.Append(attr);
        //        }
        //    }
        //}

        //private static void WriteXmlAttributeListToBinary(
        //    DCBinaryWriter writer,
        //    System.Xml.XmlAttributeCollection attrs,
        //    MyNameTable table)
        //{
        //    if (writer.WriteListCountUseInt16(attrs))
        //    {
        //        foreach (System.Xml.XmlAttribute attr in attrs)
        //        {
        //            if (string.IsNullOrEmpty(attr.Prefix))
        //            {
        //                writer.Write((short)(-table.GetIndex(attr.Name)));
        //            }
        //            else
        //            {
        //                writer.Write(table.GetIndex(attr.Prefix));
        //                writer.Write(table.GetIndex(attr.LocalName));
        //                writer.Write(table.GetIndex(attr.NamespaceURI));
        //            }
        //            writer.Write(table.GetIndex(attr.Value));
        //        }
        //    }
        //}
        //private class MyNameTable
        //{
        //    private Dictionary<string, short> _Table = null;
        //    public MyNameTable(System.Xml.XmlNode rootNode)
        //    {
        //        this._Table = new Dictionary<string, short>();
        //        BuildTable(rootNode);
        //    }
        //    private void BuildTable(System.Xml.XmlNode rootNode)
        //    {
        //        if (rootNode is XmlElement)
        //        {
        //            var e = (XmlElement)rootNode;
        //            if (string.IsNullOrEmpty(e.Prefix))
        //            {
        //                AddValue(e.Name);
        //            }
        //            else
        //            {
        //                AddValue(e.Prefix);
        //                AddValue(e.LocalName);
        //                AddValue(e.NamespaceURI);
        //            }
        //            if (e.Attributes != null && e.Attributes.Count > 0)
        //            {
        //                foreach (System.Xml.XmlAttribute a in e.Attributes)
        //                {
        //                    if (string.IsNullOrEmpty(a.Prefix))
        //                    {
        //                        AddValue(a.Name);
        //                    }
        //                    else
        //                    {
        //                        AddValue(a.Prefix);
        //                        AddValue(a.LocalName);
        //                        AddValue(a.NamespaceURI);
        //                    }
        //                    AddValue(a.Value);
        //                }
        //            }
        //            var curNode = e.FirstChild;
        //            while( curNode != null )
        //            {
        //                if( curNode is XmlCharacterData )
        //                {
        //                    AddValue(curNode.Value);
        //                }
        //                else
        //                {
        //                    BuildTable(curNode);
        //                }
        //                curNode = curNode.NextSibling;
        //            }
        //            //foreach (System.Xml.XmlNode node2 in e.ChildNodes)
        //            //{
        //            //    if (node2 is XmlCharacterData)
        //            //    {
        //            //        AddValue(node2.Value);
        //            //    }
        //            //    else
        //            //    {
        //            //        BuildTable(node2);
        //            //    }
        //            //}
        //        }
        //    }
        //    private void AddValue(string v)
        //    {
        //        if (v != null 
        //            && v.Length > 0 
        //            && this._Table.ContainsKey(v) == false)
        //        {
        //            this._Table.Add(v, (short)(this._Table.Count + 1 ));
        //        }
        //    }

        //    public MyNameTable(string[] vs, DCBinaryReader reader)
        //    {
        //        this._Values = vs;
        //        this._Reader = reader;
        //    }
        //    private DCBinaryReader _Reader = null;
        //    public Dictionary<string, short> Table
        //    {
        //        get
        //        {
        //            return this._Table;
        //        }
        //    }

        //    public short GetIndex(string v)
        //    {
        //        if (v == null)
        //        {
        //            return -1;
        //        }
        //        if (v.Length == 0)
        //        {
        //            return -2;
        //        }
        //        return this._Table[v];
        //        //short index = 0;
        //        //if (this._Table.TryGetValue(v, out index) == false)
        //        //{
        //        //    throw new NotSupportedException(v);
        //        //    //index = (short)this._Table.Count;
        //        //    //this._Table.Add(v, index);
        //        //}
        //        //return index;
        //    }
        //    public string[] ToArray()
        //    {
        //        if (this._Table.Count == 0)
        //        {
        //            return null;
        //        }
        //        var result = new string[this._Table.Count + 1];
        //        result[0] = null;
        //        foreach (var item in this._Table)
        //        {
        //            result[item.Value] = item.Key;
        //        }
        //        return result;
        //    }
        //    public string ReadStringByIndex()
        //    {
        //        var index = this._Reader.ReadInt16();
        //        if (index == -1)
        //        {
        //            return null;
        //        }
        //        if (index == -2)
        //        {
        //            return string.Empty;
        //        }
        //        return this._Values[index];
        //    }

        //    public string GetValue(short index)
        //    {
        //        if (index == -1)
        //        {
        //            return null;
        //        }
        //        if (index == -2)
        //        {
        //            return string.Empty;
        //        }
        //        return this._Values[index];
        //    }
        //    private string[] _Values = null;
        //    public void Clear()
        //    {
        //        if(this._Table != null )
        //        {
        //            this._Table.Clear();
        //            this._Table = null;
        //        }
        //        this._Values = null;
        //        this._Reader = null;
        //    }
        //}

       

        //private const string BaseEntryChars = " <>&\"'￠£¥€§©®™×÷";
       
//#if ! DCWriterForWASM
//        public static string ToXMLEntryRandom(string text, double rate)
//        {
//            if (string.IsNullOrEmpty(text))
//            {
//                return text;
//            }
//            Random rnd = new Random();
//            System.Text.StringBuilder str = new System.Text.StringBuilder();
//            foreach (char c in text)
//            {
//                if ( rnd.NextDouble() < rate || BaseEntryChars.IndexOf( c ) >= 0  )
//                {
//                    str.Append("&#x" + Convert.ToInt32(c).ToString("X") + ";");
//                }
//                else
//                {
//                    str.Append(c);
//                }
//            }
//            return str.ToString();
//        }
//#endif
        /// <summary>
        /// 清除XML的头文本
        /// </summary>
        /// <param name="strXML"></param>
        /// <returns></returns>
        public static string CleanupXMLHeader(string strXML)
        {
            if (strXML != null && strXML.Length > 0)
            {
                int startIndex = strXML.IndexOf("?>", 0, Math.Min( 100 , strXML.Length) , StringComparison.Ordinal);
                if (startIndex > 0)
                {
                    startIndex += 2;
                }
                else
                {
                    startIndex = 0;
                }
                int index = strXML.IndexOf('<', startIndex, Math.Min( 100 , strXML.Length - startIndex));
                if (index > 0)
                {
                    return strXML.Substring(index).Trim();
                }
               
            }
            return strXML;

           
        }

        ///// <summary>
        ///// 保存对象到一个XML字符串中
        ///// </summary>
        ///// <param name="instance">对象实例</param>
        ///// <returns>XML字符串</returns>
        //public static string SaveObjectToXMLString(object instance , XmlSerializer ser = null )
        //{
        //    if (instance == null)
        //    {
        //        throw new ArgumentNullException("instance");
        //    }
        //    StringWriter writer = new StringWriter();
        //    XmlTextWriter xmlWriter = new XmlTextWriter(writer);
        //    xmlWriter.Formatting = Formatting.None;
        //    SaveObjectToXMLWriter(instance, xmlWriter , ser );
        //    xmlWriter.Close();
        //    string xml = writer.ToString();
        //    xml = CleanupXMLHeader(xml);
        //    return xml;
        //}

        ///// <summary>
        ///// 保存对象到一个带缩进的XML字符串中
        ///// </summary>
        ///// <param name="instance">对象</param>
        ///// <returns>XML字符串</returns>
        ////[System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        //public static string SaveObjectToIndentXMLString(object instance)
        //{
        //    if (instance == null)
        //    {
        //        throw new ArgumentNullException("instance");
        //    }
        //    StringWriter writer = new StringWriter();
        //    XmlTextWriter xmlWriter = new XmlTextWriter(writer);
        //    xmlWriter.Formatting = Formatting.Indented;
        //    xmlWriter.Indentation = 3;
        //    xmlWriter.IndentChar = ' ';
        //    if (instance is System.Xml.XmlNode)
        //    {
        //        ((System.Xml.XmlNode)instance).WriteTo(xmlWriter);
        //    }
        //    else
        //    {
        //        SaveObjectToXMLWriter(instance, xmlWriter);
        //    }
        //    xmlWriter.Close();
        //    string xml = writer.ToString();
        //    xml = CleanupXMLHeader(xml);
        //    return xml;
        //}

        //public static object LoadObjectFromXMLString(Type type, string xml , XmlSerializer ser = null )
        //{
        //    //byte[] bs = System.Text.Encoding.UTF8.GetBytes(xml);
        //    //System.IO.MemoryStream ms = new MemoryStream(bs);
        //    //XmlSerializer ser = new XmlSerializer(type);
        //    //object result = ser.Deserialize(ms);
        //    if (string.IsNullOrEmpty(xml))
        //    {
        //        return null;
        //    }
        //    StringReader reader = new StringReader(xml);
        //    XmlTextReader xmlReader = new XmlTextReader(reader);
        //    object result = LoadObjectFromXMLReader(type, xmlReader , ser );
        //    reader.Close();
        //    return result;
        //}

        //public static object LoadObjectFromXMLReader(Type type, XmlTextReader reader , XmlSerializer ser = null )
        //{
        //    reader.Normalization = false;
        //    if (type.IsPrimitive || type.Equals(typeof(string)) || type.Equals(typeof(DateTime)))
        //    {
        //        reader.ReadStartElement();
        //        string text = reader.ReadElementString();
        //        return Convert.ChangeType(text, type);
        //    }
        //    else
        //    {
        //        if (ser == null)
        //        {
        //            XmlSerializer ser2 = new XmlSerializer(type);
        //            object result = ser2.Deserialize(reader);
        //            return result;
        //        }
        //        else
        //        {
        //            object result = ser.Deserialize(reader);
        //            return result;
        //        }
        //    }
        //}


        //public static void SaveObjectToXMLWriter(object instance, XmlTextWriter writer , XmlSerializer ser = null )
        //{
        //    Type t = instance.GetType();
        //    //writer.Formatting = Formatting.None;

        //    if (t.IsPrimitive || t.Equals(typeof(string)) || t.Equals(typeof(DateTime)))
        //    {
        //        writer.WriteStartDocument();
        //        writer.WriteStartElement("Value");
        //        writer.WriteString(Convert.ToString(instance));
        //        writer.WriteEndElement();
        //        writer.WriteEndDocument();
        //    }
        //    else
        //    {
        //        if (ser == null)
        //        {
        //            XmlSerializer ser2 = new XmlSerializer(instance.GetType());
        //            ser2.Serialize(writer, instance);
        //        }
        //        else
        //        {
        //            ser.Serialize(writer, instance);
        //        }
        //    }
        //}

//#if !DCWriterForWASM
//        public static void SaveObjectToXMLFile(object instance, string fileName)
//        {
//            if (instance == null)
//            {
//                throw new ArgumentNullException("instance");
//            }
//            if (string.IsNullOrEmpty(fileName))
//            {
//                throw new ArgumentNullException("fileName");
//            }
//            using (StreamWriter writer = new StreamWriter(fileName, false, Encoding.UTF8))
//            {
//                XmlTextWriter xmlWriter = new XmlTextWriter(writer);
//                SaveObjectToXMLWriter(instance, xmlWriter);
//            }
//        }

//        public static object LoadObjectFromXMLFile(Type type, string fileName)
//        {
//            if (type == null)
//            {
//                throw new ArgumentNullException("type");
//            }
//            if (string.IsNullOrEmpty(fileName))
//            {
//                throw new ArgumentNullException("fileName");
//            }
//            if (System.IO.File.Exists(fileName) == false)
//            {
//                throw new FileNotFoundException(fileName);
//            }
//            using (StreamReader reader = new StreamReader(fileName, Encoding.UTF8, true))
//            {
//                XmlTextReader xReader = new XmlTextReader(reader);
//                object result = LoadObjectFromXMLReader(type, xReader);
//                return result;
//            }
//        }
//#endif
        public const string XML_Stylesheet = "xml-stylesheet";
		public const string XMLContentType = "text/xml";
		/// <summary>
		/// XSL前缀元素名称空间字符串
		/// </summary>
		public const string XslNamespaceURI = "http://www.w3.org/1999/XSL/Transform";

       

        /// <summary>
        /// 根据路径字符串创建XML节点
        /// </summary>
        /// <param name="RootNode">根节点</param>
        /// <param name="strPath">路径字符串</param>
        /// <param name="Create">
        /// 创建节点的模式 0:若未找到节点则退出函数 , 
        /// 1:若未找到节点则创建节点 2:不查找节点,立即创建节点</param>
        /// <param name="nsm">名称空间管理对象</param>
        /// <returns>找到或创建的XML节点</returns>
        public static System.Xml.XmlNode CreateXMLNodeByPath( 
			System.Xml.XmlNode RootNode , 
			string strPath ,
			int Create , 
			System.Xml.XmlNamespaceManager nsm )
		{
			if( Create != 0 && Create != 1 && Create != 2 )
				throw new System.ArgumentException("Create参数无效");
			if( RootNode == null )
				throw new System.ArgumentNullException("RootNode" , "未指定根节点");
			if( strPath == null )
				throw new System.ArgumentNullException("strPath" , "未指定路径");
			if( System.Xml.XmlReader.IsName( strPath ))
			{
				if( Create == 0 || Create == 1 )
				{
					foreach( System.Xml.XmlNode n in RootNode.ChildNodes )
					{
						if( n.Name == strPath )
						{
							return n ;
						}
					}
				}
				if( Create == 1 || Create == 2 )
				{
					System.Xml.XmlElement n2 = RootNode.OwnerDocument.CreateElement( strPath );
					RootNode.AppendChild( n2 );
					return n2 ;
				}
			}
			System.Xml.XmlNode node = null ;
			if( Create == 0 || Create == 1 )
			{
				node = RootNode.SelectSingleNode( strPath , nsm );
				if( node != null )
					return node ;
				if( Create == 0 )
					return null;
			}
			string[] strItems = strPath.Split('/');
			for( int iCount = 0 ; iCount < strItems.Length ; iCount ++ )
			{
				string strItem = strItems[ iCount ];
				strItem = strItem.Trim();
				if( strItem.StartsWith("@"))
				{
					strItem = strItem.Substring( 1 );
					strItem = strItem.Trim();
				}
				if( System.Xml.XmlReader.IsName( strItem ) == false )
				{
					return null;
				}
			}
			//System.Xml.XmlNode NewNode = null;
			System.Xml.XmlDocument doc = RootNode.OwnerDocument ;
			for( int iCount = 0 ; iCount < strItems.Length ; iCount ++ )
			{
				string strItem = strItems[ iCount ];
				strItem = strItem.Trim();
				if( strItem.StartsWith("@"))
				{
					if( RootNode.Attributes == null )
					{
						return null;
					}
					string strName = strItem.Substring( 1 );
					strName = strName.Trim();
					node = RootNode.Attributes[ strName ];
					if( node == null )
					{
						node = doc.CreateAttribute( strName );
						RootNode.Attributes.Append( ( System.Xml.XmlAttribute ) node );
						break;
					}
				}
				else
				{
					bool find = false;
					if( iCount != strItems.Length -1 || Create == 1 )
					{
						foreach( System.Xml.XmlNode node2 in RootNode.ChildNodes )
						{
							if( node2.Name == strItem )
							{
								node = node2 ;
								RootNode = node ;
								find = true;
								break;
							}
						}
					}
					if( find == false )
					{
						node = doc.CreateElement( strItem );
						RootNode.AppendChild( node );
						RootNode = node ;
					}
				}
			}//for( int iCount = 0 ; iCount < strItems.Length ; iCount ++ )
			return node ;
		}

      
        ///// <summary>
        ///// 获得指定名称的XML子节点
        ///// </summary>
        ///// <param name="RootElement">根元素</param>
        ///// <param name="strName">子节点的名称</param>
        ///// <returns>获得的XML子节点对象</returns>
        //public static System.Xml.XmlNode GetChildNode(System.Xml.XmlElement RootElement, string strName , bool autoCreate = false )
        //{
        //    if (RootElement == null)
        //    {
        //        throw new ArgumentNullException("RootElement");
        //    }
        //    if (strName == null)
        //    {
        //        throw new ArgumentNullException("strName");
        //    }
        //    foreach (System.Xml.XmlNode node in RootElement.ChildNodes)
        //    {
        //        if (node.Name == strName)
        //        {
        //            return node;
        //        }
        //    }
        //    if(autoCreate)
        //    {
        //        var newElement = RootElement.OwnerDocument.CreateElement(strName);
        //        RootElement.AppendChild(newElement);
        //        return newElement;
        //    }
        //    return null;
        //}
#if !DCWriterForWASM

	

        ///// <summary>
        ///// 自由的保存对象到一个带缩进的XML字符串中
        ///// </summary>
        ///// <param name="instance">对象</param>
        ///// <returns>XML字符串</returns>
        ////[System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        //public static string SaveObjectToIndentXMLStringFreedom(object instance)
        //{
        //    if (instance == null)
        //    {
        //        throw new ArgumentNullException("instance");
        //    }
        //    StringWriter writer = new StringWriter();
        //    XmlTextWriter xmlWriter = new XmlTextWriter(writer);
        //    xmlWriter.Formatting = Formatting.Indented;
        //    xmlWriter.Indentation = 3;
        //    xmlWriter.IndentChar = ' ';
        //    string name = GetNativeXmlName(instance.GetType());
        //    xmlWriter.WriteStartDocument();
        //    xmlWriter.WriteStartElement(name);
        //    MySerilizeXml(instance, xmlWriter, 0);
        //    xmlWriter.WriteEndElement();
        //    xmlWriter.WriteEndDocument();
        //    xmlWriter.Close();
        //    string xml = writer.ToString();
        //    xml = CleanupXMLHeader(xml);
        //    return xml;
        //}

        private class XmlPropertyInfo : IComparable
        {
            public static readonly object NoneDefaultValue = new object();
            public string Name = null;
            public PropertyInfo Property = null;
            public object DefaultValue = NoneDefaultValue ;
            /// <summary>
            /// 0:属性；1：XML元素；2：XML文本。
            /// </summary>
            public int ValueType = 1;
             
            public string DataType = null;
            public Attribute[] ArrayItemAttributes = null;

            public int CompareTo(object obj)
            {
                XmlPropertyInfo info = (XmlPropertyInfo)obj;
                if (info.ValueType != this.ValueType)
                {
                    return this.ValueType - info.ValueType;
                }
                return string.Compare(this.Name, ((XmlPropertyInfo)obj).Name);
            }
        }

        /// <summary>
        /// 要序列化的对象的原始名称
        /// </summary>
        private readonly static Dictionary<Type, string> _NativeXmlName = new Dictionary<Type, string>();
        //private static string GetNativeXmlName(Type t)
        //{
        //    if (_NativeXmlName.ContainsKey(t) == false)
        //    {
        //        string name = null;
        //        XmlTypeAttribute xta = (XmlTypeAttribute)Attribute.GetCustomAttribute(t, typeof(XmlTypeAttribute), true);
        //        if (xta != null)
        //        {
        //            name = xta.TypeName;
        //        }
        //        if (string.IsNullOrEmpty(name))
        //        {
        //            name = t.Name;
        //        }
        //        _NativeXmlName[t] = name;
        //    }
        //    return _NativeXmlName[t];
        //}

        /// <summary>
        /// 要序列化的对象属性信息字典
        /// </summary>
        private readonly static Dictionary<Type, List<XmlPropertyInfo>> _SerializeInfos = new Dictionary<Type, List<XmlPropertyInfo>>();
        //private static void MySerilizeXml(object instance, XmlWriter writer, int level)
        //{
        //    if (instance == null)
        //    {
        //        return;
        //    }
        //    if (level > 20)
        //    {
        //        // 超过20层则认为是异常的状态。
        //        throw new InvalidOperationException("level=" + level);
        //    }
        //    Type instanceType = instance.GetType();
            
        //    if (instanceType.IsPrimitive 
        //        || instance is string 
        //        || instance is decimal)
        //    {
        //        string v = instance.ToString();
        //        writer.WriteString(v);
        //        return;
        //    }
        //    if (instance is DateTime)
        //    {
        //        DateTime dtm = (DateTime)instance;
        //        writer.WriteString(DateTimeCommon.FastToYYYY_MM_DD_HH_MM_SS( dtm));
        //        return ;
        //    }
        //    if( instance is Color )
        //    {
        //        writer.WriteString( XMLSerializeHelper.ColorToString( ( Color) instance ));
        //        return ;
        //    }
        //    if( instanceType.IsClass == false )
        //    {
        //        string v = instance.ToString();
        //        writer.WriteString( v );
        //        return ;
        //    }
        //    if (instance is System.Collections.IList)
        //    {
        //        foreach (object obj in (System.Collections.IList)instance)
        //        {
        //            if (obj == null || DBNull.Value.Equals(obj))
        //            {
        //                continue;
        //            }
        //            string name = GetNativeXmlName(obj.GetType());
        //            writer.WriteStartElement(name);
        //            MySerilizeXml(obj, writer, level + 1);
        //            writer.WriteEndElement();
        //        }//foreach
        //        return;
        //    }
        //    List<XmlPropertyInfo> properties = null;
        //    if (_SerializeInfos.ContainsKey(instanceType))
        //    {
        //        // 加载缓存的数据
        //        properties = _SerializeInfos[instanceType];
        //    }
        //    else
        //    {
        //        // 没有缓存的数据则分析类型
        //        properties = new List<XmlPropertyInfo>();
        //        foreach (PropertyInfo p in instanceType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
        //        {
        //            XmlIgnoreAttribute xia = (XmlIgnoreAttribute)Attribute.GetCustomAttribute(p, typeof(XmlIgnoreAttribute), true);
        //            if (xia != null)
        //            {
        //                // 指明是不能XML序列化的
        //                continue;
        //            }
        //            if (p.CanRead == false)
        //            {
        //                // 指明是不可读的
        //                continue;
        //            }
        //            ParameterInfo[] ps = p.GetIndexParameters();
        //            if (ps != null && ps.Length > 0)
        //            {
        //                // 不处理有参数的属性
        //                continue;
        //            }
        //            XmlPropertyInfo info = new XmlPropertyInfo();
        //            info.Name = p.Name;
        //            info.Property = p;
        //            info.ValueType = 1;
        //            info.DefaultValue = XmlPropertyInfo.NoneDefaultValue;
        //            DefaultValueAttribute dva = (DefaultValueAttribute)Attribute.GetCustomAttribute(p, typeof(DefaultValueAttribute), true);
        //            if (dva != null)
        //            {
        //                // 默认值
        //                info.DefaultValue = dva.Value;
        //            }

        //            XmlElementAttribute xea = (XmlElementAttribute)Attribute.GetCustomAttribute(p, typeof(XmlElementAttribute), true);
        //            if (xea != null)
        //            {
        //                // 保存到XML元素中
        //                info.ValueType = 1;
        //                info.DataType = xea.DataType;
        //                if (string.IsNullOrEmpty(xea.ElementName) == false)
        //                {
        //                    info.Name = xea.ElementName;
        //                }
        //            }
        //            else
        //            {
        //                XmlAttributeAttribute xaa = (XmlAttributeAttribute)Attribute.GetCustomAttribute(p, typeof(XmlAttributeAttribute), true);
        //                if (xaa != null)
        //                {
        //                    // 保存到XML属性中
        //                    info.ValueType = 0;
        //                    info.DataType = xaa.DataType;
        //                    if (string.IsNullOrEmpty(xaa.AttributeName) == false)
        //                    {
        //                        info.Name = xaa.AttributeName;
        //                    }
        //                }
        //                else
        //                {
        //                    XmlTextAttribute xta = (XmlTextAttribute)Attribute.GetCustomAttribute(p, typeof(XmlTextAttribute), true);
        //                    if (xta != null)
        //                    {
        //                        // 保存XML文本中
        //                        info.ValueType = 2;
        //                        info.DataType = xta.DataType;
        //                    }
        //                }
        //            }
        //            Attribute[] xats = Attribute.GetCustomAttributes(p, typeof(XmlArrayItemAttribute), true);
        //            if (xats != null)
        //            {
        //                info.ArrayItemAttributes = xats;
        //            }
        //            properties.Add(info);
        //        }//foreach
        //        properties.Sort();
        //        _SerializeInfos[instanceType] = properties;
        //    }
        //    foreach (XmlPropertyInfo property in properties)
        //    {
        //        object v = property.Property.GetValue(instance, null);
        //        if (property.DefaultValue != XmlPropertyInfo.NoneDefaultValue)
        //        {
        //            if (v == property.DefaultValue || object.Equals(v, property.DefaultValue))
        //            {
        //                // 数值等于默认值
        //                continue;
        //            }
        //        }
        //        string strValue = null;
        //        if (v == null || DBNull.Value.Equals(v))
        //        {
        //            // 数据为空
        //            strValue = string.Empty;
        //        }
        //        else
        //        {
        //            if (v.GetType().IsPrimitive || v is string || v is decimal)
        //            {
        //                strValue = v.ToString();
        //            }
        //            else if (v is DateTime)
        //            {
        //                strValue = DateTimeCommon.FastToYYYY_MM_DD_HH_MM_SS( (DateTime)v);
        //            }
        //            else if (v is Color)
        //            {
        //                strValue = XMLSerializeHelper.ColorToString((Color)v);
        //            }
        //            else if (v.GetType().IsClass)
        //            {
        //                // 输出XML子元素
        //                if (property.ArrayItemAttributes != null && property.ArrayItemAttributes.Length > 0)
        //                {
        //                    // 输出列表
        //                    System.Collections.IEnumerable list = (System.Collections.IEnumerable)v;
        //                    if (v is System.Collections.IList)
        //                    {
        //                        System.Collections.IList ls2 = (System.Collections.IList)v;
        //                        if (ls2.Count == 0)
        //                        {
        //                            // 列表为空
        //                            continue;
        //                        }
        //                    }
        //                    writer.WriteStartElement(property.Name);
        //                    foreach (object subObj in list)
        //                    {
        //                        bool match = false;
        //                        foreach (XmlArrayItemAttribute attr in property.ArrayItemAttributes)
        //                        {
        //                            if (attr.Type.IsInstanceOfType(subObj))
        //                            {
        //                                string eName = attr.ElementName;
        //                                if (string.IsNullOrEmpty(eName))
        //                                {
        //                                    eName = GetNativeXmlName(subObj.GetType());
        //                                }
        //                                writer.WriteStartElement(eName);
        //                                // 输出子对象
        //                                MySerilizeXml(subObj, writer, level + 1);
        //                                writer.WriteEndElement();
        //                                match = true;
        //                                break;
        //                            }
        //                        }//foreach
        //                        if (match == false)
        //                        {
        //                            // 没有匹配
        //                            string eName = GetNativeXmlName(subObj.GetType());
        //                            writer.WriteStartElement(eName);
        //                            MySerilizeXml(subObj, writer, level + 1);
        //                            writer.WriteEndElement();
        //                        }
        //                    }
        //                    writer.WriteEndElement();
        //                }
        //                else
        //                {
        //                    // 输出子对象
        //                    writer.WriteStartElement(property.Name);
        //                    MySerilizeXml(v, writer, level + 1);
        //                    writer.WriteEndElement();
        //                }
        //                continue;
        //            }
        //            else
        //            {
        //                strValue = v.ToString();
        //            }
        //        }
        //        if (strValue == null)
        //        {
        //            strValue = string.Empty;
        //        }
        //        if (property.ValueType == 0)
        //        {
        //            // 输出XML属性
        //            writer.WriteAttributeString(property.Name, strValue);
        //        }
        //        else if (property.ValueType == 1)
        //        {
        //            // 输出XML元素
        //            writer.WriteElementString(property.Name, strValue);
        //        }
        //        else if (property.ValueType == 2)
        //        {
        //            //输出XML文本
        //            writer.WriteString(strValue);
        //            // 结束输出XML节点。
        //            break;
        //        }
        //    }//foreach
        //}
#endif
//        /// <summary>
//        /// 将XML节点转换为XML字符串
//        /// </summary>
//        /// <param name="node">XML节点</param>
//        /// <param name="indent">是否缩进</param>
//        /// <returns>生成的字符串</returns>
//        public static string XmlNodeToString( System.Xml.XmlNode node , bool indent )
//        {
//            var str = new System.IO.StringWriter();
//            var w = new System.Xml.XmlTextWriter(str);
//            if(indent )
//            {
//                w.Formatting = Formatting.Indented;
//                w.Indentation = 3;
//                w.IndentChar = ' ';
//            }
//            else
//            {
//                w.Formatting = Formatting.None;
//            }
//            node.WriteTo(w);
//            w.Close();
//            var xml = str.ToString();
//            return xml;
//        }
//#if !DCWriterForWASM
//        /// <summary>
//        /// 获得所有子节点组成的数组
//        /// </summary>
//        /// <param name="rootNode">根节点</param>
//        /// <returns>子节点数组</returns>
//        public static System.Xml.XmlNode[] GetChildNodesArrary( System.Xml.XmlNode rootNode )
//        {
//            if( rootNode == null || rootNode.ChildNodes.Count == 0 )
//            {
//                return null;
//            }
//            var result = new System.Xml.XmlNode[rootNode.ChildNodes.Count];
//            int index = 0;
//            foreach( System.Xml.XmlNode node in rootNode.ChildNodes )
//            {
//                result[index++] = node;
//            }
//            return result;
//        }
//#endif
    }//public sealed class XMLHelper
}