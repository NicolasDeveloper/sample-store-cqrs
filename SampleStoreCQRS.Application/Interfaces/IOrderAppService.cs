using SampleStoreCQRS.Application.ViewModels;
using System.Threading.Tasks;

namespace SampleStoreCQRS.Application.Interfaces
{
    public interface IOrderAppService
    {
        Task Place(PlaceOrderViewModel customerViewModel);
        Task Pay(PayOrderViewModel customerViewModel);
        Task Ship(ShipOrderViewModel customerViewModel);
        Task Cancel(CancelOrderViewModel customerViewModel);
    }
}
