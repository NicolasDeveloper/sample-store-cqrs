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
}
