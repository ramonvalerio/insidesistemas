using InsideSistemas.Application.DTOs;

namespace InsideSistemas.Application.Services
{
    public class PedidoAppService : IPedidoAppService
    {
        public PedidoAppService()
        {
                
        }

        public Task<PedidoDTO> AdicionarProdutoAoPedidoAsync(int pedidoId, ProdutoDTO produto)
        {
            throw new NotImplementedException();
        }

        public Task<PedidoDTO> FecharPedidoAsync(int pedidoId)
        {
            throw new NotImplementedException();
        }

        public Task<PedidoDTO> IniciarNovoPedidoAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PedidoDTO>> ListarPedidosAsync()
        {
            throw new NotImplementedException();
        }

        public Task<PedidoDTO> ObterPedidoPorIdAsync(int pedidoId)
        {
            throw new NotImplementedException();
        }

        public Task<PedidoDTO> RemoverProdutoDoPedidoAsync(int pedidoId, int produtoId)
        {
            throw new NotImplementedException();
        }
    }
}
