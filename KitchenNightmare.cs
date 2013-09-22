using System;
using System.Threading;

namespace ClassLibrary4
{
    public class KitchenNightmare : Handles<Messages.PrepareFood>
    {
        private readonly Chef chef;

        public KitchenNightmare(Chef chef)
        {
            this.chef = chef;
        }

        readonly Random random = new Random();
        public void Handle(Messages.PrepareFood message)
        {
            var nextDouble = random.NextDouble();
            if (nextDouble < 0.1)
            {
                Console.WriteLine("THIS SOUP");
                Console.WriteLine("IS DRY!!!");
                return;
            }
            if (nextDouble < 0.25)
            {
                Console.WriteLine("YOU PUT SO MUCH GINGER IN THE FOOD");
                Console.WriteLine("IT HAS NO SOUL!!!");
                return;
            }

            if (nextDouble < 0.5)
            {
                Console.WriteLine("THERE'S SO MUCH OIL ON");
                Console.WriteLine("THE PLATE THE US IS ABOUT TO INVADE!!!");
                return;
            }

            chef.Handle(message);
        }
    }
}