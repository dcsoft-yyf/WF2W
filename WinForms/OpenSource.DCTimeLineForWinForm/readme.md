# 时间轴产品说明

## 1. 简述

时间轴是一款医疗时序数据可视化组件，用于结构化、可视化呈现患者在住院期间的生命体征监测记录。针对国内医院信息系统（HIS）与电子病历（EMR）的实际需求，该工具主要服务于体温单、三测单等核心护理场景，提供标准化的生命体征数据时序展示能力。

---

## 2. 客户案例

### 2.1 英文版 + 脉搏短绌
![英文版+ 脉搏短绌](https://www.dcwriter.cn/image/EnglishTimeLine.png)

上图为在中国台湾地区的使用效果，支持 Y 轴点击动态展示与隐藏，支持展示脉搏短促数据等。

### 2.2 健康档案
![健康档案](https://www.dcwriter.cn/image/Health_Record.png)
上图为时间轴产品在健康档案场景下的使用效果。具有如下功能特点：

- 支持 Y 轴示例过多时的上下分离展示；
- 支持折叠刻度点的数据；
- 支持点击病历文档超链接；
- 支持直接点击插入新点；
- 支持医嘱执行流程等。

### 2.3 体温单
![体温单](https://www.dcwriter.cn/image/Temp_Sheet.png)

上图为时间轴产品在电子病历-体温单的使用效果，具有如下功能特点：

- 分页显示视图，支持上下滚动展示，能否把握打印结果；
- 支持显示疼痛强度栏和描点等。

### 2.4 围手术期数据展示
![围手术期数据展示](https://www.dcwriter.cn/image/Periop_Data_View.png)
上图呈现了时间轴产品在围手术期的使用效果，其具备展示手术前后生命体征变化情况、手术前后病历展示等业务特性。

### 2.5 手术室排程
![手术室排程](https://www.dcwriter.cn/image/OR_Scheduling.png)
上图展示了时间轴产品在手术室排程中的使用示例。此功能能够呈现某医疗机构全部手术室的使用状况，以及手术过程中人员、耗材等的使用情形，其中包括通过色块展示手术是否成功、排期手术安排、手术时长和手术病历等信息。

### 2.6 重合点
![重合点](https://www.dcwriter.cn/image/Overlap_Point.png)

上图呈现了时间轴产品在重合点时的效果，支持不同 Y 轴重合时的自定义样式。

---

## 3. 运行使用
该软件基于 WinForm 开发并使用。运行环境至少为 .NET 2.0 SP2 及以上版本，产品提供全部源代码，这些代码全部包含在 **DCTimeLineForWinForm** 项目内。

**基础类型名称：**
```csharp
private DCSoft.TemperatureChart.TemperatureControl temperatureControl1;
```

**入口演示窗体为：**
![运行使用](https://www.dcwriter.cn/image/Entry_Demo_Form.png)
---

## 4. 功能介绍

### 4.1 数据格式
![数据格式](https://www.dcwriter.cn/image/Data_Format.png)

使用「xml」作为基本时间轴文件数据格式。示例 xml 存储在「**TimeLineDemoFiles**」文件夹内。

### 4.2 视图
![视图](https://www.dcwriter.cn/image/View.png)

支持普通视图、分页视图、时间轴视图这三种视图场景。

| 视图类型   | 说明                                                                 |
| ---------- | -------------------------------------------------------------------- |
| **普通视图** | 即全文展开呈现，一页默认展示固定天数的数据，需手动翻页查看。         |
| **分页视图** | 类似于 Word 视图，支持上下分页，且与打印结果高度一致。               |
| **时间轴视图** | 是将所有数据从左至右展开，全部呈现出来。                             |

### 4.3 设计器
软件提供配置化设计器工具，可配置化生成模板。
![设计器](https://www.dcwriter.cn/image/Designer.png)


#### 4.3.1 插入元素
![插入元素](https://www.dcwriter.cn/image/Insert_Element.png)

插入元素功能可快速在指定区域插入布局元素。元素分布如下图：

![元素分布](https://www.dcwriter.cn/image/Element_Layout.png)


#### 4.3.2 页面设置
![页面设置](https://www.dcwriter.cn/image/Page_Setup.png)

点击页面设置支持与 Word 一致的页边距、方向、纸张大小等设计。

#### 4.3.3 移动与删除
![移动与删除](https://www.dcwriter.cn/image/Move_Delete.png)

点击可操作光标选中的元素，支持上移下移和删除当前光标的元素。

#### 4.3.4 导出文档
![导出文档](https://www.dcwriter.cn/image/Export_Doc.png)

导出文档指支持导出模板 xml 文件到本地。

### 4.4 保存
![保存](https://www.dcwriter.cn/image/Save_Doc.png)

保存文档最终返回的也是 xml，该 xml 为模板 xml 与数据的组合。

### 4.5 结构树
![结构树](https://www.dcwriter.cn/image/Struct_Tree.png)

结构树可实时展示当前文档的 DOM 结构。

### 4.6 打印
![打印](https://www.dcwriter.cn/image/Print_Doc.png)

点击打印，可直接调用客户端打印功能进行打印。

---

## 5. 总结

时间轴产品拥有灵活的模板设计能力，支持快速进行定制化部署，已在全国各级医疗机构成功应用，能够全面满足国内护理信息系统对体温单管理的规范化需求。基于源码，客户还能将其扩展应用于产程图、新生儿体温单等方面，也可用于其他行业需要展示数据的场景。该产品还支持导出图片，并可将图片插入到 Word 文档中。
