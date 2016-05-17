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

//using Abnaki.Windows.Software.Wpf.PreferredControls;
using Abnaki.Windows.Software.Wpf.PreferredControls.Grid;

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
        }

        public object DockingSystem
        {
            get { return this.Docky; }
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            Abnaki.Windows.Software.Wpf.Diag.Design.DebugAncestry(this.Flag); // demo, no effect

            BindExample();
        }

        void BindExample()
        {
            ShoppingData ds = new ShoppingData();
            var vrow = ds.Vendor.AddVendorRow("Kroger");
            ds.Item.AddItemRow("Butter", vrow, new DateTime(2030, 1, 1), 4);
            ds.Item.AddItemRow("Eggs", vrow, new DateTime(2030, 1, 5), 12);

            this.Gridc.BindGrid(ds.Item);
        }

    }
}
