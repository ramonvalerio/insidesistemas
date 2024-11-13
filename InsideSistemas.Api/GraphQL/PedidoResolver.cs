using InsideSistemas.Application.Pedidos;
using InsideSistemas.Application.Pedidos.Responses;

namespace InsideSistemas.Api.GraphQL
{
    public class PedidoResolver
    {
        private readonly IPedidoAppService _pedidoAppService;

        public PedidoResolver(IPedidoAppService pedidoAppService)
        {
            _pedidoAppService = pedidoAppService;
        }

        public async Task<ProdutoResponse> GetProdutoAsync(
            [Parent] PedidoResponse pedido,
            int produtoId)
        {
            var pedidoCompleto = await _pedidoAppService.ObterPedidoPorIdAsync(pedido.Id);
            if (pedidoCompleto == null)
            {
                return null;
            }

            var produto = pedidoCompleto.Produtos.FirstOrDefault(p => p.Id == produtoId);
            return produto;
        }

        public async Task<string> GetStatusAsync([Parent] PedidoResponse pedido)
        {
            // Você poderia usar o _pedidoAppService para realizar alguma validação ou transformação
            var pedidoCompleto = await _pedidoAppService.ObterPedidoPorIdAsync(pedido.Id);
            return pedidoCompleto?.Status.ToUpper() ?? "STATUS DESCONHECIDO";
        }
    }
}
