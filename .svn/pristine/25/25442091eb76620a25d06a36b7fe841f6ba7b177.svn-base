using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace DCSoft.Common
{
    /// <summary>
    /// IDE调试模式下查看列表类型数据的视图对象
    /// </summary>
    /// <remarks>编制 袁永福</remarks>
    [System.Runtime.InteropServices.ComVisible(false)]
    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = false )]
    public class ListDebugView
    {
        /// <summary>
        /// initialize instance
        /// </summary>
        /// <param name="instance"></param>
        public ListDebugView(object instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            myInstance = instance;
        }

        private readonly object myInstance = null;

        /// <summary>
        /// output debug item at design time
        /// </summary>
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.RootHidden)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public object Items
        {
            get
            {
                if (myInstance is System.Collections.IEnumerable)
                {
                    ArrayList list = new ArrayList();
                    foreach (object obj in ((IEnumerable)myInstance))
                    {
                        list.Add(obj);
                    }
                    return list.ToArray();
                }
                else
                {
                    return myInstance;
                }
            }
        }
    }
}
