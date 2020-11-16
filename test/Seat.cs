using System;

namespace test
{
    public class Seat
    {
        public SeatId Id { get; }
        public Guid? Occupant { get; private set; }

        public Seat(SeatId id)
        {
            Id = id;
        }

        public void Reserve(Guid customerId)
        {
            Occupant = customerId;
        }

        protected bool Equals(Seat other)
        {
            return Equals(Id, other.Id) && Nullable.Equals(Occupant, other.Occupant);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Seat) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Occupant);
        }
    }
}