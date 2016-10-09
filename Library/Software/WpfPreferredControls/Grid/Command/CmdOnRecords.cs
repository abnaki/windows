using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Windows.Input;

using Xceed.Wpf.DataGrid;

namespace Abnaki.Windows.Software.Wpf.PreferredControls.Grid.Command
{
    abstract class CmdOnRecords : ICommand
    {
        public CmdOnRecords(DataGridCollectionView dcv)
        {
            this.Collection = dcv;

            this.Table = this.Collection.SourceCollection as DataTable;
        }

        public bool CanExecute(object parameter)
        {
            return this.Collection.SourceCollection != null;
        }

        public event EventHandler CanExecuteChanged;

        public abstract void Execute(object parameter);

        protected DataGridCollectionView Collection { get; private set; }

        protected DataTable Table { get; private set; }


    }
}
