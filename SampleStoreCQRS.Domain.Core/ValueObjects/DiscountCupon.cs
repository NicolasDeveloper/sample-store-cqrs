using SampleStoreCQRS.Domain.Core.Models;
using SampleStoreCQRS.Domain.Core.Validations.ValueObjects;
using System;

namespace SampleStoreCQRS.Domain.Core.ValueObjects
{
    public class DiscountCupon : ValueObject<DiscountCupon>
    {
        public virtual string Cod { get; protected set; }
        public virtual decimal Percentage { get; protected set; }
        public virtual Period ValidadePeriod { get; protected set; }
        public virtual bool Expired => ValidadePeriod.Start < DateTime.Now && ValidadePeriod.End < DateTime.Now;

        protected DiscountCupon() {
            
        }

        public DiscountCupon(string cod, decimal percentage, Period validate)
        {
            Cod = cod;
            Percentage = percentage;
            ValidadePeriod = validate;
        }

        public override bool IsValid()
        {
            ValidationResult = new DiscountCouponValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        protected override bool EqualsCore(DiscountCupon other)
        {
            return Cod == other.Cod
                && Percentage == other.Percentage
                && ValidadePeriod == other.ValidadePeriod;
        }

        protected override int GetHashCodeCore()
        {
            unchecked
            {
                int hashCode = Cod.GetHashCode();
                hashCode = (hashCode * 397) ^ Percentage.GetHashCode();
                hashCode = (hashCode * 397) ^ ValidadePeriod.GetHashCode();

                return hashCode;
            }
        }
    }
}
