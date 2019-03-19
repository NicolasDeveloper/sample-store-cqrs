using SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Enuns;
using SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Models;
using SampleStoreCQRS.Domain.Core.Events;
using System;

namespace SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Events
{
    public class OrderStatusChangedEvent : Event
    {
        public Guid Id { get; private set; }
        public Customer Customer { get; private set; }
        public string Number { get; private set; }
        public EOrderStatus Status { get; private set; }

        public OrderStatusChangedEvent(
            Guid id, 
            Customer customer, 
            EOrderStatus status, 
            string number)
        {
            Id = id;
            Number = number;
            Customer = customer;
            Status = status;
        }
    }
}