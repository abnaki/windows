using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Abnaki.Windows.Software.Wpf.NetAid
{
    // namespace to aid in using .NET framework

    public static class DialogExtension
    {
        /// <summary>
        /// Suitably simple way to set FileDialog.Filter.
        /// DefaultExt is the first.
        /// </summary>
        /// <param name="dialog"></param>
        /// <param name="extensions">filename extensions without dots
        /// </param>
        public static void SetFilters(this FileDialog dialog, params object[] extensions)
        {
            dialog.Filter = string.Join(",",
                extensions.Select(x => x.ToString().ToUpper() + "|." + x).ToArray());

            dialog.DefaultExt = extensions.First().ToString(); // they ignore leading dot
        }

        /// <summary>
        /// Less ugly than enum in your code.
        /// </summary>
        /// <param name="dialog"></param>
        /// <returns>
        /// true if OK
        /// </returns>
        public static bool ShowDialogAndOK(this FileDialog dialog)
        {
            return dialog.ShowDialog() == DialogResult.OK;
        }
    }
}
