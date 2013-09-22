using System;
using System.Collections.Generic;

namespace ClassLibrary4
{
    public class RestaurantProcessStorage : Handles<Messages.OrderPaid>,
        Handles<Messages.FoodPrepared>,
        Handles<Messages.OrderPlaced>,
        Handles<Messages.OrderTotalled>,
        Handles<Messages.ProcessCompleted>,
        Handles<Messages.PrepareFood>,
        Handles<Messages.SuspectOrderPlaced>,
        Handles<Messages.TotalOrder>
    {
        private readonly AssistantManager assistantManager;
        private readonly Cashier cashier;

        private readonly
            IDictionary<Type, Func<Handles<Messages.PrepareFood>, AssistantManager, Cashier, EnterpriseExcel, AlarmClock, dynamic>>
            factory = new Dictionary
                <Type, Func<Handles<Messages.PrepareFood>, AssistantManager, Cashier, EnterpriseExcel, AlarmClock, dynamic>>
            {
                {
                    typeof (Messages.SuspectOrderPlaced),
                    (kitchen, assistantManager, cashier, enterpriseExcel, alarmClock) => new SuspectCustomerProcess(kitchen, assistantManager, cashier, enterpriseExcel, alarmClock)
                },
                {
                    typeof (Messages.OrderPlaced),
                    (kitchen, assistantManager, cashier, enterpriseExcel, alarmClock) => new RegularCustomerProcess(kitchen, assistantManager, cashier, enterpriseExcel, alarmClock)
                }
            };

        private readonly Handles<Messages.PrepareFood> kitchen;
        private readonly EnterpriseExcel reporting;
        private readonly IDictionary<Guid, dynamic> storage = new Dictionary<Guid, dynamic>();
        private readonly AlarmClock alarmClock;

        public RestaurantProcessStorage(
            Handles<Messages.PrepareFood> kitchen, AssistantManager assistantManager, Cashier cashier,
            EnterpriseExcel reporting, AlarmClock alarmClock)
        {
            this.assistantManager = assistantManager;
            this.reporting = reporting;
            this.alarmClock = alarmClock;
            this.cashier = cashier;
            this.kitchen = kitchen;
        }

        #region Handles<FoodPrepared> Members

        public void Handle(Messages.FoodPrepared message)
        {
            storage[message.CorrelationId].Handle(message);
        }

        #endregion

        #region Handles<OrderPaid> Members

        public void Handle(Messages.OrderPaid message)
        {
            storage[message.CorrelationId].Handle(message);
        }

        #endregion

        #region Handles<OrderPlaced> Members

        public void Handle(Messages.OrderPlaced message)
        {
            CreateProcess(message);
        }

        #endregion

        #region Handles<OrderTotalled> Members

        public void Handle(Messages.OrderTotalled message)
        {
            storage[message.CorrelationId].Handle(message);
        }

        #endregion

        #region Handles<PrepareFood> Members

        public void Handle(Messages.PrepareFood message)
        {
            storage[message.CorrelationId].Handle(message);
        }

        #endregion

        #region Handles<ProcessCompleted> Members

        public void Handle(Messages.ProcessCompleted message)
        {
            storage.Remove(message.CorrelationId);
        }

        #endregion

        #region Handles<SuspectOrderPlaced> Members

        public void Handle(Messages.SuspectOrderPlaced message)
        {
            CreateProcess(message);
        }

        #endregion

        #region Handles<TotalOrder> Members

        public void Handle(Messages.TotalOrder message)
        {
            storage[message.CorrelationId].Handle(message);
        }

        #endregion

        private void CreateProcess<T>(T message) where T : Message
        {
            var process = factory[message.GetType()](kitchen, assistantManager, cashier, reporting, alarmClock);
            storage[message.CorrelationId] = process;
            process.Handle(message);
        }
    }
}
