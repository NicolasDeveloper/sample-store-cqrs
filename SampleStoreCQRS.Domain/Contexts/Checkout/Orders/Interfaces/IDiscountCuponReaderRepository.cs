using SampleStoreCQRS.Domain.Core.Interfaces;
using SampleStoreCQRS.Domain.Core.ValueObjects;

namespace SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Interfaces
{
    public interface IDiscountCuponReaderRepository: IRepository<DiscountCupon>
    {
        DiscountCupon GetCupon(string cod);
    }
}
