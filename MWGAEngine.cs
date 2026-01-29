

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
    internal class ResourceManager : System.ComponentModel.MWGAComponentResourceManager
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
    internal class ComponentResourceManager : System.ComponentModel.MWGAComponentResourceManager
    {
        public ComponentResourceManager(Type t) : base(t) { }
    }
}
namespace DCSoft.WinForm2WASM
{
    public partial class MWGAEngine
    {
        /// <summary>
        /// 启动 MWGA 引擎
        /// </summary>
        public static ValueTask Start(Microsoft.JSInterop.JSInProcessRuntime rt)
        {
            if (rt == null)
            {
                throw new ArgumentNullException("rt");
            }
            return MWGAPublish.Start(new MyJSRuntime(rt), typeof(MWGAEngine).Assembly);
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
            return MWGAPublish.DCExecuteControlCommand(handle, strCommand, args);
        }
        [JSInvokable]
        public static async ValueTask<JsonNode> DCExecuteControlCommandAsync(int handle, string strCommand, JsonNode args)
        {
            return await MWGAPublish.DCExecuteControlCommandAsync(handle, strCommand, args);
        }
        /// <summary>
        /// 发送消息到控件
        /// </summary>
        [JSInvokable]
        public static async Task SendMessageToControl(int handle, int msg, int wParam, int lParam, JsonNode objWParam, JsonNode objLParam)
        {
            await MWGAPublish.SendMessageToControl(handle, msg, wParam, lParam, objWParam, objLParam);
        }
        [JSInvokable]
        public static int PackageArgumentObjectToHandle(JsonNode json)
        {
            return MWGAPublish.PackageToHandle(json);
        }

        //[JSInvokable]
        //public static void SendMessage_WM_WINDOWPOSCHANGED( int handle , JsonObject args )
        //{
        //    MWGAPublish.SendMessage_WM_WINDOWPOSCHANGED(handle, args);
        //}



        [JSInvokable]
        public static void MarshalReturnStringValue(string str, int ptr)
        {
            MWGAPublish.MarshalReturnStringValue(str, ptr);
        }
        [JSInvokable]
        public static void MarshalFreePtr(int ptr)
        {
            MWGAPublish.MarshalFreePtr(ptr);
        }
        [JSInvokable]
        public static JsonValue MarshalPtrToJsonValue(int ptr)
        {
            return MWGAPublish.MarshalPtrToJsonValue(ptr);
        }
        [JSInvokable]
        public static int MarshalJsonValueToPtr(JsonValue json)
        {
            return MWGAPublish.MarshalJsonValueToPtr(json);
        }


        ///// <summary>
        ///// 处理已经发送的消息
        ///// </summary>
        //[JSInvokable]
        //public static void HandlePostedMessage()
        //{
        //    MWGAPublish.HandlePostedMessage();
        //}
    }
}