using SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Commands.Inputs;

namespace SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Validations.Commands
{
    public class PlaceOrderCommandValidation : OrderValidation<PlaceOrderCommand>
    {
        public PlaceOrderCommandValidation()
        {
            ValidateCustomer();
            ValidateOrderItems();
            ValidateCreditCard();
        }
    }
}
