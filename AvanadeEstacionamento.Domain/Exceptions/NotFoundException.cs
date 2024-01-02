namespace AvanadeEstacionamento.Domain.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string error) : base(error) { }

    }
}
