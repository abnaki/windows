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
using System.Diagnostics;

using Abnaki.Windows.Software.Wpf.Menu;

namespace Abnaki.Windows.Software.Wpf.Ultimate
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// MS does not support inheriting XAML properly across assemblies.  XAML is used for this class,
    /// and you will not have a subclass.   You can use public functionality.  Also see UltimateStarter.
    /// </remarks>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            mainMenub = new MainMenuBus(this.TopMenu);
        }

        MainMenuBus mainMenub;

        public void SetMainControl(System.Windows.UIElement c)
        {
            this.ClientPanel.Children.Add(c);
        }
    }
}
