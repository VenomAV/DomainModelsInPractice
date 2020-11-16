namespace test
{
    public class ReserveSeatsHandler
    {
        private readonly Screenings _screenings;

        public ReserveSeatsHandler(Screenings screenings)
        {
            _screenings = screenings;
        }

        public void Handle(ReserveSeatsCommand command)
        {
            var screening = _screenings.Get(command.ScreeningId);

            screening.Reserve(command.CustomerId, command.Seats);
            _screenings.Save(screening);
        }
    }
}