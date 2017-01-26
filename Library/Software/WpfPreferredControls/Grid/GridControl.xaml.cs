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
using CodeRed.Serialization; // SerializableDictionary

//using Xceed.Wpf.DataGrid.Views;
using Xceed.Wpf.DataGrid;
using Abnaki.Windows.Software.Wpf.Profile;
using System.Xml.Serialization;
using System.ComponentModel;
using Xceed.Wpf.DataGrid.Views;

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

            // https://xceed.com/forums/topic/Add-ToolTip-when-ho-vering-over-Cell/
            EventManager.RegisterClassHandler(typeof(Cell), Cell.MouseEnterEvent, new RoutedEventHandler(OnCellMouseEnter));

            Debug.WriteLine("Grid.ReadOnly " + this.Grid.ReadOnly);

            Clear();

            RowFontRatio = 2; // better while editing

            //var v = (Xceed.Wpf.DataGrid.Views.TableflowView)this.Grid.View;
            //v.UseDefaultHeadersFooters = false;
            //DataTemplate dt = new DataTemplate();
            //v.FixedHeaders.
        }

        // future reference:
        // Grid.CurrentContext is interesting

        GridVm vm;

        public new GridVm DataContext
        {
            set { base.DataContext = value; }
            get { return base.DataContext as GridVm;  }
        }

        public bool EnableOptionalGridControls
        {
            set
            {
                this.GridOptionalPanel.Height = value ? 30 : 0;
            }
        }

        /// <summary>
        /// Ratio of row height to FontSize.  Default 2.
        /// </summary>
        public double RowFontRatio
        {
            get
            {
                var view = (TableflowView)this.Grid.View;
                return view.ContainerHeight / this.FontSize;
            }
            set
            {
                var view = (TableflowView)this.Grid.View;
                view.ContainerHeight = value * this.FontSize;
            }
        }

        public event Action<object> DoubleClickedRecord;

        Decoration decoration = new Decoration();

        /// <summary>Absolute
        /// </summary>
        public void Clear()
        {
            GridPref = null;
            ClearData();
        }

        public void ClearData()
        {
            decoration.Clear();
            this.DataContext = null;
            this.Grid.Items.Refresh();
        }

        /// <summary>
        /// Assign data
        /// </summary>
        /// <param name="data">
        /// System.Data.DataTable or other object that can typically go into a CollectionView
        /// </param>
        public void BindGrid(System.Collections.IEnumerable data)
        {
            UpdatePref(); // survives from old binding, if any

            vm = new GridVm(data);
            this.DataContext = vm;

            this.DataContext.Data.EditCommitted += Data_EditCommitted;
        }

        /// <summary>
        /// Recommended.  Required for GridPrefs to work.
        /// </summary>
        /// <param name="columns">sequence of appearance, with Col options
        /// </param>
        public void ConfigureColumns(IEnumerable<Col> columns)
        {
            //this.Grid.Columns.Clear();

            this.Grid.UpdateLayout();

            if (this.Grid.Columns.Count == 0)
                return;

            int position = 0;

            bool prefExisted = GridPref != null;
            if (!prefExisted)
                GridPref = new Pref();

            SortedSet<int> indices = new SortedSet<int>(Enumerable.Range(0, this.Grid.Columns.Count));

            //Dictionary<string, double> colWidths = new Dictionary<string, double>();

            foreach ( Col col in columns )
            {
                ColumnBase cb = this.Grid.Columns[col.Field];
                indices.Remove(cb.Index);
                cb.Visible = true;
                cb.Title = col.Caption ?? col.Field;

                if (!string.IsNullOrWhiteSpace(col.Tooltip))
                    decoration.MapColumnTooltip[col.Field] = col.Tooltip;

                SuitableDefaults(cb);

                SetColumnFormat(cb, col.Format);

                if (!prefExisted)
                {
                    cb.VisiblePosition = position++;
                    cb.Width = BetterFittedWidth(cb) ?? cb.Width;
                    GridPref.ColumnPrefs[cb.FieldName] = new ColumnPref(cb.Width, cb.VisiblePosition);
                }
            }
            
            // now remaining indices are unspecified columns:  hide.
            foreach ( int i in indices )
            {
                ColumnBase cb = this.Grid.Columns[i];
                cb.Visible = false;
            }

            if ( prefExisted )
                EnforceColumnPrefs();

        }

        void SetColumnFormat(ColumnBase cb, string format)
        {
            if (string.IsNullOrEmpty(format))
                return;

            RelativeSource rs = null;
            PropertyPath path = new PropertyPath(cb.FieldName);

            var prop = PropertyofColumn(cb);
            if (prop.DataType.IsGenericType)
            {
                // workaround of behavior of DataGridControl 2.9
                rs = new RelativeSource(RelativeSourceMode.Self);
                path = new PropertyPath("(0)", CellContentPresenter.DataContextProperty);
            }

            FrameworkElementFactory fe = new FrameworkElementFactory(typeof(TextBlock));
            Binding textBind = new Binding()
            {
                Path = path,
                RelativeSource = rs
            };
            // b.StringFormat = "{0:" + format + "}"; // valid in some simple cases
            textBind.StringFormat = format;
            fe.SetBinding(TextBlock.TextProperty, textBind);

            //Style formStyle = new Style(typeof(TextBlock));
            //formStyle.Setters.Add(new Setter(TextBlock.TextProperty, textBind)); 
            //// but will not apply format to Nullables.
            //fe.SetValue(TextBlock.StyleProperty, formStyle);

            cb.CellContentTemplate = new DataTemplate() { VisualTree = fe };            
        }

        void EnforcePrefs()
        {
            EnforceUnboundPrefs();
            EnforceColumnPrefs();
        }

        void EnforceUnboundPrefs()
        {
            // anything from GridPref that is valid in absence of any columns or records
        }

        void EnforceColumnPrefs()
        {
            try
            {
                if (GridPref != null)
                {
                    foreach (var pair in GridPref.ColumnPrefs.OrderBy(p => p.Value.VisiblePosition) )
                    {
                        ColumnPref pref = pair.Value;
                        ColumnBase cb = this.Grid.Columns[pair.Key];
                        if (cb == null) // no longer exists
                            continue;

                        if (pref.Width > 0)
                            cb.Width = pref.Width;

                        cb.VisiblePosition = pref.VisiblePosition;
                    }

                    GridPref.Completed = true;
                }
            }
            catch ( Exception ex )
            {
                AbnakiLog.Exception(ex);
            }
        }

        void UpdatePref()
        {
            if ( GridPref != null && false == GridPref.Completed )
            {
                 // awaiting completion of usage
            }
            else if (Grid.Columns.Any()) // or any other data to persist
            {
                GridPref = new Pref();

                foreach (ColumnBase cb in this.Grid.Columns)
                {
                    GridPref.ColumnPrefs[cb.FieldName] = new ColumnPref(cb.Width, cb.VisiblePosition);
                }
            }
            else
            {   // unexpected
                GridPref = null;
            }
        }

        double? BetterFittedWidth(ColumnBase cb)
        {
            List<object> sampleValues = ((GridVm)cb.DataGridControl.DataContext).Data.Cast<object>()
                .Select(r => GetField(r, cb))
                .Where(v => v != null)
                .Take(5)
                .ToList();

            sampleValues.Add(cb.Title);

            double w = cb.GetFittedWidth();
            if (sampleValues.Any())
            {
                double wcalc = 2 + 7 * sampleValues.Max(v => Convert.ToString(v).Length);
                w = Math.Max(wcalc, w);
            }
            if ( w > 0)
                return w;

            return null;
        }

        //static object GetField(object record, ColumnBase cb)
        //{
        //    var prop = record.GetType().GetProperty(cb.FieldName);
        //    return prop.GetValue(record);
        //}

        object GetField(object record, ColumnBase cb)
        {
            var prop = PropertyofColumn(cb);
            return prop.GetValue(record);
        }

        void SuitableDefaults(ColumnBase cb)
        {
            var prop = PropertyofColumn(cb);

            // want click on a bool checkbox to work immediately.  see MouseLeftButtonHandler handler and
            //  https://xceed.com/forums/topic/Single-Click-Editing-Question-CheckBox/
            if (IsBoolean(prop))
            {
                cb.ReadOnly = true; 
            }
        }

        DataGridItemPropertyBase PropertyofColumn(ColumnBase cb)
        {
            GridVm vm = (GridVm)cb.DataGridControl.DataContext;
            return vm.Data.ItemProperties.First(p => p.Name == cb.FieldName);
        }

        static bool IsBoolean(DataGridItemPropertyBase prop)
        {
            return prop.DataType == typeof(bool)
                || prop.DataType.GetGenericArguments().FirstOrDefault() == typeof(bool); // nullable
        }


        private void OnCellMouseEnter(object sender, RoutedEventArgs e)
        {
            Cell cell = (Cell)sender;
            if (cell == null) 
                return;

            cell.ToolTip = null;
            double fitWidth = cell.GetFittedWidth();
            ColumnBase cb = cell.ParentColumn;

            if (cell.Width < fitWidth)
            {
                if (cell is ColumnManagerCell) // header
                {
                    ColumnManagerCell column = (ColumnManagerCell)cell;

                    column.ToolTip = column.Content.ToString();
                }
                else if (cell is DataCell)
                {
                    cell.ToolTip = Convert.ToString(GetField(cell.ParentRow.DataContext, cb));

                    //EntityBase2 enity = cell.DataContext as EntityBase2;
                    //if (enity != null && cell.Width < fitWidth)
                    //{
                    //    cell.ToolTip = enity.Fields[cell.FieldName].CurrentValue.ToString();
                    //}
                }
            }

            string tip = decoration.GetColumnTooltip(cb.FieldName);

            if (false == string.IsNullOrWhiteSpace(tip))
            {
                if ( cell.ToolTip != null )
                    cell.ToolTip += "\n";

                cell.ToolTip += tip;
            }

            // e.Handled = true;

        }


        private void RowDoubleClick(object sender, MouseButtonEventArgs e)
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

        public event Action<object,Event.RecordCellEventArgs> GridEditCommitted;

        void OnEditCommit(object sender, DataGridItemEventArgs e)
        {
            var erec = new Event.RecordCellEventArgs(){ItemArgs = e,  Field = this.Grid.CurrentColumn.FieldName};
            var h = GridEditCommitted;
            if (h != null)
                h(sender, erec);
        }

        void Data_EditCommitted(object sender, DataGridItemEventArgs e)
        {
            OnEditCommit(sender, e);
        }

        private void CellMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (Grid.CurrentColumn != null && Grid.CurrentItem != null)
            {
                var prop = PropertyofColumn(Grid.CurrentColumn);
                if (IsBoolean(prop))
                {
                    bool bold = (bool)(prop.GetValue(Grid.CurrentItem) ?? (object)false);
                    prop.SetValue(Grid.CurrentItem, !bold); // invert

                    DataGridItemEventArgs ecommit = new DataGridItemEventArgs(DataContext.Data, Grid.CurrentItem);
                    OnEditCommit(sender, ecommit);
                }
            }
        }

        public void Refresh()
        {
            //this.Grid.Items.DeferRefresh();

            if (this.DataContext != null)
                this.DataContext.Refresh();
        }

        IEnumerable<System.Data.DataRow> DraggedRows(DragEventArgs e)
        {
            //DataTable dt = this.DataContext.Data.SourceCollection as DataTable;

            List<object> selectList = NetAid.Dragging.Extract<List<object>>(e);
            if ( selectList != null )
            {
                return selectList.OfType<System.Data.DataRow>();
            }
            return Enumerable.Empty<System.Data.DataRow>();
        }

        private void Image_DragOver(object sender, DragEventArgs e)
        {
            if ( DraggedRows(e).Any() )
            {
                e.Effects = e.AllowedEffects;
            }
        }

        private void Image_Drop(object sender, DragEventArgs e)
        {
            var rows = DraggedRows(e);
            foreach ( var dr in rows )
            {
                //Debug.WriteLine("Want to delete " + dr);
                dr.Table.Rows.Remove(dr);
            }

            if (rows.Any())
                Refresh();
        }

        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {
            if ( Mouse.LeftButton == MouseButtonState.Pressed )
            {
                object ehit = HitGridObject(e);
                //Debug.WriteLine("Hit " + ehit);

                if (ehit is Row || ehit is DataCell)
                {
                    List<object> selectList = this.Grid.SelectedItems.Cast<object>().ToList();
                    if (selectList.Count > 0)
                    {
                        DragDrop.DoDragDrop(this.Grid, selectList, DragDropEffects.All);
                    }
                }
            }
        }

        IInputElement HitElement(MouseEventArgs e)
        {
            Point p = e.GetPosition(this.Grid);
            IInputElement ehit = this.Grid.InputHitTest(p);
            return ehit;
        }

        object HitGridObject(MouseEventArgs e)
        {
            DependencyObject x = HitElement(e) as DependencyObject;
            while ( x != null )
            {
                if (x is Cell)
                    return (Cell)x;

                if (x is Row)
                    return (Row)x;

                x = VisualTreeHelper.GetParent(x);
            }
            return null;
        }


        public void RestorePreferences<Towner>()
        {
            GridPref = Preference.ReadClassPrefs<Towner, Pref>();
            EnforceUnboundPrefs();
        }

        public void SavePreferences<Towner>()
        {
            // note Community Edition 2.9 does not have SaveUserSettings and LoadUserSettings

            UpdatePref();
            if (GridPref != null)
                Preference.WriteClassPrefs<Towner, Pref>(GridPref);
        }

        Pref GridPref { get; set; }

        public class Pref
        {
            [XmlIgnore]
            public bool Completed { get; set; }

            public SerializableDictionary<string, ColumnPref> ColumnPrefs
                = new SerializableDictionary<string, ColumnPref>();
        }

        /// <summary>Preferences most often desired by user
        /// </summary>
        public class ColumnPref
        {
            public ColumnPref(double w, int p)
            {
                Width = w;
                VisiblePosition = p;
            }

            public ColumnPref() // serializ.
            {
            }

            public double Width { get; set; }
            public int VisiblePosition { get; set; }

            public override string ToString()
            {
                return GetType().Name + ",Width=" + Width + ",VisiblePosition=" + VisiblePosition;
            }
        }


    }
}
