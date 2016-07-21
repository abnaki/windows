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

namespace Abnaki.Windows.Software.Wpf.Menu
{
    /// <summary>
    /// A control for a customary main menu to populate programmatically
    /// </summary>
    /// <remarks>
    /// Because the menu is wrapped in this class, users do not populate it with XAML.
    /// Menu commands should be sufficiently simple, not needing much customization;
    /// this public interface allows programmatic possibilities; 
    /// and lets developers avoid time-consuming designer interaction.
    /// In some situations, menu commands are known at run time, not design time.
    /// </remarks>
    public partial class MainMenu : UserControl, IMainMenu
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        public void AddCommand<Tkey>(MenuSeed<Tkey> seed)
        {
#if DEBUG
#else
            if (seed.DebugOnly)
                return;
#endif

            MenuItem item;
            if (seed.ParentKey == null)
                item = AddMenuItem(seed.Key, seed.Label, seed.DefaultCheck);
            else
                item = AddItemChild(seed.ParentKey, seed.Key, seed.Label, seed.DefaultCheck);

            CompleteItem<Tkey>(item, seed);
        }

        public void AddCommand<Tkey>(Tkey key, string label, bool? defaultCheck = null)
        {
            AddCommand<Tkey>(new MenuSeed<Tkey>(key, label, defaultCheck));
        }

        public void AddCommandChild<Tkey>(object parentKey, Tkey childKey, string label, bool? defaultCheck = null)
        {
            AddCommand<Tkey>(new MenuSeed<Tkey>(childKey, label, defaultCheck) { ParentKey = parentKey });
        }

        void CompleteItem<Tkey>(MenuItem item, MenuSeed<Tkey> seed)
        {
            item.Click += ItemClick<Tkey>;
            item.ToolTip = seed.Tooltip;

            if ( seed.Enabled.HasValue )
                item.IsEnabled = seed.Enabled.Value;

            item.Items.SortDescriptions.Clear();
            item.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription("CommandParameter", System.ComponentModel.ListSortDirection.Ascending));
        }

        void ItemClick<Tkey>(object sender, RoutedEventArgs e)
        {
            MenuItem item = (MenuItem)sender;
            Tkey key = (Tkey)item.Tag;
            bool? isChecked = item.IsCheckable ? (bool?)item.IsChecked : (bool?)null;

            ButtonBus<Tkey>.Handle(key, isChecked); // the whole point

            e.Handled = true;  // otherwise, tunnels down through MenuItem hierarchy
            
        }

        MenuItem AddMenuItem(object key, string label, bool? defaultCheck)
        {
            return AddMenuItem(this.RootMenu, key, label, defaultCheck);
        }

        MenuItem AddItemChild(object parentKey, object childKey, string label, bool? defaultCheck)
        {
            MenuItem parentItem = FindItem(parentKey, this.RootMenu.Items);

            MenuItem itemChild = AddMenuItem(parentItem, childKey, label, defaultCheck);

            // should be unnecessary
            //itemChild.Background = parentItem.Background;
            //itemChild.Foreground = parentItem.Foreground;
            //itemChild.Style = parentItem.Style;
            //itemChild.Template = parentItem.Template;

            return itemChild;
        }

        MenuItem FindItem(object tag, ItemCollection items)
        {
            MenuItem foundItem = items.Cast<MenuItem>().FirstOrDefault(item => tag.Equals(item.Tag));
            if (foundItem == null )
            { // recurse
                foreach ( MenuItem item in items )
                {
                    foundItem = FindItem(tag, item.Items);
                    if (foundItem != null)
                        break;
                }
            }
            return foundItem;
        }

        MenuItem AddMenuItem(ItemsControl container, object key, string label, bool? defaultCheck)
        {
            if (container == null)
                throw new ArgumentException("Null container of menu items for " + key);

            MenuItem item = new MenuItem();
            item.Header = label;
            //AssignTag(item, key);
            item.Tag = key;
            AssignIntegerSequence(item, key);

            item.IsCheckable = defaultCheck.HasValue;
            if (defaultCheck.HasValue)
                item.IsChecked = defaultCheck.Value;

            container.Items.Add(item);

            if (container.Items.Count > 1)
            {  // effect of AssignIntegerSequence()
                container.Items.Refresh();
            }

            return item;
        }

        static void AssignIntegerSequence(MenuItem item, object key)
        {
            // Nothing special about CommandParameter; it is an available object field.
            // (int) required for disparate enums via SortDescriptions.
            item.CommandParameter = (int)key; 
        }
    }

}
