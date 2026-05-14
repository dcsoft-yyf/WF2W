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
    public partial class MainForm4 : Form
    {// 分类与控件列表映射
        private Dictionary<string, List<string>> categoryControlsMap;

        // 用于管理非可视化组件（Timer、NotifyIcon等）
        private Container componentsContainer;

        // 当前主面板上的 Y 坐标（用于排列 GroupBox）
        private int currentMainY = 10;
        public MainForm4()
        {
            InitializeComponent();
            componentsContainer = new Container();
            BuildCategoryMap();
            BuildGroupBoxes();                               // 构建所有分组框
            this.Text = ".NET Framework 4.0 WinForms 控件全集演示（纯 Panel 手动布局）";
            this.Size = new Size(1024, 768);

            // 窗体关闭时释放后台组件
            this.FormClosing += (s, e) => componentsContainer?.Dispose();
        }

        /// <summary>
        /// 构建分类与控件列表的映射
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
        /// 根据分类映射创建 GroupBox，内部使用普通 Panel 手动布局
        /// </summary>
        private void BuildGroupBoxes()
        {
            int groupBoxWidth = pnlMain.ClientSize.Width - 30; // 减去滚动条和边距

            foreach (var category in categoryControlsMap.Keys)
            {
                // 创建分类的 GroupBox
                GroupBox groupBox = new GroupBox
                {
                    Text = category,
                    Location = new Point(10, currentMainY),
                    Width = groupBoxWidth,
                    AutoSize = false   // 手动设置高度
                };

                // 内部 Panel（用于放置所有控件演示）
                Panel innerPanel = new Panel
                {
                    Location = new Point(10, 20),  // 留出 GroupBox 标题空间
                    Width = groupBox.Width - 30,
                    AutoSize = false
                };

                // 填充该分类的控件演示（手动设置每个控件组的 Y 坐标）
                int innerY = 0;
                foreach (string controlName in categoryControlsMap[category])
                {
                    // 根据控件名调用对应的演示方法，传入 innerPanel 和当前 Y 坐标引用
                    switch (controlName)
                    {
                        case "Button": ShowButtonDemo(innerPanel, ref innerY); break;
                        case "CheckBox": ShowCheckBoxDemo(innerPanel, ref innerY); break;
                        case "RadioButton": ShowRadioButtonDemo(innerPanel, ref innerY); break;
                        case "Label": ShowLabelDemo(innerPanel, ref innerY); break;
                        case "LinkLabel": ShowLinkLabelDemo(innerPanel, ref innerY); break;
                        case "TextBox": ShowTextBoxDemo(innerPanel, ref innerY); break;
                        //case "RichTextBox": ShowRichTextBoxDemo(innerPanel, ref innerY); break;
                        case "NumericUpDown": ShowNumericUpDownDemo(innerPanel, ref innerY); break;
                        //case "PictureBox": ShowPictureBoxDemo(innerPanel, ref innerY); break;
                        //case "ProgressBar": ShowProgressBarDemo(innerPanel, ref innerY); break;
                        //case "TrackBar": ShowTrackBarDemo(innerPanel, ref innerY); break;
                        case "ComboBox": ShowComboBoxDemo(innerPanel, ref innerY); break;
                        case "ListBox": ShowListBoxDemo(innerPanel, ref innerY); break;
                        case "CheckedListBox": ShowCheckedListBoxDemo(innerPanel, ref innerY); break;
                        //case "ListView": ShowListViewDemo(innerPanel, ref innerY); break;
                        //case "TreeView": ShowTreeViewDemo(innerPanel, ref innerY); break;
                        //case "DataGridView": ShowDataGridViewDemo(innerPanel, ref innerY); break;
                        //case "MonthCalendar": ShowMonthCalendarDemo(innerPanel, ref innerY); break;
                        //case "DateTimePicker": ShowDateTimePickerDemo(innerPanel, ref innerY); break;
                        case "GroupBox": ShowGroupBoxDemo(innerPanel, ref innerY); break;
                        case "Panel": ShowPanelDemo(innerPanel, ref innerY); break;
                        //case "TabControl": ShowTabControlDemo(innerPanel, ref innerY); break;
                        //case "SplitContainer": ShowSplitContainerDemo(innerPanel, ref innerY); break;
                        //case "FlowLayoutPanel": ShowFlowLayoutPanelDemo(innerPanel, ref innerY); break;
                        //case "TableLayoutPanel": ShowTableLayoutPanelDemo(innerPanel, ref innerY); break;
                        case "MenuStrip": ShowMenuStripDemo(innerPanel, ref innerY); break;
                        case "ContextMenuStrip": ShowContextMenuStripDemo(innerPanel, ref innerY); break;
                        case "StatusStrip": ShowStatusStripDemo(innerPanel, ref innerY); break;
                        case "ToolStrip": ShowToolStripDemo(innerPanel, ref innerY); break;
                        //case "NotifyIcon": ShowNotifyIconDemo(innerPanel, ref innerY); break;
                        case "Timer": ShowTimerDemo(innerPanel, ref innerY); break;
                        case "ToolTip": ShowToolTipDemo(innerPanel, ref innerY); break;
                        //case "WebBrowser": ShowWebBrowserDemo(innerPanel, ref innerY); break;
                        default:
                            //Label lbl = new Label { Text = $"控件 [{controlName}] 的演示未实现。", AutoSize = true, ForeColor = Color.Red };
                            //lbl.Location = new Point(0, innerY);
                            //innerPanel.Controls.Add(lbl);
                            //innerY += lbl.Height + 15;
                            break;
                    }
                }

                // 设置内部 Panel 的高度
                innerPanel.Height = innerY;
                // 设置 GroupBox 的高度（内部 Panel 高度 + 标题高度 20 + 下边距 10）
                groupBox.Height = innerY + 30;

                groupBox.Controls.Add(innerPanel);
                pnlMain.Controls.Add(groupBox);

                // 更新主面板 Y 坐标
                currentMainY += groupBox.Height + 15;
            }

            // 窗体大小变化时调整 GroupBox 宽度
            this.Resize += (s, e) => AdjustGroupBoxWidths();
            AdjustGroupBoxWidths();
        }

        /// <summary>
        /// 调整所有 GroupBox 的宽度，使其与主面板宽度匹配
        /// </summary>
        private void AdjustGroupBoxWidths()
        {
            if (pnlMain.ClientSize.Width <= 0) return;

            int scrollBarWidth = SystemInformation.VerticalScrollBarWidth;
            int newWidth = pnlMain.ClientSize.Width - scrollBarWidth - 30; // 左右边距

            foreach (Control control in pnlMain.Controls)
            {
                if (control is GroupBox gb)
                {
                    gb.Width = newWidth;
                    // 调整内部 Panel 的宽度
                    if (gb.Controls.Count > 0 && gb.Controls[0] is Panel inner)
                    {
                        inner.Width = newWidth - 30;
                    }
                }
            }
        }

        // --------------------------------------------------------------------------------
        // 以下为所有控件的演示方法，均接收父 Panel 和 Y 坐标引用，手动设置每个控件组的位置
        // --------------------------------------------------------------------------------

        private void ShowButtonDemo(Panel parent, ref int y)
        {
            AddControlToPanel(parent, WithLabel(new Button { Text = "普通按钮", Width = 120 }, "普通按钮：点击弹出消息"), ref y, btn =>
                ((Button)btn).Click += (s, e) => MessageBox.Show("按钮被点击！", "演示"));

            Button btnIcon = new Button { Text = "图标按钮", Width = 120, ImageAlign = ContentAlignment.MiddleLeft };
            try { btnIcon.Image = System.Drawing.SystemIcons.Application.ToBitmap(); } catch { }
            AddControlToPanel(parent, WithLabel(btnIcon, "带图标的按钮"), ref y);

            AddControlToPanel(parent, WithLabel(new Button { Text = "Flat样式", Width = 120, FlatStyle = FlatStyle.Flat }, "FlatStyle.Flat"), ref y);

            AddControlToPanel(parent, WithLabel(new Button { Text = "禁用按钮", Width = 120, Enabled = false }, "Enabled = false"), ref y);
        }

        private void ShowCheckBoxDemo(Panel parent, ref int y)
        {
            Label lblState = new Label { Text = "勾选状态变化请查看下方", AutoSize = true };

            CheckBox chk1 = new CheckBox { Text = "未勾选", Checked = false };
            CheckBox chk2 = new CheckBox { Text = "已勾选", Checked = true };
            CheckBox chk3 = new CheckBox { Text = "三态支持", ThreeState = true, CheckState = CheckState.Indeterminate };

            chk1.CheckedChanged += (s, e) => UpdateCheckBoxState(chk1);
            chk2.CheckedChanged += (s, e) => UpdateCheckBoxState(chk2);
            chk3.CheckStateChanged += (s, e) => UpdateCheckBoxState(chk3);

            AddControlToPanel(parent, WithLabel(chk1, "普通复选框"), ref y);
            AddControlToPanel(parent, WithLabel(chk2, "默认勾选"), ref y);
            AddControlToPanel(parent, WithLabel(chk3, "三态(不确定状态)"), ref y);

            lblState.Location = new Point(10, y);
            parent.Controls.Add(lblState);
            y += lblState.Height + 10;

            void UpdateCheckBoxState(CheckBox cb)
            {
                string state = cb.CheckState.ToString();
                lblState.Text = $"{cb.Text} 当前状态: {state}";
            }
        }

        private void ShowRadioButtonDemo(Panel parent, ref int y)
        {
            GroupBox group = new GroupBox { Text = "单选组", Width = 300, Height = 120 };
            RadioButton rb1 = new RadioButton { Text = "选项一", Location = new Point(10, 20), Checked = true };
            RadioButton rb2 = new RadioButton { Text = "选项二", Location = new Point(10, 50) };
            RadioButton rb3 = new RadioButton { Text = "选项三", Location = new Point(10, 80) };
            group.Controls.AddRange(new Control[] { rb1, rb2, rb3 });

            rb1.CheckedChanged += (s, e) => ShowRadioSelection(rb1);
            rb2.CheckedChanged += (s, e) => ShowRadioSelection(rb2);
            rb3.CheckedChanged += (s, e) => ShowRadioSelection(rb3);

            AddControlToPanel(parent, WithLabel(group, "RadioButton 组"), ref y);

            Label lbl = new Label { Text = "点击单选按钮查看选中项", AutoSize = true };
            lbl.Location = new Point(10, y);
            parent.Controls.Add(lbl);
            y += lbl.Height + 10;

            void ShowRadioSelection(RadioButton rb)
            {
                if (rb.Checked)
                    MessageBox.Show($"选中了: {rb.Text}", "RadioButton演示");
            }
        }

        private void ShowLabelDemo(Panel parent, ref int y)
        {
            AddControlToPanel(parent, WithLabel(new Label { Text = "普通文本标签", AutoSize = true }, "普通标签"), ref y);
            AddControlToPanel(parent, WithLabel(new Label { Text = "带边框的标签", BorderStyle = BorderStyle.FixedSingle, AutoSize = true }, "带边框"), ref y);
            AddControlToPanel(parent, WithLabel(new Label { Text = "自动换行的标签，宽度150，内容较长时会自动折行。", Width = 150, AutoSize = false }, "自动换行"), ref y);

            Label linkStyle = new Label { Text = "仿链接样式标签", ForeColor = Color.Blue, Font = new Font("宋体", 9, FontStyle.Underline), Cursor = Cursors.Hand };
            linkStyle.Click += (s, e) => MessageBox.Show("标签点击", "演示");
            AddControlToPanel(parent, WithLabel(linkStyle, "点击可响应事件"), ref y);
        }

        private void ShowLinkLabelDemo(Panel parent, ref int y)
        {
            LinkLabel ll1 = new LinkLabel { Text = "单击打开链接 (演示)", AutoSize = true };
            ll1.LinkClicked += (s, e) => MessageBox.Show("链接被点击 (实际可打开网址)", "LinkLabel");
            AddControlToPanel(parent, WithLabel(ll1, "单链接"), ref y);

            LinkLabel ll2 = new LinkLabel { Text = "多链接：第一部分 第二部分", AutoSize = true };
            ll2.Links.Add(0, 4, "link1");
            ll2.Links.Add(5, 4, "link2");
            ll2.LinkClicked += (s, e) => MessageBox.Show($"点击了链接: {e.Link.LinkData}", "多链接");
            AddControlToPanel(parent, WithLabel(ll2, "多链接"), ref y);
        }

        private void ShowTextBoxDemo(Panel parent, ref int y)
        {
            TextBox txtSingle = new TextBox { Text = "单行文本框", Width = 200 };
            txtSingle.TextChanged += (s, e) => Console.WriteLine($"文本变化: {txtSingle.Text}");
            AddControlToPanel(parent, WithLabel(txtSingle, "单行"), ref y);

            AddControlToPanel(parent, WithLabel(new TextBox { PasswordChar = '*', Width = 200 }, "密码框"), ref y);
            AddControlToPanel(parent, WithLabel(new TextBox { Multiline = true, Height = 60, Width = 200, ScrollBars = ScrollBars.Vertical, Text = "多行\n文本框" }, "多行"), ref y);
            AddControlToPanel(parent, WithLabel(new TextBox { Text = "只读内容", ReadOnly = true, Width = 200 }, "只读"), ref y);
        }

        private void ShowRichTextBoxDemo(Panel parent, ref int y)
        {
            //RichTextBox rtb = new RichTextBox { Height = 150, Width = 400 };
            //rtb.AppendText("RichTextBox支持富文本格式。");
            //rtb.Select(0, 10);
            //rtb.SelectionColor = Color.Red;
            //rtb.SelectionFont = new Font("宋体", 12, FontStyle.Bold);
            //rtb.Select(11, 6);
            //rtb.SelectionColor = Color.Blue;
            //rtb.SelectionFont = new Font("隶书", 14, FontStyle.Underline);

            //Panel p = WithLabel(rtb, "RichTextBox");
            //p.Location = new Point(10, y);
            //parent.Controls.Add(p);
            //y += p.Height + 10;

            //Label lbl = new Label { Text = "选中部分文字可改变颜色/字体", AutoSize = true, Location = new Point(10, y) };
            //parent.Controls.Add(lbl);
            //y += lbl.Height + 10;
        }

        private void ShowNumericUpDownDemo(Panel parent, ref int y)
        {
            NumericUpDown nud = new NumericUpDown { Minimum = 0, Maximum = 100, Value = 30, Width = 100 };
            Label lblValue = new Label { Text = $"当前值: {nud.Value}", AutoSize = true };
            nud.ValueChanged += (s, e) => lblValue.Text = $"当前值: {nud.Value}";

            Panel p = WithLabel(nud, "数值选择 (0-100)");
            p.Location = new Point(10, y);
            parent.Controls.Add(p);
            y += p.Height + 5;

            lblValue.Location = new Point(120, y - 25); // 放在数值框旁边
            parent.Controls.Add(lblValue);
        }

        private void ShowPictureBoxDemo(Panel parent, ref int y)
        {
            PictureBox pb = new PictureBox { Width = 100, Height = 100, BorderStyle = BorderStyle.FixedSingle, SizeMode = PictureBoxSizeMode.StretchImage };
            try { pb.Image = System.Drawing.SystemIcons.WinLogo.ToBitmap(); } catch { pb.BackColor = Color.Gray; }
            AddControlToPanel(parent, WithLabel(pb, "PictureBox 显示图标"), ref y);
        }

        private void ShowProgressBarDemo(Panel parent, ref int y)
        {
            //ProgressBar pb = new ProgressBar { Minimum = 0, Maximum = 100, Value = 45, Width = 300 };
            //Label lbl = new Label { Text = "进度: 45%", AutoSize = true };
            //Button btnInc = new Button { Text = "增加10", Width = 80 };
            //btnInc.Click += (s, e) =>
            //{
            //    if (pb.Value < pb.Maximum) pb.Value += 10;
            //    lbl.Text = $"进度: {pb.Value}%";
            //};

            //Panel p = WithLabel(pb, "ProgressBar");
            //p.Location = new Point(10, y);
            //parent.Controls.Add(p);
            //y += p.Height + 5;

            //lbl.Location = new Point(120, y - 25);
            //parent.Controls.Add(lbl);

            //btnInc.Location = new Point(220, y - 30);
            //parent.Controls.Add(btnInc);
        }

        private void ShowTrackBarDemo(Panel parent, ref int y)
        {
            //TrackBar tb = new TrackBar { Minimum = 0, Maximum = 100, TickFrequency = 10, Width = 300 };
            //Label lbl = new Label { Text = "滑块值: 50", AutoSize = true };
            //tb.Value = 50;
            //tb.ValueChanged += (s, e) => lbl.Text = $"滑块值: {tb.Value}";

            //Panel p = WithLabel(tb, "TrackBar");
            //p.Location = new Point(10, y);
            //parent.Controls.Add(p);
            //y += p.Height + 5;

            //lbl.Location = new Point(320, y - 25);
            //parent.Controls.Add(lbl);
        }

        private void ShowComboBoxDemo(Panel parent, ref int y)
        {
            ComboBox cb = new ComboBox { Width = 150, DropDownStyle = ComboBoxStyle.DropDownList };
            cb.Items.AddRange(new object[] { "北京", "上海", "广州", "深圳" });
            cb.SelectedIndex = 0;

            ComboBox cbEditable = new ComboBox { Width = 150, DropDownStyle = ComboBoxStyle.DropDown };
            cbEditable.Items.AddRange(new object[] { "选项A", "选项B", "选项C" });

            Label lblSel = new Label { Text = $"选中: {cb.Text}", AutoSize = true };
            cb.SelectedIndexChanged += (s, e) => lblSel.Text = $"选中: {cb.Text}";

            AddControlToPanel(parent, WithLabel(cb, "下拉列表"), ref y);
            AddControlToPanel(parent, WithLabel(cbEditable, "可编辑下拉框"), ref y);

            lblSel.Location = new Point(10, y);
            parent.Controls.Add(lblSel);
            y += lblSel.Height + 10;
        }

        private void ShowListBoxDemo(Panel parent, ref int y)
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

            AddControlToPanel(parent, WithLabel(lb, "ListBox"), ref y);
            lblSel.Location = new Point(170, y - 30);
            parent.Controls.Add(lblSel);
        }

        private void ShowCheckedListBoxDemo(Panel parent, ref int y)
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

            AddControlToPanel(parent, WithLabel(clb, "CheckedListBox (可多选)"), ref y);
            btnShow.Location = new Point(220, y - 30);
            parent.Controls.Add(btnShow);
        }

        private void ShowListViewDemo(Panel parent, ref int y)
        {
            ListView lv = new ListView { Width = 350, Height = 150, View = View.Details, FullRowSelect = true, GridLines = true };
            lv.Columns.Add("姓名", 100);
            lv.Columns.Add("年龄", 60);
            lv.Columns.Add("城市", 120);
            lv.Items.Add(new ListViewItem(new[] { "张三", "25", "北京" }));
            lv.Items.Add(new ListViewItem(new[] { "李四", "30", "上海" }));
            lv.Items.Add(new ListViewItem(new[] { "王五", "28", "广州" }));

            AddControlToPanel(parent, WithLabel(lv, "ListView (详细信息视图)"), ref y);
        }

        private void ShowTreeViewDemo(Panel parent, ref int y)
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

            AddControlToPanel(parent, WithLabel(tv, "TreeView 示例"), ref y);
        }

        private void ShowDataGridViewDemo(Panel parent, ref int y)
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

            AddControlToPanel(parent, WithLabel(dgv, "DataGridView 数据绑定"), ref y);
        }

        private void ShowMonthCalendarDemo(Panel parent, ref int y)
        {
            //MonthCalendar cal = new MonthCalendar { MaxSelectionCount = 7 };
            //Label lblDate = new Label { Text = "选中范围: ", AutoSize = true };
            //cal.DateChanged += (s, e) =>
            //{
            //    lblDate.Text = $"选中范围: {cal.SelectionStart.ToShortDateString()} - {cal.SelectionEnd.ToShortDateString()}";
            //};

            //AddControlToPanel(parent, WithLabel(cal, "MonthCalendar"), ref y);
            //lblDate.Location = new Point(10, y);
            //parent.Controls.Add(lblDate);
            //y += lblDate.Height + 10;
        }

        private void ShowDateTimePickerDemo(Panel parent, ref int y)
        {
            DateTimePicker dtp = new DateTimePicker { Width = 200, Format = DateTimePickerFormat.Short };
            Label lbl = new Label { Text = $"选中日期: {dtp.Value.ToShortDateString()}", AutoSize = true };
            dtp.ValueChanged += (s, e) => lbl.Text = $"选中日期: {dtp.Value.ToShortDateString()}";

            AddControlToPanel(parent, WithLabel(dtp, "DateTimePicker"), ref y);
            lbl.Location = new Point(220, y - 30);
            parent.Controls.Add(lbl);
        }

        private void ShowGroupBoxDemo(Panel parent, ref int y)
        {
            GroupBox gb = new GroupBox { Text = "用户信息", Width = 300, Height = 150 };
            Label lblName = new Label { Text = "姓名:", Location = new Point(10, 20) };
            TextBox txtName = new TextBox { Location = new Point(60, 18), Width = 120 };
            Label lblAge = new Label { Text = "年龄:", Location = new Point(10, 50) };
            NumericUpDown nudAge = new NumericUpDown { Location = new Point(60, 48), Width = 60, Minimum = 0, Maximum = 120 };
            gb.Controls.AddRange(new Control[] { lblName, txtName, lblAge, nudAge });

            AddControlToPanel(parent, WithLabel(gb, "GroupBox 容器"), ref y);
        }

        private void ShowPanelDemo(Panel parent, ref int y)
        {
            Panel pnl = new Panel { BorderStyle = BorderStyle.FixedSingle, Width = 300, Height = 100, AutoScroll = true };
            pnl.Controls.Add(new Button { Text = "按钮1", Location = new Point(10, 10) });
            pnl.Controls.Add(new Button { Text = "按钮2", Location = new Point(10, 40) });
            pnl.Controls.Add(new Button { Text = "按钮3", Location = new Point(10, 70) });

            AddControlToPanel(parent, WithLabel(pnl, "Panel 带滚动条"), ref y);
        }

        private void ShowTabControlDemo(Panel parent, ref int y)
        {
            TabControl tc = new TabControl { Width = 400, Height = 200 };
            TabPage tp1 = new TabPage("页面1");
            tp1.Controls.Add(new Label { Text = "这是第一个标签页", AutoSize = true, Location = new Point(20, 20) });
            TabPage tp2 = new TabPage("页面2");
            tp2.Controls.Add(new Button { Text = "页面2的按钮", Location = new Point(20, 20) });
            tc.TabPages.Add(tp1);
            tc.TabPages.Add(tp2);

            AddControlToPanel(parent, WithLabel(tc, "TabControl 选项卡"), ref y);
        }

        private void ShowSplitContainerDemo(Panel parent, ref int y)
        {
            SplitContainer sc = new SplitContainer { Width = 450, Height = 150, Orientation = Orientation.Vertical };
            sc.Panel1.BackColor = Color.LightBlue;
            sc.Panel1.Controls.Add(new Label { Text = "左面板", AutoSize = true });
            sc.Panel2.BackColor = Color.LightCoral;
            sc.Panel2.Controls.Add(new Label { Text = "右面板", AutoSize = true });

            AddControlToPanel(parent, WithLabel(sc, "SplitContainer (可拖动分隔条)"), ref y);
        }

        private void ShowFlowLayoutPanelDemo(Panel parent, ref int y)
        {
            FlowLayoutPanel flp = new FlowLayoutPanel { Width = 400, Height = 120, BorderStyle = BorderStyle.FixedSingle, AutoScroll = true };
            flp.Controls.Add(new Button { Text = "按钮1" });
            flp.Controls.Add(new Button { Text = "按钮2" });
            flp.Controls.Add(new Button { Text = "按钮3" });
            flp.Controls.Add(new Button { Text = "按钮4" });
            flp.Controls.Add(new Button { Text = "按钮5" });

            AddControlToPanel(parent, WithLabel(flp, "FlowLayoutPanel 自动排列"), ref y);
        }

        private void ShowTableLayoutPanelDemo(Panel parent, ref int y)
        {
            TableLayoutPanel tlp = new TableLayoutPanel { Width = 300, Height = 150, ColumnCount = 2, RowCount = 2, BorderStyle = BorderStyle.FixedSingle };
            tlp.Controls.Add(new Label { Text = "行1列1", BackColor = Color.LightYellow }, 0, 0);
            tlp.Controls.Add(new Label { Text = "行1列2", BackColor = Color.LightGreen }, 1, 0);
            tlp.Controls.Add(new Button { Text = "行2列1" }, 0, 1);
            tlp.Controls.Add(new Button { Text = "行2列2" }, 1, 1);

            AddControlToPanel(parent, WithLabel(tlp, "TableLayoutPanel 表格布局"), ref y);
        }

        private void ShowMenuStripDemo(Panel parent, ref int y)
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

            AddControlToPanel(parent, WithLabel(p, "MenuStrip 菜单栏 (点击菜单测试)"), ref y);
        }

        private void ShowContextMenuStripDemo(Panel parent, ref int y)
        {
            ContextMenuStrip cms = new ContextMenuStrip();
            cms.Items.Add("复制", null, (s, e) => MessageBox.Show("复制操作", "右键菜单"));
            cms.Items.Add("粘贴", null, (s, e) => MessageBox.Show("粘贴操作", "右键菜单"));

            Button btn = new Button { Text = "右键点击我", Width = 120, ContextMenuStrip = cms };
            AddControlToPanel(parent, WithLabel(btn, "按钮绑定了右键菜单"), ref y);
        }

        private void ShowStatusStripDemo(Panel parent, ref int y)
        {
            StatusStrip ss = new StatusStrip { Dock = DockStyle.Bottom };
            ss.Items.Add("就绪");
            ss.Items.Add(new ToolStripStatusLabel { Text = "|" });
            ss.Items.Add(new ToolStripStatusLabel { Text = "用户: Demo" });

            Panel p = new Panel { Width = 400, Height = 60, BorderStyle = BorderStyle.FixedSingle };
            p.Controls.Add(ss);

            AddControlToPanel(parent, WithLabel(p, "StatusStrip 状态栏"), ref y);
        }

        private void ShowToolStripDemo(Panel parent, ref int y)
        {
            ToolStrip ts = new ToolStrip { Dock = DockStyle.Top };
            ts.Items.Add("新建", null, (s, e) => MessageBox.Show("新建", "工具栏"));
            ts.Items.Add("打开", null, (s, e) => MessageBox.Show("打开", "工具栏"));
            ts.Items.Add(new ToolStripSeparator());
            ts.Items.Add(new ToolStripButton { Text = "保存", DisplayStyle = ToolStripItemDisplayStyle.Text });

            Panel p = new Panel { Width = 400, Height = 60, BorderStyle = BorderStyle.FixedSingle };
            p.Controls.Add(ts);

            AddControlToPanel(parent, WithLabel(p, "ToolStrip 工具栏"), ref y);
        }

        private void ShowNotifyIconDemo(Panel parent, ref int y)
        {
            //NotifyIcon ni = new NotifyIcon(componentsContainer);
            ////ni.Icon = SystemIcons.Application;
            //ni.Text = "NotifyIcon演示";
            //ni.Visible = true;

            //Button btnShow = new Button { Text = "显示气球提示", Width = 120 };
            //btnShow.Click += (s, e) => ni.ShowBalloonTip(3000, "通知", "这是NotifyIcon的气球提示", ToolTipIcon.Info);

            //Button btnHide = new Button { Text = "隐藏图标", Width = 120 };
            //btnHide.Click += (s, e) => ni.Visible = false;

            //Panel p1 = WithLabel(btnShow, "点击显示托盘通知");
            //p1.Location = new Point(10, y);
            //parent.Controls.Add(p1);
            //y += p1.Height + 5;

            //btnHide.Location = new Point(140, y - 25);
            //parent.Controls.Add(btnHide);

            //Label lbl = new Label { Text = "注意：托盘区会出现应用图标", AutoSize = true, Location = new Point(10, y) };
            //parent.Controls.Add(lbl);
            //y += lbl.Height + 10;

            //componentsContainer.Add(ni);
        }

        private void ShowTimerDemo(Panel parent, ref int y)
        {
            //System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer(componentsContainer);
            //timer.Interval = 1000;

            //ProgressBar pb = new ProgressBar { Width = 200, Minimum = 0, Maximum = 10, Value = 0 };
            //Label lbl = new Label { Text = "计数: 0", AutoSize = true };
            //Button btnStart = new Button { Text = "启动定时器", Width = 100 };
            //Button btnStop = new Button { Text = "停止", Width = 100 };

            //timer.Tick += (s, e) =>
            //{
            //    if (pb.Value < pb.Maximum)
            //        pb.Value += 1;
            //    else
            //        pb.Value = pb.Minimum;
            //    lbl.Text = $"计数: {pb.Value}";
            //};

            //btnStart.Click += (s, e) => timer.Start();
            //btnStop.Click += (s, e) => timer.Stop();

            //Panel p = WithLabel(pb, "ProgressBar (每秒+1)");
            //p.Location = new Point(10, y);
            //parent.Controls.Add(p);
            //y += p.Height + 5;

            //lbl.Location = new Point(220, y - 25);
            //parent.Controls.Add(lbl);

            //btnStart.Location = new Point(10, y);
            //parent.Controls.Add(btnStart);

            //btnStop.Location = new Point(120, y);
            //parent.Controls.Add(btnStop);
            //y += 30;
        }

        private void ShowToolTipDemo(Panel parent, ref int y)
        {
            ToolTip tt = new ToolTip(componentsContainer);
            Button btn = new Button { Text = "鼠标悬停我", Width = 120 };
            tt.SetToolTip(btn, "这是ToolTip提示文本");

            AddControlToPanel(parent, WithLabel(btn, "按钮设置了工具提示"), ref y);
        }

        private void ShowWebBrowserDemo(Panel parent, ref int y)
        {
            //WebBrowser wb = new WebBrowser { Width = 500, Height = 300 };
            //wb.Navigate("about:blank");
            ////wb.Document.Write("<html><body><h2>WebBrowser 演示</h2><p>这是一个简单的HTML内容。</p></body></html>");

            //AddControlToPanel(parent, WithLabel(wb, "WebBrowser 显示HTML"), ref y);
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

        /// <summary>
        /// 将一个已创建的 Panel（通常由 WithLabel 返回）添加到父 Panel，并更新 Y 坐标
        /// </summary>
        private void AddControlToPanel(Panel parent, Panel controlPanel, ref int y)
        {
            controlPanel.Location = new Point(10, y);
            parent.Controls.Add(controlPanel);
            y += controlPanel.Height + 10;
        }

        /// <summary>
        /// 重载：支持为控件附加点击事件（减少重复代码）
        /// </summary>
        private void AddControlToPanel(Panel parent, Panel controlPanel, ref int y, Action<Control> initAction = null)
        {
            if (initAction != null && controlPanel.Controls.Count > 1)
            {
                // 假设第二个控件是主要控件（第一个是 Label）
                initAction(controlPanel.Controls[1]);
            }
            controlPanel.Location = new Point(10, y);
            parent.Controls.Add(controlPanel);
            y += controlPanel.Height + 10;
        }


    }
}
