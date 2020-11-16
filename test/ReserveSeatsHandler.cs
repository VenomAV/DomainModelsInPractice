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
            var screening = new Screening(_eventStore.EventsFor(command.ScreeningId));

            screening.Reserve(command.CustomerId, command.Seats);

            SaveInEventStore(screening);
        }

        private void SaveInEventStore(EventSourcedAggregate screening)
        {
            foreach (var @event in screening.UnpublishedEvents)
            {
                _eventStore.Add(@event);
            }
        }
    }
}