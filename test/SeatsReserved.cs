using System;

namespace test
{
    public class SeatsReserved : Event
    {
        public Guid ScreeningId { get; }
        public Guid CustomerId { get; }
        public SeatId[] SeatIds { get; }

        public SeatsReserved(Guid screeningId, Guid customerId, SeatId[] seatIds)
        {
            ScreeningId = screeningId;
            CustomerId = customerId;
            SeatIds = seatIds;
        }

        public Guid StreamId => ScreeningId;
    }
}