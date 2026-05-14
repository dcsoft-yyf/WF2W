/** 使用HTML 元素模拟 System.Windows.Forms.Control 控件 */
"use strict";
class SystemWindowsFormsControlFactory {
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
        rootElement.setAttribute("dctype", "System.Windows.Forms.Control");
        // some sensible defaults
        rootElement.style.position = 'absolute';
        rootElement.style.display = 'inline-block';
        rootElement.style.whiteSpace = 'nowrap';
        rootElement.style.cursor = 'default';
        __DCWin32API.BindingStandardControlEvent(
            rootElement,
            "dblclick|click|mousedown|mousemove|mouseup|keydown|keypress|keyup");
        return rootElement;
    }
    GetClientSize(rootElement) {
        return rootElement.clientWidth + "|" + rootElement.clientHeight;
    }
    SetWindowText(element, text) {
    }

    GetWindowText(element) {
        return null;
    }
    /**
     * 获得用于绘制图形的 Canvas 元素
     * @param {HTMLElement} rootElement
     * @returns {HTMLCanvasElement} 返回 Canvas 元素
     */
    GetCanvasElement(rootElement) {
        if (rootElement.nodeName == "CANVAS") {
            return rootElement;
        }
        for (var node = rootElement.firstChild; node != null; node = node.nextSibling) {
            if (node.nodeName == "CANVAS") {
                return node;
            }
        }
        return null;
    }
    GetChildHostElement(rootElement) {
        return null;
    }
    /**
     * 销毁控件元素
     * @param {HTMLElement} htmlElement
     */
    DisposeElement(rootElement) {
        if (window.__DCWin32API && typeof window.__DCWin32API.ClearStandardControlEvent === "function") {
            window.__DCWin32API.ClearStandardControlEvent(rootElement);
        }
        if (rootElement.__resizeObserver) {
            rootElement.__resizeObserver.disconnect();
            rootElement.__resizeObserver = null;
        }
        function RemoveChilds(re) {
            while (re.firstChild != null) {
                var fact = re.firstChild.__DCFactory;
                if (fact != null) {
                    var ele = re.firstChild;
                    if (fact.DisposeElement == null) {
                        throw "error:DisposeElement == null";
                    }
                    fact.DisposeElement.call(fact, ele);
                    if (re.firstChild == ele) {
                        re.removeChild(ele);
                    }
                }
                else {
                    RemoveChilds(re.firstChild);
                    if (re.firstChild != null) {
                        re.removeChild(re.firstChild);
                    }
                }
            }
        }
        RemoveChilds(rootElement);
        if (rootElement.parentNode != null) {
            rootElement.parentNode.removeChild(rootElement);
        }
        rootElement.__DCFactory = null;
    }
    /**
     * 根据选项来设置HTML元素来模拟 Button 控件
     * @param {any} htmlElement HTML元素
     * @param {any} options 控件选项，属性请参考 System.Windows.Forms.Button 类
     */
    ApplyOptions(htmlElement, options) {
        if (htmlElement == null || options == null) return;
        for (var strKey in options) {
            if (strKey === 'Win32API') continue;
            var strValue = options[strKey];
            this.SetPropertyValue.call(this, htmlElement, strKey, strValue);
        }
    }
    /**
     * 设置控件的属性
     * @param {HTMLElement} element 控件对应的 HTML 元素
     * @param {string} strPropertyName 属性名称
     * @param {any} strPropertyValue 属性值
     */
    SetPropertyValue(element, strPropertyName, strPropertyValue) {
        if (element == null) return false;
        switch (strPropertyName) {
            case "ForeColor":
                element.style.color = strPropertyValue;
                return true;
            case "BackColor":
                element.style.backgroundColor = strPropertyValue;
                return true;
            case "Left":
                element.style.left = strPropertyValue + 'px';
                return true;
            case "Top":
                element.style.top = strPropertyValue + 'px';
                return true;
            case "Width":
                element.style.width = strPropertyValue + 'px';
                return true;
            case "Height":
                element.style.height = strPropertyValue + 'px';
                return true;
            case "Visible":
                element.style.display = (strPropertyValue === false) ? 'none' : '';
                return true;
            case "Enabled":
                if (strPropertyValue === false) {
                    element.setAttribute('disabled', 'disabled');
                    element.style.cursor = 'default';
                    element.style.opacity = '0.6';
                    element.setAttribute('aria-disabled', 'true');
                } else {
                    element.removeAttribute('disabled');
                    element.style.cursor = 'default';
                    element.style.opacity = '';
                    element.removeAttribute('aria-disabled');
                }
                return true;
            case "Font":
                if (element && typeof __DCWin32API !== 'undefined' && window.__DCWin32API.SetControlFont) {
                    __DCWin32API.SetControlFont(element, strPropertyValue);
                }
                return true;
            case "Cursor":
                element.style.cursor = strPropertyValue || 'default';
                return true;
            case "Padding":
                if (typeof strPropertyValue === 'number') element.style.padding = strPropertyValue + 'px';
                else element.style.padding = strPropertyValue;
                return true;
            case "TabStop":
                if (strPropertyValue) {
                    if (!element.hasAttribute('tabindex')) element.tabIndex = 0;
                } else {
                    element.removeAttribute('tabindex');
                }
                return true;
            case "TabIndex":
                element.tabIndex = strPropertyValue;
                return true;
        }
        return false;
    }
};
if (window.__DCControlTypes == null) window.__DCControlTypes = new Object();
window.__DCControlTypes["System.Windows.Forms.Control"] = new SystemWindowsFormsControlFactory();
