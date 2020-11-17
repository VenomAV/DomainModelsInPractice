using System;

namespace app.domain
{
    public interface EventStore
    {
        Event[] EventsFor(Guid streamId);
    }
}