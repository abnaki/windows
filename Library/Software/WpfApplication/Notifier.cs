﻿using System;
using System.Collections.Generic;
using System.Windows;

namespace Abnaki.Windows.Software.Wpf
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

            Error(ex.Message);

        }

    }
}
