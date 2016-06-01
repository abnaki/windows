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
            ps.Subscribe(act, keepSubscriberReferenceAlive: true);

        }

        /// <summary>
        /// Subscribe to subtype of published event
        /// </summary>
        /// <typeparam name="Tsuper">was associated with Publish</typeparam>
        /// <typeparam name="Tsub">desired subtype</typeparam>
        /// <param name="act"></param>
        /// <remarks>
        /// Conversely, if Tsub rather than Tsuper was associated with Publish, 
        /// to pass a supertype action it is possible to use the other overload,
        /// i.e. contravariance.
        /// <code>
        /// Action<Tsuper> weakact;
        /// Subscribe<Tsub>(weakact as Action<Tsub>);
        /// </code>
        /// </remarks>
        public static void Subscribe<Tsuper,Tsub>(Action<Tsub> act)
            where Tsub : Tsuper
        {
            // Subscribe<Tsuper>(Demo<Tsuper>);

            Subscribe<Tsuper>(super =>
            {
                if (super is Tsub)
                    act((Tsub)super);
            });
        }

        static void Demo<T>(T x)
        {
            object z = x;
        }

        static IEventAggregator agg = new EventAggregator();
    }
}

