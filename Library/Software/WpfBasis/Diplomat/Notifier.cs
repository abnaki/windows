using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Threading;
using System.Linq;
using System.Diagnostics;

namespace Abnaki.Windows.Software.Wpf.Diplomat
{
    /// <summary>
    /// UI notifications
    /// </summary>
    public class Notifier
    {
        static Notifier()
        {
            rootDispatcher = Dispatcher.CurrentDispatcher;
        }

        static Dispatcher rootDispatcher;

        public static void Notify(string message)
        {
            
            ShowMessage("Note", message, MessageBoxImage.Information);
        }

        public static void Error(string message)
        {

            ShowMessage("Error", message, MessageBoxImage.Error);

            // wish to prompt to use Troubleshooter for situation when no menu is available.
        }

        static void ShowMessage(string caption, string message, MessageBoxImage image)
        {
            Window win = Application.Current.Windows.Cast<Window>().FirstOrDefault(w => w.IsLoaded);

            if (win == null || rootDispatcher == null)
            {
                MessageBox.Show(message, caption, MessageBoxButton.OK, image);
            }
            else
            {
                Action showit = () => MessageBox.Show(win, message, caption, MessageBoxButton.OK, image);
                rootDispatcher.BeginInvoke(showit);
            }

        }

        static Queue<string> recentErrors = new Queue<string>();

        /// <summary>
        /// Log and show to user
        /// </summary>
        public static void Error(Exception ex)
        {
            Abnaki.Windows.AbnakiLog.Exception(ex);

            string msg = CumulativeMessage(ex);

            if (AppendMessage(msg))
            {
                Error(msg);
            }
            else
            {
                // avoid plaguing UI with redundant messages
                Debug.WriteLine("Skipped UI notification of " + msg);
            }
        }

        static string CumulativeMessage(Exception ex)
        {
            if (ex.InnerException == null)
                return ex.Message;
            else
                return ex.Message + Environment.NewLine + CumulativeMessage(ex.InnerException);
        }

        /// <returns>true if errorMsg is different from recent messages
        /// </returns>
        static bool AppendMessage(string errorMsg)
        {
            lock (recentErrors)
            {
                int limit = 4;
                if (recentErrors.Count >= limit && recentErrors.All(m => m == errorMsg))
                {
                    return false;
                }
                while (  recentErrors.Count >= limit )
                {
                    recentErrors.Dequeue();
                }

                recentErrors.Enqueue(errorMsg);
                return true;
            }
        }
    }
}
