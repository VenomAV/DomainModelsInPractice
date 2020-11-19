using System;
using System.Collections.Generic;
using System.Linq;
using app.domain.screening.events;

namespace app.domain.screening
{
    public class AvailableSeatsReadModel : ReadModel
    {
        private readonly Dictionary<Guid, SeatId[]> _availableSeatsByScreeningId = new Dictionary<Guid, SeatId[]>();

        public AvailableSeatsReadModel(EventStore eventStore)
        {
            foreach (var @event in eventStore.History)
                Apply(@event);
        }

        public void OnEvent(Event @event)
        {
            Apply(@event);
        }

        public SeatId[] SeatsFor(Guid screeningId)
        {
            return !_availableSeatsByScreeningId.ContainsKey(screeningId)
                ? new SeatId[0]
                : _availableSeatsByScreeningId[screeningId];
        }

        private void Apply(Event @event)
        {
            switch (@event)
            {
                case ScreeningPlanned sc:
                    Apply(sc);
                    break;
                case SeatsReserved sr:
                    Apply(sr);
                    break;
            }
        }

        private void Apply(ScreeningPlanned @event)
        {
            _availableSeatsByScreeningId.Add(@event.ScreeningId, @event.Seats);
        }

        private void Apply(SeatsReserved @event)
        {
            _availableSeatsByScreeningId[@event.ScreeningId] =
                _availableSeatsByScreeningId[@event.ScreeningId]
                    .Where(id => !@event.SeatIds.Contains(id))
                    .ToArray();
        }
    }
}