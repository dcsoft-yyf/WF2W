using System;
using System.Text;
using System.Collections.Generic;

namespace DCSoft
{

    /// <summary>
    /// 命令行输出界面
    /// </summary>
    [Serializable]
    [System.Runtime.InteropServices.ComVisible( false )]
    public class DCConsole
    {
        public static DCConsole Default = new DCConsole();

        public DCConsole()
        {

        }

        public virtual void WriteLine(string value)
        {
            Console.WriteLine( value );
        }

        public virtual void WriteLineError( string value )
        {
            Console.WriteLine(value);
        }
        ///// <summary>
        ///// 输出加载文件完成的调试信息
        ///// </summary>
        ///// <param name="size">加载的数据字节数</param>
        //public void DebugLoadFileComplete(int size)
        //{
        //    this.WriteLine(string.Format(
        //            DCSR.LoadComplete_Size,
        //            DCSoft.Writer.WriterUtilsInner.FormatByteSize(size)));
        //}

        //public virtual void WriteLine(bool value)
        //{
        //    if (this._Use_System_Console)
        //    {
        //        Console.WriteLine(value);
        //    }
        //    if(this._Use_System_Diagnostics_Debug)
        //    {
        //        System.Diagnostics.Debug.WriteLine(value.ToString());
        //    }
        //    if ( this._Listeners.Count > 0 )
        //    {
        //        OutputToListeners(value.ToString() + Environment.NewLine);
        //    }
        //}
        public virtual void Write(string value)
        {
            Console.Write(value);
        }
    }

}
