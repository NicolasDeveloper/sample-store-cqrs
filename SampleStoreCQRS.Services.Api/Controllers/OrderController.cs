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
        public async Task<IActionResult> Post([FromBody]OrderViewModel orderViewModel)
        {
            await _orderAppService.Register(orderViewModel);
            return Response(orderViewModel);
        }
    }
}
