using FluentValidation;
using SampleStoreCQRS.Domain.Core.ValueObjects;

namespace SampleStoreCQRS.Domain.Core.Validations.ValueObjects
{
    public class DiscountCouponValidation: AbstractValidator<DiscountCupon>
    {
        public DiscountCouponValidation()
        {
            RuleFor(x => x.Expired)
                .NotEqual(true)
                .WithMessage("Cupom expirado");
        }
    }
}
