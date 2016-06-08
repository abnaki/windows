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

        static string CombinedPath(IEnumerable parts, bool allowRooting=true)
        {
            return parts.Cast<object>().Aggregate<object,string>("",
                (a, b) => Path.Combine(a, FromGeneralizedPath(b, allowRooting && string.IsNullOrEmpty(a))));
        }

        static string FromGeneralizedPath(object path, bool shouldBeRooted)
        {
            if (path is FileSystemInfo)
            {
                if (shouldBeRooted)
                    return ((FileSystemInfo)path).FullName;
                else
                    return ((FileSystemInfo)path).Name;
            }
            else if (path is string)
            {
                return Convert.ToString(path);
            }

            throw new ArgumentException("Expected FileInfo or string but got " + path);
        }

        public static string RelativePath(FileSystemInfo fi, DirectoryInfo diRelated)
        {
            DirectoryInfo dibase = FindCommonDirectory(fi, diRelated);
            if (dibase == null)
                throw new ArgumentException("No common directory of " + fi.FullName + " and " + diRelated.FullName);

            List<FileSystemInfo> paths = new List<FileSystemInfo>();

            List<string> pathParts = new List<string>();
            FindPathDifference(dibase, diRelated, paths); // may be nothing
            string[] doubledots = Enumerable.Range(0, paths.Count).Select(i => "..").ToArray();
            pathParts.Add(string.Join(Path.DirectorySeparatorChar.ToString(), doubledots));

            paths.Clear();
            FindPathDifference(dibase, fi, paths);
            pathParts.Add(CombinedPath(paths, allowRooting: false));

            if ( pathParts.Any(p => ! string.IsNullOrEmpty(p)) )
                return CombinedPath(pathParts, allowRooting: false);

            throw new ApplicationException("Failed in RelativePath of " + fi.FullName + " relative to " + diRelated.FullName);
        }

        static DirectoryInfo FindCommonDirectory(FileSystemInfo fi, DirectoryInfo di)
        {
            List<FileSystemInfo> fpaths = new List<FileSystemInfo>();
            List<FileSystemInfo> dpaths = new List<FileSystemInfo>();
            FindPathDifference(null, fi, fpaths);
            FindPathDifference(null, di, dpaths);

            DirectoryInfo dicommon = null;

            for ( int i = 0; i < fpaths.Count && i < dpaths.Count ; i++ )
            {
                if (fpaths[i].FullName != dpaths[i].FullName)
                    break;

                dicommon = (DirectoryInfo)dpaths[i];
            }

            return dicommon;
        }

        /// <summary></summary>
        /// <param name="dibase">null implies root</param>
        /// <param name="fidescendant"></param>
        /// <param name="paths"></param>
        static void FindPathDifference(DirectoryInfo dibase, FileSystemInfo fidescendant, List<FileSystemInfo> paths)
        {
            if ( fidescendant == null )
            {

            }
            else if ( dibase != null &&  fidescendant.FullName == dibase.FullName )
            {

            }
            else
            {
                FileSystemInfo parent = GetParent(fidescendant);
                FindPathDifference(dibase, parent, paths);

                paths.Add(fidescendant);
            }
        }

        static FileSystemInfo GetParent(FileSystemInfo fi)
        {
            if (fi is DirectoryInfo)
                return ((DirectoryInfo)fi).Parent;
            else if ( fi is FileInfo )
                return ((FileInfo)fi).Directory;
            else
                throw new ArgumentException("GetParent cannot take " + fi);
        }
    }
}
