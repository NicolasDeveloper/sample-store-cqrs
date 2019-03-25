using SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Models;
using SampleStoreCQRS.Domain.Core.Events;
using System;

namespace SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Events
{
    public class OrderPlacedEvent : Event
    {
        public Guid Id { get; private set; }
        public Customer Customer { get; private set; }
        public string Number { get; private set; }
        public Payment Payment { get; private set; }

        public OrderPlacedEvent(
            Guid id,
            Customer customer,
            Payment payment,
            string number)
        {
            Id = id;
            Number = number;
            Customer = customer;
            Payment = payment;
        }
    }
}
