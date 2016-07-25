using System;
using System.Collections.Generic;
using System.Linq;

namespace Abnaki.Windows.GUI
{
    public interface IMainMenu
    {
        void AddCommand<Tkey>(MenuSeed<Tkey> seed);

        void AddCommand<Tkey>(Tkey key, string label, bool? defaultCheck = null);

        void AddCommandChild<Tkey>(object parentKey, Tkey childKey, string label, bool? defaultCheck = null);

        void AddExclusiveCommands<TKey>(object parentKey, IEnumerable<MenuSeed<TKey>> seeds);

        /// <summary>
        /// All Commands added.  May restore saved Check property to commands.
        /// </summary>
        void Completed();
    }
}
