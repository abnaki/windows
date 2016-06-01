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
        /// After this becomes a descendant of main window
        /// </summary>
        void EmplacedInWindow();

        /// <summary>
        /// Manages panels.  Can be null.
        /// </summary>
        IDockSystem DockingSystem { get; }

        void ConfigureMenu(IMainMenu menu);

        /// <summary>
        /// Change main window Title
        /// </summary>
        event Action<string> MainTitle;
    }
}
