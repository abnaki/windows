using System;
using System.Collections.Generic;

namespace Abnaki.Windows.Software.Wpf.Ultimate
{
    /// <summary>
    /// Starts Ultimate MainWindow where a Tcontrol occupies all the useful space
    /// </summary>
    /// <typeparam name="Tcontrol">
    /// </typeparam>
    public class UltimateStarter<Tcontrol>
        where Tcontrol : System.Windows.UIElement, new()
    {
        public static int Start(string[] args)
        {
            return Starter.Start(args, InitWindow);
        }

        static MainWindow InitWindow()
        {
            MainWindow mw = new MainWindow();

            Tcontrol child = new Tcontrol();
            mw.SetMainControl(child);

            return mw;

        }
    }
}
