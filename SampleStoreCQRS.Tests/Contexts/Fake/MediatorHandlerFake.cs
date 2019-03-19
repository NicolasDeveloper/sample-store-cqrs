using System.Threading;
using System.Threading.Tasks;
using SampleStoreCQRS.Domain.Core.Bus;
using SampleStoreCQRS.Domain.Core.Commands;
using SampleStoreCQRS.Domain.Core.Events;
using SampleStoreCQRS.Domain.Core.Notifications;

namespace SampleStoreCQRS.Tests.Contexts.Fake
{
    public class MediatorHandlerFake : IMediatorHandler
    {
        private DomainNotificationHandler _domainNotificationHandler;

        public MediatorHandlerFake(DomainNotificationHandler domainNotificationHandler)
        {
            _domainNotificationHandler = domainNotificationHandler;
        }

        public Task RaiseEvent<T>(T @event) where T : Event
        {
            if(@event.MessageType == "DomainNotification")
            {
                _domainNotificationHandler.Handle(@event as DomainNotification, CancellationToken.None);
            } 

            return Task.FromResult(true);
        }

        public Task SendCommand<T>(T command) where T : Command
        {
            return Task.FromResult(true);
        }
    }
}
