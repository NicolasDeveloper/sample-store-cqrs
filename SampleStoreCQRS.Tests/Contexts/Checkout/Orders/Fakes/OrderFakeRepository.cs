using System.Collections.Generic;
using System.Linq;
using SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Interfaces;
using SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Models;
using SampleStoreCQRS.Domain.Core.ValueObjects;
using SampleStoreCQRS.Tests.Contexts.Common.Repositories;

namespace SampleStoreCQRS.Tests.Contexts.Checkout.Orders.Fakes
{
    public class OrderFakeRepository : RepositoryFake<Order>, IOrderRepository
    {
        private IReadOnlyCollection<Customer> _customers;
        private IReadOnlyCollection<Product> _products;
        private ICollection<Order> _orders;

        public OrderFakeRepository(IReadOnlyCollection<Customer> customers, IReadOnlyCollection<Product> products)
        {
            _customers = customers;
            _products = products;
            _orders = new List<Order>();

            var creditCard = new CreditCard("5361004355915434", 503, "16/06/2020", "Nicolas S. dos Santos");

            _customers.ToList().ForEach(x =>
            {
                var order = Order.Factory.Create(x, creditCard);

                products.ToList().ForEach(y =>
                {
                    order.AddItem(y, 3);
                });

                _orders.Add(order);
            });

        }

        public Order GetFirst()
        {
            return _orders.First();
        }

        public Order GetByNumber(string number)
        {
            return _orders.Where(x => x.Number == number).First();
        }
    }
}
