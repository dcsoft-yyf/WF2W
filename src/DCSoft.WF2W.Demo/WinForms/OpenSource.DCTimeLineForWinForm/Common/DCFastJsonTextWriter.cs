using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace DCSoft
{
    /// <summary>
    /// 快速的JSON字符串书写器
    /// </summary>
    [System.Runtime.InteropServices.ComVisible(false)]
    public class DCFastJsonTextWriter : DCSoft.Common.IDCPropertyWriter
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public DCFastJsonTextWriter()
        {
            this._Writer = new StringBuilder();
        }
        
        public void Close()
        {
            this._Writer = null;
            this._ChildCounts = null;
            //this._DateFormatString = null;
            this._States = null;
        }
        private System.Text.StringBuilder _Writer = null;

        public override string ToString()
        {
            return this._Writer.ToString();
        }

        public bool IndentFormat = false;
        private enum MyState
        {
            Root,
            Array,
            Object,
            Property
        }

        private int _IndentLevel = 0;
        private void WriteIndent()
        {
            if (this.IndentFormat)
            {
                if (this._IndentLevel > 0)
                {
                    this._Writer.Append(new string(' ', this._IndentLevel * 4));
                }
            }
        }
        private const int MaxStateCount = 50;
        private MyState[] _States = new MyState[MaxStateCount];
        private int[] _ChildCounts = new int[MaxStateCount];
        private int _CurrentIndex = 0;

        private void EndState(MyState state)
        {
            bool delIndent = true;
            if (state == MyState.Array || state == MyState.Object)
            {
                if (_CurrentIndex > 0)
                {
                    if (_States[_CurrentIndex - 1] == MyState.Property)
                    {
                        delIndent = false;
                    }
                }
            }
            if (state == MyState.Object)
            {
                delIndent = true;
                //delIndent = false;
            }
            if (delIndent)
            {
                this._IndentLevel--;
            }
            while (_CurrentIndex >= 0)
            {
                var last = _States[_CurrentIndex];
                _CurrentIndex--;
                if (last == MyState.Array)
                {
                    if (this.IndentFormat)
                    {
                        this._Writer.AppendLine();
                        this.WriteIndent();
                    }
                    this._Writer.Append(']');
                }
                else if (last == MyState.Object)
                {
                    if (this.IndentFormat)
                    {
                        this._Writer.AppendLine();
                        this.WriteIndent();
                    }
                    this._Writer.Append('}');
                }
                else if (last == MyState.Property)
                {

                }
                if (last == state)
                {
                    break;
                }
            }
        }
        private void InnerWriteIndent(bool enableAddNewLine = true)
        {
            if (this.IndentFormat)
            {
                if (this._Writer.Length > 0)
                {
                    this._Writer.AppendLine();
                }
                if (this._IndentLevel > 0)
                {
                    this._Writer.Append(' ', this._IndentLevel * 4);
                }
            }
        }
        private void StartState(MyState state)
        {
            if (_CurrentIndex >= MaxStateCount)
            {
                throw new IndexOutOfRangeException("JSON数据套嵌过深，超过" + MaxStateCount);
            }
            bool isFirstChild = _ChildCounts[_CurrentIndex] == 0;
            if (isFirstChild == false)
            {
                _Writer.Append(',');
            }

            var lastState = _States[_CurrentIndex];
            bool addIndent = true;
            if (state == MyState.Array)
            {
                if (_CurrentIndex > 0 && _States[_CurrentIndex] == MyState.Property)
                {
                    addIndent = false;
                }
            }
            else if (state == MyState.Object)
            {
                //addIndent = false;
            }
            //if (state == MyState.Property && lastState == MyState.Object)
            //{
            //    WriteIndent();
            //}
            //else
            if (isFirstChild == false || state != MyState.Property)
            {
                InnerWriteIndent();
            }
            if (addIndent)
            {
                this._IndentLevel++;
            }

            _ChildCounts[_CurrentIndex]++;
            _CurrentIndex++;
            _States[_CurrentIndex] = state;
            _ChildCounts[_CurrentIndex] = 0;
        }

        /// <summary>
        /// 开始输出数组
        /// </summary>
        public void WriteStartArray()
        {
            StartState(MyState.Array);
            this._Writer.Append('[');
        }
        /// <summary>
        /// 结束输出数组
        /// </summary>
        public void WriteEndArray()
        {
            EndState(MyState.Array);
        }
        /// <summary>
        /// 开始输出对象
        /// </summary>
        public void WriteStartObject( string objName = null )
        {

            StartState(MyState.Object);
            this._Writer.Append('{');
        }

        /// <summary>
        /// 结束输出对象
        /// </summary>
        public void WriteEndObject()
        {
            EndState(MyState.Object);
        }

        /// <summary>
        /// 快速模式输出属性值
        /// </summary>
        /// <param name="name">属性名</param>
        /// <param name="Value">属性值</param>
        public void WritePropertyUseAttribute(string name, string Value)
        {
            WriteProperty(name, Value);
        }
        /// <summary>
        /// 输出属性值
        /// </summary>
        /// <param name="name">属性名</param>
        /// <param name="Value">属性值</param>
        public void WriteProperty(string name, string Value)
        {
            if (_ChildCounts[_CurrentIndex] > 0)
            {
                this._Writer.Append(',');
            }
            InnerWriteIndent();
            _ChildCounts[_CurrentIndex]++;
            InnerWriteStringValue(name, this._Writer);
            this._Writer.Append(':');
            InnerWriteStringValue(Value, this._Writer);
        }
        public void WriteStartProperty(string propertyName)
        {
            //if (_ChildCounts[_CurrentIndex] > 0)
            //{
            //    this._Writer.Append(',');
            //}
            int index = _CurrentIndex;
            //_ChildCounts[_CurrentIndex]++;

            StartState(MyState.Property);
            InnerWriteStringValue(propertyName, this._Writer);
            this._Writer.Append(':');
            _ChildCounts[index]++;
        }

        //public void WriteArrayValue( string strValue , bool isStringValue )
        //{
        //    if (this._States[this._CurrentIndex] == MyState.Array)
        //    {
        //        if (this._ChildCounts[_CurrentIndex] > 0)
        //        {
        //            this._Writer.Append(',');
        //        }
        //        this.InnerWriteIndent();
        //        this._ChildCounts[this._CurrentIndex]++;
        //        if (isStringValue)
        //        {
        //            InnerWriteStringValue(strValue, this._Writer);// WriteString(Value);
        //        }
        //        else
        //        {
        //            this._Writer.Append(strValue);
        //        }
        //    }
        //    else
        //    {
        //        throw new Exception("当前不是数组状态");
        //    }
        //}

        public void WriteEndProperty()
        {
            EndState(MyState.Property);
        }

        /// <summary>
        /// 输出属性值,对属性名不做任何修正。这要确保name参数值必须符合变量名的规范。
        /// </summary>
        /// <param name="name">属性名</param>
        /// <param name="Value">属性值</param>
        public void WritePropertyNoFixName(string name, string Value)
        {
            InnerWritePropertyNoFixName(name, Value, RawValueType.False);
        }
        /// <summary>
        /// 输出属性值,对属性名不做任何修正。这要确保name参数值必须符合变量名的规范。
        /// </summary>
        /// <param name="name">属性名</param>
        /// <param name="enumType">枚举类型</param>
        /// <param name="Value">属性值</param>
        public void WritePropertyNoFixNameEnum(string name, Type enumType, System.Enum Value)
        {
            InnerWritePropertyNoFixName(
                name,
                DCSoft.Common.DCEnumTypeInfo.FastToString(enumType, Value),
                RawValueType.AddQuote);// Value.ToString());
        }
        /// <summary>
        /// 输出属性值,对属性名不做任何修正。这要确保name参数值必须符合变量名的规范。
        /// </summary>
        /// <param name="name">属性名</param>
        /// <param name="Value">属性值</param>
        public void WritePropertyNoFixNameBoolean(string name, bool Value)
        {
            if (Value)
            {
                InnerWritePropertyNoFixName(name, "true", RawValueType.True);
            }
            else
            {
                InnerWritePropertyNoFixName(name, "false", RawValueType.True);
            }
        }

        ///// <summary>
        ///// 输出属性值,对属性名不做任何修正。这要确保name参数值必须符合变量名的规范。
        ///// </summary>
        ///// <param name="name">属性名</param>
        ///// <param name="Value">属性值</param>
        //public void WritePropertyNoFixName(string name, string Value, RawValueType valueType )
        //{
        //    InnerWritePropertyNoFixName(name, Value, valueType);
        //}
        [System.Runtime.InteropServices.ComVisible(false)]
        public enum RawValueType
        {
            False,
            True,
            AddQuote
        }
        /// <summary>
        /// 输出属性值,对属性名不做任何修正。这要确保name参数值必须符合变量名的规范。
        /// </summary>
        /// <param name="name">属性名</param>
        /// <param name="Value">属性值</param>
        private void InnerWritePropertyNoFixName(string name, string Value, RawValueType rawValue)
        {
            if (name == null || name.Length == 0)
            {
                throw new ArgumentNullException("name");
            }
            if (_ChildCounts[_CurrentIndex] > 0)
            {
                this._Writer.Append(',');
            }
            if (this.IndentFormat && this._Writer[this._Writer.Length - 1] != '{')
            {
                InnerWriteIndent();
            }
            
            _ChildCounts[_CurrentIndex]++;
            this._Writer.Append('"');
            this._Writer.Append(name);
            this._Writer.Append('"');
            this._Writer.Append(':');
            if (rawValue == RawValueType.True)
            {
                this._Writer.Append(Value);
            }
            else if (rawValue == RawValueType.False)
            {
                InnerWriteStringValue(Value, this._Writer);// WriteString(Value);
            }
            else
            {
                this._Writer.Append('"');
                this._Writer.Append(Value);
                this._Writer.Append('"');
            }
        }

        //public void WriteArrayValue( System.Collections.IList vs )
        //{
        //    WriteArray(vs, this._Writer);
        //}
        //public static void WriteArray(System.Collections.IList vs , StringBuilder writer )
        //{
        //    if( vs == null )
        //    {
        //        writer.Append("null");
        //    }
        //    else if(vs.Count == 0 )
        //    {
        //        writer.Append("[]");
        //    }
        //    else
        //    {
        //        writer.Append('[');
        //        int len = vs.Count;
        //        for(int iCount = 0;iCount < len; iCount ++)
        //        {
        //            if(iCount > 0 )
        //            {
        //                writer.Append(',');
        //            }
        //            var v = vs[iCount];
        //            if(v == null )
        //            {
        //                writer.Append("null");
        //            }
        //            else if(v is string )
        //            {
        //                InnerWriteStringValue((string)v, writer);
        //            }
        //            else
        //            {
        //                writer.Append(v.ToString());
        //            }
        //        }
        //        writer.Append(']');
        //    }
        //}

        public static unsafe void InnerWriteStringValue(string v , StringBuilder writer )
        {
            if (v == null)
            {
                writer.Append("null");
                return;
            }
            else
            {
             
                bool isNormal = true;
                writer.Append('"');
                int len = v.Length;
                if (len > 0)
                {
                    fixed (char* ptr = v)
                    {
                        char* ptEnd = ptr + len;
                        char* pt2 = ptr;
                        while (pt2 < ptEnd)
                        {
                            char c = *pt2;
                            if (c <= 92)
                            {
                                if (c == '"'
                                    || c == '\\'
                                    || c == '\r'
                                    || c == '\n'
                                    || c == '\t')
                                {
                                    isNormal = false;
                                    if (pt2 > ptr)
                                    {
                                        writer.Append(v, 0, (int)(pt2 - ptr));
                                        //this._Writer.Append(v.Substring(0, (int)(pt2 - ptr)));
                                    }
                                    while (pt2 < ptEnd)
                                    {
                                        c = *pt2;
                                        switch (c)
                                        {
                                            case '"':
                                                writer.Append("\\\"");
                                                break;
                                            case '\\':
                                                writer.Append("\\\\");
                                                break;
                                            case '\r':
                                                writer.Append("\\r");
                                                break;
                                            case '\n':
                                                writer.Append("\\n");
                                                break;
                                            case '\t':
                                                writer.Append("\\t");
                                                break;
                                            default:
                                                writer.Append(c);
                                                break;

                                        }
                                        pt2++;
                                    }
                                    break;
                                }
                            }
                            pt2++;
                        }
                    }
                    if (isNormal)
                    {
                        writer.Append(v);
                    }

                }
                writer.Append('"');
            }
        }
    }
}