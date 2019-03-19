

using System;
using SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Models;
using SampleStoreCQRS.Domain.Core.ValueObjects;

namespace SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Interfaces
{
    public interface ICustomerRepository
    {
        Customer GetByDocument(Document document);
        Customer GetById(Guid id);
    }
}
