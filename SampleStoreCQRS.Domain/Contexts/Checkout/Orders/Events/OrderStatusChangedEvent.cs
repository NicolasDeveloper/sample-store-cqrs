

using System;
using SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Enuns;
using SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Models;

namespace SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Events
{
    public class OrderStatusChangedEvent : OrderEvent
    {
        public OrderStatusChangedEvent(
            Guid id, 
            Guid customerId, 
            string number, 
            EOrderStatus status, 
            decimal total, 
            decimal totalWithDiscount) : base(id, customerId, number, status, total, totalWithDiscount)
        {
        }
    }
}