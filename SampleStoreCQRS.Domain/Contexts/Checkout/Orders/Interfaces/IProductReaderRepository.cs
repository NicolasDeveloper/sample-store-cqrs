using SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Models;
using SampleStoreCQRS.Domain.Core.Interfaces;
using SampleStoreCQRS.Domain.Core.ValueObjects;
using System;
using System.Collections.Generic;

namespace SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Interfaces
{
    public interface IProductReaderRepository: IRepository<Product>
    {
        Product GetById(Guid id);
        ICollection<Product> GetById(Guid[] ids);
    }
}
