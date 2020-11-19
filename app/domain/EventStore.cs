using System;
using System.Collections.Generic;

namespace app.domain
{
    public interface EventStore
    {
        IEnumerable<Event> History { get; }
        Event[] EventsFor(Guid streamId);
    }
}