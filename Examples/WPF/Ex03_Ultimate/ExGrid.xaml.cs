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
using Abnaki.Windows.Software.Wpf.PreferredControls.Grid;
using Abnaki.Windows.Software.Wpf;
using Abnaki.Windows.Software.Wpf.Ultimate;

namespace Ex03_Ultimate
{
    /// <summary>
    /// Interaction logic for ExGrid.xaml
    /// </summary>
    public partial class ExGrid : UserControl
    {
        public ExGrid()
        {
            InitializeComponent();

            ButtonBus<ExMenuKey>.HookupSubscriber(this.HandleMenuCommand);

            MessageTube.Subscribe<FarewellMessage>(Farewell);

            Loaded += ExGrid_Loaded;
        }

        void HandleMenuCommand(ButtonMessage<ExMenuKey> m)
        {
            switch (m.Key)
            {
                case ExMenuKey.FileNew:
                    BindExample();
                    break;
            }
        }

        void BindExample()
        {
            ShoppingData ds = new ShoppingData();
            var vrow = ds.Vendor.AddVendorRow("Kroger");
            ds.Item.AddItemRow("Butter", vrow, new DateTime(2030, 1, 1), 4, false);
            ds.Item.AddItemRow("Eggs", vrow, new DateTime(2030, 1, 5), 12, true);

            this.Gridc.BindGrid(ds.Item);

            IEnumerable<Col> cols = new[]{ 
                new Col(ds.Item.QuantityColumn), 
                new Col(ds.Item.NameColumn),
                new Col(ds.Item.StockColumn){ Tooltip = "have any in the pantry" },
                new Col(ds.Item.DateColumn)
                {
                    Format = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.LongDatePattern
                }
            };

            this.Gridc.ConfigureColumns(cols);
        }

        void ExGrid_Loaded(object sender, RoutedEventArgs e)
        {
            this.Gridc.RestorePreferences<ExGrid>();
        }

        void Farewell(FarewellMessage msg)
        {
            this.Gridc.SavePreferences<ExGrid>();
        }

    }
}
