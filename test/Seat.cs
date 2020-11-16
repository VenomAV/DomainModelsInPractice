using System;

namespace test
{
    public class Seat
    {
        public SeatId Id { get; }
        public Guid? Occupant { get; set; }

        public Seat(SeatId id)
        {
            Id = id;
        }

        public Guid? ReservedBy()
        {
            return Occupant;
        }

        public void Reserve(Guid customerId)
        {
            Occupant = customerId;
        }
    }
}