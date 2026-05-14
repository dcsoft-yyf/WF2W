"use strict";

/**
 * 创建一个HatchBrush对象，可以设置到CanvasRenderingContext2D中作为填充样式
 * @param {string} strOptions 信息字符串
 * @returns {CanvasPattern} Canvas填充样式对象
 */
function _DC_CreateHatchBrush(strOptions) {
    var style = 0;
    var fg = "#000";
    var bg = "transparent";

    if (typeof strOptions === "string" && strOptions.length > 0) {
        var parts = strOptions.split("$");
        if (parts.length >= 3) {
            var stylePart = parts[0];
            if (stylePart.startsWith("^")) stylePart = stylePart.substring(1);
            var parsed = parseInt(stylePart, 10);
            if (!isNaN(parsed)) style = parsed;
            if (parts[1]) fg = parts[1];
            if (parts[2]) bg = parts[2];
        }
    }

    var canvas = document.createElement("canvas");
    // 适中的图案尺寸，可覆盖大部分 hatch 样式
    var size = 12;
    canvas.width = size;
    canvas.height = size;
    var ctx = canvas.getContext("2d");
    ctx.fillStyle = bg;
    ctx.fillRect(0, 0, size, size);
    ctx.strokeStyle = fg;
    ctx.fillStyle = fg;
    ctx.lineWidth = 1;

    function line(x1, y1, x2, y2, lw) {
        if (lw) ctx.lineWidth = lw; else ctx.lineWidth = 1;
        ctx.beginPath();
        ctx.moveTo(x1, y1);
        ctx.lineTo(x2, y2);
        ctx.stroke();
    }
    function horiz(step, lw) {
        for (var y = 0; y <= size; y += step) line(0, y + 0.5, size, y + 0.5, lw);
    }
    function vert(step, lw) {
        for (var x = 0; x <= size; x += step) line(x + 0.5, 0, x + 0.5, size, lw);
    }
    function diagForward(step, lw) { // tl-br
        for (var k = -size; k <= size; k += step) {
            line(k, 0, k + size, size, lw);
        }
    }
    function diagBackward(step, lw) { // tr-bl
        for (var k = 0; k <= size * 2; k += step) {
            line(k, 0, k - size, size, lw);
        }
    }
    function dots(step, sizeDot) {
        for (var y = 0; y < size; y += step) {
            for (var x = 0; x < size; x += step) {
                ctx.fillRect(x, y, sizeDot, sizeDot);
            }
        }
    }
    function checker(step, sizeDot) {
        for (var y = 0; y < size; y += step) {
            for (var x = 0; x < size; x += step) {
                if (((x / step) + (y / step)) % 2 === 0) ctx.fillRect(x, y, sizeDot, sizeDot);
            }
        }
    }
    function brick(horizontal) {
        var h = horizontal ? size / 3 : size / 2;
        ctx.lineWidth = 1;
        if (horizontal) {
            horiz(h, 1);
            line(0, h / 2, size / 2, h / 2, 1);
            line(size / 2, h + h / 2, size, h + h / 2, 1);
        } else {
            vert(h, 1);
            line(h / 2, 0, h / 2, size / 2, 1);
            line(h + h / 2, size / 2, h + h / 2, size, 1);
        }
    }
    function waves(step) {
        ctx.lineWidth = 1;
        for (var y = 0; y <= size; y += step) {
            ctx.beginPath();
            ctx.moveTo(0, y + step / 2);
            ctx.quadraticCurveTo(size / 4, y, size / 2, y + step / 2);
            ctx.quadraticCurveTo(size * 3 / 4, y + step, size, y + step / 2);
            ctx.stroke();
        }
    }
    function zigzag(step) {
        ctx.lineWidth = 1;
        for (var y = 0; y <= size; y += step) {
            ctx.beginPath();
            ctx.moveTo(0, y + step / 2);
            ctx.lineTo(size / 4, y);
            ctx.lineTo(size / 2, y + step / 2);
            ctx.lineTo(size * 3 / 4, y);
            ctx.lineTo(size, y + step / 2);
            ctx.stroke();
        }
    }
    function diamond(outlineOnly) {
        var half = size / 2;
        ctx.beginPath();
        ctx.moveTo(half, 0);
        ctx.lineTo(size, half);
        ctx.lineTo(half, size);
        ctx.lineTo(0, half);
        ctx.closePath();
        if (outlineOnly) ctx.stroke(); else ctx.fill();
    }

    var s = parseInt(style, 10);
    switch (s) {
        case 0: horiz(4); break; // Horizontal
        case 1: vert(4); break; // Vertical
        case 2: diagForward(4); break; // ForwardDiagonal
        case 3: diagBackward(4); break; // BackwardDiagonal
        case 4: horiz(4); vert(4); break; // Cross
        case 5: diagForward(4); diagBackward(4); break; // DiagonalCross
        case 6: dots(6, 1); break;
        case 7: dots(5, 1); break;
        case 8: dots(4, 1); break;
        case 9: dots(4, 2); break;
        case 10: dots(3, 1); break;
        case 11: dots(3, 2); break;
        case 12: checker(3, 2); break;
        case 13: dots(2.5, 2); break;
        case 14: dots(2, 2); break;
        case 15: dots(2, 2.5); break;
        case 16: dots(2, 3); break;
        case 17: dots(1.5, 3); break;
        case 18: diagForward(6); break;
        case 19: diagBackward(6); break;
        case 20: diagForward(3); break;
        case 21: diagBackward(3); break;
        case 22: diagForward(8, 2); break;
        case 23: diagBackward(8, 2); break;
        case 24: vert(6); break;
        case 25: horiz(6); break;
        case 26: vert(2); break;
        case 27: horiz(2); break;
        case 28: vert(3, 2); break;
        case 29: horiz(3, 2); break;
        case 30: diagForward(6); ctx.setLineDash([3, 3]); diagForward(6); ctx.setLineDash([]); break;
        case 31: diagBackward(6); ctx.setLineDash([3, 3]); diagBackward(6); ctx.setLineDash([]); break;
        case 32: ctx.setLineDash([3, 3]); horiz(4); ctx.setLineDash([]); break;
        case 33: ctx.setLineDash([3, 3]); vert(4); ctx.setLineDash([]); break;
        case 34: dots(3, 1); break;
        case 35: dots(4, 2); break;
        case 36: zigzag(4); break;
        case 37: waves(4); break;
        case 38: brick(false); diagForward(6); break;
        case 39: brick(true); break;
        case 40: horiz(4); vert(4); diagForward(8); diagBackward(8); break;
        case 41: horiz(3); vert(3); break;
        case 42: dots(4, 2); break;
        case 43: dots(4, 1.5); break;
        case 44: diagForward(6); diagBackward(6); break;
        case 45: horiz(4); diagForward(8); break;
        case 46: horiz(3); vert(3); break;
        case 47: ctx.beginPath(); ctx.arc(size / 2, size / 2, size / 3, 0, Math.PI * 2); ctx.fill(); break;
        case 48: horiz(3); vert(3); break;
        case 49: checker(4, 2); break;
        case 50: checker(6, 3); break;
        case 51: diamond(true); break;
        case 52: diamond(false); break;
        default:
            horiz(4);
            break;
    }

    return ctx.createPattern(canvas, "repeat");
}