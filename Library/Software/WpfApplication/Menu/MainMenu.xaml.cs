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

namespace Abnaki.Windows.Software.Wpf.Menu
{
    /// <summary>
    /// A control for a customary main menu to populate programmatically
    /// </summary>
    /// <remarks>
    /// Because the menu is wrapped in this class, users do not populate it with XAML.
    /// Menu commands should be sufficiently simple, not needing to much customization;
    /// this public interface allows programmatic possibilities; 
    /// and lets developers avoid time-consuming designer interaction.
    /// In some situations, menu commands are known at run time, not design time.
    /// </remarks>
    public partial class MainMenu : UserControl
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        public void AddCommand<Tkey>(Tkey key, string label, bool? defaultCheck = null)
        {
            MenuItem item = AddMenuItem(key, label, defaultCheck);

            item.Click += ItemClick<Tkey>;
        }

        public void AddCommandChild<Tkey>(object parentKey, Tkey childKey, string label, bool? defaultCheck = null)
        {
            MenuItem item = AddItemChild(parentKey, childKey, label, defaultCheck);

            item.Click += ItemClick<Tkey>;
        }

        void ItemClick<Tkey>(object sender, RoutedEventArgs e)
        {
            MenuItem item = (MenuItem)sender;
            Tkey key = (Tkey)item.Tag;
            bool? isChecked = item.IsCheckable ? (bool?)item.IsChecked : (bool?)null;
            ButtonBus<Tkey>.Handle(key, isChecked);
        }

        MenuItem AddMenuItem(object key, string label, bool? defaultCheck)
        {
            return AddMenuItem(this.RootMenu, key, label, defaultCheck);
        }

        MenuItem AddItemChild(object parentKey, object childKey, string label, bool? defaultCheck)
        {
            MenuItem parentItem = this.RootMenu.Items.Cast<MenuItem>().First(item => parentKey.Equals(item.Tag));

            return AddMenuItem(parentItem, childKey, label, defaultCheck);
        }

        MenuItem AddMenuItem(ItemsControl container, object key, string label, bool? defaultCheck)
        {
            MenuItem item = new MenuItem();
            item.Header = label;
            item.Tag = key;

            item.IsCheckable = defaultCheck.HasValue;
            if (defaultCheck.HasValue)
                item.IsChecked = defaultCheck.Value;

            container.Items.Add(item);
            return item;
        }

    }

}
