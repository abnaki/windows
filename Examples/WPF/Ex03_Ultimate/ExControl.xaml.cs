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

using Abnaki.Windows.GUI;
using Abnaki.Windows.Software.Wpf;
using Abnaki.Windows.Software.Wpf.PreferredControls.Grid;
using Abnaki.Windows.Software.Wpf.PreferredControls.Docking;
using Abnaki.Windows.Software.Wpf.Ultimate;
using Abnaki.Windows.Software.Wpf.Menu;

namespace Ex03_Ultimate
{
    /// <summary>
    /// Interaction logic for ExControl.xaml
    /// </summary>
    public partial class ExControl : UserControl, Abnaki.Windows.GUI.IMainControl
    {
        public ExControl()
        {
            InitializeComponent();

            ButtonBus<ExMenuKey>.HookupSubscriber(this.HandleMenuCommand);
        }

        const int panelVersion = 2;

        public IDockSystem DockingSystem
        {
            get { return new AvalonDockSystem(this.Docky, panelVersion); }
        }

        public void ConfigureMenu(IMainMenu menu)
        {
            menu.AddCommandChild(TopMenuKey.File, ExMenuKey.FileNew, "_New");
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            Abnaki.Windows.Software.Wpf.Diag.Design.DebugAncestry(this.Flag); // demo, no effect

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
            ds.Item.AddItemRow("Butter", vrow, new DateTime(2030, 1, 1), 4);
            ds.Item.AddItemRow("Eggs", vrow, new DateTime(2030, 1, 5), 12);

            this.Gridc.BindGrid(ds.Item);

            IEnumerable<Col> cols = new[]{ 
                new Col(ds.Item.QuantityColumn), 
                new Col(ds.Item.NameColumn)
            };

            this.Gridc.ConfigureColumns(cols);
        }

    }
}
