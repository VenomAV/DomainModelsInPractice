using System;
using System.Linq;

namespace app.domain.screening.queries
{
    public class AvailableSeatsResponse
    {
        public SeatId[] Seats { get; }

        public AvailableSeatsResponse(SeatId[] seats)
        {
            Seats = seats;
        }

        protected bool Equals(AvailableSeatsResponse other)
        {
            return Seats.SequenceEqual(other.Seats);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((AvailableSeatsResponse) obj);
        }

        public override int GetHashCode()
        {
            return Seats.Select(x => x.GetHashCode()).Aggregate(HashCode.Combine);
        }
    }
}