using InsideSistemas.Application.Pedidos.Requests;
using InsideSistemas.Application.Pedidos.Responses;
using InsideSistemas.Domain.Models;

namespace InsideSistemas.Application.Pedidos
{
    public class PedidoAppService : IPedidoAppService
    {
        private readonly IPedidoRepository _pedidoRepository;

        public PedidoAppService(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        public async Task<PedidoResponse> ObterPedidoPorIdAsync(int pedidoId)
        {
            var pedido = await _pedidoRepository.ObterPorIdAsync(pedidoId);
            if (pedido == null)
                return null;

            return MapToPedidoDTO(pedido);
        }

        public async Task<IEnumerable<PedidoResponse>> ListarPedidosAsync()
        {
            var pedidos = await _pedidoRepository.ListarTodosAsync();
            return pedidos.Select(MapToPedidoDTO);
        }

        public async Task<PaginatedResult<PedidoResponse>> ListarPedidosPorStatusAsync(string status, int pageNumber, int pageSize)
        {
            bool estaFechado;
            if (status.Equals("aberto", StringComparison.OrdinalIgnoreCase))
            {
                estaFechado = false;
            }
            else if (status.Equals("fechado", StringComparison.OrdinalIgnoreCase))
            {
                estaFechado = true;
            }
            else
            {
                throw new ArgumentException("Status inválido. Use 'aberto' ou 'fechado'.");
            }

            var totalItems = await _pedidoRepository.ContarTotalPorStatusAsync(estaFechado);
            var pedidos = await _pedidoRepository.ListarPaginadosPorStatusAsync(estaFechado, pageNumber, pageSize);

            var pedidoQueries = pedidos.Select(MapToPedidoDTO).ToList();

            return new PaginatedResult<PedidoResponse>
            {
                Items = pedidoQueries,
                TotalItems = totalItems,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<PedidoResponse> IniciarNovoPedidoAsync()
        {
            var novoPedido = new Pedido();

            await _pedidoRepository.AdicionarAsync(novoPedido);
            await _pedidoRepository.SalvarAlteracoesAsync();

            return MapToPedidoDTO(novoPedido);
        }

        public async Task<PedidoResponse> AdicionarProdutoAoPedidoAsync(int pedidoId, ProdutoRequest produtoDto)
        {
            var pedido = await _pedidoRepository.ObterPorIdAsync(pedidoId);

            if (pedido == null)
                throw new KeyNotFoundException("Pedido não encontrado.");

            if (pedido.EstaFechado)
                throw new InvalidOperationException("Não é possível adicionar produtos a um pedido fechado.");

            var produto = new Produto(produtoDto.Nome, produtoDto.Preco, produtoDto.Quantidade);

            if (!produto.IsValid())
                throw new InvalidOperationException("O produto não é válido.");

            pedido.AdicionarProduto(produto);

            await _pedidoRepository.SalvarAlteracoesAsync();

            return MapToPedidoDTO(pedido);
        }

        public async Task<PedidoResponse> RemoverProdutoDoPedidoAsync(int pedidoId, int produtoId)
        {
            var pedido = await _pedidoRepository.ObterPorIdAsync(pedidoId);

            if (pedido == null)
                throw new KeyNotFoundException("Pedido não encontrado.");

            if (pedido.EstaFechado)
                throw new InvalidOperationException("Não é possível remover produtos de um pedido fechado.");

            var produto = pedido.Produtos.FirstOrDefault(p => p.Id == produtoId);
            if (produto == null)
                throw new KeyNotFoundException("Produto não encontrado no pedido.");

            pedido.RemoverProduto(produto);

            await _pedidoRepository.SalvarAlteracoesAsync();
            return MapToPedidoDTO(pedido);
        }

        public async Task<PedidoResponse> FecharPedidoAsync(int pedidoId)
        {
            var pedido = await _pedidoRepository.ObterPorIdAsync(pedidoId);

            if (pedido == null)
                throw new KeyNotFoundException("Pedido não encontrado.");

            if (!pedido.Produtos.Any())
                throw new InvalidOperationException("Um pedido só pode ser fechado se contiver ao menos um produto.");

            pedido.FecharPedido();

            await _pedidoRepository.SalvarAlteracoesAsync();
            return MapToPedidoDTO(pedido);
        }

        private PedidoResponse MapToPedidoDTO(Pedido pedido)
        {
            var pedidoQuery = new PedidoResponse
            {
                Id = pedido.Id,
                DataCriacao = pedido.DataCriacao,
                Status = (pedido.EstaFechado ? "fechado" : "aberto")
            };

            if (pedido.Produtos.Count > 0)
            {
                pedidoQuery.Produtos = pedido.Produtos.Select(p => new ProdutoResponse
                {
                    Id = p.Id,
                    Nome = p.Nome,
                    Preco = p.Preco,
                    Quantidade = p.Quantidade
                }).ToList();
            }

            return pedidoQuery;
        }
    }
}
