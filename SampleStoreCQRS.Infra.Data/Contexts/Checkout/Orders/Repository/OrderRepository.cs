using Microsoft.EntityFrameworkCore;
using SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Interfaces;
using SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Models;
using SampleStoreCQRS.Infra.Data.Contexts.Common.DataContext;
using SampleStoreCQRS.Infra.Data.Contexts.Common.Repositories;
using System;
using System.Linq;

namespace SampleStoreCQRS.Infra.Data.Contexts.Checkout.Orders.Repository
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(SampleStoreCQRSDataContext context) : base(context)
        {
        }

        public Order GetByNumber(string number)
        {
            return DbSet.AsNoTracking().FirstOrDefault(c => c.Number == number);
        }
    }
}
