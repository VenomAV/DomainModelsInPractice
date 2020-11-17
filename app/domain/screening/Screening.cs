using System;
using app.domain.screening.events;

namespace app.domain.screening
{
    public class Screening
    {
        private ScreeningState _screening;
        private readonly Action<Event> _publish;

        public Screening(ScreeningState state, Action<Event> publish)
        {
            _screening = state;
            _publish = publish;
        }

        public void Reserve(Guid customerId, SeatId[] seatIds)
        {
            _publish(new SeatsReserved(_screening.Id, customerId, seatIds));
        }
    }
}