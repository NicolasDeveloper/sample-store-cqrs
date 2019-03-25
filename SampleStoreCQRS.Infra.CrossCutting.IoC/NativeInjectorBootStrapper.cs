
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SampleStoreCQRS.Application.Interfaces;
using SampleStoreCQRS.Application.Services;
using SampleStoreCQRS.Domain.Contexts.Checkout.Orders.CommandHandlers;
using SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Commands.Inputs;
using SampleStoreCQRS.Domain.Contexts.Checkout.Orders.DomainServices;
using SampleStoreCQRS.Domain.Contexts.Checkout.Orders.EventHandlers;
using SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Events;
using SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Interfaces;
using SampleStoreCQRS.Domain.Core.Bus;
using SampleStoreCQRS.Domain.Core.Events;
using SampleStoreCQRS.Domain.Core.Interfaces;
using SampleStoreCQRS.Domain.Core.Notifications;
using SampleStoreCQRS.Infra.CrossCutting.Bus;
using SampleStoreCQRS.Infra.Data.Contexts.Checkout.Orders.Repository;
using SampleStoreCQRS.Infra.Data.Contexts.Common.DataContext;
using SampleStoreCQRS.Infra.Data.Contexts.Common.EventSourcing;
using SampleStoreCQRS.Infra.Data.Contexts.Common.Repositories.EventSourcing;
using SampleStoreCQRS.Infra.Data.Contexts.Common.UoW;

namespace SampleStoreCQRS.Infra.CrossCutting.IoC
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {

            // Domain Bus (Mediator)
            services.AddScoped<IMediatorHandler, InMemoryBus>();

            #region Order Context
            // Application
            services.AddScoped<IOrderAppService, OrderAppService>();
            
            // Domain - Events
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
            services.AddScoped<INotificationHandler<OrderStatusChangedEvent>, OrderEventHandler>();
            services.AddScoped<INotificationHandler<OrderPlacedEvent>, OrderEventHandler>();
            services.AddScoped<INotificationHandler<AppliedDiscountEvent>, OrderEventHandler>();

            // Domain - Commands
            services.AddScoped<IRequestHandler<PlaceOrderCommand, bool>, OrderCommandHandler>();
            services.AddScoped<IRequestHandler<PayOrderCommand, bool>, OrderCommandHandler>();
            services.AddScoped<IRequestHandler<ShipOrderCommand, bool>, OrderCommandHandler>();
            services.AddScoped<IRequestHandler<CancelOrderCommand, bool>, OrderCommandHandler>();

            // Domain - Services
            services.AddScoped<DiscountCuponDomainService>();
            
            // Infra - Data
            services.AddScoped<ICustomerReaderRepository, CustomerReaderRepository>();
            services.AddScoped<IDiscountCuponReaderRepository, DiscountCuponReaderRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IProductReaderRepository,    ProductReaderRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<SampleStoreCQRSDataContext>();
            #endregion

            // Infra - Event Data Source
            services.AddScoped<IEventStoreRepository, EventStoreSQLRepository>();
            services.AddScoped<IEventStore, SqlEventStore>();
        }
    }
}
