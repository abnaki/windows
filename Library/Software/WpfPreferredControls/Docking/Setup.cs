using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Diagnostics;
using System.Xml;

using Xceed.Wpf.AvalonDock;
using Xceed.Wpf.AvalonDock.Layout;
using Xceed.Wpf.AvalonDock.Layout.Serialization;

using Abnaki.Windows.GUI;

namespace Abnaki.Windows.Software.Wpf.PreferredControls.Docking
{
    public class Setup
    {
        static void CompleteSetup(Abnaki.Windows.GUI.IWindow mainWindow, IDockSystem ds)
        {
            if (ds == null)
            {
                AbnakiLog.Comment("IDockSystem null");
            }
            else
            {
                ds.Defaults();

                mainWindow.SavingPanelLayout += fi => ds.SerializeLayout(fi);
                mainWindow.RestoringPanelLayout += fi => ds.DeserializeLayout(fi);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="mainWindow"></param>
        /// <param name="dockingSystem"></param>
        /// <remarks>Do not want to pollute all dependent projects with references to Xceed and make replacement difficult.
        /// </remarks>
        public static void CompleteSetupOfInterfaces(Abnaki.Windows.GUI.IWindow mainWindow, IDockSystem dockingSystem)
        {
            CompleteSetup(mainWindow, dockingSystem);
        }

    }
}
