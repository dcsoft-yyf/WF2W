/** 使用HTML 元素模拟 System.Windows.Forms.Button 控件 */
"use strict";

class SystemWindowsFormsButtonFactory extends SystemWindowsFormsControlFactory {
    /**
     * 根据选项来创建HTML元素来模拟 Button 控件
     * @param {any} options 控件选项，属性请参考 System.Windows.Forms.Button 类,必定包含一个Handle属性。
     * @returns { HTMLElement} 返回创建的 HTML 元素
     */
    Create(options) {
        if (options == null) {
            throw new Error("Options parameter is required to create a Button control.");
        }
        var rootElement = null;
        if (options.UserPaint == true) {
            rootElement = document.createElement("canvas");
            if (typeof ResizeObserver !== "undefined") {
                rootElement.__resizeObserver = new ResizeObserver(() => {
                    rootElement.width = rootElement.clientWidth;
                    rootElement.height = rootElement.clientHeight;
                });
                rootElement.__resizeObserver.observe(rootElement);
            }
        }
        else {
            rootElement = document.createElement("button");
        }
        rootElement.setAttribute("dchandle", options.Handle);
        rootElement.setAttribute("dctype", "System.Windows.Forms.Button");
        // some sensible defaults
        rootElement.style.position = 'absolute';
        rootElement.style.display = 'inline-block';
        rootElement.style.whiteSpace = 'nowrap';
        rootElement.style.cursor = 'default';
        options.Win32API.BindingStandardControlEvent(
            rootElement,
            "mousedown|mousemove|mouseup|keydown|keypress|keyup");
        //this.ApplyOptions(html, options);
        return rootElement;
    }
    DisposeElement(rootElement) {
        super.DisposeElement(rootElement);
        if (rootElement.__resizeObserver) {
            rootElement.__resizeObserver.disconnect();
            rootElement.__resizeObserver = null;
        }
    }
    /**
     * 设置按钮控件的属性
     * @param {HTMLElement} element 控件对应的 HTML 元素
     * @param {string} strPropertyName 属性名称
     * @param {any} strPropertyValue 属性值
     */
    SetPropertyValue(element, strPropertyName, strPropertyValue) {
        if (element == null) return false;
        function applyText(el, raw) {
            var r = window.__DCRenderMnemonic(raw);
            el.innerHTML = r.html;
            if (r.access) el.setAttribute('accesskey', r.access);
            el.style.textDecorationSkipInk = 'none';
            window.__DCEnsureMnemonicUnderlineStyle();
        }

        function applyTextAlign(el, align) {
            if (!align) return;
            el.style.display = 'inline-flex';
            el.style.alignItems = 'center';
            el.style.justifyContent = 'center';
            switch (align) {
                case 'TopLeft': el.style.alignItems = 'flex-start'; el.style.justifyContent = 'flex-start'; el.style.textAlign = 'left'; break;
                case 'TopCenter': el.style.alignItems = 'flex-start'; el.style.justifyContent = 'center'; el.style.textAlign = 'center'; break;
                case 'TopRight': el.style.alignItems = 'flex-start'; el.style.justifyContent = 'flex-end'; el.style.textAlign = 'right'; break;
                case 'MiddleLeft': el.style.alignItems = 'center'; el.style.justifyContent = 'flex-start'; el.style.textAlign = 'left'; break;
                case 'MiddleCenter': el.style.alignItems = 'center'; el.style.justifyContent = 'center'; el.style.textAlign = 'center'; break;
                case 'MiddleRight': el.style.alignItems = 'center'; el.style.justifyContent = 'flex-end'; el.style.textAlign = 'right'; break;
                case 'BottomLeft': el.style.alignItems = 'flex-end'; el.style.justifyContent = 'flex-start'; el.style.textAlign = 'left'; break;
                case 'BottomCenter': el.style.alignItems = 'flex-end'; el.style.justifyContent = 'center'; el.style.textAlign = 'center'; break;
                case 'BottomRight': el.style.alignItems = 'flex-end'; el.style.justifyContent = 'flex-end'; el.style.textAlign = 'right'; break;
            }
        }

        function applyImageAlign(el, align) {
            if (!align) return;
            // reset flex to allow alignment with text
            el.style.display = 'inline-flex';
            el.style.alignItems = el.style.alignItems || 'center';
            el.style.justifyContent = el.style.justifyContent || 'center';
            // no extra positioning here; layout kept simple for owner-draw layer
        }

        switch (strPropertyName) {
            case 'Text':
                if (element.nodeName != "CANVAS") {
                    applyText(element, strPropertyValue);
                }
                return true;
            case 'Image':
                if (element.nodeName != "CANVAS") {
                    for (var i = element.children.length - 1; i >= 0; i--) {
                        var c = element.children[i];
                        if (c.tagName && c.tagName.toLowerCase() === 'img' && c.getAttribute('data-dc-image') === '1') element.removeChild(c);
                    }
                    element.style.backgroundImage = '';
                    if (strPropertyValue != null) {
                        var img = document.createElement('img');
                        img.src = strPropertyValue;
                        if (window.__DCWin32API && typeof window.__DCWin32API.CheckForRealseBlobImageUrl === 'function') {
                            window.__DCWin32API.CheckForRealseBlobImageUrl(img);
                        }
                        img.setAttribute('data-dc-image', '1');
                        img.style.maxHeight = '100%';
                        img.style.maxWidth = '100%';
                        img.style.verticalAlign = 'middle';
                        img.style.pointerEvents = 'none';
                        element.insertBefore(img, element.firstChild);
                    }
                }
                return true;
            case 'ImageAlign':
                applyImageAlign(element, strPropertyValue);
                return true;
            case 'TextAlign':
                applyTextAlign(element, strPropertyValue);
                return true;
            case 'FlatStyle':
                switch (strPropertyValue) {
                    case 'Flat':
                        element.style.border = '1px solid #888';
                        element.style.boxShadow = 'none';
                        break;
                    case 'Popup':
                        element.style.border = '1px solid #666';
                        break;
                    case 'Standard':
                    default:
                        element.style.border = element.style.border || '1px solid #777';
                        break;
                }
                return true;
            case 'BorderStyle':
                switch (strPropertyValue) {
                    case 'None': element.style.border = 'none'; break;
                    case 'FixedSingle': element.style.border = '1px solid #000'; break;
                    case 'Fixed3D': element.style.border = '2px ridge #ccc'; break;
                    default: element.style.border = strPropertyValue; break;
                }
                return true;
            case 'AutoSize':
                if (element.nodeName != "CANVAS") {
                    if (strPropertyValue) {
                        var prevDisplay = element.style.display;
                        element.style.width = 'auto';
                        element.style.height = 'auto';
                        element.style.display = prevDisplay || 'inline-block';
                        var w = element.scrollWidth;
                        var h = element.scrollHeight;
                        element.style.width = (w < 1 ? 1 : w) + 'px';
                        element.style.height = (h < 1 ? 1 : h) + 'px';
                    }
                }
                return true;
            case "Text":
                if (element.nodeName != "CANVAS") {
                    element.innerText = strPropertyValue == null ? '' : strPropertyValue;
                }
                return true;
            default:
                return super.SetPropertyValue.call(this, element, strPropertyName, strPropertyValue);
        }
        return false;
    }
};
if (window.__DCControlTypes == null) window.__DCControlTypes = new Object();
window.__DCControlTypes["System.Windows.Forms.Button"] = new SystemWindowsFormsButtonFactory();