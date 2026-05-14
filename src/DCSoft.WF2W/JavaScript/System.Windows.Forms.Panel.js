/** 使用HTML 元素模拟 System.Windows.Forms.Panel 控件 */
"use strict";
class SystemWindowsFormsPanelFactory extends SystemWindowsFormsScrollableControlFactory {
    /**
        * 创建HTML元素来模拟 Panel 控件
        * @param {any} options 控件选项，属性请参考 System.Windows.Forms.Panel 类, 必含 Handle
        * @returns {HTMLElement}
        */
    Create(options) {
       
       
        if (!options) throw new Error("Options parameter is required to create a Panel control.");
        var html = super.Create.call(this, options);
        html.setAttribute("dctype", "System.Windows.Forms.Panel");
        // Default TabStop false
        html.removeAttribute("tabindex");
        return html;
    }

    SetPropertyValue(el, strPropertyName, strPropertyValue) {
        if (el == null || strPropertyName == null) return false;

        function getContent(host) {
            for (var i = 0; i < host.children.length; i++) {
                if (host.children[i].className === 'dc-panel-content') return host.children[i];
            }
            return null;
        }

        switch (strPropertyName) {
            case 'Top':
                el.style.top = strPropertyValue + 'px';
                break;
            case 'Left':
                el.style.left = strPropertyValue + 'px';
                break;
            case 'Width':
                el.style.width = strPropertyValue + 'px';
                break;
            case 'Height':
                el.style.height = strPropertyValue + 'px';
                break;
            case 'AutoSize':
                el.setAttribute('data-autosize', !!strPropertyValue);
                if (strPropertyValue) {
                    var prevDisplay = el.style.display;
                    el.style.display = prevDisplay || 'inline-block';
                    el.style.width = 'auto';
                    el.style.height = 'auto';
                    var w2 = el.scrollWidth, h2 = el.scrollHeight;
                    el.style.width = Math.max(1, w2) + 'px';
                    el.style.height = Math.max(1, h2) + 'px';
                }
                return true;
            case 'BorderStyle':
                el.setAttribute('data-borderstyle', strPropertyValue);
                switch (strPropertyValue) {
                    case 'None': el.style.border = 'none'; el.style.boxShadow = ''; break;
                    case 'FixedSingle': el.style.border = '1px solid #888'; el.style.boxShadow = ''; break;
                    case 'Fixed3D': el.style.border = '1px solid #b0b0b0'; el.style.boxShadow = 'inset 1px 1px 2px rgba(0,0,0,0.2)'; break;
                    default: el.style.border = strPropertyValue; break;
                }
                return true;
            default:
                return super.SetPropertyValue.call(this, el, strPropertyName, strPropertyValue);
        }
    }
};
if (window.__DCControlTypes == null) window.__DCControlTypes = new Object();
window.__DCControlTypes["System.Windows.Forms.Panel"] = new SystemWindowsFormsPanelFactory();