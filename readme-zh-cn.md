# MWGA - 为了复活1000亿行C#代码[简体中文版](https://github.com/dcsoft-yyf/MWGA/blob/main/readme-zh-cn.md)

**南京都昌信息科技有限公司**  
**2026-1-28**

<img src="https://github.com/dcsoft-yyf/MWGA/blob/main/MWGA.jpg?raw=true"/>

---
## 在线演示程序
- [https://dcsoft-yyf.github.io/MWGAo](https://dcsoft-yyf.github.io/MWGA)
## 更新日志
- 2026-1-28 : Upload demo Time line.
- 2026-1-19 : Update demo calculator.
- 2026-1-19 : Update demo Mimesweeper at 
---

## 1. 一句话介绍

MWGA是Make WinForms Great Again的缩写，它是一个工具软件，能快速地将使用了GDI+的WinForm.NET程序快速迁移到Blazor WASM平台上，将程序代码修改量控制在10%以下，从而复活全球1000亿行C#代码。

## 2. 项目背景

据估计，全球范围内企业级生产环境中运行着1000万至1500万个WinForms应用程序。在这些应用中，60%至80%有现代化改造需求，其中40%至60%优先选择Web化迁移，涉及的C#代码可能有数千亿行。由于可复用C#代码且具备基于浏览器的跨平台能力，Blazor WebAssembly成为热门选择。

但是有大量的WinForms使用了System.Drawing模块调用GDI+进行复杂的自定义绘图和交互，这些部分难以迁移，通常需要重写或大幅修改。为此，市场上对低改动、可复用业务逻辑和绘图代码的现代化迁移解决方案需求强烈。但长期以来一直缺乏有效工具和方法，导致许多企业面临高昂的重写成本和风险，存在巨大供需矛盾。

## 3. 我们的目标

MWGA就是专门帮助将WinForms应用程序迁移到Blazor WASM平台上，即使这些程序使用GDI+功能，我们也预期将对这些程序源码的修改量不超过10%。这极大的降低WinForms软件现代化的成本和风险。

我们的长期目标是复活全球1000亿行经过市场验证的C#代码，使其在现代Web前端平台上继续发挥价值。MWGA帮助开发者将一套C#代码同时编译成.exe和.wasm文件，两者运行效果保持高度一致。

从另外一个角度看，MWGA可以看成一个通用前端框架，但是采用了特有的WinForms编程模型，是WinForms技术栈在前端领域中的一个海外殖民地，这是一个重大的跨界融合，使得全球数百万个WinForms开发者无需更换技术栈即可在纯前端领域发挥作用。而且C#的强类型语言特性和GDI+严谨的编程模型也有助于减少AI编程产生的隐形BUG。

**软件下载地址**：[https://github.com/dcsoft-yyf/MWGA](https://github.com/dcsoft-yyf/MWGA)

## 4. 使用案例一：时间轴产品，1%代码修改量

时间轴产品是南京都昌公司的一个WinForms软件产品，现已开源，这是一个面向医院的专业软件产品，可以认为是体温单软件的增强版，它包含了7万行C#代码，其中有数万行GDI+绘图相关代码，其运行界面如下图所示：

![时间轴产品WinForms版本](https://www.dcwriter.cn/image/timeline1.png)

我们创建了一个Blazor WASM 9.0的程序，把时间轴的代码复制过来，并做一些改造，代码修改量不超过700行，也就是小于1%，比如：

```csharp
if (e.ClickedItem.Text == "打开本地时间轴文档")
{
    using (OpenFileDialog ofd = new OpenFileDialog())
    {
        if (
#if MWGA
            await
#endif
            ofd.ShowDialog() == DialogResult.OK)
        {
            var stream =
#if MWGA
                await
#endif
                ofd.OpenFile();
            var reader = new StreamReader(stream, Encoding.UTF8, true);
            var strXml = reader.ReadToEnd();
            temperatureControl1.LoadDocumentFormString(strXml);
            reader.Close();
        }
    }
}
```

由于Blazor WASM是采用浏览器非阻断线程模式，为此我们实现了异步`ShowDialog()`函数，采用`await`语句来暂停当前代码执行，这样减少对旧代码的修改量。并使用了条件编译，使得同一份C#代码无需修改即可编译成.exe和.wasm文件。最后编译成.wasm的程序在谷歌浏览器中运行效果如下图所示：

![时间轴产品Blazor WASM版本](https://www.dcwriter.cn/image/timeline2.png)

程序中数万行内容排版和绘图代码未做修改，比如：

```csharp
this.Document.InnerBehaviorMode = this.BehaviorMode;
RectangleF clipRectangle = (RectangleF)e.ClipRectangle;
clipRectangle = this.ViewTransform.TransformRectangleF(clipRectangle);
e.Graphics.PageUnit = this.Document.GraphicsUnit;
PointF lp = this.ClientToView(0, 0);
e.Graphics.TranslateTransform(-lp.X, -lp.Y);
this.Document.ViewMode = this.ViewMode;
if (this.ViewMode == DocumentViewMode.Page)
{
    // 页面视图模式
    // 绘制页面边框
    clipRectangle.Inflate(1, 1);
    if (this.Document.Config.BackColor.A != 0)
    {
        // 填充背景色
        e.Graphics.FillRectangle(
            GraphicsObjectBuffer.GetSolidBrush(this.Document.Config.BackColor),
            clipRectangle);
    }
    RectangleF pb2 = RectangleF.Intersect(this._PageViewBounds, clipRectangle);
    if (pb2.IsEmpty)
    {
        return;
    }
```

这个使用案例展示了MWGA处理复杂图形软件的能力，使得它距离复活1000亿行C#代码的最终目标又进了一大步。

## 5. 使用案例二：扫雷游戏程序，2%代码修改量

扫雷是一个经典的Windows游戏程序，我们从[https://gitee.com/dingxiaowei/MineGame](https://gitee.com/dingxiaowei/MineGame)下载了一个基于MS .NET Framework 2.0的扫雷程序，这是一个10年前写的Winforms程序，包含约2500行C#代码以及若干图片资源文件，编译成.exe文件后运行如下图所示：

![扫雷游戏WinForms版本](https://github.com/dcsoft-yyf/MWGA/blob/main/images/winform-mine.png?raw=true)

这个程序大量使用`System.Drawing.Graphics.DrawLine()`/`DrawImage()`/`FillRectangle()`的接口来绘制游戏界面。此外这个程序使用了Panel、IMessageFilter、Timer、Button、MainMenu、MessageBox、ImageList等组件。

我们创建了一个Blazor WASM 9.0的程序，将扫雷程序源码文件复制过来，并做一些兼容性修改，如下所示：

```csharp
#if MWGA
    public static async ValueTask<ShowSelfResult> ShowSelf(...)
#else
    public static ShowSelfResult ShowSelf(...)
#endif
{
    bool result;
    frmCustomGame cg = new frmCustomGame();
    cg.tbHeight.Text = height.ToString();
    cg.tbWidth.Text = width.ToString();
    cg.tbMineCount.Text = mineCount.ToString();
    cg.Location = location;
#if MWGA
    if (await cg.ShowDialog(parent) == DialogResult.OK)
#else
    if (cg.ShowDialog(parent) == DialogResult.OK)
#endif
    {
        // ...
    }
}
```

最终我们对旧代码修改了不超过50行（占比2%）就让同一套代码可以无需修改即可编译成.exe和.wasm文件。最后编译成.wasm的扫雷程序在谷歌浏览器中的运行结果如下：

![扫雷游戏Blazor WASM版本](https://github.com/dcsoft-yyf/MWGA/blob/main/images/minesweeper.png?raw=true)

程序中的上千行图形绘制代码和游戏逻辑判断未做任何修改，如下所示：

```csharp
protected override void OnPaint(PaintEventArgs e)
{
    Rectangle rect = ClientRectangle;
    Graphics g = e.Graphics;

    g.FillRectangle(grayBrush, rect);
    drawFrame(g, new Rectangle(rect.Left, rect.Top, rect.Width - 1, rect.Height - 1));
    if (Image != null)
    {
        int offset;
        if (pressed)
            offset = 1;
        else
            offset = 0;
        g.DrawImage(Image, rect.Left + 4 + offset, rect.Top + 4 + offset);
    }
}
```

## 6. 使用案例三：计算器，无代码修改

我们开发了一个Winform.NET的计算器程序，包含460行C#代码，其运行界面如图所示：

![计算器WinForms版本](https://github.com/dcsoft-yyf/MWGA/blob/main/images/winform-calculator.png?raw=true)

这里响应了窗体的大小改变事件，用于设置按钮和文本框的位置和大小，其代码如下：

```csharp
private void CalculatorForm_Resize(object sender, EventArgs e)
{
    UpdateControlLayout();
}

/// <summary>
/// 动态更新所有控件的位置和大小（完全填充窗体，无空白）
/// </summary>
private void UpdateControlLayout()
{
    // 获取窗体客户端区域（排除边框）
    Rectangle clientRect = this.ClientRectangle;

    // 1. 处理显示屏（占顶部整行，高度占客户端区域的1/6，剩余部分给按钮）
    int displayHeight = clientRect.Height / 6;
    // 显示屏位置：左、上、右间距为fixedPadding，高度为displayHeight
    txtDisplay.Location = new Point(_fixedPadding, _fixedPadding);
    var newSize = new Size(clientRect.Width - 2 * _fixedPadding, displayHeight - 2 * _fixedPadding);
    // ...
}
```

这份C#代码未做任何修改，借助MWGA，它在Blazor WASM中运行界面如下所示：

![计算器Blazor WASM版本](https://github.com/dcsoft-yyf/MWGA/blob/main/images/calculator.png?raw=true)

## 7. 基本原理

MWGA基本原理是模拟`System.Windows.Forms.Control`类型和`System.Drawing.Graphics`类型来实现WinForms代码低修改量的迁移。MWGA建立了以下的功能模块映射：

| HTML功能模块 | MWGA功能模块 |
|------------|------------|
| `<canvas>` | `System.Drawing.Graphics` |
| `<button>` | `System.Windows.Forms.Button` |
| `<img>` | `System.Windows.Forms.PictureBox` |
| `<div>` | `System.Windows.Forms.Form`<br>`System.Windows.Forms.Panel`<br>`System.Windows.Forms.Control`<br>`System.Windows.Forms.Label` |
| `<nav>` | `System.Windows.Forms.MainMenu` |
| `<input type="text">`或`<textarea>` | `System.Windows.Forms.TextBox` |
| `window.alert()` | `System.Windows.Forms.MessageBox` |
| `<div>` | `System.Windows.Forms.MessageBoxNew` |
| `element.style.cursor` | `System.Windows.Forms.Cursor` |
| `window.setTimeout()` | `System.Windows.Forms.Timer` |
| `MouseEvent`, `KeyEvent` | Win32 Message，包括`WM_KEYUP`、`WM_KEYDOWN`、`WM_LBUTTONDOWN`、`WM_LBUTTONUP`等等 |

MWGA内部还模拟实现了Win32 Message loop和消息队列，构造出了一个Winforms的底层运行框架，使得用户的基于Winforms的C#代码重新编译后即可运行在Blazor WASM上。

## 8. MWGA支持的关键功能点 (2026-1-28)

- **System.Drawing.Bitmap**
- **System.Drawing.Brush**
- **System.Drawing.Font**
- **System.Drawing.FontFamily**
- **System.Drawing.Graphics**  
  `DrawString()`, `DrawImage()`, `DrawLine()`, `DrawRectangle()`, `FillRectangle()`, `DrawEllipse()`, `FillEllipse()`, `MeasureString()`, `PageUnit`, `Transform`
- **System.Drawing.Pen**
- **System.Drawing.SolidBrush**
- **System.Drawing.Drawing2D.HatchBrush**
- **System.Drawing.Drawing2D.LinearGradientBrush**
- **System.Drawing.Drawing2D.Matrix**
- **System.Windows.Forms.Application**  
  `AddMessageFilter()`, `Run()`, `RemoveMessageFilter()`
- **System.Windows.Forms.Button**
- **System.Windows.Forms.ContainerControl**
- **System.Windows.Forms.Control**  
  `BackColor`, `ForeColor`, `Width`, `Height`, `Location`, `Size`, `Anchor`, `Dock`, `Visible`, `Enabled`, `Text`, `Font`, `Invalidate()`, `Refresh()`
- **System.Windows.Forms.Cursor**
- **System.Windows.Forms.Form**  
  `async ShowDialog()`, `Show()`, `Close()`, `FormBorderStyle`, `StartPosition`, `WindowState`, `Resize`  
  Load data from `Form.resx`  
  Support `Form.Designer.cs`
- **System.Windows.Forms.ImageList**  
  `Add()`, `Draw()`
- **System.Windows.Forms.MainMenu**
- **System.Windows.Forms.Label**
- **System.Windows.Forms.MessageBox**  
  `Show()`, `async ShowAsync()`
- **System.Windows.Forms.OpenFileDialog**  
  `async ShowDialog()`, `async OpenFile()`
- **System.Windows.Forms.Panel**
- **System.Windows.Forms.PictureBox**
- **System.Windows.Forms.Screen**
- **System.Windows.Forms.ScrollableControl**
- **System.Windows.Forms.SystemInformation**（采用 Win11 的设置）
- **System.Windows.Forms.TextBox**
- **System.Windows.Forms.Timer**
- **System.Windows.Forms.ToolStrip**
- **System.Windows.Forms.UserControl**
- **System.Resources.ResourceManager**
- **System.ComponentModel.ComponentResourceManager**
- **Platform**  
  Development: Blazor WebAssembly 9.0/10  
  Browser: Chrome (v95 or later), Firefox (v113 or later) and other mainstream browsers  
  OS: Windows (7 or later), Linux, Android

## 9. 多语言支持

MWGA支持多语言开发。MWGA内部所有的字符串都剥离出来形成一个字符串资源JS文件，其内容如图所示：

```javascript
window.__DCResourceStrings = {
    AboutBoxDesc: "显示该组件的"关于"对话框",
    AccDGCollapse: "折叠",
    AccDGEdit: "编辑",
    AccDGExpand: "展开",
    AccDGNavigate: "定位",
    AccDGNavigateBack: "向后定位",
    AccDGNewRow: "(新建)",
    AccDGParentRow: "父行",
    AccDGParentRows: "父行",
    AccessibleActionCheck: "选中",
    AccessibleActionClick: "单击",
    AccessibleActionCollapse: "折叠",
    AccessibleActionExpand: "展开",
    AccessibleActionPress: "按",
    AccessibleActionUncheck: "取消选中",
    // ...
};
```

我们目前提供简体中文版和英文版，用户可以修改这个JS文件来使用自己的语言。

另外MWGA支持`ComponentResourceManager`类型，如图所示：

```csharp
private void InitializeComponent()
{
    var resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgMessage));
    this.panel1 = new System.Windows.Forms.Panel();
    this.pictureBox1 = new System.Windows.Forms.PictureBox();
    this.label1 = new System.Windows.Forms.Label();
    this.txtMessage = new System.Windows.Forms.TextBox();
    this.btnOK = new System.Windows.Forms.Button();
    this.btnCancel = new System.Windows.Forms.Button();
    this.panel1.SuspendLayout();
    ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
    this.SuspendLayout();
    // ...
}
```

用户可以将程序资源设置到`Form.resx`文件中，编译后即可使用窗体资源文件。

## 10. 开发和部署

MWGA只包含一个4MB大小的`DCSoft.MWGA.dll`文件，就已经包含了所有的功能，不依赖任何其他第三方组件。

开发者可参考[https://github.com/dcsoft-yyf/MWGA](https://github.com/dcsoft-yyf/MWGA)提供的演示程序来进行基于MWGA的开发。主要步骤是：

### 第一，创建Blazor WASM项目

创建一个Blazor WASM 9.0/10.0的程序，添加对`DCSoft.MWGA.dll`的程序集引用。**目前不支持Blazor WASM 7.0/8.0**。

### 第二，复制源代码

将要迁移的WinForms程序的源代码以及资源文件全部复制到项目中。

### 第三，添加引导程序

添加标准化的MWGA的引导程序代码，添加运行程序界面的HTML文件。

### 第四，兼容性修改

对用户程序代码进行必要的兼容性检查和修改，主要修改点有：

| 标准WinForms功能点 | MWGA修改说明 |
|------------------|------------|
| 显示对话框可直接使用`Form.ShowDialog()` | 要将用户函数改造成异步模式，然后使用`await Form.ShowDialog()` |
| 支持P/Invoke，可以使用`[DllImport]`来导入外界API函数 | 不支持P/Invoke，必须修改用户代码来删除P/Invoke功能。不过如果P/Invoke功能没被触发，程序仍然能正常运行。 |
| 窗体资源文件`.resx` | 由于源代码文件目录结构改变导致生成的`.resx`文件重命名，需要调整用户代码来设置正确的名称。<br>MWGA自己提供的`ComponentResourceManager`、`ResourceManager`已经能自动处理这种`.resx`的重命名。 |
| 可直接重写`Control.OnPaint`方法 | 由于启用Graphics子模块需要消耗点资源，所以默认情况下MWGA中所有的WinForms控件没启用自定义绘图操作，都是用HTML DOM来模拟WinForms控件的界面和行为。<br>只有确认使用自定义绘图操作，才需要在控件的构造函数中调用`SetStyle(ControlStyles.UserPaint, true)`来启用Graphics子模块。 |
| `MessageBox.Show()` | 可以继续使用`MessageBox.Show()`，但是底层调用了`window.Alert()`来模拟的，用户界面很丑。<br>如果需要高仿请使用`await MessageBox.ShowAsync()`，需要将用户函数改造成异步模式。 |
| `OpenFileDialog.ShowDialog()` | `await OpenFileDialog.ShowDialog()` |
| `OpenFileDialog.OpenFile()` | `await OpenFileDialog.OpenFile()` |
| 本地文件访问 | MWGA不支持`new FileStream(fileName)`模式打开本地文件，只能调用`OpenFileDialog.OpenFile()`来只读打开本地文件。 |
| `Button`, `MenuItem`, `ToolStripItem.Click`事件 | 可以继续使用，但提供新的事件`ClickAsync`可以使用，新的事件以异步方式运行，可以安全使用`await`。 |
| 窗体设计器 | MWGA是轻量级的前端框架，不支持可视化的窗体设计器。开发者需要使用传统的窗体设计器来设计窗体，比如使用VS.NET或SharpDevelop等工具。 |
| 单步及断点调试 | MWGA借助Blazor WASM可以实现单步及断点调试，但使用效果没有传统的WinForms开发工具好，建议开发者使用传统WinForms开发调试工具把软件流程彻底做好，然后移植到MWGA。 |
| 内存和CPU的性能调优 | MWGA没有性能调优工具。建议开发者在传统的WinForms开发工具中把性能调优做好，然后移植到MWGA中。 |
| 客户端大内存 | MWGA运行在浏览器沙盒中，留给用户的内存不多，建议不要超过1GB，这就要求开发者需要使用传统WinForms开发工具来非常仔细的优化内存占用，不要浪费每个字节的内存。 |

### 第五，编译运行

编译运行程序。

MWGA支持谷歌（v95及后续版本）、火狐（v133及后续版本）等主流浏览器。支持Windows、Linux、Android操作系统。**不支持Windows XP等老旧操作系统**。

发布到生产环境时，可以考虑使用[https://github.com/dcsoft-yyf/BlazorWASMPackager](https://github.com/dcsoft-yyf/BlazorWASMPackager)将Blazor WASM程序文件集合打包成一个单独的JS文件，方便部署和维护。

## 11. 安全性说明

MWGA并不是开源软件，但我们采取以下措施来保证这个软件是安全的：

1. **限制使用范围**：MWGA的唯一的文件`DCSoft.MWGA.dll`限制为只能用于Blazor WASM开发。对于其他的软件类型，比如WinForms、ASP.NET CORE、命令行等等不会产生任何效果。

2. **纯前端组件**：MWGA是一个纯前端的软件组件，没有服务器端程序，只能运行在浏览器沙盒中，没有访问数据库、本地文件系统、注册表和硬件的权限。

3. **无网络操作**：MWGA承诺不会执行任何网络操作，包括http、ftp、Web Socket等等。而且用户监控浏览器的异常网络行为也是很容易的事情。

4. **打包建议**：我们建议用户使用[https://github.com/dcsoft-yyf/BlazorWASMPackager](https://github.com/dcsoft-yyf/BlazorWASMPackager)将Blazor WASM软件打包成一个单独的JS文件。可以减少下载程序文件的网络操作，甚至使用本地`file://`协议运行。进一步的减少网络安全风险。

5. **无本地数据访问**：MWGA承诺不访问任何本地数据，包括访问浏览器cookies、localStorage、IndexDB、navigator对象等等。

6. **无高权限操作**：MWGA承诺不会执行高权限有风险的操作。比如操作摄像头、位置信息获取等等。

7. **用户代码安全管控**：对于用户提供的WinForms应用程序发出访问文件或者数据库连接的请求，MWGA都会触发JS事件，让开发者自己写代码响应事件来处理这种高权限的行为。对于相关的安全风险MWGA完全避嫌。未来MWGA会提供高频变化的安全Token机制，强化Winforms用户代码安全行为的管控。

8. **错误隔离**：当MWGA或用户代码由于BUG导致程序错误和卡死，由于它是纯前端组件，只能影响到当前终端，重启客户端浏览器即可恢复，不会影响服务器，安全风险小。

9. **信创认证**：MWGA的姐妹软件DCWriter5采用了相同的软件架构。DCWriter5已经拿到统信、麒麟、方德操作系统原厂适配认证。这间接说明MWGA符合国产信创的要求。

10. **安全提醒**：当MWGA出现了本文档说明之外的高权限行为，可以怀疑`DCSoft.MWGA.dll`不是正版的，或者遭到病毒和木马的侵犯。即使如此，由于浏览器安全沙盒的限制，用户的运行环境仍然是安全的。

## 12. 同类方案对比

将WinForms程序迁移到Blazor WASM上，目前业界还有以下解决方案：

- **代码生成式迁移**：使用工具软件解析WinForms的源代码，自动生成Blazor代码，后续独立维护。
- **手动重构**：参考原先的WinForms程序，从零开始手写Blazor组件，完全脱离原先的WinForms程序。
- **混合桥接（WebView2）**：在WinForms程序中嵌入一个WebView2的浏览器组件，将软件功能一点点的迁移到BS结构中。

MWGA和其他相同目标的解决方案的对比如下：

| 对比维度 | MWGA | 代码生成式迁移 | 手动重构 | 混合桥接 |
|---------|------|--------------|---------|---------|
| **迁移效率** | 极致快速（数小时）：导入项目即可渲染，很少的编码适配，迁移效率远超其他方案。 | 较快（数天） | 极慢（周/月级） | 中等（天/周级） |
| **技术门槛** | 极低（零Blazor/前端基础要求）：仅需熟悉原有WinForms项目，普通开发即可快速上手，大幅降低迁移学习成本； | 中（需基础Blazor知识，需学习新的Blazor技术栈） | 高（精通Blazor生态，需全面掌握Blazor+前端技术栈） | 中高（需双栈知识，需同时掌握WinForms与前后端技术栈） |
| **代码修改量** | 普通项目代码零修改。<br>复杂项目和GDI+项目代码修改量小于10% | 中：生成代码后需5%-20%的适配修改（如控件事件绑定、样式调整）；GDI+功能需大幅改写（30%-60%）；需适配Blazor技术栈规范 | 100%：需全盘重写UI代码，仅可复用少量纯业务逻辑代码；GDI+绘图需完全基于Blazor/JS重构；需彻底切换至Blazor技术栈 | 中高：需修改30%-50% WinForms代码适配桥接层，同时编写Blazor前端交互代码；GDI+需额外适配桥接渲染逻辑；需掌握双技术栈适配规则 |
| **成本优势** | 极致成本优势：很少编码人力成本、无学习成本；无需后续代码维护成本；纯前端部署无服务器运维成本；含GDI+的项目可大幅节省重写成本； | 中：需承担代码生成后适配人力成本，后续维护成本较低；GDI+适配需额外增加成本；需投入Blazor技术栈培训成本 | 极高：全量重写人力成本高，前期学习成本+后期维护成本叠加；GDI+重构成本占比超50%； | 高：双栈开发人力成本高，桥接层兼容维护成本持续存在，无跨平台成本优势；GDI+适配进一步推高成本；需投入双技术栈学习与适配成本 |
| **GDI+ 支持** | 良好支持：通过模拟System.Drawing.Graphics等核心类型实现GDI+绘图迁移，可保留原有绘图逻辑与交互效果；同一套代码在WinForms与Blazor WASM环境下界面、逻辑一致；技术栈无变动，绘图相关开发经验可直接复用 | 有限支持：仅能识别基础GDI+语法，复杂绘图逻辑（如自定义渲染、动态绘图）无法直接生成代码，需大幅改写；需基于Blazor技术栈重构绘图逻辑 | 需完全重构：无原生GDI+支持，需基于Blazor组件/HTML5 Canvas+JS重新实现所有绘图功能，学习与开发成本极高；需彻底抛弃原有GDI+开发技术栈 | 部分支持：可保留WinForms原生GDI+渲染，但需适配WebView2桥接通信逻辑，存在性能损耗，且跨平台受限；需掌握双栈下GDI+适配技术 |
| **技术栈延续性** | 完全延续：无需改变企业现有WinForms技术栈，开发人员可复用原有WinForms开发经验与技能；项目代码结构、开发规范保持不变，仅改变前端渲染形态 | 部分延续：可复用WinForms业务逻辑经验，需切换至Blazor技术栈，开发规范与代码结构需适配Blazor框架要求 | 完全切换：需彻底抛弃WinForms技术栈，全面转向Blazor+前端技术栈，原有开发经验复用率低 | 双栈并存：需同时维护WinForms与Blazor两套技术栈 |
| **适配场景** | 快速验证迁移效果；<br>对跨平台需求高、数据安全敏感、迁移周期紧张、成本控制严格的场景（优势适配）；<br>含GDI+绘图功能的WinForms项目迁移；<br>注重技术栈延续性、需复用现有开发团队能力的企业项目 | 需长期维护、逐步演进纯Blazor架构项目；无复杂GDI+功能的项目；企业可接受技术栈部分切换的项目 | 大型项目、复杂逻辑、追求极致性能/长期价值；可接受全量重构成本与技术栈完全切换的GDI+项目；企业有明确Blazor技术栈转型规划的项目 | 必须保留WinForms逻辑、仅Windows平台使用；含复杂GDI+功能但无法承担重构成本的项目；企业可接受双技术栈并存的短期过渡项目 |

## 13. 版权说明

MWGA为商业闭源产品，南京都昌信息科技有限公司拥有全部版权，严禁破解和盗版。

演示项目为开源示例，用于演示迁移流程与验证兼容性。

有任何疑问请联系：28348092@qq.com 或者在 [https://github.com/dcsoft-yyf/MWGA](https://github.com/dcsoft-yyf/MWGA) 上留言。

---

**南京都昌信息科技有限公司**  
**2026年1月28日**
