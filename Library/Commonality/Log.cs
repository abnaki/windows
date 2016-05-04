using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Diagnostics;

namespace Abnaki.Windows
{
    /// <summary>
    /// Logs application information and things that happen.
    /// </summary>
    /// <remarks>
    /// Log is required to result in data that developers can view appropriately, 
    /// with the fields they need.
    /// 
    /// Designed to log Entry, and subclasses, in memory and will be possible to save in a file later.
    /// Does not aspire to be like feature-rich logging frameworks such as NLog.
    /// Not designed to write to a file/DB/service continuously.
    /// Not designed for configurable "levels of detail".
    /// Recommended usage is coarse information, not frequent programmatic log entries. 
    /// Too many entries would slow down a sophisticated application, on the main thread or a 
    /// logging thread, and it could collect astronomical amounts of data (...big files) that aren't very portable
    /// and aren't feasible to diagnose.  Instead, when the utmost detail is needed, debug.
    /// </remarks>
    public class Log
    {
        static List<Entry> entries = new List<Entry>();

        public static void Comment(string comment, object obj = null)
        {
            EntryObj e = new EntryObj() { Comment = comment, Object = Convert.ToString(obj) };
            AddEntry(e);
        }

        public static void Exception(Exception ex, string comment = null)
        {
            Debug.WriteLine(ex);

            EntryException e = new EntryException(ex, comment);
            AddEntry(e);
        }

        static void AddEntry(Entry e)
        {
            e.Number = entries.Count;
            entries.Add(e);
        }

        public static void Write(string filename)
        {
            // maybe promote generalized writing code
            using (FileStream fs = File.Open(filename, FileMode.Create, FileAccess.Write))
            {
                XmlWriterSettings xset = new XmlWriterSettings();
                xset.Indent = true;

                using (XmlWriter writer = XmlWriter.Create(fs, xset))
                {
                    // important
                    Type[] subtypes = new Type[] { 
                            typeof(Entry), 
                            typeof(EntryException), 
                            typeof(EntryObj) };

                    XmlSerializer srlz = new XmlSerializer(entries.GetType(), subtypes);
                    srlz.Serialize(writer, entries);

                    writer.Close();
                }
            }
        }

        public class Entry
        {
            public Entry()
            {
                Timeu = DateTime.UtcNow;
            }

            public int Number { get; set; }

            /// <summary>Universal time
            /// </summary>
            /// <remarks>Machine timezone can be doubtful, and should be eliminated from comparisons across different clocks.
            /// </remarks>
            public DateTime Timeu { get; set; }

            public string Comment { get; set; }

            /// <summary>
            /// May categorize by class, project, etc.
            /// </summary>
            public string Scope { get; set; }

        }

        public class EntryObj : Entry
        {
            public string Object { get; set; }

        }

        public class EntryException : Entry
        {
            public EntryException(Exception ex, string comment = null)
            {
                this.ExceptionMessage = ex.Message;
                this.ExceptionStack = ex.StackTrace;
                this.Comment = comment;
            }

            public EntryException() // serializ.
            {

            }

            public string ExceptionMessage { get; set; }

            // will be parsed into more sophisticated classes, System.Diagnostics.StackTrace
            
            public string ExceptionStack { get; set; }
        }
    }
}
