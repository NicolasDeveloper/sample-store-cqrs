using System; 
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Interfaces;
using SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Models;
using SampleStoreCQRS.Domain.Core.ValueObjects;
using SampleStoreCQRS.Infra.Data.Contexts.Common.DataContext;
using SampleStoreCQRS.Infra.Data.Contexts.Common.Repositories;

namespace SampleStoreCQRS.Infra.Data.Contexts.Checkout.Orders.Repository
{
    public class CustomerReaderRepository : Repository<Customer>, ICustomerReaderRepository
    {
        public CustomerReaderRepository(SampleStoreCQRSDataContext context) : base(context)
        {
        }

        public Customer GetByDocument(Document document) {
            return DbSet.FirstOrDefault(x => x.Document == document);
        }

        public Customer GetById(Guid id)
        {
            return DbSet.FirstOrDefault(x => x.Id == id);
        }
    }
}
