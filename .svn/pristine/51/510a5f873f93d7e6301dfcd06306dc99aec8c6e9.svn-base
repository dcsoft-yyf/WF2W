using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Drawing;
using System.ComponentModel;
using System.Xml.Serialization;
using DCSoft.Common;
using DCSoft.Drawing;

namespace DCSoft.TemperatureChart
{
    /// <summary>
    /// Y轴异常数值区域样式配置信息
    /// </summary>
    [Serializable]
    [TypeConverter(typeof(DCSoft.Common.TypeConverterSupportProperties))]
#if !DCWriterForWASM
    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = false)]
#endif
    public partial class AbNormalRangeSettings
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public AbNormalRangeSettings()
        {

        }

        private Color _NormalRangeBackColor = Color.Transparent;
        /// <summary>
        /// 正常数值区域背景色
        /// </summary>
        [DefaultColorValue("Transparent")]
        [XmlIgnore]
        [DCDisplayName(typeof(AbNormalRangeSettings), "NormalRangeBackColor")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public Color NormalRangeBackColor
        {
            get
            {
                return _NormalRangeBackColor;
            }
            set
            {
                _NormalRangeBackColor = value;
            }
        }
        /// <summary>
        /// 文本形式的NormalRangeBackColor属性值
        /// </summary>
        [Browsable(false)]
        [DefaultValue(null)]
        [System.Xml.Serialization.XmlAttribute]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string NormalRangeBackColorValue
        {
            get
            {
                return XMLSerializeHelper.ColorToString(this.NormalRangeBackColor, Color.Transparent);
            }
            set
            {
                this.NormalRangeBackColor = XMLSerializeHelper.StringToColor(value, Color.Transparent);
            }
        }

        private Color _OutofNormalRangeBackColor = Color.Transparent;
        /// <summary>
        /// 超出正常区域背景色
        /// </summary>
        [DefaultColorValue("Transparent")]
        [XmlIgnore]
        [DCDisplayName(typeof(AbNormalRangeSettings), "OutofNormalRangeBackColor")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public Color OutofNormalRangeBackColor
        {
            get
            {
                return _OutofNormalRangeBackColor;
            }
            set
            {
                _OutofNormalRangeBackColor = value;
            }
        }
        /// <summary>
        /// 文本形式的OutofNormalRangeBackColor属性值
        /// </summary>
        [Browsable(false)]
        [DefaultValue(null)]
        [System.Xml.Serialization.XmlAttribute]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string OutofNormalRangeBackColorValue
        {
            get
            {
                return XMLSerializeHelper.ColorToString(this.OutofNormalRangeBackColor, Color.Transparent);
            }
            set
            {
                this.OutofNormalRangeBackColor = XMLSerializeHelper.StringToColor(value, Color.Transparent);
            }
        }

        private float _NormalMaxValue = TemperatureDocument.InnerNullValue;
        /// <summary>
        /// 数值正常范围的最大值
        /// </summary>
        [DefaultValue(TemperatureDocument.InnerNullValue)]
        [System.Xml.Serialization.XmlAttribute]
        [DCDisplayName(typeof(AbNormalRangeSettings), "NormalMaxValue")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public float NormalMaxValue
        {
            get
            {
                return _NormalMaxValue;
            }
            set
            {
                _NormalMaxValue = value;
            }
        }

        private XPenStyle _NormalRangeUpLineStyle = new XPenStyle(Color.Transparent, 0f);
        /// <summary>
        /// 正常范围上边缘线型
        /// </summary>
        //[DefaultValue(TemperatureDocument.InnerNullValue)]
        //[System.Xml.Serialization.XmlAttribute]
        [Browsable(true)]
        [DCDisplayName(typeof(AbNormalRangeSettings), "NormalRangeUpLineStyle")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public XPenStyle NormalRangeUpLineStyle
        {
            get
            {
                return _NormalRangeUpLineStyle;
            }
            set
            {
                _NormalRangeUpLineStyle = value;
            }
        }


        private float _NormalMinValue = TemperatureDocument.NullValue;
        /// <summary>
        /// 数值正常范围的最小值
        /// </summary>
        [DefaultValue(TemperatureDocument.InnerNullValue)]
        [System.Xml.Serialization.XmlAttribute]
        [DCDisplayName(typeof(AbNormalRangeSettings), "NormalMinValue")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public float NormalMinValue
        {
            get
            {
                return _NormalMinValue;
            }
            set
            {
                _NormalMinValue = value;
            }
        }

        private XPenStyle _NormalRangeDownLineStyle = new XPenStyle(Color.Transparent, 0f);
        /// <summary>
        /// 正常范围下边缘线型
        /// </summary>
        //[DefaultValue(TemperatureDocument.InnerNullValue)]
        //[System.Xml.Serialization.XmlAttribute]
        [Browsable(true)]
        [DCDisplayName(typeof(AbNormalRangeSettings), "NormalRangeDownLineStyle")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public XPenStyle NormalRangeDownLineStyle
        {
            get
            {
                return _NormalRangeDownLineStyle;
            }
            set
            {
                _NormalRangeDownLineStyle = value;
            }
        }


    }
}
