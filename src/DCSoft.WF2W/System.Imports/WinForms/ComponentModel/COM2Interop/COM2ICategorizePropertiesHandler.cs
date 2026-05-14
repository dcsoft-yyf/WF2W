//------------------------------------------------------------------------------
// <copyright file="COM2ICategorizePropertiesHandler.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>                                                                
//------------------------------------------------------------------------------

namespace System.Windows.Forms.ComponentModel.Com2Interop {
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Globalization;
    using Microsoft.Win32;

    [System.Security.SuppressUnmanagedCodeSecurityAttribute()]
    internal class Com2ICategorizePropertiesHandler : Com2ExtendedBrowsingHandler {

        public override Type Interface {
            get {
                return typeof(WinFormNativeMethods.ICategorizeProperties);
            }
        }

        private string GetCategoryFromObject(object obj, int dispid) {
            if (obj == null) {
                return null;
            }

            if (obj is WinFormNativeMethods.ICategorizeProperties) {
                WinFormNativeMethods.ICategorizeProperties catObj = (WinFormNativeMethods.ICategorizeProperties)obj;

                try {
                    int categoryID = 0;

                    if (WinFormNativeMethods.S_OK == catObj.MapPropertyToCategory(dispid, ref categoryID)) {
                        string categoryName = null;
                        
                        switch (categoryID) {
                            case WinFormNativeMethods.ActiveX.PROPCAT_Nil:
                                return "";
                            case WinFormNativeMethods.ActiveX.PROPCAT_Misc:
                                return DCSR.GetString(DCSR.PropertyCategoryMisc);
                            case WinFormNativeMethods.ActiveX.PROPCAT_Font:
                                return DCSR.GetString(DCSR.PropertyCategoryFont);
                            case WinFormNativeMethods.ActiveX.PROPCAT_Position:
                                return DCSR.GetString(DCSR.PropertyCategoryPosition);
                            case WinFormNativeMethods.ActiveX.PROPCAT_Appearance:
                                return DCSR.GetString(DCSR.PropertyCategoryAppearance);
                            case WinFormNativeMethods.ActiveX.PROPCAT_Behavior:
                                return DCSR.GetString(DCSR.PropertyCategoryBehavior);
                            case WinFormNativeMethods.ActiveX.PROPCAT_Data:
                                return DCSR.GetString(DCSR.PropertyCategoryData);
                            case WinFormNativeMethods.ActiveX.PROPCAT_List:
                                return DCSR.GetString(DCSR.PropertyCategoryList);
                            case WinFormNativeMethods.ActiveX.PROPCAT_Text:
                                return DCSR.GetString(DCSR.PropertyCategoryText);
                            case WinFormNativeMethods.ActiveX.PROPCAT_Scale:
                                return DCSR.GetString(DCSR.PropertyCategoryScale);
                            case WinFormNativeMethods.ActiveX.PROPCAT_DDE:
                                return DCSR.GetString(DCSR.PropertyCategoryDDE);
                        }
                        
                        if (WinFormNativeMethods.S_OK == catObj.GetCategoryName(categoryID, CultureInfo.CurrentCulture.LCID, out categoryName)) {
                            return categoryName;
                        }
                    }
                }
                catch {
                }
            }
            return null;
        }

        public override void SetupPropertyHandlers(Com2PropertyDescriptor[] propDesc) {
            if (propDesc == null) {
                return;
            }
            for (int i = 0; i < propDesc.Length; i++) {
                propDesc[i].QueryGetBaseAttributes += new GetAttributesEventHandler(this.OnGetAttributes);
            }
        }

        private void OnGetAttributes(Com2PropertyDescriptor sender, GetAttributesEvent attrEvent) {

            string cat = GetCategoryFromObject(sender.TargetObject, sender.DISPID);

            if (cat != null && cat.Length > 0) {
                attrEvent.Add(new CategoryAttribute(cat));
            }
        }
    }
}
