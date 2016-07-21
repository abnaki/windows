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

            Mouse.OverrideCursor = Cursors.Wait;
        }

        #region IDisposable Members

        public void Dispose()
        {
            Mouse.OverrideCursor = _previousCursor;
        }

        #endregion
    }
}
