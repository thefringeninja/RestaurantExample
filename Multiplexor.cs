using System.Collections.Generic;
using System.Linq;

namespace ClassLibrary4
{
    public class Multiplexor<T> : Handles<T> where T : Message
    {
        private List<Handles<T>> handlers;

        public Multiplexor(params Handles<T>[] handlers)
        {
            this.handlers = handlers.ToList();
        }

        #region Handles<T> Members

        public void Handle(T message)
        {
            handlers.ForEach(handler => handler.Handle(message));
        }

        #endregion

        public void Add(Handles<T> handler)
        {
            var handlersLocal = new List<Handles<T>>(handlers) {handler};
            handlers = handlersLocal;
        }

        public void Remove(Handles<T> handler)
        {
            var handlersLocal = new List<Handles<T>>(handlers);
            handlersLocal.Remove(handler);
            handlers = handlersLocal;
        }
    }
}
