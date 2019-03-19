using FluentValidation;
using SampleStoreCQRS.Domain.Core.ValueObjects;

namespace SampleStoreCQRS.Domain.Core.Validations.ValueObjects
{
    public class EmailValidation: AbstractValidator<Email>
    {
        public EmailValidation()
        {
            RuleFor(x => x.Address).NotEmpty().WithMessage("Endereço não pode estar vazio");
            RuleFor(x => x.Address).EmailAddress().WithMessage("Endereço não pode estar vazio");
        }
    }
}
