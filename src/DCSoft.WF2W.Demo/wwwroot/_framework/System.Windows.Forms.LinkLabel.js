/** 使用HTML 元素模拟 System.Windows.Forms.LinkLabel 控件 */
"use strict";

class SystemWindowsFormsLinkLabelFactory extends SystemWindowsFormsControlFactory {
    /**
     * 根据选项来创建HTML元素来模拟 LinkLabel 控件
     * @param {any} options 控件选项，属性请参考 System.Windows.Forms.LinkLabel 类,必定包含一个Handle属性。
     * @returns {HTMLElement} 返回创建的 HTML 元素
     */
    Create(options) {
        if (options == null) {
            throw new Error("Options parameter is required to create a LinkLabel control.");
        }

        var html = options.UserPaint == true
            ? document.createElement("canvas")
            : document.createElement("a");

        html.setAttribute("dchandle", options.Handle);
        html.setAttribute("dctype", "System.Windows.Forms.LinkLabel");

        if (html.nodeName !== "CANVAS") {
            // 默认样式（尽量简单）
            html.href = "javascript:void(0)";
            html.style.cursor = "pointer";
            html.style.userSelect = "none";
            html.style.textDecoration = "underline";
            html.style.color = "#0066cc";

            // 记录颜色（用于 visited / active 切换）
            html.__dcLinkColor = "#0066cc";
            html.__dcVisitedLinkColor = "#800080";
            html.__dcActiveLinkColor = "";
            html.__dcLinkVisited = false;

            // 不进行页面跳转，但保留 click 冒泡供 Win32API 捕获
            html.addEventListener("click", function (e) { e.preventDefault(); });
            html.addEventListener("mousedown", function () {
                if (html.__dcActiveLinkColor) html.style.color = html.__dcActiveLinkColor;
            });
            html.addEventListener("mouseup", function () {
                html.style.color = html.__dcLinkVisited ? html.__dcVisitedLinkColor : html.__dcLinkColor;
            });
            html.addEventListener("mouseleave", function () {
                html.style.color = html.__dcLinkVisited ? html.__dcVisitedLinkColor : html.__dcLinkColor;
            });
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

    /** 设置属性值（保持简单，只处理常用项） */
    SetPropertyValue(element, strPropertyName, strPropertyValue, options) {
        if (!element || !strPropertyName) return false;

        switch (String(strPropertyName)) {
            case "Text":
                if (element.nodeName === "CANVAS") return true;
                element.textContent = (strPropertyValue == null) ? "" : String(strPropertyValue);
                return true;

            case "Enabled":
                if (element.nodeName === "CANVAS") return true;
                var enabled = (strPropertyValue !== false);
                element.style.opacity = enabled ? "" : "0.6";
                element.style.pointerEvents = enabled ? "" : "none";
                element.style.cursor = enabled ? "pointer" : "default";
                return true;

            case "LinkColor":
                if (element.nodeName === "CANVAS") return true;
                element.__dcLinkColor = (strPropertyValue == null) ? "" : String(strPropertyValue);
                element.style.color = element.__dcLinkVisited ? element.__dcVisitedLinkColor : element.__dcLinkColor;
                return true;

            case "VisitedLinkColor":
                if (element.nodeName === "CANVAS") return true;
                element.__dcVisitedLinkColor = (strPropertyValue == null) ? "" : String(strPropertyValue);
                element.style.color = element.__dcLinkVisited ? element.__dcVisitedLinkColor : element.__dcLinkColor;
                return true;

            case "ActiveLinkColor":
                if (element.nodeName === "CANVAS") return true;
                element.__dcActiveLinkColor = (strPropertyValue == null) ? "" : String(strPropertyValue);
                return true;

            case "LinkVisited":
                if (element.nodeName === "CANVAS") return true;
                element.__dcLinkVisited = !!strPropertyValue;
                element.style.color = element.__dcLinkVisited ? element.__dcVisitedLinkColor : element.__dcLinkColor;
                return true;

            default:
                return super.SetPropertyValue.call(this, element, strPropertyName, strPropertyValue, options);
        }
    }
};

if (window.__DCControlTypes == null) window.__DCControlTypes = new Object();
window.__DCControlTypes["System.Windows.Forms.LinkLabel"] = new SystemWindowsFormsLinkLabelFactory();