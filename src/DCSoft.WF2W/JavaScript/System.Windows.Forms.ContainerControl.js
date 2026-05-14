/** 使用HTML 元素模拟 System.Windows.Forms.ContainerControl 控件 */
"use strict";
class SystemWindowsFormsContainerControlFactory extends SystemWindowsFormsScrollableControlFactory {
    /**
    * 创建 ContainerControl 的 HTML 元素
    * @param {any} options 控件选项
    * @returns {HTMLElement}
    */
    Create (options) {
        if (!options) {
            throw new Error("Options parameter is required to create a ContainerControl.");
        }
        var html = super.Create.call(this , options);
        html.setAttribute("dctype", "System.Windows.Forms.ContainerControl");
        return html;
    }

    /** 设置属性值 */
    SetPropertyValue (element, strPropertyName, strPropertyValue, options) {
        if (!element) return false;
        switch (strPropertyName) {
            case "AutoValidate":
                element.setAttribute("data-autovalidate", strPropertyValue);
                return true;
            case "AutoScaleMode":
                element.setAttribute("data-autoscalemode", strPropertyValue);
                return true;
            case "ActiveControl":
                // 仅记录名称，实际激活由托管端处理
                if (strPropertyValue && strPropertyValue.Name) {
                    element.setAttribute("data-activecontrol", strPropertyValue.Name);
                } else if (typeof strPropertyValue === "string") {
                    element.setAttribute("data-activecontrol", strPropertyValue);
                }
                return true;
            default:
                return super.SetPropertyValue.call(this, element, strPropertyName, strPropertyValue, options);
        }
    }
};
if (window.__DCControlTypes == null) window.__DCControlTypes = new Object();
window.__DCControlTypes["System.Windows.Forms.ContainerControl"] = new SystemWindowsFormsContainerControlFactory();