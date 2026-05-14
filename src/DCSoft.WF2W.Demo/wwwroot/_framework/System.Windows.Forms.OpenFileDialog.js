"use strict";

/**
 * 模拟System.Windows.Forms.OpenFileDialog.ShowDialog方法
 * @param {boolean} options.CheckFileExists 检查文件是否存在
 * @param {boolean} options.CheckPathExists 检查路径是否存在
 * @param {string} options.DefaultExt 默认扩展名
 * @param {boolean} options.DereferenceLinks 取消链接引用
 * @param {string} options.Filter 过滤器,格式: "描述1|*.扩展名1;*.扩展名2|描述2|*.扩展名3"
 * @param {number} options.FilterIndex 过滤器索引,从1开始
 * @param {string} options.InitialDirectory 初始目录
 * @param {boolean} options.Multiselect 多选
 * @param {string} options.Title 标题
 * @param {boolean} options.ValidateNames 验证名称
 * @param {string} options.RestoreDirectory 恢复目录
 * @param {boolean} options.ShowReadOnly 是否显示只读
 * @param {boolean} options.ReadOnlyChecked 只读选中
 * @returns {Promise<string>} 选择的文件名，如果是多个文件则文件名之间用'|'分隔开来。
 * 
 */
__DCWin32API.ShowOpenFileDialog = async function (options) {
    // 初始化文件读取器集合,只初始化一次,用于缓存用户选择的文件内容读取器
    if (__DCWin32API.__FileReaders === undefined) {
        __DCWin32API.__FileReaders = new Map();
    }
    __DCWin32API.ReadContentFromFileReader = async function (strFileName) {
        const file = __DCWin32API.__FileReaders.get(strFileName);
        __DCWin32API.__FileReaders.delete(strFileName);
        if (!file) return null;
        return new Promise((resolve, reject) => {
            const reader = new FileReader();
            reader.onload = function () {
                var result2 = new Uint8Array( reader.result );
                resolve(result2);
            };
            reader.onerror = () => reject(reader.error);
            reader.readAsArrayBuffer(file);
        });
    };

    return new Promise(function (resolve) {
        let finished = false;
        let input = null;
        const cleanup = function(result){
            if (finished) return;
            finished = true;
            if (input && input.parentNode) {
                input.parentNode.removeChild(input);
            }
            if (overlay.parentNode) {
                overlay.parentNode.removeChild(overlay);
            }
            resolve(result);
        };
        // 遮罩层
        const overlay = document.createElement("div");
        overlay.style.position = "fixed";
        overlay.style.left = "0";
        overlay.style.top = "0";
        overlay.style.right = "0";
        overlay.style.bottom = "0";
        overlay.style.background = "rgba(0,0,0,0.45)";
        overlay.style.zIndex = 99999;
        overlay.style.display = "flex";
        overlay.style.alignItems = "center";
        overlay.style.justifyContent = "center";

        const btn = document.createElement("button");
        btn.textContent = __DCGetResourceString("WarringForFileOpenDialog");
        btn.style.fontSize = "22px";
        btn.style.fontWeight = "700";
        btn.style.padding = "18px 32px";
        btn.style.minWidth = "320px";
        btn.style.cursor = "pointer";
        btn.style.border = "2px solid #2c7be5";
        btn.style.background = "#2c7be5";
        btn.style.color = "#fff";
        btn.style.borderRadius = "10px";
        btn.style.boxShadow = "0 8px 24px rgba(0,0,0,0.35)";

        overlay.appendChild(btn);
        document.body.appendChild(overlay);

        // 点击遮罩退出
        overlay.addEventListener("click", function () {
            cleanup("");
        });
        btn.focus();
        window.setTimeout(function () { btn.focus(); }, 0);
        // 阻止按钮冒泡到遮罩
        btn.addEventListener("click", function (e) {
            btn.innerText = __DCGetResourceString("WaittingForShowDialog");
            btn.disabled = true;
            btn.style.cursor = "wait";
            e.stopPropagation();
            // 创建输入框并配置
            input = document.createElement("input");
            input.type = "file";

            if (options && options.Multiselect) {
                input.multiple = true;
            }

            if (options && options.Filter) {
                const parts = options.Filter.split("|");
                const acceptList = [];
                for (let i = 1; i < parts.length; i += 2) {
                    const pattern = parts[i];
                    if (pattern) {
                        pattern.split(";").forEach(p => {
                            p = p.trim();
                            if (p.startsWith("*")) p = p.substring(1);
                            if (p.startsWith(".")) acceptList.push(p);
                        });
                    }
                }
                if (acceptList.length > 0) {
                    input.accept = acceptList.join(",");
                }
            }

            input.style.position = "fixed";
            input.style.left = "-9999px";
            document.body.appendChild(input);

            const finalize = function () {
                const files = Array.from(input.files || []);
                const resultNames = [];
                files.forEach(file => {
                    resultNames.push(file.name);
                    __DCWin32API.__FileReaders.set(file.name, file);
                });
                cleanup(resultNames.join("|"));
            };

            input.addEventListener("change", function () {
                if (!finished) finalize();
            }, { once: true });

            // 支持 cancel 事件（若浏览器支持）
            input.addEventListener("cancel", function () {
                if (!finished) cleanup("");
            }, { once: true });

            input.click();
        }, { once: true });
    });

};