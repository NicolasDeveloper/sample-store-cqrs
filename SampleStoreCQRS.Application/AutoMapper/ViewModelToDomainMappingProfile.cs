using AutoMapper;
using SampleStoreCQRS.Application.ViewModels;
using SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Commands.Inputs;

namespace SampleStoreCQRS.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<OrderViewModel, PlaceOrderCommand>();
                //.ConstructUsing(c => new PlaceOrderCommand() {
                //    CreditCard = new CreditCardCommand {
                //       Cvv = c.CreditCard.Cvv,
                //       Number = c.CreditCard.Number,
                       
                //    },
                    
                //});
            //CreateMap<CustomerViewModel, UpdateCustomerCommand>()
            //    .ConstructUsing(c => new UpdateCustomerCommand(c.Id, c.Name, c.Email, c.BirthDate));
        }
    }
}
