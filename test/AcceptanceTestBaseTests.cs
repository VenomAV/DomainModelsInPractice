using System;
using System.Collections.Generic;
using System.Linq;
using app.domain;
using Xunit;

namespace test
{
    public class AcceptanceTestBaseTests : AcceptanceTestBase
    {
        [Fact]
        public void EventStoreHasInitialHistory()
        {
            var events = new Event[]
            {
                new FakeEvent(Guid.NewGuid(), "First"),
                new FakeEvent(Guid.NewGuid(), "Second"),
            };

            Given(events);

            Assert.Equal(events, EventStore.History);
        }

        [Fact]
        public void RegisteredReadModelsGetInitialHistory()
        {
            var readModel = new SpyReadModel();
            var history = new Event[]
            {
                new FakeEvent(Guid.NewGuid(), "First"),
                new FakeEvent(Guid.NewGuid(), "Second"),
            };
            Register(readModel);

            Given(history);

            Assert.Equal(history, readModel.ReceivedEvents);
        }

        [Fact]
        public void RegisteredReadModelsGetNotified()
        {
            var readModel = new SpyReadModel();
            var newEvent = new FakeEvent(Guid.NewGuid(), "Any");
            Register(readModel);
            Given();

            Published(newEvent);

            Assert.Equal(new[] {newEvent}, readModel.ReceivedEvents);
        }

        [Fact]
        public void DispatchQueryToRegisteredHandler()
        {
            var handler = new SpyHandler<FakeQuery>();
            var query = new FakeQuery("query");
            Register(handler);

            WhenQuery(query);

            Assert.Equal(new[] {query}, handler.ReceivedMessages);
        }

        [Fact]
        public void EventStoreGetsNewEvents()
        {
            var history = new Event[]
            {
                new FakeEvent(Guid.NewGuid(), "First"),
                new FakeEvent(Guid.NewGuid(), "Second"),
            };
            var newEvent = new FakeEvent(Guid.NewGuid(), "Any");
            Given(history);

            Published(newEvent);

            Assert.Equal(history.Concat(new []{newEvent}).ToArray(), EventStore.History);
        }

        private class SpyReadModel : ReadModel
        {
            public List<Event> ReceivedEvents { get; } = new List<Event>();
            public void OnEvent(Event @event) => ReceivedEvents.Add(@event);
        }

        private class SpyHandler<T> : Handler<T>
        {
            public List<T> ReceivedMessages { get; } = new List<T>();
            public void Handle(T message) => ReceivedMessages.Add(message);
        }

        private class FakeEvent : Event
        {
            public FakeEvent(Guid streamId, string message)
            {
                StreamId = streamId;
                Message = message;
            }

            public Guid StreamId { get; }
            public string Message { get; }

            protected bool Equals(FakeEvent other)
            {
                return StreamId.Equals(other.StreamId) && Message == other.Message;
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != this.GetType()) return false;
                return Equals((FakeEvent) obj);
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(StreamId, Message);
            }
        }

        private class FakeQuery
        {
            public FakeQuery(string query)
            {
                Query = query;
            }

            public string Query { get; }

            protected bool Equals(FakeQuery other)
            {
                return Query == other.Query;
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != this.GetType()) return false;
                return Equals((FakeQuery) obj);
            }

            public override int GetHashCode()
            {
                return (Query != null ? Query.GetHashCode() : 0);
            }
        }
    }
}