
using SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Validations.Models;
using SampleStoreCQRS.Domain.Core.Interfaces;
using SampleStoreCQRS.Domain.Core.Models;
using SampleStoreCQRS.Domain.Core.ValueObjects;

namespace SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Models
{
    public class Payment : Aggregate
    {
        public virtual CreditCard CreditCard { get; protected set; }

        protected Payment() { }

        public Payment(IPaymentMethod paymentMethod)
        {

            if (paymentMethod is CreditCard)
            {
                CreditCard = paymentMethod as CreditCard;
            }

            if(!paymentMethod.IsValid())
                AddValidationResults(paymentMethod.ValidationResult);
        }
        
        public override bool IsValid()
        {
            ValidationResult = new PaymentValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
