using System;

namespace Abnaki.Windows.Software.Wpf.PreferredControls.Docking
{
    /// <summary>
    /// IDockSystem depending on AvalonDock
    /// </summary>
    public class AvalonDockSystem : Abnaki.Windows.GUI.IDockSystem
    {
        public AvalonDockSystem(Xceed.Wpf.AvalonDock.DockingManager dman, int ver)
        {
            this.DockSystem = dman;
            this.Version = ver;
        }

        public object DockSystem
        {
            get;
            private set;
        }

        public int Version
        {
            get;
            private set;
        }
    }
}
