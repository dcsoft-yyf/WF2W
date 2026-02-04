# MWGA vs Blazor Hybrid & Avalonia

> *This document clarifies the positioning, key differences, and use cases of the three options to support WinForms migration and technology upgrade decisions.*
>
> **For minimal migration cost, retention of GDI+ drawing and UI, fast Web enablement, domestic/legacy system support, and lower migration risk, MWGA is the best choice today.**

---

## Part 1: Core Positioning (One-Line Summary)

| Solution | Positioning |
|:-----|:-----|
| **MWGA（Make WinForms Great Again）** | *Dedicated WinForms→Blazor WASM migration tool: minimal change, high-fidelity port, one codebase for desktop and Web; fast Web enablement with little UI/GDI+ rewrite.* |
| **Blazor Hybrid** | *Hybrid UI: Blazor inside WebView2 in WinForms/WPF/MAUI; keep native capabilities and reuse Web stack for modern UI; incremental Web UI in WinForms.* |
| **Avalonia** | *Cross-platform native UI framework (WPF-like, Skia); leave WinForms and rewrite with a modern cross-platform stack; long-term refresh and native experience.* |

---

## Part 2: MWGA's Strong Advantages (Why Choose It First)

*In WinForms migration and Web enablement, **MWGA offers the following unique advantages**:*

### 1. Very Low Migration Cost
- *Code change ≤10%; as low as **1%–2%** for complex GDI+ projects.*
- *Migration time: **hours** (import code + light environment setup).*
- *Barrier: **very low**; WinForms knowledge only, no deep Blazor/Avalonia/XAML.*

### 2. Full GDI+ Compatibility (Core Strength)
- *The **only** approach that simulates GDI+ 1:1 and maps it to Canvas.*
- *APIs such as `Graphics`, `Pen`, `Brush`, `DrawString`, `MeasureString` run with **almost no change**.*
- *Tens of thousands of lines of GDI+ drawing/reporting/visualization **reused as-is**, high-fidelity rendering.*

### 3. One Codebase, Two Targets
- *The same codebase can build to:*
  - *.exe (same deployment as today).*
  - *.wasm (runs in the browser).*
- *No need to maintain two UI layers; lowest long-term maintenance cost.*

### 4. True Cross-Platform Web
- *Static front-end deployment; Nginx, Apache, OSS, GitHub Pages, etc.*
- *Runs in browsers on **Windows, Linux, domestic/legacy OS, and mobile**.*
- *No runtime install; open the browser and run.*

### 5. Best Fit for Domestic/Legacy System Support
- *Runs in the browser on domestic OS (e.g. UOS, Kylin).*
- *No dependency on WebView2, .NET runtime, or other platform-specific components.*
- *Simplest deployment: static files only.*

---

## Part 3: Comparison by Key Dimensions

| Dimension | MWGA | Blazor Hybrid | Avalonia |
|:---------|:-----|:--------------|:---------|
| *Role* | *Migration tool / compatibility layer* | *Hybrid UI (native + Web)* | *Standalone cross-platform native UI* |
| *Stack continuity* | *Full WinForms stack* | *Dual stack* | *Full switch to Avalonia* |
| *Code churn* | *≤10%; 1%–2% for complex* | *30%–50%* | *100%* |
| *GDI+ support* | *Full sim, 1:1 to Canvas* | *Rewrite or no cross-platform* | *Full rewrite with SkiaSharp* |
| *Runtime* | *Blazor WASM in browser* | *Native + WebView2* | *Native cross-platform app* |
| *Cross-platform* | *All platforms via browser* | *Windows only* | *Native + WASM* |
| *Migration speed* | *Hours* | *Days–weeks* | *Weeks–months* |
| *Learning curve* | *Very low* | *Medium–high* | *High* |
| *Local system access* | *Web sandbox* | *Full Windows* | *Full native per platform* |
| *License* | *Commercial core; samples open* | *Microsoft, free/open* | *Open source (MIT)* |

---

## Part 4: Core Differences in Depth

### 1. Migration Approach and Code Reuse

- *Compatibility-layer simulation, "zero switch" of code. WinForms runtime is fully simulated in Blazor WASM (message loop, System.Windows.Forms APIs, GDI+→Canvas); existing Forms, controls, Paint, and drawing logic are reused with minimal changes (e.g. async dialogs, system access). One codebase builds to both WinForms exe and Blazor WASM.*

- *Native + Web bridge; two stacks long-term. No WinForms simulation; Blazor UI is hosted in WinForms via `BlazorWebView`. Old parts stay WinForms, new parts Blazor; two UI layers maintained separately — "incremental upgrade".*

- *Framework replacement; full UI rewrite. WinForms controls, layout, and rendering are dropped; UI is rewritten in Avalonia XAML and Skia. Only business logic and data code can be reused — "new tech stack", not "compatible migration".*

### 2. GDI+ Handling (Main Pain Point)

- *Full GDI+ support is MWGA's core strength. A `System.Drawing` compatibility layer maps Graphics, Pen, Brush, DrawString, MeasureString, etc. 1:1 to HTML5 Canvas; large GDI+ codebases run in the browser with high fidelity.*

- *GDI+ is split: native WinForms can keep it, but Blazor in WebView cannot reuse it; cross-UI drawing would require rewriting GDI+ to Canvas/JS; keeping only native GDI+ loses cross-platform.*

- *GDI+ is replaced entirely; no compatibility layer. SkiaSharp replaces GDI+; all drawing code must be **fully rewritten**, with similar API style but different rendering.*

### 3. Cross-Platform and Deployment

- *Static front-end deployment; no platform-specific dependency. Output is a Blazor WASM bundle (wasm + html + css + js), deployable anywhere; runs in browsers on Windows, Linux, domestic OS (e.g. UOS, Kylin), and mobile.*

- *Windows-only native deployment; requires WebView2. Does not run on Linux/macOS; deployment is the same as classic WinForms (exe/portable).*

- *Multiple deployment forms: native and WASM; per-platform installers or static bundles; unified cross-platform experience after a full rewrite.*

---

## Part 5: Selection by Scenario

### ⭐ Prefer MWGA

*When it fits:*
- *Large existing WinForms codebase with substantial GDI+ drawing/reports/visualization.*
- *No long-term rewrite plan; want quick results.*

*Key needs:*
- *Fast Web enablement and support for domestic/legacy systems.*
- *Lowest cost and shortest timeline.*
- *Keep current dev model; one codebase, two targets.*

*Trade-off: Accept Web sandbox limits and no strong requirement for UI modernization.*

---

### Prefer Blazor Hybrid

*When it fits: App must stay on Windows long-term; core features depend on native Windows (registry, hardware, drivers).*

*Key needs: No cross-platform requirement; modernize some modules' UI while keeping native capabilities.*

*Trade-off: Willing to maintain WinForms + Blazor; team has Blazor/HTML/CSS skills.*

---

### Prefer Avalonia

*When it fits: Long-term product roadmap; clear need for Windows/macOS/Linux; high bar for UI, UX, and performance.*

*Key needs: Remove Windows-only dependencies; unified cross-platform native product; sustainable modern .NET stack.*

*Trade-off: Ready to invest in full UI rewrite and team learning Avalonia/XAML.*

---

## Part 6: Summary

| Solution | One-Line Positioning |
|:-----|:-----------|
| **MWGA** | *Best option for fast Web/domestic-system enablement of existing WinForms apps — minimal change, full GDI+ compatibility, one codebase for desktop and Web; "keep alive" and quick cross-platform.* |
| **Blazor Hybrid** | *Incremental upgrade on Windows — keep native + modern UI via Web; fits Windows-only deployment and partial modernization.* |
| **Avalonia** | *Preferred framework for long-term refresh and cross-platform product — native experience, modern UI, long-term flexibility; for product-focused, unified cross-platform stack.* |

---

> *Quick decision guide:*
> - *Minimal cost to run WinForms in browser / domestic systems → **MWGA***
> - *Add modern Web UI to WinForms on Windows → Blazor Hybrid*
> - *Full rewrite for cross-platform native product → Avalonia*

---

*This document is for WinForms migration and Web enablement selection; based on public technical information, for discussion and decision reference.*

---

---


# MWGA 与 Blazor Hybrid、Avalonia 对比

> 本文清晰区分三者的定位、核心差异与适用场景，助力 WinForms 项目迁移与技术升级选型。
>
> **若追求最小改造成本、GDI+ 绘图UI保留、快速 Web 化与信创适配、降低迁移风险，MWGA 是当前最优解。**

---

## 一、三者核心定位（一句话总结）

| 方案 | 定位 |
|:-----|:-----|
| **MWGA（Make WinForms Great Again）** | WinForms → Blazor WASM 专用迁移工具，核心是「**最小改造成本、高保真还原、一套代码双端运行**」，主打存量 WinForms 项目快速 Web 化，几乎不重写 UI 与 GDI+ 绘图代码。 |
| **Blazor Hybrid** | 混合 UI 架构方案，通过 WebView2 将 Blazor 嵌入 WinForms/WPF/MAUI 原生容器，核心是「保留原生系统能力，复用 Web 生态做现代 UI」，适合给 WinForms 渐进式新增现代 Web 界面。 |
| **Avalonia** | 跨平台原生 UI 框架，对标 WPF、基于 Skia 自渲染，核心是「彻底脱离 WinForms，用现代跨平台原生栈重写/重构」，适合长期技术换代与全平台原生体验打造。 |

---

## 二、MWGA 的绝对优势（迁移场景首选理由）

在 WinForms 迁移与 Web 化场景下，**MWGA 具有以下不可替代的优势**：

### 1. 改造成本极低
- **代码修改量 ≤10%**，复杂 GDI+ 项目低至 **1%–2%**
- 迁移效率：**数小时**（代码导入 + 少量环境适配）
- 技术门槛：**极低**，仅需 WinForms 开发知识，无需深入学习 Blazor/Avalonia/XAML

### 2. GDI+ 完整兼容（核心竞争力）
- **唯一**能对 GDI+ 实现 1:1 模拟并映射至 Canvas 的方案
- `Graphics`、`Pen`、`Brush`、`DrawString`、`MeasureString` 等 API 几乎**零修改**即可运行
- 数万行 GDI+ 自定义绘图、报表、可视化代码**直接复用**，高保真渲染

### 3. 一套代码，双端运行
- 同一套代码可编译为：
  - **.exe**（延续现有部署方式）
  - **.wasm**（浏览器直接运行）
- 无需维护两套 UI 层，长期维护成本最低

### 4. 真正的跨平台 Web 化
- 纯前端静态部署，支持 Nginx、Apache、OSS、GitHub Pages 等
- 覆盖 **Windows / Linux / 国产信创系统 / 移动端** 的各类浏览器
- **无需安装运行时**，打开浏览器即可使用

### 5. 信创适配场景最优解
- 国产操作系统（统信、麒麟等）浏览器即可运行
- 无需依赖 WebView2、.NET 运行时等平台特定组件
- 部署形态最简单：静态文件即可

---

## 三、关键维度完整对比表

| 对比维度 | MWGA | Blazor Hybrid | Avalonia |
|:---------|:-----|:--------------|:---------|
| **本质角色** | WinForms 迁移专用工具/兼容层 | 混合 UI 架构（原生 + Web） | 独立跨平台原生 UI 框架 |
| **技术栈延续性** | ✅ 完全延续 WinForms 栈 | 双栈并存（WinForms + Blazor） | 彻底切换至 Avalonia |
| **代码修改量** | ✅ **≤10%，复杂项目 1%–2%** | 30%–50% | 100% |
| **GDI+ 支持** | ✅ **完整模拟，1:1 映射至 Canvas** | 需重写或跨平台失效 | 用 SkiaSharp 全重写 |
| **运行形态** | 纯前端 Blazor WASM，浏览器运行 | 原生桌面进程内嵌 WebView2 | 原生跨平台应用 |
| **跨平台能力** | ✅ **全平台浏览器（含信创/移动）** | 仅 Windows | 全平台原生 + WASM |
| **迁移效率** | ✅ **数小时** | 天–周级 | 周–月级 |
| **技术门槛** | ✅ **极低** | 中高 | 高 |
| **本地系统权限** | Web 沙盒限制 | 完整 Windows 原生权限 | 各平台完整原生权限 |
| **商业/开源** | 核心运行时商业授权，示例/模板开源 | 微软官方免费开源 | 纯开源（MIT） |

---

## 四、核心差异深度解析

### 1. 迁移思路与代码复用的本质区别

- **MWGA**：**兼容层模拟实现，代码「零切换」**。在 Blazor WASM 环境中完整模拟 WinForms 运行时（消息循环、System.Windows.Forms 全套 API、GDI+ → Canvas 映射），原有 Form、控件、Paint 事件、绘图逻辑几乎**原封不动复用**，仅需做异步对话框、系统权限相关的少量适配。真正实现「一套代码同时编译为 WinForms exe 和 Blazor WASM 前端包」。

- **Blazor Hybrid**：原生 + Web 混合桥接，双栈长期并存。不模拟 WinForms 运行时，通过 `BlazorWebView` 将 Blazor 界面嵌入 WinForms 窗口，老功能保留 WinForms，新功能用 Blazor 开发，UI 层分为原生和 Web 两块独立维护，属于「局部改造、渐进升级」思路。

- **Avalonia**：框架级替换，UI 全量重写。WinForms 的控件、布局与渲染全部放弃，用 Avalonia XAML + Skia 重写界面，仅业务逻辑与数据层可复用，属于「新技术栈」而非「兼容迁移」。

### 2. GDI+ 绘图处理差异（WinForms 迁移最大痛点）

- **MWGA**：**对 GDI+ 支持最完整**，是该方案核心优势。内置 `System.Drawing` 兼容层，`Graphics`、`Pen`、`Brush`、`DrawString`、`MeasureString` 等核心绘图 API **1:1 映射**为 HTML5 Canvas 渲染指令，数万行 GDI+ 自定义绘图、报表、可视化代码无需修改，直接在浏览器中高保真渲染。

- **Blazor Hybrid**：GDI+ 处理割裂。WinForms 原生端可保留 GDI+，但 WebView 中的 Blazor 界面无法复用；若要跨界面绘图，需将 GDI+ 重写为 Canvas/JS；若仅保留原生 GDI+，则失去跨平台意义。

- **Avalonia**：彻底替换 GDI+，无兼容层。用 SkiaSharp 替代 GDI+，原有绘图代码需**完全重写**，API 语法有相似性，但需适配 SkiaSharp 的渲染逻辑。

### 3. 跨平台与部署形态差异

- **MWGA**：**纯前端静态部署，跨平台无依赖**。最终产物是 Blazor WASM 静态资源包（wasm + html + css + js），可部署到任意静态服务，也可通过浏览器打开本地文件运行，支持 Windows、Linux、统信/麒麟等国产系统、Android/iOS 的各类浏览器。

- **Blazor Hybrid**：仅限 Windows 原生部署，依赖 WebView2 运行时，无法在 Linux、macOS 等非 Windows 平台运行，部署形态与传统 WinForms 一致（exe 安装包/绿色包）。

- **Avalonia**：多形态部署，支持原生编译和 WASM 编译，可生成各平台专属安装包或前端静态包，跨平台体验统一，但需全量重构才能实现。

---

## 五、选型建议（按项目场景直接对号入座）

### ⭐ 优先选 MWGA

**适用特征：**
- 存量 WinForms 项目代码量大，包含大量 GDI+ 自定义绘图/报表/可视化代码
- 无长期技术重构计划，希望快速见效

**核心需求：**
- 快速实现 Web 化、适配信创国产系统
- 改造成本最低、工期最短
- 保留原有开发模式，一套代码双端运行

**约束条件：** 可接受 Web 沙盒的系统权限限制，对 UI 现代美化无强需求。

---

### 优先选 Blazor Hybrid

**适用特征：** WinForms 项目仍需长期运行在 Windows 平台，核心功能依赖 Windows 原生系统权限（注册表、本地硬件、专属驱动）。

**核心需求：** 无需跨平台，仅需对部分模块做现代 UI 升级，保留原生系统能力。

**约束条件：** 可接受 WinForms + Blazor 双技术栈维护，团队具备 Blazor/HTML/CSS 基础。

---

### 优先选 Avalonia

**适用特征：** 项目处于产品化长期迭代阶段，有明确的跨 Windows/macOS/Linux 平台需求，对 UI 现代性、交互体验、运行性能要求高。

**核心需求：** 彻底脱离 Windows 专属依赖，打造统一的跨平台原生产品体验，建立长期可维护的现代 .NET 技术栈。

**约束条件：** 愿意投入工期和人力进行 UI 层全量重构，团队可学习 Avalonia/XAML 技术。

---

## 六、总结

| 方案 | 一句话定位 |
|:-----|:-----------|
| **MWGA** | **WinForms 存量项目快速 Web 化/信创化的最优解** —— 最小改造成本、GDI+ 完美兼容、一套代码双端运行，适合项目「续命」、快速落地跨平台需求。 |
| **Blazor Hybrid** | Windows 平台 WinForms 项目渐进升级的折中方案 —— 保留原生系统能力 + 复用 Web 生态做现代 UI，适合仅需 Windows 部署、局部改造。 |
| **Avalonia** | WinForms 项目长期技术换代、产品化跨平台的首选框架 —— 全平台原生体验、现代 UI 能力、长期技术自由，适合追求产品化、建立统一跨平台技术栈。 |

---

> **一句话核心区分：**
> - 想**最小成本**把 WinForms 搬上浏览器/信创系统 → **选 MWGA**
> - 想在 Windows 上给 WinForms 加现代 Web 界面 → 选 Blazor Hybrid
> - 想彻底重构做跨平台原生产品 → 选 Avalonia

---

*本文面向 WinForms 迁移与 Web 化选型场景，基于公开技术资料整理，供技术论坛讨论与决策参考。*
