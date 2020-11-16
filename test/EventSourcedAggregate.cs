using System.Collections.Generic;

namespace test
{
    public interface EventSourcedAggregate
    {
        IEnumerable<Event> UnpublishedEvents { get; }
    }
}