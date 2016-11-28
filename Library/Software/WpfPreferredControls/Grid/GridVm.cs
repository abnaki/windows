using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Data;
using System.Windows;
using System.Windows.Input;
using PropertyChanged;

namespace Abnaki.Windows.Software.Wpf.PreferredControls.Grid
{
    /// <summary>
    /// ViewModel for Grid
    /// </summary>
    [ImplementPropertyChanged]
    public class GridVm
    {
        public GridVm(IEnumerable data)
        {
            this.Data = new Xceed.Wpf.DataGrid.DataGridCollectionView(data);

            this.AddRecords = new Command.CmdAddRecords(this.Data);
            //this.DeleteRecords = new Command.CmdDeleteRecords(this.Data);
        }

        public void Refresh()
        {
            this.Data.Refresh();
        }

        /// <summary>
        /// Expect Grid to bind to it. 
        /// Inherits System.Windows.Data.CollectionView.
        /// </summary>
        public Xceed.Wpf.DataGrid.DataGridCollectionView Data { get; set; }

        public ICommand AddRecords { get; set; }
        //public ICommand DeleteRecords { get; set; }
    }
}
