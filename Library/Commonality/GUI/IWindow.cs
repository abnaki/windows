using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Abnaki.Windows.GUI
{
    /// <summary>
    /// Supports general initialization
    /// </summary>
    public interface IWindow
    {
        /// <summary>
        /// Raised when panel/docking layout should be saved to file
        /// </summary>
        event Action<FileInfo> SavingLayout;

        /// <summary>
        /// Raised when panel/docking layout should be restored from file
        /// </summary>
        event Action<FileInfo> RestoringLayout;
    }
}
