using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Events;

namespace Abnaki.Windows.Software.Wpf
{
    /// <summary>
    /// A message bus
    /// </summary>
    public static class MessageTube
    {
        public static void Publish<T>(T x)
        {
            var ps = agg.GetEvent<PubSubEvent<T>>();
            ps.Publish(x);
        }

        public static void Subscribe<T>(Action<T> act)
        {
            var ps = agg.GetEvent<PubSubEvent<T>>();
            ps.Subscribe(act);

        }

        static IEventAggregator agg = new EventAggregator();
    }
}

