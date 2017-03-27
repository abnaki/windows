using Abnaki.Windows.GUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace Abnaki.Windows.Software.Wpf.PreferredControls.Docking
{
    /// <summary>
    /// Commonalities often needed by different panel docking systems.
    /// Layout files can be versioned, and ignored if their version is outdated.
    /// </summary>
    public abstract class AbnakiDockSystem : IDockSystem
    {
        public virtual void Defaults()
        {
            
        }

        public int Version
        {
            get;
            set;
        }

        protected abstract void SerializeLayoutStream(MemoryStream ms);

        protected abstract void DeserializeLayoutFile(FileInfo fi);

        public void SerializeLayout(System.IO.FileInfo fi)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                SerializeLayoutStream(ms);
                ms.Seek(0, SeekOrigin.Begin);

                XmlDocument xdoc = new XmlDocument();
                xdoc.Load(ms);
                XmlAttribute va = xdoc.CreateAttribute(LayoutVersionAttr);
                va.Value = XmlConvert.ToString(this.Version);
                xdoc.DocumentElement.Attributes.Append(va);

                using (XmlWriter xw = XmlWriter.Create(fi.FullName, new XmlWriterSettings() { Indent = true }))
                {
                    xdoc.WriteTo(xw);
                }
            }
        }

        const string LayoutVersionAttr = "PanelVersion";

        public void DeserializeLayout(System.IO.FileInfo fi)
        {
            if (fi.Exists)
            {
                using (XmlReader xr = XmlReader.Create(fi.FullName))
                {
                    XmlDocument xdoc = new XmlDocument();
                    xdoc.Load(xr);
                    // compare Version
                    XmlNode xtop = xdoc.DocumentElement;
                    XmlAttribute va = xtop.Attributes[LayoutVersionAttr];
                    if (va != null)
                    {
                        int oldver;
                        if (int.TryParse(va.Value, out oldver))
                        {
                            if (oldver == this.Version)
                            {
                                DeserializeLayoutFile(fi);
                            }
                            // else mismatch, so invalid.
                        }
                    }

                }
            }
        }

    }
}
