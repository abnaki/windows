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
    }
}
