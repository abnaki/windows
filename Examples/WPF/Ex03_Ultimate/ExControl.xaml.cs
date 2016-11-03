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

        }

        const int panelVersion = 2;

        public IDockSystem DockingSystem
        {
            get { return new AvalonDockSystem(this.Docky, panelVersion); }
        }

        public void ConfigureMenu(IMainMenu menu)
        {
            menu.AddCommandChild(TopMenuKey.File, ExMenuKey.FileNew, "_New");

            MenuSeed<ExMenuKey> seed = new MenuSeed<ExMenuKey>(ExMenuKey.FileDemoKey, "Demo");
            seed.ParentKey = TopMenuKey.File;
            seed.ShortcutKey = Key.D; seed.ShortcutModifier = ModifierKeys.Control;
            seed.Tooltip = "Uses keyboard shortcut to invoke something.";
            menu.AddCommand(seed);

            menu.AddCommandChild(TopMenuKey.Option, ExMenuKey.OptionFlag, "_Flag");
            ButtonBus<ExMenuKey>.AddExclusiveCommands(menu, ExMenuKey.OptionFlag,
                new[] { ExMenuKey.OptionFlagAustria, ExMenuKey.OptionFlagBulg, ExMenuKey.OptionFlagNed });
        }

        public event Action<string> MainTitle;

        public void EmplacedInWindow()
        {
            MainTitle("Ultimate Example");
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            Abnaki.Windows.Software.Wpf.Diag.Design.DebugAncestry(this.Flag); // demo, no effect

        }

    }
}
