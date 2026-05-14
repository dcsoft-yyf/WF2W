namespace DCSoft
{
    using System;
    using System.Windows.Forms;
    using System.Text.Json.Nodes;
    using System.Text;
    using System.Drawing;
    using System.Threading.Tasks;

    /// <summary>
    /// 窗体消息管理器
    /// </summary>
    public static class DCMessageManager
    {
        static DCMessageManager()
        {
            System.Windows.Forms.WinFormUnsafeNativeMethods.GetKeyState = delegate (int vkey)
            {
                return _VKeyStates.TryGetValue(vkey, out var s) ? s : (short)0;
            };
        }
        /// <summary>
        /// 这里特别处理鼠标键盘消息，计算出keyState，为了将来模拟win32 API GetKeyState()做准备。
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        private static void PrepareKeyState(int msg, int wParam, int lParam)
        {
            // Keep last snapshot and update based on current message
            const short DOWN = unchecked((short)0x8000);
            short state = _LastKeyState;

            try
            {
                // helper to set per-key state with toggle low bit
                static short UpdateKey(short prev, bool isDown)
                {
                    short result = prev;
                    bool wasDown = (prev & DOWN) != 0;
                    if (isDown)
                    {
                        result |= DOWN;
                        // set toggle (low bit) on transitions
                        if (!wasDown)
                            result ^= 0x0001;
                    }
                    else
                    {
                        result &= unchecked((short)~DOWN);
                        if (wasDown)
                            result ^= 0x0001;
                    }
                    return result;
                }

                switch (msg)
                {
                    // Keyboard: set/unset modifier and key flags based on wParam (virtual-key)
                    case System.Windows.Forms.WinFormNativeMethods.WM_SYSKEYDOWN:
                    case System.Windows.Forms.WinFormNativeMethods.WM_KEYDOWN:
                        {
                            int vk = wParam;
                            // Update specific key state
                            short prev = _VKeyStates.TryGetValue(vk, out var s) ? s : (short)0;
                            _VKeyStates[vk] = UpdateKey(prev, true);

                            // Update common modifiers for convenience
                            if (vk == (int)System.Windows.Forms.Keys.ShiftKey)
                                _VKeyStates[(int)System.Windows.Forms.Keys.ShiftKey] = UpdateKey(_VKeyStates.GetValueOrDefault((int)System.Windows.Forms.Keys.ShiftKey), true);
                            else if (vk == (int)System.Windows.Forms.Keys.ControlKey)
                                _VKeyStates[(int)System.Windows.Forms.Keys.ControlKey] = UpdateKey(_VKeyStates.GetValueOrDefault((int)System.Windows.Forms.Keys.ControlKey), true);
                            else if (vk == (int)System.Windows.Forms.Keys.Menu)
                                _VKeyStates[(int)System.Windows.Forms.Keys.Menu] = UpdateKey(_VKeyStates.GetValueOrDefault((int)System.Windows.Forms.Keys.Menu), true);

                            state |= DOWN; // aggregate
                            break;
                        }
                    case System.Windows.Forms.WinFormNativeMethods.WM_SYSKEYUP:
                    case System.Windows.Forms.WinFormNativeMethods.WM_KEYUP:
                        {
                            int vk = wParam;
                            short prev = _VKeyStates.TryGetValue(vk, out var s) ? s : (short)0;
                            _VKeyStates[vk] = UpdateKey(prev, false);

                            if (vk == (int)System.Windows.Forms.Keys.ShiftKey)
                                _VKeyStates[(int)System.Windows.Forms.Keys.ShiftKey] = UpdateKey(_VKeyStates.GetValueOrDefault((int)System.Windows.Forms.Keys.ShiftKey), false);
                            else if (vk == (int)System.Windows.Forms.Keys.ControlKey)
                                _VKeyStates[(int)System.Windows.Forms.Keys.ControlKey] = UpdateKey(_VKeyStates.GetValueOrDefault((int)System.Windows.Forms.Keys.ControlKey), false);
                            else if (vk == (int)System.Windows.Forms.Keys.Menu)
                                _VKeyStates[(int)System.Windows.Forms.Keys.Menu] = UpdateKey(_VKeyStates.GetValueOrDefault((int)System.Windows.Forms.Keys.Menu), false);

                            state &= unchecked((short)~DOWN);
                            break;
                        }
                    // Character input doesn't change modifiers; keep state
                    case System.Windows.Forms.WinFormNativeMethods.WM_CHAR:
                        break;

                    // Mouse buttons down/up
                    case System.Windows.Forms.WinFormNativeMethods.WM_LBUTTONDOWN:
                    case System.Windows.Forms.WinFormNativeMethods.WM_RBUTTONDOWN:
                    case System.Windows.Forms.WinFormNativeMethods.WM_MBUTTONDOWN:
                    case System.Windows.Forms.WinFormNativeMethods.WM_XBUTTONDOWN:
                        {
                            int vk = msg switch
                            {
                                System.Windows.Forms.WinFormNativeMethods.WM_LBUTTONDOWN => (int)System.Windows.Forms.Keys.LButton,
                                System.Windows.Forms.WinFormNativeMethods.WM_RBUTTONDOWN => (int)System.Windows.Forms.Keys.RButton,
                                System.Windows.Forms.WinFormNativeMethods.WM_MBUTTONDOWN => (int)System.Windows.Forms.Keys.MButton,
                                _ => (int)System.Windows.Forms.Keys.XButton1
                            };
                            _VKeyStates[vk] = UpdateKey(_VKeyStates.GetValueOrDefault(vk), true);
                            state |= DOWN;
                        }
                        break;
                    case System.Windows.Forms.WinFormNativeMethods.WM_LBUTTONUP:
                    case System.Windows.Forms.WinFormNativeMethods.WM_RBUTTONUP:
                    case System.Windows.Forms.WinFormNativeMethods.WM_MBUTTONUP:
                    case System.Windows.Forms.WinFormNativeMethods.WM_XBUTTONUP:
                        {
                            int vk = msg switch
                            {
                                System.Windows.Forms.WinFormNativeMethods.WM_LBUTTONUP => (int)System.Windows.Forms.Keys.LButton,
                                System.Windows.Forms.WinFormNativeMethods.WM_RBUTTONUP => (int)System.Windows.Forms.Keys.RButton,
                                System.Windows.Forms.WinFormNativeMethods.WM_MBUTTONUP => (int)System.Windows.Forms.Keys.MButton,
                                _ => (int)System.Windows.Forms.Keys.XButton1
                            };
                            _VKeyStates[vk] = UpdateKey(_VKeyStates.GetValueOrDefault(vk), false);
                            state &= unchecked((short)~DOWN);
                        }
                        break;

                    case System.Windows.Forms.WinFormNativeMethods.WM_MOUSEMOVE:
                    case System.Windows.Forms.WinFormNativeMethods.WM_MOUSEWHEEL:
                        {
                            // Derive modifier keys from wParam MK_* flags if available
                            int mk = wParam & 0xFFFF;
                            bool hasShift = (mk & System.Windows.Forms.WinFormNativeMethods.MK_SHIFT) != 0;
                            bool hasCtrl = (mk & System.Windows.Forms.WinFormNativeMethods.MK_CONTROL) != 0;
                            // Update modifier virtual keys
                            _VKeyStates[(int)System.Windows.Forms.Keys.ShiftKey] = UpdateKey(_VKeyStates.GetValueOrDefault((int)System.Windows.Forms.Keys.ShiftKey), hasShift);
                            _VKeyStates[(int)System.Windows.Forms.Keys.ControlKey] = UpdateKey(_VKeyStates.GetValueOrDefault((int)System.Windows.Forms.Keys.ControlKey), hasCtrl);
                            // Update down flag generically if any button/modifier is pressed
                            state = (short)((hasShift || hasCtrl || mk != 0) ? DOWN : 0);
                            break;
                        }
                    default:
                        break;
                }
            }
            catch { /* keep last state on error */ }

            _LastKeyState = state;
        }

        private static short _LastKeyState = 0;
        // Per-virtual-key state store for GetKeyState emulation
        private static readonly Dictionary<int, short> _VKeyStates = new Dictionary<int, short>();

        // Optional: expose internal query if needed in future
        private static short QueryKeyState(int vkey)
        {
            return _VKeyStates.TryGetValue(vkey, out var s) ? s : (short)0;
        }
        /// <summary>
        /// 发送消息到控件
        /// </summary>
        /// <param name="handle">控件句柄</param>
        /// <param name="msg">消息类型</param>
        /// <param name="wParam">参数1</param>
        /// <param name="lParam">参数2</param>
        public async static Task SendMessageToControl(int handle, int msg, int wParam, int lParam ,JsonNode objWParam , JsonNode objLParam)
        {
            PrepareKeyState(msg, wParam, lParam);
            var ctl = DCWin32API.GetControl(handle) as System.Windows.Forms.Control;
            if (ctl != null)
            {
                var msg2 = new Message();
                msg2.HWnd = new IntPtr(handle);
                msg2.Msg = msg;
                msg2.WParam = wParam;
                msg2.LParam = lParam;
                msg2.ObjectWParam = objWParam;
                msg2.ObjectLParam = objLParam;
                try
                {
                    if (System.Windows.Forms.Application.ThreadContext.FromCurrent().ProcessFilters(ref msg2, out bool modified))
                    {
                        return;
                    }
                    //if (msg == WinFormNativeMethods.WM_WINDOWPOSCHANGED)
                    //{
                    //    ctl.WmWindowPosChanged(msg2);
                    //}
                    //else
                    {
                        await ctl.PublicWnProc(msg2);
                    }
                    if (DCMessageManager.HasPostedMessages())
                    {
                        // 存在已经发送的消息，等待延时处理
                        DCWin32API.JSRuntime.Invoke<object>("__DCHandlePostedMessage", null);
                    }
                }
                catch( System.Exception ext )
                {
                    Console.Error.WriteLine(ext.ToString());
                }
            }
        }



        public static async ValueTask<int> SendMessageAsync(int hWnd, int msg, int wParam, int lParam)
        {
            var ctl = Control.FromHandle(hWnd);
            if (ctl != null)
            {
                var msg2 = new Message();
                msg2.Msg = msg;
                msg2.WParam = wParam;
                msg2.LParam = lParam;
                await ctl.PublicWnProc(msg2);
                return (int)msg2.Result;
            }
            return 0;
        }
        public static int SendMessage(int hWnd, int msg, int wParam, int lParam)
        {
            var ctl = Control.FromHandle(hWnd);
            if (ctl != null)
            {
                var msg2 = new Message();
                msg2.Msg = msg;
                msg2.WParam = wParam;
                msg2.LParam = lParam;
                ctl.PublicWnProc(msg2);
                return (int)msg2.Result;
            }
            return 0;
        }
        public static bool HasPostedMessages()
        {
            return _PostedMessages != null && _PostedMessages.Count > 0;
        }
        /// <summary>
        /// 处理被发送的消息列表
        /// </summary>
        public static void HandlePostedMessage()
        {
            if (_PostedMessages != null && _PostedMessages.Count > 0)
            {
                var arr = _PostedMessages.ToArray();
                _PostedMessages.Clear();
                for (var iCount = 0; iCount < arr.Length; iCount++)
                {
                    var msg = arr[iCount];
                    if (Application.FilterMessage(ref msg))
                    {
                        continue;
                    }
                    var ctl = Control.FromHandle(msg.HWnd);
                    if (ctl != null)
                    {
                        ctl.PublicWnProc(msg);
                    }
                }
                if(_PostedMessages.Count > 0 )
                {
                    // 存在已经发送的消息，等待延时处理
                    DCWin32API.JSRuntime.Invoke<object>("__DCHandlePostedMessage", null);
                }
            }
        }

        private static List<System.Windows.Forms.Message> _PostedMessages = null;
        internal static bool PostMessage(int handle, int msg, int wParam, int lParam , object objLParam = null)
        {
            if(_PostedMessages == null )
            {
                _PostedMessages = new List<Message>();
            }
            else
            {
                const int WM_PAINT = 0x000F;
                // 优化处理，合并连续的WM_PAINT消息
                if( msg == WM_PAINT )
                {
                    for( int iCount = _PostedMessages.Count -1; iCount >=0 ; iCount-- )
                    {
                        var m = _PostedMessages[iCount];
                        if( m.Msg == WM_PAINT && m.HWnd == handle )
                        {
                            // 已经存在WM_PAINT消息，直接返回
                            if (m.ObjectWParam is Rectangle && objLParam is Rectangle)
                            {
                                var rect1 = (Rectangle)m.ObjectWParam;
                                var rect2 = (Rectangle)objLParam;
                                if(rect1.Contains(rect2 ) || rect1 == rect2 )
                                {
                                    return false;
                                }
                            }
                        }
                    }
                }
            }
            var msg3 = new System.Windows.Forms.Message();
            msg3.HWnd = handle;
            msg3.Msg = msg;
            msg3.WParam = wParam;
            msg3.LParam = lParam;
            msg3.ObjectWParam = objLParam;
            _PostedMessages.Add(msg3);
            // 存在已经发送的消息，等待延时处理
            DCWin32API.JSRuntime.Invoke<object>("__DCHandlePostedMessage", null);
            return true;
        }
        public static void SendMessage_WM_WINDOWPOSCHANGED(int handle, JsonObject args)
        {
            var info = new WinFormNativeMethods.WINDOWPOS();
            if (args != null)
            {
                foreach (var item in args)
                {
                    switch (item.Key)
                    {
                        case "Left":
                            info.x = DCValueConvert.ConvertToInt32(item.Value, 0);
                            break;
                        case "Top":
                            info.y = DCValueConvert.ConvertToInt32(item.Value, 0);
                            break;
                        case "Width":
                            info.cx = DCValueConvert.ConvertToInt32(item.Value, 0);
                            break;
                        case "Height":
                            info.cy = DCValueConvert.ConvertToInt32(item.Value, 0);
                            break;
                        case "Flags":
                            info.flags = DCValueConvert.ConvertToInt32(item.Value, 0);
                            break;
                        case "ZIndex":
                            info.hwndInsertAfter = DCValueConvert.ConvertToInt32(item.Value, 0);
                            break;
                        case "Handle":
                            info.hwnd = DCValueConvert.ConvertToInt32(item.Value, 0);
                            break;
                    }
                }
            }
            var msg = new System.Windows.Forms.Message();
            msg.HWnd = handle;
            msg.Msg = System.Windows.Forms.WinFormNativeMethods.WM_WINDOWPOSCHANGED;
            msg.WParam = 0;
            msg.LParam = DCWin32API.PackageToHandle(info);
            //msg.ObjectLParam = info;
            DCWin32API.SendMessageToControl2(msg);
            DCWin32API.GetPackagedObjectByHandleOnce(msg.LParam.ToInt32());
        }
    }
}
