/** 使用HTML 元素模拟 System.Windows.Forms.TextBox 控件*/
"use strict";
class SystemWindowsFormsTextBoxFactory extends SystemWindowsFormsControlFactory {
    /**
        * 根据选项来创建HTML元素来模拟 TextBox 控件
        * @param {any} options 控件选项，属性请参考 System.Windows.Forms.TextBox 类, 必定包含一个Handle属性。
        * @returns { HTMLElement} 返回创建的 HTML 元素
        */
    Create(options) {
        options = options || {};
        var html;
        // Multiline -> use <textarea>, otherwise <input>
        if (options.Multiline === true) {
            html = document.createElement("textarea");
            // Disable resize handle (Windows 11 bottom-right drag area)
            html.style.resize = "none";
        } else {
            html = document.createElement("input");
            html.type = (options.PasswordChar || options.UseSystemPasswordChar) ? "password" : "text";
        }
        html.setAttribute("dchandle", options.Handle);
        html.setAttribute("dctype", "System.Windows.Forms.TextBox");
        // default box model
        html.style.position = "absolute";
        html.style.boxSizing = "border-box";
        options.Win32API.BindingStandardControlEvent(html, "click");// "click|mousedown|mousemove|mouseup");
        //this.ApplyOptions(html, options);
        return html;
    }
    SetWindowText(element, text) {
        element.value = text;
    }

    GetWindowText(element) {
        return element.value;
    }

    SetPropertyValue(element, strPropertyName, strPropertyValue) {
        if (element == null || strPropertyName == null) return false;

        var isTextarea = element.tagName && element.tagName.toLowerCase() === 'textarea';

        switch (strPropertyName) {
            case 'Text':
                element.value = strPropertyValue == null ? '' : String(strPropertyValue);
                return true;
            case 'ReadOnly':
                if (strPropertyValue) element.setAttribute('readonly', 'readonly');
                else element.removeAttribute('readonly');
                return true;
            case 'AcceptsReturn':
                element.setAttribute('data-accepts-return', strPropertyValue ? 'true' : 'false');
                if (isTextarea && strPropertyValue === false) {
                    element.onkeydown = function (ev) {
                        ev = ev || window.event;
                        if (!ev) return;
                        var code = (typeof ev.keyCode === 'number' ? ev.keyCode : (typeof ev.which === 'number' ? ev.which : 0)) | 0;
                        if (code === 13) {
                            ev.preventDefault();
                            return false;
                        }
                    };
                }
                return true;
            case 'MaxLength':
                var ml = parseInt(strPropertyValue, 10);
                if (!isNaN(ml) && ml >= 0) element.maxLength = ml;
                return true;
            case 'PasswordChar':
            case 'UseSystemPasswordChar':
                if (!isTextarea) {
                    var usePwd = !!strPropertyValue;
                    element.type = usePwd ? 'password' : 'text';
                }
                return true;
            case 'AutoCompleteSource':
                if (!isTextarea) {
                    var src = String(strPropertyValue || 'None');
                    element.autocomplete = (src === 'None') ? 'off' : 'on';
                    element.setAttribute('data-autocomplete-source', src);
                }
                return true;
            case 'AutoCompleteMode':
                if (!isTextarea) {
                    element.setAttribute('data-autocomplete-mode', String(strPropertyValue));
                }
                return true;
            case 'CharacterCasing':
                switch (strPropertyValue) {
                    case 'Upper': element.style.textTransform = 'uppercase'; break;
                    case 'Lower': element.style.textTransform = 'lowercase'; break;
                    default: element.style.textTransform = 'none'; break;
                }
                return true;
            case 'TextAlign':
                switch (strPropertyValue) {
                    case 'Left': element.style.textAlign = 'left'; break;
                    case 'Center': element.style.textAlign = 'center'; break;
                    case 'Right': element.style.textAlign = 'right'; break;
                    default: element.style.textAlign = 'left'; break;
                }
                return true;
            case 'WordWrap':
                if (isTextarea) {
                    element.style.whiteSpace = strPropertyValue ? 'pre-wrap' : 'pre';
                }
                return true;
            case 'ScrollBars':
                if (isTextarea) {
                    switch (strPropertyValue) {
                        case 'None': element.style.overflow = 'hidden'; break;
                        case 'Horizontal': element.style.overflowX = 'auto'; element.style.overflowY = 'hidden'; break;
                        case 'Vertical': element.style.overflowY = 'auto'; element.style.overflowX = 'hidden'; break;
                        case 'Both': element.style.overflow = 'auto'; break;
                    }
                }
                return true;
            case 'PlaceholderText':
                element.placeholder = strPropertyValue || '';
                return true;
            case 'SelectionStart':
            case 'SelectionLength':
                try {
                    var start = (strPropertyName === 'SelectionStart') ? (strPropertyValue || 0) : (element.selectionStart || 0);
                    var len = (strPropertyName === 'SelectionLength') ? (strPropertyValue || 0) : ((element.selectionEnd || 0) - (element.selectionStart || 0));
                    element.setSelectionRange(start, start + len);
                } catch (e) { }
                return true;
            case 'Multiline':
                // Cannot safely swap element here; ignore and return false so base may handle if needed.
                return false;
            case 'Enabled':
             
                if (strPropertyValue === false) {
                    element.setAttribute('disabled', 'disabled');
                    element.style.cursor = 'default';
                    element.style.opacity = '0.6';
                    element.setAttribute('aria-disabled', 'true');
                } else {
                    element.removeAttribute('disabled');
                    element.style.cursor = 'text'; // Preserve TextBox cursor style
                    element.style.opacity = '';
                    element.removeAttribute('aria-disabled');
                }
                return true;

            default:
                return super.SetPropertyValue.call(this, element, strPropertyName, strPropertyValue);
        }
    }

    /**
        * 根据选项来设置HTML元素来模拟 TextBox 控件
        * @param {any} htmlElement HTML元素 (input 或 textarea)
        * @param {any} options 控件选项，属性请参考 System.Windows.Forms.TextBox 类
        */
    ApplyOptions(htmlElement, options) {
        if (htmlElement == null || options == null) return;
        for (var strKey in options) {
            var strValue = options[strKey];
            this.SetPropertyValue.call(this, htmlElement, strKey, strValue);
        }
    }
};

if (window.__DCControlTypes == null) window.__DCControlTypes = new Object();
window.__DCControlTypes["System.Windows.Forms.TextBox"] = new SystemWindowsFormsTextBoxFactory();