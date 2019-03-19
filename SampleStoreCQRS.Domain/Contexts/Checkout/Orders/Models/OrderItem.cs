

using SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Validations.Models;
using SampleStoreCQRS.Domain.Core.Models;

namespace SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Models
{
    public class OrderItem: Aggregate
    {
        public Product Product { get; private set; }
        public decimal Quantity { get; private set; }
        public decimal Price { get; private set; }
        public string Description { get; private set; }

        public OrderItem(Product product, decimal quantity)
        {
            Product = product;
            Quantity = quantity;
            Price = product.Price;
            Description = product.Description;

            ValidationResult = new OrderItemValidation().Validate(this);
        }

        public override bool IsValid()
        {
            return ValidationResult.IsValid;
        }
    }
}
