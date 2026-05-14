/** 使用HTML 元素模拟 System.Windows.Forms.PictureBox 控件 */
"use strict";
class SystemWindowsFormsPictureBoxFactory extends SystemWindowsFormsControlFactory {
    /**
     * 创建HTML元素来模拟 PictureBox 控件
     * @param {any} options 控件选项，属性请参考 System.Windows.Forms.PictureBox 类, 必含 Handle
     * @returns {HTMLElement}
     */
    Create(options) {
        if (!options) throw new Error("Options parameter is required to create a PictureBox control.");
        var rootElement = document.createElement("div");
        rootElement.setAttribute("dchandle", options.Handle);
        rootElement.setAttribute("dctype", "System.Windows.Forms.PictureBox");
        rootElement.style.position = 'absolute';
        rootElement.style.display = 'block';
        rootElement.style.boxSizing = 'border-box';
        rootElement.style.overflow = 'hidden';
        var canvas = null;
        if (options.UserPaint == true) {
            canvas = document.createElement('canvas');
            canvas.style.position = 'absolute';
            canvas.style.left = '0';
            canvas.style.top = '0';
            canvas.style.width = '100%';
            canvas.style.height = '100%';
            canvas.style.pointerEvents = 'none';
        }
        var img = document.createElement('img');
        img.setAttribute('data-dc-picture', '1');
        img.style.width = '100%';
        img.style.height = '100%';
        img.style.objectFit = 'contain';
        img.style.userSelect = 'none';
        img.style.pointerEvents = 'none';
        img.style.position = 'absolute';
        img.style.left = '0';
        img.style.top = '0';
        rootElement.appendChild(img);
        if (canvas) {
            rootElement.appendChild(canvas); // canvas after img
            rootElement.__canvas = canvas;
        }

        rootElement.DoChildLayout = function () {
            if (this.__canvas) {
                var w = this.clientWidth || 0;
                var h = this.clientHeight || 0;
                if (this.__canvas.width !== w) this.__canvas.width = w;
                if (this.__canvas.height !== h) this.__canvas.height = h;
            }
        };
        rootElement.DoChildLayout();
        if (typeof ResizeObserver !== "undefined" && rootElement.__canvas) {
            rootElement.__resizeObserver = new ResizeObserver(() => {
                rootElement.DoChildLayout();
            });
            rootElement.__resizeObserver.observe(rootElement);
        }

        if (options && options.Win32API && options.Win32API.BindingStandardControlEvent) {
            options.Win32API.BindingStandardControlEvent(rootElement, "click|mousedown|mousemove|mouseup|wheel");
        }
        return rootElement;
    }
    DisposeElement(element) {
        super.DisposeElement.call(this, element);
        if (element.__resizeObserver) {
            element.__resizeObserver.unobserve(element);
            element.__resizeObserver = null;
        }
    }
    SetPropertyValue(el, strPropertyName, strPropertyValue) {
        if (!el || !strPropertyName) return false;

        function getImg(host) {
            for (var i = 0; i < host.children.length; i++) {
                var c = host.children[i];
                if (c.tagName && c.tagName.toLowerCase() === 'img' && c.getAttribute('data-dc-picture') === '1') return c;
            }
            return null;
        }

        switch (strPropertyName) {
            case 'Image': {
                var img = getImg(el) || (function () { var i = document.createElement('img'); i.setAttribute('data-dc-picture', '1'); el.appendChild(i); return i; })();
                img.src = strPropertyValue || '';
                if (window.__DCWin32API && typeof window.__DCWin32API.CheckForRealseBlobImageUrl === 'function') {
                    window.__DCWin32API.CheckForRealseBlobImageUrl(img);
                }
                return true;
            }
            case 'SizeMode': {
                var img2 = getImg(el);
                if (!img2) return true;
                switch (strPropertyValue) {
                    case 'Normal':
                        img2.style.objectFit = 'none';
                        img2.style.width = '';
                        img2.style.height = '';
                        img2.style.maxWidth = '';
                        img2.style.maxHeight = '';
                        img2.style.position = 'relative';
                        img2.style.left = '0px';
                        img2.style.top = '0px';
                        img2.style.transform = '';
                        break;
                    case 'StretchImage':
                        img2.style.objectFit = 'fill';
                        img2.style.width = '100%';
                        img2.style.height = '100%';
                        img2.style.position = 'relative';
                        img2.style.transform = '';
                        break;
                    case 'AutoSize':
                        img2.style.objectFit = 'none';
                        img2.style.width = 'auto';
                        img2.style.height = 'auto';
                        img2.style.position = 'relative';
                        img2.style.transform = '';
                        if (img2.naturalWidth && img2.naturalHeight) {
                            el.style.width = img2.naturalWidth + 'px';
                            el.style.height = img2.naturalHeight + 'px';
                        }
                        break;
                    case 'CenterImage':
                        img2.style.objectFit = 'none';
                        img2.style.width = '';
                        img2.style.height = '';
                        img2.style.maxWidth = '100%';
                        img2.style.maxHeight = '100%';
                        img2.style.position = 'absolute';
                        img2.style.left = '50%';
                        img2.style.top = '50%';
                        img2.style.transform = 'translate(-50%, -50%)';
                        break;
                    case 'Zoom':
                    default:
                        img2.style.objectFit = 'contain';
                        img2.style.width = '100%';
                        img2.style.height = '100%';
                        img2.style.position = 'relative';
                        img2.style.transform = '';
                        break;
                }
                return true;
            }
            default:
                return super.SetPropertyValue.call(this, el, strPropertyName, strPropertyValue);
        }
    }

}
if (window.__DCControlTypes == null) window.__DCControlTypes = new Object();
window.__DCControlTypes["System.Windows.Forms.PictureBox"] = new SystemWindowsFormsPictureBoxFactory();window.__DCControlTypes["System.Windows.Forms.PictureBox"] = new SystemWindowsFormsPictureBoxFactory();