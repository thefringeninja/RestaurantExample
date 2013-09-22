using System.Threading;

namespace ClassLibrary4
{
    public class ShortestQueueDispatcher<T> : Handles<T> where T : Message
    {
        private readonly QueuedHandler<T>[] handlers;
        private readonly int dispatchWhenQueueIs;

        public ShortestQueueDispatcher(QueuedHandler<T>[] handlers, int dispatchWhenQueueIs = 5)
        {
            this.handlers = handlers;
            this.dispatchWhenQueueIs = dispatchWhenQueueIs;
        }

        public void Handle(T message)
        {
            start:
            foreach (var queuedHandler in handlers)
            {
                if (queuedHandler.Count < dispatchWhenQueueIs)
                {
                    queuedHandler.Handle(message);
                    return;
                }
            }

            Thread.Sleep(1);

            goto start;
        }
    }
}