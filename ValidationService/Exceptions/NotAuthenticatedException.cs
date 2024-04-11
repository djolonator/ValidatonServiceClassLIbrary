using System;

namespace ValidationService.Exceptions
{
    public class NotAuthenticatedException : Exception
    {
        public NotAuthenticatedException() : base() { }
        public NotAuthenticatedException(string msg) : base(msg) { }
    }
}
