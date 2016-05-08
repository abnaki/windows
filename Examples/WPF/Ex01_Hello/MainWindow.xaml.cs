using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Abnaki.Windows.Software.Wpf;

namespace Ex01_Hello
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new ViewModels.VmMain();

            bus = new ViewModels.VmButtonBus();
        }

        enum ButtonKey { Throw, SaveLog, TestMessage };
        
        // will promote some of this
        ViewModels.VmButtonBus bus;

        /// <summary>
        /// Button Click is wired to this method and each Button CommandParameter
        /// is the string of an enum value
        /// </summary>
        void HandleButton(Object sender, RoutedEventArgs e)
        {
            HandleButton<ButtonKey>(sender, e);
        }

        void HandleButton<Tenum>(Object sender, RoutedEventArgs e)
        {
            Button bu = (Button)sender;
            //object x = bu.CommandParameter; // contains string
            var m = new ViewModels.VmButtonBus.ButtonMessage<Tenum>(Convert.ToString(bu.CommandParameter));

            MessageTube.Publish<ViewModels.VmButtonBus.ButtonMessage>(m);
        }

    }
}
