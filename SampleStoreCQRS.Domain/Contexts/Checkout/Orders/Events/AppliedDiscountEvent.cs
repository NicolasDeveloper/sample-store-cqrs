using SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Enuns;
using SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Models;
using System;

namespace SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Events
{
    public class AppliedDiscountEvent: OrderEvent
    {

        public string CouponCod { get; set; }
        public decimal Percentage { get; set; }

        public AppliedDiscountEvent(
            Guid id, 
            Guid customerId, 
            string number, 
            EOrderStatus status, 
            decimal total, 
            decimal totalWithDiscount,
            string couponCod,
            decimal percentage) : base(id, customerId, number, status, total, totalWithDiscount)
        {
            CouponCod = couponCod;
            Percentage = percentage;
        }  
    }
}
