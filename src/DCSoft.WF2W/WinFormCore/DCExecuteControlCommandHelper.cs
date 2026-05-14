using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Nodes;
using System.Windows.Forms;

namespace DCSoft
{
    internal static class DCExecuteControlCommandHelper
    {
        //[System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public static async ValueTask<JsonNode> DCExecuteControlCommandAsync(int handle, string strCommand, JsonNode args)
        {
            try
            {
                switch (strCommand)
                {
                    case "RunBeginInvoke":
                        System.Windows.Forms.Control.RunBeginInvoke(handle);
                        break;
                    case "InvokeApplicationAsyncMethod":
                        await Application.InvokeAsyncInternal();
                        break;
                    case "RaiseEventScrollAsync":
                        {
                            var ctl = DCWin32API.GetControl(handle) as ScrollableControl;
                            if (ctl != null)
                            {
                                await ctl.RaiseEventScrollAsync();
                            }
                        }
                        break;
                    case "GetDropdownItems":
                        {
                            var uiObj = DCWin32API.GetControl(handle);
                            if (uiObj is System.Windows.Forms.MenuItem menu)
                            {
                                return await MenuItem.GetChildMenuItems(handle);
                            }
                            else if (uiObj is ToolStripDropDownItem dropDownItem)
                            {
                                var items = await dropDownItem.GetDropDownItemsAsync();
                                if (items != null && items.Count > 0)
                                {
                                    var jsonArr = new JsonArray();
                                    foreach (ToolStripItem item in items)
                                    {
                                        if (item.Available)
                                        {
                                            item.CheckCreateHandle();
                                            var itemJson = new JsonObject();
                                            DCWinFormHelper.WritePropertyValueToJson(item, itemJson , false);
                                            jsonArr.Add(itemJson);
                                        }
                                    }
                                    return jsonArr;
                                }
                            }
                            else if (uiObj is ToolStripComboBox comboBox)
                            {
                                await comboBox.OnDropDownAsync(EventArgs.Empty);
                                var items = comboBox.Items;
                                if (items != null && items.Count > 0)
                                {
                                    var jsonArr = new JsonArray();
                                    foreach (var item in items)
                                    {
                                        var itemJson = new JsonObject();
                                        itemJson["Text"] = comboBox.ComboBox.GetItemText(item);
                                        jsonArr.Add(itemJson);
                                    }
                                    return jsonArr;
                                }
                            }
                            else if( uiObj is System.Windows.Forms.ComboBox cbo)
                            {
                                await cbo._RaiseDropDownEvent();
                                var items = cbo.Items;
                                if (items != null && items.Count > 0)
                                {
                                    var jsonArr = new JsonArray();
                                    foreach (var item in items)
                                    {
                                        var itemJson = new JsonObject();
                                        itemJson["Text"] = cbo.GetItemText(item);
                                        jsonArr.Add(itemJson);
                                    }
                                    return jsonArr;
                                }
                            }
                        }
                        break;
                    
                    case "OnClick":
                        {
                            var ctl = DCWin32API.GetControl(handle);
                            if (ctl is ToolStripItem item2)
                            {
                                await item2.HandleClickAsync(EventArgs.Empty);
                            }
                            else if (ctl is MenuItem mui)
                            {
                                await mui.OnClickAsync(EventArgs.Empty);
                                //Delegate deleg = mui.OnClickAsync;
                                //var result2 = (Task) deleg.DynamicInvoke(EventArgs.Empty);// mui.OnClickAsync(EventArgs.Empty);
                                //await result2;
                                var s = 1;
                            }
                        }
                        break;

                }
                return null;
            }
            catch( Exception ext )
            {
                Console.WriteLine(ext.ToString());
                throw ext;
            }
        }

        public static JsonNode DCExecuteControlCommand(int handle, string strCommand, JsonNode args)
        {
            switch (strCommand)
            {
                case "GetAllLoggedPropertiesHasHandle": return PropertyValueLogger.GetAllLoggedPropertiesHasHandle();
                case "SetApplicationData":
                    {
                        if (args is JsonObject obj)
                        {
                            foreach (var item2 in obj)
                            {
                                switch (item2.Key)
                                {
                                    case "StartupPath":
                                        Application._StartupPath = DCValueConvert.ConvertToString(item2.Value);
                                        break;
                                    case "ExecutablePath":
                                        Application._ExecutablePath = DCValueConvert.ConvertToString(item2.Value);
                                        break;
                                    case "ProductName":
                                        Application._ProductName = DCValueConvert.ConvertToString(item2.Value);
                                        break;
                                }
                            }
                        }
                    }
                    break;
                case "SetScrollPosition":
                    {
                        var ctl = DCWin32API.GetControl(handle) as ScrollableControl;
                        if (ctl != null && args is JsonObject jsonObj)
                        {
                            var rect = jsonObj.ConvertToRectangle();
                            ctl.SetDisplayRectangleDirect(
                                rect.Left,
                                rect.Top,
                                rect.Width,
                                rect.Height);
                            return ctl.HasScrollAsync();
                        }
                        return false;
                    }
                    break;
                case "Invalidate":
                    {
                        var ctl = Control.FromHandleInternal(handle);
                        if (ctl != null)
                        {
                            var rect = args as JsonObject;
                            if (args is JsonObject jsonObj )
                            {
                                var rect5 = jsonObj.ConvertToRectangle();
                                ctl.Invalidate(rect5);
                            }
                            else
                            {
                                ctl.Invalidate();
                            }
                        }
                    }
                    break;
                case "RedrawWindow":
                    {
                        var ctl = Control.FromHandleInternal(handle);
                        if (ctl != null)
                        {
                            var rect = args as JsonObject;
                            if (args is JsonObject jsonObj)
                            {
                                var rect5 = jsonObj.ConvertToRectangle();
                                
                                ctl.WF2WRedraw(rect5);
                            }
                            else
                            {
                                ctl.WF2WRedraw( new System.Drawing.Rectangle(
                                    0 , 
                                    0 , 
                                    ctl.ClientSize.Width , 
                                    ctl.ClientSize.Height ));
                            }
                        }
                    }
                    break;
                //case "GetChildMenuItems": 
                //    return MenuItem.GetChildMenuItems(handle);
                //case "MenuItemClick":
                //    return MenuItem.RaiseClick(handle);
                case "SetScreenSize":
                    {
                        var json = (JsonObject)args;
                        var scw = json.ConvertToSize();
                        var defaultFontName = json.TryGetPropertyValue("DefaultFontName", out var v4) ? Convert.ToString(v4) : "Arial";
                        var defaultFontSize = json.TryGetPropertyValue("DefaultFontSize", out var v5) ? DCValueConvert.ConvertToSingle(v5, 9f) : 9f;
                        DCWin32API.SetScreenSize(scw.Width, scw.Height , defaultFontName, defaultFontSize);
                    }
                    break;
                //case "InstallFontNames":
                //    {
                //        var fontNames = args as JsonArray;
                //        if(fontNames != null)
                //        {
                //            var names = new List<string>();
                //            foreach (var item in fontNames)
                //            {
                //                var name = DCValueConvert.ConvertToString(item);
                //                if (name != null && name.Length > 0 )
                //                {
                //                    names.Add(name);
                //                }
                //            }
                //            System.Drawing.FontFamily.InnerSetFamilies(names.ToArray());
                //        }
                //    }
                //    break;
                case "AddStandardControlTypeName":
                    {
                        var strTypeName = DCValueConvert.ConvertToString(args);
                        if (strTypeName != null && strTypeName.Length > 0)
                        {
                            DCWin32API.AddStandardControlTypeName(strTypeName);
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
                        var json = (JsonObject)args;
                        var p9 = json.ConvertToPoint();
                        System.Windows.Forms.Control._MousePosition = p9;
                    }
                    break;
                case "TimerTick":
                    {
                        System.Windows.Forms.Timer.RaiseTick(handle);
                    }
                    break;
                case "WM_WINDOWPOSCHANGED":
                    DCMessageManager.SendMessage_WM_WINDOWPOSCHANGED(handle, (JsonObject)args);
                    break;
                case "SendMessageDirect":
                    {
                        var json = (JsonObject)args;
                        JsonNode jsonP = null;
                        var msg = new Message();
                        msg.HWnd = handle;
                        msg.Msg = json.TryGetPropertyValue("Msg", out jsonP) ? DCValueConvert.ConvertToInt32(jsonP, 0) : 0;
                        msg.WParam = json.TryGetPropertyValue("WParam", out jsonP) ? DCValueConvert.ConvertToInt32(jsonP, 0) : 0;
                        msg.LParam = json.TryGetPropertyValue("LParam", out jsonP) ? DCValueConvert.ConvertToInt32(jsonP, 0) : 0;
                        var ctl = DCWin32API.GetControl(msg.HWnd.ToInt32()) as System.Windows.Forms.Control;
                        if (ctl != null)
                        {
                            if (msg.Msg == WinFormNativeMethods.WM_WINDOWPOSCHANGED)
                            {
                                ctl.Handle_WM_WINDOWPOSCHANGED(default(Message));
                            }
                            else
                            {
                                var s = 1;
                            }
                        }
                        //DCMessageManager.SendMessage_WM_WINDOWPOSCHANGED(handle,(JsonObject)args);
                    }
                    break;
            }
            return null;
        }
        //public static void HandlePostedMessage()

    }
}
