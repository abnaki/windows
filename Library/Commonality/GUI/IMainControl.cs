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
        /// <remarks>
        /// The intent of object is to keep fundamental code free from many obscure freeware types, 
        /// avoiding cluttered references of projects.  The docking/paneling system is always by a third party.
        /// </remarks>
        object DockingSystem { get; }
    }
}
