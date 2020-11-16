using System;
using System.Collections.Generic;
using System.Linq;

namespace test
{
    public class InMemoryEventStore : EventStore
    {
        public readonly List<Event> Events = new List<Event>();
        
        public Event[] EventsFor(Guid streamId)
        {
            return Events.Where(x => x.StreamId == streamId).ToArray();
        }

        public void Add(Event @event)
        {
            Events.Add(@event);
        }
    }
}