using System;
using Xunit;

namespace test
{
    public class ReserveSeatsTests
    {
        [Fact]
        public void AllSeatsAvailable()
        {
            var eventStore = new InMemoryEventStore();
            var handler = new ReserveSeatsHandler(eventStore);
            var customerId = Guid.NewGuid();
            var screeningId = Guid.NewGuid();

            eventStore.Add(new ScreeningCreated(
                screeningId,
                new[]
                {
                    new Seat(new SeatId("A", 1)),
                    new Seat(new SeatId("A", 2)),
                    new Seat(new SeatId("A", 3)),
                    new Seat(new SeatId("A", 4)),
                }));

            var command = new ReserveSeatsCommand(
                screeningId,
                customerId,
                new[]
                {
                    new SeatId("A", 1),
                    new SeatId("A", 2)
                });

            handler.Handle(command);

            var screening = new ScreeningState(eventStore.EventsFor(screeningId));

            Assert.Equal(customerId, screening.Seat(new SeatId("A", 1)).ReservedBy());
            Assert.Equal(customerId, screening.Seat(new SeatId("A", 2)).ReservedBy());
        }
    }
}