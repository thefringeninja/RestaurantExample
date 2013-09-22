using System;

namespace ClassLibrary4
{
    public class NarrowingHandler<TInput, TOutput> : Handles<TOutput>, IEquatable<NarrowingHandler<TInput, TOutput>> where TInput : Message, TOutput
                                                                                                                     where TOutput : Message
    {
        public bool Equals(NarrowingHandler<TInput, TOutput> other)
        {
            if (ReferenceEquals(null, other))
                return false;
            if (ReferenceEquals(this, other))
                return true;
            return Equals(handler, other.handler);
        }

        public override int GetHashCode()
        {
            return (handler != null
                ? handler.GetHashCode()
                : 0);
        }

        private readonly Handles<TInput> handler;

        public NarrowingHandler(Handles<TInput> handler)
        {
            this.handler = handler;
        }

        public void Handle(TOutput message)
        {
            handler.Handle((TInput)message);
        }

        public override bool Equals(object obj)
        {
            return handler.Equals(obj);
        }
    }
}