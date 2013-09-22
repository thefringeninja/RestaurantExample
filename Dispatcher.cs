using System.Collections.Generic;

namespace ClassLibrary4
{
    public class Dispatcher : IPublishEvents
    {
        private readonly IDictionary<string, Multiplexor<Message>> router = new Dictionary<string,Multiplexor<Message>>();

        public void Subscribe<T>(Handles<T> handler) where T : Message
        {
            Subscribe(typeof(T).Name, handler);
        }

        public void Subscribe<T>(string topic, Handles<T> handler) where T : Message
        {
            Multiplexor<Message> multiplexor;
            if (false == router.TryGetValue(topic, out multiplexor))
            {
                multiplexor = new Multiplexor<Message>();
                router.Add(topic, multiplexor);
            }
            multiplexor.Add(new NarrowingHandler<T, Message>(handler));
        }

        public void Unsubscribe<T>(Handles<T> handler) where T : Message
        {
            Unsubscribe(typeof(T).Name, handler);
        }

        public void Unsubscribe<T>(string topic, Handles<T> handler) where T : Message
        {
            Multiplexor<Message> multiplexor;
            if (false == router.TryGetValue(topic, out multiplexor))
                return;
            multiplexor.Remove(new NarrowingHandler<T, Message>(handler));
        }

        public void Publish(string topic, Message message)
        {
            Multiplexor<Message> handler;
            if (router.TryGetValue(topic, out handler))
            {
                handler.Handle(message);
            }
            if (router.TryGetValue(message.CorrelationId.ToString(), out handler))
            {
                handler.Handle(message);
            }
        }

        public void Publish<T>(T message) where T : Message
        {
            Publish(typeof(T).Name, message);
        }
    }
}