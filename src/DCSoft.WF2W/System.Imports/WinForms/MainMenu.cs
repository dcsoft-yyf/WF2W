//------------------------------------------------------------------------------
// <copyright file="MainMenu.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>                                                                
//------------------------------------------------------------------------------

namespace System.Windows.Forms
{
    using System.Diagnostics;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using Microsoft.Win32;
    using System.Security.Permissions;
    using System.Runtime.Versioning;
    using System.Text.Json.Nodes;
    using DCSoft;

    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = false)]
    public class MainMenu : Menu
    {
        private JsonArray BuildMenuItemsJson(Menu.MenuItemCollection menuItems)
        {
            JsonArray result = new JsonArray();
            if (menuItems == null || menuItems.Count == 0)
            {
                return result;
            }

            foreach (MenuItem item in menuItems)
            {
                if (item == null)
                {
                    continue;
                }

                JsonObject itemJson = new JsonObject();
                DCWinFormHelper.WritePropertyValueToJson(item, itemJson);

                if (item.MenuItems != null && item.MenuItems.Count > 0)
                {
                    itemJson["MenuItems"] = BuildMenuItemsJson(item.MenuItems);
                }

                result.Add(itemJson);
            }

            return result;
        }

        public JsonObject ToJsonObject()
        {
            JsonObject json = new JsonObject();
            json["Handle"] = this.Handle.ToInt32();
            json["Type"] = this.GetType().FullName;
            json["Name"] = string.IsNullOrEmpty(this.Name) ? null : this.Name;
            json["RightToLeft"] = this.RightToLeft.ToString();
            json["MenuItems"] = BuildMenuItemsJson(this.MenuItems);
            return json;
        }

        internal Form form;
        internal Form ownerForm;  // this is the form that created this menu, and is the only form allowed to dispose it.
        private RightToLeft rightToLeft = System.Windows.Forms.RightToLeft.Inherit;
        private EventHandler onCollapse;

        /// <include file='doc\MainMenu.uex' path='docs/doc[@for="MainMenu.MainMenu"]/*' />
        /// <devdoc>
        ///     Creates a new MainMenu control.
        /// </devdoc>
        public MainMenu()
            : base(null)
        {

        }

        /// <include file='doc\MainMenu.uex' path='docs/doc[@for="MainMenu.MainMenu2"]/*' />
        /// <devdoc>
        /// <para>Initializes a new instance of the <see cref='System.Windows.Forms.MainMenu'/> class with the specified container.</para>
        /// </devdoc>
        public MainMenu(IContainer container) : this()
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            container.Add(this);
        }

        /// <include file='doc\MainMenu.uex' path='docs/doc[@for="MainMenu.MainMenu1"]/*' />
        /// <devdoc>
        ///     Creates a new MainMenu control with the given items to start
        ///     with.
        /// </devdoc>
        public MainMenu(MenuItem[] items)
            : base(items)
        {

        }

        /// <include file='doc\MainMenu.uex' path='docs/doc[@for="MainMenu.Collapse"]/*' />
        /// <devdoc>
        ///    <para>[To be supplied.]</para>
        /// </devdoc>
        [SRDescription(DCSR.MainMenuCollapseDescr)]
        public event EventHandler Collapse
        {
            add
            {
                onCollapse += value;
            }
            remove
            {
                onCollapse -= value;
            }
        }


        /// <include file='doc\MainMenu.uex' path='docs/doc[@for="MainMenu.RightToLeft"]/*' />
        /// <devdoc>
        ///     This is used for international applications where the language
        ///     is written from RightToLeft. When this property is true,
        ///     text alignment and reading order will be from right to left.
        /// </devdoc>
        [
        Localizable(true),
        AmbientValue(RightToLeft.Inherit),
        SRDescription(DCSR.MenuRightToLeftDescr)
        ]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public virtual RightToLeft RightToLeft
        {
            get
            {
                if (System.Windows.Forms.RightToLeft.Inherit == rightToLeft)
                {
                    if (form != null)
                    {
                        return form.RightToLeft;
                    }
                    else
                    {
                        return RightToLeft.Inherit;
                    }
                }
                else
                {
                    return rightToLeft;
                }
            }
            set
            {

                if (!ClientUtils.IsEnumValid(value, (int)value, (int)RightToLeft.No, (int)RightToLeft.Inherit))
                {
                    throw new InvalidEnumArgumentException("RightToLeft", (int)value, typeof(RightToLeft));
                }
                if (rightToLeft != value)
                {
                    rightToLeft = value;
                    UpdateRtl((value == System.Windows.Forms.RightToLeft.Yes));
                    ItemsChanged(0);
                }

            }
        }

        internal override bool RenderIsRightToLeft
        {
            get
            {
                return (RightToLeft == System.Windows.Forms.RightToLeft.Yes && (form == null || !form.IsMirrored));
            }
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public virtual MainMenu CloneMenu()
        {
            MainMenu newMenu = new MainMenu();
            newMenu.CloneMenu(this);
            return newMenu;
        }

        /// <include file='doc\MainMenu.uex' path='docs/doc[@for="MainMenu.CreateMenuHandle"]/*' />
        /// <devdoc>
        /// </devdoc>
        /// <internalonly/>
        [ResourceExposure(ResourceScope.Process)]
        [ResourceConsumption(ResourceScope.Process)]
        protected override IntPtr CreateMenuHandle()
        {
            return WinFormUnsafeNativeMethods.CreateMenu();
        }

        /// <include file='doc\MainMenu.uex' path='docs/doc[@for="MainMenu.Dispose"]/*' />
        /// <devdoc>
        ///     Clears out this MainMenu object and discards all of it's resources.
        ///     If the menu is parented in a form, it is disconnected from that as
        ///     well.
        /// </devdoc>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (form != null && (ownerForm == null || form == ownerForm))
                {
                    form.Menu = null;
                }
            }
            base.Dispose(disposing);
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public Form GetForm()
        {
            return form;
        }

        internal Form GetFormUnsafe()
        {
            return form;
        }

        /// <include file='doc\MainMenu.uex' path='docs/doc[@for="MainMenu.ItemsChanged"]/*' />
        /// <devdoc>
        /// </devdoc>
        /// <internalonly/>
        internal override void ItemsChanged(int change)
        {
            base.ItemsChanged(change);
            if (form != null)
                form.MenuChanged(change, this);
        }

        /// <include file='doc\MainMenu.uex' path='docs/doc[@for="MainMenu.ItemsChanged1"]/*' />
        /// <devdoc>
        /// </devdoc>
        /// <internalonly/>
        internal virtual void ItemsChanged(int change, Menu menu)
        {
            if (form != null)
                form.MenuChanged(change, menu);
        }

        /// <include file='doc\MainMenu.uex' path='docs/doc[@for="MainMenu.OnCollapse"]/*' />
        /// <devdoc>
        ///     Fires the collapse event
        /// </devdoc>
        protected internal virtual void OnCollapse(EventArgs e)
        {
            if (onCollapse != null)
            {
                onCollapse(this, e);
            }
        }

        /// <include file='doc\MainMenu.uex' path='docs/doc[@for="MainMenu.ShouldSerializeRightToLeft"]/*' />
        /// <devdoc>
        ///     Returns true if the RightToLeft should be persisted in code gen.
        /// </devdoc>
        internal virtual bool ShouldSerializeRightToLeft()
        {
            if (System.Windows.Forms.RightToLeft.Inherit == RightToLeft)
            {
                return false;
            }
            return true;
        }

        /// <include file='doc\MainMenu.uex' path='docs/doc[@for="MainMenu.ToString"]/*' />
        /// <devdoc>
        ///     Returns a string representation for this control.
        /// </devdoc>
        /// <internalonly/>
        public override string ToString()
        {
            return base.ToString();
        }
    }
}