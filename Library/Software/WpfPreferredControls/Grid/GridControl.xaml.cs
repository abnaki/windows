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

namespace Abnaki.Windows.Software.Wpf.PreferredControls.Grid
{
    /// <summary>
    /// Wraps a data grid.
    /// </summary>
    public partial class GridControl : UserControl
    {
        public GridControl()
        {
            InitializeComponent();
        }

        GridVm vm;

        /// <summary>
        /// Assign data
        /// </summary>
        /// <param name="data">
        /// System.Data.DataTable or other object that can typically go into a CollectionView
        /// </param>
        public void BindGrid(System.Collections.IEnumerable data)
        {
            vm = new GridVm(data);
            this.DataContext = vm;
        }
    }
}
