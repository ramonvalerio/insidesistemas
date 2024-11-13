using InsideSistemas.Application.Pedidos.Requests;
using InsideSistemas.Application.Pedidos.Responses;

namespace InsideSistemas.Application.Pedidos
{
    public interface IPedidoAppService
    {
        Task<PedidoResponse> ObterPedidoPorIdAsync(int pedidoId);

        Task<IEnumerable<PedidoResponse>> ListarPedidosAsync();

        Task<PaginatedResult<PedidoResponse>> ListarPedidosPorStatusAsync(string status, int pageNumber, int pageSize);

        Task<PedidoResponse> IniciarNovoPedidoAsync();

        Task<PedidoResponse> AdicionarProdutoAoPedidoAsync(int pedidoId, ProdutoRequest produto);

        Task<PedidoResponse> RemoverProdutoDoPedidoAsync(int pedidoId, int produtoId);

        Task<PedidoResponse> FecharPedidoAsync(int pedidoId);
    }
}