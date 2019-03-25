using System;
using System.Collections.Generic;
using SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Validations.Commands;

namespace SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Commands.Inputs
{
    public class PlaceOrderCommand: OrderCommand
    {
        public string DiscountCupon { get; set; }

        public PlaceOrderCommand()
        {
            OrderItems = new List<OrderItemCommand>();
        }

        public override bool IsValid()
        {
            ValidationResult = new PlaceOrderCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class OrderItemCommand
    {
        public Guid Product { get; set; }
        public decimal Quantity { get; set; }
    }

    public class CreditCardCommand
    {
        public string Number { get; set; }
        public int Cvv { get; set; }
        public virtual string Validate { get; set; }
        public virtual string PrintName { get; set; }
    }
}
