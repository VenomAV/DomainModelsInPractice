using System;

namespace app
{
    public class DomainException : Exception
    {
        public DomainException(string message) : base(message)
        {
        }
    }
}