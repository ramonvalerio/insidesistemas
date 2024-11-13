namespace InsideSistemas.Application.Pedidos.Responses
{
    public class PedidoResponse
    {
        public int Id { get; set; }

        public DateTime DataCriacao { get; set; }

        public string Status { get; set; }

        public List<ProdutoResponse>? Produtos { get; set; }
    }
}
