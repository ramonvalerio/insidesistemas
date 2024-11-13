namespace InsideSistemas.Domain.Models
{
    public class Produto
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public decimal Preco { get; private set; }
        public int Quantidade { get; private set; }

        public Produto(string nome, decimal preco, int quantidade)
        {
            Nome = nome;
            Preco = preco;
            Quantidade = quantidade;
        }

        public bool IsValid()
        {
            if (string.IsNullOrWhiteSpace(Nome) || Nome.Length > 100)
                return false;

            if (Preco <= 0)
                return false;

            if (Quantidade <= 0)
                return false;

            return true;
        }
    }
}