using FluentValidation;
using SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Models;

namespace SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Validations.Models
{
    public class OrderItemValidation: AbstractValidator<OrderItem>
    {
        public OrderItemValidation()
        {
            RuleFor(x => x.Quantity)
                .LessThan(x => x.Product.QuantityOnHand)
                .WithMessage(x => {
                    return $"Produto {x.Product.Title} não tem {x.Product.QuantityOnHand} itens em estoque.";
                });
        }
    }
}
