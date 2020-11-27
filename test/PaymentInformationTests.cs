using System;
using Xunit;
using static test.SemanticalHelper;

namespace test
{
    /*
    public class PaymentInformationTests : AcceptanceTestBase
    {
        public PaymentInformationTests()
        {
            var readModel = new PaymentInformationReadModel(EventStore);
            Register(readModel);
            Register(new PaymentInformationQueryHandler(readModel, Respond));
        }
        
        [Fact]
        public void SingleReservation()
        {
            var expirationDateTime = DateTime.Now;
            
            Given(ScreeningPlanned(
                    Screening1,
                    FarEnoughStartTime,
                    SeatA1, SeatA2, SeatA3, SeatA4),
                SeatsReserved(
                    Reservation1,
                    Screening1,
                    Marco,
                    SeatA1),
                ReservationTimeoutHasBeenSet(Reservation1, expirationDateTime),
                ReservationPriceCalculated(Reservation1, 20.Euro()));

            WhenQuery(CustomerReservations(Marco));

            ThenExpectResponse(PaymentInformationResponse(Marco, Reservation(Screening1, SeatA1), 20.Euro(), expirationDateTime));
        }
    }
    */
}