using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Abnaki.Windows.Software.Wpf.Diag
{
    /// <summary>
    /// Investigate design of WPF controls
    /// </summary>
    public class Design
    {
        public static void DebugAncestry(DependencyObject x)
        {
            Debug.WriteLine("Ancestry:");
            SubDebugAncestry(x);
            Debug.IndentLevel = 0;
        }

        static void SubDebugAncestry(DependencyObject x)
        {
            if (x == null)
                return;

            DependencyObject parent = LogicalTreeHelper.GetParent(x);
            SubDebugAncestry(parent);

            Debug.Indent();
            Debug.WriteLine(x);
        }

        // no effect
        static void GetDescendants(DependencyObject x)
        {
            var children = LogicalTreeHelper.GetChildren(x).OfType<DependencyObject>();
        }
    }
}
