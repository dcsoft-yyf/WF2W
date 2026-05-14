//------------------------------------------------------------------------------
// <copyright file="MessageDecoder.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>                                                                
//------------------------------------------------------------------------------

/*
 */
namespace System.Windows.Forms {
    using System.Runtime.Serialization.Formatters;
    using System.Text;
    using System.Runtime.Remoting;
    using System.Diagnostics;

    using System;
    using System.Windows.Forms;


    /// <include file='doc\MessageDecoder.uex' path='docs/doc[@for="MessageDecoder"]/*' />
    /// <devdoc>
    ///     Decodes Windows messages.  This is in a separate class from Message
    ///     so we can avoid loading it in the 99% case where we don't need it.
    /// </devdoc>
    internal static class MessageDecoder {

        /// <include file='doc\MessageDecoder.uex' path='docs/doc[@for="MessageDecoder.MsgToString"]/*' />
        /// <devdoc>
        ///     Returns the symbolic name of the msg value, or null if it
        ///     isn't one of the existing constants.
        /// </devdoc>
        private static string MsgToString(int msg) {
            string text;
            switch (msg) {
                case WinFormNativeMethods.WM_NULL: text = "WM_NULL"; break;
                case WinFormNativeMethods.WM_CREATE: text = "WM_CREATE"; break;
                case WinFormNativeMethods.WM_DESTROY: text = "WM_DESTROY"; break;
                case WinFormNativeMethods.WM_MOVE: text = "WM_MOVE"; break;
                case WinFormNativeMethods.WM_SIZE: text = "WM_SIZE"; break;
                case WinFormNativeMethods.WM_ACTIVATE: text = "WM_ACTIVATE"; break;
                //case NativeMethods.WA_INACTIVE: text = "WA_INACTIVE"; break;
                //case NativeMethods.WA_ACTIVE: text = "WA_ACTIVE"; break;
                //case NativeMethods.WA_CLICKACTIVE: text = "WA_CLICKACTIVE"; break;
                case WinFormNativeMethods.WM_SETFOCUS: text = "WM_SETFOCUS"; break;
                case WinFormNativeMethods.WM_KILLFOCUS: text = "WM_KILLFOCUS"; break;
                case WinFormNativeMethods.WM_ENABLE: text = "WM_ENABLE"; break;
                case WinFormNativeMethods.WM_SETREDRAW: text = "WM_SETREDRAW"; break;
                case WinFormNativeMethods.WM_SETTEXT: text = "WM_SETTEXT"; break;
                case WinFormNativeMethods.WM_GETTEXT: text = "WM_GETTEXT"; break;
                case WinFormNativeMethods.WM_GETTEXTLENGTH: text = "WM_GETTEXTLENGTH"; break;
                case WinFormNativeMethods.WM_PAINT: text = "WM_PAINT"; break;
                case WinFormNativeMethods.WM_CLOSE: text = "WM_CLOSE"; break;
                case WinFormNativeMethods.WM_QUERYENDSESSION: text = "WM_QUERYENDSESSION"; break;
                case WinFormNativeMethods.WM_QUIT: text = "WM_QUIT"; break;
                case WinFormNativeMethods.WM_QUERYOPEN: text = "WM_QUERYOPEN"; break;
                case WinFormNativeMethods.WM_ERASEBKGND: text = "WM_ERASEBKGND"; break;
                case WinFormNativeMethods.WM_SYSCOLORCHANGE: text = "WM_SYSCOLORCHANGE"; break;
                case WinFormNativeMethods.WM_ENDSESSION: text = "WM_ENDSESSION"; break;
                case WinFormNativeMethods.WM_SHOWWINDOW: text = "WM_SHOWWINDOW"; break;
                case WinFormNativeMethods.WM_WININICHANGE: text = "WM_WININICHANGE"; break;
                //case NativeMethods.WM_SETTINGCHANGE: text = "WM_SETTINGCHANGE"; break;
                case WinFormNativeMethods.WM_DEVMODECHANGE: text = "WM_DEVMODECHANGE"; break;
                case WinFormNativeMethods.WM_ACTIVATEAPP: text = "WM_ACTIVATEAPP"; break;
                case WinFormNativeMethods.WM_FONTCHANGE: text = "WM_FONTCHANGE"; break;
                case WinFormNativeMethods.WM_TIMECHANGE: text = "WM_TIMECHANGE"; break;
                case WinFormNativeMethods.WM_CANCELMODE: text = "WM_CANCELMODE"; break;
                case WinFormNativeMethods.WM_SETCURSOR: text = "WM_SETCURSOR"; break;
                case WinFormNativeMethods.WM_MOUSEACTIVATE: text = "WM_MOUSEACTIVATE"; break;
                case WinFormNativeMethods.WM_CHILDACTIVATE: text = "WM_CHILDACTIVATE"; break;
                case WinFormNativeMethods.WM_QUEUESYNC: text = "WM_QUEUESYNC"; break;
                case WinFormNativeMethods.WM_GETMINMAXINFO: text = "WM_GETMINMAXINFO"; break;
                case WinFormNativeMethods.WM_PAINTICON: text = "WM_PAINTICON"; break;
                case WinFormNativeMethods.WM_ICONERASEBKGND: text = "WM_ICONERASEBKGND"; break;
                case WinFormNativeMethods.WM_NEXTDLGCTL: text = "WM_NEXTDLGCTL"; break;
                case WinFormNativeMethods.WM_SPOOLERSTATUS: text = "WM_SPOOLERSTATUS"; break;
                case WinFormNativeMethods.WM_DRAWITEM: text = "WM_DRAWITEM"; break;
                case WinFormNativeMethods.WM_MEASUREITEM: text = "WM_MEASUREITEM"; break;
                case WinFormNativeMethods.WM_DELETEITEM: text = "WM_DELETEITEM"; break;
                case WinFormNativeMethods.WM_VKEYTOITEM: text = "WM_VKEYTOITEM"; break;
                case WinFormNativeMethods.WM_CHARTOITEM: text = "WM_CHARTOITEM"; break;
                case WinFormNativeMethods.WM_SETFONT: text = "WM_SETFONT"; break;
                case WinFormNativeMethods.WM_GETFONT: text = "WM_GETFONT"; break;
                case WinFormNativeMethods.WM_SETHOTKEY: text = "WM_SETHOTKEY"; break;
                case WinFormNativeMethods.WM_GETHOTKEY: text = "WM_GETHOTKEY"; break;
                case WinFormNativeMethods.WM_QUERYDRAGICON: text = "WM_QUERYDRAGICON"; break;
                case WinFormNativeMethods.WM_COMPAREITEM: text = "WM_COMPAREITEM"; break;
                case WinFormNativeMethods.WM_GETOBJECT: text = "WM_GETOBJECT"; break;
                case WinFormNativeMethods.WM_COMPACTING: text = "WM_COMPACTING"; break;
                case WinFormNativeMethods.WM_COMMNOTIFY: text = "WM_COMMNOTIFY"; break;
                case WinFormNativeMethods.WM_WINDOWPOSCHANGING: text = "WM_WINDOWPOSCHANGING"; break;
                case WinFormNativeMethods.WM_WINDOWPOSCHANGED: text = "WM_WINDOWPOSCHANGED"; break;
                case WinFormNativeMethods.WM_POWER: text = "WM_POWER"; break;
                case WinFormNativeMethods.WM_COPYDATA: text = "WM_COPYDATA"; break;
                case WinFormNativeMethods.WM_CANCELJOURNAL: text = "WM_CANCELJOURNAL"; break;
                case WinFormNativeMethods.WM_NOTIFY: text = "WM_NOTIFY"; break;
                case WinFormNativeMethods.WM_INPUTLANGCHANGEREQUEST: text = "WM_INPUTLANGCHANGEREQUEST"; break;
                case WinFormNativeMethods.WM_INPUTLANGCHANGE: text = "WM_INPUTLANGCHANGE"; break;
                case WinFormNativeMethods.WM_TCARD: text = "WM_TCARD"; break;
                case WinFormNativeMethods.WM_HELP: text = "WM_HELP"; break;
                case WinFormNativeMethods.WM_USERCHANGED: text = "WM_USERCHANGED"; break;
                case WinFormNativeMethods.WM_NOTIFYFORMAT: text = "WM_NOTIFYFORMAT"; break;
                case WinFormNativeMethods.WM_CONTEXTMENU: text = "WM_CONTEXTMENU"; break;
                case WinFormNativeMethods.WM_STYLECHANGING: text = "WM_STYLECHANGING"; break;
                case WinFormNativeMethods.WM_STYLECHANGED: text = "WM_STYLECHANGED"; break;
                case WinFormNativeMethods.WM_DISPLAYCHANGE: text = "WM_DISPLAYCHANGE"; break;
                case WinFormNativeMethods.WM_GETICON: text = "WM_GETICON"; break;
                case WinFormNativeMethods.WM_SETICON: text = "WM_SETICON"; break;
                case WinFormNativeMethods.WM_NCCREATE: text = "WM_NCCREATE"; break;
                case WinFormNativeMethods.WM_NCDESTROY: text = "WM_NCDESTROY"; break;
                case WinFormNativeMethods.WM_NCCALCSIZE: text = "WM_NCCALCSIZE"; break;
                case WinFormNativeMethods.WM_NCHITTEST: text = "WM_NCHITTEST"; break;
                case WinFormNativeMethods.WM_NCPAINT: text = "WM_NCPAINT"; break;
                case WinFormNativeMethods.WM_NCACTIVATE: text = "WM_NCACTIVATE"; break;
                case WinFormNativeMethods.WM_GETDLGCODE: text = "WM_GETDLGCODE"; break;
                case WinFormNativeMethods.WM_NCMOUSEMOVE: text = "WM_NCMOUSEMOVE"; break;
                case WinFormNativeMethods.WM_NCLBUTTONDOWN: text = "WM_NCLBUTTONDOWN"; break;
                case WinFormNativeMethods.WM_NCLBUTTONUP: text = "WM_NCLBUTTONUP"; break;
                case WinFormNativeMethods.WM_NCLBUTTONDBLCLK: text = "WM_NCLBUTTONDBLCLK"; break;
                case WinFormNativeMethods.WM_NCRBUTTONDOWN: text = "WM_NCRBUTTONDOWN"; break;
                case WinFormNativeMethods.WM_NCRBUTTONUP: text = "WM_NCRBUTTONUP"; break;
                case WinFormNativeMethods.WM_NCRBUTTONDBLCLK: text = "WM_NCRBUTTONDBLCLK"; break;
                case WinFormNativeMethods.WM_NCMBUTTONDOWN: text = "WM_NCMBUTTONDOWN"; break;
                case WinFormNativeMethods.WM_NCMBUTTONUP: text = "WM_NCMBUTTONUP"; break;
                case WinFormNativeMethods.WM_NCMBUTTONDBLCLK: text = "WM_NCMBUTTONDBLCLK"; break;
                //case NativeMethods.WM_KEYFIRST: text = "WM_KEYFIRST"; break;
                case WinFormNativeMethods.WM_KEYDOWN: text = "WM_KEYDOWN"; break;
                case WinFormNativeMethods.WM_KEYUP: text = "WM_KEYUP"; break;
                case WinFormNativeMethods.WM_CHAR: text = "WM_CHAR"; break;
                case WinFormNativeMethods.WM_DEADCHAR: text = "WM_DEADCHAR"; break;
                case WinFormNativeMethods.WM_SYSKEYDOWN: text = "WM_SYSKEYDOWN"; break;
                case WinFormNativeMethods.WM_SYSKEYUP: text = "WM_SYSKEYUP"; break;
                case WinFormNativeMethods.WM_SYSCHAR: text = "WM_SYSCHAR"; break;
                case WinFormNativeMethods.WM_SYSDEADCHAR: text = "WM_SYSDEADCHAR"; break;
                case WinFormNativeMethods.WM_KEYLAST: text = "WM_KEYLAST"; break;
                case WinFormNativeMethods.WM_IME_STARTCOMPOSITION: text = "WM_IME_STARTCOMPOSITION"; break;
                case WinFormNativeMethods.WM_IME_ENDCOMPOSITION: text = "WM_IME_ENDCOMPOSITION"; break;
                case WinFormNativeMethods.WM_IME_COMPOSITION: text = "WM_IME_COMPOSITION"; break;
                //case NativeMethods.WM_IME_KEYLAST: text = "WM_IME_KEYLAST"; break;
                case WinFormNativeMethods.WM_INITDIALOG: text = "WM_INITDIALOG"; break;
                case WinFormNativeMethods.WM_COMMAND: text = "WM_COMMAND"; break;
                case WinFormNativeMethods.WM_SYSCOMMAND: text = "WM_SYSCOMMAND"; break;
                case WinFormNativeMethods.WM_TIMER: text = "WM_TIMER"; break;
                case WinFormNativeMethods.WM_HSCROLL: text = "WM_HSCROLL"; break;
                case WinFormNativeMethods.WM_VSCROLL: text = "WM_VSCROLL"; break;
                case WinFormNativeMethods.WM_INITMENU: text = "WM_INITMENU"; break;
                case WinFormNativeMethods.WM_INITMENUPOPUP: text = "WM_INITMENUPOPUP"; break;
                case WinFormNativeMethods.WM_MENUSELECT: text = "WM_MENUSELECT"; break;
                case WinFormNativeMethods.WM_MENUCHAR: text = "WM_MENUCHAR"; break;
                case WinFormNativeMethods.WM_ENTERIDLE: text = "WM_ENTERIDLE"; break;
                case WinFormNativeMethods.WM_CTLCOLORMSGBOX: text = "WM_CTLCOLORMSGBOX"; break;
                case WinFormNativeMethods.WM_CTLCOLOREDIT: text = "WM_CTLCOLOREDIT"; break;
                case WinFormNativeMethods.WM_CTLCOLORLISTBOX: text = "WM_CTLCOLORLISTBOX"; break;
                case WinFormNativeMethods.WM_CTLCOLORBTN: text = "WM_CTLCOLORBTN"; break;
                case WinFormNativeMethods.WM_CTLCOLORDLG: text = "WM_CTLCOLORDLG"; break;
                case WinFormNativeMethods.WM_CTLCOLORSCROLLBAR: text = "WM_CTLCOLORSCROLLBAR"; break;
                case WinFormNativeMethods.WM_CTLCOLORSTATIC: text = "WM_CTLCOLORSTATIC"; break;
                //case NativeMethods.WM_MOUSEFIRST: text = "WM_MOUSEFIRST"; break;
                case WinFormNativeMethods.WM_MOUSEMOVE: text = "WM_MOUSEMOVE"; break;
                case WinFormNativeMethods.WM_LBUTTONDOWN: text = "WM_LBUTTONDOWN"; break;
                case WinFormNativeMethods.WM_LBUTTONUP: text = "WM_LBUTTONUP"; break;
                case WinFormNativeMethods.WM_LBUTTONDBLCLK: text = "WM_LBUTTONDBLCLK"; break;
                case WinFormNativeMethods.WM_RBUTTONDOWN: text = "WM_RBUTTONDOWN"; break;
                case WinFormNativeMethods.WM_RBUTTONUP: text = "WM_RBUTTONUP"; break;
                case WinFormNativeMethods.WM_RBUTTONDBLCLK: text = "WM_RBUTTONDBLCLK"; break;
                case WinFormNativeMethods.WM_MBUTTONDOWN: text = "WM_MBUTTONDOWN"; break;
                case WinFormNativeMethods.WM_MBUTTONUP: text = "WM_MBUTTONUP"; break;
                case WinFormNativeMethods.WM_MBUTTONDBLCLK: text = "WM_MBUTTONDBLCLK"; break;
                case WinFormNativeMethods.WM_MOUSEWHEEL: text = "WM_MOUSEWHEEL"; break;
                //case NativeMethods.WM_MOUSELAST: text = "WM_MOUSELAST"; break;
                case WinFormNativeMethods.WM_PARENTNOTIFY: text = "WM_PARENTNOTIFY"; break;
                case WinFormNativeMethods.WM_ENTERMENULOOP: text = "WM_ENTERMENULOOP"; break;
                case WinFormNativeMethods.WM_EXITMENULOOP: text = "WM_EXITMENULOOP"; break;
                case WinFormNativeMethods.WM_NEXTMENU: text = "WM_NEXTMENU"; break;
                case WinFormNativeMethods.WM_SIZING: text = "WM_SIZING"; break;
                case WinFormNativeMethods.WM_CAPTURECHANGED: text = "WM_CAPTURECHANGED"; break;
                case WinFormNativeMethods.WM_MOVING: text = "WM_MOVING"; break;
                case WinFormNativeMethods.WM_POWERBROADCAST: text = "WM_POWERBROADCAST"; break;
                case WinFormNativeMethods.WM_DEVICECHANGE: text = "WM_DEVICECHANGE"; break;
                case WinFormNativeMethods.WM_IME_SETCONTEXT: text = "WM_IME_SETCONTEXT"; break;
                case WinFormNativeMethods.WM_IME_NOTIFY: text = "WM_IME_NOTIFY"; break;
                case WinFormNativeMethods.WM_IME_CONTROL: text = "WM_IME_CONTROL"; break;
                case WinFormNativeMethods.WM_IME_COMPOSITIONFULL: text = "WM_IME_COMPOSITIONFULL"; break;
                case WinFormNativeMethods.WM_IME_SELECT: text = "WM_IME_SELECT"; break;
                case WinFormNativeMethods.WM_IME_CHAR: text = "WM_IME_CHAR"; break;
                case WinFormNativeMethods.WM_IME_KEYDOWN: text = "WM_IME_KEYDOWN"; break;
                case WinFormNativeMethods.WM_IME_KEYUP: text = "WM_IME_KEYUP"; break;
                case WinFormNativeMethods.WM_MDICREATE: text = "WM_MDICREATE"; break;
                case WinFormNativeMethods.WM_MDIDESTROY: text = "WM_MDIDESTROY"; break;
                case WinFormNativeMethods.WM_MDIACTIVATE: text = "WM_MDIACTIVATE"; break;
                case WinFormNativeMethods.WM_MDIRESTORE: text = "WM_MDIRESTORE"; break;
                case WinFormNativeMethods.WM_MDINEXT: text = "WM_MDINEXT"; break;
                case WinFormNativeMethods.WM_MDIMAXIMIZE: text = "WM_MDIMAXIMIZE"; break;
                case WinFormNativeMethods.WM_MDITILE: text = "WM_MDITILE"; break;
                case WinFormNativeMethods.WM_MDICASCADE: text = "WM_MDICASCADE"; break;
                case WinFormNativeMethods.WM_MDIICONARRANGE: text = "WM_MDIICONARRANGE"; break;
                case WinFormNativeMethods.WM_MDIGETACTIVE: text = "WM_MDIGETACTIVE"; break;
                case WinFormNativeMethods.WM_MDISETMENU: text = "WM_MDISETMENU"; break;
                case WinFormNativeMethods.WM_ENTERSIZEMOVE: text = "WM_ENTERSIZEMOVE"; break;
                case WinFormNativeMethods.WM_EXITSIZEMOVE: text = "WM_EXITSIZEMOVE"; break;
                case WinFormNativeMethods.WM_DROPFILES: text = "WM_DROPFILES"; break;
                case WinFormNativeMethods.WM_MDIREFRESHMENU: text = "WM_MDIREFRESHMENU"; break;
                case WinFormNativeMethods.WM_MOUSEHOVER: text = "WM_MOUSEHOVER"; break;
                case WinFormNativeMethods.WM_MOUSELEAVE: text = "WM_MOUSELEAVE"; break;
                case WinFormNativeMethods.WM_CUT: text = "WM_CUT"; break;
                case WinFormNativeMethods.WM_COPY: text = "WM_COPY"; break;
                case WinFormNativeMethods.WM_PASTE: text = "WM_PASTE"; break;
                case WinFormNativeMethods.WM_CLEAR: text = "WM_CLEAR"; break;
                case WinFormNativeMethods.WM_UNDO: text = "WM_UNDO"; break;
                case WinFormNativeMethods.WM_RENDERFORMAT: text = "WM_RENDERFORMAT"; break;
                case WinFormNativeMethods.WM_RENDERALLFORMATS: text = "WM_RENDERALLFORMATS"; break;
                case WinFormNativeMethods.WM_DESTROYCLIPBOARD: text = "WM_DESTROYCLIPBOARD"; break;
                case WinFormNativeMethods.WM_DRAWCLIPBOARD: text = "WM_DRAWCLIPBOARD"; break;
                case WinFormNativeMethods.WM_PAINTCLIPBOARD: text = "WM_PAINTCLIPBOARD"; break;
                case WinFormNativeMethods.WM_VSCROLLCLIPBOARD: text = "WM_VSCROLLCLIPBOARD"; break;
                case WinFormNativeMethods.WM_SIZECLIPBOARD: text = "WM_SIZECLIPBOARD"; break;
                case WinFormNativeMethods.WM_ASKCBFORMATNAME: text = "WM_ASKCBFORMATNAME"; break;
                case WinFormNativeMethods.WM_CHANGECBCHAIN: text = "WM_CHANGECBCHAIN"; break;
                case WinFormNativeMethods.WM_HSCROLLCLIPBOARD: text = "WM_HSCROLLCLIPBOARD"; break;
                case WinFormNativeMethods.WM_QUERYNEWPALETTE: text = "WM_QUERYNEWPALETTE"; break;
                case WinFormNativeMethods.WM_PALETTEISCHANGING: text = "WM_PALETTEISCHANGING"; break;
                case WinFormNativeMethods.WM_PALETTECHANGED: text = "WM_PALETTECHANGED"; break;
                case WinFormNativeMethods.WM_HOTKEY: text = "WM_HOTKEY"; break;
                case WinFormNativeMethods.WM_PRINT: text = "WM_PRINT"; break;
                case WinFormNativeMethods.WM_PRINTCLIENT: text = "WM_PRINTCLIENT"; break;
                case WinFormNativeMethods.WM_HANDHELDFIRST: text = "WM_HANDHELDFIRST"; break;
                case WinFormNativeMethods.WM_HANDHELDLAST: text = "WM_HANDHELDLAST"; break;
                case WinFormNativeMethods.WM_AFXFIRST: text = "WM_AFXFIRST"; break;
                case WinFormNativeMethods.WM_AFXLAST: text = "WM_AFXLAST"; break;
                case WinFormNativeMethods.WM_PENWINFIRST: text = "WM_PENWINFIRST"; break;
                case WinFormNativeMethods.WM_PENWINLAST: text = "WM_PENWINLAST"; break;
                case WinFormNativeMethods.WM_APP: text = "WM_APP"; break;
                case WinFormNativeMethods.WM_USER: text = "WM_USER"; break;

                case WinFormNativeMethods.WM_CTLCOLOR: text = "WM_CTLCOLOR"; break;

                    // RichEdit messages
                //case RichTextBoxConstants.WM_CONTEXTMENU: text = "WM_CONTEXTMENU"; break;

                //case RichTextBoxConstants.WM_PRINTCLIENT: text = "WM_PRINTCLIENT"; break;

                case RichTextBoxConstants.EM_GETLIMITTEXT: text = "EM_GETLIMITTEXT"; break;

                case RichTextBoxConstants.EM_POSFROMCHAR: text = "EM_POSFROMCHAR"; break;
                case RichTextBoxConstants.EM_CHARFROMPOS: text = "EM_CHARFROMPOS"; break;

                case RichTextBoxConstants.EM_SCROLLCARET: text = "EM_SCROLLCARET"; break;
                case RichTextBoxConstants.EM_CANPASTE: text = "EM_CANPASTE"; break;
                case RichTextBoxConstants.EM_DISPLAYBAND: text = "EM_DISPLAYBAND"; break;
                case RichTextBoxConstants.EM_EXGETSEL: text = "EM_EXGETSEL"; break;
                case RichTextBoxConstants.EM_EXLIMITTEXT: text = "EM_EXLIMITTEXT"; break;
                case RichTextBoxConstants.EM_EXLINEFROMCHAR: text = "EM_EXLINEFROMCHAR"; break;
                case RichTextBoxConstants.EM_EXSETSEL: text = "EM_EXSETSEL"; break;
                case RichTextBoxConstants.EM_FINDTEXT: text = "EM_FINDTEXT"; break;
                case RichTextBoxConstants.EM_FORMATRANGE: text = "EM_FORMATRANGE"; break;
                case RichTextBoxConstants.EM_GETCHARFORMAT: text = "EM_GETCHARFORMAT"; break;
                case RichTextBoxConstants.EM_GETEVENTMASK: text = "EM_GETEVENTMASK"; break;
                case RichTextBoxConstants.EM_GETOLEINTERFACE: text = "EM_GETOLEINTERFACE"; break;
                case RichTextBoxConstants.EM_GETPARAFORMAT: text = "EM_GETPARAFORMAT"; break;
                case RichTextBoxConstants.EM_GETSELTEXT: text = "EM_GETSELTEXT"; break;
                case RichTextBoxConstants.EM_HIDESELECTION: text = "EM_HIDESELECTION"; break;
                case RichTextBoxConstants.EM_PASTESPECIAL: text = "EM_PASTESPECIAL"; break;
                case RichTextBoxConstants.EM_REQUESTRESIZE: text = "EM_REQUESTRESIZE"; break;
                case RichTextBoxConstants.EM_SELECTIONTYPE: text = "EM_SELECTIONTYPE"; break;
                case RichTextBoxConstants.EM_SETBKGNDCOLOR: text = "EM_SETBKGNDCOLOR"; break;
                case RichTextBoxConstants.EM_SETCHARFORMAT: text = "EM_SETCHARFORMAT"; break;
                case RichTextBoxConstants.EM_SETEVENTMASK: text = "EM_SETEVENTMASK"; break;
                case RichTextBoxConstants.EM_SETOLECALLBACK: text = "EM_SETOLECALLBACK"; break;
                case RichTextBoxConstants.EM_SETPARAFORMAT: text = "EM_SETPARAFORMAT"; break;
                case RichTextBoxConstants.EM_SETTARGETDEVICE: text = "EM_SETTARGETDEVICE"; break;
                case RichTextBoxConstants.EM_STREAMIN: text = "EM_STREAMIN"; break;
                case RichTextBoxConstants.EM_STREAMOUT: text = "EM_STREAMOUT"; break;
                case RichTextBoxConstants.EM_GETTEXTRANGE: text = "EM_GETTEXTRANGE"; break;
                case RichTextBoxConstants.EM_FINDWORDBREAK: text = "EM_FINDWORDBREAK"; break;
                case RichTextBoxConstants.EM_SETOPTIONS: text = "EM_SETOPTIONS"; break;
                case RichTextBoxConstants.EM_GETOPTIONS: text = "EM_GETOPTIONS"; break;
                case RichTextBoxConstants.EM_FINDTEXTEX: text = "EM_FINDTEXTEX"; break;
                case RichTextBoxConstants.EM_GETWORDBREAKPROCEX: text = "EM_GETWORDBREAKPROCEX"; break;
                case RichTextBoxConstants.EM_SETWORDBREAKPROCEX: text = "EM_SETWORDBREAKPROCEX"; break;

                    // Richedit v2.0 messages
                case RichTextBoxConstants.EM_SETUNDOLIMIT: text = "EM_SETUNDOLIMIT"; break;
                case RichTextBoxConstants.EM_REDO: text = "EM_REDO"; break;
                case RichTextBoxConstants.EM_CANREDO: text = "EM_CANREDO"; break;
                case RichTextBoxConstants.EM_GETUNDONAME: text = "EM_GETUNDONAME"; break;
                case RichTextBoxConstants.EM_GETREDONAME: text = "EM_GETREDONAME"; break;
                case RichTextBoxConstants.EM_STOPGROUPTYPING: text = "EM_STOPGROUPTYPING"; break;

                case RichTextBoxConstants.EM_SETTEXTMODE: text = "EM_SETTEXTMODE"; break;
                case RichTextBoxConstants.EM_GETTEXTMODE: text = "EM_GETTEXTMODE"; break;

                case RichTextBoxConstants.EM_AUTOURLDETECT: text = "EM_AUTOURLDETECT"; break;
                case RichTextBoxConstants.EM_GETAUTOURLDETECT: text = "EM_GETAUTOURLDETECT"; break;
                case RichTextBoxConstants.EM_SETPALETTE: text = "EM_SETPALETTE"; break;
                case RichTextBoxConstants.EM_GETTEXTEX: text = "EM_GETTEXTEX"; break;
                case RichTextBoxConstants.EM_GETTEXTLENGTHEX: text = "EM_GETTEXTLENGTHEX"; break;

                    // Asia specific messages
                case RichTextBoxConstants.EM_SETPUNCTUATION: text = "EM_SETPUNCTUATION"; break;
                case RichTextBoxConstants.EM_GETPUNCTUATION: text = "EM_GETPUNCTUATION"; break;
                case RichTextBoxConstants.EM_SETWORDWRAPMODE: text = "EM_SETWORDWRAPMODE"; break;
                case RichTextBoxConstants.EM_GETWORDWRAPMODE: text = "EM_GETWORDWRAPMODE"; break;
                case RichTextBoxConstants.EM_SETIMECOLOR: text = "EM_SETIMECOLOR"; break;
                case RichTextBoxConstants.EM_GETIMECOLOR: text = "EM_GETIMECOLOR"; break;
                case RichTextBoxConstants.EM_SETIMEOPTIONS: text = "EM_SETIMEOPTIONS"; break;
                case RichTextBoxConstants.EM_GETIMEOPTIONS: text = "EM_GETIMEOPTIONS"; break;
                case RichTextBoxConstants.EM_CONVPOSITION: text = "EM_CONVPOSITION"; break;

                case RichTextBoxConstants.EM_SETLANGOPTIONS: text = "EM_SETLANGOPTIONS"; break;
                case RichTextBoxConstants.EM_GETLANGOPTIONS: text = "EM_GETLANGOPTIONS"; break;
                case RichTextBoxConstants.EM_GETIMECOMPMODE: text = "EM_GETIMECOMPMODE"; break;

                case RichTextBoxConstants.EM_FINDTEXTW: text = "EM_FINDTEXTW"; break;
                case RichTextBoxConstants.EM_FINDTEXTEXW: text = "EM_FINDTEXTEXW"; break;

                    //Rich Edit 3.0 Asia msgs
                case RichTextBoxConstants.EM_RECONVERSION: text = "EM_RECONVERSION"; break;
                case RichTextBoxConstants.EM_SETIMEMODEBIAS: text = "EM_SETIMEMODEBIAS"; break;
                case RichTextBoxConstants.EM_GETIMEMODEBIAS: text = "EM_GETIMEMODEBIAS"; break;

                    // BiDi Specific messages
                case RichTextBoxConstants.EM_SETBIDIOPTIONS: text = "EM_SETBIDIOPTIONS"; break;
                case RichTextBoxConstants.EM_GETBIDIOPTIONS: text = "EM_GETBIDIOPTIONS"; break;

                case RichTextBoxConstants.EM_SETTYPOGRAPHYOPTIONS: text = "EM_SETTYPOGRAPHYOPTIONS"; break;
                case RichTextBoxConstants.EM_GETTYPOGRAPHYOPTIONS: text = "EM_GETTYPOGRAPHYOPTIONS"; break;

                    // Extended Edit style specific messages
                case RichTextBoxConstants.EM_SETEDITSTYLE: text = "EM_SETEDITSTYLE"; break;
                case RichTextBoxConstants.EM_GETEDITSTYLE: text = "EM_GETEDITSTYLE"; break;

                default: text = null; break;
            }

            if (text == null && ((msg & WinFormNativeMethods.WM_REFLECT) == WinFormNativeMethods.WM_REFLECT)) {
                string subtext = MsgToString(msg - WinFormNativeMethods.WM_REFLECT);
                if (subtext == null) subtext = "???";
                text = "WM_REFLECT + " + subtext;
            }

            return text;
        }

        private static string Parenthesize(string input) {
            if (input == null)
                return "";
            else
                return " (" + input + ")";
        }

#if FALSE
        // If you want to use MessageDecoder.ToString(int msg) for debugging uncomment this block.
        // Don't forget to comment it back before checking in or else you will have an FxCop error.
        public static string ToString(int msg) {
            string ID = Parenthesize(MsgToString(msg));
            return "msg=0x" + Convert.ToString(msg, 16) + ID;
        }
#endif //FALSE
        
        public static string ToString(Message message) {
            return ToString(message.HWnd, message.Msg, message.WParam, message.LParam, message.Result);
        }

        public static string ToString(IntPtr hWnd, int msg, IntPtr wparam, IntPtr lparam, IntPtr result) {
            string ID = Parenthesize(MsgToString(msg));

            string lDescription = "";
            if (msg == WinFormNativeMethods.WM_PARENTNOTIFY)
                lDescription = Parenthesize(MsgToString(WinFormNativeMethods.Util.LOWORD(wparam)));

            return "msg=0x" + Convert.ToString(msg, 16) + ID
            + " hwnd=0x" + Convert.ToString((long)hWnd, 16)
            + " wparam=0x" + Convert.ToString((long)wparam, 16)
            + " lparam=0x" + Convert.ToString((long)lparam, 16) + lDescription
            + " result=0x" + Convert.ToString((long)result, 16);
        }
    }
}

