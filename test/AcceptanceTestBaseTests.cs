using System;
using System.Collections.Generic;
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
            RegisterReadModel(readModel);
            
            Given(history);
            
            Assert.Equal(history, readModel.ReceivedEvents);

        }

        [Fact]
        public void RegisteredReadModelsGetNotified()
        {
            var readModel = new SpyReadModel();
            var newEvent = new FakeEvent(Guid.NewGuid(), "Any");
            RegisterReadModel(readModel);
            Given();

            Published(newEvent);

            Assert.Equal(new[] {newEvent}, readModel.ReceivedEvents);
        }

        private class SpyReadModel : ReadModel
        {
            public List<Event> ReceivedEvents { get; } = new List<Event>();

            public void OnEvent(Event @event)
            {
                ReceivedEvents.Add(@event);
            }
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
    }
}