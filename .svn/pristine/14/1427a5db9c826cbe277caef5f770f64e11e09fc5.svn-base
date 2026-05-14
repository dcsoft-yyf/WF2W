//-----------------------------------------------------------------------------
// <copyright file="ListViewInsertionMark.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>                                                                
//------------------------------------------------------------------------------

using System.Drawing;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace System.Windows.Forms {

    /// <include file='doc\ListViewInsertionMark.uex' path='docs/doc[@for="ListViewInsertionMark"]/*' />
    /// <devdoc>
    ///    <para>
    ///         Encapsulates insertion-mark information    
    ///    </para>
    /// </devdoc>
    public sealed class ListViewInsertionMark
    {	
        private ListView listView;
        
        private int index = 0;
        private Color color = Color.Empty;
        private bool appearsAfterItem = false;

        internal ListViewInsertionMark(ListView listView) {
            this.listView = listView;
        }

        /// <include file='doc\ListViewInsertionMark.uex' path='docs/doc[@for="ListViewInsertionMark.AppearsAfterItem"]/*' />
        /// <devdoc>
        ///     Specifies whether the insertion mark appears
    	///     after the item - otherwise it appears
    	///     before the item (the default).
        /// </devdoc>
        ///
    	public bool AppearsAfterItem { 
            get
            {
                return appearsAfterItem;
            }
            set
            {
                if (appearsAfterItem != value) {
                    appearsAfterItem = value;

                    if (listView.IsHandleCreated) {
                        UpdateListView();
                    }
                }
            }
        }
    
    	/// <include file='doc\ListViewInsertionMark.uex' path='docs/doc[@for="ListViewInsertionMark.Bounds"]/*' />
        /// <devdoc>
        ///     Returns bounds of the insertion-mark.
        /// </devdoc>
        ///
    	public Rectangle Bounds {
            get
            {
                WinFormNativeMethods.RECT rect = new WinFormNativeMethods.RECT();
                listView.SendMessage(WinFormNativeMethods.LVM_GETINSERTMARKRECT, 0, ref rect);
                return Rectangle.FromLTRB(rect.left, rect.top, rect.right, rect.bottom);
            }
        }
    
    	/// <include file='doc\ListViewInsertionMark.uex' path='docs/doc[@for="ListViewInsertionMark.Color"]/*' />
        /// <devdoc>
        ///     The color of the insertion-mark.
        /// </devdoc>
        ///
    	public Color Color { 
            get 
            {
                if (color.IsEmpty) {
                    color = WinFormSafeNativeMethods.ColorFromCOLORREF((int)listView.SendMessage(WinFormNativeMethods.LVM_GETINSERTMARKCOLOR, 0, 0));
                }
                return color;
            }             
            set
            {
                if (color != value) {
                    color = value;
                    if (listView.IsHandleCreated) {
                        listView.SendMessage(WinFormNativeMethods.LVM_SETINSERTMARKCOLOR, 0, WinFormSafeNativeMethods.ColorToCOLORREF(color));
                    }
                }
            }
        }
    
    	/// <include file='doc\ListViewInsertionMark.uex' path='docs/doc[@for="ListViewInsertionMark.Index"]/*' />
        /// <devdoc>
        ///     Item next to which the insertion-mark appears.
        /// </devdoc>
        ///
    	public int Index {
            get
            {
                return index;
            }
            set
            {
                if (index != value) {
                    index = value;
                    if (listView.IsHandleCreated) {
                        UpdateListView();
                    }
                }
            }
        }        
  
        /// <include file='doc\ListViewInsertionMark.uex' path='docs/doc[@for="ListViewInsertionMark.Index"]/*' />
        /// <devdoc>
        ///     Performs a hit-test at the specified insertion point
    	///     and returns the closest item.
        /// </devdoc>
        ///
    	public int NearestIndex(Point pt)
        {
            WinFormNativeMethods.POINT point = new WinFormNativeMethods.POINT();
            point.x = pt.X;
            point.y = pt.Y;

            WinFormNativeMethods.LVINSERTMARK lvInsertMark = new WinFormNativeMethods.LVINSERTMARK();
            WinFormUnsafeNativeMethods.SendMessage(new HandleRef(listView, listView.Handle), WinFormNativeMethods.LVM_INSERTMARKHITTEST, point, lvInsertMark);

            return lvInsertMark.iItem;
        }       

        internal void UpdateListView() {
            Debug.Assert(listView.IsHandleCreated, "ApplySavedState Precondition: List-view handle must be created");
            WinFormNativeMethods.LVINSERTMARK lvInsertMark = new WinFormNativeMethods.LVINSERTMARK();                 
            lvInsertMark.dwFlags = appearsAfterItem ? WinFormNativeMethods.LVIM_AFTER : 0;
            lvInsertMark.iItem = index;
            WinFormUnsafeNativeMethods.SendMessage(new HandleRef(listView, listView.Handle), WinFormNativeMethods.LVM_SETINSERTMARK, 0, lvInsertMark);

            if (!color.IsEmpty) {
                listView.SendMessage(WinFormNativeMethods.LVM_SETINSERTMARKCOLOR, 0, WinFormSafeNativeMethods.ColorToCOLORREF(color));
            }            
        }
    }
}
