using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DCSoft
{
    /// <summary>
    /// 属性值修改日志记录器
    /// </summary>
    internal static class PropertyValueLogger
    {
        public static void GetLoggedProperties(object instance, JsonObject outputJson, bool autoRemove)
        {
            if (_Values.Count > 0
                && outputJson != null
                && instance != null)
            {
                for (var iCount = _Values.Count - 1; iCount >= 0; iCount--)
                {
                    if (_Values[iCount].Reference.IsAlive == false)
                    {
                        RemoveInstance(iCount);
                    }
                    else if (_Values[iCount].Reference.Target == instance)
                    {
                        var ps = _Values[iCount];
                        var runtimeRemove = autoRemove && ps.KeepLive == false;
                        foreach (var kvp in ps)
                        {
                            if (runtimeRemove)
                            {
                                outputJson[kvp.Key] = kvp.Value;
                            }
                            else
                            {
                                outputJson[kvp.Key] = kvp.Value?.DeepClone();
                            }
                        }
                        if (runtimeRemove)
                        {
                            RemoveInstance(iCount);
                        }
                        else
                        {
                            ps.KeepLive = true;
                        }
                        return;
                    }
                }
            }
        }
        private static void RemoveInstance(int index)
        {
            var item = _Values[index];
            item.Clear();
            item.Reference = null;
            _Values.RemoveAt(index);
        }
        /// <summary>
        /// 获得已经创建句柄的所有对象的属性值修改日志
        /// </summary>
        /// <returns></returns>
        public static JsonObject GetAllLoggedPropertiesHasHandle()
        {
            if (_Values.Count > 0)
            {
                var jsonResult = new JsonObject();
                for (var iCount = _Values.Count - 1; iCount >= 0; iCount--)
                {
                    if (_Values[iCount].Reference.IsAlive == false)
                    {
                        RemoveInstance(iCount);
                    }
                    else
                    {
                        var instance = _Values[iCount].Reference.Target;
                        var intHandle = 0;
                        if (instance is Control ctl)
                        {
                            if (ctl.IsHandleCreated)
                            {
                                intHandle = ctl.Handle.ToInt32();
                            }
                        }
                        else if (instance is ToolStripItem item)
                        {
                            if (item.IsHandleCreated)
                            {
                                // 仅处理已创建句柄的控件
                                intHandle = item.Handle.ToInt32();
                            }
                        }
                        if (intHandle > 0)
                        {
                            var propertyValues = _Values[iCount];
                            var objJson = new JsonObject();
                            foreach (var kvp in propertyValues)
                            {
                                if(propertyValues.KeepLive )
                                {
                                    objJson[kvp.Key] = kvp.Value?.DeepClone();
                                }
                                else
                                {
                                    objJson[kvp.Key] = kvp.Value;
                                }
                            }
                            jsonResult[intHandle.ToString()] = objJson;
                            if (propertyValues.KeepLive == false)
                            {
                                RemoveInstance(iCount);
                            }
                        }
                    }
                }
                return jsonResult;
            }
            else
            {
                return null;
            }
        }
        private class PropertyValuePackage : Dictionary<string, JsonNode>
        {
            public WeakReference Reference = null;
            public bool KeepLive = false;
        }
        private static readonly List<PropertyValuePackage> _Values = new List<PropertyValuePackage>();
        private static PropertyValuePackage GetPropertyValues(object instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            for (var iCount = _Values.Count - 1; iCount >= 0; iCount--)
            {
                if (_Values[iCount].Reference.IsAlive == false)
                {
                    RemoveInstance(iCount);
                }
                else if (_Values[iCount].Reference.Target == instance)
                {
                    return _Values[iCount];
                }
            }
            var newValue = new PropertyValuePackage();
            newValue.Reference = new WeakReference(instance);
            _Values.Add(newValue);
            return newValue;
        }
        //private static Dictionary<string, JsonNode> GetPropertyValues(IntPtr intHandle)
        //{
        //    Dictionary<string, JsonNode> propertyValues;
        //    if (_PropertyValues.TryGetValue(intHandle.ToInt32(), out propertyValues))
        //    {
        //        return propertyValues;
        //    }
        //    else
        //    {
        //        propertyValues = new Dictionary<string, JsonNode>();
        //        _PropertyValues[intHandle.ToInt32()] = propertyValues;
        //        return propertyValues;
        //    }
        //}
        /// <summary>
        /// 记录属性值修改日志
        /// </summary>
        /// <param name="intHandle">对象句柄</param>
        /// <param name="strName">属性名</param>
        /// <param name="vValue">属性值</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void Log(object instance, string strName, string vValue)
        {
            if (strName == null || strName.Length == 0)
            {
                throw new ArgumentNullException("strName");
            }
            var propertyValues = GetPropertyValues(instance);
            propertyValues[strName] = JsonValue.Create(vValue);
            InvalidateState(instance);
        }
        public static void Log(object instance, string strName, JsonNode vValue)
        {
            if (strName == null || strName.Length == 0)
            {
                throw new ArgumentNullException("strName");
            }
            var propertyValues = GetPropertyValues(instance);
            propertyValues[strName] = vValue;
            InvalidateState(instance);
        }
        public static void Log(object instance, string strName, bool vValue)
        {
            if (strName == null || strName.Length == 0)
            {
                throw new ArgumentNullException("strName");
            }
            var propertyValues = GetPropertyValues(instance);
            propertyValues[strName] = JsonValue.Create(vValue);
            InvalidateState(instance);
        }

        public static void Log(object instance, string strName, int vValue)
        {
            if (strName == null || strName.Length == 0)
            {
                throw new ArgumentNullException("strName");
            }
            var propertyValues = GetPropertyValues(instance);
            propertyValues[strName] = JsonValue.Create(vValue);
            InvalidateState(instance);
        }
        public static void Log(object instance, string strName, float vValue)
        {
            if (strName == null || strName.Length == 0)
            {
                throw new ArgumentNullException("strName");
            }
            var propertyValues = GetPropertyValues(instance);
            propertyValues[strName] = JsonValue.Create(vValue);
            InvalidateState(instance);
        }
        public static void Log(object instance, string strName, double vValue)
        {
            if (strName == null || strName.Length == 0)
            {
                throw new ArgumentNullException("strName");
            }
            var propertyValues = GetPropertyValues(instance);
            propertyValues[strName] = JsonValue.Create(vValue);
            InvalidateState(instance);
        }
        public static void Log(object instance, string strName, decimal vValue)
        {
            if (strName == null || strName.Length == 0)
            {
                throw new ArgumentNullException("strName");
            }
            var propertyValues = GetPropertyValues(instance);
            propertyValues[strName] = JsonValue.Create(vValue);
            InvalidateState(instance);
        }
        public static void Log(object instance, string strName, DateTime vValue)
        {
            if (strName == null || strName.Length == 0)
            {
                throw new ArgumentNullException("strName");
            }
            var propertyValues = GetPropertyValues(instance);
            propertyValues[strName] = JsonValue.Create(vValue);
            InvalidateState(instance);
        }
        public static void Log(object instance, string strName, Enum vValue)
        {
            if (strName == null || strName.Length == 0)
            {
                throw new ArgumentNullException("strName");
            }
            var propertyValues = GetPropertyValues(instance);
            propertyValues[strName] = JsonValue.Create(vValue.ToString());
            InvalidateState(instance);
        }
        public static void LogBounds(object instance, int vLeft, int vTop, int vWidth, int vHeight)
        {
            var propertyValues = GetPropertyValues(instance);
            propertyValues["Left"] = JsonValue.Create(vLeft);
            propertyValues["Top"] = JsonValue.Create(vTop);
            propertyValues["Width"] = JsonValue.Create(vWidth);
            propertyValues["Height"] = JsonValue.Create(vHeight);
            InvalidateState(instance);
        }
        public static void Log(object instance, string strName, JsonObject vValue)
        {
            if (strName == null || strName.Length == 0)
            {
                throw new ArgumentNullException("strName");
            }
            var propertyValues = GetPropertyValues(instance);
            propertyValues[strName] = vValue;
            InvalidateState(instance);
        }
        public static void Log(object instance, string strName, char vValue)
        {
            if (strName == null || strName.Length == 0)
            {
                throw new ArgumentNullException("strName");
            }
            var propertyValues = GetPropertyValues(instance);
            propertyValues[strName] = JsonValue.Create(vValue);
            InvalidateState(instance);
        }
        public static void Log(object instance, string strName, Color vValue)
        {
            if (strName == null || strName.Length == 0)
            {
                throw new ArgumentNullException("strName");
            }
            var propertyValues = GetPropertyValues(instance);
            propertyValues[strName] = JsonValue.Create(ColorTranslator.ToHtml(vValue));
            InvalidateState(instance);
        }
        public static void Log(object instance, string strName, Image vValue)
        {
            if (strName == null || strName.Length == 0)
            {
                throw new ArgumentNullException("strName");
            }
            var propertyValues = GetPropertyValues(instance);
            propertyValues[strName] = JsonValue.Create(vValue == null ? null : vValue.CreateBlobUrl());
            InvalidateState(instance);
        }

        public static void Log(object instance, string strName, Size vValue)
        {
            if (strName == null || strName.Length == 0)
            {
                throw new ArgumentNullException("strName");
            }
            var propertyValues = GetPropertyValues(instance);
            propertyValues[strName] = new JsonObject
            {
                ["Width"] = vValue.Width,
                ["Height"] = vValue.Height
            };
            InvalidateState(instance);
        }

        public static void Log(object instance, string strName, Padding vValue)
        {
            if (strName == null || strName.Length == 0)
            {
                throw new ArgumentNullException("strName");
            }
            var propertyValues = GetPropertyValues(instance);
            propertyValues[strName] = new JsonObject
            {
                ["Left"] = vValue.Left,
                ["Top"] = vValue.Top,
                ["Right"] = vValue.Right,
                ["Bottom"] = vValue.Bottom
            };
            InvalidateState(instance);
        }


        private static void InvalidateState(object instance)
        {
            if (instance is Control ctl && ctl.IsHandleCreated)
            {
                DCWin32API.JSRuntime.Invoke<object>("__DCWin32API.UpdatePropertyValues");
            }
        }
    }
}
