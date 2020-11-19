using app.domain.screening;
using app.domain.screening.commands;
using Xunit;
using static test.SemanticalHelper;

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
            Given(ScreeningPlanned(
                Screening1,
                FarEnoughStartTime,
                SeatA1, SeatA2, SeatA3, SeatA4));
            When(ReserveSeats(
                Screening1,
                Marco,
                SeatA1, new SeatId("A", 2)));
            Then(SeatsReserved(
                Screening1,
                Marco,
                SeatA1, new SeatId("A", 2)));
        }

        [Fact]
        public void SomeSeatsNotAvailable()
        {
            Given(ScreeningPlanned(
                    Screening1,
                    FarEnoughStartTime,
                    SeatA1, SeatA2, SeatA3, SeatA4),
                SeatsReserved(
                    Screening1,
                    Andrea,
                    new SeatId("A", 1)));
            When(ReserveSeats(
                Screening1,
                Marco,
                SeatA1, new SeatId("A", 2)));
            Then(SeatsReservationFailed(
                Screening1,
                Marco,
                new SeatId("A", 1)));
        }

        [Fact]
        public void LessThan15MinutesBeforeScreeningTime()
        {
            Given(ScreeningPlanned(
                Screening1,
                TooNearStartTime,
                SeatA1, SeatA2, SeatA3, SeatA4));
            When(ReserveSeats(
                Screening1,
                Marco,
                SeatA1, new SeatId("A", 2)));
            Then(SeatsReservationRequestedTooLate(
                Screening1,
                Marco,
                SeatA1, new SeatId("A", 2)));
        }
    }
}