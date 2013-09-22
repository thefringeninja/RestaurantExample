using System.Reflection.Emit;

namespace ClassLibrary4
{
    public class Combiner<T> : Handles<T> where T : Message
    {
        private readonly Handles<T> handler;

        public Combiner(Handles<T> handler)
        {
            this.handler = handler;
        }

        public void Handle(T message)
        {
            handler.Handle(message);
        }
    }
}