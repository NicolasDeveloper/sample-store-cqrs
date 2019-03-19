using SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Commands.Inputs;

namespace SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Validations.Commands
{
    public class ShipOrderCommandValidate : OrderValidation<ShipOrderCommand>
    {
        public ShipOrderCommandValidate()
        {
            ValidateNumber();
        }
    }
}
