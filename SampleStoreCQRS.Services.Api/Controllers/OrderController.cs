using MediatR;
using Microsoft.AspNetCore.Mvc;
using SampleStoreCQRS.Application.Interfaces;
using SampleStoreCQRS.Application.ViewModels;
using SampleStoreCQRS.Domain.Core.Bus;
using SampleStoreCQRS.Domain.Core.Notifications;
using System.Threading.Tasks;

namespace SampleStoreCQRS.Services.Api.Controllers
{
    public class OrderController : ApiController
    {

        private readonly IOrderAppService _orderAppService;

        public OrderController(
            IOrderAppService orderAppService,
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediator) : base(notifications, mediator)
        {
            _orderAppService = orderAppService;
        }

        [HttpPost]
        [Route("checkout/orders")]
        public async Task<IActionResult> Post([FromBody]PlaceOrderViewModel orderViewModel)
        {
            await _orderAppService.Place(orderViewModel);
            return Response(orderViewModel);
        }

        [HttpPut]
        [Route("checkout/orders/payments")]
        public async Task<IActionResult> Pay([FromBody]PayOrderViewModel orderViewModel)
        {
            await _orderAppService.Pay(orderViewModel);
            return Response(orderViewModel);
        }

        [HttpPut]
        [Route("checkout/orders/deliveries")]
        public async Task<IActionResult> Ship([FromBody]ShipOrderViewModel orderViewModel)
        {
            await _orderAppService.Ship(orderViewModel);
            return Response(orderViewModel);
        }

        [HttpPut]
        [Route("checkout/orders/cancellations")]
        public async Task<IActionResult> Cancel([FromBody]CancelOrderViewModel orderViewModel)
        {
            await _orderAppService.Cancel(orderViewModel);
            return Response(orderViewModel);
        }
    }
}
