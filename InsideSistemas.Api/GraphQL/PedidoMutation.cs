using InsideSistemas.Application.Pedidos;
using InsideSistemas.Application.Pedidos.Requests;
using InsideSistemas.Application.Pedidos.Responses;

namespace InsideSistemas.Api.GraphQL
{
    public class PedidoMutation
    {
        private readonly IPedidoAppService _pedidoAppService;

        public PedidoMutation(IPedidoAppService pedidoAppService)
        {
            _pedidoAppService = pedidoAppService;
        }

        public async Task<PedidoResponse> IniciarNovoPedido()
        {
            return await _pedidoAppService.IniciarNovoPedidoAsync();
        }

        public async Task<PedidoResponse> AdicionarProdutoAoPedido(int pedidoId, ProdutoRequest produto)
        {
            return await _pedidoAppService.AdicionarProdutoAoPedidoAsync(pedidoId, produto);
        }

        public async Task<PedidoResponse> RemoverProdutoDoPedido(int pedidoId, int produtoId)
        {
            return await _pedidoAppService.RemoverProdutoDoPedidoAsync(pedidoId, produtoId);
        }

        public async Task<PedidoResponse> FecharPedido(int pedidoId)
        {
            return await _pedidoAppService.FecharPedidoAsync(pedidoId);
        }
    }
}