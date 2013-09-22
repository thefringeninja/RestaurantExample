using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Threading;

namespace ClassLibrary4
{
    public class Chef : Handles<Messages.PrepareFood>
    {
        static readonly Random random = new Random();
        private static readonly IDictionary<string, string[]> BullshitRecipeDatabase = new Dictionary<string, string[]>
        {
            {"Pizza", new[]{"Ketchup", "Dough", "Pepperoni"}},
            {"Miguitas", new[]{"Tortillas", "Ketchup", "Weenies"}},
            {"Chicharron", new[]{"Skin", "Salsa Verde", "Cilantro"}}
        };
        private readonly IPublishEvents next;
        private readonly double efficiency;
        private readonly string name;

        public Chef(IPublishEvents next, string name, double efficiency = 1)
        {
            this.next = next;
            this.efficiency = efficiency;
            this.name = name;
        }

        public void Handle(Messages.PrepareFood message)
        {
            foreach (var item in message.Order.Items)
            {
                item.Ingredients = BullshitRecipeDatabase[item.Plate];
            }
            var cookTime = TimeSpan.FromSeconds(random.NextDouble() / efficiency);
            Thread.Sleep(cookTime);
            message.Order.CookedOn = DateTime.UtcNow;
            message.Order.Chef = name;
            next.Publish(new Messages.FoodPrepared(message.Order, message.CorrelationId, message.MessageId, message.TimeToLive));
        }

        public override string ToString()
        {
            return "Chef " + name;
        }
    }
}