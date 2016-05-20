using System;
using System.Collections.Generic;

namespace Abnaki.Windows.GUI
{
    /// <summary>
    /// Describes a menu command to create
    /// </summary>
    /// <typeparam name="Tkey"></typeparam>
    public class MenuSeed<Tkey>
    {
        public MenuSeed()
        {

        }

        public MenuSeed(Tkey key, string label, bool? defaultCheck = null, string toolTip = null)
        {
            this.Key = key;
            this.Label = label;
            this.DefaultCheck = defaultCheck;
            this.Tooltip = toolTip;
        }

        public Tkey Key { get; set; }

        /// <summary>
        /// Can be null
        /// </summary>
        public object ParentKey { get; set; }

        public string Label { get; set; }

        /// <summary>
        /// Null by default means no check exists.
        /// </summary>
        public bool? DefaultCheck { get; set; }

        public string Tooltip { get; set; }

        public bool? Enabled { get; set; }
    }
}
