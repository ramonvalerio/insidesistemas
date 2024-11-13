namespace InsideSistemas.Application.Pedidos.DTOs
{
    public class PedidoQuery
    {
        public int Id { get; set; }

        public DateTime DataCriacao { get; set; }

        public bool EstaFechado { get; set; }

        public List<ProdutoQuery> Produtos { get; set; }

        public PedidoQuery()
        {
            Produtos = new List<ProdutoQuery>();
        }
    }
}
