using System;
using System.IO;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Abnaki.Windows;

namespace CommonTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestRelativePath()
        {
            DirectoryInfo diCurrent = new DirectoryInfo(Environment.CurrentDirectory);
            DirectoryInfo diGrandparent = diCurrent.Parent.Parent;

            string rdir = AbnakiFile.RelativePath(diCurrent, diGrandparent);

            Debug.WriteLine(rdir); // should be subdir such as bin/Debug

            rdir = AbnakiFile.RelativePath(diGrandparent, diCurrent);

            Debug.WriteLine(rdir); // should be ..\..

        }

        [TestMethod]
        public void TestEntryOfEntries()
        {
            string f1 = "log1.xml";

            AbnakiLog.Comment("Vote Labour");
            AbnakiLog.Comment("Vote Tory");
            AbnakiLog.Write(f1);

            AbnakiLog.Comment("Should be followed by EntryOfEntries");
            AbnakiLog.Incorporate(f1);

            string f2 = "log2.xml";
            AbnakiLog.Write(f2);

            Debug.WriteLine("Wrote logs in " + Environment.CurrentDirectory);
        }
    }
}
