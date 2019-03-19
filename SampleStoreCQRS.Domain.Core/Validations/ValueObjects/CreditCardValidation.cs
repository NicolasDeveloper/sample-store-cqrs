using FluentValidation;
using SampleStoreCQRS.Domain.Core.ValueObjects;

namespace SampleStoreCQRS.Domain.Core.Validations.ValueObjects
{
    public class CreditCardValidation: AbstractValidator<CreditCard>
    {
        public CreditCardValidation()
        {
            RuleFor(x => x.Number)
                   .NotEmpty()
                   .CreditCard()
                   .WithMessage("Informe o numero do cartão de crédito");

            RuleFor(x => x.Cvv.ToString()).Length(3, 3).NotEmpty().WithMessage("Cvv inválido, deve conter 3 dígitos");
            RuleFor(x => x.Validate).NotEmpty().WithMessage("Informe uma data válida");
            RuleFor(x => x.PrintName).NotEmpty().WithMessage("Informe o nome impresso no cartão");
        }
    }
}
