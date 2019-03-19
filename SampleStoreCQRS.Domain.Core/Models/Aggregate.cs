
using System.Linq;
using SampleStoreCQRS.Domain.Core.Events;
using System.Collections.Generic;

namespace SampleStoreCQRS.Domain.Core.Models
{
    public abstract class Aggregate : Entity
    {
        private IList<Event> _domainEvents = new List<Event>();
        public IReadOnlyCollection<Event> DomainEvents => _domainEvents.ToArray();

        protected void AddEvent(Event _event)
        {
            _domainEvents.Add(_event);
        } 
    }
}
