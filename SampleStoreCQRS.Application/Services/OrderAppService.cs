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


        public Task Place(PlaceOrderViewModel customerViewModel)
        {
            var placeCommand = _mapper.Map<PlaceOrderCommand>(customerViewModel);
            return Bus.SendCommand(placeCommand);
        }

        public Task Pay(PayOrderViewModel customerViewModel)
        {
            var placeCommand = _mapper.Map<PayOrderCommand>(customerViewModel);
            return Bus.SendCommand(placeCommand);
        }

        public Task Ship(ShipOrderViewModel customerViewModel)
        {
            var placeCommand = _mapper.Map<ShipOrderCommand>(customerViewModel);
            return Bus.SendCommand(placeCommand);
        }

        public Task Cancel(CancelOrderViewModel customerViewModel)
        {
            var placeCommand = _mapper.Map<CancelOrderCommand>(customerViewModel);
            return Bus.SendCommand(placeCommand);
        }
    }
}
