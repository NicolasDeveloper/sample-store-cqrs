using MediatR;
using SampleStoreCQRS.Domain.Core.Bus;
using SampleStoreCQRS.Domain.Core.Commands;
using SampleStoreCQRS.Domain.Core.Events;
using SampleStoreCQRS.Domain.Core.Interfaces;
using SampleStoreCQRS.Domain.Core.Models;
using SampleStoreCQRS.Domain.Core.Notifications;
using System.Collections.Generic;

namespace SampleStoreCQRS.Domain.Core.Handlers
{
    public class CommandHandler
    {
        private readonly IUnitOfWork _uow;
        private readonly IMediatorHandler _bus;
        private readonly DomainNotificationHandler _notifications;

        public CommandHandler(
                IUnitOfWork uow, 
                IMediatorHandler bus, 
                INotificationHandler<DomainNotification> notifications)
        {
            _uow = uow;
            _notifications = (DomainNotificationHandler)notifications;
            _bus = bus;
        }

        protected void NotifyValidationErrors(Command message)
        {
            foreach (var error in message.ValidationResult.Errors)
            {
                _bus.RaiseEvent(new DomainNotification(message.MessageType, error.ErrorMessage));
            }
        }

        protected void NotifyValidationErrors(Entity message)
        {
            foreach (var error in message.ValidationResult.Errors)
            {
                _bus.RaiseEvent(new DomainNotification(message.MessageType, error.ErrorMessage));
            }
        }

        protected void NotifyValidationError(DomainNotification notification)
        {
            _bus.RaiseEvent(notification);
        }

        protected void DisparchEvents(IReadOnlyCollection<Event> _events)
        {
            foreach (var _event in _events)
            {
                _bus.RaiseEvent(_event);
            }
        }

        public bool Commit()
        {
            if (_notifications.HasNotifications()) return false;
            if (_uow.Commit()) return true;

            _bus.RaiseEvent(new DomainNotification("Commit", "We had a problem during saving your data."));
            return false;
        }
    }
}
