using System;

namespace app.domain.screening.commands
{
    public class ReserveSeatsHandler : Handler<ReserveSeats>
    {
        private readonly EventStore _eventStore;
        private readonly Action<Event> _publish;

        public ReserveSeatsHandler(EventStore eventStore, Action<Event> publish)
        {
            _eventStore = eventStore;
            _publish = publish;
        }

        public void Handle(ReserveSeats command)
        {
            var state = new ScreeningState(_eventStore.EventsFor(command.ScreeningId));
            var screening = new Screening(state, PublishWith(state));

            screening.Reserve(command.CustomerId, command.Seats);
        }

        Action<Event> PublishWith(ScreeningState state) => (Event @event) =>
        {
            state.Apply(@event);
            _publish(@event);
        };
    }
}