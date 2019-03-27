using SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Enuns;
using SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Models;
using System;

namespace SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Events
{
    public class OrderPlacedEvent : OrderEvent
    {
        public Payment Payment { get; private set; }

        public OrderPlacedEvent(
            Guid id, 
            Guid customerId, 
            string number, 
            EOrderStatus status, 
            decimal total, 
            decimal totalWithDiscount,
            Payment payment) : base(id, customerId, number, status, total, totalWithDiscount)
        {
            Payment = payment;
        }
    }
}
