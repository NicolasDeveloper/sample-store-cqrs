using FluentValidation;
using SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Commands.Inputs;
using System;

namespace SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Validations.Commands
{
    public abstract class OrderValidation<T> : AbstractValidator<T> where T : OrderCommand
    {
        protected void ValidateNumber()
        {
            RuleFor(c => c.Number)
                .NotEmpty().WithMessage("Informe o numero do pedido");
        }

        protected void ValidateCustomer()
        {
            RuleFor(c => c.CustomerId)
                .NotEmpty().WithMessage("Informe um cliente");
        }

        protected void ValidateOrderItems()
        {
            RuleFor(c => c.OrderItems)
                .NotEmpty().WithMessage("Informe os items");

            RuleFor(c => c.OrderItems.Count)
                .GreaterThan(0).WithMessage("Deve conter pelo menos 1 produto");
        }

        protected void ValidateCreditCard()
        {
            RuleFor(c => c.CreditCard.Number)
                .NotEmpty()
                .CreditCard()
                .WithMessage("Informe um número de cartão válido")
                .When(x => x.CreditCard != null);

            RuleFor(x => x.CreditCard.Cvv.ToString())
                .Length(3, 3)
                .NotEmpty()
                .WithMessage("Cvv inválido, deve conter 3 dígitos")
                .When(x => x.CreditCard != null);

            RuleFor(x => x.CreditCard.Validate)
                .NotEmpty()
                .WithMessage("Informe uma data válida")
                .When(x => x.CreditCard != null);

            RuleFor(x => x.CreditCard.PrintName)
                .NotEmpty()
                .WithMessage("Informe o nome impresso no cartão")
                .When(x => x.CreditCard != null);
        }
    }
}


