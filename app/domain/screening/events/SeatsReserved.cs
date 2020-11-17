using System;
using System.Linq;

namespace app.domain.screening.events
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

        protected bool Equals(SeatsReserved other)
        {
            return ScreeningId.Equals(other.ScreeningId) && 
                   CustomerId.Equals(other.CustomerId) &&
                   SeatIds.SequenceEqual(other.SeatIds);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((SeatsReserved) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(
                ScreeningId,
                CustomerId,
                SeatIds
                    .Select(x => x.GetHashCode())
                    .Aggregate(HashCode.Combine));
        }
    }
}