using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace Abnaki.Windows.Software.Wpf.PreferredControls.Grid.Command
{
    class CmdAddRecords : CmdOnRecords
    {
        public CmdAddRecords(Xceed.Wpf.DataGrid.DataGridCollectionView dcv) : base(dcv)
        {
            
        }

        public override void Execute(object parameter)
        {
            int n = Convert.ToInt32(parameter);

            for (int k = 0; k < n; k++)
            {
                object record;
                if (this.Table != null)
                {
                    // in a strongly typed dataset, a DataRow can't be constructed in isolation.
                    record = this.Table.NewRow();
                    this.Table.Rows.Add(record);
                }
                else
                {
                    record = this.Collection.AddNew();
                }
            }

            this.Collection.Refresh();
        }
    }
}
