using System;
using System.Collections.Generic;
using System.Diagnostics;
using Abnaki.Windows.Software.Wpf.Menu;

namespace Ex02_Menu
{
    /// <summary>
    /// Uses MessageTube with buttons
    /// </summary>
    class ExButtonBus : ButtonBus<SubMenuKey>
    {
        protected override void HandleButton(ButtonMessage<SubMenuKey> m)
        {
            base.HandleButton(m);

            //Debug.WriteLine(m);

            switch ( m.Key )
            {
                case SubMenuKey.Exit:
                    System.Windows.Application.Current.Shutdown();  // crude
                    break;
            }
        }
    }
}
