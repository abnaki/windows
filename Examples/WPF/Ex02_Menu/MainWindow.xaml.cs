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

namespace Ex02_Menu
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            PopulateMenu();
        }

        void PopulateMenu()
        {
            this.ExTopMenu.AddCommand(TopMenuKey.File, "_File");
            this.ExTopMenu.AddCommand(TopMenuKey.Help, "_Help");

            this.ExTopMenu.AddCommandChild(TopMenuKey.File, SubMenuKey.Exit, "E_xit");
        }
    }
}
