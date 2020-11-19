using System;
using System.Linq;

namespace app.domain.screening.events
{
    public class ScreeningPlanned : Event
    {
        public Guid ScreeningId { get; }
        public DateTime StartTime { get; }
        public SeatId[] Seats { get; }
        public Guid StreamId => ScreeningId;

        public ScreeningPlanned(Guid screeningId, DateTime startTime, SeatId[] seats)
        {
            ScreeningId = screeningId;
            StartTime = startTime;
            Seats = seats;
        }

        protected bool Equals(ScreeningPlanned other)
        {
            return ScreeningId.Equals(other.ScreeningId) && Seats.SequenceEqual(other.Seats);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((ScreeningPlanned) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(
                ScreeningId,
                Seats
                    .Select(x => x.GetHashCode())
                    .Aggregate(HashCode.Combine));
        }
    }
}