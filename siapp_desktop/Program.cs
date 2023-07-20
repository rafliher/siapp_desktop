using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace siapp_desktop
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Login());
        }

        static void Navigate(string url)
        {
            switch (url)
            {
                case "Login":
                    Application.Exit();
                    Application.Run(new Login());
                    break;
                case "Register":
                    Application.Exit();
                    Application.Run(new Register());
                    break;
                default: break;
            }
        }
    }
}
