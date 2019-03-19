using SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Validations.Commands;

namespace SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Commands.Inputs
{
    public class CancelOrderCommand : OrderCommand
    {
        public CancelOrderCommand()
        {

        }

        public override bool IsValid()
        {
            ValidationResult = new CancelOrderCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
