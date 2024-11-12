using InsideSistemas.Application.DTOs;

namespace InsideSistemas.Application.Services
{
    public interface IPedidoAppService
    {
        Task<PedidoDTO> IniciarNovoPedidoAsync();

        Task<PedidoDTO> AdicionarProdutoAoPedidoAsync(int pedidoId, ProdutoDTO produto);

        Task<PedidoDTO> RemoverProdutoDoPedidoAsync(int pedidoId, int produtoId);

        Task<PedidoDTO> FecharPedidoAsync(int pedidoId);

        Task<IEnumerable<PedidoDTO>> ListarPedidosAsync();

        Task<PedidoDTO> ObterPedidoPorIdAsync(int pedidoId);
    }
}