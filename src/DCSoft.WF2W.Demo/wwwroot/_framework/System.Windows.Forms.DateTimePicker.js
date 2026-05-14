"use strict";

/** 使用HTML 元素模拟 System.Windows.Forms.DateTimePicker 控件 */
class SystemWindowsFormsDateTimePickerFactory extends SystemWindowsFormsControlFactory {
    /**
     * 根据选项来创建HTML元素来模拟 DateTimePicker 控件
     * @param {any} options 控件选项，属性请参考 System.Windows.Forms.DateTimePicker 类,必定包含一个Handle属性。
     * @returns { HTMLElement} 返回创建的 HTML 元素
     */
    Create(options) {
        if (options == null) {
            throw new Error("Options parameter is required to create a DateTimePicker control.");
        }

        if (options.UserPaint === true) {
            var canvas = document.createElement("canvas");
            canvas.setAttribute("dchandle", options.Handle);
            canvas.setAttribute("dctype", "System.Windows.Forms.DateTimePicker");
            canvas.style.position = 'absolute';
            canvas.style.display = 'inline-block';
            canvas.style.cursor = 'default';
            if (typeof ResizeObserver !== "undefined") {
                canvas.__resizeObserver = new ResizeObserver(() => {
                    canvas.width = canvas.clientWidth;
                    canvas.height = canvas.clientHeight;
                });
                canvas.__resizeObserver.observe(canvas);
            }
            if (options.Win32API && options.Win32API.BindingStandardControlEvent) {
                options.Win32API.BindingStandardControlEvent(canvas, "mousedown|mousemove|mouseup|keydown|keypress|keyup");
            }
            return canvas;
        }

        var root = document.createElement("div");
        root.setAttribute("dchandle", options.Handle);
        root.setAttribute("dctype", "System.Windows.Forms.DateTimePicker");
        root.style.position = "absolute";
        root.style.display = "inline-flex";
        root.style.alignItems = "center";
        root.style.boxSizing = "border-box";
        root.style.cursor = "text";
        root.style.userSelect = "none";
        root.style.borderRadius = "8px";
        root.style.padding = "6px 10px";
        root.style.gap = "8px";
        root.style.minWidth = "120px";
        root.style.background = options.BackColor || "#fff";
        root.style.color = options.ForeColor || "#222";
        root.style.border = "1px solid rgba(16,24,40,0.12)";
        root.style.boxShadow = "0 1px 3px rgba(16,24,40,0.06)";

        var display = document.createElement("div");
        display.style.flex = "1 1 auto";
        display.style.overflow = "hidden";
        display.style.whiteSpace = "nowrap";
        display.style.textOverflow = "ellipsis";
        display.style.pointerEvents = "none";

        var button = document.createElement("div");
        button.style.width = "28px";
        button.style.height = "28px";
        button.style.display = "inline-flex";
        button.style.alignItems = "center";
        button.style.justifyContent = "center";
        button.style.borderRadius = "6px";
        button.style.cursor = "pointer";
        button.style.flex = "0 0 auto";
        button.style.background = "transparent";
        button.style.transition = "background .12s ease, color .12s ease";
        button.innerHTML = '<svg width="14" height="14" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg" aria-hidden="true"><path d="M7 10l5 5 5-5" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"/></svg>';

        var input = document.createElement("input");
        input.type = "date";
        input.style.position = "absolute";
        input.style.left = "-9999px";
        input.style.opacity = "0";
        input.setAttribute("aria-hidden", "true");

        root.appendChild(display);
        root.appendChild(button);
        root.appendChild(input);

        root.__dcDisplay = display;
        root.__dcButton = button;
        root.__dcInput = input;
        root.__dcFormat = options.Format || "Short";
        root.__dcCustomFormat = options.CustomFormat || "yyyy-MM-dd";
        root.__dcValue = null;

        var parseToDate = function (value) {
            if (value == null) return null;
            if (value instanceof Date) return value;
            if (typeof value === "number") {
                var dnum = new Date(value);
                return isNaN(dnum.getTime()) ? null : dnum;
            }
            var d = new Date(String(value));
            return isNaN(d.getTime()) ? null : d;
        };

        var getInputType = function (format, customFormat) {
            if (format === "Time") return "time";
            if (format === "Custom") {
                if (customFormat && /[Hhms]/.test(customFormat)) return "datetime-local";
            }
            return "date";
        };

        var formatForInput = function (date, type) {
            if (!date) return "";
            var yyyy = date.getFullYear().toString().padStart(4, "0");
            var MM = (date.getMonth() + 1).toString().padStart(2, "0");
            var dd = date.getDate().toString().padStart(2, "0");
            var HH = date.getHours().toString().padStart(2, "0");
            var mm = date.getMinutes().toString().padStart(2, "0");
            var ss = date.getSeconds().toString().padStart(2, "0");
            if (type === "time") return `${HH}:${mm}`;
            if (type === "datetime-local") return `${yyyy}-${MM}-${dd}T${HH}:${mm}`;
            return `${yyyy}-${MM}-${dd}`;
        };

        var formatDisplay = function (date, format, customFormat) {
            if (!date) return "";
            switch (format) {
                case "Long":
                    return date.toLocaleString(undefined, { weekday: "long", year: "numeric", month: "long", day: "numeric" });
                case "Time":
                    return date.toLocaleTimeString(undefined, { hour: "2-digit", minute: "2-digit", second: "2-digit" });
                case "Custom":
                    var fmt = customFormat || "yyyy-MM-dd";
                    return fmt
                        .replace(/yyyy/g, date.getFullYear())
                        .replace(/MM/g, String(date.getMonth() + 1).padStart(2, "0"))
                        .replace(/dd/g, String(date.getDate()).padStart(2, "0"))
                        .replace(/HH/g, String(date.getHours()).padStart(2, "0"))
                        .replace(/mm/g, String(date.getMinutes()).padStart(2, "0"))
                        .replace(/ss/g, String(date.getSeconds()).padStart(2, "0"));
                case "Short":
                default:
                    return date.toLocaleDateString();
            }
        };

        root.__dcUpdateDisplay = function () {
            var d = root.__dcValue;
            root.__dcDisplay.textContent = formatDisplay(d, root.__dcFormat, root.__dcCustomFormat);
            var type = getInputType(root.__dcFormat, root.__dcCustomFormat);
            if (root.__dcInput.type !== type) root.__dcInput.type = type;
            root.__dcInput.value = formatForInput(d, type);
        };

        var setValueFromInput = function () {
            var type = root.__dcInput.type || "date";
            var raw = root.__dcInput.value || "";
            if (!raw) {
                root.__dcValue = null;
            } else if (type === "time") {
                var parts = raw.split(":");
                var now = root.__dcValue || new Date();
                now.setHours(parseInt(parts[0] || "0", 10), parseInt(parts[1] || "0", 10), 0, 0);
                root.__dcValue = now;
            } else if (type === "datetime-local") {
                root.__dcValue = new Date(raw);
            } else {
                root.__dcValue = new Date(raw + "T00:00:00");
            }
            root.__dcUpdateDisplay();
        };

        var openPicker = function () {
            if (root.getAttribute("aria-disabled") === "true") return;
            root.__dcInput.focus();
            if (typeof root.__dcInput.showPicker === "function") {
                root.__dcInput.showPicker();
            } else {
                try { root.__dcInput.click(); } catch (e) { }
            }
        };

        button.addEventListener("mouseenter", function () { button.style.background = "rgba(0,0,0,0.04)"; });
        button.addEventListener("mouseleave", function () { button.style.background = "transparent"; });
        button.addEventListener("mousedown", function (e) { e.stopPropagation(); openPicker(); });
        root.addEventListener("click", function () { openPicker(); });
        root.addEventListener("focusin", function () { root.style.borderColor = "#4a89ff"; root.style.boxShadow = "0 0 0 3px rgba(74,137,255,0.15)"; });
        root.addEventListener("focusout", function () { root.style.borderColor = "rgba(16,24,40,0.12)"; root.style.boxShadow = "0 1px 3px rgba(16,24,40,0.06)"; });

        root.__dcInput.addEventListener("change", function () {
            setValueFromInput();
            root.dispatchEvent(new Event("change", { bubbles: true }));
        });

        if (options.Win32API && options.Win32API.BindingStandardControlEvent) {
            options.Win32API.BindingStandardControlEvent(root, "click|mousedown|mousemove|mouseup|keydown|keypress|keyup|change");
            options.Win32API.BindingStandardControlEvent(root.__dcInput, "change|keydown|keyup");
        }

        if (options.Value != null) {
            root.__dcValue = parseToDate(options.Value);
        }
        root.__dcUpdateDisplay();

        return root;
    }

    ApplyOptions(htmlElement, options) {
        if (!htmlElement || !options) return;
        for (var strKey in options) {
            if (!Object.prototype.hasOwnProperty.call(options, strKey) || strKey === "Win32API") continue;
            this.SetPropertyValue.call(this, htmlElement, strKey, options[strKey], options);
        }
    }

    SetPropertyValue(element, strPropertyName, strPropertyValue) {
        if (!element) return false;

        if (element.nodeName === "CANVAS") {
            return super.SetPropertyValue.call(this, element, strPropertyName, strPropertyValue);
        }

        switch (strPropertyName) {
            case "Value":
                element.__dcValue = (function (value) {
                    if (value == null) return null;
                    if (value instanceof Date) return value;
                    if (typeof value === "number") {
                        var dnum = new Date(value);
                        return isNaN(dnum.getTime()) ? null : dnum;
                    }
                    var d = new Date(String(value));
                    return isNaN(d.getTime()) ? null : d;
                })(strPropertyValue);
                element.__dcUpdateDisplay();
                return true;
            case "Text":
                element.__dcDisplay.textContent = strPropertyValue == null ? "" : String(strPropertyValue);
                return true;
            case "Format":
                element.__dcFormat = strPropertyValue || "Short";
                element.__dcUpdateDisplay();
                return true;
            case "CustomFormat":
                element.__dcCustomFormat = strPropertyValue || "yyyy-MM-dd";
                element.__dcUpdateDisplay();
                return true;
            case "MinDate":
                if (element.__dcInput) {
                    var dmin = new Date(String(strPropertyValue));
                    if (!isNaN(dmin.getTime())) {
                        var yyyy = dmin.getFullYear().toString().padStart(4, "0");
                        var MM = (dmin.getMonth() + 1).toString().padStart(2, "0");
                        var dd = dmin.getDate().toString().padStart(2, "0");
                        element.__dcInput.min = `${yyyy}-${MM}-${dd}`;
                    }
                }
                return true;
            case "MaxDate":
                if (element.__dcInput) {
                    var dmax = new Date(String(strPropertyValue));
                    if (!isNaN(dmax.getTime())) {
                        var yyyy2 = dmax.getFullYear().toString().padStart(4, "0");
                        var MM2 = (dmax.getMonth() + 1).toString().padStart(2, "0");
                        var dd2 = dmax.getDate().toString().padStart(2, "0");
                        element.__dcInput.max = `${yyyy2}-${MM2}-${dd2}`;
                    }
                }
                return true;
            case "Enabled":
                if (strPropertyValue === false) {
                    element.setAttribute('aria-disabled', 'true');
                    element.style.opacity = '0.6';
                    element.style.pointerEvents = 'none';
                } else {
                    element.removeAttribute('aria-disabled');
                    element.style.opacity = '';
                    element.style.pointerEvents = '';
                }
                if (element.__dcInput) {
                    if (strPropertyValue === false) element.__dcInput.setAttribute('disabled', 'disabled');
                    else element.__dcInput.removeAttribute('disabled');
                }
                return true;
            default:
                return super.SetPropertyValue.call(this, element, strPropertyName, strPropertyValue);
        }
    }
};

if (window.__DCControlTypes == null) window.__DCControlTypes = new Object();
window.__DCControlTypes["System.Windows.Forms.DateTimePicker"] = new SystemWindowsFormsDateTimePickerFactory();
