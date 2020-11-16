using System;
using System.Collections.Generic;
using System.Linq;

namespace test
{
    public class Screening : EventSourcedAggregate
    {
        private Guid Id { get; set; }
        private Seat[] Seats { get; set; }
        private readonly List<Event> _unpublishedEvents = new List<Event>();

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
            _unpublishedEvents.Add(seatReserved);
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

        public IEnumerable<Event> UnpublishedEvents => _unpublishedEvents;
    }
}