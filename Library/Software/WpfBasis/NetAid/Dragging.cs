using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Abnaki.Windows.Software.Wpf.NetAid
{
    /// <summary>
    /// Assists with Drag/Drop
    /// </summary>
    public class Dragging
    {
        /// <summary>
        /// Data that was passed to System.Windows.DragDrop.DoDragDrop()
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="e"></param>
        public static T Extract<T>(DragEventArgs e) where T : class
        {
            return e.Data.GetData(typeof(T)) as T;
        }
    }
}
