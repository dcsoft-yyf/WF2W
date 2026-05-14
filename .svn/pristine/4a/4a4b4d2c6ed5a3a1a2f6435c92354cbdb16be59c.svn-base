using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing ;
using System.Xml.Serialization;
using DCSoft.Common;
using DCSoft.Drawing;

// 袁永福到此一游

namespace DCSoft.TemperatureChart
{
    /// <summary>
    /// 时刻信息对象
    /// </summary>

#if !DCWriterForWASM
    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = false  )]
    [System.Runtime.InteropServices.ComVisible(false)]
    [Serializable]
#endif
    public partial class TickInfo
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public TickInfo()
        {
        }

        internal TickInfo(int v, string txt, Color c)
        {
            this._Value = v;
            this._Text = txt;
            this._Color = c;
        }

        internal TickInfo(float v, string txt, Color c)
        {
            this._Value = v;
            this._Text = txt;
            this._Color = c;
        }

        private float _Value = 0f;
        /// <summary>
        /// 数值
        /// </summary>
        [DefaultValue( 0f )]
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

        private string _Text = null;
        /// <summary>
        /// 文本
        /// </summary>
        [DefaultValue( null )]
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

        private Color _Color = Color.Black;
        /// <summary>
        /// 文本颜色
        /// </summary>
        [DefaultColorValue("Black")]
        [XmlIgnore]
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
        /// 文本颜色值
        /// </summary>
        [DefaultValue(null)]
        [Browsable( false )]
        [XmlAttribute]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string ColorValue
        {
            get
            {
                return XMLSerializeHelper.ColorToString(this.Color, Color.Black);
            }
            set
            {
                this.Color = XMLSerializeHelper.StringToColor(value, Color.Black);
            }
        }

        /// <summary>
        /// 复制对象
        /// </summary>
        /// <returns>复制品</returns>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public TickInfo Clone()
        {
            return (TickInfo)this.MemberwiseClone();
        }

        /// <summary>
        /// 返回表示对象数据的字符串
        /// </summary>
        /// <returns>字符串</returns>
        public override string ToString()
        {
            return this._Value + " " + this._Text + " " + ColorTranslator.ToHtml(this.Color);
        }
    }

    /// <summary>
    /// 刻度信息列表
    /// </summary>
#if !DCWriterForWASM
     
#if WINFORM || DCWriterForWinFormNET6
    [Editor( typeof( TickInfoListUITypeEditor ) , typeof( System.Drawing.Design.UITypeEditor ))]
#endif
    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = false  )]
    [System.Runtime.InteropServices.ComVisible(false)]
    [Serializable]
#endif
    public partial class TickInfoList : List<TickInfo>
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public TickInfoList()
        {
        }
#if !DCWriterForWASM

        /// <summary>
        /// 填充时刻成员
        /// </summary>
        /// <param name="tickStepSeconds">时刻长度，单位秒。</param>
        /// <param name="totalSeconds">总的秒数</param>
        /// <param name="specifyFormatString">指定的时刻格式化字符串</param>
        internal void FillTickItems(
            int tickStepSeconds , 
            float totalSeconds , 
            string specifyFormatString )
        {
            if (tickStepSeconds <= 0)
            {
                return;
            }
            for (int iCount = 0; iCount < totalSeconds; iCount += tickStepSeconds)
            {
                TickInfo item = new TickInfo();
                DateTime dtm = new DateTime(1900, 1, 1);
                dtm = dtm.AddSeconds(iCount);
                string txt = dtm.Hour.ToString();
                if (string.IsNullOrEmpty(specifyFormatString))
                {
                    if (tickStepSeconds < 60)
                    {
                        txt = dtm.ToString("HH:mm:ss");
                    }
                    else if (tickStepSeconds < 3600)
                    {
                        txt = dtm.ToString("HH:mm");
                    }
                }
                else
                {
                    txt = dtm.ToString(specifyFormatString);
                }
                item.Text = txt;
                item.Value = iCount / 3600.0f;
                this.Add(item);
            }//for
        }
#endif
        /// <summary>
        /// 添加项目
        /// </summary>
        /// <param name="v">时刻</param>
        /// <param name="txt">文本</param>
        /// <param name="c">颜色</param>
        public void AddItem(int v, string txt, Color c)
        {
            this.Add( new TickInfo( v , txt , c ));
        }

        public void AddItem(float v, string txt, Color c)
        {
            this.Add(new TickInfo(v, txt, c));
        }
#if !DCWriterForWASM

        /// <summary>
        /// 获得小时刻度序号
        /// </summary>
        /// <param name="dtm">时间</param>
        /// <returns>序号</returns>
        internal int GetStartHourTickIndex(DateTime dtm)
        {
            if (this.Count == 0)
            {
                return 0;
            }
            // 第一个刻度是0
            bool firstZero = this[0].Value == 0;
            float hour = dtm.Hour + dtm.Minute / 60f + dtm.Second / 3600f;
            for (int hourCount = 0; hourCount < this.Count; hourCount++)
            {
                if (hour == this[hourCount].Value)
                {
                    //return hourCount;
                }
                if( hour < this[ hourCount] .Value )
                {
                    if (firstZero)
                    {
                        return Math.Max( hourCount - 1, 0 );
                    }
                    else
                    {
                        return hourCount ;
                    }
                }
            }//for
            return this.Count - 1;
        }
        /// <summary>
        /// 返回表示数据的字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            foreach (TickInfo tick in this)
            {
                if (str.Length > 0)
                {
                    str.Append(",");
                }
                str.Append(tick.Value.ToString());
            }
            return str.ToString();
        }
#endif
        /// <summary>
        /// 复制对象
        /// </summary>
        /// <returns>复制品</returns>
        public TickInfoList Clone()
        {
            TickInfoList list = new TickInfoList();
            foreach (TickInfo item in this)
            {
                list.Add(item.Clone());
            }
            return list;
        }
    }
}
