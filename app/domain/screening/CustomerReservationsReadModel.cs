using System;
using System.Collections.Generic;
using app.domain.screening.events;

namespace app.domain
{
    public class CustomerReservationsReadModel : ReadModel
    {
        Dictionary<Guid, List<Reservation>> _reservationsByCustomerId = new Dictionary<Guid, List<Reservation>>();
        public CustomerReservationsReadModel(EventStore eventStore)
        {
            foreach (var @event in eventStore.History)
                Apply(@event);
        }

        public void OnEvent(Event @event)
        {
            Apply(@event);
        }

        public Reservation[] ReservationsFor(Guid customerId)
        {
            return _reservationsByCustomerId.ContainsKey(customerId)
                ? _reservationsByCustomerId[customerId].ToArray()
                : new Reservation[0];
        }

        private void Apply(Event @event)
        {
            switch (@event)
            {
                case SeatsReserved sr:
                    Apply(sr);
                    break;
            }
        }

        private void Apply(SeatsReserved seatsReserved)
        {
            if (!_reservationsByCustomerId.ContainsKey(seatsReserved.CustomerId))
            {
                _reservationsByCustomerId[seatsReserved.CustomerId] = new List<Reservation>();
            }
            var customerReservations = _reservationsByCustomerId[seatsReserved.CustomerId];
            customerReservations.Add(new Reservation(seatsReserved.ScreeningId, seatsReserved.SeatIds));
        }
    }
}