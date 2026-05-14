/** 使用HTML 元素模拟 System.Windows.Forms.RadioButton 控件 */
"use strict";

class SystemWindowsFormsRadioButtonFactory extends SystemWindowsFormsControlFactory {
    /**
     * 根据选项来创建HTML元素来模拟 RadioButton 控件
     * @param {any} options 控件选项，属性请参考 System.Windows.Forms.RadioButton 类,必定包含一个Handle属性。
     * @returns { HTMLElement} 返回创建的 HTML 元素
     */
    Create(options) {
        if (options == null) {
            throw new Error("Options parameter is required to create a RadioButton control.");
        }
        var html = options.UserPaint == true ?
            document.createElement("canvas") : document.createElement("div");
        html.setAttribute("dchandle", options.Handle);
        html.setAttribute("dctype", "System.Windows.Forms.RadioButton");

        // 一些基本默认样式（尽量简单）
        if (html.nodeName !== "CANVAS") {
            html.style.position = 'absolute';
            html.style.display = 'inline-flex';
            html.style.alignItems = 'center';
            html.style.whiteSpace = 'nowrap';
            html.style.cursor = 'default';
            html.style.userSelect = 'none';
        }


        // 如果不是 UserPaint 模式，创建单选框的视觉元素
        if (options.UserPaint !== true) {
            // 创建单选框外圈
            var RadioButton = document.createElement("div");
            RadioButton.className = 'dc-RadioButton-box';
            RadioButton.style.width = '13px';
            RadioButton.style.height = '13px';
            RadioButton.style.border = '1px solid #666';
            RadioButton.style.backgroundColor = '#fff';
            RadioButton.style.borderRadius = '50%';
            RadioButton.style.marginRight = '4px';
            RadioButton.style.flexShrink = '0';
            RadioButton.style.display = 'inline-flex';
            RadioButton.style.alignItems = 'center';
            RadioButton.style.justifyContent = 'center';
            RadioButton.style.boxSizing = 'border-box';

            html.__RadioButton = RadioButton;
            html.appendChild(RadioButton);

            // 内部圆点（初始隐藏）
            var dot = document.createElement("div");
            dot.className = 'dc-RadioButton-dot';
            dot.style.width = '7px';
            dot.style.height = '7px';
            dot.style.borderRadius = '50%';
            dot.style.backgroundColor = '#0078d4';
            dot.style.display = 'none';
            html.__radioDot = dot;
            RadioButton.appendChild(dot);

            // 创建文本标签容器
            var textLabel = document.createElement("span");
            textLabel.className = 'dc-RadioButton-text';
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
                // 处理文本显示（简单：直接文本，不解析助记符）
                if (element.nodeName === "CANVAS") {
                    return true; // Canvas 模式不处理文本
                }
                var textLabel = element.__textLabel;
                if (!textLabel) {
                    // 如果没有文本标签，创建一个
                    textLabel = document.createElement("span");
                    textLabel.className = 'dc-RadioButton-text';
                    textLabel.style.flex = '1';
                    element.__textLabel = textLabel;
                    // 插入到单选框外圈之后
                    if (element.__RadioButton && element.__RadioButton.nextSibling) {
                        element.insertBefore(textLabel, element.__RadioButton.nextSibling);
                    } else {
                        element.appendChild(textLabel);
                    }
                }
                textLabel.textContent = (strPropertyValue == null) ? '' : String(strPropertyValue);
                return true;

            case "Checked":
                if (element.nodeName === "CANVAS") {
                    return true; // Canvas 模式由 C# 端绘制
                }
                var checked = !!strPropertyValue;
                element.setAttribute("data-checked", checked ? "true" : "false");
                if (element.__radioDot) {
                    element.__radioDot.style.display = checked ? 'block' : 'none';
                }
                if (element.__RadioButton) {
                    element.__RadioButton.style.borderColor = checked ? '#0078d4' : '#666';
                }
                return true;

            case "Enabled":
                if (element.nodeName === "CANVAS") {
                    return true;
                }
                var enabled = (strPropertyValue !== false);
                element.style.opacity = enabled ? '' : '0.6';
                element.style.pointerEvents = enabled ? '' : 'none';
                return true;

            default:
                return super.SetPropertyValue.call(this, element, strPropertyName, strPropertyValue, options);
        }
    }
};
if (window.__DCControlTypes == null) window.__DCControlTypes = new Object();
window.__DCControlTypes["System.Windows.Forms.RadioButton"] = new SystemWindowsFormsRadioButtonFactory();
