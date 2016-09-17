using System;
using System.Collections.Generic;

namespace Abnaki.Windows.Software.Wpf.PreferredControls.Grid.Event
{
    /// <summary>
    /// Cell of a Field within a record or row
    /// </summary>
    public class RecordCellEventArgs
    {
        public Xceed.Wpf.DataGrid.DataGridItemEventArgs ItemArgs { get; set; }
        
        /// <summary>
        /// Name of property of Record
        /// </summary>
        public string Field { get; set; }

        /// <summary>
        /// Preferred
        /// </summary>
        public object Record
        {
            get
            {
                return ItemArgs.Item;
            }
        }
    }
}
