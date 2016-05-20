using System;
using System.Collections.Generic;
using System.Linq;

namespace Abnaki.Windows.Software.Wpf.PreferredControls.Grid
{
    /// <summary>
    /// Prescribes display of a column of a grid
    /// </summary>
    public class Col
    {
        public Col()
        {

        }

        public Col(System.Data.DataColumn dcol)
        {
            this.Field = dcol.ColumnName;
            this.Caption = dcol.Caption;
        }

        public string Field { get; set; }
        public string Caption { get; set; }

        // maybe Tooltip in the future
    }
}
