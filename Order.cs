using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace ClassLibrary4
{
    public class Order
    {
        private readonly JObject document;

        public Order(int orderNumber)
            : this(new JObject())
        {
            OrderNumber = orderNumber;
        }

        public Order(JObject document)
        {
            this.document = document;
        }

        public int OrderNumber
        {
            get { return document.Value<int>("OrderNumber"); }
            private set { document["OrderNumber"] = value; }
        }

        public int Table
        {
            get { return document.Value<int>("Table"); }
            set { document["Table"] = value; }
        }

        public string Waiter
        {
            get { return document.Value<string>("Waiter"); }
            set { document["Waiter"] = value; }
        }

        public DateTime? CookedOn
        {
            get
            {
                var cookedOn = document["CookedOn"];
                return cookedOn == null
                    ? default(DateTime?)
                    : (DateTime)cookedOn;
            }
            set
            {
                document["CookedOn"] = value;
            }
        }

        public JObject Receipt
        {
            get { return document["Receipt"].ToObject<JObject>(); }
            set { document["Receipt"] = value; }
        }

        public bool Paid
        {
            get { return document["Receipt"] != null; }
        }

        public decimal Total
        {
            get
            {
                var total = document["Total"];
                return total == null
                    ? 0m
                    : (decimal) total;
            }
            set { document["Total"] = value; }
        }

        public decimal Subtotal
        {
            get
            {
                return Items.Sum(i => i.Price ?? 0m);
            }
        }

        public decimal Tax
        {
            get
            {
                var tax = document["Tax"];

                return tax == null
                    ? 0m
                    : (decimal) tax;
            }
            set
            {
                document["Tax"] = value;
            }
        }

        public decimal Tip
        {
            get
            {
                var tax = document["Tip"];

                return tax == null
                    ? 0m
                    : (decimal)tax;
            }
            set
            {
                document["Tip"] = value;
            }
        }

        public void Add(Item item)
        {
            var items = (JArray)document["Items"];
            if (items == null)
            {
                document["Items"] = items = new JArray();
            }
            
            items.Add((JObject)item);
        }

        public IEnumerable<Item> Items
        {
            get
            {
                var items = (JArray)document["Items"];
                if (items == null) return Enumerable.Empty<Item>();

                return from item in items.OfType<JObject>()
                       select new Item(item);
            }
        }

        public DateTime TookOrderOn
        {
            get { return document.Value<DateTime>("TookOrderOn"); }
            set { document["TookOrderOn"] = value; }
        }

        public string Chef
        {
            get { return document.Value<string>("Chef"); }
            set { document["Chef"] = value; }
        }

        public class Item
        {
            private readonly JObject document;

            public Item(JObject document)
            {
                this.document = document;
            }

            public Item(string plate, int quantity, decimal? price = null)
                : this(new JObject
                {
                    {"Plate", plate},
                    {"Quantity", quantity},
                    {"Price", price},
                    {"Ingredients", ""}
                })
            {
                
            }

            public static implicit operator JObject(Item item)
            {
                return item.document;
            }

            public string Plate
            {
                get { return document.Value<string>("Plate"); }
                set { document["Plate"] = value; }
            }

            public string[] Ingredients
            {
                get { return document.Value<string>("Ingredients").Split(','); }
                set { document["Ingredients"] = String.Join(",", value); }
            }

            public int Quantity
            {
                get { return document.Value<int>("Quantity"); }
                set { document["Quantity"] = value; }
            }
            
            public decimal? Price
            {
                get
                {
                    var price = document["Price"];

                    return price == null
                        ? default(decimal?)
                        : (decimal) price;
                }
                set
                {
                    document["Price"] = value;
                }
            }


        }
    }
}