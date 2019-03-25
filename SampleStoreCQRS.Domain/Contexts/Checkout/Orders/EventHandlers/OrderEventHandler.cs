using MediatR;
using SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Events;
using System.Threading;
using System.Threading.Tasks;

namespace SampleStoreCQRS.Domain.Contexts.Checkout.Orders.EventHandlers
{
    public class OrderEventHandler :
        INotificationHandler<OrderStatusChangedEvent>,
        INotificationHandler<AppliedDiscountEvent>,
        INotificationHandler<OrderPlacedEvent>
    {
        public Task Handle(OrderStatusChangedEvent @event, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(AppliedDiscountEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(OrderPlacedEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
