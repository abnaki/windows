using System;
using System.Collections.Generic;
using System.Windows;
using System.Diagnostics;

namespace Abnaki.Windows.Software.Wpf
{
    /// <summary>
    /// Starts Application with suitable preliminaries
    /// </summary>
    public class Starter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="Tapp"></typeparam>
        /// <typeparam name="Twin"></typeparam>
        /// <param name="args"></param>
        /// <example>
        /// [STAThread]
        /// public static int Main(string[] args)
        /// {  return Starter.Start&lt;YourApp,Yourwin>(args); }
        /// </example>
        public static int Start<Tapp, Twin>(string[] args)
            where Tapp : System.Windows.Application, new()
            where Twin : System.Windows.Window, new()
        {
            //Process proc = System.Diagnostics.Process.GetCurrentProcess();
            //ProcessStartInfo pistart = proc.StartInfo; // no use

            var starta = System.Reflection.Assembly.GetEntryAssembly();
            Log.Comment("Start", starta.Location);
            Log.Comment("Version", starta.GetName().Version);

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
