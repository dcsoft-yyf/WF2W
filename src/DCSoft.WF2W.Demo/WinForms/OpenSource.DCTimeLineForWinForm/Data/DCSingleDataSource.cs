using System;
using System.Collections.Generic;
using System.Text;
//using System.Data;
using System.Collections;
using System.Xml;
using System.ComponentModel;
using System.Reflection;

namespace DCSoft.Data
{
    /// <summary>
    /// 单行数据源对象
    /// </summary>
    /// <remarks>袁永福到此一游</remarks>
    [System.Runtime.InteropServices.ComVisible( false )]
    public class DCSingleDataSource
    {
        /// <summary>
        /// 包装对象
        /// </summary>
        /// <param name="instance">数据对象实例</param>
        /// <returns>获得的数据源对象</returns>
        public static DCSingleDataSource Package(object instance)
        {
            if (instance == null)
            {
                return null;
            }
            if (instance is DCSingleDataSource)
            {
                return (DCSingleDataSource)instance;
            }
            return new DCSingleDataSource(instance);
        }

        /// <summary>
        /// 数据源对象
        /// </summary>
        public DCSingleDataSource()
        {
        }

        /// <summary>
        /// 初始化对象
        /// </summary>
        /// <param name="dataBoundItem">数据源对象</param>
        public DCSingleDataSource(object dataBoundItem)
        {
            _DataBoundItem = dataBoundItem;
        }

        private object _DataBoundItem = null;
        /// <summary>
        /// 数据源对象
        /// </summary>
        public object DataBoundItem
        {
            get { return _DataBoundItem; }
            set { _DataBoundItem = value; }
        }

        /// <summary>
        /// 读取数值
        /// </summary>
        /// <param name="fieldName">字段名</param>
        /// <returns>获得的数值</returns>
        public object ReadValue(string fieldName)
        {
            return ReadValue(_DataBoundItem, fieldName);
        }
//#if !DCWriterForWASM

//        ///// <summary>
//        ///// 写入数值
//        ///// </summary>
//        ///// <param name="fieldName">字段名</param>
//        ///// <param name="fieldValue">新数值对象</param>
//        ///// <returns>操作是否成功</returns>
//        //public bool WriteValue(string fieldName, object fieldValue)
//        //{
//        //    return WriteValue(_DataBoundItem, fieldName, fieldValue);
//        //}
//        ///// <summary>
//        ///// 获得所有可用的字段名
//        ///// </summary>
//        ///// <returns>字段名组成的数组</returns>
//        //public string[] GetFieldNames()
//        //{
//        //    return GetFieldNames(this._DataBoundItem);
//        //}
//        /// <summary>
//        /// 获得所有的字段名
//        /// </summary>
//        /// <returns></returns>
//        public static string[] GetFieldNames( object dataBoundItem)
//        {
//            if (dataBoundItem == null)
//            {
//                return null;
//            }
//            List<string> result = new List<string>();
//#if !DCWriterForWASM
//            if (dataBoundItem is System.Data.IDataRecord)
//            {
//                var record = (System.Data.IDataRecord ) dataBoundItem ;
//                for (int iCount = 0; iCount < record.FieldCount; iCount++)
//                {
//                    result.Add( record.GetName( iCount ));
//                }
//            }
//            else if (dataBoundItem is System.Data.DataRow)
//            {
//                // 数据行
//                var row = (System.Data.DataRow)dataBoundItem;
//                var table = row.Table;
//                foreach (System.Data.DataColumn col in table.Columns)
//                {
//                    result.Add(col.ColumnName);
//                }
//            }
//            else if (dataBoundItem is System.Data.DataRowView)
//            {
//                // 数据行视图
//                var row = (System.Data.DataRowView)dataBoundItem;
//                foreach (System.Data.DataColumn col in row.DataView.Table.Columns)
//                {
//                    result.Add(col.ColumnName);
//                }
//            }
//            else if (dataBoundItem is System.Data.DataTable)
//            {
//                // 数据表
//                var table = (System.Data.DataTable)dataBoundItem;
//                foreach (System.Data.DataColumn col in table.Columns)
//                {
//                    result.Add(col.ColumnName);
//                }
//            }
//            else if (dataBoundItem is System.Data.DataView)
//            {
//                // 数据视图
//                var view = (System.Data.DataView)dataBoundItem;
//                foreach (System.Data.DataColumn col in view.Table.Columns)
//                {
//                    result.Add(col.ColumnName);
//                }
//            }
//            else 
//#endif
//            if (dataBoundItem is IDictionary)
//            {
//                // 字典
//                IDictionary dic = (IDictionary)dataBoundItem;
//                foreach (object key in dic.Keys)
//                {
//                    result.Add(Convert.ToString(key));
//                }
//            }
//            else if (dataBoundItem is XmlNode)
//            {
//                // XML节点
//                XmlNode node = (XmlNode)dataBoundItem;
//                foreach (XmlNode node2 in node.ChildNodes)
//                {
//                    if (node2 is XmlElement)
//                    {
//                        result.Add(node2.Name);
//                    }
//                }
//            }
//            else if (dataBoundItem is XmlNodeList)
//            {
//                // XML节点列表
//                XmlNodeList list = (XmlNodeList)dataBoundItem;
//                if (list.Count > 0)
//                {
//                    XmlNode node = list[0];
//                    foreach (XmlNode node2 in node.ChildNodes)
//                    {
//                        if (node2 is XmlElement)
//                        {
//                            result.Add(node2.Name);
//                        }
//                    }
//                }
//            }
//            else if (dataBoundItem.GetType().IsArray)
//            {
//                // 数组
//                Array array = (Array)dataBoundItem;
//                Type t = dataBoundItem.GetType().GetElementType();
//                PropertyInfo[] ps = t.GetProperties();
//                if (ps != null)
//                {
//                    foreach (PropertyInfo p in ps)
//                    {
//                        result.Add(p.Name);
//                    }
//                }
//            }
//            else
//            {
//                // 实体对象
//                PropertyInfo[] ps = dataBoundItem.GetType().GetProperties();
//                if (ps != null)
//                {
//                    foreach (PropertyInfo p in ps)
//                    {
//                        result.Add(p.Name);
//                    }
//                }
//            }
//            return result.ToArray();
//        }
//#endif
//        /// <summary>
//        /// 写数据
//        /// </summary>
//        /// <param name="dataBoundItem">数据对象</param>
//        /// <param name="fieldName">字段名</param>
//        /// <param name="fieldValue">数据</param>
//        /// <returns>操作是否成功</returns>
//        public static bool WriteValue(object dataBoundItem, string fieldName, object fieldValue)
//        {
//            if (dataBoundItem == null)
//            {
//                return false;
//            }
//#if !DCWriterForWASM
//            if (dataBoundItem is System.Data.IDataRecord)
//            {
//                return false;
//            }
//            if (dataBoundItem is System.Data.DataRow)
//            {
//                // 数据行
//                var row = (System.Data.DataRow)dataBoundItem;
//                row[fieldName ]  = fieldValue ;
//                return true ;
//            }
//            if (dataBoundItem is System.Data.DataRowView)
//            {
//                // 数据行视图
//                var row = (System.Data.DataRowView)dataBoundItem;
//                row[ fieldName ] = fieldValue ;
//            }
//            if (dataBoundItem is System.Data.DataTable)
//            {
//                // 数据表
//                var table = (System.Data.DataTable)dataBoundItem;
//                if (table.Rows.Count > 0)
//                {
//                    table.Rows[0][fieldName ] = fieldValue ;
//                    return true ;
//                }
//                else
//                {
//                    return false ;
//                }
//            }
//            if (dataBoundItem is System.Data.DataView)
//            {
//                // 数据视图
//                var view = (System.Data.DataView)dataBoundItem;
//                if (view.Count > 0)
//                {
//                    System.Data.DataRowView row = view[0];
//                    row[fieldName ]  = fieldValue ;
//                    return true ;
//                }
//                else
//                {
//                    return false ;
//                }
//            }
//#endif
//            if (dataBoundItem is IDictionary)
//            {
//                // 字典
//                IDictionary dic = (IDictionary)dataBoundItem;
//                dic[ fieldName ] = fieldValue ;
//                return true ;
//            }
//            if (dataBoundItem is XmlNode)
//            {
//                // XML节点
//                XmlNode node = (XmlNode)dataBoundItem;
//                XmlNode node2 = DCSoft.Common.XMLHelper.CreateXMLNodeByPath( node , fieldName , 1 , null );
//                if( node2 != null )
//                {
//                    if( fieldValue == null || DBNull.Value.Equals( fieldValue ))
//                    {
//                        node2.Value = string.Empty;
//                    }
//                    else
//                    {
//                        node2.Value = Convert.ToString( fieldValue );
//                    }
//                    return true;
//                }
//                else
//                {
//                    return false ;
//                }
//            }
//            if (dataBoundItem is XmlNodeList)
//            {
//                // XML节点列表
//                XmlNodeList list = (XmlNodeList)dataBoundItem;
//                if (list.Count > 0)
//                {
//                    XmlNode node = list[0];
//                    XmlNode node2 = DCSoft.Common.XMLHelper.CreateXMLNodeByPath( node , fieldName , 1 , null );
//                    if (node2 == null)
//                    {
//                        return false ;
//                    }
//                    else
//                    {
//                        if( fieldValue == null || DBNull.Value.Equals( fieldValue ))
//                        {
//                            node2.Value = string.Empty;
//                        }
//                        else
//                        {
//                            node2.Value = Convert.ToString( fieldValue );
//                        }
//                        return true ;
//                    }
//                }
//            }
//            if (dataBoundItem.GetType().IsArray)
//            {
//                // 数组
//                Array array = (Array)dataBoundItem;
//                if (array.GetLength(0) == 0)
//                {
//                    return false ;
//                }
//                else
//                {
//                    object obj = array.GetValue(0);
//                    return WriteValue( obj , fieldName , fieldValue );
//                }
//            }
//            // 对象
//            return DCSoft.Common.ValueTypeHelper.SetPropertyValueMultiLayer( dataBoundItem , fieldName , fieldValue , false );
//        }

        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="dataBoundItem">数据对象</param>
        /// <param name="fieldName">字段名</param>
        /// <returns>读取的数据</returns>
        public static object ReadValue( object dataBoundItem , string fieldName)
        {
            if (dataBoundItem == null)
            {
                return null;
            }
#if !DCWriterForWASM

            if (dataBoundItem is System.Data.IDataRecord)
            {
                var record = (System.Data.IDataRecord)dataBoundItem;
                return record[ fieldName ];
            }
            if (dataBoundItem is System.Data.DataRow)
            {
                // 数据行
                var row = (System.Data.DataRow)dataBoundItem;
                return row[fieldName];
            }
            if (dataBoundItem is System.Data.DataRowView)
            {
                // 数据行视图
                var row = (System.Data.DataRowView)dataBoundItem;
                return row[fieldName];
            }
            if (dataBoundItem is System.Data.DataTable)
            {
                // 数据表
                var table = (System.Data.DataTable)dataBoundItem;
                if (table.Rows.Count > 0)
                {
                    return table.Rows[0][fieldName];
                }
                else
                {
                    return null;
                }
            }
            if (dataBoundItem is System.Data.DataView)
            {
                // 数据视图
                var view = (System.Data.DataView)dataBoundItem;
                if (view.Count > 0)
                {
                    var row = view[0];
                    return row[fieldName];
                }
                else
                {
                    return null;
                }
            }
#endif
            if (dataBoundItem is IDictionary)
            {
                // 字典
                IDictionary dic = (IDictionary)dataBoundItem;
                if (dic.Contains(fieldName))
                {
                    return dic[fieldName];
                }
                else
                {
                    return null;
                }
            }
            if (dataBoundItem is XmlNode)
            {
                // XML节点
                XmlNode node = (XmlNode)dataBoundItem;
                XmlNode node2 = node.SelectSingleNode(fieldName);
                if (node2 == null)
                {
                    return null;
                }
                else
                {
                    return node2.Value;
                }
            }
            if (dataBoundItem is XmlNodeList)
            {
                // XML节点列表
                XmlNodeList list = (XmlNodeList)dataBoundItem;
                if (list.Count > 0)
                {
                    XmlNode node = list[0];
                    XmlNode node2 = node.SelectSingleNode(fieldName);
                    if (node2 == null)
                    {
                        return null;
                    }
                    else
                    {
                        return node2.Value;
                    }
                }
            }
            if (dataBoundItem.GetType().IsArray)
            {
                // 数组
                Array array = (Array)dataBoundItem;
                if (array.GetLength(0) == 0)
                {
                    return null;
                }
                else
                {
                    object obj = array.GetValue(0);
                    return ReadValue(obj, fieldName);
                }
            }
            // 对象
            return DCSoft.Common.ValueTypeHelper.GetPropertyValue(dataBoundItem, fieldName, false);
        }
    }
}
