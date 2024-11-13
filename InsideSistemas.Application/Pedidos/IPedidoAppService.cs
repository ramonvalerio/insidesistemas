using InsideSistemas.Application.Pedidos.Commands;
using InsideSistemas.Application.Pedidos.DTOs;

namespace InsideSistemas.Application.Pedidos
{
    public interface IPedidoAppService
    {
        Task<PedidoQuery> ObterPedidoPorIdAsync(int pedidoId);

        Task<IEnumerable<PedidoQuery>> ListarPedidosAsync();

        Task<PaginatedResult<PedidoQuery>> ListarPedidosPorStatusAsync(string status, int pageNumber, int pageSize);

        Task<PedidoQuery> IniciarNovoPedidoAsync();

        Task<PedidoQuery> AdicionarProdutoAoPedidoAsync(int pedidoId, ProdutoCommand produto);

        Task<PedidoQuery> RemoverProdutoDoPedidoAsync(int pedidoId, int produtoId);

        Task<PedidoQuery> FecharPedidoAsync(int pedidoId);
    }
}