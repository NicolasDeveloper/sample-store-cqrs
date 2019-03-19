using System;
using SampleStoreCQRS.Domain.Core.Models;
using SampleStoreCQRS.Domain.Core.Validations.ValueObjects;

namespace SampleStoreCQRS.Domain.Core.ValueObjects
{
    public class Period : ValueObject<Period>
    {

        public DateTime Start { get; private set; }
        public DateTime End { get; private set; }

        protected Period(DateTime start, DateTime end)
        {
            Start = start;
            End = end;

            ValidationResult = new PeriodValidation().Validate(this);
        }

        public static Period Create(DateTime start, DateTime end) => new Period(start, end);

        public static Period Monthly => new Period(DateTime.Now, DateTime.Now.AddDays(30));

        public static Period Fortnightly => new Period(DateTime.Now, DateTime.Now.AddDays(15));

        public override bool IsValid()
        {
            return ValidationResult.IsValid;
        }

        protected override bool EqualsCore(Period other)
        {
            return Start == other.Start &&
                End == other.End;
        }

        protected override int GetHashCodeCore()
        {
            unchecked
            {
                int hashCode = Start.GetHashCode();
                hashCode = (hashCode * 397) ^ End.GetHashCode();

                return hashCode;
            }
        }
    }
}
