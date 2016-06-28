using System;
using System.Linq;
using System.Collections.Generic;

namespace Abnaki.Windows.Software.Wpf.Ultimate
{
    /// <summary>
    /// Starts Ultimate MainWindow where a Tcontrol occupies all the useful space
    /// </summary>
    /// <typeparam name="Tcontrol">
    /// </typeparam>
    public class UltimateStarter<Tcontrol>
        where Tcontrol : System.Windows.UIElement, Abnaki.Windows.GUI.IMainControl, new()
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
            
            // Icon:  alphabetically first embedded resource .ico 
            System.Reflection.Assembly a = child.GetType().Assembly;
            string icname = a.GetManifestResourceNames().OrderBy(n => n).FirstOrDefault(n => n.EndsWith(".ico"));
            if (icname != null)
                mw.Icon = System.Windows.Media.Imaging.BitmapFrame.Create(a.GetManifestResourceStream(icname));

            return mw;

        }
    }
}
