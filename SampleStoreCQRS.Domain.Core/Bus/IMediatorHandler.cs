using System.Threading.Tasks;
using SampleStoreCQRS.Domain.Core.Commands;
using SampleStoreCQRS.Domain.Core.Events;

namespace SampleStoreCQRS.Domain.Core.Bus
{
    public interface IMediatorHandler
    {
        Task SendCommand<T>(T command) where T : Command;
        Task RaiseEvent<T>(T @event) where T : Event;
    }
}
