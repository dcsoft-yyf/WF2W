/** 使用HTML 元素模拟 System.Windows.Forms.CheckBox 控件 */
"use strict";

class SystemWindowsFormsCheckBoxFactory extends SystemWindowsFormsControlFactory {
    /**
     * 根据选项来创建HTML元素来模拟 CheckBox 控件
     * @param {any} options 控件选项，属性请参考 System.Windows.Forms.CheckBox 类,必定包含一个Handle属性。
     * @returns { HTMLElement} 返回创建的 HTML 元素
     */
    Create(options) {
        if (options == null) {
            throw new Error("Options parameter is required to create a CheckBox control.");
        }
        var html = options.UserPaint == true ?
            document.createElement("canvas") : document.createElement("div");
        html.setAttribute("dchandle", options.Handle);
        html.setAttribute("dctype", "System.Windows.Forms.CheckBox");
        // some sensible defaults
        html.style.position = 'absolute';
        html.style.display = 'inline-flex';  // 改为 flex 布局，方便复选框和文本对齐
        html.style.alignItems = 'center';     // 垂直居中对齐
        html.style.whiteSpace = 'nowrap';
        html.style.cursor = 'default';
        html.style.userSelect = 'none';       // 防止文本选择

        // 如果不是 UserPaint 模式，创建复选框的视觉元素
        if (options.UserPaint !== true) {
            // 创建复选框框体
            var checkboxBox = document.createElement("div");
            checkboxBox.className = 'dc-checkbox-box';
            checkboxBox.style.width = '13px';
            checkboxBox.style.height = '13px';
            checkboxBox.style.border = '1px solid #666';
            checkboxBox.style.backgroundColor = '#fff';
            checkboxBox.style.marginRight = '4px';
            checkboxBox.style.flexShrink = '0';
            checkboxBox.style.display = 'inline-flex';
            checkboxBox.style.alignItems = 'center';
            checkboxBox.style.justifyContent = 'center';
            checkboxBox.style.boxSizing = 'border-box';
            html.__checkboxBox = checkboxBox;
            html.appendChild(checkboxBox);

            // 创建复选框勾选标记（初始隐藏）
            var checkmark = document.createElement("div");
            checkmark.className = 'dc-checkbox-checkmark';
            checkmark.innerHTML = '✓';  // 或者使用其他符号如 '✔'
            checkmark.style.display = 'none';
            checkmark.style.color = '#000';
            checkmark.style.fontSize = '11px';
            checkmark.style.lineHeight = '1';
            checkmark.style.fontWeight = 'bold';
            html.__checkmark = checkmark;
            checkboxBox.appendChild(checkmark);

            // 创建文本标签容器
            var textLabel = document.createElement("span");
            textLabel.className = 'dc-checkbox-text';
            textLabel.style.flex = '1';
            html.__textLabel = textLabel;
            html.appendChild(textLabel);
        }

        if (options.Win32API && typeof options.Win32API.BindingStandardControlEvent === "function") {
            options.Win32API.BindingStandardControlEvent(html, "click|mousedown|mousemove|mouseup|keydown|keypress|keyup");
        }
        //this.ApplyOptions(html, options);
        return html;
    }

    /** 应用属性 */
    ApplyOptions(htmlElement, options) {
        if (!htmlElement || !options) return;
        for (var strKey in options) {
            if (!Object.prototype.hasOwnProperty.call(options, strKey)) continue;
            this.SetPropertyValue.call(this, htmlElement, strKey, options[strKey], options);
        }
    }

    /** 设置属性值 */
    SetPropertyValue(element, strPropertyName, strPropertyValue, options) {
        if (!element) return false;
        switch (strPropertyName) {
            case "AutoSize":
                element.setAttribute("data-autosize", !!strPropertyValue);
                // 简单处理：AutoSize true 则使用内容撑开
                if (strPropertyValue === true) {
                    element.style.width = "auto";
                    element.style.height = "auto";
                }
                return true;

            case "Text":
                // 处理文本显示（支持快捷键 & 符号）
                if (element.nodeName === "CANVAS") {
                    return true; // Canvas 模式不处理文本
                }
                var textLabel = element.__textLabel;
                if (!textLabel) {
                    // 如果没有文本标签，创建一个
                    textLabel = document.createElement("span");
                    textLabel.className = 'dc-checkbox-text';
                    textLabel.style.flex = '1';
                    element.__textLabel = textLabel;
                    // 插入到复选框框体之后
                    if (element.__checkboxBox && element.__checkboxBox.nextSibling) {
                        element.insertBefore(textLabel, element.__checkboxBox.nextSibling);
                    } else {
                        element.appendChild(textLabel);
                    }
                }

                var raw = strPropertyValue == null ? '' : String(strPropertyValue);
                var r = window.__DCRenderMnemonic(raw);
                textLabel.innerHTML = r.html;
                if (r.access) element.setAttribute('accesskey', r.access);
                window.__DCEnsureMnemonicUnderlineStyle();
                return true;

            case "Checked":
                // 处理选中状态
                if (element.nodeName === "CANVAS") {
                    return true; // Canvas 模式由 C# 端绘制
                }
                var checked = !!strPropertyValue;
                element.setAttribute("data-checked", checked ? "true" : "false");

                var checkmark = element.__checkmark;
                if (checkmark) {
                    checkmark.style.display = checked ? 'block' : 'none';
                }

                // 更新复选框框体的样式
                var checkboxBox = element.__checkboxBox;
                if (checkboxBox) {
                    if (checked) {
                        checkboxBox.style.backgroundColor = '#0078d4'; // 选中时的背景色
                        checkboxBox.style.borderColor = '#0078d4';
                        if (checkmark) {
                            checkmark.style.color = '#fff'; // 选中时勾选标记为白色
                        }
                    } else {
                        checkboxBox.style.backgroundColor = '#fff';
                        checkboxBox.style.borderColor = '#666';
                    }
                }
                return true;

            case "CheckState":
                // 处理三态复选框（Unchecked, Checked, Indeterminate）
                if (element.nodeName === "CANVAS") {
                    return true;
                }
                var state = strPropertyValue;
                element.setAttribute("data-checkstate", state);

                var checkmark = element.__checkmark;
                var checkboxBox = element.__checkboxBox;

                if (checkboxBox) {
                    switch (state) {
                        case "Checked":
                            if (checkmark) {
                                checkmark.style.display = 'block';
                                checkmark.innerHTML = '✓';
                                checkmark.style.color = '#fff';
                            }
                            checkboxBox.style.backgroundColor = '#0078d4';
                            checkboxBox.style.borderColor = '#0078d4';
                            break;
                        case "Unchecked":
                            if (checkmark) {
                                checkmark.style.display = 'none';
                            }
                            checkboxBox.style.backgroundColor = '#fff';
                            checkboxBox.style.borderColor = '#666';
                            break;
                        case "Indeterminate":
                            // 中间状态：显示一个横线或填充
                            if (checkmark) {
                                checkmark.style.display = 'block';
                                checkmark.innerHTML = '−'; // 或使用其他符号
                                checkmark.style.color = '#fff';
                            }
                            checkboxBox.style.backgroundColor = '#0078d4';
                            checkboxBox.style.borderColor = '#0078d4';
                            break;
                    }
                }
                return true;

            case "CheckAlign":
                // 处理复选框对齐方式（Left, Right, Top, Bottom, MiddleLeft, MiddleRight等）
                if (element.nodeName === "CANVAS") {
                    return true;
                }
                var align = strPropertyValue;
                var checkboxBox = element.__checkboxBox;
                var textLabel = element.__textLabel;

                if (!checkboxBox || !textLabel) return true;

                // 根据对齐方式调整布局
                switch (align) {
                    case "TopLeft":
                    case "TopCenter":
                    case "TopRight":
                        element.style.flexDirection = 'column';
                        element.style.alignItems = align === "TopLeft" ? 'flex-start' :
                            align === "TopCenter" ? 'center' : 'flex-end';
                        break;
                    case "MiddleLeft":
                        element.style.flexDirection = 'row';
                        element.style.alignItems = 'center';
                        break;
                    case "MiddleRight":
                        element.style.flexDirection = 'row-reverse';
                        element.style.alignItems = 'center';
                        // 交换顺序
                        if (checkboxBox.parentNode === element && textLabel.parentNode === element) {
                            element.insertBefore(textLabel, checkboxBox);
                        }
                        break;
                    case "BottomLeft":
                    case "BottomCenter":
                    case "BottomRight":
                        element.style.flexDirection = 'column-reverse';
                        element.style.alignItems = align === "BottomLeft" ? 'flex-start' :
                            align === "BottomCenter" ? 'center' : 'flex-end';
                        break;
                    default:
                        // 默认 MiddleLeft
                        element.style.flexDirection = 'row';
                        element.style.alignItems = 'center';
                }
                return true;

            case "ThreeState":
                // 三态复选框支持（这个属性本身不需要特殊处理，CheckState 已经处理了）
                element.setAttribute("data-threestate", !!strPropertyValue ? "true" : "false");
                return true;

            default:
                return super.SetPropertyValue.call(this, element, strPropertyName, strPropertyValue, options);
        }
    }
};
if (window.__DCControlTypes == null) window.__DCControlTypes = new Object();
window.__DCControlTypes["System.Windows.Forms.CheckBox"] = new SystemWindowsFormsCheckBoxFactory();
