using SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Validations.Models;
using SampleStoreCQRS.Domain.Core.Models;

namespace SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Models
{
    public class Product: Aggregate
    {
        protected Product() { }

        public Product(
            string title,
            string description,
            string image,
            decimal price,
            int quantityOnHand)
        {
            Title = title;
            Description = description;
            Image = image;
            Price = price;
            QuantityOnHand = quantityOnHand;
        }

        public virtual string Title { get; protected set; }
        public virtual string Description { get; protected set; }
        public virtual string Image { get; protected set; }
        public virtual decimal Price { get; protected set; }
        public virtual int QuantityOnHand { get; protected set; }

        public override string ToString()
        {
            return Title;
        }

        public override bool IsValid()
        {
            ValidationResult = new ProductValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
