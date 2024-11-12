namespace InsideSistemas.Domain.Entities
{
    public class Pedido : IAggregate
    {
        public int Id { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public bool EstaFechado { get; private set; }
        public List<Produto> Produtos { get; private set; } = new List<Produto>();

        public Pedido()
        {
            DataCriacao = DateTime.UtcNow;
            EstaFechado = false;
        }

        public void AdicionarProduto(Produto produto)
        {
            if (EstaFechado)
                throw new InvalidOperationException("Produtos não podem ser adicionados a um pedido fechado.");

            Produtos.Add(produto);
        }

        public void RemoverProduto(Produto produto)
        {
            if (EstaFechado)
                throw new InvalidOperationException("Produtos não podem ser removidos de um pedido fechado.");

            Produtos.Remove(produto);
        }

        public void FecharPedido()
        {
            if (Produtos.Count == 0)
                throw new InvalidOperationException("Um pedido só pode ser fechado se contiver ao menos um produto.");

            EstaFechado = true;
        }

        public decimal CalcularTotal()
        {
            return Produtos.Sum(p => p.Preco * p.Quantidade);
        }
    }
}