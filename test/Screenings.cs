using System;

namespace test
{
    public interface Screenings
    {
        Screening Get(Guid screeningId);
        void Save(Screening screening);
    }
}