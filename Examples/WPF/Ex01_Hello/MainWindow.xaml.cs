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
using Abnaki.Windows.Software.Wpf.Menu;

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

            bus = new ExButtonBus(); 
        }

        ExButtonBus bus;

        /// <summary>
        /// Button Click is wired to this method.
        /// Note that MessageTube is used, 
        /// and this.bus is not necessarily the sole subscriber to the button.
        /// Loose couping is intended.
        /// </summary>
        void HandleButton(Object sender, RoutedEventArgs e)
        {
            ButtonBus<ButtonKey>.HandleButton(sender, e);
        }

    }
}
