using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Design;

namespace DCSoft.TemperatureChart
{
    /// <summary>
    /// 时刻列表编辑器
    /// </summary>
    [System.Runtime.InteropServices.ComVisible( false )]
    public class TickInfoListUITypeEditor : UITypeEditor
    {
        /// <summary>
        /// 采用对话框模式
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }
        /// <summary>
        /// 编辑数值
        /// </summary>
        /// <param name="context"></param>
        /// <param name="provider"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            using (dlgTickList dlg = new dlgTickList())
            {
                if (value is TickInfoList)
                {
                    dlg.InputTicks = ((TickInfoList)value).Clone();
                }
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    return dlg.InputTicks;
                }
            }
            return value;
        }
    }
}
