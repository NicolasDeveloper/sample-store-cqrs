using SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Commands.Inputs;

namespace SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Validations.Commands
{
    public class CancelOrderCommandValidation : OrderValidation<CancelOrderCommand>
    {
        public CancelOrderCommandValidation()
        {
            ValidateNumber();
        }
    }
}
