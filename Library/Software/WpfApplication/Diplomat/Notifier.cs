using System;
using System.Collections.Generic;
using System.Windows;

namespace Abnaki.Windows.Software.Wpf.Diplomat
{
    /// <summary>
    /// UI notifications
    /// </summary>
    public class Notifier
    {
        public static void Notify(string message)
        {
            MessageBox.Show(Application.Current.MainWindow, message, "Note", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public static void Error(string message)
        {
            MessageBox.Show(Application.Current.MainWindow, message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public static void Error(Exception ex)
        {
            Abnaki.Windows.Log.Exception(ex);

            Error(CumulativeMessage(ex));

        }

        static string CumulativeMessage(Exception ex)
        {
            if (ex.InnerException == null)
                return ex.Message;
            else
                return ex.Message + Environment.NewLine + CumulativeMessage(ex.InnerException);
        }
    }
}
