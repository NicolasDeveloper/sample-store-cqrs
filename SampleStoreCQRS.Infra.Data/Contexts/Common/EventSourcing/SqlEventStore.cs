using Newtonsoft.Json;
using SampleStoreCQRS.Domain.Core.Events;
using SampleStoreCQRS.Infra.Data.Contexts.Common.Repositories.EventSourcing;

namespace SampleStoreCQRS.Infra.Data.Contexts.Common.EventSourcing
{
    public class SqlEventStore : IEventStore
    {
        private readonly IEventStoreRepository _eventStoreRepository;

        public SqlEventStore(IEventStoreRepository eventStoreRepository)
        {
            _eventStoreRepository = eventStoreRepository;
            //_user = user;
        }

        public void Save<T>(T theEvent) where T : Event
        {
            var serializedData = JsonConvert.SerializeObject(theEvent);

            var storedEvent = new StoredEvent(
                theEvent,
                serializedData,
                "system");

            _eventStoreRepository.Store(storedEvent);
        }
    }
}
