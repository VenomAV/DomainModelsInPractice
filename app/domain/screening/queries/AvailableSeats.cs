using System;

namespace app.domain.screening.queries
{
    public class AvailableSeats
    {
        public AvailableSeats(Guid screeningId)
        {
            ScreeningId = screeningId;
        }

        public Guid ScreeningId { get; }
    }
}