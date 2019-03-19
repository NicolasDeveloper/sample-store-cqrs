using SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Models;

namespace SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Interfaces
{
    public interface IOrderRepository
    {
        void Save(Order order);
        void Update(Order order);
        Order GetByNumber(string number);
    }
}
