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
        /// Starts Application having Twindow as first thing user sees.
        /// </summary>
        /// <typeparam name="Tapp">System.Windows.Application, or subclass if necessary</typeparam>
        /// <typeparam name="Twindow">class of window</typeparam>
        /// <param name="args"></param>
        /// <example>
        /// <code>
        /// [STAThread]
        /// public static int Main(string[] args)
        /// {  return Starter.Start<YourApp,YourWin>(args); }
        /// </code>
        /// </example>
        public static int Start<Tapp, Twindow>(string[] args)
            where Tapp : System.Windows.Application, new()
            where Twindow : System.Windows.Window, new()
        {
            //Process proc = System.Diagnostics.Process.GetCurrentProcess();
            //ProcessStartInfo pistart = proc.StartInfo; // no use

            var starta = System.Reflection.Assembly.GetEntryAssembly();
            Log.Comment("Start", starta.Location);
            Log.Comment("Version", starta.GetName().Version);
            Log.Comment("Operating System", Environment.OSVersion);

            Tapp app = new Tapp();

            app.DispatcherUnhandledException += Current_DispatcherUnhandledException;

            Twindow mw = new Twindow();
            return app.Run(mw);
        }

        private static void Current_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            Notifier.Error(e.Exception);

            e.Handled = true; // but main window could have died, leaving app hanging

        }


    }
}
