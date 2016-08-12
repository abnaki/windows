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

using Abnaki.Windows.Software.Wpf.Menu;

namespace Ex03_Ultimate
{
    /// <summary>
    /// Interaction logic for Flag.xaml
    /// </summary>
    public partial class Flag : UserControl
    {
        public Flag()
        {
            InitializeComponent();

            ButtonBus<ExMenuKey>.HookupSubscriber(this.HandleMenuCommand);
        }

        void HandleMenuCommand(ButtonMessage<ExMenuKey> m)
        {
            if (m.Checked == true)
            {
                switch (m.Key)
                {
                    case ExMenuKey.OptionFlagNed:
                        ReviseColor(Brushes.Red, Brushes.White, Brushes.Blue);
                        break;
                    case ExMenuKey.OptionFlagAustria:
                        ReviseColor(Brushes.Red, Brushes.White, Brushes.Red);
                        break;
                    case ExMenuKey.OptionFlagBulg:
                        ReviseColor(Brushes.White, Brushes.DarkSeaGreen, Brushes.Red);
                        break;
                }
            }
        }

        void ReviseColor(Brush top, Brush mid, Brush bottom)
        {
            RecTop.Fill = top;
            RecMid.Fill = mid;
            RecBot.Fill = bottom;
            InvalidateVisual();
        }
    }
}
