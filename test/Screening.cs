using System;
using System.Linq;

namespace test
{
    public class Screening
    {
        public Guid Id { get; }
        public Seat[] Seats { get; }

        public Screening(Guid id, Seat[] seats)
        {
            Id = id;
            Seats = seats;
        }

        public Seat Seat(SeatId seatId)
        {
            return Seats.First(x => x.Id.Equals(seatId));
        }

        public void Reserve(Guid customerId, SeatId[] seatIds)
        {
            foreach (var seatId in seatIds)
            {
                Seat(seatId).Reserve(customerId);
            }
        }
    }
}