using System;
using Xunit;

namespace test
{
    public class ReserveSeatsTests
    {
        [Fact]
        public void AllSeatsAvailable()
        {
            Screenings screenings = new InMemoryScreenings();
            var handler = new ReserveSeatsHandler(screenings);
            var customerId = Guid.NewGuid();
            var screeningId = Guid.NewGuid();

            screenings.Save(new Screening(
                screeningId,
                new[]
                {
                    new Seat(new SeatId("A", 1)),
                    new Seat(new SeatId("A", 2)),
                    new Seat(new SeatId("A", 3)),
                    new Seat(new SeatId("A", 4)),
                }));

            var command = new ReserveSeatsCommand(
                screeningId,
                customerId,
                new[]
                {
                    new SeatId("A", 1),
                    new SeatId("A", 2)
                });

            handler.Handle(command);

            var screening = screenings.Get(screeningId);

            Assert.Equal(customerId, screening.Seat("A", 1).ReservedBy());
            Assert.Equal(customerId, screening.Seat("A", 2).ReservedBy());
        }
    }

    public class SeatId
    {
        public SeatId(string row, int seatNumber)
        {
            throw new NotImplementedException();
        }
    }

    public class ReserveSeatsCommand
    {
        public ReserveSeatsCommand(Guid screeningId, Guid customerId, SeatId[] seats)
        {
            throw new NotImplementedException();
        }
    }

    public class Seat
    {
        public Seat(SeatId id)
        {
            throw new NotImplementedException();
        }

        public Guid ReservedBy()
        {
            throw new NotImplementedException();
        }
    }

    public class ReserveSeatsHandler
    {
        public ReserveSeatsHandler(Screenings screenings)
        {
            throw new NotImplementedException();
        }

        public void Handle(ReserveSeatsCommand command)
        {
            throw new NotImplementedException();
        }
    }

    public class InMemoryScreenings : Screenings
    {
        public Screening Get(Guid screeningId)
        {
            throw new NotImplementedException();
        }

        public void Save(Screening screening)
        {
            throw new NotImplementedException();
        }
    }

    public interface Screenings
    {
        Screening Get(Guid screeningId);
        void Save(Screening screening);
    }

    public class Screening
    {
        public Screening(Guid screeningId, Seat[] seats)
        {
            throw new NotImplementedException();
        }

        public Seat Seat(string row, int seatNumber)
        {
            throw new NotImplementedException();
        }
    }
}