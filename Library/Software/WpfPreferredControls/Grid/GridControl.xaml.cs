using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Xceed.Wpf.DataGrid;

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

        public event Action<object> DoubleClickedRecord;

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

        public void ConfigureColumns(IEnumerable<Col> columns)
        {
            //this.Grid.Columns.Clear();

            if (this.Grid.Columns.Count == 0)
                return;

            int position = 0;

            SortedSet<int> indices = new SortedSet<int>(Enumerable.Range(0, this.Grid.Columns.Count));

            Dictionary<string, double> colWidths = new Dictionary<string, double>();

            foreach ( Col col in columns )
            {
                ColumnBase cb = this.Grid.Columns[col.Field];
                indices.Remove(cb.Index);
                cb.Visible = true;
                cb.Title = col.Caption ?? col.Field;
                cb.VisiblePosition = position++;
                colWidths[cb.FieldName] = cb.GetFittedWidth();
            }
            
            // now remaining indices are unspecified columns:  hide.
            foreach ( int i in indices )
            {
                ColumnBase cb = this.Grid.Columns[i];
                cb.Visible = false;
            }

            // tricks
            //this.Grid.Items.Refresh(); // no effect
            //this.Grid.UpdateLayout();

            foreach ( var pair in colWidths )
            {
                if ( pair.Value > 0 )
                {
                    ColumnBase cb = this.Grid.Columns[pair.Key];
                    cb.Width = pair.Value + 2.0;
                }
            }
            
        }

        private void Grid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // demo
            //Debug.WriteLine("Grid SelectedItems");
            //Debug.Indent();
            //foreach ( var dr in this.Grid.SelectedItems.OfType<System.Data.DataRow>() )
            //{
            //    Debug.WriteLine(dr);
            //}
            //Debug.Unindent();

            Debug.WriteLine("Grid CurrentItem " + this.Grid.CurrentItem);

            var hd = DoubleClickedRecord;
            if (hd != null)
                hd(this.Grid.CurrentItem);
            
        }
    }
}
