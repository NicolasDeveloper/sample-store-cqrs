using SampleStoreCQRS.Application.ViewModels;
using System.Threading.Tasks;

namespace SampleStoreCQRS.Application.Interfaces
{
    public interface IOrderAppService
    {
        Task Register(OrderViewModel customerViewModel);
    }
}
