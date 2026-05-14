/** 使用HTML 元素模拟 System.Windows.Forms.ToolStrip 控件 */
"use strict";
class SystemWindowsFormsToolStripFactory extends SystemWindowsFormsControlFactory {
    /**
        * 根据选项来创建HTML元素来模拟 ToolStrip 控件
        * @param {any} options 控件选项，属性请参考 System.Windows.Forms.ToolStrip 类,必定包含一个Handle属性。
        * @returns { HTMLElement} 返回创建的 HTML 元素
        */
    Create(options) {
        if (!options) {
            throw new Error("Options parameter is required to create a ToolStrip control.");
        }

        var rootElement = document.createElement("div");
        rootElement.setAttribute("dchandle", options.Handle);
        rootElement.setAttribute("dctype", "System.Windows.Forms.ToolStrip");
        rootElement.setAttribute("role", "toolbar");

        // 基础样式
        rootElement.style.position = "absolute";
        rootElement.style.boxSizing = "border-box";
        rootElement.style.cursor = "default";
        rootElement.style.display = "flex";
        rootElement.style.alignItems = "center";
        rootElement.style.backgroundColor = "#ffffff";
        rootElement.style.overflowX = "auto";

        // canvas 内部状态
        rootElement.__items = [];
        rootElement.__orientation = "Horizontal";
        rootElement.__backColor = "#f4f4f4";
        rootElement.__foreColor = "#000";
        rootElement.__padding = { Left: 2, Top: 2, Right: 2, Bottom: 2 };
        rootElement.__font = "12px sans-serif";

        //if (options.Win32API) {
        //    options.Win32API.BindingStandardControlEvent(rootElement, "mousedown|mousemove|mouseup|keydown|keypress|keyup");
        //}

        // 本地点击处理用于 ItemClicked
        rootElement.addEventListener("click", function (ev) {
            if (!rootElement.__items) return;
            var rect = rootElement.getBoundingClientRect();
            var x = ev.clientX - rect.left;
            var y = ev.clientY - rect.top;
            for (var i = 0; i < rootElement.__items.length; i++) {
                var it = rootElement.__items[i];
                if (!it.bounds) continue;
                var b = it.bounds;
                if (x >= b.x && x <= b.x + b.w && y >= b.y && y <= b.y + b.h) {
                    if (options && options.Win32API && typeof options.Win32API.RaiseControlEvent === "function") {
                        try {
                            var handle = rootElement.getAttribute("dchandle");
                            options.Win32API.RaiseControlEvent(handle, "ItemClicked", {
                                Index: i,
                                Name: it.Name || "",
                                Text: it.Text || ""
                            });
                        } catch (err) { }
                    }
                    break;
                }
            }
        });
        return rootElement;
    }

    /**
        * 根据选项来设置HTML元素来模拟 ToolStrip 控件
        * @param {any} rootElement HTML元素
        * @param {any} options 控件选项，属性请参考 System.Windows.Forms.ToolStrip 类
        */
    ApplyOptions(rootElement, options) {
        if (!rootElement || !options) return;
        if (rootElement.__IsToolStripItem === true) {
            // 按钮对象
            for (var strKey in options) {
                var strValue = options[strKey];
                this.SetItemPropertyValue.call(this, rootElement, strKey, strValue, options);
            }
            return;
        }

        for (var strKey in options) {
            var strValue = options[strKey];
            if (strKey == "Items") {
                for (var item of strValue) {
                    var strItemType = item.DCType;
                    var itemElement = this.RenderToHtmlElement(item, strItemType);
                    rootElement.appendChild(itemElement);
                }
            }
            else {
                this.SetPropertyValue.call(this, rootElement, strKey, strValue, options);
            }
        }
    }

    /**
     * 设置按钮的禁用样式
     * @param {HTMLElement} itemElement 按钮元素
     */
    _setDisabledStyle(itemElement) {
        itemElement.style.border = "1px solid transparent";
        itemElement.style.cursor = "not-allowed";
        itemElement.style.color = "#909399";
        itemElement.style.backgroundColor = "transparent";
    }

    /**
     * 移除元素的所有hover事件监听器
     * @param {HTMLElement} itemElement 按钮元素
     */
    _removeHoverEventListeners(itemElement) {
        // 移除基础hover事件监听器
        if (itemElement.__baseHoverEnterHandler) {
            itemElement.removeEventListener("mouseenter", itemElement.__baseHoverEnterHandler);
            itemElement.__baseHoverEnterHandler = null;
        }
        if (itemElement.__baseHoverLeaveHandler) {
            itemElement.removeEventListener("mouseleave", itemElement.__baseHoverLeaveHandler);
            itemElement.__baseHoverLeaveHandler = null;
        }
        if (itemElement.__baseMouseDownHandler) {
            itemElement.removeEventListener("mousedown", itemElement.__baseMouseDownHandler);
            itemElement.__baseMouseDownHandler = null;
        }
        if (itemElement.__baseMouseUpHandler) {
            itemElement.removeEventListener("mouseup", itemElement.__baseMouseUpHandler);
            itemElement.__baseMouseUpHandler = null;
        }
        // 移除按钮特定的hover事件监听器
        if (itemElement.__buttonHoverEnterHandler) {
            itemElement.removeEventListener("mouseenter", itemElement.__buttonHoverEnterHandler);
            itemElement.__buttonHoverEnterHandler = null;
        }
        if (itemElement.__buttonHoverLeaveHandler) {
            itemElement.removeEventListener("mouseleave", itemElement.__buttonHoverLeaveHandler);
            itemElement.__buttonHoverLeaveHandler = null;
        }
    }

    /**
     * 恢复元素的所有hover事件监听器
     * @param {HTMLElement} itemElement 按钮元素
     */
    _restoreHoverEventListeners(itemElement) {
        // 恢复基础hover事件监听器
        if (!itemElement.__baseHoverEnterHandler) {
            itemElement.__baseHoverEnterHandler = function () {
                this.classList.add("dc_ts_hover");
            };
            itemElement.addEventListener("mouseenter", itemElement.__baseHoverEnterHandler);
        }
        if (!itemElement.__baseHoverLeaveHandler) {
            itemElement.__baseHoverLeaveHandler = function () {
                this.classList.remove("dc_ts_hover");
                this.classList.remove("dc_ts_active");
            };
            itemElement.addEventListener("mouseleave", itemElement.__baseHoverLeaveHandler);
        }
        if (!itemElement.__baseMouseDownHandler) {
            itemElement.__baseMouseDownHandler = function () {
                this.classList.add("dc_ts_active");
            };
            itemElement.addEventListener("mousedown", itemElement.__baseMouseDownHandler);
        }
        if (!itemElement.__baseMouseUpHandler) {
            itemElement.__baseMouseUpHandler = function () {
                this.classList.remove("dc_ts_active");
            };
            itemElement.addEventListener("mouseup", itemElement.__baseMouseUpHandler);
        }
        // 恢复按钮特定的hover事件监听器（如果存在）
        if (!itemElement.__buttonHoverEnterHandler) {
            var strItemType = itemElement.getAttribute("dctype");
            if (strItemType === "ToolStripButton" || strItemType === "ToolStripDropDownButton" || strItemType === "ToolStripMenuItem") {
                itemElement.__buttonHoverEnterHandler = function () {
                    this.style.border = "1px solid #5faae6";
                    this.style.backgroundColor = "#b3d7f3";
                };
                itemElement.__buttonHoverLeaveHandler = function () {
                    this.style.border = "1px solid transparent";
                    this.style.backgroundColor = "transparent";
                };
                itemElement.addEventListener("mouseenter", itemElement.__buttonHoverEnterHandler);
                itemElement.addEventListener("mouseleave", itemElement.__buttonHoverLeaveHandler);
            }
        }
    }

    /**
        * 设置 ToolStrip 控件的属性
        * @param {HTMLElement} element 控件对应的 HTML 元素
        * @param {string} strPropertyName 属性名称
        * @param {any} strPropertyValue 属性值
        * @param {any} options 原始选项
        */
    SetPropertyValue(element, strPropertyName, strPropertyValue, options) {
        if (!element) return false;

        switch (strPropertyName) {
            case "Orientation":
                element.__orientation = (strPropertyValue === "Vertical") ? "Vertical" : "Horizontal";
                this._layoutAndPaint(element);
                return true;
            case "Dock":
                switch (strPropertyValue) {
                    case "Top":
                        element.style.top = "0px"; element.style.left = "0px"; element.style.right = "0px"; element.style.width = "100%"; break;
                    case "Bottom":
                        element.style.left = "0px"; element.style.right = "0px"; element.style.width = "100%"; element.style.bottom = "0px"; break;
                    case "Left":
                        element.style.top = "0px"; element.style.bottom = "0px"; element.style.left = "0px"; element.style.height = "100%"; element.__orientation = "Vertical"; break;
                    case "Right":
                        element.style.top = "0px"; element.style.bottom = "0px"; element.style.right = "0px"; element.style.height = "100%"; element.__orientation = "Vertical"; break;
                    case "Fill":
                        element.style.top = "0px"; element.style.left = "0px"; element.style.right = "0px"; element.style.bottom = "0px"; element.style.width = "100%"; element.style.height = "100%"; break;
                    default:
                        break;
                }
                this._layoutAndPaint(element);
                return true;
            case "GripStyle":
                element.setAttribute("data-grip", strPropertyValue);
                return true;
            case "BackColor":
                element.__backColor = strPropertyValue || element.__backColor;
                this._layoutAndPaint(element);
                return true;
            case "ForeColor":
                element.__foreColor = strPropertyValue || element.__foreColor;
                this._layoutAndPaint(element);
                return true;
            case "Padding":
                if (typeof strPropertyValue === "object") {
                    var p = strPropertyValue;
                    element.__padding = {
                        Left: p.Left || 0,
                        Top: p.Top || 0,
                        Right: p.Right || 0,
                        Bottom: p.Bottom || 0
                    };
                } else {
                    element.__padding = { Left: 2, Top: 2, Right: 2, Bottom: 2 };
                }
                this._layoutAndPaint(element);
                return true;
            case "Items":
                this._renderItems(element, strPropertyValue, options);
                return true;
            case "Visible":
                element.style.display = (strPropertyValue === false) ? "none" : element.style.display || "flex";
                return true;
            case "Enabled":
                if (strPropertyValue === false) {
                    element.setAttribute("aria-disabled", "true");
                    element.style.opacity = "0.6";
                    element.style.pointerEvents = "none";
                } else {
                    element.removeAttribute("aria-disabled");
                    element.style.opacity = "";
                    element.style.pointerEvents = "";
                }
                return true;
            case "Width":
                element.width = Number(strPropertyValue) || element.width;
                this._layoutAndPaint(element);
                return true;
            case "Height":
                element.height = Number(strPropertyValue) || element.height;
                this._layoutAndPaint(element);
                return true;
            case "Left":
                element.style.left = strPropertyValue + "px"; return true;
            case "Top":
                element.style.top = strPropertyValue + "px"; return true;
            case "Font":
                if (window.__DCWin32API && typeof window.__DCWin32API.SetControlFont === "function") {
                    window.__DCWin32API.SetControlFont(element, strPropertyValue);
                }
                if (strPropertyValue && strPropertyValue.Name && strPropertyValue.Size) {
                    element.__font = strPropertyValue.Size + "px " + strPropertyValue.Name;
                    this._layoutAndPaint(element);
                }
                return true;
            case "Handle":
                element.setAttribute("dchandle", strPropertyValue);
                return true;
            default:
                return super.SetPropertyValue.call(this, element, strPropertyName, strPropertyValue, options);
        }
    }
    /**
     * 设置按钮的属性值
     * @param {HTMLElement} itemElement 按钮元素HTML元素
     * @param {object} options  属性设置
     */
    SetItemPropertyValue(itemElement, strPropertyName, strPropertyValue, options) {
        if (itemElement == null || options == null) return;
        var strItemType = itemElement.getAttribute("dctype");


        switch (strPropertyName) {
            case "Name":
                if (strPropertyValue) itemElement.setAttribute("data-name", strPropertyValue);
                break;
            case "Text":
                if (strPropertyValue && strItemType !== "ToolStripSeparator" && strItemType !== "ToolStripTextBox" && itemElement.children.length === 0) {
                    itemElement.textContent = strPropertyValue;
                }
                break;
            case "Enabled":
                if (strPropertyValue === false) {
                    itemElement.disabled = true;
                    itemElement.setAttribute("aria-disabled", "true");
                    // 设置禁用样式
                    this._setDisabledStyle(itemElement);
                    // 移除所有hover事件监听器
                    this._removeHoverEventListeners(itemElement);
                }
                else {
                    itemElement.disabled = false;
                    itemElement.setAttribute("aria-disabled", "false");
                    // 恢复启用样式
                    itemElement.style.opacity = "";
                    itemElement.style.cursor = "";
                    itemElement.style.color = "";
                    // 恢复所有hover事件监听器
                    this._restoreHoverEventListeners(itemElement);
                }
                break;
            case "Checked":
                if (strPropertyValue === true && strItemType.indexOf("Button") >= 0) {
                    itemElement.setAttribute("aria-pressed", "true");
                    itemElement.classList.add("checked");
                }
                break;
            case "CheckOnClick":
                if (strPropertyValue === true && strItemType.indexOf("Button") >= 0) {
                    itemElement.addEventListener("click", function () {
                        var pressed = this.getAttribute("aria-pressed") === "true";
                        this.setAttribute("aria-pressed", (!pressed).toString());
                        this.classList.toggle("checked", !pressed);
                    });
                }
                break;
            case "Visible":
                if (strPropertyValue === false) itemElement.style.display = "none";
                break;
            case "Tag":
                if (strPropertyValue != null) itemElement.setAttribute("data-tag", strPropertyValue);
                break;
            case "Alignment":
                if (strPropertyValue === "Right") {
                    itemElement.style.marginLeft = "auto";
                }
                break;
            case "ForeColor":
                if (strPropertyValue) {
                    itemElement.style.color = strPropertyValue;
                }
                break;
            case "RightToLeft":
                if (strPropertyValue === "Yes") {
                    itemElement.dir = "rtl";
                }
                break;
            case "DisplayStyle":
                if (strPropertyValue && strPropertyValue.indexOf("Image") >= 0 && options.Image) {
                    itemElement.style.backgroundImage = "url('" + options.Image + "')";
                    itemElement.style.backgroundRepeat = "no-repeat";
                    itemElement.style.backgroundPosition = options.ImageAlign || "center left";
                    if (!options.Text) itemElement.textContent = "";
                }
                break;
            case "ImageKey":
                if (!options.Image && strPropertyValue) {
                    itemElement.setAttribute("data-imagekey", strPropertyValue);
                }
                break;
            case "ImageAlign":
                if (strPropertyValue) {
                    itemElement.style.backgroundPosition = strPropertyValue;
                }
                break;
            case "TextImageRelation":
                if (strPropertyValue && options.DisplayStyle && options.DisplayStyle.indexOf("Image") >= 0) {
                    if (strPropertyValue.indexOf("Above") >= 0) {
                        itemElement.classList.add("dc_tir_above");
                    } else if (strPropertyValue.indexOf("Below") >= 0) {
                        itemElement.classList.add("dc_tir_below");
                    } else if (strPropertyValue.indexOf("Overlay") >= 0) {
                        itemElement.classList.add("dc_tir_overlay");
                    } else if (strPropertyValue.indexOf("TextBeforeImage") >= 0) {
                        itemElement.classList.add("dc_tir_textbefore");
                    }
                }
                break;
            case "Available":
                itemElement.setAttribute("data-available", strPropertyValue !== false);
                break;
            case "DoubleClickEnabled":
                if (strPropertyValue === false) {
                    itemElement.ondblclick = function (ev) { ev.preventDefault(); };
                }
                break;
        }
    }
    /**
        * 渲染 ToolStrip Item 集合，使用按钮模拟常见条目
        * @param {HTMLElement} element 容器元素
        * @param {Array} items 条目集合
        * @param {any} options 原始选项
        */
    _renderItems(element, items, options) {
        if (!Array.isArray(items)) return;
        element.__items = items.slice();
        this._layoutAndPaint(element);
    }

    /**
        * 布局并绘制 ToolStrip 及其子项
        */
    _layoutAndPaint(element) {
        if (!element.getContext) return;
        var ctx = element.getContext("2d");
        if (!ctx) return;

        var padding = element.__padding || { Left: 2, Top: 2, Right: 2, Bottom: 2 };
        var width = element.width || element.clientWidth || 200;
        var height = element.height || element.clientHeight || 28;
        if (!element.width) element.width = width;
        if (!element.height) element.height = height;

        ctx.clearRect(0, 0, element.width, element.height);
        ctx.fillStyle = element.__backColor || "#f4f4f4";
        ctx.fillRect(0, 0, element.width, element.height);
        ctx.strokeStyle = "#b0b0b0";
        ctx.strokeRect(0.5, 0.5, element.width - 1, element.height - 1);

        ctx.font = element.__font || "12px sans-serif";
        ctx.textBaseline = "middle";
        ctx.fillStyle = element.__foreColor || "#000";

        var x = padding.Left;
        var y = padding.Top;
        var items = element.__items || [];
        var lineHeight = 22;
        for (var i = 0; i < items.length; i++) {
            var it = items[i] || {};
            var text = it.Text || "";
            var textWidth = ctx.measureText(text).width;
            var itemW = Math.max(20, textWidth + 16);
            var itemH = lineHeight;

            if (element.__orientation === "Vertical") {
                if (y + itemH > element.height - padding.Bottom) break;
            } else {
                if (x + itemW > element.width - padding.Right) break;
            }

            it.bounds = { x: x, y: y, w: itemW, h: itemH };

            // 背景
            var enabled = it.Enabled !== false;
            ctx.fillStyle = enabled ? "#ffffff" : "#e0e0e0";
            ctx.strokeStyle = "#b0b0b0";
            ctx.lineWidth = 1;
            ctx.fillRect(x + 0.5, y + 0.5, itemW - 1, itemH - 1);
            ctx.strokeRect(x + 0.5, y + 0.5, itemW - 1, itemH - 1);

            // 文本
            ctx.fillStyle = enabled ? (element.__foreColor || "#000") : "#777";
            ctx.fillText(text, x + 6, y + itemH / 2);

            if (element.__orientation === "Vertical") {
                y += itemH + 2;
            } else {
                x += itemW + 4;
            }
        }
    }
    /**
        * 创建HTML元素来模拟System.Windows.Forms.ToolStripItem
        * @param {any} itemOptions ToolStripItem 设置对象，里面的属性值和System.Windows.Forms.ToolStripXXXX类型的公开的属性保持一致。
        * @param {string} strItemType 元素类型,可以是ToolStripButton等等。
        * @returns {HTMLElement} 创建的HTML元素。
        * @description 能模拟出 ToolStripButton,ToolStripComboBox(Vue风格),ToolStripDropDown,ToolStripDropDownButton
        * @description ToolStripLabel,ToolStripSplitButton,ToolStripTextBox,
        */
    RenderToHtmlElement(itemOptions, strItemType) {
        var type = (strItemType || "ToolStripButton") + "";
        var opts = itemOptions || {};
        var itemElement;
        var factory = this; // preserve context for nested handlers


        switch (type) {
            case "ToolStripLabel":
                itemElement = document.createElement("span");
                itemElement.className = "dc_toolstrip-label";
                itemElement.style.fontSize = "13px";  // 设置字体大小与其他button一致
                itemElement.style.fontFamily = "Arial";  // 设置字体与其他button一致
                itemElement.style.padding = "0 5px";  // 设置内边距与其他button一致
                break;
            case "ToolStripSeparator":
                itemElement = document.createElement("div");
                itemElement.className = "dc_toolstrip-separator";
                itemElement.style.width = "1px";
                itemElement.style.height = "50%";
                itemElement.style.background = "#c0c0c0";

                if (opts.Size) {
                    //通过宽高大小计算是横线还是竖线
                    if (opts.Size.Width > opts.Size.Height) {
                        //横线
                        itemElement.style.width = "calc(100% - 30px)";
                        itemElement.style.height = "1px";
                        itemElement.style.margin = "0";

                    } else {
                        //竖线
                        itemElement.style.width = "1px";
                        itemElement.style.height = "50%";
                        itemElement.style.margin = "0 3px";
                    }
                }

                break;
            case "ToolStripComboBox": {
                //input可输入 + 自定义下拉面板(非select)
                itemElement = document.createElement("div");
                itemElement.className = "dc_toolstrip-combobox dc_vue-like";
                itemElement.style.backgroundColor = "transparent";
                itemElement.style.display = "inline-flex";
                itemElement.style.alignItems = "center";
                itemElement.style.position = "relative";
                itemElement.style.border = "1px solid #ccc";
                itemElement.style.borderRadius = "4px";
                itemElement.style.background = "#fff";
                itemElement.style.marginRight = "6px";
                itemElement.__items = [];
                itemElement.__selectedIndex = -1;
                itemElement.__dropDownVisible = false;

                var inputWrap = document.createElement("div");
                inputWrap.style.display = "flex";
                inputWrap.style.alignItems = "center";
                inputWrap.style.flex = "1";
                inputWrap.style.minWidth = "0";
                if (opts.Size && opts.Size.Width) {
                    itemElement.style.width = opts.Size.Width + "px";
                }

                var input = document.createElement("input");
                input.type = "text";
                input.className = "dc_toolstrip-combobox-input";
                input.style.border = "none";
                input.style.outline = "none";
                input.style.flex = "1";
                input.style.background = "transparent";
                input.style.padding = "2px 4px";
                input.style.fontSize = "13px";
                input.style.minWidth = "0";
                if (opts.Text != null) input.value = opts.Text;
                itemElement.__input = input;
                inputWrap.appendChild(input);

                var arrow = document.createElement("span");
                arrow.className = "dc_toolstrip-combobox-arrow";
                arrow.textContent = "\u25BC";
                arrow.style.fontSize = "10px";
                arrow.style.padding = "0 6px";
                arrow.style.cursor = "pointer";
                arrow.style.color = "#666";
                arrow.style.pointerEvents = "auto";
                inputWrap.appendChild(arrow);

                itemElement.appendChild(inputWrap);

                var closeDropdown = function (cbEl, cancel) {
                    if (!cbEl.__dropDownVisible) return;
                    cbEl.__dropDownVisible = false;
                    if (cbEl.__dropdown && cbEl.__dropdown.parentNode) {
                        cbEl.__dropdown.parentNode.removeChild(cbEl.__dropdown);
                    }
                    if (cbEl.__docHandler) {
                        document.removeEventListener("click", cbEl.__docHandler);
                        cbEl.__docHandler = null;
                    }
                    cbEl.__dropdown = null;
                };

                var openDropdown = async function (cbEl) {
                    if (cbEl.__dropDownVisible) {
                        closeDropdown(cbEl, true);
                        return;
                    }
                    var handle = parseInt(cbEl.getAttribute("dchandle"));
                    var listItems = await __DCExecuteControlCommandAsync(handle, "GetDropdownItems", null);
                    cbEl.__items = listItems || [];
                    cbEl.__selectedIndex = opts.SelectedIndex != null && opts.SelectedIndex >= 0 && opts.SelectedIndex < cbEl.__items.length ? opts.SelectedIndex : -1;

                    var dd = document.createElement("div");
                    dd.className = "dc_toolstrip-combobox-dropdown";
                    dd.style.position = "fixed";
                    dd.style.background = "#fff";
                    dd.style.border = "1px solid #ccc";
                    dd.style.borderRadius = "4px";
                    dd.style.boxShadow = "0 4px 12px rgba(0,0,0,0.15)";
                    dd.style.maxHeight = "200px";
                    dd.style.overflowY = "auto";
                    dd.style.zIndex = "100000";
                    dd.style.minWidth = cbEl.getBoundingClientRect().width + "px";

                    for (var i = 0; i < cbEl.__items.length; i++) {
                        (function (idx) {
                            var item = cbEl.__items[idx];
                            var text = item.Text || item.Value || item || "";
                            var row = document.createElement("div");
                            row.className = "dc_toolstrip-combobox-item";
                            row.style.padding = "6px 8px";
                            row.style.cursor = "pointer";
                            row.style.whiteSpace = "nowrap";
                            row.style.fontSize = "13px";
                            row.textContent = text;
                            if (idx === cbEl.__selectedIndex) row.style.background = "#e8f4ff";
                            row.addEventListener("mouseenter", function () {
                                dd.querySelectorAll(".dc_toolstrip-combobox-item").forEach(function (n) { n.style.background = ""; });
                                row.style.background = "#f3f3f3";
                            });
                            row.addEventListener("mouseleave", function () { row.style.background = idx === cbEl.__selectedIndex ? "#e8f4ff" : ""; });
                            row.addEventListener("mousedown", function (ev) { ev.preventDefault(); ev.stopPropagation(); });
                            row.addEventListener("click", function (ev) {
                                ev.stopPropagation();
                                cbEl.__selectedIndex = idx;
                                cbEl.__input.value = text;
                                cbEl.setAttribute("data-value", item.Value != null ? item.Value : text);
                                closeDropdown(cbEl);
                                if (typeof __DCExecuteControlCommandAsync === "function") {
                                    __DCExecuteControlCommandAsync(handle, "OnSelectedIndexChanged", { Index: idx, Text: text });
                                }
                            });
                            dd.appendChild(row);
                        })(i);
                    }

                    var rect = cbEl.getBoundingClientRect();
                    dd.style.left = rect.left + "px";
                    dd.style.top = (rect.bottom + 2) + "px";
                    document.body.appendChild(dd);
                    cbEl.__dropdown = dd;
                    cbEl.__dropDownVisible = true;

                    cbEl.__docHandler = function (ev) {
                        if (dd && !dd.contains(ev.target) && !cbEl.contains(ev.target)) {
                            closeDropdown(cbEl);
                        }
                    };
                    document.addEventListener("click", cbEl.__docHandler);
                };

                itemElement.addEventListener("click", function (ev) {
                    ev.stopPropagation();
                    openDropdown(itemElement);
                });
                input.addEventListener("click", function (ev) { ev.stopPropagation(); openDropdown(itemElement); });
                input.addEventListener("focus", function () { itemElement.classList.add("dc_combobox_focus"); });
                input.addEventListener("blur", function () { itemElement.classList.remove("dc_combobox_focus"); });
                input.addEventListener("input", function () {
                    itemElement.setAttribute("data-value", this.value);
                    var h = itemElement.getAttribute("dchandle");
                    if (h && typeof __DCExecuteControlCommandAsync === "function") {
                        __DCExecuteControlCommandAsync(parseInt(h), "OnTextChanged", { Text: this.value });
                    }
                });
                break;
            }
            case "ToolStripTextBox":
                itemElement = document.createElement("input");
                itemElement.type = "text";
                itemElement.className = "dc_toolstrip-textbox";
                if (opts.Text != null) itemElement.value = opts.Text;
                if (opts.ReadOnly === true) itemElement.readOnly = true;
                if (opts.MaxLength != null) itemElement.maxLength = parseInt(opts.MaxLength, 10);
                if (opts.CharacterCasing) {
                    itemElement.addEventListener("input", function () {
                        if (opts.CharacterCasing === "Upper") this.value = this.value.toUpperCase();
                        else if (opts.CharacterCasing === "Lower") this.value = this.value.toLowerCase();
                    });
                }
                if (opts.TextBoxTextAlign) {
                    itemElement.style.textAlign = opts.TextBoxTextAlign.toLowerCase();
                }
                break;
            case "ToolStripDropDownButton": {
                itemElement = document.createElement("button");
                itemElement.type = "button";
                itemElement.className = "dc_toolstrip-button dc_toolstrip-dropdown";
                itemElement.style.border = "none";
                itemElement.addEventListener("mousedown", async function (ev) {
                    ev.preventDefault();
                    ev.stopPropagation();
                    var btn = this;
                    // toggle existing menu
                    if (btn.__dropdown && btn.__dropdown.parentNode) {
                        btn.__dropdown.parentNode.removeChild(btn.__dropdown);
                        btn.__dropdown = null;
                        return;
                    }
                    var listItems = await __DCExecuteControlCommandAsync(
                        parseInt(btn.getAttribute("dchandle")),
                        "GetDropdownItems",
                        null);

                    if (!listItems || !listItems.length) return;
                    var menu = document.createElement("div");
                    menu.className = "dc_toolstrip-dropdown-menu";
                    menu.style.position = "absolute";
                    // 设置左边20px为灰色背景，右边不变（使用linear-gradient实现，但不渐变）
                    menu.style.background = "linear-gradient(to right, rgb(239, 239, 239) 0px, rgb(239, 239, 239) 28px, #fff 28px, #fff 100%)";
                    menu.style.paddingLeft = "30px";
                    menu.style.boxSizing = "border-box";
                    menu.style.border = "1px solid #ccc";
                    menu.style.boxShadow = "0 2px 6px rgba(0,0,0,0.2)";
                    menu.style.zIndex = "99999";
                    menu.style.minWidth = (btn.offsetWidth || 40) + "px";


                    for (var i = 0; i < listItems.length; i++) {
                        var child = factory.RenderToHtmlElement(listItems[i], listItems[i].DCType);
                        child.style.display = "block";
                        child.style.width = "100%";
                        child.addEventListener("click", function (e) {
                            e.stopPropagation();
                            if (menu && menu.parentNode) {
                                menu.parentNode.removeChild(menu);
                                btn.__dropdown = null;
                            }
                        });
                        menu.appendChild(child);
                    }
                    document.body.appendChild(menu);
                    var r = btn.getBoundingClientRect();
                    var left = r.left + window.pageXOffset;
                    var top = r.bottom + window.pageYOffset;
                    menu.style.left = left + "px";
                    menu.style.top = top + "px";

                    var closeMenu = function () {
                        if (menu && menu.parentNode) menu.parentNode.removeChild(menu);
                        btn.__dropdown = null;
                        document.removeEventListener("mousedown", onDoc, true);
                    };
                    var onDoc = function (e2) {
                        if (menu && !menu.contains(e2.target) && e2.target !== btn) {
                            closeMenu();
                        }
                    };
                    document.addEventListener("mousedown", onDoc, true);
                    btn.__dropdown = menu;
                });
                // icon
                if (opts.DisplayStyle === "ImageAndText" || opts.DisplayStyle === "Image") {
                    //显示图片和文字
                    var imgUrl2 = opts.Image || opts.ImageUrl;
                    if (imgUrl2) {
                        var imgD = document.createElement("img");
                        imgD.src = imgUrl2;
                        imgD.alt = opts.Text || "";
                        imgD.style.verticalAlign = "middle";
                        imgD.style.maxHeight = "16px";
                        imgD.style.maxWidth = "16px";
                        if (opts.ImageAlign) imgD.style.objectPosition = opts.ImageAlign;
                        itemElement.appendChild(imgD);
                    }
                }

                if (opts.Text) {
                    var spanD = document.createElement("span");
                    spanD.className = "dc_toolstrip-button-text";
                    spanD.textContent = opts.Text;
                    spanD.style.marginLeft = imgUrl2 ? "4px" : "0";
                    spanD.style.display = "inline-block";
                    spanD.style.width = "100%";
                    spanD.style.textAlign = "left";
                    itemElement.appendChild(spanD);
                }
                var arrow = document.createElement("span");
                arrow.className = "dc_toolstrip-dropdown-arrow";
                arrow.textContent = "▼";
                arrow.style.marginLeft = "2px";
                arrow.style.fontSize = "8px";
                itemElement.appendChild(arrow);
                itemElement.style.display = "flex";
                itemElement.style.alignItems = "center";
                break;
            }
            default:
                // ToolStripButton / DropDownButton / SplitButton
                itemElement = document.createElement("button");
                itemElement.type = "button";
                itemElement.className = "dc_toolstrip-button";
                // 文字+图片
                if (opts.DisplayStyle === "ImageAndText" || opts.DisplayStyle === "Image") {
                    var imgUrl = opts.Image || opts.ImageUrl;
                    if (imgUrl) {
                        var img = document.createElement("img");
                        img.src = imgUrl;
                        img.alt = opts.Text || "";
                        img.style.verticalAlign = "middle";
                        img.style.maxHeight = "16px";
                        img.style.maxWidth = "16px";
                        if (opts.ImageAlign) img.style.objectPosition = opts.ImageAlign;
                        itemElement.appendChild(img);
                    }
                }

                if (opts.Text) {
                    var span = document.createElement("span");
                    span.className = "dc_toolstrip-button-text";
                    span.textContent = opts.Text;
                    span.style.marginLeft = imgUrl ? "4px" : "0";
                    span.style.display = "inline-block";
                    span.style.width = "100%";
                    span.style.textAlign = "left";
                    itemElement.appendChild(span);
                }
                break;

        }
        itemElement.__DCFactory = this;
        itemElement.__IsToolStripItem = true;
        itemElement.setAttribute("dchandle", opts.Handle);
        itemElement.setAttribute("dctype", type);
        itemElement.setAttribute("dcname", opts.DCName);
        itemElement.onclick = async function () {
            await __DCExecuteControlCommandAsync(
                parseInt(itemElement.getAttribute("dchandle")),
                "OnClick",
                null);
        };
        if (type != "ToolStripLabel" && type != "ToolStripSeparator") {
            // hover effects（保存为命名函数，以便在禁用时可以移除）
            itemElement.__baseHoverEnterHandler = function () {
                this.classList.add("dc_ts_hover");
            };
            itemElement.__baseHoverLeaveHandler = function () {
                this.classList.remove("dc_ts_hover");
                this.classList.remove("dc_ts_active");
            };
            itemElement.__baseMouseDownHandler = function () {
                this.classList.add("dc_ts_active");
            };
            itemElement.__baseMouseUpHandler = function () {
                this.classList.remove("dc_ts_active");
            };
            itemElement.addEventListener("mouseenter", itemElement.__baseHoverEnterHandler);
            itemElement.addEventListener("mouseleave", itemElement.__baseHoverLeaveHandler);
            itemElement.addEventListener("mousedown", itemElement.__baseMouseDownHandler);
            itemElement.addEventListener("mouseup", itemElement.__baseMouseUpHandler);
        }

        //对button设置鼠标滑过样式dctype="ToolStripButton"
        if (type === "ToolStripButton" || type === "ToolStripDropDownButton" || type === "ToolStripMenuItem") {
            //补充部分样式
            itemElement.style.display = opts.Visible === false ? "none" : "flex";
            itemElement.style.alignItems = "center";
            if (type === "ToolStripMenuItem") {
                //菜单项只设置上下间距
                itemElement.style.margin = "4px 0";
            } else {
                //其他按钮设置左右间距
                // rootElement.style.marginRight = "8px";
            }
            itemElement.style.padding = "0 3px";
            itemElement.style.boxSizing = "border-box";
            itemElement.style.fontSize = "13px";

            itemElement.style.border = "1px solid transparent";
            itemElement.style.backgroundColor = "transparent";

            //鼠标滑过样式（保存为命名函数，以便在禁用时可以移除）
            itemElement.__buttonHoverEnterHandler = function () {
                this.style.border = "1px solid #5faae6";
                this.style.backgroundColor = "#b3d7f3";
            };
            itemElement.__buttonHoverLeaveHandler = function () {
                this.style.border = "1px solid transparent";
                this.style.backgroundColor = "transparent";
            };
            itemElement.addEventListener("mouseenter", itemElement.__buttonHoverEnterHandler);
            itemElement.addEventListener("mouseleave", itemElement.__buttonHoverLeaveHandler);
        }

        // 处理属性设置（在所有事件监听器添加完成后）
        for (var strKey in opts) {
            var strValue = opts[strKey];
            this.SetItemPropertyValue.call(this, itemElement, strKey, strValue, opts);
        }
        itemElement.style.whiteSpace = "nowrap";  // 设置不能换行
        return itemElement;
    }
};
if (window.__DCControlTypes == null) window.__DCControlTypes = new Object();
window.__DCControlTypes["System.Windows.Forms.ToolStrip"] = new SystemWindowsFormsToolStripFactory();