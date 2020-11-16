using System;

namespace test
{
    public interface EventStore
    {
        Event[] EventsFor(Guid streamId);
        void Add(Event @event);
    }
}