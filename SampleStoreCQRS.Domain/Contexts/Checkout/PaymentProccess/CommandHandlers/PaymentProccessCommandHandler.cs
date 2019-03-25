using MediatR;
using System.Threading;
using System.Threading.Tasks;
using SampleStoreCQRS.Domain.Contexts.Checkout.PaymentProccess.Commands.Inputs;
using SampleStoreCQRS.Domain.Core.Bus;
using SampleStoreCQRS.Domain.Core.Handlers;
using SampleStoreCQRS.Domain.Core.Interfaces;
using SampleStoreCQRS.Domain.Core.Notifications;

namespace SampleStoreCQRS.Domain.Contexts.Checkout.PaymentProccess.CommandHandlers
{
    public class PaymentProccessCommandHandler : CommandHandler,
        IRequestHandler<PaymentProccessCommand, bool>
    {
        public PaymentProccessCommandHandler(
            IUnitOfWork uow, 
            IMediatorHandler bus, 
            INotificationHandler<DomainNotification> notifications) : base(uow, bus, notifications)
        {
        }

        public Task<bool> Handle(PaymentProccessCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(true);
        }
    }
}
