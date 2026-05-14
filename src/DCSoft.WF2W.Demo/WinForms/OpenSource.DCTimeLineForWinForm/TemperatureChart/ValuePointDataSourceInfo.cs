using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Xml.Serialization;
using DCSoft.Common;
//using System.Data ;
using DCSoft.Data;

namespace DCSoft.TemperatureChart
{
    /// <summary>
    /// 数据点数据源信息对象
    /// </summary>
#if !DCWriterForWASM
    [Serializable]
    [System.Xml.Serialization.XmlType]
     
    //[DCSoft.Common.DCDescriptionResourceSource(typeof(DCSoft.TemperatureChart.DCTimeLineDescriptionResource))]
    [TypeConverter( typeof( DCSoft.Common.TypeConverterSupportProperties))]
    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = false )]
#endif
    public partial class ValuePointDataSourceInfo
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public ValuePointDataSourceInfo()
        { 
        }

        private string _SQLText = null;
        /// <summary>
        /// 查询数据使用的SQL语句
        /// </summary>
        [DefaultValue(null)]
        [XmlElement]
        [DCDisplayName(typeof(ValuePointDataSourceInfo), "SQLText")]
#if WINFORM || DCWriterForWinFormNET6
        [Editor( typeof( dlgSQLText.SQLTextEditor ) , typeof( System.Drawing.Design.UITypeEditor ))]
#endif
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string SQLText
        {
            get
            {
                return _SQLText;
            }
            set
            {
                _SQLText = value;
            }
        }

        private string _FieldNameForID = null;
        /// <summary>
        /// 表示ID的字段名
        /// </summary>
        [DefaultValue( null )]
        [XmlAttribute]
        [DCDisplayName(typeof(ValuePointDataSourceInfo), "FieldNameForID")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string FieldNameForID
        {
            get
            {
                return _FieldNameForID; 
            }
            set
            {
                _FieldNameForID = value; 
            }
        }

        private string _FieldNameForLink = null;
        /// <summary>
        /// 表示Link的字段名
        /// </summary>
        [DefaultValue(null)]
        [XmlAttribute]
        [DCDisplayName(typeof(ValuePointDataSourceInfo), "FieldNameForLink")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string FieldNameForLink
        {
            get
            {
                return _FieldNameForLink;
            }
            set
            {
                _FieldNameForLink = value;
            }
        }

        private string _FieldNameForTitle = null;
        /// <summary>
        /// 表示Title的字段名
        /// </summary>
        [DefaultValue(null)]
        [XmlAttribute]
        [DCDisplayName(typeof(ValuePointDataSourceInfo), "FieldNameForTitle")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string FieldNameForTitle
        {
            get
            {
                return _FieldNameForTitle;
            }
            set
            {
                _FieldNameForTitle = value;
            }
        }

        private string _FieldNameForTime = null;
        /// <summary>
        /// 表示Time的字段名
        /// </summary>
        [DefaultValue(null)]
        [XmlAttribute]
        [DCDisplayName(typeof(ValuePointDataSourceInfo), "FieldNameForTime")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string FieldNameForTime
        {
            get
            {
                return _FieldNameForTime;
            }
            set
            {
                _FieldNameForTime = value;
            }
        }

        private string _FieldNameForValue = null;
        /// <summary>
        /// 表示Value的字段名
        /// </summary>
        [DefaultValue(null)]
        [XmlAttribute]
        [DCDisplayName(typeof(ValuePointDataSourceInfo), "FieldNameForValue")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string FieldNameForValue
        {
            get
            {
                return _FieldNameForValue;
            }
            set
            {
                _FieldNameForValue = value;
            }
        }

        private string _FieldNameForLanternValue = null;
        /// <summary>
        /// 表示LanternValue的字段名
        /// </summary>
        [DefaultValue(null)]
        [XmlAttribute]
        [DCDisplayName(typeof(ValuePointDataSourceInfo), "FieldNameForLanternValue")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string FieldNameForLanternValue
        {
            get
            {
                return _FieldNameForLanternValue;
            }
            set
            {
                _FieldNameForLanternValue = value;
            }
        }

        private string _FieldNameForText = null;
        /// <summary>
        /// 表示Text的字段名
        /// </summary>
        [DefaultValue(null)]
        [XmlAttribute]
        [DCDisplayName(typeof(ValuePointDataSourceInfo), "FieldNameForText")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string FieldNameForText
        {
            get
            {
                return _FieldNameForText;
            }
            set
            {
                _FieldNameForText = value;
            }
        }
#if !DCWriterForWASM
        /// <summary>
        /// 填充数据
        /// </summary>
        /// <param name="table">数据表对象</param>
        /// <param name="list">数据点列表</param>
        /// <returns>填充的数据个数</returns>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public int Fill(System.Data.DataTable table, ValuePointList list)
        {
            if (table == null)
            {
                throw new ArgumentNullException("table");
            }
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            System.Data.DataTableReader reader = new System.Data.DataTableReader(table);
            return Fill(reader, list);
        }
        /// <summary>
        /// 填充数据
        /// </summary>
        /// <param name="reader">数据读取器</param>
        /// <param name="list">数据点列表</param>
        /// <returns>填充的数据个数</returns>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public int Fill(System.Data.IDataReader reader , ValuePointList list)
        {
            if (reader == null)
            {
                throw new ArgumentNullException("reader");
            }
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            int result = 0;
            int fieldIndexOfID = GetFieldIndex( reader , this.FieldNameForID );
            int fieldIndexOfLink = GetFieldIndex( reader , this.FieldNameForLink );
            int fieldIndexOfTitle = GetFieldIndex( reader, this.FieldNameForTitle) ;
            int fieldIndexOfTime = GetFieldIndex( reader , this.FieldNameForTime );
            int fieldIndexOfValue = GetFieldIndex( reader , this.FieldNameForValue );
            int fieldIndexOfLanternValue = GetFieldIndex( reader , this.FieldNameForLanternValue );
            int fieldIndexOfText = GetFieldIndex( reader , this.FieldNameForText );
            while (reader.Read())
            {
                ValuePoint vp = new ValuePoint();
                if (fieldIndexOfID >= 0 && reader.IsDBNull( fieldIndexOfID ) == false )
                {
                    vp.ID = Convert.ToString(reader.GetValue(fieldIndexOfID));
                }
                if (fieldIndexOfLanternValue >= 0 && reader.IsDBNull(fieldIndexOfLanternValue) == false)
                {
                    vp.LanternValue = Convert.ToSingle(reader.GetValue(fieldIndexOfLanternValue));
                }
                if (fieldIndexOfLink >= 0 && reader.IsDBNull(fieldIndexOfLink) == false)
                {
                    vp.Link = Convert.ToString(reader.GetValue(fieldIndexOfLink));
                }
                if (fieldIndexOfText >= 0 && reader.IsDBNull(fieldIndexOfText) == false)
                {
                    vp.Text = Convert.ToString(reader.GetValue(fieldIndexOfText));
                }
                if (fieldIndexOfTime >= 0 && reader.IsDBNull(fieldIndexOfTime) == false)
                {
                    vp.Time = Convert.ToDateTime(reader.GetValue(fieldIndexOfTime));
                }
                if (fieldIndexOfTitle >= 0 && reader.IsDBNull(fieldIndexOfTitle) == false)
                {
                    vp.Title = Convert.ToString(reader.GetValue(fieldIndexOfTitle));
                }
                if (fieldIndexOfValue >= 0 && reader.IsDBNull(fieldIndexOfValue) == false)
                {
                    vp.Value = Convert.ToSingle(reader.GetValue(fieldIndexOfValue));
                }
                list.Add(vp);
                result++;
            }//while
            return result;
        }
        private int GetFieldIndex(System.Data.IDataReader reader, string fieldName)
        {
            if (string.IsNullOrEmpty(fieldName))
            {
                return -1;
            }
            return reader.GetOrdinal(fieldName);
        }
#endif

      

    }
}
