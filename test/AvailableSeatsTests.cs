using System;
using System.Collections.Generic;
using app.domain;
using app.domain.screening;
using app.domain.screening.events;
using app.domain.screening.queries;
using Xunit;

namespace test
{
    public class AvailableSeatsTests
    {
        private Event[] _history = new Event[0];
        private readonly List<object> _receivedResponses = new List<object>();

        [Fact]
        public void AllSeatsAvailable()
        {
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
                    }),
                new SeatsReservationRequestedTooLate(screeningId, Guid.NewGuid(), new SeatId[0]));

            Query(new AvailableSeats(screeningId));

            ThenExpectResponses(new AvailableSeatsResponse(new[]
            {
                new SeatId("A", 2),
                new SeatId("A", 3),
                new SeatId("A", 4),
            }));
        }

        private void Given(params Event[] events)
        {
            _history = events;
        }

        private void Query(AvailableSeats query)
        {
            var readModel = new AvailableSeatsReadModel(_history);
            var handler = new AvailableSeatsQueryHandler(readModel, response => _receivedResponses.Add(response));

            handler.Handle(query);
        }

        private void ThenExpectResponses(params object[] responses)
        {
            Assert.Equal(responses, _receivedResponses);
        }
    }
}