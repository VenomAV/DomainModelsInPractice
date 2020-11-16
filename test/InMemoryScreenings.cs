using System;
using System.Collections.Generic;

namespace test
{
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
}