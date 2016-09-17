using System;
using System.Windows.Input;

namespace Abnaki.Windows.Software.Wpf
{
    /// <summary>
    /// Common pattern to reversibly toggle mouse to WaitCursor
    /// </summary>
    /// <example>
    /// <code>
    /// using ( new WaitCursor() ) {  /* consume time /*  }
    /// </code>
    /// </example>
    public class WaitCursor : IDisposable
    {
        private Cursor _previousCursor;

        /// <summary>
        /// Hybrid of pointer and hourglass, while background processing happens
        /// </summary>
        /// <param name="enable"></param>
        public static WaitCursor InProgress(bool enable = true)
        {
            return new WaitCursor(Cursors.AppStarting, enable);
        }

        /// <summary>
        /// </summary>
        /// <param name="cursor">default null implies most common houglass</param>
        /// <param name="enable"></param>
        public WaitCursor(Cursor cursor = null, bool enable = true)
        {
            Enabled = enable;

            if (Enabled)
            {
                _previousCursor = Mouse.OverrideCursor;

                Mouse.OverrideCursor = cursor ?? Cursors.Wait;
            }
        }



        bool Enabled { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            if (Enabled)
                Mouse.OverrideCursor = _previousCursor;
        }

        #endregion

        public static void Wait()
        {
            Mouse.OverrideCursor = Cursors.Wait;
        }

        public static void Default()
        {
            Mouse.OverrideCursor = Cursors.Arrow;
        }

        public static void Progressing()
        {
            Mouse.OverrideCursor = Cursors.AppStarting;
        }
    }
}
