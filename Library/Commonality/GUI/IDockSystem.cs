using System;
using System.IO;

namespace Abnaki.Windows.GUI
{
    /// <summary>
    /// Important related objects/values, in terms of panel/docking
    /// </summary>
    public interface IDockSystem
    {
        /// <summary>
        /// Single place for most useful default settings
        /// </summary>
        void Defaults();

        /// <summary>
        /// Create file
        /// </summary>
        /// <param name="fi"></param>
        void SerializeLayout(FileInfo fi);

        /// <summary>
        /// Read file
        /// </summary>
        /// <param name="fi"></param>
        void DeserializeLayout(FileInfo fi);

        /// <summary>
        /// Increases with development progress of panel layouts.
        /// Necessary for a new programmatic layout to have precedence over a conflicting old saved layout.
        /// </summary>
        /// <remarks>
        /// If there is an inheritance hierarchy of Control classes that contribute panels to one DockSystem, 
        /// the classes could each contribute an increment, for example 8, to a summation that will be passed to Version.  
        /// When the developer lets a particular class add/remove a panel, that class increases 8 to 9.
        /// </remarks>
        int Version { get; }
    }
}
