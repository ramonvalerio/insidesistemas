namespace InsideSistemas.Application.Pedidos.Requests
{
    public class ProdutoRequest
    {
        public string Nome { get; set; }

        public decimal Preco { get; set; }

        public int Quantidade { get; set; }
    }
}