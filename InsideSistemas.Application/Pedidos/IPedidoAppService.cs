namespace InsideSistemas.Application.Pedidos
{
    public interface IPedidoAppService
    {
        Task<PedidoQuery> IniciarNovoPedidoAsync();

        Task<PedidoQuery> AdicionarProdutoAoPedidoAsync(int pedidoId, ProdutoCommand produto);

        Task<PedidoQuery> RemoverProdutoDoPedidoAsync(int pedidoId, int produtoId);

        Task<PedidoQuery> FecharPedidoAsync(int pedidoId);

        Task<IEnumerable<PedidoQuery>> ListarPedidosAsync();

        Task<PedidoQuery> ObterPedidoPorIdAsync(int pedidoId);
    }
}