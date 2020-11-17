using System;
using app.domain.screening;
using app.domain.screening.events;
using app.domain.screening.queries;
using Xunit;

namespace test
{
    public class AvailableSeatsTests : AcceptanceTestBase
    {
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

        private void Query(AvailableSeats query)
        {
            var readModel = new AvailableSeatsReadModel(History);
            var handler = new AvailableSeatsQueryHandler(readModel, Respond);

            handler.Handle(query);
        }
    }
}