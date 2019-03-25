using System;
using System.Collections.Generic;

namespace SampleStoreCQRS.Application.ViewModels
{
    public class OrderViewModel
    {
        public string DiscountCupon { get; set; }
        public string Number { get; set; }
        public Guid CustomerId { get; set; }
        public IList<OrderItemViewModel> OrderItems { get; set; }
        public CreditCardViewModel CreditCard { get; set; }
    }

    public class OrderItemViewModel
    {
        public Guid Product { get; set; }
        public decimal Quantity { get; set; }
    }

    public class CreditCardViewModel
    {
        public string Number { get; set; }
        public int Cvv { get; set; }
        public virtual string Validate { get; set; }
        public virtual string PrintName { get; set; }
    }
}
