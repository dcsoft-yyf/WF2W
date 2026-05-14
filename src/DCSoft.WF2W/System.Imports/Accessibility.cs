using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Accessibility
{
    public interface IAccessible
    {
        object accParent
        {
            get;
        }
        //object get_accParent(object childID);
        int accChildCount
        {
            get;
        }
        //int get_accChildCount(object childID);

        //object accChild
        //{
        //    get;
        //}
        object get_accChild( Object childID);

        //string accName
        //{
        //    get;
        //    set;
        //}

        //string accValue
        //{
        //    get;
        //    set;
        //}
        string get_accName( Object childID);
        void set_accName( Object childID, string newName);
        string get_accValue( Object childID);
        void set_accValue(  Object childID, string newValue);

        //string accDescription
        //{
        //    get;
        //}
        string get_accDescription( Object childID);

        //object accRole
        //{
        //    get;
        //}
        object get_accRole( Object childID);

        //object accState
        //{
        //    get;
        //}
        object get_accState( Object childID);
        //string accHelp
        //{
        //    get;
        //}

        string get_accHelp( Object childID);
        //int accHelpTopic
        //{
        //    get;
        //}
        int get_accHelpTopic( out string pszHelpFile, Object childID);

        //string accKeyboardShortcut
        //{
        //    get;
        //}
        string get_accKeyboardShortcut( Object childID);

        //object get_accFocus( object childID);
        object accFocus
        {
            get;
        }
        //object get_accSelection( object childID);

        object accSelection
        {
            get;
        }
        string get_accDefaultAction( Object childID);
        //string accDefaultAction
        //{
        //    get;
        //}
        void accSelect([In] int flagsSelect, [Optional][In][MarshalAs(UnmanagedType.Struct)] object varChild);
        void accLocation(out int pxLeft, out int pyTop, out int pcxWidth, out int pcyHeight, [Optional][In][MarshalAs(UnmanagedType.Struct)] object varChild);
        object accNavigate([In] int navDir, [Optional][In][MarshalAs(UnmanagedType.Struct)] object varStart);

        object accHitTest([In] int xLeft, [In] int yTop);

        void accDoDefaultAction([Optional][In][MarshalAs(UnmanagedType.Struct)] object varChild);
    }
}
