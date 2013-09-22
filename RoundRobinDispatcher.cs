using System;
using System.Collections.Generic;

namespace ClassLibrary4
{
    public class RoundRobinDispatcher<T> : Handles<T> where T : Message
    {
        private readonly Queue<Handles<T>> dispatcherQueue = new Queue<Handles<T>>();

        public RoundRobinDispatcher(params Handles<T>[] handlers)
        {
            if (handlers.Length == 0)
                throw new ArgumentException("Need at least one handler", "handlers");
            handlers.ForEach(dispatcherQueue.Enqueue);
        }

        #region Handles<T> Members

        public void Handle(T message)
        {
            var handler = dispatcherQueue.Dequeue();
            handler.Handle(message);
            dispatcherQueue.Enqueue(handler);
        }

        #endregion

        public void Add(Handles<T> handler)
        {
            dispatcherQueue.Enqueue(handler);
        }
    }
}