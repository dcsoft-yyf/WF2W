using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json.Nodes;
using System.Text.Json;

namespace DCSoft
{
    /// <summary>
    /// WinForm2WASM 发布的静态方法
    /// </summary>
    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true )]
    public static class WF2WPublish
    {
        public static void MarshalReturnStringValue(string str, int ptr)
        {
            DCWin32API.MarshalReturnStringValue(str, ptr);
        }

        public static void MarshalFreePtr(int ptr)
        {
            DCWin32API.MarshalFreePtr(ptr);
        }
        public static JsonValue MarshalPtrToJsonValue(int ptr)
        {
            return DCWin32API.MarshalPtrToJsonValue(ptr);
        }
        public static int MarshalJsonValueToPtr(JsonValue json)
        {
            object v = null;
            if (json == null)
            {
                return -1;
            }
            var vk = json.GetValueKind();
            switch(vk )
            {
                case JsonValueKind.Null:
                    v = null;
                    break;
                case JsonValueKind.String:
                    v = json.GetValue<string>();
                    break;
                case JsonValueKind.Number:
                    v = json.GetValue<double>();
                    break;
                case JsonValueKind.True:
                    v = true;
                    break;
                case JsonValueKind.False:
                    v = false;
                    break;
                case JsonValueKind.Object:
                    v = json.AsObject();
                    break;
                case JsonValueKind.Array:
                    v = json.AsArray();
                    break;
            }
            return DCWin32API.MarshalObjectToPtr(v);
        }

        public static int SendDataFromJS(JsonObject json)
        {
            var strDataName = json.TryGetPropertyValue("DataName", out var v1) ? Convert.ToString(v1) : null;
            switch (strDataName)
            {
                case "SetScreenSize":
                    {
                        var width = json.TryGetPropertyValue("Width", out var v2) ? DCValueConvert.ConvertToInt32(v2, 800) : 800;
                        var height = json.TryGetPropertyValue("Height", out var v3) ? DCValueConvert.ConvertToInt32(v3, 600) : 600;
                        var defaultFontName = json.TryGetPropertyValue("DefaultFontName", out var v4) ? Convert.ToString(v4) : "Arial";
                        var defaultFontSize = json.TryGetPropertyValue("DefaultFontSize", out var v5) ? DCValueConvert.ConvertToSingle(v5, 9f) : 9f;
                        DCWin32API.SetScreenSize(width, height, defaultFontName, defaultFontSize);
                    }
                    break;
                case "AddStandardControlTypeName":
                    {
                        var typeName = json.TryGetPropertyValue("TypeName", out var v7) ? Convert.ToString(v7) : null;
                        if (typeName != null)
                        {
                            DCWin32API.AddStandardControlTypeName(typeName);
                        }
                    }
                    break;
                case "HandlePostedMessage":
                    {
                        DCMessageManager.HandlePostedMessage();
                    }
                    break;
                case "MousePosition":
                    {
                        var x = json.TryGetPropertyValue("X", out var v8) ? DCValueConvert.ConvertToInt32(v8, 0) : 0;
                        var y = json.TryGetPropertyValue("Y", out var v9) ? DCValueConvert.ConvertToInt32(v9, 0) : 0;
                        System.Windows.Forms.Control._MousePosition = new System.Drawing.Point(x, y);
                    }
                    break;
                case "TimerTick":
                    {
                        var timerId = json.TryGetPropertyValue("ID", out var v10) ? DCValueConvert.ConvertToInt32(v10, 0) : 0;
                        if (timerId > 0)
                        {
                            System.Windows.Forms.Timer.RaiseTick(timerId);
                        }
                    }
                    break;
                
            }
            return 0;
        }
        //[System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public static async ValueTask<JsonNode> DCExecuteControlCommandAsync(int handle, string strCommand, JsonNode args)
        {
            return await DCExecuteControlCommandHelper.DCExecuteControlCommandAsync(handle, strCommand, args);
        }

        public static JsonNode DCExecuteControlCommand(int handle, string strCommand, JsonNode args)
        {
            return DCExecuteControlCommandHelper.DCExecuteControlCommand(handle, strCommand, args);
        }
        //public static void HandlePostedMessage()
        //{
        //    DCMessageManager.HandlePostedMessage();
        //}
        public static async Task SendMessageToControl(int handle, int msg, int wParam, int lParam,JsonNode objWParam, JsonNode objLParam)
        {
            await DCMessageManager.SendMessageToControl(handle, msg, wParam, lParam , objWParam , objLParam);
        }
        public static void SendMessage_WM_WINDOWPOSCHANGED(int handle, JsonObject args)
        {
            DCMessageManager.SendMessage_WM_WINDOWPOSCHANGED(handle, args);
        }
        public static int PackageToHandle(object obj)
        {
            return DCWin32API.PackageToHandle(obj);
        }
        //public static void SetScreenSize(
        //    int width,
        //    int height,
        //    string defaultFontName,
        //    float defaultFontSize)
        //{
        //    DCWin32API.SetScreenSize( width, height, defaultFontName, defaultFontSize);
        //}
        //public static void SetFamilies(string[] names)
        //{
        //    System.Drawing.FontFamily.InnerSetFamilies(names);
        //}
        //public static void AddStandardControlTypeName(string typeName)
        //{
        //    DCWin32API.AddStandardControlTypeName( typeName);
        //}
        public static ValueTask Start(IDCJSRuntime rt, System.Reflection.Assembly entryPointAssembly)
        {
            return DCWin32API.Start( rt , entryPointAssembly );
        }
    }
}
