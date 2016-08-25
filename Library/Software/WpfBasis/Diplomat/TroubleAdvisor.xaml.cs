using System;
using System.Windows;
using System.Diagnostics;

namespace Abnaki.Windows.Software.Wpf.Diplomat
{
    /// <summary>
    /// Dialog for reporting an issue
    /// </summary>
    public partial class TroubleAdvisor
    {
        public TroubleAdvisor()
        {
            InitializeComponent();

            Owner = Application.Current.MainWindow;
            Icon = Application.Current.MainWindow.Icon;
        }

        public string Advice
        {
            set { AdviceBlock.Text = value; }
            get { return AdviceBlock.Text; }
        }

        string Email { get { return Troubleshooter.Email; } }

        /// <summary>True implies OK
        /// </summary>
        /// <remarks>
        /// DialogResult is beyond our control as Visibility changes
        /// </remarks>
        public bool Accept { get; private set; }

        private void bok_Click(object sender, RoutedEventArgs e)
        {
            Dismiss();
            Accept = true;
        }

        /// <summary>
        /// unused
        /// </summary>
        private void bcancel_Click(object sender, RoutedEventArgs e)
        {
            Dismiss();
            Accept = false;
        }

        void Dismiss()
        {
            Visibility = System.Windows.Visibility.Collapsed;
        }

        private void bemail_Click(object sender, RoutedEventArgs e)
        {
            if (false == string.IsNullOrEmpty(Email))
            {
                var aname = System.Reflection.Assembly.GetEntryAssembly().GetName();
                var v = aname.Version;
                string subject = string.Format("{0}%20{1}.{2}.{3}%20", aname.Name, v.Major, v.Minor, v.Build);
                string uri = string.Format("mailto:{0}?subject={1}", Email, subject);

                using ( var p = Process.Start(uri) )
                {
                    // ok
                }
            }
        }

        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            bemail.IsEnabled = !string.IsNullOrEmpty(this.Email);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
        }

        private void bsavelog_Click(object sender, RoutedEventArgs e)
        {
            Troubleshooter.DialogSaveLog();
        }
    }

}
