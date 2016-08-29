using System;
using System.Collections.Generic;
using System.Linq;
using Xceed.Wpf.AvalonDock;
using Xceed.Wpf.AvalonDock.Layout;

namespace Abnaki.Windows.Software.Wpf.PreferredControls.Docking
{
    public class Paneling
    {
        public static void ShowPanel(System.Windows.FrameworkElement control)
        {
            DockingManager dman = control.Parent as DockingManager;

            if (dman != null)
            {
                LayoutAnchorable panel = dman.Layout.Descendents().OfType<LayoutAnchorable>()
                    .FirstOrDefault<LayoutAnchorable>(lay => lay.Content == control);

                if (panel != null && panel.IsAutoHidden )
                {
                    panel.ToggleAutoHide();
                }
            }

        }
    }
}
