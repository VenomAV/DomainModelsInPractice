using System.Collections.Generic;
using app.domain;
using app.infrastructure;
using Xunit;

namespace test
{
    public abstract class AcceptanceTestBase
    {
        private readonly List<object> _receivedResponses = new List<object>();
        private readonly List<Event> _publishedEvent = new List<Event>();
        private readonly List<ReadModel> _readModels = new List<ReadModel>();
        
        protected InMemoryEventStore EventStore { get; } = new InMemoryEventStore(new Event[0]);

        protected void Given(params Event[] events)
        {
            EventStore.Reset(events);
            foreach (var @event in events)
                NotifyReadModels(@event);
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

        protected void Published(Event @event)
        {
            _publishedEvent.Add(@event);
            NotifyReadModels(@event);
        }

        private void NotifyReadModels(Event @event)
        {
            foreach (var readModel in _readModels)
                readModel.OnEvent(@event);
        }

        protected void RegisterReadModel(ReadModel readModel) => _readModels.Add(readModel);
    }
}