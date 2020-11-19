using app.domain.screening;
using app.domain.screening.commands;
using app.domain.screening.queries;
using Xunit;
using static test.SemanticalHelper;

namespace test
{
    public class IntegrationTests : AcceptanceTestBase
    {
        public IntegrationTests()
        {
            var readModel = new AvailableSeatsReadModel(EventStore);
            Register(readModel);
            Register(new AvailableSeatsQueryHandler(readModel, Respond));
            Register(new ReserveSeatsHandler(EventStore, Published));
        }

        [Fact]
        public void OverallProcess()
        {
            Given(ScreeningPlanned(
                Screening1,
                FarEnoughStartTime,
                SeatA1, SeatA2, SeatA3, SeatA4));
            
            When(ReserveSeats(Screening1, Andrea, SeatA1));
            WhenQuery(AvailableSeats(Screening1));
            ThenExpectResponse(AvailableSeatsResponse(Screening1, SeatA2, SeatA3, SeatA4));
            
            When(ReserveSeats(Screening1, Marco, SeatA3));
            WhenQuery(AvailableSeats(Screening1));
            ThenExpectResponse(AvailableSeatsResponse(Screening1, SeatA2, SeatA4));
            
            When(ReserveSeats(Screening1, Xin, SeatA1));
            WhenQuery(AvailableSeats(Screening1));
            ThenExpectResponse(AvailableSeatsResponse(Screening1, SeatA2, SeatA4));
        }
    }
}