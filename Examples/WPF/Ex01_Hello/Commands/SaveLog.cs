using System;
using System.Windows.Input;
using SaveFileDialog = System.Windows.Forms.SaveFileDialog;

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
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Filter = "XML|.xml";
                dialog.DefaultExt = ".xml";
                dialog.Title = "Save Log File";

                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    Abnaki.Windows.Log.Write(dialog.FileName);
                }
            }

        }
    }
}
