using System;
using System.Collections.Generic;
using System.Linq;

using Abnaki.Windows.Software.Wpf.Menu;

namespace Abnaki.Windows.Software.Wpf.Ultimate
{
    public class MainMenuBus : ButtonBus<SubMenuKey> 
    {
        public MainMenuBus(MainMenu menu)
        {
            menu.AddCommand(TopMenuKey.File, "_File");
            menu.AddCommand(TopMenuKey.Option, "_Option");
            menu.AddCommand(TopMenuKey.Help, "_Help");

            menu.AddCommandChild(TopMenuKey.File, SubMenuKey.FileExit, "E_xit");
        }

        protected override void HandleButton(ButtonMessage<SubMenuKey> m)
        {
            base.HandleButton(m);

            // important general commands
            switch ( m.Key )
            {
                case SubMenuKey.FileExit:
                    // will want confirmation and preliminaries/housekeeping
                    System.Windows.Application.Current.Shutdown();
                    break;

            }
        }
    }
}
