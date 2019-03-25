using AutoMapper;
using SampleStoreCQRS.Application.Interfaces;
using SampleStoreCQRS.Application.ViewModels;
using SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Commands.Inputs;
using SampleStoreCQRS.Domain.Core.Bus;
using System.Threading.Tasks;

namespace SampleStoreCQRS.Application.Services
{
    public class OrderAppService : IOrderAppService
    {
        private readonly IMapper _mapper;
        private readonly IMediatorHandler Bus;

        public OrderAppService(IMapper mapper,
                                  IMediatorHandler bus)
        {
            _mapper = mapper;
            Bus = bus;
        }

        public Task Register(OrderViewModel customerViewModel)
        {
            var placeCommand = _mapper.Map<PlaceOrderCommand>(customerViewModel);
            return Bus.SendCommand(placeCommand);
        }
    }
}
