using System;

namespace app.domain.screening.queries
{
    public class CustomerReservationsQueryHandler : Handler<CustomerReservations>
    {
        private readonly CustomerReservationsReadModel _readModel;
        private readonly Action<object> _respond;

        public CustomerReservationsQueryHandler(CustomerReservationsReadModel readModel, Action<object> respond)
        {
            _readModel = readModel;
            _respond = respond;
        }

        public void Handle(CustomerReservations message)
        {
            var reservations = _readModel.ReservationsFor(message.CustomerId);
            _respond(new CustomerReservationsResponse(message.CustomerId, reservations));
        }
    }
}