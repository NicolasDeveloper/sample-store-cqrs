﻿using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Commands.Inputs;
using SampleStoreCQRS.Domain.Contexts.Checkout.Orders.DomainServices;
using SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Interfaces;
using SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Models;
using SampleStoreCQRS.Domain.Core.Bus;
using SampleStoreCQRS.Domain.Core.Handlers;
using SampleStoreCQRS.Domain.Core.Interfaces;
using SampleStoreCQRS.Domain.Core.Notifications;
using SampleStoreCQRS.Domain.Core.ValueObjects;

namespace SampleStoreCQRS.Domain.Contexts.Checkout.Orders.CommandHandlers
{
    public class OrderCommandHandler : CommandHandler,
        IRequestHandler<PlaceOrderCommand, bool>,
        IRequestHandler<PayOrderCommand, bool>,
        IRequestHandler<ShipOrderCommand, bool>,
        IRequestHandler<CancelOrderCommand, bool>
    {

        private readonly IMediatorHandler Bus;

        private IProductRepository _productRepository;
        private ICustomerRepository _customerRepository;
        private IOrderRepository _orderRepository;
        private DiscountCuponDomainService _cupomService;

        public OrderCommandHandler(
                                    DiscountCuponDomainService cupomService,
                                    IOrderRepository orderRepository,
                                    IProductRepository productRepository,
                                    ICustomerRepository customerRepository,
                                    IUnitOfWork uow,
                                    IMediatorHandler bus,
                                    INotificationHandler<DomainNotification> notifications) : base(uow, bus, notifications)
        {
            
            Bus = bus;
            _productRepository = productRepository;
            _customerRepository = customerRepository;
            _orderRepository = orderRepository;
            _cupomService = cupomService;
        }

        public Task<bool> Handle(PlaceOrderCommand message, CancellationToken cancellationToken)
        {
            if(!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(false);
            }

            // search the clinte on database
            var customer = _customerRepository.GetById(message.CustomerId);

            if(customer == null)
            {
                NotifyValidationError(new DomainNotification(message.MessageType, $"Cliente com o id {message.CustomerId} não foi localizado"));
                return Task.FromResult(false);
            }

            // get range of products
            var products = _productRepository.GetById(message.OrderItems.Select(x => x.Product).ToArray())?.ToList();

            if (products == null || products.Count == 0)
            {
                NotifyValidationError(new DomainNotification(message.MessageType, $"nenhum produto foi encontrado"));
                return Task.FromResult(false);
            }

            // create an order
            var order = Order.Factory.Create(customer, new CreditCard(message.CreditCard.Number, message.CreditCard.Cvv, message.CreditCard.Validate, message.CreditCard.PrintName));

            // verify if some product wasn't found
            var ids = products.Select(x => x.Id).ToArray();
            message.OrderItems.ToList().ForEach((x) =>
            {
                if(ids.Contains(x.Product))
                {
                    var product = products.Where(y => y.Id == x.Product)?.First();
                    order.AddItem(product, x.Quantity);
                } 
                else
                {
                    NotifyValidationError(new DomainNotification(message.MessageType, $"produto {x.Product} não foi encontrado"));
                }
            });
            
            // place order 
            order.Place();

            // apply discount if has in command
            if(message.DiscountCupon !=  null)
            {
                order = _cupomService.CalcDiscount(message.DiscountCupon, order);
            }

            // if has notifications then notify the application
            NotifyValidationErrors(order);

            // save a order 
            _orderRepository.Save(order);

            // if already it´s ok then disparch all events
            if(Commit())
            {
                DisparchEvents(order.DomainEvents);
            } else
            {
                NotifyValidationError(new DomainNotification(message.MessageType, "houve algum problema ao criar o pedido"));
                return Task.FromResult(false);
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(PayOrderCommand message, CancellationToken cancellationToken)
        {

            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(false);
            }

            var order = _orderRepository.GetByNumber(message.Number);

            if (order == null)
            {
                NotifyValidationError(new DomainNotification(message.MessageType, $"pedido não encontrado"));
                return Task.FromResult(false);
            }

            // pay order
            order.Pay();

            // if has notifications then notify the application
            NotifyValidationErrors(order);

            _orderRepository.Update(order);

            // if already it´s ok then disparch all events
            if (Commit())
            {
                DisparchEvents(order.DomainEvents);
            } else
            {
                NotifyValidationError(new DomainNotification(message.MessageType, "houve algum problema ao salvar status do pedido"));
                return Task.FromResult(false);
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(ShipOrderCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(false);
            }

            var order = _orderRepository.GetByNumber(message.Number);

            if (order == null)
            {
                NotifyValidationError(new DomainNotification(message.MessageType, $"pedido não encontrado"));
                return Task.FromResult(false);
            }

            // ship order
            order.Ship();

            // if has notifications then notify the application
            NotifyValidationErrors(order);

            _orderRepository.Update(order);

            // if already it´s ok then disparch all events
            if (Commit())
            {
                DisparchEvents(order.DomainEvents);
            } else
            {
                NotifyValidationError(new DomainNotification(message.MessageType, "houve algum problema ao salvar status do pedido"));
                return Task.FromResult(false);
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(CancelOrderCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(false);
            }

            var order = _orderRepository.GetByNumber(message.Number);

            if (order == null)
            {
                NotifyValidationError(new DomainNotification(message.MessageType, $"pedido não encontrado"));
                return Task.FromResult(false);
            }

            // cancel order
            order.Cancel();

            // if has notifications then notify the application
            NotifyValidationErrors(order);

            _orderRepository.Update(order);

            // if already it´s ok then disparch all events
            if (Commit())
            {
                DisparchEvents(order.DomainEvents);
            } else
            {
                NotifyValidationError(new DomainNotification(message.MessageType, "houve algum problema ao salvar status do pedido"));
                return Task.FromResult(false);
            }

            return Task.FromResult(true);
        }
    }
}
