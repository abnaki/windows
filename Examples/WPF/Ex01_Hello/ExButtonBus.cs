using System;
using System.Collections.Generic;
using System.Diagnostics;
using Abnaki.Windows.Software.Wpf.Menu;


namespace Ex01_Hello
{
    /// <summary>
    /// Uses MessageTube with buttons
    /// </summary>
    class ExButtonBus : ButtonBus<ButtonKey>
    {
        protected override void HandleButton(ButtonMessage<ButtonKey> m)
        {
            base.HandleButton(m);
            Debug.WriteLine(m);
        }
    }
}
