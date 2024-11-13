using InsideSistemas.Application;
using InsideSistemas.Application.Pedidos;
using InsideSistemas.Application.Pedidos.Responses;

namespace InsideSistemas.Api.GraphQL
{
    [QueryType]
    public class PedidoQuery
    {
        private readonly IPedidoAppService _pedidoAppService;

        public PedidoQuery(IPedidoAppService pedidoAppService)
        {
            _pedidoAppService = pedidoAppService;
        }

        public async Task<PedidoResponse> ObterPedidoPorId(int pedidoId)
        {
            return await _pedidoAppService.ObterPedidoPorIdAsync(pedidoId);
        }

        public async Task<IEnumerable<PedidoResponse>> ListarPedidos()
        {
            return await _pedidoAppService.ListarPedidosAsync();
        }

        public async Task<PaginatedResult<PedidoResponse>> ListarPedidosPorStatus(string status, int pageNumber = 1, int pageSize = 10)
        {
            return await _pedidoAppService.ListarPedidosPorStatusAsync(status, pageNumber, pageSize);
        }
    }
}
