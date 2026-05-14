"use strict";

/**
 * 模拟System.Windows.Forms.FontDialog.ShowDialog方法
 * @param {object} options 传入参数
 * @returns {Promise<string>} 选择结果(JSON字符串)，取消返回空字符串
 */
__DCWin32API.ShowFontDialog = async function (options) {
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

    function parseStyle(styleValue, key) {
        if (!styleValue) return false;
        if (typeof styleValue === "number") {
            if (key === "bold") return (styleValue & 1) !== 0;
            if (key === "italic") return (styleValue & 2) !== 0;
            if (key === "underline") return (styleValue & 4) !== 0;
            if (key === "strikeout") return (styleValue & 8) !== 0;
            return false;
        }
        var text = String(styleValue).toLowerCase();
        if (key === "strikeout") {
            return text.indexOf("strike") >= 0;
        }
        return text.indexOf(key) >= 0;
    }

    var opts = options || {};
    var minSize = Math.max(1, parseFloat(opts.MinSize || opts.minSize || 1) || 1);
    var maxSizeValue = parseFloat(opts.MaxSize || opts.maxSize || 0) || 0;
    var maxSize = maxSizeValue > 0 ? Math.max(minSize, maxSizeValue) : 0;
    var showEffects = opts.ShowEffects !== false;
    var showColor = opts.ShowColor === true;

    var current = {
        fontFamily: opts.FontFamily || opts.FontName || opts.Name || "Arial",
        size: parseFloat(opts.SizeInPoints || opts.Size || opts.FontSize || 9) || 9,
        bold: opts.Bold === true || parseStyle(opts.Style, "bold"),
        italic: opts.Italic === true || parseStyle(opts.Style, "italic"),
        underline: opts.Underline === true || parseStyle(opts.Style, "underline"),
        strikeout: opts.Strikeout === true || parseStyle(opts.Style, "strikeout"),
        color: normalizeColor(opts.Color || "#000000")
    };

    current.size = Math.max(minSize, current.size);
    if (maxSize > 0) {
        current.size = Math.min(maxSize, current.size);
    }

    var commonFonts = [
        "Arial", "Microsoft YaHei", "Segoe UI", "Tahoma", "Verdana", "Times New Roman", "Georgia", "Courier New", "Consolas", "SimSun", "SimHei", "KaiTi", "FangSong"
    ];

    return new Promise(function (resolve) {
        var finished = false;

        function cleanup(result) {
            if (finished) return;
            finished = true;
            document.removeEventListener("keydown", handleKeyDown);
            if (overlay.parentNode) {
                overlay.parentNode.removeChild(overlay);
            }
            resolve(result);
        }

        function buildResultJson() {
            return JSON.stringify({
                FontFamily: current.fontFamily,
                Name: current.fontFamily,
                Size: current.size,
                SizeInPoints: current.size,
                Bold: current.bold,
                Italic: current.italic,
                Underline: current.underline,
                Strikeout: current.strikeout,
                Color: current.color,
                Style: [
                    current.bold ? "Bold" : null,
                    current.italic ? "Italic" : null,
                    current.underline ? "Underline" : null,
                    current.strikeout ? "Strikeout" : null
                ].filter(function (x) { return x; }).join(",") || "Regular"
            });
        }

        function updatePreview() {
            preview.style.fontFamily = current.fontFamily;
            preview.style.fontSize = current.size + "pt";
            preview.style.fontWeight = current.bold ? "700" : "400";
            preview.style.fontStyle = current.italic ? "italic" : "normal";
            var decorations = [];
            if (current.underline) decorations.push("underline");
            if (current.strikeout) decorations.push("line-through");
            preview.style.textDecoration = decorations.join(" ") || "none";
            preview.style.color = showColor ? current.color : "#000000";
            colorValue.textContent = current.color.toUpperCase();
            colorValue.style.visibility = showColor ? "visible" : "hidden";
            colorPicker.style.visibility = showColor ? "visible" : "hidden";
        }

        var overlay = document.createElement("div");
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

        var dialog = document.createElement("div");
        dialog.style.background = "#fff";
        dialog.style.padding = "18px";
        dialog.style.borderRadius = "8px";
        dialog.style.boxShadow = "0 8px 24px rgba(0,0,0,0.35)";
        dialog.style.width = "620px";
        dialog.style.maxWidth = "92vw";
        dialog.style.display = "flex";
        dialog.style.flexDirection = "column";
        dialog.style.gap = "10px";

        var title = document.createElement("div");
        title.textContent = MyDCGetResourceString("FontDialog_Title", "字体");
        title.style.fontSize = "18px";
        title.style.fontWeight = "600";
        dialog.appendChild(title);

        var panel = document.createElement("div");
        panel.style.display = "grid";
        panel.style.gridTemplateColumns = "1fr 120px";
        panel.style.gap = "10px";

        var familySelect = document.createElement("select");
        familySelect.size = 10;
        familySelect.style.height = "220px";
        familySelect.style.padding = "4px";
        familySelect.style.width = "100%";
        commonFonts.forEach(function (f) {
            var opt = document.createElement("option");
            opt.value = f;
            opt.textContent = f;
            if (f.toLowerCase() === String(current.fontFamily).toLowerCase()) {
                opt.selected = true;
            }
            familySelect.appendChild(opt);
        });

        var rightPanel = document.createElement("div");
        rightPanel.style.display = "flex";
        rightPanel.style.flexDirection = "column";
        rightPanel.style.gap = "8px";

        var sizeInput = document.createElement("input");
        sizeInput.type = "number";
        sizeInput.min = String(minSize);
        sizeInput.step = "0.5";
        if (maxSize > 0) {
            sizeInput.max = String(maxSize);
        }
        sizeInput.value = String(current.size);
        sizeInput.style.padding = "6px";

        function addCheckBox(text, value, onChange) {
            var label = document.createElement("label");
            label.style.display = "flex";
            label.style.alignItems = "center";
            label.style.gap = "6px";
            var cb = document.createElement("input");
            cb.type = "checkbox";
            cb.checked = value;
            cb.addEventListener("change", function () {
                onChange(cb.checked);
                updatePreview();
            });
            label.appendChild(cb);
            label.appendChild(document.createTextNode(text));
            return label;
        }

        rightPanel.appendChild(addCheckBox(MyDCGetResourceString("FontDialog_Bold", "加粗"), current.bold, function (v) { current.bold = v; }));
        rightPanel.appendChild(addCheckBox(MyDCGetResourceString("FontDialog_Italic", "倾斜"), current.italic, function (v) { current.italic = v; }));
        rightPanel.appendChild(addCheckBox(MyDCGetResourceString("FontDialog_Underline", "下划线"), current.underline, function (v) { current.underline = v; }));
        rightPanel.appendChild(addCheckBox(MyDCGetResourceString("FontDialog_Strikeout", "删除线"), current.strikeout, function (v) { current.strikeout = v; }));

        if (!showEffects) {
            rightPanel.style.display = "none";
        }

        panel.appendChild(familySelect);
        panel.appendChild(rightPanel);
        dialog.appendChild(panel);

        var sizeRow = document.createElement("div");
        sizeRow.style.display = "flex";
        sizeRow.style.alignItems = "center";
        sizeRow.style.gap = "8px";
        var sizeLabel = document.createElement("span");
        sizeLabel.textContent = MyDCGetResourceString("FontDialog_Size", "字号");
        sizeLabel.style.minWidth = "56px";
        sizeRow.appendChild(sizeLabel);
        sizeRow.appendChild(sizeInput);
        dialog.appendChild(sizeRow);

        var colorRow = document.createElement("div");
        colorRow.style.display = "flex";
        colorRow.style.alignItems = "center";
        colorRow.style.gap = "8px";
        var colorLabel = document.createElement("span");
        colorLabel.textContent = MyDCGetResourceString("FontDialog_Color", "颜色");
        colorLabel.style.minWidth = "56px";
        var colorPicker = document.createElement("input");
        colorPicker.type = "color";
        colorPicker.value = current.color;
        var colorValue = document.createElement("span");
        colorValue.style.fontFamily = "monospace";
        colorRow.appendChild(colorLabel);
        colorRow.appendChild(colorPicker);
        colorRow.appendChild(colorValue);
        dialog.appendChild(colorRow);

        var preview = document.createElement("div");
        preview.style.border = "1px solid #ccc";
        preview.style.borderRadius = "4px";
        preview.style.padding = "12px";
        preview.style.minHeight = "68px";
        preview.style.display = "flex";
        preview.style.alignItems = "center";
        preview.style.justifyContent = "center";
        preview.textContent = MyDCGetResourceString("FontDialog_PreviewText", "AaBbYyZz 中文示例");
        dialog.appendChild(preview);

        var buttonContainer = document.createElement("div");
        buttonContainer.style.display = "flex";
        buttonContainer.style.justifyContent = "flex-end";
        buttonContainer.style.gap = "10px";

        var okButton = document.createElement("button");
        okButton.textContent = MyDCGetResourceString("OK", "确定");
        okButton.style.padding = "6px 22px";
        okButton.style.border = "1px solid #2c7be5";
        okButton.style.background = "#2c7be5";
        okButton.style.color = "#fff";
        okButton.style.borderRadius = "4px";
        okButton.style.cursor = "pointer";

        var cancelButton = document.createElement("button");
        cancelButton.textContent = MyDCGetResourceString("Cancel", "取消");
        cancelButton.style.padding = "6px 22px";
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

        familySelect.addEventListener("change", function () {
            current.fontFamily = familySelect.value;
            updatePreview();
        });

        sizeInput.addEventListener("change", function () {
            var v = parseFloat(sizeInput.value || "0") || current.size;
            v = Math.max(minSize, v);
            if (maxSize > 0) {
                v = Math.min(maxSize, v);
            }
            current.size = v;
            sizeInput.value = String(v);
            updatePreview();
        });

        colorPicker.addEventListener("input", function () {
            current.color = colorPicker.value;
            updatePreview();
        });

        okButton.addEventListener("click", function (e) {
            e.stopPropagation();
            cleanup(buildResultJson());
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
                cleanup(buildResultJson());
            }
        }

        document.addEventListener("keydown", handleKeyDown);
        updatePreview();
        window.setTimeout(function () {
            familySelect.focus();
        }, 0);
    });
};