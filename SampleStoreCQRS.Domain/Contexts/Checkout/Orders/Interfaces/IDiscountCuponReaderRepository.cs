using SampleStoreCQRS.Domain.Core.ValueObjects;

namespace SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Interfaces
{
    public interface IDiscountCuponReaderRepository
    {
        DiscountCupon GetCupon(string cod);
    }
}
