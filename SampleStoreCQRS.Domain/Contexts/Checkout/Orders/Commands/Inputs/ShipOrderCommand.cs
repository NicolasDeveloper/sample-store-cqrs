using SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Validations.Commands;

namespace SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Commands.Inputs
{
    public class ShipOrderCommand: OrderCommand
    {   
        public ShipOrderCommand()
        {
            
        }

        public override bool IsValid()
        {
            ValidationResult = new ShipOrderCommandValidate().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
