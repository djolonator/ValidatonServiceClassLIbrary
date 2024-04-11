
namespace ValidationService.Exceptions
{
    public class NotValidatedException: Exception
    {
        public NotValidatedException() : base() { }
        public NotValidatedException(string msg) : base(msg) { }
    }
}
