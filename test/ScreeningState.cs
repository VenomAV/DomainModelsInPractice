using System;
using System.Linq;

namespace test
{
    public class ScreeningState
    {
        public Guid Id { get; set; }
        public Seat[] Seats { get; set; }

        public ScreeningState(Event[] events)
        {
            foreach (var @event in events)
            {
                Apply(@event);
            }
        }

        public void Apply(Event @event)
        {
            switch (@event)
            {
                case ScreeningCreated sc:
                    Apply(sc);
                    break;
                case SeatsReserved sr:
                    Apply(sr);
                    break;
            }
        }

        public Seat Seat(SeatId seatId)
        {
            return Seats.First(x => x.Id.Equals(seatId));
        }

        private void Apply(ScreeningCreated screeningCreated)
        {
            Id = screeningCreated.ScreeningId;
            Seats = screeningCreated.Seats;
        }

        private void Apply(SeatsReserved seatsReserved)
        {
            foreach (var seatId in seatsReserved.SeatIds)
            {
                Seat(seatId).Reserve(seatsReserved.CustomerId);
            }
        }
    }
}