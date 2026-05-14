/** 使用HTML 元素模拟 System.Windows.Forms.Form 窗体控件 */
"use strict";
class SystemWindowsFormsFormFactory extends SystemWindowsFormsContainerControlFactory {
    constructor() {
        super();
        this.DefaultConfig = {
            // 标题栏样式（Windows 7 风格）
            TitleBarHeight: 28,
            // 标题栏颜色（Win7 默认）
            InactiveTitleBarBackColor: '#bfcddb',
            InactiveTitleBarTextColor: '#000',
            ActiveTitleBarBackColor: '#99b4d1',
            ActiveTitleBarTextColor: '#000',
            // 主菜单栏的高度
            MainMenuHeight: 28,
            // 默认窗体边框样式
            DefaultFormBorderStyle: 'FixedSingle',
            // 默认内容背景色/前景色
            DefaultBackColor: '#fff',
            DefaultForeColor: '#000',
            DefaultBorderColor: '#888',
            DefaultBorderRadius: '3px'
        };
    }

    /**
     * 创建一个模拟窗体的 DOM 结构：外层容器 + 标题栏 + 内容区
     * options: { Handle, Text, Left, Top, Width, Height, Color, BackColor, Font, Visible, Enabled, BorderStyle, Controls }
     */
    Create(options) {
        console.log(options, 'options--form---------');
        options = options || {};
        var rootElement = super.Create.call(this, options);
        var oldChilds = [];
        for (var node = rootElement.firstChild; node; node = node.nextSibling) {
            oldChilds.push(node);
        }
        if (options.AutoDispose == true) {
            rootElement.__AutoDispose = true;
        }
        rootElement.innerText = ''; // clear existing content from base ContainerControl
        rootElement.IsForm = true;
        rootElement.setAttribute('dctype', 'System.Windows.Forms.Form');
        rootElement.setAttribute('dchandle', options.Handle);
        // allow container to receive focus
        rootElement.setAttribute('tabindex', '-1');

        // base styles: position absolute so left/top are relative to nearest positioned ancestor
        rootElement.style.position = 'absolute';
        rootElement.style.boxSizing = 'border-box';
        rootElement.style.display = 'block';
        rootElement.style.userSelect = 'none';
        var cfg = this.DefaultConfig;
        rootElement.style.background = (options.BackColor != null ? options.BackColor : cfg.DefaultBackColor);
        rootElement.style.color = (options.Color != null ? options.Color : cfg.DefaultForeColor);
        // FormBorderStyle handling (basic)
        var fbs = options.FormBorderStyle || cfg.DefaultFormBorderStyle;
        if (fbs === 'None') {
            rootElement.style.border = 'none';
            rootElement.style.borderRadius = '0';
        } else if (fbs === 'Fixed3D') {
            rootElement.style.border = '2px ridge #ccc';
            rootElement.style.borderRadius = cfg.DefaultBorderRadius;
        } else {
            // FixedSingle and others
            rootElement.style.border = '1px solid ' + cfg.DefaultBorderColor;
            rootElement.style.borderRadius = cfg.DefaultBorderRadius;
        }
        rootElement.style.overflow = 'hidden';
        // Ensure parent client area controls coordinate origin and clipping
        //function ensureParentClientArea() {
        //    try {
        //        var p = container.parentElement;
        //        if (!p) return;
        //        var target = p.__childContainer || p;
        //        var cs = window.getComputedStyle(target);
        //        if (!cs || cs.position === 'static') {
        //            target.style.position = 'relative';
        //        }
        //        // Clip overflow to emulate Win32 client area clipping
        //        target.style.overflow = 'clip';
        //    } catch (e) { }
        //}
        //// Attempt immediately (parent should be set by factory/CreateWindowEx before appending)
        //try { ensureParentClientArea(); } catch (e) { }

        // title bar
        var titleBar = document.createElement('div');
        titleBar.className = 'dc-winform-titlebar';
        titleBar.style.boxSizing = 'border-box';
        titleBar.style.width = '100%';
        titleBar.style.height = (cfg.TitleBarHeight + 'px');
        titleBar.style.lineHeight = (cfg.TitleBarHeight + 'px');
        titleBar.style.padding = '0 8px';
        titleBar.style.background = (options.TitleBackColor != null ? options.TitleBackColor : cfg.ActiveTitleBarBackColor);
        titleBar.style.color = (options.TitleForeColor != null ? options.TitleForeColor : cfg.ActiveTitleBarTextColor);
        titleBar.style.display = 'flex';
        titleBar.style.alignItems = 'center';
        titleBar.style.justifyContent = 'space-between';
        titleBar.style.cursor = 'default';

        // icon image (optional)
        var titleIcon = null;
        if (options.IconImage) {
            titleIcon = document.createElement('img');
            titleIcon.className = 'dc-winform-titleicon';
            titleIcon.src = options.IconImage;
            titleIcon.alt = '';
            titleIcon.style.width = '16px';
            titleIcon.style.height = '16px';
            titleIcon.style.objectFit = 'contain';
            titleIcon.style.marginRight = '6px';
            titleIcon.style.flex = '0 0 auto';
        }

        var titleText = document.createElement('div');
        titleText.className = 'dc-winform-titletext';
        titleText.style.flex = '1';
        titleText.style.overflow = 'hidden';
        titleText.style.textOverflow = 'ellipsis';
        titleText.style.whiteSpace = 'nowrap';
        titleText.style.paddingRight = '8px';
        titleText.innerText = options.Text || '';
        rootElement.__WindowTextElement = titleText;

        /**
        * 窗体大小或位置改变事件,模拟WIN32消息 WM_WINDOWPOSCHANGED 
        * @param {HTMLElement} formElement 窗体元素
        * @param {object} posInfo 窗体信息,模拟Win32 结构体 WINDOWPOS 
        */
        function PostMessage_WM_WINDOWPOSCHANGED(formElement, posInfo) {
            var task = {
                Name: "WM_WINDOWPOSCHANGED",
                Handle: posInfo.Handle,
                Info: posInfo,
                Eat: function (item2) {
                    return this.Handle == item2.Handle && this.Name == item2.Name;
                },
                Run: function () {
                    __DCExecuteControlCommand(
                        this.Handle,
                        this.Name,
                        this.Info);
                }
            };
            __DCWin32API.AddTask(task);
        };

        var titleButtons = document.createElement('div');
        titleButtons.style.display = 'flex';
        titleButtons.style.gap = '4px';

        // minimize button
        var minBtn = document.createElement('button');
        minBtn.type = 'button';
        minBtn.innerText = '▁';
        minBtn.title = 'Minimize';
        minBtn.style.height = '20px';
        minBtn.style.padding = '0 6px';
        // 暂时禁用最小化按钮，但仍显示（不可点击）
        minBtn.disabled = true;
        minBtn.style.cursor = 'default';
        minBtn.style.opacity = '0.7';

        // maximize / restore button
        var maxBtn = document.createElement('button');
        maxBtn.type = 'button';
        maxBtn.innerText = '▢';
        maxBtn.title = 'Maximize';
        maxBtn.style.height = '20px';
        maxBtn.style.padding = '0 6px';
        maxBtn.style.cursor = 'pointer';
        maxBtn.onclick = function (e) {
            e.stopPropagation();
            toggleMaximizeRestore();
            PostMessage_WM_WINDOWPOSCHANGED(rootElement, createPosInfo(rootElement));
        };

        // simple close button if ControlBox true (default true)
        var closeBtn = document.createElement('button');
        if (options.ControlBox === undefined || options.ControlBox === true) {
            closeBtn.type = 'button';
            closeBtn.innerText = '✕';
            closeBtn.title = 'Close';
            closeBtn.style.height = '20px';
            closeBtn.style.padding = '0 6px';
            closeBtn.style.cursor = 'pointer';
            closeBtn.onclick = function (e) {
                e.stopPropagation();
                // hide by default

                rootElement.style.display = 'none';
                if (typeof options.OnClose === 'function') options.OnClose(rootElement);
                else if (typeof options.OnClose === 'string' && window.DotNet && typeof DotNet.invokeMethodAsync === 'function') {
                    DotNet.invokeMethodAsync(window.__DCEntryPointAssemblyName, options.OnClose, rootElement.getAttribute('dchandle'));
                }
                PostMessage_WM_WINDOWPOSCHANGED(rootElement, createPosInfo(rootElement));
                if (rootElement.__AutoDispose === true) {
                    rootElement.__DCFactory.DisposeElement.call(rootElement.__DCFactory, rootElement);
                }
            };
        }

        // add buttons (order: min, max, close)
        titleButtons.appendChild(minBtn);
        titleButtons.appendChild(maxBtn);
        if (options.ControlBox === undefined || options.ControlBox === true) titleButtons.appendChild(closeBtn);

        if (titleIcon) titleBar.appendChild(titleIcon);
        titleBar.appendChild(titleText);
        titleBar.appendChild(titleButtons);

        rootElement.appendChild(titleBar);

        for (var item in oldChilds) {
            rootElement.appendChild(item);
        }
        rootElement.__TotalTitleHeight = cfg.TitleBarHeight;
        // 创建主菜单（使用 MainMenu.js 实现）
        if (options.Menu) {
            var menuFactory = window.__DCControlTypes["System.Windows.Forms.MainMenu"];
            if (menuFactory) {
                var nav = menuFactory.Create(
                    {
                        Handle: options.Menu.Handle,
                        Win32API: window.__DCWin32API
                    });
                nav.style.left = '0px';
                nav.style.top = (parseInt(titleBar.style.height) || 28) + 'px';
                nav.style.right = '0px';
                nav.style.height = '28px';
                rootElement.appendChild(nav);
                if (typeof menuFactory.ApplyOptions === 'function') {
                    menuFactory.ApplyOptions(nav, options.Menu);
                }
                var mh = nav.getBoundingClientRect().height || 28;
                rootElement.__TotalTitleHeight += mh;
                //content.style.top = ((parseInt(titleBar.style.height) || 28) + mh) + 'px';
                rootElement._dc_mainMenu = nav;
            }
        }
        rootElement.DoChildLayout = function () {
            var clientW = this.clientWidth;
            // 检查 content 中是否有 MenuStrip
            var menuStrip = this._dc_content ? this._dc_content.querySelector('nav[dctype="System.Windows.Forms.MenuStrip"]') : null;
            var menuHeight = 0;
            
            if (menuStrip) {
                menuHeight = menuStrip.getBoundingClientRect().height || 24;
                // 将 MenuStrip 移到 content 外面，放在标题栏下方
                if (menuStrip.parentElement !== this) {
                    // 先计算 MenuStrip 应该在的位置
                    var menuTop = this.__TotalTitleHeight;
                    menuStrip.style.left = '0px';
                    menuStrip.style.top = menuTop + 'px';
                    menuStrip.style.width = clientW + 'px';
                    menuStrip.style.right = '0px';
                    // 从 content 中移除，添加到 Form 根元素
                    this.insertBefore(menuStrip, this._dc_content);
                    // 更新 __TotalTitleHeight 包含菜单高度
                    this.__TotalTitleHeight = (this._dc_titleBar ? (this._dc_titleBar.getBoundingClientRect().height || 28) : 28) + menuHeight;
                }
            }
            var clientH = this.clientHeight - this.__TotalTitleHeight;
            if (this.__childHost) {
                this.__childHost.style.top = this.__TotalTitleHeight + "px";
                this.__childHost.style.width = clientW + "px";
                this.__childHost.style.height = clientH + "px";
            }
            if (this.__canvas) {
                this.__canvas.style.top = this.__TotalTitleHeight + "px";
                this.__canvas.width = clientW;
                this.__canvas.height = clientH;
                this.__canvas.style.width = clientW + "px";
                this.__canvas.style.height = clientH + "px";
            }
            // 更新 content 的 top
            if (this._dc_content) {
                this._dc_content.style.top = this.__TotalTitleHeight + 'px';
            }
        };
        rootElement.__GetClientSize = function () {
            if (this.__childHost == null) {
                return { Width: 0, Height: 0 };
            }
            else {
                return {
                    Width: this.__childHost.clientWidth,
                    Height: this.__childHost.clientHeight
                };
            }
        };
        // content area
        var content = rootElement.__GetChildContainer.call(rootElement);
        if (content == rootElement) {
            content = document.createElement("div");
            rootElement.__childHost = content;
            rootElement.__GetChildContainer = function () { return this.__childHost; };
            rootElement.appendChild(content);
        }
        content.className = 'dc-winform-content';
        content.style.position = 'absolute';
        content.style.left = '0px';
        content.style.right = '0px';
        content.style.top = rootElement.__TotalTitleHeight + 'px';
        content.style.bottom = '0px';
        content.style.overflow = 'hidden';
        content.style.backgroundColor = options.BackColor;
        //container.appendChild(content);
        // store references for ApplyOptions
        rootElement._dc_titleBar = titleBar;
        rootElement._dc_titleText = titleText;
        rootElement._dc_content = content;
        rootElement._dc_minBtn = minBtn;
        rootElement._dc_maxBtn = maxBtn;
        rootElement._dc_closeBtn = closeBtn;
        rootElement._dc_windowState = 'normal';
        rootElement._dc_prevBounds = null;

        // Windows 7 caption colors
        var WIN7_ACTIVE_CAPTION = cfg.ActiveTitleBarBackColor;
        var WIN7_INACTIVE_CAPTION = cfg.InactiveTitleBarBackColor;

        function applyCaptionActive(isActive) {
            titleBar.style.background = isActive ? WIN7_ACTIVE_CAPTION : WIN7_INACTIVE_CAPTION;
        };

        // initialize caption based on current focus
        applyCaptionActive(document.activeElement && rootElement.contains(document.activeElement));

        // focus handlers to update caption color
        rootElement.addEventListener('focusin', function () { applyCaptionActive(true); });
        rootElement.addEventListener('focusout', function () {
            // Delay to ensure focus actually leaves the form (not just moving within)
            setTimeout(function () {
                var isStillInside = document.activeElement && rootElement.contains(document.activeElement);
                applyCaptionActive(isStillInside);
            }, 0);
        });

        // focus container when clicking blank area to highlight caption
        rootElement.addEventListener('pointerdown', function (e) {
            var tag = (e.target && e.target.tagName) ? e.target.tagName.toLowerCase() : '';
            var interactive = tag === 'input' || tag === 'button' || tag === 'select' || tag === 'textarea' || tag === 'a';
            if (!interactive) {
                rootElement.focus();
            }
        });

        // disable browser context menu within the form; user handles right-click actions
        function preventContextMenu(e) { e.preventDefault(); e.stopPropagation(); return false; }
        rootElement.addEventListener('contextmenu', preventContextMenu, true);
        titleBar.addEventListener('contextmenu', preventContextMenu);
        content.addEventListener('contextmenu', preventContextMenu);

        // helper to get current pos info (without flags)
        function getCurrentPos(elem) {
            if (!elem) {
                return { Handle: -1, Left: 0, Top: 0, Width: 0, Height: 0, Visible: true, Z: 0, OffsetX: 0, OffsetY: 0 };
            }
            var handleAttr = elem.getAttribute ? elem.getAttribute('dchandle') : null;
            var leftVal = (elem.style && elem.style.left) ? parseInt(elem.style.left) : (elem.offsetLeft || 0);
            var topVal = (elem.style && elem.style.top) ? parseInt(elem.style.top) : (elem.offsetTop || 0);
            var widthVal = elem.offsetWidth || ((elem.style && elem.style.width) ? parseInt(elem.style.width) : 0) || 0;
            var heightVal = elem.offsetHeight || ((elem.style && elem.style.height) ? parseInt(elem.style.height) : 0) || 0;
            var computed = (typeof window !== 'undefined' && window.getComputedStyle) ? window.getComputedStyle(elem) : null;
            var visibleVal = !((elem.style && elem.style.display === 'none') || (computed && computed.display === 'none'));
            var zVal = (elem.style && elem.style.zIndex) ? parseInt(elem.style.zIndex) || 0 : 0;
            return {
                Handle: handleAttr != null ? (parseInt(handleAttr) || -1) : -1,
                Left: leftVal,
                Top: topVal,
                Width: widthVal,
                Height: heightVal,
                Visible: visibleVal,
                Z: zVal,
                OffsetX: leftVal,
                OffsetY: topVal
            };
        }

        // SetWindowPos/ WINDOWPOS flags (use same values as Win32 SWP_*)
        var SWP_NOSIZE = 0x0001;
        var SWP_NOMOVE = 0x0002;
        var SWP_NOZORDER = 0x0004;
        var SWP_NOACTIVATE = 0x0010;
        var SWP_DRAWFRAME = 0x0020;
        var SWP_SHOWWINDOW = 0x0040;
        var SWP_HIDEWINDOW = 0x0080;
        var SWP_NOOWNERZORDER = 0x0200;
        var SWP_NOSENDCHANGING = 0x0400;

        function computeFlags(prev, curr) {
            var flags = 0;
            if (!prev) {
                // no prev info, consider both move and size happened
            } else {
                var moved = (prev.Left !== curr.Left) || (prev.Top !== curr.Top);
                var resized = (prev.Width !== curr.Width) || (prev.Height !== curr.Height);
                var zchanged = (prev.Z !== curr.Z);
                var visChanged = (prev.Visible !== curr.Visible);

                if (!resized) flags |= SWP_NOSIZE;
                if (!moved) flags |= SWP_NOMOVE;
                if (!zchanged) flags |= SWP_NOZORDER;
                if (visChanged && curr.Visible) flags |= SWP_SHOWWINDOW;
                if (visChanged && !curr.Visible) flags |= SWP_HIDEWINDOW;
            }
            return flags;
        }

        // helper to create posInfo including flags and update lastPos
        function createPosInfo(elem) {
            var curr = getCurrentPos(elem);
            var prev = elem._dc_lastPos || null;
            curr.flags = computeFlags(prev, curr);
            // store last pos for next comparison
            elem._dc_lastPos = {
                Handle: curr.Handle,
                Left: curr.Left,
                Top: curr.Top,
                Width: curr.Width,
                Height: curr.Height,
                Visible: curr.Visible,
                Z: curr.Z
            };
            return curr;
        }

        // notify runtime when form size changes
        function OnFormResize(formElement) {
            // 更新内部子元素的布局
            if (typeof formElement.DoChildLayout === 'function') {
                formElement.DoChildLayout();
            }
            PostMessage_WM_WINDOWPOSCHANGED(formElement, createPosInfo(formElement));
        }

        // implement maximize/minimize/restore helpers
        function getParentForBounds() {
            return rootElement.__ScreenElement || rootElement.parentElement || document.querySelector('div[dctype="System.Windows.Forms.Screen"]') || document.body;
        }
        function doMaximize() {
            if (rootElement._dc_windowState === 'maximized') return;
            var parentEl = getParentForBounds();

            // 保存当前相对于 Screen 的位置和尺寸（而不是绝对位置）
            var containerRect = rootElement.getBoundingClientRect();
            var screenRect = parentEl.getBoundingClientRect();

            // 计算相对于 Screen 的位置
            var relativeLeft = parseFloat(rootElement.style.left);
            if (isNaN(relativeLeft)) {
                relativeLeft = containerRect.left - screenRect.left;
            }
            var relativeTop = parseFloat(rootElement.style.top);
            if (isNaN(relativeTop)) {
                relativeTop = containerRect.top - screenRect.top;
            }
            rootElement._dc_prevBounds = {
                left: relativeLeft,    // 相对于 Screen
                top: relativeTop,        // 相对于 Screen
                width: containerRect.width,
                height: containerRect.height
            };

            // 最大化：设置为 0,0，占满 Screen
            var pRect = parentEl.getBoundingClientRect();
            rootElement.style.left = '0px';
            rootElement.style.top = '0px';
            rootElement.style.width = "100%";
            rootElement.style.height = "100%";
            rootElement._dc_windowState = 'maximized';
            maxBtn.innerText = '❐';
            maxBtn.title = 'Restore';
            // 更新内部子元素的布局
            if (typeof rootElement.DoChildLayout === 'function') {
                rootElement.DoChildLayout();
            }
            // 更新 resizer 的 cursor 样式（禁用拖拽样式）
            updateResizersCursor();
            PostMessage_WM_WINDOWPOSCHANGED(rootElement, createPosInfo(rootElement));
        }

        function doRestore() {
            if (rootElement._dc_prevBounds) {
                // 获取 Screen 尺寸用于边界限制
                var parentEl = getParentForBounds();
                var screenWidth = parentEl.clientWidth || parentEl.offsetWidth || 0;
                var screenHeight = parentEl.clientHeight || parentEl.offsetHeight || 0;

                // 边界边距（距离边界的最小距离）
                var margin = 4;

                // 限制恢复位置在边界内，距离边界至少 margin px
                var newLeft = Math.max(margin, rootElement._dc_prevBounds.left);
                var newTop = Math.max(margin, rootElement._dc_prevBounds.top);

                // 确保不会超出边界，距离边界至少 margin px
                if (rootElement._dc_prevBounds.width <= screenWidth - 2 * margin) {
                    newLeft = Math.min(newLeft, screenWidth - rootElement._dc_prevBounds.width - margin);
                } else {
                    newLeft = Math.min(newLeft, screenWidth - margin);
                }
                if (rootElement._dc_prevBounds.height <= screenHeight - 2 * margin) {
                    newTop = Math.min(newTop, screenHeight - rootElement._dc_prevBounds.height - margin);
                } else {
                    newTop = Math.min(newTop, screenHeight - margin);
                }

                rootElement.style.left = newLeft + 'px';
                rootElement.style.top = newTop + 'px';
                rootElement.style.width = rootElement._dc_prevBounds.width + 'px';
                rootElement.style.height = rootElement._dc_prevBounds.height + 'px';
            }
            rootElement._dc_windowState = 'normal';
            maxBtn.innerText = '▢';
            maxBtn.title = 'Maximize';
            // 更新内部子元素的布局
            if (typeof rootElement.DoChildLayout === 'function') {
                rootElement.DoChildLayout();
            }
            // 更新 resizer 的 cursor 样式（恢复拖拽样式）
            updateResizersCursor();
            PostMessage_WM_WINDOWPOSCHANGED(rootElement, createPosInfo(rootElement));
        }
        function doMinimize() {
            rootElement.style.display = 'none';
            rootElement._dc_windowState = 'minimized';
            PostMessage_WM_WINDOWPOSCHANGED(rootElement, createPosInfo(rootElement));
        }
        function toggleMaximizeRestore() {
            if (rootElement._dc_windowState === 'maximized') doRestore();
            else doMaximize();
        }

        // 将最大化/还原函数绑定到 rootElement，供 DCWin32API 调用
        rootElement._dc_doMaximize = doMaximize;
        rootElement._dc_doRestore = doRestore;

        // titlebar drag to move window
        (function () {
            // 获取 Screen 容器并设置样式
            var screen = getParentForBounds();
            if (screen && screen.getAttribute('dctype') !== 'System.Windows.Forms.Screen') {
                screen = document.querySelector('div[dctype="System.Windows.Forms.Screen"]');
            }
            if (!screen) return;

            // 确保 Screen 有正确的定位和溢出控制（必须设置，否则边界限制无效）
            var screenComputedStyle = window.getComputedStyle(screen);
            if (!screenComputedStyle || screenComputedStyle.position === 'static') {
                screen.style.position = 'relative';
            }
            if (!screen.style.overflow || screen.style.overflow === 'visible') {
                screen.style.overflow = 'hidden';
            }

            var dragging = false;
            var startX = 0, startY = 0, startLeft = 0, startTop = 0;

            // 拖动开始
            titleBar.addEventListener('pointerdown', function (e) {
                // ignore drags on buttons或交互式目标
                var tag = (e.target && e.target.tagName) ? e.target.tagName.toLowerCase() : '';
                if (tag === 'button' || tag === 'input' || tag === 'select' || tag === 'a') return;
                if (rootElement._dc_windowState === 'maximized') return; // 不能在最大化时拖动
                // 捕获指针，以便我们在离开元素时仍然可以接收移动/抬起事件
                if (titleBar.setPointerCapture) titleBar.setPointerCapture(e.pointerId);
                dragging = true;

                // 记录初始鼠标位置
                startX = e.clientX;
                startY = e.clientY;

                // 计算相对于 Screen 的初始位置
                var containerRect = rootElement.getBoundingClientRect();
                var screenRect = screen.getBoundingClientRect();

                // 如果 container.style.left/top 已设置，使用它们；否则计算相对于 screen 的位置
                if (rootElement.style.left && rootElement.style.left.length) {
                    startLeft = parseFloat(rootElement.style.left);
                } else {
                    startLeft = containerRect.left - screenRect.left;
                }

                if (rootElement.style.top && rootElement.style.top.length) {
                    startTop = parseFloat(rootElement.style.top);
                } else {
                    startTop = containerRect.top - screenRect.top;
                }

                document.body.style.userSelect = 'none';
                e.preventDefault();
            });

            // 拖动过程
            titleBar.addEventListener('pointermove', function (e) {
                if (!dragging) return;

                // 计算鼠标移动距离
                var dx = e.clientX - startX;
                var dy = e.clientY - startY;

                // 计算新位置（相对于 Screen）
                var newLeft = startLeft + dx;
                var newTop = startTop + dy;

                // 获取尺寸
                var formWidth = rootElement.offsetWidth || 0;
                var formHeight = rootElement.offsetHeight || 0;
                var screenWidth = screen.clientWidth || screen.offsetWidth || 0;
                var screenHeight = screen.clientHeight || screen.offsetHeight || 0;

                // 边界边距（距离边界的最小距离）
                var margin = 4;

                // 限制边界：距离边界至少 margin px
                // 左边界：不能小于 margin
                newLeft = Math.max(margin, newLeft);
                // 上边界：不能小于 margin
                newTop = Math.max(margin, newTop);

                // 右边界：确保窗体右边缘距离 Screen 右边界至少 margin px
                if (formWidth <= screenWidth - 2 * margin) {
                    newLeft = Math.min(newLeft, screenWidth - formWidth - margin);
                } else {
                    // 窗体太大时，限制左边缘距离右边界至少 margin px
                    newLeft = Math.min(newLeft, screenWidth - margin);
                }

                // 下边界：确保窗体下边缘距离 Screen 下边界至少 margin px
                if (formHeight <= screenHeight - 2 * margin) {
                    newTop = Math.min(newTop, screenHeight - formHeight - margin);
                } else {
                    // 窗体太大时，限制上边缘距离下边界至少 margin px
                    newTop = Math.min(newTop, screenHeight - margin);
                }

                // 应用位置（相对于 Screen，因为 Screen 现在是 position: relative）
                rootElement.style.left = newLeft + 'px';
                rootElement.style.top = newTop + 'px';
                rootElement.style.transition = 'none';
            });

            // 拖动结束
            titleBar.addEventListener('pointerup', function (e) {
                if (!dragging) return;
                dragging = false;
                titleBar.releasePointerCapture(e.pointerId);
                document.body.style.userSelect = '';
                PostMessage_WM_WINDOWPOSCHANGED(rootElement, createPosInfo(rootElement));
            });

            titleBar.addEventListener('pointercancel', function (e) {
                if (!dragging) return;
                if (titleBar.releasePointerCapture) titleBar.releasePointerCapture(e.pointerId);
                dragging = false;
                document.body.style.userSelect = '';
                PostMessage_WM_WINDOWPOSCHANGED(rootElement, createPosInfo(rootElement));
            });

            // 双击标题栏切换最大化/还原
            titleBar.addEventListener('dblclick', function (e) {
                var tag = (e.target && e.target.tagName) ? e.target.tagName.toLowerCase() : '';
                if (tag === 'button' || tag === 'input' || tag === 'select' || tag === 'a') return;
                toggleMaximizeRestore();
            });
        })();

        // --- Resizers: create edges/corners for mouse resize and observe size changes ---
        var _resizers = [];
        var _resizeObserver = null;
        function updateResizersVisibility(visible) {
            _resizers.forEach(function (r) {
                r.style.display = visible ? 'block' : 'none';
            });
        }
        function updateResizersCursor() {
            // 如果窗口最大化，禁用拖拽 cursor 样式
            var isMaximized = rootElement._dc_windowState === 'maximized';
            _resizers.forEach(function (r) {
                var originalCursor = r.getAttribute('data-original-cursor');
                if (originalCursor) {
                    r.style.cursor = isMaximized ? 'default' : originalCursor;
                }
            });
        }

        function createResizersIfNeeded() {
            // default: enable resizing unless explicitly disabled
            var allowResize = true;
            if (options.Resize === false || options.Resizable === false) allowResize = false;
            if (options.FormBorderStyle === 'None') allowResize = false;
            // 若对话框模式，禁止鼠标拖拽修改窗体大小
            if (rootElement._dc_isDialog === true) allowResize = false;
            if (!allowResize) return;

            var handles = [
                { name: 'nw', cursor: 'nwse-resize', left: '0px', top: '0px', width: 8, height: 8 },
                { name: 'ne', cursor: 'nesw-resize', right: '0px', top: '0px', width: 8, height: 8 },
                { name: 'sw', cursor: 'nesw-resize', left: '0px', bottom: '0px', width: 8, height: 8 },
                { name: 'se', cursor: 'nwse-resize', right: '0px', bottom: '0px', width: 8, height: 8 },
                { name: 'n', cursor: 'ns-resize', left: '8px', right: '8px', top: '0px', height: 6 },
                { name: 's', cursor: 'ns-resize', left: '8px', right: '8px', bottom: '0px', height: 6 },
                { name: 'w', cursor: 'ew-resize', left: '0px', top: '8px', bottom: '8px', width: 6 },
                { name: 'r', cursor: 'ew-resize', right: '0px', top: '8px', bottom: '8px', width: 6 }
            ];

            handles.forEach(function (h) {
                var elGrip = document.createElement('div');
                elGrip.className = 'dc-resizer dc-resizer-' + h.name;
                elGrip.style.position = 'absolute';
                elGrip.style.background = 'transparent';
                elGrip.style.zIndex = 10000; // above window content
                elGrip.style.userSelect = 'none';
                // 根据窗口状态设置 cursor：最大化时使用 default，否则使用拖拽样式
                var isMaximized = rootElement._dc_windowState === 'maximized';
                elGrip.style.cursor = isMaximized ? 'default' : h.cursor;
                // 保存原始 cursor 值以便后续恢复
                elGrip.setAttribute('data-original-cursor', h.cursor);
                if (h.left) elGrip.style.left = h.left;
                if (h.top) elGrip.style.top = h.top;
                if (h.right) elGrip.style.right = h.right;
                if (h.bottom) elGrip.style.bottom = h.bottom;
                if (h.width) elGrip.style.width = (typeof h.width === 'number' ? h.width + 'px' : h.width);
                if (h.height) elGrip.style.height = (typeof h.height === 'number' ? h.height + 'px' : h.height);

                // pointer handlers
                elGrip.addEventListener('pointerdown', function (ev) {
                    ev.preventDefault();
                    if (rootElement._dc_windowState === 'maximized') return;

                    // 获取 Screen 元素（在 resizer 作用域内）
                    var screenEl = rootElement.__ScreenElement || rootElement.parentElement || document.querySelector('div[dctype="System.Windows.Forms.Screen"]');
                    if (!screenEl) return;

                    // 获取初始位置（相对于 Screen）
                    var containerRect = rootElement.getBoundingClientRect();
                    var screenRect = screenEl.getBoundingClientRect();
                    var startX = ev.clientX, startY = ev.clientY;

                    // 计算相对于 Screen 的初始位置
                    var startLeft = parseFloat(rootElement.style.left);
                    if (isNaN(startLeft)) {
                        startLeft = containerRect.left - screenRect.left;
                    }
                    var startTop = parseFloat(rootElement.style.top);
                    if (isNaN(startTop)) {
                        startTop = containerRect.top - screenRect.top;
                    }
                    var startWidth = containerRect.width;
                    var startHeight = containerRect.height;

                    // attach move/up to window
                    function onMove(eMove) {
                        eMove.preventDefault();
                        var dx = eMove.clientX - startX;
                        var dy = eMove.clientY - startY;
                        var minW = 50, minH = 24;

                        var newLeft = startLeft;
                        var newTop = startTop;
                        var newW = startWidth;
                        var newH = startHeight;

                        var name = h.name;
                        if (name.indexOf('n') !== -1) {
                            newTop = startTop + dy;
                            newH = startHeight - dy;
                        }
                        if (name.indexOf('s') !== -1) {
                            newH = startHeight + dy;
                        }
                        if (name.indexOf('w') !== -1) {
                            newLeft = startLeft + dx;
                            newW = startWidth - dx;
                        }
                        if (name === 'r' || name.indexOf('e') !== -1) {
                            newW = startWidth + dx;
                        }

                        if (newW < minW) {
                            // clamp and adjust left if needed
                            if (newW !== startWidth && (name.indexOf('w') !== -1)) {
                                newLeft = startLeft + (startWidth - minW);
                            }
                            newW = minW;
                        }
                        if (newH < minH) {
                            if (newH !== startHeight && (name.indexOf('n') !== -1)) {
                                newTop = startTop + (startHeight - minH);
                            }
                            newH = minH;
                        }

                        // 获取 Screen 尺寸用于边界限制
                        var screenWidth = screenEl.clientWidth || screenEl.offsetWidth || 0;
                        var screenHeight = screenEl.clientHeight || screenEl.offsetHeight || 0;

                        // 边界边距（距离边界的最小距离）
                        var margin = 4;

                        // 限制位置：距离边界至少 margin px
                        newLeft = Math.max(margin, newLeft);
                        newTop = Math.max(margin, newTop);

                        // 限制窗口大小不超出边界
                        // 右边界：窗口右边缘不能超过 screenWidth - margin
                        var maxWidth = screenWidth - newLeft - margin;
                        if (newW > maxWidth) {
                            newW = Math.max(minW, maxWidth);
                            // 如果是从左边调整大小，需要调整位置
                            if (name.indexOf('w') !== -1 && name.indexOf('e') === -1 && name.indexOf('r') === -1) {
                                newLeft = screenWidth - newW - margin;
                            }
                        }

                        // 下边界：窗口下边缘不能超过 screenHeight - margin
                        var maxHeight = screenHeight - newTop - margin;
                        if (newH > maxHeight) {
                            newH = Math.max(minH, maxHeight);
                            // 如果是从上边调整大小，需要调整位置
                            if (name.indexOf('n') !== -1 && name.indexOf('s') === -1) {
                                newTop = screenHeight - newH - margin;
                            }
                        }

                        // 再次限制位置，确保调整大小后位置仍然在边界内
                        if (newW <= screenWidth - 2 * margin) {
                            newLeft = Math.min(newLeft, screenWidth - newW - margin);
                        } else {
                            newLeft = Math.min(newLeft, screenWidth - margin);
                        }
                        if (newH <= screenHeight - 2 * margin) {
                            newTop = Math.min(newTop, screenHeight - newH - margin);
                        } else {
                            newTop = Math.min(newTop, screenHeight - margin);
                        }

                        try {
                            rootElement.style.left = Math.round(newLeft) + 'px';
                            rootElement.style.top = Math.round(newTop) + 'px';
                            rootElement.style.width = Math.round(newW) + 'px';
                            rootElement.style.height = Math.round(newH) + 'px';
                        } catch (ex) { }

                        // notify resize
                        OnFormResize(rootElement);
                    }
                    function onUp(eUp) {
                        window.removeEventListener('pointermove', onMove);
                        window.removeEventListener('pointerup', onUp);
                        document.body.style.userSelect = '';
                        PostMessage_WM_WINDOWPOSCHANGED(rootElement, createPosInfo(rootElement));
                    }
                    document.body.style.userSelect = 'none';
                    window.addEventListener('pointermove', onMove);
                    window.addEventListener('pointerup', onUp);
                });

                rootElement.appendChild(elGrip);
                _resizers.push(elGrip);
            });

            // observe size changes and call OnFormResize
            if (typeof ResizeObserver !== 'undefined') {
                _resizeObserver = new ResizeObserver(function () {
                    OnFormResize(rootElement);
                });
                _resizeObserver.observe(rootElement);
            } else {
                var lastW = rootElement.offsetWidth, lastH = rootElement.offsetHeight;
                var poll = setInterval(function () {
                    var w = rootElement.offsetWidth, h = rootElement.offsetHeight;
                    if (w !== lastW || h !== lastH) {
                        lastW = w; lastH = h;
                        OnFormResize(rootElement);
                    }
                    if (!document.body.contains(rootElement)) clearInterval(poll);
                }, 200);
            }
        }

        // apply options
        rootElement._dc_lastPos = getCurrentPos(rootElement);
        // create resize grips if allowed
        createResizersIfNeeded();
        /** 模拟win32 API GetWindowPlacement( ),返回一个WINDOWPLACEMENT结构体*/
        rootElement.GetWindowPlacement = function () {
            var SW_HIDE = 0;
            var SW_SHOWNORMAL = 1;
            var SW_SHOWMINIMIZED = 2;
            var SW_SHOWMAXIMIZED = 3;

            var placement = {
                flags: 0,
                showCmd: SW_SHOWNORMAL,
                minX: -1,
                minY: -1,
                maxX: -1,
                maxY: -1,
                normalLeft: 0,
                normalTop: 0,
                normalRight: 0,
                normalBottom: 0
            };

            try {
                if (rootElement.style && rootElement.style.display === 'none') {
                    placement.showCmd = SW_HIDE;
                } else {
                    switch (rootElement._dc_windowState) {
                        case 'minimized': placement.showCmd = SW_SHOWMINIMIZED; break;
                        case 'maximized': placement.showCmd = SW_SHOWMAXIMIZED; break;
                        default: placement.showCmd = SW_SHOWNORMAL; break;
                    }
                }
            } catch (e) { placement.showCmd = SW_SHOWNORMAL; }

            var left = 0, top = 0, width = 0, height = 0;
            // Prefer stored previous bounds when maximized
            if (rootElement._dc_windowState === 'maximized' && rootElement._dc_prevBounds) {
                // maximized: left/top should be (0,0) relative to parent
                left = 0;
                top = 0;
                width = Math.round(rootElement._dc_prevBounds.width || rootElement.offsetWidth || 0);
                height = Math.round(rootElement._dc_prevBounds.height || rootElement.offsetHeight || 0);
            } else {
                // Use current style/offset
                var rect = rootElement.getBoundingClientRect();
                var parent = rootElement.parentElement || document.body;
                var sRect = (parent && parent.getBoundingClientRect) ? parent.getBoundingClientRect() : { left: 0, top: 0 };
                var rawLeft = (rootElement.style.left && rootElement.style.left.length) ? parseFloat(rootElement.style.left) : Math.round(rect.left || rootElement.offsetLeft || 0);
                var rawTop = (rootElement.style.top && rootElement.style.top.length) ? parseFloat(rootElement.style.top) : Math.round(rect.top || rootElement.offsetTop || 0);
                left = Math.round(rawLeft - (sRect.left || 0));
                top = Math.round(rawTop - (sRect.top || 0));
                width = Math.round(rect.width || rootElement.offsetWidth || 0);
                height = Math.round(rect.height || rootElement.offsetHeight || 0);
            }

            placement.normalLeft = left;
            placement.normalTop = top;
            placement.normalRight = left + Math.max(0, width);
            placement.normalBottom = top + Math.max(0, height);

            // Max/min positions approximations
            try {
                // For this model, maximized origin is (0,0) relative to parent element
                placement.maxX = 0;
                placement.maxY = 0;
            } catch (e) { placement.maxX = 0; placement.maxY = 0; }

            // ptMinPosition: keep as (-1,-1) unless minimized; set to current left/top when minimized
            try {
                if (placement.showCmd === SW_SHOWMINIMIZED) {
                    placement.flags |= 0x0001; // WPF_SETMINPOSITION
                    placement.minX = left;
                    placement.minY = top;
                }
            } catch (e) { }

            return placement;
        };
        if (__DCWin32API.__DCShowDialog == null) {
            __DCWin32API.__DCShowDialog = async function (intHandle) {
                var dlg = __DCWin32API.DCGetControlByHandle(intHandle, true);
                if (dlg && typeof dlg.ShowDialog === 'function') {
                    return await dlg.ShowDialog();
                }
            };
        }
        if (__DCWin32API.__DCHiddenForm == null) {
            __DCWin32API.__DCHiddenForm = function (intHandle) {
                var dlg = __DCWin32API.DCGetControlByHandle(intHandle, true);
                if (dlg != null) {
                    dlg.style.display = "none";
                }
            };
        }
        // Simulate WinForms Form.ShowDialog: modal, non-freezing UI, nested allowed
        // Returns numeric DialogResult: None=0, OK=1, Cancel=2, Abort=3, Retry=4, Ignore=5, Yes=6, No=7
        rootElement.ShowDialog = async function () {
            var parentEl = getParentForBounds();
            var pRect = parentEl && parentEl.getBoundingClientRect ? parentEl.getBoundingClientRect() : { width: window.innerWidth, height: window.innerHeight };
            //container._dc_isDialog = true;
            var overlay = document.createElement('div');
            overlay.className = 'dc-modal-overlay';
            overlay.style.position = 'absolute';
            overlay.style.left = '0';
            overlay.style.top = '0';
            overlay.style.right = '0';
            overlay.style.bottom = '0';
            overlay.style.width = '100%';
            overlay.style.height = '100%';
            overlay.style.background = 'rgba(0,0,0,0.2)';
            overlay.style.zIndex = '999999';
            overlay.style.pointerEvents = 'auto';

            rootElement.style.display = '';
            rootElement.style.visibility = 'visible';
            var cz = Math.max(parseInt(rootElement.style.zIndex) || 1000, 1000);
            rootElement.style.zIndex = (cz + 2).toString();

            var rect = rootElement.getBoundingClientRect();
            var left = Math.max(0, Math.round(((pRect.width || 0) - rect.width) / 2));
            var top = Math.max(0, Math.round(((pRect.height || 0) - rect.height) / 2));
            rootElement.style.left = left + 'px';
            rootElement.style.top = top + 'px';

            var host = parentEl;
            host.appendChild(overlay);
            overlay.appendChild(rootElement);
            //if (container.parentElement !== host) host.appendChild(container);

            function trapFocus(e) {
                if (!rootElement.contains(e.target)) {
                    rootElement.focus();
                    e.stopPropagation();
                    e.preventDefault();
                }
            }
            overlay.addEventListener('focusin', trapFocus);
            parentEl && parentEl.addEventListener('focusin', trapFocus);
            rootElement.setAttribute('aria-modal', 'true');

            var resolveFn = null;
            var promise = new Promise(function (resolve) { resolveFn = resolve; });
            rootElement.__resolveDialog = resolveFn;
            if (typeof rootElement.__dialogResult !== 'number') rootElement.__dialogResult = 0;

            if (closeBtn) {
                var prevOnClick = closeBtn.onclick;
                closeBtn.onclick = function (e) {
                    rootElement.__dialogResult = (typeof options.DialogResultOnClose === 'number') ? options.DialogResultOnClose : 2;
                    if (prevOnClick) { prevOnClick.call(this, e); }
                };
            }

            if (typeof MutationObserver !== 'undefined') {
                var observer = new MutationObserver(function () {
                    var hidden = (rootElement.style.display === 'none') || (window.getComputedStyle(rootElement).display === 'none');
                    if (hidden) {
                        observer.disconnect();
                        overlay.remove();
                        if (parentEl) parentEl.removeEventListener('focusin', trapFocus);
                        var dr = (typeof rootElement.__dialogResult === 'number') ? rootElement.__dialogResult : 0;
                        if (rootElement.__resolveDialog) {
                            var fn = rootElement.__resolveDialog; rootElement.__resolveDialog = null;
                            fn(dr);
                        }
                    }
                });
                observer.observe(rootElement, { attributes: true, attributeFilter: ['style', 'class'] });
            }

            return promise;
        };
        rootElement.SetDialogResult = function (dr) {
            rootElement.__dialogResult = (typeof dr === 'number') ? dr : 0;
            rootElement.style.display = 'none';
        };
        return rootElement;
    }
    SetWindowText(rootElement, txt) {
        rootElement.__WindowTextElement.innerText = txt;
    }
    GetWindowText(rootElement) {
        return rootElement.__WindowTextElement.innerText;
    }
    /**
     * 将 options 应用到已创建的窗体元素上
     */
    ApplyOptions(htmlElement, options) {
        if (!htmlElement) return;
        options = options || {};

        var container = htmlElement;
        var titleBar = container._dc_titleBar || container.querySelector('.dc-winform-titlebar');
        var titleText = container.__WindowTextElement;
        var content = container._dc_content || container.querySelector('.dc-winform-content');

        var handle = options.Handle || options.DCHandle || options.HandleId || container.getAttribute('dchandle');
        if (handle != null) container.setAttribute('dchandle', handle);
        if (options.DCType) container.setAttribute('dctype', options.DCType);

        if (options.Left !== undefined && options.Left !== null) container.style.left = options.Left + 'px';
        if (options.Top !== undefined && options.Top !== null) container.style.top = options.Top + 'px';
  
       
        if (options.Width !== undefined && options.Width !== null) {
            var parentEl = document.querySelector("[dctype='System.Windows.Forms.Screen']")
            //判断container的宽度是否大于parentEl的宽度
            if (options.Width > parentEl.offsetWidth) {
                container.style.width = '100%';
            }else{
                container.style.width = options.Width + 'px';
            }
        }

        if (options.Height !== undefined && options.Height !== null) {
            var parentEl = document.querySelector("[dctype='System.Windows.Forms.Screen']")
            //判断container的高度是否大于parentEl的高度
            if (options.Height > parentEl.offsetHeight) {
                container.style.height = '100%';
            }else{
                container.style.height = options.Height + 'px';
            }
        }



        if (options.Text !== undefined && titleText) titleText.innerText = options.Text == null ? '' : options.Text;

        if (options.Color !== undefined) container.style.color = options.Color;
        if (options.BackColor !== undefined) container.style.background = options.BackColor;
        if (options.TitleBackColor !== undefined && titleBar) titleBar.style.background = options.TitleBackColor;
        if (options.TitleForeColor !== undefined && titleBar) titleBar.style.color = options.TitleForeColor;

        if (options.Visible !== undefined) container.style.display = options.Visible === false ? 'none' : '';

        if (options.Enabled !== undefined) {
            if (options.Enabled === false) {
                container.style.pointerEvents = 'none';
                container.style.opacity = '0.7';
            } else {
                container.style.pointerEvents = '';
                container.style.opacity = '';
            }
        }

        if (options.BorderStyle !== undefined) {
            switch (options.BorderStyle) {
                case 'None': container.style.border = 'none'; break;
                case 'FixedSingle': container.style.border = '1px solid #000'; break;
                case 'Fixed3D': container.style.border = '2px ridge #ccc'; break;
                default: container.style.border = options.BorderStyle; break;
            }
        }

        if (options.Font !== undefined) {
            if (window.__DCWin32API && typeof window.__DCWin32API.SetControlFont === "function") {
                window.__DCWin32API.SetControlFont(container, options.Font);
            }
        }



        if (titleBar && content) {
            // 使用 __TotalTitleHeight（标题栏+菜单高度）
            var th = (typeof container.__TotalTitleHeight === 'number' && container.__TotalTitleHeight >= 0)
                ? container.__TotalTitleHeight
                : titleBar.getBoundingClientRect().height;
            if (th > 0) {
                content.style.top = th + 'px';
            }
        }

        // Ensure parent client area and clipping
        var parentEl = container.parentElement;
        if (parentEl) {
            
            var target = parentEl.__GetChildContainer ? parentEl.__GetChildContainer() : parentEl;
            var cs = window.getComputedStyle(target);
            if (!cs || cs.position === 'static') {
                target.style.position = 'relative';
            }
            if (!target.style.overflow || target.style.overflow.length === 0) {
                target.style.overflow = 'clip';
            }
            
        }

        // Z-order handling: bring activated form to front among siblings; respect TopMost
        function isTopMost(elem) {
            try {
                var z = parseInt(elem.style.zIndex) || 0;
                return z >= 9999 || elem.getAttribute('data-topmost') === 'true';
            } catch (e) { return false; }
        }
        function bringFormToFront(elem) {
            var parent = elem ? elem.parentElement : null;
            if (!parent) return;
            var kids = parent.children || [];
            var maxZ = 0;
            for (var i = 0; i < kids.length; i++) {
                var k = kids[i];
                var dcbasecontroltypename = k.getAttribute('dcbasecontroltypename');
                var dctype = k.getAttribute('dctype');
                if (dcbasecontroltypename !== 'System.Windows.Forms.Form' && dctype !== 'System.Windows.Forms.Form') {
                    continue;
                }
                var z = parseInt(k.style.zIndex) || 0;
                if (z > maxZ) maxZ = z;
            }
            elem.style.zIndex = (maxZ + 1).toString();
        }

        container.addEventListener('focusin', function () { bringFormToFront(container); });
        container.addEventListener('pointerdown', function () { bringFormToFront(container); }, true);
        if (titleBar) titleBar.addEventListener('pointerdown', function () { bringFormToFront(container); }, true);
        if (content) content.addEventListener('pointerdown', function () { bringFormToFront(container); }, true);
   
    }
};

if (window.__DCControlTypes == null) window.__DCControlTypes = new Object();
window.__DCControlTypes["System.Windows.Forms.Form"] = new SystemWindowsFormsFormFactory();