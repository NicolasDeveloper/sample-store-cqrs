using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;
using SampleStoreCQRS.Domain.Core.Events;
using SampleStoreCQRS.Domain.Core.Notifications;

namespace SampleStoreCQRS.Domain.Core.Models
{
    public abstract class Entity : Message
    {
        private IList<DomainNotification> _notifications = new List<DomainNotification>();

        public virtual Guid Id { get; protected set; }
        public virtual DateTime Timestamp { get; protected set; }
        public virtual ValidationResult ValidationResult { get; protected set; }
        public virtual IReadOnlyCollection<DomainNotification> Notifications => _notifications?.ToArray();
        public virtual bool HasNotifications => _notifications?.Count > 0;

        public Entity()
        {
            Id = Guid.NewGuid();
            AggregateId = Id;
            Timestamp = DateTime.Now;
        }

        public void AddNotification(string notification)
        {
            _notifications.Add(new DomainNotification(MessageType, notification));
        }

        public void AddNotifications(IReadOnlyCollection<DomainNotification> erros)
        {
            foreach(var error in erros)
            {
                _notifications.Add(error);
            }
        }

        public void AddValidationResults(ValidationResult validation)
        {
            foreach (var error in validation.Errors)
            {
                ValidationResult.Errors.Add(error);
            }
        }

        public abstract bool IsValid();

        public override bool Equals(object obj)
        {
            var compareTo = obj as Entity;

            if (ReferenceEquals(this, compareTo)) return true;
            if (ReferenceEquals(null, compareTo)) return false;

            return Id.Equals(compareTo.Id);
        }

        public static bool operator ==(Entity a, Entity b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(Entity a, Entity b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return (GetType().GetHashCode() * 907) + Id.GetHashCode();
        }

        public override string ToString()
        {
            return GetType().Name + " [Id=" + Id + "]";
        }
    }
}
