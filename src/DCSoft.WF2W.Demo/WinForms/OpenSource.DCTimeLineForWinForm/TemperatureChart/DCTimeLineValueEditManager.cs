using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;

namespace DCSoft.TemperatureChart
{
    /// <summary>
    /// 时间轴数据编辑器管理器
    /// </summary>
    [System.Runtime.InteropServices.ComVisible( false )]
    public class DCTimeLineValueEditManager
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        /// <param name="ctl">控件对象</param>
        public DCTimeLineValueEditManager(TemperatureControl ctl)
        {
            if (ctl == null)
            {
                throw new ArgumentNullException("ctl");
            }
            _Control = ctl;
        }
        private TemperatureControl _Control = null;

        /// <summary>
        /// 编辑标题标签
        /// </summary>
        /// <param name="document">文档对象</param>
        /// <returns>操作是否修改了文档对象</returns>
        public bool EditHeaderLabels(TemperatureDocument document )
        {
            if (document == null)
            {
                throw new ArgumentNullException("lbl");
            }
            using (dlgTitleValues dlg = new dlgTitleValues())
            {
                foreach (HeaderLabelInfo info in this._Control.Document.HeaderLabels)
                {
                    dlg.InputTitles.Add(info.Title);
                    dlg.InputValues.Add(info.Value);
                }
                if (dlg.ShowDialog(this._Control) == DialogResult.OK)
                {
                    for (int iCount = 0; iCount < dlg.InputValues.Count; iCount++)
                    {
                        this._Control.Document.HeaderLabels[iCount].Value = dlg.InputValues[iCount];
                    }
                    this._Control.Document.Modified = true;
                    this._Control.Invalidate(true);
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 编辑数据点中的数据
        /// </summary>
        /// <param name="vp">数据点对象</param>
        /// <returns>是否修改数据</returns>
        public bool EditValuePoint(ValuePoint vp)
        {
            if (vp == null)
            {
                throw new ArgumentNullException("vp");
            }
            if (vp.Parent is TitleLineInfo)
            {
                TitleLineInfo line = ( TitleLineInfo ) vp.Parent ;
                using (dlgEditSingleValue dlg = new dlgEditSingleValue())
                {
                    dlg.InputTime = vp.Time;
                    dlg.InputTitle = string.Format(DCTimeLineStrings.InputTitle_Name, line.Title);
                    dlg.InputValue = vp.Text;
                    if (dlg.ShowDialog(this._Control) == System.Windows.Forms.DialogResult.OK)
                    {
                        vp.Text = dlg.InputValue;
                        this._Control.Document.InvalidateState();
                        this._Control.DocumentViewControl.Invalidate();
                        return true;
                    }
                }
            }
            else if (vp.Parent is YAxisInfo)
            {
                YAxisInfo info = (YAxisInfo)vp.Parent;
                if (info.Style == YAxisInfoStyle.Text || info.Style == YAxisInfoStyle.TextInsideGrid)
                {
                    using (dlgEditSingleValue dlg = new dlgEditSingleValue())
                    {
                        dlg.InputTime = vp.Time;
                        dlg.InputTitle = string.Format(
                            DCTimeLineStrings.InputTitle_Name, info.Title);
                        dlg.InputValue = vp.Text;
                        dlg.Text = dlg.Text + " " + vp.InstanceIndex;
                        if (dlg.ShowDialog(this._Control) == DialogResult.OK)
                        {
                            vp.Text = dlg.InputValue;
                            this._Control.Document.Modified = true;
                            this._Control.DocumentViewControl.Invalidate();
                            return true;
                        }
                    }
                }
                else if (vp.ShadowPoint == null || vp.ShowShadowPoint == false)
                {
                    if (info.EnableLanternValue)
                    {
                        // 带挂灯笼的数据
                        using (dlgEditTowValues dlg = new dlgEditTowValues())
                        {
                            dlg.InputTime = vp.Time;
                            dlg.InputTitle1 = string.Format(
                                DCTimeLineStrings.InputTitle_Name_Min_Max, 
                                info.Title, 
                                info.MinValue, 
                                info.MaxValue);
                            dlg.Text = dlg.Text + " " + vp.InstanceIndex;
                            if (TemperatureDocument.IsNullValue(vp.Value) == false)
                            {
                                dlg.InputValue1 = vp.Value.ToString();
                            }
                            dlg.InputParent1 = info;
                            if (string.IsNullOrEmpty(info.LanternValueTitle))
                            {
                                dlg.InputTitle2 = DCTimeLineStrings.DefaultLanternValueTitle;
                            }
                            else
                            {
                                dlg.InputTitle2 = info.LanternValueTitle;
                            }
                            if (TemperatureDocument.IsNullValue(vp.LanternValue) == false)
                            {
                                dlg.InputValue2 = vp.LanternValue.ToString();
                            }
                            dlg.InputParent2 = info;
                            dlg.EventOKButtonClick += new CancelEventHandler(dlg_EventOKButtonClick);
                            if (dlg.ShowDialog(this._Control) == DialogResult.OK)
                            {
                                float v = 0;
                                if (float.TryParse(dlg.InputValue1, out v))
                                {
                                    vp.Value = v;
                                }
                                else
                                {
                                    vp.Value = TemperatureDocument.NullValue;
                                }
                                if (float.TryParse(dlg.InputValue2, out v))
                                {
                                    vp.LanternValue = v;
                                }
                                else
                                {
                                    vp.LanternValue = TemperatureDocument.NullValue;
                                }
                                this._Control.Document.Modified = true;
                                this._Control.Document.InvalidateState();
                                this._Control.DocumentViewControl.Invalidate();
                                return true;
                            }
                        }
                    }
                    else
                    {
                        // 普通单个数据
                        using (dlgEditSingleValue dlg = new dlgEditSingleValue())
                        {
                            dlg.InputTime = vp.Time;
                            dlg.InputTitle = string.Format(
                                DCTimeLineStrings.InputTitle_Name_Min_Max, 
                                info.Title, 
                                info.MinValue, 
                                info.MaxValue);
                            if (TemperatureDocument.IsNullValue(vp.Value) == false)
                            {
                                dlg.InputValue = vp.Value.ToString();
                            }
                            dlg.InputParent = info;
                            dlg.EventOKButtonClick +=new CancelEventHandler(dlg_EventOKButtonClick);
                            dlg.Text = dlg.Text + " " + vp.InstanceIndex;
                            if (dlg.ShowDialog(this._Control) == DialogResult.OK)
                            {
                                float v = 0;
                                if (float.TryParse(dlg.InputValue, out v))
                                {
                                    vp.Value = v;
                                }
                                else
                                {
                                    vp.Value = TemperatureDocument.NullValue;
                                }
                                this._Control.Document.Modified = true;
                                this._Control.Document.InvalidateState();
                                this._Control.DocumentViewControl.Invalidate();
                                return true;
                            }
                        }
                    }
                }
                else
                {
                    // 显示了阴影数据
                    YAxisInfo info2 = (YAxisInfo)vp.ShadowPoint.Parent;
                    using (dlgEditTowValues dlg = new dlgEditTowValues())
                    {
                        dlg.InputTime = vp.Time;
                        dlg.InputTitle1 = string.Format(
                            DCTimeLineStrings.InputTitle_Name_Min_Max, 
                            info.Title, 
                            info.MinValue, 
                            info.MaxValue);
                        if (TemperatureDocument.IsNullValue(vp.Value) == false)
                        {
                            dlg.InputValue1 = vp.Value.ToString();
                        }
                        dlg.InputParent1 = info;
                        dlg.InputTitle2 = string.Format(
                            DCTimeLineStrings.InputTitle_Name_Min_Max, 
                            info2.Title, 
                            info2.MinValue, 
                            info2.MaxValue);
                        if (TemperatureDocument.IsNullValue(vp.ShadowPoint.Value) == false)
                        {
                            dlg.InputValue2 = vp.ShadowPoint.Value.ToString();
                        }
                        dlg.InputParent2 = info2;
                        dlg.EventOKButtonClick += new CancelEventHandler(dlg_EventOKButtonClick);
                        dlg.Text = dlg.Text + " " + vp.InstanceIndex;
                        if (dlg.ShowDialog(this._Control) == DialogResult.OK)
                        {
                            float v = 0;
                            if (float.TryParse(dlg.InputValue1, out v))
                            {
                                vp.Value = v;
                            }
                            else
                            {
                                vp.Value = TemperatureDocument.NullValue;
                            }
                            if (float.TryParse(dlg.InputValue2, out v))
                            {
                                vp.ShadowPoint.Value = v;
                            }
                            else
                            {
                                vp.Value = TemperatureDocument.NullValue;
                            }
                            this._Control.Document.Modified = true;
                            this._Control.Document.InvalidateState();
                            this._Control.DocumentViewControl.Invalidate();
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        void dlg_EventOKButtonClick(object sender, CancelEventArgs e)
        {
            if (sender is dlgEditSingleValue)
            {
                dlgEditSingleValue dlg = (dlgEditSingleValue)sender;
                float v1 = 0;
                if (float.TryParse(dlg.InputValue, out v1) == false)
                {
                    v1 = TemperatureDocument.NullValue;
                }
                else
                {
                    if (dlg.InputParent is YAxisInfo)
                    {
                        YAxisInfo info = (YAxisInfo)dlg.InputParent;
                        if (info.CheckValueRange(v1) == false)
                        {
                            e.Cancel = true;
                            MessageBox.Show(
                                dlg,
                                string.Format(
                                    DCTimeLineStrings.InputValueOutofRange_Title__MinValue_MaxValue,
                                    info.Title,
                                    info.MinValue,
                                    info.MaxValue),
                                DCTimeLineStrings.SystemAlert,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                            return;
                        }
                    }
                }
            }
            else if (sender is dlgEditTowValues)
            {
                dlgEditTowValues dlg = (dlgEditTowValues)sender;
                float v1 = 0;
                if (float.TryParse(dlg.InputValue1, out v1) == false)
                {
                    v1 = TemperatureDocument.NullValue;
                }
                else
                {
                    if (dlg.InputParent1 is YAxisInfo)
                    {
                        YAxisInfo info = (YAxisInfo)dlg.InputParent1;
                        if (info.CheckValueRange(v1) == false)
                        {
                            e.Cancel = true;
                            MessageBox.Show(
                                dlg,
                                string.Format(
                                    DCTimeLineStrings.InputValueOutofRange_Title__MinValue_MaxValue,
                                    info.Title,
                                    info.MinValue,
                                    info.MaxValue),
                                DCTimeLineStrings.SystemAlert,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                            return;
                        }
                    }
                }
                float v2 = 0;
                if (float.TryParse(dlg.InputTitle2, out v2) == false)
                {
                    v2 = TemperatureDocument.NullValue;
                }
                else
                {
                    if (dlg.InputParent2 is YAxisInfo)
                    {
                        YAxisInfo info = (YAxisInfo)dlg.InputParent2;
                        if (info.CheckValueRange(v2) == false)
                        {
                            e.Cancel = true;
                            MessageBox.Show(
                                dlg,
                                string.Format(
                                    DCTimeLineStrings.InputValueOutofRange_Title__MinValue_MaxValue,
                                    info.Title,
                                    info.MinValue,
                                    info.MaxValue),
                                DCTimeLineStrings.SystemAlert,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                            return;
                        }
                    }
                }
            }
        }
    }
}
