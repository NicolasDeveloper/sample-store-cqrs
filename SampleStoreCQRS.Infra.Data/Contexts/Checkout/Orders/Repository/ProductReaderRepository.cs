using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Interfaces;
using SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Models;
using SampleStoreCQRS.Infra.Data.Contexts.Common.DataContext;
using SampleStoreCQRS.Infra.Data.Contexts.Common.Repositories;

namespace SampleStoreCQRS.Infra.Data.Contexts.Checkout.Orders.Repository
{
    public class ProductReaderRepository : Repository<Product>, IProductReaderRepository
    {
        public ProductReaderRepository(SampleStoreCQRSDataContext context) : base(context)
        {
        }

        public Product GetById(Guid id)
        {
            return DbSet.AsNoTracking().FirstOrDefault(c => c.Id == id);
        }

        public ICollection<Product> GetById(Guid[] ids)
        {
            return DbSet.AsNoTracking().Where(c => ids.Contains(c.Id)).ToList();
        }
    }
}
