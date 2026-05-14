"use strict";

/** 使用HTML 元素模拟 System.Windows.Forms.ComboBox 控件（Vue 风格，不用 <select>） */
class SystemWindowsFormsComboBoxFactory extends SystemWindowsFormsControlFactory {
    Create(options) {
        options = options || {};
        const rootElement = document.createElement("div");
        rootElement.setAttribute("dchandle", options.Handle);
        rootElement.setAttribute("dctype", "System.Windows.Forms.ComboBox");
        rootElement.style.position = "absolute";
        rootElement.style.boxSizing = "border-box";
        rootElement.style.display = "inline-flex";
        rootElement.style.alignItems = "center";
        rootElement.style.border = "1px solid #ccc";
        rootElement.style.padding = "2px 4px";
        rootElement.style.cursor = "default";
        rootElement.style.background = "#fff";
        rootElement.__items = [];
        rootElement.__selectedIndex = -1;
        rootElement.__dropDownVisible = false;
        rootElement.__dropDownStyle = options.DropDownStyle || "DropDown";
        rootElement.__maxDropDownItems = options.MaxDropDownItems || 10;
        rootElement.__dropDownHeight = options.DropDownHeight || null;
        rootElement.__dropDownWidth = options.DropDownWidth || null;
        rootElement.__sorted = options.Sorted === true;

        const input = document.createElement("input");
        input.type = "text";
        input.className = "dc_combobox_input";
        input.style.border = "none";
        input.style.outline = "none";
        input.style.flex = "1";
        input.style.background = "transparent";
        input.style.width = "100%";
        input.style.height = "100%";
        input.style.cursor = "text";
        rootElement.appendChild(input);
        rootElement.__input = input;

        const arrow = document.createElement("span");
        arrow.textContent = "▼";
        arrow.style.fontSize = "10px";
        arrow.style.marginLeft = "4px";
        arrow.style.pointerEvents = "none";
        rootElement.appendChild(arrow);

        const closeDropdown = (cancel) => {
            if (!rootElement.__dropDownVisible) return;
            rootElement.__dropDownVisible = false;
            if (rootElement.__dropdown && rootElement.__dropdown.parentNode) {
                rootElement.__dropdown.parentNode.removeChild(rootElement.__dropdown);
            }
            if (rootElement.__docHandler) {
                document.removeEventListener("click", rootElement.__docHandler);
                rootElement.__docHandler = null;
            }
            if (rootElement.__keydownHandler) {
                document.removeEventListener("keydown", rootElement.__keydownHandler, true);
                rootElement.__keydownHandler = null;
            }
            rootElement.__dropdown = null;
            if (!cancel) {
                setTimeout(() => {
                    if (typeof window.RaiseSelectedIndexChanged === "function") {
                        window.RaiseSelectedIndexChanged(rootElement);
                    }
                }, 0);
            }
        };

        const renderSelection = (idx) => {
            rootElement.__selectedIndex = idx;
            const item = rootElement.__items[idx];
            if (item == null) {
                input.value = "";
            } else if (typeof item === "object") {
                const display = item.Text ?? item.Value ?? item.toString();
                input.value = display ?? "";
            } else {
                input.value = String(item);
            }
        };

        const buildDropdown = async () => {
            let items = rootElement.__items;
            if (typeof window.GetDropDownItems === "function") {
                try {
                    const remoteItems = await window.GetDropDownItems(rootElement);
                    if (Array.isArray(remoteItems)) {
                        items = remoteItems.slice();
                        if (rootElement.__sorted) {
                            items.sort((a, b) => String(a.Text ?? a.Value ?? a).localeCompare(String(b.Text ?? b.Value ?? b)));
                        }
                        rootElement.__items = items;
                    }
                } catch (e) { }
            }
            const dd = document.createElement("div");
            dd.className = "dc_combobox_dropdown";
            dd.style.position = "fixed";
            dd.style.background = "#fff";
            dd.style.border = "1px solid #ccc";
            dd.style.boxShadow = "0 4px 8px rgba(0,0,0,0.15)";
            dd.style.boxSizing = "border-box";
            dd.style.maxHeight = (rootElement.__dropDownHeight || rootElement.__maxDropDownItems * 28) + "px";
            dd.style.overflowY = "auto";
            dd.style.zIndex = 100000;
            dd.style.minWidth = (rootElement.__dropDownWidth || rootElement.getBoundingClientRect().width) + "px";

            items.forEach((it, idx) => {
                const row = document.createElement("div");
                row.className = "dc_combobox_item";
                row.style.padding = "6px 8px";
                row.style.cursor = "pointer";
                row.style.whiteSpace = "nowrap";
                row.textContent = typeof it === "object" ? (it.Text ?? it.Value ?? it.toString()) : String(it);
                if (idx === rootElement.__selectedIndex) {
                    row.style.background = "#e8e8e8";
                }
                row.addEventListener("mouseenter", () => {
                    dd.querySelectorAll(".dc_combobox_item").forEach(n => n.style.background = "");
                    row.style.background = "#f3f3f3";
                    rootElement.__hoverIndex = idx;
                });
                row.addEventListener("mousedown", (ev) => ev.preventDefault());
                row.addEventListener("click", () => {
                    renderSelection(idx);
                    closeDropdown(false);
                });
                dd.appendChild(row);
            });

            const rect = rootElement.getBoundingClientRect();
            let left = rect.left;
            let top = rect.bottom;
            const ddWidth = dd.offsetWidth;
            const ddHeight = dd.offsetHeight;
            if (top + ddHeight > window.innerHeight && rect.top - ddHeight > 0) {
                top = rect.top - ddHeight;
            }
            if (left + ddWidth > window.innerWidth) {
                left = Math.max(0, window.innerWidth - ddWidth - 2);
            }
            dd.style.left = left + "px";
            dd.style.top = top + "px";

            document.body.appendChild(dd);
            rootElement.__dropdown = dd;
            rootElement.__dropDownVisible = true;

            rootElement.__docHandler = (ev) => {
                if (dd && !dd.contains(ev.target) && !rootElement.contains(ev.target)) {
                    closeDropdown(true);
                }
            };
            document.addEventListener("click", rootElement.__docHandler);

            rootElement.__keydownHandler = (ev) => {
                if (!rootElement.__dropDownVisible) return;
                if (ev.key === "Escape") {
                    ev.preventDefault();
                    closeDropdown(true);
                } else if (ev.key === "ArrowDown") {
                    ev.preventDefault();
                    const next = (rootElement.__hoverIndex == null ? -1 : rootElement.__hoverIndex) + 1;
                    rootElement.__hoverIndex = Math.min(next, items.length - 1);
                    const rows = dd.querySelectorAll(".dc_combobox_item");
                    rows.forEach(n => n.style.background = "");
                    if (rows[rootElement.__hoverIndex]) {
                        rows[rootElement.__hoverIndex].style.background = "#f3f3f3";
                        rows[rootElement.__hoverIndex].scrollIntoView({ block: "nearest" });
                    }
                } else if (ev.key === "ArrowUp") {
                    ev.preventDefault();
                    const next = (rootElement.__hoverIndex == null ? items.length : rootElement.__hoverIndex) - 1;
                    rootElement.__hoverIndex = Math.max(0, next);
                    const rows = dd.querySelectorAll(".dc_combobox_item");
                    rows.forEach(n => n.style.background = "");
                    if (rows[rootElement.__hoverIndex]) {
                        rows[rootElement.__hoverIndex].style.background = "#f3f3f3";
                        rows[rootElement.__hoverIndex].scrollIntoView({ block: "nearest" });
                    }
                } else if (ev.key === "Enter") {
                    ev.preventDefault();
                    const idx = rootElement.__hoverIndex ?? rootElement.__selectedIndex;
                    if (idx != null && idx >= 0 && idx < items.length) {
                        renderSelection(idx);
                    }
                    closeDropdown(false);
                }
            };
            document.addEventListener("keydown", rootElement.__keydownHandler, true);
        };

        const openDropdown = async () => {
            if (rootElement.__dropDownVisible) {
                closeDropdown(true);
                return;
            }
            await buildDropdown();
        };

        rootElement.addEventListener("click", async (ev) => {
            ev.stopPropagation();
            await openDropdown();
        });

        input.addEventListener("focus", () => rootElement.classList.add("dc_combobox_focus"));
        input.addEventListener("blur", () => rootElement.classList.remove("dc_combobox_focus"));
        input.addEventListener("keydown", async (ev) => {
            if (ev.key === "ArrowDown" || ev.key === "ArrowUp" || ev.key === "Enter" || ev.key === "Escape") {
                ev.preventDefault();
                await openDropdown();
            }
        });

        if (options.Text != null) {
            input.value = options.Text;
        }
        return rootElement;
    }

    GetSelectedIndex(element) {
        if (!element) return -1;
        var idx = element.__selectedIndex;
        return (typeof idx === "number") ? idx : -1;
    }
    SetSelectedIndex(element, intIndex) {
        if (!element) return;
        var idx = (typeof intIndex === "number") ? intIndex : parseInt(intIndex, 10);
        if (isNaN(idx)) idx = -1;
        if (Array.isArray(element.__items)) {
            if (idx < -1) idx = -1;
            if (idx >= element.__items.length) idx = element.__items.length - 1;
            element.__selectedIndex = idx;
            if (element.__input) {
                if (idx === -1) {
                    element.__input.value = "";
                } else {
                    const item = element.__items[idx];
                    element.__input.value = item == null ? "" : (typeof item === "object" ? (item.Text ?? item.Value ?? item.toString()) : String(item));
                }
            }
        } else {
            element.__selectedIndex = idx;
        }
    }

    SetPropertyValue(element, name, value) {
        if (!element) return false;
        switch (name) {
            case "Text":
                if (element.__input) element.__input.value = value == null ? "" : String(value);
                return true;
            case "SelectedIndex":
                if (typeof value === "number") {
                    element.__selectedIndex = value;
                    if (Array.isArray(element.__items)) {
                        const idx = Math.max(0, Math.min(value, element.__items.length - 1));
                        const item = element.__items[idx];
                        element.__selectedIndex = idx;
                        if (element.__input) {
                            element.__input.value = item == null ? "" : (typeof item === "object" ? (item.Text ?? item.Value ?? item.toString()) : String(item));
                        }
                    }
                }
                return true;
            case "DropDownStyle":
                element.__dropDownStyle = value;
                if (element.__input) {
                    element.__input.readOnly = (String(value).toLowerCase() === "dropdownlist");
                }
                return true;
            case "DropDownHeight":
                element.__dropDownHeight = value;
                return true;
            case "DropDownWidth":
                element.__dropDownWidth = value;
                return true;
            case "MaxDropDownItems":
                element.__maxDropDownItems = value;
                return true;
            case "IntegralHeight":
                return true;
            case "Sorted":
                element.__sorted = value === true;
                return true;
            case "Items":
                if (Array.isArray(value)) {
                    element.__items = value.slice();
                    if (element.__sorted) {
                        element.__items.sort((a, b) => String(a.Text ?? a.Value ?? a).localeCompare(String(b.Text ?? b.Value ?? b)));
                    }
                }
                return true;
            case "Enabled":
                if (value === false) {
                    element.style.pointerEvents = "none";
                    element.style.opacity = "0.6";
                    element.setAttribute("aria-disabled", "true");
                } else {
                    element.style.pointerEvents = "";
                    element.style.opacity = "";
                    element.removeAttribute("aria-disabled");
                }
                return true;
            case "Visible":
                element.style.display = (value === false) ? "none" : "inline-flex";
                return true;
            default:
                return super.SetPropertyValue(element, name, value);
        }
    }
}

if (window.__DCControlTypes == null) window.__DCControlTypes = new Object();
window.__DCControlTypes["System.Windows.Forms.ComboBox"] = new SystemWindowsFormsComboBoxFactory();
