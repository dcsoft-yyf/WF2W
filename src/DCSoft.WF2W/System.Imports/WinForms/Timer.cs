//------------------------------------------------------------------------------
// <copyright file="Timer.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>                                                                
//------------------------------------------------------------------------------

/*
 */
namespace System.Windows.Forms
{
    using System.Threading;
    using System.Runtime.InteropServices;
    using System.ComponentModel;
    using System.ComponentModel.Design;
    using System.Diagnostics;
    using System.Windows.Forms.Design;
    using System;
    using System.Globalization;

    /// <include file='doc\Timer.uex' path='docs/doc[@for="Timer"]/*' />
    /// <devdoc>
    ///    <para>Implements a Windows-based timer that raises an event at user-defined intervals. This timer is optimized for 
    ///       use in Win Forms
    ///       applications and must be used in a window.</para>
    /// </devdoc>
    [
    DefaultProperty("Interval"),
    DefaultEvent("Tick"),
    ToolboxItemFilter("System.Windows.Forms"),
    SRDescription(DCSR.DescriptionTimer)
    ]
    [System.Reflection.Obfuscation(Exclude = true , ApplyToMembers = false )]
    public class Timer : Component
    {

        /// <include file='doc\Timer.uex' path='docs/doc[@for="Timer.interval"]/*' />
        /// <devdoc>
        /// </devdoc>
        private int interval;

        /// <include file='doc\Timer.uex' path='docs/doc[@for="Timer.enabled"]/*' />
        /// <devdoc>
        /// </devdoc>
        private bool enabled;

        /// <include file='doc\Timer.uex' path='docs/doc[@for="Timer.onTimer"]/*' />
        /// <devdoc>
        /// </devdoc>
        private EventHandler onTimer;

        /// <include file='doc\Timer.uex' path='docs/doc[@for="Timer.userData"]/*' />
        /// <devdoc>
        /// </devdoc>
        private object userData;

        private static int _IDCounter = 1;
        private readonly int _timerID = ++_IDCounter;

        public Timer()
        : base()
        {
            interval = 100;
        }

        /// <include file='doc\Timer.uex' path='docs/doc[@for="Timer.Timer1"]/*' />
        /// <devdoc>
        /// <para>Initializes a new instance of the <see cref='System.Windows.Forms.Timer'/> class with the specified container.</para>
        /// </devdoc>
        public Timer(IContainer container) : this()
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            container.Add(this);
        }

        /// <include file='doc\Timer.uex' path='docs/doc[@for="Timer.Tag"]/*' />
        [
        SRCategory(DCSR.CatData),
        Localizable(false),
        Bindable(true),
        SRDescription(DCSR.ControlTagDescr),
        DefaultValue(null),
        TypeConverter(typeof(StringConverter)),
        ]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true )]
        public object Tag
        {
            get
            {
                return userData;
            }
            set
            {
                userData = value;
            }
        }

        /// <include file='doc\Timer.uex' path='docs/doc[@for="Timer.Tick"]/*' />
        /// <devdoc>
        ///    <para>Occurs when the specified timer
        ///       interval has elapsed and the timer is enabled.</para>
        /// </devdoc>
        [SRCategory(DCSR.CatBehavior), SRDescription(DCSR.TimerTimerDescr)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public event EventHandler Tick
        {
            add
            {
                onTimer += value;
            }
            remove
            {
                onTimer -= value;
            }
        }

        /// <include file='doc\Timer.uex' path='docs/doc[@for="Timer.Dispose"]/*' />
        /// <devdoc>
        ///    <para>
        ///       Disposes of the resources (other than memory) used by the timer.
        ///    </para>
        /// </devdoc>
        protected override void Dispose(bool disposing)
        {
            this.Stop();
            base.Dispose(disposing);
        }

        /// <include file='doc\Timer.uex' path='docs/doc[@for="Timer.Enabled"]/*' />
        /// <devdoc>
        ///    <para> Indicates whether the timer is
        ///       running.</para>
        /// </devdoc>
        [
        SRCategory(DCSR.CatBehavior),
        DefaultValue(false),
        SRDescription(DCSR.TimerEnabledDescr)
        ]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public virtual bool Enabled
        {
            get
            {
                return this.enabled;
            }

            set
            {
                if (this.enabled != value)
                {
                    this.enabled = value;
                    if (value)
                    {
                        DCWin32API.JSRuntime.Invoke<object>("__DCWin32API.TimerStart", this._timerID, this.Interval);
                        if(GetTimer( this._timerID ) == null )
                        {
                            _Timers.Add(new WeakReference<Timer>(this));
                        }
                    }
                    else
                    {
                        DCWin32API.JSRuntime.Invoke<object>("__DCWin32API.TimerStop", this._timerID);
                        foreach( var item in _Timers )
                        {
                            if(item.TryGetTarget( out var t2 ) && t2 == this)
                            {
                                _Timers.Remove(item);
                                break;
                            }
                        }
                    }
                }
            }
        }

        private static readonly List<WeakReference<Timer>> _Timers = new List<WeakReference<Timer>>();
        private static Timer GetTimer( int id )
        {
            Timer result = null;
            foreach(var item in _Timers)
            {
                if(item.TryGetTarget( out result ) && result._timerID == id)
                {
                    return result;
                }
            }
            return null;
        }
        internal static void RaiseTick(int timerID)
        {
            Timer timer = GetTimer(timerID);
            if (timer != null )
            {
                try
                {
                    timer.OnTick(EventArgs.Empty);
                    if (timer.enabled)
                    {
                        DCWin32API.JSRuntime.Invoke<object>("__DCWin32API.TimerStart", timer._timerID, timer.Interval);
                    }
                }
                catch( Exception ext )
                {
                    Console.WriteLine(ext.ToString());
                    throw ext;
                }
            }
        }

        /// <include file='doc\Timer.uex' path='docs/doc[@for="Timer.Interval"]/*' />
        /// <devdoc>
        ///    <para> 
        ///       Indicates the time, in milliseconds, between timer ticks.</para>
        /// </devdoc>
        [
        SRCategory(DCSR.CatBehavior),
        DefaultValue(100),
        SRDescription(DCSR.TimerIntervalDescr)
        ]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public int Interval
        {
            get
            {
                return interval;
            }
            set
            {
                if (value < 1)
                    throw new ArgumentOutOfRangeException("Interval", DCSR.GetString(DCSR.TimerInvalidInterval, value, (0).ToString(CultureInfo.CurrentCulture)));

                if (interval != value)
                {
                    interval = value;
                    if (this.Enabled)
                    {
                        this.RestartTimer();
                    }
                }
            }
        }
        private void RestartTimer()
        {
            this.Stop();
            this.Start();
        }

        /// <include file='doc\Timer.uex' path='docs/doc[@for="Timer.OnTick"]/*' />
        /// <devdoc>
        /// <para>Raises the <see cref='System.Windows.Forms.Timer.Tick'/>
        /// event.</para>
        /// </devdoc>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        protected virtual void OnTick(EventArgs e)
        {
            if (onTimer != null) onTimer(this, e);
        }

        /// <include file='doc\Timer.uex' path='docs/doc[@for="Timer.Start"]/*' />
        /// <devdoc>
        ///    <para>Starts the
        ///       timer.</para>
        /// </devdoc>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void Start()
        {
            Enabled = true;
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void Stop()
        {
            Enabled = false;
        }

        /// <include file='doc\Timer.uex' path='docs/doc[@for="Timer.ToString"]/*' />
        /// <devdoc>
        ///     returns us as a string.
        /// </devdoc>
        /// <internalonly/>
        public override string ToString()
        {
            string s = base.ToString();
            return s + ", Interval: " + Interval.ToString(CultureInfo.CurrentCulture);
        }


        //private class TimerNativeWindow : NativeWindow
        //{


        //    // the timer that owns us
        //    //
        //    private Timer _owner;

        //    // our current id -- this is usally the same as TimerID but we also
        //    // use it as a flag of when our timer is running.
        //    //
        //    private int _timerID;

        //    // arbitrary timer ID.
        //    //
        //    private static int TimerID = 1;

        //    // setting this when we are stopping the timer so someone can't restart it in the process.
        //    //
        //    private bool _stoppingTimer;

        //    internal TimerNativeWindow(Timer owner)
        //    {

        //        this._owner = owner;
        //    }

        //    ~TimerNativeWindow()
        //    {




        //        // note this call will work form the finalizer thread.
        //        //
        //        StopTimer();
        //    }

        //    public bool IsTimerRunning
        //    {
        //        get
        //        {

        //            return _timerID != 0 && Handle != IntPtr.Zero;
        //        }
        //    }


        //    // Ensures that our HWND has been created.
        //    //
        //    private bool EnsureHandle()
        //    {
        //        if (Handle == IntPtr.Zero)
        //        {


        //            // we create a totally vanilla invisible window just for WM_TIMER messages.
        //            //
        //            CreateParams cp = new CreateParams();
        //            cp.Style = 0;
        //            cp.ExStyle = 0;
        //            cp.ClassStyle = 0;
        //            cp.Caption = GetType().Name;

        //            // Message only windows are cheaper and have fewer issues than
        //            // full blown invisible windows.  But, they are only supported
        //            // on NT.
        //            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
        //            {
        //                cp.Parent = (IntPtr)WinFormNativeMethods.HWND_MESSAGE;
        //            }

        //            CreateHandle(cp);
        //        }
        //        Debug.Assert(Handle != IntPtr.Zero, "Timer HWND creation failed!");
        //        return Handle != IntPtr.Zero;
        //    }

        //    // Returns true if we need to marshal across threads to access this timer's HWND.            
        //    //
        //    private bool GetInvokeRequired(IntPtr hWnd)
        //    {
        //        if (hWnd != IntPtr.Zero)
        //        {
        //            int pid;
        //            int hwndThread = WinFormSafeNativeMethods.GetWindowThreadProcessId(new HandleRef(this, hWnd), out pid);
        //            int currentThread = WinFormSafeNativeMethods.GetCurrentThreadId();
        //            return (hwndThread != currentThread);
        //        }
        //        return false;
        //    }

        //    // change the interval of the timer without destroying the HWND.
        //    //
        //    public void RestartTimer(int newInterval)
        //    {
        //        StopTimer(false, IntPtr.Zero);
        //        StartTimer(newInterval);
        //    }

        //    // Start the timer with the specified interval.
        //    //
        //    public void StartTimer(int interval)
        //    {

        //        if (_timerID == 0 && !_stoppingTimer)
        //        {
        //            if (EnsureHandle())
        //            {
        //                _timerID = (int)WinFormSafeNativeMethods.SetTimer(new HandleRef(this, Handle), TimerID++, interval, IntPtr.Zero);
        //            }
        //        }
        //    }

        //    // stop the timer.
        //    //
        //    public void StopTimer()
        //    {

        //        StopTimer(true, IntPtr.Zero);
        //    }

        //    // stop the timer and optionally destroy the HWND.
        //    //            
        //    public void StopTimer(bool destroyHwnd, IntPtr hWnd)
        //    {


        //        if (hWnd == IntPtr.Zero)
        //        {

        //            hWnd = Handle;
        //        }

        //        // Fire a message across threads to destroy the timer and HWND on the thread that created it.
        //        //
        //        if (GetInvokeRequired(hWnd))
        //        {
        //            WinFormUnsafeNativeMethods.PostMessage(new HandleRef(this, hWnd), WinFormNativeMethods.WM_CLOSE, 0, 0);
        //            return;
        //        }

        //        // Locking 'this' here is ok since this is an internal class.  See VSW#464499.
        //        lock (this)
        //        {

        //            if (_stoppingTimer || hWnd == IntPtr.Zero || !WinFormUnsafeNativeMethods.IsWindow(new HandleRef(this, hWnd)))
        //            {

        //                return;
        //            }

        //            if (_timerID != 0)
        //            {


        //                try
        //                {
        //                    _stoppingTimer = true;
        //                    WinFormSafeNativeMethods.KillTimer(new HandleRef(this, hWnd), _timerID);
        //                }
        //                finally
        //                {
        //                    _timerID = 0;
        //                    _stoppingTimer = false;
        //                }

        //            }

        //            if (destroyHwnd)
        //            {
        //                base.DestroyHandle();
        //            }
        //        }
        //    }

        //    // Destroy the handle, stopping the timer first.
        //    //
        //    public override void DestroyHandle()
        //    {
        //        // don't recurse!
        //        //                
        //        StopTimer(false, IntPtr.Zero);
        //        Debug.Assert(_timerID == 0, "Destroying handle with timerID still set.");
        //        base.DestroyHandle();
        //    }

        //    protected override void OnThreadException(Exception e)
        //    {
        //        Application.OnThreadException(e);
        //    }

        //    public override void ReleaseHandle()
        //    {
        //        // don't recurse!
        //        //                
        //        StopTimer(false, IntPtr.Zero);
        //        Debug.Assert(_timerID == 0, "Destroying handle with timerID still set.");

        //        base.ReleaseHandle();
        //    }

        //    protected override void WndProc(ref Message m)
        //    {

        //        Debug.Assert(m.HWnd == Handle && Handle != IntPtr.Zero, "Timer getting messages for other windows?");

        //        // for timer messages, make sure they're ours (it'll be wierd if they aren't)
        //        // and call the timer event.
        //        //
        //        if (m.Msg == WinFormNativeMethods.WM_TIMER)
        //        {
        //            //Debug.Assert((int)m.WParam == _timerID, "Why are we getting a timer message that isn't ours?");
        //            if (unchecked((int)(long)m.WParam) == _timerID)
        //            {
        //                _owner.OnTick(EventArgs.Empty);
        //                return;
        //            }
        //        }
        //        else if (m.Msg == WinFormNativeMethods.WM_CLOSE)
        //        {
        //            // this is a posted method from another thread that tells us we need
        //            // to kill the timer.  The handle may already be gone, so we specify it here.
        //            //
        //            StopTimer(true, m.HWnd);
        //            return;
        //        }
        //        base.WndProc(ref m);
        //    }

        //}
    }
}
