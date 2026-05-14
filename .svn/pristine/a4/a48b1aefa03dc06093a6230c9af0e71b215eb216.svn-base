//------------------------------------------------------------------------------
// <copyright file="COM2ComponentEditor.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>                                                                
//------------------------------------------------------------------------------

namespace System.Windows.Forms.ComponentModel.Com2Interop {

    using System.Runtime.Remoting;
    using System.Runtime.InteropServices;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System;    
    using System.Collections;
    using Microsoft.Win32;
    using System.Windows.Forms.Design;

    internal class Com2ComponentEditor : WindowsFormsComponentEditor {
    
        public static bool NeedsComponentEditor(object obj) {
            if (obj is WinFormNativeMethods.IPerPropertyBrowsing) {
                 // check for a property page
                 Guid guid = Guid.Empty;
                 int hr = ((WinFormNativeMethods.IPerPropertyBrowsing)obj).MapPropertyToPage(WinFormNativeMethods.MEMBERID_NIL, out guid);
                 if ((hr == WinFormNativeMethods.S_OK) && !guid.Equals(Guid.Empty)) {
                     return true;
                 }
            }
            
            if (obj is WinFormNativeMethods.ISpecifyPropertyPages) {
                 try {
                    WinFormNativeMethods.tagCAUUID uuids = new WinFormNativeMethods.tagCAUUID();
                    try {
                        ((WinFormNativeMethods.ISpecifyPropertyPages)obj).GetPages(uuids);
                        if (uuids.cElems > 0) {
                           return true;
                        }
                    }
                    finally {
                        if (uuids.pElems != IntPtr.Zero) {
                            Marshal.FreeCoTaskMem(uuids.pElems);
                        }
                    }
                 }
                 catch {
                 }
                 
                 return false;
            }
            return false;
        }
    
        [
            SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters") // This was shipped in Everett.
        ]
        public override bool EditComponent(ITypeDescriptorContext context, object obj, IWin32Window parent) {
        
                IntPtr handle = (parent == null ? IntPtr.Zero : parent.Handle);
        
                // try to get the page guid
                if (obj is WinFormNativeMethods.IPerPropertyBrowsing) {
                    // check for a property page
                    Guid guid = Guid.Empty;
                    int hr = ((WinFormNativeMethods.IPerPropertyBrowsing)obj).MapPropertyToPage(WinFormNativeMethods.MEMBERID_NIL, out guid);
                    if (hr == WinFormNativeMethods.S_OK) {
                        if (!guid.Equals(Guid.Empty)) {
                            object o = obj;
                            WinFormSafeNativeMethods.OleCreatePropertyFrame(new HandleRef(parent, handle), 0, 0, "PropertyPages", 1, ref o, 1, new Guid[]{guid}, Application.CurrentCulture.LCID, 0, IntPtr.Zero);
                            return true;
                        }
                    }
                } 
                
                if (obj is WinFormNativeMethods.ISpecifyPropertyPages) {
                    bool failed = false;
                    Exception failureException;

                    try {
                       WinFormNativeMethods.tagCAUUID uuids = new WinFormNativeMethods.tagCAUUID();
                       try {
                           ((WinFormNativeMethods.ISpecifyPropertyPages)obj).GetPages(uuids);
                           if (uuids.cElems <= 0) {
                               return false;
                           }
                       }
                       catch {
                           return false;
                       }
                       try {
                           object o = obj;
                           WinFormSafeNativeMethods.OleCreatePropertyFrame(new HandleRef(parent, handle), 0, 0, "PropertyPages", 1, ref o, uuids.cElems, new HandleRef(uuids, uuids.pElems), Application.CurrentCulture.LCID, 0, IntPtr.Zero);
                           return true;
                       }
                       finally {
                           if (uuids.pElems != IntPtr.Zero) {
                               Marshal.FreeCoTaskMem(uuids.pElems);
                           }
                       }
                  
                    }
                    catch (Exception ex1) {
                        failed = true;
                        failureException = ex1;
                    }

                    if (failed) {
                        String errString = DCSR.GetString(DCSR.ErrorPropertyPageFailed);

                        IUIService uiSvc = (context != null) ? ((IUIService) context.GetService(typeof(IUIService))) : null;
                        
                        if (uiSvc == null) {
                            RTLAwareMessageBox.Show(null, errString, DCSR.GetString(DCSR.PropertyGridTitle),
                                    MessageBoxButtons.OK, MessageBoxIcon.Error,
                                    MessageBoxDefaultButton.Button1, 0);
                        }
                        else if (failureException != null) {
                            uiSvc.ShowError(failureException, errString);
                        }
                        else {
                            uiSvc.ShowError(errString);
                        }
                    }
                }
                return false;
            }

    }

}
