using System;

namespace test
{
    public class ScreeningCreated : Event
    {
        public Guid ScreeningId { get; }
        public Seat[] Seats { get; }
        public Guid StreamId => ScreeningId;

        public ScreeningCreated(Guid screeningId, Seat[] seats)
        {
            ScreeningId = screeningId;
            Seats = seats;
        }
    }
}