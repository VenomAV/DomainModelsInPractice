namespace test
{
    public class ReserveSeatsHandler
    {
        private readonly EventStore _eventStore;

        public ReserveSeatsHandler(EventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public void Handle(ReserveSeatsCommand command)
        {
            var state = new ScreeningState(_eventStore.EventsFor(command.ScreeningId));
            var screening = new Screening(state, @event =>
            {
                state.Apply(@event);
                _eventStore.Add(@event);
            });

            screening.Reserve(command.CustomerId, command.Seats);
        }
    }
}