

using System;
using FluentValidation.Results;
using SampleStoreCQRS.Domain.Core.Events;

namespace SampleStoreCQRS.Domain.Core.Models
{
    public abstract class ValueObject<T>: Message where T : ValueObject<T>
    {
        public virtual DateTime Timestamp { get; protected set; }
        public virtual ValidationResult ValidationResult { get; protected set; }

        public ValueObject()
        {
            Timestamp = DateTime.Now;
        }

        public override bool Equals(object obj)
        {
            var valueObject = obj as T;
            return !ReferenceEquals(valueObject, null) && EqualsCore(valueObject);
        }

        protected abstract bool EqualsCore(T other);

        public abstract bool IsValid();

        public override int GetHashCode()
        {
            return GetHashCodeCore();
        }

        protected abstract int GetHashCodeCore();

        public static bool operator ==(ValueObject<T> a, ValueObject<T> b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(ValueObject<T> a, ValueObject<T> b)
        {
            return !(a == b);
        }
    }
}
