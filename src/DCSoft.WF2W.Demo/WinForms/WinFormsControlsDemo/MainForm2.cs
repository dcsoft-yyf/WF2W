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
    public partial class MainForm2 : Form
    {
        public MainForm2()
        {
            InitializeComponent();

            BuildCategoryMap();
            PopulateCategoryButtons();
            this.Text = ".NET Framework 4.0 WinForms 控件全集演示（工具栏导航）";
            this.Size = new Size(1024, 768);
            componentsContainer = new Container();
        }

        // 用于管理非可视化组件（Timer、NotifyIcon等）
        private Container componentsContainer;

        /// <summary>
        /// 构建分类与控件列表的映射（基于原有树结构）
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
        /// 根据分类映射生成顶部分类按钮，插入到toolStrip中
        /// </summary>
        private void PopulateCategoryButtons()
        {
            int insertIndex = 1; // 在 "分类:" 标签之后插入，原索引0是"分类:"标签
            foreach (var category in categoryControlsMap.Keys)
            {
                ToolStripButton btn = new ToolStripButton(category);
                btn.Click += CategoryButton_Click;
                toolStrip.Items.Insert(insertIndex++, btn);
            }
            // 默认选中第一个分类，并填充其控件列表
            if (toolStrip.Items.Count > 1 && toolStrip.Items[1] is ToolStripButton firstBtn)
            {
                firstBtn.PerformClick();
            }
        }

        /// <summary>
        /// 分类按钮点击：更新控件下拉框内容
        /// </summary>
        private void CategoryButton_Click(object sender, EventArgs e)
        {
            ToolStripButton clickedBtn = sender as ToolStripButton;
            if (clickedBtn == null) return;

            string category = clickedBtn.Text;
            if (categoryControlsMap.ContainsKey(category))
            {
                // 更新下拉框数据源
                cmbControls.Items.Clear();
                cmbControls.Items.AddRange(categoryControlsMap[category].ToArray());
                if (cmbControls.Items.Count > 0)
                    cmbControls.SelectedIndex = 0; // 自动选中第一个，触发加载演示
                else
                    cmbControls.Text = "";
            }
        }

        /// <summary>
        /// 控件下拉框选择变化：加载对应演示
        /// </summary>
        private void CmbControls_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbControls.SelectedItem != null)
            {
                string controlName = cmbControls.SelectedItem.ToString();
                ShowDemo(controlName);
            }
        }

        /// <summary>
        /// 核心方法：根据控件名称清除当前演示区，并加载新演示
        /// </summary>
        private void ShowDemo(string controlName)
        {
            // 清除现有演示面板的所有控件，同时清理可能遗留的组件（Timer等）
            CleanupDemoPanel();

            // 根据控件名调用对应的演示构建方法
            switch (controlName)
            {
                case "Button": ShowButtonDemo(); break;
                case "CheckBox": ShowCheckBoxDemo(); break;
                case "RadioButton": ShowRadioButtonDemo(); break;
                case "Label": ShowLabelDemo(); break;
                case "LinkLabel": ShowLinkLabelDemo(); break;
                case "TextBox": ShowTextBoxDemo(); break;
                case "RichTextBox": ShowRichTextBoxDemo(); break;
                case "NumericUpDown": ShowNumericUpDownDemo(); break;
                case "PictureBox": ShowPictureBoxDemo(); break;
                case "ProgressBar": ShowProgressBarDemo(); break;
                case "TrackBar": ShowTrackBarDemo(); break;
                case "ComboBox": ShowComboBoxDemo(); break;
                case "ListBox": ShowListBoxDemo(); break;
                case "CheckedListBox": ShowCheckedListBoxDemo(); break;
                case "ListView": ShowListViewDemo(); break;
                case "TreeView": ShowTreeViewDemo(); break;
                case "DataGridView": ShowDataGridViewDemo(); break;
                case "MonthCalendar": ShowMonthCalendarDemo(); break;
                case "DateTimePicker": ShowDateTimePickerDemo(); break;
                case "GroupBox": ShowGroupBoxDemo(); break;
                case "Panel": ShowPanelDemo(); break;
                case "TabControl": ShowTabControlDemo(); break;
                case "SplitContainer": ShowSplitContainerDemo(); break;
                case "FlowLayoutPanel": ShowFlowLayoutPanelDemo(); break;
                case "TableLayoutPanel": ShowTableLayoutPanelDemo(); break;
                case "MenuStrip": ShowMenuStripDemo(); break;
                case "ContextMenuStrip": ShowContextMenuStripDemo(); break;
                case "StatusStrip": ShowStatusStripDemo(); break;
                case "ToolStrip": ShowToolStripDemo(); break;
                case "NotifyIcon": ShowNotifyIconDemo(); break;
                case "Timer": ShowTimerDemo(); break;
                case "ToolTip": ShowToolTipDemo(); break;
                case "WebBrowser": ShowWebBrowserDemo(); break;
                default: ShowDefaultMessage(controlName); break;
            }
        }

        /// <summary>
        /// 清理演示面板：移除所有控件并释放相关组件（线程安全，避免集合修改异常）
        /// </summary>
        private void CleanupDemoPanel()
        {
            // 1. 停止并清理所有由 componentsContainer 管理的组件
            if (componentsContainer != null)
            {
                // 复制组件列表，避免在迭代过程中修改原集合
                var componentsList = new List<System.ComponentModel.IComponent>();
                foreach (System.ComponentModel.IComponent comp in componentsContainer.Components)
                {
                    componentsList.Add(comp);
                }

                foreach (var comp in componentsList)
                {
                    if (comp is System.Windows.Forms.Timer timer)
                    {
                        timer.Stop();
                    }
                    else if (comp is NotifyIcon ni)
                    {
                        ni.Visible = false;
                    }
                    // 释放组件（会将其从容器中移除）
                    comp.Dispose();
                }

                // 释放容器本身
                componentsContainer.Dispose();
            }

            // 2. 重新创建新的容器（确保下次使用不为空）
            componentsContainer = new Container();

            // 3. 清空演示面板的所有控件
            pnlDemo.Controls.Clear();
        }

        #region 演示构建方法（与之前完全一致，仅调整了内部变量声明顺序以消除警告）

        private void ShowButtonDemo()
        {
            FlowLayoutPanel panel = CreateBasePanel();

            Button btnNormal = new Button { Text = "普通按钮", Width = 120 };
            btnNormal.ClickAsync += BtnNormal_Click; //+= (s, e) => MessageBox.Show("按钮被点击！", "演示");
            panel.Controls.Add(WithLabel(btnNormal, "普通按钮：点击弹出消息"));

            Button btnIcon = new Button { Text = "图标按钮", Width = 120, ImageAlign = ContentAlignment.MiddleLeft };
            try { btnIcon.Image = System.Drawing.SystemIcons.Application.ToBitmap(); } catch { }
            panel.Controls.Add(WithLabel(btnIcon, "带图标的按钮"));

            Button btnFlat = new Button { Text = "Flat样式", Width = 120, FlatStyle = FlatStyle.Flat };
            panel.Controls.Add(WithLabel(btnFlat, "FlatStyle.Flat"));

            Button btnDisabled = new Button { Text = "禁用按钮", Width = 120, Enabled = false };
            panel.Controls.Add(WithLabel(btnDisabled, "Enabled = false"));

            pnlDemo.Controls.Add(panel);
        }

        private async Task BtnNormal_Click(object? sender, EventArgs e)
        {
            MessageBox.Show("按钮被点击！", "演示");
        }

        private void ShowCheckBoxDemo()
        {
            FlowLayoutPanel panel = CreateBasePanel();

            // 提前创建状态显示标签
            Label lblState = new Label { Text = "勾选状态变化请查看下方", AutoSize = true };

            CheckBox chk1 = new CheckBox { Text = "未勾选", Checked = false };
            CheckBox chk2 = new CheckBox { Text = "已勾选", Checked = true };
            CheckBox chk3 = new CheckBox { Text = "三态支持", ThreeState = true, CheckState = CheckState.Indeterminate };

            chk1.CheckedChanged += (s, e) => UpdateCheckBoxState(chk1);
            chk2.CheckedChanged += (s, e) => UpdateCheckBoxState(chk2);
            chk3.CheckStateChanged += (s, e) => UpdateCheckBoxState(chk3);

            panel.Controls.Add(WithLabel(chk1, "普通复选框"));
            panel.Controls.Add(WithLabel(chk2, "默认勾选"));
            panel.Controls.Add(WithLabel(chk3, "三态(不确定状态)"));
            panel.Controls.Add(lblState);

            void UpdateCheckBoxState(CheckBox cb)
            {
                string state = cb.CheckState.ToString();
                lblState.Text = $"{cb.Text} 当前状态: {state}";
            }

            pnlDemo.Controls.Add(panel);
        }

        private void ShowRadioButtonDemo()
        {
            FlowLayoutPanel panel = CreateBasePanel();

            GroupBox group = new GroupBox { Text = "单选组", Width = 300, Height = 120 };
            RadioButton rb1 = new RadioButton { Text = "选项一", Location = new Point(10, 20), Checked = true };
            RadioButton rb2 = new RadioButton { Text = "选项二", Location = new Point(10, 50) };
            RadioButton rb3 = new RadioButton { Text = "选项三", Location = new Point(10, 80) };
            group.Controls.AddRange(new Control[] { rb1, rb2, rb3 });

            rb1.CheckedChanged += (s, e) => ShowRadioSelection(rb1);
            rb2.CheckedChanged += (s, e) => ShowRadioSelection(rb2);
            rb3.CheckedChanged += (s, e) => ShowRadioSelection(rb3);

            panel.Controls.Add(group);
            panel.Controls.Add(new Label { Text = "点击单选按钮查看选中项", AutoSize = true });

            void ShowRadioSelection(RadioButton rb)
            {
                if (rb.Checked)
                    MessageBox.Show($"选中了: {rb.Text}", "RadioButton演示");
            }

            pnlDemo.Controls.Add(panel);
        }

        private void ShowLabelDemo()
        {
            FlowLayoutPanel panel = CreateBasePanel();
            panel.Controls.Add(new Label { Text = "普通文本标签", AutoSize = true });
            panel.Controls.Add(new Label { Text = "带边框的标签", BorderStyle = BorderStyle.FixedSingle, AutoSize = true });
            panel.Controls.Add(new Label { Text = "自动换行的标签，宽度150，内容较长时会自动折行。", Width = 150, AutoSize = false });
            Label linkStyle = new Label { Text = "仿链接样式标签", ForeColor = Color.Blue, Font = new Font("宋体", 9, FontStyle.Underline), Cursor = Cursors.Hand };
            linkStyle.Click += (s, e) => MessageBox.Show("标签点击", "演示");
            panel.Controls.Add(WithLabel(linkStyle, "点击可响应事件"));
            pnlDemo.Controls.Add(panel);
        }

        private void ShowLinkLabelDemo()
        {
            FlowLayoutPanel panel = CreateBasePanel();
            LinkLabel ll1 = new LinkLabel { Text = "单击打开链接 (演示)", AutoSize = true };
            ll1.LinkClicked += (s, e) => MessageBox.Show("链接被点击 (实际可打开网址)", "LinkLabel");
            panel.Controls.Add(ll1);

            LinkLabel ll2 = new LinkLabel { Text = "多链接：第一部分 第二部分", AutoSize = true };
            ll2.Links.Add(0, 4, "link1");
            ll2.Links.Add(5, 4, "link2");
            ll2.LinkClicked += (s, e) => MessageBox.Show($"点击了链接: {e.Link.LinkData}", "多链接");
            panel.Controls.Add(ll2);
            pnlDemo.Controls.Add(panel);
        }

        private void ShowTextBoxDemo()
        {
            FlowLayoutPanel panel = CreateBasePanel();

            TextBox txtSingle = new TextBox { Text = "单行文本框", Width = 200 };
            txtSingle.TextChanged += (s, e) => Console.WriteLine($"文本变化: {txtSingle.Text}");
            panel.Controls.Add(WithLabel(txtSingle, "单行"));

            TextBox txtPassword = new TextBox { PasswordChar = '*', Width = 200 };
            panel.Controls.Add(WithLabel(txtPassword, "密码框"));

            TextBox txtMulti = new TextBox { Multiline = true, Height = 60, Width = 200, ScrollBars = ScrollBars.Vertical, Text = "多行\n文本框" };
            panel.Controls.Add(WithLabel(txtMulti, "多行"));

            TextBox txtReadOnly = new TextBox { Text = "只读内容", ReadOnly = true, Width = 200 };
            panel.Controls.Add(WithLabel(txtReadOnly, "只读"));

            pnlDemo.Controls.Add(panel);
        }

        private void ShowRichTextBoxDemo()
        {
            FlowLayoutPanel panel = CreateBasePanel();

            RichTextBox rtb = new RichTextBox { Height = 150, Width = 400 };
            rtb.AppendText("RichTextBox支持富文本格式。");
            rtb.Select(0, 10);
            rtb.SelectionColor = Color.Red;
            rtb.SelectionFont = new Font("宋体", 12, FontStyle.Bold);
            rtb.Select(11, 6);
            rtb.SelectionColor = Color.Blue;
            rtb.SelectionFont = new Font("隶书", 14, FontStyle.Underline);

            panel.Controls.Add(rtb);
            panel.Controls.Add(new Label { Text = "选中部分文字可改变颜色/字体", AutoSize = true });

            pnlDemo.Controls.Add(panel);
        }

        private void ShowNumericUpDownDemo()
        {
            FlowLayoutPanel panel = CreateBasePanel();

            NumericUpDown nud = new NumericUpDown { Minimum = 0, Maximum = 100, Value = 30, Width = 100 };
            Label lblValue = new Label { Text = $"当前值: {nud.Value}", AutoSize = true };
            nud.ValueChanged += (s, e) => lblValue.Text = $"当前值: {nud.Value}";

            panel.Controls.Add(WithLabel(nud, "数值选择 (0-100)"));
            panel.Controls.Add(lblValue);
            pnlDemo.Controls.Add(panel);
        }

        private void ShowPictureBoxDemo()
        {
            FlowLayoutPanel panel = CreateBasePanel();

            PictureBox pb = new PictureBox { Width = 100, Height = 100, BorderStyle = BorderStyle.FixedSingle, SizeMode = PictureBoxSizeMode.StretchImage };
            try
            {
                pb.Image = System.Drawing.SystemIcons.WinLogo.ToBitmap();
            }
            catch
            {
                pb.BackColor = Color.Gray;
            }
            panel.Controls.Add(WithLabel(pb, "PictureBox 显示图标"));
            pnlDemo.Controls.Add(panel);
        }

        private void ShowProgressBarDemo()
        {
            FlowLayoutPanel panel = CreateBasePanel();

            ProgressBar pb = new ProgressBar { Minimum = 0, Maximum = 100, Value = 45, Width = 300 };
            Label lbl = new Label { Text = "进度: 45%", AutoSize = true };

            Button btnInc = new Button { Text = "增加10", Width = 80 };
            btnInc.Click += (s, e) =>
            {
                if (pb.Value < pb.Maximum) pb.Value += 10;
                lbl.Text = $"进度: {pb.Value}%";
            };

            panel.Controls.Add(pb);
            panel.Controls.Add(lbl);
            panel.Controls.Add(btnInc);
            pnlDemo.Controls.Add(panel);
        }

        private void ShowTrackBarDemo()
        {
            FlowLayoutPanel panel = CreateBasePanel();

            TrackBar tb = new TrackBar { Minimum = 0, Maximum = 100, TickFrequency = 10, Width = 300 };
            Label lbl = new Label { Text = "滑块值: 50", AutoSize = true };
            tb.Value = 50;
            tb.ValueChanged += (s, e) => lbl.Text = $"滑块值: {tb.Value}";

            panel.Controls.Add(tb);
            panel.Controls.Add(lbl);
            pnlDemo.Controls.Add(panel);
        }

        private void ShowComboBoxDemo()
        {
            FlowLayoutPanel panel = CreateBasePanel();

            ComboBox cb = new ComboBox { Width = 150, DropDownStyle = ComboBoxStyle.DropDownList };
            cb.Items.AddRange(new object[] { "北京", "上海", "广州", "深圳" });
            cb.SelectedIndex = 0;

            ComboBox cbEditable = new ComboBox { Width = 150, DropDownStyle = ComboBoxStyle.DropDown };
            cbEditable.Items.AddRange(new object[] { "选项A", "选项B", "选项C" });

            Label lblSel = new Label { Text = $"选中: {cb.Text}", AutoSize = true };
            cb.SelectedIndexChanged += (s, e) => lblSel.Text = $"选中: {cb.Text}";

            panel.Controls.Add(WithLabel(cb, "下拉列表"));
            panel.Controls.Add(WithLabel(cbEditable, "可编辑下拉框"));
            panel.Controls.Add(lblSel);
            pnlDemo.Controls.Add(panel);
        }

        private void ShowListBoxDemo()
        {
            FlowLayoutPanel panel = CreateBasePanel();

            ListBox lb = new ListBox { Width = 150, Height = 100 };
            lb.Items.AddRange(new object[] { "C#", "Java", "Python", "C++", "JavaScript" });
            lb.SelectionMode = SelectionMode.One;

            Label lblSel = new Label { Text = "未选中", AutoSize = true };
            lb.SelectedIndexChanged += (s, e) =>
            {
                if (lb.SelectedItem != null)
                    lblSel.Text = $"选中: {lb.SelectedItem}";
            };

            panel.Controls.Add(WithLabel(lb, "ListBox"));
            panel.Controls.Add(lblSel);
            pnlDemo.Controls.Add(panel);
        }

        private void ShowCheckedListBoxDemo()
        {
            FlowLayoutPanel panel = CreateBasePanel();

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

            panel.Controls.Add(WithLabel(clb, "CheckedListBox (可多选)"));
            panel.Controls.Add(btnShow);
            pnlDemo.Controls.Add(panel);
        }

        private void ShowListViewDemo()
        {
            FlowLayoutPanel panel = CreateBasePanel();

            ListView lv = new ListView { Width = 350, Height = 150, View = View.Details, FullRowSelect = true, GridLines = true };
            lv.Columns.Add("姓名", 100);
            lv.Columns.Add("年龄", 60);
            lv.Columns.Add("城市", 120);

            lv.Items.Add(new ListViewItem(new[] { "张三", "25", "北京" }));
            lv.Items.Add(new ListViewItem(new[] { "李四", "30", "上海" }));
            lv.Items.Add(new ListViewItem(new[] { "王五", "28", "广州" }));

            panel.Controls.Add(WithLabel(lv, "ListView (详细信息视图)"));
            pnlDemo.Controls.Add(panel);
        }

        private void ShowTreeViewDemo()
        {
            FlowLayoutPanel panel = CreateBasePanel();

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

            panel.Controls.Add(WithLabel(tv, "TreeView 示例"));
            pnlDemo.Controls.Add(panel);
        }

        private void ShowDataGridViewDemo()
        {
            FlowLayoutPanel panel = CreateBasePanel();

            DataGridView dgv = new DataGridView { Width = 450, Height = 180, AllowUserToAddRows = false, ReadOnly = true };
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("产品", typeof(string));
            dt.Columns.Add("价格", typeof(decimal));
            dt.Rows.Add(1, "笔记本", 12.5m);
            dt.Rows.Add(2, "鼠标", 8.0m);
            dt.Rows.Add(3, "键盘", 22.0m);
            dgv.DataSource = dt;

            panel.Controls.Add(WithLabel(dgv, "DataGridView 数据绑定"));
            pnlDemo.Controls.Add(panel);
        }

        private void ShowMonthCalendarDemo()
        {
            FlowLayoutPanel panel = CreateBasePanel();

            MonthCalendar cal = new MonthCalendar { MaxSelectionCount = 7 };
            Label lblDate = new Label { Text = "选中范围: ", AutoSize = true };
            cal.DateChanged += (s, e) =>
            {
                lblDate.Text = $"选中范围: {cal.SelectionStart.ToShortDateString()} - {cal.SelectionEnd.ToShortDateString()}";
            };

            panel.Controls.Add(cal);
            panel.Controls.Add(lblDate);
            pnlDemo.Controls.Add(panel);
        }

        private void ShowDateTimePickerDemo()
        {
            FlowLayoutPanel panel = CreateBasePanel();

            DateTimePicker dtp = new DateTimePicker { Width = 200, Format = DateTimePickerFormat.Short };
            Label lbl = new Label { Text = $"选中日期: {dtp.Value.ToShortDateString()}", AutoSize = true };
            dtp.ValueChanged += (s, e) => lbl.Text = $"选中日期: {dtp.Value.ToShortDateString()}";

            panel.Controls.Add(WithLabel(dtp, "DateTimePicker"));
            panel.Controls.Add(lbl);
            pnlDemo.Controls.Add(panel);
        }

        private void ShowGroupBoxDemo()
        {
            FlowLayoutPanel panel = CreateBasePanel();

            GroupBox gb = new GroupBox { Text = "用户信息", Width = 300, Height = 150 };
            Label lblName = new Label { Text = "姓名:", Location = new Point(10, 20) };
            TextBox txtName = new TextBox { Location = new Point(60, 18), Width = 120 };
            Label lblAge = new Label { Text = "年龄:", Location = new Point(10, 50) };
            NumericUpDown nudAge = new NumericUpDown { Location = new Point(60, 48), Width = 60, Minimum = 0, Maximum = 120 };
            gb.Controls.AddRange(new Control[] { lblName, txtName, lblAge, nudAge });

            panel.Controls.Add(WithLabel(gb, "GroupBox 容器"));
            pnlDemo.Controls.Add(panel);
        }

        private void ShowPanelDemo()
        {
            FlowLayoutPanel panel = CreateBasePanel();

            Panel pnl = new Panel { BorderStyle = BorderStyle.FixedSingle, Width = 300, Height = 100, AutoScroll = true };
            pnl.Controls.Add(new Button { Text = "按钮1", Location = new Point(10, 10) });
            pnl.Controls.Add(new Button { Text = "按钮2", Location = new Point(10, 40) });
            pnl.Controls.Add(new Button { Text = "按钮3", Location = new Point(10, 70) });

            panel.Controls.Add(WithLabel(pnl, "Panel 带滚动条"));
            pnlDemo.Controls.Add(panel);
        }

        private void ShowTabControlDemo()
        {
            FlowLayoutPanel panel = CreateBasePanel();

            TabControl tc = new TabControl { Width = 400, Height = 200 };
            TabPage tp1 = new TabPage("页面1");
            tp1.Controls.Add(new Label { Text = "这是第一个标签页", AutoSize = true, Location = new Point(20, 20) });
            TabPage tp2 = new TabPage("页面2");
            tp2.Controls.Add(new Button { Text = "页面2的按钮", Location = new Point(20, 20) });
            tc.TabPages.Add(tp1);
            tc.TabPages.Add(tp2);

            panel.Controls.Add(WithLabel(tc, "TabControl 选项卡"));
            pnlDemo.Controls.Add(panel);
        }

        private void ShowSplitContainerDemo()
        {
            FlowLayoutPanel panel = CreateBasePanel();

            SplitContainer sc = new SplitContainer { Width = 450, Height = 150, Orientation = Orientation.Vertical };
            sc.Panel1.BackColor = Color.LightBlue;
            sc.Panel1.Controls.Add(new Label { Text = "左面板", AutoSize = true });
            sc.Panel2.BackColor = Color.LightCoral;
            sc.Panel2.Controls.Add(new Label { Text = "右面板", AutoSize = true });

            panel.Controls.Add(WithLabel(sc, "SplitContainer (可拖动分隔条)"));
            pnlDemo.Controls.Add(panel);
        }

        private void ShowFlowLayoutPanelDemo()
        {
            FlowLayoutPanel panel = CreateBasePanel();

            FlowLayoutPanel flp = new FlowLayoutPanel { Width = 400, Height = 120, BorderStyle = BorderStyle.FixedSingle, AutoScroll = true };
            flp.Controls.Add(new Button { Text = "按钮1" });
            flp.Controls.Add(new Button { Text = "按钮2" });
            flp.Controls.Add(new Button { Text = "按钮3" });
            flp.Controls.Add(new Button { Text = "按钮4" });
            flp.Controls.Add(new Button { Text = "按钮5" });

            panel.Controls.Add(WithLabel(flp, "FlowLayoutPanel 自动排列"));
            pnlDemo.Controls.Add(panel);
        }

        private void ShowTableLayoutPanelDemo()
        {
            FlowLayoutPanel panel = CreateBasePanel();

            TableLayoutPanel tlp = new TableLayoutPanel { Width = 300, Height = 150, ColumnCount = 2, RowCount = 2, BorderStyle = BorderStyle.FixedSingle };
            tlp.Controls.Add(new Label { Text = "行1列1", BackColor = Color.LightYellow }, 0, 0);
            tlp.Controls.Add(new Label { Text = "行1列2", BackColor = Color.LightGreen }, 1, 0);
            tlp.Controls.Add(new Button { Text = "行2列1" }, 0, 1);
            tlp.Controls.Add(new Button { Text = "行2列2" }, 1, 1);

            panel.Controls.Add(WithLabel(tlp, "TableLayoutPanel 表格布局"));
            pnlDemo.Controls.Add(panel);
        }

        private void ShowMenuStripDemo()
        {
            FlowLayoutPanel panel = CreateBasePanel();

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
            panel.Controls.Add(WithLabel(p, "MenuStrip 菜单栏 (点击菜单测试)"));
            pnlDemo.Controls.Add(panel);
        }

        private void ShowContextMenuStripDemo()
        {
            FlowLayoutPanel panel = CreateBasePanel();

            ContextMenuStrip cms = new ContextMenuStrip();
            cms.Items.Add("复制", null, (s, e) => MessageBox.Show("复制操作", "右键菜单"));
            cms.Items.Add("粘贴", null, (s, e) => MessageBox.Show("粘贴操作", "右键菜单"));

            Button btn = new Button { Text = "右键点击我", Width = 120, ContextMenuStrip = cms };
            panel.Controls.Add(WithLabel(btn, "按钮绑定了右键菜单"));
            pnlDemo.Controls.Add(panel);
        }

        private void ShowStatusStripDemo()
        {
            FlowLayoutPanel panel = CreateBasePanel();

            StatusStrip ss = new StatusStrip { Dock = DockStyle.Bottom };
            ss.Items.Add("就绪");
            ss.Items.Add(new ToolStripStatusLabel { Text = "|" });
            ss.Items.Add(new ToolStripStatusLabel { Text = "用户: Demo" });

            Panel p = new Panel { Width = 400, Height = 60, BorderStyle = BorderStyle.FixedSingle };
            p.Controls.Add(ss);
            panel.Controls.Add(WithLabel(p, "StatusStrip 状态栏"));
            pnlDemo.Controls.Add(panel);
        }

        private void ShowToolStripDemo()
        {
            FlowLayoutPanel panel = CreateBasePanel();

            ToolStrip ts = new ToolStrip { Dock = DockStyle.Top };
            ts.Items.Add("新建", null, (s, e) => MessageBox.Show("新建", "工具栏"));
            ts.Items.Add("打开", null, (s, e) => MessageBox.Show("打开", "工具栏"));
            ts.Items.Add(new ToolStripSeparator());
            ts.Items.Add(new ToolStripButton { Text = "保存", DisplayStyle = ToolStripItemDisplayStyle.Text });

            Panel p = new Panel { Width = 400, Height = 60, BorderStyle = BorderStyle.FixedSingle };
            p.Controls.Add(ts);
            panel.Controls.Add(WithLabel(p, "ToolStrip 工具栏"));
            pnlDemo.Controls.Add(panel);
        }

        private void ShowNotifyIconDemo()
        {
            FlowLayoutPanel panel = CreateBasePanel();

            NotifyIcon ni = new NotifyIcon(componentsContainer);
            ni.Icon = SystemIcons.Application;
            ni.Text = "NotifyIcon演示";
            ni.Visible = true;

            Button btnShow = new Button { Text = "显示气球提示", Width = 120 };
            btnShow.Click += (s, e) => ni.ShowBalloonTip(3000, "通知", "这是NotifyIcon的气球提示", ToolTipIcon.Info);

            Button btnHide = new Button { Text = "隐藏图标", Width = 120 };
            btnHide.Click += (s, e) => ni.Visible = false;

            panel.Controls.Add(WithLabel(btnShow, "点击显示托盘通知"));
            panel.Controls.Add(btnHide);
            panel.Controls.Add(new Label { Text = "注意：托盘区会出现应用图标", AutoSize = true });

            componentsContainer.Add(ni);
            pnlDemo.Controls.Add(panel);
        }

        private void ShowTimerDemo()
        {
            FlowLayoutPanel panel = CreateBasePanel();

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

            panel.Controls.Add(WithLabel(pb, "ProgressBar (每秒+1)"));
            panel.Controls.Add(lbl);
            panel.Controls.Add(btnStart);
            panel.Controls.Add(btnStop);
            pnlDemo.Controls.Add(panel);
        }

        private void ShowToolTipDemo()
        {
            FlowLayoutPanel panel = CreateBasePanel();

            ToolTip tt = new ToolTip(componentsContainer);
            Button btn = new Button { Text = "鼠标悬停我", Width = 120 };
            tt.SetToolTip(btn, "这是ToolTip提示文本");

            panel.Controls.Add(WithLabel(btn, "按钮设置了工具提示"));
            pnlDemo.Controls.Add(panel);
        }

        private void ShowWebBrowserDemo()
        {
            FlowLayoutPanel panel = CreateBasePanel();

            WebBrowser wb = new WebBrowser { Width = 500, Height = 300 };
            wb.Navigate("about:blank");
            //wb.Document.Write("<html><body><h2>WebBrowser 演示</h2><p>这是一个简单的HTML内容。</p></body></html>");

            panel.Controls.Add(WithLabel(wb, "WebBrowser 显示HTML"));
            pnlDemo.Controls.Add(panel);
        }

        private void ShowDefaultMessage(string controlName)
        {
            Label lbl = new Label { Text = $"控件 [{controlName}] 的演示正在构建中。", AutoSize = true, ForeColor = Color.Red, Font = new Font("宋体", 12) };
            pnlDemo.Controls.Add(lbl);
        }

        #endregion

        #region 辅助方法

        private FlowLayoutPanel CreateBasePanel()
        {
            return new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                AutoScroll = true,
                Padding = new Padding(10),
                WrapContents = false
            };
        }

        private Panel WithLabel(Control ctrl, string description)
        {
            Panel p = new Panel { Height = ctrl.Height + 30, Width = 500 };
            Label lbl = new Label { Text = description, AutoSize = true, Location = new Point(10, 5) };
            ctrl.Location = new Point(10, 25);
            p.Controls.Add(lbl);
            p.Controls.Add(ctrl);
            return p;
        }

        #endregion


    }
}
