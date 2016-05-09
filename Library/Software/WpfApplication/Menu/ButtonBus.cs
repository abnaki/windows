﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace Abnaki.Windows.Software.Wpf.Menu
{
    /// <summary>
    /// Controller base class
    /// </summary>
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


        public ButtonBus()
        {
            MessageTube.Subscribe<ButtonMessage>(HandleButton);
        }

        protected virtual void HandleButton(ButtonMessage m)
        {
        }
    }
}