/** 使用HTML 元素模拟 System.Windows.Forms.Label 控件*/
"use strict";
class SystemWindowsFormsLabelFactory extends SystemWindowsFormsControlFactory {
    /**
        * 根据选项来创建HTML元素来模拟 Label 控件
        * @param {any} options 控件选项，属性请参考 System.Windows.Forms.Label 类,必定包含一个Handle属性。
        * @returns { HTMLElement} 返回创建的 HTML 元素
        */
    Create(options) {
        var html = document.createElement("div");
        html.setAttribute("dchandle", options.Handle);
        html.setAttribute("dctype", "System.Windows.Forms.Label");
        html.style.cursor = "default";
        html.style.position = 'absolute';
        options.Win32API.BindingStandardControlEvent(html, "click|mousedown|mousemove|mouseup");
        //this.ApplyOptions(html, options);
        return html;
    }
    SetPropertyValue(element, strPropertyName, strPropertyValue) {
        if (!element || !strPropertyName) return false;
        var name = String(strPropertyName);
        var v = strPropertyValue;

        switch (name) {
            case 'Text': {
                var raw = v == null ? '' : String(v);
                var r = window.__DCRenderMnemonic(raw);
                element.innerHTML = r.html;
                if (r.access) element.setAttribute('accesskey', r.access);
                window.__DCEnsureMnemonicUnderlineStyle();
                return true;
            }
            case 'BorderStyle':
                switch (v) {
                    case 'None': element.style.border = 'none'; break;
                    case 'FixedSingle': element.style.border = '1px solid #000'; break;
                    case 'Fixed3D': element.style.border = '2px ridge #ccc'; break;
                    default: element.style.border = (v == null ? '' : v); break;
                }
                return true;
            case 'FlatStyle': {
                var fs = v;
                switch (fs) {
                    case 'Flat':
                        element.style.boxShadow = '';
                        if (!element.style.border || element.style.border === '') {
                            element.style.border = '1px solid rgba(0,0,0,0.2)';
                        }
                        element.style.background = 'transparent';
                        break;
                    case 'Popup':
                        element.style.boxShadow = '0 0 4px rgba(0,0,0,0.25)';
                        if (!element.style.border || element.style.border === '') {
                            element.style.border = '1px solid rgba(0,0,0,0.15)';
                        }
                        break;
                    case 'System':
                        element.style.boxShadow = '';
                        if (!element.style.border || element.style.border === '') {
                            element.style.border = '1px solid #ccc';
                        }
                        break;
                    case 'Standard':
                    default:
                        break;
                }
                return true;
            }
            case 'TextAlign': {
                var ta = v;
                element.style.display = 'flex';
                element.style.justifyContent = 'flex-start';
                element.style.alignItems = 'flex-start';
                switch (ta) {
                    case 'TopLeft': element.style.justifyContent = 'flex-start'; element.style.alignItems = 'flex-start'; element.style.textAlign = 'left'; break;
                    case 'TopCenter': element.style.justifyContent = 'center'; element.style.alignItems = 'flex-start'; element.style.textAlign = 'center'; break;
                    case 'TopRight': element.style.justifyContent = 'flex-end'; element.style.alignItems = 'flex-start'; element.style.textAlign = 'right'; break;
                    case 'MiddleLeft': element.style.justifyContent = 'flex-start'; element.style.alignItems = 'center'; element.style.textAlign = 'left'; break;
                    case 'MiddleCenter': element.style.justifyContent = 'center'; element.style.alignItems = 'center'; element.style.textAlign = 'center'; break;
                    case 'MiddleRight': element.style.justifyContent = 'flex-end'; element.style.alignItems = 'center'; element.style.textAlign = 'right'; break;
                    case 'BottomLeft': element.style.justifyContent = 'flex-start'; element.style.alignItems = 'flex-end'; element.style.textAlign = 'left'; break;
                    case 'BottomCenter': element.style.justifyContent = 'center'; element.style.alignItems = 'flex-end'; element.style.textAlign = 'center'; break;
                    case 'BottomRight': element.style.justifyContent = 'flex-end'; element.style.alignItems = 'flex-end'; element.style.textAlign = 'right'; break;
                    default: element.style.justifyContent = 'flex-start'; element.style.alignItems = 'flex-start'; element.style.textAlign = 'left'; break;
                }
                return true;
            }
            case 'AutoEllipsis':
                if (v === true) {
                    element.style.overflow = 'hidden';
                    element.style.textOverflow = 'ellipsis';
                    element.style.whiteSpace = 'nowrap';
                } else if (v === false) {
                    if (element.style.textOverflow === 'ellipsis') element.style.textOverflow = '';
                    if (element.style.overflow === 'hidden') element.style.overflow = '';
                    if (element.style.whiteSpace === 'nowrap') element.style.whiteSpace = '';
                }
                return true;
            default:
                return super.SetPropertyValue.call(this, element, strPropertyName, strPropertyValue);
        }
    }
};
if (window.__DCControlTypes == null) window.__DCControlTypes = new Object();
window.__DCControlTypes["System.Windows.Forms.Label"] = new SystemWindowsFormsLabelFactory();