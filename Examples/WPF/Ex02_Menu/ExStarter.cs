using System;

namespace Ex02_Menu
{
    class ExStarter
    {
        // also see Ex01_Menu ExStarter
        [STAThread]
        public static int Main(string[] args)
        {
            return Abnaki.Windows.Software.Wpf.Starter.Start<MainWindow>(args);
        }
    }
}
