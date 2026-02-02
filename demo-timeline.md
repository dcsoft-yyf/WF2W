# MWGA 如何帮助 7 万行C#源代码 WinForms 程序快速迁移到 WEB 前端 丨 How MWGA Can Help Quickly Migrate 70,000 Lines of C# Source Code WinForms Program to Web Frontend

**2026-1-29**

---

## 1. 前言 丨 Introduction

MWGA，是 Make Winforms Great Again 的缩写，是一个帮助 WinForms 程序快速迁移到 Blazor WASM 平台的高效工具软件。近期，我们借助 MWGA 成功将一个约 7 万行 C# 代码的成熟商业 WinForms 程序迁移至 Web 前端，整个过程快速且代码改动量极小，验证了其在复杂项目迁移中的不可思议的迁移能力。本文将以该案例为基础，概述迁移的核心思路与显著成果。

MWGA stands for "Make WinForms Great Again". It is an efficient tool that helps WinForms applications migrate quickly to the Blazor WASM platform. Recently, with MWGA we successfully migrated a mature commercial WinForms application of about 70,000 lines of C# code to the web frontend. The process was fast and required minimal code changes, demonstrating its remarkable migration capability in complex projects. This article uses this case as a basis to outline the core migration approach and notable results.

---

## 2. 案例程序说明 丨 Case Application Overview

本次迁移的对象是一款面向医院行业的商业软件——“时间轴”，主要用于患者体温单曲线图、跨机构居民健康档案数据可视化、手术室排程及住院患者数据分析。该软件功能复杂，紧密贴合医疗临床需求，具有以下特点：

The migration target is a commercial healthcare software product called "Timeline" (时间轴). It is mainly used for patient temperature chart curves, cross-institution resident health record data visualization, operating room scheduling, and inpatient data analysis. The software is feature-rich and closely aligned with clinical needs. It has the following characteristics:

1. **代码量大**：总计约 7 万行 C# 代码，其中包含近 4 万行与文档排版和 GDI+ 绘图相关的核心逻辑。

**Large codebase**: Approximately 70,000 lines of C# code in total, including nearly 40,000 lines of core logic related to document layout and GDI+ drawing.

2. **界面复杂**：实现了可交互的时间轴、动态区域展开/收缩、超链接文本、多数据层叠加（如血压曲线、疼痛指数、用药记录）等高级可视化功能。

**Complex UI**: It implements interactive timelines, dynamic region expand/collapse, hyperlink text, multi-layer data overlay (e.g., blood pressure curves, pain index, medication records), and other advanced visualization features.

3. **市场验证**：已在国内及台湾地区的多家医院稳定部署使用，是一个经过实践检验的成熟产品。

**Market-validated**: It has been stably deployed in multiple hospitals in mainland China and Taiwan, and is a mature product proven in practice.

这是用来展示跨医疗机构的居民健康档案数据的展示界面：

The following is the interface for displaying cross-institution resident health record data:

![跨机构居民健康档案数据展示界面 丨 Cross-institution resident health record data display interface](https://github.com/dcsoft-yyf/MWGA/blob/main/images/timeline-winform1.png?raw=true)

这个界面很复杂，将门诊、社区医院、住院、手术等数据串联在一起，形成不同的时间区域，时间区域可以展开和收缩，里面还有超连接文本。用户可以点击左边的坐标尺来显示和隐藏线条，当体温值过高和过低时会有小箭头并伴随纵向文本。

This interface is complex: it strings together outpatient, community hospital, inpatient, and surgery data to form different time regions. Time regions can be expanded and collapsed and contain hyperlink text. Users can click the scale on the left to show or hide lines; when temperature values are too high or too low, small arrows appear with vertical text.

下图是医院内部手术室排程的界面，白色区域是已经开始的手术，灰色区域是计划中的手术，红色块表示发生意外的手术。

The figure below shows the in-hospital operating room scheduling interface. White areas represent surgeries that have started, gray areas represent planned surgeries, and red blocks indicate surgeries that encountered unexpected events.

![手术室排程界面 丨 Operating room scheduling interface](https://github.com/dcsoft-yyf/MWGA/blob/main/images/timeline-winform2.png?raw=true)

这个软件产品还卖到台湾医院了，下图为台湾医院中的住院患者数据展示界面，这里的阴影区域，上边缘是血压的收缩压，下边缘是舒张压，还有一个红色折线表示疼痛指数，上面的表格显示了住院时间，下面的表格显示了用药情况，可以明确展示出用药和血压及疼痛指数之间时间上的先后关系。

The software has also been sold to hospitals in Taiwan. The figure below shows the inpatient data display interface in a Taiwanese hospital. The shaded area has the upper edge as systolic blood pressure and the lower edge as diastolic blood pressure; a red polyline represents the pain index. The upper table shows length of stay and the lower table shows medication records, clearly showing the temporal relationship between medication, blood pressure, and pain index.

![台湾医院住院患者数据展示 丨 Inpatient data display in Taiwan hospital](https://github.com/dcsoft-yyf/MWGA/blob/main/images/timeline-winform3.png?raw=true)

这个软件紧密贴合医院临床需求，功能强大，是一个经过市场考验的软件产品。

The software is closely aligned with hospital clinical needs, powerful in functionality, and a market-proven product.

---

## 3. 迁移过程与成果 丨Migration Process and Results

借助 MWGA，我们以极低的成本完成了这个庞大项目的 Web 化迁移。整个过程遵循高度标准化的流程，核心环节如下：

With MWGA, we completed the web migration of this large project at very low cost. The entire process followed a highly standardized workflow. The core steps are as follows:

### 第一，创建项目 丨 Step 1: Create the Project

使用 VS.NET 2022 创建一个 Blazor WASM 9.0 的项目。并将原有 WinForms 项目的全部源代码与资源文件复制其中，包括 C# 代码文件、Form.Designer.cs、Form.resx 文件。

Create a Blazor WASM 9.0 project using VS.NET 2022. Copy all source code and resource files from the original WinForms project into it, including C# code files, Form.Designer.cs, and Form.resx files.

### 第二，引用 MWGA 程序集 丨 Step 2: Reference the MWGA Assembly

仅需引用一个独立的 DCSoft.MWGA.dll 程序集（约 4MB），即可获得完整的 WinForms 到 WebAssembly 的运行时支持。最新的 MWGA 程序文件下载地址为：【https://github.com/dcsoft-yyf/MWGA】。

Only one standalone assembly, DCSoft.MWGA.dll (about 4MB), needs to be referenced to obtain full WinForms-to-WebAssembly runtime support. The latest MWGA package can be downloaded at: [https://github.com/dcsoft-yyf/MWGA](https://github.com/dcsoft-yyf/MWGA).

### 第三，配置应用入口 丨 Step 3: Configure the Application Entry

通过 MWGA 提供的标准化引导模式，在 Blazor 应用中快速建立 WinForms 应用的运行环境。只需在 HTML 页面中添加一个指定的容器元素并进行简单配置，即可将 WinForms 主窗体启动并渲染于该容器内。

Using the standardized bootstrap mode provided by MWGA, the WinForms application runtime is quickly set up inside the Blazor app. Simply add a designated container element in the HTML page and perform minimal configuration to launch and render the WinForms main form inside that container.

### 第四，适配性调整 C# 代码 丨 Step 4: Adaptive C# Code Adjustments

针对 Web 环境与桌面环境的差异（WEB 前端编程相对于 WinForms 编程最大的差异就是异步编程模式），我们对少量代码进行了适配性修改，主要集中在异步化改造方面（例如文件对话框的调用），以确保在浏览器中的流畅交互。绝大部分业务逻辑、界面布局和 GDI+ 绘图代码均无需改动。

To address differences between the web and desktop environments (the main difference between web frontend and WinForms programming is the asynchronous programming model), we made adaptive changes to a small amount of code, mainly focusing on async adaptation (e.g., file dialog calls) to ensure smooth interaction in the browser. The vast majority of business logic, UI layout, and GDI+ drawing code required no changes.

案例程序中的其他代码不需要动，比如几万行的文档排版和绘图代码就不需要修改，例如：

Other code in the case application did not need to be touched. For example, tens of thousands of lines of document layout and drawing code required no modification, such as:

```csharp
if (text == null)
{
    // 否则只显示日数
    text = dtm.Day.ToString(this.Config.DateFormatString);
}
fullWidth = g.MeasureString(text, txtFont, 10000, centerFormat).Width;
if (rect2.Width < fullWidth)
{
    rect2.X = rect2.X - (fullWidth - rect2.Width) / 2;
    rect2.Width = fullWidth;
}
////////////////////////////////////////////////////////////////////////
if (clipRectangle.IntersectsWith(rect2))
{
    Color tc = line == null ? this.Config.ForeColor : line.TextColor;
    tc = line != null && line.BlankDateWhenNoData == true && this._NoDataInDocument == true ? Color.Transparent : tc;
    g.DrawString(
        text,
        txtFont,
        GetRuntimeForeColor(tc),
        rect2,
        centerFormat);
}
```

### 第五，处理资源文件 丨 Step 5: Handle Resource Files

借助 MWGA 提供的资源管理兼容方案，原有的 .resx 文件及其在 Designer.cs 中的初始化代码可被自动识别和处理，无需手动修改。

With the resource management compatibility provided by MWGA, existing .resx files and their initialization code in Designer.cs can be automatically recognized and processed without manual modification.

例如 WinForms 程序会使用 Forms.resx 文件来存储窗体中使用到的资源，包括字符串或者图片。只需要添加几行代码，就借助 MWGA 实现了无缝资源迁移。

For example, WinForms applications use Forms.resx to store resources used in forms, including strings and images. With just a few lines of code, MWGA enables seamless resource migration.

### 第六，测试与成果展示 丨 Step 6: Testing and Results

完成上述步骤后，项目可直接编译为 Blazor WASM 应用。我们在多个平台和浏览器中进行了测试，均获得一致且良好的运行效果。

After completing the above steps, the project can be compiled directly as a Blazor WASM application. We tested it on multiple platforms and browsers and achieved consistent, good results.

经过迁移，这款 7 万行代码的 WinForms 应用成功转变为可通过浏览器直接访问的 Web 应用，并在不同环境下展现了出色的兼容性：

After migration, this 70,000-line WinForms application was successfully transformed into a web application that can be accessed directly through the browser and showed excellent compatibility across different environments:

- **在谷歌浏览器中运行界面** / *Running in Google Chrome*

![Chrome 运行效果 丨 Running in Chrome](https://github.com/dcsoft-yyf/MWGA/blob/main/images/timeline-chrome.png?raw=true)

- **在 FireFox 中运行的效果** / *Running in Firefox*

![Firefox 运行效果 丨 Running in Firefox](https://github.com/dcsoft-yyf/MWGA/blob/main/images/timeline-firefox.png?raw=true)

- **在 iPad 中的运行效果** / *Running on iPad*

![iPad 运行效果 丨 Running on iPad](https://github.com/dcsoft-yyf/MWGA/blob/main/images/timeline-ipad.png?raw=true)

- **在安卓平板中的运行效果** / *Running on Android tablet*

![安卓平板运行效果 丨 Running on Android tablet](https://github.com/dcsoft-yyf/MWGA/blob/main/images/timeline-android.png?raw=true)

- **该软件在统信操作系统中的运行效果** / *Running on UnionTech OS*

![统信 UOS 运行效果 丨 Running on UnionTech UOS](https://github.com/dcsoft-yyf/MWGA/blob/main/images/timeline-uos.png?raw=true)

迁移后的 Web 应用完整保留了原桌面版的所有交互逻辑和界面效果，包括复杂的 GDI+ 绘图、鼠标拖拽、点击响应等行为，用户体验与原生版本高度一致。

The migrated web application fully preserves all interaction logic and UI effects of the original desktop version, including complex GDI+ drawing, mouse drag, click response, and other behaviors. The user experience is highly consistent with the native version.

**公开演示地址** / **Public demo**: 为方便体验，我们提供了该案例的在线演示，大家可通过以下链接访问：【 https://dcsoft-yyf.github.io/MWGA/dctimeline.html 】

For easy trial, we provide an online demo of this case at: [https://dcsoft-yyf.github.io/MWGA/dctimeline.html](https://dcsoft-yyf.github.io/MWGA/dctimeline.html)

---

## 4. 结论与价值 丨 Conclusions and Value

本次迁移工作验证了以下结论：

This migration validated the following conclusions:

1. **修改量极低**：面对 7 万行代码，仅对不足 1% 的代码进行了必要的适配性调整，核心业务逻辑与绘图代码得以完全复用。相较于重写或采用其他迁移方案，基于 MWGA 的改造成本几乎可以忽略不计。

**Minimal modification**: Facing 70,000 lines of code, only less than 1% required necessary adaptive changes. Core business logic and drawing code were fully reused. Compared with rewriting or other migration approaches, the transformation cost based on MWGA is almost negligible.

2. **功能完整迁移**：复杂的用户界面和交互行为被高保真还原至 Web 前端。

**Complete feature migration**: Complex user interfaces and interaction behaviors were faithfully reproduced on the web frontend.

3. **真正的跨平台**：生成的应用可无缝运行于 Windows、Linux、Android、iOS 及统信 UOS、麒麟等国产操作系统的现代浏览器中。

**True cross-platform**: The resulting application runs seamlessly in modern browsers on Windows, Linux, Android, iOS, and domestic operating systems such as UnionTech UOS and Kylin.

4. **双线发展**：借助 MWGA 和条件编译等技术手段，可以让同一份 C# 代码同时编译成 .exe 和 .wasm 文件，让开发组织用较低的成本同时维护一个软件的 Windows 桌面版和 WEB 前端版。让软件产品适应更多的应用场景，为客户系统的平滑升级争取更多的时间。

**Dual deployment**: With MWGA and conditional compilation, the same C# codebase can be compiled into both .exe and .wasm, allowing the development organization to maintain both the Windows desktop version and the web frontend version at low cost. This allows the product to fit more scenarios and buy more time for smooth customer system upgrades.

5. **信创适配**：对于中国的开发者，MWGA 能将基于 MS Windows 的 WinForms 程序快速迁移到跨平台的 WEB 前端，大量软件产品有望躲过信创的斩杀线。

**Xinchuang (信创) adaptation**: For developers in China, MWGA can quickly migrate MS Windows–based WinForms applications to a cross-platform web frontend, allowing many software products to meet Xinchuang compliance requirements.

当然 WinForms 程序不仅仅是 UI 控件和 GDI+，还有很多其他的操作，比如连接数据库、访问本地文件、连接硬件设备等等，而 MWGA 运行在 WEB 浏览器沙盒中，没有能力完整的复现 WinForms 的全部功能集。但是 MWGA 的设计目标也不是解决所有的问题，单单是解决 UI 和 GDI+ 的功能迁移问题就足以创造巨大的价值。

Of course, WinForms applications are not only about UI controls and GDI+; they also involve database connections, local file access, hardware device connections, and more. MWGA runs in the web browser sandbox and cannot fully replicate the entire WinForms feature set. However, MWGA’s design goal is not to solve every problem—addressing UI and GDI+ migration alone is enough to create significant value.

MWGA 可以说创造了一个 WEB 前端框架，但是采用了 WinForms 编程模型。这是一种跨界融合，使得 WinForms 开发者无需更换技术栈即可参与 WEB 前端开发。同时，C# 的强类型特性与 GDI+ 严谨的编程模型也有助于减少 AI 编程产生的隐形 BUG。

MWGA can be said to create a web frontend framework that adopts the WinForms programming model. This is a cross-domain fusion that allows WinForms developers to participate in web frontend development without changing their technology stack. At the same time, C#’s strong typing and GDI+’s rigorous programming model help reduce hidden bugs introduced by AI-assisted programming.

全球数百万开发者过去 20 年在 WinForms 技术栈上投资巨大，可能过千亿美元，在云原生成为主流的今天，这些资产常被视为“沉没成本”，让成千上万的企业 CTO 倍感纠结。而 MWGA 有望盘活这些“沉没资产”，它率先探索出一条全新的技术路径，随着该路径的不断成熟，有望为全球 WinForms 技术栈延寿数年，可能会对全球的企业级软件研发领域带来不小的震荡。

Over the past 20 years, millions of developers worldwide have invested heavily in the WinForms technology stack—possibly over one hundred billion US dollars. With cloud-native becoming mainstream, these assets are often seen as "sunk costs," leaving many enterprise CTOs in a dilemma. MWGA has the potential to revitalize these "sunk assets." It has pioneered a new technical path; as this path matures, it may extend the life of the global WinForms technology stack by years and could have a significant impact on the global enterprise software R&D landscape.

**【MWGA 是南京都昌信息科技有限公司完全自主独立研发的软件产品，具有所有版权。】

**[MWGA is a software product independently developed by Nanjing Duchang Information Technology Co., Ltd. All rights reserved.]**
