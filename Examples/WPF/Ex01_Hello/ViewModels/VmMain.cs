using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Abnaki.Windows.Software.Wpf;

namespace Ex01_Hello.ViewModels
{
    class VmMain : AbnakiViewModel
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
