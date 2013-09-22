using System;
using System.Collections.Generic;

namespace ClassLibrary4
{
    public class Waiter
    {
        private readonly string name;
        private readonly IPublishEvents next;

        private readonly Random random = new Random();

        public Waiter(string name, IPublishEvents next)
        {
            this.name = name;
            this.next = next;
        }

        public void PlaceOrder(int orderNumber, int table, params Order.Item[] items)
        {
            var tookOrderOn = DateTime.UtcNow;
            var order = new Order(orderNumber) {Table = table, Waiter = name, TookOrderOn = tookOrderOn};
            items.ForEach(order.Add);

            var customerWalksOutAt = tookOrderOn.AddSeconds(30);

            if (random.NextDouble() > 0.75)
            {
                next.Publish(new Messages.OrderPlaced(order, customerWalksOutAt));
            }
            else
            {
                next.Publish(new Messages.SuspectOrderPlaced(order, customerWalksOutAt));
            }
        }
    }
}