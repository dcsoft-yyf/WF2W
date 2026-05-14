using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Xml.Serialization;

namespace DCSoft.TemperatureChart
{
    /// <summary>
    /// 文档数据
    /// </summary>

#if !DCWriterForWASM
    [Serializable]
    [System.Runtime.InteropServices.ComVisible(false)]
    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = false  )]
#endif
    public partial class DocumentData
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public DocumentData()
        {
        }

        private string _Name = null;
        /// <summary>
        /// 数据名称
        /// </summary>
        [System.ComponentModel.DefaultValue( null )]
        [XmlAttribute]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string Name
        {
            get
            {
                return _Name; 
            }
            set
            {
                _Name = value; 
            }
        }

        private ValuePointList _Values = null;
        /// <summary>
        /// 数值
        /// </summary>
        [XmlArrayItem("Value" , typeof( ValuePoint ))]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public ValuePointList Values
        {
            get
            {
                if (_Values == null)
                {
                    _Values = new ValuePointList();
                }
                return _Values; 
            }
            set
            {
                _Values = value; 
            }
        }
#if !DCWriterForWASM
        /// <summary>
        /// 返回数据点数量
        /// </summary>
        public override string ToString()
        {
            return this.Name + " " + this.Values.Count + "个数据点";
        }
        internal void WriteJson(DCSoft.DCFastJsonTextWriter writer)
        {
            writer.WritePropertyNoFixName("Name", this.Name);
            if (this.Values != null && this.Values.Count > 0)
            {
                writer.WriteStartProperty("Datas");
                writer.WriteStartArray();
                foreach (ValuePoint item in this.Values)
                {
                    writer.WriteStartObject();
                    item.WriteJson(writer);
                    writer.WriteEndObject();
                }
                writer.WriteEndArray();
                writer.WriteEndProperty();
            }
        }
#endif
    }

    /// <summary>
    /// 文档数据列表
    /// </summary>
#if !DCWriterForWASM
    [Serializable]
    [System.Runtime.InteropServices.ComVisible(false)]
    [System.Diagnostics.DebuggerDisplay("Count={ Count }")]
    [System.Diagnostics.DebuggerTypeProxy(typeof(DCSoft.Common.ListDebugView))]
    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true )]
#endif
    public class DocumentDataList : List<DocumentData>
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public DocumentDataList()
        {
        }
#if ! DCWriterForWASM
        /// <summary>
        /// 根据名称获得数据，没找到则自动创建
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>获得的数据</returns>
        public ValuePointList GetValuesByName(string name)
        {
            foreach (DocumentData item in this)
            {
                if (item.Name == name)
                {
                    return item.Values;
                }
            }

            DocumentData data = new DocumentData();
            data.Name = name;
            this.Add(data);
            return data.Values;
        }
#endif

        /// <summary>
        /// 复制对象
        /// </summary>
        /// <returns>复制品</returns>
        public DocumentDataList Clone()
        {
            DocumentDataList datas = new DocumentDataList();
            foreach (DocumentData item in this)
            {
                DocumentData newItem = new DocumentData();
                newItem.Name = item.Name;
                newItem.Values = item.Values.Clone();
                datas.Add(newItem);
            }
            return datas;
        }
#if !DCWriterForWASM
        /// <summary>
        /// 根据名称获得数据
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="autoCreate">若对象不存在则自动创建</param>
        /// <returns>获得的数据</returns>
        public DocumentData GetDataByName(string name , bool autoCreate)
        {
            foreach (DocumentData item in this)
            {
                if (item.Name == name)
                {
                    return item;
                }
            }
            if (autoCreate)
            {
                DocumentData data = new DocumentData();
                data.Name = name;
                this.Add(data);
                return data;
            }
            return null;
        }

        /// <summary>
        /// 为COM接口开放的读取列表成员的方法
        /// </summary>
        /// <param name="index">从0开始的序号</param>
        /// <returns>获得的列表成员对象</returns>
        [System.Runtime.InteropServices.ComVisible(true)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public DocumentData ComGetItem(int index)
        {
            return this[index];
        }

        /// <summary>
        /// 为COM接口开放的设置列表成员的方法
        /// </summary>
        /// <param name="index">从0开始的序号</param>
        /// <param name="item">新的列表成员对象</param>
        [System.Runtime.InteropServices.ComVisible(true)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void ComSetItem(int index, DocumentData item)
        {
            this[index] = item;
        }

#endif

    }
}
