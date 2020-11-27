using app.domain;
using app.domain.screening.queries;
using Xunit;
using static test.SemanticalHelper;

namespace test
{
    public class CustomerReservationsTests : AcceptanceTestBase
    {
        public CustomerReservationsTests()
        {
            var readModel = new CustomerReservationsReadModel(EventStore);
            Register(readModel);
            Register(new CustomerReservationsQueryHandler(readModel, Respond));
        }
        
        [Fact]
        public void SingleReservation()
        {
            Given(ScreeningPlanned(
                    Screening1,
                    FarEnoughStartTime,
                    SeatA1, SeatA2, SeatA3, SeatA4),
                SeatsReserved(
                    Screening1,
                    Marco,
                    SeatA1));

            WhenQuery(CustomerReservations(Marco));

            ThenExpectResponse(CustomerReservationsResponse(Marco, Reservation(Screening1, SeatA1)));
        }
    }
}