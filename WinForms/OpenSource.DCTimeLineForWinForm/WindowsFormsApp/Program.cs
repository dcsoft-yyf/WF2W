using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Reflection;
using System.Security.Principal;
using System.Security.Permissions;

namespace WindowsFormsApp
{
    static class Program
    {

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmDCTimlineDemo());
        }
    }
}
