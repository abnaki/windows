using System;
using System.Collections.Generic;

namespace Abnaki.Windows
{
    /// <summary>
    /// Labels and documents types, such as enum values
    /// </summary>
    public class LabelAttribute : Attribute
    {
        public LabelAttribute(string label)
        {
            this.Label = label;
        }

        /// <summary>
        /// Shown on a control, button, and such things
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// Exhaustive explanation
        /// </summary>
        public string Detail { get; set; }
    }
}
