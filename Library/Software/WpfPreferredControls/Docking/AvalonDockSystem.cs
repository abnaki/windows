using Abnaki.Windows.GUI;
using System;
using System.IO;
using System.Xml;
using Xceed.Wpf.AvalonDock;
using Xceed.Wpf.AvalonDock.Layout;
using Xceed.Wpf.AvalonDock.Layout.Serialization;

namespace Abnaki.Windows.Software.Wpf.PreferredControls.Docking
{
    /// <summary>
    /// IDockSystem depending on AvalonDock
    /// </summary>
    public class AvalonDockSystem : AbnakiDockSystem
    {
        public AvalonDockSystem(Xceed.Wpf.AvalonDock.DockingManager dman, int ver)
        {
            this.DockSystem = dman;
            this.Version = ver;
        }

        Xceed.Wpf.AvalonDock.DockingManager DockSystem
        {
            get;
            set;
        }

        public override void Defaults()
        {
            base.Defaults();

            foreach (ILayoutElement element in this.DockSystem.Layout.Children)
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

        protected override void SerializeLayoutStream(MemoryStream ms)
        {
            XmlLayoutSerializer ser = NewXmlLayoutSerializer();
            ser.Serialize(ms);
        }

        protected override void DeserializeLayoutFile(FileInfo fi)
        {
            XmlLayoutSerializer serializer = NewXmlLayoutSerializer();
            serializer.Deserialize(fi.FullName);
        }

        XmlLayoutSerializer NewXmlLayoutSerializer()
        {
            return new XmlLayoutSerializer(this.DockSystem);
        }


    }
}
