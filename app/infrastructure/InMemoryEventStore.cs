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

        public IEnumerable<Event> History => _events;

        public Event[] EventsFor(Guid streamId)
        {
            return _events.Where(x => x.StreamId == streamId).ToArray();
        }

        public void Reset(IEnumerable<Event> history)
        {
            _events.Clear();
            _events.AddRange(history);
        }
    }
}