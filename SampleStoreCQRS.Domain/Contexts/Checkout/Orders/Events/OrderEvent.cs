using SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Enuns;
using SampleStoreCQRS.Domain.Core.Events;
using System;

namespace SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Events
{
    public class OrderEvent: Event
    {
        public OrderEvent(Guid id, Guid customerId, string number, EOrderStatus status, decimal total, decimal totalWithDiscount)
        {
            Id = id;
            AggregateId = id;
            CustomerId = customerId;
            Number = number;
            Status = status;
            Total = total;
            TotalWithDiscount = totalWithDiscount;
        }

        public Guid Id { get; private set; }
        public Guid CustomerId { get; private set; }
        public string Number { get; private set; }
        public EOrderStatus Status { get; private set; }
        public decimal Total { get; set; }
        public decimal TotalWithDiscount { get; set; }
    }
}
