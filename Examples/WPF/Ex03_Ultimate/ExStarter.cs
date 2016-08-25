using System;

using Abnaki.Windows.Software.Wpf.Ultimate;

namespace Ex03_Ultimate
{
    class ExStarter
    {
        [STAThread]
        public static int Main(string[] args)
        {
            // Abnaki.Windows.Software.Wpf.Diplomat.Troubleshooter.Email = "nobody@nowhere.com";
            Abnaki.Windows.Software.Wpf.Ultimate.MainMenuBus.UpgradeUri = new Uri("https://github.com/abnaki/windows/releases/latest");
            // better to do:  Abnaki.Windows.Software.Wpf.Starter.CommonSettings(Properties.Settings.Default);

            return UltimateStarter<ExControl>.Start(args);
        }

    }
}
