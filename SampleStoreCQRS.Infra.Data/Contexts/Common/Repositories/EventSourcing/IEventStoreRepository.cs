
using SampleStoreCQRS.Domain.Core.Events;
using System;
using System.Collections.Generic;

namespace SampleStoreCQRS.Infra.Data.Contexts.Common.Repositories.EventSourcing
{
    public interface IEventStoreRepository : IDisposable
    {
        void Store(StoredEvent theEvent);
        IList<StoredEvent> All(Guid aggregateId);
    }
}
