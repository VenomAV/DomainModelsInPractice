using System;
using System.Linq;

namespace app.domain.screening.queries
{
    public class CustomerReservationsResponse
    {
        public Guid CustomerId { get; }
        public Reservation[] Reservations { get; }

        public CustomerReservationsResponse(Guid customerId, Reservation[] reservations)
        {
            CustomerId = customerId;
            Reservations = reservations;
        }

        protected bool Equals(CustomerReservationsResponse other)
        {
            return CustomerId.Equals(other.CustomerId) && Reservations.SequenceEqual(other.Reservations);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((CustomerReservationsResponse) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(CustomerId, Reservations.Select(x => x.GetHashCode()).Aggregate(HashCode.Combine));
        }
    }
}