using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;

namespace Abnaki.Windows.Software.Wpf
{
    public class Notifier
    {
        public static void Error(string message)
        {
            MessageBox.Show(Application.Current.MainWindow, message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public static void Error(Exception ex)
        {
            Debug.WriteLine(ex);

            Error(ex.Message);

        }

    }
}
