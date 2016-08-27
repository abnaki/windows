using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace Abnaki.Windows.Xml
{
    /// <summary>
    /// URI that can be serialized
    /// </summary>
    public class Yuri : IXmlSerializable
    {
        public Yuri() // for serialization
        {

        }

        public Yuri(string s)
        {
            this.Uri = new Uri(s);
        }

        public Uri Uri { get; set; }

        System.Xml.Schema.XmlSchema IXmlSerializable.GetSchema()
        {
            return null;
        }

        void IXmlSerializable.ReadXml(System.Xml.XmlReader reader)
        {
            this.Uri = new Uri(reader.ReadElementContentAsString());
        }

        void IXmlSerializable.WriteXml(System.Xml.XmlWriter writer)
        {
            writer.WriteString(this.Uri.ToString());
        }

        public override string ToString()
        {
            return this.Uri.ToString();
        }
    }
}
