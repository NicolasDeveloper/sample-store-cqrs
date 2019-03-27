using System.Linq;
using Microsoft.EntityFrameworkCore;
using SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Interfaces;
using SampleStoreCQRS.Domain.Contexts.Promotions.Models;
using SampleStoreCQRS.Domain.Core.ValueObjects;
using SampleStoreCQRS.Infra.Data.Contexts.Common.DataContext;
using SampleStoreCQRS.Infra.Data.Contexts.Common.Repositories;

namespace SampleStoreCQRS.Infra.Data.Contexts.Checkout.Orders.Repository
{
    public class DiscountCuponReaderRepository : Repository<Coupon> ,IDiscountCuponReaderRepository
    {
        public DiscountCuponReaderRepository(SampleStoreCQRSDataContext context) : base(context)
        {
        }

        public DiscountCupon GetCupon(string cod)
        {
            return DbSet.AsNoTracking().Where(x => x.Cod == cod).Select(x => new DiscountCupon(x.Cod, x.Percentage, x.ValidadePeriod)).FirstOrDefault();
        }
    }
}
