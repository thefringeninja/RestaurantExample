using System;

namespace ClassLibrary4
{
    public class AdHocHandler<T> : Handles<T> where T : Message
    {
        private readonly Action<T> handler;

        public AdHocHandler(Action<T> handler)
        {
            this.handler = handler;
        }

        public void Handle(T message)
        {
            handler(message);
        }
    }
}