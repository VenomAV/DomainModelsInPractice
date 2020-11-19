using System;
using app.domain.screening;
using app.domain.screening.commands;
using app.domain.screening.events;
using app.domain.screening.queries;

namespace test
{
    public static class SemanticalHelper
    {
        public static ScreeningPlanned ScreeningPlanned(Guid screeningId, DateTime startTime, params SeatId[] seats) =>
            new ScreeningPlanned(screeningId, startTime, seats);

        public static SeatsReserved SeatsReserved(Guid screeningId, Guid customerId, params SeatId[] seats) =>
            new SeatsReserved(screeningId, customerId, seats);

        public static SeatsReservationFailed SeatsReservationFailed(Guid screeningId, Guid customerId,
            params SeatId[] seats) =>
            new SeatsReservationFailed(screeningId, customerId, seats);

        public static ReserveSeats ReserveSeats(Guid screeningId, Guid customerId, params SeatId[] seats) =>
            new ReserveSeats(screeningId, customerId, seats);

        public static AvailableSeats AvailableSeats(Guid screeningId) => new AvailableSeats(screeningId);

        public static AvailableSeatsResponse AvailableSeatsResponse(Guid screeningId, params SeatId[] seats) =>
            new AvailableSeatsResponse(screeningId, seats);

        public static SeatsReservationRequestedTooLate SeatsReservationRequestedTooLate(
            Guid screeningId,
            Guid customerId,
            params SeatId[] seats
        ) =>
            new SeatsReservationRequestedTooLate(screeningId, customerId, seats);

        public static Guid Screening1 { get; } = Guid.NewGuid();
        public static Guid Marco { get; } = Guid.NewGuid();
        public static Guid Andrea { get; } = Guid.NewGuid();
        public static DateTime FarEnoughStartTime => DateTime.Now.AddDays(1);
        public static DateTime TooNearStartTime => DateTime.Now.AddMinutes(10);
        
        public static SeatId SeatA1 {get;} = new SeatId("A", 1);
        public static SeatId SeatA2 {get;} = new SeatId("A", 2);
        public static SeatId SeatA3 {get;} = new SeatId("A", 3);
        public static SeatId SeatA4 {get;} = new SeatId("A", 4);
    }
}