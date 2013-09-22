using System;

namespace ClassLibrary4
{
    public static class Messages
    {
        #region Nested type: PrepareFood

        public class PrepareFood : TimeToLiveMessage
        {
            public readonly Order Order;
            private readonly Guid causationId;
            private readonly Guid correlationId;
            private readonly Guid messageId = Guid.NewGuid();
            private readonly DateTime timeToLive;

            public PrepareFood(Order order, DateTime timeToLive, Guid correlationId, Guid causationId)
            {
                Order = order;
                this.timeToLive = timeToLive;
                this.correlationId = correlationId;
                this.causationId = causationId;
            }

            #region TimeToLiveMessage Members

            public Guid CorrelationId
            {
                get { return correlationId; }
            }

            public Guid CausationId
            {
                get { return causationId; }
            }

            public Guid MessageId
            {
                get { return messageId; }
            }

            public DateTime TimeToLive
            {
                get { return timeToLive; }
            }

            #endregion
        }

        #endregion

        public class Timeout<T> : Message where T : Message
        {
            public readonly T Message;
            public readonly DateTime AlarmTime;
            private Guid correlationId;
            private Guid causationId;
            private Guid messageId;

            public Guid CorrelationId
            {
                get { return correlationId; }
            }

            public Guid CausationId
            {
                get { return causationId; }
            }

            public Guid MessageId
            {
                get { return messageId; }
            }

            public Timeout(T message, DateTime alarmTime)
            {
                this.Message = message;
                this.AlarmTime = alarmTime;
            }

            public override string ToString()
            {
                return string.Format("Timeout of {0} at {1}", Message, AlarmTime);
            }
        }

        #region Nested type: FoodPrepared

        public class FoodPrepared : TimeToLiveMessage
        {
            public readonly Order Order;
            private readonly Guid causationId;
            private readonly Guid correlationId;
            private readonly Guid messageId = Guid.NewGuid();
            private readonly DateTime timeToLive;

            public FoodPrepared(Order order, Guid correlationId, Guid causationId, DateTime timeToLive)
            {
                Order = order;
                this.correlationId = correlationId;
                this.causationId = causationId;
                this.timeToLive = timeToLive;
            }

            #region Message Members

            public Guid CorrelationId
            {
                get { return correlationId; }
            }

            public Guid CausationId
            {
                get { return causationId; }
            }

            public Guid MessageId
            {
                get { return messageId; }
            }

            #endregion

            public override string ToString()
            {
                return string.Format(
                    "Chef {0} prepared order {1} and it took him {2}",
                    Order.Chef,
                    Order.OrderNumber,
                    Order.CookedOn - Order.TookOrderOn);
            }

            public DateTime TimeToLive
            {
                get { return timeToLive; }
            }
        }

        #endregion

        #region Nested type: OrderPaid

        public class OrderPaid : TimeToLiveMessage
        {
            public readonly Order Order;
            private readonly Guid causationId;
            private readonly Guid correlationId;
            private readonly Guid messageId = Guid.NewGuid();
            private readonly DateTime timeToLive;

            public OrderPaid(Order order, Guid correlationId, Guid causationId, DateTime timeToLive)
            {
                Order = order;
                this.correlationId = correlationId;
                this.causationId = causationId;
                this.timeToLive = timeToLive;
            }

            #region Message Members

            public Guid CorrelationId
            {
                get { return correlationId; }
            }

            public Guid CausationId
            {
                get { return causationId; }
            }

            public Guid MessageId
            {
                get { return messageId; }
            }

            #endregion

            public override string ToString()
            {
                return String.Format("Order {0} was paid.", Order.OrderNumber);
            }

            public DateTime TimeToLive
            {
                get { return timeToLive; }
            }
        }

        #endregion

        #region Nested type: OrderPlaced

        public class OrderPlaced : TimeToLiveMessage
        {
            public readonly Order Order;
            private readonly Guid causationId = Guid.Empty;
            private readonly Guid correlationId = Guid.NewGuid();
            private readonly Guid messageId = Guid.NewGuid();
            private readonly DateTime timeToLive;

            public OrderPlaced(Order order, DateTime timeToLive)
            {
                Order = order;
                this.timeToLive = timeToLive;
            }

            #region TimeToLiveMessage Members

            public DateTime TimeToLive
            {
                get { return timeToLive; }
            }

            public Guid CorrelationId
            {
                get { return correlationId; }
            }

            public Guid CausationId
            {
                get { return causationId; }
            }

            public Guid MessageId
            {
                get { return messageId; }
            }

            #endregion

            public override string ToString()
            {
                return string.Format("Order {0}  was placed on {1}.", Order.OrderNumber, Order.TookOrderOn);
            }
        }

        #endregion

        #region Nested type: OrderPlaced

        public class SuspectOrderPlaced : TimeToLiveMessage
        {
            public readonly Order Order;
            private readonly Guid causationId = Guid.Empty;
            private readonly Guid correlationId = Guid.NewGuid();
            private readonly Guid messageId = Guid.NewGuid();
            private readonly DateTime timeToLive;

            public SuspectOrderPlaced(Order order, DateTime timeToLive)
            {
                Order = order;
                this.timeToLive = timeToLive;
            }

            #region TimeToLiveMessage Members

            public DateTime TimeToLive
            {
                get { return timeToLive; }
            }

            public Guid CorrelationId
            {
                get { return correlationId; }
            }

            public Guid CausationId
            {
                get { return causationId; }
            }

            public Guid MessageId
            {
                get { return messageId; }
            }

            #endregion

            public override string ToString()
            {
                return string.Format("Suspect order {0} was placed on {1}.", Order.OrderNumber, Order.TookOrderOn);
            }
        }

        #endregion
        #region Nested type: OrderTotalled

        public class OrderTotalled : TimeToLiveMessage
        {
            public readonly Order Order;
            private readonly Guid causationId;
            private readonly Guid correlationId;
            private readonly Guid messageId = Guid.NewGuid();
            private readonly DateTime timeToLive;

            public OrderTotalled(Order order, Guid correlationId, Guid causationId, DateTime timeToLive)
            {
                Order = order;
                this.correlationId = correlationId;
                this.causationId = causationId;
                this.timeToLive = timeToLive;
            }

            #region Message Members

            public Guid CorrelationId
            {
                get { return correlationId; }
            }

            public Guid CausationId
            {
                get { return causationId; }
            }

            public Guid MessageId
            {
                get { return messageId; }
            }

            #endregion

            public override string ToString()
            {
                return string.Format("order {0} was totalled at {1}", Order.OrderNumber, Order.Total);
            }

            public DateTime TimeToLive
            {
                get { return timeToLive; }
            }
        }

        #endregion

        #region Nested type: ProcessCompleted

        public class ProcessCompleted : Message
        {
            private readonly Guid causationId;
            private readonly Guid correlationId;
            private readonly Guid messageId = Guid.NewGuid();

            public ProcessCompleted(Guid correlationId, Guid causationId)
            {
                this.correlationId = correlationId;
                this.causationId = causationId;
            }

            #region Message Members

            public Guid CorrelationId
            {
                get { return correlationId; }
            }

            public Guid CausationId
            {
                get { return causationId; }
            }

            public Guid MessageId
            {
                get { return messageId; }
            }

            #endregion
        }

        #endregion

        public class ReceiveOrder : TimeToLiveMessage
        {
            public readonly Order Order;
            private readonly Guid causationId;
            private readonly Guid correlationId;
            private readonly Guid messageId = Guid.NewGuid();
            private readonly DateTime timeToLive;

            public ReceiveOrder(Order order, Guid correlationId, Guid causationId, DateTime timeToLive)
            {
                Order = order;
                this.correlationId = correlationId;
                this.causationId = causationId;
                this.timeToLive = timeToLive;
            }

            #region Message Members

            public Guid CorrelationId
            {
                get { return correlationId; }
            }

            public Guid CausationId
            {
                get { return causationId; }
            }

            public Guid MessageId
            {
                get { return messageId; }
            }

            #endregion

            public override string ToString()
            {
                return string.Format("order {0} was totalled at {1}", Order.OrderNumber, Order.Total);
            }

            public DateTime TimeToLive
            {
                get { return timeToLive; }
            }
        }

        public class TotalOrder : TimeToLiveMessage
        {
            public readonly Order Order;
            private readonly Guid causationId;
            private readonly Guid correlationId;
            private readonly Guid messageId = Guid.NewGuid();
            private readonly DateTime timeToLive;

            public TotalOrder(Order order, Guid correlationId, Guid causationId, DateTime timeToLive)
            {
                Order = order;
                this.correlationId = correlationId;
                this.causationId = causationId;
                this.timeToLive = timeToLive;
            }

            #region Message Members

            public Guid CorrelationId
            {
                get { return correlationId; }
            }

            public Guid CausationId
            {
                get { return causationId; }
            }

            public Guid MessageId
            {
                get { return messageId; }
            }

            #endregion

            public override string ToString()
            {
                return string.Format("order {0} was totalled at {1}", Order.OrderNumber, Order.Total);
            }

            public DateTime TimeToLive
            {
                get { return timeToLive; }
            }
        }
    }
}
