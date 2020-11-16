using System;
using System.Collections.Generic;
using System.Linq;

namespace test
{
    public class Screening : EventSourcedAggregate
    {
        public Guid Id { get; private set; }
        public Seat[] Seats { get; private set; }

        public List<Event> _unpublishedEvent = new List<Event>();

        public Screening(Event[] events)
        {
            foreach (var @event in events)
            {
                Apply(@event);
            }
        }

        public Seat Seat(SeatId seatId)
        {
            return Seats.First(x => x.Id.Equals(seatId));
        }

        public void Reserve(Guid customerId, SeatId[] seatIds)
        {
            var seatReserved = new SeatsReserved(Id, customerId, seatIds);
            Apply(seatReserved);
            _unpublishedEvent.Add(seatReserved);
        }

        private void Apply(Event @event)
        {
            switch (@event)
            {
                case ScreeningCreated sc:
                    Apply(sc);
                    break;
                case SeatsReserved sr:
                    Apply(sr);
                    break;
            }
        }

        private void Apply(ScreeningCreated screeningCreated)
        {
            Id = screeningCreated.ScreeningId;
            Seats = screeningCreated.Seats;
        }

        private void Apply(SeatsReserved seatsReserved)
        {
            foreach (var seatId in seatsReserved.SeatIds)
            {
                Seat(seatId).Reserve(seatsReserved.CustomerId);
            }
        }

        public Event[] UnpublishedEvents => _unpublishedEvent.ToArray();
    }
}