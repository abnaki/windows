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
        public AbnakiViewModel()
        {
            SubscribeEvents();
        }

        // maybe replace with Fody
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

        /// <summary>
        /// Add methods of this to MessageTube.Subscribe() per type T
        /// </summary>
        protected virtual void SubscribeEvents()
        {

        }
    }
}
