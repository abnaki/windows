using System;
using System.Collections.Generic;

namespace Abnaki.Windows.GUI
{
    /// <summary>
    /// Supports general initialization.
    /// Expected to be implemented widely.
    /// </summary>
    public interface IMainControl
    {
        /// <summary>
        /// Manages panels.  Can be null.
        /// </summary>
        IDockSystem DockingSystem { get; }

        void ConfigureMenu(IMainMenu menu);
    }
}
