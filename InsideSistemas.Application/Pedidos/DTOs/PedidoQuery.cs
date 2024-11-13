namespace InsideSistemas.Application.Pedidos.DTOs
{
    public class PedidoQuery
    {
        public int Id { get; set; }

        public DateTime DataCriacao { get; set; }

        public string Status { get; set; }

        public List<ProdutoQuery> Produtos { get; set; }
    }
}
