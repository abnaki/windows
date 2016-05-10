using System;

using Abnaki.Windows.Software.Wpf.Ultimate;

namespace Ex03_Ultimate
{
    class ExStarter
    {
        [STAThread]
        public static int Main(string[] args)
        {
            return UltimateStarter<ExControl>.Start(args);
        }

    }
}
