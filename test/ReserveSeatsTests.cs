using System;
using System.Collections.Generic;
using app.domain;
using app.domain.screening;
using app.domain.screening.commands;
using app.domain.screening.events;
using app.infrastructure;
using Xunit;

namespace test
{
    public class ReserveSeatsTests
    {
        private readonly List<Event> _publishedEvent = new List<Event>();
        private Event[] _initialEvents = new Event[0];

        [Fact]
        public void AllSeatsAvailable()
        {
            var customerId = Guid.NewGuid();
            var screeningId = Guid.NewGuid();
            var farEnoughStartTime = DateTime.Now.AddDays(1);

            Given(new ScreeningCreated(
                screeningId,
                farEnoughStartTime,
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

        [Fact]
        public void SomeSeatsNotAvailable()
        {
            var customerId = Guid.NewGuid();
            var screeningId = Guid.NewGuid();
            var farEnoughStartTime = DateTime.Now.AddDays(1);

            Given(new ScreeningCreated(
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
            When(new ReserveSeatsCommand(
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

            Given(new ScreeningCreated(
                screeningId,
                tooNearStartTime,
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
            Then(new SeatsReservationRequestedTooLate(
                screeningId,
                customerId,
                new[]
                {
                    new SeatId("A", 1),
                    new SeatId("A", 2)
                }));
        }

        private void Given(params Event[] events)
        {
            _initialEvents = events;
        }

        private void When(ReserveSeatsCommand command)
        {
            var handler = new ReserveSeatsHandler(
                new InMemoryEventStore(_initialEvents),
                e => _publishedEvent.Add(e));
            handler.Handle(command);
        }

        private void Then(params Event[] events)
        {
            Assert.Equal(events, _publishedEvent);
        }
    }
}