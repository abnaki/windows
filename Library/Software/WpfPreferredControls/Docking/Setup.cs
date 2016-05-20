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
            if (ds.DockSystem is DockingManager)
            {
                StronglyRecommendedDefaults((DockingManager)ds.DockSystem);

                mainWindow.SavingPanelLayout += fi => SerializeLayout(ds, fi);
                mainWindow.RestoringPanelLayout += fi => DeserializeLayout(ds, fi);
            }
            else
            {
                Log.Comment("DockSystem is not " + typeof(DockingManager).FullName, ds.DockSystem);
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

        public static void StronglyRecommendedDefaults(DockingManager dman)
        {
            foreach (ILayoutElement element in dman.Layout.Children)
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
            foreach (ILayoutElement desc in element.Descendents())
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

        static void SerializeLayout(IDockSystem ds, FileInfo fi)
        {
            var serializer = NewXmlLayoutSerializer(ds);
            
            //serializer.Serialize(fi.FullName);
            // rather, serz. to memory stream and add Version

            using ( MemoryStream ms = new MemoryStream() )
            {
                serializer.Serialize(ms);
                ms.Seek(0, SeekOrigin.Begin);

                XmlDocument xdoc = new XmlDocument();
                xdoc.Load(ms);
                XmlAttribute va = xdoc.CreateAttribute(LayoutVersionAttr);
                va.Value = XmlConvert.ToString(ds.Version);
                xdoc.DocumentElement.Attributes.Append(va);

                using (XmlWriter xw = XmlWriter.Create(fi.FullName, new XmlWriterSettings() { Indent = true }))
                {
                    xdoc.WriteTo(xw);
                }
            }
        }

        const string LayoutVersionAttr = "PanelVersion";

        static void DeserializeLayout(IDockSystem ds, FileInfo fi)
        {
            if (fi.Exists)
            {
                using (XmlReader xr = XmlReader.Create(fi.FullName))
                {
                    XmlDocument xdoc = new XmlDocument();
                    xdoc.Load(xr);
                    // compare  ds.Version
                    XmlNode xtop = xdoc.DocumentElement;
                    XmlAttribute va = xtop.Attributes[LayoutVersionAttr];
                    if ( va != null )
                    {
                        int oldver;
                        if ( int.TryParse(va.Value, out oldver))
                        {
                            if ( oldver == ds.Version )
                            {
                                var serializer = NewXmlLayoutSerializer(ds);
                                serializer.Deserialize(fi.FullName);
                            }
                            // else mismatch
                        }
                    }

                }
            }
        }

        static XmlLayoutSerializer NewXmlLayoutSerializer(IDockSystem ds)
        {
            DockingManager dman = (DockingManager)ds.DockSystem;
            return new XmlLayoutSerializer(dman);
        }

    }
}
