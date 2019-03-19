using System;
using System.Linq;
using System.Collections.Generic;
using SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Interfaces;
using SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Models;

namespace SampleStoreCQRS.Tests.Contexts.Checkout.Orders.Fakes
{
    public class ProductFakeRepository : IProductRepository
    {
        private IList<Product> _list;

        public ProductFakeRepository()
        {
            var product1 = new Product("mouse 1", "mouse 1 a laze", "mouse1.png", 30000, 20);
            var product2 = new Product("mouse 2", "mouse 2 a laze", "mouse2.png", 20000, 10);
            var product3 = new Product("mouse 3", "mouse 3 a laze", "mouse3.png", 10000, 30);
            var product4 = new Product("mouse 4", "mouse 4 a laze", "mouse4.png", 40000, 45);
            var product5 = new Product("mouse 5", "mouse 5 a laze", "mouse5.png", 45000, 61);
            var product6 = new Product("mouse 6", "mouse 6 a laze", "mouse6.png", 23000, 69);

            _list = new List<Product>();

            _list.Add(product1);
            _list.Add(product2);
            _list.Add(product3);
            _list.Add(product4);
            _list.Add(product5);
            _list.Add(product6);

        }

        public Product GetById(Guid id)
        {
            return _list.First();
        }

        public ICollection<Product> GetById(Guid[] ids)
        {
            return _list.Where(x => ids.Contains(x.Id)).ToList();
        }

        public List<Product> Take(int number)
        {
            return _list.Take(number).ToList();
        }
    }
}
