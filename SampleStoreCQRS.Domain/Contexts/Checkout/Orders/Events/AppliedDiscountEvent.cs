using SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Models;
using SampleStoreCQRS.Domain.Core.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Events
{
    public class AppliedDiscountEvent: Event
    {

        public Guid OrderId { get; set; }
        public Guid CustomerId { get; set; }
        public string CuponCod { get; set; }
        public decimal Percentage { get; set; }
        public decimal Total { get; set; }
        public decimal TotalWithDiscount { get; set; }
    
        public AppliedDiscountEvent(
            Guid orderId, 
            Guid customerId, 
            string cuponCod, 
            decimal percentage, 
            decimal total, 
            decimal totalWithDiscount)
        {
            OrderId = orderId;
            CustomerId = customerId;
            CuponCod = cuponCod;
            Percentage = percentage;
            Total = total;
            TotalWithDiscount = totalWithDiscount;
        }        
    }
}
