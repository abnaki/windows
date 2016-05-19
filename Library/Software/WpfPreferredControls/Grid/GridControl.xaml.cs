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
using System.Diagnostics;
using System.Data;

//using Xceed.Wpf.DataGrid.Views;
//using Xceed.Wpf.DataGrid;

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

            Debug.WriteLine("Grid.ReadOnly " + this.Grid.ReadOnly);

            //var v = (Xceed.Wpf.DataGrid.Views.TableflowView)this.Grid.View;
            //v.UseDefaultHeadersFooters = false;
            //DataTemplate dt = new DataTemplate();
            //v.FixedHeaders.
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

        private void Grid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // demo
            Debug.WriteLine("Grid SelectedItems");
            Debug.Indent();
            foreach ( DataRow dr in this.Grid.SelectedItems.OfType<DataRow>() )
            {
                Debug.WriteLine(dr);
            }
            Debug.Unindent();

            Debug.WriteLine("Grid CurrentItem " + this.Grid.CurrentItem);
            
        }
    }
}
