﻿using InsideSistemas.Domain.Entities;
using InsideSistemas.Domain.Repositories;

namespace InsideSistemas.Application.Pedidos
{
    public class PedidoAppService : IPedidoAppService
    {
        private readonly IPedidoRepository _pedidoRepository;

        public PedidoAppService(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        public async Task<PedidoQuery> IniciarNovoPedidoAsync()
        {
            var novoPedido = new Pedido();

            await _pedidoRepository.AdicionarAsync(novoPedido);
            await _pedidoRepository.SalvarAlteracoesAsync();

            return MapToPedidoDTO(novoPedido);
        }

        public async Task<PedidoQuery> AdicionarProdutoAoPedidoAsync(int pedidoId, ProdutoCommand produtoDto)
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

        public async Task<PedidoQuery> RemoverProdutoDoPedidoAsync(int pedidoId, int produtoId)
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

        public async Task<PedidoQuery> FecharPedidoAsync(int pedidoId)
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

        public async Task<IEnumerable<PedidoQuery>> ListarPedidosAsync()
        {
            var pedidos = await _pedidoRepository.ListarTodosAsync();
            return pedidos.Select(MapToPedidoDTO);
        }

        public async Task<PedidoQuery> ObterPedidoPorIdAsync(int pedidoId)
        {
            var pedido = await _pedidoRepository.ObterPorIdAsync(pedidoId);
            if (pedido == null)
                return null;

            return MapToPedidoDTO(pedido);
        }

        private PedidoQuery MapToPedidoDTO(Pedido pedido)
        {
            return new PedidoQuery
            {
                Id = pedido.Id,
                DataCriacao = pedido.DataCriacao,
                EstaFechado = pedido.EstaFechado,
                Produtos = pedido.Produtos.Select(p => new ProdutoQuery
                {
                    Id = p.Id,
                    Nome = p.Nome,
                    Preco = p.Preco,
                    Quantidade = p.Quantidade
                }).ToList()
            };
        }
    }
}
