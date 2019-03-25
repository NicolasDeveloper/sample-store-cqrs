using SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Validations.Models;
using SampleStoreCQRS.Domain.Core.Models;
using SampleStoreCQRS.Domain.Core.ValueObjects;

namespace SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Models
{
    public class Customer : Aggregate
    {
        public Name Name { get; private set; }
        public Email Email { get; private set;  }
        public string Phone { get; private set; }
        public Document Document { get; private set; }

        protected Customer() { }

        public Customer(Name name, string phone, Email email, Document document)
        {
            Name = name;
            Email = email;
            Phone = phone;
            Document = document;

            ValidationResult = new CustomerValidation().Validate(this);
        }

        public override bool IsValid()
        {
            return ValidationResult.IsValid;
        }
    }
}
