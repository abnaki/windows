using System;
using System.Windows.Input;

namespace Ex01_Hello.Commands
{
    class ThrowException : ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            throw new ApplicationException("Demonstration Exception");
        }
    }
}
