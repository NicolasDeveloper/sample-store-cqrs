using SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Validations.Models;
using SampleStoreCQRS.Domain.Core.Models;
using SampleStoreCQRS.Domain.Core.ValueObjects;

namespace SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Models
{
    public class Customer : Aggregate
    {
        public virtual Name Name { get; protected set; }
        public virtual Email Email { get; protected set;  }
        public virtual string Phone { get; protected set; }
        public virtual Document Document { get; protected set; }

        protected Customer() { }

        public Customer(Name name, string phone, Email email, Document document)
        {
            Name = name;
            Email = email;
            Phone = phone;
            Document = document;
        }
        
        public override bool IsValid()
        {
            ValidationResult = new CustomerValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
