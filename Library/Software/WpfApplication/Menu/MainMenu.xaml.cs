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

        public void AddCommand(object key, string label, bool? defaultCheck = null)
        {
            AddMenuItem(this.RootMenu, key, label, defaultCheck);
        }

        public void AddCommandChild(object parentKey, object childKey, string label, bool? defaultCheck = null)
        {
            MenuItem parentItem = this.RootMenu.Items.Cast<MenuItem>().First(item => parentKey.Equals(item.Tag));

            AddMenuItem(parentItem, childKey, label, defaultCheck);
        }

        void AddMenuItem(ItemsControl container, object key, string label, bool? defaultCheck = null)
        {
            MenuItem item = new MenuItem();
            item.Header = label;
            item.Tag = key;
            container.Items.Add(item);
        }
    }

}
