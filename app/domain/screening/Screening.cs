using System;
using System.Linq;
using app.domain.screening.events;

namespace app.domain.screening
{
    public class Screening
    {
        private readonly ScreeningState _screening;
        private readonly Action<Event> _publish;

        public Screening(ScreeningState state, Action<Event> publish)
        {
            _screening = state;
            _publish = publish;
        }

        public void Reserve(Guid customerId, SeatId[] seatIds)
        {
            var alreadyReservedSeats = seatIds
                .Select(id => _screening.Seat(id))
                .Where(seat => seat.Occupant.HasValue)
                .Select(x => x.Id)
                .ToArray();

            if ( _screening.StartTime - DateTime.Now <= TimeSpan.FromMinutes(15))
                _publish(new SeatsReservationRequestedTooLate(_screening.Id, customerId, seatIds));
            else if (alreadyReservedSeats.Any())
                _publish(new SeatsReservationFailed(_screening.Id, customerId, alreadyReservedSeats));
            else
                _publish(new SeatsReserved(_screening.Id, customerId, seatIds));
        }
    }
}