namespace InsideSistemas.Domain.Models
{
    public interface IPedidoRepository
    {
        Task<Pedido> ObterPorIdAsync(int id);
        Task<IEnumerable<Pedido>> ListarTodosAsync();
        Task<IEnumerable<Pedido>> ListarPaginadosPorStatusAsync(bool status, int pageNumber, int pageSize);
        Task<int> ContarTotalPorStatusAsync(bool status);
        Task AdicionarAsync(Pedido pedido);
        Task SalvarAlteracoesAsync();
    }
}
