﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Abnaki.Windows.GUI
{
    /// <summary>
    /// Supports general initialization.
    /// Not expected to be implemented widely by consumers.
    /// </summary>
    public interface IWindow
    {
        /// <summary>
        /// Raised when panel/docking layout should be saved to file
        /// </summary>
        event Action<FileInfo> SavingPanelLayout;

        /// <summary>
        /// Raised when panel/docking layout should be restored from file
        /// </summary>
        event Action<FileInfo> RestoringPanelLayout;
    }
}
