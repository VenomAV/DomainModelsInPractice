using System;
using System.Linq;

namespace app.domain.screening.events
{
    public class SeatsReservationRequestedTooLate : Event
    {
        public Guid ScreeningId { get; }
        public Guid CustomerId { get; }
        public SeatId[] Seats { get; }
        public Guid StreamId => ScreeningId;

        public SeatsReservationRequestedTooLate(Guid screeningId, Guid customerId, SeatId[] seats)
        {
            ScreeningId = screeningId;
            CustomerId = customerId;
            Seats = seats;
        }

        protected bool Equals(SeatsReservationRequestedTooLate other)
        {
            return ScreeningId.Equals(other.ScreeningId) &&
                   CustomerId.Equals(other.CustomerId) &&
                   Seats.SequenceEqual(other.Seats);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((SeatsReservationRequestedTooLate) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(
                ScreeningId,
                CustomerId,
                Seats
                    .Select(x => x.GetHashCode())
                    .Aggregate(HashCode.Combine));
        }
    }
}