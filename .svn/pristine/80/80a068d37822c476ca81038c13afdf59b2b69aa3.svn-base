using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Runtime.InteropServices ;
using System.Drawing;

namespace DCSoft.TemperatureChart
{
    // 时间轴文档文件操作功能模块
    partial class TemperatureDocument
    {

        
#if !DCWriterForWASM

        /// <summary>
        /// 保存文档到文件流中
        /// </summary>
        /// <param name="stream">文件流</param>
        [ComVisible( false )]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void Save(System.IO.Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }
            TemperatureDocumentWriter writer2 = new TemperatureDocumentWriter(stream);
            writer2.Write_TemperatureDocument(this);
            writer2.Flush();
            //XmlSerializer ser = new XmlSerializer(this.GetType());
            //ser.Serialize(stream, this);
        }

        /// <summary>
        /// 保存文档到文本书写器中
        /// </summary>
        /// <param name="writer">文本书写器</param>
        [ComVisible( false )]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void Save(TextWriter writer)
        {
            if (writer == null)
            {
                throw new ArgumentNullException("writer");
            }
            TemperatureDocumentWriter writer2 = new TemperatureDocumentWriter(writer);
            writer2.Write_TemperatureDocument(this);
            writer2.Flush();
            //XmlSerializer ser = new XmlSerializer(this.GetType());
            //ser.Serialize(writer, this);
        }

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="fileName">文件名</param>
        [ComVisible( true )]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void SaveToFile(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentNullException("fileName");
            }
            //XmlSerializer ser = new XmlSerializer(this.GetType());
            using (FileStream stream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                TemperatureDocumentWriter writer = new TemperatureDocumentWriter(stream);
                writer.Write_TemperatureDocument(this);
                writer.Flush();
                stream.Close();
                //ser.Serialize(stream, this);
            }
        }

        /// <summary>
        /// 保存文档到字符串中
        /// </summary>
        /// <returns>生成的字符串</returns>
        [ComVisible( true )]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string SaveToString()
        {
            StringWriter str = new StringWriter();
            TemperatureDocumentWriter writer = new TemperatureDocumentWriter(str);
            writer.Write_TemperatureDocument(this);
            writer.Flush();
            //XmlSerializer ser = new XmlSerializer(this.GetType());
            //ser.Serialize(str, this);
            string xml = str.ToString();
            writer.Close();
            return xml;
        }


        

        /// <summary>
        /// 从文件中加载文档对象
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns>操作是否成功</returns>
        [ComVisible( true )]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public bool LoadFromFile(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentNullException("fileName");
            }
            using (FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                return Load(stream);
            }
        }


        /// <summary>
        /// 从文件流中加载文档数据
        /// </summary>
        /// <param name="stream">文件流</param>
        /// <returns>操作是否成功</returns>
        [ComVisible( false )]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public bool Load(System.IO.Stream stream)
        {
            try
            {
                if (stream == null)
                {
                    throw new ArgumentNullException("stream");
                }
                TemperatureDocument doc = null;
                TemperatureDocumentReader reader = new TemperatureDocumentReader(stream);
                doc = (TemperatureDocument)reader.Read_TemperatureDocument();
                reader.Close();
                //using (StreamReader sr = new StreamReader(stream))
                //{
                //    System.Xml.XmlReader xr = System.Xml.XmlReader.Create(sr);
                //    TemperatureDocumentSerializer ser = new TemperatureDocumentSerializer();
                //    doc = (TemperatureDocument)ser.Deserialize(xr);
                //    //XmlSerializer ser = new XmlSerializer(this.GetType());
                //    //doc = (TemperatureDocument)ser.Deserialize(xr);
                //    sr.Close();
                //}
                if (doc != null)
                {
                    doc.InnerCopyTo(this);
                    return true;
                }
            }
            catch
            {
#if WINFORM || DCWriterForWinFormNET6
                if (System.Environment.UserInteractive)
                {
                    System.Windows.Forms.MessageBox.Show("XML文档有错误，请检查！");
                }
#endif
                return false;
            }

            return false;
        }

        /// <summary>
        /// 从字符串中加载数据
        /// </summary>
        /// <param name="xml">XML字符串</param>
        /// <returns>操作是否成功</returns>
        [ComVisible( true )]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public bool LoadFromString(string xml)
        {
            if (string.IsNullOrEmpty(xml))
            {
                throw new ArgumentNullException("xml");
            }
            using (StringReader reader = new StringReader(xml))
            {
                return Load(reader);
            }
        }

        /// <summary>
        /// 从文件读取器中加载文档数据
        /// </summary>
        /// <param name="reader">文本读取器</param>
        /// <returns>操作是否成功</returns>
        [ComVisible( false )]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public bool Load(TextReader reader)
        {
            if (reader == null)
            {
                throw new ArgumentNullException("reader");
            }
            TemperatureDocument doc = null;
            TemperatureDocumentReader reader2 = new TemperatureDocumentReader(reader);
            doc = (TemperatureDocument)reader2.Read_TemperatureDocument();
            reader.Close();
            //XmlSerializer ser = new XmlSerializer(this.GetType());
            //TemperatureDocument doc = (TemperatureDocument)ser.Deserialize(xr);
            if (doc != null)
            {
                doc.InnerCopyTo(this);
                return true;
            }
            return false;
        }

        /// <summary>
        /// 保存数据HTML
        /// </summary>
        /// <param name="stream">文件流</param>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void SaveDataHtml(System.IO.Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }
            System.IO.StreamWriter writer = new System.IO.StreamWriter(stream, Encoding.Default);
            writer.WriteLine("<html><body>");
            writer.WriteLine(GetDataTableHtml());
            writer.WriteLine("</body><//html>");
        }

#endif


        private void InnerCopyTo(TemperatureDocument document)
        {
            document._Config = this._Config;
            document._Datas = this._Datas;
            document._NumOfPages = this._NumOfPages;
            document._Left = this._Left;
            document._Top = this._Top;
            document._Width = this._Width;
            document._Height = this._Height;
            document._BackColor = this._BackColor;
            document._PageIndex = this._PageIndex;
            document._ViewMode = this._ViewMode;
            document._Parameters = this._Parameters;
            document.LayoutInvalidate = true;
        }

    }
}
