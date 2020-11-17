using System;

namespace app.domain
{
    public interface Event
    {
        public Guid StreamId { get; }
    }
}