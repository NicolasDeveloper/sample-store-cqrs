using System;
using System.Collections.Generic;
using FluentValidation.Results;
using SampleStoreCQRS.Domain.Core.Events;

namespace SampleStoreCQRS.Domain.Core.Models
{
    public abstract class Entity: Message
    {
        public Guid Id { get; protected set; }
        public DateTime Timestamp { get; private set; }
        public ValidationResult ValidationResult { get; set; }

        public Entity()
        {
            Id = Guid.NewGuid();
            Timestamp = DateTime.Now;
            AggregateId = Id;
        }

        public void AddNotification(string notification)
        {
            ValidationResult.Errors.Add(new ValidationFailure(MessageType, notification));
        }

        public void AddNotifications(IList<ValidationFailure> erros)
        {
            foreach(var error in erros)
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
