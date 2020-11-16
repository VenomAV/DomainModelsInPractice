using System;
using Xunit;

namespace test
{
    public class ReserveSeatsTests
    {
        [Fact]
        public void AllSeatsAvailable()
        {
            var screenings = new InMemoryScreenings();
            var handler = new ReserveSeatsHandler(screenings);
            var customerId = Guid.NewGuid();
            var screeningId = Guid.NewGuid();

            screenings.Save(new Screening(
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

            var screening = screenings.Get(screeningId);

            Assert.Equal(customerId, screening.Seat(new SeatId("A", 1)).ReservedBy());
            Assert.Equal(customerId, screening.Seat(new SeatId("A", 2)).ReservedBy());
        }
    }
}