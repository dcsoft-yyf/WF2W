using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.ComponentModel;
using System.Runtime.InteropServices;
using DCSoft.Common;

namespace DCSoft.TemperatureChart
{
    /// <summary>
    /// 文档参数
    /// </summary>

#if !DCWriterForWASM
    [ComVisible( false )]
    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = false  )]
    [Serializable]
#endif
    public partial class DCTimeLineParameter
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public DCTimeLineParameter()
        {
        }

        private string _Name = String.Empty;
        /// <summary>
        /// 参数名称
        /// </summary>
        [DefaultValue( null )]
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

        private string _Value = String.Empty;
        /// <summary>
        /// 参数值
        /// </summary>
        [DefaultValue( null )]
        [XmlText]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string Value
        {
            get
            {
                return _Value; 
            }
            set
            {
                _Value = value; 
            }
        }

        /// <summary>
        /// 复制对象
        /// </summary>
        /// <returns>复制品</returns>
        public DCTimeLineParameter Clone()
        {
            return (DCTimeLineParameter)this.MemberwiseClone();
        }
#if !DCWriterForWASM

        /// <summary>
        /// 返回数据点值
        /// </summary>
        public override string ToString()
        {
            return this.Name + "=" + this.Value;
        }
#endif
    }

    /// <summary>
    /// 文档参数列表
    /// </summary>
#if !DCWriterForWASM
    [ComVisible(false)]
    [Serializable]
    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = false  )]
#endif
    public class DCTimeLineParameterList : List<DCTimeLineParameter>
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public DCTimeLineParameterList()
        {
        }

        /// <summary>
        /// 复制对象
        /// </summary>
        /// <returns>复制品</returns>
        public DCTimeLineParameterList Clone()
        {
            DCTimeLineParameterList list = new DCTimeLineParameterList();
            foreach (DCTimeLineParameter p in this)
            {
                list.Add(p.Clone());
            }
            return list;
        }
#if !DCWriterForWASM
        /// <summary>
        /// 设置参数值
        /// </summary>
        /// <param name="name">参数名</param>
        /// <returns>参数值</returns>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string GetValue(string name)
        {
            DCTimeLineParameter p = GetParameter(name);
            if (p == null)
            {
                return null;
            }
            else
            {
                return p.Value;
            }
        }

        /// <summary>
        /// 设置参数值
        /// </summary>
        /// <param name="name">参数名</param>
        /// <param name="Value">参数值</param>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void SetValue(string name, string Value)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name");
            }
            DCTimeLineParameter p = GetParameter(name);
            if (p == null)
            {
                p = new DCTimeLineParameter();
                p.Name = name;
                this.Add(p);
            }
            p.Value = Value;
        }

        private DCTimeLineParameter GetParameter(string name)
        {
            if (name != null && name.Length > 0)// string.IsNullOrEmpty(name) == false)
            {
                foreach (DCTimeLineParameter p in this)
                {
                    if (string.Compare(p.Name, name, true) == 0)
                    {
                        return p;
                    }
                }
            }
            return null ;
        }

        /// <summary>
        /// 进行参数值转换
        /// </summary>
        /// <param name="parameterName">参数名</param>
        /// <param name="defaultValue">默认的文本</param>
        /// <returns>转换后的文本</returns>
        public string Convert(string parameterName, string defaultValue)
        {
            if (parameterName != null && parameterName.Length > 0 )// string.IsNullOrEmpty(parameterName) == false )
            {
                DCTimeLineParameter p = GetParameter(parameterName);
                if (p != null)
                {
                    return p.Value;
                }
            }
            return defaultValue;
        }

        /// <summary>
        /// 为COM接口开放的读取列表成员的方法
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        [ComVisible( true )]
        [System.Reflection.Obfuscation( Exclude =true , ApplyToMembers = true )]
        public DCTimeLineParameter ComGetItem(int index)
        {
            return this[index];
        }

        /// <summary>
        /// 为COM接口开放的设置列表成员的方法
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        [ComVisible(true)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void ComSetItem(int index, DCTimeLineParameter item)
        {
            this[index] = item;
        }
#endif
    }

}
