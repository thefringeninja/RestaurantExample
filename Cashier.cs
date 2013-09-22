using System.Collections.Concurrent;
using Newtonsoft.Json.Linq;

namespace ClassLibrary4
{
    public class Cashier : Handles<Messages.ReceiveOrder>
    {
        private readonly IPublishEvents next;
        static readonly ConcurrentDictionary<int, Messages.ReceiveOrder> AwaitingPayment = new ConcurrentDictionary<int, Messages.ReceiveOrder>();

        public Cashier(IPublishEvents next)
        {
            this.next = next;
        }

        public void Handle(Messages.ReceiveOrder message)
        {
            AwaitingPayment.TryAdd(message.Order.OrderNumber, message);
        }

        public bool TryAcceptPayment(int orderNumber)
        {
            Messages.ReceiveOrder message;
            if (false == AwaitingPayment.TryRemove(orderNumber, out message))
                return false;
            message.Order.Receipt = new JObject();
            next.Publish(new Messages.OrderPaid(message.Order, message.CorrelationId, message.MessageId, message.TimeToLive));

            return true;
        }
    }
}