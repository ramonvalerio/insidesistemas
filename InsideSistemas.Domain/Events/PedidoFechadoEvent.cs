namespace InsideSistemas.Domain.Events
{
    public class PedidoFechadoEvent
    {
        public int PedidoId { get; private set; }
        public DateTime DataFechamento { get; private set; }

        public PedidoFechadoEvent(int pedidoId)
        {
            PedidoId = pedidoId;
            DataFechamento = DateTime.UtcNow;
        }
    }
}