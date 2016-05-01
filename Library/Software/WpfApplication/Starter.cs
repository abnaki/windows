using System;
using System.Collections.Generic;
using System.Windows;
using System.Diagnostics;

namespace Abnaki.Windows.Software.Wpf
{
    public class Starter
    {
        [STAThread]
        public static int Main<Tapp, Twin>(string[] args)
            where Tapp : System.Windows.Application, new()
            where Twin : System.Windows.Window, new()
        {
            // will want to log some info

            Tapp app = new Tapp();
            app.DispatcherUnhandledException += Current_DispatcherUnhandledException;

            Twin mw = new Twin();
            return app.Run(mw);
        }

        private static void Current_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            Notifier.Error(e.Exception);

            e.Handled = true; // but main window could have died, leaving app hanging

        }


    }
}
