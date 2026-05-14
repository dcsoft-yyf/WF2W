using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.IO;
namespace System
{
    /// <summary>
    /// 异步事件处理程序委托
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <returns></returns>
    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
    public delegate Task EventHandlerAsync(object sender, EventArgs e);
}
namespace System.Windows.Forms
{
    /// <summary>
    /// 异步键盘事件处理程序委托
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <returns></returns>
    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
    public delegate Task KeyEventHandlerAsync(object sender, KeyEventArgs e);
    /// <summary>
    /// 异步键盘按下事件处理程序委托
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <returns></returns>
    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
    public delegate Task KeyPressEventHandlerAsync(object sender, KeyPressEventArgs e);
    /// <summary>
    /// 异步鼠标事件处理程序委托
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <returns></returns>
    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
    public delegate Task MouseEventHandlerAsync(object sender, MouseEventArgs e);
}

namespace DCSoft
{
    internal static class DCWinFormHelper
    {
        ///// <summary>
        ///// 将ComboBox的公开属性写入Json对象
        ///// </summary>
        ///// <param name="menu">菜单对象</param>
        ///// <param name="obj">JSON对象</param>
        //public static void WritePropertyValueToJson(System.Windows.Forms.ComboBox cboBox, JsonObject obj)
        //{
        //    if (cboBox == null)
        //    {
        //        throw new ArgumentNullException("cbobox");
        //    }
        //    if (obj == null)
        //    {
        //        throw new ArgumentNullException("obj");
        //    }
        //    obj["Text"] = cboBox.Text;
        //    obj["SelectedIndex"] = cboBox.SelectedIndex;
        //    obj["DropDownStyle"] = cboBox.DropDownStyle.ToString();
        //    obj["DropDownHeight"] = cboBox.DropDownHeight;
        //    obj["DropDownWidth"] = cboBox.DropDownWidth;
        //    obj["MaxDropDownItems"] = cboBox.MaxDropDownItems;
        //    obj["IntegralHeight"] = cboBox.IntegralHeight;
        //    obj["Sorted"] = cboBox.Sorted;
        //    obj["Enabled"] = cboBox.Enabled;
        //    obj["Visible"] = cboBox.Visible;
        //    obj["Items"] = new JsonArray();
        //    foreach( var item in cboBox.Items )
        //    {
        //        obj["Items"].AsArray().Add(item.ToString());
        //    }
        //}
        ///// <summary>
        ///// 将ContainerControl的公开属性写入Json对象
        ///// </summary>
        ///// <param name="menu">菜单对象</param>
        ///// <param name="obj">JSON对象</param>
        //public static void WritePropertyValueToJson(System.Windows.Forms.ContainerControl container, JsonObject obj)
        //{
        //    if (container == null)
        //    {
        //        throw new ArgumentNullException("container");
        //    }
        //    if (obj == null)
        //    {
        //        throw new ArgumentNullException("obj");
        //    }
        //    obj["AutoValidate"] = container.AutoValidate.ToString() ;
        //    obj["AutoScaleMode"] = container.AutoScaleMode.ToString();
        //    obj["ActiveControl"] = container.ActiveControl.Name;
        //}
        ///// <summary>
        ///// 将Label的公开属性写入Json对象
        ///// </summary>
        ///// <param name="menu">菜单对象</param>
        ///// <param name="obj">JSON对象</param>
        //public static void WritePropertyValueToJson(System.Windows.Forms.Label label, JsonObject obj)
        //{
        //    if (label == null)
        //    {
        //        throw new ArgumentNullException("label");
        //    }
        //    if (obj == null)
        //    {
        //        throw new ArgumentNullException("obj");
        //    }
        //    obj["Text"] = label.Text;
        //    obj["BorderStyle"] = label.BorderStyle.ToString();
        //    obj["FlatStyle"] = label.FlatStyle.ToString();
        //    obj["TextAlign"] = label.TextAlign.ToString();
        //    obj["AutoEllipsis"] = label.AutoEllipsis;
        //}
        ///// <summary>
        ///// 将Panel的公开属性写入Json对象
        ///// </summary>
        ///// <param name="menu">菜单对象</param>
        ///// <param name="obj">JSON对象</param>
        //public static void WritePropertyValueToJson(System.Windows.Forms.Panel panel, JsonObject obj)
        //{
        //    if (panel == null)
        //    {
        //        throw new ArgumentNullException("panel");
        //    }
        //    if (obj == null)
        //    {
        //        throw new ArgumentNullException("obj");
        //    }
        //    obj["AutoSize"] = panel.AutoSize;
        //    obj["BorderStyle"] = panel.BorderStyle.ToString();
        //}
        ///// <summary>
        ///// 将PictureBox的公开属性写入Json对象
        ///// </summary>
        ///// <param name="menu">菜单对象</param>
        ///// <param name="obj">JSON对象</param>
        //public static void WritePropertyValueToJson(System.Windows.Forms.PictureBox picBox, JsonObject obj)
        //{
        //    if (picBox == null)
        //    {
        //        throw new ArgumentNullException("picBox");
        //    }
        //    if (obj == null)
        //    {
        //        throw new ArgumentNullException("obj");
        //    }
        //    obj["Image"] = ParseImageToHtmlImageSrc(picBox.Image);
        //    obj["SizeMode"] = picBox.SizeMode.ToString();
        //}
        ///// <summary>
        ///// 将ScrollableControl的公开属性写入Json对象
        ///// </summary>
        ///// <param name="menu">菜单对象</param>
        ///// <param name="obj">JSON对象</param>
        //public static void WritePropertyValueToJson(System.Windows.Forms.ScrollableControl scrollctl, JsonObject obj)
        //{
        //    if (scrollctl == null)
        //    {
        //        throw new ArgumentNullException("scrollctl");
        //    }
        //    if (obj == null)
        //    {
        //        throw new ArgumentNullException("obj");
        //    }
        //    obj["AutoScroll"] = scrollctl.AutoScroll;
        //    obj["AutoScrollMargin"] = ParseSizeToJSONObj(scrollctl.AutoScrollMargin);
        //    obj["AutoScrollMinSize"] = ParseSizeToJSONObj(scrollctl.AutoScrollMinSize);
        //    obj["Padding"] = ParsePaddingToJSONObj(scrollctl.Padding);
        //    obj["HorizontalScroll"] = ParseVHScrollPropertiesToJSONObj(scrollctl.HorizontalScroll);
        //    obj["VerticalScroll"] = ParseVHScrollPropertiesToJSONObj(scrollctl.VerticalScroll); ;
        //    obj["Width"] = scrollctl.Width;
        //    obj["Height"] = scrollctl.Height;
        //    obj["Text"] = scrollctl.Text;
        //}
        ///// <summary>
        ///// 将TextBox的公开属性写入Json对象
        ///// </summary>
        ///// <param name="menu">菜单对象</param>
        ///// <param name="obj">JSON对象</param>
        //public static void WritePropertyValueToJson(System.Windows.Forms.TextBox txtBox, JsonObject obj)
        //{
        //    if (txtBox == null)
        //    {
        //        throw new ArgumentNullException("txtBox");
        //    }
        //    if (obj == null)
        //    {
        //        throw new ArgumentNullException("obj");
        //    }
        //    obj["Text"] = txtBox.Text;
        //    obj["ReadOnly"] = txtBox.ReadOnly;
        //    obj["AcceptsReturn"] = txtBox.AcceptsReturn;
        //    obj["MaxLength"] = txtBox.MaxLength;
        //    obj["PasswordChar"] = txtBox.PasswordChar.ToString();
        //    obj["UseSystemPasswordChar"] = txtBox.UseSystemPasswordChar;
        //    obj["AutoCompleteSource"] = txtBox.AutoCompleteSource.ToString();
        //    obj["AutoCompleteMode"] = txtBox.AutoCompleteMode.ToString();
        //    obj["CharacterCasing"] = txtBox.CharacterCasing.ToString();
        //    obj["TextAlign"] = txtBox.TextAlign.ToString();
        //    obj["WordWrap"] = txtBox.WordWrap;
        //    obj["ScrollBars"] = txtBox.ScrollBars.ToString();
        //    //obj["PlaceholderText"] = txtBox.;没有这个属性
        //    obj["SelectionStart"] = txtBox.SelectionStart;
        //    obj["SelectionLength"] = txtBox.SelectionLength;
        //    obj["Multiline"] = txtBox.Multiline;
        //    obj["Enabled"] = txtBox.Enabled;
        //}
        ///// <summary>
        ///// 将ToolStrip的公开属性写入Json对象
        ///// </summary>
        ///// <param name="menu">菜单对象</param>
        ///// <param name="obj">JSON对象</param>
        //public static void WritePropertyValueToJson(System.Windows.Forms.ToolStrip toolstrip, JsonObject obj)
        //{
        //    if (toolstrip == null)
        //    {
        //        throw new ArgumentNullException("toolstrip");
        //    }
        //    if (obj == null)
        //    {
        //        throw new ArgumentNullException("obj");
        //    }
        //    obj["Orientation"] = toolstrip.Orientation.ToString();
        //    obj["Dock"] = toolstrip.Dock.ToString();
        //    obj["GripStyle"] = toolstrip.GripStyle.ToString();
        //    obj["BackColor"] = ParseColorToRGBAString(toolstrip.BackColor);
        //    obj["ForeColor"] = ParseColorToRGBAString(toolstrip.ForeColor); ;
        //    obj["Padding"] = ParsePaddingToJSONObj(toolstrip.Padding);
        //    obj["Visible"] = toolstrip.Visible;
        //    obj["Enabled"] = toolstrip.Enabled;
        //    obj["Width"] = toolstrip.Width;
        //    obj["Height"] = toolstrip.Height;
        //    obj["Left"] = toolstrip.Left;
        //    obj["Top"] = toolstrip.Top;
        //    obj["Font"] = ParseFontToJSONObj(toolstrip.Font);
        //    obj["Handle"] = toolstrip.Handle;
        //    JsonArray arr = new JsonArray();
        //    foreach(ToolStripItem item in toolstrip.Items)
        //    {
        //        JsonObject o = new JsonObject();
        //        WritePropertyValueToJson(item, o);
        //        arr.Add(o);
        //    }
        //    obj["Items"] = arr;
        //}
        ///// <summary>
        ///// 将UserControl的公开属性写入Json对象
        ///// </summary>
        ///// <param name="menu">菜单对象</param>
        ///// <param name="obj">JSON对象</param>
        //public static void WritePropertyValueToJson(System.Windows.Forms.UserControl userCtl, JsonObject obj)
        //{
        //    if (userCtl == null)
        //    {
        //        throw new ArgumentNullException("userCtl");
        //    }
        //    if (obj == null)
        //    {
        //        throw new ArgumentNullException("obj");
        //    }
        //    obj["AutoSize"] = userCtl.AutoSize;
        //    obj["AutoSizeMode"] = userCtl.AutoSizeMode.ToString();
        //    obj["AutoValidate"] = userCtl.AutoValidate.ToString();
        //    obj["BorderStyle"] = userCtl.BorderStyle.ToString();
        //}
        ///// <summary>
        ///// 将按钮的公开属性写入Json对象
        ///// </summary>
        ///// <param name="menu">菜单对象</param>
        ///// <param name="obj">JSON对象</param>
        //public static void WritePropertyValueToJson(System.Windows.Forms.Button btn, JsonObject obj)
        //{
        //    if (btn == null)
        //    {
        //        throw new ArgumentNullException("btn");
        //    }
        //    if (obj == null)
        //    {
        //        throw new ArgumentNullException("obj");
        //    }
        //    obj["Text"] = btn.Text;
        //    obj["Image"] = ParseImageToHtmlImageSrc(btn.Image);
        //    obj["ImageAlign"] = btn.ImageAlign.ToString();
        //    obj["TextAlign"] = btn.TextAlign.ToString();
        //    obj["FlatStyle"] = btn.FlatStyle.ToString();
        //    //obj["BorderStyle"] = null; 按钮元素没有BorderStyle
        //    obj["AutoSize"] = btn.AutoSize;
        //}
        /// <summary>
        /// 将菜单项的公开属性写入Json对象
        /// </summary>
        /// <param name="menu">菜单对象</param>
        /// <param name="obj">JSON对象</param>
        public static void WritePropertyValueToJson(System.Windows.Forms.MenuItem menu, JsonObject obj)
        {
            if(menu == null )
            {
                throw new ArgumentNullException("menu");
            }
            if(obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            string text = menu.Text ?? string.Empty;
            bool isSeparator = text == "-";
            obj["DCHandle"] = menu.Handle.ToInt32();
            obj["Text"] = text;
            obj["IsSeparator"] = isSeparator;
            obj["Enabled"] = menu.Enabled;
            obj["Checked"] = menu.Checked;
            obj["Shortcut"] = menu.Shortcut != Shortcut.None ? menu.Shortcut.ToString() : null;
            obj["ShortcutKeyDisplayString"] = menu.ShowShortcut && menu.Shortcut != Shortcut.None ? menu.Shortcut.ToString() : null;
            obj["HasChildren"] = menu.IsParent;
        }
        /// <summary>
        /// 将ToolStripItem的公开属性写入Json对象
        /// </summary>
        /// <param name="item">对象</param>
        /// <param name="json">JSON对象</param>
        /// <remarks>
        /// 支持导出ToolStripButton,ToolStripComboBox,ToolStripDropDown,ToolStripDropDownButton
        /// description ToolStripLabel, ToolStripSplitButton, ToolStripTextBox的所有的公开的可读写属性</remarks>
        public static void WritePropertyValueToJson(ToolStripItem item, JsonObject json, bool autoRemovePropertyLog)
        {
            if (item == null || json == null)
            {
                return;
            }

            json["Handle"] = item.Handle.ToInt32();
            json["DCType"] = item.GetType().Name;
            json["DCName"] = item.Name;

            json["Text"] = item.Text ?? string.Empty;
            json["Enabled"] = item.Enabled;
            json["Visible"] = item.Visible;

            if (item is ToolStripMenuItem menuItem)
            {
                json["Checked"] = menuItem.Checked;
                json["ShortcutKeyDisplayString"] =
                    string.IsNullOrEmpty(menuItem.ShortcutKeyDisplayString)
                        ? null
                        : menuItem.ShortcutKeyDisplayString;
            }

            if (item is ToolStripDropDownItem dropDownItem)
            {
                json["HasChildren"] = dropDownItem.DropDownItems != null && dropDownItem.DropDownItems.Count > 0;
            }
            else
            {
                json["HasChildren"] = false;
            }

            PropertyValueLogger.GetLoggedProperties(item, json, autoRemovePropertyLog);
            //JsonObject BuildSizeJson(Size size)
            //{
            //    return new JsonObject
            //    {
            //        ["Width"] = size.Width,
            //        ["Height"] = size.Height
            //    };
            //}
            //JsonObject BuildPaddingJson(Padding padding)
            //{
            //    return new JsonObject
            //    {
            //        ["Left"] = padding.Left,
            //        ["Top"] = padding.Top,
            //        ["Right"] = padding.Right,
            //        ["Bottom"] = padding.Bottom
            //    };
            //}

            //json["Name"] = item.Name;
            //json["Text"] = item.Text;
            //json["Enabled"] = item.Enabled;
            //json["Visible"] = item.Visible;
            //json["AutoSize"] = item.AutoSize;
            //json["Alignment"] = item.Alignment.ToString();
            //json["DisplayStyle"] = item.DisplayStyle.ToString();
            //json["ToolTipText"] = item.ToolTipText;
            //if (item.Tag != null)
            //{
            //    json["Tag"] = item.Tag.ToString();
            //}
            //json["Size"] = BuildSizeJson(item.Size);
            //json["Margin"] = BuildPaddingJson(item.Margin);
            //json["Padding"] = BuildPaddingJson(item.Padding);
            //json["ImageAlign"] = item.ImageAlign.ToString();
            //json["ImageScaling"] = item.ImageScaling.ToString();
            ////json["ImageIndex"] = item.ImageIndex;
            ////json["ImageKey"] = item.ImageKey;
            //json["ImageTransparentColor"] = ColorTranslator.ToHtml(item.ImageTransparentColor);
            //json["ForeColor"] = ColorTranslator.ToHtml(item.ForeColor);
            //json["BackColor"] = ColorTranslator.ToHtml(item.BackColor);
            //json["Overflow"] = item.Overflow.ToString();
            //json["RightToLeft"] = item.RightToLeft.ToString();
            //json["TextDirection"] = item.TextDirection.ToString();
            //json["TextImageRelation"] = item.TextImageRelation.ToString();
            //json["Available"] = item.Available;
            //json["DoubleClickEnabled"] = item.DoubleClickEnabled;

            //if (item.Image != null)
            //{
            //    json["Image"] = item.Image.CreateBlobUrl();
            //}

            //if (item is ToolStripButton btn)
            //{
            //    json["Checked"] = btn.Checked;
            //    json["CheckOnClick"] = btn.CheckOnClick;
            //}

            //if (item is ToolStripComboBox combo)
            //{
            //    var arr = new JsonArray();
            //    //foreach (var obj in combo.Items)
            //    //{
            //    //    arr.Add(obj?.ToString());
            //    //}
            //    json["Items"] = arr;
            //    json["SelectedIndex"] = combo.SelectedIndex;
            //    json["DropDownStyle"] = combo.DropDownStyle.ToString();
            //    json["MaxDropDownItems"] = combo.MaxDropDownItems;
            //    json["DropDownWidth"] = combo.DropDownWidth;
            //    json["DropDownHeight"] = combo.DropDownHeight;
            //    json["IntegralHeight"] = combo.IntegralHeight;
            //    json["Sorted"] = combo.Sorted;
            //}

            //if (item is ToolStripTextBox txt)
            //{
            //    json["Text"] = txt.Text;
            //    json["ReadOnly"] = txt.ReadOnly;
            //    json["MaxLength"] = txt.MaxLength;
            //    json["AcceptsReturn"] = txt.AcceptsReturn;
            //    json["AcceptsTab"] = txt.AcceptsTab;
            //    json["CharacterCasing"] = txt.CharacterCasing.ToString();
            //    json["ShortcutsEnabled"] = txt.ShortcutsEnabled;
            //    json["TextBoxTextAlign"] = txt.TextBoxTextAlign.ToString();
            //}
            //else if (item is ToolStripProgressBar progressBar)
            //{
            //    json["Minimum"] = progressBar.Minimum;
            //    json["Maximum"] = progressBar.Maximum;
            //    json["Value"] = progressBar.Value;
            //    json["Step"] = progressBar.Step;
            //    json["Style"] = progressBar.Style.ToString();
            //    json["MarqueeAnimationSpeed"] = progressBar.MarqueeAnimationSpeed;
            //}
            //else if (item is ToolStripStatusLabel statusLabel)
            //{
            //    json["Spring"] = statusLabel.Spring;
            //    json["TextAlign"] = statusLabel.TextAlign.ToString();
            //}
        }

        /// <summary>
        /// WF2W支持的控件类型。只有WritePropertyValueToJson() 和 _framework/System.Windows.Forms.XXXXX.js 都支持的时候就往这个数组添加控件类型
        /// </summary>
        public static readonly Type[] _SupportedTypes = new Type[] {
            typeof( System.Drawing.Graphics),
            typeof( System.Drawing.Drawing2D.HatchBrush),
            typeof( System.Windows.Forms.Control),
            typeof( System.Windows.Forms.ScrollableControl ),
            typeof( System.Windows.Forms.ContainerControl ),
            typeof( System.Windows.Forms.Button ),
            //typeof( System.Windows.Forms.ComboBox ),
            typeof( System.Windows.Forms.Form ),
            typeof( System.Windows.Forms.Label),
            typeof( System.Windows.Forms.MainMenu ),
            typeof( System.Windows.Forms.MenuStrip),
            typeof( System.Windows.Forms.MessageBox ),
            typeof( System.Windows.Forms.Panel ),
            typeof( System.Windows.Forms.PictureBox ),
            //typeof( System.Windows.Forms.TabControl ),
            //typeof( System.Windows.Forms.TabPage ),
            typeof( System.Windows.Forms.TextBox),
            typeof( System.Windows.Forms.ToolStrip ),
            typeof( System.Windows.Forms.UserControl ),
            typeof( System.Windows.Forms.CheckBox ),
            typeof( System.Windows.Forms.RadioButton ),
            typeof( System.Windows.Forms.GroupBox ),
            typeof( System.Windows.Forms.LinkLabel )
        };
        ///// <summary>
        ///// 将控件的属性写入Json对象
        ///// </summary>
        ///// <param name="ctl">控件对象</param>
        ///// <param name="json">JSON对象</param>
        ///// <remarks>只输出控件属性，不递归输出子控件的属性
        ///// 支持的控件类型加上标记 [System.Reflection.Obfuscation(Exclude =true , ApplyToMembers = false  )]
        ///// 其支持的属性加上标记 [System.Reflection.Obfuscation(Exclude =true , ApplyToMembers = true)]
        ///// 注意这个ApplyToMembers属性值不要搞错了。
        ///// 修改这个方法时根据需要更新 _SupportedTypes 数组。
        ///// </remarks>
        //public static void WritePropertyValueToJson(System.Windows.Forms.Control ctl, JsonObject json)
        //{
        //    if (ctl == null || json == null)
        //    {
        //        return;
        //    }

        //    JsonObject BuildSizeJson(Size size)
        //    {
        //        var obj = new JsonObject
        //        {
        //            ["Width"] = size.Width,
        //            ["Height"] = size.Height
        //        };
        //        return obj;
        //    }
        //    JsonObject BuildPaddingJson(Padding padding)
        //    {
        //        var obj = new JsonObject
        //        {
        //            ["Left"] = padding.Left,
        //            ["Top"] = padding.Top,
        //            ["Right"] = padding.Right,
        //            ["Bottom"] = padding.Bottom
        //        };
        //        return obj;
        //    }

        //    if (ctl.IsHandleCreated)
        //    {
        //        json["Handle"] = ctl.Handle.ToInt32();
        //    }
        //    if (ctl.Name != null && ctl.Name.Length > 0)
        //    {
        //        json["DCName"] = ctl.Name;
        //    }
        //    json["Type"] = ctl.GetType().FullName;
        //    if (ctl.IsUserPaint())
        //    {
        //        json["UserPaint"] = true;
        //    }
        //    json["Left"] = ctl.Left;
        //    json["Top"] = ctl.Top;
        //    json["Width"] = ctl.Width;
        //    json["Height"] = ctl.Height;
        //    json["Visible"] = ctl.Visible;
        //    json["Enabled"] = ctl.Enabled;
        //    json["Dock"] = ctl.Dock.ToString();
        //    json["Anchor"] = ctl.Anchor.ToString();
        //    json["TabIndex"] = ctl.TabIndex;
        //    json["TabStop"] = ctl.TabStop;
        //    json["Text"] = ctl.Text;
        //    json["BackColor"] = ColorTranslator.ToHtml(ctl.BackColor);
        //    json["ForeColor"] = ColorTranslator.ToHtml(ctl.ForeColor);
        //    if (ctl.Font != null)
        //    {
        //        json["Font"] = ctl.Font.ToJsonObject();
        //    }
        //    if (ctl is ToolStrip toolStrip)
        //    {
        //        json["GripStyle"] = toolStrip.GripStyle.ToString();
        //        json["LayoutStyle"] = toolStrip.LayoutStyle.ToString();
        //        json["RenderMode"] = toolStrip.RenderMode.ToString();
        //        if (toolStrip.Items != null && toolStrip.Items.Count > 0)
        //        {
        //            var arr2 = new JsonArray();
        //            foreach (ToolStripItem item in toolStrip.Items)
        //            {
        //                var obj2 = new JsonObject();
        //                WritePropertyValueToJson(item, obj2);
        //                arr2.Add(obj2);
        //            }
        //            json["Items"] = arr2;
        //        }
        //        return;
        //    }
        //    if (ctl is ScrollableControl scrollable)
        //    {
        //        json["AutoScroll"] = scrollable.AutoScroll;
        //        json["AutoScrollMargin"] = BuildSizeJson(scrollable.AutoScrollMargin);
        //        json["AutoScrollMinSize"] = BuildSizeJson(scrollable.AutoScrollMinSize);
        //        json["Padding"] = BuildPaddingJson(scrollable.Padding);

        //        if (scrollable is Panel panel)
        //        {
        //            json["BorderStyle"] = panel.BorderStyle.ToString();
        //            if (panel is TabPage tabPage)
        //            {
        //                json["UseVisualStyleBackColor"] = tabPage.UseVisualStyleBackColor;
        //            }
        //        }

        //        if (scrollable is ContainerControl container)
        //        {
        //            json["AutoScaleMode"] = container.AutoScaleMode.ToString();
        //            json["AutoValidate"] = container.AutoValidate.ToString();
        //            if (container.ActiveControl != null)
        //            {
        //                json["ActiveControl"] = container.ActiveControl.Name;
        //            }

        //            if (container is Form form)
        //            {
        //                if (form.ShowIcon && form.Icon != null)
        //                {
        //                    json["IconImage"] = form.Icon.CreateBlobUrl(true);
        //                }
        //                if (form.Menu != null)
        //                {
        //                    json["Menu"] = form.Menu.ToJsonObject();
        //                }
        //                if (form.AcceptButton is Control ctl2)
        //                {
        //                    json["AcceptButton"] = ctl2.Handle.ToInt32();
        //                }
        //                if (form.CancelButton is Control ctl3)
        //                {
        //                    json["CancelButton"] = ctl3.Handle.ToInt32();
        //                }
        //                json["FormBorderStyle"] = form.FormBorderStyle.ToString();
        //                json["StartPosition"] = form.StartPosition.ToString();
        //                json["WindowState"] = form.WindowState.ToString();
        //                json["TopMost"] = form.TopMost;
        //                json["ShowIcon"] = form.ShowIcon;
        //                json["ShowInTaskbar"] = form.ShowInTaskbar;
        //                json["MinimizeBox"] = form.MinimizeBox;
        //                json["MaximizeBox"] = form.MaximizeBox;
        //                json["ControlBox"] = form.ControlBox;
        //                json["Opacity"] = form.Opacity;
        //                json["SizeGripStyle"] = form.SizeGripStyle.ToString();
        //            }
        //        }
        //    }

        //    if (ctl is Label label)
        //    {
        //        json["AutoSize"] = label.AutoSize;
        //        json["TextAlign"] = label.TextAlign.ToString();
        //        json["BorderStyle"] = label.BorderStyle.ToString();
        //        json["UseMnemonic"] = label.UseMnemonic;
        //    }

        //    if (ctl is ButtonBase btnBase)
        //    {
        //        json["Text"] = btnBase.Text;
        //        json["ImageAlign"] = btnBase.ImageAlign.ToString();
        //        json["TextAlign"] = btnBase.TextAlign.ToString();
        //        json["FlatStyle"] = btnBase.FlatStyle.ToString();
        //        json["UseMnemonic"] = btnBase.UseMnemonic;

        //        if (btnBase is Button btn)
        //        {
        //            json["DialogResult"] = btn.DialogResult.ToString();
        //            json["UseVisualStyleBackColor"] = btn.UseVisualStyleBackColor;
        //        }
        //        else if (btnBase is CheckBox checkBox)
        //        {
        //            json["Checked"] = checkBox.Checked;
        //            json["CheckState"] = checkBox.CheckState.ToString();
        //            json["ThreeState"] = checkBox.ThreeState;
        //        }
        //        if (btnBase is RadioButton radioButton)
        //        {
        //            json["Appearance"] = radioButton.Appearance.ToString();
        //            json["AutoCheck"] = radioButton.AutoCheck;
        //        }
        //    }

        //    if (ctl is TextBoxBase txtBase)
        //    {
        //        json["Text"] = txtBase.Text;
        //        json["MaxLength"] = txtBase.MaxLength;
        //        json["ReadOnly"] = txtBase.ReadOnly;
        //        json["AcceptsTab"] = txtBase.AcceptsTab;
        //        json["ShortcutsEnabled"] = txtBase.ShortcutsEnabled;
        //        json["SelectionStart"] = txtBase.SelectionStart;
        //        json["SelectionLength"] = txtBase.SelectionLength;
        //        json["WordWrap"] = txtBase.WordWrap;

        //        if (txtBase is TextBox txt)
        //        {
        //            json["Multiline"] = txt.Multiline;
        //            json["PasswordChar"] = txt.PasswordChar.ToString();
        //            json["ScrollBars"] = txt.ScrollBars.ToString();
        //            json["UseSystemPasswordChar"] = txt.UseSystemPasswordChar;
        //            json["AcceptsReturn"] = txt.AcceptsReturn;
        //            json["CharacterCasing"] = txt.CharacterCasing.ToString();
        //            json["TextAlign"] = txt.TextAlign.ToString();
        //        }
        //        else if (txtBase is RichTextBox richTextBox)
        //        {
        //            json["DetectUrls"] = richTextBox.DetectUrls;
        //            json["ScrollBars"] = richTextBox.ScrollBars.ToString();
        //            json["ZoomFactor"] = richTextBox.ZoomFactor;
        //        }
        //    }

        //    if (ctl is ListControl listControl)
        //    {
        //        json["SelectedIndex"] = listControl.SelectedIndex;
        //        json["DisplayMember"] = listControl.DisplayMember;
        //        json["ValueMember"] = listControl.ValueMember;

        //        if (listControl is ComboBox comboBox)
        //        {
        //            json["DropDownStyle"] = comboBox.DropDownStyle.ToString();
        //            json["DropDownHeight"] = comboBox.DropDownHeight;
        //            json["DropDownWidth"] = comboBox.DropDownWidth;
        //            json["IntegralHeight"] = comboBox.IntegralHeight;
        //            json["MaxDropDownItems"] = comboBox.MaxDropDownItems;
        //            json["ItemHeight"] = comboBox.ItemHeight;
        //            json["Sorted"] = comboBox.Sorted;
        //        }
        //        else if (listControl is ListBox listBox)
        //        {
        //            json["SelectionMode"] = listBox.SelectionMode.ToString();
        //            json["HorizontalScrollbar"] = listBox.HorizontalScrollbar;
        //            json["IntegralHeight"] = listBox.IntegralHeight;
        //            json["MultiColumn"] = listBox.MultiColumn;
        //            json["Sorted"] = listBox.Sorted;
        //        }
        //    }

        //    if (ctl is PictureBox picBox)
        //    {
        //        if (picBox.Image != null)
        //        {
        //            json["Image"] = picBox.Image.CreateBlobUrl();
        //        }
        //        json["ImageLocation"] = picBox.ImageLocation;
        //        json["SizeMode"] = picBox.SizeMode.ToString();
        //        json["BorderStyle"] = picBox.BorderStyle.ToString();
        //    }

        //    if (ctl is ProgressBar progressBar)
        //    {
        //        json["Minimum"] = progressBar.Minimum;
        //        json["Maximum"] = progressBar.Maximum;
        //        json["Value"] = progressBar.Value;
        //        json["Step"] = progressBar.Step;
        //        json["Style"] = progressBar.Style.ToString();
        //        json["MarqueeAnimationSpeed"] = progressBar.MarqueeAnimationSpeed;
        //    }

        //    if (ctl is TabControl tabControl)
        //    {
        //        json["SelectedIndex"] = tabControl.SelectedIndex;
        //        json["Alignment"] = tabControl.Alignment.ToString();
        //        json["Appearance"] = tabControl.Appearance.ToString();
        //        json["HotTrack"] = tabControl.HotTrack;
        //        json["Multiline"] = tabControl.Multiline;
        //        json["SizeMode"] = tabControl.SizeMode.ToString();
        //    }
        //}


        public static string ParseImageToHtmlImageSrc(Image image)
        {
            if (image == null)
            {
                return null;
            }

            string result = null;
            using(MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                result = "data:image/png;base64," + Convert.ToBase64String(ms.ToArray());
                ms.Close();
            }
            return result;
        }

        public static JsonNode ParseSizeToJSONObj(Size sizeze)
        {
            JsonObject obj = new JsonObject();
            obj.Add("Width", sizeze.Width);
            obj.Add("Height", sizeze.Height);
            obj.Add("IsEmpty", sizeze.IsEmpty);
            return obj;
        }
        public static JsonNode ParseSizeToJSONObj(SizeF sizeze)
        {
            JsonObject obj = new JsonObject();
            obj.Add("Width", sizeze.Width);
            obj.Add("Height", sizeze.Height);
            obj.Add("IsEmpty", sizeze.IsEmpty);
            return obj;
        }
        public static JsonNode ParsePaddingToJSONObj(Padding pad)
        {
            JsonObject obj = new JsonObject();
            obj.Add("Top", pad.Top);
            obj.Add("Left", pad.Left);
            obj.Add("Right", pad.Right);
            obj.Add("Bottom", pad.Bottom);
            obj.Add("Horizontal", pad.Horizontal);
            obj.Add("Vertical", pad.Vertical);
            obj.Add("All", pad.All);
            return obj;
        }
        public static JsonNode ParseVHScrollPropertiesToJSONObj(HScrollProperties property)
        {
            JsonObject obj = new JsonObject();
            obj.Add("Enabled", property.Enabled);
            obj.Add("HorizontalDisplayPosition", property.HorizontalDisplayPosition);
            obj.Add("LargeChange", property.LargeChange);
            obj.Add("Maximum", property.Maximum);
            obj.Add("Minimum", property.Minimum);
            obj.Add("Orientation", property.Orientation);
            obj.Add("PageSize", property.PageSize);
            obj.Add("SmallChange", property.SmallChange);
            obj.Add("Value", property.Value);
            obj.Add("VerticalDisplayPosition", property.VerticalDisplayPosition);
            obj.Add("Visible", property.Visible);
            return obj;
        }
        public static JsonNode ParseVHScrollPropertiesToJSONObj(VScrollProperties property)
        {
            JsonObject obj = new JsonObject();
            obj.Add("Enabled", property.Enabled);
            obj.Add("HorizontalDisplayPosition", property.HorizontalDisplayPosition);
            obj.Add("LargeChange", property.LargeChange);
            obj.Add("Maximum", property.Maximum);
            obj.Add("Minimum", property.Minimum);
            obj.Add("Orientation", property.Orientation);
            obj.Add("PageSize", property.PageSize);
            obj.Add("SmallChange", property.SmallChange);
            obj.Add("Value", property.Value);
            obj.Add("VerticalDisplayPosition", property.VerticalDisplayPosition);
            obj.Add("Visible", property.Visible);
            return obj;
        }
        public static JsonNode ParseFontToJSONObj(Font f)
        {
            JsonObject obj = new JsonObject();
            obj.Add("Bold", f.Bold);
            obj.Add("Height", f.Height);
            obj.Add("IsSystemFont", f.IsSystemFont);
            obj.Add("Italic", f.Italic);
            obj.Add("Name", f.Name);
            obj.Add("OriginalFontName", f.OriginalFontName);
            obj.Add("Size", f.Size);
            obj.Add("Strikeout", f.Strikeout);
            obj.Add("Underline", f.Underline);
            obj.Add("Unit", f.Unit.ToString());
            return obj;
        }
        public static string ParseColorToRGBAString(Color c)
        {
            //if (c.A != byte.MaxValue)
            //{
            //    return "rgba(" + c.R + "," + c.G + "," + c.B + "," + ((double)(int)c.A / 255.0).ToString("0.0000") + ")";
            //}
            if (c == Color.Black)
            {
                return "#000000FF";
            }
            else if (c == Color.White)
            {
                return "#FFFFFFFF";
            }
            else if (c == Color.Transparent)
            {
                return "#FFFFFF00";
            }
            else if (c == Color.Empty)
            {
                return "#00000000";
            }
            else
            {
                return $"#{c.R:X2}{c.G:X2}{c.B:X2}{c.A:X2}";
            }
        }
    }
}
