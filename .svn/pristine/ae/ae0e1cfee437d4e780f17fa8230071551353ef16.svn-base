//------------------------------------------------------------------------------
// <copyright file="PowerStatus.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>                                                                
//------------------------------------------------------------------------------

namespace System.Windows.Forms {

    /// <include file='doc\PowerStatus.uex' path='docs/doc[@for="ACLineStatus"]/*' />
    /// <devdoc>
    ///    <para>
    ///       To be supplied.
    ///    </para>
    /// </devdoc>
    public enum PowerLineStatus
    {
        /// <include file='doc\PowerStatus.uex' path='docs/doc[@for="PowerLineStatus.Offline"]/*' />
        /// <devdoc>
        ///     To be supplied.
        /// </devdoc>
        Offline = 0,

        /// <include file='doc\PowerStatus.uex' path='docs/doc[@for="PowerLineStatus.Online"]/*' />
        /// <devdoc>
        ///     To be supplied.
        /// </devdoc>
        Online = 1,

        /// <include file='doc\PowerStatus.uex' path='docs/doc[@for="PowerLineStatus.Unknown"]/*' />
        /// <devdoc>
        ///     To be supplied.
        /// </devdoc>
        Unknown = 255
    }

    /// <include file='doc\PowerStatus.uex' path='docs/doc[@for="BatteryChargeStatus"]/*' />
    /// <devdoc>
    ///    <para>
    ///       To be supplied.
    ///    </para>
    /// </devdoc>
    [Flags]
    public enum BatteryChargeStatus
    {
        /// <include file='doc\PowerStatus.uex' path='docs/doc[@for="BatteryChargeStatus.High"]/*' />
        /// <devdoc>
        ///     To be supplied.
        /// </devdoc>
        High = 1,

        /// <include file='doc\PowerStatus.uex' path='docs/doc[@for="BatteryChargeStatus.Low"]/*' />
        /// <devdoc>
        ///     To be supplied.
        /// </devdoc>
        Low = 2,

        /// <include file='doc\PowerStatus.uex' path='docs/doc[@for="BatteryChargeStatus.Critical"]/*' />
        /// <devdoc>
        ///     To be supplied.
        /// </devdoc>
        Critical = 4,

        /// <include file='doc\PowerStatus.uex' path='docs/doc[@for="BatteryChargeStatus.Charging"]/*' />
        /// <devdoc>
        ///     To be supplied.
        /// </devdoc>
        Charging = 8,

        /// <include file='doc\PowerStatus.uex' path='docs/doc[@for="BatteryChargeStatus.NoSystemBattery"]/*' />
        /// <devdoc>
        ///     To be supplied.
        /// </devdoc>
        NoSystemBattery = 128,

        /// <include file='doc\PowerStatus.uex' path='docs/doc[@for="BatteryChargeStatus.Unknown"]/*' />
        /// <devdoc>
        ///     To be supplied.
        /// </devdoc>
        Unknown = 255
    }

    /// <include file='doc\PowerStatus.uex' path='docs/doc[@for="PowerState"]/*' />
    /// <devdoc>
    ///    <para>
    ///       To be supplied.
    ///    </para>
    /// </devdoc>
    public enum PowerState
    {
        /// <include file='doc\PowerStatus.uex' path='docs/doc[@for="PowerState.Suspend"]/*' />
        /// <devdoc>
        ///     To be supplied.
        /// </devdoc>
        Suspend = 0,

        /// <include file='doc\PowerStatus.uex' path='docs/doc[@for="PowerState.Hibernate"]/*' />
        /// <devdoc>
        ///     To be supplied.
        /// </devdoc>
        Hibernate = 1
    }

    /// <include file='doc\PowerStatus.uex' path='docs/doc[@for="PowerStatus"]/*' />
    /// <devdoc>
    ///    <para>
    ///       To be supplied.
    ///    </para>
    /// </devdoc>
    public class PowerStatus
    {
        //private WinFormNativeMethods.SYSTEM_POWER_STATUS systemPowerStatus;

        internal PowerStatus() {
        }
        
        /// <include file='doc\PowerStatus.uex' path='docs/doc[@for="PowerStatus.ACLineStatus"]/*' />
        /// <devdoc>
        ///    <para>[To be supplied.]</para>
        /// </devdoc>
        public PowerLineStatus PowerLineStatus
        {
            get
            {
                return PowerLineStatus.Online;
                //UpdateSystemPowerStatus();
                //return (PowerLineStatus)systemPowerStatus.ACLineStatus;
            }
        }

        /// <include file='doc\PowerStatus.uex' path='docs/doc[@for="PowerStatus.BatteryChargeStatus"]/*' />
        /// <devdoc>
        ///    <para>[To be supplied.]</para>
        /// </devdoc>
        public BatteryChargeStatus BatteryChargeStatus
        {
            get
            {
                return BatteryChargeStatus.High;
                //UpdateSystemPowerStatus();
                //return (BatteryChargeStatus)systemPowerStatus.BatteryFlag;
            }
        }

        /// <include file='doc\PowerStatus.uex' path='docs/doc[@for="PowerStatus.BatteryFullLifetime"]/*' />
        /// <devdoc>
        ///    <para>[To be supplied.]</para>
        /// </devdoc>
        public int BatteryFullLifetime
        {
            get
            {
                return -1;
                //UpdateSystemPowerStatus();
                //return systemPowerStatus.BatteryFullLifeTime;
            }
        }

        /// <include file='doc\PowerStatus.uex' path='docs/doc[@for="PowerStatus.BatteryLifePercent"]/*' />
        /// <devdoc>
        ///    <para>[To be supplied.]</para>
        /// </devdoc>
        public float BatteryLifePercent
        {
            get
            {
                return 1;
                //UpdateSystemPowerStatus();
                //float lifePercent = systemPowerStatus.BatteryLifePercent / 100f;
                //return lifePercent > 1f ? 1f : lifePercent;
            }
        }

        /// <include file='doc\PowerStatus.uex' path='docs/doc[@for="PowerStatus.BatteryLifeRemaining"]/*' />
        /// <devdoc>
        ///    <para>[To be supplied.]</para>
        /// </devdoc>
        public int BatteryLifeRemaining
        {
            get
            {
                return 1;
                //UpdateSystemPowerStatus();
                //return systemPowerStatus.BatteryLifeTime;
            }
        }

        //private void UpdateSystemPowerStatus() {                
        //    // Fixed Windows 11 defaults: assume AC power, no system battery on desktop
        //    systemPowerStatus.ACLineStatus = (byte)PowerLineStatus.Online;
        //    systemPowerStatus.BatteryFlag = (byte)BatteryChargeStatus.NoSystemBattery;
        //    systemPowerStatus.BatteryLifePercent = 100; // 100%
        //    systemPowerStatus.BatteryLifeTime = -1; // unknown
        //    systemPowerStatus.BatteryFullLifeTime = -1; // unknown
        //}
    }
}
