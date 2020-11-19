using System;
using app.domain.screening;
using app.domain.screening.commands;
using app.domain.screening.events;
using Xunit;

namespace test
{
    public class ReserveSeatsTests : AcceptanceTestBase
    {
        public ReserveSeatsTests()
        {
            Register(new ReserveSeatsHandler(EventStore, Published));
        }
        
        [Fact]
        public void AllSeatsAvailable()
        {
            var customerId = Guid.NewGuid();
            var screeningId = Guid.NewGuid();
            var farEnoughStartTime = DateTime.Now.AddDays(1);

            Given(new ScreeningPlanned(
                screeningId,
                farEnoughStartTime,
                new[]
                {
                    new SeatId("A", 1),
                    new SeatId("A", 2),
                    new SeatId("A", 3),
                    new SeatId("A", 4),
                }));
            When(new ReserveSeats(
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

        [Fact]
        public void SomeSeatsNotAvailable()
        {
            var customerId = Guid.NewGuid();
            var screeningId = Guid.NewGuid();
            var farEnoughStartTime = DateTime.Now.AddDays(1);

            Given(new ScreeningPlanned(
                    screeningId,
                    farEnoughStartTime,
                    new[]
                    {
                        new SeatId("A", 1),
                        new SeatId("A", 2),
                        new SeatId("A", 3),
                        new SeatId("A", 4),
                    }),
                new SeatsReserved(
                    screeningId,
                    Guid.NewGuid(),
                    new[]
                    {
                        new SeatId("A", 1)
                    }));
            When(new ReserveSeats(
                screeningId,
                customerId,
                new[]
                {
                    new SeatId("A", 1),
                    new SeatId("A", 2)
                }));
            Then(new SeatsReservationFailed(
                screeningId,
                customerId,
                new[]
                {
                    new SeatId("A", 1)
                }));
        }

        [Fact]
        public void LessThan15MinutesBeforeScreeningTime()
        {
            var customerId = Guid.NewGuid();
            var screeningId = Guid.NewGuid();
            var tooNearStartTime = DateTime.Now.AddMinutes(10);

            Given(new ScreeningPlanned(
                screeningId,
                tooNearStartTime,
                new[]
                {
                    new SeatId("A", 1),
                    new SeatId("A", 2),
                    new SeatId("A", 3),
                    new SeatId("A", 4),
                }));
            When(new ReserveSeats(
                screeningId,
                customerId,
                new[]
                {
                    new SeatId("A", 1),
                    new SeatId("A", 2)
                }));
            Then(new SeatsReservationRequestedTooLate(
                screeningId,
                customerId,
                new[]
                {
                    new SeatId("A", 1),
                    new SeatId("A", 2)
                }));
        }
    }
}