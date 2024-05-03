namespace i_think_so.Domain.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string msg) : base(msg) { }
    }
}
