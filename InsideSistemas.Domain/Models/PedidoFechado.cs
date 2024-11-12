namespace InsideSistemas.Domain.Models
{
    public class PedidoFechado
    {
        public int PedidoId { get; private set; }
        public DateTime DataFechamento { get; private set; }

        public PedidoFechado(int pedidoId)
        {
            PedidoId = pedidoId;
            DataFechamento = DateTime.UtcNow;
        }
    }
}