using System;
using System.Collections.Generic;
using System.Linq;
using app.domain;

namespace app.infrastructure
{
    public class InMemoryEventStore : EventStore
    {
        private readonly List<Event> _events;

        public InMemoryEventStore(Event[] events)
        {
            _events = events.ToList();
        }

        public Event[] EventsFor(Guid streamId)
        {
            return _events.Where(x => x.StreamId == streamId).ToArray();
        }
    }
}