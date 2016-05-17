using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Diagnostics;

using Xceed.Wpf.AvalonDock;
using Xceed.Wpf.AvalonDock.Layout;
using Xceed.Wpf.AvalonDock.Layout.Serialization;

namespace Abnaki.Windows.Software.Wpf.PreferredControls
{
    public static class Extensions
    {
        // will extend DockingManager publicly

        static void CompleteSetup(this DockingManager dman, Abnaki.Windows.GUI.IWindow mainWindow)
        {
            StronglyRecommendedDefaults(dman);

            mainWindow.SavingPanelLayout += fi => SerializeLayout(dman, fi);
            mainWindow.RestoringPanelLayout += fi => DeserializeLayout(dman, fi);

        }

        /// <summary>
        /// </summary>
        /// <param name="mainWindow"></param>
        /// <param name="dockingSystem"></param>
        /// <remarks>Do not want to pollute all dependent projects with references to Xceed and make replacement difficult.
        /// </remarks>
        public static void CompleteSetupOfInterfaces(Abnaki.Windows.GUI.IWindow mainWindow, object dockingSystem)
        {
            DockingManager dman = (DockingManager)dockingSystem;
            CompleteSetup(dman, mainWindow);
        }

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

        static System.Text.Encoding LayoutEncoding = System.Text.Encoding.UTF8;

        public static void SerializeLayout(this DockingManager dman, FileInfo fi)
        {
            var serializer = new XmlLayoutSerializer(dman);
            serializer.Serialize(fi.FullName);
        }

        public static void DeserializeLayout(this DockingManager dman, FileInfo fi)
        {
            if (fi.Exists)
            {
                var serializer = new XmlLayoutSerializer(dman);
                serializer.Deserialize(fi.FullName);
            }
        }

    }
}
