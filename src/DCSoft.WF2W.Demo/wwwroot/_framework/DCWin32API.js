"use strict";
var __DCWin32API = null;
(function () {
    // Blob URL cache by MD5
    var __BlobUrlCache = new Map();
    var __TimerList = new Map();

    window.__DCWin32API = __DCWin32API = {
        BeginInvoke: async function (intID) {

        },
        GetClientSize: function (handleOrElement) {
            var ele = DCGetControlByHandle(handleOrElement);
            if (ele != null && ele.__DCFactory != null) {
                return ele.__DCFactory.GetClientSize(ele);
            }
            return null;
        },
        GetDisplayRectangle: function (handleOrElement) {
            var ele = DCGetControlByHandle(handleOrElement);
            if (ele != null && ele.__DCFactory != null && ele.__DCFactory.GetScrollPosition) {
                return ele.__DCFactory.GetDisplayRectangle(ele);
            }
            return null;
        },
        InvokeControlMethod: function (handleOrElement, strMethodName, args) {
            var ele = DCGetControlByHandle(handleOrElement);
            if (ele != null) {
                var fact = ele.__DCFactory;
                if (fact != null) {
                    var method = fact[strMethodName];
                    if (typeof (method) === "function") {
                        var result = method.call(fact, ele, args);
                        return result;
                    }
                }
            }
            return null;
        },
        UpdatePropertyValues: function () {
            var task = {
                Name: "UpdatePropertyValues20260205",
                Eat: function (item) {
                    return item.Name == this.Name;
                },
                Run: function () {
                    var jsonProperties = __DCExecuteControlCommand(0, "GetAllLoggedPropertiesHasHandle", null);
                    if (jsonProperties != null) {
                        try {
                            for (var intHandle in jsonProperties) {
                                var ele = DCGetControlByHandle(parseInt(intHandle));
                                if (ele != null) {
                                    var fact = ele.__DCFactory;
                                    if (fact != null) {
                                        var props = jsonProperties[intHandle];
                                        if (props != null) {
                                            fact.ApplyOptions.call(fact, ele, props);
                                        }
                                    }
                                }
                                else {
                                    var s = 1;
                                }
                            }
                        }
                        catch (ext) {
                            console.warn(ext);
                            throw ext;
                        }
                    }
                }
            };
            AddTask(task);
        },
        InvokeApplicationAsyncMethod: async function () {
            await __DCExecuteControlCommandAsync(0, "InvokeApplicationAsyncMethod", null);
        },
        SetScrollAutoMinSize: function (handleOrElement, intWidth, intHeight) {
            var ele = DCGetControlByHandle(handleOrElement);
            if (ele == null) return false;
            var host = GetChildContainer(ele);
            if (host != null) {
                function ensureScrollSpacer(host) {
                    if (!host.__scrollSpacer) {
                        var spacer = document.createElement('div');
                        spacer.className = 'dc-scroll-spacer';
                        spacer.style.pointerEvents = 'none';
                        spacer.style.visibility = 'hidden';
                        spacer.style.position = 'relative';
                        spacer.style.width = '1px';
                        spacer.style.height = '1px';
                        host.appendChild(spacer);
                        host.__scrollSpacer = spacer;
                    }
                    return host.__scrollSpacer;
                }
                var spacer = ensureScrollSpacer(host);
                if (spacer != null) {
                    spacer.style.width = intWidth + "px";
                    spacer.style.height = intHeight + "px";
                    return true;
                }
            }
            return false;
        },
        SetScrollPosition: function (handleOrElement, intX, intY) {
            var ele = DCGetControlByHandle(handleOrElement);
            if (ele != null && ele.__DCFactory != null && ele.__DCFactory.SetScrollPosition) {
                return ele.__DCFactory.SetScrollPosition.call(ele.__DCFactory, ele);
            }
            return false;
        },
        SetToolTip: function (handleOrElement, strTitle, strText) {
            var ele = DCGetControlByHandle(handleOrElement);
            if (ele != null) {
                if (strText == null || strText == "null" || strText == "undefined") {
                    ele.title = "";
                }
                else {
                    ele.title = strText;
                }
            }
        },
        IsChild: function (intParent, intChild) {
            var eleParent = DCGetControlByHandle(intParent);
            var eleChild = DCGetControlByHandle(intChild);
            if (eleParent != null && eleChild != null && eleParent != eleChild) {
                for (var e = eleChild; e != null; e = e.parentNode) {
                    if (e == eleParent) {
                        return true;
                    }
                }
            }
            return false;
        },
        PostMessage_WM_SIZE: function (handleOrElement, intNewWidth, intNewHeight) {
            var lParam = ((intNewHeight & 0xFFFF) << 16) | (intNewWidth & 0xFFFF);
            //var WM_SIZE = 0x0005;
            PostMessageToControl(handleOrElement, 0x0005, 0, lParam);
        },
        /**
         * 当图片地址是blob:http的，则图片加载完毕后销毁这个地址释放内存
         * @param {HTMLImageElement} imgElement 图片对象
         */
        CheckForRealseBlobImageUrl: function (imgElement) {
            if (imgElement != null) {
                var src = imgElement.src;
                if (src != null
                    && src.length > 10
                    && src.substring(0, 9).toLowerCase() == "blob:http") {
                    imgElement.onload = function () {
                        URL.revokeObjectURL(this.src);
                    };
                }
            }
        },
        TimerStart: function (intTimerId, intInterval) {
            function FuncTimer(intTimerID, intInterval) {
                __TimerList.delete(intTimerID);
                var result = __DCExecuteControlCommand(
                    intTimerID,
                    "TimerTick");
            }
            var timerHandle = __TimerList.get(intTimerId);
            if (typeof (timerHandle) == "number") {
                __TimerList.delete(intTimerId);
                window.clearTimeout(timerHandle);
            }
            timerHandle = window.setTimeout(FuncTimer, intInterval, intTimerId, intInterval);
            __TimerList.set(intTimerId, timerHandle);
        },
        TimerStop: function (intTimerId) {
            var timerHandle = __TimerList.get(intTimerId);
            if (typeof (timerHandle) == "number") {
                __TimerList.delete(intTimerId);
                window.clearTimeout(timerHandle);
            }
        },
        GetCachedBlobUrl: function (strMD5) {
            var cached = __BlobUrlCache.get(strMD5);
            return cached || null;
        },
        /** 根据二进制数据创建一个URL内部地址
            * @param {Uint8Array} arrayBuffer - 二进制数据
            * @param {string} strMime - 数据类型(MIME类型)
            * @param  {string} strMD5 - 数据的MD5值（可选）,用于缓存识别，函数内部有一个缓存机制，相同MD5值的数据只会创建一次URL地址
            * @param {boolean} bolAllowCached - 是否允许使用缓存机制
            * @returns {string} 返回创建的URL地址
            */
        CreateBlobUrl: function (arrayBuffer, strMime, strMD5, bolAllowCached) {
            if (bolAllowCached == true && typeof strMD5 === 'string' && strMD5.length > 0) {
                var cached = __BlobUrlCache.get(strMD5);
                if (cached) return cached;
            }

            if (!arrayBuffer) return null;

            var mime = (typeof strMime === 'string' && strMime.length > 0) ? strMime : 'application/octet-stream';
            var uint8;
            if (arrayBuffer instanceof ArrayBuffer) {
                uint8 = new Uint8Array(arrayBuffer);
            } else if (ArrayBuffer.isView && ArrayBuffer.isView(arrayBuffer)) {
                // TypedArray/DataView
                uint8 = new Uint8Array(arrayBuffer.buffer, arrayBuffer.byteOffset || 0, arrayBuffer.byteLength || arrayBuffer.length || 0);
            } else if (Array.isArray(arrayBuffer)) {
                uint8 = new Uint8Array(arrayBuffer);
            } else if (arrayBuffer && typeof arrayBuffer.length === 'number') {
                // assume array-like
                uint8 = new Uint8Array(arrayBuffer);
            } else {
                return null;
            }

            var blob = new Blob([uint8], { type: mime });
            var url = (window.URL || window.webkitURL).createObjectURL(blob);
            if (bolAllowCached == true) {
                if (typeof strMD5 === 'string' && strMD5.length > 0) {
                    __BlobUrlCache.set(strMD5, url);
                }
            }
            return url;
        },
        MarshalReturnStringValue: function (strValue, intPtr) {
            __DCInvokeCSMethod("MarshalReturnStringValue", strValue, intPtr);
            return 0;
        },
        //MarshalJsonValueToPtr : function (json) {
        //    return __DCInvokeCSMethod("MarshalJsonValueToPtr", json);
        //},
        //MarshalPtrToJsonValue : function (intIndex) {
        //    return __DCInvokeCSMethod("MarshalPtrToJsonValue", intIndex);
        //},
        //MarshalFreePtr : function (intIndex) {
        //    __DCInvokeCSMethod("MarshalFreePtr", intIndex);
        //},
        /** 执行控件的默认窗口过程  */
        DefWindowProc: function (handleOrElement, intMsg, intWParam, intLParam) {
            var element = DCGetControlByHandle(handleOrElement);
            if (element != null) {
                var strDCType = element.getAttribute("dctype");
                var factory = window.__DCControlTypes[strDCType];
                if (factory == null) {
                    var strBaseType = element.getAttribute("dcbasecontroltypename");
                    if (strBaseType != null && strBaseType.length > 0) {
                        factory = window.__DCControlTypes[strBaseType];
                    }
                }
                if (factory != null && typeof factory.DefWindowProc === "function") {
                    return factory.DefWindowProc.call(element, element, intMsg, intWParam, intLParam);
                }
            }
            return 0;
        },
        /** 模拟win32API GetScrollInfo
         * @param{number} handleOrElement
         * @param{int} fnBar
         * @returns {object} 一个JSON对象，模拟WIN32结构体SCROLLINFO 
         */
        GetScrollInfo: function (handleOrElement, fnBar) {
            var element = DCGetControlByHandle(handleOrElement);
            if (!element) return 0;

            var container = GetChildContainer(element) || element;
            var barKey = (fnBar === 0) ? "horz" : "vert"; // SB_HORZ=0, SB_VERT=1

            var SIF_ALL = 0x0017;

            // Prefer stored info populated by SetScrollInfo
            var infoStore = (container.__scrollInfo && container.__scrollInfo[barKey]) || null;
            var nMin, nMax, nPage, nPos, nTrackPos;

            if (infoStore) {
                nMin = infoStore.nMin | 0;
                nMax = infoStore.nMax | 0;
                nPage = infoStore.nPage | 0;
                nPos = infoStore.nPos | 0;
                nTrackPos = infoStore.nTrackPos | 0;
            } else {
                // Derive from DOM dimensions if no stored info
                if (barKey === "vert") {
                    nMin = 0;
                    nMax = Math.max(0, (container.scrollHeight | 0) - 1);
                    nPage = container.clientHeight | 0;
                    nPos = container.scrollTop | 0;
                } else {
                    nMin = 0;
                    nMax = Math.max(0, (container.scrollWidth | 0) - 1);
                    nPage = container.clientWidth | 0;
                    nPos = container.scrollLeft | 0;
                }
                nTrackPos = nPos;
            }

            return {
                cbSize: 0,
                fMask: SIF_ALL,
                nMin: nMin,
                nMax: nMax,
                nPage: nPage,
                nPos: nPos,
                nTrackPos: nTrackPos
            };
        },
        /**
         * 模拟win32 API SetScrollInfo
         * @param {number} handleOrElement
         * @param {int} fnBar
         * @param {any} si 模拟WIN32结构体 SCROLLINFO
         * @param {any} redraw
         * @returns {number} 返回值
         */
        SetScrollInfo: function (handleOrElement, fnBar, si, redraw) {
            var element = DCGetControlByHandle(handleOrElement);
            if (!element || !si) return 0;

            var container = GetChildContainer(element) || element;

            // Ensure spacer exists to simulate scrollable extent like WinForms ScrollableControl
            function ensureScrollSpacer(host) {
                if (!host.__scrollSpacer) {
                    var spacer = document.createElement('div');
                    spacer.className = 'dc-scroll-spacer';
                    spacer.style.pointerEvents = 'none';
                    spacer.style.visibility = 'hidden';
                    spacer.style.position = 'relative';
                    spacer.style.width = '1px';
                    spacer.style.height = '1px';
                    host.appendChild(spacer);
                    host.__scrollSpacer = spacer;
                }
                return host.__scrollSpacer;
            }

            // Initialize storage for scroll info per axis
            if (!container.__scrollInfo) {
                container.__scrollInfo = {
                    vert: { nMin: 0, nMax: 0, nPage: 0, nPos: 0, nTrackPos: 0 },
                    horz: { nMin: 0, nMax: 0, nPage: 0, nPos: 0, nTrackPos: 0 }
                };
            }

            var barKey = (fnBar === 0) ? "horz" : "vert"; // SB_HORZ=0, SB_VERT=1, others map to vert by default
            var info = container.__scrollInfo[barKey] || { nMin: 0, nMax: 0, nPage: 0, nPos: 0, nTrackPos: 0 };
            container.__scrollInfo[barKey] = info;

            // Win32 SCROLLINFO masks
            var SIF_RANGE = 0x0001, SIF_PAGE = 0x0002, SIF_POS = 0x0004, SIF_DISABLENOSCROLL = 0x0008, SIF_TRACKPOS = 0x0010, SIF_ALL = 0x0017;
            var mask = (typeof si.fMask === "number") ? si.fMask : SIF_ALL;

            if (mask & SIF_RANGE) {
                if (typeof si.nMin === "number") info.nMin = si.nMin | 0;
                if (typeof si.nMax === "number") info.nMax = si.nMax | 0;
            }
            if (mask & SIF_PAGE) {
                if (typeof si.nPage === "number") info.nPage = si.nPage | 0;
            }
            if (mask & SIF_POS) {
                if (typeof si.nPos === "number") info.nPos = si.nPos | 0;
            }
            if (mask & SIF_TRACKPOS) {
                if (typeof si.nTrackPos === "number") info.nTrackPos = si.nTrackPos | 0;
                else info.nTrackPos = info.nPos | 0;
            }

            // Clamp position to range taking page into account
            var span = Math.max(0, (info.nMax | 0) - (info.nMin | 0) + 1);
            var page = Math.max(0, info.nPage | 0);
            var maxPos = Math.max(info.nMin | 0, (info.nMax | 0) - Math.max(page - 1, 0));
            var newPos = Math.min(maxPos, Math.max(info.nMin | 0, info.nPos | 0));
            info.nPos = newPos;

            // Apply to DOM scroll offsets (translate to 0-based DOM coordinates)
            var domPos = newPos - (info.nMin | 0);
            if (barKey === "vert") {
                if (typeof container.scrollTop === "number") container.scrollTop = domPos;
            } else {
                if (typeof container.scrollLeft === "number") container.scrollLeft = domPos;
            }

            // Create/size spacer so DOM has a scrollable extent matching WinForms range
            var spacer = ensureScrollSpacer(container);
            if (barKey === "vert") {
                spacer.style.height = Math.max(span, page) + "px";
                // keep width minimal
                spacer.style.width = spacer.style.width || "1px";
            } else {
                spacer.style.width = Math.max(span, page) + "px";
                spacer.style.height = spacer.style.height || "1px";
            }

            // Toggle overflow visibility similar to DISABLENOSCROLL behavior
            var scrollable = span > page;
            if (!(mask & SIF_DISABLENOSCROLL)) {
                if (barKey === "vert") container.style.overflowY = scrollable ? "auto" : "hidden";
                else container.style.overflowX = scrollable ? "auto" : "hidden";
            } else {
                // leave scrollbar space even if not scrollable
                if (barKey === "vert" && !container.style.overflowY) container.style.overflowY = "auto";
                if (barKey === "horz" && !container.style.overflowX) container.style.overflowX = "auto";
            }

            return newPos | 0;
        },
        /** 模拟win32 API EnableWindow()
         * @param {number} 窗体句柄
         * @param {boolean} 窗体是否可用
         * @returns {boolean} 操作结果
         */
        EnableWindow: function (handleOrElement, bolEnabled) {
            var el = DCGetControlByHandle(handleOrElement);
            if (!el) return false;

            // Determine previous enabled state
            var prevEnabled = true;

            if (el.hasAttribute && el.hasAttribute('disabled')) prevEnabled = false;
            else if (el.getAttribute && el.getAttribute('aria-disabled') === 'true') prevEnabled = false;
            else {
                var cs = cs = window.getComputedStyle(el);
                if (cs && cs.pointerEvents === 'none') prevEnabled = false;
            }


            var enable = !!bolEnabled;
            if (enable) {
                // Remove disabled indicators
                el.removeAttribute && el.removeAttribute('disabled');
                el.removeAttribute && el.removeAttribute('aria-disabled');
                // Restore pointer interactions and opacity
                if (el.style) {
                    if (el.style.pointerEvents === 'none') el.style.pointerEvents = '';
                    if (el.style.opacity) el.style.opacity = '';
                    // default cursor
                    if (el.style.cursor === 'not-allowed') el.style.cursor = '';
                }
            } else {
                // Mark disabled
                el.setAttribute && el.setAttribute('aria-disabled', 'true');
                el.setAttribute && el.setAttribute('disabled', 'disabled');
                if (el.style) {
                    el.style.pointerEvents = 'none';
                    el.style.opacity = '0.6';
                    if (!el.style.cursor) el.style.cursor = 'not-allowed';
                }
                // If currently focused, move focus away
                if (document.activeElement === el && typeof el.blur === 'function') {
                    el.blur();
                }
            }

            // Return previous state per Win32 API convention
            return prevEnabled;
        }
    };
    var __Tasks = new Array();
    var timer_Task = null;
    // 添加任务到任务队列
    function AddTask(task) {
        if (task == null) {
            return;
        }
        if (typeof (task.Eat) === "function") {
            // 吃掉以前的正在排队的任务
            for (var iCount = __Tasks.length - 1; iCount >= 0; iCount--) {
                if (task.Eat.call(task, __Tasks[iCount]) === true) {
                    __Tasks.splice(iCount, 1);
                }
            }
        }
        __Tasks.push(task);
        if (timer_Task != null) {
            window.clearTimeout(timer_Task);
        }
        function RuntFirstTask() {
            var taskToRun = __Tasks.shift();
            if (taskToRun != null) {
                if (typeof (taskToRun) == "function") {
                    taskToRun.call(this);
                }
                else if (typeof (taskToRun.Run) === "function") {
                    taskToRun.Run.call(taskToRun);
                }
            }
            if (__Tasks.length > 0) {
                var nextTask = __Tasks[0];
                var delayTick = 5;
                if (typeof (nextTask.DelayTick) == "number") {
                    delayTick = nextTask.DelayTick;
                }
                timer_Task = window.setTimeout(RuntFirstTask, delayTick);
            }
        }
        var delayTick = 5;
        if (typeof (__Tasks[0].DelayTick) == "number") {
            delayTick = __Tasks[0].DelayTick;
        }
        timer_Task = window.setTimeout(RuntFirstTask, delayTick);
    }
    __DCWin32API.AddTask = AddTask;

    /**
        * 将坐标位置从开始元素的客户区坐标转换为目标元素的客户区坐标
        * @param {HTMLElement} srcElement 开始坐标
        * @param {HTMLElement} targetElement 目标元素坐标
        * @param {number} intX
        * @param {number} intY
        * @returns {object} 转换后的坐标，具有X,Y两个属性
        */
    function MapElementPoint(srcElement, targetElement, intX, intY) {
        if (!srcElement || !targetElement) {
            return { X: intX | 0, Y: intY | 0 };
        }

        // Get bounding rectangles
        var srcRect = srcElement.getBoundingClientRect();
        var tgtRect = targetElement.getBoundingClientRect();

        // Convert src client point to viewport coordinates
        var viewportX = (srcRect.left | 0) + (intX | 0);
        var viewportY = (srcRect.top | 0) + (intY | 0);

        // Convert viewport to target client coordinates
        var xInTarget = (viewportX - (tgtRect.left | 0)) | 0;
        var yInTarget = (viewportY - (tgtRect.top | 0)) | 0;

        // Adjust for target scroll (client coords exclude scroll offset)
        var scrollLeft = 0, scrollTop = 0;
        scrollLeft = targetElement.scrollLeft | 0;
        scrollTop = targetElement.scrollTop | 0;

        xInTarget += scrollLeft;
        yInTarget += scrollTop;

        return { X: xInTarget, Y: yInTarget };
    }
    /** 发送Win32消息到控件 */
    async function SendMessageToControl(
        handleOrElement,
        intMsg,
        intWParam,
        intLParam,
        objWParam,
        objLParam) {
        var intHandle = DCGetControlHandle(handleOrElement);
        if (intHandle >= 0) {
            if (typeof (__DCInvokeCSMethod) == "function") {
                try {
                    await __DCInvokeCSMethodAsync(
                        "SendMessageToControl",
                        intHandle,
                        intMsg,
                        intWParam,
                        intLParam,
                        objWParam,
                        objLParam);
                } catch (e) {
                    console.log(e);
                    throw e;
                }
            }
        }
    }
    /** 发送Win32消息到控件 */
    function PostMessageToControl(handleOrElement, intMsg, intWParam, intLParam) {

        var intHandle = DCGetControlHandle(handleOrElement);
        if (intHandle >= 0) {
            AddTask({
                Handle: intHandle,
                Msg: intMsg,
                WParam: intWParam,
                LParam: intLParam,
                Run: function () {
                    __DCInvokeCSMethod(
                        "SendMessageToControl",
                        this.Handle,
                        this.Msg,
                        this.WParam,
                        this.LParam);
                }
            });

        }
    }
    window.__DCWin32API.SendMessageToControl = SendMessageToControl;

    window.__DCHandlePostedMessage = function () {
        var task2 = {
            Name: "HandlePostedMessage",
            Eat: function (item) {
                if (item.Name == this.Name) {
                    return true;
                }
                return false;
            },
            Run: function () {
                __DCExecuteControlCommand(
                    0,
                    "HandlePostedMessage");
            }
        };
        AddTask(task2);
    };
    /**
     * 判断能否处理事件
     * @param {any} element
     * @param {any} eventArgs
     * @returns
     */
    function AllowHandleEvent(eventArgs) {
        var element = eventArgs.target || eventArgs.srcElement;
        if (eventArgs != null
            && eventArgs.type != null
            && element != null
            && element._allowEventNames != null) {
            eventArgs.cancelBubble = true;
            eventArgs.stopPropagation();
            if (element.__allowEventNames.indexOf(eventArgs.type.toLowerCase()) < 0) {
                return false;
            }
        }
        return true;
    };
    /** 发送按键按下消息到控件 */
    function HandleElementKeyDownEvent(eventArgs) {
        // Simulate Win32 WM_KEYDOWN
        var WM_KEYDOWN = 0x0100;
        var e = eventArgs || window.event;
        if (!e) return;
        if (AllowHandleEvent(e) == false) {
            return;
        }
        var htmlElement = e.target || e.srcElement;
        // Virtual key code
        var vk = (typeof e.keyCode === 'number' ? e.keyCode : (typeof e.which === 'number' ? e.which : 0)) | 0;

        // Compose lParam per Win32:
        // bits 0-15: repeat count (use 1 for simple keydown)
        // bits 16-23: scan code (not available in browser -> 0)
        // bit 24: extended-key flag
        // bit 29: context code (ALT key state)
        // bit 30: previous key state (0 for keydown)
        // bit 31: transition state (0 for keydown)
        var repeatCount = 1;
        var scanCode = 0;
        var extended = (e.location === 2 || e.location === 3) ? 1 : 0; // RIGHT or NUMPAD
        var altDown = e.altKey ? 1 : 0;
        var prevState = 0;
        var transition = 0;
        var lParam = (repeatCount & 0xFFFF) | ((scanCode & 0xFF) << 16) | (extended << 24) | (altDown << 29) | (prevState << 30) | (transition << 31);

        // Send message to control
        SendMessageToControl(htmlElement, WM_KEYDOWN, vk, lParam);
    }
    /** 发送按键字符消息到控件 */
    function HandleElementKeyPressEvent(eventArgs) {
        var WM_CHAR = 0x0102;
        var e = eventArgs || window.event;
        if (!e) return;
        if (AllowHandleEvent(e) == false) {
            return;
        }
        var htmlElement = e.target || e.srcElement;
        var charCode = typeof e.charCode === 'number' ? e.charCode : (typeof e.keyCode === 'number' ? e.keyCode : 0);
        var altDown = e.altKey ? 1 : 0;
        var ctrlDown = e.ctrlKey ? 1 : 0;
        var shiftDown = e.shiftKey ? 1 : 0;
        var lParam = (altDown << 29) | (ctrlDown << 28) | (shiftDown << 27);
        SendMessageToControl(htmlElement, WM_CHAR, charCode | 0, lParam);
    }
    /** 发送按键松开消息到控件 */
    function HandleElementKeyUpEvent(eventArgs) {
        var WM_KEYUP = 0x0101;
        var e = eventArgs || window.event;
        if (!e) return;
        if (AllowHandleEvent(e) == false) {
            return;
        }
        var htmlElement = e.target || e.srcElement;
        var vk = (typeof e.keyCode === 'number' ? e.keyCode : (typeof e.which === 'number' ? e.which : 0)) | 0;
        var altDown = e.altKey ? 1 : 0;
        var lParam = (altDown << 29) | (1 << 30) | (1 << 31); // prev/transition set for keyup
        SendMessageToControl(htmlElement, WM_KEYUP, vk, lParam);
    }

    async function HandleMouseEvent(eventArgs, msgId) {
        var e = eventArgs || window.event;
        if (!e) return;
        if (AllowHandleEvent(e) == false) {
            return;
        }
        var htmlElement = e.target || e.srcElement;
        e.cancelBubble = true;
        if (e.currentTarget instanceof HTMLElement
            && e.currentTarget != htmlElement) {
            return;
        }
        var x = e.offsetX | 0;
        var y = e.offsetY | 0;
        var wParam = 0;
        if (e.shiftKey) wParam |= 0x0004; // MK_SHIFT
        if (e.ctrlKey) wParam |= 0x0008;  // MK_CONTROL
        if (e.buttons & 1) wParam |= 0x0001; // MK_LBUTTON
        if (e.buttons & 2) wParam |= 0x0002; // MK_RBUTTON
        if (e.buttons & 4) wParam |= 0x0010; // MK_MBUTTON
        var lParam = (x & 0xFFFF) | ((y & 0xFFFF) << 16);
        var sc = GetOwnerScreenElement(htmlElement);
        if (sc != null) {
            var p2 = MapElementPoint(htmlElement, sc, x, y);
            __DCExecuteControlCommand(
                0,
                "MousePositon",
                {
                    X: p2.X,
                    Y: p2.Y
                });
        }
        SendMessageToControl(htmlElement, msgId, wParam, lParam);
    }

    function HandleMouseDownEvent(e) {
        if (AllowHandleEvent(e) == false) {
            return;
        }
        var btn = (typeof e.button === 'number') ? e.button : (typeof e.which === 'number' ? (e.which - 1) : 0);
        // 0: left, 1: middle, 2: right
        var msg = 0x0201; // WM_LBUTTONDOWN
        if (btn === 2) { msg = 0x0204; /* WM_RBUTTONDOWN */ e.preventDefault(); }
        else if (btn === 1) { msg = 0x0207; /* WM_MBUTTONDOWN */ }
        HandleMouseEvent(e, msg);
    }
    function HandleMouseMoveEvent(e) {
        if (AllowHandleEvent(e) == false) {
            return;
        }
        // 特别处理鼠标移动事件，由于鼠标移动事件是高频事件，在此进行
        // 一些消息的清理。
        var task = {
            Name: "HandleMouseMoveEvent",
            Element: e.target || e.srcElement,
            EventArgs: e,
            Eat: function (item) {
                return item.Name == this.Name && item.Element == this.Element;
            },
            Run: function () {
                HandleMouseEvent(this.EventArgs, 0x0200);
            }
        };
        AddTask(task);
        //HandleMouseEvent(e, 0x0200);
    }
    function HandleMouseUpEvent(e) {
        if (AllowHandleEvent(e) == false) {
            return;
        }
        var btn = (typeof e.button === 'number') ? e.button : (typeof e.which === 'number' ? (e.which - 1) : 0);
        // 0: left, 1: middle, 2: right
        var msg = 0x0202; // WM_LBUTTONUP
        if (btn === 2) { msg = 0x0205; /* WM_RBUTTONUP */ e.preventDefault(); }
        else if (btn === 1) { msg = 0x0208; /* WM_MBUTTONUP */ }
        HandleMouseEvent(e, msg);
    }

    function HandleMouseWheelEvent(eventArgs) {

        var WM_MOUSEWHEEL = 0x020A;
        var e = eventArgs || window.event;
        if (!e) return;
        if (AllowHandleEvent(e) == false) {
            return;
        }

        var htmlElement = e.target || e.srcElement;
        e.cancelBubble = true;
        var delta = e.deltaY ? -e.deltaY : (e.wheelDelta || 0); // normalize: wheel up -> positive
        var wParam = ((delta & 0xFFFF) << 16); // HIWORD: wheel delta
        if (e.shiftKey) wParam |= 0x0004; // MK_SHIFT
        if (e.ctrlKey) wParam |= 0x0008;  // MK_CONTROL
        var x = e.clientX | 0;
        var y = e.clientY | 0;
        var lParam = (x & 0xFFFF) | ((y & 0xFFFF) << 16);
        SendMessageToControl(htmlElement, WM_MOUSEWHEEL, wParam, lParam);
    }

    function HandleClickEvent(eventArgs) {
        //var e = eventArgs || window.event;
        //if (!e) return;
        //if (AllowHandleEvent(e) == false) {
        //    return;
        //}
        //var msg = 0x0201; // WM_LBUTTONDOWN for click
        //HandleMouseEvent(e, msg);
    }
    function HandleDblClickEvent(eventArgs) {
        var e = eventArgs || window.event;
        if (!e) return;
        if (AllowHandleEvent(e) == false) {
            return;
        }
        var htmlElement = e.target || e.srcElement;
        var msg = 0x0203; // WM_LBUTTONDBLCLK
        HandleMouseEvent(e, msg);
    }

    async function HandleDragOverEvent(e) {
        if (!e) return;
        var htmlElement = e.target || e.srcElement;
        e.cancelBubble = true;
        var msg = 0x400 + 1;
        var x = e.clientX | 0;
        var y = e.clientY | 0;
        var lParam = (x & 0xFFFF) | ((y & 0xFFFF) << 16);
        SendMessageToControl(htmlElement, msg, 0, lParam);
    }
    async function HandleDropEvent(e) {
        if (!e) return;
        var htmlElement = e.target || e.srcElement;
        e.cancelBubble = true;
        var msg = 0x400 + 2;
        var x = e.clientX | 0;
        var y = e.clientY | 0;
        var lParam = (x & 0xFFFF) | ((y & 0xFFFF) << 16);
        SendMessageToControl(htmlElement, msg, 0, lParam);
    }
    async function HandleScrollEvent(e) {
        // Simulate Win32 WM_VSCROLL / WM_HSCROLL on element scroll
        var WM_VSCROLL = 0x0115;
        var WM_HSCROLL = 0x0114;
        var SB_THUMBTRACK = 5; // LOWORD of wParam indicates scroll request code

        var ev = e || window.event;
        if (!ev) return;
        var htmlElement = ev.target || ev.srcElement;
        ev.cancelBubble = true;

        // Vertical scroll
        if (typeof htmlElement.scrollTop === 'number') {
            var posV = Math.max(0, htmlElement.scrollTop) | 0;
            var wParamV = (posV << 16) | (SB_THUMBTRACK & 0xFFFF);
            var lParamV = 0; // scrollbar handle not available in DOM
            SendMessageToControl(htmlElement, WM_VSCROLL, wParamV, lParamV);
        }

        // Horizontal scroll
        if (typeof htmlElement.scrollLeft === 'number') {
            var posH = Math.max(0, htmlElement.scrollLeft) | 0;
            var wParamH = (posH << 16) | (SB_THUMBTRACK & 0xFFFF);
            var lParamH = 0;
            SendMessageToControl(htmlElement, WM_HSCROLL, wParamH, lParamH);
        }
    }
    /** 标准的HTML元素事件处理函数清单 */
    window.__DCWin32API.StandardHtmlElementEventHandler = {
        onkeydown: HandleElementKeyDownEvent,
        onkeypress: HandleElementKeyPressEvent,
        onkeyup: HandleElementKeyUpEvent,
        onmousedown: HandleMouseDownEvent,
        onmousemove: HandleMouseMoveEvent,
        onmouseup: HandleMouseUpEvent,
        onmousewheel: HandleMouseWheelEvent,
        onclick: HandleClickEvent,
        ondblclick: HandleDblClickEvent,
        ondragover: HandleDragOverEvent,
        ondrop: HandleDropEvent,
        onscroll: HandleScrollEvent
    };
    window.__DCWin32API.BindingStandardControlEvent = function (element, strEventNames) {
        if (element == null) return false;
        var targetElement = element.getAttribute ? element : DCGetControlByHandle(element);
        if (!targetElement) return false;
        if (strEventNames != null) {
            var names = strEventNames.trim().toLowerCase().split('|');
            targetElement.__allowEventNames = strEventNames;

            for (var name of names) {
                switch (name) {
                    case "keydown": targetElement.addEventListener("keydown", HandleElementKeyDownEvent, false); break;
                    case "keypress": targetElement.addEventListener("keypress", HandleElementKeyPressEvent, false); break;
                    case "keyup": targetElement.addEventListener("keyup", HandleElementKeyUpEvent, false); break;
                    case "mousedown": targetElement.addEventListener("mousedown", HandleMouseDownEvent, false); break;
                    case "mousemove": targetElement.addEventListener("mousemove", HandleMouseMoveEvent, false); break;
                    case "mouseup": targetElement.addEventListener("mouseup", HandleMouseUpEvent, false); break;
                    case "mousewheel": targetElement.addEventListener("wheel", HandleMouseWheelEvent, false); break;
                    case "click": targetElement.addEventListener("click", HandleClickEvent, false); break;
                    case "dblclick": targetElement.addEventListener("dblclick", HandleDblClickEvent, false); break;
                    case "dragover": targetElement.addEventListener("dragover", HandleDragOverEvent, false); break;
                    case "drop": targetElement.addEventListener("drop", HandleDropEvent, false); break;
                    //case "scroll": targetElement.addEventListener("scroll", HandleScrollEvent, false); break;
                    default: break;
                }
            }
            return true;
        }
        return false;
    };
    /**
     * 清空绑定的标准控件事件
     * @param {any} htmlElement
     */
    window.__DCWin32API.ClearStandardControlEvent = function (htmlElement) {
        var targetElement = DCGetControlByHandle(htmlElement);
        if (!targetElement) return false;
        targetElement.removeEventListener("keydown", HandleElementKeyDownEvent, false);
        targetElement.removeEventListener("keypress", HandleElementKeyPressEvent, false);
        targetElement.removeEventListener("keyup", HandleElementKeyUpEvent, false);
        targetElement.removeEventListener("mousedown", HandleMouseDownEvent, false);
        targetElement.removeEventListener("mousemove", HandleMouseMoveEvent, false);
        targetElement.removeEventListener("mouseup", HandleMouseUpEvent, false);
        targetElement.removeEventListener("wheel", HandleMouseWheelEvent, false);
        targetElement.removeEventListener("click", HandleClickEvent, false);
        targetElement.removeEventListener("dblclick", HandleDblClickEvent, false);
        targetElement.removeEventListener("dragover", HandleDragOverEvent, false);
        targetElement.removeEventListener("drop", HandleDropEvent, false);
        targetElement.removeEventListener("scroll", HandleScrollEvent, false);
        targetElement.__allowEventNames = null;
        return true;
    };
    function BindBaseControlEvent(htmlElement2) {
        function HandleOneEvent(htmlElement2, eventName, ...args) {
            var strHandle = htmlElement2.getAttribute("dchandle");
            if (strHandle != null && strHandle.length > 0) {
                __DCInvokeCSMethod(
                    eventName,
                    parseInt(strHandle),
                    ...args);
            }
        }
        htmlElement2.onkeydown = HandleElementKeyDownEvent;
        htmlElement2.onkeypress = HandleElementKeyPressEvent;
        htmlElement2.onkeyup = HandleElementKeyUpEvent;
        htmlElement2.onmousedown = HandleMouseDownEvent;
        htmlElement2.onmousemove = HandleMouseMoveEvent;
        htmlElement2.onmouseup = HandleMouseUpEvent;
        htmlElement2.onwheel = HandleMouseWheelEvent;
        htmlElement2.onclick = HandleClickEvent;
        htmlElement2.ondblclick = HandleDblClickEvent;
        htmlElement2.ondragover = HandleDragOverEvent;
        htmlElement2.ondrop = HandleDropEvent;
        function HandleDrag(htmlElement4, strEventName, args) {

        };
    }

    /**
        * 获取指定HTML元素的完整字体样式信息（不创建临时HTML元素）
        * @param {HTMLElement} element - 目标HTML元素（必填）
        * @returns {Object|null} 字体样式信息对象，失败返回null
        * @property {string} computedFontFamily - 计算后的字体列表（浏览器最终解析的字体序列）
        * @property {string} primaryFontFamily - 优先使用的字体名称（字体列表第一个有效名称，无实际检测）
        * @property {number} fontSizePx - 字体大小（像素单位）
        * @property {number} fontSizePt - 字体大小（Point/磅单位，保留2位小数）
        * @property {boolean} isBold - 是否为粗体
        * @property {boolean} isItalic - 是否为斜体
        * @property {boolean} hasUnderline - 是否有下划线
        * @property {boolean} hasStrikethrough - 是否有删除线（对应strictline）
        */
    function DCGetElementFontFullInfo(element) {
        // 校验参数有效性
        if (!element || !(element instanceof HTMLElement)) {
            console.error("参数错误：必须传入有效的HTMLElement对象");
            return null;
        }

        // 获取元素最终计算样式（核心API，获取实际渲染样式）
        const computedStyle = window.getComputedStyle(element);

        // --------------- 1. 字体名称处理（无临时元素，解析字体列表） ---------------
        const fontFamilyStr = computedStyle.fontFamily;
        // 解析字体列表：去除引号、多余空格，过滤空值
        const fontList = fontFamilyStr
            .split(",")
            .map(font => {
                // 去除字体名称前后空格和引号（单/双引号）
                const trimmedFont = font.trim();
                return trimmedFont.replace(/^["']|["']$/g, "");
            })
            .filter(font => font.length > 0);

        // 优先生效字体（列表第一个有效字体，无临时元素时无法精准检测实际渲染字体）
        const primaryFontFamily = fontList.length > 0 ? fontList[0] : "sans-serif";
        // 计算后的完整字体列表
        const computedFontFamily = fontList.join(", ");

        // --------------- 2. 字体大小（px转pt，遵循CSS标准换算） ---------------
        const fontSizePx = parseFloat(computedStyle.fontSize);
        // 换算公式：1pt = 1/72英寸，1px = 1/96英寸 → pt = px * (72/96) = px * 0.75
        const fontSizePt = parseFloat((fontSizePx * 0.75).toFixed(2));

        // --------------- 3. 字体样式状态判断（精准兼容多种场景） ---------------
        // 粗体判断：兼容bold关键字（等同于700）和≥700的数值
        const fontWeight = computedStyle.fontWeight;
        const isBold = fontWeight === "bold" || parseInt(fontWeight, 10) >= 700;

        // 斜体判断：兼容italic（字体自带斜体）和oblique（强制倾斜）
        const fontStyle = computedStyle.fontStyle;
        const isItalic = fontStyle === "italic" || fontStyle === "oblique";

        // 下划线判断：text-decoration属性包含underline（支持多装饰共存）
        const textDecoration = computedStyle.textDecoration;
        const hasUnderline = textDecoration.includes("underline");

        // 删除线判断：text-decoration属性包含line-through（对应strictline）
        const hasStrikethrough = textDecoration.includes("line-through");

        // --------------- 返回完整结果 ---------------
        return {
            computedFontFamily: computedFontFamily,
            primaryFontFamily: primaryFontFamily,
            fontSizePx: fontSizePx,
            fontSizePt: fontSizePt,
            isBold: isBold,
            isItalic: isItalic,
            hasUnderline: hasUnderline,
            hasStrikethrough: hasStrikethrough
        };
    }
    var screenHtmlElement = null;
    function GetOwnerScreenElement(htmlElement) {
        var p = htmlElement;
        while (p != null) {
            if (p.getAttribute && p.getAttribute("dctype") === "System.Windows.Forms.Screen") {
                return p;
            }
            p = p.parentNode;
        }
        return null;
    }
    /**
        * 根据句柄获取控件对应的HTML元素
        * @param {number | HTMLElement} handle
        * @returns {HTMLElement} 获得的HTML元素
        */
    function DCGetControlByHandle(handle, bolThrowException) {
        if (handle instanceof HTMLElement) {
            var p = handle;
            while (p != null) {
                if (p.getAttribute && p.getAttribute("dchandle") != null) {
                    return p;
                }
                p = p.parentNode;
            }
            if (bolThrowException === true) {
                throw new Error("指定的HTML元素不是有效的控件元素。");
            }
            return null;
        }
        var result = document.querySelector("[dchandle='" + handle + "']");
        if (result != null) {
            return result;
        }
        if (screenHtmlElement != null && screenHtmlElement.__DCControls != null) {
            return screenHtmlElement.__DCControls.get(handle);
        }
        if (bolThrowException === true) {
            throw new Error("未找到指定句柄的HTML元素:" + handle);
        }
        return null;
    };
    window.__DCWin32API.DCGetControlByHandle = DCGetControlByHandle;
    /**
        * 获得控件的句柄
        * @param {控件对象} htmlElement
        * @returns {number} 控件句柄
        */
    function DCGetControlHandle(htmlElement) {
        if (htmlElement == null) return -1;
        if (typeof (htmlElement) == "number") {
            return htmlElement;
        }
        var p = htmlElement;
        while (p != null) {
            var strHandle = p.getAttribute("dchandle");
            if (strHandle != null && strHandle.length > 0) {
                return parseInt(strHandle);
            }
            p = p.parentNode;
        }
        return -1;
    }
    window.__DCWin32API.DCGetControlHandle = DCGetControlHandle;
    function DCRegisterControl(handle, htmlElement) {
        if (screenHtmlElement != null) {
            if (screenHtmlElement.__DCControls == null) {
                screenHtmlElement.__DCControls = new Map();
            }
            screenHtmlElement.__DCControls.set(handle, htmlElement);
            htmlElement.__ScreenElement = screenHtmlElement;
        }
    };
    window.__DCSetScreen = function (idOrElement) {
        var htmlElement = null;
        if (idOrElement instanceof HTMLElement) {
            htmlElement = idOrElement;
        }
        else {
            htmlElement = document.getElementById(idOrElement);
        }
        if (htmlElement == null) {
            htmlElement = document.querySelector("[dctype='System.Windows.Forms.Screen']");
        }
        if (htmlElement == null) {
            return false;
        }
        if (screenHtmlElement != null
            && screenHtmlElement != htmlElement
            && screenHtmlElement.__ResizeObserver != null) {
            screenHtmlElement.__ResizeObserver.disconnect();
        }
        screenHtmlElement = htmlElement;

        function ScreenResizeHandler() {
            var fontinfo = DCGetElementFontFullInfo(screenHtmlElement);
            __DCExecuteControlCommand(
                0,
                "SetScreenSize",
                {
                    Width: screenHtmlElement.clientWidth,
                    Height: screenHtmlElement.clientHeight,
                    DefaultFontName: fontinfo.computedFontFamily || null,
                    DefaultFontSize: fontinfo.fontSizePt || 0
                });
        }
        screenHtmlElement.__ResizeObserver = new ResizeObserver(ScreenResizeHandler);
        screenHtmlElement.__ResizeObserver.observe(screenHtmlElement);
        screenHtmlElement.DCRun = function (strMethodName, strCommandLine) {
            // Parse command line into arguments similar to Windows CMD -> Main(string[])
            function parseCommandLine(cmd) {
                if (cmd == null || cmd.length == null) return null;
                var s = String(cmd);
                var args = [];
                var cur = '';
                var inQuotes = false;
                var i = 0;
                while (i < s.length) {
                    var ch = s[i];
                    if (ch === ' ' || ch === '\t') {
                        if (!inQuotes) {
                            if (cur.length > 0) { args.push(cur); cur = ''; }
                            i++;
                            continue;
                        } else {
                            cur += ch; i++; continue;
                        }
                    }
                    if (ch === '"') {
                        // Count preceding backslashes
                        var bs = 0; var j = i - 1;
                        while (j >= 0 && s[j] === '\\') { bs++; j--; }
                        if (bs % 2 === 0) {
                            // Unescaped quote toggles quoting state
                            inQuotes = !inQuotes; i++;
                            continue;
                        } else {
                            // Escaped quote: include proper number of backslashes and a quote
                            cur = cur.slice(0, cur.length - Math.floor(bs / 2)) + '"';
                            i++;
                            continue;
                        }
                    }
                    if (ch === '\\') {
                        // Backslashes: may escape quotes; otherwise literal
                        // Collect run of backslashes
                        var start = i;
                        while (i < s.length && s[i] === '\\') i++;
                        var count = i - start;
                        // If next char is a quote, backslashes escape it per CommandLineToArgvW
                        if (i < s.length && s[i] === '"') {
                            // For each pair emit one backslash, odd one escapes the quote
                            cur += '\\'.repeat(Math.floor(count / 2));
                            if (count % 2 === 1) {
                                // Escapes the quote; add literal quote
                                cur += '"';
                                i++; // consume quote
                            }
                        } else {
                            cur += '\\'.repeat(count);
                        }
                        continue;
                    }
                    cur += ch;
                    i++;
                }
                if (cur.length > 0) args.push(cur);
                return args;
            }

            var argv = parseCommandLine(strCommandLine);
            __DCInvokeCSMethod(strMethodName, argv);
        };
        ScreenResizeHandler();
        while (screenHtmlElement.firstChild != null) {
            screenHtmlElement.removeChild(screenHtmlElement.firstChild);
        }
        // 执行入口点方法
        var strEnterPoint = screenHtmlElement.getAttribute("EntryPoint");
        if (strEnterPoint != null && strEnterPoint.length > 0) {
            screenHtmlElement.DCRun.call(screenHtmlElement, strEnterPoint, "");
            //__DCInvokeCSMethod(strEnterPoint);
        }
    };
    window.__DCWin32API.SetForegroundWindow = function (handleOfElement) {
        var el = DCGetControlByHandle(handleOfElement);
        if (!el) return false;

        // If detached, attach back to its screen container
        if (!el.parentNode && el.__ScreenElement) {
            el.__ScreenElement.appendChild(el);
        }
        // Ensure visible
        if (el.style.display === 'none') el.style.display = '';
        el.style.visibility = 'visible';

        // Bring to front by increasing z-index
        var currentZ = parseInt(el.style.zIndex) || 0;
        el.style.zIndex = Math.max(currentZ, 1000) + 1;

        // Try to focus
        if (typeof el.focus === 'function') {
            el.focus({ preventScroll: true });
        }
        return true;
    };
    /** 模拟 WIN32 API SetFocus( )*/
    window.__DCWin32API.SetFocus = function (handleOfElement) {
        var el = DCGetControlByHandle(handleOfElement);
        if (!el) return 0;
        // remember previous focused handle (if any)
        var prev = prev = window.__DCWin32API.GetFocus() || 0;

        // If detached from DOM, reattach to screen container so focus can work
        if (!el.parentNode && el.__ScreenElement) {
            el.__ScreenElement.appendChild(el);
        }

        // Ensure element is visible to accept focus
        if (el.style && el.style.display === 'none') el.style.display = '';

        // Make element focusable if necessary by adding temporary tabindex
        var addedTab = false;
        if (el.hasAttribute && !el.hasAttribute('tabindex')) {
            el.setAttribute('tabindex', '-1');
            addedTab = true;
        }
        if (typeof el.focus === 'function') {
            el.focus({ preventScroll: true });
        }
        if (addedTab) {
            el.removeAttribute('tabindex');
        }

        return prev | 0;
    };
    window.__DCWin32API.GetFocus = function () {
        var ae = document.activeElement;
        if (!ae) return 0;

        // If activeElement is inside shadow DOM, attempt to get deeper focus
        var root = ae.getRootNode && ae.getRootNode();
        if (root && root.activeElement) ae = root.activeElement;

        // Walk up to find element with dchandle
        for (var n = ae; n != null; n = n.parentElement) {
            var h = n.getAttribute && n.getAttribute('dchandle');
            if (h != null && isNaN(h) == false) return parseInt(h);
        }
        // Fallback: check registered controls map for focus containment
        if (screenHtmlElement && screenHtmlElement.__DCControls) {
            for (var [handle, element] of screenHtmlElement.__DCControls.entries()) {
                if (element && element.contains && element.contains(document.activeElement)) {
                    return parseInt(handle);
                }
            }
        }
        return 0;
    };
    /**
        * 获得承载子控件的容器元素
        * @param {HTMLElement} element 元素对象
        * @returns {HTMLElement} 容器元素
        */
    function GetChildContainer(element) {
        if (typeof (element.__GetChildContainer) == "function") {
            var result = element.__GetChildContainer.call(element);
            if (result != null) {
                return result;
            }
        }
        return element;
    }
    window.__DCWin32API.GetWindow = function (elementOrHandle, nCmd) {
        var el = DCGetControlByHandle(elementOrHandle);
        if (!el) return 0;

        var GW_HWNDFIRST = 0;
        var GW_HWNDLAST = 1;
        var GW_HWNDNEXT = 2;
        var GW_HWNDPREV = 3;
        var GW_OWNER = 4;
        var GW_CHILD = 5;
        var GW_ENABLEDPOPUP = 6;

        function getHandleFromElement(e) {
            if (!e) return 0;
            var h = e.getAttribute && e.getAttribute('dchandle');
            if (h != null && isNaN(h) == false) return parseInt(h);
            return 0;
        }

        function childContainerOf(e) {
            return GetChildContainer(e);
        }

        function firstChildHandle(e) {
            var container = childContainerOf(e);
            if (!container || !container.children) return 0;
            for (var i = 0; i < container.children.length; i++) {
                var h = getHandleFromElement(container.children[i]);
                if (h) return h;
            }
            return 0;
        }

        function lastChildHandle(e) {
            var container = childContainerOf(e);
            if (!container || !container.children) return 0;
            for (var i = container.children.length - 1; i >= 0; i--) {
                var h = getHandleFromElement(container.children[i]);
                if (h) return h;
            }
            return 0;
        }

        function nextSiblingHandle(e) {
            if (!e || !e.parentElement) return 0;
            var parent = childContainerOf(e.parentElement) || e.parentElement;
            var kids = parent.children || [];
            var found = false;
            for (var i = 0; i < kids.length; i++) {
                if (kids[i] === e) { found = true; continue; }
                if (found) {
                    var h = getHandleFromElement(kids[i]);
                    if (h) return h;
                }
            }
            return 0;
        }

        function prevSiblingHandle(e) {
            if (!e || !e.parentElement) return 0;
            var parent = childContainerOf(e.parentElement) || e.parentElement;
            var kids = parent.children || [];
            var prev = 0;
            for (var i = 0; i < kids.length; i++) {
                if (kids[i] === e) return prev;
                var h = getHandleFromElement(kids[i]);
                if (h) prev = h;
            }
            return 0;
        }

        switch (nCmd) {
            case GW_CHILD:
                return firstChildHandle(el);
            case GW_HWNDFIRST:
                if (el.parentElement) return firstChildHandle(el.parentElement);
                return 0;
            case GW_HWNDLAST:
                if (el.parentElement) return lastChildHandle(el.parentElement);
                return 0;
            case GW_HWNDNEXT:
                return nextSiblingHandle(el);
            case GW_HWNDPREV:
                return prevSiblingHandle(el);
            case GW_OWNER:
                return GetParent(el);
            case GW_ENABLEDPOPUP:
                return 0;
            default:
                return 0;
        }
    };
    /** 模拟WIN32 API IsWindowVisible
        * @param {number | HTMLElement} elementOrHandle 控件句柄或者HTML元素
        * @returns { boolean } 窗体是否可见
        */
    window.__DCWin32API.IsWindowVisible = function (elementOrHandle) {
        var ele = DCGetControlByHandle(elementOrHandle);
        if (!ele) return false;
        // If not attached to DOM treat as hidden
        if (typeof ele.isConnected === 'boolean' && !ele.isConnected) return false;

        // check own computed style
        var cs = window.getComputedStyle(ele);
        if (cs) {
            if (cs.display === 'none' || cs.visibility === 'hidden' || cs.visibility === 'collapse') return false;
        } else if (ele.style && ele.style.display === 'none') {
            return false;
        }

        // check ancestors
        for (var p = ele.parentElement; p != null; p = p.parentElement) {
            var pcs = window.getComputedStyle(p);
            if (pcs) {
                if (pcs.display === 'none' || pcs.visibility === 'hidden' || pcs.visibility === 'collapse') return false;
            } else if (p.style && p.style.display === 'none') {
                return false;
            }
        }

        return true;
    };
    /** 模拟WIN32 API IsWindowVisible(int)
    * @param {number | HTMLElement} elementOrHandle 控件句柄或者HTML元素
    * @returns { boolean } 窗体是否可用
    */
    window.__DCWin32API.IsWindowEnabled = function (elementOrHandle) {
        var ele = DCGetControlByHandle(elementOrHandle);
        if (!ele) return false;
        // explicit disabled attributes
        if (ele.hasAttribute && ele.hasAttribute('disabled')) return false;
        if (ele.getAttribute && ele.getAttribute('aria-disabled') === 'true') return false;

        // computed style pointer-events
        var cs = window.getComputedStyle(ele);
        if (cs && cs.pointerEvents === 'none') return false;

        // ancestor disabling
        for (var p = ele.parentElement; p != null; p = p.parentElement) {
            if (p.hasAttribute && p.hasAttribute('disabled')) return false;
            var pcs = null;
            pcs = window.getComputedStyle(p);
            if (pcs && pcs.pointerEvents === 'none') return false;
        }

        return true;
    };
    window.__DCWin32API.SetWindowText = function (elementOrHandle, strText) {
        var ele = DCGetControlByHandle(elementOrHandle);
        if (ele == null) {
            return false;
        }
        var fact = ele.__DCFactory;
        if (fact != null) {
            return fact.SetWindowText.call(fact, ele, strText);
        }
        else {
            return false;
        }
    };
    /**
        * 获得窗体文本
        * @param {number | HTMLElement} elementOrHandle
        * @returns {string} 获得的文本
        */
    window.__DCWin32API.GetWindowText = function (elementOrHandle) {
        var ele = DCGetControlByHandle(elementOrHandle);
        if (ele == null) {
            return null;
        }
        else {
            var fact = ele.__DCFactory;
            if (fact != null) {
                return fact.GetWindowText.call(fact, ele);
            }
        }
        return null;
    };
    /**
        * 模拟Win32 API ShowWindow函数
        * @param {number | HTMLElement} elementOrHandle
        * @param {number} nCmdShow
        * @returns {boolean } 操作是否成功
        */
    function ShowWindow(elementOrHandle, nCmdShow) {
        if (elementOrHandle == null) return false;
        var el = DCGetControlByHandle(elementOrHandle);
        if (!el) return false;
        if (el.parentNode == null && el.__ScreenElement != null) {
            el.__ScreenElement.appendChild(el);
        }
        // Common SW_* values
        var SW_HIDE = 0;
        var SW_SHOWNORMAL = 1;
        var SW_SHOWMINIMIZED = 2;
        var SW_SHOWMAXIMIZED = 3;
        var SW_SHOWNOACTIVATE = 4;
        var SW_SHOW = 5;
        var SW_MINIMIZE = 6;
        var SW_RESTORE = 9;
        var SW_SHOWDEFAULT = 10;

        function bringToFront(target) {
            // increase zIndex to bring to front
            var z = parseInt(target.style.zIndex) || 0;
            target.style.zIndex = Math.max(z, 1000) + 1;
        }

        switch (nCmdShow) {
            case SW_HIDE:
                // hide element
                el.style.display = 'none';
                if (el._dc_windowState) el._dc_windowState = 'minimized';
                return true;

            case SW_SHOWMINIMIZED:
            case SW_MINIMIZE:
                // minimize: hide visually but keep previous display semantics
                el.style.display = 'none';
                el._dc_windowState = 'minimized';
                return true;

            case SW_SHOWMAXIMIZED:
                // maximize
                // 如果元素有 _dc_doMaximize 方法，直接调用它（会正确处理按钮图标等）
                if (typeof el._dc_doMaximize === 'function') {
                    el._dc_doMaximize();
                    el.style.display = '';
                    el.style.visibility = 'visible';
                    bringToFront(el);
                    return true;
                }
                // 否则使用原来的逻辑
                if (!el._dc_prevBounds) {
                    var rect = el.getBoundingClientRect();
                    el._dc_prevBounds = { left: rect.left, top: rect.top, width: rect.width, height: rect.height };
                }
                var parentEl = el.__ScreenElement || el.parentElement || document.querySelector('#app') || document.body;
                var pRect = parentEl.getBoundingClientRect();
                el.style.left = '0px';
                el.style.top = '0px';
                el.style.width = Math.max(0, pRect.width) + 'px';
                el.style.height = Math.max(0, pRect.height) + 'px';
                el.style.display = '';
                el.style.visibility = 'visible';
                el._dc_windowState = 'maximized';
                bringToFront(el);
                return true;

            case SW_RESTORE:
            case SW_SHOWNORMAL:
            case SW_SHOW:
            case SW_SHOWDEFAULT:
            case SW_SHOWNOACTIVATE:
            default:
                // restore or show
                // 如果元素有 _dc_doRestore 方法且当前是最大化状态，直接调用它
                if (el._dc_windowState === 'maximized' && typeof el._dc_doRestore === 'function') {
                    el._dc_doRestore();
                } else if (el._dc_windowState === 'maximized' && el._dc_prevBounds) {
                    // 否则使用原来的逻辑
                    var b = el._dc_prevBounds;
                    el.style.left = (b.left || 0) + 'px';
                    el.style.top = (b.top || 0) + 'px';
                    if (b.width != null) el.style.width = b.width + 'px';
                    if (b.height != null) el.style.height = b.height + 'px';
                }

                // if currently hidden, make visible; otherwise leave display as-is
                if (el.style.display === 'none' || window.getComputedStyle && window.getComputedStyle(el).display === 'none') {
                    el.style.display = '';
                }

                el.style.visibility = 'visible';
                el._dc_windowState = 'normal';
                bringToFront(el);
                // try focus if appropriate
                if (typeof el.focus === 'function') el.focus();
                return true;
        }
        return false;
    };
    window.__DCWin32API.ShowWindow = ShowWindow;

    /**
        * 获得元素的边界矩形,模拟 Win32 API bool GetWindowRect(int, RECT*)
        * @param {number | HTMLElement} elementOrHandle
        * @returns {object} 返回元素的边界矩形对象，包含 Left,Top,Width,Height,ClientWidth,ClientHeight 属性
        */
    function GetWindowBounds(elementOrHandle) {
        var htmlElement = DCGetControlByHandle(elementOrHandle);
        if (htmlElement == null) {
            return null;
        }
        try {

            var rect = htmlElement.getBoundingClientRect();
            var left = 0, top = 0, width = 0, height = 0;
            if (rect) {
                left = Math.round(rect.left);
                top = Math.round(rect.top);
                width = Math.round(rect.width);
                height = Math.round(rect.height);
            } else {
                // Fallback using offset metrics
                left = htmlElement.offsetLeft || 0;
                top = htmlElement.offsetTop || 0;
                width = htmlElement.offsetWidth || 0;
                height = htmlElement.offsetHeight || 0;
            }
            if (htmlElement.offsetParent != null) {
                var rect2 = htmlElement.offsetParent.getBoundingClientRect();
                if (rect2) {
                    left -= Math.round(rect2.left);
                    top -= Math.round(rect2.top);
                }
            }
            var clientWidth = 0, clientHeight = 0;
            if (typeof (htmlElement.__GetClientSize) == "function") {
                var cs = htmlElement.__GetClientSize.call(htmlElement);
                clientWidth = cs.Width;
                clientHeight = cs.Height;
            }
            else {
                var childContainer = GetChildContainer(htmlElement);
                if (childContainer != null) {
                    clientWidth = childContainer.clientWidth || 0;
                    clientHeight = childContainer.clientHeight || 0;
                }
                else {
                    clientWidth = htmlElement.clientWidth || 0;
                    clientHeight = htmlElement.clientHeight || 0;
                }
            }
            return {
                Left: left,
                Top: top,
                Width: width,
                Height: height,
                ClientWidth: clientWidth,
                ClientHeight: clientHeight
            }
        }
        catch (ext) {
            console.log(ext);
            throw ext;
        }

    }
    window.__DCWin32API.GetWindowBounds = GetWindowBounds;
/**
    * 判断是否为窗体元素,模拟 Win32 API bool IsWindow(int)
    * @param {number | HTMLElement} elementOrHandle 控件句柄或者HTML元素
    * @returns {boolean} 如果是窗体元素则返回 true，否则返回 false
    */
function IsWindow(elementOrHandle) {
    var htmlElement = DCGetControlByHandle(elementOrHandle);
    if (htmlElement == null) return false;
    return true;
}
window.__DCWin32API.IsWindow = IsWindow;
/**
    * 判断子窗体元素,模拟 Win32 API bool IsChild(int parent, int child)
    * @param {number | HTMLElemen} parentElementOrHandle
    * @param {number | HTMLElemen} childElementOrHandle
    * @returns {boolean} 如果 childElementOrHandle 是 parentElementOrHandle 的子元素则返回 true，否则返回 false
    */
function IsChild(parentElementOrHandle, childElementOrHandle) {
    var parentEl = DCGetControlByHandle(parentElementOrHandle);
    var childEl = DCGetControlByHandle(childElementOrHandle);
    if (!parentEl || !childEl) return false;
    if (parentEl === childEl) return false; // Win32 IsChild: child must be a descendant, not the same window
    // Fallback manual traversal
    for (var n = childEl.parentElement; n != null; n = n.parentElement) {
        if (n === parentEl) return true;
    }
    return false;
}
window.__DCWin32API.IsChild = IsChild;
/**
    * 设置父窗体元素,模拟 Win32 API void SetParent(int child, int parent)
    * @param {number | HTMLElement } childElementOrHandle 子窗体元素或者句柄
    * @param {number | HTMLElement } parentElementOrHandle 父窗体元素或者句柄
    * @param {number} 返回值 执行结果，0表示失败，非0表示成功
    */
function SetParent(childElementOrHandle, parentElementOrHandle) {
    var childEl = DCGetControlByHandle(childElementOrHandle);
    var parentEl = DCGetControlByHandle(parentElementOrHandle);
    if (!childEl || !parentEl) return 0;
    var childContainer = GetChildContainer(parentEl);
    if (childEl.parentNode == childContainer) {
        return 1;
    }
    childContainer.appendChild(childEl);
    return 1;
}
window.__DCWin32API.SetParent = SetParent;
/**
    * 查找父窗体元素,模拟 Win32 API int GetParent(int)
    * @param {number | HTMLElement} elementOrHandle 控件句柄或者HTML元素
    * @returns {number} 返回父窗体元素的句柄，如果没有父窗体则返回 0
    */
function GetParent(elementOrHandle) {
    var htmlElement = DCGetControlByHandle(elementOrHandle);
    if (htmlElement == null) {
        return null;
    }
    for (var pe = htmlElement.parentNode; pe != null && pe.getAttribute; pe = pe.parentNode) {
        var handle = pe.getAttribute('dchandle');
        if (handle != null && handle.length > 0 && isNaN(handle) == false) {
            return parseInt(handle);
        }
    }
    return 0;
}
window.__DCWin32API.GetParent = GetParent;

function GetParentControl(elementOrHandle) {
    var element = DCGetControlByHandle(elementOrHandle);
    if (element != null) {
        var p = element.parentNode;
        while (p != null) {
            if (p.getAttribute && p.getAttribute("dchandle") != null) {
                return p;
            }
            p = p.parentNode;
        }
    }
    return null;
}
window.__DCWin32API.GetParentControl = GetParentControl;
/**
    *  创建窗体对象，本函数模拟 Win32 API CreateWindowEx
    * @param {number} args.Handle 窗体句柄,依据此值调用DCGetControlByHandle()
    * @param {string} args.ControlTypeName 窗体类型
    * @param {number} args.Left 窗体左上角X坐标
    * @param {number} args.Top 窗体左上角Y坐标
    * @param {number} args.Width 窗体宽度
    * @param {number} args.Height 窓体高度
    * @param {number} args.Style 窗体样式，模拟 Win32 窗体样式值
    * @param {number} args.ExStyle 窗体扩展样式，模拟 Win32 窗体扩展样式
    * @param {number} args.ParentHandle 父窗口句柄
    * @param {string} args.Param 窗体参数
    * @returns { HTMLElement} 返回创建的窗体HTML元素
    */
window.__DCWin32API.CreateWindowEx = function (args) {
    if (args == null) return null;
    var typeName = args.ControlTypeName;
    if (typeName == null || typeName.length === 0) {
        throw new Error("CreateWindowEx: ControlTypeName is required.");
    }
    var handle = args.Handle;
    if (handle == null || handle.length) {
        throw new Error("CreateWindowEx: Handle is required.");
    }
    var existElement = DCGetControlByHandle(handle);
    if (existElement != null) {
        existElement.parentNode.removeChild(existElement);
    }
    var factory = window.__DCControlTypes[typeName];
    if (factory == null && args.BaseControlTypeName != null) {
        var baseTypes = args.BaseControlTypeName.split('#');
        for (var baseType of baseTypes) {
            factory = window.__DCControlTypes[baseType];
            if (factory != null) {
                break;
            }
        }
    }
    if (factory == null) {
        throw new Error("CreateWindowEx: ControlTypeName '" + typeName + "' not found in __DCControlTypes.");
    }
    args.Win32API = window.__DCWin32API;
    if (typeof (factory.Create) != "function") {
        throw typeName + " js error!";
    }
    try {

         // 在创建之前检查，如果是 ToolStripComboBoxControl 则直接返回 null
            if (typeName === "System.Windows.Forms.ToolStripComboBox+ToolStripComboBoxControl") {
                //暂时屏蔽这个元素
                return null;
            }
        
            
            var element = factory.Create(args);
        // Final fallback: generic div
        if (!element) {
            element = document.createElement('div');
        }
        if (args.BaseControlTypeName != null) {
            element.setAttribute('dcbasecontroltypename', args.BaseControlTypeName);
        }
        // ensure element has dctype and dchandle
        element.setAttribute('dctype', typeName);
        element.setAttribute('dchandle', handle);
        if (args.DCName) {
            element.setAttribute("dcname", args.DCName);
        }
        element.__DCFactory = factory;
        factory.ApplyOptions.call(factory, element, args);

        // apply Style and ExStyle flags
        var styleFlags = args.Style || 0;
        var exStyle = args.ExStyle || 0;
        // common WS_* flags
        var WS_VISIBLE = 0x10000000;
        var WS_CHILD = 0x40000000;
        var WS_DISABLED = 0x08000000;
        var WS_VSCROLL = 0x00200000;
        var WS_HSCROLL = 0x00100000;
        var WS_BORDER = 0x00800000;
        var WS_DLGFRAME = 0x00400000;
        var WS_CAPTION = 0x00C00000; // BORDER|DLGFRAME

        // ex styles
        var WS_EX_TOPMOST = 0x00000008;
        var WS_EX_TRANSPARENT = 0x00000020;
        var WS_EX_WINDOWEDGE = 0x00000100;
        var WS_EX_CLIENTEDGE = 0x00000200;

        // visibility
        if (styleFlags & WS_VISIBLE) {
            element.style.display = element.style.display || '';
        } else {
            element.style.display = 'none';
        }

        // disabled
        if (styleFlags & WS_DISABLED) {
            element.style.pointerEvents = 'none';
            element.style.opacity = '0.6';
            element.setAttribute('aria-disabled', 'true');
        }

        // border / caption
        if (styleFlags & WS_BORDER) {
            element.style.border = element.style.border || '1px solid #000';
        }
        if (styleFlags & WS_DLGFRAME || styleFlags & WS_CAPTION) {
            element.style.border = element.style.border || '2px groove #ccc';
        }

        // scrollbars
        if (styleFlags & WS_VSCROLL) element.style.overflowY = 'auto';
        if (styleFlags & WS_HSCROLL) element.style.overflowX = 'auto';

        // ex style: topmost
        if (exStyle & WS_EX_TOPMOST) element.style.zIndex = 9999;
        if (exStyle & WS_EX_TRANSPARENT) element.style.pointerEvents = 'none';
        if (exStyle & WS_EX_WINDOWEDGE) element.style.boxShadow = element.style.boxShadow || '0 0 4px rgba(0,0,0,0.1)';
        if (exStyle & WS_EX_CLIENTEDGE) element.style.border = element.style.border || '1px solid rgba(0,0,0,0.1)';

        // Attach to parent
        var parentEl = DCGetControlByHandle(args.ParentHandle);
        if (parentEl != null) {
            SetParent(element, parentEl);
        }
        DCRegisterControl(handle, element);
    }
    catch (e) {
        console.log(e);
        throw e;
    }
};

/**
    * 设置控件的字体
    * @param {number| HTMLElement} elementOrHandle 控件句柄或者HTML元素
    * @param {string} jsonFont.Name 字体名称
    * @param {number} jsonFont.Size 字体大小
    * @param {boolean} jsonFont.Bold 是否粗体，默认false.
    * @param {boolean} jsonFont.Italic 是否斜体，默认false.
    * @param {boolean} jsonFont.Underline 是否下划线，默认false.
    * @param {boolean} jsonFont.Strikeout 是否删除线，默认false.
    * @param {string } jsonFont.Unit 字体单位, 采用System.Drawing.GraphicsUnit枚举值,默认为Point
    * @returns {boolean} 操作是否成功
    * @returns
    */
function SetControlFont(elementOrHandle, jsonFont) {
    if (elementOrHandle == null) return false;
    var htmlElement = DCGetControlByHandle(elementOrHandle);
    if (!htmlElement) return false;
    if (jsonFont == null) return false;
    if (typeof (jsonFont) == "string") {
        htmlElement.style.font = jsonFont;
        return true;
    }
    // font-family
    if (jsonFont.Name != null) htmlElement.style.fontFamily = jsonFont.Name;
    // determine font size in pixels. Default unit is Point (1pt = 96/72 px)
    var size = parseFloat(jsonFont.Size);
    var unit = (jsonFont.Unit || "Point").toString();
    var px = null;
    if (!isNaN(size)) {
        switch (unit.toLowerCase()) {
            case "pixel":
            case "px":
                px = size;
                break;
            case "point":
            case "pt":
                px = size * (96.0 / 72.0);
                break;
            case "inch":
            case "in":
                px = size * 96.0;
                break;
            case "millimeter":
            case "mm":
                px = size * (96.0 / 25.4);
                break;
            case "centimeter":
            case "cm":
                px = size * (96.0 / 2.54);
                break;
            default:
                // fallback to treating as points
                px = size * (96.0 / 72.0);
                break;
        }
    }
    if (px != null && !isNaN(px)) htmlElement.style.fontSize = px + "px";

    // weight and style
    if (jsonFont.Bold === true) htmlElement.style.fontWeight = 'bold';
    else htmlElement.style.fontWeight = 'normal';

    if (jsonFont.Italic === true) htmlElement.style.fontStyle = 'italic';
    else htmlElement.style.fontStyle = 'normal';

    // text decoration: underline and strikeout (line-through)
    var decorations = [];
    if (jsonFont.Underline === true) decorations.push('underline');
    if (jsonFont.Strikeout === true) decorations.push('line-through');
    if (decorations.length > 0) htmlElement.style.textDecoration = decorations.join(' ');
    else htmlElement.style.textDecoration = 'none';
    return true;
};
window.__DCWin32API.SetControlFont = SetControlFont;
// 设置控件的鼠标指针样式
function SetControlCursor(intHandle, strCursor) {
    var htmlElement = DCGetControlByHandle(intHandle);
    if (htmlElement != null) {
        var fact = htmlElement.__DCFactory;
        if (fact != null && fact.GetChildHostElement) {
            var ch = fact.GetChildHostElement.call(fact, htmlElement);
            if (ch != null) {
                ch.style.cursor = strCursor;
                return true;
            }
        }
        htmlElement.style.cursor = strCursor;
        return true;
    }
    else {
        return false;
    }
}
window.__DCWin32API.SetControlCursor = SetControlCursor;
// 模拟 Win32 API SetWindowPos
window.__DCWin32API.SetWindowPos = function (handle, args) {
    const HWND_TOP = 0;
    const HWND_BOTTOM = 1;
    const HWND_TOPMOST = -1;
    const HWND_NOTOPMOST = -2;
    const HWND_MESSAGE = -3;

    const SWP_NOSIZE = 0x0001;
    const SWP_NOMOVE = 0x0002;
    const SWP_NOZORDER = 0x0004;
    const SWP_NOACTIVATE = 0x0010;
    const SWP_SHOWWINDOW = 0x0040;
    const SWP_HIDEWINDOW = 0x0080;
    const SWP_DRAWFRAME = 0x0020;
    const SWP_NOOWNERZORDER = 0x0200;

    if (args == null) return false;
    var htmlElement = DCGetControlByHandle(handle);
    if (!htmlElement) return false;
    function SendMessageDirect(htmlElement, intMsg, intWParam, intLParam) {
        var intHandle = DCGetControlHandle(htmlElement);
        if (intHandle >= 0) {
            __DCExecuteControlCommand(
                intHandle,
                "SendMessageDirect",
                {
                    Msg: intMsg,
                    WParam: intWParam,
                    LParam: intLParam
                });
        }
    }
    // 确保元素是可定位的
    var computed = getComputedStyle(htmlElement);
    if (!computed || computed.position === 'static') {
        htmlElement.style.position = 'absolute';
    }

    var flags = args.Flags || 0;

    // Notify: WM_WINDOWPOSCHANGING before applying changes
    var WM_WINDOWPOSCHANGING = 0x0046;
    // We cannot pass pointer to WINDOWPOS; send flags as wParam and 0 lParam
    SendMessageToControl(htmlElement, WM_WINDOWPOSCHANGING, flags | 0, 0);

    // Cache previous bounds to detect changes
    var prevLeft = 0, prevTop = 0, prevWidth = 0, prevHeight = 0;
    prevLeft = parseInt(htmlElement.style.left) || 0;
    prevTop = parseInt(htmlElement.style.top) || 0;
    prevWidth = parseInt(htmlElement.style.width) || htmlElement.offsetWidth || 0;
    prevHeight = parseInt(htmlElement.style.height) || htmlElement.offsetHeight || 0;

    // 移动（除非指定 SWP_NOMOVE）
    if (!(flags & SWP_NOMOVE)) {
        if (!isNaN(args.Left)) htmlElement.style.left = args.Left + 'px';
        if (!isNaN(args.Top)) htmlElement.style.top = args.Top + 'px';
    }

    // 调整大小（除非指定 SWP_NOSIZE）
    if (!(flags & SWP_NOSIZE)) {
        //if (htmlElement.getAttribute("dctype") == "System.Windows.Forms.TextBox" && args.Width < 190) {
        //    debugger;
        //}
        if (!isNaN(args.Width)) {
            htmlElement.style.width = args.Width + 'px';
            if (htmlElement.nodeName == "CANVAS") {
                htmlElement.width = args.Width;
            }
        }
        if (!isNaN(args.Height)) {
            htmlElement.style.height = args.Height + 'px';
            if (htmlElement.nodeName == "CANVAS") {
                htmlElement.height = args.Height;
            }
        }
        if (htmlElement.DoChildLayout) {
            htmlElement.DoChildLayout.call(htmlElement);
        }
    }

    // Z-order / InsertAfter（除非指定 SWP_NOZORDER）
    if (!(flags & SWP_NOZORDER)) {
        var ia = args.InsertAfter;
        if (ia === HWND_TOPMOST) {
            htmlElement.style.zIndex = 9999;
        } else if (ia === HWND_NOTOPMOST) {
            htmlElement.style.zIndex = 1;
        } else if (ia === HWND_TOP) {
            htmlElement.style.zIndex = 1000;
        } else if (ia === HWND_BOTTOM) {
            htmlElement.style.zIndex = 0;
        } else if (ia) {
            // 如果 InsertAfter 是一个句柄字符串，尝试把元素移动到目标元素之后
            var target = null;
            if (typeof ia === 'string') target = document.querySelector("[dchandle='" + ia + "']") || document.querySelector(ia);
            else if (ia instanceof Element) target = ia;

            if (target && target.parentNode === htmlElement.parentNode) {
                target.parentNode.insertBefore(htmlElement, target.nextSibling);
            }
        }
    }

    // 显示/隐藏
    if (flags & SWP_SHOWWINDOW) {
        htmlElement.style.display = '';
        htmlElement.style.visibility = 'visible';
    } else if (flags & SWP_HIDEWINDOW) {
        htmlElement.style.display = 'none';
    }

    // 不改变激活状态（浏览器无法完全模拟 Win32 的激活行为），但保留占位处理
    if (!(flags & SWP_NOACTIVATE)) {
        // 可在此处触发自定义焦点事件或样式
    }


    // Send WM_MOVE if position changed
    var curLeft = parseInt(htmlElement.style.left) || 0;
    var curTop = parseInt(htmlElement.style.top) || 0;
    if (curLeft !== prevLeft || curTop !== prevTop) {
        var WM_MOVE = 0x0003;
        var lParamMove = ((curTop & 0xFFFF) << 16) | (curLeft & 0xFFFF);
        SendMessageToControl(htmlElement, WM_MOVE, 0, lParamMove);
    }

    // Send WM_SIZE if size changed
    var curWidth = parseInt(htmlElement.style.width) || htmlElement.offsetWidth || 0;
    var curHeight = parseInt(htmlElement.style.height) || htmlElement.offsetHeight || 0;
    if (curWidth !== prevWidth || curHeight !== prevHeight) {
        var WM_SIZE = 0x0005;
        // wParam: sizing type (use 0 = SIZE_RESTORED default), lParam: LOWORD=width, HIWORD=height
        var lParamSize = ((curHeight & 0xFFFF) << 16) | (curWidth & 0xFFFF);
        SendMessageToControl(htmlElement, WM_SIZE, 0, lParamSize);
    }
    // Notify: WM_WINDOWPOSCHANGED after applying changes
    var WM_WINDOWPOSCHANGED = 0x0047;
    SendMessageDirect(htmlElement, WM_WINDOWPOSCHANGED, flags | 0, 0);
    // Also send detailed data to .NET side if needed
    __DCExecuteControlCommand(
        DCGetControlHandle(htmlElement),
        "WindowPosChanged",
        {
            Left: parseInt(htmlElement.style.left) || 0,
            Top: parseInt(htmlElement.style.top) || 0,
            Width: parseInt(htmlElement.style.width) || htmlElement.offsetWidth || 0,
            Height: parseInt(htmlElement.style.height) || htmlElement.offsetHeight || 0,
            ZIndex: parseInt(htmlElement.style.zIndex) || 0,
            Flags: flags | 0
        });

    return true;
};

/**
    * 模拟 Win32 API GetWindowLong
    * @param {number|string|HTMLElement} elementOrHandle 控件句柄或元素
    * @param {number} nIndex 索引（例如 GWL_STYLE=-16, GWL_EXSTYLE=-20 等）
    * @returns {number} 返回对应的长整数值
    */
function GetWindowLong(elementOrHandle, nIndex) {
    if (elementOrHandle == null) return 0;
    var el = DCGetControlByHandle(elementOrHandle);
    if (!el) return 0;

    // define constants we may return
    var WS_VISIBLE = 0x10000000;
    var WS_CHILD = 0x40000000;
    var WS_DISABLED = 0x08000000;
    var WS_VSCROLL = 0x00200000;
    var WS_HSCROLL = 0x00100000;
    var WS_BORDER = 0x00800000;
    var WS_DLGFRAME = 0x00400000;
    var WS_CAPTION = 0x00C00000; // BORDER|DLGFRAME

    var WS_EX_TOPMOST = 0x00000008;
    var WS_EX_TRANSPARENT = 0x00000020;
    var WS_EX_WINDOWEDGE = 0x00000100;
    var WS_EX_CLIENTEDGE = 0x00000200;

    // compute visibility
    var computed = window.getComputedStyle(el);
    var isVisible = true;
    if (el.style && el.style.display === 'none') isVisible = false;
    else if (computed && (computed.display === 'none' || computed.visibility === 'hidden')) isVisible = false;

    // compute disabled
    var isDisabled = false;
    if (el.hasAttribute && el.hasAttribute('disabled')) isDisabled = true;
    else if (el.getAttribute && el.getAttribute('aria-disabled') === 'true') isDisabled = true;
    else if (computed && computed.pointerEvents === 'none') isDisabled = true;

    // compute child: consider as child if parent exists and not document/body
    var isChild = false;
    if (el.parentElement && el.parentElement !== document.body && el.parentElement !== document.documentElement) isChild = true;

    // compute border
    var hasBorder = false;
    if (computed) {
        var bw = parseFloat(computed.borderTopWidth || '0') || 0;
        var bs = computed.borderTopStyle || 'none';
        if (bw > 0 && bs !== 'none') hasBorder = true;
    } else if (el.style && el.style.border && el.style.border !== '') hasBorder = true;

    // compute scrollbars
    var hasVScroll = false, hasHScroll = false;
    if (computed) {
        var oy = computed.overflowY || computed.overflow || '';
        var ox = computed.overflowX || computed.overflow || '';
        if (oy === 'auto' || oy === 'scroll') hasVScroll = true;
        if (ox === 'auto' || ox === 'scroll') hasHScroll = true;
    }
    // also check element attributes (e.g., textarea with overflow)
    if (!hasVScroll && (el.scrollHeight && el.clientHeight && el.scrollHeight > el.clientHeight)) hasVScroll = true;
    if (!hasHScroll && (el.scrollWidth && el.clientWidth && el.scrollWidth > el.clientWidth)) hasHScroll = true;

    // compute ex styles
    var exStyleVal = 0;
    var z = parseInt(el.style.zIndex) || 0;
    if (z >= 9999) exStyleVal |= WS_EX_TOPMOST;
    if (computed && computed.pointerEvents === 'none') exStyleVal |= WS_EX_TRANSPARENT;
    // window edge: if boxShadow set
    var bs2 = (computed && computed.boxShadow) || el.style.boxShadow || '';
    if (bs2 && bs2 !== 'none' && bs2.length > 0) exStyleVal |= WS_EX_WINDOWEDGE;
    // client edge: approximate by presence of border and not heavy groove
    if (hasBorder) exStyleVal |= WS_EX_CLIENTEDGE;

    // compute style flags
    var styleVal = 0;
    if (isVisible) styleVal |= WS_VISIBLE;
    if (isChild) styleVal |= WS_CHILD;
    if (isDisabled) styleVal |= WS_DISABLED;
    if (hasVScroll) styleVal |= WS_VSCROLL;
    if (hasHScroll) styleVal |= WS_HSCROLL;
    if (hasBorder) styleVal |= WS_BORDER;
    // caption/dlgframe: if element looks like a Form (has titlebar) or dctype Form
    var isForm = false;
    if (el.getAttribute && (el.getAttribute('dctype') === 'System.Windows.Forms.Form')) isForm = true;
    if (el._dc_titleBar) isForm = true;
    if (isForm) styleVal |= (WS_DLGFRAME | WS_BORDER), styleVal |= WS_CAPTION; // set both

    // determine return based on index
    switch (nIndex) {
        case -16: // GWL_STYLE
            return styleVal | 0;
        case -20: // GWL_EXSTYLE
            return exStyleVal | 0;
        case -12: // GWLP_ID / control id - return numeric handle if available
            return parseInt(el.getAttribute('dchandle')) || 0;
        case -21: // GWL_USERDATA - not supported
        case -4:  // GWL_WNDPROC
        case -8:  // GWL_HWNDPARENT
            return GetParent(elementOrHandle);
        default:
            return 0;
    }
    return 0;
};
window.__DCWin32API.GetWindowLong = GetWindowLong;
/**
    * 模拟win32 API SetWindowLong 
    * @param {number | HTMLElement} elementOrHandle 
    * @param {number} intIndex
    * @param {number} intNewLong
    * @returns {number} 操作结果
    */
window.__DCWin32API.SetWindowLong = function (elementOrHandle, intIndex, intNewLong) {
    var htmlElement = DCGetControlByHandle(elementOrHandle);
    if (!htmlElement) {
        return 0;
    }

    // Known indices
    var GWL_STYLE = -16;
    var GWL_EXSTYLE = -20;

    // Common style flags
    var WS_VISIBLE = 0x10000000;
    var WS_CHILD = 0x40000000; // informational in DOM; no-op
    var WS_DISABLED = 0x08000000;
    var WS_VSCROLL = 0x00200000;
    var WS_HSCROLL = 0x00100000;
    var WS_BORDER = 0x00800000;
    var WS_DLGFRAME = 0x00400000;
    var WS_CAPTION = 0x00C00000; // BORDER|DLGFRAME

    // Extended style flags
    var WS_EX_TOPMOST = 0x00000008;
    var WS_EX_TRANSPARENT = 0x00000020;
    var WS_EX_WINDOWEDGE = 0x00000100;
    var WS_EX_CLIENTEDGE = 0x00000200;

    // Get previous value to return per API contract
    var prev = GetWindowLong(htmlElement, intIndex) | 0;

    switch (intIndex) {
        case GWL_STYLE: {
            var styleVal = intNewLong | 0;

            // Visibility
            if (styleVal & WS_VISIBLE) {
                htmlElement.style.display = '';
                htmlElement.style.visibility = 'visible';
            } else {
                htmlElement.style.display = 'none';
            }

            // Disabled
            if (styleVal & WS_DISABLED) {
                htmlElement.style.pointerEvents = 'none';
                htmlElement.style.opacity = '0.6';
                htmlElement.setAttribute('aria-disabled', 'true');
            } else {
                htmlElement.style.pointerEvents = '';
                htmlElement.style.opacity = '';
                htmlElement.removeAttribute('aria-disabled');
            }

            // Border / caption
            // Reset any previous border set by styles when flags removed
            var hadBorder = (styleVal & WS_BORDER) || (styleVal & WS_DLGFRAME) || (styleVal & WS_CAPTION);
            if (hadBorder) {
                // Prefer caption/dlgframe appearance if set
                if (styleVal & WS_DLGFRAME || styleVal & WS_CAPTION) {
                    if (!htmlElement.style.border || htmlElement.style.border === '') {
                        htmlElement.style.border = '2px groove #ccc';
                    }
                } else if (styleVal & WS_BORDER) {
                    if (!htmlElement.style.border || htmlElement.style.border === '') {
                        htmlElement.style.border = '1px solid #000';
                    }
                }
            } else {
                // If no border flags, don't enforce a border (leave existing manual styles as-is)
                // Avoid clearing developer-set border unintentionally
            }

            // Scrollbars
            if (styleVal & WS_VSCROLL) {
                htmlElement.style.overflowY = 'auto';
            } else {
                // Only clear if it was previously set to auto/scroll by our API
                if (htmlElement.style.overflowY === 'auto' || htmlElement.style.overflowY === 'scroll') {
                    htmlElement.style.overflowY = '';
                }
            }
            if (styleVal & WS_HSCROLL) {
                htmlElement.style.overflowX = 'auto';
            } else {
                if (htmlElement.style.overflowX === 'auto' || htmlElement.style.overflowX === 'scroll') {
                    htmlElement.style.overflowX = '';
                }
            }

            break;
        }
        case GWL_EXSTYLE: {
            var exVal = intNewLong | 0;

            // Topmost
            if (exVal & WS_EX_TOPMOST) {
                htmlElement.style.zIndex = '9999';
            } else {
                // do not force zIndex too low; clear if previously set by us
                if (htmlElement.style.zIndex === '9999') {
                    htmlElement.style.zIndex = '';
                }
            }

            // Transparent (mouse-through)
            if (exVal & WS_EX_TRANSPARENT) {
                htmlElement.style.pointerEvents = 'none';
            } else {
                // Only clear if it was set by transparency (keep disabled state if any)
                if (htmlElement.getAttribute('aria-disabled') !== 'true') {
                    if (htmlElement.style.pointerEvents === 'none') {
                        htmlElement.style.pointerEvents = '';
                    }
                }
            }

            // Window edge shadow
            if (exVal & WS_EX_WINDOWEDGE) {
                if (!htmlElement.style.boxShadow || htmlElement.style.boxShadow === '') {
                    htmlElement.style.boxShadow = '0 0 4px rgba(0,0,0,0.1)';
                }
            } else {
                // Clear if previously set by us
                if (htmlElement.style.boxShadow === '0 0 4px rgba(0,0,0,0.1)') {
                    htmlElement.style.boxShadow = '';
                }
            }

            // Client edge (light border)
            if (exVal & WS_EX_CLIENTEDGE) {
                if (!htmlElement.style.border || htmlElement.style.border === '') {
                    htmlElement.style.border = '1px solid rgba(0,0,0,0.1)';
                }
            } else {
                // If we set a client-edge border previously, clear it
                if (htmlElement.style.border === '1px solid rgba(0,0,0,0.1)') {
                    htmlElement.style.border = '';
                }
            }

            break;
        }
        default:
            // Unsupported indices: no-op, return previous
            break;
    }

    return prev;
};
window.__DCWin32API.DestroyWindow = function (elementOrHandle) {
    var el = DCGetControlByHandle(elementOrHandle);
    if (!el) return;

    // Disconnect any observers
    if (el.__ResizeObserver) { el.__ResizeObserver.disconnect(); el.__ResizeObserver = null; }

    // Unbind standard event handlers assigned via properties
    el.onkeydown = null;
    el.onkeypress = null;
    el.onkeyup = null;
    el.onmousedown = null;
    el.onmousemove = null;
    el.onmouseup = null;
    el.onwheel = null;
    el.onclick = null;
    el.ondblclick = null;
    el.ondragover = null;
    el.ondrop = null;
    el.onscroll = null;
    el.onfocusin = null;
    el.onpointerdown = null;

    // Remove any custom expando data to avoid leaks
    for (var key in el) {
        if (!Object.prototype.hasOwnProperty.call(el, key)) continue;
        if (key.startsWith('__') || key.startsWith('_dc_')) {
            el[key] = null;
            delete el[key];
        }
    }

    // Clear attributes that we added
    el.removeAttribute('dcbasecontroltypename');
    el.removeAttribute('dctype');
    var handleAttr = el.getAttribute('dchandle');
    el.removeAttribute('dchandle');

    // Remove from DOM
    if (el.parentNode) el.parentNode.removeChild(el);

    // Remove from registered controls map
    if (screenHtmlElement && screenHtmlElement.__DCControls) {
        var h = handleAttr || el.getAttribute && el.getAttribute('dchandle');
        if (h != null) screenHtmlElement.__DCControls.delete(h);
    }
};
window.__DCWin32API.GetClientRect = function (elementOrHandle) {
    var htmlElement = DCGetControlByHandle(elementOrHandle);
    if (!htmlElement) {
        return null;
    }
    htmlElement = GetChildContainer(htmlElement);
    var rect = htmlElement.getBoundingClientRect();
    var cs = window.getComputedStyle(htmlElement);

    // Compute border sizes
    var bt = 0, bb = 0, bl = 0, br = 0;
    if (cs) {
        bt = parseFloat(cs.borderTopWidth || '0') || 0;
        bb = parseFloat(cs.borderBottomWidth || '0') || 0;
        bl = parseFloat(cs.borderLeftWidth || '0') || 0;
        br = parseFloat(cs.borderRightWidth || '0') || 0;
    }

    // Client area left/top inside borders
    var clientLeft = Math.round(rect.left + bl);
    var clientTop = Math.round(rect.top + bt);

    // Prefer element.clientWidth/clientHeight for client size
    var clientWidth = 0, clientHeight = 0;
    clientWidth = htmlElement.clientWidth || 0;
    clientHeight = htmlElement.clientHeight || 0;

    // Fallback compute width/height by subtracting borders if clientWidth not available
    if (!clientWidth || !clientHeight) {
        var w = Math.max(0, rect.width - bl - br);
        var h = Math.max(0, rect.height - bt - bb);
        clientWidth = Math.round(w);
        clientHeight = Math.round(h);
    }
    return clientLeft + "|" + clientTop + "|" + clientWidth + "|" + clientHeight;
};
/** 模拟win32 api GetWindowPlacement()*/
window.__DCWin32API.GetWindowPlacement = function (elementOrHandle) {
    var ele = DCGetControlByHandle(elementOrHandle);
    if (ele == null || ele.GetWindowPlacement == null) return 0;
    return ele.GetWindowPlacement.call(ele);
};
}) ();

