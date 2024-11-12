using InsideSistemas.Domain.Entities;

namespace InsideSistemas.Domain.Repositories
{
    public interface IPedidoRepository
    {
        Task<Pedido> ObterPorIdAsync(int id);
        Task<IEnumerable<Pedido>> ListarTodosAsync();
        Task AdicionarAsync(Pedido pedido);
        Task SalvarAlteracoesAsync();
    }
}
