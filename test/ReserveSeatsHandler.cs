using System;

namespace test
{
    public class ReserveSeatsHandler
    {
        private readonly EventStore _eventStore;
        private readonly Action<Event> _publish;

        public ReserveSeatsHandler(EventStore eventStore, Action<Event> publish)
        {
            _eventStore = eventStore;
            _publish = publish;
        }

        public void Handle(ReserveSeatsCommand command)
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