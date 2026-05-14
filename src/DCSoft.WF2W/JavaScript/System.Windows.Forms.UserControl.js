/** 使用HTML 元素模拟 System.Windows.Forms.UserControl 控件 */
"use strict";
class SystemWindowsFormsUserControlFactory extends SystemWindowsFormsContainerControlFactory {
    /**
        * 创建 UserControl 的 HTML 元素
        * @param {any} options 控件选项
        * @returns {HTMLElement}
        */
    Create(options) {
        if (!options) {
            throw new Error("Options parameter is required to create a UserControl.");
        }
        var html = super.Create.call(this, options);
        html.setAttribute("dctype", "System.Windows.Forms.UserControl");
        html.setAttribute("dcbasecontroltypename", "System.Windows.Forms.ContainerControl");
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
                element.style.width = (strPropertyValue === true) ? "auto" : element.style.width;
                element.style.height = (strPropertyValue === true) ? "auto" : element.style.height;
                return true;
            case "AutoSizeMode":
                // GrowOnly / GrowAndShrink, 记录以便后续布局使用
                element.setAttribute("data-autosizemode", strPropertyValue);
                return true;
            case "AutoValidate":
                element.setAttribute("data-autovalidate", strPropertyValue);
                return true;
            case "BorderStyle":
                // BorderStyle.None / FixedSingle / Fixed3D
                element.setAttribute("data-borderstyle", strPropertyValue);
                switch (strPropertyValue) {
                    case "FixedSingle":
                        element.style.border = "1px solid #888";
                        element.style.boxShadow = "";
                        break;
                    case "Fixed3D":
                        element.style.border = "1px solid #b0b0b0";
                        element.style.boxShadow = "inset 1px 1px 2px rgba(0,0,0,0.2)";
                        break;
                    // case "None":
                    default:
                        element.style.border = "none";
                        element.style.boxShadow = "";
                        break;
                }
                return true;
            default:
                return super.SetPropertyValue.call(this, element, strPropertyName, strPropertyValue, options);
        }
    }
};
if (window.__DCControlTypes == null) window.__DCControlTypes = new Object();
window.__DCControlTypes["System.Windows.Forms.UserControl"] = new SystemWindowsFormsUserControlFactory();
