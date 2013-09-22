namespace ClassLibrary4
{
    public class Publisher<T> : Handles<T> where T : Message
    {
        private readonly Dispatcher dispatcher;

        public Publisher(Dispatcher dispatcher)
        {
            this.dispatcher = dispatcher;
        }

        public void Handle(T message)
        {
            dispatcher.Publish(typeof(T).Name, message);
        }
    }
}