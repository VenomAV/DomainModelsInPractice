using System;
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
        private readonly Dictionary<Type, object> _handlers = new Dictionary<Type, object>();

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

        protected void Register(ReadModel readModel) => _readModels.Add(readModel);

        protected void Register<TMsg>(Handler<TMsg> handler) => _handlers[typeof(TMsg)] = handler;

        protected void Query<TMsg>(TMsg query) => CallHandlerFor(query);

        private void CallHandlerFor<TMsg>(TMsg message)
        {
            if (_handlers.ContainsKey(typeof(TMsg))) 
                ((Handler<TMsg>) _handlers[typeof(TMsg)]).Handle(message);
        }
    }
}