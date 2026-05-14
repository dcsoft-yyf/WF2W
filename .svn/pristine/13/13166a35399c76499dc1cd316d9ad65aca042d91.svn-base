"use strict";
/**
 * 使用window.alert()模拟System.Windows.Forms.MessageBox.Show方法
 * @param {string} args.Text 消息文本
 * @param {string} args.Caption 消息框标题
 * @param {number} args.Buttons 消息框按钮类型
 * @param {number} args.Icon 消息框图标类型
 * @param {number} args.DefaultButton 默认按钮
 * @param {number} args.Options 其他选项
 * @returns {number} 返回值，表示用户点击的按钮,对应System.Windows.Forms.DialogResult枚举值
 */
function __DCShowMessageBoxOld(args) {
    args = args || {};
    const text = args.Text == null ? "" : String(args.Text);
    // caption not used for native alerts

    // DialogResult enum mapping
    const DialogResult = {
        None: 0,
        OK: 1,
        Cancel: 2,
        Abort: 3,
        Retry: 4,
        Ignore: 5,
        Yes: 6,
        No: 7
    };

    // MessageBoxButtons enum values in System.Windows.Forms
    const MessageBoxButtons = {
        OK: 0,
        OKCancel: 1,
        AbortRetryIgnore: 2,
        YesNoCancel: 3,
        YesNo: 4,
        RetryCancel: 5
    };

    const buttons = typeof args.Buttons === 'number' ? args.Buttons : MessageBoxButtons.OK;

    // Use simple native dialogs as a fallback
    switch (buttons) {
        case MessageBoxButtons.OKCancel: {
            const ok = window.confirm(text);
            return ok ? DialogResult.OK : DialogResult.Cancel;
        }
        case MessageBoxButtons.YesNo: {
            const yes = window.confirm(text);
            return yes ? DialogResult.Yes : DialogResult.No;
        }
        case MessageBoxButtons.RetryCancel: {
            const retry = window.confirm(text + "\n\nSelect OK to Retry, Cancel to Cancel.");
            return retry ? DialogResult.Retry : DialogResult.Cancel;
        }
        case MessageBoxButtons.AbortRetryIgnore:
        case MessageBoxButtons.YesNoCancel:
            // These complex button sets are not representable with simple alerts; show plain alert and return default
            window.alert(text);
            return DialogResult.None;
        case MessageBoxButtons.OK:
        default:
            window.alert(text);
            return DialogResult.OK;
    }
};

/**
 * 使用一个DIV高仿System.Windows.Forms.MessageBox.Show方法，并用异步等待操作返回用户点击的按钮结果
 * @param {string} args.Text 消息文本
 * @param {string} args.Caption 消息框标题
 * @param {number} args.Buttons 消息框按钮类型
 * @param {number} args.Icon 消息框图标类型
 * @param {number} args.DefaultButton 默认按钮
 * @param {number} args.Options 其他选项
 * @returns {number} 返回值，表示用户点击的按钮,对应System.Windows.Forms.DialogResult枚举值
 */
async function __DCShowMessageBoxNew(args) {
    args = args || {};
    const text = args.Text == null ? "" : String(args.Text);
    const caption = args.Caption == null ? "" : String(args.Caption);
    const buttons = typeof args.Buttons === 'number' ? args.Buttons : 0; // default OK
    const icon = typeof args.Icon === 'number' ? args.Icon : 0;
    const defaultButton = typeof args.DefaultButton === 'number' ? args.DefaultButton : 0;
    // DialogResult enum mapping
    const DialogResult = {
        None: 0,
        OK: 1,
        Cancel: 2,
        Abort: 3,
        Retry: 4,
        Ignore: 5,
        Yes: 6,
        No: 7
    };

    // MessageBoxButtons enum values in System.Windows.Forms
    const MessageBoxButtons = {
        OK: 0,
        OKCancel: 1,
        AbortRetryIgnore: 2,
        YesNoCancel: 3,
        YesNo: 4,
        RetryCancel: 5
    };

    // Icon mapping by common MessageBoxIcon numeric values (WinForms uses 16/32/48/64 for error/question/warning/info)
    const IconType = {
        None: 0,
        Error: 16,
        Question: 32,
        Warning: 48,
        Information: 64
    };

    // Inline SVGs for icons
    const svgs = {
        error: '<svg width="40" height="40" viewBox="0 0 64 64" xmlns="http://www.w3.org/2000/svg" role="img" aria-hidden="true"><circle cx="32" cy="32" r="30" fill="#D9534F"/><rect x="29" y="18" width="6" height="20" fill="#FFF"/><rect x="29" y="40" width="6" height="6" fill="#FFF"/></svg>',
        info: '<svg width="40" height="40" viewBox="0 0 64 64" xmlns="http://www.w3.org/2000/svg" role="img" aria-hidden="true"><circle cx="32" cy="32" r="30" fill="#2D89EF"/><rect x="29" y="26" width="6" height="18" fill="#FFF"/><rect x="29" y="18" width="6" height="6" fill="#FFF"/></svg>',
        warning: '<svg t="1769741366863" class="icon" viewBox="0 0 1024 1024" version="1.1" xmlns="http://www.w3.org/2000/svg" p-id="24042" width="40" height="40"><path d="M42.666667 896l938.666667 0-469.333333-810.666667-469.333333 810.666667zM554.666667 768l-85.333333 0 0-85.333333 85.333333 0 0 85.333333zM554.666667 597.333333l-85.333333 0 0-170.666667 85.333333 0 0 170.666667z" fill="#F0AD4E" p-id="24043"></path></svg>',
        question: '<svg t="1769741495995" class="icon" viewBox="0 0 1024 1024" version="1.1" xmlns="http://www.w3.org/2000/svg" p-id="31742" width="38" height="38"><path d="M512 1024A512 512 0 1 1 512 0a512 512 0 0 1 0 1024z m-63.168-320.128V832h130.624v-128.128H448.832zM311.04 416.576h122.304c0-14.976 1.664-28.992 4.992-41.984a101.12 101.12 0 0 1 15.36-34.112c7.04-9.728 15.872-17.472 26.688-23.296 10.816-5.824 23.68-8.768 38.656-8.768 22.208 0 39.552 6.08 52.032 18.304 12.48 12.16 18.688 31.04 18.688 56.576 0.576 14.976-2.048 27.52-7.872 37.44-5.824 9.984-13.44 19.2-22.912 27.52-9.408 8.32-19.648 16.64-30.72 24.96-11.136 8.32-21.696 18.112-31.68 29.44a171.904 171.904 0 0 0-26.24 41.216c-7.424 16.064-12.032 36.032-13.696 59.904v37.44H568.96v-31.616c2.24-16.64 7.68-30.528 16.256-41.6 8.576-11.072 18.432-20.928 29.504-29.504 11.136-8.64 22.912-17.216 35.392-25.792a162.048 162.048 0 0 0 59.904-75.264c6.912-17.28 10.368-39.168 10.368-65.792a149.312 149.312 0 0 0-44.928-104c-16.064-16.064-37.248-29.504-63.616-40.32-26.368-10.88-59.2-16.256-98.56-16.256-30.528 0-58.112 5.12-82.816 15.36a183.68 183.68 0 0 0-63.232 42.88A195.392 195.392 0 0 0 326.4 334.208c-9.728 24.96-14.848 52.48-15.36 82.368z" fill="#F0AD4E" p-id="31743"></path></svg>',
    };


    // Determine which icon to use
    let iconHtml = '';
    if (icon === IconType.Error) iconHtml = svgs.error;
    else if (icon === IconType.Question) iconHtml = svgs.question;
    else if (icon === IconType.Warning) iconHtml = svgs.warning;
    else if (icon === IconType.Information) iconHtml = svgs.info;

    // Determine buttons set
    let buttonSet = [];
    switch (buttons) {
        case MessageBoxButtons.OKCancel:
            buttonSet = [{ text: 'OK', result: DialogResult.OK }, { text: 'Cancel', result: DialogResult.Cancel }];
            break;
        case MessageBoxButtons.AbortRetryIgnore:
            buttonSet = [{ text: 'Abort', result: DialogResult.Abort }, { text: 'Retry', result: DialogResult.Retry }, { text: 'Ignore', result: DialogResult.Ignore }];
            break;
        case MessageBoxButtons.YesNoCancel:
            buttonSet = [{ text: 'Yes', result: DialogResult.Yes }, { text: 'No', result: DialogResult.No }, { text: 'Cancel', result: DialogResult.Cancel }];
            break;
        case MessageBoxButtons.YesNo:
            buttonSet = [{ text: 'Yes', result: DialogResult.Yes }, { text: 'No', result: DialogResult.No }];
            break;
        case MessageBoxButtons.RetryCancel:
            buttonSet = [{ text: 'Retry', result: DialogResult.Retry }, { text: 'Cancel', result: DialogResult.Cancel }];
            break;
        case MessageBoxButtons.OK:
        default:
            buttonSet = [{ text: 'OK', result: DialogResult.OK }];
            break;
    }

    // Unique container id
    window.__dc_mb_id = (window.__dc_mb_id || 0) + 1;
    const id = 'dc-messagebox-' + window.__dc_mb_id;

    // Create overlay
    const overlay = document.createElement('div');
    overlay.className = 'dc-messagebox-overlay';
    overlay.id = id;

    // Create box
    const box = document.createElement('div');
    box.className = 'dc-messagebox-box';
    box.setAttribute('role', 'dialog');
    box.setAttribute('aria-modal', 'true');
    box.setAttribute('aria-label', caption || 'Message');

    // Title bar
    const title = document.createElement('div');
    title.className = 'dc-messagebox-title';
    title.textContent = caption || '';

    // Content
    const content = document.createElement('div');
    content.className = 'dc-messagebox-content';

    const iconHolder = document.createElement('div');
    iconHolder.className = 'dc-messagebox-icon';
    if (iconHtml) iconHolder.innerHTML = iconHtml;

    const textHolder = document.createElement('div');
    textHolder.className = 'dc-messagebox-text';
    // allow HTML? keep plain text to avoid XSS
    textHolder.textContent = text;

    content.appendChild(iconHolder);
    content.appendChild(textHolder);

    // Buttons
    const buttonsBar = document.createElement('div');
    buttonsBar.className = 'dc-messagebox-buttons';

    // Create style (namespaced) if not present
    if (!document.getElementById('dc-messagebox-styles')) {
        const s = document.createElement('style');
        s.id = 'dc-messagebox-styles';
        s.textContent = `
.dc-messagebox-overlay{
    position:fixed;
    left:0;
    top:0;
    width:100%;
    height:100%;
    background:rgba(0,0,0,0.3);
    display:flex;
    align-items:center;
    justify-content:center;
    z-index:2147483646;
}
.dc-messagebox-box{
    width:420px;
    max-width:90%;
    background:#ffffff;
    border:1px solid #d0d0d0;
    box-shadow:0 2px 8px rgba(0,0,0,0.15);
    font-family:Segoe UI,Tahoma,Arial,sans-serif;
    border-radius:4px;
    overflow:hidden;
    user-select:none;
    position:absolute;
}
.dc-messagebox-title{
    background:#f5f5f5;
    padding:10px 16px;
    color:#333333;
    font-weight:600;
    font-size:14px;
    border-bottom:1px solid #e0e0e0;
    cursor:move;
    user-select:none;
}
.dc-messagebox-content{
    display:flex;
    padding:20px;
    background:#ffffff;
}
.dc-messagebox-icon{
    flex:0 0 56px;
    display:flex;
    align-items:flex-start;
    justify-content:center;
    margin-right:16px;
}
.dc-messagebox-text{
    flex:1;
    color:#333333;
    font-size:13px;
    line-height:1.5;
    white-space:pre-wrap;
    display:flex;
    align-items:center;
}
.dc-messagebox-buttons{
    display:flex;
    justify-content:center;
    padding:12px 16px;
    background:#ffffff;
    border-top:1px solid #e0e0e0;
    gap:8px;
}
.dc-messagebox-button{
    min-width:75px;
    padding:6px 16px;
    border-radius:3px;
    border:1px solid #c0c0c0;
    background:#ffffff;
    color:#333333;
    cursor:pointer;
    font-weight:normal;
    font-size:13px;
}
.dc-messagebox-button:hover{
    background:#f0f0f0;
    border-color:#a0a0a0;
}
.dc-messagebox-button:active{
    background:#e8e8e8;
}
.dc-messagebox-button:focus{
    outline:1px solid #4a90e2;
    outline-offset:1px;
}
`;
        document.head.appendChild(s);
    }

    // Promise to await
    let resolvePromise;
    const promise = new Promise((resolve) => { resolvePromise = resolve; });

    // Create button elements
    const btnElements = [];
    buttonSet.forEach((b, idx) => {
        const btn = document.createElement('button');
        btn.type = 'button';
        btn.className = 'dc-messagebox-button';
        btn.textContent = b.text;
        btn.dataset.result = String(b.result);
        // Set autofocus on default button if matches index or first
        if (defaultButton === idx || (defaultButton === 0 && idx === 0)) {
            btn.autofocus = true;
        }
        btn.addEventListener('click', () => close(Number(btn.dataset.result)));
        buttonsBar.appendChild(btn);
        btnElements.push(btn);
    });

    // Assemble
    box.appendChild(title);
    box.appendChild(content);
    box.appendChild(buttonsBar);
    overlay.appendChild(box);
    (document.body || document.documentElement).appendChild(overlay);

    // Initialize box position to center
    setTimeout(() => {
        const rect = box.getBoundingClientRect();
        const overlayRect = overlay.getBoundingClientRect();
        box.style.left = ((overlayRect.width - rect.width) / 2) + 'px';
        box.style.top = ((overlayRect.height - rect.height) / 2) + 'px';
    }, 0);

    // Drag functionality
    let isDragging = false;
    let dragStartX = 0;
    let dragStartY = 0;
    let boxStartX = 0;
    let boxStartY = 0;

    function startDrag(e) {
        if (e.button !== 0) return; // Only left mouse button
        isDragging = true;
        dragStartX = e.clientX;
        dragStartY = e.clientY;
        const rect = box.getBoundingClientRect();
        const overlayRect = overlay.getBoundingClientRect();
        boxStartX = rect.left - overlayRect.left;
        boxStartY = rect.top - overlayRect.top;
        title.style.cursor = 'grabbing';
        e.preventDefault();
        e.stopPropagation();
    }

    function onDrag(e) {
        if (!isDragging) return;
        const deltaX = e.clientX - dragStartX;
        const deltaY = e.clientY - dragStartY;
        const overlayRect = overlay.getBoundingClientRect();
        const boxRect = box.getBoundingClientRect();

        let newX = boxStartX + deltaX;
        let newY = boxStartY + deltaY;

        // Boundary constraints - keep box within overlay
        const minX = 0;
        const minY = 0;
        const maxX = overlayRect.width - boxRect.width;
        const maxY = overlayRect.height - boxRect.height;

        newX = Math.max(minX, Math.min(maxX, newX));
        newY = Math.max(minY, Math.min(maxY, newY));

        box.style.left = newX + 'px';
        box.style.top = newY + 'px';
        e.preventDefault();
    }

    function stopDrag(e) {
        if (isDragging) {
            isDragging = false;
            title.style.cursor = 'move';
        }
    }

    title.addEventListener('mousedown', startDrag);
    document.addEventListener('mousemove', onDrag);
    document.addEventListener('mouseup', stopDrag);

    // Focus management
    const prevActive = document.activeElement;
    // 注释掉自动聚焦，避免自动选中按钮
    // setTimeout(() => {
    //     const auto = box.querySelector('[autofocus]');
    //     if (auto) auto.focus(); else btnElements[0] && btnElements[0].focus();
    // }, 0);

    // Keyboard handling
    function keyHandler(e) {
        if (e.key === 'Escape') {
            // Escape -> Cancel if available, otherwise close with None
            const cancelBtn = btnElements.find(b => b.textContent.toLowerCase() === 'cancel');
            if (cancelBtn) close(Number(cancelBtn.dataset.result));
            else close(DialogResult.None);
        } else if (e.key === 'Enter') {
            // Enter -> trigger default (first focused or first button)
            const focused = document.activeElement;
            if (focused && focused.classList && focused.classList.contains('dc-messagebox-button')) {
                focused.click();
            } else if (btnElements[0]) {
                btnElements[0].click();
            }
        }
    }

    document.addEventListener('keydown', keyHandler);

    let closed = false;
    function cleanup() {
        if (overlay && overlay.parentNode) overlay.parentNode.removeChild(overlay);
        document.removeEventListener('keydown', keyHandler);
        document.removeEventListener('mousemove', onDrag);
        document.removeEventListener('mouseup', stopDrag);
        if (prevActive && prevActive.focus) try { prevActive.focus(); } catch (e) { }
    }

    function close(result) {
        if (closed) return;
        closed = true;
        cleanup();
        resolvePromise(result);
    }

    // Return awaited result
    return promise;
};
