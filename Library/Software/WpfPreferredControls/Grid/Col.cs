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

        public Col(string field)
        {
            this.Field = field;
        }

        public Col(System.Data.DataColumn dcol)
        {
            this.Field = dcol.ColumnName;
            this.Caption = dcol.Caption;
        }

        public string Field { get; set; }

        /// <summary>
        /// Null may imply Field will be displayed.
        /// </summary>
        public string Caption { get; set; }

        public string Tooltip { get; set; }
    }
}
