namespace InsideSistemas.Domain.ValueObjects
{
    public class Endereco
    {
        public string Rua { get; private set; }
        public string Cidade { get; private set; }
        public string CEP { get; private set; }

        public Endereco(string rua, string cidade, string cep)
        {
            Rua = rua;
            Cidade = cidade;
            CEP = cep;
        }

        // Sobrescreva Equals e GetHashCode para comparar objetos de valor
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var endereco = (Endereco)obj;
            return Rua == endereco.Rua && Cidade == endereco.Cidade && CEP == endereco.CEP;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Rua, Cidade, CEP);
        }
    }
}
