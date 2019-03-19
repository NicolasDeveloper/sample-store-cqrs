using SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Validations.Commands;

namespace SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Commands.Inputs
{
    public class PayOrderCommand : OrderCommand
    {
        public PayOrderCommand()
        {
        }

        public override bool IsValid()
        {
            ValidationResult = new PayOrderCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class CreditCardCommand
    {
        public string Number { get; set; }
        public int Cvv { get; set; }
        public virtual string Validate { get; set; }
        public virtual string PrintName { get; set; }
    }
}
