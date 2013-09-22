using System;
using System.Collections.Generic;

namespace ClassLibrary4
{
    public class AssistantManager : Handles<Messages.TotalOrder>
    {
        private readonly IPublishEvents next;

        private static readonly IDictionary<string, decimal> PricesDatabase = new Dictionary<string, decimal>
        {
            {"Pizza", 15m},
            {"Chicharron", 15m},
            {"Miguitas", 15m}
        };

        public AssistantManager(IPublishEvents next)
        {
            this.next = next;
        }

        private const decimal TaxRate = 0.825m;

        public void Handle(Messages.TotalOrder message)
        {
            var order = message.Order;
            foreach (var item in order.Items) 
            {
                item.Price = PricesDatabase[item.Plate];
            }

            order.Tax = order.Subtotal * TaxRate;
            order.Total = order.Subtotal + order.Tax + order.Tip;

            next.Publish(new Messages.OrderTotalled(order, message.CorrelationId, message.MessageId, message.TimeToLive));
        }

        public void TotalUp(Order order, Guid correlationId, Guid causationId)
        {
        }
    }
}