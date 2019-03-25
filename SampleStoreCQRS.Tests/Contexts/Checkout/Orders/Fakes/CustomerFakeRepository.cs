using System.Linq;
using System.Collections.Generic;
using SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Interfaces;
using SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Models;
using SampleStoreCQRS.Domain.Core.ValueObjects;
using System;
using SampleStoreCQRS.Tests.Contexts.Common.Repositories;

namespace SampleStoreCQRS.Tests.Contexts.Checkout.Orders.Fakes
{
    public class CustomerFakeRepository : RepositoryFake<Customer>, ICustomerReaderRepository
    {
        private IList<Customer> _list;

        public CustomerFakeRepository()
        {
            _list = new List<Customer>();

            var name1 = new Name("Nicolas", "silva dos santos");
            var name2 = new Name("Janderson", "silva dos santos");
            var name3 = new Name("Well", "silva dos santos");
            var name4 = new Name("Felipe", "silva dos santos");
            
            var document1 = new Document("94763001086");
            var document2 = new Document("90830590064");
            var document3 = new Document("64074577003");
            var document4 = new Document("71458318052");
            
            var email1 = new Email("abb2347837@mailboxy.fun");
            var email2 = new Email("a76b191e87@mailboxy.fun");
            var email3 = new Email("02734ad68e@mailboxy.fun");
            var email4 = new Email("0fa64e7efd@mailboxy.fun");

            _list.Add(new Customer(name1, "11964335056", email1, document1));
            _list.Add(new Customer(name2, "11964335056", email2, document2));
            _list.Add(new Customer(name3, "11964335056", email3, document3));
            _list.Add(new Customer(name4, "11964335056", email4, document4));
        }

        public Customer GetByDocument(Document document)
        {
            return _list.Where(x => x.Document == document).ToList()?.FirstOrDefault();
        }

        public Customer GetById(Guid id)
        {
            return _list.Where(x => x.Id == id).ToList()?.FirstOrDefault();
        }

        public IReadOnlyCollection<Customer> GetAll()
        {
            return _list.ToList();
        }
    }
}
