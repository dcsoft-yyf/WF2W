using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Xml.Serialization;
using DCSoft.Common;

namespace DCSoft.TemperatureChart
{
    /// <summary>
    /// y轴网格线配置信息
    /// </summary>

#if !DCWriterForWASM
    [Serializable]
    [TypeConverter(typeof(DCSoft.Common.TypeConverterSupportProperties))]
    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = false )]
#endif
    public  partial class GridYSplitInfo
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public GridYSplitInfo()
        {
        }

        private int _GridYSplitNum = 8;

        /// <summary>
        /// 网格垂直拆分数
        /// </summary>
        [DefaultValue(8)]
        [DCDisplayName(typeof(GridYSplitInfo), "GridYSplitNum")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public int GridYSplitNum
        {
            get
            {
                return _GridYSplitNum;
            }
            set
            {
                _GridYSplitNum = value;
            }
        }

        private int _GridYSpaceNum = 5;

        /// <summary>
        /// 每次绘制的线条数（包含一条粗线和多条细线）
        /// </summary>
        [DefaultValue(5)]
        [DCDisplayName(typeof(GridYSplitInfo), "GridYSpaceNum")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public int GridYSpaceNum
        {
            get
            {
                return _GridYSpaceNum;
            }
            set
            {
                _GridYSpaceNum = value;
            }
        }

        private int _GridYSpaceNumForBottomPadding = -1;
        /// <summary>
        /// 数据网格底部边距范围单独配置绘制的线条数（包含一条粗线和多条细线）
        /// </summary>
        [DefaultValue(-1)]
        [DCDisplayName(typeof(GridYSplitInfo), "GridYSpaceNumForBottomPadding")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public int GridYSpaceNumForBottomPadding
        {
            get
            {
                return _GridYSpaceNumForBottomPadding;
            }
            set
            {
                _GridYSpaceNumForBottomPadding = value;
            }
        }

        private float _ThickLineWidth = 2f;

        /// <summary>
        /// 粗线宽度
        /// </summary>
        [DefaultValue(2f)]
        [DCDisplayName(typeof(GridYSplitInfo), "ThickLineWidth")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public float ThickLineWidth
        {
            get
            {
                return _ThickLineWidth;
            }
            set
            {
                _ThickLineWidth = value;
            }
        }

        private float _ThinLineWidth = 1f;

        /// <summary>
        /// 细线宽度
        /// </summary>
        [DefaultValue(1f)]
        [DCDisplayName(typeof(GridYSplitInfo), "ThinLineWidth")]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public float ThinLineWidth
        {
            get
            {
                return _ThinLineWidth;
            }
            set
            {
                _ThinLineWidth = value;
            }
        }
    }
}
