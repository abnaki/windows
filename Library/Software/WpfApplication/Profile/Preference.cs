using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Reflection;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Abnaki.Windows.Software.Wpf.Profile
{
    /// <summary>
    /// Manages storage of preferences.
    /// Will assume that any type can have one instance of some class containing preferences,
    /// and the type will have logic responsible for timely saving/restoring, by calling methods below.
    /// </summary>
    public static class Preference
    {
        public const string FileExtWithoutDot = "xml";

        public static DirectoryInfo PrefsDir()
        {
            DirectoryInfo dipref = AbnakiFile.CombinedDirectoryPath(ApplicationProfileDir(), "prefs");
            dipref.Create();
            return dipref;
        }

        public static DirectoryInfo ApplicationProfileDir()
        {
            string appDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData, Environment.SpecialFolderOption.Create);

            Assembly appa = Assembly.GetEntryAssembly();
            //AssemblyName appNameInfo = appa.GetName();
            string company = appa.GetCustomAttribute<AssemblyCompanyAttribute>().Company;
            string title = appa.GetCustomAttribute<AssemblyTitleAttribute>().Title;

            DirectoryInfo subdir = AbnakiFile.CombinedDirectoryPath(appDir, company, title);
            subdir.Create();

            return subdir;
        }

        public static DirectoryInfo ApplicationDefaultDir()
        {
            Assembly appa = Assembly.GetEntryAssembly();

            return new DirectoryInfo(Path.GetDirectoryName(appa.Location));
        }

        static StringBuilder Test()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(ApplicationProfileDir().FullName);
            return sb;
        }

        /// <summary>
        /// In a profile subdirectory
        /// </summary>
        /// <typeparam name="T">type of most-responsible consumer class</typeparam>
        /// <param name="qualifier">can distinguish or clarify a special purpose file; be sure it's valid within filenames</param>
        /// <returns></returns>
        public static FileInfo ClassPrefsFile<T>(string qualifier = null)
        {
            string filename = ClassPrefsFileOnly<T>(qualifier);

            return AbnakiFile.CombinedFilePath(PrefsDir(), filename);
        }

        /// <summary>
        /// Filename only.
        /// Like ClassPrefsFile() minus directory.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="qualifier"></param>
        /// <returns></returns>
        public static string ClassPrefsFileOnly<T>(string qualifier = null)
        {
            string filename = typeof(T).FullName;
            if (qualifier != null)
                filename += "." + qualifier;

            filename += "." + FileExtWithoutDot;
            return filename;
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="Tclass"></typeparam>
        /// <typeparam name="Tpref">contains preferences.  Must be public.</typeparam>
        public static void WriteClassPrefs<Tclass, Tpref>(Tpref pref)
        {
            FileInfo fi = ClassPrefsFile<Tclass>();
            using ( StreamWriter fs = new StreamWriter(fi.FullName, append: false, encoding: PrefEncoding) )
            {
                XmlSerializer xs = new XmlSerializer(typeof(Tpref));
                xs.Serialize(fs, pref);
            }
        }

        public static Tpref ReadClassPrefs<Tclass, Tpref>()
            where Tpref : class
        {
            FileInfo fi = ClassPrefsFile<Tclass>();
            if (false == fi.Exists)
                return null;

            using (StreamReader sr = new StreamReader(fi.FullName, PrefEncoding))
            {
                XmlSerializer xs = new XmlSerializer(typeof(Tpref));
                return (Tpref)xs.Deserialize(sr);
            }
        }

        static System.Text.Encoding PrefEncoding = System.Text.Encoding.UTF8;
    }
}
