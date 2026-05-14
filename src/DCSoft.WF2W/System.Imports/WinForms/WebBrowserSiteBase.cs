//------------------------------------------------------------------------------
// <copyright file="WebBrowserSiteBase.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>                                                                
//------------------------------------------------------------------------------

using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Configuration.Assemblies;
using System.Runtime.Remoting;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.ComponentModel;
using System.Diagnostics;
using System;
using System.Reflection;
using System.Globalization;
using System.Security.Permissions;
using Microsoft.Win32;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Drawing;    
using System.Windows.Forms.Design;
using System.Windows.Forms.ComponentModel;
using System.Windows.Forms.ComponentModel.Com2Interop;
using System.ComponentModel.Design;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Drawing.Design;
using System.Security;

namespace System.Windows.Forms {
    /// <include file='doc\WebBrowserSiteBase.uex' path='docs/doc[@for="WebBrowserSiteBase"]/*' />
    /// <devdoc>
    ///     <para>
    /// This class implements the necessary interfaces required for an ActiveX site.
    ///
    /// This class is public, but has an internal constructor so that external
    /// users can only reference the Type (cannot instantiate it directly).
    /// Other classes have to inherit this class and expose it to the outside world.
    ///
    /// This class does not have any public property/method/event by itself.
    /// All implementations of the site interface methods are private, which
    /// means that inheritors who want to override even a single method of one
    /// of these interfaces will have to implement the whole interface.
    ///     </para>
    /// </devdoc>
    public class WebBrowserSiteBase
        : WinFormUnsafeNativeMethods.IOleControlSite, WinFormUnsafeNativeMethods.IOleClientSite, WinFormUnsafeNativeMethods.IOleInPlaceSite, WinFormUnsafeNativeMethods.ISimpleFrameSite, WinFormUnsafeNativeMethods.IPropertyNotifySink, IDisposable {

        private WebBrowserBase host;
        //private AxHost.ConnectionPointCookie connectionPoint;

        //
        // The constructor takes an WebBrowserBase as a parameter, so unfortunately,
        // this cannot be used as a standalone site. It has to be used in conjunction
        // with WebBrowserBase. Perhaps we can change it in future.
        //
        internal WebBrowserSiteBase(WebBrowserBase h) {
            if (h == null) {
                throw new ArgumentNullException("h");
            }
            this.host = h;
        }

        /// <include file='doc\WebBrowserSiteBase.uex' path='docs/doc[@for="WebBrowserSiteBase.Dispose"]/*' />
        /// <devdoc>
        ///     <para>
        /// Dispose(release the cookie)
        ///     </para>
        /// </devdoc>
        public void Dispose()
        {
            Dispose(true);
        }

        /// <include file='doc\WebBrowserSiteBase.uex' path='docs/doc[@for="WebBrowserSiteBase.Dispose"]/*' />
        /// <devdoc>
        ///     <para>
        /// Release the cookie if we're disposing
        ///     </para>
        /// </devdoc>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                StopEvents();
            }
        }
        /// <include file='doc\WebBrowserSiteBase.uex' path='docs/doc[@for="WebBrowserSiteBase.Host"]/*' />
        /// <devdoc>
        ///     <para>
        /// Retrieves the WebBrowserBase object set in the constructor.
        ///     </para>
        /// </devdoc>
        internal WebBrowserBase Host {
            get {
                return this.host;
            }
        }
        
        //
        // Interface implementations:
        //
        
        //
        // IOleControlSite methods:
        //
        /// <include file='doc\WebBrowserSiteBase.uex' path='docs/doc[@for="WebBrowserSiteBase.UnsafeNativeMethods.IOleControlSite.OnControlInfoChanged"]/*' />
        /// <internalonly/>
        int WinFormUnsafeNativeMethods.IOleControlSite.OnControlInfoChanged() {
            return WinFormNativeMethods.S_OK;
        }

        /// <include file='doc\WebBrowserSiteBase.uex' path='docs/doc[@for="WebBrowserSiteBase.UnsafeNativeMethods.IOleControlSite.LockInPlaceActive"]/*' />
        /// <internalonly/>
        int WinFormUnsafeNativeMethods.IOleControlSite.LockInPlaceActive(int fLock) {
            return WinFormNativeMethods.E_NOTIMPL;
        }

        /// <include file='doc\WebBrowserSiteBase.uex' path='docs/doc[@for="WebBrowserSiteBase.UnsafeNativeMethods.IOleControlSite.GetExtendedControl"]/*' />
        /// <internalonly/>
        int WinFormUnsafeNativeMethods.IOleControlSite.GetExtendedControl(out object ppDisp) {
            ppDisp = null;
            return WinFormNativeMethods.E_NOTIMPL;
        }

        /// <include file='doc\WebBrowserSiteBase.uex' path='docs/doc[@for="WebBrowserSiteBase.UnsafeNativeMethods.IOleControlSite.TransformCoords"]/*' />
        /// <internalonly/>
        int WinFormUnsafeNativeMethods.IOleControlSite.TransformCoords(WinFormNativeMethods._POINTL pPtlHimetric, WinFormNativeMethods.tagPOINTF pPtfContainer, int dwFlags) {
            if ((dwFlags & WinFormNativeMethods.ActiveX.XFORMCOORDS_HIMETRICTOCONTAINER)  != 0) {
                if ((dwFlags & WinFormNativeMethods.ActiveX.XFORMCOORDS_SIZE) != 0) {
                    pPtfContainer.x = (float) WebBrowserHelper.HM2Pix(pPtlHimetric.x, WebBrowserHelper.LogPixelsX);
                    pPtfContainer.y = (float) WebBrowserHelper.HM2Pix(pPtlHimetric.y, WebBrowserHelper.LogPixelsY);
                }
                else if ((dwFlags & WinFormNativeMethods.ActiveX.XFORMCOORDS_POSITION) != 0) {
                    pPtfContainer.x = (float) WebBrowserHelper.HM2Pix(pPtlHimetric.x, WebBrowserHelper.LogPixelsX);
                    pPtfContainer.y = (float) WebBrowserHelper.HM2Pix(pPtlHimetric.y, WebBrowserHelper.LogPixelsY);
                }
                else {
                    return WinFormNativeMethods.E_INVALIDARG;
                }
            }
            else if ((dwFlags & WinFormNativeMethods.ActiveX.XFORMCOORDS_CONTAINERTOHIMETRIC) != 0) {
                if ((dwFlags & WinFormNativeMethods.ActiveX.XFORMCOORDS_SIZE) != 0) {
                    pPtlHimetric.x = WebBrowserHelper.Pix2HM((int)pPtfContainer.x, WebBrowserHelper.LogPixelsX);
                    pPtlHimetric.y = WebBrowserHelper.Pix2HM((int)pPtfContainer.y, WebBrowserHelper.LogPixelsY);
                }
                else if ((dwFlags & WinFormNativeMethods.ActiveX.XFORMCOORDS_POSITION) != 0) {
                    pPtlHimetric.x = WebBrowserHelper.Pix2HM((int)pPtfContainer.x, WebBrowserHelper.LogPixelsX);
                    pPtlHimetric.y = WebBrowserHelper.Pix2HM((int)pPtfContainer.y, WebBrowserHelper.LogPixelsY);
                }
                else {
                    return WinFormNativeMethods.E_INVALIDARG;
                }
            }
            else {
                return WinFormNativeMethods.E_INVALIDARG;
            }

            return WinFormNativeMethods.S_OK;
        }

        /// <include file='doc\WebBrowserSiteBase.uex' path='docs/doc[@for="WebBrowserSiteBase.UnsafeNativeMethods.IOleControlSite.TranslateAccelerator"]/*' />
        /// <internalonly/>
        int WinFormUnsafeNativeMethods.IOleControlSite.TranslateAccelerator(ref WinFormNativeMethods.MSG pMsg, int grfModifiers) {
            Debug.Assert(!this.Host.GetAXHostState(WebBrowserHelper.siteProcessedInputKey), "Re-entering UnsafeNativeMethods.IOleControlSite.TranslateAccelerator!!!");
            this.Host.SetAXHostState(WebBrowserHelper.siteProcessedInputKey, true);

            Message msg = new Message();
            msg.Msg = pMsg.message;
            msg.WParam = pMsg.wParam;
            msg.LParam = pMsg.lParam;
            msg.HWnd = pMsg.hwnd;
            
            try {
                bool f = ((Control)this.Host).PreProcessControlMessage(ref msg) == PreProcessControlState.MessageProcessed;
                return f ? WinFormNativeMethods.S_OK : WinFormNativeMethods.S_FALSE;
            }
            finally {
                this.Host.SetAXHostState(WebBrowserHelper.siteProcessedInputKey, false);
            }
        }

        /// <include file='doc\WebBrowserSiteBase.uex' path='docs/doc[@for="WebBrowserSiteBase.UnsafeNativeMethods.IOleControlSite.OnFocus"]/*' />
        /// <internalonly/>
        int WinFormUnsafeNativeMethods.IOleControlSite.OnFocus(int fGotFocus) {
            return WinFormNativeMethods.S_OK;
        }

        /// <include file='doc\WebBrowserSiteBase.uex' path='docs/doc[@for="WebBrowserSiteBase.UnsafeNativeMethods.IOleControlSite.ShowPropertyFrame"]/*' />
        /// <internalonly/>
        int WinFormUnsafeNativeMethods.IOleControlSite.ShowPropertyFrame() {
            return WinFormNativeMethods.E_NOTIMPL;
        }

        //
        // IOleClientSite methods:
        //
        /// <include file='doc\WebBrowserSiteBase.uex' path='docs/doc[@for="WebBrowserSiteBase.UnsafeNativeMethods.IOleClientSite.SaveObject"]/*' />
        /// <internalonly/>
        int WinFormUnsafeNativeMethods.IOleClientSite.SaveObject() {
            return WinFormNativeMethods.E_NOTIMPL;
        }

        /// <include file='doc\WebBrowserSiteBase.uex' path='docs/doc[@for="WebBrowserSiteBase.UnsafeNativeMethods.IOleClientSite.GetMoniker"]/*' />
        /// <internalonly/>
        int WinFormUnsafeNativeMethods.IOleClientSite.GetMoniker(int dwAssign, int dwWhichMoniker, out Object moniker) {
            moniker = null;
            return WinFormNativeMethods.E_NOTIMPL;
        }

        /// <include file='doc\WebBrowserSiteBase.uex' path='docs/doc[@for="WebBrowserSiteBase.UnsafeNativeMethods.IOleClientSite.GetContainer"]/*' />
        /// <internalonly/>
        int WinFormUnsafeNativeMethods.IOleClientSite.GetContainer(out WinFormUnsafeNativeMethods.IOleContainer container) {
            container = this.Host.GetParentContainer();
            return WinFormNativeMethods.S_OK;
        }


        /// <include file='doc\WebBrowserSiteBase.uex' path='docs/doc[@for="WebBrowserSiteBase.UnsafeNativeMethods.IOleClientSite.ShowObject"]/*' />
        /// <internalonly/>
        int WinFormUnsafeNativeMethods.IOleClientSite.ShowObject() {
            if (this.Host.ActiveXState >= WebBrowserHelper.AXState.InPlaceActive) {
                IntPtr hwnd;
                if (WinFormNativeMethods.Succeeded(this.Host.AXInPlaceObject.GetWindow(out hwnd))) {
                    if (this.Host.GetHandleNoCreate() != hwnd) {
                        if (hwnd != IntPtr.Zero) {
                            this.Host.AttachWindow(hwnd);
                            this.OnActiveXRectChange(new WinFormNativeMethods.COMRECT(this.Host.Bounds));
                        }
                    }
                }
                else if (this.Host.AXInPlaceObject is WinFormUnsafeNativeMethods.IOleInPlaceObjectWindowless) {
                    throw new InvalidOperationException(DCSR.GetString(DCSR.AXWindowlessControl));
                }
            }
            return WinFormNativeMethods.S_OK;
        }

        /// <include file='doc\WebBrowserSiteBase.uex' path='docs/doc[@for="WebBrowserSiteBase.UnsafeNativeMethods.IOleClientSite.OnShowWindow"]/*' />
        /// <internalonly/>
        int WinFormUnsafeNativeMethods.IOleClientSite.OnShowWindow(int fShow) {
            return WinFormNativeMethods.S_OK;
        }

        /// <include file='doc\WebBrowserSiteBase.uex' path='docs/doc[@for="WebBrowserSiteBase.UnsafeNativeMethods.IOleClientSite.RequestNewObjectLayout"]/*' />
        /// <internalonly/>
        int WinFormUnsafeNativeMethods.IOleClientSite.RequestNewObjectLayout() {
            return WinFormNativeMethods.E_NOTIMPL;
        }

        //
        // IOleInPlaceSite methods:
        //
        /// <include file='doc\WebBrowserSiteBase.uex' path='docs/doc[@for="WebBrowserSiteBase.UnsafeNativeMethods.IOleInPlaceSite.GetWindow"]/*' />
        /// <internalonly/>
        IntPtr WinFormUnsafeNativeMethods.IOleInPlaceSite.GetWindow() {
            try
            {
                return WinFormUnsafeNativeMethods.GetParent(new HandleRef(Host, Host.Handle));
            }
            catch (Exception t)
            {
                Debug.Fail(t.ToString());
                throw;
            }
        }

        /// <include file='doc\WebBrowserSiteBase.uex' path='docs/doc[@for="WebBrowserSiteBase.UnsafeNativeMethods.IOleInPlaceSite.ContextSensitiveHelp"]/*' />
        /// <internalonly/>
        int WinFormUnsafeNativeMethods.IOleInPlaceSite.ContextSensitiveHelp(int fEnterMode) {
            return WinFormNativeMethods.E_NOTIMPL;
        }

        /// <include file='doc\WebBrowserSiteBase.uex' path='docs/doc[@for="WebBrowserSiteBase.UnsafeNativeMethods.IOleInPlaceSite.CanInPlaceActivate"]/*' />
        /// <internalonly/>
        int WinFormUnsafeNativeMethods.IOleInPlaceSite.CanInPlaceActivate() {
            return WinFormNativeMethods.S_OK;
        }

        /// <include file='doc\WebBrowserSiteBase.uex' path='docs/doc[@for="WebBrowserSiteBase.UnsafeNativeMethods.IOleInPlaceSite.OnInPlaceActivate"]/*' />
        /// <internalonly/>
        int WinFormUnsafeNativeMethods.IOleInPlaceSite.OnInPlaceActivate() {
            this.Host.ActiveXState = WebBrowserHelper.AXState.InPlaceActive;
            this.OnActiveXRectChange(new WinFormNativeMethods.COMRECT(this.Host.Bounds));
            return WinFormNativeMethods.S_OK;
        }

        /// <include file='doc\WebBrowserSiteBase.uex' path='docs/doc[@for="WebBrowserSiteBase.UnsafeNativeMethods.IOleInPlaceSite.OnUIActivate"]/*' />
        /// <internalonly/>
        int WinFormUnsafeNativeMethods.IOleInPlaceSite.OnUIActivate() {
            this.Host.ActiveXState = WebBrowserHelper.AXState.UIActive;
            this.Host.GetParentContainer().OnUIActivate(this.Host);
            return WinFormNativeMethods.S_OK;
        }

        /// <include file='doc\WebBrowserSiteBase.uex' path='docs/doc[@for="WebBrowserSiteBase.UnsafeNativeMethods.IOleInPlaceSite.GetWindowContext"]/*' />
        /// <internalonly/>
        int WinFormUnsafeNativeMethods.IOleInPlaceSite.GetWindowContext(out WinFormUnsafeNativeMethods.IOleInPlaceFrame ppFrame, out WinFormUnsafeNativeMethods.IOleInPlaceUIWindow ppDoc,
                                             WinFormNativeMethods.COMRECT lprcPosRect, WinFormNativeMethods.COMRECT lprcClipRect, WinFormNativeMethods.tagOIFI lpFrameInfo) {
            ppDoc = null;
            ppFrame = this.Host.GetParentContainer();
            
            lprcPosRect.left = this.Host.Bounds.X;
            lprcPosRect.top = this.Host.Bounds.Y;
            lprcPosRect.right = this.Host.Bounds.Width + this.Host.Bounds.X;
            lprcPosRect.bottom = this.Host.Bounds.Height + this.Host.Bounds.Y;
            
            lprcClipRect = WebBrowserHelper.GetClipRect();
            if (lpFrameInfo != null) {
                lpFrameInfo.cb = Marshal.SizeOf(typeof(WinFormNativeMethods.tagOIFI));
                lpFrameInfo.fMDIApp = false;
                lpFrameInfo.hAccel = IntPtr.Zero;
                lpFrameInfo.cAccelEntries = 0;
                lpFrameInfo.hwndFrame = (this.Host.ParentInternal == null) ? IntPtr.Zero : this.Host.ParentInternal.Handle;
            }
            return WinFormNativeMethods.S_OK;
        }

        /// <include file='doc\WebBrowserSiteBase.uex' path='docs/doc[@for="WebBrowserSiteBase.UnsafeNativeMethods.IOleInPlaceSite.Scroll"]/*' />
        /// <internalonly/>
        int WinFormUnsafeNativeMethods.IOleInPlaceSite.Scroll(WinFormNativeMethods.tagSIZE scrollExtant) {
            return WinFormNativeMethods.S_FALSE;
        }

        /// <include file='doc\WebBrowserSiteBase.uex' path='docs/doc[@for="WebBrowserSiteBase.UnsafeNativeMethods.IOleInPlaceSite.OnUIDeactivate"]/*' />
        /// <internalonly/>
        int WinFormUnsafeNativeMethods.IOleInPlaceSite.OnUIDeactivate(int fUndoable) {
            this.Host.GetParentContainer().OnUIDeactivate(this.Host);
            if (this.Host.ActiveXState > WebBrowserHelper.AXState.InPlaceActive) {
                this.Host.ActiveXState = WebBrowserHelper.AXState.InPlaceActive;
            }
            return WinFormNativeMethods.S_OK;
        }

        /// <include file='doc\WebBrowserSiteBase.uex' path='docs/doc[@for="WebBrowserSiteBase.UnsafeNativeMethods.IOleInPlaceSite.OnInPlaceDeactivate"]/*' />
        /// <internalonly/>
        int WinFormUnsafeNativeMethods.IOleInPlaceSite.OnInPlaceDeactivate() {
            if (this.Host.ActiveXState == WebBrowserHelper.AXState.UIActive) {
                ((WinFormUnsafeNativeMethods.IOleInPlaceSite)this).OnUIDeactivate(0);
            }

            this.Host.GetParentContainer().OnInPlaceDeactivate(this.Host);
            this.Host.ActiveXState = WebBrowserHelper.AXState.Running;
            return WinFormNativeMethods.S_OK;
        }

        /// <include file='doc\WebBrowserSiteBase.uex' path='docs/doc[@for="WebBrowserSiteBase.UnsafeNativeMethods.IOleInPlaceSite.DiscardUndoState"]/*' />
        /// <internalonly/>
        int WinFormUnsafeNativeMethods.IOleInPlaceSite.DiscardUndoState() {
            return WinFormNativeMethods.S_OK;
        }

        /// <include file='doc\WebBrowserSiteBase.uex' path='docs/doc[@for="WebBrowserSiteBase.UnsafeNativeMethods.IOleInPlaceSite.DeactivateAndUndo"]/*' />
        /// <internalonly/>
        int WinFormUnsafeNativeMethods.IOleInPlaceSite.DeactivateAndUndo() {
            return this.Host.AXInPlaceObject.UIDeactivate();
        }

        /// <include file='doc\WebBrowserSiteBase.uex' path='docs/doc[@for="WebBrowserSiteBase.UnsafeNativeMethods.IOleInPlaceSite.OnPosRectChange"]/*' />
        /// <internalonly/>
        int WinFormUnsafeNativeMethods.IOleInPlaceSite.OnPosRectChange(WinFormNativeMethods.COMRECT lprcPosRect) {
            return this.OnActiveXRectChange(lprcPosRect);
        }

        //
        // ISimpleFrameSite methods:
        //
        /// <include file='doc\WebBrowserSiteBase.uex' path='docs/doc[@for="WebBrowserSiteBase.UnsafeNativeMethods.ISimpleFrameSite.PreMessageFilter"]/*' />
        /// <internalonly/>
        int WinFormUnsafeNativeMethods.ISimpleFrameSite.PreMessageFilter(IntPtr hwnd, int msg, IntPtr wp, IntPtr lp, ref IntPtr plResult, ref int pdwCookie) {
            return WinFormNativeMethods.S_OK;
        }

        /// <include file='doc\WebBrowserSiteBase.uex' path='docs/doc[@for="WebBrowserSiteBase.UnsafeNativeMethods.ISimpleFrameSite.PostMessageFilter"]/*' />
        /// <internalonly/>
        int WinFormUnsafeNativeMethods.ISimpleFrameSite.PostMessageFilter(IntPtr hwnd, int msg, IntPtr wp, IntPtr lp, ref IntPtr plResult, int dwCookie) {
            return WinFormNativeMethods.S_FALSE;
        }

        //
        // IPropertyNotifySink methods:
        //
        /// <include file='doc\WebBrowserSiteBase.uex' path='docs/doc[@for="WebBrowserSiteBase.UnsafeNativeMethods.IPropertyNotifySink.OnChanged"]/*' />
        /// <internalonly/>
        void WinFormUnsafeNativeMethods.IPropertyNotifySink.OnChanged(int dispid) {
            // Some controls fire OnChanged() notifications when getting values of some properties. ASURT 20190.
            // To prevent this kind of recursion, we check to see if we are already inside a OnChanged() call.
            //
            if (this.Host.NoComponentChangeEvents != 0)
                return;

            this.Host.NoComponentChangeEvents++;
            try
            {
                OnPropertyChanged(dispid);
            }
            catch (Exception t)
            {
                Debug.Fail(t.ToString());
                throw;
            }
            finally {
                this.Host.NoComponentChangeEvents--;
            }
        }

        /// <include file='doc\WebBrowserSiteBase.uex' path='docs/doc[@for="WebBrowserSiteBase.UnsafeNativeMethods.IPropertyNotifySink.OnRequestEdit"]/*' />
        /// <internalonly/>
        int WinFormUnsafeNativeMethods.IPropertyNotifySink.OnRequestEdit(int dispid) {
            return WinFormNativeMethods.S_OK;
        }


        //
        // Virtual overrides:
        //
        internal virtual void OnPropertyChanged(int dispid) {
            try
            {
                ISite site = this.Host.Site;
                if (site != null)
                {
                    IComponentChangeService changeService = (IComponentChangeService)site.GetService(typeof(IComponentChangeService));

                    if (changeService != null)
                    {
                        try
                        {
                            changeService.OnComponentChanging(this.Host, null);
                        }
                        catch (CheckoutException coEx)
                        {
                            if (coEx == CheckoutException.Canceled)
                            {
                                return;
                            }
                            throw coEx;
                        }

                        // Now notify the change service that the change was successful.
                        //
                        changeService.OnComponentChanged(this.Host, null, null, null);
                    }
                }
            }
            catch (Exception t)
            {
                Debug.Fail(t.ToString());
                throw;
            }
        }


        //
        // Internal helper methods:
        //
        internal WebBrowserBase GetAXHost() {
            return this.Host;
        }

        internal void StartEvents() {
            //if (connectionPoint != null)
            //    return;

            //Object nativeObject = this.Host.activeXInstance;
            //if (nativeObject != null) {
            //    try
            //    {
            //        connectionPoint = new AxHost.ConnectionPointCookie(nativeObject, this, typeof(WinFormUnsafeNativeMethods.IPropertyNotifySink));
            //    }
            //    catch (Exception ex)
            //    {
            //        if (ClientUtils.IsCriticalException(ex))
            //        {
            //            throw;
            //        }
            //    }
            //}
        }

        internal void StopEvents() {
            //if (connectionPoint != null) {
            //    connectionPoint.Disconnect();
            //    connectionPoint = null;
            //}
        }

        private int OnActiveXRectChange(WinFormNativeMethods.COMRECT lprcPosRect) {
            this.Host.AXInPlaceObject.SetObjectRects(
                WinFormNativeMethods.COMRECT.FromXYWH(0, 0, lprcPosRect.right - lprcPosRect.left, lprcPosRect.bottom - lprcPosRect.top),
                WebBrowserHelper.GetClipRect());
            this.Host.MakeDirty();
            return WinFormNativeMethods.S_OK;
        }
    }
}

