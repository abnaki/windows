using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using Abnaki.Windows.Software.Wpf;

namespace Ex01_Hello.ViewModels
{
    // maybe promote

    /// <summary>
    /// Uses MessageTube with buttons
    /// </summary>
    class VmButtonBus : AbnakiViewModel  // not yet used as a DataContext
    {
        protected override void SubscribeEvents()
        {
            base.SubscribeEvents();

            MessageTube.Subscribe<ButtonMessage>(HandleButton);
        }

        protected virtual void HandleButton(ButtonMessage m)
        {
            Debug.WriteLine(m);
        }

        public class ButtonMessage
        {
        }

        public class ButtonMessage<Tenum> : ButtonMessage
        {
            public ButtonMessage(Tenum key)
            {
                this.Key = key;
            }

            public ButtonMessage(string s)
            {
                this.Key = (Tenum) Enum.Parse(typeof(Tenum), s, ignoreCase: true);
            }

            public Tenum Key { get; private set; }

            public override string ToString()
            {
                return base.ToString() + " " + Key;
            }
        }
    }
}
