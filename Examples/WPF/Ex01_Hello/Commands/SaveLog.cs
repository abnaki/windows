using System;
using System.Windows.Input;

namespace Ex01_Hello.Commands
{
    class SaveLog : ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            Abnaki.Windows.Software.Wpf.Diplomat.Troubleshooter.DialogSaveLog();
        }
    }
}
