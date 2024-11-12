namespace InsideSistemas.Application.DTOs
{
    public class PedidoDTO
    {
        public int Id { get; set; }

        public DateTime DataCriacao { get; set; }

        public bool EstaFechado { get; set; }

        public List<ProdutoDTO> Produtos { get; set; } = new List<ProdutoDTO>();
    }
}
