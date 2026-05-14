/** 使用HTML 元素模拟 System.Windows.Forms.GroupBox 控件 */
"use strict";

class SystemWindowsFormsGroupBoxFactory extends SystemWindowsFormsPanelFactory {
    /**
     * 创建 HTML 元素模拟 GroupBox（简单版）
     * @param {any} options 控件选项，属性请参考 System.Windows.Forms.GroupBox 类,必含 Handle
     * @returns {HTMLElement}
     */
    Create(options) {
        if (!options) throw new Error("Options parameter is required to create a GroupBox control.");

        var html = super.Create.call(this, options);
        html.setAttribute("dctype", "System.Windows.Forms.GroupBox");
        // GroupBox 不需要 TabStop（和 Panel 一样默认不聚焦）
        html.removeAttribute("tabindex");

        // 基本外观（尽量简单）
        html.style.boxSizing = "border-box";
        html.style.border = "1px solid #888";

        // 子容器默认留出标题区域
        var host = html.__childHost || html;
        host.style.boxSizing = "border-box";
        if (!host.style.paddingTop) host.style.paddingTop = "10px";

        // 标题（浮在边框上）
        var title = document.createElement("div");
        title.className = "dc-groupbox-title";
        title.style.position = "absolute";
        title.style.left = "10px";
        title.style.top = "-0.6em";
        title.style.padding = "0 4px";
        title.style.cursor = "default";
        title.style.userSelect = "none";
        title.style.whiteSpace = "nowrap";
        title.style.backgroundColor = html.style.backgroundColor || "#fff";
        title.textContent = "";
        html.__title = title;
        html.appendChild(title);

        return html;
    }

    SetPropertyValue(el, strPropertyName, strPropertyValue, options) {
        if (!el || strPropertyName == null) return false;

        switch (strPropertyName) {
            case "Text":
                if (el.__title) {
                    var t = (strPropertyValue == null) ? "" : String(strPropertyValue);
                    el.__title.textContent = t;
                    el.__title.style.display = t ? "" : "none";
                }
                return true;

            case "BackColor":
                // 先让基类设置背景色，再同步标题背景
                super.SetPropertyValue.call(this, el, strPropertyName, strPropertyValue, options);
                if (el.__title) {
                    el.__title.style.backgroundColor = (strPropertyValue != null) ? String(strPropertyValue) : (el.style.backgroundColor || "");
                }
                return true;

            case "ForeColor":
                super.SetPropertyValue.call(this, el, strPropertyName, strPropertyValue, options);
                if (el.__title) el.__title.style.color = (strPropertyValue == null) ? "" : String(strPropertyValue);
                return true;

            case "Enabled":
                // WinForms：禁用 GroupBox 会连带禁用子控件，这里用 pointerEvents 简单实现
                super.SetPropertyValue.call(this, el, strPropertyName, strPropertyValue, options);
                el.style.pointerEvents = (strPropertyValue === false) ? "none" : "";
                return true;

            default:
                return super.SetPropertyValue.call(this, el, strPropertyName, strPropertyValue, options);
        }
    }
};

if (window.__DCControlTypes == null) window.__DCControlTypes = new Object();
window.__DCControlTypes["System.Windows.Forms.GroupBox"] = new SystemWindowsFormsGroupBoxFactory();