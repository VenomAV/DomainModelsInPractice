using System;
using System.Linq;
using app.domain.screening;

namespace app.domain
{
    public class Reservation
    {
        public Guid ScreeningId { get; }
        public SeatId[] Seats { get; }

        public Reservation(Guid screeningId, SeatId[] seats)
        {
            ScreeningId = screeningId;
            Seats = seats;
        }

        protected bool Equals(Reservation other)
        {
            return ScreeningId.Equals(other.ScreeningId) && Seats.SequenceEqual(other.Seats);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Reservation) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ScreeningId, Seats.Select(x => x.GetHashCode()).Aggregate(HashCode.Combine));
        }
    }
}