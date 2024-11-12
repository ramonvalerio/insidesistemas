namespace InsideSistemas.Domain.Exceptions
{
    public class PedidoInvalidoException : Exception
    {
        public PedidoInvalidoException(string message) : base(message)
        {
        }
    }
}