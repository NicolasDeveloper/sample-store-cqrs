﻿using FluentValidation;
using SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Models;

namespace SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Validations.Models
{
    public class PaymentValidation: AbstractValidator<Payment>
    {
        public PaymentValidation()
        {

        }
    }
}
