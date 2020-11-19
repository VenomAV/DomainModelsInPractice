using System;
using System.Linq;
using app.domain.screening.events;

namespace app.domain.screening
{
    public class ScreeningState
    {
        public Guid Id { get; private set; }
        public Seat[] Seats { get; private set; }
        public DateTime StartTime { get; private set; }

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
                case ScreeningPlanned sc:
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

        private void Apply(ScreeningPlanned screeningPlanned)
        {
            Id = screeningPlanned.ScreeningId;
            Seats = screeningPlanned.Seats.Select(id => new Seat(id)).ToArray();
            StartTime = screeningPlanned.StartTime;
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