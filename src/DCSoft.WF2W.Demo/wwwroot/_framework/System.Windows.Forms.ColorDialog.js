"use strict";

/**
 * 模拟System.Windows.Forms.ColorDialog.ShowDialog方法
 * @param {boolean} options.AllowFullOpen 是否允许定义自定义颜色
 * @param {boolean} options.AnyColor 是否显示所有可用颜色
 * @param {string} options.Color 初始颜色（格式: #RRGGBB 或 rgba(r,g,b,a)）
 * @param {Array<number>} options.CustomColors 自定义颜色数组（整数ARGB格式）
 * @param {boolean} options.FullOpen 是否在打开时显示自定义颜色控件
 * @param {boolean} options.SolidColorOnly 是否仅限于纯色
 * @returns {Promise<string>} 返回选择的颜色值（格式: #RRGGBB），如果取消则返回空字符串
 */
__DCWin32API.ShowColorDialog = async function (options) {
    function MyDCGetResourceString(strName, strDefaultValue) {
        return __DCGetResourceString ? __DCGetResourceString(strName) : strDefaultValue;
    }

    function normalizeColor(color) {
        if (!color || typeof color !== "string") return "#000000";
        if (/^#[0-9A-Fa-f]{6}$/.test(color)) return color;
        if (/^#[0-9A-Fa-f]{3}$/.test(color)) {
            return "#" + color[1] + color[1] + color[2] + color[2] + color[3] + color[3];
        }
        var rgbMatch = color.match(/rgba?\((\d+),\s*(\d+),\s*(\d+)/);
        if (rgbMatch) {
            var r = Math.max(0, Math.min(255, parseInt(rgbMatch[1], 10) || 0)).toString(16).padStart(2, "0");
            var g = Math.max(0, Math.min(255, parseInt(rgbMatch[2], 10) || 0)).toString(16).padStart(2, "0");
            var b = Math.max(0, Math.min(255, parseInt(rgbMatch[3], 10) || 0)).toString(16).padStart(2, "0");
            return "#" + r + g + b;
        }
        return "#000000";
    }

    function intColorToHex(value) {
        var n = Number(value);
        if (!Number.isFinite(n)) return null;
        n = n >>> 0;
        var rgb = n & 0x00ffffff;
        return "#" + rgb.toString(16).padStart(6, "0");
    }

    var opts = options || {};
    var allowFullOpen = opts.AllowFullOpen !== false;
    var fullOpen = opts.FullOpen === true;

    var basicColors = [
        "#000000", "#800000", "#008000", "#808000", "#000080", "#800080", "#008080", "#c0c0c0",
        "#808080", "#ff0000", "#00ff00", "#ffff00", "#0000ff", "#ff00ff", "#00ffff", "#ffffff",
        "#f4cccc", "#fce5cd", "#fff2cc", "#d9ead3", "#d0e0e3", "#cfe2f3", "#d9d2e9", "#ead1dc"
    ];

    var customColors = Array.isArray(opts.CustomColors) ? opts.CustomColors.map(intColorToHex).filter(Boolean) : [];
    if (customColors.length === 0) {
        customColors = ["#ffffff", "#ffffff", "#ffffff", "#ffffff", "#ffffff", "#ffffff", "#ffffff", "#ffffff",
            "#ffffff", "#ffffff", "#ffffff", "#ffffff", "#ffffff", "#ffffff", "#ffffff", "#ffffff"];
    }

    return new Promise(function (resolve) {
        var finished = false;
        var currentColor = normalizeColor(opts.Color || "#000000");

        function cleanup(result) {
            if (finished) return;
            finished = true;
            document.removeEventListener("keydown", handleKeyDown);
            if (overlay.parentNode) {
                overlay.parentNode.removeChild(overlay);
            }
            resolve(result);
        }

        function createColorCell(color, isCustom, index) {
            var btn = document.createElement("button");
            btn.type = "button";
            btn.style.width = "24px";
            btn.style.height = "24px";
            btn.style.border = "1px solid #999";
            btn.style.borderRadius = "3px";
            btn.style.background = color;
            btn.style.cursor = "pointer";
            btn.style.padding = "0";
            btn.title = color.toUpperCase();
            btn.addEventListener("click", function (e) {
                e.stopPropagation();
                currentColor = color;
                colorInput.value = color;
                selectedColorText.textContent = color.toUpperCase();
                preview.style.background = color;
                if (isCustom && typeof index === "number") {
                    activeCustomIndex = index;
                }
            });
            return btn;
        }

        function rebuildCustomGrid() {
            while (customGrid.firstChild) {
                customGrid.removeChild(customGrid.firstChild);
            }
            for (var i = 0; i < customColors.length; i++) {
                customGrid.appendChild(createColorCell(customColors[i], true, i));
            }
        }

        //获取当前窗口中最大的zIndex，然后加1
        var maxZIndex = 0;
        var allDialogElements = document.querySelectorAll("div.dc-modal-overlay");
        for (var i = 0; i < allDialogElements.length; i++) {
            var zIndex = parseInt(allDialogElements[i].style.zIndex) || 0;
            if (zIndex > maxZIndex) {
                maxZIndex = zIndex;
            }
        }
        var zIndex = maxZIndex + 1;



        var overlay = document.createElement("div");
        overlay.style.position = "fixed";
        overlay.style.left = "0";
        overlay.style.top = "0";
        overlay.style.right = "0";
        overlay.style.bottom = "0";
        overlay.style.background = "rgba(0,0,0,0.45)";
        overlay.style.zIndex = zIndex;
        overlay.style.display = "flex";
        overlay.style.alignItems = "center";
        overlay.style.justifyContent = "center";

        var dialog = document.createElement("div");
        dialog.style.background = "#fff";
        dialog.style.padding = "18px";
        dialog.style.borderRadius = "8px";
        dialog.style.boxShadow = "0 8px 24px rgba(0,0,0,0.35)";
        dialog.style.minWidth = "380px";
        dialog.style.maxWidth = "92vw";
        dialog.style.display = "flex";
        dialog.style.flexDirection = "column";
        dialog.style.gap = "10px";

        var title = document.createElement("div");
        title.textContent = MyDCGetResourceString("ColorDialog_Title", "颜色");
        title.style.fontSize = "18px";
        title.style.fontWeight = "600";
        dialog.appendChild(title);

        var basicTitle = document.createElement("div");
        basicTitle.textContent = MyDCGetResourceString("ColorDialog_BasicColors", "基本颜色");
        basicTitle.style.fontSize = "13px";
        basicTitle.style.color = "#666";
        dialog.appendChild(basicTitle);

        var basicGrid = document.createElement("div");
        basicGrid.style.display = "grid";
        basicGrid.style.gridTemplateColumns = "repeat(8, 24px)";
        basicGrid.style.gap = "6px";
        basicColors.forEach(function (c) {
            basicGrid.appendChild(createColorCell(c, false));
        });
        dialog.appendChild(basicGrid);

        var customTitle = document.createElement("div");
        customTitle.textContent = MyDCGetResourceString("ColorDialog_CustomColors", "自定义颜色");
        customTitle.style.fontSize = "13px";
        customTitle.style.color = "#666";
        dialog.appendChild(customTitle);

        var customGrid = document.createElement("div");
        customGrid.style.display = "grid";
        customGrid.style.gridTemplateColumns = "repeat(8, 24px)";
        customGrid.style.gap = "6px";
        dialog.appendChild(customGrid);

        var activeCustomIndex = 0;
        rebuildCustomGrid();

        var advancedRow = document.createElement("div");
        advancedRow.style.display = fullOpen ? "flex" : "none";
        advancedRow.style.alignItems = "center";
        advancedRow.style.gap = "8px";

        var colorInput = document.createElement("input");
        colorInput.type = "color";
        colorInput.value = currentColor;

        var selectedColorText = document.createElement("span");
        selectedColorText.style.fontFamily = "monospace";
        selectedColorText.textContent = currentColor.toUpperCase();

        var preview = document.createElement("div");
        preview.style.width = "48px";
        preview.style.height = "24px";
        preview.style.border = "1px solid #999";
        preview.style.borderRadius = "3px";
        preview.style.background = currentColor;

        advancedRow.appendChild(colorInput);
        advancedRow.appendChild(selectedColorText);
        advancedRow.appendChild(preview);
        dialog.appendChild(advancedRow);

        var fullOpenButton = document.createElement("button");
        fullOpenButton.type = "button";
        fullOpenButton.textContent = MyDCGetResourceString("ColorDialog_DefineCustomColors", "定义自定义颜色");
        fullOpenButton.style.alignSelf = "flex-start";
        fullOpenButton.style.padding = "4px 12px";
        fullOpenButton.style.border = "1px solid #ccc";
        fullOpenButton.style.background = "#fff";
        fullOpenButton.style.borderRadius = "4px";
        fullOpenButton.style.cursor = "pointer";
        fullOpenButton.style.display = allowFullOpen ? "inline-block" : "none";
        fullOpenButton.addEventListener("click", function (e) {
            e.stopPropagation();
            advancedRow.style.display = "flex";
        });
        dialog.appendChild(fullOpenButton);

        var buttonContainer = document.createElement("div");
        buttonContainer.style.display = "flex";
        buttonContainer.style.gap = "10px";
        buttonContainer.style.justifyContent = "flex-end";

        var okButton = document.createElement("button");
        okButton.textContent = MyDCGetResourceString("OK", "确定");
        okButton.style.padding = "6px 20px";
        okButton.style.border = "1px solid #2c7be5";
        okButton.style.background = "#2c7be5";
        okButton.style.color = "#fff";
        okButton.style.borderRadius = "4px";
        okButton.style.cursor = "pointer";

        var cancelButton = document.createElement("button");
        cancelButton.textContent = MyDCGetResourceString("Cancel", "取消");
        cancelButton.style.padding = "6px 20px";
        cancelButton.style.border = "1px solid #ccc";
        cancelButton.style.background = "#fff";
        cancelButton.style.color = "#333";
        cancelButton.style.borderRadius = "4px";
        cancelButton.style.cursor = "pointer";

        buttonContainer.appendChild(okButton);
        buttonContainer.appendChild(cancelButton);
        dialog.appendChild(buttonContainer);

        overlay.appendChild(dialog);
        document.body.appendChild(overlay);

        colorInput.addEventListener("input", function () {
            currentColor = colorInput.value;
            selectedColorText.textContent = currentColor.toUpperCase();
            preview.style.background = currentColor;
            if (allowFullOpen && activeCustomIndex >= 0 && activeCustomIndex < customColors.length) {
                customColors[activeCustomIndex] = currentColor;
                rebuildCustomGrid();
            }
        });

        okButton.addEventListener("click", function (e) {
            e.stopPropagation();
            cleanup(currentColor);
        });

        cancelButton.addEventListener("click", function (e) {
            e.stopPropagation();
            cleanup("");
        });

        overlay.addEventListener("click", function (e) {
            if (e.target === overlay) {
                cleanup("");
            }
        });

        function handleKeyDown(e) {
            if (e.key === "Escape") {
                cleanup("");
                return;
            }
            if (e.key === "Enter") {
                cleanup(currentColor);
            }
        }

        document.addEventListener("keydown", handleKeyDown);
        window.setTimeout(function () {
            okButton.focus();
        }, 0);
    });
};
