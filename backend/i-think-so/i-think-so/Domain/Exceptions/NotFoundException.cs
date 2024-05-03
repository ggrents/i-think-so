namespace i_think_so.Domain.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string msg): base(msg) { }
    }
}
