using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

using Xceed.Wpf.AvalonDock;
using Xceed.Wpf.AvalonDock.Layout;

namespace Abnaki.Windows.Software.Wpf.PreferredControls
{
    public static class Extensions
    {
        // will extend DockingManager publicly

        public static void StronglyRecommendedDefaults(this DockingManager dman)
        {
            
            foreach (ILayoutElement element in dman.Layout.Children )
            {
                SetStronglyRecommendedDefaults(element);
            }
        }

        static void SetStronglyRecommendedDefaults(ILayoutElement element)
        {
            //Debug.Write(element);

            LayoutContent content = element as LayoutContent;
            if (content == null)
            {
                //Debug.Write(" not");
            }
            else
            {
                SetLayoutContentDefaults(content);

                // subclasses of LayoutContent
                LayoutAnchorable anchorable = content as LayoutAnchorable;
                if (anchorable != null)
                    SetLayoutAnchorableDefaults(anchorable);
            }

            //Debug.WriteLine(" a LayoutContent");

            //Debug.Indent();
            foreach (ILayoutElement desc in element.Descendents() )
            {
                SetStronglyRecommendedDefaults(desc);
            }
            //Debug.Unindent();
        }

        static void SetLayoutContentDefaults(LayoutContent content)
        {
            content.CanClose = false; // if true, can dock as tabbed, then close, unable to get it back
        }

        static void SetLayoutAnchorableDefaults(LayoutAnchorable anch)
        {
            anch.CanHide = false;  // if true, you would need some way to restore it to the screen
        }
    }
}
