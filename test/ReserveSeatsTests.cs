using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace test
{
    public class ReserveSeatsTests
    {
        [Fact]
        public void AllSeatsAvailable()
        {
            var screenings = new InMemoryScreenings();
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

            Assert.Equal(customerId, screening.Seat(new SeatId("A", 1)).ReservedBy());
            Assert.Equal(customerId, screening.Seat(new SeatId("A", 2)).ReservedBy());
        }
    }

    public class SeatId
    {
        public string Row { get; }
        public int SeatNumber { get; }

        public SeatId(string row, int seatNumber)
        {
            Row = row;
            SeatNumber = seatNumber;
        }

        protected bool Equals(SeatId other)
        {
            return Row == other.Row && SeatNumber == other.SeatNumber;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((SeatId) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Row, SeatNumber);
        }
    }

    public class ReserveSeatsCommand
    {
        public Guid ScreeningId { get; }
        public Guid CustomerId { get; }
        public SeatId[] Seats { get; }

        public ReserveSeatsCommand(Guid screeningId, Guid customerId, SeatId[] seats)
        {
            ScreeningId = screeningId;
            CustomerId = customerId;
            Seats = seats;
        }
    }

    public class Seat
    {
        public SeatId Id { get; }
        public Guid? Occupant { get; set; }

        public Seat(SeatId id)
        {
            Id = id;
        }

        public Guid? ReservedBy()
        {
            return Occupant;
        }

        public void Reserve(Guid customerId)
        {
            Occupant = customerId;
        }
    }

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

    public class InMemoryScreenings : Screenings
    {
        private readonly Dictionary<Guid, Screening> _screenings = new Dictionary<Guid, Screening>();

        public Screening Get(Guid screeningId)
        {
            return _screenings[screeningId];
        }

        public void Save(Screening screening)
        {
            _screenings[screening.Id] = screening;
        }
    }

    public interface Screenings
    {
        Screening Get(Guid screeningId);
        void Save(Screening screening);
    }

    public class Screening
    {
        public Guid Id { get; }
        public Seat[] Seats { get; }

        public Screening(Guid id, Seat[] seats)
        {
            Id = id;
            Seats = seats;
        }

        public Seat Seat(SeatId seatId)
        {
            return Seats.First(x => x.Id.Equals(seatId));
        }

        public void Reserve(Guid customerId, SeatId[] seatIds)
        {
            foreach (var seatId in seatIds)
            {
                Seat(seatId).Reserve(customerId);
            }
        }
    }
}