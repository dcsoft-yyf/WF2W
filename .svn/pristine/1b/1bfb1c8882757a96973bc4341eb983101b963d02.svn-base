using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsControlsDemo
{
    public partial class MenuForm : Form
    {
        public MenuForm()
        {
            InitializeComponent();
            InitializeMenu();
        }

        private void InitializeMenu()
        {
            // 创建主菜单栏
            MenuStrip menuStrip = new MenuStrip();
            menuStrip.BackColor = SystemColors.Control;
            menuStrip.Dock = DockStyle.Top;

            // ========== 文件菜单 ==========
            ToolStripMenuItem fileMenu = new ToolStripMenuItem("文件(&F)");

            // 新建子菜单
            ToolStripMenuItem newMenu = new ToolStripMenuItem("新建(&N)");
#if !WF2W
            newMenu.Image = SystemIcons.Application.ToBitmap();
#endif
            newMenu.ShortcutKeys = Keys.Control | Keys.N;
#if WF2W
            newMenu.ClickAsync += async delegate (object sender, EventArgs e)
            {
                ShowMessage("新建文件");
            };
#else
            newMenu.Click += (s, e) => ShowMessage("新建文件");
#endif

            // 打开子菜单
            ToolStripMenuItem openMenu = new ToolStripMenuItem("打开(&O)");
#if !WF2W
            openMenu.Image = SystemIcons.Information.ToBitmap();
#endif
            openMenu.ShortcutKeys = Keys.Control | Keys.O;
#if WF2W
            openMenu.ClickAsync += async delegate (object sender, EventArgs e)
            {
                ShowMessage("打开文件");
            };
#else
            openMenu.Click += (s, e) => ShowMessage("打开文件");
#endif

            // 最近使用的文件（二级菜单）
            ToolStripMenuItem recentMenu = new ToolStripMenuItem("最近使用的文件");
            recentMenu.DropDownItems.Add("文件1.txt", null, (s, e) => ShowMessage("打开文件1.txt"));
            recentMenu.DropDownItems.Add("文件2.doc", null, (s, e) => ShowMessage("打开文件2.doc"));
            recentMenu.DropDownItems.Add("文件3.xls", null, (s, e) => ShowMessage("打开文件3.xls"));

            // 分隔线
            ToolStripSeparator separator1 = new ToolStripSeparator();

            // 保存菜单
            ToolStripMenuItem saveMenu = new ToolStripMenuItem("保存(&S)");
#if !WF2W
            saveMenu.Image = SystemIcons.Save.ToBitmap();
#endif
            saveMenu.ShortcutKeys = Keys.Control | Keys.S;
#if WF2W
            saveMenu.ClickAsync += async delegate (object sender, EventArgs e)
            {
                ShowMessage("保存文件");
            };
#else
            saveMenu.Click += (s, e) => ShowMessage("保存文件");
#endif

            // 另存为菜单
            ToolStripMenuItem saveAsMenu = new ToolStripMenuItem("另存为(&A)");
#if WF2W
            saveAsMenu.ClickAsync += async delegate (object sender, EventArgs e)
            {
                ShowMessage("另存为文件");
            };
#else
            saveAsMenu.Click += (s, e) => ShowMessage("另存为文件");
#endif

            // 分隔线
            ToolStripSeparator separator2 = new ToolStripSeparator();

            // 退出菜单
            ToolStripMenuItem exitMenu = new ToolStripMenuItem("退出(&X)");
#if !WF2W
            exitMenu.Image = SystemIcons.Error.ToBitmap();
#endif
            exitMenu.ShortcutKeys = Keys.Alt | Keys.F4;
#if WF2W
            exitMenu.ClickAsync += async delegate (object sender, EventArgs e)
            {
                Application.Exit();
            };
#else
            exitMenu.Click += (s, e) => Application.Exit();
#endif

            // 添加所有子菜单到文件菜单
            fileMenu.DropDownItems.AddRange(new ToolStripItem[] {
                newMenu, openMenu, recentMenu, separator1, saveMenu, saveAsMenu, separator2, exitMenu
            });

            // ========== 编辑菜单 ==========
            ToolStripMenuItem editMenu = new ToolStripMenuItem("编辑(&E)");

            ToolStripMenuItem undoMenu = new ToolStripMenuItem("撤销(&U)");
            undoMenu.ShortcutKeys = Keys.Control | Keys.Z;
#if WF2W
            undoMenu.ClickAsync += async delegate (object sender, EventArgs e)
            {
                ShowMessage("撤销操作");
            };
#else
            undoMenu.Click += (s, e) => ShowMessage("撤销操作");
#endif

            ToolStripMenuItem redoMenu = new ToolStripMenuItem("重做(&R)");
            redoMenu.ShortcutKeys = Keys.Control | Keys.Y;
#if WF2W
            redoMenu.ClickAsync += async delegate (object sender, EventArgs e)
            {
                ShowMessage("重做操作");
            };
#else
            redoMenu.Click += (s, e) => ShowMessage("重做操作");
#endif

            ToolStripSeparator separator3 = new ToolStripSeparator();

            ToolStripMenuItem cutMenu = new ToolStripMenuItem("剪切(&T)");
            cutMenu.ShortcutKeys = Keys.Control | Keys.X;
#if WF2W
            cutMenu.ClickAsync += async delegate (object sender, EventArgs e)
            {
                ShowMessage("剪切");
            };
#else
            cutMenu.Click += (s, e) => ShowMessage("剪切");
#endif

            ToolStripMenuItem copyMenu = new ToolStripMenuItem("复制(&C)");
            copyMenu.ShortcutKeys = Keys.Control | Keys.C;
#if WF2W
            copyMenu.ClickAsync += async delegate (object sender, EventArgs e)
            {
                ShowMessage("复制");
            };
#else
            copyMenu.Click += (s, e) => ShowMessage("复制");
#endif

            ToolStripMenuItem pasteMenu = new ToolStripMenuItem("粘贴(&P)");
            pasteMenu.ShortcutKeys = Keys.Control | Keys.V;
#if WF2W
            pasteMenu.ClickAsync += async delegate (object sender, EventArgs e)
            {
                ShowMessage("粘贴");
            };
#else
            pasteMenu.Click += (s, e) => ShowMessage("粘贴");
#endif

            ToolStripMenuItem deleteMenu = new ToolStripMenuItem("删除(&D)");
            deleteMenu.ShortcutKeys = Keys.Delete;
#if WF2W
            deleteMenu.ClickAsync += async delegate (object sender, EventArgs e)
            {
                ShowMessage("删除");
            };
#else
            deleteMenu.Click += (s, e) => ShowMessage("删除");
#endif

            editMenu.DropDownItems.AddRange(new ToolStripItem[] {
                undoMenu, redoMenu, separator3, cutMenu, copyMenu, pasteMenu, deleteMenu
            });

            // ========== 视图菜单 ==========
            ToolStripMenuItem viewMenu = new ToolStripMenuItem("视图(&V)");

            // 工具栏子菜单（带复选框）
            ToolStripMenuItem toolbarMenu = new ToolStripMenuItem("工具栏(&T)");
            toolbarMenu.CheckOnClick = true;
            toolbarMenu.Checked = true;
#if WF2W
            toolbarMenu.ClickAsync += async delegate (object sender, EventArgs e)
            {
                ShowMessage("切换工具栏显示");
            };
#else
            toolbarMenu.Click += (s, e) => ShowMessage("切换工具栏显示");
#endif

            // 状态栏子菜单（带复选框）
            ToolStripMenuItem statusBarMenu = new ToolStripMenuItem("状态栏(&S)");
            statusBarMenu.CheckOnClick = true;
            statusBarMenu.Checked = true;
#if WF2W
            statusBarMenu.ClickAsync += async delegate (object sender, EventArgs e)
            {
                ShowMessage("切换状态栏显示");
            };
#else
            statusBarMenu.Click += (s, e) => ShowMessage("切换状态栏显示");
#endif

            // 缩放子菜单（三级菜单示例）
            ToolStripMenuItem zoomMenu = new ToolStripMenuItem("缩放(&Z)");

            ToolStripMenuItem zoomInMenu = new ToolStripMenuItem("放大(&I)");
            zoomInMenu.ShortcutKeys = Keys.Control | Keys.Oemplus;
#if WF2W
            zoomInMenu.ClickAsync += async delegate (object sender, EventArgs e)
            {
                ShowMessage("放大视图");
            };
#else
            zoomInMenu.Click += (s, e) => ShowMessage("放大视图");
#endif

            ToolStripMenuItem zoomOutMenu = new ToolStripMenuItem("缩小(&O)");
            zoomOutMenu.ShortcutKeys = Keys.Control | Keys.OemMinus;
#if WF2W
            zoomOutMenu.ClickAsync += async delegate (object sender, EventArgs e)
            {
                ShowMessage("缩小视图");
            };
#else
            zoomOutMenu.Click += (s, e) => ShowMessage("缩小视图");
#endif

            ToolStripMenuItem zoom100Menu = new ToolStripMenuItem("100%");
            zoom100Menu.ShortcutKeys = Keys.Control | Keys.D0;
#if WF2W
            zoom100Menu.ClickAsync += async delegate (object sender, EventArgs e)
            {
                ShowMessage("重置为100%");
            };
#else
            zoom100Menu.Click += (s, e) => ShowMessage("重置为100%");
#endif
            zoomMenu.DropDownItems.AddRange(new ToolStripItem[] {
                zoomInMenu, zoomOutMenu, zoom100Menu
            });

            viewMenu.DropDownItems.AddRange(new ToolStripItem[] {
                toolbarMenu, statusBarMenu, zoomMenu
            });

            // ========== 工具菜单 ==========
            ToolStripMenuItem toolsMenu = new ToolStripMenuItem("工具(&T)");

            // 选项菜单
            ToolStripMenuItem optionsMenu = new ToolStripMenuItem("选项(&O)");
#if WF2W
            optionsMenu.ClickAsync += async delegate (object sender, EventArgs e)
            {
                ShowOptionsDialog();
            };
#else
            optionsMenu.Click += (s, e) => ShowOptionsDialog();
#endif

            // 设置菜单（带二级菜单）
            ToolStripMenuItem settingsMenu = new ToolStripMenuItem("设置(&S)");

            ToolStripMenuItem userSettingsMenu = new ToolStripMenuItem("用户设置");
#if WF2W
            userSettingsMenu.ClickAsync += async delegate (object sender, EventArgs e)
            {
                ShowMessage("用户设置");
            };
#else
            userSettingsMenu.Click += (s, e) => ShowMessage("用户设置");
#endif

            ToolStripMenuItem systemSettingsMenu = new ToolStripMenuItem("系统设置");

#if WF2W
            systemSettingsMenu.ClickAsync += async delegate (object sender, EventArgs e)
            {
                ShowMessage("系统设置");
            };
#else
            systemSettingsMenu.Click += (s, e) => ShowMessage("系统设置");
#endif

            settingsMenu.DropDownItems.AddRange(new ToolStripItem[] {
                userSettingsMenu, systemSettingsMenu
            });

            toolsMenu.DropDownItems.AddRange(new ToolStripItem[] {
                optionsMenu, settingsMenu
            });

            // ========== 帮助菜单 ==========
            ToolStripMenuItem helpMenu = new ToolStripMenuItem("帮助(&H)");

            ToolStripMenuItem helpTopicsMenu = new ToolStripMenuItem("帮助主题(&H)");
            helpTopicsMenu.ShortcutKeys = Keys.F1;

#if WF2W
            helpTopicsMenu.ClickAsync += async delegate (object sender, EventArgs e)
            {
                ShowMessage("打开帮助主题");
            };
#else

            helpTopicsMenu.Click += (s, e) => ShowMessage("打开帮助主题");
#endif

            ToolStripSeparator separator4 = new ToolStripSeparator();

            ToolStripMenuItem aboutMenu = new ToolStripMenuItem("关于(&A)");

#if WF2W
            aboutMenu.ClickAsync += async delegate (object sender, EventArgs e)
            {
                using ( AboutDialog dialog = new AboutDialog())
                {
                    await dialog.ShowDialog(this);
                }
            };
#else

            aboutMenu.Click += (s, e) => ShowAboutDialog();
#endif

            helpMenu.DropDownItems.AddRange(new ToolStripItem[] {
                helpTopicsMenu, separator4, aboutMenu
            });

            // 添加所有主菜单到菜单栏
            menuStrip.Items.AddRange(new ToolStripItem[] {
                fileMenu, editMenu, viewMenu, toolsMenu, helpMenu
            });

            // 添加菜单栏到窗体
            this.MainMenuStrip = menuStrip;
            this.Controls.Add(menuStrip);
        }

        private void ShowMessage(string message)
        {
            MessageBox.Show($"{message} - 菜单演示", "提示",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async Task ShowOptionsDialog()
        {
            using (OptionsDialog dialog = new OptionsDialog())
            {
               await dialog.ShowDialog(this);
            }
        }

        private void ShowAboutDialog()
        {
            using (AboutDialog dialog = new AboutDialog())
            {
                dialog.ShowDialog(this);
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "Form1";
            this.Text = "多级菜单演示程序";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
        }
    }
}