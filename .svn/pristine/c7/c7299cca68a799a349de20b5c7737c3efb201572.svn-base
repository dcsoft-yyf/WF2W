"use strict";

class SystemWindowsFormsMainMenuFactory {
    Create(options) {
        options = options || {};

        var el = document.createElement("nav");
        el.setAttribute("dchandle", options.Handle);
        el.setAttribute("dctype", "System.Windows.Forms.MainMenu");
        el.style.position = "absolute";
        el.style.display = "block";
        el.style.boxSizing = "border-box";
        el.style.userSelect = "none";
        el.style.overflow = "visible";

        var ul = document.createElement("ul");
        ul.id = "dc_menu_root_ul";
        ul.style.listStyle = "none";
        ul.style.margin = "0";
        ul.style.padding = "0";
        ul.style.display = "flex";
        ul.style.alignItems = "stretch";
        ul.style.overflow = "visible";

        el.appendChild(ul);
        el.__GetChildContainer = function () { return ul; };
        return el;
    }

    ApplyOptions(htmlElement, options) {
        var target = (this && this.nodeType === 1) ? this : htmlElement;
        if (!target) return;

        options = options || {};
        target.setAttribute("dchandle", options.Handle);

        if (options.Left != null) target.style.left = options.Left + "px";
        if (options.Top != null) target.style.top = options.Top + "px";
        if (options.Width != null) target.style.width = options.Width + "px";
        if (options.Height != null) target.style.height = options.Height + "px";
        if (options.Visible !== undefined) target.style.display = options.Visible ? "" : "none";

        if (options.Enabled === false) {
            target.style.pointerEvents = "none";
            target.style.opacity = "0.6";
            target.setAttribute("aria-disabled", "true");
        }
        else {
            target.style.pointerEvents = "";
            target.style.opacity = "";
            target.removeAttribute("aria-disabled");
        }

        if (options.BackColor !== undefined) target.style.backgroundColor = options.BackColor;
        if (options.ForeColor !== undefined || options.Color !== undefined) {
            target.style.color = options.ForeColor || options.Color;
        }
        if (options.Font !== undefined && window.__DCWin32API && window.__DCWin32API.SetControlFont) {
            window.__DCWin32API.SetControlFont(target, options.Font);
        }

        if (options.RightToLeft !== undefined && options.RightToLeft !== null) {
            var rtl = String(options.RightToLeft);
            var isRtl = (rtl === "Yes" || rtl === "True" || rtl === "true");

            target.style.direction = isRtl ? "rtl" : "ltr";

            var ulRoot = target.querySelector("#dc_menu_root_ul");
            if (ulRoot) {
                ulRoot.style.flexDirection = isRtl ? "row-reverse" : "row";
                ulRoot.style.textAlign = isRtl ? "right" : "left";
            }
        }

        var topItems = options.MenuItems;
        var ul = target.querySelector("#dc_menu_root_ul") || (target.__GetChildContainer && target.__GetChildContainer());
        if (!ul || !Array.isArray(topItems)) {
            return;
        }

        while (ul.firstChild) {
            ul.removeChild(ul.firstChild);
        }

        if (window.__DCEnsureMnemonicUnderlineStyle) {
            window.__DCEnsureMnemonicUnderlineStyle();
        }

        if (!document.getElementById("dc_mainmenu_root_styles")) {
            var rootStyle = document.createElement("style");
            rootStyle.id = "dc_mainmenu_root_styles";
            rootStyle.textContent = [
                'nav[dctype="System.Windows.Forms.MainMenu"] ul#dc_menu_root_ul > li.dc-menu-root-highlight {',
                "    background: #e8e8e8;",
                "    border-radius: 4px;",
                "}"
            ].join("\n");
            document.head.appendChild(rootStyle);
        }

        function hasChildMenuItems(menuItemData) {
            if (!menuItemData) return false;
            if (Array.isArray(menuItemData.MenuItems) && menuItemData.MenuItems.length > 0) return true;
            return menuItemData.HasChildren === true;
        }

        function getItemHandle(menuItemData) {
            if (!menuItemData) return null;
            return menuItemData.dchandle || menuItemData.DCHandle || menuItemData.Handle || null;
        }

        function setRootHighlight(anchor) {
            if (!anchor) return;
            var ulRoot = anchor.parentElement;
            if (!ulRoot) return;

            ulRoot.querySelectorAll(".dc-menu-root-highlight").forEach(function (li) {
                li.classList.remove("dc-menu-root-highlight");
            });
            anchor.classList.add("dc-menu-root-highlight");
        }

        function clearRootHighlight(container) {
            var ulRoot = container && container.querySelector("#dc_menu_root_ul");
            if (!ulRoot) return;

            ulRoot.querySelectorAll(".dc-menu-root-highlight").forEach(function (li) {
                li.classList.remove("dc-menu-root-highlight");
            });
        }

        function triggerMenuItemClick(menuItemData) {
            var handle = getItemHandle(menuItemData);
            if (handle == null) return;
            if (typeof (__DCExecuteControlCommandAsync) !== "function") return;

            window.setTimeout(function () {
                __DCExecuteControlCommandAsync(handle, "OnClick");
            }, 5);
        }

        if (!target.__currentOpenMenu) {
            target.__currentOpenMenu = null;
        }

        for (var i = 0; i < topItems.length; i++) {
            var item = topItems[i] || {};
            var li = document.createElement("li");
            li.style.padding = "4px 8px";
            li.style.cursor = item.Enabled === false ? "default" : "pointer";
            li.style.position = "relative";
            li.style.listStyle = "none";
            li.style.whiteSpace = "nowrap";

            var rendered = window.__DCRenderMnemonic ? window.__DCRenderMnemonic(item.Text || "") : { html: item.Text || "" };
            li.innerHTML = rendered.html || "";
            if (rendered.access) {
                li.setAttribute("accesskey", rendered.access);
            }

            if (item.Enabled === false) {
                li.style.opacity = "0.6";
                li.setAttribute("aria-disabled", "true");
            }

            li.__menuItemData = item;
            ul.appendChild(li);

            (function (menuItemData, anchor, menuTarget) {
                var hoverTimer = null;

                anchor.addEventListener("click", async function (ev) {
                    ev.stopPropagation();

                    if (menuItemData.Enabled === false) {
                        return;
                    }

                    if (!hasChildMenuItems(menuItemData)) {
                        triggerMenuItemClick(menuItemData);
                        if (menuTarget.__currentOpenMenu && menuTarget.__currentOpenMenu.menuController) {
                            menuTarget.__currentOpenMenu.menuController.close();
                        }
                        clearRootHighlight(menuTarget);
                        menuTarget.__currentOpenMenu = null;
                        return;
                    }

                    if (menuTarget.__currentOpenMenu && menuTarget.__currentOpenMenu.anchor === anchor) {
                        if (menuTarget.__currentOpenMenu.menuController) {
                            menuTarget.__currentOpenMenu.menuController.close();
                        }
                        clearRootHighlight(menuTarget);
                        menuTarget.__currentOpenMenu = null;
                        return;
                    }

                    if (menuTarget.__currentOpenMenu && menuTarget.__currentOpenMenu.menuController) {
                        menuTarget.__currentOpenMenu.menuController.close();
                        menuTarget.__currentOpenMenu = null;
                    }

                    var menuController = await __DCShowMenu(anchor, null, null, menuItemData.MenuItems || getItemHandle(menuItemData));
                    if (menuController != null) {
                        menuTarget.__currentOpenMenu = {
                            anchor: anchor,
                            menuController: menuController
                        };
                        setRootHighlight(anchor);
                    }
                });

                anchor.addEventListener("mouseenter", function () {
                    if (menuItemData.Enabled === false) {
                        return;
                    }

                    anchor.style.backgroundColor = "#f0f0f0";

                    if (hoverTimer) {
                        clearTimeout(hoverTimer);
                    }

                    if (!menuTarget.__currentOpenMenu) {
                        setRootHighlight(anchor);
                        return;
                    }

                    if (!hasChildMenuItems(menuItemData)) {
                        setRootHighlight(anchor);
                        return;
                    }

                    hoverTimer = setTimeout(async function () {
                        if (menuTarget.__currentOpenMenu && menuTarget.__currentOpenMenu.anchor === anchor) {
                            return;
                        }

                        if (menuTarget.__currentOpenMenu && menuTarget.__currentOpenMenu.menuController) {
                            menuTarget.__currentOpenMenu.menuController.close();
                            menuTarget.__currentOpenMenu = null;
                        }

                        var menuController = await __DCShowMenu(anchor, null, null, menuItemData.MenuItems || getItemHandle(menuItemData));
                        if (menuController != null) {
                            menuTarget.__currentOpenMenu = {
                                anchor: anchor,
                                menuController: menuController
                            };
                            setRootHighlight(anchor);
                        }
                    }, 200);
                });

                anchor.addEventListener("mouseleave", function () {
                    anchor.style.backgroundColor = "";
                    if (hoverTimer) {
                        clearTimeout(hoverTimer);
                        hoverTimer = null;
                    }
                });
            })(item, li, target);
        }

        var clickOutsideHandler = function (e) {
            if (target.contains(e.target)) {
                return;
            }

            var clickedInPopupMenu = false;
            var allMenuContainers = document.querySelectorAll(".dc-menu-container");
            for (var i = 0; i < allMenuContainers.length; i++) {
                if (allMenuContainers[i].contains(e.target)) {
                    clickedInPopupMenu = true;
                    break;
                }
            }

            if (!clickedInPopupMenu && target.__currentOpenMenu) {
                clearRootHighlight(target);
                target.__currentOpenMenu = null;
            }
        };

        if (target.__clickOutsideHandler) {
            document.removeEventListener("click", target.__clickOutsideHandler);
        }

        target.__clickOutsideHandler = clickOutsideHandler;
        document.addEventListener("click", clickOutsideHandler);
    }

    SetWindowText(htmlElement, text) {
        var target = htmlElement;
        if (!target) return false;

        target.__windowText = text == null ? "" : String(text);

        var ul = target.querySelector("#dc_menu_root_ul") || (target.__GetChildContainer && target.__GetChildContainer());
        if (!ul) return true;

        if (!ul.__captionElement) {
            var caption = document.createElement("span");
            caption.style.display = "none";
            caption.className = "dc-mainmenu-caption";
            ul.__captionElement = caption;
            target.appendChild(caption);
        }

        ul.__captionElement.textContent = target.__windowText;
        return true;
    }

    GetWindowText(htmlElement) {
        var target = htmlElement;
        if (!target) return "";

        if (typeof target.__windowText === "string") {
            return target.__windowText;
        }

        var ul = target.querySelector("#dc_menu_root_ul") || (target.__GetChildContainer && target.__GetChildContainer());
        if (!ul) return "";

        var texts = [];
        var items = ul.children;
        for (var i = 0; i < items.length; i++) {
            var text = items[i].innerText || items[i].textContent || "";
            text = String(text).trim();
            if (text.length > 0) {
                texts.push(text);
            }
        }

        return texts.join(" ");
    }

    GetPreferredSize(htmlElement, args) {
        var target = htmlElement;
        if (!target) {
            return { Width: 0, Height: 0 };
        }

        var ul = target.querySelector("#dc_menu_root_ul") || (target.__GetChildContainer && target.__GetChildContainer());
        if (!ul) {
            return {
                Width: target.offsetWidth || 0,
                Height: target.offsetHeight || 0
            };
        }

        return {
            Width: Math.max(target.offsetWidth || 0, ul.scrollWidth || ul.offsetWidth || 0),
            Height: Math.max(target.offsetHeight || 0, ul.scrollHeight || ul.offsetHeight || 0)
        };
    }
}

async function __DCShowMenu(targetElement, intOffsetX, intOffsetY, arrMenuItems) {
    if (typeof (arrMenuItems) === "number") {
        if (typeof (__DCExecuteControlCommandAsync) === "function") {
            arrMenuItems = await __DCExecuteControlCommandAsync(
                arrMenuItems,
                "GetDropdownItems",
                null
            );
        }
    }

    if (!Array.isArray(arrMenuItems)) {
        return null;
    }

    async function GetChildMenuItems(menuItemData) {
        if (menuItemData == null) {
            return null;
        }

        if (Array.isArray(menuItemData.MenuItems)) {
            return menuItemData.MenuItems;
        }

        var handle = menuItemData.dchandle || menuItemData.DCHandle || menuItemData.Handle;
        if (handle != null && typeof (__DCExecuteControlCommandAsync) === "function") {
            return await __DCExecuteControlCommandAsync(handle, "GetDropdownItems");
        }

        return null;
    }

    function OnMenuItemClick(intHandle) {
        if (intHandle == null) {
            return;
        }

        if (typeof (__DCExecuteControlCommandAsync) === "function") {
            window.setTimeout(function () {
                __DCExecuteControlCommandAsync(intHandle, "OnClick");
            }, 5);
        }
    }

    function HasChildMenuItems(menuItemData) {
        if (menuItemData && Array.isArray(menuItemData.MenuItems) && menuItemData.MenuItems.length > 0) {
            return true;
        }
        return menuItemData && menuItemData.HasChildren === true;
    }

    function GetItemHandle(menuItemData) {
        if (!menuItemData) return null;
        return menuItemData.dchandle || menuItemData.DCHandle || menuItemData.Handle || null;
    }

    var menuState = {
        allMenus: [],
        hoverTimer: null,
        clickOutsideHandler: null,
        currentHoverItem: null,
        keydownHandler: null
    };

    function injectStyles() {
        if (document.getElementById("dc-menu-styles")) return;

        var style = document.createElement("style");
        style.id = "dc-menu-styles";
        style.textContent = `
            .dc-menu-container {
                position: fixed;
                background: #ffffff;
                border: 1px solid #e0e0e0;
                border-radius: 8px;
                box-shadow: 0 4px 12px rgba(0, 0, 0, 0.15);
                padding: 4px 0;
                min-width: 180px;
                z-index: 100000;
                font-family: "Segoe UI", Tahoma, Geneva, Verdana, sans-serif;
                font-size: 14px;
                user-select: none;
            }
            .dc-menu-item {
                display: flex;
                align-items: center;
                padding: 6px 32px 6px 32px;
                cursor: pointer;
                position: relative;
                white-space: nowrap;
                color: #202020;
                transition: background-color 0.1s ease;
            }
            .dc-menu-item:hover:not(.dc-menu-separator):not(.dc-menu-disabled) {
                background-color: #f3f3f3;
            }
            .dc-menu-item.dc-menu-highlight:not(.dc-menu-separator):not(.dc-menu-disabled) {
                background-color: #e8e8e8;
            }
            .dc-menu-item.dc-menu-disabled {
                color: #a0a0a0;
                cursor: default;
            }
            .dc-menu-separator {
                height: 1px;
                background-color: #e0e0e0;
                margin: 4px 8px;
                cursor: default;
                padding: 0;
            }
            .dc-menu-text {
                flex: 1;
                overflow: hidden;
                text-overflow: ellipsis;
            }
            .dc-menu-shortcut {
                margin-left: 24px;
                color: #707070;
                font-size: 12px;
            }
            .dc-menu-arrow {
                position: absolute;
                right: 8px;
                width: 0;
                height: 0;
                border-left: 4px solid #505050;
                border-top: 4px solid transparent;
                border-bottom: 4px solid transparent;
            }
            .dc-menu-checkmark {
                position: absolute;
                left: 8px;
                width: 16px;
                height: 16px;
                display: flex;
                align-items: center;
                justify-content: center;
            }
            .dc-menu-checkmark::before {
                content: '✓';
                font-size: 14px;
                font-weight: bold;
                color: #202020;
            }
            .dc-menu-icon {
                position: absolute;
                left: 8px;
                width: 16px;
                height: 16px;
                display: inline-block;
                background-size: contain;
                background-repeat: no-repeat;
                background-position: center;
            }
            .dc-menu-underline {
                text-decoration: underline;
            }
        `;

        document.head.appendChild(style);
    }

    function removeMenuElement(menu) {
        if (!menu) return;

        if (menu.__parentMenuItem) {
            menu.__parentMenuItem.__childMenuElement = null;
        }

        if (menu.parentNode) {
            menu.parentNode.removeChild(menu);
        }
    }

    function closeAllMenus() {
        if (menuState.hoverTimer) {
            clearTimeout(menuState.hoverTimer);
            menuState.hoverTimer = null;
        }

        for (var i = menuState.allMenus.length - 1; i >= 0; i--) {
            removeMenuElement(menuState.allMenus[i]);
        }

        menuState.allMenus = [];
        menuState.currentHoverItem = null;

        if (menuState.clickOutsideHandler) {
            document.removeEventListener("click", menuState.clickOutsideHandler);
            menuState.clickOutsideHandler = null;
        }

        if (menuState.keydownHandler) {
            document.removeEventListener("keydown", menuState.keydownHandler);
            menuState.keydownHandler = null;
        }

        document.querySelectorAll('nav[dctype="System.Windows.Forms.MainMenu"] ul#dc_menu_root_ul > li.dc-menu-root-highlight').forEach(function (li) {
            li.classList.remove("dc-menu-root-highlight");
        });
    }

    function createMenu(items) {
        var container = document.createElement("div");
        container.className = "dc-menu-container";

        if (!items || items.length === 0) {
            return container;
        }

        items.forEach(function (itemData) {
            container.appendChild(createMenuItem(itemData, container));
        });

        return container;
    }

    function createMenuItem(itemData, menuContainer) {
        var item = document.createElement("div");

        if (itemData.Text === "-" || itemData.IsSeparator) {
            item.className = "dc-menu-item dc-menu-separator";
            return item;
        }

        item.className = "dc-menu-item";
        item.__menuData = itemData;
        item.__childMenuElement = null;

        if (itemData.Enabled === false) {
            item.classList.add("dc-menu-disabled");
        }

        if (itemData.Image) {
            var icon = document.createElement("span");
            icon.className = "dc-menu-icon";
            icon.style.backgroundImage = 'url("' + String(itemData.Image) + '")';
            item.appendChild(icon);
        }

        if (itemData.Checked) {
            var checkmark = document.createElement("span");
            checkmark.className = "dc-menu-checkmark";
            item.appendChild(checkmark);
        }

        var textSpan = document.createElement("span");
        textSpan.className = "dc-menu-text";
        var mnemonic = window.__DCRenderMnemonic ? window.__DCRenderMnemonic(itemData.Text || "") : { html: itemData.Text || "" };
        textSpan.innerHTML = mnemonic.html || "";
        item.appendChild(textSpan);

        if (itemData.Shortcut || itemData.ShortcutKeyDisplayString) {
            var shortcut = document.createElement("span");
            shortcut.className = "dc-menu-shortcut";
            shortcut.textContent = itemData.ShortcutKeyDisplayString || itemData.Shortcut || "";
            item.appendChild(shortcut);
        }

        if (HasChildMenuItems(itemData)) {
            var arrow = document.createElement("span");
            arrow.className = "dc-menu-arrow";
            item.appendChild(arrow);
        }

        setupMenuItemEvents(item, menuContainer);
        return item;
    }

    function closeChildMenus(menuContainer) {
        var parentIndex = menuState.allMenus.indexOf(menuContainer);
        if (parentIndex < 0) return;

        for (var i = menuState.allMenus.length - 1; i > parentIndex; i--) {
            removeMenuElement(menuState.allMenus[i]);
            menuState.allMenus.splice(i, 1);
        }
    }

    async function showSubMenu(parentItem, parentMenu) {
        var itemData = parentItem.__menuData;
        var childItems = await GetChildMenuItems(itemData);

        if (!Array.isArray(childItems) || childItems.length === 0) {
            return null;
        }

        closeChildMenus(parentMenu);

        if (parentItem.__childMenuElement && parentItem.__childMenuElement.parentNode) {
            return parentItem.__childMenuElement;
        }

        var subMenu = createMenu(childItems);
        subMenu.style.position = "fixed";
        subMenu.style.display = "block";
        subMenu.__parentMenuItem = parentItem;

        document.body.appendChild(subMenu);
        parentItem.__childMenuElement = subMenu;
        menuState.allMenus.push(subMenu);

        var rect = parentItem.getBoundingClientRect();
        var menuWidth = subMenu.offsetWidth;
        var menuHeight = subMenu.offsetHeight;
        var viewportWidth = window.innerWidth;
        var viewportHeight = window.innerHeight;

        var left = rect.right + 2;
        var top = rect.top;

        if (left + menuWidth > viewportWidth) {
            left = rect.left - menuWidth - 2;
        }
        if (left < 0) {
            left = 2;
        }
        if (top + menuHeight > viewportHeight) {
            top = viewportHeight - menuHeight - 2;
        }
        if (top < 0) {
            top = 2;
        }

        subMenu.style.left = left + "px";
        subMenu.style.top = top + "px";
        return subMenu;
    }

    function setupMenuItemEvents(item, menuContainer) {
        var itemData = item.__menuData;

        item.addEventListener("mouseenter", function (e) {
            e.stopPropagation();

            if (menuState.hoverTimer) {
                clearTimeout(menuState.hoverTimer);
                menuState.hoverTimer = null;
            }

            var siblings = menuContainer.querySelectorAll(".dc-menu-item");
            siblings.forEach(function (sibling) {
                sibling.classList.remove("dc-menu-highlight");
            });

            if (!item.classList.contains("dc-menu-separator")) {
                item.classList.add("dc-menu-highlight");
                if (!item.classList.contains("dc-menu-disabled")) {
                    menuState.currentHoverItem = item;
                }
            }

            closeChildMenus(menuContainer);

            if (HasChildMenuItems(itemData) && !item.classList.contains("dc-menu-disabled")) {
                menuState.hoverTimer = setTimeout(function () {
                    showSubMenu(item, menuContainer);
                }, 300);
            }
        });

        item.addEventListener("click", async function (e) {
            e.preventDefault();
            e.stopPropagation();

            if (item.classList.contains("dc-menu-separator")) {
                return;
            }

            if (item.classList.contains("dc-menu-disabled")) {
                return;
            }

            if (HasChildMenuItems(itemData)) {
                var subMenu = await showSubMenu(item, menuContainer);
                if (subMenu) {
                    var firstValidItem = subMenu.querySelector(".dc-menu-item:not(.dc-menu-separator):not(.dc-menu-disabled)");
                    if (firstValidItem) {
                        firstValidItem.classList.add("dc-menu-highlight");
                        menuState.currentHoverItem = firstValidItem;
                    }
                }
            } else {
                var handle = GetItemHandle(itemData);
                if (handle != null) {
                    OnMenuItemClick(handle);
                }
                closeAllMenus();
            }
        });
    }

    function calculateMenuPosition(anchor, offsetX, offsetY, menu) {
        var rect = anchor.getBoundingClientRect();
        var menuWidth = menu.offsetWidth;
        var menuHeight = menu.offsetHeight;
        var viewportWidth = window.innerWidth;
        var viewportHeight = window.innerHeight;

        var left;
        var top;

        if (typeof offsetX !== "number" || typeof offsetY !== "number") {
            left = rect.left;
            top = rect.bottom + 2;

            if (top + menuHeight > viewportHeight && rect.top - menuHeight > 0) {
                top = rect.top - menuHeight - 2;
            }
        } else {
            left = rect.left + offsetX;
            top = rect.top + offsetY;
        }

        if (left + menuWidth > viewportWidth) {
            left = viewportWidth - menuWidth - 2;
        }
        if (top + menuHeight > viewportHeight) {
            top = viewportHeight - menuHeight - 2;
        }
        if (left < 0) {
            left = 2;
        }
        if (top < 0) {
            top = 2;
        }

        return { left: left, top: top };
    }

    injectStyles();

    if (!targetElement || !Array.isArray(arrMenuItems) || arrMenuItems.length === 0) {
        return null;
    }

    closeAllMenus();

    var mainMenu = createMenu(arrMenuItems);
    mainMenu.style.position = "fixed";
    mainMenu.style.display = "block";

    document.body.appendChild(mainMenu);
    menuState.allMenus.push(mainMenu);

    var position = calculateMenuPosition(targetElement, intOffsetX, intOffsetY, mainMenu);
    mainMenu.style.left = position.left + "px";
    mainMenu.style.top = position.top + "px";

    menuState.clickOutsideHandler = function (e) {
        var clickedInsideMenu = false;

        menuState.allMenus.forEach(function (menu) {
            if (menu.contains(e.target)) {
                clickedInsideMenu = true;
            }
        });

        if (!clickedInsideMenu) {
            closeAllMenus();
        }
    };

    setTimeout(function () {
        document.addEventListener("click", menuState.clickOutsideHandler);
    }, 100);

    function setRootHighlight(anchor) {
        if (!anchor) return;
        var ulRoot = anchor.parentElement;
        if (!ulRoot) return;

        ulRoot.querySelectorAll(".dc-menu-root-highlight").forEach(function (li) {
            li.classList.remove("dc-menu-root-highlight");
        });
        anchor.classList.add("dc-menu-root-highlight");
    }

    function navigateDown() {
        var currentMenu = menuState.allMenus[menuState.allMenus.length - 1];
        if (!currentMenu) return;

        var items = Array.from(currentMenu.querySelectorAll(".dc-menu-item"));
        var validItems = items.filter(function (it) {
            return !it.classList.contains("dc-menu-separator") && !it.classList.contains("dc-menu-disabled");
        });

        if (validItems.length === 0) return;

        var currentIndex = -1;
        if (menuState.currentHoverItem) {
            currentIndex = validItems.indexOf(menuState.currentHoverItem);
        }

        var nextIndex = (currentIndex + 1) % validItems.length;
        var nextItem = validItems[nextIndex];

        items.forEach(function (it) { it.classList.remove("dc-menu-highlight"); });
        nextItem.classList.add("dc-menu-highlight");
        menuState.currentHoverItem = nextItem;
        nextItem.scrollIntoView({ block: "nearest", behavior: "smooth" });
    }

    function navigateUp() {
        var currentMenu = menuState.allMenus[menuState.allMenus.length - 1];
        if (!currentMenu) return;

        var items = Array.from(currentMenu.querySelectorAll(".dc-menu-item"));
        var validItems = items.filter(function (it) {
            return !it.classList.contains("dc-menu-separator") && !it.classList.contains("dc-menu-disabled");
        });

        if (validItems.length === 0) return;

        var currentIndex = -1;
        if (menuState.currentHoverItem) {
            currentIndex = validItems.indexOf(menuState.currentHoverItem);
        }

        var prevIndex = currentIndex <= 0 ? validItems.length - 1 : currentIndex - 1;
        var prevItem = validItems[prevIndex];

        items.forEach(function (it) { it.classList.remove("dc-menu-highlight"); });
        prevItem.classList.add("dc-menu-highlight");
        menuState.currentHoverItem = prevItem;
        prevItem.scrollIntoView({ block: "nearest", behavior: "smooth" });
    }

    async function navigateRight() {
        if (menuState.currentHoverItem) {
            var itemData = menuState.currentHoverItem.__menuData;
            if (itemData && HasChildMenuItems(itemData)) {
                var currentMenu = menuState.currentHoverItem.closest(".dc-menu-container");
                var subMenu = await showSubMenu(menuState.currentHoverItem, currentMenu);
                if (subMenu) {
                    var firstValidItem = subMenu.querySelector(".dc-menu-item:not(.dc-menu-separator):not(.dc-menu-disabled)");
                    if (firstValidItem) {
                        firstValidItem.classList.add("dc-menu-highlight");
                        menuState.currentHoverItem = firstValidItem;
                    }
                }
                return;
            }
        }

        if (menuState.allMenus.length === 1) {
            await switchToNextMainMenuItem();
        }
    }

    async function navigateLeft() {
        if (menuState.allMenus.length > 1) {
            var lastMenu = menuState.allMenus[menuState.allMenus.length - 1];
            menuState.allMenus.pop();
            removeMenuElement(lastMenu);

            if (menuState.allMenus.length > 0) {
                var parentMenu = menuState.allMenus[menuState.allMenus.length - 1];
                var highlightedItem = parentMenu.querySelector(".dc-menu-item.dc-menu-highlight");
                if (highlightedItem) {
                    menuState.currentHoverItem = highlightedItem;
                }
            }
        }
        else if (menuState.allMenus.length === 1) {
            await switchToPreviousMainMenuItem();
        }
    }

    async function activateCurrentItem() {
        if (!menuState.currentHoverItem) {
            var currentMenu = menuState.allMenus[menuState.allMenus.length - 1];
            if (currentMenu) {
                var firstValidItem = currentMenu.querySelector(".dc-menu-item:not(.dc-menu-separator):not(.dc-menu-disabled)");
                if (firstValidItem) {
                    firstValidItem.classList.add("dc-menu-highlight");
                    menuState.currentHoverItem = firstValidItem;
                }
            }
            return;
        }

        var itemData = menuState.currentHoverItem.__menuData;
        if (!itemData) return;

        if (menuState.currentHoverItem.classList.contains("dc-menu-disabled") ||
            menuState.currentHoverItem.classList.contains("dc-menu-separator")) {
            return;
        }

        if (HasChildMenuItems(itemData)) {
            var currentMenu = menuState.currentHoverItem.closest(".dc-menu-container");
            var subMenu = await showSubMenu(menuState.currentHoverItem, currentMenu);
            if (subMenu) {
                var firstValidItem = subMenu.querySelector(".dc-menu-item:not(.dc-menu-separator):not(.dc-menu-disabled)");
                if (firstValidItem) {
                    firstValidItem.classList.add("dc-menu-highlight");
                    menuState.currentHoverItem = firstValidItem;
                }
            }
        } else {
            var handle = GetItemHandle(itemData);
            if (handle != null) {
                OnMenuItemClick(handle);
            }
            closeAllMenus();
        }
    }

    async function switchToNextMainMenuItem() {
        var mainMenuBar = document.querySelector('nav[dctype="System.Windows.Forms.MainMenu"]');
        if (!mainMenuBar || !mainMenuBar.__currentOpenMenu) return;

        var currentAnchor = mainMenuBar.__currentOpenMenu.anchor;
        var allMenuItems = Array.from(mainMenuBar.querySelectorAll("#dc_menu_root_ul > li"));
        var currentIndex = allMenuItems.indexOf(currentAnchor);
        if (currentIndex === -1) return;

        var nextIndex = (currentIndex + 1) % allMenuItems.length;
        var nextAnchor = allMenuItems[nextIndex];

        if (mainMenuBar.__currentOpenMenu.menuController) {
            mainMenuBar.__currentOpenMenu.menuController.close();
        }
        mainMenuBar.__currentOpenMenu = null;

        var menuItemData = nextAnchor.__menuItemData;
        var childMenuItems = await GetChildMenuItems(menuItemData);
        if (Array.isArray(childMenuItems) && childMenuItems.length > 0) {
            var menuController = await __DCShowMenu(nextAnchor, null, null, childMenuItems);
            mainMenuBar.__currentOpenMenu = {
                anchor: nextAnchor,
                menuController: menuController
            };
            setRootHighlight(nextAnchor);
        }
    }

    async function switchToPreviousMainMenuItem() {
        var mainMenuBar = document.querySelector('nav[dctype="System.Windows.Forms.MainMenu"]');
        if (!mainMenuBar || !mainMenuBar.__currentOpenMenu) return;

        var currentAnchor = mainMenuBar.__currentOpenMenu.anchor;
        var allMenuItems = Array.from(mainMenuBar.querySelectorAll("#dc_menu_root_ul > li"));
        var currentIndex = allMenuItems.indexOf(currentAnchor);
        if (currentIndex === -1) return;

        var prevIndex = currentIndex === 0 ? allMenuItems.length - 1 : currentIndex - 1;
        var prevAnchor = allMenuItems[prevIndex];

        if (mainMenuBar.__currentOpenMenu.menuController) {
            mainMenuBar.__currentOpenMenu.menuController.close();
        }
        mainMenuBar.__currentOpenMenu = null;

        var menuItemData = prevAnchor.__menuItemData;
        var childMenuItems = await GetChildMenuItems(menuItemData);
        if (Array.isArray(childMenuItems) && childMenuItems.length > 0) {
            var menuController = await __DCShowMenu(prevAnchor, null, null, childMenuItems);
            mainMenuBar.__currentOpenMenu = {
                anchor: prevAnchor,
                menuController: menuController
            };
            setRootHighlight(prevAnchor);
        }
    }

    var keydownHandler = async function (e) {
        var key = e.key || e.keyCode;

        if (key === "ArrowDown" || key === 40) {
            e.preventDefault();
            e.stopPropagation();
            navigateDown();
            return;
        }

        if (key === "ArrowUp" || key === 38) {
            e.preventDefault();
            e.stopPropagation();
            navigateUp();
            return;
        }

        if (key === "ArrowRight" || key === 39) {
            e.preventDefault();
            e.stopPropagation();
            await navigateRight();
            return;
        }

        if (key === "ArrowLeft" || key === 37) {
            e.preventDefault();
            e.stopPropagation();
            await navigateLeft();
            return;
        }

        if (key === "Enter" || key === 13) {
            e.preventDefault();
            e.stopPropagation();
            await activateCurrentItem();
            return;
        }

        if (key === "Escape" || key === 27) {
            e.preventDefault();
            e.stopPropagation();

            if (menuState.allMenus.length > 1) {
                var lastMenu = menuState.allMenus[menuState.allMenus.length - 1];
                menuState.allMenus.pop();
                removeMenuElement(lastMenu);

                if (menuState.allMenus.length > 0) {
                    var parentMenu = menuState.allMenus[menuState.allMenus.length - 1];
                    var highlightedItem = parentMenu.querySelector(".dc-menu-item.dc-menu-highlight");
                    if (highlightedItem) {
                        menuState.currentHoverItem = highlightedItem;
                        highlightedItem.scrollIntoView({ block: "nearest", behavior: "smooth" });
                    }
                }
            } else {
                closeAllMenus();
            }
            return;
        }

        if (menuState.allMenus.length === 0) {
            return;
        }

        if (typeof key !== "string" || key.length !== 1) {
            return;
        }

        var pressedKey = key.toLowerCase();
        var matchedItems = [];
        var currentMenu = menuState.allMenus[menuState.allMenus.length - 1];
        if (!currentMenu || currentMenu.style.display === "none") {
            return;
        }

        var items = currentMenu.querySelectorAll(".dc-menu-item");
        items.forEach(function (item) {
            if (item.classList.contains("dc-menu-separator") || item.classList.contains("dc-menu-disabled")) {
                return;
            }

            var itemData = item.__menuData;
            if (!itemData || !itemData.Text) {
                return;
            }

            var text = String(itemData.Text);
            for (var i = 0; i < text.length - 1; i++) {
                if (text[i] === "&" && text[i + 1] !== "&") {
                    var mnemonic = text[i + 1].toLowerCase();
                    if (mnemonic === pressedKey) {
                        matchedItems.push({
                            item: item,
                            menu: currentMenu,
                            data: itemData
                        });
                    }
                    break;
                }
            }
        });

        if (matchedItems.length === 0) {
            return;
        }

        e.preventDefault();
        e.stopPropagation();

        if (matchedItems.length === 1) {
            var matched = matchedItems[0];
            var matchedItem = matched.item;
            var matchedData = matched.data;

            if (HasChildMenuItems(matchedData)) {
                var siblings = matched.menu.querySelectorAll(".dc-menu-item");
                siblings.forEach(function (sibling) {
                    sibling.classList.remove("dc-menu-highlight");
                });

                matchedItem.classList.add("dc-menu-highlight");
                menuState.currentHoverItem = matchedItem;

                var subMenu = await showSubMenu(matchedItem, matched.menu);
                if (subMenu) {
                    var firstValidItem = subMenu.querySelector(".dc-menu-item:not(.dc-menu-separator):not(.dc-menu-disabled)");
                    if (firstValidItem) {
                        firstValidItem.classList.add("dc-menu-highlight");
                        menuState.currentHoverItem = firstValidItem;
                    }
                }
            } else {
                var matchedHandle = GetItemHandle(matchedData);
                if (matchedHandle != null) {
                    OnMenuItemClick(matchedHandle);
                }
                closeAllMenus();
            }
        } else {
            var firstMatch = matchedItems[0].item;

            items.forEach(function (it) {
                it.classList.remove("dc-menu-highlight");
            });

            firstMatch.classList.add("dc-menu-highlight");
            menuState.currentHoverItem = firstMatch;
            firstMatch.scrollIntoView({ block: "nearest", behavior: "smooth" });
        }
    };

    document.addEventListener("keydown", keydownHandler);
    menuState.keydownHandler = keydownHandler;

    setTimeout(function () {
        if (!mainMenu) return;
        var firstValidItem = mainMenu.querySelector(".dc-menu-item:not(.dc-menu-separator):not(.dc-menu-disabled)");
        if (firstValidItem) {
            firstValidItem.classList.add("dc-menu-highlight");
            menuState.currentHoverItem = firstValidItem;
        }
    }, 50);

    return {
        close: closeAllMenus
    };
}

if (window.__DCControlTypes == null) window.__DCControlTypes = new Object();
window.__DCControlTypes["System.Windows.Forms.MainMenu"] = new SystemWindowsFormsMainMenuFactory();