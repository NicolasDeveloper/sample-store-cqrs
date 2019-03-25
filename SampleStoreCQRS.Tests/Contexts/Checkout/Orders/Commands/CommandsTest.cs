using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SampleStoreCQRS.Domain.Contexts.Checkout.Orders.CommandHandlers;
using SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Commands.Inputs;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using SampleStoreCQRS.Domain.Contexts.Checkout.Orders.DomainServices;
using SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Enuns;
using SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Interfaces;
using SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Models;
using SampleStoreCQRS.Domain.Core.Notifications;
using SampleStoreCQRS.Domain.Core.ValueObjects;
using SampleStoreCQRS.Tests.Contexts.Checkout.Orders.Fakes;
using SampleStoreCQRS.Tests.Contexts.Fake;
using System.Collections.Generic;

namespace SampleStoreCQRS.Tests.Contexts.Checkout.Orders.Commands
{
    [TestClass]
    public class CommandsTest
    {
        private OrderCommandHandler _commandHanddler;

        private IOrderRepository _orderRepository;
        private IProductReaderRepository _productRepository;
        private ICustomerReaderRepository _customerRepository;
        private IDiscountCuponReaderRepository _cuponDisountRepository;

        private Customer _customer;
        private List<Product> _products;
        private Order _order;
        private CreditCardCommand _creditCardCommand;

        public CommandsTest()
        {
            // repositories
            var productRepository = new ProductFakeRepository();
            var customerRepository = new CustomerFakeRepository();
            var cuponDisountRepository = new DiscountCuponFakeReaderRepository();

            _products = productRepository.Take(10);
            _customer = customerRepository.GetByDocument(new Document("94763001086"));
            _creditCardCommand = new CreditCardCommand()
            {
                Cvv = 503,
                Number = "5361004355915434",
                PrintName = "Nicolas S. dos Santos",
                Validate = "16/06/2020",
            };

            var orderRepository = new OrderFakeRepository(customerRepository.GetAll(), _products);

            // mock order
            _order = orderRepository.GetFirst();

            // dependencies 
            var notifications = new DomainNotificationHandler();
            var uow = new UnitOfWorkFake();
            var bus = new MediatorHandlerFake(notifications);

            // tests global dependencies 
            _customerRepository = customerRepository;
            _productRepository = productRepository;
            _orderRepository = orderRepository;
            _cuponDisountRepository = cuponDisountRepository;

            // domain services
            var discountCuponDomainService = new DiscountCuponDomainService(bus, _cuponDisountRepository);

            _commandHanddler = new OrderCommandHandler(
                    discountCuponDomainService,
                    _orderRepository,
                    _productRepository,
                    _customerRepository,
                    uow,
                    bus,
                    notifications);
        }

        [TestMethod]
        public async Task ShoudPlaceValidOrder()
        {
            var orderItems = new List<OrderItemCommand>();

            _products.ForEach(x => { orderItems.Add(new OrderItemCommand { Product = x.Id, Quantity = 4 }); });

            var result = await _commandHanddler.Handle(new PlaceOrderCommand
            {
                CustomerId = _customer.Id,
                OrderItems = orderItems,
                CreditCard = _creditCardCommand,
                DiscountCupon = "XPTO1",
            }, CancellationToken.None);

            Assert.AreEqual(result, true);
        }

        [TestMethod]
        public async Task ShoudPlaceInvalidCuponOrder()
        {
            var orderItems = new List<OrderItemCommand>();

            _products.ForEach(x => { orderItems.Add(new OrderItemCommand { Product = x.Id, Quantity = 4 }); });

            var result = await _commandHanddler.Handle(new PlaceOrderCommand
            {
                CustomerId = _customer.Id,
                OrderItems = orderItems,
                CreditCard = _creditCardCommand,
                DiscountCupon = "XPTO",
            }, CancellationToken.None);

            Assert.AreEqual(result, false);
        }

        [TestMethod]
        public async Task ShoudPayValidOrder()
        {
            // change order state 
            _order.Place();

            var result = await _commandHanddler.Handle(new PayOrderCommand
            {
                Number = _order.Number,
            }, CancellationToken.None);

            Assert.AreEqual(result, true);
            Assert.AreEqual(EOrderStatus.Paid, _order.Status);
        }

        [TestMethod]
        public async Task ShoudDeliveryValidOrder()
        {
            // change order state 
            _order.Place();

            // pay order
            _order.Pay();

            var result = await _commandHanddler.Handle(new ShipOrderCommand
            {
                Number = _order.Number,
            }, CancellationToken.None);

            Assert.AreEqual(result, true);
            Assert.AreEqual(EOrderStatus.Shipped, _order.Status);
        }

        [TestMethod]
        public async Task ShoudCancelValidOrder()
        {
            // change order state 
            _order.Place();
            
            var result = await _commandHanddler.Handle(new CancelOrderCommand
            {
                Number = _order.Number,
            }, CancellationToken.None);

            Assert.AreEqual(result, true);
            Assert.AreEqual(EOrderStatus.Canceled, _order.Status);
        }

        [TestMethod]
        public async Task ShoudntCancelWhenAlreadyShippedInvalidOrder()
        {
            // change order state 
            _order.Place();

            // pay order
            _order.Pay();

            // delivery order
            _order.Ship();

            var result = await _commandHanddler.Handle(new CancelOrderCommand
            {
                Number = _order.Number,
            }, CancellationToken.None);

            Assert.AreEqual(result, false);
            Assert.AreEqual(EOrderStatus.Shipped, _order.Status);
        }

        [TestMethod]
        public async Task ShoudntShipWhenAlreadyCanceledInvalidOrder()
        {
            // change order state 
            _order.Place();

            // pay order
            _order.Pay();

            // delivery order
            _order.Cancel();

            var result = await _commandHanddler.Handle(new ShipOrderCommand
            {
                Number = _order.Number,
            }, CancellationToken.None);

            Assert.AreEqual(result, false);
            Assert.AreEqual(EOrderStatus.Canceled, _order.Status);
        }

        [TestMethod]
        public async Task ShoudntShipWhenDoesntPaidInvalidOrder()
        {
            // change order state 
            _order.Place();
            
            var result = await _commandHanddler.Handle(new ShipOrderCommand
            {
                Number = _order.Number,
            }, CancellationToken.None);

            Assert.AreEqual(result, false);
            Assert.AreEqual(EOrderStatus.Created, _order.Status);
        }

        [TestMethod]
        public async Task ShoudntPlaceWhenDoesntHaveAnyItemInvalidOrder()
        {
            
            var result = await _commandHanddler.Handle(new PlaceOrderCommand
            {
                CustomerId = _customer.Id,
            }, CancellationToken.None);

            Assert.AreEqual(result, false);
        }

        [TestMethod]
        public async Task ShoudntPlaceWhenHaveValidItemInvalidOrder()
        {

            var result = await _commandHanddler.Handle(new PlaceOrderCommand
            {
                CustomerId = _customer.Id,
                OrderItems = new List<OrderItemCommand>()
                {
                    new OrderItemCommand()
                    {
                        Product = _products.First().Id,
                        Quantity = 3,
                    },
                    new OrderItemCommand()
                    {
                        Product = Guid.NewGuid(),
                        Quantity = 3,
                    },
                    new OrderItemCommand()
                    {
                        Product = Guid.NewGuid(),
                        Quantity = 3,
                    }
                },
                CreditCard = _creditCardCommand
            }, CancellationToken.None);

            Assert.AreEqual(result, false);
        }
    }
}
