using SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Models;
using System;
using System.Collections.Generic;

namespace SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Interfaces
{
    public interface IProductRepository
    {
        Product GetById(Guid id);
        ICollection<Product> GetById(Guid[] ids);
    }
}
