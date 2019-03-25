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

            ValidationResult = new ProductValidation().Validate(this);
        }

        public string Title { get; private set; }
        public string Description { get; private set; }
        public string Image { get; private set; }
        public decimal Price { get; private set; }
        public int QuantityOnHand { get; private set; }

        public override string ToString()
        {
            return Title;
        }

        public override bool IsValid()
        {
            return ValidationResult.IsValid;
        }
    }
}
