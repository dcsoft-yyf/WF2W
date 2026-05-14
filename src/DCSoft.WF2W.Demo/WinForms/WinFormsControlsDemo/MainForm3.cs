using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsControlsDemo
{
    public partial class MainForm3 : Form
    {
        // 分类与控件列表映射（与之前相同）
        private Dictionary<string, List<string>> categoryControlsMap;

        // 用于管理非可视化组件（Timer、NotifyIcon等）
        private Container componentsContainer;

        public MainForm3()
        {
            InitializeComponent();

            componentsContainer = new Container();

            BuildCategoryMap();
            BuildTabPages();
            this.Text = ".NET Framework 4.0 WinForms 控件全集演示（TabControl 分类导航）";
            this.Size = new Size(1024, 768);
            

            this.FormClosing += (s, e) =>
            {
                if (componentsContainer != null)
                {
                    componentsContainer.Dispose();
                }
            };
        }

        /// <summary>
        /// 构建分类与控件列表的映射（与之前相同）
        /// </summary>
        private void BuildCategoryMap()
        {
            categoryControlsMap = new Dictionary<string, List<string>>();

            // 1. 基本控件
            categoryControlsMap.Add("基本控件", new List<string>
            {
                "Button", "CheckBox", "RadioButton", "Label", "LinkLabel",
                "TextBox", "RichTextBox", "NumericUpDown", "PictureBox",
                "ProgressBar", "TrackBar"
            });

            // 2. 列表与选择
            categoryControlsMap.Add("列表与选择", new List<string>
            {
                "ComboBox", "ListBox", "CheckedListBox", "ListView",
                "TreeView", "DataGridView", "MonthCalendar", "DateTimePicker"
            });

            // 3. 容器控件
            categoryControlsMap.Add("容器控件", new List<string>
            {
                "GroupBox", "Panel", "TabControl", "SplitContainer",
                "FlowLayoutPanel", "TableLayoutPanel"
            });

            // 4. 菜单、工具栏与状态
            categoryControlsMap.Add("菜单/工具栏", new List<string>
            {
                "MenuStrip", "ContextMenuStrip", "StatusStrip",
                "ToolStrip", "NotifyIcon"
            });

            // 5. 组件
            categoryControlsMap.Add("组件", new List<string>
            {
                "Timer", "ToolTip"
            });

            // 6. 其他
            categoryControlsMap.Add("其他", new List<string>
            {
                "WebBrowser"
            });
        }

        /// <summary>
        /// 根据分类映射创建 TabPage，每个页面内放置一个 FlowLayoutPanel 用于排列控件演示
        /// </summary>
        private void BuildTabPages()
        {
            foreach (var category in categoryControlsMap.Keys)
            {
                // 创建标签页
                TabPage page = new TabPage(category);

                // 创建流式布局面板，垂直排列，自动滚动
                FlowLayoutPanel flp = new FlowLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    FlowDirection = FlowDirection.TopDown,
                    AutoScroll = true,
                    WrapContents = false,
                    Padding = new Padding(10)
                };

                // 根据分类名称，调用对应的方法填充该分类下的所有控件演示
                PopulateCategoryPage(category, flp);

                page.Controls.Add(flp);
                tabControl.TabPages.Add(page);
            }
        }

        /// <summary>
        /// 根据分类名称，将对应控件列表的所有演示添加到传入的 FlowLayoutPanel 中
        /// </summary>
        private void PopulateCategoryPage(string category, FlowLayoutPanel parentPanel)
        {
            if (!categoryControlsMap.ContainsKey(category)) return;

            foreach (string controlName in categoryControlsMap[category])
            {
                // 根据控件名调用对应的演示方法（所有演示方法均已改造为接受 FlowLayoutPanel 参数）
                switch (controlName)
                {
                    case "Button": ShowButtonDemo(parentPanel); break;
                    case "CheckBox": ShowCheckBoxDemo(parentPanel); break;
                    case "RadioButton": ShowRadioButtonDemo(parentPanel); break;
                    case "Label": ShowLabelDemo(parentPanel); break;
                    case "LinkLabel": ShowLinkLabelDemo(parentPanel); break;
                    case "TextBox": ShowTextBoxDemo(parentPanel); break;
                    //case "RichTextBox": ShowRichTextBoxDemo(parentPanel); break;
                    case "NumericUpDown": ShowNumericUpDownDemo(parentPanel); break;
                    case "PictureBox": ShowPictureBoxDemo(parentPanel); break;
                    case "ProgressBar": ShowProgressBarDemo(parentPanel); break;
                    //case "TrackBar": ShowTrackBarDemo(parentPanel); break;
                    case "ComboBox": ShowComboBoxDemo(parentPanel); break;
                    case "ListBox": ShowListBoxDemo(parentPanel); break;
                    case "CheckedListBox": ShowCheckedListBoxDemo(parentPanel); break;
                    case "ListView": ShowListViewDemo(parentPanel); break;
                    case "TreeView": ShowTreeViewDemo(parentPanel); break;
                    case "DataGridView": ShowDataGridViewDemo(parentPanel); break;
                    //case "MonthCalendar": ShowMonthCalendarDemo(parentPanel); break;
                    case "DateTimePicker": ShowDateTimePickerDemo(parentPanel); break;
                    case "GroupBox": ShowGroupBoxDemo(parentPanel); break;
                    case "Panel": ShowPanelDemo(parentPanel); break;
                    case "TabControl": ShowTabControlDemo(parentPanel); break;
                    case "SplitContainer": ShowSplitContainerDemo(parentPanel); break;
                    case "FlowLayoutPanel": ShowFlowLayoutPanelDemo(parentPanel); break;
                    case "TableLayoutPanel": ShowTableLayoutPanelDemo(parentPanel); break;
                    case "MenuStrip": ShowMenuStripDemo(parentPanel); break;
                    case "ContextMenuStrip": ShowContextMenuStripDemo(parentPanel); break;
                    case "StatusStrip": ShowStatusStripDemo(parentPanel); break;
                    case "ToolStrip": ShowToolStripDemo(parentPanel); break;
                    //case "NotifyIcon": ShowNotifyIconDemo(parentPanel); break;
                    case "Timer": ShowTimerDemo(parentPanel); break;
                    case "ToolTip": ShowToolTipDemo(parentPanel); break;
                    //case "WebBrowser": ShowWebBrowserDemo(parentPanel); break;
                    default:
                        // 如果遇到未知控件，显示提示信息
                        Label lbl = new Label { Text = $"控件 [{controlName}] 的演示未实现。", AutoSize = true, ForeColor = Color.Red };
                        parentPanel.Controls.Add(lbl);
                        break;
                }
            }
        }

        // --------------------------------------------------------------------------------
        // 以下为所有控件的演示方法，均已改造为接受 FlowLayoutPanel 参数，
        // 将原本要添加到内部 FlowLayoutPanel 的控件组直接添加到传入的 parentPanel。
        // 移除了内部 CreateBasePanel 的调用，直接使用 parentPanel。
        // --------------------------------------------------------------------------------

        private void ShowButtonDemo(FlowLayoutPanel parentPanel)
        {
            Button btnNormal = new Button { Text = "普通按钮", Width = 120 };
            btnNormal.Click += (s, e) => MessageBox.Show("按钮被点击！", "演示");
            parentPanel.Controls.Add(WithLabel(btnNormal, "普通按钮：点击弹出消息"));

            Button btnIcon = new Button { Text = "图标按钮", Width = 120, ImageAlign = ContentAlignment.MiddleLeft };
            try { btnIcon.Image = System.Drawing.SystemIcons.Application.ToBitmap(); } catch { }
            parentPanel.Controls.Add(WithLabel(btnIcon, "带图标的按钮"));

            Button btnFlat = new Button { Text = "Flat样式", Width = 120, FlatStyle = FlatStyle.Flat };
            parentPanel.Controls.Add(WithLabel(btnFlat, "FlatStyle.Flat"));

            Button btnDisabled = new Button { Text = "禁用按钮", Width = 120, Enabled = false };
            parentPanel.Controls.Add(WithLabel(btnDisabled, "Enabled = false"));
        }

        private void ShowCheckBoxDemo(FlowLayoutPanel parentPanel)
        {
            Label lblState = new Label { Text = "勾选状态变化请查看下方", AutoSize = true };

            CheckBox chk1 = new CheckBox { Text = "未勾选", Checked = false };
            CheckBox chk2 = new CheckBox { Text = "已勾选", Checked = true };
            CheckBox chk3 = new CheckBox { Text = "三态支持", ThreeState = true, CheckState = CheckState.Indeterminate };

            chk1.CheckedChanged += (s, e) => UpdateCheckBoxState(chk1);
            chk2.CheckedChanged += (s, e) => UpdateCheckBoxState(chk2);
            chk3.CheckStateChanged += (s, e) => UpdateCheckBoxState(chk3);

            parentPanel.Controls.Add(WithLabel(chk1, "普通复选框"));
            parentPanel.Controls.Add(WithLabel(chk2, "默认勾选"));
            parentPanel.Controls.Add(WithLabel(chk3, "三态(不确定状态)"));
            parentPanel.Controls.Add(lblState);

            void UpdateCheckBoxState(CheckBox cb)
            {
                string state = cb.CheckState.ToString();
                lblState.Text = $"{cb.Text} 当前状态: {state}";
            }
        }

        private void ShowRadioButtonDemo(FlowLayoutPanel parentPanel)
        {
            GroupBox group = new GroupBox { Text = "单选组", Width = 300, Height = 120 };
            RadioButton rb1 = new RadioButton { Text = "选项一", Location = new Point(10, 20), Checked = true };
            RadioButton rb2 = new RadioButton { Text = "选项二", Location = new Point(10, 50) };
            RadioButton rb3 = new RadioButton { Text = "选项三", Location = new Point(10, 80) };
            group.Controls.AddRange(new Control[] { rb1, rb2, rb3 });

            rb1.CheckedChanged += (s, e) => ShowRadioSelection(rb1);
            rb2.CheckedChanged += (s, e) => ShowRadioSelection(rb2);
            rb3.CheckedChanged += (s, e) => ShowRadioSelection(rb3);

            parentPanel.Controls.Add(WithLabel(group, "RadioButton 组"));
            parentPanel.Controls.Add(new Label { Text = "点击单选按钮查看选中项", AutoSize = true });

            void ShowRadioSelection(RadioButton rb)
            {
                if (rb.Checked)
                    MessageBox.Show($"选中了: {rb.Text}", "RadioButton演示");
            }
        }

        private void ShowLabelDemo(FlowLayoutPanel parentPanel)
        {
            parentPanel.Controls.Add(new Label { Text = "普通文本标签", AutoSize = true });
            parentPanel.Controls.Add(new Label { Text = "带边框的标签", BorderStyle = BorderStyle.FixedSingle, AutoSize = true });
            parentPanel.Controls.Add(new Label { Text = "自动换行的标签，宽度150，内容较长时会自动折行。", Width = 150, AutoSize = false });
            Label linkStyle = new Label { Text = "仿链接样式标签", ForeColor = Color.Blue, Font = new Font("宋体", 9, FontStyle.Underline), Cursor = Cursors.Hand };
            linkStyle.Click += (s, e) => MessageBox.Show("标签点击", "演示");
            parentPanel.Controls.Add(WithLabel(linkStyle, "点击可响应事件"));
        }

        private void ShowLinkLabelDemo(FlowLayoutPanel parentPanel)
        {
            LinkLabel ll1 = new LinkLabel { Text = "单击打开链接 (演示)", AutoSize = true };
            ll1.LinkClicked += (s, e) => MessageBox.Show("链接被点击 (实际可打开网址)", "LinkLabel");
            parentPanel.Controls.Add(ll1);

            LinkLabel ll2 = new LinkLabel { Text = "多链接：第一部分 第二部分", AutoSize = true };
            ll2.Links.Add(0, 4, "link1");
            ll2.Links.Add(5, 4, "link2");
            ll2.LinkClicked += (s, e) => MessageBox.Show($"点击了链接: {e.Link.LinkData}", "多链接");
            parentPanel.Controls.Add(ll2);
        }

        private void ShowTextBoxDemo(FlowLayoutPanel parentPanel)
        {
            TextBox txtSingle = new TextBox { Text = "单行文本框", Width = 200 };
            txtSingle.TextChanged += (s, e) => Console.WriteLine($"文本变化: {txtSingle.Text}");
            parentPanel.Controls.Add(WithLabel(txtSingle, "单行"));

            TextBox txtPassword = new TextBox { PasswordChar = '*', Width = 200 };
            parentPanel.Controls.Add(WithLabel(txtPassword, "密码框"));

            TextBox txtMulti = new TextBox { Multiline = true, Height = 60, Width = 200, ScrollBars = ScrollBars.Vertical, Text = "多行\n文本框" };
            parentPanel.Controls.Add(WithLabel(txtMulti, "多行"));

            TextBox txtReadOnly = new TextBox { Text = "只读内容", ReadOnly = true, Width = 200 };
            parentPanel.Controls.Add(WithLabel(txtReadOnly, "只读"));
        }

        private void ShowRichTextBoxDemo(FlowLayoutPanel parentPanel)
        {
            RichTextBox rtb = new RichTextBox { Height = 150, Width = 400 };
            rtb.AppendText("RichTextBox支持富文本格式。");
            rtb.Select(0, 10);
            rtb.SelectionColor = Color.Red;
            rtb.SelectionFont = new Font("宋体", 12, FontStyle.Bold);
            rtb.Select(11, 6);
            rtb.SelectionColor = Color.Blue;
            rtb.SelectionFont = new Font("隶书", 14, FontStyle.Underline);

            parentPanel.Controls.Add(rtb);
            parentPanel.Controls.Add(new Label { Text = "选中部分文字可改变颜色/字体", AutoSize = true });
        }

        private void ShowNumericUpDownDemo(FlowLayoutPanel parentPanel)
        {
            NumericUpDown nud = new NumericUpDown { Minimum = 0, Maximum = 100, Value = 30, Width = 100 };
            Label lblValue = new Label { Text = $"当前值: {nud.Value}", AutoSize = true };
            nud.ValueChanged += (s, e) => lblValue.Text = $"当前值: {nud.Value}";

            parentPanel.Controls.Add(WithLabel(nud, "数值选择 (0-100)"));
            parentPanel.Controls.Add(lblValue);
        }

        private void ShowPictureBoxDemo(FlowLayoutPanel parentPanel)
        {
            PictureBox pb = new PictureBox { Width = 100, Height = 100, BorderStyle = BorderStyle.FixedSingle, SizeMode = PictureBoxSizeMode.StretchImage };
            try
            {
                pb.Image = System.Drawing.SystemIcons.WinLogo.ToBitmap();
            }
            catch
            {
                pb.BackColor = Color.Gray;
            }
            parentPanel.Controls.Add(WithLabel(pb, "PictureBox 显示图标"));
        }

        private void ShowProgressBarDemo(FlowLayoutPanel parentPanel)
        {
            ProgressBar pb = new ProgressBar { Minimum = 0, Maximum = 100, Value = 45, Width = 300 };
            Label lbl = new Label { Text = "进度: 45%", AutoSize = true };

            Button btnInc = new Button { Text = "增加10", Width = 80 };
            btnInc.Click += (s, e) =>
            {
                if (pb.Value < pb.Maximum) pb.Value += 10;
                lbl.Text = $"进度: {pb.Value}%";
            };

            parentPanel.Controls.Add(pb);
            parentPanel.Controls.Add(lbl);
            parentPanel.Controls.Add(btnInc);
        }

        private void ShowTrackBarDemo(FlowLayoutPanel parentPanel)
        {
            TrackBar tb = new TrackBar { Minimum = 0, Maximum = 100, TickFrequency = 10, Width = 300 };
            Label lbl = new Label { Text = "滑块值: 50", AutoSize = true };
            tb.Value = 50;
            tb.ValueChanged += (s, e) => lbl.Text = $"滑块值: {tb.Value}";

            parentPanel.Controls.Add(tb);
            parentPanel.Controls.Add(lbl);
        }

        private void ShowComboBoxDemo(FlowLayoutPanel parentPanel)
        {
            ComboBox cb = new ComboBox { Width = 150, DropDownStyle = ComboBoxStyle.DropDownList };
            cb.Items.AddRange(new object[] { "北京", "上海", "广州", "深圳" });
            cb.SelectedIndex = 0;

            ComboBox cbEditable = new ComboBox { Width = 150, DropDownStyle = ComboBoxStyle.DropDown };
            cbEditable.Items.AddRange(new object[] { "选项A", "选项B", "选项C" });

            Label lblSel = new Label { Text = $"选中: {cb.Text}", AutoSize = true };
            cb.SelectedIndexChanged += (s, e) => lblSel.Text = $"选中: {cb.Text}";

            parentPanel.Controls.Add(WithLabel(cb, "下拉列表"));
            parentPanel.Controls.Add(WithLabel(cbEditable, "可编辑下拉框"));
            parentPanel.Controls.Add(lblSel);
        }

        private void ShowListBoxDemo(FlowLayoutPanel parentPanel)
        {
            ListBox lb = new ListBox { Width = 150, Height = 100 };
            lb.Items.AddRange(new object[] { "C#", "Java", "Python", "C++", "JavaScript" });
            lb.SelectionMode = SelectionMode.One;

            Label lblSel = new Label { Text = "未选中", AutoSize = true };
            lb.SelectedIndexChanged += (s, e) =>
            {
                if (lb.SelectedItem != null)
                    lblSel.Text = $"选中: {lb.SelectedItem}";
            };

            parentPanel.Controls.Add(WithLabel(lb, "ListBox"));
            parentPanel.Controls.Add(lblSel);
        }

        private void ShowCheckedListBoxDemo(FlowLayoutPanel parentPanel)
        {
            CheckedListBox clb = new CheckedListBox { Width = 200, Height = 120 };
            clb.Items.Add("选项1", true);
            clb.Items.Add("选项2", false);
            clb.Items.Add("选项3", false);

            Button btnShow = new Button { Text = "显示选中项", Width = 100 };
            btnShow.Click += (s, e) =>
            {
                string sel = "";
                foreach (var item in clb.CheckedItems)
                    sel += item.ToString() + " ";
                MessageBox.Show("选中: " + (string.IsNullOrEmpty(sel) ? "无" : sel), "CheckedListBox");
            };

            parentPanel.Controls.Add(WithLabel(clb, "CheckedListBox (可多选)"));
            parentPanel.Controls.Add(btnShow);
        }

        private void ShowListViewDemo(FlowLayoutPanel parentPanel)
        {
            ListView lv = new ListView { Width = 350, Height = 150, View = View.Details, FullRowSelect = true, GridLines = true };
            lv.Columns.Add("姓名", 100);
            lv.Columns.Add("年龄", 60);
            lv.Columns.Add("城市", 120);

            lv.Items.Add(new ListViewItem(new[] { "张三", "25", "北京" }));
            lv.Items.Add(new ListViewItem(new[] { "李四", "30", "上海" }));
            lv.Items.Add(new ListViewItem(new[] { "王五", "28", "广州" }));

            parentPanel.Controls.Add(WithLabel(lv, "ListView (详细信息视图)"));
        }

        private void ShowTreeViewDemo(FlowLayoutPanel parentPanel)
        {
            TreeView tv = new TreeView { Width = 250, Height = 150 };
            TreeNode root = new TreeNode("中国");
            root.Nodes.Add("北京");
            root.Nodes.Add("上海");
            TreeNode gd = new TreeNode("广东");
            gd.Nodes.Add("广州");
            gd.Nodes.Add("深圳");
            root.Nodes.Add(gd);
            tv.Nodes.Add(root);
            tv.ExpandAll();

            parentPanel.Controls.Add(WithLabel(tv, "TreeView 示例"));
        }

        private void ShowDataGridViewDemo(FlowLayoutPanel parentPanel)
        {
            DataGridView dgv = new DataGridView { Width = 450, Height = 180, AllowUserToAddRows = false, ReadOnly = true };
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("产品", typeof(string));
            dt.Columns.Add("价格", typeof(decimal));
            dt.Rows.Add(1, "笔记本", 12.5m);
            dt.Rows.Add(2, "鼠标", 8.0m);
            dt.Rows.Add(3, "键盘", 22.0m);
            dgv.DataSource = dt;

            parentPanel.Controls.Add(WithLabel(dgv, "DataGridView 数据绑定"));
        }

        private void ShowMonthCalendarDemo(FlowLayoutPanel parentPanel)
        {
            MonthCalendar cal = new MonthCalendar { MaxSelectionCount = 7 };
            Label lblDate = new Label { Text = "选中范围: ", AutoSize = true };
            cal.DateChanged += (s, e) =>
            {
                lblDate.Text = $"选中范围: {cal.SelectionStart.ToShortDateString()} - {cal.SelectionEnd.ToShortDateString()}";
            };

            parentPanel.Controls.Add(cal);
            parentPanel.Controls.Add(lblDate);
        }

        private void ShowDateTimePickerDemo(FlowLayoutPanel parentPanel)
        {
            DateTimePicker dtp = new DateTimePicker { Width = 200, Format = DateTimePickerFormat.Short };
            Label lbl = new Label { Text = $"选中日期: {dtp.Value.ToShortDateString()}", AutoSize = true };
            dtp.ValueChanged += (s, e) => lbl.Text = $"选中日期: {dtp.Value.ToShortDateString()}";

            parentPanel.Controls.Add(WithLabel(dtp, "DateTimePicker"));
            parentPanel.Controls.Add(lbl);
        }

        private void ShowGroupBoxDemo(FlowLayoutPanel parentPanel)
        {
            GroupBox gb = new GroupBox { Text = "用户信息", Width = 300, Height = 150 };
            Label lblName = new Label { Text = "姓名:", Location = new Point(10, 20) };
            TextBox txtName = new TextBox { Location = new Point(60, 18), Width = 120 };
            Label lblAge = new Label { Text = "年龄:", Location = new Point(10, 50) };
            NumericUpDown nudAge = new NumericUpDown { Location = new Point(60, 48), Width = 60, Minimum = 0, Maximum = 120 };
            gb.Controls.AddRange(new Control[] { lblName, txtName, lblAge, nudAge });

            parentPanel.Controls.Add(WithLabel(gb, "GroupBox 容器"));
        }

        private void ShowPanelDemo(FlowLayoutPanel parentPanel)
        {
            Panel pnl = new Panel { BorderStyle = BorderStyle.FixedSingle, Width = 300, Height = 100, AutoScroll = true };
            pnl.Controls.Add(new Button { Text = "按钮1", Location = new Point(10, 10) });
            pnl.Controls.Add(new Button { Text = "按钮2", Location = new Point(10, 40) });
            pnl.Controls.Add(new Button { Text = "按钮3", Location = new Point(10, 70) });

            parentPanel.Controls.Add(WithLabel(pnl, "Panel 带滚动条"));
        }

        private void ShowTabControlDemo(FlowLayoutPanel parentPanel)
        {
            TabControl tc = new TabControl { Width = 400, Height = 200 };
            TabPage tp1 = new TabPage("页面1");
            tp1.Controls.Add(new Label { Text = "这是第一个标签页", AutoSize = true, Location = new Point(20, 20) });
            TabPage tp2 = new TabPage("页面2");
            tp2.Controls.Add(new Button { Text = "页面2的按钮", Location = new Point(20, 20) });
            tc.TabPages.Add(tp1);
            tc.TabPages.Add(tp2);

            parentPanel.Controls.Add(WithLabel(tc, "TabControl 选项卡"));
        }

        private void ShowSplitContainerDemo(FlowLayoutPanel parentPanel)
        {
            SplitContainer sc = new SplitContainer { Width = 450, Height = 150, Orientation = Orientation.Vertical };
            sc.Panel1.BackColor = Color.LightBlue;
            sc.Panel1.Controls.Add(new Label { Text = "左面板", AutoSize = true });
            sc.Panel2.BackColor = Color.LightCoral;
            sc.Panel2.Controls.Add(new Label { Text = "右面板", AutoSize = true });

            parentPanel.Controls.Add(WithLabel(sc, "SplitContainer (可拖动分隔条)"));
        }

        private void ShowFlowLayoutPanelDemo(FlowLayoutPanel parentPanel)
        {
            FlowLayoutPanel flp = new FlowLayoutPanel { Width = 400, Height = 120, BorderStyle = BorderStyle.FixedSingle, AutoScroll = true };
            flp.Controls.Add(new Button { Text = "按钮1" });
            flp.Controls.Add(new Button { Text = "按钮2" });
            flp.Controls.Add(new Button { Text = "按钮3" });
            flp.Controls.Add(new Button { Text = "按钮4" });
            flp.Controls.Add(new Button { Text = "按钮5" });

            parentPanel.Controls.Add(WithLabel(flp, "FlowLayoutPanel 自动排列"));
        }

        private void ShowTableLayoutPanelDemo(FlowLayoutPanel parentPanel)
        {
            TableLayoutPanel tlp = new TableLayoutPanel { Width = 300, Height = 150, ColumnCount = 2, RowCount = 2, BorderStyle = BorderStyle.FixedSingle };
            tlp.Controls.Add(new Label { Text = "行1列1", BackColor = Color.LightYellow }, 0, 0);
            tlp.Controls.Add(new Label { Text = "行1列2", BackColor = Color.LightGreen }, 1, 0);
            tlp.Controls.Add(new Button { Text = "行2列1" }, 0, 1);
            tlp.Controls.Add(new Button { Text = "行2列2" }, 1, 1);

            parentPanel.Controls.Add(WithLabel(tlp, "TableLayoutPanel 表格布局"));
        }

        private void ShowMenuStripDemo(FlowLayoutPanel parentPanel)
        {
            MenuStrip ms = new MenuStrip { Dock = DockStyle.Top };
            ToolStripMenuItem fileMenu = new ToolStripMenuItem("文件");
            fileMenu.DropDownItems.Add("打开", null, (s, e) => MessageBox.Show("打开文件", "菜单"));
            fileMenu.DropDownItems.Add("保存", null, (s, e) => MessageBox.Show("保存文件", "菜单"));
            ms.Items.Add(fileMenu);
            ms.Items.Add("编辑");
            ms.Items.Add("视图");

            Panel p = new Panel { Width = 400, Height = 80, BorderStyle = BorderStyle.FixedSingle };
            p.Controls.Add(ms);
            ms.BackColor = Color.LightGray;
            parentPanel.Controls.Add(WithLabel(p, "MenuStrip 菜单栏 (点击菜单测试)"));
        }

        private void ShowContextMenuStripDemo(FlowLayoutPanel parentPanel)
        {
            ContextMenuStrip cms = new ContextMenuStrip();
            cms.Items.Add("复制", null, (s, e) => MessageBox.Show("复制操作", "右键菜单"));
            cms.Items.Add("粘贴", null, (s, e) => MessageBox.Show("粘贴操作", "右键菜单"));

            Button btn = new Button { Text = "右键点击我", Width = 120, ContextMenuStrip = cms };
            parentPanel.Controls.Add(WithLabel(btn, "按钮绑定了右键菜单"));
        }

        private void ShowStatusStripDemo(FlowLayoutPanel parentPanel)
        {
            StatusStrip ss = new StatusStrip { Dock = DockStyle.Bottom };
            ss.Items.Add("就绪");
            ss.Items.Add(new ToolStripStatusLabel { Text = "|" });
            ss.Items.Add(new ToolStripStatusLabel { Text = "用户: Demo" });

            Panel p = new Panel { Width = 400, Height = 60, BorderStyle = BorderStyle.FixedSingle };
            p.Controls.Add(ss);
            parentPanel.Controls.Add(WithLabel(p, "StatusStrip 状态栏"));
        }

        private void ShowToolStripDemo(FlowLayoutPanel parentPanel)
        {
            ToolStrip ts = new ToolStrip { Dock = DockStyle.Top };
            ts.Items.Add("新建", null, (s, e) => MessageBox.Show("新建", "工具栏"));
            ts.Items.Add("打开", null, (s, e) => MessageBox.Show("打开", "工具栏"));
            ts.Items.Add(new ToolStripSeparator());
            ts.Items.Add(new ToolStripButton { Text = "保存", DisplayStyle = ToolStripItemDisplayStyle.Text });

            Panel p = new Panel { Width = 400, Height = 60, BorderStyle = BorderStyle.FixedSingle };
            p.Controls.Add(ts);
            parentPanel.Controls.Add(WithLabel(p, "ToolStrip 工具栏"));
        }

        private void ShowNotifyIconDemo(FlowLayoutPanel parentPanel)
        {
            NotifyIcon ni = new NotifyIcon(componentsContainer);
            ni.Icon = SystemIcons.Application;
            ni.Text = "NotifyIcon演示";
            ni.Visible = true;

            Button btnShow = new Button { Text = "显示气球提示", Width = 120 };
            btnShow.Click += (s, e) => ni.ShowBalloonTip(3000, "通知", "这是NotifyIcon的气球提示", ToolTipIcon.Info);

            Button btnHide = new Button { Text = "隐藏图标", Width = 120 };
            btnHide.Click += (s, e) => ni.Visible = false;

            parentPanel.Controls.Add(WithLabel(btnShow, "点击显示托盘通知"));
            parentPanel.Controls.Add(btnHide);
            parentPanel.Controls.Add(new Label { Text = "注意：托盘区会出现应用图标", AutoSize = true });

            componentsContainer.Add(ni);
        }

        private void ShowTimerDemo(FlowLayoutPanel parentPanel)
        {
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer(componentsContainer);
            timer.Interval = 1000;

            ProgressBar pb = new ProgressBar { Width = 200, Minimum = 0, Maximum = 10, Value = 0 };
            Label lbl = new Label { Text = "计数: 0", AutoSize = true };
            Button btnStart = new Button { Text = "启动定时器", Width = 100 };
            Button btnStop = new Button { Text = "停止", Width = 100 };

            timer.Tick += (s, e) =>
            {
                if (pb.Value < pb.Maximum)
                    pb.Value += 1;
                else
                    pb.Value = pb.Minimum;
                lbl.Text = $"计数: {pb.Value}";
            };

            btnStart.Click += (s, e) => timer.Start();
            btnStop.Click += (s, e) => timer.Stop();

            parentPanel.Controls.Add(WithLabel(pb, "ProgressBar (每秒+1)"));
            parentPanel.Controls.Add(lbl);
            parentPanel.Controls.Add(btnStart);
            parentPanel.Controls.Add(btnStop);
        }

        private void ShowToolTipDemo(FlowLayoutPanel parentPanel)
        {
            ToolTip tt = new ToolTip(componentsContainer);
            Button btn = new Button { Text = "鼠标悬停我", Width = 120 };
            tt.SetToolTip(btn, "这是ToolTip提示文本");

            parentPanel.Controls.Add(WithLabel(btn, "按钮设置了工具提示"));
        }

        private void ShowWebBrowserDemo(FlowLayoutPanel parentPanel)
        {
            WebBrowser wb = new WebBrowser { Width = 500, Height = 300 };
            wb.Navigate("about:blank");
            //wb.Document.Write("<html><body><h2>WebBrowser 演示</h2><p>这是一个简单的HTML内容。</p></body></html>");

            parentPanel.Controls.Add(WithLabel(wb, "WebBrowser 显示HTML"));
        }

        // --------------------------------------------------------------------------------
        // 辅助方法
        // --------------------------------------------------------------------------------

        /// <summary>
        /// 将一个控件与描述标签组合（水平排列）
        /// </summary>
        private Panel WithLabel(Control ctrl, string description)
        {
            Panel p = new Panel { Height = ctrl.Height + 30, Width = 500 };
            Label lbl = new Label { Text = description, AutoSize = true, Location = new Point(10, 5) };
            ctrl.Location = new Point(10, 25);
            p.Controls.Add(lbl);
            p.Controls.Add(ctrl);
            return p;
        }


    }
}
