using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Design;
using System.ComponentModel;
using System.ComponentModel.Design;

namespace DCSoft.TemperatureChart
{
    /// <summary>
    /// 标题名字编辑器
    /// </summary>
    //[System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = false )]
    [System.Runtime.InteropServices.ComVisible(false)]
    public class EditTitleUITypeEditor : UITypeEditor
{
        private System.Windows.Forms.Design.IWindowsFormsEditorService myService = null;
        /// <summary>
        /// 名称初始化
        /// </summary>
        public string strName2 = null;
        /// <summary>
        /// 初始化对象
        /// </summary>
        public EditTitleUITypeEditor()
        { 
        }
        /// <summary>
        /// 采用下拉列表模式
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
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
            using (System.Windows.Forms.ListBox lst = new System.Windows.Forms.ListBox())
            {
                if (TemperatureControl._StanderTitleList != null && TemperatureControl._StanderTitleList.Count > 0)
                {
                    for (int i = 0; i < TemperatureControl._StanderTitleList.Count; i++)
                    {
                        lst.Items.Add(TemperatureControl._StanderTitleList[i]);  
                    }
                       
                    myService = (System.Windows.Forms.Design.IWindowsFormsEditorService)
                        provider.GetService(typeof(System.Windows.Forms.Design.IWindowsFormsEditorService));
                    if (myService == null)
                        return value;
                    lst.SelectedIndexChanged += new EventHandler(lst_SelectedIndexChanged);
                    myService.DropDownControl(lst);
                    if (strName2 != null)
                    {
                        try
                        {
                            for (int k = 0; k < TemperatureControl._StanderTitleList.Count; k++)
                            {
                                if (TemperatureControl._StanderTitleList[k] == strName2)
                                {
                                    if (k <= (TemperatureControl._StanderNameList.Count - 1))
                                    {
                                        string newValue = TemperatureControl._StanderNameList[k];
                                        PropertyDescriptor pd = TypeDescriptor.GetProperties(context.Instance)["Name"];
                                        if (pd != null)
                                        {
                                            pd.SetValue(context.Instance, newValue);
                                        }
                                        
                                    }
                                    break;
                                }
                            }
                        }
                        catch
                        { 
                        }
                        return strName2;
                    }
                }
              
            return value;
            }
        }
        private void lst_SelectedIndexChanged(object sender, EventArgs e)
        {
            strName2 = ((System.Windows.Forms.ListBox)sender).Text;
            
            myService.CloseDropDown();
        }
    }
}
