using System;
using System.Collections.Generic;
using System.Text;

// 袁永福到此一游

namespace DCSoft.TemperatureChart
{
    /// <summary>
    /// 编辑数据点事件参数
    /// </summary>
    /// <param name="eventSender">事件发起者</param>
    /// <param name="args">事件参数</param>
    [System.Runtime.InteropServices.ComVisible( true )]
    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
    [System.Runtime.InteropServices. Guid("3EBDE4DC-C655-42AB-845C-3980914DA0AD")]
    public delegate void EditValuePointEventHandler( 
        object eventSender , 
        EditValuePointEventArgs args );

    /// <summary>
    /// 编辑数据点事件参数
    /// </summary>
     
    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = false )]
    public partial class EditValuePointEventArgs
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        /// <param name="ctl">时间轴控件对象</param>
        /// <param name="document">文档对象</param>
        /// <param name="vp">数据点对象</param>
        /// <param name="mode">事件模式</param>
        public EditValuePointEventArgs(
            TemperatureControl ctl , 
            TemperatureDocument document , 
            ValuePoint vp , 
            EditValuePointMode mode )
        {
            _Control = ctl;
            _Document = document;
            _ValuePoint = vp;
            _EditMode = mode ;
        }

        private TemperatureControl _Control = null;
        /// <summary>
        /// 时间轴控件对象
        /// </summary>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public TemperatureControl Control
        {
            get
            {
                return _Control; 
            }
        }
        private TemperatureDocument _Document = null;
        /// <summary>
        /// 时间轴文档对象
        /// </summary>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public TemperatureDocument Document
        {
            get
            {
                return _Document; 
            }
        }

        private EditValuePointMode _EditMode = EditValuePointMode.Insert;
        /// <summary>
        /// 处理模式
        /// </summary>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public EditValuePointMode EditMode
        {
            get
            {
                return _EditMode; 
            }
        }

        private ValuePoint _ValuePoint = null;
        /// <summary>
        /// 数据点对象
        /// </summary>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public ValuePoint ValuePoint
        {
            get
            {
                return _ValuePoint; 
            }
        }

        /// <summary>
        /// 数据序列的标题
        /// </summary>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string SerialTitle
        {
            get
            {
                if (_ValuePoint != null)
                {
                    if (_ValuePoint.Parent is YAxisInfo)
                    {
                        return ((YAxisInfo)_ValuePoint.Parent).Title;
                    }
                    else if (_ValuePoint.Parent is TitleLineInfo)
                    {
                        return ((TitleLineInfo)_ValuePoint.Parent).Title;
                    }
                }
                return null;
            }
        }

        /// <summary>
        /// 数据序列的名称
        /// </summary>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string SerialName
        {
            get
            {
                if (this.ValuePoint != null)
                {
                    if (this.ValuePoint.Parent is YAxisInfo)
                    {
                        return ((YAxisInfo)this.ValuePoint.Parent).Name;
                    }
                    else if (this.ValuePoint.Parent is TitleLineInfo)
                    {
                        return ((TitleLineInfo)this.ValuePoint.Parent).Name;
                    }
                }
                return null;
            }
        }
        /// <summary>
        /// Y轴坐标信息
        /// </summary>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public YAxisInfo YAxisInfo
        {
            get
            {
                return _ValuePoint.Parent as YAxisInfo;
            }
        }

        /// <summary>
        /// 标题行信息对象
        /// </summary>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public TitleLineInfo TitleLineInfo
        {
            get
            {
                return _ValuePoint.Parent as TitleLineInfo;
            }
        }

        private bool _Result = true ;
        /// <summary>
        /// 操作是否成功
        /// </summary>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public bool Result
        {
            get
            {
                return _Result; 
            }
            set
            {
                _Result = value; 
            }
        }
    }

    /// <summary>
    /// 编辑数据点模式
    /// </summary>
    [System.Runtime.InteropServices.ComVisible( true )]
     
    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
     [ System.Runtime.InteropServices. Guid("7668A908-E63B-413B-850F-24DA3DAC9E4E")]
    public enum EditValuePointMode
    {
        /// <summary>
        /// 新增数据点
        /// </summary>
        Insert,
        /// <summary>
        /// 删除数据点
        /// </summary>
        Delete,
        /// <summary>
        /// 修改数据点数值
        /// </summary>
        Update
    }
}
