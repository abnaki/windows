using System;
using System.Collections.Generic;

namespace Abnaki.Windows.GUI
{
    /// <summary>
    /// Supports general initialization
    /// </summary>
    public interface IMainControl
    {
        /// <summary>
        /// Manages panels
        /// </summary>
        IDockSystem DockingSystem { get; }
    }
}
