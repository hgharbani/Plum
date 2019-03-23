using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Plum;
using Plum.Form;

namespace Plum
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Create new CultureInfo
            var cultureInfo = new System.Globalization.CultureInfo("fa-IR");

            // Set the language for static text (i.e. column headings, titles)
            System.Threading.Thread.CurrentThread.CurrentUICulture = cultureInfo;

            // Set the language for dynamic text (i.e. date, time, money)
            System.Threading.Thread.CurrentThread.CurrentCulture = cultureInfo;

            ModifyInMemory.ActivateMemoryPatching();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Login());
        }
    }
}
