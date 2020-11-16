using System;
using System.Collections.Generic;
using System.Linq;

namespace test
{
    public class InMemoryEventStore : EventStore
    {
        private List<Event> _events = new List<Event>();
        
        public Event[] EventsFor(Guid streamId)
        {
            return _events.Where(x => x.StreamId == streamId).ToArray();
        }

        public void Add(Event @event)
        {
            _events.Add(@event);
        }
    }
}