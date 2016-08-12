using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using Abnaki.Windows.GUI;

namespace Abnaki.Windows.Software.Wpf.Menu
{
    /// <summary>
    /// Controller base class
    /// </summary>
    /// <typeparam name="Tenum">designed for related buttons
    /// </typeparam>
    public class ButtonBus<Tenum>
    {
        /// <summary>
        /// Button Click handlers would call this method, and Button CommandParameter
        /// is the string of an enum value
        public static void HandleButton(Object sender, RoutedEventArgs e)
        {
            Button bu = (Button)sender;
            //object x = bu.CommandParameter; // contains string

            var m = new ButtonMessage<Tenum>(Convert.ToString(bu.CommandParameter));

            MessageTube.Publish<ButtonMessage>(m);
        }

        public static void Handle(Tenum key, bool? isChecked)
        {
            var m = new ButtonMessage<Tenum>(key);
            m.Checked = isChecked;

            MessageTube.Publish<ButtonMessage>(m);
        }

        public static void HookupSubscriber(Action<ButtonMessage<Tenum>> act)
        {
            MessageTube.Subscribe<ButtonMessage, ButtonMessage<Tenum>>(act);
        }

        public ButtonBus()
        {
            HookupSubscriber(HandleButton);
        }

        protected virtual void HandleButton(ButtonMessage<Tenum> m)
        {
        }

        /// <summary>
        /// First command shall be checked; remainder false.
        /// </summary>
        public static void AddExclusiveCommands(IMainMenu menu, object parentKey, IEnumerable<Tenum> keys)
        {
            bool chk = true;
            foreach ( Tenum key in keys )
            {
                MenuSeed<Tenum> seed = new MenuSeed<Tenum>() { ParentKey = parentKey, Key = key, DefaultCheck = chk };
                seed.MutuallyExclusive = true;
                menu.AddCommand(seed);
                chk = false;
            }
        }
    }
}
