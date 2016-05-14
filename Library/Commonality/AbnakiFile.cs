using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Abnaki.Windows
{
    /// <summary>
    /// Helps with System.IO
    /// </summary>
    /// <remarks>
    /// Encourages FileInfo and DirectoryInfo usage.
    /// In Combined methods where arguments are weakly typed path parts, they are allowed to be class or string, at your discretion;
    /// e.g. if given a FileInfo or DirectoryInfo, its FullName is used.
    /// </remarks>
    public static class AbnakiFile
    {
        public static FileInfo CombinedFilePath(IEnumerable parts)
        {
            return new FileInfo(CombinedPath(parts));
        }

        public static FileInfo CombinedFilePath(params object[] parts)
        {
            return new FileInfo(CombinedPath(parts));
        }

        public static DirectoryInfo CombinedDirectoryPath(IEnumerable parts)
        {
            return new DirectoryInfo(CombinedPath(parts));
        }

        public static DirectoryInfo CombinedDirectoryPath(params object[] parts)
        {
            return new DirectoryInfo(CombinedPath(parts));
        }

        static string CombinedPath(IEnumerable parts)
        {
            return parts.Cast<object>().Aggregate<object,string>("",
                (a, b) => Path.Combine(a, FromGeneralizedPath(b)));
        }

        static string FromGeneralizedPath(object path)
        {
            if (path is FileSystemInfo)
                return ((FileSystemInfo)path).FullName;
            else if (path is string)
                return Convert.ToString(path);

            throw new ArgumentException("Expected FileInfo or string but got " + path);
        }
    }
}
