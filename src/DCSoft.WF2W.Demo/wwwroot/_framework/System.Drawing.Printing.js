


"use strict";
/** 打印功能模块 */
class WF2WPrinting20260206 {
    constructor() {
        this.__Tasks = new Map();
        this.__TaskHandleCounter = 1;
    }
    /** 开始打印 */
    BeginPrint( strTitle ) {
        var iframe = document.createElement("iframe");
        iframe.style.width = "100%";
        iframe.style.height = "500px";
        iframe.style.border = "1px solid red";
        iframe.style.backgroundColor = "white"; 
        document.body.appendChild(iframe);
        var doc = iframe.contentDocument || iframe.contentWindow.document;
        doc.write("<!DOCTYPE html><html><head><title>" + strTitle + "</title></head><body></body></html>");
        doc.close();
        var task = {
            Handle: this.__TaskHandleCounter++,
            Element: iframe
        };
        this.__Tasks.set(task.Handle, task);
        return task.Handle;
    }
    /**
     * 设置打印内容，使用SVG字符串来描述页面内容，可以设置页面宽高来控制打印布局
     * @param {number} taskHandle 任务编号，BeginPrint方法返回的值
     * @param {string} strSVG SVG字符串，描述页面内容，可以使用基本的SVG元素和样式来布局打印内容
     * @param {number} intPageWidth 页面宽度，单位为像素，控制打印内容的布局宽度
     * @param {number} intPageHeight 页面高度，单位为像素，控制打印内容的布局高度
     * @returns
     */
    SetPageContent(taskHandle, strSVG, intPageWidth, intPageHeight) {
        var task = this.__Tasks.get(taskHandle);
        if (task != null) {
            var doc = task.Element.contentDocument || task.Element.contentWindow.document;
            var svg = doc.createElementNS("http://www.w3.org/2000/svg", "svg");
            svg.style.width = intPageWidth + "px";
            svg.style.height = intPageHeight + "px";
            svg.style.pageBreakAfter = "always";
            svg.style.pageBreakInside = "avoid";
            svg.innerHTML = strSVG;
            doc.body.appendChild(svg);
            return true;
        }
        return false;
    }
    /**
     * 结束打印任务
     * @param {number} taskHandle 任务编号，BeginPrint方法返回的值
     */
    EndPrint(taskHandle) {
        var task = this.__Tasks.get(taskHandle);
        if (task != null) {
            var win = task.Element.contentWindow;
            win.print();
            setTimeout(() => {
                //document.body.removeChild(task.Element);
            }, 1000);
            return true;
        }
        return false;
    }
};

window.__WF2WPrinting20260206 = new WF2WPrinting20260206();