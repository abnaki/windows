using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Abnaki.Windows.Thread
{
    /// <summary>
    /// Raises Expire event once
    /// </summary>
    /// <typeparam name="T">of payload
    /// </typeparam>
    public class PayloadTimer<T> : IDisposable
    {
        public PayloadTimer(T payload, TimeSpan due)
        {
            //this.Payload = payload;
            timer = new Timer(new TimerCallback(Callback), payload, (int)due.TotalMilliseconds, Timeout.Infinite);
        }

        //public T Payload { get; private set; }

        public event Action<T> Expire;

        System.Threading.Timer timer;

        void Callback(object payload)
        {
            var h = Expire;
            if (h != null)
                h((T)payload);
        }

        public void Dispose()
        {
            if (timer != null)
                timer.Dispose();

            timer = null;
        }
    }
}
