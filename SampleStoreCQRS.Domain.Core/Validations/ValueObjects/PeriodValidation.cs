using FluentValidation;
using SampleStoreCQRS.Domain.Core.ValueObjects;
using System;

namespace SampleStoreCQRS.Domain.Core.Validations.ValueObjects
{
    public class PeriodValidation: AbstractValidator<Period>
    {
        public PeriodValidation()
        {
            RuleFor(x => x.Start)
                .NotEqual(DateTime.MinValue)
                .WithMessage("O Período Inicial é obrigatório");

            RuleFor(x => x.End)
                .NotEqual(DateTime.MinValue)
                .WithMessage("O Período Final é obrigatório");

            RuleFor(x => x.End)
                .GreaterThan(x => x.Start)
                .WithMessage("O Período Inicial não pode ser maior que o Período Final");
        }
    }
}
