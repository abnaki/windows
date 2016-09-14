using System;
using System.Collections.Generic;
using System.Linq;

namespace Abnaki.Windows
{
    /// <summary>
    /// Supports IComparables
    /// </summary>
    public static class Compare
    {
        /// <summary>
        /// Uses thing.CompareTo(other), defines null as less than non-null, and null equal to null.
        /// </summary>
        /// <typeparam name="T">IComparable of T</typeparam>
        /// <param name="thing"></param>
        /// <param name="other"></param>
        public static int CompareNullPossible<T>(T thing, T other)
            where T : IComparable<T>
        {
            if (thing == null)
            {
                return other == null ? 0 : -1;
            }
            else if ( other == null )
            {
                return 1;
            }

            return thing.CompareTo(other);
        }
    }
}
