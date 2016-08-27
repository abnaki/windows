using System;
using System.Collections.Generic;
using System.Linq;

namespace Abnaki.Windows.Software.Wpf.PreferredControls.Grid
{
    class Decoration
    {
        public readonly Dictionary<string, string> MapColumnTooltip = new Dictionary<string, string>();

        public string GetColumnTooltip(string fieldName)
        {
            if (MapColumnTooltip.ContainsKey(fieldName))
                return MapColumnTooltip[fieldName];

            return null;
        }

        public void Clear()
        {
            MapColumnTooltip.Clear();
        }
    }
}
