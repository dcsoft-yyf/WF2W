"use strict";
if (window.__DCControlTypes == null) window.__DCControlTypes = {};
if (window.__DCControlTypes["System.Windows.Forms.Menu"] == null) {
    window.__DCControlTypes["System.Windows.Forms.Menu"] = {
        Create: function (options) {
            options = options || {};
            var el = document.createElement("div");
            el.setAttribute("dchandle", options.Handle);
            el.setAttribute("dctype", "System.Windows.Forms.Menu");
            el.style.position = "absolute";
            el.style.display = "block";
            el.style.boxSizing = "border-box";
            el.__GetChildContainer = function () { return el; };
            if (options.Win32API && options.Win32API.BindingStandardControlEvent) {
                options.Win32API.BindingStandardControlEvent(el, "click|mousedown|mousemove|mouseup");
            }
            return el;
        },
        ApplyOptions: function (htmlElement, options) {
            if (!htmlElement) return;
            options = options || {};
            htmlElement.setAttribute("dchandle", options.Handle);
            if (options.Left != null) htmlElement.style.left = options.Left + "px";
            if (options.Top != null) htmlElement.style.top = options.Top + "px";
            if (options.Width != null) htmlElement.style.width = options.Width + "px";
            if (options.Height != null) htmlElement.style.height = options.Height + "px";
            if (options.Visible !== undefined) htmlElement.style.display = (options.Visible ? "" : "none");
            if (options.Enabled === false) { htmlElement.style.pointerEvents = "none"; htmlElement.style.opacity = "0.6"; htmlElement.setAttribute("aria-disabled", "true"); }
            else { htmlElement.style.pointerEvents = ""; htmlElement.style.opacity = ""; htmlElement.removeAttribute("aria-disabled"); }
            if (options.BackColor !== undefined) htmlElement.style.backgroundColor = options.BackColor;
            if (options.ForeColor !== undefined || options.Color !== undefined) htmlElement.style.color = options.ForeColor || options.Color;
            if (options.Font !== undefined) window.__DCWin32API.SetControlFont(htmlElement, options.Font);

            // Render contained menu items (hierarchical) from MenuItems
            if (Array.isArray(options.MenuItems)) {
                // Ensure a container exists for first level
                var container = htmlElement.querySelector('div[id="dc_menu_root"]');
                if (!container) {
                    container = document.createElement('div');
                    container.id = 'dc_menu_root';
                    container.style.display = 'block';
                    container.style.padding = '4px 0';
                    container.style.position = 'relative';
                    htmlElement.appendChild(container);
                }

                // Clear existing items
                while (container.firstChild) container.removeChild(container.firstChild);

                window.__DCEnsureMnemonicUnderlineStyle();
                for (var i = 0; i < options.MenuItems.length; i++) {
                    var item = options.MenuItems[i] || {};
                    var li = document.createElement('li');
                    li.style.listStyle = 'none';
                    li.style.padding = '4px 12px';
                    li.style.cursor = 'pointer';
                    if (item.Visible === false) li.style.display = 'none';
                    if (item.Enabled === false) { li.style.pointerEvents = 'none'; li.style.opacity = '0.6'; li.setAttribute('aria-disabled', 'true'); }
                    var text = item.Text == null ? '' : String(item.Text);
                    var r = window.__DCRenderMnemonic(text);
                    li.innerHTML = r.html;
                    if (r.access) li.setAttribute('accesskey', r.access);
                    // optional shortcut display on right
                    if (item.Shortcut) {
                        var span = document.createElement('span');
                        span.textContent = ' ' + item.Shortcut;
                        span.style.float = 'right';
                        span.style.opacity = '0.8';
                        li.appendChild(span);
                    }
                    container.appendChild(li);
                }
            }
        }
    };
}
