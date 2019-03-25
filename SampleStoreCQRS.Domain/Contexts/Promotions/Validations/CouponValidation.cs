using FluentValidation;
using SampleStoreCQRS.Domain.Contexts.Promotions.Models;


namespace SampleStoreCQRS.Domain.Core.Validations.ValueObjects
{
    public class CouponValidation : AbstractValidator<Coupon>
    {
        public CouponValidation()
        {
            RuleFor(x => x.Cod)
                .NotEmpty()
                .WithMessage("Informe o número do cupom");

            RuleFor(x => x.ValidadePeriod.IsValid())
                .NotEqual(false)
                .WithMessage("Informe um período válido");

            RuleFor(x => x.Percentage)
                .NotEmpty()
                .GreaterThan(0)
                .WithMessage("Informe uma porcentagem maior que 0");
        }
    }
}
