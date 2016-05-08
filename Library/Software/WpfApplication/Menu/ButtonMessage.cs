using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace Abnaki.Windows.Software.Wpf.Menu
{
    /// <summary>
    /// Supports messaging about buttons/commands clicked
    /// </summary>
    public class ButtonMessage
    {
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="Tenum">contains keys of related buttons
    /// </typeparam>
    public class ButtonMessage<Tenum> : ButtonMessage
    {
        public ButtonMessage(Tenum key)
        {
            this.Key = key;
        }

        public ButtonMessage(string s)
        {
            this.Key = (Tenum)Enum.Parse(typeof(Tenum), s, ignoreCase: true);
        }

        public Tenum Key { get; private set; }

        public override string ToString()
        {
            return base.ToString() + " " + Key;
        }
    }

}
