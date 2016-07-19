using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Data;
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
        }

        public void Refresh()
        {
            this.Data.Refresh();
        }

        /// <summary>
        /// Expect Grid to bind to it.
        /// </summary>
        public CollectionView Data {get; set;}
    }
}
