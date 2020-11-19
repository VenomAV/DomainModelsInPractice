using System;

namespace app.domain.screening.commands
{
    public class ReserveSeats
    {
        public Guid ScreeningId { get; }
        public Guid CustomerId { get; }
        public SeatId[] Seats { get; }

        public ReserveSeats(Guid screeningId, Guid customerId, SeatId[] seats)
        {
            ScreeningId = screeningId;
            CustomerId = customerId;
            Seats = seats;
        }
    }
}