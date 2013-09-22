using System;
using System.Collections.Concurrent;
using System.Threading;

namespace ClassLibrary4
{
    public class QueuedHandler<T> : Handles<T> where T : Message
    {
        private readonly Handles<T> handler;
        private readonly ConcurrentQueue<T> queue;
        private readonly Thread workerThread;
        private volatile bool stop;

        public int Count
        {
            get { return queue.Count; }
        }

        private void HandleNextMessage(object _)
        {
            while (false == stop)
            {
                T message;
                if (queue.TryDequeue(out message))
                {
                    handler.Handle(message);
                    continue;
                }
                Thread.Sleep(1);
            }
        }

        public QueuedHandler(Handles<T> handler)
        {
            this.handler = handler;
            queue = new ConcurrentQueue<T>();
            workerThread = new Thread(HandleNextMessage);
        }

        public void Handle(T message)
        {
            queue.Enqueue(message);
        }

        public void Start()
        {
            workerThread.Start();
        }

        public void Stop()
        {
            stop = true;
        }

        public override string ToString()
        {
            return handler + " has " + Count + " items left";
        }
    }
}