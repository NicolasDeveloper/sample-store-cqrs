using SampleStoreCQRS.Domain.Contexts.Promotions.Models;
using SampleStoreCQRS.Domain.Core.Interfaces;
using SampleStoreCQRS.Domain.Core.ValueObjects;

namespace SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Interfaces
{
    public interface IDiscountCuponReaderRepository: IRepository<Coupon>
    {
        DiscountCupon GetCupon(string cod);
    }
}
