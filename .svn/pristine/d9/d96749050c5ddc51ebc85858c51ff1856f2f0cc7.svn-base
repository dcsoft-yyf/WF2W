/** WF2W 配套的JS文件 南京都昌信息科技有限公司 2025-12-9 */
"use strict";
/** 判断模块是否完全加载完毕 */
window.IsDCWinFormWASMLoaded = function () {
    return window.__DCWinFormWASMLoaded === true;
};
function __DCInvokeCSMethod(strMethodName, ...args) {
    console.log(window.__DCEntryPointAssemblyName + " +++++ " + strMethodName);
    return DotNet.invokeMethod(
        window.__DCEntryPointAssemblyName,
        strMethodName,
        ...args);
};
function __DCInvokeCSMethodAsync(strMethodName, ...args) {
    return DotNet.invokeMethodAsync(
        window.__DCEntryPointAssemblyName,
        strMethodName,
        ...args);
};

function __DCExecuteControlCommand(intHandle, strCommandName, args) {
    return DotNet.invokeMethod(
        window.__DCEntryPointAssemblyName,
        "DCExecuteControlCommand",
        intHandle,
        strCommandName,
        args);
};
async function __DCExecuteControlCommandAsync(intHandle, strCommandName, args) {
    return await DotNet.invokeMethodAsync(
        window.__DCEntryPointAssemblyName,
        "DCExecuteControlCommandAsync",
        intHandle,
        strCommandName,
        args);
};
function __DCGetControlTypeFactory(typeName) {
    if (window.__DCControlTypes != null) {
        var fac = window.__DCControlTypes[typeName];
        if (fac != null && fac.Create != null && typeof (fac.Create) == "function") {
            return fac;
        }
    }
    return null;
};
function __DCGetResourceString(strKey) {
    if (window.__DCResourceStrings != null) {
        var str = window.__DCResourceStrings[strKey];
        if (str != null) return str;
    }
    return strKey;
};

/**
 * WinForms 助记符渲染：&X 为快捷键（下划线），&& 为字面 &。
 * @param {string} str 原始文本
 * @returns {{ html: string, access: string|null }} 渲染后的 HTML 与首个助记键（小写）
 */
function __DCRenderMnemonic(str) {
    var s = (str == null) ? "" : String(str);
    var out = "";
    var access = null;
    for (var i = 0; i < s.length; i++) {
        var ch = s[i];
        if (ch === "&") {
            if (i + 1 < s.length) {
                var next = s[i + 1];
                if (next === "&") {
                    out += "&";
                    i++;
                } else {
                    access = access || next.toLowerCase();
                    var esc = (next === "<") ? "&lt;" : (next === ">") ? "&gt;" : (next === "\"") ? "&quot;" : next;
                    out += "<span class=\"dc-underline\">" + esc + "</span>";
                    i++;
                }
            } else {
                out += "&";
            }
        } else {
            if (ch === "<") out += "&lt;";
            else if (ch === ">") out += "&gt;";
            else if (ch === "\"") out += "&quot;";
            else out += ch;
        }
    }
    return { html: out, access: access };
}

/** 确保助记符下划线样式已注入（全局一次） */
function __DCEnsureMnemonicUnderlineStyle() {
    if (!document.querySelector("style[data-dc-underline]")) {
        var style = document.createElement("style");
        style.setAttribute("data-dc-underline", "1");
        style.textContent = ".dc-underline{ text-decoration: underline; }";
        document.head.appendChild(style);
    }
}

(function () {
    if (window.__DCEntryPointAssemblyName == null) {
        window.__DCEntryPointAssemblyName = "DCSoft.WF2W";
    }
    if (window.___DCWinForm2WASM_JS_LOADED___) { return; }// 防止重复加载
    window.___DCWinForm2WASM_JS_LOADED___ = true;
    console.log("DCWinForm2WASM.js start loaded.");
    // 这里创建对象，获得一些数据来模拟System.Windows.Forms.Application和System.Environment等类的属性值
    var appInfo = {
        ProductName: document.title || "WF2W App",
        CommandLine: window.location.href,
        StartupPath: window.location.origin + window.location.pathname,
        ExecutablePath: window.location.href,
        CodeBase: window.location.href,
        AppBase: window.location.origin + "/",
        OSVersion: navigator.userAgent,
        Platform: navigator.platform,
        Culture: navigator.language || navigator.userLanguage,
        TimeZoneOffset: new Date().getTimezoneOffset()
    };
    __DCExecuteControlCommand(
        0,
        "SetApplicationData",
        appInfo);
    //async function ImportJSModule(strUrl) {
    //    var strUrl2 = strUrl;
    //    if (typeof (window.__DCResolverUrl) === "function") {
    //        strUrl2 = window.__DCResolverUrl(strUrl);
    //    }
    //    else {
    //        strUrl2 = new URL(strUrl, document.baseURI + "_framework/").href;
    //    }
    //    await import(strUrl2);
    //}
    //if (window.__DCResourceStrings == null) {
    //    await ImportJSModule("DCResourceStrings.js");
    //}

    //await ImportJSModule("DCWin32API.js");
    //await ImportJSModule("DCFontList.js");
    ////await ImportJSModule("DCWinFormAdapter.js");
    //await ImportJSModule("System.Windows.Forms.TextBox.js");
    //await ImportJSModule("System.Windows.Forms.Button.js");
    //await ImportJSModule("System.Windows.Forms.Label.js");
    //await ImportJSModule("System.Windows.Forms.Form.js");

    for (var item in window.__DCControlTypes) {
        __DCExecuteControlCommand(
            0,
            "AddStandardControlTypeName",
            item);
    }

    window.__DCSetScreen();
    window.__DCWinFormWASMLoaded = true;
})();
