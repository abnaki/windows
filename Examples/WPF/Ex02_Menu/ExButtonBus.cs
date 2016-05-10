using System;
using System.Collections.Generic;
using System.Diagnostics;
using Abnaki.Windows.Software.Wpf.Menu;
using TopMenuKey = Abnaki.Windows.Software.Wpf.Ultimate.TopMenuKey;

namespace Ex02_Menu
{
    /// <summary>
    /// Populates a MainMenu and handles user inputs.
    /// An instance of this class is kept somewhere appropriate, MainWindow.
    /// </summary>
    /// <remarks>
    /// Uses MessageTube. 
    /// </remarks>
    class ExButtonBus : ButtonBus<SubMenuKey>
    {
        public ExButtonBus(MainMenu mm)
        {
            mm.AddCommand(TopMenuKey.File, "_File");
            mm.AddCommand(TopMenuKey.Option, "_Option");
            mm.AddCommand(TopMenuKey.Help, "_Help");

            mm.AddCommandChild(TopMenuKey.File, SubMenuKey.Exit, "E_xit");

            mm.AddCommandChild(TopMenuKey.Option, SubMenuKey.Mayo, "_Mayo", false);

        }

        protected override void HandleButton(ButtonMessage<SubMenuKey> m)
        {
            base.HandleButton(m);

            //Debug.WriteLine(m);

            switch (m.Key)
            {
                case SubMenuKey.Exit:
                    System.Windows.Application.Current.Shutdown();  // crude
                    break;
            }
        }
    }
}
