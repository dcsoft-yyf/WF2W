namespace System.Windows.Forms {
    using System;
    using System.Windows.Forms;
    using System.Windows.Forms.VisualStyles;
    using Microsoft.Win32;
    using System.Drawing;
    using System.Collections;
    using System.Diagnostics;
    
    public sealed class ProfessionalColors {
        [ThreadStatic]
        private static ProfessionalColorTable professionalColorTable = null;

        [ThreadStatic]
        private static string colorScheme = null;

        [ThreadStatic]
        private static object colorFreshnessKey = null;

            
        internal static ProfessionalColorTable ColorTable {
            get {
                if (professionalColorTable == null) {
                    professionalColorTable = new ProfessionalColorTable();
                }
                return professionalColorTable;
            }
        }

        static ProfessionalColors() {
            SystemEvents.UserPreferenceChanged += new UserPreferenceChangedEventHandler(OnUserPreferenceChanged);
            SetScheme();
        }

        private ProfessionalColors() {
        }
        
        internal static string ColorScheme {
            get { return colorScheme; }
        }

        // internal object used between professional color tables
        // to identify when a userpreferencechanged has occurred
        internal static object ColorFreshnessKey {
            get { return colorFreshnessKey; }
        }

#region Colors

        [SRDescription(DCSR.ProfessionalColorsButtonSelectedHighlightDescr)]
        public static Color ButtonSelectedHighlight {
            get { return ColorTable.ButtonSelectedHighlight; }
        }

        [SRDescription(DCSR.ProfessionalColorsButtonSelectedHighlightBorderDescr)]
        public static Color ButtonSelectedHighlightBorder {
            get { return ColorTable.ButtonSelectedHighlightBorder; }
        }


        [SRDescription(DCSR.ProfessionalColorsButtonPressedHighlightDescr)]
        public static Color ButtonPressedHighlight {
             get { return ColorTable.ButtonPressedHighlight; }
        }


        [SRDescription(DCSR.ProfessionalColorsButtonPressedHighlightBorderDescr)]
        public static Color ButtonPressedHighlightBorder {
           get { return ColorTable.ButtonPressedHighlightBorder; }
        }
        
        [SRDescription(DCSR.ProfessionalColorsButtonCheckedHighlightDescr)]
        public static Color ButtonCheckedHighlight {
            get { return ColorTable.ButtonCheckedHighlight; }
        }

        [SRDescription(DCSR.ProfessionalColorsButtonCheckedHighlightBorderDescr)]
        public static Color ButtonCheckedHighlightBorder {
            get { return ColorTable.ButtonCheckedHighlightBorder; }
        }

        [SRDescription(DCSR.ProfessionalColorsButtonPressedBorderDescr)]
        public static Color ButtonPressedBorder {
            get { return ColorTable.ButtonPressedBorder; }
        }

        [SRDescription(DCSR.ProfessionalColorsButtonSelectedBorderDescr)]
        public static Color ButtonSelectedBorder {
            get { return ColorTable.ButtonSelectedBorder; }
        }

        [SRDescription(DCSR.ProfessionalColorsButtonCheckedGradientBeginDescr)]
        public static Color ButtonCheckedGradientBegin {
            get { return ColorTable.ButtonCheckedGradientBegin; }
        }

        [SRDescription(DCSR.ProfessionalColorsButtonCheckedGradientMiddleDescr)]
        public static Color ButtonCheckedGradientMiddle {
            get { return ColorTable.ButtonCheckedGradientMiddle; }
        }

        [SRDescription(DCSR.ProfessionalColorsButtonCheckedGradientEndDescr)]
        public static Color ButtonCheckedGradientEnd {
            get { return ColorTable.ButtonCheckedGradientEnd; }
        }

        [SRDescription(DCSR.ProfessionalColorsButtonSelectedGradientBeginDescr)]
        public static Color ButtonSelectedGradientBegin {
            get { return ColorTable.ButtonSelectedGradientBegin; }
        }

        [SRDescription(DCSR.ProfessionalColorsButtonSelectedGradientMiddleDescr)]
        public static Color ButtonSelectedGradientMiddle {
            get { return ColorTable.ButtonSelectedGradientMiddle; }
        }

        [SRDescription(DCSR.ProfessionalColorsButtonSelectedGradientEndDescr)]
        public static Color ButtonSelectedGradientEnd {
            get { return ColorTable.ButtonSelectedGradientEnd; }
        }

        [SRDescription(DCSR.ProfessionalColorsButtonPressedGradientBeginDescr)]
        public static Color ButtonPressedGradientBegin {
            get { return ColorTable.ButtonPressedGradientBegin; }
        }
     
        [SRDescription(DCSR.ProfessionalColorsButtonPressedGradientMiddleDescr)]
        public static Color ButtonPressedGradientMiddle {
            get { return ColorTable.ButtonPressedGradientMiddle; }
        }
        
        [SRDescription(DCSR.ProfessionalColorsButtonPressedGradientEndDescr)]
        public static Color ButtonPressedGradientEnd {
            get { return ColorTable.ButtonPressedGradientEnd; }
        }

        [SRDescription(DCSR.ProfessionalColorsCheckBackgroundDescr)]
        public static Color CheckBackground {
            get { return ColorTable.CheckBackground; }
        }
            
        [SRDescription(DCSR.ProfessionalColorsCheckSelectedBackgroundDescr)]
        public static Color CheckSelectedBackground {
            get { return ColorTable.CheckSelectedBackground; }
        }
        
        [SRDescription(DCSR.ProfessionalColorsCheckPressedBackgroundDescr)]
        public static Color CheckPressedBackground {
            get { return ColorTable.CheckPressedBackground; }
        }
        
        [SRDescription(DCSR.ProfessionalColorsGripDarkDescr)]
        public static Color GripDark {
            get { return ColorTable.GripDark; }
        }

        [SRDescription(DCSR.ProfessionalColorsGripLightDescr)]
        public static Color GripLight {
            get { return ColorTable.GripLight; }
        }

          
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
 

        [SRDescription(DCSR.ProfessionalColorsImageMarginGradientBeginDescr)]
        public static Color ImageMarginGradientBegin {
            get { return ColorTable.ImageMarginGradientBegin; }
        }

        [SRDescription(DCSR.ProfessionalColorsImageMarginGradientMiddleDescr)]
        public static Color ImageMarginGradientMiddle {
            get { return ColorTable.ImageMarginGradientMiddle; }
        }

        [SRDescription(DCSR.ProfessionalColorsImageMarginGradientEndDescr)]
        public static Color ImageMarginGradientEnd {
            get { return ColorTable.ImageMarginGradientEnd; }
        }

        [SRDescription(DCSR.ProfessionalColorsImageMarginRevealedGradientBeginDescr)]
        public static Color ImageMarginRevealedGradientBegin {
            get { return ColorTable.ImageMarginRevealedGradientBegin; }
        }

        [SRDescription(DCSR.ProfessionalColorsImageMarginRevealedGradientMiddleDescr)]
        public static Color ImageMarginRevealedGradientMiddle {
            get { return ColorTable.ImageMarginRevealedGradientMiddle; }
        }

        [SRDescription(DCSR.ProfessionalColorsImageMarginRevealedGradientEndDescr)]
        public static Color ImageMarginRevealedGradientEnd {
            get { return ColorTable.ImageMarginRevealedGradientEnd; }
        }

        [SRDescription(DCSR.ProfessionalColorsMenuStripGradientBeginDescr)]
        public static Color MenuStripGradientBegin {
            get { return ColorTable.MenuStripGradientBegin; }
        }

        [SRDescription(DCSR.ProfessionalColorsMenuStripGradientEndDescr)]
        public static Color MenuStripGradientEnd{
            get { return ColorTable.MenuStripGradientEnd; }
        }

        [SRDescription(DCSR.ProfessionalColorsMenuBorderDescr)]
        public static Color MenuBorder  {
            get { return ColorTable.MenuBorder; }
        }

        [SRDescription(DCSR.ProfessionalColorsMenuItemSelectedDescr)]
        public static Color MenuItemSelected {
            get { return ColorTable.MenuItemSelected; }  
        }

        [SRDescription(DCSR.ProfessionalColorsMenuItemBorderDescr)]
        public static Color MenuItemBorder {
            get { return ColorTable.MenuItemBorder; }
        }

        [SRDescription(DCSR.ProfessionalColorsMenuItemSelectedGradientBeginDescr)]
        public static Color MenuItemSelectedGradientBegin {
            get { return ColorTable.MenuItemSelectedGradientBegin; }
        }

        [SRDescription(DCSR.ProfessionalColorsMenuItemSelectedGradientEndDescr)]
        public static Color MenuItemSelectedGradientEnd {
            get { return ColorTable.MenuItemSelectedGradientEnd; }
        }

        [SRDescription(DCSR.ProfessionalColorsMenuItemPressedGradientBeginDescr)]
        public static Color MenuItemPressedGradientBegin {
            get { return ColorTable.MenuItemPressedGradientBegin; }
        }

        [SRDescription(DCSR.ProfessionalColorsMenuItemPressedGradientMiddleDescr)]
        public static Color MenuItemPressedGradientMiddle {
            get { return ColorTable.MenuItemPressedGradientMiddle; }
        }

        [SRDescription(DCSR.ProfessionalColorsMenuItemPressedGradientEndDescr)]
        public static Color MenuItemPressedGradientEnd {
            get { return ColorTable.MenuItemPressedGradientEnd; }
        }

   
        [SRDescription(DCSR.ProfessionalColorsRaftingContainerGradientBeginDescr)]
        public static Color RaftingContainerGradientBegin {
            get { return ColorTable.RaftingContainerGradientBegin; }
        }

        [SRDescription(DCSR.ProfessionalColorsRaftingContainerGradientEndDescr)]
        public static Color RaftingContainerGradientEnd {
            get { return ColorTable.RaftingContainerGradientEnd; }
        }

        [SRDescription(DCSR.ProfessionalColorsSeparatorDarkDescr)]
        public static Color SeparatorDark {
            get { return ColorTable.SeparatorDark; }
        }

        [SRDescription(DCSR.ProfessionalColorsSeparatorLightDescr)]
        public static Color SeparatorLight {
            get { return ColorTable.SeparatorLight; }
        }
        [SRDescription(DCSR.ProfessionalColorsStatusStripGradientBeginDescr)]
        public static Color StatusStripGradientBegin {
            get { return ColorTable.StatusStripGradientBegin; }
        }
   
        [SRDescription(DCSR.ProfessionalColorsStatusStripGradientEndDescr)]
        public static Color StatusStripGradientEnd {
            get { return ColorTable.StatusStripGradientEnd; }
        }

        [SRDescription(DCSR.ProfessionalColorsToolStripBorderDescr)]
        public static Color ToolStripBorder {
            get { return ColorTable.ToolStripBorder; }
        }

        [SRDescription(DCSR.ProfessionalColorsToolStripDropDownBackgroundDescr)]
        public static Color ToolStripDropDownBackground {
            get { return ColorTable.ToolStripDropDownBackground; }
        }

        [SRDescription(DCSR.ProfessionalColorsToolStripGradientBeginDescr)]
        public static Color ToolStripGradientBegin {
            get { return ColorTable.ToolStripGradientBegin; }
        }

        [SRDescription(DCSR.ProfessionalColorsToolStripGradientMiddleDescr)]
        public static Color ToolStripGradientMiddle {
            get { return ColorTable.ToolStripGradientMiddle; }
        }

        [SRDescription(DCSR.ProfessionalColorsToolStripGradientEndDescr)]
        public static Color ToolStripGradientEnd {
            get { return ColorTable.ToolStripGradientEnd; }
        }

        [SRDescription(DCSR.ProfessionalColorsToolStripContentPanelGradientBeginDescr)]
        public static Color ToolStripContentPanelGradientBegin {
            get { return ColorTable.ToolStripContentPanelGradientBegin; }
        }
   
        [SRDescription(DCSR.ProfessionalColorsToolStripContentPanelGradientEndDescr)]
        public static Color ToolStripContentPanelGradientEnd {
            get { return ColorTable.ToolStripContentPanelGradientEnd; }
        }
        
        [SRDescription(DCSR.ProfessionalColorsToolStripPanelGradientBeginDescr)]
        public static Color ToolStripPanelGradientBegin {
            get { return ColorTable.ToolStripPanelGradientBegin; }
        }
  
        [SRDescription(DCSR.ProfessionalColorsToolStripPanelGradientEndDescr)]
        public static Color ToolStripPanelGradientEnd {
            get { return ColorTable.ToolStripPanelGradientEnd; }
        }

        [SRDescription(DCSR.ProfessionalColorsOverflowButtonGradientBeginDescr)]
        public static Color OverflowButtonGradientBegin {
            get { return ColorTable.OverflowButtonGradientBegin; }
        }

        [SRDescription(DCSR.ProfessionalColorsOverflowButtonGradientMiddleDescr)]
        public static Color OverflowButtonGradientMiddle {
            get { return ColorTable.OverflowButtonGradientMiddle; }
        }

        [SRDescription(DCSR.ProfessionalColorsOverflowButtonGradientEndDescr)]
        public static Color OverflowButtonGradientEnd {
            get { return ColorTable.OverflowButtonGradientEnd; }
        }
#endregion Colors

      /*  public static Color ControlLight {
            get { return FromKnownColor(KnownColors.msocbvcrCBCtlBkgdLight); }
        } */
            
 
        private static void OnUserPreferenceChanged(object sender, UserPreferenceChangedEventArgs e) {
            SetScheme();
            if (e.Category == UserPreferenceCategory.Color) {
                colorFreshnessKey = new object();
            }
        }

        private static void SetScheme() {
            if (VisualStyleRenderer.IsSupported) {
                colorScheme = VisualStyleInformation.ColorScheme;
            }
            else {
                colorScheme = null;
            }
        }

    }

    
}

