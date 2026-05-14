//------------------------------------------------------------------------------
// <copyright file="Message.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>                                                                
//------------------------------------------------------------------------------

/*
 */
namespace System.Windows.Forms {
    using System.Text;
    using System.Runtime.InteropServices;
    using System.Runtime.Remoting;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Security;
    using System.Security.Permissions;
    using System;
    using System.Windows.Forms;
    using System.Runtime.CompilerServices;


    /// <include file='doc\Message.uex' path='docs/doc[@for="Message"]/*' />
    /// <devdoc>
    ///    <para> 
    ///       Implements a Windows message.</para>
    /// </devdoc>
    //[SecurityPermission(SecurityAction.LinkDemand, Flags=SecurityPermissionFlag.UnmanagedCode)]
    //[SuppressMessage("Microsoft.Security", "CA2108:ReviewDeclarativeSecurityOnValueTypes")]
    [System.Reflection.Obfuscation(Exclude = true , ApplyToMembers = false  )]
    public struct Message {
        public Message()
        {

        }
#if DEBUG
        static TraceSwitch AllWinMessages = new TraceSwitch("AllWinMessages", "Output every received message");
#endif

        IntPtr hWnd;
        int msg;
        IntPtr wparam;
        IntPtr lparam;
        IntPtr result;

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public IntPtr HWnd {
            get { return hWnd; }
            set { hWnd = value; }
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public int Msg {
            get { return msg; }
            set { msg = value; }
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public IntPtr WParam {
            get { return wparam; }
            set { wparam = value; }
        }

        internal object ObjectWParam ;

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public IntPtr LParam {
            get { return lparam; }
            set { lparam = value; }
        }
        internal object ObjectLParam = null;

        internal object GetObjectOnceFromLParam()
        {
            return DCWin32API.GetPackagedObjectByHandleOnce(this.lparam.ToInt32());
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public IntPtr Result {
             get { return result; }
             set { result = value; }
        }

        public object GetLParam(Type cls) {
            return WinFormUnsafeNativeMethods.PtrToStructure(lparam, cls);
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public static Message Create(IntPtr hWnd, int msg, IntPtr wparam, IntPtr lparam) {
            Message m = new Message();
            m.hWnd = hWnd;
            m.msg = msg;
            m.wparam = wparam;
            m.lparam = lparam;
            m.result = IntPtr.Zero;
            
#if DEBUG
            if(AllWinMessages.TraceVerbose) {
                Debug.WriteLine(m.ToString());
            }
#endif
            return m;
        }
        
        /// <include file='doc\Message.uex' path='docs/doc[@for="Message.Equals"]/*' />
        public override bool Equals(object o) {
            if (!(o is Message)) {
                return false;
            }
            
            Message m = (Message)o;
            return hWnd == m.hWnd && 
                   msg == m.msg && 
                   wparam == m.wparam && 
                   lparam == m.lparam && 
                   result == m.result;
        }

        public static bool operator !=(Message a, Message b) {
            return !a.Equals(b);
        }

        public static bool operator ==(Message a, Message b) {
            return a.Equals(b);
        }

        /// <include file='doc\Message.uex' path='docs/doc[@for="Message.GetHashCode"]/*' />
        public override int GetHashCode() {
            return (int)hWnd << 4 | msg;
        }

        /// <include file='doc\Message.uex' path='docs/doc[@for="Message.ToString"]/*' />
        /// <internalonly/>
        /// <devdoc>
        /// </devdoc>
        public override string ToString() {
            // ----URT : 151574. Link Demand on System.Windows.Forms.Message
            // fails to protect overriden methods.
            bool unrestricted = false;
            try 
            {
                //IntSecurity.UnmanagedCode.Demand();
                unrestricted = true;
            }
            catch (SecurityException)
            {
                // eat the exception.
            }
            
            if (unrestricted)
            {
                return MessageDecoder.ToString(this);
            }
            else
            {
                return base.ToString();
            }
        }
    }
}

