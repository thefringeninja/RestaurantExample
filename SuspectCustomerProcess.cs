﻿namespace ClassLibrary4
{
    public class SuspectCustomerProcess : Handles<Messages.OrderPaid>,
        Handles<Messages.FoodPrepared>,
        Handles<Messages.SuspectOrderPlaced>,
        Handles<Messages.OrderTotalled>,
        Handles<Messages.PrepareFood>
    {
        private readonly AssistantManager assistantManager;
        private readonly Cashier cashier;
        private readonly EnterpriseExcel reporting;
        private readonly AlarmClock alarmClock;
        private readonly Handles<Messages.PrepareFood> kitchen;

        public SuspectCustomerProcess(Handles<Messages.PrepareFood> kitchen, AssistantManager assistantManager, Cashier cashier, EnterpriseExcel reporting, AlarmClock alarmClock)
        {
            this.assistantManager = assistantManager;
            this.cashier = cashier;
            this.reporting = reporting;
            this.alarmClock = alarmClock;
            this.kitchen = kitchen;
        }

        public void Handle(Messages.FoodPrepared message)
        {
            reporting.Report(message.Order);
        }

        public void Handle(Messages.SuspectOrderPlaced message)
        {
            assistantManager.Handle(new Messages.TotalOrder(message.Order, message.CorrelationId, message.MessageId, message.TimeToLive));
        }

        public void Handle(Messages.OrderPaid message)
        {
            alarmClock.Handle(message);
            kitchen.Handle(new Messages.PrepareFood(message.Order, message.TimeToLive, message.CorrelationId, message.MessageId));
        }

        public void Handle(Messages.OrderTotalled message)
        {
            cashier.Handle(new Messages.ReceiveOrder(message.Order, message.CorrelationId, message.MessageId, message.TimeToLive));
        }

        public void Handle(Messages.PrepareFood message)
        {

        }
    }
}