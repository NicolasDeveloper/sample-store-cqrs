using AutoMapper;
using SampleStoreCQRS.Application.ViewModels;
using SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Commands.Inputs;

namespace SampleStoreCQRS.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<PlaceOrderViewModel, PlaceOrderCommand>();
            CreateMap<PayOrderViewModel, PayOrderCommand>();
            CreateMap<ShipOrderViewModel, ShipOrderCommand>();
            CreateMap<CancelOrderViewModel, CancelOrderCommand>();
        }
    }
}
