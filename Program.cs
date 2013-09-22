using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ClassLibrary4
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            var dispatcher = new Dispatcher();

            var cooks = new[]
            {
                new QueuedHandler<Messages.PrepareFood>(
                    new Chef(dispatcher, "Joe Bastianich", 1)),
                new QueuedHandler<Messages.PrepareFood>(
                    new Chef(dispatcher, "Gordon Ramsay", 3)),
                new QueuedHandler<Messages.PrepareFood>(
                    new KitchenNightmare(new Chef(dispatcher, "Amy's Baking Company", .5)))
            };
            var kitchenQueue = new QueuedHandler<Messages.PrepareFood>(
                new TimeToLiveHandler<Messages.PrepareFood>(
                    new ShortestQueueDispatcher<Messages.PrepareFood>(cooks)));
            kitchenQueue.Start();

            var kitchen = new ThrottlingHandler<Messages.PrepareFood>(kitchenQueue);

            var assistantManager = new AssistantManager(dispatcher);

            var cashier = new Cashier(dispatcher);

            var reporting = new EnterpriseExcel();


            var alarmClock = new AlarmClock(dispatcher);

            alarmClock.Start();

            var storage = new RestaurantProcessStorage(kitchen, assistantManager, cashier, reporting, alarmClock);
            var waiter = new Waiter("July M.", dispatcher);


            dispatcher.Subscribe(Log<Messages.OrderPlaced>(dispatcher));
            dispatcher.Subscribe(Log<Messages.SuspectOrderPlaced>(dispatcher));

            dispatcher.Subscribe<Messages.OrderPlaced>(storage);
            dispatcher.Subscribe<Messages.SuspectOrderPlaced>(storage);
            dispatcher.Subscribe<Messages.PrepareFood>(storage);
            dispatcher.Subscribe<Messages.FoodPrepared>(storage);
            dispatcher.Subscribe<Messages.TotalOrder>(storage);
            dispatcher.Subscribe<Messages.OrderTotalled>(storage);
            dispatcher.Subscribe<Messages.OrderPaid>(storage);
            dispatcher.Subscribe<Messages.ProcessCompleted>(storage);
            dispatcher.Subscribe<Messages.Timeout<Message>>(alarmClock);

            new Thread(
                _ => PayLoop(cashier)).Start();

            cooks.ForEach(x => x.Start());
            var plates = new[] {"Pizza", "Chicharron", "Miguitas"};
            for (var i = 0; i < 100000; i++)
            {
                waiter.PlaceOrder(i, 1, new Order.Item(plates[i%3], 1));
            }

            Console.ReadLine();
        }

        private static AdHocHandler<T> Log<T>(Dispatcher dispatcher) where T: Message
        {
            return new AdHocHandler<T>(
                message => dispatcher.Subscribe(
                    message.CorrelationId.ToString(),
                    new AdHocHandler<Message>(Console.WriteLine)));
        }

        private static void PayLoop(Cashier cashier)
        {
            var outstandingOrders = new Queue<int>(Enumerable.Range(0, 100000));

            while (outstandingOrders.Count > 0)
            {
                var nextOrder = outstandingOrders.Dequeue();
                if (false == cashier.TryAcceptPayment(nextOrder))
                {
                    outstandingOrders.Enqueue(nextOrder);
                }
            }
        }
    }
}
