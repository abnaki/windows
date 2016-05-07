using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;

namespace Abnaki.Windows.Software.Wpf
{
    /// <summary>
    /// Base class
    /// </summary>
    public class AbnakiViewModel : INotifyPropertyChanged
    {
        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
        {
            add
            {
                //throw new NotImplementedException();
            }
            remove
            {
                //throw new NotImplementedException();
            }
        }
    }
}
