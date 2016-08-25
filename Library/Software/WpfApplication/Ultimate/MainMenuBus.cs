using System;
using System.Collections.Generic;
using System.Linq;

using Abnaki.Windows.Software.Wpf.Menu;
using Abnaki.Windows.GUI;
using System.Diagnostics;

namespace Abnaki.Windows.Software.Wpf.Ultimate
{
    public class MainMenuBus : ButtonBus<SubMenuKey> 
    {
        public MainMenuBus(MainMenu menu)
        {
            MenuSeed<SubMenuKey> seed;

            menu.AddCommand(TopMenuKey.File, "_File");
            menu.AddCommand(TopMenuKey.Option, "_Option");
            menu.AddCommand(TopMenuKey.Window, "_Window");
            menu.AddCommand(TopMenuKey.Help, "_Help");

            menu.AddCommandChild(TopMenuKey.File, SubMenuKey.FileExit, "E_xit");

            // FYI some attributes of SubMenuKey enum values will be respected

            if (UpgradeUri != null)
            {
                var appTuple = AbnakiReflection.ApplicationNameVersionSplit();

                seed = new MenuSeed<SubMenuKey>(SubMenuKey.HelpUpgrade, "Upgrade...")
                {
                    ParentKey = TopMenuKey.Help,
                    Tooltip = "Open web browser for upgrade of your current " + appTuple.Item1 + " " + appTuple.Item2
                };
                menu.AddCommand(seed);
            }

            seed = new MenuSeed<SubMenuKey>(SubMenuKey.HelpTroubleshoot, "_Troubleshoot...")
            {
                ParentKey = TopMenuKey.Help,
                Tooltip = "Instructions for you to seek help or report issues."
            };
            menu.AddCommand(seed);

            menu.AddCommandChild(TopMenuKey.Window, SubMenuKey.SaveUserPlacement, "Save Placement");
            menu.AddCommand(new MenuSeed<SubMenuKey>() { ParentKey = TopMenuKey.Window, Key = SubMenuKey.SaveAsPlacement, 
                Label = "Save Placement As...", DebugOnly = true });
            menu.AddCommandChild(TopMenuKey.Window, SubMenuKey.ReadUserPlacement, "Restore Own Placement");
            menu.AddCommandChild(TopMenuKey.Window, SubMenuKey.ReadDefaultPlacement, "Restore Default Placement");
        }

        public static Uri UpgradeUri { get; set; }

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

                case SubMenuKey.HelpUpgrade:
                    if ( UpgradeUri != null )
                    {
                        using ( var p = Process.Start(UpgradeUri.ToString()))
                        {
                            // right
                        }
                    }
                    break;
            }
        }
    }
}
