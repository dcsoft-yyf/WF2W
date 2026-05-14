//------------------------------------------------------------------------------
// <copyright file="MenuItem.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>                                                                
//------------------------------------------------------------------------------

namespace System.Windows.Forms {
    using System.Configuration.Assemblies;
    using System.Runtime.Remoting;
    using System.Runtime.InteropServices;

    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;

    using System;
    using System.Collections;

    using System.ComponentModel;
    using System.Windows.Forms.Design;
    using System.Windows.Forms;
    using System.Windows.Forms.VisualStyles;
    using System.Drawing;
    using System.Windows.Forms.Internal;
    using System.Drawing.Design;
    using System.Drawing.Text;
    using System.Drawing.Imaging;
    using Microsoft.Win32;
    using System.Security;
    using System.Security.Permissions;
    using System.Globalization;
    using System.Threading;
    using System.Text.Json.Nodes;
    using System.Threading.Tasks;

    /// <include file='doc\MenuItem.uex' path='docs/doc[@for="MenuItem"]/*' />
    /// <devdoc>
    ///    <para>
    ///       Represents an individual item that is displayed within
    ///       a <see cref='System.Windows.Forms.Menu'/> or <see cref='System.Windows.Forms.Menu'/>.
    ///    </para>
    /// </devdoc>
    [
    ToolboxItem(false),
    DesignTimeVisible(false),
    DefaultEvent("Click"),
    DefaultProperty("Text")
    ]
    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = false )]
    public class MenuItem : Menu {
        public override JsonObject ToJsonObject()
        {
            var obj = new JsonObject();
            DCWinFormHelper.WritePropertyValueToJson(this, obj);
            return obj;
        }
        //public static async ValueTask RaiseClick( int handle )
        //{
        //    var mItem = DCWin32API.GetControl(handle) as MenuItem;
        //    if( mItem != null )
        //    {
        //        await mItem.OnClickAsync(EventArgs.Empty);
        //    }
        //}
        public static async ValueTask<JsonArray> GetChildMenuItems(int handle)
        {
            var mItem = DCWin32API.GetControl(handle) as MenuItem;
            if (mItem != null)
            {
                await mItem.OnPopupAsync(EventArgs.Empty);
                var ms = mItem.MenuItems;
                if (ms != null && ms.Count > 0)
                {
                    return ms.ToJsonArray();
                }
            }
            return null;
        }
        internal const int STATE_BARBREAK   = 0x00000020;
        internal const int STATE_BREAK      = 0x00000040;
        internal const int STATE_CHECKED    = 0x00000008;
        internal const int STATE_DEFAULT    = 0x00001000;
        internal const int STATE_DISABLED   = 0x00000003;
        internal const int STATE_RADIOCHECK = 0x00000200;
        internal const int STATE_HIDDEN     = 0x00010000;
        internal const int STATE_MDILIST    = 0x00020000;
        internal const int STATE_CLONE_MASK = 0x0003136B;
        internal const int STATE_OWNERDRAW  = 0x00000100;
        internal const int STATE_INMDIPOPUP = 0x00000200;
        internal const int STATE_HILITE     = 0x00000080;

        private Menu                _menu;
        private bool                _hasHandle;
        private MenuItemData        _data;
        private int                 _dataVersion;
        private MenuItem            _nextLinkedItem; // Next item linked to the same MenuItemData.
        
        // We need to store a table of all created menuitems, so that other objects
        // such as ContainerControl can get a reference to a particular menuitem,
        // given a unique ID.
        private static Hashtable allCreatedMenuItems = new Hashtable();
        private const uint firstUniqueID = 0xC0000000;
        private static long nextUniqueID = firstUniqueID;
        private uint _uniqueID = 0;
        private IntPtr _msaaMenuInfoPtr = IntPtr.Zero;
        private bool _menuItemIsCreated = false;

        /// <include file='doc\MenuItem.uex' path='docs/doc[@for="MenuItem.MenuItem"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Initializes a <see cref='System.Windows.Forms.MenuItem'/> with
        ///       a blank caption.
        ///    </para>
        /// </devdoc>
        public MenuItem() : this(MenuMerge.Add, 0, 0, null, null, null, null, null) {
        }

        /// <include file='doc\MenuItem.uex' path='docs/doc[@for="MenuItem.MenuItem1"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Initializes a new instance of the <see
        ///       cref='System.Windows.Forms.MenuItem'/>
        ///       class with a specified caption for
        ///       the menu item.
        ///    </para>
        /// </devdoc>
        public MenuItem(string text) : this(MenuMerge.Add, 0, 0, text, null, null, null, null) {
        }

        /// <include file='doc\MenuItem.uex' path='docs/doc[@for="MenuItem.MenuItem2"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Initializes a new instance of the
        ///       class with a
        ///       specified caption and
        ///       event handler for the menu item.
        ///    </para>
        /// </devdoc>
        public MenuItem(string text, EventHandler onClick) : this(MenuMerge.Add, 0, 0, text, onClick, null, null, null) {
        }

        /// <include file='doc\MenuItem.uex' path='docs/doc[@for="MenuItem.MenuItem3"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Initializes a new instance of the
        ///       class with a
        ///       specified caption, event handler, and associated
        ///       shorcut key for the menu item.
        ///    </para>
        /// </devdoc>
        public MenuItem(string text, EventHandler onClick, Shortcut shortcut) : this(MenuMerge.Add, 0, shortcut, text, onClick, null, null, null) {
        }

        /// <include file='doc\MenuItem.uex' path='docs/doc[@for="MenuItem.MenuItem4"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Initializes a new instance of the
        ///       class with a
        ///       specified caption and an array of
        ///       submenu items defined for the menu item.
        ///    </para>
        /// </devdoc>
        public MenuItem(string text, MenuItem[] items) : this(MenuMerge.Add, 0, 0, text, null, null, null, items) {
        }

        internal MenuItem(MenuItemData data)
        : base(null) {
            data.AddItem(this);
            
            #if DEBUG
                _debugText = data.caption;
            #endif 
        }

        /// <include file='doc\MenuItem.uex' path='docs/doc[@for="MenuItem.MenuItem5"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Initializes a new instance of the class with a specified
        ///       caption, defined event-handlers for the Click, Select and
        ///       Popup events, a shortcut key,
        ///       a merge type, and order specified for the menu item.
        /// </para>
        /// </devdoc>
        [SuppressMessage("Microsoft.Performance", "CA1806:DoNotIgnoreMethodResults")]
        public MenuItem(MenuMerge mergeType, int mergeOrder, Shortcut shortcut,
                        string text, EventHandler onClick, EventHandler onPopup,
                        EventHandler onSelect, MenuItem[] items)

        : base(items) {

            new MenuItemData(this, mergeType, mergeOrder, shortcut, true,
                             text, onClick, onPopup, onSelect, null, null);

            #if DEBUG
                _debugText = text;
                _creationNumber = CreateCount++;
            #endif 
        }

        #if DEBUG
            private string _debugText;
            private int    _creationNumber;
            private Menu   _debugParentMenu;
            private static int CreateCount;
        #endif


        /// <include file='doc\MenuItem.uex' path='docs/doc[@for="MenuItem.BarBreak"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Gets or sets a value indicating whether the item is
        ///       placed on a new line (for a menu item added to a <see
        ///       cref='System.Windows.Forms.MainMenu'/> object) or in a new
        ///       column (for a submenu or menu displayed in a <see
        ///       cref='System.Windows.Forms.ContextMenu'/>
        ///       ).
        ///    </para>
        /// </devdoc>
        [
        Browsable(false),
        DefaultValue(false)
        ]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public bool BarBreak {
            get {
                return(_data.State & STATE_BARBREAK) != 0;
            }

            set {
                _data.SetState(STATE_BARBREAK, value);
            }
        }

        /// <include file='doc\MenuItem.uex' path='docs/doc[@for="MenuItem.Break"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Gets or sets a value indicating whether the item is
        ///       placed on a new line (for a menu item added to a <see
        ///       cref='System.Windows.Forms.MainMenu'/> object) or in a new column (for a
        ///       submenu or menu displayed in a <see
        ///       cref='System.Windows.Forms.ContextMenu'/>).
        ///    </para>
        /// </devdoc>
        [
        Browsable(false),
        DefaultValue(false)
        ]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public bool Break {
            get {
                return(_data.State & STATE_BREAK) != 0;
            }

            set {
                _data.SetState(STATE_BREAK, value);
            }
        }

        /// <include file='doc\MenuItem.uex' path='docs/doc[@for="MenuItem.Checked"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Gets or sets a value indicating whether a checkmark
        ///       appears beside the text of the menu item.
        ///    </para>
        /// </devdoc>
        [
        DefaultValue(false),
        SRDescription(DCSR.MenuItemCheckedDescr)
        ]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public bool Checked {
            get {
                return(_data.State & STATE_CHECKED) != 0;
            }

            set {
                //if trying to set checked=true - if we're a top-level item (from a mainmenu) or have children, don't do this...
                if (value == true && (ItemCount != 0 || (Parent != null && (Parent is MainMenu)))) {
                    throw new ArgumentException(DCSR.GetString(DCSR.MenuItemInvalidCheckProperty));
                }

                _data.SetState(STATE_CHECKED, value);
            }
        }

        /// <include file='doc\MenuItem.uex' path='docs/doc[@for="MenuItem.DefaultItem"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Gets or sets a value indicating
        ///       whether the menu item is the default.
        ///    </para>
        /// </devdoc>
        [
        DefaultValue(false),
        SRDescription(DCSR.MenuItemDefaultDescr)
        ]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public bool DefaultItem {
            get { return(_data.State & STATE_DEFAULT) != 0;}
            set {
                if (_menu != null) {
                    if (value) {
                        WinFormUnsafeNativeMethods.SetMenuDefaultItem(new HandleRef(_menu, _menu.handle), MenuID, false);
                    }
                    else if (DefaultItem) {
                        WinFormUnsafeNativeMethods.SetMenuDefaultItem(new HandleRef(_menu, _menu.handle), -1, false);
                    }
                }
                _data.SetState(STATE_DEFAULT, value);
            }
        }

        /// <include file='doc\MenuItem.uex' path='docs/doc[@for="MenuItem.OwnerDraw"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Gets or sets a value indicating whether code
        ///       that you provide draws the menu item or Windows draws the
        ///       menu item.
        ///    </para>
        /// </devdoc>
        [
        SRCategory(DCSR.CatBehavior),
        DefaultValue(false),
        SRDescription(DCSR.MenuItemOwnerDrawDescr)
        ]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public bool OwnerDraw {
            get {
                return((_data.State & STATE_OWNERDRAW) != 0);
            }
            set {
                _data.SetState(STATE_OWNERDRAW, value);
            }
           
        }

        /// <include file='doc\MenuItem.uex' path='docs/doc[@for="MenuItem.Enabled"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Gets or sets a value indicating whether the menu
        ///       item is enabled.
        ///    </para>
        /// </devdoc>
        [
        Localizable(true),
        DefaultValue(true),
        SRDescription(DCSR.MenuItemEnabledDescr)
        ]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public bool Enabled {
            get {
                return(_data.State & STATE_DISABLED) == 0;
            }

            set {
                _data.SetState(STATE_DISABLED, !value);
            }
        }

        /// <include file='doc\MenuItem.uex' path='docs/doc[@for="MenuItem.Index"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Gets or sets the menu item's position in its parent menu.
        ///    </para>
        /// </devdoc>
        [
        Browsable(false),
        ]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public int Index {
            get {
                if (_menu != null) {
                    for (int i = 0; i < _menu.ItemCount; i++) {
                        if (_menu.items[i] == this) return i;
                    }
                }
                return -1;
            }

            set {
                int oldIndex = Index;
                if (oldIndex >= 0) {
                    if (value < 0 || value >= _menu.ItemCount) {
                        throw new ArgumentOutOfRangeException("Index", DCSR.GetString(DCSR.InvalidArgument, "Index", (value).ToString(CultureInfo.CurrentCulture)));
                    }

                    if (value != oldIndex) {
                        // this.menu reverts to null when we're removed, so hold onto it in a local variable
                        Menu parent = _menu;
                        parent.MenuItems.RemoveAt(oldIndex);
                        parent.MenuItems.Add(value, this);
                    }
                }
            }
        }

        /// <include file='doc\MenuItem.uex' path='docs/doc[@for="MenuItem.IsParent"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Gets a value indicating whether the menu item contains
        ///       child menu items.
        ///    </para>
        /// </devdoc>
        [
        Browsable(false),
        ]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public override bool IsParent {
            get {
                bool parent = false;
                if (_data != null && MdiList) {
                    for (int i=0; i<ItemCount; i++) {
                        if (!(items[i]._data.UserData is MdiListUserData)) {
                            parent = true;
                            break;
                        }
                    }
                    if (!parent) {
                        if (FindMdiForms().Length > 0) {
                            parent = true;
                        }
                    }
                    if (!parent) {
                        if (_menu != null && !(_menu is MenuItem)) {
                            parent = true;
                        }
                    }
                }
                else {
                    parent = base.IsParent;
                }
                return parent;
            }
        }

        /// <include file='doc\MenuItem.uex' path='docs/doc[@for="MenuItem.MdiList"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Gets or sets
        ///       a value indicating whether the menu item will be populated
        ///       with a list of the MDI child windows that are displayed within the
        ///       associated form.
        ///    </para>
        /// </devdoc> 
        [
        DefaultValue(false),
        SRDescription(DCSR.MenuItemMDIListDescr)
        ]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public bool MdiList {
            get {
                return(_data.State & STATE_MDILIST) != 0;
            }
            set {
                _data.MdiList = value;
                CleanListItems(this);
            }
        }

        internal Menu Menu {
                get {
                    return _menu;      
                }
                set {
                    _menu = value;
                    #if DEBUG
                        _debugParentMenu = value;
                    #endif
                }
            }

        /// <include file='doc\MenuItem.uex' path='docs/doc[@for="MenuItem.MenuID"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Gets the Windows identifier for this menu item.
        ///    </para>
        /// </devdoc> 
        protected int MenuID {
            get { return _data.GetMenuID();}
        }

        /// <include file='doc\MenuItem.uex' path='docs/doc[@for="MenuItem.Selected"]/*' />
        /// <devdoc>
        ///     Is this menu item currently selected (highlighted) by the user?
        /// </devdoc> 
        internal bool Selected {
            get {
                if (_menu == null)
                    return false;

                WinFormNativeMethods.MENUITEMINFO_T info = new WinFormNativeMethods.MENUITEMINFO_T();
                info.cbSize = Marshal.SizeOf(typeof(WinFormNativeMethods.MENUITEMINFO_T));
                info.fMask = WinFormNativeMethods.MIIM_STATE;
                WinFormUnsafeNativeMethods.GetMenuItemInfo(new HandleRef(_menu, _menu.handle), MenuID, false, info);

                return (info.fState & STATE_HILITE) != 0;
            }
        }

        
        /// <include file='doc\MenuItem.uex' path='docs/doc[@for="MenuItem.MenuIndex"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Gets the zero-based index of this menu
        ///       item in the parent menu, or -1 if this
        ///       menu item is not associated with a 
        ///       parent menu.
        ///    </para>
        /// </devdoc> 
        internal int MenuIndex {
            get {
                if (_menu == null) return -1;

                int count = WinFormUnsafeNativeMethods.GetMenuItemCount(new HandleRef(_menu, _menu.Handle));
                int id = MenuID;
                WinFormNativeMethods.MENUITEMINFO_T info = new WinFormNativeMethods.MENUITEMINFO_T();
                info.cbSize = Marshal.SizeOf(typeof(WinFormNativeMethods.MENUITEMINFO_T));
                info.fMask = WinFormNativeMethods.MIIM_ID | WinFormNativeMethods.MIIM_SUBMENU;
                
                for(int i = 0; i < count; i++) {
                    WinFormUnsafeNativeMethods.GetMenuItemInfo(new HandleRef(_menu, _menu.handle), i, true, info);

                    // For sub menus, the handle is always valid.  For
                    // items, however, it is always zero.
                    //
                    if ((info.hSubMenu == IntPtr.Zero || info.hSubMenu == Handle) && info.wID == id) {
                        return i;
                    }
                }
                return -1;
            }
        }

        /// <include file='doc\MenuItem.uex' path='docs/doc[@for="MenuItem.MergeType"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Gets or sets a value that indicates the behavior of this
        ///       menu item when its menu is merged with another.
        ///       
        ///    </para>
        /// </devdoc>
        [
        DefaultValue(MenuMerge.Add),
        SRDescription(DCSR.MenuItemMergeTypeDescr)
        ]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public MenuMerge MergeType {
            get {
                return _data.mergeType;
            }
            set {

                //valid values are 0x0 to 0x3
                if (!ClientUtils.IsEnumValid(value, (int)value, (int)MenuMerge.Add, (int)MenuMerge.Remove)){
                    throw new InvalidEnumArgumentException("value", (int)value, typeof(MenuMerge));
                }

                _data.MergeType = value;
            }
        }

        /// <include file='doc\MenuItem.uex' path='docs/doc[@for="MenuItem.MergeOrder"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Gets or sets the relative position the menu item when its
        ///       menu is merged with another.
        ///       
        ///    </para>
        /// </devdoc>
        [
        DefaultValue(0),
        SRDescription(DCSR.MenuItemMergeOrderDescr)
        ]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public int MergeOrder {
            get {
                return _data.mergeOrder;
            }
            set {
                _data.MergeOrder = value;
            }
        }

        /// <include file='doc\MenuItem.uex' path='docs/doc[@for="MenuItem.Mnemonic"]/*' />
        /// <devdoc>
        ///     <para>
        ///     Retrieves the hotkey mnemonic that is associated with this menu item.
        ///     The mnemonic is the first character after an ampersand symbol in the menu's text
        ///     that is not itself an ampersand symbol.  If no such mnemonic is defined this
        ///     will return zero.
        ///     </para>
        /// </devdoc>
        [Browsable(false)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public char Mnemonic {
            get {
                return _data.Mnemonic;
            }
        }

        /// <include file='doc\MenuItem.uex' path='docs/doc[@for="MenuItem.Parent"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Gets the menu in which this menu item
        ///       appears.
        ///    </para>
        /// </devdoc> 
        [Browsable(false)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public Menu Parent {
            get {return _menu;}
        }

        /// <include file='doc\MenuItem.uex' path='docs/doc[@for="MenuItem.RadioCheck"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Gets or sets a value that indicates whether the menu item,
        ///       if checked, displays a radio-button mark instead of a check mark.
        ///    </para>
        /// </devdoc>
        [
        DefaultValue(false),
        SRDescription(DCSR.MenuItemRadioCheckDescr)
        ]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public bool RadioCheck {
            get {
                return(_data.State & STATE_RADIOCHECK) != 0;
            }
            set {
                _data.SetState(STATE_RADIOCHECK, value);
            }
        }
        
        internal override bool RenderIsRightToLeft {
            get {
                if(Parent == null)
                    return false;
                else
                    return Parent.RenderIsRightToLeft;
            }
        }

        /// <include file='doc\MenuItem.uex' path='docs/doc[@for="MenuItem.Text"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Gets or sets the text of the menu item.
        ///    </para>
        /// </devdoc> 
        [
        Localizable(true),
        SRDescription(DCSR.MenuItemTextDescr)
        ]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string Text {
            get {
                return _data.caption;
            }
            set {
                _data.SetCaption(value);
            }
        }

        /// <include file='doc\MenuItem.uex' path='docs/doc[@for="MenuItem.Shortcut"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Gets or sets the shortcut key associated with the menu
        ///       item.
        ///    </para>
        /// </devdoc>
        [
        Localizable(true),
        DefaultValue(Shortcut.None),
        SRDescription(DCSR.MenuItemShortCutDescr)
        ]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public Shortcut Shortcut {
            get {
                return _data.shortcut;
            }
            set {
                if (!Enum.IsDefined(typeof(Shortcut), value)) {
                    throw new InvalidEnumArgumentException("value", (int)value, typeof(Shortcut));
                }

                _data.shortcut = value;
                UpdateMenuItem(true);
            }
        }

        /// <include file='doc\MenuItem.uex' path='docs/doc[@for="MenuItem.ShowShortcut"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Gets or sets a value that indicates whether the shortcut
        ///       key that is assocaited
        ///       with the menu item is displayed next to the menu item
        ///       caption.
        ///    </para>
        /// </devdoc>
        [
        DefaultValue(true),
        Localizable(true),
        SRDescription(DCSR.MenuItemShowShortCutDescr)
        ]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public bool ShowShortcut {
            get {

                return _data.showShortcut;
            }
            set {
                if (value != _data.showShortcut) {
                    _data.showShortcut = value;
                    UpdateMenuItem(true);
                }
            }
        }

        /// <include file='doc\MenuItem.uex' path='docs/doc[@for="MenuItem.Visible"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Gets or sets a value that indicates
        ///       whether the menu item is visible on its parent menu.
        ///    </para>
        /// </devdoc>
        [
        Localizable(true),
        DefaultValue(true),
        SRDescription(DCSR.MenuItemVisibleDescr)
        ]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public bool Visible {
            get {
                return(_data.State & STATE_HIDDEN) == 0;
            }
            set {
                _data.Visible = value;
            }
        }

        /// <include file='doc\MenuItem.uex' path='docs/doc[@for="MenuItem.Click"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Occurs when the menu item is clicked or selected using a
        ///       shortcut key defined for the menu item.
        ///    </para>
        /// </devdoc>
        [SRDescription(DCSR.MenuItemOnClickDescr)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public event EventHandler Click {
            add {
                _data.onClick += value;
            }
            remove {
                _data.onClick -= value;
            }
        }

        [SRDescription(DCSR.MenuItemOnClickDescr)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public event EventHandlerAsync ClickAsync
        {
            add
            {
                _data.onClickAsync += value;
            }
            remove
            {
                _data.onClickAsync -= value;
            }
        }

        /// <include file='doc\MenuItem.uex' path='docs/doc[@for="MenuItem.DrawItem"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Occurs when when the property of a menu item is set
        ///       to
        ///    <see langword='true'/>
        ///    and a request is made to draw the menu item.
        /// </para>
        /// </devdoc>
        [SRCategory(DCSR.CatBehavior), SRDescription(DCSR.drawItemEventDescr)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public event DrawItemEventHandler DrawItem {
            add {
                _data.onDrawItem += value;
            }
            remove {
                _data.onDrawItem -= value;
            }
        }

        /// <include file='doc\MenuItem.uex' path='docs/doc[@for="MenuItem.MeasureItem"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Occurs when when the menu needs to know the size of a
        ///       menu item before drawing it.
        ///    </para>
        /// </devdoc>
        [SRCategory(DCSR.CatBehavior), SRDescription(DCSR.measureItemEventDescr)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public event MeasureItemEventHandler MeasureItem {
            add {
                _data.onMeasureItem += value;
            }
            remove {
                _data.onMeasureItem -= value;
            }
        }

        /*
        private bool ParentIsRightToLeft {
            get {
                Menu parent = GetMainMenu();
                if (parent != null) {
                    if (parent is MenuItem && ((MainMenu)parent).RightToLeft == RightToLeft.Inherit) {
                        // recursivly go up the chain
                        return  ((MenuItem)parent).ParentIsRightToLeft;
                    } else {

                        return((MainMenu)parent).RightToLeft == RightToLeft.Yes;
                    }
                }
                else {
                    parent = GetContextMenu();
                    if (parent != null) {
                        if (parent is MenuItem && ((ContextMenu)parent).RightToLeft == RightToLeft.Inherit) {
                            // recursivly go up the chain
                            return  ((MenuItem)parent).ParentIsRightToLeft;
                        } else {
                            return((ContextMenu)parent).RightToLeft == RightToLeft.Yes;    
                        }                        
                    }
                }

                return false;
            }
        }  */

        /// <include file='doc\MenuItem.uex' path='docs/doc[@for="MenuItem.Popup"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Occurs before a menu item's list of menu items is
        ///       displayed.
        ///    </para>
        /// </devdoc>
        [SRDescription(DCSR.MenuItemOnInitDescr)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public event EventHandler Popup {
            add {
                _data.onPopup += value;
            }
            remove {
                _data.onPopup -= value;
            }
        }

        [SRDescription(DCSR.MenuItemOnInitDescr)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public event EventHandlerAsync PopupAsync
        {
            add
            {
                _data.onPopupAsync += value;
            }
            remove
            {
                _data.onPopupAsync -= value;
            }
        }

        /// <include file='doc\MenuItem.uex' path='docs/doc[@for="MenuItem.Select"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Occurs when the user hovers their mouse over a menu
        ///       item
        ///       or selects it with the keyboard but has not activated it.
        ///    </para>
        /// </devdoc>
        [SRDescription(DCSR.MenuItemOnSelectDescr)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public event EventHandler Select {
            add {
                _data.onSelect += value;
            }
            remove {
                _data.onSelect -= value;
            }
        }

        private static void CleanListItems(MenuItem senderMenu) {
            
            // remove dynamic items.
            
            for (int i = senderMenu.MenuItems.Count - 1; i >= 0; i--) {
                MenuItem item = senderMenu.MenuItems[i];
                if (item._data.UserData is MdiListUserData) {
                    // this is a dynamic item. clean it up!
                    // 
                    item.Dispose();
                    continue;
                }
            }
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public virtual MenuItem CloneMenu() {
            MenuItem newItem = new MenuItem();
            newItem.CloneMenu(this);
            return newItem;
        }

        /// <include file='doc\MenuItem.uex' path='docs/doc[@for="MenuItem.CloneMenu1"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Creates a copy of the specified menu item.
        ///    </para>
        /// </devdoc>
        [SuppressMessage("Microsoft.Performance", "CA1806:DoNotIgnoreMethodResults")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")] // Shipped in Everett
        protected void CloneMenu(MenuItem itemSrc) {
            base.CloneMenu(itemSrc);
            int state = itemSrc._data.State;
            new MenuItemData(this,
                             itemSrc.MergeType, itemSrc.MergeOrder, itemSrc.Shortcut, itemSrc.ShowShortcut,
                             itemSrc.Text, itemSrc._data.onClick, itemSrc._data.onPopup, itemSrc._data.onSelect,
                             itemSrc._data.onDrawItem, itemSrc._data.onMeasureItem);
            _data.SetState(state & STATE_CLONE_MASK, true);
        }

        internal virtual void CreateMenuItem() {
            if ((_data.State & STATE_HIDDEN) == 0) {
//                WinFormNativeMethods.MENUITEMINFO_T info = CreateMenuItemInfo();
//                WinFormUnsafeNativeMethods.InsertMenuItem(new HandleRef(menu, menu.handle), -1, true, info);

//                hasHandle = info.hSubMenu != IntPtr.Zero;
//                dataVersion = data.version;

//                menuItemIsCreated = true;
//                if(RenderIsRightToLeft) {
//                    Menu.UpdateRtl(true);
//                }

//#if DEBUG
//                WinFormNativeMethods.MENUITEMINFO_T infoVerify = new WinFormNativeMethods.MENUITEMINFO_T();
//                infoVerify.cbSize = Marshal.SizeOf(typeof(WinFormNativeMethods.MENUITEMINFO_T));
//                infoVerify.fMask = WinFormNativeMethods.MIIM_ID | WinFormNativeMethods.MIIM_STATE |
//                                   WinFormNativeMethods.MIIM_SUBMENU | WinFormNativeMethods.MIIM_TYPE;
//                WinFormUnsafeNativeMethods.GetMenuItemInfo(new HandleRef(menu, menu.handle), MenuID, false, infoVerify);
//#endif
            }
        }

        private WinFormNativeMethods.MENUITEMINFO_T CreateMenuItemInfo() {
            WinFormNativeMethods.MENUITEMINFO_T info = new WinFormNativeMethods.MENUITEMINFO_T();
            info.fMask = WinFormNativeMethods.MIIM_ID | WinFormNativeMethods.MIIM_STATE |
                         WinFormNativeMethods.MIIM_SUBMENU | WinFormNativeMethods.MIIM_TYPE | WinFormNativeMethods.MIIM_DATA;
            info.fType = _data.State & (STATE_BARBREAK | STATE_BREAK | STATE_RADIOCHECK | STATE_OWNERDRAW);

            // V7#646 - Top level menu items shouldn't have barbreak or break
            //          bits set on them...
            //
            bool isTopLevel = false;
            if (_menu == GetMainMenu()) {
                isTopLevel = true;
            }

            if (_data.caption.Equals("-")) {
                if (isTopLevel) {
                    _data.caption = " ";
                    info.fType |= WinFormNativeMethods.MFT_MENUBREAK;
                }
                else {
                    info.fType |= WinFormNativeMethods.MFT_SEPARATOR;
                }
            }

                                                                  
            info.fState = _data.State & (STATE_CHECKED | STATE_DEFAULT | STATE_DISABLED);

            info.wID = MenuID;
            if (IsParent) {
                info.hSubMenu = Handle;
            }
            info.hbmpChecked = IntPtr.Zero;
            info.hbmpUnchecked = IntPtr.Zero;

            // Assign a unique ID to this menu item object...
            //    The ID is stored in the dwItemData of the corresponding Win32 menu item, so that when we get Win32
            //    messages about the item later, we can delegate to the original object menu item object. A static
            //    hash table is used to map IDs to menu item objects.
            //
            if (_uniqueID == 0) {
                lock(allCreatedMenuItems) {
                    _uniqueID = (uint)Interlocked.Increment(ref nextUniqueID);
                    Debug.Assert(_uniqueID >= firstUniqueID); // ...check for ID range exhaustion (unlikely!)
                    // We add a weak ref wrapping a MenuItem to the static hash table, as supposed to adding the item 
                    // ref itself, to allow the item to be finalized in case it is not disposed and no longer referenced 
                    // anywhere else, hence preventing leaks. See bug#352644
                    allCreatedMenuItems.Add(_uniqueID, new WeakReference(this));
                }
            }

            // To check it's 32-bit OS or 64-bit OS.
            if (IntPtr.Size == 4) {
                // Store the unique ID in the dwItemData...
                //     For simple menu items, we can just put the unique ID in the dwItemData. But for owner-draw items,
                //     we need to point the dwItemData at an MSAAMENUINFO structure so that MSAA can get the item text.
                //     To allow us to reliably distinguish between IDs and structure pointers later on, we keep IDs in
                //     the 0xC0000000-0xFFFFFFFF range. This is the top 1Gb of unmananged process memory, where an app's
                //     heap allocations should never come from. So that we can still get the ID from the dwItemData for
                //     an owner-draw item later on, a copy of the ID is tacked onto the end of the MSAAMENUINFO structure.
                //
                if (_data.OwnerDraw)
                    info.dwItemData = AllocMsaaMenuInfo();
                else
                    info.dwItemData = (IntPtr) unchecked((int) _uniqueID);
            }
            else {
                // On Win64, there are no reserved address ranges we can use for menu item IDs. So instead we will
                // have to allocate an MSAMENUINFO heap structure for all menu items, not just owner-drawn ones.
                info.dwItemData = AllocMsaaMenuInfo();
            }
            
            // We won't render the shortcut if: 1) it's not set, 2) we're a parent, 3) we're toplevel
            //
            if (_data.showShortcut && _data.shortcut != 0 && !IsParent && !isTopLevel) {
                info.dwTypeData = _data.caption + "\t" + TypeDescriptor.GetConverter(typeof(Keys)).ConvertToString((Keys)(int)_data.shortcut);
            }
            else {
                // Windows issue: Items with empty captions sometimes block keyboard
                // access to other items in same menu.
                info.dwTypeData = (_data.caption.Length == 0 ? " " : _data.caption);
            }
            info.cch = 0;

            return info;
        }

        /// <include file='doc\MenuItem.uex' path='docs/doc[@for="MenuItem.Dispose"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Disposes the <see cref='System.Windows.Forms.MenuItem'/>.
        ///    </para>
        /// </devdoc>
        protected override void Dispose(bool disposing) {
            
            if (disposing) {
                if (_menu != null) {
                    _menu.MenuItems.Remove(this);
                }
                    
                if (_data != null) {
                    _data.RemoveItem(this);
                }
                lock(allCreatedMenuItems) {
                    allCreatedMenuItems.Remove(_uniqueID);
                }
                this._uniqueID = 0;
                
            }
            FreeMsaaMenuInfo();
            base.Dispose(disposing);
        }


        // Given a unique menu item ID, find the corresponding MenuItem
        // object, using the master lookup table of all created MenuItems.
        internal static MenuItem GetMenuItemFromUniqueID(uint uniqueID) {
                WeakReference weakRef = (WeakReference)allCreatedMenuItems[uniqueID];
                if (weakRef != null && weakRef.IsAlive) {
                    return (MenuItem)weakRef.Target;
                }
                Debug.Fail("Weakref for menu item has expired or has been removed!  Who is trying to access this ID?");
                return null;
        }

        // Given the "item data" value of a Win32 menu item, find the corresponding MenuItem object (using
        // the master lookup table of all created MenuItems). The item data may be either the unique menu
        // item ID, or a pointer to an MSAAMENUINFO structure with a copy of the unique ID tacked to the end.
        // To reliably tell IDs and structure addresses apart, IDs live in the 0xC0000000-0xFFFFFFFF range.
        // This is the top 1Gb of unmananged process memory, where an app's heap allocations should never be.
        [SuppressMessage("Microsoft.Performance", "CA1808:AvoidCallsThatBoxValueTypes")]
        internal static MenuItem GetMenuItemFromItemData(IntPtr itemData) {
            uint uniqueID = (uint) (ulong) itemData;

            if (uniqueID == 0)
                return null;

            // To check it's 32-bit OS or 64-bit OS.
            if (IntPtr.Size == 4) {
                if (uniqueID < firstUniqueID) {
                    MsaaMenuInfoWithId msaaMenuInfo = (MsaaMenuInfoWithId) Marshal.PtrToStructure(itemData, typeof(MsaaMenuInfoWithId));
                    uniqueID = msaaMenuInfo.uniqueID;
                }
            }
            else {
                // ...its always a pointer on Win64 (see CreateMenuItemInfo)
                MsaaMenuInfoWithId msaaMenuInfo = (MsaaMenuInfoWithId) Marshal.PtrToStructure(itemData, typeof(MsaaMenuInfoWithId));
                uniqueID = msaaMenuInfo.uniqueID;
            }

            return GetMenuItemFromUniqueID(uniqueID);
        }

        // MsaaMenuInfoWithId is an MSAAMENUINFO structure with a menu item ID field tacked onto the
        // end. This allows us to pass the data we need to Win32 / MSAA, and still be able to get the ID
        // out again later on, so we can delegate Win32 menu messages back to the correct MenuItem object.
        [StructLayout(LayoutKind.Sequential)]
        private struct MsaaMenuInfoWithId {
            public WinFormNativeMethods.MSAAMENUINFO msaaMenuInfo;
            public uint uniqueID;
            
            public MsaaMenuInfoWithId(string text, uint uniqueID) {
                msaaMenuInfo = new WinFormNativeMethods.MSAAMENUINFO(text);
                this.uniqueID = uniqueID;
            }
        }

        // Creates an MSAAMENUINFO structure (in the unmanaged heap) based on the current state
        // of this MenuItem object. Address of this structure is cached in the object so we can
        // free it later on using FreeMsaaMenuInfo(). If structure has already been allocated,
        // it is destroyed and a new one created.
        private IntPtr AllocMsaaMenuInfo() {
            FreeMsaaMenuInfo();
            _msaaMenuInfoPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(MsaaMenuInfoWithId)));

            // To check it's 32-bit OS or 64-bit OS.
            if (IntPtr.Size == 4) {
                // We only check this on Win32, irrelevant on Win64 (see CreateMenuItemInfo)
                Debug.Assert(((uint) (ulong) _msaaMenuInfoPtr) < firstUniqueID); // ...check for incursion into menu item ID range (unlikely!)
            }

            MsaaMenuInfoWithId msaaMenuInfoStruct = new MsaaMenuInfoWithId(_data.caption, _uniqueID);
            Marshal.StructureToPtr(msaaMenuInfoStruct, _msaaMenuInfoPtr, false);
            Debug.Assert(_msaaMenuInfoPtr != IntPtr.Zero);
            return _msaaMenuInfoPtr;
        }

        // Frees the MSAAMENUINFO structure (in the unmanaged heap) for the current MenuObject object,
        // if one has previously been allocated. Takes care to free sub-structures too, to avoid leaks!
        private void FreeMsaaMenuInfo() {
            if (_msaaMenuInfoPtr != IntPtr.Zero) {
                Marshal.DestroyStructure(_msaaMenuInfoPtr, typeof(MsaaMenuInfoWithId));
                Marshal.FreeHGlobal(_msaaMenuInfoPtr);
                _msaaMenuInfoPtr = IntPtr.Zero;
            }
        }

        internal override void ItemsChanged(int change) {
            base.ItemsChanged(change);

            if (change == CHANGE_ITEMS) {
                // when the menu collection changes deal w/ it locally
                Debug.Assert(!this.created, "base.ItemsChanged should have wiped out our handles");
                if (_menu != null && _menu.created) {
                    UpdateMenuItem(true);
                    CreateMenuItems();
                }
            } else {
                if (!_hasHandle && IsParent)
                    UpdateMenuItem(true);

                MainMenu main = GetMainMenu();
                if (main != null && ((_data.State & STATE_INMDIPOPUP) == 0)) {
                    main.ItemsChanged(change, this);
                }
            }
        }

        internal void ItemsChanged(int change, MenuItem item) {
            if (change == CHANGE_ITEMADDED &&
                this._data != null &&
                this._data.baseItem != null && 
                this._data.baseItem.MenuItems.Contains(item)) {
                if (_menu != null && _menu.created) {
                    UpdateMenuItem(true);
                    CreateMenuItems();
                } else if (this._data != null) {
                    MenuItem currentMenuItem = this._data.firstItem;
                    while (currentMenuItem != null) {
                        if (currentMenuItem.created) {
                            MenuItem newItem = item.CloneMenu();
                            item._data.AddItem(newItem);
                            currentMenuItem.MenuItems.Add(newItem);
                            // newItem.menu = currentMenuItem;
                            // newItem.CreateMenuItem();
                            break;
                        }
                        currentMenuItem = currentMenuItem._nextLinkedItem;
                    }
                }
            }
        }

        internal Form[] FindMdiForms() {
            Form[] forms = null;
            MainMenu main = GetMainMenu();
            Form menuForm = null;
            if (main != null) {
                menuForm = main.GetFormUnsafe();
            }
            if (menuForm != null) {
                forms = menuForm.MdiChildren;
            }
            if (forms == null) {
                forms = new Form[0];
            }
            return forms;
        }

        /// <devdoc> 
        ///     See the similar code in MdiWindowListStripcs::PopulateItems(), which is 
        ///     unfortunately just different enough in its working environment that we 
        ///     can't readily combine the two. But if you're fixing something here, chances
        ///     are that the same issue will need scrutiny over there.
        ///</devdoc>
        [SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters")] // "-" is OK
        private void PopulateMdiList() {
            MenuItem senderMenu = this;
            _data.SetState(STATE_INMDIPOPUP, true);
            try {
                CleanListItems(this);

                // add new items
                //
                Form[] forms = FindMdiForms();
                if (forms != null && forms.Length > 0) {

                    Form activeMdiChild = GetMainMenu().GetFormUnsafe().ActiveMdiChild;

                    if (senderMenu.MenuItems.Count > 0) {
                        // SECREVIEW : Late-binding does not represent a security threat, see bug#411899 for more info..
                        //
                        MenuItem sep = (MenuItem)Activator.CreateInstance(this.GetType());
                        sep._data.UserData = new MdiListUserData();
                        sep.Text = "-";
                        senderMenu.MenuItems.Add(sep);
                    }

                    // VSWhidbey 93540: Build a list of child windows to be displayed in
                    // the MDIList menu item...
                    // Show the first maxMenuForms visible elements of forms[] as Window menu items, except:
                    // Always show the active form, even if it's not in the first maxMenuForms visible elements of forms[].
                    // If the active form isn't in the first maxMenuForms forms, then show the first maxMenuForms-1 elements
                    // in forms[], and make the active form the last one on the menu.
                    // VSWhidbey 260405: don't count nonvisible forms against the limit on Window menu items.

                    const int maxMenuForms = 9; // Max number of Window menu items for forms
                    int visibleChildren = 0;    // the number of visible child forms (so we know to show More Windows...)
                    int accel = 1;              // prefix the form name with this digit, underlined, as an accelerator
                    int formsAddedToMenu = 0;
                    bool activeFormAdded = false;
                    for (int i = 0; i < forms.Length; i++) {
                        if (forms[i].Visible) {
                            visibleChildren++;
                            if ((activeFormAdded && (formsAddedToMenu < maxMenuForms))     ||  // don't exceed max
                                (!activeFormAdded && (formsAddedToMenu < (maxMenuForms-1)) ||  // save room for active if it's not in yet
                                (forms[i].Equals(activeMdiChild)))){                           // there's always room for activeMdiChild
                                // SECREVIEW : Late-binding does not represent a security threat, see bug#411899 for more info..
                                //
                                MenuItem windowItem = (MenuItem)Activator.CreateInstance(this.GetType());
                                windowItem._data.UserData = new MdiListFormData(this, i);
                                
                                if (forms[i].Equals(activeMdiChild)) {
                                    windowItem.Checked = true;
                                    activeFormAdded = true;
                                }
                                windowItem.Text = String.Format(CultureInfo.CurrentUICulture, "&{0} {1}", accel, forms[i].Text);
                                accel++;
                                formsAddedToMenu++;
                                senderMenu.MenuItems.Add(windowItem);
                            }
                        }
                    }

                    // VSWhidbey 93540: Display the More Windows menu option when there are more than 9 MDI
                    // Child menu items to be displayed. This is necessary because we're managing our own
                    // MDI lists, rather than letting Windows do this for us.
                    if (visibleChildren > maxMenuForms) {
                        // SECREVIEW : Late-binding does not represent a security threat, see bug#411899 for more info..
                        //
                        MenuItem moreWindows = (MenuItem)Activator.CreateInstance(this.GetType());
                        moreWindows._data.UserData = new MdiListMoreWindowsData(this);
                        moreWindows.Text = DCSR.GetString(DCSR.MDIMenuMoreWindows);
                        senderMenu.MenuItems.Add(moreWindows);
                    }
                }
            }
            finally {
                _data.SetState(STATE_INMDIPOPUP, false);
            }
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public virtual MenuItem MergeMenu() {
            // SECREVIEW : Late-binding does not represent a security threat, see bug#411899 for more info..
            //
            MenuItem newItem = (MenuItem)Activator.CreateInstance(this.GetType());
            _data.AddItem(newItem);
            newItem.MergeMenu(this);
            return newItem;
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void MergeMenu(MenuItem itemSrc) {
            base.MergeMenu(itemSrc);
            itemSrc._data.AddItem(this);
        }

        /// <include file='doc\MenuItem.uex' path='docs/doc[@for="MenuItem.OnClick"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Raises the <see cref='System.Windows.Forms.MenuItem.Click'/>
        ///       event.
        ///    </para>
        /// </devdoc>
        protected virtual void OnClick(EventArgs e) {
            try
            {
                if (_data.UserData is MdiListUserData)
                {
                    ((MdiListUserData)_data.UserData).OnClick(e);
                }
                else if (_data.baseItem != this)
                {
                    _data.baseItem.OnClick(e);
                }
                else if (_data.onClick != null)
                {
                    _data.onClick(this, e);
                }
            }
            catch( Exception ext )
            {
                Console.WriteLine("Menu:" + this.Text + " Error:" + ext.ToString());
                throw ext;
            }
        }

        internal virtual async Task OnClickAsync(EventArgs e)
        {
            try
            {
                if (_data.UserData is MdiListUserData)
                {
                    ((MdiListUserData)_data.UserData).OnClick(e);
                }
                else if (_data.baseItem != this)
                {
                    await _data.baseItem.OnClickAsync(e);
                }
                else
                {
                    if (_data.onClick != null)
                    {
                        _data.onClick(this, e);
                    }
                    if (_data.onClickAsync != null)
                    {
                        await _data.onClickAsync(this, e);
                    }
                }
            }
            catch (Exception ext)
            {
                Console.WriteLine("Menu:" + this.Text + " Error:" + ext.ToString());
                throw ext;
            }
        }

        /// <include file='doc\MenuItem.uex' path='docs/doc[@for="MenuItem.OnDrawItem"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Raises the <see cref='System.Windows.Forms.MenuItem.DrawItem'/>
        ///       event.
        ///    </para>
        /// </devdoc>
        protected virtual void OnDrawItem(DrawItemEventArgs e) {
            if (_data.baseItem != this) {
                _data.baseItem.OnDrawItem(e);
            }
            else if (_data.onDrawItem != null) {
                _data.onDrawItem(this, e);
            }
        }

        /// <include file='doc\MenuItem.uex' path='docs/doc[@for="MenuItem.OnMeasureItem"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Raises the <see cref='System.Windows.Forms.MenuItem.MeasureItem'/>
        ///       event.
        ///    </para>
        /// </devdoc>
        protected virtual void OnMeasureItem(MeasureItemEventArgs e) {
            if (_data.baseItem != this) {
                _data.baseItem.OnMeasureItem(e);
            }
            else if (_data.onMeasureItem != null) {
                _data.onMeasureItem(this, e);
            }

        }

        protected virtual void OnPopup(EventArgs e) {
            bool recreate = false;
            for (int i=0; i<ItemCount; i++) {
                if (items[i].MdiList) {
                    recreate = true;
                    items[i].UpdateMenuItem(true);
                }
            }
            if (recreate || (_hasHandle && !IsParent)) {
                UpdateMenuItem(true);
            }

            if (_data.baseItem != this) {
                _data.baseItem.OnPopup(e);
            }
            else if (_data.onPopup != null) {
                _data.onPopup(this, e);
            }

            // Update any subitem states that got changed in the event
            for (int i = 0; i < ItemCount; i++) {
                items[i].UpdateMenuItemIfDirty();
            }

            if (MdiList) {
                PopulateMdiList();
            }
        }

        protected virtual async Task OnPopupAsync(EventArgs e)
        {
            bool recreate = false;
            for (int i = 0; i < ItemCount; i++)
            {
                if (items[i].MdiList)
                {
                    recreate = true;
                    items[i].UpdateMenuItem(true);
                }
            }
            if (recreate || (_hasHandle && !IsParent))
            {
                UpdateMenuItem(true);
            }

            if (_data.baseItem != this)
            {
                await _data.baseItem.OnPopupAsync(e);
            }
            else
            {
                if (_data.onPopupAsync != null)
                {
                    await _data.onPopupAsync(this, e);
                }
                if (_data.onPopup != null)
                {
                    _data.onPopup(this, e);
                }
            }

            // Update any subitem states that got changed in the event
            for (int i = 0; i < ItemCount; i++)
            {
                items[i].UpdateMenuItemIfDirty();
            }

            if (MdiList)
            {
                PopulateMdiList();
            }
        }

        /// <include file='doc\MenuItem.uex' path='docs/doc[@for="MenuItem.OnSelect"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Raises the <see cref='System.Windows.Forms.MenuItem.Select'/>
        ///       event.
        ///    </para>
        /// </devdoc>
        protected virtual void OnSelect(EventArgs e) {
            if (_data.baseItem != this) {
                _data.baseItem.OnSelect(e);
            }
            else if (_data.onSelect != null) {
                _data.onSelect(this, e);
            }
        }

        /// <include file='doc\MenuItem.uex' path='docs/doc[@for="MenuItem.OnInitMenuPopup"]/*' />
        /// <internalonly/>
        protected virtual void OnInitMenuPopup(EventArgs e) {
            OnPopup(e);
        }

        // C#r
        internal virtual void _OnInitMenuPopup( EventArgs e ) {
            OnInitMenuPopup( e );
        }

        /// <include file='doc\MenuItem.uex' path='docs/doc[@for="MenuItem.PerformClick"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Generates a <see cref='System.Windows.Forms.Control.Click'/>
        ///       event for the MenuItem, simulating a click by a
        ///       user.
        ///    </para>
        /// </devdoc>
        public void PerformClick() {
            OnClick(EventArgs.Empty);
        }

        /// <include file='doc\MenuItem.uex' path='docs/doc[@for="MenuItem.PerformSelect"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Raises the <see cref='System.Windows.Forms.MenuItem.Select'/>
        ///       event for this menu item.
        ///    </para>
        /// </devdoc>
        public virtual void PerformSelect() {
            OnSelect(EventArgs.Empty);
        }

        internal virtual bool ShortcutClick() {
            if (_menu is MenuItem) {
                MenuItem parent = (MenuItem)_menu;
                if (!parent.ShortcutClick() || _menu != parent) return false;
            }
            if ((_data.State & STATE_DISABLED) != 0) return false;
            if (ItemCount > 0)
                OnPopup(EventArgs.Empty);
            else
                OnClick(EventArgs.Empty);
            return true;
        }

    
        /// <include file='doc\MenuItem.uex' path='docs/doc[@for="MenuItem.ToString"]/*' />
        /// <internalonly/>
        /// <devdoc>
        ///    <para>
        ///       Returns a string representation for this control.
        ///    </para>
        /// </devdoc>
        public override string ToString() {

            string s = base.ToString();
            
            String menuItemText = String.Empty;

            if (_data != null && _data.caption != null)
                menuItemText = _data.caption;

            return s + ", Text: " + menuItemText;
        }

        internal void UpdateMenuItemIfDirty() {
            if (_dataVersion != _data.version)
                UpdateMenuItem(true);
        }
        
        internal void UpdateItemRtl(bool setRightToLeftBit) {
            if(!_menuItemIsCreated) {
                return;
            }
                
            WinFormNativeMethods.MENUITEMINFO_T info = new WinFormNativeMethods.MENUITEMINFO_T();
            info.fMask          = WinFormNativeMethods.MIIM_TYPE | WinFormNativeMethods.MIIM_STATE | WinFormNativeMethods.MIIM_SUBMENU;
            info.dwTypeData     = new string('\0', Text.Length + 2);
            info.cbSize         = Marshal.SizeOf(typeof(WinFormNativeMethods.MENUITEMINFO_T));
            info.cch            = info.dwTypeData.Length - 1;
            WinFormUnsafeNativeMethods.GetMenuItemInfo(new HandleRef(_menu, _menu.handle), MenuID, false, info);
            if(setRightToLeftBit) {
                info.fType |= WinFormNativeMethods.MFT_RIGHTJUSTIFY | WinFormNativeMethods.MFT_RIGHTORDER;
            } else {
                info.fType &= ~(WinFormNativeMethods.MFT_RIGHTJUSTIFY | WinFormNativeMethods.MFT_RIGHTORDER);
            }            
            WinFormUnsafeNativeMethods.SetMenuItemInfo(new HandleRef(_menu, _menu.handle), MenuID, false, info);

#if DEBUG

            info.fMask          = WinFormNativeMethods.MIIM_TYPE | WinFormNativeMethods.MIIM_STATE | WinFormNativeMethods.MIIM_SUBMENU;
            info.dwTypeData     = new string('\0', 256);
            info.cbSize         = Marshal.SizeOf(typeof(WinFormNativeMethods.MENUITEMINFO_T));
            info.cch            = info.dwTypeData.Length - 1;
            WinFormUnsafeNativeMethods.GetMenuItemInfo(new HandleRef(_menu, _menu.handle), MenuID, false, info);
            Debug.Assert(((info.fType & WinFormNativeMethods.MFT_RIGHTORDER) != 0) == setRightToLeftBit, "Failed to set bit!");
            
#endif
        }

       

        internal void UpdateMenuItem(bool force) {
//            if (menu == null || !menu.created) {
//                return;
//            }

//            if (force || menu is MainMenu || menu is ContextMenu) {
//                WinFormNativeMethods.MENUITEMINFO_T info = CreateMenuItemInfo();
//                WinFormUnsafeNativeMethods.SetMenuItemInfo(new HandleRef(menu, menu.handle), MenuID, false, info);
//#if DEBUG
//                WinFormNativeMethods.MENUITEMINFO_T infoVerify = new WinFormNativeMethods.MENUITEMINFO_T();
//                infoVerify.cbSize = Marshal.SizeOf(typeof(WinFormNativeMethods.MENUITEMINFO_T));
//                infoVerify.fMask = WinFormNativeMethods.MIIM_ID | WinFormNativeMethods.MIIM_STATE |
//                                   WinFormNativeMethods.MIIM_SUBMENU | WinFormNativeMethods.MIIM_TYPE;
//                WinFormUnsafeNativeMethods.GetMenuItemInfo(new HandleRef(menu, menu.handle), MenuID, false, infoVerify);
//#endif

//                if (hasHandle && info.hSubMenu == IntPtr.Zero) {
//                    ClearHandles();
//                }
//                hasHandle = info.hSubMenu != IntPtr.Zero;
//                dataVersion = data.version;
//                if (menu is MainMenu) {                    
//                    Form f = ((MainMenu)menu).GetFormUnsafe();
//                    if (f != null) {
//                        WinFormSafeNativeMethods.DrawMenuBar(new HandleRef(f, f.Handle));
//                    }
//                }
//            }
        }

        /// <include file='doc\MenuItem.uex' path='docs/doc[@for="MenuItem.WmDrawItem"]/*' />
        /// <devdoc>
        /// </devdoc>
        /// <internalonly/>
        internal void WmDrawItem(ref Message m) {

            // Handles the OnDrawItem message sent from ContainerControl

            WinFormNativeMethods.DRAWITEMSTRUCT dis = (WinFormNativeMethods.DRAWITEMSTRUCT)m.GetLParam(typeof(WinFormNativeMethods.DRAWITEMSTRUCT));
            Debug.WriteLineIf(Control.PaletteTracing.TraceVerbose, Handle + ": Force set palette in MenuItem drawitem");
            IntPtr oldPal = Control.SetUpPalette(dis.hDC, false /*force*/, false);
            try {
                Graphics g = Graphics.FromHdcInternal(dis.hDC);
                try {
                   OnDrawItem(new DrawItemEventArgs(g, SystemInformation.MenuFont, Rectangle.FromLTRB(dis.rcItem.left, dis.rcItem.top, dis.rcItem.right, dis.rcItem.bottom), Index, (DrawItemState)dis.itemState));
                }
                finally {
                    g.Dispose();
                }
            }
            finally {
                if (oldPal != IntPtr.Zero) {
                    WinFormSafeNativeMethods.SelectPalette(new HandleRef(null, dis.hDC), new HandleRef(null, oldPal), 0);
                }
            }

            m.Result = (IntPtr)1;
        }

        /// <include file='doc\MenuItem.uex' path='docs/doc[@for="MenuItem.WmMeasureItem"]/*' />
        /// <devdoc>
        /// </devdoc>
        /// <internalonly/>
        internal void WmMeasureItem(ref Message m) {

            // Handles the OnMeasureItem message sent from ContainerControl

            // Obtain the measure item struct
            WinFormNativeMethods.MEASUREITEMSTRUCT mis = (WinFormNativeMethods.MEASUREITEMSTRUCT)m.GetLParam(typeof(WinFormNativeMethods.MEASUREITEMSTRUCT));
            // The OnMeasureItem handler now determines the height and width of the item

            IntPtr screendc = WinFormUnsafeNativeMethods.GetDC(WinFormNativeMethods.NullHandleRef);
            Graphics graphics = Graphics.FromHdcInternal(screendc);
            MeasureItemEventArgs mie = new MeasureItemEventArgs(graphics, Index);
            try {
                   OnMeasureItem(mie);
            }
            finally {
                graphics.Dispose();
            }
            WinFormUnsafeNativeMethods.ReleaseDC(WinFormNativeMethods.NullHandleRef, new HandleRef(null, screendc));

            // Update the measure item struct with the new width and height
            mis.itemHeight = mie.ItemHeight;
            mis.itemWidth = mie.ItemWidth;
            Marshal.StructureToPtr(mis, m.LParam, false);

            m.Result = (IntPtr)1;
        }


        /// <include file='doc\MenuItem.uex' path='docs/doc[@for="MenuItem.MenuItemData"]/*' />
        /// <devdoc>
        /// </devdoc>
        internal class MenuItemData : ICommandExecutor {
            internal MenuItem baseItem;
            internal MenuItem firstItem;

            private int state;
            internal int version;
            internal MenuMerge mergeType;
            internal int mergeOrder;
            internal string caption;
            internal short mnemonic;
            internal Shortcut shortcut;
            internal bool showShortcut;
            internal EventHandler onClick;
            internal EventHandlerAsync onClickAsync;
            internal EventHandler onPopup;
            internal EventHandlerAsync onPopupAsync;
            internal EventHandler onSelect;
            internal DrawItemEventHandler onDrawItem;
            internal MeasureItemEventHandler onMeasureItem;
            
            private object userData = null;
            internal Command cmd;

            internal MenuItemData(MenuItem baseItem, MenuMerge mergeType, int mergeOrder, Shortcut shortcut, bool showShortcut,
                                  string caption, EventHandler onClick, EventHandler onPopup, EventHandler onSelect, 
                                  DrawItemEventHandler onDrawItem, MeasureItemEventHandler onMeasureItem) {
                AddItem(baseItem);
                this.mergeType = mergeType;
                this.mergeOrder = mergeOrder;
                this.shortcut = shortcut;
                this.showShortcut = showShortcut;
                this.caption = caption == null? "": caption;
                this.onClick = onClick;
                this.onPopup = onPopup;
                this.onSelect = onSelect;
                this.onDrawItem = onDrawItem;
                this.onMeasureItem = onMeasureItem;
                this.version = 1;
                this.mnemonic = -1;
            }

          
            internal bool OwnerDraw {
                get {
                    return ((State & STATE_OWNERDRAW) != 0);
                }
                set {
                    SetState(STATE_OWNERDRAW, value);
                }
            }

            internal bool MdiList {
                get {
                    return HasState(STATE_MDILIST);
                }
                set {
                    if (((state & STATE_MDILIST) != 0) != value) {
                        SetState(STATE_MDILIST, value);
                        for (MenuItem item = firstItem; item != null; item = item._nextLinkedItem) {
                            item.ItemsChanged(Menu.CHANGE_MDI);
                        }
                    }
                }
            }

            internal MenuMerge MergeType {
                get {
                    return mergeType;
                }
                set {
                    if (mergeType != value) {
                        mergeType = value;
                        ItemsChanged(Menu.CHANGE_MERGE);
                    }
                }
            }

            internal int MergeOrder {
                get {
                    return mergeOrder;
                }
                set {
                    if (mergeOrder != value) {
                        mergeOrder = value;
                        ItemsChanged(Menu.CHANGE_MERGE);
                    }
                }
            }

            internal char Mnemonic {
                get {
                    if (mnemonic == -1) {
                        mnemonic = (short) WindowsFormsUtils.GetMnemonic(caption, true);
                    }
                    return(char)mnemonic;
                }
            }

            internal int State {
                get {
                    return state;
                }
            }

            internal bool Visible  {
                get {
                    return(state & MenuItem.STATE_HIDDEN) == 0;
                }
                set {
                    if (((state & MenuItem.STATE_HIDDEN) == 0) != value) {
                        state = value? state & ~MenuItem.STATE_HIDDEN: state | MenuItem.STATE_HIDDEN;
                        ItemsChanged(Menu.CHANGE_VISIBLE);
                    }
                }
            }


            internal object UserData {
                get {
                    return userData;
                }
                set {
                    userData = value;
                }
            }

            internal void AddItem(MenuItem item) {
                if (item._data != this) {
                    if (item._data != null) {
                        item._data.RemoveItem(item);
                    }
                        
                    item._nextLinkedItem = firstItem;
                    firstItem = item;
                    if (baseItem == null) baseItem = item;
                    item._data = this;
                    item._dataVersion = 0;
                    item.UpdateMenuItem(false);
                }
            }

            public void Execute() {
                if (baseItem != null) {
                    baseItem.OnClick(EventArgs.Empty);
                }
            }

            internal int GetMenuID() {
                if (null == cmd)
                    cmd = new Command(this);
                return cmd.ID;
            }

            internal void ItemsChanged(int change) {
                for (MenuItem item = firstItem; item != null; item = item._nextLinkedItem) {
                    if (item._menu != null)
                        item._menu.ItemsChanged(change);
                }
            }

            internal void RemoveItem(MenuItem item) {
                Debug.Assert(item._data == this, "bad item passed to MenuItemData.removeItem");

                if (item == firstItem) {
                    firstItem = item._nextLinkedItem;
                }
                else {
                    MenuItem itemT;
                    for (itemT = firstItem; item != itemT._nextLinkedItem;)
                        itemT = itemT._nextLinkedItem;
                    itemT._nextLinkedItem = item._nextLinkedItem;
                }
                item._nextLinkedItem = null;
                item._data = null;
                item._dataVersion = 0;

                if (item == baseItem) {
                    baseItem = firstItem;
                }

                if (firstItem == null) {
                    // No longer needed. Toss all references and the Command object.
                    Debug.Assert(baseItem == null, "why isn't baseItem null?");
                    onClick = null;
                    onPopup = null;
                    onClickAsync = null;
                    onPopupAsync = null;
                    onSelect = null;
                    onDrawItem = null;
                    onMeasureItem = null;
                    if (cmd != null) {
                        cmd.Dispose();
                        cmd = null;
                    }
                }
            }
                                    
            internal void SetCaption(string value) {
                if (value == null)
                    value = "";
                if (!caption.Equals(value)) {
                    caption = value;
                    UpdateMenuItems();
                }

                #if DEBUG
                    if (value.Length > 0) {
                        baseItem._debugText = value;
                    }
                #endif 
            }

            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
            internal bool HasState(int flag) {
                return((State & flag) == flag);
            }

            internal void SetState(int flag, bool value) {
                if (((state & flag) != 0) != value) {
                    state = value? state | flag: state & ~flag;
                    UpdateMenuItems();
                }
            }

            internal void UpdateMenuItems() {
                version++;
                for (MenuItem item = firstItem; item != null; item = item._nextLinkedItem) {
                    item.UpdateMenuItem(true);
                }
            }
        
        }

        private class MdiListUserData {
            public virtual void OnClick(EventArgs e) {
            }
        }

        private class MdiListFormData : MdiListUserData {
            private MenuItem parent;
            private   int      boundIndex;

            public MdiListFormData(MenuItem parentItem, int boundFormIndex) {
                boundIndex = boundFormIndex;
                parent = parentItem;
            }

            public override void OnClick(EventArgs e) {
                if (boundIndex != -1) {
                    // SECREVIEW : User selected a window, that means it is OK 
                    //           : to move focus
                    //
                    //IntSecurity.ModifyFocus.Assert();
                    try {
                        Form[] forms = parent.FindMdiForms();
                        Debug.Assert(forms != null, "Didn't get a list of the MDI Forms.");
                        
                        if (forms != null && forms.Length > boundIndex) {
                            Form boundForm = forms[boundIndex];                            
                            boundForm.Activate();
                            if (boundForm.ActiveControl != null && !boundForm.ActiveControl.Focused) {
                                boundForm.ActiveControl.Focus();
                            }
                        }
                    }
                    finally {
                        //CodeAccessPermission.RevertAssert();
                    }
                }
            }
        }

        private class MdiListMoreWindowsData : MdiListUserData {

            private MenuItem parent;
            
            public MdiListMoreWindowsData(MenuItem parent)  {   
                this.parent = parent;
            }

            public override void OnClick(EventArgs e) {
                Form[] forms = parent.FindMdiForms();
                Debug.Assert(forms != null, "Didn't get a list of the MDI Forms.");
                Form active = parent.GetMainMenu().GetFormUnsafe().ActiveMdiChild;                
                Debug.Assert(active != null, "Didn't get the active MDI child");
                //if (forms != null && forms.Length > 0 && active != null) {


                    
                //    // SECREVIEW : "System" style dialog, no user code will execute, and
                //    //           : we don't want the restricted dialog options...
                //    //
                //    //IntSecurity.AllWindows.Assert();
                //    try {
                //        using (MdiWindowDialog dialog = new MdiWindowDialog()) {
                //            dialog.SetItems(active, forms);
                //            DialogResult result = dialog.ShowDialog();
                //            if (result == DialogResult.OK) {

                //                // AllWindows Assert above allows this...
                //                //
                //                dialog.ActiveChildForm.Activate();
                //                if (dialog.ActiveChildForm.ActiveControl != null && !dialog.ActiveChildForm.ActiveControl.Focused) {
                //                    dialog.ActiveChildForm.ActiveControl.Focus();
                //                }
                //            }
                //        }
                //    }
                //    finally {
                //        //CodeAccessPermission.RevertAssert();
                //    }
                //}
            }
        }
    }
}

