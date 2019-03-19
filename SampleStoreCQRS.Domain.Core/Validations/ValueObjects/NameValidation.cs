using FluentValidation;
using SampleStoreCQRS.Domain.Core.ValueObjects;

namespace SampleStoreCQRS.Domain.Core.Validations.ValueObjects
{
    public class NameValidation: AbstractValidator<Name>
    {
        public NameValidation()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithName("Nome não pode ser vazio");

            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage("Sobre nome não pode ser vazio")
                .WithMessage("O Nome deve conter de 4 á 40 caracteres");

            RuleFor(x => x.LastName)
                .MinimumLength(4)
                .MaximumLength(40)
                .WithMessage("O Sobrenome deve conter de 4 á 40 caracteres");
        }
    }
}
