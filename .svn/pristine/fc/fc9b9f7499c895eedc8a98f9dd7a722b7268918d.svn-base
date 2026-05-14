global using DCSoft;
global using System;
global using System.Collections.Generic;

using System.Drawing;
using System.Runtime.CompilerServices;
using System.Text.Json.Nodes;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Text;
using DCSoft.WF2W.Core.WinFormCore;

namespace DCSoft
{
    /// <summary>
    /// WinForm底层API调用的转发器
    /// </summary>
    public static class DCWin32API
    {
        static DCWin32API()
        {
            DCSR.EventGetStringValue = delegate (string key)
            {
                return JSRuntime.Invoke<string>("__DCGetResourceString", key);
            };
            DCTextUtils.EventCreateBlobUrl = CreateBlobUrl;
            DCTextUtils.EventDeleteBlobUrl = delegate (string strUrl)
            {
                if (strUrl != null && strUrl.StartsWith("blob:http", StringComparison.OrdinalIgnoreCase))
                {
                    JSRuntime.Invoke<object>("URL.revokeObjectURL", strUrl);
                }
            };
        }



        //internal static string CreateBlobUrl(this System.Drawing.Image img)
        //{
        //    return DCWin32API.CreateBlobUrl(img.ToBinary(), img.GetMimeType());
        //}

        private static IDCJSRuntime _JSRuntime = null;

        internal static readonly JSRuntimePackage JSRuntime = new JSRuntimePackage();

        internal sealed class JSRuntimePackage
        {
            public JSRuntimePackage()
            {
            }

            public T Invoke<T>(string identifier, params object?[]? args)
            {
                try
                {
                    return _JSRuntime.Invoke<T>(identifier, args);
                }
                catch (Exception ext)
                {
                    Console.Error.WriteLine(identifier + ":" + ext.ToString());
                    throw ext;
                }
            }
            public System.Threading.Tasks.ValueTask<T> InvokeAsync<T>(string identifier, params object?[]? args)
            {
                return _JSRuntime.InvokeAsync<T>(identifier, args);
            }
            public System.Threading.Tasks.ValueTask InvokeVoidAsync(string identifier, params object?[]? args)
            {
                return _JSRuntime.InvokeVoidAsync(identifier, args);
            }
        }

        public static async ValueTask DefWindowProc(int handle, int msg, int wParam, int lParam)
        {
            await JSRuntime.InvokeVoidAsync("__DCWin32API.DefWindowProc", handle, msg, wParam, lParam);
        }

        private static readonly JsonValue _NullJsonObject = JsonValue.Create((string)null);
        private static List<object> _MarshalObjects = new List<object>();

        public static int MarshalObjectToPtr(object obj)
        {
            for (var iCount = _MarshalObjects.Count - 1; iCount >= 0; iCount--)
            {
                if (_MarshalObjects[iCount] == _NullJsonObject)
                {
                    _MarshalObjects[iCount] = obj;
                    return iCount;
                }
            }
            _MarshalObjects.Add(obj);
            return _MarshalObjects.Count - 1;
        }
        public static JsonValue MarshalPtrToJsonValue(int ptr)
        {
            if (ptr >= 0 && ptr < _MarshalObjects.Count)
            {
                var obj = _MarshalObjects[ptr];
                if (obj != _NullJsonObject)
                {
                    if (obj is JsonValue jv)
                    {
                        return jv;
                    }
                    else
                    {
                        return JsonValue.Create(obj);
                    }
                }
            }
            return null;
        }
        public static object MarshalPtrToObject(int ptr)
        {
            if (ptr >= 0 && ptr < _MarshalObjects.Count)
            {
                var obj = _MarshalObjects[ptr];
                if (obj != _NullJsonObject)
                {
                    return obj;
                }
            }
            return null;
        }
        public static void MarshalFreePtr(int ptr)
        {
            if (ptr >= 0 && ptr < _MarshalObjects.Count)
            {
                _MarshalObjects[ptr] = _NullJsonObject;
            }
        }
        public static void MarshalReturnStringValue(string str, int ptr)
        {
            if (str != null && str.Length > 0 && ptr >= 0 && ptr < _MarshalObjects.Count)
            {
                var sb = _MarshalObjects[ptr] as System.Text.StringBuilder;
                if (sb != null)
                {
                    sb.Append(str);
                }
            }
        }

        /// <summary>
        /// 启动对象
        /// </summary>
        /// <param name="rt"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static async ValueTask Start(IDCJSRuntime rt, System.Reflection.Assembly entryPointAssembly)
        {
            if (entryPointAssembly == null)
            {
                throw new ArgumentNullException("entryPointAssembly");
            }
            var eaFullName = entryPointAssembly.GetName();
            if (eaFullName != null)
            {
                Application._ProductName = eaFullName.Name;
                Application._ProductVersion = eaFullName.Version.ToString();
            }
            DCValueConvert.InBlazorWASM();
            System.Drawing.Graphics.DCPaintToImageHandler = async delegate (byte[] bsImage, string strImageFormat) {
                return await DCWin32API.JSRuntime.InvokeAsync<byte[]>("__DCPaintToImage", bsImage, strImageFormat);
            };
            System.Resources.Extensions.DCDeserializingResourceReader.EventGetType = delegate (string strTypeName)
            {
                var index2 = strTypeName.IndexOf(',');
                if (index2 > 0)
                {
                    var strSimpleName = strTypeName.Substring(0, index2);
                    var t2 = typeof(DCWin32API).Assembly.GetType(strSimpleName, false, true);
                    if (t2 != null)
                    {
                        return t2;
                    }
                    t2 = typeof(System.Drawing.Image).Assembly.GetType(strSimpleName, false, true);
                    if (t2 != null)
                    {
                        return t2;
                    }
                    t2 = Type.GetType(strSimpleName, false, true);
                    if (t2 != null)
                    {
                        return t2;
                    }
                    if (strSimpleName.Equals("System.CodeDom.MemberAttributes", StringComparison.OrdinalIgnoreCase))
                    {
                        return typeof(System.ComponentModel.MySystemCodeDomMemberAttributes);
                    }
                    switch (strSimpleName)
                    {
                        case "System.Drawing.Size": return typeof(System.Drawing.Size);
                        case "System.Drawing.SizeF": return typeof(System.Drawing.SizeF);
                        case "System.Drawing.Point": return typeof(System.Drawing.Point);
                        case "System.Drawing.PointF": return typeof(System.Drawing.PointF);
                        case "System.Drawing.Rectangle": return typeof(System.Drawing.Rectangle);
                        case "System.Drawing.RectangleF": return typeof(System.Drawing.RectangleF);
                        case "System.Drawing.Color": return typeof(System.Drawing.Color);
                        case "System.Drawing.FontStyle": return typeof(System.Drawing.FontStyle);
                        case "System.Drawing.ContentAlignment": return typeof(System.Drawing.ContentAlignment);
                        case "System.Windows.Forms.ImageLayout": return typeof(System.Windows.Forms.ImageLayout);
                        case "System.Windows.Forms.BorderStyle": return typeof(System.Windows.Forms.BorderStyle);
                        case "System.Drawing.Bitmap": return typeof(System.Drawing.Bitmap);
                    }
                }
                return null;
            };

            DCFontList.Start();

            if (rt == null)
            {
                throw new ArgumentNullException("rt");
            }
            _JSRuntime = rt;
            var bs = DCTextUtils.LoadBinaryResource(typeof(DCWin32API).Assembly, "wf2w.boot.js", true);
            var strText = System.Text.Encoding.UTF8.GetString(bs);
            if (entryPointAssembly != null)
            {
                var strName = entryPointAssembly.GetName().Name;
                var index = strName.IndexOf(",");
                if (index > 0)
                {
                    strName = strName.Substring(0, index).Trim();
                }
                strText = strText
                    + Environment.NewLine
                    + " window.__DCEntryPointAssemblyName = \"" + strName + "\";";
            }
            JSRuntime.Invoke<object>("eval", strText);

            await LoadJSModule("DCWin32API.js");
            //await LoadJSModule("DCFontList.js");
            //var supportTypes = new Type[] {
            //    typeof( System.Drawing.Graphics),
            //    typeof( System.Windows.Forms.Control),
            //    typeof( System.Windows.Forms.Button),
            //    typeof( System.Windows.Forms.Form ),
            //    typeof( System.Windows.Forms.Label),
            //    typeof( System.Windows.Forms.TextBox ),
            //    typeof( System.Windows.Forms.MessageBox ),
            //    typeof( System.Windows.Forms.Panel ),
            //    typeof( System.Windows.Forms.PictureBox),
            //    typeof( System.Windows.Forms.TabControl ),
            //    typeof( System.Windows.Forms.TabPage),
            //    typeof( System.Windows.Forms.ComboBox ),
            //    typeof( System.Windows.Forms.MainMenu ),
            //    typeof( System.Windows.Forms.ScrollableControl),
            //    typeof( System.Windows.Forms.ContainerControl ),
            //    typeof(System.Windows.Forms.UserControl ),
            //    typeof( System.Windows.Forms.ToolTip)
            //};
            foreach (var type in DCWinFormHelper._SupportedTypes)
            {
                await LoadJSModule(type.FullName + ".js");
            }
            //await LoadJSModule("System.Windows.Forms.Button.js");
            //await LoadJSModule("System.Windows.Forms.Form.js");
            //await LoadJSModule("System.Windows.Forms.Label.js");
            //await LoadJSModule("System.Windows.Forms.TextBox.js");
            //await LoadJSModule("System.Windows.Forms.MessageBox.js");
            await LoadJSModule("DCWinForm2WASM.js");
            await LoadJSModule("System.Drawing.Printing.js");
            //JSRuntime.Invoke<object>("eval", "delete __DCLoadScript20251231;");

            System.Drawing.Printing.PrintController.EventBeginPrint = delegate (string strDocumentName)
            {
                return JSRuntime.Invoke<int>("__WF2WPrinting20260206.BeginPrint", strDocumentName);
            };
            System.Drawing.Printing.PrintController.EventSetPageContent = delegate(int printJobId, string strPageContent, int pageIndex, int pageCount)
            {
                return JSRuntime.Invoke<bool>("__WF2WPrinting20260206.SetPageContent", printJobId, strPageContent, pageIndex, pageCount);
            };
            System.Drawing.Printing.PrintController.EventEndPrint = delegate(int printJobId)
            {
                return JSRuntime.Invoke<bool>("__WF2WPrinting20260206.EndPrint", printJobId);
            };

            Console.WriteLine("Loaded " + typeof(DCWin32API).Name);
            //_JSRuntime.Invoke<object>("__StartDCSoftWinForm2WASM");
        }

        public static async ValueTask EnsureLoadJSModule(string resName)
        {
            await LoadJSModule(resName);
        }
        public static async ValueTask EnsureLoadJSModule(Type t)
        {
            await LoadJSModule(t.FullName + ".js");
        }

        private static readonly List<string> _LoadedJSModules = new List<string>();
        private static async ValueTask LoadJSModule(string resName)
        {
            if (resName == null)
            {
                throw new ArgumentNullException("resName");
            }
            if (_LoadedJSModules.Contains(resName))
            {
                return;
            }
            _LoadedJSModules.Add(resName);
            var bs = DCTextUtils.LoadBinaryResource(typeof(DCWin32API).Assembly, resName, false);
            if (bs != null && bs.Length > 0)
            {
                // 尝试去掉BOM头
                string strJs = null;
                for (var iCount = 0; iCount < bs.Length && iCount < 10; iCount++)
                {
                    if (bs[iCount] >= 8 && bs[iCount] < 127)
                    {
                        strJs = Encoding.UTF8.GetString(bs, iCount, bs.Length - iCount);
                        break;
                    }
                }
                if (strJs == null)
                {
                    strJs = Encoding.UTF8.GetString(bs);
                }
                //var jmi = new DouglasCrockford.JsMin.JsMinifier();
                //var str2 = new StringBuilder();
                //jmi.Minify(strJs, str2);
                strJs = DCTextUtils.MimiJavaScript(strJs);
                bs = Encoding.UTF8.GetBytes(strJs);
                // 添加BOM头
                var bs2 = new byte[bs.Length + 3];
                bs2[0] = 0XEF;
                bs2[1] = 0XBB;
                bs2[2] = 0XBF;
                Array.Copy(bs, 0, bs2, 3, bs.Length);
                bs = bs2;
            }
            //else
            //{
            //    Console.Error.WriteLine("未找到内嵌资源:" + resName);
            //}
            Console.WriteLine("尝试加载JS模块:" + resName);
            var bolTryLoadFile = false;// true;// typeof(DCTextUtils).Name == "DCTextUtils";
            await JSRuntime.InvokeVoidAsync(
                "__DCLoadScript20251231",
                bs,
                resName,
                bolTryLoadFile);
        }
        /// <summary>
        /// 设置屏幕大小
        /// </summary>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <param name="defaultFontName">默认字体名称</param>
        /// <param name="defaultFontSize">Point为单位的默认字体大小</param>
        public static void SetScreenSize(
            int width,
            int height,
            string defaultFontName,
            float defaultFontSize)
        {
            Screen.SetScreenSize(width, height);
            if (string.IsNullOrEmpty(defaultFontName) == false
                && FontFamily.IsSupportedFontName(defaultFontName))
            {
                if (defaultFontSize <= 0)
                {
                    defaultFontSize = 9;
                }
                System.Drawing.SystemFonts.DefaultFont = new System.Drawing.Font(
                    defaultFontName,
                    defaultFontSize,
                    System.Drawing.FontStyle.Regular);
            }
        }

        private const int _NullHandle = 1000 * 1000 * 1000;
        private static int _HandleCounterForPackage = _NullHandle + 1;
        private static Dictionary<int, object> _ObjectPackage = null;
        public static int PackageToHandle(object obj)
        {
            if (obj == null)
            {
                return _NullHandle;
            }
            if (_ObjectPackage == null)
            {
                _ObjectPackage = new Dictionary<int, object>();
            }
            var handle = _HandleCounterForPackage++;
            _ObjectPackage[handle] = obj;
            return handle;
        }
        internal static T GetPackagedObjectByHandle<T>(int handle)
        {
            if (_ObjectPackage != null && _ObjectPackage.TryGetValue(handle, out object obj))
            {
                _ObjectPackage.Remove(handle);
                return (T)obj;
            }
            return default(T);
        }

        internal static object GetPackagedObjectByHandleOnce(int handle)
        {
            if (_ObjectPackage != null && _ObjectPackage.TryGetValue(handle, out object obj))
            {
                _ObjectPackage.Remove(handle);
                return obj;
            }
            return null;
        }

        private static int _HandleCounter = 1;
        public static int AllocHandle(object ctl)
        {
            if (ctl == null)
            {
                return _HandleCounter++;
            }
            else
            {
                var h = _HandleCounter++;
                _Controls[h] = ctl;
                return h;
            }
        }
        public static void ResetForAllocHandle(int seed)
        {
            _HandleCounter = seed;
        }

        private static readonly Dictionary<int, object> _Controls = new Dictionary<int, object>();
        //private static int _HandleCounter = 1;
        ///// <summary>
        ///// 分配一个唯一的Handle值
        ///// </summary>
        ///// <returns></returns>
        //public static IntPtr AllocHandle()
        //{
        //    return _HandleCounter++;
        //}
        public static void RegisterControl(int handle, object control)
        {
            if (control is System.Windows.Forms.Control ctl2)
            {
                if (ctl2.Handle == IntPtr.Zero)
                {
                    //ctl2.Handle = AllocHandle();
                }
            }
            _Controls[handle] = control;
        }
        public static void RemoveHandle(int handle)
        {
            _Controls.Remove(handle);
        }

        internal static object GetControl(int handle)
        {
            if (_Controls.TryGetValue(handle, out var control))
            {
                return control;
            }
            return null;
        }

        /// <summary>
        /// 发送消息到控件
        /// </summary>
        /// <param name="handle">控件句柄</param>
        /// <param name="msg">消息</param>

        public async static void SendMessageToControl2(System.Windows.Forms.Message msg)
        {
            var ctl = GetControl(msg.HWnd.ToInt32()) as System.Windows.Forms.Control;
            if (ctl != null)
            {
                if (System.Windows.Forms.Application.ThreadContext.FromCurrent().ProcessFilters(ref msg, out bool modified))
                {
                    return;
                }
                await ctl.PublicWnProc(msg);
                if (DCMessageManager.HasPostedMessages())
                {
                    // 存在已经发送的消息，等待延时处理
                    JSRuntime.Invoke<object>("__DCHandlePostedMessage", null);
                }
            }
        }

        public static string CreateBlobUrl(byte[] bsData, string strMimeType)
        {
            if (bsData != null && bsData.Length > 0)
            {
                var strResult = JSRuntime.Invoke<string>("__DCWin32API.CreateBlobUrl", bsData, strMimeType, null, false);
                return strResult;
            }
            else
            {
                return null;
            }
        }

        private static List<string> _StandardControlTypeNames = new List<string>();
        public static void AddStandardControlTypeName(string typeName)
        {
            if (_StandardControlTypeNames.Contains(typeName) == false)
            {
                _StandardControlTypeNames.Add(typeName);
            }
        }

        public static bool IsStandardControlType(Type t)
        {
            var tn = t.FullName;
            foreach (var item in _StandardControlTypeNames)
            {
                if (string.Equals(item, tn, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }

        public struct WindowBoundsInfo
        {
            public int Left;
            public int Top;
            public int Width;
            public int Height;
            public int ClientWidth;
            public int ClientHeight;
        }
        public static WindowBoundsInfo GetWindowBoundsInfo(IntPtr handle)
        {
            var json = JSRuntime.Invoke<JsonObject>("__DCWin32API.GetWindowBounds", handle.ToInt32());
            WindowBoundsInfo info = new WindowBoundsInfo();
            foreach (var item in json)
            {
                if (item.Key == "Left") info.Left = DCValueConvert.ConvertToInt32(item.Value, 0);
                else if (item.Key == "Top") info.Top = DCValueConvert.ConvertToInt32(item.Value, 0);
                else if (item.Key == "Width") info.Width = DCValueConvert.ConvertToInt32(item.Value, 0);
                else if (item.Key == "Height") info.Height = DCValueConvert.ConvertToInt32(item.Value, 0);
                else if (item.Key == "ClientWidth") info.ClientWidth = DCValueConvert.ConvertToInt32(item.Value, 0);
                else if (item.Key == "ClientHeight") info.ClientHeight = DCValueConvert.ConvertToInt32(item.Value, 0);
            }
            return info;
        }
        //public static JsonObject GetWindowPlacement(int handle)
        //{
        //    return _JSRuntime.Invoke<JsonObject>("__DCWin32API.GetWindowPlacement", handle);
        //}
        //public static bool SetForegroundWindow(int handle)
        //{
        //    return _JSRuntime.Invoke<bool>("__DCWin32API.SetForegroundWindow", handle);
        //}
        //public static int GetFocus()
        //{
        //    return _JSRuntime.Invoke<int>("__DCWin32API.GetFocus");
        //}

        //public static int GetWindow(int hWnd, int uCmd)
        //{
        //    return _JSRuntime.Invoke<int>("__DCWin32API.GetWindow", hWnd, uCmd);
        //}
        internal static bool GetClientRect(int handle, ref WinFormNativeMethods.RECT rect)
        {
            var strData = JSRuntime.Invoke<string>("__DCWin32API.GetClientRect", handle);
            if (string.IsNullOrEmpty(strData))
            {
                var ctl2 = Control.FromHandleInternal(handle);
                if (ctl2 != null)
                {
                    var size3 = ctl2.InnerGetClientSize();
                    if (size3.IsEmpty == false)
                    {
                        rect.left = 0;
                        rect.top = 0;
                        rect.right = size3.Width;
                        rect.bottom = size3.Height;
                        return true;
                    }
                }
                return false;
            }
            else
            {
                var strItems = strData.Split('|');
                rect.left = int.Parse(strItems[0]);
                rect.top = int.Parse(strItems[1]);
                rect.right = rect.left + int.Parse(strItems[2]);
                rect.bottom = rect.top + int.Parse(strItems[3]);
                return true;
            }
        }
        //public static JsonObject GetWindowBoundsInfo(int handle)
        //{
        //    return _JSRuntime.Invoke<JsonObject>("__DCWin32API.GetWindowBoundsInfo", handle);
        //}
        //public static bool IsWindow(int handle)
        //{
        //    return _JSRuntime.Invoke<bool>("__DCWin32API.IsWindow", handle);
        //}
        //public static JsonObject GetWindowRect(int handle)
        //{
        //    return _JSRuntime.Invoke<JsonObject>("__DCWin32API.GetWindowRect", handle);
        //}
        //public static int SetParent(int hWndChild, int hWndNewParent)
        //{
        //    return _JSRuntime.Invoke<int>("__DCWin32API.SetParent", hWndChild, hWndNewParent);
        //}
        //public static bool IsChild(int hWndParent, int hWndChild)
        //{
        //    return _JSRuntime.Invoke<bool>("__DCWin32API.IsChild", hWndParent, hWndChild);
        //}
        //public static int SetFocus(int handle)
        //{
        //    return _JSRuntime.Invoke<int>("__DCWin32API.SetFocus", handle);
        //}

        //public static int GetParent(int handle)
        //{
        //    return _JSRuntime.Invoke<int>("__DCWin32API.GetParent", handle);
        //}

        //public static int GetWindowTextLength(int handle)
        //{
        //    return _JSRuntime.Invoke<int>("__DCWin32API.GetWindowTextLength", handle);
        //}
        public static string GetWindowText(int handle)
        {
            return JSRuntime.Invoke<string>("__DCWin32API.GetWindowText", handle);
        }
        public static bool SetWindowText(int handle, string text)
        {
            return JSRuntime.Invoke<bool>("__DCWin32API.SetWindowText", handle, text);
        }
        ///// <summary>
        ///// 设置窗体可见性
        ///// </summary>
        //public static bool ShowWindow(int handle, int nCmdShow)
        //{
        //    return _JSRuntime.Invoke<bool>("__DCWin32API.ShowWindow", handle, nCmdShow);
        //}
        ///// <summary>
        ///// 设置窗体属性
        ///// </summary>
        //public static int SetWindowLong(int hWnd, int nIndex, int dwNewLong)
        //{
        //    return _JSRuntime.Invoke<int>("__DCWin32API.SetWindowLong", hWnd, nIndex, dwNewLong);
        //}
        ///// <summary>
        ///// 获得窗体属性
        ///// </summary>
        //public static int GetWindowLong(int hWnd, int nIndex)
        //{
        //    return _JSRuntime.Invoke<int>("__DCWin32API.GetWindowLong", hWnd, nIndex);
        //}
        /// <summary>
        /// 设置窗体的字体
        /// </summary>
        public static bool SetControlFont(int handle, System.Drawing.Font font)
        {
            if (font != null)
            {
                var obj = font.ToJsonObject();
                return JSRuntime.Invoke<bool>(
                    "__DCWin32API.SetControlFont",
                    handle,
                    obj);
            }
            return false;
        }
        /// <summary>
        /// 设置控件鼠标光标
        /// </summary>
        public static bool SetControlCursor(int handle, System.Windows.Forms.Cursor cur)
        {
            return JSRuntime.Invoke<bool>(
                "__DCWin32API.SetControlCursor",
                handle,
                cur == null ? Cursors.Default : cur.HtmlName);
        }
        /// <summary>
        /// 创建窗体对象
        /// </summary>
        public static IntPtr CreateWindowEx(
            int dwExStyle,
            string lpszClassName,
            string lpszWindowName,
            int style,
            int x,
            int y,
            int width,
            int height,
            System.Runtime.InteropServices.HandleRef hWndParent,
            HandleRef hMenu,
            HandleRef hInst,
            CreateParams cp)
        {
            try
            {
                var json = new JsonObject();
                if (cp.ControlType == null)
                {
                    throw new ArgumentNullException("cp.ControlType");
                }
                json["ControlTypeName"] = cp.ControlType.FullName;
                Console.WriteLine("创建控件:" + cp.ControlType.FullName);
                var strBaseTypes = new System.Text.StringBuilder();
                if (IsStandardControlType(cp.ControlType) == false)
                {
                    var bt = cp.ControlType.BaseType;
                    while (bt != null && bt != typeof(object))
                    {
                        if (strBaseTypes.Length > 0)
                        {
                            strBaseTypes.Append("#" + bt.FullName);
                        }
                        else
                        {
                            strBaseTypes.Append(bt.FullName);
                        }
                        if (IsStandardControlType(bt))
                        {
                            break;
                        }
                        bt = bt.BaseType;
                    }
                    json["BaseControlTypeName"] = strBaseTypes.ToString();
                }
                if (cp.ControlInstance != null)
                {
                    if (cp.ControlInstance.IsUserPaint())
                    {
                        json["UserPaint"] = true;
                    }
                    //DCWinFormHelper.WritePropertyValueToJson(cp.ControlInstance, json);
                    PropertyValueLogger.GetLoggedProperties(cp.ControlInstance, json,true);
                    cp.ControlInstance.WriteAdditionJsonForCreateControl(json);
                    //if(cp.ControlInstance is Form form )
                    //{
                    //    if (form.ShowIcon && form.Icon != null)
                    //    {
                    //        json["IconImage"] = form.Icon.CreateBlobUrl(true);
                    //    }
                    //}
                    //DCWinFormHelper.WritePropertyValueToJson(cp.ControlInstance, json);
                    //cp.ControlInstance.WriteAttributeTo(json);
                }
                json["Text"] = lpszWindowName;
                //json["BackColor"] = ColorTranslator.ToHtml(cp.BackColor);
                //json["ForeColor"] = ColorTranslator.ToHtml(cp.ForeColor);
                //if (cp.Font != null)
                //{
                //    json.Add("Font", DCValueConvert.FontToCssString(cp.Font));
                //}
                //json["Handle"] = WinFormAdapter.AllocHandle();
                json["ClassName"] = cp.ClassName;
                json["ClassStyle"] = cp.ClassStyle;
                json["ExStyle"] = dwExStyle;
                json["Style"] = style;
                json["Left"] = x == WinFormNativeMethods.CW_USEDEFAULT ? 0 : x;
                json["Top"] = y == WinFormNativeMethods.CW_USEDEFAULT ? 0 : y;
                json["Width"] = width == WinFormNativeMethods.CW_USEDEFAULT ? 100 : width;
                json["Height"] = height == WinFormNativeMethods.CW_USEDEFAULT ? 100 : height;
                json["ParentHandle"] = hWndParent.Handle.ToInt32();
                json["hMenu"] = hMenu.Handle.ToInt32();
                json["hInst"] = hInst.Handle.ToInt32();
                if (cp.Param != null)
                {
                    json["Param"] = Convert.ToString(cp.Param);
                }
                var newHandle = AllocHandle(null);
                json["Handle"] = newHandle;
                var str = json.ToJsonString();
                JSRuntime.Invoke<object>("__DCWin32API.CreateWindowEx", json);
                return new IntPtr(newHandle);
            }
            catch (Exception ext)
            {
                Console.WriteLine(cp.ControlType.FullName + "#" + ext.ToString());
                throw ext;
            }
            ////return IntCreateWindowEx(dwExStyle, lpszClassName,
            ////                             lpszWindowName, style, x, y, width, height, hWndParent, hMenu,
            ////                             hInst, pvParam);


            //var handle = Application.AllocHandle();
            //args["Handle"] = handle;
            //return EventInvokeJSFunctionReturnInt32("__DCWin32API.CreateWindowEx", PackageOneParameter(args));
        }
        ///// <summary>
        ///// 设置窗体位置
        ///// </summary>
        //public static bool SetWindowPos(int hWnd, JsonObject json)
        //{
        //    return _JSRuntime.Invoke<bool>("__DCWin32API.SetWindowPos", hWnd, json);
        //}

        //public static async ValueTask<DialogResult> ShowMessageBoxNew(
        //    IWin32Window owner,
        //    string text,
        //    string caption,
        //    MessageBoxButtons buttons,
        //    MessageBoxIcon icon,
        //    MessageBoxDefaultButton defaultButton,
        //    MessageBoxOptions options,
        //    bool showHelp)
        //{
        //    var args = new JsonObject();
        //    args.Add("Text", text);
        //    args.Add("Caption", caption);
        //    args.Add("Buttons", (int)buttons);
        //    args.Add("Icon", (int)icon);
        //    args.Add("DefaultButton", (int)defaultButton);
        //    args.Add("Options", (int)options);
        //    var intResult = await _JSRuntime.InvokeAsync<int>("__DCWin32API.ShowMessageBoxNew", args);
        //    return (DialogResult)intResult;
        //}
        //public static DialogResult ShowMessageBoxOld(
        //    IWin32Window owner,
        //    string text,
        //    string caption,
        //    MessageBoxButtons buttons,
        //    MessageBoxIcon icon,
        //    MessageBoxDefaultButton defaultButton,
        //    MessageBoxOptions options,
        //    bool showHelp)
        //{
        //    var args = new JsonObject();
        //    args.Add("Text", text);
        //    args.Add("Caption", caption);
        //    args.Add("Buttons", (int)buttons);
        //    args.Add("Icon", (int)icon);
        //    args.Add("DefaultButton", (int)defaultButton);
        //    args.Add("Options", (int)options);
        //    var intResult = _JSRuntime.Invoke<int>("__DCWin32API.ShowMessageBoxOld", args);
        //    return (DialogResult)intResult;
        //}
        //private static object[] _OneArray = new object[1];
        //private static object[] PackageOneParameter(object param)
        //{
        //    _OneArray[0] = param;
        //    return _OneArray;
        //}
    }
}
