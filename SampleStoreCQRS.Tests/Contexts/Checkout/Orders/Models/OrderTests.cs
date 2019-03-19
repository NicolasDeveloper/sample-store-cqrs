using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Interfaces;
using SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Models;
using SampleStoreCQRS.Tests.Contexts.Checkout.Orders.Fakes;
using SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Enuns;
using SampleStoreCQRS.Domain.Core.ValueObjects;
using System.Collections.Generic;
using System.Linq;

namespace SampleStoreCQRS.Tests.Contexts.Checkout.Orders.Models
{
    [TestClass]
    public class OrderTests
    {
        public Guid[] _ids;
        public ICustomerRepository _customerRepository;
        public IProductRepository _productRepository;

        public ICollection<Product> _products;
        public Customer _customer;
        public Order _order;

        public OrderTests()
        {
            var _productFakeRepository = new ProductFakeRepository();
            _ids = _productFakeRepository.Take(3).Select(x => x.Id).ToArray();
            
            _customerRepository = new CustomerFakeRepository();
            _productRepository = _productFakeRepository;

            // data fake
            // vos
            var document = new Document("64074577003");

            // models
            var creditCard = new CreditCard("5361004355915434", 503, "16/06/2020", "Nicolas S. dos Santos");

            _customer = _customerRepository.GetByDocument(document);
            _products = _productRepository.GetById(_ids);
            _order = Order.Factory.Create(_customer, creditCard);
        }
        
        [TestMethod]
        public void ShouldCreateValidOrder()
        {
            // add products in order
            _products.ToList().ForEach(x =>
            {
                _order.AddItem(x, 3);
            });

            // create an order
            _order.Place();

            Assert.AreEqual(EOrderStatus.Created, _order.Status);
            Assert.AreEqual(true, _order.IsValid());
        }

        [TestMethod]
        public void ShouldCancelValidOrder()
        {
            // add products in order
            _products.ToList().ForEach(x =>
            {
                _order.AddItem(x, 3);
            });

            // create an order
            _order.Place();
            Assert.AreEqual(EOrderStatus.Created, _order.Status);

            // cancel an order
            _order.Cancel();
            Assert.AreEqual(EOrderStatus.Canceled, _order.Status);

            Assert.AreEqual(true, _order.IsValid());
        }

        [TestMethod]
        public void ShouldDeliveryValidOrder()
        {
            // add products in order
            _products.ToList().ForEach(x =>
            {
                _order.AddItem(x, 3);
            });

            // create an order
            _order.Place();
            Assert.AreEqual(EOrderStatus.Created, _order.Status);

            // proceed to payment
            _order.Pay();

            // ship an order
            _order.Ship();
            Assert.AreEqual(EOrderStatus.Shipped, _order.Status);

            Assert.AreEqual(true, _order.IsValid());
        }

        [TestMethod]
        public void ShouldPayValidOrder()
        {
            // add products in order
            _products.ToList().ForEach(x =>
            {
                _order.AddItem(x, 3);
            });

            // create an order
            _order.Place();
            Assert.AreEqual(EOrderStatus.Created, _order.Status);

            // proceed to payment
            _order.Pay();
            Assert.AreEqual(EOrderStatus.Paid, _order.Status);

            Assert.AreEqual(true, _order.IsValid());
        }

        [TestMethod]
        public void ShouldntCancelValidOrderWhenAlreadyDelivery()
        {
            // add products in order
            _products.ToList().ForEach(x =>
            {
                _order.AddItem(x, 3);
            });

            // create an order
            _order.Place();
            Assert.AreEqual(EOrderStatus.Created, _order.Status);

            // proceed to payment
            _order.Pay();

            // ship an order
            _order.Ship();
            _order.Cancel();

            Assert.AreEqual(EOrderStatus.Shipped, _order.Status);

            Assert.AreEqual(true, !_order.IsValid());
        }


        [TestMethod]
        public void ShouldntCancelInvalidOrderWhenAlreadyCancelled()
        {
            // add products in order
            _products.ToList().ForEach(x =>
            {
                _order.AddItem(x, 3);
            });

            // create an order
            _order.Place();
            Assert.AreEqual(EOrderStatus.Created, _order.Status);
            
            _order.Cancel();

            // proceed to payment
            _order.Pay();

            Assert.AreEqual(EOrderStatus.Canceled, _order.Status);

            Assert.AreEqual(true, !_order.IsValid());
        }


        [TestMethod]
        public void ShouldntAddProductInvalidOrder()
        {
            // add products in order
            _products.ToList().ForEach(x =>
            {
                _order.AddItem(x, 100);
            });

            // create an order
            _order.Place();
            Assert.AreEqual(EOrderStatus.Created, _order.Status);

            // ship an order
            _order.Ship();

            Assert.AreEqual(_order.Items.Count, 0);

            Assert.AreEqual(true, !_order.IsValid());
        }

        [TestMethod]
        public void ShouldApplyTenPercentageOfDisountValidOrder()
        {
            // add products in order
            _products.ToList().ForEach(x =>
            {
                _order.AddItem(x, 5);
            });

            // create a cupon
            var cupom = new DiscountCupon("DESCONTOTOTAL", 10, Period.Fortnightly);

            // create an order
            _order.Place();

            _order.ApplyDiscount(cupom);

            Assert.AreEqual(_order.TotalWithDicount, _order.Total - (_order.Total * 10 / 100));
            Assert.AreEqual(EOrderStatus.Created, _order.Status);
            Assert.AreEqual(true, _order.IsValid());
        }

    }
}
