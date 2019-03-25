
using SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Validations.Models;
using SampleStoreCQRS.Domain.Core.Interfaces;
using SampleStoreCQRS.Domain.Core.Models;
using SampleStoreCQRS.Domain.Core.ValueObjects;

namespace SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Models
{
    public class Payment : Aggregate
    {
        public CreditCard CreditCard { get; private set; }

        protected Payment() { }

        public Payment(IPaymentMethod paymentMethod)
        {

            if (paymentMethod is CreditCard)
            {
                CreditCard = paymentMethod as CreditCard;
            }

            ValidationResult = new PaymentValidation().Validate(this);
            AddNotifications(paymentMethod.ValidationResult.Errors);
        }

        public override bool IsValid()
        {
            return ValidationResult.IsValid;
        }
    }
}
