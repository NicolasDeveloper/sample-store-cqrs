

using System;
using System.Collections.Generic;
using SampleStoreCQRS.Domain.Core.Events;

namespace SampleStoreCQRS.Infra.Data.Contexts.Common.Repositories.EventSourcing
{
    public class EventStoreSQLRepository : IEventStoreRepository
    {
        public EventStoreSQLRepository()
        {
            
        }

        public IList<StoredEvent> All(Guid aggregateId)
        {
            return null;
            //return (from e in _context.StoredEvent where e.AggregateId == aggregateId select e).ToList();
        }

        public void Store(StoredEvent theEvent)
        {
            //_context.StoredEvent.Add(theEvent);
            //_context.SaveChanges();
        }

        public void Dispose()
        {
            //_context.Dispose();
        }
    }
}
