using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Xml.Serialization;
using System.Drawing;
using DCSoft.Common;

namespace DCSoft.TemperatureChart
{
    /// <summary>
    /// 数据标尺刻度信息
    /// </summary>

#if !DCWriterForWASM
    [DCSoft.Common.DCPublishAPI]
    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = false )]
    [Serializable]
#endif
    public partial class YAxisScaleInfo
    {
        private string _Text = null;
        /// <summary>
        /// 刻度文本
        /// </summary>
        [DefaultValue(null)]
        [XmlAttribute]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string Text
        {
            get
            {
                return _Text;
            }
            set
            {
                _Text = value;
            }
        }

        private float _Value = 0f;
        /// <summary>
        /// 刻度数值
        /// </summary>
        [DefaultValue(0)]
        [XmlAttribute]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public float Value
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


        private StringAlignment _TickTextAlignment =  StringAlignment.Center;
        /// <summary>
        /// 标尺刻度文本对齐
        /// </summary>
        [DefaultValue(StringAlignment.Center)]
        [XmlAttribute]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public StringAlignment TickTextAlignment
        {
            get
            {
                return _TickTextAlignment;
            }
            set
            {
                _TickTextAlignment = value;
            }
        }

        private float _ScaleRate = 0f;
        /// <summary>
        /// 展示的刻度比例,从0.0到1.0之间
        /// </summary>
        [DefaultValue(0f)]
        [XmlAttribute]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public float ScaleRate
        {
            get
            {
                return _ScaleRate;
            }
            set
            {
                _ScaleRate = value;
            }
        }

        private Color _Color = Color.Transparent;
        /// <summary>
        /// 数据点符号颜色
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public Color Color
        {
            get
            {
                return _Color;
            }
            set
            {
                _Color = value;
            }
        }



        /// <summary>
        /// 文本形式的颜色值
        /// </summary>
        [Browsable(false)]
        [DefaultValue(null)]
        [System.Xml.Serialization.XmlAttribute]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string ColorValue
        {
            get
            {
                return XMLSerializeHelper.ColorToString(this.Color, Color.Transparent);
            }
            set
            {
                this.Color = XMLSerializeHelper.StringToColor(value, Color.Transparent);
            }
        }
#if !DCWriterForWASM

        /// <summary>
        /// 复制对象
        /// </summary>
        /// <returns>复制品</returns>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public YAxisScaleInfo Clone()
        {
            return (YAxisScaleInfo)this.MemberwiseClone();
        }

        /// <summary>
        /// 返回表示对象数据的字符串
        /// </summary>
        /// <returns>字符串</returns>
        public override string ToString()
        {
            return this.Value + "#" + this.ScaleRate;
        }
#endif
    }

    /// <summary>
    /// 数据标尺刻度信息列表
    /// </summary>

#if !DCWriterForWASM
    [Serializable]
    [System.Diagnostics.DebuggerDisplay("Count={ Count }")]
    [System.Diagnostics.DebuggerTypeProxy(typeof(DCSoft.Common.ListDebugView))]
    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = false )]
#endif
    public partial class YAxisScaleInfoList : List<YAxisScaleInfo>
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public YAxisScaleInfoList()
        {
        }
#if !DCWriterForWASM

        internal YAxisScaleInfo GetScaleInfoByValue(float Value)
        {
            if ( TemperatureDocument.IsNullValue( Value ) || this.Count == 0 )
            {
                return null;
            }
            if (this.Count == 1)
            {
                return this[0];
            }
            for (int iCount = this.Count - 1; iCount >= 0; iCount--)
            {
                if (this[iCount].Value <= Value)
                {
                    return this[iCount];
                }
            }
            return this[0];
        }

        /// <summary>
        /// 添加项目
        /// </summary>
        /// <param name="Value"></param>
        /// <param name="scaleRate"></param>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void AddItem(float Value, float scaleRate)
        {
            YAxisScaleInfo info = new YAxisScaleInfo();
            info.Value = Value;
            info.ScaleRate = scaleRate;
            this.Add(info);
        }

        /// <summary>
        /// 根据数值进行排序
        /// </summary>
        public void SortByValue()
        {
            this.Sort(new ItemComparaer());
        }

        private class ItemComparaer : IComparer<YAxisScaleInfo>
        {
            public int Compare(YAxisScaleInfo x, YAxisScaleInfo y)
            {
                if (x.Value == y.Value)
                {
                    return 0;
                }
                else if (x.Value > y.Value)
                {
                    return 1;
                }
                else
                {
                    return -1;
                }
            }
        }
#endif
    }
}
