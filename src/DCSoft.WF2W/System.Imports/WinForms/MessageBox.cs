//------------------------------------------------------------------------------
// <copyright file="MessageBox.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>                                                                
//------------------------------------------------------------------------------

/*
 */
namespace System.Windows.Forms {

    //using Microsoft.Win32;
    //using System;
    //using System.ComponentModel;
    //using System.Diagnostics;
    //using System.Drawing;
    //using System.Runtime.Remoting;
    //using System.Runtime.InteropServices;
    //using System.Windows.Forms;
    //using System.Collections;
    using System.Threading.Tasks;

    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = false )]
    public class MessageBox {
        private const int IDOK             = 1;
        private const int IDCANCEL         = 2;
        private const int IDABORT          = 3;
        private const int IDRETRY          = 4;
        private const int IDIGNORE         = 5;
        private const int IDYES            = 6;
        private const int IDNO             = 7;


        private const int HELP_BUTTON      = 0x00004000;

        [ ThreadStatic ]
        private static HelpInfo[] helpInfoTable;

        

        /// <include file='doc\MessageBox.uex' path='docs/doc[@for="MessageBox.MessageBox"]/*' />
        /// <devdoc>
        ///     This constructor is private so people aren't tempted to try and create
        ///     instances of these -- they should just use the static show
        ///     methods.
        /// </devdoc>
        private MessageBox() {
        }

        private static DialogResult Win32ToDialogResult(int value) {
            switch (value) {
                case IDOK:
                    return DialogResult.OK;
                case IDCANCEL:
                    return DialogResult.Cancel;
                case IDABORT:
                    return DialogResult.Abort;
                case IDRETRY:
                    return DialogResult.Retry;
                case IDIGNORE:
                    return DialogResult.Ignore;
                case IDYES:
                    return DialogResult.Yes;
                case IDNO:
                    return DialogResult.No;
                default:
                    return DialogResult.No;
            }
        }

        
        internal static HelpInfo HelpInfo {
            get {
                // unfortunately, there's no easy way to obtain handle of a message box.
                // we'll have to rely on the fact that modal message loops have to pop off in an orderly way.

                if (helpInfoTable != null && helpInfoTable.Length > 0) {
                    // the top of the stack is actually at the end of the array.
                    return helpInfoTable[helpInfoTable.Length - 1];
                }
                
                return null;
            }
        }


        private static void PopHelpInfo() {

             //// we roll our own stack here because we want a pretty lightweight implementation.
             //// usually there's only going to be one message box shown at a time.  But if
             //// someone shows two message boxes (say by launching them via a WM_TIMER message)
             //// we've got to gracefully handle the current help info.
             //if (helpInfoTable == null) {
             //   Debug.Fail("Why are we being called when there's nothing to pop?");
                
             //} 
             //else {
             //   if (helpInfoTable.Length == 1) {
             //       helpInfoTable = null;
             //   }
             //   else {
             //      int newCount = helpInfoTable.Length -1;
             //      HelpInfo[] newTable = new HelpInfo[newCount];
             //      Array.Copy(helpInfoTable, newTable, newCount);
             //      helpInfoTable = newTable;
                
             //   }
             //}
                
        }
        private static void PushHelpInfo(HelpInfo hpi) {
            
            // we roll our own stack here because we want a pretty lightweight implementation.
            // usually there's only going to be one message box shown at a time.  But if
            // someone shows two message boxes (say by launching them via a WM_TIMER message)
            // we've got to gracefully handle the current help info.

            int lastCount = 0;
            HelpInfo[] newTable; 
   
            if (helpInfoTable == null) {
               newTable = new HelpInfo[lastCount+1];
            }
            else {       
                // if we already have a table - allocate a new slot
                lastCount = helpInfoTable.Length;
                newTable = new HelpInfo[lastCount+1];
                Array.Copy(helpInfoTable, newTable, lastCount);
            }
            newTable[lastCount] = hpi;
            helpInfoTable = newTable;
        
        }


        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, 
                                        MessageBoxDefaultButton defaultButton, MessageBoxOptions options,bool displayHelpButton) {
          
            return ShowCore(null, text, caption, buttons, icon, defaultButton, options, displayHelpButton);
        }


        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, 
                                        MessageBoxDefaultButton defaultButton, MessageBoxOptions options, string helpFilePath) {

            HelpInfo hpi = new HelpInfo(helpFilePath);
            return ShowCore(null, text, caption, buttons, icon, defaultButton, options, hpi);
        }


        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, 
                                        MessageBoxDefaultButton defaultButton, MessageBoxOptions options, string helpFilePath) {

            HelpInfo hpi = new HelpInfo(helpFilePath);
            return ShowCore(owner, text, caption, buttons, icon, defaultButton, options, hpi);
        }



        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, 
                                        MessageBoxDefaultButton defaultButton, MessageBoxOptions options, string helpFilePath, string keyword) {

            HelpInfo hpi = new HelpInfo(helpFilePath, keyword);
            return ShowCore(null, text, caption, buttons, icon, defaultButton, options, hpi);
        }


        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, 
                                        MessageBoxDefaultButton defaultButton, MessageBoxOptions options, string helpFilePath, string keyword) {

            HelpInfo hpi = new HelpInfo(helpFilePath, keyword);
            return ShowCore(owner, text, caption, buttons, icon, defaultButton, options, hpi);
        }


        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, 
                                        MessageBoxDefaultButton defaultButton, MessageBoxOptions options,string helpFilePath, HelpNavigator navigator) {

            HelpInfo hpi = new HelpInfo(helpFilePath, navigator);
            return ShowCore(null, text, caption, buttons, icon, defaultButton, options, hpi);
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, 
                                        MessageBoxDefaultButton defaultButton, MessageBoxOptions options, string helpFilePath, HelpNavigator navigator) {

            HelpInfo hpi = new HelpInfo(helpFilePath, navigator);
            return ShowCore(owner, text, caption, buttons, icon, defaultButton, options, hpi);
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, 
                                        MessageBoxDefaultButton defaultButton, MessageBoxOptions options,string helpFilePath, HelpNavigator navigator, object param) {

            HelpInfo hpi = new HelpInfo(helpFilePath, navigator, param);

            return ShowCore(null, text, caption, buttons, icon, defaultButton, options, hpi);
        }


        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, 
                                        MessageBoxDefaultButton defaultButton, MessageBoxOptions options, string helpFilePath, HelpNavigator navigator, object param) {

            HelpInfo hpi = new HelpInfo(helpFilePath, navigator, param);
      
            return ShowCore(owner, text, caption, buttons, icon, defaultButton, options, hpi);
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, 
                                        MessageBoxDefaultButton defaultButton, MessageBoxOptions options) {
            return ShowCore(null, text, caption, buttons, icon, defaultButton, options, false);
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, 
                                        MessageBoxDefaultButton defaultButton) {
            return ShowCore(null, text, caption, buttons, icon, defaultButton, 0, false);
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon) {
            return ShowCore(null, text, caption, buttons, icon, MessageBoxDefaultButton.Button1, 0, false);
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons) {
            return ShowCore(null, text, caption, buttons, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, 0, false);
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public static DialogResult Show(string text, string caption) {
            return ShowCore(null, text, caption, MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, 0, false);
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public static DialogResult Show(string text) {
            return ShowCore(null, text, String.Empty, MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, 0, false);
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, 
                                        MessageBoxDefaultButton defaultButton, MessageBoxOptions options) {
            return ShowCore(owner, text, caption, buttons, icon, defaultButton, options, false);
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, 
                                        MessageBoxDefaultButton defaultButton) {
            return ShowCore(owner, text, caption, buttons, icon, defaultButton, 0, false);
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon) {
            return ShowCore(owner, text, caption, buttons, icon, MessageBoxDefaultButton.Button1, 0, false);
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons) {
            return ShowCore(owner, text, caption, buttons, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, 0, false);
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public static DialogResult Show(IWin32Window owner, string text, string caption) {
            return ShowCore(owner, text, caption, MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, 0, false);
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public static DialogResult Show(IWin32Window owner, string text) {
            return ShowCore(owner, text, String.Empty, MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, 0, false);
        }

        private static DialogResult ShowCore(IWin32Window owner, string text, string caption,   
                                     MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton,
                                     MessageBoxOptions options, HelpInfo hpi) {
            DialogResult result = DialogResult.None;
            try {
                PushHelpInfo(hpi);
                result = ShowCore(owner, text, caption, buttons, icon, defaultButton, options, true);
            }
            finally {
                PopHelpInfo();
            }
            return result;
            
        }

        private static DialogResult ShowCore(IWin32Window owner, string text, string caption,   
                                             MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton,
                                             MessageBoxOptions options, bool showHelp) {

            var args = new System.Text.Json.Nodes.JsonObject();
            args.Add("Text", text);
            args.Add("Caption", caption);
            args.Add("Buttons", (int)buttons);
            args.Add("Icon", (int)icon);
            args.Add("DefaultButton", (int)defaultButton);
            args.Add("Options", (int)options);
            var intResult = DCWin32API.JSRuntime.Invoke<int>("__DCShowMessageBoxOld", args);
            return (DialogResult)intResult;
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public static async ValueTask<DialogResult> ShowAsync(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon,
                                        MessageBoxDefaultButton defaultButton, MessageBoxOptions options, bool displayHelpButton)
        {

            return await ShowCoreAsync(null, text, caption, buttons, icon, defaultButton, options, displayHelpButton);
        }


        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public static async ValueTask<DialogResult> ShowAsync(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon,
                                        MessageBoxDefaultButton defaultButton, MessageBoxOptions options, string helpFilePath)
        {

            HelpInfo hpi = new HelpInfo(helpFilePath);
            return await ShowCoreAsync(null, text, caption, buttons, icon, defaultButton, options, hpi);
        }


        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public static async ValueTask<DialogResult> ShowAsync(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon,
                                        MessageBoxDefaultButton defaultButton, MessageBoxOptions options, string helpFilePath)
        {

            HelpInfo hpi = new HelpInfo(helpFilePath);
            return await ShowCoreAsync(owner, text, caption, buttons, icon, defaultButton, options, hpi);
        }



        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public static async ValueTask<DialogResult> ShowAsync(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon,
                                        MessageBoxDefaultButton defaultButton, MessageBoxOptions options, string helpFilePath, string keyword)
        {

            HelpInfo hpi = new HelpInfo(helpFilePath, keyword);
            return await ShowCoreAsync(null, text, caption, buttons, icon, defaultButton, options, hpi);
        }


        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public static async ValueTask<DialogResult> ShowAsync(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon,
                                        MessageBoxDefaultButton defaultButton, MessageBoxOptions options, string helpFilePath, string keyword)
        {

            HelpInfo hpi = new HelpInfo(helpFilePath, keyword);
            return await ShowCoreAsync(owner, text, caption, buttons, icon, defaultButton, options, hpi);
        }


        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public static async ValueTask<DialogResult> ShowAsync(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon,
                                        MessageBoxDefaultButton defaultButton, MessageBoxOptions options, string helpFilePath, HelpNavigator navigator)
        {

            HelpInfo hpi = new HelpInfo(helpFilePath, navigator);
            return await ShowCoreAsync(null, text, caption, buttons, icon, defaultButton, options, hpi);
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public static async ValueTask<DialogResult> ShowAsync(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon,
                                        MessageBoxDefaultButton defaultButton, MessageBoxOptions options, string helpFilePath, HelpNavigator navigator)
        {

            HelpInfo hpi = new HelpInfo(helpFilePath, navigator);
            return await ShowCoreAsync(owner, text, caption, buttons, icon, defaultButton, options, hpi);
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public static async ValueTask<DialogResult> ShowAsync(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon,
                                        MessageBoxDefaultButton defaultButton, MessageBoxOptions options, string helpFilePath, HelpNavigator navigator, object param)
        {

            HelpInfo hpi = new HelpInfo(helpFilePath, navigator, param);

            return await ShowCoreAsync(null, text, caption, buttons, icon, defaultButton, options, hpi);
        }


        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public static async ValueTask<DialogResult> ShowAsync(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon,
                                        MessageBoxDefaultButton defaultButton, MessageBoxOptions options, string helpFilePath, HelpNavigator navigator, object param)
        {

            HelpInfo hpi = new HelpInfo(helpFilePath, navigator, param);

            return await ShowCoreAsync(owner, text, caption, buttons, icon, defaultButton, options, hpi);
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public static async ValueTask<DialogResult> ShowAsync(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon,
                                        MessageBoxDefaultButton defaultButton, MessageBoxOptions options)
        {
            return await ShowCoreAsync(null, text, caption, buttons, icon, defaultButton, options, false);
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public static async Task<DialogResult> ShowAsync(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon,
                                        MessageBoxDefaultButton defaultButton)
        {


            var result = await ShowCoreAsync(null, text, caption, buttons, icon, defaultButton, 0, false);
            return result;
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public static async ValueTask<DialogResult> ShowAsync(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            return await ShowCoreAsync(null, text, caption, buttons, icon, MessageBoxDefaultButton.Button1, 0, false);
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public static async ValueTask<DialogResult> ShowAsync(string text, string caption, MessageBoxButtons buttons)
        {
            return await ShowCoreAsync(null, text, caption, buttons, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, 0, false);
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public static async ValueTask<DialogResult> ShowAsync(string text, string caption)
        {
            return await ShowCoreAsync(null, text, caption, MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, 0, false);
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public static async ValueTask<DialogResult> ShowAsync(string text)
        {
            return await ShowCoreAsync(null, text, String.Empty, MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, 0, false);
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public static async ValueTask<DialogResult> ShowAsync(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon,
                                        MessageBoxDefaultButton defaultButton, MessageBoxOptions options)
        {
            return await ShowCoreAsync(owner, text, caption, buttons, icon, defaultButton, options, false);
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public static async ValueTask<DialogResult> ShowAsync(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon,
                                        MessageBoxDefaultButton defaultButton)
        {
            return await ShowCoreAsync(owner, text, caption, buttons, icon, defaultButton, 0, false);
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public static async ValueTask<DialogResult> ShowAsync(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            return await ShowCoreAsync(owner, text, caption, buttons, icon, MessageBoxDefaultButton.Button1, 0, false);
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public static async ValueTask<DialogResult> ShowAsync(IWin32Window owner, string text, string caption, MessageBoxButtons buttons)
        {
            return await ShowCoreAsync(owner, text, caption, buttons, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, 0, false);
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public static async ValueTask<DialogResult> ShowAsync(IWin32Window owner, string text, string caption)
        {
            return await ShowCoreAsync(owner, text, caption, MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, 0, false);
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public static async ValueTask<DialogResult> ShowAsync(IWin32Window owner, string text)
        {
            return await ShowCoreAsync(owner, text, String.Empty, MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, 0, false);
        }

        private static async ValueTask<DialogResult> ShowCoreAsync(IWin32Window owner, string text, string caption,
                                     MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton,
                                     MessageBoxOptions options, HelpInfo hpi)
        {
            DialogResult result = DialogResult.None;
            result = await ShowCoreAsync(owner, text, caption, buttons, icon, defaultButton, options, true);
            return result;
        }

        private static async ValueTask<DialogResult> ShowCoreAsync(IWin32Window owner, string text, string caption,
                                             MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton,
                                             MessageBoxOptions options, bool showHelp)
        {
            var args = new System.Text.Json.Nodes.JsonObject();
            args.Add("Text", text);
            args.Add("Caption", caption);
            args.Add("Buttons", (int)buttons);
            args.Add("Icon", (int)icon);
            args.Add("DefaultButton", (int)defaultButton);
            args.Add("Options", (int)options);
            var intResult = await DCWin32API.JSRuntime.InvokeAsync<int>("__DCShowMessageBoxNew", args);
            return (DialogResult)intResult;
        }

    }
}

