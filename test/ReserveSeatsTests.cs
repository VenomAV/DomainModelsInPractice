using System;
using Xunit;

namespace test
{
    public class ReserveSeatsTests
    {
        private readonly InMemoryEventStore _eventStore = new InMemoryEventStore();

        [Fact]
        public void AllSeatsAvailable()
        {
            var customerId = Guid.NewGuid();
            var screeningId = Guid.NewGuid();

            Given(new ScreeningCreated(
                screeningId,
                new[]
                {
                    new SeatId("A", 1),
                    new SeatId("A", 2),
                    new SeatId("A", 3),
                    new SeatId("A", 4),
                }));
            When(new ReserveSeatsCommand(
                screeningId,
                customerId,
                new[]
                {
                    new SeatId("A", 1),
                    new SeatId("A", 2)
                }));
            Then(new SeatsReserved(screeningId, customerId, new[]
            {
                new SeatId("A", 1),
                new SeatId("A", 2)
            }));
        }

        private void Given(params Event[] events)
        {
            foreach (var @event in events)
            {
                _eventStore.Add(@event);
            }
        }

        private void When(ReserveSeatsCommand command)
        {
            var handler = new ReserveSeatsHandler(_eventStore);
            handler.Handle(command);
        }

        private void Then(params Event[] events)
        {
            foreach (var @event in events)
            {
                Assert.Contains(@event, _eventStore.Events);
            }
        }
    }
}