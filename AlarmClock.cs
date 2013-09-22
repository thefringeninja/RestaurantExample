using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ClassLibrary4
{
    public class AlarmClock : Handles<Message>
    {
        readonly SortedSet<Messages.Timeout<Message>> alarms = new SortedSet<Messages.Timeout<Message>>(AlarmClockComparer.Instance);
        readonly object alarmLock = new object();
        private volatile bool stopped;
        private readonly Thread workerThread;

        class AlarmClockComparer : IComparer<Messages.Timeout<Message>>
        {
            public static readonly IComparer<Messages.Timeout<Message>> Instance = new AlarmClockComparer();
 
            public int Compare(Messages.Timeout<Message> x, Messages.Timeout<Message> y)
            {
                return x.AlarmTime.CompareTo(y.AlarmTime);
            }
        }

        public AlarmClock(IPublishEvents bus)
        {
            workerThread = new Thread(
                _ =>
                {
                    while (!stopped)
                    {
                        lock (alarmLock)
                        {
                            var now = DateTime.UtcNow;
                            var shouldSend = alarms.Where(x => x.AlarmTime > now).ToList();
                            foreach (var alarm in shouldSend)
                            {
                                bus.Publish(alarm.Message);
                                alarms.Remove(alarm);
                            }
                        }
                        Thread.Sleep(1);
                    }
                });
        }

        public void Start()
        {
            workerThread.Start();
        }

        public void Stop()
        {
            stopped = true;
        }

        public void Handle(Message message)
        {
            var alarmTime = DateTime.UtcNow.AddSeconds(30);
            lock (alarmLock)
            {
                alarms.Add(new Messages.Timeout<Message>(message, alarmTime));
            }
        }
    }
}