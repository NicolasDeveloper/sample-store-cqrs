

using System;
using SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Models;
using SampleStoreCQRS.Domain.Core.Interfaces;
using SampleStoreCQRS.Domain.Core.ValueObjects;

namespace SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Interfaces
{
    public interface ICustomerReaderRepository: IRepository<Customer>
    {
        Customer GetByDocument(Document document);
        Customer GetById(Guid id);
    }
}
