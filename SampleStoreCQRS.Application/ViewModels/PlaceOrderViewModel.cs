using System;
using System.Collections.Generic;
using System.Text;

namespace SampleStoreCQRS.Application.ViewModels
{
    public class PlaceOrderViewModel: OrderViewModel
    {
        public string DiscountCupon { get; set; }
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
