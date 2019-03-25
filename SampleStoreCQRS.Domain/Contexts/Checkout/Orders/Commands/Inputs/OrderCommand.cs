using SampleStoreCQRS.Domain.Core.Commands;
using System;
using System.Collections.Generic;

namespace SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Commands.Inputs
{
    public abstract class OrderCommand: Command
    {
        public string Number { get; set; }
        public Guid CustomerId { get; set; }
        public IList<OrderItemCommand> OrderItems { get; set; }
        public CreditCardCommand CreditCard { get; set; }
    }

    
}
