using System;

namespace ClassLibrary4
{
    public class TimeToLiveHandler<T> : Handles<T> where T : TimeToLiveMessage
    {
        private readonly Handles<T> handler;

        public TimeToLiveHandler(Handles<T> handler)
        {
            this.handler = handler;
        }

        public void Handle(T message)
        {
            if (message.TimeToLive < DateTime.UtcNow)
            {
                return;
            }
                
            handler.Handle(message);
        }
    }
}