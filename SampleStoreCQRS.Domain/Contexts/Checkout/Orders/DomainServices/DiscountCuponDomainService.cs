using SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Interfaces;
using SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Models;
using SampleStoreCQRS.Domain.Core.Bus;
using SampleStoreCQRS.Domain.Core.Events;
using SampleStoreCQRS.Domain.Core.Notifications;

namespace SampleStoreCQRS.Domain.Contexts.Checkout.Orders.DomainServices
{
    public class DiscountCuponDomainService: Message
    {
        private IDiscountCuponReaderRepository _repostory;
        private IMediatorHandler _bus;

        public DiscountCuponDomainService(
            IMediatorHandler bus,
            IDiscountCuponReaderRepository repostory) : base()
        {
            
            _repostory = repostory;
            _bus = bus;
        }

        public Order CalcDiscount(string code, Order order)
        {
            var cupon = _repostory.GetCupon(code);

            if(cupon == null)
            {
                _bus.RaiseEvent(new DomainNotification(this.MessageType, $"Cupom {code} não foi encontrado"));
                return order;
            }

            if(cupon.Expired)
                _bus.RaiseEvent(new DomainNotification(this.MessageType, $"Cupom {cupon.Cod} está expirado"));
            
            return order.ApplyDiscount(cupon);
        }
    }
}
