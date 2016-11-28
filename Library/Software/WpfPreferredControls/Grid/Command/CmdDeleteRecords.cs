using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Data;


namespace Abnaki.Windows.Software.Wpf.PreferredControls.Grid.Command
{
    /// <summary>
    /// Unused because viewmodel does not know about multiple selected records.
    /// </summary>
    class CmdDeleteRecords : CmdOnRecords
    {
        public CmdDeleteRecords(Xceed.Wpf.DataGrid.DataGridCollectionView dcv) : base(dcv)
        {
        }

        public override void Execute(object parameter)
        {
            // DataGridCollectionView does not have any more general valid Remove functionality

            if (this.Table != null)
            {
                DataRow dr = this.Collection.CurrentItem as DataRow;
                if (dr != null)
                {
                    this.Table.Rows.Remove(dr);
                    this.Collection.Refresh();
                }
            }
        }
    }
}
