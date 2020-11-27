using System;

namespace app.domain.screening.queries
{
    public class CustomerReservations
    {
        public Guid CustomerId { get; }

        public CustomerReservations(Guid customerId)
        {
            CustomerId = customerId;
        }

        protected bool Equals(CustomerReservations other)
        {
            return CustomerId.Equals(other.CustomerId);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((CustomerReservations) obj);
        }

        public override int GetHashCode()
        {
            return CustomerId.GetHashCode();
        }
    }
}