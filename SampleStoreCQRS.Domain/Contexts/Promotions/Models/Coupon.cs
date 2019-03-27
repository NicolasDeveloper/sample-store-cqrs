using SampleStoreCQRS.Domain.Core.Models;
using SampleStoreCQRS.Domain.Core.Validations.ValueObjects;
using SampleStoreCQRS.Domain.Core.ValueObjects;

namespace SampleStoreCQRS.Domain.Contexts.Promotions.Models
{
    public class Coupon : Aggregate
    {
        public virtual string Cod { get; protected set; }
        public virtual decimal Percentage { get; protected set; }
        public virtual Period ValidadePeriod { get; protected set; }

        protected Coupon() { }

        public Coupon(string cod, decimal percentage, Period validate)
        {
            Cod = cod;
            Percentage = percentage;
            ValidadePeriod = validate;

            ValidationResult = new CouponValidation().Validate(this);
        }

        public static Coupon NewCuponTo30Days(string cod, decimal percentage)
        {
            return new Coupon(cod, percentage, Period.Monthly);
        }

        public static Coupon NewCuponTo15Days(string cod, decimal percentage)
        {
            return new Coupon(cod, percentage, Period.Fortnightly);
        }

        public override bool IsValid()
        {
            return ValidationResult.IsValid;
        }
    }
}
