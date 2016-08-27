using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace Abnaki.Windows.Xml
{
    public class AbnakiXml
    {
        public static void Write(string filename, object value, Type[] subtypes)
        {
            using (FileStream fs = File.Open(filename, FileMode.Create, FileAccess.Write))
            {
                XmlWriterSettings xset = new XmlWriterSettings();
                xset.Indent = true;

                using (XmlWriter writer = XmlWriter.Create(fs, xset))
                {
                    XmlSerializer srlz = new XmlSerializer(value.GetType(), subtypes);
                    srlz.Serialize(writer, value);

                    writer.Close();
                }
            }
        }

        public static T Read<T>(FileInfo fi, Type[] subtypes)
        {
            if (false == fi.Exists)
                throw new FileNotFoundException("Cannot read " + fi.FullName);

            using (FileStream fs = File.Open(fi.FullName, FileMode.Open, FileAccess.Read) )
            {
                using (XmlReader reader = XmlReader.Create(fs) )
                {
                    XmlSerializer srlz = new XmlSerializer(typeof(T), subtypes);

                    T tret = (T)srlz.Deserialize(reader);
                    reader.Close();

                    return tret;
                }
            }
        }
    }
}
