using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Design;
using System.ComponentModel;
namespace DCSoft.TemperatureChart
{
    /// <summary>
    /// 标题名字编辑器
    /// </summary>
    //[System.Reflection.Obfuscation( Exclude= true , ApplyToMembers= false  )]
    [System.Runtime.InteropServices.ComVisible( false )]
    public class EditNameUITypeEditor:UITypeEditor
    {
        private System.Windows.Forms.Design.IWindowsFormsEditorService myService = null;
        /// <summary>
        /// 名称初始化
        /// </summary>
        public string strName2 = null;
        /// <summary>
        /// 初始化对象
        /// </summary>
        public EditNameUITypeEditor()
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
                if (TemperatureControl._StanderNameList != null && TemperatureControl._StanderNameList.Count > 0)
                {
                    for (int i = 0; i < TemperatureControl._StanderNameList.Count; i++)
                    {
                        lst.Items.Add(TemperatureControl._StanderNameList[i]);  
                    }
                       
                    myService = (System.Windows.Forms.Design.IWindowsFormsEditorService)
                        provider.GetService(typeof(System.Windows.Forms.Design.IWindowsFormsEditorService));
                    if (myService == null)
                        return value;
                    lst.SelectedIndexChanged += new EventHandler(lst_SelectedIndexChanged);
                    myService.DropDownControl(lst);
                    if (strName2 != null)
                    {
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
