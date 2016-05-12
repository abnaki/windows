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
        /// Starts Application having Twindow.
        /// Does not need any subclass of Application.
        /// Also see other Start overload.
        /// </summary>
        /// <typeparam name="Twindow">class of window
        /// </typeparam>
        public static int Start<Twindow>(string[] args, Func<Twindow> initWindow = null)
            where Twindow : System.Windows.Window, new()
        {
            return Start<Twindow, System.Windows.Application>(args, initWindow);
        }

        /// <summary>
        /// Starts Application having Twindow as first thing user sees.
        /// </summary>
        /// <typeparam name="Tapp">System.Windows.Application, or subclass if necessary</typeparam>
        /// <typeparam name="Twindow">class of window</typeparam>
        /// <example>
        /// <code>
        /// [STAThread]
        /// public static int Main(string[] args)
        /// {  return Starter.Start<YourApp,YourWin>(args); }
        /// </code>
        /// </example>
        public static int Start<Twindow, Tapp>(string[] args, Func<Twindow> initWindow = null)
            where Tapp : System.Windows.Application, new()
            where Twindow : System.Windows.Window, new()
        {
            //Process proc = System.Diagnostics.Process.GetCurrentProcess();
            //ProcessStartInfo pistart = proc.StartInfo; // no use
            try
            {
                var starta = System.Reflection.Assembly.GetEntryAssembly();
                Log.Comment("Start", starta.Location);
                Log.Comment("Version", starta.GetName().Version);
                Log.Comment("Operating System", Environment.OSVersion);

                System.AppDomain.CurrentDomain.AssemblyLoad += CurrentDomain_AssemblyLoad;

                Tapp app = new Tapp();

                app.DispatcherUnhandledException += Current_DispatcherUnhandledException;

                Twindow mw;
                if (initWindow == null)
                    mw = new Twindow();
                else
                    mw = initWindow();

                return app.Run(mw);
            }
            catch ( Exception ex )
            {
                Diplomat.Notifier.Error(ex);
            }
            return 1; // failed
        }

        static void CurrentDomain_AssemblyLoad(object sender, AssemblyLoadEventArgs args)
        {
            string filepath = null;
            if ( args.LoadedAssembly.IsDynamic )
            {
                // no file
            }
            else
            {
                filepath = args.LoadedAssembly.Location;
            }

            Log.FileInfo(filepath, "Load " + args.LoadedAssembly.GetName());
        }

        private static void Current_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            try  // of all code, must be particularly robust
            {
                Diplomat.Notifier.Error(e.Exception);
            }
            catch ( Exception ex )
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                e.Handled = true; // but main window could have died, leaving app hanging
            }
        }


    }
}
