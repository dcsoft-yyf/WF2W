/** 使用HTML 元素模拟 System.Windows.Forms.ScrollableControl 控件 */
"use strict";
class SystemWindowsFormsScrollableControlFactory extends SystemWindowsFormsControlFactory {
    /**
     * 根据选项创建 HTML 元素模拟 ScrollableControl
     * @param {any} options 控件选项
     * @returns {HTMLElement}
     */
    Create(options) {
        if (!options) {
            throw new Error("Options parameter is required to create a ScrollableControl.");
        }
        var rootElement = document.createElement("div");
        rootElement.setAttribute("dchandle", options.Handle);
        rootElement.setAttribute("dctype", "System.Windows.Forms.ScrollableControl");
        rootElement.style.position = "absolute";
        rootElement.style.display = "block";
        rootElement.style.boxSizing = "border-box";
        rootElement.style.overflow = "hidden";
        rootElement.__autoScroll = false;
        rootElement.__childHost = null;
        rootElement.__canvas = null;
        rootElement.__resizeObserver = null;

        rootElement.__SetWindowText = function (txt) {

        };
        rootElement.__GetWindowText = function () {
            return null;
        };


        if (options.UserPaint === true) {
            rootElement.__childHost = document.createElement("div");
            rootElement.__childHost.style.position = "absolute";
            rootElement.__childHost.style.left = "0";
            rootElement.__childHost.style.top = "0";
            rootElement.__childHost.style.width = "100%";
            rootElement.__childHost.style.height = "100%";
            rootElement.__childHost.style.boxSizing = "border-box";
            rootElement.__childHost.style.overflow = "inherit";
            rootElement.__childHost.setAttribute("data-scroll-host", "1");

            rootElement.__canvas = document.createElement("canvas");
            rootElement.__canvas.style.position = "absolute";
            rootElement.__canvas.style.left = "0";
            rootElement.__canvas.style.top = "0";
            rootElement.__canvas.style.width = "100%";
            rootElement.__canvas.style.height = "100%";
            rootElement.__canvas.style.zIndex = "0";
            rootElement.__canvas.style.pointerEvents = "none";
            rootElement.__canvas.setAttribute("data-scroll-canvas", "1");

            rootElement.appendChild(rootElement.__canvas);
            rootElement.appendChild(rootElement.__childHost);

            rootElement.__GetChildContainer = function () {
                return this.__childHost;
            };
            rootElement.__GetClientSize = function () {
                return {
                    Width: this.__childHost.clientWidth,
                    Height: this.__childHost.clientHeight
                };
            };
            rootElement.DoChildLayout = function () {
                if (this.parentNode == null) return;
                var clientW = this.clientWidth;
                var clientH = this.clientHeight;
                var canvasW = clientW;
                var canvasH = clientH;
                if (clientW > 0 && clientH > 0) {
                    if (this.__childHost) {
                        this.__childHost.style.width = clientW + "px";
                        this.__childHost.style.height = clientH + "px";
                        canvasW = this.__childHost.clientWidth;
                        canvasH = this.__childHost.clientHeight;
                    }
                    if (this.__canvas) {
                        if (this.__canvas.width != canvasW || this.__canvas.height != canvasH) {
                            this.__canvas.width = canvasW;
                            this.__canvas.height = canvasH;
                            this.__canvas.style.width = canvasW + "px";
                            this.__canvas.style.height = canvasH + "px";
                            var task4 = {
                                Handle: parseInt(this.getAttribute("dchandle")),
                                Name: "InvalidateAll" + this.getAttribute("dchandle"),
                                Eat: function (item) {
                                    return this.Name == item.Name;
                                },
                                Run: function () {
                                    __DCWin32API.SendMessageToControl(
                                        this.Handle,
                                        15,
                                        0,
                                        0,
                                        null,
                                        {
                                            Left: -1,
                                            Top: -1,
                                            Width: -1,
                                            Height: -1
                                        }
                                    );
                                }
                            };
                            __DCWin32API.AddTask(task4);
                        }
                    }
                }
            };
            if (options.Win32API && typeof options.Win32API.BindingStandardControlEvent === "function") {
                options.Win32API.BindingStandardControlEvent(rootElement.__childHost, "mousedown|mousemove|mouseup|keydown|keypress|keyup");
            }
            if (typeof ResizeObserver !== "undefined") {
                rootElement.__resizeObserver = new ResizeObserver(() => {
                    rootElement.DoChildLayout.call(rootElement);
                });
                rootElement.__resizeObserver.observe(rootElement);
            }


            rootElement.__lastScrollLeft = rootElement.__childHost.scrollLeft | 0;
            rootElement.__lastScrollTop = rootElement.__childHost.scrollTop | 0;
            function DoScroll(e)
            {
                var task3 = {
                    DelayTick : 20,
                    Name: "ScrollableControl_OnScroll",
                    Element: rootElement,
                    Eat: function (item) {
                        return this.Name == item.Name && this.Element == item.Element;
                    },
                    Run: async function () {
                        this.Element.__childHost.removeEventListener("scroll", DoScroll);
                        try {
                            var bolHasScrollAnsyc = __DCExecuteControlCommand(
                                parseInt(this.Element.getAttribute("dchandle")),
                                "SetScrollPosition",
                                {
                                    Left: - this.Element.__childHost.scrollLeft,
                                    Top: - this.Element.__childHost.scrollTop,
                                    Width: this.Element.__childHost.scrollWidth,
                                    Height: this.Element.__childHost.scrollHeight
                                });
                            var canvas = this.Element.__canvas;
                            if (!canvas || !canvas.getContext) return;
                            var ctx = canvas.getContext("2d");
                            if (!ctx) return;

                            var w = canvas.width;
                            var h = canvas.height;
                            var newLeft = this.Element.__childHost.scrollLeft | 0;
                            var newTop = this.Element.__childHost.scrollTop | 0;
                            var dx = -(newLeft - (this.Element.__lastScrollLeft || 0)) | 0;
                            var dy = -(newTop - (this.Element.__lastScrollTop || 0)) | 0;
                            if (dx === 0 && dy === 0) return;

                            // Move existing content in scroll direction
                            ctx.save();
                            ctx.resetTransform();
                            ctx.globalCompositeOperation = "copy";
                            ctx.drawImage(canvas, dx, dy);
                            ctx.restore();

                            // Compute invalid rectangles (areas newly exposed)
                            var invalidRects = [];
                            if (dx > 0) invalidRects.push({ x: 0, y: 0, width: dx, height: h });
                            else if (dx < 0) invalidRects.push({ x: w + dx, y: 0, width: -dx, height: h });
                            if (dy > 0) invalidRects.push({ x: 0, y: 0, width: w, height: dy });
                            else if (dy < 0) invalidRects.push({ x: 0, y: h + dy, width: w, height: -dy });

                            //for (var i = 0; i < invalidRects.length; i++) {
                            //    var r = invalidRects[i];
                            //    if (r.width > 0 && r.height > 0) {
                            //        //ctx.fillStyle = "red";
                            //        //ctx.fillRect(r.x, r.y, r.width, r.height);
                            //        //ctx.clearRect(r.x, r.y, r.width, r.height);
                            //    }
                            //}
                            for (var i = 0; i < invalidRects.length; i++) {
                                var r = invalidRects[i];
                                if (r.width > 0 && r.height > 0) {
                                    //async function draw3() {
                                    //ctx.save();
                                    //ctx.fillStyle = "blue";
                                    //ctx.fillRect(r.x, r.y, r.width, r.height);
                                    //ctx.restore();
                                    //}
                                    //draw3();
                                    //ctx.clearRect(r.x, r.y, r.width, r.height);
                                    await __DCWin32API.SendMessageToControl(
                                        parseInt(this.Element.getAttribute("dchandle")),
                                        15,
                                        0,
                                        0,
                                        null,
                                        {
                                            Left: r.x - 2,
                                            Top: r.y - 2,
                                            Width: r.width + 4,
                                            Height: r.height + 4
                                        }
                                    );
                                    //var bs = __DCInvokeCSMethod(
                                    //    "GetControlPaintData",
                                    //    parseInt(this.Element.getAttribute("dchandle")),
                                    //    r.x - 2,
                                    //    r.y -2 ,
                                    //    r.width + 4,
                                    //    r.height + 4);
                                    //if (bs != null && bs.length > 0) {
                                    //    await __DCPaintControlContent(this.Element, bs);
                                    //}
                                    //console.log("scroll " + dx);
                                    //__DCExecuteControlCommand(parseInt(
                                    //    rootElement.getAttribute("dchandle")),
                                    //    "RedrawWindow",
                                    //    {
                                    //        Left: r.x-1,
                                    //        Top: r.y-1,
                                    //        Width: r.width+2,
                                    //        Height: r.height+2
                                    //    });
                                }
                            }
                            this.Element.__lastScrollLeft = newLeft;
                            this.Element.__lastScrollTop = newTop;
                            //__DCExecuteControlCommandAsync(
                            //    parseInt(rootElement.getAttribute("dchandle")),
                            //    "RaiseEventScrollAsync",
                            //    0);
                        }
                        finally {
                            this.Element.__childHost.addEventListener("scroll", DoScroll);
                        }
                    }
                    
                };
                __DCWin32API.AddTask(task3);
                
            }
            rootElement.__childHost.addEventListener("scroll", DoScroll);
        }
        else {
            rootElement.DoChildLayout = function () { };
            rootElement.__GetChildContainer = function () {
                return this;
            };
            rootElement.__GetClientSize = function () {
                return { Width: this.clientWidth, Height: this.clientHeight };
            };
            if (options.Win32API && typeof options.Win32API.BindingStandardControlEvent === "function") {
                options.Win32API.BindingStandardControlEvent(rootElement, "scroll|mousedown|mousemove|mouseup|keydown|keypress|keyup");
            }
        }
        rootElement.DoChildLayout.call(rootElement);
        rootElement.DisposeElement = function () {
            window.__DCControlTypes["System.Windows.Forms.ScrollableControl"].DisposeElement(this);
        };
        return rootElement;
    }
    GetClientSize(rootElement) {
        if (rootElement.__childHost != null) {
            return rootElement.__childHost.clientWidth + "|" + rootElement.__childHost.clientHeight;
        }
        else {
            return rootElement.clientWidth + "|" + rootElement.clientHeight;
        }
    }
    /**
     * 销毁控件元素，释放资源，删除所有的事件绑定
     * @param {HTMLElement} htmlElement
     */
    DisposeElement(htmlElement) {
        if (!htmlElement) return;
        // 解绑事件
        if (window.__DCWin32API && typeof window.__DCWin32API.ClearStandardControlEvent === "function") {
            window.__DCWin32API.ClearStandardControlEvent(htmlElement);
            if (htmlElement.__childHost) {
                window.__DCWin32API.ClearStandardControlEvent(htmlElement.__childHost);
            }
        }

        // 断开滚动监听（如果有存储引用可扩展，这里清理子宿主上的全部标准事件）
        // 清理 ResizeObserver
        if (htmlElement.__resizeObserver && typeof htmlElement.__resizeObserver.disconnect === "function") {
            htmlElement.__resizeObserver.disconnect();
        }

        //// 移除子元素
        //if (htmlElement.__childHost && htmlElement.contains(htmlElement.__childHost)) {
        //    htmlElement.removeChild(htmlElement.__childHost);
        //}
        //if (htmlElement.__canvas && htmlElement.contains(htmlElement.__canvas)) {
        //    htmlElement.removeChild(htmlElement.__canvas);
        //}

        // 清理引用
        htmlElement.__childHost = null;
        htmlElement.__canvas = null;
        htmlElement.__resizeObserver = null;
        htmlElement.__GetChildContainer = null;
        htmlElement.__lastScrollLeft = null;
        htmlElement.__lastScrollTop = null;
        super.DisposeElement(htmlElement);
    }
    GetDisplayRectangle(rootElement) {
        if (rootElement != null) {
            var ch = rootElement.__childHost == null ? rootElement : rootElement.__childHost;
            if (ch != null) {
                return ch.scrollLeft + "|" + ch.scrollTop + "|" + ch.clientWidth + "|" + ch.clientHeight;
            }
        }
        return null;
    }
    SetScrollPosition(rootElement, intX, intY) {
        if (rootElement != null) {
            var ch = rootElement.__childHost == null ? rootElement: rootElement.__childHost;
            if (ch != null) {
                ch.scrollTo({ left: intX, top: intY, behavior: "instant" });
                return true;
            }
        }
        return false;
    }
    GetChildHostElement(rootElement) {
        if (rootElement.__childHost != null) {
            return rootElement.__childHost;
        }
        else {
            return rootElement;
        }
    }
    /**
     * 设置控件属性
     */
    ApplyOptions(htmlElement, options) {
        if (!htmlElement || !options) return;
        super.ApplyOptions.call(this, htmlElement, options);
        htmlElement.DoChildLayout.call(htmlElement);
    }

    /**
     * 设置属性值
     */
    SetPropertyValue(rootElement, strPropertyName, strPropertyValue, options) {
        if (!rootElement) return false;
        switch (strPropertyName) {
            case "AutoScroll":
                rootElement.__autoScroll = !!strPropertyValue;
                rootElement.style.overflow = rootElement.__autoScroll ? "auto" : "hidden";
                if (rootElement.__childHost) {
                    rootElement.__childHost.style.overflow = rootElement.__childHost.style.overflow || rootElement.style.overflow;
                    rootElement.DoChildLayout.call(rootElement);
                }
                return true;
            case "AutoScrollMargin":
                if (strPropertyValue && typeof strPropertyValue === "object") {
                    var paddingValue = (strPropertyValue.Top || 0) + "px " + (strPropertyValue.Right || 0) + "px " + (strPropertyValue.Bottom || 0) + "px " + (strPropertyValue.Left || 0) + "px";
                    rootElement.style.padding = paddingValue;
                    if (rootElement.__childHost) rootElement.__childHost.style.padding = paddingValue;
                    rootElement.DoChildLayout.call(rootElement);
                }
                return true;
            case "AutoScrollMinSize":
                if (strPropertyValue && typeof strPropertyValue === "object") {
                    var w = strPropertyValue.Width || 0;
                    var h = strPropertyValue.Height || 0;
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
                    if (rootElement.__childHost) {
                        if (w == 0 && h == 0 && rootElement.__childHost.__scrollSpacer == 0) {
                            return true;
                        }
                        var spacer = ensureScrollSpacer(rootElement.__childHost);
                        if (spacer != null) {
                            spacer.style.width = w + "px";
                            spacer.style.height = h + "px";
                        }
                        rootElement.DoChildLayout.call(rootElement);
                    }
                }
                return true;
            case "Padding":
                if (typeof strPropertyValue === "object") {
                    var p = strPropertyValue;
                    var paddingValue2 = (p.Top || 0) + "px " + (p.Right || 0) + "px " + (p.Bottom || 0) + "px " + (p.Left || 0) + "px";
                    rootElement.style.padding = paddingValue2;
                    if (rootElement.__childHost) rootElement.__childHost.style.padding = paddingValue2;
                } else {
                    rootElement.style.padding = strPropertyValue;
                    if (rootElement.__childHost) rootElement.__childHost.style.padding = strPropertyValue;
                }
                rootElement.DoChildLayout.call(rootElement);
                return true;
            case "HorizontalScroll":
                if (strPropertyValue && typeof strPropertyValue === "object") {
                    if (strPropertyValue.Value != null) {
                        rootElement.scrollLeft = strPropertyValue.Value;
                        if (rootElement.__childHost) rootElement.__childHost.scrollLeft = strPropertyValue.Value;
                    }
                }
                return true;
            case "VerticalScroll":
                if (strPropertyValue && typeof strPropertyValue === "object") {
                    if (strPropertyValue.Value != null) {
                        rootElement.scrollTop = strPropertyValue.Value;
                        if (rootElement.__childHost) rootElement.__childHost.scrollTop = strPropertyValue.Value;
                    }
                }
                return true;
            case "Width":
                rootElement.style.width = strPropertyValue + "px";
                rootElement.DoChildLayout.call(rootElement);
                return true;
            case "Height":
                rootElement.style.height = strPropertyValue + "px";
                rootElement.DoChildLayout.call(rootElement);
                return true;
            case "Text": return true;
            default:
                var handled = super.SetPropertyValue.call(this, rootElement, strPropertyName, strPropertyValue);
                //if (handled && (
                //    strPropertyName === "Width"
                //    || strPropertyName === "Height"
                //    || strPropertyName === "Left"
                //    || strPropertyName === "Top")) {
                //    element.DoChildLayout.call(element);
                //}
                return handled;
        }
    }
};
if (window.__DCControlTypes == null) window.__DCControlTypes = new Object();
window.__DCControlTypes["System.Windows.Forms.ScrollableControl"] = new SystemWindowsFormsScrollableControlFactory();