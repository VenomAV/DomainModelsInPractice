using System;

namespace app.domain.screening.queries
{
    public class AvailableSeatsQueryHandler: Handler<AvailableSeats>
    {
        private readonly AvailableSeatsReadModel _readModel;
        private readonly Action<object> _respond;

        public AvailableSeatsQueryHandler(AvailableSeatsReadModel readModel, Action<object> respond)
        {
            _readModel = readModel;
            _respond = respond;
        }

        public void Handle(AvailableSeats query)
        {
            var seats = _readModel.SeatsFor(query.ScreeningId);
            _respond(new AvailableSeatsResponse(seats));
        }
    }
}