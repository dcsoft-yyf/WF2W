using System;
using System.Collections.Generic;
using System.Text;
//using System.Data;
using System.Collections;
using System.Xml;
using System.Reflection ;

namespace DCSoft.Data
{
    /// <summary>
    /// 数据源对象
    /// </summary>
    /// <remarks>袁永福到此一游</remarks>
    [System.Runtime.InteropServices.ComVisible( false )]
    public class DCDataSource
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public DCDataSource()
        {
        }

        /// <summary>
        /// 初始化对象
        /// </summary>
        /// <param name="dataSource">数据源对象</param>
        public DCDataSource(object dataSource)
        {
            this._DataSource = dataSource;
        }

        private object _DataSource = null;
        /// <summary>
        /// 数据源对象
        /// </summary>
        public object DataSource
        {
            get { return _DataSource; }
            set { _DataSource = value; }
        }

        private string _RootXPath = null;

        public string RootXPath
        {
            get { return _RootXPath; }
            set { _RootXPath = value; }
        }

        private DCDataSourceFieldList _Fields = new DCDataSourceFieldList();

        public DCDataSourceFieldList Fields
        {
            get { return _Fields; }
            set { _Fields = value; }
        }

       

        //private int _Position = 0;

        public void Start()
        {
            //_Position = 0;
            int index = 0;
            foreach (DCDataSourceField field in this.Fields)
            {
                field._Index = index++;
            }
            if (_DataSource == null)
            {
                foreach (DCDataSourceField field in this.Fields)
                {
                    field._Invalidate = true;
                }
                return;
            }
#if !DCWriterForWASM
            if (_DataSource is System.Data.IDataReader)
            {
                var reader = (System.Data.IDataReader)_DataSource;
                this._RootType = DataSourceFieldType.DataReader;
                foreach (DCDataSourceField field in this.Fields)
                {
                    field.FieldIndex = reader.GetOrdinal(field.FieldName);
                    if (field.FieldIndex >= 0)
                    {
                        field.FieldType = DataSourceFieldType.DataReader;
                        field._Invalidate = false;
                    }
                    else
                    {
                        field._Invalidate = true;
                    }
                }
            }
            else if (_DataSource is System.Data.DataTable)
            {
                var table = (System.Data.DataTable)_DataSource;
                this._RootType = DataSourceFieldType.DataTable;
                this._RootEnumerator = table.Rows.GetEnumerator();
                foreach (DCDataSourceField field in this.Fields)
                {
                    field.FieldIndex = table.Columns.IndexOf(field.FieldName);
                    if (field.FieldIndex >= 0)
                    {
                        field.FieldType = DataSourceFieldType.DataTable;
                        field._Invalidate = false;
                    }
                    else
                    {
                        field._Invalidate = true;
                    }
                }
                 
            }
            else if (_DataSource is System.Data.DataView)
            {
                var dv = (System.Data.DataView)_DataSource;
                this._RootType = DataSourceFieldType.DataTable ;
                this._RootEnumerator = dv.GetEnumerator() ;
                foreach (DCDataSourceField field in this.Fields)
                {
                    field.FieldIndex = dv.Table.Columns.IndexOf(field.FieldName);
                    if (field.FieldIndex >= 0)
                    {
                        field.FieldType = DataSourceFieldType.DataTable;
                        field._Invalidate = false;
                    }
                    else
                    {
                        field._Invalidate = true;
                    }
                }
            }
            else
#endif
            if (_DataSource is XmlNode)
            {
                XmlNode node = (XmlNode)_DataSource;
                if (string.IsNullOrEmpty(this.RootXPath) == false)
                {
                    XmlNodeList nodes = node.SelectNodes(this.RootXPath);
                    this._RootEnumerator = nodes.GetEnumerator();
                }
                else
                {
                    this._RootEnumerator = node.ChildNodes.GetEnumerator();
                }
                this._RootType = DataSourceFieldType.XPath;
                foreach (DCDataSourceField field in this.Fields)
                {
                    field._Invalidate = false;
                    field.FieldType = DataSourceFieldType.XPath;
                }
            }
            else if (_DataSource is XmlNodeList)
            {
                XmlNodeList nodes = (XmlNodeList)_DataSource;
                this._RootType = DataSourceFieldType.XPath;
                this._RootEnumerator = nodes.GetEnumerator();
                foreach (DCDataSourceField field in this.Fields)
                {
                    field._Invalidate = false;
                    field.FieldType = DataSourceFieldType.XPath;
                }
            }
            else if (_DataSource is IEnumerable)
            {
                IEnumerable enumer = (IEnumerable)_DataSource;
                this._RootEnumerator = enumer.GetEnumerator();
                this._RootType = DataSourceFieldType.Property;
                foreach (DCDataSourceField field in this.Fields)
                {
                    field._Invalidate = false;
                    field.FieldType = DataSourceFieldType.Property;
                }
            }
            foreach (DCDataSourceField field in this.Fields)
            {
                if (string.IsNullOrEmpty(field.FieldName))
                {
                    field._Invalidate = true;
                }
            }
        }

        private DataSourceFieldType _RootType = DataSourceFieldType.Property;

        private IEnumerator _RootEnumerator = null;

        /// <summary>
        /// 重置数据记录
        /// </summary>
        public void Reset()
        {
            Start();
#if !DCWriterForWASM
            if (this._DataSource is System.Data.IDataReader)
            {
                var reader = (System.Data.IDataReader)this._DataSource;
            }
#endif
            if (_RootEnumerator != null)
            {
                _RootEnumerator.Reset();
            }
        }

        /// <summary>
        /// 将数据移动到下一条
        /// </summary>
        /// <returns>操作是否成功</returns>
        public bool MoveNext()
        {
#if !DCWriterForWASM
            if ( this._DataSource is System.Data.IDataReader )
            {
                var reader = (System.Data.IDataReader)this._DataSource;
                return reader.Read();
            }
#endif
            if (_RootEnumerator == null)
            {
                return false;
            }
            return _RootEnumerator.MoveNext();
        }

        /// <summary>
        /// 获得当前数据对象
        /// </summary>
        public object Current
        {
            get
            {
#if !DCWriterForWASM
                if (this._DataSource is System.Data.IDataReader)
                {
                    var reader = (System.Data.IDataReader)this._DataSource;
                    return reader;
                }
#endif
                if (_RootEnumerator == null)
                {
                    return null;
                }
                else
                {
                    return _RootEnumerator.Current;
                }
            }
        }

        //public object ReadValue(int fieldIndex)
        //{
        //    if (fieldIndex < 0 || fieldIndex >= this.Fields.Count)
        //    {
        //        throw new ArgumentOutOfRangeException( "fieldIndex=" + fieldIndex );
        //    }
        //    if( this.Fields[ fieldIndex].Invalidate )
        //    {
        //        return null ;
        //    }
        //    string name = this.Fields[ fieldIndex ].FieldName ;
        //    return ReadValue(name);
        //}

        public object ReadValue(string fieldName)
        {
            DCSingleDataSource ds = DCSingleDataSource.Package(this.Current);
            return ds.ReadValue(fieldName);

           
        }
         

        internal enum DataSourceFieldType
        {
            DataTable,
            DataView,
            Property,
            XPath,
            DataReader
        }



    }


    /// <summary>
    /// 数据源字段对象
    /// </summary>
     
    [System.Runtime.InteropServices.ComVisible(false)]
    public class DCDataSourceField
    {
        public DCDataSourceField()
        {
        }


        internal bool _Invalidate = false;
        /// <summary>
        /// 状态无效，无法读取或设置数据
        /// </summary>
        public bool Invalidate
        {
            get { return _Invalidate; }
        }

        internal DCDataSource.DataSourceFieldType FieldType = DCDataSource.DataSourceFieldType.DataTable;
        /// <summary>
        /// 数据源字段序号
        /// </summary>
        internal int FieldIndex = 0;

        private string _FieldName = null;
        /// <summary>
        /// 绑定的字段名
        /// </summary>
        public string FieldName
        {
            get { return _FieldName; }
            set { _FieldName = value; }
        }

        private string _BindingPath = null;

        public string BindingPath
        {
            get { return _BindingPath; }
            set { _BindingPath = value; }
        }

        internal int _Index = 0;

        //public int Index
        //{
        //    get { return _Index; }
        //    //set { _Index = value; }
        //}
    }


    /// <summary>
    /// 数据源字段对象列表
    /// </summary>
     
    [System.Runtime.InteropServices.ComVisible(false)]
    public class DCDataSourceFieldList : List<DCDataSourceField>
    {
        public DCDataSourceFieldList()
        {
        }

        public DCDataSourceField this[string fieldName]
        {
            get
            {
                foreach (DCDataSourceField field in this)
                {
                    if (string.Compare(field.FieldName, fieldName, true) == 0)
                    {
                        return field;
                    }
                }
                return null;
            }
        }
        public DCDataSourceField AddField(string fieldName)
        {
            DCDataSourceField field = new DCDataSourceField();
            field.FieldName = fieldName;
            this.Add(field);
            return field;
        }
#if !DCWriterForWASM

        public DCDataSourceField AddField(string fieldName, string bindingPath)
        {
            DCDataSourceField field = new DCDataSourceField();
            field.FieldName = fieldName;
            field.BindingPath = bindingPath;
            this.Add(field);
            return field;
        }
#endif
    }
}
