using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Resources;
using System.Collections;
using System.Globalization;
using System.IO;

namespace DCSoft.Common
{
    /// <summary>
    /// 加密后的字符串资源容器对象
    /// </summary>
    [System.Runtime.InteropServices.ComVisible(false)]
    public static class EncryptResourceStringsHelper
    {
      
        private readonly static Dictionary<System.Reflection.Assembly, string[]> _asmResNames 
            = new Dictionary<Assembly, string[]>();

       
       
        private static readonly string _Ext_Resources = ".resources";
        /// <summary>
        /// 创建字符串资源清单
        /// </summary>
        /// <param name="asm">程序集</param>
        /// <param name="resName">资源名称</param>
        /// <returns>字符串清单</returns>
        public static SortedDictionary<string,string> BuildSortedDictionary(
            System.Reflection.Assembly asm,
            string resName)
        {
            var result = new SortedDictionary<string, string>();
            if (_asmResNames.ContainsKey(asm) == false)
            {
                lock (_asmResNames)
                {
                    _asmResNames[asm] = asm.GetManifestResourceNames();
                }
            }
            string targetResName = null;
            var resNames = _asmResNames[asm];
            if (resNames != null)
            {
                //foreach (var name in resNames)
                //{
                //    System.Diagnostics.Debug.WriteLine("RES:  " + name);
                //}
                foreach (var name in resNames)
                {
                    if (name == resName)
                    {
                        targetResName = name;
                        break;
                    }
                    if (name.EndsWith(_Ext_Resources))
                    {
                        string name2 = name.Substring(0, name.Length - 10);
                        if( name2 == resName)
                        {
                            targetResName = name;
                            break;
                        }
                        int index = name2.LastIndexOf('.');
                        if (index > 0)
                        {
                            name2 = name2.Substring(index + 1);
                            if( name2 == resName)
                            {
                                targetResName = name;//.Substring(0, name.Length - 10);
                                break;
                            }
                        }
                    }
                }
            }
            if (targetResName == null)
            { 
                throw new ArgumentOutOfRangeException("Miss Resources:" + resName + " in " + asm.FullName);
                //return result;
            }
            // 加载主资源文件
            var ms = asm.GetManifestResourceStream(targetResName);
            var resset = new ResourceSet(ms);
            FillStrings(resset, result, true);
            resset.Dispose();
            // 加载区域资源文件
            var man = SafeCreateResourceManager.Create(targetResName.Substring(0,targetResName.Length - 10), asm);
            var resset2 = man.GetResourceSet(CultureInfo.CurrentUICulture, true, true);
            FillStrings(resset2, result, false);
            man.ReleaseAllResources();
            
            //System.Windows.Forms.MessageBox.Show("成功加载了 " + asm.FullName + "#" + resName + "#" + result.Count);

            return result;
        }

        private static void FillStrings(ResourceSet rs, SortedDictionary<string,string> result, bool isFirstFill)
        {
            if (rs != null)
            {
                IDictionaryEnumerator values = rs.GetEnumerator();
                while (values.MoveNext())
                {
                    string name = Convert.ToString(values.Key);
                    if (name != null && name.Length > 0 && values.Value is string)
                    {
                        string v = Convert.ToString(values.Value);
                        if (v != null && v.Length > 0)
                        {
                            if (isFirstFill == false)
                            {
                                if (result.ContainsKey(name) == false)
                                {
                                    continue;
                                }
                            }
                            result[name] = v;
                        }
                    }
                }//while
            }
        }
         
       
       

    }
}