//------------------------------------------------------------------------------
// <copyright file="TextRenderer.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>                                                                
//------------------------------------------------------------------------------
namespace System.Windows.Forms
{
    using System.Internal;
    using System;
    using System.Drawing;
    using System.Windows.Forms.Internal;
    using System.Diagnostics;

    
    /// <include file='doc\TextRenderer.uex' path='docs/doc[@for="TextRenderer"]/*' />
    /// <devdoc>
    ///    <para>
    ///     This class provides API for drawing GDI text.
    ///    </para>
    /// </devdoc>
    public sealed class TextRenderer
    {
        //cannot instantiate
        private TextRenderer()
        {
        }

        /// <include file='doc\TextRenderer.uex' path='docs/doc[@for="TextRenderer.DrawText"]/*' />
        public static void DrawText(IDeviceContext dc, string text, Font font, Point pt, Color foreColor)
        {
            if (dc == null)
            {
                throw new ArgumentNullException("dc");
            }

            WindowsFontQuality fontQuality = WindowsFont.WindowsFontQualityFromTextRenderingHint(dc as Graphics);

            IntPtr hdc = dc.GetHdc();

            try
            {
                using( WindowsGraphics wg = WindowsGraphics.FromHdc( hdc ))
                {
                    using (WindowsFont wf = WindowsGraphicsCacheManager.GetWindowsFont(font, fontQuality)) {
                        wg.DrawText(text, wf, pt, foreColor);
                    }
                }
            }
            finally
            {
                dc.ReleaseHdc();
            }
        }

        /// <include file='doc\TextRenderer.uex' path='docs/doc[@for="TextRenderer.DrawText1"]/*' />
        public static void DrawText(IDeviceContext dc, string text, Font font, Point pt, Color foreColor, Color backColor)
        {
            if (dc == null)
            {
                throw new ArgumentNullException("dc");
            }

            WindowsFontQuality fontQuality = WindowsFont.WindowsFontQualityFromTextRenderingHint(dc as Graphics);

            IntPtr hdc = dc.GetHdc();

            try
            {
                using( WindowsGraphics wg = WindowsGraphics.FromHdc( hdc ))
                {
                    using (WindowsFont wf = WindowsGraphicsCacheManager.GetWindowsFont(font, fontQuality)) {
                        wg.DrawText(text, wf, pt, foreColor, backColor);
                    }
                }
            }
            finally
            {
                dc.ReleaseHdc();
            }
        }

        /// <include file='doc\TextRenderer.uex' path='docs/doc[@for="TextRenderer.DrawText2"]/*' />
        public static void DrawText(IDeviceContext dc, string text, Font font, Point pt, Color foreColor, TextFormatFlags flags)
        {
            if (dc == null)
            {
                throw new ArgumentNullException("dc");
            }

            WindowsFontQuality fontQuality = WindowsFont.WindowsFontQualityFromTextRenderingHint(dc as Graphics);

            using( WindowsGraphicsWrapper wgr = new WindowsGraphicsWrapper( dc, flags ))
            {
                using (WindowsFont wf = WindowsGraphicsCacheManager.GetWindowsFont(font, fontQuality)) {
                    wgr.WindowsGraphics.DrawText(text, wf, pt, foreColor, GetIntTextFormatFlags(flags));
                }
            }
        }

        /// <include file='doc\TextRenderer.uex' path='docs/doc[@for="TextRenderer.DrawText3"]/*' />
        public static void DrawText(IDeviceContext dc, string text, Font font, Point pt, Color foreColor, Color backColor, TextFormatFlags flags)
        {
            if (dc == null)
            {
                throw new ArgumentNullException("dc");
            }

            WindowsFontQuality fontQuality = WindowsFont.WindowsFontQualityFromTextRenderingHint(dc as Graphics);

            using( WindowsGraphicsWrapper wgr = new WindowsGraphicsWrapper( dc, flags ))
            {
                using (WindowsFont wf = WindowsGraphicsCacheManager.GetWindowsFont(font, fontQuality)) {
                    wgr.WindowsGraphics.DrawText(text, wf, pt, foreColor, backColor, GetIntTextFormatFlags(flags));
                }
            }
        }

        /// <include file='doc\TextRenderer.uex' path='docs/doc[@for="TextRenderer.DrawText4"]/*' />
        public static void DrawText(IDeviceContext dc, string text, Font font, Rectangle bounds, Color foreColor)
        {
            if (dc == null)
            {
                throw new ArgumentNullException("dc");
            }

            WindowsFontQuality fontQuality = WindowsFont.WindowsFontQualityFromTextRenderingHint(dc as Graphics);

            IntPtr hdc = dc.GetHdc();

            try
            {
                using( WindowsGraphics wg = WindowsGraphics.FromHdc( hdc ))
                {
                    using (WindowsFont wf = WindowsGraphicsCacheManager.GetWindowsFont(font, fontQuality)) {
                        wg.DrawText(text, wf, bounds, foreColor);
                    }
                }
            }
            finally
            {
                dc.ReleaseHdc();
            }
        }

        /// <include file='doc\TextRenderer.uex' path='docs/doc[@for="TextRenderer.DrawText5"]/*' />
        public static void DrawText(IDeviceContext dc, string text, Font font, Rectangle bounds, Color foreColor, Color backColor)
        {
            if (dc == null)
            {
                throw new ArgumentNullException("dc");
            }

            WindowsFontQuality fontQuality = WindowsFont.WindowsFontQualityFromTextRenderingHint(dc as Graphics);

            IntPtr hdc = dc.GetHdc();

            try
            {
                using( WindowsGraphics wg = WindowsGraphics.FromHdc( hdc ))
                {
                    using (WindowsFont wf = WindowsGraphicsCacheManager.GetWindowsFont(font, fontQuality)) {
                        wg.DrawText(text, wf, bounds, foreColor, backColor);
                    }
                }
            }
            finally
            {
                dc.ReleaseHdc();
            }
        }

        /// <include file='doc\TextRenderer.uex' path='docs/doc[@for="TextRenderer.DrawText6"]/*' />
        public static void DrawText(IDeviceContext dc, string text, Font font, Rectangle bounds, Color foreColor, TextFormatFlags flags)
        {
            if (dc == null)
            {
                throw new ArgumentNullException("dc");
            }
            if (string.IsNullOrEmpty(text))
            {
                return;
            }

            Graphics g = dc as Graphics;
            bool createdFromHdc = false;
            IntPtr hdc = IntPtr.Zero;
            if (g == null)
            {
                hdc = dc.GetHdc();
                g = Graphics.FromHdc(hdc);
                createdFromHdc = true;
            }

            try
            {
                StringFormat sf = StringFormat.GenericDefault;

                if ((flags & TextFormatFlags.HorizontalCenter) != 0)
                {
                    sf.Alignment = StringAlignment.Center;
                }
                else if ((flags & TextFormatFlags.Right) != 0)
                {
                    sf.Alignment = StringAlignment.Far;
                }
                else
                {
                    sf.Alignment = StringAlignment.Near;
                }

                if ((flags & TextFormatFlags.VerticalCenter) != 0)
                {
                    sf.LineAlignment = StringAlignment.Center;
                }
                else if ((flags & TextFormatFlags.Bottom) != 0)
                {
                    sf.LineAlignment = StringAlignment.Far;
                }
                else
                {
                    sf.LineAlignment = StringAlignment.Near;
                }

                if ((flags & TextFormatFlags.SingleLine) != 0)
                {
                    sf.FormatFlags |= StringFormatFlags.NoWrap;
                }
                if ((flags & TextFormatFlags.RightToLeft) != 0)
                {
                    sf.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
                }
                if ((flags & TextFormatFlags.NoClipping) != 0)
                {
                    // No clipping requested; DrawString still needs a layout rectangle, so keep bounds
                }
                sf.FormatFlags |= StringFormatFlags.MeasureTrailingSpaces;

                using (var brush = new SolidBrush(foreColor))
                {
                    g.DrawString(text, font, brush, bounds, sf);
                }
            }
            finally
            {
                if (createdFromHdc)
                {
                    g.Dispose();
                    dc.ReleaseHdc();
                }
            }
        }

        /// <include file='doc\TextRenderer.uex' path='docs/doc[@for="TextRenderer.DrawText7"]/*' />
        public static void DrawText(IDeviceContext dc, string text, Font font, Rectangle bounds, Color foreColor, Color backColor, TextFormatFlags flags)
        {
            if (dc == null)
            {
                throw new ArgumentNullException("dc");
            }

            WindowsFontQuality fontQuality = WindowsFont.WindowsFontQualityFromTextRenderingHint(dc as Graphics);

            using( WindowsGraphicsWrapper wgr = new WindowsGraphicsWrapper( dc, flags ))
            {
                using (WindowsFont wf = WindowsGraphicsCacheManager.GetWindowsFont( font, fontQuality )) {
                    wgr.WindowsGraphics.DrawText(text, wf, bounds, foreColor, backColor, GetIntTextFormatFlags(flags));
                }
            }
        }

        private static IntTextFormatFlags GetIntTextFormatFlags(TextFormatFlags flags)
        {
            if( ((uint)flags & WindowsGraphics.GdiUnsupportedFlagMask) == 0 )
            {
                return (IntTextFormatFlags) flags;
            }

            // Clear TextRenderer custom flags.
            IntTextFormatFlags windowsGraphicsSupportedFlags = (IntTextFormatFlags) ( ((uint)flags) & ~WindowsGraphics.GdiUnsupportedFlagMask );

            return windowsGraphicsSupportedFlags;
        }

        /// MeasureText wrappers.
       
        public static Size MeasureText(string text, Font font )
        {
            if (string.IsNullOrEmpty(text)) 
            {
                return Size.Empty;
            }
            return MeasureText( text , font , new Size(int.MaxValue, int.MaxValue) , TextFormatFlags.SingleLine);
            //using (WindowsFont wf = WindowsGraphicsCacheManager.GetWindowsFont(font)) {
            //    return WindowsGraphicsCacheManager.MeasurementGraphics.MeasureText(text, wf);
            //}           
        }

        public static Size MeasureText(string text, Font font, Size proposedSize )
        {
            if (string.IsNullOrEmpty(text)) 
            {
                return Size.Empty;
            }
            
            using (WindowsFont wf = WindowsGraphicsCacheManager.GetWindowsFont(font)) {
                return WindowsGraphicsCacheManager.MeasurementGraphics.MeasureText(text, WindowsGraphicsCacheManager.GetWindowsFont(font), proposedSize);
            }
        }

        public static Size MeasureText(string text, Font font, Size proposedSize, TextFormatFlags flags )
        {
            if (string.IsNullOrEmpty(text))
            {
                return Size.Empty;
            }
            using (var g = System.Drawing.Graphics.CreateCanvas())
            {
                StringFormat sf = StringFormat.GenericDefault;

                if ((flags & TextFormatFlags.HorizontalCenter) != 0)
                {
                    sf.Alignment = StringAlignment.Center;
                }
                else if ((flags & TextFormatFlags.Right) != 0)
                {
                    sf.Alignment = StringAlignment.Far;
                }
                else
                {
                    sf.Alignment = StringAlignment.Near;
                }

                if ((flags & TextFormatFlags.VerticalCenter) != 0)
                {
                    sf.LineAlignment = StringAlignment.Center;
                }
                else if ((flags & TextFormatFlags.Bottom) != 0)
                {
                    sf.LineAlignment = StringAlignment.Far;
                }
                else
                {
                    sf.LineAlignment = StringAlignment.Near;
                }

                if ((flags & TextFormatFlags.SingleLine) != 0)
                {
                    sf.FormatFlags |= StringFormatFlags.NoWrap;
                }
                if ((flags & TextFormatFlags.RightToLeft) != 0)
                {
                    sf.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
                }
                if ((flags & TextFormatFlags.NoPrefix) == 0)
                {
                    // Leave as default, GenericDefault already handles hotkeys
                }
                text = FixTextForMeasurement( text, flags);
                sf.FormatFlags |= StringFormatFlags.MeasureTrailingSpaces;

                SizeF measured = g.MeasureString(text, font, new SizeF(proposedSize.Width, proposedSize.Height), sf);
                return new Size((int)Math.Ceiling(measured.Width), (int)Math.Ceiling(measured.Height));
            }
        }

        private static string FixTextForMeasurement(string text, TextFormatFlags flags)
        {
            if ((flags & TextFormatFlags.NoPrefix) == 0)
            {
                return text.Replace("&&", ""); // remove escaped ampersands
            }
            return text;
        }
        public static Size MeasureText(IDeviceContext dc, string text, Font font)
        {
            if (dc == null)
            {
                throw new ArgumentNullException("dc");
            }
            if (string.IsNullOrEmpty(text)) 
            {
                return Size.Empty;
            }
            WindowsFontQuality fontQuality = WindowsFont.WindowsFontQualityFromTextRenderingHint(dc as Graphics);

            IntPtr hdc = dc.GetHdc();

            try
            {
                using( WindowsGraphics wg = WindowsGraphics.FromHdc( hdc ))
                {
                    using (WindowsFont wf = WindowsGraphicsCacheManager.GetWindowsFont(font, fontQuality)) {
                        return wg.MeasureText(text, wf);
                    }
                }
            }
            finally
            {
                dc.ReleaseHdc();
            }
        }

        public static Size MeasureText(IDeviceContext dc, string text, Font font, Size proposedSize )
        {
            if (dc == null)
            {
                throw new ArgumentNullException("dc");
            }
            if (string.IsNullOrEmpty(text)) 
            {
                return Size.Empty;
            }

            WindowsFontQuality fontQuality = WindowsFont.WindowsFontQualityFromTextRenderingHint(dc as Graphics);

            IntPtr hdc = dc.GetHdc();

            try
            {
                using( WindowsGraphics wg = WindowsGraphics.FromHdc( hdc ))
                {
                    using (WindowsFont wf = WindowsGraphicsCacheManager.GetWindowsFont(font, fontQuality)) {
                        return wg.MeasureText(text, wf, proposedSize);
                    }
                }
            }
            finally
            {
                dc.ReleaseHdc();
            }
        }

        public static Size MeasureText(IDeviceContext dc, string text, Font font, Size proposedSize, TextFormatFlags flags )
        {            
            if (dc == null)
            {
                throw new ArgumentNullException("dc");
            }
            if (string.IsNullOrEmpty(text)) 
            {
                return Size.Empty;
            }
            return MeasureText(text, font, proposedSize, flags);
            //WindowsFontQuality fontQuality = WindowsFont.WindowsFontQualityFromTextRenderingHint(dc as Graphics);

            //using (WindowsGraphicsWrapper wgr = new WindowsGraphicsWrapper(dc, flags))
            //{
            //    using (WindowsFont wf = WindowsGraphicsCacheManager.GetWindowsFont(font, fontQuality)) {
            //        return wgr.WindowsGraphics.MeasureText(text, wf, proposedSize, GetIntTextFormatFlags(flags));
            //    }
            //}
        }


        internal static Color DisabledTextColor(Color backColor) {
            //Theme specs -- if the backcolor is darker than Control, we use
            // ControlPaint.Dark(backcolor).  Otherwise we use ControlDark.
            // see VS#357226
            Color disabledTextForeColor = SystemColors.ControlDark;
            if (ControlPaint.IsDarker(backColor, SystemColors.Control)) {
                disabledTextForeColor = ControlPaint.Dark(backColor);
            }
            return disabledTextForeColor;
        }
    }
}

