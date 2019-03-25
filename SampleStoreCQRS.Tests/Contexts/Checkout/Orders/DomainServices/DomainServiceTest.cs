using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SampleStoreCQRS.Domain.Contexts.Checkout.Orders.DomainServices;
using SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Models;
using SampleStoreCQRS.Domain.Core.Notifications;
using SampleStoreCQRS.Tests.Contexts.Checkout.Orders.Fakes;
using SampleStoreCQRS.Tests.Contexts.Fake;

namespace SampleStoreCQRS.Tests.Contexts.Checkout.Orders.DomainServices
{
    [TestClass]
    public class DomainServiceTest
    {
        private DiscountCuponDomainService _service;
        private Order _order;
        private DomainNotificationHandler _notifications;

        public DomainServiceTest()
        {
            var productRepository = new ProductFakeRepository();
            var customerRepository = new CustomerFakeRepository();
            var cuponDisountRepository = new DiscountCuponFakeReaderRepository();

            // dependencies 
            var notifications = new DomainNotificationHandler();
            var bus = new MediatorHandlerFake(notifications);

            var repository = new DiscountCuponFakeReaderRepository();
            var orderRepository = new OrderFakeRepository(customerRepository.GetAll(), productRepository.Take(10));

            _order = orderRepository.GetFirst();

            _notifications = notifications;
            _service = new DiscountCuponDomainService(bus, repository);
        }

        [TestMethod]
        public void ShouldCreateValidCupom()
        {
            _service.CalcDiscount("XPTO1", _order);
            Assert.AreEqual(_notifications.HasNotifications(), false);
        }

        [TestMethod]
        public void ShouldCreateInvalidCupom()
        {
            _service.CalcDiscount("XPTO", _order);
            Assert.AreEqual(_notifications.HasNotifications(), true);
        }

        [TestMethod]
        public void ShouldCreateExpiredCupom()
        {
            _service.CalcDiscount("XPTO8", _order);
            Assert.AreEqual(_notifications.HasNotifications(), true);
        }
    }
}
