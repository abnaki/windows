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

        public WaitCursor()
        {
            _previousCursor = Mouse.OverrideCursor;

            Wait();
        }

        #region IDisposable Members

        public void Dispose()
        {
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
