using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace Ex01_Hello.ViewModels
{
    class VmMain : Abnaki.Windows.Software.Wpf.AbnakiViewModel
    {
        public VmMain()
        {
            CmdThrowException = new Commands.ThrowException();
            CmdSaveLog = new Commands.SaveLog();
        }

        // public because internal causes inactive buttons
        public ICommand CmdThrowException { get; private set; }
        public ICommand CmdSaveLog { get; private set; }

    }
}
