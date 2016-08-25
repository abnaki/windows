using System;
using System.Collections.Generic;
using System.Linq;

using Abnaki.Windows.Software.Wpf.Menu;
using Abnaki.Windows.GUI;

namespace Abnaki.Windows.Software.Wpf.Ultimate
{
    public class MainMenuBus : ButtonBus<SubMenuKey> 
    {
        public MainMenuBus(MainMenu menu)
        {
            menu.AddCommand(TopMenuKey.File, "_File");
            menu.AddCommand(TopMenuKey.Option, "_Option");
            menu.AddCommand(TopMenuKey.Window, "_Window");
            menu.AddCommand(TopMenuKey.Help, "_Help");

            menu.AddCommandChild(TopMenuKey.File, SubMenuKey.FileExit, "E_xit");

            // FYI some attributes of SubMenuKey enum values will be respected

            var seed = new MenuSeed<SubMenuKey>(){ ParentKey = TopMenuKey.Help, Key = SubMenuKey.HelpTroubleshoot, 
            Label = "_Troubleshoot", 
            Tooltip = "Provides information and files for you to seek help or report issues."};

            menu.AddCommand(seed);

            menu.AddCommandChild(TopMenuKey.Window, SubMenuKey.SaveUserPlacement, "Save Placement");
            menu.AddCommand(new MenuSeed<SubMenuKey>() { ParentKey = TopMenuKey.Window, Key = SubMenuKey.SaveAsPlacement, 
                Label = "Save Placement As...", DebugOnly = true });
            menu.AddCommandChild(TopMenuKey.Window, SubMenuKey.ReadUserPlacement, "Restore Own Placement");
            menu.AddCommandChild(TopMenuKey.Window, SubMenuKey.ReadDefaultPlacement, "Restore Default Placement");
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

                case SubMenuKey.HelpTroubleshoot:
                    Diplomat.Troubleshooter.Shoot();
                    break;

            }
        }
    }
}
