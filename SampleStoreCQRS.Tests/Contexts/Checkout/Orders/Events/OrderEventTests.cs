using Microsoft.VisualStudio.TestTools.UnitTesting;
using SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Enuns;
using SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Events;
using SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Interfaces;
using SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Models;
using SampleStoreCQRS.Domain.Core.Events;
using SampleStoreCQRS.Domain.Core.ValueObjects;
using SampleStoreCQRS.Tests.Contexts.Checkout.Orders.Fakes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SampleStoreCQRS.Tests.Contexts.Checkout.Orders.Events
{
    [TestClass]
    public class OrderEventTests
    {

        public Guid[] _ids;
        public ICustomerReaderRepository _customerRepository;
        public IProductReaderRepository _productRepository;

        public ICollection<Product> _products;
        public Customer _customer;
        public Order _order;

        public OrderEventTests()
        {
            var _productFakeRepository = new ProductFakeRepository();
            _ids = _productFakeRepository.Take(3).Select(x => x.Id).ToArray();

            _customerRepository = new CustomerFakeRepository();
            _productRepository = _productFakeRepository;

            // data fake
            // vos
            var document = new Document("64074577003");
            var creditCard = new CreditCard("5361004355915434", 503, "16/06/2020", "Nicolas S. dos Santos");

            // models
            _customer = _customerRepository.GetByDocument(document);
            _products = _productRepository.GetById(_ids);
            _order = Order.Factory.Create(_customer, creditCard);
        }

        [TestMethod]
        public void ShouldCreatePlacedOrderEvent()
        {
            // add products in order
            _products.ToList().ForEach(x =>
            {
                _order.AddItem(x, 3);
            });

            // create an order
            _order.Place();
            var _event = _order.DomainEvents.Where(x => ConditionalStatus(x ,EOrderStatus.Created)).First();

            Assert.IsNotNull(_event);
            Assert.AreEqual(true, _order.IsValid());
        }

        [TestMethod]
        public void ShouldCreatePaidOrderEvent()
        {
            // add products in order
            _products.ToList().ForEach(x =>
            {
                _order.AddItem(x, 3);
            });

            // create an order
            _order.Place();

            // pay an order
            _order.Pay();
            
            var _event = _order.DomainEvents.Where(x => ConditionalStatus(x, EOrderStatus.Paid)).First();

            Assert.IsNotNull(_event);
            Assert.AreEqual(true, _order.IsValid());
        }

        [TestMethod]
        public void ShouldCreateShippedOrderEvent()
        {
            // add products in order
            _products.ToList().ForEach(x =>
            {
                _order.AddItem(x, 3);
            });

            // create an order
            _order.Place();

            // pay an order
            _order.Pay();

            // pay an order
            _order.Ship();

            var _event = _order.DomainEvents.Where(x => ConditionalStatus(x, EOrderStatus.Shipped)).First();

            Assert.IsNotNull(_event);
            Assert.AreEqual(true, _order.IsValid());
        }

        [TestMethod]
        public void ShouldCreateCancelledOrderEvent()
        {
            // add products in order
            _products.ToList().ForEach(x =>
            {
                _order.AddItem(x, 3);
            });

            // create an order
            _order.Place();

            // pay an order
            _order.Cancel();

            var _event = _order.DomainEvents.Where(x => ConditionalStatus(x, EOrderStatus.Canceled)).First();

            Assert.IsNotNull(_event);
            Assert.AreEqual(true, _order.IsValid());
        }

        private bool ConditionalStatus(Event x, EOrderStatus status)
        {
            if (x.MessageType == nameof(OrderStatusChangedEvent))
            {
                var e = x as OrderStatusChangedEvent;
                return e.Status == status;
            }

            return false;
        }
    }
}

