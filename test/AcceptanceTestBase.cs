using System.Collections.Generic;
using app.domain;
using Xunit;

namespace test
{
    public abstract class AcceptanceTestBase
    {
        private readonly List<object> _receivedResponses = new List<object>();
        private readonly List<Event> _publishedEvent = new List<Event>();
        protected Event[] History { get; private set; } = new Event[0];

        protected void Given(params Event[] events)
        {
            History = events;
        }

        protected void Then(params Event[] events)
        {
            Assert.Equal(events, _publishedEvent);
        }

        protected void ThenExpectResponses(params object[] responses)
        {
            Assert.Equal(responses, _receivedResponses);
        }

        protected void Respond(object response) => _receivedResponses.Add(response);

        protected void Published(Event @event) => _publishedEvent.Add(@event);
    }
}