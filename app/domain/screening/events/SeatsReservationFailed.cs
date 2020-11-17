using System;
using System.Linq;

namespace app.domain.screening.events
{
    public class SeatsReservationFailed : Event
    {
        public Guid ScreeningId { get; }
        public Guid CustomerId { get; }
        public SeatId[] AlreadyReservedSeats { get; }
        public Guid StreamId => ScreeningId;

        public SeatsReservationFailed(Guid screeningId, Guid customerId, SeatId[] alreadyReservedSeats)
        {
            ScreeningId = screeningId;
            CustomerId = customerId;
            AlreadyReservedSeats = alreadyReservedSeats;
        }

        protected bool Equals(SeatsReservationFailed other)
        {
            return ScreeningId.Equals(other.ScreeningId) && 
                   CustomerId.Equals(other.CustomerId) && 
                   AlreadyReservedSeats.SequenceEqual(other.AlreadyReservedSeats);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((SeatsReservationFailed) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(
                ScreeningId,
                CustomerId,
                AlreadyReservedSeats
                    .Select(x => x.GetHashCode())
                    .Aggregate(HashCode.Combine));
        }
    }
}