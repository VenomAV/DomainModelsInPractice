using System;

namespace test
{
    public class ReserveSeatsCommand
    {
        public Guid ScreeningId { get; }
        public Guid CustomerId { get; }
        public SeatId[] Seats { get; }

        public ReserveSeatsCommand(Guid screeningId, Guid customerId, SeatId[] seats)
        {
            ScreeningId = screeningId;
            CustomerId = customerId;
            Seats = seats;
        }
    }
}