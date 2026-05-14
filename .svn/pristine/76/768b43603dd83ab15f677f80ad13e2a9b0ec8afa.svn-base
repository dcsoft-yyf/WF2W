"use strict";
if (window.__DCControlTypes == null) window.__DCControlTypes = {};
if (window.__DCControlTypes["System.Windows.Forms.MenuItem"] == null) {
    window.__DCControlTypes["System.Windows.Forms.MenuItem"] = {
        Create: function (options) {
            options = options || {};
            var li = document.createElement("li");
            li.setAttribute("dchandle", options.Handle);
            li.setAttribute("dctype", "System.Windows.Forms.MenuItem");
            li.style.position = "relative";
            li.style.listStyle = "none";
            li.style.padding = "4px 8px";
            li.style.cursor = "pointer";
            if (options.Win32API && options.Win32API.BindingStandardControlEvent) {
                options.Win32API.BindingStandardControlEvent(li, "click|mousedown|mousemove|mouseup");
            }
            return li;
        },
        ApplyOptions: function (htmlElement, options) {
            if (!htmlElement) return;
            options = options || {};
            htmlElement.setAttribute("dchandle", options.Handle);
            // Text with WinForms mnemonic '&'
            if (options.Text !== undefined) {
                var raw = options.Text == null ? '' : String(options.Text);
                var r = window.__DCRenderMnemonic(raw);
                htmlElement.innerHTML = r.html;
                if (r.access) htmlElement.setAttribute('accesskey', r.access);
                window.__DCEnsureMnemonicUnderlineStyle();
            }
            // Enabled / Visible
            if (options.Enabled !== undefined) {
                if (options.Enabled === false) {
                    htmlElement.style.pointerEvents = 'none';
                    htmlElement.style.opacity = '0.6';
                    htmlElement.setAttribute('aria-disabled', 'true');
                } else {
                    htmlElement.style.pointerEvents = '';
                    htmlElement.style.opacity = '';
                    htmlElement.removeAttribute('aria-disabled');
                }
            }
            if (options.Visible !== undefined) {
                htmlElement.style.display = options.Visible === false ? 'none' : '';
            }
            // Shortcut display (basic)
            if (options.Shortcut !== undefined) {
                htmlElement.setAttribute('data-shortcut', options.Shortcut);
            }
        }
    };
}
