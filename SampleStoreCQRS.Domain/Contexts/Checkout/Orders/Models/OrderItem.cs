

using SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Validations.Models;
using SampleStoreCQRS.Domain.Core.Models;

namespace SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Models
{
    public class OrderItem: Aggregate
    {
        public virtual decimal Quantity { get; protected set; }
        public virtual decimal Price { get; protected set; }
        public virtual string Description { get; protected set; }
        public virtual Product Product { get; protected set; }

        protected OrderItem() { }

        public OrderItem(Product product, decimal quantity)
        {
            Product = product;
            Quantity = quantity;
            Price = product.Price;
            Description = product.Description;
        }
        
        public override bool IsValid()
        {
            ValidationResult = new OrderItemValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
