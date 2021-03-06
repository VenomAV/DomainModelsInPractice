using System;

namespace app.domain.screening
{
    public class SeatId
    {
        private string Row { get; }
        private int SeatNumber { get; }

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
            if (obj.GetType() != GetType()) return false;
            return Equals((SeatId) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Row, SeatNumber);
        }
    }
}