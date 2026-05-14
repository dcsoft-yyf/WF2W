

using System.ComponentModel;

using System.Runtime.CompilerServices;
using System.Windows.Forms;
using System.Text.Json.Nodes;
using System.Collections.Generic;
using System;
using System.Drawing;
using Microsoft.JSInterop;
using DCSoft;

namespace System.Resources
{
    internal class ResourceManager : System.ComponentModel.WF2WComponentResourceManager
    {
        public ResourceManager() { }
        public ResourceManager(Type t) : base(t) { }
        public ResourceManager(string baseName, System.Reflection.Assembly asm) : base(baseName, asm) { }
    }
}
namespace System.ComponentModel
{
    /// <summary>
    /// 替换掉系统定义的System.ComponentModel.ComponentResourceManager,这可以避免修改源代码，但会导致编译警告。
    /// </summary>
    internal class ComponentResourceManager : System.ComponentModel.WF2WComponentResourceManager
    {
        public ComponentResourceManager(Type t) : base(t) { }
    }
}
namespace DCSoft.WinForm2WASM
{
    public partial class WF2WEngine
    {
        /// <summary>
        /// 启动 WF2W 引擎
        /// </summary>
        public static ValueTask Start(Microsoft.JSInterop.JSInProcessRuntime rt)
        {
            if (rt == null)
            {
                throw new ArgumentNullException("rt");
            }
            return WF2WPublish.Start(new MyJSRuntime(rt), typeof(WF2WEngine).Assembly);
        }
        private class MyJSRuntime : DCSoft.IDCJSRuntime
        {
            public MyJSRuntime(JSInProcessRuntime rt)
            {
                if (rt == null)
                {
                    throw new ArgumentNullException("rt");
                }
                this._rt = rt;
            }
            private JSInProcessRuntime _rt = null;
            public T Invoke<T>(string identifier, params object?[]? args)
            {
                return _rt.Invoke<T>(identifier, args);
            }
            public ValueTask<T> InvokeAsync<T>(string identifier, params object?[]? args)
            {
                return _rt.InvokeAsync<T>(identifier, args);
            }
            public System.Threading.Tasks.ValueTask InvokeVoidAsync(string identifier, params object?[]? args)
            {
                return _rt.InvokeVoidAsync(identifier, args);
            }
        }
        [JSInvokable]
        public static JsonNode DCExecuteControlCommand(int handle, string strCommand, JsonNode args)
        {
            return WF2WPublish.DCExecuteControlCommand(handle, strCommand, args);
        }
        [JSInvokable]
        public static async ValueTask<JsonNode> DCExecuteControlCommandAsync(int handle, string strCommand, JsonNode args)
        {
            return await WF2WPublish.DCExecuteControlCommandAsync(handle, strCommand, args);
        }
        /// <summary>
        /// 发送消息到控件
        /// </summary>
        [JSInvokable]
        public static async Task SendMessageToControl(int handle, int msg, int wParam, int lParam, JsonNode objWParam, JsonNode objLParam)
        {
            await WF2WPublish.SendMessageToControl(handle, msg, wParam, lParam, objWParam, objLParam);
        }
        [JSInvokable]
        public static int PackageArgumentObjectToHandle(JsonNode json)
        {
            return WF2WPublish.PackageToHandle(json);
        }



        [JSInvokable]
        public static void MarshalReturnStringValue(string str, int ptr)
        {
            WF2WPublish.MarshalReturnStringValue(str, ptr);
        }
        [JSInvokable]
        public static void MarshalFreePtr(int ptr)
        {
            WF2WPublish.MarshalFreePtr(ptr);
        }
        [JSInvokable]
        public static JsonValue MarshalPtrToJsonValue(int ptr)
        {
            return WF2WPublish.MarshalPtrToJsonValue(ptr);
        }
        [JSInvokable]
        public static int MarshalJsonValueToPtr(JsonValue json)
        {
            return WF2WPublish.MarshalJsonValueToPtr(json);
        }
    }
}