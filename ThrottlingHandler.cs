using System.Threading;

namespace ClassLibrary4
{
    public class ThrottlingHandler<T> : Handles<T> where T : Message
    {
        private readonly QueuedHandler<T> handler;

        public ThrottlingHandler(QueuedHandler<T> handler)
        {
            this.handler = handler;
        }

        public void Handle(T message)
        {
            while (handler.Count > 10000)
            {
                Thread.Sleep(1);
            }
            handler.Handle(message);
        }
    }
}