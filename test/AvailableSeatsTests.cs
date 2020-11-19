using System;
using app.domain.screening;
using app.domain.screening.queries;
using Xunit;
using static test.SemanticalHelper;

namespace test
{
    public class AvailableSeatsTests : AcceptanceTestBase
    {
        public AvailableSeatsTests()
        {
            var readModel = new AvailableSeatsReadModel(EventStore);
            Register(readModel);
            Register(new AvailableSeatsQueryHandler(readModel, Respond));
        }
        
        [Fact]
        public void AllSeatsAvailable()
        {
            Given(ScreeningPlanned(
                    Screening1,
                    FarEnoughStartTime,
                    SeatA1, SeatA2, SeatA3, SeatA4),
                SeatsReserved(
                    Screening1,
                    Guid.NewGuid(),
                    SeatA1),
                SeatsReservationRequestedTooLate(Screening1, Guid.NewGuid()));

            Query(AvailableSeats(Screening1));

            ThenExpectResponses(AvailableSeatsResponse(SeatA2, SeatA3, SeatA4));
        }
    }
}